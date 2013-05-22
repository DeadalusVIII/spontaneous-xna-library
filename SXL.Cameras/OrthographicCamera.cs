using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Cameras
{
    public class OrthographicCamera : Camera
    {
        private float _width;
        private float _height;
        private float _nearPlaneDistance = 1f;
        private float _farPlaneDistance = 1000;

        public OrthographicCamera(GraphicsDevice device)
            : base(device)
        {
            _width = device.Viewport.Width;
            _height = device.Viewport.Height;
        }

        public override void Initialize()
        {
            projectionMatrix = Matrix.CreateOrthographic(_width, _height, _nearPlaneDistance, _farPlaneDistance);
            viewMatrix = Matrix.CreateLookAt(position, target, UpVector);
        }

        public override void Update(GameTime gameTime)
        {
            //sets up the view in case it was changed
            //if (bUpdateView)
            viewMatrix = Matrix.CreateLookAt(Position, target, UpVector);
        }

        #region Getters and Setters

        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public float Width
        {
            get { return _width; }
            set { _width = value; }
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
