namespace Aisino.Fwkp.Wbjk.Model
{
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using System;

    public class SaleBillInfo : SaleBill
    {
        public void setDj(int mxID, double dDj)
        {
            Goods good = base.GetGood(mxID);
            if (base.ContainTax)
            {
                double num = dDj;
                double num2 = num * good.SL;
                good.HSJBZ = true;
                good.DJ = num;
                good.JE = Finacial.Div(num2, 1.0 + good.SLV, 2);
                good.SE = SaleBillCtrl.GetRound((double) (num2 - good.JE), 2);
            }
            else
            {
                good.HSJBZ = false;
                good.DJ = dDj;
                good.JE = SaleBillCtrl.GetRound((double) (dDj * good.SL), 2);
                good.SE = SaleBillCtrl.GetRound((double) (good.JE * good.SLV), 2);
            }
        }
    }
}

