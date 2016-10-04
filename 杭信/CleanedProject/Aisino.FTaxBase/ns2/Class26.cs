namespace ns2
{
    using System;
    using System.Runtime.InteropServices;

    internal static class Class26
    {
        public static int int_0;
        public static string string_0;

        static Class26()
        {
            
            string_0 = "123456";
        }

        [DllImport("USBKeyManager.dll")]
        internal static extern int Compress(byte[] byte_0, int int_1, byte[] byte_1);
        [DllImport("USBKeyManager.dll")]
        internal static extern int Expand(byte[] byte_0, int int_1, byte[] byte_1);
        [DllImport("USBKeyManager.dll")]
        internal static extern int GetInvStoreInfo(ref ulong ulong_0, ref int int_1, ref int int_2);
        [DllImport("USBKeyManager.dll")]
        internal static extern int GetKeyCheckInfo(string string_1);
        [DllImport("USBKeyManager.dll")]
        internal static extern int GetKeyUserInfo(byte[] byte_0, ref int int_1);
        [DllImport("USBKeyManager.dll")]
        internal static extern int GetKeyVersion(byte[] byte_0, ref int int_1, byte[] byte_1, ref int int_2);
        [DllImport("USBKeyManager.dll")]
        internal static extern int Login(string string_1, byte[] byte_0, ref int int_1, int int_2);
        [DllImport("USBKeyManager.dll")]
        internal static extern int Logout();
        [DllImport("USBKeyManager.dll")]
        internal static extern int ReadBSInvoice(byte[] byte_0, ref int int_1, byte byte_1, byte[] byte_2, ref int int_2, ref ulong ulong_0, ref int int_3);
        [DllImport("USBKeyManager.dll")]
        internal static extern int ReadInvoice(string string_1, ref int int_1, byte byte_0, byte[] byte_1, ref int int_2, ref ulong ulong_0, ref long long_0);
        [DllImport("USBKeyManager.dll")]
        internal static extern int ReadTaxInfo(byte[] byte_0, ref int int_1);
        [DllImport("USBKeyManager.dll")]
        internal static extern int ReadTaxReturnFiles(byte byte_0, string string_1);
        [DllImport("USBKeyManager.dll")]
        internal static extern int SetInvAttrs(string string_1, long long_0);
        internal static int smethod_0()
        {
            int num = 20;
            byte[] buffer = new byte[20];
            return Login(string_0, buffer, ref num, int_0);
        }

        [DllImport("USBKeyManager.dll")]
        internal static extern int WriteInvoice(string string_1, byte[] byte_0, byte[] byte_1, int int_1, ref ulong ulong_0, ref int int_2);
        [DllImport("USBKeyManager.dll")]
        internal static extern int WriteTaxInfo(string string_1);
        [DllImport("USBKeyManager.dll")]
        internal static extern int WriteTaxReturnFile(string string_1, string string_2);
    }
}

