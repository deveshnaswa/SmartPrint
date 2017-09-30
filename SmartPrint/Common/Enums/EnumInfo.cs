using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;


namespace SmartPrint.Common.Enums
{
    public static class EnumInfo
    {
        public static Dictionary<int,string> GetList<TEnum>()
            where TEnum : struct
        {
            var valuesAndDescriptions = new Dictionary<int, string>();
            var enumType = typeof(TEnum);
            var enumOptions = Enum.GetValues(enumType);
            foreach (var value in enumOptions)
            {
                MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();

                // we can then attempt to retrieve the    
                // description attribute from the member info    
                var descriptionAttribute =
                    memberInfo.GetCustomAttribute<DescriptionAttribute>();

                // if we find the attribute we can access its values    
                if (descriptionAttribute != null)
                {
                    valuesAndDescriptions.Add((int)value,
                        descriptionAttribute.Description);
                }
                else
                {
                    valuesAndDescriptions.Add((int)value, value.ToString());
                }
            }
            return valuesAndDescriptions;
        }

        public static SelectList GetSelectListFromEnum<TEnum>() where TEnum : struct
        {
            return new SelectList(GetList<TEnum>(), "Key", "Value");
        }
    }

}