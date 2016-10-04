namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    public class WebInvStockAPI
    {
        public WebInvStockAPI()
        {
            
        }

        [DllImport("JsDevInfoDll.dll")]
        public static extern int jsk_operate_r(byte byte_0, byte byte_1, short short_0, string string_0, byte[] byte_2, ushort ushort_0, byte[] byte_3, ref ushort ushort_1);
    }
}

