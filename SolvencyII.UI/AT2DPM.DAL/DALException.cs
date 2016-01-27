using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL
{
    public class DALException : Exception
    {
        public DALException( 
            string auxMessage, Exception inner ) :
                base( auxMessage, inner )
        {
            
        }

    }
}
