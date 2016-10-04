namespace Aisino.Fwkp.Wbjk.Model
{
    using Aisino.Fwkp.Wbjk.Common;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SaleBillImportTemp
    {
        public string _FHR = "";
        public string _HY_BZ = "";
        public string _HY_CHDW = "";
        public string _HY_CZCH = "";
        public string _HY_FHR = "";
        public string _HY_FHRMC = "";
        public string _HY_FHRSH = "";
        public string _HY_QYMD = "";
        public string _HY_SHRMC = "";
        public string _HY_SHRSH = "";
        public string _HY_SKR = "";
        public string _HY_SPFMC = "";
        public string _HY_SPFSH = "";
        private string _hysy = "0";
        public string _JDC_BZ = "";
        private string _jdc_clbm = "";
        public string _JDC_DW = "";
        public string _JDC_DZ = "";
        private string _jdc_flbm = "";
        private string _jdc_flmc = "";
        public string _JDC_KHYH = "";
        private string _jdc_lslvbs = "";
        public string _JDC_NSRSBH = "";
        public string _JDC_SFZ = "";
        public string _JDC_XCRS = "";
        private bool _jdc_xsyh = false;
        private string _jdc_xsyhsm = "";
        private string _sfzjy = "0";
        public string _SKR = "";
        public string BH = "";
        public string BZ = "";
        public string CFHB = "";
        public string DJYF = "";
        public string DJZL = "";
        public string DJZT = "";
        public string GFDZDH = "";
        public string GFMC = "";
        public string GFSH = "";
        public string GFYHZH = "";
        public string GoodsNum = "";
        public string HY_BH = "";
        public string HY_GoodsNum = "";
        public string HY_SL = "";
        public string HY_YSHWXX = "";
        public string JDC_BH = "";
        public string JDC_CD = "";
        public string JDC_CLSBH = "";
        public string JDC_CPXH = "";
        public string JDC_DH = "";
        public string JDC_FDJH = "";
        public string JDC_GHDW = "";
        public string JDC_HGZH = "";
        public string JDC_JE = "";
        public string JDC_JKZMSH = "";
        public string JDC_LX = "";
        public string JDC_SCCJMC = "";
        public string JDC_SJDH = "";
        public string JDC_SL = "";
        public string JDC_ZH = "";
        public string JEHJ = "";
        public string KPZT = "";
        public List<GoodsImportTemp> ListGoods = new List<GoodsImportTemp>();
        public string NCPBZ = "";
        public string QDHSPMC = "";
        public string XFDZDH = "";
        public string XFMC = "";
        public string XFSH = "";
        public string XFYHZH = "";
        public string XSBM = "";
        public string YDXS = "";

        public bool ContainTax { get; set; }

        public DateTime DJRQ { get; set; }

        public string FHR
        {
            get
            {
                return this._FHR;
            }
            set
            {
                this._FHR = GetSafeData.GetSafeString(value, 8);
            }
        }

        public string HY_BZ
        {
            get
            {
                return this._HY_BZ;
            }
            set
            {
                this._HY_BZ = GetSafeData.GetSafeString(value, 200);
            }
        }

        public string HY_CHDW
        {
            get
            {
                return this._HY_CHDW;
            }
            set
            {
                this._HY_CHDW = GetSafeData.GetSafeString(value, 8);
            }
        }

        public string HY_CZCH
        {
            get
            {
                return this._HY_CZCH;
            }
            set
            {
                this._HY_CZCH = GetSafeData.GetSafeString(value, 30);
            }
        }

        public DateTime HY_DJRQ { get; set; }

        public string HY_FHR
        {
            get
            {
                return this._HY_FHR;
            }
            set
            {
                this._HY_FHR = GetSafeData.GetSafeString(value, 0x10);
            }
        }

        public string HY_FHRMC
        {
            get
            {
                return this._HY_FHRMC;
            }
            set
            {
                this._HY_FHRMC = GetSafeData.GetSafeString(value, 80);
            }
        }

        public string HY_FHRSH
        {
            get
            {
                return this._HY_FHRSH;
            }
            set
            {
                this._HY_FHRSH = GetSafeData.GetSafeString(value, 20);
            }
        }

        public string HY_QYMD
        {
            get
            {
                return this._HY_QYMD;
            }
            set
            {
                this._HY_QYMD = GetSafeData.GetSafeString(value, 90);
            }
        }

        public string HY_SHRMC
        {
            get
            {
                return this._HY_SHRMC;
            }
            set
            {
                this._HY_SHRMC = GetSafeData.GetSafeString(value, 80);
            }
        }

        public string HY_SHRSH
        {
            get
            {
                return this._HY_SHRSH;
            }
            set
            {
                this._HY_SHRSH = GetSafeData.GetSafeString(value, 20);
            }
        }

        public string HY_SKR
        {
            get
            {
                return this._HY_SKR;
            }
            set
            {
                this._HY_SKR = GetSafeData.GetSafeString(value, 0x10);
            }
        }

        public string HY_SPFMC
        {
            get
            {
                return this._HY_SPFMC;
            }
            set
            {
                this._HY_SPFMC = GetSafeData.GetSafeString(value, 80);
            }
        }

        public string HY_SPFSH
        {
            get
            {
                return this._HY_SPFSH;
            }
            set
            {
                this._HY_SPFSH = GetSafeData.GetSafeString(value, 20);
            }
        }

        public string HYSY
        {
            get
            {
                return this._hysy;
            }
            set
            {
                this._hysy = value;
            }
        }

        public string JDC_BZ
        {
            get
            {
                return this._JDC_BZ;
            }
            set
            {
                this._JDC_BZ = GetSafeData.GetSafeString(value, 200);
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

        public DateTime JDC_DJRQ { get; set; }

        public string JDC_DW
        {
            get
            {
                return this._JDC_DW;
            }
            set
            {
                this._JDC_DW = GetSafeData.GetSafeString(value, 8);
            }
        }

        public string JDC_DZ
        {
            get
            {
                return this._JDC_DZ;
            }
            set
            {
                this._JDC_DZ = GetSafeData.GetSafeString(value, 80);
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

        public string JDC_KHYH
        {
            get
            {
                return this._JDC_KHYH;
            }
            set
            {
                this._JDC_KHYH = GetSafeData.GetSafeString(value, 80);
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

        public string JDC_NSRSBH
        {
            get
            {
                return this._JDC_NSRSBH;
            }
            set
            {
                this._JDC_NSRSBH = GetSafeData.GetSafeString(value, 20);
            }
        }

        public string JDC_SFZ
        {
            get
            {
                return this._JDC_SFZ;
            }
            set
            {
                this._JDC_SFZ = GetSafeData.GetSafeString(value, 0x16);
            }
        }

        public string JDC_XCRS
        {
            get
            {
                return this._JDC_XCRS;
            }
            set
            {
                this._JDC_XCRS = GetSafeData.GetSafeString(value, 12);
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

        public string SFZJY
        {
            get
            {
                return this._sfzjy;
            }
            set
            {
                this._sfzjy = value;
            }
        }

        public string SKR
        {
            get
            {
                return this._SKR;
            }
            set
            {
                this._SKR = GetSafeData.GetSafeString(value, 8);
            }
        }
    }
}

