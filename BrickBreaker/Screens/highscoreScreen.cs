using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker.Screens
{
    public partial class highscoreScreen : UserControl
    {
        public highscoreScreen()
        {
            InitializeComponent();

            GameScreen.loadScore();
            foreach (int i in GameScreen.highscores)
            {
                outputLabel.Text += Convert.ToString(i) + "\n";
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            MenuScreen hs = new MenuScreen();
            Form form = Form1.ActiveForm;

            form.Controls.Add(hs);
            form.Controls.Remove(this);

            hs.Location = new Point((form.Width - hs.Width) / 2, (form.Height - hs.Height) / 2);
        }
    }
}
