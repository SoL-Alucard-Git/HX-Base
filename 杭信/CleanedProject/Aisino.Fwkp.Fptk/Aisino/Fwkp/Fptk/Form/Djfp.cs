namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Runtime.CompilerServices;

    public class Djfp
    {
        public Djfp(string djh)
        {
            this.Djh = djh;
        }

        public string Djh { get; set; }

        public string ErrTip { get; set; }

        public string File { get; set; }

        public Aisino.Fwkp.BusinessObject.Fpxx Fpxx { get; set; }
    }
}

