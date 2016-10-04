namespace Aisino.Fwkp.Fpkj.DKFPPLXZ
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormSaveFile : Form
    {
        private AisinoBTN btnBrowse;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components;
        private Label label1;
        private Label labTips;
        private string lastPath = "";
        private string outPath = "";
        private AisinoTXT txtFilePath;

        public FormSaveFile()
        {
            this.InitializeComponent();
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
                PropertyUtil.SetValue("Aisino.Fwkp.DKFPPLXZ.DownloadSavePath", this.txtFilePath.Text.Trim());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtFilePath.Text.Trim() == "")
            {
                MessageBox.Show("请您设置文件保存路径！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txtFilePath.Select();
            }
            else
            {
                this.outPath = this.txtFilePath.Text.Trim();
                PropertyUtil.SetValue("Aisino.Fwkp.DKFPPLXZ.DownloadSavePath", this.txtFilePath.Text.Trim());
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

        private void FormSaveFile_Load(object sender, EventArgs e)
        {
            this.lastPath = PropertyUtil.GetValue("Aisino.Fwkp.DKFPPLXZ.DownloadSavePath");
            this.txtFilePath.Text = this.lastPath;
        }

        private void InitializeComponent()
        {
            this.labTips = new Label();
            this.label1 = new Label();
            this.txtFilePath = new AisinoTXT();
            this.btnBrowse = new AisinoBTN();
            this.btnCancel = new AisinoBTN();
            this.btnOK = new AisinoBTN();
            base.SuspendLayout();
            this.labTips.AutoSize = true;
            this.labTips.Font = new Font("宋体", 9f);
            this.labTips.Location = new Point(0x18, 0x16);
            this.labTips.Name = "labTips";
            this.labTips.Size = new Size(0x7d, 12);
            this.labTips.TabIndex = 0;
            this.labTips.Text = "请选择文件保存路径：";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 9f);
            this.label1.Location = new Point(0x18, 0x42);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件路径：";
            this.txtFilePath.AcceptsReturn = true;
            this.txtFilePath.Location = new Point(0x5f, 0x3e);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new Size(0xe7, 0x15);
            this.txtFilePath.TabIndex = 7;
            this.btnBrowse.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnBrowse.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnBrowse.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnBrowse.Font = new Font("宋体", 9f);
            this.btnBrowse.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnBrowse.ForeColor = Color.White;
            this.btnBrowse.Location = new Point(0x155, 60);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new Size(0x2e, 0x19);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "浏览";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
            this.btnCancel.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnCancel.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnCancel.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Font = new Font("宋体", 9f);
            this.btnCancel.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(0x119, 0x77);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x19);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnOK.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnOK.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Font = new Font("宋体", 9f);
            this.btnOK.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnOK.ForeColor = Color.White;
            this.btnOK.Location = new Point(0xbd, 0x77);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x19);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a0, 0xae);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnBrowse);
            base.Controls.Add(this.txtFilePath);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.labTips);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormSaveFile";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "选择文件保存路径";
            base.Load += new EventHandler(this.FormSaveFile_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public string DownloadSavePath
        {
            get
            {
                return this.outPath;
            }
        }
    }
}

