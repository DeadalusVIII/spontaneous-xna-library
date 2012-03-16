using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SXL.Gui.Geometry;
using SXL.Gui.Interactions;
using SXL.Gui.Windows.Controls;

namespace SXL.Gui.Windows
{
    public enum WindowState
    {
        Opening,
        Open,
        Closing,
        Closed
    }

    public class GuiWindow : GuiSystemObject
    {
        //background rectangle that will be drawn
        protected DrawableRectangle guiBackground;
        protected Rectangle bounds;

        public WindowState State { get; set; }
        
        protected List<GuiControl> controls;

        public GuiWindow(GuiSystem guiSystem) 
            : base(guiSystem)
        {
            guiBackground = new ThickBorderRectangle(guiSystem);
            guiBackground.LoadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            guiBackground.Draw(gameTime, spriteBatch, bounds, guiSystem.Theme.BoundaryColor, guiSystem.Theme.InteriorColor);
        }

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        public void OnGameWindowSizeChange(Rectangle newGameWindowSize)
        {
            throw new System.NotImplementedException();
        }
    }
}
