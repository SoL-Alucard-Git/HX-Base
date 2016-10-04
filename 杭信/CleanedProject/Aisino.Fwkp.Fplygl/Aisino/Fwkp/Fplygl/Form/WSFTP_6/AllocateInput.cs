namespace Aisino.Fwkp.Fplygl.Form.WSFTP_6
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.Common;
    using Aisino.Fwkp.Fplygl.Form.WSFTP_6.Common;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AllocateInput : DockForm
    {
        private AisinoBTN btnAllocate;
        private AisinoBTN btnCancel;
        private IContainer components;
        private AisinoLBL lblEndNumInfo;
        private AisinoLBL lblInvTypeInfo;
        private AisinoLBL lblStartNumAlloc;
        private AisinoLBL lblStartNumInfo;
        private ILog loger = LogUtil.GetLogger<AllocateInput>();
        private List<object> SelectedItems = new List<object>();
        private TextBoxRegex tbxAmountAlloc;
        private TextBoxRegex tbxMachineAlloc;
        protected XmlComponentLoader xmlComponentLoader1;

        public AllocateInput(List<object> oSelectedItems)
        {
            this.Initialize();
            foreach (object obj2 in oSelectedItems)
            {
                this.SelectedItems.Add(obj2.ToString());
            }
            this.tbxAmountAlloc.set_RegexText("^[0-9]*$");
            this.tbxAmountAlloc.MaxLength = 8;
            this.tbxMachineAlloc.set_RegexText("^[0-9]*$");
            this.tbxMachineAlloc.MaxLength = 3;
            this.lblInvTypeInfo.Text = this.SelectedItems[0].ToString().Trim();
            string str = Convert.ToString(ShareMethods.StringToInt(this.SelectedItems[3].ToString().Trim()));
            if (str.Length < 8)
            {
                str = str.PadLeft(8, '0');
            }
            this.lblStartNumInfo.Text = str;
            string str2 = Convert.ToString((int) ((ShareMethods.StringToInt(this.SelectedItems[3].ToString().Trim()) + ShareMethods.StringToInt(this.SelectedItems[4].ToString().Trim())) - 1));
            if (str2.Length < 8)
            {
                str2 = str2.PadLeft(8, '0');
            }
            this.lblEndNumInfo.Text = str2;
            this.lblStartNumAlloc.Text = str;
            this.tbxAmountAlloc.Text = Convert.ToString(ShareMethods.StringToInt(this.SelectedItems[4].ToString()));
            this.tbxMachineAlloc.Text = string.Empty;
        }

        private void btnAllocate_Click(object sender, EventArgs e)
        {
            List<InvVolumeApp> list;
            bool flag2;
            string str;
            InvVolumeApp app2;
            string str2;
            long num;
            long num2;
            InvoiceType typeCode;
            int num3;
            UnlockInvoice invoice2;
            if (this.IsRightData())
            {
                list = new List<InvVolumeApp>();
                bool flag = true;
                flag2 = false;
                str = string.Empty;
                UnlockInvoice unlockVolume = base.TaxCardInstance.NInvSearchUnlockFromMain();
                if (base.TaxCardInstance.get_RetCode() != 0)
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                    return;
                }
                if (!DownloadCommon.CheckEmpty(unlockVolume.Buffer))
                {
                    InvVolumeApp item = new InvVolumeApp();
                    flag = AllocateCommon.AllocateOneVolume(unlockVolume, out flag2, out str);
                    if (flag)
                    {
                        item.InvType = Convert.ToByte(unlockVolume.get_Kind());
                        item.TypeCode = unlockVolume.get_TypeCode();
                        item.HeadCode = Convert.ToUInt32(unlockVolume.get_Number());
                        item.Number = Convert.ToUInt16(unlockVolume.get_Count());
                        list.Add(item);
                    }
                    else if (flag2)
                    {
                        MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                    }
                    else
                    {
                        MessageManager.ShowMsgBox(str);
                    }
                }
                if (!flag)
                {
                    goto Label_0598;
                }
                app2 = new InvVolumeApp();
                str2 = this.SelectedItems[1].ToString();
                num = Convert.ToInt64(this.lblStartNumAlloc.Text);
                num2 = Convert.ToInt64(this.tbxAmountAlloc.Text);
                typeCode = (InvoiceType) ShareMethods.GetTypeCode(this.SelectedItems[0].ToString());
                num3 = Convert.ToInt32(this.tbxMachineAlloc.Text);
                if ((2 == typeCode) || (typeCode == null))
                {
                    if (base.TaxCardInstance.get_StateInfo().IsLockReached != 0)
                    {
                        MessageManager.ShowMsgBox("TCD_85_19");
                        return;
                    }
                    if (base.TaxCardInstance.get_StateInfo().IsRepReached != 0)
                    {
                        MessageManager.ShowMsgBox("TCD_85_19");
                        return;
                    }
                    goto Label_04E5;
                }
                if (11 != typeCode)
                {
                    if (12 != typeCode)
                    {
                        if (0x33 == typeCode)
                        {
                            ushort isLockTime = 2;
                            for (int j = 0; j < base.TaxCardInstance.get_StateInfo().InvTypeInfo.Count; j++)
                            {
                                if (0x33 == base.TaxCardInstance.get_StateInfo().InvTypeInfo[j].InvType)
                                {
                                    isLockTime = base.TaxCardInstance.get_StateInfo().InvTypeInfo[j].IsLockTime;
                                    ushort isRepTime = base.TaxCardInstance.get_StateInfo().InvTypeInfo[j].IsRepTime;
                                    ushort jSPRepInfo = base.TaxCardInstance.get_StateInfo().InvTypeInfo[j].JSPRepInfo;
                                    break;
                                }
                            }
                            if (1 == isLockTime)
                            {
                                MessageManager.ShowMsgBox("TCD_85_19");
                                return;
                            }
                            if (1 == isLockTime)
                            {
                                MessageManager.ShowMsgBox("TCD_85_19");
                                return;
                            }
                        }
                        else if (0x29 == typeCode)
                        {
                            ushort num10 = 2;
                            for (int k = 0; k < base.TaxCardInstance.get_StateInfo().InvTypeInfo.Count; k++)
                            {
                                if (0x33 == base.TaxCardInstance.get_StateInfo().InvTypeInfo[k].InvType)
                                {
                                    num10 = base.TaxCardInstance.get_StateInfo().InvTypeInfo[k].IsLockTime;
                                    ushort num17 = base.TaxCardInstance.get_StateInfo().InvTypeInfo[k].IsRepTime;
                                    ushort num18 = base.TaxCardInstance.get_StateInfo().InvTypeInfo[k].JSPRepInfo;
                                    break;
                                }
                            }
                            if (1 == num10)
                            {
                                MessageManager.ShowMsgBox("TCD_85_19");
                                return;
                            }
                            if (1 == num10)
                            {
                                MessageManager.ShowMsgBox("TCD_85_19");
                                return;
                            }
                        }
                    }
                    else
                    {
                        ushort num6 = 2;
                        for (int m = 0; m < base.TaxCardInstance.get_StateInfo().InvTypeInfo.Count; m++)
                        {
                            if (12 == base.TaxCardInstance.get_StateInfo().InvTypeInfo[m].InvType)
                            {
                                num6 = base.TaxCardInstance.get_StateInfo().InvTypeInfo[m].IsLockTime;
                                ushort num13 = base.TaxCardInstance.get_StateInfo().InvTypeInfo[m].IsRepTime;
                                ushort num14 = base.TaxCardInstance.get_StateInfo().InvTypeInfo[m].JSPRepInfo;
                                break;
                            }
                        }
                        if (1 == num6)
                        {
                            MessageManager.ShowMsgBox("TCD_85_19");
                            return;
                        }
                        if (1 == num6)
                        {
                            MessageManager.ShowMsgBox("TCD_85_19");
                            return;
                        }
                    }
                    goto Label_04E5;
                }
                ushort num4 = 2;
                for (int i = 0; i < base.TaxCardInstance.get_StateInfo().InvTypeInfo.Count; i++)
                {
                    if (11 == base.TaxCardInstance.get_StateInfo().InvTypeInfo[i].InvType)
                    {
                        num4 = base.TaxCardInstance.get_StateInfo().InvTypeInfo[i].IsLockTime;
                        ushort num1 = base.TaxCardInstance.get_StateInfo().InvTypeInfo[i].IsRepTime;
                        ushort num12 = base.TaxCardInstance.get_StateInfo().InvTypeInfo[i].JSPRepInfo;
                        break;
                    }
                }
                if (1 == num4)
                {
                    MessageManager.ShowMsgBox("TCD_85_19");
                    return;
                }
                if (1 != num4)
                {
                    goto Label_04E5;
                }
                MessageManager.ShowMsgBox("TCD_85_19");
            }
            return;
        Label_04E5:
            invoice2 = base.TaxCardInstance.NInvAllotToSubMachine(str2, num, num2, typeCode, num3);
            if (base.TaxCardInstance.get_RetCode() != 0)
            {
                MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                return;
            }
            if (AllocateCommon.AllocateOneVolume(invoice2, out flag2, out str))
            {
                app2.InvType = Convert.ToByte(invoice2.get_Kind());
                app2.TypeCode = invoice2.get_TypeCode();
                app2.HeadCode = Convert.ToUInt32(invoice2.get_Number());
                app2.Number = Convert.ToUInt16(invoice2.get_Count());
                list.Add(app2);
            }
            else if (flag2)
            {
                MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
            }
            else
            {
                MessageManager.ShowMsgBox(str);
            }
        Label_0598:
            if (list.Count > 0)
            {
                InvInfoWebAllocMsg msg = new InvInfoWebAllocMsg();
                msg.InsertInvVolume(list);
                msg.ShowDialog();
            }
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
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
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.lblInvTypeInfo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblInvTypeInfo");
            this.lblStartNumInfo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblStartNumInfo");
            this.lblEndNumInfo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblEndNumInfo");
            this.lblStartNumAlloc = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblStartNumAlloc");
            this.tbxAmountAlloc = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("tbxAmountAlloc");
            this.tbxMachineAlloc = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("tbxMachineAlloc");
            this.btnAllocate = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnAllocate");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.tbxAmountAlloc.KeyPress += new KeyPressEventHandler(this.tbxAmountAlloc_KeyPress);
            this.tbxMachineAlloc.KeyPress += new KeyPressEventHandler(this.tbxMachineAlloc_KeyPress);
            this.btnAllocate.Click += new EventHandler(this.btnAllocate_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AllocateInput));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x16b, 0x10d);
            this.xmlComponentLoader1.TabIndex = 7;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.WebWindows.AllocateInput\Aisino.Fwkp.Fplygl.Forms.WebWindows.AllocateInput.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x16b, 0x10d);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "AllocateInput";
            base.set_TabText("AllocateInput");
            this.Text = "向分开票机分配发票";
            base.ResumeLayout(false);
        }

        private bool IsRightData()
        {
            try
            {
                if (this.tbxAmountAlloc.Text.Equals(string.Empty))
                {
                    this.tbxAmountAlloc.Focus();
                    MessageManager.ShowMsgBox("INP-441206", new string[] { "分配张数" });
                    return false;
                }
                if (this.tbxMachineAlloc.Text.Equals(string.Empty))
                {
                    this.tbxMachineAlloc.Focus();
                    MessageManager.ShowMsgBox("INP-441206", new string[] { "分开票机号" });
                    return false;
                }
                int num = ShareMethods.StringToInt(this.lblStartNumInfo.Text.Trim());
                int num2 = ShareMethods.StringToInt(this.lblEndNumInfo.Text.Trim());
                int num3 = ShareMethods.StringToInt(this.tbxAmountAlloc.Text.Trim());
                int num4 = ShareMethods.StringToInt(this.tbxMachineAlloc.Text.Trim());
                int num5 = (num2 - num) + 1;
                if (num3 <= 0)
                {
                    this.tbxAmountAlloc.Focus();
                    MessageManager.ShowMsgBox("INP-441205", new string[] { "发票张数", "为1-65535内的整数" });
                    return false;
                }
                if (num3 > num5)
                {
                    this.tbxAmountAlloc.Focus();
                    MessageManager.ShowMsgBox("INP-441223");
                    return false;
                }
                if ((num4 <= 0) || (num4 > 0x3e7))
                {
                    this.tbxMachineAlloc.Focus();
                    MessageManager.ShowMsgBox("INP-441205", new string[] { "开票机号", "为1-999内的整数" });
                    return false;
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private void tbxAmountAlloc_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.ToString().Equals("\b"))
                {
                    e.Handled = false;
                }
                else if ((this.tbxAmountAlloc.Text.Length >= 8) && (this.tbxAmountAlloc.SelectedText.Length <= 0))
                {
                    e.Handled = true;
                }
                else if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
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

        private void tbxMachineAlloc_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.ToString().Equals("\b"))
                {
                    e.Handled = false;
                }
                else if ((this.tbxMachineAlloc.Text.Length >= 3) && (this.tbxMachineAlloc.SelectedText.Length <= 0))
                {
                    e.Handled = true;
                }
                else if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
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

