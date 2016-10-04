namespace Aisino.Fwkp.Bmgl.BLL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.DAL;
    using Aisino.Fwkp.Bmgl.IBLL;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using Factory;
    internal sealed class BMSPFLManager : IBMBaseManager
    {
        private int _currentPage = 1;
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private DAL.BMSPFLManager bmspflDAL = DALFactory.CreateInstant<DAL.BMSPFLManager>("BMSPFLManager");

        public DataTable AppendByKey(string KeyWord, int TopNo, bool isSPBMSel)
        {
            return this.bmspflDAL.AppendByKey(KeyWord, TopNo, isSPBMSel);
        }

        public void AutoImportDatabase(string codeFile)
        {
            Dictionary<string, object> dictionary;
            XmlDocument document = new XmlDocument();
            document.Load(codeFile);
            string innerText = document.GetElementsByTagName("MWJY")[0].InnerText;
            string text1 = document.GetElementsByTagName("COUNT")[0].InnerText;
            string str = string.Empty;
            XmlNodeList elementsByTagName = document.GetElementsByTagName("BBH");
            if (elementsByTagName != null)
            {
                int count = elementsByTagName.Count;
            }
            string maxBMBBBH = this.GetMaxBMBBBH();
            string s = elementsByTagName[0].InnerText;
            float.Parse(s);
            float.Parse(maxBMBBBH);
            List<string> sqlID = new List<string>();
            List<Dictionary<string, object>> listSPFL = new List<Dictionary<string, object>>();
            XmlNodeList list6 = document.GetElementsByTagName("BMXX");
            for (int i = 0; i < list6.Count; i++)
            {
                dictionary = new Dictionary<string, object>();
                XmlNodeList childNodes = list6[i].ChildNodes;
                if (childNodes != null)
                {
                    dictionary.Add("BMB_BBH", s);
                    foreach (XmlNode node in childNodes)
                    {
                        switch (node.Name)
                        {
                            case "SPBM":
                            {
                                dictionary.Add("HBBM", node.InnerText);
                                str = str + node.InnerText;
                                string bM = this.GetBM(node.InnerText);
                                dictionary.Add("BM", bM);
                                dictionary.Add("SJBM", this.GetSJBM(bM));
                                continue;
                            }
                            case "SPMC":
                            {
                                dictionary.Add("MC", node.InnerText);
                                continue;
                            }
                            case "SM":
                            {
                                dictionary.Add("SM", node.InnerText);
                                continue;
                            }
                            case "ZZSSL":
                            {
                                dictionary.Add("SLV", node.InnerText);
                                str = str + node.InnerText;
                                continue;
                            }
                            case "GJZ":
                            {
                                dictionary.Add("GJZ", node.InnerText);
                                continue;
                            }
                            case "HZX":
                            {
                                dictionary.Add("HZX", node.InnerText);
                                continue;
                            }
                            case "BB":
                            {
                                dictionary.Add("BBH", node.InnerText);
                                continue;
                            }
                            case "KYZT":
                            {
                                dictionary.Add("KYZT", node.InnerText);
                                continue;
                            }
                            case "ZZSTSGL":
                            {
                                dictionary.Add("ZZSTSGL", node.InnerText);
                                str = str + node.InnerText;
                                continue;
                            }
                            case "ZZSZCYJ":
                            {
                                dictionary.Add("ZZSZCYJ", node.InnerText);
                                continue;
                            }
                            case "ZZSTSNRDM":
                            {
                                dictionary.Add("ZZSTSNRDM", node.InnerText);
                                continue;
                            }
                            case "XFSGL":
                            {
                                dictionary.Add("XFSGL", node.InnerText);
                                continue;
                            }
                            case "XFSZCYJ":
                            {
                                dictionary.Add("XFSZCYJ", node.InnerText);
                                continue;
                            }
                            case "XFSTSNRDM":
                            {
                                dictionary.Add("XFSTSNRDM", node.InnerText);
                                continue;
                            }
                            case "TJJBM":
                            {
                                dictionary.Add("TJJBM", node.InnerText);
                                continue;
                            }
                            case "HGJCKSPPM":
                            {
                                dictionary.Add("HGJCKSPPM", node.InnerText);
                                continue;
                            }
                            case "QYSJ":
                            {
                                dictionary.Add("QYSJ", node.InnerText);
                                continue;
                            }
                            case "GDQJZSJ":
                            {
                                dictionary.Add("GDQJZSJ", node.InnerText);
                                continue;
                            }
                            case "GXSJ":
                            {
                                if (node.InnerText.Trim().Length <= 8)
                                {
                                    break;
                                }
                                dictionary.Add("GXSJ", DateTime.Parse(node.InnerText));
                                continue;
                            }
                            default:
                            {
                                continue;
                            }
                        }
                        DateTime time = DateTime.ParseExact(node.InnerText, "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                        dictionary.Add("GXSJ", time);
                    }
                    sqlID.Add("aisino.Fwkp.Bmgl.BMSPFL.ReplaceSPFL");
                    listSPFL.Add(dictionary);
                }
            }
            if ((listSPFL != null) && (listSPFL.Count > 1))
            {
                listSPFL = this.HandleWJJD(listSPFL);
            }
            XmlNodeList list8 = document.GetElementsByTagName("ZZSYHZC");
            if ((list8 != null) && (list8.Count > 1))
            {
                //Dictionary<string, object> dictionary2;
                //dictionary2 = new Dictionary<string, object> {
                //    "aisino.Fwkp.Bmgl.BMSPFL.DeleteYHZC",
                //    dictionary2
                //};
                foreach (XmlNode node2 in list8)
                {
                    dictionary = new Dictionary<string, object>();
                    XmlNodeList list9 = node2.ChildNodes;
                    if ((list9 != null) && (list9.Count >= 1))
                    {
                        dictionary = new Dictionary<string, object>();
                        foreach (XmlNode node3 in list9)
                        {
                            string name = node3.Name;
                            if (name != null)
                            {
                                if (!(name == "YHZCMC"))
                                {
                                    if (name == "SL")
                                    {
                                        goto Label_0634;
                                    }
                                }
                                else
                                {
                                    dictionary.Add("Yhzcmc", node3.InnerText);
                                    str = str + node3.InnerText;
                                }
                            }
                            continue;
                        Label_0634:
                            dictionary.Add("SLv", node3.InnerText);
                            str = str + node3.InnerText;
                        }
                        sqlID.Add("aisino.Fwkp.Bmgl.BMSPFL.InsertYHZC");
                        listSPFL.Add(dictionary);
                    }
                }
            }
            str = str + "0123456789";
            string str6 = this.UserMd5(str);
            DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
            if ((str6 == innerText) && manager.UpdateSPFLTable(sqlID, listSPFL, false))
            {
                new SPFLService().UpdateBMBBBH(s);
            }
        }

        public void checkclspfl()
        {
            DataTable table = new DAL.BMCLManager().QueryCLSPFLLNotEmpty();
            if (table != null)
            {
                DAL.BMSPFLManager manager2 = new DAL.BMSPFLManager();
                List<string> sqlID = new List<string>();
                List<Dictionary<string, object>> param = new List<Dictionary<string, object>>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    string str = GetSafeData.ValidateValue<string>(table.Rows[i], "BM");
                    string bm = GetSafeData.ValidateValue<string>(table.Rows[i], "SPFL");
                    string str3 = GetSafeData.ValidateValue<string>(table.Rows[i], "YHZC");
                    string str4 = GetSafeData.ValidateValue<string>(table.Rows[i], "YHZCMC");
                    item.Add("BM", str);
                    string sPFLMCBYBM = string.Empty;
                    if (manager2.CanUseThisSPFLBM(bm, false, false))
                    {
                        item.Add("SPFL", bm);
                        sPFLMCBYBM = manager2.GetSPFLMCBYBM(bm);
                        item.Add("SPFLMC", sPFLMCBYBM);
                        if (manager2.CanUseThisYHZC(bm))
                        {
                            item.Add("YHZC", str3);
                            item.Add("YHZCMC", str4);
                        }
                        else
                        {
                            item.Add("YHZC", "否");
                            item.Add("YHZCMC", "");
                        }
                    }
                    else
                    {
                        item.Add("SPFL", "");
                        item.Add("SPFLMC", "");
                        item.Add("YHZC", "");
                        item.Add("YHZCMC", "");
                    }
                    sqlID.Add("aisino.Fwkp.Bmgl.BMSPFL.UpdateCLSPFL");
                    param.Add(item);
                }
                new DAL.BMSPFLManager().UpdateSPFLTable(sqlID, param, false);
            }
        }

        public void checkfyxmspfl()
        {
            DataTable table = new DAL.BMFYXMManager().QueryFYXMSPFLLNotEmpty();
            if (table != null)
            {
                DAL.BMSPFLManager manager2 = new DAL.BMSPFLManager();
                List<string> sqlID = new List<string>();
                List<Dictionary<string, object>> param = new List<Dictionary<string, object>>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    string str = GetSafeData.ValidateValue<string>(table.Rows[i], "BM");
                    string bm = GetSafeData.ValidateValue<string>(table.Rows[i], "SPFL");
                    string str3 = GetSafeData.ValidateValue<string>(table.Rows[i], "YHZC");
                    string str4 = GetSafeData.ValidateValue<string>(table.Rows[i], "YHZCMC");
                    item.Add("BM", str);
                    string sPFLMCBYBM = string.Empty;
                    if (manager2.CanUseThisSPFLBM(bm, false, false))
                    {
                        item.Add("SPFL", bm);
                        sPFLMCBYBM = manager2.GetSPFLMCBYBM(bm);
                        item.Add("SPFLMC", sPFLMCBYBM);
                        if (manager2.CanUseThisYHZC(bm))
                        {
                            item.Add("YHZC", str3);
                            item.Add("YHZCMC", str4);
                        }
                        else
                        {
                            item.Add("YHZC", "否");
                            item.Add("YHZCMC", "");
                        }
                    }
                    else
                    {
                        item.Add("SPFL", "");
                        item.Add("SPFLMC", "");
                        item.Add("YHZC", "");
                        item.Add("YHZCMC", "");
                    }
                    sqlID.Add("aisino.Fwkp.Bmgl.BMSPFL.UpdateFYXMSPFL");
                    param.Add(item);
                }
                new DAL.BMSPFLManager().UpdateSPFLTable(sqlID, param, false);
            }
        }

        public void checkspspfl()
        {
            DataTable table = new DAL.BMSPManager().QuerySPSPFLLNotEmptyAndNotXT();
            if (table != null)
            {
                DAL.BMSPFLManager manager2 = new DAL.BMSPFLManager();
                List<string> sqlID = new List<string>();
                List<Dictionary<string, object>> param = new List<Dictionary<string, object>>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    string str = GetSafeData.ValidateValue<string>(table.Rows[i], "BM");
                    string bm = GetSafeData.ValidateValue<string>(table.Rows[i], "SPFL");
                    string str3 = GetSafeData.ValidateValue<string>(table.Rows[i], "LSLVBS");
                    string str4 = GetSafeData.ValidateValue<string>(table.Rows[i], "YHZC");
                    string str5 = GetSafeData.ValidateValue<string>(table.Rows[i], "YHZCMC");
                    string sPFLMCBYBM = string.Empty;
                    item.Add("BM", str);
                    if (manager2.CanUseThisSPFLBM(bm, true, false))
                    {
                        item.Add("SPFL", bm);
                        sPFLMCBYBM = manager2.GetSPFLMCBYBM(bm);
                        item.Add("SPFLMC", sPFLMCBYBM);
                        if (manager2.CanUseThisYHZC(bm))
                        {
                            item.Add("YHZC", str4);
                            item.Add("YHZCMC", str5);
                            item.Add("LSLVBS", str3);
                        }
                        else
                        {
                            item.Add("YHZC", "否");
                            item.Add("YHZCMC", "");
                            item.Add("LSLVBS", "");
                        }
                    }
                    else
                    {
                        item.Add("SPFL", "");
                        item.Add("SPFLMC", "");
                        item.Add("YHZC", "");
                        item.Add("YHZCMC", "");
                        item.Add("LSLVBS", "");
                    }
                    sqlID.Add("aisino.Fwkp.Bmgl.BMSPFL.UpdateSPSPFL");
                    param.Add(item);
                }
                new DAL.BMSPFLManager().UpdateSPFLTable(sqlID, param, false);
            }
        }

        public string ChildDetermine(string BM, string SJBM)
        {
            return string.Empty;
        }

        public void ChooseYHZCMCForCL()
        {
            DataTable table = new DAL.BMCLManager().QueryCLYHZCIsAndYHZCMCIsEmpty();
            if (table != null)
            {
                DAL.BMSPFLManager manager2 = new DAL.BMSPFLManager();
                List<string> sqlID = new List<string>();
                List<Dictionary<string, object>> param = new List<Dictionary<string, object>>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    string str = GetSafeData.ValidateValue<string>(table.Rows[i], "BM");
                    string bm = GetSafeData.ValidateValue<string>(table.Rows[i], "SPFL");
                    string str3 = GetSafeData.ValidateValue<string>(table.Rows[i], "YHZC");
                    string sPFLMCBYBM = manager2.GetSPFLMCBYBM(bm);
                    string str5 = "";
                    object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { bm });
                    if ((objArray != null) && (objArray.Length > 0))
                    {
                        string[] strArray = (objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                        if (strArray.Length > 0)
                        {
                            str5 = strArray[0];
                            if ((!Flbm.IsDK() && (strArray.Length == 1)) && strArray[0].Contains("1.5%"))
                            {
                                str3 = "否";
                                str5 = "";
                            }
                            if ((!Flbm.IsDK() && (strArray.Length > 1)) && strArray[0].Contains("1.5%"))
                            {
                                str5 = strArray[1];
                            }
                        }
                        else
                        {
                            str3 = "否";
                        }
                    }
                    else
                    {
                        str3 = "否";
                    }
                    item.Add("BM", str);
                    item.Add("SPFL", bm);
                    item.Add("SPFLMC", sPFLMCBYBM);
                    item.Add("YHZC", str3);
                    item.Add("YHZCMC", str5);
                    sqlID.Add("aisino.Fwkp.Bmgl.BMSPFL.UpdateCLSPFL");
                    param.Add(item);
                }
                new DAL.BMSPFLManager().UpdateSPFLTable(sqlID, param, false);
            }
        }

        public void ChooseYHZCMCForFYXM()
        {
            DataTable table = new DAL.BMFYXMManager().QueryFYXMYHZCIsAndYHZCMCIsEmpty();
            if (table != null)
            {
                DAL.BMSPFLManager manager2 = new DAL.BMSPFLManager();
                List<string> sqlID = new List<string>();
                List<Dictionary<string, object>> param = new List<Dictionary<string, object>>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    string str = GetSafeData.ValidateValue<string>(table.Rows[i], "BM");
                    string bm = GetSafeData.ValidateValue<string>(table.Rows[i], "SPFL");
                    string str3 = GetSafeData.ValidateValue<string>(table.Rows[i], "YHZC");
                    string sPFLMCBYBM = manager2.GetSPFLMCBYBM(bm);
                    string str5 = "";
                    object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { bm });
                    if ((objArray != null) && (objArray.Length > 0))
                    {
                        string[] strArray = (objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                        if (strArray.Length > 0)
                        {
                            str5 = strArray[0];
                            if ((!Flbm.IsDK() && (strArray.Length == 1)) && strArray[0].Contains("1.5%"))
                            {
                                str3 = "否";
                                str5 = "";
                            }
                            if ((!Flbm.IsDK() && (strArray.Length > 1)) && strArray[0].Contains("1.5%"))
                            {
                                str5 = strArray[1];
                            }
                        }
                        else
                        {
                            str3 = "否";
                        }
                    }
                    else
                    {
                        str3 = "否";
                    }
                    item.Add("BM", str);
                    item.Add("SPFL", bm);
                    item.Add("SPFLMC", sPFLMCBYBM);
                    item.Add("YHZC", str3);
                    item.Add("YHZCMC", str5);
                    sqlID.Add("aisino.Fwkp.Bmgl.BMSPFL.UpdateFYXMSPFL");
                    param.Add(item);
                }
                new DAL.BMSPFLManager().UpdateSPFLTable(sqlID, param, false);
            }
        }

        private string DecodeSPFLFile(string XTFilePath)
        {
            byte[] buffer = new byte[8];
            IDEAXT ideaxt = new IDEAXT();
            byte[] bytes = Encoding.Default.GetBytes("9781246350HQSTAR");
            ideaxt.encrypt_subkey(bytes);
            ideaxt.uncrypt_subkey();
            string str = "";
            try
            {
                int count = 0;
                FileStream stream = new FileStream(XTFilePath, FileMode.Open);
                count = (int) stream.Length;
                byte[] buffer3 = new byte[count];
                byte[] buffer4 = new byte[count];
                stream.Read(buffer3, 0, count);
                for (int i = 0; i < (count / 8); i++)
                {
                    byte[] destinationArray = new byte[8];
                    Array.Copy(buffer3, i * 8, destinationArray, 0, 8);
                    ideaxt.encrypt(destinationArray, buffer);
                    for (int j = 0; j < 8; j++)
                    {
                        buffer4[(8 * i) + j] = buffer[j];
                    }
                }
                stream.Close();
                str = Encoding.Default.GetString(buffer4);
                int num4 = str.LastIndexOf("</FPXT>");
                return str.Substring(0, num4 + 7);
            }
            catch
            {
                return "";
            }
        }

        public string DeleteData(string customerCode)
        {
            return string.Empty;
        }

        public string DeleteDataFenLei(string FenLeiCodeBM)
        {
            return string.Empty;
        }

        public string deleteFenLei(string flBM)
        {
            return string.Empty;
        }

        public string ExecZengJianWei(string BM, bool isZengWei)
        {
            return string.Empty;
        }

        public string ExportData(string pathFile, string separator, DataTable khTable = null)
        {
            return string.Empty;
        }

        private string GetBM(string hbbm)
        {
            while (hbbm.EndsWith("00"))
            {
                hbbm = hbbm.Substring(0, hbbm.Length - 2);
            }
            return hbbm;
        }

        public string GetMaxBMBBBH()
        {
            DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
            return manager.GetSBBPBBH();
        }

        public BMBaseModel GetModel(string BM)
        {
            DataTable sPFL = this.bmspflDAL.GetSPFL(BM);
            BMSPModel model = new BMSPModel();
            if (sPFL.Rows.Count > 0)
            {
                DataRow row = sPFL.Rows[0];
                model.BM = GetSafeData.ValidateValue<string>(row, "BM");
                model.MC = GetSafeData.ValidateValue<string>(row, "MC");
                model.JM = GetSafeData.ValidateValue<string>(row, "JM");
                model.KJM = GetSafeData.ValidateValue<string>(row, "KJM");
                model.SJBM = GetSafeData.ValidateValue<string>(row, "SJBM");
                model.SLV = GetSafeData.ValidateValue<double>(row, "SLV");
                model.SPSM = GetSafeData.ValidateValue<string>(row, "SPSM");
                model.GGXH = GetSafeData.ValidateValue<string>(row, "GGXH");
                model.JLDW = GetSafeData.ValidateValue<string>(row, "JLDW");
                model.DJ = GetSafeData.ValidateValue<double>(row, "DJ");
                model.HSJBZ = GetSafeData.ValidateValue<bool>(row, "HSJBZ");
                model.XSSRKM = GetSafeData.ValidateValue<string>(row, "XSSRKM");
                model.YJZZSKM = GetSafeData.ValidateValue<string>(row, "YJZZSKM");
                model.XSTHKM = GetSafeData.ValidateValue<string>(row, "XSTHKM");
                model.HYSY = GetSafeData.ValidateValue<bool>(row, "HYSY");
                model.XTHASH = GetSafeData.ValidateValue<string>(row, "XTHASH");
                model.XTCODE = GetSafeData.ValidateValue<string>(row, "XTCODE");
                model.ISHIDE = GetSafeData.ValidateValue<string>(row, "ISHIDE");
            }
            return model;
        }

        private string GetSJBM(string sBM)
        {
            string str = sBM;
            if ((str != null) && (str.Length <= 2))
            {
                return "";
            }
            if ((str != null) && (str.Length > 2))
            {
                str = str.Substring(0, str.Length - 2);
            }
            return str;
        }

        public DataTable GetSLV_BY_BM(string BM)
        {
            return this.bmspflDAL.GetSLV_BY_BM(BM);
        }

        public DataTable GetSP(string BM)
        {
            return this.bmspflDAL.GetSPFL(BM);
        }

        public string[] GetSPXXByName(string name)
        {
            return this.bmspflDAL.GetSPXXByName(name);
        }

        public int GetSuggestBMLen(string SJBM)
        {
            return 0;
        }

        private string GetWJ(string bm, List<Dictionary<string, object>> listSPFL, DataTable dtSJBM)
        {
            if (((bm != "") && (listSPFL != null)) && (listSPFL.Count >= 1))
            {
                try
                {
                    for (int i = 0; i < listSPFL.Count; i++)
                    {
                        if (bm == listSPFL[i]["SJBM"].ToString())
                        {
                            return "0";
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return this.GetWJFromDB(bm, dtSJBM);
        }

        private string GetWJFromDB(string bm, DataTable dt)
        {
            if (((bm != null) && (bm != "")) && ((dt != null) && (dt.Rows.Count >= 1)))
            {
                try
                {
                    DataRow[] rowArray = dt.Select("SJBM='" + bm + "'");
                    if ((rowArray != null) && (rowArray.Length > 0))
                    {
                        return "0";
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return "1";
        }

        private List<Dictionary<string, object>> HandleWJJD(List<Dictionary<string, object>> listSPFL)
        {
            List<Dictionary<string, object>> list = listSPFL;
            DataTable sJBM = new DAL.BMSPFLManager().GetSJBM();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i]["WJ"] = this.GetWJ(list[i]["BM"].ToString(), list, sJBM);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public ImportResult ImportData(string codeFile)
        {
            return null;
        }

        public ImportResult ImportDataSPFL(string codeFile)
        {
            Dictionary<string, object> dictionary;
            ImportResult result = new ImportResult();
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(codeFile);
                XmlNode documentElement = document.DocumentElement;
                if ((documentElement.Attributes["id"].Value != "spbm") && (documentElement.Attributes["id"].Value != "SPBM"))
                {
                    throw new Exception("导入税收分类编码文件格式不正确!");
                }
            }
            catch
            {
                throw new Exception("导入税收分类编码文件格式不正确!");
            }
            string innerText = document.GetElementsByTagName("MWJY")[0].InnerText;
            string text1 = document.GetElementsByTagName("COUNT")[0].InnerText;
            string str = string.Empty;
            XmlNodeList elementsByTagName = document.GetElementsByTagName("BBH");
            if ((elementsByTagName == null) || (elementsByTagName.Count < 1))
            {
                throw new Exception("导入税收分类编码版本号异常！");
            }
            string maxBMBBBH = this.GetMaxBMBBBH();
            string s = elementsByTagName[0].InnerText;
            if (float.Parse(maxBMBBBH) >= float.Parse(s))
            {
                throw new Exception("导入文件中税收分类编码版本号过低，系统不允许导入！");
            }
            List<string> sqlID = new List<string>();
            List<Dictionary<string, object>> listSPFL = new List<Dictionary<string, object>>();
            XmlNodeList list6 = document.GetElementsByTagName("BMXX");
            for (int i = 0; i < list6.Count; i++)
            {
                dictionary = new Dictionary<string, object>();
                XmlNodeList childNodes = list6[i].ChildNodes;
                if (childNodes != null)
                {
                    dictionary.Add("BMB_BBH", s);
                    foreach (XmlNode node2 in childNodes)
                    {
                        switch (node2.Name)
                        {
                            case "SPBM":
                            {
                                dictionary.Add("HBBM", node2.InnerText);
                                str = str + node2.InnerText;
                                string bM = this.GetBM(node2.InnerText);
                                dictionary.Add("BM", bM);
                                dictionary.Add("SJBM", this.GetSJBM(bM));
                                continue;
                            }
                            case "SPMC":
                            {
                                dictionary.Add("MC", node2.InnerText);
                                continue;
                            }
                            case "SM":
                            {
                                dictionary.Add("SM", node2.InnerText);
                                continue;
                            }
                            case "ZZSSL":
                            {
                                dictionary.Add("SLV", node2.InnerText);
                                str = str + node2.InnerText;
                                continue;
                            }
                            case "GJZ":
                            {
                                dictionary.Add("GJZ", node2.InnerText);
                                continue;
                            }
                            case "HZX":
                            {
                                dictionary.Add("HZX", node2.InnerText);
                                continue;
                            }
                            case "BB":
                            {
                                dictionary.Add("BBH", node2.InnerText);
                                continue;
                            }
                            case "KYZT":
                            {
                                dictionary.Add("KYZT", node2.InnerText);
                                continue;
                            }
                            case "ZZSTSGL":
                            {
                                dictionary.Add("ZZSTSGL", node2.InnerText);
                                str = str + node2.InnerText;
                                continue;
                            }
                            case "ZZSZCYJ":
                            {
                                dictionary.Add("ZZSZCYJ", node2.InnerText);
                                continue;
                            }
                            case "ZZSTSNRDM":
                            {
                                dictionary.Add("ZZSTSNRDM", node2.InnerText);
                                continue;
                            }
                            case "XFSGL":
                            {
                                dictionary.Add("XFSGL", node2.InnerText);
                                continue;
                            }
                            case "XFSZCYJ":
                            {
                                dictionary.Add("XFSZCYJ", node2.InnerText);
                                continue;
                            }
                            case "XFSTSNRDM":
                            {
                                dictionary.Add("XFSTSNRDM", node2.InnerText);
                                continue;
                            }
                            case "TJJBM":
                            {
                                dictionary.Add("TJJBM", node2.InnerText);
                                continue;
                            }
                            case "HGJCKSPPM":
                            {
                                dictionary.Add("HGJCKSPPM", node2.InnerText);
                                continue;
                            }
                            case "QYSJ":
                            {
                                dictionary.Add("QYSJ", node2.InnerText);
                                continue;
                            }
                            case "GDQJZSJ":
                            {
                                dictionary.Add("GDQJZSJ", node2.InnerText);
                                continue;
                            }
                            case "GXSJ":
                            {
                                if (node2.InnerText.Trim().Length <= 8)
                                {
                                    break;
                                }
                                dictionary.Add("GXSJ", DateTime.Parse(node2.InnerText));
                                continue;
                            }
                            default:
                            {
                                continue;
                            }
                        }
                        DateTime time = DateTime.ParseExact(node2.InnerText, "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                        dictionary.Add("GXSJ", time);
                    }
                    sqlID.Add("aisino.Fwkp.Bmgl.BMSPFL.ReplaceSPFL");
                    listSPFL.Add(dictionary);
                }
            }
            if ((listSPFL != null) && (listSPFL.Count > 1))
            {
                listSPFL = this.HandleWJJD(listSPFL);
            }
            XmlNodeList list8 = document.GetElementsByTagName("ZZSYHZC");
            if ((list8 != null) && (list8.Count > 1))
            {
                //Dictionary<string, object> dictionary2;
                //dictionary2 = new Dictionary<string, object> {
                //    "aisino.Fwkp.Bmgl.BMSPFL.DeleteYHZC",
                //    dictionary2
                //};
                foreach (XmlNode node3 in list8)
                {
                    dictionary = new Dictionary<string, object>();
                    XmlNodeList list9 = node3.ChildNodes;
                    if ((list9 != null) && (list9.Count >= 1))
                    {
                        dictionary = new Dictionary<string, object>();
                        foreach (XmlNode node4 in list9)
                        {
                            string name = node4.Name;
                            if (name != null)
                            {
                                if (!(name == "YHZCMC"))
                                {
                                    if (name == "SL")
                                    {
                                        goto Label_06B5;
                                    }
                                }
                                else
                                {
                                    dictionary.Add("Yhzcmc", node4.InnerText);
                                    str = str + node4.InnerText;
                                }
                            }
                            continue;
                        Label_06B5:
                            dictionary.Add("SLv", node4.InnerText);
                            str = str + node4.InnerText;
                        }
                        sqlID.Add("aisino.Fwkp.Bmgl.BMSPFL.InsertYHZC");
                        listSPFL.Add(dictionary);
                    }
                }
            }
            str = str + "0123456789";
            string str6 = this.UserMd5(str);
            DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
            if (!(str6 == innerText))
            {
                throw new Exception("密文校验失败！");
            }
            if (!manager.UpdateSPFLTable(sqlID, listSPFL, false))
            {
                throw new Exception("税收分类编码导入失败！");
            }
            this.checkspspfl();
            this.checkclspfl();
            this.checkfyxmspfl();
            new SPFLService().UpdateBMBBBH(s);
            MessageBoxHelper.Show("税收分类编码导入成功！", "正确", MessageBoxButtons.OK, MessageBoxIcon.None);
            result.ImportTable = "税收分类编码.DB";
            return result;
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            return this.bmspflDAL.listNodes(searchid);
        }

        public List<TreeNodeTemp> listNodesISHIDE(string searchid)
        {
            return this.bmspflDAL.listNodesISHIDE(searchid);
        }

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            return this.bmspflDAL.QueryByKey(KeyWord, pagesize, pageno);
        }

        public DataTable QueryByKeyAndSlv(string KeyWord, double Slv, int WJ, int Top)
        {
            return this.bmspflDAL.QueryByKeyAndSlv(KeyWord, Slv, WJ, Top);
        }

        public DataTable QueryByKeyAndSlvSEL(string KeyWord, double Slv, int WJ, int Top)
        {
            return this.bmspflDAL.QueryByKeyAndSlvSEL(KeyWord, Slv, WJ, Top);
        }

        public DataTable QueryByKeyAndSpecialSPSEL(string KeyWord, string specialSP, int WJ, int Top)
        {
            return this.bmspflDAL.QueryByKeyAndSpecialSPSEL(KeyWord, specialSP, WJ, Top);
        }

        public AisinoDataSet QueryByKeyDisplaySEL(string selectedBM, int Pagesize, int Pageno)
        {
            return this.bmspflDAL.QueryByKeyDisplaySEL(selectedBM, Pagesize, Pageno);
        }

        public AisinoDataSet QueryByKeyDisplaySEL(string selectedBM, double slv, int Pagesize, int Pageno)
        {
            return this.bmspflDAL.QueryByKeyDisplaySEL(selectedBM, slv, Pagesize, Pageno);
        }

        public AisinoDataSet QueryByKeyDisplaySEL(string selectedBM, string specialSP, int Pagesize, int Pageno)
        {
            return this.bmspflDAL.QueryByKeyDisplaySEL(selectedBM, specialSP, Pagesize, Pageno);
        }

        public AisinoDataSet QueryByKeySEL(string KeyWord, int pagesize, int pageno)
        {
            return this.bmspflDAL.QueryByKeySEL(KeyWord, pagesize, pageno);
        }

        public AisinoDataSet QueryData(int pagesize, int pageno)
        {
            return this.bmspflDAL.QueryMerchandise(pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectedBM, int Pagesize, int Pageno)
        {
            return this.bmspflDAL.SelectNodeDisplay(selectedBM, Pagesize, Pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectedBM, double slv, int Pagesize, int Pageno)
        {
            return this.bmspflDAL.SelectNodeDisplay(selectedBM, slv, Pagesize, Pageno);
        }

        public AisinoDataSet SelectNodeDisplaySEL(string selectedBM, int Pagesize, int Pageno)
        {
            return this.bmspflDAL.SelectNodeDisplaySEL(selectedBM, Pagesize, Pageno);
        }

        public AisinoDataSet SelectNodeDisplaySEL(string selectedBM, double slv, int Pagesize, int Pageno)
        {
            return this.bmspflDAL.SelectNodeDisplaySEL(selectedBM, slv, Pagesize, Pageno);
        }

        public AisinoDataSet SelectNodeDisplaySEL(string selectedBM, string specialSP, int Pagesize, int Pageno)
        {
            return this.bmspflDAL.SelectNodeDisplaySEL(selectedBM, specialSP, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            return string.Empty;
        }

        public void UpdateHZXIsHideDownToUp(string sjbm, bool hide)
        {
            this.bmspflDAL.UpdateHZXIsHideDownToUp(sjbm, hide);
        }

        public void UpdateHZXIsHideUpToDown(string sjbm, bool hide)
        {
            this.bmspflDAL.UpdateHZXIsHideUpToDown(sjbm, hide);
        }

        public void UpdateSPFLIsHide(string bm, bool isHide)
        {
            this.bmspflDAL.UpdateSPFLIsHide(bm, isHide);
        }

        private string UserMd5(string str)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            return BitConverter.ToString(provider.ComputeHash(Encoding.Default.GetBytes(str))).Replace("-", "");
        }

        public int CurrentPage
        {
            get
            {
                return this._currentPage;
            }
            set
            {
                this._currentPage = value;
            }
        }

        public int Pagesize
        {
            get
            {
                int num;
                string s = PropertyUtil.GetValue("pagesize");
                if (((s == null) || (s.Length == 0)) || !int.TryParse(s, out num))
                {
                    num = 30;
                    PropertyUtil.SetValue("pagesize", s);
                }
                return num;
            }
            set
            {
                PropertyUtil.SetValue("pagesize", value.ToString());
            }
        }
    }
}

