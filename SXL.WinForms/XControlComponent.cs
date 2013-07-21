using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.WinForms
{
    public abstract class XControlComponent
    {
        protected GraphicsDevice Device;

        protected XControlComponent(GraphicsDevice device)
        {
            Device = device;
        }

        public abstract void LoadContent(ContentManager manager);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}