namespace Aisino.Framework.Plugin.Core.Http
{
    using System;

    public class UploadEventArgs : EventArgs
    {
        private int int_0;
        private int int_1;

        public UploadEventArgs()
        {
            
        }

        public int BytesSent
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public int TotalBytes
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }
    }
}

