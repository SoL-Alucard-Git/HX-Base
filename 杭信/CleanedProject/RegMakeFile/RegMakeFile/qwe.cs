namespace RegMakeFile
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct qwe
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=15)]
        public char[] TaxCode;
        public ushort BranchNo;
        public ushort SoftwareType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=6)]
        public char[] SoftwareID;
        public uint SerialNo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
        public char[] StopDate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x10)]
        public byte[] Verify;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x10)]
        public byte[] Transfer;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x10)]
        public byte[] Disturber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x10)]
        public byte[] ProcKey;
    }
}

