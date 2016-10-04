namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Print;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class QingDanTianKai : BaseForm
    {
        private IContainer components;
        internal CustomStyleDataGrid dataGridView1;
        private FPLX fplx;
        private bool isToMX;
        private AisinoLBL lab_fpdm;
        private AisinoLBL lab_fphm;
        private ILog loger = LogUtil.GetLogger<QingDanTianKai>();
        private StatusStrip statusStrip1;
        internal ToolStripButton tool_add;
        internal ToolStripButton tool_jg;
        private ToolStripButton tool_printqd;
        internal ToolStripButton tool_remove;
        private ToolStripButton tool_wancheng;
        internal ToolStripButton tool_zhekou;
        private ToolStripButton tool_zhuanfp;
        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripStatusLabel tss_tip;
        private XmlComponentLoader xmlComponentLoader1;

        public QingDanTianKai(bool readOnly, string fpdm, string fphm, bool isdzfp = false)
        {
            this.Initialize();
            this.dataGridView1.GridStyle = CustomStyle.invWare;
            this.dataGridView1.ImeMode = ImeMode.NoControl;
            this.dataGridView1.KeyDown += new KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.CellEnter += new DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.lab_fpdm.Text = fpdm;
            this.lab_fphm.Text = fphm;
            if (isdzfp)
            {
                this.tool_printqd.Visible = false;
                this.toolStripSeparator1.Visible = false;
            }
            if (readOnly)
            {
                this.tool_add.Visible = false;
                this.tool_remove.Visible = false;
                this.tool_zhekou.Visible = false;
                this.tool_printqd.Enabled = true;
                this.dataGridView1.ReadOnly = true;
                this.tool_zhuanfp.Visible = false;
                this.Text = "查询清单信息";
            }
            else
            {
                this.tool_wancheng.Text = "完成";
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowCount = this.dataGridView1.RowCount;
            int rowIndex = e.RowIndex;
            this.tss_tip.Text = string.Format("{0}/{1}", rowIndex + 1, rowCount);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 0x2e)
            {
                e.Handled = true;
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.dataGridView1 = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("dataGridView1");
            this.lab_fpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpdm");
            this.lab_fphm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fphm");
            this.tool_wancheng = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_wancheng");
            this.tool_printqd = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_printqd");
            this.tool_add = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_add");
            this.tool_remove = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_remove");
            this.tool_zhekou = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_zhekouqd");
            this.tool_jg = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_jiageqd");
            this.statusStrip1 = this.xmlComponentLoader1.GetControlByName<StatusStrip>("statusStrip1");
            this.tss_tip = this.xmlComponentLoader1.GetControlByName<ToolStripStatusLabel>("tss_tip");
            this.tool_zhuanfp = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_zhuanfp");
            this.toolStripSeparator1 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator1");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.tool_jg.CheckOnClick = true;
            this.tool_printqd.Enabled = false;
            this.tool_wancheng.Click += new EventHandler(this.tool_wancheng_Click);
            this.tool_printqd.Click += new EventHandler(this.tool_printqd_Click);
            this.tool_zhuanfp.Click += new EventHandler(this.tool_zhuanfp_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Text = "清单填开";
            this.xmlComponentLoader1.XMLPath=PropertyUtil.GetValue("MAIN_PATH") + @"\Config\Components\Aisino.Fwkp.Fpkj.Form.QingDanTianKai\Aisino.Fwkp.Fpkj.Form.QingDanTianKai.xml";
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.ClientSize = new Size(0x324, 0x1ef);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "QingDanTianKai";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "清单填开";
            base.ResumeLayout(false);
        }

        private void tool_printqd_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count > 0)
            {
                int num;
                string fpzl = "";
                if ((int)this.fplx == 0)
                {
                    fpzl = "s";
                }
                else if (this.fplx == (FPLX)2)
                {
                    fpzl = "c";
                }
                else if (this.fplx == (FPLX)0x33)
                {
                    fpzl = "p";
                }
                if ((fpzl != "") && int.TryParse(this.lab_fphm.Text, out num))
                {
                    this.XHQDDaYin(fpzl, this.lab_fpdm.Text, num);
                }
            }
        }

        private void tool_wancheng_Click(object sender, EventArgs e)
        {
            this.isToMX = false;
            base.DialogResult = DialogResult.OK;
        }

        private void tool_zhuanfp_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count > 0)
            {
                this.dataGridView1.CurrentCell = this.dataGridView1[0, 0];
            }
            this.isToMX = true;
            base.DialogResult = DialogResult.OK;
        }

        private void XHQDDaYin(string fpzl, string fpdm, int fphm)
        {
            string str = "";
            try
            {
                IPrint print = FPPrint.Create(fpzl, fpdm, fphm, false);
                print.Print(true);
                if (print.IsPrint == "0000")
                {
                    str = "成功";
                }
                else if (print.IsPrint != "0005")
                {
                    str = "取消";
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("销货清单打印异常：" + exception.Message);
                str = "不成功";
            }
            if (!string.IsNullOrEmpty(str))
            {
                string[] textArray1 = new string[] { fphm.ToString("00000000"), str };
                MessageManager.ShowMsgBox("INP-442222", "提示", textArray1);
            }
        }

        public FPLX Fplx
        {
            get
            {
                return this.fplx;
            }
            set
            {
                this.fplx = value;
            }
        }

        public bool IsToMX
        {
            get
            {
                return this.isToMX;
            }
        }
    }
}

