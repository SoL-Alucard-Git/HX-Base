namespace Aisino.Fwkp.Publish.BLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.PubData.Message_S2C;
    using System;

    internal interface IPubManager
    {
        HtmlMessage QueryPub(string xh);
        AisinoDataSet QueryPub(string start, string end, int pageSize, int pageNo);
        bool SavePub(HtmlMessage mess);
    }
}

