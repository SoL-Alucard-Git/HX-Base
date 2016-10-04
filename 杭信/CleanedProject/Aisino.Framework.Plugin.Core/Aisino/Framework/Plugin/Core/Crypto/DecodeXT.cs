namespace Aisino.Framework.Plugin.Core.Crypto
{
    using System;
    using System.IO;
    using System.Text;

    public class DecodeXT
    {
        public DecodeXT()
        {
            
        }

        public static string DecodeXTFile(string string_0)
        {
            byte[] buffer = new byte[8];
            IDEAXT ideaxt = new IDEAXT();
            byte[] bytes = Encoding.Default.GetBytes("9781246350HQSTAR");
            ideaxt.encrypt_subkey(bytes);
            ideaxt.uncrypt_subkey();
            string str = "";
            try
            {
                int count = 0;
                FileStream stream = new FileStream(string_0, FileMode.Open);
                count = (int) stream.Length;
                byte[] buffer3 = new byte[count];
                byte[] buffer4 = new byte[count];
                stream.Read(buffer3, 0, count);
                for (int i = 0; i < (count / 8); i++)
                {
                    byte[] destinationArray = new byte[8];
                    Array.Copy(buffer3, i * 8, destinationArray, 0, 8);
                    ideaxt.encrypt(destinationArray, buffer);
                    for (int j = 0; j < 8; j++)
                    {
                        buffer4[(8 * i) + j] = buffer[j];
                    }
                }
                stream.Close();
                str = Encoding.Default.GetString(buffer4);
                int num4 = str.LastIndexOf("</body>");
                return str.Substring(0, num4 + 7);
            }
            catch
            {
                return "";
            }
        }
    }
}

