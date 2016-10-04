namespace Aisino.Fwkp.Bmgl.IBLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;

    internal interface IBMCLZLManager
    {
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        AisinoDataSet QueryCLZL(int pagesize, int pageno);
        AisinoDataSet QueryTable(int pagesize, int pageno);

        int CurrentPage { get; set; }

        int Pagesize { get; set; }
    }
}

