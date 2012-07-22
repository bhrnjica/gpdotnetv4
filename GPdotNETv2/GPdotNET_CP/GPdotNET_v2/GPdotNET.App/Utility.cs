using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using GPdotNET.Engine;
using GPdotNET.Core;
using GPdotNET.Util;
using System.Drawing;
using System.Reflection;

namespace GPdotNET.App
{
    public static class Utility
    {
        public static Image LoadImageFromName(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var pic = asm.GetManifestResourceStream(name/*"GPdotNET.App.Resources.gpabout.png"*/);
            return Image.FromStream(pic);
        }

        public static Icon LoadIconFromName(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var pic = asm.GetManifestResourceStream(name/*"GPdotNET.App.Resources.gpabout.png"*/);
            return  new Icon(pic);
        }
/*
        public static void ShowExpressionTree(TreeContainer.TreeContainer treeCont, GPNode nodes)
        {
            //Collection holds tree nodes
            Queue<GPNode> dataTree = new Queue<GPNode>();
            Queue<TreeNode> ctrls = new Queue<TreeNode>();

            //current node
            GPNode node = null;
            TreeNode treeCtrl = null;
            treeCtrl = treeCont.AddRoot(InsertNode(nodes.value));

            ctrls.Enqueue(treeCtrl);
            dataTree.Enqueue(nodes);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Dequeue();
                treeCtrl = ctrls.Dequeue();

                if (node.children != null)
                    for (int i = 0; i < node.children.Length; i++)
                    {
                        var tn = treeCont.AddNode(InsertNode(node.children[i].value), treeCtrl);
                        dataTree.Enqueue(node.children[i]);
                        ctrls.Enqueue(tn);
                    }
            }

        }

        public static Button InsertNode(int value)
        {
            Button btn = new Button();
            btn.IsHitTestVisible = false;
            if ((value >= 2000 && value < 2050) || (value >= 1000 && value < 1050))
                btn.Background = Brushes.Red;
            else
                btn.Background = Brushes.Blue;
            btn.Content = value;
            return btn;
        }

        public static void SaveAsPNG(TreeContainer.TreeContainer control)
        {
            Visual theVisual = control; //Put the aimed visual here.

            //Get the size you wants from the UI
            double width = Convert.ToDouble(control.ActualWidth);
            double height = Convert.ToDouble(control.ActualHeight);

            if (double.IsNaN(width) || double.IsNaN(height))
            {
                throw new FormatException("You need to indicate the Width and Height values of the UIElement.");
            }
            Size size = new Size(width, height);

            DrawingVisual drawingVisual = new DrawingVisual();
            VisualBrush vBrush = new VisualBrush(theVisual);

            using (DrawingContext dc = drawingVisual.RenderOpen())
            {
                dc.DrawRectangle(vBrush, null, new Rect(new Point(), size));
            }

            RenderTargetBitmap render = new RenderTargetBitmap(
                  Convert.ToInt32(width),
                  Convert.ToInt32(height),
                  96,
                  96,
                  PixelFormats.Pbgra32);
            // Indicate which control to render in the image
            render.Render(drawingVisual);

            SaveFileDialog slv = new SaveFileDialog();
            slv.DefaultExt = "PNG";
            slv.FileName = DateTime.UtcNow.Ticks.ToString();
            slv.Filter = "PNG Images|*.png";
            slv.FilterIndex = 2;
            slv.Title = "Save an Image File";
            slv.RestoreDirectory = true;
            var ret = slv.ShowDialog();


            Stream oStream = new FileStream(slv.FileName, FileMode.Create);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(render));
            encoder.Save(oStream);
            oStream.Flush();
            oStream.Close();
        }
 */
    }
}
