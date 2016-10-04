namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using Aisino.Fwkp.Wbjk.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AutoSplitView : Form
    {
        private SaleBill bill = null;
        private ToolStripButton BtnQuit;
        private ToolStripButton BtnSave;
        private IContainer components = null;
        private DataGridMX dgvGoods;
        private DataGridMX dgvHead;
        private DataGridMX dgvOriginal;
        private DataGridMX dgvOriginalHead;
        private AisinoGRP groupBox1;
        private AisinoGRP groupBox2;
        private AisinoGRP groupBox3;
        private AisinoGRP groupBox4;
        private AisinoGRP groupBox5;
        private AisinoGRP groupBox6;
        private AisinoSPL splitContainer1;
        private AisinoSPL splitContainer2;
        private AisinoSPL splitContainer3;
        private ToolStrip toolStrip1;

        internal AutoSplitView(SaleBill bill)
        {
            this.InitializeComponent();
            this.bill = bill;
        }

        private void AutoSplitView_Load(object sender, EventArgs e)
        {
            this.dgvOriginalHead.Columns.Clear();
            this.dgvOriginalHead.ColumnCount = 5;
            this.dgvOriginalHead.Columns[0].Name = "单据号";
            this.dgvOriginalHead.Columns[1].Name = "单据种类";
            this.dgvOriginalHead.Columns[2].Name = "购方名称";
            this.dgvOriginalHead.Columns[3].Name = "购方税号";
            this.dgvOriginalHead.Columns[4].Name = "金额合计";
            string str = ShowString.ShowFPZL(this.bill.DJZL);
            this.dgvOriginalHead.Rows.Add(new object[] { this.bill.BH, str, this.bill.GFMC, this.bill.GFSH, this.bill.JEHJ });
            this.dgvOriginalHead.Columns[4].DefaultCellStyle = this.dgvOriginalHead.styleMoney;
            this.dgvOriginalHead.ClearSelection();
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            DataGridViewCellStyle style7 = new DataGridViewCellStyle();
            DataGridViewCellStyle style8 = new DataGridViewCellStyle();
            DataGridViewCellStyle style9 = new DataGridViewCellStyle();
            DataGridViewCellStyle style10 = new DataGridViewCellStyle();
            DataGridViewCellStyle style11 = new DataGridViewCellStyle();
            DataGridViewCellStyle style12 = new DataGridViewCellStyle();
            this.dgvOriginalHead = new DataGridMX();
            this.dgvHead = new DataGridMX();
            this.groupBox5 = new AisinoGRP();
            this.groupBox3 = new AisinoGRP();
            this.splitContainer2 = new AisinoSPL();
            this.toolStrip1 = new ToolStrip();
            this.BtnSave = new ToolStripButton();
            this.splitContainer3 = new AisinoSPL();
            this.groupBox4 = new AisinoGRP();
            this.groupBox6 = new AisinoGRP();
            this.dgvGoods = new DataGridMX();
            this.groupBox1 = new AisinoGRP();
            this.groupBox2 = new AisinoGRP();
            this.splitContainer1 = new AisinoSPL();
            this.BtnQuit = new ToolStripButton();
            this.dgvOriginal = new DataGridMX();
            this.dgvOriginalHead.BeginInit();
            this.dgvHead.BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.dgvGoods.BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.dgvOriginal.BeginInit();
            base.SuspendLayout();
            this.dgvOriginalHead.set_AborCellPainting(false);
            this.dgvOriginalHead.AllowUserToAddRows = false;
            this.dgvOriginalHead.AllowUserToOrderColumns = true;
            style.BackColor = Color.FromArgb(0xff, 250, 240);
            this.dgvOriginalHead.AlternatingRowsDefaultCellStyle = style;
            this.dgvOriginalHead.BackgroundColor = SystemColors.ButtonFace;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style2.BackColor = Color.Cyan;
            this.dgvOriginalHead.ColumnHeadersDefaultCellStyle = style2;
            this.dgvOriginalHead.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
            this.dgvOriginalHead.Dock = DockStyle.Fill;
            this.dgvOriginalHead.GridColor = Color.Gray;
            this.dgvOriginalHead.set_GridStyle(0);
            this.dgvOriginalHead.set_KeyEnterConvertToTab(false);
            this.dgvOriginalHead.Location = new Point(3, 0x11);
            this.dgvOriginalHead.Name = "dgvOriginalHead";
            this.dgvOriginalHead.set_NewColumns(null);
            this.dgvOriginalHead.ReadOnly = true;
            style3.BackColor = Color.White;
            style3.SelectionBackColor = Color.Teal;
            this.dgvOriginalHead.RowsDefaultCellStyle = style3;
            this.dgvOriginalHead.RowTemplate.Height = 0x17;
            this.dgvOriginalHead.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvOriginalHead.Size = new Size(0x18b, 60);
            this.dgvOriginalHead.TabIndex = 0;
            this.dgvHead.set_AborCellPainting(false);
            this.dgvHead.AllowUserToAddRows = false;
            this.dgvHead.AllowUserToOrderColumns = true;
            style4.BackColor = Color.FromArgb(0xff, 250, 240);
            this.dgvHead.AlternatingRowsDefaultCellStyle = style4;
            this.dgvHead.BackgroundColor = SystemColors.ButtonFace;
            style5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style5.BackColor = Color.Cyan;
            this.dgvHead.ColumnHeadersDefaultCellStyle = style5;
            this.dgvHead.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
            this.dgvHead.Dock = DockStyle.Fill;
            this.dgvHead.GridColor = Color.Gray;
            this.dgvHead.set_GridStyle(0);
            this.dgvHead.set_KeyEnterConvertToTab(false);
            this.dgvHead.Location = new Point(3, 0x11);
            this.dgvHead.Name = "dgvHead";
            this.dgvHead.set_NewColumns(null);
            this.dgvHead.ReadOnly = true;
            style6.BackColor = Color.White;
            style6.SelectionBackColor = Color.Teal;
            this.dgvHead.RowsDefaultCellStyle = style6;
            this.dgvHead.RowTemplate.Height = 0x17;
            this.dgvHead.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvHead.Size = new Size(0x159, 0x83);
            this.dgvHead.TabIndex = 0;
            this.groupBox5.Controls.Add(this.dgvOriginal);
            this.groupBox5.Dock = DockStyle.Fill;
            this.groupBox5.Location = new Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x191, 0x1a6);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "单据明细";
            this.groupBox3.Controls.Add(this.dgvOriginalHead);
            this.groupBox3.Dock = DockStyle.Fill;
            this.groupBox3.Location = new Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x191, 80);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "销售单据";
            this.splitContainer2.Dock = DockStyle.Fill;
            this.splitContainer2.Location = new Point(3, 0x11);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = Orientation.Horizontal;
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer2.Size = new Size(0x191, 0x1fa);
            this.splitContainer2.SplitterDistance = 80;
            this.splitContainer2.TabIndex = 0;
            this.toolStrip1.ImageScalingSize = new Size(0x19, 0x19);
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.BtnQuit, this.BtnSave });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x318, 0x19);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            this.BtnSave.Image = Resources.客户;
            this.BtnSave.ImageScaling = ToolStripItemImageScaling.None;
            this.BtnSave.ImageTransparentColor = Color.Magenta;
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new Size(0x49, 0x16);
            this.BtnSave.Text = "确认拆分";
            this.BtnSave.Click += new EventHandler(this.BtnSave_Click);
            this.splitContainer3.Dock = DockStyle.Fill;
            this.splitContainer3.Location = new Point(3, 0x11);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = Orientation.Horizontal;
            this.splitContainer3.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer3.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer3.Size = new Size(0x15f, 0x1fa);
            this.splitContainer3.SplitterDistance = 0x97;
            this.splitContainer3.TabIndex = 0;
            this.groupBox4.Controls.Add(this.dgvHead);
            this.groupBox4.Dock = DockStyle.Fill;
            this.groupBox4.Location = new Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x15f, 0x97);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "销售单据";
            this.groupBox6.Controls.Add(this.dgvGoods);
            this.groupBox6.Dock = DockStyle.Fill;
            this.groupBox6.Location = new Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x15f, 0x15f);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "单据明细";
            this.dgvGoods.set_AborCellPainting(false);
            this.dgvGoods.AllowUserToAddRows = false;
            this.dgvGoods.AllowUserToOrderColumns = true;
            style7.BackColor = Color.FromArgb(0xff, 250, 240);
            this.dgvGoods.AlternatingRowsDefaultCellStyle = style7;
            this.dgvGoods.BackgroundColor = SystemColors.ButtonFace;
            style8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style8.BackColor = Color.Cyan;
            this.dgvGoods.ColumnHeadersDefaultCellStyle = style8;
            this.dgvGoods.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
            this.dgvGoods.Dock = DockStyle.Fill;
            this.dgvGoods.GridColor = Color.Gray;
            this.dgvGoods.set_GridStyle(0);
            this.dgvGoods.set_KeyEnterConvertToTab(false);
            this.dgvGoods.Location = new Point(3, 0x11);
            this.dgvGoods.Name = "dgvGoods";
            this.dgvGoods.set_NewColumns(null);
            this.dgvGoods.ReadOnly = true;
            style9.BackColor = Color.White;
            style9.SelectionBackColor = Color.Teal;
            this.dgvGoods.RowsDefaultCellStyle = style9;
            this.dgvGoods.RowTemplate.Height = 0x17;
            this.dgvGoods.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvGoods.Size = new Size(0x159, 0x14b);
            this.dgvGoods.TabIndex = 0;
            this.groupBox1.Controls.Add(this.splitContainer2);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x197, 0x20e);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "拆分前";
            this.groupBox2.Controls.Add(this.splitContainer3);
            this.groupBox2.Dock = DockStyle.Fill;
            this.groupBox2.Location = new Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x165, 0x20e);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "拆分后";
            this.splitContainer1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.splitContainer1.Location = new Point(12, 0x22);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new Size(0x300, 0x20e);
            this.splitContainer1.SplitterDistance = 0x197;
            this.splitContainer1.TabIndex = 3;
            this.BtnQuit.Image = Resources.退出;
            this.BtnQuit.ImageScaling = ToolStripItemImageScaling.None;
            this.BtnQuit.ImageTransparentColor = Color.Magenta;
            this.BtnQuit.Name = "BtnQuit";
            this.BtnQuit.Size = new Size(0x31, 0x16);
            this.BtnQuit.Text = "退出";
            this.BtnQuit.Click += new EventHandler(this.BtnQuit_Click);
            this.dgvOriginal.set_AborCellPainting(false);
            this.dgvOriginal.AllowUserToAddRows = false;
            this.dgvOriginal.AllowUserToOrderColumns = true;
            style10.BackColor = Color.FromArgb(0xff, 250, 240);
            this.dgvOriginal.AlternatingRowsDefaultCellStyle = style10;
            this.dgvOriginal.BackgroundColor = SystemColors.ButtonFace;
            style11.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style11.BackColor = Color.Cyan;
            this.dgvOriginal.ColumnHeadersDefaultCellStyle = style11;
            this.dgvOriginal.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
            this.dgvOriginal.Dock = DockStyle.Fill;
            this.dgvOriginal.GridColor = Color.Gray;
            this.dgvOriginal.set_GridStyle(0);
            this.dgvOriginal.set_KeyEnterConvertToTab(false);
            this.dgvOriginal.Location = new Point(3, 0x11);
            this.dgvOriginal.Name = "dgvOriginal";
            this.dgvOriginal.set_NewColumns(null);
            this.dgvOriginal.ReadOnly = true;
            style12.BackColor = Color.White;
            style12.SelectionBackColor = Color.Teal;
            this.dgvOriginal.RowsDefaultCellStyle = style12;
            this.dgvOriginal.RowTemplate.Height = 0x17;
            this.dgvOriginal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvOriginal.Size = new Size(0x18b, 0x192);
            this.dgvOriginal.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.splitContainer1);
            base.Controls.Add(this.toolStrip1);
            base.Name = "AutoSplitView";
            this.Text = "销售单据拆分自动拆分";
            base.Load += new EventHandler(this.AutoSplitView_Load);
            this.dgvOriginalHead.EndInit();
            this.dgvHead.EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.dgvGoods.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.dgvOriginal.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

