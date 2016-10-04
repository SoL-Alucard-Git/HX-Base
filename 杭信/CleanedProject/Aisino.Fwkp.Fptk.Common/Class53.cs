using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal class Class53
{
    [Attribute4(typeof(Attribute4.Class54<object>[]))]
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
    internal static Delegate20 delegate20_0 = null;
    internal static Delegate20 delegate20_1 = null;
    internal static Hashtable hashtable_0 = new Hashtable();
    private static int int_0 = 0;
    private static int int_1 = 1;
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

    [Attribute4(typeof(Attribute4.Class54<object>[]))]
    /* private scope */ static bool smethod_10(int int_5)
    {
        if (byte_0.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class53).Assembly.GetManifestResourceStream("vjohbi\x008b\x0086\x0095\x008bo\x0086\x008cx\x0089\x0098e\x0089.3r4r8n\x008c\x0093sy\x009e\x0092i2ib35")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0xa8;
            buffer2[0] = 0x2b;
            buffer2[0] = 0x7c;
            buffer2[0] = 0x47;
            buffer2[1] = 0x62;
            buffer2[1] = 0x72;
            buffer2[1] = 0x4c;
            buffer2[2] = 0x7a;
            buffer2[2] = 0x3d;
            buffer2[2] = 0x9e;
            buffer2[2] = 0xa6;
            buffer2[2] = 0x62;
            buffer2[2] = 0xcc;
            buffer2[3] = 0x83;
            buffer2[3] = 0x6c;
            buffer2[3] = 0x8b;
            buffer2[3] = 0x97;
            buffer2[3] = 0x94;
            buffer2[3] = 0x88;
            buffer2[4] = 0xd3;
            buffer2[4] = 0x5d;
            buffer2[4] = 0x10;
            buffer2[5] = 0xa5;
            buffer2[5] = 0x6a;
            buffer2[5] = 0x70;
            buffer2[5] = 0x5c;
            buffer2[6] = 0x58;
            buffer2[6] = 0x84;
            buffer2[6] = 0x23;
            buffer2[6] = 160;
            buffer2[6] = 0x86;
            buffer2[7] = 0x59;
            buffer2[7] = 0x84;
            buffer2[7] = 0x8a;
            buffer2[7] = 0x66;
            buffer2[7] = 0x99;
            buffer2[7] = 0xbc;
            buffer2[8] = 0x71;
            buffer2[8] = 0x6c;
            buffer2[8] = 0x74;
            buffer2[8] = 0x2b;
            buffer2[9] = 0x68;
            buffer2[9] = 0x86;
            buffer2[9] = 0xc1;
            buffer2[10] = 0x70;
            buffer2[10] = 0xce;
            buffer2[10] = 0x9e;
            buffer2[10] = 0x8f;
            buffer2[10] = 0x8a;
            buffer2[10] = 0x79;
            buffer2[11] = 100;
            buffer2[11] = 0x4e;
            buffer2[11] = 0xe5;
            buffer2[11] = 0x38;
            buffer2[11] = 0x80;
            buffer2[12] = 150;
            buffer2[12] = 90;
            buffer2[12] = 0x87;
            buffer2[12] = 0x7b;
            buffer2[12] = 3;
            buffer2[13] = 0x84;
            buffer2[13] = 0x86;
            buffer2[13] = 0x98;
            buffer2[13] = 0x39;
            buffer2[14] = 0xa3;
            buffer2[14] = 0x11;
            buffer2[14] = 0x97;
            buffer2[14] = 0x9a;
            buffer2[14] = 0x91;
            buffer2[14] = 0xf8;
            buffer2[15] = 0x8e;
            buffer2[15] = 150;
            buffer2[15] = 0x58;
            buffer2[15] = 0x3e;
            buffer2[15] = 0x62;
            buffer2[15] = 0x90;
            buffer2[0x10] = 0xa4;
            buffer2[0x10] = 0x9d;
            buffer2[0x10] = 0x9d;
            buffer2[0x10] = 0x90;
            buffer2[0x10] = 220;
            buffer2[0x10] = 0xef;
            buffer2[0x11] = 0x3d;
            buffer2[0x11] = 0x61;
            buffer2[0x11] = 0xdb;
            buffer2[0x11] = 0x13;
            buffer2[0x11] = 0xa9;
            buffer2[0x11] = 0xf4;
            buffer2[0x12] = 0x83;
            buffer2[0x12] = 0x9d;
            buffer2[0x12] = 0xc1;
            buffer2[0x13] = 90;
            buffer2[0x13] = 0xa2;
            buffer2[0x13] = 0xd0;
            buffer2[20] = 160;
            buffer2[20] = 0x54;
            buffer2[20] = 0x57;
            buffer2[20] = 0x5c;
            buffer2[20] = 0x98;
            buffer2[0x15] = 0x70;
            buffer2[0x15] = 0x8d;
            buffer2[0x15] = 0x38;
            buffer2[0x15] = 8;
            buffer2[0x16] = 0x39;
            buffer2[0x16] = 0x7e;
            buffer2[0x16] = 0xab;
            buffer2[0x16] = 0xd5;
            buffer2[0x17] = 0x5c;
            buffer2[0x17] = 0x61;
            buffer2[0x17] = 0x55;
            buffer2[0x17] = 0x59;
            buffer2[0x17] = 0x56;
            buffer2[0x17] = 10;
            buffer2[0x18] = 0x72;
            buffer2[0x18] = 0x17;
            buffer2[0x18] = 0xa3;
            buffer2[0x18] = 0xb7;
            buffer2[0x18] = 0xe5;
            buffer2[0x19] = 0x92;
            buffer2[0x19] = 0x87;
            buffer2[0x19] = 0x6a;
            buffer2[0x19] = 0x48;
            buffer2[0x1a] = 0x88;
            buffer2[0x1a] = 170;
            buffer2[0x1a] = 0x7f;
            buffer2[0x1a] = 0x27;
            buffer2[0x1b] = 0x77;
            buffer2[0x1b] = 0x6f;
            buffer2[0x1b] = 0x7d;
            buffer2[0x1b] = 90;
            buffer2[0x1b] = 0xa9;
            buffer2[0x1b] = 0x93;
            buffer2[0x1c] = 0x75;
            buffer2[0x1c] = 0x4d;
            buffer2[0x1c] = 160;
            buffer2[0x1c] = 50;
            buffer2[0x1c] = 0xc5;
            buffer2[0x1d] = 0x61;
            buffer2[0x1d] = 0x87;
            buffer2[0x1d] = 0xa2;
            buffer2[30] = 0x5e;
            buffer2[30] = 0x98;
            buffer2[30] = 120;
            buffer2[30] = 0x1f;
            buffer2[30] = 0xb5;
            buffer2[0x1f] = 110;
            buffer2[0x1f] = 0x92;
            buffer2[0x1f] = 140;
            buffer2[0x1f] = 0x85;
            buffer2[0x1f] = 0x81;
            buffer2[0x1f] = 0xe7;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0xb1;
            buffer4[0] = 0x59;
            buffer4[0] = 0x90;
            buffer4[0] = 0x94;
            buffer4[0] = 0x39;
            buffer4[0] = 0xd6;
            buffer4[1] = 140;
            buffer4[1] = 0x59;
            buffer4[1] = 0x7e;
            buffer4[1] = 0x3d;
            buffer4[1] = 0xb2;
            buffer4[2] = 0xa4;
            buffer4[2] = 0x93;
            buffer4[2] = 0x6a;
            buffer4[2] = 0x8a;
            buffer4[2] = 0x71;
            buffer4[2] = 0xb0;
            buffer4[3] = 0x8b;
            buffer4[3] = 0x83;
            buffer4[3] = 0x7e;
            buffer4[3] = 0x87;
            buffer4[3] = 0x43;
            buffer4[3] = 200;
            buffer4[4] = 0xa6;
            buffer4[4] = 0x56;
            buffer4[4] = 0x84;
            buffer4[5] = 0xa3;
            buffer4[5] = 0x58;
            buffer4[5] = 0x84;
            buffer4[5] = 0x23;
            buffer4[5] = 0x9c;
            buffer4[5] = 0xfd;
            buffer4[6] = 0x84;
            buffer4[6] = 0x21;
            buffer4[6] = 90;
            buffer4[7] = 0x60;
            buffer4[7] = 0x6a;
            buffer4[7] = 0xa8;
            buffer4[7] = 0x4a;
            buffer4[8] = 110;
            buffer4[8] = 0x7d;
            buffer4[8] = 0x6c;
            buffer4[8] = 0x74;
            buffer4[8] = 90;
            buffer4[8] = 1;
            buffer4[9] = 0xb8;
            buffer4[9] = 0x70;
            buffer4[9] = 0xce;
            buffer4[9] = 0x87;
            buffer4[9] = 0x79;
            buffer4[10] = 70;
            buffer4[10] = 0x95;
            buffer4[10] = 160;
            buffer4[10] = 0x4d;
            buffer4[11] = 0x6a;
            buffer4[11] = 0xa4;
            buffer4[11] = 0x66;
            buffer4[11] = 0xee;
            buffer4[12] = 0x85;
            buffer4[12] = 150;
            buffer4[12] = 90;
            buffer4[12] = 0x87;
            buffer4[12] = 0x33;
            buffer4[13] = 0x69;
            buffer4[13] = 0xad;
            buffer4[13] = 0x4f;
            buffer4[13] = 0xb7;
            buffer4[14] = 0x92;
            buffer4[14] = 0x74;
            buffer4[14] = 130;
            buffer4[14] = 100;
            buffer4[14] = 0xd6;
            buffer4[15] = 130;
            buffer4[15] = 0x8e;
            buffer4[15] = 0xa7;
            buffer4[15] = 0x92;
            buffer4[15] = 0x67;
            buffer4[15] = 0x8b;
            byte[] rgbIV = buffer4;
            byte[] publicKeyToken = typeof(Class53).Assembly.GetName().GetPublicKeyToken();
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
        if (byte_2.Length == 0)
        {
            byte_2 = smethod_18(smethod_17(typeof(Class53).Assembly).ToString());
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

    [Attribute4(typeof(Attribute4.Class54<object>[]))]
    /* private scope */ static string smethod_11(int int_5)
    {
        if (byte_3.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class53).Assembly.GetManifestResourceStream("uvc\x0090g\x0097\x0087\x0094u\x0096p\x0091\x008a\x0092vhb\x0088.\x009ft6\x008b\x009a\x009f\x00885k\x008a\x0093\x008dfp\x009cjp\x009a")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer3 = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer4 = new byte[0x20];
            buffer4[0] = 0x56;
            buffer4[0] = 0x74;
            buffer4[0] = 0xa4;
            buffer4[0] = 160;
            buffer4[0] = 0xd1;
            buffer4[0] = 0xb7;
            buffer4[1] = 0xca;
            buffer4[1] = 0xd3;
            buffer4[1] = 0x58;
            buffer4[1] = 0xec;
            buffer4[2] = 0x59;
            buffer4[2] = 0x97;
            buffer4[2] = 0x92;
            buffer4[2] = 0x9d;
            buffer4[2] = 0xb0;
            buffer4[3] = 0x98;
            buffer4[3] = 0x37;
            buffer4[3] = 0xf6;
            buffer4[4] = 0x98;
            buffer4[4] = 0x88;
            buffer4[4] = 0x5f;
            buffer4[4] = 150;
            buffer4[4] = 0x5e;
            buffer4[5] = 190;
            buffer4[5] = 0x7d;
            buffer4[5] = 0x62;
            buffer4[5] = 0xa4;
            buffer4[5] = 0x1c;
            buffer4[6] = 0x5f;
            buffer4[6] = 0x6c;
            buffer4[6] = 0x74;
            buffer4[6] = 0x9c;
            buffer4[6] = 0x7d;
            buffer4[7] = 0xcf;
            buffer4[7] = 0x58;
            buffer4[7] = 0x8d;
            buffer4[7] = 0x94;
            buffer4[7] = 12;
            buffer4[8] = 0x8e;
            buffer4[8] = 0x8e;
            buffer4[8] = 0x2a;
            buffer4[9] = 140;
            buffer4[9] = 0x59;
            buffer4[9] = 0x98;
            buffer4[9] = 0x7e;
            buffer4[9] = 0x9e;
            buffer4[9] = 0x2b;
            buffer4[10] = 0x9c;
            buffer4[10] = 0xa9;
            buffer4[10] = 100;
            buffer4[10] = 0x1c;
            buffer4[11] = 0xa6;
            buffer4[11] = 0x91;
            buffer4[11] = 0xb2;
            buffer4[12] = 0x74;
            buffer4[12] = 170;
            buffer4[12] = 0x2e;
            buffer4[12] = 0x25;
            buffer4[13] = 0x94;
            buffer4[13] = 0x88;
            buffer4[13] = 0x98;
            buffer4[13] = 0xaf;
            buffer4[13] = 0x5e;
            buffer4[13] = 0x53;
            buffer4[14] = 0xa3;
            buffer4[14] = 0xa1;
            buffer4[14] = 120;
            buffer4[14] = 0x56;
            buffer4[14] = 0x62;
            buffer4[14] = 0x7a;
            buffer4[15] = 0x92;
            buffer4[15] = 0x72;
            buffer4[15] = 0x5b;
            buffer4[15] = 10;
            buffer4[0x10] = 0x9a;
            buffer4[0x10] = 100;
            buffer4[0x10] = 0x8e;
            buffer4[0x10] = 0x95;
            buffer4[0x10] = 170;
            buffer4[0x11] = 0x7e;
            buffer4[0x11] = 0xda;
            buffer4[0x11] = 0x90;
            buffer4[0x11] = 170;
            buffer4[0x12] = 110;
            buffer4[0x12] = 0x89;
            buffer4[0x12] = 0x53;
            buffer4[0x13] = 0xd8;
            buffer4[0x13] = 0x7c;
            buffer4[0x13] = 0x7d;
            buffer4[0x13] = 0xb3;
            buffer4[20] = 0x8d;
            buffer4[20] = 0xa5;
            buffer4[20] = 0x5e;
            buffer4[20] = 0x66;
            buffer4[20] = 0x1f;
            buffer4[0x15] = 0x9c;
            buffer4[0x15] = 0x24;
            buffer4[0x15] = 0x79;
            buffer4[0x15] = 210;
            buffer4[0x15] = 0x5b;
            buffer4[0x16] = 0x6d;
            buffer4[0x16] = 0x57;
            buffer4[0x16] = 0x76;
            buffer4[0x17] = 0x23;
            buffer4[0x17] = 0x57;
            buffer4[0x17] = 0;
            buffer4[0x18] = 0x66;
            buffer4[0x18] = 0x8f;
            buffer4[0x18] = 0x8a;
            buffer4[0x18] = 120;
            buffer4[0x18] = 0xac;
            buffer4[0x19] = 0x5e;
            buffer4[0x19] = 0x65;
            buffer4[0x19] = 0x21;
            buffer4[0x1a] = 200;
            buffer4[0x1a] = 0x9b;
            buffer4[0x1a] = 190;
            buffer4[0x1b] = 0x80;
            buffer4[0x1b] = 0x94;
            buffer4[0x1b] = 0x85;
            buffer4[0x1b] = 0x21;
            buffer4[0x1b] = 0x4f;
            buffer4[0x1b] = 4;
            buffer4[0x1c] = 0x8a;
            buffer4[0x1c] = 0x94;
            buffer4[0x1c] = 0x31;
            buffer4[0x1c] = 150;
            buffer4[0x1c] = 0x22;
            buffer4[0x1d] = 0x56;
            buffer4[0x1d] = 0x92;
            buffer4[0x1d] = 0xe2;
            buffer4[0x1d] = 0x5e;
            buffer4[0x1d] = 0x92;
            buffer4[0x1d] = 0x4e;
            buffer4[30] = 0x71;
            buffer4[30] = 0xc9;
            buffer4[30] = 0x3d;
            buffer4[0x1f] = 0x62;
            buffer4[0x1f] = 0xb2;
            buffer4[0x1f] = 0x80;
            buffer4[0x1f] = 0x8e;
            buffer4[0x1f] = 0x5f;
            buffer4[0x1f] = 0x8b;
            byte[] rgbKey = buffer4;
            byte[] buffer6 = new byte[0x10];
            buffer6[0] = 0x72;
            buffer6[0] = 0x7d;
            buffer6[0] = 0x68;
            buffer6[0] = 0x8b;
            buffer6[0] = 0x9c;
            buffer6[0] = 120;
            buffer6[1] = 0x67;
            buffer6[1] = 0x87;
            buffer6[1] = 170;
            buffer6[1] = 0x91;
            buffer6[1] = 0xa2;
            buffer6[1] = 0xd5;
            buffer6[2] = 0x89;
            buffer6[2] = 0x5c;
            buffer6[2] = 0x8b;
            buffer6[2] = 0x90;
            buffer6[3] = 0x79;
            buffer6[3] = 0x62;
            buffer6[3] = 0x67;
            buffer6[3] = 0xf5;
            buffer6[4] = 0x6c;
            buffer6[4] = 0x9a;
            buffer6[4] = 0xb3;
            buffer6[4] = 0x69;
            buffer6[4] = 13;
            buffer6[5] = 0x6c;
            buffer6[5] = 0x6a;
            buffer6[5] = 0x79;
            buffer6[5] = 0x66;
            buffer6[5] = 0x8b;
            buffer6[5] = 0x42;
            buffer6[6] = 0xa8;
            buffer6[6] = 0x34;
            buffer6[6] = 220;
            buffer6[6] = 140;
            buffer6[6] = 0x7c;
            buffer6[7] = 0x93;
            buffer6[7] = 0x75;
            buffer6[7] = 0x71;
            buffer6[7] = 0x36;
            buffer6[8] = 0x5b;
            buffer6[8] = 0x91;
            buffer6[8] = 0xc0;
            buffer6[9] = 0xb8;
            buffer6[9] = 0xb3;
            buffer6[9] = 0x52;
            buffer6[9] = 0x9c;
            buffer6[9] = 0xb9;
            buffer6[10] = 0x84;
            buffer6[10] = 0x5c;
            buffer6[10] = 0xa7;
            buffer6[10] = 0x8a;
            buffer6[10] = 0x7b;
            buffer6[10] = 0x43;
            buffer6[11] = 0x65;
            buffer6[11] = 0x56;
            buffer6[11] = 60;
            buffer6[12] = 0x94;
            buffer6[12] = 0x91;
            buffer6[12] = 0x3e;
            buffer6[13] = 120;
            buffer6[13] = 0xab;
            buffer6[13] = 0x6b;
            buffer6[13] = 210;
            buffer6[14] = 0x70;
            buffer6[14] = 0xcc;
            buffer6[14] = 0x43;
            buffer6[14] = 0x41;
            buffer6[14] = 0xdf;
            buffer6[15] = 0x5e;
            buffer6[15] = 0x59;
            buffer6[15] = 0x7e;
            buffer6[15] = 110;
            buffer6[15] = 0x17;
            byte[] array = buffer6;
            Array.Reverse(array);
            byte[] publicKeyToken = typeof(Class53).Assembly.GetName().GetPublicKeyToken();
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
            stream2.Write(buffer3, 0, buffer3.Length);
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

    [Attribute4(typeof(Attribute4.Class54<object>[]))]
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
            Struct61 struct2 = (Struct61) obj2;
            IntPtr destination = Marshal.AllocCoTaskMem(struct2.byte_0.Length);
            Marshal.Copy(struct2.byte_0, 0, destination, struct2.byte_0.Length);
            if (struct2.bool_0)
            {
                intptr_6 = destination;
                uint_2 = (uint) struct2.byte_0.Length;
                VirtualProtect_1(intptr_6, struct2.byte_0.Length, 0x40, ref int_2);
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
        return delegate20_0(intptr_3, intptr_4, intptr_5, uint_1, intptr_6, ref uint_2);
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
        if (bool_1)
        {
            return;
        }
        bool_1 = true;
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
                bool_3 = true;
            }
        }
    Label_01C3:
        reader = new BinaryReader(typeof(Class53).Assembly.GetManifestResourceStream("\x0099\x0086\x0088\x009b5\x008c\x008d\x008dnxc\x008f\x0091\x008axrwg.g\x0098\x0092\x0097bk\x008e\x0095\x009f\x0094\x009e\x0093l\x0089\x009b\x008dl\x0086")) {
            BaseStream = { Position = 0L }
        };
        byte[] buffer9 = reader.ReadBytes((int) reader.BaseStream.Length);
        byte[] buffer10 = new byte[0x20];
        buffer10[0] = 0x66;
        buffer10[0] = 0x49;
        buffer10[0] = 0x97;
        buffer10[0] = 0x59;
        buffer10[0] = 0x89;
        buffer10[0] = 0xa6;
        buffer10[1] = 0x7b;
        buffer10[1] = 0x95;
        buffer10[1] = 0x7f;
        buffer10[2] = 0x33;
        buffer10[2] = 0x5c;
        buffer10[2] = 0xb6;
        buffer10[2] = 0x33;
        buffer10[2] = 0x79;
        buffer10[2] = 0x26;
        buffer10[3] = 0xb2;
        buffer10[3] = 60;
        buffer10[3] = 0x20;
        buffer10[4] = 0x26;
        buffer10[4] = 0x59;
        buffer10[4] = 0x56;
        buffer10[5] = 0x7e;
        buffer10[5] = 0xeb;
        buffer10[5] = 0x92;
        buffer10[5] = 170;
        buffer10[5] = 0x8b;
        buffer10[5] = 0x39;
        buffer10[6] = 0x98;
        buffer10[6] = 0x90;
        buffer10[6] = 0x27;
        buffer10[7] = 0x7c;
        buffer10[7] = 120;
        buffer10[7] = 0xa4;
        buffer10[7] = 0x1b;
        buffer10[8] = 0xab;
        buffer10[8] = 0x5b;
        buffer10[8] = 0x7a;
        buffer10[8] = 0x7a;
        buffer10[8] = 0x7a;
        buffer10[9] = 0xc6;
        buffer10[9] = 0x74;
        buffer10[9] = 0xa3;
        buffer10[9] = 110;
        buffer10[10] = 0x86;
        buffer10[10] = 0xb6;
        buffer10[10] = 0x25;
        buffer10[10] = 0x58;
        buffer10[10] = 0x83;
        buffer10[11] = 0x71;
        buffer10[11] = 0x26;
        buffer10[11] = 0xf6;
        buffer10[12] = 0x36;
        buffer10[12] = 0xa8;
        buffer10[12] = 0x94;
        buffer10[12] = 0x84;
        buffer10[12] = 0x61;
        buffer10[13] = 110;
        buffer10[13] = 0x88;
        buffer10[13] = 0x68;
        buffer10[13] = 0xe7;
        buffer10[14] = 0x61;
        buffer10[14] = 0x88;
        buffer10[14] = 0x89;
        buffer10[15] = 0x5c;
        buffer10[15] = 0x73;
        buffer10[15] = 0x69;
        buffer10[15] = 0x58;
        buffer10[15] = 0x51;
        buffer10[15] = 0xef;
        buffer10[0x10] = 0x6d;
        buffer10[0x10] = 100;
        buffer10[0x10] = 0x74;
        buffer10[0x10] = 0x7e;
        buffer10[0x10] = 0x92;
        buffer10[0x11] = 0x4a;
        buffer10[0x11] = 0x70;
        buffer10[0x11] = 0x6b;
        buffer10[0x11] = 0x13;
        buffer10[0x12] = 0x8f;
        buffer10[0x12] = 0x94;
        buffer10[0x12] = 0x58;
        buffer10[0x12] = 0x97;
        buffer10[0x12] = 0xe3;
        buffer10[0x13] = 0x94;
        buffer10[0x13] = 0x95;
        buffer10[0x13] = 0x2f;
        buffer10[0x13] = 0xb2;
        buffer10[0x13] = 0x6d;
        buffer10[0x13] = 0xd6;
        buffer10[20] = 90;
        buffer10[20] = 110;
        buffer10[20] = 0xca;
        buffer10[0x15] = 150;
        buffer10[0x15] = 0x8a;
        buffer10[0x15] = 0xcb;
        buffer10[0x15] = 0xb7;
        buffer10[0x16] = 0x68;
        buffer10[0x16] = 0x87;
        buffer10[0x16] = 0x37;
        buffer10[0x16] = 0xef;
        buffer10[0x17] = 0xcc;
        buffer10[0x17] = 0xa2;
        buffer10[0x17] = 0x3e;
        buffer10[0x17] = 0x9a;
        buffer10[0x17] = 0x68;
        buffer10[0x17] = 0x57;
        buffer10[0x18] = 0x74;
        buffer10[0x18] = 0xa7;
        buffer10[0x18] = 0x54;
        buffer10[0x18] = 0x90;
        buffer10[0x18] = 0x74;
        buffer10[0x18] = 0x3d;
        buffer10[0x19] = 0xb5;
        buffer10[0x19] = 0x26;
        buffer10[0x19] = 0x6a;
        buffer10[0x19] = 0x7a;
        buffer10[0x1a] = 120;
        buffer10[0x1a] = 0x88;
        buffer10[0x1a] = 0x2d;
        buffer10[0x1b] = 0x84;
        buffer10[0x1b] = 140;
        buffer10[0x1b] = 0x62;
        buffer10[0x1b] = 0x9c;
        buffer10[0x1c] = 0x6b;
        buffer10[0x1c] = 0xa2;
        buffer10[0x1c] = 0x2f;
        buffer10[0x1d] = 0xb5;
        buffer10[0x1d] = 0x43;
        buffer10[0x1d] = 130;
        buffer10[0x1d] = 0x5f;
        buffer10[0x1d] = 0x91;
        buffer10[0x1d] = 0xda;
        buffer10[30] = 0xb5;
        buffer10[30] = 0x68;
        buffer10[30] = 0x92;
        buffer10[30] = 0x98;
        buffer10[30] = 0x20;
        buffer10[0x1f] = 0x7e;
        buffer10[0x1f] = 0x99;
        buffer10[0x1f] = 0x5d;
        byte[] rgbKey = buffer10;
        byte[] buffer12 = new byte[0x10];
        buffer12[0] = 0x68;
        buffer12[0] = 0xcd;
        buffer12[0] = 0x7b;
        buffer12[0] = 0x37;
        buffer12[0] = 0x68;
        buffer12[0] = 0x67;
        buffer12[1] = 0x84;
        buffer12[1] = 0x6a;
        buffer12[1] = 0xaf;
        buffer12[1] = 0xc2;
        buffer12[2] = 0x8e;
        buffer12[2] = 0x9e;
        buffer12[2] = 0x8a;
        buffer12[3] = 0x72;
        buffer12[3] = 0x7f;
        buffer12[3] = 0x60;
        buffer12[3] = 0x6c;
        buffer12[4] = 0x3f;
        buffer12[4] = 0xa6;
        buffer12[4] = 0xaf;
        buffer12[4] = 0xb6;
        buffer12[5] = 0x9b;
        buffer12[5] = 130;
        buffer12[5] = 180;
        buffer12[5] = 130;
        buffer12[5] = 0x62;
        buffer12[6] = 0x6d;
        buffer12[6] = 0x59;
        buffer12[6] = 0x55;
        buffer12[6] = 0x4c;
        buffer12[7] = 0x7d;
        buffer12[7] = 0xbb;
        buffer12[7] = 0x8f;
        buffer12[7] = 0x76;
        buffer12[7] = 0x3e;
        buffer12[7] = 0x9f;
        buffer12[8] = 0xd6;
        buffer12[8] = 0x5d;
        buffer12[8] = 0x6c;
        buffer12[9] = 0x85;
        buffer12[9] = 0x57;
        buffer12[9] = 0x86;
        buffer12[9] = 0xe3;
        buffer12[10] = 0x70;
        buffer12[10] = 0x8b;
        buffer12[10] = 0x72;
        buffer12[10] = 170;
        buffer12[10] = 0x8f;
        buffer12[10] = 50;
        buffer12[11] = 0x2a;
        buffer12[11] = 0x68;
        buffer12[11] = 0x1b;
        buffer12[12] = 0x92;
        buffer12[12] = 80;
        buffer12[12] = 0x60;
        buffer12[13] = 0x5c;
        buffer12[13] = 0x86;
        buffer12[13] = 0x5f;
        buffer12[13] = 0x94;
        buffer12[13] = 0x9f;
        buffer12[14] = 0x44;
        buffer12[14] = 0x22;
        buffer12[14] = 0x9a;
        buffer12[15] = 0xa8;
        buffer12[15] = 0x98;
        buffer12[15] = 13;
        byte[] array = buffer12;
        Array.Reverse(array);
        byte[] publicKeyToken = typeof(Class53).Assembly.GetName().GetPublicKeyToken();
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
        MemoryStream stream2 = new MemoryStream();
        CryptoStream stream3 = new CryptoStream(stream2, transform, CryptoStreamMode.Write);
        stream3.Write(buffer9, 0, buffer9.Length);
        stream3.FlushFinalBlock();
        byte[] buffer15 = stream2.ToArray();
        Array.Clear(array, 0, array.Length);
        stream2.Close();
        stream3.Close();
        reader.Close();
        int num18 = buffer15.Length / 8;
        if (((source = buffer15) != null) && (source.Length != 0))
        {
            numRef = source;
            goto Label_0C70;
        }
        fixed (byte* numRef = null)
        {
            int num3;
        Label_0C70:
            num3 = 0;
            while (num3 < num18)
            {
                IntPtr ptr1 = (IntPtr) (numRef + (num3 * 8));
                ptr1[0] ^= (IntPtr) 0x7c337b27L;
                num3++;
            }
        }
        reader = new BinaryReader(new MemoryStream(buffer15)) {
            BaseStream = { Position = 0L }
        };
        long num10 = Marshal.GetHINSTANCE(typeof(Class53).Assembly.GetModules()[0]).ToInt64();
        int num16 = 0;
        int num6 = 0;
        if ((typeof(Class53).Assembly.Location == null) || (typeof(Class53).Assembly.Location.Length == 0))
        {
            num6 = 0x1c00;
        }
        int num5 = reader.ReadInt32();
        if (reader.ReadInt32() == 1)
        {
            IntPtr ptr3 = IntPtr.Zero;
            Assembly assembly = typeof(Class53).Assembly;
            ptr3 = OpenProcess(0x38, 1, (uint) Process.GetCurrentProcess().Id);
            if (IntPtr.Size == 4)
            {
                int_3 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt32();
            }
            long_0 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt64();
            IntPtr ptr2 = IntPtr.Zero;
            for (int j = 0; j < num5; j++)
            {
                IntPtr ptr4 = new IntPtr((long_0 + reader.ReadInt32()) - num6);
                VirtualProtect_1(ptr4, 4, 4, ref num16);
                if (IntPtr.Size == 4)
                {
                    WriteProcessMemory(ptr3, ptr4, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr2);
                }
                else
                {
                    WriteProcessMemory(ptr3, ptr4, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr2);
                }
                VirtualProtect_1(ptr4, 4, num16, ref num16);
            }
            while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
            {
                int num14 = reader.ReadInt32();
                IntPtr ptr8 = new IntPtr(long_0 + num14);
                int num15 = reader.ReadInt32();
                VirtualProtect_1(ptr8, num15 * 4, 4, ref num16);
                for (int k = 0; k < num15; k++)
                {
                    Marshal.WriteInt32(new IntPtr(ptr8.ToInt64() + (k * 4)), reader.ReadInt32());
                }
                VirtualProtect_1(ptr8, num15 * 4, num16, ref num16);
            }
            CloseHandle(ptr3);
            return;
        }
        for (int i = 0; i < num5; i++)
        {
            IntPtr ptr9 = new IntPtr((num10 + reader.ReadInt32()) - num6);
            VirtualProtect_1(ptr9, 4, 4, ref num16);
            Marshal.WriteInt32(ptr9, reader.ReadInt32());
            VirtualProtect_1(ptr9, 4, num16, ref num16);
        }
        hashtable_0 = new Hashtable(reader.ReadInt32() + 1);
        Struct61 struct3 = new Struct61 {
            byte_0 = new byte[] { 0x2a },
            bool_0 = false
        };
        hashtable_0.Add(0L, struct3);
        bool flag = false;
        while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
        {
            int num7 = reader.ReadInt32() - num6;
            int num8 = reader.ReadInt32();
            flag = false;
            if (num8 >= 0x70000000)
            {
                flag = true;
            }
            int count = reader.ReadInt32();
            byte[] buffer7 = reader.ReadBytes(count);
            Struct61 struct2 = new Struct61 {
                byte_0 = buffer7,
                bool_0 = flag
            };
            hashtable_0.Add(num10 + num7, struct2);
        }
        long_1 = Marshal.GetHINSTANCE(typeof(Class53).Assembly.GetModules()[0]).ToInt64();
        if (IntPtr.Size == 4)
        {
            int_0 = Convert.ToInt32(long_1);
        }
        byte[] bytes = new byte[] { 0x6d, 0x73, 0x63, 0x6f, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
        string str = Encoding.UTF8.GetString(bytes);
        IntPtr ptr10 = LoadLibrary(str);
        if (ptr10 == IntPtr.Zero)
        {
            bytes = new byte[] { 0x63, 0x6c, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
            str = Encoding.UTF8.GetString(bytes);
            ptr10 = LoadLibrary(str);
        }
        byte[] buffer17 = new byte[] { 0x67, 0x65, 0x74, 0x4a, 0x69, 0x74 };
        string str2 = Encoding.UTF8.GetString(buffer17);
        Delegate21 delegate3 = (Delegate21) smethod_15(GetProcAddress(ptr10, str2), typeof(Delegate21));
        IntPtr ptr = delegate3();
        long num = 0L;
        if (IntPtr.Size == 4)
        {
            num = Marshal.ReadInt32(ptr);
        }
        else
        {
            num = Marshal.ReadInt64(ptr);
        }
        Marshal.ReadIntPtr(ptr, 0);
        delegate20_1 = new Delegate20(Class53.smethod_13);
        IntPtr zero = IntPtr.Zero;
        zero = Marshal.GetFunctionPointerForDelegate(delegate20_1);
        long num2 = 0L;
        if (IntPtr.Size == 4)
        {
            num2 = Marshal.ReadInt32(new IntPtr(num));
        }
        else
        {
            num2 = Marshal.ReadInt64(new IntPtr(num));
        }
        Process currentProcess = Process.GetCurrentProcess();
        try
        {
            foreach (ProcessModule module in currentProcess.Modules)
            {
                if (((module.ModuleName == str) && ((num2 < module.BaseAddress.ToInt64()) || (num2 > (module.BaseAddress.ToInt64() + module.ModuleMemorySize)))) && (typeof(Class53).Assembly.EntryPoint != null))
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
                    if (module2.BaseAddress.ToInt64() == long_1)
                    {
                        goto Label_12FE;
                    }
                }
                goto Label_131D;
            Label_12FE:
                num6 = 0;
            }
        }
        catch
        {
        }
    Label_131D:
        delegate20_0 = null;
        try
        {
            delegate20_0 = (Delegate20) smethod_15(new IntPtr(num2), typeof(Delegate20));
        }
        catch
        {
            try
            {
                Delegate delegate2 = smethod_15(new IntPtr(num2), typeof(Delegate20));
                delegate20_0 = (Delegate20) Delegate.CreateDelegate(typeof(Delegate20), delegate2.Method);
            }
            catch
            {
            }
        }
        int num17 = 0;
        if (((typeof(Class53).Assembly.EntryPoint == null) || (typeof(Class53).Assembly.EntryPoint.GetParameters().Length != 2)) || ((typeof(Class53).Assembly.Location == null) || (typeof(Class53).Assembly.Location.Length <= 0)))
        {
            try
            {
                ref byte pinned numRef2;
                object obj2 = typeof(Class53).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(typeof(Class53).Assembly.ManifestModule.ModuleHandle);
                if (obj2 is IntPtr)
                {
                    intptr_0 = (IntPtr) obj2;
                }
                if (obj2.GetType().ToString() == "System.Reflection.RuntimeModule")
                {
                    intptr_0 = (IntPtr) obj2.GetType().GetField("m_pData", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(obj2);
                }
                MemoryStream stream = new MemoryStream();
                stream.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                if (IntPtr.Size == 4)
                {
                    stream.Write(BitConverter.GetBytes(intptr_0.ToInt32()), 0, 4);
                }
                else
                {
                    stream.Write(BitConverter.GetBytes(intptr_0.ToInt64()), 0, 8);
                }
                stream.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                stream.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                stream.Position = 0L;
                byte[] buffer8 = stream.ToArray();
                stream.Close();
                uint nativeSizeOfCode = 0;
                try
                {
                    if (((source = buffer8) != null) && (source.Length != 0))
                    {
                        numRef2 = source;
                    }
                    else
                    {
                        numRef2 = null;
                    }
                    delegate20_1(new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), 0xcea1d7d, new IntPtr((void*) numRef2), ref nativeSizeOfCode);
                }
                finally
                {
                    numRef2 = null;
                }
            }
            catch
            {
            }
            RuntimeHelpers.PrepareDelegate(delegate20_0);
            RuntimeHelpers.PrepareMethod(delegate20_0.Method.MethodHandle);
            RuntimeHelpers.PrepareDelegate(delegate20_1);
            RuntimeHelpers.PrepareMethod(delegate20_1.Method.MethodHandle);
            byte[] buffer = null;
            if (IntPtr.Size != 4)
            {
                buffer = new byte[] { 
                    0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0x49, 0x39, 0x40, 8, 0x74, 12, 
                    0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0xff, 0xe0, 0x48, 0xb8, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0xff, 0xe0
                 };
            }
            else
            {
                buffer = new byte[] { 
                    0x55, 0x8b, 0xec, 0x8b, 0x45, 0x10, 0x81, 120, 4, 0x7d, 0x1d, 0xea, 12, 0x74, 7, 0xb8, 
                    0xb6, 0xb1, 0x4a, 6, 0xeb, 5, 0xb8, 0xb6, 0x92, 0x40, 12, 0x5d, 0xff, 0xe0
                 };
            }
            IntPtr destination = VirtualAlloc(IntPtr.Zero, (uint) buffer.Length, 0x1000, 0x40);
            byte[] buffer5 = buffer;
            byte[] buffer3 = null;
            byte[] buffer4 = null;
            byte[] buffer2 = null;
            if (IntPtr.Size == 4)
            {
                buffer2 = BitConverter.GetBytes(intptr_0.ToInt32());
                buffer3 = BitConverter.GetBytes(zero.ToInt32());
                buffer4 = BitConverter.GetBytes(Convert.ToInt32(num2));
            }
            else
            {
                buffer2 = BitConverter.GetBytes(intptr_0.ToInt64());
                buffer3 = BitConverter.GetBytes(zero.ToInt64());
                buffer4 = BitConverter.GetBytes(num2);
            }
            if (IntPtr.Size == 4)
            {
                buffer5[9] = buffer2[0];
                buffer5[10] = buffer2[1];
                buffer5[11] = buffer2[2];
                buffer5[12] = buffer2[3];
                buffer5[0x10] = buffer4[0];
                buffer5[0x11] = buffer4[1];
                buffer5[0x12] = buffer4[2];
                buffer5[0x13] = buffer4[3];
                buffer5[0x17] = buffer3[0];
                buffer5[0x18] = buffer3[1];
                buffer5[0x19] = buffer3[2];
                buffer5[0x1a] = buffer3[3];
            }
            else
            {
                buffer5[2] = buffer2[0];
                buffer5[3] = buffer2[1];
                buffer5[4] = buffer2[2];
                buffer5[5] = buffer2[3];
                buffer5[6] = buffer2[4];
                buffer5[7] = buffer2[5];
                buffer5[8] = buffer2[6];
                buffer5[9] = buffer2[7];
                buffer5[0x12] = buffer4[0];
                buffer5[0x13] = buffer4[1];
                buffer5[20] = buffer4[2];
                buffer5[0x15] = buffer4[3];
                buffer5[0x16] = buffer4[4];
                buffer5[0x17] = buffer4[5];
                buffer5[0x18] = buffer4[6];
                buffer5[0x19] = buffer4[7];
                buffer5[30] = buffer3[0];
                buffer5[0x1f] = buffer3[1];
                buffer5[0x20] = buffer3[2];
                buffer5[0x21] = buffer3[3];
                buffer5[0x22] = buffer3[4];
                buffer5[0x23] = buffer3[5];
                buffer5[0x24] = buffer3[6];
                buffer5[0x25] = buffer3[7];
            }
            Marshal.Copy(buffer5, 0, destination, buffer5.Length);
            bool_4 = false;
            VirtualProtect_1(new IntPtr(num), IntPtr.Size, 0x40, ref num17);
            Marshal.WriteIntPtr(new IntPtr(num), destination);
            VirtualProtect_1(new IntPtr(num), IntPtr.Size, num17, ref num17);
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

    [Attribute4(typeof(Attribute4.Class54<object>[]))]
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

    [Attribute4(typeof(Attribute4.Class54<object>[]))]
    private static byte[] smethod_19(byte[] byte_4)
    {
        MemoryStream stream = new MemoryStream();
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Key = new byte[] { 
            11, 0x84, 0x18, 0x62, 90, 0xbf, 0x2f, 0x6b, 0x20, 0x8f, 0x16, 0xb7, 0x52, 0x75, 0xd1, 0x8a, 
            0xb3, 0xf3, 0x9d, 0xf2, 0x42, 0x91, 0x8e, 11, 6, 0x2a, 0x22, 0xe1, 0xa6, 0x1c, 0x66, 3
         };
        algorithm.IV = new byte[] { 70, 0xb2, 0x2f, 0x6a, 0x1b, 0xcc, 0xa8, 0xc5, 0x13, 0x91, 170, 0x1f, 0xd7, 0x25, 12, 210 };
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

    internal class Attribute4 : Attribute
    {
        [Attribute4(typeof(Class54<object>[]))]
        public Attribute4(object object_0)
        {
            Class56.smethod_0();
        }

        internal class Class54<T>
        {
            public Class54()
            {
                Class56.smethod_0();
            }
        }
    }

    internal class Class55
    {
        public Class55()
        {
            Class56.smethod_0();
        }

        [Attribute4(typeof(Class53.Attribute4.Class54<object>[]))]
        internal static string smethod_0(string string_0, string string_1)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string_0);
            byte[] buffer3 = new byte[] { 
                0x52, 0x66, 0x68, 110, 0x20, 0x4d, 0x18, 0x22, 0x76, 0xb5, 0x33, 0x11, 0x12, 0x33, 12, 0x6d, 
                10, 0x20, 0x4d, 0x18, 0x22, 0x9e, 0xa1, 0x29, 0x61, 0x1c, 0x76, 0xb5, 5, 0x19, 1, 0x58
             };
            byte[] buffer4 = Class53.smethod_9(Encoding.Unicode.GetBytes(string_1));
            MemoryStream stream = new MemoryStream();
            SymmetricAlgorithm algorithm = Class53.smethod_7();
            algorithm.Key = buffer3;
            algorithm.IV = buffer4;
            CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate uint Delegate20(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate IntPtr Delegate21();

    [Flags]
    private enum Enum4
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct61
    {
        internal bool bool_0;
        internal byte[] byte_0;
    }
}

