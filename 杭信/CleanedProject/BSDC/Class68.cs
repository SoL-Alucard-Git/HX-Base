using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal class Class68
{
    private static bool bool_0 = false;
    private static bool bool_1 = false;
    private static bool bool_2 = false;
    private static bool bool_3 = false;
    private static bool bool_4 = false;
    [Attribute6(typeof(Attribute6.Class69<object>[]))]
    private static bool bool_5 = false;
    private static bool bool_6 = false;
    private static byte[] byte_0 = new byte[0];
    private static byte[] byte_1 = new byte[0];
    private static byte[] byte_2 = new byte[0];
    private static byte[] byte_3 = new byte[0];
    internal static Delegate26 delegate26_0 = null;
    internal static Delegate26 delegate26_1 = null;
    internal static Hashtable hashtable_0 = new Hashtable();
    private static int int_0 = 0;
    private static int int_1 = 0;
    private static int int_2 = 1;
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

    [Attribute6(typeof(Attribute6.Class69<object>[]))]
    /* private scope */ static bool smethod_10(int int_5)
    {
        if (byte_2.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class68).Assembly.GetManifestResourceStream("\x009a\x008c\x0095\x0095\x009f7\x008bj\x0095n\x008c\x008f\x0096\x0092wb\x0093\x0087.h\x008bcc4q\x008ci\x008a\x008bixah\x0090qb\x0094")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0xa6;
            buffer2[0] = 0x53;
            buffer2[0] = 0xc6;
            buffer2[1] = 130;
            buffer2[1] = 0x9e;
            buffer2[1] = 0x66;
            buffer2[1] = 120;
            buffer2[1] = 0x8b;
            buffer2[2] = 0x98;
            buffer2[2] = 0x76;
            buffer2[2] = 0x76;
            buffer2[2] = 0x5b;
            buffer2[2] = 150;
            buffer2[2] = 0x80;
            buffer2[3] = 0xae;
            buffer2[3] = 0x31;
            buffer2[3] = 190;
            buffer2[3] = 140;
            buffer2[3] = 0x69;
            buffer2[3] = 0xb5;
            buffer2[4] = 0x5b;
            buffer2[4] = 0x58;
            buffer2[4] = 0x7c;
            buffer2[4] = 0x66;
            buffer2[4] = 190;
            buffer2[4] = 0xca;
            buffer2[5] = 0x76;
            buffer2[5] = 0x74;
            buffer2[5] = 0x88;
            buffer2[5] = 0x56;
            buffer2[6] = 0x6f;
            buffer2[6] = 0x6c;
            buffer2[6] = 0x90;
            buffer2[6] = 0x54;
            buffer2[6] = 0x5d;
            buffer2[7] = 0x71;
            buffer2[7] = 0xa6;
            buffer2[7] = 0xae;
            buffer2[8] = 0x66;
            buffer2[8] = 0xa8;
            buffer2[8] = 0x3b;
            buffer2[9] = 0x74;
            buffer2[9] = 100;
            buffer2[9] = 120;
            buffer2[9] = 130;
            buffer2[9] = 90;
            buffer2[9] = 0x7f;
            buffer2[10] = 0x90;
            buffer2[10] = 0x7d;
            buffer2[10] = 0xc9;
            buffer2[11] = 0x80;
            buffer2[11] = 120;
            buffer2[11] = 0x49;
            buffer2[11] = 0x9e;
            buffer2[11] = 0x86;
            buffer2[11] = 100;
            buffer2[12] = 0x94;
            buffer2[12] = 0x56;
            buffer2[12] = 0x5e;
            buffer2[12] = 0x6c;
            buffer2[12] = 0x9b;
            buffer2[12] = 0x8a;
            buffer2[13] = 0x8d;
            buffer2[13] = 0x5e;
            buffer2[13] = 0x8a;
            buffer2[14] = 0x72;
            buffer2[14] = 0xbc;
            buffer2[14] = 0x9b;
            buffer2[14] = 0xad;
            buffer2[14] = 0x88;
            buffer2[15] = 0x58;
            buffer2[15] = 0xb2;
            buffer2[15] = 0x74;
            buffer2[15] = 0x6a;
            buffer2[0x10] = 0x6d;
            buffer2[0x10] = 0x65;
            buffer2[0x10] = 110;
            buffer2[0x10] = 0xed;
            buffer2[0x10] = 0x94;
            buffer2[0x11] = 0x65;
            buffer2[0x11] = 0xb6;
            buffer2[0x11] = 0x36;
            buffer2[0x11] = 0xd1;
            buffer2[0x12] = 0x97;
            buffer2[0x12] = 0x56;
            buffer2[0x12] = 0xa1;
            buffer2[0x12] = 0x5f;
            buffer2[0x12] = 30;
            buffer2[0x13] = 0x76;
            buffer2[0x13] = 0x9b;
            buffer2[0x13] = 0xa8;
            buffer2[0x13] = 0x77;
            buffer2[0x13] = 0x61;
            buffer2[0x13] = 15;
            buffer2[20] = 0x92;
            buffer2[20] = 0x76;
            buffer2[20] = 0x3a;
            buffer2[20] = 0xdf;
            buffer2[0x15] = 0x98;
            buffer2[0x15] = 0x86;
            buffer2[0x15] = 0x7d;
            buffer2[0x16] = 0x18;
            buffer2[0x16] = 0x74;
            buffer2[0x16] = 0xd7;
            buffer2[0x16] = 0x49;
            buffer2[0x17] = 60;
            buffer2[0x17] = 0x76;
            buffer2[0x17] = 0x72;
            buffer2[0x17] = 0x7e;
            buffer2[0x17] = 0x75;
            buffer2[0x17] = 0x4c;
            buffer2[0x18] = 0x84;
            buffer2[0x18] = 0xde;
            buffer2[0x18] = 0x4e;
            buffer2[0x19] = 0xa3;
            buffer2[0x19] = 0x92;
            buffer2[0x19] = 0x77;
            buffer2[0x19] = 0xbf;
            buffer2[0x1a] = 0x7b;
            buffer2[0x1a] = 0x23;
            buffer2[0x1a] = 0xc0;
            buffer2[0x1a] = 0x8a;
            buffer2[0x1a] = 0xa3;
            buffer2[0x1b] = 130;
            buffer2[0x1b] = 0x36;
            buffer2[0x1b] = 0x94;
            buffer2[0x1c] = 0xb7;
            buffer2[0x1c] = 0xbb;
            buffer2[0x1c] = 0x55;
            buffer2[0x1c] = 0xa2;
            buffer2[0x1c] = 80;
            buffer2[0x1d] = 0x8a;
            buffer2[0x1d] = 0x9c;
            buffer2[0x1d] = 0x5e;
            buffer2[0x1d] = 0x75;
            buffer2[0x1d] = 0x9f;
            buffer2[30] = 0x34;
            buffer2[30] = 0x54;
            buffer2[30] = 0x52;
            buffer2[0x1f] = 0x55;
            buffer2[0x1f] = 0x17;
            buffer2[0x1f] = 150;
            buffer2[0x1f] = 0x60;
            buffer2[0x1f] = 90;
            buffer2[0x1f] = 0xeb;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0xa8;
            buffer4[0] = 0x26;
            buffer4[0] = 0x90;
            buffer4[0] = 0x7a;
            buffer4[0] = 0x90;
            buffer4[1] = 0x7e;
            buffer4[1] = 0x86;
            buffer4[1] = 0x94;
            buffer4[1] = 120;
            buffer4[1] = 0x87;
            buffer4[2] = 0x87;
            buffer4[2] = 0x88;
            buffer4[2] = 0x3d;
            buffer4[3] = 0x9b;
            buffer4[3] = 0x7a;
            buffer4[3] = 0x4a;
            buffer4[3] = 60;
            buffer4[4] = 0x84;
            buffer4[4] = 0xf1;
            buffer4[4] = 0xbc;
            buffer4[4] = 0x8a;
            buffer4[4] = 0x7b;
            buffer4[4] = 200;
            buffer4[5] = 120;
            buffer4[5] = 0x94;
            buffer4[5] = 0x61;
            buffer4[5] = 0x4a;
            buffer4[5] = 9;
            buffer4[6] = 160;
            buffer4[6] = 0x73;
            buffer4[6] = 0x38;
            buffer4[7] = 0xa1;
            buffer4[7] = 0x7e;
            buffer4[7] = 0x58;
            buffer4[7] = 0x6c;
            buffer4[7] = 60;
            buffer4[8] = 0xa5;
            buffer4[8] = 0x84;
            buffer4[8] = 0xa5;
            buffer4[8] = 0xeb;
            buffer4[9] = 140;
            buffer4[9] = 0x74;
            buffer4[9] = 0x4b;
            buffer4[9] = 90;
            buffer4[9] = 2;
            buffer4[10] = 0x27;
            buffer4[10] = 70;
            buffer4[10] = 0xa6;
            buffer4[10] = 0xb3;
            buffer4[11] = 130;
            buffer4[11] = 0x63;
            buffer4[11] = 0x87;
            buffer4[11] = 0x8a;
            buffer4[11] = 0x3d;
            buffer4[11] = 0xe3;
            buffer4[12] = 0x76;
            buffer4[12] = 0x61;
            buffer4[12] = 0x70;
            buffer4[12] = 0x9c;
            buffer4[12] = 0x12;
            buffer4[13] = 0x66;
            buffer4[13] = 0x9c;
            buffer4[13] = 100;
            buffer4[14] = 0x71;
            buffer4[14] = 0x76;
            buffer4[14] = 0xc7;
            buffer4[15] = 0x74;
            buffer4[15] = 0x97;
            buffer4[15] = 100;
            buffer4[15] = 0x44;
            buffer4[15] = 0x93;
            buffer4[15] = 0xb3;
            byte[] rgbIV = buffer4;
            byte[] publicKeyToken = typeof(Class68).Assembly.GetName().GetPublicKeyToken();
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
            byte_3 = smethod_18(smethod_17(typeof(Class68).Assembly).ToString());
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

    [Attribute6(typeof(Attribute6.Class69<object>[]))]
    /* private scope */ static string smethod_11(int int_5)
    {
        if (byte_0.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class68).Assembly.GetManifestResourceStream("\x0096\x009b\x00884rr\x00870\x0095\x009533jgn\x009dh0.b\x0089i\x008d\x0099o\x00886\x0088y\x009cq\x0087b4\x00912r")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer4 = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer5 = new byte[0x20];
            buffer5[0] = 0x76;
            buffer5[0] = 0x90;
            buffer5[0] = 0x68;
            buffer5[0] = 0x3b;
            buffer5[1] = 130;
            buffer5[1] = 0xa4;
            buffer5[1] = 0x4d;
            buffer5[1] = 0xa3;
            buffer5[1] = 0xae;
            buffer5[1] = 0x22;
            buffer5[2] = 0x7f;
            buffer5[2] = 0x79;
            buffer5[2] = 0x7e;
            buffer5[2] = 3;
            buffer5[3] = 0x6f;
            buffer5[3] = 0xbd;
            buffer5[3] = 0xbb;
            buffer5[4] = 0x5e;
            buffer5[4] = 0x83;
            buffer5[4] = 0x79;
            buffer5[4] = 0x73;
            buffer5[5] = 0xa5;
            buffer5[5] = 0xa2;
            buffer5[5] = 0x55;
            buffer5[5] = 0xd8;
            buffer5[5] = 0x8d;
            buffer5[5] = 0xb9;
            buffer5[6] = 0xa4;
            buffer5[6] = 0x80;
            buffer5[6] = 0x66;
            buffer5[7] = 0xa9;
            buffer5[7] = 0x7e;
            buffer5[7] = 0x51;
            buffer5[8] = 0x60;
            buffer5[8] = 0x33;
            buffer5[8] = 0x56;
            buffer5[8] = 0x56;
            buffer5[8] = 0x6b;
            buffer5[9] = 0x57;
            buffer5[9] = 100;
            buffer5[9] = 0x94;
            buffer5[9] = 0xb6;
            buffer5[10] = 0x6c;
            buffer5[10] = 0x80;
            buffer5[10] = 0x5e;
            buffer5[10] = 0xa7;
            buffer5[10] = 130;
            buffer5[10] = 0xea;
            buffer5[11] = 0xf4;
            buffer5[11] = 0x5e;
            buffer5[11] = 50;
            buffer5[11] = 0x73;
            buffer5[11] = 0x7d;
            buffer5[11] = 0x71;
            buffer5[12] = 150;
            buffer5[12] = 7;
            buffer5[12] = 0xa3;
            buffer5[12] = 0x92;
            buffer5[12] = 0x4d;
            buffer5[12] = 0x71;
            buffer5[13] = 0x1d;
            buffer5[13] = 30;
            buffer5[13] = 0x33;
            buffer5[14] = 0x70;
            buffer5[14] = 0x57;
            buffer5[14] = 0x5e;
            buffer5[14] = 0xdf;
            buffer5[15] = 0xc5;
            buffer5[15] = 0x8e;
            buffer5[15] = 0x6d;
            buffer5[15] = 0x61;
            buffer5[15] = 9;
            buffer5[0x10] = 0x18;
            buffer5[0x10] = 0x62;
            buffer5[0x10] = 0x6f;
            buffer5[0x10] = 0x98;
            buffer5[0x10] = 0x97;
            buffer5[0x10] = 0xec;
            buffer5[0x11] = 60;
            buffer5[0x11] = 0x87;
            buffer5[0x11] = 0x74;
            buffer5[0x11] = 0x68;
            buffer5[0x11] = 0x91;
            buffer5[0x11] = 0xeb;
            buffer5[0x12] = 0x9d;
            buffer5[0x12] = 0x89;
            buffer5[0x12] = 170;
            buffer5[0x12] = 0x7f;
            buffer5[0x12] = 15;
            buffer5[0x13] = 0xe4;
            buffer5[0x13] = 100;
            buffer5[0x13] = 90;
            buffer5[0x13] = 0x86;
            buffer5[0x13] = 0x6d;
            buffer5[0x13] = 40;
            buffer5[20] = 15;
            buffer5[20] = 0x62;
            buffer5[20] = 0xa5;
            buffer5[20] = 0x9d;
            buffer5[0x15] = 0x6c;
            buffer5[0x15] = 0x19;
            buffer5[0x15] = 0x59;
            buffer5[0x15] = 0xf9;
            buffer5[0x16] = 0x7a;
            buffer5[0x16] = 0x76;
            buffer5[0x16] = 0x42;
            buffer5[0x17] = 0x9a;
            buffer5[0x17] = 0x81;
            buffer5[0x17] = 0x41;
            buffer5[0x17] = 0x11;
            buffer5[0x18] = 0x43;
            buffer5[0x18] = 120;
            buffer5[0x18] = 100;
            buffer5[0x18] = 0x89;
            buffer5[0x19] = 0x71;
            buffer5[0x19] = 0x6a;
            buffer5[0x19] = 0x8e;
            buffer5[0x19] = 0x1c;
            buffer5[0x1a] = 0x72;
            buffer5[0x1a] = 0x29;
            buffer5[0x1a] = 0xe2;
            buffer5[0x1b] = 0x77;
            buffer5[0x1b] = 0x40;
            buffer5[0x1b] = 0xbf;
            buffer5[0x1b] = 0xd0;
            buffer5[0x1b] = 0x72;
            buffer5[0x1b] = 0x20;
            buffer5[0x1c] = 0x9f;
            buffer5[0x1c] = 0x98;
            buffer5[0x1c] = 0x4f;
            buffer5[0x1c] = 0xf4;
            buffer5[0x1d] = 0x9a;
            buffer5[0x1d] = 0x86;
            buffer5[0x1d] = 0x93;
            buffer5[0x1d] = 0x99;
            buffer5[0x1d] = 140;
            buffer5[0x1d] = 0x34;
            buffer5[30] = 0x58;
            buffer5[30] = 0x60;
            buffer5[30] = 1;
            buffer5[30] = 0x76;
            buffer5[30] = 230;
            buffer5[30] = 240;
            buffer5[0x1f] = 0xb0;
            buffer5[0x1f] = 0x7b;
            buffer5[0x1f] = 0xb2;
            buffer5[0x1f] = 0x6c;
            buffer5[0x1f] = 0x99;
            byte[] rgbKey = buffer5;
            byte[] buffer6 = new byte[0x10];
            buffer6[0] = 0x76;
            buffer6[0] = 0x90;
            buffer6[0] = 0x2d;
            buffer6[0] = 0x76;
            buffer6[1] = 130;
            buffer6[1] = 0xa4;
            buffer6[1] = 0x4d;
            buffer6[1] = 0x9d;
            buffer6[1] = 0x87;
            buffer6[1] = 8;
            buffer6[2] = 0xc0;
            buffer6[2] = 0x70;
            buffer6[2] = 0x60;
            buffer6[2] = 0x6f;
            buffer6[2] = 0xbd;
            buffer6[2] = 0xe8;
            buffer6[3] = 0x5e;
            buffer6[3] = 0x83;
            buffer6[3] = 0x61;
            buffer6[4] = 0xb1;
            buffer6[4] = 0x7b;
            buffer6[4] = 0xb9;
            buffer6[4] = 0x9f;
            buffer6[4] = 0x9c;
            buffer6[4] = 0x35;
            buffer6[5] = 0x58;
            buffer6[5] = 0x7f;
            buffer6[5] = 0x7e;
            buffer6[5] = 0x2b;
            buffer6[6] = 0x60;
            buffer6[6] = 110;
            buffer6[6] = 0xd8;
            buffer6[7] = 0x70;
            buffer6[7] = 0x6b;
            buffer6[7] = 0x61;
            buffer6[7] = 0xb3;
            buffer6[8] = 100;
            buffer6[8] = 0xc9;
            buffer6[8] = 0x53;
            buffer6[8] = 0xd7;
            buffer6[9] = 0x93;
            buffer6[9] = 0xc0;
            buffer6[9] = 130;
            buffer6[9] = 0xc3;
            buffer6[9] = 0x69;
            buffer6[9] = 0xd3;
            buffer6[10] = 0x45;
            buffer6[10] = 0xa4;
            buffer6[10] = 0x7a;
            buffer6[10] = 0xb7;
            buffer6[11] = 150;
            buffer6[11] = 0x58;
            buffer6[11] = 0xbf;
            buffer6[12] = 0x6d;
            buffer6[12] = 0x68;
            buffer6[12] = 0x73;
            buffer6[12] = 0xb6;
            buffer6[13] = 0x59;
            buffer6[13] = 100;
            buffer6[13] = 0x18;
            buffer6[13] = 0x49;
            buffer6[13] = 0x17;
            buffer6[13] = 8;
            buffer6[14] = 0x5e;
            buffer6[14] = 120;
            buffer6[14] = 0xc5;
            buffer6[14] = 0x58;
            buffer6[15] = 0x2f;
            buffer6[15] = 0x7e;
            buffer6[15] = 0xe8;
            byte[] array = buffer6;
            Array.Reverse(array);
            byte[] publicKeyToken = typeof(Class68).Assembly.GetName().GetPublicKeyToken();
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

    [Attribute6(typeof(Attribute6.Class69<object>[]))]
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
            Struct82 struct2 = (Struct82) obj2;
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
            if ((uint_1 == 0xcea1d7d) && !bool_5)
            {
                bool_5 = true;
                return num2;
            }
        }
        return delegate26_1(intptr_3, intptr_4, intptr_5, uint_1, intptr_6, ref uint_2);
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
        long num7 = 0L;
        Marshal.ReadIntPtr(new IntPtr((void*) &num7), 0);
        Marshal.ReadInt32(new IntPtr((void*) &num7), 0);
        Marshal.ReadInt64(new IntPtr((void*) &num7), 0);
        Marshal.WriteIntPtr(new IntPtr((void*) &num7), 0, IntPtr.Zero);
        Marshal.WriteInt32(new IntPtr((void*) &num7), 0, 0);
        Marshal.WriteInt64(new IntPtr((void*) &num7), 0, 0L);
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
                bool_4 = true;
            }
        }
    Label_01C3:
        reader = new BinaryReader(typeof(Class68).Assembly.GetManifestResourceStream("9\x008e\x009e\x00939\x009b\x008dv4\x008a\x009a3\x009a\x0091e\x008d97.\x0092\x009c\x0088\x00941\x009c\x008e\x008fn5a\x009c7o\x008bs\x00879")) {
            BaseStream = { Position = 0L }
        };
        byte[] buffer4 = reader.ReadBytes((int) reader.BaseStream.Length);
        byte[] buffer7 = new byte[0x20];
        buffer7[0] = 0x94;
        buffer7[0] = 0x90;
        buffer7[0] = 110;
        buffer7[0] = 130;
        buffer7[0] = 0x8d;
        buffer7[0] = 15;
        buffer7[1] = 0x6c;
        buffer7[1] = 0xa2;
        buffer7[1] = 0x43;
        buffer7[2] = 0x23;
        buffer7[2] = 210;
        buffer7[2] = 0x5e;
        buffer7[2] = 0x81;
        buffer7[2] = 0x19;
        buffer7[3] = 0x52;
        buffer7[3] = 0x94;
        buffer7[3] = 0x56;
        buffer7[3] = 0x75;
        buffer7[3] = 130;
        buffer7[3] = 0xf4;
        buffer7[4] = 0x10;
        buffer7[4] = 0x56;
        buffer7[4] = 0x4a;
        buffer7[4] = 0xc0;
        buffer7[4] = 0x1a;
        buffer7[4] = 0x86;
        buffer7[5] = 0xb6;
        buffer7[5] = 0x68;
        buffer7[5] = 0x58;
        buffer7[5] = 0xa7;
        buffer7[5] = 90;
        buffer7[6] = 0x6d;
        buffer7[6] = 0x83;
        buffer7[6] = 0x5b;
        buffer7[6] = 0xcb;
        buffer7[6] = 0xee;
        buffer7[7] = 0x92;
        buffer7[7] = 160;
        buffer7[7] = 0x38;
        buffer7[7] = 0x60;
        buffer7[7] = 0xe0;
        buffer7[8] = 0x9b;
        buffer7[8] = 0x9c;
        buffer7[8] = 0x1f;
        buffer7[8] = 0x94;
        buffer7[9] = 0x2c;
        buffer7[9] = 0x66;
        buffer7[9] = 0x55;
        buffer7[9] = 0x4c;
        buffer7[9] = 0x15;
        buffer7[10] = 110;
        buffer7[10] = 0x83;
        buffer7[10] = 0xb9;
        buffer7[10] = 0x55;
        buffer7[10] = 0xad;
        buffer7[11] = 0x7a;
        buffer7[11] = 0x92;
        buffer7[11] = 0x7c;
        buffer7[11] = 0xb7;
        buffer7[12] = 0x9e;
        buffer7[12] = 0x92;
        buffer7[12] = 110;
        buffer7[12] = 0xa6;
        buffer7[12] = 0x61;
        buffer7[13] = 0xd3;
        buffer7[13] = 0x59;
        buffer7[13] = 0x42;
        buffer7[13] = 0x13;
        buffer7[13] = 0x1f;
        buffer7[14] = 0x9e;
        buffer7[14] = 0xd1;
        buffer7[14] = 0x95;
        buffer7[14] = 0x77;
        buffer7[14] = 0x80;
        buffer7[14] = 240;
        buffer7[15] = 0x86;
        buffer7[15] = 0xcf;
        buffer7[15] = 0x66;
        buffer7[15] = 7;
        buffer7[0x10] = 0xa9;
        buffer7[0x10] = 0x98;
        buffer7[0x10] = 0xf7;
        buffer7[0x11] = 0x7a;
        buffer7[0x11] = 0x7b;
        buffer7[0x11] = 0x81;
        buffer7[0x11] = 130;
        buffer7[0x11] = 0xac;
        buffer7[0x12] = 0x55;
        buffer7[0x12] = 0x8f;
        buffer7[0x12] = 0x51;
        buffer7[0x12] = 0x9e;
        buffer7[0x12] = 0;
        buffer7[0x13] = 120;
        buffer7[0x13] = 0x7a;
        buffer7[0x13] = 0xc6;
        buffer7[20] = 0x8f;
        buffer7[20] = 0xa6;
        buffer7[20] = 0x7b;
        buffer7[20] = 0x22;
        buffer7[0x15] = 0x59;
        buffer7[0x15] = 0x7f;
        buffer7[0x15] = 0x72;
        buffer7[0x15] = 0x6b;
        buffer7[0x15] = 0x54;
        buffer7[0x15] = 0x4c;
        buffer7[0x16] = 7;
        buffer7[0x16] = 0x69;
        buffer7[0x16] = 0x83;
        buffer7[0x16] = 0x47;
        buffer7[0x17] = 0x9f;
        buffer7[0x17] = 0xd1;
        buffer7[0x17] = 0x8b;
        buffer7[0x17] = 0xa6;
        buffer7[0x17] = 14;
        buffer7[0x18] = 0x7f;
        buffer7[0x18] = 0x97;
        buffer7[0x18] = 0x93;
        buffer7[0x18] = 0x7b;
        buffer7[0x18] = 0x5e;
        buffer7[0x18] = 0x48;
        buffer7[0x19] = 0x75;
        buffer7[0x19] = 0xb3;
        buffer7[0x19] = 0x6a;
        buffer7[0x19] = 0x9c;
        buffer7[0x19] = 0x6f;
        buffer7[0x19] = 100;
        buffer7[0x1a] = 0x60;
        buffer7[0x1a] = 0x7e;
        buffer7[0x1a] = 0x5c;
        buffer7[0x1a] = 0x1d;
        buffer7[0x1a] = 10;
        buffer7[0x1b] = 0x8e;
        buffer7[0x1b] = 120;
        buffer7[0x1b] = 0x56;
        buffer7[0x1b] = 0x80;
        buffer7[0x1b] = 30;
        buffer7[0x1c] = 0x8a;
        buffer7[0x1c] = 0xa4;
        buffer7[0x1c] = 0x56;
        buffer7[0x1c] = 0x7e;
        buffer7[0x1c] = 0xa4;
        buffer7[0x1c] = 0x4b;
        buffer7[0x1d] = 0x6a;
        buffer7[0x1d] = 0x2f;
        buffer7[0x1d] = 0x1a;
        buffer7[30] = 0xa1;
        buffer7[30] = 0xc1;
        buffer7[30] = 0xa9;
        buffer7[30] = 0x71;
        buffer7[0x1f] = 0x75;
        buffer7[0x1f] = 0xa6;
        buffer7[0x1f] = 0x16;
        buffer7[0x1f] = 0x97;
        buffer7[0x1f] = 0xa2;
        buffer7[0x1f] = 1;
        byte[] rgbKey = buffer7;
        byte[] buffer8 = new byte[0x10];
        buffer8[0] = 0x94;
        buffer8[0] = 0x72;
        buffer8[0] = 0x99;
        buffer8[0] = 130;
        buffer8[0] = 0x7d;
        buffer8[0] = 0xbf;
        buffer8[1] = 0x6d;
        buffer8[1] = 0xb3;
        buffer8[1] = 130;
        buffer8[2] = 0x63;
        buffer8[2] = 210;
        buffer8[2] = 100;
        buffer8[2] = 0x15;
        buffer8[3] = 0x42;
        buffer8[3] = 0x42;
        buffer8[3] = 0x18;
        buffer8[4] = 0x8f;
        buffer8[4] = 0x56;
        buffer8[4] = 0x77;
        buffer8[4] = 0x10;
        buffer8[4] = 0x68;
        buffer8[4] = 0x93;
        buffer8[5] = 0x59;
        buffer8[5] = 0x62;
        buffer8[5] = 0xa4;
        buffer8[5] = 0x1a;
        buffer8[5] = 0x31;
        buffer8[6] = 0xb6;
        buffer8[6] = 0x42;
        buffer8[6] = 0x8b;
        buffer8[6] = 0x83;
        buffer8[6] = 0x7d;
        buffer8[7] = 0x5d;
        buffer8[7] = 0x97;
        buffer8[7] = 0xec;
        buffer8[8] = 0x76;
        buffer8[8] = 0xcb;
        buffer8[8] = 0x1f;
        buffer8[9] = 0xa9;
        buffer8[9] = 0x70;
        buffer8[9] = 0x67;
        buffer8[9] = 0x88;
        buffer8[9] = 0x9f;
        buffer8[9] = 0xf4;
        buffer8[10] = 0x26;
        buffer8[10] = 0x43;
        buffer8[10] = 50;
        buffer8[10] = 0xa2;
        buffer8[10] = 0x75;
        buffer8[10] = 230;
        buffer8[11] = 0x66;
        buffer8[11] = 0x60;
        buffer8[11] = 0x2c;
        buffer8[12] = 110;
        buffer8[12] = 0x83;
        buffer8[12] = 110;
        buffer8[13] = 0xa7;
        buffer8[13] = 0x5c;
        buffer8[13] = 0x7a;
        buffer8[13] = 0x92;
        buffer8[13] = 0x7c;
        buffer8[13] = 0x1b;
        buffer8[14] = 0x9a;
        buffer8[14] = 0xba;
        buffer8[14] = 150;
        buffer8[14] = 230;
        buffer8[15] = 100;
        buffer8[15] = 0x97;
        buffer8[15] = 0x63;
        buffer8[15] = 0x5d;
        buffer8[15] = 0x13;
        buffer8[15] = 0x41;
        byte[] array = buffer8;
        Array.Reverse(array);
        byte[] publicKeyToken = typeof(Class68).Assembly.GetName().GetPublicKeyToken();
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
        stream2.Write(buffer4, 0, buffer4.Length);
        stream2.FlushFinalBlock();
        byte[] buffer5 = stream.ToArray();
        Array.Clear(array, 0, array.Length);
        stream.Close();
        stream2.Close();
        reader.Close();
        int num6 = buffer5.Length / 8;
        if (((source = buffer5) != null) && (source.Length != 0))
        {
            numRef = source;
            goto Label_0D4B;
        }
        fixed (byte* numRef = null)
        {
            int num17;
        Label_0D4B:
            num17 = 0;
            while (num17 < num6)
            {
                IntPtr ptr1 = (IntPtr) (numRef + (num17 * 8));
                ptr1[0] ^= (IntPtr) 0x1c75c3ebL;
                num17++;
            }
        }
        reader = new BinaryReader(new MemoryStream(buffer5)) {
            BaseStream = { Position = 0L }
        };
        long num15 = Marshal.GetHINSTANCE(typeof(Class68).Assembly.GetModules()[0]).ToInt64();
        int num13 = 0;
        int num = 0;
        if ((typeof(Class68).Assembly.Location == null) || (typeof(Class68).Assembly.Location.Length == 0))
        {
            num = 0x1c00;
        }
        int num2 = reader.ReadInt32();
        if (reader.ReadInt32() == 1)
        {
            IntPtr ptr = IntPtr.Zero;
            Assembly assembly = typeof(Class68).Assembly;
            ptr = OpenProcess(0x38, 1, (uint) Process.GetCurrentProcess().Id);
            if (IntPtr.Size == 4)
            {
                int_3 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt32();
            }
            long_0 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt64();
            IntPtr ptr3 = IntPtr.Zero;
            for (int j = 0; j < num2; j++)
            {
                IntPtr ptr2 = new IntPtr((long_0 + reader.ReadInt32()) - num);
                VirtualProtect_1(ptr2, 4, 4, ref num13);
                if (IntPtr.Size == 4)
                {
                    WriteProcessMemory(ptr, ptr2, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr3);
                }
                else
                {
                    WriteProcessMemory(ptr, ptr2, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr3);
                }
                VirtualProtect_1(ptr2, 4, num13, ref num13);
            }
            while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
            {
                int num20 = reader.ReadInt32();
                IntPtr ptr10 = new IntPtr(long_0 + num20);
                int num11 = reader.ReadInt32();
                VirtualProtect_1(ptr10, num11 * 4, 4, ref num13);
                for (int k = 0; k < num11; k++)
                {
                    Marshal.WriteInt32(new IntPtr(ptr10.ToInt64() + (k * 4)), reader.ReadInt32());
                }
                VirtualProtect_1(ptr10, num11 * 4, num13, ref num13);
            }
            CloseHandle(ptr);
            return;
        }
        for (int i = 0; i < num2; i++)
        {
            IntPtr ptr8 = new IntPtr((num15 + reader.ReadInt32()) - num);
            VirtualProtect_1(ptr8, 4, 4, ref num13);
            Marshal.WriteInt32(ptr8, reader.ReadInt32());
            VirtualProtect_1(ptr8, 4, num13, ref num13);
        }
        hashtable_0 = new Hashtable(reader.ReadInt32() + 1);
        Struct82 struct2 = new Struct82 {
            byte_0 = new byte[] { 0x2a },
            bool_0 = false
        };
        hashtable_0.Add(0L, struct2);
        bool flag = false;
        while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
        {
            int num16 = reader.ReadInt32() - num;
            int num19 = reader.ReadInt32();
            flag = false;
            if (num19 >= 0x70000000)
            {
                flag = true;
            }
            int count = reader.ReadInt32();
            byte[] buffer10 = reader.ReadBytes(count);
            Struct82 struct3 = new Struct82 {
                byte_0 = buffer10,
                bool_0 = flag
            };
            hashtable_0.Add(num15 + num16, struct3);
        }
        long_1 = Marshal.GetHINSTANCE(typeof(Class68).Assembly.GetModules()[0]).ToInt64();
        if (IntPtr.Size == 4)
        {
            int_1 = Convert.ToInt32(long_1);
        }
        byte[] bytes = new byte[] { 0x6d, 0x73, 0x63, 0x6f, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
        string str = Encoding.UTF8.GetString(bytes);
        IntPtr ptr4 = LoadLibrary(str);
        if (ptr4 == IntPtr.Zero)
        {
            bytes = new byte[] { 0x63, 0x6c, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
            str = Encoding.UTF8.GetString(bytes);
            ptr4 = LoadLibrary(str);
        }
        byte[] buffer17 = new byte[] { 0x67, 0x65, 0x74, 0x4a, 0x69, 0x74 };
        string str2 = Encoding.UTF8.GetString(buffer17);
        Delegate27 delegate3 = (Delegate27) smethod_15(GetProcAddress(ptr4, str2), typeof(Delegate27));
        IntPtr ptr9 = delegate3();
        long num4 = 0L;
        if (IntPtr.Size == 4)
        {
            num4 = Marshal.ReadInt32(ptr9);
        }
        else
        {
            num4 = Marshal.ReadInt64(ptr9);
        }
        Marshal.ReadIntPtr(ptr9, 0);
        delegate26_0 = new Delegate26(Class68.smethod_13);
        IntPtr zero = IntPtr.Zero;
        zero = Marshal.GetFunctionPointerForDelegate(delegate26_0);
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
            foreach (ProcessModule module2 in currentProcess.Modules)
            {
                if (((module2.ModuleName == str) && ((num5 < module2.BaseAddress.ToInt64()) || (num5 > (module2.BaseAddress.ToInt64() + module2.ModuleMemorySize)))) && (typeof(Class68).Assembly.EntryPoint != null))
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
                        goto Label_13C9;
                    }
                }
                goto Label_13E8;
            Label_13C9:
                num = 0;
            }
        }
        catch
        {
        }
    Label_13E8:
        delegate26_1 = null;
        try
        {
            delegate26_1 = (Delegate26) smethod_15(new IntPtr(num5), typeof(Delegate26));
        }
        catch
        {
            try
            {
                Delegate delegate2 = smethod_15(new IntPtr(num5), typeof(Delegate26));
                delegate26_1 = (Delegate26) Delegate.CreateDelegate(typeof(Delegate26), delegate2.Method);
            }
            catch
            {
            }
        }
        int num9 = 0;
        if (((typeof(Class68).Assembly.EntryPoint == null) || (typeof(Class68).Assembly.EntryPoint.GetParameters().Length != 2)) || ((typeof(Class68).Assembly.Location == null) || (typeof(Class68).Assembly.Location.Length <= 0)))
        {
            try
            {
                ref byte pinned numRef2;
                object obj2 = typeof(Class68).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(typeof(Class68).Assembly.ManifestModule.ModuleHandle);
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
                byte[] buffer16 = stream3.ToArray();
                stream3.Close();
                uint nativeSizeOfCode = 0;
                try
                {
                    if (((source = buffer16) != null) && (source.Length != 0))
                    {
                        numRef2 = source;
                    }
                    else
                    {
                        numRef2 = null;
                    }
                    delegate26_0(new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), new IntPtr((void*) numRef2), 0xcea1d7d, new IntPtr((void*) numRef2), ref nativeSizeOfCode);
                }
                finally
                {
                    numRef2 = null;
                }
            }
            catch
            {
            }
            RuntimeHelpers.PrepareDelegate(delegate26_1);
            RuntimeHelpers.PrepareMethod(delegate26_1.Method.MethodHandle);
            RuntimeHelpers.PrepareDelegate(delegate26_0);
            RuntimeHelpers.PrepareMethod(delegate26_0.Method.MethodHandle);
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
            byte[] buffer11 = buffer15;
            byte[] buffer14 = null;
            byte[] buffer13 = null;
            byte[] buffer12 = null;
            if (IntPtr.Size == 4)
            {
                buffer12 = BitConverter.GetBytes(intptr_2.ToInt32());
                buffer14 = BitConverter.GetBytes(zero.ToInt32());
                buffer13 = BitConverter.GetBytes(Convert.ToInt32(num5));
            }
            else
            {
                buffer12 = BitConverter.GetBytes(intptr_2.ToInt64());
                buffer14 = BitConverter.GetBytes(zero.ToInt64());
                buffer13 = BitConverter.GetBytes(num5);
            }
            if (IntPtr.Size == 4)
            {
                buffer11[9] = buffer12[0];
                buffer11[10] = buffer12[1];
                buffer11[11] = buffer12[2];
                buffer11[12] = buffer12[3];
                buffer11[0x10] = buffer13[0];
                buffer11[0x11] = buffer13[1];
                buffer11[0x12] = buffer13[2];
                buffer11[0x13] = buffer13[3];
                buffer11[0x17] = buffer14[0];
                buffer11[0x18] = buffer14[1];
                buffer11[0x19] = buffer14[2];
                buffer11[0x1a] = buffer14[3];
            }
            else
            {
                buffer11[2] = buffer12[0];
                buffer11[3] = buffer12[1];
                buffer11[4] = buffer12[2];
                buffer11[5] = buffer12[3];
                buffer11[6] = buffer12[4];
                buffer11[7] = buffer12[5];
                buffer11[8] = buffer12[6];
                buffer11[9] = buffer12[7];
                buffer11[0x12] = buffer13[0];
                buffer11[0x13] = buffer13[1];
                buffer11[20] = buffer13[2];
                buffer11[0x15] = buffer13[3];
                buffer11[0x16] = buffer13[4];
                buffer11[0x17] = buffer13[5];
                buffer11[0x18] = buffer13[6];
                buffer11[0x19] = buffer13[7];
                buffer11[30] = buffer14[0];
                buffer11[0x1f] = buffer14[1];
                buffer11[0x20] = buffer14[2];
                buffer11[0x21] = buffer14[3];
                buffer11[0x22] = buffer14[4];
                buffer11[0x23] = buffer14[5];
                buffer11[0x24] = buffer14[6];
                buffer11[0x25] = buffer14[7];
            }
            Marshal.Copy(buffer11, 0, destination, buffer11.Length);
            bool_3 = false;
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

    [Attribute6(typeof(Attribute6.Class69<object>[]))]
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

    [Attribute6(typeof(Attribute6.Class69<object>[]))]
    private static byte[] smethod_19(byte[] byte_4)
    {
        MemoryStream stream = new MemoryStream();
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Key = new byte[] { 
            0x48, 0xb7, 0x9e, 190, 0x2a, 0xd7, 0x57, 0xd5, 9, 0x10, 0xf7, 50, 0xbd, 0x67, 0xe5, 0x7e, 
            0x53, 6, 0x4e, 0x13, 0x58, 0xaf, 0xc0, 0x9a, 210, 220, 0x9b, 0xf3, 0xce, 0xb7, 0x55, 0x97
         };
        algorithm.IV = new byte[] { 0xd4, 0xcf, 0x17, 0xe9, 0x62, 13, 0xbf, 0xc4, 120, 0x35, 0x23, 0x13, 240, 0x7c, 0x74, 0x59 };
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

    internal class Attribute6 : Attribute
    {
        [Attribute6(typeof(Class69<object>[]))]
        public Attribute6(object object_0)
        {
            Class71.smethod_0();
        }

        internal class Class69<T>
        {
            public Class69()
            {
                Class71.smethod_0();
            }
        }
    }

    internal class Class70
    {
        public Class70()
        {
            Class71.smethod_0();
        }

        [Attribute6(typeof(Class68.Attribute6.Class69<object>[]))]
        internal static string smethod_0(string string_0, string string_1)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string_0);
            byte[] buffer3 = new byte[] { 
                0x52, 0x66, 0x68, 110, 0x20, 0x4d, 0x18, 0x22, 0x76, 0xb5, 0x33, 0x11, 0x12, 0x33, 12, 0x6d, 
                10, 0x20, 0x4d, 0x18, 0x22, 0x9e, 0xa1, 0x29, 0x61, 0x1c, 0x76, 0xb5, 5, 0x19, 1, 0x58
             };
            byte[] buffer4 = Class68.smethod_9(Encoding.Unicode.GetBytes(string_1));
            MemoryStream stream = new MemoryStream();
            SymmetricAlgorithm algorithm = Class68.smethod_7();
            algorithm.Key = buffer3;
            algorithm.IV = buffer4;
            CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate uint Delegate26(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate IntPtr Delegate27();

    [Flags]
    private enum Enum6
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct82
    {
        internal bool bool_0;
        internal byte[] byte_0;
    }
}

