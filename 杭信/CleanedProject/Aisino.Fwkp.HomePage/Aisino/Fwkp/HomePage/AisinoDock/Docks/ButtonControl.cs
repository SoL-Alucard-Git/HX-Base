namespace Aisino.Fwkp.HomePage.AisinoDock.Docks
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.HomePage.AisinoDock;
    using Aisino.Fwkp.HomePage.Properties;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    [Designer(typeof(ButtonDesigner))]
    public class ButtonControl : UserControl
    {
        private ButtonClickStyle _ClickStyle;
        private string _clickText;
        private int _DockColumn;
        private int _DockRow;
        private Image _icon;
        private bool _IconEdit;
        private string _IconName;
        private Color _MetorColor;
        private bool _MouseMove;
        private Color _MoveColor;
        private int _MsgNum;
        private string _title;
        private bool ButtonMove;
        private IContainer components;
        private Point OldPoint;
        private IconDock owner;
        private Rectangle rectChange;
        private Rectangle rectMsg;
        private System.Windows.Forms.Timer timer1;
        private int timeSum;
        private ToolTip tool_tag;

        public ButtonControl()
        {
            this._MetorColor = Color.FromArgb(0xb8, 0xcb, 0xd9);
            this._MoveColor = Color.FromArgb(130, 0xb5, 0xd9);
            this._ClickStyle = ButtonClickStyle.System;
            this._clickText = string.Empty;
            this._IconName = string.Empty;
            this._DockColumn = 1;
            this.rectMsg = Rectangle.Empty;
            this.rectChange = Rectangle.Empty;
            this.OldPoint = Point.Empty;
            this.Cursor = Cursors.Hand;
            this.rectMsg = new Rectangle(base.Width - 20, 0, 20, 20);
            this.rectChange = new Rectangle(0, base.Height - 20, 20, 20);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public ButtonControl(IconDock dock) : this()
        {
            this.InitializeComponent();
            base.Width = 80;
            base.Height = 80;
            this.owner = dock;
            this.owner.AddButton(this);
            this.BackColor = this.MetorColor;
            this.tool_tag.SetToolTip(this, this.Title);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void iconManager_AddButtonOperate(object sender, ButtonControlExEventArgs e)
        {
            ButtonControl control = new ButtonControl(this.owner) {
                ClickText = e.Key,
                ClickStyle = e.Style,
                MetorColor = this.MetorColor,
                Icon = e.Icon,
                IconName = e.IconName,
                Title = e.Name,
                IconEdit = this.IconEdit
            };
            int[] numArray = new int[3];
            foreach (ButtonControl control2 in this.owner.Buttons)
            {
                numArray[control2.DockRow] += control2.DockColumn;
            }
            int index = 0;
            int num2 = numArray[index];
            for (int i = 0; i < 3; i++)
            {
                if (numArray[i] < num2)
                {
                    num2 = numArray[i];
                    index = i;
                }
            }
            control.DockRow = index;
            new PageXml().SaveIcon(Common.UserName, this.owner.Buttons);
        }

        private void iconManager_DelteButtonOperate(object sender, ButtonControlExEventArgs e)
        {
            foreach (ButtonControl control in this.owner.Buttons)
            {
                if (control.ClickText == e.Key)
                {
                    this.owner.DeleteButton(control);
                    return;
                }
            }
            new PageXml().SaveIcon(Common.UserName, this.owner.Buttons);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tool_tag = new ToolTip(this.components);
            base.SuspendLayout();
            this.timer1.Interval = 50;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "ButtonControl";
            base.Size = new Size(80, 80);
            base.ResumeLayout(false);
        }

        private bool IsClickChange()
        {
            Point position = Cursor.Position;
            position = base.PointToClient(position);
            return this.rectChange.Contains(position);
        }

        private bool IsPointClose()
        {
            Point position = Cursor.Position;
            position = base.PointToClient(position);
            return this.rectMsg.Contains(position);
        }

        protected override void OnClick(EventArgs e)
        {
            if (!this.IconEdit)
            {
                if (this.ClickStyle == ButtonClickStyle.Add)
                {
                    IconManager manager = new IconManager(this.owner);
                    manager.Show();
                    manager.DelteButtonOperate += new IconManager.ButtonOperate(this.iconManager_DelteButtonOperate);
                    manager.AddButtonOperate += new IconManager.ButtonOperate(this.iconManager_AddButtonOperate);
                }
                else if (this.IsPointClose())
                {
                    Thread.Sleep(200);
                    this.owner.DeleteButton(this);
                }
                else if (this.IsClickChange())
                {
                    if (this.DockColumn == 1)
                    {
                        this.DockColumn++;
                        base.Width = (base.Width * 2) + 4;
                    }
                    else
                    {
                        this.DockColumn--;
                        base.Width = (base.Width / 2) - 2;
                    }
                    base.Invalidate();
                    this.owner.ButtonSort();
                }
                else if (this.IconEdit)
                {
                    base.OnClick(e);
                }
                else
                {
                    try
                    {
                        this.timer1.Stop();
                        switch (this.ClickStyle)
                        {
                            case ButtonClickStyle.URL:
                                Process.Start(this.ClickText);
                                return;

                            case ButtonClickStyle.System:
                                ToolUtil.RunFuction(this.ClickText);
                                return;

                            case ButtonClickStyle.Exe:
                                Process.Start(this.ClickText);
                                return;
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (((e.Button == MouseButtons.Left) && !this.IsPointClose()) && !this.IsClickChange())
            {
                this.ButtonMove = true;
                this.owner.ButtonEdit = this;
                this.OldPoint = e.Location;
                this.timer1.Start();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.BackColor = this.MoveColor;
            this._MouseMove = true;
            base.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.BackColor = this.MetorColor;
            this._MouseMove = false;
            base.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((this.ButtonMove && (e.Button == MouseButtons.Left)) && (this.IconEdit && !this.IsPointClose()))
            {
                Point location = e.Location;
                Point point2 = new Point(location.X - this.OldPoint.X, location.Y - this.OldPoint.Y);
                base.Location = new Point(base.Location.X + point2.X, base.Location.Y + point2.Y);
                if (base.Parent != null)
                {
                    Point p = base.Parent.PointToClient(Cursor.Position);
                    this.owner.ButtonLocation(p);
                }
                this.owner.PageRunToPoint(Cursor.Position);
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (((e.Button == MouseButtons.Left) && this.IconEdit) && (!this.IsPointClose() && !this.IsClickChange()))
            {
                this.ButtonMove = false;
                this.timeSum = 0;
                this.IconEdit = false;
                this.BackColor = this.MetorColor;
                if (base.Parent != null)
                {
                    Point p = base.Parent.PointToClient(Cursor.Position);
                    this.owner.ButtonControl(p);
                }
            }
            this.timer1.Stop();
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            StringFormat format = new StringFormat {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            int y = base.Height / 10;
            if (this.Icon != null)
            {
                Rectangle rect = new Rectangle((base.Width - (5 * y)) / 2, y, 5 * y, 5 * y);
                graphics.DrawImage(this.Icon, rect);
            }
            if (!string.IsNullOrEmpty(this.Title))
            {
                Rectangle layoutRectangle = new Rectangle(0, 6 * y, base.Width, y * 4);
                graphics.DrawString(this.Title, new Font("宋体", 9f), new SolidBrush(Color.White), layoutRectangle, format);
            }
            if (this._MouseMove && (this.ClickStyle != ButtonClickStyle.Add))
            {
                graphics.DrawImage(Resources.close, this.rectMsg);
                graphics.DrawImage(Resources.fy, this.rectChange);
            }
            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.rectMsg = new Rectangle(base.Width - 20, 0, 20, 20);
            this.rectChange = new Rectangle(0, base.Height - 20, 20, 20);
            base.OnSizeChanged(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timeSum++;
            if (this.timeSum >= 10)
            {
                this.IconEdit = true;
                this.BackColor = Color.Red;
            }
        }

        public ButtonClickStyle ClickStyle
        {
            get
            {
                return this._ClickStyle;
            }
            set
            {
                this._ClickStyle = value;
            }
        }

        public string ClickText
        {
            get
            {
                return this._clickText;
            }
            set
            {
                this._clickText = value;
            }
        }

        public int DockColumn
        {
            get
            {
                return this._DockColumn;
            }
            set
            {
                if (((this._DockColumn != value) && (this._DockColumn > 0)) && (this._DockColumn < 3))
                {
                    this._DockColumn = value;
                }
            }
        }

        public int DockRow
        {
            get
            {
                return this._DockRow;
            }
            set
            {
                if (((this._DockRow != value) && (value >= 0)) && (value < 3))
                {
                    this._DockRow = value;
                }
            }
        }

        public Image Icon
        {
            get
            {
                return this._icon;
            }
            set
            {
                if ((value != null) && (this._icon != value))
                {
                    this._icon = value;
                    base.Invalidate();
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
                    base.Invalidate();
                }
            }
        }

        public string IconName
        {
            get
            {
                return this._IconName;
            }
            set
            {
                this._IconName = value;
            }
        }

        public Color MetorColor
        {
            get
            {
                return this._MetorColor;
            }
            set
            {
                if (this._MetorColor != value)
                {
                    this._MetorColor = value;
                    this.BackColor = value;
                    base.Invalidate();
                }
            }
        }

        public Color MoveColor
        {
            get
            {
                return this._MoveColor;
            }
            set
            {
                if (this._MoveColor != value)
                {
                    this._MoveColor = value;
                }
            }
        }

        public int MsgNum
        {
            get
            {
                return this._MsgNum;
            }
            set
            {
                if (this._MsgNum != value)
                {
                    this._MsgNum = value;
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
                if (value != this._title)
                {
                    this._title = value;
                    this.tool_tag.SetToolTip(this, this.Title);
                    base.Invalidate();
                }
            }
        }
    }
}

