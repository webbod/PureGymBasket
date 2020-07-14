using PureGym.Basket;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
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

            shop.UpdateBasketQuantity(WarehouseKeys.HAT001, 3);
            shop.UpdateBasketQuantity(WarehouseKeys.HAT001, -2);
            shop.RemoveFromBasket(WarehouseKeys.COAT01);


            shop.AddAnOffer(OfferKeys.SaveFivePoundsWithHeadGear);
            
            // each voucher gets a unique key - this allows them to stack, but the key is generated dynamically
            var voucher1 = shop.AddAVoucher(VoucherKeys.FivePoundGiftVoucher);
            var voucher2 = shop.AddAVoucher(VoucherKeys.FivePoundGiftVoucher);
            var voucher3 = shop.AddAVoucher(VoucherKeys.FivePoundGiftVoucher);

            shop.RemoveAVoucher(voucher3);

            var invoice = shop.GenerateInvoice();

            // the shop only supports Json at the moment
            var json = shop.ExportBasketToJson();
            shop.ImportBasketFromJson(json);

            RenderInvoice(invoice);
            Console.ReadLine();
            Console.ReadLine();
        }        

        static void RenderInvoice(IIsAnInvoice invoice)
        {
            string offerDetails = string.Empty;
            string message = string.Empty;

            if (invoice.Offers.Any())
            {
                var descriptionParts = invoice.Offers.First().Description.Split("::", StringSplitOptions.RemoveEmptyEntries);
                offerDetails = descriptionParts.First();

                if (descriptionParts.Count() == 2) { message = descriptionParts.Last(); }
            }

            foreach (var line in invoice.BasketItems)
            {
                Console.WriteLine(line.Description);
            }
            Console.WriteLine(invoice.BasketTotal);
            Console.WriteLine("-------------------------");

            if (!string.IsNullOrEmpty(offerDetails))
            {
                Console.WriteLine(offerDetails);
                Console.WriteLine("-------------------------");
            }

            foreach (var line in invoice.Vouchers)
            {
                Console.WriteLine(line.Description);
            }

            if (invoice.Vouchers.Any())
            {
                Console.WriteLine("-------------------------");
            }
            Console.WriteLine($"Total: {invoice.GrandTotal}");

            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine($"Message: {message}");
            }
        }
    }
}
