using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace InternetWare.Lodging.Data
{
    public class DataService
    {
        private const string UtiFileName = "InternetWare.Util.dll";
        private const string UtilNameSpace = "InternetWare.Util";
        private const string ArgsNameSpace = "Aisino.Args.dll";

        public static string DoService(BaseArgs args)
        {
            return DoService(JsonConvert.SerializeObject(args,Formatting.None), args.GetType().FullName);
        }

        public static string DoService(string args,string typeName)
        {
            Assembly assem = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UtiFileName));
            MethodInfo method = assem.GetType($"{UtilNameSpace}.DataClient").GetMethod("DoService");
            Type t = Assembly.LoadFrom(ArgsNameSpace).GetType(typeName);
            return Pack(method.Invoke(null, new object[] { JsonConvert.DeserializeObject(args,t) }) as BaseResult);
        }

        private static string Pack(BaseResult result)
        {
            string jsonresult= JsonConvert.SerializeObject(result, Formatting.None);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonresult));
        }
    }
}
