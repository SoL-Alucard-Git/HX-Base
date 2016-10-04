namespace Aisino.Fwkp.Bmgl.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.IDAL;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    internal class BMSPSMManager : IBMSPSMManager
    {
        private int _currentPage = 1;
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        private string SQLID;

        public string AddGoodsTax(BMSPSMModel spsmEntity)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SZ", spsmEntity.SZ);
            dictionary.Add("BM", spsmEntity.BM);
            dictionary.Add("MC", spsmEntity.MC);
            dictionary.Add("SLV", spsmEntity.SLV);
            dictionary.Add("ZSL", spsmEntity.ZSL);
            dictionary.Add("SLJS", spsmEntity.SLJS);
            dictionary.Add("JSDW", spsmEntity.JSDW);
            dictionary.Add("SE", spsmEntity.SE);
            dictionary.Add("MDXS", spsmEntity.MDXS);
            dictionary.Add("FHDBZ", spsmEntity.FHDBZ);
            if (this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSPSM.SPSMAdd", dictionary) > 0)
            {
                return "0";
            }
            return "e02";
        }

        public DataTable AppendByKey(string KeyWord, int TopNo)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("key", "%" + KeyWord + "%");
            dictionary.Add("TopNo", TopNo);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPSM.SPSMqueryKeyAppend", dictionary);
            if (KeyWord.Trim().Length == 0)
            {
                table.Rows.Clear();
            }
            return table;
        }

        public DataTable AppendByKeyWJ(string KeyWord, int TopNo)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("key", "%" + KeyWord + "%");
            dictionary.Add("TopNo", TopNo);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPSM.SPSMqueryKeyAppendWJ", dictionary);
            if (KeyWord.Trim().Length == 0)
            {
                table.Rows.Clear();
            }
            return table;
        }

        public string deleteFenLei(string flBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", flBM + "%");
            return this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSPSM.SPSMDeleteFenLei", dictionary).ToString();
        }

        public string DeleteGoodsTax(string spsmEntityCode, string SZ)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", spsmEntityCode);
            dictionary.Add("sz", SZ);
            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSPSM.SPSMExist", dictionary) == 0)
            {
                return "eDoseNotExist";
            }
            int num2 = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSPSM.SPSMDelete", dictionary);
            if (num2 > 0)
            {
                return num2.ToString();
            }
            return "eFail";
        }

        public string deleteNodes(string searchid)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bmLike", searchid + "%");
            int num = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSPSM.SPSMDeleteSubNodes", dictionary);
            if (num > 0)
            {
                return num.ToString();
            }
            return "error delete";
        }

        public string ExecZengJianWei(string BM, bool isZengWei)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            dictionary.Add("bmLike", BM + "%");
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPSM.SPSMBMGetSubNode", dictionary);
            int num = 0;
            if (table.Rows.Count > 0)
            {
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string str = table.Rows[i]["BM"].ToString();
                    string str2 = table.Rows[i]["SJBM"].ToString();
                    int length = BM.Length;
                    string str3 = "";
                    string str4 = "";
                    if (isZengWei)
                    {
                        str3 = str.Insert(length, "0");
                        if (str2.Length > length)
                        {
                            str4 = str2.Insert(length, "0");
                        }
                        else
                        {
                            str4 = str2;
                        }
                    }
                    else
                    {
                        str3 = str.Remove(length, 1);
                        if (str2.Length > length)
                        {
                            str4 = str2.Remove(length, 1);
                        }
                        else
                        {
                            str4 = str2;
                        }
                    }
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    item.Add("bm", str);
                    item.Add("newBM", str3);
                    item.Add("newSJBM", str4);
                    list.Add(item);
                }
                num = this.baseDAO.未确认DAO方法3("aisino.Fwkp.Bmgl.BMSPSM.SPSMBMUpdateSJBMandBM", list);
            }
            if (num > 0)
            {
                return "0";
            }
            return "e1";
        }

        public bool ExistCusTaxCode(string CusTaxCode)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SH", CusTaxCode);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSPSM.SPSMSHExist", dictionary) > 0);
        }

        public bool ExistGoodsTax(BMSPSMModel spsmEntity)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", spsmEntity.BM);
            dictionary.Add("sz", spsmEntity.SZ);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSPSM.SPSMExist", dictionary) > 0);
        }

        public bool FenLeiHasChild(string fenLeiBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", fenLeiBM);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSPSM.SPSMHasChild", dictionary) > 0);
        }

        public DataTable GetBMSPSM(string BM, string SZ = "")
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            dictionary.Add("sz", SZ);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPSM.SPSMGetModel", dictionary);
        }

        public DataTable GetExportData()
        {
            if (this.condition.Count == 0)
            {
                this.condition.Add("key", "%%%");
            }
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPSM.SPSMqueryKeyout", this.condition);
        }

        public bool JianWeiVerify(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            return (this.baseDAO.queryValueSQL<string>("aisino.Fwkp.Bmgl.BMSPSM.SPSMBmJWVerify", dictionary) == "0");
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            List<TreeNodeTemp> list = new List<TreeNodeTemp>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("sjbm", searchid);
                DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPSM.SPSMTreeView", dictionary);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    TreeNodeTemp item = new TreeNodeTemp {
                        ParentBM = table.Rows[i]["SJBM"].ToString(),
                        BM = table.Rows[i]["BM"].ToString(),
                        Name = table.Rows[i]["MC"].ToString(),
                        Foldfile = Convert.ToInt32(table.Rows[i]["WJ"])
                    };
                    list.Add(item);
                }
                return list;
            }
            catch (DateBaseException)
            {
                return list;
            }
        }

        public string ModifyGoodsTax(BMSPSMModel spsmEntity, string OldSZ, string OldBM)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("oldBM", OldBM);
                dictionary.Add("oldSZ", OldSZ);
                dictionary.Add("SZ", spsmEntity.SZ);
                dictionary.Add("BM", spsmEntity.BM);
                dictionary.Add("MC", spsmEntity.MC);
                dictionary.Add("SLV", spsmEntity.SLV);
                dictionary.Add("ZSL", spsmEntity.ZSL);
                dictionary.Add("SLJS", spsmEntity.SLJS);
                dictionary.Add("JSDW", spsmEntity.JSDW);
                dictionary.Add("SE", spsmEntity.SE);
                dictionary.Add("MDXS", spsmEntity.MDXS);
                dictionary.Add("FHDBZ", spsmEntity.FHDBZ);
                if (this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSPSM.SPSMModify", dictionary) > 0)
                {
                    return "0";
                }
                return "e31";
            }
            catch (Exception exception)
            {
                if (!exception.Message.Contains("UNIQUE constraint failed"))
                {
                    throw;
                }
                return "e1";
            }
        }

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", "%" + KeyWord + "%");
            this.SQLID = "aisino.Fwkp.Bmgl.BMSPSM.SPSMqueryKey";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
        }

        public DataTable QueryByTaxCode(string TaxCode)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SH", TaxCode);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPSM.SPSMquerySH", dictionary);
        }

        public AisinoDataSet QueryGoodsTax(int pagesize, int pageno)
        {
            this.condition.Clear();
            this.SQLID = "aisino.Fwkp.Bmgl.BMSPSM.SPSMquery";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", string.Format("{0}%", selectBM));
            this.SQLID = "aisino.Fwkp.Bmgl.BMSPSM.SPSMbmlike";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            int num;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", searchid);
            string str = this.baseDAO.queryValueSQL<string>("aisino.Fwkp.Bmgl.BMSPSM.SPSMRecommend", dictionary);
            if (str == null)
            {
                if (searchid == "")
                {
                    return (searchid + "001");
                }
                return (searchid + "01");
            }
            string s = str.Substring(searchid.Length);
            if (!int.TryParse(s, out num))
            {
                return searchid;
            }
            string str4 = (num + 1).ToString();
            if (str4.Length > s.Length)
            {
                return "NoNode";
            }
            str4 = str4.PadLeft(s.Length, '0');
            return (str.Remove(searchid.Length) + str4);
        }

        public string UpdateSubNodesSJBM(BMSPSMModel spsmEntity, string YuanBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", YuanBM);
            dictionary.Add("bmLike", YuanBM + "%");
            this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPSM.SPSMBMGetSubNode", dictionary);
            List<string> list = new List<string>();
            List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
            try
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("bm", YuanBM);
                item.Add("SZ", spsmEntity.SZ);
                item.Add("BM", spsmEntity.BM);
                item.Add("MC", spsmEntity.MC);
                item.Add("SLV", spsmEntity.SLV);
                item.Add("ZSL", spsmEntity.ZSL);
                item.Add("SLJS", spsmEntity.SLJS);
                item.Add("JSDW", spsmEntity.JSDW);
                item.Add("SE", spsmEntity.SE);
                item.Add("FHDBZ", spsmEntity.FHDBZ);
                item.Add("MDXS", spsmEntity.MDXS);
                list.Add("aisino.Fwkp.Bmgl.BMSPSM.SPSMBMModify");
                list2.Add(item);
                if (this.baseDAO.未确认DAO方法1(list.ToArray(), list2) > 0)
                {
                    return "0";
                }
                return "errorModify";
            }
            catch (Exception exception)
            {
                if (!exception.Message.Contains("UNIQUE constraint failed"))
                {
                    throw;
                }
                return "e1";
            }
        }

        public bool ZengWeiVerify(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM + "%");
            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSPSM.SPSMBmZWverify", dictionary) >= 0x10)
            {
                return false;
            }
            return true;
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
                string s = PropertyUtil.GetValue("pagesize");
                if (((s == null) || (s.Length == 0)) || !int.TryParse(s, out num))
                {
                    num = 5;
                    PropertyUtil.SetValue("pagesize", s);
                }
                return num;
            }
            set
            {
                PropertyUtil.SetValue("pagesize", value.ToString());
            }
        }
    }
}

