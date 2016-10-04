namespace Aisino.Fwkp.Bmgl.BLL
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
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
    using System.Text.RegularExpressions;
    using System.Xml;

    internal sealed class BMCLManager : IBMBaseManager
    {
        private int _currentPage = 1;
        private DAL.BMCLManager bmclDAL = new DAL.BMCLManager();
        private InvoiceHandler jyCustomer = new InvoiceHandler();

        public string AddCustomer(BMCLModel car)
        {
            return this.AddCustomerKP(car, 0);
        }

        public string AddCustomerKP(BMCLModel car, int Addtype)
        {
            string str = this.CheckCustomer(car);
            if (str == "0")
            {
                int wJ = car.WJ;
                if (this.bmclDAL.ExistCustomer(car))
                {
                    MessageManager.ShowMsgBox("INP-235108");
                    return "编码已存在";
                }
                str = this.bmclDAL.AddCustomer(car);
                if ((Addtype == 1) && (str == "0"))
                {
                    MessageManager.ShowMsgBox("INP-235201");
                }
            }
            return str;
        }

        public string AddCustomerToAuto(BMCLModel car, string SJBM)
        {
            try
            {
                DataTable table = this.bmclDAL.QueryByMCAndSJBM(car.MC, SJBM);
                if (table.Rows.Count > 0)
                {
                    car.BM = table.Rows[0]["BM"].ToString();
                    this.bmclDAL.ModifyCustomer(car, table.Rows[0]["BM"].ToString());
                    return "OverWrite";
                }
                this.bmclDAL.AddCustomer(car);
                return "Insert";
            }
            catch
            {
                return "DBError";
            }
        }

        public DataTable AppendByKey(string KeyWord, int TopNo)
        {
            return this.bmclDAL.AppendByKey(KeyWord, TopNo);
        }

        public DataTable AppendByKeyWJ(string KeyWord, int TopNo)
        {
            return this.bmclDAL.AppendByKeyWJ(KeyWord, TopNo);
        }

        public string CheckCustomer(BMCLModel car)
        {
            if (car == null)
            {
                return "e11";
            }
            if ((car.BM == null) || (car.BM.Length == 0))
            {
                return "e12";
            }
            if ((car.MC != null) && (car.MC.Length != 0))
            {
                return "0";
            }
            return "e13";
        }

        public string ChildDetermine(string BM, string SJBM)
        {
            return this.bmclDAL.ChildDetermine(BM, SJBM);
        }

        public string DeleteData(string carCode)
        {
            return this.bmclDAL.DeleteCustomer(carCode);
        }

        public string DeleteDataFenLei(string FenLeiCodeBM)
        {
            if (!this.bmclDAL.FenLeiHasChild(FenLeiCodeBM))
            {
                return this.bmclDAL.deleteFenLei(FenLeiCodeBM);
            }
            return "e";
        }

        public string deleteFenLei(string flBM)
        {
            return this.bmclDAL.deleteFenLei(flBM);
        }

        public string deleteNodes(string searchid)
        {
            return this.bmclDAL.deleteNodes(searchid);
        }

        public string ExecZengJianWei(string BM, bool isZengWei)
        {
            if (isZengWei)
            {
                if (this.bmclDAL.ZengWeiVerify(BM))
                {
                    return this.bmclDAL.ExecZengJianWei(BM, isZengWei);
                }
                return "ezw";
            }
            if (this.bmclDAL.JianWeiVerify(BM))
            {
                return this.bmclDAL.ExecZengJianWei(BM, isZengWei);
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
            DataTable exportData = this.bmclDAL.GetExportData();
            if (exportData.Rows.Count == 0)
            {
                return "没有车辆";
            }
            StringBuilder builder = new StringBuilder();
            using (StreamWriter writer = new StreamWriter(pathFile, false, ToolUtil.GetEncoding()))
            {
                writer.WriteLine("{车辆编码}[分隔符]\"" + separator + "\"");
                writer.WriteLine("// 每行格式 :");
                string str2 = "";
                if (Flbm.IsYM())
                {
                    str2 = "// 编码~~车辆类型~~简码~~厂牌型号~~产地~~生产企业名称~~税收分类编码~~是否享受优惠政策~~税收分类名称~~优惠政策类型~~编码版本号";
                }
                else
                {
                    str2 = "// 编码~~车辆类型~~简码~~厂牌型号~~产地~~生产企业名称";
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
                    if (exportData.Rows[i]["WJ"].ToString() == "1")
                    {
                        builder.Append(separator);
                        string item = exportData.Rows[i]["CPXH"].ToString().Replace("\r\n", "#|#");
                        builder.Append(GetSafeData.ExportItem(item, separator));
                        builder.Append(separator);
                        string str5 = exportData.Rows[i]["CD"].ToString().Replace("\r\n", "#|#");
                        builder.Append(GetSafeData.ExportItem(str5, separator));
                        builder.Append(separator);
                        builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["SCCJMC"].ToString(), separator));
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
                    }
                    builder.Replace(",,", ",\"\",");
                    writer.WriteLine(builder.ToString());
                }
            }
            return "0";
        }

        public DataTable GetCL(string BM)
        {
            return this.bmclDAL.GetKH(BM);
        }

        public BMBaseModel GetModel(string BM)
        {
            DataTable kH = this.bmclDAL.GetKH(BM);
            BMCLModel model = new BMCLModel();
            if (kH.Rows.Count > 0)
            {
                DataRow row = kH.Rows[0];
                model.BM = GetSafeData.ValidateValue<string>(row, "BM");
                model.MC = GetSafeData.ValidateValue<string>(row, "MC");
                model.JM = GetSafeData.ValidateValue<string>(row, "JM");
                model.SJBM = GetSafeData.ValidateValue<string>(row, "SJBM");
                model.CPXH = GetSafeData.ValidateValue<string>(row, "CPXH");
                model.CD = GetSafeData.ValidateValue<string>(row, "CD");
                model.SCCJMC = GetSafeData.ValidateValue<string>(row, "SCCJMC");
                model.WJ = GetSafeData.ValidateValue<int>(row, "WJ");
                model.SPFL = GetSafeData.ValidateValue<string>(row, "SPFL");
                model.YHZC = GetSafeData.ValidateValue<string>(row, "YHZC");
                model.SPFLMC = GetSafeData.ValidateValue<string>(row, "SPFLMC");
                model.YHZCMC = GetSafeData.ValidateValue<string>(row, "YHZCMC");
            }
            return model;
        }

        public int GetSuggestBMLen(string SJBM)
        {
            return this.bmclDAL.GetSuggestBMLen(SJBM);
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
                    if (strArray[index].StartsWith("{车辆编码}"))
                    {
                        string str2 = strArray[index];
                        if (!str2.Contains("\""))
                        {
                            throw new CustomException("此文件首行车辆编码没有指定分隔符，分隔符使用双引号标注");
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
                            throw new CustomException("此文件不符合车辆编码文本格式");
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
                    BMCLModel model;
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
                    model = new BMCLModel {
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
                        if (strArray2.Length < 6)
                        {
                            throw new CustomException(string.Format("第{0}行数据不全", i));
                        }
                        model.WJ = 1;
                        string str9 = "";
                        if (str == "~~")
                        {
                            str9 = strArray2[3].Replace(",", "\r\n").Replace("#|#", "\r\n");
                        }
                        else if (str == " ")
                        {
                            str9 = strArray2[3].Replace("#|#", "\r\n");
                        }
                        else
                        {
                            str9 = strArray2[3];
                        }
                        model.CPXH = str9;
                        string str10 = "";
                        if (str == "~~")
                        {
                            str10 = strArray2[4].Replace(",", "\r\n").Replace("#|#", "\r\n");
                        }
                        else if (str == " ")
                        {
                            str10 = strArray2[4].Replace("#|#", "\r\n");
                        }
                        else
                        {
                            str10 = strArray2[4];
                        }
                        model.CD = str10;
                        string str11 = "";
                        if (str == "~~")
                        {
                            str11 = strArray2[5].Replace(",", "\r\n").Replace("#|#", "\r\n");
                        }
                        else if (str == " ")
                        {
                            str11 = strArray2[5].Replace("#|#", "\r\n");
                        }
                        else
                        {
                            str11 = strArray2[5];
                        }
                        model.SCCJMC = str11;
                        if (Flbm.IsYM() && (strArray2.Length > 6))
                        {
                            DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                            string bm = "";
                            if (str == "~~")
                            {
                                bm = strArray2[6].Replace(",", "\r\n").Replace("#|#", "\r\n");
                            }
                            else if (str == " ")
                            {
                                bm = strArray2[6].Replace("#|#", "\r\n");
                            }
                            else
                            {
                                bm = strArray2[6];
                            }
                            if (strArray2.Length > 7)
                            {
                                string str13 = "";
                                if (str == "~~")
                                {
                                    str13 = strArray2[7].Replace(",", "\r\n").Replace("#|#", "\r\n");
                                }
                                else if (str == " ")
                                {
                                    str13 = strArray2[7].Replace("#|#", "\r\n");
                                }
                                else
                                {
                                    str13 = strArray2[7];
                                }
                                if (manager.CanUseThisSPFLBM(bm, false, false))
                                {
                                    model.SPFL = bm;
                                    model.SPFLMC = manager.GetSPFLMCBYBM(bm);
                                    if (manager.CanUseThisYHZC(bm))
                                    {
                                        if ((str13.Trim() == "是") || (str13.Trim() == "否"))
                                        {
                                            model.YHZC = str13;
                                            if (model.YHZC == "否")
                                            {
                                                model.YHZCMC = "";
                                            }
                                        }
                                        else
                                        {
                                            model.YHZC = "否";
                                            model.YHZCMC = "";
                                        }
                                        if (strArray2.Length > 9)
                                        {
                                            bool flag3 = false;
                                            if (str13.Trim() == "是")
                                            {
                                                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { model.SPFL });
                                                if ((objArray != null) && (objArray.Length > 0))
                                                {
                                                    string[] strArray3 = (objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                                                    if (strArray3.Length > 0)
                                                    {
                                                        foreach (string str14 in strArray3)
                                                        {
                                                            if (strArray2[9].Trim() == str14)
                                                            {
                                                                model.YHZC = "是";
                                                                string str15 = "";
                                                                if (str == "~~")
                                                                {
                                                                    str15 = strArray2[9].Replace(",", "\r\n").Replace("#|#", "\r\n");
                                                                }
                                                                else if (str == " ")
                                                                {
                                                                    str15 = strArray2[9].Replace("#|#", "\r\n");
                                                                }
                                                                else
                                                                {
                                                                    str15 = strArray2[9];
                                                                }
                                                                model.YHZCMC = str15;
                                                                flag3 = true;
                                                            }
                                                            if (!Flbm.IsDK() && strArray2[9].Trim().Contains("1.5%"))
                                                            {
                                                                model.YHZC = "否";
                                                                model.YHZCMC = "";
                                                            }
                                                        }
                                                    }
                                                }
                                                if (!flag3)
                                                {
                                                    model.YHZC = "否";
                                                    model.YHZCMC = "";
                                                }
                                            }
                                            else if (str13.Trim() == "否")
                                            {
                                                model.YHZC = "否";
                                                model.YHZCMC = "";
                                            }
                                            else
                                            {
                                                model.YHZC = "否";
                                                model.YHZCMC = "";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        model.YHZC = "否";
                                        model.YHZCMC = "";
                                    }
                                }
                            }
                        }
                    }
                    while (stack.Count > 0)
                    {
                        flag2 = false;
                        if (model.BM.Length > stack.Peek().BM.Length)
                        {
                            if (!model.BM.StartsWith(stack.Peek().BM))
                            {
                                goto Label_07AE;
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
                                    goto Label_07AE;
                                }
                                str5 = this.TuiJianBM(stack.Peek().BM);
                                if (model.BM.Length == str5.Length)
                                {
                                    goto Label_07AE;
                                }
                                str7 = "无效";
                                str8 = "编码长度必须与原有同级编码长度一致";
                                result.Invalid++;
                                none = ResultType.Invalid;
                            }
                            goto Label_0940;
                        }
                        stack.Pop();
                    }
                    flag2 = true;
                Label_07AE:
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
                    else if (this.bmclDAL.ExistCustomer(model))
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
                        string[] spellCode = StringUtils.GetSpellCode(model.MC);
                        for (int j = 0; j < spellCode.Length; j++)
                        {
                            string text1 = spellCode[j];
                        }
                        if (this.bmclDAL.AddCustomer(model) == "0")
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
                Label_0940:;
                    result.DtResult.Rows.Add(new object[] { model.BM, model.MC, str7, str8 });
                    stack.Push(new lastBMJG(model.BM, none));
                }
                result.ImportTable = "车辆编码.DB";
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
                    throw new Exception("不符合机动车编码格式");
                }
                XmlNode documentElement = document.DocumentElement;
                try
                {
                    if (documentElement.Attributes["TYPE"].Value != "JDCBIANMA")
                    {
                        throw new Exception("不符合机动车编码格式");
                    }
                }
                catch
                {
                    throw new Exception("不符合机动车编码格式");
                }
                XmlNodeList list = null;
                list = documentElement.SelectNodes("/Data/CLXX/Row");
                List<Dictionary<string, string>> list2 = new List<Dictionary<string, string>>();
                foreach (XmlNode node2 in list)
                {
                    Dictionary<string, string> item = new Dictionary<string, string>();
                    if (Flbm.IsYM())
                    {
                        if (node2.Attributes["SPFLBM"] == null)
                        {
                            item.Add("SPFLBM", "");
                        }
                        else
                        {
                            item.Add(node2.Attributes["SPFLBM"].Name, node2.Attributes["SPFLBM"].Value);
                        }
                    }
                    item.Add(node2.Attributes["SCQYMC"].Name, node2.Attributes["SCQYMC"].Value);
                    item.Add(node2.Attributes["DJ"].Name, node2.Attributes["DJ"].Value);
                    item.Add(node2.Attributes["CDMC"].Name, node2.Attributes["CDMC"].Value);
                    item.Add(node2.Attributes["CPXH"].Name, node2.Attributes["CPXH"].Value);
                    item.Add(node2.Attributes["CLLXMC"].Name, node2.Attributes["CLLXMC"].Value);
                    item.Add(node2.Attributes["CLDLMC"].Name, node2.Attributes["CLDLMC"].Value);
                    item.Add(node2.Attributes["CPBM"].Name, node2.Attributes["CPBM"].Value);
                    list2.Add(item);
                }
                DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                foreach (Dictionary<string, string> dictionary2 in list2)
                {
                    BMCLModel car = new BMCLModel();
                    string searchid = "";
                    searchid = this.bmclDAL.AutoNodeLogic(dictionary2["CLDLMC"]);
                    car.SJBM = searchid;
                    car.BM = this.TuiJianBM(searchid);
                    car.MC = dictionary2["CLLXMC"];
                    car.SCCJMC = dictionary2["SCQYMC"];
                    car.CPXH = dictionary2["CPXH"];
                    car.CD = dictionary2["CDMC"];
                    if (Flbm.IsYM() && manager.CanUseThisSPFLBM(dictionary2["SPFLBM"], false, false))
                    {
                        car.SPFL = dictionary2["SPFLBM"];
                        car.SPFLMC = manager.GetSPFLMCBYBM(car.SPFL);
                        if (!manager.CanUseThisYHZC(car.SPFL))
                        {
                            car.YHZC = "否";
                            car.YHZCMC = "";
                        }
                    }
                    car.KJM = CommonFunc.GenerateKJM(car.MC);
                    car.WJ = 1;
                    string str2 = this.AddCustomerToAuto(car, car.SJBM);
                    if ("OverWrite" == str2)
                    {
                        result.Correct++;
                        result.DtResult.Rows.Add(new object[] { car.BM, car.MC, "正确传入", "覆盖记录：" + car.BM });
                    }
                    else if ("Insert" == str2)
                    {
                        result.Correct++;
                        result.DtResult.Rows.Add(new object[] { car.BM, car.MC, "正确传入", "插入成功：" + car.BM });
                    }
                    else
                    {
                        result.Failed++;
                        result.DtResult.Rows.Add(new object[] { car.BM, car.MC, "失败", "数据库读写错误" });
                    }
                }
            }
            catch
            {
                throw;
            }
            result.ImportTable = "车辆编码.DB";
            return result;
        }

        public bool JianWeiVerify(string BM)
        {
            return this.bmclDAL.JianWeiVerify(BM);
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            return this.bmclDAL.listNodes(searchid);
        }

        public string ModifyCustomer(BMCLModel car, string OldBM)
        {
            string str = this.CheckCustomer(car);
            if (str != "0")
            {
                return str;
            }
            return this.bmclDAL.ModifyCustomer(car, OldBM);
        }

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            return this.bmclDAL.QueryByKey(KeyWord, pagesize, pageno);
        }

        public DataTable QueryByMC(string MC)
        {
            return this.bmclDAL.QueryByMC(MC);
        }

        public AisinoDataSet QueryData(int pagesize, int pageno)
        {
            return this.bmclDAL.QueryCustomer(pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno)
        {
            return this.bmclDAL.SelectNodeDisplay(selectBM, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            return this.bmclDAL.TuiJianBM(searchid);
        }

        public string UpdateSubNodesSJBM(BMCLModel car, string YuanBM)
        {
            string str = this.CheckCustomer(car);
            if (str != "0")
            {
                return str;
            }
            return this.bmclDAL.UpdateSubNodesSJBM(car, YuanBM);
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

