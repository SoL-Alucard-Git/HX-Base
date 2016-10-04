namespace Aisino.Fwkp.Bmgl.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Common;

    internal class BMSPFLManager
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        public string SQLID;

        public DataResult AddMerchandise(BMSPModel[] customers)
        {
            DataResult result = new DataResult {
                SuccessItems = new Dictionary<object, string>(),
                FailItems = new Dictionary<object, string>()
            };
            foreach (BMSPModel model in customers)
            {
                string str = this.AddMerchandise(model);
                if (str == "0")
                {
                    result.SuccessCount++;
                    result.SuccessItems.Add(model, str);
                }
                else
                {
                    result.FailCount++;
                    result.FailItems.Add(model, str);
                }
                result.TotalCount++;
            }
            return result;
        }

        public string AddMerchandise(BMSPModel merchandise)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BM", merchandise.BM);
            dictionary.Add("MC", merchandise.MC);
            dictionary.Add("JM", merchandise.JM);
            dictionary.Add("KJM", merchandise.KJM);
            dictionary.Add("SJBM", merchandise.SJBM);
            dictionary.Add("SLV", merchandise.SLV);
            dictionary.Add("SPSM", merchandise.SPSM);
            dictionary.Add("GGXH", merchandise.GGXH);
            dictionary.Add("JLDW", merchandise.JLDW);
            dictionary.Add("DJ", merchandise.DJ);
            dictionary.Add("HSJBZ", merchandise.HSJBZ);
            dictionary.Add("XSSRKM", merchandise.XSSRKM);
            dictionary.Add("YJZZSKM", merchandise.YJZZSKM);
            dictionary.Add("XSTHKM", merchandise.XSTHKM);
            dictionary.Add("HYSY", merchandise.HYSY);
            dictionary.Add("WJ", merchandise.WJ);
            dictionary.Add("XTHASH", merchandise.XTHASH);
            dictionary.Add("ISHIDE", merchandise.ISHIDE);
            dictionary.Add("XTCODE", merchandise.XTCODE);
            if (this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.SPAdd", dictionary) > 0)
            {
                return "0";
            }
            return "e02";
        }

        private void AddShowColumn(AisinoDataSet dataSet)
        {
            dataSet.Data.Constraints.Clear();
            dataSet.Data.Columns.Add("HZXStr");
            dataSet.Data.Columns.Add("ISHIDEStr");
            for (int i = 0; i < dataSet.Data.Rows.Count; i++)
            {
                string str = (string) dataSet.Data.Rows[i]["HZX"];
                bool flag = false;
                if (str.ToUpper() == "Y")
                {
                    flag = true;
                }
                dataSet.Data.Rows[i]["HZXStr"] = flag ? "是" : "否";
                int num2 = (int) dataSet.Data.Rows[i]["ISHIDE"];
                dataSet.Data.Rows[i]["ISHIDEStr"] = (num2 == 1) ? "是" : "否";
            }
        }

        public DataTable AppendByKey(string KeyWord, int TopNo, bool isSPBMSel)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("key", "%" + KeyWord + "%");
            dictionary.Add("TopNo", TopNo);
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (card.QYLX.ISDXQY && isSPBMSel)
            {
                dictionary.Add("DXQY", "true");
            }
            else
            {
                dictionary.Add("DXQY", "false");
            }
            if (card.QYLX.ISXT)
            {
                dictionary.Add("XTQY", "true");
            }
            else
            {
                dictionary.Add("XTQY", "false");
            }
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPFL.SPFLqueryKeyAppend", dictionary);
            if (KeyWord.Trim().Length == 0)
            {
                table.Rows.Clear();
            }
            return table;
        }

        public int BmMaxLenth(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM + "%");
            return this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSP.SPBmZWverify", dictionary);
        }

        public bool CanUseThisSPFLBM(string bm, bool isSPBMSel, bool isXTSP)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", bm);
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (card.QYLX.ISDXQY && isSPBMSel)
            {
                dictionary.Add("DXQY", 1);
            }
            else
            {
                dictionary.Add("DXQY", 0);
            }
            if (card.QYLX.ISXT)
            {
                dictionary.Add("XTQY", 1);
            }
            else
            {
                dictionary.Add("XTQY", 0);
            }
            dictionary.Add("XTSP", isXTSP ? 1 : 0);
            return (this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPFL.CanUseThisSPFLBM", dictionary).Rows.Count >= 1);
        }

        public bool CanUseThisYHZC(string bm)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", bm);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPFL.CanUseThisYHZC", dictionary);
            return ((table.Rows.Count >= 1) && (table.Rows[0]["ZZSTSGL"].ToString().Trim() != ""));
        }

        public string ChildDetermine(string BM, string SJBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sjbm", SJBM);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.SPChildDetermine", dictionary);
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

        public bool ContainXTSP(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SJBM", BM);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSP.ContainXTSP", dictionary) > 0);
        }

        public void DelCommonSPByName(string MC)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("mc", MC);
            this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.DelCommonSPByName", dictionary);
        }

        public string deleteFenLei(string searchid)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", searchid + "%");
            return this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.SPDeleteFenLei", dictionary).ToString();
        }

        public DataResult DeleteMerchandise(List<string> customerCodes)
        {
            DataResult result = new DataResult {
                SuccessItems = new Dictionary<object, string>(),
                FailItems = new Dictionary<object, string>()
            };
            foreach (string str in customerCodes)
            {
                string str2 = this.DeleteMerchandise(str);
                if (str2 == "0")
                {
                    result.SuccessCount++;
                    result.SuccessItems.Add(str, str2);
                }
                else
                {
                    result.FailCount++;
                    result.FailItems.Add(str, str2);
                }
                result.TotalCount++;
            }
            return result;
        }

        public string DeleteMerchandise(string customerCode)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", customerCode);
            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSP.SPExist", dictionary) == 0)
            {
                return "e1";
            }
            return this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.SPDelete", dictionary).ToString();
        }

        public DataResult DeleteMerchandiseFenLei(List<string> listFeiLeiCodes)
        {
            DataResult result = new DataResult {
                SuccessItems = new Dictionary<object, string>(),
                FailItems = new Dictionary<object, string>()
            };
            foreach (string str in listFeiLeiCodes)
            {
                string str2 = this.deleteFenLei(str);
                if (str2 != "0")
                {
                    result.SuccessCount++;
                    result.SuccessItems.Add(str, str2);
                }
                else
                {
                    result.FailCount++;
                    result.FailItems.Add(str, str2);
                }
                result.TotalCount++;
            }
            return result;
        }

        public string deleteNodes(string searchid)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", searchid);
            dictionary.Add("bmLike", searchid + "%");
            int num = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.SPDeleteSubNodes", dictionary);
            if (num > 0)
            {
                return num.ToString();
            }
            return "error delete";
        }

        public void DeleteXTSP()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.DeleteXT", dictionary);
        }

        public string ExecZengJianWei(string BM, bool isZengWei)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            dictionary.Add("bmLike", BM + "%");
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.SPBMGetSubNode", dictionary);
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
                num = this.baseDAO.未确认DAO方法3("aisino.Fwkp.Bmgl.BMSP.SPBMUpdateSJBMandBM", list);
            }
            if (num > 0)
            {
                return "0";
            }
            return "e1";
        }

        public bool ExistMerchandise(BMSPModel merchandise)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", merchandise.BM);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSP.SPExist", dictionary) > 0);
        }

        public bool ExistSameMCXH(BMSPModel merchandise, string OldBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (OldBM == "")
            {
                dictionary.Add("BM", merchandise.BM);
            }
            else
            {
                dictionary.Add("BM", OldBM);
            }
            dictionary.Add("MC", merchandise.MC);
            dictionary.Add("GGXH", merchandise.GGXH);
            if (string.IsNullOrEmpty(merchandise.GGXH))
            {
                return false;
            }
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSP.MCXHExist", dictionary) > 0);
        }

        public bool ExistXTSP()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMKH.XTSPExist", dictionary) > 0);
        }

        public bool FenLeiHasChild(string fenLeiBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", fenLeiBM);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSP.SPHasChild", dictionary) > 0);
        }

        public DataTable GetExportData()
        {
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.SPqueryKeyout", this.condition);
        }

        public string GetSBBPBBH()
        {
            string str = "0.0";
            try
            {
                string str2 = "aisino.Fwkp.Bmgl.BMSPF.GetSPBMBBBH";
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                str = this.baseDAO.queryValueSQL<string>(str2, dictionary);
                if ((str != null) && !(str == ""))
                {
                    return str;
                }
                str = "0.0";
            }
            catch (Exception)
            {
                throw;
            }
            return str;
        }

        public DataTable GetSJBM()
        {
            DataTable table = new DataTable();
            try
            {
                string str = "SELECT SJBM FROM BM_SPFL";
                table = this.baseDAO.querySQLDataTable(str);
            }
            catch (Exception)
            {
                throw;
            }
            return table;
        }

        internal DataTable GetSLV_BY_BM(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("key", BM);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPFL.SPFLquerySLV_BY_BM", dictionary);
        }

        public DataTable GetSPFL(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.SPFLGetModel", dictionary);
        }

        public string GetSPFLMCBYBM(string bm)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", bm);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPFL.GetSPFLMCBYBM", dictionary);
            if ((table != null) && (table.Rows.Count > 0))
            {
                return table.Rows[0]["MC"].ToString();
            }
            return string.Empty;
        }

        public string GetSPFLSLVBYBM(string bm)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", bm);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPFL.GetSPFLMCBYBM", dictionary);
            table.Constraints.Clear();
            for (int i = 1; i < table.Rows.Count; i++)
            {
                foreach (string str in table.Rows[i]["SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' }))
                {
                    if (!string.IsNullOrEmpty(str) && !table.Rows[0]["SLV"].ToString().Contains(str))
                    {
                        table.Rows[0]["SLV"] = table.Rows[0]["SLV"].ToString() + "，" + str;
                    }
                }
            }
            while (table.Rows.Count > 1)
            {
                table.Rows.RemoveAt(1);
            }
            if ((table != null) && (table.Rows.Count > 0))
            {
                return table.Rows[0]["SLV"].ToString();
            }
            return string.Empty;
        }

        public string GetSPFLSLVBYMC(string yhzcmc)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("mc", yhzcmc);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPFL.GetSPFLSLVBYMC", dictionary);
            if ((table != null) && (table.Rows.Count > 0))
            {
                return table.Rows[0]["SLV"].ToString();
            }
            return string.Empty;
        }

        public string[] GetSPXXByName(string name)
        {
            List<string> list = new List<string>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("spmc", name);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.SelectSPByMC", dictionary);
            if ((table != null) && (table.Rows.Count > 0))
            {
                int num = 0;
                for (num = 0; num < table.Rows.Count; num++)
                {
                    string item = table.Rows[num]["BM"].ToString();
                    string str2 = "";
                    if (table.Rows[num]["XTHASH"] != null)
                    {
                        str2 = table.Rows[num]["XTHASH"].ToString();
                    }
                    list.Add(item);
                    list.Add(str2);
                }
            }
            return list.ToArray();
        }

        public int GetSuggestBMLen(string SJBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SJBM", SJBM);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.GetXJBMBySJBM", dictionary);
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

        private List<string> GetUpdateBM(string BM)
        {
            List<string> updateBM = new List<string>();
            DataTable sPFL = this.GetSPFL(BM);
            string bM = "";
            if (sPFL.Rows.Count > 0)
            {
                bM = sPFL.Rows[0]["SJBM"].ToString().Trim();
            }
            if (bM != "")
            {
                updateBM = this.GetUpdateBM(bM);
                updateBM.Add(bM);
            }
            return updateBM;
        }

        public string GetXTCodeByName(string name)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("spmc", name);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMKH.XTSPByNameExist", dictionary);
            if ((table != null) && (table.Rows.Count > 0))
            {
                return table.Rows[0]["BM"].ToString();
            }
            return string.Empty;
        }

        public string InsertMerchandises(BMSPModel[] customers)
        {
            if ((customers == null) || (customers.Length < 1))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                this.baseDAO.Open();
                foreach (BMSPModel model in customers)
                {
                    dictionary.Clear();
                    dictionary.Add("BM", model.BM);
                    dictionary.Add("MC", model.MC);
                    dictionary.Add("JM", model.JM);
                    dictionary.Add("KJM", model.KJM);
                    dictionary.Add("SJBM", model.SJBM);
                    dictionary.Add("SLV", model.SLV);
                    dictionary.Add("SPSM", model.SPSM);
                    dictionary.Add("GGXH", model.GGXH);
                    dictionary.Add("JLDW", model.JLDW);
                    dictionary.Add("DJ", model.DJ);
                    dictionary.Add("HSJBZ", model.HSJBZ);
                    dictionary.Add("XSSRKM", model.XSSRKM);
                    dictionary.Add("YJZZSKM", model.YJZZSKM);
                    dictionary.Add("XSTHKM", model.XSTHKM);
                    dictionary.Add("HYSY", model.HYSY);
                    dictionary.Add("WJ", model.WJ);
                    dictionary.Add("XTHASH", model.XTHASH);
                    dictionary.Add("ISHIDE", model.ISHIDE);
                    dictionary.Add("XTCODE", model.XTCODE);
                    this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.SPAdd", dictionary);
                }
                this.baseDAO.Commit();
                this.baseDAO.Close();
                int length = customers.Length;
                return length.ToString();
            }
            catch (Exception)
            {
                this.baseDAO.RollBack();
                return "e02";
            }
        }

        public bool JianWeiVerify(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            return (this.baseDAO.queryValueSQL<string>("aisino.Fwkp.Bmgl.BMSP.SPBmJWVerify", dictionary) == "0");
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            List<TreeNodeTemp> list = new List<TreeNodeTemp>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("sjbm", searchid);
                DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPFL.SPFLTreeView", dictionary);
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

        public List<TreeNodeTemp> listNodesISHIDE(string searchid)
        {
            List<TreeNodeTemp> list = new List<TreeNodeTemp>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("sjbm", searchid);
                DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPFL.SPFLTreeViewISHIDE", dictionary);
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

        public string ModifyMerchandise(BMSPModel merchandise, string OldBM)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("oldBM", OldBM);
                dictionary.Add("BM", merchandise.BM);
                dictionary.Add("MC", merchandise.MC);
                dictionary.Add("JM", merchandise.JM);
                dictionary.Add("KJM", merchandise.KJM);
                dictionary.Add("SJBM", merchandise.SJBM);
                dictionary.Add("SLV", merchandise.SLV);
                dictionary.Add("SPSM", merchandise.SPSM);
                dictionary.Add("GGXH", merchandise.GGXH);
                dictionary.Add("JLDW", merchandise.JLDW);
                dictionary.Add("DJ", merchandise.DJ);
                dictionary.Add("HSJBZ", merchandise.HSJBZ);
                dictionary.Add("XSSRKM", merchandise.XSSRKM);
                dictionary.Add("YJZZSKM", merchandise.YJZZSKM);
                dictionary.Add("XSTHKM", merchandise.XSTHKM);
                dictionary.Add("HYSY", merchandise.HYSY);
                dictionary.Add("ISHIDE", merchandise.ISHIDE);
                if (this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.SPModify", dictionary) > 0)
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

        public DataTable QueryAllXTSP()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.QueryAllXTSP", dictionary);
            if ((table != null) && (table.Rows.Count != 0))
            {
                return table;
            }
            return null;
        }

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", "%" + KeyWord + "%");
            this.SQLID = "aisino.Fwkp.Bmgl.BMSPFL.SPFLqueryKey";
            AisinoDataSet dataSet = this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
            this.AddShowColumn(dataSet);
            return dataSet;
        }

        public DataTable QueryByKeyAndSlv(string KeyWord, double Slv, int WJ, int Top)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("key", "%" + KeyWord + "%");
            dictionary.Add("SLV", Slv);
            dictionary.Add("WJ", WJ);
            dictionary.Add("Top", Top);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.SPqueryKeySlv", dictionary);
            if (KeyWord.Trim().Length == 0)
            {
                table.Rows.Clear();
            }
            return table;
        }

        public DataTable QueryByKeyAndSlvSEL(string KeyWord, double Slv, int WJ, int Top)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("key", "%" + KeyWord + "%");
            dictionary.Add("SLV", Slv);
            dictionary.Add("WJ", WJ);
            dictionary.Add("Top", Top);
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.SPqueryKeySlvSEL", dictionary);
            if (KeyWord.Trim().Length == 0)
            {
                table.Rows.Clear();
            }
            return table;
        }

        public DataTable QueryByKeyAndSpecialSPSEL(string KeyWord, string specialSP, int WJ, int Top)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("key", "%" + KeyWord + "%");
            dictionary.Add("WJ", WJ);
            dictionary.Add("Top", Top);
            if (specialSP == "HYSY")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeyAndHYSYSEL";
            }
            else if (specialSP == "XT")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeyAndXTSEL";
            }
            else if (specialSP == "SNY")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeyAndSNYSEL";
            }
            DataTable table = this.baseDAO.querySQLDataTable(this.SQLID, dictionary);
            if (KeyWord.Trim().Length == 0)
            {
                table.Rows.Clear();
            }
            return table;
        }

        public AisinoDataSet QueryByKeyDisplaySEL(string KeyWord, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", "%" + KeyWord + "%");
            this.SQLID = "aisino.Fwkp.Bmgl.BMSPFL.SPFLqueryKeyDisplaySEL";
            AisinoDataSet dataSet = this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
            this.AddShowColumn(dataSet);
            return dataSet;
        }

        public AisinoDataSet QueryByKeyDisplaySEL(string KeyWord, double slv, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", "%" + KeyWord + "%");
            this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeyAndSlvDisplaySEL";
            AisinoDataSet dataSet = this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
            this.AddShowColumn(dataSet);
            return dataSet;
        }

        public AisinoDataSet QueryByKeyDisplaySEL(string KeyWord, string specialSP, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", "%" + KeyWord + "%");
            if (specialSP == "HYSY")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeyHYSYDisplaySEL";
            }
            else if (specialSP == "XT")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeyXTDisplaySEL";
            }
            else if (specialSP == "SNY")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeySNYDisplaySEL";
            }
            else if (specialSP == "Except_HYSY")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeyExceptHYSYDisplaySEL";
            }
            AisinoDataSet dataSet = this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
            this.AddShowColumn(dataSet);
            return dataSet;
        }

        public AisinoDataSet QueryByKeySEL(string KeyWord, int pagesize, int pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", "%" + KeyWord + "%");
            this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeySEL";
            AisinoDataSet dataSet = this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
            this.AddShowColumn(dataSet);
            return dataSet;
        }

        public AisinoDataSet QueryMerchandise(int pagesize, int pageno)
        {
            AisinoDataSet dataSet = this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
            this.AddShowColumn(dataSet);
            return dataSet;
        }

        public AisinoDataSet SelectNodeDisplay(string selectedBM, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", selectedBM + "%");
            this.SQLID = "aisino.Fwkp.Bmgl.BMSPFL.SPFLbmlike";
            AisinoDataSet dataSet = this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
            this.AddShowColumn(dataSet);
            return dataSet;
        }

        public AisinoDataSet SelectNodeDisplay(string selectedBM, double slv, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("BM", selectedBM + "%");
            this.condition.Add("SLV", slv);
            this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPbmlikeAndSlv";
            AisinoDataSet dataSet = this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
            this.AddShowColumn(dataSet);
            return dataSet;
        }

        public AisinoDataSet SelectNodeDisplaySEL(string selectedBM, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", selectedBM + "%");
            this.SQLID = "aisino.Fwkp.Bmgl.BMSPFL.SPFLbmlikeSEL";
            AisinoDataSet dataSet = this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
            this.AddShowColumn(dataSet);
            return dataSet;
        }

        public AisinoDataSet SelectNodeDisplaySEL(string selectedBM, double slv, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("BM", selectedBM + "%");
            this.condition.Add("SLV", slv);
            this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPbmlikeAndSlvSEL";
            AisinoDataSet dataSet = this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
            this.AddShowColumn(dataSet);
            return dataSet;
        }

        public AisinoDataSet SelectNodeDisplaySEL(string selectedBM, string specialSP, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("BM", "%" + selectedBM + "%");
            if (specialSP == "HYSY")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPbmlikeAndHYSYSEL";
            }
            else if (specialSP == "XT")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPbmlikeAndXTSEL";
            }
            else if (specialSP == "SNY")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPbmlikeAndSNYSEL";
            }
            else if (specialSP == "Except_HYSY")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPbmlikeAndExcept_HYSYSEL";
            }
            AisinoDataSet dataSet = this.baseDAO.querySQLDataSet(this.SQLID, this.condition, Pagesize, Pageno);
            this.AddShowColumn(dataSet);
            return dataSet;
        }

        public string TuiJianBM(string searchid)
        {
            int num2;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", searchid);
            dictionary.Add("bmLike", searchid + "%");
            ArrayList list = this.baseDAO.querySQL("aisino.Fwkp.Bmgl.BMSP.SPRecommend", dictionary);
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

        public Dictionary<string, int> UpdateDatabaseBySPFL(string SPFL)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SPFL", SPFL);
            int num = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSPFL.UpdateSPBySPFL", dictionary);
            int num2 = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSPFL.UpdateCLBySPFL", dictionary);
            int num3 = this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSPFL.UpdateFYXMBySPFL", dictionary);
            Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
            dictionary2.Add("SP", num);
            dictionary2.Add("CL", num2);
            dictionary2.Add("FYXM", num3);
            return dictionary2;
        }

        public void UpdateHZXIsHideDownToUp(string sjbm, bool hide)
        {
            List<string> updateBM = this.GetUpdateBM(sjbm);
            updateBM.Add(sjbm);
            string str = string.Empty;
            if (hide)
            {
                str = "1";
            }
            else
            {
                str = "0";
            }
            List<string> sqlID = new List<string>();
            List<Dictionary<string, object>> param = new List<Dictionary<string, object>>();
            for (int i = 0; i < updateBM.Count; i++)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("BM", updateBM[i]);
                item.Add("ISHIDE", str);
                sqlID.Add("aisino.Fwkp.Bmgl.BMSPFL.SPFLHZXUpdateXTHIDEDownToUp");
                param.Add(item);
            }
            new BMSPFLManager().UpdateSPFLTable(sqlID, param, false);
        }

        public void UpdateHZXIsHideUpToDown(string sjbm, bool hide)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SJBM", sjbm + "%");
            string str = string.Empty;
            if (hide)
            {
                str = "1";
            }
            else
            {
                str = "0";
            }
            dictionary.Add("ISHIDE", str);
            this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSPFL.SPFLHZXUpdateXTHIDEUpToDown", dictionary);
        }

        public void UpdateSPFLIsHide(string bm, bool isHide)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BM", bm);
            string str = string.Empty;
            if (isHide)
            {
                str = "1";
            }
            else
            {
                str = "0";
            }
            dictionary.Add("ISHIDE", str);
            this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSPFL.SPFLUpdateHIDE", dictionary);
        }

        public bool UpdateSPFLTable(List<string> sqlID, List<Dictionary<string, object>> param, bool isAuto)
        {
            bool flag = false;
            if (((sqlID != null) && (sqlID.Count >= 1)) && ((param != null) && (param.Count >= 1)))
            {
                try
                {
                    int num = this.baseDAO.未确认DAO方法1(sqlID.ToArray(), param);
                    if (num > 0)
                    {
                        flag = true;
                    }
                    if (!isAuto)
                    {
                    }
                }
                catch (Exception)
                {
                }
            }
            return flag;
        }

        public string UpdateSubNodesSJBM(BMSPModel merchandise, string YuanBM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", YuanBM);
            dictionary.Add("bmLike", YuanBM + "%");
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.SPBMGetSubNode", dictionary);
            List<string> list = new List<string>();
            List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
            try
            {
                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string str = table.Rows[i]["BM"].ToString();
                        string str2 = table.Rows[i]["SJBM"].ToString();
                        string str3 = merchandise.BM + str.Substring(YuanBM.Length);
                        string str4 = merchandise.BM + str2.Substring(YuanBM.Length);
                        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                        dictionary2.Add("bm", str);
                        dictionary2.Add("newBM", str3);
                        dictionary2.Add("newSJBM", str4);
                        list.Add("aisino.Fwkp.Bmgl.BMSP.SPBMUpdateSJBMandBM");
                        list2.Add(dictionary2);
                    }
                }
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("bm", YuanBM);
                item.Add("afterbm", merchandise.BM);
                item.Add("MC", merchandise.MC);
                item.Add("JM", merchandise.JM);
                item.Add("KJM", merchandise.KJM);
                item.Add("SJBM", merchandise.SJBM);
                item.Add("SLV", merchandise.SLV);
                item.Add("SPSM", merchandise.SPSM);
                item.Add("GGXH", merchandise.GGXH);
                item.Add("JLDW", merchandise.JLDW);
                item.Add("DJ", merchandise.DJ);
                item.Add("HSJBZ", merchandise.HSJBZ);
                item.Add("XSSRKM", merchandise.XSSRKM);
                item.Add("YJZZSKM", merchandise.YJZZSKM);
                item.Add("XSTHKM", merchandise.XSTHKM);
                list.Add("aisino.Fwkp.Bmgl.BMSP.SPBMModify");
                list2.Add(item);
                if (this.baseDAO.未确认DAO方法1(list.ToArray(), list2) > 0)
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

        public void UpdateXTIsHide(string sjbm, string hide)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SJBM", sjbm);
            dictionary.Add("ISHIDE", hide);
            this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.SPBMUpdateXTHIDE", dictionary);
        }

        public bool XTUseThisSPFLBM(string bm, bool isxtsp)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", bm);
            if (TaxCardFactory.CreateTaxCard().QYLX.ISXT)
            {
                dictionary.Add("XTQY", "true");
            }
            else
            {
                dictionary.Add("XTQY", "false");
            }
            if (isxtsp)
            {
                dictionary.Add("XTSP", "true");
            }
            else
            {
                dictionary.Add("XTSP", "false");
            }
            return (this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSPFL.XTCanUseThisSPFLBM", dictionary).Rows.Count >= 1);
        }

        public bool ZengWeiVerify(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM + "%");
            if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSP.SPBmZWverify", dictionary) >= 0x10)
            {
                return false;
            }
            return true;
        }
    }
}

