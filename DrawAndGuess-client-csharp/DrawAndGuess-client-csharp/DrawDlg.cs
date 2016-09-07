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

namespace DrawAndGuess_client_csharp
{
    public partial class DrawDlg : Form
    {
        Bitmap originImg;
        Image finishImg;
        Graphics g;
        DrawType dType = DrawType.Pen;
        Point StartPoint, EndPoint, FontPoint;
        Pen p = new Pen(Color.Black, 1);
        bool IsDraw;
        Rectangle FontRect;
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

        public DrawDlg(int room, string[] members)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            //将线帽样式设为圆线帽，否则笔宽变宽时会出现明显的缺口  
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.Round;

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
                IsDraw = true;
                StartPoint = e.Location;
                finishImg = (Image)originImg.Clone();
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
                    case DrawType.Line:
                        g.DrawLine(p, StartPoint, EndPoint);
                        break;
                    case DrawType.Pen:
                        g.DrawLine(p, StartPoint, EndPoint);
                        StartPoint = EndPoint;
                        break;
                    case DrawType.Rect:
                        Point leftTop = new Point(StartPoint.X, StartPoint.Y);
                        int width = Math.Abs(StartPoint.X - e.X), height = Math.Abs(StartPoint.Y - e.Y);
                        if (e.X < StartPoint.X)
                            leftTop.X = e.X;
                        if (e.Y < StartPoint.Y)
                            leftTop.Y = e.Y;
                        Rectangle rect = new Rectangle(leftTop, new Size(width, height));
                        g.DrawRectangle(p, rect);
                        break;
                    case DrawType.Ellipse:
                        leftTop = new Point(StartPoint.X, StartPoint.Y);
                        int Ewidth = Math.Abs(StartPoint.X - e.X), Eheight = Math.Abs(StartPoint.Y - e.Y);
                        if (e.X < StartPoint.X)
                            leftTop.X = e.X;
                        if (e.Y < StartPoint.Y)
                            leftTop.Y = e.Y;
                        rect = new Rectangle(leftTop, new Size(Ewidth, Eheight));
                        g.DrawEllipse(p, rect);
                        break;
                    case DrawType.Eraser:
                        Pen pen1 = new Pen(Color.White, 20);
                        pen1.StartCap = LineCap.Round;
                        pen1.StartCap = LineCap.Round;
                        g.DrawLine(pen1, StartPoint, EndPoint);
                        StartPoint = EndPoint;
                        pen1.Dispose();
                        break;
                    case DrawType.Write:  //写字前画虚线框  
                        leftTop = new Point(StartPoint.X, StartPoint.Y);
                        int w = Math.Abs(StartPoint.X - e.X);
                        int h = Math.Abs(StartPoint.Y - e.Y);
                        if (e.X < StartPoint.X)
                            leftTop.X = e.X;
                        if (e.Y < StartPoint.Y)
                            leftTop.Y = e.Y;
                        FontRect = new Rectangle(leftTop, new Size(w, h));
                        Pen pRect = new Pen(Color.Black);
                        pRect.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };
                        g.DrawRectangle(pRect, FontRect);
                        pRect.Dispose();
                        break;
                }
                reDraw();
            }
        }

        private void picDraw_MouseUp(object sender, MouseEventArgs e)
        {
            IsDraw = false;
            originImg = (Bitmap)finishImg;
            if (dType == DrawType.Write)
            {
                //清除虚线框  
                Pen pRect = new Pen(Color.White);
                g.DrawRectangle(pRect, FontRect);
                pRect.Dispose();
            }

            //此句的作用是避免窗体最小化后还原窗体时，画布内容“丢失”  
            //其实没有丢失，只是没刷新而已，读者可以在画布任意处作画，便可还原画布内容  
            picDraw.Image = originImg;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



    }
    enum DrawType
    {
        None,
        Pen,
        Line,
        Rect,
        Ellipse,
        Eraser,
        Write
    }
}
