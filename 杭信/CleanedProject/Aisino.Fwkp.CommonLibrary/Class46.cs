using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal class Class46
{
    [Attribute3(typeof(Attribute3.Class47<object>[]))]
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
    internal static Delegate17 delegate17_0 = null;
    internal static Delegate17 delegate17_1 = null;
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

    [Attribute3(typeof(Attribute3.Class47<object>[]))]
    /* private scope */ static bool smethod_10(int int_5)
    {
        if (byte_3.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class46).Assembly.GetManifestResourceStream("\x008669\x0086\x008a\x009b\x008bq\x009f0k\x0098\x0086\x008ds\x0092l\x009f.\x009148\x0095o\x0092\x008c8\x00884\x0099\x0096\x008d\x008e\x008c\x0097\x00935")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x87;
            buffer2[0] = 0x61;
            buffer2[0] = 0xa6;
            buffer2[0] = 50;
            buffer2[1] = 0xa5;
            buffer2[1] = 0x9c;
            buffer2[1] = 0x2c;
            buffer2[1] = 0xa5;
            buffer2[1] = 0x30;
            buffer2[2] = 100;
            buffer2[2] = 0x37;
            buffer2[2] = 0xfc;
            buffer2[3] = 0xbc;
            buffer2[3] = 0xa4;
            buffer2[3] = 0x80;
            buffer2[4] = 0xa2;
            buffer2[4] = 0xa6;
            buffer2[4] = 100;
            buffer2[5] = 130;
            buffer2[5] = 0x4a;
            buffer2[5] = 0xc5;
            buffer2[6] = 0x74;
            buffer2[6] = 130;
            buffer2[6] = 130;
            buffer2[7] = 0x9a;
            buffer2[7] = 0x74;
            buffer2[7] = 0x3f;
            buffer2[8] = 0x3d;
            buffer2[8] = 0xa1;
            buffer2[8] = 0x13;
            buffer2[8] = 0x9f;
            buffer2[8] = 2;
            buffer2[9] = 0x9a;
            buffer2[9] = 0x65;
            buffer2[9] = 0xa5;
            buffer2[10] = 0x92;
            buffer2[10] = 0x9b;
            buffer2[10] = 0x72;
            buffer2[10] = 0x71;
            buffer2[10] = 0xac;
            buffer2[10] = 0xd0;
            buffer2[11] = 0xc6;
            buffer2[11] = 0x42;
            buffer2[11] = 0x57;
            buffer2[11] = 130;
            buffer2[12] = 0x92;
            buffer2[12] = 0x80;
            buffer2[12] = 0x63;
            buffer2[12] = 0x37;
            buffer2[12] = 0xb9;
            buffer2[13] = 0x81;
            buffer2[13] = 0x58;
            buffer2[13] = 0x3e;
            buffer2[13] = 0xb1;
            buffer2[13] = 0x27;
            buffer2[14] = 0x59;
            buffer2[14] = 0x7a;
            buffer2[14] = 0x66;
            buffer2[14] = 0x61;
            buffer2[14] = 0x89;
            buffer2[14] = 0x5d;
            buffer2[15] = 0x8d;
            buffer2[15] = 90;
            buffer2[15] = 0x92;
            buffer2[15] = 150;
            buffer2[15] = 240;
            buffer2[0x10] = 9;
            buffer2[0x10] = 0x7e;
            buffer2[0x10] = 0x88;
            buffer2[0x10] = 0xab;
            buffer2[0x10] = 0x7c;
            buffer2[0x10] = 0x75;
            buffer2[0x11] = 0x66;
            buffer2[0x11] = 0x9c;
            buffer2[0x11] = 0x87;
            buffer2[0x11] = 0x61;
            buffer2[0x11] = 0xa6;
            buffer2[0x11] = 0x95;
            buffer2[0x12] = 0xa4;
            buffer2[0x12] = 0xac;
            buffer2[0x12] = 0x6a;
            buffer2[0x12] = 0x29;
            buffer2[0x13] = 0x9d;
            buffer2[0x13] = 0x54;
            buffer2[0x13] = 0x27;
            buffer2[20] = 0x70;
            buffer2[20] = 0xe8;
            buffer2[20] = 0x7d;
            buffer2[20] = 0x8e;
            buffer2[20] = 0x69;
            buffer2[20] = 0x4e;
            buffer2[0x15] = 0x8a;
            buffer2[0x15] = 0x65;
            buffer2[0x15] = 140;
            buffer2[0x16] = 0xb3;
            buffer2[0x16] = 0xcf;
            buffer2[0x16] = 0x9c;
            buffer2[0x16] = 0x51;
            buffer2[0x17] = 0xa6;
            buffer2[0x17] = 220;
            buffer2[0x17] = 0x7c;
            buffer2[0x17] = 0x81;
            buffer2[0x17] = 0xf6;
            buffer2[0x17] = 0x91;
            buffer2[0x18] = 140;
            buffer2[0x18] = 0x74;
            buffer2[0x18] = 0x8e;
            buffer2[0x18] = 110;
            buffer2[0x18] = 0x72;
            buffer2[0x18] = 0x79;
            buffer2[0x19] = 0x94;
            buffer2[0x19] = 0x7c;
            buffer2[0x19] = 0xae;
            buffer2[0x1a] = 0x9d;
            buffer2[0x1a] = 120;
            buffer2[0x1a] = 0x7a;
            buffer2[0x1a] = 0x3f;
            buffer2[0x1a] = 0x2c;
            buffer2[0x1b] = 0x98;
            buffer2[0x1b] = 0xba;
            buffer2[0x1b] = 0x37;
            buffer2[0x1b] = 0xc6;
            buffer2[0x1c] = 140;
            buffer2[0x1c] = 0x74;
            buffer2[0x1c] = 6;
            buffer2[0x1d] = 0x77;
            buffer2[0x1d] = 0x7c;
            buffer2[0x1d] = 0x34;
            buffer2[0x1d] = 0x41;
            buffer2[30] = 0x4f;
            buffer2[30] = 0x67;
            buffer2[30] = 0x3e;
            buffer2[30] = 0x80;
            buffer2[30] = 0x72;
            buffer2[30] = 0x36;
            buffer2[0x1f] = 0x69;
            buffer2[0x1f] = 0x47;
            buffer2[0x1f] = 0x11;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x87;
            buffer4[0] = 0x61;
            buffer4[0] = 0xb8;
            buffer4[0] = 0x9c;
            buffer4[0] = 0xcc;
            buffer4[1] = 0x2c;
            buffer4[1] = 0x7b;
            buffer4[1] = 0xb5;
            buffer4[2] = 0x37;
            buffer4[2] = 0x91;
            buffer4[2] = 0x88;
            buffer4[2] = 0xce;
            buffer4[2] = 0x7f;
            buffer4[2] = 0x20;
            buffer4[3] = 0x8b;
            buffer4[3] = 0x68;
            buffer4[3] = 30;
            buffer4[4] = 0x4a;
            buffer4[4] = 0x81;
            buffer4[4] = 0x9c;
            buffer4[4] = 0x19;
            buffer4[5] = 0x9a;
            buffer4[5] = 0x74;
            buffer4[5] = 0xa6;
            buffer4[5] = 0x3d;
            buffer4[6] = 0xb5;
            buffer4[6] = 0x56;
            buffer4[6] = 0x77;
            buffer4[6] = 0xb1;
            buffer4[6] = 0x2f;
            buffer4[6] = 0xeb;
            buffer4[7] = 0x73;
            buffer4[7] = 0x79;
            buffer4[7] = 0x74;
            buffer4[8] = 0x83;
            buffer4[8] = 0x6f;
            buffer4[8] = 0x7a;
            buffer4[8] = 0xa8;
            buffer4[8] = 0x87;
            buffer4[9] = 0x9f;
            buffer4[9] = 0x39;
            buffer4[9] = 0x89;
            buffer4[10] = 0x6d;
            buffer4[10] = 0x8e;
            buffer4[10] = 0x8a;
            buffer4[10] = 0x73;
            buffer4[10] = 90;
            buffer4[10] = 0xc4;
            buffer4[11] = 120;
            buffer4[11] = 0xa4;
            buffer4[11] = 0x12;
            buffer4[11] = 60;
            buffer4[12] = 0xa6;
            buffer4[12] = 0x43;
            buffer4[12] = 0x59;
            buffer4[12] = 0x60;
            buffer4[12] = 0x54;
            buffer4[12] = 0xac;
            buffer4[13] = 0x61;
            buffer4[13] = 0x89;
            buffer4[13] = 0xac;
            buffer4[14] = 0x36;
            buffer4[14] = 0x5f;
            buffer4[14] = 0x63;
            buffer4[14] = 0x81;
            buffer4[14] = 0x74;
            buffer4[15] = 0x56;
            buffer4[15] = 0x7e;
            buffer4[15] = 0x88;
            buffer4[15] = 0x9c;
            buffer4[15] = 0x4c;
            byte[] rgbIV = buffer4;
            byte[] publicKeyToken = typeof(Class46).Assembly.GetName().GetPublicKeyToken();
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
            byte_2 = smethod_18(smethod_17(typeof(Class46).Assembly).ToString());
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

    [Attribute3(typeof(Attribute3.Class47<object>[]))]
    /* private scope */ static string smethod_11(int int_5)
    {
        if (byte_0.Length == 0)
        {
            BinaryReader reader = new BinaryReader(typeof(Class46).Assembly.GetManifestResourceStream("ql9\x008d\x009b3\x0087prc\x0096k\x008d\x0086\x0087\x0091k\x008f.42\x0093\x0095x6\x0088q7obr\x009e\x008c\x009a\x0094ko")) {
                BaseStream = { Position = 0L }
            };
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
            byte[] buffer2 = new byte[0x20];
            buffer2[0] = 0x74;
            buffer2[0] = 0x5c;
            buffer2[0] = 0xd8;
            buffer2[1] = 0x7d;
            buffer2[1] = 0xd8;
            buffer2[1] = 0xb5;
            buffer2[2] = 0xba;
            buffer2[2] = 0x7b;
            buffer2[2] = 0x70;
            buffer2[2] = 0x13;
            buffer2[3] = 0x83;
            buffer2[3] = 0xc4;
            buffer2[3] = 0x9f;
            buffer2[4] = 0x58;
            buffer2[4] = 0x68;
            buffer2[4] = 0x54;
            buffer2[4] = 90;
            buffer2[4] = 0x3f;
            buffer2[4] = 0xe5;
            buffer2[5] = 0xa7;
            buffer2[5] = 130;
            buffer2[5] = 10;
            buffer2[5] = 0x13;
            buffer2[6] = 0x80;
            buffer2[6] = 0x62;
            buffer2[6] = 0x58;
            buffer2[6] = 0xb5;
            buffer2[6] = 0x9f;
            buffer2[7] = 0x83;
            buffer2[7] = 0xb1;
            buffer2[7] = 0x93;
            buffer2[7] = 0x16;
            buffer2[8] = 0x7b;
            buffer2[8] = 120;
            buffer2[8] = 0xb0;
            buffer2[9] = 0x5b;
            buffer2[9] = 160;
            buffer2[9] = 0x49;
            buffer2[10] = 0x36;
            buffer2[10] = 0xb2;
            buffer2[10] = 0x19;
            buffer2[10] = 0x95;
            buffer2[10] = 0x84;
            buffer2[10] = 0x36;
            buffer2[11] = 0x9c;
            buffer2[11] = 0x83;
            buffer2[11] = 100;
            buffer2[12] = 0x8b;
            buffer2[12] = 100;
            buffer2[12] = 150;
            buffer2[12] = 0x1b;
            buffer2[13] = 0x90;
            buffer2[13] = 0x5f;
            buffer2[13] = 0xae;
            buffer2[13] = 140;
            buffer2[13] = 0x6c;
            buffer2[14] = 0x90;
            buffer2[14] = 100;
            buffer2[14] = 0x7b;
            buffer2[15] = 0x62;
            buffer2[15] = 0x61;
            buffer2[15] = 50;
            buffer2[15] = 150;
            buffer2[0x10] = 0x55;
            buffer2[0x10] = 0xc4;
            buffer2[0x10] = 0x59;
            buffer2[0x10] = 0x5f;
            buffer2[0x10] = 0xc3;
            buffer2[0x11] = 0x8a;
            buffer2[0x11] = 140;
            buffer2[0x11] = 0x87;
            buffer2[0x11] = 110;
            buffer2[0x11] = 0x67;
            buffer2[0x12] = 0x97;
            buffer2[0x12] = 0x85;
            buffer2[0x12] = 0x21;
            buffer2[0x13] = 0xcc;
            buffer2[0x13] = 0x83;
            buffer2[0x13] = 0x84;
            buffer2[0x13] = 0x6b;
            buffer2[0x13] = 140;
            buffer2[0x13] = 0x75;
            buffer2[20] = 0x87;
            buffer2[20] = 0x4a;
            buffer2[20] = 0x87;
            buffer2[20] = 0xfb;
            buffer2[0x15] = 0x54;
            buffer2[0x15] = 0xa9;
            buffer2[0x15] = 0x60;
            buffer2[0x15] = 0x97;
            buffer2[0x15] = 11;
            buffer2[0x16] = 0x79;
            buffer2[0x16] = 0xcd;
            buffer2[0x16] = 90;
            buffer2[0x16] = 0x8d;
            buffer2[0x17] = 0x5d;
            buffer2[0x17] = 60;
            buffer2[0x17] = 0x67;
            buffer2[0x17] = 0x2e;
            buffer2[0x17] = 0x66;
            buffer2[0x17] = 6;
            buffer2[0x18] = 0x5e;
            buffer2[0x18] = 0x7e;
            buffer2[0x18] = 0x9f;
            buffer2[0x18] = 0xbc;
            buffer2[0x18] = 100;
            buffer2[0x19] = 0x6a;
            buffer2[0x19] = 0x53;
            buffer2[0x19] = 0xe0;
            buffer2[0x19] = 0xc7;
            buffer2[0x1a] = 0x85;
            buffer2[0x1a] = 0x58;
            buffer2[0x1a] = 0x63;
            buffer2[0x1a] = 0x5c;
            buffer2[0x1a] = 0x56;
            buffer2[0x1b] = 0x85;
            buffer2[0x1b] = 0x60;
            buffer2[0x1b] = 0x63;
            buffer2[0x1c] = 0x5b;
            buffer2[0x1c] = 130;
            buffer2[0x1c] = 110;
            buffer2[0x1c] = 0xa6;
            buffer2[0x1c] = 0xc4;
            buffer2[0x1d] = 160;
            buffer2[0x1d] = 0x73;
            buffer2[0x1d] = 0x6c;
            buffer2[0x1d] = 0xc4;
            buffer2[0x1d] = 0xab;
            buffer2[30] = 0x97;
            buffer2[30] = 0x66;
            buffer2[30] = 0x17;
            buffer2[30] = 0x7c;
            buffer2[0x1f] = 0x56;
            buffer2[0x1f] = 120;
            buffer2[0x1f] = 140;
            buffer2[0x1f] = 0x8e;
            buffer2[0x1f] = 0xc5;
            byte[] rgbKey = buffer2;
            byte[] buffer4 = new byte[0x10];
            buffer4[0] = 0x74;
            buffer4[0] = 0x56;
            buffer4[0] = 0xa8;
            buffer4[1] = 0x38;
            buffer4[1] = 0xb9;
            buffer4[1] = 0xc5;
            buffer4[2] = 0x89;
            buffer4[2] = 0xb1;
            buffer4[2] = 0x9f;
            buffer4[2] = 0xa4;
            buffer4[3] = 0x87;
            buffer4[3] = 0x9e;
            buffer4[3] = 0x7d;
            buffer4[3] = 0x11;
            buffer4[3] = 120;
            buffer4[3] = 0xca;
            buffer4[4] = 0x70;
            buffer4[4] = 0x60;
            buffer4[4] = 0xca;
            buffer4[4] = 150;
            buffer4[4] = 0xb9;
            buffer4[5] = 0x4c;
            buffer4[5] = 0x60;
            buffer4[5] = 0x33;
            buffer4[5] = 0x58;
            buffer4[5] = 220;
            buffer4[6] = 0x8a;
            buffer4[6] = 0x86;
            buffer4[6] = 0x86;
            buffer4[7] = 0x87;
            buffer4[7] = 0x6d;
            buffer4[7] = 0x34;
            buffer4[8] = 0x48;
            buffer4[8] = 0x92;
            buffer4[8] = 0x6a;
            buffer4[8] = 0xee;
            buffer4[9] = 14;
            buffer4[9] = 160;
            buffer4[9] = 0x8b;
            buffer4[9] = 0x6d;
            buffer4[9] = 0xb2;
            buffer4[9] = 0xda;
            buffer4[10] = 0x54;
            buffer4[10] = 0x95;
            buffer4[10] = 0x6a;
            buffer4[11] = 0x6d;
            buffer4[11] = 0xe2;
            buffer4[11] = 0xf2;
            buffer4[12] = 0xb7;
            buffer4[12] = 0x5c;
            buffer4[12] = 0x99;
            buffer4[12] = 0x6b;
            buffer4[12] = 0x90;
            buffer4[13] = 0x86;
            buffer4[13] = 0xa4;
            buffer4[13] = 0x51;
            buffer4[13] = 0xc0;
            buffer4[13] = 0x4e;
            buffer4[14] = 0x5c;
            buffer4[14] = 0x19;
            buffer4[14] = 0x61;
            buffer4[14] = 0x68;
            buffer4[14] = 0x20;
            buffer4[15] = 0x88;
            buffer4[15] = 0x98;
            buffer4[15] = 140;
            buffer4[15] = 0x88;
            buffer4[15] = 0xda;
            byte[] array = buffer4;
            Array.Reverse(array);
            byte[] publicKeyToken = typeof(Class46).Assembly.GetName().GetPublicKeyToken();
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

    [Attribute3(typeof(Attribute3.Class47<object>[]))]
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
            Struct53 struct2 = (Struct53) obj2;
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
            if ((uint_1 == 0xcea1d7d) && !bool_0)
            {
                bool_0 = true;
                return num2;
            }
        }
        return delegate17_0(intptr_3, intptr_4, intptr_5, uint_1, intptr_6, ref uint_2);
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
                bool_4 = true;
            }
        }
    Label_01C3:
        reader = new BinaryReader(typeof(Class46).Assembly.GetManifestResourceStream("nb7\x009d\x0099j\x008d33ew\x008d\x008cnr\x0091\x008eo.up0\x00864m\x008e\x008dby\x009c\x009e\x008dx\x0093n\x0099s")) {
            BaseStream = { Position = 0L }
        };
        byte[] buffer15 = reader.ReadBytes((int) reader.BaseStream.Length);
        byte[] buffer16 = new byte[0x20];
        buffer16[0] = 0x66;
        buffer16[0] = 0x6c;
        buffer16[0] = 0x87;
        buffer16[0] = 200;
        buffer16[1] = 0x2d;
        buffer16[1] = 0x77;
        buffer16[1] = 0x31;
        buffer16[2] = 0x8f;
        buffer16[2] = 0x34;
        buffer16[2] = 0xc3;
        buffer16[2] = 0x4f;
        buffer16[3] = 0xa6;
        buffer16[3] = 0xe1;
        buffer16[3] = 0x54;
        buffer16[3] = 0x7c;
        buffer16[4] = 0x2a;
        buffer16[4] = 0x83;
        buffer16[4] = 0x95;
        buffer16[5] = 0x85;
        buffer16[5] = 0x98;
        buffer16[5] = 0x63;
        buffer16[6] = 0x93;
        buffer16[6] = 0x5d;
        buffer16[6] = 0x69;
        buffer16[6] = 0x98;
        buffer16[7] = 0xa3;
        buffer16[7] = 0x70;
        buffer16[7] = 0x92;
        buffer16[7] = 0x75;
        buffer16[7] = 0x25;
        buffer16[8] = 90;
        buffer16[8] = 0x54;
        buffer16[8] = 0xa7;
        buffer16[8] = 0xa4;
        buffer16[8] = 0xfd;
        buffer16[9] = 0xa6;
        buffer16[9] = 0x93;
        buffer16[9] = 0x9a;
        buffer16[9] = 0x5b;
        buffer16[9] = 110;
        buffer16[9] = 0xf7;
        buffer16[10] = 0x86;
        buffer16[10] = 0x6c;
        buffer16[10] = 110;
        buffer16[10] = 0x85;
        buffer16[10] = 0x23;
        buffer16[11] = 13;
        buffer16[11] = 0xa5;
        buffer16[11] = 0xba;
        buffer16[12] = 0x72;
        buffer16[12] = 0x69;
        buffer16[12] = 0x79;
        buffer16[12] = 0x5f;
        buffer16[12] = 0x4e;
        buffer16[12] = 0xed;
        buffer16[13] = 0x8e;
        buffer16[13] = 0x67;
        buffer16[13] = 0x69;
        buffer16[14] = 0x9f;
        buffer16[14] = 0x59;
        buffer16[14] = 0xac;
        buffer16[14] = 0x90;
        buffer16[15] = 130;
        buffer16[15] = 0x20;
        buffer16[15] = 180;
        buffer16[15] = 0x60;
        buffer16[15] = 0x45;
        buffer16[0x10] = 0xa8;
        buffer16[0x10] = 0x80;
        buffer16[0x10] = 0x84;
        buffer16[0x10] = 0x7e;
        buffer16[0x10] = 0xc5;
        buffer16[0x11] = 0x76;
        buffer16[0x11] = 0x74;
        buffer16[0x11] = 0x80;
        buffer16[0x11] = 0x86;
        buffer16[0x12] = 0x86;
        buffer16[0x12] = 0xd1;
        buffer16[0x12] = 0x9e;
        buffer16[0x13] = 0x9a;
        buffer16[0x13] = 0x79;
        buffer16[0x13] = 0x95;
        buffer16[0x13] = 0xcb;
        buffer16[20] = 0xa8;
        buffer16[20] = 0x76;
        buffer16[20] = 0xc0;
        buffer16[20] = 0x6b;
        buffer16[20] = 0xea;
        buffer16[0x15] = 0x7b;
        buffer16[0x15] = 150;
        buffer16[0x15] = 0x98;
        buffer16[0x15] = 0x35;
        buffer16[0x15] = 7;
        buffer16[0x16] = 0x74;
        buffer16[0x16] = 0x75;
        buffer16[0x16] = 0x62;
        buffer16[0x16] = 0x93;
        buffer16[0x16] = 0x61;
        buffer16[0x17] = 0xb9;
        buffer16[0x17] = 100;
        buffer16[0x17] = 0xac;
        buffer16[0x17] = 0x8a;
        buffer16[0x17] = 120;
        buffer16[0x17] = 0x4e;
        buffer16[0x18] = 0x4e;
        buffer16[0x18] = 0x80;
        buffer16[0x18] = 0xa5;
        buffer16[0x18] = 0xc5;
        buffer16[0x19] = 0xbc;
        buffer16[0x19] = 0x84;
        buffer16[0x19] = 0x98;
        buffer16[0x19] = 120;
        buffer16[0x19] = 0x10;
        buffer16[0x1a] = 120;
        buffer16[0x1a] = 0x8a;
        buffer16[0x1a] = 0xba;
        buffer16[0x1b] = 0x81;
        buffer16[0x1b] = 0x74;
        buffer16[0x1b] = 0x71;
        buffer16[0x1b] = 0x86;
        buffer16[0x1b] = 0x2d;
        buffer16[0x1c] = 0x90;
        buffer16[0x1c] = 0x73;
        buffer16[0x1c] = 150;
        buffer16[0x1c] = 0x5b;
        buffer16[0x1c] = 10;
        buffer16[0x1d] = 0xa2;
        buffer16[0x1d] = 0x57;
        buffer16[0x1d] = 0xa8;
        buffer16[30] = 110;
        buffer16[30] = 0x68;
        buffer16[30] = 0x67;
        buffer16[30] = 0x60;
        buffer16[0x1f] = 0x72;
        buffer16[0x1f] = 0x56;
        buffer16[0x1f] = 0x70;
        buffer16[0x1f] = 120;
        buffer16[0x1f] = 0x2d;
        byte[] rgbKey = buffer16;
        byte[] buffer17 = new byte[0x10];
        buffer17[0] = 0x73;
        buffer17[0] = 0x6f;
        buffer17[0] = 0xa6;
        buffer17[0] = 0x73;
        buffer17[0] = 0x26;
        buffer17[0] = 0x22;
        buffer17[1] = 0x98;
        buffer17[1] = 90;
        buffer17[1] = 0xa8;
        buffer17[2] = 0x74;
        buffer17[2] = 0x8b;
        buffer17[2] = 12;
        buffer17[3] = 0x70;
        buffer17[3] = 0x31;
        buffer17[3] = 170;
        buffer17[4] = 0x6b;
        buffer17[4] = 0x60;
        buffer17[4] = 0x58;
        buffer17[5] = 0x6d;
        buffer17[5] = 110;
        buffer17[5] = 0xe0;
        buffer17[6] = 0x9a;
        buffer17[6] = 0x56;
        buffer17[6] = 0x58;
        buffer17[6] = 0x18;
        buffer17[7] = 0x70;
        buffer17[7] = 0x9d;
        buffer17[7] = 0x58;
        buffer17[7] = 160;
        buffer17[7] = 0x42;
        buffer17[8] = 0x60;
        buffer17[8] = 0x8e;
        buffer17[8] = 0x47;
        buffer17[9] = 0x7e;
        buffer17[9] = 0x93;
        buffer17[9] = 0xd9;
        buffer17[9] = 0x3a;
        buffer17[10] = 0x9c;
        buffer17[10] = 160;
        buffer17[10] = 0xe1;
        buffer17[11] = 0x9e;
        buffer17[11] = 0x74;
        buffer17[11] = 140;
        buffer17[12] = 0x5f;
        buffer17[12] = 110;
        buffer17[12] = 0x89;
        buffer17[12] = 0x6c;
        buffer17[12] = 0x92;
        buffer17[12] = 160;
        buffer17[13] = 0x68;
        buffer17[13] = 100;
        buffer17[13] = 0x44;
        buffer17[14] = 0x70;
        buffer17[14] = 0x27;
        buffer17[14] = 0x51;
        buffer17[14] = 6;
        buffer17[14] = 160;
        buffer17[14] = 0xe0;
        buffer17[15] = 0x41;
        buffer17[15] = 0x70;
        buffer17[15] = 0x88;
        buffer17[15] = 0x26;
        byte[] array = buffer17;
        Array.Reverse(array);
        byte[] publicKeyToken = typeof(Class46).Assembly.GetName().GetPublicKeyToken();
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
        stream3.Write(buffer15, 0, buffer15.Length);
        stream3.FlushFinalBlock();
        byte[] buffer9 = stream2.ToArray();
        Array.Clear(array, 0, array.Length);
        stream2.Close();
        stream3.Close();
        reader.Close();
        int num16 = buffer9.Length / 8;
        if (((source = buffer9) != null) && (source.Length != 0))
        {
            numRef2 = source;
            goto Label_0C26;
        }
        fixed (byte* numRef2 = null)
        {
            int num12;
        Label_0C26:
            num12 = 0;
            while (num12 < num16)
            {
                IntPtr ptr1 = (IntPtr) (numRef2 + (num12 * 8));
                ptr1[0] ^= (IntPtr) 0xfbe4a18L;
                num12++;
            }
        }
        reader = new BinaryReader(new MemoryStream(buffer9)) {
            BaseStream = { Position = 0L }
        };
        long num2 = Marshal.GetHINSTANCE(typeof(Class46).Assembly.GetModules()[0]).ToInt64();
        int num4 = 0;
        int num3 = 0;
        if ((typeof(Class46).Assembly.Location == null) || (typeof(Class46).Assembly.Location.Length == 0))
        {
            num3 = 0x1c00;
        }
        int num18 = reader.ReadInt32();
        if (reader.ReadInt32() == 1)
        {
            IntPtr ptr7 = IntPtr.Zero;
            Assembly assembly = typeof(Class46).Assembly;
            ptr7 = OpenProcess(0x38, 1, (uint) Process.GetCurrentProcess().Id);
            if (IntPtr.Size == 4)
            {
                int_3 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt32();
            }
            long_1 = Marshal.GetHINSTANCE(assembly.GetModules()[0]).ToInt64();
            IntPtr ptr11 = IntPtr.Zero;
            for (int j = 0; j < num18; j++)
            {
                IntPtr ptr9 = new IntPtr((long_1 + reader.ReadInt32()) - num3);
                VirtualProtect_1(ptr9, 4, 4, ref num4);
                if (IntPtr.Size == 4)
                {
                    WriteProcessMemory(ptr7, ptr9, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr11);
                }
                else
                {
                    WriteProcessMemory(ptr7, ptr9, BitConverter.GetBytes(reader.ReadInt32()), 4, out ptr11);
                }
                VirtualProtect_1(ptr9, 4, num4, ref num4);
            }
            while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
            {
                int num13 = reader.ReadInt32();
                IntPtr ptr6 = new IntPtr(long_1 + num13);
                int num14 = reader.ReadInt32();
                VirtualProtect_1(ptr6, num14 * 4, 4, ref num4);
                for (int k = 0; k < num14; k++)
                {
                    Marshal.WriteInt32(new IntPtr(ptr6.ToInt64() + (k * 4)), reader.ReadInt32());
                }
                VirtualProtect_1(ptr6, num14 * 4, num4, ref num4);
            }
            CloseHandle(ptr7);
            return;
        }
        for (int i = 0; i < num18; i++)
        {
            IntPtr ptr = new IntPtr((num2 + reader.ReadInt32()) - num3);
            VirtualProtect_1(ptr, 4, 4, ref num4);
            Marshal.WriteInt32(ptr, reader.ReadInt32());
            VirtualProtect_1(ptr, 4, num4, ref num4);
        }
        hashtable_0 = new Hashtable(reader.ReadInt32() + 1);
        Struct53 struct3 = new Struct53 {
            byte_0 = new byte[] { 0x2a },
            bool_0 = false
        };
        hashtable_0.Add(0L, struct3);
        bool flag = false;
        while (reader.BaseStream.Position < (reader.BaseStream.Length - 1L))
        {
            int num8 = reader.ReadInt32() - num3;
            int num9 = reader.ReadInt32();
            flag = false;
            if (num9 >= 0x70000000)
            {
                flag = true;
            }
            int count = reader.ReadInt32();
            byte[] buffer4 = reader.ReadBytes(count);
            Struct53 struct2 = new Struct53 {
                byte_0 = buffer4,
                bool_0 = flag
            };
            hashtable_0.Add(num2 + num8, struct2);
        }
        long_0 = Marshal.GetHINSTANCE(typeof(Class46).Assembly.GetModules()[0]).ToInt64();
        if (IntPtr.Size == 4)
        {
            int_2 = Convert.ToInt32(long_0);
        }
        byte[] bytes = new byte[] { 0x6d, 0x73, 0x63, 0x6f, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
        string str = Encoding.UTF8.GetString(bytes);
        IntPtr ptr8 = LoadLibrary(str);
        if (ptr8 == IntPtr.Zero)
        {
            bytes = new byte[] { 0x63, 0x6c, 0x72, 0x6a, 0x69, 0x74, 0x2e, 100, 0x6c, 0x6c };
            str = Encoding.UTF8.GetString(bytes);
            ptr8 = LoadLibrary(str);
        }
        byte[] buffer11 = new byte[] { 0x67, 0x65, 0x74, 0x4a, 0x69, 0x74 };
        string str2 = Encoding.UTF8.GetString(buffer11);
        Delegate18 delegate3 = (Delegate18) smethod_15(GetProcAddress(ptr8, str2), typeof(Delegate18));
        IntPtr ptr3 = delegate3();
        long num10 = 0L;
        if (IntPtr.Size == 4)
        {
            num10 = Marshal.ReadInt32(ptr3);
        }
        else
        {
            num10 = Marshal.ReadInt64(ptr3);
        }
        Marshal.ReadIntPtr(ptr3, 0);
        delegate17_1 = new Delegate17(Class46.smethod_13);
        IntPtr zero = IntPtr.Zero;
        zero = Marshal.GetFunctionPointerForDelegate(delegate17_1);
        long num6 = 0L;
        if (IntPtr.Size == 4)
        {
            num6 = Marshal.ReadInt32(new IntPtr(num10));
        }
        else
        {
            num6 = Marshal.ReadInt64(new IntPtr(num10));
        }
        Process currentProcess = Process.GetCurrentProcess();
        try
        {
            foreach (ProcessModule module in currentProcess.Modules)
            {
                if (((module.ModuleName == str) && ((num6 < module.BaseAddress.ToInt64()) || (num6 > (module.BaseAddress.ToInt64() + module.ModuleMemorySize)))) && (typeof(Class46).Assembly.EntryPoint != null))
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
                        goto Label_12C3;
                    }
                }
                goto Label_12E2;
            Label_12C3:
                num3 = 0;
            }
        }
        catch
        {
        }
    Label_12E2:
        delegate17_0 = null;
        try
        {
            delegate17_0 = (Delegate17) smethod_15(new IntPtr(num6), typeof(Delegate17));
        }
        catch
        {
            try
            {
                Delegate delegate2 = smethod_15(new IntPtr(num6), typeof(Delegate17));
                delegate17_0 = (Delegate17) Delegate.CreateDelegate(typeof(Delegate17), delegate2.Method);
            }
            catch
            {
            }
        }
        int num11 = 0;
        if (((typeof(Class46).Assembly.EntryPoint == null) || (typeof(Class46).Assembly.EntryPoint.GetParameters().Length != 2)) || ((typeof(Class46).Assembly.Location == null) || (typeof(Class46).Assembly.Location.Length <= 0)))
        {
            try
            {
                ref byte pinned numRef;
                object obj2 = typeof(Class46).Assembly.ManifestModule.ModuleHandle.GetType().GetField("m_ptr", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(typeof(Class46).Assembly.ManifestModule.ModuleHandle);
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
                byte[] buffer = stream.ToArray();
                stream.Close();
                uint nativeSizeOfCode = 0;
                try
                {
                    if (((source = buffer) != null) && (source.Length != 0))
                    {
                        numRef = source;
                    }
                    else
                    {
                        numRef = null;
                    }
                    delegate17_1(new IntPtr((void*) numRef), new IntPtr((void*) numRef), new IntPtr((void*) numRef), 0xcea1d7d, new IntPtr((void*) numRef), ref nativeSizeOfCode);
                }
                finally
                {
                    numRef = null;
                }
            }
            catch
            {
            }
            RuntimeHelpers.PrepareDelegate(delegate17_0);
            RuntimeHelpers.PrepareMethod(delegate17_0.Method.MethodHandle);
            RuntimeHelpers.PrepareDelegate(delegate17_1);
            RuntimeHelpers.PrepareMethod(delegate17_1.Method.MethodHandle);
            byte[] buffer3 = null;
            if (IntPtr.Size != 4)
            {
                buffer3 = new byte[] { 
                    0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0x49, 0x39, 0x40, 8, 0x74, 12, 
                    0x48, 0xb8, 0, 0, 0, 0, 0, 0, 0, 0, 0xff, 0xe0, 0x48, 0xb8, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0xff, 0xe0
                 };
            }
            else
            {
                buffer3 = new byte[] { 
                    0x55, 0x8b, 0xec, 0x8b, 0x45, 0x10, 0x81, 120, 4, 0x7d, 0x1d, 0xea, 12, 0x74, 7, 0xb8, 
                    0xb6, 0xb1, 0x4a, 6, 0xeb, 5, 0xb8, 0xb6, 0x92, 0x40, 12, 0x5d, 0xff, 0xe0
                 };
            }
            IntPtr destination = VirtualAlloc(IntPtr.Zero, (uint) buffer3.Length, 0x1000, 0x40);
            byte[] buffer5 = buffer3;
            byte[] buffer8 = null;
            byte[] buffer7 = null;
            byte[] buffer6 = null;
            if (IntPtr.Size == 4)
            {
                buffer6 = BitConverter.GetBytes(intptr_0.ToInt32());
                buffer8 = BitConverter.GetBytes(zero.ToInt32());
                buffer7 = BitConverter.GetBytes(Convert.ToInt32(num6));
            }
            else
            {
                buffer6 = BitConverter.GetBytes(intptr_0.ToInt64());
                buffer8 = BitConverter.GetBytes(zero.ToInt64());
                buffer7 = BitConverter.GetBytes(num6);
            }
            if (IntPtr.Size == 4)
            {
                buffer5[9] = buffer6[0];
                buffer5[10] = buffer6[1];
                buffer5[11] = buffer6[2];
                buffer5[12] = buffer6[3];
                buffer5[0x10] = buffer7[0];
                buffer5[0x11] = buffer7[1];
                buffer5[0x12] = buffer7[2];
                buffer5[0x13] = buffer7[3];
                buffer5[0x17] = buffer8[0];
                buffer5[0x18] = buffer8[1];
                buffer5[0x19] = buffer8[2];
                buffer5[0x1a] = buffer8[3];
            }
            else
            {
                buffer5[2] = buffer6[0];
                buffer5[3] = buffer6[1];
                buffer5[4] = buffer6[2];
                buffer5[5] = buffer6[3];
                buffer5[6] = buffer6[4];
                buffer5[7] = buffer6[5];
                buffer5[8] = buffer6[6];
                buffer5[9] = buffer6[7];
                buffer5[0x12] = buffer7[0];
                buffer5[0x13] = buffer7[1];
                buffer5[20] = buffer7[2];
                buffer5[0x15] = buffer7[3];
                buffer5[0x16] = buffer7[4];
                buffer5[0x17] = buffer7[5];
                buffer5[0x18] = buffer7[6];
                buffer5[0x19] = buffer7[7];
                buffer5[30] = buffer8[0];
                buffer5[0x1f] = buffer8[1];
                buffer5[0x20] = buffer8[2];
                buffer5[0x21] = buffer8[3];
                buffer5[0x22] = buffer8[4];
                buffer5[0x23] = buffer8[5];
                buffer5[0x24] = buffer8[6];
                buffer5[0x25] = buffer8[7];
            }
            Marshal.Copy(buffer5, 0, destination, buffer5.Length);
            bool_3 = false;
            VirtualProtect_1(new IntPtr(num10), IntPtr.Size, 0x40, ref num11);
            Marshal.WriteIntPtr(new IntPtr(num10), destination);
            VirtualProtect_1(new IntPtr(num10), IntPtr.Size, num11, ref num11);
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

    [Attribute3(typeof(Attribute3.Class47<object>[]))]
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

    [Attribute3(typeof(Attribute3.Class47<object>[]))]
    private static byte[] smethod_19(byte[] byte_4)
    {
        MemoryStream stream = new MemoryStream();
        SymmetricAlgorithm algorithm = smethod_7();
        algorithm.Key = new byte[] { 
            0x8d, 0xb6, 0x3a, 0x94, 0xb9, 0x48, 170, 0x5d, 0x37, 0xfc, 0x59, 0x4e, 0xfd, 0x6f, 0x94, 4, 
            230, 0xde, 0x33, 0xba, 0xf4, 0x9c, 0x3d, 0x12, 0x8e, 0xe8, 0x42, 0x84, 0x39, 0x27, 0x3b, 0xf6
         };
        algorithm.IV = new byte[] { 12, 0x4e, 0x2f, 0xfe, 0xb5, 0xe4, 0x9c, 0x25, 15, 0xa7, 0x60, 0xd8, 0x37, 15, 0x9c, 0x45 };
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
        if (!bool_5)
        {
            smethod_8();
            bool_5 = true;
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

    internal class Attribute3 : Attribute
    {
        [Attribute3(typeof(Class47<object>[]))]
        public Attribute3(object object_0)
        {
            
        }

        internal class Class47<T>
        {
            public Class47()
            {
                
            }
        }
    }

    internal class Class48
    {
        public Class48()
        {
            
        }

        [Attribute3(typeof(Class46.Attribute3.Class47<object>[]))]
        internal static string smethod_0(string string_0, string string_1)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string_0);
            byte[] buffer3 = new byte[] { 
                0x52, 0x66, 0x68, 110, 0x20, 0x4d, 0x18, 0x22, 0x76, 0xb5, 0x33, 0x11, 0x12, 0x33, 12, 0x6d, 
                10, 0x20, 0x4d, 0x18, 0x22, 0x9e, 0xa1, 0x29, 0x61, 0x1c, 0x76, 0xb5, 5, 0x19, 1, 0x58
             };
            byte[] buffer4 = Class46.smethod_9(Encoding.Unicode.GetBytes(string_1));
            MemoryStream stream = new MemoryStream();
            SymmetricAlgorithm algorithm = Class46.smethod_7();
            algorithm.Key = buffer3;
            algorithm.IV = buffer4;
            CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate uint Delegate17(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate IntPtr Delegate18();

    [Flags]
    private enum Enum3
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Struct53
    {
        internal bool bool_0;
        internal byte[] byte_0;
    }
}

