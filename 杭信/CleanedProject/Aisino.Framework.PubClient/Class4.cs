using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal class Class4
{
    private static bool bool_0 = false;
    private static bool bool_1 = false;
    private static bool bool_2 = false;
    private static bool bool_3 = false;
    private static bool bool_4 = false;
    [Attribute0(typeof(Attribute0.Class5<object>[]))]
    private static bool bool_5 = false;
    private static bool bool_6 = false;
    private static byte[] byte_0 = new byte[0];
    private static byte[] byte_1 = new byte[0];
    private static byte[] byte_2 = new byte[0];
    private static byte[] byte_3 = new byte[0];
    internal static Delegate1 delegate1_0 = null;
    internal static Delegate1 delegate1_1 = null;
    internal static Hashtable hashtable_0 = new Hashtable();
    private static int[] int_0 = new int[0];
    private static int int_1 = 0;
    private static int int_2 = 1;
    private static int int_3 = 0;
    private static int int_4 = 0;
    private static IntPtr intptr_0 = IntPtr.Zero;
    private static IntPtr intptr_1 = IntPtr.Zero;
    private static IntPtr intptr_2 = IntPtr.Zero;
    private static long long_0 = 0L;
    private static long long_1 = 0L;
    private static SortedList sortedList_0 = new SortedList();
    private static string[] string_0 = new string[0];
    private static uint[] uint_0 = new uint[] { 
        0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee, 0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501, 0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be, 0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821, 
        0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa, 0xd62f105d, 0x2441453, 0xd8a1e681, 0xe7d3fbc8, 0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed, 0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a, 
        0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c, 0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70, 0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x4881d05, 0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665, 
        0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039, 0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1, 0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1, 0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
     };

    [DllImport("kernel32.dll")]
    private static extern int CloseHandle(IntPtr intptr_3);
    [DllImport("kernel32.dll")]
    public static extern IntPtr FindResource(IntPtr intptr_3, string string_1, uint uint_1);
    [DllImport("kernel32", CharSet=CharSet.Ansi)]
    public static extern IntPtr GetProcAddress(IntPtr intptr_3, string string_1);
    [DllImport("kernel32")]
    public static extern IntPtr LoadLibrary(string string_1);
    private void method_0()
    {
    }

    private byte[] method_1()
    {
        return null;
    }

    internal byte[] method_10()
    {
        return null;
    }

    private byte[] method_2()
    {
        return null;
    }

    private byte[] method_3()
    {
        return null;
    }

    private byte[] method_4()
    {
        return null;
    }

    private byte[] method_5()
    {
        return null;
    }

    private byte[] method_6()
    {
        return null;
    }

    internal byte[] method_7()
    {
        string str = "{11111-22222-40001-00001}";
        if (str.Length > 0)
        {
            return new byte[] { 1, 2 };
        }
        return new byte[] { 1, 2 };
    }

    internal byte[] method_8()
    {
        string str = "{11111-22222-40001-00002}";
        if (str.Length > 0)
        {
            return new byte[] { 1, 2 };
        }
        return new byte[] { 1, 2 };
    }

    internal byte[] method_9()
    {
        return null;
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint uint_1, int int_5, uint uint_2);
    [DllImport("kernel32.dll")]
    private static extern int ReadProcessMemory(IntPtr intptr_3, IntPtr intptr_4, [In, Out] byte[] byte_4, uint uint_1, out IntPtr intptr_5);
    [DllImport("kernel32.dll")]
    private static extern void RtlZeroMemory(IntPtr intptr_3, uint uint_1);
    internal static byte[] smethod_0(object object_0)
    {
        uint[] numArray = new uint[0x10];
        int num5 = 0x1c0 - ((object_0.Length * 8) % 0x200);
        uint num2 = (uint) ((num5 + 0x200) % 0x200);
        if (num2 == 0)
        {
            num2 = 0x200;
        }
        uint num4 = (uint) ((object_0.Length + (num2 / 8)) + ((ulong) 8L));
        ulong num3 = (ulong) (object_0.Length * 8L);
        byte[] buffer = new byte[num4];
        for (int i = 0; i < object_0.Length; i++)
        {
            buffer[i] = (byte) object_0[i];
        }
        buffer[object_0.Length] = (byte) (buffer[object_0.Length] | 0x80);
        for (int j = 8; j > 0; j--)
        {
            buffer[(int) ((IntPtr) (num4 - j))] = (byte) ((num3 >> ((8 - j) * 8)) & ((ulong) 0xffL));
        }
        uint num = (uint) ((buffer.Length * 8) / 0x20);
        uint num8 = 0x67452301;
        uint num9 = 0xefcdab89;
        uint num10 = 0x98badcfe;
        uint num11 = 0x10325476;
        for (uint k = 0; k < (num / 0x10); k++)
        {
            uint num13 = k << 6;
            for (uint m = 0; m < 0x3d; m += 4)
            {
                numArray[m >> 2] = (uint) ((((buffer[(int) ((IntPtr) (num13 + (m + 3)))] << 0x18) | (buffer[(int) ((IntPtr) (num13 + (m + 2)))] << 0x10)) | (buffer[(int) ((IntPtr) (num13 + (m + 1)))] << 8)) | buffer[num13 + m]);
            }
            uint num15 = num8;
            uint num16 = num9;
            uint num17 = num10;
            uint num18 = num11;
            smethod_1(ref num8, num9, num10, num11, 0, 7, 1, numArray);
            smethod_1(ref num11, num8, num9, num10, 1, 12, 2, numArray);
            smethod_1(ref num10, num11, num8, num9, 2, 0x11, 3, numArray);
            smethod_1(ref num9, num10, num11, num8, 3, 0x16, 4, numArray);
            smethod_1(ref num8, num9, num10, num11, 4, 7, 5, numArray);
            smethod_1(ref num11, num8, num9, num10, 5, 12, 6, numArray);
            smethod_1(ref num10, num11, num8, num9, 6, 0x11, 7, numArray);
            smethod_1(ref num9, num10, num11, num8, 7, 0x16, 8, numArray);
            smethod_1(ref num8, num9, num10, num11, 8, 7, 9, numArray);
            smethod_1(ref num11, num8, num9, num10, 9, 12, 10, numArray);
            smethod_1(ref num10, num11, num8, num9, 10, 0x11, 11, numArray);
            smethod_1(ref num9, num10, num11, num8, 11, 0x16, 12, numArray);
            smethod_1(ref num8, num9, num10, num11, 12, 7, 13, numArray);
            smethod_1(ref num11, num8, num9, num10, 13, 12, 14, numArray);
            smethod_1(ref num10, num11, num8, num9, 14, 0x11, 15, numArray);
            smethod_1(ref num9, num10, num11, num8, 15, 0x16, 0x10, numArray);
            smethod_2(ref num8, num9, num10, num11, 1, 5, 0x11, numArray);
            smethod_2(ref num11, num8, num9, num10, 6, 9, 0x12, numArray);
            smethod_2(ref num10, num11, num8, num9, 11, 14, 0x13, numArray);
            smethod_2(ref num9, num10, num11, num8, 0, 20, 20, numArray);
            smethod_2(ref num8, num9, num10, num11, 5, 5, 0x15, numArray);
            smethod_2(ref num11, num8, num9, num10, 10, 9, 0x16, numArray);
            smethod_2(ref num10, num11, num8, num9, 15, 14, 0x17, numArray);
            smethod_2(ref num9, num10, num11, num8, 4, 20, 0x18, numArray);
            smethod_2(ref num8, num9, num10, num11, 9, 5, 0x19, numArray);
            smethod_2(ref num11, num8, num9, num10, 14, 9, 0x1a, numArray);
            smethod_2(ref num10, num11, num8, num9, 3, 14, 0x1b, numArray);
            smethod_2(ref num9, num10, num11, num8, 8, 20, 0x1c, numArray);
            smethod_2(ref num8, num9, num10, num11, 13, 5, 0x1d, numArray);
            smethod_2(ref num11, num8, num9, num10, 2, 9, 30, numArray);
            smethod_2(ref num10, num11, num8, num9, 7, 14, 0x1f, numArray);
            smethod_2(ref num9, num10, num11, num8, 12, 20, 0x20, numArray);
            smethod_3(ref num8, num9, num10, num11, 5, 4, 0x21, numArray);
            smethod_3(ref num11, num8, num9, num10, 8, 11, 0x22, numArray);
            smethod_3(ref num10, num11, num8, num9, 11, 0x10, 0x23, numArray);
            smethod_3(ref num9, num10, num11, num8, 14, 0x17, 0x24, numArray);
            smethod_3(ref num8, num9, num10, num11, 1, 4, 0x25, numArray);
            smethod_3(ref num11, num8, num9, num10, 4, 11, 0x26, numArray);
            smethod_3(ref num10, num11, num8, num9, 7, 0x10, 0x27, numArray);
            smethod_3(ref num9, num10, num11, num8, 10, 0x17, 40, numArray);
            smethod_3(ref num8, num9, num10, num11, 13, 4, 0x29, numArray);
            smethod_3(ref num11, num8, num9, num10, 0, 11, 0x2a, numArray);
            smethod_3(ref num10, num11, num8, num9, 3, 0x10, 0x2b, numArray);
            smethod_3(ref num9, num10, num11, num8, 6, 0x17, 0x2c, numArray);
            smethod_3(ref num8, num9, num10, num11, 9, 4, 0x2d, numArray);
            smethod_3(ref num11, num8, num9, num10, 12, 11, 0x2e, numArray);
            smethod_3(ref num10, num11, num8, num9, 15, 0x10, 0x2f, numArray);
            smethod_3(ref num9, num10, num11, num8, 2, 0x17, 0x30, numArray);
            smethod_4(ref num8, num9, num10, num11, 0, 6, 0x31, numArray);
            smethod_4(ref num11, num8, num9, num10, 7, 10, 50, numArray);
            smethod_4(ref num10, num11, num8, num9, 14, 15, 0x33, numArray);
            smethod_4(ref num9, num10, num11, num8, 5, 0x15, 0x34, numArray);
            smethod_4(ref num8, num9, num10, num11, 12, 6, 0x35, numArray);
            smethod_4(ref num11, num8, num9, num10, 3, 10, 0x36, numArray);
            smethod_4(ref num10, num11, num8, num9, 10, 15, 0x37, numArray);
            smethod_4(ref num9, num10, num11, num8, 1, 0x15, 0x38, numArray);
            smethod_4(ref num8, num9, num10, num11, 8, 6, 0x39, numArray);
            smethod_4(ref num11, num8, num9, num10, 15, 10, 0x3a, numArray);
            smethod_4(ref num10, num11, num8, num9, 6, 15, 0x3b, numArray);
            smethod_4(ref num9, num10, num11, num8, 13, 0x15, 60, numArray);
            smethod_4(ref num8, num9, num10, num11, 4, 6, 0x3d, numArray);
            smethod_4(ref num11, num8, num9, num10, 11, 10, 0x3e, numArray);
            smethod_4(ref num10, num11, num8, num9, 2, 15, 0x3f, numArray);
            smethod_4(ref num9, num10, num11, num8, 9, 0x15, 0x40, numArray);
            num8 += num15;
            num9 += num16;
            num10 += num17;
            num11 += num18;
        }
        byte[] destinationArray = new byte[0x10];
        Array.Copy(BitConverter.GetBytes(num8), 0, destinationArray, 0, 4);
        Array.Copy(BitConverter.GetBytes(num9), 0, destinationArray, 4, 4);
        Array.Copy(BitConverter.GetBytes(num10), 0, destinationArray, 8, 4);
        Array.Copy(BitConverter.GetBytes(num11), 0, destinationArray, 12, 4);
        return destinationArray;
    }

    private static void smethod_1(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, object object_0)
    {
        uint_1 = uint_2 + smethod_5(((uint_1 + ((uint_2 & uint_3) | (~uint_2 & uint_4))) + object_0[uint_5]) + uint_0[(int) ((IntPtr) (uint_6 - 1))], ushort_0);
    }

    [Attribute0(typeof(Attribute0.Class5<object>[]))]
    /* private scope */ static bool smethod_10(int int_5)
    {
        if (byte_2.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class4).Assembly.GetManifestResourceStream("\x0095uc\x0099i\x008f\x008be\x0094\x0086x7k\x0086\x009ft\x0092c.\x008dqyo\x009e\x0086\x008cud\x00879\x008aqvwm\x008a8")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x6b;
            buffer2[0] = 170;
            buffer2[0] = 0xf5;
            buffer2[1] = 0x2a;
            buffer2[1] = 0xa6;
            buffer2[1] = 0xb5;
            buffer2[2] = 0x70;
            buffer2[2] = 0xac;
            buffer2[2] = 0x4e;
            buffer2[3] = 120;
            buffer2[3] = 0xd7;
            buffer2[3] = 0x57;
            buffer2[4] = 0x84;
            buffer2[4] = 60;
            buffer2[4] = 180;
            buffer2[4] = 0xce;
            buffer2[5] = 170;
            buffer2[5] = 0x59;
            buffer2[5] = 0x8b;
            buffer2[5] = 0x8e;
            buffer2[5] = 0x58;
            buffer2[5] = 0xde;
            buffer2[6] = 0x83;
            buffer2[6] = 130;
            buffer2[6] = 0x70;
            buffer2[6] = 0xc5;
            buffer2[7] = 0x66;
            buffer2[7] = 0xa9;
            buffer2[7] = 0xb3;
            buffer2[7] = 0x80;
            buffer2[7] = 0x62;
            buffer2[7] = 50;
            buffer2[8] = 0x61;
            buffer2[8] = 0x93;
            buffer2[8] = 0xa7;
            buffer2[8] = 0xec;
            buffer2[9] = 0x5c;
            buffer2[9] = 110;
            buffer2[9] = 0x58;
            buffer2[9] = 0x2c;
            buffer2[10] = 0x85;
            buffer2[10] = 0x90;
            buffer2[10] = 0x92;
            buffer2[10] = 120;
            buffer2[10] = 0xa4;
            buffer2[10] = 0xd7;
            buffer2[11] = 0x55;
            buffer2[11] = 0x79;
            buffer2[11] = 0x38;
            buffer2[11] = 0x70;
            buffer2[11] = 0x61;
            buffer2[11] = 0x6d;
            buffer2[12] = 0x76;
            buffer2[12] = 0x62;
            buffer2[12] = 0x6d;
            buffer2[12] = 0x4d;
            buffer2[13] = 0x81;
            buffer2[13] = 0x49;
            buffer2[13] = 150;
            buffer2[13] = 0x37;
            buffer2[13] = 0x6d;
            buffer2[14] = 0x79;
            buffer2[14] = 0xcd;
            buffer2[14] = 0x81;
            buffer2[15] = 0x60;
            buffer2[15] = 0xa5;
            buffer2[15] = 0x41;
            buffer2[15] = 0x2e;
            buffer2[15] = 0x37;
            buffer2[0x10] = 0x9a;
            buffer2[0x10] = 0x57;
            buffer2[0x10] = 0xb0;
            buffer2[0x10] = 0x91;
            buffer2[0x10] = 0x95;
            buffer2[0x10] = 0x7c;
            buffer2[0x11] = 0x5c;
            buffer2[0x11] = 0x6c;
            buffer2[0x11] = 0xa2;
            buffer2[0x11] = 0xe9;
            buffer2[0x12] = 0x84;
            buffer2[0x12] = 0x58;
            buffer2[0x12] = 0x7e;
            buffer2[0x12] = 2;
            buffer2[0x13] = 0xb2;
            buffer2[0x13] = 0xd9;
            buffer2[0x13] = 0xa5;
            buffer2[0x13] = 0x57;
            buffer2[20] = 0x62;
            buffer2[20] = 0x1f;
            buffer2[20] = 240;
            buffer2[0x15] = 0x7c;
            buffer2[0x15] = 0x8a;
            buffer2[0x15] = 8;
            buffer2[0x16] = 0x8e;
            buffer2[0x16] = 130;
            buffer2[0x16] = 0xaf;
            buffer2[0x17] = 0x58;
            buffer2[0x17] = 0x5b;
            buffer2[0x17] = 0x63;
            buffer2[0x18] = 0x59;
            buffer2[0x18] = 0x67;
            buffer2[0x18] = 0x5b;
            buffer2[0x18] = 0xcd;
            buffer2[0x18] = 0x6d;
            buffer2[0x18] = 0x74;
            buffer2[0x19] = 0xa3;
            buffer2[0x19] = 0xc5;
            buffer2[0x19] = 0x97;
            buffer2[0x19] = 0x84;
            buffer2[0x19] = 0x1d;
            buffer2[0x1a] = 0x80;
            buffer2[0x1a] = 0x8d;
            buffer2[0x1a] = 130;
            buffer2[0x1a] = 0x69;
            buffer2[0x1a] = 0x68;
            buffer2[0x1a] = 6;
            buffer2[0x1b] = 0x67;
            buffer2[0x1b] = 0x60;
            buffer2[0x1b] = 0x58;
            buffer2[0x1b] = 0x5c;
            buffer2[0x1c] = 0xa3;
            buffer2[0x1c] = 0xa4;
            buffer2[0x1c] = 0x68;
            buffer2[0x1c] = 0x3f;
            buffer2[0x1d] = 0x71;
            buffer2[0x1d] = 0x98;
            buffer2[0x1d] = 0xca;
            buffer2[0x1d] = 0x76;
            buffer2[0x1d] = 0x29;
            buffer2[0x1d] = 0xcb;
            buffer2[30] = 140;
            buffer2[30] = 0x72;
            buffer2[30] = 0xcc;
            buffer2[0x1f] = 0x60;
            buffer2[0x1f] = 130;
            buffer2[0x1f] = 0x86;
            buffer2[0x1f] = 0x40;
            buffer2[0x1f] = 0x4a;
            buffer2[0x1f] = 0x5b;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x85;
            buffer4[0] = 140;
            buffer4[0] = 0x62;
            buffer4[0] = 0xeb;
            buffer4[1] = 0x63;
            buffer4[1] = 0x9f;
            buffer4[1] = 0x98;
            buffer4[1] = 0xee;
            buffer4[2] = 170;
            buffer4[2] = 0xcb;
            buffer4[2] = 0x71;
            buffer4[2] = 0x72;
            buffer4[2] = 0x3e;
            buffer4[3] = 0x56;
            buffer4[3] = 180;
            buffer4[3] = 0x99;
            buffer4[3] = 230;
            buffer4[4] = 10;
            buffer4[4] = 0x62;
            buffer4[4] = 0x7f;
            buffer4[5] = 0x8e;
            buffer4[5] = 0x17;
            buffer4[5] = 190;
            buffer4[5] = 0x92;
            buffer4[6] = 0x9d;
            buffer4[6] = 150;
            buffer4[6] = 0xc7;
            buffer4[6] = 0xb0;
            buffer4[7] = 0xe2;
            buffer4[7] = 0x48;
            buffer4[7] = 0x4c;
            buffer4[7] = 0x8d;
            buffer4[7] = 0x18;
            buffer4[8] = 0x59;
            buffer4[8] = 0x9c;
            buffer4[8] = 0x58;
            buffer4[8] = 0x8a;
            buffer4[8] = 6;
            buffer4[9] = 0xaf;
            buffer4[9] = 0x68;
            buffer4[9] = 0x7d;
            buffer4[10] = 0x7c;
            buffer4[10] = 0xa2;
            buffer4[10] = 0x8e;
            buffer4[10] = 0x26;
            buffer4[11] = 0x16;
            buffer4[11] = 0x55;
            buffer4[11] = 50;
            buffer4[12] = 0x60;
            buffer4[12] = 40;
            buffer4[12] = 0x61;
            buffer4[12] = 0x62;
            buffer4[12] = 130;
            buffer4[12] = 0xf9;
            buffer4[13] = 0x86;
            buffer4[13] = 0x4c;
            buffer4[13] = 0xb9;
            buffer4[13] = 6;
            buffer4[14] = 0x75;
            buffer4[14] = 0x5c;
            buffer4[14] = 0x49;
            buffer4[14] = 0x95;
            buffer4[14] = 0x9e;
            buffer4[14] = 0xf7;
            buffer4[15] = 0x74;
            buffer4[15] = 0x88;
            buffer4[15] = 0xa5;
            buffer4[15] = 0x5b;
            buffer4[15] = 0xb6;
            byte[] rgbIV = buffer4;
            byte[] publicKeyToken = typeof(Class4).Assembly.GetName().GetPublicKeyToken();
            if ((publicKeyToken != null) && (publicKeyToken.Length > 0))
            {
                rgbIV[1] = publicKeyToken[0];
                rgbIV[3] = publicKeyToken[1];
                rgbIV[5] = publicKeyToken[2];
                rgbIV[7] = publicKeyToken[3];
                rgbIV[9] = publicKeyToken[4];
                rgbIV[11] = publicKeyToken[5];
                rgbIV[13] = publicKeyToken[6];
                rgbIV[15] = publicKeyToken[7];
            }
            SymmetricAlgorithm algorithm = smethod_7();
            algorithm.Mode = CipherMode.CBC;
            ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, rgbIV);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            byte_2 = stream.ToArray();
            stream.Close();
            stream2.Close();
            reader.Close();
        }
        if (byte_3.Length == 0)
        {
            byte_3 = smethod_18(smethod_17(typeof(Class4).Assembly).ToString());
        }
        int index = 0;
        try
        {
            index = BitConverter.ToInt32(new byte[] { byte_2[int_5], byte_2[int_5 + 1], byte_2[int_5 + 2], byte_2[int_5 + 3] }, 0);
        }
        catch
        {
        }
        try
        {
            if (byte_3[index] == 0x80)
            {
                return true;
            }
        }
        catch
        {
        }
        return false;
    }

    [Attribute0(typeof(Attribute0.Class5<object>[]))]
    /* private scope */ static string smethod_11(int int_5)
    {
        if (byte_1.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class4).Assembly.GetManifestResourceStream("\x008e\x0093\x009frf5\x0087\x008d\x008d6b\x0092r\x008b\x009a\x0098g\x009d.qq\x0092x6\x0086\x0088b\x009dhl\x0097\x0093bch\x009e\x008a")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x63;
            buffer2[0] = 0x6c;
            buffer2[0] = 0x43;
            buffer2[1] = 0x81;
            buffer2[1] = 120;
            buffer2[1] = 0xc0;
            buffer2[2] = 0x9f;
            buffer2[2] = 0x8d;
            buffer2[2] = 0x58;
            buffer2[2] = 0xa1;
            buffer2[2] = 0x8e;
            buffer2[2] = 0x8d;
            buffer2[3] = 0x42;
            buffer2[3] = 0xc3;
            buffer2[3] = 140;
            buffer2[3] = 100;
            buffer2[3] = 0x54;
            buffer2[4] = 0x5c;
            buffer2[4] = 0x8a;
            buffer2[4] = 0xad;
            buffer2[5] = 0x73;
            buffer2[5] = 0x5c;
            buffer2[5] = 0x60;
            buffer2[5] = 0xab;
            buffer2[5] = 0xb9;
            buffer2[5] = 0x16;
            buffer2[6] = 11;
            buffer2[6] = 0xaf;
            buffer2[6] = 0xcc;
            buffer2[6] = 0x3b;
            buffer2[6] = 0xcc;
            buffer2[7] = 0x6d;
            buffer2[7] = 0x93;
            buffer2[7] = 0x94;
            buffer2[7] = 0x65;
            buffer2[8] = 0x37;
            buffer2[8] = 0x84;
            buffer2[8] = 130;
            buffer2[8] = 80;
            buffer2[8] = 0x49;
            buffer2[9] = 0x44;
            buffer2[9] = 100;
            buffer2[9] = 0x24;
            buffer2[10] = 0x70;
            buffer2[10] = 0x81;
            buffer2[10] = 0x95;
            buffer2[10] = 160;
            buffer2[10] = 0x2c;
            buffer2[11] = 0xa1;
            buffer2[11] = 0x97;
            buffer2[11] = 0xbf;
            buffer2[11] = 0x21;
            buffer2[12] = 0x89;
            buffer2[12] = 0xa1;
            buffer2[12] = 0x22;
            buffer2[13] = 0x95;
            buffer2[13] = 0x8a;
            buffer2[13] = 50;
            buffer2[14] = 0x91;
            buffer2[14] = 0x9c;
            buffer2[14] = 0x9c;
            buffer2[14] = 0x15;
            buffer2[15] = 0x65;
            buffer2[15] = 0x70;
            buffer2[15] = 0xd1;
            buffer2[0x10] = 0x60;
            buffer2[0x10] = 90;
            buffer2[0x10] = 0x59;
            buffer2[0x10] = 0x16;
            buffer2[0x11] = 0x58;
            buffer2[0x11] = 0xcf;
            buffer2[0x11] = 0xf5;
            buffer2[0x12] = 0xa2;
            buffer2[0x12] = 0x45;
            buffer2[0x12] = 0x66;
            buffer2[0x12] = 0x8e;
            buffer2[0x13] = 160;
            buffer2[0x13] = 0x60;
            buffer2[0x13] = 0x4d;
            buffer2[0x13] = 0x74;
            buffer2[0x13] = 0x89;
            buffer2[20] = 0x6f;
            buffer2[20] = 0x55;
            buffer2[20] = 0x77;
            buffer2[20] = 0x79;
            buffer2[0x15] = 0x92;
            buffer2[0x15] = 0x6c;
            buffer2[0x15] = 0x7d;
            buffer2[0x15] = 0x9b;
            buffer2[0x15] = 40;
            buffer2[0x15] = 0x40;
            buffer2[0x16] = 0x5f;
            buffer2[0x16] = 0x71;
            buffer2[0x16] = 0x93;
            buffer2[0x16] = 0x10;
            buffer2[0x17] = 0x56;
            buffer2[0x17] = 0x8b;
            buffer2[0x17] = 0xa6;
            buffer2[0x17] = 170;
            buffer2[0x17] = 0x7a;
            buffer2[0x17] = 0x53;
            buffer2[0x18] = 0x70;
            buffer2[0x18] = 0xcd;
            buffer2[0x18] = 0xc9;
            buffer2[0x18] = 0x7a;
            buffer2[0x18] = 0x8e;
            buffer2[0x18] = 0x57;
            buffer2[0x19] = 0x88;
            buffer2[0x19] = 100;
            buffer2[0x19] = 0xea;
            buffer2[0x1a] = 0x69;
            buffer2[0x1a] = 0x79;
            buffer2[0x1a] = 0xa2;
            buffer2[0x1a] = 0x74;
            buffer2[0x1a] = 0x5c;
            buffer2[0x1b] = 140;
            buffer2[0x1b] = 0x84;
            buffer2[0x1b] = 0xa2;
            buffer2[0x1b] = 0x7c;
            buffer2[0x1c] = 0x84;
            buffer2[0x1c] = 100;
            buffer2[0x1c] = 0x8b;
            buffer2[0x1c] = 130;
            buffer2[0x1c] = 0x75;
            buffer2[0x1c] = 0xa9;
            buffer2[0x1d] = 0x68;
            buffer2[0x1d] = 150;
            buffer2[0x1d] = 11;
            buffer2[0x1d] = 0x58;
            buffer2[30] = 0x67;
            buffer2[30] = 0x62;
            buffer2[30] = 0x45;
            buffer2[30] = 100;
            buffer2[30] = 0x6d;
            buffer2[0x1f] = 0x56;
            buffer2[0x1f] = 0x98;
            buffer2[0x1f] = 0xa3;
            buffer2[0x1f] = 0x89;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x63;
            buffer4[0] = 0x60;
            buffer4[0] = 0x81;
            buffer4[0] = 0x3d;
            buffer4[0] = 0x72;
            buffer4[0] = 0xf8;
            buffer4[1] = 0x26;
            buffer4[1] = 0x60;
            buffer4[1] = 0x61;
            buffer4[1] = 0x6a;
            buffer4[1] = 0x5c;
            buffer4[1] = 0;
            buffer4[2] = 0xc3;
            buffer4[2] = 0x8f;
            buffer4[2] = 0x9c;
            buffer4[2] = 0x6d;
            buffer4[2] = 0x85;
            buffer4[2] = 110;
            buffer4[3] = 0x6a;
            buffer4[3] = 0x8f;
            buffer4[3] = 0x60;
            buffer4[3] = 0x8a;
            buffer4[3] = 0x88;
            buffer4[3] = 0x53;
            buffer4[4] = 0xa1;
            buffer4[4] = 0x73;
            buffer4[4] = 0xb7;
            buffer4[5] = 0x4f;
            buffer4[5] = 0x72;
            buffer4[5] = 0x8e;
            buffer4[5] = 0x3b;
            buffer4[5] = 0x85;
            buffer4[5] = 0x3b;
            buffer4[6] = 0x8b;
            buffer4[6] = 0x58;
            buffer4[6] = 160;
            buffer4[7] = 0x63;
            buffer4[7] = 0x5e;
            buffer4[7] = 130;
            buffer4[7] = 0x9e;
            buffer4[8] = 0x54;
            buffer4[8] = 0x7e;
            buffer4[8] = 0x95;
            buffer4[9] = 100;
            buffer4[9] = 0xc1;
            buffer4[9] = 0x70;
            buffer4[9] = 90;
            buffer4[9] = 20;
            buffer4[10] = 160;
            buffer4[10] = 0x7a;
            buffer4[10] = 0xa1;
            buffer4[10] = 0x81;
            buffer4[10] = 0xca;
            buffer4[11] = 0xc7;
            buffer4[11] = 0x71;
            buffer4[11] = 0x68;
            buffer4[11] = 0xab;
            buffer4[11] = 0x95;
            buffer4[12] = 0x81;
            buffer4[12] = 0x5d;
            buffer4[12] = 0xbb;
            buffer4[13] = 0xbf;
            buffer4[13] = 0xa1;
            buffer4[13] = 70;
            buffer4[14] = 0x6c;
            buffer4[14] = 0xb3;
            buffer4[14] = 0x58;
            buffer4[14] = 0x60;
            buffer4[14] = 0xc6;
            buffer4[15] = 140;
            buffer4[15] = 0x79;
            buffer4[15] = 0x4e;
            byte[] array = buffer4;
            Array.Reverse(array);
            byte[] publicKeyToken = typeof(Class4).Assembly.GetName().GetPublicKeyToken();
            if ((publicKeyToken != null) && (publicKeyToken.Length > 0))
            {
                array[1] = publicKeyToken[0];
                array[3] = publicKeyToken[1];
                array[5] = publicKeyToken[2];
                array[7] = publicKeyToken[3];
                array[9] = publicKeyToken[4];
                array[11] = publicKeyToken[5];
                array[13] = publicKeyToken[6];
                array[15] = publicKeyToken[7];
            }
            SymmetricAlgorithm algorithm = smethod_7();
            algorithm.Mode = CipherMode.CBC;
            ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, array);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            byte_1 = stream.ToArray();
            stream.Close();
            stream2.Close();
            reader.Close();
        }
        int count = BitConverter.ToInt32(byte_1, int_5);
        try
        {
            return Encoding.Unicode.GetString(byte_1, int_5 + 4, count);
        }
        catch
        {
        }
        return "";
    }

    [Attribute0(typeof(Attribute0.Class5<object>[]))]
    internal static string smethod_12(string string_1)
    {
        "{11111-22222-50001-00000}".Trim();
        byte[] bytes = Convert.FromBase64String(string_1);
        return Encoding.Unicode.GetString(bytes, 0, bytes.Length);
    }

    internal static uint smethod_13(IntPtr intptr_3, IntPtr intptr_4, IntPtr intptr_5, [MarshalAs(UnmanagedType.U4)] uint uint_1, IntPtr intptr_6, ref uint uint_2)
    {
        IntPtr ptr = intptr_5;
        if (bool_4)
        {
            ptr = intptr_4;
        }
        long num = 0L;
        if (IntPtr.Size == 4)
        {
            num = Marshal.ReadInt32(ptr, IntPtr.Size * 2);
        }
        else
        {
            num = Marshal.ReadInt64(ptr, IntPtr.Size * 2);
        }
        object obj2 = hashtable_0[num];
        if (obj2 != null)
        {
            Struct0 struct2 = (Struct0) obj2;
            IntPtr destination = Marshal.AllocCoTaskMem(struct2.byte_0.Length);
            Marshal.Copy(struct2.byte_0, 0, destination, struct2.byte_0.Length);
            if (struct2.bool_0)
            {
                intptr_6 = destination;
                uint_2 = (uint) struct2.byte_0.Length;
                VirtualProtect_1(intptr_6, struct2.byte_0.Length, 0x40, ref int_1);
                return 0;
            }
            Marshal.WriteIntPtr(ptr, IntPtr.Size * 2, destination);
            Marshal.WriteInt32(ptr, IntPtr.Size * 3, struct2.byte_0.Length);
            uint num2 = 0;
            if ((uint_1 == 0xcea1d7d) && !bool_5)
            {
                bool_5 = true;
                return num2;
            }
        }
        return delegate1_1(intptr_3, intptr_4, intptr_5, uint_1, intptr_6, ref uint_2);
    }

    private static void smethod_14()
    {
        try
        {
            RSACryptoServiceProvider.UseMachineKeyStore = true;
        }
        catch
        {
        }
    }

    private static Delegate smethod_15(IntPtr intptr_3, Type type_0)
    {
        return (Delegate) typeof(Marshal).GetMethod("GetDelegateForFunctionPointer", new Type[] { typeof(IntPtr), typeof(Type) }).Invoke(null, new object[] { intptr_3, type_0 });
    }

    /* private scope */ static unsafe void smethod_16()
    {
        BinaryReader reader;
        IEnumerator enumerator;
        if (bool_3)
        {
            return;
        }
        bool_3 = true;
        long num20 = 0L;
        Marshal.ReadIntPtr(new IntPtr((void*) &num20), 0);
        Marshal.ReadInt32(new IntPtr((void*) &num20), 0);
        Marshal.ReadInt64(new IntPtr((void*) &num20), 0);
        Marshal.WriteIntPtr(new IntPtr((void*) &num20), 0, IntPtr.Zero);
        Marshal.WriteInt32(new IntPtr((void*) &num20), 0, 0);
        Marshal.WriteInt64(new IntPtr((void*) &num20), 0, 0L);
        byte[] source = new byte[1];
        Marshal.Copy(source, 0, Marshal.AllocCoTaskMem(8), 1);
        smethod_14();
        bool flag1 = FindResource(Process.GetCurrentProcess().MainModule.BaseAddress, "__", 10) != IntPtr.Zero;
        if ((IntPtr.Size == 4) && (Type.GetType("System.Reflection.ReflectionContext", false) != null))
        {
            using (enumerator = Process.GetCurrentProcess().Modules.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ProcessModule current = (ProcessModule) enumerator.Current;
                    if (current.ModuleName.ToLower() == "clrjit.dll")
                    {
                        Version version = new Version(current.FileVersionInfo.ProductMajorPart, current.FileVersionInfo.ProductMinorPart, current.FileVersionInfo.ProductBuildPart, current.FileVersionInfo.ProductPrivatePart);
                        Version version3 = new Version(4, 0, 0x766f, 0x427c);
                        Version version2 = new Version(4, 0, 0x766f, 0x4601);
                        if ((version >= version3) && (version < version2))
                        {
                            goto Label_01A6;
                        }
                    }
                }
                goto Label_01C3;
            Label_01A6:
                bool_4 = true;
            }
        }
    Label_01C3:
        reader = new BinaryReader(typeof(Class4).Assembly.GetManifestResourceStream("\x009d4\x009a\x00972a\x008d8\x008buv\x008bjf\x0092v\x008d\x009b.\x00974ki9\x0089\x008enn\x009eo4\x008dctw\x0088t")) {
            BaseStream = { Position = 0L }
        };
        byte[] buffer3 = reader.ReadBytes((int) reader.BaseStream.Length);
        byte[] buffer4 = new byte[0x20];
        buffer4[0] = 0x98;
        buffer4[0] = 0x86;
        buffer4[0] = 0x4a;
        buffer4[0] = 0x80;
        buffer4[0] = 0x7e;
        buffer4[0] = 0x1c;
        buffer4[1] = 0x57;
        buffer4[1] = 0x7a;
        buffer4[1] = 0x77;
        buffer4[1] = 0x30;
        buffer4[2] = 0x63;
        buffer4[2] = 0xa6;
        buffer4[2] = 0x7e;
        buffer4[2] = 0x94;
        buffer4[2] = 0x8a;
        buffer4[2] = 0xf5;
        buffer4[3] = 140;
        buffer4[3] = 0xb3;
        buffer4[3] = 0x43;
        buffer4[3] = 0x5b;
        buffer4[3] = 0x86;
        buffer4[4] = 0x63;
        buffer4[4] = 0x6f;
        buffer4[4] = 0x80;
        buffer4[4] = 0xb1;
        buffer4[5] = 0x7d;
        buffer4[5] = 0x9e;
        buffer4[5] = 0xeb;
        buffer4[6] = 0xc1;
        buffer4[6] = 0x74;
        buffer4[6] = 0xa9;
        buffer4[6] = 0x2d;
        buffer4[7] = 0x2b;
        buffer4[7] = 80;
        buffer4[7] = 0x37;
        buffer4[7] = 0x88;
        buffer4[7] = 0xbd;
        buffer4[8] = 0xba;
        buffer4[8] = 0x80;
        buffer4[8] = 0xa6;
        buffer4[9] = 0xba;
        buffer4[9] = 0xa2;
        buffer4[9] = 0x9e;
        buffer4[9] = 0x94;
        buffer4[9] = 60;
        buffer4[10] = 130;
        buffer4[10] = 120;
        buffer4[10] = 90;
        buffer4[10] = 0x86;
        buffer4[10] = 0x80;
        buffer4[10] = 0xd6;
        buffer4[11] = 0x92;
        buffer4[11] = 0x67;
        buffer4[11] = 0x4c;
        buffer4[11] = 0x35;
        buffer4[12] = 0x60;
        buffer4[12] = 0x55;
        buffer4[12] = 0x66;
        buffer4[12] = 0x56;
        buffer4[12] = 0xad;
        buffer4[12] = 0x71;
        buffer4[13] = 150;
        buffer4[13] = 0x99;
        buffer4[13] = 0x70;
        buffer4[13] = 0xa8;
        buffer4[13] = 0x57;
        buffer4[13] = 0x34;
        buffer4[14] = 120;
        buffer4[14] = 0x85;
        buffer4[14] = 130;
        buffer4[14] = 0x5b;
        buffer4[14] = 110;
        buffer4[14] = 0x24;
        buffer4[15] = 0x76;
        buffer4[15] = 110;
        buffer4[15] = 0x84;
        buffer4[15] = 0x24;
        buffer4[0x10] = 0x61;
        buffer4[0x10] = 0x9b;
        buffer4[0x10] = 0x54;
        buffer4[0x10] = 3;
        buffer4[0x11] = 0x9a;
        buffer4[0x11] = 0xa2;
        buffer4[0x11] = 0x8d;
        buffer4[0x11] = 0x8b;
        buffer4[0x11] = 0xb1;
        buffer4[0x12] = 0x5c;
        buffer4[0x12] = 90;
        buffer4[0x12] = 10;
        buffer4[0x13] = 0xa6;
        buffer4[0x13] = 0x5d;
        buffer4[0x13] = 0x5e;
        buffer4[20] = 0x84;
        buffer4[20] = 0x6a;
        buffer4[20] = 180;
        buffer4[20] = 0xd4;
        buffer4[20] = 0x5d;
        buffer4[20] = 0x72;
        buffer4[0x15] = 0x4d;
        buffer4[0x15] = 0x58;
        buffer4[0x15] = 0x8f;
        buffer4[0x15] = 0x74;
        buffer4[0x15] = 110;
        buffer4[0x15] = 0x72;
        buffer4[0x16] = 100;
        buffer4[0x16] = 0x74;
        buffer4[0x16] = 0xa7;
        buffer4[0x16] = 90;
        buffer4[0x16] = 0x4d;
        buffer4[0x17] = 0x93;
        buffer4[0x17] = 0x6f;
        buffer4[0x17] = 0x65;
        buffer4[0x17] = 140;
        buffer4[0x18] = 130;
        buffer4[0x18] = 0x9a;
        buffer4[0x18] = 0x8b;
        buffer4[0x18] = 0x58;
        buffer4[0x18] = 0x2e;
        buffer4[0x19] = 0x75;
        buffer4[0x19] = 0xa5;
        buffer4[0x19] = 0x62;
        buffer4[0x19] = 0xc2;
        buffer4[0x1a] = 0x67;
        buffer4[0x1a] = 0x65;
        buffer4[0x1a] = 0xa8;
        buffer4[0x1a] = 0xfd;
        buffer4[0x1b] = 0x6a;
        buffer4[0x1b] = 0x66;
        buffer4[0x1b] = 0x61;
        buffer4[0x1b] = 170;
        buffer4[0x1c] = 0x98;
        buffer4[0x1c] = 0x6c;
        buffer4[0x1c] = 0x83;
        buffer4[0x1c] = 0x7f;
        buffer4[0x1c] = 0x68;
        buffer4[0x1d] = 0x66;
        buffer4[0x1d] = 0x4f;
        buffer4[0x1d] = 0x77;
        buffer4[0x1d] = 0x1c;
        buffer4[0x1d] = 0x41;
        buffer4[30] = 0x4d;
        buffer4[30] = 70;
        buffer4[30] = 0x62;
        buffer4[30] = 0x8a;
        buffer4[30] = 0xbd;
        buffer4[0x1f] = 0x69;
        buffer4[0x1f] = 0x79;
        buffer4[0x1f] = 0xda;
        byte[] rgbKey = buffer4;
        byte[] buffer6 = new byte[0x10];
        buffer6[0] = 0x88;
        buffer6[0] = 0x54;
        buffer6[0] = 170;
        buffer6[0] = 0x57;
        buffer6[0] = 0x16;
        buffer6[1] = 0xe0;
        buffer6[1] = 60;
        buffer6[1] = 0x84;
        buffer6[2] = 0x93;
        buffer6[2] = 70;
        buffer6[2] = 0x84;
        buffer6[2] = 0x9d;
        buffer6[3] = 0x60;
        buffer6[3] = 0x53;
        buffer6[3] = 90;
        buffer6[3] = 0xa1;
        buffer6[3] = 0x4b;
        buffer6[4] = 160;
        buffer6[4] = 0xcf;
        buffer6[4] = 0x76;
        buffer6[4] = 130;
        buffer6[4] = 0xfb;
        buffer6[5] = 0x5d;
        buffer6[5] = 150;
        buffer6[5] = 0x7f;
        buffer6[5] = 0x6a;
        buffer6[5] = 0x38;
        buffer6[6] = 0xc9;
        buffer6[6] = 0x68;
        buffer6[6] = 0x71;
        buffer6[6] = 0x65;
        buffer6[6] = 0x9b;
        buffer6[6] = 0x13;
        buffer6[7] = 0x74;
        buffer6[7] = 0x51;
        buffer6[7] = 0x6d;
        buffer6[7] = 0x9d;
        buffer6[8] = 0xdd;
        buffer6[8] = 130;
        buffer6[8] = 0x6f;
        buffer6[8] = 0xbc;
        buffer6[9] = 0xae;
        buffer6[9] = 0x3e;
        buffer6[9] = 0x54;
        buffer6[10] = 0xbc;
        buffer6[10] = 0x56;
        buffer6[10] = 0x6f;
        buffer6[10] = 0x5b;
        buffer6[10] = 0x7f;
        buffer6[10] = 0x21;
        buffer6[11] = 0xa9;
        buffer6[11] = 0x86;
        buffer6[11] = 0x9f;
        buffer6[12] = 0x9e;
        buffer6[12] = 0x97;
        buffer6[12] = 0xeb;
        buffer6[13] = 0xa9;
        buffer6[13] = 0x88;
        buffer6[13] = 60;
        buffer6[14] = 0x8d;
        buffer6[14] = 0xce;
        buffer6[14] = 4;
        buffer6[15] = 0xa7;
        buffer6[15] = 0xaf;
        buffer6[15] = 0x7a;
        buffer6[15] = 0x5e;
        buffer6[15] = 0xa1;
        buffer6[15] = 12;
        byte[] array = buffer6;
        Array.Reverse(array);
        byte[] publicKeyToken = typeof(Class4).Assembly.GetName().GetPublicKeyToken();
        if ((publicKeyToken != null) && (publicKeyToken.Length > 0))
        {
            array[1] = publicKeyToken[0];
            array[3] = publicKeyToken[1];
            array[5] = publicKeyToken[2];
            array[7] = publicKeyToken[3];
            array[9] = publicKeyToken[4];
            array[11] = publicKeyToken[5];
            array[13] = publicKeyToken[6];
            array[15] = publicKeyToken[7];
            Array.Clear(publicKeyToken, 0, publicKeyToken.Length);
        }
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Mode = CipherMode.CBC;
        ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, array);
        Array.Clear(rgbKey, 0, rgbKey.Length);
        MemoryStream stream = new MemoryStream();
        CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
        stream2.Write(buffer3, 0, buffer3.Length);
        stream2.FlushFinalBlock();
        byte[] buffer = stream.ToArray();
        Array.Clear(array, 0, array.Length);
        stream.Close();
        stream2.Close();
        reader.Close();
        int num12 = buffer.Length / 8;
        if (((source = buffer) != null) && (source.Length != 0))
        {
            numRef = source;
            goto Label_0CC4;
        }
        fixed (byte* numRef = null)
        {
            int num11;
        Label_0CC4:
            num11 = 0;
            while (num11 < num12)
            {
                IntPtr ptr1 = (IntPtr) (numRef + (num11 * 8));
                ptr1[0] ^= (IntPtr) 0x5fbbaaa9L;
                num11++;
            }
        }
        reader = new BinaryReader(new MemoryStream(buffer)) {
            BaseStream = { Position = 0L }
        };
        long num15 = Marshal.GetHINSTANCE(typeof(Class4).Assembly.GetModules()[0]).ToInt64();
        int num = 0;
        int num3 = 0;
        if ((typeof(Class4).Assembly.Location == null) || (typeof(Class4).Assembly.Location.Length == 0))
        {
            num3 = 0x1c00;
        }
        int num10 = reader.ReadInt32();
        if (reader.ReadInt32() == 1)
        {
            IntPtr ptr5 = IntPtr.Zero;
            Assembly assembly = typeof(Class4).Assembly;
            ptr5 = OpenProcess(0x38, 1, (uint) Process.GetCurrentProcess().Id);
            if (IntPtr.Size == 4)
            {
                int_3 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt32();
            }
            long_0 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt64();
            IntPtr ptr6 = IntPtr.Zero;
            for (int j = 0; j < num10; j++)
            {
                IntPtr ptr = new IntPtr((long_0 + reader.ReadInt32()) - num3);
                VirtualProtect_1(ptr, 4, 4, ref num);
                if (IntPtr.Size == 4)
                {
                    WriteProcessMemory(ptr5, ptr, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr6);
                }
                else
                {
                    WriteProcessMemory(ptr5, ptr, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr6);
                }
                VirtualProtect_1(ptr, 4, num, ref num);
            }
            while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
            {
                int num18 = reader.ReadInt32();
                IntPtr ptr8 = new IntPtr(long_0 + num18);
                int num8 = reader.ReadInt32();
                VirtualProtect_1(ptr8, num8 * 4, 4, ref num);
                for (int k = 0; k < num8; k++)
                {
                    Marshal.WriteInt32(new IntPtr(ptr8.ToInt64() + (k * 4)), reader.ReadInt32());
                }
                VirtualProtect_1(ptr8, num8 * 4, num, ref num);
            }
            CloseHandle(ptr5);
            return;
        }
        for (int i = 0; i < num10; i++)
        {
            IntPtr ptr11 = new IntPtr((num15 + reader.ReadInt32()) - num3);
            VirtualProtect_1(ptr11, 4, 4, ref num);
            Marshal.WriteInt32(ptr11, reader.ReadInt32());
            VirtualProtect_1(ptr11, 4, num, ref num);
        }
        hashtable_0 = new Hashtable(reader.ReadInt32() + 1);
        Struct0 struct3 = new Struct0 {
            byte_0 = new byte[] { 0x2a },
            bool_0 = false
        };
        hashtable_0.Add(0L, struct3);
        bool flag = false;
        while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
        {
            int num4 = reader.ReadInt32() - num3;
            int num5 = reader.ReadInt32();
            flag = false;
            if (num5 >= 0x70000000)
            {
                flag = true;
            }
            int count = reader.ReadInt32();
            byte[] buffer16 = reader.ReadBytes(count);
            Struct0 struct2 = new Struct0 {
                byte_0 = buffer16,
                bool_0 = flag
            };
            hashtable_0.Add(num15 + num4, struct2);
        }
        long_1 = Marshal.GetHINSTANCE(typeof(Class4).Assembly.GetModules()[0]).ToInt64();
        if (IntPtr.Size == 4)
        {
            int_4 = Convert.ToInt32(long_1);
        }
        byte[] bytes = new byte[] { 0x6d, 0x73, 0x63, 0x6f, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
        string str2 = Encoding.UTF8.GetString(bytes);
        IntPtr ptr2 = LoadLibrary(str2);
        if (ptr2 == IntPtr.Zero)
        {
            bytes = new byte[] { 0x63, 0x6c, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
            str2 = Encoding.UTF8.GetString(bytes);
            ptr2 = LoadLibrary(str2);
        }
        byte[] buffer7 = new byte[] { 0x67, 0x65, 0x74, 0x4a, 0x69, 0x74 };
        string str = Encoding.UTF8.GetString(buffer7);
        Delegate2 delegate2 = (Delegate2) smethod_15(GetProcAddress(ptr2, str), typeof(Delegate2));
        IntPtr ptr4 = delegate2();
        long num6 = 0L;
        if (IntPtr.Size == 4)
        {
            num6 = Marshal.ReadInt32(ptr4);
        }
        else
        {
            num6 = Marshal.ReadInt64(ptr4);
        }
        Marshal.ReadIntPtr(ptr4, 0);
        delegate1_0 = new Delegate1(Class4.smethod_13);
        IntPtr zero = IntPtr.Zero;
        zero = Marshal.GetFunctionPointerForDelegate(delegate1_0);
        long num9 = 0L;
        if (IntPtr.Size == 4)
        {
            num9 = Marshal.ReadInt32(new IntPtr(num6));
        }
        else
        {
            num9 = Marshal.ReadInt64(new IntPtr(num6));
        }
        Process currentProcess = Process.GetCurrentProcess();
        try
        {
            foreach (ProcessModule module2 in currentProcess.Modules)
            {
                if (((module2.ModuleName == str2) && ((num9 < module2.BaseAddress.ToInt64()) || (num9 > (module2.BaseAddress.ToInt64() + module2.ModuleMemorySize)))) && (typeof(Class4).Assembly.EntryPoint != null))
                {
                    return;
                }
            }
        }
        catch
        {
        }
        try
        {
            using (enumerator = currentProcess.Modules.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ProcessModule module3 = (ProcessModule) enumerator.Current;
                    if (module3.BaseAddress.ToInt64() == long_1)
                    {
                        goto Label_1359;
                    }
                }
                goto Label_1378;
            Label_1359:
                num3 = 0;
            }
        }
        catch
        {
        }
    Label_1378:
        delegate1_1 = null;
        try
        {
            delegate1_1 = (Delegate1) smethod_15(new IntPtr(num9), typeof(Delegate1));
        }
        catch
        {
            try
            {
                Delegate delegate3 = smethod_15(new IntPtr(num9), typeof(Delegate1));
                delegate1_1 = (Delegate1) Delegate.CreateDelegate(typeof(Delegate1), delegate3.Method);
            }
            catch
            {
            }
        }
        int num13 = 0;
        if (((typeof(Class4).Assembly.EntryPoint == null) || (typeof(Class4).Assembly.EntryPoint.GetParameters().Length != 2)) || ((typeof(Class4).Assembly.Location == null) || (typeof(Class4).Assembly.Location.Length <= 0)))
        {
            try
            {
                ref byte pinned numRef2;
                object obj2 = typeof(Class4).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(typeof(Class4).Assembly.ManifestModule.ModuleHandle);
                if (obj2 is IntPtr)
                {
                    intptr_2 = (IntPtr) obj2;
                }
                if (obj2.GetType().ToString() == "System.Reflection.RuntimeModule")
                {
                    intptr_2 = (IntPtr) obj2.GetType().GetField("m_pData", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(obj2);
                }
                MemoryStream stream3 = new MemoryStream();
                stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                if (IntPtr.Size == 4)
                {
                    stream3.Write(BitConverter.GetBytes(intptr_2.ToInt32()), 0, 4);
                }
                else
                {
                    stream3.Write(BitConverter.GetBytes(intptr_2.ToInt64()), 0, 8);
                }
                stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                stream3.Position = 0L;
                byte[] buffer17 = stream3.ToArray();
                stream3.Close();
                uint nativeSizeOfCode = 0;
                try
                {
                    if (((source = buffer17) != null) && (source.Length != 0))
                    {
                        numRef2 = source;
                    }
                    else
                    {
                        numRef2 = null;
                    }
                    delegate1_0(new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), 0xcea1d7d, new IntPtr((void*) numRef2), ref nativeSizeOfCode);
                }
                finally
                {
                    numRef2 = null;
                }
            }
            catch
            {
            }
            RuntimeHelpers.PrepareDelegate(delegate1_1);
            RuntimeHelpers.PrepareMethod(delegate1_1.Method.MethodHandle);
            RuntimeHelpers.PrepareDelegate(delegate1_0);
            RuntimeHelpers.PrepareMethod(delegate1_0.Method.MethodHandle);
            byte[] buffer10 = null;
            if (IntPtr.Size != 4)
            {
                buffer10 = new byte[] { 
                    0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0x49, 0x39, 0x40, 8, 0x74, 12, 
                    0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0xff, 0xe0, 0x48, 0xb8, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0xff, 0xe0
                 };
            }
            else
            {
                buffer10 = new byte[] { 
                    0x55, 0x8b, 0xec, 0x8b, 0x45, 0x10, 0x81, 120, 4, 0x7d, 0x1d, 0xea, 12, 0x74, 7, 0xb8, 
                    0xb6, 0xb1, 0x4a, 6, 0xeb, 5, 0xb8, 0xb6, 0x92, 0x40, 12, 0x5d, 0xff, 0xe0
                 };
            }
            IntPtr destination = VirtualAlloc(IntPtr.Zero, (uint) buffer10.Length, 0x1000, 0x40);
            byte[] buffer11 = buffer10;
            byte[] buffer12 = null;
            byte[] buffer13 = null;
            byte[] buffer14 = null;
            if (IntPtr.Size == 4)
            {
                buffer14 = BitConverter.GetBytes(intptr_2.ToInt32());
                buffer12 = BitConverter.GetBytes(zero.ToInt32());
                buffer13 = BitConverter.GetBytes(Convert.ToInt32(num9));
            }
            else
            {
                buffer14 = BitConverter.GetBytes(intptr_2.ToInt64());
                buffer12 = BitConverter.GetBytes(zero.ToInt64());
                buffer13 = BitConverter.GetBytes(num9);
            }
            if (IntPtr.Size == 4)
            {
                buffer11[9] = buffer14[0];
                buffer11[10] = buffer14[1];
                buffer11[11] = buffer14[2];
                buffer11[12] = buffer14[3];
                buffer11[0x10] = buffer13[0];
                buffer11[0x11] = buffer13[1];
                buffer11[0x12] = buffer13[2];
                buffer11[0x13] = buffer13[3];
                buffer11[0x17] = buffer12[0];
                buffer11[0x18] = buffer12[1];
                buffer11[0x19] = buffer12[2];
                buffer11[0x1a] = buffer12[3];
            }
            else
            {
                buffer11[2] = buffer14[0];
                buffer11[3] = buffer14[1];
                buffer11[4] = buffer14[2];
                buffer11[5] = buffer14[3];
                buffer11[6] = buffer14[4];
                buffer11[7] = buffer14[5];
                buffer11[8] = buffer14[6];
                buffer11[9] = buffer14[7];
                buffer11[0x12] = buffer13[0];
                buffer11[0x13] = buffer13[1];
                buffer11[20] = buffer13[2];
                buffer11[0x15] = buffer13[3];
                buffer11[0x16] = buffer13[4];
                buffer11[0x17] = buffer13[5];
                buffer11[0x18] = buffer13[6];
                buffer11[0x19] = buffer13[7];
                buffer11[30] = buffer12[0];
                buffer11[0x1f] = buffer12[1];
                buffer11[0x20] = buffer12[2];
                buffer11[0x21] = buffer12[3];
                buffer11[0x22] = buffer12[4];
                buffer11[0x23] = buffer12[5];
                buffer11[0x24] = buffer12[6];
                buffer11[0x25] = buffer12[7];
            }
            Marshal.Copy(buffer11, 0, destination, buffer11.Length);
            bool_6 = false;
            VirtualProtect_1(new IntPtr(num6), IntPtr.Size, 0x40, ref num13);
            Marshal.WriteIntPtr(new IntPtr(num6), destination);
            VirtualProtect_1(new IntPtr(num6), IntPtr.Size, num13, ref num13);
        }
    }

    internal static object smethod_17(Assembly assembly_0)
    {
        try
        {
            if (File.Exists(assembly_0.Location))
            {
                return assembly_0.Location;
            }
        }
        catch
        {
        }
        try
        {
            if (File.Exists(assembly_0.GetName().CodeBase.ToString().Replace("file:///", "")))
            {
                return assembly_0.GetName().CodeBase.ToString().Replace("file:///", "");
            }
        }
        catch
        {
        }
        try
        {
            if (File.Exists(assembly_0.GetType().GetProperty("Location").GetValue(assembly_0, new object[0]).ToString()))
            {
                return assembly_0.GetType().GetProperty("Location").GetValue(assembly_0, new object[0]).ToString();
            }
        }
        catch
        {
        }
        return "";
    }

    [Attribute0(typeof(Attribute0.Class5<object>[]))]
    private static byte[] smethod_18(string string_1)
    {
        byte[] buffer;
        using (FileStream stream = new FileStream(string_1, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            int offset = 0;
            int length = (int) stream.Length;
            buffer = new byte[length];
            while (length > 0)
            {
                int num4 = stream.Read(buffer, offset, length);
                offset += num4;
                length -= num4;
            }
        }
        return buffer;
    }

    [Attribute0(typeof(Attribute0.Class5<object>[]))]
    private static byte[] smethod_19(byte[] byte_4)
    {
        MemoryStream stream = new MemoryStream();
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Key = new byte[] { 
            0xd0, 0x48, 0xe7, 0x5f, 0x8e, 0x35, 0x3e, 0x6a, 0x91, 0xf5, 0xa6, 230, 0x91, 1, 0xc0, 0xda, 
            0x2d, 1, 9, 0x87, 0xae, 0xd6, 0x94, 0x29, 0xb5, 0x5d, 7, 60, 0xea, 210, 0xce, 0x49
         };
        algorithm.IV = new byte[] { 0x30, 0xfd, 0x4a, 0xaf, 220, 0x7b, 0xcd, 0xae, 0x38, 0x97, 0x52, 0x6d, 0xeb, 0x1b, 0x45, 0xfd };
        CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateDecryptor(), CryptoStreamMode.Write);
        stream2.Write(byte_4, 0, byte_4.Length);
        stream2.Close();
        return stream.ToArray();
    }

    private static void smethod_2(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, object object_0)
    {
        uint_1 = uint_2 + smethod_5(((uint_1 + ((uint_2 & uint_4) | (uint_3 & ~uint_4))) + object_0[uint_5]) + uint_0[(int) ((IntPtr) (uint_6 - 1))], ushort_0);
    }

    private static void smethod_3(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, object object_0)
    {
        uint_1 = uint_2 + smethod_5(((uint_1 + ((uint_2 ^ uint_3) ^ uint_4)) + object_0[uint_5]) + uint_0[(int) ((IntPtr) (uint_6 - 1))], ushort_0);
    }

    private static void smethod_4(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, object object_0)
    {
        uint_1 = uint_2 + smethod_5(((uint_1 + (uint_3 ^ (uint_2 | ~uint_4))) + object_0[uint_5]) + uint_0[(int) ((IntPtr) (uint_6 - 1))], ushort_0);
    }

    private static uint smethod_5(uint uint_1, ushort ushort_0)
    {
        return ((uint_1 >> (0x20 - ushort_0)) | (uint_1 << ushort_0));
    }

    internal static bool smethod_6()
    {
        if (!bool_1)
        {
            smethod_8();
            bool_1 = true;
        }
        return bool_2;
    }

    internal static SymmetricAlgorithm smethod_7()
    {
        if (smethod_6())
        {
            return (SymmetricAlgorithm) Activator.CreateInstance(typeof(AesManaged).Assembly.GetType("System.Security.Cryptography.AesCryptoServiceProvider", false));
        }
        return new RijndaelManaged();
    }

    internal static void smethod_8()
    {
        try
        {
            bool_2 = (bool) typeof(RijndaelManaged).Assembly.GetType("System.Security.Cryptography.CryptoConfig", false).GetMethod("get_AllowOnlyFipsAlgorithms", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[0]);
        }
        catch
        {
        }
    }

    internal static byte[] smethod_9(byte[] byte_4)
    {
        if (!smethod_6())
        {
            return new MD5CryptoServiceProvider().ComputeHash(byte_4);
        }
        return smethod_0(byte_4);
    }

    [DllImport("kernel32.dll", SetLastError=true)]
    private static extern IntPtr VirtualAlloc(IntPtr intptr_3, uint uint_1, uint uint_2, uint uint_3);
    [DllImport("kernel32.dll", SetLastError=true)]
    private static extern int VirtualProtect(ref IntPtr intptr_3, int int_5, int int_6, ref int int_7);
    [DllImport("kernel32.dll", EntryPoint="VirtualProtect")]
    private static extern int VirtualProtect_1(IntPtr intptr_3, int int_5, int int_6, ref int int_7);
    [DllImport("kernel32.dll")]
    private static extern int WriteProcessMemory(IntPtr intptr_3, IntPtr intptr_4, [In, Out] byte[] byte_4, uint uint_1, out IntPtr intptr_5);

    internal class Attribute0 : Attribute
    {
        [Attribute0(typeof(Class5<object>[]))]
        public Attribute0(object object_0)
        {
            Class7.smethod_0();
        }

        internal class Class5<T>
        {
            public Class5()
            {
                Class7.smethod_0();
            }
        }
    }

    internal class Class6
    {
        public Class6()
        {
            Class7.smethod_0();
        }

        [Attribute0(typeof(Class4.Attribute0.Class5<object>[]))]
        internal static string smethod_0(string string_0, string string_1)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string_0);
            byte[] buffer3 = new byte[] { 
                0x52, 0x66, 0x68, 110, 0x20, 0x4d, 0x18, 0x22, 0x76, 0xb5, 0x33, 0x11, 0x12, 0x33, 12, 0x6d, 
                10, 0x20, 0x4d, 0x18, 0x22, 0x9e, 0xa1, 0x29, 0x61, 0x1c, 0x76, 0xb5, 5, 0x19, 1, 0x58
             };
            byte[] buffer4 = Class4.smethod_9(Encoding.Unicode.GetBytes(string_1));
            MemoryStream stream = new MemoryStream();
            SymmetricAlgorithm algorithm = Class4.smethod_7();
            algorithm.Key = buffer3;
            algorithm.IV = buffer4;
            CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate uint Delegate1(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate IntPtr Delegate2();

    [Flags]
    private enum Enum0
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct0
    {
        internal bool bool_0;
        internal byte[] byte_0;
    }
}

