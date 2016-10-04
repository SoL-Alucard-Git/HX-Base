namespace Aisino.Fwkp.HomePage.AisinoDock.Docks
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.HomePage.AisinoDock;
    using Aisino.Fwkp.HomePage.AisinoDock.Move;
    using Aisino.Fwkp.HomePage.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class IconDock : IDock
    {
        private Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl _ButtonEdit;
        private int _buttonWidth;
        private static IDock _dock;
        private bool _IconEdit;
        private int _viewIndex;
        public List<Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl> Buttons;
        private int ButtonSpan;
        private IContainer components;
        private Rectangle IconRectangle;
        private int PageWidth;
        private AisinoPNL panDock;
        private DoubelPanel PanList;
        private static int time = 0;
        private Timer timer1;

        public IconDock()
        {
            this.ButtonSpan = 4;
            this._buttonWidth = 80;
            this.IconRectangle = Rectangle.Empty;
            this.Buttons = new List<Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl>();
            this.PageWidth = 390;
            this.InitializeComponent();
        }

        public IconDock(PageControl page) : base(page)
        {
            this.ButtonSpan = 4;
            this._buttonWidth = 80;
            this.IconRectangle = Rectangle.Empty;
            this.Buttons = new List<Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl>();
            this.PageWidth = 390;
            this.InitializeComponent();
            base.MetorColorChange += new IDock.MetorColorChangeEventHandler(this.IconDock_MetorColorChange);
            this.PanList.Paint += new PaintEventHandler(this.PanList_Paint);
            this.timer1.Enabled = true;
        }

        public void AddButton(Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl control)
        {
            if (control != null)
            {
                control.Height = this.ButtonWidth;
                control.Width = (this.ButtonWidth * control.DockColumn) + (this.ButtonSpan * (control.DockColumn - 1));
                this.Buttons.Add(control);
                this.PanList.Controls.Add(control);
                this.ButtonSort();
            }
        }

        public void ButtonControl(Point p)
        {
            int x = 0;
            int y = 0;
            int[] numArray = new int[3];
            this.IconRectangle = Rectangle.Empty;
            int num3 = -1;
            for (int i = 0; i < this.Buttons.Count; i++)
            {
                Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl control = this.Buttons[i];
                if (control.Equals(this.ButtonEdit))
                {
                    num3 = i;
                }
                else
                {
                    x = numArray[control.DockRow];
                    y = control.DockRow * (this.ButtonSpan + this.ButtonWidth);
                    Point location = new Point(x, y);
                    Rectangle rectangle = new Rectangle(location, new Size(control.Width + this.ButtonSpan, control.Height + this.ButtonSpan));
                    if (rectangle.Contains(p))
                    {
                        this.IconRectangle = new Rectangle(x + this.ButtonSpan, y + this.ButtonSpan, this.ButtonEdit.Width, this.ButtonEdit.Height);
                        if (this.ButtonEdit != null)
                        {
                            this.Buttons.Remove(this.ButtonEdit);
                            if ((num3 > -1) && (num3 < i))
                            {
                                this.Buttons.Insert(i - 1, this.ButtonEdit);
                            }
                            else
                            {
                                this.Buttons.Insert(i, this.ButtonEdit);
                            }
                            this.ButtonEdit.DockRow = control.DockRow;
                            break;
                        }
                    }
                    x = numArray[control.DockRow];
                    control.Location = new Point(x + this.ButtonSpan, y + this.ButtonSpan);
                    numArray[control.DockRow] += this.ButtonSpan + control.Width;
                }
            }
            if (this.IconRectangle == Rectangle.Empty)
            {
                int num5 = p.Y / (this.ButtonSpan + this.ButtonWidth);
                if (num5 > 2)
                {
                    num5 = 2;
                }
                if (this.ButtonEdit != null)
                {
                    this.Buttons.Remove(this.ButtonEdit);
                    this.Buttons.Add(this.ButtonEdit);
                    this.ButtonEdit.DockRow = num5;
                }
            }
            new PageXml().SaveIcon(Common.UserName, this.Buttons);
            this.PanList.Invalidate();
            base.Invalidate();
            this.ButtonSort();
            int num6 = this.PointToPage(this.ButtonEdit.Location);
            this.PageRun(num6);
        }

        public void ButtonLocation(Point p)
        {
            int x = 0;
            int y = 0;
            int[] numArray = new int[3];
            this.IconRectangle = Rectangle.Empty;
            foreach (Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl control in this.Buttons)
            {
                if (!control.Equals(this.ButtonEdit))
                {
                    x = numArray[control.DockRow];
                    y = control.DockRow * (this.ButtonSpan + this.ButtonWidth);
                    Point location = new Point(x, y);
                    Rectangle rectangle = new Rectangle(location, new Size(control.Width + this.ButtonSpan, control.Height + this.ButtonSpan));
                    if (rectangle.Contains(p))
                    {
                        this.IconRectangle = new Rectangle(x + this.ButtonSpan, y + this.ButtonSpan, this.ButtonEdit.Width, this.ButtonEdit.Height);
                        numArray[control.DockRow] += this.ButtonSpan + this.ButtonEdit.Width;
                    }
                    x = numArray[control.DockRow];
                    control.Location = new Point(x + this.ButtonSpan, y + this.ButtonSpan);
                    numArray[control.DockRow] += this.ButtonSpan + control.Width;
                }
            }
            if (this.IconRectangle == Rectangle.Empty)
            {
                int index = p.Y / (this.ButtonSpan + this.ButtonWidth);
                if (index > 2)
                {
                    index = 2;
                }
                this.IconRectangle = new Rectangle(numArray[index] + this.ButtonSpan, (index * (this.ButtonSpan + this.ButtonWidth)) + this.ButtonSpan, this.ButtonEdit.Width, this.ButtonEdit.Height);
            }
            this.PanList.Invalidate();
        }

        public void ButtonSort()
        {
            this.ButtonWidth = (this.PanList.Height - (4 * this.ButtonSpan)) / 3;
            int num = 0;
            int num2 = 0;
            int[] numArray = new int[3];
            this.IconRectangle = Rectangle.Empty;
            for (int i = 0; i < this.Buttons.Count; i++)
            {
                Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl control = this.Buttons[i];
                num2 = control.DockRow * (this.ButtonSpan + this.ButtonWidth);
                num = numArray[control.DockRow];
                control.Location = new Point(num + this.ButtonSpan, num2 + this.ButtonSpan);
                numArray[control.DockRow] += this.ButtonSpan + control.Width;
                if (numArray[control.DockRow] > this.PanList.Width)
                {
                    this.PanList.Width = numArray[control.DockRow];
                }
            }
            base.Invalidate();
        }

        public static IDock CreateDock(PageControl page)
        {
            if ((_dock == null) || _dock.IsDisposed)
            {
                _dock = new IconDock(page);
            }
            return _dock;
        }

        public void DeleteButton(Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl control)
        {
            this.Buttons.Remove(control);
            this.PanList.Controls.Remove(control);
            this.ButtonSort();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private int GetPageCount()
        {
            int num = this.panDock.Width / (this.ButtonSpan + this.ButtonWidth);
            if ((this.panDock.Width % (this.ButtonSpan + this.ButtonWidth)) >= this.ButtonWidth)
            {
                num++;
            }
            return num;
        }

        private List<Rectangle> GetViewChangeRectangle()
        {
            int viewCount = this.GetViewCount();
            Console.WriteLine(viewCount);
            int width = 20;
            float num3 = viewCount;
            int y = base.Height - 0x16;
            int x = (base.Width - ((int) (num3 * width))) / 2;
            List<Rectangle> list = new List<Rectangle>();
            for (int i = 0; i < viewCount; i++)
            {
                Rectangle item = new Rectangle(x, y, width, width);
                list.Add(item);
                x += width;
            }
            return list;
        }

        public int GetViewCount()
        {
            int pageCount = this.GetPageCount();
            int[] numArray = new int[3];
            foreach (Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl control in this.Buttons)
            {
                numArray[control.DockRow] += control.DockColumn;
            }
            int num2 = 0;
            for (int i = 0; i < 3; i++)
            {
                if (num2 < numArray[i])
                {
                    num2 = numArray[i];
                }
            }
            int num4 = num2 / pageCount;
            if ((num2 % pageCount) > 0)
            {
                num4++;
            }
            if (num4 == 0)
            {
                num4++;
            }
            return num4;
        }

        private void IconDock_MetorColorChange(object sender, MetorChangeEventArgs e)
        {
            foreach (Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl control in this.Buttons)
            {
                control.MetorColor = base.MetorColor;
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.PanList = new DoubelPanel();
            this.panDock = new AisinoPNL();
            this.timer1 = new Timer(this.components);
            this.panDock.SuspendLayout();
            base.SuspendLayout();
            this.PanList.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.PanList.Location = new Point(3, 3);
            this.PanList.Name = "PanList";
            this.PanList.Size = new Size(0x144, 0x132);
            this.PanList.TabIndex = 0;
            this.panDock.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.panDock.Controls.Add(this.PanList);
            this.panDock.Location = new Point(3, 0x1c);
            this.panDock.Name = "panDock";
            this.panDock.Size = new Size(0x15a, 0x114);
            this.panDock.TabIndex = 1;
            this.timer1.Interval = 50;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            this.BackColor = Color.White;
            base.Controls.Add(this.panDock);
            base.Name = "IconDock";
            base.Size = new Size(0x160, 0x151);
            base.Title = "快捷方式";
            this.panDock.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public bool IsButtonExists(string value)
        {
            foreach (Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl control in this.Buttons)
            {
                if (control.ClickText == value)
                {
                    return true;
                }
            }
            return false;
        }

        public override void LoadUser(string username)
        {
            base.LoadUser(username);
            new PageXml().LoadIcon(username, this);
            this.ButtonSort();
        }

        protected override void OnClick(EventArgs e)
        {
            Point position = Cursor.Position;
            position = base.PointToClient(position);
            List<Rectangle> viewChangeRectangle = this.GetViewChangeRectangle();
            for (int i = 0; i < viewChangeRectangle.Count; i++)
            {
                Rectangle rectangle = viewChangeRectangle[i];
                if ((i != this.ViewIndex) && rectangle.Contains(position))
                {
                    this.PageRun(i);
                    return;
                }
            }
            base.OnClick(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.OnSizeChanged(null);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            List<Rectangle> viewChangeRectangle = this.GetViewChangeRectangle();
            for (int i = 0; i < viewChangeRectangle.Count; i++)
            {
                Rectangle rectangle = viewChangeRectangle[i];
                if ((i != this.ViewIndex) && rectangle.Contains(e.Location))
                {
                    this.Cursor = Cursors.Hand;
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            List<Rectangle> viewChangeRectangle = this.GetViewChangeRectangle();
            if (viewChangeRectangle.Count > 1)
            {
                for (int i = 0; i < viewChangeRectangle.Count; i++)
                {
                    Rectangle rect = viewChangeRectangle[i];
                    if (i == this.ViewIndex)
                    {
                        e.Graphics.DrawImage(Resources.m1, rect);
                    }
                    else
                    {
                        e.Graphics.DrawImage(Resources.m2, rect);
                    }
                }
            }
            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.panDock != null)
            {
                this.panDock.Size = new Size(base.Width - 8, (base.Height - this.panDock.Location.Y) - 0x16);
                if (this.PanList != null)
                {
                    this.PanList.Location = Point.Empty;
                    this.PanList.Height = this.panDock.Height;
                    this.ButtonWidth = (this.PanList.Height - (4 * this.ButtonSpan)) / 3;
                    this.PageWidth = this.GetPageCount() * (this.ButtonSpan + this.ButtonWidth);
                    this.ButtonSort();
                }
            }
        }

        private void PageRun(int i)
        {
            if (i >= this.GetViewCount())
            {
                i = this.GetViewCount() - 1;
            }
            if (i < 0)
            {
                i = 0;
            }
            StoryBoard board = new StoryBoard(this.panDock);
            StoryBoard.ControlAnimation item = new StoryBoard.ControlAnimation {
                animation = new AnimationPoint(this.PanList.Location, new Point(-i * this.PageWidth, this.PanList.Location.Y), 150),
                control = this.PanList,
                name = "Location"
            };
            board.Add(item);
            board.Start();
            this.ViewIndex = i;
            base.Invalidate();
        }

        public void PageRunBoundary(int i)
        {
            if (time >= 0x3e8)
            {
                this.PageRun(i);
                time = 0;
            }
        }

        public void PageRunToPoint(Point pt)
        {
            Point point = this.panDock.PointToClient(pt);
            if (point.X < 10)
            {
                this.PageRunBoundary(this.ViewIndex - 1);
            }
            else if (point.X > (this.panDock.Width - 10))
            {
                this.PageRunBoundary(this.ViewIndex + 1);
            }
        }

        private void PanList_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.YellowGreen), this.IconRectangle);
        }

        private int PointToPage(Point p)
        {
            int num = p.X / this.PageWidth;
            if ((p.X % this.PageWidth) != 0)
            {
                num++;
            }
            num--;
            if (num < 0)
            {
                return 0;
            }
            return num;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time += 50;
        }

        public Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl ButtonEdit
        {
            get
            {
                return this._ButtonEdit;
            }
            set
            {
                this._ButtonEdit = value;
            }
        }

        public int ButtonWidth
        {
            get
            {
                return this._buttonWidth;
            }
            set
            {
                if (this._buttonWidth != value)
                {
                    this._buttonWidth = value;
                    foreach (Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl control in this.Buttons)
                    {
                        control.Height = value;
                        control.Width = (value * control.DockColumn) + (this.ButtonSpan * (control.DockColumn - 1));
                    }
                }
            }
        }

        public bool IconEdit
        {
            get
            {
                return this._IconEdit;
            }
            set
            {
                if (this._IconEdit != value)
                {
                    this._IconEdit = value;
                    foreach (Aisino.Fwkp.HomePage.AisinoDock.Docks.ButtonControl control in this.Buttons)
                    {
                        control.IconEdit = this._IconEdit;
                    }
                    base.Invalidate();
                    this.PanList.Invalidate();
                }
            }
        }

        private int ViewIndex
        {
            get
            {
                return this._viewIndex;
            }
            set
            {
                if (this._viewIndex != value)
                {
                    int viewCount = this.GetViewCount();
                    if (value >= viewCount)
                    {
                        value = viewCount - 1;
                    }
                    if (value < 0)
                    {
                        value = 0;
                    }
                    this._viewIndex = value;
                }
            }
        }

        public class ButtonEventArgs
        {
            public ButtonControl Control;
            public int Type;
        }
    }
}

