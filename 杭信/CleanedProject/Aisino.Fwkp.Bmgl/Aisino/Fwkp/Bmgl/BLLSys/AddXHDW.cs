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

    internal sealed class AddXHDW : AbstractService
    {
        private ILog log = LogUtil.GetLogger<AddKH>();
        private BMXHDWManager xhdwManager = new BMXHDWManager();

        protected override object[] doService(object[] param)
        {
            BMXHDWModel model;
            if (!CheckPermission.Check("XHDW"))
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
                MessageBoxHelper.Show("请输入销货单位名称", "输入提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            if (taxCode.Length == 0)
            {
                MessageBoxHelper.Show("请输入销货单位税号", "输入提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    if (this.xhdwManager.QueryByTaxCode(taxCode).Rows.Count > 0)
                    {
                        MessageBoxHelper.Show("与此税号对应的销货单位已存在", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return new object[] { "Error" };
                    }
                    BMXHDWSelect select = new BMXHDWSelect();
                    if (select.ShowDialog() != DialogResult.OK)
                    {
                        return new object[] { str3 };
                    }
                    sJBM = select.SelectedSJBM;
                    break;
                }
            }
            if (this.xhdwManager.QueryByMCAndSJBM(mC, sJBM).Rows.Count > 0)
            {
                MessageBoxHelper.Show("新增收发货人名称与其同级单位名称重复！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return new object[] { "Error" };
            }
            model = new BMXHDWModel {
                SJBM = sJBM,
                MC = mC,
                SH = taxCode,
                WJ = 1
            };
            model.BM = this.xhdwManager.TuiJianBM(model.SJBM);
            str3 = this.xhdwManager.AddCustomerKP(model, 1);
            if (str3 == "0")
            {
                str3 = "OK";
                return new object[] { str3 };
            }
            this.log.Info("新增销货单位失败:" + str3);
            return new object[] { "Error:", str3 };
        }
    }
}

