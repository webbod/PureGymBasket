using PureGym.Basket;
using PureGym.Common;
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
            shop.AddToBasket(WarehouseKeys.JUMP02);

            
            var voucher1 = shop.AddAVoucher(VoucherKeys.FivePoundGiftVoucher);

            
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
            string description = string.Empty;
            string message = string.Empty;
            
            if (invoice.Offers.Any())
            {
                (description, message) = Helper.Seperate(invoice.Offers?.First()?.Description);
            }

            foreach (var line in invoice.BasketItems)
            {
                Console.WriteLine(line.Description);
            }
            Console.WriteLine(invoice.BasketTotal);
            Console.WriteLine("-------------------------");

            if (!string.IsNullOrEmpty(description))
            {
                Console.WriteLine(description);
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

            if(!invoice.Vouchers.Any() && !invoice.Offers.Any())
            {
                Console.WriteLine($"{SharedStrings.NoVouchersApplied}");
            } 
            else if (!string.IsNullOrEmpty(message)) 
            {
                Console.WriteLine($"Message: {message}");
            }
        }
    }
}
