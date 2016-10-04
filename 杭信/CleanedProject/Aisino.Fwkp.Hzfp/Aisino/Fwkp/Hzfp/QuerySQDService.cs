namespace Aisino.Fwkp.Hzfp
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Hzfp.Form;
    using Framework.Plugin.Core.Util;
    using log4net;
    using System;

    public sealed class QuerySQDService : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<QuerySQDService>();

        protected override object[] doService(object[] param)
        {
            try
            {
                if ((param.Length != 0) && (param[0] != null))
                {
                    string str = param[0].ToString();
                    SqdTianKai kai = new SqdTianKai {
                        Text = "红字发票申请单查看",
                        sqdh = str
                    };
                    kai.InitSqdMx(InitSqdMxType.Read, null);
                    kai.Show(FormMain.control_0);
                }
                return null;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }
    }
}

