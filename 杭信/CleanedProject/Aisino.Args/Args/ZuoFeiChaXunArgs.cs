using System;
using Newtonsoft.Json;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ZuoFeiChaXunArgs : BaseArgs
    {
        [JsonIgnore]
        public override ArgsType Type
        {
            get
            {
                return ArgsType.ZuoFeiChaXun;
            }
        }

        public string MathStr { get; set; }
        public bool YanQianShiBaiChecked { get; set; } = false;
    }
}
