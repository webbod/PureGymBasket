using System.Collections.Generic;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;

namespace PureGym.Interfaces.ShoppingSystem
{
    public interface IIsShoppingBasket
    {
        void Initalise(Currency currency);

        void AddAnItem(IIsABasketItem item);

        void IncrementQuantity(string key, int increment);

        void RemoveItem(string key);

        void AddAnOffer(IIsAnOfferItem offer);

        void RemoveOffer(string key);

        void AddAVoucher(IIsAVoucherItem voucher);

        void RemoveVoucher(string key);

        void CheckOffers();

        Money CalculateBasketTotal();

        IIsAnInvoice GenerateInvoice();

        string ExportBasketToJson();

        void ImportBasketFromJson(string json);
    }
}