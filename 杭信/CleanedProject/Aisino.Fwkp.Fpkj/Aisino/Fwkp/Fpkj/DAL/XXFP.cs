namespace Aisino.Fwkp.Fpkj.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.Form.FPXF;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Runtime.InteropServices;

    public class XXFP : DockForm
    {
        public IBaseDAO baseDao;
        private string code = string.Empty;
        private ILog loger = LogUtil.GetLogger<XXFP>();
        private Dictionary<string, object> Sharedict;

        public XXFP(bool isTransaction = false)
        {
            try
            {
                if (isTransaction)
                {
                    this.baseDao = BaseDAOFactory.GetBaseDAOSQLite(isTransaction);
                    this.baseDao.Open();
                }
                else
                {
                    this.baseDao = BaseDAOFactory.GetBaseDAOSQLite();
                }
                this.Sharedict = new Dictionary<string, object>();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        public bool Delete(string FPZL, string FPDM, int FPHM)
        {
            try
            {
                this.Sharedict.Clear();
                this.Sharedict.Add("FPZL", FPZL);
                this.Sharedict.Add("FPDM", FPDM);
                this.Sharedict.Add("FPHM", FPHM);
                return (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.fwkp.fpkj.DeleteXXFP", this.Sharedict) > 0);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return false;
            }
        }

        public bool DeleteAll(List<RepairReport.FPZJ> FpzjList)
        {
            try
            {
                if ((FpzjList == null) || (FpzjList.Count == 0))
                {
                    return false;
                }
                List<string> list = new List<string>();
                List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
                string item = "aisino.fwkp.fpkj.DeleteXXFP";
                string str2 = "aisino.fwkp.fpkj.DeleteXXFPMX_All";
                string str3 = "aisino.fwkp.fpkj.DeleteXXFPMXQD_All";
                IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                foreach (RepairReport.FPZJ fpzj in FpzjList)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("FPZL", fpzj.fpzl);
                    dictionary.Add("FPDM", fpzj.fpdm);
                    dictionary.Add("FPHM", fpzj.fphm);
                    list.Add(str3);
                    list2.Add(dictionary);
                    list.Add(str2);
                    list2.Add(dictionary);
                    list.Add(item);
                    list2.Add(dictionary);
                }
                baseDAOSQLite.未确认DAO方法1(list.ToArray(), list2);
                list2 = null;
                list = null;
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return false;
            }
        }

        public bool ExistDate_YearMonth(Dictionary<string, object> dictTemp)
        {
            try
            {
                ArrayList list = this.baseDao.querySQL("aisino.fwkp.fpkj.ExistDate_YearMonth", dictTemp);
                if (list == null)
                {
                    return false;
                }
                if (0 >= list.Count)
                {
                    list = null;
                    return false;
                }
                list = null;
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return false;
            }
        }

        private void FillSpxx(ArrayList list, List<Dictionary<SPXX, string>> SpList)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Dictionary<string, object> dictionary = list[i] as Dictionary<string, object>;
                Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                item.Add((SPXX)0, dictionary["SPMC"].ToString().Trim(new char[] { ' ' }));
                item.Add((SPXX)2, dictionary["SPSM"].ToString().Trim(new char[] { ' ' }));
                item.Add((SPXX)8, dictionary["SLV"].ToString().Trim(new char[] { ' ' }));
                item.Add((SPXX)10, dictionary["FPHXZ"].ToString().Trim(new char[] { ' ' }));
                item.Add((SPXX)7, dictionary["JE"].ToString().Trim(new char[] { ' ' }));
                item.Add((SPXX)9, dictionary["SE"].ToString().Trim(new char[] { ' ' }));
                item.Add((SPXX)3, dictionary["GGXH"].ToString().Trim(new char[] { ' ' }));
                item.Add((SPXX)4, dictionary["JLDW"].ToString().Trim(new char[] { ' ' }));
                item.Add((SPXX)6, dictionary["SL"].ToString().Trim(new char[] { ' ' }));
                string str = "";
                if (!(dictionary["DJ"] is DBNull))
                {
                    str = dictionary["DJ"].ToString().Trim(new char[] { ' ' });
                }
                item.Add((SPXX)5, str);
                item.Add((SPXX)11, Tool.ObjectToBool(dictionary["HSJBZ"]) ? "1" : "0");
                item.Add((SPXX)13, dictionary["FPMXXH"].ToString().Trim(new char[] { ' ' }));
                if (dictionary.ContainsKey("FLBM"))
                {
                    item.Add((SPXX)20, dictionary["FLBM"].ToString());
                    item.Add((SPXX)0x15, (dictionary["XSYH"].ToString() == "") ? null : dictionary["XSYH"].ToString());
                    item.Add((SPXX)0x16, dictionary["YHSM"].ToString());
                    item.Add((SPXX)0x17, dictionary["LSLVBS"].ToString());
                    item.Add((SPXX)1, dictionary["SPBH"].ToString());
                }
                else
                {
                    item.Add((SPXX)20, "");
                    item.Add((SPXX)0x15, null);
                    item.Add((SPXX)0x16, "");
                    item.Add((SPXX)0x17, "");
                    item.Add((SPXX)1, "");
                }
                item.Add((SPXX)0x18, "");
                SpList.Add(item);
            }
        }

        private Fpxx GetHWYSFpxx(Dictionary<string, object> data)
        {
            if (data != null)
            {
                Fpxx fpxx = new Fpxx(Invoice.ParseFPLX(data["FPZL"].ToString()), data["FPDM"].ToString(), ShareMethods.FPHMTo8Wei(data["FPHM"].ToString())) {
                    kprq = ToolUtil.FormatDateTimeEx(data["KPRQ"]),
                    cyrmc = data["XFMC"].ToString(),
                    cyrnsrsbh = data["XFSH"].ToString(),
                    mw = data["MW"].ToString(),
                    hxm = data["HXM"].ToString(),
                    spfmc = data["GFMC"].ToString(),
                    spfnsrsbh = data["GFSH"].ToString().Trim(),
                    shrmc = data["GFDZDH"].ToString(),
                    shrnsrsbh = data["CM"].ToString(),
                    fhrmc = data["XFDZDH"].ToString(),
                    fhrnsrsbh = data["TYDH"].ToString(),
                    qyd = data["XFYHZH"].ToString(),
                    yshwxx = data["YSHWXX"].ToString(),
                    Mxxx = new List<Dictionary<SPXX, string>>()
                };
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("FPZL", data["FPZL"].ToString());
                dictionary.Add("FPDM", fpxx.fpdm);
                dictionary.Add("FPHM", fpxx.fphm);
                ArrayList list = this.baseDao.querySQL("aisino.fwkp.fpkj.SelectFPMXUpDown", dictionary);
                this.FillSpxx(list, fpxx.Mxxx);
                bool flag = Tool.ObjectToBool(data["QDBZ"]);
                fpxx.Qdxx = null;
                if (flag)
                {
                    list.Clear();
                    list = this.baseDao.querySQL("aisino.fwkp.fpkj.SelectFPQDUpDown", dictionary);
                    fpxx.Qdxx = new List<Dictionary<SPXX, string>>();
                    this.FillSpxx(list, fpxx.Qdxx);
                }
                fpxx.je = Tool.ObjectToDecimal(data["HJJE"]).ToString("F2");
                fpxx.sLv = data["SLV"].ToString();
                fpxx.se = Tool.ObjectToDecimal(data["HJSE"]).ToString("F2");
                fpxx.kpjh = Tool.ObjectToInt(data["KPJH"]);
                fpxx.jqbh = data["JQBH"].ToString();
                fpxx.czch = data["QYD"].ToString();
                fpxx.ccdw = data["YYZZH"].ToString();
                fpxx.zgswjgmc = data["GFYHZH"].ToString();
                fpxx.zgswjgdm = data["HYBM"].ToString();
                fpxx.xsdjbh = data["XSDJBH"].ToString();
                fpxx.jmbbh = data["JMBBH"].ToString();
                fpxx.bsq = Tool.ObjectToInt(data["BSQ"]);
                fpxx.jym = data["JYM"].ToString();
                fpxx.isRed = Tool.ObjectToDouble(fpxx.je) < 0.0;
                fpxx.sLv = data["SLV"].ToString();
                if ((fpxx.fplx == 0) && (fpxx.sLv == "0.05"))
                {
                    fpxx.Zyfplx = (ZYFP_LX)1;
                }
                fpxx.se = Tool.ObjectToDecimal(data["HJSE"]).ToString("F2");
                fpxx.zyspmc = data["ZYSPMC"].ToString();
                fpxx.zyspsm = data["SPSM_BM"].ToString();
                fpxx.bz = data["BZ"].ToString();
                fpxx.kpr = data["KPR"].ToString();
                fpxx.fhr = data["FHR"].ToString();
                fpxx.skr = data["SKR"].ToString();
                fpxx.yysbz = data["YYSBZ"].ToString();
                fpxx.ssyf = Tool.ObjectToInt(data["SSYF"]);
                object obj2 = data["SBBZ"];
                if (obj2 != null)
                {
                    ulong.TryParse(obj2.ToString(), out fpxx.keyFlagNo);
                    if (data.ContainsKey("SYH"))
                    {
                        long.TryParse(data["SYH"].ToString(), out fpxx.invQryNo);
                    }
                }
                else
                {
                    fpxx.keyFlagNo = 0L;
                    fpxx.invQryNo = 0L;
                }
                fpxx.zfbz = Tool.ObjectToBool(data["ZFBZ"]);
                fpxx.bsbz = Tool.ObjectToBool(data["BSBZ"]);
                fpxx.dybz = Tool.ObjectToBool(data["DYBZ"]);
                fpxx.xfbz = Tool.ObjectToBool(data["XFBZ"]);
                fpxx.bszt = (data["BSZT"] is DBNull) ? -1 : Tool.ObjectToInt(data["BSZT"]);
                fpxx.sign = data["SIGN"].ToString();
                fpxx.zfsj = ToolUtil.FormatDateTimeEx(data["ZFRQ"].ToString());
                fpxx.redNum = data["HZTZDH"].ToString();
                this.GetYYSBZ(ref fpxx);
                string str = data["LZDMHM"].ToString();
                if ((str != null) && (str.Length > 0))
                {
                    int index = str.IndexOf('_');
                    if (index < 0)
                    {
                        index = 0;
                    }
                    fpxx.blueFpdm = str.Substring(0, index);
                    if ((index + 1) > str.Length)
                    {
                        index = str.Length - 1;
                    }
                    fpxx.blueFphm = str.Substring(index + 1);
                }
                else
                {
                    fpxx.blueFpdm = "";
                    fpxx.blueFphm = "";
                }
                fpxx.OtherFields = new Dictionary<string, object>();
                fpxx.OtherFields.Add("PZRQ", ToolUtil.FormatDateTimeEx(data["PZRQ"]));
                fpxx.OtherFields.Add("PZLB", data["PZLB"]);
                fpxx.OtherFields.Add("PZHM", data["PZHM"]);
                fpxx.OtherFields.Add("PZYWH", data["PZYWH"]);
                fpxx.OtherFields.Add("PZZT", data["PZZT"]);
                fpxx.OtherFields.Add("PZWLYWH", data["PZWLYWH"]);
                fpxx.OtherFields.Add("BSRZ", data["BSRZ"]);
                fpxx.kpsxh = data["KPSXH"].ToString();
                fpxx.dzsyh = data["DZSYH"].ToString();
                fpxx.bmbbbh = data["BMBBBH"].ToString();
                this.code = "0000";
                return fpxx;
            }
            this.code = "FPCX-000020";
            return null;
        }

        private _InvoiceType GetInvoiceType(FPLX type)
        {
            _InvoiceType type2;
            switch ((int)type)
            {
                case 0:
                    type2.dbfpzl = "s";
                    type2.Displayfpzl = "专用发票";
                    type2.taxCardfpzl = (InvoiceType)0;
                    return type2;

                case 2:
                    type2.dbfpzl = "c";
                    type2.Displayfpzl = "普通发票";
                    type2.taxCardfpzl = (InvoiceType)2;
                    return type2;

                case 11:
                    type2.dbfpzl = "f";
                    type2.Displayfpzl = "货物运输业增值税专用发票";
                    type2.taxCardfpzl = (InvoiceType)11;
                    return type2;

                case 12:
                    type2.dbfpzl = "j";
                    type2.Displayfpzl = "机动车销售统一发票";
                    type2.taxCardfpzl = (InvoiceType)12;
                    return type2;
            }
            type2.dbfpzl = "";
            type2.Displayfpzl = "";
            type2.taxCardfpzl = (InvoiceType)1;
            return type2;
        }

        private _InvoiceType GetInvoiceType(string type)
        {
            _InvoiceType type2;
            switch (type)
            {
                case "专用发票":
                    type2.dbfpzl = "s";
                    type2.Displayfpzl = "专用发票";
                    type2.taxCardfpzl = (InvoiceType)0;
                    return type2;

                case "普通发票":
                    type2.dbfpzl = "c";
                    type2.Displayfpzl = "普通发票";
                    type2.taxCardfpzl = (InvoiceType)2;
                    return type2;

                case "农产品销售发票":
                    type2.dbfpzl = "c";
                    type2.Displayfpzl = "普通发票";
                    type2.taxCardfpzl = (InvoiceType)2;
                    return type2;

                case "收购发票":
                    type2.dbfpzl = "c";
                    type2.Displayfpzl = "普通发票";
                    type2.taxCardfpzl = (InvoiceType)2;
                    return type2;

                case "机动车销售统一发票":
                    type2.dbfpzl = "j";
                    type2.Displayfpzl = "机动车销售统一发票";
                    type2.taxCardfpzl = (InvoiceType)12;
                    return type2;

                case "货物运输业增值税专用发票":
                    type2.dbfpzl = "f";
                    type2.Displayfpzl = "货物运输业增值税专用发票";
                    type2.taxCardfpzl = (InvoiceType)11;
                    return type2;
            }
            type2.dbfpzl = "";
            type2.Displayfpzl = "";
            type2.taxCardfpzl = (InvoiceType)1;
            return type2;
        }

        private Fpxx GetJDCFpxx(Dictionary<string, object> dict)
        {
            if (dict != null)
            {
                Fpxx fpxx = new Fpxx();
                fpxx = new Fpxx(Invoice.ParseFPLX(dict["FPZL"].ToString()), dict["FPDM"].ToString(), ShareMethods.FPHMTo8Wei(dict["FPHM"].ToString())) {
                    kprq = ToolUtil.FormatDateTimeEx(dict["KPRQ"]),
                    kpjh = Tool.ObjectToInt(dict["KPJH"]),
                    jqbh = dict["JQBH"].ToString(),
                    mw = dict["MW"].ToString(),
                    gfmc = dict["GFMC"].ToString(),
                    sfzhm = dict["XSBM"].ToString(),
                    gfsh = dict["GFSH"].ToString().Trim(),
                    cllx = dict["GFDZDH"].ToString(),
                    cpxh = dict["XFDZ"].ToString(),
                    cd = dict["KHYHMC"].ToString(),
                    hgzh = dict["CM"].ToString(),
                    jkzmsh = dict["TYDH"].ToString(),
                    sjdh = dict["QYD"].ToString(),
                    fdjhm = dict["ZHD"].ToString(),
                    clsbdh = dict["XHD"].ToString(),
                    sccjmc = dict["SCCJMC"].ToString(),
                    xfmc = dict["XFMC"].ToString(),
                    xfdh = dict["XFDH"].ToString(),
                    xfsh = dict["XFSH"].ToString(),
                    xfzh = dict["KHYHZH"].ToString(),
                    xfdz = dict["XFDZDH"].ToString(),
                    xfyh = dict["XFYHZH"].ToString(),
                    sLv = dict["SLV"].ToString(),
                    se = Tool.ObjectToDecimal(dict["HJSE"]).ToString("F2"),
                    zgswjgmc = dict["GFYHZH"].ToString(),
                    zgswjgdm = dict["HYBM"].ToString(),
                    dw = dict["YYZZH"].ToString(),
                    xcrs = dict["MDD"].ToString(),
                    bz = dict["BZ"].ToString(),
                    kpr = dict["KPR"].ToString(),
                    fhr = dict["FHR"].ToString(),
                    skr = dict["SKR"].ToString(),
                    xsdjbh = dict["XSDJBH"].ToString(),
                    jmbbh = dict["JMBBH"].ToString(),
                    bsq = Tool.ObjectToInt(dict["BSQ"]),
                    jym = dict["JYM"].ToString(),
                    zfbz = Tool.ObjectToBool(dict["ZFBZ"]),
                    bsbz = Tool.ObjectToBool(dict["BSBZ"]),
                    dybz = Tool.ObjectToBool(dict["DYBZ"]),
                    xfbz = Tool.ObjectToBool(dict["XFBZ"]),
                    redNum = dict["HZTZDH"].ToString(),
                    je = Tool.ObjectToDecimal(dict["HJJE"]).ToString("F2"),
                    isRed = Tool.ObjectToDouble(fpxx.je) < 0.0,
                    zyspmc = dict["ZYSPMC"].ToString(),
                    zyspsm = dict["SPSM_BM"].ToString(),
                    yysbz = dict["YYSBZ"].ToString(),
                    ssyf = Tool.ObjectToInt(dict["SSYF"])
                };
                object obj2 = dict["SBBZ"];
                if (obj2 != null)
                {
                    ulong.TryParse(obj2.ToString(), out fpxx.keyFlagNo);
                    if (dict.ContainsKey("SYH"))
                    {
                        long.TryParse(dict["SYH"].ToString(), out fpxx.invQryNo);
                    }
                }
                else
                {
                    fpxx.keyFlagNo = 0L;
                    fpxx.invQryNo = 0L;
                }
                fpxx.Qdxx = null;
                fpxx.Mxxx = null;
                fpxx.bszt = (dict["BSZT"] is DBNull) ? -1 : Tool.ObjectToInt(dict["BSZT"]);
                fpxx.sign = dict["SIGN"].ToString();
                fpxx.zfsj = ToolUtil.FormatDateTimeEx(dict["ZFRQ"].ToString());
                this.GetYYSBZ(ref fpxx);
                string str = dict["LZDMHM"].ToString();
                if ((str != null) && (str.Length > 0))
                {
                    int index = str.IndexOf('_');
                    if (index < 0)
                    {
                        index = 0;
                    }
                    fpxx.blueFpdm = str.Substring(0, index);
                    if ((index + 1) > str.Length)
                    {
                        index = str.Length - 1;
                    }
                    fpxx.blueFphm = str.Substring(index + 1);
                }
                else
                {
                    fpxx.blueFpdm = "";
                    fpxx.blueFphm = "";
                }
                fpxx.OtherFields = new Dictionary<string, object>();
                fpxx.OtherFields.Add("PZRQ", ToolUtil.FormatDateTimeEx(dict["PZRQ"]));
                fpxx.OtherFields.Add("PZLB", dict["PZLB"]);
                fpxx.OtherFields.Add("PZHM", dict["PZHM"]);
                fpxx.OtherFields.Add("PZYWH", dict["PZYWH"]);
                fpxx.OtherFields.Add("PZZT", dict["PZZT"]);
                fpxx.OtherFields.Add("PZWLYWH", dict["PZWLYWH"]);
                fpxx.OtherFields.Add("BSRZ", dict["BSRZ"]);
                fpxx.kpsxh = dict["KPSXH"].ToString();
                fpxx.dzsyh = dict["DZSYH"].ToString();
                fpxx.bmbbbh = dict["BMBBBH"].ToString();
                this.code = "0000";
                return fpxx;
            }
            this.code = "FPCX-000020";
            return null;
        }

        public Fpxx GetModel(string FPZL, string FPDM, int FPHM, string XSDJBH = "")
        {
            try
            {
                this.Sharedict.Clear();
                string str = "aisino.fwkp.fpkj.SelectXXFP_ZL_DM_HM";
                if ((XSDJBH == null) || (XSDJBH == ""))
                {
                    str = "aisino.fwkp.fpkj.SelectXXFP_ZL_DM_HM";
                    this.Sharedict.Add("FPZL", FPZL);
                    this.Sharedict.Add("FPDM", FPDM);
                    this.Sharedict.Add("FPHM", FPHM);
                }
                else
                {
                    this.Sharedict.Add("XSDJBH", XSDJBH);
                    str = "aisino.fwkp.fpkj.SelectXXFP_XSDJBH";
                }
                this.loger.Debug("###GetModel" + str);
                ArrayList list = this.baseDao.querySQL(str, this.Sharedict);
                Fpxx xxfp = null;
                if (list.Count > 0)
                {
                    this.Sharedict.Clear();
                    this.Sharedict = list[0] as Dictionary<string, object>;
                    xxfp = this.GetXxfp(this.Sharedict);
                    list = null;
                    return xxfp;
                }
                return null;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return null;
            }
        }

        private Fpxx GetPTZYFpxx(Dictionary<string, object> dict)
        {
            if (dict != null)
            {
                Fpxx fpxx = new Fpxx();
                fpxx = new Fpxx(Invoice.ParseFPLX(dict["FPZL"].ToString()), dict["FPDM"].ToString(), ShareMethods.FPHMTo8Wei(dict["FPHM"].ToString())) {
                    kprq = ToolUtil.FormatDateTimeEx(dict["KPRQ"]),
                    xsdjbh = dict["XSDJBH"].ToString(),
                    kpjh = Tool.ObjectToInt(dict["KPJH"]),
                    jmbbh = dict["JMBBH"].ToString(),
                    bsq = Tool.ObjectToInt(dict["BSQ"]),
                    jym = dict["JYM"].ToString(),
                    zfbz = Tool.ObjectToBool(dict["ZFBZ"]),
                    bsbz = Tool.ObjectToBool(dict["BSBZ"]),
                    dybz = Tool.ObjectToBool(dict["DYBZ"]),
                    xfbz = Tool.ObjectToBool(dict["XFBZ"]),
                    mw = dict["MW"].ToString(),
                    hxm = dict["HXM"].ToString(),
                    hzfw = fpxx.hxm.Length > 0,
                    redNum = dict["HZTZDH"].ToString(),
                    gfmc = dict["GFMC"].ToString(),
                    gfsh = dict["GFSH"].ToString().Trim(),
                    gfdzdh = dict["GFDZDH"].ToString(),
                    gfyhzh = dict["GFYHZH"].ToString(),
                    xfmc = dict["XFMC"].ToString(),
                    xfsh = dict["XFSH"].ToString(),
                    xfdzdh = dict["XFDZDH"].ToString(),
                    xfyhzh = dict["XFYHZH"].ToString(),
                    je = Tool.ObjectToDecimal(dict["HJJE"]).ToString("F2"),
                    isRed = Tool.ObjectToDouble(fpxx.je) < 0.0,
                    sLv = dict["SLV"].ToString(),
                    yysbz = dict["YYSBZ"].ToString()
                };
                if (((fpxx.fplx == 0) && (Tool.ObjectToDouble(fpxx.sLv) == 0.05)) && (fpxx.yysbz.Substring(8, 1) == "0"))
                {
                    fpxx.Zyfplx = (ZYFP_LX)1;
                }
                if (((int)fpxx.fplx == 0) || ((int)fpxx.fplx == 2))
                {
                    if (fpxx.sLv == "0.015")
                    {
                        fpxx.Zyfplx = (ZYFP_LX)10;
                    }
                    if (fpxx.yysbz.Substring(8, 1) == "2")
                    {
                        fpxx.Zyfplx = (ZYFP_LX)11;
                    }
                }
                fpxx.se = Tool.ObjectToDecimal(dict["HJSE"]).ToString("F2");
                fpxx.zyspmc = dict["ZYSPMC"].ToString();
                fpxx.zyspsm = dict["SPSM_BM"].ToString();
                fpxx.bz = dict["BZ"].ToString();
                fpxx.kpr = dict["KPR"].ToString();
                fpxx.fhr = dict["FHR"].ToString();
                fpxx.skr = dict["SKR"].ToString();
                fpxx.ssyf = Tool.ObjectToInt(dict["SSYF"]);
                object obj2 = dict["SBBZ"];
                if (obj2 != null)
                {
                    ulong.TryParse(obj2.ToString(), out fpxx.keyFlagNo);
                    if (dict.ContainsKey("SYH"))
                    {
                        long.TryParse(dict["SYH"].ToString(), out fpxx.invQryNo);
                    }
                }
                else
                {
                    fpxx.keyFlagNo = 0L;
                    fpxx.invQryNo = 0L;
                }
                bool flag = Tool.ObjectToBool(dict["QDBZ"]);
                fpxx.Qdxx = null;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("FPZL", dict["FPZL"].ToString());
                dictionary.Add("FPDM", fpxx.fpdm);
                dictionary.Add("FPHM", fpxx.fphm);
                ArrayList list = this.baseDao.querySQL("aisino.fwkp.fpkj.SelectFPMXUpDown", dictionary);
                if ((list == null) || (list.Count == 0))
                {
                    fpxx.Mxxx = null;
                }
                else
                {
                    fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                    this.FillSpxx(list, fpxx.Mxxx);
                }
                if (flag)
                {
                    list.Clear();
                    list = this.baseDao.querySQL("aisino.fwkp.fpkj.SelectFPQDUpDown", dictionary);
                    fpxx.Qdxx = new List<Dictionary<SPXX, string>>();
                    this.FillSpxx(list, fpxx.Qdxx);
                }
                fpxx.bszt = (dict["BSZT"] is DBNull) ? -1 : Tool.ObjectToInt(dict["BSZT"]);
                fpxx.sign = dict["SIGN"].ToString();
                fpxx.zfsj = ToolUtil.FormatDateTimeEx(dict["ZFRQ"].ToString());
                this.GetYYSBZ(ref fpxx);
                string str = dict["LZDMHM"].ToString();
                if ((str != null) && (str.Length > 0))
                {
                    int index = str.IndexOf('_');
                    if (index < 0)
                    {
                        index = 0;
                    }
                    fpxx.blueFpdm = str.Substring(0, index);
                    if ((index + 1) > str.Length)
                    {
                        index = str.Length - 1;
                    }
                    fpxx.blueFphm = str.Substring(index + 1);
                }
                else
                {
                    fpxx.blueFpdm = "";
                    fpxx.blueFphm = "";
                }
                fpxx.OtherFields = new Dictionary<string, object>();
                fpxx.OtherFields.Add("PZRQ", ToolUtil.FormatDateTimeEx(dict["PZRQ"]));
                fpxx.OtherFields.Add("PZLB", dict["PZLB"]);
                fpxx.OtherFields.Add("PZHM", dict["PZHM"]);
                fpxx.OtherFields.Add("PZYWH", dict["PZYWH"]);
                fpxx.OtherFields.Add("PZZT", dict["PZZT"]);
                fpxx.OtherFields.Add("PZWLYWH", dict["PZWLYWH"]);
                fpxx.OtherFields.Add("BSRZ", dict["BSRZ"]);
                fpxx.kpsxh = dict["KPSXH"].ToString();
                fpxx.dzsyh = dict["DZSYH"].ToString();
                fpxx.jqbh = dict["JQBH"].ToString();
                fpxx.bmbbbh = dict["BMBBBH"].ToString();
                if ((int)fpxx.fplx == 0x29)
                {
                    fpxx.dy_mb = Tool.getDymb(fpxx.yysbz);
                }
                this.code = "0000";
                return fpxx;
            }
            this.code = "FPCX-000020";
            return null;
        }

        public Fpxx GetXxfp(Dictionary<string, object> dict)
        {
            Fpxx fpxx;
            try
            {
                if (dict == null)
                {
                    return null;
                }
                FPLX fplx2 = Invoice.ParseFPLX(dict["FPZL"].ToString());
                if ((int)fplx2 <= 12)
                {
                    switch ((int)fplx2)
                    {
                        case 0:
                        case 2:
                            goto Label_0056;

                        case 11:
                            return this.GetHWYSFpxx(dict);

                        case 12:
                            return this.GetJDCFpxx(dict);
                    }
                    goto Label_0074;
                }
                if (((int)fplx2 != 0x29) && ((int)fplx2 != 0x33))
                {
                    goto Label_0074;
                }
            Label_0056:
                return this.GetPTZYFpxx(dict);
            Label_0074:
                fpxx = null;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                fpxx = null;
            }
            return fpxx;
        }

        private void GetYYSBZ(ref Fpxx fpxx)
        {
            if ((fpxx.yysbz != null) && (fpxx.yysbz.Length >= 10))
            {
                switch (fpxx.yysbz[2])
                {
                    case '1':
                        fpxx.Zyfplx = (ZYFP_LX)6;
                        break;

                    case '2':
                        fpxx.Zyfplx = (ZYFP_LX)7;
                        break;

                    case '3':
                        fpxx.Zyfplx = (ZYFP_LX)2;
                        break;
                }
                char ch2 = fpxx.yysbz[4];
                if (ch2 == '1')
                {
                    fpxx.isNewJdcfp = false;
                }
                else
                {
                    fpxx.isNewJdcfp = true;
                }
                switch (fpxx.yysbz[5])
                {
                    case '1':
                        fpxx.Zyfplx = (ZYFP_LX)8;
                        break;

                    case '2':
                        fpxx.Zyfplx = (ZYFP_LX)9;
                        break;
                }
                if ((Tool.ObjectToDouble(fpxx.je) == 0.0) && fpxx.zfbz)
                {
                    fpxx.isBlankWaste = true;
                }
                else
                {
                    fpxx.isBlankWaste = false;
                }
            }
        }

        public int IsExistHZFP(Dictionary<string, object> condition)
        {
            try
            {
                string str = "aisino.fwkp.fpkj.IsExistHZFP";
                ArrayList list = this.baseDao.querySQL(str, condition);
                if ((list == null) || (list.Count == 0))
                {
                    list = null;
                    return 0;
                }
                Dictionary<string, object> dictionary = list[0] as Dictionary<string, object>;
                return Tool.ObjectToInt(dictionary["NUM"]);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return 0;
            }
        }

        public bool SaveXxfp(List<Fpxx> fpList)
        {
            try
            {
                if ((fpList != null) && (fpList.Count != 0))
                {
                    List<string> sqls = new List<string>();
                    List<Dictionary<string, object>> paramAll = new List<Dictionary<string, object>>();
                    string sqlID = "aisino.fwkp.fpkj.ADDFPQD";
                    string str2 = "aisino.fwkp.fpkj.ADDFPMX";
                    IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                    foreach (Fpxx fpxx in fpList)
                    {
                        List<Dictionary<SPXX, string>> list3;
                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                        dictionary.Add("FPZL", Tool.PareFpType(fpxx.fplx));
                        dictionary.Add("FPDM", fpxx.fpdm);
                        dictionary.Add("FPHM", Tool.ObjectToInt(fpxx.fphm));
                        FPLX fplx = fpxx.fplx;
                        if ((int)fplx <= 12)
                        {
                            switch ((int)fplx)
                            {
                                case 0:
                                case 2:
                                    goto Label_00D9;

                                case 11:
                                    sqls.Add("aisino.fwkp.fpkj.AddXXFP_HWYS_JDC");
                                    paramAll.Add(this.SetHWYSFpxxParam(fpxx));
                                    break;

                                case 12:
                                    sqls.Add("aisino.fwkp.fpkj.AddXXFP_HWYS_JDC");
                                    paramAll.Add(this.SetJDCFpxxParam(fpxx));
                                    break;
                            }
                            goto Label_0128;
                        }
                        if (((int)fplx != 0x29) && ((int)fplx != 0x33))
                        {
                            goto Label_0128;
                        }
                    Label_00D9:
                        sqls.Add("aisino.fwkp.fpkj.AddXXFP_PT");
                        paramAll.Add(this.SetPTZYFpxxParam(fpxx));
                    Label_0128:
                        list3 = fpxx.Qdxx;
                        List<Dictionary<SPXX, string>> mxxx = fpxx.Mxxx;
                        if ((mxxx != null) && (mxxx.Count > 0))
                        {
                            this.SetSpxxParam(str2, sqls, paramAll, fpxx, mxxx);
                        }
                        if ((list3 != null) && (list3.Count > 0))
                        {
                            this.SetSpxxParam(sqlID, sqls, paramAll, fpxx, list3);
                        }
                    }
                    int num = baseDAOSQLite.updateSQLTransaction(sqls.ToArray(), paramAll);
                    if (num > 0)
                    {
                        this.loger.Error("成功保存到DB,发票数目为：" + num);
                        return true;
                    }
                    this.loger.Error("保存DB失败");
                }
                return false;
            }
            catch (Exception exception)
            {
                if (fpList != null)
                {
                    fpList.Clear();
                }
                this.loger.Error(exception.ToString());
                return false;
            }
        }

        public ArrayList SelectAllDate_XXFP(int Month, int year)
        {
            ArrayList list = null;
            try
            {
                this.Sharedict.Clear();
                string str = ToolUtil.FormatDateTimeEx(new DateTime(year, Month, 1).ToString("yyyy-MM-dd"));
                this.Sharedict.Add("Year", str);
                this.Sharedict.Add("Month", Month);
                list = this.baseDao.querySQL("aisino.fwkp.fpkj.SelectXXFPByMonth", this.Sharedict);
                this.loger.Info("冗余性发票数目：" + list.Count);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return list;
            }
            return list;
        }

        public AisinoDataSet SelectData(string FPHM, string FPDM, string GFSH)
        {
            try
            {
                return null;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return null;
            }
        }

        public Hashtable SelectFPListFromKprq(Dictionary<string, object> condition)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Clear();
            try
            {
                string str = "aisino.fwkp.fpkj.SelectFPListFromKprq";
                ArrayList list = this.baseDao.querySQL(str, condition);
                if ((list != null) && (list.Count > 0))
                {
                    int num = 0;
                    int count = list.Count;
                    for (num = 0; num < count; num++)
                    {
                        Dictionary<string, object> dict = list[num] as Dictionary<string, object>;
                        Fpxx xxfp = this.GetXxfp(dict);
                        if (xxfp != null)
                        {
                            string key = xxfp.fpdm + "_" + Tool.ObjectToInt(xxfp.fphm);
                            if (!hashtable.ContainsKey(key))
                            {
                                hashtable.Add(key, xxfp);
                            }
                            else
                            {
                                this.loger.Error("[SelectFPListFromKprq函数异常]：发票主键重复: " + key);
                            }
                        }
                    }
                }
                list = null;
                return hashtable;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return null;
            }
        }

        public List<Fpxx> SelectFpxx_FPDaoChu(Dictionary<string, object> condition)
        {
            List<Fpxx> list = new List<Fpxx>();
            list.Clear();
            string str = "aisino.fwkp.fpkj.SelectFPDaoChu";
            ArrayList list2 = this.baseDao.querySQL(str, condition);
            if ((list2 != null) && (list2.Count > 0))
            {
                int num = 0;
                int count = list2.Count;
                for (num = 0; num < count; num++)
                {
                    Dictionary<string, object> dict = list2[num] as Dictionary<string, object>;
                    Fpxx xxfp = this.GetXxfp(dict);
                    if (xxfp != null)
                    {
                        list.Add(xxfp);
                    }
                }
            }
            return list;
        }

        public List<Fpxx> SelectFpxx_FPZJDaoChu(Dictionary<string, object> condition)
        {
            try
            {
                List<Fpxx> list = new List<Fpxx>();
                list.Clear();
                string str = "aisino.fwkp.fpkj.SelectFPZJDataDaoChu";
                ArrayList list2 = this.baseDao.querySQL(str, condition);
                if ((list2 != null) && (list2.Count > 0))
                {
                    int num = 0;
                    int count = list2.Count;
                    string str2 = condition["BZ"].ToString();
                    string str3 = "";
                    bool flag = str2 != "";
                    for (num = 0; num < count; num++)
                    {
                        Dictionary<string, object> dict = list2[num] as Dictionary<string, object>;
                        if ((dict["BZ"] == null) || (dict["BZ"].ToString().Length == 0))
                        {
                            str3 = "";
                        }
                        else
                        {
                            str3 = ToolUtil.GetString(Convert.FromBase64String(dict["BZ"].ToString()));
                        }
                        if (!flag || str3.Contains(str2))
                        {
                            Fpxx xxfp = this.GetXxfp(dict);
                            if (xxfp != null)
                            {
                                list.Add(xxfp);
                            }
                        }
                    }
                }
                list2 = null;
                return list;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return null;
            }
        }

        public List<Fpxx> SelectFpxxPage(Dictionary<string, object> condition)
        {
            try
            {
                List<Fpxx> list = new List<Fpxx>();
                list.Clear();
                string str = "aisino.fwkp.fpkj.SelectXXFPUpDownPage";
                ArrayList list2 = this.baseDao.querySQL(str, condition);
                if ((list2 != null) && (list2.Count > 0))
                {
                    int num = 0;
                    int count = list2.Count;
                    for (num = 0; num < count; num++)
                    {
                        Dictionary<string, object> dict = list2[num] as Dictionary<string, object>;
                        Fpxx xxfp = this.GetXxfp(dict);
                        if (xxfp != null)
                        {
                            list.Add(xxfp);
                        }
                    }
                }
                list2 = null;
                return list;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return null;
            }
        }

        public List<Fpxx> SelectFpxxPatch(string fps)
        {
            try
            {
                List<Fpxx> list = new List<Fpxx>();
                list.Clear();
                string[] strArray = fps.Split(new char[] { ';' });
                string str = "aisino.fwkp.fpkj.SelectXXFPUpDownPatch";
                foreach (string str2 in strArray)
                {
                    string[] strArray2 = str2.Split(new char[] { ',' });
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    if ((strArray2 != null) && (strArray2.Length >= 3))
                    {
                        dictionary.Add("FPZL", strArray2[2]);
                        dictionary.Add("FPDM", strArray2[0]);
                        dictionary.Add("FPHM", Tool.ObjectToInt(strArray2[1]));
                        ArrayList list2 = this.baseDao.querySQL(str, dictionary);
                        if ((list2 != null) && (list2.Count > 0))
                        {
                            Dictionary<string, object> dict = list2[0] as Dictionary<string, object>;
                            Fpxx xxfp = this.GetXxfp(dict);
                            if (xxfp != null)
                            {
                                list.Add(xxfp);
                            }
                        }
                    }
                    else
                    {
                        this.loger.Error("[SelectFpxxPatch异常]" + strArray2);
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return null;
            }
        }

        public string SelectFPZLListFromDB(Dictionary<string, object> condition)
        {
            try
            {
                string str = "aisino.fwkp.fpkj.SelectXXFPMFPLBList";
                ArrayList list = this.baseDao.querySQL(str, condition);
                string str2 = "";
                if ((list != null) && (list.Count > 0))
                {
                    int num = 0;
                    int count = list.Count;
                    for (num = 0; num < count; num++)
                    {
                        Dictionary<string, object> dictionary = list[num] as Dictionary<string, object>;
                        str2 = str2 + dictionary["FPZL"].ToString();
                    }
                }
                list = null;
                return str2;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return null;
            }
        }

        public string SelectMinKprq(Dictionary<string, object> condition)
        {
            try
            {
                string str = "aisino.fwkp.fpkj.SelectXXFPMinKPRQ";
                this.loger.Info("开始读取最小日期");
                ArrayList list = this.baseDao.querySQL(str, condition);
                this.loger.Info("结束读取最小日期");
                string str2 = "205012";
                if ((list != null) && (list.Count > 0))
                {
                    Dictionary<string, object> dictionary = list[0] as Dictionary<string, object>;
                    str2 = dictionary["MinKprq"].ToString();
                    list = null;
                    return str2;
                }
                list = null;
                return str2;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return null;
            }
        }

        public AisinoDataSet SelectPage(int page, int num, Dictionary<string, object> dict)
        {
            AisinoDataSet set = null;
            try
            {
                string str = "aisino.fwkp.fpkj.SelectFPZJDaoChu";
                set = this.baseDao.querySQLDataSet(str, dict, num, page);
                if (((set == null) || (set.Data == null)) || (set.Data.Rows.Count <= 0))
                {
                    return set;
                }
                string str2 = dict["BZ"].ToString();
                string str3 = "";
                bool flag = str2 != "";
                int count = set.Data.Rows.Count;
                List<DataRow> list = new List<DataRow>();
                for (int i = 0; i < count; i++)
                {
                    DataRow item = set.Data.Rows[i];
                    if ((item["BZ"] == null) || (item["BZ"].ToString().Length == 0))
                    {
                        str3 = "";
                    }
                    else
                    {
                        str3 = ToolUtil.GetString(Convert.FromBase64String(item["BZ"].ToString()));
                    }
                    if (flag && !str3.Contains(str2))
                    {
                        list.Add(item);
                    }
                }
                if (list.Count > 0)
                {
                    foreach (DataRow row2 in list)
                    {
                        set.Data.Rows.Remove(row2);
                    }
                }
                list.Clear();
                list = null;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return set;
            }
            return set;
        }

        public AisinoDataSet SelectPage(int page, int num, int TiaojianChaXun, Dictionary<string, object> dict, int SortWay, DateTime dataYearCard, int type = -1, int sqlType = 0)
        {
            AisinoDataSet set = null;
            try
            {
                string str = string.Empty;
                switch (TiaojianChaXun)
                {
                    case 0:
                        if (SortWay != 1)
                        {
                            break;
                        }
                        str = "aisino.fwkp.fpkj.SelectFPXX_TiaoJianSort";
                        goto Label_00C1;

                    case 1:
                        str = "aisino.fwkp.fpkj.SelectFPXX_FPFZ";
                        goto Label_00C1;

                    case 2:
                        if (SortWay != 1)
                        {
                            goto Label_0081;
                        }
                        str = "aisino.fwkp.fpkj.SelectFPXX_YiKaiFaPiaoZuoFeiSort";
                        goto Label_00C1;

                    default:
                        goto Label_00AE;
                }
                switch (sqlType)
                {
                    case 0:
                        str = "aisino.fwkp.fpkj.SelectFPXX_TiaoJian";
                        goto Label_00C1;

                    case 1:
                        str = "aisino.fwkp.fpkj.SelectFPXX_TiaoJian_Date";
                        goto Label_00C1;

                    case 2:
                        str = "aisino.fwkp.fpkj.SelectFPXX_TiaoJian_QuickSearch";
                        goto Label_00C1;

                    default:
                        str = "aisino.fwkp.fpkj.SelectFPXX_TiaoJian";
                        goto Label_00C1;
                }
            Label_0081:
                switch (sqlType)
                {
                    case 0:
                        str = "aisino.fwkp.fpkj.SelectFPXX_YiKaiFaPiaoZuoFei";
                        goto Label_00C1;

                    case 1:
                        str = "aisino.fwkp.fpkj.SelectFPXX_YiKaiFaPiaoZuoFei_Date";
                        goto Label_00C1;

                    default:
                        str = "aisino.fwkp.fpkj.SelectFPXX_YiKaiFaPiaoZuoFei";
                        goto Label_00C1;
                }
            Label_00AE:
                MessageManager.ShowMsgBox("FPCX-000019");
                return set;
            Label_00C1:
                this.loger.Info("开始发票查询....");
                set = this.baseDao.querySQLDataSetExtend(str, dict, num, page, type);
                this.loger.Info("结束发票查询....");
                if (((set != null) && (set.Data != null)) && (set.Data.Rows.Count > 0))
                {
                    this.loger.Info("查询后处理开始");
                    DataView defaultView = set.Data.DefaultView;
                    defaultView.Sort = "KPRQ desc";
                    set.Data = defaultView.ToTable();
                    foreach (DataRow row in set.Data.Rows)
                    {
                        if (row["BZ"] != null)
                        {
                            row["BZ"] = Tool.FromBase64(row["BZ"].ToString());
                        }
                        if (row["YSHWXX"] != null)
                        {
                            row["YSHWXX"] = Tool.FromBase64(row["YSHWXX"].ToString());
                        }
                        if (row["SLV"].ToString() == "")
                        {
                            row["SLV"] = "多税率";
                        }
                        if (((row["SLV"] != null) && (row["SLV"].ToString() != "")) && (row["SLV"].ToString() != "多税率"))
                        {
                            row["SLV"] = ((Tool.ObjectToDouble(row["SLV"].ToString()) * 100.0)).ToString() + "%";
                        }
                        if (row["SLV"].ToString() == "1.5%")
                        {
                            row["SLV"] = "减按1.5%计算";
                        }
                    }
                    this.loger.Info("查询后处理结束");
                }
                return set;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return set;
            }
            return set;
        }

        public DateTime SelectXXFP_MinKPRQ()
        {
            try
            {
                this.Sharedict.Clear();
                ArrayList list = this.baseDao.querySQL("aisino.fwkp.fpkj.SelectXXFP_MinKPRQ", null);
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        this.Sharedict.Clear();
                        this.Sharedict = list[0] as Dictionary<string, object>;
                        string str = Convert.ToString(this.Sharedict["KPRQ"]);
                        if (string.IsNullOrEmpty(str))
                        {
                            return DingYiZhiFuChuan1.dataTimeCCRQ;
                        }
                        if (10 > str.Length)
                        {
                            return DingYiZhiFuChuan1.dataTimeCCRQ;
                        }
                        if (10 < str.Length)
                        {
                            str = str.Substring(0, 10);
                        }
                        DateTime time = DateTime.ParseExact(str, DingYiZhiFuChuan1.strYear_Month_Day_Formart, CultureInfo.CurrentCulture);
                        list = null;
                        return time;
                    }
                    list = null;
                }
                return DingYiZhiFuChuan1.dataTimeCCRQ;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return DingYiZhiFuChuan1.dataTimeCCRQ;
            }
        }

        public int SelectYiKaiZuoFeiFpCount(Dictionary<string, object> condition)
        {
            try
            {
                string str = "aisino.fwkp.fpkj.SelectFPXX_YiKaiFaPiaoZuoFeiCount";
                if (DingYiZhiFuChuan1._UserMsg.IsAdmin)
                {
                    condition.Add("AdminBz", 1);
                }
                else
                {
                    condition.Add("AdminBz", 0);
                }
                condition.Add("Admin", DingYiZhiFuChuan1._UserMsg.MC);
                ArrayList list = this.baseDao.querySQL(str, condition);
                if ((list == null) || (list.Count == 0))
                {
                    list = null;
                    return 0;
                }
                Dictionary<string, object> dictionary = list[0] as Dictionary<string, object>;
                list = null;
                return Tool.ObjectToInt(dictionary["NUM"]);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return 0;
            }
        }

        private Dictionary<string, object> SetHWYSFpxxParam(Fpxx fp)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", Invoice.FPLX2Str(fp.fplx));
            dictionary.Add("FPDM", fp.fpdm);
            dictionary.Add("FPHM", Tool.ObjectToInt(fp.fphm));
            dictionary.Add("XSDJBH", fp.xsdjbh);
            dictionary.Add("KPRQ", Tool.ObjectToDateTime(ToolUtil.FormatDateTimeEx(fp.kprq)));
            dictionary.Add("XFMC", fp.cyrmc);
            dictionary.Add("XFSH", fp.cyrnsrsbh);
            dictionary.Add("GFMC", fp.spfmc);
            dictionary.Add("GFSH", fp.spfnsrsbh);
            dictionary.Add("JMBBH", fp.jmbbh);
            dictionary.Add("MW", fp.mw);
            dictionary.Add("HXM", "");
            dictionary.Add("GFDZDH", fp.shrmc);
            dictionary.Add("CM", fp.shrnsrsbh);
            dictionary.Add("XFDZDH", fp.fhrmc);
            dictionary.Add("TYDH", fp.fhrnsrsbh);
            dictionary.Add("XFYHZH", fp.qyd);
            dictionary.Add("YSHWXX", fp.yshwxx);
            dictionary.Add("HJJE", Tool.ObjectToDecimal(fp.je));
            if ((fp.sLv == "") || (fp.sLv == "NULL"))
            {
                dictionary.Add("SLV", null);
            }
            else
            {
                dictionary.Add("SLV", Tool.ObjectToDouble(fp.sLv));
            }
            dictionary.Add("HJSE", Tool.ObjectToDecimal(fp.se));
            dictionary.Add("KPJH", fp.kpjh);
            dictionary.Add("JQBH", fp.jqbh);
            dictionary.Add("QYD", fp.czch);
            dictionary.Add("YYZZH", fp.ccdw);
            dictionary.Add("HYBM", fp.zgswjgdm);
            dictionary.Add("GFYHZH", fp.zgswjgmc);
            dictionary.Add("BZ", fp.bz);
            dictionary.Add("KPR", fp.kpr);
            dictionary.Add("SKR", fp.skr);
            dictionary.Add("FHR", fp.fhr);
            dictionary.Add("ZYSPMC", (fp.zyspmc == null) ? "" : fp.zyspmc);
            dictionary.Add("SPSM_BM", fp.zyspsm);
            dictionary.Add("DYBZ", fp.dybz ? 1 : 0);
            dictionary.Add("QDBZ", (fp.Qdxx != null) ? 1 : 0);
            dictionary.Add("ZFBZ", fp.zfbz ? 1 : 0);
            dictionary.Add("BSBZ", fp.bsbz ? 1 : 0);
            dictionary.Add("XFBZ", fp.xfbz ? 1 : 0);
            dictionary.Add("JYM", fp.jym);
            dictionary.Add("BSQ", fp.bsq);
            dictionary.Add("SSYF", fp.ssyf);
            dictionary.Add("HZTZDH", fp.redNum);
            dictionary.Add("YYSBZ", fp.yysbz);
            if (fp.keyFlagNo == 0L)
            {
                dictionary.Add("SBBZ", "");
                dictionary.Add("SYH", "");
            }
            else
            {
                dictionary.Add("SBBZ", fp.keyFlagNo.ToString());
                dictionary.Add("SYH", fp.invQryNo.ToString());
            }
            dictionary.Add("KHYHMC", "");
            dictionary.Add("KHYHZH", "");
            dictionary.Add("ZHD", "");
            dictionary.Add("XHD", "");
            dictionary.Add("MDD", "");
            dictionary.Add("XFDZ", "");
            dictionary.Add("XFDH", "");
            dictionary.Add("SCCJMC", "");
            dictionary.Add("XSBM", fp.sfzhm);
            dictionary.Add("BSZT", fp.bszt);
            dictionary.Add("SIGN", (fp.sign == null) ? "" : fp.sign);
            dictionary.Add("ZFRQ", ToolUtil.FormatDateTimeEx(fp.zfsj));
            dictionary.Add("WSPZHM", "");
            string str = "";
            if (((fp.isRed && (fp.blueFpdm != null)) && ((fp.blueFphm != null) && (fp.blueFpdm.Length > 0))) && (fp.blueFphm.Length > 0))
            {
                str = fp.blueFpdm + "_" + ShareMethods.FPHMTo8Wei(fp.blueFphm);
            }
            dictionary.Add("LZDMHM", str);
            if (fp.OtherFields != null)
            {
                dictionary.Add("PZRQ", Tool.ObjectToDateTime(ToolUtil.FormatDateTimeEx(fp.OtherFields["PZRQ"])));
                dictionary.Add("PZLB", fp.OtherFields["PZLB"]);
                dictionary.Add("PZHM", Tool.ObjectToInt(fp.OtherFields["PZHM"]));
                dictionary.Add("PZYWH", fp.OtherFields["PZYWH"]);
                dictionary.Add("PZZT", Tool.ObjectToInt(fp.OtherFields["PZZT"]));
                dictionary.Add("PZWLYWH", fp.OtherFields["PZWLYWH"]);
                dictionary.Add("BSRZ", fp.OtherFields["BSRZ"]);
            }
            else
            {
                dictionary.Add("PZRQ", Tool.ObjectToDateTime(ToolUtil.FormatDateTimeEx("1899-12-30 23:59:59")));
                dictionary.Add("PZLB", "");
                dictionary.Add("PZHM", -1);
                dictionary.Add("PZYWH", "");
                dictionary.Add("PZZT", -1);
                dictionary.Add("PZWLYWH", "");
                dictionary.Add("BSRZ", "");
            }
            dictionary.Add("DZSYH", fp.dzsyh);
            dictionary.Add("KPSXH", fp.kpsxh);
            dictionary.Add("BMBBBH", fp.bmbbbh);
            return dictionary;
        }

        private Dictionary<string, object> SetJDCFpxxParam(Fpxx fp)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", Invoice.FPLX2Str(fp.fplx));
            dictionary.Add("FPDM", fp.fpdm);
            dictionary.Add("FPHM", Tool.ObjectToInt(fp.fphm));
            dictionary.Add("KPRQ", Tool.ObjectToDateTime(ToolUtil.FormatDateTimeEx(fp.kprq)));
            dictionary.Add("KPJH", fp.kpjh);
            dictionary.Add("JQBH", fp.jqbh);
            dictionary.Add("XSDJBH", fp.xsdjbh);
            dictionary.Add("JMBBH", fp.jmbbh);
            dictionary.Add("MW", fp.mw);
            dictionary.Add("HXM", "");
            dictionary.Add("GFMC", fp.gfmc);
            dictionary.Add("GFSH", fp.gfsh);
            dictionary.Add("XSBM", fp.sfzhm);
            dictionary.Add("GFDZDH", fp.cllx);
            dictionary.Add("XFDZ", fp.cpxh);
            dictionary.Add("KHYHMC", fp.cd);
            dictionary.Add("CM", fp.hgzh);
            dictionary.Add("TYDH", fp.jkzmsh);
            dictionary.Add("QYD", fp.sjdh);
            dictionary.Add("ZHD", fp.fdjhm);
            dictionary.Add("XHD", fp.clsbdh);
            dictionary.Add("XFMC", fp.xfmc);
            dictionary.Add("XFDH", fp.xfdh);
            dictionary.Add("XFSH", fp.xfsh);
            dictionary.Add("KHYHZH", fp.xfzh);
            dictionary.Add("XFDZDH", fp.xfdz);
            dictionary.Add("XFYHZH", fp.xfyh);
            if ((fp.sLv == "") || (fp.sLv == "NULL"))
            {
                dictionary.Add("SLV", null);
            }
            else
            {
                dictionary.Add("SLV", Tool.ObjectToDouble(fp.sLv));
            }
            dictionary.Add("HJSE", Tool.ObjectToDecimal(fp.se));
            dictionary.Add("GFYHZH", fp.zgswjgmc);
            dictionary.Add("HYBM", fp.zgswjgdm);
            dictionary.Add("HJJE", Tool.ObjectToDecimal(fp.je));
            dictionary.Add("YYZZH", fp.dw);
            dictionary.Add("MDD", fp.xcrs);
            dictionary.Add("KPR", fp.kpr);
            dictionary.Add("SKR", fp.skr);
            dictionary.Add("FHR", fp.fhr);
            dictionary.Add("ZYSPMC", (fp.zyspmc == null) ? "" : fp.zyspmc);
            dictionary.Add("SPSM_BM", fp.zyspsm);
            dictionary.Add("BZ", fp.bz);
            dictionary.Add("DYBZ", fp.dybz ? 1 : 0);
            dictionary.Add("QDBZ", (fp.Qdxx != null) ? 1 : 0);
            dictionary.Add("ZFBZ", fp.zfbz ? 1 : 0);
            dictionary.Add("BSBZ", fp.bsbz ? 1 : 0);
            dictionary.Add("XFBZ", fp.xfbz ? 1 : 0);
            dictionary.Add("JYM", fp.jym);
            dictionary.Add("BSQ", fp.bsq);
            dictionary.Add("SSYF", fp.ssyf);
            dictionary.Add("HZTZDH", fp.redNum);
            dictionary.Add("YYSBZ", fp.yysbz);
            dictionary.Add("YSHWXX", fp.yshwxx);
            dictionary.Add("SCCJMC", fp.sccjmc);
            if (fp.keyFlagNo == 0L)
            {
                dictionary.Add("SBBZ", "");
                dictionary.Add("SYH", "");
            }
            else
            {
                dictionary.Add("SBBZ", fp.keyFlagNo.ToString());
                dictionary.Add("SYH", fp.invQryNo.ToString());
            }
            dictionary.Add("BSZT", fp.bszt);
            dictionary.Add("SIGN", (fp.sign == null) ? "" : fp.sign);
            dictionary.Add("ZFRQ", ToolUtil.FormatDateTimeEx(fp.zfsj));
            dictionary.Add("WSPZHM", "");
            string str = "";
            if (((fp.isRed && (fp.blueFpdm != null)) && ((fp.blueFphm != null) && (fp.blueFpdm.Length > 0))) && (fp.blueFphm.Length > 0))
            {
                str = fp.blueFpdm + "_" + ShareMethods.FPHMTo8Wei(fp.blueFphm);
            }
            dictionary.Add("LZDMHM", str);
            if (fp.OtherFields != null)
            {
                dictionary.Add("PZRQ", Tool.ObjectToDateTime(ToolUtil.FormatDateTimeEx(fp.OtherFields["PZRQ"])));
                dictionary.Add("PZLB", fp.OtherFields["PZLB"]);
                dictionary.Add("PZHM", Tool.ObjectToInt(fp.OtherFields["PZHM"]));
                dictionary.Add("PZYWH", fp.OtherFields["PZYWH"]);
                dictionary.Add("PZZT", Tool.ObjectToInt(fp.OtherFields["PZZT"]));
                dictionary.Add("PZWLYWH", fp.OtherFields["PZWLYWH"]);
                dictionary.Add("BSRZ", fp.OtherFields["BSRZ"]);
            }
            else
            {
                dictionary.Add("PZRQ", Tool.ObjectToDateTime(ToolUtil.FormatDateTimeEx("1899-12-30 23:59:59")));
                dictionary.Add("PZLB", "");
                dictionary.Add("PZHM", -1);
                dictionary.Add("PZYWH", "");
                dictionary.Add("PZZT", -1);
                dictionary.Add("PZWLYWH", "");
                dictionary.Add("BSRZ", "");
            }
            dictionary.Add("DZSYH", fp.dzsyh);
            dictionary.Add("KPSXH", fp.kpsxh);
            dictionary.Add("BMBBBH", fp.bmbbbh);
            return dictionary;
        }

        private Dictionary<string, object> SetPTZYFpxxParam(Fpxx fp)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", Invoice.FPLX2Str(fp.fplx));
            dictionary.Add("FPDM", fp.fpdm);
            dictionary.Add("FPHM", Tool.ObjectToInt(fp.fphm));
            dictionary.Add("KPJH", fp.kpjh);
            dictionary.Add("XSDJBH", fp.xsdjbh);
            dictionary.Add("JMBBH", fp.jmbbh);
            dictionary.Add("GFMC", fp.gfmc);
            dictionary.Add("XFMC", fp.xfmc);
            dictionary.Add("GFYHZH", fp.gfyhzh);
            dictionary.Add("XFYHZH", fp.xfyhzh);
            dictionary.Add("GFSH", fp.gfsh);
            dictionary.Add("XFSH", fp.xfsh);
            dictionary.Add("GFDZDH", fp.gfdzdh);
            dictionary.Add("XFDZDH", fp.xfdzdh);
            dictionary.Add("HXM", fp.hxm);
            dictionary.Add("MW", fp.mw);
            dictionary.Add("KPRQ", Tool.ObjectToDateTime(ToolUtil.FormatDateTimeEx(fp.kprq)));
            dictionary.Add("HJJE", Tool.ObjectToDecimal(fp.je));
            if ((fp.sLv == "") || (fp.sLv == "NULL"))
            {
                dictionary.Add("SLV", null);
            }
            else
            {
                dictionary.Add("SLV", Tool.ObjectToDouble(fp.sLv));
            }
            dictionary.Add("HJSE", Tool.ObjectToDecimal(fp.se));
            dictionary.Add("ZYSPMC", (fp.zyspmc == null) ? "" : fp.zyspmc);
            dictionary.Add("SPSM_BM", fp.zyspsm);
            dictionary.Add("BZ", fp.bz);
            dictionary.Add("KPR", fp.kpr);
            dictionary.Add("SKR", fp.skr);
            dictionary.Add("FHR", fp.fhr);
            dictionary.Add("DYBZ", fp.dybz ? 1 : 0);
            dictionary.Add("QDBZ", (fp.Qdxx != null) ? 1 : 0);
            dictionary.Add("ZFBZ", fp.zfbz ? 1 : 0);
            dictionary.Add("BSBZ", fp.bsbz ? 1 : 0);
            dictionary.Add("XFBZ", fp.xfbz ? 1 : 0);
            dictionary.Add("JYM", fp.jym);
            dictionary.Add("BSQ", fp.bsq);
            dictionary.Add("SSYF", fp.ssyf);
            dictionary.Add("HZTZDH", fp.redNum);
            dictionary.Add("YYSBZ", fp.yysbz);
            if (fp.keyFlagNo == 0L)
            {
                dictionary.Add("SBBZ", "");
                dictionary.Add("SYH", "");
            }
            else
            {
                dictionary.Add("SBBZ", fp.keyFlagNo.ToString());
                dictionary.Add("SYH", fp.invQryNo.ToString());
            }
            dictionary.Add("BSZT", fp.bszt);
            dictionary.Add("SIGN", (fp.sign == null) ? "" : fp.sign);
            dictionary.Add("ZFRQ", ToolUtil.FormatDateTimeEx(fp.zfsj));
            dictionary.Add("WSPZHM", "");
            string str = "";
            if (((fp.isRed && (fp.blueFpdm != null)) && ((fp.blueFphm != null) && (fp.blueFpdm.Length > 0))) && (fp.blueFphm.Length > 0))
            {
                str = fp.blueFpdm + "_" + ShareMethods.FPHMTo8Wei(fp.blueFphm);
            }
            dictionary.Add("LZDMHM", str);
            if (fp.OtherFields != null)
            {
                dictionary.Add("PZRQ", Tool.ObjectToDateTime(ToolUtil.FormatDateTimeEx(fp.OtherFields["PZRQ"])));
                dictionary.Add("PZLB", fp.OtherFields["PZLB"]);
                dictionary.Add("PZHM", Tool.ObjectToInt(fp.OtherFields["PZHM"].ToString()));
                dictionary.Add("PZYWH", fp.OtherFields["PZYWH"]);
                dictionary.Add("PZZT", Tool.ObjectToInt(fp.OtherFields["PZZT"].ToString()));
                dictionary.Add("PZWLYWH", fp.OtherFields["PZWLYWH"]);
                dictionary.Add("BSRZ", fp.OtherFields["BSRZ"]);
            }
            else
            {
                dictionary.Add("PZRQ", Tool.ObjectToDateTime(ToolUtil.FormatDateTimeEx("1899-12-30 23:59:59")));
                dictionary.Add("PZLB", "");
                dictionary.Add("PZHM", -1);
                dictionary.Add("PZYWH", "");
                dictionary.Add("PZZT", -1);
                dictionary.Add("PZWLYWH", "");
                dictionary.Add("BSRZ", "");
            }
            dictionary.Add("DZSYH", fp.dzsyh);
            dictionary.Add("KPSXH", fp.kpsxh);
            dictionary.Add("JQBH", fp.jqbh);
            dictionary.Add("BMBBBH", fp.bmbbbh);
            if ((int)fp.fplx == 0x29)
            {
                fp.dy_mb = Tool.getDymb(fp.yysbz);
            }
            return dictionary;
        }

        private void SetSpxxParam(string sqlID, List<string> sqls, List<Dictionary<string, object>> paramAll, Fpxx fp, List<Dictionary<SPXX, string>> sps)
        {
            int num = 1;
            foreach (Dictionary<SPXX, string> dictionary in sps)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("FPZL", Invoice.FPLX2Str(fp.fplx));
                item.Add("FPDM", fp.fpdm);
                item.Add("FPHM", Tool.ObjectToInt(fp.fphm));
                item.Add("FPMXXH", Tool.ObjectToDouble(dictionary[(SPXX)13]));
                item.Add("XSDJBH", fp.xsdjbh);
                item.Add("FPHXZ", dictionary[(SPXX)10]);
                item.Add("JE", Tool.ObjectToDouble(dictionary[(SPXX)7]));
                if (dictionary[(SPXX)8] == "")
                {
                    item.Add("SLV", null);
                }
                else
                {
                    item.Add("SLV", Tool.ObjectToDouble(dictionary[(SPXX)8]));
                }
                item.Add("SE", Tool.ObjectToDouble(dictionary[(SPXX)9]));
                item.Add("SPMC", dictionary[(SPXX)0]);
                item.Add("SPSM", dictionary[(SPXX)2]);
                if ((int)fp.fplx == 0x29)
                {
                    item.Add("GGXH", "");
                    item.Add("JLDW", "");
                }
                else
                {
                    item.Add("GGXH", dictionary[(SPXX)3]);
                    item.Add("JLDW", dictionary[(SPXX)4]);
                }
                if (dictionary[(SPXX)6] == "")
                {
                    item.Add("SL", null);
                }
                else
                {
                    item.Add("SL", dictionary[(SPXX)6]);
                }
                if (dictionary[(SPXX)5] == "")
                {
                    item.Add("DJ", null);
                }
                else
                {
                    item.Add("DJ", dictionary[(SPXX)5]);
                }
                item.Add("HSJBZ", dictionary[(SPXX)11].Equals("1") ? 1 : 0);
                item.Add("DJMXXH", null);
                if (dictionary.ContainsKey((SPXX)20))
                {
                    item.Add("FLBM", dictionary[(SPXX)20].ToString());
                    item.Add("XSYH", (dictionary[(SPXX)0x15].ToString() == "") ? null : dictionary[(SPXX)0x15].ToString());
                    item.Add("YHSM", dictionary[(SPXX)0x16].ToString());
                    item.Add("LSLVBS", (dictionary[(SPXX)0x17].ToString() == "") ? null : dictionary[(SPXX)0x17].ToString());
                    item.Add("SPBH", dictionary[(SPXX)1].ToString());
                }
                else
                {
                    item.Add("FLBM", "");
                    item.Add("XSYH", null);
                    item.Add("YHSM", "");
                    item.Add("LSLVBS", null);
                    item.Add("SPBH", "");
                }
                sqls.Add(sqlID);
                paramAll.Add(item);
                num++;
            }
        }

        public bool SetYiDaYin(string FPZL, string FPDM, int FPHM)
        {
            try
            {
                this.Sharedict.Clear();
                this.Sharedict.Add("FPZL", FPZL);
                this.Sharedict.Add("FPDM", FPDM);
                this.Sharedict.Add("FPHM", FPHM);
                return (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.fwkp.fpkj.SetYiDaYin", this.Sharedict) > 0);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return false;
            }
        }

        public bool UpdateXXFP_FPXF(Fpxx _Fpxx, bool bNewCard)
        {
            try
            {
                this.Sharedict.Clear();
                string str = "aisino.fwkp.fpkj.UpdateXXFP_FPXF_Fpxx_NewCard";
                if (bNewCard)
                {
                    str = "aisino.fwkp.fpkj.UpdateXXFP_FPXF_Fpxx_NewCard";
                }
                else
                {
                    str = "aisino.fwkp.fpkj.UpdateXXFP_FPXF_Fpxx_OldCard";
                }
                _InvoiceType invoiceType = this.GetInvoiceType(_Fpxx.fplx);
                this.Sharedict.Add("FPZL", invoiceType.dbfpzl);
                this.Sharedict.Add("FPDM", _Fpxx.fpdm);
                string fphm = _Fpxx.fphm;
                int result = 0;
                int.TryParse(fphm, out result);
                this.Sharedict.Add("FPHM", result);
                this.Sharedict.Add("KPJH", _Fpxx.kpjh);
                DateTime time = DateTime.ParseExact(_Fpxx.kprq, "yyyyMMdd", CultureInfo.CurrentCulture);
                this.Sharedict.Add("KPRQ", time);
                this.Sharedict.Add("SSYF", _Fpxx.ssyf);
                this.Sharedict.Add("XFBZ", 1);
                this.Sharedict.Add("BSQ", _Fpxx.bsq);
                this.Sharedict.Add("BSBZ", _Fpxx.bsbz);
                this.Sharedict.Add("MW", _Fpxx.mw);
                this.Sharedict.Add("HJJE", Tool.ObjectToDouble(_Fpxx.je));
                this.Sharedict.Add("HJSE", Tool.ObjectToDouble(_Fpxx.se));
                this.Sharedict.Add("SLV", Tool.ObjectToDouble(_Fpxx.sLv));
                this.Sharedict.Add("ZFBZ", _Fpxx.zfbz);
                this.Sharedict.Add("YYSBZ", _Fpxx.yysbz);
                this.Sharedict.Add("GFMC", _Fpxx.gfmc);
                this.Sharedict.Add("GFSH", _Fpxx.gfsh);
                this.Sharedict.Add("GFDZDH", _Fpxx.gfdzdh);
                this.Sharedict.Add("GFYHZH", _Fpxx.gfyhzh);
                this.Sharedict.Add("XFMC", _Fpxx.xfmc);
                this.Sharedict.Add("XFSH", _Fpxx.xfsh);
                this.Sharedict.Add("XFDZDH", _Fpxx.xfdzdh);
                this.Sharedict.Add("XFYHZH", _Fpxx.xfyhzh);
                this.Sharedict.Add("KPR", _Fpxx.kpr);
                this.Sharedict.Add("FHR", _Fpxx.fhr);
                this.Sharedict.Add("SKR", _Fpxx.skr);
                this.Sharedict.Add("BZ", _Fpxx.bz);
                this.Sharedict.Add("ZYSPMC", _Fpxx.zyspmc);
                this.Sharedict.Add("JMBBH", _Fpxx.jmbbh);
                this.Sharedict.Add("HZTZDH", _Fpxx.redNum.Trim());
                this.Sharedict.Add("JZPZHM", string.Empty);
                this.Sharedict.Add("JYM", _Fpxx.jym);
                this.Sharedict.Add("JMBBH", _Fpxx.jmbbh);
                if (this.baseDao.未确认DAO方法2_疑似updateSQL(str, this.Sharedict) <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return false;
            }
        }

        public int UpdateXXFPBSZT(Dictionary<string, object> condition)
        {
            try
            {
                string str = "aisino.fwkp.fpkj.ManualUpLoadXXFP";
                return this.baseDao.未确认DAO方法2_疑似updateSQL(str, condition);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
            return 0;
        }

        public bool ZuoFei(List<Dictionary<string, object>> WasteFpCondition)
        {
            try
            {
                return (this.baseDao.未确认DAO方法3("aisino.fwkp.fpkj.ZuoFeiXXFP", WasteFpCondition) > 0);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return false;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct _InvoiceType
        {
            public string dbfpzl;
            public InvoiceType taxCardfpzl;
            public string Displayfpzl;
        }
    }
}

