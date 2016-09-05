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
            this.labelRoomNum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelRoomNum
            // 
            this.labelRoomNum.AutoSize = true;
            this.labelRoomNum.Location = new System.Drawing.Point(13, 13);
            this.labelRoomNum.Name = "labelRoomNum";
            this.labelRoomNum.Size = new System.Drawing.Size(67, 19);
            this.labelRoomNum.TabIndex = 0;
            this.labelRoomNum.Text = "房间号：";
            // 
            // WaitDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 377);
            this.Controls.Add(this.labelRoomNum);
            this.Name = "WaitDlg";
            this.Text = "WaitDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRoomNum;
    }
}