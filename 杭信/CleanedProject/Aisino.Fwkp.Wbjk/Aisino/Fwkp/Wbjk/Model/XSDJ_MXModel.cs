namespace Aisino.Fwkp.Wbjk.Model
{
    using System;
    using System.Runtime.CompilerServices;

    public class XSDJ_MXModel
    {
        private double _dj;
        private int _djhxz;
        private double _djShow;
        private string _flbm;
        private string _flmc;
        private string _fpdm;
        private int _fphm;
        private string _fpzl;
        private string _ggxh;
        private bool _hsjbz;
        private double _je;
        private double _jeShow;
        private string _jldw;
        private double _kce;
        private string _lslvbs;
        private int _scfpxh;
        private double _se;
        private double _sl;
        private double _slv;
        private string _spbm;
        private string _spmc = "";
        private string _spsm;
        private int _xh;
        private string _xsdjbh;
        private bool _xsyh;
        private string _xsyhsm;
        private double _zkje;
        private double _zkl;

        public static XSDJ_MXModel Clone(XSDJ_MXModel mx)
        {
            return new XSDJ_MXModel { DJ = mx.DJ, DJHXZ = mx.DJHXZ, GGXH = mx.GGXH, HSJBZ = mx.HSJBZ, JE = mx.JE, JLDW = mx.JLDW, SE = mx.SE, SL = mx.SL, SLV = mx.SLV, SPMC = mx.SPMC, SPSM = mx.SPSM, XH = mx.XH, XSDJBH = mx.XSDJBH };
        }

        public double DJ
        {
            get
            {
                return this._dj;
            }
            set
            {
                this._dj = value;
            }
        }

        public int DJHXZ
        {
            get
            {
                return this._djhxz;
            }
            set
            {
                this._djhxz = value;
            }
        }

        public double DjShow
        {
            get
            {
                return this._djShow;
            }
            set
            {
                this._djShow = value;
            }
        }

        public string FLBM
        {
            get
            {
                return this._flbm;
            }
            set
            {
                this._flbm = value;
            }
        }

        public string FLMC
        {
            get
            {
                return this._flmc;
            }
            set
            {
                this._flmc = value;
            }
        }

        public string FPDM
        {
            get
            {
                return this._fpdm;
            }
            set
            {
                this._fpdm = value;
            }
        }

        public int FPHM
        {
            get
            {
                return this._fphm;
            }
            set
            {
                this._fphm = value;
            }
        }

        public string FPZL
        {
            get
            {
                return this._fpzl;
            }
            set
            {
                this._fpzl = value;
            }
        }

        public string GGXH
        {
            get
            {
                return this._ggxh;
            }
            set
            {
                this._ggxh = value;
            }
        }

        public bool HSJBZ
        {
            get
            {
                return this._hsjbz;
            }
            set
            {
                this._hsjbz = value;
            }
        }

        public double JE
        {
            get
            {
                return this._je;
            }
            set
            {
                this._je = value;
            }
        }

        public double JeShow
        {
            get
            {
                return this._jeShow;
            }
            set
            {
                this._jeShow = value;
            }
        }

        public string JLDW
        {
            get
            {
                return this._jldw;
            }
            set
            {
                this._jldw = value;
            }
        }

        public double KCE
        {
            get
            {
                return this._kce;
            }
            set
            {
                this._kce = value;
            }
        }

        public string LSLVBS
        {
            get
            {
                return this._lslvbs;
            }
            set
            {
                this._lslvbs = value;
            }
        }

        public string Reserve { get; set; }

        public int SCFPXH
        {
            get
            {
                return this._scfpxh;
            }
            set
            {
                this._scfpxh = value;
            }
        }

        public double SE
        {
            get
            {
                return this._se;
            }
            set
            {
                this._se = value;
            }
        }

        public double SL
        {
            get
            {
                return this._sl;
            }
            set
            {
                this._sl = value;
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

        public string SPBM
        {
            get
            {
                return this._spbm;
            }
            set
            {
                this._spbm = value;
            }
        }

        public string SPMC
        {
            get
            {
                return this._spmc;
            }
            set
            {
                this._spmc = value;
            }
        }

        public string SPSM
        {
            get
            {
                return this._spsm;
            }
            set
            {
                if (value.Trim().Length > 4)
                {
                    this._spsm = value.Substring(0, 4);
                }
                else
                {
                    this._spsm = value;
                }
            }
        }

        public int XH
        {
            get
            {
                return this._xh;
            }
            set
            {
                this._xh = value;
            }
        }

        public string XSDJBH
        {
            get
            {
                return this._xsdjbh;
            }
            set
            {
                this._xsdjbh = value;
            }
        }

        public bool XSYH
        {
            get
            {
                return this._xsyh;
            }
            set
            {
                this._xsyh = value;
            }
        }

        public string XSYHSM
        {
            get
            {
                return this._xsyhsm;
            }
            set
            {
                this._xsyhsm = value;
            }
        }

        public double ZKJE
        {
            get
            {
                return this._zkje;
            }
            set
            {
                this._zkje = value;
            }
        }

        public double ZKL
        {
            get
            {
                return this._zkl;
            }
            set
            {
                this._zkl = value;
            }
        }
    }
}

