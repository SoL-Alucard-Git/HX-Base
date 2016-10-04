using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace VehCertI
{
    [ComImport, CompilerGenerated, InterfaceType((short)2), Guid("80F567C3-32F1-4290-9988-DEB3FCC7AC60"), TypeIdentifier]
    public interface __VCertificateI
    {
    }

    [ComImport, CompilerGenerated, ComEventInterface(typeof(__VCertificateI), typeof(__VCertificateI)),Guid("8800a4df-8331-416b-9fdf-20b0c79707d3") TypeIdentifier("8800a4df-8331-416b-9fdf-20b0c79707d3", "VehCertI.__VCertificateI_Event")]
    public interface __VCertificateI_Event
    {
    }

    [ComImport, CompilerGenerated, Guid("9C4D3A0D-E736-48AB-AF61-8B59F07A13D8"), TypeIdentifier]
    public interface _VCertificateI
    {
        [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        void _VtblGap1_492();
        [DispId(0x68030005)]
        string Connect { [return: MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x68030005)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x68030005)] set; }
        [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        void _VtblGap2_5();
        [DispId(0x68030001)]
        string BarcodeInfo { [return: MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x68030001)] get; }
        [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        void _VtblGap3_3();
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x60030010)]
        void ViewBarcodeInfoByWindow();
    }

    [ComImport, CompilerGenerated, CoClass(typeof(object)), Guid("9C4D3A0D-E736-48AB-AF61-8B59F07A13D8"), TypeIdentifier]
    public interface VCertificateI : _VCertificateI, __VCertificateI_Event
    {
    }
}
