namespace Aisino.Fwkp.Fptk
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk.Form;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Framework.Plugin.Core;
    public class FPDJHelper
    {
        private int bmbbh_maxlength = 20;
        public static string djh_hx = "";
        private ILog log = LogUtil.GetLogger<FPDJHelper>();
        private int qyzbm_maxlength = 20;
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private int yhzc_maxlength = 50;

        public bool CheckErr(string a, string b, int decimals, string err)
        {
            string str = this.SetDecimals(a, decimals);
            string str2 = this.SetDecimals(b, decimals);
            return (decimal.Compare(Math.Abs(decimal.Parse(this.Subtract(str, str2))), decimal.Parse(err)) <= 0);
        }

        private string CheckValue(string djh, XmlNode node, string nodeName)
        {
            decimal num;
            if (node == null)
            {
                return string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/{1}节点！", djh, nodeName);
            }
            string s = node.InnerXml.Trim();
            if ((s != "") && !decimal.TryParse(s, out num))
            {
                return string.Format("XML文件格式错误，单据{0}中{1}值格式不正确！", djh, nodeName);
            }
            return "";
        }

        private string CheckValueDZ(string djh, XmlNode node, string nodeName)
        {
            decimal num;
            if (node == null)
            {
                return string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/{1}节点！", djh, nodeName);
            }
            string s = node.InnerXml.Trim();
            if ((s != "") && !decimal.TryParse(s, out num))
            {
                return string.Format("XML文件格式错误，单据{0}中{1}值格式不正确！", djh, nodeName);
            }
            return "";
        }

        private bool flbmCanUse(string flbm)
        {
            if (!Regex.IsMatch(flbm, @"^\d{19}$"))
            {
                return false;
            }
            if ((flbm[1] == '0') && (flbm[2] == '0'))
            {
                return false;
            }
            return true;
        }

        public static string GetErrorMessage(string code, string[] paramArray)
        {
            if (string.IsNullOrEmpty(code))
            {
                return "";
            }
            return MessageManager.GetMessageInfo(code, paramArray);
        }

        public void hx_Insert(string hxdjh, string hxerror)
        {
            XmlNode newChild = AutoImport.doc.CreateElement("RESPONSE_COMMON_FPKJ");
            AutoImport.bus.AppendChild(newChild);
            XmlElement element = AutoImport.doc.CreateElement("FPQQLSH");
            if ((hxdjh != null) && (hxdjh != ""))
            {
                element.InnerText = hxdjh;
            }
            newChild.AppendChild(element);
            XmlElement element2 = AutoImport.doc.CreateElement("JQBH");
            newChild.AppendChild(element2);
            XmlElement element3 = AutoImport.doc.CreateElement("FP_DM");
            newChild.AppendChild(element3);
            XmlElement element4 = AutoImport.doc.CreateElement("FP_HM");
            newChild.AppendChild(element4);
            XmlElement element5 = AutoImport.doc.CreateElement("KPRQ");
            newChild.AppendChild(element5);
            XmlElement element6 = AutoImport.doc.CreateElement("FP_MW");
            newChild.AppendChild(element6);
            XmlElement element7 = AutoImport.doc.CreateElement("JYM");
            newChild.AppendChild(element7);
            XmlElement element8 = AutoImport.doc.CreateElement("EWM");
            newChild.AppendChild(element8);
            XmlElement element9 = AutoImport.doc.CreateElement("BZ");
            newChild.AppendChild(element9);
            XmlElement element10 = AutoImport.doc.CreateElement("RETURNCODE");
            element10.InnerText = "0001";
            newChild.AppendChild(element10);
            XmlElement element11 = AutoImport.doc.CreateElement("RETURNMSG");
            element11.InnerText = hxerror;
            newChild.AppendChild(element11);
        }

        public bool InsertYkdj(string file, string djh)
        {
            try
            {
                string str = "aisino.fwkp.fptk.InsertYkdj";
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("FILE", file);
                dictionary.Add("DJH", djh);
                if (BaseDAOFactory.GetBaseDAOSQLite().未确认DAO方法2_疑似updateSQL(str, dictionary) > 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.Message, exception);
            }
            return false;
        }

        internal static bool IsNumber(string str)
        {
            return new Regex("^[0-9]*$").IsMatch(str);
        }

        private bool isWM()
        {
            return !FLBM_lock.isFlbm();
        }

        private void lslbz2yhzc(string slv, ref string lslvbz, ref string yhzc, ref string xsyh)
        {
            double result = 100.0;
            if (double.TryParse(slv, out result))
            {
                if (result == 0.0)
                {
                    if (lslvbz == "0")
                    {
                        yhzc = "出口零税";
                        xsyh = "1";
                    }
                    else if (lslvbz == "1")
                    {
                        yhzc = "免税";
                        xsyh = "1";
                    }
                    else if (lslvbz == "2")
                    {
                        yhzc = "不征税";
                        xsyh = "1";
                    }
                    else if (lslvbz == "3")
                    {
                        yhzc = "";
                        xsyh = "0";
                    }
                    else
                    {
                        lslvbz = "3";
                        yhzc = "";
                        xsyh = "0";
                    }
                }
                else
                {
                    lslvbz = "";
                }
            }
        }

        public List<Djfp> ParseDjFile(FPLX fplx, string djFileName, out string errorTip, string sqslv, ZYFP_LX zyfplx)
        {
            errorTip = "";
            List<Djfp> list = new List<Djfp>();
            List<double> allSlv = new FpManager().GetAllSlv(sqslv, false);
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(djFileName);
                if (!this.isWM() && (document.SelectSingleNode("Kp/Version") == null))
                {
                    errorTip = "XML文件格式错误，找不到Kp/Version节点！";
                    AutoImport.writer.WriteLine(string.Format("[{0}] 文件:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djFileName, errorTip));
                    return list;
                }
                int result = 0;
                XmlNode node = document.SelectSingleNode("/Kp/Fpxx/Zsl");
                if ((node != null) && (node.InnerXml != ""))
                {
                    int.TryParse(node.InnerXml, out result);
                }
                XmlNodeList list3 = document.SelectNodes("/Kp/Fpxx/Fpsj/Fp");
                if ((list3 == null) || (list3.Count == 0))
                {
                    errorTip = "XML文件格式错误，找不到Fpsj/Fp节点！";
                    AutoImport.writer.WriteLine(string.Format("[{0}] 文件:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djFileName, errorTip));
                    return list;
                }
                if (list3.Count != result)
                {
                    errorTip = "XML文件中单据数量不一致！";
                    AutoImport.writer.WriteLine(string.Format("[{0}] 文件:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djFileName, errorTip));
                    return list;
                }
                List<double> list4 = new List<double>();
                string str = djFileName.Substring(djFileName.LastIndexOf(@"\") + 1);
                foreach (XmlNode node2 in list3)
                {
                    errorTip = "";
                    list4.Clear();
                    djh_hx = "";
                    XmlNode node3 = node2.SelectSingleNode("Djh");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据中不存在Djh节点！", new object[0]);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:,开具结果:0,开具失败原因:{1}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), errorTip));
                        continue;
                    }
                    if (node3.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据号中Djh值不能为空!", new object[0]);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:,开具结果:0,开具失败原因:{1}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), errorTip));
                        continue;
                    }
                    string innerXml = node2.SelectSingleNode("Djh").InnerXml;
                    djh_hx = innerXml;
                    Fpxx fpxx = new Fpxx(fplx, "", "");
                    if (node2.SelectSingleNode("Gfmc") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Gfmc节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.gfmc = node2.SelectSingleNode("Gfmc").InnerXml;
                    if (node2.SelectSingleNode("Gfsh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Gfsh节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.gfsh = node2.SelectSingleNode("Gfsh").InnerXml;
                    if (node2.SelectSingleNode("Gfyhzh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Gfyhzh节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.gfyhzh = node2.SelectSingleNode("Gfyhzh").InnerXml;
                    if (node2.SelectSingleNode("Gfdzdh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Gfdzdh节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.gfdzdh = node2.SelectSingleNode("Gfdzdh").InnerXml;
                    if (node2.SelectSingleNode("Bz") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Bz节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.bz = node2.SelectSingleNode("Bz").InnerXml;
                    if (node2.SelectSingleNode("Fhr") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Fhr节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.fhr = node2.SelectSingleNode("Fhr").InnerXml;
                    if (node2.SelectSingleNode("Skr") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Skr节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.skr = node2.SelectSingleNode("Skr").InnerXml;
                    if (!this.isWM())
                    {
                        if (node2.SelectSingleNode("Spbmbbh") == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spbmbbh节点！", innerXml);
                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                            return list;
                        }
                        fpxx.bmbbbh = node2.SelectSingleNode("Spbmbbh").InnerXml;
                        if ((fpxx.bmbbbh == "") || (ToolUtil.GetByteCount(fpxx.bmbbbh) > this.bmbbh_maxlength))
                        {
                            errorTip = string.Format("Spbmbbh节点内容错误！", innerXml);
                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                            return list;
                        }
                    }
                    if (node2.SelectSingleNode("Hsbz") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Hsbz节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        return list;
                    }
                    if (((node2.SelectSingleNode("Hsbz").InnerXml == "1") && !allSlv.Contains(1.0)) && !allSlv.Contains(0.015))
                    {
                        errorTip = string.Format("税率非法！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        return list;
                    }
                    string str3 = node2.SelectSingleNode("Hsbz").InnerXml;
                    if (str3 == "2")
                    {
                        if (!FLBM_lock.isCes())
                        {
                            errorTip = string.Format("当前版本不支持差额税单据导入！", innerXml);
                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                            return list;
                        }
                        if ((zyfplx != 0) && ((int)zyfplx != 11))
                        {
                            errorTip = string.Format("当前票种不能导入差额税单据！", innerXml);
                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                            return list;
                        }
                        fpxx.Zyfplx = (ZYFP_LX)11;
                    }
                    XmlNodeList list6 = node2.SelectNodes("Spxx/Sph");
                    if (list6 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                    foreach (XmlNode node4 in list6)
                    {
                        string str4;
                        string str5;
                        string str6;
                        Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                        XmlNode node5 = node4.SelectSingleNode("Xh");
                        if (node5 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Xh节点！", innerXml);
                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        }
                        else
                        {
                            item.Add((SPXX)13, node5.InnerXml);
                            if (node4.SelectSingleNode("Spmc") == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Spmc节点！", innerXml);
                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                            }
                            else
                            {
                                item.Add((SPXX)0, node4.SelectSingleNode("Spmc").InnerXml);
                                if (node4.SelectSingleNode("Ggxh") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Ggxh节点！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                }
                                else
                                {
                                    item.Add((SPXX)3, node4.SelectSingleNode("Ggxh").InnerXml);
                                    if (node4.SelectSingleNode("Jldw") == null)
                                    {
                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Jldw节点！", innerXml);
                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    }
                                    else
                                    {
                                        item.Add((SPXX)4, node4.SelectSingleNode("Jldw").InnerXml);
                                        if (!this.isWM())
                                        {
                                            node5 = node4.SelectSingleNode("Spbm");
                                            if (node5 == null)
                                            {
                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Spbm节点！", innerXml);
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                break;
                                            }
                                            string flbm = node5.InnerXml;
                                            if (!this.flbmCanUse(flbm))
                                            {
                                                errorTip = string.Format("分类编码不可用！", innerXml);
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                break;
                                            }
                                            if (node4.SelectSingleNode("Qyspbm") == null)
                                            {
                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Qyspbm节点！", innerXml);
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                break;
                                            }
                                            string str9 = node4.SelectSingleNode("Qyspbm").InnerXml;
                                            if (ToolUtil.GetByteCount(str9) > this.qyzbm_maxlength)
                                            {
                                                errorTip = string.Format("Qyspbm节点内容错误！", innerXml);
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                break;
                                            }
                                            if (node4.SelectSingleNode("Syyhzcbz") == null)
                                            {
                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Syyhzcbz节点！", innerXml);
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                break;
                                            }
                                            string xsyh = node4.SelectSingleNode("Syyhzcbz").InnerXml.ToString();
                                            if (((xsyh != "") && (xsyh != "0")) && (xsyh != "1"))
                                            {
                                                errorTip = string.Format("Syyhzcbz节点内容不正确！", innerXml);
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                break;
                                            }
                                            if (xsyh == "")
                                            {
                                                xsyh = "0";
                                            }
                                            if (node4.SelectSingleNode("Yhzcsm") == null)
                                            {
                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Yhzcsm节点！", innerXml);
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                break;
                                            }
                                            string yhzc = node4.SelectSingleNode("Yhzcsm").InnerXml;
                                            if (ToolUtil.GetByteCount(yhzc) > this.yhzc_maxlength)
                                            {
                                                errorTip = string.Format("Yhzcsm节点内容错误！", innerXml);
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                break;
                                            }
                                            if (node4.SelectSingleNode("Lslbz") == null)
                                            {
                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Lslbz节点！", innerXml);
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                break;
                                            }
                                            string lslvbz = node4.SelectSingleNode("Lslbz").InnerXml;
                                            if ((((lslvbz != "0") && (lslvbz != "1")) && ((lslvbz != "2") && (lslvbz != "3"))) && (lslvbz != ""))
                                            {
                                                errorTip = string.Format("Lslbz节点内容不正确！", innerXml);
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                break;
                                            }
                                            node5 = node4.SelectSingleNode("Slv");
                                            errorTip = this.CheckValue(innerXml, node5, "Slv");
                                            if (errorTip != "")
                                            {
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                break;
                                            }
                                            this.lslbz2yhzc(node4.SelectSingleNode("Slv").InnerXml, ref lslvbz, ref yhzc, ref xsyh);
                                            item.Add((SPXX)20, flbm);
                                            item.Add((SPXX)1, str9);
                                            item.Add((SPXX)0x15, xsyh);
                                            item.Add((SPXX)0x16, yhzc);
                                            item.Add((SPXX)0x17, lslvbz);
                                        }
                                        else
                                        {
                                            item.Add((SPXX)1, "");
                                            item.Add((SPXX)20, "");
                                            item.Add((SPXX)0x15, "0");
                                            item.Add((SPXX)0x16, "");
                                            item.Add((SPXX)0x17, "");
                                        }
                                        node5 = node4.SelectSingleNode("Kce");
                                        errorTip = this.CheckValue(innerXml, node5, "Kce");
                                        if (errorTip != "")
                                        {
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                        }
                                        else
                                        {
                                            str4 = node5.InnerXml.Trim();
                                            if (str4 == "")
                                            {
                                                str4 = "0";
                                            }
                                            if (str4.Contains("-"))
                                            {
                                                errorTip = "扣除额格式不正确！";
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            }
                                            else
                                            {
                                                node5 = node4.SelectSingleNode("Dj");
                                                errorTip = this.CheckValue(innerXml, node5, "Dj");
                                                if (errorTip != "")
                                                {
                                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                }
                                                else
                                                {
                                                    item.Add((SPXX)5, (node4.SelectSingleNode("Dj").InnerXml.Trim() == "0") ? "" : node4.SelectSingleNode("Dj").InnerXml.Trim());
                                                    node5 = node4.SelectSingleNode("Sl");
                                                    errorTip = this.CheckValue(innerXml, node5, "Sl");
                                                    if (errorTip != "")
                                                    {
                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                    }
                                                    else
                                                    {
                                                        item.Add((SPXX)6, (node4.SelectSingleNode("Sl").InnerXml.Trim() == "0") ? "" : node4.SelectSingleNode("Sl").InnerXml.Trim());
                                                        node5 = node4.SelectSingleNode("Je");
                                                        errorTip = this.CheckValue(innerXml, node5, "Je");
                                                        if (errorTip != "")
                                                        {
                                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                        }
                                                        else if (node5.InnerXml.Trim() == "")
                                                        {
                                                            errorTip = string.Format("XML文件格式错误，单据{0}中Spxx/Sph/Je值不能为空!", innerXml);
                                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                        }
                                                        else
                                                        {
                                                            str5 = node4.SelectSingleNode("Je").InnerXml.Trim();
                                                            node5 = node4.SelectSingleNode("Slv");
                                                            errorTip = this.CheckValue(innerXml, node5, "Slv");
                                                            if (errorTip != "")
                                                            {
                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                            }
                                                            else
                                                            {
                                                                str6 = node4.SelectSingleNode("Slv").InnerXml.Trim();
                                                                double num2 = -1.0;
                                                                if (double.TryParse(str6, out num2))
                                                                {
                                                                    if ((num2 != 0.0) && ((int)zyfplx == 9))
                                                                    {
                                                                        errorTip = string.Format("收购发票不能导入非零税明细！", innerXml);
                                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                    }
                                                                    else if ((num2 == 0.015) && (str3 != "1"))
                                                                    {
                                                                        errorTip = string.Format("单据中含税标志信息与明细税率信息不一致！", innerXml);
                                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                    }
                                                                    else if (((num2 == 0.05) && (str3 == "1")) && ((int)fplx == 2))
                                                                    {
                                                                        errorTip = string.Format("普通发票不允许导入中外合作油气田税率商品！", innerXml);
                                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                    }
                                                                    else
                                                                    {
                                                                        if ((num2 == 0.015) && (str3 == "1"))
                                                                        {
                                                                            if ((fpxx.Zyfplx == (ZYFP_LX)10) || (fpxx.Zyfplx == 0))
                                                                            {
                                                                                fpxx.Zyfplx = (ZYFP_LX)10;
                                                                            }
                                                                            else
                                                                            {
                                                                                errorTip = string.Format("单据中存在税率不能混开的商品明细！", innerXml);
                                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                break;
                                                                            }
                                                                        }
                                                                        if ((num2 == 0.05) && (str3 == "1"))
                                                                        {
                                                                            if ((fpxx.Zyfplx == (ZYFP_LX)1) || (fpxx.Zyfplx == 0))
                                                                            {
                                                                                fpxx.Zyfplx = (ZYFP_LX)1;
                                                                            }
                                                                            else
                                                                            {
                                                                                errorTip = string.Format("单据中存在税率不能混开的商品明细！", innerXml);
                                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                break;
                                                                            }
                                                                        }
                                                                        if (!list4.Contains(num2))
                                                                        {
                                                                            list4.Add(num2);
                                                                        }
                                                                        if ((allSlv.Contains(num2) && ((num2 != 0.0) || ((int)fplx != 0))) && (((fpxx.Zyfplx != (ZYFP_LX)1) || (num2 == 0.05)) && ((fpxx.Zyfplx != (ZYFP_LX)10) || (num2 == 0.015))))
                                                                        {
                                                                            goto Label_1662;
                                                                        }
                                                                        errorTip = string.Format("税率非法！", innerXml);
                                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    errorTip = string.Format("税率非法！", innerXml);
                                                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    Label_1662:
                        item.Add((SPXX)7, str5);
                        item.Add((SPXX)10, str5.Contains("-") ? "4" : "0");
                        item.Add((SPXX)8, str6);
                        double num3 = double.Parse(str5) * double.Parse(str6);
                        string str7 = num3.ToString("F2");
                        item.Add((SPXX)9, str7);
                        item.Add((SPXX)2, "");
                        item.Add((SPXX)11, "N");
                        item.Add((SPXX)0x18, str4);
                        fpxx.Mxxx.Add(item);
                    }
                    if (errorTip == "")
                    {
                        if (list4.Count == 1)
                        {
                            fpxx.sLv = list4[0].ToString("F3");
                        }
                        else
                        {
                            fpxx.sLv = "";
                        }
                        Djfp djfp = new Djfp(innerXml) {
                            File = str
                        };
                        fpxx.xsdjbh = innerXml;
                        djfp.Fpxx = fpxx;
                        list.Add(djfp);
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                errorTip = "单据文件格式错误：" + exception.Message;
                this.log.Error(exception.Message);
                string[] textArray1 = new string[] { errorTip };
                MessageManager.ShowMsgBox("INP-242202", textArray1);
            }
            return list;
        }

        public List<Djfp> ParseDjFileManual(FPLX fplx, string djFileName, out string errorTip, string sqslv, ZYFP_LX zyfplx)
        {
            errorTip = "";
            List<Djfp> list = new List<Djfp>();
            List<double> allSlv = new FpManager().GetAllSlv(sqslv, false);
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(djFileName);
                int result = 0;
                if (!this.isWM() && (document.SelectSingleNode("Kp/Version") == null))
                {
                    errorTip = "XML文件格式错误，找不到Kp/Version节点！";
                    return list;
                }
                XmlNode node = document.SelectSingleNode("/Kp/Fpxx/Zsl");
                if ((node != null) && (node.InnerXml != ""))
                {
                    int.TryParse(node.InnerXml, out result);
                }
                XmlNodeList list3 = document.SelectNodes("/Kp/Fpxx/Fpsj/Fp");
                if ((list3 == null) || (list3.Count == 0))
                {
                    errorTip = "XML文件格式错误，找不到Fpsj/Fp节点！";
                    return list;
                }
                if (list3.Count != result)
                {
                    errorTip = "XML文件中单据数量不一致！";
                    return list;
                }
                List<double> list4 = new List<double>();
                string str = djFileName.Substring(djFileName.LastIndexOf(@"\") + 1);
                foreach (XmlNode node2 in list3)
                {
                    list4.Clear();
                    XmlNode node3 = node2.SelectSingleNode("Djh");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据中不存在Djh节点！", new object[0]);
                        return list;
                    }
                    if (node3.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据号中Djh值不能为空!", new object[0]);
                        return list;
                    }
                    string innerXml = node2.SelectSingleNode("Djh").InnerXml;
                    Fpxx fpxx = new Fpxx(fplx, "", "");
                    if (node2.SelectSingleNode("Gfmc") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Gfmc节点！", innerXml);
                        return list;
                    }
                    fpxx.gfmc = node2.SelectSingleNode("Gfmc").InnerXml;
                    if (node2.SelectSingleNode("Gfsh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Gfsh节点！", innerXml);
                        return list;
                    }
                    fpxx.gfsh = node2.SelectSingleNode("Gfsh").InnerXml;
                    if (node2.SelectSingleNode("Gfyhzh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Gfyhzh节点！", innerXml);
                        return list;
                    }
                    fpxx.gfyhzh = node2.SelectSingleNode("Gfyhzh").InnerXml;
                    if (node2.SelectSingleNode("Gfdzdh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Gfdzdh节点！", innerXml);
                        return list;
                    }
                    fpxx.gfdzdh = node2.SelectSingleNode("Gfdzdh").InnerXml;
                    if (node2.SelectSingleNode("Bz") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Bz节点！", innerXml);
                        return list;
                    }
                    fpxx.bz = node2.SelectSingleNode("Bz").InnerXml;
                    if (node2.SelectSingleNode("Fhr") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Fhr节点！", innerXml);
                        return list;
                    }
                    fpxx.fhr = node2.SelectSingleNode("Fhr").InnerXml;
                    if (node2.SelectSingleNode("Skr") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Skr节点！", innerXml);
                        return list;
                    }
                    fpxx.skr = node2.SelectSingleNode("Skr").InnerXml;
                    if (!this.isWM())
                    {
                        if (node2.SelectSingleNode("Spbmbbh") == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spbmbbh节点！", innerXml);
                            return list;
                        }
                        fpxx.bmbbbh = node2.SelectSingleNode("Spbmbbh").InnerXml;
                        if ((fpxx.bmbbbh == "") || (ToolUtil.GetByteCount(fpxx.bmbbbh) > this.bmbbh_maxlength))
                        {
                            errorTip = string.Format("Spbmbbh节点内容错误！", innerXml);
                            return list;
                        }
                    }
                    if (node2.SelectSingleNode("Hsbz") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Hsbz节点！", innerXml);
                        return list;
                    }
                    if (((node2.SelectSingleNode("Hsbz").InnerXml == "1") && !allSlv.Contains(1.0)) && !allSlv.Contains(0.015))
                    {
                        errorTip = string.Format("税率非法！", innerXml);
                        return list;
                    }
                    string str3 = node2.SelectSingleNode("Hsbz").InnerXml;
                    if (str3 == "2")
                    {
                        if (!FLBM_lock.isCes())
                        {
                            errorTip = string.Format("当前版本不支持差额税单据导入！", innerXml);
                            return list;
                        }
                        if ((zyfplx != 0) && ((int)zyfplx != 11))
                        {
                            errorTip = string.Format("当前票种不能导入差额税单据！", innerXml);
                            return list;
                        }
                        fpxx.Zyfplx = (ZYFP_LX)11;
                    }
                    XmlNodeList list6 = node2.SelectNodes("Spxx/Sph");
                    if (list6 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph节点！", innerXml);
                        return list;
                    }
                    fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                    foreach (XmlNode node4 in list6)
                    {
                        string str4;
                        string str5;
                        string str6;
                        Dictionary<SPXX, string> dictionary = new Dictionary<SPXX, string>();
                        XmlNode node5 = node4.SelectSingleNode("Xh");
                        if (node5 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Xh节点！", innerXml);
                        }
                        else
                        {
                            dictionary.Add((SPXX)13, node5.InnerXml);
                            if (node4.SelectSingleNode("Spmc") == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Spmc节点！", innerXml);
                            }
                            else
                            {
                                dictionary.Add(0, node4.SelectSingleNode("Spmc").InnerXml);
                                if (node4.SelectSingleNode("Ggxh") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Ggxh节点！", innerXml);
                                }
                                else
                                {
                                    dictionary.Add((SPXX)3, node4.SelectSingleNode("Ggxh").InnerXml);
                                    if (node4.SelectSingleNode("Jldw") == null)
                                    {
                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Jldw节点！", innerXml);
                                    }
                                    else
                                    {
                                        dictionary.Add((SPXX)4, node4.SelectSingleNode("Jldw").InnerXml);
                                        if (!this.isWM())
                                        {
                                            node5 = node4.SelectSingleNode("Spbm");
                                            if (node5 == null)
                                            {
                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Spbm节点！", innerXml);
                                                break;
                                            }
                                            string flbm = node5.InnerXml;
                                            if (!this.flbmCanUse(flbm))
                                            {
                                                errorTip = string.Format("分类编码不可用！", innerXml);
                                                break;
                                            }
                                            if (node4.SelectSingleNode("Qyspbm") == null)
                                            {
                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Qyspbm节点！", innerXml);
                                                break;
                                            }
                                            string str8 = node4.SelectSingleNode("Qyspbm").InnerXml;
                                            if (ToolUtil.GetByteCount(str8) > this.qyzbm_maxlength)
                                            {
                                                errorTip = string.Format("Qyspbm节点内容错误！", innerXml);
                                                break;
                                            }
                                            if (node4.SelectSingleNode("Syyhzcbz") == null)
                                            {
                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Syyhzcbz节点！", innerXml);
                                                break;
                                            }
                                            string xsyh = node4.SelectSingleNode("Syyhzcbz").InnerXml.ToString();
                                            if (((xsyh != "") && (xsyh != "0")) && (xsyh != "1"))
                                            {
                                                errorTip = string.Format("Syyhzcbz节点内容不正确！", innerXml);
                                                break;
                                            }
                                            if (xsyh == "")
                                            {
                                                xsyh = "0";
                                            }
                                            if (node4.SelectSingleNode("Yhzcsm") == null)
                                            {
                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Yhzcsm节点！", innerXml);
                                                break;
                                            }
                                            string yhzc = node4.SelectSingleNode("Yhzcsm").InnerXml;
                                            if (ToolUtil.GetByteCount(yhzc) > this.yhzc_maxlength)
                                            {
                                                errorTip = string.Format("Yhzcsm节点内容错误！", innerXml);
                                                break;
                                            }
                                            if (node4.SelectSingleNode("Lslbz") == null)
                                            {
                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在Spxx/Sph/Lslbz节点！", innerXml);
                                                break;
                                            }
                                            string lslvbz = node4.SelectSingleNode("Lslbz").InnerXml;
                                            if ((((lslvbz != "0") && (lslvbz != "1")) && ((lslvbz != "2") && (lslvbz != "3"))) && (lslvbz != ""))
                                            {
                                                errorTip = string.Format("Lslbz节点内容不正确！", innerXml);
                                                break;
                                            }
                                            node5 = node4.SelectSingleNode("Slv");
                                            errorTip = this.CheckValue(innerXml, node5, "Slv");
                                            if (errorTip != "")
                                            {
                                                break;
                                            }
                                            this.lslbz2yhzc(node4.SelectSingleNode("Slv").InnerXml, ref lslvbz, ref yhzc, ref xsyh);
                                            dictionary.Add((SPXX)20, flbm);
                                            dictionary.Add((SPXX)1, str8);
                                            dictionary.Add((SPXX)0x15, xsyh);
                                            dictionary.Add((SPXX)0x16, yhzc);
                                            dictionary.Add((SPXX)0x17, lslvbz);
                                        }
                                        else
                                        {
                                            dictionary.Add((SPXX)1, "");
                                            dictionary.Add((SPXX)20, "");
                                            dictionary.Add((SPXX)0x15, "0");
                                            dictionary.Add((SPXX)0x16, "");
                                            dictionary.Add((SPXX)0x17, "");
                                        }
                                        node5 = node4.SelectSingleNode("Kce");
                                        errorTip = this.CheckValue(innerXml, node5, "Kce");
                                        if (errorTip == "")
                                        {
                                            str4 = node5.InnerXml.Trim();
                                            if (str4 == "")
                                            {
                                                str4 = "0";
                                            }
                                            if (str4.Contains("-"))
                                            {
                                                errorTip = "扣除额格式不正确！";
                                            }
                                            else
                                            {
                                                node5 = node4.SelectSingleNode("Dj");
                                                errorTip = this.CheckValue(innerXml, node5, "Dj");
                                                if (errorTip == "")
                                                {
                                                    dictionary.Add((SPXX)5, (node4.SelectSingleNode("Dj").InnerXml.Trim() == "0") ? "" : node4.SelectSingleNode("Dj").InnerXml.Trim());
                                                    node5 = node4.SelectSingleNode("Sl");
                                                    errorTip = this.CheckValue(innerXml, node5, "Sl");
                                                    if (errorTip == "")
                                                    {
                                                        dictionary.Add((SPXX)6, (node4.SelectSingleNode("Sl").InnerXml.Trim() == "0") ? "" : node4.SelectSingleNode("Sl").InnerXml.Trim());
                                                        node5 = node4.SelectSingleNode("Je");
                                                        errorTip = this.CheckValue(innerXml, node5, "Je");
                                                        if (errorTip == "")
                                                        {
                                                            if (node5.InnerXml.Trim() == "")
                                                            {
                                                                errorTip = string.Format("XML文件格式错误，单据{0}中Spxx/Sph/Je值不能为空!", innerXml);
                                                            }
                                                            else
                                                            {
                                                                str5 = node4.SelectSingleNode("Je").InnerXml.Trim();
                                                                node5 = node4.SelectSingleNode("Slv");
                                                                errorTip = this.CheckValue(innerXml, node5, "Slv");
                                                                if (errorTip == "")
                                                                {
                                                                    str6 = node4.SelectSingleNode("Slv").InnerXml.Trim();
                                                                    double num2 = -1.0;
                                                                    if (double.TryParse(str6, out num2))
                                                                    {
                                                                        if ((num2 != 0.0) && ((int)zyfplx == 9))
                                                                        {
                                                                            errorTip = string.Format("收购发票不能导入非零税明细！", innerXml);
                                                                        }
                                                                        else if ((num2 == 0.015) && (str3 != "1"))
                                                                        {
                                                                            errorTip = string.Format("单据中含税标志信息与明细税率信息不一致！", innerXml);
                                                                        }
                                                                        else if (((num2 == 0.05) && (str3 == "1")) && ((int)fplx == 2))
                                                                        {
                                                                            errorTip = string.Format("普通发票不允许导入中外合作油气田税率商品！", innerXml);
                                                                        }
                                                                        else
                                                                        {
                                                                            if ((num2 == 0.015) && (str3 == "1"))
                                                                            {
                                                                                if ((fpxx.Zyfplx == (ZYFP_LX)10) || (fpxx.Zyfplx == 0))
                                                                                {
                                                                                    fpxx.Zyfplx = (ZYFP_LX)10;
                                                                                }
                                                                                else
                                                                                {
                                                                                    errorTip = string.Format("单据中存在税率不能混开的商品明细！", innerXml);
                                                                                    break;
                                                                                }
                                                                            }
                                                                            if ((num2 == 0.05) && (str3 == "1"))
                                                                            {
                                                                                if ((fpxx.Zyfplx == (ZYFP_LX)1) || (fpxx.Zyfplx == 0))
                                                                                {
                                                                                    fpxx.Zyfplx = (ZYFP_LX)1;
                                                                                }
                                                                                else
                                                                                {
                                                                                    errorTip = string.Format("单据中存在税率不能混开的商品明细！", innerXml);
                                                                                    break;
                                                                                }
                                                                            }
                                                                            if (!list4.Contains(num2))
                                                                            {
                                                                                list4.Add(num2);
                                                                            }
                                                                            if ((allSlv.Contains(num2) && ((num2 != 0.0) || ((int)fplx != 0))) && (((fpxx.Zyfplx != (ZYFP_LX)1) || (num2 == 0.05)) && ((fpxx.Zyfplx != (ZYFP_LX)10) || (num2 == 0.015))))
                                                                            {
                                                                                goto Label_0C88;
                                                                            }
                                                                            errorTip = string.Format("税率非法！", innerXml);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        errorTip = string.Format("税率非法！", innerXml);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    Label_0C88:
                        dictionary.Add((SPXX)7, str5);
                        dictionary.Add((SPXX)10, str5.Contains("-") ? "4" : "0");
                        dictionary.Add((SPXX)8, str6);
                        dictionary.Add((SPXX)9, "");
                        dictionary.Add((SPXX)2, "");
                        dictionary.Add((SPXX)11, "N");
                        dictionary.Add((SPXX)0x18, str4);
                        fpxx.Mxxx.Add(dictionary);
                    }
                    if (errorTip != "")
                    {
                        return list;
                    }
                    if (list4.Count == 1)
                    {
                        fpxx.sLv = list4[0].ToString("F3");
                    }
                    else
                    {
                        fpxx.sLv = "";
                    }
                    Djfp item = new Djfp(innerXml) {
                        File = str
                    };
                    fpxx.xsdjbh = innerXml;
                    item.Fpxx = fpxx;
                    list.Add(item);
                }
                return list;
            }
            catch (Exception exception)
            {
                errorTip = "单据文件格式错误。" + exception.Message;
                this.log.Error(exception.Message);
            }
            return list;
        }

        public List<Djfp> ParseDZDjFile(FPLX fplx, string djFileName, out string errorTip, string sqslv)
        {
            errorTip = "";
            List<Djfp> list = new List<Djfp>();
            List<double> allSlv = new FpManager().GetAllSlv(sqslv, false);
            try
            {
                XmlDocument document1 = new XmlDocument();
                document1.Load(djFileName);
                XmlNodeList list3 = document1.SelectNodes("/business/REQUEST_COMMON_FPKJ");
                if ((list3 == null) || (list3.Count == 0))
                {
                    errorTip = "XML文件格式错误，找不到 business/REQUEST_COMMON_FPKJ 节点!";
                    this.hx_Insert("", errorTip);
                    return list;
                }
                List<double> list4 = new List<double>();
                string str = djFileName.Substring(djFileName.LastIndexOf(@"\") + 1);
                foreach (XmlNode node in list3)
                {
                    decimal num;
                    errorTip = "";
                    djh_hx = "";
                    XmlNode node2 = node.SelectSingleNode("COMMON_FPKJ_FPT");
                    XmlNode node3 = node2.SelectSingleNode("FPQQLSH");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据中不存在FPQQLSH节点！", new object[0]);
                        this.hx_Insert("", errorTip);
                        continue;
                    }
                    if (node3.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据号中FPQQLSH值不能为空!", new object[0]);
                        this.hx_Insert("", errorTip);
                        continue;
                    }
                    string innerXml = node2.SelectSingleNode("FPQQLSH").InnerXml;
                    djh_hx = innerXml;
                    node3 = node2.SelectSingleNode("KPLX");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在KPLX节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if (node3.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中KPLX值不能为空!", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if ((node3.InnerXml.Trim() != "0") && (node3.InnerXml.Trim() != "1"))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中KPLX值只能为0或1!", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    string str3 = node2.SelectSingleNode("KPLX").InnerXml;
                    node3 = node2.SelectSingleNode("YFP_DM");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在YFP_DM节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if ((str3 == "1") && ((node3.InnerText.Trim() == "") || ((node3.InnerText.Trim() != "") && !IsNumber(node3.InnerText.Trim()))))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中YFP_DM值格式不正确！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if ((str3 == "1") && (node3.InnerText.Trim().Length != 12))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中YFP_DM应为12位电子发票代码！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    string str4 = node2.SelectSingleNode("YFP_DM").InnerXml;
                    node3 = node2.SelectSingleNode("YFP_HM");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在YFP_HM节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if ((str3 == "1") && ((node3.InnerText.Trim() == "") || ((node3.InnerText.Trim() != "") && !IsNumber(node3.InnerText.Trim()))))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中YFP_HM值格式不正确！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if ((str3 == "1") && (node3.InnerText.Trim().Length > 8))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中YFP_HM节点值最多为8位！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    string str5 = node2.SelectSingleNode("YFP_HM").InnerXml;
                    Fpxx fpxx = new Fpxx(fplx, "", "") {
                        blueFpdm = str4,
                        blueFphm = str5
                    };
                    if (!this.isWM())
                    {
                        node3 = node2.SelectSingleNode("BMB_BBH");
                        if (node3 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据中不存在BMB_BBH节点！", new object[0]);
                            this.hx_Insert(innerXml, errorTip);
                            return list;
                        }
                        fpxx.bmbbbh = node3.InnerXml;
                        if ((fpxx.bmbbbh == "") || (ToolUtil.GetByteCount(fpxx.bmbbbh) > this.bmbbh_maxlength))
                        {
                            errorTip = string.Format("BMB_BBH节点内容错误！", innerXml);
                            this.hx_Insert(innerXml, errorTip);
                            return list;
                        }
                    }
                    if (node2.SelectSingleNode("XSF_NSRSBH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在XSF_NSRSBH节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.xfsh = node2.SelectSingleNode("XSF_NSRSBH").InnerXml;
                    if (node2.SelectSingleNode("XSF_MC") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在XSF_MC节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.xfmc = node2.SelectSingleNode("XSF_MC").InnerXml;
                    if (node2.SelectSingleNode("XSF_DZDH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在XSF_DZDH节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.xfdzdh = node2.SelectSingleNode("XSF_DZDH").InnerXml;
                    if (node2.SelectSingleNode("XSF_YHZH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在XSF_YHZH节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.xfyhzh = node2.SelectSingleNode("XSF_YHZH").InnerXml;
                    if (node2.SelectSingleNode("GMF_NSRSBH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在GMF_NSRSBH节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.gfsh = node2.SelectSingleNode("GMF_NSRSBH").InnerXml;
                    if (node2.SelectSingleNode("GMF_MC") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在GMF_MC节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.gfmc = node2.SelectSingleNode("GMF_MC").InnerXml;
                    if (node2.SelectSingleNode("GMF_DZDH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在GMF_DZDH节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.gfdzdh = node2.SelectSingleNode("GMF_DZDH").InnerXml;
                    if (node2.SelectSingleNode("GMF_YHZH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在GMF_YHZH节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.gfyhzh = node2.SelectSingleNode("GMF_YHZH").InnerXml;
                    if (node2.SelectSingleNode("KPR") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在KPR节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.kpr = node2.SelectSingleNode("KPR").InnerXml;
                    if (node2.SelectSingleNode("SKR") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在SKR节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.skr = node2.SelectSingleNode("SKR").InnerXml;
                    if (node2.SelectSingleNode("FHR") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在FHR节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.fhr = node2.SelectSingleNode("FHR").InnerXml;
                    node3 = node2.SelectSingleNode("BZ");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在BZ节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if (ToolUtil.GetByteCount(node3.InnerText.Trim()) > 130)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中BZ长度限制为130！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.bz = node2.SelectSingleNode("BZ").InnerXml;
                    node3 = node2.SelectSingleNode("HJJE");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在HJJE节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if ((node3.InnerText.Trim() == "") || !decimal.TryParse(node3.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中HJJE值格式不正确！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.je = node2.SelectSingleNode("HJJE").InnerXml.Trim();
                    if (fpxx.je.Contains("-") && (str3 == "1"))
                    {
                        fpxx.isRed = true;
                    }
                    else if (!fpxx.je.Contains("-") && (str3 == "0"))
                    {
                        fpxx.isRed = false;
                    }
                    else
                    {
                        errorTip = string.Format("KPLX与金额的格式不统一！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        return list;
                    }
                    node3 = node2.SelectSingleNode("HJSE");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在HJSE节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if ((node3.InnerText.Trim() == "") || !decimal.TryParse(node3.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中HJSE值格式不正确！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    fpxx.se = node2.SelectSingleNode("HJSE").InnerXml.Trim();
                    node3 = node2.SelectSingleNode("JSHJ");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在JSHJ节点！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if ((node3.InnerText.Trim() == "") || !decimal.TryParse(node3.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中JSHJ值格式不正确！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if (decimal.Compare(decimal.Add(decimal.Parse(fpxx.je), decimal.Parse(fpxx.se)), num) != 0)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中HJJE与HJSE之和，不等于JSHJ值！", innerXml);
                        this.hx_Insert(innerXml, errorTip);
                        continue;
                    }
                    if (string.IsNullOrEmpty(errorTip))
                    {
                        XmlNodeList list6 = node.SelectNodes("COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX");
                        if (list6 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX节点！", innerXml);
                            this.hx_Insert(innerXml, errorTip);
                            continue;
                        }
                        decimal num2 = new decimal();
                        int num3 = 0;
                        fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                        foreach (XmlNode node4 in list6)
                        {
                            XmlNode node5;
                            string str6;
                            string str7;
                            Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                            if (node4.SelectSingleNode("FPHXZ") == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/FPHXZ节点！", innerXml);
                                this.hx_Insert(innerXml, errorTip);
                            }
                            else
                            {
                                if (!this.isWM())
                                {
                                    node5 = node4.SelectSingleNode("SPBM");
                                    if (node5 == null)
                                    {
                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/SPBM节点！", innerXml);
                                        this.hx_Insert(innerXml, errorTip);
                                        break;
                                    }
                                    string flbm = node5.InnerXml;
                                    if (!this.flbmCanUse(flbm))
                                    {
                                        errorTip = string.Format("分类编码不可用！", innerXml);
                                        this.hx_Insert(innerXml, errorTip);
                                        break;
                                    }
                                    if (node4.SelectSingleNode("ZXBM") == null)
                                    {
                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/ZXBM节点！", innerXml);
                                        this.hx_Insert(innerXml, errorTip);
                                        break;
                                    }
                                    string str10 = node4.SelectSingleNode("ZXBM").InnerXml;
                                    if (ToolUtil.GetByteCount(str10) > this.qyzbm_maxlength)
                                    {
                                        errorTip = string.Format("ZXBM节点内容错误！", innerXml);
                                        this.hx_Insert(innerXml, errorTip);
                                        break;
                                    }
                                    if (node4.SelectSingleNode("YHZCBS") == null)
                                    {
                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/YHZCBS节点！", innerXml);
                                        this.hx_Insert(innerXml, errorTip);
                                        break;
                                    }
                                    string xsyh = node4.SelectSingleNode("YHZCBS").InnerXml.ToString();
                                    if (((xsyh != "") && (xsyh != "0")) && (xsyh != "1"))
                                    {
                                        errorTip = string.Format("YHZCBS节点内容不正确！", innerXml);
                                        this.hx_Insert(innerXml, errorTip);
                                        break;
                                    }
                                    if (xsyh == "")
                                    {
                                        xsyh = "0";
                                    }
                                    if (node4.SelectSingleNode("ZZSTSGL") == null)
                                    {
                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/ZZSTSGL节点！", innerXml);
                                        this.hx_Insert(innerXml, errorTip);
                                        break;
                                    }
                                    string yhzc = node4.SelectSingleNode("ZZSTSGL").InnerXml;
                                    if (ToolUtil.GetByteCount(yhzc) > this.yhzc_maxlength)
                                    {
                                        errorTip = string.Format("ZZSTSGL节点内容错误！", innerXml);
                                        this.hx_Insert(innerXml, errorTip);
                                        break;
                                    }
                                    if (node4.SelectSingleNode("LSLBS") == null)
                                    {
                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/LSLBS节点！", innerXml);
                                        this.hx_Insert(innerXml, errorTip);
                                        break;
                                    }
                                    string lslvbz = node4.SelectSingleNode("LSLBS").InnerXml;
                                    if ((((lslvbz != "0") && (lslvbz != "1")) && ((lslvbz != "2") && (lslvbz != "3"))) && (lslvbz != ""))
                                    {
                                        errorTip = string.Format("LSLBS节点内容不正确！", innerXml);
                                        this.hx_Insert(innerXml, errorTip);
                                        break;
                                    }
                                    node5 = node4.SelectSingleNode("SL");
                                    errorTip = this.CheckValueDZ(innerXml, node5, "SL");
                                    if (errorTip != "")
                                    {
                                        this.hx_Insert(innerXml, errorTip);
                                        break;
                                    }
                                    this.lslbz2yhzc(node4.SelectSingleNode("SL").InnerXml, ref lslvbz, ref yhzc, ref xsyh);
                                    item.Add((SPXX)20, flbm);
                                    item.Add((SPXX)1, str10);
                                    item.Add((SPXX)0x15, xsyh);
                                    item.Add((SPXX)0x16, yhzc);
                                    item.Add((SPXX)0x17, lslvbz);
                                }
                                else
                                {
                                    item.Add((SPXX)1, "");
                                    item.Add((SPXX)20, "");
                                    item.Add((SPXX)0x15, "0");
                                    item.Add((SPXX)0x16, "");
                                    item.Add((SPXX)0x17, "");
                                }
                                if (node4.SelectSingleNode("XMMC") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/XMMC节点！", innerXml);
                                    this.hx_Insert(innerXml, errorTip);
                                }
                                else
                                {
                                    item.Add(0, node4.SelectSingleNode("XMMC").InnerXml);
                                    if (node4.SelectSingleNode("GGXH") == null)
                                    {
                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/GGXH节点！", innerXml);
                                        this.hx_Insert(innerXml, errorTip);
                                    }
                                    else
                                    {
                                        item.Add((SPXX)3, node4.SelectSingleNode("GGXH").InnerXml);
                                        if (node4.SelectSingleNode("DW") == null)
                                        {
                                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/DW节点！", innerXml);
                                            this.hx_Insert(innerXml, errorTip);
                                        }
                                        else
                                        {
                                            item.Add((SPXX)4, node4.SelectSingleNode("DW").InnerXml);
                                            node5 = node4.SelectSingleNode("XMDJ");
                                            errorTip = this.CheckValueDZ(innerXml, node5, "XMDJ");
                                            if (errorTip != "")
                                            {
                                                this.hx_Insert(innerXml, errorTip);
                                            }
                                            else
                                            {
                                                item.Add((SPXX)5, node4.SelectSingleNode("XMDJ").InnerXml.Trim());
                                                node5 = node4.SelectSingleNode("XMSL");
                                                errorTip = this.CheckValueDZ(innerXml, node5, "XMSL");
                                                if (errorTip != "")
                                                {
                                                    this.hx_Insert(innerXml, errorTip);
                                                }
                                                else
                                                {
                                                    item.Add((SPXX)6, node4.SelectSingleNode("XMSL").InnerXml.Trim());
                                                    node5 = node4.SelectSingleNode("XMJE");
                                                    errorTip = this.CheckValueDZ(innerXml, node5, "XMJE");
                                                    if (errorTip != "")
                                                    {
                                                        this.hx_Insert(innerXml, errorTip);
                                                    }
                                                    else if (node5.InnerXml.Trim() == "")
                                                    {
                                                        errorTip = string.Format("XML文件格式错误，单据{0}中COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/XMJE值不能为空!", innerXml);
                                                        this.hx_Insert(innerXml, errorTip);
                                                    }
                                                    else
                                                    {
                                                        str6 = node4.SelectSingleNode("XMJE").InnerXml.Trim();
                                                        num2 = decimal.Add(num2, decimal.Parse(str6));
                                                        node5 = node4.SelectSingleNode("SL");
                                                        errorTip = this.CheckValueDZ(innerXml, node5, "SL");
                                                        if (errorTip != "")
                                                        {
                                                            this.hx_Insert(innerXml, errorTip);
                                                        }
                                                        else
                                                        {
                                                            str7 = node4.SelectSingleNode("SL").InnerXml.Trim();
                                                            double result = -1.0;
                                                            if (double.TryParse(str7, out result))
                                                            {
                                                                if (!list4.Contains(result))
                                                                {
                                                                    list4.Add(result);
                                                                }
                                                                if (allSlv.Contains(result))
                                                                {
                                                                    goto Label_1162;
                                                                }
                                                                errorTip = string.Format("税率非法！", innerXml);
                                                                this.hx_Insert(innerXml, errorTip);
                                                            }
                                                            else
                                                            {
                                                                errorTip = string.Format("税率非法！", innerXml);
                                                                this.hx_Insert(innerXml, errorTip);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        Label_1162:
                            node5 = node4.SelectSingleNode("SE");
                            errorTip = this.CheckValueDZ(innerXml, node5, "SE");
                            if (errorTip != "")
                            {
                                this.hx_Insert(innerXml, errorTip);
                                break;
                            }
                            string str8 = node4.SelectSingleNode("SE").InnerXml.Trim();
                            item.Add((SPXX)7, str6);
                            if (fpxx.isRed)
                            {
                                item.Add((SPXX)10, "0");
                            }
                            else
                            {
                                item.Add((SPXX)10, str6.Contains("-") ? "4" : "0");
                            }
                            item.Add((SPXX)8, str7);
                            item.Add((SPXX)9, str8);
                            item.Add((SPXX)2, "");
                            item.Add((SPXX)11, "N");
                            fpxx.Mxxx.Add(item);
                            num3++;
                        }
                        if (errorTip == "")
                        {
                            if (num3 > 100)
                            {
                                this.hx_Insert(innerXml, "电子增值税普通发票最大允许100行商品明细！");
                            }
                            else if (!this.CheckErr(fpxx.je, num2.ToString(), 2, "0.01"))
                            {
                                this.hx_Insert(innerXml, string.Format("XML文件格式错误，单据{0}中商品行明细之和与HJJE的误差超过0.01！", innerXml));
                            }
                            else
                            {
                                if (list4.Count == 1)
                                {
                                    fpxx.sLv = list4[0].ToString("F3");
                                }
                                else
                                {
                                    fpxx.sLv = "";
                                }
                                Djfp djfp = new Djfp(innerXml);
                                fpxx.xsdjbh = innerXml;
                                djfp.File = str;
                                djfp.Fpxx = fpxx;
                                list.Add(djfp);
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                errorTip = "单据文件格式错误：" + exception.Message;
                this.log.Error(exception.Message);
                string[] textArray1 = new string[] { errorTip };
                MessageManager.ShowMsgBox("INP-242202", textArray1);
            }
            return list;
        }

        public List<Djfp> ParseDZDjFileManual(FPLX fplx, string djFileName, out string errorTip, string sqslv)
        {
            errorTip = "";
            List<Djfp> list = new List<Djfp>();
            List<double> allSlv = new FpManager().GetAllSlv(sqslv, false);
            try
            {
                XmlDocument document1 = new XmlDocument();
                document1.Load(djFileName);
                XmlNodeList list3 = document1.SelectNodes("/business/REQUEST_COMMON_FPKJ");
                if ((list3 == null) || (list3.Count == 0))
                {
                    errorTip = "XML文件格式错误，找不到 business/REQUEST_COMMON_FPKJ 节点!";
                    return list;
                }
                List<double> list4 = new List<double>();
                string str = djFileName.Substring(djFileName.LastIndexOf(@"\") + 1);
                foreach (XmlNode node in list3)
                {
                    decimal num;
                    errorTip = "";
                    djh_hx = "";
                    XmlNode node2 = node.SelectSingleNode("COMMON_FPKJ_FPT");
                    XmlNode node3 = node2.SelectSingleNode("FPQQLSH");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据中不存在FPQQLSH节点！", new object[0]);
                        return list;
                    }
                    if (node3.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据号中FPQQLSH值不能为空!", new object[0]);
                        return list;
                    }
                    string innerXml = node2.SelectSingleNode("FPQQLSH").InnerXml;
                    djh_hx = innerXml;
                    node3 = node2.SelectSingleNode("KPLX");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在KPLX节点！", innerXml);
                        return list;
                    }
                    if (node3.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中KPLX值不能为空!", innerXml);
                        return list;
                    }
                    if ((node3.InnerXml.Trim() != "0") && (node3.InnerXml.Trim() != "1"))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中KPLX值只能为0或1!", innerXml);
                        return list;
                    }
                    string str3 = node2.SelectSingleNode("KPLX").InnerXml;
                    node3 = node2.SelectSingleNode("YFP_DM");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在YFP_DM节点！", innerXml);
                        return list;
                    }
                    if ((str3 == "1") && ((node3.InnerText.Trim() == "") || ((node3.InnerText.Trim() != "") && !IsNumber(node3.InnerText.Trim()))))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中YFP_DM值格式不正确！", innerXml);
                        return list;
                    }
                    if ((str3 == "1") && (node3.InnerText.Trim().Length != 12))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中YFP_DM应为12位电子发票代码！", innerXml);
                        return list;
                    }
                    string str4 = node2.SelectSingleNode("YFP_DM").InnerXml;
                    node3 = node2.SelectSingleNode("YFP_HM");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在YFP_HM节点！", innerXml);
                        return list;
                    }
                    if ((str3 == "1") && ((node3.InnerText.Trim() == "") || ((node3.InnerText.Trim() != "") && !IsNumber(node3.InnerText.Trim()))))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中YFP_HM值格式不正确！", innerXml);
                        return list;
                    }
                    if ((str3 == "1") && (node3.InnerText.Trim().Length > 8))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中YFP_HM节点值最多为8位！", innerXml);
                        return list;
                    }
                    string str5 = node2.SelectSingleNode("YFP_HM").InnerXml;
                    Fpxx fpxx = new Fpxx(fplx, "", "") {
                        blueFpdm = str4,
                        blueFphm = str5
                    };
                    if (!this.isWM())
                    {
                        node3 = node2.SelectSingleNode("BMB_BBH");
                        if (node3 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据中不存在BMB_BBH节点！", new object[0]);
                            return list;
                        }
                        fpxx.bmbbbh = node3.InnerXml;
                        if ((fpxx.bmbbbh == "") || (ToolUtil.GetByteCount(fpxx.bmbbbh) > this.bmbbh_maxlength))
                        {
                            errorTip = string.Format("BMB_BBH节点内容错误！", innerXml);
                            return list;
                        }
                    }
                    if (node2.SelectSingleNode("XSF_NSRSBH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在XSF_NSRSBH节点！", innerXml);
                        return list;
                    }
                    fpxx.xfsh = node2.SelectSingleNode("XSF_NSRSBH").InnerXml;
                    if (node2.SelectSingleNode("XSF_MC") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在XSF_MC节点！", innerXml);
                        return list;
                    }
                    fpxx.xfmc = node2.SelectSingleNode("XSF_MC").InnerXml;
                    if (node2.SelectSingleNode("XSF_DZDH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在XSF_DZDH节点！", innerXml);
                        return list;
                    }
                    fpxx.xfdzdh = node2.SelectSingleNode("XSF_DZDH").InnerXml;
                    if (node2.SelectSingleNode("XSF_YHZH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在XSF_YHZH节点！", innerXml);
                        return list;
                    }
                    fpxx.xfyhzh = node2.SelectSingleNode("XSF_YHZH").InnerXml;
                    if (node2.SelectSingleNode("GMF_NSRSBH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在GMF_NSRSBH节点！", innerXml);
                        return list;
                    }
                    fpxx.gfsh = node2.SelectSingleNode("GMF_NSRSBH").InnerXml;
                    if (node2.SelectSingleNode("GMF_MC") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在GMF_MC节点！", innerXml);
                        return list;
                    }
                    fpxx.gfmc = node2.SelectSingleNode("GMF_MC").InnerXml;
                    if (node2.SelectSingleNode("GMF_DZDH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在GMF_DZDH节点！", innerXml);
                        return list;
                    }
                    fpxx.gfdzdh = node2.SelectSingleNode("GMF_DZDH").InnerXml;
                    if (node2.SelectSingleNode("GMF_YHZH") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在GMF_YHZH节点！", innerXml);
                        return list;
                    }
                    fpxx.gfyhzh = node2.SelectSingleNode("GMF_YHZH").InnerXml;
                    if (node2.SelectSingleNode("KPR") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在KPR节点！", innerXml);
                        return list;
                    }
                    fpxx.kpr = node2.SelectSingleNode("KPR").InnerXml;
                    if (node2.SelectSingleNode("SKR") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在SKR节点！", innerXml);
                        return list;
                    }
                    fpxx.skr = node2.SelectSingleNode("SKR").InnerXml;
                    if (node2.SelectSingleNode("FHR") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在FHR节点！", innerXml);
                        return list;
                    }
                    fpxx.fhr = node2.SelectSingleNode("FHR").InnerXml;
                    node3 = node2.SelectSingleNode("BZ");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在BZ节点！", innerXml);
                        return list;
                    }
                    if (ToolUtil.GetByteCount(node3.InnerText.Trim()) > 130)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中BZ长度限制为130！", innerXml);
                        return list;
                    }
                    fpxx.bz = node2.SelectSingleNode("BZ").InnerXml;
                    node3 = node2.SelectSingleNode("HJJE");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在HJJE节点！", innerXml);
                        return list;
                    }
                    if ((node3.InnerText.Trim() == "") || !decimal.TryParse(node3.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中HJJE值格式不正确！", innerXml);
                        return list;
                    }
                    fpxx.je = node2.SelectSingleNode("HJJE").InnerXml.Trim();
                    if (fpxx.je.Contains("-") && (str3 == "1"))
                    {
                        fpxx.isRed = true;
                    }
                    else if (!fpxx.je.Contains("-") && (str3 == "0"))
                    {
                        fpxx.isRed = false;
                    }
                    else
                    {
                        errorTip = string.Format("KPLX与金额的格式不统一！", innerXml);
                        return list;
                    }
                    node3 = node2.SelectSingleNode("HJSE");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在HJSE节点！", innerXml);
                        return list;
                    }
                    if ((node3.InnerText.Trim() == "") || !decimal.TryParse(node3.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中HJSE值格式不正确！", innerXml);
                        return list;
                    }
                    fpxx.se = node2.SelectSingleNode("HJSE").InnerXml.Trim();
                    node3 = node2.SelectSingleNode("JSHJ");
                    if (node3 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在JSHJ节点！", innerXml);
                        return list;
                    }
                    if ((node3.InnerText.Trim() == "") || !decimal.TryParse(node3.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中JSHJ值格式不正确！", innerXml);
                        return list;
                    }
                    if (decimal.Compare(decimal.Add(decimal.Parse(fpxx.je), decimal.Parse(fpxx.se)), num) != 0)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中HJJE与HJSE之和，不等于JSHJ值！", innerXml);
                        return list;
                    }
                    if (!string.IsNullOrEmpty(errorTip))
                    {
                        return list;
                    }
                    XmlNodeList list6 = node.SelectNodes("COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX");
                    if (list6 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX节点！", innerXml);
                        return list;
                    }
                    decimal num2 = new decimal();
                    int num3 = 0;
                    fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                    foreach (XmlNode node4 in list6)
                    {
                        XmlNode node5;
                        string str6;
                        string str7;
                        Dictionary<SPXX, string> dictionary = new Dictionary<SPXX, string>();
                        if (node4.SelectSingleNode("FPHXZ") == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/FPHXZ节点！", innerXml);
                        }
                        else
                        {
                            if (!this.isWM())
                            {
                                node5 = node4.SelectSingleNode("SPBM");
                                if (node5 == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/SPBM节点！", innerXml);
                                    break;
                                }
                                string flbm = node5.InnerXml;
                                if (!this.flbmCanUse(flbm))
                                {
                                    errorTip = string.Format("分类编码不可用！", innerXml);
                                    break;
                                }
                                if (node4.SelectSingleNode("ZXBM") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/ZXBM节点！", innerXml);
                                    break;
                                }
                                string str10 = node4.SelectSingleNode("ZXBM").InnerXml;
                                if (ToolUtil.GetByteCount(str10) > this.qyzbm_maxlength)
                                {
                                    errorTip = string.Format("ZXBM节点内容错误！", innerXml);
                                    break;
                                }
                                if (node4.SelectSingleNode("YHZCBS") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/YHZCBS节点！", innerXml);
                                    break;
                                }
                                string xsyh = node4.SelectSingleNode("YHZCBS").InnerXml.ToString();
                                if (((xsyh != "") && (xsyh != "0")) && (xsyh != "1"))
                                {
                                    errorTip = string.Format("YHZCBS节点内容不正确！", innerXml);
                                    break;
                                }
                                if (xsyh == "")
                                {
                                    xsyh = "0";
                                }
                                if (node4.SelectSingleNode("ZZSTSGL") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/ZZSTSGL节点！", innerXml);
                                    break;
                                }
                                string yhzc = node4.SelectSingleNode("ZZSTSGL").InnerXml;
                                if (ToolUtil.GetByteCount(yhzc) > this.yhzc_maxlength)
                                {
                                    errorTip = string.Format("ZZSTSGL节点内容错误！", innerXml);
                                    break;
                                }
                                if (node4.SelectSingleNode("LSLBS") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/LSLBS节点！", innerXml);
                                    break;
                                }
                                string lslvbz = node4.SelectSingleNode("LSLBS").InnerXml;
                                if ((((lslvbz != "0") && (lslvbz != "1")) && ((lslvbz != "2") && (lslvbz != "3"))) && (lslvbz != ""))
                                {
                                    errorTip = string.Format("LSLBS节点内容不正确！", innerXml);
                                    break;
                                }
                                node5 = node4.SelectSingleNode("SL");
                                errorTip = this.CheckValueDZ(innerXml, node5, "SL");
                                if (errorTip != "")
                                {
                                    break;
                                }
                                this.lslbz2yhzc(node4.SelectSingleNode("SL").InnerXml, ref lslvbz, ref yhzc, ref xsyh);
                                dictionary.Add((SPXX)20, flbm);
                                dictionary.Add((SPXX)1, str10);
                                dictionary.Add((SPXX)0x15, xsyh);
                                dictionary.Add((SPXX)0x16, yhzc);
                                dictionary.Add((SPXX)0x17, lslvbz);
                            }
                            else
                            {
                                dictionary.Add((SPXX)1, "");
                                dictionary.Add((SPXX)20, "");
                                dictionary.Add((SPXX)0x15, "0");
                                dictionary.Add((SPXX)0x16, "");
                                dictionary.Add((SPXX)0x17, "");
                            }
                            if (node4.SelectSingleNode("XMMC") == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/XMMC节点！", innerXml);
                            }
                            else
                            {
                                dictionary.Add(0, node4.SelectSingleNode("XMMC").InnerXml);
                                if (node4.SelectSingleNode("GGXH") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/GGXH节点！", innerXml);
                                }
                                else
                                {
                                    dictionary.Add((SPXX)3, node4.SelectSingleNode("GGXH").InnerXml);
                                    if (node4.SelectSingleNode("DW") == null)
                                    {
                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/DW节点！", innerXml);
                                    }
                                    else
                                    {
                                        dictionary.Add((SPXX)4, node4.SelectSingleNode("DW").InnerXml);
                                        node5 = node4.SelectSingleNode("XMDJ");
                                        errorTip = this.CheckValueDZ(innerXml, node5, "XMDJ");
                                        if (errorTip == "")
                                        {
                                            dictionary.Add((SPXX)5, node4.SelectSingleNode("XMDJ").InnerXml.Trim());
                                            node5 = node4.SelectSingleNode("XMSL");
                                            errorTip = this.CheckValueDZ(innerXml, node5, "XMSL");
                                            if (errorTip == "")
                                            {
                                                dictionary.Add((SPXX)6, node4.SelectSingleNode("XMSL").InnerXml.Trim());
                                                node5 = node4.SelectSingleNode("XMJE");
                                                errorTip = this.CheckValueDZ(innerXml, node5, "XMJE");
                                                if (errorTip == "")
                                                {
                                                    if (node5.InnerXml.Trim() == "")
                                                    {
                                                        errorTip = string.Format("XML文件格式错误，单据{0}中COMMON_FPKJ_XMXXS/COMMON_FPKJ_XMXX/XMJE值不能为空!", innerXml);
                                                    }
                                                    else
                                                    {
                                                        str6 = node4.SelectSingleNode("XMJE").InnerXml.Trim();
                                                        num2 = decimal.Add(num2, decimal.Parse(str6));
                                                        node5 = node4.SelectSingleNode("SL");
                                                        errorTip = this.CheckValueDZ(innerXml, node5, "SL");
                                                        if (errorTip == "")
                                                        {
                                                            str7 = node4.SelectSingleNode("SL").InnerXml.Trim();
                                                            double result = -1.0;
                                                            if (double.TryParse(str7, out result))
                                                            {
                                                                if (!list4.Contains(result))
                                                                {
                                                                    list4.Add(result);
                                                                }
                                                                if (allSlv.Contains(result))
                                                                {
                                                                    goto Label_0F08;
                                                                }
                                                                errorTip = string.Format("税率非法！", innerXml);
                                                            }
                                                            else
                                                            {
                                                                errorTip = string.Format("税率非法！", innerXml);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    Label_0F08:
                        node5 = node4.SelectSingleNode("SE");
                        errorTip = this.CheckValueDZ(innerXml, node5, "SE");
                        if (errorTip != "")
                        {
                            break;
                        }
                        string str8 = node4.SelectSingleNode("SE").InnerXml.Trim();
                        dictionary.Add((SPXX)7, str6);
                        if (fpxx.isRed)
                        {
                            dictionary.Add((SPXX)10, "0");
                        }
                        else
                        {
                            dictionary.Add((SPXX)10, str6.Contains("-") ? "4" : "0");
                        }
                        dictionary.Add((SPXX)8, str7);
                        dictionary.Add((SPXX)9, str8);
                        dictionary.Add((SPXX)2, "");
                        dictionary.Add((SPXX)11, "N");
                        fpxx.Mxxx.Add(dictionary);
                        num3++;
                    }
                    if (errorTip != "")
                    {
                        return list;
                    }
                    if (num3 > 100)
                    {
                        errorTip = "电子增值税普通发票最大允许100行商品明细！";
                        return list;
                    }
                    if (!this.CheckErr(fpxx.je, num2.ToString(), 2, "0.01"))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中商品行明细之和与HJJE的误差超过0.01！", innerXml);
                        return list;
                    }
                    if (list4.Count == 1)
                    {
                        fpxx.sLv = list4[0].ToString("F3");
                    }
                    else
                    {
                        fpxx.sLv = "";
                    }
                    Djfp item = new Djfp(innerXml);
                    fpxx.xsdjbh = innerXml;
                    item.File = str;
                    item.Fpxx = fpxx;
                    list.Add(item);
                }
                return list;
            }
            catch (Exception exception)
            {
                errorTip = "单据文件格式错误：" + exception.Message;
                this.log.Error(exception.Message);
                string[] textArray1 = new string[] { errorTip };
                MessageManager.ShowMsgBox("INP-242202", textArray1);
            }
            return list;
        }

        public List<Djfp> ParseHYDjFile(string djFileName, out string errorTip, string sqslv)
        {
            errorTip = "";
            List<Djfp> list = new List<Djfp>();
            List<double> allSlv = new FpManager().GetAllSlv(sqslv, true);
            try
            {
                XmlDocument document1 = new XmlDocument();
                document1.Load(djFileName);
                XmlNodeList list3 = document1.SelectNodes("/business/body");
                if ((list3 == null) || (list3.Count == 0))
                {
                    errorTip = "XML文件格式错误，找不到 business/body 节点!";
                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:,开具结果:0,开具失败原因:{1}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), errorTip));
                    return list;
                }
                string str = djFileName.Substring(djFileName.LastIndexOf(@"\") + 1);
                foreach (XmlNode node in list3)
                {
                    decimal num;
                    double num2;
                    errorTip = "";
                    djh_hx = "";
                    XmlNode node2 = node.SelectSingleNode("djh");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据中不存在djh节点！", new object[0]);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:,开具结果:0,开具失败原因:{1}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), errorTip));
                        continue;
                    }
                    if (node2.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据号中djh值不能为空!", new object[0]);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:,开具结果:0,开具失败原因:{1}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), errorTip));
                        continue;
                    }
                    string innerXml = node.SelectSingleNode("djh").InnerXml;
                    djh_hx = innerXml;
                    if (node.SelectSingleNode("fpdm") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fpdm节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    string str3 = node.SelectSingleNode("fpdm").InnerXml;
                    if (node.SelectSingleNode("fphm") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fphm节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    string str4 = node.SelectSingleNode("fphm").InnerXml;
                    Fpxx fpxx = new Fpxx((FPLX)11, str3, str4);
                    if (!this.isWM())
                    {
                        node2 = node.SelectSingleNode("bmb_bbh");
                        if (node2 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在bmb_bbh节点！", innerXml);
                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                            return list;
                        }
                        fpxx.bmbbbh = node2.InnerXml;
                        if ((fpxx.bmbbbh == "") || (ToolUtil.GetByteCount(fpxx.bmbbbh) > this.bmbbh_maxlength))
                        {
                            errorTip = string.Format("bmb_bbh节点内容错误！", innerXml);
                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                            return list;
                        }
                    }
                    if (node.SelectSingleNode("shrmc") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在shrmc节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.shrmc = node.SelectSingleNode("shrmc").InnerXml;
                    if (node.SelectSingleNode("shrsbh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在shrsbh节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.shrnsrsbh = node.SelectSingleNode("shrsbh").InnerXml;
                    if (node.SelectSingleNode("fhrmc") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fhrmc节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.fhrmc = node.SelectSingleNode("fhrmc").InnerXml;
                    if (node.SelectSingleNode("fhrsbh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fhrsbh节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.fhrnsrsbh = node.SelectSingleNode("fhrsbh").InnerXml;
                    if (node.SelectSingleNode("spfmc") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在spfmc节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.spfmc = node.SelectSingleNode("spfmc").InnerXml;
                    if (node.SelectSingleNode("spfsbh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在spfsbh节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.spfnsrsbh = node.SelectSingleNode("spfsbh").InnerXml;
                    node2 = node.SelectSingleNode("hjje");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在hjje节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    if ((node2.InnerText.Trim() == "") || !decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中hjje值格式不正确！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.je = node.SelectSingleNode("hjje").InnerXml.Trim();
                    node2 = node.SelectSingleNode("slv");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在slv节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    if ((node2.InnerText.Trim() == "") || !decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中slv值格式不正确！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.sLv = node.SelectSingleNode("slv").InnerXml.Trim();
                    if (double.TryParse(fpxx.sLv, out num2) && !allSlv.Contains(num2))
                    {
                        errorTip = string.Format("税率非法！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    node2 = node.SelectSingleNode("se");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在se节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    if ((node2.InnerText.Trim() == "") || !decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中se值格式不正确！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.se = node.SelectSingleNode("se").InnerXml.Trim();
                    node2 = node.SelectSingleNode("jshj");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在jshj节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    if ((node2.InnerText.Trim() == "") || !decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中jshj值格式不正确！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    if (decimal.Compare(decimal.Add(decimal.Parse(fpxx.je), decimal.Parse(fpxx.se)), num) != 0)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中hjje与se之和，不等于jshj值！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    if (node.SelectSingleNode("skr") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在skr节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.skr = node.SelectSingleNode("skr").InnerXml;
                    if (node.SelectSingleNode("fhr") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fhr节点！", innerXml);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        continue;
                    }
                    fpxx.fhr = node.SelectSingleNode("fhr").InnerXml;
                    if (string.IsNullOrEmpty(errorTip))
                    {
                        XmlNodeList list5 = node.SelectNodes("group");
                        if (list5 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在group节点！", innerXml);
                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                            continue;
                        }
                        fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                        foreach (XmlNode node3 in list5)
                        {
                            decimal num3;
                            Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                            XmlNode node4 = node3.SelectSingleNode("xh");
                            if (node4 == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在group/xh节点！", innerXml);
                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                break;
                            }
                            if (node4.InnerText.Trim() == "")
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中xh值不能为空！", innerXml);
                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                break;
                            }
                            item.Add((SPXX)13, node3.SelectSingleNode("xh").InnerXml);
                            if (!this.isWM())
                            {
                                node4 = node3.SelectSingleNode("spbm");
                                if (node4 == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中spbm节点不存在！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    break;
                                }
                                string flbm = node4.InnerXml;
                                if (!this.flbmCanUse(flbm))
                                {
                                    errorTip = string.Format("分类编码不可用！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    break;
                                }
                                if (node3.SelectSingleNode("zxbm") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中zxbm节点不存在！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    break;
                                }
                                string str6 = node3.SelectSingleNode("zxbm").InnerXml;
                                if (ToolUtil.GetByteCount(str6) > this.qyzbm_maxlength)
                                {
                                    errorTip = string.Format("zxbm节点内容错误！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    break;
                                }
                                if (node3.SelectSingleNode("yhzcbs") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中yhzcbs节点不存在！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    break;
                                }
                                string xsyh = node3.SelectSingleNode("yhzcbs").InnerXml.ToString();
                                if (((xsyh != "") && (xsyh != "0")) && (xsyh != "1"))
                                {
                                    errorTip = string.Format("yhzcbs节点内容不正确！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    break;
                                }
                                if (xsyh == "")
                                {
                                    xsyh = "0";
                                }
                                if (node3.SelectSingleNode("zzstsgl") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中zzstsgl节点不存在！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    break;
                                }
                                string yhzc = node3.SelectSingleNode("zzstsgl").InnerXml;
                                if (ToolUtil.GetByteCount(yhzc) > this.yhzc_maxlength)
                                {
                                    errorTip = string.Format("zzstsgl节点内容错误！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    break;
                                }
                                if (node3.SelectSingleNode("lslbs") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中lslbs节点不存在！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    break;
                                }
                                string lslvbz = node3.SelectSingleNode("lslbs").InnerXml;
                                if ((((lslvbz != "0") && (lslvbz != "1")) && ((lslvbz != "2") && (lslvbz != "3"))) && (lslvbz != ""))
                                {
                                    errorTip = string.Format("lslbs节点内容不正确！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    break;
                                }
                                this.lslbz2yhzc(fpxx.sLv, ref lslvbz, ref yhzc, ref xsyh);
                                item.Add((SPXX)20, flbm);
                                item.Add((SPXX)1, str6);
                                item.Add((SPXX)0x15, xsyh);
                                item.Add((SPXX)0x16, yhzc);
                                item.Add((SPXX)0x17, lslvbz);
                            }
                            else
                            {
                                item.Add((SPXX)1, "");
                                item.Add((SPXX)20, "");
                                item.Add((SPXX)0x15, "0");
                                item.Add((SPXX)0x16, "");
                                item.Add((SPXX)0x17, "");
                            }
                            node4 = node3.SelectSingleNode("xmmc");
                            if (node4 == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在group/xmmc节点！", innerXml);
                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                break;
                            }
                            if (node4.InnerText.Trim() == "")
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中xmmc值不能为空！", innerXml);
                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                break;
                            }
                            item.Add(0, node3.SelectSingleNode("xmmc").InnerXml.Trim());
                            node4 = node3.SelectSingleNode("je");
                            if (node4 == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在group/je节点！", innerXml);
                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                break;
                            }
                            if ((node4.InnerText.Trim() == "") || !decimal.TryParse(node4.InnerText.Trim(), out num3))
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中je值格式不正确！", innerXml);
                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                break;
                            }
                            item.Add((SPXX)7, node3.SelectSingleNode("je").InnerXml.Trim());
                            item[(SPXX)8] = fpxx.sLv;
                            item[(SPXX)9] = decimal.Multiply(decimal.Parse(node3.SelectSingleNode("je").InnerXml), decimal.Parse(fpxx.sLv)).ToString();
                            item[(SPXX)3] = string.Empty;
                            item[(SPXX)4] = string.Empty;
                            item[(SPXX)2] = string.Empty;
                            item[(SPXX)6] = string.Empty;
                            item[(SPXX)5] = string.Empty;
                            item[(SPXX)10] = 0.ToString();
                            item[(SPXX)11] = "0";
                            fpxx.Mxxx.Add(item);
                        }
                        if (errorTip == "")
                        {
                            Djfp djfp = new Djfp(innerXml);
                            fpxx.xsdjbh = innerXml;
                            djfp.File = str;
                            djfp.Fpxx = fpxx;
                            list.Add(djfp);
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                errorTip = "单据文件格式错误：" + exception.Message;
                this.log.Error(exception.Message);
                string[] textArray1 = new string[] { errorTip };
                MessageManager.ShowMsgBox("INP-242202", textArray1);
            }
            return list;
        }

        public List<Djfp> ParseHYDjFileManual(string djFileName, out string errorTip, string sqslv)
        {
            errorTip = "";
            List<Djfp> list = new List<Djfp>();
            List<double> allSlv = new FpManager().GetAllSlv(sqslv, true);
            try
            {
                XmlDocument document1 = new XmlDocument();
                document1.Load(djFileName);
                XmlNodeList list3 = document1.SelectNodes("/business/body");
                if ((list3 == null) || (list3.Count == 0))
                {
                    errorTip = "XML文件格式错误，找不到 business/body 节点!";
                    return list;
                }
                string str = djFileName.Substring(djFileName.LastIndexOf(@"\") + 1);
                foreach (XmlNode node in list3)
                {
                    decimal num;
                    double num2;
                    XmlNode node2 = node.SelectSingleNode("djh");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据中不存在djh节点！", new object[0]);
                        return list;
                    }
                    if (node2.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据号中djh值不能为空!", new object[0]);
                        return list;
                    }
                    string innerXml = node.SelectSingleNode("djh").InnerXml;
                    if (node.SelectSingleNode("fpdm") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fpdm节点！", innerXml);
                        return list;
                    }
                    string str3 = node.SelectSingleNode("fpdm").InnerXml;
                    if (node.SelectSingleNode("fphm") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fphm节点！", innerXml);
                        return list;
                    }
                    string str4 = node.SelectSingleNode("fphm").InnerXml;
                    Fpxx fpxx = new Fpxx((FPLX)11, str3, str4);
                    if (!this.isWM())
                    {
                        node2 = node.SelectSingleNode("bmb_bbh");
                        if (node2 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在bmb_bbh节点！", innerXml);
                            return list;
                        }
                        fpxx.bmbbbh = node2.InnerXml;
                        if ((fpxx.bmbbbh == "") || (ToolUtil.GetByteCount(fpxx.bmbbbh) > this.bmbbh_maxlength))
                        {
                            errorTip = string.Format("bmb_bbh节点内容错误！", innerXml);
                            return list;
                        }
                    }
                    if (node.SelectSingleNode("shrmc") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在shrmc节点！", innerXml);
                        return list;
                    }
                    fpxx.shrmc = node.SelectSingleNode("shrmc").InnerXml;
                    if (node.SelectSingleNode("shrsbh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在shrsbh节点！", innerXml);
                        return list;
                    }
                    fpxx.shrnsrsbh = node.SelectSingleNode("shrsbh").InnerXml;
                    if (node.SelectSingleNode("fhrmc") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fhrmc节点！", innerXml);
                        return list;
                    }
                    fpxx.fhrmc = node.SelectSingleNode("fhrmc").InnerXml;
                    if (node.SelectSingleNode("fhrsbh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fhrsbh节点！", innerXml);
                        return list;
                    }
                    fpxx.fhrnsrsbh = node.SelectSingleNode("fhrsbh").InnerXml;
                    if (node.SelectSingleNode("spfmc") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在spfmc节点！", innerXml);
                        return list;
                    }
                    fpxx.spfmc = node.SelectSingleNode("spfmc").InnerXml;
                    if (node.SelectSingleNode("spfsbh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在spfsbh节点！", innerXml);
                        return list;
                    }
                    fpxx.spfnsrsbh = node.SelectSingleNode("spfsbh").InnerXml;
                    node2 = node.SelectSingleNode("hjje");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在hjje节点！", innerXml);
                        return list;
                    }
                    if ((node2.InnerText.Trim() == "") || !decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中hjje值格式不正确！", innerXml);
                        return list;
                    }
                    fpxx.je = node.SelectSingleNode("hjje").InnerXml.Trim();
                    node2 = node.SelectSingleNode("slv");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在slv节点！", innerXml);
                        return list;
                    }
                    if ((node2.InnerText.Trim() == "") || !decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中slv值格式不正确！", innerXml);
                        return list;
                    }
                    fpxx.sLv = node.SelectSingleNode("slv").InnerXml.Trim();
                    if (double.TryParse(fpxx.sLv, out num2) && !allSlv.Contains(num2))
                    {
                        errorTip = string.Format("税率非法！", innerXml);
                        return list;
                    }
                    node2 = node.SelectSingleNode("se");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在se节点！", innerXml);
                        return list;
                    }
                    if ((node2.InnerText.Trim() == "") || !decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中se值格式不正确！", innerXml);
                        return list;
                    }
                    fpxx.se = node.SelectSingleNode("se").InnerXml.Trim();
                    node2 = node.SelectSingleNode("jshj");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在jshj节点！", innerXml);
                        return list;
                    }
                    if ((node2.InnerText.Trim() == "") || !decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中jshj值格式不正确！", innerXml);
                        return list;
                    }
                    if (decimal.Compare(decimal.Add(decimal.Parse(fpxx.je), decimal.Parse(fpxx.se)), num) != 0)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中hjje与se之和，不等于jshj值！", innerXml);
                        return list;
                    }
                    if (node.SelectSingleNode("skr") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在skr节点！", innerXml);
                        return list;
                    }
                    fpxx.skr = node.SelectSingleNode("skr").InnerXml;
                    if (node.SelectSingleNode("fhr") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fhr节点！", innerXml);
                        return list;
                    }
                    fpxx.fhr = node.SelectSingleNode("fhr").InnerXml;
                    if (!string.IsNullOrEmpty(errorTip))
                    {
                        return list;
                    }
                    XmlNodeList list5 = node.SelectNodes("group");
                    if (list5 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在group节点！", innerXml);
                        return list;
                    }
                    fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                    foreach (XmlNode node3 in list5)
                    {
                        decimal num3;
                        Dictionary<SPXX, string> dictionary = new Dictionary<SPXX, string>();
                        XmlNode node4 = node3.SelectSingleNode("xh");
                        if (node4 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在group/xh节点！", innerXml);
                            break;
                        }
                        if (node4.InnerText.Trim() == "")
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中xh值不能为空！", innerXml);
                            break;
                        }
                        dictionary.Add((SPXX)13, node3.SelectSingleNode("xh").InnerXml);
                        if (!this.isWM())
                        {
                            node4 = node3.SelectSingleNode("spbm");
                            if (node4 == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中spbm节点不存在！", innerXml);
                                break;
                            }
                            string flbm = node4.InnerXml;
                            if (!this.flbmCanUse(flbm))
                            {
                                errorTip = string.Format("分类编码不可用！", innerXml);
                                break;
                            }
                            if (node3.SelectSingleNode("zxbm") == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中zxbm节点不存在！", innerXml);
                                break;
                            }
                            string str6 = node3.SelectSingleNode("zxbm").InnerXml;
                            if (ToolUtil.GetByteCount(str6) > this.qyzbm_maxlength)
                            {
                                errorTip = string.Format("zxbm节点内容错误！", innerXml);
                                break;
                            }
                            if (node3.SelectSingleNode("yhzcbs") == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中yhzcbs节点不存在！", innerXml);
                                break;
                            }
                            string xsyh = node3.SelectSingleNode("yhzcbs").InnerXml.ToString();
                            if (((xsyh != "") && (xsyh != "0")) && (xsyh != "1"))
                            {
                                errorTip = string.Format("yhzcbs节点内容不正确！", innerXml);
                                break;
                            }
                            if (xsyh == "")
                            {
                                xsyh = "0";
                            }
                            if (node3.SelectSingleNode("zzstsgl") == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中zzstsgl节点不存在！", innerXml);
                                break;
                            }
                            string yhzc = node3.SelectSingleNode("zzstsgl").InnerXml;
                            if (ToolUtil.GetByteCount(yhzc) > this.yhzc_maxlength)
                            {
                                errorTip = string.Format("zzstsgl节点内容错误！", innerXml);
                                break;
                            }
                            if (node3.SelectSingleNode("lslbs") == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中lslbs节点不存在！", innerXml);
                                break;
                            }
                            string lslvbz = node3.SelectSingleNode("lslbs").InnerXml;
                            if ((((lslvbz != "0") && (lslvbz != "1")) && ((lslvbz != "2") && (lslvbz != "3"))) && (lslvbz != ""))
                            {
                                errorTip = string.Format("lslbs节点内容不正确！", innerXml);
                                break;
                            }
                            this.lslbz2yhzc(fpxx.sLv, ref lslvbz, ref yhzc, ref xsyh);
                            dictionary.Add((SPXX)20, flbm);
                            dictionary.Add((SPXX)1, str6);
                            dictionary.Add((SPXX)0x15, xsyh);
                            dictionary.Add((SPXX)0x16, yhzc);
                            dictionary.Add((SPXX)0x17, lslvbz);
                        }
                        else
                        {
                            dictionary.Add((SPXX)1, "");
                            dictionary.Add((SPXX)20, "");
                            dictionary.Add((SPXX)0x15, "0");
                            dictionary.Add((SPXX)0x16, "");
                            dictionary.Add((SPXX)0x17, "");
                        }
                        node4 = node3.SelectSingleNode("xmmc");
                        if (node4 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在group/xmmc节点！", innerXml);
                            break;
                        }
                        if (node4.InnerText.Trim() == "")
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中xmmc值不能为空！", innerXml);
                            break;
                        }
                        dictionary.Add(0, node3.SelectSingleNode("xmmc").InnerXml.Trim());
                        node4 = node3.SelectSingleNode("je");
                        if (node4 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在group/je节点！", innerXml);
                            break;
                        }
                        if ((node4.InnerText.Trim() == "") || !decimal.TryParse(node4.InnerText.Trim(), out num3))
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中je值格式不正确！", innerXml);
                            break;
                        }
                        dictionary.Add((SPXX)7, node3.SelectSingleNode("je").InnerXml.Trim());
                        dictionary[(SPXX)8] = fpxx.sLv;
                        dictionary[(SPXX)9] = decimal.Multiply(decimal.Parse(node3.SelectSingleNode("je").InnerXml), decimal.Parse(fpxx.sLv)).ToString();
                        dictionary[(SPXX)3] = string.Empty;
                        dictionary[(SPXX)4] = string.Empty;
                        dictionary[(SPXX)2] = string.Empty;
                        dictionary[(SPXX)6] = string.Empty;
                        dictionary[(SPXX)5] = string.Empty;
                        dictionary[(SPXX)10] = 0.ToString();
                        dictionary[(SPXX)11] = "0";
                        fpxx.Mxxx.Add(dictionary);
                    }
                    if (errorTip != "")
                    {
                        return list;
                    }
                    Djfp item = new Djfp(innerXml);
                    fpxx.xsdjbh = innerXml;
                    item.File = str;
                    item.Fpxx = fpxx;
                    list.Add(item);
                }
                return list;
            }
            catch (Exception exception)
            {
                errorTip = exception.Message;
            }
            return list;
        }

        public List<Djfp> ParseJDCDjFile(string djFileName, out string errorTip, string sqslv)
        {
            errorTip = "";
            List<Djfp> list = new List<Djfp>();
            List<double> allSlv = new FpManager().GetAllSlv(sqslv, false);
            try
            {
                XmlDocument document1 = new XmlDocument();
                document1.Load(djFileName);
                XmlNodeList list3 = document1.SelectNodes("/business/body");
                if ((list3 == null) || (list3.Count == 0))
                {
                    errorTip = "XML文件格式错误，找不到 business/body 节点!";
                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:,开具结果:0,开具失败原因:{1}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), errorTip));
                    return list;
                }
                string str = djFileName.Substring(djFileName.LastIndexOf(@"\") + 1);
                foreach (XmlNode node in list3)
                {
                    errorTip = "";
                    djh_hx = "";
                    XmlNode node2 = node.SelectSingleNode("djh");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据中不存在djh节点！", new object[0]);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:,开具结果:0,开具失败原因:{1}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), errorTip));
                    }
                    else if (node2.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据号中djh值不能为空!", new object[0]);
                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:,开具结果:0,开具失败原因:{1}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), errorTip));
                    }
                    else
                    {
                        string innerXml = node.SelectSingleNode("djh").InnerXml;
                        djh_hx = innerXml;
                        if (node.SelectSingleNode("fpdm") == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在fpdm节点！", innerXml);
                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                        }
                        else
                        {
                            string str3 = node.SelectSingleNode("fpdm").InnerXml;
                            if (node.SelectSingleNode("fphm") == null)
                            {
                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在fphm节点！", innerXml);
                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                            }
                            else
                            {
                                string str4 = node.SelectSingleNode("fphm").InnerXml;
                                Fpxx fpxx = new Fpxx((FPLX)12, str3, str4);
                                if (node.SelectSingleNode("gfdwmc") == null)
                                {
                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在gfdwmc节点！", innerXml);
                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                }
                                else
                                {
                                    fpxx.gfmc = node.SelectSingleNode("gfdwmc").InnerXml;
                                    if (!this.isWM())
                                    {
                                        if (node.SelectSingleNode("bmb_bbh") == null)
                                        {
                                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在bmb_bbh节点！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        fpxx.bmbbbh = node.SelectSingleNode("bmb_bbh").InnerXml;
                                        if ((fpxx.bmbbbh == "") || (ToolUtil.GetByteCount(fpxx.bmbbbh) > this.bmbbh_maxlength))
                                        {
                                            errorTip = string.Format("bmb_bbh节点内容错误！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        node2 = node.SelectSingleNode("spbm");
                                        if (node2 == null)
                                        {
                                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在spbm节点！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        string flbm = node2.InnerXml;
                                        if (!this.flbmCanUse(flbm))
                                        {
                                            errorTip = string.Format("分类编码不可用！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        node2 = node.SelectSingleNode("zxbm");
                                        if (node2 == null)
                                        {
                                            errorTip = string.Format("XML文件格式错误，单据{0}中zxbm节点不存在！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        string str6 = node2.InnerXml;
                                        if (ToolUtil.GetByteCount(str6) > this.qyzbm_maxlength)
                                        {
                                            errorTip = string.Format("zxbm节点内容错误！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        if (node.SelectSingleNode("yhzcbs") == null)
                                        {
                                            errorTip = string.Format("XML文件格式错误，单据{0}中yhzcbs节点不存在！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        string xsyh = node.SelectSingleNode("yhzcbs").InnerXml.ToString();
                                        if (((xsyh != "") && (xsyh != "0")) && (xsyh != "1"))
                                        {
                                            errorTip = string.Format("yhzcbs节点内容不正确！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        if (xsyh == "")
                                        {
                                            xsyh = "0";
                                        }
                                        node2 = node.SelectSingleNode("zzstsgl");
                                        if (node2 == null)
                                        {
                                            errorTip = string.Format("XML文件格式错误，单据{0}中zzstsgl节点不存在！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        string yhzc = node2.InnerXml;
                                        if (ToolUtil.GetByteCount(yhzc) > this.yhzc_maxlength)
                                        {
                                            errorTip = string.Format("zzstsgl节点内容错误！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        node2 = node.SelectSingleNode("lslbs");
                                        if (node2 == null)
                                        {
                                            errorTip = string.Format("XML文件格式错误，单据{0}中lslbs节点不存在！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        string lslvbz = node2.InnerXml;
                                        if ((((lslvbz != "0") && (lslvbz != "1")) && ((lslvbz != "2") && (lslvbz != "3"))) && (lslvbz != ""))
                                        {
                                            errorTip = string.Format("lslbs节点内容不正确！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            return list;
                                        }
                                        if (node.SelectSingleNode("zzssl") == null)
                                        {
                                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在zzssl节点！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            goto Label_168D;
                                        }
                                        this.lslbz2yhzc(node.SelectSingleNode("zzssl").InnerXml, ref lslvbz, ref yhzc, ref xsyh);
                                        fpxx.zyspmc = flbm;
                                        fpxx.zyspsm = str6 + "#%" + yhzc;
                                        fpxx.skr = xsyh + "#%" + lslvbz;
                                    }
                                    else
                                    {
                                        fpxx.bmbbbh = "";
                                        fpxx.zyspmc = "";
                                        fpxx.zyspsm = "";
                                        fpxx.skr = "";
                                    }
                                    if (node.SelectSingleNode("sfzhm") == null)
                                    {
                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在sfzhm节点！", innerXml);
                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                    }
                                    else
                                    {
                                        fpxx.sfzhm = node.SelectSingleNode("sfzhm").InnerXml;
                                        if (node.SelectSingleNode("gfdwsbh") == null)
                                        {
                                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在gfdwsbh节点！", innerXml);
                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                        }
                                        else
                                        {
                                            fpxx.gfsh = node.SelectSingleNode("gfdwsbh").InnerXml;
                                            if (node.SelectSingleNode("cllx") == null)
                                            {
                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在cllx节点！", innerXml);
                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                            }
                                            else
                                            {
                                                fpxx.cllx = node.SelectSingleNode("cllx").InnerXml;
                                                if (node.SelectSingleNode("cpxh") == null)
                                                {
                                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在cpxh节点！", innerXml);
                                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                }
                                                else
                                                {
                                                    fpxx.cpxh = node.SelectSingleNode("cpxh").InnerXml;
                                                    if (node.SelectSingleNode("cd") == null)
                                                    {
                                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在cd节点！", innerXml);
                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                    }
                                                    else
                                                    {
                                                        fpxx.cd = node.SelectSingleNode("cd").InnerXml;
                                                        if (node.SelectSingleNode("hgzh") == null)
                                                        {
                                                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在hgzh节点！", innerXml);
                                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                        }
                                                        else
                                                        {
                                                            fpxx.hgzh = node.SelectSingleNode("hgzh").InnerXml;
                                                            if (node.SelectSingleNode("jkzmsh") == null)
                                                            {
                                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在jkzmsh节点！", innerXml);
                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                            }
                                                            else
                                                            {
                                                                fpxx.jkzmsh = node.SelectSingleNode("jkzmsh").InnerXml;
                                                                if (node.SelectSingleNode("sjdh") == null)
                                                                {
                                                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在sjdh节点！", innerXml);
                                                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                }
                                                                else
                                                                {
                                                                    fpxx.sjdh = node.SelectSingleNode("sjdh").InnerXml;
                                                                    if (node.SelectSingleNode("fdjhm") == null)
                                                                    {
                                                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fdjhm节点！", innerXml);
                                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                    }
                                                                    else
                                                                    {
                                                                        fpxx.fdjhm = node.SelectSingleNode("fdjhm").InnerXml;
                                                                        if (node.SelectSingleNode("clsbdh") == null)
                                                                        {
                                                                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在clsbdh节点！", innerXml);
                                                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                        }
                                                                        else
                                                                        {
                                                                            fpxx.clsbdh = node.SelectSingleNode("clsbdh").InnerXml;
                                                                            if (node.SelectSingleNode("dh") == null)
                                                                            {
                                                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在dh节点！", innerXml);
                                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                            }
                                                                            else
                                                                            {
                                                                                fpxx.xfdh = node.SelectSingleNode("dh").InnerXml;
                                                                                if (node.SelectSingleNode("zh") == null)
                                                                                {
                                                                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在zh节点！", innerXml);
                                                                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                }
                                                                                else
                                                                                {
                                                                                    fpxx.xfzh = node.SelectSingleNode("zh").InnerXml;
                                                                                    if (node.SelectSingleNode("dz") == null)
                                                                                    {
                                                                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在dz节点！", innerXml);
                                                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        fpxx.xfdz = node.SelectSingleNode("dz").InnerXml;
                                                                                        if (node.SelectSingleNode("khyh") == null)
                                                                                        {
                                                                                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在khyh节点！", innerXml);
                                                                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            fpxx.xfyh = node.SelectSingleNode("khyh").InnerXml;
                                                                                            if (node.SelectSingleNode("dw") == null)
                                                                                            {
                                                                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在dw节点！", innerXml);
                                                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                fpxx.dw = node.SelectSingleNode("dw").InnerXml;
                                                                                                if (node.SelectSingleNode("xcrs") == null)
                                                                                                {
                                                                                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在xcrs节点！", innerXml);
                                                                                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    fpxx.xcrs = node.SelectSingleNode("xcrs").InnerXml;
                                                                                                    if (node.SelectSingleNode("scqymc") == null)
                                                                                                    {
                                                                                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在scqymc节点！", innerXml);
                                                                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        fpxx.sccjmc = node.SelectSingleNode("scqymc").InnerXml;
                                                                                                        node2 = node.SelectSingleNode("bhsj");
                                                                                                        if (node2 == null)
                                                                                                        {
                                                                                                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在bhsj节点！", innerXml);
                                                                                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                        }
                                                                                                        else if (node2.InnerXml.Trim() == "")
                                                                                                        {
                                                                                                            errorTip = string.Format("XML文件格式错误，单据{0}中bhsj值不能为空!", innerXml);
                                                                                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            decimal num;
                                                                                                            if (!decimal.TryParse(node2.InnerText.Trim(), out num))
                                                                                                            {
                                                                                                                errorTip = string.Format("XML文件格式错误，单据{0}中bhsj值格式不正确！", innerXml);
                                                                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                            }
                                                                                                            else if (num == decimal.Zero)
                                                                                                            {
                                                                                                                errorTip = string.Format("XML文件格式错误，单据{0}中bhsj值不能为0！", innerXml);
                                                                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                fpxx.je = node.SelectSingleNode("bhsj").InnerXml.Trim();
                                                                                                                node2 = node.SelectSingleNode("zzsse");
                                                                                                                if (node2 == null)
                                                                                                                {
                                                                                                                    errorTip = string.Format("XML文件格式错误，单据{0}中不存在zzsse节点！", innerXml);
                                                                                                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                }
                                                                                                                else if (node2.InnerXml.Trim() == "")
                                                                                                                {
                                                                                                                    errorTip = string.Format("XML文件格式错误，单据{0}中zzsse值不能为空!", innerXml);
                                                                                                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                }
                                                                                                                else if (!decimal.TryParse(node2.InnerText.Trim(), out num))
                                                                                                                {
                                                                                                                    errorTip = string.Format("XML文件格式错误，单据{0}中zzsse值格式不正确！", innerXml);
                                                                                                                    AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    fpxx.se = node.SelectSingleNode("zzsse").InnerXml.Trim();
                                                                                                                    node2 = node.SelectSingleNode("zzssl");
                                                                                                                    if (node2 == null)
                                                                                                                    {
                                                                                                                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在zzssl节点！", innerXml);
                                                                                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                    }
                                                                                                                    else if (node2.InnerXml.Trim() == "")
                                                                                                                    {
                                                                                                                        errorTip = string.Format("XML文件格式错误，单据{0}中zzssl值不能为空!", innerXml);
                                                                                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                    }
                                                                                                                    else if (!decimal.TryParse(node2.InnerText.Trim(), out num))
                                                                                                                    {
                                                                                                                        errorTip = string.Format("XML文件格式错误，单据{0}中zzssl值格式不正确！", innerXml);
                                                                                                                        AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        double num2;
                                                                                                                        fpxx.sLv = node.SelectSingleNode("zzssl").InnerXml.Trim();
                                                                                                                        if (double.TryParse(fpxx.sLv, out num2) && !allSlv.Contains(num2))
                                                                                                                        {
                                                                                                                            errorTip = string.Format("税率非法！", innerXml);
                                                                                                                            AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            node2 = node.SelectSingleNode("jshj");
                                                                                                                            if (node2 == null)
                                                                                                                            {
                                                                                                                                errorTip = string.Format("XML文件格式错误，单据{0}中不存在jshj节点！", innerXml);
                                                                                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                            }
                                                                                                                            else if (node2.InnerXml.Trim() == "")
                                                                                                                            {
                                                                                                                                errorTip = string.Format("XML文件格式错误，单据{0}中jshj值不能为空!", innerXml);
                                                                                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                            }
                                                                                                                            else if (!decimal.TryParse(node2.InnerText.Trim(), out num))
                                                                                                                            {
                                                                                                                                errorTip = string.Format("XML文件格式错误，单据{0}中jshj值格式不正确！", innerXml);
                                                                                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                            }
                                                                                                                            else if (num == decimal.Zero)
                                                                                                                            {
                                                                                                                                errorTip = string.Format("XML文件格式错误，单据{0}中jshj值不能为0！", innerXml);
                                                                                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                            }
                                                                                                                            else if (decimal.Compare(decimal.Add(decimal.Parse(fpxx.je), decimal.Parse(fpxx.se)), num) != 0)
                                                                                                                            {
                                                                                                                                errorTip = string.Format("XML文件格式错误，单据{0}中bhsj与zzsse之和，不等于jshj值！", innerXml);
                                                                                                                                AutoImport.writer.WriteLine(string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:{2}", this.taxCard.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djh_hx, errorTip));
                                                                                                                            }
                                                                                                                            else if (string.IsNullOrEmpty(errorTip) && (errorTip == ""))
                                                                                                                            {
                                                                                                                                Djfp item = new Djfp(innerXml);
                                                                                                                                fpxx.xsdjbh = innerXml;
                                                                                                                                item.File = str;
                                                                                                                                item.Fpxx = fpxx;
                                                                                                                                list.Add(item);
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                Label_168D:;
                                }
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                errorTip = "单据文件格式错误：" + exception.Message;
                this.log.Error(exception.Message);
                string[] textArray1 = new string[] { errorTip };
                MessageManager.ShowMsgBox("INP-242202", textArray1);
            }
            return list;
        }

        public List<Djfp> ParseJDCDjFileManual(string djFileName, out string errorTip, string sqslv)
        {
            errorTip = "";
            List<Djfp> list = new List<Djfp>();
            List<double> allSlv = new FpManager().GetAllSlv(sqslv, false);
            try
            {
                XmlDocument document1 = new XmlDocument();
                document1.Load(djFileName);
                XmlNodeList list3 = document1.SelectNodes("/business/body");
                if ((list3 == null) || (list3.Count == 0))
                {
                    errorTip = "XML文件格式错误，找不到 business/body 节点!";
                    return list;
                }
                string str = djFileName.Substring(djFileName.LastIndexOf(@"\") + 1);
                foreach (XmlNode node in list3)
                {
                    decimal num;
                    double num2;
                    XmlNode node2 = node.SelectSingleNode("djh");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据中不存在djh节点！", new object[0]);
                        return list;
                    }
                    if (node2.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据号中djh值不能为空!", new object[0]);
                        return list;
                    }
                    string str2 = node.SelectSingleNode("djh").InnerXml.Trim();
                    if (node.SelectSingleNode("fpdm") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fpdm节点！", str2);
                        return list;
                    }
                    string innerXml = node.SelectSingleNode("fpdm").InnerXml;
                    if (node.SelectSingleNode("fphm") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fphm节点！", str2);
                        return list;
                    }
                    string str4 = node.SelectSingleNode("fphm").InnerXml;
                    Fpxx fpxx = new Fpxx((FPLX)12, innerXml, str4);
                    if (node.SelectSingleNode("gfdwmc") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在gfdwmc节点！", str2);
                        return list;
                    }
                    fpxx.gfmc = node.SelectSingleNode("gfdwmc").InnerXml;
                    if (!this.isWM())
                    {
                        if (node.SelectSingleNode("bmb_bbh") == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在bmb_bbh节点！", str2);
                            return list;
                        }
                        fpxx.bmbbbh = node.SelectSingleNode("bmb_bbh").InnerXml;
                        if ((fpxx.bmbbbh == "") || (ToolUtil.GetByteCount(fpxx.bmbbbh) > this.bmbbh_maxlength))
                        {
                            errorTip = string.Format("bmb_bbh节点内容错误！", str2);
                            return list;
                        }
                        node2 = node.SelectSingleNode("spbm");
                        if (node2 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中spbm节点不存在！", str2);
                            return list;
                        }
                        string flbm = node2.InnerXml;
                        if (!this.flbmCanUse(flbm))
                        {
                            errorTip = string.Format("分类编码不可用！", str2);
                            return list;
                        }
                        node2 = node.SelectSingleNode("zxbm");
                        if (node2 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中zxbm节点不存在！", str2);
                            return list;
                        }
                        string str6 = node2.InnerXml;
                        if (ToolUtil.GetByteCount(str6) > this.qyzbm_maxlength)
                        {
                            errorTip = string.Format("zxbm节点内容错误！", str2);
                            return list;
                        }
                        if (node.SelectSingleNode("yhzcbs") == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中yhzcbs节点不存在！", str2);
                            return list;
                        }
                        string xsyh = node.SelectSingleNode("yhzcbs").InnerXml.ToString();
                        if (((xsyh != "") && (xsyh != "0")) && (xsyh != "1"))
                        {
                            errorTip = string.Format("yhzcbs节点内容不正确！", str2);
                            return list;
                        }
                        if (xsyh == "")
                        {
                            xsyh = "0";
                        }
                        node2 = node.SelectSingleNode("zzstsgl");
                        if (node2 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中zzstsgl节点不存在！", str2);
                            return list;
                        }
                        string yhzc = node2.InnerXml;
                        if (ToolUtil.GetByteCount(yhzc) > this.yhzc_maxlength)
                        {
                            errorTip = string.Format("zzstsgl节点内容错误！", str2);
                            return list;
                        }
                        node2 = node.SelectSingleNode("lslbs");
                        if (node2 == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中lslbs节点不存在！", str2);
                            return list;
                        }
                        string lslvbz = node2.InnerXml;
                        if ((((lslvbz != "0") && (lslvbz != "1")) && ((lslvbz != "2") && (lslvbz != "3"))) && (lslvbz != ""))
                        {
                            errorTip = string.Format("lslbs节点内容不正确！", str2);
                            return list;
                        }
                        if (node.SelectSingleNode("zzssl") == null)
                        {
                            errorTip = string.Format("XML文件格式错误，单据{0}中不存在zzssl节点！", str2);
                            continue;
                        }
                        this.lslbz2yhzc(node.SelectSingleNode("zzssl").InnerXml, ref lslvbz, ref yhzc, ref xsyh);
                        fpxx.zyspmc = flbm;
                        fpxx.zyspsm = str6 + "#%" + yhzc;
                        fpxx.skr = xsyh + "#%" + lslvbz;
                    }
                    else
                    {
                        fpxx.bmbbbh = "";
                        fpxx.zyspmc = "";
                        fpxx.zyspsm = "";
                        fpxx.skr = "";
                    }
                    if (node.SelectSingleNode("sfzhm") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在sfzhm节点！", str2);
                        return list;
                    }
                    fpxx.sfzhm = node.SelectSingleNode("sfzhm").InnerXml;
                    if (node.SelectSingleNode("gfdwsbh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在gfdwsbh节点！", str2);
                        return list;
                    }
                    fpxx.gfsh = node.SelectSingleNode("gfdwsbh").InnerXml;
                    if (node.SelectSingleNode("cllx") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在cllx节点！", str2);
                        return list;
                    }
                    fpxx.cllx = node.SelectSingleNode("cllx").InnerXml;
                    if (node.SelectSingleNode("cpxh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在cpxh节点！", str2);
                        return list;
                    }
                    fpxx.cpxh = node.SelectSingleNode("cpxh").InnerXml;
                    if (node.SelectSingleNode("cd") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在cd节点！", str2);
                        return list;
                    }
                    fpxx.cd = node.SelectSingleNode("cd").InnerXml;
                    if (node.SelectSingleNode("hgzh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在hgzh节点！", str2);
                        return list;
                    }
                    fpxx.hgzh = node.SelectSingleNode("hgzh").InnerXml;
                    if (node.SelectSingleNode("jkzmsh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在jkzmsh节点！", str2);
                        return list;
                    }
                    fpxx.jkzmsh = node.SelectSingleNode("jkzmsh").InnerXml;
                    if (node.SelectSingleNode("sjdh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在sjdh节点！", str2);
                        return list;
                    }
                    fpxx.sjdh = node.SelectSingleNode("sjdh").InnerXml;
                    if (node.SelectSingleNode("fdjhm") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在fdjhm节点！", str2);
                        return list;
                    }
                    fpxx.fdjhm = node.SelectSingleNode("fdjhm").InnerXml;
                    if (node.SelectSingleNode("clsbdh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在clsbdh节点！", str2);
                        return list;
                    }
                    fpxx.clsbdh = node.SelectSingleNode("clsbdh").InnerXml;
                    if (node.SelectSingleNode("dh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在dh节点！", str2);
                        return list;
                    }
                    fpxx.xfdh = node.SelectSingleNode("dh").InnerXml;
                    if (node.SelectSingleNode("zh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在zh节点！", str2);
                        return list;
                    }
                    fpxx.xfzh = node.SelectSingleNode("zh").InnerXml;
                    if (node.SelectSingleNode("dz") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在dz节点！", str2);
                        return list;
                    }
                    fpxx.xfdz = node.SelectSingleNode("dz").InnerXml;
                    if (node.SelectSingleNode("khyh") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在khyh节点！", str2);
                        return list;
                    }
                    fpxx.xfyh = node.SelectSingleNode("khyh").InnerXml;
                    if (node.SelectSingleNode("dw") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在dw节点！", str2);
                        return list;
                    }
                    fpxx.dw = node.SelectSingleNode("dw").InnerXml;
                    if (node.SelectSingleNode("xcrs") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在xcrs节点！", str2);
                        return list;
                    }
                    fpxx.xcrs = node.SelectSingleNode("xcrs").InnerXml;
                    if (node.SelectSingleNode("scqymc") == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在scqymc节点！", str2);
                        return list;
                    }
                    fpxx.sccjmc = node.SelectSingleNode("scqymc").InnerXml;
                    node2 = node.SelectSingleNode("bhsj");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在bhsj节点！", str2);
                        return list;
                    }
                    if (node2.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中bhsj值不能为空!", str2);
                        return list;
                    }
                    if (!decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中bhsj值格式不正确！", str2);
                        return list;
                    }
                    if (num == decimal.Zero)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中bhsj值不能为0！", str2);
                        return list;
                    }
                    fpxx.je = node.SelectSingleNode("bhsj").InnerXml.Trim();
                    node2 = node.SelectSingleNode("zzsse");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在zzsse节点！", str2);
                        return list;
                    }
                    if (node2.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中zzsse值不能为空!", str2);
                        return list;
                    }
                    if (!decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中zzsse值格式不正确！", str2);
                        return list;
                    }
                    fpxx.se = node.SelectSingleNode("zzsse").InnerXml.Trim();
                    node2 = node.SelectSingleNode("zzssl");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在zzssl节点！", str2);
                        return list;
                    }
                    if (node2.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中zzssl值不能为空!", str2);
                        return list;
                    }
                    if (!decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中zzssl值格式不正确！", str2);
                        return list;
                    }
                    fpxx.sLv = node.SelectSingleNode("zzssl").InnerXml.Trim();
                    if (double.TryParse(fpxx.sLv, out num2) && !allSlv.Contains(num2))
                    {
                        errorTip = string.Format("税率非法！", str2);
                        return list;
                    }
                    node2 = node.SelectSingleNode("jshj");
                    if (node2 == null)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中不存在jshj节点！", str2);
                        return list;
                    }
                    if (node2.InnerXml.Trim() == "")
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中jshj值不能为空!", str2);
                        return list;
                    }
                    if (!decimal.TryParse(node2.InnerText.Trim(), out num))
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中jshj值格式不正确！", str2);
                        return list;
                    }
                    if (num == decimal.Zero)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中jshj值不能为0！", str2);
                        return list;
                    }
                    if (decimal.Compare(decimal.Add(decimal.Parse(fpxx.je), decimal.Parse(fpxx.se)), num) != 0)
                    {
                        errorTip = string.Format("XML文件格式错误，单据{0}中bhsj与zzsse之和，不等于jshj值！", str2);
                        return list;
                    }
                    if (!string.IsNullOrEmpty(errorTip) || (errorTip != ""))
                    {
                        return list;
                    }
                    Djfp item = new Djfp(str2);
                    fpxx.xsdjbh = str2;
                    item.File = str;
                    item.Fpxx = fpxx;
                    list.Add(item);
                }
                return list;
            }
            catch (Exception exception)
            {
                errorTip = exception.Message;
            }
            return list;
        }

        public List<string> QueryYkdj(string files)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(files))
            {
                try
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("FILES", files);
                    ArrayList list2 = BaseDAOFactory.GetBaseDAOSQLite().querySQL("aisino.fwkp.fptk.QueryYkdj", dictionary);
                    if (list2.Count <= 0)
                    {
                        return list;
                    }
                    IEnumerator enumerator = list2.GetEnumerator();
                    {
                        while (enumerator.MoveNext())
                        {
                            string item = ((Dictionary<string, object>) enumerator.Current)["DJH"].ToString();
                            list.Add(item);
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.log.Error(exception.Message, exception);
                }
            }
            return list;
        }

        public string SetDecimals(string a, int decimals)
        {
            return decimal.Round(decimal.Parse(a), decimals, MidpointRounding.AwayFromZero).ToString("f" + decimals);
        }

        public string Subtract(string a, string b)
        {
            return decimal.Subtract(decimal.Parse(a), decimal.Parse(b)).ToString();
        }
    }
}

