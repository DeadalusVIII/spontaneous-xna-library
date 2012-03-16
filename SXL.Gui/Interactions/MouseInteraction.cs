using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SXL.Gui.Interactions
{
    public class MouseInteraction : GuiSystemObject
    {
        //necessary sprites to display the cursor status
        private Texture2D cursorSprite;
        
        //it is necessary to keep the current as well as the previous state of the mouse
        private MouseState oldState;
        private MouseState currentState;

        //also, keep a record of what the mouse is currently targeting

        public MouseInteraction(GuiSystem guiSystem)
            : base(guiSystem)
        {
            LoadContent(guiSystem.ContentManager);
            
            oldState = Mouse.GetState();
            currentState = Mouse.GetState();
        }

        private void LoadContent(ContentManager manager)
        {
            cursorSprite = manager.Load<Texture2D>(guiSystem.Theme.MouseCursorPath);
        }

        public void Update(GameTime gameTime)
        {
            oldState = currentState;
            currentState = Mouse.GetState();

            IsLeftClicked = currentState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed;
            ScrollWheelValue = (currentState.ScrollWheelValue - oldState.ScrollWheelValue) / 360f;
            IsLeftPressed = currentState.LeftButton == ButtonState.Pressed;
            IsJustLeftPressed = currentState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released;
            IsRightClicked = currentState.RightButton == ButtonState.Released && oldState.RightButton == ButtonState.Pressed;
            IsJustRightPressed = currentState.RightButton == ButtonState.Pressed && oldState.RightButton == ButtonState.Released;
            IsRightPressed = currentState.RightButton == ButtonState.Pressed;
            IsMiddlePressed = currentState.MiddleButton == ButtonState.Pressed;
            IsMiddleClicked = currentState.MiddleButton == ButtonState.Released && oldState.MiddleButton == ButtonState.Pressed;
            IsJustRightPressed = currentState.MiddleButton == ButtonState.Pressed && oldState.MiddleButton == ButtonState.Released;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draw the cursor
            spriteBatch.Draw(cursorSprite, Position, Color.White);

        }

        public Texture2D CursorSprite
        {
            get { return cursorSprite; }
            set { cursorSprite = value; }
        }

        public String CursorSpritePath
        {
            set { cursorSprite = guiSystem.ContentManager.Load<Texture2D>(value); }
        }

        public float ScrollWheelValue { get; set; }

        public Vector2 Position
        {
            get
            {
                Vector2 screenOffSet = new Vector2(guiSystem.GameWindowSize.Width / (float)guiSystem.GraphicsDevice.Viewport.Width, guiSystem.GameWindowSize.Height / (float)guiSystem.GraphicsDevice.Viewport.Height);
                return new Vector2(currentState.X * screenOffSet.X, currentState.Y * screenOffSet.Y);
            }
        }

        public Vector2 OffsetVector
        {
            get { return new Vector2(currentState.X - oldState.X, currentState.Y - oldState.Y); }
        }

        public bool IsLeftPressed { get; set;}

        public bool IsJustLeftPressed { get; set;}

        public bool IsLeftClicked { get; set; }

        public bool IsRightPressed { get; set;}

        public bool IsJustRightPressed { get; set;}

        public bool IsRightClicked { get; set;}

        public bool IsMiddlePressed { get; set;}

        public bool IsJustMiddlePressed { get; set;}

        public bool IsMiddleClicked { get; set;}

        public void ResetState()
        {
            oldState = new MouseState();
            currentState = new MouseState();
        }
    }
}