namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ImportSet : BaseForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoBTN btnOpenBF;
        private AisinoBTN btnOpenDR;
        private AisinoBTN btnOpenHX;
        private IContainer components;
        private FolderBrowserDialog folderDialog;
        private FPLX fplx;
        private string lastFolder;
        private AisinoTXT txtBFPath;
        private AisinoTXT txtDRPath;
        private AisinoTXT txtHXPath;
        private AisinoTXT txtInterval;
        private XmlComponentLoader xmlComponentLoader1;

        public ImportSet(FPLX fplx)
        {
            this.fplx = fplx;
            this.Initialize();
            this.txtDRPath.ReadOnly = true;
            this.txtBFPath.ReadOnly = true;
            this.txtHXPath.ReadOnly = true;
            if (((int)fplx == 0) || ((int)fplx == 2))
            {
                this.txtDRPath.Text = PropertyUtil.GetValue("FPDJ_DRPATH");
                this.txtBFPath.Text = PropertyUtil.GetValue("FPDJ_BFPATH");
                this.txtHXPath.Text = PropertyUtil.GetValue("FPDJ_HXPATH");
                this.txtInterval.Text = PropertyUtil.GetValue("FPDJ_INTERVAL");
            }
            else if ((int)fplx == 0x33)
            {
                this.txtDRPath.Text = PropertyUtil.GetValue("DZFPDJ_DRPATH");
                this.txtBFPath.Text = PropertyUtil.GetValue("DZFPDJ_BFPATH");
                this.txtHXPath.Text = PropertyUtil.GetValue("DZFPDJ_HXPATH");
                this.txtInterval.Text = PropertyUtil.GetValue("DZFPDJ_INTERVAL");
            }
            else if ((int)fplx == 11)
            {
                this.txtDRPath.Text = PropertyUtil.GetValue("HYFPDJ_DRPATH");
                this.txtBFPath.Text = PropertyUtil.GetValue("HYFPDJ_BFPATH");
                this.txtHXPath.Text = PropertyUtil.GetValue("HYFPDJ_HXPATH");
                this.txtInterval.Text = PropertyUtil.GetValue("HYFPDJ_INTERVAL");
            }
            else if ((int)fplx == 12)
            {
                this.txtDRPath.Text = PropertyUtil.GetValue("JDCFPDJ_DRPATH");
                this.txtBFPath.Text = PropertyUtil.GetValue("JDCFPDJ_BFPATH");
                this.txtHXPath.Text = PropertyUtil.GetValue("JDCFPDJ_HXPATH");
                this.txtInterval.Text = PropertyUtil.GetValue("JDCFPDJ_INTERVAL");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int num;
            if (!int.TryParse(this.txtInterval.Text.Trim(), out num))
            {
                MessageManager.ShowMsgBox("INP-242161");
            }
            else if ((num <= 0) || (num >= 0x15180))
            {
                MessageManager.ShowMsgBox("INP-242168");
            }
            else
            {
                string str = this.txtDRPath.Text.Trim();
                string str2 = this.txtBFPath.Text.Trim();
                string str3 = this.txtHXPath.Text.Trim();
                if (((str == "") || (str2 == "")) || (str3 == ""))
                {
                    MessageManager.ShowMsgBox("INP-242170");
                }
                else if (str == str2)
                {
                    string[] textArray1 = new string[] { "导入文件路径", "备份文件路径" };
                    MessageManager.ShowMsgBox("INP-242171", textArray1);
                }
                else if (str == str3)
                {
                    string[] textArray2 = new string[] { "导入文件路径", "回写文件路径" };
                    MessageManager.ShowMsgBox("INP-242171", textArray2);
                }
                else
                {
                    if (((int)this.fplx == 0) || ((int)this.fplx == 2))
                    {
                        PropertyUtil.SetValue("FPDJ_DRPATH", str);
                        PropertyUtil.SetValue("FPDJ_BFPATH", str2);
                        PropertyUtil.SetValue("FPDJ_HXPATH", str3);
                        PropertyUtil.SetValue("FPDJ_INTERVAL", this.txtInterval.Text.Trim());
                    }
                    else if ((int)this.fplx == 0x33)
                    {
                        PropertyUtil.SetValue("DZFPDJ_DRPATH", str);
                        PropertyUtil.SetValue("DZFPDJ_BFPATH", str2);
                        PropertyUtil.SetValue("DZFPDJ_HXPATH", str3);
                        PropertyUtil.SetValue("DZFPDJ_INTERVAL", this.txtInterval.Text.Trim());
                    }
                    else if ((int)this.fplx == 11)
                    {
                        PropertyUtil.SetValue("HYFPDJ_DRPATH", str);
                        PropertyUtil.SetValue("HYFPDJ_BFPATH", str2);
                        PropertyUtil.SetValue("HYFPDJ_HXPATH", str3);
                        PropertyUtil.SetValue("HYFPDJ_INTERVAL", this.txtInterval.Text.Trim());
                    }
                    else if ((int)this.fplx == 12)
                    {
                        PropertyUtil.SetValue("JDCFPDJ_DRPATH", str);
                        PropertyUtil.SetValue("JDCFPDJ_BFPATH", str2);
                        PropertyUtil.SetValue("JDCFPDJ_HXPATH", str3);
                        PropertyUtil.SetValue("JDCFPDJ_INTERVAL", this.txtInterval.Text.Trim());
                    }
                    base.DialogResult = DialogResult.OK;
                }
            }
        }

        private void btnOpenBF_Click(object sender, EventArgs e)
        {
            this.folderDialog = new FolderBrowserDialog();
            if (this.lastFolder != null)
            {
                this.folderDialog.SelectedPath = this.lastFolder;
            }
            if (this.folderDialog.ShowDialog() == DialogResult.OK)
            {
                this.lastFolder = this.folderDialog.SelectedPath;
                this.txtBFPath.Text = this.folderDialog.SelectedPath;
            }
        }

        private void btnOpenDR_Click(object sender, EventArgs e)
        {
            this.folderDialog = new FolderBrowserDialog();
            if (this.lastFolder != null)
            {
                this.folderDialog.SelectedPath = this.lastFolder;
            }
            if (this.folderDialog.ShowDialog() == DialogResult.OK)
            {
                this.lastFolder = this.folderDialog.SelectedPath;
                this.txtDRPath.Text = this.folderDialog.SelectedPath;
            }
        }

        private void btnOpenHX_Click(object sender, EventArgs e)
        {
            this.folderDialog = new FolderBrowserDialog();
            if (this.lastFolder != null)
            {
                this.folderDialog.SelectedPath = this.lastFolder;
            }
            if (this.folderDialog.ShowDialog() == DialogResult.OK)
            {
                this.lastFolder = this.folderDialog.SelectedPath;
                this.txtHXPath.Text = this.folderDialog.SelectedPath;
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
            this.txtHXPath = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtHXPath");
            this.txtBFPath = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtBFPath");
            this.txtDRPath = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtDRPath");
            this.txtInterval = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtInterval");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnOpenDR = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOpenDR");
            this.btnOpenBF = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOpenBF");
            this.btnOpenHX = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOpenHX");
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOpenBF.Click += new EventHandler(this.btnOpenBF_Click);
            this.btnOpenDR.Click += new EventHandler(this.btnOpenDR_Click);
            this.btnOpenHX.Click += new EventHandler(this.btnOpenHX_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ImportSet));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x194, 0x18a);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPDR.ImportSet\Aisino.Fwkp.Fpkj.Form.FPDR.ImportSet.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x194, 0x18a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ImportSet";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "导入设置";
            base.ResumeLayout(false);
        }
    }
}

