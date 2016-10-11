using InternetWare.Lodging.Data;
using Aisino.FTaxBase;
using static Aisino.Fwkp.Fpkj.Form.FPZF.FaPiaoZuoFei_WeiKai;
using Aisino.Fwkp.Fpkj.Form.FPZF;
using Aisino.Fwkp.BusinessObject;

namespace InternetWare.Util.Client
{
    internal class WeiKaiChaXunClient : BaseClient
    {
        private WeiKaiChaXunArgs _args;
        public WeiKaiChaXunClient(WeiKaiChaXunArgs args)
        {
            _args = args;
        }

        internal override ResultBase DoService()
        {
            FaPiaoZuoFei_WeiKai form = new FaPiaoZuoFei_WeiKai();
            form.FaPiaoType = CommonMethods.ParseFplx(_args.FpType);
            InvCodeNum invCodeNum = new InvCodeNum();
            if ("0000" != form.GetTaxCardCurrentFpNum(ref invCodeNum))
            {
                return new ResultBase(_args, null, true, new ErrorBase("查询发票信息失败"));
            }
            _InvoiceType invoiceType = form.GetInvoiceType(CommonMethods.ParseFplx(_args.FpType));
            int fpHasNum = form.GetTaxCardFPNum(invCodeNum.InvTypeCode, (int)invoiceType.TaxCardfpzl, Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(invCodeNum.InvNum));
            return new WeiKaiChaXunResult(_args, null, false, null, invoiceType.displayfpzl.Trim(), invCodeNum.InvTypeCode.Trim(), invCodeNum.InvNum.Trim(),fpHasNum);
        }
    }
}
