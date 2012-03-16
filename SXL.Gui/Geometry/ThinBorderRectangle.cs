using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Gui.Geometry
{
    public sealed class ThinBorderRectangle : DrawableRectangle
    {
        public ThinBorderRectangle(GuiSystem guiSystem) 
            : base(guiSystem)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            cornerBorderTexture = guiSystem.ContentManager.Load<Texture2D>(guiSystem.Theme.GeometryPath + "ThinCornerBorder");
            topBorderTexture = guiSystem.ContentManager.Load<Texture2D>(guiSystem.Theme.GeometryPath + "ThinTopBorder");
        }
    }
}