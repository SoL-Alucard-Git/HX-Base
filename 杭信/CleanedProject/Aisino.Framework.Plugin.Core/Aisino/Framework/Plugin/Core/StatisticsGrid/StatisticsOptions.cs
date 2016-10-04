namespace Aisino.Framework.Plugin.Core.StatisticsGrid
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class StatisticsOptions : BaseForm
    {
        private AisinoBTN button1;
        private AisinoBTN button2;
        private DataGridViewDisableCheckBoxColumn chkName;
        private DataGridViewDisableCheckBoxColumn ColAvg;
        private DataGridViewDisableCheckBoxColumn ColCount;
        private DataGridViewDisableCheckBoxColumn ColMax;
        private DataGridViewDisableCheckBoxColumn ColMin;
        private DataGridViewTextBoxColumn ColName;
        private DataGridViewDisableCheckBoxColumn ColSum;
        private CustomStyleDataGrid customStyleDataGrid1;
        private DataGridView dataGridView_0;
        private DataTable dataTable_0;
        private DataTable dataTable_1;
        private AisinoGRP groupBox1;
        private IContainer icontainer_2;
        private string string_0;

        public StatisticsOptions(DataGridView dataGridView_1, string string_1)
        {
            
            this.string_0 = string.Empty;
            this.InitializeComponent_1();
            try
            {
                this.dataGridView_0 = dataGridView_1;
                this.string_0 = string_1;
                this.method_3();
                base.Load += new EventHandler(this.StatisticsOptions_Load);
                this.customStyleDataGrid1.CurrentCellDirtyStateChanged += new EventHandler(this.customStyleDataGrid1_CurrentCellDirtyStateChanged);
                this.customStyleDataGrid1.CellValueChanged += new DataGridViewCellEventHandler(this.customStyleDataGrid1_CellValueChanged);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.method_2();
            this.method_4();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void customStyleDataGrid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex != -1) && (e.ColumnIndex == 0))
            {
                if (this.customStyleDataGrid1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
                {
                    DataGridViewDisableCheckBoxCell cell = (DataGridViewDisableCheckBoxCell) this.customStyleDataGrid1[e.ColumnIndex, e.RowIndex];
                    if (cell != null)
                    {
                        if (!cell.Enabled)
                        {
                            cell.Value = 0;
                            return;
                        }
                        if (Convert.ToBoolean(cell.Value))
                        {
                            for (int i = 1; i < (this.customStyleDataGrid1.Columns.Count - 1); i++)
                            {
                                if (i == 1)
                                {
                                    ((DataGridViewDisableCheckBoxCell) this.customStyleDataGrid1[i, e.RowIndex]).Value = 1;
                                }
                                else
                                {
                                    ((DataGridViewDisableCheckBoxCell) this.customStyleDataGrid1[i, e.RowIndex]).Value = 0;
                                }
                                ((DataGridViewDisableCheckBoxCell) this.customStyleDataGrid1[i, e.RowIndex]).Enabled = true;
                            }
                        }
                        else
                        {
                            for (int j = 1; j < (this.customStyleDataGrid1.Columns.Count - 1); j++)
                            {
                                ((DataGridViewDisableCheckBoxCell) this.customStyleDataGrid1[j, e.RowIndex]).Value = 0;
                                ((DataGridViewDisableCheckBoxCell) this.customStyleDataGrid1[j, e.RowIndex]).Enabled = false;
                            }
                        }
                    }
                }
                this.customStyleDataGrid1.Invalidate();
            }
        }

        private void customStyleDataGrid1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.customStyleDataGrid1.IsCurrentCellDirty)
            {
                this.customStyleDataGrid1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_2 != null))
            {
                this.icontainer_2.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(StatisticsOptions));
            this.button1 = new AisinoBTN();
            this.button2 = new AisinoBTN();
            this.groupBox1 = new AisinoGRP();
            this.customStyleDataGrid1 = new CustomStyleDataGrid();
            this.chkName = new DataGridViewDisableCheckBoxColumn();
            this.ColName = new DataGridViewTextBoxColumn();
            this.ColSum = new DataGridViewDisableCheckBoxColumn();
            this.ColAvg = new DataGridViewDisableCheckBoxColumn();
            this.ColMax = new DataGridViewDisableCheckBoxColumn();
            this.ColMin = new DataGridViewDisableCheckBoxColumn();
            this.ColCount = new DataGridViewDisableCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize) this.customStyleDataGrid1).BeginInit();
            base.SuspendLayout();
            this.button1.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.button1.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.button1.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.button1.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.button1.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.button1.Location = new Point(0xb5, 0x160);
            this.button1.Name = "button1";
            this.button1.Size = new Size(100, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "统  计";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.button2.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.button2.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.button2.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.button2.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.button2.Location = new Point(0x130, 0x160);
            this.button2.Name = "button2";
            this.button2.Size = new Size(100, 30);
            this.button2.TabIndex = 3;
            this.button2.Text = "取  消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.groupBox1.BackColor = Color.Transparent;
            this.groupBox1.Controls.Add(this.customStyleDataGrid1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x232, 0x15a);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.customStyleDataGrid1.AborCellPainting = false;
            this.customStyleDataGrid1.AllowColumnHeadersVisible = true;
            this.customStyleDataGrid1.AllowUserToAddRows = false;
            this.customStyleDataGrid1.AllowUserToDeleteRows = false;
            this.customStyleDataGrid1.AllowUserToResizeRows = false;
            style.BackColor = Color.FromArgb(0xff, 250, 240);
            this.customStyleDataGrid1.AlternatingRowsDefaultCellStyle = style;
            this.customStyleDataGrid1.BackgroundColor = SystemColors.ButtonFace;
            this.customStyleDataGrid1.BorderStyle = BorderStyle.None;
            this.customStyleDataGrid1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            this.customStyleDataGrid1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style2.BackColor = Color.FromArgb(0x15, 0x87, 0xca);
            style2.Font = new Font("微软雅黑", 10f);
            style2.ForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.customStyleDataGrid1.ColumnHeadersDefaultCellStyle = style2;
            this.customStyleDataGrid1.ColumnHeadersHeight = 0;
            this.customStyleDataGrid1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customStyleDataGrid1.Columns.AddRange(new DataGridViewColumn[] { this.chkName, this.ColName, this.ColSum, this.ColAvg, this.ColMax, this.ColMin, this.ColCount });
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = Color.FromArgb(220, 230, 240);
            style3.Font = new Font("宋体", 10f);
            style3.ForeColor = Color.FromArgb(0, 0, 0);
            style3.SelectionBackColor = Color.FromArgb(0x7d, 150, 0xb9);
            style3.SelectionForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            style3.WrapMode = DataGridViewTriState.False;
            this.customStyleDataGrid1.DefaultCellStyle = style3;
            this.customStyleDataGrid1.Dock = DockStyle.Top;
            this.customStyleDataGrid1.EnableHeadersVisualStyles = false;
            this.customStyleDataGrid1.GridColor = Color.Gray;
            this.customStyleDataGrid1.GridStyle = CustomStyle.custom;
            this.customStyleDataGrid1.Location = new Point(3, 0x11);
            this.customStyleDataGrid1.MultiSelect = false;
            this.customStyleDataGrid1.Name = "customStyleDataGrid1";
            this.customStyleDataGrid1.RowHeadersVisible = false;
            style4.BackColor = Color.White;
            style4.SelectionBackColor = Color.SlateGray;
            this.customStyleDataGrid1.RowsDefaultCellStyle = style4;
            this.customStyleDataGrid1.RowTemplate.Height = 0x17;
            this.customStyleDataGrid1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.customStyleDataGrid1.Size = new Size(0x22c, 0x143);
            this.customStyleDataGrid1.TabIndex = 0;
            this.chkName.DataPropertyName = "chkCol";
            this.chkName.Frozen = true;
            this.chkName.HeaderText = "";
            this.chkName.Name = "chkName";
            this.chkName.Resizable = DataGridViewTriState.False;
            this.chkName.Width = 20;
            this.ColName.DataPropertyName = "ColumnName";
            style5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ColName.DefaultCellStyle = style5;
            this.ColName.Frozen = true;
            this.ColName.HeaderText = "汇总栏目";
            this.ColName.Name = "ColName";
            this.ColName.ReadOnly = true;
            this.ColName.Resizable = DataGridViewTriState.True;
            this.ColName.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.ColName.Width = 80;
            this.ColSum.DataPropertyName = "sumCol";
            this.ColSum.HeaderText = "求和(Sum)";
            this.ColSum.Name = "ColSum";
            this.ColSum.Resizable = DataGridViewTriState.True;
            this.ColAvg.DataPropertyName = "avgCol";
            this.ColAvg.HeaderText = "平均值(Avg)";
            this.ColAvg.Name = "ColAvg";
            this.ColAvg.Resizable = DataGridViewTriState.True;
            this.ColMax.DataPropertyName = "maxCol";
            this.ColMax.HeaderText = "最大值(Max)";
            this.ColMax.Name = "ColMax";
            this.ColMax.Resizable = DataGridViewTriState.True;
            this.ColMin.DataPropertyName = "minCol";
            this.ColMin.HeaderText = "最小值(Min)";
            this.ColMin.Name = "ColMin";
            this.ColMin.Resizable = DataGridViewTriState.True;
            this.ColCount.DataPropertyName = "countCol";
            this.ColCount.HeaderText = "值的个数";
            this.ColCount.Name = "ColCount";
            this.ColCount.Resizable = DataGridViewTriState.True;
            this.ColCount.SortMode = DataGridViewColumnSortMode.Automatic;
            this.ColCount.ThreeState = true;
            this.ColCount.Visible = false;
            this.ColCount.Width = 80;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x232, 0x18f);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.HelpButton = true;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "StatisticsOptions";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "统计选项";
            this.groupBox1.ResumeLayout(false);
            ((ISupportInitialize) this.customStyleDataGrid1).EndInit();
            base.ResumeLayout(false);
        }

        private void method_1()
        {
            SortedDictionary<string, string> dictionary = null;
            dictionary = SerializeUtil.Deserialize(true, this.string_0) as SortedDictionary<string, string>;
            if (dictionary != null)
            {
                for (int i = 0; i < this.customStyleDataGrid1.Rows.Count; i++)
                {
                    string key = this.customStyleDataGrid1.Rows[i].Cells["ColName"].Value.ToString();
                    if (dictionary.ContainsKey(key))
                    {
                        string[] strArray = dictionary[key].Split(new char[] { ',' });
                        for (int j = 0; j < (this.customStyleDataGrid1.Columns.Count - 1); j++)
                        {
                            ((DataGridViewDisableCheckBoxCell) this.customStyleDataGrid1[j, i]).Enabled = true;
                            ((DataGridViewDisableCheckBoxCell) this.customStyleDataGrid1[j, i]).Value = Convert.ToInt32(strArray[j]);
                        }
                    }
                }
                this.customStyleDataGrid1.Invalidate();
                base.Invalidate();
            }
        }

        private DataTable method_10()
        {
            this.dataTable_0 = new DataTable();
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("ColumnName") {
                DataType = typeof(string)
            };
            table.Columns.Add(column);
            DataColumn column2 = new DataColumn("chkName") {
                DataType = typeof(bool)
            };
            this.dataTable_0.Columns.Add(column2);
            column2 = new DataColumn("ColName") {
                DataType = typeof(bool)
            };
            this.dataTable_0.Columns.Add(column2);
            column2 = new DataColumn("ColSum") {
                DataType = typeof(bool)
            };
            this.dataTable_0.Columns.Add(column2);
            column2 = new DataColumn("ColAvg") {
                DataType = typeof(bool)
            };
            this.dataTable_0.Columns.Add(column2);
            column2 = new DataColumn("ColMax") {
                DataType = typeof(bool)
            };
            this.dataTable_0.Columns.Add(column2);
            column2 = new DataColumn("ColMin") {
                DataType = typeof(bool)
            };
            this.dataTable_0.Columns.Add(column2);
            column2 = new DataColumn("ColCount") {
                DataType = typeof(bool)
            };
            this.dataTable_0.Columns.Add(column2);
            for (int i = 0; i < this.dataGridView_0.Columns.Count; i++)
            {
                if (((this.dataGridView_0.Columns[i].DefaultCellStyle.Alignment == DataGridViewContentAlignment.TopRight) || (this.dataGridView_0.Columns[i].DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)) || (this.dataGridView_0.Columns[i].DefaultCellStyle.Alignment == DataGridViewContentAlignment.BottomRight))
                {
                    DataRow row = table.NewRow();
                    row[0] = this.dataGridView_0.Columns[i].HeaderText;
                    table.Rows.Add(row);
                    DataRow row2 = this.dataTable_0.NewRow();
                    row2[0] = true;
                    row2[1] = true;
                    row2[2] = false;
                    row2[3] = false;
                    row2[4] = false;
                    row2[5] = false;
                    row2[6] = false;
                    this.dataTable_0.Rows.Add(row2);
                }
            }
            return table;
        }

        private void method_2()
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
            string str = string.Empty;
            for (int i = 0; i < this.customStyleDataGrid1.Rows.Count; i++)
            {
                if ((((DataGridViewDisableCheckBoxCell) this.customStyleDataGrid1[0, i]).Enabled && (((DataGridViewDisableCheckBoxCell) this.customStyleDataGrid1[0, i]).Value != null)) && Convert.ToBoolean(this.customStyleDataGrid1[0, i].Value))
                {
                    str = string.Empty;
                    for (int j = 0; j < (this.customStyleDataGrid1.Columns.Count - 1); j++)
                    {
                        if (string.Empty.Equals(str))
                        {
                            str = Convert.ToBoolean(this.customStyleDataGrid1[j, i].Value) ? "1" : "0";
                        }
                        else
                        {
                            str = str + "," + (Convert.ToBoolean(this.customStyleDataGrid1[j, i].Value) ? "1" : "0");
                        }
                    }
                    dictionary.Add(this.customStyleDataGrid1.Rows[i].Cells["ColName"].Value.ToString(), str);
                }
            }
            SerializeUtil.Serialize(true, this.string_0, dictionary);
        }

        private void method_3()
        {
            this.customStyleDataGrid1.DataSource = this.method_10();
            this.customStyleDataGrid1.AllowUserToAddRows = false;
            if (this.customStyleDataGrid1.Rows.Count <= 0)
            {
                this.button1.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
            }
        }

        private void method_4()
        {
            this.dataTable_1 = new DataTable();
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("ColName") {
                DataType = typeof(string)
            };
            table.Columns.Add(column);
            column = new DataColumn("ColSum") {
                DataType = typeof(bool)
            };
            table.Columns.Add(column);
            column = new DataColumn("ColAvg") {
                DataType = typeof(bool)
            };
            table.Columns.Add(column);
            column = new DataColumn("ColMax") {
                DataType = typeof(bool)
            };
            table.Columns.Add(column);
            column = new DataColumn("ColMin") {
                DataType = typeof(bool)
            };
            table.Columns.Add(column);
            DataColumn column2 = new DataColumn("统计列") {
                DataType = typeof(string)
            };
            this.dataTable_1.Columns.Add(column2);
            column2 = new DataColumn("总和") {
                DataType = typeof(string)
            };
            this.dataTable_1.Columns.Add(column2);
            column2 = new DataColumn("平均值") {
                DataType = typeof(string)
            };
            this.dataTable_1.Columns.Add(column2);
            column2 = new DataColumn("最大值") {
                DataType = typeof(string)
            };
            this.dataTable_1.Columns.Add(column2);
            column2 = new DataColumn("最小值") {
                DataType = typeof(string)
            };
            this.dataTable_1.Columns.Add(column2);
            for (int i = 0; i < this.customStyleDataGrid1.Rows.Count; i++)
            {
                DataGridViewDisableCheckBoxCell cell = this.customStyleDataGrid1.Rows[i].Cells[0] as DataGridViewDisableCheckBoxCell;
                if (((cell != null) && (cell.Value != null)) && (cell.Enabled && Convert.ToBoolean(cell.Value)))
                {
                    DataRow row2 = table.NewRow();
                    row2[0] = this.customStyleDataGrid1.Rows[i].Cells[6].Value.ToString();
                    cell = this.customStyleDataGrid1.Rows[i].Cells[1] as DataGridViewDisableCheckBoxCell;
                    row2[1] = cell.Value;
                    cell = this.customStyleDataGrid1.Rows[i].Cells[2] as DataGridViewDisableCheckBoxCell;
                    row2[2] = cell.Value;
                    cell = this.customStyleDataGrid1.Rows[i].Cells[3] as DataGridViewDisableCheckBoxCell;
                    row2[3] = cell.Value;
                    cell = this.customStyleDataGrid1.Rows[i].Cells[4] as DataGridViewDisableCheckBoxCell;
                    row2[4] = cell.Value;
                    table.Rows.Add(row2);
                }
            }
            for (int j = 0; j < table.Rows.Count; j++)
            {
                DataRow row = this.dataTable_1.NewRow();
                row[0] = table.Rows[j][0].ToString();
                if ((bool) table.Rows[j][1])
                {
                    row[1] = this.method_6(table.Rows[j][0].ToString());
                }
                if ((bool) table.Rows[j][2])
                {
                    row[2] = this.method_7(table.Rows[j][0].ToString());
                }
                if ((bool) table.Rows[j][3])
                {
                    row[3] = this.method_8(table.Rows[j][0].ToString());
                }
                if ((bool) table.Rows[j][4])
                {
                    row[4] = this.method_9(table.Rows[j][0].ToString());
                }
                this.dataTable_1.Rows.Add(row);
            }
            if ((this.dataTable_1 != null) && (this.dataTable_1.Rows.Count > 0))
            {
                new StatisticsResult(this.dataTable_1).ShowDialog();
            }
            else
            {
                MessageBoxHelper.Show("没有要统计的行", "表格统计", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private string method_5(string string_1)
        {
            string str = string.Empty;
            IEnumerator enumerator = this.dataGridView_0.Columns.GetEnumerator();
            {
                DataGridViewColumn current;
                while (enumerator.MoveNext())
                {
                    current = (DataGridViewColumn) enumerator.Current;
                    if (current.HeaderText == string_1)
                    {
                        goto Label_003E;
                    }
                }
                return str;
            Label_003E:
                str = current.Name;
            }
            return str;
        }

        private string method_6(string string_1)
        {
            string str = string.Empty;
            double num = 0.0;
            string str2 = this.method_5(string_1);
            if (string.Empty.Equals(str2))
            {
                return str;
            }
            try
            {
                for (int i = 0; i < this.dataGridView_0.Rows.Count; i++)
                {
                    if (this.dataGridView_0.Rows[i].Cells[str2].Value != null)
                    {
                        num += Convert.ToDouble(this.dataGridView_0.Rows[i].Cells[str2].Value);
                    }
                }
                return num.ToString("F2");
            }
            catch (Exception)
            {
                num = 0.0;
                return string.Empty;
            }
        }

        private string method_7(string string_1)
        {
            string str = string.Empty;
            double num = 0.0;
            string str2 = this.method_5(string_1);
            if (string.Empty.Equals(str2))
            {
                return str;
            }
            try
            {
                for (int i = 0; i < this.dataGridView_0.Rows.Count; i++)
                {
                    if (this.dataGridView_0.Rows[i].Cells[str2].Value != null)
                    {
                        num += Convert.ToDouble(this.dataGridView_0.Rows[i].Cells[str2].Value);
                    }
                }
                double num3 = num / ((double) this.dataGridView_0.Rows.Count);
                return num3.ToString("F2");
            }
            catch (Exception)
            {
                num = 0.0;
                return string.Empty;
            }
        }

        private string method_8(string string_1)
        {
            string str = string.Empty;
            double num = 0.0;
            string str2 = this.method_5(string_1);
            if (string.Empty.Equals(str2))
            {
                return str;
            }
            try
            {
                for (int i = 0; i < this.dataGridView_0.Rows.Count; i++)
                {
                    if ((this.dataGridView_0.Rows[i].Cells[str2].Value != null) && (Convert.ToDouble(this.dataGridView_0.Rows[i].Cells[str2].Value) > num))
                    {
                        num = Convert.ToDouble(this.dataGridView_0.Rows[i].Cells[str2].Value);
                    }
                }
                return num.ToString("F2");
            }
            catch (Exception)
            {
                num = 0.0;
                return string.Empty;
            }
        }

        private string method_9(string string_1)
        {
            string str = string.Empty;
            double num = 0.0;
            string str2 = this.method_5(string_1);
            if (string.Empty.Equals(str2))
            {
                return str;
            }
            try
            {
                num = Convert.ToDouble(this.dataGridView_0.Rows[0].Cells[str2].Value);
                for (int i = 0; i < this.dataGridView_0.Rows.Count; i++)
                {
                    if ((this.dataGridView_0.Rows[i].Cells[str2].Value != null) && (Convert.ToDouble(this.dataGridView_0.Rows[i].Cells[str2].Value) < num))
                    {
                        num = Convert.ToDouble(this.dataGridView_0.Rows[i].Cells[str2].Value);
                    }
                }
                return num.ToString("F2");
            }
            catch (Exception)
            {
                num = 0.0;
                return string.Empty;
            }
        }

        private void StatisticsOptions_Load(object sender, EventArgs e)
        {
            int count = this.customStyleDataGrid1.Rows.Count;
            int num2 = this.customStyleDataGrid1.Columns.Count;
            this.customStyleDataGrid1.SuspendLayout();
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    if (this.customStyleDataGrid1[j, i] is DataGridViewDisableCheckBoxCell)
                    {
                        DataGridViewDisableCheckBoxCell cell = (DataGridViewDisableCheckBoxCell) this.customStyleDataGrid1[j, i];
                        cell.Enabled = Convert.ToBoolean(this.dataTable_0.Rows[i][this.customStyleDataGrid1.Columns[j].Name]);
                    }
                }
            }
            this.method_1();
            this.customStyleDataGrid1.ResumeLayout(false);
        }
    }
}

