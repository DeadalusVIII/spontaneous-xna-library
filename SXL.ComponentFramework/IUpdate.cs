using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SXL.ComponentFramework
{
    public interface IUpdate
    {
        void Update(GameTime gameTime);
    }
}
