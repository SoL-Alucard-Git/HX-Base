namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;

    public static class FormulaHelper
    {
        public static Aisino.Framework.Plugin.Core.ExcelXml.Formula Formula(string string_0)
        {
            Aisino.Framework.Plugin.Core.ExcelXml.Formula formula = new Aisino.Framework.Plugin.Core.ExcelXml.Formula();
            formula.Add(string_0).EmptyGroup();
            return formula;
        }

        public static Aisino.Framework.Plugin.Core.ExcelXml.Formula Formula(string string_0, Aisino.Framework.Plugin.Core.ExcelXml.Formula formula_0)
        {
            Aisino.Framework.Plugin.Core.ExcelXml.Formula formula = new Aisino.Framework.Plugin.Core.ExcelXml.Formula();
            formula.Add(string_0).StartGroup().Add(formula_0).EndGroup();
            return formula;
        }

        public static Aisino.Framework.Plugin.Core.ExcelXml.Formula Formula(string string_0, Range range_0)
        {
            Aisino.Framework.Plugin.Core.ExcelXml.Formula formula = new Aisino.Framework.Plugin.Core.ExcelXml.Formula();
            formula.Add(string_0).StartGroup().Add(range_0).EndGroup();
            return formula;
        }

        public static Aisino.Framework.Plugin.Core.ExcelXml.Formula Formula(string string_0, string string_1)
        {
            Aisino.Framework.Plugin.Core.ExcelXml.Formula formula = new Aisino.Framework.Plugin.Core.ExcelXml.Formula();
            formula.Add(string_0).StartGroup().Add(string_1).EndGroup();
            return formula;
        }

        public static Aisino.Framework.Plugin.Core.ExcelXml.Formula Formula(string string_0, Range range_0, Predicate<Cell> cellCompare)
        {
            Aisino.Framework.Plugin.Core.ExcelXml.Formula formula = new Aisino.Framework.Plugin.Core.ExcelXml.Formula();
            formula.Add(string_0).StartGroup().Add(range_0, cellCompare).EndGroup();
            return formula;
        }
    }
}

