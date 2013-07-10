using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Cameras
{
    public class TargetCamera : Camera
    {
        private float _fieldOfViewDegrees = 60f;
        private float _nearPlaneDistance = 1f;
        private float _farPlaneDistance = 1000;

        public TargetCamera(GraphicsDevice device)
            : base(device)
        {
        }

        public override void Initialize()
        {
            viewMatrix = Matrix.CreateLookAt(position, target, UpVector);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60f), device.Viewport.AspectRatio, _nearPlaneDistance, _farPlaneDistance);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //sets up the view in case it was changed
            //if (bUpdateView)
                viewMatrix = Matrix.CreateLookAt(Position, target, UpVector);
        }

        public void UpdateFieldOfView()
        {
            //refreshes the field of view
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60f), device.Viewport.AspectRatio, _nearPlaneDistance, _farPlaneDistance);
        }

        #region Getters and Setters

        public float FieldOfViewDegrees
        {
            get { return _fieldOfViewDegrees; }
            set { _fieldOfViewDegrees = value; }
        }

        public float FarPlaneDistance
        {
            get { return _farPlaneDistance; }
            set { _farPlaneDistance = value; }
        }

        public float NearPlaneDistance
        {
            get { return _nearPlaneDistance; }
            set { _nearPlaneDistance = value; }
        }

        #endregion
    }
}
