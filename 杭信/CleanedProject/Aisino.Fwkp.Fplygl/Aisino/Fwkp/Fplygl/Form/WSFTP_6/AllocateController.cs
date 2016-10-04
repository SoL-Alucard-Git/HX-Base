namespace Aisino.Fwkp.Fplygl.Form.WSFTP_6
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Form.Common;
    using Aisino.Fwkp.Fplygl.Form.WSFTP_6.Common;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class AllocateController : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<AllocateController>();
        private TaxCard TaxCardInstance = TaxCardFactory.CreateTaxCard();

        public void Run()
        {
            bool flag = true;
            UnlockInvoice unlockVolume = this.TaxCardInstance.NInvSearchUnlockFromMain();
            if (this.TaxCardInstance.get_RetCode() == 0)
            {
                flag = DownloadCommon.CheckEmpty(unlockVolume.Buffer);
            }
            else
            {
                flag = true;
            }
            if (!flag && (DialogResult.Yes == MessageManager.ShowMsgBox("INP-4412B1")))
            {
                List<InvVolumeApp> invList = new List<InvVolumeApp>();
                bool isDeviceError = false;
                string str = string.Empty;
                InvVolumeApp item = new InvVolumeApp();
                if (AllocateCommon.AllocateOneVolume(unlockVolume, out isDeviceError, out str))
                {
                    item.InvType = Convert.ToByte(unlockVolume.get_Kind());
                    item.TypeCode = unlockVolume.get_TypeCode();
                    item.HeadCode = Convert.ToUInt32(unlockVolume.get_Number());
                    item.Number = Convert.ToUInt16(unlockVolume.get_Count());
                    invList.Add(item);
                    InvInfoWebAllocMsg msg = new InvInfoWebAllocMsg();
                    msg.InsertInvVolume(invList);
                    msg.ShowDialog();
                }
                else if (isDeviceError)
                {
                    MessageManager.ShowMsgBox(this.TaxCardInstance.get_ErrCode());
                }
                else
                {
                    MessageManager.ShowMsgBox(str);
                }
            }
        }
    }
}

