using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SXL.Gui.Interactions
{
    public class KeyboardInteraction : GuiSystemObject
    {
        private KeyboardState oldState;
        private KeyboardState currentState;
        private readonly KeyboardBuffer keyboardBuffer;
        private String savedText = "";

        private List<Keys> oldKeyList;
        private List<Keys> currentKeyList;

        public KeyboardInteraction(GuiSystem guiSystem)
            : base(guiSystem)
        {
            keyboardBuffer = new KeyboardBuffer(guiSystem.Game.Window.Handle) { Enabled = true, TranslateMessage = true };

            oldState = Keyboard.GetState();
            currentState = Keyboard.GetState();
        }
        
        public void Update(GameTime gameTime)
        {
            oldState = currentState;
            currentState = Keyboard.GetState();

            oldKeyList = currentKeyList;
            currentKeyList = new List<Keys>(currentState.GetPressedKeys());
            
            savedText = keyboardBuffer.GetText();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //nothing done here for now
        }

        public bool IsKeyPressed(Keys key)
        {
            //return currentKeyList.Contains(key);
            return currentState.IsKeyDown(key);
        }

        public bool CheckKey(Keys key)
        {
            return currentKeyList.Remove(key);
            //return currentState.IsKeyDown(key);
        }

        public bool IsKeyClicked(Keys key)
        {
            //return currentState.IsKeyDown(key) && !oldState.IsKeyDown(key);

            //return currentKeyList.Contains(key) && !oldKeyList.Contains(key);
            return currentState.IsKeyDown(key) && !oldState.IsKeyDown(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            //return !currentKeyList.Contains(key) && oldKeyList.Contains(key);
            return currentState.IsKeyDown(key) && !oldState.IsKeyDown(key);
        }

        public Keys[] GetPressedKeys()
        {
            return currentState.GetPressedKeys();
        }

        public Keys[] GetClickedKeys()
        {
            Keys[] keys = currentState.GetPressedKeys();
            List<Keys> keyList = new List<Keys>();
            
            foreach (Keys key in keys)
            {
                if(oldState.IsKeyUp(key))
                    keyList.Add(key);
            }

            return keyList.ToArray();
        }

        public string Text
        {
            get
            {
                //string text = keyboardBuffer.GetText();
                //keyboardBuffer.Text.Length = 0;
                return savedText; // keyboardBuffer.Text.ToString();
            }
        }

        public void ResetState()
        {
            currentState = new KeyboardState();
            oldState = new KeyboardState();
        }

        public List<Keys> OldKeyList
        {
            get { return oldKeyList; }
            set { oldKeyList = value; }
        }

        public List<Keys> CurrentKeyList
        {
            get { return currentKeyList; }
            set { currentKeyList = value; }
        }
    }
}