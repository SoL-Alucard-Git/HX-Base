namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;

    public interface IStyle
    {
        IAlignmentOptions Alignment { get; set; }

        IBorderOptions Border { get; set; }

        string CustomFormatString { get; set; }

        DisplayFormatType DisplayFormat { get; set; }

        IFontOptions Font { get; set; }

        IInteriorOptions Interior { get; set; }
    }
}

