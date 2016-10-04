namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fptk;
    using Aisino.Fwkp.Fptk.Form;
    using log4net;
    using System;
    using System.Windows.Forms;
    using BusinessObject;
    public class NCPXSInvEntry : AbstractCommand
    {
        private ILog log = LogUtil.GetLogger<CommonInvEntry>();
        internal static InvoiceForm_ZZS ncpxsIfm;

        protected override void RunCommand()
        {
            try
            {
                if ((ncpxsIfm == null) || !ncpxsIfm.HasShow())
                {
                    SPFLService service = new SPFLService();
                    if (FLBM_lock.isFlbm() && (service.GetMaxBMBBBH() == "0.0"))
                    {
                        MessageManager.ShowMsgBox("INP-242133");
                    }
                    else if ((NCPSGInvEntry.ncpsgIfm != null) && NCPSGInvEntry.ncpsgIfm.HasShow())
                    {
                        string[] textArray1 = new string[] { "收购发票", "农产品销售发票" };
                        MessageManager.ShowMsgBox("INP-242165", textArray1);
                    }
                    else if ((CommonInvEntry.commonIfm != null) && CommonInvEntry.commonIfm.HasShow())
                    {
                        string[] textArray2 = new string[] { "一般普通发票", "农产品销售发票" };
                        MessageManager.ShowMsgBox("INP-242165", textArray2);
                    }
                    else
                    {
                        IFpManager manager = new FpManager();
                        if (manager.CanInvoice((FPLX)2))
                        {
                            string[] current = manager.GetCurrent((FPLX)2);
                            if (current != null)
                            {
                                if (new StartConfirmForm((FPLX)2, current).ShowDialog() == DialogResult.OK)
                                {
                                    ncpxsIfm = new InvoiceForm_ZZS((FPLX)2, (ZYFP_LX)8, current[0], current[1]);
                                    if (ncpxsIfm.InitSuccess)
                                    {
                                        ncpxsIfm.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                MessageManager.ShowMsgBox(manager.Code());
                            }
                        }
                        else
                        {
                            MessageManager.ShowMsgBox(manager.Code());
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.log.Error("农产品销售发票开具功能加载异常：" + exception.ToString());
                string[] textArray3 = new string[] { exception.ToString() };
                MessageManager.ShowMsgBox("INP-242158", textArray3);
            }
        }

        protected override bool SetValid()
        {
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                bool flag = card.QYLX.ISPTFP && card.QYLX.ISNCPXS;
                FpManager manager = new FpManager();
                string str = PropertyUtil.GetValue("HandMade", "0");
                return (((((int)card.TaxMode == 2) & flag) && !manager.IsSWDK()) && (str == "0"));
            }
            catch (Exception exception)
            {
                this.log.Error("金税卡接口异常:" + exception.ToString());
                return false;
            }
        }
    }
}

