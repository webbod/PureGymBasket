using System.Collections.Generic;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Models.Entities;

namespace PureGym.ShoppingSystem
{
    public interface IIsShoppingBasket
    {
        void Initalise(Currency currency);

        void AddAnItem(GenericBasketItem item);
        void IncrementQuantity(string key, int increment);
        void RemoveFromBasket(string key);

        void AddAnOffer(IIsAnOffer offer);
        
        void CheckOffers();

        Money CalculateBasketTotal();

        List<string> GenerateInvoice();

        string ExportBasketToJson();
        void ImportBasketFromJson(string json);
    }
}