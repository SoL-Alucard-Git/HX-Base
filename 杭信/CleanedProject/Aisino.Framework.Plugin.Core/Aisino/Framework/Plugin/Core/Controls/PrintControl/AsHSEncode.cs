namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Runtime.InteropServices;

    public class AsHSEncode
    {
        public AsHSEncode()
        {
            
        }

        [DllImport("AsHSEncode.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern bool HS_CreateJPEGFile(ref ASHS_CODEINFO ashs_CODEINFO_0);
        [DllImport("AsHSEncode.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr HS_CreateJPEGHandle(ref ASHS_CODEINFO ashs_CODEINFO_0);
        [DllImport("AsHSEncode.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern bool HS_DrawHSCode(IntPtr intptr_0, ref ASHS_CODEINFO ashs_CODEINFO_0);
        [DllImport("AsHSEncode.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern bool HS_DrawLine(IntPtr intptr_0, ref ASHS_CODEINFO ashs_CODEINFO_0);
        [DllImport("AsHSEncode.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int HS_GetHSCodeVersion(byte byte_0, long long_0, int int_0, int int_1);
        [DllImport("AsHSEncode.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern uint HS_GetLastError();
        [DllImport("AsHSEncode.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern bool HS_PrintHSCode(ref ASHS_CODEINFO ashs_CODEINFO_0);

        [StructLayout(LayoutKind.Sequential)]
        public struct ASHS_CODEINFO
        {
            public IntPtr pData1;
            public IntPtr pData2;
            public IntPtr pData3;
            public IntPtr pData4;
            public int dwDataLength1;
            public int dwDataLength2;
            public int dwDataLength3;
            public int dwDataLength4;
            public int nEccelvel;
            public int nVersion;
            public int int_0;
            public int nNarrow;
            public int nadjBwc;
            public int nPageLeft;
            public int nPageTop;
            public int nPosX;
            public int nPosY;
            public int nSymbolSpace;
            public int nPreviewScale;
            [MarshalAs(UnmanagedType.LPStr)]
            public string szPrinterName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string szJPEGFilePathName;
        }
    }
}

