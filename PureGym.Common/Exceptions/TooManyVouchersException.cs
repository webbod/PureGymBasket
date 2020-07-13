using System;

namespace PureGym.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there are too many vouchers
    /// </summary>
    public class TooManyVouchersException : ArgumentException
    {
        public TooManyVouchersException(string message) : base(message)
        {

        }
    }
}
