using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.ShoppingSystem;

namespace PureGym.Interfaces.ShoppingSystem
{
    public interface IIsAShop
    {
        void Initalise(IIsAWarehouse warehouse, IIsAnOfferFactory offerFactory, IIsShoppingBasket basket, Currency currency = Currency.GBP);

        Currency ShopCurrency { get; }

        void AddToBasket(string key);
        void UpdateBasketQuantity(string key, int increment);
        void RemoveFromBasket(string key);

        void AddAnOffer(string key);
        void RemoveAnOffer(string key);

        string AddAVoucher(string key);
        void RemoveAVoucher(string key);

        string ExportBasketToJson();
        void ImportBasketFromJson(string json);

        IIsAnInvoice GenerateInvoice();

        void EmptyBasket();
    }
}