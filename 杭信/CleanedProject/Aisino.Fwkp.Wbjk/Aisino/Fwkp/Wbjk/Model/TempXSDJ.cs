namespace Aisino.Fwkp.Wbjk.Model
{
    using Aisino.Fwkp.Wbjk.Common;
    using System;
    using System.Collections.Generic;

    public class TempXSDJ
    {
        private string _benzhu = string.Empty;
        private string _djh = string.Empty;
        private DateTime _djrq = DateTime.Now;
        private string _djzl = string.Empty;
        private string _djzt = string.Empty;
        private string _fhr = string.Empty;
        private string _gfdzdh = string.Empty;
        private string _gfmc = string.Empty;
        private string _gfsh = string.Empty;
        private string _gfyhzh = string.Empty;
        private bool _hysy = false;
        private List<TempXSDJ_MX> _mingxi = new List<TempXSDJ_MX>();
        private string _qdhspmc = string.Empty;
        private string _sfzjy = "0";
        private string _skr = string.Empty;
        private int _sphs = 0;
        private string _xfdzdh = string.Empty;
        private string _xfyhzh = string.Empty;
        public bool Accept = true;

        public string Bz
        {
            get
            {
                return this._benzhu;
            }
            set
            {
                this._benzhu = GetSafeData.GetSafeString(value, 240);
            }
        }

        public string Djh
        {
            get
            {
                return this._djh;
            }
            set
            {
                this._djh = GetSafeData.GetSafeString(value, 20);
            }
        }

        public DateTime Djrq
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

        public string Djzl
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

        public string Fhr
        {
            get
            {
                return this._fhr;
            }
            set
            {
                this._fhr = GetSafeData.GetSafeString(value, 8);
            }
        }

        public string Gfdzdh
        {
            get
            {
                return this._gfdzdh;
            }
            set
            {
                this._gfdzdh = GetSafeData.GetSafeString(value, 100);
            }
        }

        public string Gfmc
        {
            get
            {
                return this._gfmc;
            }
            set
            {
                this._gfmc = GetSafeData.GetSafeString(value, 100);
            }
        }

        public string Gfsh
        {
            get
            {
                return this._gfsh;
            }
            set
            {
                this._gfsh = GetSafeData.GetSafeString(value, 0x19);
            }
        }

        public string Gfyhzh
        {
            get
            {
                return this._gfyhzh;
            }
            set
            {
                this._gfyhzh = GetSafeData.GetSafeString(value, 100);
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

        public List<TempXSDJ_MX> Mingxi
        {
            get
            {
                return this._mingxi;
            }
            set
            {
                this._mingxi = value;
            }
        }

        public string Qdspmc
        {
            get
            {
                return this._qdhspmc;
            }
            set
            {
                this._qdhspmc = GetSafeData.GetSafeString(value, 100);
            }
        }

        public string SFZJY
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

        public string Skr
        {
            get
            {
                return this._skr;
            }
            set
            {
                this._skr = GetSafeData.GetSafeString(value, 8);
            }
        }

        public int Sphs
        {
            get
            {
                return this._sphs;
            }
            set
            {
                this._sphs = value;
            }
        }

        public string Xfdzdh
        {
            get
            {
                return this._xfdzdh;
            }
            set
            {
                if (value.Length > 80)
                {
                    value = value.Substring(0, 80);
                }
                this._xfdzdh = value;
            }
        }

        public string Xfyhzh
        {
            get
            {
                return this._xfyhzh;
            }
            set
            {
                if (value.Length > 80)
                {
                    value = value.Substring(0, 80);
                }
                this._xfyhzh = value;
            }
        }
    }
}

