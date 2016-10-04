namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class DJXGdal
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private int CurrentPage = 1;
        private int PageSize = 0x19;

        public string AddXSDJ(XSDJMXModel xsdj)
        {
            try
            {
                double num = 0.0;
                foreach (XSDJ_MXModel model in xsdj.ListXSDJ_MX)
                {
                    num += model.JE;
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("XSDJBH", model.XSDJBH);
                    dictionary.Add("XH", model.XH);
                    dictionary.Add("SL", model.SL);
                    dictionary.Add("DJ", model.DJ);
                    dictionary.Add("JE", model.JE);
                    dictionary.Add("SLV", model.SLV);
                    dictionary.Add("SE", model.SE);
                    dictionary.Add("SPMC", model.SPMC);
                    dictionary.Add("SPSM", model.SPSM);
                    dictionary.Add("GGXH", model.GGXH);
                    dictionary.Add("JLDW", model.JLDW);
                    dictionary.Add("HSJBZ", model.HSJBZ);
                    dictionary.Add("DJHXZ", model.DJHXZ);
                    dictionary.Add("FPZL", xsdj.DJZL);
                    dictionary.Add("FPDM", null);
                    dictionary.Add("FPHM", null);
                    dictionary.Add("SCFPXH", null);
                    if (this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJMXAdd", dictionary) == 0)
                    {
                        break;
                    }
                }
                xsdj.JEHJ = num;
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
                if (this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJAdds", dictionary2) > 0)
                {
                    return "0";
                }
                return "error";
            }
            catch (Exception exception)
            {
                if ((exception.InnerException == null) || !exception.InnerException.Message.EndsWith("columns XSDJBH, XH are not unique"))
                {
                    throw;
                }
                return "e1";
            }
        }

        public string AllowImport(XSDJModel model)
        {
            string str = this.BeSpliOrColl(model.BH);
            if (str != "0")
            {
                return str;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bh", model.BH);
            switch (this.baseDAO.queryValueSQL<string>("aisino.Fwkp.Wbjk.XSDJKPState", dictionary))
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
            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.XSDJHYExist", dictionary) > 0)
            {
                str = this.baseDAO.queryValueSQL<string>("aisino.Fwkp.Wbjk.XSDJHY_YSBH", dictionary);
                if (str == XSDJBH)
                {
                    str = XSDJBH + "_0等";
                }
            }
            if (str != "")
            {
                return ("已经被拆分或者合并为" + str);
            }
            return "0";
        }

        public DataTable GetMXDJ(string XSDJBH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", XSDJBH);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJXGGetMX", dictionary);
        }

        public XSDJ_MXModel GetMXModel(string XSDJBH, string XH)
        {
            XSDJ_MXModel model = new XSDJ_MXModel();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", XSDJBH);
            dictionary.Add("XH", XH);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJMXGetModel", dictionary);
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                model.XSDJBH = GetSafeData.ValidateValue<string>(row, "XSDJBH");
                model.XH = GetSafeData.ValidateValue<int>(row, "XH");
                model.SL = GetSafeData.ValidateValue<double>(row, "SL");
                model.DJ = GetSafeData.ValidateValue<double>(row, "DJ");
                model.JE = GetSafeData.ValidateValue<double>(row, "JE");
                model.SLV = GetSafeData.ValidateValue<double>(row, "SLV");
                model.SE = GetSafeData.ValidateValue<double>(row, "SE");
                model.SPMC = GetSafeData.ValidateValue<string>(row, "SPMC");
                model.SPSM = GetSafeData.ValidateValue<string>(row, "SPSM");
                model.GGXH = GetSafeData.ValidateValue<string>(row, "GGXH");
                model.JLDW = GetSafeData.ValidateValue<string>(row, "JLDW");
                model.HSJBZ = GetSafeData.ValidateValue<bool>(row, "HSJBZ");
                model.DJHXZ = GetSafeData.ValidateValue<int>(row, "DJHXZ");
                model.FPZL = GetSafeData.ValidateValue<string>(row, "FPZL");
                model.FPDM = GetSafeData.ValidateValue<string>(row, "FPDM");
                model.FPHM = GetSafeData.ValidateValue<int>(row, "FPHM");
                model.SCFPXH = GetSafeData.ValidateValue<int>(row, "SCFPXH");
            }
            return model;
        }

        public XSDJMXModel GetXSDJandMXs(string XSDJBH, bool MXAll, bool DanJia)
        {
            XSDJMXModel model = new XSDJMXModel();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", XSDJBH);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.XSDJGetModel", dictionary);
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                model.BH = GetSafeData.ValidateValue<string>(row, "BH");
                model.GFMC = GetSafeData.ValidateValue<string>(row, "GFMC");
                model.GFSH = GetSafeData.ValidateValue<string>(row, "GFSH");
                model.GFDZDH = GetSafeData.ValidateValue<string>(row, "GFDZDH");
                model.GFYHZH = GetSafeData.ValidateValue<string>(row, "GFYHZH");
                model.XSBM = GetSafeData.ValidateValue<string>(row, "XSBM");
                model.YDXS = GetSafeData.ValidateValue<bool>(row, "YDXS");
                model.JEHJ = GetSafeData.ValidateValue<double>(row, "JEHJ");
                model.DJRQ = GetSafeData.ValidateValue<DateTime>(row, "DJRQ");
                model.DJYF = GetSafeData.ValidateValue<int>(row, "DJYF");
                model.DJZT = GetSafeData.ValidateValue<string>(row, "DJZT");
                model.KPZT = GetSafeData.ValidateValue<string>(row, "KPZT");
                model.BZ = GetSafeData.ValidateValue<string>(row, "BZ");
                model.FHR = GetSafeData.ValidateValue<string>(row, "FHR");
                model.SKR = GetSafeData.ValidateValue<string>(row, "SKR");
                model.QDHSPMC = GetSafeData.ValidateValue<string>(row, "QDHSPMC");
                model.XFYHZH = GetSafeData.ValidateValue<string>(row, "XFYHZH");
                model.XFDZDH = GetSafeData.ValidateValue<string>(row, "XFDZDH");
                model.CFHB = GetSafeData.ValidateValue<bool>(row, "CFHB");
                model.DJZL = GetSafeData.ValidateValue<string>(row, "DJZL");
                model.SFZJY = GetSafeData.ValidateValue<bool>(row, "SFZJY");
                model.HYSY = GetSafeData.ValidateValue<bool>(row, "HYSY");
            }
            else
            {
                return null;
            }
            DataTable mXDJ = this.GetMXDJ(XSDJBH);
            foreach (DataRow row in mXDJ.Rows)
            {
                XSDJ_MXModel item = new XSDJ_MXModel {
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
                    SCFPXH = GetSafeData.ValidateValue<int>(row, "SCFPXH")
                };
                if (!DanJia)
                {
                    bool flag = ((item.SLV == 0.05) && (model.DJZL == "s")) && model.HYSY;
                    if (item.DJHXZ == 4)
                    {
                        item.SL = 0.0;
                        item.DJ = 0.0;
                    }
                    else if (!flag && item.HSJBZ)
                    {
                        if (!(item.SL == 0.0))
                        {
                            item.DJ = item.JE / item.SL;
                        }
                        else
                        {
                            item.DJ = item.JE;
                        }
                    }
                }
                if (MXAll || string.IsNullOrEmpty(item.FPDM))
                {
                    model.ListXSDJ_MX.Add(item);
                }
            }
            return model;
        }

        public void HandleGoToPageEventArgs(GoToPageEventArgs e)
        {
            this.CurrentPage = e.get_PageNO();
            this.PageSize = e.get_PageSize();
            string str = this.CurrentPage.ToString();
            PropertyUtil.SetValue("WBJK_DJXG_DATAGRID", str);
        }

        public AisinoDataSet QueryXSDJ(string DJmonth, string DJtype)
        {
            int result = 1;
            int.TryParse(PropertyUtil.GetValue("WBJK_DJXG_DATAGRID"), out result);
            this.CurrentPage = result;
            return this.QueryXSDJ(DJmonth, DJtype, this.PageSize, this.CurrentPage);
        }

        public AisinoDataSet QueryXSDJ(string DJYear, string DJmonth, string DJtype)
        {
            int result = 1;
            int.TryParse(PropertyUtil.GetValue("WBJK_DJXG_DATAGRID"), out result);
            this.CurrentPage = result;
            return this.QueryXSDJ(DJYear, DJmonth, DJtype, this.PageSize, this.CurrentPage);
        }

        public AisinoDataSet QueryXSDJ(string DJmonth, string DJtype, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Clear();
            dictionary.Add("DJYF", DJmonth);
            dictionary.Add("DJZL", DJtype);
            if (DJmonth == "0")
            {
                if (CommonTool.isSPBMVersion())
                {
                    return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.XSDJGetAllMonth_FLBM", dictionary, pagesize, pageno);
                }
                return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.XSDJGetAllMonth", dictionary, pagesize, pageno);
            }
            if (CommonTool.isSPBMVersion())
            {
                return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.XSDJFenYe_FLBM", dictionary, pagesize, pageno);
            }
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.XSDJFenYe", dictionary, pagesize, pageno);
        }

        public AisinoDataSet QueryXSDJ(string DJYear, string DJMonth, string DJtype, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Clear();
            dictionary.Add("DJNF", DJYear);
            dictionary.Add("DJYF", DJMonth);
            dictionary.Add("DJZL", DJtype);
            if (DJMonth == "0")
            {
                if (CommonTool.isSPBMVersion())
                {
                    return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.XSDJGetAllMonth_FLBM", dictionary, pagesize, pageno);
                }
                return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.XSDJGetAllMonth", dictionary, pagesize, pageno);
            }
            if (CommonTool.isSPBMVersion())
            {
                return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.XSDJFenYeNY_FLBM", dictionary, pagesize, pageno);
            }
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.XSDJFenYeNY", dictionary, pagesize, pageno);
        }

        public void SaveImportXSDJ(List<XSDJMXModel> ListXSDJandMX, ErrorResolver Errors)
        {
            try
            {
                try
                {
                    for (int i = 0; i < ListXSDJandMX.Count; i++)
                    {
                        XSDJMXModel model = ListXSDJandMX[i];
                        string str = string.Format("销售单据编号: {0} ", model.BH);
                        string errowInfo = this.AllowImport(model);
                        if (errowInfo != "0")
                        {
                            Errors.AddError(errowInfo, model.BH, 0, false);
                            Errors.AbandonCount++;
                        }
                        else
                        {
                            Dictionary<string, object> dictionary = new Dictionary<string, object>();
                            dictionary.Add("bh", model.BH);
                            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.XSDJExist", dictionary) > 0)
                            {
                                int num3 = this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJDeleteFast", dictionary);
                            }
                            switch (this.AddXSDJ(model))
                            {
                                case "0":
                                    Errors.SaveCount++;
                                    break;
                            }
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
            finally
            {
            }
        }

        public int ZuoFeiDanJu(string BH, bool IsRight)
        {
            int num3;
            try
            {
                Dictionary<string, object> dictionary3;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("bh", BH);
                if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.XSDJExist", dictionary) == 0)
                {
                    return 0;
                }
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("BH", BH);
                string str = this.baseDAO.queryValueSQL<string>("aisino.Fwkp.Wbjk.XSDJYLselectDJZT", dictionary2);
                int num2 = 0;
                if (str == "W")
                {
                    string str2 = IsRight ? "Y" : "N";
                    dictionary3 = new Dictionary<string, object>();
                    dictionary3.Add("DJZT", str2);
                    dictionary3.Add("BH", BH);
                    num2 = this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJYLupdateDJZT", dictionary3);
                }
                else
                {
                    dictionary3 = new Dictionary<string, object>();
                    dictionary3.Add("DJZT", "W");
                    dictionary3.Add("BH", BH);
                    num2 = this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJYLupdateDJZT", dictionary3);
                }
                num3 = num2;
            }
            catch
            {
                throw;
            }
            return num3;
        }
    }
}

