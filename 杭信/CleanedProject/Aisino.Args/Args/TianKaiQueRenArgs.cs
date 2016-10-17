using Newtonsoft.Json;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TianKaiQueRenArgs : BaseArgs
    {
        [JsonIgnore]
        public override ArgsType Type
        {
            get
            {
                return ArgsType.TianKaiQueRen;
            }
        }

        public FaPiaoTypes FpType { get; set; }
    }
}
