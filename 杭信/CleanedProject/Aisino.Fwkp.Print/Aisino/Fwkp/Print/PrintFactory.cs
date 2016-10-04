namespace Aisino.Fwkp.Print
{
    using System;
    using System.Reflection;

    public class PrintFactory
    {
        public PrintFactory()
        {
            
        }

        public static IPrint Create(string string_0, params object[] args)
        {
            IPrint print = null;
            PrintFileModel model = ReadXml.Get()[string_0];
            if (model != null)
            {
                Type type = Assembly.LoadFile(model.AssemblyName).GetType(model.ClassName, false, true);
                if (type != null)
                {
                    print = Activator.CreateInstance(type, new object[] { model.Id }) as IPrint;
                    if (print != null)
                    {
                        print.method_0(args);
                    }
                }
            }
            return print;
        }
    }
}

