namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using log4net;
    using System;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;

    internal sealed class DeleteSPSM : AbstractService
    {
        private ILog log = LogUtil.GetLogger<AddKH>();
        private BMSPSMManager spsmManager = new BMSPSMManager();

        protected override object[] doService(object[] param)
        {
            if (param.Length < 2)
            {
                throw new ArgumentException("参数错误,至少有2个参数");
            }
            string spsmEntityCode = (param[0] as string).Trim();
            string sZ = (param[1] as string).Trim();
            if (spsmEntityCode.Length == 0)
            {
                MessageBoxHelper.Show("请输入商品税目名称", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            string str3 = this.spsmManager.DeleteGoodsTax(spsmEntityCode, sZ);
            if (str3 == "0")
            {
                str3 = "OK";
                return new object[] { str3 };
            }
            this.log.Info("删除商品税目失败:" + str3);
            return new object[] { "Error:", str3 };
        }
    }
}

