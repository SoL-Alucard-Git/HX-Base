namespace Aisino.Framework.Plugin.Core.Registry
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TextRegHead
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=15)]
        public byte[] Tax_Mw_No;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
        public byte[] Date;
        public ushort MachinNo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x18)]
        public byte[] buf;
    }
}

