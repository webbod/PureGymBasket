using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Models.Containers;
using PureGym.Models.Invoice;
using System;
using System.Collections.Generic;

namespace PureGym.Models.Compositions
{
    // TODO refactor this to be more generic
    public partial class GenericShoppingBasket<TBasketItemType, TVoucherType, TOfferType>
        where TBasketItemType : class, IIsABasketItem, new()
        where TVoucherType : class, IIsAVoucherItem, new()
        where TOfferType : class, IIsAnOfferItem, new()

    {
        protected BasketContainer<TBasketItemType> _BasketItems;
        protected VoucherContainer<TVoucherType> _VoucherItems;
        protected OfferContainer<TOfferType> _OfferItems;

        public Currency BasketCurrency {get; private set;}
        private Dictionary<StrategicActivity, Dictionary<TypesOfContainer,IIsAStrategy>> _ActivityStrategies;

        public GenericShoppingBasket(Currency basketCurrency)
        {
            BasketCurrency = basketCurrency;

            _BasketItems = new BasketContainer<TBasketItemType>(BasketCurrency);
            _VoucherItems = new VoucherContainer<TVoucherType>(BasketCurrency);
            _OfferItems = new OfferContainer<TOfferType>(BasketCurrency);

            _ActivityStrategies = new Dictionary<StrategicActivity, Dictionary<TypesOfContainer, IIsAStrategy>>();
        }

        public void Reset()
        {
            _BasketItems.Reset();
            _VoucherItems.Reset();
            _OfferItems.Reset();
        }

        #region Strategies
        /// <param name="activity">The activity to set the strategy of</param>
        /// <param name="strategy">The object that will handle this activity</param>
        /// <exception cref="ArgumentException"></exception>
        public void SetStrategyForActivity(IIsAStrategy strategy, StrategicActivity activity, TypesOfContainer containersItAppliesTo )
        {
            Helper.CheckIfValueIsNull(strategy, nameof(strategy));

            if (!_ActivityStrategies.ContainsKey(activity))
            {
                _ActivityStrategies.Add(activity, new Dictionary<TypesOfContainer, IIsAStrategy>());
            }

            foreach(TypesOfContainer container in Enum.GetValues(typeof(TypesOfContainer))) 
            {
                // Skip container types that represent several other containers - this is a flag enumeration

                if((container & (container - 1)) != 0){ continue; }

                if((containersItAppliesTo & container) == container)
                {
                    if (!_ActivityStrategies[activity].ContainsKey(container))
                    {
                        _ActivityStrategies[activity].Add(container, strategy);
                    }
                    else
                    {
                        _ActivityStrategies[activity][container] = strategy;
                    }
                }
            }
        }

        public IIsAnInvoice GenerateInvoice()
        {
            var zero = new Money(0, BasketCurrency);

            var invoice = new GenericInvoice
            {
                BasketItems = _BasketItems.Summarise(),
                BasketTotal = _BasketItems.CalculateTotal(zero),

                Vouchers = _VoucherItems.Summarise(),
                
                Offers = _OfferItems.Summarise(),
                OfferTotal = _OfferItems.CalculateTotal(zero)
            };

            invoice.VoucherTotal = _VoucherItems.CalculateTotal(invoice.BasketTotal - invoice.OfferTotal);

            return invoice;
        }

        /// <param name="activity">The activity to find a strategy for</param>
        /// <returns>The currently assigned strategy</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        protected TStrategyType GetActivityStrategy<TStrategyType>(StrategicActivity activity, TypesOfContainer container) where TStrategyType : IIsAStrategy
        {
            if (_ActivityStrategies.ContainsKey(activity) && _ActivityStrategies[activity].ContainsKey(container))
            {
                return (TStrategyType)_ActivityStrategies[activity][container];
            }

            throw new KeyNotFoundException();
        }
        #endregion

        #region Voucher Container
        /// <summary>
        /// Tries to apply a voucher to the basket
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public void TryToApplyVoucher(TVoucherType item)
        {
            Helper.CheckIfValueIsNull(item, nameof(item));
            _VoucherItems.Insert(item);
        }

        /// <summary>
        /// Removes an item from the basket
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public void RemoveVoucher(string key)
        {
            Helper.CheckIfValueIsNull(key, nameof(key));
            _VoucherItems.Remove(key);
        }

        #endregion

        #region Offer Container
        /// <summary>
        /// Tries to apply an offer to the basket
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public void TryToApplyOffer(TOfferType item)
        {
            Helper.CheckIfValueIsNull(item, nameof(item));
            _OfferItems.Insert(item);
        }

        /// <summary>
        /// Removes an offer from the basket
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public void RemoveOffer(string key)
        {
            Helper.CheckIfValueIsNull(key, nameof(key));
            _OfferItems.Remove(key);
        }
        #endregion
    }
}
