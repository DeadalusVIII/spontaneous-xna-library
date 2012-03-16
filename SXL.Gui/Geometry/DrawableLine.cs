using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Gui.Geometry
{
    public class DrawableLine : GuiSystemObject
    {
        protected Texture2D lineTexture;
        
        public DrawableLine(GuiSystem guiSystem) 
            : base(guiSystem)
        {
            LoadContent();
        }

        private void LoadContent()
        {
            lineTexture = guiSystem.ContentManager.Load<Texture2D>(guiSystem.Theme.GeometryPath + "ThinLine");
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Vector2 size,
                                 float rotation, Color color)
        {
            spriteBatch.Draw(lineTexture, new Rectangle((int) position.X, (int) position.Y, (int) size.X, (int) size.Y),
                             new Rectangle(0, 0, lineTexture.Width, lineTexture.Height), color, rotation, Vector2.Zero,
                             SpriteEffects.None, 0);
        }
    }
}