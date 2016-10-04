namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class AisinoToolBar : Control
    {
        private bool bool_0;
        private tpButton expandClick;
        private IContainer icontainer_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        private int int_5;
        private int int_6;
        private int int_7;
        private int int_8;
        private tpButton leftClick;
        private tpButton rightClick;
        private SortedDictionary<int, ToolBarItem> sortedDictionary_0;
        private Timer timer_0;

        public AisinoToolBar()
        {
            
            this.int_2 = 0x48;
            this.int_3 = 10;
            this.int_4 = 5;
            this.int_5 = 15;
            this.bool_0 = true;
            this.method_0();
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.ContainerControl, true);
            base.Height = 0x5c;
            this.sortedDictionary_0 = new SortedDictionary<int, ToolBarItem>();
            this.leftClick = new tpButton();
            this.rightClick = new tpButton();
            this.expandClick = new tpButton();
            this.method_1();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void expandClick_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                base.Height = 10;
                this.expandClick.Bitmap = Class131.smethod_14();
                this.bool_0 = false;
            }
            else
            {
                this.expandClick.Bitmap = Class131.smethod_18();
                base.Height = 0x5c;
                this.bool_0 = true;
            }
        }

        public static void FillRectangleWithMultiColors(Graphics graphics_0, Rectangle rectangle_0, Color[] color_0, float[] float_0, float float_1)
        {
            if (((rectangle_0.Width > 0) && (rectangle_0.Height > 0)) && ((color_0.Length >= 2) && (color_0.Length == float_0.Length)))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(rectangle_0, color_0[0], color_0[1], float_1))
                {
                    ColorBlend blend = new ColorBlend(color_0.Length);
                    for (int i = 0; i < color_0.Length; i++)
                    {
                        blend.Colors[i] = color_0[i];
                        blend.Positions[i] = float_0[i];
                    }
                    brush.InterpolationColors = blend;
                    graphics_0.FillRectangle(brush, rectangle_0);
                }
            }
        }

        public void Load(string string_0)
        {
            if (File.Exists(string_0))
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(string_0);
                    if ((document == null) || !document.HasChildNodes)
                    {
                        throw new Exception("XML文件格式不正确！");
                    }
                    XmlNodeList list = document.SelectNodes("root/ToolBarItemCollection/ToolBarItem");
                    if ((list == null) || (list.Count <= 0))
                    {
                        throw new Exception("XML文件格式不正确！");
                    }
                    this.method_2();
                    foreach (XmlNode node in list)
                    {
                        XmlAttributeCollection attributes = node.Attributes;
                        string str = attributes["text"].Value;
                        string str2 = attributes["image"].Value;
                        string str3 = attributes["menuID"].Value;
                        Bitmap bitmap = ResourceUtil.GetBitmap(str2);
                        if (bitmap == null)
                        {
                            bitmap = Class131.smethod_38();
                        }
                        this.NodeAdd(str, str3, bitmap);
                    }
                    return;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            throw new Exception("文件不存在！");
        }

        private void method_0()
        {
            this.icontainer_0 = new Container();
            this.timer_0 = new Timer(this.icontainer_0);
            base.SuspendLayout();
            this.timer_0.Interval = 10;
            this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
            base.ResumeLayout(false);
        }

        private void method_1()
        {
            this.int_0 = 0;
            this.int_6 = 0;
            this.leftClick.Name = "leftClick";
            this.leftClick.Bitmap = Class131.smethod_15();
            this.leftClick.Dock = DockStyle.Left;
            this.leftClick.Width = 10;
            this.leftClick.Click += new EventHandler(this.rightClick_Click);
            base.Controls.Add(this.leftClick);
            this.rightClick.Name = "rightClick";
            this.rightClick.Bitmap = Class131.smethod_17();
            this.rightClick.Dock = DockStyle.Right;
            this.rightClick.Width = 10;
            this.rightClick.Click += new EventHandler(this.rightClick_Click);
            base.Controls.Add(this.rightClick);
            this.expandClick.Name = "expandClick";
            this.expandClick.Bitmap = Class131.smethod_18();
            this.expandClick.Width = 100;
            this.expandClick.Height = 10;
            this.expandClick.Click += new EventHandler(this.expandClick_Click);
            base.Controls.Add(this.expandClick);
        }

        private void method_2()
        {
            base.Controls.Clear();
            base.Controls.Add(this.leftClick);
            base.Controls.Add(this.rightClick);
            base.Controls.Add(this.expandClick);
        }

        private int method_3()
        {
            return (base.Width / (this.int_2 + this.int_3));
        }

        public void NodeAdd(string string_0, string string_1, Bitmap bitmap_0)
        {
            ToolBarItem item = new ToolBarItem {
                Text = string_0,
                Bitmap = bitmap_0
            };
            item.Name = "ToolBarItem" + ((this.sortedDictionary_0.Count + 1)).ToString();
            item.MenuID = string_1;
            int x = this.int_3;
            if (this.sortedDictionary_0.Count > 0)
            {
                x = (this.sortedDictionary_0[this.sortedDictionary_0.Count - 1].Location.X + this.int_2) + this.int_3;
            }
            int y = (base.Height - item.Height) / 2;
            item.Location = new Point(x, y);
            this.sortedDictionary_0.Add(this.sortedDictionary_0.Count, item);
            base.Controls.Add(item);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.int_1 = this.method_3();
            bool flag = false;
            if (this.int_1 < this.sortedDictionary_0.Count)
            {
                flag = true;
            }
            else
            {
                this.int_1 = this.sortedDictionary_0.Count;
                flag = false;
            }
            if (flag)
            {
                if (this.int_0 > 0)
                {
                    this.leftClick.Visible = true;
                }
                else
                {
                    this.leftClick.Visible = false;
                }
                if ((this.sortedDictionary_0.Count - this.int_0) > this.int_1)
                {
                    this.rightClick.Visible = true;
                }
                else
                {
                    this.rightClick.Visible = false;
                }
            }
            else if (this.int_0 > 0)
            {
                this.leftClick.Visible = true;
                this.rightClick.Visible = false;
            }
            else
            {
                this.leftClick.Visible = false;
                this.rightClick.Visible = false;
            }
            int x = (base.Width - this.expandClick.Width) / 2;
            int y = base.Height - 10;
            this.expandClick.Location = new Point(x, y);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Color[] colorArray = new Color[] { ColorTranslator.FromHtml("#31819c"), ColorTranslator.FromHtml("#3889a6"), ColorTranslator.FromHtml("#319ac7"), ColorTranslator.FromHtml("#54b6d1") };
            FillRectangleWithMultiColors(e.Graphics, new Rectangle(0, 0, base.Width, base.Height), colorArray, new float[] { 0f, 0.6f, 0.9f, 1f }, 20f);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            base.Invalidate();
        }

        private void rightClick_Click(object sender, EventArgs e)
        {
            if (!this.timer_0.Enabled)
            {
                tpButton button = sender as tpButton;
                if (button.Name == "leftClick")
                {
                    this.int_6 = this.int_0 - this.int_4;
                    if (this.int_6 < 0)
                    {
                        this.int_6 = 0;
                    }
                }
                else if (button.Name == "rightClick")
                {
                    this.int_6 = this.int_0 + this.int_4;
                    if (this.int_6 > this.sortedDictionary_0.Count)
                    {
                        this.int_6 = this.sortedDictionary_0.Count;
                    }
                }
                this.int_7 = this.sortedDictionary_0[this.int_0].Location.X;
                this.int_8 = this.sortedDictionary_0[this.int_6].Location.X;
                this.timer_0.Start();
            }
        }

        private void timer_0_Tick(object sender, EventArgs e)
        {
            int num = 0;
            if (this.int_7 < this.int_8)
            {
                num = -this.int_5;
                this.int_7 += this.int_5;
                if (this.int_7 > this.int_8)
                {
                    this.int_7 = this.int_8;
                }
            }
            else if (this.int_7 > this.int_8)
            {
                num = this.int_5;
                this.int_7 -= this.int_5;
                if (this.int_7 < this.int_8)
                {
                    this.int_7 = this.int_8;
                }
            }
            else
            {
                num = 0;
            }
            foreach (Control control in base.Controls)
            {
                if (control is ToolBarItem)
                {
                    control.Location = new Point(control.Location.X + num, control.Location.Y);
                }
            }
            base.Invalidate();
            if (this.int_7 == this.int_8)
            {
                this.timer_0.Stop();
                this.int_0 = this.int_6;
            }
        }
    }
}

