using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPdotNET.Tool.Common.GUI
{
    public partial class CustomProgressBar : UserControl
    {
        public CustomProgressBar()
        {
            InitializeComponent();
            mShowPercentage = false;
        }
        public override void Refresh()
        {
            base.Refresh();
            UpdateText();
        }

        //Progress Bar Interface
        public int Minimum
        {
            get
            {
                return thePB.Minimum;
            }

            set
            {
                thePB.Minimum = value;
            }
        }

        public int Maximum
        {
            get
            {
                return thePB.Maximum;
            }

            set
            {
                thePB.Maximum = value;
            }
        }

        public int Value
        {
            get
            {
                return thePB.Value;
            }

            set
            {
                thePB.Value = value;
                Refresh();
            }
        }

        public int Step
        {
            get
            {
                return thePB.Step;
            }

            set
            {
                thePB.Step = value;
            }
        }

        public ProgressBarStyle Style
        {
            get
            {
                return thePB.Style;
            }

            set
            {
                thePB.Style = value;
            }
        }

        public Color BarColor
        {
            get
            {
                return thePB.ForeColor;
            }

            set
            {
                thePB.ForeColor = value;
            }
        }

        public int MarqueeAnimationSpeed
        {
            get
            {
                return thePB.MarqueeAnimationSpeed;
            }

            set
            {
                thePB.MarqueeAnimationSpeed = value;
            }
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            thePB.Size = this.Size;
            UpdateText();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            UpdateText();
        }

        //Text Interface
        private bool mShowPercentage;
        public bool ShowPercentage
        {
            get
            {
                return mShowPercentage;
            }

            set
            {
                mShowPercentage = value;
                UpdateText();
            }
        }

        private string mText;
        public string CenterText
        {
            get
            {
                return mText;
            }

            set
            {
                mText = value;
                UpdateText();
            }
        }

        private void UpdateText()
        {
            string s;
            if (ShowPercentage)
            {
                int percent = (int)(((double)(Value - Minimum) / (double)(Maximum - Minimum)) * 100);
                s = percent.ToString() + "%";
            }
            else
            {
                if (string.IsNullOrEmpty(CenterText))
                {
                    //Dont draw anything
                    return;
                }
                else
                {
                    s = CenterText;
                }
            }

            using (Graphics gr = thePB.CreateGraphics())
            {
                gr.DrawString(s, Font, new SolidBrush(ForeColor),
                    new PointF(Width / 2 - (gr.MeasureString(s, Font).Width / 2.0F),
                        Height / 2 - (gr.MeasureString(s, Font).Height / 2.0F)));
            }
        }
    }
}
