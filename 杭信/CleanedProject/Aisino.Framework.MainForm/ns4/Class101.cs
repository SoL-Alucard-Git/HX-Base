namespace ns4
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;

    internal static class Class101
    {
        internal static bool bool_0;
        internal static object object_0;
        public static string string_0;

        static Class101()
        {
            
            object_0 = new object();
            bool_0 = true;
            string_0 = null;
            try
            {
                string_0 = Assembly.GetExecutingAssembly().Location + @"\..\..\Log\UpDownLog.log";
                if (!File.Exists(string_0))
                {
                    File.Create(string_0);
                }
            }
            catch
            {
            }
        }

        internal static void smethod_0(string string_1)
        {
            if (bool_0)
            {
                try
                {
                    if (!string.IsNullOrEmpty(string_1))
                    {
                        smethod_2(string_1);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        internal static void smethod_1(string string_1)
        {
            try
            {
                if (!string.IsNullOrEmpty(string_1))
                {
                    smethod_2(string_1);
                }
            }
            catch (Exception)
            {
            }
        }

        internal static void smethod_2(string string_1)
        {
            try
            {
                new Class102().method_0();
                string str = DateTime.Now.ToString() + "-----" + string_1 + "\r\n";
                lock (object_0)
                {
                    using (FileStream stream = new FileStream(string_0, FileMode.Append))
                    {
                        StreamWriter writer = new StreamWriter(stream, Encoding.Default);
                        writer.Write(str);
                        writer.Flush();
                        writer.Close();
                        writer.Dispose();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

