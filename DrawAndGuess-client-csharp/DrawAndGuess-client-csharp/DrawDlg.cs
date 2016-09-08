using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
namespace DrawAndGuess_client_csharp
{
    public partial class DrawDlg : Form, MessageHandler
    {
        //static Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
        //float dpiX = graphics.DpiX;
        //float dpiY = graphics.DpiY;

        Bitmap originImg;
        Image finishImg;
        Graphics g;
        DrawType dType = DrawType.Pen;
        Point StartPoint, EndPoint;
        Pen p = new Pen(Color.Black, 1);
        bool IsDraw;

        private string nick;

        bool IsDrawer = false;

        /// <summary>  
        /// 画笔颜色  
        /// </summary>  
        Color DrawColor
        {
            get { return p.Color; }
            set { p.Color = value; }
        }
        /// <summary>  
        /// 画笔宽度  
        /// </summary>  
        float PenWidth
        {
            set { p.Width = value; }
        }

        public DrawDlg(int room, string nick, string[] nicks, bool isMaster)
        {
            //Console.WriteLine(dpiX);
            InitializeComponent();
            Program.RegisterMessageHandler(this, this);

            btnStart.Enabled = isMaster;
            btnStart.Text = isMaster ? "开始游戏" : "等待开始";

            this.Text = "你画我猜 - " + room.ToString() + "号房间";
            this.nick = nick;

            listView1.Items.Clear();
            foreach (string member in nicks)
            {
                ListViewItem item = new ListViewItem(new string[] { "", member, "0" });
                listView1.Items.Add(item);
            }

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            //将线帽样式设为圆线帽，否则笔宽变宽时会出现明显的缺口  
            //p.StartCap = LineCap.Round;
            //p.EndCap = LineCap.Round;

            originImg = new Bitmap(picDraw.Width, picDraw.Height);
            g = Graphics.FromImage(originImg);
            //画布背景初始化为白底  
            g.Clear(Color.White);

            picDraw.Image = originImg;
            finishImg = (Image)originImg.Clone();
        }

        /*protected override CreateParams CreateParams
        {
            get
            {
                int CS_NOCLOSE = 0x200;
                CreateParams parameters = base.CreateParams;
                parameters.ClassStyle |= CS_NOCLOSE;

                return parameters;
            }
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            dType = DrawType.Pen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dType = DrawType.Eraser;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.SendMessage("{\"method\": \"clear_pic\"}");
            OnClearPic();
        }

        private void OnClearPic()
        {
            g.Clear(Color.White);
            reDraw();
        }

        private void reDraw()
        {
            Graphics graphics = picDraw.CreateGraphics();
            graphics.DrawImage(finishImg, new Point(0, 0));
            graphics.Dispose();
        }

        /// <summary>  
        /// 画笔颜色设置  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void button4_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = DrawColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                DrawColor = cd.Color;
                Program.SendMessage("{\"method\": \"change_color\""
                    + ", \"color\": " + DrawColor.ToArgb().ToString()
                    + "}");
            }
        }



        /// <summary>  
        /// 画笔宽度设置  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  

        private void picDraw_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsDraw = IsDrawer;
                if (IsDraw)
                {
                    int compatX = /*(int)(*/e.Location.X/* * 100 / dpiX)*/;
                    int compatY = /*(int)(*/e.Location.Y/* * 100 / dpiY)*/;
                    OnDrawDown(compatX, compatY, dType == DrawType.Eraser);
                    Program.SendMessage("{\"method\": \"update_pic\""
                        + ", \"x\": " + compatX.ToString()
                        + ", \"y\": " + compatY.ToString()
                        + ", \"new_line\": true"
                        + ", \"eraser\": " + ((dType == DrawType.Eraser) ? "true" : "false")
                        + "}");
                    //LinePrintMessageSingle("你正在画图。");
                }
            }

            if (!IsDrawer)
            {
                LinePrintMessageSingle("你不是画图者或游戏暂未开始，无法画图");
            }
        }

        private void OnDrawDown(int x, int y, bool eraser)
        {
            originImg = (Bitmap)finishImg;

            //此句的作用是避免窗体最小化后还原窗体时，画布内容“丢失”  
            //其实没有丢失，只是没刷新而已，读者可以在画布任意处作画，便可还原画布内容
            picDraw.Image = originImg;

            dType = eraser ? DrawType.Eraser : DrawType.Pen;
            StartPoint = new Point(x, y);//(int)(x * dpiX / 100), (int)(y * dpiY / 100));
            finishImg = (Image)originImg.Clone();
        }

        private void picDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDraw)
            {
                int compatX = /*(int)(*/e.Location.X/* * 100 / dpiX)*/;
                int compatY = /*(int)(*/e.Location.Y/* * 100 / dpiY)*/;

                if (compatX % 2 == 0 || compatY % 2 == 0)// 过滤一部分点以加快响应速度
                {
                    OnDrawMove(compatX, compatY, dType == DrawType.Eraser);
                    Program.SendMessage("{\"method\": \"update_pic\""
                        + ", \"x\": " + compatX.ToString()
                        + ", \"y\": " + compatY.ToString()
                        + ", \"new_line\": false"
                        + ", \"eraser\": " + ((dType == DrawType.Eraser) ? "true" : "false")
                        + "}");
                }
            }
        }

        private void OnDrawMove(int x, int y, bool eraser)
        {
            dType = eraser ? DrawType.Eraser : DrawType.Pen;
            EndPoint = new Point(x, y);//(int)(x * dpiX / 100), (int)(y * dpiY / 100));
            g = Graphics.FromImage(finishImg);
            g.SmoothingMode = SmoothingMode.AntiAlias; //抗锯齿  
            switch (dType)
            {
                case DrawType.Pen:
                    g.DrawLine(p, StartPoint, EndPoint);
                    StartPoint = EndPoint;
                    break;
                case DrawType.Eraser:
                    Pen pen1 = new Pen(Color.White, 20);
                    pen1.StartCap = LineCap.Round;
                    pen1.StartCap = LineCap.Round;
                    g.DrawLine(pen1, StartPoint, EndPoint);
                    StartPoint = EndPoint;
                    pen1.Dispose();
                    break;
            }
            reDraw();
        }

        private void picDraw_MouseUp(object sender, MouseEventArgs e)
        {
            IsDraw = false;
        }

        private void AddScore(string nick, int score)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.SubItems[1].Text == nick)
                {
                    string oldScoreStr = item.SubItems[2].Text;
                    int oldScore = int.Parse(oldScoreStr);
                    int newScore = oldScore + score;
                    string newScoreStr = newScore.ToString();
                    item.SubItems[2].Text = newScoreStr;
                }
            }
        }

        public void HandleMessage(string message)
        {
            JObject obj = JObject.Parse(message);
            if (obj["method"] != null && (string)obj["method"] != "")
            {
                string method = (string)obj["method"];
                if (method == "start_game" && (bool)obj["success"])
                {
                    btnStart.Visible = false;
                }
                if (method == "submit_answer" && (bool)obj["success"])
                {
                    if ((bool)obj["win"])
                    {
                        LinePrintMessage("正确答案消息已隐藏，仅自己可见。");
                        LinePrintMessage("\"" + nick + "\"猜对了正确答案，加10分");
                        AddScore(nick, 10);
                    }
                }
            }
            else if (obj["event"] != null && (string)obj["event"] != "")
            {
                string _event = (string)obj["event"];

                if (_event == "user_join")// 用户加入
                {
                    string nick = (string)obj["nick"];
                    ListViewItem item = new ListViewItem(new string[] { "", nick, "0" });
                    listView1.Items.Add(item);
                }
                else if (_event == "user_exit")// 用户退出
                {
                    string nick = (string)obj["nick"];
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.SubItems[1].Text == nick)
                        {
                            listView1.Items.Remove(item);
                        }
                    }
                }
                else if (_event == "room_expire")// 房间解散
                {
                    MessageBox.Show("有人退出了房间，游戏结束。");
                    Close();
                    Dispose();
                }
                else if (_event == "game_start")
                {
                    int round = (int)obj["round"];
                    if (round == 1)
                    {
                        listView1.Items.Clear();

                        string[] members = (from str in obj["players"] select (string)str).ToArray();
                        foreach (string member in members)
                        {
                            ListViewItem item = new ListViewItem(new string[] { "", member, "0" });
                            listView1.Items.Add(item);
                        }
                    }

                    OnClearPic();
                    LinePrintMessage("游戏开始，当前是第" + round + "轮");
                }
                else if (_event == "generate_word")
                {
                    IsDrawer = true;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;

                    string word = (string)obj["word"];

                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.SubItems[1].Text == nick)
                        {
                            item.Text = "*";
                        }
                        else
                        {
                            item.Text = "";
                        }
                    }

                    LinePrintMessage("词语已生成：[" + word + "]，你现在是画图者，请开始画图。");
                    textBox2.Enabled = false;
                    StartTimer();
                }
                else if (_event == "word_generated")
                {
                    IsDrawer = false;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;

                    string drawerNick = (string)obj["nick"];
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.SubItems[1].Text == drawerNick)
                        {
                            item.Name = "*";
                        }
                        else
                        {
                            item.Name = "";
                        }
                    }
                    LinePrintMessage("词语已生成，请\"" + drawerNick + "\"画图。");
                    textBox2.Enabled = true;
                    StartTimer();
                }
                else if (_event == "time_up")
                {
                    LinePrintMessage("本局游戏结束");
                    textBox2.Enabled = true;
                    g.Clear(Color.White);
                    reDraw();
                    IsDrawer = false;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                }
                else if (_event == "answer_submitted")
                {
                    string nick = (string)obj["nick"];
                    if ((bool)obj["win"])
                    {
                        LinePrintMessage("\"" + nick + "\"猜对了正确答案，加10分");
                        AddScore(nick, 10);
                    }
                    else
                    {
                        LinePrintMessage(nick + ": " + (string)obj["answer"]);
                    }
                }
                else if (_event == "pic_updated")
                {
                    int x = (int)obj["x"];
                    int y = (int)obj["y"];
                    bool new_line = (bool)obj["new_line"];
                    bool eraser = (bool)obj["eraser"];
                    if (new_line)
                    {
                        OnDrawDown(x, y, eraser);
                        //LinePrintMessageSingle("画图者正在画图。");
                    }
                    else
                    {
                        OnDrawMove(x, y, eraser);
                    }
                }
                else if (_event == "color_changed")
                {
                    int argb = (int)obj["color"];
                    Color color = Color.FromArgb(argb);
                    DrawColor = color;
                }
                else if (_event == "pic_clear")
                {
                    OnClearPic();
                }
            }
        }

        public void LinePrintMessage(string text)
        {
            textBox1.AppendText(text + "\r\n");
            textBox1.ScrollToCaret();
        }

        public void LinePrintMessageSingle(string text)
        {
            if (!textBox1.Text.EndsWith("\r\n" + text + "\r\n") && textBox1.Text != text + "\r\n")
            {
                LinePrintMessage(text);
            }
        }

        ~DrawDlg()
        {
            Program.UnregisterMessageHandler(this);
        }

        /// <summary>
        /// 计时器部分
        /// </summary>

        protected int seconds = 0;

        protected System.Timers.Timer timer;

        protected void StartTimer()
        {
            if (timer != null)
            {
                timer.Stop();
            }

            seconds = 60;
            UpdateTimer();
            timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 1000;//执行间隔时间,单位为毫秒  
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer1_Elapsed);
        }

        protected void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            seconds--;
            UpdateTimer();
            if (seconds <= 0 && timer != null)
            {
                timer.Stop();
                OnTimeUp();
            }
            UpdateTimer();
        }

        protected void OnTimeUp()
        {
            if (IsDrawer)
            {
                Program.SendMessage("{\"method\": \"time_up\"}");
            }
        }

        public delegate void UIHandler();

        protected void UpdateTimer()
        {
            try
            {
                if (seconds <= 0)
                {
                    BeginInvoke(new UIHandler(() => lblTimer.Text = ""));
                }
                else
                {
                    BeginInvoke(new UIHandler(() => lblTimer.Text = "剩余时间：" + seconds + "秒"));
                }
            }
            catch { }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Program.SendMessage("{\"method\": \"start_game\"}");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Enabled && textBox2.Text != "")
            {
                LinePrintMessage(nick + ": " + textBox2.Text);
                Program.SendMessage("{\"method\": \"submit_answer\", \"answer\": \"" + textBox2.Text + "\"}");
                textBox2.Text = "";
            }
        }

        private void DrawDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.SendMessage("{\"method\": \"exit_room\"}");
        }
    }

    enum DrawType
    {
        Pen,
        Eraser
    }
}
