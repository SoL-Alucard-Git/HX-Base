namespace ns4
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Properties;
    using log4net;
    using ns0;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    internal class FormLogin : BaseForm
    {
        private AisinoCHK aisinoCHK_0;
        private AisinoLBL aisinoLBL_0;
        private bool bool_0;
        private Button btnOK;
        private Button btnSet;
        private AisinoCMB cmbName;
        private Container container_0;
        private static readonly ILog ilog_0;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL lblCopyright;
        private AisinoLBL lblStatus;
        private AisinoLBL lblVer;
        private AisinoLBL lbnTitle;
        private string string_0;
        private string string_1;
        private string string_2;
        private ToolTip toolTip_0;
        private AisinoMTX txtCertPassword;
        private AisinoMTX txtPwd;

        static FormLogin()
        {
            
            ilog_0 = LogUtil.GetLogger<FormLogin>();
        }

        public FormLogin()
        {
            
            this.string_1 = string.Empty;
            this.InitializeComponent_1();
            this.txtPwd.TextChanged += new EventHandler(this.txtPwd_TextChanged);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtPwd.Text.Trim().Length > 0x10)
            {
                MessageBoxHelper.Show("口令长度不能超过16位，请重新输入！", "系统登录", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.txtPwd.Focus();
            }
            else
            {
                bool flag = false;
                this.lblStatus.Text = "正在校验用户名/密码...";
                this.lblStatus.Refresh();
                string str = this.cmbName.SelectedItem.ToString();
                string str2 = this.txtPwd.Text.Trim();
                if (string.IsNullOrEmpty(this.txtCertPassword.Text.Trim()))
                {
                    MessageBoxHelper.Show("证书口令不能为空，请输入！", "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    string hashStr = MD5_Crypt.GetHashStr(str2);
                    Interface0 interface2 = new Class2();
                    try
                    {
                        flag = interface2.imethod_1(str, hashStr);
                    }
                    catch (Exception exception)
                    {
                        MessageBoxHelper.Show("数据库连接异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ilog_0.Error(exception.ToString());
                        this.lblStatus.Text = "";
                        return;
                    }
                    if (flag)
                    {
                        this.string_0 = str;
                        this.string_2 = str2;
                        this.string_1 = this.txtCertPassword.Text.Trim();
                        if (this.aisinoCHK_0.Checked)
                        {
                            this.bool_0 = true;
                            PropertyUtil.SetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(str)).Replace("-", ""), str2 + "-" + this.string_1);
                        }
                        else
                        {
                            PropertyUtil.SetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(str)).Replace("-", ""), string.Empty);
                        }
                        base.DialogResult = DialogResult.OK;
                        base.Dispose();
                    }
                    else
                    {
                        ilog_0.Error("用户/密码错误!");
                        MessageBoxHelper.Show("密码错误，请重新输入！", "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.txtPwd.Text = "";
                        this.txtPwd.Focus();
                        this.lblStatus.Text = "";
                        this.lblStatus.Refresh();
                    }
                }
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            FormSettings settings = new FormSettings();
            settings.ShowDialog();
            settings.ShowInTaskbar = false;
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbName.Text.Trim() != "")
            {
                string str = PropertyUtil.GetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(this.cmbName.Text.Trim())).Replace("-", ""));
                if (!string.IsNullOrEmpty(str))
                {
                    string[] strArray = str.Split(new char[] { '-' });
                    this.txtPwd.Text = strArray[0];
                    this.txtCertPassword.Text = strArray[1];
                    this.aisinoCHK_0.Checked = true;
                }
                else
                {
                    this.txtPwd.Text = string.Empty;
                    this.txtCertPassword.Text = string.Empty;
                    this.aisinoCHK_0.Checked = false;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormLogin_Load(object sender, EventArgs e)
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
                object obj2 = PropertyUtil.GetValue("MAIN_VER", null);
                if (obj2 != null)
                {
                    this.lblVer.Text = obj2.ToString();
                }
                else
                {
                    this.lblVer.Text = "未知版本号";
                }
                string str = string.Empty;
                object obj4 = PropertyUtil.GetValue("MAIN_CODE", null);
                if (obj4 != null)
                {
                    str = obj4.ToString();
                }
                object obj3 = PropertyUtil.GetValue("MAIN_MACHINE", null);
                if ((obj3 != null) && !string.IsNullOrEmpty(obj3.ToString()))
                {
                    if (obj3.ToString() == "0")
                    {
                        str = str + " 主机";
                    }
                    else
                    {
                        str = str + " 分机" + obj3.ToString();
                    }
                }
                if (!string.Empty.Equals(str))
                {
                    this.Text = "用户登录 【" + str + "】";
                }
                string str2 = PropertyUtil.GetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(this.cmbName.Text.Trim())).Replace("-", ""));
                if (!string.IsNullOrEmpty(str2))
                {
                    string[] strArray = str2.Split(new char[] { '-' });
                    this.txtPwd.Text = strArray[0];
                    this.txtCertPassword.Text = strArray[1];
                    this.aisinoCHK_0.Checked = true;
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show("数据库连接异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ilog_0.Error(exception.Message, exception);
                this.lblStatus.Text = "";
            }
        }

        private void InitializeComponent_1()
        {
            this.container_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormLogin));
            this.txtPwd = new AisinoMTX();
            this.label1 = new AisinoLBL();
            this.label2 = new AisinoLBL();
            this.btnOK = new Button();
            this.btnSet = new Button();
            this.lblStatus = new AisinoLBL();
            this.cmbName = new AisinoCMB();
            this.lblVer = new AisinoLBL();
            this.lbnTitle = new AisinoLBL();
            this.lblCopyright = new AisinoLBL();
            this.toolTip_0 = new ToolTip(this.container_0);
            this.txtCertPassword = new AisinoMTX();
            this.aisinoLBL_0 = new AisinoLBL();
            this.aisinoCHK_0 = new AisinoCHK();
            base.SuspendLayout();
            this.txtPwd.Location = new Point(0xd6, 0xbc);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new Size(0xd5, 0x15);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.KeyPress += new KeyPressEventHandler(this.txtPwd_KeyPress);
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.ForeColor = SystemColors.ControlText;
            this.label1.Location = new Point(0xa7, 0xa2);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2f, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "用户名:";
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.ForeColor = SystemColors.ControlText;
            this.label2.Location = new Point(0xa5, 0xc1);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2f, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "密  码:";
            this.btnOK.BackgroundImage = Resources.smethod_3();
            this.btnOK.Location = new Point(0x166, 0x138);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x5f, 40);
            this.btnOK.TabIndex = 4;
            this.toolTip_0.SetToolTip(this.btnOK, "登录系统");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnSet.AutoSize = true;
            this.btnSet.BackgroundImage = Resources.smethod_5();
            this.btnSet.Location = new Point(0xfb, 0x13f);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new Size(30, 30);
            this.btnSet.TabIndex = 4;
            this.toolTip_0.SetToolTip(this.btnSet, "设置金税卡状态");
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Visible = false;
            this.btnSet.Click += new EventHandler(this.btnSet_Click);
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = Color.Transparent;
            this.lblStatus.Location = new Point(0xf2, 0x156);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0, 12);
            this.lblStatus.TabIndex = 7;
            this.cmbName.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Location = new Point(0xd6, 0x9f);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new Size(0xd5, 20);
            this.cmbName.TabIndex = 0;
            this.cmbName.SelectedIndexChanged += new EventHandler(this.cmbName_SelectedIndexChanged);
            this.lblVer.AutoSize = true;
            this.lblVer.BackColor = Color.Transparent;
            this.lblVer.ForeColor = SystemColors.ControlText;
            this.lblVer.Location = new Point(0xf9, 0x45);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new Size(0x5f, 12);
            this.lblVer.TabIndex = 9;
            this.lblVer.Text = "V8.00.00.120807";
            this.lbnTitle.AutoSize = true;
            this.lbnTitle.BackColor = Color.Transparent;
            this.lbnTitle.FlatStyle = FlatStyle.Popup;
            this.lbnTitle.Font = new Font("幼圆", 18f, FontStyle.Bold);
            this.lbnTitle.ForeColor = SystemColors.ControlText;
            this.lbnTitle.Location = new Point(0x81, 0x19);
            this.lbnTitle.Name = "lbnTitle";
            this.lbnTitle.Size = new Size(360, 0x18);
            this.lbnTitle.TabIndex = 10;
            this.lbnTitle.Text = "税控发票开票软件（金税盘版）";
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.BackColor = Color.Transparent;
            this.lblCopyright.Location = new Point(0x16b, 0x16f);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new Size(0xc5, 12);
            this.lblCopyright.TabIndex = 11;
            this.lblCopyright.Text = "航天信息股份有限公司(c)2000-2014";
            this.txtCertPassword.Location = new Point(0xd6, 0xde);
            this.txtCertPassword.Name = "txtCertPassword";
            this.txtCertPassword.PasswordChar = '*';
            this.txtCertPassword.Size = new Size(0xd5, 0x15);
            this.txtCertPassword.TabIndex = 2;
            this.txtCertPassword.KeyPress += new KeyPressEventHandler(this.txtCertPassword_KeyPress);
            this.aisinoLBL_0.AutoSize = true;
            this.aisinoLBL_0.BackColor = Color.Transparent;
            this.aisinoLBL_0.ForeColor = SystemColors.ControlText;
            this.aisinoLBL_0.Location = new Point(0x98, 0xe3);
            this.aisinoLBL_0.Name = "aisinoLBL1";
            this.aisinoLBL_0.Size = new Size(0x3b, 12);
            this.aisinoLBL_0.TabIndex = 13;
            this.aisinoLBL_0.Text = "证书口令:";
            this.aisinoCHK_0.AutoSize = true;
            this.aisinoCHK_0.BackColor = Color.Transparent;
            this.aisinoCHK_0.Location = new Point(0xd6, 260);
            this.aisinoCHK_0.Name = "chkSavePWD";
            this.aisinoCHK_0.Size = new Size(0x48, 0x10);
            this.aisinoCHK_0.TabIndex = 3;
            this.aisinoCHK_0.Text = "记住密码";
            this.aisinoCHK_0.UseVisualStyleBackColor = false;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.ClientSize = new Size(0x250, 0x181);
            base.Controls.Add(this.aisinoCHK_0);
            base.Controls.Add(this.txtCertPassword);
            base.Controls.Add(this.aisinoLBL_0);
            base.Controls.Add(this.btnSet);
            base.Controls.Add(this.lblVer);
            base.Controls.Add(this.lblCopyright);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lbnTitle);
            base.Controls.Add(this.cmbName);
            base.Controls.Add(this.lblStatus);
            base.Controls.Add(this.txtPwd);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label2);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormLogin";
            this.Text = "用户登录";
            base.Load += new EventHandler(this.FormLogin_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public string method_1()
        {
            return this.string_0;
        }

        public string method_2()
        {
            return this.string_2;
        }

        public bool method_3()
        {
            return this.bool_0;
        }

        internal string method_4()
        {
            return this.string_1;
        }

        private void method_5(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(Resources.smethod_1(), base.ClientRectangle);
        }

        private void txtCertPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((this.txtCertPassword.SelectionLength < 1) && (this.txtCertPassword.Text.Length >= 0x10)) && (e.KeyChar.ToString() != "\b"))
            {
                e.Handled = true;
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((this.txtPwd.SelectionLength < 1) && (this.txtPwd.Text.Length >= 0x10)) && (e.KeyChar.ToString() != "\b"))
            {
                e.Handled = true;
            }
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(this.txtPwd.Text.Trim(), @"[\u4e00-\u9fa5]+$"))
            {
                this.txtPwd.Text = "";
                this.lblStatus.Text = "不允许输入汉字";
            }
        }
    }
}

