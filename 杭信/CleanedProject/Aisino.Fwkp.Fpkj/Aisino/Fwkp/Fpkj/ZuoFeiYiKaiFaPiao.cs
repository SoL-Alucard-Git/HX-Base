namespace Aisino.Fwkp.Fpkj
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.DAL;
    using Aisino.Fwkp.Fpkj.Form.FPCX;
    using Aisino.Fwkp.Fpkj.Form.FPZF;
    using log4net;
    using System;
    using System.Collections.Generic;

    public class ZuoFeiYiKaiFaPiao : AbstractCommand
    {
        private static FaPiaoZuoFei_YiKai fpzf;
        private ILog loger = LogUtil.GetLogger<ZuoFeiYiKaiFaPiao>();
        private XXFP xxfpChaXunBll = new XXFP(false);

        protected override void RunCommand()
        {
            try
            {
                GC.Collect();
                if ((fpzf == null) || !fpzf.HasShow())
                {
                    FaPiaoZuoFei_YiKai.CardClock = TaxCardFactory.CreateTaxCard().GetCardClock();
                    Dictionary<string, object> condition = new Dictionary<string, object>();
                    string data = FaPiaoZuoFei_YiKai.CardClock.ToString("yyyyMM");
                    condition.Add("Date", Tool.ObjectToInt(data));
                    if (this.xxfpChaXunBll.SelectYiKaiZuoFeiFpCount(condition) == 0)
                    {
                        MessageManager.ShowMsgBox("FPCX-000021");
                    }
                    else
                    {
                        DockForm form = base.ShowForm<FaPiaoZuoFei_YiKai>();
                        if (form != null)
                        {
                            fpzf = form as FaPiaoZuoFei_YiKai;
                            fpzf.Visible = false;
                            fpzf.TabText = "选择发票号码作废";
                            fpzf.Text = "选择发票号码作废";
                            fpzf.Edit(Aisino.Fwkp.Fpkj.Form.FPCX.FaPiaoChaXun.EditFPCX.ZuoFei);
                        }
                    }
                }
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
                TaxCard card = TaxCardFactory.CreateTaxCard();
                return ((card.TaxMode == CTaxCardMode.tcmHave) && (((card.QYLX.ISPTFP || card.QYLX.ISZYFP) || (card.QYLX.ISHY || card.QYLX.ISJDC)) || card.QYLX.ISPTFPJSP));
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return false;
            }
        }
    }
}

