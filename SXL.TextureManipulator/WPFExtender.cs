using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Graphics;
using Color = System.Windows.Media.Color;
using Pen = System.Windows.Media.Pen;

namespace SXL.TextureManipulator
{
    public static class WPFExtender
    {
        public static Texture2D Do(this Texture2D texture2D)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            //drawingContext.DrawRectangle(new SolidColorBrush(Colors.Yellow), new Pen(), new Rect(0, 0, texture2D.Width, texture2D.Height));
            drawingContext.DrawRoundedRectangle(new SolidColorBrush(Colors.Red), new Pen(new SolidColorBrush(Colors.Blue), 3), new Rect(50, 50, 200, 300), 10, 10);
            drawingContext.Close();
            
            RenderTargetBitmap bmp = new RenderTargetBitmap(texture2D.Width, texture2D.Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            Clipboard.SetImage(bmp);

            return null;
        }
    }
}
