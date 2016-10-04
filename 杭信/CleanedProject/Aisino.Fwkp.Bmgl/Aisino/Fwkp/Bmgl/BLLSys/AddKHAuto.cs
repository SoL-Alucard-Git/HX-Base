namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;

    internal sealed class AddKHAuto : AbstractService
    {
        private BMKHManager khmanager = new BMKHManager();
        private ILog log = LogUtil.GetLogger<AddKH>();

        protected override object[] doService(object[] param)
        {
            BMKHModel model;
            if (!CheckPermission.Check("KH"))
            {
                return null;
            }
            if (param.Length < 4)
            {
                throw new ArgumentException("参数错误,至少有4个参数");
            }
            string str = (param[0] as string).Trim();
            string str2 = (param[1] as string).Trim();
            if (str.Length == 0)
            {
                return new object[] { "Cancel" };
            }
            string sJBM = "";
            sJBM = this.khmanager.AutoNodeLogic();
            model = new BMKHModel {
                SJBM = sJBM,
                MC = str,
                SH = str2,
                DZDH = param[2] as string,
                YHZH = param[3] as string,
            };
            model.BM = this.khmanager.TuiJianBM(model.SJBM);
            if (model.SJBM == "NoNode")
            {
                this.log.Info("自动新增客户失败,“自动保存”上级编码已满");
                MessageBoxHelper.Show("自动保存上级编码已满，请增位后再添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "FAIL" };
            }
            if (model.BM.Length > 0x10)
            {
                this.log.Info("自动新增客户失败,“自动保存”节点下空位不足");
                return new object[] { "FAIL" };
            }
            if (model.BM == "NoNode")
            {
                this.log.Info("自动新增客户失败,“自动保存”该级编码已满");
                MessageBoxHelper.Show("自动保存该级编码已满，请增位后再添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "FAIL" };
            }
            model.KJM = CommonFunc.GenerateKJM(model.MC);
            model.WJ = 1;
            if (this.khmanager.AddCustomerToAuto(model, sJBM))
            {
                return new object[] { "OK" };
            }
            this.log.Info("自动新增客户失败");
            return new object[] { "FAIL" };
        }
    }
}

