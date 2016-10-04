namespace Aisino.Fwkp.Bmgl.Model
{
    using System;

    [Serializable]
    public class BMCDModel
    {
        private string _cddm = "";
        private string _cdjc = "";
        private string _cdmc = "";
        private string _xybz = "";
        private string _yxbz = "";

        public string CDDM
        {
            get
            {
                return this._cddm;
            }
            set
            {
                this._cddm = value;
            }
        }

        public string CDJC
        {
            get
            {
                return this._cdjc;
            }
            set
            {
                this._cdjc = value;
            }
        }

        public string CDMC
        {
            get
            {
                return this._cdmc;
            }
            set
            {
                this._cdmc = value;
            }
        }

        public string XYBZ
        {
            get
            {
                return this._xybz;
            }
            set
            {
                this._xybz = value;
            }
        }

        public string YXBZ
        {
            get
            {
                return this._yxbz;
            }
            set
            {
                this._yxbz = value;
            }
        }
    }
}

