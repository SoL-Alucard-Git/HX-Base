namespace Aisino.Framework.Plugin.Core.Crypto
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class MD5_Crypt
    {
        public MD5_Crypt()
        {
            
        }

        public static byte[] GetHash(byte[] byte_0)
        {
            return HashAlgorithm.Create("MD5").ComputeHash(byte_0);
        }

        public static byte[] GetHash(string string_0)
        {
            return GetHash(new UnicodeEncoding().GetBytes(string_0));
        }

        public static string GetHashStr(string string_0)
        {
            return Convert.ToBase64String(GetHash(string_0));
        }

        public static string GetHashString(byte[] byte_0)
        {
            return BitConverter.ToString(GetHash(byte_0)).Replace("-", "");
        }

        public static string GetHashString32(string string_0)
        {
            return BitConverter.ToString(GetHash(string_0)).Replace("-", "");
        }
    }
}

