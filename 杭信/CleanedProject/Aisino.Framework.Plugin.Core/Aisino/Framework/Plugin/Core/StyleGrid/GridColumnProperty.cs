namespace Aisino.Framework.Plugin.Core.StyleGrid
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    [Serializable, DefaultProperty("设置DataGridView中列的属性值")]
    public class GridColumnProperty
    {
        private bool bool_0;
        private Color color_0;
        private Color color_1;
        private Color color_2;
        private Color color_3;
        private DataGridViewColumn dataGridViewColumn_0;
        private DataGridViewContentAlignment dataGridViewContentAlignment_0;
        private DataGridViewTriState dataGridViewTriState_0;
        private int int_0;
        private int int_1;
        private string string_0;

        public event EventHandler DataModifyEvent;

        public GridColumnProperty(DataGridViewColumn dataGridViewColumn_1)
        {
            
            this.dataGridViewColumn_0 = dataGridViewColumn_1;
            this.int_0 = this.dataGridViewColumn_0.Width;
            this.bool_0 = this.dataGridViewColumn_0.Visible;
            this.dataGridViewContentAlignment_0 = this.dataGridViewColumn_0.DefaultCellStyle.Alignment;
            this.color_0 = this.dataGridViewColumn_0.DefaultCellStyle.BackColor;
            this.color_1 = this.dataGridViewColumn_0.DefaultCellStyle.ForeColor;
            this.color_2 = this.dataGridViewColumn_0.DefaultCellStyle.SelectionBackColor;
            this.color_3 = this.dataGridViewColumn_0.DefaultCellStyle.SelectionForeColor;
            this.string_0 = this.dataGridViewColumn_0.DefaultCellStyle.Format;
            this.dataGridViewTriState_0 = this.dataGridViewColumn_0.DefaultCellStyle.WrapMode;
            this.int_1 = this.dataGridViewColumn_0.DisplayIndex;
        }

        private void method_0()
        {
            if (this.DataModifyEvent != null)
            {
                this.DataModifyEvent(this, null);
            }
        }

        [Category("外观"), Description("设置列的背景色。")]
        public Color 背景色
        {
            get
            {
                return this.color_0;
            }
            set
            {
                if (!this.color_0.Equals(value))
                {
                    this.color_0 = value;
                    this.dataGridViewColumn_0.DefaultCellStyle.BackColor = value;
                    this.method_0();
                }
            }
        }

        [Description("设置列的对齐方式。"), Category("外观"), DefaultValue(0)]
        public DataGridViewContentAlignment 对齐方式
        {
            get
            {
                return this.dataGridViewContentAlignment_0;
            }
            set
            {
                if (!this.dataGridViewContentAlignment_0.Equals(value))
                {
                    this.dataGridViewContentAlignment_0 = value;
                    this.dataGridViewColumn_0.DefaultCellStyle.Alignment = value;
                    this.method_0();
                }
            }
        }

        [Category("外观"), Description("设置列的单元格文本内容的格式字符串。")]
        public string 格式字符串
        {
            get
            {
                return this.string_0;
            }
            set
            {
                if (!this.string_0.Equals(value))
                {
                    this.string_0 = value;
                    this.dataGridViewColumn_0.DefaultCellStyle.Format = value;
                    this.method_0();
                }
            }
        }

        [Description("设置列的可见性。"), Category("外观"), DefaultValue(true)]
        public bool 可见性
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                if (!this.bool_0.Equals(value))
                {
                    this.bool_0 = value;
                    this.dataGridViewColumn_0.Visible = value;
                    this.method_0();
                }
            }
        }

        [Category("外观"), DefaultValue(100), Description("设置列的宽度。")]
        public int 宽度
        {
            get
            {
                return this.int_0;
            }
            set
            {
                if (!this.int_0.Equals(value))
                {
                    this.int_0 = value;
                    this.dataGridViewColumn_0.Width = value;
                    this.method_0();
                }
            }
        }

        [DefaultValue(100), Category("位置"), Description("显示列的位置索引。")]
        public int 位置索引
        {
            get
            {
                return this.int_1;
            }
        }

        [Category("外观"), Description("设置列的选中背景色。")]
        public Color 选中背景色
        {
            get
            {
                return this.color_2;
            }
            set
            {
                if (!this.color_2.Equals(value))
                {
                    this.color_2 = value;
                    this.dataGridViewColumn_0.DefaultCellStyle.SelectionBackColor = value;
                    this.method_0();
                }
            }
        }

        [Description("设置列的选中字体颜色。"), Category("外观")]
        public Color 选中字体颜色
        {
            get
            {
                return this.color_3;
            }
            set
            {
                if (!this.color_3.Equals(value))
                {
                    this.color_3 = value;
                    this.dataGridViewColumn_0.DefaultCellStyle.SelectionForeColor = value;
                    this.method_0();
                }
            }
        }

        [DefaultValue(0), Description("设置列单元格的折行方式。"), Category("外观")]
        public DataGridViewTriState 折行方式
        {
            get
            {
                return this.dataGridViewTriState_0;
            }
            set
            {
                if (!this.dataGridViewTriState_0.Equals(value))
                {
                    this.dataGridViewTriState_0 = value;
                    this.dataGridViewColumn_0.DefaultCellStyle.WrapMode = value;
                    this.method_0();
                }
            }
        }

        [Description("设置列的字体颜色。"), Category("外观")]
        public Color 字体颜色
        {
            get
            {
                return this.color_1;
            }
            set
            {
                if (!this.color_1.Equals(value))
                {
                    this.color_1 = value;
                    this.dataGridViewColumn_0.DefaultCellStyle.ForeColor = value;
                    this.method_0();
                }
            }
        }
    }
}

