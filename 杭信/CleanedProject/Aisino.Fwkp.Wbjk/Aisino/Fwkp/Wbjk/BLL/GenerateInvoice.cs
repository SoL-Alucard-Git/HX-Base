namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.CommonLibrary;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Xml;

    public class GenerateInvoice
    {
        private FPTKdal fptkDAL = new FPTKdal();
        private static GenerateInvoice instance = null;
        private ILog log = LogUtil.GetLogger<GenerateInvoice>();

        private GenerateInvoice()
        {
        }

        public bool CanInvoice(SaleBill bill, int[] slvjeseIndex)
        {
            FPGenerateResult result = new FPGenerateResult(bill) {
                KPZL = this.SetInvKPZL(bill, null)
            };
            return (bool) this.CheckInvoiceFF(result, slvjeseIndex)[0];
        }

        private string CheckHZFPXXB(string num, FPLX fplx)
        {
            object[] objArray = new object[] { num, fplx };
            object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.CheckHZXXBBH", objArray);
            if (objArray2 == null)
            {
                return "A310";
            }
            if (!((bool) objArray2[0]))
            {
                return "A310";
            }
            return "0000";
        }

        public object[] CheckInvoiceFF(FPGenerateResult result, int[] idxMir)
        {
            object[] objArray5;
            try
            {
                int count;
                int num2;
                bool flag5;
                string str8;
                int companyType;
                XSDJ_MXModel model;
                string str12;
                string str13;
                bool flag7;
                double sLV;
                string str14;
                string str15;
                string infoFromNotes;
                string str32;
                string str33;
                string str34;
                FPLX invType = (FPLX) CommonTool.GetInvType(result.DJZL);
                bool flag = result.KPJE < 0.0;
                bool flag2 = false;
                flag2 = result.KPZL == "清单开票";
                SaleBillCtrl instance = SaleBillCtrl.Instance;
                byte[] sourceArray = Invoice.get_TypeByte();
                byte[] destinationArray = new byte[0x20];
                Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                byte[] buffer4 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KT" + DateTime.Now.ToString("F")), destinationArray, buffer3);
                bool flag3 = false;
                Invoice invoice = null;
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if ((result.DJZL == "c") || (result.DJZL == "s"))
                {
                    if ((result.JEHJ < 0.0) && flag2)
                    {
                        invoice = new Invoice(false, flag2, flag3, invType, buffer4, null);
                    }
                    else
                    {
                        invoice = new Invoice(flag, flag2, flag3, invType, buffer4, null);
                    }
                }
                else
                {
                    string invControlNum;
                    string zGJGDMMC;
                    string str3;
                    string str4;
                    string[] strArray;
                    if (result.DJZL == "f")
                    {
                        invoice = new Invoice(flag, flag2, flag3, invType, buffer4, null);
                        invoice.set_Shrmc(result.GFDZDH);
                        invoice.set_Shrsh(result.CM);
                        invoice.set_Fhrmc(result.XFDZDH);
                        invoice.set_Fhrsh(result.TYDH);
                        invoice.set_Qyd_jy_ddd(result.XFYHZH);
                        invoice.set_Yshwxx(result.YSHWXX);
                        invoice.set_Czch(result.QYD);
                        invoice.set_Ccdw(result.DW);
                        count = result.ListXSDJ_MX.Count;
                        for (num2 = 0; num2 < count; num2++)
                        {
                            invoice.SetSpmc(num2, result.ListXSDJ_MX[num2].SPMC);
                            invoice.SetJe(num2, result.ListXSDJ_MX[num2].JE.ToString());
                        }
                        invControlNum = card.GetInvControlNum();
                        if (string.IsNullOrEmpty(invControlNum))
                        {
                            invControlNum = "";
                        }
                        invoice.set_Jqbh(invControlNum);
                        zGJGDMMC = card.get_SQInfo().ZGJGDMMC;
                        str3 = "";
                        str4 = "";
                        if (!string.IsNullOrEmpty(zGJGDMMC))
                        {
                            strArray = zGJGDMMC.Split(new char[] { ',' });
                            if (strArray.Length == 2)
                            {
                                str3 = strArray[1];
                                str4 = strArray[0];
                            }
                        }
                        invoice.set_Zgswjg_mc(str3);
                        invoice.set_Zgswjg_dm(str4);
                    }
                    else if (result.DJZL == "j")
                    {
                        invoice = new Invoice(flag, flag2, flag3, invType, buffer4, null);
                        invoice.set_Sfzh_zzjgdm(result.GFSH);
                        invoice.set_Cllx(result.GFDZDH);
                        invoice.set_Cpxh(result.XFDZ);
                        invoice.set_Cd(result.KHYHMC);
                        invoice.set_Hgzh(result.CM);
                        invoice.set_Jkzmsh(result.TYDH);
                        invoice.set_Sjdh(result.QYD);
                        invoice.set_Fdjhm(result.ZHD);
                        invoice.set_Clsbdh_cjhm(result.XHD);
                        invoice.set_Dh(result.XFDH);
                        invoice.set_Zh(result.KHYHZH);
                        invoice.set_Dz(result.XFDZDH);
                        invoice.set_Khyh(result.XFYHZH);
                        invoice.set_Dw(result.DW);
                        invoice.set_Xcrs(result.MDD);
                        invoice.set_Sccjmc(result.SCCJMC);
                        invoice.set_Jdc_ver_new(result.ISNEWJDC);
                        invControlNum = card.GetInvControlNum();
                        if (string.IsNullOrEmpty(invControlNum))
                        {
                            invControlNum = "";
                        }
                        invoice.set_Jqbh(invControlNum);
                        zGJGDMMC = card.get_SQInfo().ZGJGDMMC;
                        str3 = "";
                        str4 = "";
                        if (!string.IsNullOrEmpty(zGJGDMMC))
                        {
                            strArray = zGJGDMMC.Split(new char[] { ',' });
                            if (strArray.Length == 2)
                            {
                                str3 = strArray[1];
                                str4 = strArray[0];
                            }
                        }
                        invoice.set_Zgswjg_mc(str3);
                        invoice.set_Zgswjg_dm(str4);
                        string str5 = result.JDC_FLBM;
                        if (CommonTool.isSPBMVersion())
                        {
                            flag5 = false;
                            if (result.QDHSPMC.Equals("详见对应正数发票及清单"))
                            {
                                flag5 = true;
                            }
                            if (!(flag5 || ((str5 != null) && (str5.Length > 0))))
                            {
                                return new object[] { false, invoice, "A314" };
                            }
                            if (str5.Length < 0x13)
                            {
                                while (str5.Length < 0x13)
                                {
                                    str5 = str5 + "0";
                                }
                            }
                            string str6 = result.JDC_XSYH ? "1" : "0";
                            invoice.set_Zyspmc(str5);
                            invoice.set_Zyspsm(result.JDC_CLBM + "#%" + result.JDC_XSYHSM);
                            invoice.set_Skr(str6 + "#%" + result.JDC_LSLVBS);
                        }
                        else
                        {
                            invoice.set_Zyspmc("");
                        }
                    }
                }
                string maxBMBBBH = new SPFLService().GetMaxBMBBBH();
                if (maxBMBBBH == "0.0")
                {
                    return new object[] { false, invoice, "A313" };
                }
                invoice.set_Bmbbbh(maxBMBBBH);
                bool flag6 = false;
                if ((result.DJZL == "c") || (result.DJZL == "s"))
                {
                    str8 = card.get_TaxCode();
                    companyType = TaxCardFactory.CreateTaxCard().get_StateInfo().CompanyType;
                    result.BZ = result.BZ.Replace("代开企业税号：", "代开企业税号:");
                    result.BZ = result.BZ.Replace("代开企业税号：", "代开企业税号:");
                    result.BZ = result.BZ.Replace("代开企业税号：", "代开企业税号:");
                    result.BZ = result.BZ.Replace("代开企业名称：", "代开企业名称:");
                    result.BZ = result.BZ.Replace("代开企业名称：", "代开企业名称:");
                    result.BZ = result.BZ.Replace("代开企业名称：", "代开企业名称:");
                    for (num2 = 0; num2 < result.ListXSDJ_MX.Count; num2++)
                    {
                        model = result.ListXSDJ_MX[num2];
                        if ((invoice.get_ZyfpLx() != 11) && (Math.Abs(model.KCE) > 1E-05))
                        {
                            invoice.SetZyfpLx(11);
                            string str9 = "";
                            if (model.KCE > 1E-06)
                            {
                                str9 = "差额征税：" + model.KCE.ToString("0.00") + "。";
                            }
                            else
                            {
                                str9 = "差额征税。";
                            }
                            result.BZ = str9 + result.BZ;
                            result.BZ = GetSafeData.GetSafeString(result.BZ, 230);
                        }
                    }
                    if ((!string.IsNullOrEmpty(str8) && (str8.Length == 15)) && (str8.Substring(8, 2) == "DK"))
                    {
                        flag6 = true;
                    }
                    if ((result.DJZL == "c") && (result.TYDH == "2"))
                    {
                        if (string.IsNullOrEmpty(result.XFDZDH))
                        {
                            if (flag6)
                            {
                                return new object[] { false, invoice, "A318" };
                            }
                            string str10 = new StringBuilder().Append(card.get_Address()).Append(" ").Append(card.get_Telephone()).ToString();
                            result.XFDZDH = str10;
                        }
                        if (string.IsNullOrEmpty(result.XFYHZH))
                        {
                            if (flag6)
                            {
                                return new object[] { false, invoice, "A319" };
                            }
                            string str11 = card.get_BankAccount().Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                            result.XFYHZH = str11;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(result.XFDZDH))
                        {
                            if (flag6)
                            {
                                return new object[] { false, invoice, "A318" };
                            }
                            str12 = new StringBuilder().Append(card.get_Address()).Append(" ").Append(card.get_Telephone()).ToString();
                            result.XFDZDH = str12;
                        }
                        if (string.IsNullOrEmpty(result.XFYHZH))
                        {
                            if (flag6 && (result.DJZL == "s"))
                            {
                                return new object[] { false, invoice, "A319" };
                            }
                            if (!flag6 || (result.DJZL != "c"))
                            {
                                str13 = card.get_BankAccount().Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                                result.XFYHZH = str13;
                            }
                        }
                    }
                }
                else if ((result.DJZL == "f") || (result.DJZL == "j"))
                {
                    str12 = new StringBuilder().Append(card.get_Address()).Append(" ").Append(card.get_Telephone()).ToString();
                    result.XFDZDH = str12;
                    str13 = card.get_BankAccount().Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                    result.XFYHZH = str13;
                }
                int num4 = 0x10;
                int num5 = 0x10;
                byte[] buffer5 = new byte[num4];
                string[] strArray2 = new string[num4];
                byte[] buffer6 = new byte[num5];
                string[] strArray3 = new string[num5];
                if ((result.DJZL == "c") || (result.DJZL == "s"))
                {
                    flag7 = false;
                    count = result.ListXSDJ_MX.Count;
                    sLV = 0.0;
                    for (num2 = 0; num2 < count; num2++)
                    {
                        if (num2 == 0)
                        {
                            sLV = result.ListXSDJ_MX[num2].SLV;
                        }
                        else if (!(sLV == result.ListXSDJ_MX[num2].SLV))
                        {
                            flag7 = true;
                            break;
                        }
                    }
                    strArray3[idxMir[0]] = "";
                    strArray3[idxMir[1]] = result.BH;
                    strArray3[idxMir[2]] = result.GFMC;
                    strArray3[idxMir[3]] = result.XFDZDH;
                    strArray3[idxMir[4]] = result.XFYHZH;
                    strArray3[idxMir[5]] = "";
                    strArray3[idxMir[6]] = result.BZ;
                    strArray3[idxMir[7]] = result.GFDZDH;
                    strArray3[idxMir[8]] = result.GFYHZH;
                    strArray3[idxMir[9]] = result.FHR;
                    if (flag2 && (result.KPJE < 0.0))
                    {
                        strArray3[idxMir[10]] = Convert.ToString(Math.Abs(result.KPSE));
                    }
                    else
                    {
                        strArray3[idxMir[10]] = Convert.ToString(result.KPSE);
                    }
                    strArray3[idxMir[11]] = result.SKR;
                    strArray3[idxMir[12]] = result.GFSH;
                    strArray3[idxMir[13]] = UserInfo.get_Yhmc();
                    if (!flag7)
                    {
                        strArray3[idxMir[14]] = Convert.ToString(result.SLV);
                    }
                    else
                    {
                        strArray3[idxMir[14]] = "";
                    }
                    if (flag2 && (result.KPJE < 0.0))
                    {
                        strArray3[idxMir[15]] = Convert.ToString(Math.Abs(result.KPJE));
                    }
                    else
                    {
                        strArray3[idxMir[15]] = Convert.ToString(result.KPJE);
                    }
                    if ((result.DJZL == "c") && (result.TYDH == "2"))
                    {
                        strArray3[idxMir[2]] = card.get_Corporation();
                        strArray3[idxMir[3]] = result.GFDZDH;
                        strArray3[idxMir[4]] = result.GFYHZH;
                        strArray3[idxMir[7]] = result.XFDZDH;
                        strArray3[idxMir[8]] = result.XFYHZH;
                        strArray3[idxMir[12]] = card.get_TaxCode();
                    }
                }
                else if (result.DJZL == "f")
                {
                    strArray3[idxMir[0]] = "";
                    strArray3[idxMir[1]] = result.BH;
                    strArray3[idxMir[2]] = result.GFMC;
                    strArray3[idxMir[3]] = result.XFDZDH;
                    strArray3[idxMir[4]] = result.XFYHZH;
                    strArray3[idxMir[5]] = "";
                    strArray3[idxMir[6]] = result.BZ;
                    strArray3[idxMir[7]] = "";
                    strArray3[idxMir[8]] = "";
                    strArray3[idxMir[9]] = result.FHR;
                    strArray3[idxMir[10]] = Convert.ToString(result.KPSE);
                    strArray3[idxMir[11]] = result.SKR;
                    strArray3[idxMir[12]] = result.GFSH;
                    strArray3[idxMir[13]] = UserInfo.get_Yhmc();
                    strArray3[idxMir[14]] = Convert.ToString(result.SLV);
                    strArray3[idxMir[15]] = Convert.ToString(result.KPJE);
                }
                else if (result.DJZL == "j")
                {
                    invoice.SetFpSLv(result.SLV.ToString());
                    invoice.SetJshj(result.JEHJ.ToString());
                    strArray3[idxMir[0]] = "";
                    strArray3[idxMir[1]] = result.BH;
                    strArray3[idxMir[2]] = result.GFMC;
                    strArray3[idxMir[3]] = result.XFDZDH;
                    strArray3[idxMir[4]] = result.XFYHZH;
                    strArray3[idxMir[5]] = "";
                    strArray3[idxMir[6]] = result.BZ;
                    strArray3[idxMir[7]] = "";
                    strArray3[idxMir[8]] = "";
                    strArray3[idxMir[9]] = "";
                    strArray3[idxMir[10]] = invoice.GetHjSe();
                    strArray3[idxMir[11]] = invoice.get_Skr();
                    strArray3[idxMir[12]] = result.GFYHZH;
                    strArray3[idxMir[13]] = UserInfo.get_Yhmc();
                    strArray3[idxMir[14]] = Convert.ToString(result.SLV);
                    strArray3[idxMir[15]] = invoice.GetHjJe();
                }
                byte[] buffer7 = invoice.get_RdmByte();
                object[] objArray = new object[] { buffer5, strArray3 };
                if ((result.DJZL == "c") || (result.DJZL == "s"))
                {
                    invoice.SetData(objArray);
                }
                else if (result.DJZL == "f")
                {
                    invoice.SetData(objArray);
                }
                else if (result.DJZL == "j")
                {
                    invoice.SetData(objArray);
                }
                bool flag8 = false;
                if ((result.HYSY && (result.DJZL == "s")) && (result.ListXSDJ_MX[0].SLV == 0.05))
                {
                    flag8 = true;
                }
                if (flag8)
                {
                    invoice.SetZyfpLx(1);
                }
                if (((result.DJZL == "c") || (result.DJZL == "s")) && (result.ListXSDJ_MX[0].SLV == 0.015))
                {
                    invoice.SetZyfpLx(10);
                }
                if (result.DJZL == "c")
                {
                    if (result.TYDH == "1")
                    {
                        invoice.SetZyfpLx(8);
                        invoice.set_Xfsh(card.get_TaxCode());
                        invoice.set_Xfmc(card.get_Corporation());
                    }
                    else if (result.TYDH == "2")
                    {
                        invoice.SetZyfpLx(9);
                        invoice.set_Xfsh(result.GFSH);
                        invoice.set_Xfmc(result.GFMC);
                    }
                    else
                    {
                        invoice.set_Xfsh(card.get_TaxCode());
                        invoice.set_Xfmc(card.get_Corporation());
                    }
                }
                else
                {
                    invoice.set_Xfsh(card.get_TaxCode());
                    invoice.set_Xfmc(card.get_Corporation());
                }
                if (((result.DJZL == "c") || (result.DJZL == "s")) && flag6)
                {
                    if (!NotesUtil.GetDKQYFromInvNotes(result.BZ, ref str15, ref str14).Equals("0000"))
                    {
                        return new object[] { false, invoice, "A321" };
                    }
                    if (str14.Length <= 0)
                    {
                        return new object[] { false, invoice, "A316" };
                    }
                    if (str15.Length <= 0)
                    {
                        return new object[] { false, invoice, "A317" };
                    }
                    InvType invoiceKind = (result.DJZL == "c") ? InvType.Common : InvType.Special;
                    FPLX fplx = (result.DJZL == "c") ? ((FPLX) 2) : ((FPLX) 0);
                    if (!SaleBillCheck.Instance.CheckTaxCode(invoiceKind, str15, false, fplx))
                    {
                        return new object[] { false, invoice, "A315" };
                    }
                    invoice.set_Dk_qymc(str14);
                    invoice.set_Dk_qysh(str15);
                }
                bool flag10 = false;
                for (num2 = 0; num2 < result.ListXSDJ_MX.Count; num2++)
                {
                    string str16;
                    string str21;
                    decimal dJ;
                    model = result.ListXSDJ_MX[num2];
                    if ((!flag8 && model.HSJBZ) && (model.SL == 0.0))
                    {
                        model.DJ = 0.0;
                    }
                    if ((num2 != 0) && (result.ListXSDJ_MX[num2].DJHXZ == 4))
                    {
                        string str17 = Convert.ToString(model.JE);
                        string str18 = Convert.ToString(model.SE);
                        int zKH = 0;
                        double disCount = 0.0;
                        string sPMC = model.SPMC;
                        if (CommonTool.isSPBMVersion())
                        {
                            disCount = CommonTool.GetDisCount(model.JE, result.ListXSDJ_MX[num2 - 1].JE, ref zKH);
                        }
                        else
                        {
                            disCount = CommonTool.GetDisCount(sPMC, ref zKH);
                        }
                        dJ = (decimal) disCount;
                        string str20 = dJ.ToString();
                        if (invoice.AddZkxx(num2 - 1, zKH, str20, str17, str18) < 0)
                        {
                            str21 = invoice.GetCode();
                            if (!(result.DJZL == "c") || !(str21 == "A001"))
                            {
                                str16 = invoice.GetCode();
                                return new object[] { false, invoice, str16 };
                            }
                            flag10 = true;
                        }
                        string fLBM = model.FLBM;
                        if (CommonTool.isSPBMVersion())
                        {
                            flag5 = false;
                            if (model.SPMC.Equals("详见对应正数发票及清单"))
                            {
                                flag5 = true;
                            }
                            if (!(flag5 || ((fLBM != null) && (fLBM.Length > 0))))
                            {
                                return new object[] { false, invoice, "A314" };
                            }
                            if (model.SPMC.Contains("折扣"))
                            {
                                return new object[] { false, invoice, "A350" };
                            }
                            if (fLBM.Length < 0x13)
                            {
                                while (fLBM.Length < 0x13)
                                {
                                    fLBM = fLBM + "0";
                                }
                            }
                            string str23 = model.XSYH ? "1" : "0";
                            invoice.SetSpbh(num2, model.SPBM);
                            invoice.SetXsyh(num2, str23);
                            invoice.SetLslvbs(num2, model.LSLVBS);
                            invoice.SetYhsm(num2, model.XSYHSM);
                            invoice.SetFlbm(num2, fLBM);
                        }
                        invoice.SetSpmc(num2, sPMC);
                    }
                    else
                    {
                        double round;
                        string spmc = model.SPMC;
                        string sPSM = model.SPSM;
                        string str26 = Convert.ToString(model.SLV);
                        if (result.DJZL == "f")
                        {
                            str26 = result.SLV.ToString();
                        }
                        string str27 = "";
                        if ((result.DJZL == "s") && flag8)
                        {
                            if ((model.SL == 0.0) && (model.DJ == 0.0))
                            {
                                str27 = "";
                            }
                            else if (!(model.DJ == 0.0))
                            {
                                dJ = (decimal) model.DJ;
                                str27 = dJ.ToString();
                            }
                            else if (!(model.SL == 0.0))
                            {
                                round = model.JE / 19.0;
                                double num10 = model.JE + round;
                                double num11 = num10 / model.SL;
                                str27 = ((decimal) num11).ToString();
                            }
                        }
                        else
                        {
                            str27 = ((model.SL == 0.0) && (model.DJ == 0.0)) ? "" : Convert.ToString((decimal) model.DJ);
                        }
                        ZYFP_LX zyfp_lx = 0;
                        if (flag8 && (result.DJZL == "s"))
                        {
                            zyfp_lx = 1;
                        }
                        if (result.DJZL == "c")
                        {
                            if (result.TYDH == "1")
                            {
                                zyfp_lx = 8;
                            }
                            else if (result.TYDH == "2")
                            {
                                zyfp_lx = 9;
                            }
                        }
                        if ((result.DJZL == "s") && (zyfp_lx == 0))
                        {
                            switch (instance.xtCheck(spmc))
                            {
                                case "1":
                                    zyfp_lx = 6;
                                    invoice.SetZyfpLx(zyfp_lx);
                                    break;

                                case "2":
                                    zyfp_lx = 7;
                                    invoice.SetZyfpLx(zyfp_lx);
                                    break;
                            }
                        }
                        if (invoice.get_ZyfpLx() == 10)
                        {
                            zyfp_lx = invoice.get_ZyfpLx();
                        }
                        if (invoice.get_ZyfpLx() == 11)
                        {
                            zyfp_lx = invoice.get_ZyfpLx();
                        }
                        bool hSJBZ = model.HSJBZ;
                        Spxx spxx = new Spxx(spmc, sPSM, str26, model.GGXH, model.JLDW, str27, hSJBZ, zyfp_lx);
                        if ((((result.DJZL == "c") || (result.DJZL == "s")) && flag2) && (model.JE < 0.0))
                        {
                            spxx.set_Je(Convert.ToString(Math.Abs(model.JE)));
                        }
                        else if ((result.DJZL == "f") && (model.JE < 0.0))
                        {
                            spxx.set_Je(Convert.ToString(Math.Abs(model.JE)));
                        }
                        else
                        {
                            spxx.set_Je(Convert.ToString(model.JE));
                        }
                        if (model.SL != 0.0)
                        {
                            if ((((result.DJZL == "c") || (result.DJZL == "s")) && flag2) && (model.JE < 0.0))
                            {
                                spxx.set_SL(Convert.ToString(Math.Abs((decimal) model.SL)));
                            }
                            else
                            {
                                spxx.set_SL(Convert.ToString((decimal) model.SL));
                            }
                        }
                        else
                        {
                            spxx.set_SL(null);
                        }
                        if (result.DJZL == "f")
                        {
                            round = 0.0;
                            if (model.JE < 0.0)
                            {
                                round = SaleBillCtrl.GetRound((double) ((0.0 - model.JE) * result.SLV), 2);
                            }
                            else
                            {
                                round = SaleBillCtrl.GetRound((double) (model.JE * result.SLV), 2);
                            }
                            spxx.set_Se(round.ToString());
                        }
                        else if ((result.DJZL == "s") && flag8)
                        {
                            round = model.SE;
                            if (flag2 && (model.JE < 0.0))
                            {
                                round = Math.Abs(round);
                            }
                            spxx.set_Se(round.ToString());
                        }
                        else if ((((result.DJZL == "c") || (result.DJZL == "s")) && flag2) && (model.JE < 0.0))
                        {
                            spxx.set_Se(Convert.ToString(Math.Abs(model.SE)));
                        }
                        else
                        {
                            spxx.set_Se(Convert.ToString(model.SE));
                        }
                        if (((result.DJZL == "s") || (result.DJZL == "c")) || (invoice.get_ZyfpLx() == 11))
                        {
                            spxx.set_Kce(Convert.ToString(model.KCE));
                        }
                        string str29 = model.FLBM;
                        if (CommonTool.isSPBMVersion())
                        {
                            bool flag13 = false;
                            if (model.SPMC.Equals("详见对应正数发票及清单"))
                            {
                                flag13 = true;
                            }
                            if (!(flag13 || ((str29 != null) && (str29.Length > 0))))
                            {
                                return new object[] { false, invoice, "A314" };
                            }
                            if (str29.Length < 0x13)
                            {
                                while (str29.Length < 0x13)
                                {
                                    str29 = str29 + "0";
                                }
                            }
                            spxx.set_Flbm(str29);
                            spxx.set_Xsyh(model.XSYH ? "1" : "0");
                            spxx.set_Yhsm(model.XSYHSM);
                            spxx.set_Lslvbs(model.LSLVBS);
                            spxx.set_Spbh(model.SPBM);
                        }
                        if (invoice.AddSpxx(spxx) < 0)
                        {
                            str21 = invoice.GetCode();
                            if (!(result.DJZL == "c") || !(str21 == "A001"))
                            {
                                str16 = invoice.GetCode();
                                return new object[] { false, invoice, str16 };
                            }
                            flag10 = true;
                        }
                    }
                }
                if (flag)
                {
                    string str31;
                    if (invType == 0)
                    {
                        infoFromNotes = CommonTool.GetInfoFromNotes(result.BZ, 1);
                        invoice.set_RedNum(infoFromNotes);
                        str31 = this.CheckHZFPXXB(infoFromNotes, 0);
                        if (str31 != "0000")
                        {
                            return new object[] { false, invoice, str31 };
                        }
                    }
                    else
                    {
                        InvCodeNum currentInvCode;
                        string invTypeCode;
                        string invNum;
                        Fpxx fpxx;
                        object[] objArray2;
                        object[] objArray3;
                        TaxCard card2;
                        if (invType == 2)
                        {
                            str32 = CommonTool.GetInfoFromNotes(result.BZ, 0);
                            str33 = str32.Substring(0, str32.Length - 8);
                            str34 = str32.Substring(str32.Length - 8, 8);
                            invoice.set_BlueFpdm(str33);
                            invoice.set_BlueFphm(str34);
                            currentInvCode = card.GetCurrentInvCode(2);
                            invTypeCode = currentInvCode.InvTypeCode;
                            invNum = currentInvCode.InvNum;
                            if (invTypeCode.Equals(str33) && invNum.Equals(str34))
                            {
                                return new object[] { false, invoice, "A304" };
                            }
                            fpxx = null;
                            objArray2 = new object[] { "c", str33, str34 };
                            objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPChanXunWenBenJieKouShareMethods", objArray2);
                            if (objArray3[0] == null)
                            {
                                str31 = this.CheckWebHZJE(result.JEHJ, str33, str34);
                                if (str31 != "0")
                                {
                                    if (str31 != "-0")
                                    {
                                        return new object[] { false, invoice, str31 };
                                    }
                                    card2 = TaxCardFactory.CreateTaxCard();
                                    if (card2.get_CardBeginDate().AddDays(180.0).CompareTo(card2.GetCardClock()) <= 0)
                                    {
                                    }
                                }
                            }
                            else
                            {
                                fpxx = (Fpxx) objArray3[0];
                                if (fpxx.zfbz)
                                {
                                    return new object[] { false, invoice, "A306" };
                                }
                                if (result.TYDH == "1")
                                {
                                    if (fpxx.zyfpLx != 8)
                                    {
                                        return new object[] { false, invoice, "A300" };
                                    }
                                }
                                else if (result.TYDH == "2")
                                {
                                    if (fpxx.zyfpLx != 9)
                                    {
                                        return new object[] { false, invoice, "A301" };
                                    }
                                }
                                else if ((((fpxx.zyfpLx != null) && (fpxx.zyfpLx != 8)) && (fpxx.zyfpLx != 10)) && (fpxx.zyfpLx != 11))
                                {
                                    return new object[] { false, invoice, "A302" };
                                }
                                str31 = this.CheckLocalHZJE(result.JEHJ, str33, str34, Convert.ToDouble(fpxx.je), fpxx.fplx);
                                if (str31 != "0000")
                                {
                                    return new object[] { false, invoice, str31 };
                                }
                            }
                        }
                        else if (invType == 11)
                        {
                            if (!card.get_QYLX().ISTLQY)
                            {
                                infoFromNotes = CommonTool.GetInfoFromNotes(result.BZ, 2);
                                invoice.set_RedNum(infoFromNotes);
                                str31 = this.CheckHZFPXXB(infoFromNotes, 11);
                                if (str31 != "0000")
                                {
                                    return new object[] { false, invoice, str31 };
                                }
                            }
                        }
                        else if (invType == 12)
                        {
                            str32 = CommonTool.GetInfoFromNotes(result.BZ, 0);
                            str33 = str32.Substring(0, str32.Length - 8);
                            str34 = str32.Substring(str32.Length - 8, 8);
                            invoice.set_BlueFpdm(str33);
                            invoice.set_BlueFphm(str34);
                            currentInvCode = card.GetCurrentInvCode(12);
                            invTypeCode = currentInvCode.InvTypeCode;
                            invNum = currentInvCode.InvNum;
                            if (invTypeCode.Equals(str33) && invNum.Equals(str34))
                            {
                                return new object[] { false, invoice, "A304" };
                            }
                            fpxx = null;
                            objArray2 = new object[] { "j", str33, str34 };
                            objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPChanXunWenBenJieKouShareMethods", objArray2);
                            if (objArray3[0] != null)
                            {
                                fpxx = (Fpxx) objArray3[0];
                                str31 = this.CheckLocalHZJE(Convert.ToDouble(invoice.GetHjJe()), str33, str34, Convert.ToDouble(fpxx.je), fpxx.fplx);
                                if (str31 != "0000")
                                {
                                    return new object[] { false, invoice, str31 };
                                }
                            }
                            else
                            {
                                card2 = TaxCardFactory.CreateTaxCard();
                                if (card2.get_CardBeginDate().AddDays(180.0).CompareTo(card2.GetCardClock()) <= 0)
                                {
                                }
                            }
                        }
                    }
                    if (flag2)
                    {
                    }
                }
                bool flag14 = false;
                if ((result.DJZL == "c") || (result.DJZL == "s"))
                {
                    flag14 = invoice.CheckFpData();
                }
                else if (result.DJZL == "f")
                {
                    flag14 = invoice.CheckFpData();
                }
                else if (result.DJZL == "j")
                {
                    flag14 = invoice.CheckFpData();
                }
                string code = invoice.GetCode();
                if (!flag14)
                {
                    if (!(result.DJZL == "c") || !(code == "A605"))
                    {
                        return new object[] { false, invoice, code };
                    }
                    flag10 = true;
                }
                if (flag10)
                {
                    result.KPZL = "清单开票";
                }
                if ((((result.DJZL == "c") || (result.DJZL == "s")) && flag2) && (result.JEHJ < 0.0))
                {
                    Invoice redInvoice = invoice.GetRedInvoice(false);
                    redInvoice.set_Bz(result.BZ);
                    if (result.DJZL == "s")
                    {
                        infoFromNotes = CommonTool.GetInfoFromNotes(result.BZ, 1);
                        redInvoice.set_RedNum(infoFromNotes);
                    }
                    else if (result.DJZL == "c")
                    {
                        str32 = CommonTool.GetInfoFromNotes(result.BZ, 0);
                        str33 = str32.Substring(0, str32.Length - 8);
                        str34 = str32.Substring(str32.Length - 8, 8);
                        redInvoice.set_BlueFpdm(str33);
                        redInvoice.set_BlueFphm(str34);
                    }
                    byte[] buffer8 = new byte[num4];
                    string[] strArray4 = new string[num5];
                    flag7 = false;
                    count = result.ListXSDJ_MX.Count;
                    sLV = 0.0;
                    for (num2 = 0; num2 < count; num2++)
                    {
                        if (num2 == 0)
                        {
                            sLV = result.ListXSDJ_MX[num2].SLV;
                        }
                        else if (!(sLV == result.ListXSDJ_MX[num2].SLV))
                        {
                            flag7 = true;
                            break;
                        }
                    }
                    strArray4[idxMir[0]] = "";
                    strArray4[idxMir[1]] = result.BH;
                    strArray4[idxMir[2]] = result.GFMC;
                    strArray4[idxMir[3]] = result.XFDZDH;
                    strArray4[idxMir[4]] = result.XFYHZH;
                    strArray4[idxMir[5]] = "";
                    strArray4[idxMir[6]] = result.BZ;
                    strArray4[idxMir[7]] = result.GFDZDH;
                    strArray4[idxMir[8]] = result.GFYHZH;
                    strArray4[idxMir[9]] = result.FHR;
                    strArray4[idxMir[10]] = Convert.ToString(result.KPSE);
                    strArray4[idxMir[11]] = result.SKR;
                    strArray4[idxMir[12]] = result.GFSH;
                    strArray4[idxMir[13]] = UserInfo.get_Yhmc();
                    if (!flag7)
                    {
                        strArray4[idxMir[14]] = Convert.ToString(result.SLV);
                    }
                    else
                    {
                        strArray4[idxMir[14]] = "";
                    }
                    strArray4[idxMir[15]] = Convert.ToString(result.KPJE);
                    if ((result.DJZL == "c") && (result.TYDH == "2"))
                    {
                        strArray4[idxMir[2]] = card.get_Corporation();
                        strArray4[idxMir[3]] = result.GFDZDH;
                        strArray4[idxMir[4]] = result.GFYHZH;
                        strArray4[idxMir[7]] = result.XFDZDH;
                        strArray4[idxMir[8]] = result.XFYHZH;
                        strArray4[idxMir[12]] = card.get_TaxCode();
                    }
                    byte[] buffer9 = redInvoice.get_RdmByte();
                    object[] objArray4 = new object[] { buffer8, strArray4 };
                    redInvoice.SetData(objArray4);
                    redInvoice.set_Bz(result.BZ);
                    flag6 = false;
                    str8 = card.get_TaxCode();
                    companyType = TaxCardFactory.CreateTaxCard().get_StateInfo().CompanyType;
                    if ((!string.IsNullOrEmpty(str8) && (str8.Length == 15)) && (str8.Substring(8, 2) == "DK"))
                    {
                        flag6 = true;
                    }
                    result.BZ = result.BZ.Replace("代开企业税号：", "代开企业税号:");
                    result.BZ = result.BZ.Replace("代开企业税号：", "代开企业税号:");
                    result.BZ = result.BZ.Replace("代开企业税号：", "代开企业税号:");
                    result.BZ = result.BZ.Replace("代开企业名称：", "代开企业名称:");
                    result.BZ = result.BZ.Replace("代开企业名称：", "代开企业名称:");
                    result.BZ = result.BZ.Replace("代开企业名称：", "代开企业名称:");
                    if (NotesUtil.GetDKQYFromInvNotes(result.BZ, ref str15, ref str14).Equals("0000"))
                    {
                        redInvoice.set_Dk_qymc(str14);
                        redInvoice.set_Dk_qysh(str15);
                    }
                    return new object[] { true, redInvoice, code };
                }
                objArray5 = new object[] { true, invoice, code };
            }
            catch (Exception exception)
            {
                HandleException.Log.Error(exception.ToString(), exception);
                throw;
            }
            return objArray5;
        }

        private string CheckLocalHZJE(double hjje, string fpdm, string fphm, double lzje, FPLX fplx)
        {
            object[] objArray = new object[] { fpdm, fphm };
            decimal num = (decimal) ServiceFactory.InvokePubService("Aisino.Fwkp.QueryYKRedJE", objArray)[0];
            double num2 = (double) num;
            num2 = Math.Abs(num2);
            hjje = Math.Abs(hjje);
            if ((fplx == 12) && (num2 != 0.0))
            {
                return "A309";
            }
            if (((lzje - num2) - hjje) < 0.0)
            {
                return "A303";
            }
            return "0000";
        }

        private string CheckWebHZJE(double hjje, string fpdm, string fphm)
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (!card.get_QYLX().ISTDQY)
            {
                double num3;
                string invControlNum = card.GetInvControlNum();
                if (string.IsNullOrEmpty(invControlNum))
                {
                    invControlNum = "";
                }
                string xml = "";
                int num = int.Parse(fphm);
                string str3 = new StringBuilder().Append("<?xml version=\"1.0\" encoding=\"GBK\"?>").Append("<FPXT><INPUT>").Append("<NSRSBH>").Append(card.get_TaxCode()).Append("</NSRSBH>").Append("<KPJH>").Append(card.get_Machine()).Append("</KPJH>").Append("<SBBH>").Append(invControlNum).Append("</SBBH>").Append("<LZFPDM>").Append(fpdm).Append("</LZFPDM>").Append("<LZFPHM>").Append(string.Format("{0:00000000}", num)).Append("</LZFPHM>").Append("<FPZL>").Append((FPLX) 2).Append("</FPZL>").Append("</INPUT></FPXT>").ToString();
                if (HttpsSender.SendMsg("0007", str3, ref xml) != 0)
                {
                    return "A308";
                }
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                string innerText = document.SelectSingleNode("/FPXT/OUTPUT").SelectSingleNode("HZJE").InnerText;
                double.TryParse(innerText, out num3);
                if (innerText.Equals("-0"))
                {
                    return "-0";
                }
                hjje = Math.Abs(hjje);
                if ((num3 <= 0.0) || (hjje.CompareTo(Math.Abs(num3)) > 0))
                {
                    return "A307";
                }
            }
            return "0";
        }

        public Invoice CreateInvoiceHead(FPGenerateResult result, int[] idxMir, ref string[] Value, SaleBill bill)
        {
            Invoice invoice2;
            try
            {
                InvType invType = CommonTool.GetInvType(result.DJZL);
                FPLX fplx = (FPLX) CommonTool.GetInvType(result.DJZL);
                bool flag = result.KPJE < 0.0;
                bool flag2 = false;
                if (TaxCardFactory.CreateTaxCard().get_StateInfo().CompanyType > 0)
                {
                    if (result.DJZL == "c")
                    {
                        InvSplitPara para = new InvSplitPara();
                        para.GetInvSplitPara(invType);
                        if (para.above7Generlist && (result.ListXSDJ_MX.Count > 7))
                        {
                            flag2 = true;
                            result.KPZL = "清单开票";
                        }
                        else if (para.below7Generlist && (result.ListXSDJ_MX.Count <= 7))
                        {
                            if (SaleBillCheck.Instance.IsOverEWM(bill))
                            {
                                flag2 = true;
                                result.KPZL = "清单开票";
                            }
                            else if (bill.QDHSPMC.Length > 0)
                            {
                                flag2 = true;
                                result.KPZL = "清单开票";
                            }
                            else
                            {
                                flag2 = false;
                                result.KPZL = "一般开票";
                            }
                        }
                    }
                }
                else if (result.ListXSDJ_MX.Count > 8)
                {
                    flag2 = true;
                    result.KPZL = "清单开票";
                }
                if (result.QDHSPMC.Trim().Length > 0)
                {
                    flag2 = true;
                    result.KPZL = "清单开票";
                }
                byte[] sourceArray = Invoice.get_TypeByte();
                byte[] destinationArray = new byte[0x20];
                Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                byte[] buffer4 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KT" + DateTime.Now.ToString("F")), destinationArray, buffer3);
                Invoice invoice = new Invoice(flag, flag2, false, fplx, buffer4, null);
                TaxCard card2 = TaxCardFactory.CreateTaxCard();
                if (string.IsNullOrEmpty(result.XFDZDH))
                {
                    string str = new StringBuilder().Append(card2.get_Address()).Append(" ").Append(card2.get_Telephone()).ToString();
                    result.XFDZDH = str;
                }
                if (string.IsNullOrEmpty(result.XFYHZH))
                {
                    bool flag5 = false;
                    string str2 = card2.get_TaxCode();
                    int companyType = TaxCardFactory.CreateTaxCard().get_StateInfo().CompanyType;
                    if ((!string.IsNullOrEmpty(str2) && (str2.Length == 15)) && (str2.Substring(8, 2) == "DK"))
                    {
                        flag5 = true;
                    }
                    if (!flag5)
                    {
                        string str3 = card2.get_BankAccount().Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                        result.XFYHZH = str3;
                    }
                    else if ((result.DJZL == "c") || (result.DJZL == "s"))
                    {
                        string str4;
                        string str5;
                        result.BZ = result.BZ.Replace("代开企业税号：", "代开企业税号:");
                        result.BZ = result.BZ.Replace("代开企业税号：", "代开企业税号:");
                        result.BZ = result.BZ.Replace("代开企业税号：", "代开企业税号:");
                        result.BZ = result.BZ.Replace("代开企业名称：", "代开企业名称:");
                        result.BZ = result.BZ.Replace("代开企业名称：", "代开企业名称:");
                        result.BZ = result.BZ.Replace("代开企业名称：", "代开企业名称:");
                        if (NotesUtil.GetDKQYFromInvNotes(result.BZ, ref str5, ref str4).Equals("0000"))
                        {
                            invoice.set_Dk_qymc(str4);
                            invoice.set_Dk_qysh(str5);
                        }
                    }
                }
                int num2 = 0x10;
                int num3 = 0x10;
                byte[] buffer5 = new byte[num2];
                string[] strArray = new string[num2];
                byte[] buffer6 = new byte[num3];
                Value[idxMir[0]] = "";
                Value[idxMir[1]] = result.BH;
                Value[idxMir[2]] = result.GFMC;
                Value[idxMir[3]] = result.XFDZDH;
                Value[idxMir[4]] = result.XFYHZH;
                Value[idxMir[5]] = "";
                Value[idxMir[6]] = result.BZ;
                Value[idxMir[7]] = result.GFDZDH;
                Value[idxMir[8]] = result.GFYHZH;
                Value[idxMir[9]] = result.FHR;
                Value[idxMir[10]] = Convert.ToString(result.KPSE);
                Value[idxMir[11]] = result.SKR;
                Value[idxMir[12]] = result.GFSH;
                Value[idxMir[13]] = UserInfo.get_Yhmc();
                Value[idxMir[14]] = Convert.ToString(result.SLV);
                Value[idxMir[15]] = Convert.ToString(result.KPJE);
                byte[] buffer7 = invoice.get_RdmByte();
                if (flag)
                {
                    if (fplx == 0)
                    {
                        string infoFromNotes = CommonTool.GetInfoFromNotes(result.BZ, 1);
                        invoice.set_RedNum(infoFromNotes);
                    }
                    else
                    {
                        string str8 = CommonTool.GetInfoFromNotes(result.BZ, 0);
                        invoice.set_BlueFpdm(str8.Substring(0, str8.Length - 8));
                        invoice.set_BlueFphm(str8.Substring(str8.Length - 8, 8));
                    }
                }
                bool flag6 = false;
                if (result.HYSY && (result.ListXSDJ_MX[0].SLV == 0.05))
                {
                    flag6 = true;
                }
                if (flag6)
                {
                    invoice.SetZyfpLx(1);
                }
                if (!(((!(result.DJZL == "s") && !(result.DJZL == "c")) || (result.ListXSDJ_MX[0].SLV != 0.05)) || flag6))
                {
                }
                if (((result.DJZL == "s") || (result.DJZL == "c")) && (result.ListXSDJ_MX[0].SLV == 0.015))
                {
                    invoice.SetZyfpLx(10);
                }
                if (result.DJZL == "c")
                {
                    if (result.TYDH == "1")
                    {
                        invoice.set_Xfsh(card2.get_TaxCode());
                        invoice.set_Xfmc(card2.get_Corporation());
                        invoice.SetZyfpLx(8);
                    }
                    else if (result.TYDH == "2")
                    {
                        invoice.set_Gfsh(card2.get_TaxCode());
                        invoice.set_Gfmc(card2.get_Corporation());
                        invoice.set_Xfsh(result.GFSH);
                        invoice.set_Xfmc(result.GFMC);
                        invoice.SetZyfpLx(9);
                    }
                    else
                    {
                        invoice.set_Xfsh(card2.get_TaxCode());
                        invoice.set_Xfmc(card2.get_Corporation());
                    }
                }
                if (((result.DJZL == "s") || (result.DJZL == "c")) && (Math.Abs(result.ListXSDJ_MX[0].KCE) > 1E-05))
                {
                    invoice.SetZyfpLx(11);
                }
                else
                {
                    invoice.set_Xfsh(card2.get_TaxCode());
                    invoice.set_Xfmc(card2.get_Corporation());
                }
                invoice2 = invoice;
            }
            catch
            {
                throw;
            }
            return invoice2;
        }

        private string GenerateInv(FPGenerateResult result, int[] idMix)
        {
            try
            {
                string messageInfo;
                object[] objArray = this.CheckInvoiceFF(result, idMix);
                if (!((bool) objArray[0]))
                {
                    string str = (string) objArray[2];
                    Invoice invoice = (Invoice) objArray[1];
                    if ((invoice.get_ZyfpLx() == 9) && ((str == "A067") || (str == "A131")))
                    {
                        str = "A057";
                    }
                    string[] @params = invoice.Params;
                    messageInfo = MessageManager.GetMessageInfo(str, @params);
                    result.KPJG = "开票失败";
                    result.SXYY = messageInfo.Replace("{0}", "");
                    return "0";
                }
                object[] objArray2 = new object[] { objArray[1] };
                object[] objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Invoice", objArray2);
                string str3 = Convert.ToString(objArray3[0]);
                if (str3 == "0000")
                {
                    result.KPJG = "成功开票";
                    result.FPDM = Convert.ToString(objArray3[1]);
                    result.FPHM = Convert.ToInt64(objArray3[2]);
                    this.fptkDAL.UpdateXSDJMX_FPDM_FPHM(result);
                    return "0";
                }
                messageInfo = MessageManager.GetMessageInfo(str3);
                result.KPJG = "开票失败";
                result.SXYY = messageInfo.Replace("{0}", "");
                if (str3.StartsWith("TCD_768") || str3.StartsWith("TCD_769"))
                {
                    FormMain.CallUpload();
                }
                return str3;
            }
            catch (Exception exception)
            {
                result.KPJG = "开票失败";
                result.SXYY = "异常" + exception.Message;
                this.log.Error(exception);
                return "0";
            }
        }

        public List<FPGenerateResult> GenerateInvForPreview(List<string> billsBH)
        {
            List<FPGenerateResult> list3;
            try
            {
                MessageHelper.MsgWait("正在校验单据，请稍候...");
                List<FPGenerateResult> list = new List<FPGenerateResult>();
                SaleBillCtrl instance = SaleBillCtrl.Instance;
                int[] slvjeseIndex = Instance.SetInvoiceTimes();
                TaxCard card = TaxCardFactory.CreateTaxCard();
                bool hzfw = card.get_StateInfo().CompanyType > 0;
                foreach (string str in billsBH)
                {
                    FPGenerateResult result;
                    SaleBill bill = instance.FindNotInv(str);
                    List<SaleBill> listBill = new List<SaleBill>();
                    string str3 = this.SetInvKPZL(bill, slvjeseIndex);
                    bool exEWMInfoSplit = str3 != "清单开票";
                    if ((bill.DJZL == "c") || (bill.DJZL == "s"))
                    {
                        bill.SeparateReason = "";
                        string reason = "";
                        string str5 = "";
                        bool flag3 = false;
                        string str6 = card.get_TaxCode();
                        int companyType = TaxCardFactory.CreateTaxCard().get_StateInfo().CompanyType;
                        if ((!string.IsNullOrEmpty(str6) && (str6.Length == 15)) && (str6.Substring(8, 2) == "DK"))
                        {
                            flag3 = true;
                        }
                        if (flag3)
                        {
                            bill.BZ = bill.BZ.Replace("代开企业税号：", "代开企业税号:");
                            bill.BZ = bill.BZ.Replace("代开企业税号：", "代开企业税号:");
                            bill.BZ = bill.BZ.Replace("代开企业税号：", "代开企业税号:");
                            bill.BZ = bill.BZ.Replace("代开企业名称：", "代开企业名称:");
                            bill.BZ = bill.BZ.Replace("代开企业名称：", "代开企业名称:");
                            bill.BZ = bill.BZ.Replace("代开企业名称：", "代开企业名称:");
                            if (string.IsNullOrEmpty(bill.XFYHZH) && (bill.DJZL == "s"))
                            {
                                str5 = "HasWrong";
                            }
                        }
                        if (!(CommonTool.isCEZS() || (Math.Abs(bill.ListGoods[0].KCE) <= 1E-05)))
                        {
                            str5 = "HasWrong";
                            bill.SeparateReason = "2016年5月1日 以后才能开具差额税发票";
                        }
                        if (!str5.StartsWith("HasWrong"))
                        {
                            str5 = instance.CheckBillRecordCF(bill, "AT", exEWMInfoSplit, ref reason, slvjeseIndex, hzfw, true);
                        }
                        if (!str5.StartsWith("HasWrong"))
                        {
                            if (str5 == "Need")
                            {
                                string str7 = instance.AutoSeparateBase(bill, SeparateType.KeepMaxJE, false, true, listBill, slvjeseIndex, true);
                                if (str7 == "-1")
                                {
                                    listBill.Clear();
                                    str5 = "HasWrong";
                                    bill.SeparateReason = "折扣组超过拆分限制，该单据不能拆分!";
                                }
                                else if (str7 == "-2")
                                {
                                    listBill.Clear();
                                    str5 = "HasWrong";
                                    bill.SeparateReason = "非清单发票商品名称中含有“清单”字样，不能开票";
                                }
                                else if (str7 == "-3")
                                {
                                    listBill.Clear();
                                    str5 = "HasWrong";
                                    bill.SeparateReason = "有单行商品行错误，无法拆分，该单据不能开票";
                                }
                                else if (str7 == "-4")
                                {
                                    listBill.Clear();
                                    str5 = "HasWrong";
                                    bill.SeparateReason = "金额超限";
                                }
                                else if (str7 == "-5")
                                {
                                    listBill.Clear();
                                    str5 = "HasWrong";
                                    bill.SeparateReason = "负数单据误差累计值大于1.27";
                                }
                                else if (str7.StartsWith("[-1]"))
                                {
                                    listBill.Clear();
                                    str5 = "HasWrong";
                                    string str8 = str7.Substring(4, str7.Length - 4);
                                    if (str8.Equals("A612"))
                                    {
                                        bill.SeparateReason = "商品名称超限，不允许生成发票";
                                    }
                                    else if (str8.Equals("A613"))
                                    {
                                        bill.SeparateReason = "计量单位超限，不允许生成发票";
                                    }
                                    else if (str8.Equals("A052"))
                                    {
                                        bill.SeparateReason = "稀土商品单价和数量为空，不允许生成发票";
                                    }
                                }
                            }
                            else if (str5 == "Needless")
                            {
                            }
                        }
                        if (listBill.Count == 0)
                        {
                            listBill.Add(bill);
                        }
                        int num2 = 0;
                        foreach (SaleBill bill2 in listBill)
                        {
                            result = new FPGenerateResult(bill2);
                            if (bill2.JEHJ == 0.0)
                            {
                                result.KKPX = "不能开票";
                                bill.SeparateReason = "金额合计为0";
                            }
                            else if (str5 == "Need")
                            {
                                result.KKPX = "部分开票";
                                result.YBH = str;
                            }
                            else if (str5.StartsWith("HasWrong"))
                            {
                                result.KKPX = "不能开票";
                            }
                            else
                            {
                                result.KKPX = "完整开票";
                            }
                            if (bill2.ListGoods.Count > 0)
                            {
                                result.SLV = bill2.ListGoods[0].SLV;
                            }
                            else
                            {
                                result.SXYY = "没有明细数据";
                                continue;
                            }
                            if (str3 == "清单开票")
                            {
                                if ((listBill[num2].QDHSPMC.Length == 0) && instance.IsCanKP(listBill[num2], slvjeseIndex))
                                {
                                    result.KPZL = "一般开票";
                                }
                                else
                                {
                                    result.KPZL = str3;
                                }
                            }
                            else
                            {
                                result.KPZL = str3;
                            }
                            result.SXYY = bill2.SeparateReason;
                            list.Add(result);
                            num2++;
                        }
                    }
                    else
                    {
                        CheckResult result2 = SaleBillCheck.Instance.CheckSaleBillBase(bill);
                        result = new FPGenerateResult(bill);
                        if (result2.HasWrong)
                        {
                            result.KKPX = "不能开票";
                        }
                        else
                        {
                            result.KKPX = "完整开票";
                        }
                        result.SLV = bill.SLV;
                        result.KPZL = str3;
                        result.SXYY = "";
                        if (result.DJZL == "j")
                        {
                            result.KPJE = bill.JEHJ;
                            result.KPSE = SaleBillCtrl.GetRound((double) (bill.JEHJ / (1.0 + bill.SLV)), 2);
                        }
                        list.Add(result);
                    }
                }
                list3 = list;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Thread.Sleep(100);
                MessageHelper.MsgWait();
            }
            return list3;
        }

        public List<FPGenerateResult> GenerateInvlist(List<FPGenerateResult> listGeneratePreview)
        {
            List<FPGenerateResult> list2;
            string message = "正在生成发票，请稍候...";
            try
            {
                MessageHelper.MsgWait("正在生成发票，请稍候...");
                List<FPGenerateResult> list = new List<FPGenerateResult>();
                if (listGeneratePreview.Count > 0)
                {
                    int[] numArray;
                    int num = listGeneratePreview.Count + 1;
                    int count = listGeneratePreview.Count;
                    if (count > 0)
                    {
                        numArray = this.SetInvoiceTimes(count, listGeneratePreview[0].DJZL);
                    }
                    else
                    {
                        numArray = this.SetInvoiceTimes(count);
                    }
                    HandleException.Log.Debug("正在生成发票,已经取得销售单据");
                    for (int i = 0; i < listGeneratePreview.Count; i++)
                    {
                        FPGenerateResult result = listGeneratePreview[i];
                        if (result.KKPX.Equals("不能开票"))
                        {
                            result.KPJG = "不能开票";
                        }
                        else
                        {
                            string str2 = this.GenerateInv(result, numArray);
                            list.Add(result);
                            if (result.KPJG == "开票失败")
                            {
                                message = string.Format("销售单据编号: {0}\n开票失败 未生成发票 发票号码:{1}", result.BH, result.FPHM.ToString("00000000"));
                            }
                            else
                            {
                                message = string.Format("销售单据编号: {0}\n已生成 发票号码:{1}", result.BH, result.FPHM.ToString("00000000"));
                            }
                            HandleException.Log.Debug(message);
                            if (result.KPJG == "开票失败")
                            {
                                if (str2.StartsWith("TCD") || ((result.SXYY != null) && result.SXYY.StartsWith("发票已用完")))
                                {
                                    break;
                                }
                            }
                            else
                            {
                                this.UpdateXSDJ_KPZT(result);
                            }
                        }
                    }
                }
                list2 = list;
            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("超时"))
                {
                    this.log.Error(exception.ToString());
                }
                else
                {
                    HandleException.HandleError(exception);
                }
                list2 = null;
            }
            finally
            {
                Thread.Sleep(100);
                MessageHelper.MsgWait();
            }
            return list2;
        }

        private string SetInvKPZL(SaleBill bill, int[] slvjeseIndex)
        {
            string str = "一般开票";
            if (bill.QDHSPMC.Trim().Length > 0)
            {
                return "清单开票";
            }
            bool flag = TaxCardFactory.CreateTaxCard().get_StateInfo().CompanyType > 0;
            InvSplitPara para = new InvSplitPara();
            para.GetInvSplitPara(bill.DJZL);
            int count = bill.ListGoods.Count;
            if (flag)
            {
                if (count > 7)
                {
                    if (bill.DJZL == "c")
                    {
                        if (para.above7Split)
                        {
                            bool flag2 = SaleBillCheck.Instance.IsOverEWM(bill);
                            return "一般开票";
                        }
                        if (para.above7Generlist)
                        {
                            str = "清单开票";
                        }
                        if (para.above7ForbiddenInv)
                        {
                        }
                        return str;
                    }
                    if (bill.DJZL == "s")
                    {
                        return "一般开票";
                    }
                    return str;
                }
                if (bill.DJZL == "c")
                {
                    if (para.below7Split)
                    {
                        if (SaleBillCheck.Instance.IsOverEWM(bill))
                        {
                            return "一般开票";
                        }
                        str = "一般开票";
                    }
                    if (para.below7Generlist)
                    {
                        if (SaleBillCheck.Instance.IsOverEWM(bill))
                        {
                            return "清单开票";
                        }
                        str = "一般开票";
                    }
                    if (para.below7ForbiddenInv)
                    {
                    }
                    return str;
                }
                if (bill.DJZL == "s")
                {
                    str = "一般开票";
                }
                return str;
            }
            if ((bill.DJZL == "c") || (bill.DJZL == "s"))
            {
                if (count > 8)
                {
                    str = "清单开票";
                }
                else
                {
                    str = "一般开票";
                }
            }
            return str;
        }

        public int[] SetInvoiceTimes()
        {
            int regTimes = 0xf4240;
            return this.SetInvoiceTimes(regTimes);
        }

        public int[] SetInvoiceTimes(int regTimes)
        {
            int[] numArray4;
            try
            {
                int num5;
                int num6;
                TaxCard card = TaxCardFactory.CreateTaxCard();
                int[] numArray = new int[0x10];
                regTimes = (regTimes + 1) * 2;
                FPLX fplx = 2;
                if (card.get_QYLX().ISPTFP)
                {
                    fplx = 2;
                }
                else if (card.get_QYLX().ISZYFP)
                {
                    fplx = 0;
                }
                else if (card.get_QYLX().ISHY)
                {
                    fplx = 11;
                }
                else if (card.get_QYLX().ISJDC)
                {
                    fplx = 12;
                }
                bool flag = false;
                bool flag2 = false;
                byte[] sourceArray = Invoice.get_TypeByte();
                byte[] destinationArray = new byte[0x20];
                Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                byte[] buffer4 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KT" + DateTime.Now.ToString("F")), destinationArray, buffer3);
                Invoice invoice = new Invoice(flag, flag2, false, fplx, buffer4, null);
                int num = 0x11;
                int num2 = 0x10;
                byte[] buffer5 = new byte[num];
                string[] strArray = new string[num];
                byte[] transfer = new byte[num2];
                string[] strArray2 = new string[num2];
                int[] numArray2 = new int[num2];
                int[] numArray3 = new int[num2];
                int maxValue = num2;
                Random random = new Random();
                for (num5 = 0; num5 < num2; num5++)
                {
                    numArray2[num5] = num5;
                }
                for (num6 = 0; num6 < num2; num6++)
                {
                    int index = random.Next(maxValue);
                    numArray3[num6] = numArray2[index];
                    numArray2[index] = numArray2[maxValue - 1];
                    maxValue--;
                }
                for (num6 = 0; num6 < num2; num6++)
                {
                    strArray2[num6] = "0";
                }
                for (num5 = 0; num5 < num2; num5++)
                {
                    buffer5[num5] = Convert.ToByte(numArray3[num5]);
                    strArray[num5] = strArray2[numArray3[num5]];
                    switch (numArray3[num5])
                    {
                        case 0:
                            numArray[0] = num5;
                            break;

                        case 1:
                            numArray[1] = num5;
                            break;

                        case 2:
                            numArray[2] = num5;
                            break;

                        case 3:
                            numArray[3] = num5;
                            break;

                        case 4:
                            numArray[4] = num5;
                            break;

                        case 5:
                            numArray[5] = num5;
                            break;

                        case 6:
                            numArray[6] = num5;
                            break;

                        case 7:
                            numArray[7] = num5;
                            break;

                        case 8:
                            numArray[8] = num5;
                            break;

                        case 9:
                            numArray[9] = num5;
                            break;

                        case 10:
                            numArray[10] = num5;
                            break;

                        case 11:
                            numArray[11] = num5;
                            break;

                        case 12:
                            numArray[12] = num5;
                            break;

                        case 13:
                            numArray[13] = num5;
                            break;

                        case 14:
                            numArray[14] = num5;
                            break;

                        case 15:
                            numArray[15] = num5;
                            break;
                    }
                }
                byte[] buffer7 = invoice.get_RdmByte();
                for (num5 = 0; num5 < num2; num5++)
                {
                    buffer5[num5] = Convert.ToByte((int) (buffer5[num5] ^ buffer7[num5]));
                }
                RegFileInfo regFileInfo = RegisterManager.GetRegFileInfo("JI");
                if (regFileInfo == null)
                {
                    MessageManager.ShowMsgBox("注册文件校验失败");
                    return null;
                }
                transfer = regFileInfo.get_FileContent().Transfer;
                for (num5 = 0; num5 < num2; num5++)
                {
                    buffer5[num5] = Convert.ToByte((int) (buffer5[num5] ^ transfer[num5]));
                }
                buffer5[0x10] = 0x7f;
                strArray[0x10] = regTimes.ToString();
                object[] objArray = new object[] { buffer5, strArray };
                invoice.SetData(objArray);
                if (!invoice.CheckFpData())
                {
                    string code = invoice.GetCode();
                }
                numArray4 = numArray;
            }
            catch (Exception exception)
            {
                HandleException.Log.Error(exception.ToString());
                throw;
            }
            return numArray4;
        }

        public int[] SetInvoiceTimes(int regTimes, string DJZL)
        {
            int[] numArray4;
            try
            {
                int num5;
                int num6;
                int[] numArray = new int[0x10];
                regTimes = (regTimes + 1) * 2;
                FPLX fplx = 2;
                if (DJZL == "c")
                {
                    fplx = 2;
                }
                else if (DJZL == "s")
                {
                    fplx = 0;
                }
                else if (DJZL == "f")
                {
                    fplx = 11;
                }
                else if (DJZL == "j")
                {
                    fplx = 12;
                }
                bool flag = false;
                bool flag2 = false;
                byte[] sourceArray = Invoice.get_TypeByte();
                byte[] destinationArray = new byte[0x20];
                Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                byte[] buffer4 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KT" + DateTime.Now.ToString("F")), destinationArray, buffer3);
                Invoice invoice = new Invoice(flag, flag2, false, fplx, buffer4, null);
                int num = 0x11;
                int num2 = 0x10;
                byte[] buffer5 = new byte[num];
                string[] strArray = new string[num];
                byte[] transfer = new byte[num2];
                string[] strArray2 = new string[num2];
                int[] numArray2 = new int[num2];
                int[] numArray3 = new int[num2];
                int maxValue = num2;
                Random random = new Random();
                for (num5 = 0; num5 < num2; num5++)
                {
                    numArray2[num5] = num5;
                }
                for (num6 = 0; num6 < num2; num6++)
                {
                    int index = random.Next(maxValue);
                    numArray3[num6] = numArray2[index];
                    numArray2[index] = numArray2[maxValue - 1];
                    maxValue--;
                }
                for (num6 = 0; num6 < num2; num6++)
                {
                    strArray2[num6] = "0";
                }
                for (num5 = 0; num5 < num2; num5++)
                {
                    buffer5[num5] = Convert.ToByte(numArray3[num5]);
                    strArray[num5] = strArray2[numArray3[num5]];
                    switch (numArray3[num5])
                    {
                        case 0:
                            numArray[0] = num5;
                            break;

                        case 1:
                            numArray[1] = num5;
                            break;

                        case 2:
                            numArray[2] = num5;
                            break;

                        case 3:
                            numArray[3] = num5;
                            break;

                        case 4:
                            numArray[4] = num5;
                            break;

                        case 5:
                            numArray[5] = num5;
                            break;

                        case 6:
                            numArray[6] = num5;
                            break;

                        case 7:
                            numArray[7] = num5;
                            break;

                        case 8:
                            numArray[8] = num5;
                            break;

                        case 9:
                            numArray[9] = num5;
                            break;

                        case 10:
                            numArray[10] = num5;
                            break;

                        case 11:
                            numArray[11] = num5;
                            break;

                        case 12:
                            numArray[12] = num5;
                            break;

                        case 13:
                            numArray[13] = num5;
                            break;

                        case 14:
                            numArray[14] = num5;
                            break;

                        case 15:
                            numArray[15] = num5;
                            break;
                    }
                }
                byte[] buffer7 = invoice.get_RdmByte();
                for (num5 = 0; num5 < num2; num5++)
                {
                    buffer5[num5] = Convert.ToByte((int) (buffer5[num5] ^ buffer7[num5]));
                }
                RegFileInfo regFileInfo = RegisterManager.GetRegFileInfo("JI");
                if (regFileInfo == null)
                {
                    MessageManager.ShowMsgBox("注册文件校验失败");
                    return null;
                }
                transfer = regFileInfo.get_FileContent().Transfer;
                for (num5 = 0; num5 < num2; num5++)
                {
                    buffer5[num5] = Convert.ToByte((int) (buffer5[num5] ^ transfer[num5]));
                }
                buffer5[0x10] = 0x7f;
                strArray[0x10] = regTimes.ToString();
                object[] objArray = new object[] { buffer5, strArray };
                invoice.SetData(objArray);
                if (!invoice.CheckFpData())
                {
                    string code = invoice.GetCode();
                }
                numArray4 = numArray;
            }
            catch (Exception exception)
            {
                HandleException.Log.Error(exception.ToString());
                throw;
            }
            return numArray4;
        }

        private void UpdateXSDJ_KPZT(FPGenerateResult result)
        {
            if (result.KKPX == "完整开票")
            {
                this.fptkDAL.UpdateXSDJ_KPZT(result.BH, "A");
            }
            else if (result.KKPX == "部分开票")
            {
                string bH = string.IsNullOrEmpty(result.YBH) ? result.BH : result.YBH;
                if (SaleBillCtrl.Instance.FindNotInv(bH).ListGoods.Count > 0)
                {
                    this.fptkDAL.UpdateXSDJ_KPZT(bH, "P");
                }
                else
                {
                    this.fptkDAL.UpdateXSDJ_KPZT(bH, "A");
                }
            }
        }

        public static GenerateInvoice Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GenerateInvoice();
                }
                return instance;
            }
        }
    }
}

