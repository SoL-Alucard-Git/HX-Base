namespace ns2
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class Class24
    {
        public Class24()
        {
            
        }

        [DllImport("JSDiskDll.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Auto)]
        private static extern int CallCmdQiYe(byte byte_0, byte byte_1, byte byte_2, byte byte_3, byte byte_4, ushort ushort_0, byte[] byte_5);
        [DllImport("JSDiskDll.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Auto)]
        private static extern int CloseJsp(byte byte_0);
        [DllImport("JSDiskDll.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Auto)]
        private static extern int OpenJsp();
        internal static int smethod_0(byte byte_0)
        {
            int num = -1;
            byte[] buffer = new byte[0x400];
            if (((CallCmdQiYe(byte_0, 1, 0xe3, 0, 0x29, 0, buffer) != 0) || (CallCmdQiYe(byte_0, 2, 0xe3, 0, 0x29, 0, buffer) != 0)) || ((Convert.ToUInt16((int) (buffer[1] + (buffer[0] << 8))) != 0) || (CallCmdQiYe(byte_0, 3, 0xe3, 0, 0x29, 3, buffer) != 0)))
            {
                return num;
            }
            if ((buffer[2] & 0x80) != 0)
            {
                return Convert.ToUInt16((int) (((buffer[2] & 0x7f) << 8) + buffer[1]));
            }
            return 0;
        }

        internal static string smethod_1(byte byte_0)
        {
            string str = "";
            byte[] buffer = new byte[0x400];
            if (((CallCmdQiYe(byte_0, 1, 0xe3, 0, 0x20, 0, buffer) == 0) && (CallCmdQiYe(byte_0, 2, 0xe3, 0, 0x20, 0, buffer) == 0)) && ((Convert.ToUInt16((int) (buffer[1] + (buffer[0] << 8))) == 0) && (CallCmdQiYe(byte_0, 3, 0xe3, 0, 0x20, 15, buffer) == 0)))
            {
                str = Encoding.GetEncoding("GBK").GetString(buffer, 0, 15);
            }
            return str;
        }
    }
}

