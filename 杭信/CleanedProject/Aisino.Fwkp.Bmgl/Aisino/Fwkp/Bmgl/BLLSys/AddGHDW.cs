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

    internal sealed class AddGHDW : AbstractService
    {
        private BMGHDWManager ghdwmanager = new BMGHDWManager();
        private ILog log = LogUtil.GetLogger<AddGHDW>();

        protected override object[] doService(object[] param)
        {
            BMGHDWModel model;
            if (!CheckPermission.Check("GHDW"))
            {
                return null;
            }
            if (param.Length < 3)
            {
                throw new ArgumentException("参数错误,至少有3个参数");
            }
            string mC = (param[0] as string).Trim();
            string taxCode = (param[1] as string).Trim();
            string str3 = (param[2] as string).Trim();
            if (mC.Length == 0)
            {
                MessageBoxHelper.Show("请输入购货单位名称", "输入提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            if (str3.Length == 0)
            {
                MessageBoxHelper.Show("请输入购货单位身份证号码/组织机构代码", "输入提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            string str4 = "";
            string sJBM = "";
            switch (MessageBoxHelper.Show("是否有上级单位?", "输入确认", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Cancel:
                    return new object[] { "Cancel" };

                case DialogResult.Yes:
                {
                    if ((taxCode.Length != 0) && (this.ghdwmanager.QueryByTaxCode(taxCode).Rows.Count > 0))
                    {
                        MessageBoxHelper.Show("与此税号对应的购货单位已存在", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return new object[] { "Error" };
                    }
                    BMGHDWSelect select = new BMGHDWSelect();
                    if (select.ShowDialog() != DialogResult.OK)
                    {
                        return new object[] { str4 };
                    }
                    sJBM = select.SelectedSJBM;
                    break;
                }
            }
            if (this.ghdwmanager.QueryByMCAndSJBM(mC, sJBM).Rows.Count > 0)
            {
                MessageBoxHelper.Show("新增购货单位名称与其同级单位名称重复！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return new object[] { "Error" };
            }
            model = new BMGHDWModel {
                SJBM = sJBM,
                MC = mC,
                SH = taxCode,
                IDCOC = str3,
                WJ = 1
            };
            model.BM = this.ghdwmanager.TuiJianBM(model.SJBM);
            str4 = this.ghdwmanager.AddPurchaseKP(model, 1);
            if (str4 == "0")
            {
                str4 = "OK";
                return new object[] { str4 };
            }
            this.log.Info("新增购货单位失败:" + str4);
            return new object[] { "Error:", str4 };
        }
    }
}

