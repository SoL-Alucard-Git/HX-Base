namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class DJCFdal
    {
        private int _currentPage = 1;
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();

        private string AddDJtoYL(XSDJModel xsdj)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", xsdj.BH);
            dictionary.Add("GFMC", xsdj.GFMC);
            dictionary.Add("GFSH", xsdj.GFSH);
            dictionary.Add("GFDZDH", xsdj.GFDZDH);
            dictionary.Add("GFYHZH", xsdj.GFYHZH);
            dictionary.Add("XSBM", xsdj.XSBM);
            dictionary.Add("YDXS", xsdj.YDXS);
            dictionary.Add("JEHJ", xsdj.JEHJ);
            dictionary.Add("DJRQ", xsdj.DJRQ);
            dictionary.Add("DJYF", xsdj.DJYF);
            dictionary.Add("DJZT", xsdj.DJZT);
            dictionary.Add("KPZT", xsdj.KPZT);
            dictionary.Add("BZ", xsdj.BZ);
            dictionary.Add("FHR", xsdj.FHR);
            dictionary.Add("SKR", xsdj.SKR);
            dictionary.Add("QDHSPMC", xsdj.QDHSPMC);
            dictionary.Add("XFYHZH", xsdj.XFYHZH);
            dictionary.Add("XFDZDH", xsdj.XFDZDH);
            dictionary.Add("CFHB", xsdj.CFHB);
            dictionary.Add("DJZL", xsdj.DJZL);
            dictionary.Add("SFZJY", xsdj.SFZJY);
            dictionary.Add("HYSY", xsdj.HYSY);
            if (this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJAddYL", dictionary) > 0)
            {
                return "OK";
            }
            return "eDJ";
        }

        private string AddMXtoYL(XSDJ_MXModel mingxi)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", mingxi.XSDJBH);
            dictionary.Add("XH", mingxi.XH);
            dictionary.Add("SL", mingxi.SL);
            dictionary.Add("DJ", mingxi.DJ);
            dictionary.Add("JE", mingxi.JE);
            dictionary.Add("SLV", mingxi.SLV);
            dictionary.Add("SE", mingxi.SE);
            dictionary.Add("SPMC", mingxi.SPMC);
            dictionary.Add("SPSM", mingxi.SPSM);
            dictionary.Add("GGXH", mingxi.GGXH);
            dictionary.Add("JLDW", mingxi.JLDW);
            dictionary.Add("HSJBZ", mingxi.HSJBZ);
            dictionary.Add("DJHXZ", mingxi.DJHXZ);
            if (this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJMXAddYL", dictionary) > 0)
            {
                return "OK";
            }
            return "eMX";
        }

        public DataTable GetCFHDanJu(string XSDJBH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BHT", XSDJBH + "_");
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.CFGetCFHDanJu", dictionary);
        }

        public DataTable GetCFHmx(string XSDJBH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", XSDJBH);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.CFGetCFHMX", dictionary);
        }

        public int GetDanJuSlvTypeNum(string XSDJBH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", XSDJBH);
            return this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.CFNeed", dictionary);
        }

        public AisinoDataSet QueryXSDJ(string KeyWord, string Month, string DJtype, string CheckRule, int pagesize, int pageno)
        {
            string str = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("key", KeyWord + "%");
            dictionary.Add("DJYF", Month);
            dictionary.Add("DJZL", DJtype);
            dictionary.Add("JYGZ", CheckRule);
            string str2 = dictionary["JYGZ"].ToString();
            if (str2 != null)
            {
                if (!(str2 == "s"))
                {
                    if (str2 == "c")
                    {
                        InvType invType = CommonTool.GetInvType(DJtype);
                        double num = TaxCardValue.GetInvLimit(DJtype) - 0.01;
                        dictionary.Remove("ZDJE");
                        dictionary.Add("ZDJE", num);
                        str = "aisino.Fwkp.Wbjk.CFQueryXSDJEx";
                        goto Label_00EA;
                    }
                    if (str2 == "x")
                    {
                        str = "aisino.Fwkp.Wbjk.CFQueryXSDJMutiSlv";
                        goto Label_00EA;
                    }
                }
                else
                {
                    str = "aisino.Fwkp.Wbjk.CFQueryXSDJAll";
                    goto Label_00EA;
                }
            }
            str = "aisino.Fwkp.Wbjk.CFQueryXSDJAll";
        Label_00EA:
            return this.baseDAO.querySQLDataSet(str, dictionary, pagesize, pageno);
        }

        public AisinoDataSet QueryXSDJMX(string XSDJBH, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", XSDJBH);
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.CFXSDJMXGet", dictionary, pagesize, pageno);
        }

        public string SaveAutoSplit(string XSDJBH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", XSDJBH);
            if (this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.CFSaveAutoCF", dictionary) > 0)
            {
                return "OK";
            }
            return "Error";
        }

        public string SaveManualSplit(string YuanBH, List<XSDJMXModel> listXSDJ)
        {
            string str;
            this.baseDAO.Open();
            try
            {
                Dictionary<string, Dictionary<string, object>> dictionary = new Dictionary<string, Dictionary<string, object>>();
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("BH", YuanBH);
                if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.XSDJ_HYcountBH", dictionary2) == 0)
                {
                    this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJ_HYinsert", dictionary2);
                }
                if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.XSDJ_MX_HYcountBH", dictionary2) == 0)
                {
                    this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJ_MX_HYinsert", dictionary2);
                }
                this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.CFDeleteBeforeCFXSDJ", dictionary2);
                foreach (XSDJMXModel model in listXSDJ)
                {
                    Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                    dictionary3.Add("BH", model.BH);
                    dictionary3.Add("GFMC", model.GFMC);
                    dictionary3.Add("GFSH", model.GFSH);
                    dictionary3.Add("GFDZDH", model.GFDZDH);
                    dictionary3.Add("GFYHZH", model.GFYHZH);
                    dictionary3.Add("XSBM", model.XSBM);
                    dictionary3.Add("YDXS", model.YDXS);
                    dictionary3.Add("JEHJ", model.JEHJ);
                    dictionary3.Add("DJRQ", model.DJRQ);
                    dictionary3.Add("DJYF", model.DJYF);
                    dictionary3.Add("DJZT", model.DJZT);
                    dictionary3.Add("KPZT", model.KPZT);
                    dictionary3.Add("BZ", model.BZ);
                    dictionary3.Add("FHR", model.FHR);
                    dictionary3.Add("SKR", model.SKR);
                    dictionary3.Add("QDHSPMC", model.QDHSPMC);
                    dictionary3.Add("XFYHZH", model.XFYHZH);
                    dictionary3.Add("XFDZDH", model.XFDZDH);
                    dictionary3.Add("CFHB", model.CFHB);
                    dictionary3.Add("DJZL", model.DJZL);
                    dictionary3.Add("SFZJY", model.SFZJY);
                    dictionary3.Add("HYSY", model.HYSY);
                    this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJAdds", dictionary3);
                    foreach (XSDJ_MXModel model2 in model.ListXSDJ_MX)
                    {
                        Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                        dictionary4.Add("XSDJBH", model2.XSDJBH);
                        dictionary4.Add("XH", model2.XH);
                        dictionary4.Add("SL", model2.SL);
                        dictionary4.Add("DJ", model2.DJ);
                        dictionary4.Add("JE", model2.JE);
                        dictionary4.Add("SLV", model2.SLV);
                        dictionary4.Add("SE", model2.SE);
                        dictionary4.Add("SPMC", model2.SPMC);
                        dictionary4.Add("SPSM", model2.SPSM);
                        dictionary4.Add("GGXH", model2.GGXH);
                        dictionary4.Add("JLDW", model2.JLDW);
                        dictionary4.Add("HSJBZ", model2.HSJBZ);
                        dictionary4.Add("DJHXZ", model2.DJHXZ);
                        dictionary4.Add("FPZL", model2.FPZL);
                        dictionary4.Add("FPDM", model2.FPDM);
                        dictionary4.Add("FPHM", model2.FPHM);
                        dictionary4.Add("SCFPXH", model2.SCFPXH);
                        this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.XSDJMXAdd", dictionary4);
                    }
                }
                this.baseDAO.Commit();
                str = "OK";
            }
            catch (Exception exception)
            {
                this.baseDAO.RollBack();
                HandleException.Log.Error(exception.Message, exception);
                str = "Error";
            }
            finally
            {
                this.baseDAO.Close();
            }
            return str;
        }

        public void SaveSeparateToPreview(List<XSDJMXModel> listDJ)
        {
            this.baseDAO.Open();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.CFclearYL", dictionary);
                foreach (XSDJMXModel model in listDJ)
                {
                    this.AddDJtoYL(model);
                    foreach (XSDJ_MXModel model2 in model.ListXSDJ_MX)
                    {
                        this.AddMXtoYL(model2);
                    }
                }
                this.baseDAO.Commit();
            }
            catch
            {
                this.baseDAO.RollBack();
            }
            finally
            {
                this.baseDAO.Close();
            }
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
                return PropValue.Pagesize;
            }
            set
            {
                PropValue.Pagesize = value;
            }
        }
    }
}

