using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SXL.Cameras.Components
{
    public class KeyboardComponent : CameraComponent
    {
        private float speed = 1;

        //depending on the mode, the accepted keys may vary
        private readonly Keys[] freeModeKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.C };

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (!IsAnyKeyDown(keyboardState, freeModeKeys))
                return;

            Vector3 direction = Camera.Direction;
            
            //get the vector indicating the direction left to the one we are looking at (normal)
            Vector3 cameraNormalDirection = Vector3.Cross(Camera.UpVector, direction);
            /*cameraNormalDirection.X = direction.Z;
            cameraNormalDirection.Z = -direction.X;
            cameraNormalDirection.Y = 0;*/

            //move the camera forward or backward
            if (keyboardState.IsKeyDown(Keys.W))
            {
                Camera.Position += direction * speed;
                Camera.Target += direction * speed;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                Camera.Position -= direction * speed;
                Camera.Target -= direction * speed;
            }

            //move the camera left or right
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Camera.Position += cameraNormalDirection * speed;
                Camera.Target += cameraNormalDirection * speed;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                Camera.Position -= cameraNormalDirection * speed;
                Camera.Target -= cameraNormalDirection * speed;
            }

            //move the camera up or down
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                Camera.Position += Camera.UpVector * speed;
                Camera.Target += Camera.UpVector * speed;
            }
            else if (keyboardState.IsKeyDown(Keys.C))
            {
                Camera.Position -= Camera.UpVector * speed;
                Camera.Target -= Camera.UpVector * speed;
            }

            Camera.UpdateView();
        }

        private static bool IsAnyKeyDown(KeyboardState keyboardState, IEnumerable<Keys> keys)
        {
            foreach (Keys key in keys)
            {
                if (keyboardState.IsKeyDown(key))
                    return true;
            }

            return false;
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
    }
}
