namespace Aisino.Framework.Plugin.Core.StatisticsGrid
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.PrintGrid;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class StatisticsResult : BaseForm
    {
        private CustomStyleDataGrid customStyleDataGrid1;
        private DataTable dataTable_0;
        private IContainer icontainer_2;
        private ToolStripButton tool_daying;
        private ToolStripButton tool_Exit;
        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;

        public StatisticsResult(DataTable dataTable_1)
        {
            
            this.InitializeComponent_1();
            this.customStyleDataGrid1.DataSource = dataTable_1;
            this.dataTable_0 = dataTable_1;
            this.method_1();
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(StatisticsResult));
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            this.toolStrip1 = new ToolStrip();
            this.tool_Exit = new ToolStripButton();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.tool_daying = new ToolStripButton();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.customStyleDataGrid1 = new CustomStyleDataGrid();
            this.toolStrip1.SuspendLayout();
            ((ISupportInitialize) this.customStyleDataGrid1).BeginInit();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.tool_Exit, this.toolStripSeparator1, this.tool_daying, this.toolStripSeparator2 });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(810, 0x19);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.tool_Exit.Image = (Image) manager.GetObject("tool_Exit.Image");
            this.tool_Exit.ImageTransparentColor = Color.Magenta;
            this.tool_Exit.Name = "tool_Exit";
            this.tool_Exit.Size = new Size(0x31, 0x16);
            this.tool_Exit.Text = "退出";
            this.tool_Exit.Click += new EventHandler(this.tool_Exit_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            this.tool_daying.Image = (Image) manager.GetObject("tool_daying.Image");
            this.tool_daying.ImageTransparentColor = Color.Magenta;
            this.tool_daying.Name = "tool_daying";
            this.tool_daying.Size = new Size(0x31, 0x16);
            this.tool_daying.Text = "打印";
            this.tool_daying.Click += new EventHandler(this.tool_daying_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 0x19);
            this.customStyleDataGrid1.AborCellPainting = false;
            this.customStyleDataGrid1.AllowUserToAddRows = false;
            this.customStyleDataGrid1.AllowUserToDeleteRows = false;
            style.BackColor = Color.FromArgb(0xff, 250, 240);
            this.customStyleDataGrid1.AlternatingRowsDefaultCellStyle = style;
            this.customStyleDataGrid1.BackgroundColor = SystemColors.ButtonFace;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style2.BackColor = SystemColors.Control;
            style2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style2.ForeColor = SystemColors.WindowText;
            style2.SelectionBackColor = SystemColors.Highlight;
            style2.SelectionForeColor = SystemColors.HighlightText;
            style2.WrapMode = DataGridViewTriState.True;
            this.customStyleDataGrid1.ColumnHeadersDefaultCellStyle = style2;
            this.customStyleDataGrid1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customStyleDataGrid1.Dock = DockStyle.Fill;
            this.customStyleDataGrid1.GridColor = Color.Gray;
            this.customStyleDataGrid1.GridStyle = CustomStyle.custom;
            this.customStyleDataGrid1.Location = new Point(0, 0x19);
            this.customStyleDataGrid1.Name = "customStyleDataGrid1";
            this.customStyleDataGrid1.ReadOnly = true;
            style3.Alignment = DataGridViewContentAlignment.MiddleRight;
            style3.BackColor = Color.White;
            style3.SelectionBackColor = Color.SlateGray;
            this.customStyleDataGrid1.RowsDefaultCellStyle = style3;
            this.customStyleDataGrid1.RowTemplate.Height = 0x17;
            this.customStyleDataGrid1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.customStyleDataGrid1.Size = new Size(810, 0x1d4);
            this.customStyleDataGrid1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(810, 0x1ed);
            base.Controls.Add(this.customStyleDataGrid1);
            base.Controls.Add(this.toolStrip1);
            base.HelpButton = true;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "StatisticsResult";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "统计结果";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((ISupportInitialize) this.customStyleDataGrid1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_1()
        {
            foreach (DataGridViewColumn column in this.customStyleDataGrid1.Columns)
            {
                column.Width = this.method_2(column.HeaderText);
            }
            foreach (DataRow row in this.dataTable_0.Rows)
            {
                for (int i = 0; i < this.dataTable_0.Columns.Count; i++)
                {
                    int num2 = this.method_2(row[i].ToString());
                    if (this.customStyleDataGrid1.Columns[i].Width < num2)
                    {
                        this.customStyleDataGrid1.Columns[i].Width = num2;
                    }
                }
            }
        }

        private int method_2(string string_0)
        {
            return (int) (base.CreateGraphics().MeasureString(string_0, this.customStyleDataGrid1.Font).Width * 2f);
        }

        private void tool_daying_Click(object sender, EventArgs e)
        {
            DataGridPrintTools.Print(this.customStyleDataGrid1, this, "统计结果打印", null, null, true);
        }

        private void tool_Exit_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}

