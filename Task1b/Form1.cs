using System;
using System.Windows.Forms;

using Task2;

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
            var username = userNameTextBox.Text;

            var message = MessageGenerator.CreateHelloMessage(username);

            userNameLabel.Text = message;
        }
    }
}
