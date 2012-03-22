using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SXL.Gui;

namespace SXL.Viewer.Gui
{
    class GuiTest : GameComponent
    {
        private GuiSystem guiSystem;

        public GuiTest(Game game) 
            : base(game)
        {
            guiSystem = new GuiSystem(game);
        }

        public override void Update(GameTime gameTime)
        {
            guiSystem.Update(gameTime);
            guiSystem.UpdateInteraction(gameTime);
            guiSystem.UpdateWindows(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            guiSystem.Draw(gameTime, spriteBatch);
            guiSystem.DrawInteraction(gameTime, spriteBatch);
        }
    }
}
