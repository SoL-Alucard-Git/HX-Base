namespace Aisino.Fwkp.Bmgl.IBLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;

    internal interface IBMCDManager
    {
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        AisinoDataSet QueryCD(int pagesize, int pageno);
        AisinoDataSet QueryTable(int pagesize, int pageno);

        int CurrentPage { get; set; }

        int Pagesize { get; set; }
    }
}

