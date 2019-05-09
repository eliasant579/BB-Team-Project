namespace BrickBreaker
{
    partial class MenuScreen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.playButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.twoPlayerButton = new System.Windows.Forms.Button();
            this.highscoreButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.BackColor = System.Drawing.Color.Transparent;
            this.playButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.playButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.playButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playButton.ForeColor = System.Drawing.Color.Transparent;
            this.playButton.Location = new System.Drawing.Point(34, 587);
            this.playButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(230, 107);
            this.playButton.TabIndex = 0;
            this.playButton.UseVisualStyleBackColor = false;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            this.playButton.Enter += new System.EventHandler(this.PlayButton_Enter);
            this.playButton.Leave += new System.EventHandler(this.PlayButton_Leave);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.Transparent;
            this.exitButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(934, 580);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(227, 114);
            this.exitButton.TabIndex = 3;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            this.exitButton.Enter += new System.EventHandler(this.exitButton_Enter);
            this.exitButton.Leave += new System.EventHandler(this.exitButton_Leave);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 69F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(328, 580);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 114);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Enter += new System.EventHandler(this.Button1_Enter);
            this.button1.Leave += new System.EventHandler(this.Button1_Leave);
            // 
            // twoPlayerButton
            // 
            this.twoPlayerButton.BackColor = System.Drawing.Color.Transparent;
            this.twoPlayerButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.twoPlayerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.twoPlayerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.twoPlayerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.twoPlayerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 69F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoPlayerButton.Location = new System.Drawing.Point(285, 369);
            this.twoPlayerButton.Name = "twoPlayerButton";
            this.twoPlayerButton.Size = new System.Drawing.Size(219, 86);
            this.twoPlayerButton.TabIndex = 2;
            this.twoPlayerButton.UseVisualStyleBackColor = false;
            this.twoPlayerButton.Click += new System.EventHandler(this.twoPlayerButton_Click_1);
            // 
            // highscoreButton
            // 
            this.highscoreButton.BackColor = System.Drawing.Color.Transparent;
            this.highscoreButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.highscoreButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.highscoreButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.highscoreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highscoreButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 69F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highscoreButton.Location = new System.Drawing.Point(640, 584);
            this.highscoreButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.highscoreButton.Name = "highscoreButton";
            this.highscoreButton.Size = new System.Drawing.Size(227, 114);
            this.highscoreButton.TabIndex = 2;
            this.highscoreButton.UseVisualStyleBackColor = false;
            this.highscoreButton.Click += new System.EventHandler(this.HighscoreButton_Click);
            this.highscoreButton.Enter += new System.EventHandler(this.HighscoreButton_Enter);
            this.highscoreButton.Leave += new System.EventHandler(this.HighscoreButton_Leave);
            // 
            // MenuScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::BrickBreaker.Properties.Resources.MenuScreenWhighscore;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.highscoreButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.playButton);
            this.Name = "MenuScreen";
            this.Size = new System.Drawing.Size(1200, 846);
            this.Enter += new System.EventHandler(this.exitButton_Enter);
            this.Leave += new System.EventHandler(this.exitButton_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button twoPlayerButton;
        private System.Windows.Forms.Button highscoreButton;
    }
}
