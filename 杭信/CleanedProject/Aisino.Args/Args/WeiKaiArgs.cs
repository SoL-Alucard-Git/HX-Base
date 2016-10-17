using Newtonsoft.Json;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class WeiKaiArgs : BaseArgs
    {
        [JsonIgnore]
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
