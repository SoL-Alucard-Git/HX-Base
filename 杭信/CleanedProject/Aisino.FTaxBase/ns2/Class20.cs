namespace ns2
{
    using Aisino.FTaxBase;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;

    internal static class Class20
    {
        internal static bool bool_0;
        private static string string_0;
        private static string string_1;
        private static string string_2;
        private static string string_3;

        static Class20()
        {
            
            string_0 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string_1 = Path.Combine(string_0, @"..\Log");
            string_2 = Path.Combine(string_1, "TaxCard.log");
            string_3 = Path.Combine(string_0, @"..\Config\BsWriteUsbKey.dat");
        }

        internal static void smethod_0()
        {
            try
            {
                long num = 0xdbba0L;
                if (!Directory.Exists(string_1))
                {
                    Directory.CreateDirectory(string_1);
                    File.Create(string_2);
                }
                if (!File.Exists(string_2))
                {
                    File.Create(string_2);
                }
                FileInfo info = new FileInfo(string_2);
                if (info.Length > num)
                {
                    string destFileName = string_2 + "." + DateTime.Now.ToString("yyyyMMddHHmm");
                    info.MoveTo(destFileName);
                }
            }
            catch (FileNotFoundException exception)
            {
                File.Create(string_2);
                smethod_2(exception.Message ?? "");
            }
            catch (Exception exception2)
            {
                smethod_2(exception2.ToString());
            }
        }

        internal static void smethod_1(object object_0)
        {
        }

        internal static void smethod_2(object object_0)
        {
        }

        internal static void smethod_3(object object_0)
        {
        }

        private static void smethod_4(object object_0, object object_1)
        {
        }

        internal static void smethod_5(string string_4, DeviceType deviceType_0)
        {
            if (deviceType_0 == DeviceType.UsbKey2)
            {
                smethod_3("报税盘2型 :" + string_4);
            }
        }

        internal static void smethod_6(string string_4, DeviceType deviceType_0)
        {
            if (deviceType_0 == DeviceType.UsbKey2)
            {
                smethod_2("报税盘2型 :" + string_4);
            }
        }

        internal static void smethod_7(string string_4)
        {
            smethod_3(string_4);
            Exception exception = new Exception(string_4);
            throw exception;
        }

        internal static void smethod_8(string string_4)
        {
            using (StreamWriter writer = new StreamWriter(string_3, true, Encoding.GetEncoding("gb2312")))
            {
                writer.WriteLine(string_4);
            }
        }

        internal static string smethod_9()
        {
            string str = "";
            try
            {
                using (StreamReader reader = new StreamReader(string_3, Encoding.GetEncoding("gb2312")))
                {
                    while (!reader.EndOfStream)
                    {
                        string str2 = reader.ReadLine();
                        if (str2.Length > 0)
                        {
                            str = str2;
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                str = "000";
            }
            return str;
        }
    }
}

