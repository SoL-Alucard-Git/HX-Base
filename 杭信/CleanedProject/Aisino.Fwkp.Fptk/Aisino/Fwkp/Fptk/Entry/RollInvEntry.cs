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
    public sealed class RollInvEntry : AbstractCommand
    {
        internal static InvoiceForm_JS commonIfm;
        private ILog log = LogUtil.GetLogger<RollInvEntry>();

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
                    else
                    {
                        IFpManager manager = new FpManager();
                        if (manager.CanInvoice((FPLX)0x29))
                        {
                            if (manager.GetCurrent((FPLX)0x29) != null)
                            {
                                if (new JSFPJSelect((FPLX)0x29).ShowDialog() == DialogResult.OK)
                                {
                                    string[] current = manager.GetCurrent((FPLX)0x29);
                                    if (current != null)
                                    {
                                        if (new StartConfirmForm((FPLX)0x29, current).ShowDialog() == DialogResult.OK)
                                        {
                                            commonIfm = new InvoiceForm_JS((FPLX)0x29, (ZYFP_LX)0, current[0], current[1]);
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
                string[] textArray1 = new string[] { exception.ToString() };
                MessageManager.ShowMsgBox("INP-242114", textArray1);
            }
        }

        protected override bool SetValid()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            new FpManager();
            string str = PropertyUtil.GetValue("HandMade", "0");
            return ((((int)card.TaxMode == 2) && card.QYLX.ISPTFPJSP) && (str == "0"));
        }
    }
}

