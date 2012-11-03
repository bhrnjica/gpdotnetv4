///This class is based on the Code project article 
///http://www.codeproject.com/Articles/18449/An-easy-way-to-add-a-Ribbon-Panel-Office-2007-styl
///An easy way to add a Ribbon Panel Office 2007 style
//By Juan Pablo G.C. | 18 Apr 2007 | Article
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GPdotNET.Tool.Common.GUI
{
    public partial class CustomPanel : Panel
    {
        public CustomPanel()
        {
            InitializeComponent();
        }
        int X0;
        int XF;
        int Y0;
        int YF;
        int T = 8;
        int i_Zero = 180;
        int i_Sweep = 90;
        int X; int Y;
        GraphicsPath path;
        int D = -1;
        int R0 = 215;
        int G0 = 227;
        int B0 = 242;
        //Color _BaseColor = Color.FromArgb(215, 227, 242);
        //Color _BaseColorOn = Color.FromArgb(215, 227, 242);
        int i_Op = 255;
        string S_TXT = "";

        public string Caption
        {
            get
            {
                return S_TXT;
            }
            set
            {
                S_TXT = value;
                this.Refresh();
            }
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {


            X0 = -3; XF = this.Width + X0 +8;
            Y0 = -2; YF = this.Height + Y0 -2;

            Point P0 = new Point(X0, Y0);
            Point PF = new Point(X0, Y0 + YF);

            
            Pen b2 = new Pen(Color.FromArgb(i_Op, R0 - 39, G0 - 24, B0 - 3));
            Pen b3 = new Pen(Color.FromArgb(i_Op, R0 + 11, G0 + 9, B0 + 3));
            //Pen b4 = new Pen(Color.FromArgb(i_Op, R0 - 8, G0 - 4, B0 - 2));
            Pen b5 = new Pen(Color.FromArgb(i_Op, R0, G0, B0));
            Pen b6 = new Pen(Color.FromArgb(i_Op, R0 - 16, G0 - 11, B0 - 5));
            Pen b8 = new Pen(Color.FromArgb(i_Op, R0 + 1, G0 + +5, B0 + 3));
             

            T = 1;
            int height = 14;
            DrawArc3(0, height+5);

            e.Graphics.PageUnit = GraphicsUnit.Pixel;
            //Brush B4 = b4.Brush;
            e.Graphics.SmoothingMode = SmoothingMode.None;
            X = X0; Y = Y0; i_Zero = 180; D = 0;

            e.Graphics.FillPath(b5.Brush, path);
            path.Dispose();

            

            LinearGradientBrush brocha = new LinearGradientBrush(P0, PF, b6.Color, b8.Color);
            DrawArc2(height, YF);
            e.Graphics.FillPath(brocha, path);
            path.Dispose();

            //golden ration from height1
            int height2 = (int)((float)height * 1.61);

            DrawArc2(YF-height2, height2);
            Pen bdown = new Pen(Color.FromArgb(i_Op, R0 - 22, G0 - 11, B0));
            e.Graphics.FillPath(bdown.Brush, path);
            path.Dispose();

            T = 6;
            DrawArc();
            DrawArc();
            e.Graphics.DrawPath(b2, path);
            path.Dispose();

            DrawArc();
            e.Graphics.DrawPath(b3, path);
            path.Dispose();


            Point P_EX = Cursor.Position;
            P_EX = this.PointToClient(P_EX);

            int ix = 10 + this.Width / 2 - S_TXT.Length * (int)this.Font.Size / 2;
            PointF P_TXT = new PointF(ix, this.Height - height2-4);
            Pen pen = new Pen(this.ForeColor);
            e.Graphics.DrawString(S_TXT, this.Font, pen.Brush, P_TXT);

            base.OnPaint(e);


        }

        public void DrawArc()
        {
            X = X0; 
            Y = Y0; 
            i_Zero = 180; 
            D++;
            path = new GraphicsPath();
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep); 
            
            i_Zero += 90; 
            X += XF - 6;
            
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep); 
            
            i_Zero += 90; 
            Y += YF - 6;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep); 
            
            i_Zero += 90; 
            X -= XF - 6;
            path.AddArc(X + D, Y - D, T, T, i_Zero, i_Sweep); 
            
            i_Zero += 90; 
            Y -= YF - 6;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep);
        }

        public void DrawArc2(int OF_Y, int SW_Y)
        {
            path = new GraphicsPath();
            X = X0 + 5; 
            Y = Y0 + OF_Y; 
            i_Zero = 180;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep); 

            i_Zero += 90; 
            X += XF - 5;
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep);
 
            i_Zero += 90; 
            Y += SW_Y;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep);
 
            i_Zero += 90; 
            X -= XF - 5;
            path.AddArc(X + D, Y - D, T, T, i_Zero, i_Sweep); 

            i_Zero += 90; 
            Y -= SW_Y;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep);
        }

        public void DrawArc3(int OF_Y, int SW_Y)
        {
            path = new GraphicsPath();
            X = X0; 
            Y = Y0 + OF_Y; 
            i_Zero = 180;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep); 

            i_Zero += 90; 
            X += XF;
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep);
 
            i_Zero += 90; 
            Y += SW_Y;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep);
 
            i_Zero += 90; 
            X -= XF;
            path.AddArc(X + D, Y - D, T, T, i_Zero, i_Sweep); 

            i_Zero += 90; 
            Y -= SW_Y;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep);
        }
    }
}
