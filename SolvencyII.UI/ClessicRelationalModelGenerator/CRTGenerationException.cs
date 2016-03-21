using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation
{
    public class CRTGenerationException : Exception
    {
        public CRTGenerationException(string message) : base(message)
        {
            
        }

        public CRTGenerationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
