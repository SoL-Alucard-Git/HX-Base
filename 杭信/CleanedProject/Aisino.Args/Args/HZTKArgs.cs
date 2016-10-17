using Newtonsoft.Json;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class HZTKArgs : BaseArgs
    {
        [JsonIgnore]
        public override ArgsType Type
        {
            get
            {
                return ArgsType.HongZi;
            }
        }

        public HZType hztype { get; set; }
    }

    public enum HZType
    {
        GFYDK,
        GFWDK,
        XF
    }
}
