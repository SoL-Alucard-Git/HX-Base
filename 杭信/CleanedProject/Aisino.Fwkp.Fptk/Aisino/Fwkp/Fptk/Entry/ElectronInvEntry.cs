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
    public sealed class ElectronInvEntry : AbstractCommand
    {
        internal static InvoiceForm_DZ commonIfm;
        private ILog log = LogUtil.GetLogger<ElectronInvEntry>();

        protected override void RunCommand()
        {
            try
            {
                if ((commonIfm == null) || !commonIfm.HasShow())
                {
                    SPFLService service = new SPFLService();
                    if (FLBM_lock.isFlbm() && (service.GetMaxBMBBBH() == "0.0"))
                    {
                        MessageManager.ShowMsgBox("INP-242133");
                    }
                    else if ((NCPXSInvEntry.ncpxsIfm != null) && NCPXSInvEntry.ncpxsIfm.HasShow())
                    {
                        string[] textArray1 = new string[] { "农产品销售发票", "一般普通发票" };
                        MessageManager.ShowMsgBox("INP-242165", textArray1);
                    }
                    else if ((NCPSGInvEntry.ncpsgIfm != null) && NCPSGInvEntry.ncpsgIfm.HasShow())
                    {
                        string[] textArray2 = new string[] { "收购发票", "一般普通发票" };
                        MessageManager.ShowMsgBox("INP-242165", textArray2);
                    }
                    else
                    {
                        IFpManager manager = new FpManager();
                        if (manager.CanInvoice((FPLX)0x33))
                        {
                            string[] current = manager.GetCurrent((FPLX)0x33);
                            if (current != null)
                            {
                                if (new StartConfirmForm((FPLX)0x33, current).ShowDialog() == DialogResult.OK)
                                {
                                    commonIfm = new InvoiceForm_DZ((FPLX)0x33, 0, current[0], current[1]);
                                    if (commonIfm.InitSuccess)
                                    {
                                        commonIfm.ShowDialog();
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
                this.log.Error("电子增值税普通发票开具功能加载异常：" + exception.ToString());
                string[] textArray3 = new string[] { exception.ToString() };
                MessageManager.ShowMsgBox("INP-242114", textArray3);
            }
        }

        protected override bool SetValid()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            new FpManager();
            string str = PropertyUtil.GetValue("HandMade", "0");
            return ((((int)card.TaxMode == 2) && card.QYLX.ISPTFPDZ) && (str == "0"));
        }
    }
}

