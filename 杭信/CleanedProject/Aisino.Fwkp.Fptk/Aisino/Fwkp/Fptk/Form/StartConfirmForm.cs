namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class StartConfirmForm : BaseForm
    {
        private AisinoBTN but_close;
        private AisinoBTN but_ok;
        private IContainer components;
        private AisinoLBL lab_dm;
        private AisinoLBL lab_hm;
        private AisinoLBL lab_zl;
        private AisinoLBL label1;
        private XmlComponentLoader xmlComponentLoader1;

        public StartConfirmForm(FPLX fplx, string[] icn)
        {
            this.Initialize();
            this.lab_zl.Text = this.GetTypeStr(fplx);
            this.lab_dm.Text = icn[0];
            this.lab_hm.Text = icn[1];
            if ((int)fplx == 0x33)
            {
                this.label1.Text = "\r\n    现在显示的为将要开具的发票的种类、代码、号码。请确认是否填开本张发票？";
            }
        }

        private void but_close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void but_ok_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
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

        private string GetTypeStr(FPLX type)
        {
            if (type == null)
            {
                return "专用发票";
            }
            if ((int)type == 2)
            {
                return "普通发票";
            }
            if ((int)type == 11)
            {
                return "货物运输业增值税专用发票";
            }
            if ((int)type == 12)
            {
                return "机动车销售统一发票";
            }
            if ((int)type == 0x33)
            {
                return "电子增值税普通发票";
            }
            if ((int)type == 0x29)
            {
                return "增值税普通发票(卷票)";
            }
            return null;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.lab_zl = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_zl");
            this.lab_dm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_dm");
            this.lab_hm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_hm");
            this.but_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.but_close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
            this.but_ok.Click += new EventHandler(this.but_ok_Click);
            this.but_close.Click += new EventHandler(this.but_close_Click);
            base.FormClosed += new FormClosedEventHandler(this.StartConfirmForm_FormClosed);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(StartConfirmForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1a2, 0x103);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPhaomaqueren\Aisino.Fwkp.Fpkj.Form.FPhaomaqueren.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x1a2, 0x103);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "StartConfirmForm";
            base.ShowIcon = false;
            this.Text = "发票号码确认";
            base.ResumeLayout(false);
        }

        private void StartConfirmForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}

