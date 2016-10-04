using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IWshRuntimeLibrary
{
    [ComImport, CompilerGenerated, TypeIdentifier, Guid("F935DC21-1CF0-11D0-ADB9-00C04FD58A0B")]
    public interface IWshShell
    {
    }

    [ComImport, Guid("24BE5A30-EDFE-11D2-B933-00104B365C9F"), TypeIdentifier, CompilerGenerated]
    public interface IWshShell2 : IWshShell
    {
    }

    [ComImport, CompilerGenerated, TypeIdentifier, Guid("41904400-BE18-11D3-A28B-00104BD35090")]
    public interface IWshShell3 : IWshShell2, IWshShell
    {
        void _VtblGap1_4();
        [return: MarshalAs(UnmanagedType.IDispatch)]
        [DispId(0x3ea)]
        object CreateShortcut([In, MarshalAs(UnmanagedType.BStr)] string PathLink);
    }

    [ComImport, Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B"), DefaultMember("FullName"), TypeIdentifier, CompilerGenerated]
    public interface IWshShortcut
    {
        void _VtblGap1_1();
        string Arguments { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3e8)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3e8)] set; }
        void _VtblGap2_7();
        string TargetPath { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ed)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ed)] set; }
        int WindowStyle { [DispId(0x3ee)] get; [param: In] [DispId(0x3ee)] set; }
        void _VtblGap3_3();
        [DispId(0x7d1)]
        void Save();
    }

    [ComImport, TypeIdentifier, CoClass(typeof(object)), CompilerGenerated, Guid("41904400-BE18-11D3-A28B-00104BD35090")]
    public interface WshShell : IWshShell3, IWshShell2, IWshShell
    {
    }
}


