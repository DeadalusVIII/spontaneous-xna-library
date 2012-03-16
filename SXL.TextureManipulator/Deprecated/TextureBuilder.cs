using System;
using System.Drawing.Drawing2D;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using XnaColor = Microsoft.Xna.Framework.Color;
using GDIColor = System.Drawing.Color;
using GDIMatrix = System.Drawing.Drawing2D.Matrix;
using GDIPoint = System.Drawing.Point;
using GDIRectangle = System.Drawing.Rectangle;


namespace SXL.TextureManipulator
{
    public class TextureBuilder
    {
        private readonly GraphicsDevice graphicsDevice;

        private readonly XnaColor[] texturePixels;
        private readonly int height;
        private readonly int width;

        public TextureBuilder(Texture2D texture2D)
        {
            //this.texture2D = texture2D;
            graphicsDevice = texture2D.GraphicsDevice;

            //reads the image properties
            width = texture2D.Width;
            height = texture2D.Height;

            //reads the image data (which is a one-dimensional array)
            texturePixels = new XnaColor[width * height];
            texture2D.GetData(texturePixels);

            //Graphics g = Graphics.FromImage(bitmap);

            //ands sets the data on a two-dimensional array (for quicker access)
            //texturePixels = new Color[width, height];
            //for (int i = 0; i < temp.Length; i++)
                //texturePixels[i % width, i / width] = temp[i];
        }

        public TextureBuilder(GraphicsDevice graphicsDevice, int height, int width)
        {
            //this.texture2D = texture2D;
            this.graphicsDevice = graphicsDevice;

            //reads the image properties
            this.width = width;
            this.height = height;

            //reads the image data (which is a one-dimensional array)
            texturePixels = new XnaColor[width * height];
        }

        public TextureBuilder(GraphicsDevice graphicsDevice, Bitmap bitmap)
        {
            //this.texture2D = texture2D;
            this.graphicsDevice = graphicsDevice;

            //reads the image properties
            width = bitmap.Width;
            height = bitmap.Height;

            //reads the image data (which is a one-dimensional array)
            texturePixels = new XnaColor[width * height];

            //copies the bitmap into the texturebuilder
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GDIColor color = bitmap.GetPixel(i, j);
                    this[i, j] = new XnaColor(color.R,color.G,color.B,color.A);
                }
            }
        }

        private Bitmap CopyArrayToBitmap()
        {
            Bitmap bitmap = new Bitmap(width,height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    XnaColor color = this[i, j];
                    bitmap.SetPixel(i, j, GDIColor.FromArgb(color.A, color.R, color.G, color.B));
                }
            }

            return bitmap;
        }


        public XnaColor this[int x, int y]
        {
            get { return texturePixels[x + width*y]; }
            set { texturePixels[x + width * y] = value; }
        }

        public TextureBuilder Rotate(float angle)
        {
            Bitmap bitmap = CopyArrayToBitmap();
            bitmap = RotateImage2(bitmap, angle);
            return new TextureBuilder(graphicsDevice,bitmap);
            
            //g.Save();
            //bitmap.Save(@"D:\Downloads\Newbitmap.png");
        }

        private static Bitmap CropImage(Bitmap bitmap, GDIRectangle cropArea)
        {
            return bitmap.Clone(cropArea, bitmap.PixelFormat);
        }

        private static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            return ResizeImagePercentage(imgToResize, (float)size.Width / imgToResize.Width, (float)size.Height / imgToResize.Height);
        }

        private static Bitmap ResizeImagePercentage(Bitmap imgToResize, float nPercentW, float nPercentH)
        {
            int destWidth = (int)(imgToResize.Width * nPercentW);
            int destHeight = (int)(imgToResize.Height * nPercentH);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }

        // Rotates the input image by theta degrees around center.
        public static Bitmap RotateImage(Bitmap bmpSrc, float theta)
        {
            GDIMatrix mRotate = new GDIMatrix();
            mRotate.Translate(bmpSrc.Width / -2, bmpSrc.Height / -2, MatrixOrder.Append);
            mRotate.RotateAt(theta, new System.Drawing.Point(0, 0), MatrixOrder.Append);
            using (GraphicsPath gp = new GraphicsPath())
            {  // transform image points by rotation matrix
                gp.AddPolygon(new System.Drawing.Point[] { new System.Drawing.Point(0, 0), new System.Drawing.Point(bmpSrc.Width, 0), new System.Drawing.Point(0, bmpSrc.Height) });
                gp.Transform(mRotate);
                System.Drawing.PointF[] pts = gp.PathPoints;

                // create destination bitmap sized to contain rotated source image
                GDIRectangle bbox = BoundingBox(bmpSrc, mRotate);
                Bitmap bmpDest = new Bitmap(bbox.Width, bbox.Height);

                using (Graphics gDest = Graphics.FromImage(bmpDest))
                {  // draw source into dest
                    GDIMatrix mDest = new GDIMatrix();
                    mDest.Translate(bmpDest.Width / 2, bmpDest.Height / 2, MatrixOrder.Append);
                    gDest.Transform = mDest;
                    gDest.DrawImage(bmpSrc, pts);
                    return bmpDest;
                }
            }
        }

        private static GDIRectangle BoundingBox(Image img, GDIMatrix matrix)
        {
            GraphicsUnit gu = new GraphicsUnit();
            GDIRectangle rImg = GDIRectangle.Round(img.GetBounds(ref gu));

            // Transform the four points of the image, to get the resized bounding box.
            System.Drawing.Point topLeft = new System.Drawing.Point(rImg.Left, rImg.Top);
            System.Drawing.Point topRight = new System.Drawing.Point(rImg.Right, rImg.Top);
            System.Drawing.Point bottomRight = new System.Drawing.Point(rImg.Right, rImg.Bottom);
            System.Drawing.Point bottomLeft = new System.Drawing.Point(rImg.Left, rImg.Bottom);
            System.Drawing.Point[] points = new System.Drawing.Point[] { topLeft, topRight, bottomRight, bottomLeft };
            GraphicsPath gp = new GraphicsPath(points,new byte[] { (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line });
            gp.Transform(matrix);
            return GDIRectangle.Round(gp.GetBounds());
        }


        private Bitmap RotateImage2(Bitmap b, float angle)
        {
            int newHeight = (int)Math.Abs(b.Width * Math.Sin(DegreesToRadians(angle)) + b.Height * Math.Cos(DegreesToRadians(angle)));
            int newWidth = (int)Math.Abs(b.Height * Math.Sin(DegreesToRadians(angle)) + b.Width * Math.Cos(DegreesToRadians(angle)));

            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(1000, 1000);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);
            //move rotation point to center of image
            Console.WriteLine(g.VisibleClipBounds);
            //g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            //rotate
            g.RotateTransform(angle);
            //move image back
            //g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            g.TranslateTransform(0, -50);

            //g.TranslateTransform(newWidth, newHeight);
            
            Console.WriteLine(g.VisibleClipBounds);

            /*RectangleF rectangleF = g.VisibleClipBounds;
            g.SetClip(rectangleF);*/

            
            //draw passed in image onto graphics object
            g.DrawImage(b, new GDIPoint(0, 0));
            return returnBitmap;
        }



        public static double DegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        /*
        private void MultiplyMatrix(Vector2 point, Matrix matrix)
        {
            Matrix pointMatrix = new Matrix(point.X, point.Y);
        }


        private void ReadData()
        {
            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
        }

        private TextureBuilder Add(Texture2D texture)
        {
            return Add(new TextureBuilder(texture));
        }

        private TextureBuilder Add(TextureBuilder builder)
        {
            
        }*/

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

        public XnaColor[] TexturePixels
        {
            get { return texturePixels; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
        }

        public Texture2D Texture2D
        {
            get
            {
                Texture2D texture2D = new Texture2D(graphicsDevice,width,height);
                texture2D.SetData(texturePixels);
                return texture2D;
            }
        }

        
    }
}
