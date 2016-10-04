namespace Aisino.Framework.Plugin.Core.Controls.OutlookBar
{
    using Aisino.Framework.Plugin.Core.Tree;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class OutlookItem : Control
    {
        private bool bool_0;
        private bool bool_1;
        private Color color_0;
        private Color color_1;
        private Color color_2;
        private IContainer icontainer_0;
        private int int_0;
        private Aisino.Framework.Plugin.Core.Controls.OutlookBar.MenuDirection menuDirection_0;
        private Aisino.Framework.Plugin.Core.Controls.OutlookBar.MenuDirection menuDirection_1;
        private string string_0;
        private TreeNodeCommand treeNodeCommand_0;

        public OutlookItem()
        {
            
            this.color_0 = Color.Transparent;
            this.color_1 = Color.FromArgb(0xe7, 0xe7, 0xe7);
            this.color_2 = Color.FromArgb(0xd4, 0xd4, 0xd4);
            this.menuDirection_0 = Aisino.Framework.Plugin.Core.Controls.OutlookBar.MenuDirection.down;
            this.menuDirection_1 = Aisino.Framework.Plugin.Core.Controls.OutlookBar.MenuDirection.down;
            this.string_0 = "";
            this.method_0();
            base.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;
            base.MouseEnter += new EventHandler(this.OutlookItem_MouseEnter);
            base.MouseLeave += new EventHandler(this.OutlookItem_MouseLeave);
            base.Click += new EventHandler(this.OutlookItem_Click);
        }

        public OutlookItem(string string_1) : this()
        {
            
            this.Text = string_1;
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

        private void method_1(Graphics graphics_0)
        {
            Bitmap image = null;
            int x = 0;
            int y = 0;
            if (this.menuDirection_1 == Aisino.Framework.Plugin.Core.Controls.OutlookBar.MenuDirection.right)
            {
                x = 10;
                y = (base.Height / 2) - 2;
                image = Class131.smethod_6();
            }
            else if (this.menuDirection_1 == Aisino.Framework.Plugin.Core.Controls.OutlookBar.MenuDirection.down)
            {
                x = 10;
                y = (base.Height / 2) - 4;
                image = Class131.smethod_40();
            }
            x += this.int_0;
            if (image != null)
            {
                graphics_0.DrawImage(image, new Point(x, y));
            }
        }

        private void method_2(Graphics graphics_0)
        {
            Bitmap image = null;
            int x = 0;
            int y = 0;
            if (this.menuDirection_0 == Aisino.Framework.Plugin.Core.Controls.OutlookBar.MenuDirection.down)
            {
                x = 10;
                y = (base.Height / 2) - 6;
                image = Class131.smethod_17();
            }
            else if (this.menuDirection_0 == Aisino.Framework.Plugin.Core.Controls.OutlookBar.MenuDirection.right)
            {
                x = 10;
                y = (base.Height / 2) - 5;
                image = Class131.smethod_14();
            }
            x += this.int_0;
            if (image != null)
            {
                graphics_0.DrawImage(image, new Point(x, y));
            }
        }

        private void method_3(Graphics graphics_0)
        {
            int x = 0x19;
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
            x = 0x19 + this.int_0;
            int width = ((base.Width - 20) - 20) - this.int_0;
            Rectangle layoutRectangle = new Rectangle(x, 0, width, base.Height);
            graphics_0.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), layoutRectangle, format);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            if (this.bool_0)
            {
                this.method_2(e.Graphics);
            }
            this.method_3(e.Graphics);
            if (this.bool_1)
            {
                this.method_1(e.Graphics);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            float x = 0f;
            float y = 0f;
            float width = Convert.ToSingle(base.Width);
            float height = Convert.ToSingle(base.Height);
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), new RectangleF(x, y, width, height));
        }

        private void OutlookItem_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs) e;
            if ((args.Button != MouseButtons.Right) && ((this.string_0 != null) && (this.string_0 != "")))
            {
                this.treeNodeCommand_0.onClick();
            }
        }

        private void OutlookItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = this.color_1;
        }

        private void OutlookItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = this.color_0;
        }

        public Aisino.Framework.Plugin.Core.Controls.OutlookBar.MenuDirection IconDirection
        {
            get
            {
                return this.menuDirection_0;
            }
            set
            {
                this.menuDirection_0 = value;
                base.Invalidate();
            }
        }

        public int IconIdent
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
                base.Invalidate();
            }
        }

        public Aisino.Framework.Plugin.Core.Controls.OutlookBar.MenuDirection MenuDirection
        {
            get
            {
                return this.menuDirection_1;
            }
            set
            {
                this.menuDirection_1 = value;
                base.Invalidate();
            }
        }

        public string MenuID
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

        public TreeNodeCommand NodeCommand
        {
            get
            {
                return this.treeNodeCommand_0;
            }
            set
            {
                this.treeNodeCommand_0 = value;
            }
        }

        public bool ShowIcon
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
                base.Invalidate();
            }
        }

        public bool ShowImage
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
                base.Invalidate();
            }
        }
    }
}

