namespace ConsoleApplication1
{
    using System;
    using System.IO;
    using System.Text;
    using WindowsFormsApplication1;

    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string pwd = args[0];
                try
                {
                    if (DigitalEnvelop.DigEnvInit(false, true, true) == 0)
                    {
                        try
                        {
                            int num = DigitalEnvelop.SetCaCertAndCrlByPfx("server.pfx", pwd, "");
                            byte[] bytes = Encoding.Default.GetBytes("return=" + num.ToString());
                            if (File.Exists("caResult.dat"))
                            {
                                File.Delete("caResult.dat");
                            }
                            FileStream stream = new FileStream("caResult.dat", FileMode.OpenOrCreate);
                            stream.Write(bytes, 0, bytes.Length);
                            stream.Flush();
                            stream.Close();
                        }
                        catch (Exception exception)
                        {
                            byte[] buffer = Encoding.Default.GetBytes("Exception=" + exception.ToString());
                            if (File.Exists("caResult.dat"))
                            {
                                File.Delete("caResult.dat");
                            }
                            FileStream stream2 = new FileStream("caResult.dat", FileMode.OpenOrCreate);
                            stream2.Write(buffer, 0, buffer.Length);
                            stream2.Flush();
                            stream2.Close();
                        }
                    }
                }
                catch (Exception exception2)
                {
                    byte[] buffer3 = Encoding.Default.GetBytes("Exception=" + exception2.ToString());
                    if (File.Exists("caResult.dat"))
                    {
                        File.Delete("caResult.dat");
                    }
                    FileStream stream3 = new FileStream("caResult.dat", FileMode.OpenOrCreate);
                    stream3.Write(buffer3, 0, buffer3.Length);
                    stream3.Flush();
                    stream3.Close();
                }
                DigitalEnvelop.DigEnvClose();
            }
            else
            {
                byte[] buffer4 = Encoding.Default.GetBytes("参数不正确");
                FileStream stream4 = new FileStream("caResult.dat", FileMode.OpenOrCreate);
                stream4.Write(buffer4, 0, buffer4.Length);
                stream4.Flush();
                stream4.Close();
            }
        }
    }
}

