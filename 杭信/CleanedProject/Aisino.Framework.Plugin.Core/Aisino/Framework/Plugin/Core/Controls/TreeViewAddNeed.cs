namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [Description("树编码控件")]
    public class TreeViewAddNeed : UserControl
    {
        private BackgroundWorker backgroundWorker_0;
        private bool bool_0;
        private bool bool_1;
        private ContextMenuStrip contextMenuStrip1;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private List<TreeNodeTemp> list_0;
        private AisinoPIC pictureBox1;
        private ToolStripMenuItem ShowBMToolStripMenuItem;
        private ToolStripMenuItem ShowMCToolStripMenuItem;
        private string string_0;
        private string string_1;
        private string string_2;
        private TreeNode treeNode_0;
        private AisinoTVW treeView1;

        public event GetListNodes getListNodes;

        public event OnTreeNodeClick onTreeNodeClick;

        public event OnTreeNodeDoubleClick onTreeNodeDoubleClick;

        public TreeViewAddNeed()
        {
            
            this.string_0 = "根节点";
            this.list_0 = new List<TreeNodeTemp>();
            this.InitializeComponent();
            if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
            {
                this.treeView1.ShowNodeToolTips = false;
            }
            else
            {
                this.treeView1.ShowNodeToolTips = true;
            }
        }

        private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
        {
            this.list_0 = this.getListNodes(this.treeNode_0.Name);
        }

        private void backgroundWorker_0_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.method(this.list_0, this.treeNode_0);
            this.treeNode_0.Text = this.string_2;
            this.treeNode_0.Expand();
            this.pictureBox1.Visible = false;
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(TreeViewAddNeed));
            this.treeView1 = new AisinoTVW();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
            this.ShowBMToolStripMenuItem = new ToolStripMenuItem();
            this.ShowMCToolStripMenuItem = new ToolStripMenuItem();
            this.backgroundWorker_0 = new BackgroundWorker();
            this.pictureBox1 = new AisinoPIC();
            this.contextMenuStrip1.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList_0;
            this.treeView1.Location = new Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new Size(0x75, 0xf1);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.BeforeExpand += new TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseEnter += new EventHandler(this.treeView1_MouseEnter);
            this.treeView1.Leave += new EventHandler(this.treeView1_Leave);
            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.MouseLeave += new EventHandler(this.treeView1_MouseLeave);
            this.imageList_0.ImageStream = (ImageListStreamer) manager.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "small1.BMP");
            this.imageList_0.Images.SetKeyName(1, "small1-1.BMP");
            this.imageList_0.Images.SetKeyName(2, "folder-closed.gif");
            this.imageList_0.Images.SetKeyName(3, "folder.gif");
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.ShowBMToolStripMenuItem, this.ShowMCToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x77, 0x30);
            this.ShowBMToolStripMenuItem.CheckOnClick = true;
            this.ShowBMToolStripMenuItem.Name = "ShowBMToolStripMenuItem";
            this.ShowBMToolStripMenuItem.Size = new Size(0x76, 0x16);
            this.ShowBMToolStripMenuItem.Text = "显示编码";
            this.ShowBMToolStripMenuItem.CheckedChanged += new EventHandler(this.ShowBMToolStripMenuItem_CheckedChanged);
            this.ShowMCToolStripMenuItem.Checked = true;
            this.ShowMCToolStripMenuItem.CheckOnClick = true;
            this.ShowMCToolStripMenuItem.CheckState = CheckState.Checked;
            this.ShowMCToolStripMenuItem.Enabled = false;
            this.ShowMCToolStripMenuItem.Name = "ShowMCToolStripMenuItem";
            this.ShowMCToolStripMenuItem.Size = new Size(0x76, 0x16);
            this.ShowMCToolStripMenuItem.Text = "显示名称";
            this.ShowMCToolStripMenuItem.CheckedChanged += new EventHandler(this.ShowMCToolStripMenuItem_CheckedChanged);
            this.backgroundWorker_0.DoWork += new DoWorkEventHandler(this.backgroundWorker_0_DoWork);
            this.backgroundWorker_0.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_0_RunWorkerCompleted);
            this.pictureBox1.BackColor = Color.White;
            this.pictureBox1.Image = (Image) manager.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(0x30, 0x6f);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(20, 0x13);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.treeView1);
            base.Name = "TreeViewAddNeed";
            base.Size = new Size(0x75, 0xf1);
            this.contextMenuStrip1.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
        }

        private void method(List<TreeNodeTemp> ListNodes, TreeNode treeNode_1)
        {
            foreach (TreeNodeTemp temp in ListNodes)
            {
                this.list_0 = this.getListNodes(temp.BM);
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
                if (this.list_0.Count > 0)
                {
                    if (((this.string_1 != null) && this.string_1.StartsWith(temp.BM)) && (this.string_1 != temp.BM))
                    {
                        this.method(this.list_0, node);
                        node.Expand();
                    }
                    else
                    {
                        node.Nodes.Add("T");
                    }
                }
                if (this.string_1 == temp.BM)
                {
                    this.treeView1.SelectedNode = node;
                }
            }
        }

        private void method_1(TreeNode treeNode_1)
        {
            foreach (TreeNode node in treeNode_1.Nodes)
            {
                if (node.ImageIndex == 2)
                {
                    string text = node.Text;
                    node.Text = node.ToolTipText;
                    node.ToolTipText = text;
                    this.method_1(node);
                }
            }
        }

        private void ShowBMToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ShowBMToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.ShowMCToolStripMenuItem.Checked = false;
                this.ShowBMToolStripMenuItem.Enabled = false;
                this.ShowMCToolStripMenuItem.Enabled = true;
                this.method_1(this.treeView1.TopNode);
                this.bool_0 = !this.bool_0;
            }
        }

        private void ShowMCToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ShowMCToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.ShowBMToolStripMenuItem.Checked = false;
                this.ShowMCToolStripMenuItem.Enabled = false;
                this.ShowBMToolStripMenuItem.Enabled = true;
                this.method_1(this.treeView1.TopNode);
                this.bool_0 = !this.bool_0;
            }
        }

        public void TreeLoad()
        {
            this.treeView1.Nodes.Clear();
            TreeNode node = new TreeNode(this.string_0) {
                Text = this.string_0,
                Name = "",
                ToolTipText = this.string_0,
                ImageIndex = 0,
                SelectedImageIndex = 1
            };
            this.treeView1.Nodes.Add(node);
            this.string_2 = this.string_0;
            this.treeNode_0 = node;
            this.pictureBox1.Location = new Point(this.treeView1.Location.X + (this.treeView1.Size.Width / 2), this.treeView1.Location.Y + (this.treeView1.Size.Height / 2));
            this.pictureBox1.Visible = true;
            this.backgroundWorker_0.RunWorkerAsync();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.onTreeNodeClick != null)
            {
                this.onTreeNodeClick(this, e);
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.FirstNode.Text == "T")
            {
                while (this.backgroundWorker_0.IsBusy)
                {
                    Application.DoEvents();
                }
                this.treeNode_0 = e.Node;
                this.backgroundWorker_0.RunWorkerAsync();
                Rectangle bounds = e.Node.Bounds;
                this.pictureBox1.Location = new Point(bounds.Location.X - 0x10, bounds.Location.Y);
                this.pictureBox1.Visible = true;
                this.string_2 = e.Node.Text;
                e.Node.Text = this.string_2 + " (正在展开...)";
                e.Node.Nodes.Clear();
            }
        }

        private void treeView1_Leave(object sender, EventArgs e)
        {
            this.bool_1 = false;
            base.Visible = false;
        }

        private void treeView1_MouseEnter(object sender, EventArgs e)
        {
            this.bool_1 = true;
        }

        private void treeView1_MouseLeave(object sender, EventArgs e)
        {
            this.bool_1 = false;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.treeView1.SelectedNode = e.Node;
                this.contextMenuStrip1.Show(this.treeView1, e.Location, ToolStripDropDownDirection.AboveRight);
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.onTreeNodeDoubleClick(sender, e);
            this.bool_1 = false;
        }

        public List<TreeNodeTemp> listNode
        {
            get
            {
                return this.list_0;
            }
        }

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

        public string SelectBM
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public TreeNode SelectedNode
        {
            get
            {
                return this.treeView1.SelectedNode;
            }
        }

        public bool ShowTree
        {
            get
            {
                return this.bool_1;
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

        public delegate void OnTreeNodeClick(object sender, TreeViewEventArgs e);

        public delegate void OnTreeNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e);
    }
}

