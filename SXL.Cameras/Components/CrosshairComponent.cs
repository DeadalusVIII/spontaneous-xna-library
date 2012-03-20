using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Cameras.Components
{
    public class CrosshairComponent : CameraComponent
    {
        private Vector2 crosshairPosition;
        private Texture2D crosshairTexture;

        public override void LoadContent(ContentManager contentManager)
        {
            crosshairTexture = contentManager.Load<Texture2D>("Cameras/Crosshairs/Crosshair01");

            crosshairPosition = new Vector2(Camera.Graphics.GraphicsDevice.Viewport.Width / 2.0f - crosshairTexture.Width / 2.0f,
                                            Camera.Graphics.GraphicsDevice.Viewport.Height / 2.0f - crosshairTexture.Height / 2.0f);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(crosshairTexture, crosshairPosition, Color.White);
        }
    }
}
