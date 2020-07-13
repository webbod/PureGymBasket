using PureGym.Common.Enumerations;
using System;

namespace PureGym.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an offer can not be applied to a basket
    /// </summary>
    public class OfferNotValidException : InvalidOperationException
    {
        /// <summary>
        /// Describes reasons why an offer was invalid
        /// </summary>
        public struct ValidityError
        {
            public StockCategory Category;
            public Money OutstandingBalance;

            public bool IsMissingCategory => Category != StockCategory.Undefined;
            public bool IsInsuffientSpend => OutstandingBalance > 0;
        }
        
        public ValidityError Reason { get; private set; }
        
        public OfferNotValidException(ValidityError reason) : base() {
            Reason = reason;
        }
    }
}
