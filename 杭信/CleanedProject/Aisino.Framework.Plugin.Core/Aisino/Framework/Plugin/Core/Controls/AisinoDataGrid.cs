namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using PrintGrid;
    public class AisinoDataGrid : UserControl
    {
        private AisinoDataSet aisinoDataSet_0;
        private AisinoDataSet aisinoDataSet_1;
        private bool bool_0;
        private bool bool_1;
        [CompilerGenerated]
        private bool bool_2;
        private Button btnNextPage;
        private Button btnPrePage;
        private ComboBox cbCurPage;
        private ComboBox cbPageSize;
        private AisinoCHK chkShowAll;
        public Dictionary<string, object> ComboBoxColumnDataSource;
        private CustomStyleDataGrid customStyleDataGrid1;
        private Dictionary<string, Color> dictionary_0;
        private IContainer icontainer_0;
        private ILog ilog_0;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label lblCur;
        private Label lblCurPage;
        private Label lblPageSize;
        private Label lblTotal;
        private Label lblTotalPage;
        private List<KeyValuePair<Control, bool>> list_0;
        private Panel pnlTurnLeft;
        private Panel pnlTurnMid;
        private Panel pnlTurnPage;
        private Panel pnlTurnRight;
        private string string_0;

        public event EventHandler<DataGridRowCellCancelEventArgs> DataGridCellBeginEditEvent;

        public event EventHandler<DataGridRowEventArgs> DataGridCellEndEditEvent;

        public event EventHandler<DataGridViewCellFormattingEventArgs> DataGridCellFormatting;

        public event EventHandler<DataGridViewCellPaintingEventArgs> DataGridCellPainting;

        public event EventHandler<DataGridRowCellValidatingEventArgs> DataGridCellValidatingEvent;

        public event EventHandler<DataGridRowEventArgs> DataGridRowClickEvent;

        public event EventHandler<DataGridRowEventArgs> DataGridRowDbClickEvent;

        public event KeyEventHandler DataGridRowKeyDown;

        public event EventHandler<DataGridRowEventArgs> DataGridRowLeaveEvent;

        public event EventHandler<DataGridViewRowPostPaintEventArgs> DataGridRowPostPaint;

        public event EventHandler<DataGridRowEventArgs> DataGridRowSelectionChanged;

        public event EventHandler<GoToPageEventArgs> GoToPageEvent;

        public AisinoDataGrid()
        {
            
            this.ComboBoxColumnDataSource = new Dictionary<string, object>();
            this.ilog_0 = LogUtil.GetLogger<AisinoDataGrid>();
            this.InitializeComponent();
            this.method_0(false);
            this.list_0 = new List<KeyValuePair<Control, bool>>();
            this.IsShowAll = false;
            this.customStyleDataGrid1.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.customStyleDataGrid1_CellBeginEdit);
            this.customStyleDataGrid1.CellClick += new DataGridViewCellEventHandler(this.customStyleDataGrid1_CellClick);
            this.customStyleDataGrid1.CellDoubleClick += new DataGridViewCellEventHandler(this.customStyleDataGrid1_CellDoubleClick);
            this.customStyleDataGrid1.CellEndEdit += new DataGridViewCellEventHandler(this.customStyleDataGrid1_CellEndEdit);
            this.customStyleDataGrid1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.customStyleDataGrid1_CellFormatting);
            this.customStyleDataGrid1.CellValidating += new DataGridViewCellValidatingEventHandler(this.customStyleDataGrid1_CellValidating);
            this.customStyleDataGrid1.RowLeave += new DataGridViewCellEventHandler(this.customStyleDataGrid1_RowLeave);
            this.customStyleDataGrid1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.customStyleDataGrid1_RowPostPaint);
            this.customStyleDataGrid1.SelectionChanged += new EventHandler(this.customStyleDataGrid1_SelectionChanged);
            this.customStyleDataGrid1.KeyDown += new KeyEventHandler(this.customStyleDataGrid1_KeyDown);
        }

        public void AddRow(Dictionary<string, object> rowDic)
        {
            try
            {
                DataTable dataSource = this.customStyleDataGrid1.DataSource as DataTable;
                DataRow row = dataSource.NewRow();
                Dictionary<string, object>.KeyCollection.Enumerator enumerator = rowDic.Keys.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    row[enumerator.Current] = rowDic[enumerator.Current];
                }
                dataSource.Rows.Add(row);
            }
            catch (Exception exception)
            {
                this.ilog_0.Error("添加数据行时异常", exception);
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if ((this.GoToPageEvent != null) && (this.customStyleDataGrid1.DataSource != null))
            {
                this.GoToPageEvent(this, new GoToPageEventArgs(this.aisinoDataSet_0.CurrentPage + 1, this.aisinoDataSet_0.PageSize, false));
            }
        }

        private void btnPrePage_Click(object sender, EventArgs e)
        {
            if ((this.GoToPageEvent != null) && (this.customStyleDataGrid1.DataSource != null))
            {
                this.GoToPageEvent(this, new GoToPageEventArgs(this.aisinoDataSet_0.CurrentPage - 1, this.aisinoDataSet_0.PageSize, false));
            }
        }

        public void CancelEdit()
        {
            this.customStyleDataGrid1.CancelEdit();
        }

        private void cbCurPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblCurPage.Text = this.cbCurPage.SelectedItem.ToString();
            if ((this.GoToPageEvent != null) && (this.customStyleDataGrid1.DataSource != null))
            {
                this.GoToPageEvent(this, new GoToPageEventArgs(int.Parse(this.lblCurPage.Text), int.Parse(this.lblPageSize.Text), false));
            }
        }

        private void cbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblPageSize.Text = this.cbPageSize.SelectedItem.ToString();
            int num = int.Parse(this.lblPageSize.Text);
            if ((this.GoToPageEvent != null) && (this.customStyleDataGrid1.DataSource != null))
            {
                this.GoToPageEvent(this, new GoToPageEventArgs(1, num, false));
            }
        }

        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkShowAll.Checked)
            {
                this.IsShowAll = true;
                this.list_0.Clear();
                this.method_4(this.list_0);
                if ((this.GoToPageEvent != null) && (this.customStyleDataGrid1.DataSource != null))
                {
                    this.GoToPageEvent(this, new GoToPageEventArgs(1, this.aisinoDataSet_1.AllRows, this.chkShowAll.Checked));
                }
                this.method_3();
            }
            else
            {
                this.IsShowAll = false;
                if ((this.GoToPageEvent != null) && (this.customStyleDataGrid1.DataSource != null))
                {
                    this.GoToPageEvent(this, new GoToPageEventArgs(this.aisinoDataSet_1.CurrentPage, this.aisinoDataSet_1.PageSize, this.chkShowAll.Checked));
                }
                this.method_5();
            }
            this.customStyleDataGrid1.Invalidate();
        }

        public void ClearSelection()
        {
            this.customStyleDataGrid1.ClearSelection();
        }

        public int ColumnAdd(DataGridViewColumn dataGridViewColumn_0)
        {
            return this.customStyleDataGrid1.ColumnAdd(dataGridViewColumn_0);
        }

        private void customStyleDataGrid1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.DataGridCellBeginEditEvent != null)
            {
                DataGridRowCellCancelEventArgs args = new DataGridRowCellCancelEventArgs(this.customStyleDataGrid1.Rows[e.RowIndex], e.Cancel, e.ColumnIndex);
                this.DataGridCellBeginEditEvent(this, args);
                e.Cancel = args.Cancel;
            }
        }

        private void customStyleDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((this.DataGridRowClickEvent != null) && (e.RowIndex >= 0))
            {
                this.DataGridRowClickEvent(this, new DataGridRowEventArgs(this.customStyleDataGrid1.Rows[e.RowIndex], e.ColumnIndex));
            }
        }

        private void customStyleDataGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((this.DataGridRowDbClickEvent != null) && (e.RowIndex >= 0))
            {
                this.DataGridRowDbClickEvent(this, new DataGridRowEventArgs(this.customStyleDataGrid1.Rows[e.RowIndex], e.ColumnIndex));
            }
        }

        private void customStyleDataGrid1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.DataGridCellEndEditEvent != null)
            {
                this.DataGridCellEndEditEvent(this, new DataGridRowEventArgs(this.customStyleDataGrid1.Rows[e.RowIndex], e.ColumnIndex));
            }
        }

        private void customStyleDataGrid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.DataGridCellFormatting != null)
            {
                this.DataGridCellFormatting(sender, e);
            }
        }

        private void customStyleDataGrid1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (this.DataGridCellPainting != null)
            {
                this.DataGridCellPainting (this, e);
            }
        }

        private void customStyleDataGrid1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.DataGridCellValidatingEvent != null)
            {
                DataGridRowCellValidatingEventArgs args = new DataGridRowCellValidatingEventArgs(this.customStyleDataGrid1.Rows[e.RowIndex], e.Cancel, e.ColumnIndex, e.FormattedValue);
                this.DataGridCellValidatingEvent(this, args);
                e.Cancel = args.Cancel;
            }
        }

        private void customStyleDataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.DataGridRowKeyDown != null)
            {
                this.DataGridRowKeyDown(sender, e);
            }
        }

        private void customStyleDataGrid1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (this.DataGridRowLeaveEvent != null)
            {
                this.DataGridRowLeaveEvent(this, new DataGridRowEventArgs(this.customStyleDataGrid1.Rows[e.RowIndex], e.ColumnIndex));
            }
        }

        private void customStyleDataGrid1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (((this.string_0 != null) && (this.customStyleDataGrid1.Rows[e.RowIndex].Cells[this.string_0].Value != null)) && this.dictionary_0.ContainsKey(this.customStyleDataGrid1.Rows[e.RowIndex].Cells[this.string_0].Value.ToString()))
            {
                this.customStyleDataGrid1.Rows[e.RowIndex].DefaultCellStyle.BackColor = this.dictionary_0[this.customStyleDataGrid1.Rows[e.RowIndex].Cells[this.string_0].Value.ToString()];
            }
            if (this.DataGridRowPostPaint != null)
            {
                this.DataGridRowPostPaint(this, e);
            }
        }

        private void customStyleDataGrid1_SelectionChanged(object sender, EventArgs e)
        {
            if ((this.DataGridRowSelectionChanged != null) && (this.customStyleDataGrid1.SelectedRows.Count > 0))
            {
                this.DataGridRowSelectionChanged(this, new DataGridRowEventArgs(this.customStyleDataGrid1.SelectedRows[0], 0));
            }
        }

        private void customStyleDataGrid1_SelectionChanged_1(object sender, EventArgs e)
        {
            int num = 0;
            if ((this.DataSource != null) && (this.customStyleDataGrid1.CurrentCell != null))
            {
                num = (this.DataSource.CurrentPage - 1) * this.DataSource.PageSize;
                this.lblCur.Text = ((num + this.customStyleDataGrid1.CurrentRow.Index) + 1).ToString();
            }
        }

        public void DeleteRowAt(int int_0)
        {
            try
            {
                this.customStyleDataGrid1.Rows.RemoveAt(int_0);
            }
            catch (Exception exception)
            {
                this.ilog_0.Error("删除数据行时异常", exception);
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

        public void Export(string string_1)
        {
            this.customStyleDataGrid1.Export(string_1);
        }

        public Rectangle GetCellDisplayRectangle(int int_0, int int_1, bool bool_3)
        {
            return this.customStyleDataGrid1.GetCellDisplayRectangle(int_0, int_1, bool_3);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            this.pnlTurnPage = new Panel();
            this.pnlTurnMid = new Panel();
            this.lblCurPage = new Label();
            this.btnPrePage = new Button();
            this.btnNextPage = new Button();
            this.cbCurPage = new ComboBox();
            this.label6 = new Label();
            this.lblTotalPage = new Label();
            this.label10 = new Label();
            this.label11 = new Label();
            this.pnlTurnRight = new Panel();
            this.lblCur = new Label();
            this.lblTotal = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.pnlTurnLeft = new Panel();
            this.chkShowAll = new AisinoCHK();
            this.cbPageSize = new ComboBox();
            this.lblPageSize = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.customStyleDataGrid1 = new CustomStyleDataGrid();
            this.pnlTurnPage.SuspendLayout();
            this.pnlTurnMid.SuspendLayout();
            this.pnlTurnRight.SuspendLayout();
            this.pnlTurnLeft.SuspendLayout();
            ((ISupportInitialize) this.customStyleDataGrid1).BeginInit();
            base.SuspendLayout();
            this.pnlTurnPage.BackColor = Color.Transparent;
            this.pnlTurnPage.Controls.Add(this.pnlTurnMid);
            this.pnlTurnPage.Controls.Add(this.pnlTurnRight);
            this.pnlTurnPage.Controls.Add(this.pnlTurnLeft);
            this.pnlTurnPage.Dock = DockStyle.Bottom;
            this.pnlTurnPage.Location = new Point(0, 0x9d);
            this.pnlTurnPage.Name = "pnlTurnPage";
            this.pnlTurnPage.Size = new Size(0x2e1, 40);
            this.pnlTurnPage.TabIndex = 3;
            this.pnlTurnPage.SizeChanged += new EventHandler(this.pnlTurnPage_SizeChanged);
            this.pnlTurnMid.Anchor = AnchorStyles.Bottom | AnchorStyles.Top;
            this.pnlTurnMid.Controls.Add(this.lblCurPage);
            this.pnlTurnMid.Controls.Add(this.btnPrePage);
            this.pnlTurnMid.Controls.Add(this.btnNextPage);
            this.pnlTurnMid.Controls.Add(this.cbCurPage);
            this.pnlTurnMid.Controls.Add(this.label6);
            this.pnlTurnMid.Controls.Add(this.lblTotalPage);
            this.pnlTurnMid.Controls.Add(this.label10);
            this.pnlTurnMid.Controls.Add(this.label11);
            this.pnlTurnMid.Location = new Point(0x100, 0);
            this.pnlTurnMid.Name = "pnlTurnMid";
            this.pnlTurnMid.Size = new Size(0xc1, 40);
            this.pnlTurnMid.TabIndex = 5;
            this.lblCurPage.FlatStyle = FlatStyle.Flat;
            this.lblCurPage.ForeColor = SystemColors.HotTrack;
            this.lblCurPage.Location = new Point(0x33, 14);
            this.lblCurPage.Name = "lblCurPage";
            this.lblCurPage.Size = new Size(0x1d, 12);
            this.lblCurPage.TabIndex = 13;
            this.lblCurPage.Text = "1";
            this.lblCurPage.TextAlign = ContentAlignment.MiddleCenter;
            this.btnPrePage.Location = new Point(2, 8);
            this.btnPrePage.Name = "btnPrePage";
            this.btnPrePage.Size = new Size(30, 0x18);
            this.btnPrePage.TabIndex = 7;
            this.btnPrePage.Text = "<";
            this.btnPrePage.UseVisualStyleBackColor = true;
            this.btnPrePage.Click += new EventHandler(this.btnPrePage_Click);
            this.btnNextPage.Location = new Point(160, 8);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new Size(30, 0x18);
            this.btnNextPage.TabIndex = 6;
            this.btnNextPage.Text = ">";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new EventHandler(this.btnNextPage_Click);
            this.cbCurPage.BackColor = Color.White;
            this.cbCurPage.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbCurPage.DropDownWidth = 50;
            this.cbCurPage.FlatStyle = FlatStyle.Flat;
            this.cbCurPage.ForeColor = SystemColors.HotTrack;
            this.cbCurPage.Location = new Point(0x51, 10);
            this.cbCurPage.Name = "cbCurPage";
            this.cbCurPage.Size = new Size(0x11, 20);
            this.cbCurPage.TabIndex = 12;
            this.cbCurPage.TabStop = false;
            this.cbCurPage.SelectedIndexChanged += new EventHandler(this.cbCurPage_SelectedIndexChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x62, 14);
            this.label6.Name = "label6";
            this.label6.Size = new Size(11, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "/";
            this.lblTotalPage.FlatStyle = FlatStyle.Flat;
            this.lblTotalPage.ForeColor = Color.Black;
            this.lblTotalPage.Location = new Point(0x6b, 14);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new Size(0x1d, 12);
            this.lblTotalPage.TabIndex = 10;
            this.lblTotalPage.Text = "30";
            this.lblTotalPage.TextAlign = ContentAlignment.MiddleCenter;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x26, 14);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x11, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "第";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x89, 14);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x11, 12);
            this.label11.TabIndex = 7;
            this.label11.Text = "页";
            this.pnlTurnRight.Controls.Add(this.lblCur);
            this.pnlTurnRight.Controls.Add(this.lblTotal);
            this.pnlTurnRight.Controls.Add(this.label5);
            this.pnlTurnRight.Controls.Add(this.label4);
            this.pnlTurnRight.Controls.Add(this.label3);
            this.pnlTurnRight.Dock = DockStyle.Right;
            this.pnlTurnRight.Location = new Point(0x23f, 0);
            this.pnlTurnRight.Name = "pnlTurnRight";
            this.pnlTurnRight.Size = new Size(0xa2, 40);
            this.pnlTurnRight.TabIndex = 4;
            this.lblCur.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.lblCur.FlatStyle = FlatStyle.Flat;
            this.lblCur.ForeColor = Color.Black;
            this.lblCur.Location = new Point(0x16, 14);
            this.lblCur.Name = "lblCur";
            this.lblCur.Size = new Size(0x30, 12);
            this.lblCur.TabIndex = 5;
            this.lblCur.Text = "30";
            this.lblCur.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTotal.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.lblTotal.FlatStyle = FlatStyle.Flat;
            this.lblTotal.ForeColor = Color.Black;
            this.lblTotal.Location = new Point(0x5d, 14);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new Size(0x30, 12);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "30";
            this.lblTotal.TextAlign = ContentAlignment.MiddleCenter;
            this.label5.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x4c, 14);
            this.label5.Name = "label5";
            this.label5.Size = new Size(11, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "/";
            this.label4.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(5, 14);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "第";
            this.label3.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x8f, 14);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "条";
            this.pnlTurnLeft.Controls.Add(this.chkShowAll);
            this.pnlTurnLeft.Controls.Add(this.cbPageSize);
            this.pnlTurnLeft.Controls.Add(this.lblPageSize);
            this.pnlTurnLeft.Controls.Add(this.label2);
            this.pnlTurnLeft.Controls.Add(this.label1);
            this.pnlTurnLeft.Dock = DockStyle.Left;
            this.pnlTurnLeft.Location = new Point(0, 0);
            this.pnlTurnLeft.Name = "pnlTurnLeft";
            this.pnlTurnLeft.Size = new Size(0xbb, 40);
            this.pnlTurnLeft.TabIndex = 3;
            this.chkShowAll.AutoSize = true;
            this.chkShowAll.Location = new Point(0x6d, 13);
            this.chkShowAll.Name = "chkShowAll";
            this.chkShowAll.Size = new Size(0x48, 0x10);
            this.chkShowAll.TabIndex = 6;
            this.chkShowAll.Text = "全部显示";
            this.chkShowAll.UseVisualStyleBackColor = true;
            this.chkShowAll.CheckedChanged += new EventHandler(this.chkShowAll_CheckedChanged);
            this.cbPageSize.BackColor = Color.White;
            this.cbPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbPageSize.DropDownWidth = 40;
            this.cbPageSize.FlatStyle = FlatStyle.Flat;
            this.cbPageSize.ForeColor = Color.Black;
            this.cbPageSize.Items.AddRange(new object[] { "10", "20", "30", "50", "100", "500", "1000", "5000" });
            this.cbPageSize.Location = new Point(0x3e, 10);
            this.cbPageSize.Name = "cbPageSize";
            this.cbPageSize.Size = new Size(0x11, 20);
            this.cbPageSize.TabIndex = 0;
            this.cbPageSize.TabStop = false;
            this.cbPageSize.SelectedIndexChanged += new EventHandler(this.cbPageSize_SelectedIndexChanged);
            this.lblPageSize.FlatStyle = FlatStyle.Flat;
            this.lblPageSize.ForeColor = Color.Blue;
            this.lblPageSize.Location = new Point(0x1d, 14);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new Size(0x1d, 12);
            this.lblPageSize.TabIndex = 3;
            this.lblPageSize.Text = "30";
            this.lblPageSize.TextAlign = ContentAlignment.MiddleCenter;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(80, 14);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "条";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "每页";
            this.customStyleDataGrid1.AborCellPainting = false;
            this.customStyleDataGrid1.AllowColumnHeadersVisible = true;
            this.customStyleDataGrid1.AllowUserToAddRows = false;
            this.customStyleDataGrid1.AllowUserToResizeRows = false;
            style.BackColor = Color.FromArgb(240, 250, 0xff);
            this.customStyleDataGrid1.AlternatingRowsDefaultCellStyle = style;
            this.customStyleDataGrid1.BackgroundColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.customStyleDataGrid1.BorderStyle = BorderStyle.None;
            this.customStyleDataGrid1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            this.customStyleDataGrid1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style2.BackColor = Color.FromArgb(0x15, 0x87, 0xca);
            style2.Font = new Font("微软雅黑", 10f);
            style2.ForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.customStyleDataGrid1.ColumnHeadersDefaultCellStyle = style2;
            this.customStyleDataGrid1.ColumnHeadersHeight = 0;
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = Color.FromArgb(220, 230, 240);
            style3.Font = new Font("宋体", 10f);
            style3.ForeColor = Color.FromArgb(0, 0, 0);
            style3.SelectionBackColor = Color.FromArgb(0x7d, 150, 0xb9);
            style3.SelectionForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            style3.WrapMode = DataGridViewTriState.False;
            this.customStyleDataGrid1.DefaultCellStyle = style3;
            this.customStyleDataGrid1.Dock = DockStyle.Fill;
            this.customStyleDataGrid1.EnableHeadersVisualStyles = false;
            this.customStyleDataGrid1.GridStyle = CustomStyle.custom;
            this.customStyleDataGrid1.Location = new Point(0, 0);
            this.customStyleDataGrid1.Name = "customStyleDataGrid1";
            this.customStyleDataGrid1.RowHeadersVisible = false;
            this.customStyleDataGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.customStyleDataGrid1.Size = new Size(0x2e1, 0x9d);
            this.customStyleDataGrid1.TabIndex = 4;
            this.customStyleDataGrid1.SelectionChanged += new EventHandler(this.customStyleDataGrid1_SelectionChanged_1);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = Color.White;
            base.Controls.Add(this.customStyleDataGrid1);
            base.Controls.Add(this.pnlTurnPage);
            base.Name = "AisinoDataGrid";
            this.RightToLeft = RightToLeft.No;
            base.Size = new Size(0x2e1, 0xc5);
            this.pnlTurnPage.ResumeLayout(false);
            this.pnlTurnMid.ResumeLayout(false);
            this.pnlTurnMid.PerformLayout();
            this.pnlTurnRight.ResumeLayout(false);
            this.pnlTurnRight.PerformLayout();
            this.pnlTurnLeft.ResumeLayout(false);
            this.pnlTurnLeft.PerformLayout();
            ((ISupportInitialize) this.customStyleDataGrid1).EndInit();
            base.ResumeLayout(false);
        }

        public void LoadGridStyles(XmlNode xmlNode_0)
        {
            XmlAttributeCollection attributes = xmlNode_0.Attributes;
            string text1 = attributes["id"].Value;
            string text2 = attributes["control.type"].Value;
            XmlComponentUtil.SetDataGridProperty(attributes, this.customStyleDataGrid1);
            SortedList<int, DataGridViewColumn> list = new SortedList<int, DataGridViewColumn>();
            this.AutoGenerateColumns = false;
            if (xmlNode_0.HasChildNodes)
            {
                foreach (XmlNode node in xmlNode_0)
                {
                    if ("DataGridViewColumnCollection".Equals(node.Name))
                    {
                        if (node.HasChildNodes)
                        {
                            foreach (XmlNode node2 in node)
                            {
                                if ("DataGridViewColumnItem".Equals(node2.Name))
                                {
                                    DataGridViewColumn dataGridViewColumnItem = XmlComponentUtil.GetDataGridViewColumnItem(node2, this.customStyleDataGrid1);
                                    if ((dataGridViewColumnItem != null) && !this.Columns.Contains(dataGridViewColumnItem.Name))
                                    {
                                        list.Add(dataGridViewColumnItem.DisplayIndex, dataGridViewColumnItem);
                                    }
                                }
                            }
                            if ((list != null) && (list.Count > 0))
                            {
                                foreach (int num in list.Keys)
                                {
                                    this.customStyleDataGrid1.Columns.Add(list[num]);
                                }
                            }
                        }
                    }
                    else
                    {
                        DataGridViewCellStyle dataGridCellStyle = XmlComponentUtil.GetDataGridCellStyle(node, this.customStyleDataGrid1);
                        if (dataGridCellStyle != null)
                        {
                            PropertyInfo property = this.customStyleDataGrid1.GetType().GetProperty(node.Name);
                            if (property != null)
                            {
                                property.SetValue(this.customStyleDataGrid1, dataGridCellStyle, null);
                            }
                        }
                    }
                }
            }
        }

        private void method_0(bool bool_3)
        {
            this.cbCurPage.Enabled = bool_3;
            this.btnPrePage.Enabled = bool_3;
            this.btnNextPage.Enabled = bool_3;
            this.chkShowAll.Enabled = bool_3;
        }

        private void method_1(AisinoDataSet aisinoDataSet_2)
        {
            if (aisinoDataSet_2.CurrentPage > 1)
            {
                this.btnPrePage.Enabled = true;
            }
            else
            {
                this.btnPrePage.Enabled = false;
            }
            if (aisinoDataSet_2.CurrentPage < aisinoDataSet_2.AllPageNum)
            {
                this.btnNextPage.Enabled = true;
            }
            else
            {
                this.btnNextPage.Enabled = false;
            }
            if (aisinoDataSet_2.AllPageNum > 1)
            {
                this.chkShowAll.Enabled = true;
                this.cbCurPage.Enabled = true;
                this.cbCurPage.Items.Clear();
                for (int i = 1; i <= aisinoDataSet_2.AllPageNum; i++)
                {
                    this.cbCurPage.Items.Add(i.ToString());
                }
            }
            else
            {
                if (!this.chkShowAll.Checked)
                {
                    this.chkShowAll.Enabled = false;
                }
                this.cbCurPage.Enabled = false;
            }
            this.lblPageSize.Text = aisinoDataSet_2.PageSize.ToString();
            this.lblCurPage.Text = aisinoDataSet_2.CurrentPage.ToString();
            this.lblTotalPage.Text = aisinoDataSet_2.AllPageNum.ToString();
            this.lblTotal.Text = aisinoDataSet_2.AllRows.ToString();
        }

        private void method_2(string string_1)
        {
            this.dictionary_0 = new Dictionary<string, Color>();
            if (string_1 == "DJHXZ")
            {
                this.dictionary_0.Add("3", Color.LightCyan);
                this.dictionary_0.Add("4", Color.LightBlue);
            }
            else
            {
                if (!(string_1 == "WJ"))
                {
                    return;
                }
                this.dictionary_0.Add("0", SystemColor.GRID_ROW_BACKCOLOR);
                this.dictionary_0.Add("1", SystemColor.GRID_ALTROW_BACKCOLOR);
            }
            this.customStyleDataGrid1.CellPainting += new DataGridViewCellPaintingEventHandler(this.customStyleDataGrid1_CellPainting);
        }

        private void method_3()
        {
            foreach (KeyValuePair<Control, bool> pair in this.list_0)
            {
                pair.Key.Enabled = false;
            }
        }

        private void method_4(List<KeyValuePair<Control, bool>> list)
        {
            this.aisinoDataSet_1 = new AisinoDataSet();
            this.aisinoDataSet_1.AllPageNum = this.aisinoDataSet_0.AllPageNum;
            this.aisinoDataSet_1.AllRows = this.aisinoDataSet_0.AllRows;
            this.aisinoDataSet_1.CurrentPage = this.aisinoDataSet_0.CurrentPage;
            this.aisinoDataSet_1.PageSize = this.aisinoDataSet_0.PageSize;
            list.Add(new KeyValuePair<Control, bool>(this.cbCurPage, this.cbCurPage.Enabled));
            list.Add(new KeyValuePair<Control, bool>(this.cbPageSize, this.cbPageSize.Enabled));
            list.Add(new KeyValuePair<Control, bool>(this.btnNextPage, this.btnNextPage.Enabled));
            list.Add(new KeyValuePair<Control, bool>(this.btnPrePage, this.btnPrePage.Enabled));
        }

        private void method_5()
        {
            foreach (KeyValuePair<Control, bool> pair in this.list_0)
            {
                pair.Key.Enabled = pair.Value;
            }
        }

        private void pnlTurnPage_SizeChanged(object sender, EventArgs e)
        {
            int x = (this.pnlTurnPage.Width - this.pnlTurnMid.Width) / 2;
            this.pnlTurnMid.Location = new Point(x, this.pnlTurnMid.Location.Y);
        }

        public void Print(string string_1, object object_0, List<PrinterItems> header, List<PrinterItems> footer, bool bool_3)
        {
            this.customStyleDataGrid1.method_6(string_1, base.Name, object_0, header, footer, bool_3);
        }

        public void ResetAllPageControl()
        {
            this.chkShowAll.Checked = false;
            this.method_5();
            this.IsShowAll = false;
        }

        public void Select(object object_0)
        {
            if (this.customStyleDataGrid1.DataSource == null)
            {
                this.customStyleDataGrid1.DataSource = this.DataSource.Data;
            }
            this.customStyleDataGrid1.Select(object_0);
        }

        public void SelectAll()
        {
            this.customStyleDataGrid1.SelectAll();
        }

        public void SetColumnReadOnly(int int_0, bool bool_3)
        {
            this.customStyleDataGrid1.SetColumnReadOnly(int_0, bool_3);
        }

        public void SetColumnReadOnly(string string_1, bool bool_3)
        {
            this.customStyleDataGrid1.SetColumnReadOnly(string_1, bool_3);
        }

        public void SetColumnsStyle(string string_1, object object_0)
        {
            this.customStyleDataGrid1.SetColumnStyles(string_1, object_0, base.Name, base.GetType().FullName);
        }

        public void SetSelectRows(int int_0)
        {
            this.customStyleDataGrid1.Rows[int_0].Selected = true;
        }

        public void Statistics(object object_0)
        {
            this.customStyleDataGrid1.Statistics(object_0);
        }

        [Category("Appearance"), Browsable(true)]
        public bool AborCellPainting
        {
            get
            {
                this.bool_1 = this.customStyleDataGrid1.AborCellPainting;
                return this.bool_1;
            }
            set
            {
                this.customStyleDataGrid1.AborCellPainting = value;
                this.bool_1 = value;
            }
        }

        public bool AutoGenerateColumns
        {
            set
            {
                this.customStyleDataGrid1.AutoGenerateColumns = value;
            }
        }

        public List<Dictionary<string, string>> ColumeHead
        {
            set
            {
                if (value != null)
                {
                    foreach (Dictionary<string, string> dictionary in value)
                    {
                        string str2;
                        string str3;
                        string str4;
                        string str5;
                        int num;
                        DataGridViewColumn column = null;
                        string str6 = dictionary["Type"];
                        if (str6 != null)
                        {
                            if (str6 != "Text")
                            {
                                if (!(str6 == "AisinoBTN"))
                                {
                                    if (str6 == "ComboBox")
                                    {
                                        column = new DataGridViewComboBoxColumn();
                                        DataGridViewComboBoxColumn column2 = (DataGridViewComboBoxColumn) column;
                                        column2.DataSource = this.ComboBoxColumnDataSource[dictionary["DataSource"]];
                                        column2.DisplayMember = dictionary["DisplayMember"];
                                        column2.ValueMember = dictionary["ValueMember"];
                                        column2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                                        column2.FlatStyle = FlatStyle.System;
                                    }
                                }
                                else
                                {
                                    column = new DataGridViewButtonColumn();
                                }
                            }
                            else
                            {
                                column = new DataGridViewTextBoxColumn();
                            }
                        }
                        column.DataPropertyName = dictionary["Property"];
                        column.HeaderText = dictionary["AisinoLBL"];
                        column.Name = dictionary["Property"];
                        column.ValueType = typeof(string);
                        column.Visible = !dictionary.ContainsKey("Visible") || !dictionary["Visible"].Equals("False");
                        column.ReadOnly = dictionary.ContainsKey("ReadOnly") ? dictionary.ContainsKey("ReadOnly") : false;
                        string str = "";
                        if (dictionary.TryGetValue("DisplayIndex", out str))
                        {
                            column.DisplayIndex = int.Parse(str);
                        }
                        if (dictionary.TryGetValue("Width", out str2))
                        {
                            column.Width = int.Parse(str2);
                        }
                        if (dictionary.TryGetValue("Align", out str3))
                        {
                            column.DefaultCellStyle.Alignment = (DataGridViewContentAlignment) Enum.Parse(typeof(DataGridViewContentAlignment), str3);
                        }
                        if (dictionary.TryGetValue("HeadAlign", out str4))
                        {
                            column.HeaderCell.Style.Alignment = (DataGridViewContentAlignment) Enum.Parse(typeof(DataGridViewContentAlignment), str4);
                        }
                        if (dictionary.TryGetValue("FillWeight", out str5) && int.TryParse(str5, out num))
                        {
                            column.FillWeight = num;
                        }
                        dictionary.TryGetValue("RowStyleField", out this.string_0);
                        dictionary.Add("Index", this.customStyleDataGrid1.ColumnAdd(column).ToString());
                    }
                }
            }
        }

        public DataGridViewColumnCollection Columns
        {
            get
            {
                return this.customStyleDataGrid1.Columns;
            }
        }

        public DataGridViewCell CurrentCell
        {
            get
            {
                return this.customStyleDataGrid1.CurrentCell;
            }
            set
            {
                this.customStyleDataGrid1.CurrentCell = value;
            }
        }

        public CustomStyleDataGrid DataGrid
        {
            get
            {
                return this.customStyleDataGrid1;
            }
        }

        public AisinoDataSet DataSource
        {
            get
            {
                return this.aisinoDataSet_0;
            }
            set
            {
                if (this.IsShowAll && !this.bool_0)
                {
                    this.chkShowAll.Checked = true;
                }
                if (value != null)
                {
                    this.customStyleDataGrid1.DataSource = value.Data;
                    if ((value.Data != null) && (value.Data.Rows.Count > 0))
                    {
                        this.chkShowAll.Enabled = true;
                    }
                    else
                    {
                        this.chkShowAll.Enabled = false;
                    }
                    this.aisinoDataSet_0 = value;
                    this.method_1(value);
                    if (!string.IsNullOrEmpty(this.string_0))
                    {
                        this.method_2(this.string_0);
                    }
                    if (this.customStyleDataGrid1.Columns.Contains("SLV"))
                    {
                        this.customStyleDataGrid1.Columns["SLV"].DefaultCellStyle.Format = "0%";
                    }
                    this.customStyleDataGrid1.ClearSelection();
                }
                else
                {
                    this.method_0(false);
                }
                this.bool_0 = false;
            }
        }

        [Browsable(false)]
        public int FirstDisplayedScrollingRowIndex
        {
            get
            {
                return this.customStyleDataGrid1.FirstDisplayedScrollingRowIndex;
            }
            set
            {
                if ((value >= 0) && (value <= (this.customStyleDataGrid1.RowCount - 1)))
                {
                    this.customStyleDataGrid1.FirstDisplayedScrollingRowIndex = value;
                }
            }
        }

        public bool IsShowAll
        {
            [CompilerGenerated]
            get
            {
                return this.bool_2;
            }
            [CompilerGenerated]
            set
            {
                this.bool_2 = value;
            }
        }

        [DefaultValue(true)]
        public bool MultiSelect
        {
            get
            {
                return this.customStyleDataGrid1.MultiSelect;
            }
            set
            {
                this.customStyleDataGrid1.MultiSelect = value;
            }
        }

        [DefaultValue(true)]
        public bool ReadOnly
        {
            get
            {
                return this.customStyleDataGrid1.ReadOnly;
            }
            set
            {
                this.customStyleDataGrid1.ReadOnly = value;
                this.customStyleDataGrid1.AllowUserToAddRows = !value;
            }
        }

        public DataGridViewRowCollection Rows
        {
            get
            {
                return this.customStyleDataGrid1.Rows;
            }
        }

        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                return this.customStyleDataGrid1.SelectedRows;
            }
        }

        [DefaultValue(1)]
        public DataGridViewSelectionMode SelectionMode
        {
            get
            {
                return this.customStyleDataGrid1.SelectionMode;
            }
            set
            {
                this.customStyleDataGrid1.SelectionMode = value;
            }
        }

        public bool ShowAllChkVisible
        {
            get
            {
                return this.chkShowAll.Visible;
            }
            set
            {
                this.chkShowAll.Visible = value;
            }
        }

        [DefaultValue(true)]
        public bool ShowPageBar
        {
            set
            {
                if (!value)
                {
                    this.pnlTurnPage.Visible = false;
                }
                else
                {
                    this.pnlTurnPage.Visible = true;
                }
            }
        }
    }
}

