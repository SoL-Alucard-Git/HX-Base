namespace Aisino.Fwkp.Bmgl.Model
{
    using System;

    [Serializable]
    internal class BMXZQYModel
    {
        private string _bm = "";
        private string _mc = "";

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
    }
}

