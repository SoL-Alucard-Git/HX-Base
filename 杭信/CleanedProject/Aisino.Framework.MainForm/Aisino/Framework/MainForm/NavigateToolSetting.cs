namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;

    public class NavigateToolSetting : SkinForm
    {
        private bool bool_0;
        private AisinoBTN btnDown;
        private AisinoBTN btnMoveLeftALL;
        private AisinoBTN btnMoveLeftOne;
        private AisinoBTN btnMoveRightALL;
        private AisinoBTN btnMoveRightOne;
        private AisinoBTN btnSave;
        private AisinoBTN btnUp;
        private AisinoGRP groupBox1;
        private AisinoGRP groupBox2;
        private AisinoGRP groupBox3;
        private IContainer icontainer_1;
        private List<NavigateMenuNode> list_0;
        private ListView lvSelect;
        private ListView lvToolBar;
        private NavigateMenuNode navigateMenuNode_0;
        public NavigateToolSaveHander SaveChange;
        private string string_0;

        public NavigateToolSetting(NavigateMenuNode navigateMenuNode_1, string string_1)
        {
            
            this.string_0 = string.Empty;
            this.InitializeComponent_1();
            this.navigateMenuNode_0 = navigateMenuNode_1;
            this.string_0 = string_1;
            this.btnUp.Enabled = false;
            this.btnDown.Enabled = false;
            this.btnMoveLeftALL.Enabled = false;
            this.btnMoveLeftOne.Enabled = false;
            this.btnMoveRightALL.Enabled = false;
            this.btnMoveRightOne.Enabled = false;
            this.btnSave.Enabled = false;
            this.list_0 = new List<NavigateMenuNode>();
            base.Load += new EventHandler(this.NavigateToolSetting_Load);
            base.FormClosing += new FormClosingEventHandler(this.NavigateToolSetting_FormClosing);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (this.lvToolBar.SelectedItems.Count != 0)
            {
                int index = this.lvToolBar.SelectedItems[0].Index;
                int num = index + 1;
                if (num <= (this.lvToolBar.Items.Count - 1))
                {
                    string name = this.lvToolBar.SelectedItems[0].Name;
                    string imageKey = this.lvToolBar.SelectedItems[0].ImageKey;
                    string text = this.lvToolBar.SelectedItems[0].Text;
                    string toolTipText = this.lvToolBar.SelectedItems[0].ToolTipText;
                    string str5 = this.lvToolBar.Items[num].Name;
                    string str6 = this.lvToolBar.Items[num].ImageKey;
                    string str7 = this.lvToolBar.Items[num].Text;
                    string str8 = this.lvToolBar.Items[num].ToolTipText;
                    this.lvToolBar.Items[index].Name = str5;
                    this.lvToolBar.Items[index].ImageKey = str6;
                    this.lvToolBar.Items[index].Text = str7;
                    this.lvToolBar.Items[index].ToolTipText = str8;
                    this.lvToolBar.Items[num].Name = name;
                    this.lvToolBar.Items[num].ImageKey = imageKey;
                    this.lvToolBar.Items[num].Text = text;
                    this.lvToolBar.Items[num].ToolTipText = toolTipText;
                    this.lvToolBar.Focus();
                    this.lvToolBar.Items[index].Selected = false;
                    this.lvToolBar.Items[num].Selected = true;
                    this.method_1(true);
                }
            }
        }

        private void btnMoveLeftALL_Click(object sender, EventArgs e)
        {
            if (this.lvToolBar.SelectedItems.Count > 0)
            {
                ListViewItem item2 = null;
                foreach (ListViewItem item in this.lvToolBar.Items)
                {
                    item2 = new ListViewItem {
                        Name = item.Name,
                        ImageKey = item.ImageKey,
                        Text = item.Text,
                        ToolTipText = item.ToolTipText
                    };
                    this.lvSelect.Items.Add(item2);
                    this.lvToolBar.Items.Remove(item);
                }
                this.method_1(true);
            }
        }

        private void btnMoveLeftOne_Click(object sender, EventArgs e)
        {
            if (this.lvToolBar.SelectedItems.Count > 0)
            {
                ListViewItem item = null;
                foreach (ListViewItem item2 in this.lvToolBar.SelectedItems)
                {
                    item = new ListViewItem {
                        Name = item2.Name,
                        ImageKey = item2.ImageKey,
                        Text = item2.Text,
                        ToolTipText = item2.ToolTipText
                    };
                    this.lvSelect.Items.Add(item);
                    this.lvToolBar.Items.Remove(item2);
                }
                this.method_1(true);
            }
        }

        private void btnMoveRightALL_Click(object sender, EventArgs e)
        {
            if (this.lvSelect.SelectedItems.Count > 0)
            {
                ListViewItem item2 = null;
                foreach (ListViewItem item in this.lvSelect.Items)
                {
                    item2 = new ListViewItem {
                        Name = item.Name,
                        ImageKey = item.ImageKey,
                        Text = item.Text,
                        ToolTipText = item.ToolTipText
                    };
                    this.lvToolBar.Items.Add(item2);
                    this.lvSelect.Items.Remove(item);
                }
                this.method_1(true);
            }
        }

        private void btnMoveRightOne_Click(object sender, EventArgs e)
        {
            if (this.lvSelect.SelectedItems.Count > 0)
            {
                ListViewItem item2 = null;
                foreach (ListViewItem item in this.lvSelect.SelectedItems)
                {
                    item2 = new ListViewItem {
                        Name = item.Name,
                        ImageKey = item.ImageKey,
                        Text = item.Text,
                        ToolTipText = item.ToolTipText
                    };
                    this.lvToolBar.Items.Add(item2);
                    this.lvSelect.Items.Remove(item);
                }
                this.method_1(true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.method_4();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (this.lvToolBar.SelectedItems.Count != 0)
            {
                int index = this.lvToolBar.SelectedItems[0].Index;
                int num = index - 1;
                if (num >= 0)
                {
                    string name = this.lvToolBar.SelectedItems[0].Name;
                    string imageKey = this.lvToolBar.SelectedItems[0].ImageKey;
                    string text = this.lvToolBar.SelectedItems[0].Text;
                    string toolTipText = this.lvToolBar.SelectedItems[0].ToolTipText;
                    string str5 = this.lvToolBar.Items[num].Name;
                    string str6 = this.lvToolBar.Items[num].ImageKey;
                    string str7 = this.lvToolBar.Items[num].Text;
                    string str8 = this.lvToolBar.Items[num].ToolTipText;
                    this.lvToolBar.Items[index].Name = str5;
                    this.lvToolBar.Items[index].ImageKey = str6;
                    this.lvToolBar.Items[index].Text = str7;
                    this.lvToolBar.Items[index].ToolTipText = str8;
                    this.lvToolBar.Items[num].Name = name;
                    this.lvToolBar.Items[num].ImageKey = imageKey;
                    this.lvToolBar.Items[num].Text = text;
                    this.lvToolBar.Items[num].ToolTipText = toolTipText;
                    this.lvToolBar.Focus();
                    this.lvToolBar.Items[index].Selected = false;
                    this.lvToolBar.Items[num].Selected = true;
                    this.method_1(true);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_1 != null))
            {
                this.icontainer_1.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            this.lvToolBar = new ListView();
            this.btnMoveRightOne = new AisinoBTN();
            this.btnMoveRightALL = new AisinoBTN();
            this.btnMoveLeftOne = new AisinoBTN();
            this.btnMoveLeftALL = new AisinoBTN();
            this.lvSelect = new ListView();
            this.btnSave = new AisinoBTN();
            this.groupBox1 = new AisinoGRP();
            this.groupBox2 = new AisinoGRP();
            this.groupBox3 = new AisinoGRP();
            this.btnUp = new AisinoBTN();
            this.btnDown = new AisinoBTN();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.lvToolBar.Dock = DockStyle.Fill;
            this.lvToolBar.HeaderStyle = ColumnHeaderStyle.None;
            this.lvToolBar.Location = new Point(3, 0x11);
            this.lvToolBar.Name = "lvToolBar";
            this.lvToolBar.ShowItemToolTips = true;
            this.lvToolBar.Size = new Size(0xc5, 0x189);
            this.lvToolBar.TabIndex = 0;
            this.lvToolBar.UseCompatibleStateImageBehavior = false;
            this.lvToolBar.View = View.Tile;
            this.lvToolBar.SelectedIndexChanged += new EventHandler(this.lvToolBar_SelectedIndexChanged);
            this.btnMoveRightOne.Location = new Point(6, 0x10c);
            this.btnMoveRightOne.Name = "btnMoveRightOne";
            this.btnMoveRightOne.Size = new Size(40, 20);
            this.btnMoveRightOne.TabIndex = 1;
            this.btnMoveRightOne.Text = ">";
            this.btnMoveRightOne.UseVisualStyleBackColor = true;
            this.btnMoveRightOne.Click += new EventHandler(this.btnMoveRightOne_Click);
            this.btnMoveRightALL.Location = new Point(6, 0xf2);
            this.btnMoveRightALL.Name = "btnMoveRightALL";
            this.btnMoveRightALL.Size = new Size(40, 20);
            this.btnMoveRightALL.TabIndex = 2;
            this.btnMoveRightALL.Text = ">>";
            this.btnMoveRightALL.UseVisualStyleBackColor = true;
            this.btnMoveRightALL.Click += new EventHandler(this.btnMoveRightALL_Click);
            this.btnMoveLeftOne.Location = new Point(6, 0x126);
            this.btnMoveLeftOne.Name = "btnMoveLeftOne";
            this.btnMoveLeftOne.Size = new Size(40, 20);
            this.btnMoveLeftOne.TabIndex = 3;
            this.btnMoveLeftOne.Text = "<";
            this.btnMoveLeftOne.UseVisualStyleBackColor = true;
            this.btnMoveLeftOne.Click += new EventHandler(this.btnMoveLeftOne_Click);
            this.btnMoveLeftALL.Location = new Point(6, 320);
            this.btnMoveLeftALL.Name = "btnMoveLeftALL";
            this.btnMoveLeftALL.Size = new Size(40, 20);
            this.btnMoveLeftALL.TabIndex = 4;
            this.btnMoveLeftALL.Text = "<<";
            this.btnMoveLeftALL.UseVisualStyleBackColor = true;
            this.btnMoveLeftALL.Click += new EventHandler(this.btnMoveLeftALL_Click);
            this.lvSelect.Dock = DockStyle.Fill;
            this.lvSelect.HeaderStyle = ColumnHeaderStyle.None;
            this.lvSelect.Location = new Point(3, 0x11);
            this.lvSelect.Name = "lvSelect";
            this.lvSelect.ShowItemToolTips = true;
            this.lvSelect.Size = new Size(0xb6, 0x189);
            this.lvSelect.TabIndex = 5;
            this.lvSelect.UseCompatibleStateImageBehavior = false;
            this.lvSelect.View = View.Tile;
            this.lvSelect.SelectedIndexChanged += new EventHandler(this.lvSelect_SelectedIndexChanged);
            this.btnSave.Location = new Point(3, 0x15a);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x2d, 0x17);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.groupBox1.Controls.Add(this.lvSelect);
            this.groupBox1.Dock = DockStyle.Left;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xbc, 0x19d);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "具有操作权限的选项";
            this.groupBox2.Controls.Add(this.lvToolBar);
            this.groupBox2.Dock = DockStyle.Fill;
            this.groupBox2.Location = new Point(0xf4, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xcb, 0x19d);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工具栏项";
            this.groupBox3.Controls.Add(this.btnUp);
            this.groupBox3.Controls.Add(this.btnDown);
            this.groupBox3.Controls.Add(this.btnMoveRightALL);
            this.groupBox3.Controls.Add(this.btnMoveRightOne);
            this.groupBox3.Controls.Add(this.btnMoveLeftOne);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.btnMoveLeftALL);
            this.groupBox3.Dock = DockStyle.Left;
            this.groupBox3.Location = new Point(0xbc, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x38, 0x19d);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.btnUp.Location = new Point(15, 0x2f);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new Size(0x19, 30);
            this.btnUp.TabIndex = 8;
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new EventHandler(this.btnUp_Click);
            this.btnDown.Location = new Point(15, 0x54);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new Size(0x19, 30);
            this.btnDown.TabIndex = 7;
            this.btnDown.Text = "↓";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new EventHandler(this.btnDown_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1bf, 0x19d);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox1);
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "NavigateToolSetting";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "工具栏设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void lvSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnMoveRightALL.Enabled = true;
            this.btnMoveRightOne.Enabled = true;
            this.btnMoveLeftALL.Enabled = false;
            this.btnMoveLeftOne.Enabled = false;
        }

        private void lvToolBar_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnMoveLeftALL.Enabled = true;
            this.btnMoveLeftOne.Enabled = true;
            this.btnMoveRightALL.Enabled = false;
            this.btnMoveRightOne.Enabled = false;
            if (this.lvToolBar.SelectedIndices.Count == 1)
            {
                if (this.lvToolBar.SelectedIndices[0] > 0)
                {
                    this.btnUp.Enabled = true;
                }
                if (this.lvToolBar.SelectedIndices[0] < (this.lvToolBar.Items.Count - 1))
                {
                    this.btnDown.Enabled = true;
                }
            }
            else
            {
                this.btnUp.Enabled = false;
                this.btnDown.Enabled = false;
            }
        }

        private bool method_0()
        {
            return this.bool_0;
        }

        private void method_1(bool bool_1)
        {
            this.bool_0 = bool_1;
            if (bool_1)
            {
                this.btnSave.Enabled = true;
            }
            else
            {
                this.btnSave.Enabled = false;
            }
        }

        private void method_2(object object_0, ListView.ListViewItemCollection listViewItemCollection_0)
        {
            if (this.SaveChange != null)
            {
                this.SaveChange(object_0, listViewItemCollection_0);
            }
        }

        private void method_3(NavigateMenuNodeCollection navigateMenuNodeCollection_0)
        {
            foreach (NavigateMenuNode node in navigateMenuNodeCollection_0)
            {
                if ((node.Node != null) && (node.Node.Count > 0))
                {
                    this.method_3(node.Node);
                }
                else
                {
                    this.list_0.Add(node);
                }
            }
        }

        private void method_4()
        {
            this.method_2(this, this.lvToolBar.Items);
            this.method_1(false);
        }

        private void NavigateToolSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.method_0() && (MessageBoxHelper.Show("是否保存修改内容？", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
            {
                this.method_4();
            }
        }

        private void NavigateToolSetting_Load(object sender, EventArgs e)
        {
            this.list_0.Clear();
            this.lvSelect.Items.Clear();
            this.lvToolBar.Items.Clear();
            if (this.navigateMenuNode_0 != null)
            {
                List<string> list2 = new List<string>();
                XmlDocument document = new XmlDocument();
                document.Load(this.string_0);
                if ((document != null) && document.HasChildNodes)
                {
                    XmlNodeList list = document.SelectNodes("root/ToolBarItemCollection/ToolBarItem");
                    if ((list == null) || (list.Count <= 0))
                    {
                        throw new Exception("XML文件格式不正确！");
                    }
                    foreach (XmlNode node in list)
                    {
                        string str = node.Attributes["menuID"].Value;
                        list2.Add(str);
                    }
                }
                this.method_3(this.navigateMenuNode_0.Node);
                ListViewItem item = null;
                foreach (NavigateMenuNode node2 in this.list_0)
                {
                    item = new ListViewItem {
                        Name = node2.Name,
                        ImageKey = node2.ImageKey,
                        Text = node2.SimpleText,
                        ToolTipText = node2.SimpleText
                    };
                    if (list2.Contains(node2.Name))
                    {
                        this.lvToolBar.Items.Add(item);
                    }
                    else
                    {
                        this.lvSelect.Items.Add(item);
                    }
                }
            }
        }
    }
}

