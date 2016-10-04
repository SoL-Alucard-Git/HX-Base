namespace ns7
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    internal class ChangeCertPass : MessageBaseForm
    {
        private AisinoLBL aisinoLBL_0;
        private AisinoMTX aisinoMTX_0;
        private AisinoMTX aisinoMTX_1;
        private AisinoMTX aisinoMTX_2;
        private bool bool_1;
        private AisinoBTN button1;
        private AisinoBTN button2;
        private IDisposable idisposable_0;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private Label label3;
        private Panel panel1;
        private string string_0;
        private string string_1;
        private string string_2;

        public ChangeCertPass()
        {
            
            this.InitializeComponent_1();
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Click += new EventHandler(this.button2_Click);
            this.aisinoMTX_0.KeyPress += new KeyPressEventHandler(this.aisinoMTX_0_KeyPress);
            this.aisinoMTX_2.KeyPress += new KeyPressEventHandler(this.aisinoMTX_2_KeyPress);
            this.aisinoMTX_1.KeyPress += new KeyPressEventHandler(this.aisinoMTX_1_KeyPress);
        }

        public ChangeCertPass(bool bool_2) : this()
        {
            
            this.bool_1 = bool_2;
        }

        private void aisinoMTX_0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((this.aisinoMTX_0.SelectionLength < 1) && (this.aisinoMTX_0.Text.Length >= 0x10)) && (e.KeyChar.ToString() != "\b"))
            {
                e.Handled = true;
            }
        }

        private void aisinoMTX_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((this.aisinoMTX_1.SelectionLength < 1) && (this.aisinoMTX_1.Text.Length >= 0x10)) && (e.KeyChar.ToString() != "\b"))
            {
                e.Handled = true;
            }
        }

        private void aisinoMTX_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((this.aisinoMTX_2.SelectionLength < 1) && (this.aisinoMTX_2.Text.Length >= 0x10)) && (e.KeyChar.ToString() != "\b"))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((this.aisinoMTX_0.Text.Trim().Length >= 8) && (this.aisinoMTX_0.Text.Trim().Length <= 0x10))
            {
                if ((this.aisinoMTX_2.Text.Trim().Length >= 8) && (this.aisinoMTX_2.Text.Trim().Length <= 0x10))
                {
                    if ((this.aisinoMTX_1.Text.Trim().Length >= 8) && (this.aisinoMTX_1.Text.Trim().Length <= 0x10))
                    {
                        if (this.aisinoMTX_2.Text.Trim() != this.aisinoMTX_1.Text.Trim())
                        {
                            MessageBoxHelper.Show("两次输入证书口令必须一致！", "修改证书口令", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else if (this.aisinoMTX_2.Text.Trim() == "88888888")
                        {
                            MessageBoxHelper.Show("不能修改为初始证书口令！", "修改证书口令", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            Regex regex = new Regex(@"[\u4e00-\u9fa5]+");
                            if (regex.Match(this.aisinoMTX_2.Text.Trim()).Length != 0)
                            {
                                MessageBoxHelper.Show("证书口令中不能有汉字！", "修改证书口令", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            else
                            {
                                TaxCard card = TaxCardFactory.CreateTaxCard();
                                if (this.bool_1)
                                {
                                    card.OpenDevice(this.aisinoMTX_0.Text.Trim(), "");
                                    if (card.RetCode != 0)
                                    {
                                        if ((card.RetCode < 0x381063cf) && (card.RetCode > 0x381063c0))
                                        {
                                            MessageBoxHelper.Show("证书原口令错误，再有" + ((card.RetCode - 0x381063c0)).ToString() + "次后将锁死，请谨慎操作", "证书登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                            return;
                                        }
                                        if (card.RetCode == 0x381063c0)
                                        {
                                            MessageBoxHelper.Show("证书口令被锁死，请解锁后重新登录", "证书登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                            return;
                                        }
                                        MessageManager.ShowMsgBox("CA_" + card.RetCode.ToString());
                                        return;
                                    }
                                }
                                int num3 = card.SignChangePassword(this.aisinoMTX_0.Text.Trim(), this.aisinoMTX_2.Text.Trim());
                                if (num3 != 0)
                                {
                                    MessageManager.ShowMsgBox("CA_" + num3.ToString());
                                }
                                else
                                {
                                    this.string_1 = this.aisinoMTX_2.Text.Trim();
                                    base.DialogResult = DialogResult.OK;
                                    base.Close();
                                    if (this.bool_1)
                                    {
                                        card.CloseDevice();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBoxHelper.Show("证书口令必须是8-16位，请重新输入！", "修改证书口令", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.aisinoMTX_1.Focus();
                    }
                }
                else
                {
                    MessageBoxHelper.Show("证书口令必须是8-16位，请重新输入！", "修改证书口令", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.aisinoMTX_2.Focus();
                }
            }
            else
            {
                MessageBoxHelper.Show("证书口令必须是8-16位，请重新输入！", "修改证书口令", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.aisinoMTX_0.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ChangeCertPass));
            this.label3 = new Label();
            this.aisinoMTX_0 = new AisinoMTX();
            this.button2 = new AisinoBTN();
            this.button1 = new AisinoBTN();
            this.aisinoMTX_1 = new AisinoMTX();
            this.aisinoLBL_0 = new AisinoLBL();
            this.label1 = new AisinoLBL();
            this.aisinoMTX_2 = new AisinoMTX();
            this.label2 = new AisinoLBL();
            this.panel1 = new Panel();
            base.pnlForm.SuspendLayout();
            base.TitleArea.SuspendLayout();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            base.pnlForm.Size = new Size(400, 280);
            base.lblTitle.Text = "证书口令修改";
            base.BodyBounds.Size = new Size(400, 250);
            base.BodyClient.Controls.Add(this.panel1);
            base.BodyClient.Controls.Add(this.label3);
            base.BodyClient.Controls.Add(this.aisinoMTX_0);
            base.BodyClient.Controls.Add(this.aisinoMTX_1);
            base.BodyClient.Controls.Add(this.aisinoLBL_0);
            base.BodyClient.Controls.Add(this.label1);
            base.BodyClient.Controls.Add(this.aisinoMTX_2);
            base.BodyClient.Controls.Add(this.label2);
            base.BodyClient.Size = new Size(0x184, 0xf4);
            this.label3.AutoSize = true;
            this.label3.BackColor = Color.Transparent;
            this.label3.ForeColor = Color.Red;
            this.label3.Location = new Point(0x58, 0x24);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xc5, 12);
            this.label3.TabIndex = 0x21;
            this.label3.Text = "证书口令不能为默认口令，请修改！";
            this.aisinoMTX_0.Location = new Point(0x6f, 0x4a);
            this.aisinoMTX_0.Name = "txtOldPWD";
            this.aisinoMTX_0.PasswordChar = '*';
            this.aisinoMTX_0.Size = new Size(0xd5, 0x15);
            this.aisinoMTX_0.TabIndex = 0;
            this.button2.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.button2.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.button2.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.button2.Font = new Font("宋体", 9f);
            this.button2.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.button2.Location = new Point(0x11c, 14);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 30);
            this.button2.TabIndex = 4;
            this.button2.Text = "取 消";
            this.button2.UseVisualStyleBackColor = true;
            this.button1.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.button1.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.button1.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.button1.Font = new Font("宋体", 9f);
            this.button1.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.button1.Location = new Point(0xc0, 14);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "确 定";
            this.button1.UseVisualStyleBackColor = true;
            this.aisinoMTX_1.Location = new Point(0x6f, 0x8d);
            this.aisinoMTX_1.Name = "txtNewPWD2";
            this.aisinoMTX_1.PasswordChar = '*';
            this.aisinoMTX_1.Size = new Size(0xd5, 0x15);
            this.aisinoMTX_1.TabIndex = 2;
            this.aisinoLBL_0.AutoSize = true;
            this.aisinoLBL_0.BackColor = Color.Transparent;
            this.aisinoLBL_0.ForeColor = SystemColors.ControlText;
            this.aisinoLBL_0.Location = new Point(0x31, 0x92);
            this.aisinoLBL_0.Name = "aisinoLBL1";
            this.aisinoLBL_0.Size = new Size(0x3b, 12);
            this.aisinoLBL_0.TabIndex = 0x1d;
            this.aisinoLBL_0.Text = "确认口令:";
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.ForeColor = SystemColors.ControlText;
            this.label1.Location = new Point(0x40, 0x4f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2f, 12);
            this.label1.TabIndex = 0x1a;
            this.label1.Text = "原口令:";
            this.aisinoMTX_2.Location = new Point(0x6f, 0x6b);
            this.aisinoMTX_2.Name = "txtNewPWD";
            this.aisinoMTX_2.PasswordChar = '*';
            this.aisinoMTX_2.Size = new Size(0xd5, 0x15);
            this.aisinoMTX_2.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.ForeColor = SystemColors.ControlText;
            this.label2.Location = new Point(0x3e, 0x70);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2f, 12);
            this.label2.TabIndex = 0x1b;
            this.label2.Text = "新口令:";
            this.panel1.BackColor = Color.FromArgb(0xe4, 0xe7, 0xe9);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0xba);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x184, 0x3a);
            this.panel1.TabIndex = 0x22;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(400, 280);
            base.ControlBox = false;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ChangeCertPass";
            this.Text = "修改证书口令";
            base.pnlForm.ResumeLayout(false);
            base.TitleArea.ResumeLayout(false);
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            base.BodyClient.PerformLayout();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public string method_1()
        {
            return this.string_0;
        }

        public void method_2(string string_3)
        {
            this.string_0 = string_3;
            this.aisinoMTX_0.Text = string_3;
        }

        public AisinoMTX method_3()
        {
            return this.aisinoMTX_0;
        }

        public void method_4(string string_3)
        {
            this.string_2 = string_3;
        }

        public string method_5()
        {
            return this.string_1;
        }
    }
}

