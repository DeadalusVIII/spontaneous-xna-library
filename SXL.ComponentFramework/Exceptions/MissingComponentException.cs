using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SXL.ComponentFramework.Exceptions
{
    class MissingComponentException : Exception
    {
        public MissingComponentException(string componentName)
            : base("The requested component '" + componentName + "' does not exist in the required actor.")
        {
        }
    }
}
