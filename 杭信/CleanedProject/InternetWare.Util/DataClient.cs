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
            if (args is DaYinArgs)
                return new DaYinClient(args as DaYinArgs);
            else if (args is ChaXunArgs)
                return new ChaXunClient(args as ChaXunArgs);
            else if (args is ZuoFeiChaXunArgs)
                return new ZuoFeiChaXunClient(args as ZuoFeiChaXunArgs);
            else if (args is ZuoFeiArgs)
                return new ZuoFeiClient(args as ZuoFeiArgs);
            else if (args is TianKaiQueRenArgs)
                return new TianKaiQueRenClient(args as TianKaiQueRenArgs);
            else if (args is HZTKArgs)
                return new HZTKClient(args as HZTKArgs);
            else if (args is WeiKaiArgs)
                return new WeiKaiClient(args as WeiKaiArgs);
            else if (args is WeiKaiChaXunArgs)
                return new WeiKaiChaXunClient(args as WeiKaiChaXunArgs);
            else if (args is GetKHArgs)
                return new GetKHClient(args as GetKHArgs);
            else if (args is GetSPArgs)
                return new GetSPClient(args as GetSPArgs);
            return new BaseClient();
        }
    }
}
