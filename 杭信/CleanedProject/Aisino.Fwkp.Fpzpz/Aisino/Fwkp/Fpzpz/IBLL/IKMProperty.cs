namespace Aisino.Fwkp.Fpzpz.IBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IKMProperty
    {
        bool Delete();
        bool Delete(string BM);
        DataTable GetKMInfo();
        List<string> GetKMPropertyBM();
        bool ReplaceRecord(Dictionary<string, object> dict);
        bool ReplaceRecords(List<Dictionary<string, object>> list);
    }
}

