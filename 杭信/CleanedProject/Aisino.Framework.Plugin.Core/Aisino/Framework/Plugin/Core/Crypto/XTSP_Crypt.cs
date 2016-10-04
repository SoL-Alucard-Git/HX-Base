namespace Aisino.Framework.Plugin.Core.Crypto
{
    using System;
    using System.Text;

    public class XTSP_Crypt
    {
        public XTSP_Crypt()
        {
            
        }

        public static string EncodeXTGoodsName(string string_0)
        {
            byte[] buffer = smethod_0(string_0, 8);
            int length = buffer.Length;
            byte[] buffer2 = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                buffer2[i] = 0;
            }
            byte[] buffer3 = new byte[8];
            IDEAXT ideaxt = new IDEAXT();
            byte[] bytes = Encoding.Default.GetBytes("9781246350HQSTAR");
            ideaxt.encrypt_subkey(bytes);
            byte[] buffer5 = new byte[0x10];
            for (int j = 0; j < (length / 8); j++)
            {
                byte[] buffer6 = new byte[8];
                for (int m = 0; m < 8; m++)
                {
                    buffer6[m] = buffer[(j * 8) + m];
                }
                ideaxt.encrypt(buffer6, buffer3);
                for (int n = 0; n < 8; n++)
                {
                    buffer2[n] = (byte) (buffer2[n] + buffer3[n]);
                }
            }
            for (int k = 0; k < 8; k++)
            {
                byte num6 = Convert.ToByte((int) (Convert.ToByte('A') + (buffer2[k] & 15)));
                buffer5[2 * k] = num6;
                num6 = Convert.ToByte((int) (Convert.ToByte('A') + (buffer2[k] >> 4)));
                buffer5[(2 * k) + 1] = num6;
            }
            return Encoding.Default.GetString(buffer5);
        }

        private static byte[] smethod_0(string string_0, int int_0)
        {
            int num4;
            int length = string_0.Length;
            if ((length % int_0) == 0)
            {
                num4 = 0;
            }
            else
            {
                num4 = int_0 - (length % int_0);
            }
            int num3 = length + num4;
            byte[] buffer = new byte[num3];
            byte[] bytes = Encoding.Default.GetBytes(string_0);
            for (int i = 0; i < string_0.Length; i++)
            {
                buffer[i] = bytes[i];
            }
            for (int j = length; j < num3; j++)
            {
                buffer[j] = (byte) (buffer[j] + Convert.ToByte(' '));
            }
            return buffer;
        }
    }
}

