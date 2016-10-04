namespace Aisino.Framework.Plugin.Core.MutiQuery
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Properties;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class MutiConditionQuery : Form
    {
        private bool bool_0;
        private DataGridViewTextBoxColumn clnField;
        private DataGridViewTextBoxColumn clnName;
        private DataGridViewTextBoxColumn clnRel;
        private DataGridViewTextBoxColumn clnValue;
        private ComboBox comboBox_0;
        private ComboBox comboBox_1;
        private ComboBox comboBox_2;
        private ComboBox comboBox_3;
        private CustomStyleDataGrid dgvBase;
        private readonly Dictionary<FieldType, ComboBox> dictionary_0;
        private readonly Dictionary<string, DataField> dictionary_1;
        private readonly Dictionary<string, Condition> dictionary_2;
        private IContainer icontainer_0;
        private int int_0;
        private int int_1;
        private List<DataField> list_0;
        private List<DataField> list_1;
        private string string_0;
        private string string_1;
        private string string_2;
        private string[] string_3;
        private string[] string_4;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbAdd;
        private ToolStripButton tsbDelete;
        private ToolStripButton tsbExit;
        private ToolStripButton tsbSelect;

        public MutiConditionQuery()
        {
            
            this.int_0 = -1;
            this.int_1 = 1;
            this.string_0 = "";
            this.string_1 = "";
            this.comboBox_0 = new ComboBox();
            this.comboBox_1 = new ComboBox();
            this.comboBox_2 = new ComboBox();
            this.comboBox_3 = new ComboBox();
            this.string_3 = Enum.GetNames(typeof(ArithOperator));
            this.string_4 = Enum.GetNames(typeof(LogicOperator));
            this.list_0 = new List<DataField>();
            this.list_1 = new List<DataField>();
            this.dictionary_0 = new Dictionary<FieldType, ComboBox>();
            this.dictionary_1 = new Dictionary<string, DataField>();
            this.dictionary_2 = new Dictionary<string, Condition>();
            this.InitializeComponent();
            this.string_0 = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Config\GridSearch\");
            this.dgvBase.Columns["clnValue"].ReadOnly = true;
            this.comboBox_0.Visible = false;
            this.comboBox_1.Visible = false;
            this.comboBox_2.Visible = false;
            this.comboBox_3.Visible = false;
            this.comboBox_0.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_1.ItemHeight = 20;
            this.dgvBase.Controls.Add(this.comboBox_0);
            this.dgvBase.Controls.Add(this.comboBox_1);
            this.dgvBase.Controls.Add(this.comboBox_2);
            this.dgvBase.Controls.Add(this.comboBox_3);
            base.Load += new EventHandler(this.MutiConditionQuery_Load);
        }

        private void dgvBase_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.method_3(e.RowIndex);
            if (e.ColumnIndex == this.clnField.DisplayIndex)
            {
                this.method_7(e);
            }
            else if (e.ColumnIndex == this.clnRel.DisplayIndex)
            {
                this.method_6(e);
            }
            else if (e.ColumnIndex == this.clnValue.DisplayIndex)
            {
                DataGridViewCell cell = this.dgvBase.Rows[e.RowIndex].Cells[e.ColumnIndex];
                Rectangle rectangle = this.dgvBase.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
                ComboBox box = this.comboBox_2;
                DataField tag = this.dgvBase.Rows[e.RowIndex].Cells["clnField"].Tag as DataField;
                if (tag != null)
                {
                    this.dictionary_0.TryGetValue(tag.DataType, out box);
                }
                box.Location = rectangle.Location;
                box.Size = rectangle.Size;
                this.method_9(box, (string) cell.Value);
                box.Visible = true;
                box.Focus();
            }
        }

        private void dgvBase_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.clnField.DisplayIndex)
            {
                DataGridViewCell cell2 = this.dgvBase.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell2.Value = this.comboBox_0.Text;
                cell2.Tag = this.comboBox_0.SelectedItem;
                this.comboBox_0.Visible = false;
            }
            else if (e.ColumnIndex == this.clnRel.DisplayIndex)
            {
                DataGridViewCell cell = this.dgvBase.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Value = this.comboBox_1.Text;
                this.comboBox_1.Visible = false;
            }
            else if (e.ColumnIndex == this.clnValue.DisplayIndex)
            {
                ComboBox box = this.comboBox_2;
                DataField tag = this.dgvBase.Rows[e.RowIndex].Cells["clnField"].Tag as DataField;
                if (tag != null)
                {
                    this.dictionary_0.TryGetValue(tag.DataType, out box);
                }
                DataGridViewCell cell3 = this.dgvBase.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell3.Value = box.Text;
                cell3.Tag = box.SelectedItem;
                box.Visible = false;
            }
            this.int_0 = e.RowIndex;
        }

        private void dgvBase_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewRow row = this.dgvBase.Rows[e.RowIndex];
            this.dgvBase.Rows[e.RowIndex].ErrorText = "";
            if (e.ColumnIndex == this.clnName.DisplayIndex)
            {
                if ((e.FormattedValue != null) && (e.FormattedValue.ToString() != ""))
                {
                    if (this.method_13(this.dgvBase.Rows[e.RowIndex].Cells[e.ColumnIndex]))
                    {
                        this.dgvBase.Rows[e.RowIndex].ErrorText = "条件名称已存在";
                        e.Cancel = true;
                    }
                    else if ((this.dgvBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null) && (e.FormattedValue != null))
                    {
                        string str = e.FormattedValue.ToString();
                        string key = this.dgvBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        if ((key != str) && this.dictionary_1.ContainsKey(key))
                        {
                            this.comboBox_0.Items.Remove(this.dictionary_1[key]);
                            this.comboBox_3.Items.Remove(this.dictionary_1[key]);
                            this.dictionary_1.Remove(key);
                        }
                    }
                }
                else
                {
                    this.dgvBase.Rows[e.RowIndex].ErrorText = "请输入条件名称";
                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == this.clnField.DisplayIndex)
            {
                if ((e.FormattedValue == null) || (e.FormattedValue.ToString() == ""))
                {
                    this.dgvBase.Rows[e.RowIndex].ErrorText = "请选择查询字段";
                    e.Cancel = true;
                }
            }
            else if ((e.ColumnIndex == this.clnValue.DisplayIndex) && (e.FormattedValue != null))
            {
                string str3 = e.FormattedValue.ToString();
                object obj2 = this.dgvBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if ((obj2 != null) && (obj2.ToString() == "0"))
                {
                    str3 = obj2.ToString();
                }
                if (str3 == "")
                {
                    this.dgvBase.Rows[e.RowIndex].ErrorText = "参数值不能为空";
                    e.Cancel = true;
                }
                else
                {
                    DataField tag = this.dgvBase.Rows[e.RowIndex].Cells["clnField"].Tag as DataField;
                    if ((tag != null) && this.method_12(str3, tag.DataType))
                    {
                        this.dgvBase.Rows[e.RowIndex].ErrorText = "设置参数值无效";
                        e.Cancel = true;
                    }
                }
            }
            if (this.method_11(row))
            {
                this.tsbAdd.Enabled = true;
                this.tsbSelect.Enabled = true;
            }
            else
            {
                this.tsbAdd.Enabled = false;
                this.tsbSelect.Enabled = false;
            }
        }

        private void dgvBase_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataField field;
            DataGridViewRow row = this.dgvBase.Rows[e.RowIndex];
            this.dgvBase.Rows[e.RowIndex].ErrorText = "";
            IEnumerator enumerator = row.Cells.GetEnumerator();
            {
                while (enumerator.MoveNext())
                {
                    DataGridViewCell current = (DataGridViewCell) enumerator.Current;
                    if ((current.Value == null) || (current.Value.ToString() == ""))
                    {
                        goto Label_007E;
                    }
                }
                goto Label_00A9;
            Label_007E:
                e.Cancel = true;
                row.ErrorText = "当前行数据不完整";
                return;
            }
        Label_00A9:
            field = this.dgvBase.Rows[e.RowIndex].Cells["clnField"].Tag as DataField;
            object obj2 = this.dgvBase.Rows[e.RowIndex].Cells["clnValue"].Value;
            object obj3 = this.dgvBase.Rows[e.RowIndex].Cells["clnRel"].Value;
            List<string> list = new List<string>(this.string_4);
            List<string> list2 = new List<string>(this.string_3);
            if ((field != null) && (((field.DataType == FieldType.DataField) && !list.Contains(obj3.ToString())) || ((field.DataType != FieldType.DataField) && !list2.Contains(obj3.ToString()))))
            {
                row.ErrorText = "关系符号不正确";
                e.Cancel = true;
            }
            if (((field != null) && (obj2 != null)) && this.method_12(obj2.ToString(), field.DataType))
            {
                row.ErrorText = "设置参数值无效";
                e.Cancel = true;
            }
        }

        private void dgvBase_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvBase.SelectedRows.Count > 0)
            {
                DataGridViewRow row = this.dgvBase.SelectedRows[0];
                if (this.method_11(row))
                {
                    this.tsbAdd.Enabled = true;
                    this.tsbSelect.Enabled = true;
                }
                else
                {
                    this.tsbAdd.Enabled = false;
                    this.tsbSelect.Enabled = false;
                }
                this.method_10(row);
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
            this.tsbDelete = new ToolStripButton();
            this.tsbAdd = new ToolStripButton();
            this.tsbSelect = new ToolStripButton();
            this.dgvBase = new CustomStyleDataGrid();
            this.clnName = new DataGridViewTextBoxColumn();
            this.clnField = new DataGridViewTextBoxColumn();
            this.clnRel = new DataGridViewTextBoxColumn();
            this.clnValue = new DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((ISupportInitialize) this.dgvBase).BeginInit();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.tsbExit, this.tsbDelete, this.tsbAdd, this.tsbSelect });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x252, 0x19);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.tsbExit.Alignment = ToolStripItemAlignment.Right;
            this.tsbExit.Image = Resources.smethod_4();
            this.tsbExit.ImageTransparentColor = Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new Size(0x34, 0x16);
            this.tsbExit.Text = "退出";
            this.tsbExit.Click += new EventHandler(this.tsbExit_Click);
            this.tsbDelete.Alignment = ToolStripItemAlignment.Right;
            this.tsbDelete.Image = Resources.smethod_3();
            this.tsbDelete.ImageTransparentColor = Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.RightToLeft = RightToLeft.No;
            this.tsbDelete.Size = new Size(0x34, 0x16);
            this.tsbDelete.Text = "删除";
            this.tsbDelete.TextAlign = ContentAlignment.MiddleRight;
            this.tsbDelete.Click += new EventHandler(this.tsbDelete_Click);
            this.tsbAdd.Alignment = ToolStripItemAlignment.Right;
            this.tsbAdd.Image = Resources.smethod_5();
            this.tsbAdd.ImageTransparentColor = Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new Size(0x34, 0x16);
            this.tsbAdd.Text = "添加";
            this.tsbAdd.Click += new EventHandler(this.tsbAdd_Click);
            this.tsbSelect.Alignment = ToolStripItemAlignment.Right;
            this.tsbSelect.Image = Resources.smethod_1();
            this.tsbSelect.ImageTransparentColor = Color.Magenta;
            this.tsbSelect.Name = "tsbSelect";
            this.tsbSelect.Size = new Size(0x4c, 0x16);
            this.tsbSelect.Text = "执行查找";
            this.tsbSelect.Click += new EventHandler(this.tsbSelect_Click);
            this.dgvBase.AborCellPainting = false;
            this.dgvBase.AllowColumnHeadersVisible = true;
            this.dgvBase.AllowUserToAddRows = false;
            this.dgvBase.AllowUserToDeleteRows = false;
            this.dgvBase.AllowUserToResizeRows = false;
            style.BackColor = Color.FromArgb(0xff, 250, 240);
            this.dgvBase.AlternatingRowsDefaultCellStyle = style;
            this.dgvBase.BackgroundColor = SystemColors.ButtonFace;
            this.dgvBase.BorderStyle = BorderStyle.None;
            this.dgvBase.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            this.dgvBase.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style2.BackColor = Color.FromArgb(0x15, 0x87, 0xca);
            style2.Font = new Font("Microsoft YaHei", 10f);
            style2.ForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.dgvBase.ColumnHeadersDefaultCellStyle = style2;
            this.dgvBase.ColumnHeadersHeight = 0;
            this.dgvBase.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBase.Columns.AddRange(new DataGridViewColumn[] { this.clnName, this.clnField, this.clnRel, this.clnValue });
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = Color.FromArgb(220, 230, 240);
            style3.Font = new Font("SimSun", 10f);
            style3.ForeColor = Color.FromArgb(0, 0, 0);
            style3.SelectionBackColor = Color.FromArgb(0x7d, 150, 0xb9);
            style3.SelectionForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            style3.WrapMode = DataGridViewTriState.False;
            this.dgvBase.DefaultCellStyle = style3;
            this.dgvBase.EnableHeadersVisualStyles = false;
            this.dgvBase.GridColor = Color.Gray;
            this.dgvBase.GridStyle = CustomStyle.custom;
            this.dgvBase.Location = new Point(6, 0x1c);
            this.dgvBase.MultiSelect = false;
            this.dgvBase.Name = "dgvBase";
            this.dgvBase.RowHeadersVisible = false;
            style4.BackColor = Color.White;
            style4.SelectionBackColor = Color.SlateGray;
            this.dgvBase.RowsDefaultCellStyle = style4;
            this.dgvBase.RowTemplate.Height = 0x17;
            this.dgvBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvBase.Size = new Size(0x246, 0x1d5);
            this.dgvBase.TabIndex = 1;
            this.dgvBase.CellEnter += new DataGridViewCellEventHandler(this.dgvBase_CellEnter);
            this.dgvBase.CellLeave += new DataGridViewCellEventHandler(this.dgvBase_CellLeave);
            this.dgvBase.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgvBase_CellValidating);
            this.dgvBase.RowValidating += new DataGridViewCellCancelEventHandler(this.dgvBase_RowValidating);
            this.dgvBase.SelectionChanged += new EventHandler(this.dgvBase_SelectionChanged);
            this.clnName.HeaderText = "条件名称";
            this.clnName.Name = "clnName";
            this.clnName.Width = 140;
            this.clnField.HeaderText = "查询字段";
            this.clnField.Name = "clnField";
            this.clnField.Resizable = DataGridViewTriState.True;
            this.clnField.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clnField.Width = 140;
            this.clnRel.FillWeight = 90f;
            this.clnRel.HeaderText = "关系符号";
            this.clnRel.Name = "clnRel";
            this.clnRel.Resizable = DataGridViewTriState.True;
            this.clnRel.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.clnValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.clnValue.HeaderText = "查询参数";
            this.clnValue.Name = "clnValue";
            this.clnValue.Resizable = DataGridViewTriState.True;
            this.clnValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x252, 0x1f1);
            base.Controls.Add(this.toolStrip1);
            base.Controls.Add(this.dgvBase);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "MutiConditionQuery";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "组合条件查询";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((ISupportInitialize) this.dgvBase).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0()
        {
            if (File.Exists(this.string_1))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.string_1);
                XmlNodeList list = document.SelectNodes("/Conditions/Condition");
                if (list != null)
                {
                    foreach (XmlNode node in list)
                    {
                        string innerText = node.SelectSingleNode("Name").InnerText;
                        string str2 = node.SelectSingleNode("Field").InnerText;
                        string str3 = node.SelectSingleNode("Operator").InnerText;
                        string str4 = node.SelectSingleNode("Value").InnerText;
                        this.dgvBase.Rows.Add(new object[] { innerText, str2, str3, str4 });
                        DataField field = new DataField(innerText, innerText) {
                            DataType = FieldType.DataField
                        };
                        this.dictionary_1.Add(innerText, field);
                    }
                    this.method_1();
                }
            }
        }

        private void method_1()
        {
            for (int i = 0; i < this.dgvBase.Rows.Count; i++)
            {
                object obj3 = this.dgvBase.Rows[i].Cells["clnField"].Value;
                if (obj3 != null)
                {
                    if (this.dictionary_1.ContainsKey(obj3.ToString()))
                    {
                        this.dgvBase.Rows[i].Cells["clnField"].Tag = this.dictionary_1[obj3.ToString()];
                        object obj2 = this.dgvBase.Rows[i].Cells["clnValue"].Value;
                        if ((obj2 != null) && this.dictionary_1.ContainsKey(obj2.ToString()))
                        {
                            this.dgvBase.Rows[i].Cells["clnValue"].Tag = this.dictionary_1[obj2.ToString()];
                        }
                    }
                    else
                    {
                        using (List<DataField>.Enumerator enumerator = this.list_1.GetEnumerator())
                        {
                            DataField current;
                            while (enumerator.MoveNext())
                            {
                                current = enumerator.Current;
                                if (current.Name == obj3.ToString())
                                {
                                    goto Label_0130;
                                }
                            }
                            continue;
                        Label_0130:
                            this.dgvBase.Rows[i].Cells["clnField"].Tag = current;
                        }
                    }
                }
            }
        }

        private void method_10(DataGridViewRow dataGridViewRow_0)
        {
            object obj2 = dataGridViewRow_0.Cells["clnName"].Value;
            if (obj2 != null)
            {
                this.tsbDelete.Enabled = true;
                IEnumerator enumerator = ((IEnumerable)this.dgvBase.Rows).GetEnumerator();
                {
                    while (enumerator.MoveNext())
                    {
                        DataGridViewRow current = (DataGridViewRow) enumerator.Current;
                        DataField tag = current.Cells["clnField"].Tag as DataField;
                        if (((tag != null) && (tag.DataType == FieldType.DataField)) && (tag.Name == obj2.ToString()))
                        {
                            goto Label_00D0;
                        }
                        DataField field2 = current.Cells["clnValue"].Tag as DataField;
                        if (((field2 != null) && (field2.DataType == FieldType.DataField)) && (field2.Name == obj2.ToString()))
                        {
                            goto Label_00DE;
                        }
                    }
                    return;
                Label_00D0:
                    this.tsbDelete.Enabled = false;
                    return;
                Label_00DE:
                    this.tsbDelete.Enabled = false;
                }
            }
        }

        private bool method_11(DataGridViewRow dataGridViewRow_0)
        {
            bool flag = true;
            IEnumerator enumerator = dataGridViewRow_0.Cells.GetEnumerator();
            {
                while (enumerator.MoveNext())
                {
                    DataGridViewCell current = (DataGridViewCell) enumerator.Current;
                    object editedFormattedValue = current.Value;
                    if (current.IsInEditMode && ((editedFormattedValue == null) || (editedFormattedValue.ToString() != "0")))
                    {
                        editedFormattedValue = current.EditedFormattedValue;
                    }
                    if ((editedFormattedValue == null) || (editedFormattedValue.ToString() == ""))
                    {
                        goto Label_0067;
                    }
                }
                return flag;
            Label_0067:
                flag = false;
            }
            return flag;
        }

        private bool method_12(string string_5, FieldType fieldType_0)
        {
            if ((string_5 == "|空值|") || (string_5 == "|参数|"))
            {
                return false;
            }
            bool flag = false;
            if (fieldType_0 == FieldType.String)
            {
                flag = true;
            }
            else if (fieldType_0 == FieldType.Int)
            {
                decimal num;
                float num2;
                int num3;
                flag = (int.TryParse(string_5, out num3) || float.TryParse(string_5, out num2)) || decimal.TryParse(string_5, out num);
            }
            else if (fieldType_0 == FieldType.Bool)
            {
                bool flag2;
                flag = bool.TryParse(string_5, out flag2);
            }
            else if (fieldType_0 == FieldType.DateTime)
            {
                DateTime time;
                flag = DateTime.TryParse(string_5, out time);
            }
            else if (fieldType_0 == FieldType.DataField)
            {
                flag = this.dictionary_1.ContainsKey(string_5);
            }
            return !flag;
        }

        private bool method_13(DataGridViewCell dataGridViewCell_0)
        {
            string str = dataGridViewCell_0.EditedFormattedValue.ToString();
            foreach (DataGridViewRow row in (IEnumerable) this.dgvBase.Rows)
            {
                if ((row.Index != dataGridViewCell_0.RowIndex) && (row.Cells[dataGridViewCell_0.ColumnIndex].Value.ToString() == str))
                {
                    return true;
                }
            }
            return false;
        }

        private void method_14()
        {
            Queue<DataGridViewRow> queue = new Queue<DataGridViewRow>();
            for (int i = 0; i < this.dgvBase.Rows.Count; i++)
            {
                DataGridViewRow item = this.dgvBase.Rows[i];
                queue.Enqueue(item);
            }
            while (queue.Count > 0)
            {
                Condition condition;
                DataGridViewRow row = queue.Dequeue();
                DataField tag = row.Cells["clnField"].Tag as DataField;
                row.Cells["clnName"].Value.ToString();
                if ((tag != null) && (tag.DataType != FieldType.DataField))
                {
                    condition = this.method_15(row);
                }
                else
                {
                    condition = this.method_16(row);
                }
                if (condition != null)
                {
                    this.dictionary_2.Add(condition.Name, condition);
                }
            }
        }

        private Condition method_15(DataGridViewRow dataGridViewRow_0)
        {
            DataField tag = dataGridViewRow_0.Cells["clnField"].Tag as DataField;
            if ((tag != null) && (tag.DataType != FieldType.DataField))
            {
                BaseCondition condition = new BaseCondition {
                    Name = dataGridViewRow_0.Cells["clnName"].Value.ToString(),
                    DataField = tag
                };
                string str = dataGridViewRow_0.Cells["clnRel"].Value.ToString();
                ArithOperator @operator = (ArithOperator) Enum.Parse(typeof(ArithOperator), str);
                condition.Operator = @operator;
                condition.ValueType = tag.DataType;
                condition.Value = dataGridViewRow_0.Cells["clnValue"].Value.ToString();
                dataGridViewRow_0.Tag = condition;
                return condition;
            }
            return null;
        }

        private Condition method_16(DataGridViewRow dataGridViewRow_0)
        {
            DataField tag = dataGridViewRow_0.Cells["clnField"].Tag as DataField;
            if ((tag != null) && (tag.DataType == FieldType.DataField))
            {
                Condition condition2;
                Condition condition3;
                DataField field2 = dataGridViewRow_0.Cells["clnValue"].Tag as DataField;
                if ((((tag.Name != null) && this.dictionary_2.TryGetValue(tag.Name, out condition2)) && ((field2 != null) && (field2.Name != null))) && this.dictionary_2.TryGetValue(field2.Name, out condition3))
                {
                    CompCondition condition = new CompCondition {
                        Name = dataGridViewRow_0.Cells["clnName"].Value.ToString(),
                        LeftCondition = condition2
                    };
                    string str = dataGridViewRow_0.Cells["clnRel"].Value.ToString();
                    LogicOperator @operator = (LogicOperator) Enum.Parse(typeof(LogicOperator), str);
                    condition.Operator = @operator;
                    condition.RightCondition = condition3;
                    condition.ValueType = FieldType.DataField;
                    dataGridViewRow_0.Tag = condition;
                    return condition;
                }
            }
            return null;
        }

        private void method_17()
        {
            if (!Directory.Exists(this.string_0))
            {
                Directory.CreateDirectory(this.string_0);
            }
            XmlDocument document = new XmlDocument();
            document.CreateXmlDeclaration("1.0", null, null);
            XmlNode newChild = document.CreateElement("Conditions");
            foreach (DataGridViewRow row in (IEnumerable) this.dgvBase.Rows)
            {
                XmlElement element = document.CreateElement("Condition");
                XmlElement element2 = document.CreateElement("Name");
                XmlText text = document.CreateTextNode(row.Cells["clnName"].Value.ToString());
                element2.AppendChild(text);
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("Field");
                XmlText text2 = document.CreateTextNode(row.Cells["clnField"].Value.ToString());
                element3.AppendChild(text2);
                element.AppendChild(element3);
                XmlElement element4 = document.CreateElement("Operator");
                XmlText text3 = document.CreateTextNode(row.Cells["clnRel"].Value.ToString());
                element4.AppendChild(text3);
                element.AppendChild(element4);
                XmlElement element5 = document.CreateElement("Value");
                string str = "";
                if ((row.Tag != null) && (row.Tag is Condition))
                {
                    str = ((Condition) row.Tag).ValueType.ToString();
                }
                XmlAttribute node = document.CreateAttribute("ValueType");
                node.Value = str;
                element5.Attributes.Append(node);
                XmlText text4 = document.CreateTextNode(row.Cells["clnValue"].Value.ToString());
                element5.AppendChild(text4);
                element.AppendChild(element5);
                newChild.AppendChild(element);
            }
            document.AppendChild(newChild);
            document.Save(this.string_1);
        }

        private bool method_18(Condition condition_0)
        {
            List<BaseCondition> collection = new List<BaseCondition>();
            Stack<Condition> stack = new Stack<Condition>();
            stack.Push(condition_0);
            while (stack.Count > 0)
            {
                Condition condition = stack.Pop();
                if (condition is CompCondition)
                {
                    stack.Push(((CompCondition) condition).RightCondition);
                    stack.Push(((CompCondition) condition).LeftCondition);
                }
                else if (((condition is BaseCondition) && condition.Expression.Contains("|参数|")) && !collection.Contains((BaseCondition) condition))
                {
                    collection.Add((BaseCondition) condition);
                }
            }
            QueryArgsForm form = new QueryArgsForm();
            form.ArgsList.AddRange(collection);
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.string_2 = condition_0.Expression;
                return true;
            }
            return false;
        }

        private void method_2()
        {
            this.comboBox_2.Items.AddRange(new string[] { "|空值|", "|参数|" });
            this.dictionary_0.Add(FieldType.String, this.comboBox_2);
            ComboBox box = new ComboBox();
            box.Items.AddRange(new string[] { "0", "|空值|", "|参数|" });
            box.Visible = false;
            this.dictionary_0.Add(FieldType.Int, box);
            this.dgvBase.Controls.Add(box);
            ComboBox box2 = new ComboBox();
            box2.Items.AddRange(new string[] { "|空值|", "True", "False" });
            box2.Visible = false;
            this.dictionary_0.Add(FieldType.Bool, box2);
            this.dgvBase.Controls.Add(box2);
            ComboBox box3 = new ComboBox();
            box3.Items.AddRange(new string[] { DateTime.Today.ToShortDateString(), "|空值|", "|参数|" });
            box3.Visible = false;
            this.dictionary_0.Add(FieldType.DateTime, box3);
            this.dgvBase.Controls.Add(box3);
            this.comboBox_3.Items.Clear();
            this.dictionary_0.Add(FieldType.DataField, this.comboBox_3);
        }

        private void method_3(int int_2)
        {
            this.method_5(int_2);
            object obj2 = this.dgvBase.Rows[int_2].Cells["clnName"].Value;
            if (((obj2 != null) && (obj2.ToString() != "")) && this.dictionary_1.ContainsKey(obj2.ToString()))
            {
                DataField field = this.dictionary_1[obj2.ToString()];
                this.comboBox_0.Items.Remove(field);
                this.comboBox_3.Items.Remove(field);
                this.list_0.Clear();
                this.method_4(obj2.ToString());
                foreach (DataField field2 in this.list_0)
                {
                    this.comboBox_0.Items.Remove(field2);
                    this.comboBox_3.Items.Remove(field2);
                }
            }
            this.method_8(int_2);
        }

        private void method_4(string string_5)
        {
            for (int i = 0; i < this.dgvBase.Rows.Count; i++)
            {
                DataField tag = this.dgvBase.Rows[i].Cells["clnField"].Tag as DataField;
                if ((tag != null) && (tag.Name == string_5))
                {
                    string str = this.dgvBase.Rows[i].Cells["clnName"].Value.ToString();
                    this.list_0.Add(this.dictionary_1[str]);
                    this.method_4(str);
                }
            }
        }

        private void method_5(int int_2)
        {
            if (this.int_0 != int_2)
            {
                if ((this.int_0 != -1) & (this.int_0 < this.dgvBase.Rows.Count))
                {
                    DataField field;
                    DataGridViewRow row = this.dgvBase.Rows[this.int_0];
                    string key = row.Cells["clnName"].Value.ToString();
                    if (this.dictionary_1.ContainsKey(key))
                    {
                        field = this.dictionary_1[key];
                    }
                    else
                    {
                        field = new DataField(key, key) {
                            DataType = FieldType.DataField
                        };
                        this.dictionary_1.Add(key, field);
                    }
                    this.comboBox_0.Items.Add(field);
                    this.comboBox_3.Items.Add(field);
                }
                foreach (DataField field2 in this.list_0)
                {
                    this.comboBox_0.Items.Add(field2);
                    this.comboBox_3.Items.Add(field2);
                }
            }
        }

        private void method_6(DataGridViewCellEventArgs dataGridViewCellEventArgs_0)
        {
            DataGridViewCell cell = this.dgvBase.Rows[dataGridViewCellEventArgs_0.RowIndex].Cells[dataGridViewCellEventArgs_0.ColumnIndex];
            Rectangle rectangle = this.dgvBase.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
            this.comboBox_1.Location = rectangle.Location;
            this.comboBox_1.Size = rectangle.Size;
            this.method_9(this.comboBox_1, (string) cell.Value);
            this.comboBox_1.Visible = true;
            this.comboBox_1.Focus();
        }

        private void method_7(DataGridViewCellEventArgs dataGridViewCellEventArgs_0)
        {
            DataGridViewCell cell = this.dgvBase.Rows[dataGridViewCellEventArgs_0.RowIndex].Cells[dataGridViewCellEventArgs_0.ColumnIndex];
            Rectangle rectangle = this.dgvBase.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
            this.comboBox_0.Location = rectangle.Location;
            this.comboBox_0.Size = rectangle.Size;
            this.method_9(this.comboBox_0, (string) cell.Value);
            this.comboBox_0.Visible = true;
            this.comboBox_0.Focus();
        }

        private void method_8(int int_2)
        {
            DataField tag = this.dgvBase.Rows[int_2].Cells["clnField"].Tag as DataField;
            if (tag != null)
            {
                this.comboBox_1.Items.Clear();
                if (tag.DataType == FieldType.DataField)
                {
                    this.comboBox_1.Items.AddRange(this.string_4);
                }
                else
                {
                    this.comboBox_1.Items.AddRange(this.string_3);
                }
            }
        }

        private void method_9(ComboBox comboBox_4, string string_5)
        {
            comboBox_4.SelectedIndex = -1;
            if (string_5 == null)
            {
                comboBox_4.Text = "";
            }
            else
            {
                comboBox_4.Text = string_5;
                foreach (object obj2 in comboBox_4.Items)
                {
                    if (obj2.ToString() == string_5)
                    {
                        comboBox_4.SelectedItem = obj2;
                    }
                }
            }
        }

        private void MutiConditionQuery_Load(object sender, EventArgs e)
        {
            if (this.IsFirstLoaded)
            {
                this.string_1 = Path.Combine(this.string_0, this.string_1);
                this.comboBox_0.Items.AddRange(this.list_1.ToArray());
                this.comboBox_1.Items.AddRange(this.string_3);
                this.method_2();
                this.method_0();
                foreach (DataField field in this.dictionary_1.Values)
                {
                    this.comboBox_0.Items.Add(field);
                    this.comboBox_3.Items.Add(field);
                }
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            int num = this.dgvBase.Rows.Add();
            DataGridViewRow row = this.dgvBase.Rows[num];
            row.Cells[0].Value = "条件" + this.int_1++;
            row.Selected = true;
            row.Cells[0].Selected = true;
            this.tsbAdd.Enabled = false;
            this.tsbSelect.Enabled = false;
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            this.dgvBase.EndEdit();
            if (this.dgvBase.SelectedRows.Count > 0)
            {
                this.dgvBase.SelectedRows[0].Cells["clnName"].Selected = true;
                if (MessageBoxHelper.Show("确定要删除该行么？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    DataField field;
                    Condition condition;
                    DataGridViewRow dataGridViewRow = this.dgvBase.SelectedRows[0];
                    object obj2 = dataGridViewRow.Cells["clnName"].Value;
                    if ((obj2 != null) && this.dictionary_1.TryGetValue(obj2.ToString(), out field))
                    {
                        this.dictionary_1.Remove(field.Name);
                        this.comboBox_0.Items.Remove(field);
                        this.comboBox_3.Items.Remove(field);
                    }
                    if ((obj2 != null) && this.dictionary_2.TryGetValue(obj2.ToString(), out condition))
                    {
                        this.dictionary_2.Remove(obj2.ToString());
                    }
                    this.dgvBase.Rows.Remove(dataGridViewRow);
                    this.tsbAdd.Enabled = true;
                    this.comboBox_0.Visible = false;
                }
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void tsbSelect_Click(object sender, EventArgs e)
        {
            if (this.dgvBase.SelectedRows.Count > 0)
            {
                Condition condition;
                DataGridViewRow row = this.dgvBase.SelectedRows[0];
                row.Cells["clnName"].Selected = true;
                if (!string.IsNullOrEmpty(row.ErrorText))
                {
                    return;
                }
                this.dictionary_2.Clear();
                this.method_14();
                object obj2 = row.Cells["clnName"].Value;
                if ((obj2 == null) || !this.dictionary_2.TryGetValue(obj2.ToString(), out condition))
                {
                    this.string_2 = "";
                    MessageBoxHelper.Show("设置参数值无效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                this.string_2 = condition.Expression;
                if (this.string_2.Contains("|参数|") && !this.method_18(condition))
                {
                    return;
                }
                this.string_2 = this.string_2.Replace("!= '|空值|'", "is not null");
                this.string_2 = this.string_2.Replace("= '|空值|'", "is null");
                this.method_17();
            }
            base.DialogResult = DialogResult.OK;
        }

        public string Expression
        {
            get
            {
                return this.string_2;
            }
        }

        public List<DataField> FieldList
        {
            get
            {
                return this.list_1;
            }
        }

        public string FileName
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

        public bool IsFirstLoaded
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
    }
}

