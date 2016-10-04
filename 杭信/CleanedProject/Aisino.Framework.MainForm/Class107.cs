using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal class Class107
{
    private static bool bool_0 = false;
    [Attribute8(typeof(Attribute8.Class108<object>[]))]
    private static bool bool_1 = false;
    private static bool bool_2 = false;
    private static bool bool_3 = false;
    private static bool bool_4 = false;
    private static bool bool_5 = false;
    private static bool bool_6 = false;
    private static byte[] byte_0 = new byte[0];
    private static byte[] byte_1 = new byte[0];
    private static byte[] byte_2 = new byte[0];
    private static byte[] byte_3 = new byte[0];
    internal static Delegate32 delegate32_0 = null;
    internal static Delegate32 delegate32_1 = null;
    internal static Hashtable hashtable_0 = new Hashtable();
    private static int int_0 = 0;
    private static int[] int_1 = new int[0];
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
    internal static byte[] smethod_0(byte[] object_0)
    {
        uint[] numArray = new uint[0x10];
        int num5 = 0x1c0 - ((object_0.Length * 8) % 0x200);
        uint num2 = (uint) ((num5 + 0x200) % 0x200);
        if (num2 == 0)
        {
            num2 = 0x200;
        }
        uint num4 = (uint) ((uint)(object_0.Length + (num2 / 8)) + ((ulong) 8L));
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

    private static void smethod_1(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, uint[] object_0)
    {
        uint_1 = uint_2 + smethod_5(((uint_1 + ((uint_2 & uint_3) | (~uint_2 & uint_4))) + object_0[uint_5]) + uint_0[(int) ((IntPtr) (uint_6 - 1))], ushort_0);
    }

    [Attribute8(typeof(Attribute8.Class108<object>[]))]
    /* private scope */ static bool smethod_10(int int_5)
    {
        if (byte_2.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class107).Assembly.GetManifestResourceStream("\x009e\x009f9tf6\x008b\x0087lg\x0090fq\x008ct\x009d\x009a\x0088.\x008a\x008bai\x008ak\x008cfu0b\x0092\x009d\x009e\x008e2mq")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x5e;
            buffer2[0] = 110;
            buffer2[0] = 0x95;
            buffer2[0] = 0x74;
            buffer2[0] = 0xdd;
            buffer2[1] = 0x62;
            buffer2[1] = 0x49;
            buffer2[1] = 0x2f;
            buffer2[1] = 0x80;
            buffer2[2] = 90;
            buffer2[2] = 0xa4;
            buffer2[2] = 0x7a;
            buffer2[2] = 0x8b;
            buffer2[2] = 0xb3;
            buffer2[3] = 0x95;
            buffer2[3] = 0x74;
            buffer2[3] = 0x8b;
            buffer2[3] = 0x56;
            buffer2[3] = 0x53;
            buffer2[4] = 0x95;
            buffer2[4] = 0x9c;
            buffer2[4] = 0x57;
            buffer2[4] = 160;
            buffer2[5] = 0x5c;
            buffer2[5] = 160;
            buffer2[5] = 0xa4;
            buffer2[5] = 0x9d;
            buffer2[5] = 0x1c;
            buffer2[6] = 0x5e;
            buffer2[6] = 0x81;
            buffer2[6] = 0xb5;
            buffer2[6] = 0x4d;
            buffer2[6] = 0x9e;
            buffer2[7] = 13;
            buffer2[7] = 0x91;
            buffer2[7] = 0x58;
            buffer2[7] = 0xf5;
            buffer2[8] = 0x2e;
            buffer2[8] = 160;
            buffer2[8] = 0x79;
            buffer2[8] = 0xa4;
            buffer2[8] = 0x4a;
            buffer2[9] = 0x54;
            buffer2[9] = 0x6f;
            buffer2[9] = 0x89;
            buffer2[9] = 0x35;
            buffer2[9] = 0xa4;
            buffer2[10] = 0x80;
            buffer2[10] = 140;
            buffer2[10] = 0x79;
            buffer2[10] = 0xbf;
            buffer2[11] = 0x85;
            buffer2[11] = 160;
            buffer2[11] = 0x73;
            buffer2[11] = 0x12;
            buffer2[12] = 0x9f;
            buffer2[12] = 0x37;
            buffer2[12] = 0x65;
            buffer2[12] = 0x74;
            buffer2[12] = 0x81;
            buffer2[12] = 0xe0;
            buffer2[13] = 0x7c;
            buffer2[13] = 0x72;
            buffer2[13] = 0x85;
            buffer2[13] = 0xa1;
            buffer2[13] = 0x23;
            buffer2[14] = 160;
            buffer2[14] = 150;
            buffer2[14] = 0x22;
            buffer2[15] = 0x63;
            buffer2[15] = 0x5c;
            buffer2[15] = 0x92;
            buffer2[0x10] = 0xb3;
            buffer2[0x10] = 0x97;
            buffer2[0x10] = 90;
            buffer2[0x10] = 0x29;
            buffer2[0x11] = 90;
            buffer2[0x11] = 0x58;
            buffer2[0x11] = 0xa4;
            buffer2[0x11] = 0x73;
            buffer2[0x11] = 0x95;
            buffer2[0x12] = 0x69;
            buffer2[0x12] = 0x97;
            buffer2[0x12] = 0x8f;
            buffer2[0x12] = 0x94;
            buffer2[0x12] = 0x56;
            buffer2[0x12] = 0x76;
            buffer2[0x13] = 0x7c;
            buffer2[0x13] = 0x94;
            buffer2[0x13] = 240;
            buffer2[20] = 0x7f;
            buffer2[20] = 0x9a;
            buffer2[20] = 0x6a;
            buffer2[20] = 0x54;
            buffer2[20] = 0xa6;
            buffer2[0x15] = 0x92;
            buffer2[0x15] = 0x38;
            buffer2[0x15] = 0xa9;
            buffer2[0x15] = 0xa8;
            buffer2[0x15] = 0xe4;
            buffer2[0x15] = 0x98;
            buffer2[0x16] = 0xeb;
            buffer2[0x16] = 0x8a;
            buffer2[0x16] = 0x62;
            buffer2[0x16] = 0x69;
            buffer2[0x16] = 0xa3;
            buffer2[0x16] = 0x2a;
            buffer2[0x17] = 0x62;
            buffer2[0x17] = 0xb3;
            buffer2[0x17] = 0xea;
            buffer2[0x18] = 150;
            buffer2[0x18] = 0x67;
            buffer2[0x18] = 0xbf;
            buffer2[0x18] = 0x68;
            buffer2[0x18] = 0x3f;
            buffer2[0x18] = 0xa1;
            buffer2[0x19] = 100;
            buffer2[0x19] = 0x61;
            buffer2[0x19] = 170;
            buffer2[0x19] = 7;
            buffer2[0x1a] = 110;
            buffer2[0x1a] = 0xdd;
            buffer2[0x1a] = 0x74;
            buffer2[0x1a] = 0xe1;
            buffer2[0x1b] = 0x65;
            buffer2[0x1b] = 0x62;
            buffer2[0x1b] = 0x7d;
            buffer2[0x1c] = 0x16;
            buffer2[0x1c] = 0xa5;
            buffer2[0x1c] = 0x67;
            buffer2[0x1c] = 0x4c;
            buffer2[0x1d] = 0x62;
            buffer2[0x1d] = 130;
            buffer2[0x1d] = 0xe3;
            buffer2[0x1d] = 0x80;
            buffer2[0x1d] = 7;
            buffer2[30] = 0x57;
            buffer2[30] = 0x54;
            buffer2[30] = 0xd3;
            buffer2[30] = 0xbb;
            buffer2[30] = 90;
            buffer2[30] = 0x90;
            buffer2[0x1f] = 0x7d;
            buffer2[0x1f] = 0x76;
            buffer2[0x1f] = 0x7c;
            buffer2[0x1f] = 0xa2;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x6c;
            buffer4[0] = 0x8b;
            buffer4[0] = 0x3e;
            buffer4[1] = 0x75;
            buffer4[1] = 0x68;
            buffer4[1] = 0x5e;
            buffer4[1] = 0x6f;
            buffer4[1] = 0xf6;
            buffer4[2] = 0x48;
            buffer4[2] = 0xe2;
            buffer4[2] = 0xb8;
            buffer4[3] = 0x72;
            buffer4[3] = 0x68;
            buffer4[3] = 0x85;
            buffer4[3] = 13;
            buffer4[3] = 0xd8;
            buffer4[4] = 0xb1;
            buffer4[4] = 0xae;
            buffer4[4] = 0x7a;
            buffer4[4] = 0x7d;
            buffer4[4] = 0xa7;
            buffer4[4] = 0x9f;
            buffer4[5] = 0x66;
            buffer4[5] = 0xa9;
            buffer4[5] = 0x22;
            buffer4[5] = 0x83;
            buffer4[6] = 0x44;
            buffer4[6] = 0x73;
            buffer4[6] = 0x25;
            buffer4[6] = 0x36;
            buffer4[7] = 0x7b;
            buffer4[7] = 0x6c;
            buffer4[7] = 0x6b;
            buffer4[7] = 0x80;
            buffer4[7] = 0x84;
            buffer4[7] = 0x25;
            buffer4[8] = 0x9a;
            buffer4[8] = 0x9e;
            buffer4[8] = 0x9d;
            buffer4[8] = 0xa8;
            buffer4[8] = 0x35;
            buffer4[9] = 0x25;
            buffer4[9] = 120;
            buffer4[9] = 0x86;
            buffer4[9] = 0x7f;
            buffer4[9] = 0x33;
            buffer4[10] = 0x70;
            buffer4[10] = 0xa7;
            buffer4[10] = 0xa2;
            buffer4[11] = 110;
            buffer4[11] = 0x7e;
            buffer4[11] = 0x62;
            buffer4[11] = 210;
            buffer4[12] = 0x62;
            buffer4[12] = 0x7a;
            buffer4[12] = 0xe2;
            buffer4[13] = 0x7b;
            buffer4[13] = 80;
            buffer4[13] = 0x3d;
            buffer4[13] = 0x41;
            buffer4[13] = 0x7c;
            buffer4[13] = 40;
            buffer4[14] = 120;
            buffer4[14] = 0x9c;
            buffer4[14] = 0x68;
            buffer4[14] = 0x9d;
            buffer4[14] = 0x23;
            buffer4[15] = 0x90;
            buffer4[15] = 150;
            buffer4[15] = 0x7a;
            buffer4[15] = 200;
            byte[] rgbIV = buffer4;
            byte[] publicKeyToken = typeof(Class107).Assembly.GetName().GetPublicKeyToken();
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
        if (byte_0.Length == 0)
        {
            byte_0 = smethod_18(smethod_17(typeof(Class107).Assembly).ToString());
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
            if (byte_0[index] == 0x80)
            {
                return true;
            }
        }
        catch
        {
        }
        return false;
    }

    [Attribute8(typeof(Attribute8.Class108<object>[]))]
    /* private scope */ static string smethod_11(int int_5)
    {
        if (byte_3.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class107).Assembly.GetManifestResourceStream("\x009f2\x008efgm\x0087\x00940qr\x008f8cpd\x0088d.\x0086o\x00999\x00864\x0088\x0093ab\x008643r\x009e\x0097o\x0091")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x43;
            buffer2[0] = 0x97;
            buffer2[0] = 0x7c;
            buffer2[0] = 120;
            buffer2[1] = 0x94;
            buffer2[1] = 0xb8;
            buffer2[1] = 0x56;
            buffer2[1] = 0x30;
            buffer2[2] = 0x88;
            buffer2[2] = 0x63;
            buffer2[2] = 0x70;
            buffer2[2] = 0x8b;
            buffer2[3] = 0x2e;
            buffer2[3] = 0x81;
            buffer2[3] = 0x98;
            buffer2[3] = 0x8e;
            buffer2[3] = 210;
            buffer2[4] = 0x92;
            buffer2[4] = 0x7e;
            buffer2[4] = 170;
            buffer2[5] = 80;
            buffer2[5] = 0x54;
            buffer2[5] = 0x6f;
            buffer2[5] = 0x79;
            buffer2[5] = 150;
            buffer2[5] = 0x69;
            buffer2[6] = 0xc1;
            buffer2[6] = 0x94;
            buffer2[6] = 1;
            buffer2[7] = 0x6a;
            buffer2[7] = 0x60;
            buffer2[7] = 0x23;
            buffer2[8] = 0xa1;
            buffer2[8] = 0xb2;
            buffer2[8] = 0x8f;
            buffer2[8] = 0x4e;
            buffer2[8] = 140;
            buffer2[8] = 0xf4;
            buffer2[9] = 0x7c;
            buffer2[9] = 0x8e;
            buffer2[9] = 0x13;
            buffer2[9] = 0x56;
            buffer2[9] = 0xa7;
            buffer2[9] = 0x63;
            buffer2[10] = 0x71;
            buffer2[10] = 0x61;
            buffer2[10] = 0xc6;
            buffer2[10] = 0x7b;
            buffer2[11] = 0x7c;
            buffer2[11] = 0x7e;
            buffer2[11] = 0x73;
            buffer2[11] = 0xa1;
            buffer2[12] = 0x56;
            buffer2[12] = 0xdd;
            buffer2[12] = 0xd9;
            buffer2[13] = 0x5b;
            buffer2[13] = 0x6b;
            buffer2[13] = 0x56;
            buffer2[13] = 0xa4;
            buffer2[14] = 100;
            buffer2[14] = 0xb6;
            buffer2[14] = 0xad;
            buffer2[14] = 0xba;
            buffer2[14] = 0x4e;
            buffer2[15] = 0x98;
            buffer2[15] = 0x70;
            buffer2[15] = 0xa6;
            buffer2[15] = 0x79;
            buffer2[15] = 0x74;
            buffer2[15] = 0xae;
            buffer2[0x10] = 0x67;
            buffer2[0x10] = 0x63;
            buffer2[0x10] = 0x60;
            buffer2[0x10] = 0x84;
            buffer2[0x10] = 0xc6;
            buffer2[0x10] = 90;
            buffer2[0x11] = 0x63;
            buffer2[0x11] = 0x6f;
            buffer2[0x11] = 0x81;
            buffer2[0x11] = 0x7e;
            buffer2[0x11] = 0xa5;
            buffer2[0x11] = 0xf9;
            buffer2[0x12] = 0x8f;
            buffer2[0x12] = 0x56;
            buffer2[0x12] = 0x5b;
            buffer2[0x13] = 0x5e;
            buffer2[0x13] = 70;
            buffer2[0x13] = 130;
            buffer2[0x13] = 0xce;
            buffer2[0x13] = 0x8e;
            buffer2[0x13] = 0xfd;
            buffer2[20] = 0x6d;
            buffer2[20] = 0x8a;
            buffer2[20] = 0x7c;
            buffer2[20] = 190;
            buffer2[20] = 0x62;
            buffer2[20] = 0xcf;
            buffer2[0x15] = 0x5e;
            buffer2[0x15] = 100;
            buffer2[0x15] = 0x76;
            buffer2[0x15] = 0x72;
            buffer2[0x15] = 0x3d;
            buffer2[0x16] = 0x55;
            buffer2[0x16] = 0x60;
            buffer2[0x16] = 0x79;
            buffer2[0x16] = 3;
            buffer2[0x17] = 0x59;
            buffer2[0x17] = 0x8e;
            buffer2[0x17] = 0x7a;
            buffer2[0x17] = 0x66;
            buffer2[0x17] = 0x7b;
            buffer2[0x17] = 0xc1;
            buffer2[0x18] = 0x9e;
            buffer2[0x18] = 0x85;
            buffer2[0x18] = 0x9c;
            buffer2[0x18] = 0xc0;
            buffer2[0x18] = 0x29;
            buffer2[0x18] = 0x5c;
            buffer2[0x19] = 0x75;
            buffer2[0x19] = 0x9c;
            buffer2[0x19] = 0x98;
            buffer2[0x19] = 0x98;
            buffer2[0x19] = 0xf3;
            buffer2[0x1a] = 0x68;
            buffer2[0x1a] = 0x62;
            buffer2[0x1a] = 0xcc;
            buffer2[0x1a] = 0x22;
            buffer2[0x1a] = 0x26;
            buffer2[0x1a] = 0x66;
            buffer2[0x1b] = 0x5c;
            buffer2[0x1b] = 0x5d;
            buffer2[0x1b] = 0x3b;
            buffer2[0x1b] = 0xb1;
            buffer2[0x1b] = 0x52;
            buffer2[0x1c] = 0x7c;
            buffer2[0x1c] = 100;
            buffer2[0x1c] = 0x89;
            buffer2[0x1c] = 0x98;
            buffer2[0x1c] = 0x8b;
            buffer2[0x1c] = 0x41;
            buffer2[0x1d] = 0x68;
            buffer2[0x1d] = 0x65;
            buffer2[0x1d] = 0xab;
            buffer2[0x1d] = 100;
            buffer2[0x1d] = 110;
            buffer2[0x1d] = 0xc5;
            buffer2[30] = 0x5c;
            buffer2[30] = 0x72;
            buffer2[30] = 0x7d;
            buffer2[30] = 0x3f;
            buffer2[30] = 0x9b;
            buffer2[30] = 0x85;
            buffer2[0x1f] = 0xa6;
            buffer2[0x1f] = 0x8e;
            buffer2[0x1f] = 0xe4;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x57;
            buffer4[0] = 0x9e;
            buffer4[0] = 100;
            buffer4[1] = 0x59;
            buffer4[1] = 0x6c;
            buffer4[1] = 0x86;
            buffer4[1] = 0xa8;
            buffer4[1] = 0x24;
            buffer4[1] = 0x5f;
            buffer4[2] = 0x23;
            buffer4[2] = 0x55;
            buffer4[2] = 0x7a;
            buffer4[2] = 0x2e;
            buffer4[2] = 0xa3;
            buffer4[2] = 0xad;
            buffer4[3] = 0x8e;
            buffer4[3] = 0x7c;
            buffer4[3] = 0x92;
            buffer4[3] = 0xb5;
            buffer4[3] = 80;
            buffer4[3] = 0xd5;
            buffer4[4] = 0x6f;
            buffer4[4] = 0x79;
            buffer4[4] = 120;
            buffer4[4] = 0x69;
            buffer4[4] = 0x5d;
            buffer4[4] = 0xfd;
            buffer4[5] = 0x9a;
            buffer4[5] = 0x9a;
            buffer4[5] = 0x6a;
            buffer4[5] = 0xa4;
            buffer4[6] = 0xb8;
            buffer4[6] = 120;
            buffer4[6] = 0xb1;
            buffer4[6] = 0x8f;
            buffer4[7] = 0x6a;
            buffer4[7] = 140;
            buffer4[7] = 0x86;
            buffer4[7] = 0xbd;
            buffer4[8] = 0x92;
            buffer4[8] = 0xa3;
            buffer4[8] = 70;
            buffer4[8] = 0xd7;
            buffer4[9] = 0xbf;
            buffer4[9] = 0x71;
            buffer4[9] = 0x98;
            buffer4[10] = 0x70;
            buffer4[10] = 0x85;
            buffer4[10] = 0x42;
            buffer4[10] = 0x37;
            buffer4[11] = 0x9a;
            buffer4[11] = 160;
            buffer4[11] = 0xce;
            buffer4[12] = 0xdd;
            buffer4[12] = 0x99;
            buffer4[12] = 0xa3;
            buffer4[13] = 170;
            buffer4[13] = 0xa2;
            buffer4[13] = 100;
            buffer4[13] = 30;
            buffer4[14] = 0x80;
            buffer4[14] = 0xad;
            buffer4[14] = 200;
            buffer4[15] = 90;
            buffer4[15] = 0x98;
            buffer4[15] = 0x54;
            buffer4[15] = 0xc1;
            buffer4[15] = 0x80;
            byte[] array = buffer4;
            Array.Reverse(array);
            byte[] publicKeyToken = typeof(Class107).Assembly.GetName().GetPublicKeyToken();
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
            byte_3 = stream.ToArray();
            stream.Close();
            stream2.Close();
            reader.Close();
        }
        int count = BitConverter.ToInt32(byte_3, int_5);
        try
        {
            return Encoding.Unicode.GetString(byte_3, int_5 + 4, count);
        }
        catch
        {
        }
        return "";
    }

    [Attribute8(typeof(Attribute8.Class108<object>[]))]
    internal static string smethod_12(string string_1)
    {
        "{11111-22222-50001-00000}".Trim();
        byte[] bytes = Convert.FromBase64String(string_1);
        return Encoding.Unicode.GetString(bytes, 0, bytes.Length);
    }

    internal static uint smethod_13(IntPtr intptr_3, IntPtr intptr_4, IntPtr intptr_5, [MarshalAs(UnmanagedType.U4)] uint uint_1, IntPtr intptr_6, ref uint uint_2)
    {
        IntPtr ptr = intptr_5;
        if (bool_2)
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
            Struct148 struct2 = (Struct148) obj2;
            IntPtr destination = Marshal.AllocCoTaskMem(struct2.byte_0.Length);
            Marshal.Copy(struct2.byte_0, 0, destination, struct2.byte_0.Length);
            if (struct2.bool_0)
            {
                intptr_6 = destination;
                uint_2 = (uint) struct2.byte_0.Length;
                VirtualProtect_1(intptr_6, struct2.byte_0.Length, 0x40, ref int_3);
                return 0;
            }
            Marshal.WriteIntPtr(ptr, IntPtr.Size * 2, destination);
            Marshal.WriteInt32(ptr, IntPtr.Size * 3, struct2.byte_0.Length);
            uint num2 = 0;
            if ((uint_1 == 0xcea1d7d) && !bool_1)
            {
                bool_1 = true;
                return num2;
            }
        }
        return delegate32_1(intptr_3, intptr_4, intptr_5, uint_1, intptr_6, ref uint_2);
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

    //没有引用，暂时注释掉
    ///* private scope */ static unsafe void smethod_16()
    //{
    //    BinaryReader reader;
    //    IEnumerator enumerator;
    //    if (bool_4)
    //    {
    //        return;
    //    }
    //    bool_4 = true;
    //    long num19 = 0L;
    //    Marshal.ReadIntPtr(new IntPtr((void*) &num19), 0);
    //    Marshal.ReadInt32(new IntPtr((void*) &num19), 0);
    //    Marshal.ReadInt64(new IntPtr((void*) &num19), 0);
    //    Marshal.WriteIntPtr(new IntPtr((void*) &num19), 0, IntPtr.Zero);
    //    Marshal.WriteInt32(new IntPtr((void*) &num19), 0, 0);
    //    Marshal.WriteInt64(new IntPtr((void*) &num19), 0, 0L);
    //    byte[] source = new byte[1];
    //    Marshal.Copy(source, 0, Marshal.AllocCoTaskMem(8), 1);
    //    smethod_14();
    //    bool flag1 = FindResource(Process.GetCurrentProcess().MainModule.BaseAddress, "__", 10) != IntPtr.Zero;
    //    if ((IntPtr.Size == 4) && (Type.GetType("System.Reflection.ReflectionContext", false) != null))
    //    {
    //        using (enumerator = Process.GetCurrentProcess().Modules.GetEnumerator())
    //        {
    //            while (enumerator.MoveNext())
    //            {
    //                ProcessModule current = (ProcessModule) enumerator.Current;
    //                if (current.ModuleName.ToLower() == "clrjit.dll")
    //                {
    //                    Version version = new Version(current.FileVersionInfo.ProductMajorPart, current.FileVersionInfo.ProductMinorPart, current.FileVersionInfo.ProductBuildPart, current.FileVersionInfo.ProductPrivatePart);
    //                    Version version2 = new Version(4, 0, 0x766f, 0x427c);
    //                    Version version3 = new Version(4, 0, 0x766f, 0x4601);
    //                    if ((version >= version2) && (version < version3))
    //                    {
    //                        goto Label_01A6;
    //                    }
    //                }
    //            }
    //            goto Label_01C3;
    //        Label_01A6:
    //            bool_2 = true;
    //        }
    //    }
    //Label_01C3:
    //    reader = new BinaryReader(typeof(Class107).Assembly.GetManifestResourceStream("\x008efi2\x0095\x008a\x008dq\x008e\x009bf\x0092t\x0093l2a\x0089.dby\x009d\x009cr\x008ecn\x0096sj\x0086p\x0089\x008d\x009c\x009d")) {
    //        BaseStream = { Position = 0L }
    //    };
    //    byte[] buffer12 = reader.ReadBytes((int) reader.BaseStream.Length);
    //    byte[] buffer13 = new byte[0x20];
    //    buffer13[0] = 0x9f;
    //    buffer13[0] = 0x38;
    //    buffer13[0] = 0x86;
    //    buffer13[0] = 0x62;
    //    buffer13[0] = 0xae;
    //    buffer13[1] = 0xa6;
    //    buffer13[1] = 0x8b;
    //    buffer13[1] = 170;
    //    buffer13[1] = 0x17;
    //    buffer13[2] = 0x86;
    //    buffer13[2] = 0x9c;
    //    buffer13[2] = 0x9a;
    //    buffer13[2] = 0x54;
    //    buffer13[2] = 0x91;
    //    buffer13[2] = 0xd1;
    //    buffer13[3] = 0x54;
    //    buffer13[3] = 40;
    //    buffer13[3] = 0xfe;
    //    buffer13[4] = 0x97;
    //    buffer13[4] = 0x63;
    //    buffer13[4] = 0x85;
    //    buffer13[4] = 0x9d;
    //    buffer13[4] = 0x57;
    //    buffer13[5] = 170;
    //    buffer13[5] = 110;
    //    buffer13[5] = 0x63;
    //    buffer13[5] = 100;
    //    buffer13[5] = 0x76;
    //    buffer13[5] = 0xc0;
    //    buffer13[6] = 0x73;
    //    buffer13[6] = 0x9b;
    //    buffer13[6] = 0xc2;
    //    buffer13[7] = 0x75;
    //    buffer13[7] = 120;
    //    buffer13[7] = 0x55;
    //    buffer13[7] = 0x21;
    //    buffer13[8] = 0x63;
    //    buffer13[8] = 0x8e;
    //    buffer13[8] = 0x62;
    //    buffer13[8] = 0x69;
    //    buffer13[9] = 0x86;
    //    buffer13[9] = 0x79;
    //    buffer13[9] = 0x38;
    //    buffer13[9] = 0x30;
    //    buffer13[9] = 0x7e;
    //    buffer13[9] = 0x69;
    //    buffer13[10] = 0x66;
    //    buffer13[10] = 0x54;
    //    buffer13[10] = 80;
    //    buffer13[11] = 0x51;
    //    buffer13[11] = 0x56;
    //    buffer13[11] = 0x6a;
    //    buffer13[11] = 0xbd;
    //    buffer13[11] = 0x69;
    //    buffer13[12] = 0x74;
    //    buffer13[12] = 0x38;
    //    buffer13[12] = 0x5f;
    //    buffer13[12] = 0x6a;
    //    buffer13[12] = 220;
    //    buffer13[13] = 0xc4;
    //    buffer13[13] = 0x65;
    //    buffer13[13] = 0x8b;
    //    buffer13[14] = 0x7c;
    //    buffer13[14] = 0x6a;
    //    buffer13[14] = 0x5e;
    //    buffer13[14] = 0x54;
    //    buffer13[14] = 0x9f;
    //    buffer13[14] = 0x58;
    //    buffer13[15] = 110;
    //    buffer13[15] = 0x68;
    //    buffer13[15] = 0x7a;
    //    buffer13[15] = 0xc9;
    //    buffer13[15] = 100;
    //    buffer13[15] = 0xc4;
    //    buffer13[0x10] = 0x8b;
    //    buffer13[0x10] = 0x66;
    //    buffer13[0x10] = 0x70;
    //    buffer13[0x11] = 0x5b;
    //    buffer13[0x11] = 0xa3;
    //    buffer13[0x11] = 0x73;
    //    buffer13[0x11] = 0x65;
    //    buffer13[0x11] = 0x9a;
    //    buffer13[0x11] = 0x99;
    //    buffer13[0x12] = 0x88;
    //    buffer13[0x12] = 0x9c;
    //    buffer13[0x12] = 0x91;
    //    buffer13[0x12] = 0x87;
    //    buffer13[0x12] = 160;
    //    buffer13[0x12] = 0xb0;
    //    buffer13[0x13] = 0x70;
    //    buffer13[0x13] = 0x6c;
    //    buffer13[0x13] = 0x33;
    //    buffer13[20] = 0xb1;
    //    buffer13[20] = 0x5e;
    //    buffer13[20] = 0x37;
    //    buffer13[0x15] = 0xb6;
    //    buffer13[0x15] = 90;
    //    buffer13[0x15] = 0xb5;
    //    buffer13[0x16] = 0x81;
    //    buffer13[0x16] = 0x95;
    //    buffer13[0x16] = 0xe8;
    //    buffer13[0x16] = 0x4c;
    //    buffer13[0x17] = 120;
    //    buffer13[0x17] = 60;
    //    buffer13[0x17] = 0x99;
    //    buffer13[0x17] = 0x52;
    //    buffer13[0x18] = 0x91;
    //    buffer13[0x18] = 0x69;
    //    buffer13[0x18] = 0x42;
    //    buffer13[0x19] = 0x6c;
    //    buffer13[0x19] = 120;
    //    buffer13[0x19] = 0x73;
    //    buffer13[0x19] = 0x9f;
    //    buffer13[0x19] = 0x80;
    //    buffer13[0x1a] = 130;
    //    buffer13[0x1a] = 0x58;
    //    buffer13[0x1a] = 0xba;
    //    buffer13[0x1a] = 0xae;
    //    buffer13[0x1b] = 0xa4;
    //    buffer13[0x1b] = 0x69;
    //    buffer13[0x1b] = 0x56;
    //    buffer13[0x1b] = 0x85;
    //    buffer13[0x1b] = 0x1c;
    //    buffer13[0x1b] = 20;
    //    buffer13[0x1c] = 0x89;
    //    buffer13[0x1c] = 0xda;
    //    buffer13[0x1c] = 0x16;
    //    buffer13[0x1d] = 0xa4;
    //    buffer13[0x1d] = 0x6a;
    //    buffer13[0x1d] = 0x42;
    //    buffer13[0x1d] = 0x21;
    //    buffer13[0x1d] = 0x83;
    //    buffer13[0x1d] = 0xac;
    //    buffer13[30] = 0x7b;
    //    buffer13[30] = 0x41;
    //    buffer13[30] = 0x5e;
    //    buffer13[30] = 0xb9;
    //    buffer13[30] = 0x66;
    //    buffer13[30] = 0x51;
    //    buffer13[0x1f] = 0x41;
    //    buffer13[0x1f] = 0x56;
    //    buffer13[0x1f] = 250;
    //    byte[] rgbKey = buffer13;
    //    byte[] buffer15 = new byte[0x10];
    //    buffer15[0] = 0x56;
    //    buffer15[0] = 0xba;
    //    buffer15[0] = 0x84;
    //    buffer15[1] = 0x72;
    //    buffer15[1] = 0xcb;
    //    buffer15[1] = 0xa8;
    //    buffer15[1] = 0x84;
    //    buffer15[1] = 0x69;
    //    buffer15[2] = 0x2a;
    //    buffer15[2] = 0x39;
    //    buffer15[2] = 0x83;
    //    buffer15[3] = 0x79;
    //    buffer15[3] = 0x94;
    //    buffer15[3] = 0xc6;
    //    buffer15[4] = 0x43;
    //    buffer15[4] = 150;
    //    buffer15[4] = 0x92;
    //    buffer15[4] = 0xe3;
    //    buffer15[5] = 0x70;
    //    buffer15[5] = 0x2a;
    //    buffer15[5] = 100;
    //    buffer15[5] = 0x31;
    //    buffer15[6] = 0x83;
    //    buffer15[6] = 220;
    //    buffer15[6] = 0x2d;
    //    buffer15[7] = 0x56;
    //    buffer15[7] = 0x36;
    //    buffer15[7] = 0x76;
    //    buffer15[7] = 0x98;
    //    buffer15[7] = 0x9b;
    //    buffer15[8] = 0x79;
    //    buffer15[8] = 0x9b;
    //    buffer15[8] = 0x6f;
    //    buffer15[9] = 0x8d;
    //    buffer15[9] = 90;
    //    buffer15[9] = 0x59;
    //    buffer15[9] = 0x5b;
    //    buffer15[10] = 0x7a;
    //    buffer15[10] = 160;
    //    buffer15[10] = 0x74;
    //    buffer15[10] = 0x62;
    //    buffer15[10] = 0xd0;
    //    buffer15[10] = 20;
    //    buffer15[11] = 0x81;
    //    buffer15[11] = 0x84;
    //    buffer15[11] = 110;
    //    buffer15[11] = 0xf5;
    //    buffer15[12] = 100;
    //    buffer15[12] = 0x57;
    //    buffer15[12] = 0x1b;
    //    buffer15[12] = 0x88;
    //    buffer15[12] = 0xa1;
    //    buffer15[12] = 0xb8;
    //    buffer15[13] = 0xa4;
    //    buffer15[13] = 200;
    //    buffer15[13] = 0x9d;
    //    buffer15[13] = 0x7f;
    //    buffer15[13] = 150;
    //    buffer15[13] = 0x4b;
    //    buffer15[14] = 0x97;
    //    buffer15[14] = 0x72;
    //    buffer15[14] = 120;
    //    buffer15[14] = 5;
    //    buffer15[15] = 0x2a;
    //    buffer15[15] = 0x76;
    //    buffer15[15] = 0xbb;
    //    byte[] array = buffer15;
    //    Array.Reverse(array);
    //    byte[] publicKeyToken = typeof(Class107).Assembly.GetName().GetPublicKeyToken();
    //    if ((publicKeyToken != null) && (publicKeyToken.Length > 0))
    //    {
    //        array[1] = publicKeyToken[0];
    //        array[3] = publicKeyToken[1];
    //        array[5] = publicKeyToken[2];
    //        array[7] = publicKeyToken[3];
    //        array[9] = publicKeyToken[4];
    //        array[11] = publicKeyToken[5];
    //        array[13] = publicKeyToken[6];
    //        array[15] = publicKeyToken[7];
    //        Array.Clear(publicKeyToken, 0, publicKeyToken.Length);
    //    }
    //    SymmetricAlgorithm algorithm = smethod_7();
    //    algorithm.Mode = CipherMode.CBC;
    //    ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, array);
    //    Array.Clear(rgbKey, 0, rgbKey.Length);
    //    MemoryStream stream = new MemoryStream();
    //    CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
    //    stream2.Write(buffer12, 0, buffer12.Length);
    //    stream2.FlushFinalBlock();
    //    byte[] buffer11 = stream.ToArray();
    //    Array.Clear(array, 0, array.Length);
    //    stream.Close();
    //    stream2.Close();
    //    reader.Close();
    //    int num18 = buffer11.Length / 8;
    //    if (((source = buffer11) != null) && (source.Length != 0))
    //    {
    //        numRef = source;
    //        goto Label_0C75;
    //    }
    //    fixed (byte* numRef = null)
    //    {
    //        int num12;
    //    Label_0C75:
    //        num12 = 0;
    //        while (num12 < num18)
    //        {
    //            IntPtr ptr1 = (IntPtr) (numRef + (num12 * 8));
    //            ptr1[0] ^= (IntPtr) 0x3f135968L;
    //            num12++;
    //        }
    //    }
    //    reader = new BinaryReader(new MemoryStream(buffer11)) {
    //        BaseStream = { Position = 0L }
    //    };
    //    long num8 = Marshal.GetHINSTANCE(typeof(Class107).Assembly.GetModules()[0]).ToInt64();
    //    int num6 = 0;
    //    int num = 0;
    //    if ((typeof(Class107).Assembly.Location == null) || (typeof(Class107).Assembly.Location.Length == 0))
    //    {
    //        num = 0x1c00;
    //    }
    //    int num10 = reader.ReadInt32();
    //    if (reader.ReadInt32() == 1)
    //    {
    //        IntPtr ptr3 = IntPtr.Zero;
    //        Assembly assembly = typeof(Class107).Assembly;
    //        ptr3 = OpenProcess(0x38, 1, (uint) Process.GetCurrentProcess().Id);
    //        if (IntPtr.Size == 4)
    //        {
    //            int_0 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt32();
    //        }
    //        long_0 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt64();
    //        IntPtr ptr5 = IntPtr.Zero;
    //        for (int j = 0; j < num10; j++)
    //        {
    //            IntPtr ptr4 = new IntPtr((long_0 + reader.ReadInt32()) - num);
    //            VirtualProtect_1(ptr4, 4, 4, ref num6);
    //            if (IntPtr.Size == 4)
    //            {
    //                WriteProcessMemory(ptr3, ptr4, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr5);
    //            }
    //            else
    //            {
    //                WriteProcessMemory(ptr3, ptr4, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr5);
    //            }
    //            VirtualProtect_1(ptr4, 4, num6, ref num6);
    //        }
    //        while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
    //        {
    //            int num15 = reader.ReadInt32();
    //            IntPtr ptr2 = new IntPtr(long_0 + num15);
    //            int num5 = reader.ReadInt32();
    //            VirtualProtect_1(ptr2, num5 * 4, 4, ref num6);
    //            for (int k = 0; k < num5; k++)
    //            {
    //                Marshal.WriteInt32(new IntPtr(ptr2.ToInt64() + (k * 4)), reader.ReadInt32());
    //            }
    //            VirtualProtect_1(ptr2, num5 * 4, num6, ref num6);
    //        }
    //        CloseHandle(ptr3);
    //        return;
    //    }
    //    for (int i = 0; i < num10; i++)
    //    {
    //        IntPtr ptr9 = new IntPtr((num8 + reader.ReadInt32()) - num);
    //        VirtualProtect_1(ptr9, 4, 4, ref num6);
    //        Marshal.WriteInt32(ptr9, reader.ReadInt32());
    //        VirtualProtect_1(ptr9, 4, num6, ref num6);
    //    }
    //    hashtable_0 = new Hashtable(reader.ReadInt32() + 1);
    //    Struct148 struct3 = new Struct148 {
    //        byte_0 = new byte[] { 0x2a },
    //        bool_0 = false
    //    };
    //    hashtable_0.Add(0L, struct3);
    //    bool flag = false;
    //    while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
    //    {
    //        int num2 = reader.ReadInt32() - num;
    //        int num3 = reader.ReadInt32();
    //        flag = false;
    //        if (num3 >= 0x70000000)
    //        {
    //            flag = true;
    //        }
    //        int count = reader.ReadInt32();
    //        byte[] buffer2 = reader.ReadBytes(count);
    //        Struct148 struct2 = new Struct148 {
    //            byte_0 = buffer2,
    //            bool_0 = flag
    //        };
    //        hashtable_0.Add(num8 + num2, struct2);
    //    }
    //    long_1 = Marshal.GetHINSTANCE(typeof(Class107).Assembly.GetModules()[0]).ToInt64();
    //    if (IntPtr.Size == 4)
    //    {
    //        int_4 = Convert.ToInt32(long_1);
    //    }
    //    byte[] bytes = new byte[] { 0x6d, 0x73, 0x63, 0x6f, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
    //    string str = Encoding.UTF8.GetString(bytes);
    //    IntPtr ptr = LoadLibrary(str);
    //    if (ptr == IntPtr.Zero)
    //    {
    //        bytes = new byte[] { 0x63, 0x6c, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
    //        str = Encoding.UTF8.GetString(bytes);
    //        ptr = LoadLibrary(str);
    //    }
    //    byte[] buffer16 = new byte[] { 0x67, 0x65, 0x74, 0x4a, 0x69, 0x74 };
    //    string str2 = Encoding.UTF8.GetString(buffer16);
    //    Delegate33 delegate3 = (Delegate33) smethod_15(GetProcAddress(ptr, str2), typeof(Delegate33));
    //    IntPtr ptr6 = delegate3();
    //    long num17 = 0L;
    //    if (IntPtr.Size == 4)
    //    {
    //        num17 = Marshal.ReadInt32(ptr6);
    //    }
    //    else
    //    {
    //        num17 = Marshal.ReadInt64(ptr6);
    //    }
    //    Marshal.ReadIntPtr(ptr6, 0);
    //    delegate32_0 = new Delegate32(Class107.smethod_13);
    //    IntPtr zero = IntPtr.Zero;
    //    zero = Marshal.GetFunctionPointerForDelegate(delegate32_0);
    //    long num13 = 0L;
    //    if (IntPtr.Size == 4)
    //    {
    //        num13 = Marshal.ReadInt32(new IntPtr(num17));
    //    }
    //    else
    //    {
    //        num13 = Marshal.ReadInt64(new IntPtr(num17));
    //    }
    //    Process currentProcess = Process.GetCurrentProcess();
    //    try
    //    {
    //        foreach (ProcessModule module in currentProcess.Modules)
    //        {
    //            if (((module.ModuleName == str) && ((num13 < module.BaseAddress.ToInt64()) || (num13 > (module.BaseAddress.ToInt64() + module.ModuleMemorySize)))) && (typeof(Class107).Assembly.EntryPoint != null))
    //            {
    //                return;
    //            }
    //        }
    //    }
    //    catch
    //    {
    //    }
    //    try
    //    {
    //        using (enumerator = currentProcess.Modules.GetEnumerator())
    //        {
    //            while (enumerator.MoveNext())
    //            {
    //                ProcessModule module2 = (ProcessModule) enumerator.Current;
    //                if (module2.BaseAddress.ToInt64() == long_1)
    //                {
    //                    goto Label_12F4;
    //                }
    //            }
    //            goto Label_1312;
    //        Label_12F4:
    //            num = 0;
    //        }
    //    }
    //    catch
    //    {
    //    }
    //Label_1312:
    //    delegate32_1 = null;
    //    try
    //    {
    //        delegate32_1 = (Delegate32) smethod_15(new IntPtr(num13), typeof(Delegate32));
    //    }
    //    catch
    //    {
    //        try
    //        {
    //            Delegate delegate2 = smethod_15(new IntPtr(num13), typeof(Delegate32));
    //            delegate32_1 = (Delegate32) Delegate.CreateDelegate(typeof(Delegate32), delegate2.Method);
    //        }
    //        catch
    //        {
    //        }
    //    }
    //    int num16 = 0;
    //    if (((typeof(Class107).Assembly.EntryPoint == null) || (typeof(Class107).Assembly.EntryPoint.GetParameters().Length != 2)) || ((typeof(Class107).Assembly.Location == null) || (typeof(Class107).Assembly.Location.Length <= 0)))
    //    {
    //        try
    //        {
    //            ref byte pinned numRef2;
    //            object obj2 = typeof(Class107).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(typeof(Class107).Assembly.ManifestModule.ModuleHandle);
    //            if (obj2 is IntPtr)
    //            {
    //                intptr_1 = (IntPtr) obj2;
    //            }
    //            if (obj2.GetType().ToString() == "System.Reflection.RuntimeModule")
    //            {
    //                intptr_1 = (IntPtr) obj2.GetType().GetField("m_pData", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(obj2);
    //            }
    //            MemoryStream stream3 = new MemoryStream();
    //            stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
    //            if (IntPtr.Size == 4)
    //            {
    //                stream3.Write(BitConverter.GetBytes(intptr_1.ToInt32()), 0, 4);
    //            }
    //            else
    //            {
    //                stream3.Write(BitConverter.GetBytes(intptr_1.ToInt64()), 0, 8);
    //            }
    //            stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
    //            stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
    //            stream3.Position = 0L;
    //            byte[] buffer17 = stream3.ToArray();
    //            stream3.Close();
    //            uint nativeSizeOfCode = 0;
    //            try
    //            {
    //                if (((source = buffer17) != null) && (source.Length != 0))
    //                {
    //                    numRef2 = source;
    //                }
    //                else
    //                {
    //                    numRef2 = null;
    //                }
    //                delegate32_0(new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), 0xcea1d7d, new IntPtr((void*) numRef2), ref nativeSizeOfCode);
    //            }
    //            finally
    //            {
    //                numRef2 = null;
    //            }
    //        }
    //        catch
    //        {
    //        }
    //        RuntimeHelpers.PrepareDelegate(delegate32_1);
    //        RuntimeHelpers.PrepareMethod(delegate32_1.Method.MethodHandle);
    //        RuntimeHelpers.PrepareDelegate(delegate32_0);
    //        RuntimeHelpers.PrepareMethod(delegate32_0.Method.MethodHandle);
    //        byte[] buffer9 = null;
    //        if (IntPtr.Size != 4)
    //        {
    //            buffer9 = new byte[] { 
    //                0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0x49, 0x39, 0x40, 8, 0x74, 12, 
    //                0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0xff, 0xe0, 0x48, 0xb8, 0, 0, 
    //                0, 0, 0, 0, 0, 0, 0xff, 0xe0
    //             };
    //        }
    //        else
    //        {
    //            buffer9 = new byte[] { 
    //                0x55, 0x8b, 0xec, 0x8b, 0x45, 0x10, 0x81, 120, 4, 0x7d, 0x1d, 0xea, 12, 0x74, 7, 0xb8, 
    //                0xb6, 0xb1, 0x4a, 6, 0xeb, 5, 0xb8, 0xb6, 0x92, 0x40, 12, 0x5d, 0xff, 0xe0
    //             };
    //        }
    //        IntPtr destination = VirtualAlloc(IntPtr.Zero, (uint) buffer9.Length, 0x1000, 0x40);
    //        byte[] buffer3 = buffer9;
    //        byte[] buffer6 = null;
    //        byte[] buffer5 = null;
    //        byte[] buffer4 = null;
    //        if (IntPtr.Size == 4)
    //        {
    //            buffer4 = BitConverter.GetBytes(intptr_1.ToInt32());
    //            buffer6 = BitConverter.GetBytes(zero.ToInt32());
    //            buffer5 = BitConverter.GetBytes(Convert.ToInt32(num13));
    //        }
    //        else
    //        {
    //            buffer4 = BitConverter.GetBytes(intptr_1.ToInt64());
    //            buffer6 = BitConverter.GetBytes(zero.ToInt64());
    //            buffer5 = BitConverter.GetBytes(num13);
    //        }
    //        if (IntPtr.Size == 4)
    //        {
    //            buffer3[9] = buffer4[0];
    //            buffer3[10] = buffer4[1];
    //            buffer3[11] = buffer4[2];
    //            buffer3[12] = buffer4[3];
    //            buffer3[0x10] = buffer5[0];
    //            buffer3[0x11] = buffer5[1];
    //            buffer3[0x12] = buffer5[2];
    //            buffer3[0x13] = buffer5[3];
    //            buffer3[0x17] = buffer6[0];
    //            buffer3[0x18] = buffer6[1];
    //            buffer3[0x19] = buffer6[2];
    //            buffer3[0x1a] = buffer6[3];
    //        }
    //        else
    //        {
    //            buffer3[2] = buffer4[0];
    //            buffer3[3] = buffer4[1];
    //            buffer3[4] = buffer4[2];
    //            buffer3[5] = buffer4[3];
    //            buffer3[6] = buffer4[4];
    //            buffer3[7] = buffer4[5];
    //            buffer3[8] = buffer4[6];
    //            buffer3[9] = buffer4[7];
    //            buffer3[0x12] = buffer5[0];
    //            buffer3[0x13] = buffer5[1];
    //            buffer3[20] = buffer5[2];
    //            buffer3[0x15] = buffer5[3];
    //            buffer3[0x16] = buffer5[4];
    //            buffer3[0x17] = buffer5[5];
    //            buffer3[0x18] = buffer5[6];
    //            buffer3[0x19] = buffer5[7];
    //            buffer3[30] = buffer6[0];
    //            buffer3[0x1f] = buffer6[1];
    //            buffer3[0x20] = buffer6[2];
    //            buffer3[0x21] = buffer6[3];
    //            buffer3[0x22] = buffer6[4];
    //            buffer3[0x23] = buffer6[5];
    //            buffer3[0x24] = buffer6[6];
    //            buffer3[0x25] = buffer6[7];
    //        }
    //        Marshal.Copy(buffer3, 0, destination, buffer3.Length);
    //        bool_5 = false;
    //        VirtualProtect_1(new IntPtr(num17), IntPtr.Size, 0x40, ref num16);
    //        Marshal.WriteIntPtr(new IntPtr(num17), destination);
    //        VirtualProtect_1(new IntPtr(num17), IntPtr.Size, num16, ref num16);
    //    }
    //}

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

    [Attribute8(typeof(Attribute8.Class108<object>[]))]
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

    [Attribute8(typeof(Attribute8.Class108<object>[]))]
    private static byte[] smethod_19(byte[] byte_4)
    {
        MemoryStream stream = new MemoryStream();
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Key = new byte[] { 
            0xce, 0xc9, 0x8f, 6, 0x25, 0x4a, 0x67, 0xb8, 0xa4, 0x5d, 210, 0xef, 0x8e, 0x4c, 0xe4, 0xe4, 
            0xc7, 0xcb, 0x56, 0x42, 120, 0x57, 0xfc, 0xbf, 0xeb, 0xb8, 0xf7, 0x4f, 0x1f, 0xf6, 0xdf, 0x7b
         };
        algorithm.IV = new byte[] { 0xe9, 0xef, 0xf9, 0xc7, 0x47, 0x4c, 0x73, 0x1a, 0xd8, 0x25, 0x42, 0xab, 0xa7, 8, 8, 0x99 };
        CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateDecryptor(), CryptoStreamMode.Write);
        stream2.Write(byte_4, 0, byte_4.Length);
        stream2.Close();
        return stream.ToArray();
    }

    private static void smethod_2(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, uint[] object_0)
    {
        uint_1 = uint_2 + smethod_5(((uint_1 + ((uint_2 & uint_4) | (uint_3 & ~uint_4))) + object_0[uint_5]) + uint_0[(int) ((IntPtr) (uint_6 - 1))], ushort_0);
    }

    private static void smethod_3(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, uint[] object_0)
    {
        uint_1 = uint_2 + smethod_5(((uint_1 + ((uint_2 ^ uint_3) ^ uint_4)) + object_0[uint_5]) + uint_0[(int) ((IntPtr) (uint_6 - 1))], ushort_0);
    }

    private static void smethod_4(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, uint[] object_0)
    {
        uint_1 = uint_2 + smethod_5(((uint_1 + (uint_3 ^ (uint_2 | ~uint_4))) + object_0[uint_5]) + uint_0[(int) ((IntPtr) (uint_6 - 1))], ushort_0);
    }

    private static uint smethod_5(uint uint_1, ushort ushort_0)
    {
        return ((uint_1 >> (0x20 - ushort_0)) | (uint_1 << ushort_0));
    }

    internal static bool smethod_6()
    {
        if (!bool_6)
        {
            smethod_8();
            bool_6 = true;
        }
        return bool_0;
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
            bool_0 = (bool) typeof(RijndaelManaged).Assembly.GetType("System.Security.Cryptography.CryptoConfig", false).GetMethod("get_AllowOnlyFipsAlgorithms", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[0]);
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

    internal class Attribute8 : Attribute
    {
        [Attribute8(typeof(Class108<object>[]))]
        public Attribute8(object object_0)
        {
            Class110.smethod_0();
        }

        internal class Class108<T>
        {
            public Class108()
            {
                Class110.smethod_0();
            }
        }
    }

    internal class Class109
    {
        public Class109()
        {
            Class110.smethod_0();
        }

        [Attribute8(typeof(Class107.Attribute8.Class108<object>[]))]
        internal static string smethod_0(string string_0, string string_1)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string_0);
            byte[] buffer3 = new byte[] { 
                0x52, 0x66, 0x68, 110, 0x20, 0x4d, 0x18, 0x22, 0x76, 0xb5, 0x33, 0x11, 0x12, 0x33, 12, 0x6d, 
                10, 0x20, 0x4d, 0x18, 0x22, 0x9e, 0xa1, 0x29, 0x61, 0x1c, 0x76, 0xb5, 5, 0x19, 1, 0x58
             };
            byte[] buffer4 = Class107.smethod_9(Encoding.Unicode.GetBytes(string_1));
            MemoryStream stream = new MemoryStream();
            SymmetricAlgorithm algorithm = Class107.smethod_7();
            algorithm.Key = buffer3;
            algorithm.IV = buffer4;
            CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate uint Delegate32(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate IntPtr Delegate33();

    [Flags]
    private enum Enum11
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct148
    {
        internal bool bool_0;
        internal byte[] byte_0;
    }
}

