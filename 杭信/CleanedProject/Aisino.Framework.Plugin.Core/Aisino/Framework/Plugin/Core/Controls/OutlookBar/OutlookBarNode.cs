namespace Aisino.Framework.Plugin.Core.Controls.OutlookBar
{
    using Aisino.Framework.Plugin.Core.Tree;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class OutlookBarNode : UserControl
    {
        private OutlookBounds Body;
        private bool bool_0;
        private Color color_0;
        private Font font_0;
        private Font font_1;
        private IContainer icontainer_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private OutlookBarNodeCollection outlookBarNodeCollection_0;
        private OutlookItemCollection outlookItemCollection_0;
        private OutlookItem Title;

        public event TitleCilckEventHander TitleClick;

        public OutlookBarNode()
        {
            
            this.int_0 = 40;
            this.int_1 = 30;
            this.font_0 = new Font("微软雅黑", 14f);
            this.font_1 = new Font("微软雅黑", 10f);
            this.color_0 = Color.Black;
            this.outlookItemCollection_0 = new OutlookItemCollection();
            this.outlookBarNodeCollection_0 = new OutlookBarNodeCollection();
            this.InitializeComponent();
            this.Title.SizeChanged += new EventHandler(this.Title_SizeChanged);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ContainerControl, true);
            this.BackColor = Color.Transparent;
            this.Title.Name = base.Name + "title";
            this.Title.Font = this.font_0;
            this.Title.Height = this.int_0;
            this.Title.Text = this.Text;
            this.Title.ShowIcon = true;
            this.Title.IconIdent = this.int_3;
            this.Title.IconDirection = MenuDirection.down;
            this.Title.Dock = DockStyle.Top;
            this.Title.Click += new EventHandler(this.Title_Click);
            this.Body.Dock = DockStyle.Fill;
            this.Body.Visible = false;
            base.Height = this.int_0;
            this.Dock = DockStyle.Top;
        }

        public OutlookBarNode(string string_0, bool bool_1, bool bool_2, int int_4) : this()
        {
            
            this.Title.Text = string_0;
            this.Title.ShowIcon = bool_1;
            this.Title.ShowImage = bool_2;
            this.Title.IconIdent = int_4;
            this.Text = string_0;
            this.int_3 = int_4;
        }

        public OutlookBarNode(string string_0, bool bool_1, bool bool_2, int int_4, Font font_2, int int_5) : this(string_0, bool_1, bool_2, int_4)
        {
            
            this.Title.ForeColor = this.color_0;
            if (font_2 != null)
            {
                this.Title.Font = font_2;
                this.font_0 = font_2;
            }
            if (int_5 > 0)
            {
                this.Title.Height = int_5;
                this.int_0 = int_5;
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
            this.Title = new OutlookItem();
            this.Body = new OutlookBounds();
            base.SuspendLayout();
            this.Title.BackColor = Color.Transparent;
            this.Title.Dock = DockStyle.Top;
            this.Title.Font = new Font("微软雅黑", 18f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.Title.IconDirection = MenuDirection.down;
            this.Title.IconIdent = 0;
            this.Title.Location = new Point(0, 0);
            this.Title.MenuDirection = MenuDirection.down;
            this.Title.Name = "Title";
            this.Title.ShowIcon = false;
            this.Title.ShowImage = false;
            this.Title.Size = new Size(150, 0x23);
            this.Title.TabIndex = 0;
            this.Title.Text = "Title";
            this.Body.BackColor = Color.Transparent;
            this.Body.Dock = DockStyle.Fill;
            this.Body.Font = new Font("微软雅黑", 10f);
            this.Body.Location = new Point(0, 0x23);
            this.Body.Name = "Body";
            this.Body.Size = new Size(150, 0x73);
            this.Body.TabIndex = 1;
            this.Body.Text = "Body";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.Body);
            base.Controls.Add(this.Title);
            base.Name = "OutlookBarNode";
            base.ResumeLayout(false);
        }

        private void method_0(object object_0, MenuDirection menuDirection_0)
        {
            if (this.TitleClick != null)
            {
                this.TitleClick(object_0, menuDirection_0);
            }
        }

        private void method_1(object object_0, MenuDirection menuDirection_0)
        {
            base.Height = this.method_2();
        }

        private int method_2()
        {
            int num = 0;
            foreach (OutlookBarNode node in this.outlookBarNodeCollection_0)
            {
                num += node.Height;
            }
            num += this.int_0;
            return (num + (this.int_1 * this.outlookItemCollection_0.Count));
        }

        public void NodeAdd(OutlookItem outlookItem_0)
        {
            this.outlookItemCollection_0.Add(outlookItem_0);
            this.int_2 = this.int_0 + this.method_2();
            outlookItem_0.Height = this.int_1;
            outlookItem_0.Font = this.font_1;
            outlookItem_0.IconIdent = this.int_3;
            outlookItem_0.ShowIcon = false;
            outlookItem_0.ShowImage = false;
            outlookItem_0.Dock = DockStyle.Top;
            outlookItem_0.ForeColor = this.color_0;
            this.Body.Controls.Add(outlookItem_0);
            this.ResetControlHeight();
        }

        public void NodeAdd(string string_0, string string_1, string string_2, TreeNodeCommand treeNodeCommand_0)
        {
            OutlookItem item = new OutlookItem();
            if ((string_0 != null) && !(string_0 == ""))
            {
                item.Name = string_0;
            }
            else
            {
                item.Name = "NewNodeName" + ((this.outlookItemCollection_0.Count + 1)).ToString();
            }
            if ((string_1 != null) && !(string_1 == ""))
            {
                item.Text = string_1;
            }
            else
            {
                item.Text = item.Name;
            }
            if ((string_2 != null) && (string_2 != ""))
            {
                item.MenuID = string_2;
            }
            item.NodeCommand = treeNodeCommand_0;
            this.NodeAdd(item);
        }

        public void ResetControlHeight()
        {
            this.Title.Height = this.int_0;
            foreach (OutlookBarNode node in this.outlookBarNodeCollection_0)
            {
                node.ResetControlHeight();
                node.Invalidate();
            }
            foreach (OutlookItem item in this.outlookItemCollection_0)
            {
                item.Height = this.int_1;
            }
            base.Height = this.int_0;
            base.Invalidate();
        }

        public void SubNodeAdd(OutlookBarNode outlookBarNode_0)
        {
            this.int_2 = outlookBarNode_0.Height + this.method_2();
            outlookBarNode_0.TitleClick += new TitleCilckEventHander(this.method_1);
            this.outlookBarNodeCollection_0.Add(outlookBarNode_0);
            outlookBarNode_0.Dock = DockStyle.Top;
            this.Body.Controls.Add(outlookBarNode_0);
            this.Title.Height = this.int_0;
            base.Height = this.int_0;
            this.bool_0 = true;
            this.ResetControlHeight();
        }

        private void Title_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs) e;
            if (args.Button != MouseButtons.Right)
            {
                this.ResetControlHeight();
                if (this.Body.Visible)
                {
                    this.Body.Visible = false;
                    base.Height = this.int_0;
                    this.Title.IconDirection = MenuDirection.down;
                    this.Title.MenuDirection = MenuDirection.down;
                }
                else
                {
                    this.Body.Visible = true;
                    base.Height = this.method_2();
                    this.Title.IconDirection = MenuDirection.right;
                    this.Title.MenuDirection = MenuDirection.right;
                }
                this.method_0(sender, this.Title.IconDirection);
            }
        }

        private void Title_SizeChanged(object sender, EventArgs e)
        {
            int height = this.Title.Height;
        }

        public int ControlHeight
        {
            get
            {
                return this.int_2;
            }
        }

        public int IconIdent
        {
            get
            {
                return this.int_3;
            }
            set
            {
                this.int_3 = value;
                base.Invalidate();
            }
        }

        public OutlookItemCollection Nodes
        {
            get
            {
                return this.outlookItemCollection_0;
            }
        }

        public override string Text
        {
            get
            {
                return this.Title.Text;
            }
            set
            {
                this.Title.Text = value;
            }
        }
    }
}

