namespace Aisino.Fwkp.Fplygl.Form.FPLYGL_1
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DrxgfpFromCard : BaseForm
    {
        protected Dictionary<string, object> _dictFPLBBM;
        private IContainer components;
        private ILog loger = LogUtil.GetLogger<DrxgfpFromCard>();

        public DrxgfpFromCard()
        {
            try
            {
                this.Initialize();
                this._dictFPLBBM = ShareMethods.GetFPLBBM();
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0x10a);
            base.Name = "DrxgfpFromCard";
            this.Text = "从金税设备读入新购发票";
            base.ResumeLayout(false);
        }

        public void Run()
        {
            try
            {
                if (DialogResult.Yes != MessageManager.ShowMsgBox("INP-441211"))
                {
                    base.Close();
                }
                else
                {
                    List<InvVolumeApp> invList = null;
                    List<InvStockRepeat> noReadInvStock = null;
                    string deviceType = string.Empty;
                    if (1 == base.TaxCardInstance.get_StateInfo().TBRegFlag)
                    {
                        ChooseReadMedium medium = new ChooseReadMedium();
                        if (DialogResult.OK != medium.ShowDialog())
                        {
                            return;
                        }
                        deviceType = medium.GetDeviceType();
                    }
                    else
                    {
                        deviceType = "1";
                    }
                    InvReadReValue value2 = base.TaxCardInstance.InvRead(deviceType, null, 0, 0);
                    invList = value2.ReadNewInvStock;
                    noReadInvStock = value2.NoReadInvStock;
                    List<NoReadInvStock> allRepeats = new List<NoReadInvStock>();
                    if (noReadInvStock != null)
                    {
                        foreach (InvStockRepeat repeat in noReadInvStock)
                        {
                            foreach (NoReadInvStock stock in repeat.ErrInfo)
                            {
                                allRepeats.Add(stock);
                            }
                        }
                    }
                    if ((base.TaxCardInstance.get_RetCode() != 0) && (510 != base.TaxCardInstance.get_RetCode()))
                    {
                        MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                    }
                    else if ((invList == null) || (invList.Count == 0))
                    {
                        MessageManager.ShowMsgBox("INP-441212");
                    }
                    else if (invList.Count > 0)
                    {
                        InvInfoReadMsg msg = new InvInfoReadMsg();
                        msg.InsertInvVolume(invList);
                        msg.ShowDialog();
                        List<InvVolumeApp> invVols = ShareMethods.FilterOutSpecType(invList, 0x29);
                        if (invVols.Count > 0)
                        {
                            new SetFormat(invVols, "NEW76mmX177mm").ShowDialog();
                        }
                    }
                    if (allRepeats.Count > 0)
                    {
                        InvErrMsg msg2 = new InvErrMsg();
                        msg2.InsertData(allRepeats);
                        msg2.ShowDialog();
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

