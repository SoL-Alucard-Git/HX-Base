namespace Aisino.Fwkp.Fplygl.Entry.WebModualEntry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.WLSL_5;
    using Aisino.Fwkp.Fplygl.IBLL;
    using log4net;
    using System;
    using System.Windows.Forms;

    public sealed class Apply_ApplyFormEntry : AbstractCommand
    {
        private string adminType = string.Empty;
        private string cofPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Bin\");
        private ILog loger = LogUtil.GetLogger<Apply_ApplyFormEntry>();
        private readonly ILYGL_PSXX psxxDal = BLLFactory.CreateInstant<ILYGL_PSXX>("LYGL_PSXX");
        private TaxCard TaxCardInstance = TaxCardFactory.CreateTaxCard();

        protected override void RunCommand()
        {
            try
            {
                string driverVersion = Aisino.Fwkp.Fplygl.Common.Tool.Instance().GetDriverVersion();
                string softVersion = Aisino.Fwkp.Fplygl.Common.Tool.Instance().GetSoftVersion();
                string str3 = driverVersion.Substring(7, 2) + driverVersion.Substring(10, 6);
                if (softVersion.Equals("FWKP_V2.0_Svr_Client"))
                {
                    if (str3.CompareTo("A0150729") < 0)
                    {
                        MessageManager.ShowMsgBox("INP-441291");
                        return;
                    }
                }
                else if (str3.CompareTo("L0110501") < 0)
                {
                    MessageManager.ShowMsgBox("INP-441291");
                    return;
                }
                if (ShareMethods.ApplyAdminCheck("申领"))
                {
                    string adminType = ApplyCommon.GetAdminType();
                    if (adminType.Equals("JS"))
                    {
                        string str5 = PropertyUtil.GetValue("Aisino.Fwkp.Fplygl.WLSL_5.ExcutiveUpdateTime");
                        bool flag = false;
                        string strB = this.TaxCardInstance.GetCardClock().ToString("yyyyMMdd");
                        if (str5.CompareTo(strB) < 0)
                        {
                            flag = true;
                        }
                        else if (this.psxxDal.CountSynAddrItems() <= 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                        if (flag)
                        {
                            ApplyCommon.SynAddressExcutive();
                        }
                    }
                    string str7 = PropertyUtil.GetValue("Aisino.Fwkp.Fplygl.WLSL_5.ApplyLegalNotice.Repeat");
                    if (string.IsNullOrEmpty(str7) || str7.Equals("1"))
                    {
                        ApplyLegalNotice notice = new ApplyLegalNotice(false);
                        if (DialogResult.Yes != notice.ShowDialog())
                        {
                            return;
                        }
                    }
                    new ApplyVolume(adminType.Equals("JS")).ShowDialog();
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected override bool SetValid()
        {
            try
            {
                if (!GetTaxMode.GetTaxModValue())
                {
                    return false;
                }
                if (!PopUpBox.NoIsZhu_KaiPiaoJi(GetTaxMode.GetTaxCard(), GetTaxMode.GetTaxStateInfo()))
                {
                    return false;
                }
                if (GetTaxMode.GetTaxCard().get_QYLX().ISTDQY)
                {
                    return false;
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return true;
        }
    }
}

