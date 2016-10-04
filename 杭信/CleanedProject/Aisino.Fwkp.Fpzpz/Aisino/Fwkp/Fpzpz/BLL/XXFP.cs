namespace Aisino.Fwkp.Fpzpz.BLL
{
    using Aisino.Fwkp.Fpzpz.IBLL;
    using Aisino.Fwkp.Fpzpz.IDAL;
    using System;
    using System.Collections.Generic;
    using BusinessObject;
    public class XXFP : Aisino.Fwkp.Fpzpz.IBLL.IXXFP
    {
        private Aisino.Fwkp.Fpzpz.IDAL.IXXFP dal = new XXFP();

        public bool bIfMakedPZ(string fpdm, string fphm, string fpzl)
        {
            return this.dal.bIfMakedPZ(fpdm, fphm, fpzl);
        }

        public bool CreateTempTable()
        {
            return this.dal.CreateTempTable();
        }

        public bool DropTempTable()
        {
            return this.dal.DropTempTable();
        }

        public bool EmptyTempTable()
        {
            return this.dal.EmptyTempTable();
        }

        public bool ExistTempTable(string strTableName)
        {
            return this.dal.ExistTempTable(strTableName);
        }

        public List<Fpxx> SelectPagePZXXFP(Dictionary<string, object> dict)
        {
            return this.dal.SelectPagePZXXFP(dict);
        }

        public List<Fpxx> SelectPageXXFP(Dictionary<string, object> dict)
        {
            return this.dal.SelectPageXXFP(dict);
        }
    }
}

