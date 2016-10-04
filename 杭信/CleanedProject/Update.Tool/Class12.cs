using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal class Class12
{
    private static bool bool_0 = false;
    private static bool bool_1 = false;
    [Attribute1(typeof(Attribute1.OKFMOHKLILCGIGFJIHLMAFEPGMLOAJMAPPJC<object>[]))]
    private static bool bool_2 = false;
    private static bool bool_3 = false;
    private static bool bool_4 = false;
    private static bool bool_5 = false;
    private static bool bool_6 = false;
    private static byte[] byte_0 = new byte[0];
    private static byte[] byte_1 = new byte[0];
    private static byte[] byte_2 = new byte[0];
    private static byte[] byte_3 = new byte[0];
    internal static Delegate6 delegate6_0 = null;
    internal static Delegate6 delegate6_1 = null;
    internal static Hashtable hashtable_0 = new Hashtable();
    private static int int_0 = 0;
    private static int int_1 = 0;
    private static int[] int_2 = new int[0];
    private static int int_3 = 0;
    private static int int_4 = 1;
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

    [Attribute1(typeof(Attribute1.OKFMOHKLILCGIGFJIHLMAFEPGMLOAJMAPPJC<object>[]))]
    /* private scope */ static bool smethod_10(int int_5)
    {
        if (byte_3.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class12).Assembly.GetManifestResourceStream("O0DY3LF9wqERovFDk3.9Yv7X2GL69hahsolrg")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x7c;
            buffer2[0] = 80;
            buffer2[0] = 0x68;
            buffer2[0] = 0x68;
            buffer2[1] = 170;
            buffer2[1] = 90;
            buffer2[1] = 0x72;
            buffer2[2] = 0xcc;
            buffer2[2] = 120;
            buffer2[2] = 0x9e;
            buffer2[2] = 0x91;
            buffer2[2] = 0x7d;
            buffer2[2] = 70;
            buffer2[3] = 0x58;
            buffer2[3] = 0x93;
            buffer2[3] = 200;
            buffer2[3] = 0x7d;
            buffer2[3] = 0x91;
            buffer2[3] = 5;
            buffer2[4] = 0x54;
            buffer2[4] = 0xa4;
            buffer2[4] = 0xa2;
            buffer2[4] = 0x1d;
            buffer2[5] = 0x9c;
            buffer2[5] = 0x70;
            buffer2[5] = 0x8a;
            buffer2[5] = 0xa6;
            buffer2[6] = 0x86;
            buffer2[6] = 0x47;
            buffer2[6] = 0xa2;
            buffer2[6] = 0x95;
            buffer2[6] = 90;
            buffer2[7] = 0xb0;
            buffer2[7] = 0x60;
            buffer2[7] = 0xa3;
            buffer2[7] = 0x56;
            buffer2[7] = 0x89;
            buffer2[7] = 0x9e;
            buffer2[8] = 0x58;
            buffer2[8] = 0x8a;
            buffer2[8] = 0x37;
            buffer2[8] = 140;
            buffer2[8] = 0x41;
            buffer2[9] = 0x2e;
            buffer2[9] = 0x6f;
            buffer2[9] = 0x7b;
            buffer2[9] = 0x74;
            buffer2[9] = 10;
            buffer2[9] = 7;
            buffer2[10] = 0xad;
            buffer2[10] = 0x92;
            buffer2[10] = 120;
            buffer2[10] = 0x1b;
            buffer2[11] = 0x7c;
            buffer2[11] = 0xb5;
            buffer2[11] = 0x6f;
            buffer2[11] = 0x40;
            buffer2[11] = 0x88;
            buffer2[12] = 0xb5;
            buffer2[12] = 0x76;
            buffer2[12] = 0xd6;
            buffer2[12] = 0xee;
            buffer2[13] = 0x72;
            buffer2[13] = 0x94;
            buffer2[13] = 0x61;
            buffer2[13] = 0x57;
            buffer2[14] = 0x6a;
            buffer2[14] = 0x3d;
            buffer2[14] = 0x5d;
            buffer2[14] = 0xa3;
            buffer2[14] = 0xdf;
            buffer2[14] = 0x8e;
            buffer2[15] = 0x63;
            buffer2[15] = 0x3b;
            buffer2[15] = 0xe3;
            buffer2[0x10] = 0xcf;
            buffer2[0x10] = 0x48;
            buffer2[0x10] = 0x48;
            buffer2[0x10] = 0x68;
            buffer2[0x10] = 60;
            buffer2[0x11] = 0x77;
            buffer2[0x11] = 0x8e;
            buffer2[0x11] = 0xed;
            buffer2[0x12] = 0xd3;
            buffer2[0x12] = 0x5b;
            buffer2[0x12] = 170;
            buffer2[0x13] = 0x7f;
            buffer2[0x13] = 0x86;
            buffer2[0x13] = 0x58;
            buffer2[20] = 0x6b;
            buffer2[20] = 0x75;
            buffer2[20] = 11;
            buffer2[0x15] = 0x9f;
            buffer2[0x15] = 0x90;
            buffer2[0x15] = 0x2e;
            buffer2[0x15] = 0x55;
            buffer2[0x15] = 0x84;
            buffer2[0x15] = 0x7c;
            buffer2[0x16] = 0x3e;
            buffer2[0x16] = 0x6f;
            buffer2[0x16] = 0x70;
            buffer2[0x16] = 110;
            buffer2[0x16] = 130;
            buffer2[0x16] = 0xd7;
            buffer2[0x17] = 0x55;
            buffer2[0x17] = 0xb5;
            buffer2[0x17] = 0x95;
            buffer2[0x17] = 0x2f;
            buffer2[0x18] = 130;
            buffer2[0x18] = 0x88;
            buffer2[0x18] = 0xb1;
            buffer2[0x18] = 0x24;
            buffer2[0x18] = 0x6a;
            buffer2[0x19] = 0x56;
            buffer2[0x19] = 0x9e;
            buffer2[0x19] = 0x44;
            buffer2[0x1a] = 0x2a;
            buffer2[0x1a] = 160;
            buffer2[0x1a] = 0x7a;
            buffer2[0x1a] = 0x54;
            buffer2[0x1a] = 0x5b;
            buffer2[0x1b] = 0x69;
            buffer2[0x1b] = 0x7a;
            buffer2[0x1b] = 0x4c;
            buffer2[0x1b] = 0x19;
            buffer2[0x1b] = 0x7a;
            buffer2[0x1b] = 0xbd;
            buffer2[0x1c] = 0xeb;
            buffer2[0x1c] = 130;
            buffer2[0x1c] = 0x66;
            buffer2[0x1c] = 0x92;
            buffer2[0x1c] = 0x2c;
            buffer2[0x1c] = 0x51;
            buffer2[0x1d] = 0x8e;
            buffer2[0x1d] = 0x5f;
            buffer2[0x1d] = 0xa2;
            buffer2[0x1d] = 0x62;
            buffer2[0x1d] = 0x66;
            buffer2[0x1d] = 0x8e;
            buffer2[30] = 0x86;
            buffer2[30] = 0x40;
            buffer2[30] = 0x7a;
            buffer2[30] = 0x9a;
            buffer2[30] = 0x9d;
            buffer2[0x1f] = 0x1b;
            buffer2[0x1f] = 210;
            buffer2[0x1f] = 0xf2;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x58;
            buffer4[0] = 0xa7;
            buffer4[0] = 0x70;
            buffer4[0] = 0xbc;
            buffer4[0] = 0x73;
            buffer4[0] = 0xb5;
            buffer4[1] = 0x86;
            buffer4[1] = 0x71;
            buffer4[1] = 60;
            buffer4[2] = 0xbc;
            buffer4[2] = 0x8d;
            buffer4[2] = 0x7e;
            buffer4[2] = 0xb8;
            buffer4[2] = 0x7c;
            buffer4[2] = 180;
            buffer4[3] = 0x5b;
            buffer4[3] = 0x6d;
            buffer4[3] = 0xa6;
            buffer4[4] = 0x6a;
            buffer4[4] = 0xa8;
            buffer4[4] = 0x93;
            buffer4[5] = 0x2c;
            buffer4[5] = 0x22;
            buffer4[5] = 0xa9;
            buffer4[5] = 130;
            buffer4[5] = 10;
            buffer4[6] = 0xb3;
            buffer4[6] = 0x68;
            buffer4[6] = 0x55;
            buffer4[6] = 0x5f;
            buffer4[7] = 0x81;
            buffer4[7] = 0x92;
            buffer4[7] = 0x7e;
            buffer4[7] = 0x65;
            buffer4[8] = 0x2b;
            buffer4[8] = 0x9b;
            buffer4[8] = 0xcf;
            buffer4[9] = 0x76;
            buffer4[9] = 0x77;
            buffer4[9] = 100;
            buffer4[9] = 0xa8;
            buffer4[9] = 0x2a;
            buffer4[10] = 0x80;
            buffer4[10] = 0xe9;
            buffer4[10] = 0x7b;
            buffer4[10] = 0x5e;
            buffer4[10] = 0x7e;
            buffer4[11] = 80;
            buffer4[11] = 0x98;
            buffer4[11] = 0x89;
            buffer4[11] = 150;
            buffer4[11] = 110;
            buffer4[11] = 70;
            buffer4[12] = 0x59;
            buffer4[12] = 110;
            buffer4[12] = 200;
            buffer4[13] = 0x61;
            buffer4[13] = 0x56;
            buffer4[13] = 0x8b;
            buffer4[13] = 0x98;
            buffer4[13] = 0x66;
            buffer4[13] = 0x36;
            buffer4[14] = 0xac;
            buffer4[14] = 0x88;
            buffer4[14] = 0x7d;
            buffer4[14] = 0x60;
            buffer4[14] = 0x58;
            buffer4[14] = 0x71;
            buffer4[15] = 160;
            buffer4[15] = 0xa4;
            buffer4[15] = 0x8a;
            byte[] rgbIV = buffer4;
            byte[] publicKeyToken = typeof(Class12).Assembly.GetName().GetPublicKeyToken();
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
            byte_3 = stream.ToArray();
            stream.Close();
            stream2.Close();
            reader.Close();
        }
        if (byte_2.Length == 0)
        {
            byte_2 = smethod_18(smethod_17(typeof(Class12).Assembly).ToString());
        }
        int index = 0;
        try
        {
            index = BitConverter.ToInt32(new byte[] { byte_3[int_5], byte_3[int_5 + 1], byte_3[int_5 + 2], byte_3[int_5 + 3] }, 0);
        }
        catch
        {
        }
        try
        {
            if (byte_2[index] == 0x80)
            {
                return true;
            }
        }
        catch
        {
        }
        return false;
    }

    [Attribute1(typeof(Attribute1.OKFMOHKLILCGIGFJIHLMAFEPGMLOAJMAPPJC<object>[]))]
    /* private scope */ static string smethod_11(int int_5)
    {
        if (byte_0.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class12).Assembly.GetManifestResourceStream("LK4wO9BhMwkcmTe4AT.klMq4kCLVHIG4tOxVV")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x5c;
            buffer2[0] = 0x74;
            buffer2[0] = 0xa4;
            buffer2[0] = 0x9f;
            buffer2[1] = 0xe4;
            buffer2[1] = 0x9e;
            buffer2[1] = 0xdf;
            buffer2[2] = 0x71;
            buffer2[2] = 0x73;
            buffer2[2] = 0x98;
            buffer2[2] = 0x7a;
            buffer2[3] = 0xb2;
            buffer2[3] = 0x84;
            buffer2[3] = 120;
            buffer2[3] = 0x5c;
            buffer2[3] = 0xae;
            buffer2[4] = 0x6b;
            buffer2[4] = 110;
            buffer2[4] = 210;
            buffer2[5] = 100;
            buffer2[5] = 130;
            buffer2[5] = 170;
            buffer2[5] = 0xb0;
            buffer2[5] = 0x40;
            buffer2[5] = 0x5d;
            buffer2[6] = 0x67;
            buffer2[6] = 0x8a;
            buffer2[6] = 0x43;
            buffer2[6] = 0x18;
            buffer2[7] = 120;
            buffer2[7] = 0x2d;
            buffer2[7] = 0xb3;
            buffer2[8] = 40;
            buffer2[8] = 0x8e;
            buffer2[8] = 0x33;
            buffer2[8] = 0x9b;
            buffer2[8] = 0xd5;
            buffer2[9] = 0x68;
            buffer2[9] = 0x2f;
            buffer2[9] = 0x8a;
            buffer2[9] = 110;
            buffer2[9] = 0x1f;
            buffer2[10] = 0xb0;
            buffer2[10] = 0xb1;
            buffer2[10] = 0x17;
            buffer2[10] = 0x59;
            buffer2[10] = 0x9f;
            buffer2[11] = 0x9f;
            buffer2[11] = 0x5c;
            buffer2[11] = 0x36;
            buffer2[12] = 0x93;
            buffer2[12] = 0xa3;
            buffer2[12] = 0xad;
            buffer2[13] = 0x9d;
            buffer2[13] = 0xa1;
            buffer2[13] = 0x84;
            buffer2[13] = 0xa7;
            buffer2[13] = 0xfd;
            buffer2[14] = 0x8f;
            buffer2[14] = 0x90;
            buffer2[14] = 0xbb;
            buffer2[14] = 0x99;
            buffer2[14] = 0xd4;
            buffer2[15] = 0x48;
            buffer2[15] = 0xb2;
            buffer2[15] = 0x7a;
            buffer2[15] = 40;
            buffer2[0x10] = 0x9a;
            buffer2[0x10] = 0x8b;
            buffer2[0x10] = 0x74;
            buffer2[0x10] = 0x99;
            buffer2[0x10] = 0xc7;
            buffer2[0x10] = 0xe5;
            buffer2[0x11] = 0x93;
            buffer2[0x11] = 0xaf;
            buffer2[0x11] = 0xa7;
            buffer2[0x11] = 0x33;
            buffer2[0x12] = 0x5d;
            buffer2[0x12] = 0xca;
            buffer2[0x12] = 0x84;
            buffer2[0x12] = 0x80;
            buffer2[0x12] = 0xb9;
            buffer2[0x13] = 0x9c;
            buffer2[0x13] = 0x80;
            buffer2[0x13] = 15;
            buffer2[20] = 0x7f;
            buffer2[20] = 0xc9;
            buffer2[20] = 0x47;
            buffer2[20] = 150;
            buffer2[20] = 0xa2;
            buffer2[20] = 0x99;
            buffer2[0x15] = 0x90;
            buffer2[0x15] = 0x4f;
            buffer2[0x15] = 0xa8;
            buffer2[0x15] = 0x77;
            buffer2[0x15] = 0x30;
            buffer2[0x15] = 0x85;
            buffer2[0x16] = 0x5f;
            buffer2[0x16] = 0x63;
            buffer2[0x16] = 0x54;
            buffer2[0x16] = 0x4d;
            buffer2[0x16] = 0xc4;
            buffer2[0x17] = 0x8e;
            buffer2[0x17] = 0x7b;
            buffer2[0x17] = 0x92;
            buffer2[0x18] = 0xc3;
            buffer2[0x18] = 0x7e;
            buffer2[0x18] = 0xa6;
            buffer2[0x18] = 0xef;
            buffer2[0x18] = 0x34;
            buffer2[0x19] = 100;
            buffer2[0x19] = 160;
            buffer2[0x19] = 0x65;
            buffer2[0x19] = 0x7d;
            buffer2[0x1a] = 0x7a;
            buffer2[0x1a] = 0x6d;
            buffer2[0x1a] = 0xe7;
            buffer2[0x1a] = 0x90;
            buffer2[0x1a] = 0x73;
            buffer2[0x1b] = 0x58;
            buffer2[0x1b] = 130;
            buffer2[0x1b] = 0x99;
            buffer2[0x1c] = 0xcc;
            buffer2[0x1c] = 0x6c;
            buffer2[0x1c] = 0x5e;
            buffer2[0x1c] = 80;
            buffer2[0x1c] = 100;
            buffer2[0x1d] = 0xa6;
            buffer2[0x1d] = 0x67;
            buffer2[0x1d] = 0x9d;
            buffer2[0x1d] = 0x55;
            buffer2[0x1d] = 40;
            buffer2[30] = 0x6b;
            buffer2[30] = 120;
            buffer2[30] = 0x55;
            buffer2[30] = 0x68;
            buffer2[30] = 0x5b;
            buffer2[0x1f] = 0x7a;
            buffer2[0x1f] = 0x27;
            buffer2[0x1f] = 0x8a;
            buffer2[0x1f] = 0x62;
            buffer2[0x1f] = 0x9f;
            buffer2[0x1f] = 30;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x5c;
            buffer4[0] = 0x74;
            buffer4[0] = 0x9c;
            buffer4[0] = 0x97;
            buffer4[1] = 0xc0;
            buffer4[1] = 0xa4;
            buffer4[1] = 0x71;
            buffer4[1] = 0x73;
            buffer4[1] = 0x98;
            buffer4[1] = 0xc2;
            buffer4[2] = 0x92;
            buffer4[2] = 0x69;
            buffer4[2] = 0xac;
            buffer4[2] = 0x5b;
            buffer4[2] = 0xba;
            buffer4[2] = 40;
            buffer4[3] = 0x3b;
            buffer4[3] = 0xa1;
            buffer4[3] = 130;
            buffer4[4] = 0x8a;
            buffer4[4] = 0x99;
            buffer4[4] = 0x68;
            buffer4[4] = 0x67;
            buffer4[4] = 0x19;
            buffer4[5] = 0x9d;
            buffer4[5] = 0xae;
            buffer4[5] = 0x48;
            buffer4[5] = 0x37;
            buffer4[6] = 0x92;
            buffer4[6] = 0x9c;
            buffer4[6] = 0x6a;
            buffer4[6] = 0xa4;
            buffer4[7] = 130;
            buffer4[7] = 0x9c;
            buffer4[7] = 0x68;
            buffer4[7] = 0x2d;
            buffer4[8] = 0x1a;
            buffer4[8] = 0x6b;
            buffer4[8] = 0xc1;
            buffer4[9] = 0xda;
            buffer4[9] = 0xae;
            buffer4[9] = 0xa4;
            buffer4[9] = 0x1d;
            buffer4[9] = 20;
            buffer4[10] = 0x9b;
            buffer4[10] = 0x38;
            buffer4[10] = 0xd8;
            buffer4[10] = 0x71;
            buffer4[10] = 0x1f;
            buffer4[11] = 0x39;
            buffer4[11] = 160;
            buffer4[11] = 0xc5;
            buffer4[11] = 0xcb;
            buffer4[12] = 0x9f;
            buffer4[12] = 0x74;
            buffer4[12] = 0xa8;
            buffer4[12] = 0x9e;
            buffer4[12] = 0x31;
            buffer4[13] = 0x92;
            buffer4[13] = 0x6b;
            buffer4[13] = 0x3f;
            buffer4[14] = 0x71;
            buffer4[14] = 0x9d;
            buffer4[14] = 0xa4;
            buffer4[14] = 90;
            buffer4[14] = 0x9a;
            buffer4[14] = 230;
            buffer4[15] = 0x42;
            buffer4[15] = 0xdb;
            buffer4[15] = 0x36;
            byte[] array = buffer4;
            Array.Reverse(array);
            byte[] publicKeyToken = typeof(Class12).Assembly.GetName().GetPublicKeyToken();
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
            byte_0 = stream.ToArray();
            stream.Close();
            stream2.Close();
            reader.Close();
        }
        int count = BitConverter.ToInt32(byte_0, int_5);
        try
        {
            return Encoding.Unicode.GetString(byte_0, int_5 + 4, count);
        }
        catch
        {
        }
        return "";
    }

    [Attribute1(typeof(Attribute1.OKFMOHKLILCGIGFJIHLMAFEPGMLOAJMAPPJC<object>[]))]
    internal static string smethod_12(string string_1)
    {
        "{11111-22222-50001-00000}".Trim();
        byte[] bytes = Convert.FromBase64String(string_1);
        return Encoding.Unicode.GetString(bytes, 0, bytes.Length);
    }

    internal static uint smethod_13(IntPtr intptr_3, IntPtr intptr_4, IntPtr intptr_5, [MarshalAs(UnmanagedType.U4)] uint uint_1, IntPtr intptr_6, ref uint uint_2)
    {
        IntPtr ptr = intptr_5;
        if (bool_5)
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
            Struct15 struct2 = (Struct15) obj2;
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
            if ((uint_1 == 0xcea1d7d) && !bool_2)
            {
                bool_2 = true;
                return num2;
            }
        }
        return delegate6_0(intptr_3, intptr_4, intptr_5, uint_1, intptr_6, ref uint_2);
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
        if (bool_4)
        {
            return;
        }
        bool_4 = true;
        long num5 = 0L;
        Marshal.ReadIntPtr(new IntPtr((void*) &num5), 0);
        Marshal.ReadInt32(new IntPtr((void*) &num5), 0);
        Marshal.ReadInt64(new IntPtr((void*) &num5), 0);
        Marshal.WriteIntPtr(new IntPtr((void*) &num5), 0, IntPtr.Zero);
        Marshal.WriteInt32(new IntPtr((void*) &num5), 0, 0);
        Marshal.WriteInt64(new IntPtr((void*) &num5), 0, 0L);
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
                        Version version2 = new Version(4, 0, 0x766f, 0x427c);
                        Version version3 = new Version(4, 0, 0x766f, 0x4601);
                        if ((version >= version2) && (version < version3))
                        {
                            goto Label_01A6;
                        }
                    }
                }
                goto Label_01C3;
            Label_01A6:
                bool_5 = true;
            }
        }
    Label_01C3:
        reader = new BinaryReader(typeof(Class12).Assembly.GetManifestResourceStream("AJ3n07Hb7iBxZPuJMw.7t9gv3InJWms22edgF")) {
            BaseStream = { Position = 0L }
        };
        byte[] buffer6 = reader.ReadBytes((int) reader.BaseStream.Length);
        byte[] buffer15 = new byte[0x20];
        buffer15[0] = 0x66;
        buffer15[0] = 0x55;
        buffer15[0] = 160;
        buffer15[1] = 0x7e;
        buffer15[1] = 90;
        buffer15[1] = 0xb2;
        buffer15[1] = 160;
        buffer15[1] = 80;
        buffer15[2] = 130;
        buffer15[2] = 0x74;
        buffer15[2] = 0xc4;
        buffer15[2] = 0x2b;
        buffer15[3] = 0xcc;
        buffer15[3] = 0x8a;
        buffer15[3] = 0x6c;
        buffer15[3] = 0x7e;
        buffer15[3] = 220;
        buffer15[3] = 0x47;
        buffer15[4] = 120;
        buffer15[4] = 0x5d;
        buffer15[4] = 0x6d;
        buffer15[4] = 220;
        buffer15[4] = 0x3e;
        buffer15[4] = 0x21;
        buffer15[5] = 0x43;
        buffer15[5] = 0x8d;
        buffer15[5] = 0x5e;
        buffer15[5] = 0xc1;
        buffer15[6] = 0x5c;
        buffer15[6] = 0x75;
        buffer15[6] = 0x85;
        buffer15[6] = 0x5e;
        buffer15[6] = 0x90;
        buffer15[6] = 0xa5;
        buffer15[7] = 0xad;
        buffer15[7] = 160;
        buffer15[7] = 0x85;
        buffer15[7] = 0x12;
        buffer15[8] = 0x6d;
        buffer15[8] = 0xa5;
        buffer15[8] = 0x40;
        buffer15[9] = 0x85;
        buffer15[9] = 0xa2;
        buffer15[9] = 0x6c;
        buffer15[9] = 0x63;
        buffer15[9] = 0x4d;
        buffer15[9] = 7;
        buffer15[10] = 0x65;
        buffer15[10] = 0x72;
        buffer15[10] = 0x6c;
        buffer15[10] = 0x70;
        buffer15[11] = 0x7a;
        buffer15[11] = 0xb7;
        buffer15[11] = 0x5b;
        buffer15[11] = 0x60;
        buffer15[12] = 0x62;
        buffer15[12] = 0x6b;
        buffer15[12] = 0x84;
        buffer15[12] = 0x61;
        buffer15[12] = 0xb5;
        buffer15[13] = 0x35;
        buffer15[13] = 0x81;
        buffer15[13] = 90;
        buffer15[13] = 0x86;
        buffer15[13] = 0x66;
        buffer15[13] = 3;
        buffer15[14] = 0x5f;
        buffer15[14] = 0x8f;
        buffer15[14] = 0xa4;
        buffer15[14] = 0x26;
        buffer15[14] = 0x81;
        buffer15[14] = 9;
        buffer15[15] = 0xd3;
        buffer15[15] = 0xa1;
        buffer15[15] = 0x4f;
        buffer15[15] = 0xd9;
        buffer15[15] = 0x43;
        buffer15[0x10] = 0xc1;
        buffer15[0x10] = 0xa9;
        buffer15[0x10] = 0x9f;
        buffer15[0x10] = 0x6d;
        buffer15[0x10] = 0xe7;
        buffer15[0x11] = 0x6d;
        buffer15[0x11] = 0x95;
        buffer15[0x11] = 0x9c;
        buffer15[0x11] = 0xa6;
        buffer15[0x11] = 0xc2;
        buffer15[0x12] = 0x4d;
        buffer15[0x12] = 0x76;
        buffer15[0x12] = 0xf7;
        buffer15[0x13] = 0x90;
        buffer15[0x13] = 120;
        buffer15[0x13] = 0x68;
        buffer15[20] = 0xbf;
        buffer15[20] = 0x7a;
        buffer15[20] = 0xa2;
        buffer15[20] = 0x52;
        buffer15[20] = 0x85;
        buffer15[0x15] = 0x66;
        buffer15[0x15] = 0x62;
        buffer15[0x15] = 0x58;
        buffer15[0x15] = 0xe8;
        buffer15[0x16] = 150;
        buffer15[0x16] = 0xa8;
        buffer15[0x16] = 0x59;
        buffer15[0x16] = 0xa6;
        buffer15[0x16] = 0xb0;
        buffer15[0x16] = 0xa3;
        buffer15[0x17] = 0x9d;
        buffer15[0x17] = 0xd3;
        buffer15[0x17] = 0xd8;
        buffer15[0x17] = 0x7b;
        buffer15[0x17] = 0x84;
        buffer15[0x17] = 0xa3;
        buffer15[0x18] = 120;
        buffer15[0x18] = 0x2e;
        buffer15[0x18] = 0x84;
        buffer15[0x18] = 0x37;
        buffer15[0x19] = 0x75;
        buffer15[0x19] = 120;
        buffer15[0x19] = 0x29;
        buffer15[0x1a] = 0xc0;
        buffer15[0x1a] = 0x3b;
        buffer15[0x1a] = 0x13;
        buffer15[0x1b] = 0x8f;
        buffer15[0x1b] = 0x84;
        buffer15[0x1b] = 0x80;
        buffer15[0x1b] = 0x7f;
        buffer15[0x1b] = 0x9c;
        buffer15[0x1b] = 0x37;
        buffer15[0x1c] = 0x97;
        buffer15[0x1c] = 0x61;
        buffer15[0x1c] = 130;
        buffer15[0x1c] = 0x74;
        buffer15[0x1c] = 0x60;
        buffer15[0x1d] = 0xef;
        buffer15[0x1d] = 0x25;
        buffer15[0x1d] = 0x94;
        buffer15[0x1d] = 0x66;
        buffer15[0x1d] = 0xa6;
        buffer15[0x1d] = 0xba;
        buffer15[30] = 0x70;
        buffer15[30] = 0xa2;
        buffer15[30] = 0x79;
        buffer15[0x1f] = 0x92;
        buffer15[0x1f] = 0x6b;
        buffer15[0x1f] = 0xe9;
        byte[] rgbKey = buffer15;
        byte[] buffer16 = new byte[0x10];
        buffer16[0] = 0x76;
        buffer16[0] = 0x9f;
        buffer16[0] = 0x7e;
        buffer16[0] = 0x70;
        buffer16[1] = 0x8a;
        buffer16[1] = 0xa3;
        buffer16[1] = 0x1d;
        buffer16[2] = 0x99;
        buffer16[2] = 0x4c;
        buffer16[2] = 230;
        buffer16[2] = 0xbf;
        buffer16[3] = 0x83;
        buffer16[3] = 0x89;
        buffer16[3] = 0x9a;
        buffer16[3] = 0x5c;
        buffer16[3] = 0x8e;
        buffer16[4] = 0xa7;
        buffer16[4] = 0x7c;
        buffer16[4] = 0x57;
        buffer16[4] = 0x58;
        buffer16[4] = 0x76;
        buffer16[4] = 0xd7;
        buffer16[5] = 220;
        buffer16[5] = 0x60;
        buffer16[5] = 0x9a;
        buffer16[5] = 0x7f;
        buffer16[5] = 0x4d;
        buffer16[5] = 0xd8;
        buffer16[6] = 160;
        buffer16[6] = 0xa4;
        buffer16[6] = 0x62;
        buffer16[6] = 0x9d;
        buffer16[6] = 0x85;
        buffer16[6] = 0x4f;
        buffer16[7] = 0x5e;
        buffer16[7] = 0x8a;
        buffer16[7] = 0x85;
        buffer16[8] = 90;
        buffer16[8] = 0x6f;
        buffer16[8] = 0x85;
        buffer16[8] = 0x5b;
        buffer16[8] = 200;
        buffer16[9] = 140;
        buffer16[9] = 0x10;
        buffer16[9] = 160;
        buffer16[9] = 0xac;
        buffer16[10] = 0x93;
        buffer16[10] = 0x8f;
        buffer16[10] = 0x85;
        buffer16[11] = 0x8a;
        buffer16[11] = 0x65;
        buffer16[11] = 0x54;
        buffer16[12] = 100;
        buffer16[12] = 130;
        buffer16[12] = 0x76;
        buffer16[12] = 0xcf;
        buffer16[12] = 0x6b;
        buffer16[12] = 0xcf;
        buffer16[13] = 0x7a;
        buffer16[13] = 0x72;
        buffer16[13] = 0x21;
        buffer16[14] = 100;
        buffer16[14] = 0x85;
        buffer16[14] = 10;
        buffer16[15] = 0x81;
        buffer16[15] = 0x53;
        buffer16[15] = 0xc7;
        byte[] array = buffer16;
        Array.Reverse(array);
        byte[] publicKeyToken = typeof(Class12).Assembly.GetName().GetPublicKeyToken();
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
        stream2.Write(buffer6, 0, buffer6.Length);
        stream2.FlushFinalBlock();
        byte[] buffer7 = stream.ToArray();
        Array.Clear(array, 0, array.Length);
        stream.Close();
        stream2.Close();
        reader.Close();
        int num9 = buffer7.Length / 8;
        if (((source = buffer7) != null) && (source.Length != 0))
        {
            numRef = source;
            goto Label_0CE2;
        }
        fixed (byte* numRef = null)
        {
            int num7;
        Label_0CE2:
            num7 = 0;
            while (num7 < num9)
            {
                IntPtr ptr1 = (IntPtr) (numRef + (num7 * 8));
                ptr1[0] ^= (IntPtr) 0x4af854a8L;
                num7++;
            }
        }
        reader = new BinaryReader(new MemoryStream(buffer7)) {
            BaseStream = { Position = 0L }
        };
        long num3 = Marshal.GetHINSTANCE(typeof(Class12).Assembly.GetModules()[0]).ToInt64();
        int num13 = 0;
        int num6 = 0;
        if ((typeof(Class12).Assembly.Location == null) || (typeof(Class12).Assembly.Location.Length == 0))
        {
            num6 = 0x1c00;
        }
        int num15 = reader.ReadInt32();
        if (reader.ReadInt32() == 1)
        {
            IntPtr ptr8 = IntPtr.Zero;
            Assembly assembly = typeof(Class12).Assembly;
            ptr8 = OpenProcess(0x38, 1, (uint) Process.GetCurrentProcess().Id);
            if (IntPtr.Size == 4)
            {
                int_3 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt32();
            }
            long_1 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt64();
            IntPtr ptr5 = IntPtr.Zero;
            for (int j = 0; j < num15; j++)
            {
                IntPtr ptr9 = new IntPtr((long_1 + reader.ReadInt32()) - num6);
                VirtualProtect_1(ptr9, 4, 4, ref num13);
                if (IntPtr.Size == 4)
                {
                    WriteProcessMemory(ptr8, ptr9, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr5);
                }
                else
                {
                    WriteProcessMemory(ptr8, ptr9, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr5);
                }
                VirtualProtect_1(ptr9, 4, num13, ref num13);
            }
            while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
            {
                int num20 = reader.ReadInt32();
                IntPtr ptr6 = new IntPtr(long_1 + num20);
                int num12 = reader.ReadInt32();
                VirtualProtect_1(ptr6, num12 * 4, 4, ref num13);
                for (int k = 0; k < num12; k++)
                {
                    Marshal.WriteInt32(new IntPtr(ptr6.ToInt64() + (k * 4)), reader.ReadInt32());
                }
                VirtualProtect_1(ptr6, num12 * 4, num13, ref num13);
            }
            CloseHandle(ptr8);
            return;
        }
        for (int i = 0; i < num15; i++)
        {
            IntPtr ptr7 = new IntPtr((num3 + reader.ReadInt32()) - num6);
            VirtualProtect_1(ptr7, 4, 4, ref num13);
            Marshal.WriteInt32(ptr7, reader.ReadInt32());
            VirtualProtect_1(ptr7, 4, num13, ref num13);
        }
        hashtable_0 = new Hashtable(reader.ReadInt32() + 1);
        Struct15 struct3 = new Struct15 {
            byte_0 = new byte[] { 0x2a },
            bool_0 = false
        };
        hashtable_0.Add(0L, struct3);
        bool flag = false;
        while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
        {
            int num4 = reader.ReadInt32() - num6;
            int num8 = reader.ReadInt32();
            flag = false;
            if (num8 >= 0x70000000)
            {
                flag = true;
            }
            int count = reader.ReadInt32();
            byte[] buffer2 = reader.ReadBytes(count);
            Struct15 struct2 = new Struct15 {
                byte_0 = buffer2,
                bool_0 = flag
            };
            hashtable_0.Add(num3 + num4, struct2);
        }
        long_0 = Marshal.GetHINSTANCE(typeof(Class12).Assembly.GetModules()[0]).ToInt64();
        if (IntPtr.Size == 4)
        {
            int_0 = Convert.ToInt32(long_0);
        }
        byte[] bytes = new byte[] { 0x6d, 0x73, 0x63, 0x6f, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
        string str2 = Encoding.UTF8.GetString(bytes);
        IntPtr ptr = LoadLibrary(str2);
        if (ptr == IntPtr.Zero)
        {
            bytes = new byte[] { 0x63, 0x6c, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
            str2 = Encoding.UTF8.GetString(bytes);
            ptr = LoadLibrary(str2);
        }
        byte[] buffer = new byte[] { 0x67, 0x65, 0x74, 0x4a, 0x69, 0x74 };
        string str = Encoding.UTF8.GetString(buffer);
        Delegate7 delegate2 = (Delegate7) smethod_15(GetProcAddress(ptr, str), typeof(Delegate7));
        IntPtr ptr3 = delegate2();
        long num = 0L;
        if (IntPtr.Size == 4)
        {
            num = Marshal.ReadInt32(ptr3);
        }
        else
        {
            num = Marshal.ReadInt64(ptr3);
        }
        Marshal.ReadIntPtr(ptr3, 0);
        delegate6_1 = new Delegate6(Class12.smethod_13);
        IntPtr zero = IntPtr.Zero;
        zero = Marshal.GetFunctionPointerForDelegate(delegate6_1);
        long num18 = 0L;
        if (IntPtr.Size == 4)
        {
            num18 = Marshal.ReadInt32(new IntPtr(num));
        }
        else
        {
            num18 = Marshal.ReadInt64(new IntPtr(num));
        }
        Process currentProcess = Process.GetCurrentProcess();
        try
        {
            foreach (ProcessModule module2 in currentProcess.Modules)
            {
                if (((module2.ModuleName == str2) && ((num18 < module2.BaseAddress.ToInt64()) || (num18 > (module2.BaseAddress.ToInt64() + module2.ModuleMemorySize)))) && (typeof(Class12).Assembly.EntryPoint != null))
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
                    if (module3.BaseAddress.ToInt64() == long_0)
                    {
                        goto Label_136F;
                    }
                }
                goto Label_138E;
            Label_136F:
                num6 = 0;
            }
        }
        catch
        {
        }
    Label_138E:
        delegate6_0 = null;
        try
        {
            delegate6_0 = (Delegate6) smethod_15(new IntPtr(num18), typeof(Delegate6));
        }
        catch
        {
            try
            {
                Delegate delegate3 = smethod_15(new IntPtr(num18), typeof(Delegate6));
                delegate6_0 = (Delegate6) Delegate.CreateDelegate(typeof(Delegate6), delegate3.Method);
            }
            catch
            {
            }
        }
        int num19 = 0;
        if (((typeof(Class12).Assembly.EntryPoint == null) || (typeof(Class12).Assembly.EntryPoint.GetParameters().Length != 2)) || ((typeof(Class12).Assembly.Location == null) || (typeof(Class12).Assembly.Location.Length <= 0)))
        {
            try
            {
                ref byte pinned numRef2;
                object obj2 = typeof(Class12).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(typeof(Class12).Assembly.ManifestModule.ModuleHandle);
                if (obj2 is IntPtr)
                {
                    intptr_1 = (IntPtr) obj2;
                }
                if (obj2.GetType().ToString() == "System.Reflection.RuntimeModule")
                {
                    intptr_1 = (IntPtr) obj2.GetType().GetField("m_pData", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(obj2);
                }
                MemoryStream stream3 = new MemoryStream();
                stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                if (IntPtr.Size == 4)
                {
                    stream3.Write(BitConverter.GetBytes(intptr_1.ToInt32()), 0, 4);
                }
                else
                {
                    stream3.Write(BitConverter.GetBytes(intptr_1.ToInt64()), 0, 8);
                }
                stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                stream3.Position = 0L;
                byte[] buffer13 = stream3.ToArray();
                stream3.Close();
                uint nativeSizeOfCode = 0;
                try
                {
                    if (((source = buffer13) != null) && (source.Length != 0))
                    {
                        numRef2 = source;
                    }
                    else
                    {
                        numRef2 = null;
                    }
                    delegate6_1(new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), 0xcea1d7d, new IntPtr((void*) numRef2), ref nativeSizeOfCode);
                }
                finally
                {
                    numRef2 = null;
                }
            }
            catch
            {
            }
            RuntimeHelpers.PrepareDelegate(delegate6_0);
            RuntimeHelpers.PrepareMethod(delegate6_0.Method.MethodHandle);
            RuntimeHelpers.PrepareDelegate(delegate6_1);
            RuntimeHelpers.PrepareMethod(delegate6_1.Method.MethodHandle);
            byte[] buffer14 = null;
            if (IntPtr.Size != 4)
            {
                buffer14 = new byte[] { 
                    0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0x49, 0x39, 0x40, 8, 0x74, 12, 
                    0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0xff, 0xe0, 0x48, 0xb8, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0xff, 0xe0
                 };
            }
            else
            {
                buffer14 = new byte[] { 
                    0x55, 0x8b, 0xec, 0x8b, 0x45, 0x10, 0x81, 120, 4, 0x7d, 0x1d, 0xea, 12, 0x74, 7, 0xb8, 
                    0xb6, 0xb1, 0x4a, 6, 0xeb, 5, 0xb8, 0xb6, 0x92, 0x40, 12, 0x5d, 0xff, 0xe0
                 };
            }
            IntPtr destination = VirtualAlloc(IntPtr.Zero, (uint) buffer14.Length, 0x1000, 0x40);
            byte[] buffer8 = buffer14;
            byte[] buffer11 = null;
            byte[] buffer10 = null;
            byte[] buffer9 = null;
            if (IntPtr.Size == 4)
            {
                buffer9 = BitConverter.GetBytes(intptr_1.ToInt32());
                buffer11 = BitConverter.GetBytes(zero.ToInt32());
                buffer10 = BitConverter.GetBytes(Convert.ToInt32(num18));
            }
            else
            {
                buffer9 = BitConverter.GetBytes(intptr_1.ToInt64());
                buffer11 = BitConverter.GetBytes(zero.ToInt64());
                buffer10 = BitConverter.GetBytes(num18);
            }
            if (IntPtr.Size == 4)
            {
                buffer8[9] = buffer9[0];
                buffer8[10] = buffer9[1];
                buffer8[11] = buffer9[2];
                buffer8[12] = buffer9[3];
                buffer8[0x10] = buffer10[0];
                buffer8[0x11] = buffer10[1];
                buffer8[0x12] = buffer10[2];
                buffer8[0x13] = buffer10[3];
                buffer8[0x17] = buffer11[0];
                buffer8[0x18] = buffer11[1];
                buffer8[0x19] = buffer11[2];
                buffer8[0x1a] = buffer11[3];
            }
            else
            {
                buffer8[2] = buffer9[0];
                buffer8[3] = buffer9[1];
                buffer8[4] = buffer9[2];
                buffer8[5] = buffer9[3];
                buffer8[6] = buffer9[4];
                buffer8[7] = buffer9[5];
                buffer8[8] = buffer9[6];
                buffer8[9] = buffer9[7];
                buffer8[0x12] = buffer10[0];
                buffer8[0x13] = buffer10[1];
                buffer8[20] = buffer10[2];
                buffer8[0x15] = buffer10[3];
                buffer8[0x16] = buffer10[4];
                buffer8[0x17] = buffer10[5];
                buffer8[0x18] = buffer10[6];
                buffer8[0x19] = buffer10[7];
                buffer8[30] = buffer11[0];
                buffer8[0x1f] = buffer11[1];
                buffer8[0x20] = buffer11[2];
                buffer8[0x21] = buffer11[3];
                buffer8[0x22] = buffer11[4];
                buffer8[0x23] = buffer11[5];
                buffer8[0x24] = buffer11[6];
                buffer8[0x25] = buffer11[7];
            }
            Marshal.Copy(buffer8, 0, destination, buffer8.Length);
            bool_3 = false;
            VirtualProtect_1(new IntPtr(num), IntPtr.Size, 0x40, ref num19);
            Marshal.WriteIntPtr(new IntPtr(num), destination);
            VirtualProtect_1(new IntPtr(num), IntPtr.Size, num19, ref num19);
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

    [Attribute1(typeof(Attribute1.OKFMOHKLILCGIGFJIHLMAFEPGMLOAJMAPPJC<object>[]))]
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

    [Attribute1(typeof(Attribute1.OKFMOHKLILCGIGFJIHLMAFEPGMLOAJMAPPJC<object>[]))]
    private static byte[] smethod_19(byte[] byte_4)
    {
        MemoryStream stream = new MemoryStream();
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Key = new byte[] { 
            0x7e, 0x91, 110, 0x35, 0x8b, 0x4a, 0x44, 0xa2, 0xfd, 0xe2, 0xa2, 0x61, 0x54, 0xcd, 0x4c, 0x7a, 
            0xe7, 0xea, 0x36, 0xb3, 0xed, 0xa9, 0xec, 0x72, 0x49, 0xbb, 5, 0x89, 0x77, 0xb8, 0xb8, 0x92
         };
        algorithm.IV = new byte[] { 0xcc, 0xbf, 8, 0x21, 0xa8, 0xbc, 0xdb, 0xac, 0, 0x69, 200, 0x2b, 0x54, 0x86, 0xc2, 0xbc };
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
        if (!bool_6)
        {
            smethod_8();
            bool_6 = true;
        }
        return bool_1;
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
            bool_1 = (bool) typeof(RijndaelManaged).Assembly.GetType("System.Security.Cryptography.CryptoConfig", false).GetMethod("get_AllowOnlyFipsAlgorithms", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[0]);
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

    internal class Attribute1 : Attribute
    {
        [Attribute1(typeof(OKFMOHKLILCGIGFJIHLMAFEPGMLOAJMAPPJC<object>[]))]
        public Attribute1(object object_0)
        {
            Class14.smethod_0();
        }

        internal class OKFMOHKLILCGIGFJIHLMAFEPGMLOAJMAPPJC<T>
        {
            public OKFMOHKLILCGIGFJIHLMAFEPGMLOAJMAPPJC()
            {
                Class14.smethod_0();
            }
        }
    }

    internal class Class13
    {
        public Class13()
        {
            Class14.smethod_0();
        }

        [Attribute1(typeof(Class12.Attribute1.OKFMOHKLILCGIGFJIHLMAFEPGMLOAJMAPPJC<object>[]))]
        internal static string smethod_0(string string_0, string string_1)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string_0);
            byte[] buffer3 = new byte[] { 
                0x52, 0x66, 0x68, 110, 0x20, 0x4d, 0x18, 0x22, 0x76, 0xb5, 0x33, 0x11, 0x12, 0x33, 12, 0x6d, 
                10, 0x20, 0x4d, 0x18, 0x22, 0x9e, 0xa1, 0x29, 0x61, 0x1c, 0x76, 0xb5, 5, 0x19, 1, 0x58
             };
            byte[] buffer4 = Class12.smethod_9(Encoding.Unicode.GetBytes(string_1));
            MemoryStream stream = new MemoryStream();
            SymmetricAlgorithm algorithm = Class12.smethod_7();
            algorithm.Key = buffer3;
            algorithm.IV = buffer4;
            CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate uint Delegate6(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate IntPtr Delegate7();

    [Flags]
    private enum Enum1
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct15
    {
        internal bool bool_0;
        internal byte[] byte_0;
    }
}

