namespace Aisino.Fwkp.HomePage.AisinoDock
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class GetIcon
    {
        public static Icon GetDirectoryIcon()
        {
            SHFILEINFO psfi = new SHFILEINFO();
            if (SHGetFileInfo("", 0, ref psfi, (uint) Marshal.SizeOf(psfi), 0x100).Equals(IntPtr.Zero))
            {
                return null;
            }
            return Icon.FromHandle(psfi.hIcon);
        }

        public static Icon GetFileIcon(string p_Path)
        {
            SHFILEINFO psfi = new SHFILEINFO();
            if (SHGetFileInfo(p_Path, 0, ref psfi, (uint) Marshal.SizeOf(psfi), 0x110).Equals(IntPtr.Zero))
            {
                return null;
            }
            return Icon.FromHandle(psfi.hIcon);
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
            public string szTypeName;
        }

        public enum SHGFI
        {
            SHGFI_ICON = 0x100,
            SHGFI_LARGEICON = 0,
            SHGFI_USEFILEATTRIBUTES = 0x10
        }
    }
}

