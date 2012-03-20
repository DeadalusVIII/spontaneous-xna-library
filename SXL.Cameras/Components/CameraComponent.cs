using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Cameras.Components
{
    public abstract class CameraComponent
    {
        protected internal Camera Camera { get; set; }

        public virtual void Initialize()
        {
            //does nothing here
        }

        public virtual void LoadContent(ContentManager contentManager)
        {
            //does nothing here   
        }

        public virtual void Update(GameTime gameTime)
        {
            //does nothing here   
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //does nothing here   
        }
    }
}
