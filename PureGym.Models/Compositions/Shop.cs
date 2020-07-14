using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.ShoppingSystem;
using System.Collections.Generic;

namespace PureGym.Models.Compositions
{
    public class Shop : IShop
    {
        public Currency ShopCurrency { get; private set; }

        protected IIsAWarehouse Warehouse { get; private set; }

        protected IIsAnOfferFactory OfferFactory { get; private set; }

        protected IIsShoppingBasket Basket { get; private set; }

        public Shop(IIsAWarehouse warehouse, IIsAnOfferFactory offerFactory, IIsShoppingBasket basket, Currency currency = Currency.GBP)
        {
            Initalise(warehouse, offerFactory, basket, currency);
        }

        public void Initalise(IIsAWarehouse warehouse, IIsAnOfferFactory offerFactory, IIsShoppingBasket basket, Currency currency = Currency.GBP)
        {
            if (Helper.CheckIfValueIsNotNull(Basket)) { return; }

            ShopCurrency = currency;

            Warehouse = warehouse;
            Warehouse.Initalise(ShopCurrency);

            OfferFactory = offerFactory;
            OfferFactory.Initalise(ShopCurrency);

            Basket = basket;
            Basket.Initalise(ShopCurrency);
        }

        public void AddToBasket(string key)
        {
            Basket.AddAnItem(Warehouse.GetItem(key));
        }

        public void UpdateBasketQuantity(string key, int increment)
        {
            Basket.IncrementQuantity(key, increment);
        }

        public void RemoveFromBasket(string key)
        {
            Basket.RemoveItem(key);
        }

        public void AddAnOffer(string key)
        {
            Basket.AddAnOffer(OfferFactory.IssueOffer(key));
        }

        public void RemoveAnOffer(string key)
        {
            Basket.RemoveOffer(key);
        }

        public void AddAVoucher(string key)
        {
            Basket.AddAVoucher(OfferFactory.IssueVoucher(key));
        }

        public void RemoveAVoucher(string key)
        {
            Basket.RemoveVoucher(key);
        }

        // TODO to make these work with different persistence models
        public void ImportBasketFromJson(string json)
        {
            Basket.ImportBasketFromJson(json);
        }

        // TODO to make these work with different persistence models
        public string ExportBasketToJson()
        {
            return Basket.ExportBasketToJson();
        }

        public IIsAnInvoice GenerateInvoice()
        {
            return Basket.GenerateInvoice();
        }
    }
}
