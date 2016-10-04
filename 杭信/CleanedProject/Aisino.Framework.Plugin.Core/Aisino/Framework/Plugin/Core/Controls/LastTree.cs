namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    public class LastTree : AisinoTVW
    {
        private IContainer icontainer_0;

        public LastTree()
        {
            
            base.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            this.method_0();
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

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                e.DrawDefault = true;
            }
            else if (VisualStyleRenderer.IsSupported)
            {
                SizeF ef = e.Graphics.MeasureString(e.Node.Text, this.Font);
                int x = (((e.Bounds.X + 5) + (e.Node.Level * 0x13)) + 0x12) + 0x10;
                int y = e.Bounds.Y;
                Rectangle bounds = new Rectangle(x, y, (int) ef.Width, (int) ef.Height);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, this.Font, bounds, this.ForeColor);
                Pen pen = new Pen(SystemColors.GrayText) {
                    DashStyle = DashStyle.Dot
                };
                int num2 = e.Bounds.Height / 2;
                x -= 0x10;
                e.Graphics.DrawLine(pen, new Point(x, y + num2), new Point(x + 0x10, y + num2));
                if ((e.Node.Nodes.Count > 0) && e.Node.IsExpanded)
                {
                    e.Graphics.DrawLine(pen, new Point(x + 8, y + num2), new Point(x + 8, y + e.Bounds.Height));
                }
                x -= 8;
                e.Graphics.DrawLine(pen, new Point(x, y + num2), new Point(x + 8, y + num2));
                x -= 10;
                e.Graphics.DrawLine(pen, new Point(x + 5, y + num2), new Point(x + 10, y + num2));
                int num3 = y + e.Bounds.Height;
                if (e.Node.NextNode == null)
                {
                    num3 = y + num2;
                }
                e.Graphics.DrawLine(pen, new Point(x + 7, y), new Point(x + 7, num3));
                VisualStyleElement opened = null;
                if (e.Node.Nodes.Count > 0)
                {
                    if (e.Node.IsExpanded)
                    {
                        opened = VisualStyleElement.TreeView.Glyph.Opened;
                    }
                    else
                    {
                        opened = VisualStyleElement.TreeView.Glyph.Closed;
                    }
                }
                Rectangle rectangle8 = new Rectangle(x, y, 0x10, 0x10);
                VisualStyleRenderer renderer = new VisualStyleRenderer(opened);
                if (e.Node.Nodes.Count > 0)
                {
                    renderer.DrawBackground(e.Graphics, rectangle8);
                }
                TreeNode parent = e.Node;
                while (parent.Parent != null)
                {
                    parent = parent.Parent;
                    x -= 0x13;
                    if (parent.IsExpanded && (parent.NextNode != null))
                    {
                        e.Graphics.DrawLine(pen, new Point(x + 7, y), new Point(x + 7, y + e.Bounds.Height));
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
    }
}

