namespace Aisino.Fwkp.Sjbf.Common
{
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.IO;

    internal class AES_Crypt_File
    {
        private static byte[] key = new byte[] { 
            0xaf, 0x52, 0xde, 0x45, 15, 0x58, 0xcd, 0x10, 0x23, 0x8b, 0x45, 0x6a, 0x10, 0x90, 0xaf, 0xbd, 
            0xad, 0xdb, 0xae, 0x8d, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
         };
        private static string sysTempDir = Path.GetTempPath();
        private static byte[] vec = new byte[] { 2, 0xaf, 0xbc, 0xab, 0xcc, 0x90, 0x39, 0x90, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 };

        public static string DecryptFile(string inFileName)
        {
            string path = sysTempDir + Path.GetFileName(inFileName);
            try
            {
                FileStream stream = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                DateTime time = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                TimeSpan span = (TimeSpan) (DateTime.Now - time);
                byte[] buffer2 = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                    0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                    0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                 }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
                byte[] buffer3 = AES_Crypt.Decrypt(buffer, key, vec, buffer2);
                if (buffer3 == null)
                {
                    stream.Close();
                    return "";
                }
                FileStream stream2 = new FileStream(path, FileMode.Create, FileAccess.Write) {
                    Position = 0L
                };
                stream2.Write(buffer3, 0, buffer3.Length);
                stream.Close();
                stream2.Close();
            }
            catch
            {
                return "";
            }
            return path;
        }

        public static void EncryptFile(string inFileName)
        {
            FileStream stream = new FileStream(inFileName, FileMode.Open, FileAccess.ReadWrite);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            byte[] buffer2 = AES_Crypt.Encrypt(buffer, key, vec);
            stream.Position = 0L;
            stream.Write(buffer2, 0, buffer2.Length);
            stream.Close();
        }
    }
}

