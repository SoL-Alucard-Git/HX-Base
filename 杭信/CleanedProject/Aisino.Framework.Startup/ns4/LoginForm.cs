namespace ns4
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Properties;
    using Aisino.FTaxBase;
    using log4net;
    using ns0;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    internal class LoginForm : MessageBaseForm
    {
        private AisinoCHK aisinoCHK_0;
        private bool bool_1;
        private Button btnChangeCertPWD;
        private AisinoBTN btnOK;
        private AisinoCMB cmbName;
        private IDisposable idisposable_0;
        private static readonly ILog ilog_0;
        private Label label1;
        private Label label4;
        private Label lblSoftName;
        private Label lblVer;
        private Panel panel1;
        private PictureBox picCertPassword;
        private PictureBox picUserName;
        private PictureBox picUserPassword;
        private Panel pnlLoginBody;
        private Panel pnlLoginBottom;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private TaxCard taxCard_0;
        private AisinoMTX txtCertPassword;
        private AisinoMTX txtPwd;

        static LoginForm()
        {
            
            ilog_0 = LogUtil.GetLogger<FormLogin>();
        }

        public LoginForm()
        {
            
            this.string_1 = string.Empty;
            this.string_3 = "请输入用户密码...";
            this.string_4 = "请输入证书口令...";
            this.string_5 = "请输入开票服务器地址...";
            this.taxCard_0 = TaxCardFactory.CreateTaxCard();
            this.InitializeComponent_1();
            this.txtPwd.TextChanged += new EventHandler(this.txtPwd_TextChanged);
            base.Load += new EventHandler(this.LoginForm_Load);
            this.txtPwd.Enter += new EventHandler(this.txtPwd_Enter);
            this.txtPwd.Leave += new EventHandler(this.txtPwd_Leave);
            if (this.taxCard_0.SoftVersion == "FWKP_V2.0_Svr_Client")
            {
                this.btnChangeCertPWD.Visible = false;
                this.txtCertPassword.Enter += new EventHandler(this.txtCertPassword_Enter);
                this.txtCertPassword.Leave += new EventHandler(this.txtCertPassword_Leave);
            }
            else
            {
                this.txtCertPassword.Enter += new EventHandler(this.txtCertPassword_Enter);
                this.txtCertPassword.Leave += new EventHandler(this.txtCertPassword_Leave);
            }
        }

        private void btnChangeCertPWD_Click(object sender, EventArgs e)
        {
            ChangeCertPass pass = new ChangeCertPass(true);
            pass.method_2("");
            if (pass.ShowDialog() == DialogResult.OK)
            {
                MessageBoxHelper.Show("证书口令修改成功，请输入证书新口令登录系统！", "证书口令修改", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txtCertPassword.Focus();
                this.txtCertPassword.SelectAll();
            }
            else
            {
                MessageBoxHelper.Show("取消证书口令修改！", "证书口令修改", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtPwd.Text.Trim().Length > 0x10)
            {
                MessageBoxHelper.Show("用户密码长度不能超过16位，请重新输入！", "系统登录", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.txtPwd.Focus();
            }
            else
            {
                bool flag = false;
                string str3 = this.cmbName.SelectedItem.ToString();
                string str = this.txtPwd.Text.Trim();
                if (str == this.string_3)
                {
                    str = "";
                }
                this.string_1 = this.txtCertPassword.Text.Trim();
                if (this.taxCard_0.SoftVersion != "FWKP_V2.0_Svr_Client")
                {
                    if (this.string_1 == this.string_4)
                    {
                        this.string_1 = "";
                    }
                    if (string.IsNullOrEmpty(this.string_1))
                    {
                        MessageBoxHelper.Show("证书口令不能为空，请输入！", "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                    if (this.string_1.Length < 8)
                    {
                        MessageBoxHelper.Show("证书口令长度不足8位，请重新输入！", "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                    if (this.string_1.Length > 0x10)
                    {
                        MessageBoxHelper.Show("证书口令长度不能大于16位，请重新输入！", "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                }
                string hashStr = MD5_Crypt.GetHashStr(str);
                Interface0 interface2 = new Class2();
                try
                {
                    flag = interface2.imethod_1(str3, hashStr);
                }
                catch (Exception exception)
                {
                    MessageBoxHelper.Show("数据库连接异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ilog_0.Error(exception.ToString());
                    return;
                }
                if (flag)
                {
                    this.string_0 = str3;
                    this.string_2 = str;
                    this.taxCard_0.String_0 = this.string_1;
                    if (this.aisinoCHK_0.Checked)
                    {
                        this.bool_1 = true;
                        PropertyUtil.SetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(str3)).Replace("-", ""), str + "-" + this.string_1);
                    }
                    else
                    {
                        PropertyUtil.SetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(str3)).Replace("-", ""), string.Empty);
                    }
                    if (string.Equals(this.taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client"))
                    {
                        PropertyUtil.SetValue("KPS_PROXY_URL", this.string_1);
                    }
                    base.DialogResult = DialogResult.OK;
                    base.Dispose();
                }
                else
                {
                    ilog_0.Error("用户/密码错误!");
                    MessageBoxHelper.Show("密码错误，请重新输入！", "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.method_6("");
                    this.txtPwd.Focus();
                }
            }
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbName.Text.Trim() != "")
            {
                string str = PropertyUtil.GetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(this.cmbName.Text.Trim())).Replace("-", ""));
                if (!string.IsNullOrEmpty(str))
                {
                    string[] strArray = str.Split(new char[] { '-' });
                    this.method_6(strArray[0]);
                    if (string.Equals(this.taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client"))
                    {
                        this.method_7(PropertyUtil.GetValue("KPS_PROXY_URL", ""));
                    }
                    else
                    {
                        this.method_7(strArray[1]);
                    }
                    this.aisinoCHK_0.Checked = true;
                }
                else
                {
                    this.method_6("");
                    this.method_7("");
                    this.aisinoCHK_0.Checked = false;
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
            this.pnlLoginBody = new Panel();
            this.btnChangeCertPWD = new Button();
            this.panel1 = new Panel();
            this.lblVer = new Label();
            this.lblSoftName = new Label();
            this.btnOK = new AisinoBTN();
            this.aisinoCHK_0 = new AisinoCHK();
            this.txtCertPassword = new AisinoMTX();
            this.cmbName = new AisinoCMB();
            this.txtPwd = new AisinoMTX();
            this.picCertPassword = new PictureBox();
            this.picUserPassword = new PictureBox();
            this.picUserName = new PictureBox();
            this.pnlLoginBottom = new Panel();
            this.label4 = new Label();
            this.label1 = new Label();
            base.pnlForm.SuspendLayout();
            base.TitleArea.SuspendLayout();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            this.pnlLoginBody.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.picCertPassword).BeginInit();
            ((ISupportInitialize) this.picUserPassword).BeginInit();
            ((ISupportInitialize) this.picUserName).BeginInit();
            this.pnlLoginBottom.SuspendLayout();
            base.SuspendLayout();
            base.pnlForm.Size = new Size(600, 450);
            base.TitleArea.Size = new Size(600, 40);
            base.pnlCaption.Size = new Size(600, 40);
            base.lblTitle.Size = new Size(570, 40);
            base.lblTitle.Text = "税控发票开票软件（金税盘版）";
            base.BodyBounds.Location = new Point(0, 40);
            base.BodyBounds.Size = new Size(600, 410);
            base.BodyClient.Controls.Add(this.pnlLoginBottom);
            base.BodyClient.Controls.Add(this.pnlLoginBody);
            base.BodyClient.Size = new Size(0x24c, 0x194);
            this.pnlLoginBody.Controls.Add(this.btnChangeCertPWD);
            this.pnlLoginBody.Controls.Add(this.panel1);
            this.pnlLoginBody.Controls.Add(this.btnOK);
            this.pnlLoginBody.Controls.Add(this.aisinoCHK_0);
            this.pnlLoginBody.Controls.Add(this.txtCertPassword);
            this.pnlLoginBody.Controls.Add(this.cmbName);
            this.pnlLoginBody.Controls.Add(this.txtPwd);
            this.pnlLoginBody.Controls.Add(this.picCertPassword);
            this.pnlLoginBody.Controls.Add(this.picUserPassword);
            this.pnlLoginBody.Controls.Add(this.picUserName);
            this.pnlLoginBody.Dock = DockStyle.Top;
            this.pnlLoginBody.Location = new Point(0, 0);
            this.pnlLoginBody.Name = "pnlLoginBody";
            this.pnlLoginBody.Size = new Size(0x24c, 0x152);
            this.pnlLoginBody.TabIndex = 0;
            this.btnChangeCertPWD.BackColor = Color.Transparent;
            this.btnChangeCertPWD.FlatAppearance.BorderSize = 0;
            this.btnChangeCertPWD.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0xac, 0xfb);
            this.btnChangeCertPWD.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0x91, 0xe0);
            this.btnChangeCertPWD.FlatStyle = FlatStyle.Flat;
            this.btnChangeCertPWD.Location = new Point(0x198, 0xe9);
            this.btnChangeCertPWD.Name = "btnChangeCertPWD";
            this.btnChangeCertPWD.Size = new Size(0x58, 0x17);
            this.btnChangeCertPWD.TabIndex = 6;
            this.btnChangeCertPWD.Text = "更改证书口令";
            this.btnChangeCertPWD.UseVisualStyleBackColor = false;
            this.btnChangeCertPWD.Click += new EventHandler(this.btnChangeCertPWD_Click);
            this.panel1.Controls.Add(this.lblVer);
            this.panel1.Controls.Add(this.lblSoftName);
            this.panel1.Location = new Point(110, 0x36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x178, 0x42);
            this.panel1.TabIndex = 5;
            this.lblVer.AutoSize = true;
            this.lblVer.BackColor = Color.Transparent;
            this.lblVer.Font = new Font("宋体", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblVer.ForeColor = Color.DarkRed;
            this.lblVer.Location = new Point(110, 0x2f);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new Size(140, 13);
            this.lblVer.TabIndex = 6;
            this.lblVer.Text = "V2.0.00.20141122.01";
            this.lblSoftName.AutoSize = true;
            this.lblSoftName.Font = new Font("微软雅黑", 15.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lblSoftName.Location = new Point(0x10, 4);
            this.lblSoftName.Name = "lblSoftName";
            this.lblSoftName.Size = new Size(0x138, 0x1c);
            this.lblSoftName.TabIndex = 0;
            this.lblSoftName.Text = "税控发票开票软件（金税盘版） ";
            this.btnOK.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnOK.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnOK.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnOK.Font = new Font("楷体", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnOK.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnOK.ForeColor = Color.White;
            this.btnOK.Location = new Point(0x11f, 0x114);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x74, 0x22);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "登录";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.aisinoCHK_0.AutoSize = true;
            this.aisinoCHK_0.BackColor = Color.Transparent;
            this.aisinoCHK_0.Location = new Point(0xad, 0x114);
            this.aisinoCHK_0.Name = "chkSavePWD";
            this.aisinoCHK_0.Size = new Size(0x48, 0x10);
            this.aisinoCHK_0.TabIndex = 4;
            this.aisinoCHK_0.Text = "记住密码";
            this.aisinoCHK_0.UseVisualStyleBackColor = false;
            this.txtCertPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtCertPassword.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.txtCertPassword.ForeColor = Color.LightGray;
            this.txtCertPassword.Location = new Point(0xcb, 0xe5);
            this.txtCertPassword.Name = "txtCertPassword";
            this.txtCertPassword.Size = new Size(200, 0x1d);
            this.txtCertPassword.TabIndex = 3;
            this.txtCertPassword.Text = "请输入证书口令...";
            this.txtCertPassword.KeyPress += new KeyPressEventHandler(this.txtCertPassword_KeyPress);
            this.cmbName.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbName.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Location = new Point(0xcb, 0x90);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new Size(200, 0x1d);
            this.cmbName.TabIndex = 1;
            this.cmbName.SelectedIndexChanged += new EventHandler(this.cmbName_SelectedIndexChanged);
            this.txtPwd.BorderStyle = BorderStyle.FixedSingle;
            this.txtPwd.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.txtPwd.ForeColor = Color.LightGray;
            this.txtPwd.Location = new Point(0xcb, 0xbb);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new Size(200, 0x1d);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.Text = "请输入用户密码...";
            this.txtPwd.KeyPress += new KeyPressEventHandler(this.txtPwd_KeyPress);
            this.picCertPassword.Image = Resources.smethod_0();
            this.picCertPassword.Location = new Point(0xad, 0xe5);
            this.picCertPassword.Name = "picCertPassword";
            this.picCertPassword.Size = new Size(30, 0x1d);
            this.picCertPassword.TabIndex = 2;
            this.picCertPassword.TabStop = false;
            this.picUserPassword.Image = Resources.smethod_9();
            this.picUserPassword.Location = new Point(0xad, 0xbb);
            this.picUserPassword.Name = "picUserPassword";
            this.picUserPassword.Size = new Size(30, 0x1d);
            this.picUserPassword.TabIndex = 1;
            this.picUserPassword.TabStop = false;
            this.picUserName.Image = Resources.smethod_8();
            this.picUserName.Location = new Point(0xad, 0x90);
            this.picUserName.Name = "picUserName";
            this.picUserName.Size = new Size(30, 0x1d);
            this.picUserName.TabIndex = 0;
            this.picUserName.TabStop = false;
            this.pnlLoginBottom.BackColor = Color.FromArgb(0xe4, 0xe7, 0xe9);
            this.pnlLoginBottom.Controls.Add(this.label4);
            this.pnlLoginBottom.Controls.Add(this.label1);
            this.pnlLoginBottom.Dock = DockStyle.Fill;
            this.pnlLoginBottom.Location = new Point(0, 0x152);
            this.pnlLoginBottom.Name = "pnlLoginBottom";
            this.pnlLoginBottom.Size = new Size(0x24c, 0x42);
            this.pnlLoginBottom.TabIndex = 1;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label4.ForeColor = SystemColors.ControlDarkDark;
            this.label4.Location = new Point(0x173, 13);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x68, 0x11);
            this.label4.TabIndex = 2;
            this.label4.Text = "国家税务总局监制";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = SystemColors.ControlDarkDark;
            this.label1.Location = new Point(0x173, 0x25);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xcb, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "航天信息股份有限公司(c)2014-2016";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(600, 450);
            base.Name = "LoginForm";
            base.ShowInTaskbar = true;
            this.Text = "LoginForm";
            base.pnlForm.ResumeLayout(false);
            base.TitleArea.ResumeLayout(false);
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            this.pnlLoginBody.ResumeLayout(false);
            this.pnlLoginBody.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize) this.picCertPassword).EndInit();
            ((ISupportInitialize) this.picUserPassword).EndInit();
            ((ISupportInitialize) this.picUserName).EndInit();
            this.pnlLoginBottom.ResumeLayout(false);
            this.pnlLoginBottom.PerformLayout();
            base.ResumeLayout(false);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Interface0 interface2 = new Class2();
            try
            {
                List<string> list = interface2.imethod_0();
                this.cmbName.Items.AddRange(list.ToArray());
                if (this.cmbName.Items.Count > 0)
                {
                    this.cmbName.SelectedIndex = 0;
                }
                object obj3 = PropertyUtil.GetValue("MAIN_VER", null);
                if (obj3 != null)
                {
                    this.lblVer.Text = obj3.ToString();
                }
                else
                {
                    this.lblVer.Text = "未知版本号";
                }
                string str2 = string.Empty;
                object obj2 = PropertyUtil.GetValue("MAIN_CODE", null);
                if (obj2 != null)
                {
                    str2 = obj2.ToString();
                }
                if (!string.Empty.Equals(str2))
                {
                    base.lblTitle.Text = ProductName + str2;
                }
                string str = PropertyUtil.GetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(this.cmbName.Text.Trim())).Replace("-", ""));
                if (!string.IsNullOrEmpty(str))
                {
                    string[] strArray = str.Split(new char[] { '-' });
                    this.method_6(strArray[0]);
                    if (string.Equals(this.taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client"))
                    {
                        this.method_7(PropertyUtil.GetValue("KPS_PROXY_URL", ""));
                    }
                    else
                    {
                        this.method_7(strArray[1]);
                    }
                    this.aisinoCHK_0.Checked = true;
                }
                if (string.Equals(this.taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client"))
                {
                    this.method_7(PropertyUtil.GetValue("KPS_PROXY_URL", ""));
                }
                this.lblSoftName.Text = ProductName;
                if (this.lblSoftName.Text.Contains("代开"))
                {
                    this.lblSoftName.Location = new Point(60, 4);
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show("数据库连接异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ilog_0.Error(exception.Message, exception);
            }
        }

        public string method_1()
        {
            return this.string_0;
        }

        private void method_10(object sender, EventArgs e)
        {
            ChangeCertPass pass = new ChangeCertPass();
            pass.method_2("");
            pass.ShowDialog();
        }

        public string method_2()
        {
            return this.string_2;
        }

        public bool method_3()
        {
            return this.bool_1;
        }

        internal string method_4()
        {
            return this.string_1;
        }

        internal void method_5(string string_6)
        {
            this.string_1 = string_6;
        }

        private void method_6(string string_6)
        {
            if (!string.IsNullOrEmpty(string_6) && !(string_6 == this.string_3))
            {
                this.txtPwd.PasswordChar = '*';
                this.txtPwd.Text = string_6;
                this.txtPwd.ForeColor = Color.Black;
            }
            else
            {
                this.txtPwd.PasswordChar = '\0';
                this.txtPwd.Text = this.string_3;
                this.txtPwd.ForeColor = Color.LightGray;
            }
        }

        private void method_7(string string_6)
        {
            if ((!string.IsNullOrEmpty(string_6) && (string_6 != this.string_4)) && (string_6 != this.string_5))
            {
                if (this.taxCard_0.SoftVersion == "FWKP_V2.0_Svr_Client")
                {
                    this.txtCertPassword.Text = string_6;
                }
                else
                {
                    this.txtCertPassword.PasswordChar = '*';
                    this.txtCertPassword.Text = string_6;
                }
                this.txtCertPassword.ForeColor = Color.Black;
            }
            else
            {
                if (this.taxCard_0.SoftVersion != "FWKP_V2.0_Svr_Client")
                {
                    this.txtCertPassword.PasswordChar = '\0';
                    this.txtCertPassword.Text = this.string_4;
                }
                else
                {
                    this.txtCertPassword.Text = this.string_5;
                }
                this.txtCertPassword.ForeColor = Color.LightGray;
            }
        }

        private void method_8(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Dispose();
        }

        private void method_9(object sender, EventArgs e)
        {
            FormSettings settings = new FormSettings();
            settings.ShowDialog();
            settings.ShowInTaskbar = false;
        }

        private void txtCertPassword_Enter(object sender, EventArgs e)
        {
            if (this.txtCertPassword.Text.Trim() == this.string_4)
            {
                this.txtCertPassword.PasswordChar = '*';
                this.txtCertPassword.Text = "";
                this.txtCertPassword.ForeColor = Color.Black;
            }
            else if (this.txtCertPassword.Text.Trim() == this.string_5)
            {
                this.txtCertPassword.ForeColor = Color.Black;
                this.txtCertPassword.Text = "";
            }
            else
            {
                this.txtCertPassword.ForeColor = Color.Black;
            }
        }

        private void txtCertPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.txtCertPassword.SelectionLength < 1)
            {
                int length = this.txtCertPassword.Text.Length;
                if (this.taxCard_0.SoftVersion == "FWKP_V2.0_Svr_Client")
                {
                    if ((length > 0x1c) && (e.KeyChar.ToString() != "\b"))
                    {
                        e.Handled = true;
                    }
                }
                else if ((length >= 0x10) && (e.KeyChar.ToString() != "\b"))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCertPassword_Leave(object sender, EventArgs e)
        {
            this.method_7(this.txtCertPassword.Text.Trim());
        }

        private void txtPwd_Enter(object sender, EventArgs e)
        {
            if (this.txtPwd.Text.Trim() == this.string_3)
            {
                this.txtPwd.PasswordChar = '*';
                this.txtPwd.Text = "";
                this.txtPwd.ForeColor = Color.Black;
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((this.txtPwd.SelectionLength < 1) && (this.txtPwd.Text.Length >= 0x10)) && (e.KeyChar.ToString() != "\b"))
            {
                e.Handled = true;
            }
        }

        private void txtPwd_Leave(object sender, EventArgs e)
        {
            this.method_6(this.txtPwd.Text.Trim());
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(this.txtPwd.Text.Trim(), @"[\u4e00-\u9fa5]+$"))
            {
                this.method_6("");
                MessageBoxHelper.Show("密码不能输入汉字，请重新输入！", "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.txtPwd.Focus();
            }
        }
    }
}

