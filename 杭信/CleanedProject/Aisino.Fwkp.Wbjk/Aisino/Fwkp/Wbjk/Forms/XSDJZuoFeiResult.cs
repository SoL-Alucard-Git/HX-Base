namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk.Model;
    using Aisino.Fwkp.Wbjk.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class XSDJZuoFeiResult : BaseForm
    {
        private AisinoBTN btn_OK;
        private IContainer components = null;
        private AisinoGRP groupBox1;
        private ImageList imageList1 = new ImageList();
        private DJZFImportResult ImportResult;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL labelSuccess;
        private AisinoLBL labelTotal;
        private AisinoLBL labelUndo;
        private ListView listView1;
        private XmlComponentLoader xmlComponentLoader1;

        public XSDJZuoFeiResult(DJZFImportResult result)
        {
            this.Initialize();
            this.ImportResult = result;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            base.Close();
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
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.btn_OK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_OK");
            this.labelSuccess = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelSuccess");
            this.labelUndo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelUndo");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.listView1 = this.xmlComponentLoader1.GetControlByName<ListView>("listView1");
            this.label5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label5");
            this.labelTotal = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelTotal");
            this.listView1.GridLines = true;
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1c7, 0x1e5);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.XSDJZuoFeiResult\Aisino.Fwkp.Wbjk.XSDJZuoFeiResult.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1c7, 0x1e5);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "XSDJZuoFeiResult";
            this.Text = "作废单据传入结果";
            base.Load += new EventHandler(this.XSDJZuoFeiResult_Load);
            base.ResumeLayout(false);
        }

        private void SetImgList()
        {
            this.imageList1.Images.Add("OK", Resources.OK);
            this.imageList1.Images.Add("BCZ", Resources.NoError);
            this.imageList1.Images.Add("YZF", Resources.NoAccess1);
        }

        private void XSDJZuoFeiResult_Load(object sender, EventArgs e)
        {
            try
            {
                this.SetImgList();
                this.labelSuccess.Text = this.ImportResult.Success.ToString();
                this.labelUndo.Text = this.ImportResult.Undo.ToString();
                this.labelTotal.Text = ((this.ImportResult.Undo + this.ImportResult.Success)).ToString() + "个";
                this.listView1.Columns.Add("结果", 50);
                this.listView1.Columns.Add("单据号", 100);
                this.listView1.Columns.Add("结果提示", 300);
                this.listView1.SmallImageList = this.imageList1;
                ListViewItem item = null;
                foreach (KeyValuePair<string, string> pair in this.ImportResult.Result)
                {
                    item = new ListViewItem(" ", pair.Value);
                    item.SubItems.Add(pair.Key);
                    item.SubItems.Add(pair.Value);
                    this.listView1.Items.Add(item);
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-274104", new string[] { exception.Message });
            }
        }
    }
}

