using Newtonsoft.Json;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class WeiKaiChaXunResult : BaseResult
    {
        /// <summary>发票种类名称</summary>
        public string Fpzl { get;set; }
        /// <summary>发票类型代码</summary>
        public string Fpdm { get; set; }
        /// <summary>起始发票编号</summary>
        public string InvNum { get; set; }
        /// <summary>剩余发票张数</summary>
        public int FpHasNum { get; set; }

        public WeiKaiChaXunResult(BaseArgs args, string fpzl, string fpdm, string invNum,int fpHasNum, ErrorBase error = null) : base(args, error)
        {
            Fpzl = fpzl;
            Fpdm = fpdm;
            InvNum = invNum;
            FpHasNum = fpHasNum;
        }

        public WeiKaiChaXunResult()
        {
        }
    }
}
