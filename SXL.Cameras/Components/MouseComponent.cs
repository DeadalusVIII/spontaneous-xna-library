using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SXL.Cameras.Components
{
    public class MouseComponent : CameraComponent
    {
        //from the main game system
        private MouseState previousMouseState;

        //for the mouse, this is also important
        private float mouseSensivity = 0.1f;

        //if the value is 1, the mouse is normal, if it is negative, it becomes inverted
        private float mouseYInverted = 1;

        public override void Initialize()
        {
            //centers the mouse into the window
            Mouse.SetPosition(Camera.Device.Viewport.Width / 2, Camera.Device.Viewport.Height / 2);

            //save the current state of the mouse and keyboard to check for changes later
            previousMouseState = Mouse.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            int mouseX = previousMouseState.X - mouseState.X;
            int mouseY = previousMouseState.Y - mouseState.Y;

            if (mouseX != 0 || mouseY != 0)
            {
                //get the vector indicating the direction left to the one we are looking at (normal)
                //get the vector indicating the direction we are looking at
                Vector3 direction = Camera.Direction;

                //get the vector indicating the direction left to the one we are looking at (normal)
                Vector3 cameraNormalDirection = Vector3.Cross(Camera.UpVector, direction);

                if (mouseX != 0)
                    Camera.Target += cameraNormalDirection * mouseX * mouseSensivity;

                if (mouseY != 0)
                {
                    Vector3 cameraTargetNormalDirectionUp = Vector3.Cross(direction, cameraNormalDirection);

                    Camera.Target += cameraTargetNormalDirectionUp * mouseY * mouseSensivity * mouseYInverted;
                }

                Camera.UpdateView();
                Mouse.SetPosition(Camera.Device.Viewport.Width / 2, Camera.Device.Viewport.Height / 2);
            }
        }


        public float MouseSensivity
        {
            get { return mouseSensivity; }
            set { mouseSensivity = value; }
        }

        public bool MouseYInverted
        {
            get { if (mouseYInverted < 0) return true; return false; }
            set { if (value) mouseYInverted = -1; else mouseYInverted = 1; }
        }
    }
}
