namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Xml;

    public class DataDict
    {
        public string CanvasName;
        private List<Dictionary<string, object>> list_0;
        private XmlDocument xmlDocument_0;

        public DataDict(Dictionary<string, object> dict)
        {
            
            this.list_0 = new List<Dictionary<string, object>>();
            this.CanvasName = "Canvas";
            this.list_0.Add(dict);
        }

        public DataDict(List<Dictionary<string, object>> ListDict)
        {
            
            this.list_0 = new List<Dictionary<string, object>>();
            this.CanvasName = "Canvas";
            this.list_0 = ListDict;
        }

        public DataDict(string string_0)
        {
            
            this.list_0 = new List<Dictionary<string, object>>();
            this.CanvasName = "Canvas";
            if (!File.Exists(string_0))
            {
                throw new Exception("xml文件不存在");
            }
            this.xmlDocument_0 = new XmlDocument();
            this.xmlDocument_0.Load(string_0);
            this.list_0 = this.method_0();
        }

        public DataDict(XmlDocument xmlDocument_1)
        {
            
            this.list_0 = new List<Dictionary<string, object>>();
            this.CanvasName = "Canvas";
            this.xmlDocument_0 = xmlDocument_1;
            this.list_0 = this.method_0();
        }

        public Dictionary<string, object> Data(int int_0)
        {
            if (int_0 >= this.list_0.Count)
            {
                throw new Exception("数据超过了索引");
            }
            return this.list_0[int_0];
        }

        public List<Dictionary<string, object>> DataList()
        {
            return this.list_0;
        }

        private List<Dictionary<string, object>> method_0()
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            XmlElement documentElement = this.xmlDocument_0.DocumentElement;
            this.CanvasName = documentElement.GetAttribute("Temp");
            if (this.CanvasName == string.Empty)
            {
                throw new Exception("未指定模板名称");
            }
            foreach (XmlNode node in documentElement.ChildNodes)
            {
                if (node.Name == "Data")
                {
                    XmlElement element2 = node as XmlElement;
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    foreach (XmlNode node2 in element2.ChildNodes)
                    {
                        KeyValuePair<string, object> pair = this.method_1(node2 as XmlElement);
                        if ((pair.Key != string.Empty) && !item.ContainsKey(pair.Key))
                        {
                            item.Add(pair.Key, pair.Value);
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        private KeyValuePair<string, object> method_1(XmlElement xmlElement_0)
        {
            switch (xmlElement_0.Name)
            {
                case "Label":
                    return this.method_2(xmlElement_0);

                case "Image":
                    return this.method_4(xmlElement_0);

                case "Table":
                    return this.method_5(xmlElement_0);

                case "Check":
                    return this.method_3(xmlElement_0);
            }
            throw new XmlException("解析节点错误" + xmlElement_0.OuterXml);
        }

        private KeyValuePair<string, object> method_2(XmlElement xmlElement_0)
        {
            if (!(xmlElement_0.Name == "Label"))
            {
                return new KeyValuePair<string, object>(string.Empty, null);
            }
            string attribute = xmlElement_0.GetAttribute("id");
            string innerText = xmlElement_0.InnerText;
            if (attribute == string.Empty)
            {
                throw new Exception(xmlElement_0.ToString(), new XmlException());
            }
            return new KeyValuePair<string, object>(attribute, innerText);
        }

        private KeyValuePair<string, object> method_3(XmlElement xmlElement_0)
        {
            if (!(xmlElement_0.Name == "Check"))
            {
                return new KeyValuePair<string, object>(string.Empty, null);
            }
            string attribute = xmlElement_0.GetAttribute("id");
            string innerText = xmlElement_0.InnerText;
            if (attribute == string.Empty)
            {
                throw new Exception(xmlElement_0.ToString(), new XmlException());
            }
            return new KeyValuePair<string, object>(attribute, Common.ToBool(innerText));
        }

        private KeyValuePair<string, object> method_4(XmlElement xmlElement_0)
        {
            Bitmap bitmap;
            if (!(xmlElement_0.Name == "Image"))
            {
                return new KeyValuePair<string, object>(string.Empty, null);
            }
            string attribute = xmlElement_0.GetAttribute("id");
            string innerText = xmlElement_0.InnerText;
            if (attribute == string.Empty)
            {
                throw new Exception(xmlElement_0.ToString(), new XmlException());
            }
            if (File.Exists(innerText))
            {
                bitmap = Image.FromFile(innerText) as Bitmap;
            }
            else
            {
                bitmap = Common.Base64ToBitMap(innerText);
            }
            return new KeyValuePair<string, object>(attribute, bitmap);
        }

        private KeyValuePair<string, object> method_5(XmlElement xmlElement_0)
        {
            if (!(xmlElement_0.Name == "Table"))
            {
                return new KeyValuePair<string, object>(string.Empty, null);
            }
            string attribute = xmlElement_0.GetAttribute("id");
            XmlNodeList childNodes = xmlElement_0.ChildNodes;
            DataTable table = new DataTable();
            foreach (XmlNode node in childNodes)
            {
                XmlElement element = node as XmlElement;
                if (element.Name == "Row")
                {
                    foreach (XmlNode node2 in element.ChildNodes)
                    {
                        string name = (node2 as XmlElement).GetAttribute("id");
                        if ((name != string.Empty) && !table.Columns.Contains(name))
                        {
                            table.Columns.Add(name);
                        }
                    }
                }
            }
            foreach (XmlNode node3 in childNodes)
            {
                XmlElement element3 = node3 as XmlElement;
                if (element3.Name == "Row")
                {
                    DataRow row = table.NewRow();
                    foreach (XmlNode node4 in element3.ChildNodes)
                    {
                        KeyValuePair<string, object> pair;
                        XmlElement element4 = node4 as XmlElement;
                        element4.GetAttribute("id");
                        string str3 = element4.Name;
                        if (str3 != null)
                        {
                            if (!(str3 == "Label"))
                            {
                                if (!(str3 == "Image"))
                                {
                                    goto Label_01CE;
                                }
                                pair = this.method_4(element4);
                            }
                            else
                            {
                                pair = this.method_2(element4);
                            }
                            goto Label_01DB;
                        }
                    Label_01CE:
                        pair = new KeyValuePair<string, object>(string.Empty, null);
                    Label_01DB:
                        if (pair.Key != string.Empty)
                        {
                            row[pair.Key] = pair.Value;
                        }
                    }
                    table.Rows.Add(row);
                }
            }
            return new KeyValuePair<string, object>(attribute, table);
        }

        public int Count
        {
            get
            {
                return this.list_0.Count;
            }
        }
    }
}

