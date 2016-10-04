namespace Aisino.Fwkp.Fpzpz.BLL
{
    using Aisino.Fwkp.Fpzpz.IBLL;
    using Aisino.Fwkp.Fpzpz.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class KMProperty : Aisino.Fwkp.Fpzpz.IBLL.IKMProperty
    {
        private Aisino.Fwkp.Fpzpz.IDAL.IKMProperty kmp = new KMProperty();

        public bool Delete()
        {
            return this.kmp.Delete();
        }

        public bool Delete(string BM)
        {
            return this.kmp.Delete(BM);
        }

        public DataTable GetKMInfo()
        {
            return this.kmp.GetKMInfo();
        }

        public List<string> GetKMPropertyBM()
        {
            return this.kmp.GetKMPropertyBM();
        }

        public bool ReplaceRecord(Dictionary<string, object> dict)
        {
            return this.kmp.ReplaceRecord(dict);
        }

        public bool ReplaceRecords(List<Dictionary<string, object>> list)
        {
            return this.kmp.ReplaceRecords(list);
        }
    }
}

