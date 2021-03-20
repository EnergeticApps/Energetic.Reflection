using Energetic.Reflection;
using System.ComponentModel;
using System.Linq;

namespace System
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum enumValue)
            where TEnum : Enum
        {
            var attribute = enumValue.GetAttribute<TEnum, DescriptionAttribute>();

            if (attribute is null)
                throw MissingAttributeException.MissingFromField<DescriptionAttribute>(nameof(enumValue), typeof(TEnum));

            return attribute.Description;
        }

        public static TAttribute? GetAttribute<TEnum, TAttribute>(this TEnum enumValue)
            where TEnum : Enum
            where TAttribute : Attribute
        {
            var type = typeof(TEnum);
            var memberInfos = type.GetMember(enumValue.ToString());

            if (memberInfos.Length > 1)
                throw new InvalidOperationException("The enum value passed is made up of multiple members. This function only works on single members.");

            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == type);
            var attributes = enumValueMemberInfo.GetCustomAttributes(typeof(TAttribute), false);

            return attributes.Length == 1 ? attributes?[0] as TAttribute : null;
        }
    }
}