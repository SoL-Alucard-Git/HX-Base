namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Xtgl.Common;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class UpdUserInfoForm : Form
    {
        private AisinoLBL aisinoLBL1;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components;
        private AisinoLBL label1;
        private Label label14;
        private Label label15;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label9;
        private AisinoLBL lblTip;
        private string mOldPwd;
        private string mUserCode;
        private string mUserRealName;
        private Panel panel1;
        private AisinoTXT textBoxNewName;
        private AisinoTXT textBoxNewPwd;
        private AisinoTXT textBoxOldName;
        private AisinoTXT textBoxOldPwd;
        private AisinoTXT textBoxReNewPwd;
        private RoleUserDAL userDAL = new RoleUserDAL();

        public UpdUserInfoForm(string userCode, string userRealName, string oldPwd)
        {
            this.mUserCode = userCode;
            this.mUserRealName = userRealName;
            this.mOldPwd = oldPwd;
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.textBoxNewName.Text.Trim() == "")
            {
                this.lblTip.Text = "用户名不能为空！";
                this.ReInputName();
            }
            else if ((this.mUserRealName != this.textBoxNewName.Text.Trim()) && this.userDAL.ExistUserName(this.textBoxNewName.Text.Trim()))
            {
                this.lblTip.Text = "此用户名已存在，请重新输入！";
                this.ReInputName();
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
            else if (this.userDAL.UpdateUserNamePwd(this.mUserCode, this.textBoxNewName.Text.Trim(), MD5_Crypt.GetHashStr(this.textBoxNewPwd.Text.Trim())) != -1)
            {
                if ((this.textBoxOldName.Text.Trim() != this.textBoxNewName.Text.Trim()) || (this.mOldPwd != MD5_Crypt.GetHashStr(this.textBoxNewPwd.Text.Trim())))
                {
                    MD5_Crypt.GetHashStr(this.textBoxNewPwd.Text.Trim());
                    UserLoginUtil.ChangeRemaindNameAndPassword(this.textBoxOldName.Text.Trim(), this.textBoxNewName.Text.Trim(), this.textBoxNewPwd.Text.Trim());
                    PropertyUtil.Save();
                }
                MessageManager.ShowMsgBox("INP-131203");
                base.DialogResult = DialogResult.OK;
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

        private void InitializeComponent()
        {
            this.btnCancel = new AisinoBTN();
            this.btnOK = new AisinoBTN();
            this.panel1 = new Panel();
            this.label14 = new Label();
            this.label9 = new AisinoLBL();
            this.label15 = new Label();
            this.label1 = new AisinoLBL();
            this.aisinoLBL1 = new AisinoLBL();
            this.textBoxOldName = new AisinoTXT();
            this.textBoxOldPwd = new AisinoTXT();
            this.textBoxNewName = new AisinoTXT();
            this.label2 = new AisinoLBL();
            this.label3 = new AisinoLBL();
            this.textBoxReNewPwd = new AisinoTXT();
            this.lblTip = new AisinoLBL();
            this.textBoxNewPwd = new AisinoTXT();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnCancel.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnCancel.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnCancel.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new Point(0x10c, 0x11d);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x52, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnOK.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnOK.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnOK.FlatStyle = FlatStyle.Flat;
            this.btnOK.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnOK.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnOK.ForeColor = Color.White;
            this.btnOK.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnOK.Location = new Point(0xab, 0x11d);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x52, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "修改";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel1.BackColor = Color.White;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.aisinoLBL1);
            this.panel1.Controls.Add(this.textBoxOldName);
            this.panel1.Controls.Add(this.textBoxOldPwd);
            this.panel1.Controls.Add(this.textBoxNewName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxReNewPwd);
            this.panel1.Controls.Add(this.lblTip);
            this.panel1.Controls.Add(this.textBoxNewPwd);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x191, 0x112);
            this.panel1.TabIndex = 2;
            this.label14.BackColor = SystemColors.Control;
            this.label14.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label14.ForeColor = Color.Blue;
            this.label14.Location = new Point(0x2d, 0x15);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x131, 0x17);
            this.label14.TabIndex = 50;
            this.label14.Text = "当前用户验证：";
            this.label9.AutoSize = true;
            this.label9.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label9.Location = new Point(0x29, 0x36);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x4a, 0x13);
            this.label9.TabIndex = 0x31;
            this.label9.Text = "当前用户名";
            this.label15.BackColor = SystemColors.Control;
            this.label15.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label15.ForeColor = Color.Blue;
            this.label15.Location = new Point(0x2d, 0x77);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x131, 0x17);
            this.label15.TabIndex = 0x3a;
            this.label15.Text = "修改用户名密码：";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(0x36, 0x56);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3d, 0x13);
            this.label1.TabIndex = 0x30;
            this.label1.Text = "当前密码";
            this.aisinoLBL1.AutoSize = true;
            this.aisinoLBL1.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoLBL1.Location = new Point(0x36, 0x97);
            this.aisinoLBL1.Name = "aisinoLBL1";
            this.aisinoLBL1.Size = new Size(0x3d, 0x13);
            this.aisinoLBL1.TabIndex = 0x39;
            this.aisinoLBL1.Text = "新用户名";
            this.textBoxOldName.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBoxOldName.Location = new Point(0x7f, 0x33);
            this.textBoxOldName.Name = "textBoxOldName";
            this.textBoxOldName.ReadOnly = true;
            this.textBoxOldName.Size = new Size(0xdf, 0x19);
            this.textBoxOldName.TabIndex = 0x2e;
            this.textBoxOldPwd.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBoxOldPwd.Location = new Point(0x7f, 0x53);
            this.textBoxOldPwd.Name = "textBoxOldPwd";
            this.textBoxOldPwd.PasswordChar = '*';
            this.textBoxOldPwd.Size = new Size(0xdf, 0x19);
            this.textBoxOldPwd.TabIndex = 0x2f;
            this.textBoxOldPwd.UseSystemPasswordChar = true;
            this.textBoxOldPwd.TextChanged += new EventHandler(this.textBoxOldPwd_TextChanged);
            this.textBoxOldPwd.TextChanged += new EventHandler(this.Pwd_TextChanged);
            this.textBoxOldPwd.KeyPress += new KeyPressEventHandler(this.Pwd_KeyPress);
            this.textBoxNewName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.textBoxNewName.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBoxNewName.Location = new Point(0x7f, 0x94);
            this.textBoxNewName.Name = "textBoxNewName";
            this.textBoxNewName.Size = new Size(0xdf, 0x19);
            this.textBoxNewName.TabIndex = 0x33;
            this.textBoxNewName.TextChanged += new EventHandler(this.textBoxNewName_TextChanged);
            this.textBoxNewName.KeyPress += new KeyPressEventHandler(this.textBoxNewName_KeyPress);
            this.label2.AutoSize = true;
            this.label2.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.Location = new Point(0x43, 180);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x30, 0x13);
            this.label2.TabIndex = 0x36;
            this.label2.Text = "新密码";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.Location = new Point(0x29, 0xd1);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4a, 0x13);
            this.label3.TabIndex = 0x37;
            this.label3.Text = "确认新密码";
            this.textBoxReNewPwd.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBoxReNewPwd.Location = new Point(0x7f, 0xce);
            this.textBoxReNewPwd.Name = "textBoxReNewPwd";
            this.textBoxReNewPwd.PasswordChar = '*';
            this.textBoxReNewPwd.Size = new Size(0xdf, 0x19);
            this.textBoxReNewPwd.TabIndex = 0x35;
            this.textBoxReNewPwd.UseSystemPasswordChar = true;
            this.textBoxReNewPwd.TextChanged += new EventHandler(this.Pwd_TextChanged);
            this.textBoxReNewPwd.KeyPress += new KeyPressEventHandler(this.Pwd_KeyPress);
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblTip.ForeColor = Color.Blue;
            this.lblTip.Location = new Point(0x29, 0xf2);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new Size(0xa4, 0x13);
            this.lblTip.TabIndex = 0x38;
            this.lblTip.Text = "(提示：密码长度为4-16位)";
            this.textBoxNewPwd.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBoxNewPwd.Location = new Point(0x7f, 0xb1);
            this.textBoxNewPwd.Name = "textBoxNewPwd";
            this.textBoxNewPwd.PasswordChar = '*';
            this.textBoxNewPwd.Size = new Size(0xdf, 0x19);
            this.textBoxNewPwd.TabIndex = 0x34;
            this.textBoxNewPwd.UseSystemPasswordChar = true;
            this.textBoxNewPwd.TextChanged += new EventHandler(this.Pwd_TextChanged);
            this.textBoxNewPwd.KeyPress += new KeyPressEventHandler(this.Pwd_KeyPress);
            base.AutoScaleMode = AutoScaleMode.None;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.ClientSize = new Size(0x191, 0x146);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "UpdUserInfoForm";
            this.Text = "用户名密码修改";
            base.Load += new EventHandler(this.UpdUserPwdForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void Pwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                int length = GetLength((sender as TextBox).Text);
                int num2 = GetLength((sender as TextBox).SelectedText);
                if (((length - num2) + 1) > 0x10)
                {
                    e.Handled = true;
                }
            }
        }

        private void Pwd_TextChanged(object sender, EventArgs e)
        {
            byte[] bytes = ToolUtil.GetBytes((sender as TextBox).Text);
            if (bytes.Length >= 0x10)
            {
                (sender as TextBox).Text = ToolUtil.GetString(bytes, 0, 0x10);
            }
        }

        private void ReInputName()
        {
            this.lblTip.ForeColor = Color.Red;
            this.textBoxNewName.Text = "";
            this.textBoxNewName.Select();
        }

        private void ReInputPwd()
        {
            this.lblTip.ForeColor = Color.Red;
            this.textBoxNewPwd.Text = "";
            this.textBoxReNewPwd.Text = "";
            this.textBoxNewPwd.Select();
        }

        private void textBoxNewName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char.IsControl(e.KeyChar);
        }

        private void textBoxNewName_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxNewName.Text.Trim() != "")
            {
                int selectionStart = this.textBoxNewName.SelectionStart;
                this.textBoxNewName.Text = StringUtils.GetSubString(this.textBoxNewName.Text, 8);
                this.textBoxNewName.SelectionStart = selectionStart;
            }
        }

        private void textBoxOldPwd_TextChanged(object sender, EventArgs e)
        {
            if (MD5_Crypt.GetHashStr(this.textBoxOldPwd.Text.Trim()) == this.mOldPwd)
            {
                this.textBoxNewName.ReadOnly = false;
                this.textBoxNewPwd.ReadOnly = false;
                this.textBoxReNewPwd.ReadOnly = false;
                this.btnOK.Enabled = true;
            }
            else
            {
                this.textBoxNewName.ReadOnly = true;
                this.textBoxNewPwd.ReadOnly = true;
                this.textBoxReNewPwd.ReadOnly = true;
                this.btnOK.Enabled = false;
            }
        }

        private void UpdUserPwdForm_Load(object sender, EventArgs e)
        {
            this.textBoxOldName.Text = this.mUserRealName;
            this.textBoxNewName.ReadOnly = true;
            this.textBoxNewPwd.ReadOnly = true;
            this.textBoxReNewPwd.ReadOnly = true;
            this.btnOK.Enabled = false;
        }
    }
}

