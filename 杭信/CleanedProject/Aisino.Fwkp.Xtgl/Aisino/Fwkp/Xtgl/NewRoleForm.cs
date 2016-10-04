namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.Xtgl.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewRoleForm : DockForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components;
        private List<string> funcIdList = new List<string>();
        private List<PermFunc> funcList = new List<PermFunc>();
        private TriStateTreeView tvFunc;
        private AisinoTXT txtDesc;
        private AisinoTXT txtName;
        private XmlComponentLoader xmlComponentLoader1;

        public NewRoleForm()
        {
            this.Initialize();
        }

        private void AddCheckedNodes(TreeNode treeNode)
        {
            if (treeNode.Checked)
            {
                PermFunc item = new PermFunc(treeNode.Name) {
                    CreateDate = DateTime.Now
                };
                this.funcList.Add(item);
                foreach (TreeNode node in treeNode.Nodes)
                {
                    this.AddCheckedNodes(node);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim() == "")
            {
                MessageManager.ShowMsgBox("INP-131101");
                this.txtName.Focus();
            }
            else if (this.HasNonChecked())
            {
                MessageManager.ShowMsgBox("INP-131104");
                this.tvFunc.Focus();
            }
            else
            {
                try
                {
                    RoleUserDAL rdal = new RoleUserDAL();
                    if (rdal.ExistRoleName(this.txtName.Text.Trim()))
                    {
                        MessageManager.ShowMsgBox("INP-131103");
                        this.txtName.Focus();
                        this.txtName.SelectAll();
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageManager.ShowMsgBox("INP-131301");
                    return;
                }
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

        public Role GetRole()
        {
            Role role;
            role = new Role {
                Name = this.txtName.Text.Trim(),
                Description = this.txtDesc.Text.Trim(),
                CreateDate = DateTime.Now
            };
            role.ID = role.Name.GetHashCode().ToString();
            role.PermFuncList.Clear();
            this.funcIdList.Clear();
            foreach (TreeNode node in this.tvFunc.Nodes)
            {
                this.AddCheckedNodes(node);
            }
            role.PermFuncList.AddRange(this.funcList);
            PermFunc item = new PermFunc("Aisino.Fwkp.Resources.Entry") {
                CreateDate = DateTime.Now
            };
            role.PermFuncList.Add(item);
            return role;
        }

        private bool HasNonChecked()
        {
            foreach (TreeNode node in this.tvFunc.Nodes)
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
            this.txtName = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtName");
            this.txtDesc = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtDesc");
            this.tvFunc = this.xmlComponentLoader1.GetControlByName<TriStateTreeView>("tvFunc");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.tvFunc.CheckBoxes = true;
            this.txtName.KeyPress += new KeyPressEventHandler(this.txtName_KeyPress);
            this.txtName.TextChanged += new EventHandler(this.txtName_TextChanged);
            this.txtDesc.TextChanged += new EventHandler(this.txtDesc_TextChanged);
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
            this.xmlComponentLoader1.Size = new Size(0x181, 0x1bd);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Xtgl.NewRoleForm\Aisino.Fwkp.Xtgl.NewRoleForm.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x181, 0x1bd);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "NewRoleForm";
            this.Text = "新增角色";
            base.Load += new EventHandler(this.NewRoleForm_Load);
            base.ResumeLayout(false);
        }

        private void NewRoleForm_Load(object sender, EventArgs e)
        {
            this.tvFunc.Nodes.Clear();
            TreeLoader.Load(this.tvFunc, this, "/Aisino/Menu", true);
            foreach (TreeNode node in this.tvFunc.Nodes)
            {
                if (node.Text == "系统维护")
                {
                    foreach (TreeNode node2 in node.Nodes)
                    {
                        if (node2.Text == "用户权限设置")
                        {
                            this.tvFunc.Nodes.Remove(node2);
                            break;
                        }
                    }
                }
            }
            if (!UserInfo.IsAdmin)
            {
                if (UserInfo.Gnqx != null)
                {
                    this.funcIdList = UserInfo.Gnqx;
                }
                foreach (TreeNode node3 in this.tvFunc.Nodes)
                {
                    this.RemoveUnPermNodes(node3);
                }
            }
            this.tvFunc.ExpandAll();
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
                this.txtName.Text = StringUtils.GetSubString(this.txtName.Text, 20);
                this.txtName.SelectionStart = selectionStart;
            }
        }
    }
}

