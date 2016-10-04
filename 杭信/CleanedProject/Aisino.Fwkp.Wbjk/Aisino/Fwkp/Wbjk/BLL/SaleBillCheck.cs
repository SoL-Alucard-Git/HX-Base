namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Https;
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
    using log4net.Config;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class SaleBillCheck : TaxCardValue
    {
        private bool blQDFW = TaxCardValue.taxCard.blQDEWM();
        private SaleBillDAL dbBill = new SaleBillDAL();
        private bool Flag_Hzfw = (TaxCardValue.taxCard.get_StateInfo().CompanyType != 0);
        private static SaleBillCheck instance = null;
        private InvoiceHandler invoiceHandler = new InvoiceHandler();
        private Invoice InvoiceKPTemp;
        private bool isDebug;
        private ILog loger = LogUtil.GetLogger<SaleBillCheck>();
        private string SellerName = TaxCardValue.taxCard.get_Corporation();
        private string VersionName;
        private List<string> xtgoodsList = new List<string>();

        public SaleBillCheck()
        {
            XmlConfigurator.Configure(new FileInfo("InvoiceInDownload.dll.config"));
            this.isDebug = false;
        }

        private void AddCheckResult(CheckResult result)
        {
            if ((result.listErrorMX.Count > 0) || (result.listErrorDJ.Count > 0))
            {
                if (result.listErrorDJ.Count > 0)
                {
                    foreach (string str in result.listErrorDJ)
                    {
                        if (!str.StartsWith("CF:"))
                        {
                            result.HasWrong = true;
                            break;
                        }
                    }
                }
                if (!result.HasWrong && (result.listErrorMX.Count > 0))
                {
                    foreach (string str in result.listErrorMX)
                    {
                        if (!str.StartsWith("CF:"))
                        {
                            result.HasWrong = true;
                            break;
                        }
                    }
                }
            }
        }

        public bool BillHasMultiSlv(SaleBill bill)
        {
            Goods goods;
            double sLV = 0.0;
            if (bill.ListGoods.Count > 0)
            {
                goods = bill.ListGoods[0];
                sLV = goods.SLV;
            }
            for (int i = 0; i < bill.ListGoods.Count; i++)
            {
                goods = bill.ListGoods[i];
                if (!(sLV == goods.SLV))
                {
                    return true;
                }
            }
            return false;
        }

        private Fpxx ChangeToFoxx(SaleBill bill, int StartXH, int RowCount)
        {
            FPLX fplx = (bill.DJZL == "s") ? ((FPLX) 0) : ((FPLX) 2);
            Fpxx fpxx = new Fpxx(fplx, "0000000000", "00000000") {
                xsdjbh = bill.BH,
                bz = bill.BZ,
                gfdzdh = bill.GFDZDH,
                gfmc = bill.GFMC,
                gfsh = bill.GFSH,
                gfyhzh = bill.GFYHZH,
                xfsh = TaxCardValue.taxCard.get_TaxCode(),
                xfmc = TaxCardValue.taxCard.get_Corporation()
            };
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (string.IsNullOrEmpty(bill.XFDZDH))
            {
                string str = new StringBuilder().Append(card.get_Address()).Append(" ").Append(card.get_Telephone()).ToString();
                fpxx.xfdzdh = str;
            }
            else
            {
                fpxx.xfdzdh = bill.XFDZDH;
            }
            if (string.IsNullOrEmpty(bill.XFYHZH))
            {
                string str2 = card.get_BankAccount().Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                fpxx.xfyhzh = str2;
            }
            else
            {
                fpxx.xfyhzh = bill.XFYHZH;
            }
            fpxx.hzfw = TaxCardValue.taxCard.get_StateInfo().CompanyType > 0;
            fpxx.kpr = UserInfo.get_Yhmc();
            fpxx.skr = bill.SKR;
            fpxx.fhr = bill.FHR;
            List<Dictionary<SPXX, string>> list = new List<Dictionary<SPXX, string>>();
            double num = 0.0;
            double num2 = 0.0;
            double sLV = 0.0;
            bool flag = false;
            for (int i = StartXH; i < RowCount; i++)
            {
                if (i >= bill.ListGoods.Count)
                {
                    break;
                }
                Goods goods = bill.ListGoods[i];
                if ((i != StartXH) && !(goods.SLV == sLV))
                {
                    flag = true;
                }
                sLV = goods.SLV;
                Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                item.Add(10, goods.DJHXZ.ToString());
                item.Add(7, goods.JE.ToString());
                item.Add(8, goods.SLV.ToString());
                item.Add(9, goods.SE.ToString());
                item.Add(0, goods.SPMC);
                item.Add(2, goods.SPSM);
                item.Add(11, "0");
                string str3 = (goods.DJ == 0.0) ? "" : goods.DJ.ToString();
                item.Add(5, str3);
                item.Add(3, goods.GGXH);
                item.Add(4, goods.JLDW);
                string str4 = (goods.SL == 0.0) ? "" : goods.SL.ToString();
                item.Add(6, str4);
                list.Add(item);
                num += goods.JE;
                num2 += goods.SE;
            }
            if (flag)
            {
                fpxx.sLv = "";
            }
            else
            {
                fpxx.sLv = bill.ListGoods[0].SLV.ToString();
            }
            fpxx.je = num.ToString();
            fpxx.se = num2.ToString();
            fpxx.Mxxx = list;
            return fpxx;
        }

        public bool CheckCEZS(SaleBill bill)
        {
            foreach (Goods goods in bill.ListGoods)
            {
                if (!(goods.KCE == 0.0))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckFLBM_HZX(string flbm)
        {
            SaleBillDAL ldal = new SaleBillDAL();
            return (ldal.GET_FLBM_HZX(flbm).ToUpper() == "Y");
        }

        private bool CheckFLBM_KYZT(string flbm)
        {
            SaleBillDAL ldal = new SaleBillDAL();
            return (ldal.GET_FLBM_KYZT(flbm).ToUpper() == "Y");
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

        internal string CheckHZFWBill(SaleBill bill, int StartXH, int RowCount)
        {
            if ((RowCount != 0) && (bill.ListGoods.Count != 0))
            {
                Fpxx fpxx = this.ChangeToFoxx(bill, StartXH, RowCount);
                if (!this.invoiceHandler.CheckHzfwFpxxForWBJK(fpxx))
                {
                    return this.invoiceHandler.GetCode();
                }
            }
            return "0000";
        }

        private string CheckLocalHZJE(double hjje, string fpdm, string fphm, double lzje, FPLX fplx)
        {
            if (hjje > 0.0)
            {
                return "E003";
            }
            object[] objArray = new object[] { fpdm, fphm };
            decimal num = (decimal) ServiceFactory.InvokePubService("Aisino.Fwkp.QueryYKRedJE", objArray)[0];
            double num2 = (double) num;
            num2 = Math.Abs(num2);
            double num3 = Math.Abs(hjje);
            if ((fplx == 12) && (num2 != 0.0))
            {
                return "A309";
            }
            if (lzje < 0.0)
            {
                return "E001";
            }
            if (num3 > lzje)
            {
                return "E002";
            }
            if (((lzje - num2) - num3) < 0.0)
            {
                return "A303";
            }
            return "0000";
        }

        public CheckResult CheckSaleBillBase(SaleBill bill)
        {
            TaxCard card;
            string infoFromNotes;
            string str6;
            string str7;
            string str8;
            string str9;
            Fpxx fpxx;
            object[] objArray;
            object[] objArray2;
            int num12;
            string str14;
            object[] objArray5;
            CheckResult result = new CheckResult();
            int num = 0;
            DateTime now = DateTime.Now;
            InvType invType = CommonTool.GetInvType(bill.DJZL);
            if ((invType != InvType.Common) && (invType != InvType.Special))
            {
                int count;
                switch (invType)
                {
                    case InvType.transportation:
                        card = TaxCardFactory.CreateTaxCard();
                        if (bill.GFMC.Length == 0)
                        {
                            result.AddErrorDJ("实际受票方名称为空，请重新录入\n");
                        }
                        if (bill.GFSH.Length == 0)
                        {
                            result.AddErrorDJ("实际受票方税号为空，请重新录入\n");
                        }
                        if (bill.GFDZDH.Length == 0)
                        {
                            result.AddErrorDJ("收货人名称为空，请重新录入\n");
                        }
                        if (bill.CM.Length == 0)
                        {
                            result.AddErrorDJ("收货人税号为空，请重新录入\n");
                        }
                        if (bill.XFDZDH.Length == 0)
                        {
                            result.AddErrorDJ("发货人名称为空，请重新录入\n");
                        }
                        if (bill.TYDH.Length == 0)
                        {
                            result.AddErrorDJ("发货人税号为空，请重新录入\n");
                        }
                        if (bill.BZ.Contains("\x00a4") || bill.BZ.Contains("￠\x00a7"))
                        {
                        }
                        if (!this.CheckTaxRate(invType, false, bill.SLV, false, ""))
                        {
                            result.AddErrorDJ("税率非法，请重新录入\n");
                        }
                        else if (Math.Abs((double) (bill.SLV - 0.015)) < base.DataPrecision)
                        {
                            str14 = Convert.ToString((double) (bill.SLV * 100.0)) + "%";
                            result.AddErrorDJ(str14 + " 税率非法，请重新录入\n");
                        }
                        if (!this.CheckTaxCode(invType, bill.GFSH, false, 11))
                        {
                            result.AddErrorDJ("实际受票方税号校验不通过，请重新录入\n");
                        }
                        if (!this.CheckTaxCode(invType, bill.CM, false, 11))
                        {
                            result.AddErrorDJ("收货人税号校验不通过，请重新录入\n");
                        }
                        if (!this.CheckTaxCode(invType, bill.TYDH, false, 11))
                        {
                            result.AddErrorDJ("发货人税号校验不通过，请重新录入\n");
                        }
                        count = card.get_SQInfo().PZSQType.Count;
                        for (num12 = 0; num12 < count; num12++)
                        {
                            if (card.get_SQInfo().PZSQType[num12].invType == 11)
                            {
                                base.InvLimit = card.get_SQInfo().PZSQType[num12].InvAmountLimit;
                            }
                        }
                        if (Math.Abs(bill.JEHJ) > base.InvLimit)
                        {
                            result.AddErrorDJ("金额超过金税设备允许范围，请重新录入\n");
                        }
                        if (bill.JEHJ == 0.0)
                        {
                            result.AddErrorDJ("金额合计不能为0\n");
                        }
                        if (bill.ListGoods.Count == 0)
                        {
                            result.AddErrorDJ("没有费用项目明细\n");
                        }
                        if (bill.JEHJ < 0.0)
                        {
                            infoFromNotes = CommonTool.GetInfoFromNotes(bill.BZ, 2);
                            if (!card.get_QYLX().ISTLQY)
                            {
                                if (!this.PosiInvTypeNo(bill.BZ, invType))
                                {
                                    if ((infoFromNotes.Length > 0) && infoFromNotes.Substring(0, 6).Equals("000000"))
                                    {
                                        result.AddErrorDJ("红字发票信息表编号不正确，不允许生成发票\n");
                                    }
                                    else
                                    {
                                        result.AddErrorDJ("负数单据没有16位信息表编号，备注首行必须为“开具红字货物运输业增值税专用发票信息表编号XXXXXXXXXXXXXXXX”，其中“X”为数字\n");
                                    }
                                }
                                else if (this.CheckHZFPXXB(infoFromNotes, 11) != "0000")
                                {
                                    result.AddErrorDJ("该红字发票信息表编号已使用过\n");
                                }
                            }
                        }
                        if (bill.ListGoods.Count > 0x12)
                        {
                            result.AddErrorDJ("单据明细超过18行\n");
                        }
                        foreach (Goods goods in bill.ListGoods)
                        {
                            if (goods.JE == 0.0)
                            {
                                result.AddErrorMX(string.Format("第{0}行，金额为0\n", goods.XH));
                            }
                            if ((bill.JEHJ < 0.0) && (goods.JE > 0.0))
                            {
                                result.AddErrorMX(string.Format("第{0}行，负数单据金额为正数\n", goods.XH));
                            }
                            if ((bill.JEHJ > 0.0) && (goods.JE < 0.0))
                            {
                                result.AddErrorMX(string.Format("第{0}行，正数单据金额为负数\n", goods.XH));
                            }
                            if (CommonTool.isSPBMVersion())
                            {
                                if (goods.FLBM.Trim() == "")
                                {
                                    result.AddErrorMX(string.Format("第{0}组费用项目 没有对应的分类编码\n", goods.XH));
                                }
                                else
                                {
                                    if (!this.CheckFLBM_KYZT(goods.FLBM.Trim()))
                                    {
                                        result.AddErrorMX(string.Format("第{0}组费用项目 对应的商品分类编码处于无效状态\n", goods.XH));
                                    }
                                    if (this.CheckFLBM_HZX(goods.FLBM.Trim()))
                                    {
                                        result.AddErrorMX(string.Format("第{0}组费用项目 对应的商品分类编码是汇总项\n", goods.XH));
                                    }
                                    objArray5 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", new object[] { goods.FLBM.Trim(), false });
                                    if ((objArray5 != null) && !bool.Parse(objArray5[0].ToString()))
                                    {
                                        result.AddErrorMX(string.Format("当前企业没有所选税收分类编码授权\n", goods.XH));
                                    }
                                }
                            }
                            if ((bill.ListGoods.Count > 10) && (ToolUtil.GetByteCount(goods.SPMC) > 12))
                            {
                                result.AddErrorMX(string.Format("第{0}组费用项目 费用项目超过10行，费用项目名称不能超过12个字符\n", goods.XH));
                            }
                        }
                        break;

                    case InvType.vehiclesales:
                    {
                        card = TaxCardFactory.CreateTaxCard();
                        if (bill.GFMC.Length == 0)
                        {
                            result.AddErrorDJ("购货单位(人)为空，请重新录入\n");
                        }
                        if (bill.GFSH.Length == 0)
                        {
                            result.AddErrorDJ("身份证号码/组织机构代码为空，请重新录入\n");
                        }
                        if (bill.GFDZDH.Length == 0)
                        {
                            result.AddErrorDJ("车辆类型为空，请重新录入\n");
                        }
                        if (bill.XFDZ.Length == 0)
                        {
                            result.AddErrorDJ("厂牌型号为空，请重新录入\n");
                        }
                        if (bill.KHYHMC.Length == 0)
                        {
                            result.AddErrorDJ("产地为空，请重新录入\n");
                        }
                        if (bill.BZ.Contains("\x00a4") || bill.BZ.Contains("￠\x00a7"))
                        {
                        }
                        if (!this.CheckTaxRate(invType, false, bill.SLV, bill.JDC_XSYH, bill.JDC_CLBM))
                        {
                            result.AddErrorDJ("税率非法，请重新录入\n");
                        }
                        else if (Math.Abs((double) (bill.SLV - 0.015)) < base.DataPrecision)
                        {
                            str14 = Convert.ToString((double) (bill.SLV * 100.0)) + "%";
                            result.AddErrorDJ(str14 + " 税率非法，请重新录入\n");
                        }
                        if (bill.JEHJ == 0.0)
                        {
                            result.AddErrorDJ("金额不能为0，请重新录入\n");
                        }
                        if ((bill.GFYHZH.Length > 0) && !this.CheckTaxCode(invType, bill.GFYHZH, false, 12))
                        {
                            result.AddErrorDJ("纳税人识别号校验不通过，请重新录入\n");
                        }
                        count = card.get_SQInfo().PZSQType.Count;
                        for (num12 = 0; num12 < count; num12++)
                        {
                            if (card.get_SQInfo().PZSQType[num12].invType == 12)
                            {
                                base.InvLimit = card.get_SQInfo().PZSQType[num12].InvAmountLimit;
                            }
                        }
                        if (Math.Abs(bill.JEHJ) > base.InvLimit)
                        {
                            result.AddErrorDJ("金额超过金税设备允许范围，请重新录入\n");
                        }
                        if (bill.JEHJ < 0.0)
                        {
                            if (!this.PosiInvTypeNo(bill.BZ, invType))
                            {
                                result.AddErrorDJ("负数单据没有对应的正数发票代码、号码，备注首行必须为“对应正数发票代码:XXXXXXXXXXXX号码:YYYYYYYY”，其中“X”、“Y”均为数字\n");
                            }
                            else
                            {
                                str7 = CommonTool.GetInfoFromNotes(bill.BZ, 0);
                                str8 = str7.Substring(0, str7.Length - 8);
                                str9 = str7.Substring(str7.Length - 8, 8);
                                fpxx = null;
                                objArray = new object[] { "j", str8, str9 };
                                objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPChanXunWenBenJieKouShareMethods", objArray);
                                if (objArray2[0] != null)
                                {
                                    fpxx = (Fpxx) objArray2[0];
                                    str6 = this.CheckLocalHZJE(SaleBillCtrl.GetRound((double) (bill.JEHJ / (1.0 + bill.SLV)), 2), str8, str9, Convert.ToDouble(fpxx.je), fpxx.fplx);
                                    if (str6 != "0000")
                                    {
                                        if (str6 == "A303")
                                        {
                                            result.AddErrorDJ("金额超限\n");
                                        }
                                        else if (str6 == "A309")
                                        {
                                            result.AddErrorDJ("对应的蓝字发票已开过红字发票\n");
                                        }
                                    }
                                }
                                else if (card.get_CardBeginDate().AddDays(180.0).CompareTo(card.GetCardClock()) <= 0)
                                {
                                }
                            }
                        }
                        byte[] buffer = null;
                        Invoice invoice = new Invoice(false, false, false, 12, buffer, null);
                        string xHD = bill.XHD;
                        if (xHD.Length == 0)
                        {
                            result.AddErrorDJ("车辆识别代号/车架号码为空，请重新录入\n");
                        }
                        else
                        {
                            invoice.set_Clsbdh_cjhm(xHD);
                            if (invoice.get_Clsbdh_cjhm().Length == 0)
                            {
                                result.AddErrorDJ("车辆识别代号/车架号码含有非法字符，请重新录入\n");
                            }
                        }
                        if (CommonTool.isSPBMVersion())
                        {
                            if (bill.JDC_FLBM.Trim() == "")
                            {
                                result.AddErrorMX(string.Format("厂牌型号没有对应的分类编码\n", new object[0]));
                            }
                            else
                            {
                                if (!this.CheckFLBM_KYZT(bill.JDC_FLBM.Trim()))
                                {
                                    result.AddErrorDJ(string.Format("车辆类型对应的商品分类编码处于无效状态\n", new object[0]));
                                }
                                if (this.CheckFLBM_HZX(bill.JDC_FLBM.Trim()))
                                {
                                    result.AddErrorDJ(string.Format("车辆类型对应的商品分类编码是汇总项\n", new object[0]));
                                }
                                objArray5 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", new object[] { bill.JDC_FLBM.Trim(), false });
                                if ((objArray5 != null) && !bool.Parse(objArray5[0].ToString()))
                                {
                                    result.AddErrorDJ(string.Format("当前企业没有所选税收分类编码授权\n", new object[0]));
                                }
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                TimeSpan span;
                base.InvLimit = TaxCardValue.GetInvLimit(invType);
                double jEHJ = bill.JEHJ;
                bool hYSY = bill.HYSY;
                card = TaxCardFactory.CreateTaxCard();
                if ((invType == InvType.Common) && !card.get_QYLX().ISPTFP)
                {
                    result.AddErrorDJ("无增值税普通发票授权\n");
                    this.AddCheckResult(result);
                    result.Kpzt = "所有数据未开票";
                    return result;
                }
                if ((invType == InvType.Special) && !card.get_QYLX().ISZYFP)
                {
                    result.AddErrorDJ("无增值税专用发票授权\n");
                    this.AddCheckResult(result);
                    result.Kpzt = "所有数据未开票";
                    return result;
                }
                bool iSNCPXS = card.get_QYLX().ISNCPXS;
                bool iSNCPSG = card.get_QYLX().ISNCPSG;
                if (bill.TYDH.Trim().Length > 0)
                {
                    if (bill.TYDH == "1")
                    {
                        if (!iSNCPXS)
                        {
                            result.AddErrorDJ("农产品销售没有授权\n");
                        }
                    }
                    else if (bill.TYDH == "2")
                    {
                        if (!iSNCPSG)
                        {
                            result.AddErrorDJ("农产品收购没有授权\n");
                        }
                    }
                    else
                    {
                        result.AddErrorDJ("农产品授权错误\n");
                    }
                }
                if (invType == InvType.Special)
                {
                    if ((bill.GFSH.Length == 0) || (bill.GFMC.Length == 0))
                    {
                        result.AddErrorDJ("购方信息不能为空\n");
                    }
                }
                else if (invType == InvType.Common)
                {
                    if (bill.TYDH == "2")
                    {
                        if (bill.KHYHMC.Length == 0)
                        {
                            result.AddErrorDJ("销方名称不能为空\n");
                        }
                    }
                    else if (bill.GFMC.Length == 0)
                    {
                        result.AddErrorDJ("购方名称不能为空\n");
                    }
                }
                if (this.isDebug)
                {
                    span = (TimeSpan) (DateTime.Now - now);
                    this.loger.Debug("processAA2-- 购方销方名称校验  = " + span.ToString());
                    now = DateTime.Now;
                }
                if (SaleBillCtrl.Instance.IsSWDK())
                {
                    string str;
                    string str2;
                    if (string.IsNullOrEmpty(bill.XFYHZH.Trim()) && (bill.DJZL == "s"))
                    {
                        result.AddErrorDJ("完税凭证号(销售方 开户银行及账号)不能为空\n");
                    }
                    if (string.IsNullOrEmpty(bill.XFDZDH.Trim()))
                    {
                        result.AddErrorDJ("代开企业地址电话（销售方 地址、电话）不能为空\n");
                    }
                    if (bill.HYSY)
                    {
                        result.AddErrorDJ("代开企业不能开具中外合作油气田单据\n");
                    }
                    if (NotesUtil.GetDKQYFromInvNotes(bill.BZ.Trim().Replace("代开企业税号：", "代开企业税号:").Replace("代开企业税号：", "代开企业税号:").Replace("代开企业税号：", "代开企业税号:").Replace("代开企业名称：", "代开企业名称:").Replace("代开企业名称：", "代开企业名称:").Replace("代开企业名称：", "代开企业名称:"), ref str2, ref str).Equals("0000"))
                    {
                        if (str.Length <= 0)
                        {
                            result.AddErrorDJ("备注中 代开企业名称不能为空\n");
                        }
                        if (str2.Length <= 0)
                        {
                            result.AddErrorDJ("备注中 代开企业税号不能为空\n");
                        }
                        FPLX fplx = (bill.DJZL == "c") ? ((FPLX) 2) : ((FPLX) 0);
                        if (!Instance.CheckTaxCode(invType, str2, false, fplx))
                        {
                            result.AddErrorDJ("备注中 代开企业税号非法\n");
                        }
                    }
                    else
                    {
                        result.AddErrorDJ("备注中 代开企业信息不完整\n");
                    }
                }
                if (bill.BZ.Contains("\x00a4") || bill.BZ.Contains("￠\x00a7"))
                {
                }
                FPLX fplx2 = (invType == InvType.Common) ? ((FPLX) 2) : ((FPLX) 0);
                bool flag6 = false;
                if ((invType == InvType.Common) && (bill.TYDH == "2"))
                {
                    flag6 = this.CheckTaxCode(invType, bill.KHYHZH, bill.SFZJY, fplx2);
                }
                else
                {
                    flag6 = this.CheckTaxCode(invType, bill.GFSH, bill.SFZJY, fplx2);
                }
                if (!flag6)
                {
                    result.AddErrorDJ("纳税人识别号非法\n");
                }
                if (this.isDebug)
                {
                    span = (TimeSpan) (DateTime.Now - now);
                    this.loger.Debug("processAA3-- 税号校验  = " + span.ToString());
                    now = DateTime.Now;
                }
                if (jEHJ < 0.0)
                {
                    if (!this.PosiInvTypeNo(bill.BZ, invType))
                    {
                        if (invType == InvType.Special)
                        {
                            infoFromNotes = CommonTool.GetInfoFromNotes(bill.BZ, 1);
                            if ((infoFromNotes.Length > 0) && infoFromNotes.Substring(0, 6).Equals("000000"))
                            {
                                result.AddErrorDJ("红字发票信息表编号不正确，不允许生成发票\n");
                            }
                            else
                            {
                                result.AddErrorDJ("负数单据没有16位信息表编号，内容类似“开具红字增值税专用发票信息表编号XXXXXXXXXXXXXXXX”，其中“X”为数字，不含空格。\n");
                            }
                        }
                        else if (invType == InvType.Common)
                        {
                            result.AddErrorDJ("负数单据没有对应的正数发票代码、号码，内容类似“对应正数发票代码:XXXXXXXXXX号码:YYYYYYYY”，其中“X”、“Y”均为数字，不含空格。\n");
                        }
                        else if (invType == InvType.transportation)
                        {
                            result.AddErrorDJ("负数单据没有16位通知单编号，内容类似“开具红字货物运输业增值税专用发票信息表编号XXXXXXXXXXXXXXXX”，其中“X”为数字，不含空格。\n");
                        }
                    }
                    else if (invType == InvType.Special)
                    {
                        infoFromNotes = CommonTool.GetInfoFromNotes(bill.BZ, 1);
                        if (this.CheckHZFPXXB(infoFromNotes, 0) != "0000")
                        {
                            result.AddErrorDJ("该红字发票信息表编号已使用过\n");
                        }
                    }
                    else if (invType == InvType.Common)
                    {
                        str7 = CommonTool.GetInfoFromNotes(bill.BZ, 0);
                        str8 = str7.Substring(0, str7.Length - 8);
                        str9 = str7.Substring(str7.Length - 8, 8);
                        fpxx = null;
                        objArray = new object[] { "c", str8, str9 };
                        objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPChanXunWenBenJieKouShareMethods", objArray);
                        if (objArray2[0] != null)
                        {
                            fpxx = (Fpxx) objArray2[0];
                            if (fpxx.zfbz)
                            {
                                result.AddErrorDJ("对应的蓝字发票已作废\n");
                            }
                            else
                            {
                                str6 = this.CheckLocalHZJE(bill.JEHJ, str8, str9, Convert.ToDouble(fpxx.je), fpxx.fplx);
                                if (str6 != "0000")
                                {
                                    if (str6 == "A309")
                                    {
                                        result.AddErrorDJ("对应的蓝字发票已开过红字发票\n");
                                    }
                                    else if (str6 == "A303")
                                    {
                                        result.AddErrorDJ("金额超限\n");
                                    }
                                    else if (str6 == "E001")
                                    {
                                        result.AddErrorDJ("对应的蓝字发票为红字发票\n");
                                    }
                                    else if (str6 == "E002")
                                    {
                                        result.AddErrorDJ("开票金额超过对应蓝字发票金额\n");
                                    }
                                }
                            }
                        }
                        else
                        {
                            str6 = this.CheckWebHZJE(bill.JEHJ, str8, str9);
                            if (str6 != "0")
                            {
                                if (str6 == "-0")
                                {
                                    if (card.get_CardBeginDate().AddDays(180.0).CompareTo(card.GetCardClock()) <= 0)
                                    {
                                    }
                                }
                                else if (str6 == "A307")
                                {
                                    result.AddErrorDJ("数据库中未找到对应的蓝票，且受理平台无可开红字发票金额，不能开红字发票！\n");
                                }
                                else if (str6 == "A308")
                                {
                                    result.AddErrorDJ("数据库中未找到对应的蓝票，且与受理平台网络不通，不能开红字发票！\n");
                                }
                            }
                        }
                    }
                    bool flag8 = false;
                    if ((card.get_ECardType() == 3) && (card.get_StateInfo().TBType == 0))
                    {
                        flag8 = true;
                    }
                    if (!flag8 && this.BillHasMultiSlv(bill))
                    {
                        result.AddErrorDJ("负数单据不能开具多重税率\n");
                    }
                    if (this.isDebug)
                    {
                        span = (TimeSpan) (DateTime.Now - now);
                        this.loger.Debug("processAA4-- 红字发票校验  = " + span.ToString());
                        now = DateTime.Now;
                    }
                }
                if (jEHJ == 0.0)
                {
                    result.AddErrorDJ("单据合计金额等于0\n");
                }
                InvSplitPara para = new InvSplitPara();
                para.GetInvSplitPara(invType);
                if ((this.Flag_Hzfw && (invType == InvType.Special)) && this.IsOverEWM(bill, 0, bill.ListGoods.Count))
                {
                    result.AddErrorMX("CF:作为汉字防伪用户 超过了汉字防伪的限制\n", 1);
                }
                if (this.isDebug)
                {
                    span = (TimeSpan) (DateTime.Now - now);
                    this.loger.Debug("processAA5-- 汉字防伪校验  = " + span.ToString());
                    now = DateTime.Now;
                }
                string str10 = (bill.QDHSPMC == null) ? "" : bill.QDHSPMC.Trim();
                if ((this.Flag_Hzfw && (str10.Length > 0)) && (invType == InvType.Special))
                {
                    result.AddErrorMX("作为汉字防伪用户 专用发票不允许开清单，请将清单行商品名称删除\n");
                }
                bool flag9 = this.CheckCEZS(bill);
                if (flag9)
                {
                    if (bill.QDHSPMC.Trim() != "")
                    {
                        result.AddErrorMX(string.Format("差额征税单据，不允许开清单\n", 0));
                    }
                    FatchSaleBill bill2 = new FatchSaleBill();
                    if (bill2.CheckMultiSlv(bill))
                    {
                        result.AddErrorMX(string.Format("差额征税单据，应该是单税率，且仅一行商品\n", 0));
                    }
                    if (bill.ListGoods.Count > 2)
                    {
                        result.AddErrorMX(string.Format("差额征税单据，只能包含一行商品\n", 0));
                    }
                    if ((bill.ListGoods.Count == 2) && (bill.ListGoods[1].DJHXZ != 4))
                    {
                        result.AddErrorMX(string.Format("差额征税单据，只能包含一行商品\n", 0));
                    }
                    if (bill.JZ_50_15)
                    {
                        result.AddErrorMX(string.Format("1.5%税率不能开具差额征税单据\n", 0));
                    }
                    if (bill.HYSY)
                    {
                        result.AddErrorMX(string.Format("中外合作油气田不能开具差额征税单据\n", 0));
                    }
                }
                double num3 = 0.0;
                double num4 = 0.0;
                double sLV = 0.0;
                double num6 = 0.0;
                double num7 = 0.0;
                int num8 = 0;
                if (bill.ListGoods.Count <= 0)
                {
                    result.AddErrorMX("没有明细商品信息\n");
                }
                else
                {
                    if (this.Flag_Hzfw)
                    {
                        if ((((invType == InvType.Special) && this.Flag_Hzfw) && (bill.JEHJ < 0.0)) && this.IsOverEWM(bill, 0, bill.ListGoods.Count))
                        {
                            result.AddErrorMX("作为汉字防伪用户 负数专用发票不允许超出开票范围限制\n");
                        }
                        this.loger.Debug("汉字防伪 校验某些字段");
                        string str11 = this.CheckHZFWBill(bill, 0, bill.ListGoods.Count);
                        if ("A612" == str11)
                        {
                            result.AddErrorMX("汉字防伪用户 商品名称超限\n");
                            this.AddCheckResult(result);
                            result.Kpzt = "所有数据未开票";
                            return result;
                        }
                        if ("A613" == str11)
                        {
                            result.AddErrorMX("汉字防伪用户 计量单位超限\n");
                            this.AddCheckResult(result);
                            result.Kpzt = "所有数据未开票";
                            return result;
                        }
                        if ((invType == InvType.Common) && (str10.Length == 0))
                        {
                            if ((para.below7ForbiddenInv && (bill.ListGoods.Count <= 7)) && this.IsOverEWM(bill, 0, 7))
                            {
                                result.AddErrorMX("作为汉字防伪用户 您已经将小于等于7行\n且超过汉字防伪限制的普通发票设置为不允许开具\n");
                            }
                            if (para.above7ForbiddenInv && (bill.ListGoods.Count > 7))
                            {
                                result.AddErrorMX("作为汉字防伪用户 您已经将大于7行\n且超过汉字防伪限制的普通发票设置为不允许开具\n");
                            }
                        }
                        if (this.isDebug)
                        {
                            span = (TimeSpan) (DateTime.Now - now);
                            this.loger.Debug("processAA6-- 汉字防伪校验  = " + span.ToString());
                            now = DateTime.Now;
                        }
                    }
                    List<string> list = new List<string>();
                    string str12 = "0";
                    bool flag10 = false;
                    for (num12 = 0; num12 < bill.ListGoods.Count; num12++)
                    {
                        Goods mx = bill.ListGoods[num12];
                        if (mx.DJHXZ != 4)
                        {
                            string str15;
                            double round;
                            double jE;
                            if (base.IsXTCorpAgent)
                            {
                                object[] objArray3 = new object[] { mx.SPMC };
                                string str13 = (string) ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMCheckXTSPByName", objArray3)[0];
                                if (str13.Equals("3"))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，稀土商品错误\n", mx.XH));
                                    break;
                                }
                                if (str13.Equals("1") || str13.Equals("2"))
                                {
                                    if (Math.Abs((double) (mx.SLV - 0.015)) < base.DataPrecision)
                                    {
                                        result.AddErrorMX(string.Format("第{0}行，稀土商品不允许开具1.5%税率\n", mx.XH));
                                    }
                                    if (!(mx.KCE == 0.0))
                                    {
                                        result.AddErrorMX(string.Format("第{0}行，稀土商品不允许开具差额征收单据\n", mx.XH));
                                    }
                                    if (bill.DJZL == "c")
                                    {
                                        result.AddErrorDJ("普票不可开具稀土商品\n");
                                    }
                                    if (((bill.DJZL == "s") && bill.HYSY) && (mx.SLV == 0.05))
                                    {
                                        result.AddErrorMX(string.Format("第{0}行，稀土商品不允许存在中外合作油气田税率\n", mx.XH));
                                        break;
                                    }
                                    if ((jEHJ < 0.0) && (Finacial.Equal(mx.SL, 0.0) ^ (mx.JLDW.Length == 0)))
                                    {
                                        result.AddErrorMX(string.Format("第{0}行，稀土商品数量和计量单位必须同时为空或非空\n", mx.XH));
                                    }
                                    if (((jEHJ >= 0.0) || !Finacial.Equal(mx.SL, 0.0)) && !(mx.JLDW.Equals("吨") || mx.JLDW.Equals("公斤")))
                                    {
                                        result.AddErrorMX(string.Format("第{0}行，稀土商品计量单位不是吨或公斤\n", mx.XH));
                                    }
                                }
                                if ((num12 > 0) && !str13.Equals(str12))
                                {
                                    result.AddErrorDJ("只允许存在同一类稀土产品\n");
                                    break;
                                }
                                str12 = str13;
                            }
                            if (this.isDebug)
                            {
                                span = (TimeSpan) (DateTime.Now - now);
                                this.loger.Debug("process2-- end IsXTCorpAgent((稀土单据商品行校验)) = " + span.ToString());
                                now = DateTime.Now;
                            }
                            double num13 = Convert.ToDouble(mx.JE);
                            double num14 = Convert.ToDouble(mx.SE);
                            if ((num13 * num14) < 0.0)
                            {
                                result.AddErrorMX(string.Format("第{0}行，金额税额不同号\n", mx.XH));
                            }
                            if (this.isDebug)
                            {
                                span = (TimeSpan) (DateTime.Now - now);
                                this.loger.Debug("processAA7-- Amount * Tax  = " + span.ToString());
                                now = DateTime.Now;
                            }
                            if (!this.CheckSPMC(mx, fplx2))
                            {
                                result.AddErrorMX(string.Format("第{0}行，商品名称长度超限\n", mx.XH));
                                break;
                            }
                            if (this.isDebug)
                            {
                                span = (TimeSpan) (DateTime.Now - now);
                                this.loger.Debug("processAA8-- CheckSPMC  = " + span.ToString());
                                now = DateTime.Now;
                            }
                            if (ToolUtil.GetByteCount(mx.JLDW) > 0x16)
                            {
                                result.AddErrorMX(string.Format("第{0}行，计量单位长度超限\n", mx.XH));
                                break;
                            }
                            if (this.isDebug)
                            {
                                span = (TimeSpan) (DateTime.Now - now);
                                this.loger.Debug("processAA9-- GetByteCount = " + span.ToString());
                                now = DateTime.Now;
                            }
                            if (this.Flag_Hzfw)
                            {
                                if ((((invType == InvType.Special) && this.Flag_Hzfw) && (bill.JEHJ < 0.0)) && this.IsOverEWM(bill, 0, bill.ListGoods.Count))
                                {
                                    result.AddErrorMX("作为汉字防伪用户 负数专用发票不允许超出开票范围限制\n");
                                    break;
                                }
                                if ((invType == InvType.Common) && (str10.Length == 0))
                                {
                                    if ((para.below7ForbiddenInv && (bill.ListGoods.Count <= 7)) && this.IsOverEWM(bill, 0, 7))
                                    {
                                        result.AddErrorMX("作为汉字防伪用户 您已经将小于等于7行\n且超过汉字防伪限制的普通发票设置为不允许开具\n");
                                        break;
                                    }
                                    if (this.isDebug)
                                    {
                                        span = (TimeSpan) (DateTime.Now - now);
                                        this.loger.Debug("process3--小于等于7-- = " + span.ToString());
                                        now = DateTime.Now;
                                    }
                                    if (para.above7ForbiddenInv && (bill.ListGoods.Count > 7))
                                    {
                                        result.AddErrorMX("作为汉字防伪用户 您已经将大于7行\n且超过汉字防伪限制的普通发票设置为不允许开具\n");
                                        break;
                                    }
                                    if (this.isDebug)
                                    {
                                        span = (TimeSpan) (DateTime.Now - now);
                                        this.loger.Debug("process3--大于7行-- = " + span.ToString());
                                        now = DateTime.Now;
                                    }
                                }
                            }
                            if (this.isDebug)
                            {
                                span = (TimeSpan) (DateTime.Now - now);
                                this.loger.Debug("process4-- end-- hanzifangwei  = " + span.ToString());
                                now = DateTime.Now;
                            }
                            if (mx.SPMC.Contains("详见红字发票"))
                            {
                                result.AddErrorMX(string.Format("第{0}行，商品名称含有“详见红字发票”字样\n", mx.XH));
                            }
                            if (((Math.Abs((double) (mx.SLV - 0.05)) < base.DataPrecision) && hYSY) && (bill.DJZL == "s"))
                            {
                                now = DateTime.Now;
                                if (!this.CheckTaxRate(InvType.Special, hYSY, mx.SLV, mx.XSYH, mx.SPBM))
                                {
                                    str14 = Convert.ToString((double) (mx.SLV * 100.0)) + "%";
                                    result.AddErrorMX(string.Format("第{0}行，非法税率{1}\n", mx.XH, str14));
                                }
                                else if (Math.Abs((double) (mx.JE + mx.SE)) < base.DataPrecision)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，开票金额为0\n", mx.XH));
                                }
                                else if (((Math.Round(Math.Abs((double) (mx.JE + mx.SE)), 3) - base.InvLimit) > 0.0) && (mx.DJHXZ == 0))
                                {
                                    str15 = string.Format("第{0}行，开票金额超出金税设备允许范围 {1}\n", mx.XH, this.InvLimit.ToString("C"));
                                    result.AddErrorMX(str15, 1);
                                }
                                else if ((Math.Abs(mx.SE) >= TaxCardValue.SoftTaxLimit) && (mx.DJHXZ == 0))
                                {
                                    str15 = string.Format("第{0}行，开票税额超出金税设备允许范围 {1}\n", mx.XH, TaxCardValue.SoftTaxLimit.ToString("C"));
                                    result.AddErrorMX(str15, 1);
                                }
                                else if ((SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) ((mx.JE + mx.SE) * mx.SLV), 2) - mx.SE)), 2) - base.SoftTaxPre) > base.DataPrecision)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，含税金额乘以税率减税额的绝对值大于{1}\n", mx.XH, base.SoftTaxPre));
                                }
                                else if (((mx.SLV > 0.0) && ((SaleBillCtrl.GetRound(Math.Abs((double) (((mx.SE / mx.SLV) - mx.JE) - mx.SE)), 3) - TaxCardValue.SoftAAmountPre) > base.DataPrecision)) && (base.Card_Type == 0))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，含税金额减税额除以税率的绝对值大于{1}\n", mx.XH, TaxCardValue.SoftAAmountPre));
                                }
                                else if (((SaleBillCtrl.GetRound(Math.Abs((double) (((mx.JE + mx.SE) * mx.SLV) - mx.SE)), 3) - TaxCardValue.SoftATaxPre) > base.DataPrecision) && (base.Card_Type >= 1))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，含税金额乘以税率减去税额的绝对值大于{1}\n", mx.XH, TaxCardValue.SoftATaxPre));
                                }
                                else if (mx.DJ < 0.0)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，单价为负数\n", mx.XH));
                                }
                                else if ((mx.SL * mx.JE) < 0.0)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，商品数据非法\n", mx.XH));
                                }
                                else if ((mx.SL == 0.0) && (mx.DJ != 0.0))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，商品数据非法\n", mx.XH));
                                }
                                else if ((jEHJ < 0.0) && (mx.JE > 0.0))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，负数单据金额为正数\n", mx.XH));
                                }
                                else if ((jEHJ > 0.0) && (mx.JE < 0.0))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，正数单据金额为负数\n", mx.XH));
                                }
                                else if ((Math.Abs(Convert.ToDouble(mx.SL)) > base.DataPrecision) && (mx.DJ > base.DataPrecision))
                                {
                                    round = 0.0;
                                    jE = 0.0;
                                    if (jEHJ > 0.0)
                                    {
                                        round = SaleBillCtrl.GetRound(Convert.ToDouble((double) (mx.SL * mx.DJ)), 2);
                                        jE = mx.JE + mx.SE;
                                        if (Math.Abs((double) (jE - round)) > (base.DataPrecision + 0.01))
                                        {
                                            result.AddErrorMX(string.Format("第{0}行，单价乘以数量不等于含税金额\n", mx.XH));
                                        }
                                    }
                                    else if (jEHJ < 0.0)
                                    {
                                        round = SaleBillCtrl.GetRound(Convert.ToDouble((double) (mx.SL * mx.DJ)), 2);
                                        jE = mx.JE + mx.SE;
                                        if (Math.Abs((double) (jE - round)) > (base.DataPrecision + 0.01))
                                        {
                                            round = SaleBillCtrl.GetRound(Convert.ToDouble((double) ((mx.SL * mx.DJ) * (1.0 - mx.SLV))), 2);
                                            if (Math.Abs((double) (mx.JE - round)) > (base.DataPrecision + 0.01))
                                            {
                                                result.AddErrorMX(string.Format("第{0}行，单价乘以数量不等于金额\n", mx.XH));
                                            }
                                        }
                                    }
                                }
                                if (this.isDebug)
                                {
                                    span = (TimeSpan) (DateTime.Now - now);
                                    this.loger.Debug("process5-- end-- haiyangshiyou(中外合作油气田校验)  = " + span.ToString());
                                    now = DateTime.Now;
                                }
                            }
                            else if (Math.Abs((double) (mx.SLV - 0.015)) < base.DataPrecision)
                            {
                                if (!this.CheckTaxRate(InvType.Special, hYSY, mx.SLV, mx.XSYH, mx.SPBM))
                                {
                                    str14 = Convert.ToString((double) (mx.SLV * 100.0)) + "%";
                                    result.AddErrorMX(string.Format("第{0}行，非法税率{1}\n", mx.XH, str14));
                                }
                                else if (Math.Abs((double) (mx.JE + mx.SE)) < base.DataPrecision)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，开票金额为0\n", mx.XH));
                                }
                                else if (((Math.Round(Math.Abs(mx.JE), 2) - base.InvLimit) > 0.0) && (mx.DJHXZ == 0))
                                {
                                    str15 = string.Format("第{0}行，开票金额超出金税设备允许范围 {1}\n", mx.XH, this.InvLimit.ToString("C"));
                                    result.AddErrorMX(str15, 1);
                                }
                                else if ((Math.Abs(mx.SE) >= TaxCardValue.SoftTaxLimit) && (mx.DJHXZ == 0))
                                {
                                    str15 = string.Format("第{0}行，开票税额超出金税设备允许范围 {1}\n", mx.XH, TaxCardValue.SoftTaxLimit.ToString("C"));
                                    result.AddErrorMX(str15, 1);
                                }
                                else if (mx.DJ < 0.0)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，单价为负数\n", mx.XH));
                                }
                                else if ((mx.SL * mx.JE) < 0.0)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，商品数据非法\n", mx.XH));
                                }
                                else if ((mx.SL == 0.0) && (mx.DJ != 0.0))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，商品数据非法\n", mx.XH));
                                }
                                else if ((jEHJ < 0.0) && (mx.JE > 0.0))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，负数单据金额为正数\n", mx.XH));
                                }
                                else if ((jEHJ > 0.0) && (mx.JE < 0.0))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，正数单据金额为负数\n", mx.XH));
                                }
                                else
                                {
                                    if ((Math.Abs(Convert.ToDouble(mx.SL)) > base.DataPrecision) && (mx.DJ > base.DataPrecision))
                                    {
                                        round = 0.0;
                                        jE = 0.0;
                                        if (jEHJ > 0.0)
                                        {
                                            if (mx.HSJBZ)
                                            {
                                                round = SaleBillCtrl.GetRound(Convert.ToDouble((double) (mx.SL * mx.DJ)), 2);
                                                jE = mx.JE + mx.SE;
                                                if (Math.Abs((double) (round - jE)) > (base.DataPrecision + 0.01))
                                                {
                                                    result.AddErrorMX(string.Format("第{0}行，含税单价乘以数量不等于含税金额\n", mx.XH));
                                                }
                                            }
                                            else
                                            {
                                                round = SaleBillCtrl.GetRound(Convert.ToDouble((double) (mx.SL * mx.DJ)), 2);
                                                jE = mx.JE;
                                                if (Math.Abs((double) (round - jE)) > (base.DataPrecision + 0.01))
                                                {
                                                    result.AddErrorMX(string.Format("第{0}行，单价乘以数量不等于金额\n", mx.XH));
                                                }
                                            }
                                        }
                                        else if (jEHJ < 0.0)
                                        {
                                            if (mx.HSJBZ)
                                            {
                                                round = SaleBillCtrl.GetRound(Convert.ToDouble((double) (mx.SL * mx.DJ)), 2);
                                                jE = mx.JE + mx.SE;
                                                if (Math.Abs((double) (round - jE)) > (base.DataPrecision + 0.01))
                                                {
                                                    result.AddErrorMX(string.Format("第{0}行，含税单价乘以数量不等于含税金额\n", mx.XH));
                                                }
                                            }
                                            else
                                            {
                                                round = SaleBillCtrl.GetRound(Convert.ToDouble((double) (mx.SL * mx.DJ)), 2);
                                                jE = mx.JE;
                                                if (Math.Abs((double) (round - jE)) > (base.DataPrecision + 0.01))
                                                {
                                                    result.AddErrorMX(string.Format("第{0}行，单价乘以数量不等于金额\n", mx.XH));
                                                }
                                            }
                                        }
                                    }
                                    if ((Math.Abs(mx.JE) > base.DataPrecision) && (Math.Abs(mx.SE) > base.DataPrecision))
                                    {
                                        double num18 = mx.JE / 69.0;
                                        if (Math.Abs(Math.Round((double) (num18 - mx.SE), 3)) > 0.06)
                                        {
                                            result.AddErrorMX(string.Format("第{0}行，税额误差大于0.06\n", mx.XH));
                                        }
                                    }
                                }
                                for (int i = 0; i < bill.ListGoods.Count; i++)
                                {
                                    if (Math.Abs((double) (bill.ListGoods[i].SLV - 0.015)) > base.DataPrecision)
                                    {
                                        result.AddErrorMX(string.Format("第{0}行，1.5%税率不能与其他税率混用\n", mx.XH));
                                        break;
                                    }
                                }
                            }
                            else if (flag9)
                            {
                                if (!this.CheckTaxRate(invType, hYSY, mx.SLV, mx.XSYH, mx.SPBM))
                                {
                                    str14 = Convert.ToString((double) (mx.SLV * 100.0)) + "%";
                                    result.AddErrorMX(string.Format("第{0}行，非法税率{1}\n", mx.XH, str14));
                                }
                                else if (Math.Abs(mx.JE) < base.DataPrecision)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，开票金额为0\n", mx.XH));
                                }
                                else if ((Math.Abs(mx.JE) > base.InvLimit) && (mx.DJHXZ == 0))
                                {
                                    str15 = string.Format("第{0}行，开票金额超出金税设备允许范围 {1}\n", mx.XH, this.InvLimit.ToString("C"));
                                    result.AddErrorMX(str15, 1);
                                }
                                else if ((Math.Abs(mx.SE) >= TaxCardValue.SoftTaxLimit) && (mx.DJHXZ == 0))
                                {
                                    str15 = string.Format("第{0}行，开票税额超出金税设备允许范围 {1}\n", mx.XH, TaxCardValue.SoftTaxLimit.ToString("C"));
                                    result.AddErrorMX(str15, 1);
                                }
                                else if ((SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) ((mx.JE - mx.KCE) * mx.SLV), 2) - mx.SE)), 2) - base.SoftTaxPre) > base.DataPrecision)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，税额误差超过{1}\n", mx.XH, base.SoftTaxPre));
                                }
                                else if ((mx.SLV == 0.0) && (Math.Abs(mx.SE) > base.DataPrecision))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，免税税率发票开票税额不为0\n", mx.XH));
                                }
                                else if ((mx.JE * mx.KCE) < 0.0)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，金额与扣除额不同号\n", mx.XH));
                                }
                                else if (((mx.SLV > 0.0) && ((SaleBillCtrl.GetRound(Math.Abs((double) (((mx.SE / mx.SLV) + mx.KCE) - mx.JE)), 3) - TaxCardValue.SoftAAmountPre) > base.DataPrecision)) && (base.Card_Type == 0))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，由税率反算的金额与实际金额的误差大于{1}\n", mx.XH, TaxCardValue.SoftAAmountPre));
                                }
                                else if (((SaleBillCtrl.GetRound(Math.Abs((double) (((mx.JE - mx.KCE) * mx.SLV) - mx.SE)), 3) - TaxCardValue.SoftATaxPre) > base.DataPrecision) && (base.Card_Type >= 1))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，(含税销售额 减 扣除额)乘以税率 与 税额 的误差大于{1}\n", mx.XH, TaxCardValue.SoftATaxPre));
                                }
                                else if (mx.DJ < 0.0)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，单价为负数\n", mx.XH));
                                }
                                else if ((mx.SL * mx.JE) < 0.0)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，数量与金额应该同号\n", mx.XH));
                                }
                                else if ((mx.SL == 0.0) && (mx.DJ != 0.0))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，数量为0时，单价应该为0\n", mx.XH));
                                }
                                else if ((jEHJ < 0.0) && (mx.JE > 0.0))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，负数单据金额为正数\n", mx.XH));
                                }
                                else if ((jEHJ > 0.0) && (mx.JE < 0.0))
                                {
                                    result.AddErrorMX(string.Format("第{0}行，正数单据金额为负数\n", mx.XH));
                                }
                                else if ((Math.Abs(Convert.ToDouble(mx.SL)) > base.DataPrecision) && (mx.DJ > base.DataPrecision))
                                {
                                    round = 0.0;
                                    jE = 0.0;
                                    if (jEHJ > 0.0)
                                    {
                                        round = SaleBillCtrl.GetRound(Convert.ToDouble((double) (mx.SL * mx.DJ)), 2);
                                        jE = mx.HSJBZ ? (mx.JE + mx.SE) : mx.JE;
                                        if (Math.Abs((double) (jE - round)) > (base.DataPrecision + 0.01))
                                        {
                                            if (mx.HSJBZ)
                                            {
                                                result.AddErrorMX(string.Format("第{0}行，单价乘以数量不等于含税金额\n", mx.XH));
                                            }
                                            else
                                            {
                                                result.AddErrorMX(string.Format("第{0}行，单价乘以数量不等于金额\n", mx.XH));
                                            }
                                        }
                                    }
                                    else if (jEHJ < 0.0)
                                    {
                                        round = SaleBillCtrl.GetRound(Convert.ToDouble((double) (mx.SL * mx.DJ)), 2);
                                        jE = mx.HSJBZ ? (mx.JE + mx.SE) : mx.JE;
                                        if (Math.Abs((double) (jE - round)) > (base.DataPrecision + 0.01))
                                        {
                                            if (mx.HSJBZ)
                                            {
                                                round = SaleBillCtrl.GetRound((double) (round / (1.0 + mx.SLV)), 2);
                                                if (Math.Abs((double) (mx.JE - round)) > (base.DataPrecision + 0.01))
                                                {
                                                    result.AddErrorMX(string.Format("第{0}行，单价乘以数量与实际金额误差超过0.01\n", mx.XH));
                                                }
                                            }
                                            else
                                            {
                                                result.AddErrorMX(string.Format("第{0}行，单价乘以数量 与 实际金额 误差超过0.01\n", mx.XH));
                                            }
                                        }
                                    }
                                }
                                if (mx.JE > 0.0)
                                {
                                    if ((mx.KCE - mx.JE) > 0.0)
                                    {
                                        result.AddErrorMX(string.Format("第{0}行，扣除额大于金额\n", mx.XH));
                                    }
                                }
                                else if ((mx.JE - mx.KCE) > 0.0)
                                {
                                    result.AddErrorMX(string.Format("第{0}行，负数单据，差额大于0\n", mx.XH));
                                }
                            }
                            else if (!this.CheckTaxRate(invType, hYSY, mx.SLV, mx.XSYH, mx.SPBM))
                            {
                                str14 = Convert.ToString((double) (mx.SLV * 100.0)) + "%";
                                result.AddErrorMX(string.Format("第{0}行，非法税率{1}\n", mx.XH, str14));
                            }
                            else if (Math.Abs(mx.JE) < base.DataPrecision)
                            {
                                result.AddErrorMX(string.Format("第{0}行，开票金额为0\n", mx.XH));
                            }
                            else if ((Math.Abs(mx.JE) > base.InvLimit) && (mx.DJHXZ == 0))
                            {
                                str15 = string.Format("第{0}行，开票金额超出金税设备允许范围 {1}\n", mx.XH, this.InvLimit.ToString("C"));
                                result.AddErrorMX(str15, 1);
                            }
                            else if ((Math.Abs(mx.SE) >= TaxCardValue.SoftTaxLimit) && (mx.DJHXZ == 0))
                            {
                                str15 = string.Format("第{0}行，开票税额超出金税设备允许范围 {1}\n", mx.XH, TaxCardValue.SoftTaxLimit.ToString("C"));
                                result.AddErrorMX(str15, 1);
                            }
                            else if ((SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) (mx.JE * mx.SLV), 2) - mx.SE)), 2) - base.SoftTaxPre) > base.DataPrecision)
                            {
                                result.AddErrorMX(string.Format("第{0}行，金额乘以税率减税额的绝对值大于{1}\n", mx.XH, base.SoftTaxPre));
                            }
                            else if ((mx.SLV == 0.0) && (Math.Abs(mx.SE) > base.DataPrecision))
                            {
                                result.AddErrorMX(string.Format("第{0}行，免税税率发票开票税额不为0\n", mx.XH));
                            }
                            else if (((mx.SLV > 0.0) && ((SaleBillCtrl.GetRound(Math.Abs((double) ((mx.SE / mx.SLV) - mx.JE)), 3) - TaxCardValue.SoftAAmountPre) > base.DataPrecision)) && (base.Card_Type == 0))
                            {
                                result.AddErrorMX(string.Format("第{0}行，金额 减 税额除以税率 的绝对值大于{1}\n", mx.XH, TaxCardValue.SoftAAmountPre));
                            }
                            else if (((SaleBillCtrl.GetRound(Math.Abs((double) ((mx.JE * mx.SLV) - mx.SE)), 3) - TaxCardValue.SoftATaxPre) > base.DataPrecision) && (base.Card_Type >= 1))
                            {
                                result.AddErrorMX(string.Format("第{0}行，金额乘以税率减去税额的绝对值大于{1}\n", mx.XH, TaxCardValue.SoftATaxPre));
                            }
                            else if (mx.DJ < 0.0)
                            {
                                result.AddErrorMX(string.Format("第{0}行，单价为负数\n", mx.XH));
                            }
                            else if ((mx.SL * mx.JE) < 0.0)
                            {
                                result.AddErrorMX(string.Format("第{0}行，商品数据非法\n", mx.XH));
                            }
                            else if ((mx.SL == 0.0) && (mx.DJ != 0.0))
                            {
                                result.AddErrorMX(string.Format("第{0}行，商品数据非法\n", mx.XH));
                            }
                            else if ((jEHJ < 0.0) && (mx.JE > 0.0))
                            {
                                result.AddErrorMX(string.Format("第{0}行，负数单据金额为正数\n", mx.XH));
                            }
                            else if ((jEHJ > 0.0) && (mx.JE < 0.0))
                            {
                                result.AddErrorMX(string.Format("第{0}行，正数单据金额为负数\n", mx.XH));
                            }
                            else if ((Math.Abs(Convert.ToDouble(mx.SL)) > base.DataPrecision) && (mx.DJ > base.DataPrecision))
                            {
                                round = 0.0;
                                jE = 0.0;
                                if (jEHJ > 0.0)
                                {
                                    round = SaleBillCtrl.GetRound(Convert.ToDouble((double) (mx.SL * mx.DJ)), 2);
                                    jE = mx.HSJBZ ? (mx.JE + mx.SE) : mx.JE;
                                    if (Math.Abs((double) (jE - round)) > (base.DataPrecision + 0.01))
                                    {
                                        if (mx.HSJBZ)
                                        {
                                            result.AddErrorMX(string.Format("第{0}行，单价乘以数量不等于含税金额\n", mx.XH));
                                        }
                                        else
                                        {
                                            result.AddErrorMX(string.Format("第{0}行，单价乘以数量不等于金额\n", mx.XH));
                                        }
                                    }
                                }
                                else if (jEHJ < 0.0)
                                {
                                    round = SaleBillCtrl.GetRound(Convert.ToDouble((double) (mx.SL * mx.DJ)), 2);
                                    jE = mx.HSJBZ ? (mx.JE + mx.SE) : mx.JE;
                                    if (Math.Abs((double) (jE - round)) > (base.DataPrecision + 0.01))
                                    {
                                        if (mx.HSJBZ)
                                        {
                                            round = SaleBillCtrl.GetRound((double) (round / (1.0 + mx.SLV)), 2);
                                            if (Math.Abs((double) (mx.JE - round)) > (base.DataPrecision + 0.01))
                                            {
                                                result.AddErrorMX(string.Format("第{0}行，单价乘以数量不等于金额\n", mx.XH));
                                            }
                                        }
                                        else
                                        {
                                            result.AddErrorMX(string.Format("第{0}行，单价乘以数量不等于金额\n", mx.XH));
                                        }
                                    }
                                }
                            }
                            if (this.isDebug)
                            {
                                span = (TimeSpan) (DateTime.Now - now);
                                this.loger.Debug("process6-- end-- None -haiyangshiyou  = " + span.ToString());
                                now = DateTime.Now;
                            }
                            if (CommonTool.isSPBMVersion())
                            {
                                if ((mx.FLBM.Trim() == "") && (mx.SPMC.Trim() != "详见对应正数发票及清单"))
                                {
                                    result.AddErrorMX(string.Format("第{0}组商品 没有对应的分类编码\n", mx.XH));
                                }
                                else if ((mx.FLBM.Trim() != "") && (mx.SPMC.Trim() != "详见对应正数发票及清单"))
                                {
                                    if (!this.CheckFLBM_KYZT(mx.FLBM.Trim()))
                                    {
                                        result.AddErrorMX(string.Format("第{0}组商品 对应的商品分类编码处于无效状态\n", mx.XH));
                                    }
                                    if (this.CheckFLBM_HZX(mx.FLBM.Trim()))
                                    {
                                        if (card.get_QYLX().ISDXQY)
                                        {
                                            string str16 = mx.FLBM.Trim();
                                            while (str16.Length < 0x13)
                                            {
                                                str16 = str16 + "0";
                                            }
                                            string str17 = str16.Substring(0, 5);
                                            if (!(((invType != InvType.Special) || str17.Contains("30301")) || str17.Contains("30302")))
                                            {
                                                result.AddErrorMX(string.Format("第{0}组商品 电信企业 专用发票只能开具的商品分类为：基础电信服务、增值电信服务 及其子类\n", mx.XH));
                                            }
                                            else if (!((invType != InvType.Common) || str17.Contains("303")))
                                            {
                                                result.AddErrorMX(string.Format("第{0}组商品 电信企业 普通发票只能开具的商品分类为：电信服务及其子类\n", mx.XH));
                                            }
                                        }
                                        else
                                        {
                                            result.AddErrorMX(string.Format("第{0}组商品 不能使用汇总项分类编码\n", mx.XH));
                                        }
                                    }
                                    DataTable table = new SaleBillDAL().GET_SP_BY_BM(mx.SPBM);
                                    bool flag11 = false;
                                    if ((table.Rows.Count > 0) && (table.Rows[0]["XTHASH"].ToString().Trim() != ""))
                                    {
                                        flag11 = true;
                                    }
                                    objArray5 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", new object[] { mx.FLBM.Trim(), true, flag11 });
                                    if ((objArray5 != null) && !bool.Parse(objArray5[0].ToString()))
                                    {
                                        result.AddErrorMX(string.Format("第{0}组商品 当前企业没有所选税收分类编码授权\n", mx.XH));
                                    }
                                }
                                else if ((mx.SPMC.Trim() == "详见对应正数发票及清单") && ((mx.JE >= 0.0) && (mx.FLBM.Trim() == "")))
                                {
                                    result.AddErrorMX(string.Format("第{0}组商品 没有对应的分类编码\n", mx.XH));
                                }
                            }
                            if (mx.DJHXZ == 3)
                            {
                                num6 += mx.JE;
                                num7 += mx.SE;
                            }
                            num3 += mx.JE;
                            num4 += mx.SE;
                            if (num12 == 0)
                            {
                                sLV = mx.SLV;
                            }
                            else if (!(sLV == mx.SLV))
                            {
                                flag10 = true;
                            }
                        }
                        else
                        {
                            num8++;
                            int zKH = 0;
                            double disCount = 0.0;
                            if (CommonTool.isSPBMVersion())
                            {
                                double je = 0.0;
                                double zkje = Math.Abs(mx.JE);
                                if ((num12 - 1) >= 0)
                                {
                                    je = bill.ListGoods[num12 - 1].JE;
                                    disCount = CommonTool.GetDisCount(zkje, je, ref zKH);
                                }
                            }
                            else if ((num12 - 1) >= 0)
                            {
                                if (mx.SPMC.Trim() == bill.ListGoods[num12 - 1].SPMC.Trim())
                                {
                                    string disCountMC = CommonTool.GetDisCountMC(1.0, 1);
                                    result.AddErrorMX(string.Format("第{0}组折扣商品 折扣行名称不正确，应为 " + disCountMC + "\n", num8));
                                }
                                else
                                {
                                    disCount = CommonTool.GetDisCount(mx.SPMC, ref zKH);
                                }
                            }
                            if ((disCount == 0.0) && (num6 != 0.0))
                            {
                                disCount = (-1.0 * mx.JE) / num6;
                            }
                            if (((mx.FPZL == "s") && (Math.Abs((double) (mx.SLV - 0.05)) < base.DataPrecision)) && hYSY)
                            {
                                if (Math.Abs((double) (((num6 + mx.JE) + num7) + mx.SE)) > base.InvLimit)
                                {
                                    result.AddErrorMX(string.Format("第{0}组折扣商品 开票金额超出金税设备允许范围 {1}\n", num8, this.InvLimit.ToString("C")));
                                }
                                else if (Math.Abs((double) (num7 + mx.SE)) > TaxCardValue.SoftTaxLimit)
                                {
                                    result.AddErrorMX(string.Format("第{0}组折扣商品 开票税额超出金税设备允许范围 {1}\n", num8, TaxCardValue.SoftTaxLimit.ToString("C")));
                                }
                            }
                            else if (Math.Abs((double) (num6 + mx.JE)) > base.InvLimit)
                            {
                                result.AddErrorMX(string.Format("第{0}组折扣商品 开票金额超出金税设备允许范围 {1}\n", num8, this.InvLimit.ToString("C")));
                            }
                            else if (Math.Abs((double) (num7 + mx.SE)) > TaxCardValue.SoftTaxLimit)
                            {
                                result.AddErrorMX(string.Format("第{0}组折扣商品 开票税额超出金税设备允许范围 {1}\n", num8, TaxCardValue.SoftTaxLimit.ToString("C")));
                            }
                            num6 = 0.0;
                            num7 = 0.0;
                            if (mx.JE == 0.0)
                            {
                                result.AddErrorMX(string.Format("第{0}组折扣商品 折扣金额不能为0\n", num8));
                            }
                            else
                            {
                                double num25 = Math.Round(Math.Abs((double) (Math.Abs((double) (mx.JE * mx.SLV)) - Math.Abs(mx.SE))), 3);
                                if (Math.Abs((double) (mx.SLV - 0.015)) < 1E-05)
                                {
                                    num25 = Math.Round(Math.Abs((double) (Math.Abs((double) (mx.JE / 69.0)) - Math.Abs(mx.SE))), 3);
                                }
                                if ((Math.Abs((double) (mx.SLV - 0.05)) < 1E-05) && bill.HYSY)
                                {
                                    num25 = Math.Round(Math.Abs((double) (Math.Abs((double) (mx.JE / 19.0)) - Math.Abs(mx.SE))), 3);
                                }
                                if (Math.Abs(num25) > base.SoftTaxPre)
                                {
                                    result.AddErrorMX(string.Format("第{0}组折扣商品 折扣税额误差大于0.06\n", num8));
                                }
                            }
                            if ((flag9 && ((num12 - 1) >= 0)) && (Math.Abs(mx.JE) > (bill.ListGoods[num12 - 1].JE - bill.ListGoods[num12 - 1].KCE)))
                            {
                                result.AddErrorMX(string.Format("第{0}组折扣商品 折扣金额超过差额征税的差额值\n", num8));
                            }
                            if (this.isDebug)
                            {
                                this.loger.Debug("process7-- end-- zhekoujisuan（折扣行校验）  = " + ((TimeSpan) (DateTime.Now - now)).ToString());
                                now = DateTime.Now;
                            }
                        }
                        if (!string.IsNullOrEmpty(mx.FPDM))
                        {
                            num++;
                        }
                        this.loger.Info("循环校验 Index= " + num12 + " ");
                    }
                    if (!flag10)
                    {
                        double num26;
                        bool flag13 = ((bill.DJZL == "s") && bill.HYSY) && (Math.Abs((double) (sLV - 0.05)) < base.DataPrecision);
                        if (!flag13)
                        {
                            num26 = (sLV == 0.0) ? 0.0 : (SaleBillCtrl.GetRound(Math.Abs((double) ((num4 / sLV) - num3)), 3) - base.DataPrecision);
                        }
                        else
                        {
                            num26 = (sLV == 0.0) ? 0.0 : (SaleBillCtrl.GetRound(Math.Abs((double) (((num4 / sLV) - num3) - num4)), 3) - base.DataPrecision);
                        }
                        if (base.Card_Type == 0)
                        {
                            if (num26 > TaxCardValue.SoftAAmountPre)
                            {
                                result.AddErrorMX("CF:合计金额减合计税额除以税率的绝对值不能大于" + TaxCardValue.SoftAAmountPre, 1);
                            }
                        }
                        else if (num26 > TaxCardValue.SoftATaxPre)
                        {
                            result.AddErrorMX("CF:合计金额减合计税额除以税率的绝对值不能大于" + TaxCardValue.SoftATaxPre, 1);
                        }
                    }
                    else
                    {
                        result.NeedCF = true;
                        result.AddErrorDJ("CF:单据含有多个税率", 1);
                    }
                }
            }
            this.AddCheckResult(result);
            if (num == 0)
            {
                result.Kpzt = "所有数据未开票";
            }
            else if (num == bill.ListGoods.Count)
            {
                result.Kpzt = "所有数据已开票";
            }
            else
            {
                result.Kpzt = "部分数据已开票";
            }
            if (((bill.DJZL == "f") || (bill.DJZL == "j")) && (bill.KPZT == "A"))
            {
                result.Kpzt = "所有数据已开票";
            }
            return result;
        }

        private bool CheckSH(string nsrsbh)
        {
            int num2;
            char ch;
            if (nsrsbh.Length != 15)
            {
                DateTime time;
                if (nsrsbh.Length == 0x11)
                {
                    if (nsrsbh.Substring(15, 2).CompareTo("XX") == 0)
                    {
                        string str2 = nsrsbh.Substring(6, 6);
                        string s = "19" + str2;
                        string str4 = "20" + str2;
                        try
                        {
                            time = DateTime.ParseExact(s, "yyyyMMdd", null);
                            return true;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                time = DateTime.ParseExact(str4, "yyyyMMdd", null);
                                return true;
                            }
                            catch (Exception)
                            {
                                return false;
                            }
                        }
                    }
                    num2 = 0;
                    while (num2 < nsrsbh.Length)
                    {
                        if (!char.IsDigit(nsrsbh[num2]))
                        {
                            return false;
                        }
                        num2++;
                    }
                    return true;
                }
                if ((nsrsbh.Length == 0x12) || (nsrsbh.Length == 20))
                {
                    for (num2 = 0; num2 < nsrsbh.Length; num2++)
                    {
                        if (num2 == 0x11)
                        {
                            if (!(char.IsDigit(nsrsbh[num2]) || (nsrsbh[num2] == 'X')))
                            {
                                return false;
                            }
                        }
                        else if (!char.IsDigit(nsrsbh[num2]))
                        {
                            return false;
                        }
                    }
                    try
                    {
                        time = DateTime.ParseExact(nsrsbh.Substring(6, 8), "yyyyMMdd", null);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    int index = 0;
                    byte[] buffer = new byte[] { 
                        7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 
                        2, 1
                     };
                    char[] chArray = new char[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
                    for (num2 = 0; num2 < 0x11; num2++)
                    {
                        char ch2 = nsrsbh[num2];
                        index += int.Parse(ch2.ToString()) * buffer[num2];
                    }
                    index = index % 11;
                    return (nsrsbh[0x11] == chArray[index]);
                }
                return false;
            }
            int num = 15;
            for (num2 = 0; num2 < nsrsbh.Length; num2++)
            {
                if (!char.IsDigit(nsrsbh[num2]))
                {
                    num = num2;
                    break;
                }
            }
            if (num == 15)
            {
                return true;
            }
            if (num < 6)
            {
                return false;
            }
            int num3 = 0;
            int[] numArray = new int[] { 3, 7, 9, 10, 5, 8, 4, 2 };
            for (num2 = 6; num2 < 14; num2++)
            {
                if ("0123456789ABCDEFGHJKLMNPQRTUVWXY".IndexOf(nsrsbh[num2]) < 0)
                {
                    return false;
                }
                num3 += numArray[num2 - 6] * ((nsrsbh[num2] <= '9') ? (nsrsbh[num2] - '0') : ((nsrsbh[num2] - 'A') + 10));
            }
            num3 = 11 - (num3 % 11);
            switch (num3)
            {
                case 10:
                    ch = 'X';
                    break;

                case 11:
                    ch = '0';
                    break;

                default:
                    ch = (char) (num3 + 0x30);
                    break;
            }
            return (nsrsbh[14] == ch);
        }

        private bool CheckSPMC(Goods mx, FPLX fplx)
        {
            TimeSpan span;
            DateTime now = DateTime.Now;
            if (this.isDebug)
            {
                span = (TimeSpan) (DateTime.Now - now);
                this.loger.Debug("process CheckSPMC start time = " + span.ToString());
                now = DateTime.Now;
            }
            if (this.isDebug)
            {
                span = (TimeSpan) (DateTime.Now - now);
                this.loger.Debug("process CheckSPMC Invoice InvoiceKP = null time = " + span.ToString());
                now = DateTime.Now;
            }
            if (this.InvoiceKPTemp == null)
            {
                byte[] sourceArray = Invoice.get_TypeByte();
                byte[] destinationArray = new byte[0x20];
                Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                byte[] buffer4 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KT" + DateTime.Now.ToString("F")), destinationArray, buffer3);
                this.InvoiceKPTemp = new Invoice(false, false, false, 2, buffer4, null);
            }
            if (this.isDebug)
            {
                span = (TimeSpan) (DateTime.Now - now);
                this.loger.Debug("process new  Invoice-- InvoiceKP.new Invoice  = " + span.ToString());
                now = DateTime.Now;
            }
            this.InvoiceKPTemp.DelSpxxAll();
            if (this.isDebug)
            {
                span = (TimeSpan) (DateTime.Now - now);
                this.loger.Debug("process after DelSpxxAll-- InvoiceKP.DelSpxxAll  = " + span.ToString());
                now = DateTime.Now;
            }
            int num = this.InvoiceKPTemp.AddSpxx(mx.SPMC, mx.SPSM, mx.SLV.ToString(), 0);
            if (this.isDebug)
            {
                span = (TimeSpan) (DateTime.Now - now);
                this.loger.Debug("process SPMC-- InvoiceKP.AddSpxx  = " + span.ToString());
                now = DateTime.Now;
            }
            List<Dictionary<SPXX, string>> mxxxs = this.InvoiceKPTemp.GetMxxxs();
            if ((mxxxs != null) && (mxxxs.Count > 0))
            {
                Dictionary<SPXX, string> dictionary = mxxxs[0];
                string str = dictionary[0];
                if (!mx.SPMC.Equals(str))
                {
                    return false;
                }
            }
            if (this.isDebug)
            {
                span = (TimeSpan) (DateTime.Now - now);
                this.loger.Debug("process SPMC-- mx.SPMC.Equals(spmc)  = " + span.ToString());
                now = DateTime.Now;
            }
            if (this.Flag_Hzfw && (this.IsOverLimit(fplx, 0, mx.SPMC) == 3))
            {
                return false;
            }
            if (this.isDebug)
            {
                this.loger.Debug("process SPMC-- Flag_Hzfw--IsOverLimit = " + ((TimeSpan) (DateTime.Now - now)).ToString());
                now = DateTime.Now;
            }
            return true;
        }

        public bool CheckTaxCode(InvType InvoiceKind, string GFSH, bool SFZJY, FPLX fplx)
        {
            if ((InvoiceKind == InvType.Common) && ((((GFSH == "000000000000000") || (GFSH == "00000000000000000")) || (GFSH == "000000000000000000")) || (GFSH == "00000000000000000000")))
            {
                GFSH = "";
            }
            string str = "0000";
            if (((InvoiceKind != InvType.Common) && (InvoiceKind != InvType.vehiclesales)) || !(GFSH == ""))
            {
                str = this.invoiceHandler.CheckTaxCode(GFSH, fplx);
            }
            if (str.Equals("0000"))
            {
                if (Regex.IsMatch(GFSH, "[a-z]"))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool CheckTaxRate(InvType InvoiceKind, bool HYSY, double SLV, bool XSYH, string BM)
        {
            int num2;
            int num3;
            int num4;
            int num5;
            if (SLV < 0.0)
            {
                return false;
            }
            if ((InvoiceKind != InvType.Special) && HYSY)
            {
                return false;
            }
            int count = TaxCardValue.taxCard.get_SQInfo().PZSQType.Count;
            List<double> list = new List<double>();
            if (InvoiceKind == InvType.Special)
            {
                if (SLV == 0.0)
                {
                    return false;
                }
                if (HYSY)
                {
                    return (SaleBillCtrl.GetRound(SLV, 2) == 0.05);
                }
                if (SaleBillCtrl.GetRound(SLV, 2) == 0.05)
                {
                }
                for (num2 = 0; num2 < count; num2++)
                {
                    if (TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].invType == 0)
                    {
                        num3 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate.Count;
                        num4 = 0;
                        while (num4 < num3)
                        {
                            list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate[num4].Rate);
                            num4++;
                        }
                        num5 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate2.Count;
                        num4 = 0;
                        while (num4 < num5)
                        {
                            if (HYSY || (Math.Abs((double) (TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate2[num4].Rate - 0.05)) >= 1E-07))
                            {
                                list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate2[num4].Rate);
                            }
                            num4++;
                        }
                    }
                }
            }
            else if (InvoiceKind == InvType.Common)
            {
                for (num2 = 0; num2 < count; num2++)
                {
                    if (TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].invType == 2)
                    {
                        num3 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate.Count;
                        num4 = 0;
                        while (num4 < num3)
                        {
                            list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate[num4].Rate);
                            num4++;
                        }
                        num5 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate2.Count;
                        num4 = 0;
                        while (num4 < num5)
                        {
                            list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate2[num4].Rate);
                            num4++;
                        }
                    }
                }
            }
            else if (InvoiceKind == InvType.transportation)
            {
                for (num2 = 0; num2 < count; num2++)
                {
                    if (TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].invType == 11)
                    {
                        num3 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate.Count;
                        num4 = 0;
                        while (num4 < num3)
                        {
                            list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate[num4].Rate);
                            num4++;
                        }
                        num5 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate2.Count;
                        num4 = 0;
                        while (num4 < num5)
                        {
                            list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate2[num4].Rate);
                            num4++;
                        }
                    }
                }
            }
            else if (InvoiceKind == InvType.vehiclesales)
            {
                for (num2 = 0; num2 < count; num2++)
                {
                    if (TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].invType == 12)
                    {
                        num3 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate.Count;
                        num4 = 0;
                        while (num4 < num3)
                        {
                            list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate[num4].Rate);
                            num4++;
                        }
                        num5 = TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate2.Count;
                        for (num4 = 0; num4 < num5; num4++)
                        {
                            list.Add(TaxCardValue.taxCard.get_SQInfo().PZSQType[num2].TaxRate2[num4].Rate);
                        }
                    }
                }
            }
            if (CommonTool.isSPBMVersion())
            {
                List<double> list2;
                string dJLX = "c";
                if ((InvoiceKind == InvType.Common) || (InvoiceKind == InvType.Special))
                {
                    dJLX = "c";
                }
                else if (InvoiceKind == InvType.transportation)
                {
                    dJLX = "h";
                }
                else if (InvoiceKind == InvType.vehiclesales)
                {
                    dJLX = "j";
                }
                if (XSYH && (BM.Trim() != ""))
                {
                    SaleBillDAL ldal = new SaleBillDAL();
                    list2 = ldal.GET_YHZCSLV_BY_SPBM(BM, dJLX);
                    foreach (double num6 in list2)
                    {
                        list.Add(num6);
                    }
                }
                else
                {
                    list2 = new SaleBillDAL().GET_YHZCSYSLV_BY_SPBM(BM, dJLX);
                    foreach (double num6 in list2)
                    {
                        list.Add(num6);
                    }
                }
            }
            bool flag = false;
            for (num2 = 0; num2 < list.Count; num2++)
            {
                if (Math.Abs((double) (SLV - list[num2])) < 1E-05)
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

        private bool CheckZKZ(SaleBill bill)
        {
            int num = 0;
            for (int i = 0; i < bill.ListGoods.Count; i++)
            {
                Goods goods = bill.ListGoods[i];
                if ((goods.DJHXZ == 3) || (goods.DJHXZ == 4))
                {
                    num++;
                    if (this.Flag_Hzfw)
                    {
                        if (num > 7)
                        {
                            return false;
                        }
                    }
                    else if (num > 8)
                    {
                        return false;
                    }
                    if (goods.DJHXZ == 4)
                    {
                        num = 0;
                    }
                }
                else
                {
                    num = 0;
                }
            }
            return true;
        }

        public string ConvertToReportMsg(CheckResult checkResult)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("【作废状态】  ");
            builder.Append(checkResult.Zfzt);
            builder.Append("\n【使用状态】  ");
            builder.Append(checkResult.Kpzt);
            if (!checkResult.HasWrong)
            {
                builder.Append("\n【数据状态】  正常\n");
            }
            else
            {
                string current;
                builder.Append("\n【数据状态】  有错\n");
                if (checkResult.listErrorDJ.Count > 0)
                {
                    StringBuilder builder2 = new StringBuilder();
                    using (List<string>.Enumerator enumerator = checkResult.listErrorDJ.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            current = enumerator.Current;
                            if (!current.StartsWith("CF:"))
                            {
                                builder2.Append(current);
                            }
                        }
                    }
                    if (builder2.Length > 0)
                    {
                        builder.Append("\n————————单据错误————————\n");
                        builder.Append(builder2);
                    }
                }
                if (checkResult.listErrorMX.Count > 0)
                {
                    StringBuilder builder3 = new StringBuilder();
                    for (int i = 0; i < checkResult.listErrorMX.Count; i++)
                    {
                        if (i > 9)
                        {
                            builder3.Append("...");
                            break;
                        }
                        current = checkResult.listErrorMX[i];
                        if (!current.StartsWith("CF:"))
                        {
                            builder3.Append(current);
                        }
                    }
                    if (builder3.Length > 0)
                    {
                        builder.Append("\n————————明细错误————————\n");
                        builder.Append(builder3);
                    }
                }
            }
            return builder.ToString();
        }

        public string ConvertToReportMsgForSplitBill(CheckResult checkResult)
        {
            StringBuilder builder = new StringBuilder();
            if (checkResult.listErrorDJ.Count > 0)
            {
                builder.Append("\n————————单据错误————————\n");
                foreach (string str in checkResult.listErrorDJ)
                {
                    if (str.StartsWith("CF:"))
                    {
                        builder.Append(str.Replace("CF:", ""));
                    }
                    else
                    {
                        builder.Append(str);
                    }
                }
            }
            if (checkResult.listErrorMX.Count > 0)
            {
                builder.Append("\n————————明细错误————————\n");
                foreach (string str in checkResult.listErrorMX)
                {
                    if (str.StartsWith("CF:"))
                    {
                        builder.Append(str.Replace("CF:", ""));
                    }
                    else
                    {
                        builder.Append(str);
                    }
                }
            }
            string str2 = builder.ToString();
            if (str2.Length == 0)
            {
                str2 = "0";
            }
            return str2;
        }

        internal bool IsOverEWM(SaleBill bill)
        {
            int count = bill.ListGoods.Count;
            return this.IsOverEWM(bill, 0, count);
        }

        internal bool IsOverEWM(SaleBill bill, int StartXH, int RowCount)
        {
            if ((RowCount == 0) || (bill.ListGoods.Count == 0))
            {
                return false;
            }
            Fpxx fpxx = this.ChangeToFoxx(bill, StartXH, RowCount);
            return !this.invoiceHandler.CheckHzfwFpxxForWBJK(fpxx);
        }

        public int IsOverLimit(FPLX fpzl, SplittingField field, string fieldContent)
        {
            int num = 0;
            if (!string.IsNullOrEmpty(fieldContent))
            {
                char ch;
                int num8;
                double num2 = 0.0;
                double num3 = 0.0;
                double num4 = 0.0;
                if (fpzl == 0)
                {
                    if (field == 0)
                    {
                        num2 = 194.0;
                        num3 = 1.0;
                        num4 = 10.0;
                    }
                    else if (field == 1)
                    {
                        num2 = 46.0;
                        num3 = 1.0;
                        num4 = 1.0;
                    }
                }
                else if (fpzl == 2)
                {
                    if (field == 0)
                    {
                        num2 = 194.0;
                        num3 = 1.0;
                        num4 = 10.0;
                    }
                    else if (field == 1)
                    {
                        num2 = 46.0;
                        num3 = 1.0;
                        num4 = 1.0;
                    }
                }
                double num5 = 0.0;
                int num6 = 12;
                List<string> list = new List<string>();
                int num7 = 0;
                num7 = 0;
                while (num7 < fieldContent.Length)
                {
                    ch = fieldContent[num7];
                    num8 = num6;
                    if (ToolUtil.GetByteCount(ch.ToString()) == 1)
                    {
                        num8 = num6 / 2;
                    }
                    if (((num5 + num8) + 1.5) > (num2 - 10.0))
                    {
                        break;
                    }
                    num5 += num8 + num3;
                    num7++;
                }
                if (num7 == fieldContent.Length)
                {
                    return num;
                }
                num6 = 7;
                num5 = 0.0;
                num7 = 0;
                while (num7 < fieldContent.Length)
                {
                    ch = fieldContent[num7];
                    num8 = num6;
                    if (ToolUtil.GetByteCount(ch.ToString()) == 1)
                    {
                        num8 = ((num6 % 2) == 0) ? (num6 / 2) : ((num6 / 2) + 1);
                    }
                    if ((num5 + num8) > (num2 - num4))
                    {
                        break;
                    }
                    num5 += num8 + num3;
                    num7++;
                }
                if (num7 == fieldContent.Length)
                {
                    return 1;
                }
                num5 = 0.0;
                while (num7 < fieldContent.Length)
                {
                    ch = fieldContent[num7];
                    num8 = num6;
                    if (ToolUtil.GetByteCount(ch.ToString()) == 1)
                    {
                        num8 = ((num6 % 2) == 0) ? (num6 / 2) : ((num6 / 2) + 1);
                    }
                    if ((num5 + num8) > (num2 - num4))
                    {
                        break;
                    }
                    num5 += num8 + num3;
                    num7++;
                }
                if (num7 == fieldContent.Length)
                {
                    num = 2;
                }
                else
                {
                    num = 3;
                }
            }
            return num;
        }

        private bool PosiInvTypeNo(string Str, InvType InvoiceKind)
        {
            string infoFromNotes;
            string str2;
            string str3;
            string str4;
            string str5;
            string str6;
            bool flag2;
            if (InvoiceKind == InvType.Special)
            {
                infoFromNotes = CommonTool.GetInfoFromNotes(Str, 1);
                if (string.IsNullOrEmpty(infoFromNotes))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(infoFromNotes))
                {
                    return false;
                }
                str2 = "s";
                if (infoFromNotes.Substring(0, 6).Equals("000000"))
                {
                    return false;
                }
                if (!NotesUtil.CheckTzdh(infoFromNotes, str2))
                {
                    return false;
                }
                return true;
            }
            if (InvoiceKind == InvType.Common)
            {
                str3 = string.Empty;
                str4 = string.Empty;
                infoFromNotes = CommonTool.GetInfoFromNotes(Str, 0);
                if (!string.IsNullOrEmpty(infoFromNotes))
                {
                    str3 = infoFromNotes.Substring(0, infoFromNotes.Length - 8);
                    str4 = infoFromNotes.Substring(infoFromNotes.Length - 8, 8);
                }
                str5 = new string('0', str3.Length);
                str6 = new string('0', str4.Length);
                flag2 = (str3 == str5) || (str4 == str6);
                return ((((str3.Length == 10) || (str3.Length == 12)) && (str4.Length == 8)) && !flag2);
            }
            if (InvoiceKind == InvType.transportation)
            {
                infoFromNotes = CommonTool.GetInfoFromNotes(Str, 2);
                if (string.IsNullOrEmpty(infoFromNotes))
                {
                    return false;
                }
                str2 = "f";
                if (infoFromNotes.Substring(0, 6).Equals("000000"))
                {
                    return false;
                }
                if (!NotesUtil.CheckTzdh(infoFromNotes, str2))
                {
                    return false;
                }
                return true;
            }
            if (InvoiceKind == InvType.vehiclesales)
            {
                str3 = string.Empty;
                str4 = string.Empty;
                infoFromNotes = CommonTool.GetInfoFromNotes(Str, 0);
                if (!string.IsNullOrEmpty(infoFromNotes))
                {
                    str3 = infoFromNotes.Substring(0, infoFromNotes.Length - 8);
                    str4 = infoFromNotes.Substring(infoFromNotes.Length - 8, 8);
                }
                str5 = new string('0', str3.Length);
                str6 = new string('0', str4.Length);
                flag2 = (str3 == str5) || (str4 == str6);
                bool flag3 = false;
                if (str3.Length == 12)
                {
                    flag3 = true;
                }
                return ((flag3 && (str4.Length == 8)) && !flag2);
            }
            return false;
        }

        private List<string> QueryPZSQ_Info(string PZ, string SQ)
        {
            string str = ",";
            string str2 = "-";
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            List<string> list = new List<string>();
            string[] strArray = SQ.Split(new string[] { str }, StringSplitOptions.None);
            foreach (string str3 in strArray)
            {
                list.Add(str3);
            }
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                string str4 = list[i];
                List<string> list2 = new List<string>();
                string[] strArray2 = str4.Split(new string[] { str2 }, StringSplitOptions.None);
                foreach (string str5 in strArray2)
                {
                    list2.Add(str5);
                }
                if (list2.Count > 0)
                {
                    string key = list2[0];
                    dictionary.Add(key, list2);
                }
            }
            if (dictionary.ContainsKey(PZ))
            {
                return dictionary[PZ];
            }
            return null;
        }

        public void SaveCheckResult(CheckResult result, SaleBill bill)
        {
            if (!result.HasWrong)
            {
                if (bill.DJZT == "N")
                {
                    this.dbBill.UpdateDJZT(bill.BH, "Y");
                    bill.DJZT = "Y";
                }
            }
            else if ((bill.DJZT == "Y") || (bill.DJZT.Trim() == ""))
            {
                this.dbBill.UpdateDJZT(bill.BH, "N");
                bill.DJZT = "N";
            }
            if (bill.DJZT == "W")
            {
                result.Zfzt = "已作废";
            }
            else
            {
                result.Zfzt = "未作废";
            }
        }

        public static SaleBillCheck Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SaleBillCheck();
                }
                return instance;
            }
        }
    }
}

