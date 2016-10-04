namespace Aisino.Fwkp.Wbjk.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Xml;

    public class ExportToExcel
    {
        private string FilePath = @"C:\ExcelFile.xml";

        private void CreateXlsTemplate()
        {
            string path = this.FilePath.Remove(this.FilePath.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] contents = new string[] { 
                "<?xml version=\"1.0\" encoding=\"GBK\"?>", "<?mso-application progid=\"Excel.Sheet\"?>", "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"", "xmlns:o=\"urn:schemas-microsoft-com:office:office\"", "xmlns:x=\"urn:schemas-microsoft-com:office:excel\"", "xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"", "xmlns:html=\" http://www.w3.org/TR/REC-html40  \">", "<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">", " <Author>开票系统</Author>", "<LastAuthor>开票系统</LastAuthor>", "<Created>2011-09-20T02:55:54Z</Created>", " <LastSaved>2011-09-20T06:07:13Z</LastSaved>", "<Company>Microsoft</Company>", " <Version>11.9999</Version>", " </DocumentProperties>", " <ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">", 
                " <WindowHeight>11370</WindowHeight>", " <WindowWidth>17115</WindowWidth>", " <WindowTopX>0</WindowTopX>", " <WindowTopY>60</WindowTopY>", " <ProtectStructure>False</ProtectStructure>", " <ProtectWindows>False</ProtectWindows>", "</ExcelWorkbook>", "<Styles>", " <Style ss:ID=\"Default\" ss:Name=\"Normal\">", " <Alignment ss:Vertical=\"Center\"/>", " <Borders/>", " <Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Size=\"12\"/>", " <Interior/>", " <NumberFormat/>", " <Protection/>", "</Style>", 
                "<Style ss:ID=\"s21\">", "<Font ss:FontName=\"宋体\" x:CharSet=\"134\" ss:Bold=\"1\"/>", "</Style>", "<Style ss:ID=\"s22\">", " <Font ss:FontName=\"Georgia\" x:Family=\"Roman\" ss:Size=\"14\" ss:Bold=\"1\"/>", "</Style>", "<Style ss:ID=\"s23\">", "<Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Center\"/>", "</Style>", " </Styles>", "<Worksheet ss:Name=\"Sheet1\">", "<Table ss:ExpandedColumnCount=\"0\" ss:ExpandedRowCount=\"0\" x:FullColumns=\"1\"  ", " x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"14.25\"/>", "<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">", " <Unsynced/>", " <Print>", 
                " <ValidPrinterInfo/>", " <PaperSizeIndex>9</PaperSizeIndex>", " <HorizontalResolution>600</HorizontalResolution>", "<VerticalResolution>600</VerticalResolution>", "</Print>", "<Selected/>", " <Panes>", " <Pane>", " <Number>3</Number>", " <ActiveRow>0</ActiveRow>", " <ActiveCol>0</ActiveCol>", "</Pane>", " </Panes>", " <ProtectObjects>False</ProtectObjects>", " <ProtectScenarios>False</ProtectScenarios>", "</WorksheetOptions>", 
                "</Worksheet>", "<Worksheet ss:Name=\"Sheet2\">", "<Table ss:ExpandedColumnCount=\"0\" ss:ExpandedRowCount=\"0\" x:FullColumns=\"1\"  ", " x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"14.25\"/>", "<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">", " <Unsynced/>", " <ProtectObjects>False</ProtectObjects>", " <ProtectScenarios>False</ProtectScenarios>", "</WorksheetOptions>", "</Worksheet>", "<Worksheet ss:Name=\"Sheet3\">", "<Table ss:ExpandedColumnCount=\"0\" ss:ExpandedRowCount=\"0\" x:FullColumns=\"1\"  ", " x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"14.25\"/>", " <WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">", " <Unsynced/>", " <ProtectObjects>False</ProtectObjects>", 
                " <ProtectScenarios>False</ProtectScenarios>", "</WorksheetOptions>", "</Worksheet>", "</Workbook>"
             };
            File.WriteAllLines(this.FilePath, contents, ToolUtil.GetEncoding());
        }

        public void DataToExcel(string filePath, DataTable data, List<Dictionary<string, string>> listColumnsName, string title)
        {
            try
            {
                try
                {
                    this.FilePath = filePath;
                    this.CreateXlsTemplate();
                    this.WriteExcel(data, listColumnsName, title);
                }
                catch (Exception)
                {
                    File.Delete(this.FilePath);
                    throw;
                }
            }
            finally
            {
            }
        }

        private void WriteExcel(DataTable table, List<Dictionary<string, string>> listColumnName, string tableTitle)
        {
            int num;
            int num6;
            TaxCard card = TaxCardFactory.CreateTaxCard();
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            for (num = 0; num < listColumnName.Count; num++)
            {
                if (listColumnName[num].ContainsKey("AisinoLBL") && (!listColumnName[num].ContainsKey("Visible") || ((listColumnName[num]["Visible"].ToUpper() != "FALSE") || (num == 0x10))))
                {
                    list.Add(listColumnName[num]["AisinoLBL"]);
                    if (listColumnName[num].ContainsKey("Align") && (listColumnName[num]["Align"] == "MiddleRight"))
                    {
                        num6 = num + 1;
                        list2.Add(num6.ToString());
                    }
                }
            }
            if (File.Exists(this.FilePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.FilePath);
                XmlNode node = document.DocumentElement.ChildNodes.Item(3);
                string namespaceURI = "urn:schemas-microsoft-com:office:spreadsheet";
                XmlElement firstChild = (XmlElement) node.FirstChild;
                num6 = table.Columns.Count + 1;
                firstChild.SetAttribute("ExpandedColumnCount", namespaceURI, num6.ToString());
                num6 = table.Rows.Count + 1;
                firstChild.SetAttribute("ExpandedRowCount", namespaceURI, num6.ToString());
                XmlElement newChild = document.CreateElement("Column", namespaceURI);
                newChild.SetAttribute("AutoFitWidth", namespaceURI, "0");
                newChild.SetAttribute("Width", namespaceURI, "50");
                firstChild.AppendChild(newChild);
                if (list2.Count > 0)
                {
                    for (num = 0; num < list2.Count; num++)
                    {
                        XmlElement element3 = document.CreateElement("Column", namespaceURI);
                        element3.SetAttribute("Index", namespaceURI, list2[num]);
                        element3.SetAttribute("StyleID", namespaceURI, "s23");
                        firstChild.AppendChild(element3);
                    }
                }
                if (tableTitle.Length > 0)
                {
                    XmlElement element4 = document.CreateElement("Row", namespaceURI);
                    element4.SetAttribute("Height", namespaceURI, "22");
                    firstChild.AppendChild(element4);
                    XmlElement element5 = document.CreateElement("Cell", namespaceURI);
                    element5.SetAttribute("Index", namespaceURI, (table.Columns.Count / 2).ToString());
                    element5.SetAttribute("StyleID", namespaceURI, "s22");
                    element4.AppendChild(element5);
                    XmlElement element6 = document.CreateElement("Data", namespaceURI);
                    element6.SetAttribute("Type", namespaceURI, "String");
                    element6.InnerText = tableTitle;
                    element5.AppendChild(element6);
                    firstChild.SetAttribute("ExpandedRowCount", namespaceURI, (table.Rows.Count + 2).ToString());
                }
                XmlElement element7 = document.CreateElement("Row", namespaceURI);
                element7.SetAttribute("Height", namespaceURI, "20");
                firstChild.AppendChild(element7);
                int num3 = 0;
                for (num = 0; num < table.Columns.Count; num++)
                {
                    if (((table.Columns[num].ColumnName != "FPMXXH") && (table.Columns[num].ColumnName != "YYSBZ")) && (!card.get_QYLX().ISTDQY || !(table.Columns[num].ColumnName == "BSZT")))
                    {
                        XmlElement element8 = document.CreateElement("Cell", namespaceURI);
                        element8.SetAttribute("StyleID", namespaceURI, "s21");
                        element7.AppendChild(element8);
                        XmlElement element9 = document.CreateElement("Data", namespaceURI);
                        element9.SetAttribute("Type", namespaceURI, "String");
                        element9.InnerText = list[num3];
                        element8.AppendChild(element9);
                        num3++;
                    }
                }
                for (num = 0; num < table.Rows.Count; num++)
                {
                    XmlElement element10 = document.CreateElement("Row", namespaceURI);
                    firstChild.AppendChild(element10);
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (((table.Columns[i].ColumnName != "FPMXXH") && (table.Columns[i].ColumnName != "YYSBZ")) && (!card.get_QYLX().ISTDQY || (table.Columns[i].ColumnName != "BSZT")))
                        {
                            XmlElement element11 = document.CreateElement("Cell", namespaceURI);
                            element10.AppendChild(element11);
                            XmlElement element12 = document.CreateElement("Data", namespaceURI);
                            element12.SetAttribute("Type", namespaceURI, "String");
                            if (table.Columns[i].DataType == typeof(DateTime))
                            {
                                element12.InnerText = Convert.ToDateTime(table.Rows[num][i]).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                string str2;
                                if (table.Columns[i].ColumnName == "SLV")
                                {
                                    str2 = table.Rows[num][i].ToString();
                                    if (((str2 == "") || (str2 == null)) || (str2 == "多税率"))
                                    {
                                        element12.InnerText = "多税率";
                                    }
                                    else if (str2 == "中外合作油气田")
                                    {
                                        element12.InnerText = str2;
                                    }
                                    else if (str2 == "0.015")
                                    {
                                        element12.InnerText = Convert.ToSingle(table.Rows[num][i]).ToString("0.0%");
                                    }
                                    else
                                    {
                                        element12.InnerText = Convert.ToSingle(table.Rows[num][i]).ToString("0%");
                                    }
                                }
                                else if (table.Columns[i].ColumnName == "GFSH")
                                {
                                    str2 = table.Rows[num][i].ToString();
                                    if (((str2.Equals("000000000000000") || str2.Equals("00000000000000000")) || str2.Equals("000000000000000000")) || str2.Equals("00000000000000000000"))
                                    {
                                        if ((table.Rows[num][1].ToString() == "普通发票") || (table.Rows[num][1].ToString() == "收购发票"))
                                        {
                                            if (((table.Rows[num][20].ToString() == "是") && (((table.Rows[num][11].ToString() == "0") || (table.Rows[num][11].ToString() == null)) || (table.Rows[num][11].ToString() == ""))) && (((table.Rows[num][13].ToString() == "0") || (table.Rows[num][13].ToString() == null)) || (table.Rows[num][13].ToString() == "")))
                                            {
                                                element12.InnerText = table.Rows[num][i].ToString();
                                            }
                                            else
                                            {
                                                element12.InnerText = "";
                                            }
                                        }
                                        else
                                        {
                                            element12.InnerText = table.Rows[num][i].ToString();
                                        }
                                    }
                                    else
                                    {
                                        element12.InnerText = table.Rows[num][i].ToString();
                                    }
                                }
                                else if (table.Columns[i].ColumnName == "BZ")
                                {
                                    string s = table.Rows[num][i].ToString();
                                    if ((s == "") || (s == null))
                                    {
                                        element12.InnerText = "";
                                    }
                                    else
                                    {
                                        byte[] buffer = Convert.FromBase64String(s);
                                        if ((buffer != null) && (buffer.Length >= 1))
                                        {
                                            s = ToolUtil.GetString(buffer);
                                        }
                                        else
                                        {
                                            s = "";
                                        }
                                        element12.InnerText = s;
                                    }
                                }
                                else if (table.Columns[i].ColumnName == "FPHM")
                                {
                                    element12.InnerText = table.Rows[num][i].ToString().PadLeft(8, '0');
                                }
                                else if ((((table.Columns[i].ColumnName == "JE") || (table.Columns[i].ColumnName == "SE")) || (table.Columns[i].ColumnName == "HJJE")) || (table.Columns[i].ColumnName == "HJSE"))
                                {
                                    double result = 0.0;
                                    if (double.TryParse(table.Rows[num][i].ToString(), out result))
                                    {
                                        element12.InnerText = result.ToString("0.00");
                                    }
                                    else
                                    {
                                        element12.InnerText = "0.00";
                                    }
                                }
                                else
                                {
                                    element12.InnerText = table.Rows[num][i].ToString();
                                }
                            }
                            element11.AppendChild(element12);
                        }
                    }
                }
                document.Save(this.FilePath);
            }
        }
    }
}

