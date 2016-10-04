namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class ResultForm : BaseForm
    {
        private AisinoBTN btnOK;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private ErrorResolver ErrorsResolver;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoTAB tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private AisinoTXT textBox1;
        private AisinoTXT textBox2;
        private AisinoTXT textBox3;
        private AisinoTXT textBox4;
        private XmlComponentLoader xmlComponentLoader1;

        public ResultForm(ErrorResolver ErrorResolver)
        {
            this.Initialize();
            this.ErrorsResolver = ErrorResolver;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void DataDisplay(DataGridView dgv, bool Accept)
        {
            dgv.DataSource = this.GetErrorDataTable(Accept);
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.WhiteSmoke;
            dgv.Columns[0].HeaderText = "单据编号";
            dgv.Columns[1].HeaderText = "行";
            dgv.Columns[2].HeaderText = "错误信息";
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.Columns[0].Width = 100;
            dgv.Columns[1].Width = 50;
            dgv.Columns[2].Width = 200;
            dgv.ScrollBars = ScrollBars.Both;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private DataTable GetErrorDataTable(bool Accept)
        {
            DataTable table = new DataTable();
            table.Columns.Add("DJBH", typeof(string));
            table.Columns.Add("RowIndex", typeof(int));
            table.Columns.Add("ErrowInfo", typeof(string));
            foreach (ImportErrorDetail detail in this.ErrorsResolver.GetErrorsList())
            {
                if (detail.Accept == Accept)
                {
                    DataRow row = table.NewRow();
                    row["DJBH"] = detail.DJBH;
                    row["RowIndex"] = detail.RowIndex;
                    row["ErrowInfo"] = detail.ErrowInfo;
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.tabControl1 = this.xmlComponentLoader1.GetControlByName<AisinoTAB>("tabControl1");
            this.tabPage1 = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage1");
            this.dataGridView1 = this.xmlComponentLoader1.GetControlByName<DataGridView>("dataGridView1");
            this.textBox2 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox2");
            this.textBox1 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox1");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.tabPage2 = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage2");
            this.dataGridView2 = this.xmlComponentLoader1.GetControlByName<DataGridView>("dataGridView2");
            this.textBox3 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox3");
            this.textBox4 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox4");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.ResultForm_Load);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ResultForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x18d, 0x1dd);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "结果显示";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.ResultForm\Aisino.Fwkp.Wbjk.ResultForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x18d, 0x1dd);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.MinimumSize = new Size(400, 450);
            base.Name = "ResultForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "结果显示";
            base.Load += new EventHandler(this.ResultForm_Load);
            base.ResumeLayout(false);
        }

        private void ResultForm_Load(object sender, EventArgs e)
        {
            this.DataDisplay(this.dataGridView1, true);
            this.DataDisplay(this.dataGridView2, false);
            this.textBox1.ReadOnly = true;
            this.textBox2.ReadOnly = true;
            this.textBox3.ReadOnly = true;
            this.textBox4.ReadOnly = true;
            this.textBox1.Text = this.ErrorsResolver.SaveCount.ToString();
            this.textBox3.Text = this.ErrorsResolver.AbandonCount.ToString();
            this.textBox2.Text = this.dataGridView1.RowCount.ToString();
            this.textBox4.Text = this.dataGridView2.RowCount.ToString();
        }
    }
}

