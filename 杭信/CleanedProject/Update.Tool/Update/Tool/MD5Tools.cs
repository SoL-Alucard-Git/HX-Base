namespace Update.Tool
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class MD5Tools
    {
        public MD5Tools()
        {
           
        }

        public static string GetMd5(string input)
        {
            string str;
            new MD5CryptoServiceProvider();
            try
            {
                byte[] sourceArray = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
                char[] destinationArray = new char[sourceArray.Length];
                Array.Copy(sourceArray, destinationArray, sourceArray.Length);
                str = new string(destinationArray);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        public static string smethod_0(string pathName)
        {
            string str = "";
            FileStream inputStream = null;
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            try
            {
                inputStream = new FileStream(pathName.Replace("\"", ""), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                byte[] buffer = provider.ComputeHash(inputStream);
                inputStream.Close();
                str = BitConverter.ToString(buffer).Replace("-", "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        public static string smethod_1(string str)
        {
            byte[] buffer = MD5.Create().ComputeHash(Encoding.GetEncoding("UTF-8").GetBytes(str));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

