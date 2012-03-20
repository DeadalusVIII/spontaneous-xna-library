using Microsoft.Xna.Framework;

namespace SXL.Cameras
{
    public class TargetCamera : Camera
    {
        public TargetCamera(GraphicsDeviceManager graphics)
            : base(graphics)
        {
        }

        public override void Initialize()
        {
            viewMatrix = Matrix.CreateLookAt(position, target, UpVector);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60f), graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000000);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //sets up the view in case it was changed
            //if (bUpdateView)
                viewMatrix = Matrix.CreateLookAt(Position, target, UpVector);
        }
    }
}
