namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;
    using System.Windows.Forms;

    public sealed class GenerateZYFPEntry : AbstractCommand
    {
        private bool CanXTInv(TaxCard taxCard)
        {
            try
            {
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMIsNeedImportXTSP", null);
                if ((((objArray != null) && (objArray.Length > 0)) && (objArray[0] != null)) && Convert.ToBoolean(objArray[0]))
                {
                    ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMImportXTSP", null);
                }
                if (taxCard.get_QYLX().ISXT)
                {
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMCheckXTSP", null);
                    if (!(((objArray2 != null) && (objArray2[0] is bool)) && Convert.ToBoolean(objArray2[0])))
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        protected override void RunCommand()
        {
            TaxCard taxCard = TaxCardFactory.CreateTaxCard();
            new InvSplitPara().GetInvSplitPara(InvType.Special);
            SPFLService service = new SPFLService();
            if (CommonTool.isSPBMVersion() && (service.GetMaxBMBBBH() == "0.0"))
            {
                MessageBox.Show("商品和服务税收分类编码表为空，请先更新商品和服务税收分类编码表后，再开具发票！");
            }
            else
            {
                GenerateFP efp = new GenerateFP(InvType.Special);
                string code = "";
                if (!efp.CanInvoice(0, out code))
                {
                    MessageManager.ShowMsgBox(code);
                }
                else if (!this.CanXTInv(taxCard))
                {
                    MessageManager.ShowMsgBox("INP-242132");
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
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if (card.get_StateInfo().CompanyType == 0)
                {
                    bool flag3 = ((WbjkEntry.RegFlag_JT || WbjkEntry.RegFlag_ST) || WbjkEntry.RegFlag_KT) || WbjkEntry.RegFlag_JS;
                    bool iSZYFP = card.get_QYLX().ISZYFP;
                    return (flag3 && iSZYFP);
                }
                return card.get_QYLX().ISZYFP;
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

