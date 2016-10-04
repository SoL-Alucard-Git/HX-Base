namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Drawing;

    public interface IFontOptions
    {
        bool Bold { get; set; }

        System.Drawing.Color Color { get; set; }

        bool Italic { get; set; }

        string Name { get; set; }

        int Size { get; set; }

        bool Strikeout { get; set; }

        bool Underline { get; set; }
    }
}

