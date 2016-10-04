namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;

    public sealed class GenerateSetCommon : AbstractCommand
    {
        protected override void RunCommand()
        {
            GenerateInvSetForm form2 = new GenerateInvSetForm(InvType.Common) {
                IsSetOnly = 1
            };
            form2.ShowDialog();
        }

        protected override bool SetValid()
        {
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                bool iSPTFP = card.get_QYLX().ISPTFP;
                bool flag2 = card.get_StateInfo().CompanyType != 0;
                bool flag3 = false;
                if (!flag2)
                {
                    flag3 = false;
                }
                else if (true)
                {
                    if (WbjkEntry.RegFlag_JS)
                    {
                        flag3 = false;
                    }
                    if ((WbjkEntry.RegFlag_JT || WbjkEntry.RegFlag_KT) || WbjkEntry.RegFlag_ST)
                    {
                        flag3 = true;
                    }
                }
                return (flag3 && iSPTFP);
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

