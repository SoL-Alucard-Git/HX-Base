namespace Aisino.Fwkp.Xtsz
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Xtsz.DAL;
    using Aisino.Fwkp.Xtsz.Model;
    using System;
    using System.Collections.Generic;

    internal sealed class QueryCorporationInfoService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if (param.Length != 1)
            {
                return null;
            }
            SysTaxInfoModel taxAdminModel = new SysTaxInfoModel();
            new ParaSetDAL().GetCorporationInfoByCode(param[0].ToString(), ref taxAdminModel);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("QYBH", param[0].ToString());
            dictionary.Add("QYMC", taxAdminModel.QYMC);
            dictionary.Add("ZCLX", taxAdminModel.ZCLX);
            dictionary.Add("KPXE", taxAdminModel.KPXE);
            dictionary.Add("BSRQ", taxAdminModel.BSRQ);
            dictionary.Add("BSRPHF", taxAdminModel.BSRPHF);
            dictionary.Add("YHZH", taxAdminModel.YHZH);
            dictionary.Add("FRDB", taxAdminModel.FRDB);
            dictionary.Add("YYDZ", taxAdminModel.YYDZ);
            dictionary.Add("DHHM", taxAdminModel.DHHM);
            dictionary.Add("JYZS", taxAdminModel.JYZS);
            dictionary.Add("KJZG", taxAdminModel.KJZG);
            return new object[] { dictionary };
        }
    }
}

