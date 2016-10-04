namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public class SPCXbll
    {
        private int _currentPage = 1;
        private SPCXdal spcxDAL = new SPCXdal();

        public void ExportExel(string filePath, DataTable fpTable, string excelTitle, List<Dictionary<string, string>> listColumnsName)
        {
            new ExportToExcel().DataToExcel(filePath, fpTable, listColumnsName, excelTitle);
        }

        public ArrayList GetAllGoods()
        {
            return this.spcxDAL.GetAllGoods();
        }

        public DataTable QueryGetGoods(InvoiceQueryCondition QueryArgs)
        {
            return this.spcxDAL.QueryGetGoods(QueryArgs);
        }

        public DataTable QueryGetGoods(FaPiaoQueryArgs QueryArgs)
        {
            return this.spcxDAL.QueryGetGoods(QueryArgs);
        }

        public AisinoDataSet QueryGoods(InvoiceQueryCondition QueryArgs, int pagesize, int pageno)
        {
            if (QueryArgs.m_dtStart > QueryArgs.m_dtEnd)
            {
                throw new CustomException("截止日期不能小于起始日期");
            }
            return this.spcxDAL.QueryGoods(QueryArgs, this.Pagesize, pageno);
        }

        public AisinoDataSet QueryGoods(FaPiaoQueryArgs QueryArgs, int pagesize, int pageno)
        {
            return this.spcxDAL.QueryGoods(QueryArgs, pagesize, pageno);
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

