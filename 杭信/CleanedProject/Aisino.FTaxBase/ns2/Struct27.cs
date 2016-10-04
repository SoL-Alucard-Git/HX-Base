namespace ns2
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct27
    {
        public uint uint_0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=20)]
        public char[] char_0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=20)]
        public char[] char_1;
    }
}

