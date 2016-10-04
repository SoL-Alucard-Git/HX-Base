namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.Xtgl.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewUserForm : DockForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoCKL chklRole;
        private IContainer components;
        private string orgPhoneText = "";
        private AisinoTXT txtCreateDate;
        private AisinoTXT txtCreator;
        private AisinoTXT txtDescription;
        private AisinoTXT txtName;
        private AisinoTXT txtPhone;
        private AisinoTXT txtPwd;
        private XmlComponentLoader xmlComponentLoader1;

        public NewUserForm()
        {
            this.Initialize();
            this.txtCreator.Text = UserInfo.Yhmc;
            this.txtCreateDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string name = this.txtName.Text.Trim();
            if (name.Length == 0)
            {
                MessageBoxHelper.Show("用户名不能为空！", "提示");
                this.txtName.Focus();
            }
            else
            {
                string str2 = this.txtPwd.Text.Trim();
                if ((str2 == "") || (str2.Length < 4))
                {
                    MessageManager.ShowMsgBox("INP-132203");
                    this.txtPwd.Focus();
                }
                else
                {
                    RoleUserDAL rdal = new RoleUserDAL();
                    if (rdal.ExistUserName(name))
                    {
                        MessageManager.ShowMsgBox("INP-132102");
                        this.txtName.Focus();
                        this.txtName.SelectAll();
                    }
                    else if (this.chklRole.CheckedIndices.Count == 0)
                    {
                        MessageManager.ShowMsgBox("INP-132103");
                        this.chklRole.Focus();
                    }
                    else
                    {
                        base.DialogResult = DialogResult.OK;
                    }
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

        public User GetUser()
        {
            User user = new User();
            user = new User {
                RealName = this.txtName.Text.Trim(),
                Password = MD5_Crypt.GetHashStr(this.txtPwd.Text.Trim()),
                Telephone = this.txtPhone.Text.Trim(),
                CreateDate = DateTime.Now,
                Description = this.txtDescription.Text.Trim()
            };
            user.Code = user.RealName.GetHashCode().ToString();
            foreach (object obj2 in this.chklRole.CheckedItems)
            {
                if (obj2 is Role)
                {
                    user.RoleList.Add((Role) obj2);
                }
            }
            return user;
        }

        private bool HasNonDigitChar(string text)
        {
            foreach (char ch in text.ToCharArray())
            {
                if (!char.IsDigit(ch))
                {
                    return true;
                }
            }
            return false;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.txtName = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtName");
            this.txtPwd = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtPwd");
            this.txtPwd.MaxLength = 0x10;
            this.txtPhone = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtPhone");
            this.txtDescription = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtDescription");
            this.txtCreator = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCreator");
            this.txtCreateDate = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCreateDate");
            this.chklRole = this.xmlComponentLoader1.GetControlByName<AisinoCKL>("chklRole");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.txtName.KeyPress += new KeyPressEventHandler(this.txtName_KeyPress);
            this.txtPwd.KeyPress += new KeyPressEventHandler(this.txtPwd_KeyPress);
            this.txtPhone.KeyPress += new KeyPressEventHandler(this.txtPhone_KeyPress);
            this.txtName.TextChanged += new EventHandler(this.txtName_TextChanged);
            this.txtPwd.TextChanged += new EventHandler(this.txtPwd_TextChanged);
            this.txtPhone.TextChanged += new EventHandler(this.txtPhone_TextChanged);
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;
            this.txtDescription.KeyPress += new KeyPressEventHandler(this.txtDescription_KeyPress);
            this.txtDescription.TextChanged += new EventHandler(this.txtDescription_TextChanged);
            this.chklRole.CheckOnClick = true;
            this.txtPwd.PasswordChar = '*';
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
            this.xmlComponentLoader1.Size = new Size(0x162, 410);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Xtgl.NewUserForm\Aisino.Fwkp.Xtgl.NewUserForm.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x162, 410);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "NewUserForm";
            this.Text = "新增用户";
            base.Load += new EventHandler(this.NewUserForm_Load);
            base.ResumeLayout(false);
        }

        private void NewUserForm_Load(object sender, EventArgs e)
        {
            List<Role> list = new RoleUserDAL().SelectAllRoleInfo(UserInfo.Yhdm);
            this.chklRole.BeginUpdate();
            this.chklRole.Items.Clear();
            foreach (Role role in list)
            {
                if (role.Name != "管理员")
                {
                    this.chklRole.Items.Add(role);
                }
            }
            this.chklRole.EndUpdate();
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.txtDescription.SelectionStart;
            this.txtDescription.Text = StringUtils.GetSubString(this.txtDescription.Text, 100);
            this.txtDescription.SelectionStart = selectionStart;
            this.txtDescription.ScrollToCaret();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char.IsControl(e.KeyChar);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim() != "")
            {
                int selectionStart = this.txtName.SelectionStart;
                this.txtName.Text = StringUtils.GetSubString(this.txtName.Text, 8);
                this.txtName.SelectionStart = selectionStart;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.orgPhoneText = this.txtPhone.Text;
            if (!char.IsControl(e.KeyChar))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                else
                {
                    int length = GetLength(this.txtPhone.Text);
                    int num2 = GetLength(this.txtPhone.SelectedText);
                    if (((length - num2) + 1) > 50)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (this.HasNonDigitChar(this.txtPhone.Text))
            {
                this.txtPhone.Text = this.orgPhoneText;
            }
            else
            {
                int selectionStart = this.txtPhone.SelectionStart;
                this.txtPhone.Text = StringUtils.GetSubString(this.txtPhone.Text, 50).Trim();
                this.txtPhone.SelectionStart = selectionStart;
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.txtPwd.SelectionStart;
            this.txtPwd.Text = StringUtils.GetSubString(this.txtPwd.Text, 20).Trim();
            this.txtPwd.SelectionStart = selectionStart;
        }
    }
}

