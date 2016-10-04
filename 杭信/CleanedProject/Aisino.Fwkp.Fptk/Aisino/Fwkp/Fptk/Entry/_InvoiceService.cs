namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk;
    using System;

    internal sealed class _InvoiceService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if (param.Length != 1)
            {
                return null;
            }
            Invoice invoice = param[0] as Invoice;
            if (invoice == null)
            {
                return null;
            }
            IFpManager manager = new FpManager();
            if ((((invoice.Fplx == (FPLX)2) || (invoice.Fplx == (FPLX)0x33)) || (invoice.Fplx == (FPLX)0x29)) && (invoice.Zyfplx == (ZYFP_LX)9))
            {
                invoice.Gfsh=manager.GetXfsh();
                invoice.Gfmc=manager.GetXfmc();
            }
            else
            {
                invoice.Xfsh=manager.GetXfsh();
                invoice.Xfmc=manager.GetXfmc();
            }
            if (!FLBM_lock.isFlbm())
            {
                invoice.Bmbbbh="";
            }
            Fpxx fpData = invoice.GetFpData();
            if (fpData != null)
            {
                if ((invoice.IsRed && (fpData.fplx == 0)) && !manager.CheckRedNum(fpData.redNum, fpData.fplx))
                {
                    return new object[] { "INP-242106", fpData.redNum };
                }
                if (invoice.MakeCardInvoice(fpData, false))
                {
                    if (manager.SaveXxfp(fpData))
                    {
                        return new object[] { "0000", fpData.fpdm, fpData.fphm };
                    }
                    return new object[] { "INP-242111" };
                }
            }
            return new object[] { invoice.GetCode() };
        }
    }
}

