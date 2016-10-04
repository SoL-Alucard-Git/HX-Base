namespace ns4
{
    using System;
    using System.IO;

    internal class Class102
    {
        public Class102()
        {
            
        }

        public void method_0()
        {
            try
            {
                string path = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + @"\Log\UpDownLog.log";
                if (File.Exists(path))
                {
                    FileInfo info = new FileInfo(path);
                    if (info.Length > 0x1400000L)
                    {
                        info.MoveTo(path + ".bak" + DateTime.Now.ToString("yyyyMMdd"));
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        File.Create(path);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

