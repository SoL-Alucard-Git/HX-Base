namespace Aisino.Fwkp.Fpzpz.Model
{
    using System;

    public class PZFLBModal
    {
        private DateTime _dateKPRQ = DingYiZhiFuChuan.dataTimeCCRQ;
        private double _dDJ;
        private decimal _decJE = 0M;
        private double _dSL;
        private int _iHSBZ;
        private int _iJDBZ;
        private int _iZH;
        private string _strDJXX = string.Empty;
        private string _strJLDW = string.Empty;
        private string _strKHBH = string.Empty;
        private string _strKM = string.Empty;
        private string _strSPBH = string.Empty;

        public double DJ
        {
            get
            {
                return this._dDJ;
            }
            set
            {
                this._dDJ = value;
            }
        }

        public string DJXX
        {
            get
            {
                return this._strDJXX;
            }
            set
            {
                this._strDJXX = value;
            }
        }

        public int HSBZ
        {
            get
            {
                return this._iHSBZ;
            }
            set
            {
                this._iHSBZ = value;
            }
        }

        public int JDBZ
        {
            get
            {
                return this._iJDBZ;
            }
            set
            {
                this._iJDBZ = value;
            }
        }

        public decimal JE
        {
            get
            {
                return this._decJE;
            }
            set
            {
                this._decJE = value;
            }
        }

        public string JLDW
        {
            get
            {
                return this._strJLDW;
            }
            set
            {
                this._strJLDW = value;
            }
        }

        public string KHBH
        {
            get
            {
                return this._strKHBH;
            }
            set
            {
                this._strKHBH = value;
            }
        }

        public string KM
        {
            get
            {
                return this._strKM;
            }
            set
            {
                this._strKM = value;
            }
        }

        public DateTime KPRQ
        {
            get
            {
                return this._dateKPRQ;
            }
            set
            {
                this._dateKPRQ = value;
            }
        }

        public double SL
        {
            get
            {
                return this._dSL;
            }
            set
            {
                this._dSL = value;
            }
        }

        public string SPBH
        {
            get
            {
                return this._strSPBH;
            }
            set
            {
                this._strSPBH = value;
            }
        }

        public int ZH
        {
            get
            {
                return this._iZH;
            }
            set
            {
                this._iZH = value;
            }
        }
    }
}

