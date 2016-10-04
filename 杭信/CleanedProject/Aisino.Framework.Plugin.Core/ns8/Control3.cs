namespace ns8
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Threading;
    using System.Windows.Forms;

    internal class Control3 : Control
    {
        private bool bool_0;
        private bool bool_1;
        private Container container_0;
        private ContextMenuStrip contextMenuStrip_0;
        private Font font_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        private int int_5;
        private int int_6;
        private int int_7;
        private List<NavigateMenuNodeRect> list_0;
        private MenuColorTable menuColorTable_0;
        private NavigateMenuNode navigateMenuNode_0;
        private Point point_0;
        private Rectangle rectangle_0;
        private ToolStripMenuItem toolStripMenuItem_0;
        private ToolStripMenuItem toolStripMenuItem_1;
        private ToolTip toolTip_0;

        internal event Delegate34 Event_0;

        public Control3()
        {
            
            this.int_0 = 0x20;
            this.int_1 = 10;
            this.int_2 = 0x20;
            this.int_3 = 20;
            this.int_4 = 30;
            this.int_5 = 0xea;
            this.int_6 = 6;
            this.font_0 = new Font("宋体", 10f);
            this.rectangle_0 = new Rectangle(0, 0, 0, 0);
            this.toolTip_0 = new ToolTip();
            this.method_14();
            this.method_4();
            this.menuColorTable_0 = new MenuColorTable();
            this.contextMenuStrip_0 = new ContextMenuStrip(this.container_0);
            this.toolStripMenuItem_1 = new ToolStripMenuItem();
            this.toolStripMenuItem_1.Text = "显示更多菜单项";
            this.toolStripMenuItem_0 = new ToolStripMenuItem();
            this.toolStripMenuItem_0.Text = "显示更少菜单项";
            this.toolStripMenuItem_1.Click += new EventHandler(this.toolStripMenuItem_1_Click);
            this.toolStripMenuItem_0.Click += new EventHandler(this.toolStripMenuItem_0_Click);
            this.list_0 = new List<NavigateMenuNodeRect>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(disposing);
        }

        public int method_0()
        {
            return this.int_6;
        }

        public void method_1(int int_8)
        {
            if (this.int_6 != int_8)
            {
                this.int_6 = int_8;
                this.int_5 = ((this.int_6 + 1) * this.int_0) + this.int_1;
                this.method_10();
                base.Invalidate();
            }
        }

        private void method_10()
        {
            if (base.Height > this.int_5)
            {
                base.Height = this.int_5;
            }
            if (base.Height < (this.int_0 + this.int_1))
            {
                base.Height = this.int_0 + this.int_1;
            }
            else
            {
                int num = (base.Height - this.int_0) - this.int_1;
                int count = num / this.int_0;
                if (((this.navigateMenuNode_0 != null) && (this.navigateMenuNode_0.Node != null)) && ((this.navigateMenuNode_0.Node.Count > 0) && (count > this.navigateMenuNode_0.Node.Count)))
                {
                    count = this.navigateMenuNode_0.Node.Count;
                }
                base.Height = ((count * this.int_0) + this.int_0) + this.int_1;
            }
        }

        private void method_11(int int_8, int int_9)
        {
            Point pt = new Point(int_8, int_9);
            foreach (NavigateMenuNodeRect rect in this.list_0)
            {
                if (rect.Rect.Contains(pt))
                {
                    this.rectangle_0 = rect.Rect;
                    this.toolTip_0.Show(rect.Node.SimpleText, this, int_8, int_9, 0xbb8);
                }
            }
        }

        private void method_12(NavigateMenuNode navigateMenuNode_1)
        {
            if (((navigateMenuNode_1 != null) && (navigateMenuNode_1.Node != null)) && (navigateMenuNode_1.Node.Count > 0))
            {
                foreach (NavigateMenuNode node in navigateMenuNode_1.Node)
                {
                    node.IsExpand = false;
                    if ((node.Node != null) && (node.Node.Count > 0))
                    {
                        this.method_12(node);
                    }
                }
            }
        }

        private Bitmap method_13(Bitmap bitmap_0, int int_8, int int_9)
        {
            Bitmap bitmap;
            Bitmap bitmap2;
            try
            {
                bitmap = new Bitmap(bitmap_0);
                bitmap2 = new Bitmap(int_8, int_9);
                Graphics graphics = Graphics.FromImage(bitmap2);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(bitmap, new Rectangle(0, 0, int_8, int_9), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                graphics.Dispose();
                return bitmap2;
            }
            catch
            {
            }
            finally
            {
                bitmap = null;
                bitmap2 = null;
                GC.Collect();
            }
            return null;
        }

        private void method_14()
        {
            this.container_0 = new Container();
        }

        public NavigateMenuNode method_2()
        {
            return this.navigateMenuNode_0;
        }

        public void method_3(NavigateMenuNode navigateMenuNode_1)
        {
            if (this.navigateMenuNode_0 != navigateMenuNode_1)
            {
                this.navigateMenuNode_0 = navigateMenuNode_1;
                base.Invalidate();
            }
        }

        private void method_4()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.UpdateStyles();
        }

        private void method_5(object sender, EventArgs e)
        {
            if (this.navigateMenuNode_0 != null)
            {
                ToolStripMenuItem item = (ToolStripMenuItem) sender;
                IEnumerator enumerator = navigateMenuNode_0.Node.GetEnumerator();
                {
                    NavigateMenuNode current;
                    while (enumerator.MoveNext())
                    {
                        current = (NavigateMenuNode) enumerator.Current;
                        if (item.Name == current.Name)
                        {
                            goto Label_004C;
                        }
                    }
                    return;
                Label_004C:
                    this.vmethod_0(this, new EventArgs0(current));
                }
            }
        }

        private void method_6(PaintEventArgs paintEventArgs_0)
        {
            Rectangle rect = new Rectangle(0, 0, base.Width, this.int_1);
            LinearGradientBrush brush = new LinearGradientBrush(rect, this.menuColorTable_0.MenuSplitTop, this.menuColorTable_0.MenuSplitBottom, 90f);
            paintEventArgs_0.Graphics.FillRectangle(brush, rect);
            Rectangle rectangle2 = new Rectangle(0, 0, base.Bounds.Width - 1, this.int_1 - 1);
            paintEventArgs_0.Graphics.DrawRectangle(new Pen(new SolidBrush(this.menuColorTable_0.MenuVertualLine)), rectangle2);
            string text = "……";
            SizeF ef = paintEventArgs_0.Graphics.MeasureString(text, this.font_0);
            int num = (base.Width - ((int) ef.Width)) / 2;
            int num2 = (this.int_1 - ((int) ef.Height)) / 2;
            paintEventArgs_0.Graphics.DrawString(text, this.font_0, new SolidBrush(this.menuColorTable_0.MenuFont), (float) num, (float) num2);
        }

        private void method_7(PaintEventArgs paintEventArgs_0, int int_8, int int_9, bool bool_2, bool bool_3, NavigateMenuNode navigateMenuNode_1)
        {
            Color menuBackColor = this.menuColorTable_0.MenuBackColor;
            Rectangle rect = new Rectangle(int_8, int_9, base.Width, this.int_0);
            if (!this.bool_1)
            {
                if (bool_3)
                {
                    paintEventArgs_0.Graphics.DrawImage(Class131.smethod_30(), rect);
                }
                else if (bool_2)
                {
                    paintEventArgs_0.Graphics.DrawImage(Class131.smethod_29(), rect);
                }
                else
                {
                    paintEventArgs_0.Graphics.FillRectangle(new SolidBrush(menuBackColor), rect);
                }
            }
            else
            {
                paintEventArgs_0.Graphics.FillRectangle(new SolidBrush(menuBackColor), rect);
            }
            if (!bool_2 || this.bool_1)
            {
                paintEventArgs_0.Graphics.FillRectangle(new SolidBrush(this.menuColorTable_0.MenuDarkBackColor), new Rectangle(int_8, int_9, this.int_2, this.int_0));
            }
            this.method_8(int_8, int_9, paintEventArgs_0);
            Bitmap icon = navigateMenuNode_1.Icon;
            icon = this.method_13(icon, 0x18, 0x18);
            paintEventArgs_0.Graphics.DrawImage(icon, (int) (((int_8 + this.int_2) - icon.Width) / 2), (int) (int_9 + ((this.int_0 - icon.Height) / 2)));
            if (base.Width > this.int_2)
            {
                paintEventArgs_0.Graphics.DrawString(navigateMenuNode_1.SimpleText, this.font_0, new SolidBrush(this.menuColorTable_0.MenuFont), new Rectangle((int_8 + this.int_2) + this.int_3, int_9 + 7, (base.Width - this.int_2) - this.int_3, (this.int_0 - 2) - 14));
            }
        }

        private void method_8(int int_8, int int_9, PaintEventArgs paintEventArgs_0)
        {
            paintEventArgs_0.Graphics.DrawLine(new Pen(new SolidBrush(this.menuColorTable_0.MenuBoderInternal)), new Point(int_8, (int_9 + this.int_0) - 2), new Point(int_8 + base.Width, (int_9 + this.int_0) - 2));
            paintEventArgs_0.Graphics.DrawLine(new Pen(new SolidBrush(this.menuColorTable_0.MenuBoderOuter)), new Point(int_8, (int_9 + this.int_0) - 1), new Point(int_8 + base.Width, (int_9 + this.int_0) - 1));
            paintEventArgs_0.Graphics.DrawLine(new Pen(new SolidBrush(this.menuColorTable_0.MenuVertualLine)), new Point(int_8, int_9), new Point(int_8, (int_9 + this.int_0) - 2));
            paintEventArgs_0.Graphics.DrawLine(new Pen(new SolidBrush(this.menuColorTable_0.MenuVertualLine)), new Point(int_8 + this.int_2, int_9), new Point(int_8 + this.int_2, (int_9 + this.int_0) - 2));
            if (base.Width > this.int_2)
            {
                paintEventArgs_0.Graphics.DrawLine(new Pen(new SolidBrush(this.menuColorTable_0.MenuVertualLine)), new Point((int_8 + base.Width) - 1, int_9), new Point((int_8 + base.Width) - 1, (int_9 + this.int_0) - 2));
            }
        }

        private void method_9(PaintEventArgs paintEventArgs_0, int int_8, ref int int_9)
        {
            if (((this.navigateMenuNode_0 != null) && (this.navigateMenuNode_0.Node != null)) && (this.navigateMenuNode_0.Node.Count > 0))
            {
                Color menuBackColor = this.menuColorTable_0.MenuBackColor;
                int x = 0;
                int y = base.Height - this.int_0;
                paintEventArgs_0.Graphics.FillRectangle(new SolidBrush(this.menuColorTable_0.MenuDarkBackColor), new Rectangle(0, base.Height - this.int_0, base.Width, this.int_0));
                if (base.Width > this.int_2)
                {
                    int num6 = (base.Width - this.int_2) / this.int_4;
                    if (this.navigateMenuNode_0.Node.Count > int_8)
                    {
                        int num2 = num6 - 1;
                        int num = this.navigateMenuNode_0.Node.Count - int_8;
                        if (num <= (num6 - 1))
                        {
                            num2 = num;
                        }
                        else
                        {
                            int_9 = num - num2;
                        }
                        for (int i = 0; i < num2; i++)
                        {
                            x = base.Width - (((num2 + 1) - i) * this.int_4);
                            y = base.Height - this.int_0;
                            Rectangle rectangle = new Rectangle(x, y, this.int_4, this.int_0 - 2);
                            this.list_0.Add(new NavigateMenuNodeRect(this.navigateMenuNode_0.Node[int_8 + i], rectangle));
                            if (this.int_7 == (int_8 + i))
                            {
                                paintEventArgs_0.Graphics.DrawImage(Class131.smethod_30(), rectangle);
                            }
                            else if (rectangle.Contains(this.point_0))
                            {
                                paintEventArgs_0.Graphics.DrawImage(Class131.smethod_29(), rectangle);
                            }
                            else
                            {
                                menuBackColor = this.menuColorTable_0.MenuDarkBackColor;
                                paintEventArgs_0.Graphics.FillRectangle(new SolidBrush(menuBackColor), rectangle);
                            }
                            Bitmap icon = this.navigateMenuNode_0.Node[int_8 + i].Icon;
                            icon = this.method_13(icon, 0x18, 0x18);
                            paintEventArgs_0.Graphics.DrawImage(icon, new Point(x + ((this.int_4 - icon.Width) / 2), y + (((this.int_0 - 2) - icon.Height) / 2)));
                        }
                    }
                }
                else
                {
                    int_9 = this.navigateMenuNode_0.Node.Count - int_8;
                }
                x = base.Width - this.int_4;
                y = base.Height - this.int_0;
                Rectangle rect = new Rectangle(x, y, this.int_4, this.int_0 - 2);
                if (rect.Contains(this.point_0))
                {
                    menuBackColor = this.menuColorTable_0.MenuMoreHover;
                    paintEventArgs_0.Graphics.DrawImage(Class131.smethod_29(), rect);
                }
                else
                {
                    menuBackColor = this.menuColorTable_0.MenuDarkBackColor;
                    paintEventArgs_0.Graphics.FillRectangle(new SolidBrush(menuBackColor), rect);
                }
                Image image = Class131.smethod_22();
                int num7 = x + ((this.int_4 - image.Width) / 2);
                int num8 = y + (((this.int_0 - 2) - image.Height) / 2);
                paintEventArgs_0.Graphics.DrawImage(image, new Point(num7, num8));
                x = 0;
                y = base.Height - this.int_0;
                this.method_8(0, y, paintEventArgs_0);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Rectangle rectangle3 = new Rectangle(0, 0, base.Width, this.int_1);
                if (rectangle3.Contains(this.point_0))
                {
                    this.bool_1 = true;
                }
                if (((this.navigateMenuNode_0 != null) && (this.navigateMenuNode_0.Node != null)) && (this.navigateMenuNode_0.Node.Count > 0))
                {
                    Rectangle rectangle2 = new Rectangle(0, this.int_1, base.Width, (base.Height - this.int_1) - this.int_0);
                    if (rectangle2.Contains(this.point_0))
                    {
                        int num8 = (this.point_0.Y - this.int_1) / this.int_0;
                        this.method_12(this.navigateMenuNode_0.Node[num8]);
                        this.vmethod_0(this, new EventArgs0(this.navigateMenuNode_0.Node[num8]));
                    }
                    Rectangle rectangle4 = new Rectangle(this.int_2, base.Height - this.int_0, base.Width - this.int_2, this.int_0);
                    if (rectangle4.Contains(this.point_0))
                    {
                        int num9 = (base.Height - this.int_1) - this.int_0;
                        int num6 = num9 / this.int_0;
                        int num5 = (base.Width - this.int_2) / this.int_4;
                        if (this.navigateMenuNode_0.Node.Count > num6)
                        {
                            int num = num5 - 1;
                            int num7 = this.navigateMenuNode_0.Node.Count - num6;
                            if (num7 < (num5 - 1))
                            {
                                num = num7;
                            }
                            for (int i = 0; i < num; i++)
                            {
                                int x = base.Width - (((num + 1) - i) * this.int_4);
                                int y = base.Height - this.int_0;
                                Rectangle rectangle = new Rectangle(x, y, this.int_4, this.int_0 - 2);
                                if (rectangle.Contains(this.point_0))
                                {
                                    this.vmethod_0(this, new EventArgs0(this.navigateMenuNode_0.Node[num6 + i]));
                                    break;
                                }
                            }
                        }
                    }
                    Rectangle rectangle5 = new Rectangle(base.Width - this.int_4, base.Height - this.int_0, this.int_4, this.int_0);
                    if (rectangle5.Contains(this.point_0))
                    {
                        this.contextMenuStrip_0.Show(this, this.point_0);
                        this.bool_0 = true;
                    }
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!this.bool_0)
            {
                this.point_0 = new Point(-1, -1);
                base.Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.point_0 = new Point(e.X, e.Y);
            Rectangle rectangle = new Rectangle(0, 0, base.Width, this.int_1);
            if (rectangle.Contains(this.point_0))
            {
                if ((this.point_0.X >= 0) && (this.point_0.X <= base.Width))
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else
                {
                    this.Cursor = Cursors.Arrow;
                }
            }
            else if (this.bool_1)
            {
                if ((this.point_0.X >= 0) && (this.point_0.X <= base.Width))
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else
                {
                    this.Cursor = Cursors.Arrow;
                }
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
            if (((this.point_0.X >= 0) || (this.point_0.X <= base.Width)) && this.bool_1)
            {
                if (this.point_0.Y < 0)
                {
                    if (-this.point_0.Y >= this.int_0)
                    {
                        base.Height += this.int_0;
                    }
                }
                else if ((base.Height > (this.int_1 + this.int_0)) && ((this.point_0.Y - this.int_1) >= this.int_0))
                {
                    base.Height -= this.int_0;
                }
            }
            Rectangle rectangle2 = new Rectangle(0, 0, 0, 0);
            if ((e.X <= 0) || (e.Y <= 0))
            {
                this.rectangle_0 = rectangle2;
            }
            if (!rectangle2.Equals(this.rectangle_0))
            {
                if (!this.rectangle_0.Contains(new Point(e.X, e.Y)))
                {
                    this.method_11(e.X + 20, e.Y);
                }
            }
            else
            {
                this.method_11(e.X + 20, e.Y);
            }
            base.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                this.bool_1 = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            bool flag = false;
            this.bool_0 = false;
            int num = 0;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.FillRectangle(new SolidBrush(this.menuColorTable_0.MenuBackColor), new Rectangle(-1, -1, base.Width + 1, base.Height + 1));
            this.method_6(e);
            int num2 = (base.Height - this.int_1) - this.int_0;
            int num3 = num2 / this.int_0;
            this.list_0.Clear();
            if (((this.navigateMenuNode_0 != null) && (this.navigateMenuNode_0.Node != null)) && ((this.navigateMenuNode_0.Node.Count > 0) && (num3 > 0)))
            {
                int num4 = (num3 > this.navigateMenuNode_0.Node.Count) ? this.navigateMenuNode_0.Node.Count : num3;
                for (int i = 0; i < num4; i++)
                {
                    int num7 = 0;
                    int y = this.int_1 + (i * this.int_0);
                    Rectangle rectangle = new Rectangle(0, y, base.Width, this.int_0);
                    this.list_0.Add(new NavigateMenuNodeRect(this.navigateMenuNode_0.Node[i], rectangle));
                    if (rectangle.Contains(this.point_0))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    bool flag2 = false;
                    if (this.int_7 == i)
                    {
                        flag2 = true;
                    }
                    this.method_7(e, num7, y, flag, flag2, this.navigateMenuNode_0.Node[i]);
                }
            }
            this.method_9(e, num3, ref num);
            this.contextMenuStrip_0.Items.Clear();
            this.contextMenuStrip_0.Items.Add(this.toolStripMenuItem_1);
            this.contextMenuStrip_0.Items.Add(this.toolStripMenuItem_0);
            if (((this.navigateMenuNode_0 != null) && (this.navigateMenuNode_0.Node != null)) && ((this.navigateMenuNode_0.Node.Count > 0) && (num > 0)))
            {
                ToolStripSeparator separator = new ToolStripSeparator();
                this.contextMenuStrip_0.Items.Add(separator);
                for (int j = this.navigateMenuNode_0.Node.Count - num; j < this.navigateMenuNode_0.Node.Count; j++)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem {
                        Name = this.navigateMenuNode_0.Node[j].Name,
                        Text = this.navigateMenuNode_0.Node[j].Text
                    };
                    item.Click += new EventHandler(this.method_5);
                    this.contextMenuStrip_0.Items.Add(item);
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.method_10();
            if (base.Width < this.int_2)
            {
                base.Width = this.int_2;
            }
            base.Invalidate();
        }

        private void toolStripMenuItem_0_Click(object sender, EventArgs e)
        {
            base.Height -= this.int_0;
        }

        private void toolStripMenuItem_1_Click(object sender, EventArgs e)
        {
            base.Height += this.int_0;
        }

        protected virtual void vmethod_0(object sender, EventArgs0 e)
        {
            for (int i = 0; i < this.method_2().Node.Count; i++)
            {
                if (e.method_0().Equals(this.method_2().Node[i]))
                {
                    this.int_7 = i;
                    break;
                }
            }
            if (this.Event_0 != null)
            {
                e.method_0().IsExpand = true;
                this.Event_0(sender, e);
            }
        }
    }
}

