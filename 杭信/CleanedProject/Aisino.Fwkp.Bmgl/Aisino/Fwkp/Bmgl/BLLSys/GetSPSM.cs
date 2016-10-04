namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;

    internal sealed class GetSPSM : AbstractService
    {
        private ILog log = LogUtil.GetLogger<AddKH>();
        private BMSPSMManager spsmManager = new BMSPSMManager();

        protected override object[] doService(object[] param)
        {
            if (param.Length < 1)
            {
                throw new ArgumentException("参数错误,至少有1个参数");
            }
            string bM = (param[0] as string).Trim();
            if (bM.Length == 0)
            {
                MessageBoxHelper.Show("请输入商品税目名称", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            BMSPSMModel model = this.spsmManager.GetModel(bM, "");
            if (model != null)
            {
                return new object[] { model };
            }
            this.log.Info("获取商品税目失败:");
            return new object[] { "Error:获取商品税目失败" };
        }
    }
}

