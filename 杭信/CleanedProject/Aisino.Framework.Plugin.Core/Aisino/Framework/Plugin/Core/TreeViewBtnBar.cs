namespace Aisino.Framework.Plugin.Core
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class TreeViewBtnBar : AisinoTVW
    {
        private Color color_0;
        private Color color_1;
        private readonly Font font_0;
        private readonly Font font_1;
        private readonly Image image_0;
        private int int_0;
        private int int_1;

        public TreeViewBtnBar()
        {
            
            this.color_0 = Color.WhiteSmoke;
            this.color_1 = Color.Bisque;
            this.image_0 = Class131.smethod_19();
            this.font_0 = new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Bold);
            this.font_1 = new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Regular);
            this.int_0 = 5;
            this.int_1 = 1;
            this.method_0();
            base.DrawNode += new DrawTreeNodeEventHandler(this.TreeViewBtnBar_DrawNode);
        }

        private void method_0()
        {
            this.BackColor = SystemColors.Info;
            base.BorderStyle = BorderStyle.None;
            base.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            base.FullRowSelect = true;
            base.ImageIndex = 0;
            base.Indent = 10;
            base.ItemHeight = 0x23;
        }

        private void TreeViewBtnBar_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                e.Graphics.DrawImage(this.image_0, e.Bounds.X + 10, e.Bounds.Y + 1, 40, 0x23);
                e.Graphics.DrawString(e.Node.Text, this.font_1, Brushes.Black, (float) (e.Bounds.X + this.int_0), (float) (e.Bounds.Y + 10));
            }
            else
            {
                Rectangle rect = new Rectangle(e.Bounds.X + this.int_1, e.Bounds.Y + this.int_1, e.Bounds.Width - (2 * this.int_1), e.Bounds.Height - (2 * this.int_1));
                using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, this.color_0, this.color_1, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
                Pen pen = new Pen(Color.LightCyan, 2f);
                e.Graphics.DrawRectangle(pen, rect);
                e.Graphics.DrawString(e.Node.Text, this.font_0, Brushes.WhiteSmoke, (float) ((e.Bounds.X + this.int_1) + 50), (float) ((e.Bounds.Y + this.int_1) + 2));
            }
        }

        public Color EndColor
        {
            get
            {
                return this.color_1;
            }
            set
            {
                this.color_1 = value;
            }
        }

        public int Pad
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public Color StartColor
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
            }
        }

        public int SubNodeIndent
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }
    }
}

