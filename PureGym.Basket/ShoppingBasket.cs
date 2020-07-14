using PureGym.Basket.Strategies.Persistence;
using PureGym.Basket.Strategies.Presentation;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.ShoppingSystem;
using PureGym.Models.Compositions;
using PureGym.Models.Entities;
using PureGym.Models.Summary;
using System.Collections.Generic;

namespace PureGym.Basket
{
    public class ShoppingBasket : IIsShoppingBasket
    {
        protected GenericShoppingBasket<GenericBasketItem, GenericVoucher, GenericOffer> Basket;

        public void Initalise(Currency currency)
        {
            if (Helper.CheckIfValueIsNotNull(Basket)) { return; }

            Basket = new GenericShoppingBasket<GenericBasketItem, GenericVoucher, GenericOffer>(currency);

            var persistenceFactory = new JsonStrategyFactory();
            var presentationFactory = new TextStrategyFactory();

            // all of the containers share the same persistence strategy 
            Basket.SetStrategyForActivity(persistenceFactory, StrategicActivity.Persistence, TypesOfContainer.AllShoppingBasketContainers);

            // each container has a different presentation strategy
            Basket.SetStrategyForActivity(presentationFactory, StrategicActivity.Presentation, TypesOfContainer.Basket);
            Basket.SetStrategyForActivity(presentationFactory, StrategicActivity.Presentation, TypesOfContainer.Offer);
            Basket.SetStrategyForActivity(presentationFactory, StrategicActivity.Presentation, TypesOfContainer.Voucher);
        }

        public void AddAnItem(IIsABasketItem item)
        {
            Basket.AddToBasket(item as GenericBasketItem);
        }

        public void IncrementQuantity(string key, int increment)
        {
            Basket.IncrementQuantity(key, increment);
        }

        public void RemoveItem(string key)
        {
            Basket.RemoveFromBasket(key);
        }

        public void AddAnOffer(IIsAnOfferItem offer)
        {
            Basket.TryToApplyOffer(offer as GenericOffer);
        }

        public void RemoveOffer(string key)
        {
            Basket.RemoveOffer(key);
        }

        public void RemoveVoucher(string key)
        {
            Basket.RemoveVoucher(key);
        }

        public void AddAVoucher(IIsAVoucherItem voucher)
        {
            Basket.TryToApplyVoucher(voucher as GenericVoucher);
        }

        public void CheckOffers()
        {
            Basket.SeeIfOffersCanBeApplied();
        }

        public Money CalculateBasketTotal()
        {
            return Basket.CalculateBasketTotal();
        }

        public IIsAnInvoice GenerateInvoice()
        {
            CheckOffers();

            return Basket.GenerateInvoice();
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
