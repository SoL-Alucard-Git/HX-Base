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
    public class CargoInvEntry : AbstractCommand
    {
        internal static HYInvoiceForm hyfpfm;
        private ILog log = LogUtil.GetLogger<CargoInvEntry>();

        protected override void RunCommand()
        {
            try
            {
                if ((hyfpfm == null) || !hyfpfm.HasShow())
                {
                    SPFLService service = new SPFLService();
                    if (FLBM_lock.isFlbm() && (service.GetMaxBMBBBH() == "0.0"))
                    {
                        MessageManager.ShowMsgBox("INP-242133");
                    }
                    else
                    {
                        IFpManager manager = new FpManager();
                        if (manager.CanInvoice((FPLX)11))
                        {
                            string[] current = manager.GetCurrent((FPLX)11);
                            if (current != null)
                            {
                                if (new StartConfirmForm((FPLX)11, current).ShowDialog() == DialogResult.OK)
                                {
                                    hyfpfm = new HYInvoiceForm((FPLX)11, current[0], current[1]);
                                    if (hyfpfm.InitSuccess)
                                    {
                                        hyfpfm.ShowDialog();
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
                this.log.Error("货物运输业增值税专用发票开具功能加载异常：" + exception.ToString());
                string[] textArray1 = new string[] { exception.ToString() };
                MessageManager.ShowMsgBox("INP-242155", textArray1);
            }
        }

        protected override bool SetValid()
        {
            TaxCard card1 = TaxCardFactory.CreateTaxCard();
            bool iSHY = card1.QYLX.ISHY;
            FpManager manager = new FpManager();
            string str = PropertyUtil.GetValue("HandMade", "0");
            return (((((int)card1.TaxMode == 2) & iSHY) && !manager.IsSWDK()) && (str == "0"));
        }
    }
}

