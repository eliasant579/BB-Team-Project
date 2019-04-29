﻿
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

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values
        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown, pauseArrowDown, upArrowDown, onPaddle = true;

        // Game values
        static int lives;
        int score;

        int level = 1;
        int ballStartX, ballStartY, paddleStartX, paddleStartY, ballStartSpeedX = 0, ballStartSpeedY = -10;
        static int bbucks = 0;

        // constants
        const int BALLSPEED = 6;
        const int PADDLESPEED = 8;
        const int PADDLEWIDTH = 80;

        // Paddle and Ball objects
        static Paddle paddle;
        static Ball ball;

        // list of all blocks for current level
        List<Block> blocks = new List<Block>();
        static List<Ball> balls = new List<Ball>();

        // Brushes

        SolidBrush drawBrush = new SolidBrush(Color.White);
        Font drawFont = new Font("Arial", 12);

        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Black);
        SolidBrush blockBrush2 = new SolidBrush(Color.White);
        SolidBrush shadowBrush = new SolidBrush(Color.LightGray);




        #endregion

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        
        }


        public void OnStart()
        {
          
            //set life counter
            lives = 3;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;

            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleHeight = 20;
            paddleStartX = ((this.Width / 2) - (paddleWidth / 2));
            paddleStartY = (this.Height - paddleHeight) - 60;
            int paddleSpeed = 8;

            paddle = new Paddle(paddleStartX, paddleStartY, paddleWidth, paddleHeight, paddleSpeed, Color.White);


            // setup starting ball values
            ballStartX = this.Width / 2 - 10;
            ballStartY = this.Height - paddle.height - 85;

            // Creates a new ball

            int xSpeed = -6;
            int ySpeed = -6;

            int ballSize = 20;
            ball = new Ball(ballStartX, ballStartY, 0, 0, ballSize);
            balls.Clear();
            balls.Add(ball);

            //loads current level
            LoadLevel("Resources/level5.xml");

            // start the game engine loop
            gameTimer.Enabled = true;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.P:
                    pauseArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.P:
                    pauseArrowDown = false;
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
           
           
            // Move the paddle
            if (upArrowDown == true && onPaddle == true)
            {
                balls[0].xSpeed = ballStartSpeedX;
                balls[0].ySpeed = ballStartSpeedY;
                onPaddle = false;
            }

            if (leftArrowDown && onPaddle)
            {
                if (ballStartSpeedX > -8 && ballStartSpeedX <= 0)
                {
                    ballStartSpeedX--;
                    ballStartSpeedY++;
                }
                else if (ballStartSpeedX > -8 && ballStartSpeedX > 0)
                {
                    ballStartSpeedX--;
                    ballStartSpeedY--;
                }
            }
            else if (leftArrowDown && paddle.x > 0) { paddle.Move("left"); }

            if (rightArrowDown && onPaddle)
            {
                if (ballStartSpeedX < 8 && ballStartSpeedX >= 0)
                {
                    ballStartSpeedX++;
                    ballStartSpeedY++;
                }
                else if (ballStartSpeedX < 8 && ballStartSpeedX < 0)
                {
                    ballStartSpeedX++;
                    ballStartSpeedY--;
                }
            }
            else if (rightArrowDown && paddle.x < (this.Width - paddle.width)) { paddle.Move("right"); }

            if (pauseArrowDown)
            {
                PauseScreen ps = new PauseScreen();
                Form form = this.FindForm();

                gameTimer.Enabled = false;

                form.Controls.Add(ps);
                form.Controls.Remove(this);

                ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);
            }
           

            // Move ball
            foreach (Ball b in balls) { b.Move(); }

            // Check for collision with all walls
            foreach (Ball b in balls)
            {
                //Check for ball hitting top and side walls
                b.WallCollision(this);

                // Check for ball hitting bottom of screen
                if (b.BottomCollision(this) && balls.Count == 1)
                {
                    lives--;

                    // Moves the ball back to origin
                    onPaddle = true;
                    paddle.x = paddleStartX;
                    balls[0].xSpeed = 0;
                    balls[0].ySpeed = 0;
                    b.x = ((paddle.x - (b.size / 2)) + (paddle.width / 2));
                    b.y = (this.Height - paddle.height) - 85;

                    if (lives == 0)
                    {
                        gameTimer.Enabled = false;
                        OnEnd();
                    }
                }
                else if (b.BottomCollision(this))
                {
                    balls.Remove(b);
                    break;
                }
            }

            // Check for collision of ball with paddle, (incl. paddle movement)
            foreach (Ball b in balls) { b.PaddleCollision(paddle, leftArrowDown, rightArrowDown); }

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                if (ball.BlockCollision(b))
                {
                    //removing block logic
                    b.hp--;
                    if (b.hp == 0)
                    {
                        blocks.Remove(b);
                        score += 50;
                        break;
                    }
                    //if all blocks are broken go to next level
                    if (blocks.Count == 0)
                    {
                        gameTimer.Enabled = false;
                        NextLevel();
                    }
                }
            }

            //redraw the screen
            Refresh();
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Draws paddle
            drawBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(drawBrush, paddle.x, paddle.y, paddle.width, paddle.height);

            // Draws blocks
            foreach (Block b in blocks)
            {

                switch (b.hp)
                {
                    case 1:
                        drawBrush.Color = Color.Red;
                        break;
                    case 2:
                        drawBrush.Color = Color.Yellow;
                        break;
                    case 3:
                        drawBrush.Color = Color.Green;
                        break;
                }
                e.Graphics.FillRectangle(drawBrush, b.x, b.y, b.width, b.height);
               // e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.width, b.height);
                e.Graphics.FillRectangle(shadowBrush, b.x + 3, b.y + 3, b.width, b.height); 
                e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.width, b.height);
                e.Graphics.FillRectangle(blockBrush2, b.x + 1, b.y + 1, b.width - 2, b.height - 2);
            }
            
            var g = e.Graphics;

            // Draws paddle
            //yeet

            paddleBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(shadowBrush, paddle.x + 3, paddle.y + 3, paddle.width, paddle.height);
            e.Graphics.FillRectangle(blockBrush, paddle.x, paddle.y, paddle.width, paddle.height);
            e.Graphics.FillRectangle(blockBrush2, paddle.x + 1, paddle.y + 1, paddle.width - 2, paddle.height - 2);
            
            // Draws blocks


            // Draws ball(s)
            drawBrush.Color = Color.White;
            foreach (Ball b in balls) { e.Graphics.FillRectangle(drawBrush, b.x, b.y, b.size, b.size); }

            //draw score and lives
            e.Graphics.DrawString("Lives: " + ballStartSpeedX, drawFont, drawBrush, 100, 85);
            e.Graphics.DrawString("Score: " + ballStartSpeedY, drawFont, drawBrush, 100, 100);

        }

        public void OnEnd()
        {
            // Goes to the game over screen
            Form form = this.FindForm();
            MenuScreen ps = new MenuScreen();

            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }


        public void NextLevel()
        {
            
                gameTimer.Enabled = false;
                level++;
                switch (level)
                {
                    case 2:
                        LoadLevel("");
                        break;
                    default:
                        OnEnd();
                        break;

                }



            paddle.x = paddleStartX; paddle.y = paddleStartY;

            balls.Clear();
            ball = new Ball(ballStartX, ballStartY, 6, 6, 20);
            balls.Add(ball);


                /*
                // Draws ball
                e.Graphics.FillEllipse(shadowBrush, ball.x + 3, ball.y + 3, ball.size, ball.size);
                e.Graphics.FillEllipse(blockBrush, ball.x, ball.y, ball.size, ball.size);
                e.Graphics.FillEllipse(blockBrush2, ball.x + 1, ball.y + 1, ball.size - 2, ball.size - 2);
                */

                //TODO set ball and paddle to starting position
            

        }

        public void LoadLevel(string level)
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

        #region change value functions
        public static void ChangeSpeeds(int xSpeed, int ySpeed, int paddleSpeed)
        {
            if (ball.xSpeed < 0) { ball.xSpeed -= xSpeed; }
            else { ball.xSpeed += xSpeed; }

            if (ball.ySpeed < 0) { ball.ySpeed -= ySpeed; }
            else { ball.ySpeed += ySpeed; }

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
            paddle.width = PADDLESPEED;
        }

        public static void GiveBBuck (int bigmonies)
        {
            bbucks += bigmonies;
        }
        #endregion
    }
}
