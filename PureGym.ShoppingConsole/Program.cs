using PureGym.Basket;
using PureGym.Common.Enumerations;
using PureGym.ShoppingSystem;

namespace PureGym.ShoppingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var shop = new Shop(new TestWarehouse(), new TestOfferFactory(), new ShoppingBasket(), Currency.GBP);
               
            shop.AddToBasket("HAT001");
            shop.AddToBasket("COAT01");
            shop.AddToBasket("SHOE11");

            shop.UpdateBasketQuantity("HAT001", 3);
            shop.UpdateBasketQuantity("COAT01", 2);
            shop.RemoveFromBasket("SHOE11");
            shop.UpdateBasketQuantity("HAT001", 20);

            shop.AddAnOffer("SaveFivePoundsWithHeadGear");

            var total = shop.Total();
            var invoice = shop.GenerateInvoice();

            // the shop only supports Json at the moment
            var json = shop.ExportBasketToJson();
            shop.ImportBasketFromJson(json);
            
            
        }        
    }
}
