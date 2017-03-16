namespace WinForm1
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
            this.nBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nBox2 = new System.Windows.Forms.TextBox();
            this.resBox = new System.Windows.Forms.ListBox();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.stopB = new System.Windows.Forms.Button();
            this.stressB = new System.Windows.Forms.Button();
            this.loadB = new System.Windows.Forms.Button();
            this.saveB = new System.Windows.Forms.Button();
            this.clearB = new System.Windows.Forms.Button();
            this.clrB = new System.Windows.Forms.Button();
            this.divB = new System.Windows.Forms.Button();
            this.mulB = new System.Windows.Forms.Button();
            this.subB = new System.Windows.Forms.Button();
            this.addB = new System.Windows.Forms.Button();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // nBox1
            // 
            this.nBox1.Location = new System.Drawing.Point(42, 9);
            this.nBox1.Name = "nBox1";
            this.nBox1.Size = new System.Drawing.Size(100, 20);
            this.nBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "N1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "N2:";
            // 
            // nBox2
            // 
            this.nBox2.Location = new System.Drawing.Point(186, 9);
            this.nBox2.Name = "nBox2";
            this.nBox2.Size = new System.Drawing.Size(100, 20);
            this.nBox2.TabIndex = 2;
            // 
            // resBox
            // 
            this.resBox.FormattingEnabled = true;
            this.resBox.Location = new System.Drawing.Point(309, 9);
            this.resBox.Name = "resBox";
            this.resBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.resBox.Size = new System.Drawing.Size(409, 199);
            this.resBox.TabIndex = 5;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.stopB);
            this.buttonPanel.Controls.Add(this.stressB);
            this.buttonPanel.Controls.Add(this.loadB);
            this.buttonPanel.Controls.Add(this.saveB);
            this.buttonPanel.Controls.Add(this.clearB);
            this.buttonPanel.Controls.Add(this.clrB);
            this.buttonPanel.Controls.Add(this.divB);
            this.buttonPanel.Controls.Add(this.mulB);
            this.buttonPanel.Controls.Add(this.subB);
            this.buttonPanel.Controls.Add(this.addB);
            this.buttonPanel.Location = new System.Drawing.Point(15, 49);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(270, 158);
            this.buttonPanel.TabIndex = 6;
            // 
            // stopB
            // 
            this.stopB.Location = new System.Drawing.Point(210, 95);
            this.stopB.Name = "stopB";
            this.stopB.Size = new System.Drawing.Size(57, 40);
            this.stopB.TabIndex = 9;
            this.stopB.Text = "Stop";
            this.stopB.UseVisualStyleBackColor = true;
            this.stopB.Click += new System.EventHandler(this.stopB_Click);
            // 
            // stressB
            // 
            this.stressB.Location = new System.Drawing.Point(147, 95);
            this.stressB.Name = "stressB";
            this.stressB.Size = new System.Drawing.Size(57, 40);
            this.stressB.TabIndex = 8;
            this.stressB.Text = "Stress Test";
            this.stressB.UseVisualStyleBackColor = true;
            this.stressB.Click += new System.EventHandler(this.stressB_Click);
            // 
            // loadB
            // 
            this.loadB.Location = new System.Drawing.Point(75, 95);
            this.loadB.Name = "loadB";
            this.loadB.Size = new System.Drawing.Size(66, 40);
            this.loadB.TabIndex = 7;
            this.loadB.Text = "Load";
            this.loadB.UseVisualStyleBackColor = true;
            this.loadB.Click += new System.EventHandler(this.loadB_Click);
            // 
            // saveB
            // 
            this.saveB.Location = new System.Drawing.Point(3, 95);
            this.saveB.Name = "saveB";
            this.saveB.Size = new System.Drawing.Size(66, 40);
            this.saveB.TabIndex = 6;
            this.saveB.Text = "Save";
            this.saveB.UseVisualStyleBackColor = true;
            this.saveB.Click += new System.EventHandler(this.saveB_Click);
            // 
            // clearB
            // 
            this.clearB.Location = new System.Drawing.Point(147, 49);
            this.clearB.Name = "clearB";
            this.clearB.Size = new System.Drawing.Size(120, 40);
            this.clearB.TabIndex = 5;
            this.clearB.Text = "Clear";
            this.clearB.UseVisualStyleBackColor = true;
            this.clearB.Click += new System.EventHandler(this.clearB_Click);
            // 
            // clrB
            // 
            this.clrB.Location = new System.Drawing.Point(147, 3);
            this.clrB.Name = "clrB";
            this.clrB.Size = new System.Drawing.Size(120, 40);
            this.clrB.TabIndex = 4;
            this.clrB.Text = "Clr";
            this.clrB.UseVisualStyleBackColor = true;
            this.clrB.Click += new System.EventHandler(this.clrB_Click);
            // 
            // divB
            // 
            this.divB.Location = new System.Drawing.Point(75, 49);
            this.divB.Name = "divB";
            this.divB.Size = new System.Drawing.Size(66, 40);
            this.divB.TabIndex = 3;
            this.divB.Text = "Div";
            this.divB.UseVisualStyleBackColor = true;
            this.divB.Click += new System.EventHandler(this.divB_Click);
            // 
            // mulB
            // 
            this.mulB.Location = new System.Drawing.Point(3, 49);
            this.mulB.Name = "mulB";
            this.mulB.Size = new System.Drawing.Size(66, 40);
            this.mulB.TabIndex = 2;
            this.mulB.Text = "Mul";
            this.mulB.UseVisualStyleBackColor = true;
            this.mulB.Click += new System.EventHandler(this.mulB_Click);
            // 
            // subB
            // 
            this.subB.Location = new System.Drawing.Point(75, 3);
            this.subB.Name = "subB";
            this.subB.Size = new System.Drawing.Size(66, 40);
            this.subB.TabIndex = 1;
            this.subB.Text = "Sub";
            this.subB.UseVisualStyleBackColor = true;
            this.subB.Click += new System.EventHandler(this.subB_Click);
            // 
            // addB
            // 
            this.addB.Location = new System.Drawing.Point(3, 3);
            this.addB.Name = "addB";
            this.addB.Size = new System.Drawing.Size(66, 40);
            this.addB.TabIndex = 0;
            this.addB.Text = "Add";
            this.addB.UseVisualStyleBackColor = true;
            this.addB.Click += new System.EventHandler(this.addB_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 222);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.resBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nBox2;
        private System.Windows.Forms.ListBox resBox;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button divB;
        private System.Windows.Forms.Button mulB;
        private System.Windows.Forms.Button subB;
        private System.Windows.Forms.Button addB;
        private System.Windows.Forms.Button clrB;
        private System.Windows.Forms.Button clearB;
        private System.Windows.Forms.Button loadB;
        private System.Windows.Forms.Button saveB;
        private System.Windows.Forms.Button stressB;
        private System.Windows.Forms.Button stopB;
    }
}

