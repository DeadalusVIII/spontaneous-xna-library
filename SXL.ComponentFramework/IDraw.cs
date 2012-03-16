using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.ComponentFramework
{
    interface IDraw
    {
        void Draw(GameTime gameTime, SpriteBatch spritebatch);
    }
}
