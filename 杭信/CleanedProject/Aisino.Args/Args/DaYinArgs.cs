using Newtonsoft.Json;

namespace InternetWare.Lodging.Data
{

    [JsonObject(MemberSerialization.OptOut)]
    public class DaYinArgs : BaseArgs
    {
        [JsonIgnore]
        public override ArgsType Type
        {
            get
            {
                return ArgsType.DaYin;
            }
        }

        public string FPZL { get; set; }
        public string FPDM { get; set; }
        public int FPHM { get; set; }
        public string Printer { get; set; }
    }
}
