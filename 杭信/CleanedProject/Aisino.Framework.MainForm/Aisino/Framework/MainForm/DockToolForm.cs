namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Tree;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DockToolForm : DockForm
    {
        internal bool bool_0;
        private IContainer icontainer_3;
        private ImageList imageList_0;
        private AisinoTVW TreeView1;

        public DockToolForm(object object_0)
        {
            
            this.InitializeComponent_1();
            TreeLoader.Load(this.TreeView1, object_0, "/Aisino/Tree", false);
            this.bool_0 = this.TreeView1.Nodes.Count > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_3 != null))
            {
                this.icontainer_3.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            this.icontainer_3 = new Container();
            this.TreeView1 = new AisinoTVW();
            this.imageList_0 = new ImageList(this.icontainer_3);
            base.SuspendLayout();
            this.TreeView1.BorderStyle = BorderStyle.FixedSingle;
            this.TreeView1.Dock = DockStyle.Fill;
            this.TreeView1.ImageIndex = 0;
            this.TreeView1.ImageList = this.imageList_0;
            this.TreeView1.Location = new Point(0, 0);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.SelectedImageIndex = 0;
            this.TreeView1.Size = new Size(0x124, 0x111);
            this.TreeView1.TabIndex = 0;
            this.TreeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
            this.imageList_0.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList_0.ImageSize = new Size(0x10, 0x10);
            this.imageList_0.TransparentColor = Color.Transparent;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0x111);
            base.Controls.Add(this.TreeView1);
            base.Name = "DockToolForm";
            this.Text = "菜单栏";
            base.ResumeLayout(false);
        }

        internal void method_3(object object_0)
        {
            this.TreeView1.Nodes.Clear();
            TreeLoader.Load(this.TreeView1, object_0, "/Aisino/Tree", false);
            this.bool_0 = this.TreeView1.Nodes.Count > 0;
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ((TreeNodeCommand) e.Node).onClick();
        }

        public bool HasChild
        {
            get
            {
                return this.bool_0;
            }
        }
    }
}

