namespace ns2
{
    using System;

    internal class Class23
    {
        private static byte[] byte_0;
        private static uint[] uint_0;
        private uint[] uint_1;
        private uint[] uint_2;
        private uint[] uint_3;
        private uint uint_4;

        static Class23()
        {
            
            uint_0 = new uint[] { 
                0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee, 0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501, 0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be, 0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821, 
                0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa, 0xd62f105d, 0x2441453, 0xd8a1e681, 0xe7d3fbc8, 0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed, 0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a, 
                0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c, 0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70, 0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x4881d05, 0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665, 
                0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039, 0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1, 0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1, 0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
             };
            byte_0 = new byte[] { 7, 12, 0x11, 0x16, 5, 9, 14, 20, 4, 11, 0x10, 0x17, 6, 10, 15, 0x15 };
        }

        public Class23()
        {
            
            this.uint_1 = new uint[0x10];
            this.uint_2 = new uint[4];
            this.uint_3 = new uint[4];
            this.method_0();
        }

        private void method_0()
        {
            this.uint_2[0] = 0x1234567;
            this.uint_2[1] = 0x89abcdef;
            this.uint_2[2] = 0xfedcba98;
            this.uint_2[3] = 0x76543210;
            this.uint_4 = 0;
        }

        private void method_1()
        {
            for (int i = 0; i < 4; i++)
            {
                this.uint_3[i] = this.uint_2[i];
            }
            for (int j = 0; j < 0x40; j++)
            {
                switch ((j & 3))
                {
                    case 0:
                        this.uint_3[0] = this.method_2(this.uint_3[0], this.uint_3[1], this.uint_3[2], this.uint_3[3], j);
                        break;

                    case 1:
                        this.uint_3[3] = this.method_2(this.uint_3[3], this.uint_3[0], this.uint_3[1], this.uint_3[2], j);
                        break;

                    case 2:
                        this.uint_3[2] = this.method_2(this.uint_3[2], this.uint_3[3], this.uint_3[0], this.uint_3[1], j);
                        break;

                    case 3:
                        this.uint_3[1] = this.method_2(this.uint_3[1], this.uint_3[2], this.uint_3[3], this.uint_3[0], j);
                        break;
                }
            }
            for (int k = 0; k < 4; k++)
            {
                this.uint_2[k] += this.uint_3[k];
            }
        }

        private uint method_2(uint uint_5, uint uint_6, uint uint_7, uint uint_8, int int_0)
        {
            int num = int_0 >> 4;
            switch (num)
            {
                case 0:
                    uint_5 += (uint_6 & uint_7) | ((~uint_6 & uint_8) + this.uint_1[int_0 & 15]);
                    break;

                case 1:
                    uint_5 += (uint_6 & uint_8) | ((uint_7 & ~uint_8) + this.uint_1[(((int_0 - 0x10) * 5) + 1) & 15]);
                    break;

                case 2:
                    uint_5 += (uint_6 ^ uint_7) ^ (uint_8 + this.uint_1[(((int_0 - 0x20) * 3) + 5) & 15]);
                    break;

                case 3:
                    uint_5 += uint_7 ^ ((uint_6 & ~uint_8) + this.uint_1[((int_0 - 0x30) * 7) & 15]);
                    break;
            }
            return (uint_6 + this.method_3(uint_5 + uint_0[int_0], byte_0[((num * 4) + int_0) & 3]));
        }

        private uint method_3(uint uint_5, int int_0)
        {
            return ((uint_5 << int_0) | (uint_5 >> (0x20 - int_0)));
        }

        private byte[] method_4(uint[] uint_5)
        {
            int num = uint_5.Length * 4;
            byte[] array = new byte[num];
            for (int i = 0; i < uint_5.Length; i++)
            {
                BitConverter.GetBytes(uint_5[i]).CopyTo(array, (int) (i * 4));
            }
            return array;
        }

        private uint[] method_5(byte[] byte_1)
        {
            int num = byte_1.Length / 4;
            uint[] numArray = new uint[num];
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = BitConverter.ToUInt32(byte_1, i * 4);
            }
            return numArray;
        }

        public void method_6(byte[] byte_1, uint uint_5)
        {
            while (uint_5 > 0)
            {
                uint num = this.uint_4 & 0x3f;
                int num4 = 0;
                byte[] buffer = this.method_4(this.uint_1);
                if ((uint_5 + num) >= 0x40)
                {
                    for (uint i = num; i < 0x40; i++)
                    {
                        buffer[i] = byte_1[num4++];
                    }
                    this.method_1();
                    uint_5 -= 0x40 - num;
                    this.uint_4 += 0x40 - num;
                }
                else
                {
                    for (uint j = num; j < (num + uint_5); j++)
                    {
                        buffer[j] = byte_1[num4++];
                    }
                    this.uint_4 += uint_5;
                    uint_5 = 0;
                }
                this.uint_1 = this.method_5(buffer);
            }
        }

        public void method_7(byte[] byte_1, uint uint_5)
        {
            uint index = this.uint_4 & 0x3f;
            byte[] buffer = this.method_4(this.uint_1);
            buffer[index] = 1;
            if (index > 0x37)
            {
                for (uint j = index + 1; j < 0x40; j++)
                {
                    buffer[j] = 0;
                }
                this.method_1();
                for (int k = 0; k < 14; k++)
                {
                    this.uint_1[k] = 0;
                }
            }
            else
            {
                for (uint m = index + 1; m < 0x38; m++)
                {
                    buffer[m] = 0;
                }
            }
            this.uint_1 = this.method_5(buffer);
            this.uint_1[14] = this.uint_4 >> 0x1d;
            this.uint_1[15] = this.uint_4 << 3;
            this.method_1();
            byte[] buffer2 = this.method_4(this.uint_2);
            for (int i = 0; i < uint_5; i++)
            {
                byte_1[i] = buffer2[i];
            }
        }
    }
}

