namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal class SaleBillDAL
    {
        private static IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog log = LogUtil.GetLogger<SaleBillDAL>();

        private string AddSaleBillatom(SaleBill bill)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Clear();
                dictionary.Add("BH", bill.BH);
                ArrayList list = baseDAO.querySQL("aisino.Fwkp.Wbjk.XSDJ_HY_BH", dictionary);
                if ((list != null) && (list.Count > 0))
                {
                    return "e2";
                }
                double jEHJ = bill.JEHJ;
                foreach (Goods goods in bill.ListGoods)
                {
                    if (goods.FPZL.Trim() == "")
                    {
                        goods.FPZL = bill.DJZL;
                    }
                    Dictionary<string, object> dictionary2 = this.PushMX(goods);
                    if (baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJMXAdd", dictionary2) == 0)
                    {
                        break;
                    }
                }
                Dictionary<string, object> dictionary3 = this.PushXSDJ(bill);
                if (baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJAdds", dictionary3) > 0)
                {
                    return "0";
                }
                return "error";
            }
            catch (SQLiteException exception)
            {
                if (!exception.Message.Contains("UNIQUE constraint failed"))
                {
                    throw;
                }
                return "e1";
            }
        }

        public string AddSaveSaleBill(SaleBill bill)
        {
            string str2;
            string str = "";
            try
            {
                str = this.AddSaleBillatom(bill);
                switch (str)
                {
                    case "e1":
                    case "e2":
                        return str;
                }
                str2 = str;
            }
            catch (Exception)
            {
                throw;
            }
            return str2;
        }

        public string AddXSDJ(SaleBill xsdj)
        {
            try
            {
                List<string> list = new List<string>();
                List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
                double num = 0.0;
                foreach (Goods goods in xsdj.ListGoods)
                {
                    num += goods.JE;
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    item.Add("XSDJBH", goods.XSDJBH);
                    item.Add("XH", goods.XH);
                    item.Add("SL", goods.SL);
                    item.Add("DJ", goods.DJ);
                    item.Add("JE", goods.JE);
                    item.Add("SLV", goods.SLV);
                    item.Add("SE", goods.SE);
                    item.Add("SPMC", goods.SPMC);
                    item.Add("SPSM", goods.SPSM);
                    item.Add("GGXH", goods.GGXH);
                    item.Add("JLDW", goods.JLDW);
                    item.Add("HSJBZ", goods.HSJBZ);
                    item.Add("DJHXZ", goods.DJHXZ);
                    item.Add("FPZL", xsdj.DJZL);
                    item.Add("FPDM", null);
                    item.Add("FPHM", null);
                    item.Add("SCFPXH", null);
                    item.Add("FLBM", goods.FLBM);
                    item.Add("XSYH", goods.XSYH);
                    item.Add("FLMC", goods.FLMC);
                    item.Add("XSYHSM", goods.XSYHSM);
                    item.Add("SPBM", goods.SPBM);
                    list.Add("aisino.Fwkp.Wbjk.XSDJMXAdd");
                    list2.Add(item);
                }
                baseDAO.updateSQL(list.ToArray(), list2);
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("BH", xsdj.BH);
                dictionary2.Add("GFMC", xsdj.GFMC);
                dictionary2.Add("GFSH", xsdj.GFSH);
                dictionary2.Add("GFDZDH", xsdj.GFDZDH);
                dictionary2.Add("GFYHZH", xsdj.GFYHZH);
                dictionary2.Add("XSBM", xsdj.XSBM);
                dictionary2.Add("YDXS", xsdj.YDXS);
                dictionary2.Add("JEHJ", xsdj.JEHJ);
                dictionary2.Add("DJRQ", xsdj.DJRQ);
                dictionary2.Add("DJYF", xsdj.DJYF);
                dictionary2.Add("DJZT", "Y");
                dictionary2.Add("KPZT", "N");
                dictionary2.Add("BZ", xsdj.BZ);
                dictionary2.Add("FHR", xsdj.FHR);
                dictionary2.Add("SKR", xsdj.SKR);
                dictionary2.Add("QDHSPMC", xsdj.QDHSPMC);
                dictionary2.Add("XFYHZH", xsdj.XFYHZH);
                dictionary2.Add("XFDZDH", xsdj.XFDZDH);
                dictionary2.Add("CFHB", xsdj.CFHB);
                dictionary2.Add("DJZL", xsdj.DJZL);
                dictionary2.Add("SFZJY", xsdj.SFZJY);
                dictionary2.Add("HYSY", xsdj.HYSY);
                dictionary2.Add("CM", xsdj.CM);
                dictionary2.Add("DLGRQ", xsdj.DLGRQ);
                dictionary2.Add("KHYHMC", xsdj.KHYHMC);
                dictionary2.Add("KHYHZH", xsdj.KHYHZH);
                dictionary2.Add("TYDH", xsdj.TYDH);
                dictionary2.Add("QYD", xsdj.QYD);
                dictionary2.Add("ZHD", xsdj.ZHD);
                dictionary2.Add("XHD", xsdj.XHD);
                dictionary2.Add("MDD", xsdj.MDD);
                dictionary2.Add("XFDZ", xsdj.XFDZ);
                dictionary2.Add("XFDH", xsdj.XFDH);
                dictionary2.Add("YSHWXX", xsdj.YSHWXX);
                dictionary2.Add("SCCJMC", xsdj.SCCJMC);
                dictionary2.Add("SLV", xsdj.SLV);
                dictionary2.Add("DW", xsdj.DW);
                if (baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJAdds", dictionary2) > 0)
                {
                    return "0";
                }
                return "error";
            }
            catch (Exception exception)
            {
                string message = exception.Message;
                if (exception.InnerException != null)
                {
                    message = message + "  " + exception.InnerException.Message;
                }
                return message;
            }
        }

        public string AllowImport(string XSDJBH)
        {
            string str = this.BeSpliOrColl(XSDJBH);
            if (str != "0")
            {
                return str;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bh", XSDJBH);
            switch (baseDAO.queryValueSQL<string>("aisino.Fwkp.Wbjk.XSDJKPState", dictionary))
            {
                case "A":
                case "P":
                    return "已经开具发票";
            }
            return "0";
        }

        public string BeSpliOrColl(string XSDJBH)
        {
            string str = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bh", XSDJBH);
            if (baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.XSDJHYExist", dictionary) > 0)
            {
                str = baseDAO.queryValueSQL<string>("aisino.Fwkp.Wbjk.XSDJHY_YSBH", dictionary);
                if (str.Trim() != "")
                {
                    str = XSDJBH + "_0等";
                }
            }
            if (str != "")
            {
                return ("已经被拆分合并为" + str);
            }
            return "0";
        }

        public int DeleteSaleBill(List<string> listBH)
        {
            int num4;
            try
            {
                int num = 0;
                for (int i = 0; i < listBH.Count; i++)
                {
                    string str = listBH[i];
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("BH", str);
                    if ((baseDAO.queryValueSQL<string>("aisino.Fwkp.Wbjk.XSDJYLselectDJZT", dictionary) != "W") && (baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJDelete", dictionary) > 0))
                    {
                        num++;
                    }
                }
                num4 = num;
            }
            catch
            {
                throw;
            }
            return num4;
        }

        public SaleBill Find(string XSDJBH, string spzt)
        {
            SaleBill bill = new SaleBill {
                IsANew = false
            };
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", XSDJBH);
            DataTable table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJGetModel", dictionary);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = table.Rows[0];
            if (GetSafeData.ValidateValue<string>(row, "BH") != null)
            {
                bill.BH = GetSafeData.ValidateValue<string>(row, "BH");
            }
            if (GetSafeData.ValidateValue<string>(row, "GFMC") != null)
            {
                bill.GFMC = GetSafeData.ValidateValue<string>(row, "GFMC");
            }
            if (GetSafeData.ValidateValue<string>(row, "GFSH") != null)
            {
                bill.GFSH = GetSafeData.ValidateValue<string>(row, "GFSH");
            }
            if (GetSafeData.ValidateValue<string>(row, "GFDZDH") != null)
            {
                bill.GFDZDH = GetSafeData.ValidateValue<string>(row, "GFDZDH");
            }
            if (GetSafeData.ValidateValue<string>(row, "GFYHZH") != null)
            {
                bill.GFYHZH = GetSafeData.ValidateValue<string>(row, "GFYHZH");
            }
            if (GetSafeData.ValidateValue<string>(row, "XSBM") != null)
            {
                bill.XSBM = GetSafeData.ValidateValue<string>(row, "XSBM");
            }
            GetSafeData.ValidateValue<bool>(row, "YDXS");
            bool flag = 1 == 0;
            bill.YDXS = GetSafeData.ValidateValue<bool>(row, "YDXS");
            GetSafeData.ValidateValue<double>(row, "JEHJ");
            flag = 1 == 0;
            bill.JEHJ = GetSafeData.ValidateValue<double>(row, "JEHJ");
            GetSafeData.ValidateValue<DateTime>(row, "DJRQ");
            flag = 1 == 0;
            bill.DJRQ = GetSafeData.ValidateValue<DateTime>(row, "DJRQ");
            GetSafeData.ValidateValue<int>(row, "DJYF");
            flag = 1 == 0;
            bill.DJYF = GetSafeData.ValidateValue<int>(row, "DJYF");
            if (GetSafeData.ValidateValue<string>(row, "DJZT") != null)
            {
                bill.DJZT = GetSafeData.ValidateValue<string>(row, "DJZT");
            }
            if (GetSafeData.ValidateValue<string>(row, "KPZT") != null)
            {
                bill.KPZT = GetSafeData.ValidateValue<string>(row, "KPZT");
            }
            if (GetSafeData.ValidateValue<string>(row, "BZ") != null)
            {
                bill.BZ = GetSafeData.ValidateValue<string>(row, "BZ");
            }
            if (GetSafeData.ValidateValue<string>(row, "FHR") != null)
            {
                bill.FHR = GetSafeData.ValidateValue<string>(row, "FHR");
            }
            if (GetSafeData.ValidateValue<string>(row, "SKR") != null)
            {
                bill.SKR = GetSafeData.ValidateValue<string>(row, "SKR");
            }
            if (GetSafeData.ValidateValue<string>(row, "QDHSPMC") != null)
            {
                bill.QDHSPMC = GetSafeData.ValidateValue<string>(row, "QDHSPMC");
            }
            if (GetSafeData.ValidateValue<string>(row, "XFYHZH") != null)
            {
                bill.XFYHZH = GetSafeData.ValidateValue<string>(row, "XFYHZH");
            }
            if (GetSafeData.ValidateValue<string>(row, "XFDZDH") != null)
            {
                bill.XFDZDH = GetSafeData.ValidateValue<string>(row, "XFDZDH");
            }
            GetSafeData.ValidateValue<bool>(row, "CFHB");
            flag = 1 == 0;
            bill.CFHB = GetSafeData.ValidateValue<bool>(row, "CFHB");
            if (GetSafeData.ValidateValue<string>(row, "DJZL") != null)
            {
                bill.DJZL = GetSafeData.ValidateValue<string>(row, "DJZL");
            }
            if (GetSafeData.ValidateValue<bool>(row, "SFZJY"))
            {
                bill.SFZJY = GetSafeData.ValidateValue<bool>(row, "SFZJY");
            }
            GetSafeData.ValidateValue<bool>(row, "HYSY");
            flag = 1 == 0;
            bill.HYSY = GetSafeData.ValidateValue<bool>(row, "HYSY");
            if (GetSafeData.ValidateValue<string>(row, "CM") != null)
            {
                bill.CM = GetSafeData.ValidateValue<string>(row, "CM");
            }
            GetSafeData.ValidateValue<DateTime>(row, "DLGRQ");
            flag = 1 == 0;
            bill.DLGRQ = GetSafeData.ValidateValue<DateTime>(row, "DLGRQ");
            if (GetSafeData.ValidateValue<string>(row, "KHYHMC") != null)
            {
                bill.KHYHMC = GetSafeData.ValidateValue<string>(row, "KHYHMC");
            }
            if (GetSafeData.ValidateValue<string>(row, "KHYHZH") != null)
            {
                bill.KHYHZH = GetSafeData.ValidateValue<string>(row, "KHYHZH");
            }
            if (GetSafeData.ValidateValue<string>(row, "TYDH") != null)
            {
                bill.TYDH = GetSafeData.ValidateValue<string>(row, "TYDH");
            }
            if (GetSafeData.ValidateValue<string>(row, "QYD") != null)
            {
                bill.QYD = GetSafeData.ValidateValue<string>(row, "QYD");
            }
            if (GetSafeData.ValidateValue<string>(row, "ZHD") != null)
            {
                bill.ZHD = GetSafeData.ValidateValue<string>(row, "ZHD");
            }
            if (GetSafeData.ValidateValue<string>(row, "XHD") != null)
            {
                bill.XHD = GetSafeData.ValidateValue<string>(row, "XHD");
            }
            if (GetSafeData.ValidateValue<string>(row, "MDD") != null)
            {
                bill.MDD = GetSafeData.ValidateValue<string>(row, "MDD");
            }
            if (GetSafeData.ValidateValue<string>(row, "XFDZ") != null)
            {
                bill.XFDZ = GetSafeData.ValidateValue<string>(row, "XFDZ");
            }
            if (GetSafeData.ValidateValue<string>(row, "XFDH") != null)
            {
                bill.XFDH = GetSafeData.ValidateValue<string>(row, "XFDH");
            }
            if (GetSafeData.ValidateValue<string>(row, "YSHWXX") != null)
            {
                bill.YSHWXX = GetSafeData.ValidateValue<string>(row, "YSHWXX");
            }
            if (GetSafeData.ValidateValue<string>(row, "SCCJMC") != null)
            {
                bill.SCCJMC = GetSafeData.ValidateValue<string>(row, "SCCJMC");
            }
            GetSafeData.ValidateValue<double>(row, "SLV");
            flag = 1 == 0;
            bill.SLV = GetSafeData.ValidateValue<double>(row, "SLV");
            if (GetSafeData.ValidateValue<string>(row, "DW") != null)
            {
                bill.DW = GetSafeData.ValidateValue<string>(row, "DW");
            }
            GetSafeData.ValidateValue<bool>(row, "JZ_50_15");
            flag = 1 == 0;
            bill.JZ_50_15 = GetSafeData.ValidateValue<bool>(row, "JZ_50_15");
            if (CommonTool.isSPBMVersion() && (bill.DJZL.ToUpper() == "J"))
            {
                bill.JDC_XSYH = false;
                string str = row["XSYH"].ToString().Trim();
                if ((str != "") && ((((str == "1") || (str == "是")) || (str == "享受")) || (str.ToUpper() == "TRUE")))
                {
                    bill.JDC_XSYH = true;
                }
                bill.JDC_FLBM = row["FLBM"].ToString().Trim();
                bill.JDC_FLMC = "";
                bill.JDC_LSLVBS = row["LSLVBS"].ToString().Trim();
                bill.JDC_XSYHSM = row["XSYHSM"].ToString().Trim();
                bill.JDC_CLBM = row["CLBM"].ToString().Trim();
                bill.JDC_LX = bill.GFDZDH;
                bill.JDC_CPXH = bill.XFDZ;
                string str2 = bill.JDC_CPXH.Trim();
                string str3 = bill.JDC_LX.Trim();
                if (str2 != "")
                {
                    bool flag1 = str3 != "";
                }
                flag = 0 == 0;
            }
            DataTable mXDJ = this.GetMXDJ(XSDJBH);
            foreach (DataRow row in mXDJ.Rows)
            {
                Goods item = new Goods {
                    XSDJBH = GetSafeData.ValidateValue<string>(row, "XSDJBH"),
                    XH = GetSafeData.ValidateValue<int>(row, "XH"),
                    SL = GetSafeData.ValidateValue<double>(row, "SL"),
                    DJ = GetSafeData.ValidateValue<double>(row, "DJ"),
                    JE = GetSafeData.ValidateValue<double>(row, "JE"),
                    SLV = GetSafeData.ValidateValue<double>(row, "SLV"),
                    SE = GetSafeData.ValidateValue<double>(row, "SE"),
                    SPMC = GetSafeData.ValidateValue<string>(row, "SPMC"),
                    SPSM = GetSafeData.ValidateValue<string>(row, "SPSM"),
                    GGXH = GetSafeData.ValidateValue<string>(row, "GGXH"),
                    JLDW = GetSafeData.ValidateValue<string>(row, "JLDW"),
                    HSJBZ = GetSafeData.ValidateValue<bool>(row, "HSJBZ"),
                    DJHXZ = GetSafeData.ValidateValue<int>(row, "DJHXZ"),
                    FPZL = GetSafeData.ValidateValue<string>(row, "FPZL"),
                    FPDM = GetSafeData.ValidateValue<string>(row, "FPDM"),
                    FPHM = GetSafeData.ValidateValue<int>(row, "FPHM"),
                    SCFPXH = GetSafeData.ValidateValue<int>(row, "SCFPXH"),
                    KCE = GetSafeData.ValidateValue<double>(row, "KCE")
                };
                if (CommonTool.isSPBMVersion())
                {
                    item.FLBM = row["FLBM"].ToString().Trim();
                    item.FLMC = "";
                    item.XSYHSM = row["XSYHSM"].ToString().Trim();
                    item.SPBM = row["SPBM"].ToString().Trim();
                    item.LSLVBS = row["LSLVBS"].ToString().Trim();
                    if (row["XSYH"] != null)
                    {
                        string str4 = row["XSYH"].ToString().Trim();
                        if ((str4 != "") && ((((str4 == "1") || (str4 == "是")) || (str4 == "享受")) || (str4.ToUpper() == "TRUE")))
                        {
                            item.XSYH = true;
                        }
                    }
                    item.LSLVBS = GetSafeData.ValidateValue<string>(row, "LSLVBS");
                    if (item.FLBM.Trim() == "")
                    {
                        bool flag2 = item.SPMC != "";
                    }
                    flag = 0 == 0;
                }
                if (spzt == "NotInv")
                {
                    if (string.IsNullOrEmpty(item.FPDM))
                    {
                        bill.ListGoods.Add(item);
                    }
                }
                else
                {
                    bill.ListGoods.Add(item);
                }
            }
            return bill;
        }

        public DataTable GET_CLXX_BY_CPXH(string CPXH, string CLLX)
        {
            DataTable table = new DataTable();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", CLLX);
            dictionary.Add("CPXH", CPXH);
            return baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetCLByCPXH", dictionary);
        }

        public string GET_FLBM_HZX(string FLBM)
        {
            string str = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FLBM", FLBM);
            DataTable table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetFLBM", dictionary);
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["HZX"].ToString().Trim();
            }
            return str;
        }

        public string GET_FLBM_KYZT(string FLBM)
        {
            string str = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FLBM", FLBM);
            DataTable table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetFLBM", dictionary);
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["KYZT"].ToString().Trim();
            }
            return str;
        }

        public string GET_FLBM_MC(string FLBM)
        {
            string str = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FLBM", FLBM);
            DataTable table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetFLBM", dictionary);
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["MC"].ToString().Trim();
            }
            return str;
        }

        public DataTable GET_SP_BY_BM(string BM)
        {
            DataTable table = new DataTable();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BM", BM);
            return baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_SP_GetSP", dictionary);
        }

        public DataTable GET_SPXX_BY_NAME(string MC, string DJLX, string CPXH = "")
        {
            DataTable table = new DataTable();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", MC);
            if ((DJLX.ToUpper() == "S") || (DJLX.ToUpper() == "C"))
            {
                table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetSPByName", dictionary);
            }
            if (DJLX.ToUpper() == "J")
            {
                dictionary.Add("CPXH", CPXH);
                table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetCLByCPXH", dictionary);
            }
            if (DJLX.ToUpper() == "F")
            {
                table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetFYXMByName", dictionary);
            }
            return table;
        }

        public List<string> GET_YHZCMC_BY_YHZCSLV(double slv)
        {
            List<string> list = new List<string>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetYHZC", dictionary);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string zCMC = table.Rows[i]["YHZCMC"].ToString().Trim();
                List<double> list2 = this.GET_YHZCSLV_BY_YHZCMC(zCMC);
                bool flag = false;
                for (int j = 0; j < list2.Count; j++)
                {
                    if (list2[j] == slv)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    list.Add(zCMC);
                }
            }
            return list;
        }

        public List<double> GET_YHZCSLV_BY_FLBM(string FLBM)
        {
            string str3;
            string str4;
            double num2;
            List<double> list = new List<double>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FLBM", FLBM);
            DataTable table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetFLBM", dictionary);
            string str = "";
            string str2 = "";
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["SLV"].ToString().Trim();
                str2 = table.Rows[0]["ZZSTSGL"].ToString().Trim();
            }
            string[] strArray = str.Split(new char[] { '、' });
            int index = 0;
            while (index < strArray.Length)
            {
                str3 = strArray[index].Trim();
                if (str3 != "")
                {
                    if (str3.EndsWith("%"))
                    {
                        num2 = Convert.ToDouble(str3.Replace("%", "")) / 100.0;
                        list.Add(num2);
                    }
                    else
                    {
                        str4 = str3;
                        num2 = Convert.ToDouble(str4);
                        if (num2 > 100.0)
                        {
                            num2 /= 100.0;
                        }
                        list.Add(num2);
                    }
                }
                index++;
            }
            foreach (string str5 in str2.Split(new char[] { '、' }))
            {
                dictionary = new Dictionary<string, object>();
                dictionary.Add("YHZCMC", str5);
                table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetYHZCSLV", dictionary);
                string str6 = "";
                if (table.Rows.Count > 0)
                {
                    str6 = table.Rows[0]["SLV"].ToString().Trim();
                }
                string[] strArray3 = str6.Split(new char[] { '、' });
                for (index = 0; index < strArray3.Length; index++)
                {
                    str3 = strArray3[index].Trim();
                    if (str3 != "")
                    {
                        if (str3.EndsWith("%"))
                        {
                            num2 = Convert.ToDouble(str3.Replace("%", "")) / 100.0;
                            list.Add(num2);
                        }
                        else
                        {
                            str4 = str3;
                            num2 = Convert.ToDouble(str4);
                            if (num2 > 100.0)
                            {
                                num2 /= 100.0;
                            }
                            list.Add(num2);
                        }
                    }
                }
            }
            return list;
        }

        public List<double> GET_YHZCSLV_BY_SPBM(string BM, string DJLX)
        {
            List<double> list = new List<double>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BM", BM);
            DataTable table = new DataTable();
            if ((DJLX.ToUpper() == "S") || (DJLX.ToUpper() == "C"))
            {
                table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetSPByBM", dictionary);
            }
            if (DJLX.ToUpper() == "J")
            {
                table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetCLByBM", dictionary);
            }
            if (DJLX.ToUpper() == "F")
            {
                table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetFYXMByBM", dictionary);
            }
            string str = "";
            if (table.Rows.Count > 0)
            {
                string parastrslv = table.Rows[0]["SYSLV"].ToString().Trim();
                List<double> list2 = this.GETDoubleSLV(parastrslv);
                foreach (double num in list2)
                {
                    list.Add(num);
                }
                str = table.Rows[0]["SPFL_ZZSTSGL"].ToString().Trim();
            }
            string[] strArray = str.Split(new char[] { '、' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string zCMC = strArray[i].Trim();
                List<double> list3 = this.GET_YHZCSLV_BY_YHZCMC(zCMC);
                foreach (double num3 in list3)
                {
                    list.Add(num3);
                }
            }
            return list;
        }

        public List<double> GET_YHZCSLV_BY_YHZCMC(string ZCMC)
        {
            List<double> list = new List<double>();
            string str = ZCMC.Trim();
            if (str != "")
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = new Dictionary<string, object>();
                dictionary.Add("YHZCMC", str);
                DataTable table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetYHZCSLV", dictionary);
                string str2 = "";
                if (table.Rows.Count > 0)
                {
                    str2 = table.Rows[0]["SLV"].ToString().Trim();
                }
                string[] strArray = str2.Split(new char[] { '、' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    string str3 = strArray[i].Trim();
                    if (str3.Contains("_"))
                    {
                        str3 = str3.Substring(0, str3.IndexOf('_'));
                    }
                    if (str3 != "")
                    {
                        double num2;
                        if (str3.EndsWith("%"))
                        {
                            num2 = Convert.ToDouble(str3.Replace("%", "")) / 100.0;
                            list.Add(num2);
                        }
                        else
                        {
                            string str5 = str3;
                            num2 = Convert.ToDouble(str5);
                            if (num2 > 100.0)
                            {
                                num2 /= 100.0;
                            }
                            list.Add(num2);
                        }
                    }
                }
            }
            return list;
        }

        public List<double> GET_YHZCSYSLV_BY_FLBM(string FLBM)
        {
            List<double> list = new List<double>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FLBM", FLBM);
            DataTable table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetFLBM", dictionary);
            string str = "";
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["SLV"].ToString().Trim();
            }
            string[] strArray = str.Split(new char[] { '、' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string str2 = strArray[i].Trim();
                if (str2 != "")
                {
                    double num2;
                    if (str2.EndsWith("%"))
                    {
                        num2 = Convert.ToDouble(str2.Replace("%", "")) / 100.0;
                        list.Add(num2);
                    }
                    else
                    {
                        string str3 = str2;
                        num2 = Convert.ToDouble(str3);
                        if (num2 > 100.0)
                        {
                            num2 /= 100.0;
                        }
                        list.Add(num2);
                    }
                }
            }
            return list;
        }

        public List<double> GET_YHZCSYSLV_BY_SPBM(string BM, string DJLX)
        {
            List<double> list = new List<double>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BM", BM);
            DataTable table = new DataTable();
            if ((DJLX.ToUpper() == "S") || (DJLX.ToUpper() == "C"))
            {
                table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetSPByBM", dictionary);
            }
            if (DJLX.ToUpper() == "J")
            {
                table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetCLByBM", dictionary);
            }
            if (DJLX.ToUpper() == "F")
            {
                table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJ_FLBM_GetFYXMByBM", dictionary);
            }
            if (table.Rows.Count > 0)
            {
                string fLBM = table.Rows[0]["SPFL"].ToString().Trim();
                List<double> list2 = this.GET_YHZCSYSLV_BY_FLBM(fLBM);
                foreach (double num in list2)
                {
                    list.Add(num);
                }
            }
            return list;
        }

        public DataTable GetAllDJforCheck(string DJmonth, string DJtype)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DJYF", DJmonth);
            dictionary.Add("DJZL", DJtype);
            return baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJGetDanJus", dictionary);
        }

        private List<double> GETDoubleSLV(string parastrslv)
        {
            List<double> list = new List<double>();
            double naN = double.NaN;
            try
            {
                string[] strArray = parastrslv.Split(new char[] { '、' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    double num3;
                    string str = strArray[i].Trim();
                    if (str.EndsWith("%"))
                    {
                        num3 = Convert.ToDouble(str.Replace("%", "")) / 100.0;
                        naN = num3;
                    }
                    else
                    {
                        num3 = Convert.ToDouble(str);
                        if (num3 > 100.0)
                        {
                            num3 /= 100.0;
                        }
                        naN = num3;
                    }
                    list.Add(naN);
                }
            }
            catch (Exception)
            {
            }
            return list;
        }

        public DataTable GetMXDJ(string XSDJBH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", XSDJBH);
            return baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJXGGetMX", dictionary);
        }

        public bool isXT(string mc)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", mc);
            return (baseDAO.querySQL("aisino.fwkp.Wbjk.selectXThash", dictionary).Count > 0);
        }

        private Dictionary<string, object> PushMX(Goods mx)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", mx.XSDJBH);
            dictionary.Add("XH", mx.XH);
            dictionary.Add("SL", mx.SL);
            dictionary.Add("DJ", mx.DJ);
            dictionary.Add("JE", mx.JE);
            dictionary.Add("SLV", mx.SLV);
            dictionary.Add("SE", mx.SE);
            dictionary.Add("SPMC", mx.SPMC);
            dictionary.Add("SPSM", mx.SPSM);
            dictionary.Add("GGXH", mx.GGXH);
            dictionary.Add("JLDW", mx.JLDW);
            dictionary.Add("HSJBZ", mx.HSJBZ);
            dictionary.Add("DJHXZ", mx.DJHXZ);
            dictionary.Add("FPZL", mx.FPZL);
            dictionary.Add("FPDM", mx.FPDM);
            dictionary.Add("FPHM", mx.FPHM);
            dictionary.Add("SCFPXH", mx.SCFPXH);
            dictionary.Add("FLBM", mx.FLBM);
            dictionary.Add("FLMC", mx.FLMC);
            dictionary.Add("XSYH", mx.XSYH);
            dictionary.Add("XSYHSM", mx.XSYHSM);
            dictionary.Add("SPBM", mx.SPBM);
            dictionary.Add("LSLVBS", mx.LSLVBS);
            dictionary.Add("KCE", mx.KCE);
            return dictionary;
        }

        private Dictionary<string, object> PushXSDJ(SaleBill bill)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", bill.BH);
            dictionary.Add("GFMC", bill.GFMC);
            dictionary.Add("GFSH", bill.GFSH);
            dictionary.Add("GFDZDH", bill.GFDZDH);
            dictionary.Add("GFYHZH", bill.GFYHZH);
            dictionary.Add("XSBM", bill.XSBM);
            dictionary.Add("YDXS", bill.YDXS);
            dictionary.Add("JEHJ", bill.JEHJ);
            dictionary.Add("DJRQ", bill.DJRQ);
            dictionary.Add("DJYF", bill.DJYF);
            dictionary.Add("DJZT", bill.DJZT);
            dictionary.Add("KPZT", bill.KPZT);
            dictionary.Add("BZ", bill.BZ);
            dictionary.Add("FHR", bill.FHR);
            dictionary.Add("SKR", bill.SKR);
            dictionary.Add("QDHSPMC", bill.QDHSPMC);
            dictionary.Add("XFYHZH", bill.XFYHZH.Trim());
            dictionary.Add("XFDZDH", bill.XFDZDH);
            dictionary.Add("CFHB", bill.CFHB);
            dictionary.Add("DJZL", bill.DJZL);
            dictionary.Add("SFZJY", bill.SFZJY);
            dictionary.Add("HYSY", bill.HYSY);
            dictionary.Add("CM", bill.CM);
            dictionary.Add("DLGRQ", bill.DLGRQ);
            dictionary.Add("KHYHMC", bill.KHYHMC);
            dictionary.Add("KHYHZH", bill.KHYHZH);
            dictionary.Add("TYDH", bill.TYDH);
            dictionary.Add("QYD", bill.QYD);
            dictionary.Add("ZHD", bill.ZHD);
            dictionary.Add("XHD", bill.XHD);
            dictionary.Add("MDD", bill.MDD);
            dictionary.Add("XFDZ", bill.XFDZ);
            dictionary.Add("XFDH", bill.XFDH);
            dictionary.Add("YSHWXX", bill.YSHWXX);
            dictionary.Add("SCCJMC", bill.SCCJMC);
            dictionary.Add("SLV", bill.SLV);
            dictionary.Add("DW", bill.DW);
            dictionary.Add("FLBM", bill.JDC_FLBM);
            dictionary.Add("FLMC", bill.JDC_FLMC);
            dictionary.Add("XSYH", bill.JDC_XSYH);
            dictionary.Add("XSYHSM", bill.JDC_XSYHSM);
            dictionary.Add("CLBM", bill.JDC_CLBM);
            dictionary.Add("LSLVBS", bill.JDC_LSLVBS);
            dictionary.Add("JZ_50_15", bill.JZ_50_15);
            return dictionary;
        }

        public object[] SaleBillMonth()
        {
            DataTable table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJGetMonth", null);
            object[] objArray = new object[table.Rows.Count + 1];
            objArray[0] = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                objArray[i + 1] = table.Rows[i][0];
            }
            return objArray;
        }

        public object[] SaleBillYearMonth(string DJZL1, string DJZL2 = "")
        {
            if ((DJZL1 == "") && (DJZL2 == ""))
            {
                return new object[0];
            }
            if (DJZL2 == "")
            {
                DJZL2 = DJZL1;
            }
            else if (DJZL1 == "")
            {
                DJZL1 = DJZL2;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DJZL1", DJZL1);
            dictionary.Add("DJZL2", DJZL2);
            DataTable table = baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJGetYearMonth", dictionary);
            object[] objArray = new object[table.Rows.Count + 1];
            objArray[0] = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                objArray[i + 1] = table.Rows[i][0];
            }
            return objArray;
        }

        public string SaveCollectDiscountToRealTable(SaleBill bill)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("BH", bill.BH);
                int num = baseDAO.updateSQL("aisino.Fwkp.Wbjk.ZKHZSaveHZ", dictionary);
                if (bill == null)
                {
                    throw new Exception("没有选择要汇总的单据编码");
                }
                if (num > 0)
                {
                    return "0";
                }
                return "SomeWrong";
            }
            catch (Exception exception)
            {
                return ("从预览表转正存库Fail:" + exception.ToString() + " " + exception.InnerException.ToString());
            }
        }

        internal void SaveImportBill(List<SaleBillImportTemp> listBill, PriceType priceType, InvType invType, ErrorResolver errorAnalyse)
        {
            try
            {
                MessageHelper.MsgWait("正在导入单据，请稍候...");
                errorAnalyse.ImportTotal = listBill.Count;
                try
                {
                    try
                    {
                        List<string> listBH = new List<string>();
                        List<string> list2 = new List<string>();
                        List<Dictionary<string, object>> list3 = new List<Dictionary<string, object>>();
                        if (listBill.Count == 0)
                        {
                            errorAnalyse.AddError("无被接收的销售单据", "无单据被接收", 0, true);
                        }
                        else
                        {
                            List<SaleBill> list4 = new List<SaleBill>();
                            for (int i = 0; i < listBill.Count; i++)
                            {
                                SaleBillImportTemp tmpbillImport = listBill[i];
                                string bH = "";
                                if ((invType == InvType.Common) || (invType == InvType.Special))
                                {
                                    bH = tmpbillImport.BH;
                                }
                                else if (invType == InvType.vehiclesales)
                                {
                                    bH = tmpbillImport.JDC_BH;
                                }
                                else if (invType == InvType.transportation)
                                {
                                    bH = tmpbillImport.HY_BH;
                                }
                                string str2 = string.Format("销售单据编号: {0} ", bH);
                                FatchSaleBill bill = new FatchSaleBill();
                                SaleBill saleBill = new SaleBill();
                                bool flag = bill.AnalyseSaleBill(listBH, tmpbillImport, priceType, invType, errorAnalyse, saleBill);
                                if (saleBill.ReserveA != "Abandon")
                                {
                                    string errowInfo = this.AllowImport(saleBill.BH);
                                    if (errowInfo != "0")
                                    {
                                        errorAnalyse.AddError(errowInfo, saleBill.BH, 0, false);
                                    }
                                    else
                                    {
                                        Dictionary<string, object> item = new Dictionary<string, object>();
                                        item.Add("bh", saleBill.BH);
                                        if (baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.XSDJExist", item) > 0)
                                        {
                                            list2.Add("aisino.Fwkp.Wbjk.XSDJDeleteFast");
                                            list3.Add(item);
                                        }
                                        foreach (Goods goods in saleBill.ListGoods)
                                        {
                                            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                                            dictionary2.Add("XSDJBH", goods.XSDJBH);
                                            dictionary2.Add("XH", goods.XH);
                                            dictionary2.Add("SL", goods.SL);
                                            dictionary2.Add("DJ", goods.DJ);
                                            dictionary2.Add("JE", goods.JE);
                                            dictionary2.Add("SLV", goods.SLV);
                                            dictionary2.Add("SE", goods.SE);
                                            dictionary2.Add("SPMC", goods.SPMC);
                                            dictionary2.Add("SPSM", goods.SPSM);
                                            dictionary2.Add("GGXH", goods.GGXH);
                                            dictionary2.Add("JLDW", goods.JLDW);
                                            dictionary2.Add("HSJBZ", goods.HSJBZ);
                                            dictionary2.Add("DJHXZ", goods.DJHXZ);
                                            dictionary2.Add("FPZL", saleBill.DJZL);
                                            dictionary2.Add("FPDM", null);
                                            dictionary2.Add("FPHM", null);
                                            dictionary2.Add("SCFPXH", null);
                                            dictionary2.Add("FLBM", goods.FLBM);
                                            dictionary2.Add("FLMC", goods.FLMC);
                                            dictionary2.Add("XSYH", goods.XSYH);
                                            dictionary2.Add("XSYHSM", goods.XSYHSM);
                                            dictionary2.Add("SPBM", goods.SPBM);
                                            dictionary2.Add("LSLVBS", goods.LSLVBS);
                                            dictionary2.Add("KCE", goods.KCE);
                                            list2.Add("aisino.Fwkp.Wbjk.XSDJMXAdd");
                                            list3.Add(dictionary2);
                                        }
                                        Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                                        dictionary3.Add("BH", saleBill.BH);
                                        dictionary3.Add("GFMC", saleBill.GFMC);
                                        dictionary3.Add("GFSH", saleBill.GFSH);
                                        dictionary3.Add("GFDZDH", saleBill.GFDZDH);
                                        dictionary3.Add("GFYHZH", saleBill.GFYHZH);
                                        dictionary3.Add("XSBM", saleBill.XSBM);
                                        dictionary3.Add("YDXS", saleBill.YDXS);
                                        dictionary3.Add("JEHJ", saleBill.JEHJ);
                                        dictionary3.Add("DJRQ", saleBill.DJRQ);
                                        dictionary3.Add("DJYF", saleBill.DJYF);
                                        dictionary3.Add("DJZT", "Y");
                                        dictionary3.Add("KPZT", "N");
                                        dictionary3.Add("BZ", saleBill.BZ);
                                        dictionary3.Add("FHR", saleBill.FHR);
                                        dictionary3.Add("SKR", saleBill.SKR);
                                        dictionary3.Add("QDHSPMC", saleBill.QDHSPMC);
                                        dictionary3.Add("XFYHZH", saleBill.XFYHZH.Trim());
                                        dictionary3.Add("XFDZDH", saleBill.XFDZDH);
                                        dictionary3.Add("CFHB", saleBill.CFHB);
                                        dictionary3.Add("DJZL", saleBill.DJZL);
                                        dictionary3.Add("SFZJY", saleBill.SFZJY);
                                        dictionary3.Add("HYSY", saleBill.HYSY);
                                        dictionary3.Add("CM", saleBill.CM);
                                        dictionary3.Add("DLGRQ", saleBill.DLGRQ);
                                        dictionary3.Add("KHYHMC", saleBill.KHYHMC);
                                        dictionary3.Add("KHYHZH", saleBill.KHYHZH);
                                        dictionary3.Add("TYDH", saleBill.TYDH);
                                        dictionary3.Add("QYD", saleBill.QYD);
                                        dictionary3.Add("ZHD", saleBill.ZHD);
                                        dictionary3.Add("XHD", saleBill.XHD);
                                        dictionary3.Add("MDD", saleBill.MDD);
                                        dictionary3.Add("XFDZ", saleBill.XFDZ);
                                        dictionary3.Add("XFDH", saleBill.XFDH);
                                        dictionary3.Add("YSHWXX", saleBill.YSHWXX);
                                        dictionary3.Add("SCCJMC", saleBill.SCCJMC);
                                        dictionary3.Add("SLV", saleBill.SLV);
                                        dictionary3.Add("DW", saleBill.DW);
                                        dictionary3.Add("FLBM", saleBill.JDC_FLBM);
                                        dictionary3.Add("FLMC", saleBill.JDC_FLMC);
                                        dictionary3.Add("XSYH", saleBill.JDC_XSYH);
                                        dictionary3.Add("XSYHSM", saleBill.JDC_XSYHSM);
                                        dictionary3.Add("CLBM", saleBill.JDC_CLBM);
                                        dictionary3.Add("LSLVBS", saleBill.JDC_LSLVBS);
                                        dictionary3.Add("JZ_50_15", saleBill.JZ_50_15);
                                        list2.Add("aisino.Fwkp.Wbjk.XSDJAdds");
                                        list3.Add(dictionary3);
                                        list4.Add(saleBill);
                                        errorAnalyse.SaveCount++;
                                    }
                                }
                                else
                                {
                                    errorAnalyse.AbandonCount++;
                                }
                            }
                            if ((list2.Count > 0) && (list3.Count > 0))
                            {
                                baseDAO.updateSQLTransaction(list2.ToArray(), list3);
                            }
                        }
                    }
                    catch (Exception exception1)
                    {
                        string str4 = exception1.ToString();
                        throw;
                    }
                }
                finally
                {
                }
            }
            catch (Exception exception2)
            {
                Exception ex = exception2;
                if (ex.ToString().Contains("超时"))
                {
                    this.log.Error(ex.ToString());
                }
                else
                {
                    HandleException.HandleError(ex);
                }
            }
            finally
            {
                Thread.Sleep(100);
                MessageHelper.MsgWait();
            }
        }

        public string SaveSaleBill(SaleBill bill)
        {
            List<string> list = new List<string>();
            List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
            double num = 0.0;
            Dictionary<string, object> item = new Dictionary<string, object>();
            item.Add("BH", bill.BH);
            list.Add("aisino.Fwkp.Wbjk.XSDJMXDeleteMX");
            list2.Add(item);
            foreach (Goods goods in bill.ListGoods)
            {
                if (goods.FPZL.Trim() == "")
                {
                    goods.FPZL = bill.DJZL;
                }
                num += goods.JE;
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("XSDJBH", goods.XSDJBH);
                dictionary2.Add("XH", goods.XH);
                dictionary2.Add("SL", goods.SL);
                dictionary2.Add("DJ", goods.DJ);
                dictionary2.Add("JE", goods.JE);
                dictionary2.Add("SLV", goods.SLV);
                dictionary2.Add("SE", goods.SE);
                dictionary2.Add("SPMC", goods.SPMC);
                dictionary2.Add("SPSM", goods.SPSM);
                dictionary2.Add("GGXH", goods.GGXH);
                dictionary2.Add("JLDW", goods.JLDW);
                dictionary2.Add("HSJBZ", goods.HSJBZ);
                dictionary2.Add("DJHXZ", goods.DJHXZ);
                dictionary2.Add("FPZL", goods.FPZL);
                dictionary2.Add("FPDM", goods.FPDM);
                dictionary2.Add("FPHM", goods.FPHM);
                dictionary2.Add("SCFPXH", goods.SCFPXH);
                dictionary2.Add("FLBM", goods.FLBM);
                dictionary2.Add("FLMC", goods.FLMC);
                dictionary2.Add("XSYH", goods.XSYH);
                dictionary2.Add("XSYHSM", goods.XSYHSM);
                dictionary2.Add("SPBM", goods.SPBM);
                dictionary2.Add("LSLVBS", goods.LSLVBS);
                dictionary2.Add("KCE", goods.KCE);
                list.Add("aisino.Fwkp.Wbjk.XSDJMXAdd");
                list2.Add(dictionary2);
            }
            Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
            dictionary3.Add("BH", bill.BH);
            dictionary3.Add("GFMC", bill.GFMC);
            dictionary3.Add("GFSH", bill.GFSH);
            dictionary3.Add("GFDZDH", bill.GFDZDH);
            dictionary3.Add("GFYHZH", bill.GFYHZH);
            dictionary3.Add("XSBM", bill.XSBM);
            dictionary3.Add("YDXS", bill.YDXS);
            dictionary3.Add("DJRQ", bill.DJRQ);
            dictionary3.Add("DJYF", bill.DJYF);
            dictionary3.Add("BZ", bill.BZ);
            dictionary3.Add("FHR", bill.FHR);
            dictionary3.Add("SKR", bill.SKR);
            dictionary3.Add("QDHSPMC", bill.QDHSPMC);
            dictionary3.Add("XFYHZH", bill.XFYHZH);
            dictionary3.Add("XFDZDH", bill.XFDZDH);
            dictionary3.Add("CFHB", bill.CFHB);
            dictionary3.Add("DJZL", bill.DJZL);
            dictionary3.Add("JEHJ", bill.JEHJ);
            dictionary3.Add("SFZJY", bill.SFZJY);
            dictionary3.Add("HYSY", bill.HYSY);
            dictionary3.Add("CM", bill.CM);
            dictionary3.Add("DLGRQ", bill.DLGRQ);
            dictionary3.Add("KHYHMC", bill.KHYHMC);
            dictionary3.Add("KHYHZH", bill.KHYHZH);
            dictionary3.Add("TYDH", bill.TYDH);
            dictionary3.Add("QYD", bill.QYD);
            dictionary3.Add("ZHD", bill.ZHD);
            dictionary3.Add("XHD", bill.XHD);
            dictionary3.Add("MDD", bill.MDD);
            dictionary3.Add("XFDZ", bill.XFDZ);
            dictionary3.Add("XFDH", bill.XFDH);
            dictionary3.Add("YSHWXX", bill.YSHWXX);
            dictionary3.Add("SCCJMC", bill.SCCJMC);
            dictionary3.Add("SLV", bill.SLV);
            dictionary3.Add("DW", bill.DW);
            dictionary3.Add("KPZT", bill.KPZT);
            dictionary3.Add("FLBM", bill.JDC_FLBM);
            dictionary3.Add("FLMC", bill.JDC_FLMC);
            dictionary3.Add("XSYH", bill.JDC_XSYH);
            dictionary3.Add("XSYHSM", bill.JDC_XSYHSM);
            dictionary3.Add("CLBM", bill.JDC_CLBM);
            dictionary3.Add("LSLVBS", bill.JDC_LSLVBS);
            dictionary3.Add("JZ_50_15", bill.JZ_50_15);
            list.Add("aisino.Fwkp.Wbjk.XSDJModify");
            list2.Add(dictionary3);
            if (baseDAO.updateSQL(list.ToArray(), list2) > 0)
            {
                return "0";
            }
            return "error";
        }

        public string SaveToRealTable(List<SaleBill> beforebill, List<SaleBill> afterbill, string YSBH)
        {
            try
            {
                for (int i = 0; i < beforebill.Count; i++)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("BH", beforebill[i].BH);
                    dictionary.Add("NewBH", YSBH);
                    int num2 = baseDAO.updateSQL("aisino.Fwkp.Wbjk.SaveToHyTable", dictionary);
                }
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                baseDAO.updateSQL("aisino.Fwkp.Wbjk.YLtoRealTable", dictionary2);
                return "0";
            }
            catch (Exception exception)
            {
                return ("从预览表转正存库Fail:" + exception.ToString() + " " + exception.InnerException.ToString());
            }
        }

        public string SaveToTempTable(List<SaleBill> billList, int type = 0)
        {
            try
            {
                int num = 0;
                while (num < billList.Count)
                {
                    string bH = billList[num].BH;
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Clear();
                    dictionary.Add("BH", bH);
                    ArrayList list = baseDAO.querySQL("aisino.Fwkp.Wbjk.CX_XSDJ_BH", dictionary);
                    if ((list != null) && (list.Count > 0))
                    {
                        if (type == 1)
                        {
                            return "拆分后的单据号和原始单据号有重复，请删除原始单据，重新进行拆分！";
                        }
                        if (type == 2)
                        {
                            return "合并后的单据号和原始单据号有重复，请删除原始单据，重新进行合并！";
                        }
                    }
                    list.Clear();
                    list = baseDAO.querySQL("aisino.Fwkp.Wbjk.XSDJ_HY_BH", dictionary);
                    if ((list != null) && (list.Count > 0))
                    {
                        if (type == 1)
                        {
                            return "拆分后的单据号有重复，请删除单据，重新进行拆分！";
                        }
                        if (type == 2)
                        {
                            return "合并后的单据号有重复，请删除单据，重新进行合并！";
                        }
                    }
                    num++;
                }
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                baseDAO.updateSQL("aisino.Fwkp.Wbjk.CFclearYL", dictionary2);
                List<string> list2 = new List<string>();
                List<Dictionary<string, object>> list3 = new List<Dictionary<string, object>>();
                for (int i = 0; i < billList.Count; i++)
                {
                    SaleBill bill = billList[i];
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    item.Add("BH", bill.BH);
                    item.Add("GFMC", bill.GFMC);
                    item.Add("GFSH", bill.GFSH);
                    item.Add("GFDZDH", bill.GFDZDH);
                    item.Add("GFYHZH", bill.GFYHZH);
                    item.Add("XSBM", bill.XSBM);
                    item.Add("YDXS", bill.YDXS);
                    item.Add("JEHJ", bill.JEHJ);
                    item.Add("DJRQ", bill.DJRQ);
                    item.Add("DJYF", bill.DJYF);
                    item.Add("DJZT", bill.DJZT);
                    item.Add("KPZT", bill.KPZT);
                    item.Add("BZ", bill.BZ);
                    item.Add("FHR", bill.FHR);
                    item.Add("SKR", bill.SKR);
                    item.Add("QDHSPMC", bill.QDHSPMC);
                    item.Add("XFYHZH", bill.XFYHZH);
                    item.Add("XFDZDH", bill.XFDZDH);
                    item.Add("CFHB", bill.CFHB);
                    item.Add("DJZL", bill.DJZL);
                    item.Add("SFZJY", bill.SFZJY);
                    item.Add("HYSY", bill.HYSY);
                    item.Add("JZ_50_15", bill.JZ_50_15);
                    list2.Add("aisino.Fwkp.Wbjk.XSDJAddYL");
                    list3.Add(item);
                    for (num = 0; num < bill.ListGoods.Count; num++)
                    {
                        Goods goods = bill.ListGoods[num];
                        Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                        dictionary4.Add("XSDJBH", goods.XSDJBH);
                        dictionary4.Add("XH", goods.XH);
                        dictionary4.Add("SL", goods.SL);
                        dictionary4.Add("DJ", goods.DJ);
                        dictionary4.Add("JE", goods.JE);
                        dictionary4.Add("SLV", goods.SLV);
                        dictionary4.Add("SE", goods.SE);
                        dictionary4.Add("SPMC", goods.SPMC);
                        dictionary4.Add("SPSM", goods.SPSM);
                        dictionary4.Add("GGXH", goods.GGXH);
                        dictionary4.Add("JLDW", goods.JLDW);
                        dictionary4.Add("HSJBZ", goods.HSJBZ);
                        dictionary4.Add("DJHXZ", goods.DJHXZ);
                        dictionary4.Add("FPZL", goods.FPZL);
                        dictionary4.Add("FPDM", goods.FPDM);
                        dictionary4.Add("FPHM", goods.FPHM);
                        dictionary4.Add("SCFPXH", goods.SCFPXH);
                        dictionary4.Add("FLBM", goods.FLBM);
                        dictionary4.Add("XSYH", goods.XSYH);
                        dictionary4.Add("FLMC", goods.FLMC);
                        dictionary4.Add("SPBM", goods.SPBM);
                        dictionary4.Add("XSYHSM", goods.XSYHSM);
                        dictionary4.Add("LSLVBS", goods.LSLVBS);
                        dictionary4.Add("KCE", goods.KCE);
                        list2.Add("aisino.Fwkp.Wbjk.HBSaveInMX_YL");
                        list3.Add(dictionary4);
                    }
                }
                if ((list2.Count > 0) && (list3.Count > 0))
                {
                    baseDAO.updateSQLTransaction(list2.ToArray(), list3);
                }
                return "0";
            }
            catch (Exception exception)
            {
                return ("Fail:" + exception.ToString() + " " + exception.InnerException.ToString());
            }
        }

        public int UpdateDJZT(string XSDJBH)
        {
            int num2;
            try
            {
                Dictionary<string, object> dictionary2;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("BH", XSDJBH);
                string str = baseDAO.queryValueSQL<string>("aisino.Fwkp.Wbjk.XSDJYLselectDJZT", dictionary);
                int num = 0;
                switch (str)
                {
                    case "N":
                        dictionary2 = new Dictionary<string, object>();
                        dictionary2.Add("DJZT", "Y");
                        dictionary2.Add("BH", XSDJBH);
                        num = baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJYLupdateDJZT", dictionary2);
                        break;

                    case "Y":
                        dictionary2 = new Dictionary<string, object>();
                        dictionary2.Add("DJZT", "N");
                        dictionary2.Add("BH", XSDJBH);
                        num = baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJYLupdateDJZT", dictionary2);
                        break;
                }
                num2 = num;
            }
            catch (Exception)
            {
                throw;
            }
            return num2;
        }

        public int UpdateDJZT(string XSDJBH, string djzt)
        {
            int num2;
            try
            {
                int num = 0;
                if ((djzt == "N") || (djzt == "Y"))
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("DJZT", djzt);
                    dictionary.Add("BH", XSDJBH);
                    num = baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJYLupdateDJZT", dictionary);
                }
                num2 = num;
            }
            catch (Exception)
            {
                throw;
            }
            return num2;
        }

        public string WasteSaleBill(string BH, bool IsRight)
        {
            string str4;
            try
            {
                Dictionary<string, object> dictionary3;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("bh", BH);
                if (baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.XSDJExist", dictionary) == 0)
                {
                    return ("单据:" + BH + "不存在");
                }
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("BH", BH);
                string str2 = baseDAO.queryValueSQL<string>("aisino.Fwkp.Wbjk.XSDJYLselectDJZT", dictionary2);
                int num2 = 0;
                if (str2 == "W")
                {
                    string str3 = IsRight ? "Y" : "N";
                    dictionary3 = new Dictionary<string, object>();
                    dictionary3.Add("DJZT", str3);
                    dictionary3.Add("BH", BH);
                    num2 = baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJYLupdateDJZT", dictionary3);
                }
                else
                {
                    dictionary3 = new Dictionary<string, object>();
                    dictionary3.Add("DJZT", "W");
                    dictionary3.Add("BH", BH);
                    num2 = baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJYLupdateDJZT", dictionary3);
                }
                str4 = "0";
            }
            catch
            {
                throw;
            }
            return str4;
        }
    }
}

