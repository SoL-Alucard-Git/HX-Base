using Newtonsoft.Json;
using System.Data;

namespace InternetWare.Lodging.Data
{
    /// <summary>发票作废的先置查询结果</summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ZFCXResult : BaseResult
    {
        public DataTable DataTable { get; set; }

        public ZFCXResult() {; }

        public ZFCXResult(ZuoFeiChaXunArgs args, DataTable data, ErrorBase error = null) : base(args, error)
        {
            DataTable = data;
        }
    }
}
