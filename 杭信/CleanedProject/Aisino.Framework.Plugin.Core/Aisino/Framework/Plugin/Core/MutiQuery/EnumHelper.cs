namespace Aisino.Framework.Plugin.Core.MutiQuery
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    public static class EnumHelper
    {
        public static string GetDescription(ArithOperator enum_0)
        {
            if (enum_0 == 0)
            {
                throw new ArgumentNullException("value");
            }
            string name = enum_0.ToString();
            FieldInfo field = enum_0.GetType().GetField(name);
            Type attributeType = typeof(DescriptionAttribute);
            object[] customAttributes = field.GetCustomAttributes(attributeType, false);
            if ((customAttributes != null) && (customAttributes.Length > 0))
            {
                name = ((DescriptionAttribute) customAttributes[0]).Description;
            }
            return name;
        }

        public static IList ToList(Type type_0)
        {
            if (type_0 == null)
            {
                throw new ArgumentNullException("type");
            }
            if (!type_0.IsEnum)
            {
                throw new ArgumentException("Type provided must be an Enum.", "type");
            }
            ArrayList list = new ArrayList();
            foreach (ArithOperator enum2 in ArithOperator.GetValues(type_0))
            {
                list.Add(new KeyValuePair<Enum, string>(enum2, GetDescription(enum2)));
            }
            return list;
        }
    }
}

