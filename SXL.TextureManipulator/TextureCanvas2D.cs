using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XnaColor = Microsoft.Xna.Framework.Color;
using WPFColor = System.Windows.Media.Color;

namespace SXL.TextureManipulator
{
    public class TextureCanvas2D
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly int _width;
        private readonly int _height;

        private readonly DrawingVisual _drawingVisual;
        private readonly DrawingContext _drawingContext;

        private bool _isClosed;

        public TextureCanvas2D(GraphicsDevice graphicsDevice, int width, int height)
        {
            _graphicsDevice = graphicsDevice;
            _width = width;
            _height = height;

            _drawingVisual = new DrawingVisual();
            
            _drawingContext = _drawingVisual.RenderOpen();
            DrawRoundedRectangle(new Rectangle(0, 0, width, height), XnaColor.Transparent, XnaColor.Transparent, 0, 0, 0);
        }

        public void DrawRoundedRectangle(Rectangle rectangle, XnaColor fillColor, XnaColor borderColor, int borderThickness, double radiusX, double radiusY)
        {
            if (_isClosed)
                throw new Exception("Canvas has already been processed and closed.");

            _drawingContext.DrawRoundedRectangle(new SolidColorBrush(fillColor.ToWPFColor()), new Pen(new SolidColorBrush(borderColor.ToWPFColor()), borderThickness), new Rect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), radiusX, radiusY);    
        }
        
        
        /// <summary>
        /// Transforms the Vector-based drawings into a pixel-based texture2D format.
        /// </summary>
        /// <returns></returns>
        public Texture2D CloseAndProcessToTexture2D()
        {   
            //close the drawing context
            _drawingContext.Close();
            _isClosed = true;


            RenderTargetBitmap bmp = new RenderTargetBitmap(_width, _height, 96, 96, PixelFormats.Pbgra32);
            //RenderOptions.SetEdgeMode(_drawingVisual, EdgeMode.Aliased);
            
            bmp.Render(_drawingVisual);
            Clipboard.SetImage(bmp);

            return ConvertToBitmap(bmp).ToTexture2D(_graphicsDevice);
        }

        private static System.Drawing.Bitmap ConvertToBitmap(BitmapSource target)
        {
            System.Drawing.Bitmap bitmap;

            using (MemoryStream outStream = new MemoryStream())
            {
                PngBitmapEncoder enc = new PngBitmapEncoder();
                //enc.Interlace = PngInterlaceOption.Off;
                //enc.Palette = BitmapPalettes.Halftone256;
                enc.Interlace = PngInterlaceOption.Off;
                enc.Frames.Add(BitmapFrame.Create(target));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
            }

            return bitmap;
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public bool IsClosed
        {
            get { return _isClosed; }
        }
    }
}
