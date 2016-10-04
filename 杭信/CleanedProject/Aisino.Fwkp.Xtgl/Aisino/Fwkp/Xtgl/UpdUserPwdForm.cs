namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class UpdUserPwdForm : DockForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components;
        private AisinoLBL lblTip;
        private string mOldPwd;
        private string mUserCode;
        private AisinoTXT textBoxNewPwd;
        private AisinoTXT textBoxOldPwd;
        private AisinoTXT textBoxReNewPwd;
        private XmlComponentLoader xmlComponentLoader1;

        public UpdUserPwdForm()
        {
            this.Initialize();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (MD5_Crypt.GetHashStr(this.textBoxOldPwd.Text.Trim()) != this.mOldPwd)
            {
                this.lblTip.Text = "当前密码输入错误，请重新输入！";
                this.lblTip.ForeColor = Color.Red;
                this.textBoxOldPwd.Focus();
                this.textBoxOldPwd.SelectAll();
            }
            else if (this.textBoxNewPwd.Text != this.textBoxReNewPwd.Text)
            {
                this.lblTip.Text = "新输入密码不匹配，请重新输入！";
                this.ReInputPwd();
            }
            else if (GetLength(this.textBoxNewPwd.Text.Trim()) < 4)
            {
                this.lblTip.Text = "密码长度至少4位";
                this.ReInputPwd();
            }
            else
            {
                RoleUserDAL rdal = new RoleUserDAL();
                if (rdal.UpdateUserPwd(this.mUserCode, MD5_Crypt.GetHashStr(this.textBoxNewPwd.Text.Trim())) != -1)
                {
                    if (this.mOldPwd != MD5_Crypt.GetHashStr(this.textBoxNewPwd.Text.Trim()))
                    {
                        UserLoginUtil.UpdateRemaindPasswrodByName(rdal.SelectUserByDM(this.mUserCode).RealName);
                        PropertyUtil.Save();
                    }
                    MessageManager.ShowMsgBox("INP-132201");
                    base.DialogResult = DialogResult.OK;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private static int GetLength(string orgStr)
        {
            int length = orgStr.Length;
            char[] chArray = orgStr.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (Convert.ToInt32(chArray[i]) > 0xff)
                {
                    length++;
                }
            }
            return length;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.textBoxOldPwd = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtOldPwd");
            this.textBoxOldPwd.MaxLength = 0x10;
            this.textBoxNewPwd = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtNewPwd");
            this.textBoxNewPwd.MaxLength = 0x10;
            this.textBoxReNewPwd = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtReNewPwd");
            this.textBoxReNewPwd.MaxLength = 0x10;
            this.lblTip = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblTip");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnOK.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.btnCancel.ImageAlign = ContentAlignment.MiddleLeft;
            this.textBoxOldPwd.PasswordChar = '*';
            this.textBoxNewPwd.PasswordChar = '*';
            this.textBoxReNewPwd.PasswordChar = '*';
            this.textBoxNewPwd.KeyPress += new KeyPressEventHandler(this.txtNewPwd_KeyPress);
            this.textBoxNewPwd.TextChanged += new EventHandler(this.txtNewPwd_TextChanged);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x158, 0xad);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Xtgl.UpdUserPwdForm\Aisino.Fwkp.Xtgl.UpdUserPwdForm.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x158, 0xad);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "UpdUserPwdForm";
            this.Text = "修改用户密码";
            base.ResumeLayout(false);
        }

        private void ReInputPwd()
        {
            this.lblTip.ForeColor = Color.Red;
            this.textBoxNewPwd.Text = "";
            this.textBoxReNewPwd.Text = "";
            this.textBoxNewPwd.Select();
        }

        public void SetOldPwd(string userCode, string oldPwd)
        {
            this.mUserCode = userCode;
            this.mOldPwd = oldPwd;
        }

        private void txtNewPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                int length = GetLength(this.textBoxNewPwd.Text);
                int num2 = GetLength(this.textBoxNewPwd.SelectedText);
                if (((length - num2) + 1) > 0x10)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtNewPwd_TextChanged(object sender, EventArgs e)
        {
            byte[] bytes = ToolUtil.GetBytes(this.textBoxNewPwd.Text);
            if (bytes.Length >= 0x10)
            {
                this.textBoxNewPwd.Text = ToolUtil.GetString(bytes, 0, 0x10);
            }
        }

        public string NewPassword
        {
            get
            {
                return MD5_Crypt.GetHashStr(this.textBoxNewPwd.Text.Trim());
            }
        }
    }
}

