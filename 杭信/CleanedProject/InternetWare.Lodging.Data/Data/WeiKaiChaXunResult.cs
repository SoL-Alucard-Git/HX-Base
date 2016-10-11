using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetWare.Lodging.Data
{
    public class WeiKaiChaXunResult : ResultBase
    {
        /// <summary>发票种类名称</summary>
        public string Fpzl { get;private set; }
        /// <summary>发票类型代码</summary>
        public string Fpdm { get; private set; }
        /// <summary>起始发票编号</summary>
        public string InvNum { get; private set; }
        /// <summary>剩余发票张数</summary>
        public int FpHasNum { get; private set; }

        public WeiKaiChaXunResult(BaseArgs args, object data, bool hasError, ErrorBase error, string fpzl, string fpdm, string invNum,int fpHasNum) : base(args, data, hasError, error)
        {
            Fpzl = fpzl;
            Fpdm = fpdm;
            InvNum = invNum;
            FpHasNum = fpHasNum;
        }
    }
}
