using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Gui.Windows
{
    public class GuiWindowManager : GuiSystemObject
    {
        private readonly List<GuiWindow> guiWindows;
        private readonly List<GuiWindow> windowsToAdd;

        public GuiWindowManager(GuiSystem guiSystem) 
            : base(guiSystem)
        {
            guiWindows = new List<GuiWindow>();
            windowsToAdd = new List<GuiWindow>();

            
        }

        public virtual void Update(GameTime gameTime)
        {
            //draws all the windows that are opening, opened or closing
            foreach (GuiWindow window in guiWindows)
                window.Update(gameTime);

            //in the previous step, some windows might have been opened
            //but could not be added since we were 'foreach looping' the list
            guiWindows.AddRange(windowsToAdd);
            windowsToAdd.Clear();

            //okay, now remove all windows whose status is "closed"
            guiWindows.RemoveAll(IsClosed);

            /*if (HasOpenedWindows())
            {
                //interaction.Mouse..Deselect();
            }*/
        }

        private static bool IsClosed(GuiWindow window)
        {
            return window.State == WindowState.Closed;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draws all the windows that are opening, opened or closing
            foreach (GuiWindow window in guiWindows)
                window.Draw(gameTime, spriteBatch);
        }

        public void OnGameWindowSizeChange(Rectangle newGameWindowSize)
        {
            foreach (GuiWindow guiWindow in guiWindows)
            {
                guiWindow.OnGameWindowSizeChange(newGameWindowSize);
            }
        }



        /*public void OpenWindow(GuiWindow window)
        {
            //foreach (FormWindow animatedWindow in windows)
            //    animatedWindow.State = WindowState.Closing;

            window.FormManager = this;
            window.Caller = null;
            window.State = WindowState.Opening;
            window.AnimationDirection = WindowAnimationDirection.Left;

            windowsToAdd.Add(window);
            window.AjustPositionToScreen();
        }

        public void SwitchFromTo(FormWindow oldwindow, FormWindow newWindow)
        {
            newWindow.Caller = oldwindow;
            oldwindow.State = WindowState.Closing;
            windowsToAdd.Add(newWindow);

            oldwindow.AnimationDirection = WindowAnimationDirection.Left;
            newWindow.AnimationDirection = WindowAnimationDirection.Left;

            oldwindow.AjustPositionToScreen();
            newWindow.AjustPositionToScreen();

            newWindow.FormManager = this;
        }

        public void SwitchBackToCaller(FormWindow window)
        {
            if (window.Caller != null)
            {
                window.Caller.State = WindowState.Opening;
                windowsToAdd.Add(window.Caller);
                window.Caller.AjustPositionToScreen();
                window.Caller.AnimationDirection = WindowAnimationDirection.Right;
            }

            window.AnimationDirection = WindowAnimationDirection.Right;

            window.State = WindowState.Closing;
            window.AjustPositionToScreen();
        }

        public void CloseAllWindows()
        {
            foreach (FormWindow animatedWindow in guiWindows)
            {
                animatedWindow.AnimationDirection = WindowAnimationDirection.Right;
                animatedWindow.State = WindowState.Closing;
            }
        }


        public bool AllWindowsAllowToolTipBox()
        {
            foreach (FormWindow window in guiWindows)
            {
                if (!window.AllowTootipBox)
                    return false;
            }

            return true;
        }

        public bool HasOpenedWindows()
        {
            foreach (FormWindow window in guiWindows)
            {
                if (window.State == WindowState.Open || window.State == WindowState.Opening)
                    return true;
            }

            return false;
        }*/
    }
}
