using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.WinForms.Components
{
    public class FrameRateDisplay : XControlComponent
    {
        private readonly string _font;
        private SpriteFont _spriteFont;

        private int _frameRate = 0;
        private int _frameCounter = 0;
        private TimeSpan _elapsedTime = TimeSpan.Zero;


        public FrameRateDisplay(GraphicsDevice device, String font)
            : base(device)
        {
            _font = font;
        }

        public override void LoadContent(ContentManager manager)
        {
            _spriteFont = manager.Load<SpriteFont>(_font);
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime;

            if (_elapsedTime > TimeSpan.FromSeconds(1))
            {
                _elapsedTime -= TimeSpan.FromSeconds(1);
                _frameRate = _frameCounter;
                _frameCounter = 0;
            }
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _frameCounter++;

            String fps = String.Format("Fps: {0}", _frameRate);

            spriteBatch.DrawString(_spriteFont, fps, new Vector2(Device.Viewport.Width - 80, 10), Color.Black);
            spriteBatch.DrawString(_spriteFont, fps, new Vector2(Device.Viewport.Width - 81, 9), Color.White);
        }
    }
}
