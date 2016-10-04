using InternetWare.Util.Client;
using IntetnetWare.Lodging.Args;
using System;

namespace InternetWare.Util
{
    public class DataClient
    {
        public static object DoService(EventArgs args)
        {
            return ClientFactory(args as ChaXunArgs).DoService();
        }

        internal static BaseClient ClientFactory(EventArgs args)
        {
            if (args is ChaXunArgs)
                return new ChaXunClient(args as ChaXunArgs);
            return new BaseClient();
        }
    }
}
