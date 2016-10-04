using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal class Class39
{
    private static bool bool_0 = false;
    private static bool bool_1 = false;
    private static bool bool_2 = false;
    private static bool bool_3 = false;
    [Attribute2(typeof(Attribute2.Class40<object>[]))]
    private static bool bool_4 = false;
    private static bool bool_5 = false;
    private static bool bool_6 = false;
    private static byte[] byte_0 = new byte[0];
    private static byte[] byte_1 = new byte[0];
    private static byte[] byte_2 = new byte[0];
    private static byte[] byte_3 = new byte[0];
    internal static Delegate14 delegate14_0 = null;
    internal static Delegate14 delegate14_1 = null;
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

    [Attribute2(typeof(Attribute2.Class40<object>[]))]
    /* private scope */ static bool smethod_10(int int_5)
    {
        if (byte_0.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class39).Assembly.GetManifestResourceStream("\x0090q\x009d\x009bo\x009c\x008b\x0093ypbm8\x0092\x0090oxg.op\x0086\x0093\x0087\x0095\x008cv\x00950ek7tb\x008cy\x009b")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x85;
            buffer2[0] = 0x86;
            buffer2[0] = 0x5c;
            buffer2[0] = 14;
            buffer2[0] = 0x60;
            buffer2[0] = 0xf3;
            buffer2[1] = 0x92;
            buffer2[1] = 0x61;
            buffer2[1] = 0x88;
            buffer2[1] = 0x54;
            buffer2[1] = 0x69;
            buffer2[1] = 0x9e;
            buffer2[2] = 0x6a;
            buffer2[2] = 0x55;
            buffer2[2] = 0x98;
            buffer2[2] = 0x9c;
            buffer2[2] = 0x79;
            buffer2[2] = 0xb1;
            buffer2[3] = 0x68;
            buffer2[3] = 0x93;
            buffer2[3] = 80;
            buffer2[3] = 0x92;
            buffer2[3] = 0x22;
            buffer2[4] = 140;
            buffer2[4] = 0x73;
            buffer2[4] = 0x4c;
            buffer2[4] = 0x56;
            buffer2[5] = 0x66;
            buffer2[5] = 0xa9;
            buffer2[5] = 0x9a;
            buffer2[5] = 0x5c;
            buffer2[5] = 0x69;
            buffer2[6] = 0x5c;
            buffer2[6] = 0x83;
            buffer2[6] = 0x84;
            buffer2[6] = 0x80;
            buffer2[6] = 80;
            buffer2[7] = 140;
            buffer2[7] = 0x73;
            buffer2[7] = 0x60;
            buffer2[7] = 0x86;
            buffer2[8] = 0x1f;
            buffer2[8] = 0x73;
            buffer2[8] = 0x93;
            buffer2[8] = 0x6c;
            buffer2[8] = 0xe4;
            buffer2[9] = 0x2a;
            buffer2[9] = 0x68;
            buffer2[9] = 0x45;
            buffer2[10] = 130;
            buffer2[10] = 140;
            buffer2[10] = 0x9e;
            buffer2[10] = 220;
            buffer2[11] = 0xda;
            buffer2[11] = 0x87;
            buffer2[11] = 110;
            buffer2[11] = 0x92;
            buffer2[12] = 170;
            buffer2[12] = 0xd0;
            buffer2[12] = 0x92;
            buffer2[12] = 8;
            buffer2[13] = 0x84;
            buffer2[13] = 0x40;
            buffer2[13] = 0x71;
            buffer2[13] = 150;
            buffer2[13] = 0xd8;
            buffer2[13] = 0x56;
            buffer2[14] = 0x61;
            buffer2[14] = 0x4b;
            buffer2[14] = 0x21;
            buffer2[15] = 0x9e;
            buffer2[15] = 0x93;
            buffer2[15] = 0x17;
            buffer2[0x10] = 0xa8;
            buffer2[0x10] = 0x7b;
            buffer2[0x10] = 0x98;
            buffer2[0x10] = 0x7c;
            buffer2[0x11] = 0x6a;
            buffer2[0x11] = 0x9c;
            buffer2[0x11] = 0x19;
            buffer2[0x11] = 0xaf;
            buffer2[0x12] = 0xa8;
            buffer2[0x12] = 0xa6;
            buffer2[0x12] = 0x6a;
            buffer2[0x12] = 0xdd;
            buffer2[0x13] = 0xa9;
            buffer2[0x13] = 0x56;
            buffer2[0x13] = 130;
            buffer2[0x13] = 0x8a;
            buffer2[0x13] = 0xe8;
            buffer2[20] = 0xa7;
            buffer2[20] = 0x4b;
            buffer2[20] = 0xa1;
            buffer2[0x15] = 0x7f;
            buffer2[0x15] = 0x6f;
            buffer2[0x15] = 0xe0;
            buffer2[0x16] = 0x92;
            buffer2[0x16] = 0x5d;
            buffer2[0x16] = 0x92;
            buffer2[0x16] = 0x98;
            buffer2[0x16] = 0x92;
            buffer2[0x16] = 0xb2;
            buffer2[0x17] = 0x5f;
            buffer2[0x17] = 0x76;
            buffer2[0x17] = 0x94;
            buffer2[0x17] = 0xf1;
            buffer2[0x17] = 0x36;
            buffer2[0x17] = 0xa6;
            buffer2[0x18] = 0x59;
            buffer2[0x18] = 0x59;
            buffer2[0x18] = 0xda;
            buffer2[0x19] = 0x5e;
            buffer2[0x19] = 0x63;
            buffer2[0x19] = 0x81;
            buffer2[0x19] = 0x93;
            buffer2[0x1a] = 0xa2;
            buffer2[0x1a] = 0x94;
            buffer2[0x1a] = 0x7b;
            buffer2[0x1a] = 0x7a;
            buffer2[0x1a] = 0xa9;
            buffer2[0x1b] = 90;
            buffer2[0x1b] = 0x80;
            buffer2[0x1b] = 0x75;
            buffer2[0x1b] = 0xba;
            buffer2[0x1c] = 0x68;
            buffer2[0x1c] = 0xaf;
            buffer2[0x1c] = 100;
            buffer2[0x1c] = 0x91;
            buffer2[0x1c] = 0xee;
            buffer2[0x1d] = 60;
            buffer2[0x1d] = 0x9c;
            buffer2[0x1d] = 0x90;
            buffer2[0x1d] = 0x83;
            buffer2[0x1d] = 13;
            buffer2[30] = 0x51;
            buffer2[30] = 0x9e;
            buffer2[30] = 0x5c;
            buffer2[0x1f] = 0xc4;
            buffer2[0x1f] = 140;
            buffer2[0x1f] = 0x9b;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x85;
            buffer4[0] = 0x86;
            buffer4[0] = 0x52;
            buffer4[1] = 0x72;
            buffer4[1] = 0x58;
            buffer4[1] = 0x4e;
            buffer4[1] = 160;
            buffer4[1] = 0x13;
            buffer4[2] = 0x99;
            buffer4[2] = 0x8b;
            buffer4[2] = 0x31;
            buffer4[2] = 0x71;
            buffer4[2] = 0x26;
            buffer4[2] = 0x9a;
            buffer4[3] = 0x87;
            buffer4[3] = 0x79;
            buffer4[3] = 0x11;
            buffer4[4] = 0x68;
            buffer4[4] = 0x93;
            buffer4[4] = 0x73;
            buffer4[4] = 0x6a;
            buffer4[4] = 0x7e;
            buffer4[4] = 4;
            buffer4[5] = 0x55;
            buffer4[5] = 0x56;
            buffer4[5] = 0x7b;
            buffer4[5] = 0xb9;
            buffer4[6] = 0x59;
            buffer4[6] = 0xa9;
            buffer4[6] = 140;
            buffer4[6] = 0x71;
            buffer4[6] = 0x83;
            buffer4[7] = 0x5c;
            buffer4[7] = 0x83;
            buffer4[7] = 140;
            buffer4[7] = 0x41;
            buffer4[8] = 0x94;
            buffer4[8] = 0x95;
            buffer4[8] = 0xb2;
            buffer4[9] = 0x98;
            buffer4[9] = 0x61;
            buffer4[9] = 0x6f;
            buffer4[9] = 0x7b;
            buffer4[9] = 0x80;
            buffer4[9] = 0x66;
            buffer4[10] = 0x29;
            buffer4[10] = 0x6c;
            buffer4[10] = 0x57;
            buffer4[10] = 130;
            buffer4[10] = 0xac;
            buffer4[11] = 0x9e;
            buffer4[11] = 130;
            buffer4[11] = 0xa4;
            buffer4[11] = 0x87;
            buffer4[11] = 0x47;
            buffer4[12] = 0x69;
            buffer4[12] = 170;
            buffer4[12] = 0x98;
            buffer4[12] = 0x98;
            buffer4[12] = 0x6a;
            buffer4[13] = 0x84;
            buffer4[13] = 0x40;
            buffer4[13] = 0x71;
            buffer4[13] = 0xe3;
            buffer4[14] = 0x95;
            buffer4[14] = 0x45;
            buffer4[14] = 0x2f;
            buffer4[14] = 0x57;
            buffer4[14] = 0x72;
            buffer4[14] = 0x34;
            buffer4[15] = 0x93;
            buffer4[15] = 120;
            buffer4[15] = 0xa6;
            buffer4[15] = 60;
            byte[] rgbIV = buffer4;
            byte[] publicKeyToken = typeof(Class39).Assembly.GetName().GetPublicKeyToken();
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
            byte_2 = smethod_18(smethod_17(typeof(Class39).Assembly).ToString());
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

    [Attribute2(typeof(Attribute2.Class40<object>[]))]
    /* private scope */ static string smethod_11(int int_5)
    {
        if (byte_1.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class39).Assembly.GetManifestResourceStream("\x0094\x008f1ysb\x0087\x009f\x0094r\x009f3ktr\x0096v4.9w\x00992w\x009d\x0088kev299iwb05")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer3 = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer4 = new byte[0x20];
            buffer4[0] = 0x91;
            buffer4[0] = 0x67;
            buffer4[0] = 0x58;
            buffer4[1] = 0x81;
            buffer4[1] = 0x76;
            buffer4[1] = 0xf2;
            buffer4[2] = 0x79;
            buffer4[2] = 0x90;
            buffer4[2] = 0xce;
            buffer4[2] = 0x38;
            buffer4[3] = 0xdd;
            buffer4[3] = 0xdb;
            buffer4[3] = 0xde;
            buffer4[4] = 0x9f;
            buffer4[4] = 0xa1;
            buffer4[4] = 0xbc;
            buffer4[5] = 0x7b;
            buffer4[5] = 0x16;
            buffer4[5] = 0x8f;
            buffer4[5] = 0x49;
            buffer4[6] = 0x59;
            buffer4[6] = 0x71;
            buffer4[6] = 0x5b;
            buffer4[6] = 0xa2;
            buffer4[6] = 0xc3;
            buffer4[7] = 0x6c;
            buffer4[7] = 0x3a;
            buffer4[7] = 0xa4;
            buffer4[7] = 0x6d;
            buffer4[7] = 0x80;
            buffer4[7] = 0x60;
            buffer4[8] = 0x62;
            buffer4[8] = 0x8d;
            buffer4[8] = 130;
            buffer4[8] = 0xb1;
            buffer4[8] = 0x88;
            buffer4[8] = 170;
            buffer4[9] = 0x88;
            buffer4[9] = 0x87;
            buffer4[9] = 0x6c;
            buffer4[9] = 0x97;
            buffer4[9] = 0x2c;
            buffer4[9] = 0x7e;
            buffer4[10] = 0x9d;
            buffer4[10] = 0x84;
            buffer4[10] = 0x5d;
            buffer4[10] = 0x2d;
            buffer4[10] = 0x2f;
            buffer4[11] = 0x58;
            buffer4[11] = 0x9a;
            buffer4[11] = 0x39;
            buffer4[12] = 0x88;
            buffer4[12] = 0xaf;
            buffer4[12] = 0x6f;
            buffer4[12] = 2;
            buffer4[13] = 0xa3;
            buffer4[13] = 170;
            buffer4[13] = 0x15;
            buffer4[13] = 180;
            buffer4[14] = 0x9c;
            buffer4[14] = 0xa4;
            buffer4[14] = 0x88;
            buffer4[14] = 0xdb;
            buffer4[14] = 0x7a;
            buffer4[14] = 0xbd;
            buffer4[15] = 0x7a;
            buffer4[15] = 0x3b;
            buffer4[15] = 0x7c;
            buffer4[15] = 0x6b;
            buffer4[15] = 0x92;
            buffer4[15] = 0x30;
            buffer4[0x10] = 0xb0;
            buffer4[0x10] = 0x31;
            buffer4[0x10] = 0x7a;
            buffer4[0x10] = 0xd7;
            buffer4[0x11] = 0x63;
            buffer4[0x11] = 0x72;
            buffer4[0x11] = 0xec;
            buffer4[0x11] = 0x8a;
            buffer4[0x11] = 0x23;
            buffer4[0x12] = 0x62;
            buffer4[0x12] = 0x8f;
            buffer4[0x12] = 0x21;
            buffer4[0x12] = 0x4f;
            buffer4[0x13] = 0x88;
            buffer4[0x13] = 0x60;
            buffer4[0x13] = 0x34;
            buffer4[0x13] = 0x71;
            buffer4[0x13] = 0xa7;
            buffer4[0x13] = 0x9a;
            buffer4[20] = 0x65;
            buffer4[20] = 0x63;
            buffer4[20] = 0x58;
            buffer4[20] = 0x4d;
            buffer4[0x15] = 0x55;
            buffer4[0x15] = 0x5e;
            buffer4[0x15] = 0xfc;
            buffer4[0x16] = 100;
            buffer4[0x16] = 0xa8;
            buffer4[0x16] = 170;
            buffer4[0x17] = 0x6b;
            buffer4[0x17] = 0xb3;
            buffer4[0x17] = 0x99;
            buffer4[0x17] = 0x5c;
            buffer4[0x17] = 0x77;
            buffer4[0x17] = 0x49;
            buffer4[0x18] = 0xba;
            buffer4[0x18] = 0xa2;
            buffer4[0x18] = 0x76;
            buffer4[0x18] = 0x43;
            buffer4[0x19] = 0x5f;
            buffer4[0x19] = 0xb6;
            buffer4[0x19] = 0x71;
            buffer4[0x19] = 0x1d;
            buffer4[0x19] = 0xa9;
            buffer4[0x1a] = 0xd1;
            buffer4[0x1a] = 0x86;
            buffer4[0x1a] = 60;
            buffer4[0x1a] = 130;
            buffer4[0x1a] = 0x70;
            buffer4[0x1b] = 0x79;
            buffer4[0x1b] = 200;
            buffer4[0x1b] = 0xaf;
            buffer4[0x1c] = 0x56;
            buffer4[0x1c] = 0x5e;
            buffer4[0x1c] = 0x7a;
            buffer4[0x1d] = 0x75;
            buffer4[0x1d] = 170;
            buffer4[0x1d] = 0x61;
            buffer4[0x1d] = 0xaf;
            buffer4[0x1d] = 0x84;
            buffer4[0x1d] = 0x24;
            buffer4[30] = 0xa7;
            buffer4[30] = 130;
            buffer4[30] = 0xbd;
            buffer4[0x1f] = 0x90;
            buffer4[0x1f] = 0x7d;
            buffer4[0x1f] = 0x5c;
            buffer4[0x1f] = 0x56;
            byte[] rgbKey = buffer4;
            byte[] buffer5 = new byte[0x10];
            buffer5[0] = 0x9a;
            buffer5[0] = 0x65;
            buffer5[0] = 0xa2;
            buffer5[1] = 0x92;
            buffer5[1] = 0x86;
            buffer5[1] = 0x5e;
            buffer5[2] = 0x79;
            buffer5[2] = 0xb0;
            buffer5[2] = 0x88;
            buffer5[2] = 0xdd;
            buffer5[2] = 0xf8;
            buffer5[3] = 0x6a;
            buffer5[3] = 0x73;
            buffer5[3] = 0x5e;
            buffer5[3] = 0x80;
            buffer5[3] = 0x9f;
            buffer5[3] = 6;
            buffer5[4] = 0x6a;
            buffer5[4] = 0xa1;
            buffer5[4] = 0x6f;
            buffer5[4] = 0x52;
            buffer5[4] = 0x48;
            buffer5[5] = 0x6f;
            buffer5[5] = 0x90;
            buffer5[5] = 0x4a;
            buffer5[5] = 0x6b;
            buffer5[6] = 0x76;
            buffer5[6] = 0x9e;
            buffer5[6] = 0x90;
            buffer5[6] = 0x54;
            buffer5[6] = 130;
            buffer5[6] = 0x53;
            buffer5[7] = 0xba;
            buffer5[7] = 0x74;
            buffer5[7] = 0x67;
            buffer5[7] = 0x53;
            buffer5[7] = 0x61;
            buffer5[7] = 230;
            buffer5[8] = 0x97;
            buffer5[8] = 0x5c;
            buffer5[8] = 0x84;
            buffer5[8] = 0xa5;
            buffer5[8] = 0x91;
            buffer5[8] = 0xcd;
            buffer5[9] = 100;
            buffer5[9] = 0x61;
            buffer5[9] = 140;
            buffer5[9] = 220;
            buffer5[10] = 0x58;
            buffer5[10] = 0x9a;
            buffer5[10] = 0x49;
            buffer5[10] = 150;
            buffer5[10] = 0x80;
            buffer5[10] = 150;
            buffer5[11] = 0x74;
            buffer5[11] = 0x9c;
            buffer5[11] = 0x11;
            buffer5[11] = 0x3d;
            buffer5[11] = 0x7b;
            buffer5[12] = 0x54;
            buffer5[12] = 0xa7;
            buffer5[12] = 0xa7;
            buffer5[12] = 0x71;
            buffer5[12] = 0x92;
            buffer5[13] = 0x6c;
            buffer5[13] = 0xa4;
            buffer5[13] = 0x6c;
            buffer5[13] = 160;
            buffer5[13] = 0x2e;
            buffer5[14] = 0x85;
            buffer5[14] = 40;
            buffer5[14] = 0x77;
            buffer5[14] = 0x81;
            buffer5[14] = 0x40;
            buffer5[15] = 0x41;
            buffer5[15] = 0x91;
            buffer5[15] = 0x98;
            buffer5[15] = 8;
            byte[] array = buffer5;
            Array.Reverse(array);
            byte[] publicKeyToken = typeof(Class39).Assembly.GetName().GetPublicKeyToken();
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

    [Attribute2(typeof(Attribute2.Class40<object>[]))]
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
            Struct45 struct2 = (Struct45) obj2;
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
            if ((uint_1 == 0xcea1d7d) && !bool_4)
            {
                bool_4 = true;
                return num2;
            }
        }
        return delegate14_1(intptr_3, intptr_4, intptr_5, uint_1, intptr_6, ref uint_2);
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
        IEnumerator enumerator;
        BinaryReader reader;
        if (bool_2)
        {
            return;
        }
        bool_2 = true;
        long num11 = 0L;
        Marshal.ReadIntPtr(new IntPtr((void*) &num11), 0);
        Marshal.ReadInt32(new IntPtr((void*) &num11), 0);
        Marshal.ReadInt64(new IntPtr((void*) &num11), 0);
        Marshal.WriteIntPtr(new IntPtr((void*) &num11), 0, IntPtr.Zero);
        Marshal.WriteInt32(new IntPtr((void*) &num11), 0, 0);
        Marshal.WriteInt64(new IntPtr((void*) &num11), 0, 0L);
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
                bool_6 = true;
            }
        }
    Label_01C3:
        reader = new BinaryReader(typeof(Class39).Assembly.GetManifestResourceStream("x4kvt0\x008d\x009c\x009bwve\x0090\x009bv\x0089i4.\x008a\x0099pg\x008a\x008b\x008e3\x00874\x008ddoch\x009f8\x009d")) {
            BaseStream = { Position = 0L }
        };
        byte[] buffer6 = reader.ReadBytes((int) reader.BaseStream.Length);
        byte[] buffer7 = new byte[0x20];
        buffer7[0] = 0x63;
        buffer7[0] = 0x75;
        buffer7[0] = 0xa4;
        buffer7[0] = 0x9a;
        buffer7[0] = 0x7b;
        buffer7[0] = 0x92;
        buffer7[1] = 0x80;
        buffer7[1] = 0x6d;
        buffer7[1] = 0x59;
        buffer7[1] = 0x72;
        buffer7[1] = 0x54;
        buffer7[1] = 0x3f;
        buffer7[2] = 0x6b;
        buffer7[2] = 0x54;
        buffer7[2] = 120;
        buffer7[3] = 0x9e;
        buffer7[3] = 0x6d;
        buffer7[3] = 0x37;
        buffer7[3] = 0x5f;
        buffer7[3] = 0xcd;
        buffer7[4] = 0x8d;
        buffer7[4] = 0xa2;
        buffer7[4] = 0x8e;
        buffer7[4] = 0x76;
        buffer7[4] = 0x83;
        buffer7[4] = 0x2f;
        buffer7[5] = 90;
        buffer7[5] = 0x83;
        buffer7[5] = 0x7c;
        buffer7[5] = 0x7d;
        buffer7[5] = 0x8e;
        buffer7[6] = 0x25;
        buffer7[6] = 0xca;
        buffer7[6] = 1;
        buffer7[7] = 0x79;
        buffer7[7] = 0xab;
        buffer7[7] = 0xa3;
        buffer7[7] = 0xe1;
        buffer7[8] = 0x59;
        buffer7[8] = 0x79;
        buffer7[8] = 0xf5;
        buffer7[9] = 0xb2;
        buffer7[9] = 0xa3;
        buffer7[9] = 0x8b;
        buffer7[9] = 0x56;
        buffer7[9] = 0xa1;
        buffer7[9] = 150;
        buffer7[10] = 0x7b;
        buffer7[10] = 0x67;
        buffer7[10] = 160;
        buffer7[10] = 0x4b;
        buffer7[10] = 240;
        buffer7[11] = 0x7a;
        buffer7[11] = 0x8f;
        buffer7[11] = 0xdd;
        buffer7[11] = 0x71;
        buffer7[11] = 0x56;
        buffer7[12] = 0x48;
        buffer7[12] = 0x3a;
        buffer7[12] = 0x56;
        buffer7[12] = 140;
        buffer7[12] = 0xbf;
        buffer7[13] = 0x9a;
        buffer7[13] = 0xb1;
        buffer7[13] = 0xbd;
        buffer7[13] = 100;
        buffer7[13] = 0x76;
        buffer7[13] = 0xf2;
        buffer7[14] = 90;
        buffer7[14] = 0x7a;
        buffer7[14] = 0x2d;
        buffer7[15] = 0xa8;
        buffer7[15] = 0x8a;
        buffer7[15] = 0x9c;
        buffer7[15] = 0x58;
        buffer7[15] = 0x88;
        buffer7[15] = 0x41;
        buffer7[0x10] = 130;
        buffer7[0x10] = 0xa4;
        buffer7[0x10] = 0x35;
        buffer7[0x10] = 150;
        buffer7[0x10] = 0x7d;
        buffer7[0x10] = 0x86;
        buffer7[0x11] = 0x59;
        buffer7[0x11] = 0x94;
        buffer7[0x11] = 160;
        buffer7[0x11] = 0x87;
        buffer7[0x11] = 0x29;
        buffer7[0x12] = 0xa2;
        buffer7[0x12] = 0x87;
        buffer7[0x12] = 0x7e;
        buffer7[0x12] = 0xe1;
        buffer7[0x12] = 0x15;
        buffer7[0x13] = 0x54;
        buffer7[0x13] = 0xa3;
        buffer7[0x13] = 0x54;
        buffer7[0x13] = 0x4a;
        buffer7[0x13] = 0x9a;
        buffer7[0x13] = 0x73;
        buffer7[20] = 0x5b;
        buffer7[20] = 0xbd;
        buffer7[20] = 0x97;
        buffer7[20] = 8;
        buffer7[0x15] = 0x61;
        buffer7[0x15] = 0x5c;
        buffer7[0x15] = 0x85;
        buffer7[0x15] = 0x68;
        buffer7[0x15] = 0x38;
        buffer7[0x16] = 0xa8;
        buffer7[0x16] = 0xb9;
        buffer7[0x16] = 0x88;
        buffer7[0x16] = 0x81;
        buffer7[0x17] = 0x9a;
        buffer7[0x17] = 1;
        buffer7[0x17] = 0x37;
        buffer7[0x17] = 0x1d;
        buffer7[0x17] = 160;
        buffer7[0x18] = 0x74;
        buffer7[0x18] = 0x89;
        buffer7[0x18] = 0x8e;
        buffer7[0x18] = 0x8f;
        buffer7[0x19] = 0x70;
        buffer7[0x19] = 0x8d;
        buffer7[0x19] = 0x1f;
        buffer7[0x19] = 0x61;
        buffer7[0x1a] = 0x7c;
        buffer7[0x1a] = 0x5f;
        buffer7[0x1a] = 0xe5;
        buffer7[0x1b] = 0xa8;
        buffer7[0x1b] = 0x7f;
        buffer7[0x1b] = 8;
        buffer7[0x1c] = 0x3e;
        buffer7[0x1c] = 0xb2;
        buffer7[0x1c] = 0x9b;
        buffer7[0x1c] = 0xc1;
        buffer7[0x1d] = 0x2f;
        buffer7[0x1d] = 0x7a;
        buffer7[0x1d] = 0xa3;
        buffer7[0x1d] = 0x66;
        buffer7[0x1d] = 0xec;
        buffer7[30] = 0x1d;
        buffer7[30] = 110;
        buffer7[30] = 0x53;
        buffer7[30] = 0xa8;
        buffer7[30] = 0xdb;
        buffer7[0x1f] = 0x15;
        buffer7[0x1f] = 0xa4;
        buffer7[0x1f] = 0x1a;
        buffer7[0x1f] = 0xcc;
        buffer7[0x1f] = 0x99;
        buffer7[0x1f] = 0x17;
        byte[] rgbKey = buffer7;
        byte[] buffer9 = new byte[0x10];
        buffer9[0] = 0x66;
        buffer9[0] = 150;
        buffer9[0] = 0;
        buffer9[1] = 0xac;
        buffer9[1] = 0x95;
        buffer9[1] = 0x9c;
        buffer9[1] = 130;
        buffer9[1] = 0x65;
        buffer9[2] = 0x56;
        buffer9[2] = 0x72;
        buffer9[2] = 0x54;
        buffer9[2] = 100;
        buffer9[2] = 0x56;
        buffer9[2] = 80;
        buffer9[3] = 0x72;
        buffer9[3] = 0xd4;
        buffer9[3] = 0xf9;
        buffer9[4] = 0x71;
        buffer9[4] = 0x7d;
        buffer9[4] = 0xe3;
        buffer9[5] = 0x80;
        buffer9[5] = 130;
        buffer9[5] = 0x8e;
        buffer9[5] = 0x66;
        buffer9[5] = 0xa8;
        buffer9[5] = 0xa8;
        buffer9[6] = 0xc0;
        buffer9[6] = 0x5c;
        buffer9[6] = 0x83;
        buffer9[6] = 0x41;
        buffer9[6] = 0x52;
        buffer9[7] = 0x5c;
        buffer9[7] = 7;
        buffer9[7] = 0x94;
        buffer9[7] = 0x59;
        buffer9[8] = 0x85;
        buffer9[8] = 0x62;
        buffer9[8] = 0x8e;
        buffer9[9] = 0x88;
        buffer9[9] = 0x59;
        buffer9[9] = 0x9d;
        buffer9[9] = 0x75;
        buffer9[10] = 0xb0;
        buffer9[10] = 0x86;
        buffer9[10] = 0xa7;
        buffer9[10] = 0x67;
        buffer9[10] = 0x73;
        buffer9[10] = 100;
        buffer9[11] = 0xa6;
        buffer9[11] = 0x84;
        buffer9[11] = 0x7c;
        buffer9[11] = 0xec;
        buffer9[12] = 90;
        buffer9[12] = 0xa1;
        buffer9[12] = 0xa6;
        buffer9[12] = 0x73;
        buffer9[12] = 0x81;
        buffer9[12] = 150;
        buffer9[13] = 0x7a;
        buffer9[13] = 0x4f;
        buffer9[13] = 0xc7;
        buffer9[13] = 0x9a;
        buffer9[13] = 160;
        buffer9[13] = 0x9a;
        buffer9[14] = 0xf1;
        buffer9[14] = 0xae;
        buffer9[14] = 0x5c;
        buffer9[14] = 0xba;
        buffer9[15] = 0x70;
        buffer9[15] = 0x34;
        buffer9[15] = 0x99;
        buffer9[15] = 180;
        byte[] array = buffer9;
        Array.Reverse(array);
        byte[] publicKeyToken = typeof(Class39).Assembly.GetName().GetPublicKeyToken();
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
        byte[] buffer14 = stream.ToArray();
        Array.Clear(array, 0, array.Length);
        stream.Close();
        stream2.Close();
        reader.Close();
        int num10 = buffer14.Length / 8;
        if (((source = buffer14) != null) && (source.Length != 0))
        {
            numRef = source;
            goto Label_0D4A;
        }
        fixed (byte* numRef = null)
        {
            int num9;
        Label_0D4A:
            num9 = 0;
            while (num9 < num10)
            {
                IntPtr ptr1 = (IntPtr) (numRef + (num9 * 8));
                ptr1[0] ^= (IntPtr) 0x11d396ceL;
                num9++;
            }
        }
        reader = new BinaryReader(new MemoryStream(buffer14)) {
            BaseStream = { Position = 0L }
        };
        long num3 = Marshal.GetHINSTANCE(typeof(Class39).Assembly.GetModules()[0]).ToInt64();
        int num5 = 0;
        int num4 = 0;
        if ((typeof(Class39).Assembly.Location == null) || (typeof(Class39).Assembly.Location.Length == 0))
        {
            num4 = 0x1c00;
        }
        int num8 = reader.ReadInt32();
        if (reader.ReadInt32() == 1)
        {
            IntPtr ptr7 = IntPtr.Zero;
            Assembly assembly = typeof(Class39).Assembly;
            ptr7 = OpenProcess(0x38, 1, (uint) Process.GetCurrentProcess().Id);
            if (IntPtr.Size == 4)
            {
                int_0 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt32();
            }
            long_0 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt64();
            IntPtr ptr9 = IntPtr.Zero;
            for (int j = 0; j < num8; j++)
            {
                IntPtr ptr8 = new IntPtr((long_0 + reader.ReadInt32()) - num4);
                VirtualProtect_1(ptr8, 4, 4, ref num5);
                if (IntPtr.Size == 4)
                {
                    WriteProcessMemory(ptr7, ptr8, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr9);
                }
                else
                {
                    WriteProcessMemory(ptr7, ptr8, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr9);
                }
                VirtualProtect_1(ptr8, 4, num5, ref num5);
            }
            while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
            {
                int num20 = reader.ReadInt32();
                IntPtr ptr11 = new IntPtr(long_0 + num20);
                int num18 = reader.ReadInt32();
                VirtualProtect_1(ptr11, num18 * 4, 4, ref num5);
                for (int k = 0; k < num18; k++)
                {
                    Marshal.WriteInt32(new IntPtr(ptr11.ToInt64() + (k * 4)), reader.ReadInt32());
                }
                VirtualProtect_1(ptr11, num18 * 4, num5, ref num5);
            }
            CloseHandle(ptr7);
            return;
        }
        for (int i = 0; i < num8; i++)
        {
            IntPtr ptr6 = new IntPtr((num3 + reader.ReadInt32()) - num4);
            VirtualProtect_1(ptr6, 4, 4, ref num5);
            Marshal.WriteInt32(ptr6, reader.ReadInt32());
            VirtualProtect_1(ptr6, 4, num5, ref num5);
        }
        hashtable_0 = new Hashtable(reader.ReadInt32() + 1);
        Struct45 struct2 = new Struct45 {
            byte_0 = new byte[] { 0x2a },
            bool_0 = false
        };
        hashtable_0.Add(0L, struct2);
        bool flag = false;
        while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
        {
            int num13 = reader.ReadInt32() - num4;
            int num14 = reader.ReadInt32();
            flag = false;
            if (num14 >= 0x70000000)
            {
                flag = true;
            }
            int count = reader.ReadInt32();
            byte[] buffer16 = reader.ReadBytes(count);
            Struct45 struct3 = new Struct45 {
                byte_0 = buffer16,
                bool_0 = flag
            };
            hashtable_0.Add(num3 + num13, struct3);
        }
        long_1 = Marshal.GetHINSTANCE(typeof(Class39).Assembly.GetModules()[0]).ToInt64();
        if (IntPtr.Size == 4)
        {
            int_3 = Convert.ToInt32(long_1);
        }
        byte[] bytes = new byte[] { 0x6d, 0x73, 0x63, 0x6f, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
        string str2 = Encoding.UTF8.GetString(bytes);
        IntPtr ptr4 = LoadLibrary(str2);
        if (ptr4 == IntPtr.Zero)
        {
            bytes = new byte[] { 0x63, 0x6c, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
            str2 = Encoding.UTF8.GetString(bytes);
            ptr4 = LoadLibrary(str2);
        }
        byte[] buffer12 = new byte[] { 0x67, 0x65, 0x74, 0x4a, 0x69, 0x74 };
        string str = Encoding.UTF8.GetString(buffer12);
        Delegate15 delegate2 = (Delegate15) smethod_15(GetProcAddress(ptr4, str), typeof(Delegate15));
        IntPtr ptr2 = delegate2();
        long num2 = 0L;
        if (IntPtr.Size == 4)
        {
            num2 = Marshal.ReadInt32(ptr2);
        }
        else
        {
            num2 = Marshal.ReadInt64(ptr2);
        }
        Marshal.ReadIntPtr(ptr2, 0);
        delegate14_0 = new Delegate14(Class39.smethod_13);
        IntPtr zero = IntPtr.Zero;
        zero = Marshal.GetFunctionPointerForDelegate(delegate14_0);
        long num = 0L;
        if (IntPtr.Size == 4)
        {
            num = Marshal.ReadInt32(new IntPtr(num2));
        }
        else
        {
            num = Marshal.ReadInt64(new IntPtr(num2));
        }
        Process currentProcess = Process.GetCurrentProcess();
        try
        {
            foreach (ProcessModule module2 in currentProcess.Modules)
            {
                if (((module2.ModuleName == str2) && ((num < module2.BaseAddress.ToInt64()) || (num > (module2.BaseAddress.ToInt64() + module2.ModuleMemorySize)))) && (typeof(Class39).Assembly.EntryPoint != null))
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
                        goto Label_13E7;
                    }
                }
                goto Label_1406;
            Label_13E7:
                num4 = 0;
            }
        }
        catch
        {
        }
    Label_1406:
        delegate14_1 = null;
        try
        {
            delegate14_1 = (Delegate14) smethod_15(new IntPtr(num), typeof(Delegate14));
        }
        catch
        {
            try
            {
                Delegate delegate3 = smethod_15(new IntPtr(num), typeof(Delegate14));
                delegate14_1 = (Delegate14) Delegate.CreateDelegate(typeof(Delegate14), delegate3.Method);
            }
            catch
            {
            }
        }
        int num15 = 0;
        if (((typeof(Class39).Assembly.EntryPoint == null) || (typeof(Class39).Assembly.EntryPoint.GetParameters().Length != 2)) || ((typeof(Class39).Assembly.Location == null) || (typeof(Class39).Assembly.Location.Length <= 0)))
        {
            try
            {
                ref byte pinned numRef2;
                object obj2 = typeof(Class39).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(typeof(Class39).Assembly.ManifestModule.ModuleHandle);
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
                    delegate14_0(new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), 0xcea1d7d, new IntPtr((void*) numRef2), ref nativeSizeOfCode);
                }
                finally
                {
                    numRef2 = null;
                }
            }
            catch
            {
            }
            RuntimeHelpers.PrepareDelegate(delegate14_1);
            RuntimeHelpers.PrepareMethod(delegate14_1.Method.MethodHandle);
            RuntimeHelpers.PrepareDelegate(delegate14_0);
            RuntimeHelpers.PrepareMethod(delegate14_0.Method.MethodHandle);
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
                buffer5 = BitConverter.GetBytes(intptr_2.ToInt32());
                buffer3 = BitConverter.GetBytes(zero.ToInt32());
                buffer4 = BitConverter.GetBytes(Convert.ToInt32(num));
            }
            else
            {
                buffer5 = BitConverter.GetBytes(intptr_2.ToInt64());
                buffer3 = BitConverter.GetBytes(zero.ToInt64());
                buffer4 = BitConverter.GetBytes(num);
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
            bool_0 = false;
            VirtualProtect_1(new IntPtr(num2), IntPtr.Size, 0x40, ref num15);
            Marshal.WriteIntPtr(new IntPtr(num2), destination);
            VirtualProtect_1(new IntPtr(num2), IntPtr.Size, num15, ref num15);
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

    [Attribute2(typeof(Attribute2.Class40<object>[]))]
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

    [Attribute2(typeof(Attribute2.Class40<object>[]))]
    private static byte[] smethod_19(byte[] byte_4)
    {
        MemoryStream stream = new MemoryStream();
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Key = new byte[] { 
            140, 13, 0x51, 0x57, 0xa2, 0xe9, 0x90, 0x49, 0xe0, 0x4e, 0xd5, 0x29, 0x89, 10, 3, 0xb5, 
            0x8e, 230, 0x10, 0x97, 0xd5, 0x85, 0xc0, 0x7a, 0x87, 0x6a, 0xab, 0x5f, 0x6d, 0xaf, 0x6d, 80
         };
        algorithm.IV = new byte[] { 0xeb, 0x43, 0xad, 0x44, 0x30, 0x20, 0xb2, 0xef, 0x19, 0x1b, 0x16, 0x8d, 0xfe, 0, 0x8e, 0xef };
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
        if (!bool_3)
        {
            smethod_8();
            bool_3 = true;
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

    internal class Attribute2 : Attribute
    {
        [Attribute2(typeof(Class40<object>[]))]
        public Attribute2(object object_0)
        {
            Class42.smethod_0();
        }

        internal class Class40<T>
        {
            public Class40()
            {
                Class42.smethod_0();
            }
        }
    }

    internal class Class41
    {
        public Class41()
        {
            Class42.smethod_0();
        }

        [Attribute2(typeof(Class39.Attribute2.Class40<object>[]))]
        internal static string smethod_0(string string_0, string string_1)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string_0);
            byte[] buffer3 = new byte[] { 
                0x52, 0x66, 0x68, 110, 0x20, 0x4d, 0x18, 0x22, 0x76, 0xb5, 0x33, 0x11, 0x12, 0x33, 12, 0x6d, 
                10, 0x20, 0x4d, 0x18, 0x22, 0x9e, 0xa1, 0x29, 0x61, 0x1c, 0x76, 0xb5, 5, 0x19, 1, 0x58
             };
            byte[] buffer4 = Class39.smethod_9(Encoding.Unicode.GetBytes(string_1));
            MemoryStream stream = new MemoryStream();
            SymmetricAlgorithm algorithm = Class39.smethod_7();
            algorithm.Key = buffer3;
            algorithm.IV = buffer4;
            CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate uint Delegate14(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate IntPtr Delegate15();

    [Flags]
    private enum Enum2
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct45
    {
        internal bool bool_0;
        internal byte[] byte_0;
    }
}

