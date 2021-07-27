
namespace Task1b
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.userNameLabel = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.SubmitUserNameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(23, 50);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(0, 17);
            this.userNameLabel.TabIndex = 0;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(26, 12);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(100, 22);
            this.userNameTextBox.TabIndex = 1;
            // 
            // SubmitUserNameButton
            // 
            this.SubmitUserNameButton.Location = new System.Drawing.Point(132, 12);
            this.SubmitUserNameButton.Name = "SubmitUserNameButton";
            this.SubmitUserNameButton.Size = new System.Drawing.Size(42, 22);
            this.SubmitUserNameButton.TabIndex = 2;
            this.SubmitUserNameButton.Text = "OK";
            this.SubmitUserNameButton.UseVisualStyleBackColor = true;
            this.SubmitUserNameButton.Click += new System.EventHandler(this.SubmitUserNameButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 93);
            this.Controls.Add(this.SubmitUserNameButton);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.userNameLabel);
            this.Name = "Form1";
            this.Text = "Task1b app";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Button SubmitUserNameButton;
    }
}

