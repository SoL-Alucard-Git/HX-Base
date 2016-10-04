namespace Aisino.Fwkp.Fpkj
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.Form.FPCX;
    using Aisino.Fwkp.Fpkj.Form.FPXF;
    using log4net;
    using System;
    using System.Windows.Forms;

    public class FaPiaoXiuFu : AbstractCommand
    {
        private static SelectMonth dlgSelectMonth;
        private FpxfMain fpxfMain;
        private ILog loger = LogUtil.GetLogger<FaPiaoXiuFu>();

        protected override void RunCommand()
        {
            try
            {
                this.fpxfMain = new FpxfMain();
                if ((this.fpxfMain.xfThrad == null) || !this.fpxfMain.xfThrad.IsAlive)
                {
                    SelectMonth.IsChaxun = false;
                    SelectMonth.IsSingle = false;
                    dlgSelectMonth = new SelectMonth();
                    dlgSelectMonth.Text = "发票修复";
                    if (DialogResult.OK == dlgSelectMonth.ShowDialog())
                    {
                        this.fpxfMain.XFYear = dlgSelectMonth.Year;
                        this.fpxfMain.XFMonth = dlgSelectMonth.Month;
                        this.fpxfMain.XFFpdm = SelectMonth.FPDM;
                        this.fpxfMain.XFFPHM = SelectMonth.FPHM;
                        this.fpxfMain.IsSingle = SelectMonth.IsSingle;
                        this.fpxfMain.XFIsShowDialog = true;
                        if (dlgSelectMonth != null)
                        {
                            dlgSelectMonth.Close();
                            dlgSelectMonth.Dispose();
                            dlgSelectMonth = null;
                        }
                        this.fpxfMain.ShowDialog();
                        this.fpxfMain.AutoEvent.WaitOne();
                    }
                    if (this.fpxfMain != null)
                    {
                        this.fpxfMain.DisposeFormThread(this.fpxfMain);
                        this.fpxfMain = null;
                    }
                    this.loger.Error("发票修复线程运行结束，所有资源释放结束");
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        protected override bool SetValid()
        {
            try
            {
                return GetTaxMode.GetTaxModValue();
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                MessageManager.ShowMsgBox(exception2.Message);
                return false;
            }
        }
    }
}

