namespace ns14
{
    using Aisino.Framework.Plugin.Core.ExcelXml;
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;

    internal class Class139
    {
        internal Range range_0;
        internal string string_0;
        internal Worksheet worksheet_0;

        internal Class139(Range range_1, string string_1, Worksheet worksheet_1)
        {
            
            if (range_1 == null)
            {
                throw new ArgumentNullException("range");
            }
            if (string_1.IsNullOrEmpty())
            {
                throw new ArgumentNullException("name");
            }
            this.worksheet_0 = worksheet_1;
            this.range_0 = range_1;
            this.string_0 = string_1;
        }
    }
}

