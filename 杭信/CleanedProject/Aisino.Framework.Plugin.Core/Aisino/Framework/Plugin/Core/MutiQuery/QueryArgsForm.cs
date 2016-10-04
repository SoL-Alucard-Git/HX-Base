namespace Aisino.Framework.Plugin.Core.MutiQuery
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Properties;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class QueryArgsForm : Form
    {
        private DataGridViewTextBoxColumn clnArgs;
        private DataGridViewTextBoxColumn clnField;
        private DataGridViewTextBoxColumn clnName;
        private DataGridViewTextBoxColumn clnNo;
        private DataGridViewTextBoxColumn clnRel;
        private DataGridViewTextBoxColumn clnType;
        private ComboBox comboBox_0;
        private CustomStyleDataGrid dgvArgs;
        private readonly Dictionary<string, List<string>> dictionary_0;
        private IContainer icontainer_0;
        private readonly List<BaseCondition> list_0;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbExit;
        private ToolStripButton tsbSelect;

        public QueryArgsForm()
        {
            
            this.comboBox_0 = new ComboBox();
            this.dictionary_0 = new Dictionary<string, List<string>>();
            this.list_0 = new List<BaseCondition>();
            this.InitializeComponent();
            this.dgvArgs.Columns["clnArgs"].ReadOnly = true;
            this.dgvArgs.Controls.Add(this.comboBox_0);
            this.comboBox_0.Visible = false;
        }

        private void dgvArgs_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.clnArgs.DisplayIndex)
            {
                DataGridViewRow row = this.dgvArgs.Rows[e.RowIndex];
                string key = row.Cells["clnType"].Value.ToString();
                if ((key != null) && this.dictionary_0.ContainsKey(key))
                {
                    this.comboBox_0.Items.Clear();
                    this.comboBox_0.Items.AddRange(this.dictionary_0[key].ToArray());
                    DataGridViewCell cell = this.dgvArgs.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    Rectangle rectangle = this.dgvArgs.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
                    this.comboBox_0.Location = rectangle.Location;
                    this.comboBox_0.Size = rectangle.Size;
                    this.method_0(this.comboBox_0, (string) cell.Value);
                    this.comboBox_0.Visible = true;
                    this.comboBox_0.Focus();
                }
            }
        }

        private void dgvArgs_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.clnArgs.DisplayIndex)
            {
                DataGridViewCell cell = this.dgvArgs.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Value = this.comboBox_0.Text;
                this.comboBox_0.Visible = false;
            }
        }

        private void dgvArgs_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.dgvArgs.Rows[e.RowIndex].ErrorText = "";
            if (e.ColumnIndex == this.clnArgs.DisplayIndex)
            {
                if ((e.FormattedValue == null) || (e.FormattedValue.ToString() == ""))
                {
                    this.dgvArgs.Rows[e.RowIndex].ErrorText = "请设置参数值";
                    e.Cancel = true;
                }
                else if (e.FormattedValue.ToString() != "|空值|")
                {
                    BaseCondition tag = this.dgvArgs.Rows[e.RowIndex].Tag as BaseCondition;
                    if ((tag != null) && this.method_1(e.FormattedValue.ToString(), tag.ValueType))
                    {
                        this.dgvArgs.Rows[e.RowIndex].ErrorText = "设置参数值无效";
                        e.Cancel = true;
                    }
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

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            this.toolStrip1 = new ToolStrip();
            this.tsbExit = new ToolStripButton();
            this.tsbSelect = new ToolStripButton();
            this.dgvArgs = new CustomStyleDataGrid();
            this.clnNo = new DataGridViewTextBoxColumn();
            this.clnName = new DataGridViewTextBoxColumn();
            this.clnField = new DataGridViewTextBoxColumn();
            this.clnRel = new DataGridViewTextBoxColumn();
            this.clnType = new DataGridViewTextBoxColumn();
            this.clnArgs = new DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((ISupportInitialize) this.dgvArgs).BeginInit();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.tsbExit, this.tsbSelect });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x23f, 0x19);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            this.tsbExit.Alignment = ToolStripItemAlignment.Right;
            this.tsbExit.Image = Resources.smethod_4();
            this.tsbExit.ImageTransparentColor = Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new Size(0x34, 0x16);
            this.tsbExit.Text = "退出";
            this.tsbExit.Click += new EventHandler(this.tsbExit_Click);
            this.tsbSelect.Alignment = ToolStripItemAlignment.Right;
            this.tsbSelect.Image = Resources.smethod_1();
            this.tsbSelect.ImageTransparentColor = Color.Magenta;
            this.tsbSelect.Name = "tsbSelect";
            this.tsbSelect.Size = new Size(0x4c, 0x16);
            this.tsbSelect.Text = "执行查找";
            this.tsbSelect.Click += new EventHandler(this.tsbSelect_Click);
            this.dgvArgs.AborCellPainting = false;
            this.dgvArgs.AllowColumnHeadersVisible = true;
            this.dgvArgs.AllowUserToAddRows = false;
            this.dgvArgs.AllowUserToDeleteRows = false;
            this.dgvArgs.AllowUserToResizeRows = false;
            style.BackColor = Color.FromArgb(0xff, 250, 240);
            this.dgvArgs.AlternatingRowsDefaultCellStyle = style;
            this.dgvArgs.BackgroundColor = SystemColors.ButtonFace;
            this.dgvArgs.BorderStyle = BorderStyle.None;
            this.dgvArgs.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            this.dgvArgs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style2.BackColor = Color.FromArgb(0x15, 0x87, 0xca);
            style2.Font = new Font("Microsoft YaHei", 10f);
            style2.ForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.dgvArgs.ColumnHeadersDefaultCellStyle = style2;
            this.dgvArgs.ColumnHeadersHeight = 0;
            this.dgvArgs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArgs.Columns.AddRange(new DataGridViewColumn[] { this.clnNo, this.clnName, this.clnField, this.clnRel, this.clnType, this.clnArgs });
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = Color.FromArgb(220, 230, 240);
            style3.Font = new Font("SimSun", 10f);
            style3.ForeColor = Color.FromArgb(0, 0, 0);
            style3.SelectionBackColor = Color.FromArgb(0x7d, 150, 0xb9);
            style3.SelectionForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            style3.WrapMode = DataGridViewTriState.False;
            this.dgvArgs.DefaultCellStyle = style3;
            this.dgvArgs.EnableHeadersVisualStyles = false;
            this.dgvArgs.GridColor = Color.Gray;
            this.dgvArgs.GridStyle = CustomStyle.custom;
            this.dgvArgs.Location = new Point(2, 0x1c);
            this.dgvArgs.MultiSelect = false;
            this.dgvArgs.Name = "dgvArgs";
            this.dgvArgs.RowHeadersVisible = false;
            style4.BackColor = Color.White;
            style4.SelectionBackColor = Color.SlateGray;
            this.dgvArgs.RowsDefaultCellStyle = style4;
            this.dgvArgs.RowTemplate.Height = 0x17;
            this.dgvArgs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvArgs.Size = new Size(0x23d, 0xdb);
            this.dgvArgs.TabIndex = 0;
            this.dgvArgs.CellEnter += new DataGridViewCellEventHandler(this.dgvArgs_CellEnter);
            this.dgvArgs.CellLeave += new DataGridViewCellEventHandler(this.dgvArgs_CellLeave);
            this.dgvArgs.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgvArgs_CellValidating);
            this.clnNo.HeaderText = "序号";
            this.clnNo.Name = "clnNo";
            this.clnNo.ReadOnly = true;
            this.clnNo.Width = 0x2e;
            this.clnName.HeaderText = "条件名称";
            this.clnName.Name = "clnName";
            this.clnName.ReadOnly = true;
            this.clnName.Width = 130;
            this.clnField.HeaderText = "查询字段";
            this.clnField.Name = "clnField";
            this.clnField.ReadOnly = true;
            this.clnField.Width = 110;
            this.clnRel.HeaderText = "关系符号";
            this.clnRel.Name = "clnRel";
            this.clnRel.ReadOnly = true;
            this.clnRel.Width = 80;
            this.clnType.HeaderText = "字段类型";
            this.clnType.Name = "clnType";
            this.clnType.ReadOnly = true;
            this.clnArgs.HeaderText = "查询参数";
            this.clnArgs.Name = "clnArgs";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x23f, 0xf8);
            base.Controls.Add(this.toolStrip1);
            base.Controls.Add(this.dgvArgs);
            base.Name = "QueryArgsForm";
            this.Text = "查询条件参数设置";
            base.Load += new EventHandler(this.QueryArgsForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((ISupportInitialize) this.dgvArgs).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(ComboBox comboBox_1, string string_0)
        {
            comboBox_1.SelectedIndex = -1;
            if (string_0 == null)
            {
                comboBox_1.Text = "";
            }
            else
            {
                comboBox_1.Text = string_0;
                foreach (object obj2 in comboBox_1.Items)
                {
                    if (obj2.ToString() == string_0)
                    {
                        comboBox_1.SelectedItem = obj2;
                    }
                }
            }
        }

        private bool method_1(string string_0, FieldType fieldType_0)
        {
            bool flag = false;
            if (fieldType_0 == FieldType.String)
            {
                flag = true;
            }
            else if (fieldType_0 == FieldType.Int)
            {
                int num;
                float num2;
                decimal num3;
                flag = (int.TryParse(string_0, out num) || float.TryParse(string_0, out num2)) || decimal.TryParse(string_0, out num3);
            }
            else if (fieldType_0 == FieldType.Bool)
            {
                bool flag2;
                flag = bool.TryParse(string_0, out flag2);
            }
            else if (fieldType_0 == FieldType.DateTime)
            {
                DateTime time;
                flag = DateTime.TryParse(string_0, out time);
            }
            return !flag;
        }

        private void QueryArgsForm_Load(object sender, EventArgs e)
        {
            int num = 1;
            foreach (BaseCondition condition in this.ArgsList)
            {
                if ((condition == null) || (condition.DataField == null))
                {
                    continue;
                }
                FieldType valueType = condition.ValueType;
                string description = EnumHelper.GetDescription((ArithOperator)valueType);
                string str2 = "";
                List<string> list = new List<string>();
                switch (valueType)
                {
                    case FieldType.Int:
                        str2 = "0";
                        list.AddRange(new string[] { "0", "|空值|" });
                        break;

                    case FieldType.Bool:
                        str2 = "true";
                        list.AddRange(new string[] { "true", "false", "|空值|" });
                        break;

                    case FieldType.DateTime:
                        str2 = DateTime.Today.ToShortDateString();
                        list.AddRange(new string[] { str2, "|空值|" });
                        break;

                    default:
                        str2 = "|空值|";
                        list.Add("|空值|");
                        break;
                }
                if (!this.dictionary_0.ContainsKey(description))
                {
                    this.dictionary_0.Add(description, list);
                }
                this.dgvArgs.Rows.Add(new object[] { num, condition.Name, condition.DataField.Name, condition.Operator.ToString(), description, str2 });
                this.dgvArgs.Rows[num - 1].Tag = condition;
                num++;
            }
            if (this.dgvArgs.Rows.Count > 0)
            {
                this.dgvArgs.Rows[0].Cells["clnArgs"].Selected = true;
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void tsbSelect_Click(object sender, EventArgs e)
        {
            this.dgvArgs.SelectedRows[0].Cells[0].Selected = true;
            if (string.IsNullOrEmpty(this.dgvArgs.SelectedRows[0].ErrorText))
            {
                foreach (DataGridViewRow row in (IEnumerable) this.dgvArgs.Rows)
                {
                    string str = row.Cells["clnArgs"].Value.ToString();
                    BaseCondition tag = row.Tag as BaseCondition;
                    if (tag != null)
                    {
                        tag.Value = str;
                    }
                }
                base.DialogResult = DialogResult.OK;
            }
        }

        public List<BaseCondition> ArgsList
        {
            get
            {
                return this.list_0;
            }
        }
    }
}

