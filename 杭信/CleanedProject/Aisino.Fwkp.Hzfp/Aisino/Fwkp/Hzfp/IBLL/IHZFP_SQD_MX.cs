namespace Aisino.Fwkp.Hzfp.IBLL
{
    using Aisino.Fwkp.Hzfp.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IHZFP_SQD_MX
    {
        bool Delete(string SQDH);
        bool Insert(List<HZFP_SQD_MX> models);
        DataTable SelectList(string SQDH);
        bool Updata(HZFP_SQD_MX model);
    }
}

