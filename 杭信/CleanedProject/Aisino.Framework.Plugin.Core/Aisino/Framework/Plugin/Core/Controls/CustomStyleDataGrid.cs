namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.MutiQuery;
    using Aisino.Framework.Plugin.Core.PrintGrid;
    using Aisino.Framework.Plugin.Core.StatisticsGrid;
    using Aisino.Framework.Plugin.Core.StyleGrid;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class CustomStyleDataGrid : DataGridView
    {
        private bool bool_0;
        [CompilerGenerated]
        private bool bool_1;
        private Color color_0;
        private Color color_1;
        private CustomStyle customStyle_0;
        [CompilerGenerated]
        private DataGridViewColumnHeadersHeightSizeMode dataGridViewColumnHeadersHeightSizeMode_0;
        private DataTable dataTable_0;
        private DataTable dataTable_1;
        private IContainer icontainer_0;
        private ILog ilog_0;
        [CompilerGenerated]
        private int int_0;
        private List<int> list_0;
        private MutiConditionQuery mutiConditionQuery_0;

        public event CSDGridColumnWidthChangedHandler CSDGridColumnWidthChanged;

        public CustomStyleDataGrid()
        {
            
            this.color_0 = Color.FromArgb(230, 230, 230);
            this.color_1 = Color.FromArgb(0x59, 0x59, 0x59);
            this.ilog_0 = LogUtil.GetLogger<CustomStyleDataGrid>();
            this.list_0 = new List<int>();
            this.method_0();
            this.method_2(this);
        }

        public int ColumnAdd(DataGridViewColumn dataGridViewColumn_0)
        {
            if (dataGridViewColumn_0.ReadOnly)
            {
                dataGridViewColumn_0.DefaultCellStyle.BackColor = this.color_0;
                dataGridViewColumn_0.DefaultCellStyle.ForeColor = this.color_1;
            }
            if (!base.Columns.Contains(dataGridViewColumn_0.Name))
            {
                return base.Columns.Add(dataGridViewColumn_0);
            }
            return -1;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Export(string string_0)
        {
            this.ilog_0.Debug("数据导出开始执行。");
            new DataGridUtil().Export(this);
            this.ilog_0.Debug("数据导出结束执行。");
        }

        private void method_0()
        {
            this.icontainer_0 = new Container();
        }

        private void method_1(DataGridView dataGridView_0)
        {
            base.AllowUserToResizeRows = false;
            base.ColumnHeadersHeight = SystemColor.INVGRID_TITLE_HEIGHT;
            base.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView_0.BorderStyle = BorderStyle.FixedSingle;
            dataGridView_0.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView_0.RowHeadersVisible = true;
            dataGridView_0.RowHeadersWidth = SystemColor.INVGRID_ROWHEAD_WIDTH;
            dataGridView_0.BackgroundColor = SystemColor.INVGRID_CONTROL_BACKCOLOR;
            dataGridView_0.AllowUserToAddRows = false;
            dataGridView_0.BackgroundColor = SystemColor.INVGRID_BACKCOLOR;
            dataGridView_0.EnableHeadersVisualStyles = false;
            dataGridView_0.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            DataGridViewCellStyle style = new DataGridViewCellStyle {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Font = SystemColor.INVGRID_TITLE_FONT,
                ForeColor = SystemColor.INVGRID_TITLE_FONTCOLOR,
                BackColor = SystemColor.INVGRID_TITLE_BACKCOLOR
            };
            dataGridView_0.ColumnHeadersDefaultCellStyle = style;
            dataGridView_0.RowTemplate.Height = SystemColor.INVGRID_ROW_HEIGHT;
            dataGridView_0.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            DataGridViewCellStyle style2 = new DataGridViewCellStyle {
                BackColor = SystemColor.INVGRID_ROW_BACKCOLOR,
                SelectionBackColor = SystemColor.INVGRID_ROWHOVER_BACKCOLOR,
                SelectionForeColor = SystemColor.INVGRID_TITLE_FONTCOLOR,
                Font = SystemColor.INVGRID_BODY_FONT,
                ForeColor = SystemColor.INVGRID_BODY_FONTCOLOR
            };
            dataGridView_0.DefaultCellStyle = style2;
            dataGridView_0.AlternatingRowsDefaultCellStyle.BackColor = SystemColor.INVGRID_ALTROW_BACKCOLOR;
        }

        private void method_2(DataGridView dataGridView_0)
        {
            base.AllowUserToResizeRows = false;
            base.ColumnHeadersHeight = SystemColor.GRID_TITLE_HEIGHT;
            base.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView_0.BorderStyle = BorderStyle.None;
            dataGridView_0.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_0.RowHeadersVisible = false;
            dataGridView_0.BackgroundColor = SystemColor.GRID_CONTROL_BACKCOLOR;
            dataGridView_0.AllowUserToAddRows = false;
            dataGridView_0.BackgroundColor = SystemColor.GRID_BACKCOLOR;
            dataGridView_0.EnableHeadersVisualStyles = false;
            dataGridView_0.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            DataGridViewCellStyle style = new DataGridViewCellStyle {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Font = SystemColor.GRID_TITLE_FONT,
                ForeColor = SystemColor.GRID_TITLE_FONTCOLOR,
                BackColor = SystemColor.GRID_TITLE_BACKCOLOR
            };
            dataGridView_0.ColumnHeadersDefaultCellStyle = style;
            dataGridView_0.RowTemplate.Height = SystemColor.GRID_ROW_HEIGHT;
            dataGridView_0.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            DataGridViewCellStyle style2 = new DataGridViewCellStyle {
                BackColor = SystemColor.GRID_ROW_BACKCOLOR,
                SelectionBackColor = SystemColor.GRID_ROWHOVER_BACKCOLOR,
                SelectionForeColor = SystemColor.GRID_TITLE_FONTCOLOR,
                Font = SystemColor.GRID_BODY_FONT,
                ForeColor = SystemColor.GRID_BODY_FONTCOLOR
            };
            dataGridView_0.DefaultCellStyle = style2;
            dataGridView_0.AlternatingRowsDefaultCellStyle.BackColor = SystemColor.GRID_ALTROW_BACKCOLOR;
        }

        private void method_3(DataGridViewColumnEventArgs dataGridViewColumnEventArgs_0)
        {
            if (this.CSDGridColumnWidthChanged != null)
            {
                this.CSDGridColumnWidthChanged(dataGridViewColumnEventArgs_0);
            }
        }

        private void method_4()
        {
            List<DataField> list = new List<DataField>();
            foreach (DataGridViewColumn column in base.Columns)
            {
                DataField item = new DataField(column.HeaderText, column.DataPropertyName.ToUpper());
                IEnumerator enumerator2 = this.dataTable_0.Columns.GetEnumerator();
                {
                    DataColumn current;
                    while (enumerator2.MoveNext())
                    {
                        current = (DataColumn) enumerator2.Current;
                        if (current.ColumnName == item.Field)
                        {
                            goto Label_0080;
                        }
                    }
                    goto Label_0196;
                Label_0080:
                    if (((current.DataType != typeof(int)) && (current.DataType != typeof(long))) && ((current.DataType != typeof(float)) && (current.DataType != typeof(decimal))))
                    {
                        if (current.DataType == typeof(bool))
                        {
                            item.DataType = FieldType.Bool;
                        }
                        else
                        {
                            if ((current.DataType != typeof(char)) && (current.DataType != typeof(string)))
                            {
                                if (current.DataType == typeof(DateTime))
                                {
                                    item.DataType = FieldType.DateTime;
                                }
                                else
                                {
                                    item.DataType = FieldType.String;
                                }
                                goto Label_0196;
                            }
                            item.DataType = FieldType.String;
                        }
                    }
                    else
                    {
                        item.DataType = FieldType.Int;
                    }
                }
            Label_0196:
                list.Add(item);
            }
            this.mutiConditionQuery_0.FieldList.AddRange(list.ToArray());
        }

        private void method_5(string string_0)
        {
            DataTable table = new DataTable();
            if (this.dataTable_1 == null)
            {
                this.dataTable_1 = this.dataTable_0;
            }
            table = this.dataTable_1.Clone();
            if (!string.IsNullOrEmpty(string_0))
            {
                try
                {
                    DataRow[] rowArray = this.dataTable_1.Select(string_0);
                    for (int i = 0; i < rowArray.Length; i++)
                    {
                        table.ImportRow(rowArray[i]);
                    }
                }
                catch (Exception)
                {
                }
            }
            base.DataSource = table;
            this.Refresh();
        }

        internal void method_6(string string_0, string string_1, object object_0, List<PrinterItems> header, List<PrinterItems> footer, bool bool_2)
        {
            this.ilog_0.Debug("数据打印开始执行。");
            DataGridPrintTools.smethod_0(this, string_1, object_0, string_0, header, footer, bool_2);
            this.ilog_0.Debug("数据打印结束执行。");
        }

        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            string str2;
            if ((((e.ColumnIndex >= 0) && ((str2 = base.Columns[e.ColumnIndex].Name) != null)) && ((str2 == "SL") || (str2 == "DJ"))) && ((e.Value != null) && (e.Value.ToString() == "0")))
            {
                e.Value = "";
            }
            base.OnCellFormatting(e);
        }

        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnWidthChanged(e);
            this.method_3(e);
        }

        public void Print(string string_0, object object_0, List<PrinterItems> header, List<PrinterItems> footer, bool bool_2)
        {
            this.ilog_0.Debug("数据打印开始执行。");
            DataGridPrintTools.Print(this, object_0, string_0, header, footer, bool_2);
            this.ilog_0.Debug("数据打印结束执行。");
        }

        public void Print(string string_0, object object_0, List<PrinterItems> header, List<PrinterItems> footer, bool bool_2, bool bool_3)
        {
            this.ilog_0.Debug("数据打印开始执行。");
            DataGridPrintTools.Print(bool_3, this, object_0, string_0, header, footer, bool_2);
            this.ilog_0.Debug("数据打印结束执行。");
        }

        public void Select(object object_0)
        {
            this.dataTable_0 = base.DataSource as DataTable;
            if (this.dataTable_0 != null)
            {
                this.dataTable_1 = this.dataTable_0;
                if (this.mutiConditionQuery_0 == null)
                {
                    this.mutiConditionQuery_0 = new MutiConditionQuery();
                    this.mutiConditionQuery_0.IsFirstLoaded = true;
                    this.mutiConditionQuery_0.FileName = object_0.GetType().FullName + "_" + base.Name + ".xml";
                    this.method_4();
                }
                else
                {
                    this.mutiConditionQuery_0.IsFirstLoaded = false;
                }
                if (this.mutiConditionQuery_0.ShowDialog() == DialogResult.OK)
                {
                    this.method_5(this.mutiConditionQuery_0.Expression);
                }
            }
        }

        public void SetColumnReadOnly(int int_1, bool bool_2)
        {
            if ((base.Columns.Count > int_1) && bool_2)
            {
                base.Columns[int_1].DefaultCellStyle.BackColor = this.color_0;
                base.Columns[int_1].DefaultCellStyle.ForeColor = this.color_1;
            }
        }

        public void SetColumnReadOnly(string string_0, bool bool_2)
        {
            if (base.Columns.Contains(string_0) && bool_2)
            {
                base.Columns[string_0].DefaultCellStyle.BackColor = this.color_0;
                base.Columns[string_0].DefaultCellStyle.ForeColor = this.color_1;
            }
        }

        public void SetColumnStyles(string string_0, object object_0)
        {
            this.ilog_0.Debug("格式设置开始执行,xmlFileName:" + string_0);
            if (!string.Empty.Equals(ToolUtil.CheckPath(string_0)) && File.Exists(ToolUtil.CheckPath(string_0)))
            {
                new PropertyForm(this, object_0, ToolUtil.CheckPath(string_0), object_0.GetType().FullName).ShowDialog();
                this.ilog_0.Debug("格式设置结束执行。");
            }
        }

        public void SetColumnStyles(string string_0, object object_0, string string_1, string string_2)
        {
            this.ilog_0.Debug("格式设置开始执行,xmlFileName:" + string_0);
            if (!string.Empty.Equals(ToolUtil.CheckPath(string_0)) && File.Exists(ToolUtil.CheckPath(string_0)))
            {
                new PropertyForm(this, object_0, ToolUtil.CheckPath(string_0), object_0.GetType().FullName, string_1, string_2).ShowDialog();
                this.ilog_0.Debug("格式设置结束执行。");
            }
        }

        public void Statistics(object object_0)
        {
            this.ilog_0.Debug("表格统计开始执行。");
            GridStatisticsUtil.GridStatistics(this, object_0);
            this.ilog_0.Debug("表格统计结束执行。");
        }

        public bool AborCellPainting
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

        public bool AllowColumnHeadersVisible
        {
            get
            {
                return base.ColumnHeadersVisible;
            }
            set
            {
                base.ColumnHeadersVisible = value;
            }
        }

        public bool AllowUserToResizeRows
        {
            [CompilerGenerated]
            get
            {
                return this.bool_1;
            }
            [CompilerGenerated]
            set
            {
                this.bool_1 = value;
            }
        }

        public int ColumnHeadersHeight
        {
            [CompilerGenerated]
            get
            {
                return this.int_0;
            }
            [CompilerGenerated]
            set
            {
                this.int_0 = value;
            }
        }

        public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
        {
            [CompilerGenerated]
            get
            {
                return this.dataGridViewColumnHeadersHeightSizeMode_0;
            }
            [CompilerGenerated]
            set
            {
                this.dataGridViewColumnHeadersHeightSizeMode_0 = value;
            }
        }

        public CustomStyle GridStyle
        {
            get
            {
                return this.customStyle_0;
            }
            set
            {
                if (this.customStyle_0 != value)
                {
                    this.customStyle_0 = value;
                    if (this.customStyle_0 == CustomStyle.custom)
                    {
                        this.method_2(this);
                    }
                    else
                    {
                        this.method_1(this);
                    }
                    base.Invalidate();
                }
            }
        }

        public delegate void CSDGridColumnWidthChangedHandler(DataGridViewColumnEventArgs e);
    }
}

