namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class ImportCode : BaseForm
    {
        private BMType bmType;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components;
        private FileControl fileControl1;
        private string filepath = "";
        private AisinoGRP groupBox1;
        private GetReadWriteXml rwxml = new GetReadWriteXml();
        private XmlComponentLoader xmlComponentLoader1;

        public ImportCode()
        {
            this.Initialize();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string path = this.fileControl1.TextBoxFile.Text.Trim();
            if (path.Length == 0)
            {
                MessageBoxHelper.Show("请选择导入文件路径!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!File.Exists(path))
            {
                MessageBoxHelper.Show("找不到文件:" + path, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.filepath = path;
                if (this.bmType == BMType.BM_KH)
                {
                    this.rwxml.ImportCustomerPath = this.filepath;
                }
                if (this.bmType == BMType.BM_SP)
                {
                    this.rwxml.ImportGoodsPath = this.filepath;
                }
                if (this.bmType == BMType.BM_SFHR)
                {
                    this.rwxml.ImportRecSenPath = this.filepath;
                }
                if (this.bmType == BMType.BM_FYXM)
                {
                    this.rwxml.ImportExpensePath = this.filepath;
                }
                if (this.bmType == BMType.BM_GHDW)
                {
                    this.rwxml.ImportPurchasePath = this.filepath;
                }
                if (this.bmType == BMType.BM_CL)
                {
                    this.rwxml.ImportCarPath = this.filepath;
                }
                if (this.bmType == BMType.BM_XHDW)
                {
                    this.rwxml.ImportXHDWPath = this.filepath;
                }
                base.DialogResult = DialogResult.OK;
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

        private void ImportCode_Load(object sender, EventArgs e)
        {
            if (this.bmType == BMType.BM_SP)
            {
                this.fileControl1.FileFilter = "文本文件(*.txt)|*.txt|稀土商品(*.dat)|*.dat|XML文件(*.xml)|*.xml";
            }
            else if ((this.bmType == BMType.BM_KH) || (this.bmType == BMType.BM_CL))
            {
                this.fileControl1.FileFilter = "文本文件(*.txt)|*.txt|XML文件(*.xml)|*.xml";
            }
            else if (this.bmType == BMType.BM_SPFL)
            {
                this.fileControl1.FileFilter = "XML文件(*.xml)|*.xml";
            }
            else
            {
                this.fileControl1.FileFilter = "文本文件(*.txt)|*.txt";
            }
            string fileFullName = this.rwxml.GetFileFullName(this.bmType);
            this.fileControl1.TextBoxFile.Text = fileFullName;
            this.fileControl1.FileFullname = fileFullName.Substring(fileFullName.LastIndexOf('\\') + 1);
            this.fileControl1.ButtonOk.Text = "浏览";
            this.fileControl1.OsFlag = "open";
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.fileControl1 = this.xmlComponentLoader1.GetControlByName<FileControl>("fileControl1");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.Load += new EventHandler(this.ImportCode_Load);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1c6, 0x9e);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.ImportCode\Aisino.Fwkp.Bmgl.Forms.ImportCode.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1c6, 0x9e);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ImportCode";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "编码导入";
            base.ResumeLayout(false);
        }

        public BMType BmType
        {
            set
            {
                this.bmType = value;
            }
        }

        public string FilePath
        {
            get
            {
                return this.filepath;
            }
        }
    }
}

