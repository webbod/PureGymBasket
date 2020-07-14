using System.Collections.Generic;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.ShoppingSystem;

namespace PureGym.Models.Interfaces.ShoppingSystem
{
    public interface IShop
    {
        Currency ShopCurrency { get; }

        void Initalise(IIsAWarehouse warehouse, IIsAnOfferFactory offerFactory, IIsShoppingBasket basket, Currency currency = Currency.GBP);

        void AddToBasket(string key);

        void UpdateBasketQuantity(string key, int increment);

        void AddAnOffer(string key);

        void AddAVoucher(string key);

        void RemoveFromBasket(string key);

        void RemoveAnOffer(string key);

        void RemoveAVoucher(string key);
        
        string ExportToJson();

        void ImportFromJson(string json);

        IIsAnInvoice GenerateInvoice();
    }
}