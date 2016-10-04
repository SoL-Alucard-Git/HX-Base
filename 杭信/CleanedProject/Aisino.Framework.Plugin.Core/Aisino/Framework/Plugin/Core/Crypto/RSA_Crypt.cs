namespace Aisino.Framework.Plugin.Core.Crypto
{
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class RSA_Crypt
    {
        private static ILog ilog_0;
        private static RSACryptoServiceProvider rsacryptoServiceProvider_0;

        static RSA_Crypt()
        {
            
            ilog_0 = LogUtil.GetLogger<RSA_Crypt>();
            rsacryptoServiceProvider_0 = new RSACryptoServiceProvider();
        }

        public RSA_Crypt()
        {
            
        }

        public static bool CheckSignature_MD5(byte[] byte_0, byte[] byte_1)
        {
            try
            {
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(rsacryptoServiceProvider_0);
                deformatter.SetHashAlgorithm("MD5");
                return deformatter.VerifySignature(byte_0, byte_1);
            }
            catch (Exception exception)
            {
                ilog_0.Error("验证签名失败", exception);
                return false;
            }
        }

        public static bool CheckSignature_MD5(string string_0, byte[] byte_0, byte[] byte_1)
        {
            try
            {
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(string_0);
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("MD5");
                return deformatter.VerifySignature(byte_0, byte_1);
            }
            catch (Exception exception)
            {
                ilog_0.Error("指定公钥验证签名失败", exception);
                return false;
            }
        }

        public static byte[] Decrypt(string string_0)
        {
            return Decrypt(Convert.FromBase64String(string_0));
        }

        public static byte[] Decrypt(byte[] byte_0)
        {
            try
            {
                return rsacryptoServiceProvider_0.Decrypt(byte_0, false);
            }
            catch (Exception exception)
            {
                ilog_0.Error("数据解密错误", exception);
                return null;
            }
        }

        public static byte[] Decrypt(string string_0, string string_1)
        {
            byte[] buffer = Convert.FromBase64String(string_1);
            return Decrypt(string_0, buffer);
        }

        public static byte[] Decrypt(string string_0, byte[] byte_0)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(string_0);
                return provider.Decrypt(byte_0, false);
            }
            catch (Exception exception)
            {
                ilog_0.Error("指定密钥解密错误", exception);
                return null;
            }
        }

        public static byte[] Encrypt(string string_0)
        {
            return Encrypt(new UnicodeEncoding().GetBytes(string_0));
        }

        public static byte[] Encrypt(byte[] byte_0)
        {
            try
            {
                return rsacryptoServiceProvider_0.Encrypt(byte_0, false);
            }
            catch (Exception exception)
            {
                ilog_0.Error("数据加密错误", exception);
                return null;
            }
        }

        public static byte[] Encrypt(string string_0, string string_1)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(string_1);
            return Encrypt(string_0, bytes);
        }

        public static byte[] Encrypt(string string_0, byte[] byte_0)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(string_0);
                return provider.Encrypt(byte_0, false);
            }
            catch (Exception exception)
            {
                ilog_0.Error("指定密钥加密错误", exception);
                return null;
            }
        }

        public static byte[] Signature_MD5(byte[] byte_0)
        {
            try
            {
                RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(rsacryptoServiceProvider_0);
                formatter.SetHashAlgorithm("MD5");
                return formatter.CreateSignature(byte_0);
            }
            catch (Exception exception)
            {
                ilog_0.Error("数据签名失败", exception);
                return null;
            }
        }

        public static byte[] Signature_MD5(string string_0, byte[] byte_0)
        {
            try
            {
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(string_0);
                RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
                formatter.SetHashAlgorithm("MD5");
                return formatter.CreateSignature(byte_0);
            }
            catch (Exception exception)
            {
                ilog_0.Error("指定私钥数据签名失败", exception);
                return null;
            }
        }
    }
}

