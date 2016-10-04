namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class XSDJZuoFeiCR : BaseForm
    {
        private AisinoBTN btn_Cancel;
        private AisinoBTN btn_OK;
        private IContainer components = null;
        private InvalidateSaleBill djzfcr = new InvalidateSaleBill();
        private FileControl fileControl1;
        private XmlComponentLoader xmlComponentLoader1;

        public XSDJZuoFeiCR()
        {
            this.Initialize();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(this.fileControl1.get_TextBoxFile().Text.Trim()))
                {
                    MessageManager.ShowMsgBox("INP-274101");
                }
                else
                {
                    string str = "";
                    DJZFImportResult result = this.djzfcr.MakeInvalidExecute(this.fileControl1.get_TextBoxFile().Text, ref str);
                    if (result == null)
                    {
                        MessageManager.ShowMsgBox("INP-274103", new string[] { str });
                    }
                    else
                    {
                        XSDJZuoFeiResult result2 = new XSDJZuoFeiResult(result);
                        base.Close();
                        this.djzfcr.strBillImportPath = this.fileControl1.get_TextBoxFile().Text.Trim();
                        result2.ShowInTaskbar = false;
                        result2.ShowDialog();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-274102", new string[] { exception.Message });
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
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.fileControl1 = this.xmlComponentLoader1.GetControlByName<FileControl>("fileControl1");
            this.btn_OK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_OK");
            this.btn_Cancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_Cancel");
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.btn_Cancel.Click += new EventHandler(this.btn_Cancel_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(XSDJZuoFeiCR));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1be, 0xf1);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "作废单据传入";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.XSDJZuoFeiCR\Aisino.Fwkp.Wbjk.XSDJZuoFeiCR.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1be, 0xf1);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "XSDJZuoFeiCR";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "作废单据传入";
            base.Load += new EventHandler(this.XSDJZuoFeiCR_Load);
            base.ResumeLayout(false);
        }

        private void XSDJZuoFeiCR_Load(object sender, EventArgs e)
        {
            try
            {
                this.fileControl1.set_FileFilter("文本文件(*.txt)|*.txt");
                this.fileControl1.get_TextBoxFile().Text = this.djzfcr.strBillImportPath;
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-274102", new string[] { exception.Message });
            }
        }
    }
}

