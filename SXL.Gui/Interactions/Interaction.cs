using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Gui.Interactions
{
    public class Interaction : GuiSystemObject
    {
        private readonly MouseInteraction mouse;
        private readonly KeyboardInteraction keyboard;

        internal Interaction(GuiSystem guiSystem) : base(guiSystem)
        {
            keyboard = new KeyboardInteraction(guiSystem);
            mouse = new MouseInteraction(guiSystem);
        }

        internal void Update(GameTime gameTime)
        {
            keyboard.Update(gameTime);
            mouse.Update(gameTime);
        }

        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            keyboard.Draw(gameTime, spriteBatch);
            mouse.Draw(gameTime, spriteBatch);
        }

        public MouseInteraction Mouse
        {
            get { return mouse; }
        }

        public KeyboardInteraction Keyboard
        {
            get { return keyboard; }
        }
    }
}