namespace Aisino.Fwkp.Fpkj.Entry
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fpkj.Form.FPCX;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;
    public class FaPiaoWebChaXun
    {
        private static bool bMainExit;
        private static SelectMonth dlgSelectMonth;
        private static FaPiaoChaXunShow fpcx;
        private ILog loger = LogUtil.GetLogger<FaPiaoChaXun>();

        private void fpcx_Closing(object sender, CancelEventArgs e)
        {
            try
            {
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public BaseForm RunCommand()
        {
            try
            {
                bMainExit = false;
                if ((fpcx == null) || !fpcx.HasShow())
                {
                    dlgSelectMonth = new SelectMonth();
                    if (DialogResult.OK == dlgSelectMonth.ShowDialog())
                    {
                        int month = dlgSelectMonth.Month;
                        int year = dlgSelectMonth.Year;
                        FaPiaoChaXun.CardClock = dlgSelectMonth.CardClock;
                        fpcx = new FaPiaoChaXunShow();
                        if (fpcx == null)
                        {
                            return null;
                        }
                        fpcx.TabText = "选择发票号码查询";
                        fpcx.Text = "选择发票号码查询";
                        fpcx.Month = month;
                        fpcx.Year = year;
                        fpcx.Edit(FaPiaoChaXun.EditFPCX.ChaXun);
                        fpcx.Closing += new CancelEventHandler(this.fpcx_Closing);
                        FormMain.ExecuteBeforeExitEvent += runexit;
                        return fpcx;
                    }
                }
                return null;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return null;
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
    }
}

