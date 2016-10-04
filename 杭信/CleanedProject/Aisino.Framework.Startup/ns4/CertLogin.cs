namespace ns4
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class CertLogin : MessageBaseForm
    {
        private AisinoBTN btnNo;
        private AisinoBTN btnYes;
        private IDisposable idisposable_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblMsg;
        private Panel panel1;
        private Panel panel2;
        private string string_0;
        private AisinoMTX txtPWD;

        public CertLogin(int int_0)
        {
            
            this.InitializeComponent_1();
            this.lblMsg.Text = int_0.ToString();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPWD.Text.Trim()))
            {
                MessageBoxHelper.Show("证书口令不能为空，请输入！", "证书登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.txtPWD.Focus();
            }
            else if (this.txtPWD.Text.Trim().Length < 8)
            {
                MessageBoxHelper.Show("证书口令长度不足8位，请重新输入！", "证书登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.txtPWD.Focus();
            }
            else if (this.txtPWD.Text.Trim().Length > 0x10)
            {
                MessageBoxHelper.Show("证书口令长度不能大于16位，请重新输入！", "证书登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                card.OpenDevice(this.txtPWD.Text.Trim(), "");
                int num = card.RetCode;
                switch (num)
                {
                    case 0:
                        this.string_0 = this.txtPWD.Text.Trim();
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                        return;

                    case 0x3810002a:
                        MessageBoxHelper.Show("证书口令长度不足8个字符", "证书登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.txtPWD.Focus();
                        return;

                    case 0x381063c0:
                        MessageBoxHelper.Show("证书密码被锁死，请解锁后重新登录", "证书登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        base.DialogResult = DialogResult.Cancel;
                        base.Close();
                        return;
                }
                if ((num < 0x381063cf) && (num > 0x381063c0))
                {
                    this.lblMsg.Text = (num - 0x381063c0).ToString();
                    this.txtPWD.Text = "";
                    this.txtPWD.Focus();
                }
                else
                {
                    MessageManager.ShowMsgBox("CA_" + num.ToString());
                    base.DialogResult = DialogResult.Cancel;
                    base.Close();
                }
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

        private void InitializeComponent_1()
        {
            this.panel1 = new Panel();
            this.btnYes = new AisinoBTN();
            this.btnNo = new AisinoBTN();
            this.panel2 = new Panel();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.lblMsg = new Label();
            this.txtPWD = new AisinoMTX();
            base.pnlForm.SuspendLayout();
            base.TitleArea.SuspendLayout();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            base.pnlForm.Size = new Size(400, 230);
            base.lblTitle.Text = "证书口令登录";
            base.BodyBounds.Size = new Size(400, 200);
            base.BodyClient.Controls.Add(this.panel2);
            base.BodyClient.Controls.Add(this.panel1);
            base.BodyClient.Size = new Size(0x184, 0xc2);
            this.panel1.BackColor = Color.FromArgb(0xe4, 0xe7, 0xe9);
            this.panel1.Controls.Add(this.btnYes);
            this.panel1.Controls.Add(this.btnNo);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x184, 0x41);
            this.panel1.TabIndex = 0;
            this.btnYes.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnYes.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnYes.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnYes.Font = new Font("宋体", 9f);
            this.btnYes.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnYes.Location = new Point(0xbf, 0x13);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new Size(0x4b, 30);
            this.btnYes.TabIndex = 0x20;
            this.btnYes.Text = "确 定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new EventHandler(this.btnYes_Click);
            this.btnNo.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnNo.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnNo.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnNo.Font = new Font("宋体", 9f);
            this.btnNo.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnNo.Location = new Point(0x116, 0x13);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new Size(0x4b, 30);
            this.btnNo.TabIndex = 0x21;
            this.btnNo.Text = "取 消";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new EventHandler(this.btnNo_Click);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblMsg);
            this.panel2.Controls.Add(this.txtPWD);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x184, 0x81);
            this.panel2.TabIndex = 1;
            this.label4.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label4.Location = new Point(0x1c, 0x13);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x98, 20);
            this.label4.TabIndex = 0x26;
            this.label4.Text = "证书口令错误，您还有";
            this.label3.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.Location = new Point(0xce, 0x13);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x5f, 20);
            this.label3.TabIndex = 0x25;
            this.label3.Text = "次重试机会。";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.Location = new Point(0x1c, 0x2b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xf7, 20);
            this.label2.TabIndex = 0x24;
            this.label2.Text = "重试失败后将锁死设备，请谨慎输入！";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(0x1c, 0x52);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5d, 20);
            this.label1.TabIndex = 0x23;
            this.label1.Text = "请输入口令：";
            this.lblMsg.BackColor = Color.Transparent;
            this.lblMsg.Font = new Font("微软雅黑", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lblMsg.ForeColor = Color.Red;
            this.lblMsg.Location = new Point(0xb2, 0x13);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(0x18, 0x19);
            this.lblMsg.TabIndex = 0x22;
            this.lblMsg.Text = " 3 ";
            this.txtPWD.Location = new Point(0x7f, 0x55);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new Size(0xe2, 0x15);
            this.txtPWD.TabIndex = 0x21;
            this.txtPWD.KeyPress += new KeyPressEventHandler(this.txtPWD_KeyPress);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(400, 230);
            base.Name = "CertLogin";
            this.Text = "CertLogin";
            base.pnlForm.ResumeLayout(false);
            base.TitleArea.ResumeLayout(false);
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
        }

        public string method_1()
        {
            return this.string_0;
        }

        private void txtPWD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((this.txtPWD.SelectionLength < 1) && (this.txtPWD.Text.Length >= 0x10)) && (e.KeyChar.ToString() != "\b"))
            {
                e.Handled = true;
            }
        }
    }
}

