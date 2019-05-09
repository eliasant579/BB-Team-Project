using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker
{
    public partial class PauseForm : Form
    {
        private static PauseForm pauseForm;
        private static DialogResult buttonResult = new DialogResult();

        public PauseForm()
        {
            InitializeComponent();
        }

        public static DialogResult Show()
        {
            pauseForm = new PauseForm();
            pauseForm.ShowDialog();
           
            Form form = Form1.ActiveForm;


            pauseForm.Location = new Point((form.Width - pauseForm.Width) / 2, (form.Height - pauseForm.Height) / 2);

            return buttonResult;
        }

        private static void ButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Text)
            {
                case "Continue":
                    buttonResult = DialogResult.Cancel;
                    break;
                case "Exit Game":
                    buttonResult = DialogResult.Abort;
                    break;
            }

            pauseForm.Close();

        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            buttonResult = DialogResult.Cancel;
            pauseForm.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            buttonResult = DialogResult.Abort;
            pauseForm.Close();
        }

        private void ContinueButton_Enter(object sender, EventArgs e)
        {
            continueButton.BackColor = Color.Gray;
        }

        private void ContinueButton_Leave(object sender, EventArgs e)
        {
            continueButton.BackColor = Color.Transparent;
        }

        private void ExitButton_Enter(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.Gray;
        }

        private void ExitButton_Leave(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.Transparent;
        }
    }
}
