using Newtonsoft.Json;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class WeiKaiChaXunArgs : BaseArgs
    {
        [JsonIgnore]
        public override ArgsType Type
        {
            get
            {
                return ArgsType.WeiKaiChaXun;
            }
        }

        /// <summary>发票类型</summary>
        public FaPiaoTypes FpType { get; set; }
    }
}
