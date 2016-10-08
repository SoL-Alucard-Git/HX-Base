using InternetWare.Util.Client;
using InternetWare.Lodging.Data;
using System;

namespace InternetWare.Util
{
    public class DataClient
    {
        public static object DoService(EventArgs args)
        {
            return ClientFactory(args).DoService();
        }

        internal static BaseClient ClientFactory(EventArgs args)
        {
            if (args is ChaXunArgs)
                return new ChaXunClient(args as ChaXunArgs);
            else if (args is DaYinArgs)
                return new DaYinClient(args as DaYinArgs);
            return new BaseClient();
        }
    }
}
