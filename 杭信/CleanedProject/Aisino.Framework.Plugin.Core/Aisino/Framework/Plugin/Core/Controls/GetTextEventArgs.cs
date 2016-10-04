namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;

    public sealed class GetTextEventArgs : EventArgs
    {
        private readonly string string_0;

        public GetTextEventArgs(string string_1)
        {
            
            this.string_0 = string_1;
        }

        public string StartText
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

