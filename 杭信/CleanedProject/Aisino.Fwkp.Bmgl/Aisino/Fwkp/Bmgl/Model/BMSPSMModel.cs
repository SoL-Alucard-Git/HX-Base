namespace Aisino.Fwkp.Bmgl.Model
{
    using System;

    [Serializable]
    internal class BMSPSMModel
    {
        private string _bm = "";
        private bool _fhdbz;
        private string _jsdw = "";
        private string _mc = "";
        private double _mdxs;
        private double _se;
        private byte _sljs;
        private double _slv;
        private string _sz = "";
        private double _zsl;

        public string BM
        {
            get
            {
                return this._bm;
            }
            set
            {
                this._bm = value;
            }
        }

        public bool FHDBZ
        {
            get
            {
                return this._fhdbz;
            }
            set
            {
                this._fhdbz = value;
            }
        }

        public string JSDW
        {
            get
            {
                return this._jsdw;
            }
            set
            {
                this._jsdw = value;
            }
        }

        public string MC
        {
            get
            {
                return this._mc;
            }
            set
            {
                this._mc = value;
            }
        }

        public double MDXS
        {
            get
            {
                return this._mdxs;
            }
            set
            {
                this._mdxs = value;
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

        public byte SLJS
        {
            get
            {
                return this._sljs;
            }
            set
            {
                this._sljs = value;
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

        public string SZ
        {
            get
            {
                return this._sz;
            }
            set
            {
                this._sz = value;
            }
        }

        public double ZSL
        {
            get
            {
                return this._zsl;
            }
            set
            {
                this._zsl = value;
            }
        }
    }
}

