namespace Aisino.Fwkp.HzfpHy.IBLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.HzfpHy.Model;
    using System;
    using System.Collections.Generic;

    internal interface IHZFPHY_SQD
    {
        bool Delete(string SQDH);
        bool Insert(HZFPHY_SQD model);
        HZFPHY_SQD Select(string SQDH);
        AisinoDataSet SelectList(int page, int count, Dictionary<string, object> dict);
        bool Updata(HZFPHY_SQD model);
        bool Updatazt(HZFPHY_SQD model);

        int CurrentPage { get; set; }

        int Pagesize { get; set; }
    }
}

