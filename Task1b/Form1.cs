using System;
using System.Windows.Forms;

namespace Task1b
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SubmitUserNameButton_Click(object sender, EventArgs e)
        {
            userNameLabel.Text = $"Hello, {userNameTextBox.Text}!";
        }
    }
}
