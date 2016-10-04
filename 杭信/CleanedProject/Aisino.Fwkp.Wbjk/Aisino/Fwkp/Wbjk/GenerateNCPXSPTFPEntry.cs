namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;

    public sealed class GenerateNCPXSPTFPEntry : AbstractCommand
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
            InvSplitPara para = new InvSplitPara();
            para.GetInvSplitPara(InvType.Common);
            bool flag = taxCard.get_StateInfo().CompanyType != 0;
            if (para.ShowSetForm && flag)
            {
                GenerateInvSetForm form2 = new GenerateInvSetForm(InvType.Common) {
                    NCPBZ = 1
                };
                form2.ShowDialog();
            }
            else
            {
                GenerateFP efp = new GenerateFP(InvType.Common, 1);
                string code = "";
                if (!efp.CanInvoice(2, out code))
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
                bool flag3;
                bool flag4;
                bool flag = false;
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if (card.get_StateInfo().CompanyType == 0)
                {
                    flag3 = (WbjkEntry.RegFlag_JT || WbjkEntry.RegFlag_ST) || WbjkEntry.RegFlag_KT;
                    flag4 = card.get_QYLX().ISPTFP && card.get_QYLX().ISNCPXS;
                    flag = flag3 && flag4;
                }
                else
                {
                    flag3 = (WbjkEntry.RegFlag_JT || WbjkEntry.RegFlag_ST) || WbjkEntry.RegFlag_KT;
                    flag4 = card.get_QYLX().ISPTFP && card.get_QYLX().ISNCPXS;
                    flag = flag3 && flag4;
                }
                return false;
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

