namespace Aisino.Fwkp.Xtsz
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Xtsz.BLL;
    using Aisino.Fwkp.Xtsz.Forms;
    using System;

    internal sealed class InitialService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            new Config().ResetAccountData();
            WizardParaSetForm form = new WizardParaSetForm();
            if ((form.m_bInitial && !form.IsInitialTaxCodeInXTSWXX()) && form.IsParamEmpty())
            {
                ParaSetBLL tbll = new ParaSetBLL();
                if (!tbll.UpdateXTSWXX_QYBH(TaxCardFactory.CreateTaxCard().get_TaxCode()))
                {
                    return new object[] { false };
                }
            }
            if (!form.m_bInitial || form.IsParamEmpty())
            {
                form.ShowDialog();
                return new object[] { false };
            }
            return new object[] { true };
        }
    }
}

