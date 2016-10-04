namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.PrintGrid;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Model;
    using Aisino.Fwkp.Bmgl.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    internal class ImportReport : BaseForm
    {
        private IContainer components;
        private CustomStyleDataGrid dataGridView;
        private AisinoGRP groupBox1;
        private ImageList imageList1 = new ImageList();
        private ImportResult importResult;
        private ResultManager importResultBll = new ResultManager();
        private AisinoLBL label1;
        private AisinoLBL label10;
        private AisinoLBL label2;
        private AisinoLBL label5;
        private AisinoLBL label6;
        private AisinoLBL label7;
        private AisinoLBL label8;
        private AisinoLBL label9;
        private AisinoLBL labelDuplicate;
        private AisinoLBL labelFail;
        private AisinoLBL labelFromFile;
        private AisinoLBL labelInvalid;
        private AisinoLBL labelReportType;
        private AisinoLBL labelTotal;
        private AisinoLBL lableCorrect;
        private CustomListView listView1;
        private SaveFileDialog saveFileDialog1;
        private ToolStripStatusLabel StatusLabel2;
        private ToolStripStatusLabel StatusLabel3;
        private StatusStrip statusStrip1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripPrint;
        private ToolStripButton toolStripSave;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private XmlComponentLoader xmlComponentLoader1;

        internal ImportReport(ImportResult Result)
        {
            this.Initialize();
            this.SetImgList();
            this.importResult = Result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ImportReport_Load(object sender, EventArgs e)
        {
            this.dataGridView.Visible = false;
            this.listView1.Visible = true;
            this.listView1.GridLines = true;
            this.StatusLabel2.Text = "0";
            this.StatusLabel3.Text = this.importResult.Total.ToString();
            this.saveFileDialog1 = new SaveFileDialog();
            this.labelDuplicate.Text = this.importResult.Duplicated.ToString();
            this.labelFail.Text = this.importResult.Failed.ToString();
            this.labelInvalid.Text = this.importResult.Invalid.ToString();
            this.labelTotal.Text = this.importResult.Total.ToString();
            this.lableCorrect.Text = this.importResult.Correct.ToString();
            this.listView1.Columns.Add("表名", 100);
            this.listView1.Columns.Add("字段1名称", 80);
            this.listView1.Columns.Add("编码", 90);
            this.listView1.Columns.Add("字段2名称", 80);
            this.listView1.Columns.Add("名称", 150);
            this.listView1.Columns.Add("传入结果", 70);
            this.listView1.Columns.Add("原因", 220);
            this.listView1.SmallImageList = this.imageList1;
            ListViewItem item = null;
            foreach (DataRow row in this.importResult.DtResult.Rows)
            {
                if (row["Result"].Equals("正确传入"))
                {
                    item = new ListViewItem(this.importResult.ImportTable, "OK");
                }
                else if (row["Result"].Equals("重复"))
                {
                    item = new ListViewItem(this.importResult.ImportTable, "Duplicate");
                }
                else if (row["Result"].Equals("无效"))
                {
                    item = new ListViewItem(this.importResult.ImportTable, "Invalid");
                }
                else
                {
                    item = new ListViewItem(this.importResult.ImportTable, "Failed");
                }
                item.SubItems.Add("编码");
                item.SubItems.Add(row["Code"].ToString());
                item.SubItems.Add("名称");
                item.SubItems.Add(row["Name"].ToString());
                item.SubItems.Add(row["Result"].ToString());
                item.SubItems.Add(row["Reason"].ToString());
                this.listView1.Items.Add(item);
            }
            DataTable table = new DataTable();
            table.Columns.Add("BOM");
            table.Columns.Add("ZD1");
            table.Columns.Add("BM");
            table.Columns.Add("ZD2");
            table.Columns.Add("MC");
            table.Columns.Add("JG");
            table.Columns.Add("YY");
            foreach (DataRow row2 in this.importResult.DtResult.Rows)
            {
                table.Rows.Add(new object[] { this.importResult.ImportTable, "编码", row2["Code"], "名称", row2["Name"], row2["Result"], row2["Reason"] });
            }
            this.dataGridView.DataSource = table;
            this.dataGridView.Columns[0].HeaderText = "表名";
            this.dataGridView.Columns[1].HeaderText = "字段1名称";
            this.dataGridView.Columns[2].HeaderText = "编码";
            this.dataGridView.Columns[3].HeaderText = "字段2名称";
            this.dataGridView.Columns[4].HeaderText = "名称";
            this.dataGridView.Columns[5].HeaderText = "传入结果";
            this.dataGridView.Columns[6].HeaderText = "原因";
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.dataGridView = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.labelReportType = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelReportType");
            this.labelFromFile = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelFromFile");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.labelTotal = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelTotal");
            this.labelFail = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelFail");
            this.labelDuplicate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelDuplicate");
            this.labelInvalid = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelInvalid");
            this.lableCorrect = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableCorrect");
            this.label10 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label10");
            this.label9 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label9");
            this.label8 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label8");
            this.label7 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label7");
            this.label6 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label6");
            this.statusStrip1 = this.xmlComponentLoader1.GetControlByName<StatusStrip>("statusStrip1");
            this.toolStripStatusLabel1 = this.xmlComponentLoader1.GetControlByName<ToolStripStatusLabel>("toolStripStatusLabel1");
            this.StatusLabel2 = this.xmlComponentLoader1.GetControlByName<ToolStripStatusLabel>("StatusLabel2");
            this.toolStripStatusLabel2 = this.xmlComponentLoader1.GetControlByName<ToolStripStatusLabel>("toolStripStatusLabel2");
            this.StatusLabel3 = this.xmlComponentLoader1.GetControlByName<ToolStripStatusLabel>("StatusLabel3");
            this.label5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label5");
            this.listView1 = this.xmlComponentLoader1.GetControlByName<CustomListView>("listView1");
            this.toolStripPrint = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripPrint");
            this.toolStripSave = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripSave");
            this.toolStripPrint.Click += new EventHandler(this.Print_Click);
            this.toolStripSave.Click += new EventHandler(this.Save_Click);
            this.listView1.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Text = "传入报告";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.ImportReport\Aisino.Fwkp.Bmgl.Forms.ImportReport.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "ImportReport";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "传入报告";
            base.Load += new EventHandler(this.ImportReport_Load);
            base.ResumeLayout(false);
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.StatusLabel2.Text = (e.ItemIndex + 1).ToString();
        }

        private void Print_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView.Columns[0].Width = this.listView1.Columns[0].Width;
                this.dataGridView.Columns[1].Width = this.listView1.Columns[1].Width;
                this.dataGridView.Columns[2].Width = this.listView1.Columns[2].Width;
                this.dataGridView.Columns[3].Width = this.listView1.Columns[3].Width;
                this.dataGridView.Columns[4].Width = this.listView1.Columns[4].Width;
                this.dataGridView.Columns[5].Width = this.listView1.Columns[5].Width;
                this.dataGridView.Columns[6].Width = this.listView1.Columns[6].Width;
                List<PrinterItems> header = new List<PrinterItems>();
                List<PrinterItems> footer = new List<PrinterItems> {
                    new PrinterItems("报告种类：编码导入          报告来源：文本文件 ", HorizontalAlignment.Left),
                    new PrinterItems(string.Format("正确:{0}        无效:{1}       重复:{2}        失败:{3}       总计:{4}", new object[] { this.importResult.Correct, this.importResult.Invalid, this.importResult.Duplicated, this.importResult.Failed, this.importResult.Total }), HorizontalAlignment.Left),
                    new PrinterItems("详细信息", HorizontalAlignment.Left),
                    new PrinterItems("", HorizontalAlignment.Left)
                };
                DataGridPrintTools.Print(this.dataGridView, this.dataGridView.Parent, "编码导入报告", header, footer, true);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
                this.saveFileDialog1.FileName = "传入报告.txt";
                this.saveFileDialog1.RestoreDirectory = true;
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string str = this.importResultBll.SaveTxt(this.saveFileDialog1.FileName, this.importResult);
                    if (str != "0")
                    {
                        MessageBoxHelper.Show("未导出传入报告，原因：" + str);
                    }
                    else
                    {
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString());
            }
        }

        private void SetImgList()
        {
            this.imageList1.Images.Add("OK", Resources.Success);
            this.imageList1.Images.Add("Duplicate", Resources.Duplicate);
            this.imageList1.Images.Add("Failed", Resources.Failed);
            this.imageList1.Images.Add("Invalid", Resources.Invalid);
        }

        private void TSMenuItemExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}

