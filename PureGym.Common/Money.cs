using Newtonsoft.Json;
using PureGym.Common.Enumerations;
using PureGym.Common.Exceptions;
using System;

namespace PureGym.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    /// <summary>
    /// Money represents a value in a specific currency
    /// You can add, subtract Money
    /// You can discount or increase the value of Money by a percentage
    /// </summary>
    public class Money : IComparable
    {
        [JsonProperty]
        public Decimal Value { get; private set; }

        [JsonProperty]
        public Currency Currency { get; private set; }

        public Money(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }

        public Money ToZero()
        {
            Value = 0m;
            return this;
        }

        public static Money Zero(Currency currency)
        {
            return new Money(0m, currency);
        }

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Money Discount(decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
            {
                throw new ArgumentOutOfRangeException($"{percentage} {SharedStrings.OutOfRangeOf} {nameof(Discount)}");
            }

            Value -= Value * percentage / 100;

            return this;
        }

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Money Increase(decimal percentage)
        {
            if (percentage < 0)
            {
                throw new ArgumentOutOfRangeException($"{percentage} {SharedStrings.OutOfRangeOf} {nameof(Increase)}");
            }

            Value += Value * percentage / 100;

            return this;
        }

        public override string ToString()
        {
            var currencyLayout = Currency.GetDescription() ?? "{Currency} ";
            return $"{currencyLayout} {Math.Round(Value, 2):F2}";
        }
        
        #region Operators

        public static Money operator +(Money a, Money b)
        {
            CheckCurrenciesAreCompatible(a, b);
            return new Money(a.Value + b.Value, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            CheckCurrenciesAreCompatible(a, b);
            return new Money(a.Value - b.Value, a.Currency);
        }

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Money operator *(Money a, decimal multiplier)
        {
            if (multiplier < 0) { throw new ArgumentOutOfRangeException($"{multiplier} {SharedStrings.WasNegative}"); }
            return new Money(a.Value * multiplier, a.Currency);
        }

        public static Money operator *(decimal multiplier, Money a)
        {
            return a * multiplier;
        }

        public static Money operator +(Money a, decimal b)
        {
            a.Value += b;
            return a;
        }

        public static Money operator +(decimal a, Money b)
        {
            b.Value += a;
            return b;
        }

        public static Money operator -(Money a, decimal b)
        {
            a.Value -= b;
            return a;
        }

        public static Money operator -(decimal a, Money b)
        {
            b.Value = a - b.Value;
            return b;
        }

        #endregion

        #region Comparators

        public static bool CurrenciesMatch(Money a, Money b)
        {
            return a.Currency == b.Currency;
        }

        public static bool operator <(Money a, Money b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator >(Money a, Money b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator >=(Money a, Money b)
        {
            return a.CompareTo(b) >= 0;
        }

        public static bool operator <=(Money a, Money b)
        {
            return a.CompareTo(b) <= 0;
        }
        
        public static bool operator ==(Money a, Money b)
        {
            return a.CompareTo(b) == 0;
        }

        public static bool operator !=(Money a, Money b)
        {
            return a.CompareTo(b) != 0;
        }

        public static bool operator <(Money a, decimal b)
        {
            return a.Value.CompareTo(b) < 0;
        }

        public static bool operator >(Money a, decimal b)
        {
            return a.Value.CompareTo(b) > 0;
        }

        public static bool operator >=(Money a, decimal b)
        {
            return a.Value.CompareTo(b) >= 0;
        }

        public static bool operator <=(Money a, decimal b)
        {
            return a.Value.CompareTo(b) <= 0;
        }

        public static bool operator ==(Money a, decimal b)
        {
            return a.Value.CompareTo(b) == 0;
        }

        public static bool operator !=(Money a, decimal b)
        {
            return a.Value.CompareTo(b) != 0;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="IncompatibleCurrenciesException"></exception>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var otherMoney = obj as Money;

            if (otherMoney == null)
            {
                throw new ArgumentException($"{nameof(obj)} {SharedStrings.IsNot} {nameof(Money)}");
            }

            if (Currency != otherMoney.Currency)
            {
                throw new IncompatibleCurrenciesException($"{SharedStrings.IsIncompatibleWith} {nameof(obj)}");
            }

            return Value.CompareTo(otherMoney.Value);
        }

        #endregion

        #region CheckCurrenciesAreCompatible

        public static void CheckCurrenciesAreCompatible(Currency a, Currency b)
        {
            if (a != b)
            {
                throw new IncompatibleCurrenciesException($"{a} {SharedStrings.IsIncompatibleWith} {b}");
            }
        }

        public static void CheckCurrenciesAreCompatible(Money a, Money b)
        {
            CheckCurrenciesAreCompatible(a.Currency, b.Currency);
        }

        public static void CheckCurrenciesAreCompatible(Money a, Currency b)
        {
            CheckCurrenciesAreCompatible(a.Currency, b);
        }

        #endregion

    }
}
