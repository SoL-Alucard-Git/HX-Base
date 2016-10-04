namespace Aisino.Fwkp.Xtsz.Model
{
    using System;

    public class SysTaxInfoModel
    {
        private int _bsrphf;
        private DateTime _bsrq;
        private string _dhhm;
        private string _frdb;
        private bool _jyzs;
        private string _kjzg;
        private int _kpxe;
        private string _qybh;
        private string _qymc;
        private string _yhzh;
        private string _yydz;
        private string _zclx;

        public SysTaxInfoModel()
        {
            this._qybh = string.Empty;
            this._qymc = string.Empty;
            this._zclx = string.Empty;
            this._bsrq = DateTime.Now;
            this._yhzh = string.Empty;
            this._frdb = string.Empty;
            this._yydz = string.Empty;
            this._dhhm = string.Empty;
            this._kjzg = string.Empty;
            this.QYBH = "";
            this.QYMC = "";
            this.ZCLX = "";
            this.KPXE = 0;
            this.BSRQ = DateTime.Now;
            this.BSRPHF = 0;
            this.YHZH = "";
            this.FRDB = "";
            this.YYDZ = "";
            this.DHHM = "";
            this.JYZS = false;
            this.KJZG = "";
        }

        public SysTaxInfoModel(string strQYBH, string strQYMC, string strZCLX, int nKPXE, DateTime dtBSRQ, int nBSRPHF, string strYHZH, string strFRDB, string strYYDZ, string strDHHM, bool bJYZS, string strKJZG)
        {
            this._qybh = string.Empty;
            this._qymc = string.Empty;
            this._zclx = string.Empty;
            this._bsrq = DateTime.Now;
            this._yhzh = string.Empty;
            this._frdb = string.Empty;
            this._yydz = string.Empty;
            this._dhhm = string.Empty;
            this._kjzg = string.Empty;
            this.QYBH = strQYBH;
            this.QYMC = strQYMC;
            this.ZCLX = strZCLX;
            this.KPXE = nKPXE;
            this.BSRQ = dtBSRQ;
            this.BSRPHF = nBSRPHF;
            this.YHZH = strYHZH;
            this.FRDB = strFRDB;
            this.YYDZ = strYYDZ;
            this.DHHM = strDHHM;
            this.JYZS = bJYZS;
            this.KJZG = strKJZG;
        }

        public int BSRPHF
        {
            get
            {
                return this._bsrphf;
            }
            set
            {
                this._bsrphf = value;
            }
        }

        public DateTime BSRQ
        {
            get
            {
                return this._bsrq;
            }
            set
            {
                this._bsrq = value;
            }
        }

        public string DHHM
        {
            get
            {
                return this._dhhm;
            }
            set
            {
                this._dhhm = value;
            }
        }

        public string FRDB
        {
            get
            {
                return this._frdb;
            }
            set
            {
                this._frdb = value;
            }
        }

        public bool JYZS
        {
            get
            {
                return this._jyzs;
            }
            set
            {
                this._jyzs = value;
            }
        }

        public string KJZG
        {
            get
            {
                return this._kjzg;
            }
            set
            {
                this._kjzg = value;
            }
        }

        public int KPXE
        {
            get
            {
                return this._kpxe;
            }
            set
            {
                this._kpxe = value;
            }
        }

        public string QYBH
        {
            get
            {
                return this._qybh;
            }
            set
            {
                this._qybh = value;
            }
        }

        public string QYMC
        {
            get
            {
                return this._qymc;
            }
            set
            {
                this._qymc = value;
            }
        }

        public string YHZH
        {
            get
            {
                return this._yhzh;
            }
            set
            {
                this._yhzh = value;
            }
        }

        public string YYDZ
        {
            get
            {
                return this._yydz;
            }
            set
            {
                this._yydz = value;
            }
        }

        public string ZCLX
        {
            get
            {
                return this._zclx;
            }
            set
            {
                this._zclx = value;
            }
        }
    }
}

