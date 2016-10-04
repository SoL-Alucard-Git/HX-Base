namespace Aisino.Framework.Plugin.Core.Crypto
{
    using Aisino.Framework.Plugin.Core.Util;
    using System;

    public class IDEA_Ctypt
    {
        private static ushort[] ushort_0;
        private static ushort[] ushort_1;

        static IDEA_Ctypt()
        {
            
            ushort_0 = new ushort[0x34];
            ushort_1 = new ushort[0x34];
        }

        public IDEA_Ctypt()
        {
            
        }

        public static byte[] Crypto(byte[] byte_0)
        {
            return smethod_3(byte_0, true);
        }

        public static byte[] DataToCryp(byte[] byte_0, byte[] byte_1)
        {
            if ((byte_0 == null) || (byte_1 == null))
            {
                return null;
            }
            int num = ((byte_1.Length % 7) == 0) ? (byte_1.Length / 7) : ((byte_1.Length / 7) + 1);
            byte[] destinationArray = new byte[num * 7];
            for (int i = 0; i < destinationArray.Length; i++)
            {
                destinationArray[i] = 0x20;
            }
            if ((byte_1.Length % 7) != 0)
            {
                if (((byte_1.Length >= 2) && (byte_1[byte_1.Length - 2] == 13)) && (byte_1[byte_1.Length - 1] == 10))
                {
                    Array.Copy(byte_1, 0, destinationArray, 0, byte_1.Length - 2);
                    Array.Copy(byte_1, byte_1.Length - 2, destinationArray, destinationArray.Length - 2, 2);
                }
                else
                {
                    Array.Copy(byte_1, 0, destinationArray, 0, byte_1.Length);
                }
            }
            else
            {
                Array.Copy(byte_1, 0, destinationArray, 0, byte_1.Length);
            }
            byte[] buffer2 = new byte[num * 8];
            for (int j = 0; j < num; j++)
            {
                Array.Copy(destinationArray, j * 7, buffer2, j * 8, 7);
                Random random = new Random();
                buffer2[(j * 8) + 7] = (byte) ((random.Next() % 0x40) + 0x30);
            }
            SetCryptoKey(byte_0);
            return Crypto(buffer2);
        }

        public static string DataToCryp(byte[] byte_0, byte[] byte_1, bool bool_0)
        {
            byte[] buffer = DataToCryp(byte_0, byte_1);
            if (buffer == null)
            {
                return string.Empty;
            }
            return BitConverter.ToString(buffer).Replace("-", "");
        }

        public static void SetCryptoKey(byte[] byte_0)
        {
            byte[] bytes = ToolUtil.GetBytes("1234567890123456");
            byte[] destinationArray = new byte[byte_0.Length + bytes.Length];
            Array.Copy(byte_0, 0, destinationArray, 0, byte_0.Length);
            Array.Copy(bytes, 0, destinationArray, byte_0.Length, bytes.Length);
            for (int i = 0x10; i < (destinationArray.Length - 1); i++)
            {
                destinationArray[i - 0x10] = (byte) (destinationArray[i - 0x10] + destinationArray[i]);
            }
            smethod_0(destinationArray);
        }

        private static void smethod_0(Array array_0)
        {
            ushort[] dst = new ushort[8];
            Buffer.BlockCopy(array_0, 0, dst, 0, 0x10);
            ushort[] numArray2 = ushort_0;
            for (int i = 0; i < 8; i++)
            {
                numArray2[i] = dst[i];
            }
            int num3 = 0;
            int num4 = 1;
            for (int j = 8; j < 0x34; j++)
            {
                numArray2[(num4 + 7) + num3] = (ushort) ((numArray2[(num4 & 7) + num3] << 9) | (numArray2[((num4 + 1) & 7) + num3] >> 7));
                num3 += num4 & 8;
                num4 &= 7;
                num4++;
            }
        }

        private static ushort smethod_1(ushort ushort_2, ushort ushort_3)
        {
            if (ushort_2 <= 0)
            {
                return (ushort) (1 - ushort_3);
            }
            if (ushort_3 > 0)
            {
                ulong num = (ulong) (ushort_2 * ushort_3);
                ushort_3 = (ushort) num;
                ushort_2 = (ushort) (num >> 0x10);
                return ((ushort_3 < ushort_2) ? ((ushort) ((1 + ushort_3) - ushort_2)) : ((ushort) (ushort_3 - ushort_2)));
            }
            return (ushort) (1 - ushort_2);
        }

        private static byte[] smethod_2(Array array_0, ushort[] object_0)
        {
            byte[] dst = new byte[8];
            int num = 8;
            ushort[] numArray = new ushort[4];
            Buffer.BlockCopy(array_0, 0, numArray, 0, 8);
            ushort num2 = numArray[0];
            ushort num3 = numArray[1];
            ushort num4 = numArray[2];
            ushort num5 = numArray[3];
            int index = 0;
            do
            {
                num2 = smethod_1(num2, (ushort) object_0[index++]);
                num3 += object_0[index++];
                num4 += object_0[index++];
                num5 = smethod_1(num5, (ushort) object_0[index++]);
                ushort num7 = num4;
                num4 = (ushort) (num4 ^ num2);
                num4 = smethod_1(num4, (ushort) object_0[index++]);
                ushort num8 = num3;
                num3 = (ushort) (num3 ^ num5);
                num3 = (ushort) (num3 + num4);
                num3 = smethod_1(num3, (ushort) object_0[index++]);
                num4 = (ushort) (num4 + num3);
                num2 = (ushort) (num2 ^ num3);
                num5 = (ushort) (num5 ^ num4);
                num3 = (ushort) (num3 ^ num7);
                num4 = (ushort) (num4 ^ num8);
            }
            while (--num > 0);
            num2 = smethod_1(num2, (ushort) object_0[index++]);
            num4 += object_0[index++];
            num3 += object_0[index++];
            num5 = smethod_1(num5, (ushort) object_0[index]);
            Buffer.BlockCopy(new ushort[] { num2, num4, num3, num5 }, 0, dst, 0, 8);
            return dst;
        }

        private static byte[] smethod_3(Array array_0, bool bool_0)
        {
            int length = array_0.Length;
            int num3 = ((length % 8) == 0) ? (length / 8) : ((length / 8) + 1);
            byte[] destinationArray = new byte[num3 * 8];
            int num5 = 0;
            while (num5 < destinationArray.Length)
            {
                destinationArray[num5++] = 0x20;
            }
            Array.Copy(array_0, 0, destinationArray, 0, length);
            byte[] buffer4 = new byte[8];
            byte[] buffer = new byte[num3 * 8];
            for (int i = 0; i < num3; i++)
            {
                Array.Copy(destinationArray, i * 8, buffer4, 0, 8);
                byte[] buffer2 = new byte[8];
                if (bool_0)
                {
                    buffer2 = smethod_4(buffer4);
                }
                for (int j = 0; j < 8; j++)
                {
                    buffer[(i * 8) + j] = buffer2[j];
                }
            }
            return buffer;
        }

        private static byte[] smethod_4(Array array_0)
        {
            return smethod_2(array_0, ushort_0);
        }
    }
}

