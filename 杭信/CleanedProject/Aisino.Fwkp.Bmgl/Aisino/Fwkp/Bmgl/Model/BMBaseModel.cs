namespace Aisino.Fwkp.Bmgl.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class BMBaseModel
    {
        private string _bm = string.Empty;
        private string _jm = string.Empty;
        private string _kjm = string.Empty;
        private string _mc = string.Empty;
        private string _sjbm = string.Empty;

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

        public string JM
        {
            get
            {
                return this._jm;
            }
            set
            {
                this._jm = value;
            }
        }

        public string KJM
        {
            get
            {
                return this._kjm;
            }
            set
            {
                this._kjm = value;
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

        public virtual string SJBM
        {
            get
            {
                return this._sjbm;
            }
            set
            {
                this._sjbm = value;
            }
        }

        public virtual int WJ { get; set; }
    }
}

