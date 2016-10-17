namespace InternetWare.Lodging.Data
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class GetKHArgs : BaseArgs
    {
        public override ArgsType Type
        {
            get
            {
                return ArgsType.GetKH;
            }
        }
    }
}
