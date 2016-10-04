namespace Aisino.Fwkp.Xtgl
{
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
    using System.Windows.Forms;

    public class RoleForm : DockForm
    {
        private const string ADMIN = "管理员";
        private IContainer components;
        private List<string> funcIdList = new List<string>();
        private bool hasDelPermint;
        private bool hasNewPermit;
        private bool hasUpdPermit;
        private bool isEditState;
        private RoleUserDAL roleDAL = new RoleUserDAL();
        private List<string> selFuncIdList = new List<string>();
        private ToolStripButton tsBtnCancel;
        private ToolStripButton tsbtnDelRole;
        private ToolStripButton tsbtnNewRole;
        private ToolStripButton tsBtnSave;
        private ToolStripButton tsbtnUpdRole;
        private AisinoTVW tvRole;
        private TriStateTreeView tvRolePerm;
        private AisinoTXT txtRoleCreateDate;
        private AisinoTXT txtRoleCreator;
        private AisinoTXT txtRoleDesc;
        private AisinoTXT txtRoleName;
        private XmlComponentLoader xmlComponentLoader1;

        public RoleForm()
        {
            this.Initialize();
            this.tsBtnCancel.Enabled = false;
            this.tsBtnSave.Enabled = false;
            this.tvRolePerm.CheckBoxesEnabled = false;
            this.tvRolePerm.ItemHeight = 0x12;
        }

        private void AddCheckedFuncNodes(TreeNode treeNode)
        {
            if (treeNode.Checked)
            {
                this.selFuncIdList.Add(treeNode.Name);
                foreach (TreeNode node in treeNode.Nodes)
                {
                    this.AddCheckedFuncNodes(node);
                }
            }
        }

        private TreeNode AddRoleNode(Role role)
        {
            TreeNode node = new TreeNode(role.Name) {
                Tag = role
            };
            this.tvRole.Nodes[0].Nodes.Add(node);
            return node;
        }

        private void ChangeRoleBtnState()
        {
            if (this.funcIdList.Contains("Menu.Xtgl.Qxsz.Jsgl.NewRole"))
            {
                this.hasNewPermit = true;
            }
            else
            {
                this.hasNewPermit = false;
            }
            if (this.funcIdList.Contains("Menu.Xtgl.Qxsz.Jsgl.UpdateRole"))
            {
                this.hasUpdPermit = true;
            }
            else
            {
                this.hasUpdPermit = false;
            }
            if (this.funcIdList.Contains("Menu.Xtgl.Qxsz.Jsgl.DeleteRole"))
            {
                this.hasDelPermint = true;
            }
            else
            {
                this.hasDelPermint = false;
            }
            this.tsbtnNewRole.Enabled = this.hasNewPermit;
            this.tsbtnUpdRole.Enabled = this.hasUpdPermit;
            this.tsbtnDelRole.Enabled = this.hasDelPermint;
        }

        private bool CheckInputValid(string roleName)
        {
            string name = this.txtRoleName.Text.Trim();
            if (name == "")
            {
                MessageManager.ShowMsgBox("INP-131101");
                this.txtRoleName.Focus();
                return false;
            }
            if ((name != roleName) && this.roleDAL.ExistRoleName(name))
            {
                MessageManager.ShowMsgBox("INP-131103");
                this.txtRoleName.Focus();
                this.txtRoleName.SelectAll();
                return false;
            }
            if (this.HasNonChecked())
            {
                MessageManager.ShowMsgBox("INP-131104");
                this.tvRolePerm.Focus();
                return false;
            }
            return true;
        }

        private void ClearCheckedNodes()
        {
            foreach (TreeNode node in this.tvRolePerm.Nodes)
            {
                foreach (TreeNode node2 in TreeHelper.TraversalLeafNodes(node))
                {
                    if (node2.Checked)
                    {
                        node2.Checked = false;
                    }
                }
            }
        }

        private void CollapseUncheckedNodes()
        {
            this.tvRolePerm.BeginUpdate();
            this.tvRolePerm.CollapseAll();
            this.tvRolePerm.BeforeExpand += new TreeViewCancelEventHandler(this.tvRolePerm_BeforeExpand);
            this.tvRolePerm.ExpandAll();
            this.tvRolePerm.BeforeExpand -= new TreeViewCancelEventHandler(this.tvRolePerm_BeforeExpand);
            this.tvRolePerm.EndUpdate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillRoleInfo(Role role)
        {
            if (role != null)
            {
                this.txtRoleName.Text = role.Name;
                this.txtRoleDesc.Text = role.Description;
                this.txtRoleCreator.Text = role.CreatorName;
                this.txtRoleCreateDate.Text = role.CreateDate.ToString("yyyy-MM-dd");
                this.tvRolePerm.BeginUpdate();
                this.ClearCheckedNodes();
                if (role.Name == "管理员")
                {
                    foreach (TreeNode node in this.tvRolePerm.Nodes)
                    {
                        node.Checked = true;
                    }
                }
                else
                {
                    foreach (PermFunc func in role.PermFuncList)
                    {
                        foreach (TreeNode node2 in this.tvRolePerm.Nodes.Find(func.FuncID, true))
                        {
                            if (node2.Nodes.Count == 0)
                            {
                                node2.Checked = true;
                            }
                        }
                    }
                }
                this.tvRolePerm.EndUpdate();
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

        private bool HasBaseChanged(Role role)
        {
            bool flag = false;
            if (this.txtRoleName.Text.Trim() != role.Name)
            {
                flag = true;
                role.Name = this.txtRoleName.Text.Trim();
            }
            if (this.txtRoleDesc.Text.Trim() != role.Description)
            {
                flag = true;
                role.Description = this.txtRoleDesc.Text.Trim();
            }
            return flag;
        }

        private bool HasCheckedChildNodes(TreeNode node)
        {
            if (node.Nodes.Count != 0)
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    if (node2.Checked)
                    {
                        return true;
                    }
                    if (this.HasCheckedChildNodes(node2))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool HasFuncChanged(Role role)
        {
            bool flag = false;
            this.selFuncIdList.Clear();
            foreach (TreeNode node in this.tvRolePerm.Nodes)
            {
                this.AddCheckedFuncNodes(node);
            }
            if (this.selFuncIdList.Count != role.PermFuncList.Count)
            {
                flag = true;
            }
            else
            {
                foreach (PermFunc func in role.PermFuncList)
                {
                    if ((func != null) && !this.selFuncIdList.Contains(func.FuncID))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (flag)
            {
                role.PermFuncList.Clear();
                foreach (string str in this.selFuncIdList)
                {
                    PermFunc func2 = new PermFunc(str) {
                        CreateDate = DateTime.Now
                    };
                    role.PermFuncList.Add(func2);
                }
                PermFunc item = new PermFunc("Aisino.Fwkp.Resources.Entry") {
                    CreateDate = DateTime.Now
                };
                role.PermFuncList.Add(item);
            }
            return flag;
        }

        private bool HasNonChecked()
        {
            foreach (TreeNode node in this.tvRolePerm.Nodes)
            {
                if (node.Checked)
                {
                    return false;
                }
            }
            return true;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.txtRoleCreateDate = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtRoleCreateDate");
            this.txtRoleCreator = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtRoleCreator");
            this.txtRoleDesc = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtRoleDesc");
            this.txtRoleName = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtRoleName");
            this.tvRole = this.xmlComponentLoader1.GetControlByName<AisinoTVW>("tvRole");
            this.tvRolePerm = this.xmlComponentLoader1.GetControlByName<TriStateTreeView>("tvRolePerm");
            this.tsbtnNewRole = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tsbtnNewRole");
            this.tsbtnNewRole.ToolTipText = "新增角色";
            this.tsbtnUpdRole = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tsbtnUpdRole");
            this.tsbtnUpdRole.ToolTipText = "修改角色";
            this.tsbtnDelRole = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tsbtnDelRole");
            this.tsbtnDelRole.ToolTipText = "删除角色";
            this.tsBtnCancel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tsBtnCancel");
            this.tsBtnSave = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tsBtnSave");
            this.txtRoleName.KeyPress += new KeyPressEventHandler(this.txtRoleName_KeyPress);
            this.txtRoleDesc.KeyPress += new KeyPressEventHandler(this.txtRoleDesc_KeyPress);
            this.txtRoleName.TextChanged += new EventHandler(this.txtRoleName_TextChanged);
            this.txtRoleDesc.TextChanged += new EventHandler(this.txtRoleDesc_TextChanged);
            this.tvRole.HideSelection = false;
            this.tvRolePerm.CheckBoxes = true;
            this.tsBtnSave.Alignment = ToolStripItemAlignment.Right;
            this.tsBtnCancel.Alignment = ToolStripItemAlignment.Right;
            this.tsbtnNewRole.Image = Resources.group_add;
            this.tsbtnUpdRole.Image = Resources.group_edit;
            this.tsbtnDelRole.Image = Resources.group_delete;
            this.tsBtnCancel.Image = Resources.cancel;
            this.tsBtnSave.Image = Resources.accept;
            this.tsbtnNewRole.Click += new EventHandler(this.tsbtnNewRole_Click);
            this.tsbtnUpdRole.Click += new EventHandler(this.tsbtnUpdRole_Click);
            this.tsbtnDelRole.Click += new EventHandler(this.tsbtnDelRole_Click);
            this.tvRole.BeforeSelect += new TreeViewCancelEventHandler(this.tvRole_BeforeSelect);
            this.tvRole.AfterSelect += new TreeViewEventHandler(this.tvRole_AfterSelect);
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
            this.xmlComponentLoader1.Size = new Size(0x248, 0x20a);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Xtgl.RoleForm\Aisino.Fwkp.Xtgl.RoleForm.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x248, 0x20a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "RoleForm";
            this.Text = "角色管理";
            base.Load += new EventHandler(this.RoleForm_Load);
            base.ResumeLayout(false);
        }

        private void InitRoleTree()
        {
            List<Role> list = this.roleDAL.SelectRoles(UserInfo.Yhdm);
            if (list.Count > 0)
            {
                this.tvRole.BeginUpdate();
                foreach (Role role in list)
                {
                    this.AddRoleNode(role);
                }
                this.tvRole.EndUpdate();
                this.tvRole.ExpandAll();
            }
        }

        private void RemoveUnPermNodes(TreeNode node)
        {
            if (this.funcIdList.Contains(node.Name))
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    this.RemoveUnPermNodes(node2);
                }
            }
            else
            {
                node.Remove();
            }
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            this.tvRole.Nodes.Clear();
            TreeNode node = new TreeNode("角色");
            this.tvRole.Nodes.Add(node);
            this.tvRolePerm.Nodes.Clear();
            this.InitRoleTree();
            if (node.Nodes.Count > 0)
            {
                TreeLoader.Load(this.tvRolePerm, this, "/Aisino/Menu", true);
                if (UserInfo.IsAdmin)
                {
                    this.hasNewPermit = true;
                    this.hasUpdPermit = true;
                    this.hasDelPermint = true;
                }
                else
                {
                    if (UserInfo.Gnqx != null)
                    {
                        this.funcIdList = UserInfo.Gnqx;
                    }
                    foreach (TreeNode node2 in this.tvRolePerm.Nodes)
                    {
                        this.RemoveUnPermNodes(node2);
                    }
                    this.ChangeRoleBtnState();
                }
                this.tvRole.SelectedNode = node.Nodes[0];
            }
        }

        private void SetFuncsEnabled(bool enabled)
        {
            this.isEditState = enabled;
            this.tvRolePerm.CheckBoxesEnabled = enabled;
            this.tsBtnSave.Enabled = enabled;
            this.tsBtnCancel.Enabled = enabled;
            this.tsbtnUpdRole.Enabled = this.hasUpdPermit && !enabled;
            this.tsbtnNewRole.Enabled = this.hasNewPermit && !enabled;
            this.tsbtnDelRole.Enabled = this.hasDelPermint && !enabled;
            this.txtRoleName.ReadOnly = !enabled;
            this.txtRoleDesc.ReadOnly = !enabled;
        }

        private void tsBtnCancel_Click(object sender, EventArgs e)
        {
            this.SetFuncsEnabled(false);
            this.tvRole.Select();
            this.FillRoleInfo((Role) this.tvRole.SelectedNode.Tag);
        }

        private void tsbtnDelRole_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvRole.SelectedNode;
            if (selectedNode.Tag is Role)
            {
                Role tag = (Role) selectedNode.Tag;
                if (tag.Name == "管理员")
                {
                    MessageManager.ShowMsgBox("INP-131403");
                }
                else if (this.roleDAL.IsExistUsrBelongToRole(tag.ID))
                {
                    MessageManager.ShowMsgBox("INP-131404");
                }
                else if (MessageManager.ShowMsgBox("INP-131401") == DialogResult.OK)
                {
                    this.tvRole.Nodes[0].Nodes.Remove(selectedNode);
                    if (tag != null)
                    {
                        this.roleDAL.DeleteRole(tag.ID);
                    }
                }
            }
        }

        private void tsbtnNewRole_Click(object sender, EventArgs e)
        {
            NewRoleForm form = new NewRoleForm {
                StartPosition = FormStartPosition.CenterScreen,
                ShowInTaskbar = true
            };
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Role role = form.GetRole();
                TreeNode node = this.AddRoleNode(role);
                this.tvRole.SelectedNode = node;
                this.roleDAL.InsertRole(role);
            }
        }

        private void tsBtnSave_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvRole.SelectedNode;
            if (this.CheckInputValid(selectedNode.Text))
            {
                Role tag = selectedNode.Tag as Role;
                if (tag != null)
                {
                    bool flag = this.HasBaseChanged(tag);
                    bool flag2 = this.HasFuncChanged(tag);
                    if (flag && flag2)
                    {
                        selectedNode.Text = this.txtRoleName.Text.Trim();
                        this.roleDAL.UpdateRole(tag);
                    }
                    else if (flag)
                    {
                        selectedNode.Text = this.txtRoleName.Text.Trim();
                        this.roleDAL.UpdateRoleBase(tag);
                    }
                    else if (flag2)
                    {
                        this.roleDAL.UpdateRoleFuncs(tag);
                    }
                }
                this.SetFuncsEnabled(false);
                this.tvRole.Select();
            }
        }

        private void tsbtnUpdRole_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvRole.SelectedNode;
            if (selectedNode.Tag is Role)
            {
                Role tag = (Role) selectedNode.Tag;
                if (tag.Name == "管理员")
                {
                    MessageManager.ShowMsgBox("INP-131201");
                }
                else
                {
                    this.SetFuncsEnabled(true);
                    this.txtRoleName.SelectAll();
                }
            }
        }

        private void tvRole_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = this.tvRole.SelectedNode;
            if (selectedNode.Tag is Role)
            {
                Role tag = (Role) selectedNode.Tag;
                if (tag.Name != "管理员")
                {
                    this.tsbtnUpdRole.Enabled = true;
                    this.tsbtnDelRole.Enabled = true;
                    foreach (TreeNode node2 in this.tvRolePerm.Nodes)
                    {
                        if (node2.Text == "系统维护")
                        {
                            foreach (TreeNode node3 in node2.Nodes)
                            {
                                if (node3.Text == "用户权限设置")
                                {
                                    this.tvRolePerm.Nodes.Remove(node3);
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.tsbtnUpdRole.Enabled = false;
                    this.tsbtnDelRole.Enabled = false;
                    this.tvRolePerm.Nodes.Clear();
                    TreeLoader.Load(this.tvRolePerm, this, "/Aisino/Menu", true);
                }
                this.FillRoleInfo((Role) selectedNode.Tag);
                this.CollapseUncheckedNodes();
            }
        }

        private void tvRole_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.isEditState)
            {
                if (MessageBoxHelper.Show("确定离开对当前角色的修改么？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    this.SetFuncsEnabled(false);
                }
            }
        }

        private void tvRolePerm_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (!this.HasCheckedChildNodes(e.Node))
            {
                e.Cancel = true;
            }
        }

        private void txtRoleDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtRoleDesc_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.txtRoleDesc.SelectionStart;
            this.txtRoleDesc.Text = StringUtils.GetSubString(this.txtRoleDesc.Text, 100);
            this.txtRoleDesc.SelectionStart = selectionStart;
        }

        private void txtRoleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char.IsControl(e.KeyChar);
        }

        private void txtRoleName_TextChanged(object sender, EventArgs e)
        {
            if (this.txtRoleName.Text.Trim() != "")
            {
                int selectionStart = this.txtRoleName.SelectionStart;
                this.txtRoleName.Text = StringUtils.GetSubString(this.txtRoleName.Text, 20);
                this.txtRoleName.SelectionStart = selectionStart;
            }
        }
    }
}

