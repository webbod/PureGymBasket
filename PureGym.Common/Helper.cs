using System;
using System.Linq;

namespace PureGym.Common
{
    public static class Helper
    {
        #region NULL checkers
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckIfValueIsNull(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException();
            }
        }

        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckIfValueIsNull(object value, string name)
        {
            if (value == null || value.Equals(default))
            {
                throw new ArgumentNullException();
            }
        }

        /// <returns>false if it is null</returns>
        public static bool CheckIfValueIsNotNull<T>(T value)
        {
            return !(value == null || value.Equals(default(T)));
        }
        #endregion

        public static string Combine(object first, object second)
        {
            return $"{first}{SharedStrings.Seperator}{second}";
        }

        public static (string first, string last) Seperate(string encoded)
        {
            if (!encoded.Contains(SharedStrings.Seperator))
            {
                return (encoded, string.Empty);
            }

            var parts = encoded.Split(SharedStrings.Seperator, StringSplitOptions.RemoveEmptyEntries);

            return (parts.First(), parts.Last());
        }
    }
}
