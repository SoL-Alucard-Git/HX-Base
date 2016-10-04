namespace Aisino.Fwkp.Hzfp.BLL
{
    using Aisino.Fwkp.Hzfp.IBLL;
    using Aisino.Fwkp.Hzfp.IDAL;
    using Aisino.Fwkp.Hzfp.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class HZFP_SQD_MX : IHZFP_SQD_MX
    {
        private Aisino.Fwkp.Hzfp.IDAL.HZFP_SQD_MX dal = new Aisino.Fwkp.Hzfp.DAL.HZFP_SQD_MX();

        public bool Delete(string SQDH)
        {
            return this.dal.Delete(SQDH);
        }

        public bool Insert(List<Aisino.Fwkp.Hzfp.Model.HZFP_SQD_MX> models)
        {
            return this.dal.Insert(models);
        }

        public DataTable SelectList(string SQDH)
        {
            return this.dal.SelectList(SQDH);
        }

        public bool Updata(Aisino.Fwkp.Hzfp.Model.HZFP_SQD_MX model)
        {
            return this.dal.Updata(model);
        }
    }
}

