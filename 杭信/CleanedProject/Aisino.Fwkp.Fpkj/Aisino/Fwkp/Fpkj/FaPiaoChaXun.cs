namespace Aisino.Fwkp.Fpkj
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.Form.FPCX;
    using log4net;
    using System;
    using System.ComponentModel;
    using Framework.Plugin.Core.Util;
    public class FaPiaoChaXun : AbstractCommand
    {
        private static bool bMainExit;
        private static FaPiaoChaXunShow fpcx;
        private ILog loger = LogUtil.GetLogger<Aisino.Fwkp.Fpkj.FaPiaoChaXun>();

        private void fpcx_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                GC.Collect();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        protected override void RunCommand()
        {
            try
            {
                bMainExit = false;
                if ((fpcx != null) && fpcx.HasShow())
                {
                    fpcx.Dispose();
                    fpcx.Close();
                    fpcx = null;
                }
                SelectMonth.IsChaxun = true;
                DateTime cardClock = TaxCardFactory.CreateTaxCard().GetCardClock();
                fpcx = base.ShowForm<FaPiaoChaXunShow>() as FaPiaoChaXunShow;
                fpcx.TabText = "选择发票号码查询";
                fpcx.Text = "选择发票号码查询";
                fpcx.Month = cardClock.Month;
                fpcx.Year = cardClock.Year;
                fpcx.Edit(Aisino.Fwkp.Fpkj.Form.FPCX.FaPiaoChaXun.EditFPCX.ChaXun);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void runexit()
        {
            try
            {
                bMainExit = true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        protected override bool SetValid()
        {
            try
            {
                return GetTaxMode.GetTaxModValue();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return false;
            }
        }
    }
}

