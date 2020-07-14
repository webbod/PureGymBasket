using System;

namespace PureGym.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an item has an invalid price
    /// </summary>
    public class ValueOutOfRangeException : ArgumentOutOfRangeException
    {
        public ValueOutOfRangeException(string message) : base(message)
        {

        }
    }
}
