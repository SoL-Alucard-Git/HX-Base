namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [Description("文本接口不分页数据展示控件")]
    public class DataGridMX : CustomStyleDataGrid
    {
        private bool bool_2;
        private bool bool_3;
        private DataGridViewCellStyle dataGridViewCellStyle_0;
        private List<string> list_1;
        public DataGridViewCellStyle styleMoney;

        public DataGridMX()
        {
            
            this.styleMoney = new DataGridViewCellStyle();
            this.dataGridViewCellStyle_0 = new DataGridViewCellStyle();
            this.list_1 = new List<string>();
            base.AutoGenerateColumns = false;
            this.AutoSize = false;
            base.AllowUserToOrderColumns = true;
            base.AllowUserToAddRows = false;
            base.BackgroundColor = Color.WhiteSmoke;
            base.ReadOnly = true;
            base.ScrollBars = ScrollBars.Both;
            base.AllowUserToResizeColumns = true;
            base.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.method_7();
        }

        public void Bind()
        {
            foreach (string str in this.list_1)
            {
                string[] strArray = str.Split(new char[] { ';' });
                if (strArray.Length >= 2)
                {
                    base.Columns.Add(this.method_8(strArray));
                }
            }
            if (this.bool_2)
            {
                this.method_9("DJHXZ");
                this.bool_2 = false;
            }
            base.ClearSelection();
        }

        public void ColumnsClear()
        {
            this.list_1.Clear();
            base.Columns.Clear();
        }

        private void method_7()
        {
            this.styleMoney.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.styleMoney.ForeColor = Color.Blue;
            this.styleMoney.Format = "N2";
            this.dataGridViewCellStyle_0.Alignment = DataGridViewContentAlignment.MiddleCenter;
            base.EnableHeadersVisualStyles = true;
            base.ColumnHeadersDefaultCellStyle = this.dataGridViewCellStyle_0;
        }

        private DataGridViewColumn method_8(string[] string_0)
        {
            DataGridViewColumn column;
            if (string_0.Length == 2)
            {
                column = new DataGridViewTextBoxColumn();
            }
            else if (string_0[2] == "ComboBox")
            {
                column = new DataGridViewComboBoxColumn();
            }
            else if (string_0[2] == "AisinoCHK")
            {
                column = new DataGridViewCheckBoxColumn();
            }
            else if (string_0[2] == "Hide")
            {
                column = new DataGridViewTextBoxColumn {
                    Visible = false
                };
            }
            else if (string_0[2] == "ZKHStyle")
            {
                column = new DataGridViewTextBoxColumn {
                    Visible = false
                };
                this.bool_2 = true;
            }
            else
            {
                column = new DataGridViewTextBoxColumn();
            }
            column.HeaderText = string_0[0];
            column.Name = string_0[1];
            column.DataPropertyName = string_0[1];
            if (string_0.Length > 3)
            {
                column.Width = int.Parse(string_0[3]);
                if (string_0.Length <= 4)
                {
                    return column;
                }
                if (string_0[4] == "money")
                {
                    column.DefaultCellStyle = this.styleMoney;
                    return column;
                }
                if (string_0[4] == "MiddleRight")
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    return column;
                }
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            return column;
        }

        private void method_9(string string_0)
        {
            for (int i = 0; i < base.Rows.Count; i++)
            {
                switch (Convert.ToInt32(base.Rows[i].Cells[string_0].Value))
                {
                    case 3:
                        base.Rows[i].DefaultCellStyle.BackColor = Color.LightCyan;
                        break;

                    case 4:
                        base.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                        break;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        {
            int num = 0x100;
            int num2 = 260;
            if (this.bool_3 && ((m.Msg == num) || (m.Msg == num2)))
            {
                if (keyData == Keys.Enter)
                {
                    SendKeys.Send("{Tab}");
                    return true;
                }
            }
            return base.ProcessCmdKey(ref m, keyData);
        }

        public bool KeyEnterConvertToTab
        {
            get
            {
                return this.bool_3;
            }
            set
            {
                this.bool_3 = value;
            }
        }

        public List<string> NewColumns
        {
            get
            {
                return this.list_1;
            }
            set
            {
                this.list_1 = value;
            }
        }
    }
}

