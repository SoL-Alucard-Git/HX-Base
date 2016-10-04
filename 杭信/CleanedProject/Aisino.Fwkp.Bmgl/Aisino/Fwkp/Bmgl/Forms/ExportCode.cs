namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class ExportCode : BaseForm
    {
        private BMType bmType;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoCMB cbbSeparator;
        private IContainer components;
        private FileControl fileControl1;
        private string filepath = "";
        private AisinoGRP groupBox1;
        private AisinoLBL label1;
        private GetReadWriteXml rwxml = new GetReadWriteXml();
        private string separator = "";
        private XmlComponentLoader xmlComponentLoader1;

        public ExportCode()
        {
            this.Initialize();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.fileControl1.TextBoxFile.Text.Length > 0)
            {
                this.filepath = this.fileControl1.TextBoxFile.Text.Trim();
                if (this.IsValidFileNameOrPath() && (!File.Exists(this.filepath) || (DialogResult.Yes == MessageBoxHelper.Show("文件已存在，是否覆盖?", "询问", MessageBoxButtons.YesNo))))
                {
                    this.separator = this.cbbSeparator.SelectedValue.ToString();
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                    if (this.bmType == BMType.BM_KH)
                    {
                        this.rwxml.ExportCustomerPath = this.fileControl1.TextBoxFile.Text.Trim();
                    }
                    if (this.bmType == BMType.GoodsTax)
                    {
                        this.rwxml.ExportGoodsPath = this.fileControl1.TextBoxFile.Text.Trim();
                    }
                    if (this.bmType == BMType.BM_SFHR)
                    {
                        this.rwxml.ExportRecSenPath = this.fileControl1.TextBoxFile.Text.Trim();
                    }
                    if (this.bmType == BMType.BM_SP)
                    {
                        this.rwxml.ExportGoodsPath = this.fileControl1.TextBoxFile.Text.Trim();
                    }
                    if (this.bmType == BMType.BM_GHDW)
                    {
                        this.rwxml.ExportPurchasePath = this.fileControl1.TextBoxFile.Text.Trim();
                    }
                    if (this.bmType == BMType.InvoiceType)
                    {
                        this.rwxml.ExportInvoiceTypePath = this.fileControl1.TextBoxFile.Text.Trim();
                    }
                    if (this.bmType == BMType.BM_FYXM)
                    {
                        this.rwxml.ExportExpensePath = this.fileControl1.TextBoxFile.Text.Trim();
                    }
                    if (this.bmType == BMType.District)
                    {
                        this.rwxml.ExportDistrictPath = this.fileControl1.TextBoxFile.Text.Trim();
                    }
                    if (this.bmType == BMType.BM_CL)
                    {
                        this.rwxml.ExportCarPath = this.fileControl1.TextBoxFile.Text.Trim();
                    }
                }
            }
            else
            {
                MessageBoxHelper.Show("请选择导出文件路径!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void ExportCode_Load(object sender, EventArgs e)
        {
            this.fileControl1.CheckFileExists = false;
            string fileFullName = this.rwxml.GetFileFullName(this.bmType);
            this.fileControl1.TextBoxFile.Text = fileFullName;
            this.fileControl1.ButtonOk.Text = "浏览";
            DataTable table = new DataTable();
            table.Columns.Add("Display");
            table.Columns.Add("Value");
            table.Rows.Add(new object[] { "双波浪线", "~~" });
            table.Rows.Add(new object[] { "逗号", "," });
            table.Rows.Add(new object[] { "空格", " " });
            this.cbbSeparator.DataSource = table;
            this.cbbSeparator.DisplayMember = "Display";
            this.cbbSeparator.ValueMember = "Value";
            this.fileControl1.FileFilter = "文本文件(*.txt)|*.txt";
            this.fileControl1.OsFlag = "save";
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.fileControl1 = this.xmlComponentLoader1.GetControlByName<FileControl>("fileControl1");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.cbbSeparator = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cbbSeparator");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
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
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.ExportCode\Aisino.Fwkp.Bmgl.Forms.ExportCode.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1c6, 0x9e);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ExportCode";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "编码导出";
            base.Load += new EventHandler(this.ExportCode_Load);
            base.ResumeLayout(false);
        }

        private bool IsValidFileNameOrPath()
        {
            if (string.IsNullOrEmpty(this.filepath))
            {
                return false;
            }
            string directoryName = Path.GetDirectoryName(this.filepath);
            if (directoryName == string.Empty)
            {
                if (DialogResult.Yes != MessageBoxHelper.Show("无路径默认保存于C盘,是否确定?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    return false;
                }
                directoryName = @"C:\";
                this.filepath = directoryName + this.filepath;
                this.fileControl1.TextBoxFile.Text = this.filepath;
            }
            if (!Directory.Exists(directoryName))
            {
                MessageBoxHelper.Show("所选路径不存在，请输入有效路径!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return false;
            }
            string fileName = Path.GetFileName(this.filepath);
            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                if (fileName.IndexOf(ch) >= 0)
                {
                    MessageBoxHelper.Show("文件名含有非法字符，请输入有效文件名!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return false;
                }
            }
            return true;
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

        public string Separator
        {
            get
            {
                return this.separator;
            }
        }
    }
}

