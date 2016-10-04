namespace BSDC
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BSLoginForm : DockForm
    {
        private AisinoMTX aisinoMTX_0;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoGRP groupBox1;
        private IContainer icontainer_3;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private List<string> list_0;
        private AisinoTXT txtTaxNo;
        private AisinoTXT txtYear;

        public BSLoginForm()
        {
            
            this.InitializeComponent_1();
            this.txtTaxNo.Text = base.TaxCardInstance.TaxCode;
            this.txtYear.Text = base.TaxCardInstance.SysYear.ToString();
            this.list_0 = base.TaxCardInstance.MaintPassWord(this.txtTaxNo.Text, this.txtYear.Text, false);
        }

        private void aisinoMTX_0_TextChanged(object sender, EventArgs e)
        {
            string item = this.aisinoMTX_0.Text.Trim();
            if ((this.list_0 != null) && this.list_0.Contains(item))
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string item = this.aisinoMTX_0.Text.Trim();
            if ((this.list_0 != null) && this.list_0.Contains(item))
            {
                new BSDataOutput { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true }.ShowDialog();
                base.Close();
            }
            else
            {
                MessageManager.ShowMsgBox("INP-251211");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_3 != null))
            {
                this.icontainer_3.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BSLoginForm));
            this.groupBox1 = new AisinoGRP();
            this.aisinoMTX_0 = new AisinoMTX();
            this.txtYear = new AisinoTXT();
            this.txtTaxNo = new AisinoTXT();
            this.label3 = new AisinoLBL();
            this.label2 = new AisinoLBL();
            this.label1 = new AisinoLBL();
            this.btnOK = new AisinoBTN();
            this.btnCancel = new AisinoBTN();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.aisinoMTX_0);
            this.groupBox1.Controls.Add(this.txtYear);
            this.groupBox1.Controls.Add(this.txtTaxNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(330, 0x9d);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统维护口令";
            this.aisinoMTX_0.AsciiOnly = true;
            this.aisinoMTX_0.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoMTX_0.Location = new Point(0x61, 0x77);
            this.aisinoMTX_0.Mask = ">AAAAAAAAAAAAAAAA";
            this.aisinoMTX_0.Name = "msktxtPwd";
            this.aisinoMTX_0.ResetOnSpace = false;
            this.aisinoMTX_0.Size = new Size(0xbc, 0x17);
            this.aisinoMTX_0.TabIndex = 5;
            this.aisinoMTX_0.TextChanged += new EventHandler(this.aisinoMTX_0_TextChanged);
            this.txtYear.Location = new Point(0x61, 0x4c);
            this.txtYear.Name = "txtYear";
            this.txtYear.ReadOnly = true;
            this.txtYear.Size = new Size(0xbc, 0x15);
            this.txtYear.TabIndex = 4;
            this.txtTaxNo.Location = new Point(0x61, 0x22);
            this.txtTaxNo.Name = "txtTaxNo";
            this.txtTaxNo.ReadOnly = true;
            this.txtTaxNo.Size = new Size(0xbc, 0x15);
            this.txtTaxNo.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x26, 0x7a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "维护口令";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x26, 0x4f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "授权年份";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x26, 0x25);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户税号";
            this.btnOK.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnOK.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnOK.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnOK.Enabled = false;
            this.btnOK.Font = new Font("宋体", 9f);
            this.btnOK.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnOK.ForeColor = Color.White;
            this.btnOK.Location = new Point(0xaf, 180);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 30);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnCancel.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnCancel.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnCancel.Font = new Font("宋体", 9f);
            this.btnCancel.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(0x10b, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x162, 0xd8);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "BSLoginForm";
            base.TabText = "输入系统维护口令";
            this.Text = "输入系统维护口令";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}

