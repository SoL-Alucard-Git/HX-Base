namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DJCWXXXX : BaseForm
    {
        private IContainer components = null;
        private DataGridMX dataGridMX1;
        private int djTotal = 0;
        private List<ImportErrorDetail> listError;
        private StatusStrip statusStrip1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnQuit;
        private XmlComponentLoader xmlComponentLoader1;

        public DJCWXXXX(List<ImportErrorDetail> ListError, int TotalDJ)
        {
            this.Initialize();
            this.listError = ListError;
            this.djTotal = TotalDJ;
        }

        private void dataGridMX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBoxHelper.Show(this.dataGridMX1.Rows[e.RowIndex].Cells["XXCW"].Value.ToString(), "详细信息", MessageBoxButtons.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DJCWXXXX_Load(object sender, EventArgs e)
        {
            if (MessageBoxHelper.Show(string.Format("共检验单据 {0}张\n其中数据错误 {1}张\n按确认键查看详细错误信息……", this.djTotal, this.listError.Count), "确认对话框", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                this.dataGridMX1.get_NewColumns().Add("单据号;BH;;100");
                this.dataGridMX1.get_NewColumns().Add("详细描述;XXCW;;500");
                this.dataGridMX1.Bind();
                foreach (ImportErrorDetail detail in this.listError)
                {
                    this.dataGridMX1.Rows.Add(new object[] { detail.DJBH, detail.ErrowInfo });
                }
            }
            else
            {
                base.Close();
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.dataGridMX1 = this.xmlComponentLoader1.GetControlByName<DataGridMX>("dataGridMX1");
            this.statusStrip1 = this.xmlComponentLoader1.GetControlByName<StatusStrip>("statusStrip1");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripBtnQuit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnQuit");
            this.toolStripBtnQuit.Click += new EventHandler(this.toolStripBtnQuit_Click);
            this.dataGridMX1.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridMX1_CellDoubleClick);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DJCWXXXX));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x250, 0x1d2);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "单据数据错误信息详细描述";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.DJCWXXXX\Aisino.Fwkp.Wbjk.DJCWXXXX.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x250, 0x1d2);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "DJCWXXXX";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "单据数据错误信息详细描述";
            base.Load += new EventHandler(this.DJCWXXXX_Load);
            base.ResumeLayout(false);
        }

        private void toolStripBtnQuit_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}

