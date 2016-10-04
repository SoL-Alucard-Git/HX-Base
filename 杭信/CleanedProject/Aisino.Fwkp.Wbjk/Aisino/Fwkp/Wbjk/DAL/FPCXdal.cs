namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public class FPCXdal
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<FPCXdal>();
        private string SQLID = "";

        public ArrayList GetAllCustomers()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            try
            {
                list2 = this.baseDAO.querySQL("aisino.Fwkp.Wbjk.SelectBM_KH", dictionary);
                for (int i = 0; i < list2.Count; i++)
                {
                    dictionary2 = list2[i] as Dictionary<string, object>;
                    list.Add(dictionary2["MC"].ToString());
                }
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return list;
        }

        public ArrayList GetGHDW()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            try
            {
                list2 = this.baseDAO.querySQL("aisino.Fwkp.Wbjk.SelectBM_GHDW", dictionary);
                for (int i = 0; i < list2.Count; i++)
                {
                    dictionary2 = list2[i] as Dictionary<string, object>;
                    list.Add(dictionary2["MC"].ToString());
                }
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return list;
        }

        public AisinoDataSet GetInvoiceDetail(InvoiceQueryCondition QueryArgs, int nPageSize, int nPageNo)
        {
            AisinoDataSet set;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", QueryArgs.m_strInvType);
            dictionary.Add("ZFBZ", QueryArgs.m_strWasteFlag);
            dictionary.Add("GFSH", "%");
            dictionary.Add("GFMC", "%");
            dictionary.Add("FPDM", "%");
            dictionary.Add("FPHM", "%");
            dictionary.Add("XSDJBH", "%");
            if (QueryArgs.m_strBuyerCodeList.Count > 0)
            {
                dictionary["GFSH"] = QueryArgs.m_strBuyerCodeList;
            }
            if (QueryArgs.m_strBuyerNameList.Count > 0)
            {
                dictionary["GFMC"] = QueryArgs.m_strBuyerNameList;
            }
            if (QueryArgs.m_strInvCodeList.Count > 0)
            {
                dictionary["FPDM"] = QueryArgs.m_strInvCodeList;
            }
            if (QueryArgs.m_strInvNumList.Count > 0)
            {
                dictionary["FPHM"] = QueryArgs.m_strInvNumList;
            }
            if (QueryArgs.m_strBillCodeList.Count > 0)
            {
                dictionary["XSDJBH"] = QueryArgs.m_strBillCodeList;
            }
            if (QueryArgs.m_dtStart == Convert.ToDateTime("0001-01-01"))
            {
                this.SQLID = "aisino.Fwkp.Wbjk.InvQueryAllDateXXFP";
            }
            else
            {
                dictionary.Add("BEGINDATE", QueryArgs.m_dtStart);
                dictionary.Add("ENDDATE", QueryArgs.m_dtEnd);
                this.SQLID = "aisino.Fwkp.Wbjk.InvQueryTheDateXXFP";
            }
            try
            {
                set = this.baseDAO.querySQLDataSet(this.SQLID, dictionary, nPageSize, nPageNo);
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return set;
        }

        public XXFPPaper GetInvPaper(string FPZL, string FPDM, string FPHM, bool GetInvQD)
        {
            XXFPPaper paper2;
            try
            {
                double sLV;
                Dictionary<string, object> dictionary2;
                int num2;
                XXFP_MXModel model;
                string str3;
                double num3;
                string str4;
                double num4;
                string str5;
                double num5;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("FPZL", FPZL);
                dictionary.Add("FPDM", FPDM);
                dictionary.Add("FPHM", FPHM);
                DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.GetInfoXXFP", dictionary);
                XXFPPaper paper = new XXFPPaper();
                if (table.Rows.Count > 0)
                {
                    string str;
                    byte[] buffer;
                    DataRow row = table.Rows[0];
                    paper.FPZL = GetSafeData.ValidateValue<string>(row, "FPZL");
                    paper.FPDM = GetSafeData.ValidateValue<string>(row, "FPDM");
                    paper.FPHM = (long) GetSafeData.ValidateValue<int>(row, "FPHM");
                    if ((paper.FPZL == "s") || (paper.FPZL == "c"))
                    {
                        paper.XSDJBH = GetSafeData.ValidateValue<string>(row, "XSDJBH");
                        paper.XFMC = GetSafeData.ValidateValue<string>(row, "XFMC");
                        paper.XFSH = GetSafeData.ValidateValue<string>(row, "XFSH");
                        paper.GFMC = GetSafeData.ValidateValue<string>(row, "GFMC");
                        paper.GFSH = GetSafeData.ValidateValue<string>(row, "GFSH");
                        paper.GFYHZH = GetSafeData.ValidateValue<string>(row, "GFYHZH");
                        paper.GFDZDH = GetSafeData.ValidateValue<string>(row, "GFDZDH");
                        paper.XFDZDH = GetSafeData.ValidateValue<string>(row, "XFDZDH");
                        paper.XFYHZH = GetSafeData.ValidateValue<string>(row, "XFYHZH");
                        str = GetSafeData.ValidateValue<string>(row, "BZ");
                        if ((str == null) || (str == ""))
                        {
                            str = "";
                        }
                        else
                        {
                            buffer = Convert.FromBase64String(str);
                            if ((buffer != null) && (buffer.Length >= 1))
                            {
                                str = ToolUtil.GetString(buffer);
                            }
                            else
                            {
                                str = "";
                            }
                        }
                        paper.BZ = str;
                        paper.SKR = GetSafeData.ValidateValue<string>(row, "SKR");
                        paper.FHR = GetSafeData.ValidateValue<string>(row, "FHR");
                        paper.KPRQ = GetSafeData.ValidateValue<DateTime>(row, "KPRQ");
                        paper.KPR = GetSafeData.ValidateValue<string>(row, "KPR");
                        paper.HJSE = GetSafeData.ValidateValue<double>(row, "HJSE");
                        paper.HJJE = GetSafeData.ValidateValue<double>(row, "HJJE");
                        paper.SLV = GetSafeData.ValidateValue<double>(row, "SLV");
                        paper.ZFBZ = GetSafeData.ValidateValue<bool>(row, "ZFBZ");
                        paper.QDBZ = GetSafeData.ValidateValue<bool>(row, "QDBZ");
                    }
                    else if (paper.FPZL == "f")
                    {
                        paper.XSDJBH = GetSafeData.ValidateValue<string>(row, "XSDJBH");
                        paper.HJJE = GetSafeData.ValidateValue<double>(row, "HJJE");
                        paper.SLV = GetSafeData.ValidateValue<double>(row, "SLV");
                        paper.HJSE = GetSafeData.ValidateValue<double>(row, "HJSE");
                        paper.GFMC = GetSafeData.ValidateValue<string>(row, "GFMC");
                        paper.GFSH = GetSafeData.ValidateValue<string>(row, "GFSH");
                        paper.SHRMC = GetSafeData.ValidateValue<string>(row, "GFDZDH");
                        paper.SHRSH = GetSafeData.ValidateValue<string>(row, "CM");
                        paper.FHRMC = GetSafeData.ValidateValue<string>(row, "XFDZDH");
                        paper.FHRSH = GetSafeData.ValidateValue<string>(row, "TYDH");
                        paper.XFMC = GetSafeData.ValidateValue<string>(row, "XFMC");
                        paper.XFSH = GetSafeData.ValidateValue<string>(row, "XFSH");
                        paper.QYJYMD = GetSafeData.ValidateValue<string>(row, "XFYHZH");
                        paper.CZCH = GetSafeData.ValidateValue<string>(row, "QYD");
                        paper.CCDW = GetSafeData.ValidateValue<string>(row, "YYZZH");
                        paper.KPR = GetSafeData.ValidateValue<string>(row, "KPR");
                        paper.FHR = GetSafeData.ValidateValue<string>(row, "FHR");
                        paper.SKR = GetSafeData.ValidateValue<string>(row, "SKR");
                        paper.ZGSWMC = GetSafeData.ValidateValue<string>(row, "GFYHZH");
                        paper.ZGSWDM = GetSafeData.ValidateValue<string>(row, "HYBM");
                        paper.JQBH = GetSafeData.ValidateValue<string>(row, "JQBH");
                        paper.ZFBZ = GetSafeData.ValidateValue<bool>(row, "ZFBZ");
                        string s = GetSafeData.ValidateValue<string>(row, "YSHWXX");
                        if ((s == null) || (s == ""))
                        {
                            paper.YSHWXX = "";
                        }
                        else
                        {
                            buffer = Convert.FromBase64String(s);
                            if ((buffer != null) && (buffer.Length >= 1))
                            {
                                s = ToolUtil.GetString(buffer);
                            }
                            else
                            {
                                s = "";
                            }
                        }
                        paper.YSHWXX = s;
                        str = GetSafeData.ValidateValue<string>(row, "BZ");
                        if ((str == null) || (str == ""))
                        {
                            paper.BZ = "";
                        }
                        else
                        {
                            buffer = Convert.FromBase64String(str);
                            if ((buffer != null) && (buffer.Length >= 1))
                            {
                                str = ToolUtil.GetString(buffer);
                            }
                            else
                            {
                                str = "";
                            }
                        }
                        paper.BZ = str;
                    }
                    else if (paper.FPZL == "j")
                    {
                        paper.XSDJBH = GetSafeData.ValidateValue<string>(row, "XSDJBH");
                        paper.HJJE = GetSafeData.ValidateValue<double>(row, "HJJE");
                        paper.SLV = GetSafeData.ValidateValue<double>(row, "SLV");
                        paper.HJSE = GetSafeData.ValidateValue<double>(row, "HJSE");
                        paper.GFMC = GetSafeData.ValidateValue<string>(row, "GFMC");
                        paper.GFSH = GetSafeData.ValidateValue<string>(row, "GFSH");
                        paper.SFZHM = GetSafeData.ValidateValue<string>(row, "XSBM");
                        paper.CLLX = GetSafeData.ValidateValue<string>(row, "GFDZDH");
                        paper.CPXH = GetSafeData.ValidateValue<string>(row, "XFDZ");
                        paper.CD = GetSafeData.ValidateValue<string>(row, "KHYHMC");
                        paper.SCCJMC = GetSafeData.ValidateValue<string>(row, "SCCJMC");
                        paper.HGZH = GetSafeData.ValidateValue<string>(row, "CM");
                        paper.JKZMSH = GetSafeData.ValidateValue<string>(row, "TYDH");
                        paper.SJDH = GetSafeData.ValidateValue<string>(row, "QYD");
                        paper.FDJH = GetSafeData.ValidateValue<string>(row, "ZHD");
                        paper.CLSBDH = GetSafeData.ValidateValue<string>(row, "XHD");
                        paper.XFMC = GetSafeData.ValidateValue<string>(row, "XFMC");
                        paper.XFSH = GetSafeData.ValidateValue<string>(row, "XFSH");
                        paper.DH = GetSafeData.ValidateValue<string>(row, "XFDH");
                        paper.ZH = GetSafeData.ValidateValue<string>(row, "KHYHZH");
                        paper.DZ = GetSafeData.ValidateValue<string>(row, "XFDZDH");
                        paper.KHYH = GetSafeData.ValidateValue<string>(row, "XFYHZH");
                        paper.DW = GetSafeData.ValidateValue<string>(row, "YYZZH");
                        paper.XCRS = GetSafeData.ValidateValue<string>(row, "MDD");
                        paper.KPR = GetSafeData.ValidateValue<string>(row, "KPR");
                        paper.ZGSWMC = GetSafeData.ValidateValue<string>(row, "GFYHZH");
                        paper.ZGSWDM = GetSafeData.ValidateValue<string>(row, "HYBM");
                        paper.JQBH = GetSafeData.ValidateValue<string>(row, "JQBH");
                        paper.ZFBZ = GetSafeData.ValidateValue<bool>(row, "ZFBZ");
                        paper.ZYSPMC = row["ZYSPMC"].ToString().Trim();
                        paper.SPSM = row["SPSM_BM"].ToString().Trim();
                        paper.SKR = row["SKR"].ToString().Trim();
                        str = GetSafeData.ValidateValue<string>(row, "BZ");
                        if ((str == null) || (str == ""))
                        {
                            paper.BZ = "";
                        }
                        else
                        {
                            buffer = Convert.FromBase64String(str);
                            if ((buffer != null) && (buffer.Length >= 1))
                            {
                                str = ToolUtil.GetString(buffer);
                            }
                            else
                            {
                                str = "";
                            }
                        }
                        paper.BZ = str;
                    }
                }
                bool flag = false;
                bool flag2 = false;
                ArrayList list = this.baseDAO.querySQL("aisino.Fwkp.Wbjk.FPCCGetFaPiaoMX", dictionary);
                if (list.Count > 0)
                {
                    sLV = 0.0;
                    dictionary2 = new Dictionary<string, object>();
                    for (num2 = 0; num2 < list.Count; num2++)
                    {
                        dictionary2 = list[num2] as Dictionary<string, object>;
                        model = new XXFP_MXModel {
                            SPMC = dictionary2["SPMC"].ToString(),
                            GGXH = dictionary2["GGXH"].ToString(),
                            JLDW = dictionary2["JLDW"].ToString()
                        };
                        str3 = dictionary2["SL"].ToString();
                        num3 = 0.0;
                        model.SL = new double?(double.TryParse(str3, out num3) ? num3 : 0.0);
                        str4 = dictionary2["DJ"].ToString();
                        num4 = 0.0;
                        model.DJ = new double?(double.TryParse(str4, out num4) ? num4 : 0.0);
                        model.JE = Convert.ToDouble(dictionary2["JE"]);
                        str5 = dictionary2["SLV"].ToString();
                        num5 = 0.0;
                        model.SLV = double.TryParse(str5, out num5) ? num5 : 0.0;
                        model.SE = Convert.ToDouble(dictionary2["SE"]);
                        model.SPSM = dictionary2["SPSM"].ToString();
                        model.FPHXZ = Convert.ToInt32(dictionary2["FPHXZ"]);
                        model.HSJBZ = Convert.ToBoolean(dictionary2["HSJBZ"]);
                        paper.ListInvWare.Add(model);
                        if ((num2 > 0) && !(sLV == model.SLV))
                        {
                            flag = true;
                        }
                        sLV = model.SLV;
                        model.FLBM = dictionary2["FLBM"].ToString().Trim();
                        model.XSYH = false;
                        if (dictionary2["XSYH"].ToString().Trim() == "1")
                        {
                            model.XSYH = true;
                        }
                    }
                }
                ArrayList list2 = this.baseDAO.querySQL("aisino.Fwkp.Wbjk.FPCCGetFaPiaoQD", dictionary);
                if (list2.Count > 0)
                {
                    sLV = 0.0;
                    dictionary2 = new Dictionary<string, object>();
                    for (num2 = 0; num2 < list2.Count; num2++)
                    {
                        dictionary2 = list2[num2] as Dictionary<string, object>;
                        model = new XXFP_MXModel {
                            SPMC = dictionary2["SPMC"].ToString(),
                            GGXH = dictionary2["GGXH"].ToString(),
                            JLDW = dictionary2["JLDW"].ToString()
                        };
                        str3 = dictionary2["SL"].ToString();
                        num3 = 0.0;
                        model.SL = new double?(double.TryParse(str3, out num3) ? num3 : 0.0);
                        str4 = dictionary2["DJ"].ToString();
                        num4 = 0.0;
                        model.DJ = new double?(double.TryParse(str4, out num4) ? num4 : 0.0);
                        model.JE = Convert.ToDouble(dictionary2["JE"]);
                        str5 = dictionary2["SLV"].ToString();
                        num5 = 0.0;
                        model.SLV = double.TryParse(str5, out num5) ? num5 : 0.0;
                        model.SE = Convert.ToDouble(dictionary2["SE"]);
                        model.SPSM = dictionary2["SPSM"].ToString();
                        model.FPHXZ = Convert.ToInt32(dictionary2["FPHXZ"]);
                        model.HSJBZ = Convert.ToBoolean(dictionary2["HSJBZ"]);
                        model.FLBM = dictionary2["FLBM"].ToString().Trim();
                        model.XSYH = false;
                        if (dictionary2["XSYH"].ToString().Trim() == "1")
                        {
                            model.XSYH = true;
                        }
                        model.LSLVBS = dictionary2["LSLVBS"].ToString().Trim();
                        if (GetInvQD)
                        {
                            paper.ListInvWare.Add(model);
                        }
                        if ((num2 > 0) && !(sLV == model.SLV))
                        {
                            flag2 = true;
                        }
                        sLV = model.SLV;
                    }
                }
                if (flag || flag2)
                {
                    paper.MULSLV = true;
                }
                paper2 = paper;
            }
            catch (Exception)
            {
                string message = string.Format("FPZL={0},FPDM={1},FPHM={2}", FPZL, FPDM, FPHM);
                HandleException.Log.Error(message);
                throw;
            }
            return paper2;
        }

        public ArrayList GetSPFMC()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            try
            {
                list2 = this.baseDAO.querySQL("aisino.Fwkp.Wbjk.SelectBM_SFHR", dictionary);
                for (int i = 0; i < list2.Count; i++)
                {
                    dictionary2 = list2[i] as Dictionary<string, object>;
                    list.Add(dictionary2["MC"].ToString());
                }
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return list;
        }

        public AisinoDataSet QueryFaPiao(FaPiaoQueryArgs QueryArgs, int pagesize, int pageno)
        {
            AisinoDataSet set;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPDM", QueryArgs.FPDM + "%");
            dictionary.Add("FPHM", QueryArgs.FPHM + "%");
            dictionary.Add("GFMC", QueryArgs.GFMC + "%");
            dictionary.Add("GFSH", QueryArgs.GFSH + "%");
            dictionary.Add("DJBH", QueryArgs.DJBH + "%");
            dictionary.Add("QSRQ", QueryArgs.StartTime);
            dictionary.Add("JZRQ", QueryArgs.EndTime);
            dictionary.Add("FPZL", QueryArgs.FPZL);
            dictionary.Add("ZFBZ", QueryArgs.ZFBZ);
            if (QueryArgs.StartTime.ToShortDateString() == "1753-1-1")
            {
                this.SQLID = "aisino.Fwkp.Wbjk.FPCXQueryAllDate";
            }
            else
            {
                this.SQLID = "aisino.Fwkp.Wbjk.FPCXQuery";
            }
            try
            {
                set = this.baseDAO.querySQLDataSet(this.SQLID, dictionary, pagesize, pageno);
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return set;
        }

        public DataTable QueryGetFaPiao(InvoiceQueryCondition QueryArgs)
        {
            DataTable table;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", QueryArgs.m_strInvType);
            dictionary.Add("ZFBZ", QueryArgs.m_strWasteFlag);
            dictionary.Add("GFSH", "%");
            dictionary.Add("GFMC", "%");
            dictionary.Add("FPDM", "%");
            dictionary.Add("FPHM", "%");
            dictionary.Add("XSDJBH", "%");
            if (QueryArgs.m_strBuyerCodeList.Count > 0)
            {
                dictionary["GFSH"] = QueryArgs.m_strBuyerCodeList;
            }
            if (QueryArgs.m_strBuyerNameList.Count > 0)
            {
                dictionary["GFMC"] = QueryArgs.m_strBuyerNameList;
            }
            if (QueryArgs.m_strInvCodeList.Count > 0)
            {
                dictionary["FPDM"] = QueryArgs.m_strInvCodeList;
            }
            if (QueryArgs.m_strInvNumList.Count > 0)
            {
                dictionary["FPHM"] = QueryArgs.m_strInvNumList;
            }
            if (QueryArgs.m_strBillCodeList.Count > 0)
            {
                dictionary["XSDJBH"] = QueryArgs.m_strBillCodeList;
            }
            if (QueryArgs.m_dtStart == Convert.ToDateTime("0001-01-01"))
            {
                this.SQLID = "aisino.Fwkp.Wbjk.InvQueryAllDateExportXXFP";
            }
            else
            {
                dictionary.Add("BEGINDATE", QueryArgs.m_dtStart);
                dictionary.Add("ENDDATE", QueryArgs.m_dtEnd);
                this.SQLID = "aisino.Fwkp.Wbjk.InvQueryTheDateExportXXFP";
            }
            try
            {
                table = this.baseDAO.querySQLDataTable(this.SQLID, dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return table;
        }

        public DataTable QueryGetFaPiao(FaPiaoQueryArgs QueryArgs)
        {
            DataTable table;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPDM", "%" + QueryArgs.FPDM + "%");
            dictionary.Add("FPHM", "%" + QueryArgs.FPHM + "%");
            dictionary.Add("GFMC", "%" + QueryArgs.GFMC + "%");
            dictionary.Add("GFSH", "%" + QueryArgs.GFSH + "%");
            dictionary.Add("DJBH", "%" + QueryArgs.DJBH + "%");
            dictionary.Add("QSRQ", QueryArgs.StartTime);
            dictionary.Add("JZRQ", QueryArgs.EndTime);
            dictionary.Add("FPZL", QueryArgs.FPZL);
            dictionary.Add("ZFBZ", QueryArgs.ZFBZ);
            if (QueryArgs.StartTime.ToShortDateString() == "1753-1-1")
            {
                this.SQLID = "aisino.Fwkp.Wbjk.FPCXQueryGetAllDate";
            }
            else
            {
                this.SQLID = "aisino.Fwkp.Wbjk.FPCXQueryGet";
            }
            try
            {
                table = this.baseDAO.querySQLDataTable(this.SQLID, dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return table;
        }

        public DataTable QueryGetFaPiaoxml(InvoiceQueryCondition QueryArgs)
        {
            DataTable table;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", QueryArgs.m_strInvType);
            dictionary.Add("ZFBZ", QueryArgs.m_strWasteFlag);
            dictionary.Add("GFSH", "%");
            dictionary.Add("GFMC", "%");
            dictionary.Add("FPDM", "%");
            dictionary.Add("FPHM", "%");
            dictionary.Add("XSDJBH", "%");
            if (QueryArgs.m_strBuyerCodeList.Count > 0)
            {
                dictionary["GFSH"] = QueryArgs.m_strBuyerCodeList;
            }
            if (QueryArgs.m_strBuyerNameList.Count > 0)
            {
                dictionary["GFMC"] = QueryArgs.m_strBuyerNameList;
            }
            if (QueryArgs.m_strInvCodeList.Count > 0)
            {
                dictionary["FPDM"] = QueryArgs.m_strInvCodeList;
            }
            if (QueryArgs.m_strInvNumList.Count > 0)
            {
                dictionary["FPHM"] = QueryArgs.m_strInvNumList;
            }
            if (QueryArgs.m_strBillCodeList.Count > 0)
            {
                dictionary["XSDJBH"] = QueryArgs.m_strBillCodeList;
            }
            if (QueryArgs.m_dtStart == Convert.ToDateTime("0001-01-01"))
            {
                this.SQLID = "aisino.Fwkp.Wbjk.InvQueryAllDateToXML";
            }
            else
            {
                dictionary.Add("QSRQ", QueryArgs.m_dtStart);
                dictionary.Add("JZRQ", QueryArgs.m_dtEnd);
                this.SQLID = "aisino.Fwkp.Wbjk.InvQueryTheDateToXML";
            }
            try
            {
                table = this.baseDAO.querySQLDataTable(this.SQLID, dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return table;
        }

        public DataTable QueryGetFaPiaoxml(FaPiaoQueryArgs QueryArgs)
        {
            DataTable table;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPDM", "%" + QueryArgs.FPDM + "%");
            dictionary.Add("FPHM", "%" + QueryArgs.FPHM + "%");
            dictionary.Add("GFMC", "%" + QueryArgs.GFMC + "%");
            dictionary.Add("GFSH", "%" + QueryArgs.GFSH + "%");
            dictionary.Add("DJBH", "%" + QueryArgs.DJBH + "%");
            dictionary.Add("QSRQ", QueryArgs.StartTime);
            dictionary.Add("JZRQ", QueryArgs.EndTime);
            dictionary.Add("FPZL", QueryArgs.FPZL);
            dictionary.Add("ZFBZ", QueryArgs.ZFBZ);
            if (QueryArgs.StartTime.ToShortDateString() == "1753-1-1")
            {
                this.SQLID = "aisino.Fwkp.Wbjk.FPCXQueryGetAllDateXML";
            }
            else
            {
                this.SQLID = "aisino.Fwkp.Wbjk.FPCXQueryGetXML";
            }
            try
            {
                table = this.baseDAO.querySQLDataTable(this.SQLID, dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return table;
        }
    }
}

