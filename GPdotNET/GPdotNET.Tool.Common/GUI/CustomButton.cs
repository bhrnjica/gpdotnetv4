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
using System.Diagnostics;

namespace GPdotNET.Tool.Common.GUI
{
    public partial class CustomButton : Button
    {
               //Timer
        private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();

        //Images
        private Image _img_on;
        private Image _img_click;
        private Image _img_back;
        private Image _img;
        private Image _img_fad;
        private String s_folder;
        private String s_filename;
        private String _infotitle = "";
        private String _infocomment = "";
        private String _infoimage = "";
        private Color _infocolor = Color.FromArgb(201, 217, 239);
        //private Color _TextColor = Color.Black;

        private Image _toshow;

        //Fading
        bool b_fad = false;
        int i_fad = 0; //0 nothing, 1 entering, 2 leaving
        int i_value = 255; //Level of transparency


        //InfoForm
        CustomToolTip info=null;



        //Constructor
        public CustomButton()
        {

            this.SetStyle(ControlStyles.ResizeRedraw |
                           ControlStyles.SupportsTransparentBackColor |
                           ControlStyles.UserPaint |
                           ControlStyles.AllPaintingInWmPaint |
                           ControlStyles.DoubleBuffer, true);


            this.BackColor = Color.Transparent;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FlatAppearance.BorderSize = 0;
            this.TextAlign = ContentAlignment.BottomCenter;
            this.ImageAlign = ContentAlignment.TopCenter;
            this.FlatAppearance.BorderColor = Color.FromArgb(100, 255, 255, 255);
            this.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 255, 255, 255);
            this.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, 255, 255, 255);
            this._toshow = this._img_back;
            this._isToolTipEnabled = false;
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2.Interval = 10;
            timer2.Tick += new EventHandler(timer2_Tick);


        }

        //Properties
        public Image img_on
        {
            get { return _img_on; }
            set { _img_on = value; }
        }
        public Image img_click
        {
            get { return _img_click; }
            set { _img_click = value; }
        }
        public Image img_back
        {
            get { return _img_back; }
            set
            {
                _img_back = value;
            }
        }
        public Image img
        {
            get { return _img; }
            set
            {
                _img = value;
                this.Image = _img;
            }
        }
        public string folder
        {
            get { return s_folder; }
            set
            {
                if (value != null)
                {
                    if ((char)value[value.Length - 1] != '\\')
                    {
                        s_folder = value + "\\";
                    }
                    else
                    {
                        s_folder = value;
                    }
                }

            }
        }
        public string filename
        {
            get { return s_filename; }
            set
            {
                s_filename = value;

                if (s_folder != null & s_filename != null)
                {
                    _img = Image.FromFile(s_folder + s_filename);
                    this.Image = _img;
                }
            }
        }
        public string InfoTitle
        {
            get { return _infotitle; }
            set
            {
                _infotitle = value;
            }
        }
        public string InfoComment
        {
            get { return _infocomment; }
            set
            {
                _infocomment = value;
            }
        }

        public string InfoImage
        {
            get { return _infoimage; }
            set
            {
                _infoimage = value;
            }
        }

        public Color InfoColor
        {
            get { return _infocolor; }
            set { _infocolor = value; }
        }

        private bool _isToolTipEnabled;
        public bool IsToolTipEnabled
        {
            get { return _isToolTipEnabled; }
            set { _isToolTipEnabled = value; }
        }
        //Methods
        public void PaintBackground()
        {
            if (b_fad)
            {
                object _img_temp = new object();
                if (i_fad == 1)
                {
                    _img_temp = _img_on.Clone();
                }
                else if (i_fad == 2)
                {
                    _img_temp = _img_back.Clone();
                }
                _img_fad = (Image)_img_temp;
                Graphics _grf = Graphics.FromImage(_img_fad);
                SolidBrush brocha = new SolidBrush(Color.FromArgb(i_value, 255, 255, 255));
                _grf.FillRectangle(brocha, 0, 0, _img_fad.Width, _img_fad.Height);
                this.BackgroundImage = _img_fad;
            }
        }

        int t = 0, t_end = 100;

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (t < t_end)
            {
                t++;
            }
            else
            {
                timer2.Stop();
                t = 0;
                ShowInfo();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (i_fad)
            {
                case 1:
                    {
                        if (i_value == 0)
                        {
                            i_value = 255;
                        }
                        if (i_value > -1)
                        {
                            PaintBackground();
                            i_value -= 10;
                        }
                        else
                        {
                            i_value = 0;
                            PaintBackground();
                            timer1.Stop();
                        }
                        break;
                    }
                case 2:
                    {
                        if (i_value == 0)
                        {
                            i_value = 255;
                        }
                        if (i_value > -1)
                        {
                            PaintBackground();
                            i_value -= 10;
                        }
                        else
                        {
                            i_value = 0;
                            PaintBackground();
                            timer1.Stop();
                        }
                        break;

                    }
            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            if (b_fad)
            {
                i_fad = 1;
                timer1.Start();
            }
            else
            {
                this.BackgroundImage = _img_on;
                _toshow = _img_on;
            }
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (b_fad) { i_fad = 2; timer1.Start(); }
            else
            {
                this.BackgroundImage = _img_back;
                _toshow = _img_back;
            }

            //Close the info form
            if (info != null)
            {
                info.Close();
            }
            timer2.Stop();
            base.OnMouseLeave(e);

        }


        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs mevent)
        {

            this.BackgroundImage = _img_click;
            this._toshow = _img_click;
            base.OnMouseDown(mevent);
        }
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs mevent)
        {
            this.BackgroundImage = _img_on;
            this._toshow = _img_on;
            base.OnMouseUp(mevent);
        }

        public enum Side { UpLeft, UpRight, DownLeft, DownRight }

        protected override void OnMouseHover(EventArgs e)
        {
            timer2.Start();
            base.OnMouseHover(e);
        }

        public void ShowInfo()
        {
            if (!IsToolTipEnabled)
                return;
            return;//version 2 will not support tooltip
            //info = new CustomToolTip();
            //info.Title = _infotitle;
            //info.Comment = _infocomment;
            //info.Picture = _infoimage;
            //info.FillColor = _infocolor;
            //if (GetInfoLocation() == Side.UpLeft)
            //{
            //    info.Location = new Point(Cursor.Position.X, Application.OpenForms[0].Location.Y + this.Bottom + 80);

            //}
            //else
            //{
            //    info.Location = new Point(Cursor.Position.X - info.Width, Application.OpenForms[0].Location.Y + this.Bottom + 80);
            //}

            //info.Show();
        }

        public Side GetInfoLocation()
        {
            int CPX = Cursor.Position.X - Application.OpenForms[0].Location.X;
            int HSX = Application.OpenForms[0].Width / 2;
            if (CPX < HSX)
            {
                return Side.UpLeft;
            }
            else
            {
                return Side.UpRight;
            }
        }


        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //para pintar el fondo hay que tener en cuenta el 
            //desplazamiento con el control contenedor
            if (this.Parent != null)
            {
                GraphicsContainer cstate = pevent.Graphics.BeginContainer();
                pevent.Graphics.TranslateTransform(-this.Left, -this.Top);
                Rectangle clip = pevent.ClipRectangle;
                clip.Offset(this.Left, this.Top);
                PaintEventArgs pe = new PaintEventArgs(pevent.Graphics, clip);
                //pinta el fondo del contenedor
                InvokePaintBackground(this.Parent, pe);
                //pinta el resto del contenedor
                InvokePaint(this.Parent, pe);
                //restaura el Graphics a su estado original
                pevent.Graphics.EndContainer(cstate);
            }
            else
                base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (this.Parent != null)
            {
                GraphicsContainer cstate = pevent.Graphics.BeginContainer();
                pevent.Graphics.TranslateTransform(-this.Left, -this.Top);
                Rectangle clip = pevent.ClipRectangle;
                clip.Offset(this.Left, this.Top);
                PaintEventArgs pe = new PaintEventArgs(pevent.Graphics, clip);
                
                InvokePaint(this.Parent, pe);
                
                pevent.Graphics.EndContainer(cstate);

                Graphics g = pevent.Graphics;
                try {
                    if(_toshow!=null)
                     g.DrawImage(_toshow, pevent.ClipRectangle); 
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                }

                SizeF textsize = g.MeasureString(this.Text, this.Font);
                Rectangle rect = pevent.ClipRectangle;
                int X = 0;
                try
                {

                    int newHeight = rect.Height - (int)textsize.Height - (int)(textsize.Height/5);
                    int newWidth = newHeight  * _img.Width / _img.Height;

                    int xPos = (pevent.ClipRectangle.Width - newWidth) / 2;
                    //int yPos = (pevent.ClipRectangle.Height - newHeight) / 2;
                    Point _imgpos = new Point(xPos, 4);

                    Rectangle r = new Rectangle(_imgpos, new Size(newWidth, newHeight));
                    g.DrawImage(_img, r);
                }
                catch (Exception ex) 
                {
                    Debug.WriteLine(ex.Message);

                }
                
                
                X = rect.X + rect.Width; X = (X - (int)textsize.Width) / 2;
                int Y = rect.Y + rect.Height; Y = Y - (int)textsize.Height - 2;

                Point _textpos = new Point(X, Y);
                Pen PForeColor = new Pen(this.ForeColor);

                g.DrawString(this.Text, this.Font, PForeColor.Brush, _textpos);

            }
            else
                base.OnPaint(pevent);
        }


    }
}
