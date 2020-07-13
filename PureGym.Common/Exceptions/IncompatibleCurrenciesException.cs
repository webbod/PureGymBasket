using System;

namespace PureGym.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an operation involves Money from different currencies
    /// </summary>
    public class IncompatibleCurrenciesException : ArgumentException
    {
        public IncompatibleCurrenciesException(string message): base(message)
        {

        }
    }
}
