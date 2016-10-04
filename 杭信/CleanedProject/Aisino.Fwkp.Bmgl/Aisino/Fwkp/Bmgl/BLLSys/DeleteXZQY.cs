namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using log4net;
    using System;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;

    internal sealed class DeleteXZQY : AbstractService
    {
        private ILog log = LogUtil.GetLogger<DeleteXZQY>();
        private BMXZQYManager xzqyManager = new BMXZQYManager();

        protected override object[] doService(object[] param)
        {
            if (param.Length < 1)
            {
                throw new ArgumentException("参数错误,至少有1个参数");
            }
            string xzqEntityCode = (param[1] as string).Trim();
            if (xzqEntityCode.Length == 0)
            {
                MessageBoxHelper.Show("请输入行政区域编码", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            string str2 = this.xzqyManager.DeleteDistrict(xzqEntityCode);
            if (str2 == "0")
            {
                str2 = "OK";
                return new object[] { str2 };
            }
            this.log.Info("删除行政区域失败:" + str2);
            return new object[] { "Error:", str2 };
        }
    }
}

