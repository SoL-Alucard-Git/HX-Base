namespace Aisino.Fwkp.Xtsz.Model
{
    using System;

    public class OperatorModel
    {
        private string _kl;
        private string _mc;
        private string _qscd;
        private string _sf;
        private object _tb;

        public OperatorModel()
        {
            this._sf = string.Empty;
            this._mc = string.Empty;
            this._kl = string.Empty;
            this._qscd = string.Empty;
            this.SF = "管理员";
            this.MC = "";
            this.KL = "";
            this.TB = null;
            this.QSCD = "";
        }

        public OperatorModel(string strSF, string strMC)
        {
            this._sf = string.Empty;
            this._mc = string.Empty;
            this._kl = string.Empty;
            this._qscd = string.Empty;
            this.SF = strSF;
            this.MC = strMC;
            this.KL = "";
            this.TB = null;
            this.QSCD = "";
        }

        public OperatorModel(string strSF, string strMC, string strKL)
        {
            this._sf = string.Empty;
            this._mc = string.Empty;
            this._kl = string.Empty;
            this._qscd = string.Empty;
            this.SF = strSF;
            this.MC = strMC;
            this.KL = strKL;
            this.TB = null;
            this.QSCD = "";
        }

        public OperatorModel(string strSF, string strMC, string strKL, string strQSCD)
        {
            this._sf = string.Empty;
            this._mc = string.Empty;
            this._kl = string.Empty;
            this._qscd = string.Empty;
            this.SF = strSF;
            this.MC = strMC;
            this.KL = strKL;
            this.TB = null;
            this.QSCD = strQSCD;
        }

        public string KL
        {
            get
            {
                return this._kl;
            }
            set
            {
                this._kl = value;
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

        public string QSCD
        {
            get
            {
                return this._qscd;
            }
            set
            {
                this._qscd = value;
            }
        }

        public string SF
        {
            get
            {
                return this._sf;
            }
            set
            {
                this._sf = value;
            }
        }

        public object TB
        {
            get
            {
                return this._tb;
            }
            set
            {
                this._tb = value;
            }
        }
    }
}

