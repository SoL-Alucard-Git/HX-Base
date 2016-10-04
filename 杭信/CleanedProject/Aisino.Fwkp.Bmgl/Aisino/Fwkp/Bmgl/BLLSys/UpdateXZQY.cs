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

    internal sealed class UpdateXZQY : AbstractService
    {
        private ILog log = LogUtil.GetLogger<UpdateXZQY>();
        private BMXZQYManager xzqyManager = new BMXZQYManager();

        protected override object[] doService(object[] param)
        {
            if (param.Length < 2)
            {
                throw new ArgumentException("参数错误,至少有2个参数");
            }
            string oldBM = (param[0] as string).Trim();
            string str2 = (param[1] as string).Trim();
            string str3 = (param[2] as string).Trim();
            if (str2.Length == 0)
            {
                MessageBoxHelper.Show("请输入行政区域编码", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            if (str3.Length == 0)
            {
                MessageBoxHelper.Show("请输入行政区域名称", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            BMXZQYModel xzqEntity = new BMXZQYModel {
                BM = str2,
                MC = str3
            };
            string str4 = this.xzqyManager.ModifyDistrict(xzqEntity, oldBM);
            if (str4 == "0")
            {
                str4 = "OK";
                return new object[] { str4 };
            }
            this.log.Info("修改行政区域失败:" + str4);
            return new object[] { "Error:", str4 };
        }
    }
}

