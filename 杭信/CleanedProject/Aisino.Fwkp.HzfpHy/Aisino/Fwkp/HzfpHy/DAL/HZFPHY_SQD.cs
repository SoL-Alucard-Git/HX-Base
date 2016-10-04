namespace Aisino.Fwkp.HzfpHy.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.HzfpHy.IDAL;
    using Aisino.Fwkp.HzfpHy.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class HZFPHY_SQD : IHZFPHY_SQD
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
                if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.HzfpHy.DeleteSQD", this.dict) <= 0)
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

        public bool Insert(Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD model)
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
                this.dict.Add("FHRMC", model.FHFMC);
                this.dict.Add("FHRSH", model.FHFSH);
                this.dict.Add("SHRMC", model.SHFMC);
                this.dict.Add("SHRSH", model.SHFSH);
                this.dict.Add("TKRQ", model.TKRQ);
                this.dict.Add("SSYF", model.SSYF);
                this.dict.Add("HJJE", model.HJJE);
                this.dict.Add("SL", model.SL);
                this.dict.Add("HJSE", model.HJSE);
                this.dict.Add("YSHWXX", model.YSHWXX);
                this.dict.Add("JQBH", model.JQBH);
                this.dict.Add("CZCH", model.CZCH);
                this.dict.Add("CCDW", model.CCDW);
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
                if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.HzfpHy.InsertSQD", this.dict) <= 0)
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

        public Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD Select(string SQDH)
        {
            try
            {
                this.dict.Clear();
                this.dict.Add("SQDH", SQDH);
                ArrayList list = this.baseDao.querySQL("aisino.Fwkp.HzfpHy.SelectSQD", this.dict);
                Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD hzfphy_sqd = new Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD();
                if (list.Count > 0)
                {
                    this.dict = list[0] as Dictionary<string, object>;
                    hzfphy_sqd.FLBMBBBH = this.dict["FLBMBBBH"].ToString();
                    hzfphy_sqd.SQDH = this.dict["SQDH"].ToString();
                    hzfphy_sqd.FPDM = this.dict["FPDM"].ToString();
                    if (this.dict["FPHM"].ToString() != "")
                    {
                        hzfphy_sqd.FPHM = this.dict["FPHM"].ToString();
                    }
                    if (this.dict["KPJH"].ToString() != "")
                    {
                        hzfphy_sqd.KPJH = int.Parse(this.dict["KPJH"].ToString());
                    }
                    hzfphy_sqd.FPZL = this.dict["FPZL"].ToString();
                    hzfphy_sqd.GFMC = this.dict["GFMC"].ToString();
                    hzfphy_sqd.GFSH = this.dict["GFSH"].ToString();
                    hzfphy_sqd.XFMC = this.dict["XFMC"].ToString();
                    hzfphy_sqd.XFSH = this.dict["XFSH"].ToString();
                    hzfphy_sqd.FHFMC = this.dict["FHRMC"].ToString();
                    hzfphy_sqd.FHFSH = this.dict["FHRSH"].ToString();
                    hzfphy_sqd.SHFMC = this.dict["SHRMC"].ToString();
                    hzfphy_sqd.SHFSH = this.dict["SHRSH"].ToString();
                    if (this.dict["TKRQ"].ToString() != "")
                    {
                        hzfphy_sqd.TKRQ = DateTime.Parse(this.dict["TKRQ"].ToString());
                    }
                    if (this.dict["HJJE"].ToString() != "")
                    {
                        hzfphy_sqd.HJJE = decimal.Parse(this.dict["HJJE"].ToString());
                    }
                    if (this.dict["SL"].ToString() != "")
                    {
                        hzfphy_sqd.SL = double.Parse(this.dict["SL"].ToString());
                    }
                    if (this.dict["HJSE"].ToString() != "")
                    {
                        hzfphy_sqd.HJSE = decimal.Parse(this.dict["HJSE"].ToString());
                    }
                    hzfphy_sqd.YSHWXX = this.dict["YSHWXX"].ToString();
                    hzfphy_sqd.JQBH = this.dict["JQBH"].ToString();
                    hzfphy_sqd.CZCH = this.dict["CZCH"].ToString();
                    hzfphy_sqd.CCDW = this.dict["CCDW"].ToString();
                    hzfphy_sqd.JBR = this.dict["JBR"].ToString();
                    hzfphy_sqd.SQXZ = this.dict["SQXZ"].ToString();
                    hzfphy_sqd.YYSBZ = this.dict["YYSBZ"].ToString();
                    return hzfphy_sqd;
                }
                return null;
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

        public AisinoDataSet SelectList(int page, int count, Dictionary<string, object> dict)
        {
            AisinoDataSet set = null;
            try
            {
                set = this.baseDao.querySQLDataSet("aisino.Fwkp.HzfpHy.SelectSQDList", dict, count, page);
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

        public bool Updata(Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD model)
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
                this.dict.Add("FHRMC", model.FHFMC);
                this.dict.Add("FHRSH", model.FHFSH);
                this.dict.Add("SHRMC", model.SHFMC);
                this.dict.Add("SHRSH", model.SHFSH);
                this.dict.Add("TKRQ", model.TKRQ);
                this.dict.Add("SSYF", model.SSYF);
                this.dict.Add("HJJE", model.HJJE);
                this.dict.Add("SL", model.SL);
                this.dict.Add("HJSE", model.HJSE);
                this.dict.Add("YSHWXX", model.YSHWXX);
                this.dict.Add("JQBH", model.JQBH);
                this.dict.Add("CZCH", model.CZCH);
                this.dict.Add("CCDW", model.CCDW);
                this.dict.Add("JBR", model.JBR);
                this.dict.Add("SQXZ", model.SQXZ);
                this.dict.Add("SQLY", model.SQLY);
                this.dict.Add("SQRDH", model.SQRDH);
                this.dict.Add("YYSBZ", model.YYSBZ);
                this.dict.Add("XXBBH", model.XXBBH);
                this.dict.Add("XXBZT", model.XXBZT);
                this.dict.Add("XXBMS", model.XXBMS);
                if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.HzfpHy.UpdateSQD", this.dict) <= 0)
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

        public bool Updatazt(Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD model)
        {
            try
            {
                this.dict.Clear();
                this.dict.Add("SQDH", model.SQDH);
                this.dict.Add("XXBBH", model.XXBBH);
                this.dict.Add("XXBZT", model.XXBZT);
                this.dict.Add("XXBMS", model.XXBMS);
                if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.HzfpHy.UpdateztSQD", this.dict) <= 0)
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
                string s = PropertyUtil.GetValue("Aisino.Fwkp.HzfpHy.pagesize");
                if (((s == null) || (s.Length == 0)) || !int.TryParse(s, out num))
                {
                    num = 5;
                    PropertyUtil.SetValue("Aisino.Fwkp.HzfpHy.pagesize", s);
                }
                return num;
            }
            set
            {
                PropertyUtil.SetValue("Aisino.Fwkp.HzfpHy.pagesize", value.ToString());
            }
        }
    }
}

