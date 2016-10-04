namespace ns14
{
    using Aisino.Framework.Plugin.Core.ExcelXml;
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    internal static class Class129
    {
        private static Regex regex_0;
        private static Regex regex_1;
        private static Regex regex_2;
        private static Regex regex_3;
        private static Regex regex_4;

        static Class129()
        {
            
            regex_0 = new Regex(@"^((\[(?<File>[\w\.]+)\])?('?(?<Sheet>.+)'?!))?R(?<RowStart>\d+)?C(?<ColStart>\d+)?(:R(?<RowEnd>\d+)?C(?<ColEnd>\d+)?)?$");
            regex_1 = new Regex(@"^((\[(?<File>[\w\.]+)\])?('?(?<Sheet>.+)'?!))?R(\[(?<RowStart>[\-0-9]+)\])?C(\[(?<ColStart>[\-0-9]+)\])?(:R(\[(?<RowEnd>[\-0-9]+)\])?C(\[(?<ColEnd>[\-0-9]+)\])?)?$");
            regex_2 = new Regex(@"^(?<FunctionName>[\w\+\-]+)\((?<Parameters>.*)\)$");
            regex_3 = new Regex(@"^('?(?<Sheet>.+)'?!)?R(?<RowStart>\d+)(:R(?<RowEnd>\d+))?$");
            regex_4 = new Regex(@"^('?(?<Sheet>.+)'?!)?C(?<ColStart>\d+)(:C(?<ColEnd>\d+))?$");
        }

        private static Cell smethod_0(Worksheet worksheet_0, Row row_0, bool bool_0, int int_0, int int_1)
        {
            if (bool_0)
            {
                int_1 = row_0.int_0 + int_1;
                int_0 = row_0.int_0 + int_0;
            }
            return worksheet_0[int_1, int_0];
        }

        private static void smethod_1(object object_0, Formula formula_0, string string_0)
        {
            Match match;
            switch (smethod_2(string_0, out match))
            {
                case ((Enum16) 0):
                    formula_0.Add(string_0);
                    return;

                case ((Enum16) 1):
                {
                    string str = match.Groups["FunctionName"].Value;
                    Formula formula = new Formula();
                    formula.Add(str).StartGroup();
                    foreach (string str2 in match.Groups["Parameters"].Value.Split(new char[] { ',' }))
                    {
                        smethod_1(object_0, formula, str2);
                    }
                    formula.EndGroup();
                    formula_0.Add(formula);
                    return;
                }
                case ((Enum16) 2):
                case ((Enum16) 3):
                {
                    Range range = new Range(string_0);
                    formula_0.Add(range);
                    return;
                }
            }
        }

        internal static Enum16 smethod_2(string string_0, out Match match_0)
        {
            Match match = regex_2.Match(string_0);
            if (match.Success)
            {
                match_0 = match;
                return (Enum16) 1;
            }
            Match match3 = regex_0.Match(string_0);
            if (match3.Success)
            {
                match_0 = match3;
                return (Enum16) 2;
            }
            Match match2 = regex_1.Match(string_0);
            if (match2.Success)
            {
                match_0 = match2;
                return (Enum16) 3;
            }
            match_0 = null;
            return (Enum16) 0;
        }

        internal static void smethod_3(Cell cell_0, string string_0)
        {
            if (string_0[0] != '=')
            {
                cell_0.Value = string_0;
                cell_0.contentType_0 = ContentType.UnresolvedValue;
            }
            string_0 = string_0.Substring(1);
            Formula formula = new Formula();
            smethod_1(cell_0, formula, string_0);
            if (formula.list_0[0].ParameterType == ParameterType.Formula)
            {
                cell_0.Value = formula.list_0[0].Value as Formula;
                cell_0.contentType_0 = ContentType.Formula;
            }
            else
            {
                cell_0.Value = formula;
                cell_0.contentType_0 = ContentType.Formula;
            }
        }

        internal static bool smethod_4(Cell object_0, Match match_0, out Range range_0, bool bool_0)
        {
            Worksheet worksheet;
            range_0 = null;
            if (match_0.Groups["File"].Success)
            {
                return false;
            }
            if (match_0.Groups["Sheet"].Success)
            {
                string str = match_0.Groups["Sheet"].Value;
                if (str.Right(1) == "'")
                {
                    str = str.Left(str.Length - 1);
                }
                worksheet = object_0.vmethod_0()[str];
            }
            else
            {
                worksheet = object_0.row_0.worksheet_0;
            }
            if (worksheet == null)
            {
                return false;
            }
            int result = 0;
            int num4 = 0;
            if ((match_0.Groups["RowStart"].Success && int.TryParse(match_0.Groups["RowStart"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result)) && !bool_0)
            {
                result--;
            }
            if ((match_0.Groups["ColStart"].Success && int.TryParse(match_0.Groups["ColStart"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num4)) && !bool_0)
            {
                num4--;
            }
            Cell cell = null;
            Cell cell2 = smethod_0(worksheet, object_0.row_0, bool_0, result, num4);
            if (match_0.Groups["RowEnd"].Success)
            {
                int num2 = 0;
                int num = 0;
                if ((match_0.Groups["RowEnd"].Success && int.TryParse(match_0.Groups["RowEnd"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2)) && !bool_0)
                {
                    num2--;
                }
                if ((match_0.Groups["ColEnd"].Success && int.TryParse(match_0.Groups["ColEnd"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num)) && !bool_0)
                {
                    num--;
                }
                cell = smethod_0(worksheet, object_0.row_0, bool_0, num2, num);
            }
            range_0 = new Range(cell2, cell);
            return true;
        }

        internal static void smethod_5(Worksheet worksheet_0, string string_0)
        {
            foreach (string str in string_0.Split(new char[] { ',' }))
            {
                Match match = regex_3.Match(str);
                if (match.Success)
                {
                    int num2;
                    if (int.TryParse(match.Groups["RowStart"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
                    {
                        int result = 0;
                        if (!match.Groups["RowEnd"].Success || !int.TryParse(match.Groups["RowEnd"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
                        {
                            result = num2;
                        }
                        worksheet_0.PrintOptions.SetTitleRows(num2, result);
                    }
                }
                else
                {
                    int num4;
                    match = regex_4.Match(str);
                    if (match.Success && int.TryParse(match.Groups["ColStart"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num4))
                    {
                        int num5 = 0;
                        if (!match.Groups["ColEnd"].Success || !int.TryParse(match.Groups["ColEnd"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num5))
                        {
                            num5 = num4;
                        }
                        worksheet_0.PrintOptions.SetTitleColumns(num4, num5);
                    }
                }
            }
        }
    }
}

