namespace Aisino.Fwkp.HomePage.AisinoDock
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.HomePage.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PageControl : UserControl
    {
        private bool _IsEdit;
        private Color _metorColor = Color.FromArgb(0xbf, 0xcf, 0xd8);
        private Rectangle addRect = Rectangle.Empty;
        private int Border = 2;
        private IContainer components;
        public List<IDock> Docks = new List<IDock>();
        private int H = 10;
        private int headControls = 2;
        private AisinoPNL panControl;
        private AisinoPNL panDock;
        private Rectangle rectEdit = new Rectangle();
        private VScrollBar vBar;

        public PageControl()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            this.HeadControls = Common.HeadControl;
            this.panDock.Paint += new PaintEventHandler(this.panDock_Paint);
            this.panDock.Click += new EventHandler(this.panDock_Click);
            this.panControl.SizeChanged += new EventHandler(this.panControl_SizeChanged);
        }

        public void AddDock(IDock dock)
        {
            if ((dock != null) && !this.DockContains(dock.GetType()))
            {
                dock.Width = Common.HeadWidth;
                dock.Height = Common.HeadHeight;
                int[] numArray = new int[2];
                foreach (IDock dock2 in this.Docks)
                {
                    if (dock2.ColumnsValue == 1)
                    {
                        numArray[1]++;
                    }
                    else
                    {
                        numArray[0]++;
                    }
                }
                if (numArray[1] < numArray[0])
                {
                    dock.ColumnsValue = 1;
                }
                this.Docks.Add(dock);
                this.panDock.Controls.Add(dock);
                this.panDock.Invalidate();
                this.DockSort();
            }
        }

        public void DeleteDock(IDock dock)
        {
            if (dock != null)
            {
                this.Docks.Remove(dock);
                this.panDock.Controls.Remove(dock);
                this.panDock.Invalidate();
                this.DockSort();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DockContains(System.Type type)
        {
            foreach (IDock dock in this.Docks)
            {
                if (dock.GetType() == type)
                {
                    return true;
                }
            }
            return false;
        }

        public bool DockExists(string DockName)
        {
            foreach (IDock dock in this.Docks)
            {
                if (dock.GetType().Name == DockName)
                {
                    return true;
                }
            }
            return false;
        }

        public void DockSort()
        {
            int[] numArray = new int[this.headControls];
            int num = 0;
            foreach (IDock dock in this.Docks)
            {
                dock.Location = new Point((dock.ColumnsValue * Common.HeadWidth) + this.Border, numArray[dock.ColumnsValue]);
                dock.Width = Common.HeadWidth - 10;
                numArray[dock.ColumnsValue] += dock.Height + this.H;
                if (numArray[dock.ColumnsValue] > num)
                {
                    num = numArray[dock.ColumnsValue];
                }
            }
            this.panDock.Invalidate();
            if (num > (this.panControl.Height + 0x17))
            {
                this.panDock.Height = num;
                this.vBar.Minimum = 0;
                this.vBar.Maximum = num - this.panControl.Height;
                this.vBar.Visible = true;
            }
            else
            {
                this.panDock.Height = this.panControl.Height;
                this.vBar.Visible = false;
            }
            this.panDock.Location = Point.Empty;
        }

        public void init(string username)
        {
            try
            {
                Common.UserName = username;
                new PageXml().LoadDock(username, this);
                this.DockSort();
            }
            catch
            {
            }
        }

        private void InitializeComponent()
        {
            this.panControl = new AisinoPNL();
            this.panDock = new AisinoPNL();
            this.vBar = new VScrollBar();
            this.panControl.SuspendLayout();
            base.SuspendLayout();
            this.panControl.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.panControl.BackColor = Color.White;
            this.panControl.Controls.Add(this.panDock);
            this.panControl.Location = new Point(3, 3);
            this.panControl.Name = "panControl";
            this.panControl.Size = new Size(0x1e7, 0x20c);
            this.panControl.TabIndex = 2;
            this.panDock.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panDock.Location = new Point(3, 4);
            this.panDock.Name = "panDock";
            this.panDock.Size = new Size(0x1e1, 0x205);
            this.panDock.TabIndex = 0;
            this.vBar.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            this.vBar.Location = new Point(0x1df, 3);
            this.vBar.Name = "vBar";
            this.vBar.Size = new Size(20, 0x205);
            this.vBar.TabIndex = 3;
            this.vBar.Scroll += new ScrollEventHandler(this.vBar_Scroll);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.Controls.Add(this.vBar);
            base.Controls.Add(this.panControl);
            base.Name = "PageControl";
            base.Size = new Size(500, 530);
            this.panControl.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void InitTable()
        {
            int num = (this.panControl.Width - (2 * this.Border)) / 2;
            Common.HeadWidth = num;
            int num2 = Screen.PrimaryScreen.Bounds.Height / 320;
            int num3 = ((this.panControl.Height - 15) - (num2 * this.Border)) / num2;
            Common.HeadHeight = num3;
            foreach (IDock dock in this.Docks)
            {
                dock.Width = num;
                dock.Height = num3;
            }
            this.DockSort();
        }

        public void menuClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            string dockName = item.Tag.ToString();
            if (!item.Checked)
            {
                if (this.DockExists(dockName))
                {
                    return;
                }
                Common.DockFactory(dockName, this);
                this.DockSort();
            }
            else
            {
                foreach (IDock dock in this.Docks)
                {
                    if (dock.GetType().Name == dockName)
                    {
                        this.DeleteDock(dock);
                        break;
                    }
                }
            }
            item.Checked = !item.Checked;
            new PageXml().SaveDock(Common.UserName, this.Docks);
        }

        private void panControl_SizeChanged(object sender, EventArgs e)
        {
            this.InitTable();
        }

        private void panDock_Click(object sender, EventArgs e)
        {
            if (this.rectEdit != Rectangle.Empty)
            {
                Point pt = this.panDock.PointToClient(Cursor.Position);
                if (this.rectEdit.Contains(pt))
                {
                    Common.MenuDockManager(Cursor.Position, this, new EventHandler(this.menuClick));
                }
            }
        }

        private void panDock_Paint(object sender, PaintEventArgs e)
        {
            if (this.IsEdit)
            {
                new Pen(this.MetorColor, 1f);
                e.Graphics.FillRectangle(new SolidBrush(this.MetorColor), this.rectEdit);
            }
            if (this.Docks.Count == 0)
            {
                int x = (this.panDock.Width / 2) - 0x20;
                int y = (this.panDock.Height / 2) - 0x20;
                this.rectEdit = new Rectangle(x, y, 0x40, 0x40);
                e.Graphics.DrawImage(Resources.addIcon, this.rectEdit);
            }
            else
            {
                this.rectEdit = Rectangle.Empty;
            }
            base.OnPaint(e);
        }

        public void SetControl(Point p, IDock dock)
        {
            int index = p.X / Common.HeadWidth;
            if (index < 0)
            {
                index = 0;
            }
            if (index >= this.headControls)
            {
                index = this.headControls - 1;
            }
            if (!this.rectEdit.Contains(p))
            {
                int[] numArray = new int[this.headControls];
                for (int i = 0; i < this.headControls; i++)
                {
                    numArray[i] = this.Border;
                }
                int num3 = 0;
                this.rectEdit = Rectangle.Empty;
                foreach (IDock dock2 in this.Docks)
                {
                    if (!dock.Equals(dock2))
                    {
                        if (((p.Y > numArray[dock2.ColumnsValue]) && (p.Y < (numArray[dock2.ColumnsValue] + dock2.Height))) && (index == dock2.ColumnsValue))
                        {
                            this.rectEdit = new Rectangle((dock2.ColumnsValue * Common.HeadWidth) + this.Border, numArray[dock2.ColumnsValue], dock.Width, dock.Height);
                            numArray[dock2.ColumnsValue] += dock.Height + this.H;
                        }
                        new Point(dock2.ColumnsValue * Common.HeadWidth, numArray[dock2.ColumnsValue]);
                        dock2.Location = new Point((dock2.ColumnsValue * Common.HeadWidth) + this.Border, numArray[dock2.ColumnsValue]);
                        numArray[dock2.ColumnsValue] += dock2.Height + this.H;
                        if (numArray[dock2.ColumnsValue] > num3)
                        {
                            num3 = numArray[dock2.ColumnsValue];
                        }
                    }
                }
                if (this.rectEdit == Rectangle.Empty)
                {
                    this.rectEdit = new Rectangle((index * Common.HeadWidth) + this.Border, numArray[index], dock.Width, dock.Height);
                }
                int height = base.Height;
                this.panDock.Invalidate();
            }
        }

        public void SetLocaion(Point p, IDock dock)
        {
            int index = p.X / Common.HeadWidth;
            if (index < 0)
            {
                index = 0;
            }
            if (index >= this.headControls)
            {
                index = this.headControls - 1;
            }
            int[] numArray = new int[this.headControls];
            for (int i = 0; i < this.headControls; i++)
            {
                numArray[i] = this.Border;
            }
            int num3 = 0;
            this.rectEdit = Rectangle.Empty;
            int num4 = -1;
            for (int j = 0; j < this.Docks.Count; j++)
            {
                IDock dock2 = this.Docks[j];
                if (!dock.Equals(dock2))
                {
                    if (((p.Y > numArray[dock2.ColumnsValue]) && (p.Y < (numArray[dock2.ColumnsValue] + dock2.Height))) && (index == dock2.ColumnsValue))
                    {
                        this.rectEdit = new Rectangle((dock2.ColumnsValue * Common.HeadWidth) + this.Border, numArray[dock2.ColumnsValue], dock.Width, dock.Height);
                        numArray[dock2.ColumnsValue] += dock.Height + this.H;
                        if (this.Docks.Contains(dock))
                        {
                            this.Docks.Remove(dock);
                            dock.ColumnsValue = index;
                            if ((num4 > -1) && (num4 < j))
                            {
                                this.Docks.Insert(j - 1, dock);
                            }
                            else
                            {
                                this.Docks.Insert(j, dock);
                            }
                        }
                    }
                    dock2.Location = new Point((dock2.ColumnsValue * Common.HeadWidth) + this.Border, numArray[dock2.ColumnsValue]);
                    numArray[dock2.ColumnsValue] += dock2.Height + this.H;
                    if (numArray[dock2.ColumnsValue] > num3)
                    {
                        num3 = numArray[dock2.ColumnsValue];
                    }
                }
                else
                {
                    num4 = j;
                }
            }
            if (this.rectEdit == Rectangle.Empty)
            {
                this.rectEdit = new Rectangle((index * Common.HeadWidth) + this.Border, numArray[index], dock.Width, dock.Height);
                if (this.Docks.Contains(dock))
                {
                    this.Docks.Remove(dock);
                    dock.ColumnsValue = index;
                    this.Docks.Add(dock);
                }
            }
            new PageXml().SaveDock(Common.UserName, this.Docks);
            this.DockSort();
            int height = base.Height;
            this.panDock.Invalidate();
        }

        private void skinColor_OnSelectSkin(Color color)
        {
            this.MetorColor = color;
        }

        private void vBar_Scroll(object sender, ScrollEventArgs e)
        {
            int newValue = e.NewValue;
            this.panDock.Location = new Point(this.panDock.Location.X, -newValue);
        }

        [DefaultValue(2)]
        public int HeadControls
        {
            get
            {
                return this.headControls;
            }
            set
            {
                this.headControls = value;
            }
        }

        public bool IsEdit
        {
            get
            {
                return this._IsEdit;
            }
            set
            {
                if (value != this._IsEdit)
                {
                    this._IsEdit = value;
                    foreach (IDock dock in this.Docks)
                    {
                        dock.IsEdit = value;
                    }
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
                    foreach (IDock dock in this.Docks)
                    {
                        dock.MetorColor = value;
                    }
                }
            }
        }
    }
}

