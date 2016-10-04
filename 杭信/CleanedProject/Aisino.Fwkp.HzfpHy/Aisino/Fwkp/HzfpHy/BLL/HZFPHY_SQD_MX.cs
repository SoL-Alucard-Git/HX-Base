namespace Aisino.Fwkp.HzfpHy.BLL
{
    using Aisino.Fwkp.HzfpHy.IBLL;
    using Aisino.Fwkp.HzfpHy.IDAL;
    using Aisino.Fwkp.HzfpHy.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class HZFPHY_SQD_MX : Aisino.Fwkp.HzfpHy.IBLL.IHZFPHY_SQD_MX
    {
        private Aisino.Fwkp.HzfpHy.IDAL.IHZFPHY_SQD_MX dal = new Aisino.Fwkp.HzfpHy.DAL.HZFPHY_SQD_MX();

        public bool Delete(string SQDH)
        {
            return this.dal.Delete(SQDH);
        }

        public bool Insert(List<Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD_MX> models)
        {
            return this.dal.Insert(models);
        }

        public DataTable SelectList(string SQDH)
        {
            return this.dal.SelectList(SQDH);
        }

        public bool Updata(Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD_MX model)
        {
            return this.dal.Updata(model);
        }
    }
}

