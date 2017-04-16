namespace semProjectClient
{
    partial class LoginForm
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
            this.usernameTB = new System.Windows.Forms.TextBox();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.loginB = new System.Windows.Forms.Button();
            this.registerB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernameTB
            // 
            this.usernameTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.usernameTB.ForeColor = System.Drawing.Color.Gray;
            this.usernameTB.Location = new System.Drawing.Point(44, 23);
            this.usernameTB.Name = "usernameTB";
            this.usernameTB.Size = new System.Drawing.Size(276, 20);
            this.usernameTB.TabIndex = 0;
            this.usernameTB.Text = "Username";
            this.usernameTB.GotFocus += OnFocus;
            this.usernameTB.LostFocus += usernameTB_LostFocus;
            // 
            // passwordTB
            // 
            this.passwordTB.ForeColor = System.Drawing.Color.Gray;
            this.passwordTB.Location = new System.Drawing.Point(44, 67);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(276, 20);
            this.passwordTB.TabIndex = 1;
            this.passwordTB.Text = "Password";
            this.passwordTB.GotFocus += OnFocus;
            this.passwordTB.LostFocus += passwordTB_LostFocus;
            // 
            // loginB
            // 
            this.loginB.Location = new System.Drawing.Point(67, 108);
            this.loginB.Name = "loginB";
            this.loginB.Size = new System.Drawing.Size(95, 24);
            this.loginB.TabIndex = 2;
            this.loginB.Text = "Log In";
            this.loginB.UseVisualStyleBackColor = true;
            this.loginB.Click += new System.EventHandler(this.loginB_Click);
            // 
            // registerB
            // 
            this.registerB.Location = new System.Drawing.Point(201, 108);
            this.registerB.Name = "registerB";
            this.registerB.Size = new System.Drawing.Size(95, 24);
            this.registerB.TabIndex = 3;
            this.registerB.Text = "Register";
            this.registerB.UseVisualStyleBackColor = true;
            this.registerB.Click += new System.EventHandler(this.registerB_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 154);
            this.Controls.Add(this.registerB);
            this.Controls.Add(this.loginB);
            this.Controls.Add(this.passwordTB);
            this.Controls.Add(this.usernameTB);
            this.Name = "LoginForm";
            this.Text = "Log In";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameTB;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.Button loginB;
        private System.Windows.Forms.Button registerB;
    }
}

