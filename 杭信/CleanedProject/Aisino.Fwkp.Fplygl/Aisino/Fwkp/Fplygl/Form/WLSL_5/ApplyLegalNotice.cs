namespace Aisino.Fwkp.Fplygl.Form.WLSL_5
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ApplyLegalNotice : DockForm
    {
        private AisinoBTN btn_Agree;
        private AisinoBTN btn_Disagree;
        private AisinoCHK chk_NotRepeat;
        private IContainer components;
        protected XmlComponentLoader xmlComponentLoader1;

        public ApplyLegalNotice(bool disableDisagree = false)
        {
            this.Initialize();
            string str = PropertyUtil.GetValue("Aisino.Fwkp.Fplygl.WLSL_5.ApplyLegalNotice.Repeat");
            bool flag = false;
            if (string.IsNullOrEmpty(str))
            {
                flag = false;
            }
            else
            {
                flag = str.Equals("1");
            }
            if (flag)
            {
                this.chk_NotRepeat.Checked = false;
            }
            else
            {
                this.chk_NotRepeat.Checked = true;
            }
            this.btn_Disagree.Enabled = !disableDisagree;
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            string str = "1";
            if (this.chk_NotRepeat.Checked)
            {
                str = "0";
            }
            PropertyUtil.SetValue("Aisino.Fwkp.Fplygl.WLSL_5.ApplyLegalNotice.Repeat", str);
            base.DialogResult = DialogResult.Yes;
            base.Close();
        }

        private void btn_Disagree_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.No;
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
            this.chk_NotRepeat = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chk_NotRepeat");
            this.btn_Agree = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_Agree");
            this.btn_Disagree = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_Disagree");
            this.btn_Agree.Click += new EventHandler(this.btn_Confirm_Click);
            this.btn_Disagree.Click += new EventHandler(this.btn_Disagree_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ApplyLegalNotice));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x113, 0xe1);
            this.xmlComponentLoader1.TabIndex = 6;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.WebWindows.ApplyLegalNotice\Aisino.Fwkp.Fplygl.Forms.WebWindows.ApplyLegalNotice.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x113, 0xe1);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "ApplyLegalNotice";
            base.set_TabText("ApplyLegalNotice");
            this.Text = "法律声明";
            base.ResumeLayout(false);
        }
    }
}

