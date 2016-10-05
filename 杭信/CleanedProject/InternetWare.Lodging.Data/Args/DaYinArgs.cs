using System;

namespace InternetWare.Lodging.Args
{
    public class DaYinArgs : EventArgs
    {
        public string FPZL { get; set; }

        public string FPDM { get; set; }

        public int FPHM { get; set; }

        public string Printer { get; set; }
    }
}
