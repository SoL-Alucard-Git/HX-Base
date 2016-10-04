namespace Aisino.Fwkp.Wbjk.Common
{
    using Aisino.Fwkp.Wbjk.Service;
    using System;
    using System.Data;
    using System.IO;
    using System.Xml;

    public class readXMLSlv
    {
        private static string TxtWbcrXmlPath = ConfigFile.GetXmlFilePath;

        internal static double[] ReadXMLSlv()
        {
            if (File.Exists(TxtWbcrXmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(TxtWbcrXmlPath);
                string xpath = "/configuration/SLV";
                string[] strArray = document.SelectSingleNode(xpath).InnerText.Trim().Split(new char[] { ';' });
                double[] numArray = new double[strArray.Length];
                for (int i = 0; i < strArray.Length; i++)
                {
                    numArray[i] = Convert.ToDouble(strArray[i]);
                }
                return numArray;
            }
            new CreateXmlFile().CreateXml(TxtWbcrXmlPath);
            return new double[1];
        }

        internal static DataTable ReadXMLSlvTable()
        {
            DataTable table = new DataTable();
            if (File.Exists(TxtWbcrXmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(TxtWbcrXmlPath);
                string xpath = "/configuration/SLV";
                string[] strArray = document.SelectSingleNode(xpath).InnerText.Trim().Split(new char[] { ';' });
                table.Columns.Add(new DataColumn("Display", typeof(string)));
                table.Columns.Add(new DataColumn("Value", typeof(double)));
                for (int i = 0; i < strArray.Length; i++)
                {
                    double num2 = Convert.ToDouble(strArray[i]) * 100.0;
                    table.Rows.Add(new object[] { num2.ToString() + "%", num2 });
                }
                return table;
            }
            new CreateXmlFile().CreateXml(TxtWbcrXmlPath);
            return table;
        }
    }
}

