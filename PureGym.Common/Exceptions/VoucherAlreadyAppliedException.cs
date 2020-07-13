using System;

namespace PureGym.Common.Exceptions
{

    /// <summary>
    /// The exception that is thrown when a voucher has already been used
    /// </summary>
    public class VoucherAlreadyAppliedException : ArgumentException
    {
        public VoucherAlreadyAppliedException(string message) : base(message)
        {

        }
    }
}
