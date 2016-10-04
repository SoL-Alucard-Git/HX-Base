namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.Xtgl.Common;
    using Aisino.Fwkp.Xtgl.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class UserForm : DockForm
    {
        private AisinoCKL chklRole;
        private IContainer components;
        private bool hasDelPermint;
        private bool hasNewPermit;
        private bool hasUpdPermit;
        private bool isEditState;
        private string orgPhoneText = "";
        private ToolStripButton tsBtnCancel;
        private ToolStripButton tsbtnChangePwd;
        private ToolStripButton tsbtnDelUser;
        private ToolStripButton tsbtnNewUser;
        private ToolStripButton tsBtnSave;
        private ToolStripButton tsbtnUpdUser;
        private AisinoTVW tvUser;
        private AisinoTXT txtCreateDate;
        private AisinoTXT txtDesc;
        private AisinoTXT txtName;
        private AisinoTXT txtPhone;
        private RoleUserDAL userDAL = new RoleUserDAL();
        private XmlComponentLoader xmlComponentLoader1;

        public UserForm()
        {
            this.Initialize();
            this.tsBtnCancel.Enabled = false;
            this.tsBtnSave.Enabled = false;
            this.chklRole.Enabled = false;
        }

        private TreeNode AddUserNode(User user)
        {
            TreeNode node = new TreeNode(user.RealName) {
                Tag = user
            };
            this.tvUser.Nodes[0].Nodes.Add(node);
            return node;
        }

        private void ChangeUserBtnState()
        {
            if (UserInfo.IsAdmin)
            {
                this.hasNewPermit = true;
                this.hasUpdPermit = true;
                this.hasDelPermint = true;
            }
            else
            {
                List<string> list = UserInfo.Gnqx;
                if (list != null)
                {
                    if (list.Contains("Menu.Xtgl.Qxsz.Yhgl.NewUser"))
                    {
                        this.hasNewPermit = true;
                    }
                    else
                    {
                        this.hasNewPermit = false;
                    }
                    if (list.Contains("Menu.Xtgl.Qxsz.Yhgl.UpdateUser"))
                    {
                        this.hasUpdPermit = true;
                    }
                    else
                    {
                        this.hasUpdPermit = false;
                    }
                    if (list.Contains("Menu.Xtgl.Qxsz.Yhgl.DeleteUser"))
                    {
                        this.hasDelPermint = true;
                    }
                    else
                    {
                        this.hasDelPermint = false;
                    }
                }
                this.tsbtnNewUser.Enabled = this.hasNewPermit;
                this.tsbtnUpdUser.Enabled = this.hasUpdPermit;
                this.tsbtnDelUser.Enabled = this.hasDelPermint;
            }
        }

        private bool CheckInputValid(string userName)
        {
            string name = this.txtName.Text.Trim();
            if (name.Length == 0)
            {
                MessageBoxHelper.Show("用户名不能为空！", "提示");
                this.txtName.Focus();
                return false;
            }
            if ((userName != name) && this.userDAL.ExistUserName(name))
            {
                MessageManager.ShowMsgBox("INP-132102");
                this.txtName.Focus();
                this.txtName.SelectAll();
                return false;
            }
            if (this.chklRole.CheckedIndices.Count == 0)
            {
                MessageManager.ShowMsgBox("INP-132103");
                this.chklRole.Focus();
                return false;
            }
            return true;
        }

        private bool CheckPhoneFormat()
        {
            string pattern = @"^(((\(\d{3,4}\)|\d{3,4})-)?\d{7,8})$|^((\(\+86\)|\(86\))?0?\d{11})$";
            if (!Regex.Match(this.txtPhone.Text, pattern).Success)
            {
                MessageManager.ShowMsgBox("INP-111001", "提示", new string[] { "电话号码格式错误" });
                this.txtPhone.SelectAll();
                this.txtPhone.Focus();
                return false;
            }
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillUserInfo(User user)
        {
            if (user != null)
            {
                this.txtName.Text = user.RealName;
                this.txtPhone.Text = user.Telephone;
                this.txtDesc.Text = user.Description;
                this.txtCreateDate.Text = user.CreateDate.ToString("yyyy-MM-dd");
                this.InitChkRole(user.IsAdmin);
                this.chklRole.BeginUpdate();
                foreach (int num in this.chklRole.CheckedIndices)
                {
                    this.chklRole.SetItemChecked(num, false);
                }
                foreach (Role role in user.RoleList)
                {
                    for (int i = 0; i < this.chklRole.Items.Count; i++)
                    {
                        if (this.chklRole.Items[i].ToString() == role.Name)
                        {
                            this.chklRole.SetItemChecked(i, true);
                            break;
                        }
                    }
                }
                this.chklRole.EndUpdate();
            }
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

        private bool HasBaseChanged(User user)
        {
            bool flag = false;
            if (this.txtName.Text.Trim() != user.RealName)
            {
                flag = true;
                user.RealName = this.txtName.Text.Trim();
            }
            if (this.txtPhone.Text.Trim() != user.Telephone)
            {
                flag = true;
                user.Telephone = this.txtPhone.Text.Trim();
            }
            if (this.txtDesc.Text.Trim() != user.Description)
            {
                flag = true;
                user.Description = this.txtDesc.Text.Trim();
            }
            return flag;
        }

        private bool HasNonValidChar(string text)
        {
            char[] chArray = text.ToCharArray();
            char[] chArray2 = new char[] { '+', '-', '(', ')' };
            foreach (char ch in chArray)
            {
                if (char.IsDigit(ch))
                {
                    continue;
                }
                bool flag2 = false;
                foreach (char ch2 in chArray2)
                {
                    if (ch2 == ch)
                    {
                        flag2 = true;
                        break;
                    }
                }
                return !flag2;
            }
            return false;
        }

        private bool HasRoleChanged(User user)
        {
            bool flag = false;
            List<Role> collection = new List<Role>();
            List<string> list2 = new List<string>();
            foreach (object obj2 in this.chklRole.CheckedItems)
            {
                collection.Add((Role) obj2);
                list2.Add(((Role) obj2).Name);
            }
            if (collection.Count != user.RoleList.Count)
            {
                flag = true;
            }
            else
            {
                foreach (Role role in user.RoleList)
                {
                    if (!list2.Contains(role.Name))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (flag)
            {
                user.RoleList.Clear();
                user.RoleList.AddRange(collection);
            }
            return flag;
        }

        private void InitChkRole(bool isAdmin)
        {
            List<Role> list = this.userDAL.SelectAllRoleInfo(UserInfo.Yhdm);
            this.chklRole.BeginUpdate();
            this.chklRole.Items.Clear();
            foreach (Role role in list)
            {
                if (isAdmin || (role.Name != "管理员"))
                {
                    this.chklRole.Items.Add(role);
                }
            }
            this.chklRole.EndUpdate();
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.txtPhone = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtPhone");
            this.txtCreateDate = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCreateDate");
            this.txtDesc = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtDesc");
            this.txtName = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtName");
            this.tvUser = this.xmlComponentLoader1.GetControlByName<AisinoTVW>("tvUser");
            this.chklRole = this.xmlComponentLoader1.GetControlByName<AisinoCKL>("chklRole");
            this.tsbtnNewUser = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tsbtnNewUser");
            this.tsbtnUpdUser = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tsbtnUpdUser");
            this.tsbtnDelUser = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tsbtnDelUser");
            this.tsbtnChangePwd = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tsbtnChangePwd");
            this.tsBtnCancel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tsBtnCancel");
            this.tsBtnSave = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tsBtnSave");
            this.txtName.KeyPress += new KeyPressEventHandler(this.txtName_KeyPress);
            this.txtPhone.KeyPress += new KeyPressEventHandler(this.txtPhone_KeyPress);
            this.txtDesc.KeyPress += new KeyPressEventHandler(this.txtDesc_KeyPress);
            this.txtName.TextChanged += new EventHandler(this.txtName_TextChanged);
            this.txtPhone.TextChanged += new EventHandler(this.txtPhone_TextChanged);
            this.txtDesc.TextChanged += new EventHandler(this.txtDesc_TextChanged);
            this.chklRole.CheckOnClick = true;
            this.tsBtnSave.Alignment = ToolStripItemAlignment.Right;
            this.tsBtnCancel.Alignment = ToolStripItemAlignment.Right;
            this.tsbtnNewUser.Image = Resources.user_add;
            this.tsbtnNewUser.ToolTipText = "新增用户";
            this.tsbtnUpdUser.Image = Resources.user_edit;
            this.tsbtnUpdUser.ToolTipText = "修改用户";
            this.tsbtnDelUser.Image = Resources.user_delete;
            this.tsbtnDelUser.ToolTipText = "删除用户";
            this.tsbtnChangePwd.Image = Resources.change_password;
            this.tsbtnChangePwd.ToolTipText = "修改密码";
            this.tsBtnCancel.Image = Resources.cancel;
            this.tsBtnSave.Image = Resources.accept;
            this.tsbtnNewUser.Click += new EventHandler(this.tsbtnNewUser_Click);
            this.tsbtnUpdUser.Click += new EventHandler(this.tsbtnUpdUser_Click);
            this.tsbtnDelUser.Click += new EventHandler(this.tsbtnDelUser_Click);
            this.tsbtnChangePwd.Click += new EventHandler(this.tsbtnChangePwd_Click);
            this.tvUser.AfterSelect += new TreeViewEventHandler(this.tvUser_AfterSelect);
            this.tvUser.BeforeSelect += new TreeViewCancelEventHandler(this.tvUser_BeforeSelect);
            this.tsBtnCancel.Click += new EventHandler(this.tsBtnCancel_Click);
            this.tsBtnSave.Click += new EventHandler(this.tsBtnSave_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x211, 0x1a9);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Xtgl.UserForm\Aisino.Fwkp.Xtgl.UserForm.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x211, 0x1a9);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "UserForm";
            this.Text = "用户管理";
            base.Load += new EventHandler(this.UserForm_Load);
            base.ResumeLayout(false);
        }

        private void InitUserTree()
        {
            this.tvUser.Nodes.Clear();
            TreeNode node = new TreeNode("用户");
            this.tvUser.Nodes.Add(node);
            List<User> list = this.userDAL.SelectUsers(UserInfo.Yhdm);
            if (list.Count > 0)
            {
                this.tvUser.BeginUpdate();
                foreach (User user in list)
                {
                    this.AddUserNode(user);
                }
                this.tvUser.EndUpdate();
                this.tvUser.ExpandAll();
                if (node.Nodes.Count > 0)
                {
                    this.tvUser.SelectedNode = node.Nodes[0];
                }
            }
        }

        private void SetEditControlsEnabled(bool enabled)
        {
            this.isEditState = enabled;
            TreeNode selectedNode = this.tvUser.SelectedNode;
            if (selectedNode.Tag is User)
            {
                User tag = (User) selectedNode.Tag;
                this.tsBtnSave.Enabled = enabled;
                this.tsBtnCancel.Enabled = enabled;
                this.tsbtnUpdUser.Enabled = this.hasUpdPermit && !enabled;
                this.tsbtnNewUser.Enabled = this.hasNewPermit && !enabled;
                this.tsbtnDelUser.Enabled = this.hasDelPermint && !enabled;
                this.tsbtnChangePwd.Enabled = !enabled;
                this.txtPhone.ReadOnly = !enabled;
                if (tag.IsAdmin)
                {
                    this.txtName.ReadOnly = !enabled;
                    this.txtDesc.ReadOnly = !enabled;
                    this.chklRole.Enabled = false;
                }
                else
                {
                    this.txtName.ReadOnly = !enabled;
                    this.txtDesc.ReadOnly = !enabled;
                    this.chklRole.Enabled = enabled;
                }
            }
        }

        private void SetPhoneEnabled(bool enabled)
        {
            this.tsbtnUpdUser.Enabled = !enabled;
            this.tsbtnNewUser.Enabled = !enabled;
            this.tsbtnDelUser.Enabled = !enabled;
            this.tsbtnChangePwd.Enabled = !enabled;
            this.tsBtnSave.Enabled = enabled;
            this.tsBtnCancel.Enabled = enabled;
            this.txtPhone.ReadOnly = !enabled;
            this.txtName.ReadOnly = false;
            this.txtDesc.ReadOnly = false;
            this.chklRole.Enabled = false;
        }

        private void tsBtnCancel_Click(object sender, EventArgs e)
        {
            this.chklRole.ClearSelected();
            this.tvUser.Select();
            this.FillUserInfo((User) this.tvUser.SelectedNode.Tag);
            this.SetEditControlsEnabled(false);
        }

        private void tsbtnChangePwd_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvUser.SelectedNode;
            if (selectedNode.Tag is User)
            {
                User tag = (User) selectedNode.Tag;
                UpdUserPwdForm form = new UpdUserPwdForm {
                    StartPosition = FormStartPosition.CenterScreen,
                    ShowInTaskbar = true
                };
                form.SetOldPwd(tag.Code, tag.Password);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    tag.Password = form.NewPassword;
                }
            }
        }

        private void tsbtnDelUser_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvUser.SelectedNode;
            if (selectedNode.Tag is User)
            {
                User tag = (User) selectedNode.Tag;
                if (tag.IsAdmin)
                {
                    MessageManager.ShowMsgBox("INP-132403", new string[] { tag.RealName });
                }
                else if (MessageManager.ShowMsgBox("INP-132401") == DialogResult.OK)
                {
                    this.tvUser.Nodes[0].Nodes.Remove(selectedNode);
                    if (tag != null)
                    {
                        this.userDAL.DeleteUser(tag.Code);
                    }
                }
            }
        }

        private void tsbtnNewUser_Click(object sender, EventArgs e)
        {
            NewUserForm form = new NewUserForm {
                StartPosition = FormStartPosition.CenterScreen,
                ShowInTaskbar = true
            };
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                User user = form.GetUser();
                TreeNode node = this.AddUserNode(user);
                this.tvUser.SelectedNode = node;
                this.userDAL.InsertUser(user);
            }
        }

        private void tsBtnSave_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvUser.SelectedNode;
            if (this.CheckInputValid(selectedNode.Text))
            {
                User tag = selectedNode.Tag as User;
                if (tag != null)
                {
                    bool flag = this.HasBaseChanged(tag);
                    bool flag2 = this.HasRoleChanged(tag);
                    if (flag && flag2)
                    {
                        this.userDAL.UpdateUser(tag);
                    }
                    else if (flag)
                    {
                        this.userDAL.UpdateUserBase(tag);
                    }
                    else if (flag2)
                    {
                        this.userDAL.UpdateUserRole(tag);
                    }
                    if (selectedNode.Text != this.txtName.Text.Trim())
                    {
                        selectedNode.Text = this.txtName.Text.Trim();
                        if (tag.IsAdmin)
                        {
                            MessageBoxHelper.Show("管理员名称已修改，将重启开票软件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            FormMain.ResetForm();
                        }
                    }
                }
                this.SetEditControlsEnabled(false);
                this.chklRole.ClearSelected();
                this.tvUser.Select();
            }
        }

        private void tsbtnUpdUser_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvUser.SelectedNode;
            if (selectedNode.Tag is User)
            {
                User tag = (User) selectedNode.Tag;
                this.SetEditControlsEnabled(true);
                this.txtPhone.SelectAll();
            }
        }

        private void tvUser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = this.tvUser.SelectedNode;
            if (selectedNode.Tag is User)
            {
                User tag = (User) selectedNode.Tag;
                this.FillUserInfo((User) selectedNode.Tag);
            }
        }

        private void tvUser_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.isEditState)
            {
                if (MessageBoxHelper.Show("确定离开对当前用户的修改么？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    this.SetEditControlsEnabled(false);
                }
            }
        }

        private void txtDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.txtDesc.SelectionStart;
            this.txtDesc.Text = StringUtils.GetSubString(this.txtDesc.Text, 100);
            this.txtDesc.SelectionStart = selectionStart;
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
                if (char.IsDigit(e.KeyChar))
                {
                    int length = GetLength(this.txtPhone.Text);
                    int num2 = GetLength(this.txtPhone.SelectedText);
                    if (((length - num2) + 1) > 50)
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    char[] chArray = new char[] { '+', '-', '(', ')' };
                    bool flag = false;
                    foreach (char ch in chArray)
                    {
                        if (ch == e.KeyChar)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (this.HasNonValidChar(this.txtPhone.Text))
            {
                this.txtPhone.Text = this.orgPhoneText;
            }
            else
            {
                byte[] bytes = ToolUtil.GetBytes(this.txtPhone.Text);
                if (bytes.Length >= 50)
                {
                    this.txtPhone.Text = ToolUtil.GetString(bytes, 0, 50);
                }
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            this.ChangeUserBtnState();
            this.InitChkRole(true);
            this.InitUserTree();
        }
    }
}

