namespace Aisino.Fwkp.Yccb
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using System;

    public class YCCBByAuto : AbstractCommand
    {
        protected override void RunCommand()
        {
            RemoteReport report = new RemoteReport {
                bByAuto = true
            };
            TaxCardFactory.CreateTaxCard().get_TaxClock().ToString("yyyyMM");
            if (report.CheckStatus())
            {
                if (report.ISZP)
                {
                    report.ProcessMsg(FPZL.ZP, OPTYPE.ZPCB);
                    if (report.ISCBZP)
                    {
                        PropertyUtil.SetValue(AttributeName.ZPCSDateName, report.dtZPLastCSDate.ToString("yyyyMM"));
                    }
                }
                if (report.ISHY)
                {
                    report.ProcessMsg(FPZL.HY, OPTYPE.HYCB);
                    if (report.ISCBHy)
                    {
                        PropertyUtil.SetValue(AttributeName.HYCSDateName, report.dtHYLastCSDate.ToString("yyyyMM"));
                    }
                }
                if (report.ISJDC)
                {
                    report.ProcessMsg(FPZL.JDC, OPTYPE.JDCCB);
                    if (report.ISCBJDC)
                    {
                        PropertyUtil.SetValue(AttributeName.JDCCSDateName, report.dtJDCLastCSDate.ToString("yyyyMM"));
                    }
                }
                if (report.ISJSP)
                {
                    report.ProcessMsg(FPZL.JSFP, OPTYPE.JSPCB);
                    if (report.ISCBJSFP)
                    {
                        PropertyUtil.SetValue(AttributeName.JSPCSDateName, report.dtJSPLastCSDate.ToString("yyyyMM"));
                    }
                }
                PropertyUtil.Save();
            }
        }

        protected override bool SetValid()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            return ((card.get_TaxMode() == 2) && !card.get_QYLX().ISTDQY);
        }
    }
}

