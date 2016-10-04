namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.Form.FPCX;
    using Aisino.Fwkp.Fpkj.Form.FPFZ;
    using Aisino.Fwkp.Fpkj.Form.FPZF;
    using log4net;
    using System;

    internal sealed class FPFZMethods : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<FPFZMethods>();

        protected override object[] doService(object[] param)
        {
            object[] objArray;
            FaPiaoChaXun_FPFZ n_fpfz = new FaPiaoChaXun_FPFZ();
            try
            {
                if (param == null)
                {
                    MessageManager.ShowMsgBox("FPCX-000014");
                    return null;
                }
                if (param.Length != 2)
                {
                    MessageManager.ShowMsgBox("FPCX-000015");
                    return null;
                }
                Tool.FPZL = (string) param[0];
                Tool.ZYFPZL = (ZYFP_LX) param[1];
                FaPiaoZuoFei_YiKai.CardClock = DingYiZhiFuChuan1.dataTimeCCRQ;
                n_fpfz.Edit(FaPiaoChaXun.EditFPCX.FuZhi);
                n_fpfz.ShowDialog();
                if (n_fpfz.OBJECT_FPFZ == null)
                {
                    return null;
                }
                objArray = n_fpfz.OBJECT_FPFZ;
            }
            catch (Exception exception)
            {
                this.loger.Error("[FPFZMethods函数异常]" + exception.Message);
                objArray = null;
            }
            finally
            {
                if (n_fpfz != null)
                {
                    n_fpfz.Close();
                    n_fpfz.Dispose();
                    n_fpfz = null;
                }
            }
            return objArray;
        }
    }
}

