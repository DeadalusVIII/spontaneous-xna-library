using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SXL.Cameras.Components;

namespace SXL.Cameras
{
    public abstract class Camera
    {
        //from the main game system
        protected readonly GraphicsDeviceManager graphics;

        //standard camera fields
        protected Matrix projectionMatrix;
        protected Matrix viewMatrix;

        //Major properties, like the position and the target
        protected Vector3 upVector = Vector3.UnitY;
        protected Vector3 position = new Vector3(1,1,1);
        protected Vector3 target = Vector3.Zero;

        //to manage the plugins
        protected readonly Dictionary<String, CameraComponent> cameraComponents = new Dictionary<String, CameraComponent>();

        //reduce the calculation of the view matrix to a minimum, to improve performance
        //protected bool bUpdateView;
        

        protected Camera(GraphicsDeviceManager newGraphics)
        {
            graphics = newGraphics;
        }

        #region Component Management

        public void AddComponent(CameraComponent component)
        {
            cameraComponents.Add(component.GetType().Name, component);
            component.Camera = this;

            component.Initialize();
        }

        public T GetComponent<T>()
        {
            return (T)(Object)cameraComponents[typeof(T).Name];
        }

        #endregion


        public virtual void Initialize()
        {
            foreach (CameraComponent cameraComponent in cameraComponents.Values)
                cameraComponent.Initialize();
        }

        public virtual void LoadContent(ContentManager contentManager)
        {
            foreach (CameraComponent cameraComponent in cameraComponents.Values)
                cameraComponent.LoadContent(contentManager);
        }

        public virtual void Update(GameTime gameTime)
        {
            //as a standard procedure, do not recalculate the view
            //but the components may require it to be recalculated
            //bUpdateView = false;

            foreach (CameraComponent cameraComponent in cameraComponents.Values)
                cameraComponent.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (CameraComponent cameraComponent in cameraComponents.Values)
            {
                cameraComponent.Draw(gameTime,spriteBatch);
            }
        }

        public virtual void UpdateView()
        {
            //bUpdateView = true;
        }
        

        #region Getters and Setters

        internal GraphicsDeviceManager Graphics
        {
            get { return graphics; }
        }

        public Matrix ProjectionMatrix
        {
            get { return projectionMatrix; }
            set { projectionMatrix = value; }
        }

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
            set { viewMatrix = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector3 Target
        {
            get { return target; }
            set { target = value; }
        }

        public Vector3 UpVector
        {
            get { return upVector; }
            set { upVector = value; }
        }

        public Vector3 Direction
        {
            get { return Vector3.Normalize(target - position);}
        }

        #endregion
    }
}