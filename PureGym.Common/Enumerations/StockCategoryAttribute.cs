using System.ComponentModel;

namespace PureGym.Common.Enumerations
{
    public class StockCategoryAttribute : DescriptionAttribute
    {
        public bool EligibleForDiscount { get; set; }

        public StockCategoryAttribute(string description, bool eligibleForDiscount = true) : base(description)
        {
            EligibleForDiscount = eligibleForDiscount;
        }
    }
}
