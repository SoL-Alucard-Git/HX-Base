using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal class Class28
{
    [Attribute1(typeof(Attribute1.Class29<object>[]))]
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
    internal static Delegate11 delegate11_0 = null;
    internal static Delegate11 delegate11_1 = null;
    internal static Hashtable hashtable_0 = new Hashtable();
    private static int[] int_0 = new int[0];
    private static int int_1 = 1;
    private static int int_2 = 0;
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
    [DllImport("Kernel32", CharSet=CharSet.Ansi)]
    public static extern IntPtr GetProcAddress(IntPtr intptr_3, string string_1);
    [DllImport("Kernel32")]
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

    [Attribute1(typeof(Attribute1.Class29<object>[]))]
    /* private scope */ static bool smethod_10(int int_5)
    {
        if (byte_1.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class28).Assembly.GetManifestResourceStream("\x0086mn8\x0099a\x008b8h\x008blq\x0098w3jlg.au\x008ao\x0089o\x008c\x009b2plum\x008dp\x0092\x0099j")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x73;
            buffer2[0] = 0x9a;
            buffer2[0] = 0x4d;
            buffer2[0] = 0x4c;
            buffer2[0] = 0x80;
            buffer2[0] = 0x73;
            buffer2[1] = 140;
            buffer2[1] = 0x92;
            buffer2[1] = 0x74;
            buffer2[1] = 0x7b;
            buffer2[1] = 0x34;
            buffer2[2] = 0x6d;
            buffer2[2] = 0x30;
            buffer2[2] = 0x89;
            buffer2[2] = 0x98;
            buffer2[2] = 0x7c;
            buffer2[2] = 20;
            buffer2[3] = 0x6a;
            buffer2[3] = 0xad;
            buffer2[3] = 0x16;
            buffer2[4] = 0x4f;
            buffer2[4] = 0x40;
            buffer2[4] = 0x68;
            buffer2[4] = 0x41;
            buffer2[4] = 0xad;
            buffer2[5] = 0x76;
            buffer2[5] = 0x6c;
            buffer2[5] = 0x10;
            buffer2[6] = 0x31;
            buffer2[6] = 0x4f;
            buffer2[6] = 0x85;
            buffer2[7] = 0x77;
            buffer2[7] = 0x71;
            buffer2[7] = 0x5b;
            buffer2[7] = 0xc9;
            buffer2[8] = 0x68;
            buffer2[8] = 0x60;
            buffer2[8] = 0x8a;
            buffer2[8] = 0xdf;
            buffer2[9] = 0x9f;
            buffer2[9] = 0x6d;
            buffer2[9] = 0x74;
            buffer2[9] = 0x8a;
            buffer2[9] = 190;
            buffer2[9] = 230;
            buffer2[10] = 0xa8;
            buffer2[10] = 0x6c;
            buffer2[10] = 90;
            buffer2[10] = 0x3d;
            buffer2[11] = 0x2d;
            buffer2[11] = 140;
            buffer2[11] = 0xe9;
            buffer2[11] = 0xc6;
            buffer2[12] = 0x9c;
            buffer2[12] = 0x81;
            buffer2[12] = 0x9c;
            buffer2[12] = 0x80;
            buffer2[12] = 0x85;
            buffer2[12] = 0x24;
            buffer2[13] = 0x43;
            buffer2[13] = 0x9d;
            buffer2[13] = 0x83;
            buffer2[13] = 0x63;
            buffer2[13] = 0xe5;
            buffer2[14] = 0xa4;
            buffer2[14] = 0xce;
            buffer2[14] = 0x7a;
            buffer2[14] = 0x3e;
            buffer2[14] = 0xc7;
            buffer2[14] = 0xad;
            buffer2[15] = 0x4e;
            buffer2[15] = 0x84;
            buffer2[15] = 0x83;
            buffer2[0x10] = 0x72;
            buffer2[0x10] = 0x4f;
            buffer2[0x10] = 90;
            buffer2[0x10] = 0xf5;
            buffer2[0x11] = 0x9b;
            buffer2[0x11] = 0xc2;
            buffer2[0x11] = 0xad;
            buffer2[0x11] = 0xa8;
            buffer2[0x11] = 0x29;
            buffer2[0x12] = 0x5f;
            buffer2[0x12] = 0x77;
            buffer2[0x12] = 0x8e;
            buffer2[0x12] = 0x9e;
            buffer2[0x12] = 0x7e;
            buffer2[0x12] = 0x16;
            buffer2[0x13] = 0x67;
            buffer2[0x13] = 0x9c;
            buffer2[0x13] = 0xa8;
            buffer2[20] = 0x53;
            buffer2[20] = 0x86;
            buffer2[20] = 0x43;
            buffer2[0x15] = 0x76;
            buffer2[0x15] = 0x7a;
            buffer2[0x15] = 0xd9;
            buffer2[0x15] = 0xb2;
            buffer2[0x16] = 0x6f;
            buffer2[0x16] = 0x76;
            buffer2[0x16] = 0x8d;
            buffer2[0x17] = 0x72;
            buffer2[0x17] = 0x59;
            buffer2[0x17] = 0x70;
            buffer2[0x17] = 0x8b;
            buffer2[0x18] = 100;
            buffer2[0x18] = 0xa8;
            buffer2[0x18] = 0x87;
            buffer2[0x18] = 0x60;
            buffer2[0x18] = 0x58;
            buffer2[0x19] = 90;
            buffer2[0x19] = 0xce;
            buffer2[0x19] = 0x62;
            buffer2[0x1a] = 0x5b;
            buffer2[0x1a] = 0x80;
            buffer2[0x1a] = 0xb7;
            buffer2[0x1a] = 0xa2;
            buffer2[0x1a] = 0x89;
            buffer2[0x1a] = 0x63;
            buffer2[0x1b] = 0xa2;
            buffer2[0x1b] = 0x4d;
            buffer2[0x1b] = 3;
            buffer2[0x1c] = 0x7a;
            buffer2[0x1c] = 0x49;
            buffer2[0x1c] = 0x76;
            buffer2[0x1c] = 0xc4;
            buffer2[0x1c] = 0x67;
            buffer2[0x1c] = 0x67;
            buffer2[0x1d] = 0x7e;
            buffer2[0x1d] = 110;
            buffer2[0x1d] = 0x77;
            buffer2[0x1d] = 0x2d;
            buffer2[30] = 150;
            buffer2[30] = 0x9b;
            buffer2[30] = 0x84;
            buffer2[30] = 0x7f;
            buffer2[30] = 0xa1;
            buffer2[0x1f] = 0x8f;
            buffer2[0x1f] = 0x8f;
            buffer2[0x1f] = 0x80;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x73;
            buffer4[0] = 0x9a;
            buffer4[0] = 0x58;
            buffer4[0] = 130;
            buffer4[0] = 15;
            buffer4[1] = 0x70;
            buffer4[1] = 160;
            buffer4[1] = 0x9f;
            buffer4[1] = 0xc1;
            buffer4[1] = 0xb8;
            buffer4[1] = 0xc4;
            buffer4[2] = 0x72;
            buffer4[2] = 0x11;
            buffer4[2] = 0xbd;
            buffer4[2] = 0x88;
            buffer4[3] = 0xb8;
            buffer4[3] = 0x7f;
            buffer4[3] = 0xcf;
            buffer4[3] = 0x59;
            buffer4[3] = 0x7e;
            buffer4[3] = 0xf7;
            buffer4[4] = 110;
            buffer4[4] = 0x9e;
            buffer4[4] = 0x3a;
            buffer4[4] = 0x5c;
            buffer4[4] = 0x9e;
            buffer4[5] = 0xa1;
            buffer4[5] = 0x56;
            buffer4[5] = 0x72;
            buffer4[5] = 1;
            buffer4[5] = 0xb0;
            buffer4[5] = 0x9c;
            buffer4[6] = 0x63;
            buffer4[6] = 0x56;
            buffer4[6] = 0x91;
            buffer4[6] = 0x34;
            buffer4[6] = 0x94;
            buffer4[6] = 0x30;
            buffer4[7] = 0x7a;
            buffer4[7] = 0xb1;
            buffer4[7] = 190;
            buffer4[7] = 7;
            buffer4[8] = 90;
            buffer4[8] = 0x4d;
            buffer4[8] = 0xb3;
            buffer4[8] = 0xcb;
            buffer4[9] = 0x41;
            buffer4[9] = 0xa6;
            buffer4[9] = 0x90;
            buffer4[10] = 0xa8;
            buffer4[10] = 0xa9;
            buffer4[10] = 0xd8;
            buffer4[10] = 0x92;
            buffer4[10] = 0x62;
            buffer4[11] = 0x5d;
            buffer4[11] = 0x66;
            buffer4[11] = 0xa3;
            buffer4[11] = 0x73;
            buffer4[12] = 0x84;
            buffer4[12] = 90;
            buffer4[12] = 0xa4;
            buffer4[12] = 0xa9;
            buffer4[12] = 0x74;
            buffer4[12] = 0x7a;
            buffer4[13] = 0xad;
            buffer4[13] = 0xdb;
            buffer4[13] = 0x5e;
            buffer4[13] = 0x66;
            buffer4[13] = 130;
            buffer4[13] = 0x1a;
            buffer4[14] = 90;
            buffer4[14] = 0x6c;
            buffer4[14] = 0x39;
            buffer4[15] = 0xa1;
            buffer4[15] = 0x70;
            buffer4[15] = 0xba;
            buffer4[15] = 0x60;
            byte[] rgbIV = buffer4;
            byte[] publicKeyToken = typeof(Class28).Assembly.GetName().GetPublicKeyToken();
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
            byte_2 = smethod_18(smethod_17(typeof(Class28).Assembly).ToString());
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

    [Attribute1(typeof(Attribute1.Class29<object>[]))]
    /* private scope */ static string smethod_11(int int_5)
    {
        if (byte_3.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class28).Assembly.GetManifestResourceStream("\x009a\x0097\x008c8\x0094v\x0087g\x0099v4dfxk\x0099\x0098\x0099.q2\x0092dqv\x00880k\x008evf\x0096\x009a\x00885\x009fg")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer3 = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer4 = new byte[0x20];
            buffer4[0] = 0x56;
            buffer4[0] = 0xa6;
            buffer4[0] = 0x70;
            buffer4[0] = 0x81;
            buffer4[0] = 220;
            buffer4[0] = 0x38;
            buffer4[1] = 0x80;
            buffer4[1] = 0x9e;
            buffer4[1] = 0xc2;
            buffer4[1] = 0x75;
            buffer4[1] = 0xa3;
            buffer4[1] = 0x93;
            buffer4[2] = 0x76;
            buffer4[2] = 0x70;
            buffer4[2] = 0x83;
            buffer4[2] = 0x9b;
            buffer4[2] = 0x8e;
            buffer4[3] = 0xbf;
            buffer4[3] = 0x70;
            buffer4[3] = 0xd7;
            buffer4[3] = 0x6f;
            buffer4[3] = 0x62;
            buffer4[3] = 0x1c;
            buffer4[4] = 130;
            buffer4[4] = 0x65;
            buffer4[4] = 0xc5;
            buffer4[5] = 0x62;
            buffer4[5] = 0x7b;
            buffer4[5] = 0x43;
            buffer4[5] = 0x45;
            buffer4[5] = 130;
            buffer4[6] = 0x4b;
            buffer4[6] = 0xc4;
            buffer4[6] = 0x6c;
            buffer4[6] = 0x33;
            buffer4[6] = 0x9f;
            buffer4[7] = 0x98;
            buffer4[7] = 0x81;
            buffer4[7] = 0x80;
            buffer4[7] = 0x86;
            buffer4[7] = 110;
            buffer4[7] = 0x77;
            buffer4[8] = 0xa1;
            buffer4[8] = 0xa1;
            buffer4[8] = 0x8e;
            buffer4[9] = 0x31;
            buffer4[9] = 60;
            buffer4[9] = 0xf9;
            buffer4[10] = 0x73;
            buffer4[10] = 0x9e;
            buffer4[10] = 0xa4;
            buffer4[11] = 0x56;
            buffer4[11] = 0x4a;
            buffer4[11] = 160;
            buffer4[11] = 150;
            buffer4[11] = 0x68;
            buffer4[11] = 0xf7;
            buffer4[12] = 0x56;
            buffer4[12] = 0x5e;
            buffer4[12] = 0x6c;
            buffer4[12] = 0x7c;
            buffer4[12] = 20;
            buffer4[13] = 0x67;
            buffer4[13] = 0xa1;
            buffer4[13] = 0x98;
            buffer4[13] = 0x81;
            buffer4[13] = 0x92;
            buffer4[13] = 100;
            buffer4[14] = 0x90;
            buffer4[14] = 0x7e;
            buffer4[14] = 0x44;
            buffer4[14] = 0x7e;
            buffer4[14] = 0xe4;
            buffer4[15] = 0x90;
            buffer4[15] = 0x7a;
            buffer4[15] = 0x8b;
            buffer4[15] = 0x86;
            buffer4[15] = 0xe0;
            buffer4[15] = 0x9b;
            buffer4[0x10] = 0x57;
            buffer4[0x10] = 120;
            buffer4[0x10] = 90;
            buffer4[0x10] = 0xbf;
            buffer4[0x10] = 0x1b;
            buffer4[0x10] = 0x5c;
            buffer4[0x11] = 0x5e;
            buffer4[0x11] = 0x56;
            buffer4[0x11] = 0x98;
            buffer4[0x11] = 0xa8;
            buffer4[0x11] = 0x4c;
            buffer4[0x12] = 90;
            buffer4[0x12] = 0x62;
            buffer4[0x12] = 0x49;
            buffer4[0x12] = 0xa5;
            buffer4[0x13] = 0xa3;
            buffer4[0x13] = 0x67;
            buffer4[0x13] = 120;
            buffer4[0x13] = 0x9c;
            buffer4[0x13] = 0x5d;
            buffer4[20] = 0x6a;
            buffer4[20] = 0x8d;
            buffer4[20] = 110;
            buffer4[20] = 0x7a;
            buffer4[20] = 0x97;
            buffer4[0x15] = 0xc1;
            buffer4[0x15] = 0x71;
            buffer4[0x15] = 0x9e;
            buffer4[0x16] = 150;
            buffer4[0x16] = 140;
            buffer4[0x16] = 0x62;
            buffer4[0x16] = 0xdd;
            buffer4[0x17] = 0x7b;
            buffer4[0x17] = 0x90;
            buffer4[0x17] = 0x76;
            buffer4[0x17] = 0x72;
            buffer4[0x17] = 0x66;
            buffer4[0x17] = 0x2c;
            buffer4[0x18] = 0x68;
            buffer4[0x18] = 60;
            buffer4[0x18] = 0x5e;
            buffer4[0x19] = 0x9a;
            buffer4[0x19] = 0x9a;
            buffer4[0x19] = 0x61;
            buffer4[0x19] = 0x9a;
            buffer4[0x19] = 0x84;
            buffer4[0x1a] = 0x7e;
            buffer4[0x1a] = 0x8b;
            buffer4[0x1a] = 0x6c;
            buffer4[0x1a] = 0x75;
            buffer4[0x1a] = 0x36;
            buffer4[0x1b] = 0xa4;
            buffer4[0x1b] = 0x61;
            buffer4[0x1b] = 0x95;
            buffer4[0x1b] = 0x75;
            buffer4[0x1c] = 0x7f;
            buffer4[0x1c] = 0x6a;
            buffer4[0x1c] = 11;
            buffer4[0x1c] = 0x91;
            buffer4[0x1c] = 0x79;
            buffer4[0x1c] = 0xc9;
            buffer4[0x1d] = 0xc2;
            buffer4[0x1d] = 160;
            buffer4[0x1d] = 0xab;
            buffer4[0x1d] = 0x7e;
            buffer4[0x1d] = 0x8e;
            buffer4[0x1d] = 0x29;
            buffer4[30] = 0x5b;
            buffer4[30] = 0x76;
            buffer4[30] = 110;
            buffer4[30] = 0xc4;
            buffer4[0x1f] = 0xc6;
            buffer4[0x1f] = 0x9b;
            buffer4[0x1f] = 0x5b;
            byte[] rgbKey = buffer4;
            byte[] buffer5 = new byte[0x10];
            buffer5[0] = 0x72;
            buffer5[0] = 0x6c;
            buffer5[0] = 0x40;
            buffer5[1] = 0x66;
            buffer5[1] = 0xda;
            buffer5[1] = 0x67;
            buffer5[1] = 3;
            buffer5[2] = 0x6c;
            buffer5[2] = 0x12;
            buffer5[2] = 0x62;
            buffer5[2] = 0x76;
            buffer5[2] = 0x30;
            buffer5[3] = 0x9e;
            buffer5[3] = 0x4c;
            buffer5[3] = 0x7b;
            buffer5[3] = 0x54;
            buffer5[3] = 0x92;
            buffer5[3] = 40;
            buffer5[4] = 0x5c;
            buffer5[4] = 0x98;
            buffer5[4] = 0x8b;
            buffer5[5] = 0x68;
            buffer5[5] = 0x57;
            buffer5[5] = 0x63;
            buffer5[5] = 0x9e;
            buffer5[5] = 0x5b;
            buffer5[6] = 0x7b;
            buffer5[6] = 0x24;
            buffer5[6] = 0x16;
            buffer5[7] = 0x55;
            buffer5[7] = 0x7b;
            buffer5[7] = 0xf9;
            buffer5[8] = 0x42;
            buffer5[8] = 0xa2;
            buffer5[8] = 130;
            buffer5[8] = 0x12;
            buffer5[8] = 0x18;
            buffer5[9] = 0x92;
            buffer5[9] = 0x9b;
            buffer5[9] = 0x5e;
            buffer5[9] = 0x54;
            buffer5[10] = 0x8e;
            buffer5[10] = 0x67;
            buffer5[10] = 60;
            buffer5[11] = 0x81;
            buffer5[11] = 0xe0;
            buffer5[11] = 0x35;
            buffer5[12] = 0x9d;
            buffer5[12] = 140;
            buffer5[12] = 0x6a;
            buffer5[12] = 0x5f;
            buffer5[13] = 0x8d;
            buffer5[13] = 140;
            buffer5[13] = 0x58;
            buffer5[13] = 0x8e;
            buffer5[13] = 0x5b;
            buffer5[13] = 0xee;
            buffer5[14] = 0x60;
            buffer5[14] = 0x5b;
            buffer5[14] = 0x62;
            buffer5[14] = 0x89;
            buffer5[14] = 0xb7;
            buffer5[15] = 0x55;
            buffer5[15] = 0x7b;
            buffer5[15] = 0x59;
            buffer5[15] = 0xab;
            buffer5[15] = 0xd6;
            byte[] array = buffer5;
            Array.Reverse(array);
            byte[] publicKeyToken = typeof(Class28).Assembly.GetName().GetPublicKeyToken();
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

    [Attribute1(typeof(Attribute1.Class29<object>[]))]
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
            Struct32 struct2 = (Struct32) obj2;
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
            if ((uint_1 == 0xcea1d7d) && !bool_0)
            {
                bool_0 = true;
                return num2;
            }
        }
        return delegate11_1(intptr_3, intptr_4, intptr_5, uint_1, intptr_6, ref uint_2);
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

    ///* private scope */ static unsafe void smethod_16()
    //{
    //    BinaryReader reader;
    //    IEnumerator enumerator;
    //    if (bool_3)
    //    {
    //        return;
    //    }
    //    bool_3 = true;
    //    long num13 = 0L;
    //    Marshal.ReadIntPtr(new IntPtr((void*) &num13), 0);
    //    Marshal.ReadInt32(new IntPtr((void*) &num13), 0);
    //    Marshal.ReadInt64(new IntPtr((void*) &num13), 0);
    //    Marshal.WriteIntPtr(new IntPtr((void*) &num13), 0, IntPtr.Zero);
    //    Marshal.WriteInt32(new IntPtr((void*) &num13), 0, 0);
    //    Marshal.WriteInt64(new IntPtr((void*) &num13), 0, 0L);
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
    //                    Version version3 = new Version(4, 0, 0x766f, 0x427c);
    //                    Version version2 = new Version(4, 0, 0x766f, 0x4601);
    //                    if ((version >= version3) && (version < version2))
    //                    {
    //                        goto Label_01A6;
    //                    }
    //                }
    //            }
    //            goto Label_01C3;
    //        Label_01A6:
    //            bool_5 = true;
    //        }
    //    }
    //Label_01C3:
    //    reader = new BinaryReader(typeof(Class28).Assembly.GetManifestResourceStream("rp\x008edei\x008dprl\x00864\x0099\x008d\x008e\x008fa\x0099.64\x009e\x008d\x0091b\x008e\x009a\x009dikf\x0090\x0091t\x0099\x008dw")) {
    //        BaseStream = { Position = 0L }
    //    };
    //    byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
    //    byte[] buffer14 = new byte[0x20];
    //    buffer14[0] = 0x57;
    //    buffer14[0] = 0x4b;
    //    buffer14[0] = 0x79;
    //    buffer14[1] = 90;
    //    buffer14[1] = 0x21;
    //    buffer14[1] = 20;
    //    buffer14[1] = 0xb5;
    //    buffer14[2] = 160;
    //    buffer14[2] = 13;
    //    buffer14[2] = 0x62;
    //    buffer14[3] = 0x6d;
    //    buffer14[3] = 0x76;
    //    buffer14[3] = 0x9c;
    //    buffer14[4] = 0x7c;
    //    buffer14[4] = 0x6b;
    //    buffer14[4] = 0x7c;
    //    buffer14[4] = 0x3b;
    //    buffer14[4] = 0x2b;
    //    buffer14[5] = 0x79;
    //    buffer14[5] = 0x66;
    //    buffer14[5] = 0x69;
    //    buffer14[6] = 0x7f;
    //    buffer14[6] = 0x69;
    //    buffer14[6] = 0x8a;
    //    buffer14[6] = 0x81;
    //    buffer14[6] = 0x85;
    //    buffer14[7] = 0xc9;
    //    buffer14[7] = 0x71;
    //    buffer14[7] = 0x57;
    //    buffer14[7] = 15;
    //    buffer14[8] = 0x8e;
    //    buffer14[8] = 0x61;
    //    buffer14[8] = 0x67;
    //    buffer14[8] = 0x92;
    //    buffer14[8] = 0xa8;
    //    buffer14[8] = 0x9d;
    //    buffer14[9] = 0x58;
    //    buffer14[9] = 0x9c;
    //    buffer14[9] = 0xc2;
    //    buffer14[10] = 0x7a;
    //    buffer14[10] = 0x90;
    //    buffer14[10] = 0x49;
    //    buffer14[10] = 0xba;
    //    buffer14[10] = 4;
    //    buffer14[11] = 0xa4;
    //    buffer14[11] = 0x3d;
    //    buffer14[11] = 0x61;
    //    buffer14[11] = 0xdf;
    //    buffer14[11] = 0x8a;
    //    buffer14[12] = 0xb9;
    //    buffer14[12] = 0x6d;
    //    buffer14[12] = 0xa4;
    //    buffer14[12] = 0x90;
    //    buffer14[12] = 100;
    //    buffer14[12] = 0xf1;
    //    buffer14[13] = 0x90;
    //    buffer14[13] = 0x92;
    //    buffer14[13] = 0x5c;
    //    buffer14[13] = 0x56;
    //    buffer14[13] = 0x70;
    //    buffer14[13] = 0x8e;
    //    buffer14[14] = 0x6c;
    //    buffer14[14] = 90;
    //    buffer14[14] = 140;
    //    buffer14[14] = 0x38;
    //    buffer14[15] = 0x8f;
    //    buffer14[15] = 0x76;
    //    buffer14[15] = 0x77;
    //    buffer14[15] = 0xa8;
    //    buffer14[15] = 4;
    //    buffer14[0x10] = 100;
    //    buffer14[0x10] = 0x71;
    //    buffer14[0x10] = 0x95;
    //    buffer14[0x10] = 0x9a;
    //    buffer14[0x10] = 100;
    //    buffer14[0x10] = 0xac;
    //    buffer14[0x11] = 0x93;
    //    buffer14[0x11] = 0x90;
    //    buffer14[0x11] = 0x59;
    //    buffer14[0x11] = 0xa4;
    //    buffer14[0x11] = 0x81;
    //    buffer14[0x11] = 3;
    //    buffer14[0x12] = 120;
    //    buffer14[0x12] = 0x7a;
    //    buffer14[0x12] = 0x9c;
    //    buffer14[0x12] = 0x80;
    //    buffer14[0x12] = 0xef;
    //    buffer14[0x13] = 0x5f;
    //    buffer14[0x13] = 0x5d;
    //    buffer14[0x13] = 0x25;
    //    buffer14[0x13] = 0x95;
    //    buffer14[20] = 0xa8;
    //    buffer14[20] = 0x8f;
    //    buffer14[20] = 0x15;
    //    buffer14[0x15] = 0x67;
    //    buffer14[0x15] = 0x58;
    //    buffer14[0x15] = 0x57;
    //    buffer14[0x15] = 0xa1;
    //    buffer14[0x15] = 0x76;
    //    buffer14[0x15] = 0x76;
    //    buffer14[0x16] = 0xd1;
    //    buffer14[0x16] = 0xd5;
    //    buffer14[0x16] = 0x9a;
    //    buffer14[0x16] = 0x7e;
    //    buffer14[0x16] = 130;
    //    buffer14[0x17] = 0x9e;
    //    buffer14[0x17] = 0x67;
    //    buffer14[0x17] = 0x92;
    //    buffer14[0x17] = 0x3a;
    //    buffer14[0x18] = 140;
    //    buffer14[0x18] = 0xd7;
    //    buffer14[0x18] = 0x12;
    //    buffer14[0x19] = 0x4b;
    //    buffer14[0x19] = 110;
    //    buffer14[0x19] = 110;
    //    buffer14[0x19] = 0x67;
    //    buffer14[0x19] = 0x8a;
    //    buffer14[0x1a] = 0x4f;
    //    buffer14[0x1a] = 0xb7;
    //    buffer14[0x1a] = 0xa9;
    //    buffer14[0x1a] = 0x2d;
    //    buffer14[0x1a] = 0x21;
    //    buffer14[0x1b] = 0xa5;
    //    buffer14[0x1b] = 0x97;
    //    buffer14[0x1b] = 0x7b;
    //    buffer14[0x1b] = 0x5e;
    //    buffer14[0x1b] = 150;
    //    buffer14[0x1b] = 0x31;
    //    buffer14[0x1c] = 0x91;
    //    buffer14[0x1c] = 0x73;
    //    buffer14[0x1c] = 0xf8;
    //    buffer14[0x1d] = 0x7c;
    //    buffer14[0x1d] = 0x67;
    //    buffer14[0x1d] = 0x6f;
    //    buffer14[0x1d] = 0x55;
    //    buffer14[0x1d] = 0x21;
    //    buffer14[30] = 0x95;
    //    buffer14[30] = 0x9e;
    //    buffer14[30] = 0x60;
    //    buffer14[30] = 0x8e;
    //    buffer14[30] = 0x58;
    //    buffer14[0x1f] = 0xb2;
    //    buffer14[0x1f] = 0x2d;
    //    buffer14[0x1f] = 0x63;
    //    buffer14[0x1f] = 0x66;
    //    buffer14[0x1f] = 130;
    //    buffer14[0x1f] = 0x21;
    //    byte[] rgbKey = buffer14;
    //    byte[] buffer15 = new byte[0x10];
    //    buffer15[0] = 0x57;
    //    buffer15[0] = 0x4b;
    //    buffer15[0] = 0x70;
    //    buffer15[0] = 100;
    //    buffer15[0] = 0x51;
    //    buffer15[0] = 0x97;
    //    buffer15[1] = 0x1c;
    //    buffer15[1] = 0xa4;
    //    buffer15[1] = 0x25;
    //    buffer15[1] = 0x34;
    //    buffer15[2] = 0x6f;
    //    buffer15[2] = 0x5c;
    //    buffer15[2] = 0xa2;
    //    buffer15[2] = 0xe0;
    //    buffer15[3] = 0x44;
    //    buffer15[3] = 0x44;
    //    buffer15[3] = 90;
    //    buffer15[3] = 8;
    //    buffer15[4] = 0x65;
    //    buffer15[4] = 0x7f;
    //    buffer15[4] = 0x1f;
    //    buffer15[4] = 0x44;
    //    buffer15[5] = 0x8f;
    //    buffer15[5] = 0x33;
    //    buffer15[5] = 0x8f;
    //    buffer15[5] = 0x70;
    //    buffer15[5] = 0x84;
    //    buffer15[6] = 0x5d;
    //    buffer15[6] = 0x85;
    //    buffer15[6] = 0x67;
    //    buffer15[6] = 0x45;
    //    buffer15[7] = 0x6c;
    //    buffer15[7] = 0xb5;
    //    buffer15[7] = 0x83;
    //    buffer15[7] = 0x93;
    //    buffer15[7] = 0x1c;
    //    buffer15[8] = 0x8b;
    //    buffer15[8] = 0x49;
    //    buffer15[8] = 0xa3;
    //    buffer15[8] = 0xa8;
    //    buffer15[8] = 0x5d;
    //    buffer15[9] = 0x3d;
    //    buffer15[9] = 0x61;
    //    buffer15[9] = 0x3f;
    //    buffer15[10] = 0xd4;
    //    buffer15[10] = 190;
    //    buffer15[10] = 0xb6;
    //    buffer15[11] = 0xde;
    //    buffer15[11] = 0x90;
    //    buffer15[11] = 0xd0;
    //    buffer15[12] = 0x98;
    //    buffer15[12] = 180;
    //    buffer15[12] = 0x37;
    //    buffer15[12] = 0x56;
    //    buffer15[12] = 0xe0;
    //    buffer15[13] = 0x9d;
    //    buffer15[13] = 0x62;
    //    buffer15[13] = 0x49;
    //    buffer15[13] = 0x6a;
    //    buffer15[13] = 0x67;
    //    buffer15[14] = 0x9d;
    //    buffer15[14] = 0x95;
    //    buffer15[14] = 0x94;
    //    buffer15[14] = 0x97;
    //    buffer15[14] = 0x63;
    //    buffer15[14] = 0x24;
    //    buffer15[15] = 50;
    //    buffer15[15] = 0x7b;
    //    buffer15[15] = 0xeb;
    //    byte[] array = buffer15;
    //    Array.Reverse(array);
    //    byte[] publicKeyToken = typeof(Class28).Assembly.GetName().GetPublicKeyToken();
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
    //    stream2.Write(buffer, 0, buffer.Length);
    //    stream2.FlushFinalBlock();
    //    byte[] buffer6 = stream.ToArray();
    //    Array.Clear(array, 0, array.Length);
    //    stream.Close();
    //    stream2.Close();
    //    reader.Close();
    //    int num8 = buffer6.Length / 8;
    //    if (((source = buffer6) != null) && (source.Length != 0))
    //    {
    //        numRef = source;
    //        goto Label_0CCA;
    //    }
    //    fixed (byte* numRef = null)
    //    {
    //        int num2;
    //    Label_0CCA:
    //        num2 = 0;
    //        while (num2 < num8)
    //        {
    //            IntPtr ptr1 = (IntPtr) (numRef + (num2 * 8));
    //            ptr1[0] ^= (IntPtr) 0x50dd5c49L;
    //            num2++;
    //        }
    //    }
    //    reader = new BinaryReader(new MemoryStream(buffer6)) {
    //        BaseStream = { Position = 0L }
    //    };
    //    long num11 = Marshal.GetHINSTANCE(typeof(Class28).Assembly.GetModules()[0]).ToInt64();
    //    int num7 = 0;
    //    int num18 = 0;
    //    if ((typeof(Class28).Assembly.Location == null) || (typeof(Class28).Assembly.Location.Length == 0))
    //    {
    //        num18 = 0x1c00;
    //    }
    //    int num15 = reader.ReadInt32();
    //    if (reader.ReadInt32() == 1)
    //    {
    //        IntPtr ptr4 = IntPtr.Zero;
    //        Assembly assembly = typeof(Class28).Assembly;
    //        ptr4 = OpenProcess(0x38, 1, (uint) Process.GetCurrentProcess().Id);
    //        if (IntPtr.Size == 4)
    //        {
    //            int_2 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt32();
    //        }
    //        long_1 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt64();
    //        IntPtr ptr2 = IntPtr.Zero;
    //        for (int j = 0; j < num15; j++)
    //        {
    //            IntPtr ptr5 = new IntPtr((long_1 + reader.ReadInt32()) - num18);
    //            VirtualProtect_1(ptr5, 4, 4, ref num7);
    //            if (IntPtr.Size == 4)
    //            {
    //                WriteProcessMemory(ptr4, ptr5, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr2);
    //            }
    //            else
    //            {
    //                WriteProcessMemory(ptr4, ptr5, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr2);
    //            }
    //            VirtualProtect_1(ptr5, 4, num7, ref num7);
    //        }
    //        while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
    //        {
    //            int num19 = reader.ReadInt32();
    //            IntPtr ptr6 = new IntPtr(long_1 + num19);
    //            int num6 = reader.ReadInt32();
    //            VirtualProtect_1(ptr6, num6 * 4, 4, ref num7);
    //            for (int k = 0; k < num6; k++)
    //            {
    //                Marshal.WriteInt32(new IntPtr(ptr6.ToInt64() + (k * 4)), reader.ReadInt32());
    //            }
    //            VirtualProtect_1(ptr6, num6 * 4, num7, ref num7);
    //        }
    //        CloseHandle(ptr4);
    //        return;
    //    }
    //    for (int i = 0; i < num15; i++)
    //    {
    //        IntPtr ptr11 = new IntPtr((num11 + reader.ReadInt32()) - num18);
    //        VirtualProtect_1(ptr11, 4, 4, ref num7);
    //        Marshal.WriteInt32(ptr11, reader.ReadInt32());
    //        VirtualProtect_1(ptr11, 4, num7, ref num7);
    //    }
    //    hashtable_0 = new Hashtable(reader.ReadInt32() + 1);
    //    Struct32 struct3 = new Struct32 {
    //        byte_0 = new byte[] { 0x2a },
    //        bool_0 = false
    //    };
    //    hashtable_0.Add(0L, struct3);
    //    bool flag = false;
    //    while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
    //    {
    //        int num12 = reader.ReadInt32() - num18;
    //        int num20 = reader.ReadInt32();
    //        flag = false;
    //        if (num20 >= 0x70000000)
    //        {
    //            flag = true;
    //        }
    //        int count = reader.ReadInt32();
    //        byte[] buffer7 = reader.ReadBytes(count);
    //        Struct32 struct2 = new Struct32 {
    //            byte_0 = buffer7,
    //            bool_0 = flag
    //        };
    //        hashtable_0.Add(num11 + num12, struct2);
    //    }
    //    long_0 = Marshal.GetHINSTANCE(typeof(Class28).Assembly.GetModules()[0]).ToInt64();
    //    if (IntPtr.Size == 4)
    //    {
    //        int_3 = Convert.ToInt32(long_0);
    //    }
    //    byte[] bytes = new byte[] { 0x6d, 0x73, 0x63, 0x6f, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
    //    string str = Encoding.UTF8.GetString(bytes);
    //    IntPtr ptr9 = LoadLibrary(str);
    //    if (ptr9 == IntPtr.Zero)
    //    {
    //        bytes = new byte[] { 0x63, 0x6c, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
    //        str = Encoding.UTF8.GetString(bytes);
    //        ptr9 = LoadLibrary(str);
    //    }
    //    byte[] buffer16 = new byte[] { 0x67, 0x65, 0x74, 0x4a, 0x69, 0x74 };
    //    string str2 = Encoding.UTF8.GetString(buffer16);
    //    Delegate12 delegate3 = (Delegate12) smethod_15(GetProcAddress(ptr9, str2), typeof(Delegate12));
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
    //    delegate11_0 = new Delegate11(Class28.smethod_13);
    //    IntPtr zero = IntPtr.Zero;
    //    zero = Marshal.GetFunctionPointerForDelegate(delegate11_0);
    //    long num4 = 0L;
    //    if (IntPtr.Size == 4)
    //    {
    //        num4 = Marshal.ReadInt32(new IntPtr(num3));
    //    }
    //    else
    //    {
    //        num4 = Marshal.ReadInt64(new IntPtr(num3));
    //    }
    //    Process currentProcess = Process.GetCurrentProcess();
    //    try
    //    {
    //        foreach (ProcessModule module2 in currentProcess.Modules)
    //        {
    //            if (((module2.ModuleName == str) && ((num4 < module2.BaseAddress.ToInt64()) || (num4 > (module2.BaseAddress.ToInt64() + module2.ModuleMemorySize)))) && (typeof(Class28).Assembly.EntryPoint != null))
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
    //                ProcessModule module3 = (ProcessModule) enumerator.Current;
    //                if (module3.BaseAddress.ToInt64() == long_0)
    //                {
    //                    goto Label_1358;
    //                }
    //            }
    //            goto Label_1377;
    //        Label_1358:
    //            num18 = 0;
    //        }
    //    }
    //    catch
    //    {
    //    }
    //Label_1377:
    //    delegate11_1 = null;
    //    try
    //    {
    //        delegate11_1 = (Delegate11) smethod_15(new IntPtr(num4), typeof(Delegate11));
    //    }
    //    catch
    //    {
    //        try
    //        {
    //            Delegate delegate2 = smethod_15(new IntPtr(num4), typeof(Delegate11));
    //            delegate11_1 = (Delegate11) Delegate.CreateDelegate(typeof(Delegate11), delegate2.Method);
    //        }
    //        catch
    //        {
    //        }
    //    }
    //    int num17 = 0;
    //    if (((typeof(Class28).Assembly.EntryPoint == null) || (typeof(Class28).Assembly.EntryPoint.GetParameters().Length != 2)) || ((typeof(Class28).Assembly.Location == null) || (typeof(Class28).Assembly.Location.Length <= 0)))
    //    {
    //        try
    //        {
    //            ref byte pinned numRef2;
    //            object obj2 = typeof(Class28).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(typeof(Class28).Assembly.ManifestModule.ModuleHandle);
    //            if (obj2 is IntPtr)
    //            {
    //                intptr_0 = (IntPtr) obj2;
    //            }
    //            if (obj2.GetType().ToString() == "System.Reflection.RuntimeModule")
    //            {
    //                intptr_0 = (IntPtr) obj2.GetType().GetField("m_pData", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(obj2);
    //            }
    //            MemoryStream stream3 = new MemoryStream();
    //            stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
    //            if (IntPtr.Size == 4)
    //            {
    //                stream3.Write(BitConverter.GetBytes(intptr_0.ToInt32()), 0, 4);
    //            }
    //            else
    //            {
    //                stream3.Write(BitConverter.GetBytes(intptr_0.ToInt64()), 0, 8);
    //            }
    //            stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
    //            stream3.Write(new byte[IntPtr.Size], 0, IntPtr.Size);
    //            stream3.Position = 0L;
    //            byte[] buffer8 = stream3.ToArray();
    //            stream3.Close();
    //            uint nativeSizeOfCode = 0;
    //            try
    //            {
    //                if (((source = buffer8) != null) && (source.Length != 0))
    //                {
    //                    numRef2 = source;
    //                }
    //                else
    //                {
    //                    numRef2 = null;
    //                }
    //                delegate11_0(new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), 0xcea1d7d, new IntPtr((void*) numRef2), ref nativeSizeOfCode);
    //            }
    //            finally
    //            {
    //                numRef2 = null;
    //            }
    //        }
    //        catch
    //        {
    //        }
    //        RuntimeHelpers.PrepareDelegate(delegate11_1);
    //        RuntimeHelpers.PrepareMethod(delegate11_1.Method.MethodHandle);
    //        RuntimeHelpers.PrepareDelegate(delegate11_0);
    //        RuntimeHelpers.PrepareMethod(delegate11_0.Method.MethodHandle);
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
    //        byte[] buffer10 = buffer9;
    //        byte[] buffer13 = null;
    //        byte[] buffer12 = null;
    //        byte[] buffer11 = null;
    //        if (IntPtr.Size == 4)
    //        {
    //            buffer11 = BitConverter.GetBytes(intptr_0.ToInt32());
    //            buffer13 = BitConverter.GetBytes(zero.ToInt32());
    //            buffer12 = BitConverter.GetBytes(Convert.ToInt32(num4));
    //        }
    //        else
    //        {
    //            buffer11 = BitConverter.GetBytes(intptr_0.ToInt64());
    //            buffer13 = BitConverter.GetBytes(zero.ToInt64());
    //            buffer12 = BitConverter.GetBytes(num4);
    //        }
    //        if (IntPtr.Size == 4)
    //        {
    //            buffer10[9] = buffer11[0];
    //            buffer10[10] = buffer11[1];
    //            buffer10[11] = buffer11[2];
    //            buffer10[12] = buffer11[3];
    //            buffer10[0x10] = buffer12[0];
    //            buffer10[0x11] = buffer12[1];
    //            buffer10[0x12] = buffer12[2];
    //            buffer10[0x13] = buffer12[3];
    //            buffer10[0x17] = buffer13[0];
    //            buffer10[0x18] = buffer13[1];
    //            buffer10[0x19] = buffer13[2];
    //            buffer10[0x1a] = buffer13[3];
    //        }
    //        else
    //        {
    //            buffer10[2] = buffer11[0];
    //            buffer10[3] = buffer11[1];
    //            buffer10[4] = buffer11[2];
    //            buffer10[5] = buffer11[3];
    //            buffer10[6] = buffer11[4];
    //            buffer10[7] = buffer11[5];
    //            buffer10[8] = buffer11[6];
    //            buffer10[9] = buffer11[7];
    //            buffer10[0x12] = buffer12[0];
    //            buffer10[0x13] = buffer12[1];
    //            buffer10[20] = buffer12[2];
    //            buffer10[0x15] = buffer12[3];
    //            buffer10[0x16] = buffer12[4];
    //            buffer10[0x17] = buffer12[5];
    //            buffer10[0x18] = buffer12[6];
    //            buffer10[0x19] = buffer12[7];
    //            buffer10[30] = buffer13[0];
    //            buffer10[0x1f] = buffer13[1];
    //            buffer10[0x20] = buffer13[2];
    //            buffer10[0x21] = buffer13[3];
    //            buffer10[0x22] = buffer13[4];
    //            buffer10[0x23] = buffer13[5];
    //            buffer10[0x24] = buffer13[6];
    //            buffer10[0x25] = buffer13[7];
    //        }
    //        Marshal.Copy(buffer10, 0, destination, buffer10.Length);
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

    [Attribute1(typeof(Attribute1.Class29<object>[]))]
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

    [Attribute1(typeof(Attribute1.Class29<object>[]))]
    private static byte[] smethod_19(byte[] byte_4)
    {
        MemoryStream stream = new MemoryStream();
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Key = new byte[] { 
            0xbb, 0x4a, 0xf6, 0x12, 0xce, 0x84, 0xb5, 0xd6, 0xb3, 10, 0xde, 0x22, 0x1b, 0x81, 0xfd, 0x90, 
            0xac, 0x17, 0xcd, 0xd3, 0xf5, 0x6c, 0xa4, 0x94, 150, 230, 9, 0xcf, 0x5d, 0x9f, 0xe5, 0xa9
         };
        algorithm.IV = new byte[] { 0x55, 0x1d, 0xe0, 0xae, 12, 0xcc, 0x9c, 0xe3, 0xd9, 0x3b, 0x36, 3, 0x75, 0x4b, 0x48, 0x25 };
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
        return bool_6;
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
            bool_6 = (bool) typeof(RijndaelManaged).Assembly.GetType("System.Security.Cryptography.CryptoConfig", false).GetMethod("get_AllowOnlyFipsAlgorithms", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[0]);
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
        [Attribute1(typeof(Class29<object>[]))]
        public Attribute1(object object_0)
        {
            
        }

        internal class Class29<T>
        {
            public Class29()
            {
                
            }
        }
    }

    internal class Class30
    {
        public Class30()
        {
            
        }

        [Attribute1(typeof(Class28.Attribute1.Class29<object>[]))]
        internal static string smethod_0(string string_0, string string_1)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string_0);
            byte[] buffer3 = new byte[] { 
                0x52, 0x66, 0x68, 110, 0x20, 0x4d, 0x18, 0x22, 0x76, 0xb5, 0x33, 0x11, 0x12, 0x33, 12, 0x6d, 
                10, 0x20, 0x4d, 0x18, 0x22, 0x9e, 0xa1, 0x29, 0x61, 0x1c, 0x76, 0xb5, 5, 0x19, 1, 0x58
             };
            byte[] buffer4 = Class28.smethod_9(Encoding.Unicode.GetBytes(string_1));
            MemoryStream stream = new MemoryStream();
            SymmetricAlgorithm algorithm = Class28.smethod_7();
            algorithm.Key = buffer3;
            algorithm.IV = buffer4;
            CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate uint Delegate11(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate IntPtr Delegate12();

    [Flags]
    private enum Enum1
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct32
    {
        internal bool bool_0;
        internal byte[] byte_0;
    }
}

