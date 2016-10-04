namespace Aisino.Fwkp.Bmgl.IDAL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;

    internal interface IBMSCCJManager
    {
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        AisinoDataSet QuerySCCJ(int pagesize, int pageno);
        AisinoDataSet QueryTable(int pagesize, int pageno);
    }
}

