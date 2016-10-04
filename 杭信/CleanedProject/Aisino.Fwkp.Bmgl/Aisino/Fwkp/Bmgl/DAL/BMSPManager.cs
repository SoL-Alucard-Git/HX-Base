namespace Aisino.Fwkp.Bmgl.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using Aisino.Fwkp.Bmgl.IDAL;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Common;

    internal class BMSPManager : IBMSPManager
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
            dictionary.Add("SPFL", merchandise.SPFL);
            dictionary.Add("YHZC", merchandise.YHZC);
            dictionary.Add("SPFLMC", merchandise.SPFLMC);
            dictionary.Add("YHZCMC", merchandise.YHZCMC);
            dictionary.Add("LSLVBS", merchandise.LSLVBS);
            if (this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.SPAdd", dictionary) > 0)
            {
                return "0";
            }
            return "e02";
        }

        private void AddShowColumn(AisinoDataSet dataSet)
        {
            dataSet.Data.Constraints.Clear();
            dataSet.Data.Columns.Add("SLVStr");
            dataSet.Data.Columns.Add("DJStr");
            dataSet.Data.Columns.Add("ISHIDEBOOL");
            dataSet.Data.Columns.Add("HSJBZSTR");
            for (int i = 0; i < dataSet.Data.Rows.Count; i++)
            {
                double num2 = (double) dataSet.Data.Rows[i]["SLV"];
                double num3 = (double) dataSet.Data.Rows[i]["DJ"];
                string str = (string) dataSet.Data.Rows[i]["ISHIDE"];
                bool flag = (bool) dataSet.Data.Rows[i]["HSJBZ"];
                int num4 = (int) dataSet.Data.Rows[i]["WJ"];
                bool flag2 = (bool) dataSet.Data.Rows[i]["HYSY"];
                string str2 = dataSet.Data.Rows[i]["LSLVBS"].ToString();
                if ((num2 == 0.05) && flag2)
                {
                    dataSet.Data.Rows[i]["SLVStr"] = "中外合作油气田";
                }
                else if ((("0.00" == num2.ToString("f2")) && (str2 == "1")) && Flbm.IsYM())
                {
                    dataSet.Data.Rows[i]["SLVStr"] = "免税";
                }
                else if ((("0.00" == num2.ToString("f2")) && (str2 == "2")) && Flbm.IsYM())
                {
                    dataSet.Data.Rows[i]["SLVStr"] = "不征税";
                }
                else if ((("0.00" == num2.ToString("f2")) && (num4 == 1)) && Flbm.IsYM())
                {
                    dataSet.Data.Rows[i]["SLVStr"] = "0%";
                }
                else if ("0.015" == num2.ToString("f3"))
                {
                    dataSet.Data.Rows[i]["SLVStr"] = "减按1.5%计算";
                }
                else if ((("0.00" == num2.ToString("f2")) && (num4 == 1)) && !Flbm.IsYM())
                {
                    dataSet.Data.Rows[i]["SLVStr"] = "免税";
                }
                else
                {
                    dataSet.Data.Rows[i]["SLVStr"] = (num2 == 0.0) ? "" : num2.ToString("P0");
                }
                dataSet.Data.Rows[i]["ISHIDEBOOL"] = (str == "0000000000") ? "否" : "是";
                if (num4 == 1)
                {
                    dataSet.Data.Rows[i]["DJStr"] = (num3 == 0.0) ? "" : num3.ToString("C");
                    dataSet.Data.Rows[i]["HSJBZSTR"] = flag ? "是" : "否";
                }
            }
        }

        public DataTable AppendByKey(string KeyWord, int TopNo, double slv, string specialSP, string specialFlag)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("key", "%" + KeyWord + "%");
            dictionary.Add("TopNo", TopNo);
            if ("0" == specialFlag)
            {
                dictionary.Add("MUST_WITH_SPFL", "true");
            }
            else
            {
                dictionary.Add("MUST_WITH_SPFL", "false");
            }
            if (specialSP == string.Empty)
            {
                dictionary.Add("slv", slv);
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeySlvAppend";
            }
            else if (specialSP == "HYSY")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeyHYSYAppend";
            }
            else if (specialSP == "XT")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeyXTAppend";
            }
            else if (specialSP == "OPF")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeyOPFAppend";
            }
            else if (specialSP == "SNY")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeySNYAppend";
            }
            DataTable data = this.baseDAO.querySQLDataTable(this.SQLID, dictionary);
            AisinoDataSet dataSet = new AisinoDataSet {
                Data = data
            };
            this.AddShowColumn(dataSet);
            data = dataSet.Data;
            if (KeyWord.Trim().Length == 0)
            {
                data.Rows.Clear();
            }
            return data;
        }

        public int BmMaxLenth(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM + "%");
            return this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bmgl.BMSP.SPBmZWverify", dictionary);
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

        public DataTable GetSP(string BM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("bm", BM);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.SPGetModel", dictionary);
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
                    dictionary.Add("SPFL", model.SPFL);
                    dictionary.Add("YHZC", model.YHZC);
                    dictionary.Add("SPFLMC", model.SPFLMC);
                    dictionary.Add("YHZCMC", model.YHZCMC);
                    dictionary.Add("LSLVBS", model.LSLVBS);
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
                DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.SPTreeView", dictionary);
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
                DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.SPTreeViewISHIDE", dictionary);
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
                dictionary.Add("SPFL", merchandise.SPFL);
                dictionary.Add("YHZC", merchandise.YHZC);
                dictionary.Add("SPFLMC", merchandise.SPFLMC);
                dictionary.Add("YHZCMC", merchandise.YHZCMC);
                dictionary.Add("LSLVBS", merchandise.LSLVBS);
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
            this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKey";
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
            this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPqueryKeyDisplaySEL";
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

        public DataTable QuerySPNotYHZCMC()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.QuerySPNotYHZCMC", dictionary);
            if ((table != null) && (table.Rows.Count != 0))
            {
                return table;
            }
            return null;
        }

        public DataTable QuerySPSPFLLNotEmptyAndNotXT()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Bmgl.BMSP.QuerySPSPFLNotEmptyAndNotXT", dictionary);
            if ((table != null) && (table.Rows.Count != 0))
            {
                return table;
            }
            return null;
        }

        public AisinoDataSet SelectNodeDisplay(string selectedBM, int Pagesize, int Pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", selectedBM + "%");
            this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPbmlike";
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
            this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPbmlikeSEL";
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
            this.condition.Add("BM", selectedBM + "%");
            if (specialSP == "HYSY")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPbmlikeAndHYSYSEL";
            }
            else if (specialSP == "XT")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPbmlikeAndXTSEL";
            }
            else if (specialSP == "OPF")
            {
                this.SQLID = "aisino.Fwkp.Bmgl.BMSP.SPbmlikeAndOPFSEL";
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

        public void Update_SP()
        {
            List<string> sqlID = new List<string>();
            List<Dictionary<string, object>> param = new List<Dictionary<string, object>>();
            DataTable table = this.QuerySPNotYHZCMC();
            if (table != null)
            {
                BMSPFLManager manager = new BMSPFLManager();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string str = table.Rows[i]["BM"].ToString();
                    string str2 = table.Rows[i]["MC"].ToString();
                    string slv = table.Rows[i]["SLV"].ToString();
                    bool flag = bool.Parse(table.Rows[i]["HYSY"].ToString());
                    string bm = table.Rows[i]["SPFL"].ToString();
                    string str5 = table.Rows[i]["YHZC"].ToString();
                    string str6 = table.Rows[i]["YHZCMC"].ToString();
                    string str7 = table.Rows[i]["LSLVBS"].ToString();
                    string sPFLMCBYBM = manager.GetSPFLMCBYBM(bm);
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                    item.Add("BM", bm);
                    ArrayList list3 = baseDAOSQLite.querySQL("aisino.Fwkp.Bmgl.BMSPFL.SelectYHZCS", item);
                    str5 = "否";
                    bool ishysy = flag;
                    foreach (Dictionary<string, object> dictionary2 in list3)
                    {
                        string[] strArray = dictionary2["ZZSTSGL"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < strArray.Length; j++)
                        {
                            if (this.yhzc_contain_slv(strArray[j], slv, ishysy))
                            {
                                str5 = "是";
                                str6 = strArray[j];
                                if ((str6 == "出口零税") && (slv == "0"))
                                {
                                    str7 = "0";
                                }
                                else if ((str6 == "免税") && (slv == "0"))
                                {
                                    str7 = "1";
                                }
                                else if ((str6 == "不征税") && (slv == "0"))
                                {
                                    str7 = "2";
                                }
                                break;
                            }
                        }
                    }
                    item.Clear();
                    item.Add("SPFLMC", sPFLMCBYBM);
                    item.Add("YHZC", str5);
                    item.Add("YHZCMC", str6);
                    item.Add("BM", str);
                    item.Add("MC", str2);
                    item.Add("LSLVBS", str7);
                    sqlID.Add("aisino.Fwkp.Bmgl.BMSP.UpdataBM_SP");
                    param.Add(item);
                }
                new BMSPFLManager().UpdateSPFLTable(sqlID, param, false);
            }
        }

        public void UpdateSPIsHide(string bm, bool isHide)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BM", bm);
            string str = string.Empty;
            if (isHide)
            {
                str = "1000000000";
            }
            else
            {
                str = "0000000000";
            }
            dictionary.Add("ISHIDE", str);
            this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.SPBMUpdateSPHIDE", dictionary);
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

        public bool yhzc_contain_slv(string yhzc, string slv, bool ishysy)
        {
            string str = "aisino.Fwkp.Bmgl.BMSPFL.SelectYhzcs";
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            ArrayList list = baseDAOSQLite.querySQL(str, dictionary);
            if (ishysy)
            {
                foreach (Dictionary<string, object> dictionary2 in list)
                {
                    if ((dictionary2["YHZCMC"].ToString() == yhzc) && (dictionary2["SLV"].ToString() == ""))
                    {
                        return true;
                    }
                }
                return false;
            }
            slv = ((double.Parse(slv) * 100.0)).ToString() + "%";
            foreach (Dictionary<string, object> dictionary3 in list)
            {
                if ((dictionary3["YHZCMC"].ToString() == yhzc) && (dictionary3["SLV"].ToString() == ""))
                {
                    return true;
                }
                if ((dictionary3["YHZCMC"].ToString() == yhzc) && dictionary3["SLV"].ToString().Contains(slv))
                {
                    return true;
                }
            }
            return false;
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

