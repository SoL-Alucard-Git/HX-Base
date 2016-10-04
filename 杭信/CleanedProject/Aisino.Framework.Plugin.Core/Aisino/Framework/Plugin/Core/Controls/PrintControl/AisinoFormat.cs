namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Text;

    public class AisinoFormat : IFormatProvider, ICustomFormatter
    {
        public AisinoFormat()
        {
            
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return string.Format("{0}", arg);
            }
            if (!format.StartsWith("A"))
            {
                if (arg is IFormattable)
                {
                    return ((IFormattable) arg).ToString(format, formatProvider);
                }
                if (arg != null)
                {
                    return arg.ToString();
                }
            }
            int result = 0;
            char ch = ' ';
            if (format.Length > 1)
            {
                int.TryParse(format[1].ToString(), out result);
            }
            if (format.Length > 2)
            {
                ch = format[2];
            }
            if (result <= 0)
            {
                return arg.ToString();
            }
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (char ch3 in arg.ToString())
            {
                num++;
                builder.Append(ch3);
                if (num >= result)
                {
                    num = 0;
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            return null;
        }
    }
}

