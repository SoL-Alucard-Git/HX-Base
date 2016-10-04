namespace RegMakeFile
{
    using System;
    using System.Runtime.InteropServices;

    public class Xihaa
    {
        [DllImport("xihaa.dll")]
        public static extern uint abc(string fileName, string taxCode, ushort branchNo, string date, ref qwe qw);
        [DllImport("xihaa.dll")]
        public static extern uint MakeTransFile(string fileName, byte[] transfer);
    }
}

