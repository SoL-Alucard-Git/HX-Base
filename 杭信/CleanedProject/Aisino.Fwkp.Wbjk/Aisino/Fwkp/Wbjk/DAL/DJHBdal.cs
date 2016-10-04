namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class DJHBdal
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private int CurrentPage = 1;
        private int CurrentPageMX = 1;
        private int PageSize = 8;
        private int PageSizeMX = 5;

        public string CanComplex(List<string> listBH)
        {
            int num = 0;
            int num2 = 0;
            List<string> list = new List<string>();
            List<object> list2 = new List<object>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            for (int i = 0; i < listBH.Count; i++)
            {
                list2.Add(listBH[i]);
            }
            dictionary.Add("BH", list2);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.HBGetSps", dictionary);
            int count = table.Rows.Count;
            foreach (DataRow row in table.Rows)
            {
                string item = row[0].ToString();
                list.Add(item);
            }
            num = this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.HBGetZks", dictionary);
            num2 = this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.HBGetZsphs", dictionary);
            if ((list.Count > 1) && (num > 0))
            {
                return "zkMutiSp";
            }
            if (list.Count == num2)
            {
                if (list.Count == 1)
                {
                    return "OneSp";
                }
                if (list.Count > 1)
                {
                    return "NotSameSp";
                }
            }
            return "OK";
        }

        public List<XSDJ_MXModel> GetMergedMX(List<string> listBH)
        {
            int num;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            List<object> list = new List<object>();
            for (num = 0; num < listBH.Count; num++)
            {
                list.Add(listBH[num]);
            }
            dictionary.Add("BH", list);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.HBGetMXForMerge", dictionary);
            int count = table.Rows.Count;
            List<XSDJ_MXModel> list2 = new List<XSDJ_MXModel>();
            for (num = 0; num < table.Rows.Count; num++)
            {
                DataRow row = table.Rows[num];
                XSDJ_MXModel item = new XSDJ_MXModel {
                    XSDJBH = GetSafeData.ValidateValue<string>(row, "XSDJBH"),
                    XH = num + 1,
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
                list2.Add(item);
            }
            return list2;
        }

        public AisinoDataSet GetMingXi(int pagesize, int pageno)
        {
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.HBGetMingXiHBH", null, pagesize, pageno);
        }

        public void HandleGoToPageEventArgs(GoToPageEventArgs e)
        {
            this.CurrentPage = e.get_PageNO();
            this.PageSize = e.get_PageSize();
            string str = this.CurrentPage.ToString();
            PropertyUtil.SetValue("WBJK_DJHB_DATAGRID_HBMX", str);
        }

        public void HandleGoToPageEventArgs_1(GoToPageEventArgs e)
        {
            this.CurrentPage = e.get_PageNO();
            string str = this.CurrentPage.ToString();
            PropertyUtil.SetValue("WBJK_DJHB_DATAGRID_HBMX_1", str);
        }

        public void HandleGoToPageMXEventArgs(GoToPageEventArgs e)
        {
            this.CurrentPageMX = e.get_PageNO();
            this.PageSizeMX = e.get_PageSize();
        }

        public AisinoDataSet QueryByKey(string KeyWord)
        {
            return this.QueryByKey(KeyWord, PropValue.Pagesize, this.CurrentPage);
        }

        public AisinoDataSet QueryByKey(string Key, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("key", Key);
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.CFXSDJqueryKey", dictionary, pagesize, pageno);
        }

        public AisinoDataSet QueryXSDJ(string DJMonth, string DJType, string GFName, string GFSH)
        {
            int result = 1;
            int.TryParse(PropertyUtil.GetValue("WBJK_DJHB_DATAGRID_HBMX"), out result);
            this.CurrentPage = result;
            return this.QueryXSDJ(DJMonth, DJType, GFName, GFSH, this.PageSize, this.CurrentPage);
        }

        public AisinoDataSet QueryXSDJ(string DJMonth, string DJType, string GFName, string GFSH, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DJYF", DJMonth);
            dictionary.Add("DJZL", DJType);
            dictionary.Add("GFMC", "%" + GFName + "%");
            dictionary.Add("GFSH", "%" + GFSH + "%");
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.HBShowXSDJ", dictionary, pagesize, pageno);
        }

        public AisinoDataSet QueryXSDJMX(string XSDJBH)
        {
            return this.QueryXSDJMX(XSDJBH, this.PageSizeMX, this.CurrentPageMX);
        }

        public AisinoDataSet QueryXSDJMX(string XSDJBH, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", XSDJBH);
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.HBXSDJMXGet", dictionary, pagesize, pageno);
        }

        public AisinoDataSet QueryXSDJMX_1(string XSDJBH, int pagesize, int currentpage)
        {
            return this.QueryXSDJMX(XSDJBH, pagesize, currentpage);
        }
    }
}

