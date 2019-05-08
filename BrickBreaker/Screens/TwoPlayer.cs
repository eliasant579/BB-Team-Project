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


namespace BrickBreaker.Screens
{
    public partial class TwoPlayer : UserControl
    {
        #region global values
        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown, pauseArrowDown, upArrowDown, onPaddle = true, aKeyDown, dKeyDown, bKeyDown,mKeyDown;
        // Game values
        static int lives;
        int score;
        public static Boolean Twoplayer = false;
        int level = 1;
        int ballStartX, ballStartY, paddleStartX, paddleStartY, ballStartSpeedX = 0, ballStartSpeedY = -10;
        static int bbucks = 0;

        Random rng = new Random();

        // constants
        const int BALLSPEED = 6;
        const int PADDLESPEED = 8;
        const int PADDLEWIDTH = 80; const int PADDLEHEIGHT = 20;
        // Paddle and Ball objects
        public static Paddle pad, pad2; public static Ball ball;

        // list of all blocks and paddles for current level
        List<Block> blocks = new List<Block>();
        List<int> highscores = new List<int>();
        List<Paddle> paddles = new List<Paddle>();
        List<Ball> balls = new List<Ball>();
        List<PowerUps> powerups = new List<PowerUps>();

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Black);
        SolidBrush drawBrush = new SolidBrush(Color.Tan);
        SolidBrush shadowBrush = new SolidBrush(Color.LightGray);
        Font drawFont = new Font("Arial", 12);

        #endregion
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
        public TwoPlayer()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()
        {

            // Setup variables 
            int paddleBoostY = 80;
            int paddleY = (this.Height - PADDLEHEIGHT) - paddleBoostY;
            int paddleSpeed = 8;
            int paddleStartX = ((this.Width / 2) - (PADDLEWIDTH / 2));
            int paddleStartY = (this.Height - PADDLEHEIGHT) - 60;
            //Creating and Adding paddles/balls to a list
            Paddle pad = new Paddle(paddleStartX, paddleStartY, PADDLEWIDTH, PADDLEHEIGHT, paddleSpeed, Color.White);
            paddles.Add(pad);
            Paddle pad2 = new Paddle(paddleStartX, paddleY, PADDLEWIDTH, PADDLEHEIGHT, paddleSpeed, Color.Blue);
            paddles.Add(pad2);
            ballStartX = this.Width / 2 - 10;
            ballStartY = this.Height - pad.height - 85;
            int ballSize = 20;
            balls.Clear();
            ball = new Ball(ballStartX, ballStartY, 0, 0, ballSize);
            balls.Add(ball);

            //load score
            //loadScore();
            LoadLevel("Resources/twoplayerlevel1.xml");

            // start the game engine loop
            gameTimer.Enabled = true;
        }
        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 and 2 button presses
            switch (e.KeyCode)
            {
                case Keys.B:
                    bKeyDown = true;
                    break;
                case Keys.M:
                    mKeyDown = true;
                    break;
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
                case Keys.B:
                    bKeyDown = false;
                    break;
                case Keys.M:
                    mKeyDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.P:
                    pauseArrowDown = false;
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            // shoot ball off paddle
            if (upArrowDown && onPaddle)

            {
                ball.xSpeed = ballStartSpeedX;
                ball.ySpeed = ballStartSpeedY;
                onPaddle = false;
            }
           
            //center ball on paddle if it is supposed to be
            if (onPaddle) { balls[0].x = pad.x + PADDLEWIDTH / 2; }
             
            //move paddle left and right
            if (leftArrowDown && pad.x > 0) { pad.Move("left"); }
            if (rightArrowDown && pad.x < (this.Width - pad.width)) { pad.Move("right"); }
            if (aKeyDown == true && pad2.x > 0) { pad.Move("left"); }
            if (dKeyDown && pad2.x < (this.Width - pad2.width)) { pad2.Move("right"); }

            //aim ball left and right from paddle
            if (bKeyDown && onPaddle)
            {
                if (ballStartSpeedX > -8 && ballStartSpeedX <= 0)
                {
                    ballStartSpeedX--;
                    ballStartSpeedY++;
                }
                else if (ballStartSpeedX < 8 && ballStartSpeedX > 0)
                {
                    ballStartSpeedX++;
                    ballStartSpeedY--;
                }
            }
            if (mKeyDown && onPaddle)
            {
                if (ballStartSpeedX < 8 && ballStartSpeedX >= 0)
                {
                    ballStartSpeedX++;
                    ballStartSpeedY--;
                }
                else if (ballStartSpeedX > -8 && ballStartSpeedX < 0)
                {
                    ballStartSpeedX--;
                    ballStartSpeedY++;
                }
            }

            //pause game
            if (pauseArrowDown)
            {
                PauseScreen ps = new PauseScreen();
                Form form = this.FindForm();

                gameTimer.Enabled = false;

                form.Controls.Add(ps);
                form.Controls.Remove(this);

                ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);
            }

            // move ball
            foreach (Ball b in balls) { ball.Move(); }
        }
        private void twoPlayer_Paint(object sender, PaintEventArgs e)
        {
                foreach (Paddle p in paddles)
                {
                    paddleBrush.Color = p.colour;
                    e.Graphics.FillRectangle(paddleBrush, p.x, p.y, p.width, p.height);
                }
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
                e.Graphics.FillRectangle(shadowBrush, b.x + 3, b.y + 3, b.width, b.height);
                e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.width, b.height);
                e.Graphics.FillRectangle(drawBrush, b.x + 1, b.y + 1, b.width - 2, b.height - 2);
            }
            foreach (Ball b in balls) { e.Graphics.FillRectangle(drawBrush, b.x, b.y, b.size, b.size); }
        }
    }
    } 
