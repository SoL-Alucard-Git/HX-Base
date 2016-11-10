namespace Aisino.Fwkp.Fpkj
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fpkj.Form.FPZF;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    public class ZuoFeiWeiKai_S : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<ZuoFeiWeiKai_S>();

        protected override void RunCommand()
        {
            FaPiaoZuoFei_WeiKai kai = new FaPiaoZuoFei_WeiKai();
            try
            {
                DockForm form = base.ShowForm<FaPiaoZuoFei_WeiKai>();
                if (form != null)
                {
                    form.Close();
                    kai.FaPiaoType = BusinessObject.FPLX.ZYFP;
                    if (kai.SetValue())
                    {
                        kai.ShowDialog();
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
            finally
            {
                if (kai != null)
                {
                    kai.Close();
                    kai.Dispose();
                    kai = null;
                }
            }
        }

        protected override bool SetValid()
        {
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                return ((card.TaxMode == CTaxCardMode.tcmHave) && card.QYLX.ISZYFP);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return false;
            }
        }
    }
}

