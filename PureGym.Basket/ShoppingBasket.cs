using PureGym.Basket.Strategies.Persistence;
using PureGym.Basket.Strategies.Presentation;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.Strategies;
using PureGym.Models.Compositions;
using PureGym.Models.Containers;
using PureGym.Models.Entities;
using PureGym.ShoppingSystem;
using System;
using System.Collections.Generic;

namespace PureGym.Basket
{
    public class ShoppingBasket : IIsShoppingBasket
    {
        protected GenericShoppingBasket<GenericBasketItem, GenericVoucher, IIsAnOffer> Basket;

        public void Initalise(Currency currency)
        {
            if (Helper.CheckIfValueIsNotNull(Basket)) { return; }

            Basket = new GenericShoppingBasket<GenericBasketItem, GenericVoucher, IIsAnOffer>(currency);

            var persistenceFactory = new JsonStrategyFactory();
            var presentationFactory = new TextStrategyFactory();

            // all of the containers share the same persistence strategy 
            Basket.SetStrategyForActivity(persistenceFactory, StrategicActivity.Persistence, TypesOfContainer.AllShoppingBasketContainers);

            // each container has a different presentation strategy
            Basket.SetStrategyForActivity(presentationFactory, StrategicActivity.Presentation, TypesOfContainer.Basket);
            Basket.SetStrategyForActivity(presentationFactory, StrategicActivity.Presentation, TypesOfContainer.Offer);
            Basket.SetStrategyForActivity(presentationFactory, StrategicActivity.Presentation, TypesOfContainer.Voucher);
        }

        public void AddAnItem(GenericBasketItem item)
        {
            Basket.AddToBasket(item);
        }

        public void IncrementQuantity(string key, int increment)
        {
            Basket.IncrementQuantity(key, increment);
        }

        public void RemoveFromBasket(string key)
        {
            Basket.RemoveFromBasket(key);
        }

        public void AddAnOffer(IIsAnOffer offer)
        {
            Basket.TryToApplyOffer(offer);
        }

        public void CheckOffers()
        {
            Basket.SeeIfOffersCanBeApplied();
        }

        public Money CalculateBasketTotal()
        {
            return Basket.CalculateBasketTotal();
        }

        public List<string> GenerateInvoice()
        {
            return Basket.RenderBasket<string>();
        }

        public string ExportBasketToJson()
        {
            return Basket.ExportBasket<string>();
        }

        public void ImportBasketFromJson(string json)
        {
            Basket.ImportBasket<string>(json);
        }
    }
}
