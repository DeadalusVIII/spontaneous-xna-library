using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Gui.Geometry
{
    public abstract class DrawableRectangle : GuiSystemObject
    {
        protected Texture2D centerTexture;
        protected Texture2D cornerBorderTexture;
        protected Texture2D cornerTexture;
        protected Texture2D topBorderTexture;


        protected DrawableRectangle(GuiSystem guiSystem) 
            : base(guiSystem)
        {
        }

        public virtual void LoadContent()
        {
            cornerTexture = guiSystem.ContentManager.Load<Texture2D>(guiSystem.Theme.GeometryPath + "CornerInside");
            centerTexture = guiSystem.ContentManager.Load<Texture2D>(guiSystem.Theme.GeometryPath + "Center");
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle bounds, Color borderColor, Color innerColor)
        {
            if (bounds.Height >= cornerTexture.Height * 2 && bounds.Width >= cornerTexture.Width * 2)
            {
                //draw the corners first
                spriteBatch.Draw(cornerTexture, new Vector2(bounds.X, bounds.Y), innerColor);
                spriteBatch.Draw(cornerTexture, new Vector2(bounds.X + bounds.Width, bounds.Y),
                                 new Rectangle(0, 0, cornerTexture.Width, cornerTexture.Height), innerColor,
                                 MathHelper.PiOver2, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(cornerTexture, new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height),
                                 new Rectangle(0, 0, cornerTexture.Width, cornerTexture.Height), innerColor,
                                 MathHelper.Pi, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(cornerTexture, new Vector2(bounds.X, bounds.Y + bounds.Height),
                                 new Rectangle(0, 0, cornerTexture.Width, cornerTexture.Height), innerColor,
                                 -MathHelper.PiOver2, Vector2.Zero, 1, SpriteEffects.None, 0);

                //now the corner borders
                spriteBatch.Draw(cornerBorderTexture, new Vector2(bounds.X, bounds.Y), borderColor);
                spriteBatch.Draw(cornerBorderTexture, new Vector2(bounds.X + bounds.Width, bounds.Y),
                                 new Rectangle(0, 0, cornerTexture.Width, cornerTexture.Height), borderColor,
                                 MathHelper.PiOver2, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(cornerBorderTexture, new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height),
                                 new Rectangle(0, 0, cornerTexture.Width, cornerTexture.Height), borderColor,
                                 MathHelper.Pi, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(cornerBorderTexture, new Vector2(bounds.X, bounds.Y + bounds.Height),
                                 new Rectangle(0, 0, cornerTexture.Width, cornerTexture.Height), borderColor,
                                 -MathHelper.PiOver2, Vector2.Zero, 1, SpriteEffects.None, 0);

                //now the sides
                spriteBatch.Draw(centerTexture,
                                 new Rectangle(bounds.X + cornerTexture.Width, bounds.Y,
                                               bounds.Width - cornerTexture.Width*2, topBorderTexture.Height),
                                 innerColor);
                spriteBatch.Draw(centerTexture,
                                 new Rectangle(bounds.X + bounds.Width, bounds.Y + cornerTexture.Height,
                                               bounds.Height - 2*cornerTexture.Height, topBorderTexture.Height),
                                 new Rectangle(0, 0, topBorderTexture.Width, topBorderTexture.Height), innerColor,
                                 MathHelper.PiOver2, Vector2.Zero, SpriteEffects.None, 0);
                spriteBatch.Draw(centerTexture,
                                 new Rectangle(bounds.X + bounds.Width - topBorderTexture.Width,
                                               bounds.Y + bounds.Height, bounds.Width - cornerTexture.Width * 2,
                                               topBorderTexture.Height),
                                 new Rectangle(0, 0, topBorderTexture.Width, topBorderTexture.Height), innerColor,
                                 MathHelper.Pi, Vector2.Zero, SpriteEffects.None, 0);
                spriteBatch.Draw(centerTexture,
                                 new Rectangle(bounds.X, bounds.Y + bounds.Height - topBorderTexture.Height,
                                               bounds.Height - cornerTexture.Height*2, topBorderTexture.Width),
                                 new Rectangle(0, 0, topBorderTexture.Width, topBorderTexture.Height), innerColor,
                                 -MathHelper.PiOver2, Vector2.Zero, SpriteEffects.None, 0);

                //now the side borders
                spriteBatch.Draw(topBorderTexture,
                                 new Rectangle(bounds.X + cornerTexture.Width, bounds.Y,
                                               bounds.Width - cornerTexture.Width*2, topBorderTexture.Height),
                                 borderColor);
                spriteBatch.Draw(topBorderTexture,
                                 new Rectangle(bounds.X + bounds.Width,bounds.Y + cornerTexture.Height,
                                               bounds.Height - 2*cornerTexture.Height, topBorderTexture.Height),
                                 new Rectangle(0, 0, topBorderTexture.Width, topBorderTexture.Height), borderColor,
                                 MathHelper.PiOver2, Vector2.Zero, SpriteEffects.None, 0);
                spriteBatch.Draw(topBorderTexture,
                                 new Rectangle(bounds.X + bounds.Width - topBorderTexture.Width,
                                               bounds.Y + bounds.Height, bounds.Width - cornerTexture.Width * 2,
                                               topBorderTexture.Height),
                                 new Rectangle(0, 0, topBorderTexture.Width, topBorderTexture.Height), borderColor,
                                 MathHelper.Pi, Vector2.Zero, SpriteEffects.None, 0);
                spriteBatch.Draw(topBorderTexture,
                                 new Rectangle(bounds.X, bounds.Y + bounds.Height - topBorderTexture.Height,
                                               bounds.Height - cornerTexture.Height*2, topBorderTexture.Width),
                                 new Rectangle(0, 0, topBorderTexture.Width, topBorderTexture.Height), borderColor,
                                 -MathHelper.PiOver2, Vector2.Zero, SpriteEffects.None, 0);

                //now the center
                spriteBatch.Draw(centerTexture,
                                 new Rectangle(bounds.X + cornerTexture.Width,
                                               bounds.Y + cornerTexture.Width,
                                               bounds.Width - cornerTexture.Width*2,
                                               bounds.Height - cornerTexture.Height*2), innerColor);
            }
        }


        public virtual void DrawWhiteBorder(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Vector2 size,
                                            Color color)
        {
            //Draw(gameTime, spriteBatch, position, size, new Color(255, 255, 255, color.A), color);
        }
    }
}