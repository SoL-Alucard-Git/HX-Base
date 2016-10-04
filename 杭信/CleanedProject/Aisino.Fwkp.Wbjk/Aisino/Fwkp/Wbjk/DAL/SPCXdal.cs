namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public class SPCXdal
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<SPCXdal>();
        private string SQLID = "";

        public ArrayList GetAllGoods()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            try
            {
                list2 = this.baseDAO.querySQL("aisino.Fwkp.Wbjk.SelectBM_SP", dictionary);
                for (int i = 0; i < list2.Count; i++)
                {
                    dictionary2 = list2[i] as Dictionary<string, object>;
                    list.Add(dictionary2["MC"].ToString());
                }
            }
            catch (Exception exception)
            {
                this.loger.Info("获取所有商品名称失败");
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return list;
        }

        public DataTable QueryGetGoods(InvoiceQueryCondition QueryArgs)
        {
            DataTable table;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPDM", "%");
            dictionary.Add("FPHM", "%");
            dictionary.Add("GFSH", "%");
            dictionary.Add("GFMC", "%");
            dictionary.Add("SPMC", "%");
            dictionary.Add("GGXH", "%");
            if (QueryArgs.m_strInvCodeList.Count > 0)
            {
                dictionary["FPDM"] = QueryArgs.m_strInvCodeList;
            }
            if (QueryArgs.m_strInvNumList.Count > 0)
            {
                dictionary["FPHM"] = QueryArgs.m_strInvNumList;
            }
            if (QueryArgs.m_strBuyerCodeList.Count > 0)
            {
                dictionary["GFSH"] = QueryArgs.m_strBuyerCodeList;
            }
            if (QueryArgs.m_strBuyerNameList.Count > 0)
            {
                dictionary["GFMC"] = QueryArgs.m_strBuyerNameList;
            }
            if (QueryArgs.m_strGoodsNameList.Count > 0)
            {
                dictionary["SPMC"] = QueryArgs.m_strGoodsNameList;
            }
            if (QueryArgs.m_strStateList.Count > 0)
            {
                dictionary["GGXH"] = QueryArgs.m_strStateList;
            }
            dictionary.Add("QSRQ", QueryArgs.m_dtStart);
            dictionary.Add("JZRQ", QueryArgs.m_dtEnd);
            dictionary.Add("FPZL", QueryArgs.m_strInvType);
            dictionary.Add("ZFBZ", QueryArgs.m_strWasteFlag);
            if (QueryArgs.m_dtStart == Convert.ToDateTime("0001-01-01"))
            {
                this.SQLID = "aisino.Fwkp.Wbjk.GoodsQueryAllDateExport";
            }
            else
            {
                this.SQLID = "aisino.Fwkp.Wbjk.GoodsQueryTheDateExport";
            }
            try
            {
                table = this.baseDAO.querySQLDataTable(this.SQLID, dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Info("商品查询失败");
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return table;
        }

        public DataTable QueryGetGoods(FaPiaoQueryArgs QueryArgs)
        {
            DataTable table;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPDM", "%" + QueryArgs.FPDM + "%");
            dictionary.Add("FPHM", "%" + QueryArgs.FPHM + "%");
            dictionary.Add("GFMC", "%" + QueryArgs.GFMC + "%");
            dictionary.Add("GFSH", "%" + QueryArgs.GFSH + "%");
            dictionary.Add("GGXH", "%" + QueryArgs.GGXH + "%");
            dictionary.Add("SPMC", "%" + QueryArgs.SPMC + "%");
            dictionary.Add("QSRQ", QueryArgs.StartTime);
            dictionary.Add("JZRQ", QueryArgs.EndTime);
            dictionary.Add("FPZL", QueryArgs.FPZL);
            dictionary.Add("ZFBZ", QueryArgs.ZFBZ);
            if (QueryArgs.StartTime.ToShortDateString() == "1753-1-1")
            {
                this.SQLID = "aisino.Fwkp.Wbjk.SPCXQueryGetAllDate";
            }
            else
            {
                this.SQLID = "aisino.Fwkp.Wbjk.SPCXQueryGet";
            }
            try
            {
                table = this.baseDAO.querySQLDataTable(this.SQLID, dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Info("商品查询失败");
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return table;
        }

        public AisinoDataSet QueryGoods(InvoiceQueryCondition QueryArgs, int pagesize, int pageno)
        {
            AisinoDataSet set;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPDM", "%");
            dictionary.Add("FPHM", "%");
            dictionary.Add("GFSH", "%");
            dictionary.Add("GFMC", "%");
            dictionary.Add("SPMC", "%");
            dictionary.Add("GGXH", "%");
            if (QueryArgs.m_strInvCodeList.Count > 0)
            {
                dictionary["FPDM"] = QueryArgs.m_strInvCodeList;
            }
            if (QueryArgs.m_strInvNumList.Count > 0)
            {
                dictionary["FPHM"] = QueryArgs.m_strInvNumList;
            }
            if (QueryArgs.m_strBuyerCodeList.Count > 0)
            {
                dictionary["GFSH"] = QueryArgs.m_strBuyerCodeList;
            }
            if (QueryArgs.m_strBuyerNameList.Count > 0)
            {
                dictionary["GFMC"] = QueryArgs.m_strBuyerNameList;
            }
            if (QueryArgs.m_strGoodsNameList.Count > 0)
            {
                dictionary["SPMC"] = QueryArgs.m_strGoodsNameList;
            }
            if (QueryArgs.m_strStateList.Count > 0)
            {
                dictionary["GGXH"] = QueryArgs.m_strStateList;
            }
            dictionary.Add("QSRQ", QueryArgs.m_dtStart);
            dictionary.Add("JZRQ", QueryArgs.m_dtEnd);
            dictionary.Add("FPZL", QueryArgs.m_strInvType);
            dictionary.Add("ZFBZ", QueryArgs.m_strWasteFlag);
            if (QueryArgs.m_dtStart == Convert.ToDateTime("0001-01-01"))
            {
                this.SQLID = "aisino.Fwkp.Wbjk.GoodsQueryAllDate";
            }
            else
            {
                this.SQLID = "aisino.Fwkp.Wbjk.GoodsQueryTheDate";
            }
            try
            {
                set = this.baseDAO.querySQLDataSet(this.SQLID, dictionary, pagesize, pageno);
            }
            catch (Exception exception)
            {
                this.loger.Info("商品查询失败");
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return set;
        }

        public AisinoDataSet QueryGoods(FaPiaoQueryArgs QueryArgs, int pagesize, int pageno)
        {
            AisinoDataSet set;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPDM", "%" + QueryArgs.FPDM + "%");
            dictionary.Add("FPHM", "%" + QueryArgs.FPHM + "%");
            dictionary.Add("GFMC", "%" + QueryArgs.GFMC + "%");
            dictionary.Add("GFSH", "%" + QueryArgs.GFSH + "%");
            dictionary.Add("GGXH", "%" + QueryArgs.GGXH + "%");
            dictionary.Add("SPMC", "%" + QueryArgs.SPMC + "%");
            dictionary.Add("QSRQ", QueryArgs.StartTime);
            dictionary.Add("JZRQ", QueryArgs.EndTime);
            dictionary.Add("FPZL", QueryArgs.FPZL);
            dictionary.Add("ZFBZ", QueryArgs.ZFBZ);
            if (QueryArgs.StartTime.ToShortDateString() == "1753-1-1")
            {
                this.SQLID = "aisino.Fwkp.Wbjk.SPCXQueryAllDate";
            }
            else
            {
                this.SQLID = "aisino.Fwkp.Wbjk.SPCXQuery";
            }
            try
            {
                set = this.baseDAO.querySQLDataSet(this.SQLID, dictionary, pagesize, pageno);
            }
            catch (Exception exception)
            {
                this.loger.Info("商品查询失败");
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return set;
        }
    }
}

