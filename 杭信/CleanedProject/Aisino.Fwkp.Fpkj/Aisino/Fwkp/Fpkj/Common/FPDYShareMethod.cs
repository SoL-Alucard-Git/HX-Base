namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.DAL;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    internal sealed class FPDYShareMethod : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<FPXiuFuShareMethods>();

        protected override object[] doService(object[] param)
        {
            object[] objArray2;
            object[] objArray = new object[] { 0 };
            XXFP xxfp = new XXFP(false);
            try
            {
                if (xxfp == null)
                {
                    xxfp = new XXFP(false);
                }
                if ((param == null) || (param.Length < 3))
                {
                    this.loger.Error("发票打印标志回写数据库：参数个数错误");
                    return null;
                }
                string fPZL = param[0].ToString();
                string fPDM = param[1].ToString();
                int fPHM = int.Parse(param[2].ToString());
                if (!xxfp.SetYiDaYin(fPZL, fPDM, fPHM))
                {
                    this.loger.Error("[设置发票已打印状态失败]" + ("发票种类：" + fPZL + "发票代码：" + fPDM + "发票号码：" + fPHM.ToString()));
                }
                objArray[0] = 1;
                objArray2 = objArray;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                objArray2 = objArray;
            }
            finally
            {
                xxfp = null;
            }
            return objArray2;
        }

        private string GetInvoiceType(FPLX type)
        {
            switch (type)
            {
                case 0:
                    return "专用发票";

                case (FPLX)2:
                    return "普通发票";

                case (FPLX)11:
                    return "货物运输业增值税专用发票";

                case (FPLX)12:
                    return "机动车销售统一发票";
            }
            return "专用发票";
        }
    }
}

