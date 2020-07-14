using PureGym.Basket.Rules;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Models.Entities;
using System;
using System.Collections.Generic;
using PureGym.Interfaces.ShoppingSystem;

namespace PureGym.Basket
{
    public class OfferKeys
    {
        public const string SaveFivePoundsWithHeadGear = "SaveFivePoundsWithHeadGear";
        public const string SaveFivePoundsOnBasketsOverFifty = "SaveFivePoundsOnBasketsOverFifty";
    }

    public class VoucherKeys
    {
        public const string FivePoundGiftVoucher = "FivePoundGiftVoucher";
        public const string TenPoundGiftVoucher = "TenPoundGiftVoucher";
        public const string TwentyFivePoundGiftVoucher = "TwentyFivePoundGiftVoucher";
        public const string FiftyPoundsGiftVoucher = "FiftyPoundsGiftVoucher";
    }

    public class TestOfferFactory : IIsAnOfferFactory
    {
        protected Currency OffersCurrency;

        public void Initalise(Currency currency)
        {
            if (Helper.CheckIfValueIsNotNull(OffersCurrency)) { return; }

            OffersCurrency = currency;
        }

        public IIsAnOfferItem IssueOffer(string key)
        {
            switch (key)
            {
                case OfferKeys.SaveFivePoundsWithHeadGear:
                    return SaveFivePoundsWithHeadGear(OfferKeys.SaveFivePoundsWithHeadGear);

                case OfferKeys.SaveFivePoundsOnBasketsOverFifty:
                    return SaveFivePoundsOnBasketsOverFifty(OfferKeys.SaveFivePoundsOnBasketsOverFifty);
            }

            throw new KeyNotFoundException($"{nameof(key)} {SharedStrings.NotFound}");
        }

        public IIsAVoucherItem IssueVoucher(string key)
        {
            Helper.CheckIfValueIsNull(key, nameof(key));

            (string realKey, string id) = Helper.Seperate(key);

            switch (realKey)
            {
                case VoucherKeys.FivePoundGiftVoucher:
                    return FixedPriceGiftVoucher(VoucherKeys.FivePoundGiftVoucher, new Money(5, OffersCurrency));

                case VoucherKeys.TenPoundGiftVoucher:
                    return FixedPriceGiftVoucher(VoucherKeys.TenPoundGiftVoucher, new Money(10, OffersCurrency));

                case VoucherKeys.TwentyFivePoundGiftVoucher:
                    return FixedPriceGiftVoucher(VoucherKeys.TwentyFivePoundGiftVoucher, new Money(25, OffersCurrency));

                case VoucherKeys.FiftyPoundsGiftVoucher:
                    return FixedPriceGiftVoucher(VoucherKeys.FiftyPoundsGiftVoucher, new Money(50, OffersCurrency));
            }

            throw new KeyNotFoundException($"{nameof(key)} {SharedStrings.NotFound}");
        }

        private IIsAnOfferItem SaveFivePoundsWithHeadGear(string key)
        {
            var validationTest = new Func<List<IIsABasketItem>, bool>((basket) => basket.WhenSpendingOver50PoundsWithAnyHeadGearPurchase());
            return new GenericOffer(key, "£5.00 off Head Gear in baskets over £50.00 Offer", new Money(5, OffersCurrency), validationTest);
        }

        private IIsAnOfferItem SaveFivePoundsOnBasketsOverFifty(string key)
        {
            var validationTest = new Func<List<IIsABasketItem>, bool>((basket) => basket.WhenSpendingOver50Pounds());
            return new GenericOffer(key, "£5.00 off baskets over £50.00 Offer", new Money(5, OffersCurrency), validationTest);
        }

        private IIsAVoucherItem FixedPriceGiftVoucher(string key, Money value)
        {
            var voucherKey = Helper.Combine(key, Guid.NewGuid());
            return new GenericVoucher(voucherKey, $"{value} Gift Voucher", value);
        }
    }
}
