namespace Aisino.Framework.Plugin.Core.ExcelXml.Extensions
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class EncryptDecrypt
    {
        public static string Decrypt(string string_0, string string_1)
        {
            byte[] buffer = Convert.FromBase64String(string_0);
            PasswordDeriveBytes bytes = new PasswordDeriveBytes(string_1, new byte[] { 0x49, 0x76, 0x61, 110, 0x20, 0x4d, 0x65, 100, 0x76, 0x65, 100, 0x65, 0x76 });
            byte[] buffer2 = Decrypt(buffer, bytes.GetBytes(0x20), bytes.GetBytes(0x10));
            return Encoding.Unicode.GetString(buffer2);
        }

        public static byte[] Decrypt(byte[] byte_0, string string_0)
        {
            PasswordDeriveBytes bytes = new PasswordDeriveBytes(string_0, new byte[] { 0x49, 0x76, 0x61, 110, 0x20, 0x4d, 0x65, 100, 0x76, 0x65, 100, 0x65, 0x76 });
            return Decrypt(byte_0, bytes.GetBytes(0x20), bytes.GetBytes(0x10));
        }

        public static void Decrypt(string string_0, string string_1, string string_2)
        {
            int num2;
            FileStream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
            FileStream stream2 = new FileStream(string_1, FileMode.OpenOrCreate, FileAccess.Write);
            PasswordDeriveBytes bytes = new PasswordDeriveBytes(string_2, new byte[] { 0x49, 0x76, 0x61, 110, 0x20, 0x4d, 0x65, 100, 0x76, 0x65, 100, 0x65, 0x76 });
            Aes aes = Aes.Create();
            aes.Key = bytes.GetBytes(0x20);
            aes.IV = bytes.GetBytes(0x10);
            CryptoStream stream3 = new CryptoStream(stream2, aes.CreateDecryptor(), CryptoStreamMode.Write);
            int count = 0x1000;
            byte[] buffer = new byte[0x1000];
            do
            {
                num2 = stream.Read(buffer, 0, count);
                stream3.Write(buffer, 0, num2);
            }
            while (num2 != 0);
            stream3.Close();
            stream.Close();
        }

        public static byte[] Decrypt(byte[] byte_0, byte[] byte_1, byte[] byte_2)
        {
            MemoryStream stream = new MemoryStream();
            Aes aes = Aes.Create();
            aes.Key = byte_1;
            aes.IV = byte_2;
            CryptoStream stream2 = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(byte_0, 0, byte_0.Length);
            stream2.Close();
            return stream.ToArray();
        }

        public static string Encrypt(string string_0, string string_1)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(string_0);
            PasswordDeriveBytes bytes = new PasswordDeriveBytes(string_1, new byte[] { 0x49, 0x76, 0x61, 110, 0x20, 0x4d, 0x65, 100, 0x76, 0x65, 100, 0x65, 0x76 });
            return Convert.ToBase64String(Encrypt(buffer, bytes.GetBytes(0x20), bytes.GetBytes(0x10)));
        }

        public static byte[] Encrypt(byte[] byte_0, string string_0)
        {
            PasswordDeriveBytes bytes = new PasswordDeriveBytes(string_0, new byte[] { 0x49, 0x76, 0x61, 110, 0x20, 0x4d, 0x65, 100, 0x76, 0x65, 100, 0x65, 0x76 });
            return Encrypt(byte_0, bytes.GetBytes(0x20), bytes.GetBytes(0x10));
        }

        public static void Encrypt(string string_0, string string_1, string string_2)
        {
            int num2;
            FileStream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
            FileStream stream2 = new FileStream(string_1, FileMode.OpenOrCreate, FileAccess.Write);
            PasswordDeriveBytes bytes = new PasswordDeriveBytes(string_2, new byte[] { 0x49, 0x76, 0x61, 110, 0x20, 0x4d, 0x65, 100, 0x76, 0x65, 100, 0x65, 0x76 });
            Aes aes = Aes.Create();
            aes.Key = bytes.GetBytes(0x20);
            aes.IV = bytes.GetBytes(0x10);
            CryptoStream stream3 = new CryptoStream(stream2, aes.CreateEncryptor(), CryptoStreamMode.Write);
            int count = 0x1000;
            byte[] buffer = new byte[0x1000];
            do
            {
                num2 = stream.Read(buffer, 0, count);
                stream3.Write(buffer, 0, num2);
            }
            while (num2 != 0);
            stream3.Close();
            stream.Close();
        }

        public static byte[] Encrypt(byte[] byte_0, byte[] byte_1, byte[] byte_2)
        {
            MemoryStream stream = new MemoryStream();
            Aes aes = Aes.Create();
            aes.Key = byte_1;
            aes.IV = byte_2;
            CryptoStream stream2 = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(byte_0, 0, byte_0.Length);
            stream2.Close();
            return stream.ToArray();
        }
    }
}

