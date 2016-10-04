namespace Aisino.Fwkp.Wbjk.Model
{
    using System;
    using System.Runtime.CompilerServices;

    public class GoodsImportTemp
    {
        private string _flbm = "";
        private string _flmc = "";
        private string _kce;
        private string _lslvbs = "";
        private string _spbm = "";
        private bool _xsyh = false;
        private string _xsyhsm = "";

        public GoodsImportTemp()
        {
            this._flbm = "";
            this._xsyh = false;
        }

        public string DJ { get; set; }

        public string DJHXZ { get; set; }

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

        public string GGXH { get; set; }

        public string HSJBZ { get; set; }

        public string JE { get; set; }

        public string JLDW { get; set; }

        public string KCE
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

        public string SE { get; set; }

        public string SL { get; set; }

        public string SLV { get; set; }

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

        public string SPMC { get; set; }

        public string SPSM { get; set; }

        public int XH { get; set; }

        public string XSDJBH { get; set; }

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

        public string ZKJE { get; set; }

        public string ZKL { get; set; }

        public string ZKSE { get; set; }
    }
}

