namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Drawing;

    public interface IBorderOptions
    {
        System.Drawing.Color Color { get; set; }

        Borderline LineStyle { get; set; }

        BorderSides Sides { get; set; }

        int Weight { get; set; }
    }
}

