namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;

    public interface IAlignmentOptions
    {
        HorizontalAlignment Horizontal { get; set; }

        int Indent { get; set; }

        int Rotate { get; set; }

        bool ShrinkToFit { get; set; }

        VerticalAlignment Vertical { get; set; }

        bool WrapText { get; set; }
    }
}

