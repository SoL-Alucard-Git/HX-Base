namespace Aisino.Fwkp.Fpzpz.Model
{
    using System;

    public class XZQYBMModal
    {
        private string _strBM = string.Empty;
        private string _strMC = string.Empty;

        public string BM
        {
            get
            {
                return this._strBM;
            }
            set
            {
                this._strBM = value;
            }
        }

        public string MC
        {
            get
            {
                return this._strMC;
            }
            set
            {
                this._strMC = value;
            }
        }
    }
}

