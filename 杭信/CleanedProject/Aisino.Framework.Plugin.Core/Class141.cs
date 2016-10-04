using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal class Class141
{
    [Attribute9(typeof(Attribute9.Class142<object>[]))]
    private static bool bool_0 = false;
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
    internal static Delegate45 delegate45_0 = null;
    internal static Delegate45 delegate45_1 = null;
    internal static Hashtable hashtable_0 = new Hashtable();
    private static int int_0 = 1;
    private static int int_1 = 0;
    private static int int_2 = 0;
    private static int int_3 = 0;
    private static int[] int_4 = new int[0];
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

    [Attribute9(typeof(Attribute9.Class142<object>[]))]
    /* private scope */ static bool smethod_10(int int_5)
    {
        if (byte_1.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class141).Assembly.GetManifestResourceStream("\x0098o\x0086\x0088v\x0098\x008b\x008a\x00999nv\x00899c\x009d31.ph5\x008d\x008fe\x008cxf\x0097d1e\x008e\x0094\x00871f")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x85;
            buffer2[0] = 0x8f;
            buffer2[0] = 0x58;
            buffer2[0] = 0x98;
            buffer2[1] = 0x9a;
            buffer2[1] = 0x36;
            buffer2[1] = 0xa6;
            buffer2[1] = 30;
            buffer2[2] = 140;
            buffer2[2] = 0x83;
            buffer2[2] = 0x87;
            buffer2[2] = 170;
            buffer2[2] = 0x7a;
            buffer2[2] = 0x37;
            buffer2[3] = 0xde;
            buffer2[3] = 0x72;
            buffer2[3] = 0x55;
            buffer2[3] = 0xa4;
            buffer2[3] = 0xa9;
            buffer2[3] = 0xf1;
            buffer2[4] = 0x9f;
            buffer2[4] = 0x66;
            buffer2[4] = 0x55;
            buffer2[4] = 0x5f;
            buffer2[4] = 0x5d;
            buffer2[4] = 0x65;
            buffer2[5] = 0xa9;
            buffer2[5] = 0x36;
            buffer2[5] = 0x7c;
            buffer2[5] = 0xc3;
            buffer2[5] = 0xb7;
            buffer2[6] = 50;
            buffer2[6] = 0xc4;
            buffer2[6] = 0x20;
            buffer2[6] = 0xa2;
            buffer2[6] = 0xd6;
            buffer2[6] = 0x44;
            buffer2[7] = 0x88;
            buffer2[7] = 0x7b;
            buffer2[7] = 0xae;
            buffer2[8] = 0x8a;
            buffer2[8] = 0x3f;
            buffer2[8] = 0x94;
            buffer2[8] = 0xb5;
            buffer2[8] = 160;
            buffer2[8] = 0x1a;
            buffer2[9] = 0x4d;
            buffer2[9] = 0x71;
            buffer2[9] = 0x67;
            buffer2[9] = 110;
            buffer2[9] = 0xa3;
            buffer2[10] = 150;
            buffer2[10] = 0xb7;
            buffer2[10] = 0x60;
            buffer2[10] = 0x75;
            buffer2[10] = 0x56;
            buffer2[11] = 110;
            buffer2[11] = 100;
            buffer2[11] = 0xa7;
            buffer2[11] = 0x71;
            buffer2[12] = 0x85;
            buffer2[12] = 0x86;
            buffer2[12] = 0x68;
            buffer2[12] = 0x7a;
            buffer2[12] = 60;
            buffer2[13] = 0x90;
            buffer2[13] = 100;
            buffer2[13] = 0xa6;
            buffer2[13] = 0x54;
            buffer2[13] = 0xb7;
            buffer2[14] = 0x92;
            buffer2[14] = 0x54;
            buffer2[14] = 200;
            buffer2[14] = 0x2c;
            buffer2[15] = 0x8e;
            buffer2[15] = 0x45;
            buffer2[15] = 0x9b;
            buffer2[0x10] = 0x41;
            buffer2[0x10] = 0xe7;
            buffer2[0x10] = 0x8a;
            buffer2[0x10] = 0x6d;
            buffer2[0x10] = 0xcb;
            buffer2[0x11] = 0xa5;
            buffer2[0x11] = 0x72;
            buffer2[0x11] = 0x5d;
            buffer2[0x11] = 0xa4;
            buffer2[0x11] = 0x52;
            buffer2[0x12] = 0xc0;
            buffer2[0x12] = 0x45;
            buffer2[0x12] = 0xab;
            buffer2[0x13] = 0x66;
            buffer2[0x13] = 0x4d;
            buffer2[0x13] = 0x6b;
            buffer2[20] = 0x7a;
            buffer2[20] = 0x75;
            buffer2[20] = 0x88;
            buffer2[20] = 0x74;
            buffer2[0x15] = 140;
            buffer2[0x15] = 0x58;
            buffer2[0x15] = 0xd6;
            buffer2[0x15] = 0x4e;
            buffer2[0x15] = 0x5f;
            buffer2[0x15] = 0x60;
            buffer2[0x16] = 0x61;
            buffer2[0x16] = 0xe5;
            buffer2[0x16] = 0x8d;
            buffer2[0x16] = 0x57;
            buffer2[0x16] = 0xfc;
            buffer2[0x17] = 0xb3;
            buffer2[0x17] = 0x7a;
            buffer2[0x17] = 0x9c;
            buffer2[0x17] = 0x9a;
            buffer2[0x17] = 30;
            buffer2[0x18] = 0x4c;
            buffer2[0x18] = 0xa9;
            buffer2[0x18] = 0x94;
            buffer2[0x18] = 0xc0;
            buffer2[0x19] = 0x31;
            buffer2[0x19] = 70;
            buffer2[0x19] = 0x98;
            buffer2[0x19] = 0x88;
            buffer2[0x1a] = 150;
            buffer2[0x1a] = 0x8a;
            buffer2[0x1a] = 0xa6;
            buffer2[0x1a] = 0xd5;
            buffer2[0x1a] = 0x45;
            buffer2[0x1b] = 0x67;
            buffer2[0x1b] = 0x54;
            buffer2[0x1b] = 0xe3;
            buffer2[0x1c] = 0x27;
            buffer2[0x1c] = 0x6a;
            buffer2[0x1c] = 0x62;
            buffer2[0x1d] = 0xbf;
            buffer2[0x1d] = 0x9d;
            buffer2[0x1d] = 0x9e;
            buffer2[0x1d] = 130;
            buffer2[0x1d] = 0xbd;
            buffer2[30] = 0x87;
            buffer2[30] = 0x72;
            buffer2[30] = 0xa8;
            buffer2[30] = 0xb5;
            buffer2[30] = 0x5b;
            buffer2[30] = 0xc9;
            buffer2[0x1f] = 150;
            buffer2[0x1f] = 0x92;
            buffer2[0x1f] = 0xa5;
            buffer2[0x1f] = 0x72;
            buffer2[0x1f] = 0xe4;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 140;
            buffer4[0] = 0x3a;
            buffer4[0] = 0x7e;
            buffer4[0] = 0x8f;
            buffer4[0] = 0x7e;
            buffer4[1] = 0x9f;
            buffer4[1] = 0x86;
            buffer4[1] = 0xb2;
            buffer4[1] = 0xdf;
            buffer4[2] = 60;
            buffer4[2] = 0x66;
            buffer4[2] = 0x89;
            buffer4[2] = 0xbf;
            buffer4[3] = 0x93;
            buffer4[3] = 0x91;
            buffer4[3] = 0x92;
            buffer4[4] = 0x5d;
            buffer4[4] = 0x6b;
            buffer4[4] = 0x9e;
            buffer4[4] = 0x6a;
            buffer4[4] = 0x57;
            buffer4[4] = 0x74;
            buffer4[5] = 130;
            buffer4[5] = 0x60;
            buffer4[5] = 0x8a;
            buffer4[5] = 0x69;
            buffer4[6] = 0x60;
            buffer4[6] = 0xa7;
            buffer4[6] = 0x76;
            buffer4[6] = 0xd9;
            buffer4[7] = 0x9d;
            buffer4[7] = 0x79;
            buffer4[7] = 0x7f;
            buffer4[7] = 0x90;
            buffer4[7] = 0x54;
            buffer4[7] = 50;
            buffer4[8] = 0x9e;
            buffer4[8] = 0x6a;
            buffer4[8] = 0xb5;
            buffer4[8] = 0x65;
            buffer4[8] = 0x84;
            buffer4[8] = 0x87;
            buffer4[9] = 0x8a;
            buffer4[9] = 0x55;
            buffer4[9] = 0x6c;
            buffer4[9] = 160;
            buffer4[10] = 0x5c;
            buffer4[10] = 0x5c;
            buffer4[10] = 0x6a;
            buffer4[10] = 130;
            buffer4[10] = 130;
            buffer4[10] = 0x38;
            buffer4[11] = 0x99;
            buffer4[11] = 0x6a;
            buffer4[11] = 110;
            buffer4[11] = 0x2c;
            buffer4[11] = 8;
            buffer4[12] = 0x6b;
            buffer4[12] = 0x93;
            buffer4[12] = 120;
            buffer4[12] = 0x7a;
            buffer4[13] = 0x91;
            buffer4[13] = 0x8e;
            buffer4[13] = 0x6a;
            buffer4[13] = 110;
            buffer4[13] = 0x8d;
            buffer4[13] = 0xf4;
            buffer4[14] = 0x7b;
            buffer4[14] = 0x86;
            buffer4[14] = 0xd6;
            buffer4[15] = 110;
            buffer4[15] = 0xd4;
            buffer4[15] = 100;
            buffer4[15] = 0x61;
            byte[] rgbIV = buffer4;
            byte[] publicKeyToken = typeof(Class141).Assembly.GetName().GetPublicKeyToken();
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
            byte_1 = stream.ToArray();
            stream.Close();
            stream2.Close();
            reader.Close();
        }
        if (byte_2.Length == 0)
        {
            byte_2 = smethod_18(smethod_17(typeof(Class141).Assembly).ToString());
        }
        int index = 0;
        try
        {
            index = BitConverter.ToInt32(new byte[] { byte_1[int_5], byte_1[int_5 + 1], byte_1[int_5 + 2], byte_1[int_5 + 3] }, 0);
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

    [Attribute9(typeof(Attribute9.Class142<object>[]))]
    /* private scope */ static string smethod_11(int int_5)
    {
        if (byte_3.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class141).Assembly.GetManifestResourceStream("79k\x00981\x0090\x0087y8\x009a\x0095t0o\x0096\x009co\x0090.x7f\x0088h7\x0088q\x009b\x0099\x009f\x0097i1gu\x008er")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer4 = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer5 = new byte[0x20];
            buffer5[0] = 0x7f;
            buffer5[0] = 0x5f;
            buffer5[0] = 0x98;
            buffer5[0] = 0x90;
            buffer5[0] = 0x80;
            buffer5[1] = 140;
            buffer5[1] = 0x68;
            buffer5[1] = 0x99;
            buffer5[1] = 0x94;
            buffer5[1] = 0x3a;
            buffer5[2] = 130;
            buffer5[2] = 0x6a;
            buffer5[2] = 0x80;
            buffer5[2] = 0x92;
            buffer5[2] = 0x91;
            buffer5[3] = 0x73;
            buffer5[3] = 0x77;
            buffer5[3] = 0x15;
            buffer5[4] = 0xa9;
            buffer5[4] = 0xb8;
            buffer5[4] = 0x9b;
            buffer5[4] = 0x85;
            buffer5[4] = 0xa8;
            buffer5[4] = 0x61;
            buffer5[5] = 0x7a;
            buffer5[5] = 160;
            buffer5[5] = 0x57;
            buffer5[5] = 0xa2;
            buffer5[5] = 0x7b;
            buffer5[5] = 0x91;
            buffer5[6] = 0x76;
            buffer5[6] = 0x75;
            buffer5[6] = 0x8a;
            buffer5[6] = 0x9e;
            buffer5[7] = 0x51;
            buffer5[7] = 5;
            buffer5[7] = 0xc7;
            buffer5[7] = 0x56;
            buffer5[7] = 0x5c;
            buffer5[8] = 0x9e;
            buffer5[8] = 0x99;
            buffer5[8] = 0x9d;
            buffer5[8] = 0x6b;
            buffer5[9] = 100;
            buffer5[9] = 0x42;
            buffer5[9] = 0xec;
            buffer5[10] = 0x2b;
            buffer5[10] = 0x99;
            buffer5[10] = 0xa4;
            buffer5[10] = 0x5e;
            buffer5[10] = 0xd5;
            buffer5[10] = 0xa9;
            buffer5[11] = 0x51;
            buffer5[11] = 12;
            buffer5[11] = 0x92;
            buffer5[11] = 0xa5;
            buffer5[11] = 0x55;
            buffer5[11] = 0x8d;
            buffer5[12] = 110;
            buffer5[12] = 0x88;
            buffer5[12] = 100;
            buffer5[12] = 0xa3;
            buffer5[12] = 0x84;
            buffer5[12] = 11;
            buffer5[13] = 0x9b;
            buffer5[13] = 0x99;
            buffer5[13] = 0x6a;
            buffer5[13] = 100;
            buffer5[13] = 0x9a;
            buffer5[14] = 0xba;
            buffer5[14] = 0x97;
            buffer5[14] = 0xb3;
            buffer5[15] = 0x49;
            buffer5[15] = 0x89;
            buffer5[15] = 0x74;
            buffer5[15] = 0x8b;
            buffer5[15] = 0x62;
            buffer5[15] = 0x97;
            buffer5[0x10] = 0x8b;
            buffer5[0x10] = 90;
            buffer5[0x10] = 0x60;
            buffer5[0x10] = 120;
            buffer5[0x10] = 0x56;
            buffer5[0x10] = 0x24;
            buffer5[0x11] = 0x8d;
            buffer5[0x11] = 0xbb;
            buffer5[0x11] = 0xa9;
            buffer5[0x11] = 0x3a;
            buffer5[0x11] = 0x7e;
            buffer5[0x11] = 0xac;
            buffer5[0x12] = 0x9d;
            buffer5[0x12] = 0xa9;
            buffer5[0x12] = 0x5c;
            buffer5[0x12] = 120;
            buffer5[0x12] = 0x51;
            buffer5[0x12] = 80;
            buffer5[0x13] = 0x5c;
            buffer5[0x13] = 120;
            buffer5[0x13] = 0x67;
            buffer5[0x13] = 150;
            buffer5[0x13] = 0x71;
            buffer5[0x13] = 0xb3;
            buffer5[20] = 0x94;
            buffer5[20] = 0x56;
            buffer5[20] = 0xa9;
            buffer5[20] = 0x56;
            buffer5[20] = 0xc5;
            buffer5[20] = 0xac;
            buffer5[0x15] = 0xa8;
            buffer5[0x15] = 90;
            buffer5[0x15] = 0x74;
            buffer5[0x15] = 0x45;
            buffer5[0x15] = 0x77;
            buffer5[0x16] = 0x67;
            buffer5[0x16] = 150;
            buffer5[0x16] = 0xa2;
            buffer5[0x16] = 90;
            buffer5[0x17] = 0x8d;
            buffer5[0x17] = 0xbb;
            buffer5[0x17] = 0x6a;
            buffer5[0x17] = 0x8a;
            buffer5[0x17] = 0xbb;
            buffer5[0x18] = 0xa4;
            buffer5[0x18] = 0x91;
            buffer5[0x18] = 0x8e;
            buffer5[0x18] = 0x8f;
            buffer5[0x19] = 0x97;
            buffer5[0x19] = 0x7e;
            buffer5[0x19] = 190;
            buffer5[0x19] = 0xae;
            buffer5[0x19] = 0xc3;
            buffer5[0x19] = 0xe2;
            buffer5[0x1a] = 0x3d;
            buffer5[0x1a] = 0x6b;
            buffer5[0x1a] = 0x89;
            buffer5[0x1a] = 0x90;
            buffer5[0x1a] = 0x98;
            buffer5[0x1a] = 0x33;
            buffer5[0x1b] = 0x62;
            buffer5[0x1b] = 180;
            buffer5[0x1b] = 0x6f;
            buffer5[0x1c] = 140;
            buffer5[0x1c] = 0x7e;
            buffer5[0x1c] = 100;
            buffer5[0x1c] = 0x80;
            buffer5[0x1c] = 0xc3;
            buffer5[0x1d] = 0x86;
            buffer5[0x1d] = 0x92;
            buffer5[0x1d] = 0xa9;
            buffer5[0x1d] = 0x7a;
            buffer5[30] = 0x7a;
            buffer5[30] = 0xb3;
            buffer5[30] = 0x71;
            buffer5[30] = 0xab;
            buffer5[0x1f] = 0x9c;
            buffer5[0x1f] = 0x80;
            buffer5[0x1f] = 0x60;
            byte[] rgbKey = buffer5;
            byte[] buffer6 = new byte[0x10];
            buffer6[0] = 0xb0;
            buffer6[0] = 170;
            buffer6[0] = 0xa4;
            buffer6[0] = 0xc5;
            buffer6[0] = 0xae;
            buffer6[1] = 0x7e;
            buffer6[1] = 0x6d;
            buffer6[1] = 0x6c;
            buffer6[1] = 0x86;
            buffer6[2] = 0x25;
            buffer6[2] = 0x68;
            buffer6[2] = 0x93;
            buffer6[2] = 0xc1;
            buffer6[3] = 0x58;
            buffer6[3] = 0xb6;
            buffer6[3] = 0xfd;
            buffer6[4] = 0x4b;
            buffer6[4] = 0x58;
            buffer6[4] = 0x22;
            buffer6[4] = 0x20;
            buffer6[5] = 0x90;
            buffer6[5] = 0x6a;
            buffer6[5] = 130;
            buffer6[5] = 0x53;
            buffer6[6] = 0x6a;
            buffer6[6] = 0x99;
            buffer6[6] = 0xbb;
            buffer6[6] = 110;
            buffer6[6] = 0x10;
            buffer6[7] = 0x8e;
            buffer6[7] = 0x74;
            buffer6[7] = 130;
            buffer6[7] = 50;
            buffer6[8] = 0x90;
            buffer6[8] = 0x37;
            buffer6[8] = 0x5e;
            buffer6[8] = 80;
            buffer6[9] = 0x41;
            buffer6[9] = 0x7e;
            buffer6[9] = 160;
            buffer6[9] = 0x2a;
            buffer6[9] = 90;
            buffer6[9] = 0x97;
            buffer6[10] = 0xa1;
            buffer6[10] = 0x4c;
            buffer6[10] = 0xc0;
            buffer6[11] = 0xee;
            buffer6[11] = 0x9a;
            buffer6[11] = 0x48;
            buffer6[11] = 0xb5;
            buffer6[11] = 0x88;
            buffer6[11] = 0x8b;
            buffer6[12] = 0x70;
            buffer6[12] = 0x8a;
            buffer6[12] = 0xbb;
            buffer6[12] = 0x84;
            buffer6[12] = 0x68;
            buffer6[12] = 80;
            buffer6[13] = 0xb3;
            buffer6[13] = 0x87;
            buffer6[13] = 0xa4;
            buffer6[13] = 0x76;
            buffer6[13] = 0x90;
            buffer6[13] = 0xde;
            buffer6[14] = 0x74;
            buffer6[14] = 0x68;
            buffer6[14] = 0x92;
            buffer6[14] = 0x94;
            buffer6[14] = 0x60;
            buffer6[14] = 0xc6;
            buffer6[15] = 0xd0;
            buffer6[15] = 0x95;
            buffer6[15] = 0x8d;
            buffer6[15] = 0x81;
            buffer6[15] = 0x5e;
            buffer6[15] = 0xe3;
            byte[] array = buffer6;
            Array.Reverse(array);
            byte[] publicKeyToken = typeof(Class141).Assembly.GetName().GetPublicKeyToken();
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
            stream2.Write(buffer4, 0, buffer4.Length);
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

    [Attribute9(typeof(Attribute9.Class142<object>[]))]
    internal static string smethod_12(string string_1)
    {
        "{11111-22222-50001-00000}".Trim();
        byte[] bytes = Convert.FromBase64String(string_1);
        return Encoding.Unicode.GetString(bytes, 0, bytes.Length);
    }

    internal static uint smethod_13(IntPtr intptr_3, IntPtr intptr_4, IntPtr intptr_5, [MarshalAs(UnmanagedType.U4)] uint uint_1, IntPtr intptr_6, ref uint uint_2)
    {
        IntPtr ptr = intptr_5;
        if (bool_6)
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
            Struct168 struct2 = (Struct168) obj2;
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
            if ((uint_1 == 0xcea1d7d) && !bool_0)
            {
                bool_0 = true;
                return num2;
            }
        }
        return delegate45_0(intptr_3, intptr_4, intptr_5, uint_1, intptr_6, ref uint_2);
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
                bool_6 = true;
            }
        }
    Label_01C3:
        reader = new BinaryReader(typeof(Class141).Assembly.GetManifestResourceStream("\x008a\x0090\x008epig\x008dodpa\x0094g\x009a\x008bdl\x0096.\x0091xq9\x0089d\x008ea\x008b4\x009ca\x009d\x0096\x009e\x0086\x0094\x009c")) {
            BaseStream = { Position = 0L }
        };
        byte[] buffer9 = reader.ReadBytes((int) reader.BaseStream.Length);
        byte[] buffer12 = new byte[0x20];
        buffer12[0] = 0x81;
        buffer12[0] = 0x8a;
        buffer12[0] = 0x54;
        buffer12[0] = 0x9f;
        buffer12[0] = 170;
        buffer12[0] = 0xd7;
        buffer12[1] = 0x58;
        buffer12[1] = 0xe2;
        buffer12[1] = 0x83;
        buffer12[1] = 0xb6;
        buffer12[2] = 0x9c;
        buffer12[2] = 0x91;
        buffer12[2] = 0x58;
        buffer12[3] = 0xc4;
        buffer12[3] = 0x9d;
        buffer12[3] = 15;
        buffer12[3] = 0x8a;
        buffer12[3] = 60;
        buffer12[4] = 0x56;
        buffer12[4] = 0x81;
        buffer12[4] = 0x94;
        buffer12[4] = 0x7f;
        buffer12[4] = 0xc3;
        buffer12[5] = 0xa7;
        buffer12[5] = 0x88;
        buffer12[5] = 0x74;
        buffer12[5] = 0x5f;
        buffer12[5] = 0xa5;
        buffer12[5] = 140;
        buffer12[6] = 0xc2;
        buffer12[6] = 0x75;
        buffer12[6] = 0x70;
        buffer12[6] = 0x55;
        buffer12[6] = 0x6c;
        buffer12[6] = 70;
        buffer12[7] = 0xb0;
        buffer12[7] = 0x79;
        buffer12[7] = 0xc4;
        buffer12[7] = 0xa5;
        buffer12[7] = 0x91;
        buffer12[7] = 0xa1;
        buffer12[8] = 0x41;
        buffer12[8] = 0x93;
        buffer12[8] = 9;
        buffer12[9] = 0x7d;
        buffer12[9] = 0xab;
        buffer12[9] = 0x69;
        buffer12[9] = 150;
        buffer12[10] = 0x70;
        buffer12[10] = 0xc0;
        buffer12[10] = 150;
        buffer12[10] = 0x4b;
        buffer12[10] = 0x5e;
        buffer12[10] = 0xb5;
        buffer12[11] = 0xc0;
        buffer12[11] = 0x24;
        buffer12[11] = 0x7c;
        buffer12[11] = 140;
        buffer12[11] = 0x45;
        buffer12[11] = 0x86;
        buffer12[12] = 0x61;
        buffer12[12] = 0xa8;
        buffer12[12] = 0xa2;
        buffer12[12] = 0x94;
        buffer12[13] = 0x98;
        buffer12[13] = 0x86;
        buffer12[13] = 130;
        buffer12[14] = 0xa9;
        buffer12[14] = 120;
        buffer12[14] = 0xbc;
        buffer12[14] = 0x5d;
        buffer12[15] = 0x89;
        buffer12[15] = 150;
        buffer12[15] = 0x54;
        buffer12[15] = 0x2e;
        buffer12[15] = 0x68;
        buffer12[15] = 0xfc;
        buffer12[0x10] = 0x54;
        buffer12[0x10] = 0x6a;
        buffer12[0x10] = 0x77;
        buffer12[0x10] = 0xcd;
        buffer12[0x10] = 220;
        buffer12[0x11] = 170;
        buffer12[0x11] = 14;
        buffer12[0x11] = 0xed;
        buffer12[0x12] = 0x74;
        buffer12[0x12] = 0x74;
        buffer12[0x12] = 0x3a;
        buffer12[0x12] = 110;
        buffer12[0x12] = 0xac;
        buffer12[0x13] = 0x47;
        buffer12[0x13] = 0x17;
        buffer12[0x13] = 0x7f;
        buffer12[0x13] = 0x8e;
        buffer12[0x13] = 0xf8;
        buffer12[20] = 0xd9;
        buffer12[20] = 0x7f;
        buffer12[20] = 0x52;
        buffer12[20] = 160;
        buffer12[20] = 0xa2;
        buffer12[20] = 0xa1;
        buffer12[0x15] = 0xb1;
        buffer12[0x15] = 50;
        buffer12[0x15] = 0x7b;
        buffer12[0x15] = 0x8e;
        buffer12[0x15] = 120;
        buffer12[0x15] = 0x7c;
        buffer12[0x16] = 0x95;
        buffer12[0x16] = 0x5e;
        buffer12[0x16] = 0xc7;
        buffer12[0x17] = 0x8d;
        buffer12[0x17] = 0xf6;
        buffer12[0x17] = 0x8d;
        buffer12[0x17] = 0x79;
        buffer12[0x17] = 0x73;
        buffer12[0x17] = 0xe7;
        buffer12[0x18] = 0x31;
        buffer12[0x18] = 0x6b;
        buffer12[0x18] = 0x81;
        buffer12[0x18] = 0x5f;
        buffer12[0x18] = 0x87;
        buffer12[0x18] = 0x5c;
        buffer12[0x19] = 90;
        buffer12[0x19] = 110;
        buffer12[0x19] = 0x36;
        buffer12[0x19] = 0x73;
        buffer12[0x19] = 0x69;
        buffer12[0x1a] = 0x66;
        buffer12[0x1a] = 0x5e;
        buffer12[0x1a] = 110;
        buffer12[0x1a] = 0x53;
        buffer12[0x1b] = 0x71;
        buffer12[0x1b] = 0x56;
        buffer12[0x1b] = 0x8a;
        buffer12[0x1b] = 0xec;
        buffer12[0x1b] = 0xa8;
        buffer12[0x1b] = 0x1b;
        buffer12[0x1c] = 0x7d;
        buffer12[0x1c] = 0x5c;
        buffer12[0x1c] = 0x6d;
        buffer12[0x1d] = 0x49;
        buffer12[0x1d] = 0x85;
        buffer12[0x1d] = 0x89;
        buffer12[0x1d] = 0x39;
        buffer12[0x1d] = 0x5e;
        buffer12[0x1d] = 0x19;
        buffer12[30] = 0x9a;
        buffer12[30] = 0x62;
        buffer12[30] = 0xa8;
        buffer12[0x1f] = 0x56;
        buffer12[0x1f] = 0x76;
        buffer12[0x1f] = 0x81;
        buffer12[0x1f] = 0x5f;
        buffer12[0x1f] = 0x62;
        buffer12[0x1f] = 0x4f;
        byte[] rgbKey = buffer12;
        byte[] buffer13 = new byte[0x10];
        buffer13[0] = 0x66;
        buffer13[0] = 0x5c;
        buffer13[0] = 0x99;
        buffer13[0] = 0x39;
        buffer13[0] = 0x77;
        buffer13[0] = 0xc5;
        buffer13[1] = 0xc9;
        buffer13[1] = 0x94;
        buffer13[1] = 0xa3;
        buffer13[1] = 180;
        buffer13[1] = 0xa1;
        buffer13[1] = 0xa6;
        buffer13[2] = 0x67;
        buffer13[2] = 0x80;
        buffer13[2] = 0xe8;
        buffer13[2] = 0x66;
        buffer13[3] = 14;
        buffer13[3] = 0x61;
        buffer13[3] = 0xa5;
        buffer13[3] = 0x85;
        buffer13[3] = 0x66;
        buffer13[4] = 140;
        buffer13[4] = 0x62;
        buffer13[4] = 0x71;
        buffer13[4] = 0x48;
        buffer13[5] = 0x94;
        buffer13[5] = 0x89;
        buffer13[5] = 0x61;
        buffer13[5] = 150;
        buffer13[5] = 1;
        buffer13[6] = 0xe5;
        buffer13[6] = 190;
        buffer13[6] = 210;
        buffer13[6] = 0x60;
        buffer13[6] = 0xc6;
        buffer13[7] = 0x8e;
        buffer13[7] = 160;
        buffer13[7] = 0x5e;
        buffer13[7] = 0xae;
        buffer13[7] = 0x62;
        buffer13[8] = 150;
        buffer13[8] = 0x99;
        buffer13[8] = 0xa7;
        buffer13[8] = 0x2c;
        buffer13[9] = 0x5c;
        buffer13[9] = 0x8e;
        buffer13[9] = 0xc7;
        buffer13[9] = 0x62;
        buffer13[9] = 0xaf;
        buffer13[9] = 0x25;
        buffer13[10] = 0x6d;
        buffer13[10] = 0x8a;
        buffer13[10] = 0x55;
        buffer13[10] = 0x4b;
        buffer13[10] = 0x67;
        buffer13[11] = 0x98;
        buffer13[11] = 0x79;
        buffer13[11] = 0xa4;
        buffer13[12] = 0x2e;
        buffer13[12] = 0x89;
        buffer13[12] = 0xe8;
        buffer13[13] = 0xc3;
        buffer13[13] = 0x72;
        buffer13[13] = 0x59;
        buffer13[14] = 0xcc;
        buffer13[14] = 0x90;
        buffer13[14] = 0xed;
        buffer13[15] = 0xa3;
        buffer13[15] = 0x6d;
        buffer13[15] = 0x6c;
        buffer13[15] = 0x45;
        buffer13[15] = 0xbc;
        buffer13[15] = 0xa1;
        byte[] array = buffer13;
        Array.Reverse(array);
        byte[] publicKeyToken = typeof(Class141).Assembly.GetName().GetPublicKeyToken();
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
        stream2.Write(buffer9, 0, buffer9.Length);
        stream2.FlushFinalBlock();
        byte[] buffer = stream.ToArray();
        Array.Clear(array, 0, array.Length);
        stream.Close();
        stream2.Close();
        reader.Close();
        int num11 = buffer.Length / 8;
        if (((source = buffer) != null) && (source.Length != 0))
        {
            numRef = source;
            goto Label_0D88;
        }
        fixed (byte* numRef = null)
        {
            int num3;
        Label_0D88:
            num3 = 0;
            while (num3 < num11)
            {
                IntPtr ptr1 = (IntPtr) (numRef + (num3 * 8));
                ptr1[0] ^= (IntPtr) 0x53f0d08cL;
                num3++;
            }
        }
        reader = new BinaryReader(new MemoryStream(buffer)) {
            BaseStream = { Position = 0L }
        };
        long num14 = Marshal.GetHINSTANCE(typeof(Class141).Assembly.GetModules()[0]).ToInt64();
        int num6 = 0;
        int num5 = 0;
        if ((typeof(Class141).Assembly.Location == null) || (typeof(Class141).Assembly.Location.Length == 0))
        {
            num5 = 0x1c00;
        }
        int num18 = reader.ReadInt32();
        if (reader.ReadInt32() == 1)
        {
            IntPtr ptr = IntPtr.Zero;
            Assembly assembly = typeof(Class141).Assembly;
            ptr = OpenProcess(0x38, 1, (uint) Process.GetCurrentProcess().Id);
            if (IntPtr.Size == 4)
            {
                int_2 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt32();
            }
            long_1 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt64();
            IntPtr ptr3 = IntPtr.Zero;
            for (int j = 0; j < num18; j++)
            {
                IntPtr ptr2 = new IntPtr((long_1 + reader.ReadInt32()) - num5);
                VirtualProtect_1(ptr2, 4, 4, ref num6);
                if (IntPtr.Size == 4)
                {
                    WriteProcessMemory(ptr, ptr2, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr3);
                }
                else
                {
                    WriteProcessMemory(ptr, ptr2, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr3);
                }
                VirtualProtect_1(ptr2, 4, num6, ref num6);
            }
            while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
            {
                int num7 = reader.ReadInt32();
                IntPtr ptr6 = new IntPtr(long_1 + num7);
                int num8 = reader.ReadInt32();
                VirtualProtect_1(ptr6, num8 * 4, 4, ref num6);
                for (int k = 0; k < num8; k++)
                {
                    Marshal.WriteInt32(new IntPtr(ptr6.ToInt64() + (k * 4)), reader.ReadInt32());
                }
                VirtualProtect_1(ptr6, num8 * 4, num6, ref num6);
            }
            CloseHandle(ptr);
            return;
        }
        for (int i = 0; i < num18; i++)
        {
            IntPtr ptr10 = new IntPtr((num14 + reader.ReadInt32()) - num5);
            VirtualProtect_1(ptr10, 4, 4, ref num6);
            Marshal.WriteInt32(ptr10, reader.ReadInt32());
            VirtualProtect_1(ptr10, 4, num6, ref num6);
        }
        hashtable_0 = new Hashtable(reader.ReadInt32() + 1);
        Struct168 struct3 = new Struct168 {
            byte_0 = new byte[] { 0x2a },
            bool_0 = false
        };
        hashtable_0.Add(0L, struct3);
        bool flag = false;
        while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
        {
            int num15 = reader.ReadInt32() - num5;
            int num17 = reader.ReadInt32();
            flag = false;
            if (num17 >= 0x70000000)
            {
                flag = true;
            }
            int count = reader.ReadInt32();
            byte[] buffer16 = reader.ReadBytes(count);
            Struct168 struct2 = new Struct168 {
                byte_0 = buffer16,
                bool_0 = flag
            };
            hashtable_0.Add(num14 + num15, struct2);
        }
        long_0 = Marshal.GetHINSTANCE(typeof(Class141).Assembly.GetModules()[0]).ToInt64();
        if (IntPtr.Size == 4)
        {
            int_1 = Convert.ToInt32(long_0);
        }
        byte[] bytes = new byte[] { 0x6d, 0x73, 0x63, 0x6f, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
        string str = Encoding.UTF8.GetString(bytes);
        IntPtr ptr7 = LoadLibrary(str);
        if (ptr7 == IntPtr.Zero)
        {
            bytes = new byte[] { 0x63, 0x6c, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
            str = Encoding.UTF8.GetString(bytes);
            ptr7 = LoadLibrary(str);
        }
        byte[] buffer17 = new byte[] { 0x67, 0x65, 0x74, 0x4a, 0x69, 0x74 };
        string str2 = Encoding.UTF8.GetString(buffer17);
        Delegate46 delegate2 = (Delegate46) smethod_15(GetProcAddress(ptr7, str2), typeof(Delegate46));
        IntPtr ptr4 = delegate2();
        long num2 = 0L;
        if (IntPtr.Size == 4)
        {
            num2 = Marshal.ReadInt32(ptr4);
        }
        else
        {
            num2 = Marshal.ReadInt64(ptr4);
        }
        Marshal.ReadIntPtr(ptr4, 0);
        delegate45_1 = new Delegate45(Class141.smethod_13);
        IntPtr zero = IntPtr.Zero;
        zero = Marshal.GetFunctionPointerForDelegate(delegate45_1);
        long num4 = 0L;
        if (IntPtr.Size == 4)
        {
            num4 = Marshal.ReadInt32(new IntPtr(num2));
        }
        else
        {
            num4 = Marshal.ReadInt64(new IntPtr(num2));
        }
        Process currentProcess = Process.GetCurrentProcess();
        try
        {
            foreach (ProcessModule module in currentProcess.Modules)
            {
                if (((module.ModuleName == str) && ((num4 < module.BaseAddress.ToInt64()) || (num4 > (module.BaseAddress.ToInt64() + module.ModuleMemorySize)))) && (typeof(Class141).Assembly.EntryPoint != null))
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
                    ProcessModule module2 = (ProcessModule) enumerator.Current;
                    if (module2.BaseAddress.ToInt64() == long_0)
                    {
                        goto Label_1406;
                    }
                }
                goto Label_1425;
            Label_1406:
                num5 = 0;
            }
        }
        catch
        {
        }
    Label_1425:
        delegate45_0 = null;
        try
        {
            delegate45_0 = (Delegate45) smethod_15(new IntPtr(num4), typeof(Delegate45));
        }
        catch
        {
            try
            {
                Delegate delegate3 = smethod_15(new IntPtr(num4), typeof(Delegate45));
                delegate45_0 = (Delegate45) Delegate.CreateDelegate(typeof(Delegate45), delegate3.Method);
            }
            catch
            {
            }
        }
        int num16 = 0;
        if (((typeof(Class141).Assembly.EntryPoint == null) || (typeof(Class141).Assembly.EntryPoint.GetParameters().Length != 2)) || ((typeof(Class141).Assembly.Location == null) || (typeof(Class141).Assembly.Location.Length <= 0)))
        {
            try
            {
                ref byte pinned numRef2;
                object obj2 = typeof(Class141).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(typeof(Class141).Assembly.ManifestModule.ModuleHandle);
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
                byte[] buffer14 = stream3.ToArray();
                stream3.Close();
                uint nativeSizeOfCode = 0;
                try
                {
                    if (((source = buffer14) != null) && (source.Length != 0))
                    {
                        numRef2 = source;
                    }
                    else
                    {
                        numRef2 = null;
                    }
                    delegate45_1(new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), 0xcea1d7d, new IntPtr((void*) numRef2), ref nativeSizeOfCode);
                }
                finally
                {
                    numRef2 = null;
                }
            }
            catch
            {
            }
            RuntimeHelpers.PrepareDelegate(delegate45_0);
            RuntimeHelpers.PrepareMethod(delegate45_0.Method.MethodHandle);
            RuntimeHelpers.PrepareDelegate(delegate45_1);
            RuntimeHelpers.PrepareMethod(delegate45_1.Method.MethodHandle);
            byte[] buffer15 = null;
            if (IntPtr.Size != 4)
            {
                buffer15 = new byte[] { 
                    0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0x49, 0x39, 0x40, 8, 0x74, 12, 
                    0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0xff, 0xe0, 0x48, 0xb8, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0xff, 0xe0
                 };
            }
            else
            {
                buffer15 = new byte[] { 
                    0x55, 0x8b, 0xec, 0x8b, 0x45, 0x10, 0x81, 120, 4, 0x7d, 0x1d, 0xea, 12, 0x74, 7, 0xb8, 
                    0xb6, 0xb1, 0x4a, 6, 0xeb, 5, 0xb8, 0xb6, 0x92, 0x40, 12, 0x5d, 0xff, 0xe0
                 };
            }
            IntPtr destination = VirtualAlloc(IntPtr.Zero, (uint) buffer15.Length, 0x1000, 0x40);
            byte[] buffer4 = buffer15;
            byte[] buffer7 = null;
            byte[] buffer6 = null;
            byte[] buffer5 = null;
            if (IntPtr.Size == 4)
            {
                buffer5 = BitConverter.GetBytes(intptr_2.ToInt32());
                buffer7 = BitConverter.GetBytes(zero.ToInt32());
                buffer6 = BitConverter.GetBytes(Convert.ToInt32(num4));
            }
            else
            {
                buffer5 = BitConverter.GetBytes(intptr_2.ToInt64());
                buffer7 = BitConverter.GetBytes(zero.ToInt64());
                buffer6 = BitConverter.GetBytes(num4);
            }
            if (IntPtr.Size == 4)
            {
                buffer4[9] = buffer5[0];
                buffer4[10] = buffer5[1];
                buffer4[11] = buffer5[2];
                buffer4[12] = buffer5[3];
                buffer4[0x10] = buffer6[0];
                buffer4[0x11] = buffer6[1];
                buffer4[0x12] = buffer6[2];
                buffer4[0x13] = buffer6[3];
                buffer4[0x17] = buffer7[0];
                buffer4[0x18] = buffer7[1];
                buffer4[0x19] = buffer7[2];
                buffer4[0x1a] = buffer7[3];
            }
            else
            {
                buffer4[2] = buffer5[0];
                buffer4[3] = buffer5[1];
                buffer4[4] = buffer5[2];
                buffer4[5] = buffer5[3];
                buffer4[6] = buffer5[4];
                buffer4[7] = buffer5[5];
                buffer4[8] = buffer5[6];
                buffer4[9] = buffer5[7];
                buffer4[0x12] = buffer6[0];
                buffer4[0x13] = buffer6[1];
                buffer4[20] = buffer6[2];
                buffer4[0x15] = buffer6[3];
                buffer4[0x16] = buffer6[4];
                buffer4[0x17] = buffer6[5];
                buffer4[0x18] = buffer6[6];
                buffer4[0x19] = buffer6[7];
                buffer4[30] = buffer7[0];
                buffer4[0x1f] = buffer7[1];
                buffer4[0x20] = buffer7[2];
                buffer4[0x21] = buffer7[3];
                buffer4[0x22] = buffer7[4];
                buffer4[0x23] = buffer7[5];
                buffer4[0x24] = buffer7[6];
                buffer4[0x25] = buffer7[7];
            }
            Marshal.Copy(buffer4, 0, destination, buffer4.Length);
            bool_1 = false;
            VirtualProtect_1(new IntPtr(num2), IntPtr.Size, 0x40, ref num16);
            Marshal.WriteIntPtr(new IntPtr(num2), destination);
            VirtualProtect_1(new IntPtr(num2), IntPtr.Size, num16, ref num16);
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

    [Attribute9(typeof(Attribute9.Class142<object>[]))]
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

    [Attribute9(typeof(Attribute9.Class142<object>[]))]
    private static byte[] smethod_19(byte[] byte_4)
    {
        MemoryStream stream = new MemoryStream();
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Key = new byte[] { 
            0, 0x69, 0x63, 0x4e, 0xb5, 0xa4, 0x22, 220, 70, 0x63, 0x9f, 20, 0xaf, 0x1c, 0x37, 0xc7, 
            0xeb, 0x38, 0x3d, 0x38, 0x9e, 0x49, 14, 130, 0xf7, 190, 0x42, 0x53, 0x17, 0x18, 0x94, 0x90
         };
        algorithm.IV = new byte[] { 210, 0x88, 11, 0xee, 0x7f, 0x1b, 0xd5, 0xc5, 0x84, 0xe2, 0x7c, 0x2b, 0x81, 0xec, 0xbc, 0xbc };
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
        if (!bool_2)
        {
            smethod_8();
            bool_2 = true;
        }
        return bool_5;
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
            bool_5 = (bool) typeof(RijndaelManaged).Assembly.GetType("System.Security.Cryptography.CryptoConfig", false).GetMethod("get_AllowOnlyFipsAlgorithms", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[0]);
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

    internal class Attribute9 : Attribute
    {
        [Attribute9(typeof(Class142<object>[]))]
        public Attribute9(object object_0)
        {
            
        }

        internal class Class142<T>
        {
            public Class142()
            {
                
            }
        }
    }

    internal class Class143
    {
        public Class143()
        {
            
        }

        [Attribute9(typeof(Class141.Attribute9.Class142<object>[]))]
        internal static string smethod_0(string string_0, string string_1)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string_0);
            byte[] buffer3 = new byte[] { 
                0x52, 0x66, 0x68, 110, 0x20, 0x4d, 0x18, 0x22, 0x76, 0xb5, 0x33, 0x11, 0x12, 0x33, 12, 0x6d, 
                10, 0x20, 0x4d, 0x18, 0x22, 0x9e, 0xa1, 0x29, 0x61, 0x1c, 0x76, 0xb5, 5, 0x19, 1, 0x58
             };
            byte[] buffer4 = Class141.smethod_9(Encoding.Unicode.GetBytes(string_1));
            MemoryStream stream = new MemoryStream();
            SymmetricAlgorithm algorithm = Class141.smethod_7();
            algorithm.Key = buffer3;
            algorithm.IV = buffer4;
            CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate uint Delegate45(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate IntPtr Delegate46();

    [Flags]
    private enum Enum20
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct168
    {
        internal bool bool_0;
        internal byte[] byte_0;
    }
}

