namespace Aisino.Fwkp.Fplygl.Common
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Form.WLSL_5;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;

    internal class ShareMethods
    {
        private static string adminType = string.Empty;
        private static string cofPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Bin\");
        private static ILog loger = LogUtil.GetLogger<ShareMethods>();

        public static bool ApplyAdminCheck(string operType)
        {
            if (File.Exists(cofPath + @"\AdminType.xml"))
            {
                adminType = ApplyCommon.GetAdminType();
            }
            else
            {
                MessageManager.ShowMsgBox("INP-4412AE");
                if (ApplyCommon.DownloadAdminType())
                {
                    adminType = ApplyCommon.GetAdminType();
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-441210", new string[] { operType });
                    return false;
                }
            }
            return true;
        }

        public static bool CheckHasDownloadAllowInvType()
        {
            foreach (PZSQType type in GetTaxMode.GetTaxCard().get_SQInfo().PZSQType)
            {
                int num = Convert.ToInt32(type.invType);
                if ((((0x23 <= num) && (num <= 40)) || ((0x2f <= num) && (num <= 0x37))) || ((0x3d <= num) && (num <= 0x41)))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<InvVolumeApp> FilterOutSpecType(List<InvVolumeApp> oriList, int type)
        {
            List<InvVolumeApp> list = new List<InvVolumeApp>();
            foreach (InvVolumeApp app in oriList)
            {
                if (app.InvType == type)
                {
                    list.Add(app);
                }
            }
            return list;
        }

        public static string FPHMTo8Wei(uint iValue)
        {
            try
            {
                string format = "{0:00000000}";
                if ((iValue > 0x5f5e0ff) || (iValue < 0))
                {
                    iValue = 0;
                }
                return string.Format(format, iValue);
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return new string('0', 8);
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return new string('0', 8);
            }
        }

        public static DataTable GetFormatTable(out string key, out string value)
        {
            string columnName = "key";
            string str2 = "value";
            DataTable table = new DataTable();
            table.Columns.Add(columnName);
            table.Columns.Add(str2);
            table.PrimaryKey = new DataColumn[] { table.Columns["value"] };
            key = columnName;
            value = str2;
            DataRow row = table.NewRow();
            row[columnName] = "76mm * 177mm";
            row[str2] = "NEW76mmX177mm";
            table.Rows.Add(row);
            row = table.NewRow();
            row[columnName] = "76mm * 152mm";
            row[str2] = "NEW76mmX152mm";
            table.Rows.Add(row);
            row = table.NewRow();
            row[columnName] = "76mm * 127mm";
            row[str2] = "NEW76mmX127mm";
            table.Rows.Add(row);
            row = table.NewRow();
            row[columnName] = "57mm * 177mm";
            row[str2] = "NEW57mmX177mm";
            table.Rows.Add(row);
            row = table.NewRow();
            row[columnName] = "57mm * 152mm";
            row[str2] = "NEW57mmX152mm";
            table.Rows.Add(row);
            row = table.NewRow();
            row[columnName] = "57mm * 127mm";
            row[str2] = "NEW57mmX127mm";
            table.Rows.Add(row);
            row = table.NewRow();
            row[columnName] = "76mm * 177mm（北京）";
            row[str2] = "BJ76mmX177mm";
            table.Rows.Add(row);
            row = table.NewRow();
            row[columnName] = "76mm * 177mm（上海）";
            row[str2] = "SH76mmX177mm";
            table.Rows.Add(row);
            row = table.NewRow();
            row[columnName] = "76mm * 177mm（黑龙江）";
            row[str2] = "HLJ76mmX177mm";
            table.Rows.Add(row);
            row = table.NewRow();
            row[columnName] = "76mm * 127mm（云南）";
            row[str2] = "YN76mmX127mm";
            table.Rows.Add(row);
            row = table.NewRow();
            row[columnName] = "76mm * 177mm（云南）";
            row[str2] = "YN76mmX177mm";
            table.Rows.Add(row);
            Dictionary<string, int> jsPrintTemplate = ToolUtil.GetJsPrintTemplate();
            if (jsPrintTemplate.Count > 0)
            {
                foreach (string str3 in jsPrintTemplate.Keys)
                {
                    if (table.Rows.Find(str3) == null)
                    {
                        row = table.NewRow();
                        row[columnName] = str3;
                        row[str2] = str3;
                        table.Rows.Add(row);
                    }
                }
            }
            return table;
        }

        public static Dictionary<string, object> GetFPLBBM()
        {
            try
            {
                IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Clear();
                foreach (Dictionary<string, object> dictionary2 in baseDAOSQLite.querySQL("aisino.Fwkp.Fplygl.FPLBBM.AllData", dictionary))
                {
                    string key = dictionary2["BM"].ToString().Trim();
                    string str2 = dictionary2["MC"].ToString().Trim();
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary.Add(key, str2);
                    }
                }
                return dictionary;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }

        public static string GetFPLBMC(InvVolumeApp invVolume, Dictionary<string, object> dictFPLBBM)
        {
            try
            {
                string invType = string.Empty;
                invType = GetInvType(invVolume.InvType);
                string typeCode = invVolume.TypeCode;
                string key = string.Empty;
                string str4 = string.Empty;
                if (!string.IsNullOrEmpty(typeCode))
                {
                    typeCode = typeCode.Trim();
                    if (2 <= typeCode.Length)
                    {
                        if (10 == typeCode.Length)
                        {
                            key = typeCode.Substring(0, 2) + "00";
                        }
                        else if (12 == typeCode.Length)
                        {
                            key = typeCode.Substring(1, 2) + "00";
                        }
                    }
                    else
                    {
                        typeCode = string.Empty;
                        key = string.Empty;
                    }
                }
                else
                {
                    typeCode = string.Empty;
                    key = string.Empty;
                }
                if ((dictFPLBBM != null) && (dictFPLBBM.Count > 0))
                {
                    str4 = dictFPLBBM.ContainsKey(key) ? dictFPLBBM[key].ToString().Trim() : string.Empty;
                    str4 = str4.Trim() + invType;
                }
                return str4;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return string.Empty;
            }
        }

        public static string GetInvType(byte typeCode)
        {
            switch (typeCode)
            {
                case 0:
                    return "增值税专用发票";

                case 2:
                    return "增值税普通发票";

                case 11:
                    return "货物运输业增值税专用发票";

                case 12:
                    return "机动车销售统一发票";

                case 0x29:
                    return "增值税普通发票(卷票)";

                case 0x33:
                    return "电子增值税普通发票";
            }
            return "未知类型发票";
        }

        public static byte GetTypeCode(string typeName)
        {
            byte num = 0xff;
            string str = typeName;
            if (str == null)
            {
                return num;
            }
            if (!(str == "增值税专用发票"))
            {
                if (str != "增值税普通发票")
                {
                    if (str == "货物运输业增值税专用发票")
                    {
                        return 11;
                    }
                    if (str == "机动车销售统一发票")
                    {
                        return 12;
                    }
                    if (str == "电子增值税普通发票")
                    {
                        return 0x33;
                    }
                    if (str != "增值税普通发票(卷票)")
                    {
                        return num;
                    }
                    return 0x29;
                }
            }
            else
            {
                return 0;
            }
            return 2;
        }

        public static double GetUpLimit(InvSQInfo invUpLimit, string strFpzl)
        {
            foreach (PZSQType type in invUpLimit.PZSQType)
            {
                if (type.invType.Equals((InvoiceType) 0) && strFpzl.Equals("增值税专用发票"))
                {
                    return type.InvAmountLimit;
                }
                if (type.invType.Equals((InvoiceType) 2) && strFpzl.Equals("增值税普通发票"))
                {
                    return type.InvAmountLimit;
                }
                if (type.invType.Equals((InvoiceType) 12) && strFpzl.Equals("机动车销售统一发票"))
                {
                    return type.InvAmountLimit;
                }
                if (type.invType.Equals((InvoiceType) 11) && strFpzl.Equals("货物运输业增值税专用发票"))
                {
                    return type.InvAmountLimit;
                }
                if (type.invType.Equals((InvoiceType) 0x33) && strFpzl.Equals("电子增值税普通发票"))
                {
                    return type.InvAmountLimit;
                }
                if (type.invType.Equals((InvoiceType) 0x29) && strFpzl.Equals("增值税普通发票(卷票)"))
                {
                    return type.InvAmountLimit;
                }
            }
            return 0.0;
        }

        public static int GetVolumnEndNum(InvVolumeApp invVolumn)
        {
            return (((int) (invVolumn.HeadCode + invVolumn.Number)) - 1);
        }

        public static int GetVolumnStartNum(InvVolumeApp invVolumn)
        {
            return (((int) (invVolumn.HeadCode + invVolumn.Number)) - invVolumn.BuyNumber);
        }

        public static bool IsHXInv(int type)
        {
            if ((type != 0) && (2 != type))
            {
                return false;
            }
            return true;
        }

        public static bool IsZCInv(int type)
        {
            if (((11 != type) && (12 != type)) && ((0x33 != type) && (0x29 != type)))
            {
                return false;
            }
            return true;
        }

        public static int StringToInt(string strTemp)
        {
            int result = 0;
            try
            {
                int.TryParse(strTemp.Trim(), out result);
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return result;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return result;
            }
            return result;
        }
    }
}

