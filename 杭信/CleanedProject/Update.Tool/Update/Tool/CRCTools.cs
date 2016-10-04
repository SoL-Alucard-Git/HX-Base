namespace Update.Tool
{
    using System;
    using System.IO;

    public class CRCTools
    {
        private static bool bool_0;
        private static uint[] uint_0;

        static CRCTools()
        {
           
            uint_0 = new uint[0x100];
        }

        public CRCTools()
        {
           
            bool_0 = false;
        }

        public static string CreateCRCValue(string strFileName)
        {
            if (!bool_0)
            {
                smethod_0();
            }
            FileStream stream = File.Open(strFileName, FileMode.Open);
            long length = stream.Length;
            byte[] buffer = new byte[length];
            int offset = 0;
            int count = buffer.Length;
            while (count > 0)
            {
                int num5 = stream.Read(buffer, offset, count);
                if (count <= 0)
                {
                    throw new EndOfStreamException("文件读取失败");
                }
                count -= num5;
                offset += num5;
            }
            stream.Close();
            return smethod_2(buffer, Convert.ToUInt32(length)).ToString("x").ToUpper();
        }

        private static void smethod_0()
        {
            uint num = 0x4c11db7;
            for (uint i = 0; i <= 0xff; i++)
            {
                uint_0[i] = smethod_1(i, (char)8) << 0x18;
                for (int j = 0; j < 8; j++)
                {
                    uint_0[i] = (uint_0[i] << 1) ^ (((uint_0[i] & 0x80000000) > 0) ? num : 0);
                }
                uint_0[i] = smethod_1(uint_0[i], ' ');
            }
            bool_0 = true;
        }

        private static uint smethod_1(uint uint_1, char char_0)
        {
            uint num = 0;
            for (int i = 1; i < (char_0 + '\x0001'); i++)
            {
                if ((uint_1 & 1) > 0)
                {
                    num |= ((uint) 1) << (char_0 - i);
                }
                uint_1 = uint_1 >> 1;
            }
            return Convert.ToUInt32(num);
        }

        private static uint smethod_2(byte[] object_0, uint uint_1)
        {
            ulong num = 0xffffffffL;
            for (int i = 0; i < uint_1; i++)
            {
                num = (num >> 8) ^ uint_0[(int) ((IntPtr) ((num & ((ulong) 0xffL)) ^ ((ulong) object_0[i])))];
            }
            return Convert.ToUInt32((ulong) (num ^ ((ulong) 0xffffffffL)));
        }
    }
}

