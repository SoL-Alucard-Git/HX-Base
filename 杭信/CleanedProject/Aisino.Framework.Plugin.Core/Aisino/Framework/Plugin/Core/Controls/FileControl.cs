namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [Description("上传文件控件")]
    public class FileControl : UserControl
    {
        private bool bool_0;
        private AisinoBTN but_ok;
        private IContainer icontainer_0;
        private string string_0;
        private string string_1;
        [CompilerGenerated]
        private string string_2;
        private AisinoTXT txt_file;

        public event OnClickEnd onClickEnd;

        public FileControl()
        {
            
            this.bool_0 = true;
            this.string_0 = "";
            this.string_1 = "open";
            this.InitializeComponent();
            this.but_ok.Click += new EventHandler(this.ButtonOkClick);
        }

        public virtual void ButtonOkClick(object sender, EventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            if ("save" == this.string_1)
            {
                dialog = new SaveFileDialog();
            }
            dialog.Title = "选择文件";
            dialog.Filter = this.string_0;
            dialog.CheckPathExists = true;
            if (!string.IsNullOrEmpty(this.TextBoxFile.Text.Trim()))
            {
                dialog.CheckFileExists = this.bool_0;
                dialog.FileName = this.TextBoxFile.Text.Trim().Substring(this.TextBoxFile.Text.Trim().LastIndexOf('\\') + 1);
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txt_file.Text = dialog.FileName;
                if (this.onClickEnd != null)
                {
                    this.onClickEnd(sender, e);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.but_ok = new AisinoBTN();
            this.txt_file = new AisinoTXT();
            base.SuspendLayout();
            this.but_ok.Dock = DockStyle.Right;
            this.but_ok.Location = new Point(0xf6, 0);
            this.but_ok.Name = "but_ok";
            this.but_ok.Size = new Size(0x29, 0x15);
            this.but_ok.TabIndex = 6;
            this.but_ok.Text = "...";
            this.but_ok.UseVisualStyleBackColor = true;
            this.txt_file.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txt_file.Location = new Point(0, 0);
            this.txt_file.Name = "txt_file";
            this.txt_file.Size = new Size(240, 0x15);
            this.txt_file.TabIndex = 5;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.but_ok);
            base.Controls.Add(this.txt_file);
            base.Name = "FileControl";
            base.Size = new Size(0x11f, 0x15);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public AisinoBTN ButtonOk
        {
            get
            {
                return this.but_ok;
            }
        }

        public bool CheckFileExists
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public string FileFilter
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public string FileFullname
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            set
            {
                this.string_2 = value;
            }
        }

        public string OsFlag
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public AisinoTXT TextBoxFile
        {
            get
            {
                return this.txt_file;
            }
        }

        public delegate void OnClickEnd(object sender, EventArgs e);
    }
}

