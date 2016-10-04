namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Globalization;
    using System.Reflection;

    public class PointFConverter : TypeConverter
    {
        public PointFConverter()
        {
            
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (!(sourceType == typeof(string)))
            {
                return base.CanConvertFrom(context, sourceType);
            }
            return true;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (!(destinationType == typeof(InstanceDescriptor)))
            {
                return base.CanConvertTo(context, destinationType);
            }
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, culture, value);
            }
            string str = ((string) value).Trim();
            if (str.Length == 0)
            {
                return null;
            }
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            char ch = culture.TextInfo.ListSeparator[0];
            string[] strArray = str.Split(new char[] { ch });
            float[] numArray = new float[strArray.Length];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (float) converter.ConvertFromString(context, culture, strArray[i]);
            }
            if (numArray.Length != 2)
            {
                throw new ArgumentException("格式不正确！");
            }
            return new PointF(numArray[0], numArray[1]);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if ((destinationType == typeof(string)) && (value is PointF))
            {
                PointF tf2 = (PointF) value;
                if (culture == null)
                {
                    culture = CultureInfo.CurrentCulture;
                }
                string separator = culture.TextInfo.ListSeparator + " ";
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
                string[] strArray = new string[] { converter.ConvertToString(context, culture, tf2.X), converter.ConvertToString(context, culture, tf2.Y) };
                return string.Join(separator, strArray);
            }
            if ((destinationType == typeof(InstanceDescriptor)) && (value is SizeF))
            {
                PointF tf = (PointF) value;
                ConstructorInfo constructor = typeof(PointF).GetConstructor(new Type[] { typeof(float), typeof(float) });
                if (constructor != null)
                {
                    return new InstanceDescriptor(constructor, new object[] { tf.X, tf.Y });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            return new PointF((float) propertyValues["X"], (float) propertyValues["Y"]);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(PointF), attributes).Sort(new string[] { "X", "Y" });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}

