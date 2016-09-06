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
    public partial class Draw : Form
    {
        Bitmap originImg;
        Image finishImg;
        Graphics g;
        //DrawType dType;
        Point StartPoint, EndPoint, FontPoint;
        Pen p = new Pen(Color.Black, 1);
        bool IsDraw;
        Font font;
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
        
        
        public Draw()
        {
            InitializeComponent();
            //cmbThickness.SelectedIndex = 0;
            //将文本输入框的父容器设为picDraw，否则显示时会出现错位  
            //txtWrite.Parent = picDraw;

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
    }
}
