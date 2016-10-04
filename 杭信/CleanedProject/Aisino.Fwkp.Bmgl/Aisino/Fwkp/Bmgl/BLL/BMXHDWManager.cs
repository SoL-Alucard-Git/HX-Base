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

    internal sealed class BMXHDWManager : IBMBaseManager
    {
        private int _currentPage = 1;
        private DAL.BMXHDWManager bmsfhrDAL = new DAL.BMXHDWManager();
        private InvoiceHandler jyCustomer = new InvoiceHandler();

        public string AddCustomer(BMXHDWModel customer)
        {
            return this.AddCustomerKP(customer, 0);
        }

        public string AddCustomerKP(BMXHDWModel customer, int Addtype)
        {
            string str = this.CheckCustomer(customer);
            if (str == "0")
            {
                if (customer.WJ == 1)
                {
                    string str2 = this.jyCustomer.CheckTaxCode(customer.SH, (FPLX)11);
                    if (str2 != "0000")
                    {
                        MessageManager.ShowMsgBox(str2, new string[] { "税号" });
                        return str2;
                    }
                }
                if (this.bmsfhrDAL.ExistCusMC(customer.BM, customer.MC, customer.SJBM))
                {
                    MessageManager.ShowMsgBox("INP-235135");
                    return "销货单位已存在";
                }
                if (this.bmsfhrDAL.ExistCustomer(customer))
                {
                    MessageManager.ShowMsgBox("INP-235108");
                    return "编码已存在";
                }
                str = this.bmsfhrDAL.AddCustomer(customer);
                if ((Addtype == 1) && (str == "0"))
                {
                    MessageManager.ShowMsgBox("INP-235201");
                }
            }
            return str;
        }

        public DataTable AppendByKey(string KeyWord, int TopNo)
        {
            return this.bmsfhrDAL.AppendByKey(KeyWord, TopNo);
        }

        public DataTable AppendByKeyWJ(string KeyWord, int TopNo)
        {
            return this.bmsfhrDAL.AppendByKeyWJ(KeyWord, TopNo);
        }

        public string CheckCustomer(BMXHDWModel customer)
        {
            if (customer == null)
            {
                return "e11";
            }
            if ((customer.BM == null) || (customer.BM.Length == 0))
            {
                return "e12";
            }
            if ((customer.MC != null) && (customer.MC.Length != 0))
            {
                return "0";
            }
            return "e13";
        }

        public string ChildDetermine(string BM, string SJBM)
        {
            return this.bmsfhrDAL.ChildDetermine(BM, SJBM);
        }

        public string DeleteData(string customerCode)
        {
            return this.bmsfhrDAL.DeleteCustomer(customerCode);
        }

        public string DeleteDataFenLei(string FenLeiCodeBM)
        {
            if (!this.bmsfhrDAL.FenLeiHasChild(FenLeiCodeBM))
            {
                return this.bmsfhrDAL.deleteFenLei(FenLeiCodeBM);
            }
            return "e";
        }

        public string deleteFenLei(string flBM)
        {
            return this.bmsfhrDAL.deleteFenLei(flBM);
        }

        public string deleteNodes(string searchid)
        {
            return this.bmsfhrDAL.deleteNodes(searchid);
        }

        public string ExecZengJianWei(string BM, bool isZengWei)
        {
            if (isZengWei)
            {
                if (this.bmsfhrDAL.ZengWeiVerify(BM))
                {
                    return this.bmsfhrDAL.ExecZengJianWei(BM, isZengWei);
                }
                return "ezw";
            }
            if (this.bmsfhrDAL.JianWeiVerify(BM))
            {
                return this.bmsfhrDAL.ExecZengJianWei(BM, isZengWei);
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
            DataTable exportData = this.bmsfhrDAL.GetExportData();
            if (exportData.Rows.Count == 0)
            {
                return "没有销货单位";
            }
            StringBuilder builder = new StringBuilder();
            using (StreamWriter writer = new StreamWriter(pathFile, false, ToolUtil.GetEncoding()))
            {
                writer.WriteLine("{销货单位编码}[分隔符]\"" + separator + "\"");
                writer.WriteLine("// 每行格式 :");
                string str2 = "// 编码,名称,简码,税号,地址电话,银行账号,邮政编码";
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
                        builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["YZBM"].ToString(), separator));
                    }
                    builder.Replace(",,", ",\"\",");
                    writer.WriteLine(builder.ToString());
                }
            }
            return "0";
        }

        public DataTable GetKH(string BM)
        {
            return this.bmsfhrDAL.GetKH(BM);
        }

        public BMBaseModel GetModel(string BM)
        {
            DataTable kH = this.bmsfhrDAL.GetKH(BM);
            BMXHDWModel model = new BMXHDWModel();
            if (kH.Rows.Count > 0)
            {
                DataRow row = kH.Rows[0];
                model.BM = GetSafeData.ValidateValue<string>(row, "BM");
                model.MC = GetSafeData.ValidateValue<string>(row, "MC");
                model.JM = GetSafeData.ValidateValue<string>(row, "JM");
                model.SJBM = GetSafeData.ValidateValue<string>(row, "SJBM");
                model.SH = GetSafeData.ValidateValue<string>(row, "SH");
                model.DZDH = GetSafeData.ValidateValue<string>(row, "DZDH");
                model.YHZH = GetSafeData.ValidateValue<string>(row, "YHZH");
                model.YZBM = GetSafeData.ValidateValue<string>(row, "YZBM");
                model.WJ = GetSafeData.ValidateValue<int>(row, "WJ");
            }
            return model;
        }

        public int GetSuggestBMLen(string SJBM)
        {
            return this.bmsfhrDAL.GetSuggestBMLen(SJBM);
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
                    if (strArray[index].StartsWith("{销货单位编码}") || strArray[index].StartsWith("{发货人编码}"))
                    {
                        string str2 = strArray[index];
                        if (!str2.Contains("\""))
                        {
                            throw new CustomException("此文件首行销货单位编码没有指定分隔符，分隔符使用双引号标注");
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
                            throw new CustomException("此文件不符合销货单位编码文本格式");
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
                    BMXHDWModel model;
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
                    model = new BMXHDWModel {
                        BM = strArray2[0],
                        MC = strArray2[1],
                        JM = strArray2[2],
                    };
                    model.KJM = CommonFunc.GenerateKJM(model.MC);
                    string str7 = "";
                    string str8 = "";
                    ResultType none = ResultType.None;
                    if (strArray2.Length > 3)
                    {
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
                        string str11 = "";
                        if (str == "~~")
                        {
                            str11 = strArray2[6].Replace(",", "\r\n").Replace("#|#", "\r\n");
                        }
                        else if (str == " ")
                        {
                            str11 = strArray2[6].Replace("#|#", "\r\n");
                        }
                        else
                        {
                            str11 = strArray2[6];
                        }
                        model.YZBM = str11;
                    }
                    while (stack.Count > 0)
                    {
                        flag2 = false;
                        if (model.BM.Length > stack.Peek().BM.Length)
                        {
                            if (!model.BM.StartsWith(stack.Peek().BM))
                            {
                                goto Label_0430;
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
                                    goto Label_0430;
                                }
                                str5 = this.TuiJianBM(stack.Peek().BM);
                                if (model.BM.Length == str5.Length)
                                {
                                    goto Label_0430;
                                }
                                str7 = "无效";
                                str8 = "编码长度必须与原有同级编码长度一致";
                                result.Invalid++;
                                none = ResultType.Invalid;
                            }
                            goto Label_06AF;
                        }
                        stack.Pop();
                    }
                    flag2 = true;
                Label_0430:
                    if ((flag2 && (str4 != "001")) && (model.BM.Length != str4.Length))
                    {
                        str7 = "无效";
                        str8 = "编码长度必须与原有同级编码长度一致";
                        result.Invalid++;
                        none = ResultType.Invalid;
                    }
                    else if ("0" != this.CheckCustomer(model))
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
                    if (this.bmsfhrDAL.ExistCusMC(model.BM, model.MC, model.SJBM))
                    {
                        str7 = "失败";
                        str8 = "同级同族已存在同名销货单位" + model.SH;
                        result.Failed++;
                        none = ResultType.Failed;
                    }
                    else if (this.bmsfhrDAL.ExistCustomer(model))
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
                    else if (((model.SH != null) && (model.SH.Length != 0)) && (((model.SH.Length < 6) || (model.SH.Length > 20)) || (((model.WJ == 1) && (model.SH.Length != 0)) && ("0000" != this.jyCustomer.CheckTaxCode(model.SH, (FPLX)12)))))
                    {
                        str7 = "失败";
                        str8 = "销货单位税号错误" + model.SH;
                        result.Failed++;
                        none = ResultType.Failed;
                    }
                    else
                    {
                        string[] spellCode = StringUtils.GetSpellCode(model.MC);
                        for (int j = 0; j < spellCode.Length; j++)
                        {
                            string text1 = spellCode[j];
                        }
                        if (this.bmsfhrDAL.AddCustomer(model) == "0")
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
                Label_06AF:;
                    result.DtResult.Rows.Add(new object[] { model.BM, model.MC, str7, str8 });
                    stack.Push(new lastBMJG(model.BM, none));
                }
                result.ImportTable = "销货单位编码.DB";
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
            return this.bmsfhrDAL.JianWeiVerify(BM);
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            return this.bmsfhrDAL.listNodes(searchid);
        }

        public string ModifyCustomer(BMXHDWModel customer, string OldBM)
        {
            string str = this.CheckCustomer(customer);
            if (str != "0")
            {
                return str;
            }
            if (customer.WJ == 1)
            {
                string str2 = this.jyCustomer.CheckTaxCode(customer.SH, (FPLX)11);
                if (str2 != "0000")
                {
                    MessageManager.ShowMsgBox(str2, new string[] { "税号" });
                    return str2;
                }
            }
            if ((customer.BM != OldBM) && this.bmsfhrDAL.ExistCustomer(customer))
            {
                MessageManager.ShowMsgBox("INP-235108");
                return "编码已存在";
            }
            if (this.bmsfhrDAL.ExistCusMC(OldBM, customer.MC, customer.SJBM))
            {
                MessageManager.ShowMsgBox("INP-235135");
                return "销货单位已存在";
            }
            return this.bmsfhrDAL.ModifyCustomer(customer, OldBM);
        }

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            return this.bmsfhrDAL.QueryByKey(KeyWord, pagesize, pageno);
        }

        public DataTable QueryByMCAndSJBM(string MC, string SJBM)
        {
            return this.bmsfhrDAL.QueryByMCAndSJBM(MC, SJBM);
        }

        public DataTable QueryByTaxCode(string TaxCode)
        {
            return this.bmsfhrDAL.QueryByTaxCode(TaxCode);
        }

        public AisinoDataSet QueryData(int pagesize, int pageno)
        {
            return this.bmsfhrDAL.QueryCustomer(pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno)
        {
            return this.bmsfhrDAL.SelectNodeDisplay(selectBM, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            return this.bmsfhrDAL.TuiJianBM(searchid);
        }

        public string UpdateSubNodesSJBM(BMXHDWModel customer, string YuanBM)
        {
            string str = this.CheckCustomer(customer);
            if (str != "0")
            {
                return str;
            }
            return this.bmsfhrDAL.UpdateSubNodesSJBM(customer, YuanBM);
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

