namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    public class TriStateTreeView : AisinoTVW
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private bool bool_3;
        private ImageList imageList_0;

        public TriStateTreeView()
        {
            
            this.bool_2 = true;
            this.imageList_0 = new ImageList();
            CheckBoxState uncheckedNormal = CheckBoxState.UncheckedNormal;
            for (int i = 0; i <= 2; i++)
            {
                Bitmap image = new Bitmap(0x10, 0x10);
                Graphics g = Graphics.FromImage(image);
                switch (i)
                {
                    case 0:
                        uncheckedNormal = CheckBoxState.UncheckedNormal;
                        break;

                    case 1:
                        uncheckedNormal = CheckBoxState.CheckedNormal;
                        break;

                    case 2:
                        uncheckedNormal = CheckBoxState.MixedNormal;
                        break;
                }
                CheckBoxRenderer.DrawCheckBox(g, new Point(1, 0), uncheckedNormal);
                g.Save();
                this.imageList_0.Images.Add(image);
                this.bool_0 = true;
            }
        }

        private void method_0()
        {
            if (this.bool_2)
            {
                this.ForeColor = Color.Black;
            }
            else
            {
                this.ForeColor = Color.Gray;
            }
        }

        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            base.OnAfterCheck(e);
            if (!this.bool_3)
            {
                this.OnNodeMouseClick(new TreeNodeMouseClickEventArgs(e.Node, MouseButtons.None, 0, 0, 0));
            }
        }

        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            base.OnAfterExpand(e);
            foreach (TreeNode node in e.Node.Nodes)
            {
                if (node.StateImageIndex == -1)
                {
                    node.StateImageIndex = node.Checked ? 1 : 0;
                }
            }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            this.Refresh();
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);
            int num = (base.ImageList == null) ? 0 : 0x12;
            if (((e.X > (e.Node.Bounds.Left - num)) || (e.X < (e.Node.Bounds.Left - (num + 0x10)))) && (e.Button != MouseButtons.None))
            {
                return;
            }
            if ((e.Button != MouseButtons.None) && !this.bool_2)
            {
                return;
            }
            this.bool_3 = true;
            TreeNode item = e.Node;
            if (e.Button == MouseButtons.Left)
            {
                item.Checked = !item.Checked;
            }
            item.StateImageIndex = item.Checked ? 1 : item.StateImageIndex;
            Stack<TreeNode> stack = new Stack<TreeNode>(item.Nodes.Count);
            stack.Push(item);
        Label_00FC:
            item = stack.Pop();
            item.Checked = e.Node.Checked;
            for (int i = 0; i < item.Nodes.Count; i++)
            {
                stack.Push(item.Nodes[i]);
            }
            if (stack.Count <= 0)
            {
                bool flag = false;
                for (item = e.Node; item.Parent != null; item = item.Parent)
                {
                    foreach (TreeNode node2 in item.Parent.Nodes)
                    {
                        flag |= (node2.Checked != item.Checked) | (node2.StateImageIndex == 2);
                    }
                    int num2 = (int) Convert.ToUInt32(item.Checked);
                    item.Parent.Checked = flag || (num2 > 0);
                    if (flag)
                    {
                        item.Parent.StateImageIndex = this.CheckBoxesTriState ? 2 : 1;
                    }
                    else
                    {
                        item.Parent.StateImageIndex = num2;
                    }
                }
                this.bool_3 = false;
            }
            else
            {
                goto Label_00FC;
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            if (this.bool_1)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>(base.Nodes.Count);
                foreach (TreeNode node2 in base.Nodes)
                {
                    stack.Push(node2);
                }
                while (stack.Count > 0)
                {
                    TreeNode node = stack.Pop();
                    if (node.StateImageIndex == -1)
                    {
                        node.StateImageIndex = node.Checked ? 1 : 0;
                    }
                    for (int i = 0; i < node.Nodes.Count; i++)
                    {
                        stack.Push(node.Nodes[i]);
                    }
                }
                this.method_0();
            }
        }

        [Category("Appearance"), DefaultValue(false), Description("Sets tree view to display checkboxes or not.")]
        public bool CheckBoxes
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
                base.CheckBoxes = false;
                this.StateImageList = this.bool_1 ? this.imageList_0 : null;
            }
        }

        [Description("Sets tree view's checkboxes can enabled or not."), Browsable(true), DefaultValue(true)]
        public bool CheckBoxesEnabled
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
                this.method_0();
            }
        }

        [DefaultValue(true), Category("Appearance"), Description("Sets tree view to use tri-state checkboxes or not.")]
        public bool CheckBoxesTriState
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        [Browsable(false)]
        public ImageList StateImageList
        {
            get
            {
                return base.StateImageList;
            }
            set
            {
                base.StateImageList = value;
            }
        }
    }
}

