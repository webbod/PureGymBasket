using System;

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
            if (value == null || value == default)
            {
                throw new ArgumentNullException();
            }
        }

        public static bool CheckIfValueIsNotNull(object value)
        {
            return !(value == null || value == default);
        }
        #endregion
    }
}
