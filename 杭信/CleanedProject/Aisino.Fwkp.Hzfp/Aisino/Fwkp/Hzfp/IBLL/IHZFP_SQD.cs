namespace Aisino.Fwkp.Hzfp.IBLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Hzfp.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IHZFP_SQD
    {
        bool Delete(string SQDH);
        DataTable GetData(int Year);
        bool Insert(HZFP_SQD model);
        HZFP_SQD Select(string SQDH);
        AisinoDataSet SelectList(int page, int count, int month);
        AisinoDataSet SelectSelList(int page, int count, int month, string xfsh);
        AisinoDataSet SelectSqdlist(int page, int count, Dictionary<string, object> dict);
        AisinoDataSet SelectSqdradlist(int page, int count, Dictionary<string, object> dict);
        bool Updata(HZFP_SQD model);
        bool Updatazt(HZFP_SQD model);

        int CurrentPage { get; set; }

        int Pagesize { get; set; }
    }
}

