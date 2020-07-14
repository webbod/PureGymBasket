using PureGym.Interfaces.Common;
using System;
using PureGym.Models.Containers;
using PureGym.Common.Exceptions;
using System.Collections.Generic;
using PureGym.Common.Enumerations;
using PureGym.Common;
using PureGym.Interfaces.Strategies;
using PureGym.Models.Entities;
using System.Linq;

namespace PureGym.Models.Compositions
{
    public partial class GenericShoppingBasket<TBasketItemType, TVoucherType, TOfferType>
    {

        /// <param name="key">The key to locate the item</param>
        /// <param name="increment">The amount to change the quantity by</param>
        /// <exception cref="QuantityOutOfRangeException"></exception>
        public void IncrementQuantity(string key, int increment)
        {
            var item = _BasketItems.Find(key);
            Helper.CheckIfValueIsNull(item, nameof(item));

            var newQuantity = item.Quantity + increment;
            if (newQuantity < 0)
            {
                throw new QuantityOutOfRangeException($"{nameof(item.Quantity)} {SharedStrings.WasNegative}");
            }

            item.UpdateQuantity(quantity: newQuantity);
        }

        public void SeeIfOffersCanBeApplied()
        {
            foreach (var offer in _OfferItems.GetAll())
            {
                SeeIfOfferCanBeApplied(offer);
            }
        }

        private void SeeIfOfferCanBeApplied(IIsAnOfferItem offer)
        {
            // this is required to convert the 'specific' generic list to the interface list
            var items = (IEnumerable<IIsABasketItem>)_BasketItems.GetAll();
            offer.SeeIfItCanBeAppliedTo(items.ToList());
        }



        #region Basket Container - Generic operations
        /// <summary>
        /// Adds an item to the basket
        /// If it already exists then only the quantity will be changed
        /// </summary>
        /// <param name="item">Item to add to the basket</param>
        public BasketContainer<TBasketItemType> AddToBasket(TBasketItemType item)
        {
            GenericContainerOperations.AddToContainer<TBasketItemType>(_BasketItems, item);
            return _BasketItems;
        }

        /// <param name="key">the key of the element to remove</param>
        public void RemoveFromBasket(string key)
        {
            GenericContainerOperations.RemoveFromContainer<TBasketItemType>(_BasketItems, key);
        }

        /// <returns>The value of items in the basket</returns>
        public Money CalculateBasketTotal()
        {
            return GenericContainerOperations.CalculateContainerTotal<TBasketItemType>(_BasketItems, new Func<TBasketItemType, Money>((i) => i.Value * i.Quantity));
        }

        /// <typeparam name="TImportType">The type of the data parameter</typeparam>
        /// <param name="data">a representation of the basket in a different schema</param>
        public void ImportBasket<TImportType>(TImportType data) where TImportType : class
        {
            var strategy = GetActivityStrategy<IIsAContainerPersistenceStrategy>(StrategicActivity.Persistence, TypesOfContainer.Basket);
            GenericContainerOperations.ImportContainer<TBasketItemType, TImportType>(_BasketItems, strategy, data);
        }

        /// <typeparam name="TExportType">The type of the exported data</typeparam>
        /// <returns>a representation of the basket in a different schema</returns>
        public TExportType ExportBasket<TExportType>() 
            where TExportType : class
        {
            var strategy = GetActivityStrategy<IIsAContainerPersistenceStrategy>(StrategicActivity.Persistence, TypesOfContainer.Basket);
            return GenericContainerOperations.ExportContainer<TBasketItemType, TExportType>(_BasketItems, strategy);
        }

        /// <typeparam name="TOutputType">The type of the rendered data</typeparam>
        /// <returns>a representation of each item in the container</returns>
        public List<TOutputType> RenderBasket<TOutputType>()
            where TOutputType : class
        {
            var strategy = GetActivityStrategy<IIsAContainerPresentationStrategy>(StrategicActivity.Presentation, TypesOfContainer.Basket);
            return GenericContainerOperations.RenderContainer<TBasketItemType, TOutputType>(_BasketItems, strategy);
        }
        #endregion
    }
}