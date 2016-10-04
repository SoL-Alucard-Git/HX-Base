namespace Aisino.Fwkp.Fpkj
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Fpkj.Common;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    public class ShareMethodClass : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<ShareMethodClass>();

        public ShareMethodClass()
        {
            try
            {
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.FaPiaoChaXun_FPFZ", "Aisino.Fwkp.Fpkj.dll", typeof(FPFZMethods).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.FaPiaoXiuFu", "Aisino.Fwkp.Fpkj.dll", typeof(FPXiuFuShareMethods).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.EmailSet", "Aisino.Fwkp.Fpkj.dll", typeof(EmailSetShareMethods).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.PopShareMethods", "Aisino.Fwkp.Fpkj.dll", typeof(PopShareMethods).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.EmailSendFPTK", "Aisino.Fwkp.Fpkj.dll", typeof(EmailSendFPTK).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.EmailOutFileShareMethods", "Aisino.Fwkp.Fpkj.dll", typeof(EmailOutFileShareMethods).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.FPChanXunPageShareMethods", "Aisino.Fwkp.Fpkj.dll", typeof(FPChanXunPageShareMethods).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.FPChanXunWenBenJieKouShareMethods", "Aisino.Fwkp.Fpkj.dll", typeof(FPChanXunWenBenJieKouShareMethods).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.FPYiKaiZuoFeiWenBenJieKouShareMethods", "Aisino.Fwkp.Fpkj.dll", typeof(FPYiKaiZuoFeiWenBenJieKouShareMethods).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.FPDYShareMethod", "Aisino.Fwkp.Fpkj.dll", typeof(FPDYShareMethod).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.FPALLDYShareMethods", "Aisino.Fwkp.Fpkj.dll", typeof(FPALLDYShareMethods).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.FPPrecisionShareMethod", "Aisino.Fwkp.Fpkj.dll", typeof(FPPrecisionShareMethod).FullName, null);
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpkj.FPXiuFuSingleShareMethods", "Aisino.Fwkp.Fpkj.dll", typeof(FPXiuFuSingleShareMethods).FullName, null);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }
    }
}

