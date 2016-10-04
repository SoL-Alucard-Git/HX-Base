namespace Aisino.Fwkp.Fpkj
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.Form.FPZF;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    public class ZuoFeiWeiKai_Q : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<ZuoFeiWeiKai_Q>();

        protected override void RunCommand()
        {
            FaPiaoZuoFei_WeiKai kai = new FaPiaoZuoFei_WeiKai();
            try
            {
                object[] objArray = new object[] { "q" };
                object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.SELECTJSFP", objArray);
                if ((objArray2 != null) && Tool.ObjectToBool(objArray2[0]))
                {
                    DockForm form = base.ShowForm<FaPiaoZuoFei_WeiKai>();
                    if (form != null)
                    {
                        form.Close();
                        kai.FaPiaoType = BusinessObject.FPLX.JSFP;
                        if (kai.SetValue())
                        {
                            kai.ShowDialog();
                        }
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
                return ((card.TaxMode == CTaxCardMode.tcmHave) && card.QYLX.ISPTFPJSP);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return false;
            }
        }
    }
}

