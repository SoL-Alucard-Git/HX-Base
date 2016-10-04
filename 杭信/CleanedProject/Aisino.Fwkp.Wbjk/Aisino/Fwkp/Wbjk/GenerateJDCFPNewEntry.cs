namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;
    using System.Windows.Forms;

    public sealed class GenerateJDCFPNewEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            SPFLService service = new SPFLService();
            if (CommonTool.isSPBMVersion() && (service.GetMaxBMBBBH() == "0.0"))
            {
                MessageBox.Show("商品和服务税收分类编码表为空，请先更新商品和服务税收分类编码表后，再开具发票！");
            }
            else
            {
                GenerateFP efp = new GenerateFP(InvType.vehiclesales, 3);
                string code = "";
                if (!efp.CanInvoice(12, out code))
                {
                    MessageManager.ShowMsgBox(code);
                }
                else
                {
                    efp.ShowDialog();
                }
            }
        }

        protected override bool SetValid()
        {
            try
            {
                bool iSJDC = false;
                iSJDC = TaxCardFactory.CreateTaxCard().get_QYLX().ISJDC;
                return (((WbjkEntry.RegFlag_JT || WbjkEntry.RegFlag_ST) || WbjkEntry.RegFlag_KT) && iSJDC);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return true;
        }
    }
}

