namespace semProjectClient
{
    partial class ChatForm
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
            this.chatRTB = new System.Windows.Forms.RichTextBox();
            this.messageTB = new System.Windows.Forms.TextBox();
            this.sendB = new System.Windows.Forms.Button();
            this.onlineBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // chatRTB
            // 
            this.chatRTB.Location = new System.Drawing.Point(12, 12);
            this.chatRTB.Name = "chatRTB";
            this.chatRTB.Size = new System.Drawing.Size(222, 213);
            this.chatRTB.TabIndex = 0;
            this.chatRTB.Text = "";
            // 
            // messageTB
            // 
            this.messageTB.Location = new System.Drawing.Point(12, 241);
            this.messageTB.Name = "messageTB";
            this.messageTB.Size = new System.Drawing.Size(222, 20);
            this.messageTB.TabIndex = 1;
            // 
            // sendB
            // 
            this.sendB.Location = new System.Drawing.Point(240, 239);
            this.sendB.Name = "sendB";
            this.sendB.Size = new System.Drawing.Size(75, 23);
            this.sendB.TabIndex = 2;
            this.sendB.Text = "Send";
            this.sendB.UseVisualStyleBackColor = true;
            this.sendB.Click += new System.EventHandler(this.sendB_Click);
            // 
            // onlineBox
            // 
            this.onlineBox.FormattingEnabled = true;
            this.onlineBox.Location = new System.Drawing.Point(240, 13);
            this.onlineBox.Name = "onlineBox";
            this.onlineBox.Size = new System.Drawing.Size(134, 212);
            this.onlineBox.TabIndex = 3;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 270);
            this.Controls.Add(this.onlineBox);
            this.Controls.Add(this.sendB);
            this.Controls.Add(this.messageTB);
            this.Controls.Add(this.chatRTB);
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox chatRTB;
        private System.Windows.Forms.TextBox messageTB;
        private System.Windows.Forms.Button sendB;
        private System.Windows.Forms.ListBox onlineBox;
    }
}