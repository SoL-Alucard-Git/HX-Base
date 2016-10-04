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
    using Common;
    internal class BMCLManager : IBMCLManager
    {
        private int _currentPage = 1;
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        private string SQLID;

        public string AddCustomer(BMCLModel car)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BM", car.BM);
            dictionary.Add("MC", car.MC);
            dictionary.Add("JM", car.JM);
            dictionary.Add("CPXH", car.CPXH);
            dictionary.Add("CD", car.CD);
            dictionary.Add("SCCJMC", car.SCCJMC);
            dictionary.Add("KJM", car.KJM);
            dictionary.Add("SJBM", car.SJBM);
            dictionary.Add("WJ", car.WJ);
            dictionary.Add("SPFL", car.SPFL);
            dictionary.Add("YHZC", car.YHZC);
            dictionary.Add("SPFLMC", car.SPFLMC);
            dictionary.Add("YHZCMC", car.YHZCMC);
            if (this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMCL.CLAdd", dictionary) > 0)
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
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.CLqueryKeyAppendWJ", dictionary);
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
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.CLqueryKeyAppendWJ", dictionary);
            if (KeyWord.Trim().Length == 0)
            {
                table.Rows.Clear();
            }
            return table;
        }

        public string AutoNodeLogic(string DLMC)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", DLMC);
            dictionary.Add("SJBM", "");
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.CLMCExist", dictionary);
            int count = table.Rows.Count;
            if (count == 0)
            {
                string str = this.TuiJianBM("");
                BMCLModel car = new BMCLModel {
                    BM = str,
                    MC = DLMC,
                    SJBM = "",
                    WJ = 0
                };
                if ("0" == this.AddCustomer(car))
                {
                    return car.BM;
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
            return this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMCL.CLBmZWverify", dictionary);
        }

        public string ChildDetermine(string BM, string SJBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sjbm", SJBM);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.CLChildDetermine", dictionary);
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

        public string DeleteCustomer(string carCode)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", carCode);
            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMCL.CLExist", dictionary) == 0)
            {
                return "eDoseNotExist";
            }
            int num2 = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMCL.CLDelete", dictionary);
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
            return this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMCL.CLDeleteFenLei", dictionary).ToString();
        }

        public string deleteNodes(string searchid)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bmLike", searchid + "%");
            int num = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMCL.CLDeleteSubNodes", dictionary);
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
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.CLBMGetSubNode", dictionary);
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
                num = this.baseDAO.未确认DAO方法3("aisino.Fwkp.Bmgl.BMCL.CLBMUpdateSJBMandBM", list);
            }
            if (num > 0)
            {
                return "0";
            }
            return "e1";
        }

        public bool ExistCustomer(BMCLModel car)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", car.BM);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMCL.CLExist", dictionary) > 0);
        }

        public bool FenLeiHasChild(string fenLeiBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", fenLeiBM);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMCL.CLHasChild", dictionary) > 0);
        }

        public DataTable GetExportData()
        {
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.CLqueryKeyout", this.condition);
        }

        public DataTable GetKH(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.CLGetModel", dictionary);
        }

        public int GetSuggestBMLen(string SJBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SJBM", SJBM);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.GetXJBMBySJBM", dictionary);
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
            return (this.baseDAO.queryValueSQL<string>("aisino.Fwkp.Bmgl.BMCL.CLBmJWVerify", dictionary) == "0");
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            List<TreeNodeTemp> list = new List<TreeNodeTemp>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("sjbm", searchid);
                DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.CLTreeView", dictionary);
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

        public string ModifyCustomer(BMCLModel car, string OldBM)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("oldBM", OldBM);
                dictionary.Add("BM", car.BM);
                dictionary.Add("MC", car.MC);
                dictionary.Add("JM", car.JM);
                dictionary.Add("SJBM", car.SJBM);
                dictionary.Add("CPXH", car.CPXH);
                dictionary.Add("CD", car.CD);
                dictionary.Add("SCCJMC", car.SCCJMC);
                dictionary.Add("KJM", car.KJM);
                dictionary.Add("WJ", car.WJ);
                dictionary.Add("SPFL", car.SPFL);
                dictionary.Add("YHZC", car.YHZC);
                dictionary.Add("SPFLMC", car.SPFLMC);
                dictionary.Add("YHZCMC", car.YHZCMC);
                if (this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMCL.CLModify", dictionary) > 0)
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
            this.SQLID = "aisino.Fwkp.Bmgl.BMCL.CLqueryKey";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
        }

        public DataTable QueryByMC(string MC)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", MC);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.CLQueryMC", dictionary);
        }

        public DataTable QueryByMCAndSJBM(string MC, string SJBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", MC);
            dictionary.Add("SJBM", SJBM);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.CLQueryMCAndSJBM", dictionary);
        }

        public DataTable QueryCLSPFLLNotEmpty()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.QueryCLSPFLNotEmpty", dictionary);
            if ((table != null) && (table.Rows.Count != 0))
            {
                return table;
            }
            return null;
        }

        public DataTable QueryCLYHZCIsAndYHZCMCIsEmpty()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.QueryCLYHZCIsAndYHZCMCIsEmpty", dictionary);
            if ((table != null) && (table.Rows.Count != 0))
            {
                return table;
            }
            return null;
        }

        public AisinoDataSet QueryCustomer(int pagesize, int pageno)
        {
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", string.Format("{0}%", selectBM));
            this.SQLID = "aisino.Fwkp.Bmgl.BMCL.CLbmlike";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            int num2;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", searchid);
            ArrayList list = this.baseDAO.querySQL("aisino.Fwkp.Bmgl.BMCL.CLRecommend", dictionary);
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

        public string UpdateSubNodesSJBM(BMCLModel car, string YuanBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", YuanBM);
            dictionary.Add("bmLike", YuanBM + "%");
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMCL.CLBMGetSubNode", dictionary);
            List<string> list = new List<string>();
            List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
            try
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("bm", YuanBM);
                item.Add("afterbm", car.BM);
                item.Add("MC", car.MC);
                item.Add("JM", car.JM);
                item.Add("CPXH", car.CPXH);
                item.Add("CD", car.CD);
                item.Add("SCCJMC", car.SCCJMC);
                item.Add("SJBM", car.SJBM);
                list.Add("aisino.Fwkp.Bmgl.BMCL.CLBMModify");
                list2.Add(item);
                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string str = table.Rows[i]["BM"].ToString();
                        string str2 = table.Rows[i]["SJBM"].ToString();
                        string str3 = car.BM + str.Substring(YuanBM.Length);
                        string str4 = car.BM + str2.Substring(YuanBM.Length);
                        Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                        dictionary3.Add("bm", str);
                        dictionary3.Add("newBM", str3);
                        dictionary3.Add("newSJBM", str4);
                        list.Add("aisino.Fwkp.Bmgl.BMCL.CLBMUpdateSJBMandBM");
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
            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMCL.CLBmZWverify", dictionary) >= 0x10)
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

