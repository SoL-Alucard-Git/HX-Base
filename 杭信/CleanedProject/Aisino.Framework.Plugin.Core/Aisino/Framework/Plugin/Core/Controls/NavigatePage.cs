namespace Aisino.Framework.Plugin.Core.Controls
{
    using ns13;
    using ns8;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NavigatePage : UserControl
    {
        private IContainer icontainer_0;
        private int int_0;
        private int int_1;
        private Control3 navigateMenu1;
        private NavigateMenuNode navigateMenuNode_0;
        private Control1 navigateMenuTree1;

        public NavigatePage()
        {
            
            this.int_0 = 0x20;
            this.InitializeComponent();
            this.int_1 = base.Width;
            this.navigateMenuNode_0 = new NavigateMenuNode();
            this.navigateMenu1.Event_0 += new Delegate34(this.method_2);
            this.navigateMenuTree1.Event_0 += new Delegate37(this.method_1);
            this.navigateMenuTree1.Event_1 += new Delegate34(this.method_0);
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
            this.navigateMenu1 = new Control3();
            this.navigateMenuTree1 = new Control1();
            base.SuspendLayout();
            this.navigateMenu1.Dock = DockStyle.Bottom;
            this.navigateMenu1.Location = new Point(0, 0xe7);
            this.navigateMenu1.method_1(6);
            this.navigateMenu1.Name = "navigateMenu1";
            this.navigateMenu1.method_3(null);
            this.navigateMenu1.Size = new Size(0xe1, 0xea);
            this.navigateMenu1.TabIndex = 0;
            this.navigateMenu1.Text = "navigateMenu1";
            this.navigateMenuTree1.Dock = DockStyle.Fill;
            this.navigateMenuTree1.Location = new Point(0, 0);
            this.navigateMenuTree1.Name = "navigateMenuTree1";
            this.navigateMenuTree1.method_2(null);
            this.navigateMenuTree1.Size = new Size(0xe1, 0xe7);
            this.navigateMenuTree1.TabIndex = 1;
            this.navigateMenuTree1.Text = "navigateMenuTree1";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.navigateMenuTree1);
            base.Controls.Add(this.navigateMenu1);
            base.Name = "NavigatePage";
            base.Size = new Size(0xe1, 0x1d1);
            base.ResumeLayout(false);
        }

        private void method_0(object sender, EventArgs0 e)
        {
            if (e.method_0().NodeCommand != null)
            {
                e.method_0().NodeCommand.onClick();
            }
        }

        private void method_1(object object_0, Class122 class122_0)
        {
            if (class122_0.method_0())
            {
                base.Width = this.int_0;
            }
            else
            {
                base.Width = this.int_1;
            }
        }

        private void method_2(object sender, EventArgs0 e)
        {
            if (((e.method_0() != null) && (e.method_0().Node != null)) && (e.method_0().Node.Count > 0))
            {
                this.navigateMenuTree1.method_2(e.method_0());
            }
        }

        public int MaxMenuCount
        {
            get
            {
                return this.navigateMenu1.method_0();
            }
            set
            {
                this.navigateMenu1.method_1(value);
            }
        }

        public NavigateMenuNode Nodes
        {
            get
            {
                return this.navigateMenuNode_0;
            }
            set
            {
                if (this.navigateMenuNode_0 != value)
                {
                    this.navigateMenuNode_0 = value;
                    this.navigateMenu1.method_3(value);
                    if ((value != null) && (value.Node != null))
                    {
                        value.Node[0].IsExpand = true;
                        this.navigateMenuTree1.method_2(value.Node[0]);
                    }
                    else
                    {
                        this.navigateMenuTree1.method_2(null);
                    }
                }
            }
        }
    }
}

