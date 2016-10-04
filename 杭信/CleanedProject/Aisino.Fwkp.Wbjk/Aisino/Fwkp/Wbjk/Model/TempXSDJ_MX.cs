namespace Aisino.Fwkp.Wbjk.Model
{
    using System;

    public class TempXSDJ_MX
    {
        private double? _bhsje = 0.0;
        private bool _danjiaerror = false;
        private double _dj = 0.0;
        private string _DJBH;
        private short _djhxz = 0;
        private string _fpzl = string.Empty;
        private string _gg = string.Empty;
        private int _hangShu;
        private string _hwmc = string.Empty;
        private string _jgfs = string.Empty;
        private string _jldw = string.Empty;
        private double? _se = 0.0;
        private double _sl = 0.0;
        private double? _slv = 0.0;
        private string _spsm = string.Empty;
        private double? _zkje = 0.0;
        private double? _zkl = 0.0;
        private string _zkse = string.Empty;

        public double? Bhsje
        {
            get
            {
                return this._bhsje;
            }
            set
            {
                this._bhsje = value;
            }
        }

        public bool DanJiaError
        {
            get
            {
                return this._danjiaerror;
            }
            set
            {
                this._danjiaerror = value;
            }
        }

        public double Dj
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

        public string DJBH
        {
            get
            {
                return this._DJBH;
            }
            set
            {
                this._DJBH = value;
            }
        }

        public short Djhxz
        {
            get
            {
                return this._djhxz;
            }
            set
            {
                if (value > 5)
                {
                    value = 0;
                }
                this._djhxz = value;
            }
        }

        public string Fpzl
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

        public string Gg
        {
            get
            {
                return this._gg;
            }
            set
            {
                if (value.Length > 30)
                {
                    value = value.Substring(0, 30);
                }
                this._gg = value;
            }
        }

        public int HangShu
        {
            get
            {
                return this._hangShu;
            }
            set
            {
                this._hangShu = value;
            }
        }

        public string Hwmc
        {
            get
            {
                return this._hwmc;
            }
            set
            {
                if (value.Length > 60)
                {
                    value = value.Substring(0, 60);
                }
                this._hwmc = value;
            }
        }

        public string Jgfs
        {
            get
            {
                return this._jgfs;
            }
            set
            {
                this._jgfs = value;
            }
        }

        public string Jldw
        {
            get
            {
                return this._jldw;
            }
            set
            {
                if (value.Length > 0x10)
                {
                    value = value.Substring(0, 0x10);
                }
                this._jldw = value;
            }
        }

        public double? Se
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

        public double Sl
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

        public double? Slv
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

        public string Spsm
        {
            get
            {
                return this._spsm;
            }
            set
            {
                if (value.Length > 4)
                {
                    value = value.Substring(0, 4);
                }
                this._spsm = value;
            }
        }

        public double? Zkje
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

        public double? Zkl
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

        public string Zkse
        {
            get
            {
                return this._zkse;
            }
            set
            {
                this._zkse = value;
            }
        }
    }
}

