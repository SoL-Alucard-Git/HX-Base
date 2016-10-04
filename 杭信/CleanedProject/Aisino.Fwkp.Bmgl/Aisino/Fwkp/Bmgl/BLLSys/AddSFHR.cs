namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Forms;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;

    internal sealed class AddSFHR : AbstractService
    {
        private ILog log = LogUtil.GetLogger<AddKH>();
        private BMSFHRManager sfhrManager = new BMSFHRManager();

        protected override object[] doService(object[] param)
        {
            BMSFHRModel model;
            if (!CheckPermission.Check("SFHR"))
            {
                return null;
            }
            if (param.Length < 2)
            {
                throw new ArgumentException("参数错误,至少有2个参数");
            }
            string mC = (param[0] as string).Trim();
            string taxCode = (param[1] as string).Trim();
            if (mC.Length == 0)
            {
                MessageBoxHelper.Show("请输入收/发货人名称", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            if (taxCode.Length == 0)
            {
                MessageBoxHelper.Show("请输入收/发货人税号", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    if (this.sfhrManager.QueryByTaxCode(taxCode).Rows.Count > 0)
                    {
                        MessageBoxHelper.Show("与此税号对应的收/发货人已存在", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return new object[] { "Error" };
                    }
                    BMSFHRSelect select = new BMSFHRSelect();
                    if (select.ShowDialog() != DialogResult.OK)
                    {
                        return new object[] { str3 };
                    }
                    sJBM = select.SelectedSJBM;
                    break;
                }
            }
            if (this.sfhrManager.QueryByMCAndSJBM(mC, sJBM).Rows.Count > 0)
            {
                MessageBoxHelper.Show("新增收发货人名称与其同级单位名称重复！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return new object[] { "Error" };
            }
            model = new BMSFHRModel {
                SJBM = sJBM,
                MC = mC,
                SH = taxCode,
                WJ = 1
            };
            model.BM = this.sfhrManager.TuiJianBM(model.SJBM);
            str3 = this.sfhrManager.AddCustomerKP(model, 1);
            if (str3 == "0")
            {
                str3 = "OK";
                return new object[] { str3 };
            }
            this.log.Info("新增收/发货人失败:" + str3);
            return new object[] { "Error:", str3 };
        }
    }
}

