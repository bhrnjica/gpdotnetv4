///This class is based on the Code project article 
///http://www.codeproject.com/Articles/18449/An-easy-way-to-add-a-Ribbon-Panel-Office-2007-styl
///An easy way to add a Ribbon Panel Office 2007 style
//By Juan Pablo G.C. | 18 Apr 2007 | Article
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GPdotNET.Util;

namespace GPdotNET.Tool.Common.GUI
{
    public partial class CustomToolTip : Form
    {
        private string _title = "";
        private string _comment = "";
        private string _picture = "";
        private Color _fillcolor;
        private Image img;

        private int XC = 8, YC = 20, WC = 220, HC = 90;
        int X0;
        int XF;
        int Y0;
        int YF;
        int T = 2;
        int i_Zero = 180;
        int i_Sweep = 90;
        int X; int Y;
        GraphicsPath path;
        int D = -1;

        OffInfoShadow shadow;

        public CustomToolTip()
        {
            //Transparency and Alpha Channel Enabled
            this.BackColor = Color.Fuchsia;
            this.TransparencyKey = Color.Fuchsia;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            this.ShowInTaskbar = false;
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = 10;
            this.Height = 10;
            this.Opacity = 0.8;
            this.StartPosition = FormStartPosition.Manual;

            timer.Interval = 5;
            timer.Tick += new EventHandler(timer_Tick);

        }


        private Timer timer = new Timer();
        private int ms = 300, j = 10;
        private bool appearing = true;

        protected override void OnLoad(EventArgs e)
        {
            shadow = new OffInfoShadow();
            shadow.Location = new Point(this.Location.X + 8, this.Location.Y + 12);
            shadow.Size = this.Size;
            shadow.Show();
            timer.Start();
            base.OnLoad(e);
        }

        public string Title
        {
            get { return _title; }
            set
            {
                this.Size = new Size(150, 30);
                _title = value;
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (value != "")
                {
                    this.Size = new Size(240, 100);
                    _comment = value;
                }
            }
        }

        public string Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                if (value != "")
                {
                    this.Size = new Size(380, 180);
                    XC = 122; YC = 30; WC = 240; HC = 120;
                    _picture = value;
                    try
                    {
                       // img = Image.FromFile(_picture);
                        img = GPModelGlobals.LoadImageFromName(_picture);
                    }
                    catch { }
                }
            }
        }

        public Color FillColor
        {
            get { return _fillcolor; }
            set { _fillcolor = value; }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (appearing)
            {
                if (this.Opacity == 1)
                {
                    if (j < ms)
                    {
                        j++;
                    }
                    else
                    {
                        appearing = !appearing;
                    }
                }
                else
                {
                    this.Opacity += 0.1;
                }
            }
            if (!appearing)
            {
                if (this.Opacity == 0)
                {

                    this.Close();
                }
                else
                {
                    this.Opacity -= 0.2;
                    shadow.Close();
                }
            }

        }

        public new void Close()
        {
            appearing = false;
            timer.Start();
        }


        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            //Paint the Background             
            T = 6; D = -1; X0 = 0; Y0 = 0; XF = e.ClipRectangle.Width - 10; YF = e.ClipRectangle.Height - 7;
            Rectangle rpath = new Rectangle(X0, Y0, XF, YF + 14);
            DrawArc();
            try
            {
                LinearGradientBrush b;
                if (this._fillcolor.GetBrightness() > 0.5)
                {
                    this.ForeColor = Color.FromArgb(0, 0, 0);
                    b = new LinearGradientBrush(rpath, Color.White, _fillcolor, LinearGradientMode.Vertical);
                }
                else
                {
                    this.ForeColor = Color.FromArgb(220, 220, 220);
                    b = new LinearGradientBrush(rpath, _fillcolor, Color.FromArgb(60, 60, 60), LinearGradientMode.Vertical);

                }
                e.Graphics.FillPath(b, path);
            }
            catch { }
            T = 6; D = -1; X0 = 0; Y0 = 0; XF = e.ClipRectangle.Width - 10; YF = e.ClipRectangle.Height - 7;
            rpath = new Rectangle(X0, Y0, XF, YF + 14);
            DrawArc();

            e.Graphics.DrawPath(new Pen(Color.FromArgb(118, 118, 118)), path);


            //Title
            Point TitlePos = new Point(5, 3);
            Font TitleFont = new Font(this.Font, FontStyle.Bold);
            Pen ForePen = new Pen(this.ForeColor);
            g.DrawString(_title, TitleFont, ForePen.Brush, TitlePos);
            //Comment
            RectangleF rect = new RectangleF(XC, YC, WC, HC);
            g.DrawString(_comment, this.Font, ForePen.Brush, rect);

            if (img != null)
            {
                g.DrawImage(img, 12, 30, img.Width, img.Height);
                Pen pline = new Pen(_fillcolor);
                g.DrawLine(pline, 5, 166, 372, 166);
                g.DrawLine(new Pen(Color.White), 5, 167, 372, 167);
            }

            base.OnPaint(e);


        }

        public void DrawArc()
        {
            X = X0; Y = Y0; i_Zero = 180; D++;
            path = new GraphicsPath();
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; X += XF;
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y += YF;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; X -= XF;
            path.AddArc(X + D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y -= YF;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep);
        }



       //private Pen p = new Pen(Color.Black, 8.0f);
       // private Brush b = new SolidBrush(Color.FromArgb(160, 0, 255, 0));


    }

    public class OffInfoShadow : Form
    {
        int X0, XF, Y0, YF;
        int T = 2;
        int i_Zero = 180;
        int i_Sweep = 90;
        int X; int Y;
        GraphicsPath path;
        int D = -1;

        public OffInfoShadow()
        {
            this.ShowInTaskbar = false;
            this.BackColor = Color.FromArgb(255, 255, 255);
            this.TransparencyKey = Color.FromArgb(255, 255, 255);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Opacity = 0.5;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            T = 12; D = -1; X0 = -2; Y0 = -2; XF = e.ClipRectangle.Width - 14; YF = e.ClipRectangle.Height - 14;
            //Rectangle rpath = new Rectangle(X0, Y0, XF, YF); 
			DrawArc();
            Color HBlack = Color.FromArgb(120, 100, 100, 100);
            PathGradientBrush pgb = new PathGradientBrush(path);
            Color SBlack = Color.FromArgb(255, 100, 100, 100);
            pgb.CenterColor = SBlack;
            pgb.SurroundColors = new Color[] { HBlack };
            pgb.FocusScales = new PointF(0.96f, 0.92f);
            g.FillPath(pgb, path);

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }

        public void DrawArc()
        {
            X = X0; Y = Y0; i_Zero = 180; D++;
            path = new GraphicsPath();
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; X += XF;
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y += YF;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; X -= XF;
            path.AddArc(X + D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y -= YF;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep);
        }
    }
}
