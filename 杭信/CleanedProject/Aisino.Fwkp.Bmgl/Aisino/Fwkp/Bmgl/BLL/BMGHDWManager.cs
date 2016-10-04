namespace Aisino.Fwkp.Bmgl.BLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl;
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
    using System.Text.RegularExpressions;

    internal sealed class BMGHDWManager : IBMBaseManager
    {
        private int _currentPage = 1;
        private DAL.BMGHDWManager bmghdwDAL = new DAL.BMGHDWManager();
        private InvoiceHandler jyPurchase = new InvoiceHandler();

        public bool AddCustomerToAuto(BMGHDWModel customer, string SJBM)
        {
            try
            {
                if (this.bmghdwDAL.QueryByMCAndSH(customer.MC, customer.SH).Rows.Count > 0)
                {
                    return false;
                }
                if (customer.SH.Length != 0)
                {
                    DataTable table2 = this.bmghdwDAL.QueryBySHAndSJBM(customer.SH, SJBM);
                    if ((table2.Rows.Count > 0) && (customer.MC.Trim() == table2.Rows[0]["MC"].ToString().Trim()))
                    {
                        customer.BM = table2.Rows[0]["BM"].ToString();
                        this.bmghdwDAL.ModifyPurchase(customer, table2.Rows[0]["BM"].ToString());
                        return true;
                    }
                }
                else
                {
                    DataTable table3 = this.bmghdwDAL.QueryByMCAndSJBMAndSHEmpty(customer.MC, SJBM);
                    if (table3.Rows.Count > 0)
                    {
                        customer.BM = table3.Rows[0]["BM"].ToString();
                        this.bmghdwDAL.ModifyPurchase(customer, table3.Rows[0]["BM"].ToString());
                        return true;
                    }
                }
                this.bmghdwDAL.AddPurchase(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string AddPurchase(BMGHDWModel purchase)
        {
            return this.AddPurchaseKP(purchase, 0);
        }

        public string AddPurchaseKP(BMGHDWModel purchase, int Addtype)
        {
            string str = this.CheckPurchase(purchase);
            if (str == "0")
            {
                if ((purchase.WJ == 1) && (purchase.SH.Length != 0))
                {
                    string str2 = this.jyPurchase.CheckTaxCode(purchase.SH, (FPLX)12);
                    if (str2 != "0000")
                    {
                        MessageManager.ShowMsgBox(str2, new string[] { "税号" });
                        return str2;
                    }
                }
                if (this.bmghdwDAL.ExistPurMC(purchase.BM, purchase.MC, purchase.SJBM))
                {
                    MessageManager.ShowMsgBox("INP-235131");
                    return "同名购货单位已存在";
                }
                if (this.bmghdwDAL.ExistPurchase(purchase))
                {
                    MessageManager.ShowMsgBox("INP-235108");
                    return "编码已存在";
                }
                str = this.bmghdwDAL.AddPurchase(purchase);
                if ((Addtype == 1) && (str == "0"))
                {
                    MessageManager.ShowMsgBox("INP-235201");
                }
            }
            return str;
        }

        public DataTable AppendByKey(string KeyWord, int TopNo)
        {
            return this.bmghdwDAL.AppendByKey(KeyWord, TopNo);
        }

        public DataTable AppendByKeyWJ(string KeyWord, int TopNo)
        {
            return this.bmghdwDAL.AppendByKeyWJ(KeyWord, TopNo);
        }

        public string AutoNodeLogic()
        {
            return this.bmghdwDAL.AutoNodeLogic();
        }

        public string CheckPurchase(BMGHDWModel purchase)
        {
            if (purchase == null)
            {
                return "e11";
            }
            if ((purchase.BM == null) || (purchase.BM.Length == 0))
            {
                return "e12";
            }
            if ((purchase.MC != null) && (purchase.MC.Length != 0))
            {
                return "0";
            }
            return "e13";
        }

        public string ChildDetermine(string BM, string SJBM)
        {
            return this.bmghdwDAL.ChildDetermine(BM, SJBM);
        }

        public string DeleteData(string purchaseCode)
        {
            return this.bmghdwDAL.DeletePurchase(purchaseCode);
        }

        public string DeleteDataFenLei(string FenLeiCodeBM)
        {
            if (!this.bmghdwDAL.FenLeiHasChild(FenLeiCodeBM))
            {
                return this.bmghdwDAL.deleteFenLei(FenLeiCodeBM);
            }
            return "e";
        }

        public string deleteFenLei(string flBM)
        {
            return this.bmghdwDAL.deleteFenLei(flBM);
        }

        public string deleteNodes(string searchid)
        {
            return this.bmghdwDAL.deleteNodes(searchid);
        }

        public string ExecZengJianWei(string BM, bool isZengWei)
        {
            if (isZengWei)
            {
                if (this.bmghdwDAL.ZengWeiVerify(BM))
                {
                    return this.bmghdwDAL.ExecZengJianWei(BM, isZengWei);
                }
                return "ezw";
            }
            if (this.bmghdwDAL.JianWeiVerify(BM))
            {
                return this.bmghdwDAL.ExecZengJianWei(BM, isZengWei);
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
            DataTable exportData = this.bmghdwDAL.GetExportData();
            if (exportData.Rows.Count == 0)
            {
                return "没有购货单位";
            }
            StringBuilder builder = new StringBuilder();
            using (StreamWriter writer = new StreamWriter(pathFile, false, ToolUtil.GetEncoding()))
            {
                writer.WriteLine("{购货单位编码}[分隔符]\"" + separator + "\"");
                writer.WriteLine("// 每行格式 :");
                string str2 = "// 编码,名称,简码,税号,地址电话,银行账号,身份证号码/组织机构代码";
                writer.WriteLine(str2.Replace(",", separator));
                for (int i = 0; i < exportData.Rows.Count; i++)
                {
                    builder.Remove(0, builder.Length);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["BM"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["MC"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["JM"].ToString(), separator));
                    if (exportData.Rows[i]["WJ"].ToString() == "1")
                    {
                        builder.Append(separator);
                        builder.Append(exportData.Rows[i]["SH"]);
                        builder.Append(separator);
                        string item = exportData.Rows[i]["DZDH"].ToString().Replace("\r\n", "#|#");
                        builder.Append(GetSafeData.ExportItem(item, separator));
                        builder.Append(separator);
                        string str4 = exportData.Rows[i]["YHZH"].ToString().Replace("\r\n", "#|#");
                        builder.Append(GetSafeData.ExportItem(str4, separator));
                        builder.Append(separator);
                        string str5 = exportData.Rows[i]["IDCOC"].ToString().Replace("\r\n", "#|#");
                        builder.Append(GetSafeData.ExportItem(str5, separator));
                    }
                    builder.Replace(",,", ",\"\",");
                    writer.WriteLine(builder.ToString());
                }
            }
            return "0";
        }

        public DataTable GetGHDW(string BM)
        {
            return this.bmghdwDAL.GetGHDW(BM);
        }

        public BMBaseModel GetModel(string BM)
        {
            DataTable gHDW = this.bmghdwDAL.GetGHDW(BM);
            BMGHDWModel model = new BMGHDWModel();
            if (gHDW.Rows.Count > 0)
            {
                DataRow row = gHDW.Rows[0];
                model.BM = GetSafeData.ValidateValue<string>(row, "BM");
                model.MC = GetSafeData.ValidateValue<string>(row, "MC");
                model.JM = GetSafeData.ValidateValue<string>(row, "JM");
                model.SH = GetSafeData.ValidateValue<string>(row, "SH");
                model.DZDH = GetSafeData.ValidateValue<string>(row, "DZDH");
                model.YHZH = GetSafeData.ValidateValue<string>(row, "YHZH");
                model.IDCOC = GetSafeData.ValidateValue<string>(row, "IDCOC");
                model.SJBM = GetSafeData.ValidateValue<string>(row, "SJBM");
            }
            return model;
        }

        public int GetSuggestBMLen(string SJBM)
        {
            return this.bmghdwDAL.GetSuggestBMLen(SJBM);
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
                    if (strArray[index].StartsWith("{购货单位编码}"))
                    {
                        string str2 = strArray[index];
                        if (!str2.Contains("\""))
                        {
                            throw new CustomException("此文件首行购货单位编码没有指定分隔符，分隔符使用双引号标注");
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
                            throw new CustomException("此文件不符合购货单位编码文本格式");
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
                    BMGHDWModel model;
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
                    model = new BMGHDWModel {
                        BM = strArray2[0],
                        MC = strArray2[1],
                        JM = strArray2[2],
                    };
                    model.KJM = CommonFunc.GenerateKJM(model.MC);
                    string str7 = "";
                    string str8 = "";
                    ResultType none = ResultType.None;
                    if (strArray2.Length <= 3)
                    {
                        goto Label_0307;
                    }
                    if (strArray2.Length < 7)
                    {
                        continue;
                    }
                    model.SH = strArray2[3].ToUpper();
                    model.WJ = 1;
                    string str9 = "";
                    if (str == "~~")
                    {
                        str9 = strArray2[4].Replace(",", "\r\n").Replace("#|#", "\r\n");
                    }
                    else if (str == " ")
                    {
                        str9 = strArray2[4].Replace("#|#", "\r\n");
                    }
                    else
                    {
                        str9 = strArray2[4];
                    }
                    model.DZDH = str9;
                    string str10 = "";
                    if (str == "~~")
                    {
                        str10 = strArray2[5].Replace(",", "\r\n").Replace("#|#", "\r\n");
                    }
                    else if (str == " ")
                    {
                        str10 = strArray2[5].Replace("#|#", "\r\n");
                    }
                    else
                    {
                        str10 = strArray2[5];
                    }
                    model.YHZH = str10;
                    if (strArray2.Length <= 6)
                    {
                        goto Label_0307;
                    }
                    string str11 = string.Empty;
                    string str13 = str;
                    if (str13 == null)
                    {
                        goto Label_02F8;
                    }
                    if (!(str13 == "~~"))
                    {
                        if (str13 == " ")
                        {
                            goto Label_02D2;
                        }
                        goto Label_02F8;
                    }
                    str11 = strArray2[6].Replace(",", "\r\n").Replace("#|#", "\r\n");
                    goto Label_02FE;
                Label_02D2:
                    str11 = strArray2[6].Replace(" ", "\r\n").Replace("#|#", "\r\n");
                    goto Label_02FE;
                Label_02F8:
                    str11 = strArray2[6];
                Label_02FE:
                    model.IDCOC = str11;
                Label_0307:
                    while (stack.Count > 0)
                    {
                        flag2 = false;
                        if (model.BM.Length > stack.Peek().BM.Length)
                        {
                            if (!model.BM.StartsWith(stack.Peek().BM))
                            {
                                goto Label_043E;
                            }
                            model.SJBM = stack.Peek().BM;
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
                                    goto Label_043E;
                                }
                                str5 = this.TuiJianBM(stack.Peek().BM);
                                if (model.BM.Length == str5.Length)
                                {
                                    goto Label_043E;
                                }
                                str7 = "无效";
                                str8 = "编码长度必须与原有同级编码长度一致";
                                result.Invalid++;
                                none = ResultType.Invalid;
                            }
                            goto Label_06F3;
                        }
                        stack.Pop();
                    }
                    flag2 = true;
                Label_043E:
                    if ((flag2 && (str4 != "001")) && (model.BM.Length != str4.Length))
                    {
                        str7 = "无效";
                        str8 = "编码长度必须与原有同级编码长度一致";
                        result.Invalid++;
                        none = ResultType.Invalid;
                    }
                    else if ("0" != this.CheckPurchase(model))
                    {
                        str7 = "无效";
                        str8 = "编码或名称为空";
                        result.Invalid++;
                        none = ResultType.Invalid;
                    }
                    else if (!Regex.IsMatch(model.BM, "^[0-9a-z]{1,16}$"))
                    {
                        str7 = "无效";
                        str8 = "编码需小于16位，且仅由数字和小写字母组成";
                        result.Invalid++;
                        none = ResultType.Invalid;
                    }
                    else if (this.bmghdwDAL.ExistPurchase(model))
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
                    else if (((model.SH != null) && (model.SH.Length != 0)) && (((model.SH.Length < 6) || (model.SH.Length > 20)) || (((model.WJ == 1) && (model.SH.Length != 0)) && ("0000" != this.jyPurchase.CheckTaxCode(model.SH, (FPLX)12)))))
                    {
                        str7 = "失败";
                        str8 = "购货单位税号错误" + model.SH;
                        result.Failed++;
                        none = ResultType.Failed;
                    }
                    else if ((1 == model.WJ) && string.IsNullOrEmpty(model.IDCOC))
                    {
                        str7 = "失败";
                        str8 = "身份证号码/组织机构代码不允许为空" + model.IDCOC;
                        result.Failed++;
                        none = ResultType.Failed;
                    }
                    else if (this.bmghdwDAL.ExistPurMC(model.BM, model.MC, model.SJBM))
                    {
                        str7 = "失败";
                        str8 = "同级同族已存在同名购货单位" + model.SH;
                        result.Failed++;
                        none = ResultType.Failed;
                    }
                    else
                    {
                        StringUtils.GetSpellCode(model.MC);
                        if (this.bmghdwDAL.AddPurchase(model) == "0")
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
                Label_06F3:;
                    result.DtResult.Rows.Add(new object[] { model.BM, model.MC, str7, str8 });
                    stack.Push(new lastBMJG(model.BM, none));
                }
                result.ImportTable = "购货单位编码.DB";
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
            return this.bmghdwDAL.JianWeiVerify(BM);
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            return this.bmghdwDAL.listNodes(searchid);
        }

        public string ModifyPurchase(BMGHDWModel purchase, string OldBM)
        {
            string str = this.CheckPurchase(purchase);
            if (str != "0")
            {
                return str;
            }
            if ((purchase.WJ == 1) && (purchase.SH.Length != 0))
            {
                string str2 = this.jyPurchase.CheckTaxCode(purchase.SH, (FPLX)12);
                if (str2 != "0000")
                {
                    MessageManager.ShowMsgBox(str2, new string[] { "税号" });
                    return str2;
                }
            }
            if ((purchase.BM != OldBM) && this.bmghdwDAL.ExistPurchase(purchase))
            {
                MessageManager.ShowMsgBox("INP-235108");
                return "编码已存在";
            }
            if (this.bmghdwDAL.ExistPurMC(OldBM, purchase.MC, purchase.SJBM))
            {
                MessageManager.ShowMsgBox("INP-235131");
                return "同名购货单位已存在";
            }
            return this.bmghdwDAL.ModifyPurchase(purchase, OldBM);
        }

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            return this.bmghdwDAL.QueryByKey(KeyWord, pagesize, pageno);
        }

        public DataTable QueryByMCAndSJBM(string MC, string SJBM)
        {
            return this.bmghdwDAL.QueryByMCAndSJBM(MC, SJBM);
        }

        public DataTable QueryByTaxCode(string TaxCode)
        {
            return this.bmghdwDAL.QueryByTaxCode(TaxCode);
        }

        public AisinoDataSet QueryData(int pagesize, int pageno)
        {
            return this.bmghdwDAL.QueryPurchase(pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno)
        {
            return this.bmghdwDAL.SelectNodeDisplay(selectBM, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            return this.bmghdwDAL.TuiJianBM(searchid);
        }

        public string UpdateSubNodesSJBM(BMGHDWModel purchase, string YuanBM)
        {
            string str = this.CheckPurchase(purchase);
            if (str != "0")
            {
                return str;
            }
            return this.bmghdwDAL.UpdateSubNodesSJBM(purchase, YuanBM);
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

