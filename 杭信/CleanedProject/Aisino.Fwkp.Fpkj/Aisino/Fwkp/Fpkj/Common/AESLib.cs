using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AESLib
{
    [ComImport, TypeIdentifier, CompilerGenerated, Guid("75E65591-8C7B-4BAB-998A-E69B58613746")]
    public interface _Encryptor
    {
        [return: MarshalAs(UnmanagedType.BStr)]
        [DispId(0x60030000)]
        string CryptEncrypt([In, Out, MarshalAs(UnmanagedType.BStr)] ref string inString);
    }

    [ComImport, CompilerGenerated, TypeIdentifier, CoClass(typeof(object)), Guid("75E65591-8C7B-4BAB-998A-E69B58613746")]
    public interface Encryptor : _Encryptor
    {
    }
}
