namespace Aisino.Fwkp.Hzfp.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Hzfp.IDAL;
    using Aisino.Fwkp.Hzfp.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public class HZFP_SQD : Aisino.Fwkp.Hzfp.IDAL.HZFP_SQD
    {
        private int _currentPage = 1;
        private IBaseDAO baseDao = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> dict = new Dictionary<string, object>();

        public bool Delete(string SQDH)
        {
            try
            {
                this.dict.Clear();
                this.dict.Add("SQDH", SQDH);
                if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Hzfp.DeleteSQD", this.dict) <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return false;
        }

        public DataTable GetData(int Year)
        {
            DataTable table = null;
            try
            {
                this.dict.Clear();
                this.dict.Add("YEAR", Year);
                table = this.baseDao.querySQLDataTable("aisino.Fwkp.Hzfp.GetDataList", this.dict);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return table;
        }

        public bool Insert(Aisino.Fwkp.Hzfp.Model.HZFP_SQD model)
        {
            try
            {
                this.dict.Clear();
                this.dict.Add("FLBMBBBH", model.FLBMBBBH);
                this.dict.Add("SQDH", model.SQDH);
                this.dict.Add("FPZL", model.FPZL);
                this.dict.Add("FPDM", model.FPDM);
                this.dict.Add("FPHM", model.FPHM);
                this.dict.Add("KPJH", model.KPJH);
                this.dict.Add("GFMC", model.GFMC);
                this.dict.Add("GFSH", model.GFSH);
                this.dict.Add("XFMC", model.XFMC);
                this.dict.Add("XFSH", model.XFSH);
                string str = ToolUtil.FormatDateTimeEx(model.TKRQ.ToString("yyyy-MM-dd"));
                this.dict.Add("TKRQ", str);
                this.dict.Add("SSYF", model.SSYF);
                this.dict.Add("HJJE", model.HJJE);
                if (model.SL == -1.0)
                {
                    this.dict.Add("SL", null);
                }
                else
                {
                    this.dict.Add("SL", model.SL);
                }
                this.dict.Add("HJSE", model.HJSE);
                this.dict.Add("JBR", model.JBR);
                this.dict.Add("SQXZ", model.SQXZ);
                this.dict.Add("SQLY", model.SQLY);
                this.dict.Add("SQRDH", model.SQRDH);
                this.dict.Add("BBBZ", model.BBBZ);
                this.dict.Add("YYSBZ", model.YYSBZ);
                this.dict.Add("REQNSRSBH", model.REQNSRSBH);
                this.dict.Add("JSPH", model.JSPH);
                this.dict.Add("XXBBH", model.XXBBH);
                this.dict.Add("XXBZT", model.XXBZT);
                this.dict.Add("XXBMS", model.XXBMS);
                if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Hzfp.InsertSQD", this.dict) <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return false;
        }

        public Aisino.Fwkp.Hzfp.Model.HZFP_SQD Select(string SQDH)
        {
            try
            {
                this.dict.Clear();
                this.dict.Add("SQDH", SQDH);
                ArrayList list = this.baseDao.querySQL("aisino.Fwkp.Hzfp.SelectSQD", this.dict);
                Aisino.Fwkp.Hzfp.Model.HZFP_SQD hzfp_sqd = new Aisino.Fwkp.Hzfp.Model.HZFP_SQD();
                if (list.Count <= 0)
                {
                    return null;
                }
                this.dict = list[0] as Dictionary<string, object>;
                hzfp_sqd.FLBMBBBH = this.dict["FLBMBBBH"].ToString();
                hzfp_sqd.SQDH = this.dict["SQDH"].ToString();
                hzfp_sqd.FPDM = this.dict["FPDM"].ToString();
                if (this.dict["FPHM"].ToString() != "")
                {
                    hzfp_sqd.FPHM = int.Parse(this.dict["FPHM"].ToString());
                }
                if (this.dict["KPJH"].ToString() != "")
                {
                    hzfp_sqd.KPJH = int.Parse(this.dict["KPJH"].ToString());
                }
                hzfp_sqd.FPZL = this.dict["FPZL"].ToString();
                hzfp_sqd.GFMC = this.dict["GFMC"].ToString();
                hzfp_sqd.GFSH = this.dict["GFSH"].ToString();
                hzfp_sqd.XFMC = this.dict["XFMC"].ToString();
                hzfp_sqd.XFSH = this.dict["XFSH"].ToString();
                if (this.dict["TKRQ"].ToString() != "")
                {
                    hzfp_sqd.TKRQ = DateTime.Parse(this.dict["TKRQ"].ToString());
                }
                if (this.dict["HJJE"].ToString() != "")
                {
                    hzfp_sqd.HJJE = decimal.Parse(this.dict["HJJE"].ToString());
                }
                if (this.dict["SL"].ToString() != "")
                {
                    hzfp_sqd.SL = double.Parse(this.dict["SL"].ToString());
                }
                else
                {
                    hzfp_sqd.SL = -1.0;
                }
                if (this.dict["HJSE"].ToString() != "")
                {
                    hzfp_sqd.HJSE = decimal.Parse(this.dict["HJSE"].ToString());
                }
                hzfp_sqd.JBR = this.dict["JBR"].ToString();
                hzfp_sqd.SQXZ = this.dict["SQXZ"].ToString();
                hzfp_sqd.SQLY = this.dict["SQLY"].ToString();
                hzfp_sqd.SQRDH = this.dict["SQRDH"].ToString();
                hzfp_sqd.YYSBZ = this.dict["YYSBZ"].ToString();
                hzfp_sqd.REQNSRSBH = this.dict["REQNSRSBH"].ToString();
                hzfp_sqd.JSPH = this.dict["JSPH"].ToString();
                hzfp_sqd.XXBBH = this.dict["XXBBH"].ToString();
                hzfp_sqd.XXBZT = this.dict["XXBZT"].ToString();
                hzfp_sqd.XXBMS = this.dict["XXBMS"].ToString();
                return hzfp_sqd;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public AisinoDataSet SelectList(int page, int count, int month)
        {
            AisinoDataSet set = null;
            try
            {
                this.dict.Clear();
                if (month != 0)
                {
                    this.dict.Add("MONbz", 1);
                    this.dict.Add("MON", new DateTime(DateTime.Now.Year, month, DateTime.Now.Day).ToString("yyyy-MM-dd"));
                }
                else
                {
                    this.dict.Add("MONbz", 0);
                    this.dict.Add("MON", "");
                }
                set = this.baseDao.querySQLDataSet("aisino.Fwkp.Hzfp.SelectSQDList", this.dict, count, page);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return set;
        }

        public AisinoDataSet SelectSelList(int page, int count, int month, string xfsh)
        {
            AisinoDataSet set = null;
            try
            {
                this.dict.Clear();
                if (month != 0)
                {
                    this.dict.Add("MONbz", 1);
                    this.dict.Add("MON", new DateTime(DateTime.Now.Year, month, DateTime.Now.Day).ToString("yyyy-MM-dd"));
                }
                else
                {
                    this.dict.Add("MONbz", 0);
                    this.dict.Add("MON", "");
                    this.dict.Add("xfsh", xfsh);
                }
                set = this.baseDao.querySQLDataSet("aisino.Fwkp.Hzfp.SelectSQDSelList", this.dict, count, page);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return set;
        }

        public AisinoDataSet SelectSqdlist(int page, int count, Dictionary<string, object> dictionary)
        {
            AisinoDataSet set = null;
            try
            {
                set = this.baseDao.querySQLDataSet("aisino.Fwkp.Hzfp.SelectSqdlist", dictionary, count, page);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return set;
        }

        public AisinoDataSet SelectSqdradlist(int page, int count, Dictionary<string, object> dictionary)
        {
            AisinoDataSet set = null;
            try
            {
                set = this.baseDao.querySQLDataSet("aisino.Fwkp.Hzfp.SelectSqdradlist", dictionary, count, page);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return set;
        }

        public bool Updata(Aisino.Fwkp.Hzfp.Model.HZFP_SQD model)
        {
            try
            {
                this.dict.Clear();
                this.dict.Add("FLBMBBBH", model.FLBMBBBH);
                this.dict.Add("SQDH", model.SQDH);
                this.dict.Add("FPDM", model.FPDM);
                this.dict.Add("FPHM", model.FPHM);
                this.dict.Add("KPJH", model.KPJH);
                this.dict.Add("GFMC", model.GFMC);
                this.dict.Add("GFSH", model.GFSH);
                this.dict.Add("XFMC", model.XFMC);
                this.dict.Add("XFSH", model.XFSH);
                string str = ToolUtil.FormatDateTimeEx(model.TKRQ.ToString("yyyy-MM-dd"));
                this.dict.Add("TKRQ", str);
                this.dict.Add("SSYF", model.SSYF);
                this.dict.Add("HJJE", model.HJJE);
                if (model.SL == -1.0)
                {
                    this.dict.Add("SL", null);
                }
                else
                {
                    this.dict.Add("SL", model.SL);
                }
                this.dict.Add("HJSE", model.HJSE);
                this.dict.Add("JBR", model.JBR);
                this.dict.Add("SQXZ", model.SQXZ);
                this.dict.Add("SQLY", model.SQLY);
                this.dict.Add("SQRDH", model.SQRDH);
                this.dict.Add("YYSBZ", model.YYSBZ);
                this.dict.Add("XXBBH", model.XXBBH);
                this.dict.Add("XXBZT", model.XXBZT);
                this.dict.Add("XXBMS", model.XXBMS);
                if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Hzfp.UpdateSQD", this.dict) <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return false;
        }

        public bool Updatazt(Aisino.Fwkp.Hzfp.Model.HZFP_SQD model)
        {
            try
            {
                this.dict.Clear();
                this.dict.Add("SQDH", model.SQDH);
                this.dict.Add("XXBBH", model.XXBBH);
                this.dict.Add("XXBZT", model.XXBZT);
                this.dict.Add("XXBMS", model.XXBMS);
                if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Hzfp.UpdateztSQD", this.dict) <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return false;
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
                int num;
                string s = PropertyUtil.GetValue("Aisino.Fwkp.Hzfp.pagesize");
                if (((s == null) || (s.Length == 0)) || !int.TryParse(s, out num))
                {
                    num = 5;
                    PropertyUtil.SetValue("Aisino.Fwkp.Hzfp.pagesize", s);
                }
                return num;
            }
            set
            {
                PropertyUtil.SetValue("Aisino.Fwkp.Hzfp.pagesize", value.ToString());
            }
        }
    }
}

