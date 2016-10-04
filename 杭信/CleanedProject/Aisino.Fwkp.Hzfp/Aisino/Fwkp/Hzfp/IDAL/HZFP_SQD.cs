namespace Aisino.Fwkp.Hzfp.IDAL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Hzfp.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface HZFP_SQD
    {
        bool Delete(string SQDH);
        DataTable GetData(int Year);
        bool Insert(Aisino.Fwkp.Hzfp.Model.HZFP_SQD model);
        Aisino.Fwkp.Hzfp.Model.HZFP_SQD Select(string SQDH);
        AisinoDataSet SelectList(int page, int count, int month);
        AisinoDataSet SelectSelList(int page, int count, int month, string xfsh);
        AisinoDataSet SelectSqdlist(int page, int count, Dictionary<string, object> dict);
        AisinoDataSet SelectSqdradlist(int page, int count, Dictionary<string, object> dict);
        bool Updata(Aisino.Fwkp.Hzfp.Model.HZFP_SQD model);
        bool Updatazt(Aisino.Fwkp.Hzfp.Model.HZFP_SQD model);
    }
}

