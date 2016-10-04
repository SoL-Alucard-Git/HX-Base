namespace Aisino.Fwkp.HzfpHy
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.HzfpHy.Form;
    using System;

    public sealed class QueryHySQDService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if ((param.Length != 0) && (param[0] != null))
            {
                string str = param[0].ToString();
                try
                {
                    HySqdTianKai kai = new HySqdTianKai {
                        Text = "红字货物运输业增值税专用发票信息表查看",
                        sqdh = str
                    };
                    kai.InitSqdMx(InitSqdMxType.Read, null);
                    kai.Show(FormMain.control_0);
                }
                catch (BaseException exception)
                {
                    ExceptionHandler.HandleError(exception);
                }
                catch (Exception exception2)
                {
                    ExceptionHandler.HandleError(exception2);
                }
            }
            return null;
        }
    }
}

