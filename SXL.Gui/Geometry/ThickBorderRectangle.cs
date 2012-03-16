using Microsoft.Xna.Framework.Graphics;

namespace SXL.Gui.Geometry
{
    public sealed class ThickBorderRectangle : DrawableRectangle
    {
        public ThickBorderRectangle(GuiSystem guiSystem) 
            : base(guiSystem)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            cornerBorderTexture = guiSystem.ContentManager.Load<Texture2D>(guiSystem.Theme.GeometryPath + "ThickCornerBorder");
            topBorderTexture = guiSystem.ContentManager.Load<Texture2D>(guiSystem.Theme.GeometryPath + "ThickTopBorder");
        }
    }
}