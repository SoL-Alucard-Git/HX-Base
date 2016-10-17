using Newtonsoft.Json;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ZFResult : BaseResult
    {
        public int Total { get; set; }
        public int Succeed { get; set; }
        public int Failed { get; set; }

        public ZFResult(BaseArgs args, int total, int succeed, int failed, ErrorBase error = null) : base(args, error)
        {
            Total = total;
            Succeed = succeed;
            Failed = failed;
        }
    }
}
