using System;
using System.ComponentModel;

namespace PureGym.Common.Enumerations
{
    public static class Extensions
    {

        /// <returns>Description of the enum</returns>
        public static string GetDescription(this Enum value)
        {
            var attr = value.GetAttributeOfType<DescriptionAttribute>();
            return attr != null ? attr.Description : SharedStrings.Undefined;
        }

        /// <returns>Attribute on the enum</returns>
        public static TAttributeType GetAttributeOfType<TAttributeType>(this Enum enumVal) 
            where TAttributeType : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(TAttributeType), false);
            return (attributes.Length > 0) ? (TAttributeType)attributes[0] : null;
        }
    }
}
