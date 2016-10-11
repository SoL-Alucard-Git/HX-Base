using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetWare.Lodging.Data
{
    public class ZuoFeiChaXunArgs : BaseArgs
    {
        public override ArgsType Type
        {
            get
            {
                return ArgsType.ZuoFeiChaXun;
            }
        }

        public string MathStr { get; set; }
        public bool YanQianShiBaiChecked { get; set; } = false;
    }
}
