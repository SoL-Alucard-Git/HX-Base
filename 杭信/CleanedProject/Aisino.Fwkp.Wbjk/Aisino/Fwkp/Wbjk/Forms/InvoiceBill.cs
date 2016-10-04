namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Fwkp.BusinessObject;
    using System;

    public class InvoiceBill : Invoice
    {
        public InvoiceBill(bool isRed, bool qdbz, bool hsjbz, FPLX fplx, byte[] val) : base(isRed, qdbz, hsjbz, fplx, val, null)
        {
        }

        private bool CheckSLv(string sLv, int flag)
        {
            return true;
        }

        public bool SetFpSLv(string slv)
        {
            Invoice.set_IsGfSqdFp_Static(true);
            bool flag = base.SetFpSLv(slv);
            Invoice.set_IsGfSqdFp_Static(false);
            return flag;
        }
    }
}

