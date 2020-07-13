using PureGym.Interfaces.Common;
using System;
using PureGym.Models.Containers;
using PureGym.Common.Exceptions;
using System.Collections.Generic;
using PureGym.Common.Enumerations;
using PureGym.Common;
using PureGym.Interfaces.Strategies;
using PureGym.Models.Entities;

namespace PureGym.Models.Compositions
{
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
            CheckIsNotNull(item);
            _VoucherItems.Insert(item);
        }

        /// <summary>
        /// Removes an item from the basket
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public void RemoveVoucher(string key)
        {
            CheckIsNotNull(key);
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
            CheckIsNotNull(item);
            _OfferItems.Insert(item);
        }

        /// <summary>
        /// Removes an offer from the basket
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public void RemoveOffer(string key)
        {
            CheckIsNotNull(key);
            _OfferItems.Remove(key);
        }
        #endregion

        protected void CheckIsNotNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
        }

    }
}
