using PureGym.Basket.Rules;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Models.Entities;
using System;
using System.Collections.Generic;
using PureGym.ShoppingSystem;

namespace PureGym.Basket
{
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
                case "SaveFivePoundsWithHeadGear":
                    return SaveFivePoundsWithHeadGear();
            }

            throw new KeyNotFoundException("Offer not found");
        }

        public IIsAnOfferItem SaveFivePoundsWithHeadGear()
        {
            var validationTest = new Func<List<IIsABasketItem>, bool>((basket) => basket.WhenSpendingOver50PoundsWithAnyHeadGearPurchase());
            return new GenericOffer("YY-YYYY", "£5.00 off Head Gear in baskets over £50.00 Offer", new Money(5, OffersCurrency), validationTest);
        }
    }
}
