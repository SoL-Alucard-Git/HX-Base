namespace Aisino.Framework.Plugin.Core.Crypto
{
    using System;

    public class IDEAXT
    {
        private int[] int_0;
        private int[] int_1;

        public IDEAXT()
        {
            
            this.int_0 = new int[0x34];
            this.int_1 = new int[0x34];
        }

        public int bytesToInt(byte[] byte_0, int int_2)
        {
            return (((byte_0[int_2] << 8) & 0xff00) + (byte_0[int_2 + 1] & 0xff));
        }

        public void encrypt(byte[] byte_0, byte[] byte_1)
        {
            int index = 0;
            int num2 = this.bytesToInt(byte_0, 0);
            int num3 = this.bytesToInt(byte_0, 2);
            int num4 = this.bytesToInt(byte_0, 4);
            int num5 = this.bytesToInt(byte_0, 6);
            for (int i = 0; i < 8; i++)
            {
                num2 = this.x_multiply_y(num2, this.int_0[index++]);
                num3 += this.int_0[index++];
                num3 &= 0xffff;
                num4 += this.int_0[index++];
                num4 &= 0xffff;
                num5 = this.x_multiply_y(num5, this.int_0[index++]);
                int num7 = num3;
                int num8 = num4;
                num4 ^= num2;
                num3 ^= num5;
                num4 = this.x_multiply_y(num4, this.int_0[index++]);
                num3 += num4;
                num3 &= 0xffff;
                num3 = this.x_multiply_y(num3, this.int_0[index++]);
                num4 += num3;
                num4 &= 0xffff;
                num2 ^= num3;
                num5 ^= num4;
                num3 ^= num8;
                num4 ^= num7;
            }
            this.intToBytes(this.x_multiply_y(num2, this.int_0[index++]), byte_1, 0);
            this.intToBytes(num4 + this.int_0[index++], byte_1, 2);
            this.intToBytes(num3 + this.int_0[index++], byte_1, 4);
            this.intToBytes(this.x_multiply_y(num5, this.int_0[index]), byte_1, 6);
        }

        public void encrypt_subkey(byte[] byte_0)
        {
            for (int i = 0; i < 8; i++)
            {
                this.int_0[i] = this.bytesToInt(byte_0, i * 2);
            }
            for (int j = 8; j < 0x34; j++)
            {
                if ((j & 7) < 6)
                {
                    this.int_0[j] = (((this.int_0[j - 7] & 0x7f) << 9) | (this.int_0[j - 6] >> 7)) & 0xffff;
                }
                else if ((j & 7) == 6)
                {
                    this.int_0[j] = (((this.int_0[j - 7] & 0x7f) << 9) | (this.int_0[j - 14] >> 7)) & 0xffff;
                }
                else
                {
                    this.int_0[j] = (((this.int_0[j - 15] & 0x7f) << 9) | (this.int_0[j - 14] >> 7)) & 0xffff;
                }
            }
        }

        public int fun_a(int int_2)
        {
            if (int_2 < 2)
            {
                return int_2;
            }
            int num3 = 1;
            int num4 = 0x10001 / int_2;
            int num = 0x10001 % int_2;
            while (num != 1)
            {
                int num2 = int_2 / num;
                int_2 = int_2 % num;
                num3 = (num3 + (num4 * num2)) & 0xffff;
                if (int_2 == 1)
                {
                    return num3;
                }
                num2 = num / int_2;
                num = num % int_2;
                num4 = (num4 + (num3 * num2)) & 0xffff;
            }
            return ((1 - num4) & 0xffff);
        }

        public int fun_b(int int_2)
        {
            return (-int_2 & 0xffff);
        }

        public void intToBytes(int int_2, byte[] byte_0, int int_3)
        {
            byte_0[int_3] = (byte) (int_2 >> 8);
            byte_0[int_3 + 1] = (byte) int_2;
        }

        public void uncrypt_subkey()
        {
            int num = 0x34;
            int index = 0;
            index = 1;
            int num3 = this.fun_a(this.int_0[0]);
            index = 2;
            int num4 = this.fun_b(this.int_0[1]);
            index = 3;
            int num5 = this.fun_b(this.int_0[2]);
            index = 4;
            int num6 = this.fun_a(this.int_0[3]);
            num = 0x33;
            this.int_1[0x33] = num6;
            num = 50;
            this.int_1[50] = num5;
            num = 0x31;
            this.int_1[0x31] = num4;
            num = 0x30;
            this.int_1[0x30] = num3;
            for (int i = 1; i < 8; i++)
            {
                num3 = this.int_0[index++];
                num4 = this.int_0[index++];
                this.int_1[--num] = num4;
                this.int_1[--num] = num3;
                num3 = this.fun_a(this.int_0[index++]);
                num4 = this.fun_b(this.int_0[index++]);
                num5 = this.fun_b(this.int_0[index++]);
                num6 = this.fun_a(this.int_0[index++]);
                this.int_1[--num] = num6;
                this.int_1[--num] = num4;
                this.int_1[--num] = num5;
                this.int_1[--num] = num3;
            }
            num3 = this.int_0[index++];
            num4 = this.int_0[index++];
            this.int_1[--num] = num4;
            this.int_1[--num] = num3;
            num3 = this.fun_a(this.int_0[index++]);
            num4 = this.fun_b(this.int_0[index++]);
            num5 = this.fun_b(this.int_0[index++]);
            num6 = this.fun_a(this.int_0[index]);
            this.int_1[--num] = num6;
            this.int_1[--num] = num5;
            this.int_1[--num] = num4;
            this.int_1[--num] = num3;
            for (int j = 0; j < 0x34; j++)
            {
                this.int_0[j] = this.int_1[j];
            }
        }

        public int x_multiply_y(int int_2, int int_3)
        {
            if (int_2 == 0)
            {
                int_2 = 0x10001 - int_3;
            }
            else if (int_3 == 0)
            {
                int_2 = 0x10001 - int_2;
            }
            else
            {
                uint num = (uint) (int_2 * int_3);
                int_3 = ((int) num) & 0xffff;
                int_2 = (int) (num >> 0x10);
                int_2 = (int_3 - int_2) + ((int_3 < int_2) ? 1 : 0);
            }
            return (int_2 & 0xffff);
        }
    }
}

