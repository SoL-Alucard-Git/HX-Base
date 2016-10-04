namespace Aisino.Fwkp.Wbjk.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk.Service;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class CbbXmlBind
    {
        private static string TxtWbcrXmlPath = ConfigFile.GetXmlFilePath;

        private static bool IsCanSQ(string PZ)
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (PZ.CompareTo("f") == 0)
            {
                if (card.get_QYLX().ISHY)
                {
                    return true;
                }
            }
            else if (PZ.CompareTo("j") == 0)
            {
                if (card.get_QYLX().ISJDC)
                {
                    return true;
                }
            }
            else if (PZ.CompareTo("c") == 0)
            {
                if (card.get_QYLX().ISPTFP)
                {
                    return true;
                }
            }
            else if (PZ.CompareTo("s") == 0)
            {
                if (card.get_QYLX().ISZYFP)
                {
                    return true;
                }
            }
            else if (PZ.CompareTo("a") == 0)
            {
                if (card.get_QYLX().ISPTFP || card.get_QYLX().ISZYFP)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        private static List<List<string>> QZSQ_Split(string SQ)
        {
            string str = ",";
            string str2 = "-";
            List<List<string>> list = new List<List<string>>();
            List<string> list2 = new List<string>();
            string[] strArray = SQ.Split(new string[] { str }, StringSplitOptions.None);
            foreach (string str3 in strArray)
            {
                list2.Add(str3);
            }
            int count = list2.Count;
            for (int i = 0; i < count; i++)
            {
                string str4 = list2[i];
                List<string> item = new List<string>();
                string[] strArray2 = str4.Split(new string[] { str2 }, StringSplitOptions.None);
                foreach (string str5 in strArray2)
                {
                    item.Add(str5);
                }
                list.Add(item);
            }
            return list;
        }

        internal static BindingSource ReadXmlNode(string XMLnode, bool IsHYJDC)
        {
            try
            {
                if (!File.Exists(TxtWbcrXmlPath))
                {
                    new CreateXmlFile().CreateXml(TxtWbcrXmlPath);
                }
                BindingSource source = new BindingSource();
                XmlDocument document = new XmlDocument();
                document.Load(TxtWbcrXmlPath);
                string xpath = "/configuration/" + XMLnode;
                XmlNodeList childNodes = document.SelectSingleNode(xpath).ChildNodes;
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (XmlNode node in childNodes)
                {
                    string attribute;
                    string str3;
                    XmlElement element = (XmlElement) node;
                    if ((XMLnode.CompareTo("InvType") == 0) || (XMLnode.CompareTo("InvType_1") == 0))
                    {
                        if (IsHYJDC)
                        {
                            attribute = element.GetAttribute("display");
                            str3 = element.GetAttribute("value");
                            if (IsCanSQ(str3))
                            {
                                dictionary.Add(str3, attribute);
                            }
                        }
                        else
                        {
                            attribute = element.GetAttribute("display");
                            str3 = element.GetAttribute("value");
                            if ((str3.CompareTo("f") != 0) && (str3.CompareTo("j") != 0))
                            {
                                dictionary.Add(str3, attribute);
                            }
                        }
                    }
                    else
                    {
                        attribute = element.GetAttribute("display");
                        str3 = element.GetAttribute("value");
                        dictionary.Add(str3, attribute);
                    }
                }
                source.DataSource = dictionary;
                return source;
            }
            catch (Exception)
            {
                new CreateXmlFile().CreateXml(TxtWbcrXmlPath);
                return null;
            }
        }
    }
}

