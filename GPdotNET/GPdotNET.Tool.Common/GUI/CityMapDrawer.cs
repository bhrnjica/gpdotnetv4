using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPdotNET.Tool.Common.GUI
{
    public partial class CityMapDrawer : Panel
    {
        double[][] data;
        int[] path;
        int _maxX = int.MinValue;
        int _maxY = int.MinValue;
        int _minX = int.MaxValue;
        int _minY = int.MaxValue;
        int ofsetXY = 70;
        double _scaleX=0; 
        double _scaleY=0; 
        int realOffsetX=0; 
        int realOffsetY=0; 
        public CityMapDrawer()
            : base()
        {

            this.BackColor = SystemColors.Window;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.AutoScroll = true;
            path = null;
           
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            Pen linePen = new Pen(System.Drawing.Color.Gray);
            Graphics grphx = this.CreateGraphics();
            grphx.Clear(this.BackColor);

            //for (int i = 1; i < 11; i++)
            //{
            //    //Draw verticle line
            //    grphx.DrawLine(linePen,
            //        (this.ClientSize.Width / 10) * i,
            //        0,
            //        (this.ClientSize.Width / 10) * i,
            //        this.ClientSize.Height);

            //    //Draw horizontal line
            //    grphx.DrawLine(linePen,
            //        0,
            //        (this.ClientSize.Height / 10) * i,
            //        this.ClientSize.Width,
            //        (this.ClientSize.Height / 10) * i);
            //}
            linePen.Dispose();
            base.OnPaint(pe);
            if(data!=null && data.Length>0)
            {
                drawMap(pe.Graphics);
                if (path != null)
                    drawPath(pe.Graphics);
            }
               
        }

        private void drawPath(Graphics graphics)
        {
            Pen pn = new Pen(Brushes.Blue,1);
           
            for (int i = 0; i < data.Length; i++)
            {
                int index1, index2= 0;
                index1=path[i];
                if (i + 1 == data.Length)
                    index2= path[0];
                else
                    index2= path[i+1];

                var x1 = (int)(data[index1][0] * _scaleX + realOffsetX);
                var y1 = (int)(data[index1][1] * _scaleY + realOffsetY);
                var x2 = (int)(data[index2][0] * _scaleX + realOffsetX);
                var y2 = (int)(data[index2][1] * _scaleY + realOffsetY);
                
                Point pt1 = new Point(x1,y1);
                Point pt2 = new Point(x2, y2);
                graphics.DrawLine(pn,pt1, pt2);
            }
            if(pn!=null)
                pn.Dispose();
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        private void drawMap(Graphics graphics)
        {
            var drawFont = new Font("Arial", 8);
            var drawBrush = new SolidBrush(Color.Black);

            double [][]_data=null;
           
            var size = this.Size;

            _scaleX = ((double)size.Width - ofsetXY) / (double)Math.Abs(_maxX - _minX);
            _scaleY = ((double)size.Height - ofsetXY) / (double)Math.Abs(_maxY - _minY);

            //
            realOffsetX = 0 - (int)(_minX * _scaleX) + ofsetXY/2;
            realOffsetY = 0 - (int)(_minY * _scaleY) + ofsetXY/2;
            _data = data;
            for (int i = 0; i < data.Length; i++)
            {
                var w = 8;
                var x = (int)(data[i][0] * _scaleX + realOffsetX-4);
                var y = (int)(data[i][1] * _scaleY + realOffsetY-4);

                var r = new Rectangle(x, y, w, w);
                graphics.DrawString("C" + (i+1).ToString(), drawFont, drawBrush, x + w/2, y+w/2);
                graphics.FillEllipse(Brushes.Red, r);
            }

            //dispose
            drawFont.Dispose();
            drawBrush.Dispose();
           
        }

        internal void SetData(int maxX, int maxY, int minX, int minY, double[][] d)
        {
            data=d;
            _maxX = maxX;
            _maxY = maxY;
            _minX = minX;
            _minY = minY;
        }

        internal void SetPath(int [] _path)
        {
            path = _path;
            this.Invalidate();
        }
    }
}
