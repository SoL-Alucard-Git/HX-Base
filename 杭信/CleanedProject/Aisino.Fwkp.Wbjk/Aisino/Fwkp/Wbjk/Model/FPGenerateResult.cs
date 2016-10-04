namespace Aisino.Fwkp.Wbjk.Model
{
    using Aisino.Fwkp.Wbjk.BLL;
    using System;
    using System.Runtime.CompilerServices;

    public class FPGenerateResult : XSDJMXModel
    {
        private string fPDM;
        private long fPHM;
        private bool IsNewJDC;
        private string kKPX;
        private double kPJE;
        private string kPJG;
        private double kPSE;
        private string kPZL;
        private double sLV;
        private string sXYY;

        public FPGenerateResult()
        {
            this.fPDM = "";
            this.kPJG = "";
            this.kKPX = "";
            this.kPZL = "";
            this.sXYY = "";
            this.sLV = 0.0;
            this.kPJE = 0.0;
            this.kPSE = 0.0;
            this.IsNewJDC = false;
        }

        public FPGenerateResult(SaleBill XSDJ)
        {
            this.fPDM = "";
            this.kPJG = "";
            this.kKPX = "";
            this.kPZL = "";
            this.sXYY = "";
            this.sLV = 0.0;
            this.kPJE = 0.0;
            this.kPSE = 0.0;
            this.IsNewJDC = false;
            base.BH = XSDJ.BH;
            base.GFMC = XSDJ.GFMC;
            base.GFSH = XSDJ.GFSH;
            base.GFDZDH = XSDJ.GFDZDH;
            base.GFYHZH = XSDJ.GFYHZH;
            base.XSBM = XSDJ.XSBM;
            base.YDXS = XSDJ.YDXS;
            base.JEHJ = XSDJ.JEHJ;
            base.DJRQ = XSDJ.DJRQ;
            base.DJYF = XSDJ.DJYF;
            base.DJZT = XSDJ.DJZT;
            base.KPZT = XSDJ.KPZT;
            base.BZ = XSDJ.BZ;
            base.FHR = XSDJ.FHR;
            base.SKR = XSDJ.SKR;
            base.QDHSPMC = XSDJ.QDHSPMC;
            base.XFYHZH = XSDJ.XFYHZH;
            base.XFDZDH = XSDJ.XFDZDH;
            base.CFHB = XSDJ.CFHB;
            base.DJZL = XSDJ.DJZL;
            base.SFZJY = XSDJ.SFZJY;
            base.HYSY = XSDJ.HYSY;
            base.CM = XSDJ.CM;
            base.DLGRQ = XSDJ.DLGRQ;
            base.KHYHMC = XSDJ.KHYHMC;
            base.KHYHZH = XSDJ.KHYHZH;
            base.TYDH = XSDJ.TYDH;
            base.QYD = XSDJ.QYD;
            base.ZHD = XSDJ.ZHD;
            base.XHD = XSDJ.XHD;
            base.MDD = XSDJ.MDD;
            base.XFDZ = XSDJ.XFDZ;
            base.XFDH = XSDJ.XFDH;
            base.YSHWXX = XSDJ.YSHWXX;
            base.SCCJMC = XSDJ.SCCJMC;
            base.DJSLV = XSDJ.SLV;
            base.DW = XSDJ.DW;
            base.JDC_FLBM = XSDJ.JDC_FLBM;
            base.JDC_XSYH = XSDJ.JDC_XSYH;
            base.JDC_FLMC = "";
            base.JDC_XSYHSM = XSDJ.JDC_XSYHSM;
            base.JDC_CLBM = XSDJ.JDC_CLBM;
            base.JDC_LSLVBS = XSDJ.JDC_LSLVBS;
            base.JZ_50_15 = XSDJ.JZ_50_15;
            if (XSDJ.ListGoods.Count > 0)
            {
                this.SLV = XSDJ.ListGoods[0].SLV;
            }
            for (int i = 0; i < XSDJ.ListGoods.Count; i++)
            {
                Goods goods = XSDJ.ListGoods[i];
                this.KPJE += XSDJ.ListGoods[i].JE;
                this.KPSE += SaleBillCtrl.GetRound(XSDJ.ListGoods[i].SE, 2);
                this.KPJE = SaleBillCtrl.GetRound(this.KPJE, 2);
                this.KPSE = SaleBillCtrl.GetRound(this.KPSE, 2);
                XSDJ_MXModel item = new XSDJ_MXModel {
                    DJ = goods.DJ,
                    DJHXZ = goods.DJHXZ,
                    GGXH = goods.GGXH,
                    HSJBZ = goods.HSJBZ,
                    JE = goods.JE,
                    JLDW = goods.JLDW,
                    SE = SaleBillCtrl.GetRound(goods.SE, 2),
                    SL = goods.SL,
                    SLV = goods.SLV,
                    SPMC = goods.SPMC,
                    SPSM = goods.SPSM,
                    XH = goods.XH,
                    XSDJBH = goods.XSDJBH,
                    Reserve = goods.Reserve,
                    FLBM = goods.FLBM,
                    XSYH = goods.XSYH,
                    FLMC = "",
                    SPBM = goods.SPBM,
                    XSYHSM = goods.XSYHSM,
                    LSLVBS = goods.LSLVBS,
                    KCE = goods.KCE
                };
                base.ListXSDJ_MX.Add(item);
            }
        }

        public FPGenerateResult(XSDJMXModel XSDJ)
        {
            this.fPDM = "";
            this.kPJG = "";
            this.kKPX = "";
            this.kPZL = "";
            this.sXYY = "";
            this.sLV = 0.0;
            this.kPJE = 0.0;
            this.kPSE = 0.0;
            this.IsNewJDC = false;
            base.BH = XSDJ.BH;
            base.GFMC = XSDJ.GFMC;
            base.GFSH = XSDJ.GFSH;
            base.GFDZDH = XSDJ.GFDZDH;
            base.GFYHZH = XSDJ.GFYHZH;
            base.XSBM = XSDJ.XSBM;
            base.YDXS = XSDJ.YDXS;
            base.JEHJ = XSDJ.JEHJ;
            base.DJRQ = XSDJ.DJRQ;
            base.DJYF = XSDJ.DJYF;
            base.DJZT = XSDJ.DJZT;
            base.KPZT = XSDJ.KPZT;
            base.BZ = XSDJ.BZ;
            base.FHR = XSDJ.FHR;
            base.SKR = XSDJ.SKR;
            base.QDHSPMC = XSDJ.QDHSPMC;
            base.XFYHZH = XSDJ.XFYHZH;
            base.XFDZDH = XSDJ.XFDZDH;
            base.CFHB = XSDJ.CFHB;
            base.DJZL = XSDJ.DJZL;
            base.SFZJY = XSDJ.SFZJY;
            base.HYSY = XSDJ.HYSY;
            base.CM = XSDJ.CM;
            base.DLGRQ = XSDJ.DLGRQ;
            base.KHYHMC = XSDJ.KHYHMC;
            base.KHYHZH = XSDJ.KHYHZH;
            base.TYDH = XSDJ.TYDH;
            base.QYD = XSDJ.QYD;
            base.ZHD = XSDJ.ZHD;
            base.XHD = XSDJ.XHD;
            base.MDD = XSDJ.MDD;
            base.XFDZ = XSDJ.XFDZ;
            base.XFDH = XSDJ.XFDH;
            base.YSHWXX = XSDJ.YSHWXX;
            base.SCCJMC = XSDJ.SCCJMC;
            base.DJSLV = XSDJ.DJSLV;
            base.DW = XSDJ.DW;
            base.ListXSDJ_MX = XSDJ.ListXSDJ_MX;
            if (base.ListXSDJ_MX.Count > 0)
            {
                this.SLV = base.ListXSDJ_MX[0].SLV;
            }
            for (int i = 0; i < base.ListXSDJ_MX.Count; i++)
            {
                this.KPJE += base.ListXSDJ_MX[i].JE;
                this.KPSE += base.ListXSDJ_MX[i].SE;
                this.KPJE = SaleBillCtrl.GetRound(this.KPJE, 2);
                this.KPSE = SaleBillCtrl.GetRound(this.KPSE, 2);
            }
        }

        public string FPDM
        {
            get
            {
                return this.fPDM;
            }
            set
            {
                this.fPDM = value;
            }
        }

        public long FPHM
        {
            get
            {
                return this.fPHM;
            }
            set
            {
                this.fPHM = value;
            }
        }

        public bool ISNEWJDC
        {
            get
            {
                return this.IsNewJDC;
            }
            set
            {
                this.IsNewJDC = value;
            }
        }

        public string KKPX
        {
            get
            {
                return this.kKPX;
            }
            set
            {
                this.kKPX = value;
            }
        }

        public double KPJE
        {
            get
            {
                return this.kPJE;
            }
            set
            {
                this.kPJE = value;
            }
        }

        public string KPJG
        {
            get
            {
                return this.kPJG;
            }
            set
            {
                this.kPJG = value;
            }
        }

        public double KPSE
        {
            get
            {
                return this.kPSE;
            }
            set
            {
                this.kPSE = value;
            }
        }

        public string KPZL
        {
            get
            {
                return this.kPZL;
            }
            set
            {
                this.kPZL = value;
            }
        }

        public double SLV
        {
            get
            {
                return this.sLV;
            }
            set
            {
                this.sLV = value;
            }
        }

        public string SXYY
        {
            get
            {
                return this.sXYY;
            }
            set
            {
                this.sXYY = value;
            }
        }

        public string YBH { get; set; }
    }
}

