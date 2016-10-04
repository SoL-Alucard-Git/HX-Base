namespace Aisino.Fwkp.Wbjk.Model
{
    using System;

    [Serializable]
    public class XSDJModel
    {
        private string _bh;
        private string _bz;
        private bool _cfhb;
        private string _cm;
        private DateTime _djrq;
        private int _djyf;
        private string _djzl;
        private string _djzt;
        private DateTime _dlgrq;
        private string _dw;
        private string _fhr;
        private string _gfdzdh;
        private string _gfmc;
        private string _gfsh;
        private string _gfyhzh;
        private bool _hysy;
        private string _jdc_clbm;
        private string _jdc_flbm;
        private string _jdc_flmc;
        private string _jdc_lslvbs;
        private bool _jdc_xsyh;
        private string _jdc_xsyhsm;
        private double _jehj;
        private bool _jz_50_15;
        private string _khyhmc;
        private string _khyhzh;
        private string _kpzt;
        private string _mdd;
        private string _qdhspmc;
        private string _qyd;
        private string _sccjmc;
        private bool _sfzjy;
        private string _skr;
        private double _slv;
        private string _tydh;
        private string _xfdh;
        private string _xfdz;
        private string _xfdzdh;
        private string _xfyhzh;
        private string _xhd;
        private string _xsbm;
        private bool _ydxs;
        private string _yshwxx;
        private string _zhd;

        public XSDJModel Clone()
        {
            return new XSDJModel { 
                _bh = this._bh, _gfmc = this._gfmc, _gfsh = this._gfsh, _gfdzdh = this._gfdzdh, _gfyhzh = this._gfyhzh, _xsbm = this._xsbm, _ydxs = this._ydxs, _jehj = this._jehj, _djrq = this._djrq, _djyf = this._djyf, _djzt = this._djzt, _kpzt = this._kpzt, _bz = this._bz, _fhr = this._fhr, _skr = this._skr, _qdhspmc = this._qdhspmc, 
                _xfyhzh = this._xfyhzh, _xfdzdh = this._xfdzdh, _cfhb = this._cfhb, _djzl = this._djzl, _hysy = this._hysy, _sfzjy = this._sfzjy, _cm = this._cm, _dlgrq = this._dlgrq, _khyhmc = this._khyhmc, _khyhzh = this._khyhzh, _tydh = this._tydh, _qyd = this._qyd, _zhd = this._zhd, _xhd = this._xhd, _mdd = this._mdd, _xfdz = this._xfdz, 
                _xfdh = this._xfdh, _yshwxx = this._yshwxx, _sccjmc = this._sccjmc, _slv = this._slv, _dw = this._dw, _jz_50_15 = this._jz_50_15
             };
        }

        public string BH
        {
            get
            {
                return this._bh;
            }
            set
            {
                this._bh = value;
            }
        }

        public string BZ
        {
            get
            {
                return this._bz;
            }
            set
            {
                this._bz = value;
            }
        }

        public bool CFHB
        {
            get
            {
                return this._cfhb;
            }
            set
            {
                this._cfhb = value;
            }
        }

        public string CM
        {
            get
            {
                return this._cm;
            }
            set
            {
                this._cm = value;
            }
        }

        public DateTime DJRQ
        {
            get
            {
                return this._djrq;
            }
            set
            {
                this._djrq = value;
            }
        }

        public double DJSLV
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

        public int DJYF
        {
            get
            {
                return this._djyf;
            }
            set
            {
                this._djyf = value;
            }
        }

        public string DJZL
        {
            get
            {
                return this._djzl;
            }
            set
            {
                this._djzl = value;
            }
        }

        public string DJZT
        {
            get
            {
                return this._djzt;
            }
            set
            {
                this._djzt = value;
            }
        }

        public DateTime DLGRQ
        {
            get
            {
                return this._dlgrq;
            }
            set
            {
                this._dlgrq = value;
            }
        }

        public string DW
        {
            get
            {
                return this._dw;
            }
            set
            {
                this._dw = value;
            }
        }

        public string FHR
        {
            get
            {
                return this._fhr;
            }
            set
            {
                this._fhr = value;
            }
        }

        public string GFDZDH
        {
            get
            {
                return this._gfdzdh;
            }
            set
            {
                this._gfdzdh = value;
            }
        }

        public string GFMC
        {
            get
            {
                return this._gfmc;
            }
            set
            {
                this._gfmc = value;
            }
        }

        public string GFSH
        {
            get
            {
                return this._gfsh;
            }
            set
            {
                this._gfsh = value;
            }
        }

        public string GFYHZH
        {
            get
            {
                return this._gfyhzh;
            }
            set
            {
                this._gfyhzh = value;
            }
        }

        public bool HYSY
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

        public double JEHJ
        {
            get
            {
                return this._jehj;
            }
            set
            {
                this._jehj = value;
            }
        }

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
                return this._khyhmc;
            }
            set
            {
                this._khyhmc = value;
            }
        }

        public string KHYHZH
        {
            get
            {
                return this._khyhzh;
            }
            set
            {
                this._khyhzh = value;
            }
        }

        public string KPZT
        {
            get
            {
                return this._kpzt;
            }
            set
            {
                this._kpzt = value;
            }
        }

        public string MDD
        {
            get
            {
                return this._mdd;
            }
            set
            {
                this._mdd = value;
            }
        }

        public string QDHSPMC
        {
            get
            {
                return this._qdhspmc;
            }
            set
            {
                this._qdhspmc = value;
            }
        }

        public string QYD
        {
            get
            {
                return this._qyd;
            }
            set
            {
                this._qyd = value;
            }
        }

        public string SCCJMC
        {
            get
            {
                return this._sccjmc;
            }
            set
            {
                this._sccjmc = value;
            }
        }

        public bool SFZJY
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
                return this._skr;
            }
            set
            {
                this._skr = value;
            }
        }

        public string TYDH
        {
            get
            {
                return this._tydh;
            }
            set
            {
                this._tydh = value;
            }
        }

        public string XFDH
        {
            get
            {
                return this._xfdh;
            }
            set
            {
                this._xfdh = value;
            }
        }

        public string XFDZ
        {
            get
            {
                return this._xfdz;
            }
            set
            {
                this._xfdz = value;
            }
        }

        public string XFDZDH
        {
            get
            {
                return this._xfdzdh;
            }
            set
            {
                this._xfdzdh = value;
            }
        }

        public string XFYHZH
        {
            get
            {
                return this._xfyhzh;
            }
            set
            {
                this._xfyhzh = value;
            }
        }

        public string XHD
        {
            get
            {
                return this._xhd;
            }
            set
            {
                this._xhd = value;
            }
        }

        public string XSBM
        {
            get
            {
                return this._xsbm;
            }
            set
            {
                this._xsbm = value;
            }
        }

        public bool YDXS
        {
            get
            {
                return this._ydxs;
            }
            set
            {
                this._ydxs = value;
            }
        }

        public string YSHWXX
        {
            get
            {
                return this._yshwxx;
            }
            set
            {
                this._yshwxx = value;
            }
        }

        public string ZHD
        {
            get
            {
                return this._zhd;
            }
            set
            {
                this._zhd = value;
            }
        }
    }
}

