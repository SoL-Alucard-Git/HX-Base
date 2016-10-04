namespace Aisino.FTaxBase
{
    using System;
    using System.IO;
    using System.Text;

    public class IDEA
    {
        private ushort[] ushort_0;
        private ushort[] ushort_1;

        public IDEA()
        {
            
            this.ushort_0 = new ushort[0x34];
            this.ushort_1 = new ushort[0x34];
        }

        public void Decrypto(byte[] byte_0, byte[] byte_1)
        {
            this.method_2(byte_0, byte_1, this.ushort_1, 0);
        }

        public void Encrypto(byte[] byte_0, byte[] byte_1)
        {
            this.method_2(byte_0, byte_1, this.ushort_0, 0);
        }

        public bool EncryptoFile(string string_0, string string_1)
        {
            FileStream stream = File.OpenRead(string_0);
            if (stream.Length < 0L)
            {
                return false;
            }
            FileStream stream2 = new FileStream(string_1, FileMode.Create, FileAccess.Write);
            byte[] buffer2 = new byte[8];
            byte[] buffer3 = new byte[8];
            byte[] buffer = new byte[0x10];
            for (int i = 0; i < 0x10; i++)
            {
                buffer[i] = (byte) this.ushort_0[i];
            }
            int num5 = 0;
            while (stream.Read(buffer2, 0, 8) != 0)
            {
                this.Encrypto(buffer2, buffer3);
                stream2.Write(buffer3, 0, 8);
                int num3 = ((num5 % 2) == 0) ? 0 : 8;
                for (int j = 0; j < 8; j++)
                {
                    buffer[j + num3] = (byte) (buffer[j + num3] ^ buffer2[j]);
                }
                if (++num5 == 0x1000)
                {
                    num5 = 0;
                    this.GetEncryptoKey(buffer);
                }
            }
            stream2.Dispose();
            stream.Dispose();
            return true;
        }

        public void GetEncryptoKey(byte[] byte_0)
        {
            ushort[] numArray = this.ushort_0;
            for (int i = 0; i < 8; i++)
            {
                numArray[i] = BitConverter.ToUInt16(byte_0, i * 2);
            }
            int num2 = 0;
            int num4 = 1;
            for (int j = 8; j < 0x34; j++)
            {
                int index = num4 & 7;
                int num6 = (num4 + 1) & 7;
                index += num2;
                num6 += num2;
                numArray[num4 + 7] = (ushort) ((numArray[index] << 9) | (numArray[num6] >> 7));
                if ((num4 % 8) == 0)
                {
                    num2 += 8;
                }
                num4++;
            }
        }

        public string IdeaCrypto(string string_0, bool bool_0)
        {
            string str = string_0;
            int length = str.Length;
            int num2 = length / 8;
            if ((length % 8) > 0)
            {
                num2++;
                str = str + new string(' ', 8 - (length % 8));
            }
            byte[] bytes = new byte[str.Length];
            byte[] buffer2 = CommonTool.StringToBytes(str);
            byte[] buffer3 = new byte[num2 * 8];
            int num3 = 0;
            int num4 = 0;
            for (int i = 0; i < (num2 * 8); i++)
            {
                buffer3[i] = buffer2[num4++];
            }
            for (int j = 0; j < num2; j++)
            {
                byte[] buffer5 = new byte[8];
                byte[] buffer4 = CommonTool.GetSubArray(buffer3, num3, 8);
                if (bool_0)
                {
                    this.Encrypto(buffer4, buffer5);
                }
                else
                {
                    this.Decrypto(buffer4, buffer5);
                }
                for (int m = 0; m < 8; m++)
                {
                    bytes[(j * 8) + m] = buffer5[m];
                }
                num3 += 8;
            }
            string str2 = Encoding.GetEncoding("GBK").GetString(bytes);
            string str3 = "";
            for (int k = 0; k < 8; k++)
            {
                str3 = str3 + ((char) bytes[k]);
            }
            return str2;
        }

        public string IdeaCrypto(byte[] byte_0, bool bool_0)
        {
            byte[] buffer = byte_0;
            int length = byte_0.Length;
            int num2 = length / 8;
            if ((length % 8) > 0)
            {
                num2++;
            }
            byte[] bytes = new byte[buffer.Length];
            byte[] buffer3 = buffer;
            byte[] buffer2 = new byte[num2 * 8];
            int num6 = 0;
            int num4 = 0;
            for (int i = 0; i < (num2 * 8); i++)
            {
                buffer2[i] = buffer3[num4++];
            }
            for (int j = 0; j < num2; j++)
            {
                byte[] buffer6 = new byte[8];
                byte[] buffer5 = CommonTool.GetSubArray(buffer2, num6, 8);
                if (bool_0)
                {
                    this.Encrypto(buffer5, buffer6);
                }
                else
                {
                    this.Decrypto(buffer5, buffer6);
                }
                for (int k = 0; k < 8; k++)
                {
                    bytes[(j * 8) + k] = buffer6[k];
                }
                num6 += 8;
            }
            return Encoding.GetEncoding("GBK").GetString(bytes);
        }

        public void MakeDecryptoKey()
        {
            ushort num = 0;
            ushort num2 = 0;
            ushort num3 = 0;
            int num4 = 0x34;
            int num5 = 0;
            num5 = 1;
            num = this.method_0(this.ushort_0[0]);
            num5 = 2;
            num2 = (ushort)-this.ushort_0[1];
            num5 = 3;
            num3 = (ushort)-this.ushort_0[2];
            num4 = 0x33;
            num5 = 4;
            this.ushort_1[0x33] = this.method_0(this.ushort_0[3]);
            num4 = 50;
            this.ushort_1[50] = num3;
            num4 = 0x31;
            this.ushort_1[0x31] = num2;
            num4 = 0x30;
            this.ushort_1[0x30] = num;
            for (int i = 1; i < 8; i++)
            {
                num = this.ushort_0[num5++];
                this.ushort_1[--num4] = this.ushort_0[num5++];
                this.ushort_1[--num4] = num;
                num = this.method_0(this.ushort_0[num5++]);
                num2 = (ushort)-this.ushort_0[num5++];
                num3 = (ushort)-this.ushort_0[num5++];
                this.ushort_1[--num4] = this.method_0(this.ushort_0[num5++]);
                this.ushort_1[--num4] = num2;
                this.ushort_1[--num4] = num3;
                this.ushort_1[--num4] = num;
            }
            num = this.ushort_0[num5++];
            this.ushort_1[--num4] = this.ushort_0[num5++];
            this.ushort_1[--num4] = num;
            num = this.method_0(this.ushort_0[num5++]);
            num2 = (ushort)-this.ushort_0[num5++];
            num3 = (ushort)-this.ushort_0[num5++];
            this.ushort_1[--num4] = this.method_0(this.ushort_0[num5++]);
            this.ushort_1[--num4] = num3;
            this.ushort_1[--num4] = num2;
            this.ushort_1[--num4] = num;
        }

        private ushort method_0(ushort ushort_2)
        {
            ushort num2;
            if (ushort_2 <= 1)
            {
                return ushort_2;
            }
            ushort num4 = (ushort) (0x10001L / ((ulong) ushort_2));
            ushort num = (ushort) (0x10001L % ((ulong) ushort_2));
            if (num == 1)
            {
                return (ushort) (1 - num4);
            }
            ushort num3 = 1;
        Label_0047:
            num2 = (ushort) (ushort_2 / num);
            ushort_2 = (ushort) (ushort_2 % num);
            num3 = (ushort) (num3 + ((ushort) (num2 * num4)));
            if (ushort_2 != 1)
            {
                num2 = (ushort) (num / ushort_2);
                num = (ushort) (num % ushort_2);
                num4 = (ushort) (num4 + ((ushort) (num2 * num3)));
                if (num == 1)
                {
                    return (ushort) (1 - num4);
                }
                goto Label_0047;
            }
            return num3;
        }

        private ushort method_1(ushort ushort_2, ushort ushort_3)
        {
            if (ushort_2 <= 0)
            {
                return (ushort) (1 - ushort_3);
            }
            if (ushort_3 > 0)
            {
                uint num = (uint) (ushort_2 * ushort_3);
                ushort_3 = (ushort) num;
                ushort_2 = (ushort) (num >> 0x10);
                int num2 = (ushort_3 < ushort_2) ? ((1 + ushort_3) - ushort_2) : (ushort_3 - ushort_2);
                return (ushort) num2;
            }
            return (ushort) (1 - ushort_2);
        }

        private void method_2(byte[] byte_0, byte[] byte_1, ushort[] ushort_2, int int_0)
        {
            ushort num = 0;
            int num2 = 8;
            ushort num3 = BitConverter.ToUInt16(byte_0, 0);
            ushort num4 = BitConverter.ToUInt16(byte_0, 2);
            ushort num5 = BitConverter.ToUInt16(byte_0, 4);
            ushort num6 = BitConverter.ToUInt16(byte_0, 6);
            do
            {
                num3 = this.method_1(num3, ushort_2[int_0++]);
                num4 = (ushort) (num4 + ushort_2[int_0++]);
                num5 = (ushort) (num5 + ushort_2[int_0++]);
                num6 = this.method_1(num6, ushort_2[int_0++]);
                ushort num7 = num5;
                num5 = (ushort) (num5 ^ num3);
                num5 = this.method_1(num5, ushort_2[int_0++]);
                ushort num8 = num4;
                num4 = (ushort) (num4 ^ num6);
                num4 = (ushort) (num4 + num5);
                num4 = this.method_1(num4, ushort_2[int_0++]);
                num5 = (ushort) (num5 + num4);
                num3 = (ushort) (num3 ^ num4);
                num6 = (ushort) (num6 ^ num5);
                num4 = (ushort) (num4 ^ num7);
                num5 = (ushort) (num5 ^ num8);
            }
            while (--num2 > 0);
            num3 = this.method_1(num3, ushort_2[int_0++]);
            num5 = (ushort) (num5 + ushort_2[int_0++]);
            num4 = (ushort) (num4 + ushort_2[int_0++]);
            num6 = this.method_1(num6, ushort_2[int_0]);
            this.method_3(byte_1, BitConverter.GetBytes((short) num3), num);
            num = (ushort) (num + 2);
            this.method_3(byte_1, BitConverter.GetBytes((short) num5), num);
            num = (ushort) (num + 2);
            this.method_3(byte_1, BitConverter.GetBytes((short) num4), num);
            num = (ushort) (num + 2);
            this.method_3(byte_1, BitConverter.GetBytes((short) num6), num);
            num = (ushort) (num + 2);
        }

        private void method_3(byte[] byte_0, byte[] byte_1, int int_0)
        {
            if (int_0 <= byte_0.Length)
            {
                for (int i = 0; i < byte_1.Length; i++)
                {
                    byte_0[int_0 + i] = byte_1[i];
                }
            }
        }

        private bool method_4(string string_0, string string_1)
        {
            FileStream stream = File.OpenRead(string_0);
            long length = stream.Length;
            FileStream stream2 = new FileStream(string_1, FileMode.Create, FileAccess.Write);
            byte[] buffer = new byte[8];
            byte[] buffer2 = new byte[8];
            byte[] buffer3 = new byte[0x10];
            for (int i = 0; i < 0x10; i++)
            {
                buffer3[i] = (byte) this.ushort_0[i];
            }
            int offset = 0;
            while (stream.Read(buffer, offset, 8) >= 8)
            {
                offset = 0x40;
                this.Decrypto(buffer, buffer2);
                stream2.Write(buffer2, 0x40, 8);
                for (int j = 0; j < 8; j++)
                {
                    buffer3[j] = (byte) (buffer3[j] ^ buffer2[j]);
                }
                if (offset == 0x1000)
                {
                    this.GetEncryptoKey(buffer3);
                    this.MakeDecryptoKey();
                }
            }
            stream2.Dispose();
            stream.Dispose();
            return (length == 0L);
        }
    }
}

