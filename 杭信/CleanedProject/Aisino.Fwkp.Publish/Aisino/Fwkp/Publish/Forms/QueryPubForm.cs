namespace Aisino.Fwkp.Publish.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Publish;
    using Aisino.Fwkp.Publish.BLL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class QueryPubForm : DockForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private IContainer components;
        private DateTimePicker enddt;
        private static IPubManager pm;
        private DateTimePicker startdt;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;

        public QueryPubForm()
        {
            this.InitializeComponent();
            this.aisinoDataGrid1.GoToPageEvent += aisinoDataGrid1_GoToPageEvent;
            this.aisinoDataGrid1.DataGridRowDbClickEvent += aisinoDataGrid1_DoubleClick;
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("Type", "Text");
            item.Add("AisinoLBL", "接收时间");
            item.Add("Property", "JSSJ");
            item.Add("FillWeight", "100");
            list.Add(item);
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("Type", "Text");
            dictionary2.Add("AisinoLBL", "类型");
            dictionary2.Add("Property", "LX");
            dictionary2.Add("FillWeight", "100");
            list.Add(dictionary2);
            Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
            dictionary3.Add("Type", "Text");
            dictionary3.Add("AisinoLBL", "标题");
            dictionary3.Add("Property", "BT");
            dictionary3.Add("FillWeight", "300");
            list.Add(dictionary3);
            Dictionary<string, string> dictionary4 = new Dictionary<string, string>();
            dictionary4.Add("Type", "Text");
            dictionary4.Add("AisinoLBL", "内容");
            dictionary4.Add("Property", "NR");
            dictionary4.Add("FillWeight", "500");
            list.Add(dictionary4);
            Dictionary<string, string> dictionary5 = new Dictionary<string, string>();
            dictionary5.Add("Type", "Text");
            dictionary5.Add("AisinoLBL", "XH");
            dictionary5.Add("Property", "XH");
            dictionary5.Add("Width", "0");
            dictionary5.Add("Visible", "False");
            list.Add(dictionary5);
            this.aisinoDataGrid1.ColumeHead = list;
            this.aisinoDataGrid1.DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.aisinoDataGrid1.DataGrid.GridStyle = CustomStyle.custom;
            this.aisinoDataGrid1.DataGrid.AllowUserToAddRows = false;
            this.aisinoDataGrid1.DataGrid.ReadOnly = true;
            ToolStripControlHost host = new ToolStripControlHost(this.enddt) {
                Alignment = ToolStripItemAlignment.Right
            };
            this.toolStrip1.Items.Insert(0, host);
            ToolStripLabel label = new ToolStripLabel("至") {
                Alignment = ToolStripItemAlignment.Right
            };
            this.toolStrip1.Items.Insert(0, label);
            ToolStripControlHost host2 = new ToolStripControlHost(this.startdt) {
                Alignment = ToolStripItemAlignment.Right
            };
            this.toolStrip1.Items.Insert(0, host2);
            ToolStripLabel label2 = new ToolStripLabel("接收日期：") {
                Alignment = ToolStripItemAlignment.Right
            };
            this.toolStrip1.Items.Insert(0, label2);
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
        }

        private void aisinoDataGrid1_DoubleClick(object sender, DataGridRowEventArgs args)
        {
            object obj2 = args.CurrentRow.Cells["XH"].Value;
            if (obj2 != null)
            {
                this.ShowMessageDetail(obj2.ToString());
            }
        }

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs args)
        {
            PropertyUtil.SetValue("PUB_PAGE_SIZE", args.PageSize.ToString());
            string start = this.startdt.Value.Date.ToString("u");
            string end = this.enddt.Value.Date.AddDays(1.0).ToString("u");
            AisinoDataSet set = pm.QueryPub(start, end, args.PageSize, args.PageNO);
            if (set != null)
            {
                this.ParseData(set.Data);
                this.aisinoDataGrid1.DataSource = set;
            }
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(QueryPubForm));
            this.startdt = new DateTimePicker();
            this.enddt = new DateTimePicker();
            this.aisinoDataGrid1 = new AisinoDataGrid();
            this.toolStrip1 = new ToolStrip();
            this.toolStripButton2 = new ToolStripButton();
            this.toolStripButton1 = new ToolStripButton();
            this.toolStripButton3 = new ToolStripButton();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.startdt.CustomFormat = "";
            this.startdt.Location = new Point(0xbb, 4);
            this.startdt.Name = "startdt";
            this.startdt.Size = new Size(0x84, 0x15);
            this.startdt.TabIndex = 0;
            this.enddt.CustomFormat = "";
            this.enddt.Location = new Point(0x160, 4);
            this.enddt.Name = "enddt";
            this.enddt.Size = new Size(0x84, 0x15);
            this.enddt.TabIndex = 1;
            this.aisinoDataGrid1.AborCellPainting = false;
            this.aisinoDataGrid1.AutoSize = true;
            this.aisinoDataGrid1.BackColor = Color.White;
            this.aisinoDataGrid1.CurrentCell = null;
            this.aisinoDataGrid1.DataSource = null;
            this.aisinoDataGrid1.Dock = DockStyle.Fill;
            this.aisinoDataGrid1.FirstDisplayedScrollingRowIndex = -1;
            this.aisinoDataGrid1.IsShowAll = false;
            this.aisinoDataGrid1.Location = new Point(0, 0x19);
            this.aisinoDataGrid1.Name = "aisinoDataGrid1";
            this.aisinoDataGrid1.ReadOnly = false;
            this.aisinoDataGrid1.RightToLeft = RightToLeft.No;
            this.aisinoDataGrid1.ShowAllChkVisible = true;
            this.aisinoDataGrid1.Size = new Size(0x372, 0x210);
            this.aisinoDataGrid1.TabIndex = 4;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripButton3, this.toolStripButton2, this.toolStripButton1 });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new Padding(0);
            this.toolStrip1.Size = new Size(0x372, 0x19);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripButton2.Image = Resources.chakanmingxi_03;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new Size(0x31, 0x16);
            this.toolStripButton2.Text = "明细";
            this.toolStripButton2.Click += new EventHandler(this.toolStripButton2_Click);
            this.toolStripButton1.Image = Resources.chazhao_03;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new Size(0x31, 0x16);
            this.toolStripButton1.Text = "查询";
            this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
            this.toolStripButton3.Image = Resources.bianmazuguanli_03;
            this.toolStripButton3.ImageTransparentColor = Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new Size(0x31, 0x16);
            this.toolStripButton3.Text = "设置";
            this.toolStripButton3.Click += new EventHandler(this.toolStripButton3_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x372, 0x229);
            base.Controls.Add(this.startdt);
            base.Controls.Add(this.enddt);
            base.Controls.Add(this.aisinoDataGrid1);
            base.Controls.Add(this.toolStrip1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "QueryPubForm";
            base.TabText = "公告信息查询";
            this.Text = "公告信息查询";
            base.Load += new EventHandler(this.QueryPubForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void ParseData(DataTable dt)
        {
            int count = dt.Rows.Count;
            int num2 = dt.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    dt.Rows[i][j] = new HtmlParser(dt.Rows[i][j].ToString()).Text();
                }
            }
        }

        private void QueryPubForm_Load(object sender, EventArgs e)
        {
            this.startdt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.toolStripButton1.PerformClick();
        }

        private void ShowMessageDetail(string xh)
        {
            new MessageDetailForm(pm.QueryPub(xh)).ShowDialog(this);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string start = this.startdt.Value.Date.ToString("u");
            string end = this.enddt.Value.Date.AddDays(1.0).ToString("u");
            int pageSize = int.Parse(PropertyUtil.GetValue("PUB_PAGE_SIZE", "15"));
            pm = new PubManager();
            AisinoDataSet set = pm.QueryPub(start, end, pageSize, 1);
            if (set != null)
            {
                this.ParseData(set.Data);
                this.aisinoDataGrid1.DataSource = set;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.aisinoDataGrid1.SelectedRows.Count > 0)
            {
                object obj2 = this.aisinoDataGrid1.SelectedRows[0].Cells["XH"].Value;
                if (obj2 != null)
                {
                    this.ShowMessageDetail(obj2.ToString());
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            new ConfigForm().ShowDialog();
        }
    }
}

