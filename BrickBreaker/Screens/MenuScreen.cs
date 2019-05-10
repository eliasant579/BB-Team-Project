﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrickBreaker.Screens;

namespace BrickBreaker
{
    public partial class MenuScreen : UserControl
    {
        public MenuScreen()
        {
            InitializeComponent();
            Form form = Form1.ActiveForm;
            this.Location = new Point((form.Width - this.Width) / 2, (form.Height - this.Height) / 2);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // Goes to the game screen          
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
        }

        private void twoPlayerButton_Click_1(object sender, EventArgs e)
        {
            TwoPlayer gs = new TwoPlayer();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
        }

        private void HighscoreButton_Click(object sender, EventArgs e)
        {
            highscoreScreen hs = new highscoreScreen();
            Form form = this.FindForm();

            form.Controls.Add(hs);
            form.Controls.Remove(this);

            hs.Location = new Point((form.Width - hs.Width) / 2, (form.Height - hs.Height) / 2);
        }
        private void exitButton_Enter(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.Gray;
        }

        private void exitButton_Leave(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.Transparent;
        }

        private void HighscoreButton_Enter(object sender, EventArgs e)
        {
            highscoreButton.BackColor = Color.Gray;
        }

        private void Button1_Enter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gray;
        }

        private void PlayButton_Enter(object sender, EventArgs e)
        {
            playButton.BackColor = Color.Gray;
        }

        private void PlayButton_Leave(object sender, EventArgs e)
        {
            playButton.BackColor = Color.Transparent;
        }

        private void Button1_Leave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
        }

        private void HighscoreButton_Leave(object sender, EventArgs e)
        {
            highscoreButton.BackColor = Color.Transparent;
        }
    }
}
