namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Drawing;

    public interface IInteriorOptions
    {
        System.Drawing.Color Color { get; set; }

        Aisino.Framework.Plugin.Core.ExcelXml.Pattern Pattern { get; set; }

        System.Drawing.Color PatternColor { get; set; }
    }
}

