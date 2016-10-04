namespace Aisino.Fwkp.Fplygl.Form.FJFPGL_2
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.IBLL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class XfkpjfpfpForm : BaseForm
    {
        private AisinoBTN btnAllot;
        private AisinoBTN btnFq;
        private IContainer components;
        private readonly ILYGL_JPXX jpxxDal = BLLFactory.CreateInstant<ILYGL_JPXX>("LYGL_JPXX");
        private AisinoLBL lblFpqshm;
        private AisinoLBL lblQshm;
        private AisinoLBL lblZjhm;
        private ILog loger = LogUtil.GetLogger<XfkpjfpfpForm>();
        private List<object> SelectedItems = new List<object>();
        private TextBoxRegex tbxCount;
        private XmlComponentLoader xmlComponentLoader1;

        public XfkpjfpfpForm(List<object> oSelectedItems)
        {
            try
            {
                this.Initialize();
                foreach (object obj2 in oSelectedItems)
                {
                    this.SelectedItems.Add(obj2.ToString());
                }
                this.tbxCount.set_RegexText("^[0-9]*$");
                this.tbxCount.MaxLength = 9;
                string str = Convert.ToString(ShareMethods.StringToInt(this.SelectedItems[4].ToString().Trim()));
                if (str.Length < 8)
                {
                    str = str.PadLeft(8, '0');
                }
                this.lblQshm.Text = str;
                string str2 = Convert.ToString((int) ((ShareMethods.StringToInt(this.SelectedItems[4].ToString().Trim()) + ShareMethods.StringToInt(this.SelectedItems[5].ToString().Trim())) - 1));
                if (str2.Length < 8)
                {
                    str2 = str2.PadLeft(8, '0');
                }
                this.lblZjhm.Text = str2;
                this.lblFpqshm.Text = str;
                this.tbxCount.Text = Convert.ToString(ShareMethods.StringToInt(this.SelectedItems[5].ToString()));
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

        private void btnAllot_Click(object sender, EventArgs e)
        {
            try
            {
                string deviceType;
                if (this.IsRightData())
                {
                    if (DialogResult.OK != MessageManager.ShowMsgBox("INP-441221"))
                    {
                        goto Label_02C4;
                    }
                    deviceType = string.Empty;
                    ChooseAllocateMedium medium = new ChooseAllocateMedium();
                    if (DialogResult.OK == medium.ShowDialog())
                    {
                        deviceType = medium.GetDeviceType();
                        goto Label_0040;
                    }
                }
                return;
            Label_0040:
                if (base.TaxCardInstance.InvAllot(ShareMethods.StringToInt(this.SelectedItems[0].ToString()), this.lblFpqshm.Text.Trim(), ShareMethods.StringToInt(this.tbxCount.Text.Trim()), deviceType))
                {
                    new AllocateSuccess(this.SelectedItems[4].ToString(), this.tbxCount.Text.Trim()).ShowDialog();
                    string str2 = this.SelectedItems[2].ToString();
                    uint num = (Convert.ToUInt32(this.SelectedItems[4]) + Convert.ToUInt32(this.SelectedItems[5])) - Convert.ToUInt32(this.SelectedItems[7]);
                    uint num2 = (Convert.ToUInt32(this.SelectedItems[4]) + Convert.ToUInt32(this.SelectedItems[5])) - 1;
                    InvVolumeApp invVolume = new InvVolumeApp {
                        TypeCode = str2,
                        HeadCode = num,
                        Number = Convert.ToUInt16(this.SelectedItems[5]),
                        BuyNumber = Convert.ToInt32(this.SelectedItems[7])
                    };
                    string specificFormat = this.jpxxDal.GetSpecificFormat(invVolume);
                    Convert.ToUInt32(this.SelectedItems[4]);
                    uint num3 = (Convert.ToUInt32(this.SelectedItems[4]) + Convert.ToUInt32(this.tbxCount.Text)) - 1;
                    if (num3 < num2)
                    {
                        InvVolumeApp invVolumn = new InvVolumeApp {
                            BuyDate = Convert.ToDateTime(this.SelectedItems[6]),
                            BuyNumber = (int) (num2 - num3),
                            HeadCode = num3 + 1,
                            InvType = ShareMethods.GetTypeCode(this.SelectedItems[1].ToString()),
                            Number = (ushort) (num2 - num3),
                            TypeCode = this.SelectedItems[2].ToString()
                        };
                        this.jpxxDal.InsertSingleVolumn(invVolumn, specificFormat);
                    }
                    this.jpxxDal.DeleteSingleVolumn(invVolume);
                }
                else
                {
                    if (base.TaxCardInstance.get_RetCode() != 0)
                    {
                        base.TaxCardInstance.get_RetCode();
                        MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                    }
                    return;
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
        Label_02C4:
            base.DialogResult = DialogResult.OK;
        }

        private void btnFq_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.Cancel;
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
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.lblQshm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblQshm");
            this.lblZjhm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblZjhm");
            this.lblFpqshm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFpqshm");
            this.tbxCount = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("tbxCount");
            this.btnAllot = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnAllot");
            this.btnFq = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnFq");
            this.btnAllot.Click += new EventHandler(this.btnAllot_Click);
            this.btnFq.Click += new EventHandler(this.btnFq_Click);
            this.tbxCount.KeyPress += new KeyPressEventHandler(this.tbxCount_KeyPress);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(430, 0x116);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.AllotForm\Aisino.Fwkp.Fplygl.Forms.AllotForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(430, 0x116);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "XfkpjfpfpForm";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "向分开票机分配发票";
            base.ResumeLayout(false);
        }

        private bool IsRightData()
        {
            try
            {
                int num = ShareMethods.StringToInt(this.lblQshm.Text.Trim());
                int num2 = ShareMethods.StringToInt(this.lblZjhm.Text.Trim());
                int num3 = ShareMethods.StringToInt(this.tbxCount.Text.Trim());
                int num4 = (num2 - num) + 1;
                if (num3 <= 0)
                {
                    this.tbxCount.Focus();
                    MessageManager.ShowMsgBox("INP-441222");
                    return false;
                }
                if (num3 > num4)
                {
                    this.tbxCount.Focus();
                    MessageManager.ShowMsgBox("INP-441223");
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

        private void tbxCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar > ' ') && !char.IsDigit(e.KeyChar))
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

