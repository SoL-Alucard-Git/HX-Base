namespace Aisino.Fwkp.Fpzpz.Common
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Plugin.Core.WebService;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpzpz.BLL;
    using Aisino.Fwkp.Fpzpz.Form;
    using Aisino.Fwkp.Fpzpz.IBLL;
    using Aisino.Fwkp.Fpzpz.Model;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    public class Tool
    {
        private static bool _FistFindIsA6Version = true;
        private static bool _IsA6Version = false;
        private static ILog _Loger = LogUtil.GetLogger<Tool>();
        private static FPZPZSet qyglrjMsgSet = new FPZPZSet();
        private static IXXFP xxfpChaXunBll = new XXFP();

        public static bool AddAreaIDToCusrTBL()
        {
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            baseDAOSQLite.Open();
            try
            {
                IKHBM ikhbm = new KHBM();
                IXZQYBM ixzqybm = new XZQYBM();
                List<KHBMModal> list = ikhbm.SelectKHBM_WJ(1);
                if (list == null)
                {
                    return false;
                }
                if (0 < list.Count)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        KHBMModal modal = list[i];
                        string sH = modal.SH;
                        if (sH.Length >= 4)
                        {
                            if (!string.IsNullOrEmpty(sH) && (4 <= sH.Length))
                            {
                                ikhbm.UpdateKHBM_DQBM(modal.BM, sH.Substring(0, 4));
                            }
                            string strBM = sH.Substring(0, 4);
                            List<XZQYBMModal> list2 = ixzqybm.SelectXZQYBM_BM(strBM);
                            if ((list2 != null) && (0 < list2.Count))
                            {
                                ikhbm.UpdateKHBM_DQMC(modal.BM, list2[0].MC.Trim());
                            }
                        }
                    }
                    PropertyUtil.SetValue(DingYiZhiFuChuan.A6KHMsgWrite, "已经添加地区代码到客户编码表");
                }
                else
                {
                    return false;
                }
            }
            catch (BaseException exception)
            {
                writeLogfile("Errinfo:" + exception.Message, _Loger);
                _Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                writeLogfile("Errinfo:" + exception2.Message, _Loger);
                _Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
            finally
            {
                baseDAOSQLite.Commit();
                baseDAOSQLite.Close();
            }
            return true;
        }

        public static List<Fpxx> ArrayListToListFpxx(ArrayList arrayList, ILog loger)
        {
            try
            {
                if (arrayList == null)
                {
                    return null;
                }
                if (0 >= arrayList.Count)
                {
                    return null;
                }
                List<Fpxx> list = new List<Fpxx>();
                foreach (object obj2 in arrayList)
                {
                    Dictionary<string, object> dictionary = obj2 as Dictionary<string, object>;
                    Fpxx item = new Fpxx();
                    string str = dictionary[DingYiZhiFuChuan.XXFPCulmnDataName[0]].ToString();
                    if (str.Equals(DingYiZhiFuChuan.strFPZL_s_c[1]))
                    {
                        item.fplx = 2;
                    }
                    else if (str.Equals(DingYiZhiFuChuan.strFPZL_s_c[0]))
                    {
                        item.fplx = 0;
                    }
                    else if (str.Equals(DingYiZhiFuChuan.strFPZL_s_c[2]))
                    {
                        item.fplx = 12;
                    }
                    else if (str.Equals(DingYiZhiFuChuan.strFPZL_s_c[3]))
                    {
                        item.fplx = 11;
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.XXFPCulmnDataName[1]))
                    {
                        item.fpdm = dictionary[DingYiZhiFuChuan.XXFPCulmnDataName[1]].ToString();
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.XXFPCulmnDataName[2]))
                    {
                        item.fphm = dictionary[DingYiZhiFuChuan.XXFPCulmnDataName[2]].ToString();
                        int result = 0;
                        int.TryParse(item.fphm, out result);
                        item.fphm = string.Format("{0:00000000}", result);
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.XXFPCulmnDataName[5]))
                    {
                        item.gfmc = dictionary[DingYiZhiFuChuan.XXFPCulmnDataName[5]].ToString();
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.XXFPCulmnDataName[6]))
                    {
                        item.gfsh = dictionary[DingYiZhiFuChuan.XXFPCulmnDataName[6]].ToString();
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.XXFPCulmnDataName[0x11]))
                    {
                        item.kprq = dictionary[DingYiZhiFuChuan.XXFPCulmnDataName[0x11]].ToString();
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.XXFPCulmnDataName[0x13]))
                    {
                        item.je = dictionary[DingYiZhiFuChuan.XXFPCulmnDataName[0x13]].ToString();
                        double num2 = 0.0;
                        double.TryParse(item.je, out num2);
                        item.je = string.Format("{0:F2}", num2);
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.XXFPCulmnDataName[0x15]))
                    {
                        item.se = dictionary[DingYiZhiFuChuan.XXFPCulmnDataName[0x15]].ToString();
                        double num3 = 0.0;
                        double.TryParse(item.se, out num3);
                        item.se = string.Format("{0:F2}", num3);
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.XXFPCulmnDataName[0x22]))
                    {
                        string str2 = dictionary[DingYiZhiFuChuan.XXFPCulmnDataName[0x22]].ToString();
                        item.zfbz = Convert.ToBoolean(str2);
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.XXFPCulmnDataName[0x1f]))
                    {
                        item.gfbh = dictionary[DingYiZhiFuChuan.XXFPCulmnDataName[0x1f]].ToString();
                    }
                    item.CustomFields = new Dictionary<string, object>();
                    string key = DingYiZhiFuChuan.PZCulmnDataName[0];
                    if (dictionary.ContainsKey(key))
                    {
                        string str4 = dictionary[key].ToString();
                        item.CustomFields.Add(key, str4);
                    }
                    key = DingYiZhiFuChuan.PZCulmnDataName[1];
                    if (dictionary.ContainsKey(key))
                    {
                        item.CustomFields.Add(key, dictionary[key].ToString());
                    }
                    key = DingYiZhiFuChuan.PZCulmnDataName[2];
                    if (dictionary.ContainsKey(key))
                    {
                        string s = dictionary[key].ToString();
                        int num4 = 0;
                        int.TryParse(s, out num4);
                        if (num4 > 0)
                        {
                            s = string.Format("{0:0000}", num4);
                        }
                        else
                        {
                            s = "";
                        }
                        item.CustomFields.Add(key, s);
                    }
                    key = DingYiZhiFuChuan.PZCulmnDataName[3];
                    if (dictionary.ContainsKey(key))
                    {
                        item.CustomFields.Add(key, dictionary[key].ToString());
                    }
                    key = DingYiZhiFuChuan.PZCulmnDataName[4];
                    if (dictionary.ContainsKey(key))
                    {
                        string str6 = dictionary[key].ToString();
                        int num5 = 0;
                        int.TryParse(str6, out num5);
                        switch (num5)
                        {
                            case 0:
                                str6 = "未审核";
                                break;

                            case 1:
                                str6 = "已审核";
                                break;

                            case 2:
                                str6 = "已记账";
                                break;

                            case 3:
                                str6 = "暂存";
                                break;

                            default:
                                str6 = string.Empty;
                                break;
                        }
                        if (dictionary.ContainsKey(DingYiZhiFuChuan.PZCulmnDataName[12]))
                        {
                            item.CustomFields.Add(DingYiZhiFuChuan.PZCulmnDataName[12], dictionary[DingYiZhiFuChuan.PZCulmnDataName[12]].ToString());
                        }
                        item.CustomFields.Add(key, str6);
                    }
                    key = DingYiZhiFuChuan.PZCulmnDataName[5];
                    if (dictionary.ContainsKey(key))
                    {
                        item.CustomFields.Add(key, dictionary[key].ToString());
                    }
                    list.Add(item);
                }
                return list;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public static List<KHBMModal> ArrayListToListKHBMModal(ArrayList arrayList, ILog loger)
        {
            try
            {
                if (arrayList == null)
                {
                    return null;
                }
                if (0 >= arrayList.Count)
                {
                    return null;
                }
                List<KHBMModal> list = new List<KHBMModal>();
                foreach (object obj2 in arrayList)
                {
                    Dictionary<string, object> dictionary = obj2 as Dictionary<string, object>;
                    KHBMModal item = new KHBMModal {
                        BM = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[0]].ToString(),
                        MC = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[1]].ToString(),
                        JM = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[2]].ToString(),
                        SJBM = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[3]].ToString(),
                        KJM = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[4]].ToString(),
                        SH = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[5]].ToString(),
                        DZDH = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[6]].ToString(),
                        YHZH = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[8]].ToString(),
                        YJDZ = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[9]].ToString(),
                        BZ = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[10]].ToString(),
                        YSKM = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[11]].ToString(),
                        DQBM = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[12]].ToString(),
                        DQMC = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[13]].ToString(),
                        DQKM = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[14]].ToString()
                    };
                    string s = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[0x10]].ToString();
                    int result = 0;
                    int.TryParse(s, out result);
                    item.WJ = result;
                    list.Add(item);
                }
                return list;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public static List<KHBMModal> ArrayListToListKHKMBModal(ArrayList arrayList, ILog loger)
        {
            try
            {
                if (arrayList == null)
                {
                    return null;
                }
                if (0 >= arrayList.Count)
                {
                    return null;
                }
                List<KHBMModal> list = new List<KHBMModal>();
                foreach (object obj2 in arrayList)
                {
                    Dictionary<string, object> dictionary = obj2 as Dictionary<string, object>;
                    KHBMModal item = new KHBMModal {
                        BM = dictionary["KHBH"].ToString(),
                        SJBM = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[3]].ToString(),
                        YSKM = dictionary[DingYiZhiFuChuan.KHBMCulmnDataName[11]].ToString()
                    };
                    list.Add(item);
                }
                return list;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public static List<TPZEntry_InfoModal> ArrayListToListPZFLB(ArrayList arrayList, ILog loger)
        {
            try
            {
                if (arrayList == null)
                {
                    return null;
                }
                if (0 >= arrayList.Count)
                {
                    return null;
                }
                List<TPZEntry_InfoModal> list = new List<TPZEntry_InfoModal>();
                foreach (object obj2 in arrayList)
                {
                    Dictionary<string, object> dictionary = obj2 as Dictionary<string, object>;
                    TPZEntry_InfoModal item = new TPZEntry_InfoModal();
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[0]))
                    {
                        int.TryParse(dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[0]].ToString(), out item.PZEntry_Group);
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[1]))
                    {
                        item.PZEntry_KindcodeNo = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[1]].ToString();
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[2]))
                    {
                        item.PZEntry_CusrID = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[2]].ToString();
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[3]))
                    {
                        decimal.TryParse(dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[3]].ToString(), out item.PZEntry_JE);
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[4]))
                    {
                        item.PZEntry_SkKm = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[4]].ToString();
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[5]))
                    {
                        int.TryParse(dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[5]].ToString(), out item.PZEntry_JDSFlag);
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[6]))
                    {
                        item.PZEntry_GooDsID = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[6]].ToString();
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[7]))
                    {
                        double.TryParse(dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[7]].ToString(), out item.PZEntry_SPNum);
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[8]))
                    {
                        double.TryParse(dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[8]].ToString(), out item.PZEntry_SPPrice);
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[9]))
                    {
                        int.TryParse(dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[9]].ToString(), out item.PZEntry_NumCheck);
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[10]))
                    {
                        string s = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[10]].ToString();
                        item.PZEntry_Date = DateTime.ParseExact(s, DingYiZhiFuChuan.strYear_Month_Day_Formart, CultureInfo.CurrentCulture);
                    }
                    if (dictionary.ContainsKey(DingYiZhiFuChuan.PZFLBCulmnDataName[11]))
                    {
                        item.PZEntry_Jldw = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[11]].ToString();
                    }
                    list.Add(item);
                }
                return list;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public static List<SPBMModal> ArrayListToListSPBMModal(ArrayList arrayList, ILog loger)
        {
            try
            {
                if (arrayList == null)
                {
                    return null;
                }
                if (0 >= arrayList.Count)
                {
                    return null;
                }
                List<SPBMModal> list = new List<SPBMModal>();
                foreach (object obj2 in arrayList)
                {
                    Dictionary<string, object> dictionary = obj2 as Dictionary<string, object>;
                    SPBMModal item = new SPBMModal {
                        BM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[0]].ToString(),
                        MC = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[1]].ToString(),
                        JM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[2]].ToString(),
                        SJBM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[3]].ToString(),
                        KJM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[4]].ToString()
                    };
                    double result = 0.0;
                    double.TryParse(dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[5]].ToString(), out result);
                    item.SLV = result;
                    item.SPSM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[6]].ToString();
                    item.GGXH = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[7]].ToString();
                    item.JLDW = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[8]].ToString();
                    double num2 = 0.0;
                    double.TryParse(dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[9]].ToString(), out num2);
                    item.DJ = num2;
                    item.XSSRKM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[11]].ToString();
                    item.YJZZSKM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[12]].ToString();
                    item.XSTHKM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[13]].ToString();
                    item.SPZL = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[15]].ToString();
                    item.SPSX = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[0x10]].ToString();
                    string s = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[0x10]].ToString();
                    int num3 = 0;
                    int.TryParse(s, out num3);
                    item.WJ = num3;
                    list.Add(item);
                }
                return list;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public static List<SPBMModal> ArrayListToListSPKMBModal(ArrayList arrayList, ILog loger)
        {
            try
            {
                if (arrayList == null)
                {
                    return null;
                }
                if (0 >= arrayList.Count)
                {
                    return null;
                }
                List<SPBMModal> list = new List<SPBMModal>();
                foreach (object obj2 in arrayList)
                {
                    Dictionary<string, object> dictionary = obj2 as Dictionary<string, object>;
                    SPBMModal item = new SPBMModal {
                        BM = dictionary["SPBH"].ToString(),
                        SJBM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[3]].ToString(),
                        XSSRKM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[11]].ToString(),
                        YJZZSKM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[12]].ToString(),
                        XSTHKM = dictionary[DingYiZhiFuChuan.SPBMCulmnDataName[13]].ToString()
                    };
                    list.Add(item);
                }
                return list;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public static List<XZQYBMModal> ArrayListToListXZQYBMModal(ArrayList arrayList, ILog loger)
        {
            try
            {
                if (arrayList == null)
                {
                    return null;
                }
                if (0 >= arrayList.Count)
                {
                    return null;
                }
                List<XZQYBMModal> list = new List<XZQYBMModal>();
                foreach (object obj2 in arrayList)
                {
                    Dictionary<string, object> dictionary = obj2 as Dictionary<string, object>;
                    XZQYBMModal item = new XZQYBMModal {
                        BM = dictionary[DingYiZhiFuChuan.XZQYBMCulmnDataName[0]].ToString(),
                        MC = dictionary[DingYiZhiFuChuan.XZQYBMCulmnDataName[1]].ToString()
                    };
                    list.Add(item);
                }
                return list;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public static AisinoDataSet ChaXunDate(int page, int num, Dictionary<string, object> dict, FP_PZ fp_pz)
        {
            return null;
        }

        public static string GetJshj(Fpxx fpxxModal, ILog loger)
        {
            try
            {
                double result = 0.0;
                double num2 = 0.0;
                double num3 = 0.0;
                double.TryParse(fpxxModal.je, out result);
                double.TryParse(fpxxModal.se, out num2);
                num3 = result + num2;
                return Convert.ToDouble(num3).ToString("0.00");
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return string.Empty;
        }

        public static bool IsA6Version()
        {
            try
            {
                if (_FistFindIsA6Version)
                {
                    _FistFindIsA6Version = false;
                    DingYiZhiFuChuan.GetLinkUserSuit();
                    if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6ServerLink))
                    {
                        _IsA6Version = false;
                        return _IsA6Version;
                    }
                    string str = DingYiZhiFuChuan.A6ServerLink + "/pzWebService.ws";
                    writeLogfile("A6Link:" + str, _Loger);
                    if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6SuitGuid) || string.IsNullOrEmpty(DingYiZhiFuChuan.A6UserGuid))
                    {
                        _IsA6Version = false;
                        return _IsA6Version;
                    }
                    string str2 = DingYiZhiFuChuan.A6SuitGuid;
                    string str3 = DingYiZhiFuChuan.A6UserGuid;
                    str2 = str2.Substring(0, str2.IndexOf("="));
                    str3 = str3.Substring(0, str3.IndexOf("="));
                    object obj2 = new object();
                    string str4 = "getAcctGUID";
                    string[] strArray2 = new string[2];
                    strArray2[0] = str2;
                    string[] strArray = strArray2;
                    string str5 = (string) WebServiceFactory.InvokeWebService(str, str4, strArray);
                    if (str5.Length <= 0)
                    {
                        _IsA6Version = false;
                        return _IsA6Version;
                    }
                    if (str5.Equals("1"))
                    {
                        _IsA6Version = true;
                        return _IsA6Version;
                    }
                    _IsA6Version = false;
                }
                return _IsA6Version;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message + "  不是A6平台");
                _IsA6Version = false;
                return _IsA6Version;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message + "  不是A6平台");
                _IsA6Version = false;
                return _IsA6Version;
            }
        }

        public static string IsRightA6Info()
        {
            try
            {
                qyglrjMsgSet.PzInfoInit();
                return qyglrjMsgSet.IsRightA6Info();
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
            return string.Empty;
        }

        public static bool PzInterFaceLinkInfo(string strErrID, ILog loger)
        {
            try
            {
                MessageManager.ShowMsgBox(strErrID);
                return false;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return false;
        }

        public static string RMBToDaXie(decimal num)
        {
            try
            {
                if (num == 0M)
                {
                    return "零";
                }
                string str = "零壹贰叁肆伍陆柒捌玖";
                string str2 = "万仟佰拾亿仟佰拾万仟佰拾元";
                string str3 = "角分";
                string str4 = string.Empty;
                string[] strArray = num.ToString().Split(new char[] { '.' });
                if (strArray.Length > 0)
                {
                    if (strArray[0].Length > 14)
                    {
                        throw new Exception("值越界");
                    }
                    bool flag = true;
                    for (int i = 0; i < strArray[0].Length; i++)
                    {
                        string str5 = strArray[0].Substring((strArray[0].Length - i) - 1, 1);
                        if ((str5 != "0") || !flag)
                        {
                            flag = false;
                            int startIndex = Convert.ToInt32(str5);
                            str5 = str.Substring(startIndex, 1);
                            if (i < 13)
                            {
                                string str6 = str2.Substring((str2.Length - 1) - i, 1);
                                str4 = str5 + str6 + str4;
                            }
                            else
                            {
                                str4 = str5 + str4;
                            }
                        }
                    }
                    str4 = str4.TrimEnd(new char[] { '元' }) + "元";
                }
                if (strArray.Length > 1)
                {
                    strArray[1] = strArray[1].TrimEnd(new char[] { '0' });
                    for (int j = 0; j < strArray[1].Length; j++)
                    {
                        if (j > 1)
                        {
                            break;
                        }
                        string str7 = strArray[1].Substring(j, 1);
                        str7 = str.Substring(Convert.ToInt32(str7), 1);
                        str4 = str4 + str7 + str3[j];
                    }
                }
                return (str4 + "整");
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
            return string.Empty;
        }

        public static bool writeLogfile(string msgstr, ILog loger)
        {
            return false;
        }

        public enum FP_PZ
        {
            FPFind,
            PZFind
        }
    }
}

