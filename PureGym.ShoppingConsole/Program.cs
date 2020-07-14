using PureGym.Basket;
using PureGym.Common.Enumerations;
using PureGym .Models.Compositions;

namespace PureGym.ShoppingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var shop = new GenericShop(new TestWarehouse(), new TestOfferFactory(), new ShoppingBasket(), Currency.GBP);
               
            shop.AddToBasket(WarehouseKeys.HAT001);
            shop.AddToBasket(WarehouseKeys.COAT01);
            shop.AddToBasket(WarehouseKeys.SHOE11);

            shop.UpdateBasketQuantity(WarehouseKeys.HAT001, 1);
            shop.RemoveFromBasket(WarehouseKeys.COAT01);


            shop.AddAnOffer(OfferKeys.SaveFivePoundsWithHeadGear);
            shop.AddAVoucher(VoucherKeys.FivePoundGiftVoucher);
            shop.AddAVoucher(VoucherKeys.FivePoundGiftVoucher);
            shop.AddAVoucher(VoucherKeys.FivePoundGiftVoucher);

            var invoice = shop.GenerateInvoice();

            // the shop only supports Json at the moment
            var json = shop.ExportBasketToJson();
            shop.ImportBasketFromJson(json);
            
            
        }        
    }
}
