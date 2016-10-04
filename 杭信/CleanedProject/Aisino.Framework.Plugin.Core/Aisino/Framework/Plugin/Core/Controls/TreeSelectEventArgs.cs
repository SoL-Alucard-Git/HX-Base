namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;

    public sealed class TreeSelectEventArgs : EventArgs
    {
        private readonly string string_0;

        public TreeSelectEventArgs(string string_1)
        {
            
            this.string_0 = string_1;
        }

        public string BmString
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

