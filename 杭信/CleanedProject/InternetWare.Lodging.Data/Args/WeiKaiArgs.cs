using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetWare.Lodging.Data
{
    public class WeiKaiArgs : BaseArgs
    {
        public override ArgsType Type
        {
            get
            {
                return ArgsType.WeiKai;
            }
        }

        /// <summary>作废张数</summary>
        public int Count { get; set; }
        /// <summary>发票类型</summary>
        public FaPiaoTypes FpType { get; set; }
    }
}
