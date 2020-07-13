namespace PureGym.Common.Enumerations
{
    // TODO: Replace this with something more generic - there is a lot of business logic in here
    public enum StockCategory
    {
        [StockCategory(SharedStrings.Undefined)]
        Undefined,
        [StockCategory("Head Gear")]
        HeadGear,
        [StockCategory("Footwear")]
        Footwear,
        [StockCategory("Casualwear")]
        Casualwear,
        [StockCategory("Swimwear")]
        Swimwear,
        [StockCategory("Underwear")]
        Underwear,
        [StockCategory("Formalwear")]
        Formalwear,
        [StockCategory("Vouchers", eligibleForDiscount: false)]
        Vouchers
    }
}
