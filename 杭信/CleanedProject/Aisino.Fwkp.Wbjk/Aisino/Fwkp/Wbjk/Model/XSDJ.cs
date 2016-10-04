namespace Aisino.Fwkp.Wbjk.Model
{
    using System;
    using System.Collections.Generic;

    public class XSDJ
    {
        private string _bdbs = string.Empty;
        private string _bdfz = string.Empty;
        private string _bdmc = string.Empty;
        private List<TempXSDJ> _dj = new List<TempXSDJ>();

        public string Bdbs
        {
            get
            {
                return this._bdbs;
            }
            set
            {
                this._bdbs = value;
            }
        }

        public string Bdfz
        {
            get
            {
                return this._bdfz;
            }
            set
            {
                this._bdfz = value;
            }
        }

        public string Bdmc
        {
            get
            {
                return this._bdmc;
            }
            set
            {
                this._bdmc = value;
            }
        }

        public List<TempXSDJ> Dj
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
    }
}

