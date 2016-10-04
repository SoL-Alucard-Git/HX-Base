namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk.Form;
    using System;
    using System.Windows.Forms;

    internal sealed class SELECTJSFPService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            object[] objArray = new object[] { false };
            if (param.Length == 1)
            {
                FPLX fplx = Invoice.ParseFPLX(param[0].ToString());
                InvoiceType type = (InvoiceType)2;
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if (((int)fplx != 0x29) && ((int)fplx != 2))
                {
                    return objArray;
                }
                if ((int)fplx == 0x29)
                {
                    type = (InvoiceType)0x29;
                }
                card.GetCurrentInvCode(type);
                if (card.RetCode != 0)
                {
                    MessageManager.ShowMsgBox(card.ErrCode);
                    objArray[0] = false;
                    return objArray;
                }
                if (new JSFPJSelect(fplx).ShowDialog() == DialogResult.OK)
                {
                    objArray[0] = true;
                }
            }
            return objArray;
        }
    }
}

