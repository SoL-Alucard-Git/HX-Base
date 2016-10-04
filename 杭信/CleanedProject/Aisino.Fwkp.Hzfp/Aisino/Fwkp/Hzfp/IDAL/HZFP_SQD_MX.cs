namespace Aisino.Fwkp.Hzfp.IDAL
{
    using Aisino.Fwkp.Hzfp.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface HZFP_SQD_MX
    {
        bool Delete(string SQDH);
        bool Insert(List<Aisino.Fwkp.Hzfp.Model.HZFP_SQD_MX> models);
        DataTable SelectList(string SQDH);
        bool Updata(Aisino.Fwkp.Hzfp.Model.HZFP_SQD_MX model);
    }
}

