namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BSLoginForm : DockForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components;
        private AisinoGRP groupBox1;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoMTX msktxtPwd;
        private List<string> pwdList;
        private AisinoTXT txtTaxNo;
        private AisinoTXT txtYear;
        private XmlComponentLoader xmlComponentLoader1;

        public BSLoginForm()
        {
            this.Initialize();
            this.txtTaxNo.Text = base.TaxCardInstance.get_TaxCode();
            this.txtYear.Text = base.TaxCardInstance.get_SysYear().ToString();
            this.pwdList = base.TaxCardInstance.MaintPassWord(this.txtTaxNo.Text, this.txtYear.Text, false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            new BSDataOutput { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true }.ShowDialog();
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
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.txtTaxNo = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtTaxNo");
            this.txtYear = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtYear");
            this.msktxtPwd = this.xmlComponentLoader1.GetControlByName<AisinoMTX>("msktxtPwd");
            this.btnOK.Enabled = false;
            this.msktxtPwd.Select();
            this.msktxtPwd.Mask = ">AAAAAAAAAAAAAAAA";
            this.msktxtPwd.ResetOnSpace = false;
            this.msktxtPwd.TextChanged += new EventHandler(this.msktxtPwd_TextChanged);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BSLoginForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x162, 0xdf);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.BSLoginForm\Aisino.Fwkp.Bsgl.BSLoginForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x162, 0xdf);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.Name = "BSLoginForm";
            this.Text = "输入系统维护口令";
            base.ResumeLayout(false);
        }

        private void msktxtPwd_TextChanged(object sender, EventArgs e)
        {
            string item = this.msktxtPwd.Text.Trim();
            if ((this.pwdList != null) && this.pwdList.Contains(item))
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }
    }
}

