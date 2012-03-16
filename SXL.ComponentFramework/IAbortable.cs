using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SXL.ComponentFramework
{
    public interface IAbortable
    {
        void Abort(GameEvent reason);
    }
}
