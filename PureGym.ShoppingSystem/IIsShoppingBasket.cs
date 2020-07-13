using System.Collections.Generic;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Models.Entities;

namespace PureGym.ShoppingSystem
{
    public interface IIsShoppingBasket
    {
        void Initalise(Currency currency);

        void AddAnItem(IIsABasketItem item);

        void IncrementQuantity(string key, int increment);

        void RemoveFromBasket(string key);

        void AddAnOffer(IIsAnOfferItem offer);

        void AddAVoucher(IIsAVoucherItem voucher);

        void CheckOffers();

        Money CalculateBasketTotal();

        List<string> GenerateInvoice();

        string ExportBasketToJson();
        void ImportBasketFromJson(string json);
    }
}