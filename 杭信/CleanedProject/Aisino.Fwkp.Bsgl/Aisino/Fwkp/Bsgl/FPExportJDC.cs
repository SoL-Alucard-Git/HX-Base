namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FPExportJDC : DockForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components;
        private AisinoGRP groupBox;
        private bool isExcel = true;
        private ILog loger = LogUtil.GetLogger<ChaoshuiForm>();
        private AisinoRDO rbtExcel;
        private AisinoRDO rbtnXML;
        private XmlComponentLoader xmlComponentLoader1;

        public FPExportJDC()
        {
            this.Initial();
            this.InitLoad();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Initial()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.groupBox = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox");
            this.rbtExcel = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtExcel");
            this.rbtExcel.Checked = true;
            this.rbtnXML = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtnXML");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.rbtExcel.CheckedChanged += new EventHandler(this.rbtExcel_CheckedChanged);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FPExportJDC));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x152, 0xd5);
            this.xmlComponentLoader1.TabIndex = 1;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.FPExportJDC\Aisino.Fwkp.Bsgl.FPExportJDC.xml");
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x152, 0xd5);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.Name = "FPExportJDC";
            this.Text = "导出文件类型";
            base.ResumeLayout(false);
        }

        private void InitLoad()
        {
            this.rbtExcel.Checked = true;
            this.isExcel = true;
        }

        private void rbtExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtExcel.Checked)
            {
                this.isExcel = true;
            }
            else
            {
                this.isExcel = false;
            }
        }

        public bool IsExcel
        {
            get
            {
                return this.isExcel;
            }
        }
    }
}

