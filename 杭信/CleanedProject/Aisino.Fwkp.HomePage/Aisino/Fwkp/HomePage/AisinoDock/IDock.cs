namespace Aisino.Fwkp.HomePage.AisinoDock
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Fwkp.HomePage.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class IDock : UserControl
    {
        private List<IDockClickEventArgs> _buttons;
        private int _ColumnsValue;
        private Point _DockLoacation;
        private Size _DockSize;
        private Image _icon;
        private Color _metorColor;
        private PageControl _page;
        private string _title;
        private IContainer components;
        private bool isEdit;
        private Rectangle MoveRectangle;
        private Point OldPoint;
        private static readonly int Radius = 6;

        public event MetorColorChangeEventHandler MetorColorChange;

        public IDock()
        {
            this._DockSize = new Size(2, 2);
            this._DockLoacation = new Point(0, 0);
            this._metorColor = Color.FromArgb(0xbf, 0xcf, 0xd8);
            this._title = string.Empty;
            this._buttons = new List<IDockClickEventArgs>();
            this.MoveRectangle = Rectangle.Empty;
            this.OldPoint = Point.Empty;
            this.InitializeComponent();
            base.Width = Common.HeadWidth - 10;
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            IDockClickEventArgs item = new IDockClickEventArgs {
                Image = Resources.close
            };
            item.Click += new EventHandler(this.close_Click);
            IDockClickEventArgs args2 = new IDockClickEventArgs {
                Image = Resources.advanced
            };
            args2.Click += new EventHandler(this.manager_Click);
            this.ToolMenu.Add(item);
            this.ToolMenu.Add(args2);
            this.MoveRectangle = new Rectangle(0, 0, base.Width, 0x19);
        }

        public IDock(PageControl page) : this()
        {
            this._page = page;
            this._page.AddDock(this);
        }

        private void close_Click(object sender, EventArgs e)
        {
            if (MessageBoxHelper.Show("确定要删除当前模块", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.MetorColor = Color.FromArgb(10, this.MetorColor);
                Thread.Sleep(500);
                this._page.DeleteDock(this);
            }
        }

        private GraphicsPath CreatePath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(rect.X, rect.Bottom + 1, rect.X, rect.Y + (Radius / 2));
            path.AddArc(rect.X, rect.Y, Radius, Radius, 180f, 90f);
            path.AddArc(rect.Right - Radius, rect.Y, Radius, Radius, 270f, 90f);
            path.AddLine(rect.Right, rect.Y + (Radius / 2), rect.Right, rect.Bottom + 1);
            path.CloseFigure();
            return path;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public IDockClickEventArgs GetButton(Point MousePoint)
        {
            foreach (IDockClickEventArgs args in this.ToolMenu)
            {
                if (args.Rect.Contains(MousePoint))
                {
                    return args;
                }
            }
            return null;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.MinimumSize = new Size(150, 150);
            base.Name = "IDock";
            base.Size = new Size(380, 320);
            base.ResumeLayout(false);
        }

        public virtual void LoadUser(string username)
        {
        }

        private void manager_Click(object sender, EventArgs e)
        {
            Common.MenuDockManager(Cursor.Position, this._page, new EventHandler(this._page.menuClick));
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point position = Cursor.Position;
                position = base.PointToClient(position);
                IDockClickEventArgs button = this.GetButton(position);
                if (button != null)
                {
                    button.OnClick(this, null);
                }
            }
            base.OnMouseClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (((e.Button == MouseButtons.Left) && this.MoveRectangle.Contains(e.Location)) && (this.GetButton(e.Location) == null))
            {
                this.IsEdit = true;
                this.OldPoint = e.Location;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool flag = false;
            if (!this.MoveRectangle.Contains(e.Location))
            {
                this.Cursor = Cursors.Default;
            }
            else
            {
                foreach (IDockClickEventArgs args in this.ToolMenu)
                {
                    if (args.Rect.Contains(e.Location))
                    {
                        this.Cursor = Cursors.Hand;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    this.Cursor = Cursors.SizeAll;
                }
            }
            if (((e.Button == MouseButtons.Left) && this.isEdit) && (this.MoveRectangle.Contains(e.Location) && (this.GetButton(e.Location) == null)))
            {
                Point location = e.Location;
                Point point2 = new Point(location.X - this.OldPoint.X, location.Y - this.OldPoint.Y);
                base.Location = new Point(base.Location.X + point2.X, base.Location.Y + point2.Y);
                Point p = base.PointToScreen(e.Location);
                this._page.SetControl(this._page.PointToClient(p), this);
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && this.isEdit)
            {
                this.IsEdit = false;
                Point p = base.PointToScreen(e.Location);
                this._page.SetLocaion(this._page.PointToClient(p), this);
                this.Cursor = Cursors.Default;
            }
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            new SolidBrush(this.MetorColor);
            Rectangle rect = new Rectangle(1, 1, base.Width - 2, 0x19);
            e.Graphics.FillRectangle(new LinearGradientBrush(new Point(rect.X, rect.Y), new Point(rect.X, rect.Bottom), Color.FromArgb(0xf9, 0xf9, 0xf9), Color.FromArgb(0xe3, 0xe3, 0xe3)), rect);
            Rectangle layoutRectangle = new Rectangle(10, 0, base.Width - 10, 0x19);
            StringFormat format = new StringFormat {
                LineAlignment = StringAlignment.Far
            };
            e.Graphics.DrawString(this.Title, new Font("宋体", 10f), new SolidBrush(Color.Black), layoutRectangle, format);
            Pen pen = new Pen(Color.FromArgb(0xa6, 0xa6, 0xa6), 1f);
            e.Graphics.DrawLine(pen, new Point(1, layoutRectangle.Bottom), new Point(base.Width - 2, layoutRectangle.Bottom));
            e.Graphics.DrawPath(pen, this.CreatePath(new Rectangle(1, 1, base.Width - 2, base.Height - 3)));
            int num = base.Width - 5;
            int width = 20;
            foreach (IDockClickEventArgs args in this.ToolMenu)
            {
                args.Rect = new Rectangle(num - width, 5, width, width);
                num = (num - width) - 5;
                if (args.Image == null)
                {
                    args.Image = Resources.m1;
                }
                e.Graphics.DrawImage(args.Image, args.Rect);
            }
            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.MoveRectangle = new Rectangle(0, 0, base.Width, 0x19);
            base.Invalidate();
        }

        public int ColumnsValue
        {
            get
            {
                return this._ColumnsValue;
            }
            set
            {
                if (this._ColumnsValue != value)
                {
                    this._ColumnsValue = value;
                }
            }
        }

        public Point DockLoacation
        {
            get
            {
                return this._DockLoacation;
            }
            set
            {
                this._DockLoacation = value;
                base.Location = new Point(value.X * Common.Length, value.Y * Common.Length);
            }
        }

        public Size DockSize
        {
            get
            {
                return this._DockSize;
            }
            set
            {
                this._DockSize = value;
                base.Width = value.Width * Common.Length;
                base.Height = value.Height * Common.Length;
            }
        }

        [DefaultValue((string) null), Description("图标")]
        public Image Icon
        {
            get
            {
                return this._icon;
            }
            set
            {
                this._icon = value;
            }
        }

        public bool IsEdit
        {
            get
            {
                return this.isEdit;
            }
            set
            {
                if (this.isEdit != value)
                {
                    this.isEdit = value;
                    this._page.IsEdit = this.isEdit;
                    base.Invalidate();
                }
            }
        }

        public Color MetorColor
        {
            get
            {
                return this._metorColor;
            }
            set
            {
                if (value != this._metorColor)
                {
                    this._metorColor = value;
                    if (this.MetorColorChange != null)
                    {
                        MetorChangeEventArgs e = new MetorChangeEventArgs {
                            Color = this.MetorColor
                        };
                        this.MetorColorChange(this, e);
                    }
                    base.Invalidate();
                }
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
                base.Invalidate();
            }
        }

        public List<IDockClickEventArgs> ToolMenu
        {
            get
            {
                return this._buttons;
            }
        }

        public delegate void MetorColorChangeEventHandler(object sender, MetorChangeEventArgs e);
    }
}

