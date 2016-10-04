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
    public class NCPSGInvEntry : AbstractCommand
    {
        private ILog log = LogUtil.GetLogger<NCPSGInvEntry>();
        internal static InvoiceForm_ZZS ncpsgIfm;

        protected override void RunCommand()
        {
            try
            {
                if ((ncpsgIfm == null) || !ncpsgIfm.HasShow())
                {
                    SPFLService service = new SPFLService();
                    if (FLBM_lock.isFlbm() && (service.GetMaxBMBBBH() == "0.0"))
                    {
                        MessageManager.ShowMsgBox("INP-242133");
                    }
                    else if ((NCPXSInvEntry.ncpxsIfm != null) && NCPXSInvEntry.ncpxsIfm.HasShow())
                    {
                        string[] textArray1 = new string[] { "农产品销售发票", "收购发票" };
                        MessageManager.ShowMsgBox("INP-242165", textArray1);
                    }
                    else if ((CommonInvEntry.commonIfm != null) && CommonInvEntry.commonIfm.HasShow())
                    {
                        string[] textArray2 = new string[] { "一般普通发票", "收购发票" };
                        MessageManager.ShowMsgBox("INP-242165", textArray2);
                    }
                    else
                    {
                        IFpManager manager = new FpManager();
                        TaxCardFactory.CreateTaxCard();
                        if (manager.CanInvoice((FPLX)2))
                        {
                            if (manager.GetCurrent((FPLX)2) != null)
                            {
                                if (new JSFPJSelect((FPLX)2).ShowDialog() == DialogResult.OK)
                                {
                                    string[] current = manager.GetCurrent((FPLX)2);
                                    if (current != null)
                                    {
                                        if (new StartConfirmForm((FPLX)2, current).ShowDialog() == DialogResult.OK)
                                        {
                                            ncpsgIfm = new InvoiceForm_ZZS((FPLX)2,(ZYFP_LX)9, current[0], current[1]);
                                            if (ncpsgIfm.InitSuccess)
                                            {
                                                ncpsgIfm.ShowDialog();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageManager.ShowMsgBox(manager.Code());
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
                this.log.Error("收购发票开具功能加载异常：" + exception.ToString());
                string[] textArray3 = new string[] { exception.ToString() };
                MessageManager.ShowMsgBox("INP-242159", textArray3);
            }
        }

        protected override bool SetValid()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            bool flag = card.QYLX.ISPTFP && card.QYLX.ISNCPSG;
            FpManager manager = new FpManager();
            string str = PropertyUtil.GetValue("HandMade", "0");
            return (((((int)card.TaxMode == 2) & flag) && !manager.IsSWDK()) && (str == "0"));
        }
    }
}

