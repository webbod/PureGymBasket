using PureGym.Basket;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Models.Compositions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PureGym.UnitTests
{
    public class ShopTests
    {
        private GenericShop Shop = new GenericShop(new TestWarehouse(), new TestOfferFactory(), new ShoppingBasket(), Currency.GBP);
        
        [Fact]
        public void TestCase_1()
        {
            Shop.EmptyBasket();
            Shop.AddToBasket(WarehouseKeys.HAT001);
            Shop.AddToBasket(WarehouseKeys.JUMP02);
            Shop.AddAVoucher(VoucherKeys.FivePoundGiftVoucher);

            var invoice = Shop.GenerateInvoice();

            var expected = 60.15m;
            var actual = invoice.GrandTotal.Value;

            Assert.Equal(expected, actual);

            Assert.Single(invoice.Vouchers);
            Assert.Empty(invoice.Offers);
            Assert.Equal(2, invoice.BasketItems.Count);
        }

        [Fact]
        public void TestCase_2()
        {
            Shop.EmptyBasket();
            Shop.AddToBasket(WarehouseKeys.HAT002);
            Shop.AddToBasket(WarehouseKeys.JUMP01);
            Shop.AddAnOffer(OfferKeys.SaveFivePoundsWithHeadGear);

            var invoice = Shop.GenerateInvoice();

            (string description, string message)  = Helper.Seperate(invoice.Offers.First().Description);

            var expected =51.00m;
            var actual = invoice.GrandTotal.Value;

            Assert.Equal(expected, actual);
            Assert.Empty(invoice.Vouchers);
            Assert.Single(invoice.Offers);
            Assert.Equal(2, invoice.BasketItems.Count);
            Assert.False(string.IsNullOrEmpty(message));         
        }

        [Fact]
        public void TestCase_3()
        {
            Shop.EmptyBasket();
            Shop.AddToBasket(WarehouseKeys.HAT002);
            Shop.AddToBasket(WarehouseKeys.JUMP01);
            Shop.AddToBasket(WarehouseKeys.LIGHT1);
            Shop.AddAnOffer(OfferKeys.SaveFivePoundsWithHeadGear);

            var invoice = Shop.GenerateInvoice();

            (string description, string message) = Helper.Seperate(invoice.Offers.First().Description);

            // The answer given on test case 3 is wrong
            var expected = 49.50m;
            var actual = invoice.GrandTotal.Value;

            Assert.Equal(expected, actual);
            Assert.Empty(invoice.Vouchers);
            Assert.Single(invoice.Offers);
            Assert.Equal(3, invoice.BasketItems.Count);
            Assert.True(string.IsNullOrEmpty(message));
        }

        [Fact]
        public void TestCase_4()
        {
            Shop.EmptyBasket();
            Shop.AddToBasket(WarehouseKeys.HAT002);
            Shop.AddToBasket(WarehouseKeys.JUMP01);
            Shop.AddAVoucher(VoucherKeys.FivePoundGiftVoucher);
            Shop.AddAnOffer(OfferKeys.SaveFivePoundsOnBasketsOverFifty);

            var invoice = Shop.GenerateInvoice();

            (string description, string message) = Helper.Seperate(invoice.Offers.First().Description);

            var expected = 41.00m;
            var actual = invoice.GrandTotal.Value;

            Assert.Equal(expected, actual);
            Assert.Single(invoice.Vouchers);
            Assert.Single(invoice.Offers);
            Assert.Equal(2, invoice.BasketItems.Count);
            Assert.True(string.IsNullOrEmpty(message));
        }

        [Fact]
        public void TestCase_5()
        {
            Shop.EmptyBasket();
            Shop.AddToBasket(WarehouseKeys.HAT002);
            Shop.AddToBasket(WarehouseKeys.VOUR30);
            Shop.AddAnOffer(OfferKeys.SaveFivePoundsWithHeadGear);

            var invoice = Shop.GenerateInvoice();

            (string description, string message) = Helper.Seperate(invoice.Offers.First().Description);

            var expected = 55.00m;
            var actual = invoice.GrandTotal.Value;

            Assert.Equal(expected, actual);
            Assert.Empty(invoice.Vouchers);
            Assert.Single(invoice.Offers);
            Assert.Equal(2, invoice.BasketItems.Count);
            Assert.False(string.IsNullOrEmpty(message));
        }

        [Fact]
        public void TestCase_6()
        {
            Shop.EmptyBasket();
            Shop.AddToBasket(WarehouseKeys.JUMP02);
            Shop.AddToBasket(WarehouseKeys.LIGHT1);

            var invoice = Shop.GenerateInvoice();
            var expected = 58.15m;
            var actual = invoice.GrandTotal.Value;

            Assert.Equal(expected, actual);
            Assert.Empty(invoice.Vouchers);
            Assert.Empty(invoice.Offers);
            Assert.Equal(2, invoice.BasketItems.Count);
        }
    }
}
