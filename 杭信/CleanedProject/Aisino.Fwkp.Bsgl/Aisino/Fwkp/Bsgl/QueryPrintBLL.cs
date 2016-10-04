namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.PrintGrid;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    public class QueryPrintBLL
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private DataTable dataTable = new DataTable();
        private ILog loger = LogUtil.GetLogger<QueryPrintBLL>();
        private QueryPrintDAL queryPrintDAL = new QueryPrintDAL();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private TaxStateInfo taxStatInfo = new TaxStateInfo();

        public QueryPrintBLL()
        {
            this.taxStatInfo = this.taxCard.get_StateInfo();
        }

        public DateTime GetCurrentDate()
        {
            try
            {
                return this.taxCard.GetCardClock();
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return DateTime.Now;
        }

        public string GetCurrentMonth()
        {
            try
            {
                return (this.taxCard.GetCardClock().Year.ToString() + "-" + this.taxCard.GetCardClock().Month);
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return "";
            }
        }

        public int GetCurrentRepPeriod()
        {
            int num = 0;
            try
            {
                num = this.taxCard.GetPeriodCount(0)[1];
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return num;
        }

        public int GetLastRepPeroid()
        {
            int num = 0;
            try
            {
                num = this.taxCard.GetPeriodCount(0)[0];
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return num;
        }

        public List<int> GetMonthList(int _nYear)
        {
            List<int> monthStatPeriod = new List<int>();
            try
            {
                monthStatPeriod = this.taxCard.GetMonthStatPeriod(_nYear);
                if ((_nYear == this.taxCard.get_SysYear()) && (monthStatPeriod.Count == 0))
                {
                    monthStatPeriod.Add(this.taxCard.get_SysMonth());
                }
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return monthStatPeriod;
        }

        public TaxDataEntity GetOpenAccountMonth()
        {
            TaxDataEntity entity = new TaxDataEntity();
            try
            {
                int year = DateTime.Now.AddMonths(-13).Year;
                int month = DateTime.Now.AddMonths(-13).Month;
                entity.m_nYear = year;
                entity.m_nMonth = month;
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return entity;
        }

        public bool GetTaxcardVersion()
        {
            try
            {
                if (this.taxCard.get_ECardType() == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Info("tax card version:" + exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
        }

        public List<int> GetTaxPeriod(int nYear, int nMonth)
        {
            List<int> list = new List<int>();
            try
            {
                int num = 0;
                if (num <= 0)
                {
                    return list;
                }
                list.Clear();
                for (int i = 0; i < num; i++)
                {
                    list.Add(i + 1);
                }
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return list;
        }

        public List<int> GetYearList()
        {
            List<int> list = new List<int>();
            try
            {
                int year = this.taxCard.GetCardClock().Year;
                list.Add(year);
                list.Add(year - 1);
                list.Add(year - 2);
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return list;
        }

        public bool IsLocked()
        {
            try
            {
                if (this.taxStatInfo.IsLockReached == 0)
                {
                    return false;
                }
                this.loger.Debug("does not locked");
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
        }

        public bool MakeTable(ref CustomStyleDataGrid dataGridView, QueryPrintEntity queryPrintEntity)
        {
            DateTime time2;
            DateTime dtStart = Convert.ToDateTime(queryPrintEntity.m_nYear.ToString() + "-" + queryPrintEntity.m_nMonth.ToString() + "-01");
            if (queryPrintEntity.m_nMonth == 12)
            {
                time2 = Convert.ToDateTime(((queryPrintEntity.m_nYear + 1)).ToString() + "-01-01");
            }
            else
            {
                time2 = Convert.ToDateTime(queryPrintEntity.m_nYear.ToString() + "-" + ((queryPrintEntity.m_nMonth + 1)).ToString() + "-01");
            }
            dataGridView.ReadOnly = true;
            string strType = "";
            bool bWaste = false;
            if (queryPrintEntity.m_invType == INV_TYPE.INV_COMMON)
            {
                strType = "c";
            }
            else if (queryPrintEntity.m_invType == INV_TYPE.INV_SPECIAL)
            {
                strType = "s";
            }
            else if (queryPrintEntity.m_invType == INV_TYPE.INV_TRANSPORTATION)
            {
                strType = "f";
            }
            else if (queryPrintEntity.m_invType == INV_TYPE.INV_VEHICLESALES)
            {
                strType = "j";
            }
            else if (queryPrintEntity.m_invType == INV_TYPE.INV_PTDZ)
            {
                strType = "p";
            }
            else if (queryPrintEntity.m_invType == INV_TYPE.INV_JSFP)
            {
                strType = "q";
            }
            if ((queryPrintEntity.m_itemAction == ITEM_ACTION.ITEM_PLUS) || (queryPrintEntity.m_itemAction == ITEM_ACTION.ITEM_MINUS))
            {
                bWaste = false;
                if (queryPrintEntity.m_itemAction == ITEM_ACTION.ITEM_PLUS)
                {
                    this.dataTable = this.queryPrintDAL.GetPlusTable(strType, bWaste, dtStart, time2, queryPrintEntity.m_nTaxPeriod);
                }
                else
                {
                    this.dataTable = this.queryPrintDAL.GetMinusTable(strType, bWaste, dtStart, time2, queryPrintEntity.m_nTaxPeriod);
                }
            }
            else
            {
                bWaste = true;
                if (queryPrintEntity.m_itemAction == ITEM_ACTION.ITEM_PLUS_WASTE)
                {
                    this.dataTable = this.queryPrintDAL.GetPlusTable(strType, bWaste, dtStart, time2, queryPrintEntity.m_nTaxPeriod);
                }
                else
                {
                    this.dataTable = this.queryPrintDAL.GetMinusTable(strType, bWaste, dtStart, time2, queryPrintEntity.m_nTaxPeriod);
                }
            }
            this.dataTable.Columns.Add("税率1", Type.GetType("System.String"));
            for (int i = 0; i < this.dataTable.Rows.Count; i++)
            {
                string str2 = this.dataTable.Rows[i]["发票种类"].ToString();
                string str3 = this.dataTable.Rows[i]["税率"].ToString();
                if (string.IsNullOrEmpty(str3))
                {
                    this.dataTable.Rows[i]["税率1"] = "多税率";
                }
                else if (str3.Equals("0"))
                {
                    this.dataTable.Rows[i]["税率1"] = "0%";
                }
                else if (str3.Equals("0.05") && str2.Equals("s"))
                {
                    this.dataTable.Rows[i]["税率1"] = "5%";
                }
                else
                {
                    str3 = string.Format("{0}%", float.Parse(str3) * 100f);
                    this.dataTable.Rows[i]["税率1"] = str3;
                }
                this.dataTable.Rows[i]["开票日期"] = ((DateTime) this.dataTable.Rows[i]["开票日期"]).ToShortDateString();
                this.dataTable.Rows[i]["合计金额"].GetType();
                this.dataTable.Rows[i]["合计金额"] = Convert.ToDecimal(this.dataTable.Rows[i]["合计金额"]).ToString("F2");
                this.dataTable.Rows[i]["合计税额"] = Convert.ToDecimal(this.dataTable.Rows[i]["合计税额"]).ToString("F2");
                int num4 = Convert.ToInt32(this.dataTable.Rows[i]["发票号码"]);
                string str4 = string.Format("{0:00000000}", num4);
                this.dataTable.Rows[i]["发票号码"] = str4;
            }
            this.dataTable.Columns.Remove("税率");
            this.dataTable.Columns["税率1"].ColumnName = "税率";
            if (this.dataTable == null)
            {
                this.dataTable = new DataTable();
                this.dataTable.Columns.Add("发票种类");
                this.dataTable.Columns.Add("类别代码");
                this.dataTable.Columns.Add("发票号码");
                this.dataTable.Columns.Add("开票日期");
                this.dataTable.Columns.Add("购方税号");
                this.dataTable.Columns.Add("合计金额");
                this.dataTable.Columns.Add("合计税额");
                this.dataTable.Columns.Add("税率");
                dataGridView.DataSource = this.dataTable;
                if (dataGridView.Columns["营业税标志"] != null)
                {
                    dataGridView.Columns["营业税标志"].Visible = false;
                }
                return false;
            }
            if (this.dataTable.Rows.Count > 0)
            {
                dataGridView.AllowUserToAddRows = false;
            }
            dataGridView.DataSource = this.dataTable;
            if (dataGridView.Columns["营业税标志"] != null)
            {
                dataGridView.Columns["营业税标志"].Visible = false;
            }
            for (int j = 0; j < dataGridView.Columns.Count; j++)
            {
                dataGridView.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int k = 0; k < this.dataTable.Rows.Count; k++)
            {
                if (queryPrintEntity.m_invType == INV_TYPE.INV_COMMON)
                {
                    dataGridView["发票种类", k].Value = "普通发票";
                }
                else if (queryPrintEntity.m_invType == INV_TYPE.INV_SPECIAL)
                {
                    dataGridView["发票种类", k].Value = "专用发票";
                }
                else if (queryPrintEntity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                {
                    dataGridView["发票种类", k].Value = "货物运输业增值税专用发票";
                }
                else if (queryPrintEntity.m_invType == INV_TYPE.INV_VEHICLESALES)
                {
                    dataGridView["发票种类", k].Value = "机动车销售统一发票";
                }
                else if (queryPrintEntity.m_invType == INV_TYPE.INV_PTDZ)
                {
                    dataGridView["发票种类", k].Value = "电子增值税普通发票";
                }
                else if (queryPrintEntity.m_invType == INV_TYPE.INV_JSFP)
                {
                    dataGridView["发票种类", k].Value = "增值税普通发票(卷票)";
                }
            }
            return true;
        }

        public bool PrintTable(ref CustomStyleDataGrid dataGridView, string strTitle, List<PrinterItems> _PIHead, List<PrinterItems> _PIFoot, bool _bShow)
        {
            try
            {
                return DataGridPrintToolsQD.Print(dataGridView, dataGridView.Parent, strTitle, _PIHead, _PIFoot, _bShow);
            }
            catch (Exception exception)
            {
                this.loger.Info("print:" + exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
        }

        public bool PrintTableSerial(ref CustomStyleDataGrid dataGridView, string strTitle, List<PrinterItems> _PIHead, List<PrinterItems> _PIFoot, bool _bShow, bool _isSerialPrint, string showText)
        {
            try
            {
                return DataGridPrintToolsQD.PrintSerial(dataGridView, dataGridView.Parent, strTitle, _PIHead, _PIFoot, _bShow, _isSerialPrint, showText);
            }
            catch (Exception exception)
            {
                this.loger.Info("print:" + exception.Message);
                if (exception.Message.Equals("用户放弃连续打印"))
                {
                    throw exception;
                }
                return false;
            }
        }

        public string Search(ref CustomStyleDataGrid dataGridView)
        {
            string str = "";
            try
            {
                str = "";
            }
            catch (Exception exception)
            {
                this.loger.Info("search:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return str;
        }

        public void SetColumnStyles(ref CustomStyleDataGrid dataGridView, string xmlFileName)
        {
        }

        public void Statistics(ref CustomStyleDataGrid dataGridView)
        {
        }
    }
}

