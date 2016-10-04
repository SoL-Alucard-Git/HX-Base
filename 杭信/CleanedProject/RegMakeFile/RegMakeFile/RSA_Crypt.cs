namespace RegMakeFile
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class RSA_Crypt
    {
        private static RSACryptoServiceProvider RSA_Provider = new RSACryptoServiceProvider();

        public static bool CheckSignature_MD5(byte[] data, byte[] signatureData)
        {
            try
            {
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(RSA_Provider);
                deformatter.SetHashAlgorithm("MD5");
                return deformatter.VerifySignature(data, signatureData);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CheckSignature_MD5(string publicXmlKey, byte[] data, byte[] signatureData)
        {
            try
            {
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(publicXmlKey);
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("MD5");
                return deformatter.VerifySignature(data, signatureData);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static byte[] Decrypt(string inputBase64Str)
        {
            return Decrypt(Convert.FromBase64String(inputBase64Str));
        }

        public static byte[] Decrypt(byte[] inputByt)
        {
            try
            {
                return RSA_Provider.Decrypt(inputByt, false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static byte[] Decrypt(string xmlKey, string inputbase64Str)
        {
            byte[] inputByt = Convert.FromBase64String(inputbase64Str);
            return Decrypt(xmlKey, inputByt);
        }

        public static byte[] Decrypt(string xmlKey, byte[] inputByt)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlKey);
                return provider.Decrypt(inputByt, false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static byte[] Encrypt(string inputStr)
        {
            return Encrypt(new UnicodeEncoding().GetBytes(inputStr));
        }

        public static byte[] Encrypt(byte[] inputByt)
        {
            try
            {
                return RSA_Provider.Encrypt(inputByt, false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static byte[] Encrypt(string xmlKey, string inputStr)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(inputStr);
            return Encrypt(xmlKey, bytes);
        }

        public static byte[] Encrypt(string xmlKey, byte[] inputByt)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlKey);
                return provider.Encrypt(inputByt, false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static byte[] Signature_MD5(byte[] inputByt)
        {
            try
            {
                RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(RSA_Provider);
                formatter.SetHashAlgorithm("MD5");
                return formatter.CreateSignature(inputByt);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static byte[] Signature_MD5(string privateXmlKey, byte[] inputByt)
        {
            try
            {
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(privateXmlKey);
                RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
                formatter.SetHashAlgorithm("MD5");
                return formatter.CreateSignature(inputByt);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

