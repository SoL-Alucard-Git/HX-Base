namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using ns14;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class Worksheet : Styles, IEnumerable<Cell>, IEnumerable
    {
        internal bool bool_0;
        internal bool bool_1;
        internal ExcelXmlWorkbook excelXmlWorkbook_0;
        internal int int_0;
        [CompilerGenerated]
        private int int_1;
        [CompilerGenerated]
        private int int_2;
        [CompilerGenerated]
        private int int_3;
        private List<Column> list_0;
        internal List<Row> list_1;
        internal List<Aisino.Framework.Plugin.Core.ExcelXml.Range> list_2;
        [CompilerGenerated]
        private Aisino.Framework.Plugin.Core.ExcelXml.PrintOptions printOptions_0;
        private string string_1;

        internal Worksheet(ExcelXmlWorkbook excelXmlWorkbook_1)
        {
            
            if (excelXmlWorkbook_1 == null)
            {
                throw new ArgumentNullException("parent");
            }
            this.excelXmlWorkbook_0 = excelXmlWorkbook_1;
            this.PrintOptions = new Aisino.Framework.Plugin.Core.ExcelXml.PrintOptions();
            this.PrintOptions.Layout = PageLayout.None;
            this.PrintOptions.Orientation = PageOrientation.None;
            this.list_1 = new List<Row>();
            this.list_0 = new List<Column>();
            this.list_2 = new List<Aisino.Framework.Plugin.Core.ExcelXml.Range>();
            this.TabColor = -1;
            this.PrintOptions.int_1 = 1;
            this.PrintOptions.int_2 = 1;
            this.PrintOptions.int_0 = 100;
            this.PrintOptions.ResetMargins();
        }

        public void AddNamedRange(Aisino.Framework.Plugin.Core.ExcelXml.Range range_0, string string_2)
        {
            if (string_2.IsNullOrEmpty())
            {
                throw new ArgumentNullException("name");
            }
            if (Aisino.Framework.Plugin.Core.ExcelXml.Range.smethod_2(string_2))
            {
                throw new ArgumentException(string_2 + "is a excel internal range name");
            }
            this.vmethod_0().method_10(range_0, string_2, this);
        }

        public Row AddRow()
        {
            return this[this.list_1.Count];
        }

        public Column Columns(int int_4)
        {
            if (int_4 < 0)
            {
                throw new ArgumentOutOfRangeException("colIndex");
            }
            if ((int_4 + 1) > this.list_0.Count)
            {
                for (int i = this.list_0.Count; i <= int_4; i++)
                {
                    this.list_0.Add(new Column(this));
                }
            }
            return this.list_0[int_4];
        }

        public void Delete()
        {
            this.vmethod_0().DeleteSheet(this);
        }

        public void DeleteColumn(int int_4)
        {
            this.DeleteColumns(int_4, 1, true);
        }

        public void DeleteColumn(int int_4, bool bool_2)
        {
            this.DeleteColumns(int_4, 1, bool_2);
        }

        public void DeleteColumns(int int_4, int int_5)
        {
            this.DeleteColumns(int_4, int_5, true);
        }

        public void DeleteColumns(int int_4, int int_5, bool bool_2)
        {
            if (int_4 >= 0)
            {
                if (bool_2 && (int_4 < this.list_0.Count))
                {
                    for (int i = int_4; i < (int_4 + int_5); i++)
                    {
                        if (int_4 >= this.list_0.Count)
                        {
                            break;
                        }
                        this.list_0.RemoveAt(int_4);
                    }
                }
                if (int_4 <= this.int_0)
                {
                    foreach (Row row in this.list_1)
                    {
                        if (int_4 < row.list_0.Count)
                        {
                            row.DeleteCells(int_4, int_5, bool_2);
                        }
                    }
                }
            }
        }

        public void DeleteRow(Row row_0)
        {
            Row row = row_0;
            if (row != null)
            {
                this.DeleteRow(this.list_1.FindIndex(r => r == row));
            }
        }

        public void DeleteRow(int int_4)
        {
            this.DeleteRows(int_4, 1);
        }

        public void DeleteRow(Row row_0, bool bool_2)
        {
            Row row = row_0;
            if (row != null)
            {
                this.DeleteRow(this.list_1.FindIndex(r => r == row), bool_2);
            }
        }

        public void DeleteRow(int int_4, bool bool_2)
        {
            if (bool_2)
            {
                this.DeleteRow(int_4);
            }
            else if ((int_4 >= 0) && (int_4 < this.list_1.Count))
            {
                foreach (Cell cell in this.list_1[int_4].list_0)
                {
                    cell.Empty();
                }
            }
        }

        public void DeleteRows(Row row_0, int int_4)
        {
            Row row = row_0;
            if (row != null)
            {
                this.DeleteRows(this.list_1.FindIndex(r => r == row), int_4);
            }
        }

        public void DeleteRows(int int_4, int int_5)
        {
            if ((int_5 >= 0) && ((int_4 >= 0) && (int_4 < this.list_1.Count)))
            {
                if ((int_4 + int_5) > this.list_1.Count)
                {
                    int_5 = this.list_1.Count - int_4;
                }
                for (int i = int_4; i < (int_4 + int_5); i++)
                {
                    this.list_1[int_4].method_7();
                    this.list_1.RemoveAt(int_4);
                }
                this.method_14(int_4);
            }
        }

        public void DeleteRows(Row row_0, int int_4, bool bool_2)
        {
            Row row = row_0;
            if (row != null)
            {
                this.DeleteRows(this.list_1.FindIndex(r => r == row), int_4, bool_2);
            }
        }

        public void DeleteRows(int int_4, int int_5, bool bool_2)
        {
            if (bool_2)
            {
                this.DeleteRows(int_4, int_5);
            }
            else if ((int_4 >= 0) && (int_4 < this.list_1.Count))
            {
                if ((int_4 + int_5) > this.list_1.Count)
                {
                    int_5 = this.list_1.Count - int_4;
                }
                for (int i = int_4; i < (int_4 + int_5); i++)
                {
                    foreach (Cell cell in this.list_1[i].list_0)
                    {
                        cell.Empty();
                    }
                }
            }
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            int iteratorVariable0 = 0;
            while (true)
            {
                if (iteratorVariable0 >= this.list_1.Count)
                {
                    break;
                }
                for (int i = 0; i <= this.int_0; i++)
                {
                    yield return this[i, iteratorVariable0];
                }
                iteratorVariable0++;
            }
        }

        public void InsertColumnAfter(int int_4)
        {
            this.InsertColumnsAfter(int_4, 1);
        }

        public void InsertColumnBefore(int int_4)
        {
            this.InsertColumnsBefore(int_4, 1);
        }

        public void InsertColumnsAfter(int int_4, int int_5)
        {
            if (int_4 >= 0)
            {
                if (int_4 < (this.list_0.Count - 1))
                {
                    Column item = new Column(this);
                    this.list_0.Insert(int_4 + 1, item);
                }
                if (int_4 <= (this.int_0 - 1))
                {
                    foreach (Row row in this.list_1)
                    {
                        if (int_4 < row.list_0.Count)
                        {
                            row.InsertCellsAfter(int_4, int_5);
                        }
                    }
                }
            }
        }

        public void InsertColumnsBefore(int int_4, int int_5)
        {
            if (int_4 >= 0)
            {
                if (int_4 < this.list_0.Count)
                {
                    Column item = new Column(this);
                    this.list_0.Insert(int_4, item);
                }
                if (int_4 <= this.int_0)
                {
                    foreach (Row row in this.list_1)
                    {
                        if (int_4 < row.list_0.Count)
                        {
                            row.InsertCellsBefore(int_4, int_5);
                        }
                    }
                }
            }
        }

        public Row InsertRowAfter(Row row_0)
        {
            Row row = row_0;
            return this.InsertRowAfter(this.list_1.FindIndex(r => r == row));
        }

        public Row InsertRowAfter(int int_4)
        {
            if (int_4 < 0)
            {
                return this.AddRow();
            }
            if (int_4 >= (this.list_1.Count - 1))
            {
                return this[int_4 + 1];
            }
            this.InsertRowsAfter(int_4, 1);
            return this.list_1[int_4];
        }

        public Row InsertRowBefore(Row row_0)
        {
            Row row = row_0;
            return this.InsertRowBefore(this.list_1.FindIndex(r => r == row));
        }

        public Row InsertRowBefore(int int_4)
        {
            if (int_4 < 0)
            {
                return this.AddRow();
            }
            if (int_4 >= this.list_1.Count)
            {
                return this[int_4];
            }
            this.InsertRowsBefore(int_4, 1);
            return this.list_1[int_4];
        }

        public void InsertRowsAfter(Row row_0, int int_4)
        {
            Row row = row_0;
            if (row != null)
            {
                this.InsertRowsAfter(this.list_1.FindIndex(r => r == row), int_4);
            }
        }

        public void InsertRowsAfter(int int_4, int int_5)
        {
            if (((int_5 >= 0) && (int_4 >= 0)) && (int_4 < (this.list_1.Count - 1)))
            {
                for (int i = int_4; i < (int_4 + int_5); i++)
                {
                    Row item = new Row(this, int_4);
                    this.list_1.Insert(int_4 + 1, item);
                }
                this.method_14(int_4);
            }
        }

        public void InsertRowsBefore(Row row_0, int int_4)
        {
            Row row = row_0;
            this.InsertRowsBefore(this.list_1.FindIndex(r => r == row), int_4);
        }

        public void InsertRowsBefore(int int_4, int int_5)
        {
            if (((int_5 >= 0) && (int_4 >= 0)) && (int_4 < this.list_1.Count))
            {
                for (int i = int_4; i < (int_4 + int_5); i++)
                {
                    Row item = new Row(this, int_4);
                    this.list_1.Insert(int_4, item);
                }
                this.method_14(int_4);
            }
        }

        private void method_10(XmlReader xmlReader_0)
        {
            bool isEmptyElement = xmlReader_0.IsEmptyElement;
            int count = this.list_1.Count;
            double num2 = -1.0;
            XmlStyle style = null;
            bool flag2 = false;
            foreach (XmlReaderAttributeItem item in xmlReader_0.GetAttributes())
            {
                if ((item.LocalName == "Height") && item.HasValue)
                {
                    item.Value.ParseToInt<double>(out num2);
                }
                if ((item.LocalName == "Index") && item.HasValue)
                {
                    item.Value.ParseToInt<int>(out count);
                    count--;
                }
                if ((item.LocalName == "StyleID") && item.HasValue)
                {
                    style = this.excelXmlWorkbook_0.method_6(item.Value);
                }
                if ((item.LocalName == "Hidden") && item.HasValue)
                {
                    flag2 = item.Value == "1";
                }
            }
            Row row = this.method_13(count);
            row.Hidden = flag2;
            if (num2 != -1.0)
            {
                row.Height = num2;
            }
            if (style != null)
            {
                row.Style = style;
            }
            if (!isEmptyElement)
            {
                while (xmlReader_0.Read())
                {
                    if ((xmlReader_0.Name == "Row") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                    {
                        return;
                    }
                    if ((xmlReader_0.NodeType == XmlNodeType.Element) && (xmlReader_0.Name == "Cell"))
                    {
                        this.method_11(xmlReader_0, row);
                    }
                }
            }
        }

        private void method_11(XmlReader xmlReader_0, Row row_0)
        {
            bool isEmptyElement = xmlReader_0.IsEmptyElement;
            int count = row_0.list_0.Count;
            int num2 = 0;
            int num3 = 0;
            XmlStyle style = null;
            string str = "";
            string str2 = "";
            foreach (XmlReaderAttributeItem item in xmlReader_0.GetAttributes())
            {
                if ((item.LocalName == "Index") && item.HasValue)
                {
                    item.Value.ParseToInt<int>(out count);
                    count--;
                }
                if ((item.LocalName == "StyleID") && item.HasValue)
                {
                    style = this.excelXmlWorkbook_0.method_6(item.Value);
                }
                if ((item.LocalName == "HRef") && item.HasValue)
                {
                    str2 = item.Value;
                }
                if ((item.LocalName == "Formula") && item.HasValue)
                {
                    str = item.Value;
                }
                if ((item.LocalName == "MergeAcross") && item.HasValue)
                {
                    item.Value.ParseToInt<int>(out num3);
                }
                if ((item.LocalName == "MergeDown") && item.HasValue)
                {
                    item.Value.ParseToInt<int>(out num2);
                }
            }
            Cell cell = this.method_12(count, row_0.int_0);
            if (style != null)
            {
                cell.Style = style;
            }
            if (!str2.IsNullOrEmpty())
            {
                cell.HRef = str2;
            }
            if (!str.IsNullOrEmpty())
            {
                Class129.smethod_3(cell, str);
            }
            else if (!isEmptyElement)
            {
                if ((num2 > 0) || (num3 > 0))
                {
                    cell.bool_0 = true;
                    Aisino.Framework.Plugin.Core.ExcelXml.Range range = new Aisino.Framework.Plugin.Core.ExcelXml.Range(cell, this.method_12(count + num3, row_0.int_0 + num2));
                    this.list_2.Add(range);
                    cell.ColumnSpan = range.ColumnCount;
                    cell.RowSpan = range.RowCount;
                }
                while (xmlReader_0.Read())
                {
                    if ((xmlReader_0.Name == "Cell") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                    {
                        break;
                    }
                    if (xmlReader_0.NodeType == XmlNodeType.Element)
                    {
                        if (xmlReader_0.Name == "Data")
                        {
                            smethod_2(xmlReader_0, cell);
                        }
                        else if (xmlReader_0.Name == "Comment")
                        {
                            smethod_3(xmlReader_0, cell);
                        }
                    }
                }
            }
        }

        internal Cell method_12(int int_4, int int_5)
        {
            if (int_4 < 0)
            {
                throw new ArgumentOutOfRangeException("colIndex");
            }
            if (int_5 < 0)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if ((int_5 + 1) > this.list_1.Count)
            {
                for (int i = this.list_1.Count; i <= int_5; i++)
                {
                    this.list_1.Add(new Row(this, i));
                }
            }
            if ((int_4 + 1) > this.list_1[int_5].list_0.Count)
            {
                for (int j = this.list_1[int_5].list_0.Count; j <= int_4; j++)
                {
                    this.list_1[int_5].list_0.Add(new Cell(this.list_1[int_5], j));
                }
            }
            this.int_0 = Math.Max(int_4, this.int_0);
            return this.list_1[int_5].list_0[int_4];
        }

        internal Row method_13(int int_4)
        {
            if (int_4 < 0)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if ((int_4 + 1) > this.list_1.Count)
            {
                for (int i = this.list_1.Count; i <= int_4; i++)
                {
                    this.list_1.Add(new Row(this, i));
                }
            }
            return this.list_1[int_4];
        }

        internal void method_14(int int_4)
        {
            for (int i = int_4; i < this.list_1.Count; i++)
            {
                this.list_1[i].int_0 = i;
            }
        }

        internal bool method_15(Cell cell_0)
        {
            foreach (Aisino.Framework.Plugin.Core.ExcelXml.Range range in this.list_2)
            {
                if (range.Contains(cell_0))
                {
                    return true;
                }
            }
            return false;
        }

        internal void method_16(XmlWriter xmlWriter_0)
        {
            xmlWriter_0.WriteStartElement("Worksheet");
            xmlWriter_0.WriteAttributeString("ss", "Name", null, this.Name);
            this.excelXmlWorkbook_0.method_14(xmlWriter_0, this);
            xmlWriter_0.WriteStartElement("Table");
            xmlWriter_0.WriteAttributeString("ss", "FullColumns", null, "1");
            xmlWriter_0.WriteAttributeString("ss", "FullRows", null, "1");
            if (!base.StyleID.IsNullOrEmpty() && (base.StyleID != "Default"))
            {
                xmlWriter_0.WriteAttributeString("ss", "StyleID", null, base.StyleID);
            }
            foreach (Column column in this.list_0)
            {
                column.method_0(xmlWriter_0);
            }
            foreach (Row row in this.list_1)
            {
                row.method_8(xmlWriter_0);
            }
            xmlWriter_0.WriteEndElement();
            this.method_18(xmlWriter_0);
            if (this.bool_0)
            {
                string str = this.vmethod_0().method_12(this);
                xmlWriter_0.WriteStartElement("", "AutoFilter", "urn:schemas-microsoft-com:office:excel");
                xmlWriter_0.WriteAttributeString("", "Range", null, str);
                xmlWriter_0.WriteEndElement();
            }
            xmlWriter_0.WriteEndElement();
        }

        private void method_17(XmlWriter xmlWriter_0)
        {
            string str;
            if ((this.FreezeLeftColumns > 0) && (this.FreezeTopRows > 0))
            {
                str = "3210";
            }
            else
            {
                str = (this.FreezeLeftColumns > 0) ? "31" : "32";
            }
            xmlWriter_0.WriteElementString("ActivePane", str[str.Length - 1].ToString());
            xmlWriter_0.WriteStartElement("Panes");
            foreach (char ch in str)
            {
                xmlWriter_0.WriteStartElement("Pane");
                xmlWriter_0.WriteElementString("Number", ch.ToString());
                xmlWriter_0.WriteEndElement();
            }
            xmlWriter_0.WriteEndElement();
        }

        private void method_18(XmlWriter xmlWriter_0)
        {
            xmlWriter_0.WriteStartElement("", "WorksheetOptions", "urn:schemas-microsoft-com:office:excel");
            this.PrintOptions.method_1(xmlWriter_0);
            xmlWriter_0.WriteElementString("Selected", "");
            if (this.TabColor != -1)
            {
                xmlWriter_0.WriteElementString("TabColor", this.TabColor.ToString(CultureInfo.InvariantCulture));
            }
            if ((this.FreezeLeftColumns > 0) || (this.FreezeTopRows > 0))
            {
                xmlWriter_0.WriteElementString("FreezePanes", "");
                xmlWriter_0.WriteElementString("FrozenNoSplit", "");
                if (this.FreezeTopRows > 0)
                {
                    xmlWriter_0.WriteElementString("SplitHorizontal", this.FreezeTopRows.ToString(CultureInfo.InvariantCulture));
                    xmlWriter_0.WriteElementString("TopRowBottomPane", this.FreezeTopRows.ToString(CultureInfo.InvariantCulture));
                }
                if (this.FreezeLeftColumns > 0)
                {
                    xmlWriter_0.WriteElementString("SplitVertical", this.FreezeLeftColumns.ToString(CultureInfo.InvariantCulture));
                    xmlWriter_0.WriteElementString("LeftColumnRightPane", this.FreezeLeftColumns.ToString(CultureInfo.InvariantCulture));
                }
                this.method_17(xmlWriter_0);
            }
            xmlWriter_0.WriteEndElement();
        }

        internal void method_5(XmlReader xmlReader_0)
        {
            foreach (XmlReaderAttributeItem item in xmlReader_0.GetAttributes())
            {
                if ((item.LocalName == "Name") && item.HasValue)
                {
                    this.Name = item.Value;
                }
                if ((item.LocalName == "StyleID") && item.HasValue)
                {
                    base.Style = this.excelXmlWorkbook_0.method_6(item.Value);
                }
            }
            while (xmlReader_0.Read())
            {
                string str;
                if ((xmlReader_0.Name == "Worksheet") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                {
                    break;
                }
                if ((xmlReader_0.NodeType == XmlNodeType.Element) && ((str = xmlReader_0.Name) != null))
                {
                    if (str != "Names")
                    {
                        if (!(str == "Table"))
                        {
                            if (str == "WorksheetOptions")
                            {
                                this.method_6(xmlReader_0);
                            }
                        }
                        else
                        {
                            this.method_9(xmlReader_0);
                        }
                    }
                    else
                    {
                        ExcelXmlWorkbook.smethod_0(xmlReader_0,this.vmethod_0().list_2, this);
                    }
                }
            }
        }

        private void method_6(XmlReader xmlReader_0)
        {
            if (!xmlReader_0.IsEmptyElement)
            {
                while (xmlReader_0.Read())
                {
                    string str;
                    if ((xmlReader_0.Name == "WorksheetOptions") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                    {
                        return;
                    }
                    if ((xmlReader_0.NodeType == XmlNodeType.Element) && ((str = xmlReader_0.Name) != null))
                    {
                        if (str == "PageSetup")
                        {
                            this.method_8(xmlReader_0);
                        }
                        else if (str == "FitToPage")
                        {
                            this.PrintOptions.bool_0 = true;
                        }
                        else if (str == "TabColorIndex")
                        {
                            if (!xmlReader_0.IsEmptyElement)
                            {
                                int num2;
                                xmlReader_0.Read();
                                if ((xmlReader_0.NodeType == XmlNodeType.Text) && xmlReader_0.Value.ParseToInt<int>(out num2))
                                {
                                    this.TabColor = num2;
                                }
                            }
                        }
                        else if (str == "Print")
                        {
                            this.method_7(xmlReader_0);
                        }
                        else if (!(str == "SplitHorizontal"))
                        {
                            if ((str == "SplitVertical") && !xmlReader_0.IsEmptyElement)
                            {
                                int num;
                                xmlReader_0.Read();
                                if ((xmlReader_0.NodeType == XmlNodeType.Text) && xmlReader_0.Value.ParseToInt<int>(out num))
                                {
                                    this.FreezeLeftColumns = num;
                                }
                            }
                        }
                        else if (!xmlReader_0.IsEmptyElement)
                        {
                            int num3;
                            xmlReader_0.Read();
                            if ((xmlReader_0.NodeType == XmlNodeType.Text) && xmlReader_0.Value.ParseToInt<int>(out num3))
                            {
                                this.FreezeTopRows = num3;
                            }
                        }
                    }
                }
            }
        }

        private void method_7(XmlReader xmlReader_0)
        {
            if (!xmlReader_0.IsEmptyElement)
            {
                while (xmlReader_0.Read())
                {
                    string str;
                    if ((xmlReader_0.Name == "Print") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                    {
                        return;
                    }
                    if ((xmlReader_0.NodeType == XmlNodeType.Element) && ((str = xmlReader_0.Name) != null))
                    {
                        if (str != "FitHeight")
                        {
                            if (!(str == "FitWidth"))
                            {
                                if ((str == "Scale") && !xmlReader_0.IsEmptyElement)
                                {
                                    int num2;
                                    xmlReader_0.Read();
                                    if ((xmlReader_0.NodeType == XmlNodeType.Text) && xmlReader_0.Value.ParseToInt<int>(out num2))
                                    {
                                        this.PrintOptions.int_0 = num2;
                                    }
                                }
                            }
                            else if (!xmlReader_0.IsEmptyElement)
                            {
                                int num;
                                xmlReader_0.Read();
                                if ((xmlReader_0.NodeType == XmlNodeType.Text) && xmlReader_0.Value.ParseToInt<int>(out num))
                                {
                                    this.PrintOptions.int_2 = num;
                                }
                            }
                        }
                        else if (!xmlReader_0.IsEmptyElement)
                        {
                            int num3;
                            xmlReader_0.Read();
                            if ((xmlReader_0.NodeType == XmlNodeType.Text) && xmlReader_0.Value.ParseToInt<int>(out num3))
                            {
                                this.PrintOptions.int_1 = num3;
                            }
                        }
                    }
                }
            }
        }

        private void method_8(XmlReader xmlReader_0)
        {
            if (!xmlReader_0.IsEmptyElement)
            {
                while (xmlReader_0.Read())
                {
                    string str;
                    if ((xmlReader_0.Name == "PageSetup") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                    {
                        return;
                    }
                    if ((xmlReader_0.NodeType == XmlNodeType.Element) && ((str = xmlReader_0.Name) != null))
                    {
                        if (str == "Layout")
                        {
                            XmlReaderAttributeItem item3 = xmlReader_0.GetSingleAttribute("Orientation", true);
                            if (item3 != null)
                            {
                                this.PrintOptions.Orientation = ObjectExtensions.ParseEnum<PageOrientation>(item3.Value);
                            }
                        }
                        else if (str == "Header")
                        {
                            double num3;
                            XmlReaderAttributeItem item4 = xmlReader_0.GetSingleAttribute("Margin", true);
                            if ((item4 != null) && item4.Value.ParseToInt<double>(out num3))
                            {
                                this.PrintOptions.double_4 = num3;
                            }
                        }
                        else if (str == "Footer")
                        {
                            double num;
                            XmlReaderAttributeItem item = xmlReader_0.GetSingleAttribute("Margin", true);
                            if ((item != null) && item.Value.ParseToInt<double>(out num))
                            {
                                this.PrintOptions.double_5 = num;
                            }
                        }
                        else if (str == "PageMargins")
                        {
                            foreach (XmlReaderAttributeItem item2 in xmlReader_0.GetAttributes())
                            {
                                double num2;
                                string str2;
                                if (item2.Value.ParseToInt<double>(out num2) && ((str2 = item2.LocalName) != null))
                                {
                                    if (str2 == "Bottom")
                                    {
                                        this.PrintOptions.double_3 = num2;
                                    }
                                    else if (str2 == "Left")
                                    {
                                        this.PrintOptions.double_0 = num2;
                                    }
                                    else if (!(str2 == "Right"))
                                    {
                                        if (str2 == "Top")
                                        {
                                            this.PrintOptions.double_2 = num2;
                                        }
                                    }
                                    else
                                    {
                                        this.PrintOptions.double_1 = num2;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void method_9(XmlReader xmlReader_0)
        {
            if (!xmlReader_0.IsEmptyElement)
            {
                int num = 0;
                while (xmlReader_0.Read())
                {
                    string str;
                    if ((xmlReader_0.Name == "Table") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                    {
                        break;
                    }
                    if ((xmlReader_0.NodeType == XmlNodeType.Element) && ((str = xmlReader_0.Name) != null))
                    {
                        if (!(str == "Column"))
                        {
                            if (str == "Row")
                            {
                                this.method_10(xmlReader_0);
                            }
                        }
                        else
                        {
                            double num2 = 0.0;
                            bool flag = false;
                            int num4 = 1;
                            XmlStyle style = null;
                            foreach (XmlReaderAttributeItem item in xmlReader_0.GetAttributes())
                            {
                                double num5;
                                if (((item.LocalName == "Width") && item.HasValue) && item.Value.ParseToInt<double>(out num5))
                                {
                                    num2 = num5;
                                }
                                if ((item.LocalName == "Hidden") && item.HasValue)
                                {
                                    flag = item.Value == "1";
                                }
                                if ((item.LocalName == "Index") && item.HasValue)
                                {
                                    item.Value.ParseToInt<int>(out num);
                                }
                                if ((item.LocalName == "Span") && item.HasValue)
                                {
                                    item.Value.ParseToInt<int>(out num4);
                                }
                                if ((item.LocalName == "StyleID") && item.HasValue)
                                {
                                    style = this.excelXmlWorkbook_0.method_6(item.Value);
                                }
                            }
                            for (int i = 1; i <= num4; i++)
                            {
                                this.Columns(num).Width = num2;
                                this.Columns(num).Hidden = flag;
                                if (style != null)
                                {
                                    this.Columns(num).Style = style;
                                }
                                num++;
                            }
                        }
                    }
                }
            }
        }

        public void RemovePrintArea()
        {
            this.bool_1 = false;
            this.vmethod_0().method_11("Print_Area", this);
        }

        internal static void smethod_2(XmlReader xmlReader_0, Cell cell_0)
        {
            if (xmlReader_0.IsEmptyElement)
            {
                cell_0.Value = "";
            }
            else
            {
                XmlReaderAttributeItem singleAttribute = xmlReader_0.GetSingleAttribute("Type");
                if (singleAttribute != null)
                {
                    xmlReader_0.Read();
                    if (xmlReader_0.NodeType != XmlNodeType.Text)
                    {
                        cell_0.Value = "";
                    }
                    else
                    {
                        string str = singleAttribute.Value;
                        if (str != null)
                        {
                            if (str == "String")
                            {
                                cell_0.Value = xmlReader_0.Value;
                            }
                            else if (str == "Number")
                            {
                                decimal num;
                                if (xmlReader_0.Value.ParseToInt<decimal>(out num))
                                {
                                    cell_0.Value = num;
                                }
                                else
                                {
                                    cell_0.Value = xmlReader_0.Value;
                                }
                            }
                            else if (str == "DateTime")
                            {
                                DateTime time;
                                if (DateTime.TryParseExact(xmlReader_0.Value, @"yyyy-MM-dd\Thh:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                                {
                                    cell_0.Value = time;
                                }
                                else
                                {
                                    cell_0.Value = xmlReader_0.Value;
                                }
                            }
                            else if (str == "Boolean")
                            {
                                cell_0.Value = xmlReader_0.Value == "1";
                            }
                        }
                    }
                }
            }
        }

        internal static void smethod_3(XmlReader xmlReader_0, Cell cell_0)
        {
            xmlReader_0.Read();
            if (xmlReader_0.LocalName == "Data")
            {
                string str = xmlReader_0.ReadInnerXml();
                if (!str.IsNullOrEmpty())
                {
                    cell_0.Comment = str;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        internal override ExcelXmlWorkbook vmethod_0()
        {
            return this.excelXmlWorkbook_0;
        }

        internal override void vmethod_1(Styles.Delegate36 delegate36_0)
        {
        }

        internal override Cell vmethod_2()
        {
            return null;
        }

        public int ColumnCount
        {
            get
            {
                return this.int_0;
            }
        }

        public int FreezeLeftColumns
        {
            [CompilerGenerated]
            get
            {
                return this.int_2;
            }
            [CompilerGenerated]
            set
            {
                this.int_2 = value;
            }
        }

        public int FreezeTopRows
        {
            [CompilerGenerated]
            get
            {
                return this.int_1;
            }
            [CompilerGenerated]
            set
            {
                this.int_1 = value;
            }
        }

        public bool IsPrintAreaSet
        {
            get
            {
                return this.bool_1;
            }
        }

        public Cell this[int int_4, int int_5]
        {
            get
            {
                return this.method_12(int_4, int_5);
            }
        }

        public Row this[int int_4]
        {
            get
            {
                return this.method_13(int_4);
            }
        }

        public string Name
        {
            get
            {
                return this.string_1;
            }
            set
            {
                if (!value.IsNullOrEmpty())
                {
                    Worksheet worksheet = this.vmethod_0()[this.string_1];
                    if ((worksheet == null) || (worksheet == this))
                    {
                        this.string_1 = value.Trim();
                    }
                }
            }
        }

        public Aisino.Framework.Plugin.Core.ExcelXml.PrintOptions PrintOptions
        {
            [CompilerGenerated]
            get
            {
                return this.printOptions_0;
            }
            [CompilerGenerated]
            set
            {
                this.printOptions_0 = value;
            }
        }

        public int RowCount
        {
            get
            {
                return this.list_1.Count;
            }
        }

        public int TabColor
        {
            [CompilerGenerated]
            get
            {
                return this.int_3;
            }
            [CompilerGenerated]
            set
            {
                this.int_3 = value;
            }
        }

    }
}

