namespace Aisino.Fwkp.Publish.BLL
{
    using Aisino.Framework.Plugin.Core.Util;
    using System;

    internal class Config
    {
        internal static string clientPort = PropertyUtil.GetValue("PUB_CLIENT_PORT", "0");
        internal static string connOnLoad = PropertyUtil.GetValue("PUB_CONN_ONLOAD", "1");
        internal static string maxConnWaitTime = PropertyUtil.GetValue("PUB_MAX_CONN_TIME", "30");
        internal static string maxMessID = PropertyUtil.GetValue("PUB_MAX_MESS_ID", "0");
        internal static string serverIP = PropertyUtil.GetValue("PUB_SERVER_IP", "192.168.1.1");
        internal static string serverPort = PropertyUtil.GetValue("PUB_SERVER_PORT", "3288");

        internal static void reLoad()
        {
            serverIP = PropertyUtil.GetValue("PUB_SERVER_IP", "192.168.1.1");
            serverPort = PropertyUtil.GetValue("PUB_SERVER_PORT", "3288");
            clientPort = PropertyUtil.GetValue("PUB_CLIENT_PORT", "0");
            maxConnWaitTime = PropertyUtil.GetValue("PUB_MAX_CONN_TIME", "30");
            connOnLoad = PropertyUtil.GetValue("PUB_CONN_ONLOAD", "1");
            maxMessID = PropertyUtil.GetValue("PUB_MAX_MESS_ID", "0");
        }
    }
}

