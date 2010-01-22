using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TreeContainer;
using System.IO;
using GPdotNETLib;

namespace gpWpfTreeDrawerLib
{
    /// <summary>
    /// Interaction logic for wpfTreeDrawerCtrl.xaml
    /// </summary>
    public partial class wpfTreeDrawerCtrl : UserControl
    {
        private GPFunctionSet functionSet=null;
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
            treDrawer.FlowDirection = FlowDirection.RightToLeft;
        }

        public void DrawTreeExpression(GPTreeNode treeNode, GPFunctionSet funcSet)
       {
           functionSet = funcSet;
           treDrawer.Clear();
           if (treeNode == null)
               return;
          DrawComponentTree(treeNode, null);
       }
        public void DrawTreeExpressionIndex(GPTreeNode treeNode)
        {
            treDrawer.Clear();
            if (treeNode == null)
                return;
            DrawComponentTreeIndex(treeNode, null);
        }
        private void DrawComponentTreeIndex(GPdotNETLib.GPTreeNode treeNode, TreeNode tnControl)
       {
           TreeNode tnSubtreeRoot;
           Button btn = new Button();
           
           btn.IsHitTestVisible = false;
           btn.Background = Brushes.Transparent;
           btn.Content = treeNode.Value;
           
           	
           if (tnControl == null)
           {
               tnSubtreeRoot = treDrawer.AddRoot(btn);
           }
           else
           {
               tnSubtreeRoot = treDrawer.AddNode(btn, tnControl);
           }
           if (!treeNode.HasChildren)
               return;
           foreach (GPTreeNode child in treeNode.Nodes)
           {
               DrawComponentTreeIndex(child, tnSubtreeRoot);
           }
			
       }
       private void DrawComponentTree(GPdotNETLib.GPTreeNode treeNode, TreeNode tnControl)
       {
           TreeNode tnSubtreeRoot;
           Button btn = new Button();

           btn.IsHitTestVisible = false;
           btn.Background = Brushes.Transparent;
           btn.Content = treeNode.Value;

           if (treeNode.Value >= 1000 && treeNode.Value < 2000)
           {
               btn.FontWeight = FontWeights.Bold;
               btn.Content = functionSet.terminals[treeNode.Value - 1000].Name;
           }
           else//Ako je token funkcija tada ubacene argumente evaluiramo preko odredjene funkcije
           {
               btn.FontWeight = FontWeights.Medium;
               btn.Content = functionSet.functions[treeNode.Value - 2000].Name;
           }

           if (tnControl == null)
           {
               tnSubtreeRoot = treDrawer.AddRoot(btn);
           }
           else
           {
               tnSubtreeRoot = treDrawer.AddNode(btn, tnControl);
           }
           if (!treeNode.HasChildren)
               return;
           foreach (GPTreeNode child in treeNode.Nodes)
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
