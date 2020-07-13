using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Models.Entities;
using PureGym.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PureGym.Basket.Rules
{
    /// <summary>
    /// A set of extension methods that can be chained to construct tests to check if an offer can be used against a basket
    /// These tests should be very granular and only encode a single piece of business logic
    /// Try to keep the naming chatty and in such a way that the chain reads like a sentence and would 
    /// make sense to a non-technical person
    /// </summary>
    /// <example>
    /// Keep all the lists as IIsABasketItem, if you need to access something more specific then cast the contents of the list
    /// e.g.
    ///     var concreteList = items.Select( item => item as MoreSpecificType);
    /// </example>
    public static class BusinessRuleExtensions
    {
        #region start with one of these
        public static IEnumerable<IIsABasketItem> IncludingAnyVouchers(this IEnumerable<IIsABasketItem> items) =>
            items.IsNotEmpty();

        public static IEnumerable<IIsABasketItem> IgnoringAnyVouchers(this IEnumerable<IIsABasketItem> items) =>
            items.IsNotEmpty()
            .AndIfWeIgnoreStuffFrom(StockCategory.Vouchers);
        #endregion

        #region any restrictions
        public static IEnumerable<IIsABasketItem> AndOnlyWhenThereIsHeadGear(this IEnumerable<IIsABasketItem> items) {
            var i = items.First() as GenericBasketItem;
            return items.AndHasStuffFrom(StockCategory.HeadGear);
        }
        #endregion

        #region any other tests
        public static IEnumerable<IIsABasketItem> WhenSpendingAtLeast50Pounds(this IEnumerable<IIsABasketItem> items) =>
            items.AndTheValueIsAtLeast(new Money(50, Currency.GBP));
        #endregion

        #region end with this
        public static bool ThenItCanBeUsed(this IEnumerable<IIsABasketItem> items) => 
            items.ThenItIsValid();
        #endregion
    }
}

