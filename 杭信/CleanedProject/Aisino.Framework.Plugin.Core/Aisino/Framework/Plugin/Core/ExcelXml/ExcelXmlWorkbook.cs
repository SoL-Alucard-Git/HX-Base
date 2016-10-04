namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using ns14;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Xml;

    public class ExcelXmlWorkbook
    {
        [CompilerGenerated]
        private DocumentProperties documentProperties_0;
        private List<XmlStyle> list_0;
        private List<Worksheet> list_1;
        internal List<Class139> list_2;

        public ExcelXmlWorkbook()
        {
            
            this.method_3();
        }

        public Worksheet Add()
        {
            return this[this.list_1.Count];
        }

        public Worksheet Add(string string_0)
        {
            this[this.list_1.Count].Name = string_0;
            return this[this.list_1.Count];
        }

        public void AddNamedRange(Aisino.Framework.Plugin.Core.ExcelXml.Range range_0, string string_0)
        {
            if (string_0.IsNullOrEmpty())
            {
                throw new ArgumentNullException("name");
            }
            if (Aisino.Framework.Plugin.Core.ExcelXml.Range.smethod_2(string_0))
            {
                throw new ArgumentException(string_0 + "is a excel internal range name");
            }
            this.method_10(range_0, string_0, null);
        }

        public static ExcelXmlWorkbook DataSetToWorkbook(DataSet dataSet_0)
        {
            ExcelXmlWorkbook workbook = new ExcelXmlWorkbook();
            for (int i = 0; i < dataSet_0.Tables.Count; i++)
            {
                Worksheet worksheet = workbook[i];
                worksheet.Name = "Table" + i.ToString(CultureInfo.InvariantCulture);
                int count = dataSet_0.Tables[i].Columns.Count;
                for (int j = 0; j < count; j++)
                {
                    worksheet[j, 0].Value = dataSet_0.Tables[i].Columns[j].ColumnName;
                    worksheet[j, 0].Font.Bold = true;
                }
                int num2 = 0;
                foreach (DataRow row in dataSet_0.Tables[i].Rows)
                {
                    num2++;
                    for (int k = 0; k < count; k++)
                    {
                        switch (row[k].GetType().FullName)
                        {
                            case "System.DateTime":
                                worksheet[k, num2].Value = (DateTime) row[k];
                                break;

                            case "System.Boolean":
                                worksheet[k, num2].Value = (bool) row[k];
                                break;

                            case "System.SByte":
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                            case "System.UInt16":
                            case "System.UInt32":
                            case "System.UInt64":
                            case "System.Single":
                            case "System.Double":
                            case "System.Decimal":
                                worksheet[k, num2].Value = Convert.ToDecimal(row[k], CultureInfo.InvariantCulture);
                                break;

                            case "System.DBNull":
                                break;

                            default:
                                worksheet[k, num2].Value = row[k].ToString();
                                break;
                        }
                    }
                }
            }
            return workbook;
        }

        public void DeleteSheet(Worksheet worksheet_0)
        {
            Worksheet ws = worksheet_0;
            if (ws != null)
            {
                this.DeleteSheet(this.list_1.FindIndex(s => s == ws));
            }
        }

        public void DeleteSheet(int int_0)
        {
            if ((int_0 >= 0) && (int_0 < this.list_1.Count))
            {
                this.list_1.RemoveAt(int_0);
            }
        }

        public void DeleteSheet(string string_0)
        {
            this.DeleteSheet(this.method_5(string_0));
        }

        public bool Export(Stream stream_0)
        {
            XmlWriterSettings settings = new XmlWriterSettings {
                Indent = true,
                IndentChars = "    "
            };
            if (!stream_0.CanWrite)
            {
                return false;
            }
            XmlWriter writer = XmlWriter.Create(stream_0, settings);
            if (writer == null)
            {
                return false;
            }
            writer.WriteStartDocument();
            writer.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");
            writer.WriteWhitespace("\n");
            writer.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");
            writer.WriteAttributeString("xmlns", "o", null, "urn:schemas-microsoft-com:office:office");
            writer.WriteAttributeString("xmlns", "x", null, "urn:schemas-microsoft-com:office:excel");
            writer.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");
            writer.WriteAttributeString("xmlns", "html", null, "http://www.w3.org/TR/REC-html40");
            this.Properties.method_0(writer);
            writer.WriteStartElement("Styles");
            foreach (XmlStyle style in this.list_0)
            {
                style.method_11(writer);
            }
            writer.WriteEndElement();
            this.method_14(writer, null);
            foreach (Worksheet worksheet in this.list_1)
            {
                worksheet.method_16(writer);
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
            return true;
        }

        public bool Export(string string_0)
        {
            bool flag;
            try
            {
                FileStream stream = new FileStream(string_0, FileMode.Create);
                bool flag2 = this.Export(stream);
                stream.Close();
                stream.Dispose();
                return flag2;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public Worksheet GetSheetByName(string string_0)
        {
            string sheetName = string_0;
            return this.list_1.Find(sheet => sheet.Name.Equals(sheetName, StringComparison.OrdinalIgnoreCase));
        }

        public static ExcelXmlWorkbook Import(Stream stream_0)
        {
            return null;
            //XmlReaderSettings settings = new XmlReaderSettings {
            //    CloseInput = false,
            //    IgnoreComments = true,
            //    IgnoreProcessingInstructions = true,
            //    IgnoreWhitespace = true
            //};
            //if (!stream_0.CanRead)
            //{
            //    return null;
            //}
            //XmlReader reader = XmlReader.Create(stream_0, settings);
            //ExcelXmlWorkbook workbook = new ExcelXmlWorkbook();
            //workbook.list_0.Clear();
            //int num = 0;
            //while (reader.Read())
            //{
            //    string str;
            //    if ((reader.NodeType == XmlNodeType.Element) && ((str = reader.Name) != null))
            //    {
            //        if (str == "DocumentProperties")
            //        {
            //            if (!reader.IsEmptyElement)
            //            {
            //                workbook.Properties.method_1(reader);
            //            }
            //        }
            //        else if (str == "Styles")
            //        {
            //            if (!reader.IsEmptyElement)
            //            {
            //                workbook.method_0(reader);
            //            }
            //        }
            //        else if (!(str == "Names"))
            //        {
            //            if ((str == "Worksheet") && !reader.IsEmptyElement)
            //            {
            //                workbook[num++].method_5(reader);
            //            }
            //        }
            //        else
            //        {
            //            smethod_0(reader, (List<Class139>) workbook, null);
            //        }
            //    }
            //}
            //workbook.method_1();
            //workbook.method_2();
            //reader.Close();
            //stream_0.Close();
            //stream_0.Dispose();
            //return workbook;
        }

        public static ExcelXmlWorkbook Import(string string_0)
        {
            ExcelXmlWorkbook workbook;
            if (!File.Exists(string_0))
            {
                return null;
            }
            try
            {
                Stream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
                ExcelXmlWorkbook workbook2 = Import(stream);
                stream.Close();
                stream.Dispose();
                return workbook2;
            }
            catch (IOException)
            {
                workbook = null;
            }
            catch (SecurityException)
            {
                workbook = null;
            }
            catch (UnauthorizedAccessException)
            {
                workbook = null;
            }
            return workbook;
        }

        public Worksheet InsertAfter(Worksheet worksheet_0)
        {
            Worksheet ws = worksheet_0;
            return this.InsertAfter(this.list_1.FindIndex(s => s == ws));
        }

        public Worksheet InsertAfter(int int_0)
        {
            if (int_0 < 0)
            {
                return this.Add();
            }
            if (int_0 >= this.list_1.Count)
            {
                return this[int_0];
            }
            Worksheet worksheet = new Worksheet(this);
            this.method_4(worksheet, int_0 + 1);
            this.list_1.Insert(int_0 + 1, worksheet);
            return worksheet;
        }

        public Worksheet InsertAfter(string string_0)
        {
            return this.InsertAfter(this.method_5(string_0));
        }

        public Worksheet InsertBefore(Worksheet worksheet_0)
        {
            Worksheet ws = worksheet_0;
            return this.InsertBefore(this.list_1.FindIndex(s => s == ws));
        }

        public Worksheet InsertBefore(int int_0)
        {
            if (int_0 < 0)
            {
                return this.Add();
            }
            if (int_0 >= this.list_1.Count)
            {
                return this[int_0];
            }
            Worksheet worksheet = new Worksheet(this);
            this.method_4(worksheet, int_0);
            this.list_1.Insert(int_0, worksheet);
            return worksheet;
        }

        public Worksheet InsertBefore(string string_0)
        {
            return this.InsertBefore(this.method_5(string_0));
        }

        private void method_0(XmlReader xmlReader_0)
        {
            while (xmlReader_0.Read())
            {
                if ((xmlReader_0.Name == "Styles") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                {
                    break;
                }
                XmlStyle item = XmlStyle.smethod_2(xmlReader_0);
                if (item != null)
                {
                    this.list_0.Add(item);
                }
            }
        }

        private void method_1()
        {
            int index = -1;
            int num2 = -1;
            foreach (Class139 class2 in this.list_2)
            {
                num2++;
                if (class2.string_0 == "Print_Titles")
                {
                    Class129.smethod_5(class2.worksheet_0, class2.range_0.string_1);
                    index = num2;
                }
                else
                {
                    Worksheet worksheet = class2.worksheet_0 ?? this[0];
                    class2.range_0.method_9(worksheet[0, 0]);
                    if (class2.string_0 == "_FilterDatabase")
                    {
                        worksheet.bool_0 = true;
                    }
                    if (class2.string_0 == "Print_Area")
                    {
                        worksheet.bool_1 = true;
                    }
                }
            }
            if (index != -1)
            {
                this.list_2.RemoveAt(index);
            }
        }

        internal void method_10(Aisino.Framework.Plugin.Core.ExcelXml.Range range_0, string string_0, Worksheet worksheet_0)
        {
            //Predicate<Class139> match = null;
            //<>c__DisplayClassf classf;
            //Aisino.Framework.Plugin.Core.ExcelXml.Range range = range_0;
            //string name = string_0;
            //Worksheet ws = worksheet_0;
            //if ((range.vmethod_2() != null) && (range.vmethod_2().vmethod_0() != this))
            //{
            //    throw new InvalidOperationException("Named range parent book should be same");
            //}
            //Class139 class2 = this.list_2.Find(new Predicate<Class139>(classf.<AddNamedRange>b__c));
            //if (class2 == null)
            //{
            //    if (match == null)
            //    {
            //        match = new Predicate<Class139>(classf.<AddNamedRange>b__d);
            //    }
            //    class2 = this.list_2.Find(match);
            //    if (class2 == null)
            //    {
            //        if (name == "_FilterDatabase")
            //        {
            //            this.list_2.Insert(0, new Class139(range, name, ws));
            //        }
            //        else
            //        {
            //            this.list_2.Add(new Class139(range, name, ws));
            //        }
            //    }
            //    else
            //    {
            //        class2.string_0 = name;
            //    }
            //}
            //else
            //{
            //    class2.range_0 = range;
            //}
        }

        internal void method_11(string string_0, Worksheet worksheet_0)
        {
            //<>c__DisplayClass12 class2;
            //string name = string_0;
            //Worksheet ws = worksheet_0;
            //this.list_2.RemoveAll(new Predicate<Class139>(class2.<RemoveNamedRange>b__11));
            return ;
        }

        internal string method_12(Worksheet worksheet_0)
        {
            //<>c__DisplayClass15 class2;
            //Worksheet ws = worksheet_0;
            //Class139 class3 = this.list_2.Find(new Predicate<Class139>(class2.<GetAutoFilterRange>b__14));
            //if (class3 == null)
            //{
            //    return "";
            //}
            //return class3.range_0.method_7(false);
            return null;
        }

        internal List<string> method_13(Cell cell_0)
        {
            List<string> list = new List<string>();
            PrintOptions printOptions = cell_0.row_0.worksheet_0.PrintOptions;
            if (printOptions.bool_1)
            {
                int num = cell_0.row_0.int_0 + 1;
                int num2 = cell_0.int_0 + 1;
                if ((num >= printOptions.int_3) && (num <= printOptions.int_4))
                {
                    list.Add("Print_Titles");
                }
                else if ((num2 >= printOptions.int_5) && (num2 <= printOptions.int_6))
                {
                    list.Add("Print_Titles");
                }
            }
            foreach (Class139 class2 in this.list_2)
            {
                if (class2.range_0.Contains(cell_0))
                {
                    list.Add(class2.string_0);
                }
            }
            return list;
        }

        internal void method_14(XmlWriter xmlWriter_0, Worksheet worksheet_0)
        {
            bool flag = false;
            if ((worksheet_0 != null) && worksheet_0.PrintOptions.bool_1)
            {
                flag = true;
                xmlWriter_0.WriteStartElement("Names");
                xmlWriter_0.WriteStartElement("NamedRange");
                xmlWriter_0.WriteAttributeString("ss", "Name", null, "Print_Titles");
                xmlWriter_0.WriteAttributeString("ss", "RefersTo", null, worksheet_0.PrintOptions.method_0(worksheet_0.Name));
                xmlWriter_0.WriteEndElement();
            }
            foreach (Class139 class2 in this.list_2)
            {
                if (class2.worksheet_0 == worksheet_0)
                {
                    if (!flag)
                    {
                        flag = true;
                        xmlWriter_0.WriteStartElement("Names");
                    }
                    xmlWriter_0.WriteStartElement("NamedRange");
                    xmlWriter_0.WriteAttributeString("ss", "Name", null, class2.string_0);
                    xmlWriter_0.WriteAttributeString("ss", "RefersTo", null, class2.range_0.method_7(true));
                    if (class2.string_0 == "_FilterDatabase")
                    {
                        xmlWriter_0.WriteAttributeString("ss", "Hidden", null, "1");
                    }
                    xmlWriter_0.WriteEndElement();
                }
            }
            if (flag)
            {
                xmlWriter_0.WriteEndElement();
            }
        }

        private void method_2()
        {
            for (int i = 0; i < this.list_1.Count; i++)
            {
                Worksheet worksheet = this[i];
                foreach (Row row in worksheet.list_1)
                {
                    foreach (Cell cell in row.list_0)
                    {
                        cell.method_5();
                    }
                }
            }
        }

        private void method_3()
        {
            this.Properties = new DocumentProperties();
            this.list_0 = new List<XmlStyle>();
            this.list_1 = new List<Worksheet>();
            this.list_2 = new List<Class139>();
            XmlStyle item = new XmlStyle {
                ID = "Default"
            };
            item.Alignment.Vertical = VerticalAlignment.Bottom;
            this.list_0.Add(item);
        }

        private void method_4(Worksheet worksheet_0, int int_0)
        {
            int_0++;
            string str = "Sheet" + int_0.ToString(CultureInfo.InvariantCulture);
            while (this.method_5(str) != -1)
            {
                str = "Sheet" + (++int_0).ToString(CultureInfo.InvariantCulture);
            }
            worksheet_0.Name = str;
        }

        private int method_5(string string_0)
        {
            //<>c__DisplayClass1 class2;
            //string sheetName = string_0;
            //return this.list_1.FindIndex(new Predicate<Worksheet>(class2.<GetSheetIDByName>b__0));
            return 0;
        }

        internal XmlStyle method_6(string string_0)
        {
            //<>c__DisplayClass4 class2;
            //string ID = string_0;
            //if (ID.IsNullOrEmpty())
            //{
            //    return this.list_0[0];
            //}
            //return this.list_0.Find(new Predicate<XmlStyle>(class2.<GetStyleByID>b__3));
            return null;
        }

        internal bool method_7(string string_0)
        {
            //<>c__DisplayClass7 class2;
            //string ID = string_0;
            //return this.list_0.Exists(new Predicate<XmlStyle>(class2.<HasStyleID>b__6));
            return false;
        }

        internal string method_8(XmlStyle xmlStyle_0)
        {
            XmlStyle style = this.method_9(xmlStyle_0);
            if (style != null)
            {
                return style.ID;
            }
            int count = this.list_0.Count;
            xmlStyle_0.ID = string.Format(CultureInfo.InvariantCulture, "S{0:00}", new object[] { count++ });
            while (this.method_7(xmlStyle_0.ID))
            {
                xmlStyle_0.ID = string.Format(CultureInfo.InvariantCulture, "S{0:00}", new object[] { count++ });
            }
            this.list_0.Add(xmlStyle_0);
            return xmlStyle_0.ID;
        }

        internal XmlStyle method_9(XmlStyle xmlStyle_0)
        {
            //<>c__DisplayClassa classa;
            //XmlStyle style = xmlStyle_0;
            //return this.list_0.Find(new Predicate<XmlStyle>(classa.<FindStyle>b__9));
            return null;
        }

        internal static void smethod_0(XmlReader xmlReader_0, List<Class139> list_3, Worksheet worksheet_0)
        {
            if (!xmlReader_0.IsEmptyElement)
            {
                while (xmlReader_0.Read())
                {
                    if ((xmlReader_0.Name == "Names") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                    {
                        break;
                    }
                    if ((xmlReader_0.NodeType == XmlNodeType.Element) && (xmlReader_0.Name == "NamedRange"))
                    {
                        Aisino.Framework.Plugin.Core.ExcelXml.Range range = null;
                        string str = "";
                        foreach (XmlReaderAttributeItem item in xmlReader_0.GetAttributes())
                        {
                            if ((item.LocalName == "Name") && item.HasValue)
                            {
                                str = item.Value;
                            }
                            if ((item.LocalName == "RefersTo") && item.HasValue)
                            {
                                range = new Aisino.Framework.Plugin.Core.ExcelXml.Range(item.Value);
                            }
                        }
                        Class139 class2 = new Class139(range, str, worksheet_0);
                        list_3.Add(class2);
                    }
                }
            }
        }

        public XmlStyle DefaultStyle
        {
            get
            {
                return this.list_0[0];
            }
            set
            {
                if ((value != null) && (value.ID == "Default"))
                {
                    this.list_0[0] = value;
                }
            }
        }

        public Worksheet this[int int_0]
        {
            get
            {
                if (int_0 < 0)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                if ((int_0 + 1) > this.list_1.Count)
                {
                    for (int i = this.list_1.Count; i <= int_0; i++)
                    {
                        Worksheet worksheet = new Worksheet(this);
                        this.method_4(worksheet, i);
                        this.list_1.Add(worksheet);
                    }
                }
                return this.list_1[int_0];
            }
        }

        public Worksheet this[string string_0]
        {
            get
            {
                return this.GetSheetByName(string_0);
            }
        }

        public DocumentProperties Properties
        {
            [CompilerGenerated]
            get
            {
                return this.documentProperties_0;
            }
            [CompilerGenerated]
            set
            {
                this.documentProperties_0 = value;
            }
        }

        public int SheetCount
        {
            get
            {
                return this.list_1.Count;
            }
        }
    }
}

