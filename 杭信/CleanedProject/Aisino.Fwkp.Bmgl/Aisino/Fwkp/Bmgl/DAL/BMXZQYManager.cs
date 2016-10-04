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

    internal class BMXZQYManager : IBMXZQYManager
    {
        private int _currentPage = 1;
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        private string SQLID;

        public string AddDistrict(BMXZQYModel xzqEntity)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BM", xzqEntity.BM);
            dictionary.Add("MC", xzqEntity.MC);
            if (this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMXZQY.XZQYAdd", dictionary) > 0)
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
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMXZQY.XZQYqueryKeyAppendWJ", dictionary);
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
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMXZQY.XZQYqueryKeyAppendWJ", dictionary);
            if (KeyWord.Trim().Length == 0)
            {
                table.Rows.Clear();
            }
            return table;
        }

        public string DeleteDistrict(string xzqEntityCode)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", xzqEntityCode);
            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMXZQY.XZQYExist", dictionary) == 0)
            {
                return "eDoseNotExist";
            }
            int num2 = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMXZQY.XZQYDelete", dictionary);
            if (num2 > 0)
            {
                return num2.ToString();
            }
            return "eFail";
        }

        public string deleteFenLei(string flBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", flBM + "%");
            return this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMXZQY.XZQYDeleteFenLei", dictionary).ToString();
        }

        public string deleteNodes(string searchid)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bmLike", searchid + "%");
            int num = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMXZQY.XZQYDeleteSubNodes", dictionary);
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
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMXZQY.XZQYBMGetSubNode", dictionary);
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
                num = this.baseDAO.未确认DAO方法3("aisino.Fwkp.Bmgl.BMXZQY.XZQYBMUpdateSJBMandBM", list);
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
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMXZQY.XZQYSHExist", dictionary) > 0);
        }

        public bool ExistDistrict(BMXZQYModel xzqEntity)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", xzqEntity.BM);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMXZQY.XZQYExist", dictionary) > 0);
        }

        public bool FenLeiHasChild(string fenLeiBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", fenLeiBM);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMXZQY.XZQYHasChild", dictionary) > 0);
        }

        public DataTable GetBMXZQY(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMXZQY.BMXZQYGetModel", dictionary);
        }

        public DataTable GetExportData()
        {
            if (this.condition.Count == 0)
            {
                this.condition.Add("key", "%%%");
            }
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMXZQY.XZQYqueryKeyout", this.condition);
        }

        public int GetXZQYSJBM(string BM)
        {
            this.condition.Clear();
            if ((this.condition.Count == 0) && (BM.Length < 2))
            {
                this.condition.Add("bm", "0000");
            }
            else
            {
                string str = BM.Substring(0, 2);
                if (BM.Substring(2, 2) == "00")
                {
                    this.condition.Add("bm", BM);
                }
                else
                {
                    this.condition.Add("bm", string.Format("{0}00", str));
                }
            }
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMXZQY.XZQYLike", this.condition);
            if (table != null)
            {
                return int.Parse(table.Rows[0]["NUM"].ToString());
            }
            return 0;
        }

        public bool JianWeiVerify(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            return (this.baseDAO.queryValueSQL<string>("aisino.Fwkp.Bmgl.BMXZQY.XZQYBmJWVerify", dictionary) == "0");
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            List<TreeNodeTemp> list = new List<TreeNodeTemp>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("sjbm", searchid);
                DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMXZQY.XZQYTreeView", dictionary);
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

        public string ModifyDistrict(BMXZQYModel xzqEntity, string OldBM)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("oldBM", OldBM);
                dictionary.Add("BM", xzqEntity.BM);
                dictionary.Add("MC", xzqEntity.MC);
                if (this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMXZQY.XZQYModify", dictionary) > 0)
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
            this.SQLID = "aisino.Fwkp.Bmgl.BMXZQY.XZQYqueryKey";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
        }

        public DataTable QueryByTaxCode(string TaxCode)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SH", TaxCode);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMXZQY.XZQYquerySH", dictionary);
        }

        public AisinoDataSet QueryDistrict(int pagesize, int pageno)
        {
            this.condition.Clear();
            this.SQLID = "aisino.Fwkp.Bmgl.BMXZQY.XZQYquery";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", string.Format("{0}%", selectBM));
            this.SQLID = "aisino.Fwkp.Bmgl.BMXZQY.XZQYbmlike";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            int num;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", searchid);
            string str = this.baseDAO.queryValueSQL<string>("aisino.Fwkp.Bmgl.BMXZQY.XZQYRecommend", dictionary);
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

        public string UpdateSubNodesSJBM(BMXZQYModel xzqEntity, string YuanBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", YuanBM);
            dictionary.Add("bmLike", YuanBM + "%");
            this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMXZQY.XZQYBMGetSubNode", dictionary);
            List<string> list = new List<string>();
            List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
            try
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("bm", YuanBM);
                item.Add("afterbm", xzqEntity.BM);
                item.Add("MC", xzqEntity.MC);
                list.Add("aisino.Fwkp.Bmgl.BMXZQY.XZQYBMModify");
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
            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMXZQY.XZQYBmZWverify", dictionary) >= 0x10)
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

