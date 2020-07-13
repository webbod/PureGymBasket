using System;
using System.Collections.Generic;
using System.Linq;

namespace PureGym.Common
{
    public static class MoneyExtensions
    {
        /// <returns>The total value of the list</returns>
        public static Money Sum(this IEnumerable<Money> source)
        {
            if(source == null || !source.Any())
            {
                return new Money(0);
            }

            var output = source.First();

            foreach(var item in source.Skip(1))
            {
                output += item;
            }

            return output;
        }

        ///<param name="selector">a function that calculates the value of each item</param>
        /// <returns>The total value of the list</returns>
        public static Money Sum<TItemType>(this IEnumerable<TItemType> source, Func<TItemType, Money> selector)
        {
            if (source == null || !source.Any())
            {
                return new Money(0);
            }

            var output = selector(source.First());

            foreach (var item in source.Skip(1))
            {
                var value = selector(item);
                output += value;
            }

            var l = new List<int>();
            return output;
        }

    }
}
