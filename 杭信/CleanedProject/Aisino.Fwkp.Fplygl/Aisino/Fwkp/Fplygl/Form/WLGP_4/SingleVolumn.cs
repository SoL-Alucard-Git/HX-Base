namespace Aisino.Fwkp.Fplygl.Form.WLGP_4
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Form.Common;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class SingleVolumn : DockForm
    {
        private AisinoBTN btn_close;
        private AisinoBTN btn_download;
        private IContainer components;
        public int electricApplyNum;
        private TextBoxRegex fpdm;
        private AisinoCMB fplx;
        private TextBoxRegex fpzs;
        public bool isElectricDownload;
        private AisinoLBL labelHeadCode;
        private AisinoLBL labelTypeCode;
        private AisinoLBL lbl_Fpdm;
        private AisinoLBL lbl_Fpzs;
        private AisinoLBL lbl_Qshm;
        private ILog loger = LogUtil.GetLogger<SingleVolumn>();
        private const int MaxAmount = 0xffff;
        private TextBoxRegex qshm;
        public List<InvVolumeApp> singleInvVolumn = new List<InvVolumeApp>();
        protected XmlComponentLoader xmlComponentLoader1;

        public SingleVolumn()
        {
            this.Initialize();
            this.fplx.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboxBind();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            int num = 0x33;
            if (this.fplx.SelectedValue.ToString() != num.ToString())
            {
                if (this.fpdm.Text.Equals(string.Empty))
                {
                    MessageManager.ShowMsgBox("INP-441231");
                    return;
                }
                if (this.qshm.Text.Equals(string.Empty))
                {
                    MessageManager.ShowMsgBox("INP-441232");
                    return;
                }
            }
            if (this.fpzs.Text.Equals(string.Empty))
            {
                MessageManager.ShowMsgBox("INP-441233");
            }
            else
            {
                int num2 = 0x33;
                if (this.fplx.SelectedValue.ToString() == num2.ToString())
                {
                    if (Convert.ToInt32(this.fpzs.Text) <= 0)
                    {
                        MessageManager.ShowMsgBox("INP-441234");
                        return;
                    }
                    if (Convert.ToInt32(this.fpzs.Text) > Convert.ToInt32(this.qshm.Text))
                    {
                        MessageManager.ShowMsgBox("INP-441235");
                        return;
                    }
                }
                else if ((0 >= Convert.ToInt32(this.fpzs.Text)) || (Convert.ToInt32(this.fpzs.Text) > 0xffff))
                {
                    MessageManager.ShowMsgBox("INP-441236");
                    return;
                }
                this.singleInvVolumn.Clear();
                int num3 = 0x33;
                if (this.fplx.SelectedValue.ToString() == num3.ToString())
                {
                    this.singleInvVolumn = DownloadCommon.GetElectricDownloadVolumes(0x33, Convert.ToInt32(this.fpzs.Text), 0xffff);
                    this.isElectricDownload = true;
                    this.electricApplyNum = Convert.ToInt32(this.fpzs.Text);
                }
                else
                {
                    InvVolumeApp item = new InvVolumeApp {
                        InvType = Convert.ToByte(this.fplx.SelectedValue.ToString()),
                        TypeCode = this.fpdm.Text,
                        HeadCode = Convert.ToUInt32(this.qshm.Text.PadLeft(8, '0')),
                        Number = Convert.ToUInt16(this.fpzs.Text)
                    };
                    this.singleInvVolumn.Add(item);
                }
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        private void ComboxBind()
        {
            DataRow row;
            char ch1 = base.TaxCardInstance.get_SQInfo().DHYBZ[0];
            DataTable table = new DataTable();
            table.Columns.Add("key");
            table.Columns.Add("value");
            if (base.TaxCardInstance.get_QYLX().ISZYFP && (base.TaxCardInstance.get_Machine() == 0))
            {
                row = table.NewRow();
                row["key"] = "增值税专用发票";
                row["value"] = "0";
                table.Rows.Add(row);
            }
            if (base.TaxCardInstance.get_QYLX().ISPTFP && (base.TaxCardInstance.get_Machine() == 0))
            {
                row = table.NewRow();
                row["key"] = "增值税普通发票";
                row["value"] = "2";
                table.Rows.Add(row);
            }
            if (base.TaxCardInstance.get_QYLX().ISHY && (base.TaxCardInstance.get_Machine() == 0))
            {
                row = table.NewRow();
                row["key"] = "货物运输业增值税专用发票";
                row["value"] = "11";
                table.Rows.Add(row);
            }
            if (base.TaxCardInstance.get_QYLX().ISJDC && (base.TaxCardInstance.get_Machine() == 0))
            {
                row = table.NewRow();
                row["key"] = "机动车销售统一发票";
                row["value"] = "12";
                table.Rows.Add(row);
            }
            if (base.TaxCardInstance.get_QYLX().ISPTFPDZ)
            {
                row = table.NewRow();
                row["key"] = "电子增值税普通发票";
                row["value"] = 0x33.ToString();
                table.Rows.Add(row);
            }
            if (base.TaxCardInstance.get_QYLX().ISPTFPJSP && (base.TaxCardInstance.get_Machine() == 0))
            {
                row = table.NewRow();
                row["key"] = "增值税普通发票(卷票)";
                row["value"] = 0x29.ToString();
                table.Rows.Add(row);
            }
            this.fplx.ValueMember = "value";
            this.fplx.DisplayMember = "key";
            this.fplx.DataSource = table;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void fpdm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((ToolUtil.GetBytes(this.fpdm.Text).Length >= 12) && (this.fpdm.SelectedText.Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void fpdm_TextChanged(object sender, EventArgs e)
        {
            this.lbl_Fpdm.Text = this.fpdm.Text.Length.ToString().PadLeft(2, '0');
        }

        private void fplx_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.fplx.SelectedValue != null)
            {
                int num2 = 0x33;
                if (this.fplx.SelectedValue.ToString() == num2.ToString())
                {
                    this.fpdm.Text = string.Empty;
                    this.labelTypeCode.Visible = false;
                    this.fpdm.Visible = false;
                    this.lbl_Fpdm.Visible = false;
                    this.labelHeadCode.Text = "可领张数：";
                    long curMaxAmount = 0L;
                    if (!this.WriteEnableCheck(out curMaxAmount))
                    {
                        this.btn_download.Enabled = false;
                        MessageManager.ShowMsgBox("INP-441237");
                        this.qshm.Text = string.Empty;
                    }
                    else
                    {
                        this.qshm.Text = curMaxAmount.ToString();
                    }
                    this.qshm.Enabled = false;
                }
                else
                {
                    this.labelTypeCode.Visible = true;
                    this.fpdm.Visible = true;
                    this.lbl_Fpdm.Visible = true;
                    this.labelHeadCode.Text = "起始号码：";
                    this.qshm.Text = string.Empty;
                    this.qshm.Enabled = true;
                }
            }
        }

        private void fpzs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((ToolUtil.GetBytes(this.fpzs.Text).Length >= 8) && (this.fpzs.SelectedText.Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void fpzs_TextChanged(object sender, EventArgs e)
        {
            this.lbl_Fpzs.Text = this.fpzs.Text.Length.ToString().PadLeft(2, '0');
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.fpdm = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("rtxtFpdm");
            this.qshm = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("rtxtQshm");
            this.fpzs = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("rtxtFpzs");
            this.fplx = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbFplx");
            this.labelTypeCode = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelTypeCode");
            this.labelHeadCode = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelHeadCode");
            this.lbl_Fpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFpdm");
            this.lbl_Qshm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblQshm");
            this.lbl_Fpzs = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFpzs");
            this.btn_download = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_xz");
            this.btn_close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
            this.fplx.SelectedValueChanged += new EventHandler(this.fplx_SelectedValueChanged);
            this.fpdm.KeyPress += new KeyPressEventHandler(this.fpdm_KeyPress);
            this.qshm.KeyPress += new KeyPressEventHandler(this.qshm_KeyPress);
            this.fpzs.KeyPress += new KeyPressEventHandler(this.fpzs_KeyPress);
            this.fpdm.TextChanged += new EventHandler(this.fpdm_TextChanged);
            this.qshm.TextChanged += new EventHandler(this.qshm_TextChanged);
            this.fpzs.TextChanged += new EventHandler(this.fpzs_TextChanged);
            this.btn_download.Click += new EventHandler(this.btn_download_Click);
            this.btn_close.Click += new EventHandler(this.btn_close_Click);
            this.fpdm.set_RegexText("^[0-9]{0,12}$");
            this.qshm.set_RegexText("^[0-9]{0,8}$");
            this.fpzs.set_RegexText("^[0-9]{0,8}$");
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(SingleVolumn));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(360, 0xd7);
            this.xmlComponentLoader1.TabIndex = 3;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.SingleVolume\Aisino.Fwkp.Fplygl.Forms.SingleVolume.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(360, 0xd7);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Name = "DownInv";
            base.set_TabText("DownInv");
            this.Text = "网上领票";
            base.ResumeLayout(false);
        }

        private void qshm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((ToolUtil.GetBytes(this.qshm.Text).Length >= 8) && (this.qshm.SelectedText.Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void qshm_TextChanged(object sender, EventArgs e)
        {
            this.lbl_Qshm.Text = this.qshm.Text.Length.ToString().PadLeft(2, '0');
        }

        private bool WriteEnableCheck(out long curMaxAmount)
        {
            bool flag = false;
            string str = base.TaxCardInstance.CanWriteInvCount(0x33, 1, ref flag, ref curMaxAmount);
            if (ToolUtil.GetReturnErrCode(str) != 0)
            {
                MessageManager.ShowMsgBox(str);
                return false;
            }
            return flag;
        }
    }
}

