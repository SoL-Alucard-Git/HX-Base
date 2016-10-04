namespace Aisino.Fwkp.Fplygl.Form.FJFPGL_2
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AllocateSuccess : DockForm
    {
        private AisinoBTN btnConfirm;
        private IContainer components;
        private AisinoLBL lblNum;
        private AisinoLBL lblStart;
        protected XmlComponentLoader xmlComponentLoader1;

        public AllocateSuccess(string invStart, string invNum)
        {
            this.Initialize();
            this.lblStart.Text = invStart;
            this.lblNum.Text = invNum;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
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
            base.ShowInTaskbar = true;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.lblStart = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblStart");
            this.lblNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblNum");
            this.btnConfirm = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnConfirm");
            this.btnConfirm.Click += new EventHandler(this.btnConfirm_Click);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(AllocateSuccess));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x143, 0x103);
            this.xmlComponentLoader1.TabIndex = 2;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.AllotSuccess\Aisino.Fwkp.Fplygl.Forms.AllotSuccess.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x143, 0x103);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "AllocateSuccess";
            base.set_TabText("AllocateSuccess");
            this.Text = "向分开票机分配发票成功";
            base.ResumeLayout(false);
        }
    }
}

