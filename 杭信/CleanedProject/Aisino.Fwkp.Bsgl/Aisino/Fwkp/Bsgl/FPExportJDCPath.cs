namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FPExportJDCPath : DockForm
    {
        private AisinoBTN btnBrowse;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private string lastPath = "";
        private ILog loger = LogUtil.GetLogger<StubFPOutput>();
        private string outPath = "";
        private AisinoTXT txtFilePath;
        private XmlComponentLoader xmlComponentLoader1;

        public FPExportJDCPath()
        {
            this.Initialize();
            base.Load += new EventHandler(this.FPExportJDCPath_Load);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = dialog.SelectedPath;
            }
            if (string.IsNullOrEmpty(this.lastPath) || (this.lastPath != this.txtFilePath.Text.Trim()))
            {
                PropertyUtil.SetValue("Aisino.Fwkp.Bsgl.FPExportJDCPath.OutputPath", this.txtFilePath.Text.Trim());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtFilePath.Text.Trim() == "")
            {
                MessageManager.ShowMsgBox("INP-251206");
                this.txtFilePath.Select();
            }
            else
            {
                this.outPath = this.txtFilePath.Text.Trim();
                PropertyUtil.SetValue("Aisino.Fwkp.Bsgl.FPExportJDCPath.OutputPath", this.txtFilePath.Text.Trim());
                base.Close();
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

        private void FPExportJDCPath_Load(object sender, EventArgs e)
        {
            this.lastPath = PropertyUtil.GetValue("Aisino.Fwkp.Bsgl.FPExportJDCPath.OutputPath");
            this.txtFilePath.Text = this.lastPath;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.txtFilePath = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtFilePath");
            this.btnBrowse = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnBrowse");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FPExportJDCPath));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1a0, 0xae);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.FPExportJDCPath\Aisino.Fwkp.Bsgl.FPExportJDCPath.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a0, 0xae);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FPExportJDCPath";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            base.set_TabText("选择传出文件路径");
            this.Text = "选择传出文件路径";
            base.ResumeLayout(false);
        }

        public string OutPath
        {
            get
            {
                return this.outPath;
            }
        }
    }
}

