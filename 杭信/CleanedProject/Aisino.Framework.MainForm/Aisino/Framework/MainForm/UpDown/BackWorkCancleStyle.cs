namespace Aisino.Framework.MainForm.UpDown
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct BackWorkCancleStyle
    {
        public CancleStyle CanclType;
        public object UserData;
    }
}

