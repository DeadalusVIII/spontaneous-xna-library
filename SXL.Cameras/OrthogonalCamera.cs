using Microsoft.Xna.Framework;

namespace SXL.Cameras
{
    class OrthogonalCamera : Camera
    {
        public OrthogonalCamera(GraphicsDeviceManager newGraphics) 
            : base(newGraphics)
        {
        }

        public override void Initialize()
        {
            projectionMatrix = Matrix.CreateOrthographic(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height, 0.1f, 10000);
            viewMatrix = Matrix.CreateLookAt(position, target, UpVector);
        }

        public override void Update(GameTime gameTime)
        {
            //sets up the view in case it was changed
            //if (bUpdateView)
            viewMatrix = Matrix.CreateLookAt(Position, target, UpVector);
        }
    }
}
