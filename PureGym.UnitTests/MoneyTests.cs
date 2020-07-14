using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PureGym.UnitTests
{
    public class MoneyTests
    {
        [Fact]
        public void MoneyFromDifferentCurrenciesCannotBeCombined()
        {
            var pounds = new Money(100, Currency.GBP);
            var euros = new Money(100, Currency.EUR);

            Assert.Throws<IncompatibleCurrenciesException>(() => pounds + euros);
            Assert.Throws<IncompatibleCurrenciesException>(() => pounds - euros);
        }

        [Fact]
        public void AddingADecimaAmountlToMoneyIncreasesTheValue()
        {
            var money = new Money(100, Currency.GBP);
            decimal amount = 100m;

            var expected = new Money(200, Currency.GBP);
            var actual = money + amount;

            Assert.Equal<Money>(expected, actual);
        }

        [Fact]
        public void NegativeMoneyIsAllowed()
        {
            var negative = new Money(-100, Currency.GBP);
            var positive = new Money(100, Currency.GBP);

            var actual = negative + positive;
            var expected = new Money(0, Currency.GBP);

            Assert.Equal<Money>(actual, expected);
        }

        [Fact]
        public void YouCannotMultiplyMoneyByANegativeNumber()
        {
            var money = new Money(100, Currency.GBP);
            var multiplier = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => money * multiplier);
        }

        [Fact]
        public void YouCanPushMoneyToZero()
        {
            var money = new Money(100, Currency.GBP);

            Assert.Equal(Money.Zero(Currency.GBP), money.ToZero());
        }

        [Fact]
        public void YouCanIncreaseTheValueOfMoney()
        {
            var money = new Money(100, Currency.GBP);
            decimal multiplier = 10m;

            var expected = new Money(110, Currency.GBP);
            var actual = money.Increase(multiplier);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void YouCanDiscountTheValueOfMoney()
        {
            var money = new Money(100, Currency.GBP);
            decimal multiplier = 10m;

            var expected = new Money(90, Currency.GBP);
            var actual = money.Discount(multiplier);

            Assert.Equal(expected, actual);
        }
    }
}
