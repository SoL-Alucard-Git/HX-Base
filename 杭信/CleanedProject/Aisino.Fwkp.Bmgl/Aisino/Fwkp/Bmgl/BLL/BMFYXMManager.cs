namespace Aisino.Fwkp.Bmgl.BLL
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.DAL;
    using Aisino.Fwkp.Bmgl.IBLL;
    using Aisino.Fwkp.Bmgl.Model;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    internal sealed class BMFYXMManager : IBMBaseManager
    {
        private int _currentPage = 1;
        private DAL.BMFYXMManager bmfyxmDAL = new DAL.BMFYXMManager();
        private InvoiceHandler jyExpense = new InvoiceHandler();

        public string AddExpense(BMFYXMModel feiyong)
        {
            return this.AddExpenseKP(feiyong, 0);
        }

        public string AddExpenseKP(BMFYXMModel feiyong, int Addtype)
        {
            string str = this.CheckExpense(feiyong);
            if (str == "0")
            {
                if (TaxCardFactory.CreateTaxCard().QYLX.ISHY && this.bmfyxmDAL.ExistCusMC(feiyong.BM, feiyong.MC, feiyong.SJBM))
                {
                    MessageManager.ShowMsgBox("INP-235136");
                    return "费用项目已存在";
                }
                if (this.bmfyxmDAL.ExistExpense(feiyong))
                {
                    MessageManager.ShowMsgBox("INP-235108");
                    return "编码已存在";
                }
                str = this.bmfyxmDAL.AddExpense(feiyong);
                if ((Addtype == 1) && (str == "0"))
                {
                    MessageManager.ShowMsgBox("INP-235201");
                }
            }
            return str;
        }

        public DataTable AppendByKey(string KeyWord, int TopNo)
        {
            return this.bmfyxmDAL.AppendByKey(KeyWord, TopNo);
        }

        public DataTable AppendByKeyWJ(string KeyWord, int TopNo)
        {
            return this.bmfyxmDAL.AppendByKeyWJ(KeyWord, TopNo);
        }

        public string CheckExpense(BMFYXMModel feiyong)
        {
            if (feiyong == null)
            {
                return "e11";
            }
            if ((feiyong.BM == null) || (feiyong.BM.Length == 0))
            {
                return "e12";
            }
            if ((feiyong.MC != null) && (feiyong.MC.Length != 0))
            {
                return "0";
            }
            return "e13";
        }

        public string ChildDetermine(string BM, string SJBM)
        {
            return this.bmfyxmDAL.ChildDetermine(BM, SJBM);
        }

        public string DeleteData(string feiyongCode)
        {
            return this.bmfyxmDAL.DeleteExpense(feiyongCode);
        }

        public string DeleteDataFenLei(string FenLeiCodeBM)
        {
            if (!this.bmfyxmDAL.FenLeiHasChild(FenLeiCodeBM))
            {
                return this.bmfyxmDAL.deleteFenLei(FenLeiCodeBM);
            }
            return "e";
        }

        public string deleteFenLei(string flBM)
        {
            return this.bmfyxmDAL.deleteFenLei(flBM);
        }

        public string deleteNodes(string searchid)
        {
            return this.bmfyxmDAL.deleteNodes(searchid);
        }

        public string ExecZengJianWei(string BM, bool isZengWei)
        {
            if (isZengWei)
            {
                if (this.bmfyxmDAL.ZengWeiVerify(BM))
                {
                    return this.bmfyxmDAL.ExecZengJianWei(BM, isZengWei);
                }
                return "ezw";
            }
            if (this.bmfyxmDAL.JianWeiVerify(BM))
            {
                return this.bmfyxmDAL.ExecZengJianWei(BM, isZengWei);
            }
            return "ejw";
        }

        public string ExportData(string pathFile, string separator, DataTable khTable = null)
        {
            string path = pathFile.Remove(pathFile.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DataTable exportData = this.bmfyxmDAL.GetExportData();
            if (exportData.Rows.Count == 0)
            {
                return "没有费用项目";
            }
            StringBuilder builder = new StringBuilder();
            using (StreamWriter writer = new StreamWriter(pathFile, false, ToolUtil.GetEncoding()))
            {
                writer.WriteLine("{费用项目编码}[分隔符]\"" + separator + "\"");
                writer.WriteLine("// 每行格式 :");
                string str2 = "";
                if (Flbm.IsYM())
                {
                    str2 = "// 编码~~名称~~简码~~商品分类~~是否享受优惠政策~~税收分类名称~~优惠政策类型~~编码版本号";
                }
                else
                {
                    str2 = "// 编码~~名称~~简码";
                }
                writer.WriteLine(str2.Replace(",", separator));
                string maxBMBBBH = new SPFLService().GetMaxBMBBBH();
                for (int i = 0; i < exportData.Rows.Count; i++)
                {
                    builder.Remove(0, builder.Length);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["BM"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["MC"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["JM"].ToString(), separator));
                    if (Flbm.IsYM())
                    {
                        builder.Append(separator);
                        builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["SPFL"].ToString(), separator));
                        builder.Append(separator);
                        builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["YHZC"].ToString(), separator));
                        builder.Append(separator);
                        builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["SPFLMC"].ToString(), separator));
                        builder.Append(separator);
                        builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["YHZCMC"].ToString(), separator));
                        builder.Append(separator);
                        builder.Append(GetSafeData.ExportItem(maxBMBBBH, separator));
                    }
                    builder.Replace(",,", ",\"\",");
                    writer.WriteLine(builder.ToString());
                }
            }
            return "0";
        }

        public DataTable GetFYXM(string BM)
        {
            return this.bmfyxmDAL.GetFYXM(BM);
        }

        public BMBaseModel GetModel(string BM)
        {
            DataTable fYXM = this.bmfyxmDAL.GetFYXM(BM);
            BMFYXMModel model = new BMFYXMModel();
            if (fYXM.Rows.Count > 0)
            {
                DataRow row = fYXM.Rows[0];
                model.BM = GetSafeData.ValidateValue<string>(row, "BM");
                model.MC = GetSafeData.ValidateValue<string>(row, "MC");
                model.JM = GetSafeData.ValidateValue<string>(row, "JM");
                model.SJBM = GetSafeData.ValidateValue<string>(row, "SJBM");
                model.SPFL = GetSafeData.ValidateValue<string>(row, "SPFL");
                model.YHZC = GetSafeData.ValidateValue<string>(row, "YHZC");
                model.SPFLMC = GetSafeData.ValidateValue<string>(row, "SPFLMC");
                model.YHZCMC = GetSafeData.ValidateValue<string>(row, "YHZCMC");
            }
            return model;
        }

        public int GetSuggestBMLen(string SJBM)
        {
            return this.bmfyxmDAL.GetSuggestBMLen(SJBM);
        }

        public ImportResult ImportData(string codeFile)
        {
            ImportResult result2;
            try
            {
                string[] strArray = File.ReadAllLines(codeFile, ToolUtil.GetEncoding());
                if (strArray.Length == 0)
                {
                    throw new CustomException("此文件没有内容");
                }
                string str = "";
                int index = 0;
                bool flag = true;
                while (flag)
                {
                    if (strArray[index].StartsWith("{费用项目编码}"))
                    {
                        string str2 = strArray[index];
                        if (!str2.Contains("\""))
                        {
                            throw new CustomException("此文件首行费用项目编码没有指定分隔符，分隔符使用双引号标注");
                        }
                        str = str2.Substring(str2.IndexOf("\"")).Trim().Trim(new char[] { '"' });
                        flag = false;
                        index++;
                    }
                    else
                    {
                        index++;
                        if (index == strArray.Length)
                        {
                            throw new CustomException("此文件不符合费用项目编码文本格式");
                        }
                    }
                }
                ImportResult result = new ImportResult();
                Stack<lastBMJG> stack = new Stack<lastBMJG>();
                string str4 = this.TuiJianBM("");
                string str5 = "";
                bool flag2 = false;
                for (int i = index; i < strArray.Length; i++)
                {
                    BMFYXMModel model;
                    string lineText = strArray[i].Trim();
                    if (((lineText.Length == 0) || lineText.StartsWith("//")) || !lineText.Contains(str))
                    {
                        continue;
                    }
                    string[] strArray2 = GetSafeData.Split(lineText, str);
                    if (strArray2.Length < 3)
                    {
                        throw new CustomException("文本首行指定的分隔符与实际分隔符不一致，\n格式不正确等原因导致不能导入此文件！");
                    }
                    model = new BMFYXMModel {
                        BM = strArray2[0],
                        MC = strArray2[1],
                        JM = strArray2[2],
                    };
                    model.KJM = CommonFunc.GenerateKJM(model.MC);
                    string str7 = "";
                    string str8 = "";
                    ResultType none = ResultType.None;
                    while (true)
                    {
                        if (stack.Count <= 0)
                        {
                            break;
                        }
                        flag2 = false;
                        if (model.BM.Length > stack.Peek().BM.Length)
                        {
                            if (!model.BM.StartsWith(stack.Peek().BM))
                            {
                                goto Label_02B0;
                            }
                            model.SJBM = stack.Peek().BM;
                            model.WJ = 1;
                            if (stack.Peek().Result == ResultType.Invalid)
                            {
                                str7 = "无效";
                                str8 = "上级编码无效";
                                none = ResultType.Invalid;
                                result.Invalid++;
                            }
                            else if (stack.Peek().Result == ResultType.Failed)
                            {
                                str7 = "失败";
                                str8 = "上级编码失败";
                                none = ResultType.Failed;
                                result.Failed++;
                            }
                            else
                            {
                                if (stack.Peek().Result != ResultType.Duplicated)
                                {
                                    goto Label_02B0;
                                }
                                str5 = this.TuiJianBM(stack.Peek().BM);
                                if (model.BM.Length == str5.Length)
                                {
                                    goto Label_02B0;
                                }
                                str7 = "无效";
                                str8 = "编码长度必须与原有同级编码长度一致";
                                result.Invalid++;
                                none = ResultType.Invalid;
                            }
                            goto Label_03B0;
                        }
                        stack.Pop();
                    }
                    flag2 = true;
                Label_02B0:
                    if ((flag2 && (str4 != "001")) && (model.BM.Length != str4.Length))
                    {
                        str7 = "无效";
                        str8 = "编码长度必须与原有同级编码长度一致";
                        result.Invalid++;
                        none = ResultType.Invalid;
                    }
                    else if (this.bmfyxmDAL.ExistExpense(model))
                    {
                        if (str7.Length == 0)
                        {
                            str7 = "重复";
                            result.Duplicated++;
                        }
                        if (str8.Length == 0)
                        {
                            str8 = "编码重复";
                            none = ResultType.Duplicated;
                        }
                        else
                        {
                            str8 = str8 + "且编码重复";
                            none = ResultType.Invalid;
                        }
                    }
                    else
                    {
                        StringUtils.GetSpellCode(model.MC);
                        if (this.bmfyxmDAL.AddExpense(model) == "0")
                        {
                            str7 = "正确传入";
                            result.Correct++;
                            none = ResultType.Correct;
                        }
                        else
                        {
                            str7 = "失败";
                            result.Failed++;
                            none = ResultType.Failed;
                        }
                    }
                Label_03B0:;
                    result.DtResult.Rows.Add(new object[] { model.BM, model.MC, str7, str8 });
                    stack.Push(new lastBMJG(model.BM, none));
                }
                result.ImportTable = "费用项目编码.DB";
                result2 = result;
            }
            catch
            {
                throw;
            }
            return result2;
        }

        public bool JianWeiVerify(string BM)
        {
            return this.bmfyxmDAL.JianWeiVerify(BM);
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            return this.bmfyxmDAL.listNodes(searchid);
        }

        public string ModifyExpense(BMFYXMModel feiyong, string OldBM)
        {
            string str = this.CheckExpense(feiyong);
            if (str != "0")
            {
                return str;
            }
            if (TaxCardFactory.CreateTaxCard().QYLX.ISHY && this.bmfyxmDAL.ExistCusMC(OldBM, feiyong.MC, feiyong.SJBM))
            {
                MessageManager.ShowMsgBox("INP-235136");
                return "费用项目已存在";
            }
            return this.bmfyxmDAL.ModifyExpense(feiyong, OldBM);
        }

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            return this.bmfyxmDAL.QueryByKey(KeyWord, pagesize, pageno);
        }

        public AisinoDataSet QueryData(int pagesize, int pageno)
        {
            return this.bmfyxmDAL.QueryExpense(pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno)
        {
            return this.bmfyxmDAL.SelectNodeDisplay(selectBM, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            return this.bmfyxmDAL.TuiJianBM(searchid);
        }

        public string UpdateSubNodesSJBM(BMFYXMModel feiyong, string YuanBM)
        {
            string str = this.CheckExpense(feiyong);
            if (str != "0")
            {
                return str;
            }
            return this.bmfyxmDAL.UpdateSubNodesSJBM(feiyong, YuanBM);
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
                    num = 30;
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

