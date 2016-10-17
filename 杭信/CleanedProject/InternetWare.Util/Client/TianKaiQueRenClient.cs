using Aisino.FTaxBase;
using Aisino.Fwkp.Fptk.Form;
using InternetWare.Lodging.Data;
using System.Collections.Generic;

namespace InternetWare.Util.Client
{
    internal class TianKaiQueRenClient : BaseClient
    {
        private TianKaiQueRenArgs _args;
        public TianKaiQueRenClient(TianKaiQueRenArgs args)
        {
            _args = args;
        }
        internal override BaseResult DoService()
        {
            JSFPJSelect form = new JSFPJSelect(CommonMethods.ParseFplx(_args.FpType));
            List<InvVolumeApp> list = form.TaxCardInstance.GetInvStock();
            List<TianKaiQueRenResultEntity> resultList = new List<TianKaiQueRenResultEntity>();
            InvVolumeApp iva;
            if (form.mfplx == Aisino.Fwkp.BusinessObject.FPLX.JSFP && (iva = list.Find(p => p.InvType == 0x29)) != null)
                resultList.Add(new TianKaiQueRenResultEntity ("增值税普通发票(卷票)", iva.TypeCode, JSFPJSelect.FPHMTo8Wei(iva.HeadCode), iva.Number ));
            if (form.mfplx == Aisino.Fwkp.BusinessObject.FPLX.PTFP && (iva = list.Find(p => p.InvType == 2)) != null)
                resultList.Add(new TianKaiQueRenResultEntity ( "增值税普通发票", iva.TypeCode, JSFPJSelect.FPHMTo8Wei(iva.HeadCode), iva.Number ));
            return new TianKaiQueRenResult(_args, resultList);
        }

    }
}
