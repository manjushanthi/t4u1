using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Arelle
{
    public class ArelleException : Exception
    {
         public ArelleException(string message) : base(message)
        {
            
        }

         public ArelleException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
