namespace Aisino.Fwkp.Bmgl.BLL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
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
    using System.Xml;

    internal sealed class BMKHManager : IBMBaseManager
    {
        private int _currentPage = 1;
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private DAL.BMKHManager bmkhDAL = new DAL.BMKHManager();
        private InvoiceHandler jyCustomer = new InvoiceHandler();

        public string AddCustomer(BMKHModel customer)
        {
            return this.AddCustomerKP(customer, 0);
        }

        public string AddCustomerKP(BMKHModel customer, int Addtype)
        {
            string str = this.CheckCustomer(customer);
            if (str == "0")
            {
                if ((customer.WJ == 1) && (customer.SH.Length != 0))
                {
                    string str2 = this.jyCustomer.CheckTaxCode(customer.SH, 0);
                    if (str2 != "0000")
                    {
                        MessageManager.ShowMsgBox(str2, new string[] { "税号" });
                        return str2;
                    }
                }
                if (this.bmkhDAL.ExistCusMC(customer.BM, customer.MC, customer.SJBM))
                {
                    MessageManager.ShowMsgBox("INP-235130");
                    return "客户已存在";
                }
                if (this.bmkhDAL.ExistCustomer(customer))
                {
                    MessageManager.ShowMsgBox("INP-235108");
                    return "编码已存在";
                }
                str = this.bmkhDAL.AddCustomer(customer);
                if ((Addtype == 1) && (str == "0"))
                {
                    MessageManager.ShowMsgBox("INP-235201");
                }
            }
            return str;
        }

        public bool AddCustomerToAuto(BMKHModel customer, string SJBM)
        {
            try
            {
                if (this.bmkhDAL.QueryByMCSH(customer.MC, customer.SH).Rows.Count > 0)
                {
                    return false;
                }
                if (customer.SH.Length != 0)
                {
                    DataTable table2 = this.bmkhDAL.QueryBySHAndSJBM(customer.SH, SJBM);
                    if ((table2.Rows.Count > 0) && (customer.MC.Trim() == table2.Rows[0]["MC"].ToString().Trim()))
                    {
                        customer.BM = table2.Rows[0]["BM"].ToString();
                        this.bmkhDAL.ModifyCustomer(customer, table2.Rows[0]["BM"].ToString());
                        return true;
                    }
                }
                else
                {
                    DataTable table3 = this.bmkhDAL.QueryByMCAndSJBMAndSHEmpty(customer.MC, SJBM);
                    if (table3.Rows.Count > 0)
                    {
                        customer.BM = table3.Rows[0]["BM"].ToString();
                        this.bmkhDAL.ModifyCustomer(customer, table3.Rows[0]["BM"].ToString());
                        return true;
                    }
                }
                this.bmkhDAL.AddCustomer(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable AppendByKey(string KeyWord, int TopNo)
        {
            return this.bmkhDAL.AppendByKey(KeyWord, TopNo);
        }

        public DataTable AppendByKeyWJ(string KeyWord, int TopNo)
        {
            return this.bmkhDAL.AppendByKeyWJ(KeyWord, TopNo);
        }

        public AisinoDataSet AppendKhByMc(string keyWordMc)
        {
            return this.bmkhDAL.AppendKhByMc(keyWordMc);
        }

        public AisinoDataSet AppendKhByMcAndSh(string keyWordMc, string keyWordSh)
        {
            return this.bmkhDAL.AppendKhByMcAndSh(keyWordMc, keyWordSh);
        }

        public AisinoDataSet AppendKhBySh(string keyWordSh)
        {
            return this.bmkhDAL.AppendKhBySh(keyWordSh);
        }

        public string AutoNodeLogic()
        {
            return this.bmkhDAL.AutoNodeLogic();
        }

        public string CheckCustomer(BMKHModel customer)
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
            return this.bmkhDAL.ChildDetermine(BM, SJBM);
        }

        public string DeleteData(string customerCode)
        {
            return this.bmkhDAL.DeleteCustomer(customerCode);
        }

        public string DeleteDataFenLei(string FenLeiCodeBM)
        {
            if (!this.bmkhDAL.FenLeiHasChild(FenLeiCodeBM))
            {
                return this.bmkhDAL.deleteFenLei(FenLeiCodeBM);
            }
            return "e";
        }

        public string deleteFenLei(string flBM)
        {
            return this.bmkhDAL.deleteFenLei(flBM);
        }

        public string deleteNodes(string searchid)
        {
            return this.bmkhDAL.deleteNodes(searchid);
        }

        public string ExecZengJianWei(string BM, bool isZengWei)
        {
            if (isZengWei)
            {
                if (this.bmkhDAL.ZengWeiVerify(BM))
                {
                    return this.bmkhDAL.ExecZengJianWei(BM, isZengWei);
                }
                return "ezw";
            }
            if (this.bmkhDAL.JianWeiVerify(BM))
            {
                return this.bmkhDAL.ExecZengJianWei(BM, isZengWei);
            }
            return "ejw";
        }

        public string ExportData(string pathFile, string separator, DataTable khTable = null)
        {
            DataTable exportData;
            string path = pathFile.Remove(pathFile.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (khTable == null)
            {
                exportData = this.bmkhDAL.GetExportData();
            }
            else
            {
                exportData = khTable;
            }
            if (exportData.Rows.Count == 0)
            {
                return "没有客户";
            }
            StringBuilder builder = new StringBuilder();
            using (StreamWriter writer = new StreamWriter(pathFile, false, ToolUtil.GetEncoding()))
            {
                writer.WriteLine("{客户编码}[分隔符]\"" + separator + "\"");
                writer.WriteLine("// 每行格式 :");
                string str2 = "// 编码,名称,简码,税号,地址电话,银行账号,邮件地址,备注,身份证校验";
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
                        builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["YJDZ"].ToString(), separator));
                        builder.Append(separator);
                        builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["BZ"].ToString(), separator));
                        builder.Append(separator);
                        builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["SFZJY"].ToString(), separator));
                    }
                    builder.Replace(",,", ",\"\",");
                    writer.WriteLine(builder.ToString());
                }
            }
            return "0";
        }

        public DataTable GetKH(string BM)
        {
            return this.bmkhDAL.GetKH(BM);
        }

        public BMBaseModel GetModel(string BM)
        {
            DataTable kH = this.bmkhDAL.GetKH(BM);
            BMKHModel model = new BMKHModel();
            if (kH.Rows.Count > 0)
            {
                DataRow row = kH.Rows[0];
                model.BM = GetSafeData.ValidateValue<string>(row, "BM");
                model.MC = GetSafeData.ValidateValue<string>(row, "MC");
                model.JM = GetSafeData.ValidateValue<string>(row, "JM");
                model.KJM = GetSafeData.ValidateValue<string>(row, "KJM");
                model.SH = GetSafeData.ValidateValue<string>(row, "SH");
                model.DZDH = GetSafeData.ValidateValue<string>(row, "DZDH");
                model.YHZH = GetSafeData.ValidateValue<string>(row, "YHZH");
                model.YJDZ = GetSafeData.ValidateValue<string>(row, "YJDZ");
                model.BZ = GetSafeData.ValidateValue<string>(row, "BZ");
                model.YSKM = GetSafeData.ValidateValue<string>(row, "YSKM");
                model.DQBM = GetSafeData.ValidateValue<string>(row, "DQBM");
                model.DQMC = GetSafeData.ValidateValue<string>(row, "DQMC");
                model.DQKM = GetSafeData.ValidateValue<string>(row, "DQKM");
                model.SJBM = GetSafeData.ValidateValue<string>(row, "SJBM");
                model.SFZJY = GetSafeData.ValidateValue<bool>(row, "SFZJY");
            }
            return model;
        }

        public int GetSuggestBMLen(string SJBM)
        {
            return this.bmkhDAL.GetSuggestBMLen(SJBM);
        }

        public ImportResult ImportData(string codeFile)
        {
            ImportResult result2;
            try
            {
                string[] array = File.ReadAllLines(codeFile, ToolUtil.GetEncoding());
                if (array.Length == 0)
                {
                    throw new CustomException("此文件没有内容");
                }
                string str = "";
                int index = 0;
                bool flag = true;
                while (flag)
                {
                    if (array[index].StartsWith("{客户编码}"))
                    {
                        string str2 = array[index];
                        if (!str2.Contains("\""))
                        {
                            throw new CustomException("此文件首行客户编码没有指定分隔符，分隔符使用双引号标注");
                        }
                        str = str2.Substring(str2.IndexOf("\"")).Trim().Trim(new char[] { '"' });
                        flag = false;
                        index++;
                    }
                    else
                    {
                        index++;
                        if (index == array.Length)
                        {
                            throw new CustomException("此文件不符合客户编码文本格式");
                        }
                    }
                }
                if (TaxCardFactory.CreateTaxCard().SoftVersion == "FWKP_V2.0_Svr_Client")
                {
                    Array.Sort<string>(array, index, array.Length - index);
                }
                ImportResult result = new ImportResult();
                Stack<lastBMJG> stack = new Stack<lastBMJG>();
                string str4 = this.TuiJianBM("");
                string str5 = "";
                bool flag2 = false;
                for (int i = index; i < array.Length; i++)
                {
                    string lineText = array[i].Trim();
                    if (((lineText.Length == 0) || lineText.StartsWith("//")) || !lineText.Contains(str))
                    {
                        continue;
                    }
                    string[] strArray2 = GetSafeData.Split(lineText, str);
                    if (strArray2.Length < 3)
                    {
                        throw new CustomException("文本首行指定的分隔符与实际分隔符不一致，\n格式不正确等原因导致不能导入此文件！");
                    }
                    BMKHModel customer = new BMKHModel {
                        BM = strArray2[0],
                        MC = strArray2[1],
                        JM = strArray2[2]
                    };
                    string str7 = "";
                    string str8 = "";
                    ResultType none = ResultType.None;
                    customer.KJM = CommonFunc.GenerateKJM(customer.MC);
                    if (strArray2.Length > 3)
                    {
                        if (strArray2.Length < 7)
                        {
                            continue;
                        }
                        customer.SH = strArray2[3].ToUpper();
                        customer.WJ = 1;
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
                        customer.DZDH = str9;
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
                        customer.YHZH = str10;
                        customer.YJDZ = strArray2[6];
                        customer.BZ = strArray2[7];
                    }
                    while (stack.Count > 0)
                    {
                        flag2 = false;
                        if (customer.BM.Length > stack.Peek().BM.Length)
                        {
                            if (!customer.BM.StartsWith(stack.Peek().BM))
                            {
                                goto Label_03EA;
                            }
                            customer.SJBM = stack.Peek().BM;
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
                                    goto Label_03EA;
                                }
                                str5 = this.TuiJianBM(stack.Peek().BM);
                                if (customer.BM.Length == str5.Length)
                                {
                                    goto Label_03EA;
                                }
                                str7 = "无效";
                                str8 = "编码长度必须与原有同级编码长度一致";
                                result.Invalid++;
                                none = ResultType.Invalid;
                            }
                            goto Label_068B;
                        }
                        stack.Pop();
                    }
                    flag2 = true;
                Label_03EA:
                    if ((flag2 && (str4 != "001")) && (customer.BM.Length != str4.Length))
                    {
                        str7 = "无效";
                        str8 = "编码长度必须与原有同级编码长度一致";
                        result.Invalid++;
                        none = ResultType.Invalid;
                    }
                    else if ("0" != this.CheckCustomer(customer))
                    {
                        str7 = "无效";
                        str8 = "编码或名称为空";
                        result.Invalid++;
                        none = ResultType.Invalid;
                    }
                    else if (!Regex.IsMatch(customer.BM, "^[0-9a-z]{1,16}$"))
                    {
                        str7 = "无效";
                        str8 = "编码需小于16位，且仅由数字和小写字母组成";
                        result.Invalid++;
                        none = ResultType.Invalid;
                    }
                    else if (this.bmkhDAL.ExistCustomer(customer))
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
                    else if (((customer.SH != null) && (customer.SH.Length != 0)) && (((customer.SH.Length < 6) || (customer.SH.Length > 20)) || (((customer.WJ == 1) && (customer.SH.Length != 0)) && ("0000" != this.jyCustomer.CheckTaxCode(customer.SH, 0)))))
                    {
                        str7 = "失败";
                        str8 = "客户税号错误" + customer.SH;
                        result.Failed++;
                        none = ResultType.Failed;
                    }
                    else if (this.bmkhDAL.ExistCusMC(customer.BM, customer.MC, customer.SJBM))
                    {
                        str7 = "失败";
                        str8 = "客户名称与其同级单位名称重复" + customer.SH;
                        result.Failed++;
                        none = ResultType.Failed;
                    }
                    else
                    {
                        foreach (string str11 in StringUtils.GetSpellCode(customer.MC))
                        {
                            customer.KJM = customer.KJM + str11;
                        }
                        if (this.bmkhDAL.AddCustomer(customer) == "0")
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
                Label_068B:;
                    result.DtResult.Rows.Add(new object[] { customer.BM, customer.MC, str7, str8 });
                    stack.Push(new lastBMJG(customer.BM, none));
                }
                result.ImportTable = "客户编码.DB";
                result2 = result;
            }
            catch
            {
                throw;
            }
            return result2;
        }

        public ImportResult ImportDataZC(string codeFile)
        {
            new Dictionary<string, object>();
            DataTable exportData = this.bmkhDAL.GetExportData();
            exportData.Rows.Clear();
            ImportResult result = new ImportResult();
            XmlDocument document = new XmlDocument();
            try
            {
                try
                {
                    document.Load(codeFile);
                }
                catch
                {
                    throw new Exception("不符合客户编码格式");
                }
                XmlNode documentElement = document.DocumentElement;
                if (documentElement.Attributes["TYPE"].Value != "KEHUBIANMA")
                {
                    throw new Exception("不符合客户编码格式");
                }
                XmlNodeList list = null;
                list = documentElement.SelectNodes("/Data/FENLEI/Row");
                List<Dictionary<string, string>> list2 = new List<Dictionary<string, string>>();
                foreach (XmlNode node2 in list)
                {
                    Dictionary<string, string> item = new Dictionary<string, string>();
                    item.Add(node2.Attributes["MC"].Name, node2.Attributes["MC"].Value);
                    item.Add(node2.Attributes["PID"].Name, node2.Attributes["PID"].Value);
                    item.Add(node2.Attributes["BM"].Name, node2.Attributes["BM"].Value);
                    list2.Add(item);
                }
                list = documentElement.SelectNodes("/Data/KHXX/Row");
                List<Dictionary<string, string>> list3 = new List<Dictionary<string, string>>();
                foreach (XmlNode node3 in list)
                {
                    Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                    dictionary2.Add(node3.Attributes["MC"].Name, node3.Attributes["MC"].Value);
                    dictionary2.Add(node3.Attributes["PID"].Name, node3.Attributes["PID"].Value);
                    dictionary2.Add(node3.Attributes["BM"].Name, node3.Attributes["BM"].Value);
                    dictionary2.Add(node3.Attributes["YJDZ"].Name, node3.Attributes["YJDZ"].Value);
                    dictionary2.Add(node3.Attributes["YHZH"].Name, node3.Attributes["YHZH"].Value);
                    dictionary2.Add(node3.Attributes["DZ"].Name, node3.Attributes["DZ"].Value);
                    dictionary2.Add(node3.Attributes["NSRSBH"].Name, node3.Attributes["NSRSBH"].Value);
                    dictionary2.Add(node3.Attributes["JM"].Name, node3.Attributes["JM"].Value);
                    list3.Add(dictionary2);
                }
                foreach (Dictionary<string, string> dictionary3 in list2)
                {
                    DataRow row = exportData.NewRow();
                    row["BM"] = dictionary3["BM"];
                    row["MC"] = dictionary3["MC"];
                    if (dictionary3["PID"] == "0")
                    {
                        row["SJBM"] = "";
                    }
                    else
                    {
                        row["SJBM"] = dictionary3["PID"];
                    }
                    row["WJ"] = 0;
                    exportData.Rows.Add(row);
                }
                foreach (Dictionary<string, string> dictionary4 in list3)
                {
                    DataRow row2 = exportData.NewRow();
                    row2["BM"] = dictionary4["BM"];
                    row2["MC"] = dictionary4["MC"];
                    row2["SJBM"] = dictionary4["PID"];
                    row2["YJDZ"] = dictionary4["YJDZ"];
                    row2["YHZH"] = dictionary4["YHZH"];
                    row2["DZDH"] = dictionary4["DZ"];
                    row2["SH"] = dictionary4["NSRSBH"];
                    row2["JM"] = dictionary4["JM"];
                    row2["WJ"] = 1;
                    exportData.Rows.Add(row2);
                }
                DataView defaultView = exportData.DefaultView;
                defaultView.Sort = "BM";
                exportData = defaultView.ToTable();
                string pathFile = Path.ChangeExtension(codeFile, "txt");
                this.ExportData(pathFile, "~~", exportData);
                if (!string.IsNullOrEmpty(pathFile))
                {
                    result = this.ImportData(pathFile);
                    File.Delete(pathFile);
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool JianWeiVerify(string BM)
        {
            return this.bmkhDAL.JianWeiVerify(BM);
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            return this.bmkhDAL.listNodes(searchid);
        }

        public string ModifyCustomer(BMKHModel customer, string OldBM)
        {
            string str = this.CheckCustomer(customer);
            if (str != "0")
            {
                return str;
            }
            if ((customer.WJ == 1) && (customer.SH.Length != 0))
            {
                string str2 = this.jyCustomer.CheckTaxCode(customer.SH, 0);
                if (str2 != "0000")
                {
                    MessageManager.ShowMsgBox(str2, new string[] { "税号" });
                    return str2;
                }
            }
            if ((customer.BM != OldBM) && this.bmkhDAL.ExistCustomer(customer))
            {
                MessageManager.ShowMsgBox("INP-235108");
                return "编码已存在";
            }
            if (this.bmkhDAL.ExistCusMC(OldBM, customer.MC, customer.SJBM))
            {
                MessageManager.ShowMsgBox("INP-235130");
                return "客户已存在";
            }
            return this.bmkhDAL.ModifyCustomer(customer, OldBM);
        }

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            return this.bmkhDAL.QueryByKey(KeyWord, pagesize, pageno);
        }

        public DataTable QueryByMCAndSJBM(string MC, string SJBM)
        {
            return this.bmkhDAL.QueryByMCAndSJBM(MC, SJBM);
        }

        public DataTable QueryByTaxCode(string TaxCode)
        {
            return this.bmkhDAL.QueryByTaxCode(TaxCode);
        }

        public AisinoDataSet QueryData(int pagesize, int pageno)
        {
            return this.bmkhDAL.QueryCustomer(pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno)
        {
            return this.bmkhDAL.SelectNodeDisplay(selectBM, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            return this.bmkhDAL.TuiJianBM(searchid);
        }

        public string UpdateSubNodesSJBM(BMKHModel customer, string YuanBM)
        {
            string str = this.CheckCustomer(customer);
            if (str != "0")
            {
                return str;
            }
            return this.bmkhDAL.UpdateSubNodesSJBM(customer, YuanBM);
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

