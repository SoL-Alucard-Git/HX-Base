namespace Aisino.Framework.Plugin.Core.Controls
{
    using ns8;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;

    public class AisinoMultiCombox : Control
    {
        public AisinoComboxColumns _Columns;
        private AisinoComboxBorderStyle aisinoComboxBorderStyle_0;
        private AisinoComboxBorderStyle aisinoComboxBorderStyle_1;
        private AisinoPIC aisinoPIC_0;
        private AutoComplateStyle autoComplateStyle_0;
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private bool bool_3;
        private ButtonStyle buttonStyle_0;
        private Class115 class115_0;
        private Class116 class116_0;
        private Color color_0;
        private Color color_1;
        private Control2 control2_0;
        private DataTable dataTable_0;
        private DataTable dataTable_1;
        private Dictionary<string, string> dictionary_0;
        private EditStyle editStyle_0;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private int int_0;
        private int int_1;
        public EventHandler OnTextChanged;
        private string string_0;
        public static Color SystemBorderColor;

        public event EventHandler OnAutoComplate;

        public event EventHandler OnButtonClick;

        public event EventHandler OnSelectValue;

        static AisinoMultiCombox()
        {
            
            SystemBorderColor = Color.FromArgb(0x7f, 0x9d, 0xb9);
        }

        public AisinoMultiCombox()
        {
            
            this.editStyle_0 = EditStyle.TextBox;
            this.buttonStyle_0 = ButtonStyle.Select;
            this.bool_0 = true;
            this.int_0 = 8;
            this.string_0 = string.Empty;
            this.int_1 = 1;
            this.dictionary_0 = new Dictionary<string, string>();
            this.aisinoComboxBorderStyle_0 = AisinoComboxBorderStyle.System;
            this.color_0 = SystemBorderColor;
            this.color_1 = SystemBorderColor;
            this.bool_2 = true;
            this.method_1();
            base.SetStyle(ControlStyles.FixedHeight, false);
            base.SetStyle(ControlStyles.FixedWidth, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.Selectable, false);
            base.Height = this.class115_0.Height + 7;
            base.VisibleChanged += new EventHandler(this.AisinoMultiCombox_VisibleChanged);
            base.Resize += new EventHandler(this.AisinoMultiCombox_Resize);
        }

        private void AisinoMultiCombox_Resize(object sender, EventArgs e)
        {
            if (this.Edit != EditStyle.Label)
            {
                this.class115_0.Width = base.Width - 2;
            }
        }

        private void AisinoMultiCombox_VisibleChanged(object sender, EventArgs e)
        {
            if (!base.Visible)
            {
                this.control2_0.method_7(false);
                if (base.Parent != null)
                {
                    base.Parent.Focus();
                }
            }
        }

        private void aisinoPIC_0_Click(object sender, EventArgs e)
        {
            this.class115_0.Focus();
            if (this.buttonStyle == ButtonStyle.Button)
            {
                if (this.OnButtonClick != null)
                {
                    this.OnButtonClick(this, null);
                }
            }
            else if (this.buttonStyle == ButtonStyle.Select)
            {
                if (this.control2_0.method_6())
                {
                    this.control2_0.method_7(false);
                }
                else
                {
                    this.Data = this.DataSource;
                    this.control2_0.method_7(true);
                }
            }
        }

        private void aisinoPIC_0_MouseEnter(object sender, EventArgs e)
        {
        }

        private void aisinoPIC_0_MouseLeave(object sender, EventArgs e)
        {
            if (this.ButtonAutoHide)
            {
                Point position = Cursor.Position;
                Rectangle rectangle = this.class115_0.RectangleToScreen(this.class115_0.ClientRectangle);
                if (((position.X < rectangle.X) || (position.X > (rectangle.X + rectangle.Width))) || ((position.Y < rectangle.Y) || (position.Y > (rectangle.Y + rectangle.Height))))
                {
                    this.aisinoPIC_0.Visible = false;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool Focus()
        {
            if (this.bool_3)
            {
                return base.Focus();
            }
            if (base.Parent is DataGridView)
            {
                DataGridView parent = base.Parent as DataGridView;
                if (parent != null)
                {
                    parent.EndEdit();
                }
            }
            return this.class115_0.Focus();
        }

        private void method_0()
        {
            if (this._Columns != null)
            {
                int num = 0;
                foreach (AisinoComboxColumn column in this._Columns)
                {
                    if (column.Visible)
                    {
                        num += column.Width;
                    }
                }
                float num2 = 0f;
                if (num < base.Width)
                {
                    int num3 = base.Width - num;
                    if (num3 > 0)
                    {
                        num2 = ((float) num3) / ((float) num);
                    }
                }
                foreach (AisinoComboxColumn column2 in this._Columns)
                {
                    if (column2.Visible)
                    {
                        column2.int_1 = (int) (column2.Width * (1f + num2));
                    }
                    else
                    {
                        column2.int_1 = 0;
                    }
                }
                this.control2_0.Width = (num > base.Width) ? num : base.Width;
            }
        }

        private void method_1()
        {
            this.icontainer_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AisinoMultiCombox));
            this.imageList_0 = new ImageList(this.icontainer_0);
            base.SuspendLayout();
            base.Height = 20;
            base.Width = 0x7a;
            this.aisinoPIC_0 = new AisinoPIC();
            this.class115_0 = new Class115(this);
            this.control2_0 = new Control2(this);
            this.imageList_0.ImageStream = (ImageListStreamer) manager.GetObject("img.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "Select");
            this.imageList_0.Images.SetKeyName(1, "Button");
            base.ResumeLayout(false);
            this.aisinoPIC_0.Width = 20;
            this.aisinoPIC_0.Height = 0x10;
            this.aisinoPIC_0.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            this.aisinoPIC_0.Location = new Point((base.Width - this.aisinoPIC_0.Width) - 2, (base.Height - this.aisinoPIC_0.Height) / 2);
            this.aisinoPIC_0.Image = this.imageList_0.Images["Select"];
            this.aisinoPIC_0.Click += new EventHandler(this.aisinoPIC_0_Click);
            this.aisinoPIC_0.MouseLeave += new EventHandler(this.aisinoPIC_0_MouseLeave);
            this.aisinoPIC_0.MouseEnter += new EventHandler(this.aisinoPIC_0_MouseEnter);
            this.aisinoPIC_0.Visible = !this.bool_2;
            base.Controls.Add(this.aisinoPIC_0);
            this.class115_0.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.class115_0.Location = new Point(1, 4);
            this.class115_0.Width = base.Width - 2;
            base.Controls.Add(this.class115_0);
            this.aisinoPIC_0.BackColor = this.class115_0.BackColor;
            base.Controls.Add(this.control2_0);
            this._Columns = new AisinoComboxColumns(this);
        }

        protected override void OnClick(EventArgs e)
        {
            if (this.bool_3 && (this.Edit == EditStyle.TextBox))
            {
                this.control2_0.method_7(true);
            }
            base.OnClick(e);
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            base.Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (this.editStyle_0 == EditStyle.Label)
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyPress(e);
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.control2_0.method_9())
            {
                int num = e.Delta / 120;
                this.control2_0.method_8(num);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.Height = this.class115_0.Height + 7;
            if (this.Edit == EditStyle.Label)
            {
                if (base.Parent != null)
                {
                    StringFormat format = new StringFormat {
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisWord
                    };
                    Rectangle rect = new Rectangle(4, 3, base.Width - 4, base.Height - 7);
                    e.Graphics.FillRectangle(new SolidBrush(base.Parent.BackColor), rect);
                    e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), rect, format);
                }
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(this.class115_0.BackColor), e.ClipRectangle);
                Pen pen = new Pen(this.color_0, 1f);
                if (this.aisinoComboxBorderStyle_0 == AisinoComboxBorderStyle.System)
                {
                    e.Graphics.DrawLine(pen, new Point(0, 0), new Point(base.Width - 1, 0));
                    e.Graphics.DrawLine(pen, new Point(0, 0), new Point(0, base.Height - 1));
                    e.Graphics.DrawLine(pen, new Point(base.Width - 1, 0), new Point(base.Width - 1, base.Height - 1));
                    if (this.aisinoComboxBorderStyle_1 != AisinoComboxBorderStyle.System)
                    {
                        e.Graphics.DrawLine(pen, new Point(0, base.Height - 1), new Point(base.Width - 1, base.Height - 1));
                    }
                }
                if (this.aisinoComboxBorderStyle_1 == AisinoComboxBorderStyle.System)
                {
                    pen = new Pen(this.color_1, 1f);
                    e.Graphics.DrawLine(pen, new Point(0, base.Height - 1), new Point(base.Width - 1, base.Height - 1));
                }
            }
        }

        public void Select(int int_2, int int_3)
        {
            this.class115_0.Select(int_2, int_3);
        }

        [Category("设置"), Description("自动提示类型 分为自动System 手动HeadWork 或不提示None")]
        public AutoComplateStyle AutoComplate
        {
            get
            {
                return this.autoComplateStyle_0;
            }
            set
            {
                this.autoComplateStyle_0 = value;
            }
        }

        [Description("输入多少个字符的时候开始提示"), Category("设置")]
        public int AutoIndex
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

        [Category("样式"), Description("边框颜色")]
        public Color BorderColor
        {
            get
            {
                if (this.color_0 == SystemBorderColor)
                {
                    return Color.Transparent;
                }
                return this.color_0;
            }
            set
            {
                if (value == Color.Transparent)
                {
                    this.color_0 = SystemBorderColor;
                }
                else
                {
                    this.color_0 = value;
                }
            }
        }

        [Description("边框状态 有显示边框和不显示边框"), Category("样式")]
        public AisinoComboxBorderStyle BorderStyle
        {
            get
            {
                return this.aisinoComboxBorderStyle_0;
            }
            set
            {
                this.aisinoComboxBorderStyle_0 = value;
            }
        }

        [Category("设置"), Description("按钮是否自动隐藏")]
        public bool ButtonAutoHide
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                if ((this.Edit == EditStyle.TextBox) && (this.bool_2 != value))
                {
                    this.bool_2 = value;
                    if (value)
                    {
                        this.aisinoPIC_0.Visible = this.class115_0.Focused;
                    }
                    else
                    {
                        this.aisinoPIC_0.Visible = true;
                    }
                }
            }
        }

        [Description("选择按钮状态 有Button和Select状态"), Category("设置")]
        public ButtonStyle buttonStyle
        {
            get
            {
                return this.buttonStyle_0;
            }
            set
            {
                if (this.buttonStyle_0 != value)
                {
                    this.buttonStyle_0 = value;
                    if (this.buttonStyle_0 == ButtonStyle.Select)
                    {
                        this.aisinoPIC_0.Image = this.imageList_0.Images["Select"];
                    }
                    else
                    {
                        this.aisinoPIC_0.Image = this.imageList_0.Images["Button"];
                    }
                }
            }
        }

        [Description("数据列"), Category("设置")]
        public AisinoComboxColumns Columns
        {
            get
            {
                return this._Columns;
            }
            set
            {
                this._Columns = value;
            }
        }

        private DataTable Data
        {
            get
            {
                return this.dataTable_0;
            }
            set
            {
                this.dataTable_0 = value;
                if (this.dataTable_0 != null)
                {
                    this.control2_0.method_1(this.dataTable_0.Rows.Count);
                    this.control2_0.method_11(this.dataTable_0.Rows.Count - this.MaxIndex);
                    this.control2_0.method_15();
                    this.control2_0.Invalidate();
                }
            }
        }

        [Category("设置"), Description("数据绑定 ，数据类型为DataTable")]
        public DataTable DataSource
        {
            get
            {
                return this.dataTable_1;
            }
            set
            {
                this.dataTable_1 = value;
                switch (this.autoComplateStyle_0)
                {
                    case AutoComplateStyle.None:
                    case AutoComplateStyle.HeadWork:
                        this.Data = this.dataTable_1;
                        return;

                    case AutoComplateStyle.System:
                        this.class116_0 = new Class116(value);
                        this.Data = this.class116_0.method_1(string.Empty);
                        return;
                }
            }
        }

        [Description("是否显示标题"), Category("设置")]
        public bool DrawHead
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        [Category("设置"), Description("当前编辑状态 有Label状态和Combobox状态 ")]
        public EditStyle Edit
        {
            get
            {
                return this.editStyle_0;
            }
            set
            {
                if (this.editStyle_0 != value)
                {
                    this.editStyle_0 = value;
                    if (value == EditStyle.Label)
                    {
                        this.ReadOnly = false;
                        this.aisinoPIC_0.Visible = false;
                        this.class115_0.Width = 0;
                        this.control2_0.method_7(false);
                        this.bool_2 = false;
                        this.aisinoComboxBorderStyle_0 = AisinoComboxBorderStyle.None;
                        base.SetStyle(ControlStyles.Selectable, false);
                        base.Enabled = false;
                        base.TabStop = false;
                        base.Invalidate();
                    }
                    else
                    {
                        this.class115_0.Width = base.Width - 2;
                        this.bool_2 = true;
                        this.aisinoComboxBorderStyle_0 = AisinoComboxBorderStyle.System;
                        base.SetStyle(ControlStyles.Selectable, true);
                        base.TabStop = true;
                        base.Enabled = true;
                        base.Invalidate();
                    }
                }
            }
        }

        [Category("设置"), Description("获取当前控件是否得到焦点")]
        public override bool Focused
        {
            get
            {
                if (this.bool_3)
                {
                    return base.Focused;
                }
                return this.class115_0.Focused;
            }
        }

        [Category("设置"), Description("在光标进入时是否全选 ")]
        public bool IsSelectAll
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        [Category("设置"), Description("最多显示多少项")]
        public int MaxIndex
        {
            get
            {
                return this.int_0;
            }
            set
            {
                if (value < 0)
                {
                    value = 10;
                }
                this.int_0 = value;
            }
        }

        [Description("设置最大文本长度"), Category("设置")]
        public int MaxLength
        {
            get
            {
                return this.class115_0.MaxLength;
            }
            set
            {
                this.class115_0.MaxLength = value;
            }
        }

        [Description("文本框是否只读 "), Category("设置")]
        public bool ReadOnly
        {
            get
            {
                return this.bool_3;
            }
            set
            {
                if (this.Edit != EditStyle.Label)
                {
                    this.bool_3 = value;
                    if (this.bool_3)
                    {
                        this.ButtonAutoHide = false;
                        base.Invalidate();
                    }
                    else
                    {
                        this.class115_0.Visible = true;
                        base.Invalidate();
                    }
                }
            }
        }

        [Description("选择数据后得到的数据键值"), Category("设置")]
        public Dictionary<string, string> SelectDict
        {
            get
            {
                return this.dictionary_0;
            }
        }

        [Category("设置"), Description("获取或设置当前选择的项")]
        public int SelectedIndex
        {
            get
            {
                return this.control2_0.method_2();
            }
            set
            {
                this.control2_0.method_3(value);
            }
        }

        public int SelectionStart
        {
            get
            {
                return this.class115_0.SelectionStart;
            }
            set
            {
                this.class115_0.SelectionStart = value;
            }
        }

        [Description("要显示的数据列，值为列值"), Category("设置")]
        public string ShowText
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

        [Description("当前文本信息"), Category("设置")]
        public override string Text
        {
            get
            {
                return this.class115_0.Text;
            }
            set
            {
                AutoComplateStyle style = this.autoComplateStyle_0;
                this.autoComplateStyle_0 = AutoComplateStyle.None;
                this.class115_0.Text = value;
                this.autoComplateStyle_0 = style;
            }
        }

        [Description("下划线颜色"), Category("样式")]
        public Color UnderLineColor
        {
            get
            {
                if (this.color_1 == SystemBorderColor)
                {
                    return Color.Transparent;
                }
                return this.color_1;
            }
            set
            {
                if (value == Color.Transparent)
                {
                    this.color_1 = SystemBorderColor;
                }
                else
                {
                    this.color_1 = value;
                }
            }
        }

        [Description("下划线状态"), Category("样式")]
        public AisinoComboxBorderStyle UnderLineStyle
        {
            get
            {
                return this.aisinoComboxBorderStyle_1;
            }
            set
            {
                this.aisinoComboxBorderStyle_1 = value;
            }
        }

        public class AisinoComboxColumn
        {
            private bool bool_0;
            private int int_0;
            internal int int_1;
            private string string_0;
            private string string_1;

            public AisinoComboxColumn(string string_2, string string_3)
            {
                
                this.string_0 = string.Empty;
                this.string_1 = string.Empty;
                this.bool_0 = true;
                this.int_0 = 100;
                this.int_1 = 100;
                this.string_0 = string_2;
                this.string_1 = string_3;
            }

            public AisinoComboxColumn(string string_2, string string_3, int int_2) : this(string_2, string_3)
            {
                
                this.int_0 = int_2;
            }

            public string DataPropertyName
            {
                get
                {
                    return this.string_1;
                }
                set
                {
                    this.string_1 = value;
                }
            }

            public string HeaderText
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

            public bool Visible
            {
                get
                {
                    return this.bool_0;
                }
                set
                {
                    this.bool_0 = value;
                }
            }

            public int Width
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

        public class AisinoComboxColumns
        {
            private AisinoMultiCombox aisinoMultiCombox_0;
            private List<AisinoMultiCombox.AisinoComboxColumn> list_0;

            public AisinoComboxColumns(AisinoMultiCombox aisinoMultiCombox_1)
            {
                
                this.list_0 = new List<AisinoMultiCombox.AisinoComboxColumn>();
                this.aisinoMultiCombox_0 = aisinoMultiCombox_1;
                this.aisinoMultiCombox_0.method_0();
            }

            public void Add(AisinoMultiCombox.AisinoComboxColumn aisinoComboxColumn_0)
            {
                this.list_0.Add(aisinoComboxColumn_0);
                this.aisinoMultiCombox_0.method_0();
            }

            public void Clear()
            {
                this.list_0.Clear();
                this.aisinoMultiCombox_0.method_0();
            }

            public bool Contains(AisinoMultiCombox.AisinoComboxColumn aisinoComboxColumn_0)
            {
                return this.list_0.Contains(aisinoComboxColumn_0);
            }

            public void CopyTo(AisinoMultiCombox.AisinoComboxColumn[] aisinoComboxColumn_0, int int_0)
            {
                this.list_0.CopyTo(aisinoComboxColumn_0, int_0);
            }

            public IEnumerator GetEnumerator()
            {
                return this.list_0.GetEnumerator();
            }

            public int IndexOf(AisinoMultiCombox.AisinoComboxColumn aisinoComboxColumn_0)
            {
                return this.list_0.IndexOf(aisinoComboxColumn_0);
            }

            public void Insert(int int_0, AisinoMultiCombox.AisinoComboxColumn aisinoComboxColumn_0)
            {
                this.list_0.IndexOf(aisinoComboxColumn_0, int_0);
                this.aisinoMultiCombox_0.method_0();
            }

            public void Remove(AisinoMultiCombox.AisinoComboxColumn aisinoComboxColumn_0)
            {
                this.list_0.Remove(aisinoComboxColumn_0);
                this.aisinoMultiCombox_0.method_0();
            }

            public void RemoveAt(int int_0)
            {
                this.list_0.RemoveAt(int_0);
                this.aisinoMultiCombox_0.method_0();
            }

            public int Count
            {
                get
                {
                    return this.list_0.Count;
                }
            }

            public AisinoMultiCombox.AisinoComboxColumn this[int int_0]
            {
                get
                {
                    return this.list_0[int_0];
                }
                set
                {
                    this.list_0[int_0] = value;
                }
            }
        }

        private class Class115 : AisinoTXT
        {
            private bool bool_0;
            private AisinoMultiCombox object_0;

            public Class115(AisinoMultiCombox aisinoMultiCombox_0)
            {
                
                this.object_0 = aisinoMultiCombox_0;
            }

            protected override void OnGotFocus(EventArgs e)
            {
                base.OnGotFocus(e);
                if (this.object_0.IsSelectAll)
                {
                    base.SelectAll();
                    this.bool_0 = true;
                }
                if (this.object_0.ButtonAutoHide)
                {
                    this.object_0.aisinoPIC_0.Visible = true;
                }
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                this.object_0.OnKeyDown(e);
                base.OnKeyDown(e);
                Console.WriteLine("OnKeyDown");
            }

            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                this.object_0.OnKeyPress(e);
                base.OnKeyPress(e);
                Console.WriteLine("OnKeyPress");
            }

            protected override void OnKeyUp(KeyEventArgs e)
            {
                this.object_0.OnKeyUp(e);
                base.OnKeyUp(e);
                Console.WriteLine("OnKeyUp");
            }

            protected override void OnLostFocus(EventArgs e)
            {
                base.OnLostFocus(e);
                if (this.object_0.ButtonAutoHide)
                {
                    this.object_0.aisinoPIC_0.Visible = false;
                }
            }

            protected override void OnMouseClick(MouseEventArgs e)
            {
                if (this.object_0.IsSelectAll && this.bool_0)
                {
                    base.SelectAll();
                    this.bool_0 = false;
                }
                base.OnMouseClick(e);
            }

            protected override void OnMouseDoubleClick(MouseEventArgs e)
            {
                this.object_0.OnMouseDoubleClick(e);
                base.OnMouseDoubleClick(e);
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                if (this.object_0.ButtonAutoHide)
                {
                    this.object_0.aisinoPIC_0.Visible = true;
                }
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                if (this.object_0.ButtonAutoHide)
                {
                    Point position = Cursor.Position;
                    Rectangle rectangle = this.object_0.aisinoPIC_0.RectangleToScreen(this.object_0.aisinoPIC_0.ClientRectangle);
                    if (((position.X < rectangle.X) || (position.X > (rectangle.X + rectangle.Width))) || ((position.Y < rectangle.Y) || (position.Y > (rectangle.Y + rectangle.Height))))
                    {
                        this.object_0.aisinoPIC_0.Visible = false;
                    }
                }
            }

            protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
            {
                if (this.object_0.control2_0.method_6())
                {
                    if (this.object_0.dataTable_0 == null)
                    {
                        return;
                    }
                    int num = this.object_0.control2_0.method_4();
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            if (num >= 0)
                            {
                                num--;
                                if (num < 0)
                                {
                                    num = 0;
                                }
                                if (num >= this.object_0.dataTable_0.Rows.Count)
                                {
                                    num = this.object_0.dataTable_0.Rows.Count - 1;
                                }
                                this.object_0.control2_0.method_5(num);
                                break;
                            }
                            return;

                        case Keys.Down:
                            if (this.object_0.dataTable_0 != null)
                            {
                                num++;
                                if (num < 0)
                                {
                                    num = 0;
                                }
                                if (num >= this.object_0.dataTable_0.Rows.Count)
                                {
                                    num = this.object_0.dataTable_0.Rows.Count - 1;
                                }
                            }
                            this.object_0.control2_0.method_5(num);
                            break;

                        case Keys.Enter:
                            if (((num >= 0) && (num <= this.object_0.dataTable_0.Rows.Count)) && this.object_0.control2_0.method_6())
                            {
                                this.object_0.control2_0.method_3(num);
                                this.object_0.control2_0.method_7(false);
                            }
                            break;
                    }
                }
                this.object_0.OnPreviewKeyDown(e);
                base.OnPreviewKeyDown(e);
                Console.WriteLine("PreviewKeyDown");
            }

            protected override void OnTextChanged(EventArgs e)
            {
                if (this.object_0.Edit == EditStyle.TextBox)
                {
                    if (this.Text.Length > this.object_0.int_1)
                    {
                        if (this.object_0.autoComplateStyle_0 == AutoComplateStyle.HeadWork)
                        {
                            if (this.object_0.OnAutoComplate != null)
                            {
                                this.object_0.OnAutoComplate(this.object_0, null);
                            }
                            if (((this.object_0.Data != null) && (this.object_0.Data.Rows.Count > 0)) && (this.object_0.control2_0.method_0() > 0))
                            {
                                this.object_0.control2_0.method_7(true);
                            }
                        }
                        if (this.object_0.autoComplateStyle_0 == AutoComplateStyle.System)
                        {
                            this.object_0.Data = this.object_0.class116_0.method_1(this.Text);
                            if (((this.object_0.Data != null) && (this.object_0.Data.Rows.Count > 0)) && (this.object_0.control2_0.method_0() > 0))
                            {
                                this.object_0.control2_0.method_7(true);
                            }
                        }
                    }
                    else
                    {
                        this.object_0.control2_0.method_7(false);
                    }
                    if (this.object_0.OnTextChanged != null)
                    {
                        this.object_0.OnTextChanged(this, e);
                    }
                }
                this.object_0.Invalidate();
                base.OnTextChanged(e);
            }
        }

        private class Control2 : Control
        {
            private bool bool_0;
            public int int_0;
            private int int_1;
            private int int_2;
            private int int_3;
            private AisinoMultiCombox object_0;
            private Point point_0;
            private VScrollBar vscrollBar_0;

            public Control2(AisinoMultiCombox aisinoMultiCombox_0)
            {
                
                this.int_1 = -1;
                this.int_2 = -1;
                this.point_0 = Point.Empty;
                this.object_0 = aisinoMultiCombox_0;
                base.Width = this.object_0.Width;
                base.Height = this.object_0.Font.Height + 2;
                this.int_0 = this.object_0.Font.Height + 2;
                this.vscrollBar_0 = new VScrollBar();
                base.Controls.Add(this.vscrollBar_0);
                this.vscrollBar_0.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
                this.vscrollBar_0.Visible = false;
                this.vscrollBar_0.SmallChange = 1;
                this.vscrollBar_0.LargeChange = 1;
                this.vscrollBar_0.ValueChanged += new EventHandler(this.vscrollBar_0_ValueChanged);
                base.MouseWheel += new MouseEventHandler(this.Control2_MouseWheel);
                base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.Opaque, true);
                base.SetStyle(ControlStyles.Selectable, false);
                base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            }

            private void Control2_MouseWheel(object sender, MouseEventArgs e)
            {
                if (this.vscrollBar_0.Visible)
                {
                    int num = e.Delta / 120;
                    this.method_8(num);
                }
            }

            public int method_0()
            {
                return this.int_3;
            }

            public void method_1(int int_4)
            {
                this.int_3 = int_4;
            }

            public int method_10()
            {
                return this.vscrollBar_0.Value;
            }

            public void method_11(int int_4)
            {
                if (int_4 <= 0)
                {
                    this.vscrollBar_0.Visible = false;
                }
                else
                {
                    this.vscrollBar_0.Visible = true;
                    this.vscrollBar_0.Minimum = 0;
                    this.vscrollBar_0.Maximum = int_4;
                    this.vscrollBar_0.Value = 0;
                }
            }

            public void method_12(int int_4)
            {
                try
                {
                    if (((this.object_0.dataTable_0 != null) && (int_4 >= 0)) && (int_4 <= this.object_0.dataTable_0.Rows.Count))
                    {
                        AutoComplateStyle style = this.object_0.autoComplateStyle_0;
                        this.object_0.autoComplateStyle_0 = AutoComplateStyle.None;
                        this.object_0.dictionary_0.Clear();
                        foreach (DataColumn column in this.object_0.dataTable_0.Columns)
                        {
                            this.object_0.dictionary_0.Add(column.ColumnName, this.object_0.dataTable_0.Rows[int_4][column].ToString());
                        }
                        if ((this.object_0.ShowText != string.Empty) && this.object_0.dataTable_0.Columns.Contains(this.object_0.ShowText))
                        {
                            this.object_0.Text = this.object_0.dataTable_0.Rows[int_4][this.object_0.ShowText].ToString();
                            this.object_0.Select(0, this.object_0.Text.Length);
                            if (this.object_0.OnSelectValue != null)
                            {
                                this.object_0.OnSelectValue(this.object_0, null);
                            }
                            this.object_0.autoComplateStyle_0 = style;
                        }
                        else
                        {
                            this.object_0.Text = this.object_0.dataTable_0.Rows[int_4][0].ToString();
                            this.object_0.Select(0, this.object_0.Text.Length);
                            if (this.object_0.OnSelectValue != null)
                            {
                                this.object_0.OnSelectValue(this.object_0, null);
                            }
                            this.object_0.autoComplateStyle_0 = style;
                        }
                    }
                }
                catch
                {
                }
            }

            private int method_13(int int_4, int int_5)
            {
                Rectangle rectangle;
                if (this.method_0() > this.object_0.MaxIndex)
                {
                    rectangle = new Rectangle(0, 0, base.Width - this.vscrollBar_0.Width, base.Height);
                }
                else
                {
                    rectangle = new Rectangle(0, 0, base.Width, base.Height);
                }
                if (((int_4 >= 0) && (int_4 <= rectangle.Width)) && ((int_5 >= 0) && (int_5 <= rectangle.Height)))
                {
                    int num = 0;
                    if (this.object_0.bool_0)
                    {
                        num = this.int_0;
                    }
                    int num2 = (int_5 - num) / this.int_0;
                    if (this.object_0.dataTable_0 == null)
                    {
                        return -1;
                    }
                    if (num2 < 0)
                    {
                        return -1;
                    }
                    if (num2 < this.object_0.dataTable_0.Rows.Count)
                    {
                        return num2;
                    }
                }
                return -1;
            }

            public void method_14()
            {
                if (this.object_0.dataTable_0 == null)
                {
                    base.Width = this.object_0.Width;
                }
                Rectangle rectangle = new Rectangle();
                int height = Screen.GetWorkingArea(this).Height;
                Point p = new Point(0, this.object_0.Height);
                p = this.object_0.PointToScreen(p);
                if ((height - p.Y) < base.Height)
                {
                    base.Location = this.object_0.PointToScreen(new Point(0, -base.Height));
                }
                else
                {
                    base.Location = this.object_0.PointToScreen(new Point(0, this.object_0.Height));
                }
                base.Capture = true;
                this.method_15();
                base.Show();
            }

            public void method_15()
            {
                if (this.object_0.DrawHead)
                {
                    this.vscrollBar_0.Location = new Point((base.Width - this.vscrollBar_0.Width) - 1, this.int_0);
                    this.vscrollBar_0.Height = (base.Height - this.int_0) - 1;
                }
                else
                {
                    this.vscrollBar_0.Location = new Point((base.Width - this.vscrollBar_0.Width) - 1, 1);
                    this.vscrollBar_0.Height = base.Height - 2;
                }
                this.int_0 = this.object_0.Height + 2;
                int num = (this.method_0() > this.object_0.MaxIndex) ? this.object_0.MaxIndex : this.method_0();
                if (this.object_0.DrawHead)
                {
                    base.Height = (num + 1) * this.int_0;
                }
                else
                {
                    base.Height = num * this.int_0;
                }
                this.object_0.control2_0.method_3(-1);
                this.object_0.control2_0.method_5(-1);
            }

            public void method_16()
            {
                base.Capture = false;
                base.Hide();
                this.object_0.Focus();
                this.object_0.Invalidate();
            }

            public int method_2()
            {
                return this.int_2;
            }

            public void method_3(int int_4)
            {
                if ((int_4 >= 0) && (int_4 < this.method_0()))
                {
                    this.int_2 = int_4;
                    this.method_5(int_4);
                    this.method_12(this.int_2);
                }
            }

            public int method_4()
            {
                return this.int_1;
            }

            public void method_5(int int_4)
            {
                if (this.int_1 != int_4)
                {
                    this.int_1 = int_4;
                    if (((this.int_1 != -1) && (this.object_0.dataTable_0 != null)) && (this.method_0() >= this.int_1))
                    {
                        if ((this.vscrollBar_0.Value + this.object_0.MaxIndex) <= this.int_1)
                        {
                            int num2 = (this.int_1 - this.object_0.MaxIndex) + 1;
                            if (num2 < 0)
                            {
                                num2 = 0;
                            }
                            this.vscrollBar_0.Value = (num2 > this.vscrollBar_0.Maximum) ? this.vscrollBar_0.Maximum : num2;
                        }
                        if (this.vscrollBar_0.Value > this.int_1)
                        {
                            int num = this.int_1;
                            if (num < 0)
                            {
                                num = 0;
                            }
                            this.vscrollBar_0.Value = num;
                        }
                        base.Invalidate();
                    }
                }
            }

            public bool method_6()
            {
                return this.bool_0;
            }

            public void method_7(bool bool_1)
            {
                if (this.bool_0 != bool_1)
                {
                    this.bool_0 = bool_1;
                    if (this.bool_0)
                    {
                        this.method_14();
                    }
                    else
                    {
                        this.method_16();
                    }
                }
            }

            public void method_8(int int_4)
            {
                int maximum = this.vscrollBar_0.Value - int_4;
                if (maximum < 0)
                {
                    maximum = 0;
                }
                if (maximum > this.vscrollBar_0.Maximum)
                {
                    maximum = this.vscrollBar_0.Maximum;
                }
                this.vscrollBar_0.Value = maximum;
            }

            public bool method_9()
            {
                return this.vscrollBar_0.Visible;
            }

            protected override void OnLostFocus(EventArgs e)
            {
                this.method_7(false);
                base.OnLostFocus(e);
            }

            protected override void OnMouseMove(MouseEventArgs e)
            {
                this.Cursor = Cursors.Default;
                int num = this.method_13(e.X, e.Y);
                if (num >= 0)
                {
                    if (this.method_0() > this.object_0.MaxIndex)
                    {
                        this.method_5(num + this.vscrollBar_0.Value);
                    }
                    else
                    {
                        this.method_5(num);
                    }
                    base.OnMouseMove(e);
                }
            }

            protected override void OnMouseUp(MouseEventArgs mevent)
            {
                int num = this.method_13(mevent.X, mevent.Y);
                if (num >= 0)
                {
                    if (this.method_0() > this.object_0.MaxIndex)
                    {
                        this.method_3(num + this.vscrollBar_0.Value);
                    }
                    else
                    {
                        this.method_3(num);
                    }
                    base.OnMouseUp(mevent);
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.FillRectangle(new SolidBrush(this.object_0.class115_0.BackColor), e.ClipRectangle);
                e.Graphics.DrawRectangle(new Pen(AisinoMultiCombox.SystemBorderColor), e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
                int y = 0;
                if (this.object_0.bool_0 && (this.method_0() > 0))
                {
                    int x = 1;
                    Rectangle rect = new Rectangle(new Point(1, 1), new Size(base.Width - 2, this.int_0));
                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), rect);
                    rect = new Rectangle(1, this.int_0 - 4, base.Width - 2, 4);
                    e.Graphics.FillRectangle(new LinearGradientBrush(new Point(rect.Width / 2, 0), new Point(rect.Width / 2, 4), SystemColors.Control, SystemColors.GrayText), rect);
                    foreach (AisinoMultiCombox.AisinoComboxColumn column in this.object_0._Columns)
                    {
                        if (column.Visible)
                        {
                            Rectangle layoutRectangle = new Rectangle(new Point(x, 0), new Size(column.int_1, this.int_0));
                            StringFormat format = new StringFormat {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center,
                                Trimming = StringTrimming.EllipsisWord
                            };
                            e.Graphics.DrawString(column.HeaderText, this.object_0.Font, new SolidBrush(this.object_0.ForeColor), layoutRectangle, format);
                            x += column.int_1;
                            e.Graphics.DrawLine(new Pen(SystemColors.ScrollBar, 1f), x, 2, x, this.int_0 - 4);
                            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight, 1f), x + 1, 2, x + 1, this.int_0 - 4);
                        }
                    }
                    y += this.int_0;
                }
                if (this.method_0() > 0)
                {
                    int num4 = this.vscrollBar_0.Value;
                    int num5 = ((this.object_0.MaxIndex + num4) < this.object_0.dataTable_0.Rows.Count) ? (this.object_0.MaxIndex + num4) : this.object_0.dataTable_0.Rows.Count;
                    for (int i = num4; i < num5; i++)
                    {
                        DataRow row = this.object_0.dataTable_0.Rows[i];
                        int num3 = 0;
                        Brush brush = new SolidBrush(this.object_0.ForeColor);
                        if (num4 == this.method_4())
                        {
                            Rectangle rectangle8 = new Rectangle(0, y, base.Width, this.int_0);
                            e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), rectangle8);
                            brush = new SolidBrush(Color.White);
                        }
                        foreach (AisinoMultiCombox.AisinoComboxColumn column2 in this.object_0._Columns)
                        {
                            if (column2.Visible)
                            {
                                Rectangle rectangle7 = new Rectangle(new Point(num3 + 2, y), new Size(column2.int_1, this.int_0));
                                StringFormat format2 = new StringFormat {
                                    LineAlignment = StringAlignment.Center,
                                    Trimming = StringTrimming.EllipsisWord
                                };
                                if (row.Table.Columns.Contains(column2.DataPropertyName))
                                {
                                    string s = row[column2.DataPropertyName].ToString();
                                    e.Graphics.DrawString(s, this.object_0.Font, brush, rectangle7, format2);
                                }
                                num3 += column2.int_1;
                                e.Graphics.DrawLine(new Pen(SystemColors.ControlDark, 1f), new Point(num3 + 1, y), new Point(num3 + 1, y + this.int_0));
                            }
                        }
                        y += this.int_0;
                        num4++;
                    }
                }
                base.OnPaint(e);
            }

            private void vscrollBar_0_ValueChanged(object sender, EventArgs e)
            {
                base.Invalidate();
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case 7:
                        this.object_0.Select(false, true);
                        break;

                    case 0x201:
                        if (this.vscrollBar_0.Visible)
                        {
                            Point point = this.vscrollBar_0.PointToClient(Control.MousePosition);
                            if (((point.X > 0) && (point.Y > 0)) && ((point.X < this.vscrollBar_0.Width) && (point.Y < this.vscrollBar_0.Height)))
                            {
                                return;
                            }
                        }
                        this.method_7(false);
                        break;
                }
                base.WndProc(ref m);
            }

            protected override System.Windows.Forms.CreateParams CreateParams
            {
                get
                {
                    System.Windows.Forms.CreateParams createParams = base.CreateParams;
                    if (this.object_0 != null)
                    {
                        createParams.Style ^= 0x40000000;
                        createParams.Style ^= 0x10000000;
                        createParams.Style |= -2147483648;
                        createParams.ExStyle |= 0x88;
                    }
                    return createParams;
                }
            }
        }

        [Flags]
        internal enum Enum13
        {
        }

        [Flags]
        internal enum Enum14
        {
        }
    }
}

