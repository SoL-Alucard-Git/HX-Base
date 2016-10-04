namespace Aisino.Framework.Plugin.Core.Crypto
{
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;

    public class AES_Crypt
    {
        private static ILog ilog_0;

        static AES_Crypt()
        {
            
            ilog_0 = LogUtil.GetLogger<AES_Crypt>();
        }

        public AES_Crypt()
        {
            
        }

        public static byte[] Decrypt(byte[] byte_0, byte[] byte_1, byte[] byte_2, byte[] byte_3 = null)
        {
            byte[] buffer = null;
            Aes aes = Aes.Create();
            try
            {
                if (byte_0 == null)
                {
                    return null;
                }
                if (((byte_1[0] == 0xaf) && (byte_1[1] == 0x52)) && ((byte_2[0] == 2) && (byte_2[1] == 0xaf)))
                {
                    if (byte_3 == null)
                    {
                        return byte_0;
                    }
                    DateTime time = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                    TimeSpan span = (TimeSpan) (DateTime.Now - time);
                    double totalSeconds = span.TotalSeconds;
                    byte[] buffer3 = Decrypt(byte_3, new byte[] { 
                        0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                        0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                     }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 }, null);
                    double result = 0.0;
                    double.TryParse(ToolUtil.GetString(buffer3), out result);
                    double num3 = Math.Abs((double) (totalSeconds - result));
                    if (num3 > 2.0)
                    {
                        ilog_0.ErrorFormat("数据处理超时：{0}", num3);
                        return byte_0;
                    }
                }
                using (MemoryStream stream = new MemoryStream(byte_0))
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, aes.CreateDecryptor(byte_1, byte_2), CryptoStreamMode.Read))
                    {
                        using (MemoryStream stream3 = new MemoryStream())
                        {
                            byte[] buffer4 = new byte[0x400];
                            int count = 0;
                            while ((count = stream2.Read(buffer4, 0, buffer4.Length)) > 0)
                            {
                                stream3.Write(buffer4, 0, count);
                            }
                            return stream3.ToArray();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                buffer = null;
                ilog_0.Error("数据处理异常2：" + exception.ToString());
                ilog_0.ErrorFormat("{0},{1},{2}", Convert.ToBase64String(byte_0), Convert.ToBase64String(byte_1), Convert.ToBase64String(byte_2));
            }
            return buffer;
        }

        public static byte[] Encrypt(byte[] byte_0, byte[] byte_1, byte[] byte_2)
        {
            byte[] buffer = null;
            Aes aes = Aes.Create();
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, aes.CreateEncryptor(byte_1, byte_2), CryptoStreamMode.Write))
                    {
                        stream2.Write(byte_0, 0, byte_0.Length);
                        stream2.FlushFinalBlock();
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception exception)
            {
                buffer = null;
                ilog_0.Error("数据处理异常1：" + exception.ToString());
            }
            return buffer;
        }
    }
}

