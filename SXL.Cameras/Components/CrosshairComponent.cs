using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Cameras.Components
{
    public class CrosshairComponent : CameraComponent
    {
        private readonly string _crosshairPath;
        private Vector2 _crosshairPosition;
        private Texture2D _crosshairTexture;

        public CrosshairComponent(string crosshairPath)
        {
            _crosshairPath = crosshairPath;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            _crosshairTexture = contentManager.Load<Texture2D>(_crosshairPath);

            _crosshairPosition = new Vector2(Camera.Device.Viewport.Width / 2.0f - _crosshairTexture.Width / 2.0f,
                                            Camera.Device.Viewport.Height / 2.0f - _crosshairTexture.Height / 2.0f);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_crosshairTexture, _crosshairPosition, Color.White);
        }
    }
}
