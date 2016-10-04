namespace Aisino.Fwkp.Fplygl.Form.FJFPGL_2
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class ShfjgpForm : BaseForm
    {
        private IContainer components;
        private ILog loger = LogUtil.GetLogger<ShfjtpForm>();

        public ShfjgpForm()
        {
            try
            {
                this.Initialize();
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Initialize()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "ShfjgpForm";
        }

        public void Run()
        {
            try
            {
                if (DialogResult.OK != MessageManager.ShowMsgBox("INP-441224"))
                {
                    base.Close();
                }
                else
                {
                    base.TaxCardInstance.GetStateInfo(false);
                    int invType = -1;
                    string deviceType = string.Empty;
                    ChooseRevokeMedium medium = new ChooseRevokeMedium();
                    if (DialogResult.OK == medium.ShowDialog())
                    {
                        invType = medium.GetInvType();
                        deviceType = medium.GetDeviceType();
                    }
                    else
                    {
                        return;
                    }
                    List<InvVolumeApp> invList = base.TaxCardInstance.ReadFJNewInv(invType, deviceType);
                    if (0 < base.TaxCardInstance.get_RetCode())
                    {
                        MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                    }
                    else if ((invList == null) || (0 >= invList.Count))
                    {
                        MessageManager.ShowMsgBox("INP-441225");
                    }
                    else
                    {
                        InvInfoNewReclaimMsg msg = new InvInfoNewReclaimMsg();
                        msg.InsertInvVolume(invList);
                        msg.ShowDialog();
                        List<InvVolumeApp> invVols = ShareMethods.FilterOutSpecType(invList, 0x29);
                        if (invVols.Count > 0)
                        {
                            new SetFormat(invVols, "NEW76mmX177mm").ShowDialog();
                        }
                    }
                    base.Close();
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
    }
}

