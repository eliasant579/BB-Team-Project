﻿
/*  Created by: Brick Beaker Team 1
 *  Project: Brick Breaker
 *  Date: Tuesday, April 4th
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Xml;
using System.Diagnostics;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        static UserControl uc = new UserControl();
        #region global values
        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown, upArrowDown, onPaddle = true, aKeyDown, dKeyDown, smallPaddle, largePaddle, fastBoi, slowBoi;

        // Game values
        static int lives;
        int score;
        public static Boolean Twoplayer = false;
        int level = 1;
        int ballStartX, ballStartY, paddleStartX, paddleStartY, ballStartSpeedX = 8, ballStartSpeedY = -8;

        Random rng = new Random();

        // constants
        const int BALLSPEED = 6;
        const int PADDLESPEED = 12;
        const int PADDLEWIDTH = 80; const int PADDLEHEIGHT = 20;
        // Paddle and Ball objects
        public static Paddle paddle; public static Ball ball;

        // list of all blocks and paddles for current level
        public static List<Block> blocks = new List<Block>();
        public static List<int> highscores = new List<int>();
        public static List<Paddle> paddles = new List<Paddle>();
        public static List<Ball> balls = new List<Ball>();
        List<PowerUps> powerups = new List<PowerUps>();

        // Brushes
        SolidBrush drawBrush = new SolidBrush(Color.Tan);
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Black);
        SolidBrush blockBrush2 = new SolidBrush(Color.White);
        SolidBrush shadowBrush = new SolidBrush(Color.LightGray);
        SolidBrush powerBrush = new SolidBrush(Color.White);
        Font drawFont = new Font("Arial", 8);

        Stopwatch smallPAddleWatch = new Stopwatch();
        Stopwatch largePaddleWatch = new Stopwatch();
        Stopwatch fastWatch = new Stopwatch();
        Stopwatch slowWatch = new Stopwatch();
        Stopwatch aimWatch = new Stopwatch();

        #endregion

       

        public GameScreen()
        {
            InitializeComponent();
            OnStart();

            for (int x = highscores.Count(); x<10; x++)
            {
                highscores.Add(0);
            }
            
        }

        public void OnStart()
        {
            //set life counter
            lives = 3;
            level = 0;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = aKeyDown = dKeyDown = false;

            // setup starting paddle values and create paddle object
            onPaddle = true;
            int paddleSpeed = 8;
            paddleStartX = ((this.Width / 2) - (PADDLEWIDTH / 2));
            paddleStartY = (this.Height - PADDLEHEIGHT);
            paddle = new Paddle(paddleStartX, paddleStartY, PADDLEWIDTH, PADDLEHEIGHT, paddleSpeed, Color.White);

            ballStartX = this.Width / 2 - 10;
            ballStartY = this.Height - paddle.height - 25;
            int ballSize = 20;
            balls.Clear();
            ball = new Ball (ballStartX, ballStartY, 0, 0, ballSize);
            balls.Add(ball);

            //load score
            
            NextLevel();

            aimWatch.Start();

            // start the game engine loop
            gameTimer.Enabled = true;

          
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.P && gameTimer.Enabled)
            {
                gameTimer.Enabled = false;

                rightArrowDown = leftArrowDown = false;

                DialogResult result = PauseForm.Show();

                if (result == DialogResult.Cancel)
                {
                    gameTimer.Enabled = true;
                }
                else if (result == DialogResult.Abort)
                {
                    Form f = this.FindForm();
                    f.Controls.Remove(this);
                    this.Dispose();
                    MenuScreen ms = new MenuScreen();
                    f.Controls.Add(ms);
                }

            }
            
            //player 1 and 2 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.A:
                    aKeyDown = true;
                    break;
                case Keys.D:
                    dKeyDown = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 and 2 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.A:
                    aKeyDown = false;
                    break;
                case Keys.D:
                    dKeyDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //shoot ball off paddle
            if (upArrowDown && onPaddle)

            {
                ball.xSpeed = ballStartSpeedX;
                ball.ySpeed = ballStartSpeedY;
                onPaddle = false;
            }

            //center ball on paddle if it is supposed to be
            if (onPaddle) { balls[0].x = paddle.x + PADDLEWIDTH / 2; }

            //move paddle left and right
            if (leftArrowDown && paddle.x > 0) { paddle.Move("left"); }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width)) { paddle.Move("right"); }

            //aim ball left and right on paddle
            if (aKeyDown && onPaddle)
            {
                if (ballStartSpeedX > -8 && ballStartSpeedX <= 0)
                {
                    ballStartSpeedX--;
                    ballStartSpeedY++;
                }
                else if (ballStartSpeedX <= 8 && ballStartSpeedX > 0)
                {
                    ballStartSpeedX--;
                    ballStartSpeedY--;
                }
            }
            if (dKeyDown && onPaddle)
            {
                if (ballStartSpeedX < 8 && ballStartSpeedX >= 0)
                {
                    ballStartSpeedX++;
                    ballStartSpeedY++;
                }
                else if (ballStartSpeedX >= -8 && ballStartSpeedX < 0)
                {
                    ballStartSpeedX++;
                    ballStartSpeedY--;
                }
            }

            // move ball
            if (onPaddle == false)
            {
                foreach (Ball b in balls) { b.Move(); }
            }

            //check for ball hitting side of screen
            foreach (Ball b in balls) { b.WallCollision(this); }

            // Check for ball hitting bottom of screen
            foreach (Ball b in balls)
            {
                if (b.BottomCollision(this))
                {
                    if (balls.Count == 1)
                    {
                        lives--;
                        if (lives == 0)
                        {
                            gameTimer.Enabled = false;
                            OnEnd();
                            
                        }
                        OnDeath();
                    }
                    else if (b.BottomCollision(this))
                    {
                        balls.Remove(b);
                        break;
                    }
                }
            }
            

            //check for ball and paddle collision
            foreach (Ball b in balls) { b.PaddleCollision(paddle, leftArrowDown, rightArrowDown); }

            // Check if ball has collided with any blocks
            foreach (Ball a in balls)
            {
                foreach (Block b in blocks)
                {
                    if (a.BlockCollision(b))
                    {
                        b.hp--;
                        if (b.hp == 0)
                        {
                            score += 100;
                            if (rng.Next(1, 9) == 7)
                            {
                                powerups.Add(randomGenBoi(b.x, b.y));
                            }

                            blocks.Remove(b);
                            break;
                        }
                    }

                    
                }
            }

            //if all blocks are broken go to next level
            if (blocks.Count == 0)
            {
                NextLevel();
            }
            //move powerups
            foreach (PowerUps p in powerups)
            {
                p.Move();
                if (p.y > this.Height)
                {
                    powerups.Remove(p);
                    break;
                }
            }
            //check to see if power ups have hit the paddle
            foreach (PowerUps p in powerups)
            {
                if (p.Collision(paddle))
                {
                    score += 50;
                    switch (p.name)
                    {
                        case "multiBoi":
                            MultiBoi();
                            break;

                        case "lifeBoi":
                            ChangeLives(1);
                            break;

                        case "smallBoi":
                            if (smallPaddle == false)
                            {
                                smallPaddle = true;
                                smallPAddleWatch.Start();
                                paddle.x += 10;
                                ChangePaddle(-20);
                            }
                            break;

                        case "enlargedBoi":
                            if (largePaddle == false)
                            {
                                largePaddle = true;
                                largePaddleWatch.Start();
                                paddle.x -= 15;
                                ChangePaddle(30);
                            }
                            break;

                        case "slowBoi":
                            slowBoi = true;
                            slowWatch.Restart();
                            ChangeSpeeds(-4, -4, -4);
                            break;

                        case "fastBoi":
                            fastBoi = true;
                            fastWatch.Restart();
                            ChangeSpeeds(4, 4, 4);
                            break;

                    }
                    powerups.Remove(p);
                    break;
                }
            }

            if (smallPaddle && smallPAddleWatch.ElapsedMilliseconds >= 10000)
            {
                smallPaddle = false;
                smallPAddleWatch.Reset();
                ChangePaddle(20);
            }
            if (largePaddle && largePaddleWatch.ElapsedMilliseconds >= 10000)
            {
                largePaddle = false;
                largePaddleWatch.Reset();
                ChangePaddle(-30);
            }
            if (fastBoi && fastWatch.ElapsedMilliseconds >= 7000)
            {
                fastBoi = false;
                fastWatch.Reset();
                ReturnSpeeds();
            }
            if (slowBoi && slowWatch.ElapsedMilliseconds >= 7000)
            {
                slowBoi = false;
                slowWatch.Reset();
                ReturnSpeeds();
            }

            // check to see if game is lost
            if (lives == 0)
            {
                OnEnd();
            }

            //redraw the screen
            Refresh();
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Draws paddle
            drawBrush.Color = paddle.colour;

            e.Graphics.FillRectangle(shadowBrush, paddle.x + 3, paddle.y + 3, paddle.width, paddle.height);
            e.Graphics.FillRectangle(blockBrush, paddle.x - 1, paddle.y - 1, paddle.width + 2, paddle.height + 2);
            e.Graphics.FillRectangle(drawBrush, paddle.x, paddle.y, paddle.width, paddle.height);


            // Draws blocks
            foreach (Block b in blocks)
            {

                e.Graphics.FillRectangle(shadowBrush, b.x + 3, b.y + 3, b.width, b.height);
                e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.width, b.height);
                e.Graphics.FillRectangle(drawBrush, b.x + 1, b.y + 1, b.width - 2, b.height - 2);

                switch (b.hp)
                {
                    case 1:
                        drawBrush.Color = Color.White;
                        break;
                    case 2:
                        drawBrush.Color = Color.White;
                        e.Graphics.DrawImage(Properties.Resources.LVL2Scratch, b.x, b.y, b.width, b.height);
                        break;
                    case 3:
                        drawBrush.Color = Color.White;
                        e.Graphics.DrawImage(Properties.Resources.LVL3Scratch, b.x, b.y, b.width, b.height);
                        break;
                }

            }

            //draw powerups
            foreach (PowerUps p in powerups)
            {
                switch (p.name)
                {
                    case "multiBoi":
                        e.Graphics.DrawImage(Properties.Resources.multiBoi, p.x, p.y, 40, 40);
                        break;
                    case "lifeBoi":
                        e.Graphics.DrawImage(Properties.Resources.lifeBoi, p.x, p.y, 40, 40);
                        break;
                    case "smallBoi":
                        e.Graphics.DrawImage(Properties.Resources.smallBoi, p.x, p.y, 40, 40);
                        break;
                    case "enlargedBoi":
                        e.Graphics.DrawImage(Properties.Resources.enlargedBoi, p.x, p.y, 40, 40);
                        break;
                    case "slowBoi":
                        e.Graphics.DrawImage(Properties.Resources.slowBoi, p.x, p.y, 40, 40);
                        break;
                    case "fastBoi":
                        e.Graphics.DrawImage(Properties.Resources.fastBoi, p.x, p.y, 40, 40);
                        break;
                }
            }

            // Draws ball(s)
            drawBrush.Color = Color.White;
            Pen drawPen = new Pen(Color.Black);

            if (onPaddle)
            {
                e.Graphics.DrawLine(drawPen, balls[0].x + 10, balls[0].y + 10, balls[0].x + 10 * ballStartSpeedX + 10, balls[0].y - 60);
            }

            foreach (Ball b in balls)
            {
                e.Graphics.FillRectangle(shadowBrush, b.x + 3, b.y + 3, b.size, b.size);
                e.Graphics.FillRectangle(blockBrush, b.x - 1, b.y - 1, b.size + 2, b.size + 2);
                e.Graphics.FillRectangle(drawBrush, b.x, b.y, b.size, b.size);
               
            }
      

            //draw score and lives
            drawBrush.Color = Color.Black;
            for (int i = 0; i < lives; i++){ e.Graphics.DrawImage(Properties.Resources.lifeBoi, 715 + (20 * i), 10, 20, 20);}              
            e.Graphics.DrawString("Score: " + score, drawFont, drawBrush, 715, 35);


        }

        public PowerUps randomGenBoi(int _x, int _y)
        {
            Random rnd = new Random();

            int randomNumber = rnd.Next(0, 60);

            if (randomNumber <= 10)
            {
                return new PowerUps(_x, _y, "mutliBoi");
            }
            else if (randomNumber <= 20)
            {
                return new PowerUps(_x, _y, "fastBoi");
            }
            else if (randomNumber <= 30)
            {
                return new PowerUps(_x, _y, "slowBoi");
            }
            else if (randomNumber <= 40)
            {
                return new PowerUps(_x, _y, "smallBoi");
            }
            else if (randomNumber <= 50)
            {
                return new PowerUps(_x, _y, "enlargedBoi");
            }
            else   // if its lower than 105
            {
                return new PowerUps(_x, _y, "lifeBoi");
            }
        }


        #region Death and moving on
        public void NextLevel()
        {
            level++;
            powerups.Clear();

            switch (level)
            {
                case 1:
                    LoadLevel("Resources/level1.xml");
                    break;
                case 2:
                    LoadLevel("Resources/level2.xml");
                    break;
                case 3:
                    LoadLevel("Resources/level3.xml");
                    break;
                case 4:
                    LoadLevel("Resources/level4.xml");
                    break;
                case 5:
                    LoadLevel("Resources/level5.xml");
                    break;
                case 6:
                    LoadLevel("Resources/level6.xml");
                    break;
                case 7:
                    LoadLevel("Resources/level7.xml");
                    break;
                default:
                    OnEnd();
                    break;
            }

            OnDeath();
        }
        
        public void OnEnd()
        {
            saveScore();

            MenuScreen hs = new MenuScreen();
            Form form = Form1.ActiveForm;

            form.Controls.Add(hs);
            form.Controls.Remove(this);

            hs.Location = new Point((form.Width - hs.Width) / 2, (form.Height - hs.Height) / 2);
        }
       
        public void OnDeath()
        {
            onPaddle = true;
            ball.x = paddle.x + PADDLEWIDTH / 2 - ball.size /2;
            ball.y = ballStartY;
            balls[0].xSpeed = 0;
            balls[0].ySpeed = 0;
        }
        #endregion

        #region Levels and Scores
        public static void LoadLevel(string level)
        {
            //creates variables and xml reader needed
            XmlReader reader = XmlReader.Create(level);
            string blockX;
            string blockY;
            string blockHP;
            int intX;
            int intY;
            int intHP;

            //Grabs all the blocks for the current level and adds them to the list
            while (reader.Read())
            {
                reader.ReadToFollowing("x");
                blockX = reader.ReadString();
                reader.ReadToFollowing("y");
                blockY = reader.ReadString();
                reader.ReadToFollowing("hp");
                blockHP = reader.ReadString();

                if (blockX != "")
                {
                    intX = Convert.ToInt16(blockX);
                    intY = Convert.ToInt16(blockY);
                    intHP = Convert.ToInt16(blockHP);

                    Block b = new Block(intX, intY, intHP);

                    blocks.Add(b);
                }
            }


        }

        public void saveScore()
        {
            highscores.Add(score);

            highscores.Sort();
            highscores.Reverse();

            XmlWriter writer = XmlWriter.Create("Resources/scores.xml", null);

            writer.WriteStartElement("scores");

            for (int i = 0; i < 10; i++)
            {
                writer.WriteElementString("score", highscores[i].ToString());
            }
            writer.WriteEndElement();

            writer.Close();

        }

        public static void loadScore()
        {
            string newScore;
            int intScore;

            XmlReader reader = XmlReader.Create("Resources/scores.xml");


            for (int i = 0; i < 10; i++)
            {
                reader.ReadToFollowing("score");
                newScore = reader.ReadString();

                if (newScore != "")
                {
                    intScore = Convert.ToInt16(newScore);
                    highscores.Add(intScore);
                }
                else
                {
                    break;
                }
            }
            reader.Close();
        }
        #endregion

        #region change value functions
        public void MultiBoi ()
        {
            Ball b = new Ball(paddle.x + PADDLEWIDTH / 4, paddle.y - 30, 6, -6, 20);
            balls.Add(b);
            Ball b1 = new Ball(paddle.x + PADDLEWIDTH * (3/ 4), paddle.y - 30, -6, -6, 20);
            balls.Add(b1);
        }

        public static void ChangeSpeeds(int xSpeed, int ySpeed, int paddleSpeed)
        {
            foreach (Ball b in balls)
            {
                if (b.xSpeed < 0) { b.xSpeed -= xSpeed; }
                else { b.xSpeed += xSpeed; }

                if (b.ySpeed < 0) { b.ySpeed -= ySpeed; }
                else { b.ySpeed += ySpeed; }
            }

            paddle.speed += paddleSpeed;
        }

        public static void ChangePaddle(int width)
        {
            paddle.width += width;
        }


        public static void ChangeLives(int number)
        {
            lives += number;
        }


        public void ReturnSpeeds()
        {
            if (ball.xSpeed < 0) { ball.xSpeed = -BALLSPEED; }
            else { ball.xSpeed = BALLSPEED; }

            if (ball.ySpeed < 0) { ball.ySpeed = -BALLSPEED; }
            else { ball.ySpeed = BALLSPEED; }

            paddle.speed = PADDLESPEED;
        }

        public static void ReturnPaddle()
        {
            paddle.width = PADDLEWIDTH;
        }
        #endregion
    }
}
