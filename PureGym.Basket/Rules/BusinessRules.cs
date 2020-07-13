﻿using PureGym.Interfaces.Common;
using System.Collections.Generic;


namespace PureGym.Basket.Rules
{
    public static class BusinessRules
    {
        public static bool WhenSpendingOver50PoundsWithAnyHeadGearPurchase(this IEnumerable<IIsABasketItem> basket) =>
            basket
            .IgnoringAnyVouchers()
            .AndOnlyWhenThereIsHeadGear()
            .WhenSpendingAtLeast50Pounds()
            .ThenItCanBeUsed();

    }
}
