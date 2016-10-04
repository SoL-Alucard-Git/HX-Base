namespace Aisino.Framework.Plugin.Core.StyleGrid
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class PropertyForm : SkinForm
    {
        private bool bool_0;
        private AisinoBTN btnDown;
        private AisinoBTN btnUp;
        private PropertyGrid ColProperty;
        private DataGridView dataGridView_0;
        private DataTable dataTable_0;
        private Dictionary<string, DataGridViewColumn> dictionary_0;
        private Dictionary<string, DataGridViewColumn> dictionary_1;
        private IContainer icontainer_1;
        private AisinoLST listBox1;
        private object object_0;
        private string string_0;
        private string string_1;
        private string string_2;

        public PropertyForm(DataGridView dataGridView_1, object object_1, string string_3, string string_4)
        {
            
            this.dictionary_0 = new Dictionary<string, DataGridViewColumn>();
            this.dictionary_1 = new Dictionary<string, DataGridViewColumn>();
            this.dataTable_0 = new DataTable();
            this.string_0 = string.Empty;
            this.string_1 = string.Empty;
            this.string_2 = string.Empty;
            this.InitializeComponent_1();
            this.object_0 = object_1;
            this.dataGridView_0 = dataGridView_1;
            this.string_0 = string_3;
            this.dictionary_0.Clear();
            this.method_3();
            this.method_0();
            this.method_2();
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            base.FormClosing += new FormClosingEventHandler(this.PropertyForm_FormClosing);
        }

        public PropertyForm(DataGridView dataGridView_1, object object_1, string string_3, string string_4, string string_5, string string_6) : this(dataGridView_1, object_1, string_3, string_4)
        {
            
            this.string_1 = string_5;
            this.string_2 = string_6;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listBox1.SelectedIndex;
            DataGridViewColumn local1 = this.dictionary_1[selectedIndex.ToString()];
            local1.DisplayIndex++;
            this.method_5(this.dataTable_0, selectedIndex, selectedIndex + 1);
            this.method_4(this.dictionary_1, selectedIndex, selectedIndex + 1);
            this.listBox1.SelectedIndex = selectedIndex + 1;
            this.bool_0 = true;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listBox1.SelectedIndex;
            DataGridViewColumn local1 = this.dictionary_1[selectedIndex.ToString()];
            local1.DisplayIndex--;
            this.method_5(this.dataTable_0, selectedIndex, selectedIndex - 1);
            this.method_4(this.dictionary_1, selectedIndex, selectedIndex - 1);
            this.listBox1.SelectedIndex = selectedIndex - 1;
            this.bool_0 = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_1 != null))
            {
                this.icontainer_1.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(PropertyForm));
            this.ColProperty = new PropertyGrid();
            this.btnDown = new AisinoBTN();
            this.btnUp = new AisinoBTN();
            this.listBox1 = new AisinoLST();
            base.SuspendLayout();
            this.ColProperty.Dock = DockStyle.Right;
            this.ColProperty.Location = new Point(0xbc, 0);
            this.ColProperty.Name = "ColProperty";
            this.ColProperty.Size = new Size(210, 0x124);
            this.ColProperty.TabIndex = 3;
            this.btnDown.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnDown.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnDown.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnDown.Font = new Font("宋体", 9f);
            this.btnDown.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnDown.Location = new Point(0x9a, 0x8e);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new Size(0x19, 0x34);
            this.btnDown.TabIndex = 2;
            this.btnDown.Text = "↓";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new EventHandler(this.btnDown_Click);
            this.btnUp.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnUp.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnUp.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnUp.Font = new Font("宋体", 9f);
            this.btnUp.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnUp.Location = new Point(0x9a, 0x49);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new Size(0x19, 0x34);
            this.btnUp.TabIndex = 1;
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new EventHandler(this.btnUp_Click);
            this.listBox1.Dock = DockStyle.Left;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0x94, 0x124);
            this.listBox1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x18e, 0x124);
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.btnUp);
            base.Controls.Add(this.btnDown);
            base.Controls.Add(this.ColProperty);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.HelpButton = true;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PropertyForm";
            base.ShowIcon = false;
            this.Text = "网格样式定制";
            base.ResumeLayout(false);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.method_0();
            this.method_2();
        }

        private void method_0()
        {
            if (this.listBox1.SelectedItems.Count > 0)
            {
                int selectedIndex = this.listBox1.SelectedIndex;
                string str = this.dataTable_0.Rows[selectedIndex]["DispID"].ToString();
                DataGridViewColumn column = this.dictionary_0[str];
                GridColumnProperty property = new GridColumnProperty(column);
                property.DataModifyEvent += new EventHandler(this.method_1);
                this.ColProperty.SelectedObject = property;
                this.ColProperty.Refresh();
            }
        }

        private void method_1(object sender, EventArgs e)
        {
            this.bool_0 = true;
        }

        private void method_2()
        {
            if (this.listBox1.Items.Count > 0)
            {
                if (this.listBox1.SelectedItems.Count > 0)
                {
                    int selectedIndex = this.listBox1.SelectedIndex;
                    if (selectedIndex > 0)
                    {
                        this.btnUp.Enabled = true;
                    }
                    else
                    {
                        this.btnUp.Enabled = false;
                    }
                    if (selectedIndex < (this.listBox1.Items.Count - 1))
                    {
                        this.btnDown.Enabled = true;
                    }
                    else
                    {
                        this.btnDown.Enabled = false;
                    }
                }
                else
                {
                    this.btnDown.Enabled = false;
                    this.btnUp.Enabled = false;
                }
            }
        }

        private void method_3()
        {
            this.btnUp.Enabled = false;
            this.btnDown.Enabled = false;
            this.dataTable_0.Rows.Clear();
            this.dataTable_0.Columns.Clear();
            this.dictionary_0.Clear();
            DataColumn column = new DataColumn("DispID") {
                DataType = typeof(string)
            };
            this.dataTable_0.Columns.Add(column);
            column = new DataColumn("DispName") {
                DataType = typeof(string)
            };
            this.dataTable_0.Columns.Add(column);
            foreach (DataGridViewColumn column2 in this.dataGridView_0.Columns)
            {
                this.dictionary_0.Add(column2.Name, column2);
                this.dictionary_1.Add(column2.DisplayIndex.ToString(), column2);
            }
            for (int i = 0; i < this.dictionary_1.Count; i++)
            {
                DataGridViewColumn column3 = this.dictionary_1[i.ToString()];
                DataRow row = this.dataTable_0.NewRow();
                row[0] = column3.Name;
                row[1] = column3.HeaderText;
                this.dataTable_0.Rows.Add(row);
            }
            this.listBox1.DataSource = this.dataTable_0;
            this.listBox1.DisplayMember = "DispName";
        }

        private void method_4(Dictionary<string, DataGridViewColumn> cl, int int_0, int int_1)
        {
            DataGridViewColumn column = cl[int_0.ToString()];
            cl[int_0.ToString()] = cl[int_1.ToString()];
            cl[int_1.ToString()] = column;
        }

        private void method_5(DataTable dataTable_1, int int_0, int int_1)
        {
            if (int_0 != int_1)
            {
                DataRow row = dataTable_1.Rows[int_0];
                DataRow row2 = dataTable_1.NewRow();
                foreach (DataColumn column in dataTable_1.Columns)
                {
                    row2[column.ColumnName] = row[column.ColumnName];
                }
                if (int_0 > int_1)
                {
                    dataTable_1.Rows.RemoveAt(int_0);
                    dataTable_1.Rows.InsertAt(row2, int_1);
                }
                else
                {
                    this.dataTable_0.Rows.RemoveAt(int_0);
                    this.dataTable_0.Rows.InsertAt(row2, int_1);
                }
            }
        }

        private void PropertyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bool_0 && (MessageBoxHelper.Show("属性已修改，是否把修改后的样式永久保存到本地？\n", "数据保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
            {
                XmlComponentUtil.SaveDataGridViewStyles(this.string_0, this.dataGridView_0, this.string_1, this.string_2);
            }
        }
    }
}

