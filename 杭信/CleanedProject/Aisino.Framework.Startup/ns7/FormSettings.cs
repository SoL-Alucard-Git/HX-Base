namespace ns7
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class FormSettings : BaseForm
    {
        private AisinoCMB aisinoCMB_0;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IDisposable idisposable_0;
        private AisinoLBL label1;

        public FormSettings()
        {
            
            this.InitializeComponent_1();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.aisinoCMB_0.SelectedIndex == -1)
            {
                MessageBoxHelper.Show("请选择是否使用金税卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if (this.aisinoCMB_0.SelectedIndex == 0)
                {
                    card.TaxMode = CTaxCardMode.tcmHave;
                }
                else
                {
                    card.TaxMode = CTaxCardMode.tcmNone;
                }
                PropertyUtil.SetValue("Aisino.Framework.Login.FormSettings.CardMode", card.TaxMode.ToString());
                base.Close();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.idisposable_0 != null))
            {
                this.idisposable_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            this.aisinoCMB_0.Items.Clear();
            this.aisinoCMB_0.Items.Insert(0, "R=已用金税卡");
            this.aisinoCMB_0.Items.Insert(1, "N=暂无金税卡");
            if (PropertyUtil.GetValue("Aisino.Framework.Login.FormSettings.CardMode") == CTaxCardMode.tcmNone.ToString())
            {
                this.aisinoCMB_0.SelectedIndex = 1;
            }
            else
            {
                this.aisinoCMB_0.SelectedIndex = 0;
            }
        }

        private void InitializeComponent_1()
        {
            this.label1 = new AisinoLBL();
            this.aisinoCMB_0 = new AisinoCMB();
            this.btnOK = new AisinoBTN();
            this.btnCancel = new AisinoBTN();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x17, 0x1f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "防伪税控";
            this.aisinoCMB_0.FormattingEnabled = true;
            this.aisinoCMB_0.Location = new Point(90, 0x1c);
            this.aisinoCMB_0.Name = "cmbHasJSK";
            this.aisinoCMB_0.Size = new Size(0x79, 20);
            this.aisinoCMB_0.TabIndex = 1;
            this.btnOK.Location = new Point(0x7a, 0x65);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Location = new Point(0xcb, 0x65);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(290, 0x8b);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.aisinoCMB_0);
            base.Controls.Add(this.label1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormSettings";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "设置";
            base.Load += new EventHandler(this.FormSettings_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

