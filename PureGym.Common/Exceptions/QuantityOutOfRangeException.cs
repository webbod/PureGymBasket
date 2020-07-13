using System;

namespace PureGym.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the quantity is out of range
    /// </summary>
    public class QuantityOutOfRangeException : ArgumentOutOfRangeException
    {
        public QuantityOutOfRangeException(string message) : base(message)
        {

        }
    }
}
