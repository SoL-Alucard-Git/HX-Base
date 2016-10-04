namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.MainForm.Properties;
    using Aisino.Framework.Plugin.Core.Util;
    using ns5;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SystemSettingNavForm : BaseNavForm
    {
        private Button btnCLBM;
        private Button btnCSH;
        private Button btnCSSZ;
        private Button btnFYXM;
        private Button btnGHDW;
        private Button btnKHBM;
        private Button btnSFHR;
        private Button btnSPBM;
        private IContainer icontainer_1;
        private List<string> list_0;
        private List<string> list_1;

        public SystemSettingNavForm()
        {
            
            this.InitializeComponent_1();
            base.FormTagFlag = "NavForm";
            this.list_0 = new List<string>();
            this.list_1 = new List<string>();
            base.Load += new EventHandler(this.SystemSettingNavForm_Load);
            this.list_1.Add("Menu.Xtsz.SystemSet");
            this.list_1.Add("Menu.Xtsz.BaseParasSet");
            this.list_1.Add("_BMKHManager");
            this.list_1.Add("_BMSPManager");
            this.list_1.Add("_BMGHDWManager");
            this.list_1.Add("_BMFYXMManager");
            this.list_1.Add("_BMSFHRManager");
            this.list_1.Add("_BMCLManager");
        }

        private void btnCLBM_Click(object sender, EventArgs e)
        {
            this.method_1("_BMCLManager");
        }

        private void btnCSH_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Xtsz.SystemSet");
        }

        private void btnCSSZ_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Xtsz.BaseParasSet");
        }

        private void btnFYXM_Click(object sender, EventArgs e)
        {
            this.method_1("_BMFYXMManager");
        }

        private void btnGHDW_Click(object sender, EventArgs e)
        {
            this.method_1("_BMGHDWManager");
        }

        private void btnKHBM_Click(object sender, EventArgs e)
        {
            this.method_1("_BMKHManager");
        }

        private void btnSFHR_Click(object sender, EventArgs e)
        {
            this.method_1("_BMSFHRManager");
        }

        private void btnSPBM_Click(object sender, EventArgs e)
        {
            this.method_1("_BMSPManager");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_1 != null))
            {
                this.icontainer_1.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SystemSettingNavForm));
            this.btnCSSZ = new Button();
            this.btnCSH = new Button();
            this.btnCLBM = new Button();
            this.btnGHDW = new Button();
            this.btnSFHR = new Button();
            this.btnFYXM = new Button();
            this.btnSPBM = new Button();
            this.btnKHBM = new Button();
            base.NavPanle.SuspendLayout();
            base.SuspendLayout();
            base.NavPanle.BackgroundImage = Resources.smethod_41();
            base.NavPanle.Controls.Add(this.btnCSSZ);
            base.NavPanle.Controls.Add(this.btnCSH);
            base.NavPanle.Controls.Add(this.btnCLBM);
            base.NavPanle.Controls.Add(this.btnGHDW);
            base.NavPanle.Controls.Add(this.btnSFHR);
            base.NavPanle.Controls.Add(this.btnFYXM);
            base.NavPanle.Controls.Add(this.btnSPBM);
            base.NavPanle.Controls.Add(this.btnKHBM);
            this.btnCSSZ.Image = Resources.smethod_17();
            this.btnCSSZ.Location = new Point(0xe1, 0xdb);
            this.btnCSSZ.Name = "btnCSSZ";
            this.btnCSSZ.Size = new Size(0x41, 0x4b);
            this.btnCSSZ.TabIndex = 15;
            this.btnCSSZ.UseVisualStyleBackColor = true;
            this.btnCSSZ.Click += new EventHandler(this.btnCSSZ_Click);
            this.btnCSH.Image = Resources.smethod_14();
            this.btnCSH.Location = new Point(0x53, 0xdb);
            this.btnCSH.Name = "btnCSH";
            this.btnCSH.Size = new Size(0x41, 0x4b);
            this.btnCSH.TabIndex = 14;
            this.btnCSH.UseVisualStyleBackColor = true;
            this.btnCSH.Click += new EventHandler(this.btnCSH_Click);
            this.btnCLBM.Image = Resources.车辆编码;
            this.btnCLBM.Location = new Point(0x26b, 0x17d);
            this.btnCLBM.Name = "btnCLBM";
            this.btnCLBM.Size = new Size(0x41, 0x4b);
            this.btnCLBM.TabIndex = 0x11;
            this.btnCLBM.UseVisualStyleBackColor = true;
            this.btnCLBM.Click += new EventHandler(this.btnCLBM_Click);
            this.btnGHDW.Image = Resources.smethod_49();
            this.btnGHDW.Location = new Point(0x1b0, 0x17d);
            this.btnGHDW.Name = "btnGHDW";
            this.btnGHDW.Size = new Size(0x41, 0x4b);
            this.btnGHDW.TabIndex = 0x10;
            this.btnGHDW.UseVisualStyleBackColor = true;
            this.btnGHDW.Click += new EventHandler(this.btnGHDW_Click);
            this.btnSFHR.Image = Resources.smethod_36();
            this.btnSFHR.Location = new Point(0x1b0, 0x38);
            this.btnSFHR.Name = "btnSFHR";
            this.btnSFHR.Size = new Size(0x41, 0x4b);
            this.btnSFHR.TabIndex = 13;
            this.btnSFHR.UseVisualStyleBackColor = true;
            this.btnSFHR.Click += new EventHandler(this.btnSFHR_Click);
            this.btnFYXM.Image = Resources.smethod_50();
            this.btnFYXM.Location = new Point(0x26b, 0x38);
            this.btnFYXM.Name = "btnFYXM";
            this.btnFYXM.Size = new Size(0x41, 0x4b);
            this.btnFYXM.TabIndex = 11;
            this.btnFYXM.UseVisualStyleBackColor = true;
            this.btnFYXM.Click += new EventHandler(this.btnFYXM_Click);
            this.btnSPBM.Image = Resources.商品编码;
            this.btnSPBM.Location = new Point(0x26b, 0xdb);
            this.btnSPBM.Name = "btnSPBM";
            this.btnSPBM.Size = new Size(0x41, 0x4b);
            this.btnSPBM.TabIndex = 10;
            this.btnSPBM.UseVisualStyleBackColor = true;
            this.btnSPBM.Click += new EventHandler(this.btnSPBM_Click);
            this.btnKHBM.Image = Resources.smethod_32();
            this.btnKHBM.Location = new Point(0x1b0, 0xdb);
            this.btnKHBM.Name = "btnKHBM";
            this.btnKHBM.Size = new Size(0x41, 0x4b);
            this.btnKHBM.TabIndex = 12;
            this.btnKHBM.UseVisualStyleBackColor = true;
            this.btnKHBM.Click += new EventHandler(this.btnKHBM_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(800, 530);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "SystemSettingNavForm";
            this.Text = "系统设置";
            base.NavPanle.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            if (this.list_0.Contains("Menu.Xtsz.SystemSet"))
            {
                this.btnCSH.Enabled = true;
            }
            else
            {
                this.btnCSH.Enabled = false;
            }
            if (this.list_0.Contains("Menu.Xtsz.BaseParasSet"))
            {
                this.btnCSSZ.Enabled = true;
            }
            else
            {
                this.btnCSSZ.Enabled = false;
            }
            if (this.list_0.Contains("_BMKHManager"))
            {
                this.btnKHBM.Enabled = true;
            }
            else
            {
                this.btnKHBM.Enabled = false;
            }
            if (this.list_0.Contains("_BMSPManager"))
            {
                this.btnSPBM.Enabled = true;
            }
            else
            {
                this.btnSPBM.Enabled = false;
            }
            if (this.list_0.Contains("_BMGHDWManager"))
            {
                this.btnGHDW.Enabled = true;
            }
            else
            {
                this.btnGHDW.Enabled = false;
            }
            if (this.list_0.Contains("_BMFYXMManager"))
            {
                this.btnFYXM.Enabled = true;
            }
            else
            {
                this.btnFYXM.Enabled = false;
            }
            if (this.list_0.Contains("_BMSFHRManager"))
            {
                this.btnSFHR.Enabled = true;
            }
            else
            {
                this.btnSFHR.Enabled = false;
            }
            if (this.list_0.Contains("_BMCLManager"))
            {
                this.btnCLBM.Enabled = true;
            }
            else
            {
                this.btnCLBM.Enabled = false;
            }
        }

        private void method_1(string string_0)
        {
            ToolUtil.RunFuction(string_0);
        }

        private void SystemSettingNavForm_Load(object sender, EventArgs e)
        {
            Class82.smethod_0(base.MdiParent.MainMenuStrip, this.list_1, this.list_0);
            this.method_0();
        }
    }
}

