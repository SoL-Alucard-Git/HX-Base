using Newtonsoft.Json;
using System;

namespace InternetWare.Lodging.Data
{

    [JsonObject(MemberSerialization.OptOut)]
    public class BaseArgs : EventArgs
    {
        
        public string MessageId { get; set; } = "";

        [JsonIgnore]
        public virtual ArgsType Type
        {
            get { return ArgsType.DefaultValue; }
        }

        public BaseArgs()
        {
            MessageId = string.Empty;
        }

        public BaseArgs(string messageId)
        {
            MessageId = messageId;
        }
    }
}
