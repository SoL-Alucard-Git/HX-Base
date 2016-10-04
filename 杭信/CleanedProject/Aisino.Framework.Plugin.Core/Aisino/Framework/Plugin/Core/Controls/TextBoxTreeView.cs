namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class TextBoxTreeView : AisinoTXT
    {
        private PopupHost popupHost_0;
        private string string_0;
        private TreeViewAddNeed treeViewAddNeed_0;

        public event GetListNodes getListNodes;

        public event SelectChanged selectChanged;

        public TextBoxTreeView()
        {
            
            this.BackColor = Color.FromKnownColor(KnownColor.Window);
            this.treeViewAddNeed_0 = new TreeViewAddNeed();
            this.treeViewAddNeed_0.Size = new Size(180, 220);
            this.treeViewAddNeed_0.onTreeNodeDoubleClick += new TreeViewAddNeed.OnTreeNodeDoubleClick(this.method_2);
            this.treeViewAddNeed_0.getListNodes += new TreeViewAddNeed.GetListNodes(this.method_1);
            this.popupHost_0 = new PopupHost(this.treeViewAddNeed_0);
            this.Text = "下拉树";
            base.ReadOnly = true;
            this.Cursor = Cursors.Arrow;
        }

        private Rectangle method()
        {
            return new Rectangle(2, 2, base.Height - 4, base.Height - 4);
        }

        private List<TreeNodeTemp> method_1(string string_1)
        {
            if (this.getListNodes != null)
            {
                return this.getListNodes(string_1);
            }
            return null;
        }

        private void method_2(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.string_0 = e.Node.Name;
            this.Text = e.Node.Name;
            base.Invalidate();
            this.popupHost_0.Close();
            this.selectChanged(sender, e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.popupHost_0.Show(this, 0, base.Height);
            }
            base.OnMouseClick(e);
        }

        public void TreeLoad()
        {
            this.treeViewAddNeed_0.TreeLoad();
        }

        public string RootNodeString
        {
            get
            {
                return this.treeViewAddNeed_0.RootNodeString;
            }
            set
            {
                this.treeViewAddNeed_0.RootNodeString = value;
            }
        }

        public string SelectBM
        {
            get
            {
                return this.string_0;
            }
            set
            {
                if (value != null)
                {
                    this.string_0 = value;
                    this.Text = this.string_0;
                    this.treeViewAddNeed_0.SelectBM = this.string_0;
                    base.Invalidate();
                }
            }
        }

        public delegate List<TreeNodeTemp> GetListNodes(string ParentBM);

        public delegate void SelectChanged(object sender, TreeNodeMouseClickEventArgs e);
    }
}

