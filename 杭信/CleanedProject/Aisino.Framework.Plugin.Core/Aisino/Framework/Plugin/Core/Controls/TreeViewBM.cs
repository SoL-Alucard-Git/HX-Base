namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [Description("树编码控件")]
    public class TreeViewBM : UserControl
    {
        public ToolStripMenuItem AddFenLeiToolStripMenuItem;
        public ToolStripMenuItem AddFLToolStripMenuItem;
        public ToolStripMenuItem AddSubNodeToolStripMenuItem;
        public BackgroundWorker backgroundWorker1;
        public ToolStripMenuItem BMZJWToolStripMenuItem;
        public ToolStripMenuItem BMZSCToolStripMenuItem;
        public ToolStripMenuItem BMZZWToolStripMenuItem;
        private bool bool_0;
        private bool bool_1;
        public ToolStripMenuItem CodeToolStripMenuItem;
        public ContextMenuStrip contextMenuStrip1;
        private IContainer icontainer_0;
        public ImageList imageList1;
        public AisinoLBL labelUI;
        public List<TreeNodeTemp> listNode;
        public ToolStripMenuItem ModifyFLToolStripMenuItem;
        public ToolStripMenuItem ModToolStripMenuItem;
        public ToolStripMenuItem NameToolStripMenuItem;
        public AisinoPIC pictureBox1;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        public TableLayoutPanel tableLayoutPanel1;
        public ToolStrip toolStrip1;
        public ToolStripDropDownButton toolStripDropDownButton1;
        public ToolStripLabel toolStripLabel1;
        public ToolStripSeparator toolStripSeparator1;
        public ToolStripSeparator toolStripSeparator2;
        public ToolStripSeparator toolStripSeparator3;
        private TreeNode treeNode_0;
        public AisinoTVW treeView1;
        public ToolStripMenuItem UToolStripMenuItem;
        public ToolStripMenuItem VToolStripMenuItem;
        public ToolStripMenuItem WToolStripMenuItem;

        public event GetListNodes getListNodes;

        public event OnClickAdd onClickAdd;

        public event OnClickAddFenLei onClickAddFenLei;

        public event OnClickDelete onClickDelete;

        public event OnClickJianWei onClickJianWei;

        public event OnClickModify onClickModify;

        public event OnClickZengWei onClickZengWei;

        public event OnTreeNodeClick onTreeNodeClick;

        public TreeViewBM()
        {
            
            this.string_0 = "根节点";
            this.string_2 = "";
            this.string_3 = "";
            this.listNode = new List<TreeNodeTemp>();
            this.InitializeComponent();
            this.labelUI.BringToFront();
            if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
            {
                this.treeView1.ShowNodeToolTips = false;
            }
            else
            {
                this.treeView1.ShowNodeToolTips = true;
            }
            this.toolStrip1.BackColor = SystemColor.GRID_TITLE_BACKCOLOR;
        }

        private void AddFenLeiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeSelectEventArgs args = new TreeSelectEventArgs(this.treeView1.SelectedNode.Name.Trim());
            this.onClickAddFenLei(sender, args);
        }

        private void AddSubNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeSelectEventArgs args = new TreeSelectEventArgs(this.treeView1.SelectedNode.Name.Trim());
            this.onClickAdd(sender, args);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.listNode = this.getListNodes(this.treeNode_0.Name);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.method_1(this.listNode, this.treeNode_0);
            this.treeNode_0.Text = this.string_1;
            this.treeNode_0.Expand();
            this.pictureBox1.Visible = false;
        }

        private void BMZJWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int length = this.treeView1.SelectedNode.Name.Length;
            if (this.onClickJianWei(sender, e))
            {
                this.method_8(this.treeView1.SelectedNode, length);
            }
        }

        private void BMZSCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.onClickDelete(sender, e);
            this.method_4(new TreeViewEventArgs(this.SelectedNode));
        }

        private void BMZZWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int length = this.treeView1.SelectedNode.Name.Length;
            if (this.onClickZengWei(sender, e))
            {
                this.method_7(this.treeView1.SelectedNode, length);
            }
        }

        private void CodeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CodeToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.NameToolStripMenuItem.Checked = false;
                this.CodeToolStripMenuItem.Enabled = false;
                this.NameToolStripMenuItem.Enabled = true;
                if (this.treeView1.Nodes.Count > 0)
                {
                    this.method_3(this.treeView1.Nodes[0]);
                }
                this.bool_0 = true;
                PropertyUtil.SetValue(this.RootNodeString.Replace('/', '-') + "编码-名称", this.bool_0.ToString());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(TreeViewBM));
            this.imageList1 = new ImageList(this.icontainer_0);
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.treeView1 = new AisinoTVW();
            this.toolStrip1 = new ToolStrip();
            this.toolStripDropDownButton1 = new ToolStripDropDownButton();
            this.AddFLToolStripMenuItem = new ToolStripMenuItem();
            this.ModifyFLToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.UToolStripMenuItem = new ToolStripMenuItem();
            this.VToolStripMenuItem = new ToolStripMenuItem();
            this.WToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.CodeToolStripMenuItem = new ToolStripMenuItem();
            this.NameToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripLabel1 = new ToolStripLabel();
            this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
            this.AddFenLeiToolStripMenuItem = new ToolStripMenuItem();
            this.ModToolStripMenuItem = new ToolStripMenuItem();
            this.AddSubNodeToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.BMZZWToolStripMenuItem = new ToolStripMenuItem();
            this.BMZJWToolStripMenuItem = new ToolStripMenuItem();
            this.BMZSCToolStripMenuItem = new ToolStripMenuItem();
            this.backgroundWorker1 = new BackgroundWorker();
            this.pictureBox1 = new AisinoPIC();
            this.labelUI = new AisinoLBL();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.imageList1.ImageStream = (ImageListStreamer) manager.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "small1.BMP");
            this.imageList1.Images.SetKeyName(1, "small1-1.BMP");
            this.imageList1.Images.SetKeyName(2, "folder-close.png");
            this.imageList1.Images.SetKeyName(3, "folder.png");
            this.imageList1.Images.SetKeyName(4, "file.gif");
            this.imageList1.Images.SetKeyName(5, "circleLoading.gif");
            this.imageList1.Images.SetKeyName(6, "folder1.gif");
            this.imageList1.Images.SetKeyName(7, "closed.gif");
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Size = new Size(0x7f, 0xf4);
            this.tableLayoutPanel1.TabIndex = 1;
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new Point(3, 0x27);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 1;
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new Size(0x79, 0xca);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeExpand += new TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.toolStrip1.BackColor = SystemColors.ActiveCaption;
            this.toolStrip1.Dock = DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripDropDownButton1, this.toolStripLabel1 });
            this.toolStrip1.LayoutStyle = ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = ToolStripRenderMode.System;
            this.toolStrip1.Size = new Size(0x7f, 0x24);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripDropDownButton1.AutoSize = false;
            this.toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { this.AddFLToolStripMenuItem, this.ModifyFLToolStripMenuItem, this.toolStripSeparator3, this.UToolStripMenuItem, this.VToolStripMenuItem, this.WToolStripMenuItem, this.toolStripSeparator1, this.CodeToolStripMenuItem, this.NameToolStripMenuItem });
            this.toolStripDropDownButton1.ForeColor = SystemColors.ButtonHighlight;
            this.toolStripDropDownButton1.Image = Class131.smethod_49();
            this.toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new Size(0x1d, 0x24);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ToolTipText = "编码族管理";
            this.toolStripDropDownButton1.Click += new EventHandler(this.toolStripDropDownButton1_Click);
            this.AddFLToolStripMenuItem.Image = Class131.增加分类;
            this.AddFLToolStripMenuItem.Name = "AddFLToolStripMenuItem";
            this.AddFLToolStripMenuItem.Size = new Size(0x9c, 0x16);
            this.AddFLToolStripMenuItem.Text = "增加分类";
            this.AddFLToolStripMenuItem.Click += new EventHandler(this.AddFenLeiToolStripMenuItem_Click);
            this.ModifyFLToolStripMenuItem.Image = Class131.修改分类;
            this.ModifyFLToolStripMenuItem.Name = "ModifyFLToolStripMenuItem";
            this.ModifyFLToolStripMenuItem.Size = new Size(0x9c, 0x16);
            this.ModifyFLToolStripMenuItem.Text = "修改分类";
            this.ModifyFLToolStripMenuItem.Click += new EventHandler(this.ModToolStripMenuItem_Click);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(0x99, 6);
            this.UToolStripMenuItem.Image = Class131.编码族增位;
            this.UToolStripMenuItem.Name = "UToolStripMenuItem";
            this.UToolStripMenuItem.Size = new Size(0x9c, 0x16);
            this.UToolStripMenuItem.Text = "编码族增位(&U)";
            this.UToolStripMenuItem.Click += new EventHandler(this.BMZZWToolStripMenuItem_Click);
            this.VToolStripMenuItem.Image = Class131.编码族减位;
            this.VToolStripMenuItem.Name = "VToolStripMenuItem";
            this.VToolStripMenuItem.Size = new Size(0x9c, 0x16);
            this.VToolStripMenuItem.Text = "编码族减位(&V)";
            this.VToolStripMenuItem.Click += new EventHandler(this.BMZJWToolStripMenuItem_Click);
            this.WToolStripMenuItem.Image = Class131.编码族删除;
            this.WToolStripMenuItem.Name = "WToolStripMenuItem";
            this.WToolStripMenuItem.Size = new Size(0x9c, 0x16);
            this.WToolStripMenuItem.Text = "编码族删除(&W)";
            this.WToolStripMenuItem.Click += new EventHandler(this.BMZSCToolStripMenuItem_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(0x99, 6);
            this.CodeToolStripMenuItem.Checked = true;
            this.CodeToolStripMenuItem.CheckOnClick = true;
            this.CodeToolStripMenuItem.CheckState = CheckState.Checked;
            this.CodeToolStripMenuItem.Image = Class131.显示编码;
            this.CodeToolStripMenuItem.Name = "CodeToolStripMenuItem";
            this.CodeToolStripMenuItem.Size = new Size(0x9c, 0x16);
            this.CodeToolStripMenuItem.Text = "显示编码";
            this.CodeToolStripMenuItem.CheckedChanged += new EventHandler(this.CodeToolStripMenuItem_CheckedChanged);
            this.NameToolStripMenuItem.CheckOnClick = true;
            this.NameToolStripMenuItem.Enabled = false;
            this.NameToolStripMenuItem.Image = Class131.显示名称;
            this.NameToolStripMenuItem.Name = "NameToolStripMenuItem";
            this.NameToolStripMenuItem.Size = new Size(0x9c, 0x16);
            this.NameToolStripMenuItem.Text = "显示名称";
            this.NameToolStripMenuItem.CheckedChanged += new EventHandler(this.NameToolStripMenuItem_CheckedChanged);
            this.toolStripLabel1.AutoSize = false;
            this.toolStripLabel1.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.toolStripLabel1.ForeColor = Color.White;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(0x4a, 0x24);
            this.toolStripLabel1.Text = "  编码族管理";
            this.toolStripLabel1.TextImageRelation = TextImageRelation.Overlay;
            this.contextMenuStrip1.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.AddFenLeiToolStripMenuItem, this.ModToolStripMenuItem, this.AddSubNodeToolStripMenuItem, this.toolStripSeparator2, this.BMZZWToolStripMenuItem, this.BMZJWToolStripMenuItem, this.BMZSCToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x89, 0x8e);
            this.AddFenLeiToolStripMenuItem.Image = Class131.增加分类;
            this.AddFenLeiToolStripMenuItem.Name = "AddFenLeiToolStripMenuItem";
            this.AddFenLeiToolStripMenuItem.Size = new Size(0x88, 0x16);
            this.AddFenLeiToolStripMenuItem.Text = "增加分类";
            this.AddFenLeiToolStripMenuItem.Click += new EventHandler(this.AddFenLeiToolStripMenuItem_Click);
            this.ModToolStripMenuItem.Image = Class131.修改分类;
            this.ModToolStripMenuItem.Name = "ModToolStripMenuItem";
            this.ModToolStripMenuItem.Size = new Size(0x88, 0x16);
            this.ModToolStripMenuItem.Text = "修改分类";
            this.ModToolStripMenuItem.Click += new EventHandler(this.ModToolStripMenuItem_Click);
            this.AddSubNodeToolStripMenuItem.Image = Class131.smethod_48();
            this.AddSubNodeToolStripMenuItem.Name = "AddSubNodeToolStripMenuItem";
            this.AddSubNodeToolStripMenuItem.Size = new Size(0x88, 0x16);
            this.AddSubNodeToolStripMenuItem.Text = "增加子节点";
            this.AddSubNodeToolStripMenuItem.Click += new EventHandler(this.AddSubNodeToolStripMenuItem_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(0x85, 6);
            this.BMZZWToolStripMenuItem.Image = Class131.编码族增位;
            this.BMZZWToolStripMenuItem.Name = "BMZZWToolStripMenuItem";
            this.BMZZWToolStripMenuItem.Size = new Size(0x88, 0x16);
            this.BMZZWToolStripMenuItem.Text = "编码族增位";
            this.BMZZWToolStripMenuItem.Click += new EventHandler(this.BMZZWToolStripMenuItem_Click);
            this.BMZJWToolStripMenuItem.Image = Class131.编码族减位;
            this.BMZJWToolStripMenuItem.Name = "BMZJWToolStripMenuItem";
            this.BMZJWToolStripMenuItem.Size = new Size(0x88, 0x16);
            this.BMZJWToolStripMenuItem.Text = "编码族减位";
            this.BMZJWToolStripMenuItem.Click += new EventHandler(this.BMZJWToolStripMenuItem_Click);
            this.BMZSCToolStripMenuItem.Image = Class131.编码族删除;
            this.BMZSCToolStripMenuItem.Name = "BMZSCToolStripMenuItem";
            this.BMZSCToolStripMenuItem.Size = new Size(0x88, 0x16);
            this.BMZSCToolStripMenuItem.Text = "编码族删除";
            this.BMZSCToolStripMenuItem.Click += new EventHandler(this.BMZSCToolStripMenuItem_Click);
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.pictureBox1.BackColor = Color.White;
            this.pictureBox1.Image = (Image) manager.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(3, 0xde);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(20, 0x13);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.labelUI.AutoSize = true;
            this.labelUI.Location = new Point(0, 0);
            this.labelUI.Name = "labelUI";
            this.labelUI.Size = new Size(0x47, 12);
            this.labelUI.TabIndex = 4;
            this.labelUI.Text = "正在展开...";
            this.labelUI.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.tableLayoutPanel1);
            base.Controls.Add(this.labelUI);
            base.Name = "TreeViewBM";
            base.Size = new Size(0x7f, 0xf4);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method()
        {
            this.toolStrip1.BackColor = SystemColor.GRID_TITLE_BACKCOLOR;
            this.ForeColor = Color.White;
            foreach (ToolStripItem item in this.toolStrip1.Items)
            {
                item.Height = SystemColor.TOOLSCRIPT_CONTROL_HEIGHT;
                item.Font = SystemColor.TOOLSCRIPT_BOLD_FONT;
            }
        }

        private void method_1(List<TreeNodeTemp> ListNodes, TreeNode treeNode_1)
        {
            foreach (TreeNodeTemp temp in ListNodes)
            {
                this.listNode = this.getListNodes(temp.BM);
                TreeNode node = new TreeNode();
                if (this.bool_0)
                {
                    node.Text = temp.BM;
                    node.ToolTipText = temp.Name;
                }
                else
                {
                    node.Text = temp.Name;
                    node.ToolTipText = temp.BM;
                }
                node.Name = temp.BM;
                node.ImageIndex = 2;
                node.SelectedImageIndex = 3;
                treeNode_1.Nodes.Add(node);
                if ((this.listNode.Count > 0) && (this.listNode.Count > 0))
                {
                    node.Nodes.Add("T");
                }
            }
        }

        private void method_2(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode.Text.Length > 0)
            {
                this.treeView1.SelectedNode.Name.Trim();
                this.onClickDelete(sender, e);
            }
        }

        private void method_3(TreeNode treeNode_1)
        {
            foreach (TreeNode node in treeNode_1.Nodes)
            {
                if (node.ImageIndex == 2)
                {
                    string text = node.Text;
                    node.Text = node.ToolTipText;
                    node.ToolTipText = text;
                    this.method_3(node);
                }
            }
        }

        private void method_4(TreeViewEventArgs treeViewEventArgs_0)
        {
            object sender = 0;
            if (this.onTreeNodeClick(sender, treeViewEventArgs_0))
            {
                this.BMZZWToolStripMenuItem.Enabled = true;
                this.BMZJWToolStripMenuItem.Enabled = true;
            }
            else
            {
                this.BMZZWToolStripMenuItem.Enabled = false;
                this.BMZJWToolStripMenuItem.Enabled = false;
            }
        }

        private void method_5(TreeViewEventArgs treeViewEventArgs_0)
        {
            object sender = 0;
            if (this.onTreeNodeClick(sender, treeViewEventArgs_0))
            {
                this.UToolStripMenuItem.Enabled = true;
                this.VToolStripMenuItem.Enabled = true;
            }
            else
            {
                this.UToolStripMenuItem.Enabled = false;
                this.VToolStripMenuItem.Enabled = false;
            }
        }

        private TreeNode method_6(TreeNode treeNode_1, string string_4)
        {
            if (treeNode_1 == null)
            {
                return null;
            }
            if (treeNode_1.Text == string_4)
            {
                return treeNode_1;
            }
            if ((treeNode_1.FirstNode != null) && (treeNode_1.FirstNode.Text == "T"))
            {
                treeNode_1.Nodes.Clear();
                this.listNode = this.getListNodes(treeNode_1.Name);
                this.method_1(this.listNode, treeNode_1);
                treeNode_1.Collapse(true);
            }
            TreeNode node = null;
            IEnumerator enumerator = treeNode_1.Nodes.GetEnumerator();
            {
                while (enumerator.MoveNext())
                {
                    TreeNode current = (TreeNode) enumerator.Current;
                    node = this.method_6(current, string_4);
                    if (node != null)
                    {
                        goto Label_009D;
                    }
                }
                return node;
            Label_009D:
                treeNode_1.Expand();
            }
            return node;
        }

        private void method_7(TreeNode treeNode_1, int int_0)
        {
            foreach (TreeNode node in treeNode_1.Nodes)
            {
                if (node.Text != "T")
                {
                    this.method_7(node, int_0);
                    if (node.Text == node.Name)
                    {
                        node.Name = node.Name.Insert(int_0, "0");
                        node.Text = node.Name;
                    }
                    else
                    {
                        node.Name = node.Name.Insert(int_0, "0");
                        node.ToolTipText = node.Name;
                    }
                }
            }
        }

        private void method_8(TreeNode treeNode_1, int int_0)
        {
            foreach (TreeNode node in treeNode_1.Nodes)
            {
                if (node.Text != "T")
                {
                    this.method_8(node, int_0);
                    if (node.Text == node.Name)
                    {
                        node.Name = node.Name.Remove(int_0, 1);
                        node.Text = node.Name;
                    }
                    else
                    {
                        node.Name = node.Name.Remove(int_0, 1);
                        node.ToolTipText = node.Name;
                    }
                }
            }
        }

        private void ModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeSelectEventArgs args = new TreeSelectEventArgs(this.treeView1.SelectedNode.Name.Trim());
            this.onClickModify(sender, args);
        }

        private void NameToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.NameToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.CodeToolStripMenuItem.Checked = false;
                this.NameToolStripMenuItem.Enabled = false;
                this.CodeToolStripMenuItem.Enabled = true;
                this.method_3(this.treeView1.Nodes[0]);
                this.bool_0 = false;
                PropertyUtil.SetValue(this.RootNodeString.Replace('/', '-') + "编码-名称", this.bool_0.ToString());
            }
        }

        public void SelectNodeByText(string string_4)
        {
            TreeNode node = this.treeView1.Nodes[0];
            TreeNode node2 = this.method_6(node, string_4);
            if (node2 != null)
            {
                this.treeView1.Focus();
                this.treeView1.SelectedNode = node2;
            }
        }

        public void SubTreeLoad()
        {
            this.treeView1.SelectedNode.Nodes.Clear();
            this.listNode = this.getListNodes(this.treeView1.SelectedNode.Name);
            this.method_1(this.listNode, this.treeView1.SelectedNode);
            this.treeView1.SelectedNode.Expand();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            this.treeView1.Focus();
            if (!this.bool_1)
            {
                TreeViewEventArgs args = new TreeViewEventArgs(this.treeView1.SelectedNode);
                this.method_5(args);
            }
            if (((this.treeView1.SelectedNode != null) && (this.treeView1.SelectedNode.Name == "")) || this.bool_1)
            {
                this.ModifyFLToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.ModifyFLToolStripMenuItem.Enabled = true;
            }
        }

        public void TreeLoad()
        {
            this.treeView1.Nodes.Clear();
            TreeNode node = new TreeNode(this.string_0) {
                Text = this.string_0,
                Name = "",
                ToolTipText = this.string_0,
                ImageIndex = 2,
                SelectedImageIndex = 3
            };
            this.treeView1.Nodes.Add(node);
            this.string_1 = this.string_0;
            this.treeNode_0 = node;
            string str = PropertyUtil.GetValue(this.string_0.Replace('/', '-') + "编码-名称");
            if ("" != str)
            {
                bool.TryParse(str, out this.bool_0);
            }
            if (this.bool_0)
            {
                this.CodeToolStripMenuItem.Checked = true;
                this.CodeToolStripMenuItem.Enabled = false;
                this.NameToolStripMenuItem.Checked = false;
                this.NameToolStripMenuItem.Enabled = true;
            }
            else
            {
                this.NameToolStripMenuItem.Checked = true;
                this.NameToolStripMenuItem.Enabled = false;
                this.CodeToolStripMenuItem.Checked = false;
                this.CodeToolStripMenuItem.Enabled = true;
            }
            this.listNode = this.getListNodes(this.treeNode_0.Name);
            this.method_1(this.listNode, this.treeNode_0);
            this.treeNode_0.Text = this.string_1;
            this.treeNode_0.Expand();
            if (!string.IsNullOrEmpty(this.string_3))
            {
                this.AddSubNodeToolStripMenuItem.Text = this.string_3;
            }
            if (this.bool_1)
            {
                this.treeView1.NodeMouseClick -= new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
                this.UToolStripMenuItem.Enabled = false;
                this.VToolStripMenuItem.Enabled = false;
                this.WToolStripMenuItem.Enabled = false;
                this.AddFLToolStripMenuItem.Enabled = false;
                this.ModifyFLToolStripMenuItem.Enabled = false;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.method_4(e);
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.FirstNode.Text == "T")
            {
                while (this.backgroundWorker1.IsBusy)
                {
                    Application.DoEvents();
                }
                this.string_1 = e.Node.Text;
                e.Node.Text = this.string_1 + "(正在展开...)";
                e.Node.Nodes.Clear();
                this.treeNode_0 = e.Node;
                this.backgroundWorker1.RunWorkerAsync();
                Rectangle bounds = e.Node.Bounds;
                this.pictureBox1.Location = new Point(bounds.Location.X - 15, bounds.Location.Y + 30);
                this.pictureBox1.Visible = true;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.treeView1.SelectedNode = e.Node;
                this.contextMenuStrip1.Show(this.treeView1, e.Location, ToolStripDropDownDirection.AboveRight);
                if (e.Node.Name == "")
                {
                    this.ModToolStripMenuItem.Enabled = false;
                }
                else
                {
                    this.ModToolStripMenuItem.Enabled = true;
                }
            }
        }

        public string ChildText
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        [Description("根节点text"), DefaultValue("根节点"), Category("Appearance")]
        public string RootNodeString
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public TreeNode SelectedNode
        {
            get
            {
                return this.treeView1.SelectedNode;
            }
            set
            {
                this.treeView1.SelectedNode = value;
            }
        }

        public TreeNode TopNode
        {
            get
            {
                return this.treeView1.TopNode;
            }
        }

        public delegate List<TreeNodeTemp> GetListNodes(string ParentBM);

        public delegate void OnClickAdd(object sender, TreeSelectEventArgs e);

        public delegate void OnClickAddFenLei(object sender, TreeSelectEventArgs e);

        public delegate void OnClickDelete(object sender, EventArgs e);

        public delegate bool OnClickJianWei(object sender, EventArgs e);

        public delegate void OnClickModify(object sender, TreeSelectEventArgs e);

        public delegate bool OnClickZengWei(object sender, EventArgs e);

        public delegate bool OnTreeNodeClick(object sender, TreeViewEventArgs e);
    }
}

