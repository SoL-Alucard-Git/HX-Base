namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Forms;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class SaleBillCtrl
    {
        private SaleBillDAL dbBill = new SaleBillDAL();
        private static SaleBillCtrl instance = null;
        private ILog log = LogUtil.GetLogger<SaleBillCtrl>();
        public string XGDJZL = "";

        private SaleBillCtrl()
        {
        }

        public string AutoSeparate(SaleBill bill, SeparateType SepType, bool MergeSmallJE, bool ExInfoSplit, int[] slvjeseIndex, bool hzfw)
        {
            List<SaleBill> listBill = new List<SaleBill>();
            int num = hzfw ? 1 : 0;
            string str = this.AutoSeparateBase(bill, SepType, MergeSmallJE, ExInfoSplit, listBill, slvjeseIndex, num);
            if (str == "0")
            {
                string str2 = this.dbBill.SaveToTempTable(listBill, 1);
                if (str2 != "0")
                {
                    return ("[sql]" + str2);
                }
            }
            return str;
        }

        internal string AutoSeparateBase(SaleBill bill, SeparateType SepType, bool MergeSmallJE, bool ExInfoSplit, List<SaleBill> listBill, int[] slvjeseIndex, bool IsKP)
        {
            string str18;
            try
            {
                double softAAmountPre;
                double num3;
                int num5;
                Goods goods;
                TaxCard card = TaxCardFactory.CreateTaxCard();
                bool flag = card.blQDEWM();
                bool flag2 = card.get_StateInfo().CompanyType > 0;
                string separateReason = "";
                List<Goods> list = new List<Goods>();
                double invLimit = TaxCardValue.GetInvLimit(bill.DJZL);
                bool hYSY = bill.HYSY;
                bool flag4 = bill.JZ_50_15;
                if (TaxCardValue.taxCard.get_ECardType() == 0)
                {
                    softAAmountPre = TaxCardValue.SoftAAmountPre;
                    num3 = TaxCardValue.SoftTaxLimit - 0.01;
                }
                else
                {
                    softAAmountPre = TaxCardValue.SoftATaxPre;
                    num3 = invLimit;
                }
                for (num5 = 0; num5 < bill.ListGoods.Count; num5++)
                {
                    goods = bill.ListGoods[num5];
                    goods.Reserve = goods.XH.ToString();
                    list.Add(goods);
                }
                if (MergeSmallJE)
                {
                    list.Sort(new ComparerMXje());
                }
                double num6 = 0.0;
                double num7 = 0.0;
                double num8 = 0.0;
                int num9 = 0;
                int num10 = 1;
                double sLV = list[0].SLV;
                List<Goods> listMXtmp = new List<Goods>();
                double num12 = 0.0;
                double num13 = 0.0;
                double num14 = 0.0;
                FPGenerateResult result = new FPGenerateResult(bill);
                string[] strArray = new string[0x10];
                Invoice invoice2 = GenerateInvoice.Instance.CreateInvoiceHead(result, slvjeseIndex, ref strArray, bill);
                string maxBMBBBH = new SPFLService().GetMaxBMBBBH();
                invoice2.set_Bmbbbh(maxBMBBBH);
                int num15 = 0;
                int num16 = -1;
                int num17 = 0;
                int num18 = -1;
                bool flag5 = false;
                for (num5 = 0; num5 < list.Count; num5++)
                {
                    double num19;
                Label_01B5:
                    goods = list[num5];
                    bool flag6 = false;
                    if (hYSY && (sLV == 0.05))
                    {
                        flag6 = true;
                    }
                    bool flag7 = false;
                    if (flag4 && (sLV == 0.015))
                    {
                        flag7 = true;
                    }
                    if (flag6)
                    {
                        invoice2.SetZyfpLx(1);
                        num19 = Math.Abs((double) (goods.JE - ((goods.SE * (1.0 - goods.SLV)) / goods.SLV)));
                        num8 += num19;
                    }
                    else if (flag7)
                    {
                        invoice2.SetZyfpLx(10);
                        num19 = Math.Abs((double) (goods.JE - ((goods.SE * 1.035) / goods.SLV)));
                        num8 += num19;
                    }
                    else
                    {
                        num19 = (goods.SLV == 0.0) ? 0.0 : Math.Abs((double) (goods.JE - (goods.SE / goods.SLV)));
                        num8 += num19;
                    }
                    bool flag8 = false;
                    bool flag9 = !(goods.SLV == sLV);
                    bool flag10 = false;
                    if ((card.get_ECardType() == 3) && (card.get_StateInfo().TBType == 0))
                    {
                        flag10 = true;
                    }
                    bool flag11 = false;
                    if (flag10)
                    {
                        flag11 = true;
                    }
                    else
                    {
                        flag11 = !flag9;
                    }
                    bool flag12 = true;
                    if (flag11 && (num12 == 0.0))
                    {
                        string code;
                        if (num18 == -1)
                        {
                            num18 = num5;
                        }
                        if (goods.DJHXZ == 4)
                        {
                            string str3 = Convert.ToString(goods.JE);
                            string str4 = Convert.ToString(goods.SE);
                            int zKH = 0;
                            double round = 0.0;
                            if (CommonTool.isSPBMVersion())
                            {
                                double jE = bill.ListGoods[num5 - 1].JE;
                                round = CommonTool.GetDisCount(Math.Abs(goods.JE), jE, ref zKH);
                            }
                            else
                            {
                                round = GetRound(CommonTool.GetDisCount(goods.SPMC, ref zKH), 5);
                            }
                            if (CommonTool.isSPBMVersion())
                            {
                                string str6 = goods.XSYH ? "1" : "0";
                                invoice2.SetSpbh(num5, goods.SPBM);
                                invoice2.SetXsyh(num5, str6);
                                invoice2.SetLslvbs(num5, goods.LSLVBS);
                                invoice2.SetYhsm(num5, goods.XSYHSM);
                                invoice2.SetFlbm(num5, goods.FLBM);
                            }
                            string str7 = ((decimal) round).ToString();
                            int count = invoice2.GetSpxxs().Count;
                            if (invoice2.AddZkxx(count - 1, zKH, str7, str3, str4) < 0)
                            {
                                flag12 = false;
                                code = invoice2.GetCode();
                            }
                            num15++;
                            if ((num15 > 7) && !invoice2.get_Qdbz())
                            {
                                return "-1";
                            }
                            flag5 = true;
                            num15 = 0;
                        }
                        else
                        {
                            string sPMC;
                            string sPSM;
                            string str11;
                            string str12;
                            ZYFP_LX zyfp_lx;
                            Spxx spxx;
                            if (goods.DJHXZ == 3)
                            {
                                sPMC = goods.SPMC;
                                sPSM = goods.SPSM;
                                str11 = Convert.ToString(goods.SLV);
                                str12 = Convert.ToString((decimal) goods.DJ);
                                if (flag6)
                                {
                                    zyfp_lx = 1;
                                }
                                else if (flag7)
                                {
                                    zyfp_lx = 10;
                                }
                                else
                                {
                                    zyfp_lx = 0;
                                }
                                if ((bill.DJZL == "s") && (zyfp_lx == 0))
                                {
                                    switch (this.xtCheck(sPMC))
                                    {
                                        case "1":
                                            zyfp_lx = 6;
                                            invoice2.SetZyfpLx(zyfp_lx);
                                            break;

                                        case "2":
                                            zyfp_lx = 7;
                                            invoice2.SetZyfpLx(zyfp_lx);
                                            break;
                                    }
                                }
                                spxx = new Spxx(sPMC, sPSM, str11, goods.GGXH, goods.JLDW, str12, goods.HSJBZ, zyfp_lx);
                                spxx.set_Je(Convert.ToString(goods.JE));
                                spxx.set_Se(Convert.ToString(goods.SE));
                                spxx.set_SL(Convert.ToString((decimal) goods.SL));
                                spxx.set_Flbm(goods.FLBM);
                                spxx.set_Xsyh(goods.XSYH ? "1" : "0");
                                spxx.set_Yhsm(goods.XSYHSM);
                                spxx.set_Lslvbs(goods.LSLVBS);
                                spxx.set_Spbh(goods.SPBM);
                                if (invoice2.AddSpxx(spxx) < 0)
                                {
                                    flag12 = false;
                                    code = invoice2.GetCode();
                                }
                                num15++;
                                if ((num15 > 7) && !invoice2.get_Qdbz())
                                {
                                    return "-1";
                                }
                                if (flag5 || (num16 == -1))
                                {
                                    num16 = num5;
                                    flag5 = false;
                                }
                            }
                            else
                            {
                                sPMC = goods.SPMC;
                                sPSM = goods.SPSM;
                                str11 = Convert.ToString(goods.SLV);
                                str12 = Convert.ToString((decimal) goods.DJ);
                                if (flag6)
                                {
                                    zyfp_lx = 1;
                                }
                                else if (flag7)
                                {
                                    zyfp_lx = 10;
                                }
                                else
                                {
                                    zyfp_lx = 0;
                                }
                                if ((bill.DJZL == "s") && (zyfp_lx == 0))
                                {
                                    switch (this.xtCheck(sPMC))
                                    {
                                        case "1":
                                            zyfp_lx = 6;
                                            invoice2.SetZyfpLx(zyfp_lx);
                                            break;

                                        case "2":
                                            zyfp_lx = 7;
                                            invoice2.SetZyfpLx(zyfp_lx);
                                            break;
                                    }
                                }
                                spxx = new Spxx(sPMC, sPSM, str11, goods.GGXH, goods.JLDW, str12, goods.HSJBZ, zyfp_lx);
                                spxx.set_Je(Convert.ToString(goods.JE));
                                spxx.set_Se(Convert.ToString(goods.SE));
                                spxx.set_SL(Convert.ToString((decimal) goods.SL));
                                spxx.set_Flbm(goods.FLBM);
                                spxx.set_Xsyh(goods.XSYH ? "1" : "0");
                                spxx.set_Yhsm(goods.XSYHSM);
                                spxx.set_Lslvbs(goods.LSLVBS);
                                spxx.set_Spbh(goods.SPBM);
                                if (invoice2.AddSpxx(spxx) < 0)
                                {
                                    flag12 = false;
                                    code = invoice2.GetCode();
                                }
                                num15 = 0;
                                num16 = -1;
                                flag5 = false;
                            }
                        }
                        bool flag14 = false;
                        if (flag12)
                        {
                            num13 += goods.JE;
                            num14 += goods.SE;
                            strArray[slvjeseIndex[10]] = invoice2.GetHjSe();
                            strArray[slvjeseIndex[14]] = invoice2.get_SLv();
                            strArray[slvjeseIndex[15]] = invoice2.GetHjJeNotHs();
                            byte[] buffer = new byte[0x10];
                            object[] objArray = new object[] { buffer, strArray };
                            invoice2.SetData(objArray);
                            if (ExInfoSplit)
                            {
                                if (invoice2.get_Qdbz() && (list.Count > 200))
                                {
                                    double num24 = 0.0;
                                    double num25 = 0.0;
                                    double num26 = 0.0;
                                    if (flag6)
                                    {
                                        num24 = GetRound((double) (num6 + (goods.JE + goods.SE)), 2);
                                    }
                                    else
                                    {
                                        num24 = GetRound((double) (num6 + goods.JE), 2);
                                    }
                                    num25 = GetRound((double) (num7 + goods.SE), 2);
                                    if (flag6)
                                    {
                                        num26 = GetRound((double) (num24 * 0.05), 2) - num25;
                                    }
                                    else if (flag7)
                                    {
                                        num26 = GetRound((double) ((num24 / 1.035) * 0.015), 2) - num25;
                                    }
                                    else
                                    {
                                        num26 = GetRound((double) (num24 * goods.SLV), 2) - num25;
                                    }
                                    num26 = GetRound(num26, 2);
                                    if ((num24 > invLimit) || (Math.Abs(GetRound(num26, 2)) > 1.27))
                                    {
                                        if (flag2)
                                        {
                                            flag14 = invoice2.CheckFpData();
                                        }
                                        else
                                        {
                                            flag14 = invoice2.CheckFpData_WBJK_ChaiFen();
                                        }
                                    }
                                    else
                                    {
                                        flag14 = true;
                                    }
                                }
                                else if (flag2)
                                {
                                    flag14 = invoice2.CheckFpData();
                                }
                                else
                                {
                                    flag14 = invoice2.CheckFpData_WBJK_ChaiFen();
                                }
                            }
                        }
                        if (!flag14)
                        {
                            string str14 = invoice2.GetCode();
                            if (str14 != "0000")
                            {
                                if ((str14 == "A059") || (str14 == "A058"))
                                {
                                    return "-2";
                                }
                                if ((str14 == "A015") && (bill.JEHJ < 0.0))
                                {
                                    return "-5";
                                }
                                if ((str14 == "A028") && (bill.JEHJ < 0.0))
                                {
                                    return "-4";
                                }
                                if (((!(str14 == "A028") || (goods.DJHXZ != 3)) && (!(str14 == "A016") || (goods.DJHXZ != 4))) && (((((str14 != "A079") && (str14 != "A080")) && ((str14 != "A081") && (str14 != "A082"))) && (str14 != "A083")) && !(str14 == "A084")))
                                {
                                    flag8 = true;
                                    if ((goods.DJHXZ == 3) || (goods.DJHXZ == 4))
                                    {
                                        if (num16 == num18)
                                        {
                                            return "-1";
                                        }
                                        if (num16 < num18)
                                        {
                                            return "-1";
                                        }
                                        listMXtmp.Clear();
                                        for (int j = num18; j < num16; j++)
                                        {
                                            Goods item = list[j];
                                            item.XSDJBH = string.Format("{0}_{1}", bill.BH, num9);
                                            listMXtmp.Add(item);
                                        }
                                        num5 = num16;
                                    }
                                }
                            }
                        }
                    }
                    bool flag15 = false;
                    if (flag10)
                    {
                        flag15 = !flag8;
                    }
                    else
                    {
                        flag15 = !flag8 && !flag9;
                    }
                    if (!flag15)
                    {
                    }
                    if (flag15)
                    {
                        if (flag6)
                        {
                            num6 += GetRound((double) (goods.JE + goods.SE), 2);
                        }
                        else
                        {
                            num6 += goods.JE;
                        }
                        num7 += goods.SE;
                        if (IsKP)
                        {
                            goods.XSDJBH = bill.BH;
                        }
                        else
                        {
                            goods.XSDJBH = string.Format("{0}_{1}", bill.BH, num9);
                        }
                        num10++;
                        listMXtmp.Add(goods);
                        num17++;
                    }
                    else
                    {
                        if (listMXtmp.Count == 0)
                        {
                            string str15 = invoice2.GetCode();
                            if ((str15.Equals("A612") || str15.Equals("A613")) || str15.Equals("A052"))
                            {
                                return ("[-1]" + str15);
                            }
                            return "-3";
                        }
                        string str16 = invoice2.GetCode();
                        string[] @params = invoice2.Params;
                        if (!string.IsNullOrEmpty(str16))
                        {
                            if (str16 == "A028")
                            {
                                separateReason = "发票面额超出金税设备最大限额！";
                            }
                            else
                            {
                                separateReason = MessageManager.GetMessageInfo(str16, @params);
                            }
                        }
                        this.SetNewBill(separateReason, bill, ref num9, listMXtmp, listBill, ref num5, IsKP);
                        num6 = 0.0;
                        num7 = 0.0;
                        num10 = 1;
                        sLV = goods.SLV;
                        num8 = 0.0;
                        num13 = 0.0;
                        num14 = 0.0;
                        invoice2.DelSpxxAll();
                        num18 = -1;
                        num17 = 0;
                        num16 = -1;
                        num15 = 0;
                        flag5 = false;
                        goto Label_01B5;
                    }
                }
                if (!flag2 && (listBill.Count <= 1))
                {
                    invoice2.CheckFpData();
                    string str17 = invoice2.GetCode();
                    if (str17 != "0000")
                    {
                        if ((str17 == "A059") || (str17 == "A058"))
                        {
                            return "-2";
                        }
                        if ((str17 == "A015") && (bill.JEHJ < 0.0))
                        {
                            return "-5";
                        }
                        if ((str17 == "A028") && (bill.JEHJ < 0.0))
                        {
                            return "-4";
                        }
                    }
                }
                int i = 0;
                this.SetNewBill(separateReason, bill, ref num9, listMXtmp, listBill, ref i, IsKP);
                str18 = "0";
            }
            catch (Exception exception)
            {
                HandleException.Log.Error(exception.ToString());
                throw;
            }
            return str18;
        }

        internal string AutoSeparateBase(SaleBill bill, SeparateType SepType, bool MergeSmallJE, bool ExInfoSplit, List<SaleBill> listBill, int[] slvjeseIndex, int hzfw)
        {
            string str16;
            try
            {
                double softAAmountPre;
                double num3;
                int num5;
                Goods goods;
                bool flag9;
                MessageHelper.MsgWait("正在拆分单据，请稍候...");
                TaxCard card = TaxCardFactory.CreateTaxCard();
                bool flag = card.blQDEWM();
                bool flag2 = card.get_StateInfo().CompanyType > 0;
                bool flag3 = false;
                bool flag4 = false;
                bool flag5 = false;
                string separateReason = "";
                List<Goods> list = new List<Goods>();
                List<int> list2 = new List<int>();
                double invLimit = TaxCardValue.GetInvLimit(bill.DJZL);
                bool hYSY = bill.HYSY;
                bool flag7 = bill.JZ_50_15;
                if (TaxCardValue.taxCard.get_ECardType() == 0)
                {
                    softAAmountPre = TaxCardValue.SoftAAmountPre;
                    num3 = TaxCardValue.SoftTaxLimit - 0.01;
                }
                else
                {
                    softAAmountPre = TaxCardValue.SoftATaxPre;
                    num3 = invLimit;
                }
                for (num5 = 0; num5 < bill.ListGoods.Count; num5++)
                {
                    int num40;
                    goods = bill.ListGoods[num5];
                    goods.Reserve = goods.XH.ToString();
                    double round = 0.0;
                    double num7 = 0.0;
                    double num8 = 0.0;
                    bool flag8 = false;
                    flag9 = false;
                    bool hSJBZ = goods.HSJBZ;
                    if (hSJBZ)
                    {
                        if (hYSY && (goods.SLV == 0.05))
                        {
                            round = GetRound((double) (goods.JE + goods.SE), 2);
                            flag8 = true;
                        }
                        else if (flag7 && (goods.SLV == 0.015))
                        {
                            round = goods.JE;
                            flag9 = true;
                        }
                        else
                        {
                            round = goods.JE;
                        }
                    }
                    else if (flag7 && (goods.SLV == 0.015))
                    {
                        round = goods.JE;
                        flag9 = true;
                    }
                    else
                    {
                        round = goods.JE;
                    }
                    if (((goods.DJ != 0.0) && (goods.SL != 0.0)) && (Math.Abs(GetRound((double) ((goods.DJ * goods.SL) - round), 2)) > 0.01))
                    {
                        if (!goods.HSJBZ)
                        {
                            num40 = num5 + 1;
                            return ("[error_1_bhs]" + num40.ToString());
                        }
                        if (hYSY && (goods.SLV == 0.05))
                        {
                            num40 = num5 + 1;
                            return ("[error_1_hs]" + num40.ToString());
                        }
                        if (flag7 && (goods.SLV == 0.015))
                        {
                            if (((goods.DJ != 0.0) && (goods.SL != 0.0)) && (Math.Abs(GetRound((double) (((goods.DJ * goods.SL) - round) - goods.SE), 2)) > 0.01))
                            {
                                num40 = num5 + 1;
                                return ("[error_1_hs]" + num40.ToString());
                            }
                        }
                        else if (((goods.DJ != 0.0) && (goods.SL != 0.0)) && (Math.Abs(GetRound((double) (((goods.DJ * goods.SL) - round) - goods.SE), 2)) > 0.01))
                        {
                            num40 = num5 + 1;
                            return ("[error_1_hs]" + num40.ToString());
                        }
                    }
                    if ((((goods.DJ == 0.0) && (goods.SL != 0.0)) && (goods.JE != 0.0)) || (((goods.DJ != 0.0) && (goods.SL == 0.0)) && (goods.JE != 0.0)))
                    {
                        if (goods.HSJBZ)
                        {
                            num40 = num5 + 1;
                            return ("[error_1_hs]" + num40.ToString());
                        }
                        num40 = num5 + 1;
                        return ("[error_1_bhs]" + num40.ToString());
                    }
                    if ((goods.DJ < 0.0) && (goods.SL < 0.0))
                    {
                        num40 = num5 + 1;
                        return ("[error_2]" + num40.ToString());
                    }
                    if (goods.JE == 0.0)
                    {
                        num40 = num5 + 1;
                        return ("[error]" + num40.ToString());
                    }
                    if (round > invLimit)
                    {
                        if ((goods.DJHXZ != 3) && (goods.DJHXZ != 4))
                        {
                            while (round > invLimit)
                            {
                                Goods item = Goods.Clone(goods);
                                if (SepType == SeparateType.KeepMaxJE)
                                {
                                    if (hSJBZ)
                                    {
                                        if (flag8)
                                        {
                                            item.JE = GetRound((double) (invLimit * 0.95), 2);
                                            item.SE = GetRound((double) ((item.JE / 0.95) * 0.05), 2);
                                            item.SL = (item.DJ == 0.0) ? 0.0 : ((item.JE + item.SE) / item.DJ);
                                            num7 += item.SE;
                                            num7 = GetRound(num7, 2);
                                            flag3 = true;
                                        }
                                        else if (flag9)
                                        {
                                            item.JE = GetRound(invLimit, 2);
                                            item.SE = GetRound((double) ((invLimit / 1.035) * 0.015), 2);
                                            item.SL = (item.DJ == 0.0) ? 0.0 : ((item.JE + item.SE) / item.DJ);
                                            num7 += item.SE;
                                            num7 = GetRound(num7, 2);
                                        }
                                        else
                                        {
                                            item.JE = GetRound(invLimit, 2);
                                            item.SE = item.JE * item.SLV;
                                            item.SE = GetRound(item.SE, 2);
                                            item.SL = (item.DJ == 0.0) ? 0.0 : ((item.JE + item.SE) / item.DJ);
                                            num7 += item.SE;
                                            num7 = GetRound(num7, 2);
                                        }
                                    }
                                    else if (flag9)
                                    {
                                        item.JE = GetRound(invLimit, 2);
                                        item.SL = (item.DJ == 0.0) ? 0.0 : (item.JE / item.DJ);
                                        item.SE = GetRound((double) ((item.JE / 1.035) * 0.015), 2);
                                        num7 += item.SE;
                                        num7 = GetRound(num7, 2);
                                    }
                                    else
                                    {
                                        item.JE = GetRound(invLimit, 2);
                                        item.SL = (item.DJ == 0.0) ? 0.0 : (item.JE / item.DJ);
                                        item.SE = item.JE * item.SLV;
                                        item.SE = GetRound(item.SE, 2);
                                        num7 += item.SE;
                                        num7 = GetRound(num7, 2);
                                    }
                                }
                                else
                                {
                                    double d = 0.0;
                                    if (hSJBZ)
                                    {
                                        if (flag8)
                                        {
                                            d = (item.DJ == 0.0) ? 0.0 : (invLimit / item.DJ);
                                        }
                                        else if (flag9)
                                        {
                                            d = (item.DJ == 0.0) ? 0.0 : ((invLimit + ((invLimit / 1.035) * 0.015)) / item.DJ);
                                        }
                                        else
                                        {
                                            d = (item.DJ == 0.0) ? 0.0 : ((invLimit * (1.0 + item.SLV)) / item.DJ);
                                        }
                                    }
                                    else
                                    {
                                        d = (item.DJ == 0.0) ? 0.0 : (invLimit / item.DJ);
                                    }
                                    if ((d != 0.0) && (d < 1.0))
                                    {
                                        return "[-1]INP-272221";
                                    }
                                    d = Math.Floor(d);
                                    num8 += d;
                                    if (hSJBZ)
                                    {
                                        if (flag8)
                                        {
                                            item.SE = (item.DJ == 0.0) ? GetRound((double) (invLimit * 0.05), 2) : GetRound((double) ((d * item.DJ) * 0.05), 2);
                                            item.JE = (item.DJ == 0.0) ? (invLimit - item.SE) : ((d * item.DJ) - item.SE);
                                            item.JE = GetRound(item.JE, 2);
                                            item.SL = d;
                                            num7 += item.SE;
                                            num7 = GetRound(num7, 2);
                                            flag3 = true;
                                        }
                                        else if (flag9)
                                        {
                                            item.SE = (item.DJ == 0.0) ? GetRound((double) ((invLimit / 1.035) * 0.015), 2) : GetRound((double) (((d * item.DJ) / 1.035) * 0.015), 2);
                                            item.JE = (item.DJ == 0.0) ? invLimit : ((d * item.DJ) - item.SE);
                                            item.JE = GetRound(item.JE, 2);
                                            item.SL = d;
                                            num7 += item.SE;
                                            num7 = GetRound(num7, 2);
                                        }
                                        else
                                        {
                                            item.JE = (item.DJ == 0.0) ? invLimit : (d * (item.DJ / (1.0 + item.SLV)));
                                            item.JE = GetRound(item.JE, 2);
                                            item.SL = d;
                                            if (item.DJ == 0.0)
                                            {
                                                item.SE = item.JE * item.SLV;
                                                item.SE = GetRound(item.SE, 2);
                                            }
                                            else
                                            {
                                                item.SE = (item.SL * item.DJ) - item.JE;
                                                item.SE = GetRound(item.SE, 2);
                                            }
                                            num7 += item.SE;
                                            num7 = GetRound(num7, 2);
                                        }
                                    }
                                    else if (flag9)
                                    {
                                        item.JE = (item.DJ == 0.0) ? invLimit : (d * item.DJ);
                                        item.JE = GetRound(item.JE, 2);
                                        item.SL = d;
                                        item.SE = GetRound((double) ((item.JE / 1.035) * 0.015), 2);
                                        num7 += item.SE;
                                        num7 = GetRound(num7, 2);
                                    }
                                    else
                                    {
                                        item.JE = (item.DJ == 0.0) ? invLimit : (d * item.DJ);
                                        item.JE = GetRound(item.JE, 2);
                                        item.SL = d;
                                        item.SE = item.JE * item.SLV;
                                        item.SE = GetRound(item.SE, 2);
                                        num7 += item.SE;
                                        num7 = GetRound(num7, 2);
                                    }
                                }
                                list.Add(item);
                                goods.JE -= item.JE;
                                goods.JE = GetRound(goods.JE, 2);
                                if (bill.HYSY && (goods.SLV == 0.05))
                                {
                                    round -= item.JE + item.SE;
                                }
                                else
                                {
                                    if (bill.JZ_50_15 && (goods.SLV == 0.015))
                                    {
                                        round = goods.JE;
                                        continue;
                                    }
                                    round = goods.JE;
                                }
                            }
                            int count = list.Count;
                            list2.Add(count);
                        }
                        if (flag8)
                        {
                            goods.SE = GetRound((double) (bill.ListGoods[num5].SE - num7), 2);
                            if (SepType == SeparateType.KeepMaxJE)
                            {
                                goods.SL = (goods.DJ == 0.0) ? 0.0 : ((goods.JE + goods.SE) / goods.DJ);
                            }
                            else
                            {
                                goods.SL = (goods.DJ == 0.0) ? 0.0 : (goods.SL - num8);
                            }
                        }
                        else if (flag9)
                        {
                            goods.SE = GetRound((double) (bill.ListGoods[num5].SE - num7), 2);
                            if (SepType == SeparateType.KeepMaxJE)
                            {
                                if (hSJBZ)
                                {
                                    goods.SL = (goods.DJ == 0.0) ? 0.0 : ((goods.JE + goods.SE) / goods.DJ);
                                }
                                else
                                {
                                    goods.SL = (goods.DJ == 0.0) ? 0.0 : (goods.JE / goods.DJ);
                                }
                            }
                            else
                            {
                                goods.SL = (goods.DJ == 0.0) ? 0.0 : (goods.SL - num8);
                            }
                        }
                        else
                        {
                            goods.SE = GetRound((double) (bill.ListGoods[num5].SE - num7), 2);
                            if (SepType == SeparateType.KeepMaxJE)
                            {
                                if (hSJBZ)
                                {
                                    goods.SL = (goods.DJ == 0.0) ? 0.0 : ((goods.JE + goods.SE) / goods.DJ);
                                }
                                else
                                {
                                    goods.SL = (goods.DJ == 0.0) ? 0.0 : (goods.JE / goods.DJ);
                                }
                            }
                            else
                            {
                                goods.SL = (goods.DJ == 0.0) ? 0.0 : (goods.SL - num8);
                            }
                        }
                    }
                    list.Add(goods);
                }
                if (MergeSmallJE)
                {
                    List<Goods> list3 = new List<Goods>();
                    List<Goods> list4 = new List<Goods>();
                    for (num5 = 0; num5 < list.Count; num5++)
                    {
                        if ((list[num5].DJHXZ == 3) || (list[num5].DJHXZ == 4))
                        {
                            list4.Add(list[num5]);
                        }
                        else
                        {
                            list3.Add(list[num5]);
                        }
                    }
                    list3.Sort(new ComparerMXje());
                    for (num5 = 0; num5 < list4.Count; num5++)
                    {
                        list3.Add(list4[num5]);
                    }
                    list.Clear();
                    for (num5 = 0; num5 < list3.Count; num5++)
                    {
                        list.Add(list3[num5]);
                    }
                }
                double num11 = 0.0;
                double num12 = 0.0;
                double num13 = 0.0;
                double num14 = 0.0;
                int num15 = 0;
                int num16 = 1;
                double sLV = list[0].SLV;
                List<Goods> listMXtmp = new List<Goods>();
                double num18 = 0.0;
                double num19 = 0.0;
                double num20 = 0.0;
                FPGenerateResult result = new FPGenerateResult(bill);
                string[] strArray = new string[0x10];
                Invoice invoice2 = GenerateInvoice.Instance.CreateInvoiceHead(result, slvjeseIndex, ref strArray, bill);
                if (flag3)
                {
                    invoice2.set_IsRed(false);
                }
                if (hzfw == 1)
                {
                    if (ExInfoSplit)
                    {
                        invoice2.SetQdbz(false);
                    }
                    else
                    {
                        invoice2.SetQdbz(true);
                    }
                    ExInfoSplit = true;
                }
                else
                {
                    invoice2.SetQdbz(true);
                }
                int num22 = 0;
                int num23 = -1;
                int num24 = 0;
                int num25 = -1;
                bool flag11 = false;
                int num26 = 0;
                for (num5 = 0; num5 < list.Count; num5++)
                {
                    double num27;
                    string str13;
                    bool flag22;
                    if (list[num5].DJHXZ == 4)
                    {
                        num26++;
                    }
                Label_139C:
                    goods = list[num5];
                    bool flag12 = false;
                    if (hYSY && (sLV == 0.05))
                    {
                        flag12 = true;
                    }
                    flag9 = false;
                    if (flag7 && (sLV == 0.015))
                    {
                        flag9 = true;
                    }
                    bool flag13 = false;
                    if (flag12)
                    {
                        invoice2.SetZyfpLx(1);
                        num27 = ((goods.JE + goods.SE) * 0.05) - goods.SE;
                        num14 += num27;
                        if (GetRound(num27, 2) > 0.06)
                        {
                            flag13 = true;
                        }
                    }
                    else if (flag9)
                    {
                        invoice2.SetZyfpLx(10);
                        num27 = (((goods.JE + goods.SE) / 1.05) * 0.015) - goods.SE;
                        num14 += num27;
                        if (GetRound(num27, 2) > 0.06)
                        {
                            flag13 = true;
                        }
                    }
                    else
                    {
                        num27 = (goods.JE * goods.SLV) - goods.SE;
                        num14 += num27;
                        if (GetRound(num27, 2) > 0.06)
                        {
                            flag13 = true;
                        }
                    }
                    bool flag14 = false;
                    bool flag15 = false;
                    if (flag12)
                    {
                        double num28 = GetRound((double) (goods.JE + goods.SE), 2);
                        if (goods.DJHXZ == 3)
                        {
                            num13 += num28;
                            num13 = GetRound(num13, 2);
                        }
                        else if (goods.DJHXZ == 4)
                        {
                            num13 += num28;
                            if (GetRound(num13, 2) > invLimit)
                            {
                                return ("group" + num26.ToString());
                            }
                            num13 = 0.0;
                            flag15 = (num11 + num28) > invLimit;
                        }
                        else
                        {
                            flag15 = (num11 + num28) > invLimit;
                        }
                    }
                    else if (goods.DJHXZ == 3)
                    {
                        num13 += goods.JE;
                        num13 = GetRound(num13, 2);
                    }
                    else if (goods.DJHXZ == 4)
                    {
                        num13 += goods.JE;
                        if (GetRound(num13, 2) > invLimit)
                        {
                            return ("group" + num26);
                        }
                        num13 = 0.0;
                        flag15 = (num11 + goods.JE) > invLimit;
                    }
                    else
                    {
                        flag15 = (num11 + goods.JE) > invLimit;
                    }
                    bool flag16 = !(goods.SLV == sLV);
                    bool flag17 = false;
                    if ((card.get_ECardType() == 3) && (card.get_StateInfo().TBType == 0))
                    {
                        flag17 = true;
                    }
                    bool flag18 = false;
                    if (flag17)
                    {
                        flag18 = true;
                    }
                    else
                    {
                        flag18 = !flag16;
                    }
                    bool flag19 = true;
                    if (flag18 && (num18 == 0.0))
                    {
                        string code;
                        if (num25 == -1)
                        {
                            num25 = num5;
                        }
                        if (goods.DJHXZ == 4)
                        {
                            string str2 = Convert.ToString(goods.JE);
                            string str3 = Convert.ToString(goods.SE);
                            int zKH = 0;
                            double num30 = 0.0;
                            if (CommonTool.isSPBMVersion())
                            {
                                double jE = bill.ListGoods[num5 - 1].JE;
                                num30 = CommonTool.GetDisCount(Math.Abs(goods.JE), jE, ref zKH);
                            }
                            else
                            {
                                num30 = GetRound(CommonTool.GetDisCount(goods.SPMC, ref zKH), 5);
                            }
                            string str5 = ((decimal) num30).ToString();
                            int num32 = invoice2.GetSpxxs().Count;
                            if (invoice2.AddZkxx(num32 - 1, zKH, str5, str2, str3) < 0)
                            {
                                flag19 = false;
                                code = invoice2.GetCode();
                            }
                            num22++;
                            if (num22 > 7)
                            {
                                return "-1";
                            }
                            flag11 = true;
                            num22 = 0;
                        }
                        else
                        {
                            string sPMC;
                            string sPSM;
                            string str9;
                            string str10;
                            ZYFP_LX zyfp_lx;
                            Spxx spxx;
                            if (goods.DJHXZ == 3)
                            {
                                sPMC = goods.SPMC;
                                sPSM = goods.SPSM;
                                str9 = Convert.ToString(goods.SLV);
                                str10 = Convert.ToString((decimal) goods.DJ);
                                if (flag12)
                                {
                                    zyfp_lx = 1;
                                }
                                else if (flag9)
                                {
                                    zyfp_lx = 10;
                                }
                                else
                                {
                                    zyfp_lx = 0;
                                }
                                if ((bill.DJZL == "s") && (zyfp_lx == 0))
                                {
                                    switch (this.xtCheck(sPMC))
                                    {
                                        case "1":
                                            zyfp_lx = 6;
                                            invoice2.SetZyfpLx(zyfp_lx);
                                            break;

                                        case "2":
                                            zyfp_lx = 7;
                                            invoice2.SetZyfpLx(zyfp_lx);
                                            break;
                                    }
                                }
                                spxx = new Spxx(sPMC, sPSM, str9, goods.GGXH, goods.JLDW, str10, goods.HSJBZ, zyfp_lx);
                                if (!sPMC.Equals(spxx.get_Spmc()))
                                {
                                    return "[-1]A612";
                                }
                                spxx.set_Je(Convert.ToString(goods.JE));
                                spxx.set_Se(Convert.ToString(goods.SE));
                                spxx.set_SL(Convert.ToString((decimal) goods.SL));
                                spxx.set_Flbm(goods.FLBM);
                                spxx.set_Xsyh(goods.XSYH ? "1" : "0");
                                spxx.set_Yhsm(goods.XSYHSM);
                                spxx.set_Lslvbs(goods.LSLVBS);
                                spxx.set_Spbh(goods.SPBM);
                                if (invoice2.AddSpxx(spxx) < 0)
                                {
                                    flag19 = false;
                                    code = invoice2.GetCode();
                                }
                                num22++;
                                if (num22 > 7)
                                {
                                    return "-1";
                                }
                                if (flag11 || (num23 == -1))
                                {
                                    num23 = num5;
                                    flag11 = false;
                                }
                            }
                            else
                            {
                                sPMC = goods.SPMC;
                                sPSM = goods.SPSM;
                                str9 = Convert.ToString(goods.SLV);
                                str10 = Convert.ToString((decimal) goods.DJ);
                                if (flag12)
                                {
                                    zyfp_lx = 1;
                                }
                                else if (flag9)
                                {
                                    zyfp_lx = 10;
                                }
                                else
                                {
                                    zyfp_lx = 0;
                                }
                                if ((bill.DJZL == "s") && (zyfp_lx == 0))
                                {
                                    switch (this.xtCheck(sPMC))
                                    {
                                        case "1":
                                            zyfp_lx = 6;
                                            invoice2.SetZyfpLx(zyfp_lx);
                                            break;

                                        case "2":
                                            zyfp_lx = 7;
                                            invoice2.SetZyfpLx(zyfp_lx);
                                            break;
                                    }
                                }
                                spxx = new Spxx(sPMC, sPSM, str9, goods.GGXH, goods.JLDW, str10, goods.HSJBZ, zyfp_lx);
                                if (!sPMC.Equals(spxx.get_Spmc()))
                                {
                                    return "[-1]A612";
                                }
                                spxx.set_Je(Convert.ToString(goods.JE));
                                spxx.set_Se(Convert.ToString(goods.SE));
                                spxx.set_SL(Convert.ToString((decimal) goods.SL));
                                spxx.set_Flbm(goods.FLBM);
                                spxx.set_Xsyh(goods.XSYH ? "1" : "0");
                                spxx.set_Yhsm(goods.XSYHSM);
                                spxx.set_Lslvbs(goods.LSLVBS);
                                spxx.set_Spbh(goods.SPBM);
                                if (invoice2.AddSpxx(spxx) < 0)
                                {
                                    flag19 = false;
                                    code = invoice2.GetCode();
                                }
                                num22 = 0;
                                num23 = -1;
                                flag11 = false;
                            }
                        }
                        double num33 = num11 + goods.JE;
                        bool flag21 = false;
                        if (flag19)
                        {
                            num19 += goods.JE;
                            num20 += goods.SE;
                            strArray[slvjeseIndex[10]] = invoice2.GetHjSe();
                            strArray[slvjeseIndex[14]] = invoice2.get_SLv();
                            strArray[slvjeseIndex[15]] = invoice2.GetHjJeNotHs();
                            byte[] buffer = new byte[0x10];
                            object[] objArray = new object[] { buffer, strArray };
                            invoice2.SetData(objArray);
                            if (ExInfoSplit)
                            {
                                if (invoice2.get_Qdbz() && (list.Count > 200))
                                {
                                    double num34 = 0.0;
                                    double num35 = 0.0;
                                    double num36 = 0.0;
                                    if (flag12)
                                    {
                                        num34 = GetRound((double) (num11 + (goods.JE + goods.SE)), 2);
                                    }
                                    else
                                    {
                                        num34 = GetRound((double) (num11 + goods.JE), 2);
                                    }
                                    num35 = GetRound((double) (num12 + goods.SE), 2);
                                    if (flag12)
                                    {
                                        num36 = GetRound((double) (num34 * 0.05), 2) - num35;
                                    }
                                    else if (flag9)
                                    {
                                        num36 = GetRound((double) ((num34 / 1.035) * 0.015), 2) - num35;
                                    }
                                    else
                                    {
                                        num36 = GetRound((double) (num34 * goods.SLV), 2) - num35;
                                    }
                                    num36 = GetRound(num36, 2);
                                    if ((num34 > invLimit) || (Math.Abs(GetRound(num36, 2)) > 1.27))
                                    {
                                        if (flag2)
                                        {
                                            flag21 = invoice2.CheckFpData();
                                        }
                                        else
                                        {
                                            flag21 = invoice2.CheckFpData_WBJK_ChaiFen();
                                        }
                                    }
                                    else
                                    {
                                        flag21 = true;
                                    }
                                }
                                else if (flag2)
                                {
                                    flag21 = invoice2.CheckFpData();
                                }
                                else
                                {
                                    flag21 = invoice2.CheckFpData_WBJK_ChaiFen();
                                }
                            }
                        }
                        if (!flag21)
                        {
                            if (!flag2 && flag21)
                            {
                                invoice2.CheckFpData();
                            }
                            string str12 = invoice2.GetCode();
                            if ((str12 != "0000") || (!ExInfoSplit && flag15))
                            {
                                if ((str12 == "A059") || (str12 == "A058"))
                                {
                                    return "-2";
                                }
                                if ((str12 == "A015") && (bill.JEHJ < 0.0))
                                {
                                    return "-5";
                                }
                                if ((str12 == "A028") && (bill.JEHJ < 0.0))
                                {
                                    return "-4";
                                }
                                if (((str12 != "A028") || (goods.DJHXZ != 3)) && ((str12 != "A016") || (goods.DJHXZ != 4)))
                                {
                                    switch (str12)
                                    {
                                        case "A073":
                                            return "-10";

                                        case "A105":
                                            return "-11";

                                        case "A085":
                                            return "-3";

                                        case "A108":
                                            if (list2.Count <= 0)
                                            {
                                                return "-7";
                                            }
                                            if (!list2.Contains(num5) || !(goods.SE == 0.0))
                                            {
                                                return "-7";
                                            }
                                            if (!flag5)
                                            {
                                                if ((goods.DJHXZ != 0) || hYSY)
                                                {
                                                    return "-3";
                                                }
                                                if (num33 <= invLimit)
                                                {
                                                    if (MessageManager.ShowMsgBox("INP-272212") != DialogResult.OK)
                                                    {
                                                        return "-999";
                                                    }
                                                    flag5 = true;
                                                }
                                                else
                                                {
                                                    flag14 = true;
                                                }
                                            }
                                            else
                                            {
                                                if ((goods.DJHXZ != 0) || hYSY)
                                                {
                                                    return "-3";
                                                }
                                                if (num33 > invLimit)
                                                {
                                                    flag14 = true;
                                                }
                                            }
                                            goto Label_22AC;
                                    }
                                    flag14 = true;
                                    if ((goods.DJHXZ == 3) || (goods.DJHXZ == 4))
                                    {
                                        if (num23 == num25)
                                        {
                                            return "-1";
                                        }
                                        if (num23 < num25)
                                        {
                                            return "-1";
                                        }
                                        listMXtmp.Clear();
                                        for (int j = num25; j < num23; j++)
                                        {
                                            Goods goods3 = list[j];
                                            str13 = string.Format("{0}_{1}", bill.BH, num15);
                                            if (ToolUtil.GetByteCount(str13) > 50)
                                            {
                                                return "-9";
                                            }
                                            goods3.XSDJBH = str13;
                                            listMXtmp.Add(goods3);
                                        }
                                        num5 = num23;
                                    }
                                }
                            }
                        }
                        else if (((list2.Count > 0) && (num33 > invLimit)) && flag5)
                        {
                            if (!((goods.DJHXZ != 0) || hYSY))
                            {
                                flag14 = true;
                            }
                            else if ((goods.DJHXZ != 3) || hYSY)
                            {
                                return "-3";
                            }
                        }
                    }
                Label_22AC:
                    flag22 = false;
                    if (flag17)
                    {
                        if (!ExInfoSplit)
                        {
                            flag22 = !flag14;
                        }
                        else
                        {
                            flag22 = !flag14;
                        }
                    }
                    else if (!ExInfoSplit)
                    {
                        flag22 = (!flag14 && !flag15) && !flag16;
                    }
                    else
                    {
                        flag22 = !flag14 && !flag16;
                    }
                    if (!flag22)
                    {
                    }
                    if (flag22)
                    {
                        if (flag12)
                        {
                            num11 += GetRound((double) (goods.JE + goods.SE), 2);
                        }
                        else
                        {
                            num11 += goods.JE;
                        }
                        num12 += goods.SE;
                        str13 = string.Format("{0}_{1}", bill.BH, num15);
                        if (ToolUtil.GetByteCount(str13) > 50)
                        {
                            return "-9";
                        }
                        goods.XSDJBH = str13;
                        num16++;
                        listMXtmp.Add(goods);
                        num24++;
                    }
                    else
                    {
                        if (listMXtmp.Count == 0)
                        {
                            string str14 = invoice2.GetCode();
                            if (((((str14.Equals("A612") || str14.Equals("A613")) || (str14.Equals("A017") || str14.Equals("A018"))) || ((str14.Equals("A128") || str14.Equals("A052")) || (str14.Equals("A024") || str14.Equals("A631")))) || (((str14.Equals("A632") || str14.Equals("A633")) || (str14.Equals("A634") || str14.Equals("A635"))) || str14.Equals("A636"))) || str14.Equals("A637"))
                            {
                                return ("[-1]" + str14);
                            }
                            if (!(str14 == "A105") || !flag3)
                            {
                                return "-3";
                            }
                            invoice2.set_IsRed(true);
                            flag4 = true;
                            num11 = 0.0;
                            num16 = 1;
                            sLV = goods.SLV;
                            num14 = 0.0;
                            num19 = 0.0;
                            num20 = 0.0;
                            invoice2.DelSpxxAll();
                            num25 = -1;
                            num24 = 0;
                            num23 = -1;
                            num22 = 0;
                            flag11 = false;
                        }
                        else
                        {
                            string str15 = invoice2.GetCode();
                            string[] @params = invoice2.Params;
                            if (!string.IsNullOrEmpty(str15))
                            {
                                separateReason = MessageManager.GetMessageInfo(str15, @params);
                            }
                            if ((str15 == "A105") && flag3)
                            {
                                invoice2.set_IsRed(true);
                                flag4 = true;
                                if ((listMXtmp[0].JE < 0.0) && (goods.JE < 0.0))
                                {
                                    goto Label_139C;
                                }
                            }
                            this.SetNewBill(separateReason, bill, ref num15, listMXtmp, listBill, ref num5, false);
                            num11 = 0.0;
                            num12 = 0.0;
                            num16 = 1;
                            sLV = goods.SLV;
                            num14 = 0.0;
                            num19 = 0.0;
                            num20 = 0.0;
                            invoice2.DelSpxxAll();
                            num25 = -1;
                            num24 = 0;
                            num23 = -1;
                            num22 = 0;
                            flag11 = false;
                        }
                        goto Label_139C;
                    }
                }
                int i = 0;
                this.SetNewBill(separateReason, bill, ref num15, listMXtmp, listBill, ref i, false);
                if (listBill.Count <= 1)
                {
                    return "-6";
                }
                str16 = "0";
            }
            catch (Exception exception)
            {
                HandleException.Log.Error(exception.ToString());
                throw;
            }
            finally
            {
                Thread.Sleep(100);
                MessageHelper.MsgWait();
            }
            return str16;
        }

        public double CalZkhJE(SaleBill bill, int id, ref int rows)
        {
            int num2;
            if (bill.ListGoods[id].DJHXZ != 3)
            {
                return bill.ListGoods[id].JE;
            }
            double num = 0.0;
            for (num2 = id; num2 < bill.ListGoods.Count; num2++)
            {
                if (bill.ListGoods[num2].DJHXZ != 3)
                {
                    break;
                }
                num += bill.ListGoods[num2].JE;
                rows++;
            }
            for (num2 = id - 1; num2 >= 0; num2--)
            {
                if (bill.ListGoods[num2].DJHXZ != 3)
                {
                    return num;
                }
                num += bill.ListGoods[num2].JE;
                rows++;
            }
            return num;
        }

        public int CanEditBill(SaleBill bill)
        {
            if ((bill.DJZT == "W") || (bill.KPZT == "A"))
            {
                return 1;
            }
            if (bill.KPZT == "P")
            {
                return 2;
            }
            return 0;
        }

        public string chdbdecimal(double dvalue)
        {
            if ((dvalue > 1E+15) || ((dvalue > 0.0) && (dvalue < 1E-15)))
            {
                return "e1";
            }
            return "0";
        }

        public string Check(SaleBill bill)
        {
            Goods current;
            string str = "0";
            if ((bill.DJZL == "c") || (bill.DJZL == "s"))
            {
                if (bill.ListGoods.Count == 0)
                {
                    str = "商品明细不能为空";
                }
                else if (bill.BH.Trim().Length == 0)
                {
                    str = "请设置销售单据编号";
                }
                else if (bill.GFMC.Trim().Length == 0)
                {
                }
                using (List<Goods>.Enumerator enumerator = bill.ListGoods.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        if (current.SPMC.Trim().Length == 0)
                        {
                            return string.Format("第{0}行 商品名称不能为空", current.XH);
                        }
                    }
                }
                return str;
            }
            if (bill.DJZL == "f")
            {
                if (bill.BH.Length == 0)
                {
                    str = "请设置销售单据编号";
                }
                else if (bill.GFMC.Length == 0)
                {
                    str = "实际受票方名称不能为空";
                }
                else if (bill.GFSH.Length == 0)
                {
                    str = "实际受票方税号不能为空";
                }
                else if (bill.GFDZDH.Length == 0)
                {
                    str = "收货人名称不能为空";
                }
                else if (bill.CM.Length == 0)
                {
                    str = "收货人税号不能为空";
                }
                else if (bill.XFDZDH.Length == 0)
                {
                    str = "发货人名称不能为空";
                }
                else if (bill.TYDH.Length == 0)
                {
                    str = "发货人税号不能为空";
                }
                else if (bill.JEHJ == 0.0)
                {
                    str = "金额合计不能为0或负";
                }
                else if ((bill.SLV < 0.0) || (bill.SLV > 1.0))
                {
                    str = "税率非法";
                }
                int count = bill.ListGoods.Count;
                for (int i = 0; i < count; i++)
                {
                    current = bill.ListGoods[i];
                    if (current.SPMC.Length <= 0)
                    {
                        return "商品行名称不能为空";
                    }
                    if (current.JE == 0.0)
                    {
                        return "商品行金额不能为0";
                    }
                }
            }
            return str;
        }

        public string CheckBeforeCF(SaleBill bill)
        {
            double round = 0.0;
            for (int i = 0; i < bill.ListGoods.Count; i++)
            {
                int num3;
                Goods goods = bill.ListGoods[i];
                if (goods.HSJBZ)
                {
                    if (bill.HYSY && (goods.SLV == 0.05))
                    {
                        round = GetRound((double) (goods.JE + goods.SE), 2);
                    }
                    else if (bill.JZ_50_15 && (goods.SLV == 0.015))
                    {
                        round = GetRound((double) (goods.JE + goods.SE), 2);
                    }
                    else
                    {
                        round = GetRound((double) (goods.JE + goods.SE), 2);
                    }
                }
                else
                {
                    round = goods.JE;
                }
                if (((goods.DJ != 0.0) && (goods.SL != 0.0)) && (Math.Abs(GetRound((double) ((goods.DJ * goods.SL) - round), 2)) > 0.01))
                {
                    if (goods.HSJBZ)
                    {
                        num3 = i + 1;
                        return ("[error_hs]" + num3.ToString());
                    }
                    num3 = i + 1;
                    return ("[error_bhs]" + num3.ToString());
                }
                if ((((goods.DJ == 0.0) && (goods.SL != 0.0)) && (goods.JE != 0.0)) || (((goods.DJ != 0.0) && (goods.SL == 0.0)) && (goods.JE != 0.0)))
                {
                    if (goods.HSJBZ)
                    {
                        num3 = i + 1;
                        return ("[error_hs]" + num3.ToString());
                    }
                    num3 = i + 1;
                    return ("[error_bhs]" + num3.ToString());
                }
                if ((goods.DJ < 0.0) && (goods.SL < 0.0))
                {
                    num3 = i + 1;
                    return ("[error_1]" + num3.ToString());
                }
            }
            return "0";
        }

        public string CheckBill(SaleBill bill)
        {
            SaleBillCheck instance = SaleBillCheck.Instance;
            CheckResult result = instance.CheckSaleBillBase(bill);
            instance.SaveCheckResult(result, bill);
            return instance.ConvertToReportMsg(result);
        }

        public void CheckBillMonth(string DJmonth, string DJtype, bool ShowType)
        {
            DataTable allDJforCheck = this.dbBill.GetAllDJforCheck(DJmonth, DJtype);
            int count = allDJforCheck.Rows.Count;
            List<ImportErrorDetail> listError = new List<ImportErrorDetail>();
            try
            {
                MessageHelper.MsgWait("正在检验单据，请稍候...");
                for (int i = 0; i < count; i++)
                {
                    string str = allDJforCheck.Rows[i]["BH"].ToString();
                    string str2 = string.Format("销售单据编号: {0} ", str);
                    SaleBill bill = this.Find(str);
                    string errowInfo = this.CheckBill(bill);
                    if (errowInfo.Contains("有错"))
                    {
                        listError.Add(new ImportErrorDetail(errowInfo, str, i, true));
                    }
                }
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
            }
            finally
            {
                Thread.Sleep(100);
                MessageHelper.MsgWait();
            }
            if (ShowType && (listError.Count > 0))
            {
                new DJCWXXXX(listError, count).ShowDialog();
            }
        }

        public string CheckBillRecordCF(SaleBill bill, string SplitType, bool ExEWMInfoSplit, ref string reason)
        {
            int num2;
            double invLimit = TaxCardValue.GetInvLimit(bill.DJZL);
            bool flag = false;
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if ((card.get_ECardType() == 3) && (card.get_StateInfo().TBType == 0))
            {
                flag = true;
            }
            if (bill.BH.Length > 0x30)
            {
                reason = "单据编号长度超过48位的单据无法拆分";
                return "CanNot";
            }
            if (bill.JEHJ < 0.0)
            {
                reason = "红字发票不能拆分";
                return "CanNot";
            }
            if (this.IsCES(bill))
            {
                reason = "差额税单据不允许拆分";
                return "CanNot";
            }
            if (bill.HYSY)
            {
                bool flag2 = false;
                bool flag3 = false;
                for (num2 = 0; num2 < bill.ListGoods.Count; num2++)
                {
                    if (bill.ListGoods[num2].SLV == 0.05)
                    {
                        flag2 = true;
                    }
                    else
                    {
                        flag3 = true;
                    }
                }
                if (flag2 && flag3)
                {
                    reason = "该单据包含中外合作油气田和其他税率，无法拆分";
                    return "CanNot";
                }
            }
            bool flag4 = this.IsSWDK();
            if (bill.JZ_50_15)
            {
                bool flag5 = false;
                bool flag6 = false;
                for (num2 = 0; num2 < bill.ListGoods.Count; num2++)
                {
                    if (bill.ListGoods[num2].SLV == 0.015)
                    {
                        flag5 = true;
                    }
                    else
                    {
                        flag6 = true;
                    }
                }
                if (flag5 && flag6)
                {
                    reason = "该单据包含1.5%税率和其他税率，无法拆分";
                    return "CanNot";
                }
            }
            string str = this.dbBill.BeSpliOrColl(bill.BH);
            if (str != "0")
            {
                reason = "编号为" + bill.BH + " 的单据" + str + "，\n无法再次进行拆分";
                return "CanNot";
            }
            string str2 = this.CheckZK(bill);
            if (!str2.Equals("0"))
            {
                reason = str2;
                return "CanNot";
            }
            if (SplitType == "MT")
            {
                return "Need";
            }
            SaleBillCheck instance = SaleBillCheck.Instance;
            CheckResult checkResult = instance.CheckSaleBillBase(bill);
            string str3 = instance.ConvertToReportMsgForSplitBill(checkResult);
            if (checkResult.HasWrong)
            {
                if (str3.Contains("行，开票金额超出金税设备允许范围"))
                {
                    checkResult.NeedCF = true;
                }
                if (str3.Contains("组折扣商品") || str3.Contains("金额减税额除以税率的绝对值大于"))
                {
                    checkResult.NeedCF = false;
                }
            }
            if (checkResult.NeedCF)
            {
                if (!flag)
                {
                    return "Need";
                }
                int count = checkResult.listErrorDJ.Count;
                for (num2 = 0; num2 < count; num2++)
                {
                    string str4 = checkResult.listErrorDJ[num2];
                    if (str4.CompareTo("CF:单据含有多个税率") != 0)
                    {
                        return "Need";
                    }
                }
                if (checkResult.listErrorMX.Count > 0)
                {
                    return "Need";
                }
            }
            if (bill.JEHJ > invLimit)
            {
                reason = "单据超过最大金额限制!";
                return "Need";
            }
            if (!flag && instance.BillHasMultiSlv(bill))
            {
                reason = "含有多种税率";
                return "Need";
            }
            if (ExEWMInfoSplit && instance.IsOverEWM(bill))
            {
                reason = "超过汉字防伪限制";
                return "Need";
            }
            return "Needless";
        }

        public string CheckBillRecordCF(SaleBill bill, string SplitType, bool ExEWMInfoSplit, ref string reason, int[] slvjeseIndex, bool hzfw, bool value = true)
        {
            int num2;
            double invLimit = TaxCardValue.GetInvLimit(bill.DJZL);
            bool flag = false;
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if ((card.get_ECardType() == 3) && (card.get_StateInfo().TBType == 0))
            {
                flag = true;
            }
            if (!value && (bill.BH.Length > 0x30))
            {
                reason = "单据编号长度超过48位的单据无法拆分";
                return "CanNot";
            }
            if (bill.JEHJ < 0.0)
            {
                reason = "红字发票不能拆分";
                return "CanNot";
            }
            if (this.IsCES(bill))
            {
                reason = "差额税单据不允许拆分";
                return "Needless";
            }
            if (bill.HYSY)
            {
                bool flag2 = false;
                bool flag3 = false;
                for (num2 = 0; num2 < bill.ListGoods.Count; num2++)
                {
                    if (bill.ListGoods[num2].SLV == 0.05)
                    {
                        flag2 = true;
                    }
                    else
                    {
                        flag3 = true;
                    }
                }
                if (flag2 && flag3)
                {
                    reason = "该单据包含中外合作油气田和其他税率，无法拆分";
                    return "CanNot";
                }
            }
            bool flag4 = this.IsSWDK();
            if (bill.JZ_50_15)
            {
                bool flag5 = false;
                bool flag6 = false;
                for (num2 = 0; num2 < bill.ListGoods.Count; num2++)
                {
                    if (bill.ListGoods[num2].SLV == 0.015)
                    {
                        flag5 = true;
                    }
                    else
                    {
                        flag6 = true;
                    }
                }
                if (flag5 && flag6)
                {
                    reason = "该单据包含1.5%税率和其他税率，无法拆分";
                    return "CanNot";
                }
            }
            string str = this.dbBill.BeSpliOrColl(bill.BH);
            if (str != "0")
            {
                reason = "编号为" + bill.BH + " 的单据" + str + "，\n无法再次进行拆分";
                return "CanNot";
            }
            string str2 = this.CheckZK(bill);
            if (!str2.Equals("0"))
            {
                reason = str2;
                return "CanNot";
            }
            if (SplitType == "MT")
            {
                return "Need";
            }
            SaleBillCheck instance = SaleBillCheck.Instance;
            FPGenerateResult result = new FPGenerateResult(bill);
            string[] strArray = new string[0x10];
            Invoice invoice2 = GenerateInvoice.Instance.CreateInvoiceHead(result, slvjeseIndex, ref strArray, bill);
            if (hzfw)
            {
                if (ExEWMInfoSplit)
                {
                    invoice2.SetQdbz(false);
                }
                else
                {
                    invoice2.SetQdbz(true);
                }
            }
            else
            {
                invoice2.SetQdbz(true);
            }
            string maxBMBBBH = new SPFLService().GetMaxBMBBBH();
            invoice2.set_Bmbbbh(maxBMBBBH);
            int count = bill.ListGoods.Count;
            for (num2 = 0; num2 < count; num2++)
            {
                string str8;
                Goods goods = bill.ListGoods[num2];
                bool flag7 = false;
                if (bill.HYSY && (goods.SLV == 0.05))
                {
                    flag7 = true;
                }
                if (flag7)
                {
                    invoice2.SetZyfpLx(1);
                }
                bool flag8 = false;
                if (bill.JZ_50_15 && (goods.SLV == 0.015))
                {
                    flag8 = true;
                }
                if (flag8)
                {
                    invoice2.SetZyfpLx(10);
                }
                bool flag9 = true;
                if (goods.DJHXZ == 4)
                {
                    string str4 = Convert.ToString(goods.JE);
                    string str5 = Convert.ToString(goods.SE);
                    int zKH = 0;
                    double disCount = 0.0;
                    if (CommonTool.isSPBMVersion())
                    {
                        double jE = bill.ListGoods[num2 - 1].JE;
                        disCount = CommonTool.GetDisCount(Math.Abs(goods.JE), jE, ref zKH);
                    }
                    else
                    {
                        disCount = CommonTool.GetDisCount(goods.SPMC, ref zKH);
                    }
                    string str7 = ((decimal) disCount).ToString();
                    int num9 = invoice2.GetSpxxs().Count;
                    if (invoice2.AddZkxx(num9 - 1, zKH, str7, str4, str5) < 0)
                    {
                        flag9 = false;
                        str8 = invoice2.GetCode();
                        return "Need";
                    }
                }
                else
                {
                    ZYFP_LX zyfp_lx;
                    string sPMC = goods.SPMC;
                    string sPSM = goods.SPSM;
                    string str11 = Convert.ToString(goods.SLV);
                    string str12 = Convert.ToString((decimal) goods.DJ);
                    if (flag7)
                    {
                        zyfp_lx = 1;
                    }
                    else if (flag8)
                    {
                        zyfp_lx = 10;
                    }
                    else
                    {
                        zyfp_lx = 0;
                    }
                    if ((bill.DJZL == "s") && (zyfp_lx == 0))
                    {
                        switch (this.xtCheck(sPMC))
                        {
                            case "1":
                                zyfp_lx = 6;
                                invoice2.SetZyfpLx(zyfp_lx);
                                break;

                            case "2":
                                zyfp_lx = 7;
                                invoice2.SetZyfpLx(zyfp_lx);
                                break;
                        }
                    }
                    Spxx spxx = new Spxx(sPMC, sPSM, str11, goods.GGXH, goods.JLDW, str12, goods.HSJBZ, zyfp_lx);
                    spxx.set_Je(Convert.ToString(goods.JE));
                    spxx.set_Se(Convert.ToString(goods.SE));
                    spxx.set_SL(Convert.ToString((decimal) goods.SL));
                    if (invoice2.AddSpxx(spxx) < 0)
                    {
                        flag9 = false;
                        str8 = invoice2.GetCode();
                        return "Need";
                    }
                }
            }
            strArray[slvjeseIndex[10]] = invoice2.GetHjSe();
            strArray[slvjeseIndex[14]] = invoice2.get_SLv();
            strArray[slvjeseIndex[15]] = invoice2.GetHjJeNotHs();
            byte[] buffer = new byte[0x10];
            object[] objArray = new object[] { buffer, strArray };
            invoice2.SetData(objArray);
            invoice2.CheckFpData();
            string code = invoice2.GetCode();
            if ((code != "0000") && (((((code != "A079") && (code != "A080")) && ((code != "A081") && (code != "A082"))) && (code != "A083")) && !(code == "A084")))
            {
                return "Need";
            }
            if (!flag && instance.BillHasMultiSlv(bill))
            {
                reason = "含有多种税率";
                return "Need";
            }
            if (((!ExEWMInfoSplit && (bill.QDHSPMC.Length == 0)) && (bill.ListGoods.Count < 8)) && instance.IsOverEWM(bill))
            {
            }
            return "Needless";
        }

        public string CheckDisCount(SaleBill bill, int selectIndex, int zkhs, double zkl, Goods discountRow, double zkje = 0.0)
        {
            if ((selectIndex < 0) || (zkhs < 1))
            {
                return "e1";
            }
            zkl = GetRound(zkl, 5);
            zkje = GetRound(zkje, 2);
            Goods goods = bill.ListGoods[selectIndex];
            int num = (selectIndex - zkhs) + 1;
            double num2 = 0.0;
            double num3 = 0.0;
            if (num < 0)
            {
                num = 0;
            }
            for (int i = num; i < (num + zkhs); i++)
            {
                num2 += bill.ListGoods[i].SE;
                num3 += bill.ListGoods[i].JE;
            }
            if (!(zkje == 0.0))
            {
                discountRow.JE = -1.0 * zkje;
            }
            else
            {
                double num5 = Finacial.Mul(num3, zkl, 7);
                discountRow.JE = -1.0 * Finacial.GetRound(num5, 2);
                double num6 = -1.0 * GetRound((double) (num3 * zkl), 3);
            }
            discountRow.DJHXZ = 4;
            discountRow.SLV = bill.ListGoods[num].SLV;
            if (bill.HYSY && (Math.Abs((double) (discountRow.SLV - 0.05)) < 1E-05))
            {
                discountRow.SE = -1.0 * Finacial.GetRound((double) (num2 * zkl), 2);
            }
            else
            {
                double round;
                if (bill.JZ_50_15 && (Math.Abs((double) (discountRow.SLV - 0.015)) < 1E-05))
                {
                    round = Finacial.GetRound((double) ((discountRow.JE / 1.035) * 0.015), 2);
                    if ((zkl == 1.0) && ((Math.Abs(round) > num2) || (num3 == zkje)))
                    {
                        discountRow.SE = -1.0 * Finacial.GetRound((double) (num2 * zkl), 7);
                    }
                    else
                    {
                        discountRow.SE = round;
                    }
                }
                else
                {
                    round = Finacial.GetRound((double) (discountRow.JE * discountRow.SLV), 2);
                    if ((zkl == 1.0) && ((Math.Abs(round) > num2) || (num3 == zkje)))
                    {
                        discountRow.SE = -1.0 * Finacial.GetRound((double) (num2 * zkl), 7);
                    }
                    else
                    {
                        discountRow.SE = round;
                    }
                }
            }
            discountRow.SE = Finacial.GetRound(discountRow.SE, 2);
            return "0";
        }

        public string CheckEditGoods(SaleBill bill)
        {
            for (int i = 0; i < bill.ListGoods.Count; i++)
            {
                Goods goods = bill.ListGoods[i];
                if (goods.DJHXZ == 0)
                {
                    if (goods.JE == 0.0)
                    {
                        return ("第" + (i + 1) + "行，商品金额不能为0");
                    }
                    if (goods.SPMC.Trim().Length == 0)
                    {
                        return ("第" + (i + 1) + "行，商品名称不能空白");
                    }
                }
            }
            return "0";
        }

        public string CheckSetOneGroup(SaleBill newbill, int[] slvjeseIndex)
        {
            string str4;
            try
            {
                double round;
                double num3;
                int num4;
                int num16;
                SaleBillCheck instance = SaleBillCheck.Instance;
                double invLimit = TaxCardValue.GetInvLimit(newbill.DJZL);
                if (newbill.HYSY)
                {
                    round = 0.0;
                    num3 = 0.0;
                    for (num4 = 0; num4 < newbill.ListGoods.Count; num4++)
                    {
                        if (newbill.HYSY && (newbill.ListGoods[num4].SLV == 0.05))
                        {
                            round += newbill.ListGoods[num4].JE + newbill.ListGoods[num4].SE;
                            if (!newbill.ListGoods[num4].HSJBZ)
                            {
                                num16 = num4 + 1;
                                return ("拆分后的第" + num16.ToString() + "行，单价乘以数量不等于含税金额");
                            }
                        }
                        else
                        {
                            num3 += newbill.ListGoods[num4].JE;
                        }
                    }
                    round = GetRound(round, 2);
                    if (GetRound(num3, 2) > 0.0)
                    {
                    }
                    if (round > invLimit)
                    {
                        return "该组单据含税金额超过开票限额，不能进行拆分!";
                    }
                }
                else if (newbill.JZ_50_15)
                {
                    round = 0.0;
                    num3 = 0.0;
                    for (num4 = 0; num4 < newbill.ListGoods.Count; num4++)
                    {
                        if (newbill.JZ_50_15 && (newbill.ListGoods[num4].SLV == 0.015))
                        {
                            round += newbill.ListGoods[num4].JE;
                        }
                        else
                        {
                            num3 += newbill.ListGoods[num4].JE;
                        }
                    }
                    round = GetRound(round, 2);
                    if (GetRound(num3, 2) > 0.0)
                    {
                    }
                    if (round > invLimit)
                    {
                        return "该组单据超过开票限额，不能进行拆分!";
                    }
                }
                else if (newbill.JEHJ > invLimit)
                {
                    return "该组单据超过开票限额，不能进行拆分!";
                }
                bool flag = false;
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if ((card.get_ECardType() == 3) && (card.get_StateInfo().TBType == 0))
                {
                    flag = true;
                }
                if (!flag && instance.BillHasMultiSlv(newbill))
                {
                    return "该组中含有多种税率，不能设为一组，请重新设置分组";
                }
                double num5 = 0.0;
                for (num4 = 0; num4 < newbill.ListGoods.Count; num4++)
                {
                    double jE = 0.0;
                    bool flag2 = false;
                    bool flag3 = false;
                    if (newbill.HYSY && (newbill.ListGoods[num4].SLV == 0.05))
                    {
                        jE = newbill.ListGoods[num4].JE + newbill.ListGoods[num4].SE;
                        flag2 = true;
                    }
                    else if (newbill.JZ_50_15 && (newbill.ListGoods[num4].SLV == 0.015))
                    {
                        jE = newbill.ListGoods[num4].JE;
                        flag3 = true;
                    }
                    else
                    {
                        jE = newbill.ListGoods[num4].JE;
                    }
                    double sE = newbill.ListGoods[num4].SE;
                    double sLV = newbill.ListGoods[num4].SLV;
                    double num9 = 0.0;
                    if (flag3)
                    {
                        num9 = ((jE / 1.035) * 0.015) - sE;
                    }
                    else
                    {
                        num9 = (jE * sLV) - sE;
                    }
                    num9 = GetRound(num9, 2);
                    if (Math.Abs(num9) > 0.06)
                    {
                        if (flag2)
                        {
                            num16 = num4 + 1;
                            return ("该组的第" + num16.ToString() + "行，含税金额乘以税率减税额的绝对值大于0.06!");
                        }
                        if (flag3)
                        {
                            num16 = num4 + 1;
                            return ("该组的第" + num16.ToString() + "行，税额误差大于0.06!");
                        }
                        num16 = num4 + 1;
                        return ("该组的第" + num16.ToString() + "行，金额乘以税率减税额的绝对值大于0.06!");
                    }
                    num5 += num9;
                    if (Math.Abs(GetRound(num5, 2)) > 1.27)
                    {
                        return "该组税额总误差超过1.27，请重新拆分";
                    }
                }
                int num10 = 0;
                int zKH = 0;
                double num12 = 0.0;
                for (num4 = 0; num4 < newbill.ListGoods.Count; num4++)
                {
                    Goods goods = newbill.ListGoods[num4];
                    int dJHXZ = goods.DJHXZ;
                    switch (dJHXZ)
                    {
                        case 0:
                            break;

                        case 3:
                            num10++;
                            break;

                        default:
                            if (dJHXZ == 4)
                            {
                                if (CommonTool.isSPBMVersion())
                                {
                                    zKH = 1;
                                }
                                else
                                {
                                    CommonTool.GetDisCount(goods.SPMC, ref zKH);
                                }
                                bool flag5 = false;
                                if ((num4 - zKH) >= 0)
                                {
                                    int xH = newbill.ListGoods[num4 - zKH].XH;
                                    flag5 = (newbill.ListGoods[num4].XH - xH) == zKH;
                                }
                                if ((num10 == zKH) && flag5)
                                {
                                    num10 = 0;
                                    zKH = 0;
                                }
                            }
                            break;
                    }
                    num12 += newbill.ListGoods[num4].JE;
                }
                if (num12 == 0.0)
                {
                    return "该组单据金额不能为0!";
                }
                if ((num10 > 0) || (zKH > 0))
                {
                    return "同一折扣必须设为一组";
                }
                string str2 = "";
                if (str2 == "Need")
                {
                    return "该组单据不能设为一组\n";
                }
                str4 = "0";
            }
            catch (Exception)
            {
                throw;
            }
            return str4;
        }

        public bool CheckSP_Legal(SaleBill bill)
        {
            bool flag = false;
            int count = bill.ListGoods.Count;
            for (int i = 0; i < count; i++)
            {
                Goods goods = bill.ListGoods[i];
                if (goods.DJHXZ != 4)
                {
                    if (i == 0)
                    {
                        if (goods.KCE == 0.0)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        if (flag && (goods.KCE == 0.0))
                        {
                            return false;
                        }
                        if (!(flag || (goods.KCE == 0.0)))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public string CheckZK(SaleBill bill)
        {
            int num;
            string sPMC;
            string str3;
            string str = "0";
            if (CommonTool.isSPBMVersion())
            {
                for (num = 0; num < bill.ListGoods.Count; num++)
                {
                    if (bill.ListGoods[num].DJHXZ == 4)
                    {
                        if (num < 1)
                        {
                            return "该单据折扣错误";
                        }
                        sPMC = bill.ListGoods[num].SPMC;
                        str3 = bill.ListGoods[num - 1].SPMC;
                        if (!sPMC.Equals(str3))
                        {
                            return "商品编码版本存在错误的折扣名称";
                        }
                    }
                }
                return str;
            }
            for (num = 0; num < bill.ListGoods.Count; num++)
            {
                if (bill.ListGoods[num].DJHXZ == 4)
                {
                    if (num < 1)
                    {
                        return "该单据折扣错误";
                    }
                    sPMC = bill.ListGoods[num].SPMC;
                    str3 = bill.ListGoods[num - 1].SPMC;
                    if (sPMC.Equals(str3))
                    {
                        return "非商品编码版本存在错误的折扣名称";
                    }
                }
            }
            return str;
        }

        public void CleanBill(SaleBill bill)
        {
            string str;
            SaleBillDAL ldal = new SaleBillDAL();
            bill.TotalAmount = 0.0;
            bill.TotalTax = 0.0;
            bill.TotalAmountTax = 0.0;
            double num = 0.0;
            bool flag = false;
            bill.JZ_50_15 = false;
            for (int i = 0; i < bill.ListGoods.Count; i++)
            {
                Goods goods = bill.ListGoods[i];
                if (((goods.SLV == 0.05) && (bill.DJZL == "s")) && bill.HYSY)
                {
                    if (!(goods.KCE == 0.0))
                    {
                        flag = true;
                        goods.KCE = 0.0;
                        bill.setSlv(i, goods.SLV.ToString());
                    }
                    goods.HSJBZ = true;
                    bill.TotalAmount += goods.JE;
                }
                else
                {
                    if (Math.Abs((double) (goods.SLV - 0.015)) < 1E-05)
                    {
                        if (!(goods.KCE == 0.0))
                        {
                            goods.KCE = 0.0;
                            bill.setSlv(i, goods.SLV.ToString());
                        }
                        bill.JZ_50_15 = true;
                    }
                    if (bill.ContainTax)
                    {
                        bill.TotalAmount += Finacial.Add(goods.JE, goods.SE, 15);
                    }
                    else
                    {
                        bill.TotalAmount += goods.JE;
                    }
                }
                if (bill.HYSY && flag)
                {
                    MessageBoxHelper.Show("中外合作油气田 取消 差额征税");
                }
                bill.TotalTax += goods.SE;
                num += goods.JE;
                goods.XSDJBH = bill.BH;
                goods.XH = i + 1;
                goods.FPZL = bill.DJZL;
                if (CommonTool.isSPBMVersion())
                {
                    if ((goods.FLBM.Trim() == "") || (goods.SPMC.Trim() == ""))
                    {
                        DataTable table = ldal.GET_SPXX_BY_NAME(goods.SPMC, bill.DJZL, "");
                        if (table.Rows.Count > 0)
                        {
                            if (goods.FLBM.Trim() == "")
                            {
                                goods.FLBM = table.Rows[0]["SPFL"].ToString().Trim();
                                str = table.Rows[0]["YHZC"].ToString().Trim();
                                if ((str != "") && ((((str == "1") || (str == "是")) || (str == "享受")) || (str.ToUpper() == "TRUE")))
                                {
                                    goods.XSYH = true;
                                }
                                SaleBillDAL ldal2 = new SaleBillDAL();
                                if (ldal2.GET_YHZCSLV_BY_YHZCMC(table.Rows[0]["SPFL_ZZSTSGL"].ToString().Trim()).Contains(goods.SLV))
                                {
                                    goods.XSYHSM = table.Rows[0]["SPFL_ZZSTSGL"].ToString().Trim();
                                    goods.XSYH = true;
                                }
                                else
                                {
                                    goods.XSYHSM = "";
                                    goods.XSYH = false;
                                }
                            }
                            if (goods.SPBM.Trim() == "")
                            {
                                goods.SPBM = table.Rows[0]["BM"].ToString().Trim();
                            }
                        }
                    }
                    bool xSYH = goods.XSYH;
                    string str2 = "出口零税";
                    string str3 = "免税";
                    string str4 = "不征税";
                    goods.LSLVBS = "";
                    if (goods.XSYH && (goods.SLV == 0.0))
                    {
                        if (goods.XSYHSM.Contains(str2))
                        {
                            goods.LSLVBS = "0";
                        }
                        else if (goods.XSYHSM.Contains(str3))
                        {
                            goods.LSLVBS = "1";
                        }
                        else if (goods.XSYHSM.Contains(str4))
                        {
                            goods.LSLVBS = "2";
                        }
                        else
                        {
                            goods.LSLVBS = "3";
                        }
                    }
                    else if (goods.SLV == 0.0)
                    {
                        goods.LSLVBS = "3";
                    }
                }
            }
            if (CommonTool.isSPBMVersion() && (bill.DJZL.ToUpper() == "J"))
            {
                string cPXH = bill.JDC_CPXH.Trim();
                string cLLX = bill.JDC_LX.Trim();
                if (bill.JDC_FLBM.Trim() == "")
                {
                    bill.JDC_XSYH = false;
                    if ((cPXH != "") && (cLLX != ""))
                    {
                        DataTable table2 = ldal.GET_CLXX_BY_CPXH(cPXH, cLLX);
                        if ((table2.Rows.Count > 0) && (bill.JDC_FLBM.Trim() == ""))
                        {
                            bill.JDC_FLBM = table2.Rows[0]["SPFL"].ToString().Trim();
                            str = table2.Rows[0]["YHZC"].ToString().Trim();
                            if ((str != "") && ((((str == "1") || (str == "是")) || (str == "享受")) || (str.ToUpper() == "TRUE")))
                            {
                                bill.JDC_XSYH = true;
                            }
                            if (bill.JDC_CLBM.Trim() == "")
                            {
                                bill.JDC_CLBM = table2.Rows[0]["BM"].ToString().Trim();
                            }
                            if (bill.JDC_XSYHSM.Trim() == "")
                            {
                                bill.JDC_XSYHSM = table2.Rows[0]["SPFL_ZZSTSGL"].ToString().Trim();
                            }
                        }
                    }
                }
            }
            bill.TotalAmountTax = bill.ContainTax ? bill.TotalAmount : (bill.TotalAmount + bill.TotalTax);
            bill.JEHJ = Finacial.GetRound(num, 2);
            bill.FHR = GetSafeData.GetSafeString(bill.FHR, 8);
            bill.SKR = GetSafeData.GetSafeString(bill.SKR, 8);
        }

        public void CleanBillWithoutFLBM(SaleBill bill)
        {
            SaleBillDAL ldal = new SaleBillDAL();
            bill.TotalAmount = 0.0;
            bill.TotalTax = 0.0;
            bill.TotalAmountTax = 0.0;
            double num = 0.0;
            bill.JZ_50_15 = false;
            for (int i = 0; i < bill.ListGoods.Count; i++)
            {
                Goods goods = bill.ListGoods[i];
                if (((goods.SLV == 0.05) && (bill.DJZL == "s")) && bill.HYSY)
                {
                    goods.HSJBZ = true;
                    bill.TotalAmount += goods.JE;
                }
                else
                {
                    if (Math.Abs((double) (goods.SLV - 0.015)) < 1E-05)
                    {
                        bill.JZ_50_15 = true;
                    }
                    if (bill.ContainTax)
                    {
                        bill.TotalAmount += Finacial.Add(goods.JE, goods.SE, 15);
                    }
                    else
                    {
                        bill.TotalAmount += goods.JE;
                    }
                }
                bill.TotalTax += goods.SE;
                num += goods.JE;
                goods.XSDJBH = bill.BH;
                goods.XH = i + 1;
                goods.FPZL = bill.DJZL;
            }
            if (CommonTool.isSPBMVersion() && (bill.DJZL.ToUpper() == "J"))
            {
                string cPXH = bill.JDC_CPXH.Trim();
                string cLLX = bill.JDC_LX.Trim();
                if (bill.JDC_FLBM.Trim() == "")
                {
                    bill.JDC_XSYH = false;
                    if ((cPXH != "") && (cLLX != ""))
                    {
                        DataTable table = ldal.GET_CLXX_BY_CPXH(cPXH, cLLX);
                        if ((table.Rows.Count > 0) && (bill.JDC_FLBM.Trim() == ""))
                        {
                            bill.JDC_FLBM = table.Rows[0]["SPFL"].ToString().Trim();
                            string str3 = table.Rows[0]["YHZC"].ToString().Trim();
                            if ((str3 != "") && ((((str3 == "1") || (str3 == "是")) || (str3 == "享受")) || (str3.ToUpper() == "TRUE")))
                            {
                                bill.JDC_XSYH = true;
                            }
                            if (bill.JDC_CLBM.Trim() == "")
                            {
                                bill.JDC_CLBM = table.Rows[0]["BM"].ToString().Trim();
                            }
                            if (bill.JDC_XSYHSM.Trim() == "")
                            {
                                bill.JDC_XSYHSM = table.Rows[0]["SPFL_ZZSTSGL"].ToString().Trim();
                            }
                        }
                    }
                }
            }
            bill.TotalAmountTax = bill.ContainTax ? bill.TotalAmount : (bill.TotalAmount + bill.TotalTax);
            bill.JEHJ = Finacial.GetRound(num, 2);
            bill.FHR = GetSafeData.GetSafeString(bill.FHR, 8);
            bill.SKR = GetSafeData.GetSafeString(bill.SKR, 8);
        }

        public string CollectDiscount(SaleBill bill)
        {
            int num = 0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            for (int i = 0; i < bill.ListGoods.Count; i++)
            {
                Goods item = bill.ListGoods[i];
                if ((item.DJHXZ == 4) || (item.JE <= 0.0))
                {
                    num3 += item.JE;
                    num4 += item.SE;
                    bill.ListGoods.Remove(item);
                    i--;
                }
                else
                {
                    num++;
                    num2 += item.JE;
                    item.DJHXZ = 3;
                }
            }
            double zkl = Math.Abs((double) (num3 / num2));
            if (CommonTool.isSPBMVersion())
            {
                this.DisCount2(bill, bill.ListGoods.Count - 1, bill.ListGoods.Count, zkl, Math.Abs(num3));
            }
            else
            {
                this.DisCount(bill, bill.ListGoods.Count - 1, bill.ListGoods.Count, zkl, Math.Abs(num3), Math.Abs(num4));
            }
            this.CleanBill(bill);
            List<SaleBill> billList = new List<SaleBill> {
                bill
            };
            return this.dbBill.SaveToTempTable(billList, 0);
        }

        public string ComplexMerge(List<Goods> listMX)
        {
            int num5;
            Goods goods2;
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            if (listMX.Count == 1)
            {
                return "只有一行商品，无需进行复杂合并";
            }
            int num4 = 0;
            for (num5 = 0; num5 < listMX.Count; num5++)
            {
                Goods goods = listMX[num5];
                if (goods.DJHXZ == 4)
                {
                    num4++;
                }
                else if (goods.DJHXZ == 0)
                {
                    num4++;
                }
            }
            if ((num4 <= 1) && (listMX.Count <= 2))
            {
                return "只有一行商品，无需进行复杂合并";
            }
            int num6 = 0;
            int num7 = 0;
            for (num5 = 0; num5 < listMX.Count; num5++)
            {
                goods2 = listMX[num5];
                if (goods2.DJHXZ == 4)
                {
                    num7++;
                    string sPMC = goods2.SPMC;
                    num += goods2.JE;
                    num2 += goods2.SE;
                    listMX.RemoveAt(num5);
                    num5--;
                }
                else
                {
                    num3 += goods2.JE;
                }
            }
            for (num5 = 0; num5 < listMX.Count; num5++)
            {
                goods2 = listMX[num5];
                for (int i = num5 + 1; i < listMX.Count; i++)
                {
                    Goods goods3 = listMX[i];
                    if ((((((goods3.SPMC == goods2.SPMC) && (goods3.GGXH == goods2.GGXH)) && ((goods3.JLDW == goods2.JLDW) && (goods3.DJ == goods2.DJ))) && (((goods3.SLV == goods2.SLV) && (goods3.SPSM == goods2.SPSM)) && ((goods3.FLBM == goods2.FLBM) && (goods3.XSYH == goods2.XSYH)))) && (((goods3.XSYHSM == goods2.XSYHSM) && (goods3.FLMC == goods2.FLMC)) && (goods3.SPBM == goods2.SPBM))) && (goods3.LSLVBS == goods2.LSLVBS))
                    {
                        num6++;
                        goods2.SL += goods3.SL;
                        goods2.JE += goods3.JE;
                        goods2.SE += goods3.SE;
                        listMX.RemoveAt(i);
                        i--;
                    }
                    else if (num7 > 0)
                    {
                        return "所选单据包含多种商品并且有折扣，不能进行复杂合并";
                    }
                }
            }
            if (num6 == 0)
            {
                return "非同类商品，不能进行复杂合并";
            }
            if (num < 0.0)
            {
                Goods item = new Goods {
                    XSDJBH = listMX[0].XSDJBH
                };
                double zKL = (-1.0 * num) / num3;
                if (CommonTool.isSPBMVersion())
                {
                    item.SPMC = listMX[0].SPMC;
                }
                else
                {
                    item.SPMC = CommonTool.GetDisCountMC(zKL, 1);
                }
                item.SLV = listMX[0].SLV;
                item.JE = GetRound(num, 2);
                item.SE = GetRound(num2, 2);
                item.SPSM = listMX[0].SPSM;
                item.DJHXZ = 4;
                listMX.Add(item);
                listMX[0].DJHXZ = 3;
            }
            return "0";
        }

        public int DeleteSaleBill(List<string> listBH)
        {
            return this.dbBill.DeleteSaleBill(listBH);
        }

        public string DisCount(SaleBill bill, int selectIndex, int zkhs, double zkl, double zkje = 0.0, double crzkse = 0.0)
        {
            int num8;
            string str = "0";
            if ((selectIndex < 0) || (zkhs < 1))
            {
                return "e1";
            }
            zkl = Finacial.GetRound(zkl, 5);
            zkje = Finacial.GetRound(zkje, 2);
            Goods good = bill.GetGood(selectIndex);
            int num = (selectIndex - zkhs) + 1;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = zkje;
            if (bill.ContainTax)
            {
                double sLV = bill.ListGoods[num].SLV;
                zkje = Finacial.Div(num4, 1.0 + sLV, 15);
            }
            int num6 = num;
            double jE = bill.ListGoods[num].JE;
            for (num8 = num; num8 < (num + zkhs); num8++)
            {
                bill.ListGoods[num8].DJHXZ = 3;
                num2 += bill.ListGoods[num8].SE;
                num3 += bill.ListGoods[num8].JE;
                if ((num8 != num) && (bill.ListGoods[num8].JE > jE))
                {
                    jE = bill.ListGoods[num8].JE;
                    num6 = num8;
                }
            }
            if (Finacial.Equal(zkl, 0.0) && Finacial.Equal(zkje, 0.0))
            {
                for (num8 = num; num8 < (num + zkhs); num8++)
                {
                    bill.ListGoods[num8].DJHXZ = 0;
                }
                return "1";
            }
            Goods item = new Goods {
                XSDJBH = bill.BH,
                XH = selectIndex + 2
            };
            int num9 = (num + zkhs) - 1;
            item.SPSM = bill.ListGoods[num9].SPSM;
            item.Reserve = bill.ListGoods[num9].Reserve;
            item.HSJBZ = bill.ListGoods[num9].HSJBZ;
            item.SLV = bill.ListGoods[num9].SLV;
            item.LSLVBS = bill.ListGoods[num9].LSLVBS;
            item.XSYHSM = bill.ListGoods[num9].XSYHSM;
            item.XSYH = bill.ListGoods[num9].XSYH;
            item.FLBM = bill.ListGoods[num9].FLBM;
            item.SPBM = bill.ListGoods[num9].SPBM;
            if (CommonTool.isSPBMVersion())
            {
                item.SPMC = bill.ListGoods[num9].SPMC;
            }
            else
            {
                item.SPMC = CommonTool.GetDisCountMC(zkl, zkhs);
            }
            if (zkje != 0.0)
            {
                item.JE = -1.0 * zkje;
                double num10 = Finacial.Div(Math.Abs(zkje), num3, 5);
                if (!(num10 == zkl))
                {
                    zkl = num10;
                }
                item.ZKL = zkl;
            }
            else
            {
                double num11 = Finacial.Mul(num3, zkl, 7);
                item.JE = -1.0 * Finacial.GetRound(num11, 2);
                item.ZKL = zkl;
            }
            item.DJHXZ = 4;
            item.SLV = bill.ListGoods[num].SLV;
            if (((bill.DJZL == "s") && bill.HYSY) && (item.SLV == 0.05))
            {
                item.SE = -1.0 * GetRound((double) ((Math.Abs(item.JE) / 0.95) * 0.05), 2);
            }
            else
            {
                double round = Finacial.GetRound((double) (item.JE * item.SLV), 2);
                if (Math.Abs((double) (item.SLV - 0.015)) < 1E-06)
                {
                    round = Finacial.GetRound((double) (item.JE * 0.014492753623188406), 2);
                }
                if ((zkl == 1.0) && ((Math.Abs(round) > num2) || (num3 == zkje)))
                {
                    item.SE = -1.0 * Finacial.GetRound((double) (num2 * zkl), 7);
                }
                else
                {
                    item.SE = round;
                }
                if (zkl == 1.0)
                {
                    double num13 = Math.Abs(round) - Math.Abs(num2);
                    item.SE = Math.Abs(num2) * -1.0;
                }
            }
            item.SE = Finacial.GetRound(item.SE, 2);
            if (!(crzkse == 0.0))
            {
                item.SE = Finacial.GetRound((double) (-1.0 * crzkse), 2);
            }
            if (item.JE == 0.0)
            {
                for (num8 = num; num8 < (num + zkhs); num8++)
                {
                    bill.ListGoods[num8].DJHXZ = 0;
                }
                return str;
            }
            bill.ListGoods.Insert(selectIndex + 1, item);
            return str;
        }

        public string DisCount2(SaleBill bill, int selectIndex, int zkhs, double zkl, double zkje = 0.0)
        {
            if ((selectIndex < 0) || (zkhs < 1))
            {
                return "e1";
            }
            zkl = Finacial.GetRound(zkl, 5);
            zkje = Finacial.GetRound(zkje, 2);
            Goods good = bill.GetGood(selectIndex);
            int num = (selectIndex - zkhs) + 1;
            if (Finacial.Equal(zkl, 0.0) && Finacial.Equal(zkje, 0.0))
            {
                for (int j = num; j < (num + zkhs); j++)
                {
                    bill.ListGoods[j].DJHXZ = 0;
                }
                return "1";
            }
            double sE = 0.0;
            double num4 = 0.0;
            if (zkhs > 1)
            {
                int num5 = zkhs;
                double num6 = 0.0;
                int num7 = selectIndex;
                do
                {
                    if (this.DisCount2(bill, num7, 1, zkl, 0.0) == "0")
                    {
                        double jE = bill.ListGoods[num7 + 1].JE;
                        if (bill.ContainTax)
                        {
                            double sLV = bill.ListGoods[num7 + 1].SLV;
                            jE = Finacial.Mul(jE, 1.0 + sLV, 15);
                        }
                        num6 += Finacial.Add(num6, Math.Abs(jE), 15);
                    }
                    else
                    {
                        return "e2";
                    }
                    num7--;
                    num5--;
                }
                while (num5 > 1);
                if (num5 == 1)
                {
                    string str3;
                    double num10 = zkje - num6;
                    if (num10 <= 0.0)
                    {
                        str3 = this.DisCount2(bill, num7, 1, zkl, 0.0);
                    }
                    else
                    {
                        str3 = this.DisCount2(bill, num7, 1, zkl, num10);
                    }
                }
            }
            else
            {
                Goods item = new Goods {
                    XSDJBH = bill.BH,
                    SPMC = bill.ListGoods[num].SPMC,
                    XH = bill.ListGoods[num].XH,
                    SPSM = bill.ListGoods[num].SPSM,
                    Reserve = bill.ListGoods[num].Reserve,
                    DJHXZ = 4,
                    SLV = bill.ListGoods[num].SLV,
                    HSJBZ = bill.ListGoods[num].HSJBZ,
                    LSLVBS = bill.ListGoods[num].LSLVBS,
                    XSYHSM = bill.ListGoods[num].XSYHSM,
                    XSYH = bill.ListGoods[num].XSYH,
                    FLBM = bill.ListGoods[num].FLBM,
                    SPBM = bill.ListGoods[num].SPBM
                };
                num4 = bill.ListGoods[num].JE;
                sE = bill.ListGoods[num].SE;
                if (zkje > 0.0)
                {
                    if (bill.ContainTax)
                    {
                        double num11 = bill.ListGoods[num].SLV;
                        zkje = Finacial.Div(zkje, 1.0 + num11, 15);
                    }
                    item.JE = -1.0 * zkje;
                    double num12 = Finacial.Div(Math.Abs(zkje), num4, 5);
                    if (!(num12 == zkl))
                    {
                        zkl = num12;
                    }
                }
                else
                {
                    zkje = Finacial.Mul(num4, zkl, 7);
                    item.JE = -1.0 * Finacial.GetRound(zkje, 2);
                }
                item.ZKL = zkl;
                if (((bill.DJZL == "s") && bill.HYSY) && (item.SLV == 0.05))
                {
                    item.SE = -1.0 * GetRound((double) ((Math.Abs(item.JE) / 0.95) * 0.05), 2);
                }
                else
                {
                    double round = Finacial.GetRound((double) (item.JE * item.SLV), 2);
                    if (Math.Abs((double) (item.SLV - 0.015)) < 1E-06)
                    {
                        round = Finacial.GetRound((double) (item.JE * 0.014492753623188406), 2);
                    }
                    if ((zkl == 1.0) && ((Math.Abs(round) > sE) || (num4 == zkje)))
                    {
                        item.SE = -1.0 * Finacial.GetRound((double) (sE * zkl), 7);
                    }
                    else
                    {
                        item.SE = round;
                    }
                }
                item.SE = Finacial.GetRound(item.SE, 2);
                if (item.JE == 0.0)
                {
                    bill.ListGoods[num].DJHXZ = 0;
                }
                else
                {
                    bill.ListGoods[num].DJHXZ = 3;
                    bill.ListGoods.Insert(num + 1, item);
                }
            }
            for (int i = 0; i < bill.ListGoods.Count; i++)
            {
                bill.ListGoods[i].XH = i + 1;
            }
            return "0";
        }

        public void DisCountChangeRowNO(SaleBill bill, int selectIndex, ref int zkhs, double zkl, ref double totalAmount, ref double zkje)
        {
            int num7;
            totalAmount = 0.0;
            zkje = 0.0;
            int num = 0;
            if (zkhs == 0)
            {
                zkhs = 1;
            }
            int num2 = 0;
            double sLV = 0.0;
            sLV = bill.GetGood(selectIndex).SLV;
            int mxID = selectIndex;
            while (true)
            {
                if (mxID <= -1)
                {
                    mxID = 0;
                    break;
                }
                Goods good = bill.GetGood(mxID);
                double num5 = good.SLV;
                if ((((good.DJHXZ == 0) && (num5 == sLV)) && (good.FPHM == 0)) && (good.JE >= 0.0))
                {
                    mxID--;
                    num2++;
                }
                else
                {
                    mxID++;
                    break;
                }
            }
            num = num2;
            if (!CommonTool.isSPBMVersion())
            {
                if (zkhs > num2)
                {
                    zkhs = num2;
                }
            }
            else
            {
                int num6 = 0;
                for (num7 = selectIndex; num7 >= 0; num7--)
                {
                    if (!(bill.ListGoods[num7].DJHXZ.ToString().Trim() == "0"))
                    {
                        break;
                    }
                    num6++;
                }
                if (zkhs > num6)
                {
                    zkhs = num6;
                }
            }
            double num8 = 0.0;
            double num9 = 0.0;
            mxID = (selectIndex - zkhs) + 1;
            for (num7 = selectIndex; num7 > (selectIndex - zkhs); num7--)
            {
                num8 += bill.GetGood(num7).JE;
                num9 += bill.GetGood(num7).SE;
            }
            totalAmount = num8;
            if (bill.ContainTax)
            {
                totalAmount = num8 + num9;
            }
            double num10 = bill.ListGoods[selectIndex].SLV;
            zkje = zkl * totalAmount;
        }

        public string DisCountValidate(SaleBill bill, int selectIndex)
        {
            string str = "0";
            Goods good = bill.GetGood(selectIndex);
            if (good.DJHXZ != 0)
            {
                return "折扣行不能继续折扣！";
            }
            if (good.JE == 0.0)
            {
                return "金额为零不能折扣！";
            }
            if (good.JE < 0.0)
            {
                return "负数商品行不能添加折扣";
            }
            if (good.FPHM > 0)
            {
                str = "已生成发票明细不能添加折扣";
            }
            return str;
        }

        public string EditGoodsBaseYY(SaleBill bill, int mxID, string field, string strValue)
        {
            double jE;
            double sE;
            double sL;
            double dJ;
            double sLV;
            string str7;
            double result = 0.0;
            Goods goods = bill.ListGoods[mxID];
            List<string> list = new List<string>();
            list.AddRange(new string[] { "JE", "SL", "DJ", "SLV", "SE" });
            if (list.Contains(field))
            {
                double num8;
                if (strValue == null)
                {
                    strValue = "0";
                }
                if (!this.TryTodouble(strValue, out result) && !string.IsNullOrEmpty(strValue))
                {
                    return field;
                }
                if ((field == "DJ") && (result < 0.0))
                {
                    return "DJ-";
                }
                bool flag2 = (bill.DJZL == "s") && bill.HYSY;
                bool flag3 = (flag2 && (goods.SLV == 0.05)) || (flag2 && ((field == "SLV") && (result == 0.05)));
                jE = goods.JE;
                sE = goods.SE;
                sL = goods.SL;
                dJ = goods.DJ;
                sLV = goods.SLV;
                if (!flag3)
                {
                    str7 = field;
                    if (str7 != null)
                    {
                        if (!(str7 == "JE"))
                        {
                            if (str7 == "SL")
                            {
                                goods.setSl(result, bill.ContainTax);
                            }
                            else if (str7 == "DJ")
                            {
                                goods.setDj(result, bill.ContainTax);
                            }
                            else if (str7 == "SLV")
                            {
                                num8 = result;
                                if ((num8 <= 0.0) || (num8 >= 1.0))
                                {
                                    if ((num8 >= 1.0) && (num8 < 100.0))
                                    {
                                        num8 = Finacial.Div(num8, 100.0, 15);
                                    }
                                    else
                                    {
                                        num8 = 0.0;
                                    }
                                }
                                goods.SLV = num8;
                                goods.SE = GetRound((double) (goods.SLV * goods.JE), 2);
                                if (!(goods.SL == 0.0))
                                {
                                    goods.DJnoTax = Finacial.Div(goods.JE, goods.SL, 15);
                                }
                            }
                            else if (str7 == "SE")
                            {
                                goods.SE = GetRound(result, 2);
                            }
                        }
                        else
                        {
                            if (!bill.ContainTax)
                            {
                                goods.JE = GetRound(result, 2);
                            }
                            else
                            {
                                goods.JE = GetRound((double) (result / (1.0 + goods.SLV)), 2);
                            }
                            if (goods.JE > 0.0)
                            {
                                if (goods.SL > 0.0)
                                {
                                    goods.DJnoTax = goods.JE / goods.SL;
                                }
                                else if (goods.SL < 0.0)
                                {
                                    goods.SL = goods.JE / goods.DJnoTax;
                                }
                                else if (!(goods.DJ == 0.0))
                                {
                                    goods.SL = goods.JE / goods.DJnoTax;
                                }
                            }
                            else if (!(goods.SL == 0.0))
                            {
                                goods.SL = goods.JE / goods.DJnoTax;
                            }
                            goods.SE = GetRound((double) (goods.JE * goods.SLV), 2);
                        }
                    }
                    goto Label_0768;
                }
                str7 = field;
                if (str7 != null)
                {
                    if (!(str7 == "JE"))
                    {
                        if (str7 == "SL")
                        {
                            goods.SL = result;
                            goods.JE = GetRound((double) (((goods.DJ * 19.0) / 20.0) * goods.SL), 2);
                            goods.SE = GetRound((double) (goods.JE / 19.0), 2);
                        }
                        else if (str7 == "DJ")
                        {
                            goods.DJ = result;
                            goods.JE = GetRound((double) (((goods.DJ * 19.0) / 20.0) * goods.SL), 2);
                            goods.SE = GetRound((double) (goods.JE / 19.0), 2);
                        }
                        else if (str7 == "SLV")
                        {
                            num8 = result;
                            if ((num8 <= 0.0) || (num8 >= 1.0))
                            {
                                if ((num8 >= 1.0) && (num8 < 100.0))
                                {
                                    num8 = Finacial.Div(num8, 100.0, 15);
                                }
                                else
                                {
                                    num8 = 0.0;
                                }
                            }
                            goods.SLV = num8;
                            double num9 = goods.JE / 19.0;
                            goods.SE = GetRound(num9, 2);
                            if (!(goods.SL == 0.0))
                            {
                                goods.DJ = (goods.JE + num9) / goods.SL;
                            }
                        }
                        else if (str7 == "SE")
                        {
                            goods.SE = GetRound(result, 2);
                        }
                    }
                    else
                    {
                        goods.JE = result;
                        double num7 = (goods.JE * goods.SLV) / 0.95;
                        goods.SE = GetRound(num7, 2);
                        if (!(goods.SL == 0.0))
                        {
                            goods.DJ = (num7 + goods.JE) / goods.SL;
                        }
                    }
                }
            }
            else
            {
                if (field == "HSJBZ")
                {
                    if (strValue == null)
                    {
                        strValue = "false";
                    }
                    goods.HSJBZ = Convert.ToBoolean(strValue);
                }
                else
                {
                    if (strValue == null)
                    {
                        strValue = "";
                    }
                    string str = Convert.ToString(strValue);
                    str7 = field;
                    if (str7 != null)
                    {
                        if (!(str7 == "SPMC"))
                        {
                            if (str7 == "JLDW")
                            {
                                goods.JLDW = str;
                            }
                            else if (str7 == "GGXH")
                            {
                                goods.GGXH = str;
                            }
                            else if (str7 == "SPSM")
                            {
                                goods.SPSM = str;
                            }
                        }
                        else
                        {
                            goods.SPMC = str;
                        }
                    }
                }
                return "0";
            }
            goods.HSJBZ = true;
        Label_0768:
            if (list.Contains(field))
            {
                double num10 = Convert.ToDouble("0.000000000000001");
                double num11 = Convert.ToDouble("999999999999999.99");
                string str2 = "0";
                if ((goods.JE > 1E+15) || ((goods.JE > 0.0) && (goods.JE < 1E-15)))
                {
                    str2 = "JESEex";
                }
                else if ((goods.SE > 1E+15) || ((goods.SE > 0.0) && (goods.SE < 1E-15)))
                {
                    str2 = "JESEex";
                }
                if (str2 != "0")
                {
                    goods.JE = jE;
                    goods.SE = sE;
                    goods.SL = sL;
                    goods.DJ = dJ;
                    goods.SLV = sLV;
                    return str2;
                }
                if (field == "DJ")
                {
                    if (Finacial.Equal(goods.JE, 0.0) || Finacial.Equal(goods.SE, 0.0))
                    {
                        decimal num12 = 0M;
                        if (!((strValue.Length <= 0) || decimal.TryParse(strValue, out num12)))
                        {
                        }
                        string str3 = num12.ToString();
                        goods.strEEEDj = str3;
                        if (str3.Length > Aisino.Fwkp.Wbjk.PARAMS.DJ_MAX_LEN)
                        {
                            return "DJex1";
                        }
                    }
                    else if ((goods.DJ > 1E+15) || (goods.DJ < 1E-15))
                    {
                        str2 = "DJex1";
                    }
                    else
                    {
                        string str5 = GetSubDecimal(decimal.Round(decimal.Parse(strValue), Aisino.Fwkp.Wbjk.PARAMS.DJ_MAX_DECIMALS, MidpointRounding.AwayFromZero).ToString(), Aisino.Fwkp.Wbjk.PARAMS.DJ_MAX_LEN, false);
                        if (strValue != str5)
                        {
                            goods.strEEEDj = str5;
                        }
                        else
                        {
                            goods.strEEEDj = "";
                        }
                    }
                }
            }
            return "0";
        }

        private void EditGoodsSplit(SaleBill bill, Goods mx, string field, object value, double SourceSE, double SourceJE, double SourceSL, bool SourceCF)
        {
            double result = 0.0;
            List<string> list = new List<string>();
            list.AddRange(new string[] { "JE", "SL", "DJ", "SLV", "SE" });
            if (!list.Contains(field) || this.TryTodouble(value, out result))
            {
                double sE;
                double jE;
                string str;
                double sL = 0.0;
                if (!bill.HYSY || !(mx.SLV == 0.05))
                {
                    if (!bill.JZ_50_15 || (Math.Abs((double) (mx.SLV - 0.015)) >= 1E-05))
                    {
                        str = field;
                        if (str != null)
                        {
                            if (!(str == "JE"))
                            {
                                if (str == "SL")
                                {
                                    sL = mx.SL;
                                    mx.SL = result;
                                    if (SourceCF)
                                    {
                                        mx.JE = GetRound((double) ((result / sL) * mx.JE), 2);
                                        mx.SE = GetRound((double) (mx.JE * mx.SLV), 2);
                                    }
                                    else
                                    {
                                        jE = mx.JE;
                                        mx.JE = GetRound((double) (SourceJE - jE), 2);
                                        sE = mx.SE;
                                        mx.SE = GetRound((double) (SourceSE - sE), 2);
                                    }
                                }
                                else if (str == "SE")
                                {
                                    sL = mx.SE;
                                    mx.SE = GetRound(result, 2);
                                    mx.JE = GetRound((double) ((result / sL) * mx.JE), 2);
                                    mx.SL = (result / sL) * mx.SL;
                                }
                            }
                            else
                            {
                                sL = SourceJE;
                                mx.JE = GetRound(result, 2);
                                if ((mx.DJ == 0.0) && (mx.SL == 0.0))
                                {
                                    mx.SL = 0.0;
                                }
                                else
                                {
                                    mx.SL = (result / sL) * SourceSL;
                                }
                                if (SourceCF)
                                {
                                    mx.SE = GetRound((double) (mx.JE * mx.SLV), 2);
                                }
                                else
                                {
                                    sE = mx.SE;
                                    mx.SE = GetRound((double) (SourceSE - sE), 2);
                                }
                            }
                        }
                    }
                    else
                    {
                        str = field;
                        if (str != null)
                        {
                            if (!(str == "JE"))
                            {
                                if (str == "SL")
                                {
                                    sL = mx.SL;
                                    mx.SL = result;
                                    if (SourceCF)
                                    {
                                        mx.JE = GetRound((double) ((result / sL) * mx.JE), 2);
                                        mx.SE = GetRound((double) ((mx.JE / 1.035) * 0.015), 2);
                                    }
                                    else
                                    {
                                        jE = mx.JE;
                                        mx.JE = GetRound((double) (SourceJE - jE), 2);
                                        sE = mx.SE;
                                        mx.SE = GetRound((double) (SourceSE - sE), 2);
                                    }
                                }
                                else if (str == "SE")
                                {
                                    sL = mx.SE;
                                    mx.SE = GetRound(result, 2);
                                    mx.JE = GetRound((double) ((result / sL) * mx.JE), 2);
                                    mx.SL = (result / sL) * mx.SL;
                                }
                            }
                            else
                            {
                                sL = SourceJE;
                                mx.JE = GetRound(result, 2);
                                if ((mx.DJ == 0.0) && (mx.SL == 0.0))
                                {
                                    mx.SL = 0.0;
                                }
                                else
                                {
                                    mx.SL = (result / sL) * SourceSL;
                                }
                                if (SourceCF)
                                {
                                    mx.SE = GetRound((double) ((mx.JE / 1.035) * 0.015), 2);
                                }
                                else
                                {
                                    sE = mx.SE;
                                    mx.SE = GetRound((double) (SourceSE - sE), 2);
                                }
                            }
                        }
                    }
                }
                else
                {
                    str = field;
                    if (str != null)
                    {
                        if (!(str == "JE"))
                        {
                            if (str == "SL")
                            {
                                sL = mx.SL;
                                mx.SL = result;
                                if (SourceCF)
                                {
                                    mx.JE = GetRound((double) ((result / sL) * mx.JE), 2);
                                    mx.SE = GetRound((double) ((mx.JE * mx.SLV) / (1.0 - mx.SLV)), 2);
                                }
                                else
                                {
                                    jE = mx.JE;
                                    mx.JE = GetRound((double) (SourceJE - jE), 2);
                                    sE = mx.SE;
                                    mx.SE = GetRound((double) (SourceSE - sE), 2);
                                }
                            }
                            else if (str == "SE")
                            {
                                sL = mx.SE;
                                mx.SE = GetRound(result, 2);
                                mx.JE = GetRound((double) ((result / sL) * mx.JE), 2);
                                mx.SL = (result / sL) * mx.SL;
                            }
                        }
                        else
                        {
                            sL = SourceJE;
                            mx.JE = GetRound(result, 2);
                            if ((mx.DJ == 0.0) && (mx.SL == 0.0))
                            {
                                mx.SL = 0.0;
                            }
                            else
                            {
                                mx.SL = (result / sL) * SourceSL;
                            }
                            if (SourceCF)
                            {
                                mx.SE = GetRound((double) ((mx.JE * mx.SLV) / (1.0 - mx.SLV)), 2);
                            }
                            else
                            {
                                sE = mx.SE;
                                mx.SE = GetRound((double) (SourceSE - sE), 2);
                            }
                        }
                    }
                }
            }
        }

        public SaleBill Find(string BH)
        {
            SaleBill bill = this.dbBill.Find(BH, "All");
            bill.ContainTax = false;
            return bill;
        }

        public SaleBill FindNotInv(string BH)
        {
            SaleBill bill = this.dbBill.Find(BH, "NotInv");
            bill.ContainTax = false;
            return bill;
        }

        private int GetDiscountRow(int zkid, List<Goods> listMxNew)
        {
            while (zkid < listMxNew.Count)
            {
                if (listMxNew[zkid].DJHXZ != 4)
                {
                    zkid++;
                }
                else
                {
                    return zkid;
                }
            }
            return 0;
        }

        public static decimal GetRound(decimal value, int digits = 2)
        {
            return Math.Round(value, digits, MidpointRounding.AwayFromZero);
        }

        public static double GetRound(double value, int digits = 2)
        {
            return Math.Round(value, digits, MidpointRounding.AwayFromZero);
        }

        internal static string GetSubDecimal(string s, int len, bool addTail)
        {
            try
            {
                if ((s == null) || (s.Trim().Length == 0))
                {
                    if (!addTail)
                    {
                        return string.Empty;
                    }
                    return new string(' ', len);
                }
                decimal d = decimal.Parse(s);
                string str = d.ToString();
                int length = str.Length;
                if (length > len)
                {
                    int index = str.IndexOf('.');
                    if (index >= 0)
                    {
                        int num4 = (length - (index + 1)) - (length - len);
                        if (num4 >= -1)
                        {
                            return decimal.Round(d, (num4 < 0) ? 0 : num4, MidpointRounding.AwayFromZero).ToString();
                        }
                        return null;
                    }
                    if (length > len)
                    {
                        return null;
                    }
                    if (addTail)
                    {
                        return new StringBuilder().Append(str).Append(' ', len - length).ToString();
                    }
                    return str;
                }
                if (addTail)
                {
                    return new StringBuilder().Append(str).Append(' ', len - length).ToString();
                }
                return str;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DateTime GetTaxCardDateTime()
        {
            DateTime now = DateTime.Now;
            try
            {
                now = TaxCardFactory.CreateTaxCard().get_TaxClock();
            }
            catch (Exception exception)
            {
                this.log.Error("读取金税卡接口时异常:" + exception.ToString());
            }
            return now;
        }

        public string InsertGoods(SaleBill bill, Goods mx, int index, int InsertUp = 0)
        {
            string str = "0";
            mx.XSDJBH = bill.BH;
            if (this.CanEditBill(bill) == 1)
            {
                return "2";
            }
            if (bill.ListGoods.Count == 0)
            {
                mx.XH = 1;
                bill.ListGoods.Add(mx);
                return str;
            }
            if (index < 0)
            {
                return "OO";
            }
            if (InsertUp == 1)
            {
                if ((index > 0) && (bill.ListGoods[index - 1].DJHXZ == 3))
                {
                    str = "被折扣商品行之间不能添加商品行！";
                }
                else
                {
                    bill.ListGoods.Insert(index, mx);
                }
            }
            else if (InsertUp == 0)
            {
                if (bill.ListGoods[index].DJHXZ == 3)
                {
                    str = "被折扣商品行之间不能添加商品行！";
                }
                else
                {
                    bill.ListGoods.Insert(index + 1, mx);
                }
            }
            for (int i = 0; i < bill.ListGoods.Count; i++)
            {
                bill.ListGoods[i].XH = i + 1;
            }
            return str;
        }

        public bool IsCanKP(SaleBill bill, int[] slvjeseIndex)
        {
            FPGenerateResult result = new FPGenerateResult(bill);
            string[] strArray = new string[0x10];
            if (this.IsCES(bill))
            {
                return true;
            }
            Invoice invoice2 = GenerateInvoice.Instance.CreateInvoiceHead(result, slvjeseIndex, ref strArray, bill);
            invoice2.SetQdbz(false);
            for (int i = 0; i < bill.ListGoods.Count; i++)
            {
                Goods goods = bill.ListGoods[i];
                bool flag = false;
                if (bill.HYSY && (goods.SLV == 0.05))
                {
                    flag = true;
                }
                if (flag)
                {
                    invoice2.SetZyfpLx(1);
                }
                bool flag2 = false;
                if (bill.JZ_50_15 && (goods.SLV == 0.015))
                {
                    flag2 = true;
                }
                if (flag2)
                {
                    invoice2.SetZyfpLx(10);
                }
                if (goods.DJHXZ == 4)
                {
                    string str = Convert.ToString(goods.JE);
                    string str2 = Convert.ToString(goods.SE);
                    int zKH = 0;
                    double round = 0.0;
                    if (CommonTool.isSPBMVersion())
                    {
                        double jE = bill.ListGoods[i - 1].JE;
                        round = CommonTool.GetDisCount(Math.Abs(goods.JE), jE, ref zKH);
                    }
                    else
                    {
                        round = GetRound(CommonTool.GetDisCount(goods.SPMC, ref zKH), 5);
                    }
                    string str4 = ((decimal) round).ToString();
                    int count = invoice2.GetSpxxs().Count;
                    if (invoice2.AddZkxx(count - 1, zKH, str4, str, str2) < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    string sPMC;
                    string sPSM;
                    string str7;
                    string str8;
                    ZYFP_LX zyfp_lx;
                    Spxx spxx;
                    if (goods.DJHXZ == 3)
                    {
                        sPMC = goods.SPMC;
                        sPSM = goods.SPSM;
                        str7 = Convert.ToString(goods.SLV);
                        str8 = Convert.ToString((decimal) goods.DJ);
                        if (flag)
                        {
                            zyfp_lx = 1;
                        }
                        else if (flag2)
                        {
                            zyfp_lx = 10;
                        }
                        else
                        {
                            zyfp_lx = 0;
                        }
                        if ((bill.DJZL == "s") && (zyfp_lx == 0))
                        {
                            switch (this.xtCheck(sPMC))
                            {
                                case "1":
                                    zyfp_lx = 6;
                                    invoice2.SetZyfpLx(zyfp_lx);
                                    break;

                                case "2":
                                    zyfp_lx = 7;
                                    invoice2.SetZyfpLx(zyfp_lx);
                                    break;
                            }
                        }
                        spxx = new Spxx(sPMC, sPSM, str7, goods.GGXH, goods.JLDW, str8, goods.HSJBZ, zyfp_lx);
                        spxx.set_Je(Convert.ToString(goods.JE));
                        spxx.set_Se(Convert.ToString(goods.SE));
                        spxx.set_SL(Convert.ToString((decimal) goods.SL));
                        if (invoice2.AddSpxx(spxx) < 0)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        sPMC = goods.SPMC;
                        sPSM = goods.SPSM;
                        str7 = Convert.ToString(goods.SLV);
                        str8 = Convert.ToString((decimal) goods.DJ);
                        if (flag)
                        {
                            zyfp_lx = 1;
                        }
                        else if (flag2)
                        {
                            zyfp_lx = 10;
                        }
                        else
                        {
                            zyfp_lx = 0;
                        }
                        if ((bill.DJZL == "s") && (zyfp_lx == 0))
                        {
                            switch (this.xtCheck(sPMC))
                            {
                                case "1":
                                    zyfp_lx = 6;
                                    invoice2.SetZyfpLx(zyfp_lx);
                                    break;

                                case "2":
                                    zyfp_lx = 7;
                                    invoice2.SetZyfpLx(zyfp_lx);
                                    break;
                            }
                        }
                        spxx = new Spxx(sPMC, sPSM, str7, goods.GGXH, goods.JLDW, str8, goods.HSJBZ, zyfp_lx);
                        spxx.set_Je(Convert.ToString(goods.JE));
                        spxx.set_Se(Convert.ToString(goods.SE));
                        spxx.set_SL(Convert.ToString((decimal) goods.SL));
                        if (invoice2.AddSpxx(spxx) < 0)
                        {
                            return false;
                        }
                    }
                }
            }
            strArray[slvjeseIndex[10]] = invoice2.GetHjSe();
            strArray[slvjeseIndex[14]] = invoice2.get_SLv();
            strArray[slvjeseIndex[15]] = invoice2.GetHjJeNotHs();
            byte[] buffer = new byte[0x10];
            object[] objArray = new object[] { buffer, strArray };
            invoice2.SetData(objArray);
            invoice2.CheckFpData();
            return (invoice2.GetCode() == "0000");
        }

        public bool IsCES(SaleBill bill)
        {
            int count = bill.ListGoods.Count;
            for (int i = 0; i < count; i++)
            {
                Goods goods = bill.ListGoods[i];
                if ((goods.DJHXZ != 4) && !(goods.KCE == 0.0))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsSWDK()
        {
            try
            {
                string str = TaxCardFactory.CreateTaxCard().get_TaxCode();
                int companyType = TaxCardFactory.CreateTaxCard().get_StateInfo().CompanyType;
                if ((!string.IsNullOrEmpty(str) && (str.Length == 15)) && (str.Substring(8, 2) == "DK"))
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.log.Error("读取金税卡接口时异常:" + exception.ToString());
            }
            return false;
        }

        private void ListSlvSort(List<Goods> listMxNew)
        {
            double sLV = listMxNew[0].SLV;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            while (num2 < (listMxNew.Count - 1))
            {
                num2 += num4;
                num3 = num2 + 1;
                num4 = 0;
                while (num3 < listMxNew.Count)
                {
                    Goods item = listMxNew[num3];
                    if (listMxNew[num2].SLV == listMxNew[num3].SLV)
                    {
                        num4++;
                        if ((num3 - num2) != 1)
                        {
                            listMxNew.Remove(item);
                            listMxNew.Insert(num2 + num4, item);
                        }
                    }
                    num3++;
                }
                num2++;
            }
        }

        public string MergeSaleBill(List<SaleBill> listBill, SaleBill mergedBill, MergerType mType, MergerRemarkType MergerRemark)
        {
            int num;
            Goods goods;
            double num6;
            double num7;
            int num10;
            TaxCard card = TaxCardFactory.CreateTaxCard();
            bool flag = this.IsSWDK();
            string str = "0";
            if (listBill.Count == 0)
            {
                return "没有单据";
            }
            if ((listBill.Count < 2) && (mType == MergerType.SimpleMerger))
            {
                return "一条单据无需进行简单合并";
            }
            if (listBill.Count > 800)
            {
                return "一次选中单据行数不能超过 800";
            }
            for (num = 0; num < listBill.Count; num++)
            {
                SaleBill bill = listBill[num];
                if (this.IsCES(bill))
                {
                    return "所选单据存在差额税单据，不能进行合并";
                }
                string str2 = this.dbBill.BeSpliOrColl(bill.BH);
                if (str2 != "0")
                {
                    return ("编号为" + bill.BH + " 的单据" + str2 + "，\n无法合并");
                }
                if (listBill[0].GFSH != bill.GFSH)
                {
                    return "所选单据不是同一个客户，不能进行合并";
                }
                if (listBill[0].GFMC != bill.GFMC)
                {
                    return "所选单据不是同一个客户，不能进行合并";
                }
                if (listBill[0].DJZL != bill.DJZL)
                {
                    return "所选单据不是同一种类的单据，不能进行合并";
                }
                if ((listBill[num].DJZL == "s") && (listBill[0].HYSY != bill.HYSY))
                {
                    return "所选专用发票单据不是同一中外合作油气田标志的单据，不能进行合并";
                }
                if (listBill[0].JZ_50_15 != bill.JZ_50_15)
                {
                    return "1.5%税率商品不允许与其他税率商品混开，不能进行合并";
                }
                if ((num == 0) && (ToolUtil.GetByteCount(listBill[num].BH) >= 0x31))
                {
                    return "单据编号长度过长，不能进行合并";
                }
                string str4 = this.CheckZK(bill);
                if (!str4.Equals("0"))
                {
                    return str4;
                }
            }
            List<Goods> listMX = new List<Goods>();
            foreach (SaleBill bill in listBill)
            {
                listMX.AddRange(bill.ListGoods);
            }
            SaleBillCheck instance = SaleBillCheck.Instance;
            mergedBill.CopySaleBill(listBill[0]);
            mergedBill.ListGoods = listMX;
            bool flag2 = false;
            TaxCard card2 = TaxCardFactory.CreateTaxCard();
            if ((card2.get_ECardType() == 3) && (card2.get_StateInfo().TBType == 0))
            {
                flag2 = true;
            }
            if (flag2)
            {
                if (mType == MergerType.ComplexMerger)
                {
                }
            }
            else if (instance.BillHasMultiSlv(mergedBill))
            {
                return "所选单据含有多个税率，不能进行合并";
            }
            if (CommonTool.isSPBMVersion())
            {
                int num3 = 0;
                for (num = 0; num < listMX.Count; num++)
                {
                    goods = listMX[num];
                    if (goods.DJHXZ == 3)
                    {
                        num3++;
                    }
                    else if (goods.DJHXZ == 4)
                    {
                        num3 = 0;
                    }
                    if (num3 > 1)
                    {
                        return "一个折扣组包含两个或两个以上折扣行，不能进行合并";
                    }
                }
            }
            if (mType == MergerType.ComplexMerger)
            {
                str = this.ComplexMerge(listMX);
                if (str != "0")
                {
                    return str;
                }
            }
            string str5 = string.Empty;
            string str6 = string.Empty;
            double round = 0.0;
            bool flag4 = false;
            for (num = 0; num < listBill.Count; num++)
            {
                if (flag && (num == 0))
                {
                    round += listBill[num].JEHJ;
                    str5 = str5 + listBill[num].BZ + "\r\n";
                    str6 = str6 + listBill[num].BH + ", ";
                }
                else
                {
                    round += listBill[num].JEHJ;
                    str5 = str5 + listBill[num].BZ + " ";
                    str6 = str6 + listBill[num].BH + ", ";
                }
                if (listBill[num].SFZJY)
                {
                    flag4 = true;
                }
            }
            round = GetRound(round, 2);
            mergedBill.CopySaleBill(listBill[0]);
            mergedBill.BH = mergedBill.BH + "~0";
            mergedBill.JEHJ = round;
            mergedBill.ListGoods = listMX;
            str5 = str5.Trim();
            if (str5.Length > 240)
            {
                str5 = str5.Substring(0, 240);
            }
            str6 = str6.Trim();
            str6 = str6.Substring(0, str6.Length - 1);
            if (str6.Length > 240)
            {
                str6 = str6.Substring(0, 240);
            }
            if ((MergerRemark == MergerRemarkType.DoMergerRemarkFill) && string.IsNullOrEmpty(str5))
            {
                str5 = str6;
            }
            if ((MergerRemark == MergerRemarkType.DoMergerRemarkFill) || (MergerRemark == MergerRemarkType.OnlyMergerRemark))
            {
                mergedBill.BZ = str5;
            }
            else if (MergerRemark == MergerRemarkType.NoMergerRemark)
            {
                mergedBill.BZ = "";
            }
            if (flag4)
            {
                mergedBill.SFZJY = true;
            }
            this.CleanBill(mergedBill);
            if (mergedBill.DJZL == "c")
            {
                if (!card2.get_QYLX().ISPTFP)
                {
                    return "无法取得普通发票开票限额，不能合并!";
                }
            }
            else if ((mergedBill.DJZL == "s") && !card2.get_QYLX().ISZYFP)
            {
                return "无法取得专用发票开票限额，不能合并!";
            }
            double invLimit = TaxCardValue.GetInvLimit(CommonTool.GetInvType(mergedBill.DJZL));
            if (mergedBill.HYSY)
            {
                num6 = 0.0;
                num7 = 0.0;
                for (num = 0; num < mergedBill.ListGoods.Count; num++)
                {
                    if (mergedBill.ListGoods[num].SLV == 0.05)
                    {
                        num6 += mergedBill.ListGoods[num].JE + mergedBill.ListGoods[num].SE;
                        if (!mergedBill.ListGoods[num].HSJBZ)
                        {
                            num10 = num + 1;
                            return ("合并后的第" + num10.ToString() + "行，单价乘以数量不等于含税金额");
                        }
                    }
                    else
                    {
                        return "合并后海洋石油单据商品行存在非海洋石油税率，不能进行合并。";
                    }
                }
                num6 = GetRound(num6, 2);
                num7 = GetRound(num7, 2);
                if (num6 > invLimit)
                {
                    return "所选单据合并后的金额或税额超过开票限额，不能进行合并。";
                }
            }
            else if (mergedBill.JZ_50_15)
            {
                num6 = 0.0;
                num7 = 0.0;
                for (num = 0; num < mergedBill.ListGoods.Count; num++)
                {
                    if (mergedBill.ListGoods[num].SLV == 0.015)
                    {
                        num6 += mergedBill.ListGoods[num].JE;
                    }
                    else
                    {
                        return "合并后1.5%税率单据商品行存在其他税率，不能进行合并。";
                    }
                }
                num6 = GetRound(num6, 2);
                num7 = GetRound(num7, 2);
                if (num6 > invLimit)
                {
                    return "所选单据合并后的金额或税额超过开票限额，不能进行合并。";
                }
            }
            else if (round > invLimit)
            {
                return "所选单据合并后的金额或税额超过开票限额，不能进行合并。";
            }
            double num8 = 0.0;
            for (num = 0; num < mergedBill.ListGoods.Count; num++)
            {
                goods = mergedBill.ListGoods[num];
                double num9 = 0.0;
                if (goods.JE == 0.0)
                {
                    num10 = num + 1;
                    return ("合并后，第" + num10.ToString() + "行，金额为0，无法进行合并");
                }
                if (mergedBill.HYSY && (goods.SLV == 0.05))
                {
                    num9 = GetRound((double) (((goods.JE / 0.95) * 0.05) - goods.SE), 2);
                }
                else if (mergedBill.JZ_50_15 && (Math.Abs((double) (goods.SLV - 0.015)) < 1E-05))
                {
                    num9 = GetRound((double) (((goods.JE / 1.035) * 0.015) - goods.SE), 2);
                }
                else
                {
                    num9 = GetRound((double) ((goods.JE * goods.SLV) - goods.SE), 2);
                }
                if (Math.Abs(num9) > 0.06)
                {
                    if (mergedBill.HYSY && (goods.SLV == 0.05))
                    {
                        num10 = num + 1;
                        return ("合并后，第" + num10.ToString() + "行，含税金额乘以税率减税额的绝对值大于0.06，无法进行合并");
                    }
                    if (mergedBill.JZ_50_15 && (Math.Abs((double) (goods.SLV - 0.015)) < 1E-05))
                    {
                        num10 = num + 1;
                        return ("合并后，第" + num10.ToString() + "行，1.5%税率商品行税额误差大于0.06，无法进行合并");
                    }
                    num10 = num + 1;
                    return ("合并后，第" + num10.ToString() + "行，金额乘以税率减税额的绝对值大于0.06，无法进行合并");
                }
                num8 += num9;
            }
            if (Math.Abs(GetRound(num8, 2)) > 1.27)
            {
                return "所选单据合并后的误差累计值大于1.27，不能进行合并";
            }
            List<SaleBill> billList = new List<SaleBill> {
                mergedBill
            };
            return this.dbBill.SaveToTempTable(billList, 2);
        }

        private string ModifyDiscountRow(SaleBill bill, int rowindex, int zkhIndex, string splittype, string field, double zkl = 0.0)
        {
            bool flag = CommonTool.isSPBMVersion();
            string sPMC = bill.ListGoods[zkhIndex].SPMC;
            int zKH = 0;
            if (flag)
            {
                zKH = 1;
            }
            else
            {
                CommonTool.GetDisCount(sPMC, ref zKH);
            }
            double num2 = 0.0;
            double num3 = 0.0;
            for (int i = 0; i < zKH; i++)
            {
                int num5 = zkhIndex - (i + 1);
                if ((num5 < 0) || (bill.ListGoods[num5].DJHXZ != 3))
                {
                    zKH = i;
                    break;
                }
                num2 += bill.ListGoods[num5].JE;
                num3 += bill.ListGoods[num5].SE;
            }
            if (splittype == "discountOut")
            {
                if (flag)
                {
                    bill.ListGoods[zkhIndex].SPMC = sPMC;
                }
                else
                {
                    bill.ListGoods[zkhIndex].SPMC = CommonTool.GetDisCountMC(zkl, zKH);
                }
                bill.ListGoods[zkhIndex].JE = -1.0 * GetRound((double) (num2 * zkl), 2);
                bill.ListGoods[zkhIndex].SE = -1.0 * GetRound((double) (num3 * zkl), 2);
            }
            else if (splittype == "discountHold")
            {
                double zKL = (-1.0 * bill.ListGoods[zkhIndex].JE) / num2;
                if (zKL > 1.0)
                {
                    if (field == "JE")
                    {
                        return "拆分金额不能低于折扣金额!";
                    }
                    if (field == "SL")
                    {
                        return "拆分数量不能导致金额低于折扣金额!";
                    }
                    return "拆分后的折扣率不能大于100%\n请确保拆分后的金额大于折扣金额";
                }
                if (flag)
                {
                    bill.ListGoods[zkhIndex].SPMC = sPMC;
                }
                else
                {
                    bill.ListGoods[zkhIndex].SPMC = CommonTool.GetDisCountMC(zKL, zKH);
                }
            }
            return "0";
        }

        public void OceanOilHasChanged(string beforeChangedDJZL, bool beforeChangedHYSY, SaleBill bill)
        {
            bool flag = (beforeChangedDJZL == "s") && beforeChangedHYSY;
            bool flag2 = (bill.DJZL == "s") && bill.HYSY;
            if (flag != flag2)
            {
                for (int i = 0; i < bill.ListGoods.Count; i++)
                {
                    Goods goods = bill.ListGoods[i];
                    if (goods.SLV == 0.05)
                    {
                        if (flag2 && (goods.SLV == 0.05))
                        {
                            goods.HSJBZ = true;
                        }
                        object jE = goods.JE;
                        string strValue = goods.JE.ToString();
                        this.EditGoodsBaseYY(bill, i, "JE", strValue);
                    }
                }
            }
        }

        public string RemoveGoods(SaleBill bill, int index)
        {
            string str = "0";
            if (bill.ListGoods.Count == 0)
            {
                return "没有可以删除的明细";
            }
            if (index >= bill.ListGoods.Count)
            {
                return "1";
            }
            Goods good = bill.GetGood(index);
            if ((good.FPHM > 0) && (good.FPDM.Length > 0))
            {
                return "不能删除已经生成发票的明细";
            }
            if (good.DJHXZ == 0)
            {
                bill.ListGoods.RemoveAt(index);
                return str;
            }
            if (good.DJHXZ == 4)
            {
                bill.ListGoods.RemoveAt(index);
                index--;
                while (index >= 0)
                {
                    if (bill.ListGoods[index].DJHXZ == 3)
                    {
                        bill.ListGoods[index].DJHXZ = 0;
                        index--;
                    }
                    else
                    {
                        return str;
                    }
                }
                return str;
            }
            if (good.DJHXZ == 3)
            {
                if (MessageBoxHelper.Show("您将要删除的商品行有折扣行，删除此行也可能将删除折扣行。你要继续吗？", "确认", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return "0";
                }
                int mxID = index;
                while (mxID >= 0)
                {
                    if (bill.ListGoods[mxID].DJHXZ != 3)
                    {
                        break;
                    }
                    mxID++;
                }
                if (bill.ListGoods[mxID].DJHXZ == 4)
                {
                    int zKH = 0;
                    double zkl = 0.0;
                    bool flag = CommonTool.isSPBMVersion();
                    if (flag)
                    {
                        zkl = CommonTool.GetDisCount(bill.GetGood(mxID).JE, bill.GetGood(mxID - 1).JE, ref zKH);
                    }
                    else
                    {
                        zkl = CommonTool.GetDisCount(bill.GetGood(mxID).SPMC, ref zKH);
                    }
                    bill.ListGoods.RemoveAt(mxID);
                    bill.ListGoods.RemoveAt(index);
                    if (flag)
                    {
                        this.DisCount2(bill, mxID - 2, zKH - 1, zkl, 0.0);
                    }
                    else
                    {
                        string str2 = this.DisCount(bill, mxID - 2, zKH - 1, zkl, 0.0, 0.0);
                    }
                }
            }
            return str;
        }

        public object[] SaleBillMonth()
        {
            return this.dbBill.SaleBillMonth();
        }

        public object[] SaleBillYearMonth(string DJZL1, string DJZL2 = "")
        {
            return this.dbBill.SaleBillYearMonth(DJZL1, DJZL2);
        }

        public string Save(SaleBill bill)
        {
            string str = "0";
            if ((bill.BH == null) || (bill.BH.Trim().Length == 0))
            {
                return "单据号码不能为空";
            }
            str = this.Check(bill);
            if (str == "0")
            {
                if (bill.IsANew)
                {
                    str = this.dbBill.AddSaveSaleBill(bill);
                    switch (str)
                    {
                        case "e2":
                            return "单据号码与已经合并或者拆分的单据相重";

                        case "e1":
                            return "单据号码相重";
                    }
                    return str;
                }
                this.dbBill.SaveSaleBill(bill);
            }
            return str;
        }

        public string SDCF_before(SaleBill bill)
        {
            SaleBillCheck instance = SaleBillCheck.Instance;
            bool flag = TaxCardFactory.CreateTaxCard().get_StateInfo().CompanyType != 0;
            FPLX fpzl = (bill.DJZL == "c") ? ((FPLX) 2) : ((FPLX) 0);
            int count = bill.ListGoods.Count;
            for (int i = 0; i < count; i++)
            {
                Goods goods = bill.ListGoods[i];
                if ((goods.DJHXZ == 0) || (goods.DJHXZ == 3))
                {
                    string sPMC = goods.SPMC;
                    string sPSM = goods.SPSM;
                    string str3 = Convert.ToString(goods.SLV);
                    string str4 = Convert.ToString((decimal) goods.DJ);
                    string jLDW = goods.JLDW;
                    Spxx spxx = new Spxx(sPMC, sPSM, str3, goods.GGXH, jLDW, str4, false, 0);
                    if (flag)
                    {
                        if (instance.IsOverLimit(fpzl, 0, goods.SPMC) == 3)
                        {
                            return "该单据存在单行商品的商品名称超出范围";
                        }
                        if (instance.IsOverLimit(fpzl, 1, goods.JLDW) == 3)
                        {
                            return "该单据存在单行商品的计量单位超出范围";
                        }
                    }
                    else
                    {
                        if (!sPMC.Equals(spxx.get_Spmc()))
                        {
                            return "该单据存在单行商品的商品名称超出范围";
                        }
                        if (!jLDW.Equals(spxx.get_JLdw()))
                        {
                            return "该单据存在单行商品的计量单位超出范围";
                        }
                    }
                }
            }
            return "0";
        }

        private int SeekDiscountRow(SaleBill bill, int rowId)
        {
            int num = rowId;
            Goods goods = bill.ListGoods[rowId];
            int num2 = -1;
            while (bill.ListGoods[num].DJHXZ == 3)
            {
                num++;
            }
            if (bill.ListGoods[num].DJHXZ == 4)
            {
                num2 = num;
            }
            return num2;
        }

        public string SeparateNeed(SaleBill bill, string SplitType, bool ExEWMInfoSplit, int[] slvjeseIndex)
        {
            double invLimit = TaxCardValue.GetInvLimit(bill.DJZL);
            if (bill.BH.Length > 0x30)
            {
                return "单据编号长度超过48位的单据无法拆分";
            }
            string str2 = this.dbBill.BeSpliOrColl(bill.BH);
            if (str2 != "0")
            {
                return ("编号为" + bill.BH + " 的单据" + str2 + "，\n无法再次进行拆分");
            }
            SaleBillCheck instance = SaleBillCheck.Instance;
            CheckResult checkResult = instance.CheckSaleBillBase(bill);
            string str3 = instance.ConvertToReportMsgForSplitBill(checkResult);
            if (checkResult.HasWrong)
            {
                if (str3.Contains("行，开票金额超出金税设备允许范围"))
                {
                    checkResult.NeedCF = true;
                }
                if (str3.Contains("组折扣商品") || str3.Contains("金额减税额除以税率的绝对值大于"))
                {
                    checkResult.NeedCF = false;
                }
            }
            if (SplitType == "MT")
            {
                if (checkResult.HasWrong && !checkResult.NeedCF)
                {
                    for (int i = 0; i < checkResult.listErrorMX.Count; i++)
                    {
                        if (checkResult.listErrorMX[i].Contains("非法税率"))
                        {
                        }
                    }
                    return ("该单据有错误,不能拆分\n" + str3);
                }
                return "Need";
            }
            if (checkResult.NeedCF)
            {
                return "Need";
            }
            if (checkResult.HasWrong)
            {
                return ("HasWrong" + str3);
            }
            string str = "Needless";
            if (bill.JEHJ >= invLimit)
            {
                return "Need";
            }
            if (instance.BillHasMultiSlv(bill))
            {
                return "Need";
            }
            if (ExEWMInfoSplit)
            {
                if (instance.IsOverEWM(bill, 0, bill.ListGoods.Count))
                {
                    return "Need";
                }
                FPGenerateResult result2 = new FPGenerateResult(bill) {
                    KPZL = "一般开票"
                };
                if (!ExEWMInfoSplit)
                {
                    InvSplitPara para = new InvSplitPara();
                    para.GetInvSplitPara(bill.DJZL);
                    if (bill.ListGoods.Count > 7)
                    {
                        if (para.above7Split)
                        {
                            result2.KPZL = "一般开票";
                            return "Need";
                        }
                        if (para.above7Generlist)
                        {
                            result2.KPZL = "清单开票";
                        }
                        if (para.above7ForbiddenInv)
                        {
                            return "Need";
                        }
                    }
                    else
                    {
                        if (para.below7Split)
                        {
                            result2.KPZL = "一般开票";
                        }
                        if (para.below7Generlist)
                        {
                            result2.KPZL = "清单开票";
                        }
                        if (para.below7ForbiddenInv)
                        {
                        }
                    }
                    if (bill.QDHSPMC.Trim().Length > 0)
                    {
                        result2.KPZL = "清单开票";
                    }
                }
                if (result2.KPZL == "清单开票")
                {
                    str = str + "清单开票";
                }
            }
            return str;
        }

        public void setDj(double dDj, int mxID, SaleBill bill)
        {
            Goods good = bill.GetGood(mxID);
            if (bill.ContainTax)
            {
                double num = dDj;
                double num2 = num * good.SL;
                good.HSJBZ = true;
                good.DJ = num;
                good.JE = Finacial.Div(num2, 1.0 + good.SLV, 2);
                good.SE = Finacial.GetRound((double) (num2 - good.JE), 2);
            }
            else
            {
                good.HSJBZ = false;
                good.DJ = dDj;
                good.JE = Finacial.GetRound((double) (dDj * good.SL), 2);
                good.SE = Finacial.GetRound((double) (good.JE * good.SLV), 2);
            }
        }

        private string SetNewBill(string SeparateReason, SaleBill bill, ref int group, List<Goods> listMXtmp, List<SaleBill> listBill, ref int i, bool IsKP)
        {
            SaleBill bill2 = new SaleBill {
                SeparateReason = SeparateReason
            };
            bill2.CopySaleBill(bill);
            if (IsKP)
            {
                bill2.BH = bill.BH;
            }
            else
            {
                bill2.BH = string.Format("{0}_{1}", bill.BH, (int) group);
            }
            if (listMXtmp.Count == 0)
            {
                throw new CustomException("此单据不需要拆分");
            }
            for (int j = 0; j < listMXtmp.Count; j++)
            {
                listMXtmp[j].XH = j + 1;
                bill2.ListGoods.Add(listMXtmp[j]);
            }
            int num2 = bill2.ListGoods.Count - 1;
            listMXtmp.Clear();
            this.CleanBill(bill2);
            listBill.Add(bill2);
            group++;
            return "0";
        }

        public string ShowSLV(SaleBill bill, string _xh, string _str = "0")
        {
            double num = Convert.ToDouble(_str);
            switch (num)
            {
                case 0.0:
                {
                    int num2 = 1;
                    if ((_xh != null) && (_xh != ""))
                    {
                        num2 = Convert.ToInt32(_xh);
                    }
                    int num3 = num2 - 1;
                    Goods goods = bill.ListGoods[num3];
                    if (goods.XSYH && (goods.LSLVBS == "1"))
                    {
                        return "免税";
                    }
                    if (goods.XSYH && (goods.LSLVBS == "2"))
                    {
                        return "不征税";
                    }
                    num *= 100.0;
                    return (num.ToString() + "%");
                }
                case 0.015:
                    return "减按1.5%计算";
            }
            num *= 100.0;
            return (num.ToString() + "%");
        }

        public string SplitDiscountGroup(SaleBill bill, List<int> ListSelectedRows)
        {
            int num;
            Goods goods;
            int num14;
            if (ListSelectedRows.Count == 0)
            {
                return "请选择折扣商品行";
            }
            List<Goods> list = new List<Goods>();
            for (num = 0; num < ListSelectedRows.Count; num++)
            {
                goods = bill.ListGoods[ListSelectedRows[num]];
                if (goods.DJHXZ != 3)
                {
                    if (ListSelectedRows.Count == 1)
                    {
                        return "请选择至少一行折扣商品";
                    }
                    return "不能包含折扣行";
                }
                list.Add(goods);
            }
            ListSelectedRows.Sort();
            bool flag = CommonTool.isSPBMVersion();
            if (flag)
            {
                int num2 = ListSelectedRows[0];
                int num3 = ListSelectedRows[ListSelectedRows.Count - 1];
                if ((num2 >= bill.ListGoods.Count) || (num3 >= bill.ListGoods.Count))
                {
                    return "拆分错误";
                }
                int num4 = 0;
                for (num = num2; num <= num3; num++)
                {
                    goods = bill.ListGoods[num];
                    if (goods.DJHXZ == 3)
                    {
                        num4++;
                    }
                    else if (goods.DJHXZ == 4)
                    {
                        num4 = 0;
                    }
                    if (num4 > 1)
                    {
                        return "只能拆分单行折扣";
                    }
                }
            }
            int id = ListSelectedRows[0];
            int num6 = this.SeekDiscountRow(bill, ListSelectedRows[0]);
            Goods goods2 = bill.ListGoods[num6];
            string sPMC = goods2.SPMC;
            double jE = goods2.JE;
            double sE = goods2.SE;
            int zKH = 0;
            double zkl = 0.0;
            int rows = 0;
            if (flag)
            {
                double je = this.CalZkhJE(bill, id, ref rows);
                zkl = CommonTool.GetDisCount(Math.Abs(jE), je, ref zKH);
            }
            else
            {
                zkl = CommonTool.GetDisCount(sPMC, ref zKH);
                rows = zKH;
            }
            if (ListSelectedRows.Count == rows)
            {
                return "只能选择部分折扣商品进行拆分";
            }
            Goods discountRow = new Goods();
            this.CheckDisCount(bill, id, ListSelectedRows.Count, zkl, discountRow, 0.0);
            double num13 = discountRow.JE;
            if (num13 == 0.0)
            {
                return "选中部分的折扣金额不能为0";
            }
            if (GetRound((double) (0.0 - Math.Abs((double) (jE - num13))), 2) == 0.0)
            {
                return "未选中部分的折扣金额不能为0";
            }
            for (num14 = 0; num14 < ListSelectedRows.Count; num14++)
            {
                bill.ListGoods.Remove(list[num14]);
            }
            for (num14 = 0; num14 < ListSelectedRows.Count; num14++)
            {
                bill.ListGoods.Add(list[num14]);
            }
            if (flag)
            {
                this.DisCount2(bill, bill.ListGoods.Count - 1, ListSelectedRows.Count, zkl, 0.0);
            }
            else
            {
                this.DisCount(bill, bill.ListGoods.Count - 1, ListSelectedRows.Count, zkl, 0.0, 0.0);
            }
            int zkhIndex = num6 - list.Count;
            this.ModifyDiscountRow(bill, zkhIndex - 1, zkhIndex, "discountOut", "", zkl);
            int num16 = bill.ListGoods.Count - 1;
            double num17 = bill.ListGoods[num16].JE;
            double num18 = bill.ListGoods[num16].SE;
            double round = GetRound((double) (0.0 - Math.Abs((double) (jE - num17))), 2);
            double num20 = GetRound((double) (0.0 - Math.Abs((double) (sE - num18))), 2);
            bill.ListGoods[zkhIndex].JE = round;
            bill.ListGoods[zkhIndex].SE = num20;
            return "0";
        }

        public string SplitGoodsMannual(SaleBill bill, int rowID, string field, object value, string discountSplit)
        {
            bool flag2;
            Goods goods2;
            Goods mx = bill.ListGoods[rowID];
            double result = 0.0;
            List<string> list = new List<string>();
            list.AddRange(new string[] { "JE", "SL", "SE" });
            if (list.Contains(field))
            {
                if (!this.TryTodouble(value, out result))
                {
                    return "非法输入值";
                }
            }
            else
            {
                return "请拆分金额，数量，税额，其他字段不能操作";
            }
            double num2 = 0.0;
            double num3 = 0.0;
            string str4 = field;
            if (str4 != null)
            {
                if (!(str4 == "JE"))
                {
                    if (str4 == "SL")
                    {
                        num2 = mx.SL;
                    }
                    else if (str4 == "SE")
                    {
                        num2 = mx.SE;
                        if (mx.HSJBZ)
                        {
                            if (bill.HYSY && (mx.SLV == 0.05))
                            {
                                if ((mx.SL != 0.0) && (mx.DJ != 0.0))
                                {
                                    num3 = (mx.SL * mx.DJ) * mx.SLV;
                                    num2 = num3 + 0.06;
                                }
                                else
                                {
                                    num3 = (mx.JE + mx.SE) * mx.SLV;
                                    num2 = num3 + 0.06;
                                }
                            }
                            else if (bill.JZ_50_15 && (mx.SLV == 0.015))
                            {
                                num3 = (mx.JE / 1.035) * 0.015;
                                num2 = num3 + 0.06;
                            }
                            else
                            {
                                num3 = mx.JE * mx.SLV;
                                num2 = num3 + 0.06;
                            }
                        }
                        else if (bill.JZ_50_15 && (mx.SLV == 0.015))
                        {
                            num3 = (mx.JE / 1.035) * 0.015;
                            num2 = num3 + 0.06;
                        }
                        else
                        {
                            num2 = (mx.JE * mx.SLV) + 0.06;
                            num3 = mx.JE * mx.SLV;
                        }
                    }
                }
                else
                {
                    num2 = mx.JE;
                }
            }
            if (field == "SE")
            {
                if ((Math.Abs(GetRound((double) (result - num3), 2)) > 0.06) || (result < 0.0))
                {
                    return "税额超过精度范围，请重新拆分";
                }
                mx.SE = GetRound(result, 2);
                return "0";
            }
            if ((result >= num2) || (result <= 0.0))
            {
                return "非法输入值";
            }
            double round = 0.0;
            if (field == "SL")
            {
                round = num2 - result;
            }
            else
            {
                round = GetRound((double) (num2 - result), 2);
            }
            if ((num2 == 0.0) || (round == 0.0))
            {
                return "不能拆分0值";
            }
            if ((field == "JE") && (((num2 < 0.01) || (result < 0.01)) || (round < 0.01)))
            {
                return "拆分金额不能小于0.01元，不进行拆分";
            }
            double num5 = 0.0;
            double num6 = 0.0;
            if (field == "SL")
            {
                if (bill.HYSY && (mx.SLV == 0.05))
                {
                    num5 = (mx.DJ * result) * 0.95;
                    num6 = mx.JE - num5;
                }
                else
                {
                    double num7;
                    if (bill.JZ_50_15 && (mx.SLV == 0.015))
                    {
                        if (mx.HSJBZ)
                        {
                            num7 = ((mx.DJ * result) / 1.05) * 0.015;
                            num5 = (mx.DJ * result) - num7;
                            num6 = mx.JE - num5;
                        }
                        else
                        {
                            num5 = mx.DJ * result;
                            num6 = mx.JE - num5;
                        }
                    }
                    else if (mx.HSJBZ)
                    {
                        num7 = (mx.DJ * result) / (1.0 + mx.SLV);
                        num5 = (mx.DJ * result) - num7;
                        num6 = mx.JE - num5;
                    }
                    else
                    {
                        num5 = mx.DJ * result;
                        num6 = mx.JE - num5;
                    }
                }
            }
            if ((field == "SL") && ((num5 < 0.01) || (num6 < 0.01)))
            {
                return "拆分数量不能导致金额小于0.01元，不进行拆分";
            }
            if ((num2 / 2.0) >= result)
            {
                flag2 = false;
            }
            else
            {
                flag2 = true;
            }
            double sE = mx.SE;
            double jE = mx.JE;
            double sL = mx.SL;
            if (mx.DJHXZ == 0)
            {
                this.EditGoodsSplit(bill, mx, field, result, sE, jE, sL, true);
                goods2 = Goods.Clone(mx);
                this.InsertGoods(bill, goods2, rowID, 0);
                this.EditGoodsSplit(bill, goods2, field, round, sE, jE, sL, false);
            }
            else if (mx.DJHXZ == 3)
            {
                int index = this.SeekDiscountRow(bill, rowID);
                if (!(discountSplit == "每行包含折扣"))
                {
                    double num23;
                    double num24;
                    string str2;
                    if (discountSplit == "大行包含折扣")
                    {
                        if (flag2)
                        {
                            num23 = result;
                            num24 = round;
                        }
                        else
                        {
                            num23 = round;
                            num24 = result;
                        }
                        this.EditGoodsSplit(bill, mx, field, num23, sE, jE, sL, true);
                        goods2 = Goods.Clone(mx);
                        goods2.DJHXZ = 0;
                        this.InsertGoods(bill, goods2, index, 0);
                        this.EditGoodsSplit(bill, goods2, field, num24, sE, jE, sL, false);
                        str2 = this.ModifyDiscountRow(bill, rowID, index, "discountHold", field, 0.0);
                        if (str2 != "0")
                        {
                            this.RemoveGoods(bill, index + 1);
                            this.EditGoodsSplit(bill, mx, field, num2, sE, jE, sL, true);
                        }
                        return str2;
                    }
                    if (discountSplit == "小行包含折扣")
                    {
                        if (flag2)
                        {
                            num23 = round;
                            num24 = result;
                        }
                        else
                        {
                            num23 = result;
                            num24 = round;
                        }
                        this.EditGoodsSplit(bill, mx, field, num23, sE, jE, sL, true);
                        goods2 = Goods.Clone(mx);
                        goods2.DJHXZ = 0;
                        this.InsertGoods(bill, goods2, index, 0);
                        this.EditGoodsSplit(bill, goods2, field, num24, sE, jE, sL, false);
                        str2 = this.ModifyDiscountRow(bill, rowID, index, "discountHold", field, 0.0);
                        if (str2 != "0")
                        {
                            this.RemoveGoods(bill, index + 1);
                            this.EditGoodsSplit(bill, mx, field, num2, sE, jE, sL, true);
                        }
                        return str2;
                    }
                }
                else
                {
                    int num14;
                    string sPMC = bill.ListGoods[index].SPMC;
                    int zKH = 0;
                    double zkl = 0.0;
                    bool flag3 = CommonTool.isSPBMVersion();
                    if (flag3)
                    {
                        num14 = index - 1;
                        if (num14 < 0)
                        {
                            return "拆分错误";
                        }
                        zkl = CommonTool.GetDisCount(Math.Abs(bill.ListGoods[index].JE), Math.Abs(bill.ListGoods[num14].JE), ref zKH);
                    }
                    else
                    {
                        zkl = CommonTool.GetDisCount(sPMC, ref zKH);
                    }
                    if (zkl == 0.0)
                    {
                        double num15 = 0.0;
                        num14 = index - 1;
                        int num16 = 0;
                        while (zKH > 0)
                        {
                            Goods goods3 = bill.ListGoods[num14 - num16];
                            if (mx.DJHXZ != 3)
                            {
                                break;
                            }
                            num15 += goods3.JE;
                            num16++;
                            zKH--;
                        }
                        zkl = Math.Abs(bill.ListGoods[index].JE) / num15;
                    }
                    double num17 = Math.Abs(bill.ListGoods[index].JE);
                    double num18 = 0.0;
                    double zkje = 0.0;
                    if (field == "JE")
                    {
                        num18 = Math.Abs(GetRound((double) (result * zkl), 2));
                        zkje = Math.Abs(GetRound((double) (num17 - num18), 2));
                    }
                    else if (field == "SL")
                    {
                        num18 = Math.Abs(GetRound((double) (num5 * zkl), 2));
                        zkje = Math.Abs(GetRound((double) (num17 - num18), 2));
                    }
                    if ((field == "JE") || (field == "SL"))
                    {
                        if (num18 < 0.01)
                        {
                            return "输入金额的折扣金额不能为0";
                        }
                        if (zkje < 0.01)
                        {
                            return "拆分出金额的折扣金额不能为0";
                        }
                    }
                    double num20 = bill.ListGoods[index].JE;
                    double num21 = bill.ListGoods[index].SE;
                    if (zkl == 0.0)
                    {
                        throw new CustomException("输入金额的折扣金额不能等于0");
                    }
                    this.EditGoodsSplit(bill, mx, field, result, sE, jE, sL, true);
                    goods2 = Goods.Clone(mx);
                    goods2.DJHXZ = 3;
                    this.InsertGoods(bill, goods2, index, 0);
                    this.EditGoodsSplit(bill, goods2, field, round, sE, jE, sL, false);
                    if (flag3)
                    {
                        this.DisCount2(bill, index + 1, 1, zkl, zkje);
                    }
                    else
                    {
                        this.DisCount(bill, index + 1, 1, zkl, zkje, 0.0);
                    }
                    this.ModifyDiscountRow(bill, rowID, index, "discountOut", field, zkl);
                    int num22 = index + 2;
                    bill.ListGoods[num22].JE = GetRound((double) (num20 - bill.ListGoods[index].JE), 2);
                    bill.ListGoods[num22].SE = GetRound((double) (num21 - bill.ListGoods[index].SE), 2);
                }
            }
            return "0";
        }

        private bool TryTodouble(object sldj, out double result)
        {
            try
            {
                result = 0.0;
                if (sldj != null)
                {
                    string s = sldj.ToString();
                    if (s.Contains("%"))
                    {
                        s = "0." + s.Replace("%", "");
                    }
                    return double.TryParse(s, out result);
                }
                return false;
            }
            catch
            {
                result = 0.0;
                return false;
            }
        }

        public void TurnContainTax(SaleBill bill)
        {
            if (bill.ContainTax)
            {
                bill.ContainTax = false;
            }
            else
            {
                bill.ContainTax = true;
            }
        }

        public string WasteSaleBill(SaleBill bill)
        {
            bool isRight = !SaleBillCheck.Instance.CheckSaleBillBase(bill).HasWrong;
            return this.dbBill.WasteSaleBill(bill.BH, isRight);
        }

        public string xtCheck(string spmc)
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            bool flag = false;
            if ((card.get_CorpAgent().Substring(0, 10) != "02C2511011") && (card.get_CorpAgent().Substring(9, 1) == "1"))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            if (flag)
            {
                object[] objArray = new object[] { spmc };
                return (string) ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMCheckXTSPByName", objArray)[0];
            }
            return "0";
        }

        public void ZFRecover(string fpdm, string fphm, string djbh)
        {
            SaleBill bill = this.Find(djbh);
            if (bill == null)
            {
                this.log.Debug("手工发票作废解锁单据失败，无法找到单据，单据编号" + djbh);
            }
            else if (bill.ListGoods.Count == 0)
            {
                this.log.Debug("手工发票作废解锁单据失败，单据无明细，单据编号" + djbh);
            }
            else
            {
                int num;
                for (num = 0; num < bill.ListGoods.Count; num++)
                {
                    string fPDM = bill.ListGoods[num].FPDM;
                    int fPHM = bill.ListGoods[num].FPHM;
                    if (((fPDM != null) && !(fPDM == "")) && (fPHM != 0))
                    {
                        string str2 = fPDM;
                        string str3 = fPHM.ToString();
                        if (fpdm.Equals(str2) && fphm.Equals(str3))
                        {
                            bill.ListGoods[num].FPDM = "";
                            bill.ListGoods[num].FPHM = 0;
                        }
                    }
                }
                bool flag = true;
                int num3 = 0;
                for (num = 0; num < bill.ListGoods.Count; num++)
                {
                    if ((bill.ListGoods[num].FPDM != null) && (bill.ListGoods[num].FPDM != ""))
                    {
                        flag = false;
                        num3++;
                    }
                    else if (bill.ListGoods[num].FPHM != 0)
                    {
                        flag = false;
                        num3++;
                    }
                }
                if (bill.DJZL == "j")
                {
                    bill.KPZT = "N";
                    bill.DJZT = "Y";
                }
                else if (bill.ListGoods.Count != num3)
                {
                    if (flag)
                    {
                        bill.KPZT = "N";
                        bill.DJZT = "Y";
                    }
                    else
                    {
                        bill.KPZT = "P";
                        bill.DJZT = "Y";
                    }
                }
                this.dbBill.SaveSaleBill(bill);
            }
        }

        public static SaleBillCtrl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SaleBillCtrl();
                }
                return instance;
            }
        }

        private class ComparerMXje : IComparer<Goods>
        {
            public int Compare(Goods A, Goods B)
            {
                return A.JE.CompareTo(B.JE);
            }
        }
    }
}

