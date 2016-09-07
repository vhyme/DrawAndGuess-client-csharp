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

namespace DrawAndGuess_client_csharp
{
    public partial class DrawDlg : Form, MessageHandler
    {
        Bitmap originImg;
        Image finishImg;
        Graphics g;
        DrawType dType = DrawType.Pen;
        Point StartPoint, EndPoint;
        Pen p = new Pen(Color.Black, 1);
        bool IsDraw;
        Rectangle FontRect;

        string nick;
        bool isDrawer = false;

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

        public DrawDlg(int room, string nick)
        {
            InitializeComponent();
            Program.RegisterMessageHandler(this, this);

            this.Text = "你画我猜 - " + room.ToString() + "号房间";
            this.nick = nick;

            Program.SendMessage("{\"method\": \"start_game\"}");

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
                IsDraw = isDrawer;
                StartPoint = e.Location;
                finishImg = (Image)originImg.Clone();
            }
            if (!isDrawer)
            {
                LinePrintMessageSingle("你不是画图者或游戏暂未开始，无法画图");
            }
        }

        private void picDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDraw)
            {
                EndPoint = e.Location;
                if (dType != DrawType.Pen && dType != DrawType.Eraser)
                {
                    finishImg = (Image)originImg.Clone();
                }
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
        }

        private void picDraw_MouseUp(object sender, MouseEventArgs e)
        {
            IsDraw = false;
            originImg = (Bitmap)finishImg;

            //此句的作用是避免窗体最小化后还原窗体时，画布内容“丢失”  
            //其实没有丢失，只是没刷新而已，读者可以在画布任意处作画，便可还原画布内容  
            picDraw.Image = originImg;
        }

        public void HandleMessage(string message)
        {
            JObject obj = JObject.Parse(message);
            if (obj["method"] != null && (string)obj["method"] != "")
            {
                string method = (string)obj["method"];
                if (method == "start_game" && !(bool)obj["success"])
                {
                    // 创建房间失败，此时不需要弹出窗口，因为Program中已经弹出了错误信息
                    // 此处只要负责退出即可
                    Close();
                    Dispose();
                }
            }
            else if (obj["event"] != null && (string)obj["event"] != "")
            {
                string _event = (string) obj["event"];

                if (_event == "game_start")
                {
                    listView1.Clear();
                    string[] members = (from str in obj["players"] select (string)str).ToArray();
                    foreach (string member in members)
                    {
                        ListViewItem item = new ListViewItem(new string[] { "", member, "0" });
                        listView1.Items.Add(item);
                    }
                }
                if (_event == "generate_word")
                {
                    isDrawer = true;
                    string word = (string)obj["word"];
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.SubItems[1].ToString() == nick)
                        {
                            item.SubItems[0].Text = "*";
                        }
                        else
                        {
                            item.SubItems[0].Text = "";
                        }
                    }
                    LinePrintMessage("词语已生成：[" + word + "]，你现在是画图者，请开始画图。");
                }
                else if (_event == "word_generated")
                {
                    isDrawer = false;
                    string drawerNick = (string)obj["nick"];
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.SubItems[1].ToString() == drawerNick)
                        {
                            item.SubItems[0].Text = "*";
                        }
                        else
                        {
                            item.SubItems[0].Text = "";
                        }
                    }
                    LinePrintMessage("词语已生成，请\"" + drawerNick + "\"画图。");
                }
            }
        }

        public void LinePrintMessage(string text)
        {
            textBox1.Text += text + "\n";
        }

        public void LinePrintMessageSingle(string text)
        {
            if (textBox1.Text.EndsWith("\n" + text + "\n") || textBox1.Text == text + "\n")
            {
                textBox1.Text += text + "\n";
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
            if (seconds <= 0 && timer != null)
            {
                timer.Stop();
            }
            UpdateTimer();
        }

        protected void UpdateTimer()
        {
            if (seconds <= 0)
            {
                lblTimer.Text = "";
            }
            else 
            {
                lblTimer.Text = "剩余时间：" + seconds + "秒";
            }
        }
    }

    enum DrawType
    {
        Pen,
        Eraser
    }
}
