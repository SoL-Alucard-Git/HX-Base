namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;

    public sealed class GenerateSetSpecial : AbstractCommand
    {
        protected override void RunCommand()
        {
            GenerateInvSetForm form2 = new GenerateInvSetForm(InvType.Special) {
                IsSetOnly = 1
            };
            form2.ShowDialog();
        }

        protected override bool SetValid()
        {
            try
            {
                bool flag = false;
                TaxCard card = TaxCardFactory.CreateTaxCard();
                bool iSZYFP = card.get_QYLX().ISZYFP;
                if (card.get_StateInfo().CompanyType == 0)
                {
                    flag = false;
                }
                else
                {
                    flag = false;
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

