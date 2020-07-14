using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Common.Exceptions;
using PureGym.Models.Containers;
using PureGym.Models.Entities;
using PureGym.Models.Rules;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PureGym.UnitTests
{
    public class BasketRulesTests
    {
        private BasketContainer<GenericBasketItem> newBasket(){
            var basket = new BasketContainer<GenericBasketItem>(Currency.GBP);
            basket.Insert(new GenericBasketItem("T01", "footwear", new Money(80, Currency.GBP), StockCategory.Footwear, 1));
            basket.Insert(new GenericBasketItem("T02", "shoes", new Money(30, Currency.GBP), StockCategory.Vouchers, 1));
            return basket;
        }

        [Fact]
        public void ThisRuleIsValidIfBasketContainsTheRightStuff()
        {
            var basket = newBasket();
            var actual = basket.GetAll()
                .IsNotEmpty()
                .AndHasStuffFrom(StockCategory.Footwear)
                .ThenItIsValid();
            Assert.True(actual);
        }

        [Fact]
        public void ThisRuleIsNotValidBecauseBasketDoesntContainTheRightStuff()
        {
            var basket = newBasket();

            try
            {
                basket.GetAll()
                .IsNotEmpty()
                .AndHasStuffFrom(StockCategory.Swimwear)
                .ThenItIsValid();
            }
            catch(OfferNotValidException ex)
            {
                Assert.True(ex.Reason.IsMissingCategory);
            }
        }

        [Fact]
        public void ThisRuleIsValidIfBasketHasAHighEnoughValue()
        {
            var basket = newBasket();
            var actual = basket.GetAll()
                .IsNotEmpty()
                .AndTheValueIsAtLeast(new Money(100, Currency.GBP))
                .ThenItIsValid();
            Assert.True(actual);
        }

        [Fact]
        public void ThisRuleIsNotValidBecauseBasketValueIsTooLow()
        {
            var basket = newBasket();
            try
            {
                basket.GetAll()
                .IsNotEmpty()
                .AndTheValueIsAtLeast(new Money(500, Currency.GBP))
                .ThenItIsValid();
            }
            catch(OfferNotValidException ex)
            {
                Assert.True(ex.Reason.IsInsuffientSpend);
            }
        }

        [Fact]
        public void ThisRuleIsValidIfBasketHasAHighEnoughValueAfterIgnoringSomeStuff()
        {
            var basket = newBasket();
            var actual = basket.GetAll()
                .IsNotEmpty()
                .AndIfWeIgnoreStuffFrom(StockCategory.Vouchers)
                .AndTheValueIsAtLeast(new Money(70, Currency.GBP))
                .ThenItIsValid();

            Assert.True(actual);
        }

        [Fact]
        public void ThisRuleIsNotValidBecaueBasketValueIsTooLowAfterIgnoringSomeStuff()
        {
            var basket = newBasket();

            try
            {
                basket.GetAll()
                .IsNotEmpty()
                .AndIfWeIgnoreStuffFrom(StockCategory.Footwear)
                .AndTheValueIsAtLeast(new Money(70, Currency.GBP))
                .ThenItIsValid();
            }
            catch(OfferNotValidException ex)
            {
                Assert.True(ex.Reason.IsInsuffientSpend);
            }
            
        }
    }
}
