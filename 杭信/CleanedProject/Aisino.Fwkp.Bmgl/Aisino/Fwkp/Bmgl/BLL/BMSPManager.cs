namespace Aisino.Fwkp.Bmgl.BLL
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Bmgl;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.DAL;
    using Aisino.Fwkp.Bmgl.IBLL;
    using Aisino.Fwkp.Bmgl.IDAL;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml;
    using Factory;
    internal sealed class BMSPManager : IBLL.IBMBaseManager
    {
        private int _currentPage = 1;
        private IBMSPManager bmspDAL = DALFactory.CreateInstant<IBMSPManager>("BMSPManager");
        private DataTable XTTable;

        public string AddMerchandise(BMSPModel merchandise)
        {
            string str = this.CheckMerchandise(merchandise);
            if (str != "0")
            {
                return str;
            }
            if (("" != merchandise.SJBM) && this.bmspDAL.ContainXTSP(merchandise.SJBM))
            {
                return "e4";
            }
            if (string.Empty != this.bmspDAL.GetXTCodeByName(merchandise.MC))
            {
                return "e3";
            }
            if (this.bmspDAL.ExistMerchandise(merchandise))
            {
                return "e1";
            }
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if ((((card.QYLX.ISPTFP || card.QYLX.ISZYFP) || (card.QYLX.ISNCPSG || card.QYLX.ISNCPXS)) || (card.QYLX.ISPTFPDZ || card.QYLX.ISPTFPJSP)) && this.bmspDAL.ExistSameMCXH(merchandise, merchandise.BM))
            {
                return "e2";
            }
            return this.bmspDAL.AddMerchandise(merchandise);
        }

        public DataTable AppendByKey(string KeyWord, int TopNo, double slv, string specialSP, string specialFlag)
        {
            return this.bmspDAL.AppendByKey(KeyWord, TopNo, slv, specialSP, specialFlag);
        }

        public string CheckMerchandise(BMSPModel Merchandise)
        {
            if (Merchandise == null)
            {
                return "e11";
            }
            if ((Merchandise.BM == null) || (Merchandise.BM.Length == 0))
            {
                return "e12";
            }
            if ((Merchandise.MC != null) && (Merchandise.MC.Length != 0))
            {
                return "0";
            }
            return "e13";
        }

        public bool CheckXTExist()
        {
            return this.bmspDAL.ExistXTSP();
        }

        public string CheckXTSP(string name)
        {
            string[] sPXXByName = this.bmspDAL.GetSPXXByName(name);
            bool flag = false;
            bool flag2 = false;
            if ((sPXXByName != null) && (sPXXByName.Length > 1))
            {
                int num = 0;
                for (num = 0; num < sPXXByName.Length; num += 2)
                {
                    if (string.IsNullOrEmpty(sPXXByName[num + 1]))
                    {
                        flag = true;
                    }
                    else
                    {
                        string str = sPXXByName[num + 1].Substring(0, 1);
                        string str2 = sPXXByName[num + 1].Substring(1);
                        if (XTSP_Crypt.EncodeXTGoodsName(str + name) == str2)
                        {
                            if (str == "2")
                            {
                                return "1";
                            }
                            if (str == "3")
                            {
                                return "2";
                            }
                            flag2 = true;
                        }
                        else
                        {
                            flag2 = true;
                        }
                    }
                }
                if (flag2)
                {
                    return "3";
                }
                if (flag)
                {
                    return "0";
                }
            }
            return "0";
        }

        public string ChildDetermine(string BM, string SJBM)
        {
            return this.bmspDAL.ChildDetermine(BM, SJBM);
        }

        public bool ContainXTSP(string BM)
        {
            return this.bmspDAL.ContainXTSP(BM);
        }

        private string ConvertToString(object CellDataTable)
        {
            if (CellDataTable.ToString().Length > 0)
            {
                return CellDataTable.ToString();
            }
            return "";
        }

        private BMSPModel CreateBMSPModel(string kindName, string kindCode)
        {
            string[] spellCode = null;
            try
            {
                spellCode = StringUtils.GetSpellCode(kindName);
            }
            catch
            {
            }
            string str = string.Empty;
            if ((spellCode != null) && (spellCode.Length > 0))
            {
                str = spellCode[0];
            }
            string str2 = this.TuiJianBM(string.Empty);
            if ("NoNode" == str2)
            {
                if ("ezw" == this.ExecZengJianWei("", true))
                {
                    MessageBoxHelper.Show("编码已无空位，无法导入稀土编码！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                str2 = this.TuiJianBM(string.Empty);
            }
            return new BMSPModel { BM = str2, MC = kindName, SJBM = "", DJ = 0.0, HSJBZ = false, SLV = 0.17, WJ = 0, KJM = str, ISHIDE = "0000000000", XTHASH = kindCode };
        }

        private BMSPModel CreateSubBMSPModel(string cpbm, string cpmc, string spbm, string sjbm, string kindCode)
        {
            string[] spellCode = null;
            try
            {
                spellCode = StringUtils.GetSpellCode(cpmc);
            }
            catch
            {
            }
            string str = string.Empty;
            if ((spellCode != null) && (spellCode.Length > 0))
            {
                str = spellCode[0];
            }
            return new BMSPModel { SJBM = sjbm, BM = spbm, MC = cpmc, DJ = 0.0, HSJBZ = false, SLV = 0.17, WJ = 1, KJM = str, ISHIDE = "1000000000", XTHASH = kindCode + XTSP_Crypt.EncodeXTGoodsName(kindCode + cpmc), XTCODE = cpbm };
        }

        public string DeleteData(string customerCode)
        {
            return this.bmspDAL.DeleteMerchandise(customerCode);
        }

        public string DeleteDataFenLei(string FenLeiCodeBM)
        {
            if (!this.bmspDAL.FenLeiHasChild(FenLeiCodeBM))
            {
                return this.bmspDAL.deleteFenLei(FenLeiCodeBM);
            }
            return "e";
        }

        public string deleteFenLei(string searchid)
        {
            return this.bmspDAL.deleteFenLei(searchid);
        }

        public string deleteNodes(string searchid)
        {
            return this.bmspDAL.deleteNodes(searchid);
        }

        public void DeleteXTData()
        {
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Bin\xtcpk.dat");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void DeleteXTSP()
        {
            this.bmspDAL.DeleteXTSP();
        }

        public string ExecZengJianWei(string BM, bool isZengWei)
        {
            if (isZengWei)
            {
                if (this.bmspDAL.ZengWeiVerify(BM))
                {
                    return this.bmspDAL.ExecZengJianWei(BM, isZengWei);
                }
                return "ezw";
            }
            if (this.bmspDAL.JianWeiVerify(BM))
            {
                return this.bmspDAL.ExecZengJianWei(BM, isZengWei);
            }
            return "ejw";
        }

        public string ExportData(string pathFile, string separator, DataTable spTable = null)
        {
            DataTable exportData;
            string path = pathFile.Remove(pathFile.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string maxBMBBBH = new SPFLService().GetMaxBMBBBH();
            if (spTable == null)
            {
                exportData = this.bmspDAL.GetExportData();
            }
            else
            {
                exportData = spTable;
            }
            bool flag = false;
            foreach (DataRow row in exportData.Rows)
            {
                if (string.IsNullOrEmpty(row["XTHASH"].ToString()))
                {
                    flag = true;
                }
            }
            if (exportData.Rows.Count == 0)
            {
                return "没有客户";
            }
            if (!flag)
            {
                return "稀土编码不允许导出";
            }
            StringBuilder builder = new StringBuilder();
            using (StreamWriter writer = new StreamWriter(pathFile, false, ToolUtil.GetEncoding()))
            {
                writer.WriteLine("{商品编码}[分隔符]\"" + separator + "\"");
                writer.WriteLine("// 每行格式 :");
                string str3 = "";
                if (Flbm.IsYM())
                {
                    str3 = "// 编码~~名称~~简码~~商品税目~~税率~~规格型号~~计量单位~~单价~~含税价标志~~隐藏标志~~中外合作油气田~~税收分类编码~~是否享受优惠政策~~税收分类编码名称~~优惠政策类型~~零税率标识~~编码版本号";
                }
                else
                {
                    str3 = "// 编码~~名称~~简码~~商品税目~~税率~~规格型号~~计量单位~~单价~~含税价标志~~隐藏标志~~中外合作油气田";
                }
                writer.WriteLine(str3.Replace("~~", separator));
                for (int i = 0; i < exportData.Rows.Count; i++)
                {
                    if (exportData.Rows[i]["XTHASH"].ToString().Length <= 0)
                    {
                        builder.Remove(0, builder.Length);
                        builder.Append(this.ConvertToString(exportData.Rows[i]["BM"]));
                        builder.Append(separator);
                        builder.Append(GetSafeData.ExportItem(this.ConvertToString(exportData.Rows[i]["MC"]), separator));
                        builder.Append(separator);
                        builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["JM"].ToString(), separator));
                        if (exportData.Rows[i]["WJ"].ToString() == "1")
                        {
                            builder.Append(separator);
                            builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["SPSM"].ToString(), separator));
                            builder.Append(separator);
                            builder.Append(this.ConvertToString(exportData.Rows[i]["SLV"]));
                            builder.Append(separator);
                            builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["GGXH"].ToString(), separator));
                            builder.Append(separator);
                            builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["JLDW"].ToString(), separator));
                            builder.Append(separator);
                            builder.Append(this.ConvertToString(exportData.Rows[i]["DJ"]));
                            builder.Append(separator);
                            builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["HSJBZ"].ToString(), separator));
                            builder.Append(separator);
                            builder.Append(this.ConvertToString(exportData.Rows[i]["ISHIDE"]));
                            builder.Append(separator);
                            builder.Append(this.ConvertToString(exportData.Rows[i]["HYSY"]));
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
                                builder.Append(GetSafeData.ExportItem(exportData.Rows[i]["LSLVBS"].ToString(), separator));
                                builder.Append(separator);
                                builder.Append(GetSafeData.ExportItem(maxBMBBBH, separator));
                            }
                        }
                        writer.WriteLine(builder.ToString());
                    }
                }
            }
            return "0";
        }

        public BMBaseModel GetModel(string BM)
        {
            DataTable sP = this.bmspDAL.GetSP(BM);
            BMSPModel model = new BMSPModel();
            if (sP.Rows.Count > 0)
            {
                DataRow row = sP.Rows[0];
                model.BM = GetSafeData.ValidateValue<string>(row, "BM");
                model.MC = GetSafeData.ValidateValue<string>(row, "MC");
                model.JM = GetSafeData.ValidateValue<string>(row, "JM");
                model.KJM = GetSafeData.ValidateValue<string>(row, "KJM");
                model.SJBM = GetSafeData.ValidateValue<string>(row, "SJBM");
                model.SLV = GetSafeData.ValidateValue<double>(row, "SLV");
                model.SPSM = GetSafeData.ValidateValue<string>(row, "SPSM");
                model.GGXH = GetSafeData.ValidateValue<string>(row, "GGXH");
                model.JLDW = GetSafeData.ValidateValue<string>(row, "JLDW");
                model.DJ = GetSafeData.ValidateValue<double>(row, "DJ");
                model.HSJBZ = GetSafeData.ValidateValue<bool>(row, "HSJBZ");
                model.XSSRKM = GetSafeData.ValidateValue<string>(row, "XSSRKM");
                model.YJZZSKM = GetSafeData.ValidateValue<string>(row, "YJZZSKM");
                model.XSTHKM = GetSafeData.ValidateValue<string>(row, "XSTHKM");
                model.HYSY = GetSafeData.ValidateValue<bool>(row, "HYSY");
                model.XTHASH = GetSafeData.ValidateValue<string>(row, "XTHASH");
                model.XTCODE = GetSafeData.ValidateValue<string>(row, "XTCODE");
                model.ISHIDE = GetSafeData.ValidateValue<string>(row, "ISHIDE");
                model.SPFL = GetSafeData.ValidateValue<string>(row, "SPFL");
                model.YHZC = GetSafeData.ValidateValue<string>(row, "YHZC");
                model.SPFLMC = GetSafeData.ValidateValue<string>(row, "SPFLMC");
                model.YHZCMC = GetSafeData.ValidateValue<string>(row, "YHZCMC");
                model.LSLVBS = GetSafeData.ValidateValue<string>(row, "LSLVBS");
            }
            return model;
        }

        public DataTable GetSP(string BM)
        {
            return this.bmspDAL.GetSP(BM);
        }

        public string[] GetSPXXByName(string name)
        {
            return this.bmspDAL.GetSPXXByName(name);
        }

        public int GetSuggestBMLen(string SJBM)
        {
            return this.bmspDAL.GetSuggestBMLen(SJBM);
        }

        public string GetXTCodeByName(string name)
        {
            return this.bmspDAL.GetXTCodeByName(name);
        }

        public ImportResult ImportData(string codeFile)
        {
            string[] array = File.ReadAllLines(codeFile, ToolUtil.GetEncoding());
            if (array.Length == 0)
            {
                throw new CustomException("此文件没有内容");
            }
            int index = 0;
            string str = "";
            bool flag = true;
            while (flag)
            {
                if (array[index].StartsWith("{商品编码}"))
                {
                    string str2 = array[index];
                    if (!str2.Contains("\""))
                    {
                        throw new CustomException("此文件首行商品编码没有指定分隔符，分隔符使用双引号标注");
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
                        throw new CustomException("此文件不符合商品编码文本格式");
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
            for (int i = 1; i < array.Length; i++)
            {
                BMSPModel model;
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
                model = new BMSPModel {
                    BM = strArray2[0].Trim(),
                    MC = strArray2[1].Trim(),
                    JM = strArray2[2].Trim(),
                    XTCODE = string.Empty,
                    XTHASH = string.Empty,
                    ISHIDE = "0000000000",
                };
                model.KJM = CommonFunc.GenerateKJM(model.MC);
                string str7 = "";
                string str8 = "";
                ResultType none = ResultType.None;
                if (strArray2.Length > 3)
                {
                    double num2;
                    double num3;
                    if (strArray2.Length < 6)
                    {
                        continue;
                    }
                    model.WJ = 1;
                    model.SPSM = strArray2[3].Trim();
                    model.GGXH = strArray2[5].Trim();
                    model.JLDW = strArray2[6].Trim();
                    if (double.TryParse(strArray2[4], out num2))
                    {
                        model.SLV = num2;
                    }
                    else
                    {
                        result.Failed++;
                        str7 = "失败";
                        str8 = str8 + "字段赋值失败：税率，" + strArray2[4];
                        none = ResultType.Failed;
                    }
                    if (double.TryParse(strArray2[7], out num3))
                    {
                        model.DJ = num3;
                    }
                    else
                    {
                        if (str7.Length == 0)
                        {
                            result.Failed++;
                        }
                        str7 = "失败";
                        str8 = str8 + "字段赋值失败：单价，" + strArray2[7];
                        none = ResultType.Failed;
                    }
                    bool flag3 = false;
                    if (bool.TryParse(strArray2[8], out flag3))
                    {
                        model.HSJBZ = flag3;
                        if (model.HSJBZ && (model.SLV == 0.05))
                        {
                            model.HYSY = true;
                        }
                    }
                    else if ((strArray2[8] == "0") || (strArray2[8] == "1"))
                    {
                        model.HSJBZ = strArray2[8] != "0";
                        if (model.HSJBZ && (model.SLV == 0.05))
                        {
                            model.HYSY = true;
                        }
                    }
                    else
                    {
                        if (str7.Length == 0)
                        {
                            result.Failed++;
                        }
                        str7 = "失败";
                        str8 = str8 + "字段赋值失败：含税价标志，" + strArray2[8];
                        none = ResultType.Failed;
                    }
                    if (strArray2.Length > 9)
                    {
                        if (!string.IsNullOrEmpty(strArray2[9]))
                        {
                            model.ISHIDE = strArray2[9];
                        }
                        else if (strArray2[9] == string.Empty)
                        {
                            model.ISHIDE = "0000000000";
                        }
                        else
                        {
                            if (str7.Length == 0)
                            {
                                result.Failed++;
                            }
                            str7 = "失败";
                            str8 = str8 + "字段赋值失败：隐藏标志，" + strArray2[9];
                            none = ResultType.Failed;
                        }
                    }
                    bool flag4 = false;
                    if (strArray2.Length > 10)
                    {
                        if (bool.TryParse(strArray2[10], out flag4))
                        {
                            model.HYSY = flag4;
                        }
                        else if ((strArray2[10] == "0") || (strArray2[10] == "1"))
                        {
                            model.HYSY = strArray2[10] != "0";
                        }
                        else
                        {
                            model.HYSY = false;
                        }
                    }
                    if (Flbm.IsYM())
                    {
                        DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                        if (strArray2.Length > 11)
                        {
                            if (manager.CanUseThisSPFLBM(strArray2[11].Trim(), true, false))
                            {
                                model.SPFL = strArray2[11].Trim();
                                model.SPFLMC = manager.GetSPFLMCBYBM(strArray2[11].Trim());
                                if (manager.CanUseThisYHZC(strArray2[11].Trim()))
                                {
                                    if (strArray2.Length > 12)
                                    {
                                        if ((strArray2[12].Trim() == "是") || (strArray2[12].Trim() == "否"))
                                        {
                                            model.YHZC = strArray2[12].Trim();
                                            if (model.YHZC == "否")
                                            {
                                                model.YHZCMC = "";
                                                if (model.SLV == 0.0)
                                                {
                                                    model.LSLVBS = "3";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            model.YHZC = "否";
                                            model.YHZCMC = "";
                                            if (model.SLV == 0.0)
                                            {
                                                model.LSLVBS = "3";
                                            }
                                        }
                                    }
                                    if (strArray2.Length > 14)
                                    {
                                        bool flag5 = false;
                                        if (strArray2[12].Trim() == "是")
                                        {
                                            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { model.SPFL });
                                            if ((objArray != null) && (objArray.Length > 0))
                                            {
                                                string[] strArray3 = (objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                                                if (strArray3.Length > 0)
                                                {
                                                    foreach (string str9 in strArray3)
                                                    {
                                                        if (strArray2[14].Trim() == str9)
                                                        {
                                                            model.YHZC = "是";
                                                            model.YHZCMC = strArray2[14].Trim();
                                                            flag5 = true;
                                                        }
                                                    }
                                                }
                                            }
                                            if ((model.YHZCMC == "出口零税") && (model.SLV == 0.0))
                                            {
                                                model.LSLVBS = "0";
                                            }
                                            else if ((model.YHZCMC == "免税") && (model.SLV == 0.0))
                                            {
                                                model.LSLVBS = "1";
                                            }
                                            else if ((model.YHZCMC == "不征税") && (model.SLV == 0.0))
                                            {
                                                model.LSLVBS = "2";
                                            }
                                            if (!flag5)
                                            {
                                                model.YHZC = "否";
                                                model.YHZCMC = "";
                                                if (model.SLV == 0.0)
                                                {
                                                    model.LSLVBS = "3";
                                                }
                                            }
                                        }
                                        else if (strArray2[12].Trim() == "否")
                                        {
                                            model.YHZC = "否";
                                            model.YHZCMC = "";
                                            if (model.SLV == 0.0)
                                            {
                                                model.LSLVBS = "3";
                                            }
                                        }
                                        else
                                        {
                                            model.YHZC = "否";
                                            model.YHZCMC = "";
                                            if (model.SLV == 0.0)
                                            {
                                                model.LSLVBS = "3";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    model.YHZC = "否";
                                    model.YHZCMC = "";
                                    if (model.SLV == 0.0)
                                    {
                                        model.LSLVBS = "3";
                                    }
                                }
                            }
                            if ((strArray2.Length > 15) && (((strArray2[15] == "0") || (strArray2[15] == "1")) || (((strArray2[15] == "2") || (strArray2[15] == "3")) || (strArray2[15] == ""))))
                            {
                                model.LSLVBS = strArray2[15];
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
                            goto Label_09EF;
                        }
                        model.SJBM = stack.Peek().BM;
                        if (this.bmspDAL.ContainXTSP(model.SJBM))
                        {
                            str7 = "无效";
                            str8 = "上级编码无效，且为稀土编码族";
                            none = ResultType.Invalid;
                            result.Invalid++;
                        }
                        else if (stack.Peek().Result == ResultType.Invalid)
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
                                goto Label_09EF;
                            }
                            str5 = this.TuiJianBM(stack.Peek().BM);
                            if (model.BM.Length == str5.Length)
                            {
                                goto Label_09EF;
                            }
                            str7 = "无效";
                            str8 = "编码长度必须与原有同级编码长度一致";
                            result.Invalid++;
                            none = ResultType.Invalid;
                        }
                        goto Label_0BDD;
                    }
                    stack.Pop();
                }
                flag2 = true;
            Label_09EF:
                if ((flag2 && (str4 != "001")) && (model.BM.Length != str4.Length))
                {
                    str7 = "无效";
                    str8 = "编码长度必须与原有同级编码长度一致";
                    result.Invalid++;
                    none = ResultType.Invalid;
                }
                else if ("0" != this.CheckMerchandise(model))
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
                else if (string.Empty != this.bmspDAL.GetXTCodeByName(model.MC))
                {
                    str7 = "无效";
                    str8 = "已存在相同名称的稀土商品";
                    result.Invalid++;
                    none = ResultType.Invalid;
                }
                else if (this.bmspDAL.ExistSameMCXH(model, model.BM))
                {
                    str7 = "无效";
                    str8 = "名称和规格型号不能重复";
                    result.Invalid++;
                    none = ResultType.Invalid;
                }
                else if (this.bmspDAL.ExistMerchandise(model))
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
                else if (none != ResultType.Failed)
                {
                    if (this.bmspDAL.AddMerchandise(model) == "0")
                    {
                        str7 = "正确传入";
                        result.Correct++;
                        none = ResultType.Correct;
                    }
                    else
                    {
                        str7 = "失败";
                        str8 = "保存失败";
                        result.Failed++;
                        none = ResultType.Failed;
                    }
                }
            Label_0BDD:;
                result.DtResult.Rows.Add(new object[] { model.BM, model.MC, str7, str8 });
                stack.Push(new lastBMJG(model.BM, none));
            }
            result.ImportTable = "商品编码.DB";
            if (Flbm.IsYM())
            {
                new DAL.BMSPManager().Update_SP();
            }
            return result;
        }

        public ImportResult ImportDataZC(string codeFile)
        {
            new Dictionary<string, object>();
            DataTable exportData = this.bmspDAL.GetExportData();
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
                    throw new Exception("不符合商品编码格式");
                }
                XmlNode documentElement = document.DocumentElement;
                try
                {
                    if (documentElement.Attributes["TYPE"].Value != "SPBIANMA")
                    {
                        throw new Exception("不符合商品编码格式");
                    }
                }
                catch
                {
                    throw new Exception("不符合商品编码格式");
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
                list = documentElement.SelectNodes("/Data/SPXX/Row");
                List<Dictionary<string, string>> list3 = new List<Dictionary<string, string>>();
                foreach (XmlNode node3 in list)
                {
                    Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                    dictionary2.Add(node3.Attributes["MC"].Name, node3.Attributes["MC"].Value);
                    dictionary2.Add(node3.Attributes["PID"].Name, node3.Attributes["PID"].Value);
                    dictionary2.Add(node3.Attributes["BM"].Name, node3.Attributes["BM"].Value);
                    dictionary2.Add(node3.Attributes["HSBZ"].Name, node3.Attributes["HSBZ"].Value);
                    dictionary2.Add(node3.Attributes["DJ"].Name, node3.Attributes["DJ"].Value);
                    dictionary2.Add(node3.Attributes["JLDW"].Name, node3.Attributes["JLDW"].Value);
                    dictionary2.Add(node3.Attributes["GGXH"].Name, node3.Attributes["GGXH"].Value);
                    dictionary2.Add(node3.Attributes["SL"].Name, node3.Attributes["SL"].Value);
                    dictionary2.Add(node3.Attributes["SPSM"].Name, node3.Attributes["SPSM"].Value);
                    dictionary2.Add(node3.Attributes["JM"].Name, node3.Attributes["JM"].Value);
                    if (Flbm.IsYM())
                    {
                        if (node3.Attributes["SPFLBM"] == null)
                        {
                            dictionary2.Add("SPFLBM", "");
                        }
                        else
                        {
                            dictionary2.Add(node3.Attributes["SPFLBM"].Name, node3.Attributes["SPFLBM"].Value);
                        }
                    }
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
                    bool flag = false;
                    bool.TryParse(dictionary4["HSBZ"], out flag);
                    row2["HSJBZ"] = flag;
                    float num = 0f;
                    float.TryParse(dictionary4["DJ"], out num);
                    row2["DJ"] = num;
                    row2["JLDW"] = dictionary4["JLDW"];
                    row2["GGXH"] = dictionary4["GGXH"];
                    float num2 = 0f;
                    float.TryParse(dictionary4["SL"], out num2);
                    row2["SLV"] = Math.Round((double) num2, 2);
                    row2["SPSM"] = dictionary4["SPSM"];
                    row2["JM"] = dictionary4["JM"];
                    row2["WJ"] = 1;
                    if (Flbm.IsYM())
                    {
                        row2["SPFL"] = dictionary4["SPFLBM"];
                    }
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
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public ImportResult ImportXTSP(string impPath, bool delFlag)
        {
            ImportResult result = new ImportResult();
            ProgressHinter instance = ProgressHinter.GetInstance();
            instance.SetMsg("正在导入稀土类商品编码...");
            instance.StartCycle();
            try
            {
                string str = PropertyUtil.GetValue("MAIN_PATH");
                Path.Combine(str, @"Config\Common");
                string path = string.Empty;
                if (string.Empty.Equals(impPath))
                {
                    path = Path.Combine(str, @"Bin\xtcpk.dat");
                }
                else
                {
                    path = impPath;
                }
                if (!File.Exists(path))
                {
                    throw new CustomException("不存在要导入的稀土商品文件");
                }
                string xml = string.Empty;
                try
                {
                    xml = DecodeXT.DecodeXTFile(path);
                }
                catch
                {
                    throw new CustomException("稀土商品文件解密失败");
                }
                XmlDocument document = new XmlDocument();
                try
                {
                    document.LoadXml(xml);
                }
                catch
                {
                    throw new CustomException("稀土商品文件格式不正确");
                }
                DateTime lastWriteTime = File.GetLastWriteTime(path);
                this.XTTable = this.bmspDAL.QueryAllXTSP();
                bool flag = false;
                if (this.CheckXTExist())
                {
                    if (delFlag)
                    {
                        this.DeleteXTSP();
                        flag = true;
                    }
                    else
                    {
                        string str4 = PropertyUtil.GetValue("稀土dat文件最后修改日期");
                        if (string.IsNullOrEmpty(str4))
                        {
                            this.DeleteXTSP();
                            flag = true;
                        }
                        else if (str4.Trim() != "")
                        {
                            DateTime time2 = Convert.ToDateTime(str4);
                            if (DateTime.Compare(Convert.ToDateTime(lastWriteTime.ToString()), time2) > 0)
                            {
                                this.DeleteXTSP();
                                flag = true;
                            }
                        }
                        else
                        {
                            this.DeleteXTSP();
                            flag = true;
                        }
                    }
                }
                else if (File.Exists(path))
                {
                    flag = true;
                }
                if (flag)
                {
                    XmlNodeList list = document.SelectNodes("body/data/cplb");
                    if ((list == null) || (list.Count == 0))
                    {
                        throw new CustomException("稀土商品文件格式不正确");
                    }
                    List<BMSPModel> list2 = new List<BMSPModel>();
                    foreach (XmlNode node in list)
                    {
                        XmlNode node2 = node.SelectSingleNode("head");
                        string innerText = node2["cplbmc"].InnerText;
                        string kindCode = node2["zyjgbz"].InnerText;
                        BMSPModel merchandise = this.CreateBMSPModel(innerText, kindCode);
                        if (merchandise == null)
                        {
                            return null;
                        }
                        try
                        {
                            this.bmspDAL.DelCommonSPByName(innerText);
                            if (this.XTTable != null)
                            {
                                string filterExpression = "MC = '" + innerText + "'";
                                DataRow[] rowArray = this.XTTable.Select(filterExpression);
                                if ((rowArray != null) && (rowArray.Length == 1))
                                {
                                    merchandise.ISHIDE = Convert.ToString(rowArray[0]["ISHIDE"]);
                                }
                            }
                            this.AddMerchandise(merchandise);
                            result.Correct++;
                            result.DtResult.Rows.Add(new object[] { merchandise.BM, merchandise.MC, "正确传入", "" });
                        }
                        catch
                        {
                            result.Failed++;
                            result.DtResult.Rows.Add(new object[] { merchandise.BM, merchandise.MC, "失败", "保存失败" });
                            continue;
                        }
                        list2.Clear();
                        XmlNodeList list3 = node.SelectNodes("data/row");
                        int num = 0;
                        string str8 = list3.Count.ToString().Length.ToString();
                        foreach (XmlNode node3 in list3)
                        {
                            num++;
                            string cpbm = node3["cpbm"].InnerText;
                            string mC = node3["cpmc"].InnerText;
                            string spbm = merchandise.BM + num.ToString("D" + str8);
                            this.bmspDAL.DelCommonSPByName(mC);
                            BMSPModel item = this.CreateSubBMSPModel(cpbm, mC, spbm, merchandise.BM, kindCode);
                            if (this.XTTable != null)
                            {
                                string str12 = "MC = '" + mC + "'";
                                DataRow[] rowArray2 = this.XTTable.Select(str12);
                                if ((rowArray2 != null) && (rowArray2.Length == 1))
                                {
                                    item.SPSM = Convert.ToString(rowArray2[0]["SPSM"]);
                                    item.JM = Convert.ToString(rowArray2[0]["JM"]);
                                    item.SLV = Convert.ToDouble(rowArray2[0]["SLV"]);
                                    item.GGXH = Convert.ToString(rowArray2[0]["GGXH"]);
                                    item.DJ = Convert.ToDouble(rowArray2[0]["DJ"]);
                                    item.JLDW = Convert.ToString(rowArray2[0]["JLDW"]);
                                    item.ISHIDE = Convert.ToString(rowArray2[0]["ISHIDE"]);
                                }
                            }
                            list2.Add(item);
                        }
                        string str13 = this.bmspDAL.InsertMerchandises(list2.ToArray());
                        if ((str13 != null) && (str13 == list2.Count.ToString()))
                        {
                            foreach (BMSPModel model3 in list2)
                            {
                                result.Correct++;
                                result.DtResult.Rows.Add(new object[] { model3.BM, model3.MC, "正确传入", "" });
                            }
                        }
                        else
                        {
                            foreach (BMSPModel model4 in list2)
                            {
                                result.Failed++;
                                result.DtResult.Rows.Add(new object[] { model4.BM, model4.MC, "失败", "保存失败" });
                            }
                        }
                    }
                    PropertyUtil.SetValue("稀土dat文件最后修改日期", lastWriteTime.ToString());
                }
                return result;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                instance.CloseCycle();
            }
            return result;
        }

        public bool IsNeedImportXTSP(string impPath)
        {
            if (!this.IsXTCorp())
            {
                this.DeleteXTSP();
                this.DeleteXTData();
                return false;
            }
            string str = PropertyUtil.GetValue("MAIN_PATH");
            Path.Combine(str, @"Config\Common");
            string path = string.Empty;
            bool flag = false;
            if (string.Empty.Equals(impPath))
            {
                path = Path.Combine(str, @"Bin\xtcpk.dat");
            }
            else
            {
                path = impPath;
            }
            if (!File.Exists(path))
            {
                return false;
            }
            DateTime lastWriteTime = File.GetLastWriteTime(path);
            if (this.CheckXTExist())
            {
                string str3 = PropertyUtil.GetValue("稀土dat文件最后修改日期");
                if (!string.IsNullOrEmpty(str3) && (str3.Trim() != ""))
                {
                    DateTime time2 = Convert.ToDateTime(str3);
                    if (DateTime.Compare(Convert.ToDateTime(lastWriteTime.ToString()), time2) > 0)
                    {
                        flag = true;
                    }
                    return flag;
                }
                return true;
            }
            if (File.Exists(path))
            {
                flag = true;
            }
            return flag;
        }

        public bool IsXTCorp()
        {
            string companyAuth = TaxCardFactory.CreateTaxCard().StateInfo.CompanyAuth;
            if (("0000000000" != companyAuth) && CheckCodeRoles.IsXT(companyAuth))
            {
                switch (CodeRoles.ChangeXTCodeToName(companyAuth))
                {
                    case "CORP_RATE_XTKCPSCQY":
                    case "CORP_RATE_XTYLFLQY":
                    case "CORP_RATE_XTSMQY":
                        return true;
                }
            }
            return false;
        }

        public bool JianWeiVerify(string BM)
        {
            return this.bmspDAL.JianWeiVerify(BM);
        }

        public List<TreeNodeTemp> listNodes(string searchid)
        {
            return this.bmspDAL.listNodes(searchid);
        }

        public List<TreeNodeTemp> listNodesISHIDE(string searchid)
        {
            return this.bmspDAL.listNodesISHIDE(searchid);
        }

        public string ModifyData(BMSPModel merchandise, string OldBM)
        {
            string str = this.CheckMerchandise(merchandise);
            if (str != "0")
            {
                return str;
            }
            if ((string.IsNullOrEmpty(merchandise.XTHASH) && ("" != merchandise.SJBM)) && this.bmspDAL.ContainXTSP(merchandise.SJBM))
            {
                return "e4";
            }
            if (string.IsNullOrEmpty(merchandise.XTHASH) && (string.Empty != this.bmspDAL.GetXTCodeByName(merchandise.MC)))
            {
                return "e3";
            }
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if ((((card.QYLX.ISPTFP || card.QYLX.ISZYFP) || (card.QYLX.ISNCPSG || card.QYLX.ISNCPXS)) || (card.QYLX.ISPTFPDZ || card.QYLX.ISPTFPJSP)) && this.bmspDAL.ExistSameMCXH(merchandise, OldBM))
            {
                return "e2";
            }
            return this.bmspDAL.ModifyMerchandise(merchandise, OldBM);
        }

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            return this.bmspDAL.QueryByKey(KeyWord, pagesize, pageno);
        }

        public DataTable QueryByKeyAndSlv(string KeyWord, double Slv, int WJ, int Top)
        {
            return this.bmspDAL.QueryByKeyAndSlv(KeyWord, Slv, WJ, Top);
        }

        public DataTable QueryByKeyAndSlvSEL(string KeyWord, double Slv, int WJ, int Top)
        {
            return this.bmspDAL.QueryByKeyAndSlvSEL(KeyWord, Slv, WJ, Top);
        }

        public DataTable QueryByKeyAndSpecialSPSEL(string KeyWord, string specialSP, int WJ, int Top)
        {
            return this.bmspDAL.QueryByKeyAndSpecialSPSEL(KeyWord, specialSP, WJ, Top);
        }

        public AisinoDataSet QueryByKeyDisplaySEL(string selectedBM, int Pagesize, int Pageno)
        {
            return this.bmspDAL.QueryByKeyDisplaySEL(selectedBM, Pagesize, Pageno);
        }

        public AisinoDataSet QueryByKeyDisplaySEL(string selectedBM, double slv, int Pagesize, int Pageno)
        {
            return this.bmspDAL.QueryByKeyDisplaySEL(selectedBM, slv, Pagesize, Pageno);
        }

        public AisinoDataSet QueryByKeyDisplaySEL(string selectedBM, string specialSP, int Pagesize, int Pageno)
        {
            return this.bmspDAL.QueryByKeyDisplaySEL(selectedBM, specialSP, Pagesize, Pageno);
        }

        public AisinoDataSet QueryByKeySEL(string KeyWord, int pagesize, int pageno)
        {
            return this.bmspDAL.QueryByKeySEL(KeyWord, pagesize, pageno);
        }

        public AisinoDataSet QueryData(int pagesize, int pageno)
        {
            return this.bmspDAL.QueryMerchandise(pagesize, pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectedBM, int Pagesize, int Pageno)
        {
            return this.bmspDAL.SelectNodeDisplay(selectedBM, Pagesize, Pageno);
        }

        public AisinoDataSet SelectNodeDisplay(string selectedBM, double slv, int Pagesize, int Pageno)
        {
            return this.bmspDAL.SelectNodeDisplay(selectedBM, slv, Pagesize, Pageno);
        }

        public AisinoDataSet SelectNodeDisplaySEL(string selectedBM, int Pagesize, int Pageno)
        {
            return this.bmspDAL.SelectNodeDisplaySEL(selectedBM, Pagesize, Pageno);
        }

        public AisinoDataSet SelectNodeDisplaySEL(string selectedBM, double slv, int Pagesize, int Pageno)
        {
            return this.bmspDAL.SelectNodeDisplaySEL(selectedBM, slv, Pagesize, Pageno);
        }

        public AisinoDataSet SelectNodeDisplaySEL(string selectedBM, string specialSP, int Pagesize, int Pageno)
        {
            return this.bmspDAL.SelectNodeDisplaySEL(selectedBM, specialSP, Pagesize, Pageno);
        }

        public string TuiJianBM(string searchid)
        {
            return this.bmspDAL.TuiJianBM(searchid);
        }

        public void UpdateSPIsHide(string bm, bool isHide)
        {
            this.bmspDAL.UpdateSPIsHide(bm, isHide);
        }

        public string UpdateSubNodesSJBM(BMSPModel merchandise, string YuanBM)
        {
            string str = this.CheckMerchandise(merchandise);
            if (str != "0")
            {
                return str;
            }
            return this.bmspDAL.UpdateSubNodesSJBM(merchandise, YuanBM);
        }

        public void UpdateXTIsHide(string sjbm, string hide)
        {
            this.bmspDAL.UpdateXTIsHide(sjbm, hide);
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

