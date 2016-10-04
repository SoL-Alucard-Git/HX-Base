namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class FaPiaoZFresult : BaseForm
    {
        private IContainer components = null;
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        private DataGridMX dataGridMX1;
        private DataTable DataSet;
        private StatusStrip statusStrip1;
        private XmlComponentLoader xmlComponentLoader1;

        public FaPiaoZFresult(DataTable Asds)
        {
            this.Initialize();
            this.DataSet = Asds;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FaPiaoZF_Load(object sender, EventArgs e)
        {
            this.dataGridMX1.get_NewColumns().Add("作废结果;ZFBZ");
            this.dataGridMX1.get_NewColumns().Add("发票种类;FPZL");
            this.dataGridMX1.get_NewColumns().Add("发票类别;FPDM");
            this.dataGridMX1.get_NewColumns().Add("发票号码;FPHM");
            this.dataGridMX1.get_NewColumns().Add("编号;BH");
            this.dataGridMX1.Bind();
            this.dataGridMX1.DataSource = this.DataSet;
            for (int i = 0; i < this.dataGridMX1.Rows.Count; i++)
            {
                string str = this.dataGridMX1.Rows[i].Cells[3].Value.ToString().PadLeft(8, '0');
                this.dataGridMX1.Rows[i].Cells[3].Value = str;
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MinimizeBox = false;
            base.MaximizeBox = false;
            this.statusStrip1 = this.xmlComponentLoader1.GetControlByName<StatusStrip>("statusStrip1");
            this.dataGridMX1 = this.xmlComponentLoader1.GetControlByName<DataGridMX>("dataGridMX1");
            this.dataGridMX1.ReadOnly = true;
            this.dataGridMX1.AllowUserToDeleteRows = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FaPiaoZFresult));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x250, 0x16e);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "查看作废结果";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.FaPiaoZFresult\Aisino.Fwkp.Wbjk.FaPiaoZFresult.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x250, 0x16e);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "FaPiaoZFresult";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "查看作废结果";
            base.Load += new EventHandler(this.FaPiaoZF_Load);
            base.ResumeLayout(false);
        }
    }
}

