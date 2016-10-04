namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using System;
    using System.Windows.Forms;

    public class ChaoShuiProcess : AbstractCommand
    {
        protected override void RunCommand()
        {
            if (TaxCardFactory.CreateTaxCard().get_ECardType() == 3)
            {
                ChaoshuiForm form = new ChaoshuiForm {
                    StartPosition = FormStartPosition.CenterScreen,
                    ShowInTaskbar = true
                };
                if (form.ShowTips())
                {
                    form.ShowDialog();
                }
            }
            else
            {
                ChaoshuiForm form2 = new ChaoshuiForm {
                    StartPosition = FormStartPosition.CenterScreen,
                    ShowInTaskbar = true
                };
                if (form2.ShowTips())
                {
                    form2.ShowDialog();
                }
            }
        }

        protected override bool SetValid()
        {
            return (TaxCardFactory.CreateTaxCard().get_TaxMode() == 2);
        }
    }
}

