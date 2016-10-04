namespace Aisino.Fwkp.Hzfp.Form
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class FPProgressBar : BaseForm
    {
        private IContainer components;
        public ProgressBar fpxf_progressBar;
        private Label label_Tip;
        private Label label_Title;
        private ILog loger = LogUtil.GetLogger<FPProgressBar>();
        private XmlComponentLoader xmlComponentLoader1;

        public FPProgressBar()
        {
            try
            {
                this.Initialize();
                this.InitializeFPProgressBar();
                this.InitProgressBar();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
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
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FPProgressBar));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            this.label_Tip = new Label();
            this.label_Title = new Label();
            this.fpxf_progressBar = new ProgressBar();
            this.xmlComponentLoader1.SuspendLayout();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Controls.Add(this.label_Tip);
            this.xmlComponentLoader1.Controls.Add(this.label_Title);
            this.xmlComponentLoader1.Controls.Add(this.fpxf_progressBar);
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1ee, 0x98);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=("");
            this.label_Tip.AutoSize = true;
            this.label_Tip.Font = new Font("微软雅黑", 10f);
            this.label_Tip.Location = new Point(0x16d, 0x60);
            this.label_Tip.Name = "label_Tip";
            this.label_Tip.Size = new Size(0x6b, 20);
            this.label_Tip.TabIndex = 1;
            this.label_Tip.Text = "请等待任务完成";
            this.label_Title.AutoSize = true;
            this.label_Title.Font = new Font("微软雅黑", 10f);
            this.label_Title.Location = new Point(0x18, 0x25);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new Size(0x95, 20);
            this.label_Title.TabIndex = 1;
            this.label_Title.Text = "正在下载信息表";
            this.fpxf_progressBar.Location = new Point(0x15, 0x3d);
            this.fpxf_progressBar.Name = "fpxf_progressBar";
            this.fpxf_progressBar.Size = new Size(0x1c9, 0x1d);
            this.fpxf_progressBar.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1ee, 0x98);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FPProgressBar";
            this.Text = "信息表下载中";
            this.xmlComponentLoader1.ResumeLayout(false);
            this.xmlComponentLoader1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void InitializeFPProgressBar()
        {
            int x = 300;
            int y = 100;
            Rectangle virtualScreen = SystemInformation.VirtualScreen;
            x = (virtualScreen.Width - base.Width) / 2;
            if (x <= 0)
            {
                x = 300;
            }
            y = (virtualScreen.Height - base.Height) / 2;
            if (y <= 0)
            {
                y = 100;
            }
            base.Location = new Point(x, y);
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.ControlBox = false;
        }

        private void InitProgressBar()
        {
            this.fpxf_progressBar.Visible = true;
            this.fpxf_progressBar.Minimum = 1;
            this.fpxf_progressBar.Maximum = 0x2710;
            this.fpxf_progressBar.Value = 1;
            this.fpxf_progressBar.Step = 1;
        }

        public void SetTip(string title, string tip, string Topic = "信息表上传下载过程")
        {
            this.Text = Topic;
            this.label_Tip.Text = tip;
            this.label_Title.Text = title;
            this.Refresh();
        }
    }
}

