namespace ns5
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    internal class Class104
    {
        public Class104()
        {
            
        }

        [DllImport("DLLUSB.dll")]
        public static extern int oRegisterDeviceInterface(IntPtr intptr_0);
        public static int smethod_0(ref Message message_0)
        {
            int num = 0;
            if (message_0.Msg == 0x219)
            {
                int num2 = message_0.WParam.ToInt32();
                switch (num2)
                {
                    case 7:
                        return 3;

                    case 0x8000:
                        num = 1;
                        if (smethod_3(smethod_1(ref message_0)))
                        {
                            num = 5;
                        }
                        return num;
                }
                if (num2 != 0x8004)
                {
                    return num;
                }
                num = 4;
                if (smethod_2(smethod_1(ref message_0)))
                {
                    num = 2;
                }
                if (smethod_3(smethod_1(ref message_0)))
                {
                    num = 6;
                }
            }
            return num;
        }

        private static string smethod_1(ref Message message_0)
        {
            Struct144 struct2 = (Struct144) Marshal.PtrToStructure(message_0.LParam, typeof(Struct144));
            char[] chars = new char[0x40];
            for (int i = 0; i < 0x40; i++)
            {
                chars[i] = struct2.char_0[i * 2];
            }
            return Encoding.Default.GetString(Encoding.Default.GetBytes(chars)).Substring(8, 0x11).ToUpper();
        }

        private static bool smethod_2(string string_0)
        {
            if (((string.Compare(string_0, "VID_1234&PID_ABCD", true) != 0) && (string.Compare(string_0, "VID_1111&PID_2322", true) != 0)) && ((string.Compare(string_0, "VID_101D&PID_0001", true) != 0) && (string.Compare(string_0, "VID_101D&PID_0003", true) != 0)))
            {
                return false;
            }
            return true;
        }

        private static bool smethod_3(string string_0)
        {
            return (string.Compare(string_0, "VID_101D&PID_0002", true) == 0);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Struct144
        {
            public uint uint_0;
            public uint uint_1;
            public uint uint_2;
            public Guid guid_0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x80)]
            public char[] char_0;
        }
    }
}

