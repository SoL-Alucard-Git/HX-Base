namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Forms;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Text;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;

    internal sealed class AddKH : AbstractService
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
            string mC = (param[0] as string).Trim();
            string str2 = (param[1] as string).Trim();
            if (mC.Length == 0)
            {
                MessageBoxHelper.Show("请输入客户名称", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            string str3 = "";
            string sJBM = "";
            switch (MessageBoxHelper.Show("是否有上级单位?", "输入确认", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Cancel:
                    return new object[] { "Cancel" };

                case DialogResult.Yes:
                {
                    BMKHSelect select = new BMKHSelect();
                    if (select.ShowDialog() != DialogResult.OK)
                    {
                        return new object[] { str3 };
                    }
                    sJBM = select.SelectedSJBM;
                    break;
                }
            }
            if (this.khmanager.QueryByMCAndSJBM(mC, sJBM).Rows.Count > 0)
            {
                MessageBoxHelper.Show("新增客户名称与其同级单位名称重复！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return new object[] { "Error" };
            }
            model = new BMKHModel {
                SJBM = sJBM,
                MC = mC,
                SH = str2,
                DZDH = param[2] as string,
                YHZH = param[3] as string,
            };
            model.BM = this.khmanager.TuiJianBM(model.SJBM);
            if (model.BM == "NoNode")
            {
                this.log.Info("新增客户失败,该级编码已满");
                MessageBoxHelper.Show("该级编码已满，请增位后再添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "FAIL" };
            }
            string[] spellCode = StringUtils.GetSpellCode(model.MC);
            if (spellCode.Length > 1)
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < spellCode.Length; i++)
                {
                    builder.Append(spellCode[i]);
                }
                model.KJM = builder.ToString();
            }
            else
            {
                model.KJM = spellCode[0];
            }
            model.WJ = 1;
            str3 = this.khmanager.AddCustomerKP(model, 1);
            if (str3 == "0")
            {
                str3 = "OK";
                return new object[] { str3 };
            }
            this.log.Info("新增客户失败:" + str3);
            return new object[] { "Error:", str3 };
        }
    }
}

