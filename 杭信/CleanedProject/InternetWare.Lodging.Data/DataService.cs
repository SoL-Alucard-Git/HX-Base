using System;
using System.IO;
using System.Reflection;

namespace InternetWare.Lodging.Data
{
    public class DataService
    {
        private const string UtiFileName = "InternetWare.Util.dll";
        private const string UtilNameSpace = "InternetWare.Util";

        public static object DoService(EventArgs args)
        {
            Assembly assem = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UtiFileName));
            MethodInfo method = assem.GetType($"{UtilNameSpace}.DataClient").GetMethod("DoService");
            return method.Invoke(null, new object[] { args});
        }
    }
}
