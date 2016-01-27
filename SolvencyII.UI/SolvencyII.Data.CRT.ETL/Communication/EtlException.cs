using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Exception throw by internal ETL
    /// </summary>
    public class EtlException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtlException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EtlException(string message) : base(message)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EtlException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public EtlException(string message, Exception innerException)
            : base(message, innerException)
        {

        }       
    }

    /// <summary>
    /// Exception indicating data point duplication
    /// </summary>
    public class DataPointDuplicationException : EtlException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPointDuplicationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DataPointDuplicationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPointDuplicationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public DataPointDuplicationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
