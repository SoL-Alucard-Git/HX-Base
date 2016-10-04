namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Resolver : TaxCardValue
    {
        private bool bZKSE;
        private SaleBillCheck check = SaleBillCheck.Instance;
        private string DJZT;
        protected ErrorResolver errorResolver = new ErrorResolver();
        private InvType Fplx;
        private double FSoftTaxPre = 0.06;
        private bool hysy_x;
        protected string Importtype = "txt";
        private DJXGdal imsaveDAL = new DJXGdal();
        private PriceType Jesm;
        private bool JesmTemp;
        private double[] slvArray = readXMLSlv.ReadXMLSlv();

        private bool CalcZKJE_ZKSE(TempXSDJ_MX mx, double ZKJE, double ZKSE)
        {
            double num3 = Convert.ToDouble(mx.Slv);
            double num = Math.Abs(ZKJE);
            if (this.JesmTemp)
            {
                if (((num3 == 0.05) && this.hysy_x) && (this.Fplx == InvType.Special))
                {
                    if (!this.bZKSE)
                    {
                        ZKJE = SaleBillCtrl.GetRound((double) (num * 0.95), 2);
                        ZKSE = SaleBillCtrl.GetRound((double) (num - ZKJE), 2);
                        mx.Zkje = new double?(Convert.ToDouble(ZKJE));
                        mx.Zkse = Convert.ToString(ZKSE);
                        return true;
                    }
                    if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) (num * 0.05), 2) - ZKSE)), 2) <= this.FSoftTaxPre)
                    {
                        ZKJE = SaleBillCtrl.GetRound((double) (num - ZKSE), 2);
                        ZKSE = SaleBillCtrl.GetRound(ZKSE, 2);
                        mx.Zkje = new double?(Convert.ToDouble(ZKJE));
                        mx.Zkse = Convert.ToString(ZKSE);
                        return true;
                    }
                    mx.Zkje = new double?(Convert.ToDouble(ZKJE));
                    mx.Zkse = Convert.ToString(ZKSE);
                    return false;
                }
                if (this.bZKSE)
                {
                    if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) ((num - ZKSE) * num3), 2) - ZKSE)), 2) > this.FSoftTaxPre)
                    {
                        if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) (num * num3), 2) - ZKSE)), 2) > this.FSoftTaxPre)
                        {
                            ZKJE = SaleBillCtrl.GetRound((double) (num - ZKSE), 2);
                            mx.Zkje = new double?(Convert.ToDouble(ZKJE));
                            mx.Zkse = Convert.ToString(ZKSE);
                            return false;
                        }
                        this.JesmTemp = false;
                        ZKJE = SaleBillCtrl.GetRound(num, 2);
                    }
                    else
                    {
                        ZKJE = SaleBillCtrl.GetRound((double) (num - ZKSE), 2);
                    }
                }
                else
                {
                    ZKJE = SaleBillCtrl.GetRound((double) (num / (1.0 + num3)), 2);
                    ZKSE = SaleBillCtrl.GetRound((double) (num - ZKJE), 2);
                }
            }
            else
            {
                if (((num3 == 0.05) && this.hysy_x) && (this.Fplx == InvType.Special))
                {
                    if (!this.bZKSE)
                    {
                        ZKJE = SaleBillCtrl.GetRound(num, 2);
                        ZKSE = SaleBillCtrl.GetRound((double) (num / 19.0), 2);
                        mx.Zkje = new double?(Convert.ToDouble(ZKJE));
                        mx.Zkse = Convert.ToString(ZKSE);
                        return true;
                    }
                    if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) (num / 19.0), 2) - ZKSE)), 2) <= this.FSoftTaxPre)
                    {
                        ZKJE = SaleBillCtrl.GetRound(num, 2);
                        ZKSE = SaleBillCtrl.GetRound(ZKSE, 2);
                        mx.Zkje = new double?(Convert.ToDouble(ZKJE));
                        mx.Zkse = Convert.ToString(ZKSE);
                        return true;
                    }
                    mx.Zkje = new double?(Convert.ToDouble(ZKJE));
                    mx.Zkse = Convert.ToString(ZKSE);
                    return false;
                }
                if (!this.bZKSE)
                {
                    ZKSE = SaleBillCtrl.GetRound((double) (ZKJE * num3), 2);
                }
                else if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) (ZKJE * num3), 2) - ZKSE)), 2) > this.FSoftTaxPre)
                {
                    if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) ((num - ZKSE) * num3), 2) - ZKSE)), 2) > this.FSoftTaxPre)
                    {
                        mx.Zkje = new double?(Convert.ToDouble(ZKJE));
                        mx.Zkse = Convert.ToString(ZKSE);
                        return false;
                    }
                    this.JesmTemp = true;
                    ZKJE = SaleBillCtrl.GetRound((double) (num - ZKSE), 2);
                }
            }
            ZKJE = SaleBillCtrl.GetRound(ZKJE, 2);
            ZKSE = SaleBillCtrl.GetRound(ZKSE, 2);
            mx.Zkje = new double?(Convert.ToDouble(ZKJE));
            mx.Zkse = Convert.ToString(ZKSE);
            return true;
        }

        private bool CalcZKJE_ZKSE_x(TempXSDJ_MX mx, double ZKJE, double ZKSE)
        {
            double round;
            double num2 = Convert.ToDouble(mx.Slv);
            double num3 = Convert.ToDouble(mx.Bhsje);
            double num4 = Convert.ToDouble(mx.Zkl);
            double num6 = Convert.ToDouble(mx.Se);
            double num = Math.Abs(ZKJE);
            if (this.JesmTemp)
            {
                if (((num2 == 0.05) && this.hysy_x) && (this.Fplx == InvType.Special))
                {
                    round = SaleBillCtrl.GetRound((double) (num3 * num4), 2);
                    ZKJE = SaleBillCtrl.GetRound((double) (round * 0.95), 2);
                    if (SaleBillCtrl.GetRound(ZKSE, 8) == 0.0)
                    {
                        ZKSE = SaleBillCtrl.GetRound((double) (round - ZKJE), 2);
                    }
                    ZKJE = SaleBillCtrl.GetRound(ZKJE, 2);
                    ZKSE = SaleBillCtrl.GetRound(ZKSE, 2);
                    mx.Zkje = new double?(Convert.ToDouble(ZKJE));
                    mx.Zkse = Convert.ToString(ZKSE);
                    return true;
                }
                if (num3 == 0.0)
                {
                    ZKJE = SaleBillCtrl.GetRound((double) ((num6 / num2) * num4), 2);
                    if (SaleBillCtrl.GetRound(ZKSE, 8) == 0.0)
                    {
                        ZKSE = SaleBillCtrl.GetRound((double) (num6 * num4), 2);
                    }
                }
                else
                {
                    round = SaleBillCtrl.GetRound((double) (num3 * num4), 2);
                    ZKJE = SaleBillCtrl.GetRound((double) (round / (1.0 + num2)), 2);
                    if (SaleBillCtrl.GetRound(ZKSE, 8) == 0.0)
                    {
                        ZKSE = SaleBillCtrl.GetRound((double) (round - ZKJE), 2);
                    }
                }
            }
            else if (((num2 == 0.05) && this.hysy_x) && (this.Fplx == InvType.Special))
            {
                round = SaleBillCtrl.GetRound((double) (num3 * num4), 2);
                if (SaleBillCtrl.GetRound(ZKSE, 8) == 0.0)
                {
                    ZKSE = SaleBillCtrl.GetRound((double) (round / 19.0), 2);
                }
                ZKJE = SaleBillCtrl.GetRound(ZKJE, 2);
                ZKSE = SaleBillCtrl.GetRound(ZKSE, 2);
            }
            else
            {
                ZKJE = SaleBillCtrl.GetRound((double) (num3 * num4), 2);
                if (SaleBillCtrl.GetRound(ZKSE, 8) == 0.0)
                {
                    ZKSE = SaleBillCtrl.GetRound((double) (num6 * num4), 2);
                }
            }
            ZKJE = SaleBillCtrl.GetRound(ZKJE, 2);
            ZKSE = SaleBillCtrl.GetRound(ZKSE, 2);
            mx.Zkje = new double?(Convert.ToDouble(ZKJE));
            mx.Zkse = Convert.ToString(ZKSE);
            return true;
        }

        private List<XSDJMXModel> ConvertImportXSDJ(XSDJ XsdjImport)
        {
            List<XSDJMXModel> list = new List<XSDJMXModel>();
            for (int i = 0; i < XsdjImport.Dj.Count; i++)
            {
                XSDJMXModel item = new XSDJMXModel {
                    BH = XsdjImport.Dj[i].Djh,
                    GFMC = XsdjImport.Dj[i].Gfmc,
                    GFSH = XsdjImport.Dj[i].Gfsh,
                    GFDZDH = XsdjImport.Dj[i].Gfdzdh,
                    GFYHZH = XsdjImport.Dj[i].Gfyhzh,
                    XSBM = "",
                    YDXS = false,
                    DJRQ = XsdjImport.Dj[i].Djrq.Date,
                    DJYF = XsdjImport.Dj[i].Djrq.Month,
                    DJZT = XsdjImport.Dj[i].DJZT,
                    KPZT = "N",
                    BZ = XsdjImport.Dj[i].Bz,
                    FHR = XsdjImport.Dj[i].Fhr,
                    SKR = XsdjImport.Dj[i].Skr,
                    QDHSPMC = XsdjImport.Dj[i].Qdspmc,
                    XFYHZH = XsdjImport.Dj[i].Xfyhzh,
                    XFDZDH = XsdjImport.Dj[i].Xfdzdh,
                    CFHB = false,
                    DJZL = XsdjImport.Dj[i].Djzl,
                    SFZJY = false,
                    HYSY = XsdjImport.Dj[i].HYSY
                };
                double num2 = 0.0;
                for (int j = 0; j < XsdjImport.Dj[i].Mingxi.Count; j++)
                {
                    XSDJ_MXModel model2 = new XSDJ_MXModel {
                        XSDJBH = XsdjImport.Dj[i].Mingxi[j].DJBH,
                        XH = Convert.ToInt16((int) (j + 1)),
                        SL = XsdjImport.Dj[i].Mingxi[j].Sl,
                        DJ = XsdjImport.Dj[i].Mingxi[j].Dj,
                        JE = Convert.ToDouble(XsdjImport.Dj[i].Mingxi[j].Bhsje),
                        SLV = Convert.ToDouble(XsdjImport.Dj[i].Mingxi[j].Slv),
                        SE = Convert.ToDouble(XsdjImport.Dj[i].Mingxi[j].Se),
                        SPMC = XsdjImport.Dj[i].Mingxi[j].Hwmc,
                        SPSM = XsdjImport.Dj[i].Mingxi[j].Spsm,
                        GGXH = XsdjImport.Dj[i].Mingxi[j].Gg,
                        JLDW = XsdjImport.Dj[i].Mingxi[j].Jldw,
                        HSJBZ = (XsdjImport.Dj[i].Mingxi[j].Jgfs == "0") ? false : true,
                        DJHXZ = XsdjImport.Dj[i].Mingxi[j].Djhxz,
                        FPZL = XsdjImport.Dj[i].Mingxi[j].Fpzl.ToString(),
                        FPDM = "",
                        FPHM = 0,
                        SCFPXH = 0
                    };
                    num2 += Convert.ToDouble(XsdjImport.Dj[i].Mingxi[j].Bhsje);
                    item.ListXSDJ_MX.Add(model2);
                }
                item.JEHJ = num2;
                list.Add(item);
            }
            return list;
        }

        private void CreateZheKouHang(XSDJ xsdjt, int i, int j, double JE, double SE, double ZKL, int ZKHCount)
        {
            TempXSDJ_MX item = new TempXSDJ_MX {
                Hwmc = CommonTool.GetDisCountMC(ZKL, ZKHCount),
                Zkl = new double?(ZKL),
                Bhsje = new double?(-1.0 * JE),
                Se = new double?(-1.0 * SE),
                DJBH = xsdjt.Dj[i].Djh,
                Slv = xsdjt.Dj[i].Mingxi[j - 1].Slv,
                Jgfs = xsdjt.Dj[i].Mingxi[j - 1].Jgfs,
                Spsm = xsdjt.Dj[i].Mingxi[j - 1].Spsm,
                Fpzl = xsdjt.Dj[i].Mingxi[j - 1].Fpzl,
                Djhxz = 4,
                Sl = 0.0,
                Dj = 0.0
            };
            xsdjt.Dj[i].Mingxi.Insert(j, item);
        }

        private void ErrorAdd(string ErrowInfo, string DJBH, int LineNum, bool Accept)
        {
            this.errorResolver.AddError(ErrowInfo, DJBH, LineNum, Accept);
        }

        protected void FanSuan(XSDJ xsdj)
        {
            if (xsdj.Dj.Count == 0)
            {
                this.ErrorAdd("无被接受的销售单据", "无单据被接收", 0, true);
            }
            else
            {
                for (int i = 0; i < xsdj.Dj.Count; i++)
                {
                    xsdj.Dj[i].Djzl = CommonTool.GetInvTypeStr(this.Fplx);
                    int num2 = this.ValidHead(xsdj.Dj[i]);
                    if (num2 == 2)
                    {
                        xsdj.Dj.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        if (num2 == 1)
                        {
                            xsdj.Dj[i].DJZT = "N";
                        }
                        else
                        {
                            xsdj.Dj[i].DJZT = "Y";
                        }
                        this.hysy_x = xsdj.Dj[i].HYSY;
                        if (this.Jesm == PriceType.HanShui)
                        {
                            this.JesmTemp = true;
                        }
                        else
                        {
                            this.JesmTemp = false;
                        }
                        if (this.FanSuanMX(xsdj.Dj[i].Mingxi, xsdj.Dj[i].Djh))
                        {
                            xsdj.Dj[i].DJZT = this.DJZT;
                            xsdj.Dj.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            xsdj.Dj[i].DJZT = this.DJZT;
                        }
                    }
                }
            }
            this.HeBingZheKouHang(xsdj);
        }

        private void FanSuanDjJeSln(TempXSDJ_MX mx)
        {
            double num = Convert.ToDouble(mx.Sl);
            double num2 = Convert.ToDouble(mx.Dj);
            double num3 = Convert.ToDouble(mx.Bhsje);
            bool flag = true;
            if (mx.Jgfs == "0")
            {
                flag = false;
            }
            Dictionary<string, double> dictionary = new Dictionary<string, double>();
            if (mx.Bhsje != 0.0)
            {
                dictionary.Add("JE", num3);
            }
            if (!(mx.Dj == 0.0))
            {
                dictionary.Add("DJ", num2);
            }
            if (!(mx.Sl == 0.0))
            {
                dictionary.Add("SL", num);
            }
            if (dictionary.Count < 2)
            {
                if (!(!(this.Importtype == "Excel") || dictionary.ContainsKey("JE")))
                {
                    this.ErrorAdd("单价、数量和金额三者关系不符", mx.DJBH, 0, true);
                }
            }
            else
            {
                double round;
                if (((mx.Slv == 0.05) && this.hysy_x) && (this.Fplx == InvType.Special))
                {
                    if (!dictionary.ContainsKey("DJ"))
                    {
                        num2 = (num3 + Convert.ToDouble(mx.Se)) / num;
                    }
                    else if (flag)
                    {
                        if (mx.Jgfs == "1")
                        {
                            round = SaleBillCtrl.GetRound((double) (((num2 * num) - num3) - Convert.ToDouble(mx.Se)), 8);
                        }
                        else
                        {
                            round = SaleBillCtrl.GetRound((double) ((num2 * num) - num3), 8);
                            num2 = SaleBillCtrl.GetRound((double) (num2 / 0.95), 8);
                        }
                        if (Math.Abs(round) > this.FSoftTaxPre)
                        {
                            this.DJZT = "N";
                            mx.DanJiaError = true;
                            this.ErrorAdd("单价、数量和金额三者关系不符", mx.DJBH, mx.HangShu, true);
                        }
                    }
                    else if (Math.Abs(SaleBillCtrl.GetRound((double) ((num2 * num) - num3), 8)) <= this.FSoftTaxPre)
                    {
                        num2 = SaleBillCtrl.GetRound((double) (num2 / 0.95), 8);
                    }
                    else if (Math.Abs(SaleBillCtrl.GetRound((double) (((num2 * num) - num3) - Convert.ToDouble(mx.Se)), 8)) <= this.FSoftTaxPre)
                    {
                        mx.Jgfs = "1";
                    }
                    else
                    {
                        mx.Jgfs = CommonTool.ToStringBool(this.JesmTemp);
                        this.DJZT = "N";
                        mx.DanJiaError = true;
                        this.ErrorAdd("单价、数量和金额三者关系不符", mx.DJBH, mx.HangShu, true);
                    }
                    mx.Jgfs = "1";
                }
                else if (!dictionary.ContainsKey("DJ"))
                {
                    if (flag)
                    {
                        if (mx.Jgfs == "1")
                        {
                            num2 = (num3 + Convert.ToDouble(mx.Se)) / num;
                        }
                        else
                        {
                            num2 = num3 / num;
                        }
                    }
                    else
                    {
                        if (this.JesmTemp)
                        {
                            num2 = (num3 + Convert.ToDouble(mx.Se)) / num;
                        }
                        else
                        {
                            num2 = num3 / num;
                        }
                        mx.Jgfs = CommonTool.ToStringBool(this.JesmTemp);
                    }
                }
                else if (flag)
                {
                    if (mx.Jgfs == "1")
                    {
                        round = SaleBillCtrl.GetRound((double) (((num2 * num) - num3) - Convert.ToDouble(mx.Se)), 8);
                    }
                    else
                    {
                        round = SaleBillCtrl.GetRound((double) ((num2 * num) - num3), 8);
                    }
                    if (Math.Abs(round) > this.FSoftTaxPre)
                    {
                        this.DJZT = "N";
                        mx.DanJiaError = true;
                        this.ErrorAdd("单价、数量和金额三者关系不符", mx.DJBH, mx.HangShu, true);
                    }
                }
                else if (Math.Abs(SaleBillCtrl.GetRound((double) ((num2 * num) - num3), 8)) <= this.FSoftTaxPre)
                {
                    mx.Jgfs = "0";
                }
                else if (Math.Abs(SaleBillCtrl.GetRound((double) (((num2 * num) - num3) - Convert.ToDouble(mx.Se)), 8)) <= this.FSoftTaxPre)
                {
                    mx.Jgfs = "1";
                }
                else
                {
                    mx.Jgfs = CommonTool.ToStringBool(this.JesmTemp);
                    this.DJZT = "N";
                    mx.DanJiaError = true;
                    this.ErrorAdd("单价、数量和金额三者关系不符", mx.DJBH, mx.HangShu, true);
                }
                mx.Dj = num2;
            }
        }

        private bool FanSuanJeSeSlvn(TempXSDJ_MX mx)
        {
            double round;
            double num2;
            double num3;
            double num6 = 0.0;
            bool flag = true;
            bool flag2 = true;
            bool flag3 = true;
            bool jesmTemp = this.JesmTemp;
            Dictionary<string, double> dictionary = new Dictionary<string, double>();
            if (!mx.Bhsje.HasValue)
            {
                flag = false;
                round = 0.0;
            }
            else
            {
                round = Convert.ToDouble(mx.Bhsje);
            }
            if (!mx.Slv.HasValue)
            {
                flag2 = false;
                num2 = 0.0;
            }
            else
            {
                num2 = Convert.ToDouble(mx.Slv);
            }
            if (!mx.Se.HasValue)
            {
                flag3 = false;
                num3 = 0.0;
            }
            else
            {
                num3 = Convert.ToDouble(mx.Se);
            }
            if (flag)
            {
                dictionary.Add("JE", round);
            }
            if (flag3)
            {
                dictionary.Add("SE", num3);
            }
            if (flag2)
            {
                dictionary.Add("SLV", num2);
            }
            if (dictionary.Count == 0)
            {
                if ((flag && flag2) && flag3)
                {
                    this.ErrorAdd("金额、税率和税额均为0", mx.DJBH, mx.HangShu, false);
                    return false;
                }
                if (flag2 && flag3)
                {
                    this.ErrorAdd("没有金额、且税率和税额均为0", mx.DJBH, mx.HangShu, false);
                    return false;
                }
                if (flag && flag2)
                {
                    this.ErrorAdd("没有税额、且税率和金额均为0", mx.DJBH, mx.HangShu, false);
                    return false;
                }
                if (flag && flag3)
                {
                    this.ErrorAdd("没有税率、且税额和金额均为0", mx.DJBH, mx.HangShu, false);
                    return false;
                }
                this.ErrorAdd("没有金额、税率和税额", mx.DJBH, mx.HangShu, false);
                return false;
            }
            if (dictionary.Count < 2)
            {
                if (dictionary.ContainsKey("JE"))
                {
                    this.ErrorAdd("只有金额", mx.DJBH, mx.HangShu, false);
                }
                else if (dictionary.ContainsKey("SE"))
                {
                    if (!(flag2 || flag))
                    {
                        this.ErrorAdd("只有税额", mx.DJBH, mx.HangShu, false);
                    }
                    else if (!(!flag2 || flag))
                    {
                        this.ErrorAdd("没有金额且税率为0", mx.DJBH, mx.HangShu, false);
                    }
                }
                else if (dictionary.ContainsKey("SLV"))
                {
                    this.ErrorAdd("只有税率", mx.DJBH, mx.HangShu, false);
                }
                return false;
            }
            if ((round * num3) < 0.0)
            {
                this.ErrorAdd("金额和税额不同号", mx.DJBH, mx.HangShu, false);
                return false;
            }
            if (dictionary.Count != 2)
            {
                if (dictionary.Count == 3)
                {
                    if (this.JesmTemp)
                    {
                        if (((num2 == 0.05) && this.hysy_x) && (this.Fplx == InvType.Special))
                        {
                            round = SaleBillCtrl.GetRound((double) (round - num3), 2);
                            if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) (round * 0.05), 2) - num3)), 2) <= this.FSoftTaxPre)
                            {
                                num3 = SaleBillCtrl.GetRound(num3, 2);
                            }
                            else
                            {
                                this.ErrorAdd("税额有错误", mx.DJBH, mx.HangShu, true);
                            }
                        }
                        else if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) ((round - num3) * num2), 2) - num3)), 2) > this.FSoftTaxPre)
                        {
                            if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) (round * num2), 2) - num3)), 2) > this.FSoftTaxPre)
                            {
                                this.DJZT = "N";
                                this.ErrorAdd("税额有错误", mx.DJBH, mx.HangShu, true);
                                round -= num3;
                                this.JesmTemp = jesmTemp;
                            }
                            else
                            {
                                this.JesmTemp = false;
                                mx.Jgfs = "0";
                            }
                        }
                        else
                        {
                            round -= num3;
                        }
                    }
                    else if (((num2 == 0.05) && this.hysy_x) && (this.Fplx == InvType.Special))
                    {
                        if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) ((round + num3) * 0.05), 2) - num3)), 2) <= this.FSoftTaxPre)
                        {
                            num3 = SaleBillCtrl.GetRound(num3, 2);
                        }
                        else
                        {
                            this.ErrorAdd("税额有错误", mx.DJBH, mx.HangShu, true);
                        }
                    }
                    else if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) (round * num2), 2) - num3)), 2) > this.FSoftTaxPre)
                    {
                        if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) ((round - num3) * num2), 2) - num3)), 2) > this.FSoftTaxPre)
                        {
                            this.DJZT = "N";
                            this.ErrorAdd("税额有错误", mx.DJBH, mx.HangShu, true);
                            this.JesmTemp = jesmTemp;
                        }
                        else
                        {
                            round -= num3;
                            this.JesmTemp = true;
                            mx.Jgfs = "1";
                        }
                    }
                }
            }
            else
            {
                num2 = SaleBillCtrl.GetRound(num2, 2);
                if (this.JesmTemp)
                {
                    if (((num2 == 0.05) && this.hysy_x) && (this.Fplx == InvType.Special))
                    {
                        if (!dictionary.ContainsKey("JE"))
                        {
                            round = SaleBillCtrl.GetRound((double) (19.0 * num3), 2);
                        }
                        else
                        {
                            num3 = SaleBillCtrl.GetRound((double) (round * 0.05), 2);
                            round = SaleBillCtrl.GetRound((double) (round - num3), 2);
                        }
                    }
                    else if (!dictionary.ContainsKey("JE"))
                    {
                        round = SaleBillCtrl.GetRound((double) (num3 / num2), 2);
                    }
                    else if (!dictionary.ContainsKey("SLV"))
                    {
                        round = SaleBillCtrl.GetRound((double) (round - num3), 2);
                    }
                    else if (!dictionary.ContainsKey("SE"))
                    {
                        num3 = SaleBillCtrl.GetRound((double) ((round * num2) / (1.0 + num2)), 2);
                        round = SaleBillCtrl.GetRound((double) (round - num3), 2);
                    }
                }
                else if (((num2 == 0.05) && this.hysy_x) && (this.Fplx == InvType.Special))
                {
                    if (!dictionary.ContainsKey("JE"))
                    {
                        round = SaleBillCtrl.GetRound((double) (19.0 * num3), 2);
                    }
                    else
                    {
                        num3 = SaleBillCtrl.GetRound((double) (round / 19.0), 2);
                    }
                }
                else if (!dictionary.ContainsKey("JE"))
                {
                    round = SaleBillCtrl.GetRound((double) (num3 / num2), 2);
                }
                else if (dictionary.ContainsKey("SLV"))
                {
                    if (!dictionary.ContainsKey("SE"))
                    {
                        num3 = SaleBillCtrl.GetRound((double) (round * num2), 2);
                    }
                }
                else
                {
                    List<double> list = new List<double>(this.slvArray);
                    list.Remove(0.05);
                    if (list.Count > 0)
                    {
                        int num7 = 0;
                        for (int i = 0; i < list.Count; i++)
                        {
                            num6 = Convert.ToDouble(list[i]);
                            if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) (round * num6), 2) - num3)), 2) <= this.FSoftTaxPre)
                            {
                                this.JesmTemp = false;
                                num7++;
                                break;
                            }
                            if (SaleBillCtrl.GetRound(Math.Abs((double) (SaleBillCtrl.GetRound((double) ((round - num3) * num6), 2) - num3)), 2) <= this.FSoftTaxPre)
                            {
                                this.JesmTemp = true;
                                num7++;
                                break;
                            }
                        }
                        if (num7 == 1)
                        {
                            num2 = num6;
                        }
                        else
                        {
                            this.JesmTemp = jesmTemp;
                            this.ErrorAdd("税率有错误", mx.DJBH, mx.HangShu, true);
                        }
                    }
                }
            }
            if (this.Fplx == InvType.Special)
            {
                if (!base.slvList.Contains(Convert.ToDouble(num2)))
                {
                    this.ErrorAdd("税率有错误", mx.DJBH, mx.HangShu, true);
                }
                else if (!((num2 != 0.05) || this.hysy_x))
                {
                    this.ErrorAdd("税率有错误", mx.DJBH, mx.HangShu, true);
                }
            }
            else
            {
                int num10;
                int num9 = Convert.ToInt16((double) (num2 * 100.0));
                if (TaxCardValue.taxCard.get_ECardType() == 0)
                {
                    num10 = 0x1f;
                }
                else
                {
                    num10 = 0x19;
                }
                if (num9 > num10)
                {
                    this.ErrorAdd("税率有错误", mx.DJBH, mx.HangShu, true);
                }
            }
            mx.Bhsje = new double?(SaleBillCtrl.GetRound(round, 2));
            mx.Se = new double?(SaleBillCtrl.GetRound(num3, 2));
            mx.Slv = new double?(num2);
            this.JesmTemp = jesmTemp;
            return true;
        }

        private bool FanSuanMX(List<TempXSDJ_MX> ListMx, string DJBH)
        {
            bool flag = false;
            this.DJZT = "Y";
            foreach (TempXSDJ_MX pxsdj_mx in ListMx)
            {
                pxsdj_mx.DanJiaError = false;
                double sl = pxsdj_mx.Sl;
                if (!pxsdj_mx.Zkje.HasValue)
                {
                    flag = true;
                    this.ErrorAdd("折扣金额有错误", DJBH, pxsdj_mx.HangShu, false);
                }
                else if (!pxsdj_mx.Zkl.HasValue)
                {
                    flag = true;
                    this.ErrorAdd("折扣率有错误", DJBH, pxsdj_mx.HangShu, false);
                }
                else if (string.IsNullOrEmpty(pxsdj_mx.Hwmc))
                {
                    flag = true;
                    this.ErrorAdd("没有货物名称", DJBH, pxsdj_mx.HangShu, false);
                }
                else if (!this.FanSuanJeSeSlvn(pxsdj_mx))
                {
                    flag = true;
                }
                else
                {
                    this.FanSuanDjJeSln(pxsdj_mx);
                    if (!this.FanSuanZheKou(pxsdj_mx))
                    {
                        flag = true;
                    }
                    else
                    {
                        pxsdj_mx.Fpzl = CommonTool.GetInvTypeStr(this.Fplx);
                    }
                }
            }
            if (flag)
            {
                this.errorResolver.AbandonCount++;
            }
            return flag;
        }

        private bool FanSuanZheKou(TempXSDJ_MX mx)
        {
            double zKJE = Convert.ToDouble(mx.Zkje);
            double num2 = Convert.ToDouble(mx.Zkl);
            double result = 0.0;
            double num4 = Convert.ToDouble(mx.Bhsje);
            double num5 = Convert.ToDouble(mx.Se);
            double num8 = Convert.ToDouble(mx.Slv);
            if (mx.Zkse == "ImportNull")
            {
                result = 0.0;
                this.bZKSE = false;
            }
            else
            {
                if (mx.Zkse == "ImportError")
                {
                    this.ErrorAdd("折扣税额有错误", mx.DJBH, mx.HangShu, false);
                    return false;
                }
                this.bZKSE = true;
                double.TryParse(mx.Zkse, out result);
            }
            if (((zKJE == 0.0) && (num2 == 0.0)) && (result == 0.0))
            {
                mx.Djhxz = 0;
                return true;
            }
            if ((zKJE != 0.0) && (num2 != 0.0))
            {
                if (this.JesmTemp)
                {
                    if ((zKJE * (num4 + num5)) > 0.0)
                    {
                        if (SaleBillCtrl.GetRound(Math.Abs((double) (((num4 + num5) * num2) - zKJE)), 2) > this.FSoftTaxPre)
                        {
                            this.ErrorAdd("折扣率有错误", mx.DJBH, mx.HangShu, false);
                            return false;
                        }
                    }
                    else if (SaleBillCtrl.GetRound(Math.Abs((double) (((num4 + num5) * num2) + zKJE)), 2) > this.FSoftTaxPre)
                    {
                        this.ErrorAdd("折扣率有错误", mx.DJBH, mx.HangShu, false);
                        return false;
                    }
                }
                else if ((zKJE * num4) > 0.0)
                {
                    if (SaleBillCtrl.GetRound(Math.Abs((double) ((num4 * num2) - zKJE)), 2) > this.FSoftTaxPre)
                    {
                        this.ErrorAdd("折扣率有错误", mx.DJBH, mx.HangShu, false);
                        return false;
                    }
                }
                else if (SaleBillCtrl.GetRound(Math.Abs((double) ((num4 * num2) + zKJE)), 2) > this.FSoftTaxPre)
                {
                    this.ErrorAdd("折扣率有错误", mx.DJBH, mx.HangShu, false);
                    return false;
                }
            }
            if ((zKJE * result) < 0.0)
            {
                this.ErrorAdd("折扣金额和折扣税额不同号", mx.DJBH, mx.HangShu, false);
                return false;
            }
            if (zKJE != 0.0)
            {
                if (!this.CalcZKJE_ZKSE(mx, zKJE, result))
                {
                    this.ErrorAdd("折扣税额错误", mx.DJBH, mx.HangShu, false);
                    return false;
                }
                zKJE = Convert.ToDouble(mx.Zkje);
                double.TryParse(mx.Zkse, out result);
                if (num4 == 0.0)
                {
                    num2 = 0.0;
                }
                else
                {
                    double round;
                    if (this.JesmTemp)
                    {
                        round = SaleBillCtrl.GetRound((double) ((zKJE + result) / (num4 + num5)), 5);
                    }
                    else
                    {
                        round = SaleBillCtrl.GetRound((double) (zKJE / num4), 5);
                    }
                    round = Math.Abs(round);
                    if (num2 == 0.0)
                    {
                        num2 = round;
                    }
                    if (num2 > 100.0000001)
                    {
                        this.ErrorAdd("折扣率错误", mx.DJBH, mx.HangShu, false);
                        return false;
                    }
                    if (Math.Abs((double) (SaleBillCtrl.GetRound(num2, 5) - round)) > base.DataPrecision)
                    {
                        double num6;
                        if (this.JesmTemp)
                        {
                            num6 = Math.Abs((double) (Math.Abs((double) (zKJE + result)) - Math.Abs(SaleBillCtrl.GetRound((double) (num2 * (num4 + num5)), 2))));
                        }
                        else
                        {
                            num6 = Math.Abs((double) (Math.Abs(zKJE) - Math.Abs(SaleBillCtrl.GetRound((double) (num2 * num4), 2))));
                        }
                        if (num6 > base.DataPrecision)
                        {
                            this.ErrorAdd("折扣率错误", mx.DJBH, mx.HangShu, false);
                            return false;
                        }
                    }
                }
            }
            else if (num2 != 0.0)
            {
                if (result != 0.0)
                {
                    if (num2 < 0.0)
                    {
                        this.ErrorAdd("折扣率错误", mx.DJBH, mx.HangShu, false);
                        return false;
                    }
                    if (SaleBillCtrl.GetRound((double) ((num5 * num2) - Math.Abs(result)), 2) > this.FSoftTaxPre)
                    {
                        this.ErrorAdd("折扣税额错误", mx.DJBH, mx.HangShu, false);
                        return false;
                    }
                }
                this.CalcZKJE_ZKSE_x(mx, zKJE, result);
                zKJE = Convert.ToDouble(mx.Zkje);
                double.TryParse(mx.Zkse, out result);
            }
            else if (result != 0.0)
            {
                if ((result * num5) > 0.0)
                {
                    num2 = SaleBillCtrl.GetRound((double) (result / num5), 5);
                }
                else if (num5 == 0.0)
                {
                    num2 = 0.0;
                }
                else
                {
                    num2 = -SaleBillCtrl.GetRound((double) (result / num5), 5);
                }
                if (num4 >= 0.0)
                {
                    zKJE = SaleBillCtrl.GetRound((double) (num2 * num4), 2);
                }
                else
                {
                    zKJE = -SaleBillCtrl.GetRound((double) (num2 * num4), 2);
                }
            }
            zKJE = Math.Abs(zKJE);
            result = Math.Abs(result);
            if (num4 >= 0.0)
            {
                if (zKJE > num4)
                {
                    this.ErrorAdd("折扣金额大于金额", mx.DJBH, mx.HangShu, false);
                    return false;
                }
            }
            else
            {
                if ((result != 0.0) && (zKJE == 0.0))
                {
                    zKJE = SaleBillCtrl.GetRound((double) (result / num8), 2);
                }
                if (zKJE > -num4)
                {
                    this.ErrorAdd("折扣金额大于金额", mx.DJBH, mx.HangShu, false);
                    return false;
                }
            }
            if (SaleBillCtrl.GetRound((double) ((zKJE * num8) - result), 2) > this.FSoftTaxPre)
            {
                this.ErrorAdd("折扣税额错误", mx.DJBH, mx.HangShu, false);
                return false;
            }
            mx.Zkje = new double?(zKJE);
            mx.Zkl = new double?(num2);
            mx.Zkse = Convert.ToString(result);
            mx.Djhxz = (num2 == 0.0) ? ((short) 0) : ((short) 3);
            return true;
        }

        public ErrorResolver GetError()
        {
            return this.errorResolver;
        }

        internal void HeBingZheKouHang(XSDJ xsdjt)
        {
            for (int i = 0; i < xsdjt.Dj.Count; i++)
            {
                int zKHCount = 1;
                bool flag = false;
                double jE = 0.0;
                double sE = 0.0;
                double zKL = 0.0;
                double round = 0.0;
                double num7 = 0.0;
                double num8 = 0.0;
                for (int j = 0; j < (xsdjt.Dj[i].Mingxi.Count + 1); j++)
                {
                    if (round < 0.0)
                    {
                        round = SaleBillCtrl.GetRound(Convert.ToDouble(xsdjt.Dj[i].Mingxi[j].Bhsje), 2);
                        num7 = SaleBillCtrl.GetRound(Convert.ToDouble(xsdjt.Dj[i].Mingxi[j].Se), 2);
                        num8 = Convert.ToDouble(xsdjt.Dj[i].Mingxi[j].Sl);
                        jE = SaleBillCtrl.GetRound(Convert.ToDouble(xsdjt.Dj[i].Mingxi[j].Zkje), 2);
                        sE = SaleBillCtrl.GetRound(Convert.ToDouble(xsdjt.Dj[i].Mingxi[j].Zkse), 2);
                        if (flag)
                        {
                            this.CreateZheKouHang(xsdjt, i, j, jE, sE, zKL, zKHCount);
                        }
                        flag = false;
                        round = SaleBillCtrl.GetRound((double) (round + Math.Abs(jE)), 2);
                        num7 = SaleBillCtrl.GetRound((double) (num7 + Math.Abs(sE)), 2);
                        if ((Math.Abs(num8) > base.DataPrecision) && !xsdjt.Dj[i].Mingxi[j].DanJiaError)
                        {
                            if (((Convert.ToDouble(xsdjt.Dj[i].Mingxi[j].Slv) == 0.05) && (this.Fplx == InvType.Special)) && xsdjt.Dj[i].HYSY)
                            {
                                xsdjt.Dj[i].Mingxi[j].Dj = SaleBillCtrl.GetRound((double) ((round + num7) / num8), 2);
                            }
                            else if (xsdjt.Dj[i].Mingxi[j].Jgfs == "1")
                            {
                                xsdjt.Dj[i].Mingxi[j].Dj = SaleBillCtrl.GetRound((double) ((round + num7) / num8), 2);
                            }
                            else
                            {
                                xsdjt.Dj[i].Mingxi[j].Dj = SaleBillCtrl.GetRound((double) (round / num8), 2);
                            }
                        }
                        xsdjt.Dj[i].Mingxi[j].Bhsje = new double?(round);
                        xsdjt.Dj[i].Mingxi[j].Se = new double?(num7);
                        jE = 0.0;
                        sE = 0.0;
                        zKL = 0.0;
                    }
                    else
                    {
                        if (j == xsdjt.Dj[i].Mingxi.Count)
                        {
                            if (flag)
                            {
                                this.CreateZheKouHang(xsdjt, i, j, jE, sE, zKL, zKHCount);
                            }
                            break;
                        }
                        if (xsdjt.Dj[i].Mingxi[j].Zkje != 0.0)
                        {
                            if ((j > 0) && flag)
                            {
                                bool flag2 = xsdjt.Dj[i].Mingxi[j].Zkl == xsdjt.Dj[i].Mingxi[j - 1].Zkl;
                                bool flag3 = xsdjt.Dj[i].Mingxi[j].Slv == xsdjt.Dj[i].Mingxi[j - 1].Slv;
                                if (flag2 && flag3)
                                {
                                    jE += Convert.ToDouble(xsdjt.Dj[i].Mingxi[j].Zkje);
                                    sE += Convert.ToDouble(xsdjt.Dj[i].Mingxi[j].Zkse);
                                    zKHCount++;
                                    goto Label_062A;
                                }
                                this.CreateZheKouHang(xsdjt, i, j, jE, sE, zKL, zKHCount);
                                j++;
                                jE = 0.0;
                                sE = 0.0;
                                zKL = 0.0;
                                zKHCount = 1;
                            }
                            zKL = Convert.ToDouble(xsdjt.Dj[i].Mingxi[j].Zkl);
                            jE = Convert.ToDouble(xsdjt.Dj[i].Mingxi[j].Zkje);
                            sE = Convert.ToDouble(xsdjt.Dj[i].Mingxi[j].Zkse);
                            flag = true;
                        }
                        else if (flag)
                        {
                            this.CreateZheKouHang(xsdjt, i, j, jE, sE, zKL, zKHCount);
                            j++;
                            jE = 0.0;
                            sE = 0.0;
                            zKL = 0.0;
                            flag = false;
                        }
                    Label_062A:;
                    }
                }
            }
        }

        public void SaveImport(XSDJ xsdj)
        {
            try
            {
                MessageHelper.MsgWait("正在导入单据，请稍候...");
                int count = xsdj.Dj.Count;
                this.FanSuan(xsdj);
                ErrorResolver errorResolver = this.errorResolver;
                XSDJ xsdjImport = xsdj;
                List<XSDJMXModel> listXSDJandMX = this.ConvertImportXSDJ(xsdjImport);
                this.imsaveDAL.SaveImportXSDJ(listXSDJandMX, errorResolver);
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
            finally
            {
                Thread.Sleep(100);
                MessageHelper.MsgWait();
            }
        }

        protected int ValidHead(TempXSDJ XSDJtemp)
        {
            if (string.IsNullOrEmpty(XSDJtemp.Djh))
            {
                this.ErrorAdd("没有单据号", "", 0, false);
                this.errorResolver.AbandonCount++;
                return 2;
            }
            if (string.IsNullOrEmpty(XSDJtemp.Gfmc))
            {
                this.ErrorAdd("没有购方名称", XSDJtemp.Djh, 0, true);
                return 1;
            }
            if ((this.Fplx == InvType.Special) && string.IsNullOrEmpty(XSDJtemp.Gfsh))
            {
                this.ErrorAdd("没有税号", XSDJtemp.Djh, 0, true);
                return 1;
            }
            FPLX fplx = 2;
            if (this.Fplx == InvType.Common)
            {
                fplx = 2;
            }
            else if (this.Fplx == InvType.Special)
            {
                fplx = 0;
            }
            else if (this.Fplx == InvType.transportation)
            {
                fplx = 11;
            }
            else if (this.Fplx == InvType.vehiclesales)
            {
                fplx = 12;
            }
            bool sFZJY = XSDJtemp.SFZJY == "0";
            if (!this.check.CheckTaxCode(this.Fplx, XSDJtemp.Gfsh, sFZJY, fplx))
            {
                this.ErrorAdd("税号有错误", XSDJtemp.Djh, 0, true);
                return 1;
            }
            return 0;
        }

        protected InvType FaPiaomode
        {
            get
            {
                return this.Fplx;
            }
            set
            {
                this.Fplx = value;
            }
        }

        protected PriceType Pricemode
        {
            get
            {
                return this.Jesm;
            }
            set
            {
                this.Jesm = value;
            }
        }
    }
}

