namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fptk.Entry;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using BusinessObject;
    public class JDCVersionSet : DockForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoCHK chkSave;
        private IContainer components;
        private ILog log = LogUtil.GetLogger<JDCVersionSet>();
        private string mFpdm;
        private string mFphm;
        private JDCVersion mVersion;
        private AisinoRDO rdoNew;
        private AisinoRDO rdoOld;
        private XmlComponentLoader xmlComponentLoader1;

        public JDCVersionSet(string fpdm, string fphm, JDCVersion version)
        {
            this.Initialize();
            this.mFpdm = fpdm;
            this.mFphm = fphm;
            this.mVersion = version;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string str = this.rdoNew.Checked ? "新版" : "老版";
            if (this.mVersion == JDCVersion.NULL)
            {
                string[] textArray1 = new string[] { str };
                MessageManager.ShowMsgBox("INP-242167", textArray1);
                if (this.chkSave.Checked)
                {
                    this.SaveJDCVersion();
                }
                if (this.rdoOld.Checked)
                {
                    VehicleInvEntry.oldJdcfpfm = new JDCInvoiceForm_old((FPLX)12, this.mFpdm, this.mFphm);
                    VehicleInvEntry.oldJdcfpfm.ShowDialog();
                }
                else if (this.rdoNew.Checked)
                {
                    VehicleInvEntry.newJdcfpfm = new JDCInvoiceForm_new((FPLX)12, this.mFpdm, this.mFphm);
                    VehicleInvEntry.newJdcfpfm.ShowDialog();
                }
            }
            else
            {
                this.SaveJDCVersion();
                string[] textArray2 = new string[] { str };
                MessageManager.ShowMsgBox("INP-242157", textArray2);
            }
            base.DialogResult = DialogResult.OK;
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
            this.rdoNew = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rdoNew");
            this.rdoOld = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rdoOld");
            this.chkSave = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkSave");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.Load += new EventHandler(this.JDCVersionSet_Load);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(JDCVersionSet));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x158, 0xd4);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.JDCFPSet\Aisino.Fwkp.Fpkj.Form.JDCFPSet.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x158, 0xd4);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "JDCVersionSet";
            base.TabText="机动车发票设置";
            this.Text = "机动车发票设置";
            base.ResumeLayout(false);
        }

        private void JDCVersionSet_Load(object sender, EventArgs e)
        {
            if (this.mVersion != JDCVersion.NULL)
            {
                this.chkSave.Visible = false;
            }
            if (this.mVersion == JDCVersion.New)
            {
                this.rdoNew.Checked = true;
            }
            else
            {
                this.rdoOld.Checked = true;
            }
        }

        private void SaveJDCVersion()
        {
            int num = 0;
            if (this.rdoNew.Checked)
            {
                num = 1;
            }
            else if (this.rdoOld.Checked)
            {
                num = 0;
            }
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Config\Common\JDCVerSelect.xml");
            try
            {
                XmlDocument document = new XmlDocument();
                if (!File.Exists(path))
                {
                    document.LoadXml(string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><JDCVersion>{0}</JDCVersion>", num));
                    document.Save(path);
                }
                else
                {
                    document.Load(path);
                    XmlNode node = document.SelectSingleNode("/JDCVersion");
                    if (node == null)
                    {
                        XmlElement newChild = document.CreateElement("JDCVersion");
                        newChild.InnerText = num.ToString();
                        document.DocumentElement.AppendChild(newChild);
                    }
                    else
                    {
                        node.InnerText = num.ToString();
                    }
                    document.Save(path);
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.Message, exception);
            }
        }
    }
}

