namespace Aisino.Fwkp.Xtsz
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Xtsz.DAL;
    using Aisino.Fwkp.Xtsz.Model;
    using System;

    internal sealed class UpdateDZDZInfoService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if (param.Length == 6)
            {
                DZDZInfoModel dzdzInfoModel = new DZDZInfoModel();
                new ParaSetDAL();
                Config.GetDZDZInfoFromXML(ref dzdzInfoModel);
                dzdzInfoModel.UploadNowFlag = bool.Parse(param[0].ToString());
                dzdzInfoModel.IntervalFlag = bool.Parse(param[1].ToString());
                dzdzInfoModel.IntervalTime = int.Parse(param[2].ToString());
                dzdzInfoModel.AccumulateFlag = bool.Parse(param[3].ToString());
                dzdzInfoModel.AccumulateNum = int.Parse(param[4].ToString());
                dzdzInfoModel.DataSize = int.Parse(param[5].ToString());
                if (Config.CreateDZDZXML(dzdzInfoModel))
                {
                    return new object[] { true };
                }
            }
            return new object[] { false };
        }
    }
}

