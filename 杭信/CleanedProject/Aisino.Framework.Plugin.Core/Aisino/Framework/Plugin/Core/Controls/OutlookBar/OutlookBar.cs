namespace Aisino.Framework.Plugin.Core.Controls.OutlookBar
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class OutlookBar : AisinoPNL
    {
        private IContainer icontainer_0;
        private OutlookBarNodeCollection outlookBarNodeCollection_0;

        public OutlookBar()
        {
            
            this.method_0();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.ContainerControl, true);
            this.BackColor = Color.Transparent;
            this.AutoScroll = true;
            this.outlookBarNodeCollection_0 = new OutlookBarNodeCollection();
            this.outlookBarNodeCollection_0.Changed += new EventHandler(this.outlookBarNodeCollection_0_Changed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void method_0()
        {
            this.icontainer_0 = new Container();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void outlookBarNodeCollection_0_Changed(object sender, EventArgs e)
        {
            base.Controls.Clear();
            foreach (OutlookBarNode node in this.outlookBarNodeCollection_0)
            {
                node.Dock = DockStyle.Top;
                base.Controls.Add(node);
            }
            base.Invalidate();
        }

        public OutlookBarNodeCollection Nodes
        {
            get
            {
                return this.outlookBarNodeCollection_0;
            }
        }
    }
}

