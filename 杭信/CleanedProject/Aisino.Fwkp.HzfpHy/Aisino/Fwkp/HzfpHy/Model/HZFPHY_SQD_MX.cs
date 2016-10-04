namespace Aisino.Fwkp.HzfpHy.Model
{
    using System;

    [Serializable]
    public class HZFPHY_SQD_MX
    {
        private int _fphxz;
        private bool _hsjbz;
        private decimal _je;
        private int _mxxh;
        private decimal _se;
        private string _spmc;
        private string _sqdh;
        private string flbm;
        private string lslbs;
        private string qyspbm;
        private string sfxsyhzc;
        private string yhzcmc;

        public string FLBM
        {
            get
            {
                return this.flbm;
            }
            set
            {
                this.flbm = value;
            }
        }

        public int FPHXZ
        {
            get
            {
                return this._fphxz;
            }
            set
            {
                this._fphxz = value;
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

        public decimal JE
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

        public string LSLBS
        {
            get
            {
                return this.lslbs;
            }
            set
            {
                this.lslbs = value;
            }
        }

        public int MXXH
        {
            get
            {
                return this._mxxh;
            }
            set
            {
                this._mxxh = value;
            }
        }

        public string QYSPBM
        {
            get
            {
                return this.qyspbm;
            }
            set
            {
                this.qyspbm = value;
            }
        }

        public decimal SE
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

        public string SFXSYHZC
        {
            get
            {
                return this.sfxsyhzc;
            }
            set
            {
                this.sfxsyhzc = value;
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

        public string SQDH
        {
            get
            {
                return this._sqdh;
            }
            set
            {
                this._sqdh = value;
            }
        }

        public string YHZCMC
        {
            get
            {
                return this.yhzcmc;
            }
            set
            {
                this.yhzcmc = value;
            }
        }
    }
}

