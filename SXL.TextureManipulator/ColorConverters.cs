using System.Drawing;
using XNAColor = Microsoft.Xna.Framework.Color;
using WPFColor = System.Windows.Media.Color;
using GDIColor = System.Drawing.Color;

namespace SXL.TextureManipulator
{
    internal static class ColorConverters
    {
        #region XNA and WPF

        internal static XNAColor ToXNAColor(this WPFColor wpfColor)
        {
            return new XNAColor(wpfColor.R, wpfColor.G, wpfColor.B, wpfColor.A);
        }

        internal static WPFColor ToWPFColor(this XNAColor xnaColor)
        {
            return new WPFColor { R = xnaColor.R, G = xnaColor.G, B = xnaColor.B, A = xnaColor.A };
        }

        #endregion

        #region XNA and GDI

        public static XNAColor ToXNAColor(this Color gdiColor)
        {
            return new XNAColor(gdiColor.R, gdiColor.G, gdiColor.B, gdiColor.A);
        }

        public static GDIColor ToGDIColor(this XNAColor xnaColor)
        {
            return GDIColor.FromArgb(xnaColor.A, xnaColor.R, xnaColor.G, xnaColor.B);
        }

        #endregion
    }
}
