using PureGym.Basket;
using PureGym.Common.Enumerations;
using PureGym .Models.Compositions;
using System;
using System.Linq;

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
            
            foreach(var line in invoice.BasketItems)
            {
                Console.WriteLine(line.Description);
            }
            Console.WriteLine(invoice.BasketTotal);
            Console.WriteLine("-------------------------");
            foreach (var line in invoice.Offers)
            {
                Console.WriteLine(line.Description);
            }
            if (invoice.OfferTotal > 0)
            {
                Console.WriteLine(invoice.BasketTotal);
            }
            if (invoice.Offers.Any())
            {
                Console.WriteLine("-------------------------");
            }
            foreach (var line in invoice.Vouchers)
            {
                Console.WriteLine(line.Description);
            }
            if (invoice.VoucherTotal > 0)
            {
                Console.WriteLine(invoice.VoucherTotal);
            }
            if (invoice.Vouchers.Any())
            {
                Console.WriteLine("-------------------------");
            }
            Console.WriteLine(invoice.GrandTotal);
            Console.ReadLine();
            Console.ReadLine();
        }        
    }
}
