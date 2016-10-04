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

    internal sealed class BMSPSMManager : IBMSPSMManager
    {
        private int _currentPage = 1;
        private DAL.BMSPSMManager bmkhDAL = new DAL.BMSPSMManager();
        private InvoiceHandler jyGoodsTax = new InvoiceHandler();

        public string AddGoodsTax(BMSPSMModel spsmEntity)
        {
            return this.AddGoodsTaxKP(spsmEntity, 0);
        }

        public string AddGoodsTaxKP(BMSPSMModel spsmEntity, int Addtype)
        {
            string str = this.CheckGoodsTax(spsmEntity);
            if (str == "0")
            {
                if (this.bmkhDAL.ExistGoodsTax(spsmEntity))
                {
                    MessageManager.ShowMsgBox("INP-235108");
                    return "编码已存在";
                }
                str = this.bmkhDAL.AddGoodsTax(spsmEntity);
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

        public string CheckGoodsTax(BMSPSMModel spsmEntity)
        {
            if (spsmEntity == null)
            {
                return "e11";
            }
            if ((spsmEntity.BM == null) || (spsmEntity.BM.Length == 0))
            {
                return "e12";
            }
            if ((spsmEntity.MC != null) && (spsmEntity.MC.Length != 0))
            {
                return "0";
            }
            return "e13";
        }

        public string deleteFenLei(string flBM)
        {
            return this.bmkhDAL.deleteFenLei(flBM);
        }

        public string DeleteGoodsTax(string spsmEntityCode, string SZ)
        {
            return this.bmkhDAL.DeleteGoodsTax(spsmEntityCode, SZ);
        }

        public string DeleteGoodsTaxFenLei(string FenLeiCodeBM)
        {
            if (!this.bmkhDAL.FenLeiHasChild(FenLeiCodeBM))
            {
                return this.bmkhDAL.deleteFenLei(FenLeiCodeBM);
            }
            return "e";
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

        public string ExportGoodsTax(string pathFile, string separator)
        {
            string path = pathFile.Remove(pathFile.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DataTable exportData = this.bmkhDAL.GetExportData();
            if (exportData.Rows.Count == 0)
            {
                return "没有商品税目";
            }
            StringBuilder builder = new StringBuilder();
            using (StreamWriter writer = new StreamWriter(pathFile, false, ToolUtil.GetEncoding()))
            {
                writer.WriteLine("{商品税目编码}[分隔符]\"" + separator + "\"");
                writer.WriteLine("// 每行格式 :");
                string str2 = "// 税种,编码,名称,税率,征收率,数量计税,计税单位,税额,非核定标志";
                writer.WriteLine(str2.Replace(",", separator));
                for (int i = 0; i < exportData.Rows.Count; i++)
                {
                    builder.Remove(0, builder.Length);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["SZ"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["BM"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["MC"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["SLV"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["ZSL"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["SLJS"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["JSDW"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["SE"].ToString(), separator));
                    builder.Append(separator);
                    builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["FHDBZ"].ToString(), separator));
                    builder.Append(separator);
                    builder.Replace(",,", ",\"\",");
                    writer.WriteLine(builder.ToString());
                }
            }
            return "0";
        }

        public DataTable GetBMSPSM(string BM)
        {
            return this.bmkhDAL.GetBMSPSM(BM, "");
        }

        public BMSPSMModel GetModel(string BM, string SZ = "")
        {
            DataTable bMSPSM = this.bmkhDAL.GetBMSPSM(BM, SZ);
            BMSPSMModel model = new BMSPSMModel();
            if (bMSPSM.Rows.Count > 0)
            {
                DataRow row = bMSPSM.Rows[0];
                string str = string.Empty;
                str = GetSafeData.ValidateValue<string>(row, "SZ");
                model.SZ = (str == null) ? "" : str;
                str = GetSafeData.ValidateValue<string>(row, "MC");
                model.MC = (str == null) ? "" : str;
                str = GetSafeData.ValidateValue<string>(row, "BM");
                model.BM = (str == null) ? "" : str;
                model.SLV = GetSafeData.ValidateValue<double>(row, "SLV");
                model.ZSL = GetSafeData.ValidateValue<double>(row, "ZSL");
                model.SLJS = GetSafeData.ValidateValue<byte>(row, "SLJS");
                str = GetSafeData.ValidateValue<string>(row, "JSDW");
                model.JSDW = (str == null) ? "" : str;
                model.SE = GetSafeData.ValidateValue<double>(row, "SE");
                model.FHDBZ = GetSafeData.ValidateValue<bool>(row, "FHDBZ");
                model.MDXS = GetSafeData.ValidateValue<double>(row, "MDXS");
            }
            return model;
        }

        public ImportResult ImportGoodsTax(string codeFile)
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
                    if (strArray[index].StartsWith("{商品税目编码}"))
                    {
                        string str2 = strArray[index];
                        if (!str2.Contains("\""))
                        {
                            throw new CustomException("此文件首行商品税目编码没有指定分隔符，分隔符使用双引号标注");
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
                            throw new CustomException("此文件不符合商品税目编码文本格式");
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
                        BMSPSMModel spsmEntity = new BMSPSMModel {
                            SZ = strArray2[0],
                            BM = strArray2[1],
                            MC = strArray2[2]
                        };
                        double num3 = 0.0;
                        if (!double.TryParse(strArray2[3], out num3))
                        {
                            throw new CustomException(string.Format("第{0}行数据格式不正确", i));
                        }
                        spsmEntity.SLV = num3;
                        if (!double.TryParse(strArray2[4], out num3))
                        {
                            throw new CustomException(string.Format("第{0}行数据格式不正确", i));
                        }
                        spsmEntity.ZSL = num3;
                        spsmEntity.SLJS = (strArray2[5] == "false") ? byte.Parse("0") : byte.Parse("1");
                        spsmEntity.JSDW = strArray2[6];
                        double num4 = 0.0;
                        if (!double.TryParse(strArray2[7], out num4))
                        {
                            throw new CustomException(string.Format("第{0}行数据格式不正确", i));
                        }
                        spsmEntity.SE = num4;
                        bool flag3 = false;
                        if (!bool.TryParse(strArray2[8], out flag3))
                        {
                            throw new CustomException(string.Format("第{0}行数据格式不正确", i));
                        }
                        spsmEntity.FHDBZ = flag3;
                        if (this.bmkhDAL.ExistGoodsTax(spsmEntity))
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
                        else if (this.bmkhDAL.AddGoodsTax(spsmEntity) == "0")
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
                        result.DtResult.Rows.Add(new object[] { spsmEntity.BM, spsmEntity.MC, str5, str6 });
                        stack.Push(new lastBMJG(spsmEntity.BM, none));
                    }
                }
                result.ImportTable = "SPSMBM.DB";
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

        public string ModifyGoodsTax(BMSPSMModel spsmEntity, string OldSZ, string OldBM)
        {
            string str = this.CheckGoodsTax(spsmEntity);
            if (str != "0")
            {
                return str;
            }
            return this.bmkhDAL.ModifyGoodsTax(spsmEntity, OldSZ, OldBM);
        }

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            return this.bmkhDAL.QueryByKey(KeyWord, pagesize, pageno);
        }

        public DataTable QueryByTaxCode(string TaxCode)
        {
            return this.bmkhDAL.QueryByTaxCode(TaxCode);
        }

        public AisinoDataSet QueryGoodsTax(int pagesize, int pageno)
        {
            return this.bmkhDAL.QueryGoodsTax(pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno)
        {
            return this.bmkhDAL.SelectNodeDisplay(selectBM, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            return this.bmkhDAL.TuiJianBM(searchid);
        }

        public string UpdateSubNodesSJBM(BMSPSMModel spsmEntity, string YuanBM)
        {
            string str = this.CheckGoodsTax(spsmEntity);
            if (str != "0")
            {
                return str;
            }
            return this.bmkhDAL.UpdateSubNodesSJBM(spsmEntity, YuanBM);
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

