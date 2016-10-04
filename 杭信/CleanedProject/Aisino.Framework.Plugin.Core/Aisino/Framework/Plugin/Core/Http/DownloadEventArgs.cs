namespace Aisino.Framework.Plugin.Core.Http
{
    using System;

    public class DownloadEventArgs : EventArgs
    {
        private byte[] byte_0;
        private int int_0;
        private int int_1;

        public DownloadEventArgs()
        {
            
        }

        public int BytesReceived
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

        public byte[] ReceivedData
        {
            get
            {
                return this.byte_0;
            }
            set
            {
                this.byte_0 = value;
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

