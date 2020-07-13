using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Common.Exceptions;
using PureGym.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PureGym.Models.Rules
{
    /// <summary>
    /// A set of extension methods for lists of basket items that can be chained to construct tests for exploring the state of a BasketContainer, 
    /// DO NOT PUT ANY BUSINESS LOGIC HERE
    /// </summary>
    public static class BasketRuleExtensions
    {
        /// <summary>
        /// Tests if the list has any items
        /// Always start a chain with this
        /// </summary>
        /// <param name="items">a list of basket items</param>
        /// <returns>the original list is not empty</returns>
        /// <exception cref="ArgumentException">thrown if the list is empty</exception>
        /// <example>
        ///     Basket.IsNotEmpty().ThenItIsValid(); 
        /// </example>
        public static IEnumerable<IIsABasketItem> IsNotEmpty(this IEnumerable<IIsABasketItem> items) => 
            items?.Any() == true ? 
                items : 
                throw new ArgumentNullException();

        /// <summary>
        /// Tests if any of the items in the list are from a particular StockCategory
        /// </summary>
        /// <param name="items">a list of basket items</param>
        /// <returns>the original list it contains a suitable item</returns>
        /// <exception cref="OfferNotValidException">thrown if the list doesn't contain a suitable item</exception>
        /// <example>
        ///     Basket.IsNotEmpty().AndHasStuffFrom(StockCategory.HeadGear).ThenItIsValid();
        /// </example>
        public static IEnumerable<IIsABasketItem> AndHasStuffFrom(this IEnumerable<IIsABasketItem> items, StockCategory category) => 
            items.Any(i => i.Category == category) ? 
                items : 
                throw new OfferNotValidException(new OfferNotValidException.ValidityError { Category = category });

        /// <summary>
        /// Excludes items from the list if they from a particular StockCategory
        /// </summary>
        /// <param name="items">a list of basket items</param>
        /// <returns>a filtered list of items</returns>
        /// <example>
        ///     Basket.IsNotEmpty().AndIfWeIgnoreStuffFrom(StockCategory.Vouchers).ThenItIsValid();
        /// </example>
        public static IEnumerable<IIsABasketItem> AndIfWeIgnoreStuffFrom(this IEnumerable<IIsABasketItem> items, StockCategory category) => 
            items.Where(i => i.Category != category).ToList();

        /// <summary>
        /// Tests if the total value of the list is at least the minimumValue
        /// </summary>
        /// <param name="items">a list of basket items</param>
        /// <param name="minimumValue">the minium value the basket needs to have - the currecnies need to match</param>
        /// <returns>the original list the value is high enough</returns>
        /// <exception cref="OfferNotValidException">if the value of the list is too low</exception>
        /// <exception cref="IncompatibleCurrenciesException">If the minimum value is a different currency to the list</exception>
        /// <example>
        ///     var fiftyPounds = new Money(50);        
        ///     Basket.IsNotEmpty().AndTheValueIsAtLeast(50).ThenItIsValid();
        /// </example>        
        public static IEnumerable<IIsABasketItem> AndTheValueIsAtLeast(this IEnumerable<IIsABasketItem> items, Money minimumValue) =>
            Money.CurrenciesMatch(minimumValue, items.First().Price) ?
                items.AndTheValueIsAtLeast(minimumValue.Value) :
                throw new IncompatibleCurrenciesException($"The {nameof(items)} is in a different currency to the {nameof(minimumValue)}");

        /// <summary>
        /// Tests if the total value of the list is at least the minimumValue
        /// Ignores the currency of the items
        /// </summary>
        /// <param name="items">a list of basket items</param>
        /// <param name="minimumValue">the minium value the basket needs to have</param>
        /// <returns>the original list the value is high enough</returns>
        /// <exception cref="OfferNotValidException">if the value of the list is too low</exception>
        /// <example>
        ///     Basket.IsNotEmpty().AndTheValueIsAtLeast(50).ThenItIsValid();
        /// </example>                
        public static IEnumerable<IIsABasketItem> AndTheValueIsAtLeast(this IEnumerable<IIsABasketItem> items, decimal minimumValue)
        {
            var currency = items.First()?.Price.Currency ?? Currency.Unknown;
            var total = items.Sum(i => i.Price * i.Quantity, currency);
            return total >= minimumValue ? 
                items : 
                throw new OfferNotValidException(new OfferNotValidException.ValidityError { OutstandingBalance = minimumValue - total });
        }

        /// <summary>
        /// Tests if the list has any items
        /// Always end a chain with this
        /// </summary>
        /// <param name="items">a list of basket items</param>
        /// <returns>true if the list is not empty</returns>
        /// <example>
        ///     Basket.IsNotEmpty()
        ///     .AndHasStuffFrom(StockCategory.HeadGear)
        ///     .AndIfWeIgnoreStuffFrom(StockCategory.Vouchers)
        ///     .AndTheValueIsAtLeast(50)
        ///     .ThenItIsValid(); 
        /// </example>
        public static bool ThenItIsValid(this IEnumerable<IIsABasketItem> items) => items.Any() == true;
        
    }
}
