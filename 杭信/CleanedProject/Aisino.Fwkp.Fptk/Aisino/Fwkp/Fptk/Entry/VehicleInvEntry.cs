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
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using BusinessObject;
    public class VehicleInvEntry : AbstractCommand
    {
        private ILog log = LogUtil.GetLogger<CommonInvEntry>();
        internal static JDCInvoiceForm_new newJdcfpfm;
        internal static JDCInvoiceForm_old oldJdcfpfm;

        private string GetJdcVerSelect()
        {
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Config\Common\JDCVerSelect.xml");
            try
            {
                if (!File.Exists(path))
                {
                    return "";
                }
                XmlDocument document1 = new XmlDocument();
                document1.Load(path);
                XmlNode node = document1.SelectSingleNode("/JDCVersion");
                if (node == null)
                {
                    return "";
                }
                return node.InnerText;
            }
            catch (Exception exception)
            {
                this.log.Error(exception.Message, exception);
                return "";
            }
        }

        protected override void RunCommand()
        {
            try
            {
                if (((oldJdcfpfm == null) || !oldJdcfpfm.HasShow()) && ((newJdcfpfm == null) || !newJdcfpfm.HasShow()))
                {
                    SPFLService service = new SPFLService();
                    if (FLBM_lock.isFlbm() && (service.GetMaxBMBBBH() == "0.0"))
                    {
                        MessageManager.ShowMsgBox("INP-242133");
                    }
                    else
                    {
                        IFpManager manager = new FpManager();
                        if (manager.CanInvoice((FPLX)12))
                        {
                            string[] current = manager.GetCurrent((FPLX)12);
                            if (current != null)
                            {
                                if (new StartConfirmForm((FPLX)12, current).ShowDialog() == DialogResult.OK)
                                {
                                    string jdcVerSelect = this.GetJdcVerSelect();
                                    switch ("1")
                                    {
                                        case "":
                                            new JDCVersionSet(current[0], current[1], JDCVersion.NULL).ShowDialog();
                                            return;

                                        case "0":
                                            oldJdcfpfm = new JDCInvoiceForm_old((FPLX)12, current[0], current[1]);
                                            if (oldJdcfpfm.InitSuccess)
                                            {
                                                oldJdcfpfm.ShowDialog();
                                            }
                                            break;

                                        case "1":
                                            newJdcfpfm = new JDCInvoiceForm_new((FPLX)12, current[0], current[1]);
                                            if (newJdcfpfm.InitSuccess)
                                            {
                                                newJdcfpfm.ShowDialog();
                                            }
                                            break;
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
                this.log.Error("机动车销售统一发票开具功能加载异常：" + exception.ToString());
                string[] textArray1 = new string[] { exception.ToString() };
                MessageManager.ShowMsgBox("INP-242156", textArray1);
            }
        }

        protected override bool SetValid()
        {
            TaxCard card1 = TaxCardFactory.CreateTaxCard();
            bool iSJDC = card1.QYLX.ISJDC;
            FpManager manager = new FpManager();
            string str = PropertyUtil.GetValue("HandMade", "0");
            return (((((int)card1.TaxMode == 2) & iSJDC) && !manager.IsSWDK()) && (str == "0"));
        }
    }
}

