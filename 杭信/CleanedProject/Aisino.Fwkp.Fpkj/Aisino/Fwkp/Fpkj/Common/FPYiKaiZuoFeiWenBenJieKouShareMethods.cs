namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Fpkj.DAL;
    using Aisino.Fwkp.Fpkj.Form.FPZF;
    using log4net;
    using System;
    using System.Collections.Generic;
    using Framework.Plugin.Core.Util;
    internal sealed class FPYiKaiZuoFeiWenBenJieKouShareMethods : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<FPYiKaiZuoFeiWenBenJieKouShareMethods>();

        protected override object[] doService(object[] param)
        {
            object[] objArray2;
            FaPiaoZuoFei_YiKai kai = new FaPiaoZuoFei_YiKai();
            object[] objArray = new object[] { false, "" };
            new XXFP(false);
            try
            {
                new Dictionary<string, object>();
                if ((param == null) || (param.Length < 3))
                {
                    this.loger.Debug("[FPYiKaiZuoFeiWenBenJieKouShareMethods函数异常]：发票作废传递参数个数有误！");
                    objArray[0] = false;
                    objArray[1] = "发票作废传递参数个数有误";
                    return objArray;
                }
                List<DaiKaiXml.SWDKDMHM> swdkZyList = null;
                List<DaiKaiXml.SWDKDMHM> swdkPtList = null;
                if (Tool.IsShuiWuDKSQ())
                {
                    swdkZyList = new List<DaiKaiXml.SWDKDMHM>();
                    swdkPtList = new List<DaiKaiXml.SWDKDMHM>();
                }
                int type = 0;
                if (param.Length > 3)
                {
                    type = Tool.ObjectToInt(param[3]);
                }
                string str = kai.YiKaiZuoFeiMainFunction(param[0].ToString(), param[1].ToString(), param[2].ToString(), swdkZyList, swdkPtList, type);
                if (str == "0000")
                {
                    kai.SaveToDB();
                    if (Tool.IsShuiWuDKSQ())
                    {
                        new DaiKaiXml().DaiKaiFpZuoFeiUpload(swdkZyList, swdkPtList);
                    }
                    objArray[0] = true;
                    objArray[1] = "";
                }
                else
                {
                    objArray[0] = false;
                    objArray[1] = str;
                }
                objArray2 = objArray;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                objArray[0] = false;
                objArray2 = objArray;
            }
            finally
            {
                if (kai != null)
                {
                    kai.Close();
                    kai.Dispose();
                    kai = null;
                }
            }
            return objArray2;
        }
    }
}

