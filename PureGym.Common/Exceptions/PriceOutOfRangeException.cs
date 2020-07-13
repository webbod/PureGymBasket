using System;

namespace PureGym.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an item has an invalid price
    /// </summary>
    public class PriceOutOfRangeException : ArgumentOutOfRangeException
    {
        public PriceOutOfRangeException(string message) : base(message)
        {

        }
    }
}
