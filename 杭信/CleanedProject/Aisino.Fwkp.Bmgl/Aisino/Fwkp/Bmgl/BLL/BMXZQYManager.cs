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
    using System.Text;

    internal sealed class BMXZQYManager : IBMXZQYManager
    {
        private int _currentPage = 1;
        private DAL.BMXZQYManager bmkhDAL = new DAL.BMXZQYManager();
        private InvoiceHandler jyDistrict = new InvoiceHandler();

        public string AddDistrict(BMXZQYModel xzqEntity)
        {
            return this.AddDistrictKP(xzqEntity, 0);
        }

        public string AddDistrictKP(BMXZQYModel xzqEntity, int Addtype)
        {
            string str = this.CheckDistrict(xzqEntity);
            if (str == "0")
            {
                if (this.bmkhDAL.ExistDistrict(xzqEntity))
                {
                    MessageManager.ShowMsgBox("INP-235108");
                    return "编码已存在";
                }
                str = this.bmkhDAL.AddDistrict(xzqEntity);
                if ((Addtype == 1) && (str == "0"))
                {
                    MessageManager.ShowMsgBox("INP-235201");
                }
            }
            return str;
        }

        public DataTable AppendByKey(string KeyWord, int TopNo)
        {
            return this.bmkhDAL.AppendByKey(KeyWord, TopNo);
        }

        public DataTable AppendByKeyWJ(string KeyWord, int TopNo)
        {
            return this.bmkhDAL.AppendByKeyWJ(KeyWord, TopNo);
        }

        public string CheckDistrict(BMXZQYModel xzqEntity)
        {
            if (xzqEntity == null)
            {
                return "e11";
            }
            if ((xzqEntity.BM == null) || (xzqEntity.BM.Length == 0))
            {
                return "e12";
            }
            if ((xzqEntity.MC != null) && (xzqEntity.MC.Length != 0))
            {
                return "0";
            }
            return "e13";
        }

        public string DeleteDistrict(string xzqEntityCode)
        {
            return this.bmkhDAL.DeleteDistrict(xzqEntityCode);
        }

        public string DeleteDistrictFenLei(string FenLeiCodeBM)
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

        public string ExportDistrict(string pathFile, string separator)
        {
            string path = pathFile.Remove(pathFile.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DataTable exportData = this.bmkhDAL.GetExportData();
            if (exportData.Rows.Count == 0)
            {
                return "没有行政区";
            }
            StringBuilder builder = new StringBuilder();
            using (StreamWriter writer = new StreamWriter(pathFile, false, ToolUtil.GetEncoding()))
            {
                writer.WriteLine("{行政区编码}[分隔符]\"" + separator + "\"");
                writer.WriteLine("// 每行格式 :");
                string str2 = "// 编码,名称";
                writer.WriteLine(str2.Replace(",", separator));
                for (int i = 0; i < exportData.Rows.Count; i++)
                {
                    builder.Remove(0, builder.Length);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["BM"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["MC"].ToString(), separator));
                    builder.Append(separator);
                    builder.Replace(",,", ",\"\",");
                    writer.WriteLine(builder.ToString());
                }
            }
            return "0";
        }

        public DataTable GetBMXZQY(string BM)
        {
            return this.bmkhDAL.GetBMXZQY(BM);
        }

        public BMXZQYModel GetModel(string BM)
        {
            DataTable bMXZQY = this.bmkhDAL.GetBMXZQY(BM);
            BMXZQYModel model = new BMXZQYModel();
            if (bMXZQY.Rows.Count > 0)
            {
                DataRow row = bMXZQY.Rows[0];
                model.BM = GetSafeData.ValidateValue<string>(row, "BM");
                model.MC = GetSafeData.ValidateValue<string>(row, "MC");
            }
            return model;
        }

        public int GetXZQYSJBM(string BM)
        {
            return this.bmkhDAL.GetXZQYSJBM(BM);
        }

        public ImportResult ImportDistrict(string codeFile)
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
                    if (strArray[index].StartsWith("{行政区编码}"))
                    {
                        string str2 = strArray[index];
                        if (!str2.Contains("\""))
                        {
                            throw new CustomException("此文件首行行政区编码没有指定分隔符，分隔符使用双引号标注");
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
                            throw new CustomException("此文件不符合行政区编码文本格式");
                        }
                    }
                }
                ImportResult result = new ImportResult();
                Stack<lastBMJG> stack = new Stack<lastBMJG>();
                this.TuiJianBM("");
                for (int i = index; i < strArray.Length; i++)
                {
                    string lineText = strArray[i].Trim();
                    if (((lineText.Length != 0) && !lineText.StartsWith("//")) && lineText.Contains(str))
                    {
                        string[] strArray2 = GetSafeData.Split(lineText, str);
                        if (strArray2.Length < 3)
                        {
                            throw new CustomException("文本首行指定的分隔符与实际分隔符不一致，\n格式不正确等原因导致不能导入此文件！");
                        }
                        string str5 = "";
                        string str6 = "";
                        ResultType none = ResultType.None;
                        BMXZQYModel xzqEntity = new BMXZQYModel {
                            BM = strArray2[0],
                            MC = strArray2[1]
                        };
                        if (this.bmkhDAL.ExistDistrict(xzqEntity))
                        {
                            if (str5.Length == 0)
                            {
                                str5 = "重复";
                                result.Duplicated++;
                            }
                            if (str6.Length == 0)
                            {
                                str6 = "编码重复";
                                none = ResultType.Duplicated;
                            }
                            else
                            {
                                str6 = str6 + "且编码重复";
                                none = ResultType.Invalid;
                            }
                        }
                        else if (this.bmkhDAL.AddDistrict(xzqEntity) == "0")
                        {
                            str5 = "正确传入";
                            result.Correct++;
                            none = ResultType.Correct;
                        }
                        else
                        {
                            str5 = "失败";
                            result.Failed++;
                            none = ResultType.Failed;
                        }
                        result.DtResult.Rows.Add(new object[] { xzqEntity.BM, xzqEntity.MC, str5, str6 });
                        stack.Push(new lastBMJG(xzqEntity.BM, none));
                    }
                }
                result.ImportTable = "XZQBM";
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
            return this.bmkhDAL.JianWeiVerify(BM);
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            return this.bmkhDAL.listNodes(searchid);
        }

        public string ModifyDistrict(BMXZQYModel xzqEntity, string OldBM)
        {
            string str = this.CheckDistrict(xzqEntity);
            if (str != "0")
            {
                return str;
            }
            return this.bmkhDAL.ModifyDistrict(xzqEntity, OldBM);
        }

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            return this.bmkhDAL.QueryByKey(KeyWord, pagesize, pageno);
        }

        public DataTable QueryByTaxCode(string TaxCode)
        {
            return this.bmkhDAL.QueryByTaxCode(TaxCode);
        }

        public AisinoDataSet QueryDistrict(int pagesize, int pageno)
        {
            return this.bmkhDAL.QueryDistrict(pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno)
        {
            return this.bmkhDAL.SelectNodeDisplay(selectBM, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            return this.bmkhDAL.TuiJianBM(searchid);
        }

        public string UpdateSubNodesSJBM(BMXZQYModel xzqEntity, string YuanBM)
        {
            string str = this.CheckDistrict(xzqEntity);
            if (str != "0")
            {
                return str;
            }
            return this.bmkhDAL.UpdateSubNodesSJBM(xzqEntity, YuanBM);
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

