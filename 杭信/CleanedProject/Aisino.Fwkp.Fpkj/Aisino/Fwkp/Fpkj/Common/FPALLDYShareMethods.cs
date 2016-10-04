namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Fpkj.Form.FPCX;
    using log4net;
    using System;
    using System.Collections.Generic;
    using Framework.Plugin.Core.Util;
    internal sealed class FPALLDYShareMethods : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<FPALLDYShareMethods>();

        protected override object[] doService(object[] param)
        {
            object[] objArray2;
            object[] objArray = new object[1];
            FaPiaoChaXun xun = new FaPiaoChaXun();
            try
            {
                if ((param == null) || (param.Length < 2))
                {
                    this.loger.Debug("发票传递参数个数有误！");
                    return null;
                }
                List<string[]> list = param[0] as List<string[]>;
                bool iSQD = Tool.ObjectToBool(param[1].ToString());
                List<FaPiaoChaXun.FpPrint> allFpPrintList = new List<FaPiaoChaXun.FpPrint>();
                for (int i = 0; i < list.Count; i++)
                {
                    FaPiaoChaXun.FpPrint item = new FaPiaoChaXun.FpPrint {
                        fpzl = Tool.GetFPType(list[i][0]),
                        fpdm = list[i][1],
                        fphm = Tool.ObjectToInt(list[i][2]),
                        index = Tool.ObjectToInt(list[i][3]),
                        qdbz = Tool.ObjectToBool(list[i][4])
                    };
                    allFpPrintList.Add(item);
                }
                xun.WenBenSelectedFPListPrint(allFpPrintList, iSQD);
                objArray2 = objArray;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                objArray2 = objArray;
            }
            finally
            {
                xun.Close();
                xun = null;
            }
            return objArray2;
        }
    }
}

