namespace Aisino.Fwkp.Fptk.Form
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;

    internal class DigitalEnvelop
    {
        public static T Clone<T>(T RealObject)
        {
            using (Stream stream = new MemoryStream())
            {
                BinaryFormatter formatter1 = new BinaryFormatter();
                formatter1.Serialize(stream, RealObject);
                stream.Seek(0L, SeekOrigin.Begin);
                return (T) formatter1.Deserialize(stream);
            }
        }

        [DllImport("DigitalEnvelop.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int DigEnvClose();
        [DllImport("DigitalEnvelop.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int DigEnvInit(bool vLocalCert, bool vTargetCert, bool vTargetCertAccess);
        [DllImport("DigitalEnvelop.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int GetErrInfo([Out] char[] err, ref int errLen);
        [DllImport("DigitalEnvelop.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int GetExt1(string base64Cert, string tag, [Out] byte[] context, ref int contextLen);
        [DllImport("MakeHashCode.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int getHashNsh(byte[] taxcode, byte[] path);
        [DllImport("DigitalEnvelop.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int Pack(ref byte[] cert, int certLen, ref byte[] dataIn, int dataInLen, ref byte[] dataOut, ref int dataOutLen);
        [DllImport("DigitalEnvelop.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int SetAccessByPfx(string pfxFile, string pwd);
        [DllImport("DigitalEnvelop.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int SetCaCertAndCrlByPfx(string pfxFile, string pwd, string crlFile);
        [DllImport("DigitalEnvelop.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int SetPrivateKeyAndCertByPfx(string pfxFile, string pwd);
        [DllImport("DigitalEnvelop.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int Unpack(byte[] dataIn, int dataInLen, [Out] byte[] cert, ref int certLen, [Out] byte[] dataOut, ref int dataOutLen);
    }
}

