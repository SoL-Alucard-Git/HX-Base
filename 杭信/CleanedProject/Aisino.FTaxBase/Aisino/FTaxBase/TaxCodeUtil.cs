namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class TaxCodeUtil
    {
        private static TaxCodeUtil taxCodeUtil_0;

        private TaxCodeUtil()
        {
            
        }

        public string CompressTaxCode(string string_0)
        {
            string str = "";
            int length = string_0.Trim().Length;
            if (length == 15)
            {
                return string_0;
            }
            str = string_0.Substring(0, 6);
            byte[] buffer2 = new byte[8];
            this.method_1(buffer2, string_0, length);
            byte[] buffer = new byte[5];
            for (int i = 0; i < 5; i++)
            {
                buffer[i] = buffer2[3 + i];
            }
            str = str + this.method_0(buffer);
            switch (length)
            {
                case 0x11:
                    return (str + "V");

                case 0x12:
                    return (str + "W");

                case 0x13:
                    return str;

                case 20:
                    return (str + "Y");
            }
            return str;
        }

        public string CompressTaxCode(string string_0, string string_1)
        {
            try
            {
                byte[] array = new byte[30];
                Encoding.GetEncoding("GBK").GetBytes(string_0).CopyTo(array, 0);
                byte[] bytes = Encoding.GetEncoding("GBK").GetBytes(string_1);
                if (getHashNsh(array, bytes) == 0)
                {
                    return Encoding.GetEncoding("GBK").GetString(array).Trim(new char[1]).Substring(0, 15);
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static TaxCodeUtil CreateInstance()
        {
            if (taxCodeUtil_0 == null)
            {
                taxCodeUtil_0 = new TaxCodeUtil();
            }
            return taxCodeUtil_0;
        }

        [DllImport("MakeHashCode.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int getHashNsh(byte[] byte_0, byte[] byte_1);
        private string method_0(byte[] byte_0)
        {
            char[] chArray = "0123456789ABCDEFGHJKLMNPQRTUVWXY".ToCharArray();
            char[] chArray2 = new char[8];
            int num = 8;
            int num2 = 0;
            int index = 0;
            int num4 = 0;
            for (int i = 0; i < 8; i++)
            {
                int num6 = ((byte_0[index] << num) + (byte_0[num4] >> num2)) & 0x1f;
                chArray2[i] = chArray[num6];
                num2 += 5;
                if (num2 > 8)
                {
                    num2 -= 8;
                    num4++;
                }
                else
                {
                    index++;
                }
                num = 8 - num2;
                if (num == 4)
                {
                    index++;
                }
            }
            return new string(chArray2);
        }

        private void method_1(byte[] byte_0, string string_0, int int_0)
        {
            string str = "";
            if (int_0 == 15)
            {
                int_0 = this.method_3(str, string_0);
            }
            else
            {
                str = string_0;
            }
            byte num = 0;
            switch (int_0)
            {
                case 15:
                    for (int j = 6; j < 15; j++)
                    {
                        if (str[j] > '9')
                        {
                            num = 4;
                            break;
                        }
                    }
                    break;

                case 0x11:
                    num = 3;
                    break;

                case 0x12:
                    num = 1;
                    break;

                case 20:
                    num = 2;
                    break;

                default:
                    byte_0[0] = 15;
                    num = 15;
                    break;
            }
            byte[] bytes = BitConverter.GetBytes((uint) (uint.Parse(str.Substring(0, 6)) << 4));
            for (int i = 0; i < 4; i++)
            {
                byte_0[i] = bytes[i];
            }
            byte_0[0] = (byte) (byte_0[0] | num);
            switch (num)
            {
                case 0:
                case 4:
                    break;

                case 1:
                    this.method_2(byte_0, 3, str, 6);
                    break;

                case 2:
                    byte_0[7] = byte.Parse(str.Substring(0x12, 2));
                    this.method_2(byte_0, 3, str, 6);
                    return;

                case 3:
                {
                    byte_0[7] = byte.Parse(str.Substring(15, 2));
                    byte[] buffer = BitConverter.GetBytes(int.Parse(str.Substring(6, 9)));
                    for (int k = 0; k < 4; k++)
                    {
                        byte_0[k + 3] = buffer[k];
                    }
                    return;
                }
                default:
                    return;
            }
        }

        private void method_2(byte[] byte_0, int int_0, string string_0, int int_1)
        {
            int num = (((int.Parse(string_0.Substring(int_1, 4)) - 0x76c) * 12) + int.Parse(string_0.Substring(int_1 + 4, 2))) - 1;
            int num2 = int.Parse(string_0.Substring(int_1 + 6, 2)) - 1;
            int num3 = num | (num2 << 11);
            byte[] bytes = BitConverter.GetBytes(num3);
            for (int i = 0; i < 2; i++)
            {
                byte_0[int_0 + i] = bytes[i];
            }
            int num7 = int.Parse(string_0.Substring(int_1 + 8, 3));
            string s = string_0.Substring(int_1 + 11, 1);
            int result = 0;
            if (!int.TryParse(s, out result))
            {
                char ch = s[0];
                result = ch;
            }
            if ((result < 0) || (result > 9))
            {
                result = 10;
            }
            byte[] buffer2 = BitConverter.GetBytes((int) (num7 | (result << 12)));
            for (int j = 0; j < 2; j++)
            {
                byte_0[(int_0 + 2) + j] = buffer2[j];
            }
        }

        private int method_3(string string_0, string string_1)
        {
            int num;
            switch (string_1[13])
            {
                case 'V':
                    num = 0x11;
                    break;

                case 'W':
                    num = 0x12;
                    break;

                case 'Y':
                    num = 20;
                    break;

                default:
                    num = 15;
                    break;
            }
            if (num == 15)
            {
                string_1 = string_0;
                return num;
            }
            string_1 = string_0.Substring(0, 6);
            return num;
        }
    }
}

