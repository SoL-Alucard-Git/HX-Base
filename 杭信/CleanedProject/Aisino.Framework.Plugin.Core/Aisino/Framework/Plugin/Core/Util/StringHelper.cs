namespace Aisino.Framework.Plugin.Core.Util
{
    using System;
    using System.Text;

    public class StringHelper
    {
        public StringHelper()
        {
            
        }

        public static string arr2str(byte[] byte_0)
        {
            return new UnicodeEncoding().GetString(byte_0, 0, byte_0.Length);
        }

        public static string hexascarr2str(byte[] byte_0)
        {
            return smethod_5(smethod_3(byte_0, byte_0.Length));
        }

        public static void smethod_0(byte[] byte_0, byte[] byte_1, int int_0)
        {
            for (int i = 0; i < (int_0 / 2); i++)
            {
                if (byte_0[2 * i] < 0x41)
                {
                    byte_1[i] = Convert.ToByte((int) (((byte_0[2 * i] - 0x30) << 4) & 240));
                }
                else
                {
                    byte_1[i] = Convert.ToByte((int) (((byte_0[2 * i] - 0x37) << 4) & 240));
                }
                if (byte_0[(2 * i) + 1] < 0x41)
                {
                    byte_1[i] = (byte) (byte_1[i] | Convert.ToByte((int) ((byte_0[(2 * i) + 1] - 0x30) & 15)));
                }
                else
                {
                    byte_1[i] = (byte) (byte_1[i] | Convert.ToByte((int) ((byte_0[(2 * i) + 1] - 0x37) & 15)));
                }
            }
        }

        public static byte[] smethod_1(byte[] byte_0, int int_0)
        {
            if ((int_0 % 2) != 0)
            {
                return null;
            }
            byte[] buffer = new byte[int_0 / 2];
            smethod_0(byte_0, buffer, int_0);
            return buffer;
        }

        public static void smethod_2(byte[] byte_0, byte[] byte_1, int int_0)
        {
            for (int i = 0; i < int_0; i++)
            {
                byte num2 = Convert.ToByte((int) ((byte_0[i] >> 4) & 15));
                if (num2 < 10)
                {
                    byte_1[2 * i] = Convert.ToByte((int) (num2 + 0x30));
                }
                else
                {
                    byte_1[2 * i] = Convert.ToByte((int) (num2 + 0x37));
                }
                num2 = Convert.ToByte((int) (byte_0[i] & 15));
                if (num2 < 10)
                {
                    byte_1[(2 * i) + 1] = Convert.ToByte((int) (num2 + 0x30));
                }
                else
                {
                    byte_1[(2 * i) + 1] = Convert.ToByte((int) (num2 + 0x37));
                }
            }
        }

        public static byte[] smethod_3(byte[] byte_0, int int_0)
        {
            byte[] buffer = new byte[int_0 * 2];
            smethod_2(byte_0, buffer, int_0);
            return buffer;
        }

        public static byte[] smethod_4(string string_0)
        {
            return Encoding.Convert(Encoding.Unicode, Encoding.ASCII, str2arr(string_0));
        }

        public static string smethod_5(byte[] byte_0)
        {
            return Encoding.Unicode.GetString(Encoding.Convert(Encoding.ASCII, Encoding.Unicode, byte_0));
        }

        public static byte[] str2arr(string string_0)
        {
            return new UnicodeEncoding().GetBytes(string_0);
        }

        public static byte[] str2hexascarr(string string_0)
        {
            byte[] buffer = smethod_4(string_0);
            return smethod_3(buffer, buffer.Length);
        }
    }
}

