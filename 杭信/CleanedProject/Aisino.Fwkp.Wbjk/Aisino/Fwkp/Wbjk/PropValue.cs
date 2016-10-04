namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk.Common;
    using System;

    internal class PropValue
    {
        private static bool GetValue(string Node, string Attribute)
        {
            string str = ReadWriteXml.Read(Node, Attribute);
            bool result = false;
            bool.TryParse(str, out result);
            return result;
        }

        private static void SetValue(string Node, string Attribute, bool value)
        {
            string attributeValue = value.ToString();
            ReadWriteXml.Write(Node, Attribute, attributeValue);
        }

        public static bool AllDate
        {
            get
            {
                return GetValue("FPQueryExcelExport", "AllDate");
            }
            set
            {
                SetValue("FPQueryExcelExport", "AllDate", value);
            }
        }

        public static bool CheckBoxKqd
        {
            get
            {
                string str = ReadWriteXml.Read("FaPiaoExport", "MakeaList");
                bool result = false;
                bool.TryParse(str, out result);
                return result;
            }
            set
            {
                string attributeValue = value.ToString();
                ReadWriteXml.Write("FaPiaoExport", "MakeaList", attributeValue);
            }
        }

        public static bool ContainEndDay
        {
            get
            {
                return GetValue("FPQueryExcelExport", "ContainEndDay");
            }
            set
            {
                SetValue("FPQueryExcelExport", "ContainEndDay", value);
            }
        }

        public static bool ContainStartDay
        {
            get
            {
                return GetValue("FPQueryExcelExport", "ContainStartDay");
            }
            set
            {
                SetValue("FPQueryExcelExport", "ContainStartDay", value);
            }
        }

        public static string ExcelAmountType
        {
            get
            {
                return ReadWriteXml.Read("ExcelImport", "JinEType");
            }
            set
            {
                ReadWriteXml.Write("ExcelImport", "JinEType", value);
            }
        }

        public static string ExcelFile1Path
        {
            get
            {
                return IniRead.GetPrivateProfileString("File", "File1Path");
            }
            set
            {
                IniRead.WritePrivateProfileString("File", "File1Path", value);
                ReadWriteXml.Write("ExcelImport", "File1Path", value);
            }
        }

        public static string ExcelFile2Path
        {
            get
            {
                return IniRead.GetPrivateProfileString("File", "File2Path");
            }
            set
            {
                IniRead.WritePrivateProfileString("File", "File2Path", value);
                ReadWriteXml.Write("ExcelImport", "File2Path", value);
            }
        }

        public static string ExcelInvType
        {
            get
            {
                return ReadWriteXml.Read("ExcelImport", "InvType");
            }
            set
            {
                ReadWriteXml.Write("ExcelImport", "InvType", value);
            }
        }

        public static string InvExportTxtPath
        {
            get
            {
                return ReadWriteXml.Read("FaPiaoExport", "Path");
            }
            set
            {
                ReadWriteXml.Write("FaPiaoExport", "Path", value);
            }
        }

        public static string InvExportXmlPath
        {
            get
            {
                string str = PropertyUtil.GetValue("InvExportXmlPath", @"C:\发票导出.xml");
                if (!str.Contains("发票导出"))
                {
                    return str.Insert(str.LastIndexOf('\\') + 1, DateTime.Now.ToString("yyyyMMddHHmm") + "发票导出");
                }
                return str.Insert(str.LastIndexOf('\\') + 1, DateTime.Now.ToString("yyyyMMddHHmm"));
            }
            set
            {
                string str = value;
                int length = value.LastIndexOf('\\') + 1;
                str = str.Substring(0, length);
                PropertyUtil.SetValue("InvExportXmlPath", str);
            }
        }

        public static string InvExportXslPath
        {
            get
            {
                string str = PropertyUtil.GetValue("InvExportXslPath", @"C:\发票导出.xls");
                if (!str.Contains("发票导出"))
                {
                    return str.Insert(str.LastIndexOf('\\') + 1, DateTime.Now.ToString("yyyyMMddHHmm") + "发票导出");
                }
                return str.Insert(str.LastIndexOf('\\') + 1, DateTime.Now.ToString("yyyyMMddHHmm"));
            }
            set
            {
                string str = value;
                int length = value.LastIndexOf('\\') + 1;
                str = str.Substring(0, length);
                PropertyUtil.SetValue("InvExportXslPath", str);
            }
        }

        public static string InvSPExportXslPath
        {
            get
            {
                string str = PropertyUtil.GetValue("FPSPQueryExcelExport", @"C:\商品导出.xls");
                if (!str.Contains("商品导出"))
                {
                    return str.Insert(str.LastIndexOf('\\') + 1, DateTime.Now.ToString("yyyyMMddHHmm") + "商品导出");
                }
                return str.Insert(str.LastIndexOf('\\') + 1, DateTime.Now.ToString("yyyyMMddHHmm"));
            }
            set
            {
                string str = value;
                int length = value.LastIndexOf('\\') + 1;
                str = str.Substring(0, length);
                PropertyUtil.SetValue("FPSPQueryExcelExport", str);
            }
        }

        public static bool IsBlankBZAdd
        {
            get
            {
                string str = ReadWriteXml.Read("DanJuMerge", "IsBlankBZAdd");
                bool result = false;
                bool.TryParse(str, out result);
                return result;
            }
            set
            {
                string attributeValue = value.ToString();
                ReadWriteXml.Write("DanJuMerge", "IsBlankBZAdd", attributeValue);
            }
        }

        public static bool IsJiaoYan
        {
            get
            {
                string str = ReadWriteXml.Read("DanJuLuRu", "IsJiaoYan");
                bool result = false;
                bool.TryParse(str, out result);
                return result;
            }
            set
            {
                string attributeValue = value.ToString();
                ReadWriteXml.Write("DanJuLuRu", "IsJiaoYan", attributeValue);
            }
        }

        public static bool IsMergeBZ
        {
            get
            {
                string str = ReadWriteXml.Read("DanJuMerge", "IsMergeBZ");
                bool result = false;
                bool.TryParse(str, out result);
                return result;
            }
            set
            {
                string attributeValue = value.ToString();
                ReadWriteXml.Write("DanJuMerge", "IsMergeBZ", attributeValue);
            }
        }

        public static string KHSH
        {
            get
            {
                return ReadWriteXml.Read("DanJuMerge", "KHSH");
            }
            set
            {
                ReadWriteXml.Write("DanJuMerge", "KHSH", value);
            }
        }

        public static int Pagesize
        {
            get
            {
                string s = PropertyUtil.GetValue("pagesize");
                int result = 10;
                if (!((s != null) && int.TryParse(s, out result)))
                {
                    result = 10;
                    PropertyUtil.SetValue("pagesize", s);
                }
                if (result == 0)
                {
                    result = 10;
                }
                return result;
            }
            set
            {
                PropertyUtil.SetValue("pagesize", value.ToString());
            }
        }

        public static string SimpleComplex
        {
            get
            {
                return ReadWriteXml.Read("DanJuMerge", "SimpleComplex");
            }
            set
            {
                ReadWriteXml.Write("DanJuMerge", "SimpleComplex", value);
            }
        }

        public static string SingleDoubleTable
        {
            get
            {
                return IniRead.GetPrivateProfileString("FieldCon", "FileNumber");
            }
            set
            {
                IniRead.WritePrivateProfileString("FieldCon", "FileNumber", value);
            }
        }

        public static string TxtAmountType
        {
            get
            {
                return ReadWriteXml.Read("TxtImport", "JinEType");
            }
            set
            {
                ReadWriteXml.Write("TxtImport", "JinEType", value);
            }
        }

        public static string TxtInvType
        {
            get
            {
                return ReadWriteXml.Read("TxtImport", "InvType");
            }
            set
            {
                ReadWriteXml.Write("TxtImport", "InvType", value);
            }
        }

        public static string TxtPath
        {
            get
            {
                return ReadWriteXml.Read("TxtImport", "Path");
            }
            set
            {
                ReadWriteXml.Write("TxtImport", "Path", value);
            }
        }
    }
}

