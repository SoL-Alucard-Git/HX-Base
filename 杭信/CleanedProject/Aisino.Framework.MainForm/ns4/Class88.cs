namespace ns4
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal static class Class88
    {
        public static void smethod_0()
        {
            try
            {
                lock (Class97.dataTable_0)
                {
                    List<string> list = new List<string>();
                    List<Dictionary<string, object>> parameter = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> list3 = new List<Dictionary<string, object>>();
                    List<Dictionary<string, string>> list4 = new List<Dictionary<string, string>>();
                    Class89 class2 = new Class89();
                    IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                    TaxCard card = TaxCard.CreateInstance(CTaxCardType.const_7);
                    DataRow[] rowArray = Class97.dataTable_0.Select("isDBUpdated=false AND FPStatus = '3'");
                    if ((rowArray != null) && (rowArray.Length >= 1))
                    {
                        foreach (DataRow row in rowArray)
                        {
                            if (row["FPStatus"].ToString() == "3")
                            {
                                list.Add("Aisino.Framework.MainForm.UpDown.UpdateFpStatus");
                                parameter.Add(class2.method_0(row));
                                list.Add("Aisino.Framework.MainForm.UpDown.replaceFPSLH");
                                parameter.Add(class2.method_1(row));
                                list3.Add(class2.method_2(row));
                                if (card.SubSoftVersion == "Linux")
                                {
                                    list4.Add(class2.method_4(row, "up"));
                                }
                            }
                        }
                        if (card.SubSoftVersion == "Linux")
                        {
                            string str = new Class84().method_30(list4);
                            Class101.smethod_0("发送给Linux服务器端数据：" + str);
                            Class101.smethod_0("GeneralCmdHandle返回：" + card.GeneralCmdHandle("C028", str));
                            string errCode = card.ErrCode;
                            Class101.smethod_0(string.Concat(new object[] { "ErrorCode:", errCode, "   retCode:", card.RetCode }));
                            string messageInfo = MessageManager.GetMessageInfo(errCode);
                            Class101.smethod_0("RetCode:" + card.RetCode);
                            Class101.smethod_0("底层返回信息：ErrCode：" + errCode + "  描述信息：" + messageInfo);
                            if (((card.RetCode == 0) && (baseDAOSQLite.未确认DAO方法1(list.ToArray(), parameter) > 0)) && (list3.Count >= 1))
                            {
                                for (int i = 0; i < list3.Count; i++)
                                {
                                    class2.method_3(list3[i]);
                                    Class101.smethod_0("（上传线程）更新临时表：发票号码：" + list3[i]["FPHM"].ToString().PadLeft(8, '0') + "  发票代码：" + list3[i]["FPDM"].ToString() + "  发票状态：3");
                                }
                            }
                        }
                        else if ((baseDAOSQLite.未确认DAO方法1(list.ToArray(), parameter) > 0) && (list3.Count >= 1))
                        {
                            for (int j = 0; j < list3.Count; j++)
                            {
                                class2.method_3(list3[j]);
                                Class101.smethod_0("（上传线程）更新临时表：发票号码：" + list3[j]["FPHM"].ToString().PadLeft(8, '0') + "  发票代码：" + list3[j]["FPDM"].ToString() + "  发票状态：3");
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("(上传线程)批量更新数据库（3）失败！" + exception.ToString());
            }
        }

        public static void smethod_1()
        {
            try
            {
                lock (Class97.dataTable_0)
                {
                    List<string> list = new List<string>();
                    List<Dictionary<string, object>> parameter = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> list3 = new List<Dictionary<string, object>>();
                    Class89 class2 = new Class89();
                    IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                    List<Dictionary<string, string>> list4 = new List<Dictionary<string, string>>();
                    TaxCard card = TaxCard.CreateInstance(CTaxCardType.const_7);
                    DataRow[] rowArray = Class97.dataTable_0.Select("(FPStatus = '0' OR FPStatus = '2' OR FPStatus = '4') AND isDBUpdated=false");
                    if ((rowArray != null) && (rowArray.Length >= 1))
                    {
                        Class81 class3 = new Class81();
                        foreach (DataRow row in rowArray)
                        {
                            if (row["FPStatus"].ToString() == "0")
                            {
                                list.Add("Aisino.Framework.MainForm.UpDown.UpdateFpStatus");
                                parameter.Add(class2.method_0(row));
                                list.Add("Aisino.Framework.MainForm.UpDown.replaceFPSLH");
                                parameter.Add(class2.method_1(row));
                            }
                            else
                            {
                                if (!row["Fplx"].ToString().Equals("DZFP"))
                                {
                                    class3.method_6(row["FPNO"].ToString(), row["FPDM"].ToString(), row["Fplx"].ToString(), row["FPStatus"].ToString());
                                }
                                list.Add("Aisino.Framework.MainForm.UpDown.DeleteFPSLH");
                                parameter.Add(class2.method_1(row));
                            }
                            list3.Add(class2.method_2(row));
                            if (card.SubSoftVersion == "Linux")
                            {
                                list4.Add(class2.method_4(row, "down"));
                            }
                        }
                        if (card.SubSoftVersion == "Linux")
                        {
                            string str = new Class84().method_30(list4);
                            Class101.smethod_0("发送给Linux服务器端数据：" + str);
                            Class101.smethod_0("GeneralCmdHandle返回：" + card.GeneralCmdHandle("C028", str));
                            string errCode = card.ErrCode;
                            Class101.smethod_0(string.Concat(new object[] { "ErrorCode:", errCode, "   retCode:", card.RetCode }));
                            string messageInfo = MessageManager.GetMessageInfo(errCode);
                            Class101.smethod_0("RetCode:" + card.RetCode);
                            Class101.smethod_0("底层返回信息：ErrCode：" + errCode + "  描述信息：" + messageInfo);
                            if (((card.RetCode == 0) && (baseDAOSQLite.未确认DAO方法1(list.ToArray(), parameter) > 0)) && (list3.Count >= 1))
                            {
                                for (int i = 0; i < list3.Count; i++)
                                {
                                    class2.method_3(list3[i]);
                                    Class101.smethod_0("（下载线程）更新数据库：发票代码：" + list3[i]["FPDM"].ToString() + "   发票号码：" + list3[i]["FPHM"].ToString() + "   报送状态：" + list3[i]["BSZT"].ToString());
                                }
                            }
                        }
                        else if ((baseDAOSQLite.未确认DAO方法1(list.ToArray(), parameter) > 0) && (list3.Count >= 1))
                        {
                            for (int j = 0; j < list3.Count; j++)
                            {
                                class2.method_3(list3[j]);
                                Class101.smethod_0("（下载线程）更新数据库：发票代码：" + list3[j]["FPDM"].ToString() + "   发票号码：" + list3[j]["FPHM"].ToString() + "   报送状态：" + list3[j]["BSZT"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("（下载线程）批量更新数据库（0-2）失败！" + exception.ToString());
            }
        }

        public static void smethod_2()
        {
            try
            {
                lock (Class97.dataTable_0)
                {
                    List<string> list = new List<string>();
                    List<Dictionary<string, object>> parameter = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> list3 = new List<Dictionary<string, object>>();
                    Class89 class2 = new Class89();
                    IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                    List<Dictionary<string, string>> list4 = new List<Dictionary<string, string>>();
                    TaxCard card = TaxCard.CreateInstance(CTaxCardType.const_7);
                    DataRow[] rowArray = Class97.dataTable_0.Select("isDBUpdated=false AND (FPStatus = '1' or FPStatus = '999')");
                    Class101.smethod_0("UpdateDBByDownloadMethod drFP.count：" + rowArray.Length);
                    if ((rowArray != null) && (rowArray.Length >= 1))
                    {
                        foreach (DataRow row in rowArray)
                        {
                            list.Add("Aisino.Framework.MainForm.UpDown.UpdateFpDBSuccStatus");
                            parameter.Add(class2.method_0(row));
                            list.Add("Aisino.Framework.MainForm.UpDown.DeleteFPSLH");
                            parameter.Add(class2.method_1(row));
                            list3.Add(class2.method_2(row));
                            if (card.SubSoftVersion == "Linux")
                            {
                                list4.Add(class2.method_4(row, "down"));
                            }
                        }
                        if (card.SubSoftVersion == "Linux")
                        {
                            string str = new Class84().method_30(list4);
                            Class101.smethod_0("发送给Linux服务器端数据：" + str);
                            Class101.smethod_0("GeneralCmdHandle返回：" + card.GeneralCmdHandle("C028", str));
                            string errCode = card.ErrCode;
                            Class101.smethod_0(string.Concat(new object[] { "ErrorCode:", errCode, "   retCode:", card.RetCode }));
                            string messageInfo = MessageManager.GetMessageInfo(errCode);
                            Class101.smethod_0("RetCode:" + card.RetCode);
                            Class101.smethod_0("底层返回信息：ErrCode：" + errCode + "  描述信息：" + messageInfo);
                            if (((card.RetCode == 0) && (baseDAOSQLite.未确认DAO方法1(list.ToArray(), parameter) > 0)) && (list3.Count >= 1))
                            {
                                for (int i = 0; i < list3.Count; i++)
                                {
                                    class2.method_3(list3[i]);
                                    Class101.smethod_0("（下载线程-UpdateDBByDownloadMethod）更新数据库：发票号码：" + list3[i]["FPHM"].ToString().PadLeft(8, '0') + "  发票代码：" + list3[i]["FPDM"].ToString() + "  发票状态：1或2");
                                }
                            }
                        }
                        else if ((baseDAOSQLite.未确认DAO方法1(list.ToArray(), parameter) > 0) && (list3.Count >= 1))
                        {
                            for (int j = 0; j < list3.Count; j++)
                            {
                                class2.method_3(list3[j]);
                                Class101.smethod_0("（下载线程-UpdateDBByDownloadMethod）更新数据库：发票号码：" + list3[j]["FPHM"].ToString().PadLeft(8, '0') + "  发票代码：" + list3[j]["FPDM"].ToString() + "  发票状态：1");
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("（下载线程）批量更新数据库（1）失败！" + exception.ToString());
            }
        }

        public static void smethod_3()
        {
            try
            {
                if (Class87.list_0.Count >= 1)
                {
                    IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                    new Dictionary<string, string>();
                    TaxCard.CreateInstance(CTaxCardType.const_7);
                    List<string> list = new List<string>();
                    for (int i = 0; i < Class87.list_0.Count; i++)
                    {
                        if (Class87.list_0[i].ContainsKey("BSRZ"))
                        {
                            list.Add("Aisino.Framework.MainForm.UpDown.UpdateBSRZ");
                            Class87.list_0[i]["BSRZ"] = "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" + Class87.list_0[i]["BSRZ"].ToString();
                            if (Class87.list_0[i]["BSRZ"].ToString().Length > 0xc3)
                            {
                                Class87.list_0[i]["BSRZ"] = Class87.list_0[i]["BSRZ"].ToString().Substring(0, 0xc3);
                            }
                        }
                    }
                    if (baseDAOSQLite.未确认DAO方法1(list.ToArray(), Class87.list_0) > 0)
                    {
                        Class87.list_0.Clear();
                    }
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("(上传线程)更改发票报送日志出错：" + exception.ToString());
            }
        }

        public static void smethod_4()
        {
            try
            {
                if (Class87.list_1.Count >= 1)
                {
                    IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                    new Dictionary<string, string>();
                    TaxCard.CreateInstance(CTaxCardType.const_7);
                    List<string> list = new List<string>();
                    for (int i = 0; i < Class87.list_1.Count; i++)
                    {
                        if (Class87.list_1[i].ContainsKey("BSRZ"))
                        {
                            list.Add("Aisino.Framework.MainForm.UpDown.UpdateBSRZ");
                            Class87.list_1[i]["BSRZ"] = "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" + Class87.list_1[i]["BSRZ"].ToString();
                            if (Class87.list_1[i]["BSRZ"].ToString().Length > 0xc3)
                            {
                                Class87.list_1[i]["BSRZ"] = Class87.list_1[i]["BSRZ"].ToString().Substring(0, 0xc3);
                            }
                        }
                    }
                    if (baseDAOSQLite.未确认DAO方法1(list.ToArray(), Class87.list_1) > 0)
                    {
                        Class87.list_1.Clear();
                    }
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("(下载线程)更改发票报送日志出错：" + exception.ToString());
            }
        }
    }
}

