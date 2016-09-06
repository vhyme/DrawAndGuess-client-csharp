namespace DrawAndGuess_client_csharp
{
    partial class WaitDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitDlg));
            this.labelRoomNum = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // labelRoomNum
            // 
            this.labelRoomNum.AutoSize = true;
            this.labelRoomNum.BackColor = System.Drawing.Color.Transparent;
            this.labelRoomNum.Location = new System.Drawing.Point(187, 9);
            this.labelRoomNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRoomNum.Name = "labelRoomNum";
            this.labelRoomNum.Size = new System.Drawing.Size(53, 12);
            this.labelRoomNum.TabIndex = 0;
            this.labelRoomNum.Text = "Room ID:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(281, 207);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(81, 22);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start Game";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(189, 36);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(173, 156);
            this.listBox1.TabIndex = 3;
            // 
            // WaitDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(518, 240);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.labelRoomNum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "WaitDlg";
            this.Text = "WaitDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRoomNum;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox listBox1;
    }
}