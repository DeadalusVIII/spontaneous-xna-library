using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Debug
{
    public class FrameRateDisplay : GameComponent
    {
        private readonly Game _game;
        private readonly string _font;
        private SpriteFont _spriteFont;

        private int _frameRate = 0;
        private int _frameCounter = 0;
        private TimeSpan _elapsedTime = TimeSpan.Zero;


        public FrameRateDisplay(Game game, String font) : base(game)
        {
            _font = font;
        }

        public override void Initialize()
        {
            _spriteFont = Game.Content.Load<SpriteFont>(_font);
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


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _frameCounter++;

            String fps = String.Format("Fps: {0}", _frameRate);

            spriteBatch.DrawString(_spriteFont, fps, new Vector2(Game.GraphicsDevice.Viewport.Width - 80, 10), Color.Black);
            spriteBatch.DrawString(_spriteFont, fps, new Vector2(Game.GraphicsDevice.Viewport.Width - 81, 9), Color.White);
        }
    }
}
