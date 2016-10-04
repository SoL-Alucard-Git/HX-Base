namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    public interface ISignAPI
    {
        int CloseDevice(IntPtr intptr_0);
        int GetCertInfo(IntPtr intptr_0, CertInfo certInfo_0);
        int OpenDevice(ref IntPtr intptr_0, string string_0, string string_1, string string_2);
        int SignData(IntPtr intptr_0, string string_0, string string_1, out string string_2);
        int VerifySignedData(IntPtr intptr_0, string string_0, string string_1);
    }
}

