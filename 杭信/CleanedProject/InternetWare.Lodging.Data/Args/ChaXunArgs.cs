using System;

namespace IntetnetWare.Lodging.Args
{
    public class ChaXunArgs : EventArgs
    {
        public int Year { get; set; }
        /// <summary>小于等于0 时表示全年</summary>
        public int Month { get; set; }
        /// <summary> 空：全部  "s" 专用发票 "c" 普通发票</summary>
        public string FPZL { get; set; }
        public string MathStr { get; set; }
        public bool WeiBaoSongChecked { get; set; } = false;
        public bool YanQianShiBaiChecked { get; set; } = false;

    }
}
