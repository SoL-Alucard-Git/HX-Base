namespace Aisino.Framework.Plugin.Core.Registry
{
    using System;
    using System.Runtime.InteropServices;

    public class Xihaa
    {
        public Xihaa()
        {
            
        }

        [DllImport("xihaa.dll")]
        public static extern uint abc(string string_0, string string_1, ushort ushort_0, string string_2, ref qwe qwe_0);
        [DllImport("MakeHashCode.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern void GenProKey(byte[] byte_0, byte[] byte_1);
        [DllImport("MakeHashCode.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern void GenTextRegKey(byte[] byte_0, byte[] byte_1, ref TextRegHead textRegHead_0);
        [DllImport("xihaa.dll")]
        public static extern uint MakeTransFile(string string_0, byte[] byte_0);
    }
}

