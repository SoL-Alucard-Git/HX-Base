namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class FatchSaleBill
    {
        private SaleBillCtrl billBL = SaleBillCtrl.Instance;
        protected double DataPrecision = 1E-08;
        public static string FileFilterExcel = "Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx";
        public static string FileFilterTxt = "文本文件(*.txt)|*.txt";
        private double[] slvArray = readXMLSlv.ReadXMLSlv();

        internal bool AnalyseSaleBill(List<string> listBH, SaleBillImportTemp tmpbillImport, PriceType priceType, InvType invType, ErrorResolver errorAnalyse, SaleBill saleBill)
        {
            double round;
            SaleBillDAL ldal;
            string str7;
            string str10;
            string str11;
            string str12;
            string str13;
            string str14;
            string str15;
            string str16;
            int num2;
            string gFSH;
            double num3;
            SaleBillCheck check;
            int byteCount;
            bool flag8;
            string bH;
            string sPMC;
            int length;
            double num6;
            string str23;
            DataTable table2;
            bool flag15;
            if ((invType == InvType.Common) || (invType == InvType.Special))
            {
                saleBill.BH = tmpbillImport.BH.Trim();
                saleBill.GFMC = tmpbillImport.GFMC;
                saleBill.GFSH = tmpbillImport.GFSH.ToUpper();
                saleBill.GFDZDH = tmpbillImport.GFDZDH;
                saleBill.GFYHZH = tmpbillImport.GFYHZH;
                saleBill.XSBM = tmpbillImport.XSBM;
                saleBill.DJRQ = tmpbillImport.DJRQ;
                saleBill.DJZT = tmpbillImport.DJZT;
                saleBill.KPZT = tmpbillImport.KPZT;
                saleBill.BZ = tmpbillImport.BZ;
                saleBill.FHR = tmpbillImport.FHR;
                saleBill.SKR = tmpbillImport.SKR;
                saleBill.QDHSPMC = tmpbillImport.QDHSPMC;
                saleBill.XFYHZH = tmpbillImport.XFYHZH;
                saleBill.XFDZDH = tmpbillImport.XFDZDH;
                saleBill.DJZL = tmpbillImport.DJZL;
                saleBill.SFZJY = CommonTool.ToBoolString(tmpbillImport.SFZJY);
                saleBill.HYSY = CommonTool.ToBoolString(tmpbillImport.HYSY);
                saleBill.DJYF = saleBill.DJRQ.Month;
            }
            else if (invType == InvType.vehiclesales)
            {
                saleBill.BH = tmpbillImport.JDC_BH.Trim();
                saleBill.GFMC = tmpbillImport.JDC_GHDW;
                saleBill.GFSH = tmpbillImport.JDC_SFZ;
                saleBill.GFDZDH = tmpbillImport.JDC_LX;
                saleBill.XFDZ = tmpbillImport.JDC_CPXH;
                saleBill.KHYHMC = tmpbillImport.JDC_CD;
                saleBill.SCCJMC = tmpbillImport.JDC_SCCJMC;
                saleBill.CM = tmpbillImport.JDC_HGZH;
                saleBill.TYDH = tmpbillImport.JDC_JKZMSH;
                saleBill.QYD = tmpbillImport.JDC_SJDH;
                saleBill.ZHD = tmpbillImport.JDC_FDJH;
                saleBill.XHD = tmpbillImport.JDC_CLSBH.ToUpper();
                saleBill.XFDH = tmpbillImport.JDC_DH;
                saleBill.KHYHZH = tmpbillImport.JDC_ZH;
                saleBill.XFDZDH = tmpbillImport.JDC_DZ;
                saleBill.XFYHZH = tmpbillImport.JDC_KHYH;
                saleBill.DW = tmpbillImport.JDC_DW;
                saleBill.MDD = tmpbillImport.JDC_XCRS;
                saleBill.DJRQ = tmpbillImport.JDC_DJRQ;
                saleBill.BZ = tmpbillImport.JDC_BZ;
                saleBill.GFYHZH = tmpbillImport.JDC_NSRSBH.ToUpper();
                saleBill.DJZL = tmpbillImport.DJZL;
                saleBill.DJZT = tmpbillImport.DJZT;
                saleBill.KPZT = tmpbillImport.KPZT;
                saleBill.DJYF = saleBill.DJRQ.Month;
                if (CommonTool.isSPBMVersion())
                {
                    string cPXH = tmpbillImport.JDC_CPXH.Trim();
                    string cLLX = tmpbillImport.JDC_LX.Trim();
                    if ((cPXH != "") && (cLLX != ""))
                    {
                        round = -1.0;
                        double.TryParse(tmpbillImport.JDC_SL, out round);
                        ldal = new SaleBillDAL();
                        DataTable table = ldal.GET_CLXX_BY_CPXH(cPXH, cLLX);
                        if (table.Rows.Count > 0)
                        {
                            string str3 = table.Rows[0]["BM"].ToString();
                            string str4 = table.Rows[0]["SPFL"].ToString();
                            string str5 = table.Rows[0]["YHZC"].ToString();
                            string str6 = table.Rows[0]["SPFL_ZZSTSGL"].ToString();
                            str7 = str4.Trim();
                            string str8 = str3.Trim();
                            str10 = str5.Trim();
                            str11 = str6.Trim();
                            str12 = "";
                            tmpbillImport.JDC_XSYH = false;
                            if (((str10.ToUpper() == "TRUE") || (str10 == "1")) || (str10 == "是"))
                            {
                                if (this.CheckSlvIsYHZCSLV(tmpbillImport.JDC_SL, str11))
                                {
                                    tmpbillImport.JDC_XSYH = true;
                                }
                                else
                                {
                                    tmpbillImport.JDC_XSYH = false;
                                    str11 = "";
                                }
                            }
                            if (tmpbillImport.JDC_XSYH)
                            {
                                if (round == 0.0)
                                {
                                    str13 = "出口零税";
                                    str14 = "免税";
                                    str15 = "不征税";
                                    str16 = "普通零税率";
                                    if (str11.Contains(str13))
                                    {
                                        str12 = "0";
                                    }
                                    else if (str11.Contains(str14))
                                    {
                                        str12 = "1";
                                    }
                                    else if (str11.Contains(str15))
                                    {
                                        str12 = "2";
                                    }
                                    else if (str11.Contains(str16))
                                    {
                                        str12 = "3";
                                    }
                                    else
                                    {
                                        str12 = "3";
                                    }
                                }
                            }
                            else if (round == 0.0)
                            {
                                str12 = "3";
                            }
                            tmpbillImport.JDC_FLBM = str7;
                            tmpbillImport.JDC_FLMC = "";
                            tmpbillImport.JDC_XSYHSM = str11;
                            tmpbillImport.JDC_CLBM = str8;
                            tmpbillImport.JDC_LSLVBS = str12;
                        }
                    }
                }
                saleBill.JDC_FLBM = tmpbillImport.JDC_FLBM;
                saleBill.JDC_FLMC = "";
                saleBill.JDC_XSYH = tmpbillImport.JDC_XSYH;
                saleBill.JDC_XSYHSM = tmpbillImport.JDC_XSYHSM;
                saleBill.JDC_CLBM = tmpbillImport.JDC_CLBM;
                saleBill.JDC_LSLVBS = tmpbillImport.JDC_LSLVBS;
                this.InitInvoice(saleBill, 12);
            }
            else if (invType == InvType.transportation)
            {
                saleBill.BH = tmpbillImport.HY_BH;
                saleBill.GFMC = tmpbillImport.HY_SPFMC;
                saleBill.GFSH = tmpbillImport.HY_SPFSH.ToUpper();
                saleBill.GFDZDH = tmpbillImport.HY_SHRMC;
                saleBill.CM = tmpbillImport.HY_SHRSH.ToUpper();
                saleBill.XFDZDH = tmpbillImport.HY_FHRMC;
                saleBill.TYDH = tmpbillImport.HY_FHRSH.ToUpper();
                saleBill.XFYHZH = tmpbillImport.HY_QYMD;
                saleBill.QYD = tmpbillImport.HY_CZCH;
                saleBill.DW = tmpbillImport.HY_CHDW;
                saleBill.YSHWXX = tmpbillImport.HY_YSHWXX;
                saleBill.BZ = tmpbillImport.HY_BZ;
                saleBill.FHR = tmpbillImport.HY_FHR;
                saleBill.SKR = tmpbillImport.HY_SKR;
                saleBill.DJRQ = tmpbillImport.HY_DJRQ;
                saleBill.DJZL = tmpbillImport.DJZL;
                saleBill.DJZT = tmpbillImport.DJZT;
                saleBill.KPZT = tmpbillImport.KPZT;
                saleBill.DJYF = saleBill.DJRQ.Month;
            }
            ErrorResolver.PtSaleBill = saleBill;
            if (saleBill.BH.Trim() == "")
            {
                errorAnalyse.AddError("单据号不能为空", tmpbillImport.BH, 1, false);
                return false;
            }
            if ((saleBill.DJRQ > DateTimePicker.MaximumDateTime) || (saleBill.DJRQ < DateTimePicker.MinimumDateTime))
            {
                saleBill.DJRQ = DateTime.Now;
                saleBill.DJYF = saleBill.DJRQ.Month;
            }
            if ((invType == InvType.Common) || (invType == InvType.Special))
            {
                string str17;
                DateTime dJRQ = tmpbillImport.DJRQ;
                flag15 = 0 == 0;
                num2 = 0;
                if (!int.TryParse(tmpbillImport.GoodsNum, out num2))
                {
                    errorAnalyse.AddError("单据商品行数有错误", tmpbillImport.BH, 1, false);
                    return false;
                }
                if (listBH.Contains(tmpbillImport.BH))
                {
                    errorAnalyse.AddError("单据号有重复", tmpbillImport.BH, 0, false);
                    return false;
                }
                if (listBH.Contains(saleBill.BH))
                {
                    errorAnalyse.AddError("单据号超过50字符，截取50字符后，单据号有重复", saleBill.BH, 0, false);
                    return false;
                }
                listBH.Add(tmpbillImport.BH);
                saleBill.DJZL = CommonTool.GetInvTypeStr(invType);
                if (string.IsNullOrEmpty(tmpbillImport.BH))
                {
                    errorAnalyse.AddError("没有单据号", "", 0, false);
                    return false;
                }
                if (tmpbillImport.NCPBZ != "2")
                {
                    if (string.IsNullOrEmpty(tmpbillImport.GFMC))
                    {
                        errorAnalyse.AddError("没有购方名称", tmpbillImport.BH, 0, true);
                    }
                    if ((invType == InvType.Special) && string.IsNullOrEmpty(tmpbillImport.GFSH))
                    {
                        errorAnalyse.AddError("没有税号", tmpbillImport.BH, 0, true);
                    }
                    if (saleBill.GFSH.Length == 0x11)
                    {
                        str17 = saleBill.GFSH.Substring(15, 2);
                        gFSH = saleBill.GFSH.Substring(0, 15);
                        if (str17.CompareTo("XX") == 0)
                        {
                            saleBill.GFSH = gFSH;
                            saleBill.SFZJY = true;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(tmpbillImport.XFMC))
                    {
                        errorAnalyse.AddError("没有销方名称", tmpbillImport.BH, 0, true);
                    }
                    if ((invType == InvType.Special) && string.IsNullOrEmpty(tmpbillImport.XFSH))
                    {
                        errorAnalyse.AddError("没有销方税号", tmpbillImport.BH, 0, true);
                    }
                    if (saleBill.KHYHZH.Length == 0x11)
                    {
                        str17 = saleBill.KHYHZH.Substring(15, 2);
                        gFSH = saleBill.KHYHZH.Substring(0, 15);
                        if (str17.CompareTo("XX") == 0)
                        {
                            saleBill.KHYHZH = gFSH;
                            saleBill.SFZJY = true;
                        }
                    }
                }
                if (saleBill.BZ.IndexOf(@"\n") != -1)
                {
                    saleBill.BZ = saleBill.BZ.Replace(@"\n", "\r\n");
                }
                if (this.CheckCEZS(tmpbillImport))
                {
                    if (tmpbillImport.QDHSPMC.Trim() != "")
                    {
                        errorAnalyse.AddError("差额税单据，不允许开清单", tmpbillImport.BH, 1, false);
                        return false;
                    }
                    if (saleBill.HYSY)
                    {
                        errorAnalyse.AddError("中外合作油气田税率 不允许开具差额征税单据", tmpbillImport.BH, 1, false);
                        return false;
                    }
                }
            }
            else if (invType == InvType.vehiclesales)
            {
                DateTime time2 = tmpbillImport.JDC_DJRQ;
                flag15 = 0 == 0;
                if (listBH.Contains(tmpbillImport.JDC_BH))
                {
                    errorAnalyse.AddError("单据号有重复", tmpbillImport.JDC_BH, 0, false);
                    return false;
                }
                if (listBH.Contains(saleBill.BH))
                {
                    errorAnalyse.AddError("单据号超过50字符，截取50字符后，单据号有重复", saleBill.BH, 0, false);
                    return false;
                }
                listBH.Add(tmpbillImport.JDC_BH);
                saleBill.DJZL = CommonTool.GetInvTypeStr(invType);
                if (string.IsNullOrEmpty(tmpbillImport.JDC_BH))
                {
                    errorAnalyse.AddError("没有单据号", "", 0, false);
                    return false;
                }
                if (string.IsNullOrEmpty(tmpbillImport.JDC_GHDW))
                {
                    errorAnalyse.AddError("没有购货单位", tmpbillImport.JDC_BH, 0, true);
                }
                if (string.IsNullOrEmpty(tmpbillImport.JDC_SFZ))
                {
                    errorAnalyse.AddError("身份证号码/组织机构代码为空", tmpbillImport.JDC_BH, 0, true);
                }
                num3 = 0.0;
                if (tmpbillImport.JDC_JE.Length == 0)
                {
                    errorAnalyse.AddError("价税合计不能为空", tmpbillImport.JDC_BH, 0, false);
                    return false;
                }
                if (tmpbillImport.JDC_JE.Equals("0"))
                {
                    errorAnalyse.AddError("价税合计不能为0", tmpbillImport.JDC_BH, 0, false);
                    return false;
                }
                num3 = TodoubleValidate(tmpbillImport.JDC_JE);
                if (num3 == 0.0)
                {
                    errorAnalyse.AddError("价税合计格式不正确", tmpbillImport.JDC_BH, 0, false);
                    return false;
                }
                saleBill.JEHJ = this.GetRound(num3, 2);
                round = 0.0;
                tmpbillImport.JDC_SL = ToValidateZKL(tmpbillImport.JDC_SL);
                if (!double.TryParse(tmpbillImport.JDC_SL, out round))
                {
                    errorAnalyse.AddError("税率格式非法", tmpbillImport.JDC_BH, 0, false);
                    return false;
                }
                round = this.GetRound(round, 3);
                check = new SaleBillCheck();
                if (!check.CheckTaxRate(invType, false, round, saleBill.JDC_XSYH, saleBill.JDC_CLBM))
                {
                    errorAnalyse.AddError("税率未授权", tmpbillImport.JDC_BH, 0, true);
                }
                saleBill.SLV = round;
                if (CommonTool.isSPBMVersion() && (saleBill.JDC_FLBM.Trim() == ""))
                {
                    errorAnalyse.AddError("没有找到厂牌型号对应的分类编码", tmpbillImport.JDC_BH, 0, true);
                }
                if (saleBill.BZ.IndexOf(@"\n") != -1)
                {
                    saleBill.BZ = saleBill.BZ.Replace(@"\n", "\r\n");
                }
            }
            else if (invType == InvType.transportation)
            {
                DateTime time3 = tmpbillImport.HY_DJRQ;
                flag15 = 0 == 0;
                num2 = 0;
                if (!int.TryParse(tmpbillImport.GoodsNum, out num2))
                {
                    errorAnalyse.AddError("单据商品行数有错误", tmpbillImport.HY_BH, 1, false);
                    return false;
                }
                if (listBH.Contains(tmpbillImport.HY_BH))
                {
                    errorAnalyse.AddError("单据号有重复", tmpbillImport.HY_BH, 0, false);
                    return false;
                }
                if (listBH.Contains(saleBill.BH))
                {
                    errorAnalyse.AddError("单据号超过50字符，截取50字符后，单据号有重复", saleBill.BH, 0, false);
                    return false;
                }
                listBH.Add(tmpbillImport.HY_BH);
                saleBill.DJZL = CommonTool.GetInvTypeStr(invType);
                if (string.IsNullOrEmpty(tmpbillImport.HY_BH))
                {
                    errorAnalyse.AddError("没有单据号", "", 0, false);
                    return false;
                }
                round = 0.0;
                tmpbillImport.HY_SL = ToValidateZKL(tmpbillImport.HY_SL);
                if (!double.TryParse(tmpbillImport.HY_SL, out round))
                {
                    errorAnalyse.AddError("税率格式非法", tmpbillImport.HY_BH, 0, false);
                    return false;
                }
                round = this.GetRound(round, 2);
                check = new SaleBillCheck();
                if (!check.CheckTaxRate(invType, false, round, false, ""))
                {
                    errorAnalyse.AddError("税率未授权", tmpbillImport.HY_BH, 0, true);
                }
                saleBill.SLV = round;
                if (saleBill.BZ.IndexOf(@"\n") != -1)
                {
                    saleBill.BZ = saleBill.BZ.Replace(@"\n", "\r\n");
                }
                if (saleBill.YSHWXX.IndexOf(@"\n") != -1)
                {
                    saleBill.YSHWXX = saleBill.YSHWXX.Replace(@"\n", "\r\n");
                }
            }
            if ((invType == InvType.Common) || (invType == InvType.Special))
            {
                gFSH = "";
                gFSH = saleBill.GFSH;
                FPLX fplx = (invType == InvType.Common) ? ((FPLX) 2) : ((FPLX) 0);
                bool sFZJY = CommonTool.ToBoolString(tmpbillImport.SFZJY);
                if (!SaleBillCheck.Instance.CheckTaxCode(invType, gFSH, sFZJY, fplx) && ((invType != InvType.Special) || !string.IsNullOrEmpty(tmpbillImport.GFSH)))
                {
                    errorAnalyse.AddError("税号有错误", tmpbillImport.BH, 0, true);
                }
            }
            else if (invType == InvType.transportation)
            {
                bool flag4 = SaleBillCheck.Instance.CheckTaxCode(invType, saleBill.GFSH, false, 11);
                bool flag5 = SaleBillCheck.Instance.CheckTaxCode(invType, saleBill.TYDH, false, 11);
                bool flag6 = SaleBillCheck.Instance.CheckTaxCode(invType, saleBill.CM, false, 11);
                if (!flag4)
                {
                    errorAnalyse.AddError("实际受票方税号错误", tmpbillImport.HY_BH, 0, true);
                }
                if (!flag5)
                {
                    errorAnalyse.AddError("发货人税号错误", tmpbillImport.HY_BH, 0, true);
                }
                if (!flag6)
                {
                    errorAnalyse.AddError("收货人税号错误", tmpbillImport.HY_BH, 0, true);
                }
            }
            else if ((invType == InvType.vehiclesales) && !SaleBillCheck.Instance.CheckTaxCode(invType, saleBill.GFYHZH, false, 12))
            {
                errorAnalyse.AddError("纳税人识别号有错误", tmpbillImport.JDC_BH, 0, true);
            }
            if (SaleBillCtrl.Instance.IsSWDK())
            {
                string str19 = saleBill.XFYHZH.Trim();
                if (ToolUtil.GetByteCount(str19) > 100)
                {
                    while (ToolUtil.GetByteCount(str19) > 100)
                    {
                        str19 = str19.Substring(0, str19.Length - 1);
                    }
                    saleBill.XFYHZH = str19.Trim();
                }
            }
            if (priceType == PriceType.HanShui)
            {
                flag8 = true;
            }
            else
            {
                flag8 = false;
            }
            if ((invType == InvType.Common) || (invType == InvType.Special))
            {
                bool isCEZS = this.CheckCEZS(tmpbillImport);
                if (isCEZS && (tmpbillImport.ListGoods.Count > 1))
                {
                    errorAnalyse.AddError("差额税单据，只能包含一个商品行", tmpbillImport.BH, 1, false);
                    return false;
                }
                foreach (GoodsImportTemp temp in tmpbillImport.ListGoods)
                {
                    bH = tmpbillImport.BH;
                    if (string.IsNullOrEmpty(temp.SPMC))
                    {
                        errorAnalyse.AddError("没有商品名称", bH, temp.XH, false);
                    }
                    else
                    {
                        Goods goods;
                        sPMC = temp.SPMC;
                        byteCount = ToolUtil.GetByteCount(sPMC);
                        while (byteCount > 100)
                        {
                            length = sPMC.Length;
                            sPMC = sPMC.Substring(0, length - 1);
                            byteCount = ToolUtil.GetByteCount(sPMC);
                        }
                        temp.SPMC = sPMC;
                        temp.HSJBZ = ToValidate(temp.HSJBZ);
                        temp.JE = ToValidate(temp.JE);
                        temp.SLV = ToValidateZKL(temp.SLV);
                        temp.DJ = ToValidate(temp.DJ);
                        temp.SE = ToValidate(temp.SE);
                        temp.SL = ToValidate(temp.SL);
                        temp.ZKJE = ToValidate(temp.ZKJE.Trim());
                        temp.ZKSE = ToValidate(temp.ZKSE.Trim());
                        temp.ZKL = ToValidateZKL(temp.ZKL.Trim());
                        temp.KCE = ToValidate(temp.KCE);
                        num6 = -1.0;
                        double.TryParse(temp.SLV, out num6);
                        if (saleBill.HYSY)
                        {
                        }
                        if (CommonTool.isSPBMVersion())
                        {
                            string mC = temp.SPMC;
                            str7 = "";
                            str10 = "";
                            str11 = "";
                            str23 = "";
                            str12 = "";
                            ldal = new SaleBillDAL();
                            table2 = ldal.GET_SPXX_BY_NAME(mC, "c", "");
                            if (table2.Rows.Count > 0)
                            {
                                str23 = table2.Rows[0]["BM"].ToString();
                                str7 = table2.Rows[0]["SPFL"].ToString();
                                str10 = table2.Rows[0]["YHZC"].ToString();
                                str11 = table2.Rows[0]["SPFL_ZZSTSGL"].ToString();
                            }
                            temp.XSYH = false;
                            if (((str10.ToUpper() == "TRUE") || (str10 == "1")) || (str10 == "是"))
                            {
                                if (this.CheckSlvIsYHZCSLV(temp.SLV, str11))
                                {
                                    temp.XSYH = true;
                                    if (saleBill.HYSY)
                                    {
                                        temp.XSYH = false;
                                        str11 = "";
                                    }
                                }
                                else
                                {
                                    str11 = "";
                                }
                            }
                            if (temp.XSYH)
                            {
                                if (num6 == 0.0)
                                {
                                    str13 = "出口零税";
                                    str14 = "免税";
                                    str15 = "不征税";
                                    str16 = "普通零税率";
                                    if (str11.Contains(str13))
                                    {
                                        str12 = "0";
                                    }
                                    else if (str11.Contains(str14))
                                    {
                                        str12 = "1";
                                    }
                                    else if (str11.Contains(str15))
                                    {
                                        str12 = "2";
                                    }
                                    else if (str11.Contains(str16))
                                    {
                                        str12 = "3";
                                    }
                                    else
                                    {
                                        str12 = "3";
                                    }
                                }
                            }
                            else if (num6 == 0.0)
                            {
                                str12 = "3";
                            }
                            temp.FLBM = str7;
                            temp.FLMC = "";
                            temp.XSYHSM = str11;
                            temp.SPBM = str23;
                            temp.LSLVBS = str12;
                        }
                        if (CommonTool.isSPBMVersion() && (temp.FLBM == ""))
                        {
                            if (temp.SPMC == "详见对应正数发票及清单")
                            {
                                double result = 0.0;
                                if (double.TryParse(temp.JE, out result))
                                {
                                    if (result >= 0.0)
                                    {
                                        errorAnalyse.AddError("没有找到商品对应的分类编码", bH, temp.XH, true);
                                    }
                                }
                                else
                                {
                                    errorAnalyse.AddError("没有找到商品对应的分类编码", bH, temp.XH, true);
                                }
                            }
                            else
                            {
                                errorAnalyse.AddError("没有找到商品对应的分类编码", bH, temp.XH, true);
                            }
                        }
                        if (temp.SL == "ImportError")
                        {
                            errorAnalyse.AddError("数量有错误", bH, temp.XH, false);
                            return false;
                        }
                        if (temp.ZKJE == "ImportError")
                        {
                            errorAnalyse.AddError("折扣金额有错误", bH, temp.XH, false);
                            return false;
                        }
                        if (temp.ZKSE == "ImportError")
                        {
                            errorAnalyse.AddError("折扣税额有错误", bH, temp.XH, false);
                            return false;
                        }
                        if (temp.ZKL == "ImportError")
                        {
                            errorAnalyse.AddError("折扣率有错误", bH, temp.XH, false);
                            return false;
                        }
                        goods = new Goods {
                            XSDJBH = temp.XSDJBH,
                            XSDJBH = goods.XSDJBH.TrimEnd(new char[] { ' ' }),
                            XSDJBH = goods.XSDJBH.TrimStart(new char[] { ' ' }),
                            SPMC = temp.SPMC,
                            SPSM = temp.SPSM,
                            GGXH = temp.GGXH,
                            JLDW = temp.JLDW,
                            SL = TodoubleValidate(temp.SL),
                            DJ = TodoubleValidateJE(temp.DJ),
                            JE = this.GetRound(TodoubleValidateJE(temp.JE), 2),
                            SE = this.GetRound(TodoubleValidateJE(temp.SE), 2),
                            SLV = this.GetRound(TodoubleValidate(temp.SLV), 3),
                            ZKJE = this.GetRound(TodoubleValidate(temp.ZKJE), 2),
                            ZKSE = this.GetRound(TodoubleValidate(temp.ZKSE), 2),
                            ZKL = TodoubleValidate(temp.ZKL),
                            FLBM = ToValidateFLBM(temp.FLBM),
                            FLMC = "",
                            XSYH = ToValidateXSYH(temp.XSYH.ToString()),
                            XSYHSM = temp.XSYHSM,
                            SPBM = temp.SPBM,
                            LSLVBS = temp.LSLVBS,
                            KCE = TodoubleValidateJE(temp.KCE)
                        };
                        if (((goods.SE == 0.0) && (goods.JE == 0.0)) && (goods.SLV == 0.0))
                        {
                            if ((this.Contain(temp.JE) && this.Contain(temp.SE)) && this.Contain(temp.SLV))
                            {
                                errorAnalyse.AddError("金额、税率和税额均为0", bH, temp.XH, false);
                            }
                            else if (this.Contain(temp.SE) && this.Contain(temp.SLV))
                            {
                                errorAnalyse.AddError("没有金额、且税率和税额均为0", bH, temp.XH, false);
                            }
                            else if (this.Contain(temp.JE) && this.Contain(temp.SLV))
                            {
                                errorAnalyse.AddError("没有税额、且税率和金额均为0", bH, temp.XH, false);
                            }
                            else if (this.Contain(temp.SE) && this.Contain(temp.JE))
                            {
                                errorAnalyse.AddError("没有税率、且金额和税额均为0", bH, temp.XH, false);
                            }
                            else
                            {
                                errorAnalyse.AddError("没有金额、税率和税额", bH, temp.XH, false);
                            }
                        }
                        else if (!(this.Contain(temp.SE) || this.Contain(temp.JE)))
                        {
                            errorAnalyse.AddError("只有税率", bH, temp.XH, false);
                        }
                        else if (!(this.Contain(temp.SLV) || this.Contain(temp.JE)))
                        {
                            errorAnalyse.AddError("只有税额", bH, temp.XH, false);
                        }
                        else if (!(this.Contain(temp.SLV) || this.Contain(temp.SE)))
                        {
                            errorAnalyse.AddError("只有金额", bH, temp.XH, false);
                        }
                        else if ((goods.JE * goods.SE) < 0.0)
                        {
                            errorAnalyse.AddError("金额和税额不同号", bH, temp.XH, false);
                        }
                        else
                        {
                            if (isCEZS)
                            {
                                if (!this.Contain(temp.SLV))
                                {
                                    errorAnalyse.AddError("差额征税单据，必须传入税率", bH, temp.XH, false);
                                    return false;
                                }
                                if (!this.Contain(temp.KCE))
                                {
                                    errorAnalyse.AddError("差额征税单据，必须传入扣除额", bH, temp.XH, false);
                                    return false;
                                }
                                if (goods.KCE == 0.0)
                                {
                                    errorAnalyse.AddError("差额征税单据，扣除额不能为0", bH, temp.XH, false);
                                    return false;
                                }
                            }
                            if (isCEZS)
                            {
                                if (Math.Abs((double) (goods.SLV - 0.015)) < 1E-05)
                                {
                                    errorAnalyse.AddError("差额征税单据，禁止开具1.5%税率", bH, temp.XH, false);
                                    return false;
                                }
                                if (saleBill.HYSY && (Math.Abs((double) (goods.SLV - 0.05)) < 1E-05))
                                {
                                    errorAnalyse.AddError("差额征税单据，禁止开具中外合作油气田税率", bH, temp.XH, false);
                                    return false;
                                }
                                if (Math.Abs(goods.ZKJE) > Math.Abs((double) (goods.JE - goods.KCE)))
                                {
                                    errorAnalyse.AddError("差额征税单据，折扣金额不能 大于 差额( 含税销售额 减 扣除额)", bH, temp.XH, false);
                                    return false;
                                }
                                if ((goods.JE * goods.KCE) < 0.0)
                                {
                                    errorAnalyse.AddError(" 含税销售额 与 扣除额 不同号", bH, temp.XH, false);
                                    return false;
                                }
                            }
                            bool flag9 = this.CalcJinEShuiE(saleBill, invType, temp, goods, flag8, errorAnalyse, bH, isCEZS);
                            bool flag10 = this.Contain(temp.HSJBZ);
                            if (!this.CalcDanJiaShuLiang(saleBill.HYSY, invType, temp, goods, flag8, flag10))
                            {
                                errorAnalyse.AddError("单价、数量和金额三者关系不符", bH, temp.XH, true);
                                saleBill.DJZT = "N";
                            }
                            if (!this.CalcZheKou(flag8, saleBill.HYSY, invType, temp, goods, errorAnalyse))
                            {
                            }
                            goods.HSJBZ = temp.HSJBZ == "1";
                            if (((Math.Abs((double) (goods.SLV - 0.015)) < 1E-05) && (invType != InvType.vehiclesales)) && (invType != InvType.transportation))
                            {
                                saleBill.JZ_50_15 = true;
                            }
                            saleBill.DJZL = CommonTool.GetInvTypeStr(invType);
                            check = new SaleBillCheck();
                            double.TryParse(temp.SLV, out num6);
                            if (!check.CheckTaxRate(invType, saleBill.HYSY, num6, goods.XSYH, goods.SPBM))
                            {
                                errorAnalyse.AddError("税率未授权", bH, temp.XH, true);
                            }
                            saleBill.ListGoods.Add(goods);
                        }
                    }
                }
                if (isCEZS && this.CheckMultiSlv(saleBill))
                {
                    errorAnalyse.AddError("差额征税单据，应该是单税率", tmpbillImport.BH, 1, false);
                    return false;
                }
                if (CommonTool.isSPBMVersion())
                {
                    this.HeBingZheKouHang2(saleBill, invType);
                }
                else
                {
                    this.HeBingZheKouHang(saleBill, invType);
                }
                this.billBL.CleanBill(saleBill);
            }
            else if (invType == InvType.transportation)
            {
                double num8 = 0.0;
                saleBill.TotalAmount = 0.0;
                saleBill.TotalTax = 0.0;
                double naN = double.NaN;
                int num10 = 1;
                foreach (GoodsImportTemp temp in tmpbillImport.ListGoods)
                {
                    bH = tmpbillImport.HY_BH;
                    if (string.IsNullOrEmpty(temp.SPMC))
                    {
                        errorAnalyse.AddError("没有费用项目名称", bH, temp.XH, false);
                    }
                    else
                    {
                        Goods goods2;
                        sPMC = temp.SPMC.Trim();
                        for (byteCount = ToolUtil.GetByteCount(sPMC); byteCount > 20; byteCount = ToolUtil.GetByteCount(sPMC))
                        {
                            length = sPMC.Length;
                            sPMC = sPMC.Substring(0, length - 1);
                        }
                        temp.SPMC = sPMC;
                        if (temp.JE.Length == 0)
                        {
                            errorAnalyse.AddError("费用项目金额不能为空", bH, temp.XH, false);
                        }
                        if (temp.JE.Equals("0"))
                        {
                            errorAnalyse.AddError("费用项目金额不能为0", bH, temp.XH, false);
                        }
                        temp.JE = ToValidate(temp.JE);
                        double num11 = TodoubleValidate(temp.JE);
                        num3 = TodoubleValidate(temp.JE);
                        if (num3 == 0.0)
                        {
                            errorAnalyse.AddError("费用项目金额格式不正确", bH, temp.XH, false);
                        }
                        if (double.IsNaN(naN))
                        {
                            if (!(num3 == 0.0))
                            {
                                naN = num3;
                            }
                        }
                        else if ((num3 != 0.0) && ((num3 * naN) < 0.0))
                        {
                            errorAnalyse.AddError("费用项目金额必须同号", bH, temp.XH, false);
                        }
                        if (flag8)
                        {
                            num3 /= 1.0 + saleBill.SLV;
                        }
                        saleBill.TotalAmount += num3;
                        num8 += num3;
                        goods2 = new Goods {
                            XSDJBH = bH,
                            SPMC = temp.SPMC,
                            JE = this.GetRound(num3, 2),
                            SE = this.GetRound((double) (goods2.JE * saleBill.SLV), 2)
                        };
                        if (flag8 && !((goods2.JE + goods2.SE) == num11))
                        {
                            goods2.SE = num11 - goods2.JE;
                        }
                        goods2.XH = temp.XH;
                        if (CommonTool.isSPBMVersion())
                        {
                            string str24 = goods2.SPMC;
                            str7 = "";
                            str10 = "";
                            str11 = "";
                            str23 = "";
                            str12 = "";
                            num6 = -1.0;
                            double.TryParse(saleBill.SLV.ToString(), out num6);
                            table2 = new SaleBillDAL().GET_SPXX_BY_NAME(str24, "f", "");
                            if (table2.Rows.Count > 0)
                            {
                                str23 = table2.Rows[0]["BM"].ToString();
                                str7 = table2.Rows[0]["SPFL"].ToString();
                                str10 = table2.Rows[0]["YHZC"].ToString();
                                str11 = table2.Rows[0]["SPFL_ZZSTSGL"].ToString();
                            }
                            goods2.XSYH = false;
                            if (((str10.ToUpper() == "TRUE") || (str10 == "1")) || (str10 == "是"))
                            {
                                if (this.CheckSlvIsYHZCSLV(saleBill.SLV.ToString(), str11))
                                {
                                    goods2.XSYH = true;
                                }
                                else
                                {
                                    str11 = "";
                                }
                            }
                            if (goods2.XSYH)
                            {
                                if (num6 == 0.0)
                                {
                                    str13 = "出口零税";
                                    str14 = "免税";
                                    str15 = "不征税";
                                    str16 = "普通零税率";
                                    if (str11.Contains(str13))
                                    {
                                        str12 = "0";
                                    }
                                    else if (str11.Contains(str14))
                                    {
                                        str12 = "1";
                                    }
                                    else if (str11.Contains(str15))
                                    {
                                        str12 = "2";
                                    }
                                    else if (str11.Contains(str16))
                                    {
                                        str12 = "3";
                                    }
                                    else
                                    {
                                        str12 = "3";
                                    }
                                }
                            }
                            else if (num6 == 0.0)
                            {
                                str12 = "3";
                            }
                            goods2.FLBM = str7;
                            goods2.FLMC = "";
                            goods2.XSYHSM = str11;
                            goods2.SPBM = str23;
                            goods2.LSLVBS = str12;
                        }
                        if (CommonTool.isSPBMVersion() && (goods2.FLBM == ""))
                        {
                            errorAnalyse.AddError("没有找到费用项目对应的分类编码", bH, goods2.XH, true);
                        }
                        saleBill.ListGoods.Add(goods2);
                        num10++;
                    }
                }
                saleBill.JEHJ = num8;
            }
            return true;
        }

        private bool CalcDanJiaShuLiang(bool HYSYsign, InvType invType, GoodsImportTemp mx, Goods mxWare, bool JesmTemp, bool Have_JGFS)
        {
            double round;
            double num = 0.01;
            double num3 = TodoubleValidate(mx.DJ);
            double num4 = TodoubleValidate(mx.SL);
            double num5 = 0.0;
            double jE = mxWare.JE;
            double sE = mxWare.SE;
            double kCE = mxWare.KCE;
            if (jE < 0.0)
            {
                kCE = -1.0 * Math.Abs(kCE);
            }
            else
            {
                kCE = Math.Abs(kCE);
            }
            bool hSJBZ = mxWare.HSJBZ;
            if (((mxWare.SLV == 0.05) && HYSYsign) && (invType == InvType.Special))
            {
                if (!this.Contain(mx.DJ))
                {
                    if (num4 == 0.0)
                    {
                        num5 = num3;
                        mx.HSJBZ = CommonTool.ToStringBool(true);
                        return true;
                    }
                    num5 = (jE + sE) / num4;
                    mxWare.DJ = num5;
                }
                else
                {
                    num5 = num3;
                    if (Have_JGFS)
                    {
                        if (mx.HSJBZ == "1")
                        {
                            round = this.GetRound((double) (((num3 * num4) - jE) - sE), 8);
                            mxWare.DJ = num3;
                        }
                        else
                        {
                            round = this.GetRound((double) ((num3 * num4) - jE), 8);
                            num5 = this.GetRound((double) (num3 / 0.95), 8);
                            mxWare.DJ = num5;
                        }
                        if (round < 0.0)
                        {
                            round = -round;
                        }
                        if (round > num)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        round = this.GetRound((double) ((num3 * num4) - jE), 8);
                        if (round < 0.0)
                        {
                            round = -round;
                        }
                        if (round <= num)
                        {
                            if (sE == 0.0)
                            {
                                mxWare.DJ = num3;
                            }
                            else
                            {
                                num5 = this.GetRound((double) (num3 / 0.95), 8);
                                mxWare.DJ = num5;
                            }
                        }
                        else
                        {
                            round = this.GetRound((double) (((num3 * num4) - jE) - sE), 8);
                            if (round < 0.0)
                            {
                                round = -round;
                            }
                            if (round <= num)
                            {
                                mxWare.DJ = num3;
                            }
                            else
                            {
                                mx.HSJBZ = CommonTool.ToStringBool(JesmTemp);
                                return false;
                            }
                        }
                    }
                }
                mx.HSJBZ = CommonTool.ToStringBool(true);
                return true;
            }
            if (Math.Abs((double) (mxWare.SLV - 0.015)) < 1E-05)
            {
                if ((mxWare.DJ == 0.0) && (mxWare.SL != 0.0))
                {
                    if (CommonTool.JZ50_15_DJType() != "1")
                    {
                        if (!Have_JGFS)
                        {
                            num5 = Math.Round((double) (mxWare.JE / mxWare.SL), 2);
                            if (Math.Abs(Math.Round((double) (((mxWare.SL * num5) - mxWare.JE) - mxWare.SE), 3)) > num)
                            {
                                num5 = Math.Round((double) (mxWare.JE / mxWare.SL), 2);
                                if (Math.Abs(Math.Round((double) ((mxWare.SL * num5) - mxWare.JE), 3)) > num)
                                {
                                    mx.HSJBZ = CommonTool.ToStringBool(JesmTemp);
                                    return false;
                                }
                                if (mxWare.SE == 0.0)
                                {
                                    mxWare.DJ = num3;
                                    mx.HSJBZ = CommonTool.ToStringBool(false);
                                }
                                else
                                {
                                    mxWare.DJ = num5;
                                    mx.HSJBZ = CommonTool.ToStringBool(true);
                                }
                            }
                            else if (mxWare.SE == 0.0)
                            {
                                mxWare.DJ = num3;
                                mx.HSJBZ = CommonTool.ToStringBool(false);
                            }
                            else
                            {
                                mxWare.DJ = num5;
                                mx.HSJBZ = CommonTool.ToStringBool(true);
                            }
                        }
                        else if (mx.HSJBZ.Trim() == "1")
                        {
                            mxWare.DJ = Math.Round((double) ((mxWare.JE + mxWare.SE) / mxWare.SL), 2);
                        }
                        else
                        {
                            mxWare.DJ = Math.Round((double) (mxWare.JE / mxWare.SL), 2);
                        }
                    }
                    else
                    {
                        mxWare.DJ = Math.Round((double) ((mxWare.JE + mxWare.SE) / mxWare.SL), 2);
                        mx.HSJBZ = CommonTool.ToStringBool(true);
                    }
                }
                else if ((mxWare.DJ != 0.0) && (mxWare.SL == 0.0))
                {
                    if (Have_JGFS)
                    {
                        if (mx.HSJBZ.Trim() == "1")
                        {
                            mxWare.SL = Math.Round((double) ((mxWare.JE + mxWare.SE) / mxWare.DJ), 8);
                        }
                        else
                        {
                            mxWare.SL = Math.Round((double) (mxWare.JE / mxWare.DJ), 8);
                        }
                    }
                    else
                    {
                        mxWare.SL = Math.Round((double) (mxWare.JE / mxWare.DJ), 8);
                        mx.HSJBZ = CommonTool.ToStringBool(false);
                    }
                }
                if ((mxWare.DJ != 0.0) && (mxWare.SL != 0.0))
                {
                    double num9 = 0.0;
                    if (!Have_JGFS)
                    {
                        if (Math.Abs(Math.Round((double) (((mxWare.SL * mxWare.DJ) - mxWare.JE) - mxWare.SE), 3)) > num)
                        {
                            if (Math.Abs(Math.Round((double) ((mxWare.SL * mxWare.DJ) - mxWare.JE), 3)) > num)
                            {
                                mx.HSJBZ = CommonTool.ToStringBool(JesmTemp);
                                return false;
                            }
                            mx.HSJBZ = CommonTool.ToStringBool(false);
                        }
                        else
                        {
                            mx.HSJBZ = CommonTool.ToStringBool(true);
                        }
                    }
                    else
                    {
                        if (mx.HSJBZ.Trim() == "1")
                        {
                            num9 = Math.Abs(Math.Round((double) (((mxWare.SL * mxWare.DJ) - mxWare.JE) - mxWare.SE), 3));
                        }
                        else
                        {
                            num9 = Math.Abs(Math.Round((double) ((mxWare.SL * mxWare.DJ) - mxWare.JE), 3));
                        }
                        if (num9 > num)
                        {
                            return false;
                        }
                    }
                }
            }
            else if (!this.Contain(mx.DJ) || mx.DJ.Equals("0"))
            {
                if (num4 == 0.0)
                {
                    num5 = num3;
                    return true;
                }
                if (Have_JGFS)
                {
                    if (mx.HSJBZ == "1")
                    {
                        num5 = (jE + sE) / num4;
                    }
                    else
                    {
                        num5 = jE / num4;
                    }
                }
                else
                {
                    if (JesmTemp)
                    {
                        num5 = (jE + sE) / num4;
                    }
                    else
                    {
                        num5 = jE / num4;
                    }
                    mx.HSJBZ = CommonTool.ToStringBool(JesmTemp);
                }
                mxWare.DJ = num5;
            }
            else
            {
                num5 = num3;
                if (!this.Contain(mx.SL) || mx.SL.Equals("0"))
                {
                    if (Have_JGFS)
                    {
                        if (mx.HSJBZ == "1")
                        {
                            num4 = (jE + sE) / num5;
                        }
                        else
                        {
                            num4 = jE / num5;
                        }
                    }
                    else
                    {
                        num4 = jE / num5;
                    }
                    mxWare.SL = num4;
                    mxWare.DJ = num3;
                }
                else if (Have_JGFS)
                {
                    if (mx.HSJBZ == "1")
                    {
                        round = this.GetRound((double) (((num3 * num4) - jE) - sE), 4);
                        num5 = num3;
                        mxWare.DJ = num5;
                    }
                    else
                    {
                        round = this.GetRound((double) ((num3 * num4) - jE), 4);
                        mxWare.DJ = num3;
                    }
                    if (round < 0.0)
                    {
                        round = -round;
                    }
                    if (round > num)
                    {
                        return false;
                    }
                }
                else
                {
                    round = this.GetRound((double) ((num3 * num4) - jE), 4);
                    if (round < 0.0)
                    {
                        round = -round;
                    }
                    if (round <= num)
                    {
                        mx.HSJBZ = CommonTool.ToStringBool(false);
                    }
                    else
                    {
                        round = this.GetRound((double) (((num3 * num4) - jE) - sE), 4);
                        if (round < 0.0)
                        {
                            round = -round;
                        }
                        if (round <= num)
                        {
                            num5 = num3;
                            mxWare.DJ = num5;
                            mx.HSJBZ = CommonTool.ToStringBool(true);
                        }
                        else
                        {
                            mx.HSJBZ = CommonTool.ToStringBool(JesmTemp);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool CalcDanJiaShuLiang_backup(bool HYSYsign, InvType invType, GoodsImportTemp mx, Goods mxWare, bool JesmTemp, bool bJGFS)
        {
            double round;
            double num = 0.01;
            double num3 = TodoubleValidate(mx.DJ);
            double num4 = TodoubleValidate(mx.SL);
            double num5 = 0.0;
            double jE = mxWare.JE;
            double sE = mxWare.SE;
            if (((mxWare.SLV == 0.05) && HYSYsign) && (invType == InvType.Special))
            {
                if (!this.Contain(mx.DJ))
                {
                    if (num4 == 0.0)
                    {
                        num5 = num3;
                        mx.HSJBZ = CommonTool.ToStringBool(true);
                        return true;
                    }
                    num5 = (jE + sE) / num4;
                    mxWare.DJ = num5;
                }
                else
                {
                    num5 = num3;
                    if (bJGFS)
                    {
                        if (mx.HSJBZ == "1")
                        {
                            round = this.GetRound((double) (((num3 * num4) - jE) - sE), 8);
                            mxWare.DJ = num3;
                        }
                        else
                        {
                            round = this.GetRound((double) ((num3 * num4) - jE), 8);
                            num5 = this.GetRound((double) (num3 / 0.95), 8);
                            mxWare.DJ = num5;
                        }
                        if (round < 0.0)
                        {
                            round = -round;
                        }
                        if (round > num)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        round = this.GetRound((double) ((num3 * num4) - jE), 8);
                        if (round < 0.0)
                        {
                            round = -round;
                        }
                        if (round <= num)
                        {
                            if (sE == 0.0)
                            {
                                mxWare.DJ = num3;
                            }
                            else
                            {
                                num5 = this.GetRound((double) (num3 / 0.95), 8);
                                mxWare.DJ = num5;
                            }
                        }
                        else
                        {
                            round = this.GetRound((double) (((num3 * num4) - jE) - sE), 8);
                            if (round < 0.0)
                            {
                                round = -round;
                            }
                            if (round <= num)
                            {
                                mxWare.DJ = num3;
                            }
                            else
                            {
                                mx.HSJBZ = CommonTool.ToStringBool(JesmTemp);
                                return false;
                            }
                        }
                    }
                }
                mx.HSJBZ = CommonTool.ToStringBool(true);
                return true;
            }
            if (Math.Abs((double) (mxWare.SLV - 0.015)) < 1E-05)
            {
                if ((mxWare.DJ == 0.0) && (mxWare.SL != 0.0))
                {
                    if (CommonTool.JZ50_15_DJType() == "1")
                    {
                        mxWare.DJ = Math.Round((double) ((mxWare.JE + mxWare.SE) / mxWare.SL), 2);
                        mxWare.HSJBZ = true;
                    }
                    else
                    {
                        if (mx.HSJBZ.Trim() == "1")
                        {
                            mxWare.HSJBZ = true;
                        }
                        else if (mx.HSJBZ.Trim() == "0")
                        {
                            mxWare.HSJBZ = false;
                        }
                        else
                        {
                            mxWare.HSJBZ = JesmTemp;
                        }
                        if (mxWare.HSJBZ)
                        {
                            mxWare.DJ = Math.Round((double) ((mxWare.JE + mxWare.SE) / mxWare.SL), 2);
                        }
                        else
                        {
                            mxWare.DJ = Math.Round((double) (mxWare.JE / mxWare.SL), 2);
                        }
                    }
                }
                else if ((mxWare.DJ != 0.0) && (mxWare.SL == 0.0))
                {
                    if (mx.HSJBZ.Trim() == "1")
                    {
                        mxWare.HSJBZ = true;
                    }
                    else if (mx.HSJBZ.Trim() == "0")
                    {
                        mxWare.HSJBZ = false;
                    }
                    else
                    {
                        mxWare.HSJBZ = JesmTemp;
                    }
                    if (mxWare.HSJBZ)
                    {
                        mxWare.SL = Math.Round((double) ((mxWare.JE + mxWare.SE) / mxWare.DJ), 8);
                    }
                    else
                    {
                        mxWare.SL = Math.Round((double) (mxWare.JE / mxWare.DJ), 8);
                    }
                }
                else if (mxWare.JE == 0.0)
                {
                    if (mx.HSJBZ.Trim() == "1")
                    {
                        mxWare.HSJBZ = true;
                    }
                    else if (mx.HSJBZ.Trim() == "0")
                    {
                        mxWare.HSJBZ = false;
                    }
                    else
                    {
                        mxWare.HSJBZ = JesmTemp;
                    }
                    if (mxWare.HSJBZ)
                    {
                        mxWare.JE = Math.Round((double) ((mxWare.DJ * mxWare.SL) - mxWare.SE), 2);
                    }
                    else
                    {
                        mxWare.JE = Math.Round((double) (mxWare.DJ * mxWare.SL), 2);
                    }
                }
                if (mxWare.HSJBZ)
                {
                    if (((mxWare.DJ != 0.0) && (mxWare.SL != 0.0)) && (Math.Abs(Math.Round((double) (((mxWare.SL * mxWare.DJ) - mxWare.JE) - mxWare.SE), 3)) > num))
                    {
                        return false;
                    }
                }
                else if ((mxWare.SL != 0.0) && (Math.Abs(Math.Round((double) ((mxWare.JE * 0.014492753623188406) - mxWare.SE), 3)) > num))
                {
                    return false;
                }
            }
            else if (!this.Contain(mx.DJ) || mx.DJ.Equals("0"))
            {
                if (num4 == 0.0)
                {
                    num5 = num3;
                    return true;
                }
                if (bJGFS)
                {
                    if (mx.HSJBZ == "1")
                    {
                        num5 = (jE + sE) / num4;
                        mxWare.HSJBZ = true;
                    }
                    else
                    {
                        num5 = jE / num4;
                    }
                }
                else
                {
                    if (JesmTemp)
                    {
                        num5 = (jE + sE) / num4;
                    }
                    else
                    {
                        num5 = jE / num4;
                    }
                    mx.HSJBZ = CommonTool.ToStringBool(JesmTemp);
                }
                mxWare.DJ = num5;
            }
            else
            {
                num5 = num3;
                if (!this.Contain(mx.SL) || mx.SL.Equals("0"))
                {
                    if (bJGFS)
                    {
                        if (mx.HSJBZ == "1")
                        {
                            num4 = (jE + sE) / num5;
                            mxWare.HSJBZ = true;
                        }
                        else
                        {
                            num4 = jE / num5;
                        }
                    }
                    else
                    {
                        num4 = jE / num5;
                    }
                    mxWare.SL = num4;
                    mxWare.DJ = num3;
                }
                else if (bJGFS)
                {
                    if (mx.HSJBZ == "1")
                    {
                        round = this.GetRound((double) (((num3 * num4) - jE) - sE), 4);
                        num5 = num3;
                        mxWare.DJ = num5;
                        mxWare.HSJBZ = true;
                    }
                    else
                    {
                        round = this.GetRound((double) ((num3 * num4) - jE), 4);
                        mxWare.DJ = num3;
                    }
                    if (round < 0.0)
                    {
                        round = -round;
                    }
                    if (round > num)
                    {
                        return false;
                    }
                }
                else
                {
                    round = this.GetRound((double) ((num3 * num4) - jE), 4);
                    if (round < 0.0)
                    {
                        round = -round;
                    }
                    if (round <= num)
                    {
                        mx.HSJBZ = CommonTool.ToStringBool(false);
                    }
                    else
                    {
                        round = this.GetRound((double) (((num3 * num4) - jE) - sE), 4);
                        if (round < 0.0)
                        {
                            round = -round;
                        }
                        if (round <= num)
                        {
                            num5 = num3;
                            mxWare.DJ = num5;
                            mx.HSJBZ = CommonTool.ToStringBool(true);
                        }
                        else
                        {
                            mx.HSJBZ = CommonTool.ToStringBool(JesmTemp);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool CalcJinEShuiE(SaleBill bill, InvType invType, GoodsImportTemp mx, Goods mxWare, bool JesmTemp, ErrorResolver errorAnalyse, string DJBH, bool IsCEZS)
        {
            double num9;
            double num = 0.06;
            bool hYSY = bill.HYSY;
            bool flag2 = JesmTemp;
            bool flag3 = JesmTemp;
            double num3 = 0.0;
            double num4 = 0.0;
            double round = 0.0;
            double jE = mxWare.JE;
            double sE = mxWare.SE;
            double sLV = mxWare.SLV;
            if (jE < 0.0)
            {
                mxWare.KCE = -1.0 * Math.Abs(mxWare.KCE);
            }
            else
            {
                mxWare.KCE = Math.Abs(mxWare.KCE);
            }
            if ((hYSY && (sLV == 0.05)) && (invType == InvType.Special))
            {
                if (flag3)
                {
                    if (!(this.Contain(mx.JE) && !mx.JE.Equals("0")))
                    {
                        jE = this.GetRound((double) (sE / sLV), 2);
                    }
                    if (!this.Contain(mx.SE))
                    {
                        sE = jE * sLV;
                    }
                    round = this.GetRound((double) (jE - sE), 2);
                    if (this.GetRound(Math.Abs((double) (this.GetRound((double) (jE * sLV), 2) - sE)), 2) <= num)
                    {
                        mxWare.JE = this.GetRound(round, 2);
                        mxWare.SE = this.GetRound(sE, 2);
                        mxWare.SLV = sLV;
                        return true;
                    }
                    errorAnalyse.AddError("中外合作油气田单据税额有错误", DJBH, mx.XH, false);
                    return false;
                }
                if (!(this.Contain(mx.JE) && !mx.JE.Equals("0")))
                {
                    jE = this.GetRound((double) (sE / sLV), 2) - sE;
                    round = jE;
                }
                if (!this.Contain(mx.SE))
                {
                    sE = (jE / (1.0 - sLV)) * sLV;
                    round = this.GetRound(jE, 2);
                }
                else
                {
                    round = jE;
                }
                if (this.GetRound(Math.Abs((double) (this.GetRound((double) ((jE + sE) * sLV), 2) - sE)), 2) <= num)
                {
                    mxWare.JE = this.GetRound(round, 2);
                    mxWare.SE = this.GetRound(sE, 2);
                    mxWare.SLV = sLV;
                    return true;
                }
                errorAnalyse.AddError("税额有错误", DJBH, mx.XH, false);
                return false;
            }
            if (!this.Contain(mx.JE) || mx.JE.Equals("0"))
            {
                if (sLV == 0.0)
                {
                    errorAnalyse.AddError("没有金额且税率为0", DJBH, mx.XH, false);
                    return false;
                }
                if (((Math.Abs((double) (sLV - 0.015)) < 1E-05) && (invType != InvType.vehiclesales)) && (invType != InvType.transportation))
                {
                    num9 = Finacial.Mul(sE, 1.05, 15) / 0.015;
                    round = num9 - sE;
                }
                else
                {
                    if (!(sLV == 0.0))
                    {
                        round = this.GetRound((double) (sE / sLV), 2);
                    }
                    else
                    {
                        return false;
                    }
                    if (IsCEZS)
                    {
                        if (sE < 0.0)
                        {
                            mxWare.KCE = -1.0 * Math.Abs(mxWare.KCE);
                        }
                        else
                        {
                            mxWare.KCE = Math.Abs(mxWare.KCE);
                        }
                        if (!(sLV == 0.0))
                        {
                            round = (sE / sLV) + mxWare.KCE;
                        }
                        if (round >= 0.0)
                        {
                            if (round < mxWare.KCE)
                            {
                                errorAnalyse.AddError("不含税销售额 不能小于 扣除额" + mxWare.KCE.ToString(), DJBH, mx.XH, false);
                                return false;
                            }
                        }
                        else if (round > mxWare.KCE)
                        {
                            errorAnalyse.AddError("负数单据，不含税销售额 不能大于 扣除额" + mxWare.KCE.ToString(), DJBH, mx.XH, false);
                            return false;
                        }
                    }
                }
            }
            else if (this.Contain(mx.SLV))
            {
                if (!this.Contain(mx.SE))
                {
                    if (JesmTemp)
                    {
                        round = this.GetRound((double) (jE / (1.0 + sLV)), 2);
                        sE = jE - round;
                        if (Math.Abs((double) (sLV - 0.015)) < 1E-05)
                        {
                            bill.JZ_50_15 = true;
                            sE = (jE * 0.015) / 1.05;
                            round = jE - sE;
                        }
                        if (IsCEZS)
                        {
                            sE = ((mxWare.JE - mxWare.KCE) * mxWare.SLV) / (1.0 + mxWare.SLV);
                            round = jE - sE;
                            if (round >= 0.0)
                            {
                                if (round < mxWare.KCE)
                                {
                                    errorAnalyse.AddError("不含税销售额 不能小于 扣除额" + mxWare.KCE.ToString(), DJBH, mx.XH, false);
                                    return false;
                                }
                            }
                            else if (round > mxWare.KCE)
                            {
                                errorAnalyse.AddError("负数单据，不含税销售额 不能大于 扣除额" + mxWare.KCE.ToString(), DJBH, mx.XH, false);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        double num16 = this.GetRound(jE, 2);
                        double num17 = this.GetRound(sLV, 2);
                        round = jE;
                        sE = this.GetRound((double) (num16 * num17), 2);
                        if (sLV == 0.015)
                        {
                            bill.JZ_50_15 = true;
                            round = jE;
                            sE = (jE * 0.015) / 1.0350000000000001;
                        }
                        if (IsCEZS)
                        {
                            sE = (mxWare.JE - mxWare.KCE) * mxWare.SLV;
                            if (round >= 0.0)
                            {
                                if (round < mxWare.KCE)
                                {
                                    errorAnalyse.AddError("不含税销售额 不能小于 扣除额" + mxWare.KCE.ToString(), DJBH, mx.XH, false);
                                    return false;
                                }
                            }
                            else if (round > mxWare.KCE)
                            {
                                errorAnalyse.AddError("负数单据，不含税销售额 不能大于 扣除额" + mxWare.KCE.ToString(), DJBH, mx.XH, false);
                                return false;
                            }
                        }
                    }
                }
                else if (JesmTemp)
                {
                    if (Math.Abs((double) (sLV - 0.015)) >= 1E-05)
                    {
                        if (!IsCEZS)
                        {
                            num3 = jE - sE;
                            if (this.GetRound(Math.Abs((double) (this.GetRound((double) (num3 * sLV), 2) - sE)), 2) > num)
                            {
                                if (this.GetRound(Math.Abs((double) (this.GetRound((double) (jE * sLV), 2) - sE)), 2) > num)
                                {
                                    bill.DJZT = "N";
                                    round = jE - sE;
                                    errorAnalyse.AddError("税额误差超过允许范围" + num.ToString(), DJBH, mx.XH, true);
                                    return true;
                                }
                                flag3 = false;
                                round = jE;
                            }
                            else
                            {
                                round = jE - sE;
                            }
                        }
                        else
                        {
                            num3 = (jE - sE) - mxWare.KCE;
                            if (this.GetRound(Math.Abs((double) (this.GetRound((double) (num3 * sLV), 2) - sE)), 2) <= num)
                            {
                                round = jE - sE;
                            }
                            else
                            {
                                num3 = jE - mxWare.KCE;
                                if (this.GetRound(Math.Abs((double) (this.GetRound((double) (num3 * sLV), 2) - sE)), 2) <= num)
                                {
                                    flag3 = false;
                                    round = jE;
                                }
                                else
                                {
                                    bill.DJZT = "N";
                                    round = jE - sE;
                                    errorAnalyse.AddError("差额征税，税额误差超过允许范围" + num.ToString(), DJBH, mx.XH, true);
                                    return true;
                                }
                            }
                            if (round >= 0.0)
                            {
                                if (round < mxWare.KCE)
                                {
                                    errorAnalyse.AddError("不含税销售额 不能小于 扣除额" + mxWare.KCE.ToString(), DJBH, mx.XH, false);
                                    return false;
                                }
                            }
                            else if (round > mxWare.KCE)
                            {
                                errorAnalyse.AddError("负数单据，不含税销售额 不能大于 扣除额" + mxWare.KCE.ToString(), DJBH, mx.XH, false);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        bill.JZ_50_15 = true;
                        if (this.GetRound(Math.Abs((double) ((this.GetRound((double) (jE * 0.015), 2) / 1.05) - sE)), 2) > num)
                        {
                            if (this.GetRound(Math.Abs((double) ((this.GetRound((double) ((jE + sE) * 0.015), 2) / 1.05) - sE)), 2) > num)
                            {
                                bill.DJZT = "N";
                                round = jE - sE;
                                errorAnalyse.AddError("税额误差超过允许范围" + num.ToString(), DJBH, mx.XH, true);
                                return true;
                            }
                            flag3 = false;
                            round = jE;
                        }
                        else
                        {
                            round = jE - sE;
                        }
                    }
                }
                else if (Math.Abs((double) (sLV - 0.015)) < 1E-05)
                {
                    bill.JZ_50_15 = true;
                    if (this.GetRound(Math.Abs((double) ((this.GetRound((double) ((jE + sE) * 0.015), 2) / 1.05) - sE)), 2) > num)
                    {
                        if (this.GetRound(Math.Abs((double) ((this.GetRound((double) (jE * 0.015), 2) / 1.05) - sE)), 2) > num)
                        {
                            bill.DJZT = "N";
                            errorAnalyse.AddError("税额误差超过允许范围" + num.ToString(), DJBH, mx.XH, true);
                            return true;
                        }
                        flag3 = true;
                        round = jE - sE;
                    }
                    else
                    {
                        round = jE;
                    }
                }
                else if (IsCEZS)
                {
                    num3 = jE - mxWare.KCE;
                    if (this.GetRound(Math.Abs((double) (this.GetRound((double) (num3 * sLV), 2) - sE)), 2) <= num)
                    {
                        round = jE;
                    }
                    else
                    {
                        num3 = (jE - sE) - mxWare.KCE;
                        if (this.GetRound(Math.Abs((double) (this.GetRound((double) (num3 * sLV), 2) - sE)), 2) <= num)
                        {
                            flag3 = false;
                            round = jE - sE;
                        }
                        else
                        {
                            bill.DJZT = "N";
                            round = jE;
                            errorAnalyse.AddError("差额征税，税额误差超过允许范围" + num.ToString(), DJBH, mx.XH, true);
                            return true;
                        }
                    }
                    if (round >= 0.0)
                    {
                        if (round < mxWare.KCE)
                        {
                            errorAnalyse.AddError("不含税销售额 不能小于 扣除额" + mxWare.KCE.ToString(), DJBH, mx.XH, false);
                            return false;
                        }
                    }
                    else if (round > mxWare.KCE)
                    {
                        errorAnalyse.AddError("负数单据，不含税销售额 不能大于 扣除额" + mxWare.KCE.ToString(), DJBH, mx.XH, false);
                        return false;
                    }
                }
                else
                {
                    round = jE;
                    if (this.GetRound(Math.Abs((double) (this.GetRound((double) (round * sLV), 2) - sE)), 2) > num)
                    {
                        num3 = jE - sE;
                        if (this.GetRound(Math.Abs((double) (this.GetRound((double) (num3 * sLV), 2) - sE)), 2) > num)
                        {
                            bill.DJZT = "N";
                            errorAnalyse.AddError("税额误差超过允许范围" + num.ToString(), DJBH, mx.XH, true);
                            return true;
                        }
                        flag3 = true;
                        round = jE - sE;
                    }
                }
            }
            else
            {
                int num11;
                int num12;
                int num13;
                int count = TaxCardValue.taxCard.get_SQInfo().PZSQType.Count;
                List<double> list = new List<double>();
                if (invType == InvType.Special)
                {
                    for (num11 = 0; num11 < count; num11++)
                    {
                        if (TaxCardValue.taxCard.get_SQInfo().PZSQType[num11].invType == 0)
                        {
                            num12 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num11].TaxRate.Count;
                            num13 = 0;
                            while (num13 < num12)
                            {
                                list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num11].TaxRate[num13].Rate);
                                num13++;
                            }
                            num12 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num11].TaxRate2.Count;
                            num13 = 0;
                            while (num13 < num12)
                            {
                                double rate = TaxCardValue.taxCard.get_SQInfo().PZSQType[num11].TaxRate2[num13].Rate;
                                if (Math.Abs((double) (rate - 0.05)) >= 1E-06)
                                {
                                    list.Add(rate);
                                }
                                num13++;
                            }
                        }
                    }
                }
                else if (invType == InvType.Common)
                {
                    for (num11 = 0; num11 < count; num11++)
                    {
                        if (TaxCardValue.taxCard.get_SQInfo().PZSQType[num11].invType == 2)
                        {
                            num12 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num11].TaxRate.Count;
                            num13 = 0;
                            while (num13 < num12)
                            {
                                list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num11].TaxRate[num13].Rate);
                                num13++;
                            }
                            num12 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num11].TaxRate2.Count;
                            for (num13 = 0; num13 < num12; num13++)
                            {
                                list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num11].TaxRate2[num13].Rate);
                            }
                        }
                    }
                }
                num3 = this.GetRound((double) (mxWare.JE - mxWare.SE), 2);
                string str = "";
                if (list.Count > 0)
                {
                    for (num11 = 0; num11 < list.Count; num11++)
                    {
                        num4 = Convert.ToDouble(list[num11]);
                        if (Math.Abs((double) (num4 - 0.015)) < 1E-05)
                        {
                            num9 = jE;
                            if (!JesmTemp)
                            {
                                num9 = jE + sE;
                            }
                            double num15 = (num9 * 0.015) / 1.05;
                            if (this.GetRound(Math.Abs((double) (num15 - sE)), 2) > num)
                            {
                                continue;
                            }
                            bill.JZ_50_15 = true;
                            round = jE;
                            if (JesmTemp)
                            {
                                round = jE - sE;
                            }
                            str = "1";
                            break;
                        }
                        if (this.GetRound(Math.Abs((double) (this.GetRound((double) (num3 * num4), 2) - mxWare.SE)), 2) <= num)
                        {
                            flag3 = true;
                            round = mxWare.JE - mxWare.SE;
                            str = "1";
                            break;
                        }
                        if (this.GetRound(Math.Abs((double) (this.GetRound((double) (jE * num4), 2) - sE)), 2) <= num)
                        {
                            flag3 = false;
                            round = jE;
                            str = "1";
                            break;
                        }
                    }
                    if (str == "1")
                    {
                        sLV = num4;
                        mx.SLV = num4.ToString();
                    }
                    else
                    {
                        errorAnalyse.AddError("税率有错误", DJBH, mx.XH, true);
                        return true;
                    }
                }
            }
            mxWare.JE = this.GetRound(round, 2);
            mxWare.SE = this.GetRound(sE, 2);
            mxWare.SLV = sLV;
            JesmTemp = flag2;
            return true;
        }

        private bool CalcZheKou(bool JesmTemp, bool HYSYsign, InvType invType, GoodsImportTemp mx, Goods mxWare, ErrorResolver errorAnalyse)
        {
            double num2;
            double num = 0.06;
            double num4 = TodoubleValidate(mx.ZKJE);
            double num5 = TodoubleValidate(mx.ZKL);
            double num6 = TodoubleValidate(mx.ZKSE);
            num4 = Math.Round(num4, 2);
            num6 = Math.Round(num6, 2);
            double jE = mxWare.JE;
            double sE = mxWare.SE;
            double sLV = mxWare.SLV;
            if (mx.ZKSE == "ImportError")
            {
                errorAnalyse.AddError("折扣税额有错误", mx.XSDJBH, mx.XH, false);
                return false;
            }
            if (((num4 == 0.0) && (num5 == 0.0)) && (num6 == 0.0))
            {
                mx.DJHXZ = "0";
                return true;
            }
            if (JesmTemp)
            {
                double round = num4;
                round = this.GetRound((double) (num4 / (1.0 + sLV)), 2);
                if (Math.Abs((double) (sLV - 0.015)) < 1E-05)
                {
                    round = (num4 * 69.0) / 70.0;
                }
                if ((Math.Abs((double) (sLV - 0.05)) < 1E-05) && HYSYsign)
                {
                    round = num4 * 0.95;
                }
                num4 = round;
                if (!(num4 == 0.0))
                {
                    JesmTemp = false;
                }
            }
            if (num5 < 0.0)
            {
                errorAnalyse.AddError("折扣率有错误", mx.XSDJBH, mx.XH, false);
                return false;
            }
            if ((num4 != 0.0) && (num5 != 0.0))
            {
                if (JesmTemp)
                {
                    if ((num4 * (jE + sE)) > 0.0)
                    {
                        if (this.GetRound(Math.Abs((double) ((jE * num5) - num4)), 2) > num)
                        {
                            errorAnalyse.AddError("折扣率有错误", mx.XSDJBH, mx.XH, false);
                            return false;
                        }
                    }
                    else
                    {
                        num2 = this.GetRound(Math.Abs((double) (((jE + sE) * num5) + num4)), 2);
                        if (this.GetRound(Math.Abs((double) ((jE * num5) + num4)), 2) > num)
                        {
                            errorAnalyse.AddError("折扣率有错误", mx.XSDJBH, mx.XH, false);
                            return false;
                        }
                    }
                }
                else if ((num4 * jE) > 0.0)
                {
                    if (this.GetRound(Math.Abs((double) ((jE * num5) - num4)), 2) > num)
                    {
                        errorAnalyse.AddError("折扣率有错误", mx.XSDJBH, mx.XH, false);
                        return false;
                    }
                }
                else if (this.GetRound(Math.Abs((double) ((jE * num5) + num4)), 2) > num)
                {
                    errorAnalyse.AddError("折扣率有错误", mx.XSDJBH, mx.XH, false);
                    return false;
                }
            }
            if ((num4 * num6) < 0.0)
            {
                errorAnalyse.AddError("折扣金额和折扣税额不同号", mx.XSDJBH, mx.XH, false);
                return false;
            }
            if (num4 != 0.0)
            {
                if (!this.CalcZKJE_ZKSE(JesmTemp, HYSYsign, invType, mx, mxWare, ref num4, ref num6))
                {
                    mxWare.ZKJE = num4;
                    mxWare.ZKSE = num6;
                    errorAnalyse.AddError("折扣税额错误", mx.XSDJBH, mx.XH, false);
                    return false;
                }
                if (jE == 0.0)
                {
                    num5 = 0.0;
                }
                else
                {
                    double num3;
                    if (JesmTemp)
                    {
                        double num11 = TodoubleValidate(mx.ZKJE);
                        num3 = this.GetRound((double) ((num11 / (1.0 + sLV)) / jE), 5);
                        if (Math.Abs((double) (sLV - 0.015)) < 1E-05)
                        {
                            num3 = ((num11 * 69.0) / 70.0) / jE;
                        }
                    }
                    else
                    {
                        num3 = this.GetRound((double) (num4 / jE), 5);
                    }
                    num3 = Math.Abs(num3);
                    if (!this.Contain(mx.ZKL))
                    {
                        num5 = num3;
                    }
                    else if ((num5 == 0.0) && !this.Contain(mx.ZKJE))
                    {
                        errorAnalyse.AddError("折扣率错误", mx.XSDJBH, mx.XH, false);
                        return false;
                    }
                    if (num5 > 100.0000001)
                    {
                        errorAnalyse.AddError("折扣率错误", mx.XSDJBH, mx.XH, false);
                        return false;
                    }
                    if (Math.Abs((double) (this.GetRound(num5, 5) - num3)) > 1E-05)
                    {
                        if (JesmTemp)
                        {
                            num2 = Math.Abs((double) (Math.Abs((double) (num4 + num6)) - Math.Abs(this.GetRound((double) (num5 * (jE + sE)), 2))));
                        }
                        else
                        {
                            num2 = Math.Abs((double) (Math.Abs(num4) - Math.Abs(this.GetRound((double) (num5 * jE), 2))));
                        }
                        if (num2 > 1E-05)
                        {
                            errorAnalyse.AddError("折扣率错误", mx.XSDJBH, mx.XH, false);
                            return false;
                        }
                    }
                }
            }
            else if (num5 != 0.0)
            {
                if (num6 != 0.0)
                {
                    if (num5 < 0.0)
                    {
                        errorAnalyse.AddError("折扣率错误", mx.XSDJBH, mx.XH, false);
                        return false;
                    }
                    if (this.GetRound((double) ((sE * num5) - Math.Abs(num6)), 2) > num)
                    {
                        errorAnalyse.AddError("折扣税额错误", mx.XSDJBH, mx.XH, false);
                        return false;
                    }
                }
                this.CalcZKJE_ZKSE_x(JesmTemp, HYSYsign, invType, mx, mxWare, ref num4, ref num6);
                mxWare.ZKJE = num4;
                mxWare.ZKSE = num6;
            }
            else if (num6 != 0.0)
            {
                if ((num6 * sE) > 0.0)
                {
                    num5 = this.GetRound((double) (num6 / sE), 5);
                    num4 = Finacial.Div(num6, sLV, 2);
                    if (Math.Abs((double) (sLV - 0.015)) < 1E-05)
                    {
                        num4 = Finacial.Mul(num6, 69.0, 2);
                    }
                    num5 = Finacial.Div(num4, jE, 2);
                }
                else
                {
                    num5 = (sE == 0.0) ? 0.0 : -this.GetRound((double) (num6 / sE), 5);
                    num4 = Finacial.Div(num6, sLV, 2);
                    if (Math.Abs((double) (sLV - 0.015)) < 1E-05)
                    {
                        num4 = Finacial.Mul(num6, 69.0, 2);
                    }
                    num5 = Math.Abs(Finacial.Div(num4, jE, 2));
                }
                if (jE >= 0.0)
                {
                    num4 = Math.Abs(num4);
                }
                else
                {
                    num4 = -1.0 * Math.Abs(num4);
                }
            }
            num4 = Math.Abs(num4);
            num6 = Math.Abs(num6);
            if (jE >= 0.0)
            {
                if (num4 > jE)
                {
                    errorAnalyse.AddError("折扣金额大于金额", mx.XSDJBH, mx.XH, false);
                    return false;
                }
            }
            else
            {
                if ((num6 != 0.0) && (num4 == 0.0))
                {
                    num4 = this.GetRound((double) (num6 / sLV), 2);
                }
                if (num4 > -jE)
                {
                    errorAnalyse.AddError("折扣金额大于金额", mx.XSDJBH, mx.XH, false);
                    return false;
                }
            }
            if (mxWare.JE < 0.0)
            {
                mxWare.JE = this.GetRound((double) (mxWare.JE + Math.Abs(num4)), 2);
                mxWare.SE = this.GetRound((double) (mxWare.SE + Math.Abs(num6)), 2);
                if ((mxWare.SL != 0.0) && (mxWare.DJ != 0.0))
                {
                    mxWare.DJ = mxWare.JE / mxWare.SL;
                    mx.HSJBZ = "0";
                    if (HYSYsign && (Math.Abs((double) (mxWare.SLV - 0.05)) < 1E-05))
                    {
                        mxWare.DJ = (mxWare.JE + mxWare.SE) / mxWare.SL;
                        mx.HSJBZ = "1";
                    }
                }
                num4 = 0.0;
                num5 = 0.0;
                num6 = 0.0;
                mxWare.ZKJE = 0.0;
                mxWare.ZKL = 0.0;
                mxWare.ZKSE = 0.0;
            }
            else
            {
                mxWare.ZKJE = num4;
                mxWare.ZKL = num5;
                mxWare.ZKSE = num6;
            }
            mxWare.DJHXZ = ((num4 == 0.0) || (num5 == 0.0)) ? 0 : 3;
            return true;
        }

        private bool CalcZKJE_ZKSE(bool JesmTemp, bool hysy_x, InvType Fplx, GoodsImportTemp mx, Goods mxWare, ref double ZKJE, ref double ZKSE)
        {
            double num = 0.06;
            double num2 = Math.Abs(ZKJE);
            double sLV = mxWare.SLV;
            if (JesmTemp)
            {
                if (((sLV == 0.05) && hysy_x) && (Fplx == InvType.Special))
                {
                    if (!(this.Contain(mx.ZKSE) && !(mx.ZKSE == "0")))
                    {
                        ZKJE = this.GetRound((double) (num2 * 0.95), 2);
                        ZKSE = this.GetRound((double) (num2 - ZKJE), 2);
                        return true;
                    }
                    if (this.GetRound(Math.Abs((double) (this.GetRound((double) (num2 * 0.05), 2) - ZKSE)), 2) <= num)
                    {
                        ZKJE = this.GetRound((double) (num2 - ZKSE), 5);
                        ZKSE = this.GetRound(ZKSE, 5);
                        return true;
                    }
                    return false;
                }
                if (Math.Abs((double) (sLV - 0.015)) < 1E-06)
                {
                    if (!(this.Contain(mx.ZKSE) && !(mx.ZKSE == "0")))
                    {
                        ZKSE = this.GetRound((double) ((num2 * 0.015) / 1.05), 2);
                        ZKJE = this.GetRound((double) (num2 - ZKSE), 2);
                        return true;
                    }
                    if (this.GetRound(Math.Abs((double) (this.GetRound((double) ((num2 * 0.015) / 1.05), 2) - ZKSE)), 2) <= num)
                    {
                        ZKSE = this.GetRound(ZKSE, 5);
                        ZKJE = this.GetRound((double) (num2 - ZKSE), 5);
                        return true;
                    }
                    return false;
                }
                if (this.Contain(mx.ZKSE) && !(mx.ZKSE == "0"))
                {
                    if (this.GetRound(Math.Abs((double) (this.GetRound((double) ((mxWare.ZKJE - ZKSE) * sLV), 2) - ZKSE)), 2) > num)
                    {
                        if (this.GetRound(Math.Abs((double) (this.GetRound((double) (mxWare.ZKJE * sLV), 2) - ZKSE)), 2) > num)
                        {
                            ZKJE = this.GetRound((double) (mxWare.ZKJE - ZKSE), 5);
                            return false;
                        }
                        JesmTemp = false;
                        ZKJE = this.GetRound(mxWare.ZKJE, 5);
                    }
                    else
                    {
                        ZKJE = this.GetRound((double) (mxWare.ZKJE - ZKSE), 5);
                    }
                }
                else
                {
                    ZKSE = this.GetRound((double) (mxWare.ZKJE - ZKJE), 2);
                }
            }
            else
            {
                if (((sLV == 0.05) && hysy_x) && (Fplx == InvType.Special))
                {
                    if (!(this.Contain(mx.ZKSE) && !(mx.ZKSE == "0")))
                    {
                        ZKJE = this.GetRound(num2, 5);
                        ZKSE = this.GetRound((double) (num2 / 19.0), 5);
                        return true;
                    }
                    if (this.GetRound(Math.Abs((double) (this.GetRound((double) (num2 / 19.0), 2) - ZKSE)), 2) <= num)
                    {
                        ZKJE = this.GetRound(num2, 5);
                        ZKSE = this.GetRound(ZKSE, 5);
                        return true;
                    }
                    return false;
                }
                if (Math.Abs((double) (sLV - 0.015)) < 1E-06)
                {
                    if (!(this.Contain(mx.ZKSE) && !(mx.ZKSE == "0")))
                    {
                        ZKSE = this.GetRound((double) (num2 / 69.0), 2);
                        ZKJE = this.GetRound(num2, 2);
                        return true;
                    }
                    if (this.GetRound(Math.Abs((double) (this.GetRound((double) (num2 / 69.0), 2) - ZKSE)), 2) <= num)
                    {
                        ZKSE = this.GetRound(ZKSE, 5);
                        ZKJE = this.GetRound(num2, 5);
                        return true;
                    }
                    return false;
                }
                if (!(this.Contain(mx.ZKSE) && !(mx.ZKSE == "0")))
                {
                    ZKSE = this.GetRound((double) (ZKJE * sLV), 5);
                }
                else if (this.GetRound(Math.Abs((double) (this.GetRound((double) (ZKJE * sLV), 2) - ZKSE)), 2) > num)
                {
                    if (this.GetRound(Math.Abs((double) (this.GetRound((double) ((num2 - ZKSE) * sLV), 2) - ZKSE)), 2) > num)
                    {
                        return false;
                    }
                    JesmTemp = true;
                    ZKJE = this.GetRound((double) (num2 - ZKSE), 5);
                }
            }
            ZKJE = this.GetRound(ZKJE, 5);
            ZKSE = this.GetRound(ZKSE, 5);
            return true;
        }

        private bool CalcZKJE_ZKSE_x(bool JesmTemp, bool hysy_x, InvType Fplx, GoodsImportTemp mx, Goods mxWare, ref double ZKJE, ref double ZKSE)
        {
            double round;
            double sLV = mxWare.SLV;
            double jE = mxWare.JE;
            double zKL = mxWare.ZKL;
            double sE = mxWare.SE;
            double num = Math.Abs(ZKJE);
            if (JesmTemp)
            {
                if (((sLV == 0.05) && hysy_x) && (Fplx == InvType.Special))
                {
                    round = this.GetRound((double) (jE * zKL), 5);
                    ZKJE = round;
                    if (this.GetRound(ZKSE, 8) == 0.0)
                    {
                        ZKSE = this.GetRound((double) (sE * zKL), 5);
                    }
                    ZKJE = this.GetRound(ZKJE, 5);
                    ZKSE = this.GetRound(ZKSE, 5);
                    return true;
                }
                if (jE == 0.0)
                {
                    ZKJE = this.GetRound((double) ((sE / sLV) * zKL), 5);
                    if (this.GetRound(ZKSE, 8) == 0.0)
                    {
                        ZKSE = this.GetRound((double) (sE * zKL), 5);
                        ZKSE = this.GetRound((double) (ZKJE * sLV), 5);
                        if (Math.Abs((double) (sLV - 0.015)) < 1E-05)
                        {
                            ZKSE = this.GetRound((double) ((ZKJE * 1.0) / 69.0), 5);
                        }
                    }
                }
                else
                {
                    round = this.GetRound((double) (jE * zKL), 5);
                    ZKJE = round;
                    if (this.GetRound(ZKSE, 8) == 0.0)
                    {
                        ZKSE = this.GetRound((double) (sE * zKL), 5);
                        ZKSE = this.GetRound((double) (ZKJE * sLV), 5);
                        if (Math.Abs((double) (sLV - 0.015)) < 1E-05)
                        {
                            ZKSE = this.GetRound((double) ((ZKJE * 1.0) / 69.0), 5);
                        }
                    }
                }
            }
            else if (((sLV == 0.05) && hysy_x) && (Fplx == InvType.Special))
            {
                round = this.GetRound((double) (jE * zKL), 5);
                if (this.GetRound(ZKSE, 8) == 0.0)
                {
                    ZKSE = this.GetRound((double) (round / 19.0), 5);
                }
                ZKJE = this.GetRound(round, 5);
                ZKSE = this.GetRound(ZKSE, 5);
            }
            else
            {
                ZKJE = this.GetRound((double) (jE * zKL), 5);
                if (this.GetRound(ZKSE, 8) == 0.0)
                {
                    ZKSE = this.GetRound((double) (sE * zKL), 5);
                    ZKSE = this.GetRound((double) (ZKJE * sLV), 5);
                    if (Math.Abs((double) (sLV - 0.015)) < 1E-05)
                    {
                        ZKSE = this.GetRound((double) ((ZKJE * 1.0) / 69.0), 5);
                    }
                }
            }
            ZKJE = this.GetRound(ZKJE, 5);
            ZKSE = this.GetRound(ZKSE, 5);
            return true;
        }

        private bool CheckCEZS(SaleBillImportTemp ImportBill)
        {
            foreach (GoodsImportTemp temp in ImportBill.ListGoods)
            {
                if ((temp.KCE.Trim() != "") && (temp.KCE != null))
                {
                    double result = 1.0;
                    if (double.TryParse(temp.KCE, out result) && (result != 0.0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckDanJiaZhongLei(ref string DanJiaZhongLei, ref bool FangShiChuanRu, ref bool DanJiaHanShui)
        {
            bool flag = true;
            if ((((DanJiaZhongLei == "1") || (DanJiaZhongLei == "Y")) || (DanJiaZhongLei == "T")) || (DanJiaZhongLei == "是"))
            {
                FangShiChuanRu = true;
                DanJiaHanShui = true;
            }
            else if ((((DanJiaZhongLei == "0") || (DanJiaZhongLei == "N")) || (DanJiaZhongLei == "F")) || (DanJiaZhongLei == "否"))
            {
                FangShiChuanRu = true;
                DanJiaHanShui = false;
            }
            else
            {
                DanJiaZhongLei = "";
                FangShiChuanRu = false;
                DanJiaHanShui = false;
            }
            if (DanJiaHanShui)
            {
                DanJiaZhongLei = "Y";
                return flag;
            }
            DanJiaZhongLei = "N";
            return flag;
        }

        public bool CheckMultiSlv(SaleBill saleBill)
        {
            bool flag = false;
            double sLV = -1.0;
            foreach (Goods goods in saleBill.ListGoods)
            {
                if (sLV == -1.0)
                {
                    sLV = goods.SLV;
                }
                else if (Math.Abs((double) (sLV - goods.SLV)) > 1E-07)
                {
                    flag = true;
                }
            }
            return flag;
        }

        private bool CheckSLV(InvoiceType invType, double slv)
        {
            int num2;
            int count = TaxCardValue.taxCard.get_SQInfo().PZSQType.Count;
            List<double> list = new List<double>();
            for (num2 = 0; num2 < count; num2++)
            {
                if (TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].invType == invType)
                {
                    int num3 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate.Count;
                    for (int i = 0; i < num3; i++)
                    {
                        list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate[i].Rate);
                    }
                }
            }
            if (invType == 0)
            {
                list.Add(0.05);
            }
            bool flag = false;
            for (num2 = 0; num2 < list.Count; num2++)
            {
                if (slv == list[num2])
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                return false;
            }
            return true;
        }

        public bool CheckSlvIsYHZCSLV(string slv, string XSYHSM)
        {
            double result = -1.0;
            if (slv.Contains("%"))
            {
                if (!double.TryParse(slv.Replace("%", ""), out result))
                {
                    return false;
                }
                result /= 100.0;
            }
            else if (!double.TryParse(slv, out result))
            {
                return false;
            }
            List<double> list = new List<double>();
            string[] strArray = XSYHSM.Split(new char[] { '、' });
            foreach (string str in strArray)
            {
                List<double> list2 = new SaleBillDAL().GET_YHZCSLV_BY_YHZCMC(str);
                foreach (double num2 in list2)
                {
                    bool flag3 = false;
                    foreach (double num3 in list)
                    {
                        if (num3 == num2)
                        {
                            flag3 = true;
                        }
                    }
                    if (!flag3)
                    {
                        list.Add(num2);
                    }
                }
            }
            bool flag4 = false;
            if (list.Count == 0)
            {
                flag4 = true;
            }
            else
            {
                foreach (double num4 in list)
                {
                    if (num4 == result)
                    {
                        return true;
                    }
                }
            }
            return flag4;
        }

        private bool Contain(string field)
        {
            return !field.StartsWith("Import");
        }

        public string DisCount(SaleBill bill, int selectIndex, int zkhs, double zkl, double JEr, double SEr)
        {
            Goods goods = bill.ListGoods[selectIndex];
            int num = (selectIndex - zkhs) + 1;
            for (int i = num; i < (num + zkhs); i++)
            {
                bill.ListGoods[i].DJHXZ = 3;
            }
            Goods item = new Goods {
                XSDJBH = bill.BH,
                XH = selectIndex + 2
            };
            int num3 = (num + zkhs) - 1;
            item.SPSM = bill.ListGoods[num3].SPSM;
            item.Reserve = bill.ListGoods[num3].Reserve;
            item.JE = (double) this.GetRound((decimal) (-1.0 * JEr), 2);
            item.SPMC = CommonTool.GetDisCountMC(zkl, zkhs);
            item.DJHXZ = 4;
            item.SE = (double) this.GetRound((decimal) (-1.0 * SEr), 2);
            item.SLV = bill.ListGoods[num].SLV;
            bill.ListGoods.Insert(selectIndex + 1, item);
            return "0";
        }

        private DataTable ExcelLoad(string file1, string file2, InvType invtype)
        {
            DataTable table2;
            try
            {
                if (invtype == InvType.Common)
                {
                    IniRead.type = "c";
                }
                else if (invtype == InvType.Special)
                {
                    IniRead.type = "s";
                }
                else if (invtype == InvType.transportation)
                {
                    IniRead.type = "f";
                }
                else if (invtype == InvType.vehiclesales)
                {
                    IniRead.type = "j";
                }
                string privateProfileString = IniRead.GetPrivateProfileString("FieldCon", "Invtype");
                if ((invtype == InvType.Common) || (invtype == InvType.Special))
                {
                    string str2 = invtype.ToString();
                    if ((privateProfileString.CompareTo("Common") != 0) && (privateProfileString.CompareTo("Special") != 0))
                    {
                        throw new CustomException("请设置单据格式");
                    }
                }
                else if (invtype.ToString().CompareTo(privateProfileString) != 0)
                {
                    throw new CustomException("请设置单据格式");
                }
                if (IniRead.GetPrivateProfileString("FieldCon", "IsSeted").CompareTo("0") == 0)
                {
                    throw new CustomException("销售单据错误,请设置单据格式");
                }
                string str4 = IniRead.GetPrivateProfileString("File", "TableInFile1");
                string str5 = IniRead.GetPrivateProfileString("File", "TableInFile2");
                string s = IniRead.GetPrivateProfileString("FieldCon", "FileNumber");
                int result = 1;
                int.TryParse(s, out result);
                List<ExcelMappingItem.Relation> yingShe = new List<ExcelMappingItem.Relation>();
                DataTable table = WenBenItem.Items();
                foreach (DataRow row in table.Rows)
                {
                    ExcelMappingItem.Relation item = new ExcelMappingItem.Relation();
                    string key = row["key"].ToString();
                    if (key == "KouChuE")
                    {
                        string str8 = key;
                    }
                    string str9 = IniRead.GetPrivateProfileString("FieldCon", key);
                    item.Key = row["key"].ToString();
                    int num2 = 0;
                    int.TryParse(str9.Substring(0, str9.IndexOf('.')), out num2);
                    item.TableFlag = num2;
                    num2 = 0;
                    int.TryParse(str9.Substring(str9.LastIndexOf('.') + 1), out num2);
                    item.ColumnName = num2;
                    yingShe.Add(item);
                }
                string defaultFuHeRen = IniRead.GetPrivateProfileString("FieldCon", "DefaultFuHeRen");
                string defaultShouKuanRen = IniRead.GetPrivateProfileString("FieldCon", "DefaultShouKuanRen");
                string defaultShuiLv = IniRead.GetPrivateProfileString("FieldCon", "DefaultShuiLv");
                int num3 = 0;
                int.TryParse(IniRead.GetPrivateProfileString("TableCon", "MainTableField"), out num3);
                int num4 = num3 - 1;
                num3 = 0;
                int.TryParse(IniRead.GetPrivateProfileString("TableCon", "AssistantTableField"), out num3);
                int num5 = num3 - 1;
                num3 = 0;
                int.TryParse(IniRead.GetPrivateProfileString("TableCon", "MainTableIgnoreRow"), out num3);
                int num6 = num3;
                num3 = 0;
                int.TryParse(IniRead.GetPrivateProfileString("TableCon", "AssistantTableIgnoreRow"), out num3);
                int subHeadline = num3;
                switch (result)
                {
                    case 1:
                        return this.GetFileData(file1, str4, num6, yingShe, defaultFuHeRen, defaultShouKuanRen, defaultShuiLv);

                    case 2:
                        return ResolverExcel.GetFileData(file1, file2, str4, str5, num6, subHeadline, num4, num5, yingShe, defaultFuHeRen, defaultShouKuanRen, defaultShuiLv);
                }
                table2 = null;
            }
            catch (Exception)
            {
                throw;
            }
            return table2;
        }

        private List<SaleBillImportTemp> FetchBillFromExcel(string File1Path, string File2Path, InvType invtype)
        {
            DataTable table = this.ExcelLoad(File1Path, File2Path, invtype);
            List<SaleBillImportTemp> billList = new List<SaleBillImportTemp>();
            foreach (DataRow row in table.Rows)
            {
                GoodsImportTemp temp2;
                string iD = Convert.ToString(row["DanJuHaoMa"]).Replace("\n", " ");
                SaleBillImportTemp item = this.GetTempXSDJ(iD, billList, invtype);
                if (item == null)
                {
                    DateTime time;
                    item = new SaleBillImportTemp();
                    if ((invtype == InvType.Common) || (invtype == InvType.Special))
                    {
                        item.BH = iD;
                        item.GoodsNum = "0";
                        item.GFMC = Convert.ToString(row["GouFangMingCheng"]).Replace("\n", " ");
                        item.GFSH = Convert.ToString(row["GouFangShuiHao"]).Replace("\n", " ");
                        item.GFDZDH = Convert.ToString(row["GouFangDiZhiDianHua"]).Replace("\n", " ");
                        item.GFYHZH = Convert.ToString(row["GouFangYinHangZhangHao"]).Replace("\n", " ");
                        item.BZ = Convert.ToString(row["BeiZhu"]).Replace("\n", " ");
                        item.FHR = Convert.ToString(row["FuHeRen"]).Replace("\n", " ");
                        item.SKR = Convert.ToString(row["ShouKuanRen"]).Replace("\n", " ");
                        item.QDHSPMC = Convert.ToString(row["QingDanHangShangPinMingCheng"]).Replace("\n", " ");
                        time = new DateTime(0x76c, 1, 1);
                        if (DateTime.TryParse(row["DanJuRiQi"].ToString().Replace("\n", " "), out time))
                        {
                            item.DJRQ = time;
                        }
                        else
                        {
                            item.DJRQ = DateTime.Now.Date;
                        }
                        item.XFYHZH = Convert.ToString(row["XiaoFangYinHangZhangHao"]).Replace("\n", " ");
                        item.XFDZDH = Convert.ToString(row["XiaoFangDiZhiDianHua"]).Replace("\n", " ");
                        item.SFZJY = CommonTool.ToStringBool(row["ShenFenZhengJiaoYan"].ToString().Replace("\n", " "));
                        string str2 = Convert.ToString(row["HaiYangShiYou"]).Replace("\n", " ");
                        item.HYSY = CommonTool.ToStringBool(str2);
                    }
                    else if (invtype == InvType.transportation)
                    {
                        item.HY_BH = iD;
                        item.GoodsNum = "0";
                        item.HY_SPFMC = Convert.ToString(row["ShouPiaoFangMC"]).Replace("\n", " ");
                        item.HY_SPFSH = Convert.ToString(row["ShouPiaoFangSH"]).Replace("\n", " ");
                        item.HY_SHRMC = Convert.ToString(row["ShouHuoRenMC"]).Replace("\n", " ");
                        item.HY_SHRSH = Convert.ToString(row["ShouHuoRenSH"]).Replace("\n", " ");
                        item.HY_FHRMC = Convert.ToString(row["FaHuoRenMC"]).Replace("\n", " ");
                        item.HY_FHRSH = Convert.ToString(row["FaHuoRenSH"]).Replace("\n", " ");
                        item.HY_SL = Convert.ToString(row["ShuiLv-HY"]).Replace("\n", " ");
                        item.HY_QYMD = Convert.ToString(row["QiYouDaoDa"]).Replace("\n", " ");
                        item.HY_CZCH = Convert.ToString(row["CheChongCheHao"]).Replace("\n", " ");
                        item.HY_CHDW = Convert.ToString(row["CheChuanDunWei"]).Replace("\n", " ");
                        item.HY_YSHWXX = Convert.ToString(row["YunShuHuoWuXX"]).Replace("\n", " ");
                        item.HY_BZ = Convert.ToString(row["BeiZhu-HY"]).Replace("\n", " ");
                        item.HY_FHR = Convert.ToString(row["FuHeRen-HY"]).Replace("\n", " ");
                        item.HY_SKR = Convert.ToString(row["ShouKuanRen-HY"]).Replace("\n", " ");
                        time = new DateTime(0x76c, 1, 1);
                        if (DateTime.TryParse(row["DanJuRiQi-HY"].ToString().Replace("\n", " "), out time))
                        {
                            item.HY_DJRQ = time;
                        }
                        else
                        {
                            item.HY_DJRQ = DateTime.Now.Date;
                        }
                    }
                    else if (invtype == InvType.vehiclesales)
                    {
                        item.JDC_BH = iD;
                        item.JDC_GHDW = Convert.ToString(row["GouHuoDanWei"]).Replace("\n", " ");
                        item.JDC_SFZ = Convert.ToString(row["ShenFenZhengHaoMa"]).Replace("\n", " ");
                        item.JDC_JE = Convert.ToString(row["JinE-JDC"]).Replace("\n", " ");
                        item.JDC_SL = Convert.ToString(row["ShuiLv-JDC"]).Replace("\n", " ");
                        item.JDC_LX = Convert.ToString(row["CheLiangLeiXing"]).Replace("\n", " ");
                        item.JDC_CPXH = Convert.ToString(row["ChangPaiXingHao"]).Replace("\n", " ");
                        item.JDC_CD = Convert.ToString(row["ChanDi"]).Replace("\n", " ");
                        item.JDC_SCCJMC = Convert.ToString(row["ShengChanChangJiaMC"]).Replace("\n", " ");
                        item.JDC_HGZH = Convert.ToString(row["HeGeZhengHao"]).Replace("\n", " ");
                        item.JDC_JKZMSH = Convert.ToString(row["JinKouZhengMingShuHao"]).Replace("\n", " ");
                        item.JDC_SJDH = Convert.ToString(row["ShangJianDanHao"]).Replace("\n", " ");
                        item.JDC_FDJH = Convert.ToString(row["FaDongJiHaoMa"]).Replace("\n", " ");
                        item.JDC_CLSBH = Convert.ToString(row["CheLiangShiBieDM"]).Replace("\n", " ");
                        item.JDC_DH = Convert.ToString(row["DianHua"]).Replace("\n", " ");
                        item.JDC_ZH = Convert.ToString(row["ZhangHao"]).Replace("\n", " ");
                        item.JDC_DZ = Convert.ToString(row["DiZhi"]).Replace("\n", " ");
                        item.JDC_KHYH = Convert.ToString(row["KaiHuYinHang"]).Replace("\n", " ");
                        item.JDC_DW = Convert.ToString(row["DunWei"]).Replace("\n", " ");
                        item.JDC_XCRS = Convert.ToString(row["XianChengRenShu"]).Replace("\n", " ");
                        time = new DateTime(0x76c, 1, 1);
                        if (DateTime.TryParse(row["DanJuRiQi-JDC"].ToString().Replace("\n", " "), out time))
                        {
                            item.JDC_DJRQ = time;
                        }
                        else
                        {
                            item.JDC_DJRQ = DateTime.Now.Date;
                        }
                        item.JDC_BZ = Convert.ToString(row["BeiZhu-JDC"]).Replace("\n", " ");
                        item.JDC_NSRSBH = Convert.ToString(row["NaShuiRenShiBieHao"]).Replace("\n", " ");
                    }
                    billList.Add(item);
                }
                if ((invtype == InvType.Common) || (invtype == InvType.Special))
                {
                    temp2 = new GoodsImportTemp {
                        XSDJBH = item.BH,
                        XH = item.ListGoods.Count + 1,
                        SPMC = Convert.ToString(row["HuoWuMingCheng"]).Replace("\n", " "),
                        JLDW = Convert.ToString(row["JiLiangDanWei"]).Replace("\n", " "),
                        GGXH = Convert.ToString(row["GuiGe"]).Replace("\n", " "),
                        SL = Convert.ToString(row["ShuLiang"]).Replace("\n", " "),
                        JE = Convert.ToString(row["BuHanShuiJinE"]).Replace("\n", " "),
                        SLV = Convert.ToString(row["ShuiLv"]).Replace("\n", " "),
                        SPSM = Convert.ToString(row["ShangPinShuiMu"]).Replace("\n", " "),
                        ZKJE = Convert.ToString(row["ZheKouJinE"]).Replace("\n", " "),
                        SE = Convert.ToString(row["ShuiE"]).Replace("\n", " "),
                        ZKSE = Convert.ToString(row["ZheKouShuiE"]).Replace("\n", " "),
                        ZKL = Convert.ToString(row["ZheKouLv"]).Replace("\n", " "),
                        DJ = Convert.ToString(row["DanJia"]).Replace("\n", " "),
                        HSJBZ = Convert.ToString(row["JiaGeFangShi"].ToString()).Replace("\n", " ")
                    };
                    item.ListGoods.Add(temp2);
                }
                else if (invtype == InvType.transportation)
                {
                    temp2 = new GoodsImportTemp {
                        XSDJBH = item.BH,
                        XH = item.ListGoods.Count + 1
                    };
                    string safeString = GetSafeData.GetSafeString(Convert.ToString(row["HuoWuMingCheng-HY"]).Replace("\n", " "), 20);
                    temp2.SPMC = safeString;
                    temp2.JE = Convert.ToString(row["JinE-HY"]).Replace("\n", " ");
                    item.ListGoods.Add(temp2);
                }
            }
            return billList;
        }

        private List<SaleBillImportTemp> FetchBillFromExcel(string File1Path, string File2Path, InvType invtype, ErrorResolver errorAnalyse)
        {
            DataView defaultView = this.ExcelLoad(File1Path, File2Path, invtype).DefaultView;
            defaultView.Sort = "DanJuHaoMa ASC";
            DataTable table = defaultView.ToTable();
            List<SaleBillImportTemp> list = new List<SaleBillImportTemp>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DateTime time;
                bool flag;
                GoodsImportTemp temp2;
                DataRow row = table.Rows[i];
                string str = Convert.ToString(row["DanJuHaoMa"]).Replace("\n", " ");
                SaleBillImportTemp item = new SaleBillImportTemp();
                if ((invtype == InvType.Common) || (invtype == InvType.Special))
                {
                    item.BH = str;
                    item.GoodsNum = "0";
                    item.GFMC = Convert.ToString(row["GouFangMingCheng"]).Replace("\n", " ");
                    item.GFSH = Convert.ToString(row["GouFangShuiHao"]).Replace("\n", " ");
                    item.GFDZDH = Convert.ToString(row["GouFangDiZhiDianHua"]).Replace("\n", " ");
                    item.GFYHZH = Convert.ToString(row["GouFangYinHangZhangHao"]).Replace("\n", " ");
                    item.BZ = Convert.ToString(row["BeiZhu"]).Replace("\n", " ");
                    item.FHR = Convert.ToString(row["FuHeRen"]).Replace("\n", " ");
                    item.SKR = Convert.ToString(row["ShouKuanRen"]).Replace("\n", " ");
                    item.QDHSPMC = Convert.ToString(row["QingDanHangShangPinMingCheng"]).Replace("\n", " ");
                    time = new DateTime(0x76c, 1, 1);
                    if (DateTime.TryParse(row["DanJuRiQi"].ToString().Replace("\n", " "), out time))
                    {
                        item.DJRQ = time;
                    }
                    else
                    {
                        item.DJRQ = DateTime.Now.Date;
                    }
                    item.XFYHZH = Convert.ToString(row["XiaoFangYinHangZhangHao"]).Replace("\n", " ");
                    item.XFDZDH = Convert.ToString(row["XiaoFangDiZhiDianHua"]).Replace("\n", " ");
                    item.SFZJY = CommonTool.ToStringBool(row["ShenFenZhengJiaoYan"].ToString().Replace("\n", " "));
                    string str2 = Convert.ToString(row["HaiYangShiYou"]).Replace("\n", " ");
                    item.HYSY = CommonTool.ToStringBool(str2);
                    flag = false;
                    do
                    {
                        if (flag)
                        {
                            i++;
                            flag = false;
                            row = table.Rows[i];
                        }
                        temp2 = new GoodsImportTemp {
                            XSDJBH = item.BH,
                            XH = item.ListGoods.Count + 1,
                            SPMC = Convert.ToString(row["HuoWuMingCheng"]).Replace("\n", " "),
                            JLDW = Convert.ToString(row["JiLiangDanWei"]).Replace("\n", " "),
                            GGXH = Convert.ToString(row["GuiGe"]).Replace("\n", " "),
                            SL = Convert.ToString(row["ShuLiang"]).Replace("\n", " "),
                            JE = Convert.ToString(row["BuHanShuiJinE"]).Replace("\n", " "),
                            SLV = Convert.ToString(row["ShuiLv"]).Replace("\n", " "),
                            SPSM = Convert.ToString(row["ShangPinShuiMu"]).Replace("\n", " "),
                            ZKJE = Convert.ToString(row["ZheKouJinE"]).Replace("\n", " "),
                            SE = Convert.ToString(row["ShuiE"]).Replace("\n", " "),
                            ZKSE = Convert.ToString(row["ZheKouShuiE"]).Replace("\n", " "),
                            ZKL = Convert.ToString(row["ZheKouLv"]).Replace("\n", " "),
                            DJ = Convert.ToString(row["DanJia"]).Replace("\n", " "),
                            HSJBZ = Convert.ToString(row["JiaGeFangShi"].ToString()).Replace("\n", " ")
                        };
                        if (CommonTool.isCEZS())
                        {
                            temp2.KCE = Convert.ToString(row["KouChuE"].ToString()).Replace("\n", " ");
                        }
                        else
                        {
                            temp2.KCE = "0";
                        }
                        item.ListGoods.Add(temp2);
                        if ((i + 1) < table.Rows.Count)
                        {
                            if (table.Rows[i + 1]["DanJuHaoMa"].ToString().Trim() == str.Trim())
                            {
                                flag = true;
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    while (flag);
                }
                else if (invtype == InvType.transportation)
                {
                    item.HY_BH = str;
                    item.GoodsNum = "0";
                    item.HY_SPFMC = Convert.ToString(row["ShouPiaoFangMC"]).Replace("\n", " ");
                    item.HY_SPFSH = Convert.ToString(row["ShouPiaoFangSH"]).Replace("\n", " ");
                    item.HY_SHRMC = Convert.ToString(row["ShouHuoRenMC"]).Replace("\n", " ");
                    item.HY_SHRSH = Convert.ToString(row["ShouHuoRenSH"]).Replace("\n", " ");
                    item.HY_FHRMC = Convert.ToString(row["FaHuoRenMC"]).Replace("\n", " ");
                    item.HY_FHRSH = Convert.ToString(row["FaHuoRenSH"]).Replace("\n", " ");
                    item.HY_SL = Convert.ToString(row["ShuiLv-HY"]).Replace("\n", " ");
                    item.HY_QYMD = Convert.ToString(row["QiYouDaoDa"]).Replace("\n", " ");
                    item.HY_CZCH = Convert.ToString(row["CheChongCheHao"]).Replace("\n", " ");
                    item.HY_CHDW = Convert.ToString(row["CheChuanDunWei"]).Replace("\n", " ");
                    item.HY_YSHWXX = Convert.ToString(row["YunShuHuoWuXX"]).Replace("\n", " ");
                    item.HY_BZ = Convert.ToString(row["BeiZhu-HY"]).Replace("\n", " ");
                    item.HY_FHR = Convert.ToString(row["FuHeRen-HY"]).Replace("\n", " ");
                    item.HY_SKR = Convert.ToString(row["ShouKuanRen-HY"]).Replace("\n", " ");
                    time = new DateTime(0x76c, 1, 1);
                    if (DateTime.TryParse(row["DanJuRiQi-HY"].ToString().Replace("\n", " "), out time))
                    {
                        item.HY_DJRQ = time;
                    }
                    else
                    {
                        item.HY_DJRQ = DateTime.Now.Date;
                    }
                    flag = false;
                    do
                    {
                        if (flag)
                        {
                            i++;
                            flag = false;
                            row = table.Rows[i];
                        }
                        temp2 = new GoodsImportTemp {
                            XSDJBH = item.BH,
                            XH = item.ListGoods.Count + 1
                        };
                        string safeString = GetSafeData.GetSafeString(Convert.ToString(row["HuoWuMingCheng-HY"]).Replace("\n", " "), 20);
                        temp2.SPMC = safeString;
                        temp2.JE = Convert.ToString(row["JinE-HY"]).Replace("\n", " ");
                        item.ListGoods.Add(temp2);
                        if ((i + 1) < table.Rows.Count)
                        {
                            if (table.Rows[i + 1]["DanJuHaoMa"].ToString().Trim() == str.Trim())
                            {
                                flag = true;
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    while (flag);
                }
                else if (invtype == InvType.vehiclesales)
                {
                    item.JDC_BH = str;
                    item.JDC_GHDW = Convert.ToString(row["GouHuoDanWei"]).Replace("\n", " ");
                    item.JDC_SFZ = Convert.ToString(row["ShenFenZhengHaoMa"]).Replace("\n", " ");
                    item.JDC_JE = Convert.ToString(row["JinE-JDC"]).Replace("\n", " ");
                    item.JDC_SL = Convert.ToString(row["ShuiLv-JDC"]).Replace("\n", " ");
                    item.JDC_LX = Convert.ToString(row["CheLiangLeiXing"]).Replace("\n", " ");
                    item.JDC_CPXH = Convert.ToString(row["ChangPaiXingHao"]).Replace("\n", " ");
                    item.JDC_CD = Convert.ToString(row["ChanDi"]).Replace("\n", " ");
                    item.JDC_SCCJMC = Convert.ToString(row["ShengChanChangJiaMC"]).Replace("\n", " ");
                    item.JDC_HGZH = Convert.ToString(row["HeGeZhengHao"]).Replace("\n", " ");
                    item.JDC_JKZMSH = Convert.ToString(row["JinKouZhengMingShuHao"]).Replace("\n", " ");
                    item.JDC_SJDH = Convert.ToString(row["ShangJianDanHao"]).Replace("\n", " ");
                    item.JDC_FDJH = Convert.ToString(row["FaDongJiHaoMa"]).Replace("\n", " ");
                    item.JDC_CLSBH = Convert.ToString(row["CheLiangShiBieDM"]).Replace("\n", " ");
                    item.JDC_DH = Convert.ToString(row["DianHua"]).Replace("\n", " ");
                    item.JDC_ZH = Convert.ToString(row["ZhangHao"]).Replace("\n", " ");
                    item.JDC_DZ = Convert.ToString(row["DiZhi"]).Replace("\n", " ");
                    item.JDC_KHYH = Convert.ToString(row["KaiHuYinHang"]).Replace("\n", " ");
                    item.JDC_DW = Convert.ToString(row["DunWei"]).Replace("\n", " ");
                    item.JDC_XCRS = Convert.ToString(row["XianChengRenShu"]).Replace("\n", " ");
                    time = new DateTime(0x76c, 1, 1);
                    if (DateTime.TryParse(row["DanJuRiQi-JDC"].ToString().Replace("\n", " "), out time))
                    {
                        item.JDC_DJRQ = time;
                    }
                    else
                    {
                        item.JDC_DJRQ = DateTime.Now.Date;
                    }
                    item.JDC_BZ = Convert.ToString(row["BeiZhu-JDC"]).Replace("\n", " ");
                    item.JDC_NSRSBH = Convert.ToString(row["NaShuiRenShiBieHao"]).Replace("\n", " ");
                }
                list.Add(item);
            }
            return list;
        }

        private List<SaleBillImportTemp> FetchBillFromTXT(string Path, InvType invType, ErrorResolver errorAnalyse)
        {
            int count;
            SaleBillImportTemp temp;
            int num9;
            DateTime now;
            string safeString;
            int num10;
            int num11;
            List<string> list4;
            GoodsImportTemp temp2;
            bool flag2;
            string str = "~~";
            string str2 = "SJJK0101";
            string str3 = "SJJK0103";
            string str4 = "SJJK0104";
            string str5 = "";
            if (Path.Length >= 3)
            {
                str5 = Path.Substring(Path.Length - 3, 3).ToUpper();
            }
            if (str5 != "TXT")
            {
                throw new CustomException("001");
            }
            string[] strArray = File.ReadAllLines(Path, ToolUtil.GetEncoding());
            List<string> list = new List<string>();
            for (count = 0; count < strArray.Length; count++)
            {
                if (!((strArray[count].Trim().Trim(new char[1]).Length == 0) || strArray[count].Trim().StartsWith("//")))
                {
                    list.Add(strArray[count]);
                }
            }
            if ((list.Count < 2) && (invType != InvType.vehiclesales))
            {
                throw new CustomException("001");
            }
            if (list.Count == 0)
            {
                return new List<SaleBillImportTemp>();
            }
            string row = list[0];
            bool flag = false;
            for (count = 0; count < list.Count; count++)
            {
                if (list[count].Contains("~~"))
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                if (!row.Contains("~~"))
                {
                    throw new Exception("文本文件与票种不匹配");
                }
                string str7 = row.Substring(0, row.IndexOf(str)).Trim();
                if ((invType == InvType.Common) || (invType == InvType.Special))
                {
                    if (!str7.Equals(str2))
                    {
                        throw new Exception("文本文件与票种不匹配");
                    }
                }
                else if (invType == InvType.transportation)
                {
                    if (!str7.Equals(str3))
                    {
                        throw new Exception("文本文件与票种不匹配");
                    }
                }
                else if ((invType == InvType.vehiclesales) && !str7.Equals(str4))
                {
                    throw new Exception("文本文件与票种不匹配");
                }
            }
            else
            {
                int index = row.IndexOf('"');
                int num3 = row.IndexOf('"', index + 1);
                int num4 = row.IndexOf(' ');
                int num5 = row.IndexOf(',');
                if ((index != -1) && ((index < num4) && (index < num5)))
                {
                    int num6 = row.IndexOf(' ', num3 + 1);
                    int num7 = row.IndexOf(',', num3 + 1);
                    if ((num6 != -1) && (num7 != -1))
                    {
                        if (num6 < num7)
                        {
                            str = " ";
                        }
                        else
                        {
                            str = ",";
                        }
                    }
                    else if (num6 != -1)
                    {
                        str = " ";
                    }
                    else
                    {
                        str = ",";
                    }
                }
                else if ((num4 != -1) && (num5 != -1))
                {
                    if (num4 < num5)
                    {
                        str = " ";
                    }
                    else
                    {
                        str = ",";
                    }
                }
                else if (num4 != -1)
                {
                    str = " ";
                }
                else
                {
                    str = ",";
                }
            }
            int num8 = 0;
            if (!str.Equals("~~"))
            {
                num8 = -1;
            }
            List<SaleBillImportTemp> list2 = new List<SaleBillImportTemp>();
            goto Label_1242;
        Label_1237:
            list2.Add(temp);
        Label_1242:
            flag2 = true;
            num8++;
            if (num8 >= list.Count)
            {
                return list2;
            }
            row = list[num8];
            List<string> list3 = this.StringSplit(row, str);
            temp = new SaleBillImportTemp();
            if ((invType == InvType.Common) || (invType == InvType.Special))
            {
                temp.BH = list3[0];
                num9 = 0;
                if (list3.Count > 1)
                {
                    string s = list3[1].Replace(" ", "");
                    if (!int.TryParse(s, out num9))
                    {
                        num9 = -1;
                    }
                    if (list3.Count < 7)
                    {
                        count = list3.Count;
                        while (count < 7)
                        {
                            list3.Add("");
                            count++;
                        }
                    }
                    temp.GoodsNum = s;
                    temp.GFMC = list3[2].Trim();
                    temp.GFSH = list3[3].Trim();
                    temp.GFDZDH = list3[4].Trim();
                    temp.GFYHZH = list3[5].Trim();
                    temp.BZ = list3[6].Trim();
                    if (list3.Count > 7)
                    {
                        temp.FHR = list3[7].Trim();
                    }
                    if (list3.Count > 8)
                    {
                        temp.SKR = list3[8].Trim();
                    }
                    if (list3.Count > 9)
                    {
                        temp.QDHSPMC = list3[9].Trim();
                    }
                    temp.DJRQ = DateTime.Now.Date;
                    if ((list3.Count > 10) && !string.IsNullOrEmpty(list3[10].Trim()))
                    {
                        now = DateTime.Now;
                        if (list3[10].Trim().Length == 10)
                        {
                            if (((list3[10].Trim().Substring(4, 1).CompareTo("-") == 0) && (list3[10].Trim().Substring(7, 1).CompareTo("-") == 0)) && !DateTime.TryParse(list3[10].Trim(), out now))
                            {
                                now = DateTime.Now;
                            }
                        }
                        else if (list3[10].Length == 8)
                        {
                            safeString = list3[10].Trim();
                            if (!DateTime.TryParse(safeString.Substring(0, 4) + "-" + safeString.Substring(4, 2) + "-" + safeString.Substring(6, 2), out now))
                            {
                                now = DateTime.Now;
                            }
                        }
                        temp.DJRQ = now.Date;
                    }
                    if (list3.Count > 11)
                    {
                        temp.XFYHZH = list3[11].Trim();
                    }
                    if (list3.Count > 12)
                    {
                        temp.XFDZDH = list3[12].Trim();
                    }
                    if (list3.Count > 13)
                    {
                        temp.SFZJY = list3[13].Trim();
                    }
                    if (list3.Count > 14)
                    {
                        temp.HYSY = list3[14].Trim();
                    }
                    num10 = num9;
                    while (0 < num9--)
                    {
                        num8++;
                        num11 = num10 - num9;
                        if (num8 < list.Count)
                        {
                            list4 = this.StringSplit(list[num8], str);
                            for (count = list4.Count; count < 14; count++)
                            {
                                list4.Add("");
                            }
                            temp2 = new GoodsImportTemp {
                                XSDJBH = temp.BH,
                                XH = num11,
                                SPMC = list4[0].Trim(),
                                JLDW = list4[1].Trim(),
                                GGXH = list4[2].Trim(),
                                SL = list4[3].Trim(),
                                JE = list4[4].Trim(),
                                SLV = list4[5].Trim(),
                                SPSM = list4[6].Trim(),
                                ZKJE = list4[7].Trim(),
                                SE = list4[8].Trim()
                            };
                            try
                            {
                                if ((temp2.SE.Trim() != "") && double.Parse(temp2.SE).Equals((double) 0.0))
                                {
                                    temp2.SE = "";
                                }
                            }
                            catch (Exception exception)
                            {
                                string str11 = exception.ToString();
                            }
                            temp2.ZKSE = list4[9].Trim();
                            temp2.ZKL = list4[10].Trim();
                            temp2.DJ = list4[11].Trim();
                            temp2.HSJBZ = list4[12].Trim();
                            if (CommonTool.isCEZS())
                            {
                                temp2.KCE = list4[13].Trim();
                            }
                            else
                            {
                                temp2.KCE = "0";
                            }
                            temp.ListGoods.Add(temp2);
                        }
                    }
                }
            }
            else
            {
                if (invType == InvType.vehiclesales)
                {
                    if (list3.Count <= 1)
                    {
                        goto Label_1237;
                    }
                    temp.JDC_BH = list3[0];
                    if (list3.Count >= 0x16)
                    {
                        temp.JDC_GHDW = list3[1].Trim();
                        temp.JDC_SFZ = list3[2].Trim();
                        temp.JDC_JE = list3[3].Trim();
                        temp.JDC_SL = list3[4].Trim();
                        temp.JDC_LX = list3[5].Trim();
                        temp.JDC_CPXH = list3[6].Trim();
                        temp.JDC_CD = list3[7].Trim();
                        temp.JDC_SCCJMC = list3[8].Trim();
                        temp.JDC_HGZH = list3[9].Trim();
                        temp.JDC_JKZMSH = list3[10].Trim();
                        temp.JDC_SJDH = list3[11].Trim();
                        temp.JDC_FDJH = list3[12].Trim();
                        temp.JDC_CLSBH = list3[13].Trim();
                        temp.JDC_DH = list3[14].Trim();
                        temp.JDC_ZH = list3[15].Trim();
                        temp.JDC_DZ = list3[0x10].Trim();
                        temp.JDC_KHYH = list3[0x11].Trim();
                        temp.JDC_DW = list3[0x12].Trim();
                        temp.JDC_XCRS = list3[0x13].Trim();
                        temp.JDC_DJRQ = DateTime.Now.Date;
                        if (!string.IsNullOrEmpty(list3[20].Trim()))
                        {
                            now = DateTime.Now;
                            if (((list3[20].Trim().Length == 10) && ((list3[20].Trim().Substring(4, 1).CompareTo("-") == 0) && (list3[20].Trim().Substring(7, 1).CompareTo("-") == 0))) && !DateTime.TryParse(list3[20].Trim(), out now))
                            {
                                now = DateTime.Now;
                            }
                            if (list3[20].Trim().Length == 8)
                            {
                                safeString = list3[20].Trim();
                                if (!DateTime.TryParse(safeString.Substring(0, 4) + "-" + safeString.Substring(4, 2) + "-" + safeString.Substring(6, 2), out now))
                                {
                                    now = DateTime.Now;
                                }
                            }
                            temp.JDC_DJRQ = now.Date;
                        }
                        temp.JDC_BZ = list3[0x15].Trim();
                        temp.JDC_NSRSBH = list3[0x16].Trim();
                        goto Label_1237;
                    }
                    errorAnalyse.AddError("单据格式错误", temp.JDC_BH, 0, false);
                    goto Label_1242;
                }
                if ((invType == InvType.transportation) && (list3.Count > 1))
                {
                    temp.HY_BH = list3[0].Trim();
                    num9 = 0;
                    string str12 = list3[1].Replace(" ", "");
                    if (!int.TryParse(str12, out num9))
                    {
                        num9 = -1;
                    }
                    if (list3.Count >= 0x11)
                    {
                        temp.GoodsNum = str12;
                        temp.HY_SPFMC = list3[2].Trim();
                        temp.HY_SPFSH = list3[3].Trim();
                        temp.HY_SHRMC = list3[4].Trim();
                        temp.HY_SHRSH = list3[5].Trim();
                        temp.HY_FHRMC = list3[6].Trim();
                        temp.HY_FHRSH = list3[7].Trim();
                        temp.HY_SL = list3[8].Trim();
                        temp.HY_QYMD = list3[9].Trim();
                        temp.HY_CZCH = list3[10].Trim();
                        temp.HY_CHDW = list3[11].Trim();
                        temp.HY_YSHWXX = list3[12].Trim();
                        temp.HY_BZ = list3[13].Trim();
                        temp.HY_FHR = list3[14].Trim();
                        temp.HY_SKR = list3[15].Trim();
                        temp.HY_DJRQ = DateTime.Now.Date;
                        if (!string.IsNullOrEmpty(list3[0x10].Trim()))
                        {
                            now = DateTime.Now;
                            if (((list3[0x10].Trim().Length == 10) && ((list3[0x10].Trim().Substring(4, 1).CompareTo("-") == 0) && (list3[0x10].Trim().Substring(7, 1).CompareTo("-") == 0))) && !DateTime.TryParse(list3[0x10].Trim(), out now))
                            {
                                now = DateTime.Now;
                            }
                            if (list3[0x10].Trim().Length == 8)
                            {
                                safeString = list3[0x10].Trim();
                                if (!DateTime.TryParse(safeString.Substring(0, 4) + "-" + safeString.Substring(4, 2) + "-" + safeString.Substring(6, 2), out now))
                                {
                                    now = DateTime.Now;
                                }
                            }
                            temp.HY_DJRQ = now.Date;
                        }
                    }
                    else
                    {
                        errorAnalyse.AddError("单据格式错误", temp.HY_BH, 0, false);
                        goto Label_1242;
                    }
                    num10 = num9;
                    while (0 < num9--)
                    {
                        num8++;
                        num11 = num10 - num9;
                        if (num8 < list.Count)
                        {
                            list4 = this.StringSplit(list[num8], str);
                            if (list4.Count >= 2)
                            {
                                temp2 = new GoodsImportTemp {
                                    XSDJBH = temp.BH,
                                    XH = num11
                                };
                                safeString = GetSafeData.GetSafeString(list4[0].Trim(), 20);
                                temp2.SPMC = safeString;
                                temp2.JE = list4[1].Trim();
                                temp.ListGoods.Add(temp2);
                            }
                            else if (list4.Count == 1)
                            {
                                temp2 = new GoodsImportTemp {
                                    XSDJBH = temp.BH,
                                    XH = num11
                                };
                                safeString = GetSafeData.GetSafeString(list4[0].Trim(), 20);
                                temp2.SPMC = safeString;
                                temp2.JE = "";
                                temp.ListGoods.Add(temp2);
                            }
                        }
                    }
                }
            }
            goto Label_1237;
        }

        public DataTable GetFileData(string File1, string Sheet1, int row1, List<ExcelMappingItem.Relation> YingShe, string DefaultFuHeRen, string DefaultShouKuanRen, string DefaultShuiLv)
        {
            return ResolverExcel.GetFileData(File1, string.Empty, Sheet1, string.Empty, row1, 0, 0, 0, YingShe, DefaultFuHeRen, DefaultShouKuanRen, DefaultShuiLv);
        }

        private decimal GetRound(decimal value, int digits = 2)
        {
            return SaleBillCtrl.GetRound(value, 2);
        }

        private double GetRound(double value, int digits = 2)
        {
            return SaleBillCtrl.GetRound(value, digits);
        }

        protected SaleBillImportTemp GetTempXSDJ(string ID, List<SaleBillImportTemp> billList, InvType invtype)
        {
            foreach (SaleBillImportTemp temp in billList)
            {
                if ((invtype == InvType.Common) || (invtype == InvType.Special))
                {
                    if (temp.BH == ID)
                    {
                        return temp;
                    }
                }
                else if (invtype == InvType.transportation)
                {
                    if (temp.HY_BH == ID)
                    {
                        return temp;
                    }
                }
                else if ((invtype == InvType.vehiclesales) && (temp.JDC_BH == ID))
                {
                    return temp;
                }
            }
            return null;
        }

        internal void HeBingZheKouHang(SaleBill bill, InvType Fplx)
        {
            SaleBillCtrl instance = SaleBillCtrl.Instance;
            int zkhs = 1;
            bool flag = false;
            double jEr = 0.0;
            double sEr = 0.0;
            double zkl = 0.0;
            double round = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            for (int i = 0; i <= bill.ListGoods.Count; i++)
            {
                if (i == bill.ListGoods.Count)
                {
                    if (flag)
                    {
                        this.DisCount(bill, i - 1, zkhs, zkl, jEr, sEr);
                    }
                    break;
                }
                Goods goods = bill.ListGoods[i];
                if (round < 0.0)
                {
                    round = this.GetRound(goods.JE, 2);
                    num6 = this.GetRound(goods.SE, 2);
                    num7 = this.GetRound(goods.SL, 2);
                    jEr = this.GetRound(goods.ZKJE, 2);
                    sEr = this.GetRound(goods.ZKSE, 2);
                    if (flag)
                    {
                        instance.DisCount(bill, i, zkhs, zkl, 0.0, 0.0);
                    }
                    flag = false;
                    round = this.GetRound((double) (round + Math.Abs(jEr)), 2);
                    num6 = this.GetRound((double) (num6 + Math.Abs(sEr)), 2);
                    if ((Math.Abs(num7) > 1E-08) && !goods.DanJiaError)
                    {
                        if (((goods.SLV == 0.05) && (Fplx == InvType.Special)) && bill.HYSY)
                        {
                            goods.DJ = (round + num6) / num7;
                        }
                        else if (goods.HSJBZ)
                        {
                            goods.DJ = (round + num6) / num7;
                        }
                        else
                        {
                            goods.DJ = round / num7;
                        }
                    }
                    goods.JE = this.GetRound(round, 2);
                    goods.SE = this.GetRound(num6, 2);
                    jEr = 0.0;
                    sEr = 0.0;
                    zkl = 0.0;
                }
                else
                {
                    if (goods.ZKJE != 0.0)
                    {
                        if ((i > 0) && flag)
                        {
                            bool flag2 = Math.Abs((double) (goods.ZKL - bill.ListGoods[i - 1].ZKL)) < 1E-05;
                            bool flag3 = Math.Abs((double) (goods.SLV - bill.ListGoods[i - 1].SLV)) < 1E-05;
                            if (flag2 && flag3)
                            {
                                jEr += SaleBillCtrl.GetRound(goods.ZKJE, 2);
                                sEr += SaleBillCtrl.GetRound(goods.ZKSE, 2);
                                zkhs++;
                                goto Label_03B3;
                            }
                            this.DisCount(bill, i - 1, zkhs, zkl, jEr, sEr);
                            i++;
                            jEr = 0.0;
                            sEr = 0.0;
                            zkl = 0.0;
                            zkhs = 1;
                        }
                        zkl = goods.ZKL;
                        jEr = SaleBillCtrl.GetRound(goods.ZKJE, 2);
                        sEr = SaleBillCtrl.GetRound(goods.ZKSE, 2);
                        flag = true;
                    }
                    else if (flag)
                    {
                        this.DisCount(bill, i - 1, zkhs, zkl, jEr, sEr);
                        i++;
                        jEr = 0.0;
                        sEr = 0.0;
                        zkl = 0.0;
                        flag = false;
                        zkhs = 1;
                    }
                Label_03B3:;
                }
            }
        }

        internal void HeBingZheKouHang2(SaleBill bill, InvType Fplx)
        {
            int num8;
            SaleBillCtrl instance = SaleBillCtrl.Instance;
            int zkhs = 1;
            double num2 = 0.0;
            double num3 = 0.0;
            double round = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            for (num8 = 0; num8 < bill.ListGoods.Count; num8++)
            {
                Goods goods = bill.ListGoods[num8];
                round = this.GetRound(goods.JE, 2);
                if (round < 0.0)
                {
                    num6 = this.GetRound(goods.SE, 2);
                    num7 = this.GetRound(goods.SL, 2);
                    num2 = this.GetRound(goods.ZKJE, 2);
                    num3 = this.GetRound(goods.ZKSE, 2);
                    instance.DisCount(bill, num8, zkhs, goods.ZKL, goods.ZKJE, 0.0);
                    num8++;
                    round = this.GetRound((double) (round + Math.Abs(num2)), 2);
                    num6 = this.GetRound((double) (num6 + Math.Abs(num3)), 2);
                    if ((Math.Abs(num7) > 1E-08) && !goods.DanJiaError)
                    {
                        if (((goods.SLV == 0.05) && (Fplx == InvType.Special)) && bill.HYSY)
                        {
                            goods.DJ = (round + num6) / num7;
                        }
                        else if (goods.HSJBZ)
                        {
                            goods.DJ = (round + num6) / num7;
                        }
                        else
                        {
                            goods.DJ = round / num7;
                        }
                    }
                    goods.JE = this.GetRound(round, 2);
                    goods.SE = this.GetRound(num6, 2);
                    num2 = 0.0;
                    num3 = 0.0;
                }
                else if (goods.ZKJE != 0.0)
                {
                    try
                    {
                        instance.DisCount(bill, num8, zkhs, goods.ZKL, goods.ZKJE, 0.0);
                        num8++;
                    }
                    catch (Exception exception)
                    {
                        string str = exception.ToString();
                    }
                }
            }
            for (num8 = 0; num8 < bill.ListGoods.Count; num8++)
            {
                bill.ListGoods[num8].XH = num8 + 1;
            }
        }

        public ErrorResolver ImportSaleBillExcel(string File1, string File2, PriceType priceType, InvType invType)
        {
            ErrorResolver errorAnalyse = new ErrorResolver();
            List<SaleBillImportTemp> listBill = this.FetchBillFromExcel(File1, File2, invType, errorAnalyse);
            new SaleBillDAL().SaveImportBill(listBill, priceType, invType, errorAnalyse);
            return errorAnalyse;
        }

        public ErrorResolver ImportSaleBillTxt(string path, PriceType priceType, InvType invType)
        {
            ErrorResolver errorAnalyse = new ErrorResolver();
            List<SaleBillImportTemp> listBill = this.FetchBillFromTXT(path, invType, errorAnalyse);
            new SaleBillDAL().SaveImportBill(listBill, priceType, invType, errorAnalyse);
            return errorAnalyse;
        }

        private void InitInvoice(SaleBill bill, FPLX fplx)
        {
            byte[] buffer = null;
            Invoice invoice = new Invoice(false, false, false, fplx, buffer, null);
            if (fplx == 12)
            {
                invoice.set_Jdc_ver_new(true);
                string gFMC = bill.GFMC;
                invoice.set_Gfmc(gFMC);
                string strB = invoice.get_Gfmc();
                if (gFMC.CompareTo(strB) != 0)
                {
                    bill.GFMC = strB;
                }
                string gFDZDH = bill.GFDZDH;
                invoice.set_Cllx(gFDZDH);
                string str4 = invoice.get_Cllx();
                if (gFDZDH.CompareTo(str4) != 0)
                {
                    bill.GFDZDH = str4;
                }
            }
        }

        internal void MergeDiscountGoods(SaleBill bill, InvType Fplx)
        {
        }

        private List<string> StringSplit(string row, string SplitStr)
        {
            string[] strArray;
            string str2;
            int index;
            int num2;
            int num3;
            string str3;
            string str5;
            bool flag;
            List<string> list = new List<string>();
            row = row.Trim();
            row = row.TrimEnd(new char[1]);
            if (SplitStr == "~~")
            {
                strArray = row.Split(new string[] { SplitStr }, StringSplitOptions.None);
                foreach (string str in strArray)
                {
                    list.Add(str.Trim());
                }
                return list;
            }
            if (!(SplitStr == " "))
            {
                str2 = "";
                str = row;
            }
            else
            {
                str2 = "";
                str = row;
                while (true)
                {
                    flag = true;
                    index = str.IndexOf('"');
                    num2 = 0;
                    if (index != -1)
                    {
                        num2 = str.IndexOf('"', index + 1);
                    }
                    if ((index != -1) && (num2 != -1))
                    {
                        num3 = num2 - index;
                        str3 = str.Substring(index, num3 + 1);
                        if (num3 == 1)
                        {
                            str3 = "##~##";
                        }
                        else
                        {
                            str3 = str3.Replace(" ", "~~").Replace("\"", "");
                        }
                        str2 = str2 + str.Substring(0, index) + str3;
                        str = str.Substring(num2 + 1, (str.Length - num2) - 1);
                    }
                    else
                    {
                        str2 = str2 + str;
                        strArray = str2.Split(new string[] { SplitStr }, StringSplitOptions.None);
                        foreach (string str4 in strArray)
                        {
                            str5 = str4;
                            if (str5.Length != 0)
                            {
                                if (str5.Equals("##~##"))
                                {
                                    str5 = "";
                                }
                                str5 = str5.Replace("~~", " ");
                                list.Add(str5);
                            }
                        }
                        return list;
                    }
                }
            }
            while (true)
            {
                flag = true;
                index = str.IndexOf('"');
                num2 = 0;
                if (index != -1)
                {
                    num2 = str.IndexOf('"', index + 1);
                }
                if ((index != -1) && (num2 != -1))
                {
                    num3 = num2 - index;
                    str3 = str.Substring(index, num3 + 1).Replace(",", "~~").Replace("\"", "");
                    str2 = str2 + str.Substring(0, index) + str3;
                    str = str.Substring(num2 + 1, (str.Length - num2) - 1);
                }
                else
                {
                    strArray = (str2 + str).Split(new string[] { SplitStr }, StringSplitOptions.None);
                    foreach (string str4 in strArray)
                    {
                        str5 = str4;
                        str5 = str5.Replace("~~", ",");
                        list.Add(str5);
                    }
                    return list;
                }
            }
        }

        public static double TodoubleValidate(string value)
        {
            string str = value;
            double result = 0.0;
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                str = "ImportNull";
                return result;
            }
            if (!double.TryParse(value, out result))
            {
                str = "ImportError";
            }
            return result;
        }

        public static double TodoubleValidateJE(string value)
        {
            string str = value;
            double result = 0.0;
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                str = "ImportNull";
                return result;
            }
            if (!double.TryParse(value, out result))
            {
                str = "ImportError";
            }
            return result;
        }

        private static double ToSlv(string Slv)
        {
            double result = 0.0;
            if (Slv.Trim().Length > 0)
            {
                if (Slv.EndsWith("%"))
                {
                    Slv = Slv.Trim(new char[] { '%' });
                }
                if (!double.TryParse(Slv, out result))
                {
                    return -1000.0;
                }
                if (result >= 1.0)
                {
                    result /= 100.0;
                }
            }
            return result;
        }

        public static string ToValidate(string value)
        {
            string str = value.Trim();
            double result = 0.0;
            if (string.IsNullOrEmpty(value))
            {
                return "ImportNull";
            }
            if (!double.TryParse(value, out result))
            {
                str = "ImportError";
            }
            return str;
        }

        private static string ToValidateFLBM(string flbm)
        {
            return flbm.Trim();
        }

        public static bool ToValidateXSYH(string xsyh)
        {
            return ((((xsyh.ToUpper() == "TRUE") || (xsyh.Trim() == "1")) || (xsyh.Trim() == "是")) || (xsyh.Trim() == "享受"));
        }

        public static string ToValidateZKL(string zkl)
        {
            double result = 0.0;
            zkl = zkl.Trim();
            if (string.IsNullOrEmpty(zkl))
            {
                return "ImportNull";
            }
            if (zkl.EndsWith("%"))
            {
                if (!double.TryParse(zkl.Trim(new char[] { '%' }), out result))
                {
                    return "ImportError";
                }
                result /= 100.0;
            }
            else
            {
                if (!double.TryParse(zkl, out result))
                {
                    return "ImportError";
                }
                if (result > 1.0)
                {
                    result /= 100.0;
                }
            }
            return result.ToString();
        }
    }
}

