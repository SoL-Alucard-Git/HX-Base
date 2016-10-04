namespace Aisino.Fwkp.Bmgl.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.IDAL;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using Common;

    internal class BMKHManager : IBMKHManager
    {
        private int _currentPage = 1;
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        private string SQLID;

        public string AddCustomer(BMKHModel customer)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BM", customer.BM);
            dictionary.Add("MC", customer.MC);
            dictionary.Add("JM", customer.JM);
            dictionary.Add("KJM", customer.KJM);
            dictionary.Add("SH", customer.SH);
            dictionary.Add("DZDH", customer.DZDH);
            dictionary.Add("YHZH", customer.YHZH);
            dictionary.Add("YJDZ", customer.YJDZ);
            dictionary.Add("BZ", customer.BZ);
            dictionary.Add("YSKM", customer.YSKM);
            dictionary.Add("DQBM", customer.DQBM);
            dictionary.Add("DQMC", customer.DQMC);
            dictionary.Add("DQKM", customer.DQKM);
            dictionary.Add("SJBM", customer.SJBM);
            dictionary.Add("WJ", customer.WJ);
            dictionary.Add("SFZJY", customer.SFZJY);
            if (this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMKH.KHAdd", dictionary) > 0)
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
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHqueryKeyAppendWJ", dictionary);
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
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHqueryKeyAppendWJ", dictionary);
            if (KeyWord.Trim().Length == 0)
            {
                table.Rows.Clear();
            }
            return table;
        }

        public AisinoDataSet AppendKhByMc(string keyWordMc)
        {
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            dictionary2.Add("keyWordMc", keyWordMc);
            Dictionary<string, object> dictionary = dictionary2;
            AisinoDataSet set = this.baseDAO.querySQLDataSet("aisino.Fwkp.Bmgl.BMKH.KHqueryKeyByMcAppendWJ", dictionary, 0x1388, 1);
            if (string.IsNullOrEmpty(keyWordMc))
            {
                set.Data.Rows.Clear();
            }
            return set;
        }

        public AisinoDataSet AppendKhByMcAndSh(string keyWordMc, string keyWordSh)
        {
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            dictionary2.Add("keyWordMc", keyWordMc);
            dictionary2.Add("keyWordSh", keyWordSh);
            Dictionary<string, object> dictionary = dictionary2;
            AisinoDataSet set = this.baseDAO.querySQLDataSet("aisino.Fwkp.Bmgl.BMKH.KHqueryKeyByMcAndShAppendWJ", dictionary, 0x1388, 1);
            if (string.IsNullOrEmpty(keyWordMc) || string.IsNullOrEmpty(keyWordSh))
            {
                set.Data.Rows.Clear();
            }
            return set;
        }

        public AisinoDataSet AppendKhBySh(string keyWordSh)
        {
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            dictionary2.Add("keyWordSh", keyWordSh);
            Dictionary<string, object> dictionary = dictionary2;
            AisinoDataSet set = this.baseDAO.querySQLDataSet("aisino.Fwkp.Bmgl.BMKH.KHqueryKeyByShAppendWJ", dictionary, 0x1388, 1);
            if (string.IsNullOrEmpty(keyWordSh))
            {
                set.Data.Rows.Clear();
            }
            return set;
        }

        public string AutoNodeLogic()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", "自动保存");
            dictionary.Add("SJBM", "");
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHMCExist", dictionary);
            int count = table.Rows.Count;
            if ((count == 0) && (this.TuiJianBM("") != "NoNode"))
            {
                string str = this.TuiJianBM("");
                BMKHModel customer = new BMKHModel {
                    BM = str,
                    MC = "自动保存",
                    SJBM = "",
                    WJ = 0
                };
                if ("0" == this.AddCustomer(customer))
                {
                    return customer.BM;
                }
            }
            if (1 == count)
            {
                return table.Rows[0]["BM"].ToString();
            }
            return "";
        }

        public int BmMaxLenth(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM + "%");
            return this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMKH.KHBmZWverify", dictionary);
        }

        public string ChildDetermine(string BM, string SJBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sjbm", SJBM);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHChildDetermine", dictionary);
            if (table.Rows.Count == 0)
            {
                return "NoXJBM";
            }
            if ((table.Rows.Count == 1) && (BM == table.Rows[0]["BM"].ToString()))
            {
                return "OnlyBMAndIsSelf";
            }
            return "Other";
        }

        public string DeleteCustomer(string customerCode)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", customerCode);
            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMKH.KHExist", dictionary) == 0)
            {
                return "eDoseNotExist";
            }
            int num2 = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMKH.KHDelete", dictionary);
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
            return this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMKH.KHDeleteFenLei", dictionary).ToString();
        }

        public string deleteNodes(string searchid)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bmLike", searchid + "%");
            int num = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMKH.KHDeleteSubNodes", dictionary);
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
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHBMGetSubNode", dictionary);
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
                num = this.baseDAO.未确认DAO方法3("aisino.Fwkp.Bmgl.BMKH.KHBMUpdateSJBMandBM", list);
            }
            if (num > 0)
            {
                return "0";
            }
            return "e1";
        }

        public bool ExistCusMC(string oldBM, string MC, string SJBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", MC);
            dictionary.Add("SJBM", SJBM);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHMCExist", dictionary);
            int count = table.Rows.Count;
            if ((count <= 1) && (((count != 1) || !(oldBM != table.Rows[0]["BM"].ToString())) || !(MC == table.Rows[0]["MC"].ToString())))
            {
                return false;
            }
            return true;
        }

        public bool ExistCusTaxCode(string CusTaxCode, string OldBM = "NoOldBM")
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SH", CusTaxCode);
            dictionary.Add("BM", OldBM);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMKH.KHSHExist", dictionary) > 0);
        }

        public bool ExistCustomer(BMKHModel customer)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", customer.BM);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMKH.KHExist", dictionary) > 0);
        }

        public bool FenLeiHasChild(string fenLeiBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", fenLeiBM);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMKH.KHHasChild", dictionary) > 0);
        }

        public DataTable GetExportData()
        {
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHqueryKeyout", this.condition);
        }

        public DataTable GetKH(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHGetModel", dictionary);
        }

        public int GetSuggestBMLen(string SJBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SJBM", SJBM);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.GetXJBMBySJBM", dictionary);
            if ((table != null) && (table.Rows.Count > 0))
            {
                return table.Rows[0]["BM"].ToString().Length;
            }
            if (SJBM == "")
            {
                return (SJBM.Length + 3);
            }
            return (SJBM.Length + 2);
        }

        public bool JianWeiVerify(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            return (this.baseDAO.queryValueSQL<string>("aisino.Fwkp.Bmgl.BMKH.KHBmJWVerify", dictionary) == "0");
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            List<TreeNodeTemp> list = new List<TreeNodeTemp>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("sjbm", searchid);
                DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHTreeView", dictionary);
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
                list.Sort(new Comparison<TreeNodeTemp>(StringUtils.CompareBM));
                return list;
            }
            catch (DateBaseException)
            {
                return list;
            }
        }

        public string ModifyCustomer(BMKHModel customer, string OldBM)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("oldBM", OldBM);
                dictionary.Add("BM", customer.BM);
                dictionary.Add("MC", customer.MC);
                dictionary.Add("JM", customer.JM);
                dictionary.Add("KJM", customer.KJM);
                dictionary.Add("SH", customer.SH);
                dictionary.Add("DZDH", customer.DZDH);
                dictionary.Add("YHZH", customer.YHZH);
                dictionary.Add("YJDZ", customer.YJDZ);
                dictionary.Add("BZ", customer.BZ);
                dictionary.Add("YSKM", customer.YSKM);
                dictionary.Add("DQBM", customer.DQBM);
                dictionary.Add("DQMC", customer.DQMC);
                dictionary.Add("DQKM", customer.DQKM);
                dictionary.Add("SJBM", customer.SJBM);
                dictionary.Add("SFZJY", customer.SFZJY);
                if (this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMKH.KHModify", dictionary) > 0)
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
            this.SQLID = "aisino.Fwkp.Bmgl.BMKH.KHqueryKey";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
        }

        public DataTable QueryByMCAndSJBM(string MC, string SJBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", MC);
            dictionary.Add("SJBM", SJBM);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHqueryMCAndSJBM", dictionary);
        }

        public DataTable QueryByMCAndSJBMAndSHEmpty(string MC, string SJBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", MC);
            dictionary.Add("SJBM", SJBM);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHqueryMCAndSJBMAndSHEmpty", dictionary);
        }

        public DataTable QueryByMCSH(string MC, string SH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", MC);
            dictionary.Add("SH", SH);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHqueryMCAndSH", dictionary);
        }

        public DataTable QueryBySHAndSJBM(string SH, string SJBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SH", SH);
            dictionary.Add("SJBM", SJBM);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHquerySHAndSJBM", dictionary);
        }

        public DataTable QueryByTaxCode(string TaxCode)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SH", TaxCode);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHquerySH", dictionary);
        }

        public AisinoDataSet QueryCustomer(int pagesize, int pageno)
        {
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", string.Format("{0}%", selectBM));
            this.SQLID = "aisino.Fwkp.Bmgl.BMKH.KHbmlike";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            int num2;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", searchid);
            ArrayList list = this.baseDAO.querySQL("aisino.Fwkp.Bmgl.BMKH.KHRecommend", dictionary);
            List<string> list2 = new List<string>();
            list2.Clear();
            List<string> list3 = new List<string>();
            list3.Clear();
            foreach (Dictionary<string, object> dictionary2 in list)
            {
                int num;
                if (int.TryParse(dictionary2["BM"].ToString().Substring(searchid.Length), out num))
                {
                    list2.Add(dictionary2["BM"].ToString().Substring(searchid.Length));
                }
                else
                {
                    list3.Add(dictionary2["BM"].ToString().Substring(searchid.Length));
                }
            }
            string str = string.Empty;
            list2.Sort();
            if (list2.Count != 0)
            {
                str = searchid + list2[list2.Count - 1];
            }
            else if (list3.Count != 0)
            {
                str = searchid + new string('0', list3[0].Length);
            }
            else
            {
                str = null;
            }
            if (str == null)
            {
                if (searchid == "")
                {
                    return (searchid + "001");
                }
                return (searchid + "01");
            }
            string s = str.Substring(searchid.Length);
            if (!int.TryParse(s, out num2))
            {
                return searchid;
            }
            string str4 = (num2 + 1).ToString();
            if (str4.Length > s.Length)
            {
                return "NoNode";
            }
            str4 = str4.PadLeft(s.Length, '0');
            return (str.Remove(searchid.Length) + str4);
        }

        public string UpdateSubNodesSJBM(BMKHModel customer, string YuanBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", YuanBM);
            dictionary.Add("bmLike", YuanBM + "%");
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.KHBMGetSubNode", dictionary);
            List<string> list = new List<string>();
            List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
            try
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("bm", YuanBM);
                item.Add("afterbm", customer.BM);
                item.Add("MC", customer.MC);
                item.Add("JM", customer.JM);
                item.Add("KJM", customer.KJM);
                item.Add("SH", customer.SH);
                item.Add("DZDH", customer.DZDH);
                item.Add("YHZH", customer.YHZH);
                item.Add("YJDZ", customer.YJDZ);
                item.Add("BZ", customer.BZ);
                item.Add("YSKM", customer.YSKM);
                item.Add("DQBM", customer.DQBM);
                item.Add("DQMC", customer.DQMC);
                item.Add("DQKM", customer.DQKM);
                item.Add("SJBM", customer.SJBM);
                item.Add("SFZJY", customer.SFZJY);
                list.Add("aisino.Fwkp.Bmgl.BMKH.KHBMModify");
                list2.Add(item);
                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string str = table.Rows[i]["BM"].ToString();
                        string str2 = table.Rows[i]["SJBM"].ToString();
                        string str3 = customer.BM + str.Substring(YuanBM.Length);
                        string str4 = customer.BM + str2.Substring(YuanBM.Length);
                        Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                        dictionary3.Add("bm", str);
                        dictionary3.Add("newBM", str3);
                        dictionary3.Add("newSJBM", str4);
                        list.Add("aisino.Fwkp.Bmgl.BMKH.KHBMUpdateSJBMandBM");
                        list2.Add(dictionary3);
                    }
                }
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
            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMKH.KHBmZWverify", dictionary) >= 0x10)
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

