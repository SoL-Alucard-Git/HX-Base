namespace Aisino.Fwkp.HzfpHy.IBLL
{
    using Aisino.Fwkp.HzfpHy.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IHZFPHY_SQD_MX
    {
        bool Delete(string SQDH);
        bool Insert(List<HZFPHY_SQD_MX> models);
        DataTable SelectList(string SQDH);
        bool Updata(HZFPHY_SQD_MX model);
    }
}

