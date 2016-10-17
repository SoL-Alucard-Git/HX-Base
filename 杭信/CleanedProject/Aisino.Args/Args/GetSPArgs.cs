namespace InternetWare.Lodging.Data
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class GetSPArgs : BaseArgs
    {
        public override ArgsType Type
        {
            get
            {
                return ArgsType.GetSP;
            }
        }
    }
}
