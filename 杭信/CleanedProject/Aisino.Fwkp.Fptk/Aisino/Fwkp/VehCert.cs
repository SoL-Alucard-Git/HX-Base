using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VCertificate
{
    [ComImport, CompilerGenerated, CoClass(typeof(object)), Guid("C5835054-3ED0-4748-84AB-8837E638A58D"), TypeIdentifier]
    public interface VehCert : _VehCert, __VehCert_Event
    {
    }

    [ComImport, CompilerGenerated, InterfaceType((short)2), Guid("572EF376-18E0-44FE-84E5-03D912364AE0"), TypeIdentifier]
    public interface __VehCert
    {
    }

    [ComImport, CompilerGenerated, ComEventInterface(typeof(__VehCert), typeof(__VehCert)), Guid("34b89858-39aa-4c68-9714-8cf0b4ab766a"), TypeIdentifier("34b89858-39aa-4c68-9714-8cf0b4ab766a","VCertificate.__VehCert_Event")]
    public interface __VehCert_Event
    {
    }

    [ComImport, CompilerGenerated, Guid("C5835054-3ED0-4748-84AB-8837E638A58D"), TypeIdentifier]
    public interface _VehCert
    {
        [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        void _VtblGap1_584();
        [DispId(0x6803000e)]
        string Veh_Tmxx { [return: MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x6803000e)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x6803000e)] set; }
        [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        void _VtblGap2_9();
        [DispId(0x68030007)]
        string Veh_Connect { [return: MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x68030007)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x68030007)] set; }
        [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        void _VtblGap3_16();
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x6003004a)]
        void ViewBarcodeInfo([In, MarshalAs(UnmanagedType.BStr)] string sKey);
    }
}
