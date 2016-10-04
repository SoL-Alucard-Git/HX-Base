namespace Aisino.Fwkp.Xtsz
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Xtsz.DAL;
    using Aisino.Fwkp.Xtsz.Model;
    using System;
    using System.Collections.Generic;

    internal sealed class QueryDZDZInfoService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            DZDZInfoModel dzdzInfoModel = new DZDZInfoModel();
            new ParaSetDAL();
            if (!Config.GetDZDZInfoFromXML(ref dzdzInfoModel))
            {
                return null;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("ACCEPT_WEB_SERVER", dzdzInfoModel.AcceptWebServer);
            if (dzdzInfoModel.IsUseProxy)
            {
                dictionary.Add("proxyType", dzdzInfoModel.ProxyType);
                dictionary.Add("proxyHost", dzdzInfoModel.ProxyHost);
                dictionary.Add("proxyPort", dzdzInfoModel.ProxyPort);
            }
            else
            {
                dictionary.Add("proxyType", "");
                dictionary.Add("proxyHost", "");
                dictionary.Add("proxyPort", "");
            }
            if (dzdzInfoModel.IsUseProxy && dzdzInfoModel.IsAuthConfirm)
            {
                dictionary.Add("proxyAuthType", dzdzInfoModel.ProxyAuthType);
                dictionary.Add("proxyAuthUser", dzdzInfoModel.ProxyAuthUser);
                dictionary.Add("proxyAuthPassword", dzdzInfoModel.ProxyAuthPassword);
            }
            else
            {
                dictionary.Add("proxyAuthType", "");
                dictionary.Add("proxyAuthUser", "");
                dictionary.Add("proxyAuthPassword", "");
            }
            dictionary.Add("UPLOADNOW", dzdzInfoModel.UploadNowFlag);
            dictionary.Add("INTERVALFLAG", dzdzInfoModel.IntervalFlag);
            dictionary.Add("INTERVALTIME", dzdzInfoModel.IntervalTime);
            dictionary.Add("ACCUMULATEFLAG", dzdzInfoModel.AccumulateFlag);
            dictionary.Add("ACCUMULATENUM", dzdzInfoModel.AccumulateNum);
            dictionary.Add("DATASIZE", dzdzInfoModel.DataSize);
            return new object[] { dictionary };
        }
    }
}

