namespace Aisino.Fwkp.Wbjk.Model
{
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SaleBill
    {
        private string _jdc_clbm;
        private string _jdc_flbm;
        private string _jdc_flmc;
        private string _jdc_lslvbs;
        private bool _jdc_xsyh;
        private string _jdc_xsyhsm;
        private bool _jz_50_15;
        private double _slv;
        private string bh;
        private string bz;
        private string cm;
        public bool ContainTax;
        public string DJZL;
        public string DJZT;
        private string dw;
        private string fhr;
        private string gfdzdh;
        private string gfmc;
        private string gfsh;
        private string gfyhzh;
        private string hy_bh;
        private string hy_bz;
        private string hy_chdw;
        private string hy_czch;
        private string hy_djrq;
        private string hy_fhr;
        private string hy_fhrmc;
        private string hy_fhrsh;
        private string hy_qymd;
        private string hy_shrmc;
        private string hy_shrsh;
        private string hy_skr;
        private string hy_spfmc;
        private string hy_spfsh;
        private string hy_yshwxx;
        private string jdc_bh;
        private string jdc_bz;
        private string jdc_cd;
        private string jdc_clsbh;
        private string jdc_cpxh;
        private string jdc_dh;
        private string jdc_dw;
        private string jdc_dz;
        private string jdc_fdjh;
        private string jdc_ghdw;
        private string jdc_hgzh;
        private string jdc_jkzmsh;
        private string jdc_khyh;
        private string jdc_lx;
        private string jdc_sccjmc;
        private string jdc_sfz;
        private string jdc_sjdh;
        private string jdc_xcrs;
        private string jdc_zh;
        private string khyhmc;
        private string khyhzh;
        public string KPZT;
        public List<Goods> ListGoods;
        private string mdd;
        private string qdhspmc;
        private string qyd;
        public string ReserveA;
        private string sccjmc;
        private string skr;
        private string tydh;
        private string xfdh;
        private string xfdz;
        private string xfdzdh;
        private string xfyhzh;
        private string xhd;
        private string xsbm;
        private string yshwxx;
        private string zhd;

        public SaleBill()
        {
            this.bh = "";
            this.gfmc = "";
            this.gfsh = "";
            this.gfdzdh = "";
            this.gfyhzh = "";
            this.xsbm = "";
            this.DJZT = "";
            this.KPZT = "";
            this.bz = "";
            this.fhr = "";
            this.skr = "";
            this.qdhspmc = "";
            this.xfyhzh = "";
            this.xfdzdh = "";
            this.DJZL = "";
            this._jz_50_15 = false;
            this.cm = "";
            this.khyhzh = "";
            this.khyhmc = "";
            this.tydh = "";
            this.qyd = "";
            this.zhd = "";
            this.xhd = "";
            this.mdd = "";
            this.xfdz = "";
            this.xfdh = "";
            this.yshwxx = "";
            this.sccjmc = "";
            this._slv = 0.0;
            this.dw = "";
            this.ListGoods = new List<Goods>();
            this.jdc_bh = "";
            this.jdc_ghdw = "";
            this.jdc_sfz = "";
            this.jdc_lx = "";
            this.jdc_cpxh = "";
            this.jdc_cd = "";
            this.jdc_sccjmc = "";
            this.jdc_hgzh = "";
            this.jdc_jkzmsh = "";
            this.jdc_sjdh = "";
            this.jdc_fdjh = "";
            this.jdc_clsbh = "";
            this.jdc_dh = "";
            this.jdc_zh = "";
            this.jdc_dz = "";
            this.jdc_khyh = "";
            this.jdc_dw = "";
            this.jdc_xcrs = "";
            this.jdc_bz = "";
            this._jdc_flbm = "";
            this._jdc_flmc = "";
            this._jdc_xsyh = false;
            this._jdc_xsyhsm = "";
            this._jdc_clbm = "";
            this._jdc_lslvbs = "";
            this.hy_bh = "";
            this.hy_spfmc = "";
            this.hy_spfsh = "";
            this.hy_shrmc = "";
            this.hy_shrsh = "";
            this.hy_fhrmc = "";
            this.hy_fhrsh = "";
            this.hy_qymd = "";
            this.hy_czch = "";
            this.hy_chdw = "";
            this.hy_yshwxx = "";
            this.hy_bz = "";
            this.hy_fhr = "";
            this.hy_skr = "";
            this.hy_djrq = "";
            this.ContainTax = false;
            this.ReserveA = "";
            this.DJZL = "c";
            this.KPZT = "N";
            this.DJZT = "Y";
            this.IsANew = true;
        }

        public SaleBill(SaleBill bill)
        {
            this.bh = "";
            this.gfmc = "";
            this.gfsh = "";
            this.gfdzdh = "";
            this.gfyhzh = "";
            this.xsbm = "";
            this.DJZT = "";
            this.KPZT = "";
            this.bz = "";
            this.fhr = "";
            this.skr = "";
            this.qdhspmc = "";
            this.xfyhzh = "";
            this.xfdzdh = "";
            this.DJZL = "";
            this._jz_50_15 = false;
            this.cm = "";
            this.khyhzh = "";
            this.khyhmc = "";
            this.tydh = "";
            this.qyd = "";
            this.zhd = "";
            this.xhd = "";
            this.mdd = "";
            this.xfdz = "";
            this.xfdh = "";
            this.yshwxx = "";
            this.sccjmc = "";
            this._slv = 0.0;
            this.dw = "";
            this.ListGoods = new List<Goods>();
            this.jdc_bh = "";
            this.jdc_ghdw = "";
            this.jdc_sfz = "";
            this.jdc_lx = "";
            this.jdc_cpxh = "";
            this.jdc_cd = "";
            this.jdc_sccjmc = "";
            this.jdc_hgzh = "";
            this.jdc_jkzmsh = "";
            this.jdc_sjdh = "";
            this.jdc_fdjh = "";
            this.jdc_clsbh = "";
            this.jdc_dh = "";
            this.jdc_zh = "";
            this.jdc_dz = "";
            this.jdc_khyh = "";
            this.jdc_dw = "";
            this.jdc_xcrs = "";
            this.jdc_bz = "";
            this._jdc_flbm = "";
            this._jdc_flmc = "";
            this._jdc_xsyh = false;
            this._jdc_xsyhsm = "";
            this._jdc_clbm = "";
            this._jdc_lslvbs = "";
            this.hy_bh = "";
            this.hy_spfmc = "";
            this.hy_spfsh = "";
            this.hy_shrmc = "";
            this.hy_shrsh = "";
            this.hy_fhrmc = "";
            this.hy_fhrsh = "";
            this.hy_qymd = "";
            this.hy_czch = "";
            this.hy_chdw = "";
            this.hy_yshwxx = "";
            this.hy_bz = "";
            this.hy_fhr = "";
            this.hy_skr = "";
            this.hy_djrq = "";
            this.ContainTax = false;
            this.ReserveA = "";
            this.BH = bill.BH;
            this.GFMC = bill.GFMC;
            this.GFSH = bill.GFSH;
            this.GFDZDH = bill.GFDZDH;
            this.GFYHZH = bill.GFYHZH;
            this.XSBM = bill.XSBM;
            this.YDXS = bill.YDXS;
            this.JEHJ = bill.JEHJ;
            this.DJRQ = bill.DJRQ;
            this.DJYF = bill.DJYF;
            this.DJZT = bill.DJZT;
            this.KPZT = bill.KPZT;
            this.BZ = bill.BZ;
            this.FHR = bill.FHR;
            this.SKR = bill.SKR;
            this.QDHSPMC = bill.QDHSPMC;
            this.XFYHZH = bill.XFYHZH;
            this.XFDZDH = bill.XFDZDH;
            this.CFHB = bill.CFHB;
            this.DJZL = bill.DJZL;
            this.SFZJY = bill.SFZJY;
            this.HYSY = bill.HYSY;
            this.CM = bill.CM;
            this.DLGRQ = bill.DLGRQ;
            this.KHYHMC = bill.KHYHMC;
            this.KHYHZH = bill.KHYHZH;
            this.TYDH = bill.TYDH;
            this.QYD = bill.QYD;
            this.ZHD = bill.ZHD;
            this.XHD = bill.XHD;
            this.MDD = bill.MDD;
            this.XFDZ = bill.XFDZ;
            this.XFDH = bill.XFDH;
            this.YSHWXX = bill.YSHWXX;
            this.SCCJMC = bill.SCCJMC;
            this.SLV = bill.SLV;
            this.DW = bill.DW;
            this.JZ_50_15 = bill.JZ_50_15;
            this.JDC_BH = bill.JDC_BH;
            this.JDC_GHDW = bill.JDC_GHDW;
            this.JDC_SFZ = bill.JDC_SFZ;
            this.JDC_JE = bill.JDC_JE;
            this.JDC_SL = bill.JDC_SL;
            this.JDC_LX = bill.JDC_LX;
            this.JDC_CPXH = bill.JDC_CPXH;
            this.JDC_CD = bill.JDC_CD;
            this.JDC_SCCJMC = bill.JDC_SCCJMC;
            this.JDC_HGZH = bill.JDC_HGZH;
            this.JDC_JKZMSH = bill.JDC_JKZMSH;
            this.JDC_SJDH = bill.JDC_SJDH;
            this.JDC_FDJH = bill.JDC_FDJH;
            this.JDC_CLSBH = bill.JDC_CLSBH;
            this.JDC_DH = bill.JDC_DH;
            this.JDC_ZH = bill.JDC_ZH;
            this.JDC_DZ = bill.JDC_DZ;
            this.JDC_KHYH = bill.JDC_KHYH;
            this.JDC_DW = bill.JDC_DW;
            this.JDC_XCRS = bill.JDC_XCRS;
            this.JDC_DJRQ = bill.JDC_DJRQ;
            this.JDC_BZ = bill.JDC_BZ;
            this.HY_BH = bill.HY_BH;
            this.HY_SPFMC = bill.HY_SPFMC;
            this.HY_SPFSH = bill.HY_SPFSH;
            this.HY_SHRMC = bill.HY_SHRMC;
            this.HY_SHRSH = bill.HY_SHRSH;
            this.HY_FHRMC = bill.HY_FHRSH;
            this.HY_FHRSH = bill.HY_FHRSH;
            this.HY_SL = bill.HY_SL;
            this.HY_QYMD = bill.HY_QYMD;
            this.HY_CZCH = bill.HY_CZCH;
            this.HY_CHDW = bill.HY_CHDW;
            this.HY_YSHWXX = bill.HY_YSHWXX;
            this.HY_BZ = bill.HY_BZ;
            this.HY_FHR = bill.HY_FHR;
            this.HY_SKR = bill.HY_SKR;
            this.HY_DJRQ = bill.HY_DJRQ;
        }

        public string chedjjingdu(double tmp_DJ, double tmp_JE, double tmp_SE)
        {
            string str6;
            string str = tmp_SE.ToString();
            if ((tmp_JE.ToString().Length > Aisino.Fwkp.Wbjk.PARAMS.JE_MAX_LEN) || (str.Length > Aisino.Fwkp.Wbjk.PARAMS.SE_MAX_LEN))
            {
            }
            string s = tmp_DJ.ToString();
            if (SaleBillCtrl.Instance.chdbdecimal(tmp_JE) == "e1")
            {
                return "A123";
            }
            if (SaleBillCtrl.Instance.chdbdecimal(tmp_SE) == "e1")
            {
                return "A123";
            }
            decimal result = 0M;
            if (Finacial.Equal(tmp_JE, 0.0) || Finacial.Equal(tmp_SE, 0.0))
            {
                if (!((s.Length <= 0) || decimal.TryParse(s, out result)))
                {
                }
                if (result.ToString().Length > Aisino.Fwkp.Wbjk.PARAMS.DJ_MAX_LEN)
                {
                    return "DJex1";
                }
            }
            double num2 = Convert.ToDouble(result);
            if ("e1" == SaleBillCtrl.Instance.chdbdecimal(tmp_DJ))
            {
                return "DJex1";
            }
            string str8 = SaleBillCtrl.GetSubDecimal(decimal.Round(decimal.Parse(s), Aisino.Fwkp.Wbjk.PARAMS.DJ_MAX_DECIMALS, MidpointRounding.AwayFromZero).ToString(), Aisino.Fwkp.Wbjk.PARAMS.DJ_MAX_LEN, false);
            if (s != str8)
            {
                str6 = str8;
            }
            else
            {
                return "0";
            }
            return str6;
        }

        public void CopySaleBill(SaleBill bill)
        {
            this.BH = bill.BH;
            this.GFMC = bill.GFMC;
            this.GFSH = bill.GFSH;
            this.GFDZDH = bill.GFDZDH;
            this.GFYHZH = bill.GFYHZH;
            this.XSBM = bill.XSBM;
            this.YDXS = bill.YDXS;
            this.JEHJ = bill.JEHJ;
            this.DJRQ = bill.DJRQ;
            this.DJYF = bill.DJYF;
            this.DJZT = bill.DJZT;
            this.KPZT = bill.KPZT;
            this.BZ = bill.BZ;
            this.FHR = bill.FHR;
            this.SKR = bill.SKR;
            this.QDHSPMC = bill.QDHSPMC;
            this.XFYHZH = bill.XFYHZH;
            this.XFDZDH = bill.XFDZDH;
            this.CFHB = bill.CFHB;
            this.DJZL = bill.DJZL;
            this.SFZJY = bill.SFZJY;
            this.HYSY = bill.HYSY;
            this.CM = bill.CM;
            this.DLGRQ = bill.DLGRQ;
            this.KHYHMC = bill.KHYHMC;
            this.KHYHZH = bill.KHYHZH;
            this.TYDH = bill.TYDH;
            this.QYD = bill.QYD;
            this.ZHD = bill.ZHD;
            this.XHD = bill.XHD;
            this.MDD = bill.MDD;
            this.XFDZ = bill.XFDZ;
            this.XFDH = bill.XFDH;
            this.YSHWXX = bill.YSHWXX;
            this.SCCJMC = bill.SCCJMC;
            this.SLV = bill.SLV;
            this.DW = bill.DW;
            this.JZ_50_15 = bill.JZ_50_15;
            this.JDC_BH = bill.JDC_BH;
            this.JDC_GHDW = bill.JDC_GHDW;
            this.JDC_SFZ = bill.JDC_SFZ;
            this.JDC_JE = bill.JDC_JE;
            this.JDC_SL = bill.JDC_SL;
            this.JDC_LX = bill.JDC_LX;
            this.JDC_CPXH = bill.JDC_CPXH;
            this.JDC_CD = bill.JDC_CD;
            this.JDC_SCCJMC = bill.JDC_SCCJMC;
            this.JDC_HGZH = bill.JDC_HGZH;
            this.JDC_JKZMSH = bill.JDC_JKZMSH;
            this.JDC_SJDH = bill.JDC_SJDH;
            this.JDC_FDJH = bill.JDC_FDJH;
            this.JDC_CLSBH = bill.JDC_CLSBH;
            this.JDC_DH = bill.JDC_DH;
            this.JDC_ZH = bill.JDC_ZH;
            this.JDC_DZ = bill.JDC_DZ;
            this.JDC_KHYH = bill.JDC_KHYH;
            this.JDC_DW = bill.JDC_DW;
            this.JDC_XCRS = bill.JDC_XCRS;
            this.JDC_DJRQ = bill.JDC_DJRQ;
            this.JDC_BZ = bill.JDC_BZ;
            this.HY_BH = bill.HY_BH;
            this.HY_SPFMC = bill.HY_SPFMC;
            this.HY_SPFSH = bill.HY_SPFSH;
            this.HY_SHRMC = bill.HY_SHRMC;
            this.HY_SHRSH = bill.HY_SHRSH;
            this.HY_FHRMC = bill.HY_FHRSH;
            this.HY_FHRSH = bill.HY_FHRSH;
            this.HY_SL = bill.HY_SL;
            this.HY_QYMD = bill.HY_QYMD;
            this.HY_CZCH = bill.HY_CZCH;
            this.HY_CHDW = bill.HY_CHDW;
            this.HY_YSHWXX = bill.HY_YSHWXX;
            this.HY_BZ = bill.HY_BZ;
            this.HY_FHR = bill.HY_FHR;
            this.HY_SKR = bill.HY_SKR;
            this.HY_DJRQ = bill.HY_DJRQ;
        }

        public string getDj(int mxID)
        {
            Goods good = this.GetGood(mxID);
            if (this.getIsOcienBill() && (good.SLV == 0.05))
            {
                good.strDj = good.DJ.ToString();
            }
            else
            {
                double num = good.getDj(this.ContainTax);
                good.strDj = Convert.ToDecimal(num).ToString();
            }
            return good.strDj;
        }

        private double getDouble(string strValue)
        {
            try
            {
                double result = 0.0;
                if ((strValue != null) && double.TryParse(strValue, out result))
                {
                    return result;
                }
                return 0.0;
            }
            catch
            {
                return 0.0;
            }
        }

        public Goods GetGood(int mxID)
        {
            if (mxID >= this.ListGoods.Count)
            {
                return null;
            }
            return this.ListGoods[mxID];
        }

        public bool getIsOcienBill()
        {
            return ((this.DJZL == "s") && this.HYSY);
        }

        public string getJe(int mxID)
        {
            Goods good = this.GetGood(mxID);
            if (this.ContainTax)
            {
                good.strJe = Convert.ToDecimal((double) (good.SE + good.JE)).ToString("0.00");
            }
            else
            {
                good.strJe = Convert.ToDecimal(good.JE).ToString("0.00");
            }
            return good.strJe;
        }

        public string getSe(int mxID)
        {
            Goods good = this.GetGood(mxID);
            good.strSe = Convert.ToDecimal(good.SE).ToString("0.00");
            return good.strSe;
        }

        public string getSl(int mxID)
        {
            Goods good = this.GetGood(mxID);
            good.strSl = Convert.ToDecimal(good.SL).ToString();
            return good.strSl;
        }

        private double getSlvDouble(string strValue)
        {
            try
            {
                double result = 0.0;
                if (strValue == null)
                {
                    return 0.0;
                }
                if (strValue.EndsWith("%"))
                {
                    strValue = "0." + strValue.Replace("%", "");
                }
                if (double.TryParse(strValue, out result))
                {
                }
                double num2 = result;
                if ((num2 <= 0.0) || (num2 >= 1.0))
                {
                    if ((num2 >= 1.0) && (num2 < 100.0))
                    {
                        num2 = Finacial.Div(num2, 100.0, 15);
                    }
                    else
                    {
                        num2 = 0.0;
                    }
                }
                return num2;
            }
            catch
            {
                return 0.0;
            }
        }

        public string setDj(int mxID, string sDj)
        {
            double num7;
            double num = this.getDouble(sDj);
            Goods good = this.GetGood(mxID);
            double jE = good.JE;
            double sE = good.SE;
            double sL = good.SL;
            double dJ = good.DJ;
            double sLV = good.SLV;
            bool hSJBZ = good.HSJBZ;
            string str = this.getDj(mxID);
            if (sDj == str)
            {
                return "0";
            }
            if (this.getIsOcienBill() && (sLV == 0.05))
            {
                dJ = num;
                hSJBZ = true;
                if (!Finacial.Equal(sL, 0.0))
                {
                    jE = Finacial.GetRound((double) (((dJ * 19.0) / 20.0) * sL), 2);
                    sE = Finacial.GetRound((double) (jE / 19.0), 2);
                }
                else
                {
                    num7 = Finacial.Add(jE, sE, 15);
                    if (!(dJ == 0.0))
                    {
                        sL = Finacial.Div(num7, dJ, 15);
                    }
                    else
                    {
                        sL = 0.0;
                    }
                }
            }
            else if (Math.Abs((double) (good.SLV - 0.015)) < 1E-05)
            {
                if (num < 0.0)
                {
                    num = -1.0 * num;
                }
                if (this.ContainTax)
                {
                    if (good.HSJBZ)
                    {
                        dJ = num;
                        if (sL == 0.0)
                        {
                            sL = Finacial.Div(jE + sE, dJ, 15);
                        }
                        else
                        {
                            sE = ((sL * num) * 0.015) / 1.05;
                            jE = (sL * num) - sE;
                        }
                    }
                    else
                    {
                        dJ = num;
                        hSJBZ = true;
                        if (!(sL == 0.0))
                        {
                            sE = ((sL * num) * 0.015) / 1.05;
                            jE = (sL * num) - sE;
                        }
                        else
                        {
                            sL = Finacial.Div(sL * num, dJ, 15);
                        }
                    }
                }
                else if (good.HSJBZ)
                {
                    if (!(sL == 0.0))
                    {
                        jE = sL * num;
                        sE = jE / 69.0;
                    }
                    else
                    {
                        sL = Finacial.Div(jE, num, 15);
                    }
                    dJ = (num * 70.0) / 69.0;
                }
                else
                {
                    dJ = num;
                    if (!(sL == 0.0))
                    {
                        jE = sL * num;
                        sE = jE / 69.0;
                    }
                    else
                    {
                        sL = Finacial.Div(jE, num, 15);
                    }
                }
                good.JE = jE;
                good.DJ = dJ;
                good.SE = sE;
                good.SL = sL;
                good.SLV = sLV;
            }
            else
            {
                double num9;
                if (good.KCE != 0.0)
                {
                    if (num < 0.0)
                    {
                        num = -1.0 * num;
                    }
                    if (!Finacial.Equal(sL, 0.0))
                    {
                        if (this.ContainTax)
                        {
                            double num8 = num;
                            num7 = Finacial.Mul(num8, sL, 2);
                            if (num7 < 0.0)
                            {
                                good.KCE = -1.0 * Math.Abs(good.KCE);
                                if (good.KCE < num7)
                                {
                                    good.KCE = 0.0;
                                }
                            }
                            else
                            {
                                good.KCE = Math.Abs(good.KCE);
                                if (num7 < good.KCE)
                                {
                                    good.KCE = 0.0;
                                }
                            }
                            hSJBZ = true;
                            dJ = num8;
                            jE = Finacial.Div(num7 - good.KCE, 1.0 + sLV, 2) + good.KCE;
                            if (!((dJ * sL) == 0.0))
                            {
                                sE = Finacial.GetRound((double) ((dJ * sL) - jE), 2);
                            }
                            else
                            {
                                sE = Finacial.GetRound((double) ((jE - good.KCE) * sLV), 2);
                            }
                        }
                        else
                        {
                            hSJBZ = false;
                            dJ = num;
                            jE = Finacial.Mul(num, sL, 2);
                            if (jE < 0.0)
                            {
                                good.KCE = -1.0 * Math.Abs(good.KCE);
                                if (good.KCE < jE)
                                {
                                    good.KCE = 0.0;
                                }
                            }
                            else
                            {
                                good.KCE = Math.Abs(good.KCE);
                                if (jE < good.KCE)
                                {
                                    good.KCE = 0.0;
                                }
                            }
                            sE = Finacial.Mul(jE - good.KCE, sLV, 2);
                        }
                    }
                    else if (this.ContainTax)
                    {
                        num9 = num;
                        num7 = Finacial.Add(jE, sE, 15);
                        hSJBZ = true;
                        dJ = num9;
                        sL = Finacial.Div(num7, dJ, 15);
                    }
                    else
                    {
                        hSJBZ = false;
                        dJ = num;
                        sL = Finacial.Div(jE, dJ, 15);
                    }
                }
                else if (!Finacial.Equal(sL, 0.0))
                {
                    if (this.ContainTax)
                    {
                        num9 = num;
                        num7 = Finacial.Mul(num9, sL, 2);
                        hSJBZ = true;
                        dJ = num9;
                        jE = Finacial.Div(num7, 1.0 + sLV, 2);
                        sE = Finacial.GetRound((double) (num7 - jE), 2);
                    }
                    else
                    {
                        hSJBZ = false;
                        dJ = num;
                        jE = Finacial.Mul(num, sL, 2);
                        sE = Finacial.Mul(jE, sLV, 2);
                    }
                }
                else if (this.ContainTax)
                {
                    num9 = num;
                    num7 = Finacial.Add(jE, sE, 15);
                    hSJBZ = true;
                    dJ = num9;
                    sL = Finacial.Div(num7, dJ, 15);
                }
                else
                {
                    hSJBZ = false;
                    dJ = num;
                    sL = Finacial.Div(jE, dJ, 15);
                }
            }
            string str2 = "0";
            if ("0" == str2)
            {
                good.JE = jE;
                good.SE = sE;
                good.SL = sL;
                good.DJ = dJ;
                good.SLV = sLV;
                good.HSJBZ = hSJBZ;
                return "0";
            }
            return str2;
        }

        public string setJe(int mxID, string sJe)
        {
            double round = Finacial.GetRound(this.getDouble(sJe), 2);
            Goods good = this.GetGood(mxID);
            double jE = good.JE;
            double sE = good.SE;
            double sL = good.SL;
            double dJ = good.DJ;
            double sLV = good.SLV;
            bool hSJBZ = good.HSJBZ;
            if (!this.ContainTax)
            {
                if (Finacial.Equal(round, jE))
                {
                    return "0";
                }
            }
            else if (Finacial.Equal(round, jE + sE))
            {
                return "0";
            }
            if (this.getIsOcienBill() && (good.SLV == 0.05))
            {
                jE = round;
                double num7 = Finacial.Div(jE * sLV, 0.95, 15);
                sE = Finacial.GetRound(num7, 2);
                if (!Finacial.Equal(sL, 0.0))
                {
                    dJ = Finacial.Div(num7 + jE, sL, 15);
                }
            }
            else if (Math.Abs((double) (good.SLV - 0.015)) < 1E-05)
            {
                if (this.ContainTax)
                {
                    jE = (round * 69.0) / 70.0;
                    if (good.HSJBZ)
                    {
                        if (!(good.DJ == 0.0))
                        {
                            sL = Finacial.Div(round, good.DJ, 15);
                        }
                        else if ((dJ == 0.0) && (sL != 0.0))
                        {
                            dJ = Finacial.Div(round, sL, 15);
                        }
                    }
                    else if (!(good.DJ == 0.0))
                    {
                        sL = Finacial.Div(jE, good.DJ, 15);
                    }
                    else if ((dJ == 0.0) && (sL != 0.0))
                    {
                        dJ = Finacial.Div(jE, sL, 15);
                    }
                    sE = Finacial.GetRound((double) (round - jE), 2);
                }
                else
                {
                    jE = round;
                    if (good.HSJBZ)
                    {
                        if (!(good.DJ == 0.0))
                        {
                            sL = Finacial.Div((round * 70.0) / 69.0, good.DJ, 15);
                        }
                        else if ((dJ == 0.0) && (sL != 0.0))
                        {
                            dJ = Finacial.Div((round * 70.0) / 69.0, sL, 15);
                        }
                    }
                    else if (!(good.DJ == 0.0))
                    {
                        sL = Finacial.Div(round, good.DJ, 15);
                    }
                    else if ((dJ == 0.0) && (sL != 0.0))
                    {
                        dJ = Finacial.Div(jE, sL, 15);
                    }
                    sE = round / 69.0;
                }
                good.SE = sE;
                good.SL = sL;
                good.JE = jE;
                good.DJ = dJ;
                good.SLV = sLV;
            }
            else
            {
                double num8;
                if (good.KCE != 0.0)
                {
                    if (!this.ContainTax)
                    {
                        jE = round;
                        num8 = good.getDj(false);
                        if (jE > 0.0)
                        {
                            if (sL > 0.0)
                            {
                                hSJBZ = false;
                                dJ = Finacial.Div(jE, sL, 15);
                            }
                            else if (sL < 0.0)
                            {
                                sL = Finacial.Div(jE, num8, 15);
                                hSJBZ = false;
                                dJ = Finacial.Div(jE, sL, 15);
                            }
                            else if (!Finacial.Equal(dJ, 0.0))
                            {
                                sL = Finacial.Div(jE, num8, 15);
                            }
                        }
                        else if (!Finacial.Equal(num8, 0.0))
                        {
                            sL = Finacial.Div(jE, num8, 15);
                        }
                        if (jE < 0.0)
                        {
                            good.KCE = -1.0 * Math.Abs(good.KCE);
                            if (good.KCE < jE)
                            {
                                good.KCE = 0.0;
                            }
                        }
                        else
                        {
                            good.KCE = Math.Abs(good.KCE);
                            if (jE < good.KCE)
                            {
                                good.KCE = 0.0;
                            }
                        }
                        sE = Finacial.GetRound((double) ((jE - good.KCE) * sLV), 2);
                    }
                    else
                    {
                        if (round < 0.0)
                        {
                            good.KCE = -1.0 * Math.Abs(good.KCE);
                            if (good.KCE < round)
                            {
                                good.KCE = 0.0;
                            }
                        }
                        else
                        {
                            good.KCE = Math.Abs(good.KCE);
                            if (round < good.KCE)
                            {
                                good.KCE = 0.0;
                            }
                        }
                        jE = Finacial.Div(round - good.KCE, 1.0 + sLV, 2) + good.KCE;
                        num8 = good.getDj(true);
                        if (jE > 0.0)
                        {
                            if (sL > 0.0)
                            {
                                hSJBZ = true;
                                dJ = Finacial.Div(round, sL, 15);
                            }
                            else if (sL < 0.0)
                            {
                                sL = Finacial.Div(round, num8, 15);
                                hSJBZ = true;
                                dJ = Finacial.Div(round, sL, 15);
                            }
                            else if (!Finacial.Equal(dJ, 0.0))
                            {
                                sL = Finacial.Div(round, num8, 15);
                            }
                        }
                        else if (!Finacial.Equal(num8, 0.0))
                        {
                            sL = Finacial.Div(round, num8, 15);
                        }
                        sE = Finacial.GetRound((double) (round - jE), 2);
                    }
                }
                else if (!this.ContainTax)
                {
                    jE = round;
                    num8 = good.getDj(false);
                    if (jE > 0.0)
                    {
                        if (sL > 0.0)
                        {
                            hSJBZ = false;
                            dJ = Finacial.Div(jE, sL, 15);
                        }
                        else if (sL < 0.0)
                        {
                            sL = Finacial.Div(jE, num8, 15);
                            hSJBZ = false;
                            dJ = Finacial.Div(jE, sL, 15);
                        }
                        else if (!Finacial.Equal(dJ, 0.0))
                        {
                            sL = Finacial.Div(jE, num8, 15);
                        }
                    }
                    else if (!Finacial.Equal(num8, 0.0))
                    {
                        sL = Finacial.Div(jE, num8, 15);
                    }
                    sE = Finacial.GetRound((double) (jE * sLV), 2);
                }
                else
                {
                    jE = Finacial.Div(round, 1.0 + sLV, 2);
                    num8 = good.getDj(true);
                    if (jE > 0.0)
                    {
                        if (sL > 0.0)
                        {
                            hSJBZ = true;
                            dJ = Finacial.Div(round, sL, 15);
                        }
                        else if (sL < 0.0)
                        {
                            sL = Finacial.Div(round, num8, 15);
                            hSJBZ = true;
                            dJ = Finacial.Div(round, sL, 15);
                        }
                        else if (!Finacial.Equal(dJ, 0.0))
                        {
                            sL = Finacial.Div(round, num8, 15);
                        }
                    }
                    else if (!Finacial.Equal(num8, 0.0))
                    {
                        sL = Finacial.Div(round, num8, 15);
                    }
                    sE = Finacial.GetRound((double) (round - jE), 2);
                }
            }
            good.JE = jE;
            good.SE = sE;
            good.SL = sL;
            good.DJ = dJ;
            good.SLV = sLV;
            good.HSJBZ = hSJBZ;
            return "0";
        }

        public string setKCE(int mxID, string kce)
        {
            double round = Finacial.GetRound(this.getDouble(kce), 2);
            Goods good = this.GetGood(mxID);
            if (round != good.KCE)
            {
                if (this.HYSY && (Math.Abs((double) (good.SLV - 0.05)) < 1E-05))
                {
                    good.KCE = 0.0;
                    this.setSlv(mxID, good.SLV.ToString());
                    return "0";
                }
                if (Math.Abs((double) (good.SLV - 0.015)) < 1E-05)
                {
                    good.KCE = 0.0;
                    this.setSlv(mxID, good.SLV.ToString());
                    return "0";
                }
                good.KCE = Finacial.GetRound(round, 2);
                if (good.JE < 0.0)
                {
                    good.KCE = -1.0 * Math.Abs(good.KCE);
                    if (this.ContainTax)
                    {
                        if (good.KCE < (good.JE + good.SE))
                        {
                            good.KCE = 0.0;
                        }
                    }
                    else if (good.KCE < good.JE)
                    {
                        good.KCE = 0.0;
                    }
                }
                else
                {
                    good.KCE = Math.Abs(good.KCE);
                    if (this.ContainTax)
                    {
                        if ((good.JE + good.SE) < good.KCE)
                        {
                            good.KCE = 0.0;
                        }
                    }
                    else if (good.JE < good.KCE)
                    {
                        good.KCE = 0.0;
                    }
                }
                if (this.ContainTax)
                {
                    double num2 = good.JE + good.SE;
                    good.JE = Finacial.Div(num2 - good.KCE, 1.0 + good.SLV, 15) + good.KCE;
                    good.SE = num2 - good.JE;
                }
                else
                {
                    good.SE = (good.JE - good.KCE) * good.SLV;
                }
                good.SE = (good.JE - good.KCE) * good.SLV;
                if (!good.HSJBZ)
                {
                    if (!(good.SL == 0.0))
                    {
                        good.DJ = Finacial.Div(good.JE, good.SL, 15);
                    }
                    else
                    {
                        good.DJ = 0.0;
                    }
                }
                else if (!(good.SL == 0.0))
                {
                    good.DJ = Finacial.Div(good.JE + good.SE, good.SL, 15);
                }
                else
                {
                    good.DJ = 0.0;
                }
            }
            return "0";
        }

        public string setSe(int mxID, string sSe)
        {
            double round = Finacial.GetRound(this.getDouble(sSe), 2);
            this.GetGood(mxID).SE = Finacial.GetRound(round, 2);
            return "0";
        }

        public string setSl(int mxID, string sSl)
        {
            double num = this.getDouble(sSl);
            Goods good = this.GetGood(mxID);
            double jE = good.JE;
            double sE = good.SE;
            double sL = good.SL;
            double dJ = good.DJ;
            double sLV = good.SLV;
            bool hSJBZ = good.HSJBZ;
            if (num == sL)
            {
                return "0";
            }
            if (this.getIsOcienBill() && (good.SLV == 0.05))
            {
                sL = num;
                if (!Finacial.Equal(dJ, 0.0))
                {
                    jE = Finacial.GetRound((double) (((dJ * 19.0) / 20.0) * sL), 2);
                    sE = Finacial.GetRound((double) (jE / 19.0), 2);
                }
                else
                {
                    double num7 = Finacial.Add(jE, sE, 15);
                    if (!(sL == 0.0))
                    {
                        dJ = Finacial.Div(num7, sL, 15);
                    }
                    else
                    {
                        dJ = 0.0;
                    }
                }
            }
            else if (Math.Abs((double) (good.SLV - 0.015)) < 1E-05)
            {
                if (good.HSJBZ)
                {
                    sL = num;
                    if (!(dJ == 0.0))
                    {
                        sE = ((sL * dJ) * 0.015) / 1.05;
                        jE = (sL * dJ) - sE;
                    }
                    else if ((dJ == 0.0) && (sL != 0.0))
                    {
                        dJ = Finacial.Div(jE + sE, sL, 15);
                    }
                }
                else
                {
                    sL = num;
                    if (!(dJ == 0.0))
                    {
                        jE = sL * dJ;
                        sE = jE / 69.0;
                    }
                    else if ((dJ == 0.0) && (sL != 0.0))
                    {
                        dJ = Finacial.Div(jE, sL, 15);
                    }
                }
                good.JE = jE;
                good.SE = sE;
                good.SL = sL;
                good.DJ = dJ;
                good.SLV = sLV;
            }
            else if (good.KCE != 0.0)
            {
                sL = num;
                if (!Finacial.Equal(dJ, 0.0))
                {
                    double round = Finacial.GetRound((double) (good.DJ * sL), 2);
                    if (round < 0.0)
                    {
                        good.KCE = -1.0 * Math.Abs(good.KCE);
                        if (good.KCE < round)
                        {
                            good.KCE = 0.0;
                        }
                    }
                    else
                    {
                        good.KCE = Math.Abs(good.KCE);
                        if (round < good.KCE)
                        {
                            good.KCE = 0.0;
                        }
                    }
                    if (good.HSJBZ)
                    {
                        double num9 = sL * dJ;
                        sE = Finacial.GetRound((double) (((num9 - good.KCE) * good.SLV) / (1.0 + good.SLV)), 2);
                        jE = num9 - sE;
                    }
                    else
                    {
                        jE = sL * dJ;
                        sE = Finacial.GetRound((double) (((sL * dJ) - good.KCE) * good.SLV), 2);
                    }
                }
                else
                {
                    dJ = Finacial.Div(good.HSJBZ ? (jE + sE) : jE, sL, 15);
                }
            }
            else
            {
                sL = num;
                double num10 = good.getDj(false);
                if (!Finacial.Equal(dJ, 0.0))
                {
                    jE = Finacial.GetRound((double) (num10 * sL), 2);
                    if (this.ContainTax)
                    {
                        sE = Finacial.GetRound(Finacial.Subtract(sL * good.getDj(true), jE, 15), 2);
                    }
                    else
                    {
                        sE = Finacial.GetRound((double) (jE * good.SLV), 2);
                    }
                }
                else
                {
                    dJ = Finacial.Div(this.ContainTax ? (jE + sE) : jE, sL, 15);
                }
            }
            string str = "0";
            if ("0" == str)
            {
                good.JE = jE;
                good.SE = sE;
                good.SL = sL;
                good.DJ = dJ;
                return "0";
            }
            return str;
        }

        public void setSlv(int mxID, string sSlv)
        {
            double round = Finacial.GetRound(this.getDouble(sSlv), 3);
            if ((round <= 0.0) || (round >= 1.0))
            {
                if ((round >= 1.0) && (round < 100.0))
                {
                    round = Finacial.Div(round, 100.0, 15);
                }
                else
                {
                    round = 0.0;
                }
            }
            Goods good = this.GetGood(mxID);
            if (good.DJHXZ == 0)
            {
                bool flag2 = this.getIsOcienBill() && (round == 0.05);
                double jE = good.JE;
                double sE = good.SE;
                double sL = good.SL;
                double dJ = good.DJ;
                double sLV = good.SLV;
                bool hSJBZ = good.HSJBZ;
                if (flag2)
                {
                    good.KCE = 0.0;
                    good.SLV = round;
                    double num7 = Finacial.Div(good.JE, 19.0, 15);
                    good.SE = Finacial.GetRound(num7, 2);
                    if (!(good.SL == 0.0))
                    {
                        good.DJ = Finacial.Div(good.JE + num7, good.SL, 15);
                        good.HSJBZ = true;
                    }
                }
                else if (Math.Abs((double) (round - 0.015)) < 1E-05)
                {
                    good.KCE = 0.0;
                    sLV = round;
                    if (jE != 0.0)
                    {
                        if (this.ContainTax)
                        {
                            double num8 = good.JE + good.SE;
                            sE = (num8 * 0.015) / 1.05;
                            jE = num8 - sE;
                            dJ = Finacial.Div(jE, sL, 15);
                            hSJBZ = false;
                        }
                        else
                        {
                            sE = jE / 69.0;
                            dJ = Finacial.Div(jE, sL, 15);
                            hSJBZ = false;
                        }
                    }
                    good.SLV = sLV;
                    good.JE = jE;
                    good.SE = sE;
                    good.DJ = dJ;
                    good.HSJBZ = hSJBZ;
                }
                else if (good.KCE != 0.0)
                {
                    good.SLV = round;
                    if (!this.ContainTax)
                    {
                        if (jE < 0.0)
                        {
                            good.KCE = -1.0 * Math.Abs(good.KCE);
                            if (good.KCE < good.JE)
                            {
                                good.KCE = 0.0;
                            }
                        }
                        else
                        {
                            good.KCE = Math.Abs(good.KCE);
                            if (good.JE < good.KCE)
                            {
                                good.KCE = 0.0;
                            }
                        }
                        sE = Finacial.GetRound((double) ((jE - good.KCE) * good.SLV), 2);
                        good.SE = sE;
                        good.JE = jE;
                        if (good.HSJBZ)
                        {
                            good.DJ = Finacial.Div(good.JE + good.SE, good.SL, 15);
                        }
                        if (Finacial.Equal(good.SL, 0.0))
                        {
                            good.DJnoTax = Finacial.Div(good.JE, good.SL, 15);
                        }
                    }
                    else
                    {
                        double num9 = jE + sE;
                        if (num9 < 0.0)
                        {
                            good.KCE = -1.0 * Math.Abs(good.KCE);
                            if (good.KCE < num9)
                            {
                                good.KCE = 0.0;
                            }
                        }
                        else
                        {
                            good.KCE = Math.Abs(good.KCE);
                            if (num9 < good.KCE)
                            {
                                good.KCE = 0.0;
                            }
                        }
                        jE = ((num9 - good.KCE) / (1.0 + good.SLV)) + good.KCE;
                        sE = ((num9 - good.KCE) * good.SLV) / (1.0 + good.SLV);
                        if (!Finacial.Equal(good.DJ, 0.0))
                        {
                            if (good.HSJBZ)
                            {
                                sL = Finacial.Div(jE + sE, dJ, 15);
                            }
                            else
                            {
                                sL = Finacial.Div(jE, dJ, 15);
                            }
                        }
                        else
                        {
                            sL = 0.0;
                        }
                        good.SE = sE;
                        good.JE = jE;
                        good.SL = sL;
                        if (Finacial.Equal(good.SL, 0.0))
                        {
                            good.DJnoTax = Finacial.Div(good.JE, good.SL, 15);
                        }
                    }
                }
                else
                {
                    good.SLV = round;
                    double num10 = good.getDj(false);
                    if (!Finacial.Equal(good.DJ, 0.0))
                    {
                        jE = Finacial.GetRound((double) (num10 * good.SL), 2);
                        if (this.ContainTax)
                        {
                            sE = Finacial.GetRound(Finacial.Subtract(sL * good.getDj(true), jE, 15), 2);
                        }
                        else
                        {
                            sE = Finacial.GetRound((double) (jE * good.SLV), 2);
                        }
                    }
                    else
                    {
                        sE = Finacial.GetRound((double) (jE * good.SLV), 2);
                    }
                    good.SE = sE;
                    good.JE = jE;
                    if (Finacial.Equal(good.SL, 0.0))
                    {
                        good.DJnoTax = Finacial.Div(good.JE, good.SL, 15);
                    }
                }
            }
        }

        public string BH
        {
            get
            {
                return this.bh;
            }
            set
            {
                this.bh = GetSafeData.GetSafeString(value, 50);
            }
        }

        public string BZ
        {
            get
            {
                return this.bz;
            }
            set
            {
                this.bz = GetSafeData.GetSafeString(value, 230);
            }
        }

        public bool CFHB { get; set; }

        public string CM
        {
            get
            {
                return this.cm;
            }
            set
            {
                this.cm = GetSafeData.GetSafeString(value, 50);
            }
        }

        public DateTime DJRQ { get; set; }

        public int DJYF { get; set; }

        public DateTime DLGRQ { get; set; }

        public string DW
        {
            get
            {
                return this.dw;
            }
            set
            {
                this.dw = GetSafeData.GetSafeString(value, 50);
            }
        }

        public string FHR
        {
            get
            {
                return this.fhr;
            }
            set
            {
                this.fhr = GetSafeData.GetSafeString(value, 0x10);
            }
        }

        public string GFDZDH
        {
            get
            {
                return this.gfdzdh;
            }
            set
            {
                this.gfdzdh = GetSafeData.GetSafeString(value, 100);
            }
        }

        public string GFMC
        {
            get
            {
                return this.gfmc;
            }
            set
            {
                this.gfmc = GetSafeData.GetSafeString(value, 100);
            }
        }

        public string GFSH
        {
            get
            {
                return this.gfsh;
            }
            set
            {
                this.gfsh = GetSafeData.GetSafeString(value, 0x19);
            }
        }

        public string GFYHZH
        {
            get
            {
                return this.gfyhzh;
            }
            set
            {
                this.gfyhzh = GetSafeData.GetSafeString(value, 100);
            }
        }

        public int GoodsNum { get; set; }

        public string HY_BH
        {
            get
            {
                return this.hy_bh;
            }
            set
            {
                this.hy_bh = GetSafeData.GetSafeString(value, 20);
            }
        }

        public string HY_BZ
        {
            get
            {
                return this.hy_bz;
            }
            set
            {
                this.hy_bz = GetSafeData.GetSafeString(value, 200);
            }
        }

        public string HY_CHDW
        {
            get
            {
                return this.hy_chdw;
            }
            set
            {
                this.hy_chdw = GetSafeData.GetSafeString(value, 8);
            }
        }

        public string HY_CZCH
        {
            get
            {
                return this.hy_czch;
            }
            set
            {
                this.hy_czch = GetSafeData.GetSafeString(value, 50);
            }
        }

        public string HY_DJRQ
        {
            get
            {
                return this.hy_djrq;
            }
            set
            {
                this.hy_djrq = GetSafeData.GetSafeString(value, 0x10);
            }
        }

        public string HY_FHR
        {
            get
            {
                return this.hy_fhr;
            }
            set
            {
                this.hy_fhr = GetSafeData.GetSafeString(value, 8);
            }
        }

        public string HY_FHRMC
        {
            get
            {
                return this.hy_fhrmc;
            }
            set
            {
                this.hy_fhrmc = GetSafeData.GetSafeString(value, 80);
            }
        }

        public string HY_FHRSH
        {
            get
            {
                return this.hy_fhrsh;
            }
            set
            {
                this.hy_fhrsh = GetSafeData.GetSafeString(value, 20);
            }
        }

        public string HY_QYMD
        {
            get
            {
                return this.hy_qymd;
            }
            set
            {
                this.hy_qymd = GetSafeData.GetSafeString(value, 90);
            }
        }

        public string HY_SHRMC
        {
            get
            {
                return this.hy_shrmc;
            }
            set
            {
                this.hy_shrmc = GetSafeData.GetSafeString(value, 80);
            }
        }

        public string HY_SHRSH
        {
            get
            {
                return this.hy_shrsh;
            }
            set
            {
                this.hy_shrsh = GetSafeData.GetSafeString(value, 20);
            }
        }

        public string HY_SKR
        {
            get
            {
                return this.hy_skr;
            }
            set
            {
                this.hy_skr = GetSafeData.GetSafeString(value, 8);
            }
        }

        public double HY_SL { get; set; }

        public string HY_SPFMC
        {
            get
            {
                return this.hy_spfmc;
            }
            set
            {
                this.hy_spfmc = GetSafeData.GetSafeString(value, 80);
            }
        }

        public string HY_SPFSH
        {
            get
            {
                return this.hy_spfsh;
            }
            set
            {
                this.hy_spfsh = GetSafeData.GetSafeString(value, 20);
            }
        }

        public string HY_YSHWXX
        {
            get
            {
                return this.hy_yshwxx;
            }
            set
            {
                this.hy_yshwxx = GetSafeData.GetSafeString(value, 200);
            }
        }

        public bool HYSY { get; set; }

        public bool IsANew { get; set; }

        public string JDC_BH
        {
            get
            {
                return this.jdc_bh;
            }
            set
            {
                this.jdc_bh = GetSafeData.GetSafeString(value, 20);
            }
        }

        public string JDC_BZ
        {
            get
            {
                return this.jdc_bz;
            }
            set
            {
                this.jdc_bz = GetSafeData.GetSafeString(value, 230);
            }
        }

        public string JDC_CD
        {
            get
            {
                return this.jdc_cd;
            }
            set
            {
                this.jdc_cd = GetSafeData.GetSafeString(value, 0x20);
            }
        }

        public string JDC_CLBM
        {
            get
            {
                return this._jdc_clbm;
            }
            set
            {
                this._jdc_clbm = value;
            }
        }

        public string JDC_CLSBH
        {
            get
            {
                return this.jdc_clsbh;
            }
            set
            {
                this.jdc_clsbh = GetSafeData.GetSafeString(value, 0x17);
            }
        }

        public string JDC_CPXH
        {
            get
            {
                return this.jdc_cpxh;
            }
            set
            {
                this.jdc_cpxh = GetSafeData.GetSafeString(value, 60);
            }
        }

        public string JDC_DH
        {
            get
            {
                return this.jdc_dh;
            }
            set
            {
                this.jdc_dh = GetSafeData.GetSafeString(value, 40);
            }
        }

        public DateTime JDC_DJRQ { get; set; }

        public string JDC_DW
        {
            get
            {
                return this.jdc_dw;
            }
            set
            {
                this.jdc_dw = GetSafeData.GetSafeString(value, 50);
            }
        }

        public string JDC_DZ
        {
            get
            {
                return this.jdc_dz;
            }
            set
            {
                this.jdc_dz = GetSafeData.GetSafeString(value, 80);
            }
        }

        public string JDC_FDJH
        {
            get
            {
                return this.jdc_fdjh;
            }
            set
            {
                this.jdc_fdjh = GetSafeData.GetSafeString(value, 50);
            }
        }

        public string JDC_FLBM
        {
            get
            {
                return this._jdc_flbm;
            }
            set
            {
                this._jdc_flbm = value;
            }
        }

        public string JDC_FLMC
        {
            get
            {
                return this._jdc_flmc;
            }
            set
            {
                this._jdc_flmc = value;
            }
        }

        public string JDC_GHDW
        {
            get
            {
                return this.jdc_ghdw;
            }
            set
            {
                this.jdc_ghdw = GetSafeData.GetSafeString(value, 0x6c);
            }
        }

        public string JDC_HGZH
        {
            get
            {
                return this.jdc_hgzh;
            }
            set
            {
                this.jdc_hgzh = GetSafeData.GetSafeString(value, 50);
            }
        }

        public double JDC_JE { get; set; }

        public string JDC_JKZMSH
        {
            get
            {
                return this.jdc_jkzmsh;
            }
            set
            {
                this.jdc_jkzmsh = GetSafeData.GetSafeString(value, 0x24);
            }
        }

        public string JDC_KHYH
        {
            get
            {
                return this.jdc_khyh;
            }
            set
            {
                this.jdc_khyh = GetSafeData.GetSafeString(value, 80);
            }
        }

        public string JDC_LSLVBS
        {
            get
            {
                return this._jdc_lslvbs;
            }
            set
            {
                this._jdc_lslvbs = value;
            }
        }

        public string JDC_LX
        {
            get
            {
                return this.jdc_lx;
            }
            set
            {
                this.jdc_lx = GetSafeData.GetSafeString(value, 40);
            }
        }

        public string JDC_SCCJMC
        {
            get
            {
                return this.jdc_sccjmc;
            }
            set
            {
                this.jdc_sccjmc = GetSafeData.GetSafeString(value, 80);
            }
        }

        public string JDC_SFZ
        {
            get
            {
                return this.jdc_sfz;
            }
            set
            {
                this.jdc_sfz = GetSafeData.GetSafeString(value, 20);
            }
        }

        public string JDC_SJDH
        {
            get
            {
                return this.jdc_sjdh;
            }
            set
            {
                this.jdc_sjdh = GetSafeData.GetSafeString(value, 0x20);
            }
        }

        public double JDC_SL { get; set; }

        public string JDC_XCRS
        {
            get
            {
                return this.jdc_xcrs;
            }
            set
            {
                this.jdc_xcrs = GetSafeData.GetSafeString(value, 50);
            }
        }

        public bool JDC_XSYH
        {
            get
            {
                return this._jdc_xsyh;
            }
            set
            {
                this._jdc_xsyh = value;
            }
        }

        public string JDC_XSYHSM
        {
            get
            {
                return this._jdc_xsyhsm;
            }
            set
            {
                this._jdc_xsyhsm = value;
            }
        }

        public string JDC_ZH
        {
            get
            {
                return this.jdc_zh;
            }
            set
            {
                this.jdc_zh = GetSafeData.GetSafeString(value, 40);
            }
        }

        public double JEHJ { get; set; }

        public bool JZ_50_15
        {
            get
            {
                return this._jz_50_15;
            }
            set
            {
                this._jz_50_15 = value;
            }
        }

        public string KHYHMC
        {
            get
            {
                return this.khyhmc;
            }
            set
            {
                this.khyhmc = GetSafeData.GetSafeString(value, 0x20);
            }
        }

        public string KHYHZH
        {
            get
            {
                return this.khyhzh;
            }
            set
            {
                this.khyhzh = GetSafeData.GetSafeString(value, 40);
            }
        }

        public string MDD
        {
            get
            {
                return this.mdd;
            }
            set
            {
                this.mdd = GetSafeData.GetSafeString(value, 50);
            }
        }

        public string QDHSPMC
        {
            get
            {
                return this.qdhspmc;
            }
            set
            {
                this.qdhspmc = GetSafeData.GetSafeString(value, 100);
            }
        }

        public string QYD
        {
            get
            {
                return this.qyd;
            }
            set
            {
                this.qyd = GetSafeData.GetSafeString(value, 0x20);
            }
        }

        public string SCCJMC
        {
            get
            {
                return this.sccjmc;
            }
            set
            {
                this.sccjmc = GetSafeData.GetSafeString(value, 80);
            }
        }

        public string SeparateReason { get; set; }

        public bool SFZJY { get; set; }

        public string SKR
        {
            get
            {
                return this.skr;
            }
            set
            {
                this.skr = GetSafeData.GetSafeString(value, 0x10);
            }
        }

        public double SLV
        {
            get
            {
                return this._slv;
            }
            set
            {
                this._slv = value;
            }
        }

        public double TotalAmount { get; set; }

        public double TotalAmountTax { get; set; }

        public double TotalTax { get; set; }

        public string TYDH
        {
            get
            {
                return this.tydh;
            }
            set
            {
                this.tydh = GetSafeData.GetSafeString(value, 0x24);
            }
        }

        public string XFDH
        {
            get
            {
                return this.xfdh;
            }
            set
            {
                this.xfdh = GetSafeData.GetSafeString(value, 40);
            }
        }

        public string XFDZ
        {
            get
            {
                return this.xfdz;
            }
            set
            {
                this.xfdz = GetSafeData.GetSafeString(value, 60);
            }
        }

        public string XFDZDH
        {
            get
            {
                return this.xfdzdh;
            }
            set
            {
                this.xfdzdh = GetSafeData.GetSafeString(value, 100);
            }
        }

        public string XFYHZH
        {
            get
            {
                return this.xfyhzh;
            }
            set
            {
                this.xfyhzh = GetSafeData.GetSafeStringWithoutTrim(value, 100);
            }
        }

        public string XHD
        {
            get
            {
                return this.xhd;
            }
            set
            {
                this.xhd = GetSafeData.GetSafeString(value, 0x17);
            }
        }

        public string XSBM
        {
            get
            {
                return this.xsbm;
            }
            set
            {
                this.xsbm = GetSafeData.GetSafeString(value, 40);
            }
        }

        public bool YDXS { get; set; }

        public string YSHWXX
        {
            get
            {
                return this.yshwxx;
            }
            set
            {
                this.yshwxx = GetSafeData.GetSafeString(value, 200);
            }
        }

        public string ZHD
        {
            get
            {
                return this.zhd;
            }
            set
            {
                this.zhd = GetSafeData.GetSafeString(value, 50);
            }
        }
    }
}

