namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk;
    using System;

    public class ShareCommonEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            ServiceFactory.RegPubService("Aisino.Fwkp.QueryFPMX", "Aisino.Fwkp.Fptk.dll", "Aisino.Fwkp.Fptk.Entry._QueryFPMXService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.QueryFPXX", "Aisino.Fwkp.Fptk.dll", "Aisino.Fwkp.Fptk.Entry._QueryFPXXService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Invoice", "Aisino.Fwkp.Fptk.dll", "Aisino.Fwkp.Fptk.Entry._InvoiceService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.SaveFPXX", "Aisino.Fwkp.Fptk.dll", "Aisino.Fwkp.Fptk.Entry._SaveFPXXService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.QueryYKRedJE", "Aisino.Fwkp.Fptk.dll", "Aisino.Fwkp.Fptk.Entry._QueryTRedJEService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.UploadSWDKFP", "Aisino.Fwkp.Fptk.dll", "Aisino.Fwkp.Fptk.Entry.UploadSWDKFPService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.CheckHZXXBBH", "Aisino.Fwkp.Fptk.dll", "Aisino.Fwkp.Fptk.Entry.CheckHZXXBBHPService", null);
            TaxCardFactory.CreateTaxCard().GetInvoiceQryNo += tc_GetInvoiceQryNo;
            ServiceFactory.RegPubService("Aisino.Fwkp.SELECTJSFP", "Aisino.Fwkp.Fptk.dll", "Aisino.Fwkp.Fptk.Entry.SELECTJSFPService", null);
        }

        private string[] tc_GetInvoiceQryNo(string fpdm, string fphm, InvoiceType fpzl)
        {
            Fpxx fpxx = new FpManager().GetXxfp((FPLX)fpzl, fpdm, int.Parse(fphm));
            if (fpxx != null)
            {
                return new string[] { fpxx.keyFlagNo.ToString(), fpxx.invQryNo.ToString() };
            }
            return new string[] { "0", "-1" };
        }
    }
}

