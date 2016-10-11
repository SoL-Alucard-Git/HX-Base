using InternetWare.Lodging.Data;
using System;

namespace InternetWare.Lodging.Data
{
    public class BaseArgs : EventArgs
    {
        public string MessageId { get; private set; }

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
