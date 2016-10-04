namespace Aisino.Fwkp.Fpzpz.IBLL
{
    using System;
    using System.Collections.Generic;

    internal interface IXXFP
    {
        bool bIfMakedPZ(string fpdm, string fphm, string fpzl);
        bool CreateTempTable();
        bool DropTempTable();
        bool EmptyTempTable();
        bool ExistTempTable(string strTableName);
        List<Fpxx> SelectPagePZXXFP(Dictionary<string, object> dict);
        List<Fpxx> SelectPageXXFP(Dictionary<string, object> dict);
    }
}

