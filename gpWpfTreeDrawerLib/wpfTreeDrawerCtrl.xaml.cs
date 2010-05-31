//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2010 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the GNU Library General Public License (LGPL)       //
// See licence section of  http://gpdotnet.codeplex.com/license                         //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GPNETLib;
using TreeContainer;

namespace gpWpfTreeDrawerLib
{
    /// <summary>
    /// Interaction logic for wpfTreeDrawerCtrl.xaml
    /// </summary>
    public partial class wpfTreeDrawerCtrl : UserControl
    {
        private GPNETLib.GPFunctionSet functionSet=null;
       // private Style defaultStyle;// = (Style)FindResource("MyTestStyle");
        public wpfTreeDrawerCtrl()
        {
            InitializeComponent();
           // defaultStyle = (Style)FindResource("ShadowStyle");
        }
        public void Clear()
        {
            if(treDrawer != null)
                treDrawer.Clear();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //treDrawer.FlowDirection = FlowDirection.RightToLeft;
        }
        public void DrawTreeExpression(FunctionTree treeNode, GPFunctionSet fun)
        {
            if (fun == null)
                return;
            functionSet = fun;
            DrawComponentTree(treeNode, null);
        }
        private void DrawComponentTree(FunctionTree treeNode, TreeNode tnControl)
		{
			TreeNode tnSubtreeRoot;
			Button btn = new Button();
            
            btn.IsHitTestVisible = false;
            btn.Background = Brushes.Transparent;

            if (treeNode.NodeValue >= 1000 && treeNode.NodeValue < 2000)
            {
                btn.FontWeight = FontWeights.Bold;
                btn.Content = functionSet.terminals[treeNode.NodeValue - 1000].Name;
            }
            else//Ako je token funkcija tada ubacene argumente evaluiramo preko odredjene funkcije
            {
                btn.FontWeight = FontWeights.Medium;
                btn.Content = functionSet.functions[treeNode.NodeValue - 2000].Name;
            }
        		
			if (tnControl == null)
			{
				tnSubtreeRoot = treDrawer.AddRoot(btn);
			}
			else
			{
				tnSubtreeRoot = treDrawer.AddNode(btn, tnControl);
			}
            if (treeNode.SubFunctionTree == null)
                return;
            foreach (FunctionTree child in treeNode.SubFunctionTree)
			{
                DrawComponentTree(child, tnSubtreeRoot);
			}
			
		}

       public void SaveAsBitmap(string fileName)
        {
            FileStream fs=null;
            try
            {
                RenderTargetBitmap targetBitmap =
                new RenderTargetBitmap((int)grid.ActualWidth,

                                       (int)grid.ActualHeight,

                                       96d, 96d,

                                       PixelFormats.Default);
                //reset image to white and add som copiright
                TextBlock lbl = new TextBlock();
                lbl.HorizontalAlignment = HorizontalAlignment.Left;
                lbl.VerticalAlignment = VerticalAlignment.Top;
                lbl.Text = "Generated with GPdotNET!";
                lbl.Foreground = Brushes.Black;
                lbl.FontFamily = new FontFamily("Arial");
                lbl.FontSize = 12;

                Rectangle vRect = new Rectangle();
                vRect.Width = (int)grid.ActualWidth;
                vRect.Height = (int)grid.ActualHeight;
                vRect.Fill = Brushes.White;
                vRect.Arrange(new Rect(0, 0, vRect.Width, vRect.Height));

                targetBitmap.Render(lbl);
                targetBitmap.Render(vRect);
                
                //Iscrtavanje TreeEpression
                targetBitmap.Render(grid);
                
                // add the RenderTargetBitmap to a Bitmapencoder
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(targetBitmap));
                // save file to disk
                fs = File.Open(fileName, FileMode.OpenOrCreate);
                encoder.Save(fs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }
 

        }
    }
}
