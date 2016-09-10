using System.Drawing;
namespace DrawAndGuess_client_csharp
{
    partial class BeginDialog
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BeginDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnJoin = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.pnlJoin = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxRoomId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxNickJoin = new System.Windows.Forms.TextBox();
            this.btnJoinSubmit = new System.Windows.Forms.Button();
            this.pnlCreate = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxNickCreate = new System.Windows.Forms.TextBox();
            this.btnCreateSubmit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlJoin.SuspendLayout();
            this.pnlCreate.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(31, 267);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 57);
            this.label1.TabIndex = 4;
            this.label1.Text = "你画我猜";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(743, -1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 35);
            this.button1.TabIndex = 5;
            this.button1.Text = "×";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnJoin.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnJoin.FlatAppearance.BorderSize = 3;
            this.btnJoin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoin.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnJoin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnJoin.Location = new System.Drawing.Point(29, 107);
            this.btnJoin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(175, 51);
            this.btnJoin.TabIndex = 1;
            this.btnJoin.Text = "加入房间";
            this.btnJoin.UseVisualStyleBackColor = false;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCreate.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnCreate.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCreate.FlatAppearance.BorderSize = 3;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCreate.Location = new System.Drawing.Point(29, 34);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(175, 51);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "创建房间";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // pnlJoin
            // 
            this.pnlJoin.BackColor = System.Drawing.Color.Transparent;
            this.pnlJoin.Controls.Add(this.label4);
            this.pnlJoin.Controls.Add(this.tbxRoomId);
            this.pnlJoin.Controls.Add(this.label3);
            this.pnlJoin.Controls.Add(this.tbxNickJoin);
            this.pnlJoin.Controls.Add(this.btnJoinSubmit);
            this.pnlJoin.Location = new System.Drawing.Point(217, 0);
            this.pnlJoin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlJoin.Name = "pnlJoin";
            this.pnlJoin.Size = new System.Drawing.Size(555, 203);
            this.pnlJoin.TabIndex = 5;
            this.pnlJoin.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(42, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 27);
            this.label4.TabIndex = 4;
            this.label4.Text = "房间号";
            // 
            // tbxRoomId
            // 
            this.tbxRoomId.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxRoomId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxRoomId.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbxRoomId.Location = new System.Drawing.Point(47, 82);
            this.tbxRoomId.Name = "tbxRoomId";
            this.tbxRoomId.Size = new System.Drawing.Size(165, 38);
            this.tbxRoomId.TabIndex = 3;
            this.tbxRoomId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxRoomId_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(217, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "设置昵称";
            // 
            // tbxNickJoin
            // 
            this.tbxNickJoin.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxNickJoin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxNickJoin.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbxNickJoin.Location = new System.Drawing.Point(222, 82);
            this.tbxNickJoin.Name = "tbxNickJoin";
            this.tbxNickJoin.Size = new System.Drawing.Size(165, 38);
            this.tbxNickJoin.TabIndex = 1;
            this.tbxNickJoin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxNickJoin_KeyDown);
            // 
            // btnJoinSubmit
            // 
            this.btnJoinSubmit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnJoinSubmit.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnJoinSubmit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnJoinSubmit.FlatAppearance.BorderSize = 3;
            this.btnJoinSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoinSubmit.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnJoinSubmit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnJoinSubmit.Location = new System.Drawing.Point(411, 75);
            this.btnJoinSubmit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnJoinSubmit.Name = "btnJoinSubmit";
            this.btnJoinSubmit.Size = new System.Drawing.Size(107, 51);
            this.btnJoinSubmit.TabIndex = 0;
            this.btnJoinSubmit.Text = "加入";
            this.btnJoinSubmit.UseVisualStyleBackColor = false;
            this.btnJoinSubmit.Click += new System.EventHandler(this.btnJoinSubmit_Click);
            // 
            // pnlCreate
            // 
            this.pnlCreate.BackColor = System.Drawing.Color.Transparent;
            this.pnlCreate.Controls.Add(this.label2);
            this.pnlCreate.Controls.Add(this.tbxNickCreate);
            this.pnlCreate.Controls.Add(this.btnCreateSubmit);
            this.pnlCreate.Location = new System.Drawing.Point(217, 0);
            this.pnlCreate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlCreate.Name = "pnlCreate";
            this.pnlCreate.Size = new System.Drawing.Size(555, 203);
            this.pnlCreate.TabIndex = 4;
            this.pnlCreate.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(42, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "设置昵称";
            // 
            // tbxNickCreate
            // 
            this.tbxNickCreate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxNickCreate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxNickCreate.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbxNickCreate.Location = new System.Drawing.Point(47, 82);
            this.tbxNickCreate.Name = "tbxNickCreate";
            this.tbxNickCreate.Size = new System.Drawing.Size(340, 38);
            this.tbxNickCreate.TabIndex = 1;
            this.tbxNickCreate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxNickCreate_KeyDown);
            // 
            // btnCreateSubmit
            // 
            this.btnCreateSubmit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCreateSubmit.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnCreateSubmit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCreateSubmit.FlatAppearance.BorderSize = 3;
            this.btnCreateSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateSubmit.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateSubmit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCreateSubmit.Location = new System.Drawing.Point(411, 75);
            this.btnCreateSubmit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreateSubmit.Name = "btnCreateSubmit";
            this.btnCreateSubmit.Size = new System.Drawing.Size(107, 51);
            this.btnCreateSubmit.TabIndex = 0;
            this.btnCreateSubmit.Text = "创建";
            this.btnCreateSubmit.UseVisualStyleBackColor = false;
            this.btnCreateSubmit.Click += new System.EventHandler(this.btnCreateSubmit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlJoin);
            this.panel1.Controls.Add(this.pnlCreate);
            this.panel1.Controls.Add(this.btnCreate);
            this.panel1.Controls.Add(this.btnJoin);
            this.panel1.Location = new System.Drawing.Point(24, 341);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 190);
            this.panel1.TabIndex = 3;
            // 
            // BeginDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(809, 553);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "BeginDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "你画我猜";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BeginDialog_MouseDown);
            this.pnlJoin.ResumeLayout(false);
            this.pnlJoin.PerformLayout();
            this.pnlCreate.ResumeLayout(false);
            this.pnlCreate.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Panel pnlJoin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxRoomId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxNickJoin;
        private System.Windows.Forms.Button btnJoinSubmit;
        private System.Windows.Forms.Panel pnlCreate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxNickCreate;
        private System.Windows.Forms.Button btnCreateSubmit;
        private System.Windows.Forms.Panel panel1;
    }
}

