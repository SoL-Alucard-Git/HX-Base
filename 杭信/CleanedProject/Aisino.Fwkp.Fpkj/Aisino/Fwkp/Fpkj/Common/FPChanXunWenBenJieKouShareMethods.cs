namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.DAL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using Framework.Plugin.Core.Util;
    internal sealed class FPChanXunWenBenJieKouShareMethods : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<FPChanXunWenBenJieKouShareMethods>();

        protected override object[] doService(object[] param)
        {
            object[] objArray2;
            object[] objArray = new object[1];
            XXFP xxfp = new XXFP(false);
            try
            {
                this.loger.Debug("###开始查询。。。。");
                new Dictionary<string, object>();
                if ((param == null) || (param.Length < 3))
                {
                    this.loger.Debug("[FPChanXunWenBenJieKouShareMethod函数异常]：发票查询传递参数个数有误！");
                    return null;
                }
                string xSDJBH = "";
                this.loger.Debug("param长度" + param.Length.ToString());
                if ((param.Length > 3) && (param[3] != null))
                {
                    xSDJBH = param[3].ToString();
                }
                if (param[0] == null)
                {
                    this.loger.Debug("发票种类is null");
                    param[0] = "";
                }
                if (param[1] == null)
                {
                    this.loger.Debug("发票代码is null");
                    param[1] = "";
                }
                if (param[2] == null)
                {
                    this.loger.Debug("发票号码is null");
                    param[2] = "";
                }
                if ((param.Length > 3) && (param[3] == null))
                {
                    this.loger.Debug("销售单据编号is null");
                    param[3] = "";
                }
                this.loger.Debug("###查询fpzl" + param[0].ToString() + " fpdm " + param[1].ToString() + " fphm " + param[2].ToString() + " xsdjbh " + xSDJBH);
                Fpxx fpxx = xxfp.GetModel(param[0].ToString(), param[1].ToString(), Tool.ObjectToInt(param[2]), xSDJBH);
                objArray[0] = fpxx;
                objArray2 = objArray;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                objArray2 = objArray;
            }
            finally
            {
                xxfp = null;
            }
            return objArray2;
        }
    }
}

