using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal class Class75
{
    private static bool bool_0 = false;
    private static bool bool_1 = false;
    private static bool bool_2 = false;
    private static bool bool_3 = false;
    private static bool bool_4 = false;
    private static bool bool_5 = false;
    [Attribute7(typeof(Attribute7.Class76<object>[]))]
    private static bool bool_6 = false;
    private static byte[] byte_0 = new byte[0];
    private static byte[] byte_1 = new byte[0];
    private static byte[] byte_2 = new byte[0];
    private static byte[] byte_3 = new byte[0];
    internal static Delegate29 delegate29_0 = null;
    internal static Delegate29 delegate29_1 = null;
    internal static Hashtable hashtable_0 = new Hashtable();
    private static int int_0 = 0;
    private static int int_1 = 0;
    private static int int_2 = 1;
    private static int[] int_3 = new int[0];
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
        uint num4 = (uint) ((ulong)(object_0.Length + (num2 / 8)) + ((ulong) 8L));
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

    [Attribute7(typeof(Attribute7.Class76<object>[]))]
    /* private scope */ static bool smethod_10(int int_5)
    {
        if (byte_0.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class75).Assembly.GetManifestResourceStream("\x0095\x008dm\x0086xd\x008bw2dlrimr\x008c\x0087u.k\x00949rp\x009d\x008c3\x009c\x009cv0bf\x0086e78")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x75;
            buffer2[0] = 0x94;
            buffer2[0] = 110;
            buffer2[0] = 11;
            buffer2[1] = 0x6b;
            buffer2[1] = 0x9c;
            buffer2[1] = 0x93;
            buffer2[1] = 0x51;
            buffer2[1] = 0x77;
            buffer2[2] = 170;
            buffer2[2] = 0x9a;
            buffer2[2] = 0xcf;
            buffer2[2] = 0x5d;
            buffer2[2] = 0xe9;
            buffer2[3] = 0x6c;
            buffer2[3] = 0x8b;
            buffer2[3] = 0x94;
            buffer2[4] = 0xb9;
            buffer2[4] = 40;
            buffer2[4] = 0x55;
            buffer2[5] = 0x86;
            buffer2[5] = 0x8a;
            buffer2[5] = 0xbc;
            buffer2[5] = 110;
            buffer2[6] = 110;
            buffer2[6] = 120;
            buffer2[6] = 0x69;
            buffer2[6] = 0x5c;
            buffer2[6] = 12;
            buffer2[6] = 0xb6;
            buffer2[7] = 0xf1;
            buffer2[7] = 0xa5;
            buffer2[7] = 0x8d;
            buffer2[8] = 0x7a;
            buffer2[8] = 0x97;
            buffer2[8] = 0x97;
            buffer2[8] = 0xa4;
            buffer2[8] = 0x20;
            buffer2[9] = 0x92;
            buffer2[9] = 0x6d;
            buffer2[9] = 0x49;
            buffer2[9] = 0x86;
            buffer2[9] = 0x4b;
            buffer2[10] = 0x97;
            buffer2[10] = 0x5e;
            buffer2[10] = 0x2f;
            buffer2[10] = 0xa3;
            buffer2[10] = 0x17;
            buffer2[11] = 0x39;
            buffer2[11] = 0x2c;
            buffer2[11] = 0x2b;
            buffer2[12] = 0x4e;
            buffer2[12] = 0x83;
            buffer2[12] = 0x92;
            buffer2[12] = 0xc4;
            buffer2[12] = 0xa6;
            buffer2[12] = 15;
            buffer2[13] = 0xa4;
            buffer2[13] = 0xd6;
            buffer2[13] = 0x73;
            buffer2[13] = 0x40;
            buffer2[14] = 0x68;
            buffer2[14] = 0x90;
            buffer2[14] = 0x91;
            buffer2[14] = 0x35;
            buffer2[15] = 0xb6;
            buffer2[15] = 0x9e;
            buffer2[15] = 0xa1;
            buffer2[15] = 0x56;
            buffer2[15] = 120;
            buffer2[15] = 0xfd;
            buffer2[0x10] = 0x58;
            buffer2[0x10] = 0xbf;
            buffer2[0x10] = 0x69;
            buffer2[0x10] = 0x4b;
            buffer2[0x11] = 0x9a;
            buffer2[0x11] = 0x84;
            buffer2[0x11] = 0x4e;
            buffer2[0x12] = 0x68;
            buffer2[0x12] = 0x5e;
            buffer2[0x12] = 0x84;
            buffer2[0x12] = 160;
            buffer2[0x12] = 1;
            buffer2[0x13] = 0x8a;
            buffer2[0x13] = 0x73;
            buffer2[0x13] = 0x81;
            buffer2[0x13] = 0x6b;
            buffer2[0x13] = 0xd9;
            buffer2[20] = 0x52;
            buffer2[20] = 0x1d;
            buffer2[20] = 0x68;
            buffer2[20] = 160;
            buffer2[20] = 0xc1;
            buffer2[0x15] = 100;
            buffer2[0x15] = 0x84;
            buffer2[0x15] = 0x8b;
            buffer2[0x15] = 0xb5;
            buffer2[0x15] = 0x6c;
            buffer2[0x15] = 0x23;
            buffer2[0x16] = 0x52;
            buffer2[0x16] = 0x92;
            buffer2[0x16] = 0x1b;
            buffer2[0x17] = 0x8a;
            buffer2[0x17] = 0x4f;
            buffer2[0x17] = 0x83;
            buffer2[0x17] = 0x81;
            buffer2[0x17] = 90;
            buffer2[0x17] = 0x9e;
            buffer2[0x18] = 80;
            buffer2[0x18] = 70;
            buffer2[0x18] = 0xc6;
            buffer2[0x18] = 0x11;
            buffer2[0x18] = 12;
            buffer2[0x19] = 0x8f;
            buffer2[0x19] = 0x7c;
            buffer2[0x19] = 0xa6;
            buffer2[0x19] = 0x7c;
            buffer2[0x19] = 0x57;
            buffer2[0x19] = 0x4f;
            buffer2[0x1a] = 0x6c;
            buffer2[0x1a] = 0x68;
            buffer2[0x1a] = 0x7b;
            buffer2[0x1a] = 0x73;
            buffer2[0x1a] = 0x91;
            buffer2[0x1b] = 0xba;
            buffer2[0x1b] = 0x18;
            buffer2[0x1b] = 0x71;
            buffer2[0x1b] = 0x85;
            buffer2[0x1b] = 0xf7;
            buffer2[0x1c] = 0x79;
            buffer2[0x1c] = 0x61;
            buffer2[0x1c] = 0x62;
            buffer2[0x1c] = 0x95;
            buffer2[0x1c] = 0xa2;
            buffer2[0x1d] = 0x80;
            buffer2[0x1d] = 0x22;
            buffer2[0x1d] = 0x95;
            buffer2[0x1d] = 0x5f;
            buffer2[0x1d] = 0xb9;
            buffer2[0x1d] = 0xb7;
            buffer2[30] = 0x39;
            buffer2[30] = 0x75;
            buffer2[30] = 0x37;
            buffer2[30] = 0x5c;
            buffer2[30] = 0x6c;
            buffer2[0x1f] = 0x60;
            buffer2[0x1f] = 0x9e;
            buffer2[0x1f] = 0x76;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 180;
            buffer4[0] = 0x5e;
            buffer4[0] = 0x9b;
            buffer4[1] = 0xa2;
            buffer4[1] = 0x92;
            buffer4[1] = 0x61;
            buffer4[1] = 0x6c;
            buffer4[1] = 11;
            buffer4[2] = 0x7c;
            buffer4[2] = 0xae;
            buffer4[2] = 0x7f;
            buffer4[2] = 110;
            buffer4[2] = 0xb8;
            buffer4[3] = 0x60;
            buffer4[3] = 0x8e;
            buffer4[3] = 0x80;
            buffer4[3] = 0x88;
            buffer4[3] = 0x9e;
            buffer4[3] = 190;
            buffer4[4] = 0x40;
            buffer4[4] = 0x7a;
            buffer4[4] = 0xca;
            buffer4[5] = 0x7c;
            buffer4[5] = 0x66;
            buffer4[5] = 0x23;
            buffer4[6] = 0x57;
            buffer4[6] = 0x55;
            buffer4[6] = 10;
            buffer4[7] = 110;
            buffer4[7] = 0x70;
            buffer4[7] = 0x80;
            buffer4[7] = 0x98;
            buffer4[7] = 0x92;
            buffer4[7] = 0xc4;
            buffer4[8] = 0x9e;
            buffer4[8] = 0x5f;
            buffer4[8] = 0xcc;
            buffer4[8] = 0x5b;
            buffer4[8] = 0xed;
            buffer4[8] = 0xe1;
            buffer4[9] = 0x76;
            buffer4[9] = 0x89;
            buffer4[9] = 6;
            buffer4[9] = 0x72;
            buffer4[9] = 0x8e;
            buffer4[9] = 20;
            buffer4[10] = 0xa4;
            buffer4[10] = 0x7e;
            buffer4[10] = 0x70;
            buffer4[10] = 0x89;
            buffer4[10] = 0x5f;
            buffer4[10] = 0x4d;
            buffer4[11] = 0x58;
            buffer4[11] = 0x7b;
            buffer4[11] = 0xa5;
            buffer4[11] = 0x4b;
            buffer4[11] = 0x95;
            buffer4[12] = 0x80;
            buffer4[12] = 0x9b;
            buffer4[12] = 0x4a;
            buffer4[13] = 0x76;
            buffer4[13] = 0x95;
            buffer4[13] = 0xb8;
            buffer4[13] = 0x8f;
            buffer4[14] = 0xa4;
            buffer4[14] = 150;
            buffer4[14] = 0xa5;
            buffer4[14] = 8;
            buffer4[14] = 0x84;
            buffer4[14] = 0x3f;
            buffer4[15] = 0xcc;
            buffer4[15] = 0x7f;
            buffer4[15] = 0xa8;
            byte[] rgbIV = buffer4;
            byte[] publicKeyToken = typeof(Class75).Assembly.GetName().GetPublicKeyToken();
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
            byte_0 = stream.ToArray();
            stream.Close();
            stream2.Close();
            reader.Close();
        }
        if (byte_3.Length == 0)
        {
            byte_3 = smethod_18(smethod_17(typeof(Class75).Assembly).ToString());
        }
        int index = 0;
        try
        {
            index = BitConverter.ToInt32(new byte[] { byte_0[int_5], byte_0[int_5 + 1], byte_0[int_5 + 2], byte_0[int_5 + 3] }, 0);
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

    [Attribute7(typeof(Attribute7.Class76<object>[]))]
    /* private scope */ static string smethod_11(int int_5)
    {
        if (byte_1.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class75).Assembly.GetManifestResourceStream("\x0087\x008b\x0098kad\x0087\x008b\x00860\x009d\x0086y\x009b\x0088q\x00930.\x009b\x0093\x0099\x0099\x0099x\x0088\x008cr\x0088w\x00945\x008f9se\x0091")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x43;
            buffer2[0] = 0x70;
            buffer2[0] = 120;
            buffer2[0] = 0x86;
            buffer2[0] = 0xbb;
            buffer2[0] = 120;
            buffer2[1] = 0x6c;
            buffer2[1] = 120;
            buffer2[1] = 0x9c;
            buffer2[1] = 240;
            buffer2[2] = 0x76;
            buffer2[2] = 0x94;
            buffer2[2] = 0x5f;
            buffer2[2] = 150;
            buffer2[2] = 0x88;
            buffer2[2] = 0xb9;
            buffer2[3] = 0x7f;
            buffer2[3] = 0x8e;
            buffer2[3] = 0x74;
            buffer2[3] = 100;
            buffer2[3] = 0x63;
            buffer2[4] = 0x61;
            buffer2[4] = 0xb3;
            buffer2[4] = 0xa1;
            buffer2[4] = 0xd1;
            buffer2[5] = 0x6c;
            buffer2[5] = 0x98;
            buffer2[5] = 120;
            buffer2[5] = 0x2c;
            buffer2[6] = 0x52;
            buffer2[6] = 0xa6;
            buffer2[6] = 0x4b;
            buffer2[7] = 0xa8;
            buffer2[7] = 0x6f;
            buffer2[7] = 0x94;
            buffer2[7] = 0x66;
            buffer2[7] = 0x63;
            buffer2[7] = 190;
            buffer2[8] = 0x83;
            buffer2[8] = 0x84;
            buffer2[8] = 0x30;
            buffer2[8] = 0x61;
            buffer2[9] = 0x92;
            buffer2[9] = 0x1b;
            buffer2[9] = 120;
            buffer2[9] = 0x62;
            buffer2[9] = 0x92;
            buffer2[9] = 0x10;
            buffer2[10] = 0x48;
            buffer2[10] = 0x7e;
            buffer2[10] = 0x7a;
            buffer2[10] = 0x66;
            buffer2[10] = 230;
            buffer2[11] = 0xad;
            buffer2[11] = 0x9f;
            buffer2[11] = 0x5b;
            buffer2[11] = 0xa7;
            buffer2[12] = 0x59;
            buffer2[12] = 0x6f;
            buffer2[12] = 0x7c;
            buffer2[12] = 0xca;
            buffer2[13] = 0x7a;
            buffer2[13] = 0xa7;
            buffer2[13] = 0x48;
            buffer2[14] = 0x88;
            buffer2[14] = 0x60;
            buffer2[14] = 0x66;
            buffer2[14] = 100;
            buffer2[14] = 0xdd;
            buffer2[15] = 0x86;
            buffer2[15] = 0x66;
            buffer2[15] = 10;
            buffer2[0x10] = 0xba;
            buffer2[0x10] = 50;
            buffer2[0x10] = 0x88;
            buffer2[0x10] = 0x87;
            buffer2[0x10] = 0xb8;
            buffer2[0x11] = 0x54;
            buffer2[0x11] = 30;
            buffer2[0x11] = 200;
            buffer2[0x12] = 210;
            buffer2[0x12] = 0x69;
            buffer2[0x12] = 0x68;
            buffer2[0x12] = 0x5e;
            buffer2[0x12] = 0xa7;
            buffer2[0x13] = 0x8e;
            buffer2[0x13] = 0x7b;
            buffer2[0x13] = 7;
            buffer2[20] = 0x34;
            buffer2[20] = 0x7f;
            buffer2[20] = 0x63;
            buffer2[20] = 0x6f;
            buffer2[0x15] = 0x58;
            buffer2[0x15] = 0x7e;
            buffer2[0x15] = 15;
            buffer2[0x16] = 0xa1;
            buffer2[0x16] = 0x72;
            buffer2[0x16] = 0x73;
            buffer2[0x16] = 0x5f;
            buffer2[0x16] = 0x6c;
            buffer2[0x16] = 0xc1;
            buffer2[0x17] = 0x60;
            buffer2[0x17] = 0x85;
            buffer2[0x17] = 0x9d;
            buffer2[0x17] = 0x6a;
            buffer2[0x17] = 0x93;
            buffer2[0x18] = 0x5c;
            buffer2[0x18] = 0x94;
            buffer2[0x18] = 0x3a;
            buffer2[0x19] = 140;
            buffer2[0x19] = 20;
            buffer2[0x19] = 0x4e;
            buffer2[0x19] = 0x62;
            buffer2[0x19] = 230;
            buffer2[0x1a] = 0xa6;
            buffer2[0x1a] = 130;
            buffer2[0x1a] = 0x58;
            buffer2[0x1a] = 0xae;
            buffer2[0x1a] = 0x65;
            buffer2[0x1b] = 0xa9;
            buffer2[0x1b] = 120;
            buffer2[0x1b] = 0x65;
            buffer2[0x1c] = 0x68;
            buffer2[0x1c] = 120;
            buffer2[0x1c] = 0x91;
            buffer2[0x1d] = 0x5f;
            buffer2[0x1d] = 120;
            buffer2[0x1d] = 0xa7;
            buffer2[0x1d] = 0x47;
            buffer2[30] = 0x97;
            buffer2[30] = 0x6b;
            buffer2[30] = 0x76;
            buffer2[30] = 0x63;
            buffer2[30] = 0x56;
            buffer2[30] = 0xa7;
            buffer2[0x1f] = 0xa2;
            buffer2[0x1f] = 0x43;
            buffer2[0x1f] = 100;
            buffer2[0x1f] = 120;
            buffer2[0x1f] = 0xa1;
            buffer2[0x1f] = 0x45;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x43;
            buffer4[0] = 0x70;
            buffer4[0] = 120;
            buffer4[0] = 0x86;
            buffer4[0] = 0xa4;
            buffer4[0] = 0x88;
            buffer4[1] = 0x59;
            buffer4[1] = 0x59;
            buffer4[1] = 0x83;
            buffer4[1] = 0x7d;
            buffer4[1] = 0x76;
            buffer4[1] = 0xeb;
            buffer4[2] = 0x5f;
            buffer4[2] = 150;
            buffer4[2] = 0x7e;
            buffer4[2] = 0x6c;
            buffer4[2] = 0x9a;
            buffer4[2] = 0xf8;
            buffer4[3] = 0x8e;
            buffer4[3] = 0x74;
            buffer4[3] = 100;
            buffer4[3] = 0x48;
            buffer4[3] = 0x6f;
            buffer4[3] = 0x88;
            buffer4[4] = 0xc2;
            buffer4[4] = 0x5e;
            buffer4[4] = 0x98;
            buffer4[4] = 0x62;
            buffer4[4] = 0xfb;
            buffer4[5] = 0x89;
            buffer4[5] = 120;
            buffer4[5] = 0x3d;
            buffer4[5] = 160;
            buffer4[6] = 0x88;
            buffer4[6] = 0x6f;
            buffer4[6] = 0x88;
            buffer4[6] = 0x16;
            buffer4[6] = 0x2f;
            buffer4[7] = 0x61;
            buffer4[7] = 0x58;
            buffer4[7] = 0x75;
            buffer4[8] = 0x92;
            buffer4[8] = 100;
            buffer4[8] = 0x9c;
            buffer4[8] = 0x79;
            buffer4[8] = 0x7a;
            buffer4[9] = 80;
            buffer4[9] = 0x7b;
            buffer4[9] = 0x70;
            buffer4[9] = 0x7e;
            buffer4[9] = 0x7a;
            buffer4[9] = 0x33;
            buffer4[10] = 0x9d;
            buffer4[10] = 0x73;
            buffer4[10] = 0xa7;
            buffer4[10] = 0xa8;
            buffer4[10] = 0x45;
            buffer4[10] = 0x9f;
            buffer4[11] = 0x88;
            buffer4[11] = 0xa9;
            buffer4[11] = 0x65;
            buffer4[11] = 0x2d;
            buffer4[12] = 0x54;
            buffer4[12] = 0x75;
            buffer4[12] = 0xa9;
            buffer4[12] = 0;
            buffer4[13] = 0x88;
            buffer4[13] = 0x60;
            buffer4[13] = 0x66;
            buffer4[13] = 0xd7;
            buffer4[14] = 0x77;
            buffer4[14] = 150;
            buffer4[14] = 0x31;
            buffer4[15] = 0xbf;
            buffer4[15] = 0x7b;
            buffer4[15] = 100;
            buffer4[15] = 0x88;
            buffer4[15] = 0x87;
            buffer4[15] = 0x83;
            byte[] array = buffer4;
            Array.Reverse(array);
            byte[] publicKeyToken = typeof(Class75).Assembly.GetName().GetPublicKeyToken();
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

    [Attribute7(typeof(Attribute7.Class76<object>[]))]
    internal static string smethod_12(string string_1)
    {
        "{11111-22222-50001-00000}".Trim();
        byte[] bytes = Convert.FromBase64String(string_1);
        return Encoding.Unicode.GetString(bytes, 0, bytes.Length);
    }

    internal static uint smethod_13(IntPtr intptr_3, IntPtr intptr_4, IntPtr intptr_5, [MarshalAs(UnmanagedType.U4)] uint uint_1, IntPtr intptr_6, ref uint uint_2)
    {
        IntPtr ptr = intptr_5;
        if (bool_3)
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
            Struct136 struct2 = (Struct136) obj2;
            IntPtr destination = Marshal.AllocCoTaskMem(struct2.byte_0.Length);
            Marshal.Copy(struct2.byte_0, 0, destination, struct2.byte_0.Length);
            if (struct2.bool_0)
            {
                intptr_6 = destination;
                uint_2 = (uint) struct2.byte_0.Length;
                VirtualProtect_1(intptr_6, struct2.byte_0.Length, 0x40, ref int_0);
                return 0;
            }
            Marshal.WriteIntPtr(ptr, IntPtr.Size * 2, destination);
            Marshal.WriteInt32(ptr, IntPtr.Size * 3, struct2.byte_0.Length);
            uint num2 = 0;
            if ((uint_1 == 0xcea1d7d) && !bool_6)
            {
                bool_6 = true;
                return num2;
            }
        }
        return delegate29_1(intptr_3, intptr_4, intptr_5, uint_1, intptr_6, ref uint_2);
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

    //没有引用，屏蔽
    ///* private scope */ static unsafe void smethod_16()
    //{
    //    BinaryReader reader;
    //    IEnumerator enumerator;
    //    if (bool_5)
    //    {
    //        return;
    //    }
    //    bool_5 = true;
    //    long num9 = 0L;
    //    Marshal.ReadIntPtr(new IntPtr((void*) &num9), 0);
    //    Marshal.ReadInt32(new IntPtr((void*) &num9), 0);
    //    Marshal.ReadInt64(new IntPtr((void*) &num9), 0);
    //    Marshal.WriteIntPtr(new IntPtr((void*) &num9), 0, IntPtr.Zero);
    //    Marshal.WriteInt32(new IntPtr((void*) &num9), 0, 0);
    //    Marshal.WriteInt64(new IntPtr((void*) &num9), 0, 0L);
    //    byte[] source = new byte[1];
    //    Marshal.Copy(source, 0, Marshal.AllocCoTaskMem(8), 1);
    //    smethod_14();
    //    bool flag1 = FindResource(Process.GetCurrentProcess().MainModule.BaseAddress, "__", 10) != IntPtr.Zero;
    //    if ((IntPtr.Size == 4) && (Type.GetType("System.Reflection.ReflectionContext", false) != null))
    //    {
    //        enumerator = Process.GetCurrentProcess().Modules.GetEnumerator();
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
    //            bool_3 = true;
    //        }
    //    }
    //Label_01C3:
    //    reader = new BinaryReader(typeof(Class75).Assembly.GetManifestResourceStream("\x0095b7\x009d\x0098\x0099\x008d\x0087vs\x0086\x00876o\x009fpgc.\x009clbk\x009a\x008b\x008ek\x009f\x00918\x009fcxywv\x009f")) {
    //        BaseStream = { Position = 0L }
    //    };
    //    byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
    //    byte[] buffer12 = new byte[0x20];
    //    buffer12[0] = 140;
    //    buffer12[0] = 0x9d;
    //    buffer12[0] = 0x70;
    //    buffer12[0] = 0x63;
    //    buffer12[0] = 90;
    //    buffer12[0] = 0xa1;
    //    buffer12[1] = 0xd1;
    //    buffer12[1] = 90;
    //    buffer12[1] = 0xb8;
    //    buffer12[2] = 0xde;
    //    buffer12[2] = 0x92;
    //    buffer12[2] = 0x21;
    //    buffer12[3] = 0x9a;
    //    buffer12[3] = 0x91;
    //    buffer12[3] = 0x9a;
    //    buffer12[3] = 140;
    //    buffer12[3] = 0x9f;
    //    buffer12[4] = 0x7a;
    //    buffer12[4] = 0x9c;
    //    buffer12[4] = 0x73;
    //    buffer12[4] = 0x62;
    //    buffer12[4] = 0x7e;
    //    buffer12[4] = 0x75;
    //    buffer12[5] = 0xd4;
    //    buffer12[5] = 0x4c;
    //    buffer12[5] = 90;
    //    buffer12[5] = 0xc2;
    //    buffer12[6] = 0x5b;
    //    buffer12[6] = 0x88;
    //    buffer12[6] = 0x6f;
    //    buffer12[6] = 0x86;
    //    buffer12[6] = 0xa6;
    //    buffer12[6] = 0xc9;
    //    buffer12[7] = 0xa9;
    //    buffer12[7] = 0x6b;
    //    buffer12[7] = 0x7f;
    //    buffer12[7] = 0xba;
    //    buffer12[8] = 0x62;
    //    buffer12[8] = 0x9e;
    //    buffer12[8] = 0xa4;
    //    buffer12[8] = 0xc4;
    //    buffer12[9] = 0x93;
    //    buffer12[9] = 0x86;
    //    buffer12[9] = 0x67;
    //    buffer12[9] = 0x51;
    //    buffer12[9] = 0xdd;
    //    buffer12[10] = 0x58;
    //    buffer12[10] = 0x86;
    //    buffer12[10] = 190;
    //    buffer12[11] = 0x84;
    //    buffer12[11] = 0x70;
    //    buffer12[11] = 0xc3;
    //    buffer12[11] = 0x93;
    //    buffer12[11] = 0x8f;
    //    buffer12[11] = 0x16;
    //    buffer12[12] = 0x83;
    //    buffer12[12] = 0xa4;
    //    buffer12[12] = 0xb6;
    //    buffer12[13] = 0x83;
    //    buffer12[13] = 0x9c;
    //    buffer12[13] = 0x3a;
    //    buffer12[13] = 0x35;
    //    buffer12[13] = 0x83;
    //    buffer12[14] = 0x83;
    //    buffer12[14] = 0xa2;
    //    buffer12[14] = 0x54;
    //    buffer12[14] = 240;
    //    buffer12[15] = 0x75;
    //    buffer12[15] = 0x84;
    //    buffer12[15] = 0x97;
    //    buffer12[15] = 0x71;
    //    buffer12[15] = 0x5c;
    //    buffer12[15] = 0x44;
    //    buffer12[0x10] = 0x2a;
    //    buffer12[0x10] = 0xa4;
    //    buffer12[0x10] = 110;
    //    buffer12[0x10] = 0xa1;
    //    buffer12[0x10] = 9;
    //    buffer12[0x11] = 0x9f;
    //    buffer12[0x11] = 0x53;
    //    buffer12[0x11] = 0xa6;
    //    buffer12[0x11] = 0x37;
    //    buffer12[0x12] = 130;
    //    buffer12[0x12] = 0x8e;
    //    buffer12[0x12] = 0x81;
    //    buffer12[0x12] = 0x55;
    //    buffer12[0x12] = 7;
    //    buffer12[0x13] = 0x7a;
    //    buffer12[0x13] = 0x68;
    //    buffer12[0x13] = 0x7c;
    //    buffer12[20] = 0x59;
    //    buffer12[20] = 0x85;
    //    buffer12[20] = 0x57;
    //    buffer12[20] = 0x7c;
    //    buffer12[20] = 0x3f;
    //    buffer12[0x15] = 0x84;
    //    buffer12[0x15] = 0x7a;
    //    buffer12[0x15] = 0x77;
    //    buffer12[0x15] = 0x5e;
    //    buffer12[0x15] = 0x63;
    //    buffer12[0x15] = 0x71;
    //    buffer12[0x16] = 0x83;
    //    buffer12[0x16] = 0x7c;
    //    buffer12[0x16] = 0x68;
    //    buffer12[0x16] = 0x67;
    //    buffer12[0x16] = 0x5b;
    //    buffer12[0x16] = 0x27;
    //    buffer12[0x17] = 0x76;
    //    buffer12[0x17] = 0x8e;
    //    buffer12[0x17] = 140;
    //    buffer12[0x17] = 0x57;
    //    buffer12[0x18] = 100;
    //    buffer12[0x18] = 0x6a;
    //    buffer12[0x18] = 0x27;
    //    buffer12[0x19] = 0x58;
    //    buffer12[0x19] = 0x16;
    //    buffer12[0x19] = 0x85;
    //    buffer12[0x1a] = 0x9f;
    //    buffer12[0x1a] = 0x91;
    //    buffer12[0x1a] = 150;
    //    buffer12[0x1a] = 70;
    //    buffer12[0x1a] = 0xb1;
    //    buffer12[0x1b] = 0x37;
    //    buffer12[0x1b] = 130;
    //    buffer12[0x1b] = 0xa9;
    //    buffer12[0x1b] = 0x7e;
    //    buffer12[0x1b] = 0x5b;
    //    buffer12[0x1b] = 0xc9;
    //    buffer12[0x1c] = 0xa7;
    //    buffer12[0x1c] = 0x56;
    //    buffer12[0x1c] = 0xcd;
    //    buffer12[0x1d] = 0x68;
    //    buffer12[0x1d] = 0xa5;
    //    buffer12[0x1d] = 0x7a;
    //    buffer12[0x1d] = 0x54;
    //    buffer12[0x1d] = 0x3a;
    //    buffer12[0x1d] = 0x9b;
    //    buffer12[30] = 120;
    //    buffer12[30] = 0x94;
    //    buffer12[30] = 0xb1;
    //    buffer12[0x1f] = 0x9c;
    //    buffer12[0x1f] = 0x58;
    //    buffer12[0x1f] = 0x65;
    //    buffer12[0x1f] = 0xca;
    //    byte[] rgbKey = buffer12;
    //    byte[] buffer14 = new byte[0x10];
    //    buffer14[0] = 0x67;
    //    buffer14[0] = 80;
    //    buffer14[0] = 0x63;
    //    buffer14[0] = 0x79;
    //    buffer14[0] = 0xd1;
    //    buffer14[0] = 0x4a;
    //    buffer14[1] = 0x5b;
    //    buffer14[1] = 0xa6;
    //    buffer14[1] = 0x9f;
    //    buffer14[1] = 0x5f;
    //    buffer14[2] = 0x98;
    //    buffer14[2] = 140;
    //    buffer14[2] = 0x7f;
    //    buffer14[2] = 0x9a;
    //    buffer14[2] = 140;
    //    buffer14[2] = 90;
    //    buffer14[3] = 150;
    //    buffer14[3] = 0x9e;
    //    buffer14[3] = 0x4b;
    //    buffer14[3] = 0x57;
    //    buffer14[3] = 170;
    //    buffer14[4] = 0x8a;
    //    buffer14[4] = 0x68;
    //    buffer14[4] = 0x69;
    //    buffer14[4] = 90;
    //    buffer14[4] = 0xa2;
    //    buffer14[4] = 0xf9;
    //    buffer14[5] = 0x75;
    //    buffer14[5] = 0x80;
    //    buffer14[5] = 0x3b;
    //    buffer14[5] = 0xa3;
    //    buffer14[5] = 0x89;
    //    buffer14[6] = 0x7d;
    //    buffer14[6] = 0x80;
    //    buffer14[6] = 0x55;
    //    buffer14[6] = 0xf2;
    //    buffer14[7] = 0xba;
    //    buffer14[7] = 0x75;
    //    buffer14[7] = 0xc9;
    //    buffer14[7] = 0xaf;
    //    buffer14[8] = 0x84;
    //    buffer14[8] = 0x41;
    //    buffer14[8] = 80;
    //    buffer14[8] = 0xd8;
    //    buffer14[9] = 0x51;
    //    buffer14[9] = 8;
    //    buffer14[9] = 0x86;
    //    buffer14[9] = 0x8e;
    //    buffer14[9] = 0x89;
    //    buffer14[9] = 0xe0;
    //    buffer14[10] = 0x81;
    //    buffer14[10] = 0xa9;
    //    buffer14[10] = 0x68;
    //    buffer14[10] = 0x48;
    //    buffer14[11] = 0x99;
    //    buffer14[11] = 0x79;
    //    buffer14[11] = 0x6c;
    //    buffer14[11] = 0xef;
    //    buffer14[12] = 0x9c;
    //    buffer14[12] = 0x6c;
    //    buffer14[12] = 170;
    //    buffer14[12] = 0x87;
    //    buffer14[13] = 0x99;
    //    buffer14[13] = 120;
    //    buffer14[13] = 220;
    //    buffer14[14] = 0x8d;
    //    buffer14[14] = 0x84;
    //    buffer14[14] = 0x16;
    //    buffer14[14] = 0x93;
    //    buffer14[14] = 0xa5;
    //    buffer14[15] = 0x6d;
    //    buffer14[15] = 0x6c;
    //    buffer14[15] = 0x68;
    //    buffer14[15] = 0xa9;
    //    buffer14[15] = 0xb6;
    //    byte[] array = buffer14;
    //    Array.Reverse(array);
    //    byte[] publicKeyToken = typeof(Class75).Assembly.GetName().GetPublicKeyToken();
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
    //    MemoryStream stream2 = new MemoryStream();
    //    CryptoStream stream3 = new CryptoStream(stream2, transform, CryptoStreamMode.Write);
    //    stream3.Write(buffer, 0, buffer.Length);
    //    stream3.FlushFinalBlock();
    //    byte[] buffer10 = stream2.ToArray();
    //    Array.Clear(array, 0, array.Length);
    //    stream2.Close();
    //    stream3.Close();
    //    reader.Close();
    //    int num16 = buffer10.Length / 8;
    //    //不会改先屏蔽了
    //    //if (((source = buffer10) != null) && (source.Length != 0))
    //    //{
    //    //    numRef = source;
    //    //    goto Label_0D2A;
    //    //}
    //    //fixed (byte* numRef = null)
    //    //{
    //    //    int num2;
    //    //Label_0D2A:
    //    //    num2 = 0;
    //    //    while (num2 < num16)
    //    //    {
    //    //        IntPtr ptr1 = (IntPtr) (numRef + (num2 * 8));
    //    //        ptr1[0] ^= (IntPtr) 0x32b4073bL;
    //    //        num2++;
    //    //    }
    //    //}
    //    reader = new BinaryReader(new MemoryStream(buffer10)) {
    //        BaseStream = { Position = 0L }
    //    };
    //    long num4 = Marshal.GetHINSTANCE(typeof(Class75).Assembly.GetModules()[0]).ToInt64();
    //    int num6 = 0;
    //    int num5 = 0;
    //    if ((typeof(Class75).Assembly.Location == null) || (typeof(Class75).Assembly.Location.Length == 0))
    //    {
    //        num5 = 0x1c00;
    //    }
    //    int num15 = reader.ReadInt32();
    //    if (reader.ReadInt32() == 1)
    //    {
    //        IntPtr ptr3 = IntPtr.Zero;
    //        Assembly assembly = typeof(Class75).Assembly;
    //        ptr3 = OpenProcess(0x38, 1, (uint) Process.GetCurrentProcess().Id);
    //        if (IntPtr.Size == 4)
    //        {
    //            int_4 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt32();
    //        }
    //        long_1 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt64();
    //        IntPtr ptr8 = IntPtr.Zero;
    //        for (int j = 0; j < num15; j++)
    //        {
    //            IntPtr ptr4 = new IntPtr((long_1 + reader.ReadInt32()) - num5);
    //            VirtualProtect_1(ptr4, 4, 4, ref num6);
    //            if (IntPtr.Size == 4)
    //            {
    //                WriteProcessMemory(ptr3, ptr4, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr8);
    //            }
    //            else
    //            {
    //                WriteProcessMemory(ptr3, ptr4, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr8);
    //            }
    //            VirtualProtect_1(ptr4, 4, num6, ref num6);
    //        }
    //        while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
    //        {
    //            int num18 = reader.ReadInt32();
    //            IntPtr ptr6 = new IntPtr(long_1 + num18);
    //            int num13 = reader.ReadInt32();
    //            VirtualProtect_1(ptr6, num13 * 4, 4, ref num6);
    //            for (int k = 0; k < num13; k++)
    //            {
    //                Marshal.WriteInt32(new IntPtr(ptr6.ToInt64() + (k * 4)), reader.ReadInt32());
    //            }
    //            VirtualProtect_1(ptr6, num13 * 4, num6, ref num6);
    //        }
    //        CloseHandle(ptr3);
    //        return;
    //    }
    //    for (int i = 0; i < num15; i++)
    //    {
    //        IntPtr ptr2 = new IntPtr((num4 + reader.ReadInt32()) - num5);
    //        VirtualProtect_1(ptr2, 4, 4, ref num6);
    //        Marshal.WriteInt32(ptr2, reader.ReadInt32());
    //        VirtualProtect_1(ptr2, 4, num6, ref num6);
    //    }
    //    hashtable_0 = new Hashtable(reader.ReadInt32() + 1);
    //    Struct136 struct3 = new Struct136 {
    //        byte_0 = new byte[] { 0x2a },
    //        bool_0 = false
    //    };
    //    hashtable_0.Add(0L, struct3);
    //    bool flag = false;
    //    while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
    //    {
    //        int num12 = reader.ReadInt32() - num5;
    //        int num20 = reader.ReadInt32();
    //        flag = false;
    //        if (num20 >= 0x70000000)
    //        {
    //            flag = true;
    //        }
    //        int count = reader.ReadInt32();
    //        byte[] buffer5 = reader.ReadBytes(count);
    //        Struct136 struct2 = new Struct136 {
    //            byte_0 = buffer5,
    //            bool_0 = flag
    //        };
    //        hashtable_0.Add(num4 + num12, struct2);
    //    }
    //    long_0 = Marshal.GetHINSTANCE(typeof(Class75).Assembly.GetModules()[0]).ToInt64();
    //    if (IntPtr.Size == 4)
    //    {
    //        int_1 = Convert.ToInt32(long_0);
    //    }
    //    byte[] bytes = new byte[] { 0x6d, 0x73, 0x63, 0x6f, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
    //    string str = Encoding.UTF8.GetString(bytes);
    //    IntPtr ptr5 = LoadLibrary(str);
    //    if (ptr5 == IntPtr.Zero)
    //    {
    //        bytes = new byte[] { 0x63, 0x6c, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
    //        str = Encoding.UTF8.GetString(bytes);
    //        ptr5 = LoadLibrary(str);
    //    }
    //    byte[] buffer17 = new byte[] { 0x67, 0x65, 0x74, 0x4a, 0x69, 0x74 };
    //    string str2 = Encoding.UTF8.GetString(buffer17);
    //    Delegate30 delegate3 = (Delegate30) smethod_15(GetProcAddress(ptr5, str2), typeof(Delegate30));
    //    IntPtr ptr = delegate3();
    //    long num3 = 0L;
    //    if (IntPtr.Size == 4)
    //    {
    //        num3 = Marshal.ReadInt32(ptr);
    //    }
    //    else
    //    {
    //        num3 = Marshal.ReadInt64(ptr);
    //    }
    //    Marshal.ReadIntPtr(ptr, 0);
    //    delegate29_0 = new Delegate29(Class75.smethod_13);
    //    IntPtr zero = IntPtr.Zero;
    //    zero = Marshal.GetFunctionPointerForDelegate(delegate29_0);
    //    long num14 = 0L;
    //    if (IntPtr.Size == 4)
    //    {
    //        num14 = Marshal.ReadInt32(new IntPtr(num3));
    //    }
    //    else
    //    {
    //        num14 = Marshal.ReadInt64(new IntPtr(num3));
    //    }
    //    Process currentProcess = Process.GetCurrentProcess();
    //    try
    //    {
    //        foreach (ProcessModule module in currentProcess.Modules)
    //        {
    //            if (((module.ModuleName == str) && ((num14 < module.BaseAddress.ToInt64()) || (num14 > (module.BaseAddress.ToInt64() + module.ModuleMemorySize)))) && (typeof(Class75).Assembly.EntryPoint != null))
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
    //        enumerator = currentProcess.Modules.GetEnumerator();
    //        while (enumerator.MoveNext())
    //        {
    //            ProcessModule module2 = (ProcessModule)enumerator.Current;
    //            if (module2.BaseAddress.ToInt64() == long_0)
    //            {
    //                goto Label_13B9;
    //            }
    //        }
    //        goto Label_13D8;
    //        Label_13B9:
    //        num5 = 0;
    //    }
    //    catch
    //    {
    //    }
    //Label_13D8:
    //    delegate29_1 = null;
    //    try
    //    {
    //        delegate29_1 = (Delegate29) smethod_15(new IntPtr(num14), typeof(Delegate29));
    //    }
    //    catch
    //    {
    //        try
    //        {
    //            Delegate delegate2 = smethod_15(new IntPtr(num14), typeof(Delegate29));
    //            delegate29_1 = (Delegate29) Delegate.CreateDelegate(typeof(Delegate29), delegate2.Method);
    //        }
    //        catch
    //        {
    //        }
    //    }
    //    int num17 = 0;
    //    if (((typeof(Class75).Assembly.EntryPoint == null) || (typeof(Class75).Assembly.EntryPoint.GetParameters().Length != 2)) || ((typeof(Class75).Assembly.Location == null) || (typeof(Class75).Assembly.Location.Length <= 0)))
    //    {
    //        try
    //        { 
    //            ref byte[] numRef2;
    //            object obj2 = typeof(Class75).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(typeof(Class75).Assembly.ManifestModule.ModuleHandle);
    //            if (obj2 is IntPtr)
    //            {
    //                intptr_2 = (IntPtr)obj2;
    //            }
    //            if (obj2.GetType().ToString() == "System.Reflection.RuntimeModule")
    //            {
    //                intptr_2 = (IntPtr)obj2.GetType().GetField("m_pData", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(obj2);
    //            }
    //            MemoryStream stream = new MemoryStream();
    //            stream.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
    //            if (IntPtr.Size == 4)
    //            {
    //                stream.Write(BitConverter.GetBytes(intptr_2.ToInt32()), 0, 4);
    //            }
    //            else
    //            {
    //                stream.Write(BitConverter.GetBytes(intptr_2.ToInt64()), 0, 8);
    //            }
    //            stream.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
    //            stream.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
    //            stream.Position = 0L;
    //            byte[] buffer4 = stream.ToArray();
    //            stream.Close();
    //            uint nativeSizeOfCode = 0;
    //            try
    //            {
    //                if (((source = buffer4) != null) && (source.Length != 0))
    //                {
    //                    numRef2 = source;
    //                }
    //                else
    //                {
    //                    numRef2 = null;
    //                }
    //                GCHandle handle = GCHandle.Alloc(numRef2);
    //                IntPtr ptrNumRef2 = GCHandle.ToIntPtr(handle);
    //                delegate29_0(ptrNumRef2, ptrNumRef2, ptrNumRef2, 0xcea1d7d, ptrNumRef2, ref nativeSizeOfCode);
    //            }
    //            finally
    //            {
    //                numRef2 = null;
    //            }
    //        }
    //        catch
    //        {
    //        }
    //        RuntimeHelpers.PrepareDelegate(delegate29_1);
    //        RuntimeHelpers.PrepareMethod(delegate29_1.Method.MethodHandle);
    //        RuntimeHelpers.PrepareDelegate(delegate29_0);
    //        RuntimeHelpers.PrepareMethod(delegate29_0.Method.MethodHandle);
    //        byte[] buffer2 = null;
    //        if (IntPtr.Size != 4)
    //        {
    //            buffer2 = new byte[] { 
    //                0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0x49, 0x39, 0x40, 8, 0x74, 12, 
    //                0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0xff, 0xe0, 0x48, 0xb8, 0, 0, 
    //                0, 0, 0, 0, 0, 0, 0xff, 0xe0
    //             };
    //        }
    //        else
    //        {
    //            buffer2 = new byte[] { 
    //                0x55, 0x8b, 0xec, 0x8b, 0x45, 0x10, 0x81, 120, 4, 0x7d, 0x1d, 0xea, 12, 0x74, 7, 0xb8, 
    //                0xb6, 0xb1, 0x4a, 6, 0xeb, 5, 0xb8, 0xb6, 0x92, 0x40, 12, 0x5d, 0xff, 0xe0
    //             };
    //        }
    //        IntPtr destination = VirtualAlloc(IntPtr.Zero, (uint) buffer2.Length, 0x1000, 0x40);
    //        byte[] buffer6 = buffer2;
    //        byte[] buffer7 = null;
    //        byte[] buffer8 = null;
    //        byte[] buffer9 = null;
    //        if (IntPtr.Size == 4)
    //        {
    //            buffer9 = BitConverter.GetBytes(intptr_2.ToInt32());
    //            buffer7 = BitConverter.GetBytes(zero.ToInt32());
    //            buffer8 = BitConverter.GetBytes(Convert.ToInt32(num14));
    //        }
    //        else
    //        {
    //            buffer9 = BitConverter.GetBytes(intptr_2.ToInt64());
    //            buffer7 = BitConverter.GetBytes(zero.ToInt64());
    //            buffer8 = BitConverter.GetBytes(num14);
    //        }
    //        if (IntPtr.Size == 4)
    //        {
    //            buffer6[9] = buffer9[0];
    //            buffer6[10] = buffer9[1];
    //            buffer6[11] = buffer9[2];
    //            buffer6[12] = buffer9[3];
    //            buffer6[0x10] = buffer8[0];
    //            buffer6[0x11] = buffer8[1];
    //            buffer6[0x12] = buffer8[2];
    //            buffer6[0x13] = buffer8[3];
    //            buffer6[0x17] = buffer7[0];
    //            buffer6[0x18] = buffer7[1];
    //            buffer6[0x19] = buffer7[2];
    //            buffer6[0x1a] = buffer7[3];
    //        }
    //        else
    //        {
    //            buffer6[2] = buffer9[0];
    //            buffer6[3] = buffer9[1];
    //            buffer6[4] = buffer9[2];
    //            buffer6[5] = buffer9[3];
    //            buffer6[6] = buffer9[4];
    //            buffer6[7] = buffer9[5];
    //            buffer6[8] = buffer9[6];
    //            buffer6[9] = buffer9[7];
    //            buffer6[0x12] = buffer8[0];
    //            buffer6[0x13] = buffer8[1];
    //            buffer6[20] = buffer8[2];
    //            buffer6[0x15] = buffer8[3];
    //            buffer6[0x16] = buffer8[4];
    //            buffer6[0x17] = buffer8[5];
    //            buffer6[0x18] = buffer8[6];
    //            buffer6[0x19] = buffer8[7];
    //            buffer6[30] = buffer7[0];
    //            buffer6[0x1f] = buffer7[1];
    //            buffer6[0x20] = buffer7[2];
    //            buffer6[0x21] = buffer7[3];
    //            buffer6[0x22] = buffer7[4];
    //            buffer6[0x23] = buffer7[5];
    //            buffer6[0x24] = buffer7[6];
    //            buffer6[0x25] = buffer7[7];
    //        }
    //        Marshal.Copy(buffer6, 0, destination, buffer6.Length);
    //        bool_1 = false;
    //        VirtualProtect_1(new IntPtr(num3), IntPtr.Size, 0x40, ref num17);
    //        Marshal.WriteIntPtr(new IntPtr(num3), destination);
    //        VirtualProtect_1(new IntPtr(num3), IntPtr.Size, num17, ref num17);
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

    [Attribute7(typeof(Attribute7.Class76<object>[]))]
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

    [Attribute7(typeof(Attribute7.Class76<object>[]))]
    private static byte[] smethod_19(byte[] byte_4)
    {
        MemoryStream stream = new MemoryStream();
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Key = new byte[] { 
            0x58, 0x83, 0x7a, 0xa5, 70, 0x21, 0xbf, 12, 190, 0xa5, 170, 0xef, 0x1b, 0xb1, 180, 0x53, 
            0xc5, 4, 0x7a, 0x8e, 170, 0xb9, 0xc7, 0xdd, 0xa8, 0x42, 3, 0x9b, 0x59, 0xc1, 190, 0x23
         };
        algorithm.IV = new byte[] { 0xae, 0x20, 0x6f, 80, 2, 0xed, 0x2b, 0x95, 0x58, 11, 0x6f, 0x53, 0xe8, 0xa4, 0xe1, 0x51 };
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
        if (!bool_2)
        {
            smethod_8();
            bool_2 = true;
        }
        return bool_4;
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
            bool_4 = (bool) typeof(RijndaelManaged).Assembly.GetType("System.Security.Cryptography.CryptoConfig", false).GetMethod("get_AllowOnlyFipsAlgorithms", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[0]);
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

    internal class Attribute7 : Attribute
    {
        [Attribute7(typeof(Class76<object>[]))]
        public Attribute7(object object_0)
        {
            Class78.smethod_0();
        }

        internal class Class76<T>
        {
            public Class76()
            {
                Class78.smethod_0();
            }
        }
    }

    internal class Class77
    {
        public Class77()
        {
            Class78.smethod_0();
        }

        [Attribute7(typeof(Class75.Attribute7.Class76<object>[]))]
        internal static string smethod_0(string string_0, string string_1)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string_0);
            byte[] buffer3 = new byte[] { 
                0x52, 0x66, 0x68, 110, 0x20, 0x4d, 0x18, 0x22, 0x76, 0xb5, 0x33, 0x11, 0x12, 0x33, 12, 0x6d, 
                10, 0x20, 0x4d, 0x18, 0x22, 0x9e, 0xa1, 0x29, 0x61, 0x1c, 0x76, 0xb5, 5, 0x19, 1, 0x58
             };
            byte[] buffer4 = Class75.smethod_9(Encoding.Unicode.GetBytes(string_1));
            MemoryStream stream = new MemoryStream();
            SymmetricAlgorithm algorithm = Class75.smethod_7();
            algorithm.Key = buffer3;
            algorithm.IV = buffer4;
            CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate uint Delegate29(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate IntPtr Delegate30();

    [Flags]
    private enum Enum7
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct136
    {
        internal bool bool_0;
        internal byte[] byte_0;
    }
}

