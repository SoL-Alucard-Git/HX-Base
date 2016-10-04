using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal class Class60
{
    private static bool bool_0 = false;
    private static bool bool_1 = false;
    private static bool bool_2 = false;
    [Attribute5(typeof(Attribute5.Class61<object>[]))]
    private static bool bool_3 = false;
    private static bool bool_4 = false;
    private static bool bool_5 = false;
    private static bool bool_6 = false;
    private static byte[] byte_0 = new byte[0];
    private static byte[] byte_1 = new byte[0];
    private static byte[] byte_2 = new byte[0];
    private static byte[] byte_3 = new byte[0];
    internal static Delegate23 delegate23_0 = null;
    internal static Delegate23 delegate23_1 = null;
    internal static Hashtable hashtable_0 = new Hashtable();
    private static int int_0 = 1;
    private static int int_1 = 0;
    private static int[] int_2 = new int[0];
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

    [Attribute5(typeof(Attribute5.Class61<object>[]))]
    /* private scope */ static bool smethod_10(int int_5)
    {
        if (byte_2.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class60).Assembly.GetManifestResourceStream("kwi4k\x0098\x008b2pwovl\x00893\x0098\x0099e.os\x0092q\x00955\x008c\x009b\x00870l\x0089\x0099e7\x0089m\x0094")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer4 = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer5 = new byte[0x20];
            buffer5[0] = 0xb5;
            buffer5[0] = 0x5b;
            buffer5[0] = 0xba;
            buffer5[0] = 90;
            buffer5[0] = 0x30;
            buffer5[0] = 0xca;
            buffer5[1] = 0x22;
            buffer5[1] = 0x8f;
            buffer5[1] = 0x76;
            buffer5[1] = 0x54;
            buffer5[1] = 0x6f;
            buffer5[1] = 0xac;
            buffer5[2] = 140;
            buffer5[2] = 0x84;
            buffer5[2] = 0x5e;
            buffer5[2] = 90;
            buffer5[2] = 0x85;
            buffer5[2] = 0x1d;
            buffer5[3] = 0xad;
            buffer5[3] = 0x70;
            buffer5[3] = 0x84;
            buffer5[3] = 0x9a;
            buffer5[3] = 0x2c;
            buffer5[4] = 0x85;
            buffer5[4] = 0x6c;
            buffer5[4] = 130;
            buffer5[4] = 0x94;
            buffer5[4] = 0xd9;
            buffer5[5] = 0x94;
            buffer5[5] = 0xa6;
            buffer5[5] = 0x58;
            buffer5[5] = 0x73;
            buffer5[5] = 0x72;
            buffer5[5] = 0x27;
            buffer5[6] = 60;
            buffer5[6] = 0x60;
            buffer5[6] = 0xa5;
            buffer5[7] = 150;
            buffer5[7] = 0xb5;
            buffer5[7] = 130;
            buffer5[7] = 0x1b;
            buffer5[8] = 0x39;
            buffer5[8] = 0x74;
            buffer5[8] = 0x90;
            buffer5[8] = 0x68;
            buffer5[9] = 110;
            buffer5[9] = 160;
            buffer5[9] = 0x57;
            buffer5[9] = 0x5e;
            buffer5[9] = 0x27;
            buffer5[10] = 0x94;
            buffer5[10] = 190;
            buffer5[10] = 0xb9;
            buffer5[11] = 110;
            buffer5[11] = 0x9d;
            buffer5[11] = 0x63;
            buffer5[11] = 15;
            buffer5[11] = 0xae;
            buffer5[12] = 0xc2;
            buffer5[12] = 0x57;
            buffer5[12] = 0x90;
            buffer5[12] = 8;
            buffer5[13] = 0x8d;
            buffer5[13] = 0x7a;
            buffer5[13] = 0x94;
            buffer5[13] = 0xf5;
            buffer5[14] = 0x36;
            buffer5[14] = 0x60;
            buffer5[14] = 0xa6;
            buffer5[14] = 0x7d;
            buffer5[14] = 0x4b;
            buffer5[14] = 100;
            buffer5[15] = 0x21;
            buffer5[15] = 0x7b;
            buffer5[15] = 0x3f;
            buffer5[0x10] = 0x84;
            buffer5[0x10] = 0x7e;
            buffer5[0x10] = 0x7d;
            buffer5[0x10] = 0x72;
            buffer5[0x10] = 0x4c;
            buffer5[0x11] = 0xab;
            buffer5[0x11] = 0x95;
            buffer5[0x11] = 0x9c;
            buffer5[0x11] = 0x7d;
            buffer5[0x11] = 0x66;
            buffer5[0x11] = 0xfb;
            buffer5[0x12] = 0x95;
            buffer5[0x12] = 190;
            buffer5[0x12] = 0x68;
            buffer5[0x12] = 150;
            buffer5[0x12] = 9;
            buffer5[0x13] = 0x80;
            buffer5[0x13] = 0x3d;
            buffer5[0x13] = 0x55;
            buffer5[0x13] = 0x52;
            buffer5[0x13] = 0x93;
            buffer5[0x13] = 0x47;
            buffer5[20] = 0x7e;
            buffer5[20] = 0x87;
            buffer5[20] = 0xdd;
            buffer5[0x15] = 210;
            buffer5[0x15] = 0x55;
            buffer5[0x15] = 0x62;
            buffer5[0x16] = 0x63;
            buffer5[0x16] = 0x6b;
            buffer5[0x16] = 150;
            buffer5[0x16] = 0x5b;
            buffer5[0x16] = 0xe4;
            buffer5[0x17] = 0x76;
            buffer5[0x17] = 0xb6;
            buffer5[0x17] = 0x90;
            buffer5[0x17] = 0x35;
            buffer5[0x17] = 0x88;
            buffer5[0x17] = 0x63;
            buffer5[0x18] = 0x58;
            buffer5[0x18] = 0x98;
            buffer5[0x18] = 0x84;
            buffer5[0x18] = 0x40;
            buffer5[0x19] = 0xa2;
            buffer5[0x19] = 0x83;
            buffer5[0x19] = 0xcc;
            buffer5[0x1a] = 0x7b;
            buffer5[0x1a] = 80;
            buffer5[0x1a] = 0xb5;
            buffer5[0x1b] = 0x4c;
            buffer5[0x1b] = 0x68;
            buffer5[0x1b] = 0x83;
            buffer5[0x1c] = 0x94;
            buffer5[0x1c] = 0x7e;
            buffer5[0x1c] = 0xac;
            buffer5[0x1c] = 0x53;
            buffer5[0x1c] = 0xb3;
            buffer5[0x1c] = 0x8f;
            buffer5[0x1d] = 0x34;
            buffer5[0x1d] = 0x8e;
            buffer5[0x1d] = 0x3a;
            buffer5[0x1d] = 0x87;
            buffer5[0x1d] = 0xcd;
            buffer5[30] = 0x73;
            buffer5[30] = 0x29;
            buffer5[30] = 0x66;
            buffer5[30] = 0x73;
            buffer5[30] = 0x3a;
            buffer5[0x1f] = 140;
            buffer5[0x1f] = 230;
            buffer5[0x1f] = 0x1b;
            byte[] rgbKey = buffer5;
            byte[] buffer6 = new byte[0x10];
            buffer6[0] = 170;
            buffer6[0] = 0x51;
            buffer6[0] = 0xba;
            buffer6[0] = 0x2c;
            buffer6[1] = 0x6c;
            buffer6[1] = 0x5f;
            buffer6[1] = 0x56;
            buffer6[1] = 0x5e;
            buffer6[1] = 0x5f;
            buffer6[1] = 0x5d;
            buffer6[2] = 0x3d;
            buffer6[2] = 0xc6;
            buffer6[2] = 0x76;
            buffer6[2] = 0x75;
            buffer6[3] = 0x87;
            buffer6[3] = 0x87;
            buffer6[3] = 0x90;
            buffer6[3] = 0x69;
            buffer6[3] = 0x2d;
            buffer6[4] = 0x3e;
            buffer6[4] = 0x85;
            buffer6[4] = 0x95;
            buffer6[4] = 5;
            buffer6[5] = 0x85;
            buffer6[5] = 0x92;
            buffer6[5] = 130;
            buffer6[5] = 0x56;
            buffer6[5] = 190;
            buffer6[5] = 0xc6;
            buffer6[6] = 0x9a;
            buffer6[6] = 0xa6;
            buffer6[6] = 0x58;
            buffer6[6] = 0x73;
            buffer6[6] = 160;
            buffer6[6] = 0xdf;
            buffer6[7] = 9;
            buffer6[7] = 0x8a;
            buffer6[7] = 0x94;
            buffer6[8] = 0x39;
            buffer6[8] = 0xc4;
            buffer6[8] = 0x8b;
            buffer6[8] = 130;
            buffer6[8] = 0x90;
            buffer6[9] = 0x4e;
            buffer6[9] = 150;
            buffer6[9] = 0xab;
            buffer6[9] = 0xc6;
            buffer6[10] = 110;
            buffer6[10] = 160;
            buffer6[10] = 20;
            buffer6[11] = 0x81;
            buffer6[11] = 0x7d;
            buffer6[11] = 0x9f;
            buffer6[11] = 0x35;
            buffer6[12] = 0x7c;
            buffer6[12] = 0x7e;
            buffer6[12] = 0x63;
            buffer6[12] = 0xf8;
            buffer6[13] = 0x56;
            buffer6[13] = 0x6b;
            buffer6[13] = 0xe4;
            buffer6[14] = 0x5f;
            buffer6[14] = 0xa6;
            buffer6[14] = 0xba;
            buffer6[14] = 140;
            buffer6[14] = 0x5d;
            buffer6[14] = 0x57;
            buffer6[15] = 0x70;
            buffer6[15] = 0x72;
            buffer6[15] = 0x65;
            buffer6[15] = 0xcd;
            byte[] rgbIV = buffer6;
            byte[] publicKeyToken = typeof(Class60).Assembly.GetName().GetPublicKeyToken();
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
            stream2.Write(buffer4, 0, buffer4.Length);
            stream2.FlushFinalBlock();
            byte_2 = stream.ToArray();
            stream.Close();
            stream2.Close();
            reader.Close();
        }
        if (byte_0.Length == 0)
        {
            byte_0 = smethod_18(smethod_17(typeof(Class60).Assembly).ToString());
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

    [Attribute5(typeof(Attribute5.Class61<object>[]))]
    /* private scope */ static string smethod_11(int int_5)
    {
        if (byte_3.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class60).Assembly.GetManifestResourceStream("\x009c4\x0091d\x0093l\x0087h\x009el\x008ecaw\x0088\x0093\x008a0.q\x0086a6\x0098b\x00880li9i\x0086\x009ff6ml")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x9c;
            buffer2[0] = 0x9e;
            buffer2[0] = 0xdb;
            buffer2[1] = 160;
            buffer2[1] = 0x9e;
            buffer2[1] = 0xd6;
            buffer2[1] = 0x48;
            buffer2[2] = 0x98;
            buffer2[2] = 0x9a;
            buffer2[2] = 130;
            buffer2[2] = 0x9a;
            buffer2[2] = 0x5c;
            buffer2[2] = 0x4c;
            buffer2[3] = 0x7d;
            buffer2[3] = 160;
            buffer2[3] = 0x49;
            buffer2[4] = 0x58;
            buffer2[4] = 0xa4;
            buffer2[4] = 0xa2;
            buffer2[4] = 0x2b;
            buffer2[4] = 0x75;
            buffer2[4] = 170;
            buffer2[5] = 0xc2;
            buffer2[5] = 170;
            buffer2[5] = 0x94;
            buffer2[5] = 0x90;
            buffer2[5] = 0x9c;
            buffer2[5] = 0xa5;
            buffer2[6] = 0xb6;
            buffer2[6] = 0x9f;
            buffer2[6] = 0x8d;
            buffer2[6] = 0x68;
            buffer2[6] = 0x5e;
            buffer2[6] = 0xc4;
            buffer2[7] = 0x97;
            buffer2[7] = 0x70;
            buffer2[7] = 0x89;
            buffer2[7] = 0x7d;
            buffer2[7] = 12;
            buffer2[7] = 0xd6;
            buffer2[8] = 0x9c;
            buffer2[8] = 60;
            buffer2[8] = 0xc3;
            buffer2[8] = 0x5e;
            buffer2[8] = 0x9b;
            buffer2[9] = 0x6f;
            buffer2[9] = 0x81;
            buffer2[9] = 0x42;
            buffer2[9] = 0x97;
            buffer2[9] = 0x72;
            buffer2[10] = 0x84;
            buffer2[10] = 0x9b;
            buffer2[10] = 0x7c;
            buffer2[10] = 120;
            buffer2[11] = 0x7f;
            buffer2[11] = 0x66;
            buffer2[11] = 0x41;
            buffer2[12] = 0x54;
            buffer2[12] = 0x66;
            buffer2[12] = 0x8e;
            buffer2[12] = 90;
            buffer2[12] = 0xa7;
            buffer2[12] = 0x91;
            buffer2[13] = 160;
            buffer2[13] = 0x88;
            buffer2[13] = 0x65;
            buffer2[13] = 0x89;
            buffer2[13] = 0xa3;
            buffer2[13] = 0x61;
            buffer2[14] = 0x5e;
            buffer2[14] = 0x55;
            buffer2[14] = 0x7a;
            buffer2[14] = 160;
            buffer2[14] = 0x9a;
            buffer2[15] = 130;
            buffer2[15] = 0x97;
            buffer2[15] = 0x77;
            buffer2[15] = 0xeb;
            buffer2[0x10] = 0x8d;
            buffer2[0x10] = 0x5e;
            buffer2[0x10] = 0x5c;
            buffer2[0x10] = 0x98;
            buffer2[0x10] = 0x92;
            buffer2[0x10] = 0xa4;
            buffer2[0x11] = 0xa6;
            buffer2[0x11] = 0x86;
            buffer2[0x11] = 0x47;
            buffer2[0x12] = 0x98;
            buffer2[0x12] = 0x53;
            buffer2[0x12] = 0x7e;
            buffer2[0x12] = 0x88;
            buffer2[0x12] = 0x95;
            buffer2[0x13] = 0x94;
            buffer2[0x13] = 0x95;
            buffer2[0x13] = 0x79;
            buffer2[20] = 0x63;
            buffer2[20] = 0x72;
            buffer2[20] = 0x5f;
            buffer2[20] = 0xe9;
            buffer2[0x15] = 0x84;
            buffer2[0x15] = 0x51;
            buffer2[0x15] = 0x27;
            buffer2[0x16] = 0x72;
            buffer2[0x16] = 0x58;
            buffer2[0x16] = 0xb0;
            buffer2[0x17] = 0x54;
            buffer2[0x17] = 0x88;
            buffer2[0x17] = 0x9d;
            buffer2[0x17] = 0x5c;
            buffer2[0x18] = 0x9c;
            buffer2[0x18] = 0x8d;
            buffer2[0x18] = 0x7c;
            buffer2[0x18] = 0x74;
            buffer2[0x19] = 160;
            buffer2[0x19] = 0x66;
            buffer2[0x19] = 0x90;
            buffer2[0x1a] = 0x60;
            buffer2[0x1a] = 0x93;
            buffer2[0x1a] = 0x98;
            buffer2[0x1a] = 0x20;
            buffer2[0x1b] = 0x66;
            buffer2[0x1b] = 0x84;
            buffer2[0x1b] = 0x72;
            buffer2[0x1b] = 0xc9;
            buffer2[0x1b] = 140;
            buffer2[0x1b] = 0x88;
            buffer2[0x1c] = 0x9a;
            buffer2[0x1c] = 140;
            buffer2[0x1c] = 3;
            buffer2[0x1d] = 0x59;
            buffer2[0x1d] = 130;
            buffer2[0x1d] = 0x39;
            buffer2[0x1d] = 0xa4;
            buffer2[0x1d] = 0x7e;
            buffer2[0x1d] = 0x2e;
            buffer2[30] = 0x7e;
            buffer2[30] = 0x31;
            buffer2[30] = 0xc0;
            buffer2[30] = 240;
            buffer2[0x1f] = 0x10;
            buffer2[0x1f] = 0x92;
            buffer2[0x1f] = 0x9a;
            buffer2[0x1f] = 0x75;
            buffer2[0x1f] = 0x7d;
            buffer2[0x1f] = 0xe8;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x77;
            buffer4[0] = 0x9d;
            buffer4[0] = 0xf1;
            buffer4[1] = 0x89;
            buffer4[1] = 0x6a;
            buffer4[1] = 0x57;
            buffer4[1] = 0x86;
            buffer4[1] = 0x5f;
            buffer4[1] = 70;
            buffer4[2] = 0xa9;
            buffer4[2] = 0x45;
            buffer4[2] = 0x65;
            buffer4[2] = 0x90;
            buffer4[2] = 0x84;
            buffer4[2] = 0xf8;
            buffer4[3] = 0x94;
            buffer4[3] = 0x66;
            buffer4[3] = 0x73;
            buffer4[3] = 0xbc;
            buffer4[4] = 0xa3;
            buffer4[4] = 0x58;
            buffer4[4] = 0xc0;
            buffer4[5] = 0x9a;
            buffer4[5] = 0x31;
            buffer4[5] = 0x12;
            buffer4[5] = 0x84;
            buffer4[5] = 120;
            buffer4[6] = 0xcd;
            buffer4[6] = 0x65;
            buffer4[6] = 0x86;
            buffer4[6] = 0x5c;
            buffer4[7] = 0xa4;
            buffer4[7] = 0x6d;
            buffer4[7] = 150;
            buffer4[7] = 0x87;
            buffer4[7] = 15;
            buffer4[8] = 0x4d;
            buffer4[8] = 0xd0;
            buffer4[8] = 130;
            buffer4[8] = 0x7c;
            buffer4[8] = 0x48;
            buffer4[8] = 0xc2;
            buffer4[9] = 0x6c;
            buffer4[9] = 0xa4;
            buffer4[9] = 0xa2;
            buffer4[9] = 0x89;
            buffer4[9] = 0x5c;
            buffer4[9] = 0xf6;
            buffer4[10] = 15;
            buffer4[10] = 0x74;
            buffer4[10] = 0xb3;
            buffer4[10] = 0x86;
            buffer4[10] = 0xbc;
            buffer4[11] = 0xaf;
            buffer4[11] = 0x62;
            buffer4[11] = 0xa9;
            buffer4[11] = 0xa3;
            buffer4[11] = 3;
            buffer4[11] = 0xe7;
            buffer4[12] = 0x97;
            buffer4[12] = 0x67;
            buffer4[12] = 0x69;
            buffer4[12] = 0x5d;
            buffer4[12] = 0xf9;
            buffer4[13] = 0x2c;
            buffer4[13] = 0x88;
            buffer4[13] = 0xd4;
            buffer4[13] = 0x5f;
            buffer4[13] = 0xe2;
            buffer4[14] = 0x69;
            buffer4[14] = 110;
            buffer4[14] = 0xa2;
            buffer4[14] = 40;
            buffer4[15] = 0xcb;
            buffer4[15] = 0x9c;
            buffer4[15] = 0x2c;
            buffer4[15] = 0xc1;
            buffer4[15] = 0x5e;
            buffer4[15] = 0xd8;
            byte[] array = buffer4;
            Array.Reverse(array);
            byte[] publicKeyToken = typeof(Class60).Assembly.GetName().GetPublicKeyToken();
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

    [Attribute5(typeof(Attribute5.Class61<object>[]))]
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
            Struct72 struct2 = (Struct72) obj2;
            IntPtr destination = Marshal.AllocCoTaskMem(struct2.byte_0.Length);
            Marshal.Copy(struct2.byte_0, 0, destination, struct2.byte_0.Length);
            if (struct2.bool_0)
            {
                intptr_6 = destination;
                uint_2 = (uint) struct2.byte_0.Length;
                VirtualProtect_1(intptr_6, struct2.byte_0.Length, 0x40, ref int_4);
                return 0;
            }
            Marshal.WriteIntPtr(ptr, IntPtr.Size * 2, destination);
            Marshal.WriteInt32(ptr, IntPtr.Size * 3, struct2.byte_0.Length);
            uint num2 = 0;
            if ((uint_1 == 0xcea1d7d) && !bool_3)
            {
                bool_3 = true;
                return num2;
            }
        }
        return delegate23_1(intptr_3, intptr_4, intptr_5, uint_1, intptr_6, ref uint_2);
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
        if (bool_0)
        {
            return;
        }
        bool_0 = true;
        long num10 = 0L;
        Marshal.ReadIntPtr(new IntPtr((void*) &num10), 0);
        Marshal.ReadInt32(new IntPtr((void*) &num10), 0);
        Marshal.ReadInt64(new IntPtr((void*) &num10), 0);
        Marshal.WriteIntPtr(new IntPtr((void*) &num10), 0, IntPtr.Zero);
        Marshal.WriteInt32(new IntPtr((void*) &num10), 0, 0);
        Marshal.WriteInt64(new IntPtr((void*) &num10), 0, 0L);
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
        reader = new BinaryReader(typeof(Class60).Assembly.GetManifestResourceStream("x\x008e\x0096v\x00891\x008d\x009fawps\x0089\x0088h\x009be\x0087.\x009f\x0094oj\x008b\x009e\x008e\x008d\x0087mx\x009dk\x009bmd\x0099o")) {
            BaseStream = { Position = 0L }
        };
        byte[] buffer14 = reader.ReadBytes((int) reader.BaseStream.Length);
        byte[] buffer16 = new byte[0x20];
        buffer16[0] = 0x9a;
        buffer16[0] = 0x66;
        buffer16[0] = 0x9a;
        buffer16[0] = 200;
        buffer16[0] = 210;
        buffer16[1] = 0x59;
        buffer16[1] = 0xb5;
        buffer16[1] = 0xd3;
        buffer16[2] = 120;
        buffer16[2] = 0x80;
        buffer16[2] = 0x48;
        buffer16[2] = 160;
        buffer16[2] = 120;
        buffer16[2] = 2;
        buffer16[3] = 0x61;
        buffer16[3] = 50;
        buffer16[3] = 0xab;
        buffer16[3] = 0x7a;
        buffer16[3] = 0xee;
        buffer16[4] = 0x7e;
        buffer16[4] = 0x81;
        buffer16[4] = 0x7e;
        buffer16[4] = 110;
        buffer16[4] = 0x6a;
        buffer16[5] = 0x2a;
        buffer16[5] = 0x76;
        buffer16[5] = 110;
        buffer16[5] = 0x99;
        buffer16[5] = 0xb3;
        buffer16[6] = 0x95;
        buffer16[6] = 0xa5;
        buffer16[6] = 0x65;
        buffer16[6] = 0x27;
        buffer16[6] = 0x30;
        buffer16[7] = 0x35;
        buffer16[7] = 0x61;
        buffer16[7] = 0x72;
        buffer16[8] = 40;
        buffer16[8] = 0x98;
        buffer16[8] = 0x9f;
        buffer16[8] = 0xd1;
        buffer16[9] = 0xe2;
        buffer16[9] = 0x76;
        buffer16[9] = 0x6d;
        buffer16[9] = 0x9c;
        buffer16[10] = 0x91;
        buffer16[10] = 0x95;
        buffer16[10] = 0x3b;
        buffer16[10] = 0x90;
        buffer16[11] = 0xcb;
        buffer16[11] = 0x81;
        buffer16[11] = 0x6f;
        buffer16[11] = 160;
        buffer16[11] = 0x44;
        buffer16[12] = 0x91;
        buffer16[12] = 0x5b;
        buffer16[12] = 130;
        buffer16[12] = 0x7e;
        buffer16[12] = 0x6b;
        buffer16[12] = 0x95;
        buffer16[13] = 0x80;
        buffer16[13] = 0x88;
        buffer16[13] = 0x98;
        buffer16[13] = 150;
        buffer16[14] = 0x3d;
        buffer16[14] = 130;
        buffer16[14] = 0x6d;
        buffer16[14] = 120;
        buffer16[14] = 0xa5;
        buffer16[15] = 0x81;
        buffer16[15] = 0x88;
        buffer16[15] = 0x85;
        buffer16[15] = 0x80;
        buffer16[0x10] = 0x8a;
        buffer16[0x10] = 0x5e;
        buffer16[0x10] = 0xa2;
        buffer16[0x11] = 0x5c;
        buffer16[0x11] = 0xed;
        buffer16[0x11] = 0x43;
        buffer16[0x11] = 190;
        buffer16[0x11] = 0x73;
        buffer16[0x11] = 0xbf;
        buffer16[0x12] = 0x6c;
        buffer16[0x12] = 0x7d;
        buffer16[0x12] = 0xbb;
        buffer16[0x12] = 0x51;
        buffer16[0x12] = 0x44;
        buffer16[0x13] = 0xa2;
        buffer16[0x13] = 0x62;
        buffer16[0x13] = 0xb8;
        buffer16[0x13] = 0xa1;
        buffer16[0x13] = 0xa5;
        buffer16[0x13] = 0x13;
        buffer16[20] = 70;
        buffer16[20] = 0x8a;
        buffer16[20] = 0x69;
        buffer16[20] = 70;
        buffer16[0x15] = 0xd3;
        buffer16[0x15] = 80;
        buffer16[0x15] = 0x2e;
        buffer16[0x15] = 0x48;
        buffer16[0x16] = 0x56;
        buffer16[0x16] = 0x61;
        buffer16[0x16] = 0x6b;
        buffer16[0x16] = 0xc9;
        buffer16[0x17] = 0x9c;
        buffer16[0x17] = 0x7a;
        buffer16[0x17] = 0xe4;
        buffer16[0x18] = 0xdd;
        buffer16[0x18] = 0x89;
        buffer16[0x18] = 0x4a;
        buffer16[0x19] = 0x7f;
        buffer16[0x19] = 0x48;
        buffer16[0x19] = 0x79;
        buffer16[0x19] = 0x39;
        buffer16[0x1a] = 0xb7;
        buffer16[0x1a] = 0x73;
        buffer16[0x1a] = 0x47;
        buffer16[0x1b] = 0x41;
        buffer16[0x1b] = 0xa2;
        buffer16[0x1b] = 160;
        buffer16[0x1b] = 0x77;
        buffer16[0x1b] = 0x84;
        buffer16[0x1b] = 0xf7;
        buffer16[0x1c] = 0xa6;
        buffer16[0x1c] = 0x6c;
        buffer16[0x1c] = 130;
        buffer16[0x1c] = 0x66;
        buffer16[0x1d] = 120;
        buffer16[0x1d] = 0x4d;
        buffer16[0x1d] = 0x43;
        buffer16[0x1d] = 0xb0;
        buffer16[0x1d] = 0x22;
        buffer16[30] = 0x75;
        buffer16[30] = 0x66;
        buffer16[30] = 0x7b;
        buffer16[0x1f] = 130;
        buffer16[0x1f] = 160;
        buffer16[0x1f] = 0x8d;
        buffer16[0x1f] = 0xbb;
        byte[] rgbKey = buffer16;
        byte[] buffer17 = new byte[0x10];
        buffer17[0] = 0x53;
        buffer17[0] = 0x7c;
        buffer17[0] = 0xe0;
        buffer17[1] = 0xca;
        buffer17[1] = 13;
        buffer17[1] = 0x4b;
        buffer17[2] = 0x66;
        buffer17[2] = 0xc3;
        buffer17[2] = 0x61;
        buffer17[2] = 0x74;
        buffer17[2] = 0xdd;
        buffer17[3] = 0x84;
        buffer17[3] = 9;
        buffer17[3] = 0x4c;
        buffer17[4] = 13;
        buffer17[4] = 0x84;
        buffer17[4] = 0x60;
        buffer17[4] = 0x88;
        buffer17[4] = 0xe9;
        buffer17[5] = 160;
        buffer17[5] = 0x67;
        buffer17[5] = 0x8f;
        buffer17[5] = 0x53;
        buffer17[5] = 0x94;
        buffer17[5] = 0x5c;
        buffer17[6] = 0x6c;
        buffer17[6] = 110;
        buffer17[6] = 0x8a;
        buffer17[6] = 0xcc;
        buffer17[7] = 0xd5;
        buffer17[7] = 0x5d;
        buffer17[7] = 0x60;
        buffer17[7] = 0x6f;
        buffer17[7] = 0x62;
        buffer17[7] = 2;
        buffer17[8] = 0x76;
        buffer17[8] = 0x53;
        buffer17[8] = 0x59;
        buffer17[8] = 0xde;
        buffer17[9] = 0x62;
        buffer17[9] = 0x68;
        buffer17[9] = 150;
        buffer17[9] = 0x7b;
        buffer17[9] = 0x63;
        buffer17[9] = 0x9a;
        buffer17[10] = 0x11;
        buffer17[10] = 0xc2;
        buffer17[10] = 0x9a;
        buffer17[10] = 0x4a;
        buffer17[11] = 0x89;
        buffer17[11] = 0x66;
        buffer17[11] = 0x7c;
        buffer17[11] = 0xe2;
        buffer17[11] = 0x72;
        buffer17[11] = 0xda;
        buffer17[12] = 0x5b;
        buffer17[12] = 0x76;
        buffer17[12] = 0xa2;
        buffer17[12] = 0x60;
        buffer17[12] = 0x9c;
        buffer17[12] = 0xbc;
        buffer17[13] = 0x4f;
        buffer17[13] = 170;
        buffer17[13] = 0xa2;
        buffer17[13] = 0x4e;
        buffer17[13] = 0xb7;
        buffer17[13] = 0xf5;
        buffer17[14] = 0xa7;
        buffer17[14] = 0x9c;
        buffer17[14] = 160;
        buffer17[14] = 0xd3;
        buffer17[14] = 0xc6;
        buffer17[14] = 0x52;
        buffer17[15] = 0x60;
        buffer17[15] = 0x86;
        buffer17[15] = 0x80;
        byte[] array = buffer17;
        Array.Reverse(array);
        byte[] publicKeyToken = typeof(Class60).Assembly.GetName().GetPublicKeyToken();
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
        stream3.Write(buffer14, 0, buffer14.Length);
        stream3.FlushFinalBlock();
        byte[] buffer10 = stream2.ToArray();
        Array.Clear(array, 0, array.Length);
        stream2.Close();
        stream3.Close();
        reader.Close();
        int num15 = buffer10.Length / 8;
        if (((source = buffer10) != null) && (source.Length != 0))
        {
            numRef = source;
            goto Label_0CEE;
        }
        fixed (byte* numRef = null)
        {
            int num;
        Label_0CEE:
            num = 0;
            while (num < num15)
            {
                IntPtr ptr1 = (IntPtr) (numRef + (num * 8));
                ptr1[0] ^= (IntPtr) 0x540e8b75L;
                num++;
            }
        }
        reader = new BinaryReader(new MemoryStream(buffer10)) {
            BaseStream = { Position = 0L }
        };
        long num11 = Marshal.GetHINSTANCE(typeof(Class60).Assembly.GetModules()[0]).ToInt64();
        int num2 = 0;
        int num12 = 0;
        if ((typeof(Class60).Assembly.Location == null) || (typeof(Class60).Assembly.Location.Length == 0))
        {
            num12 = 0x1c00;
        }
        int num16 = reader.ReadInt32();
        if (reader.ReadInt32() == 1)
        {
            IntPtr ptr2 = IntPtr.Zero;
            Assembly assembly = typeof(Class60).Assembly;
            ptr2 = OpenProcess(0x38, 1, (uint) Process.GetCurrentProcess().Id);
            if (IntPtr.Size == 4)
            {
                int_1 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt32();
            }
            long_0 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt64();
            IntPtr ptr3 = IntPtr.Zero;
            for (int j = 0; j < num16; j++)
            {
                IntPtr ptr = new IntPtr((long_0 + reader.ReadInt32()) - num12);
                VirtualProtect_1(ptr, 4, 4, ref num2);
                if (IntPtr.Size == 4)
                {
                    WriteProcessMemory(ptr2, ptr, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr3);
                }
                else
                {
                    WriteProcessMemory(ptr2, ptr, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr3);
                }
                VirtualProtect_1(ptr, 4, num2, ref num2);
            }
            while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
            {
                int num6 = reader.ReadInt32();
                IntPtr ptr6 = new IntPtr(long_0 + num6);
                int num7 = reader.ReadInt32();
                VirtualProtect_1(ptr6, num7 * 4, 4, ref num2);
                for (int k = 0; k < num7; k++)
                {
                    Marshal.WriteInt32(new IntPtr(ptr6.ToInt64() + (k * 4)), reader.ReadInt32());
                }
                VirtualProtect_1(ptr6, num7 * 4, num2, ref num2);
            }
            CloseHandle(ptr2);
            return;
        }
        for (int i = 0; i < num16; i++)
        {
            IntPtr ptr7 = new IntPtr((num11 + reader.ReadInt32()) - num12);
            VirtualProtect_1(ptr7, 4, 4, ref num2);
            Marshal.WriteInt32(ptr7, reader.ReadInt32());
            VirtualProtect_1(ptr7, 4, num2, ref num2);
        }
        hashtable_0 = new Hashtable(reader.ReadInt32() + 1);
        Struct72 struct2 = new Struct72 {
            byte_0 = new byte[] { 0x2a },
            bool_0 = false
        };
        hashtable_0.Add(0L, struct2);
        bool flag = false;
        while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
        {
            int num18 = reader.ReadInt32() - num12;
            int num19 = reader.ReadInt32();
            flag = false;
            if (num19 >= 0x70000000)
            {
                flag = true;
            }
            int count = reader.ReadInt32();
            byte[] buffer11 = reader.ReadBytes(count);
            Struct72 struct3 = new Struct72 {
                byte_0 = buffer11,
                bool_0 = flag
            };
            hashtable_0.Add(num11 + num18, struct3);
        }
        long_1 = Marshal.GetHINSTANCE(typeof(Class60).Assembly.GetModules()[0]).ToInt64();
        if (IntPtr.Size == 4)
        {
            int_3 = Convert.ToInt32(long_1);
        }
        byte[] bytes = new byte[] { 0x6d, 0x73, 0x63, 0x6f, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
        string str = Encoding.UTF8.GetString(bytes);
        IntPtr ptr9 = LoadLibrary(str);
        if (ptr9 == IntPtr.Zero)
        {
            bytes = new byte[] { 0x63, 0x6c, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
            str = Encoding.UTF8.GetString(bytes);
            ptr9 = LoadLibrary(str);
        }
        byte[] buffer15 = new byte[] { 0x67, 0x65, 0x74, 0x4a, 0x69, 0x74 };
        string str2 = Encoding.UTF8.GetString(buffer15);
        Delegate24 delegate3 = (Delegate24) smethod_15(GetProcAddress(ptr9, str2), typeof(Delegate24));
        IntPtr ptr5 = delegate3();
        long num4 = 0L;
        if (IntPtr.Size == 4)
        {
            num4 = Marshal.ReadInt32(ptr5);
        }
        else
        {
            num4 = Marshal.ReadInt64(ptr5);
        }
        Marshal.ReadIntPtr(ptr5, 0);
        delegate23_0 = new Delegate23(Class60.smethod_13);
        IntPtr zero = IntPtr.Zero;
        zero = Marshal.GetFunctionPointerForDelegate(delegate23_0);
        long num5 = 0L;
        if (IntPtr.Size == 4)
        {
            num5 = Marshal.ReadInt32(new IntPtr(num4));
        }
        else
        {
            num5 = Marshal.ReadInt64(new IntPtr(num4));
        }
        Process currentProcess = Process.GetCurrentProcess();
        try
        {
            foreach (ProcessModule module in currentProcess.Modules)
            {
                if (((module.ModuleName == str) && ((num5 < module.BaseAddress.ToInt64()) || (num5 > (module.BaseAddress.ToInt64() + module.ModuleMemorySize)))) && (typeof(Class60).Assembly.EntryPoint != null))
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
                        goto Label_137C;
                    }
                }
                goto Label_139B;
            Label_137C:
                num12 = 0;
            }
        }
        catch
        {
        }
    Label_139B:
        delegate23_1 = null;
        try
        {
            delegate23_1 = (Delegate23) smethod_15(new IntPtr(num5), typeof(Delegate23));
        }
        catch
        {
            try
            {
                Delegate delegate2 = smethod_15(new IntPtr(num5), typeof(Delegate23));
                delegate23_1 = (Delegate23) Delegate.CreateDelegate(typeof(Delegate23), delegate2.Method);
            }
            catch
            {
            }
        }
        int num9 = 0;
        if (((typeof(Class60).Assembly.EntryPoint == null) || (typeof(Class60).Assembly.EntryPoint.GetParameters().Length != 2)) || ((typeof(Class60).Assembly.Location == null) || (typeof(Class60).Assembly.Location.Length <= 0)))
        {
            try
            {
                ref byte pinned numRef2;
                object obj2 = typeof(Class60).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(typeof(Class60).Assembly.ManifestModule.ModuleHandle);
                if (obj2 is IntPtr)
                {
                    intptr_1 = (IntPtr) obj2;
                }
                if (obj2.GetType().ToString() == "System.Reflection.RuntimeModule")
                {
                    intptr_1 = (IntPtr) obj2.GetType().GetField("m_pData", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(obj2);
                }
                MemoryStream stream = new MemoryStream();
                stream.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                if (IntPtr.Size == 4)
                {
                    stream.Write(BitConverter.GetBytes(intptr_1.ToInt32()), 0, 4);
                }
                else
                {
                    stream.Write(BitConverter.GetBytes(intptr_1.ToInt64()), 0, 8);
                }
                stream.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                stream.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
                stream.Position = 0L;
                byte[] buffer7 = stream.ToArray();
                stream.Close();
                uint nativeSizeOfCode = 0;
                try
                {
                    if (((source = buffer7) != null) && (source.Length != 0))
                    {
                        numRef2 = source;
                    }
                    else
                    {
                        numRef2 = null;
                    }
                    delegate23_0(new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), 0xcea1d7d, new IntPtr((void*) numRef2), ref nativeSizeOfCode);
                }
                finally
                {
                    numRef2 = null;
                }
            }
            catch
            {
            }
            RuntimeHelpers.PrepareDelegate(delegate23_1);
            RuntimeHelpers.PrepareMethod(delegate23_1.Method.MethodHandle);
            RuntimeHelpers.PrepareDelegate(delegate23_0);
            RuntimeHelpers.PrepareMethod(delegate23_0.Method.MethodHandle);
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
            byte[] buffer2 = buffer;
            byte[] buffer3 = null;
            byte[] buffer4 = null;
            byte[] buffer5 = null;
            if (IntPtr.Size == 4)
            {
                buffer5 = BitConverter.GetBytes(intptr_1.ToInt32());
                buffer3 = BitConverter.GetBytes(zero.ToInt32());
                buffer4 = BitConverter.GetBytes(Convert.ToInt32(num5));
            }
            else
            {
                buffer5 = BitConverter.GetBytes(intptr_1.ToInt64());
                buffer3 = BitConverter.GetBytes(zero.ToInt64());
                buffer4 = BitConverter.GetBytes(num5);
            }
            if (IntPtr.Size == 4)
            {
                buffer2[9] = buffer5[0];
                buffer2[10] = buffer5[1];
                buffer2[11] = buffer5[2];
                buffer2[12] = buffer5[3];
                buffer2[0x10] = buffer4[0];
                buffer2[0x11] = buffer4[1];
                buffer2[0x12] = buffer4[2];
                buffer2[0x13] = buffer4[3];
                buffer2[0x17] = buffer3[0];
                buffer2[0x18] = buffer3[1];
                buffer2[0x19] = buffer3[2];
                buffer2[0x1a] = buffer3[3];
            }
            else
            {
                buffer2[2] = buffer5[0];
                buffer2[3] = buffer5[1];
                buffer2[4] = buffer5[2];
                buffer2[5] = buffer5[3];
                buffer2[6] = buffer5[4];
                buffer2[7] = buffer5[5];
                buffer2[8] = buffer5[6];
                buffer2[9] = buffer5[7];
                buffer2[0x12] = buffer4[0];
                buffer2[0x13] = buffer4[1];
                buffer2[20] = buffer4[2];
                buffer2[0x15] = buffer4[3];
                buffer2[0x16] = buffer4[4];
                buffer2[0x17] = buffer4[5];
                buffer2[0x18] = buffer4[6];
                buffer2[0x19] = buffer4[7];
                buffer2[30] = buffer3[0];
                buffer2[0x1f] = buffer3[1];
                buffer2[0x20] = buffer3[2];
                buffer2[0x21] = buffer3[3];
                buffer2[0x22] = buffer3[4];
                buffer2[0x23] = buffer3[5];
                buffer2[0x24] = buffer3[6];
                buffer2[0x25] = buffer3[7];
            }
            Marshal.Copy(buffer2, 0, destination, buffer2.Length);
            bool_4 = false;
            VirtualProtect_1(new IntPtr(num4), IntPtr.Size, 0x40, ref num9);
            Marshal.WriteIntPtr(new IntPtr(num4), destination);
            VirtualProtect_1(new IntPtr(num4), IntPtr.Size, num9, ref num9);
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

    [Attribute5(typeof(Attribute5.Class61<object>[]))]
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

    [Attribute5(typeof(Attribute5.Class61<object>[]))]
    private static byte[] smethod_19(byte[] byte_4)
    {
        MemoryStream stream = new MemoryStream();
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Key = new byte[] { 
            0x9d, 0x3d, 0xc1, 0xb3, 0x91, 0xda, 0x69, 0xab, 0x94, 0xf6, 2, 0xdf, 0x6f, 0x77, 0x68, 0x26, 
            0xd6, 0xf2, 0x51, 0xdf, 140, 0xc7, 0xe7, 0xb7, 0xb0, 0x5b, 0xd6, 0x1a, 0x4b, 0x8b, 0xdd, 0xc2
         };
        algorithm.IV = new byte[] { 0xad, 8, 0xba, 0x24, 0x95, 0x45, 0x7a, 0xca, 130, 0x47, 9, 2, 0x72, 0xf2, 0xbd, 0x12 };
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

    internal class Attribute5 : Attribute
    {
        [Attribute5(typeof(Class61<object>[]))]
        public Attribute5(object object_0)
        {
            Class63.smethod_0();
        }

        internal class Class61<T>
        {
            public Class61()
            {
                Class63.smethod_0();
            }
        }
    }

    internal class Class62
    {
        public Class62()
        {
            Class63.smethod_0();
        }

        [Attribute5(typeof(Class60.Attribute5.Class61<object>[]))]
        internal static string smethod_0(string string_0, string string_1)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string_0);
            byte[] buffer3 = new byte[] { 
                0x52, 0x66, 0x68, 110, 0x20, 0x4d, 0x18, 0x22, 0x76, 0xb5, 0x33, 0x11, 0x12, 0x33, 12, 0x6d, 
                10, 0x20, 0x4d, 0x18, 0x22, 0x9e, 0xa1, 0x29, 0x61, 0x1c, 0x76, 0xb5, 5, 0x19, 1, 0x58
             };
            byte[] buffer4 = Class60.smethod_9(Encoding.Unicode.GetBytes(string_1));
            MemoryStream stream = new MemoryStream();
            SymmetricAlgorithm algorithm = Class60.smethod_7();
            algorithm.Key = buffer3;
            algorithm.IV = buffer4;
            CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate uint Delegate23(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate IntPtr Delegate24();

    [Flags]
    private enum Enum5
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct72
    {
        internal bool bool_0;
        internal byte[] byte_0;
    }
}

