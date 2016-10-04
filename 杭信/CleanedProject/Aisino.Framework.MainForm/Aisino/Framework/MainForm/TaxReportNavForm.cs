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

    public class TaxReportNavForm : BaseNavForm
    {
        private Button btnCSCL;
        private Button btnFPZL;
        private Button btnYCBS;
        private Button btnYCQK;
        private Button btnYDTJ;
        private Button btnZLCC;
        private Button btnZTCX;
        private IContainer icontainer_1;
        private List<string> list_0;
        private List<string> list_1;

        public TaxReportNavForm()
        {
            
            this.InitializeComponent_1();
            base.FormTagFlag = "NavForm";
            this.list_0 = new List<string>();
            this.list_1 = new List<string>();
            base.Load += new EventHandler(this.TaxReportNavForm_Load);
            this.list_1.Add("Menu.Bsgl.Cbsgl.Cscl");
            this.list_1.Add("Menu.Bsgl.YccbCommand");
            this.list_1.Add("Menu.Bsgl.JgcxCommand");
            this.list_1.Add("Menu.Bsgl.Cbsgl.Fpcxdy");
            this.list_1.Add("Menu.Bsgl.Jskgl.Ztcx");
            this.list_1.Add("Menu.Bsgl.Cbsgl.Bszlcc");
            this.list_1.Add("Menu.Bsgl.InvDataMonthlyStatistic");
            this.list_1.Add("Menu.Bsgl.InvDataStatistic");
            this.list_1.Add("Menu.Bsgl.InvoiceReport");
        }

        private void btnCSCL_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Bsgl.Cbsgl.Cscl");
        }

        private void btnFPZL_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Bsgl.Cbsgl.Fpcxdy");
        }

        private void btnYCBS_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Bsgl.YccbCommand");
        }

        private void btnYCQK_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Bsgl.JgcxCommand");
        }

        private void btnYDTJ_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Bsgl.InvDataMonthlyStatistic");
        }

        private void btnZLCC_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Bsgl.Cbsgl.Bszlcc");
        }

        private void btnZTCX_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Bsgl.Jskgl.Ztcx");
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(TaxReportNavForm));
            this.btnFPZL = new Button();
            this.btnYCBS = new Button();
            this.btnYDTJ = new Button();
            this.btnZTCX = new Button();
            this.btnZLCC = new Button();
            this.btnYCQK = new Button();
            this.btnCSCL = new Button();
            base.NavPanle.SuspendLayout();
            base.SuspendLayout();
            base.NavPanle.BackgroundImage = Resources.smethod_43();
            base.NavPanle.Controls.Add(this.btnFPZL);
            base.NavPanle.Controls.Add(this.btnYCBS);
            base.NavPanle.Controls.Add(this.btnYDTJ);
            base.NavPanle.Controls.Add(this.btnZTCX);
            base.NavPanle.Controls.Add(this.btnZLCC);
            base.NavPanle.Controls.Add(this.btnYCQK);
            base.NavPanle.Controls.Add(this.btnCSCL);
            this.btnFPZL.Image = Resources.smethod_27();
            this.btnFPZL.Location = new Point(0x260, 0x12d);
            this.btnFPZL.Name = "btnFPZL";
            this.btnFPZL.Size = new Size(0x41, 0x4b);
            this.btnFPZL.TabIndex = 14;
            this.btnFPZL.UseVisualStyleBackColor = true;
            this.btnFPZL.Click += new EventHandler(this.btnFPZL_Click);
            this.btnYCBS.Image = Resources.smethod_52();
            this.btnYCBS.Location = new Point(0xa3, 0x95);
            this.btnYCBS.Name = "btnYCBS";
            this.btnYCBS.Size = new Size(0x41, 0x4b);
            this.btnYCBS.TabIndex = 15;
            this.btnYCBS.UseVisualStyleBackColor = true;
            this.btnYCBS.Click += new EventHandler(this.btnYCBS_Click);
            this.btnYDTJ.Image = Resources.smethod_39();
            this.btnYDTJ.Location = new Point(0x260, 0x95);
            this.btnYDTJ.Name = "btnYDTJ";
            this.btnYDTJ.Size = new Size(0x41, 0x4b);
            this.btnYDTJ.TabIndex = 20;
            this.btnYDTJ.UseVisualStyleBackColor = true;
            this.btnYDTJ.Click += new EventHandler(this.btnYDTJ_Click);
            this.btnZTCX.Image = Resources.smethod_46();
            this.btnZTCX.Location = new Point(0x1d8, 0x95);
            this.btnZTCX.Name = "btnZTCX";
            this.btnZTCX.Size = new Size(0x41, 0x4b);
            this.btnZTCX.TabIndex = 11;
            this.btnZTCX.UseVisualStyleBackColor = true;
            this.btnZTCX.Click += new EventHandler(this.btnZTCX_Click);
            this.btnZLCC.Image = Resources.smethod_51();
            this.btnZLCC.Location = new Point(0x131, 0x130);
            this.btnZLCC.Name = "btnZLCC";
            this.btnZLCC.Size = new Size(0x41, 0x4b);
            this.btnZLCC.TabIndex = 0x13;
            this.btnZLCC.UseVisualStyleBackColor = true;
            this.btnZLCC.Click += new EventHandler(this.btnZLCC_Click);
            this.btnYCQK.Image = Resources.smethod_53();
            this.btnYCQK.Location = new Point(0x130, 0x95);
            this.btnYCQK.Name = "btnYCQK";
            this.btnYCQK.Size = new Size(0x41, 0x4b);
            this.btnYCQK.TabIndex = 12;
            this.btnYCQK.UseVisualStyleBackColor = true;
            this.btnYCQK.Click += new EventHandler(this.btnYCQK_Click);
            this.btnCSCL.Image = Resources.smethod_31();
            this.btnCSCL.Location = new Point(0xa2, 0x130);
            this.btnCSCL.Name = "btnCSCL";
            this.btnCSCL.Size = new Size(0x41, 0x4b);
            this.btnCSCL.TabIndex = 13;
            this.btnCSCL.UseVisualStyleBackColor = true;
            this.btnCSCL.Click += new EventHandler(this.btnCSCL_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(800, 530);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "TaxReportNavForm";
            this.Text = "报税处理";
            base.Load += new EventHandler(this.TaxReportNavForm_Load);
            base.NavPanle.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.btnCSCL.Enabled = this.list_0.Contains("Menu.Bsgl.Cbsgl.Cscl");
            this.btnYCBS.Enabled = this.list_0.Contains("Menu.Bsgl.YccbCommand");
            this.btnYCQK.Enabled = this.list_0.Contains("Menu.Bsgl.JgcxCommand");
            this.btnFPZL.Enabled = this.list_0.Contains("Menu.Bsgl.Cbsgl.Fpcxdy");
            this.btnZTCX.Enabled = this.list_0.Contains("Menu.Bsgl.Jskgl.Ztcx");
            this.btnZLCC.Enabled = this.list_0.Contains("Menu.Bsgl.Cbsgl.Bszlcc");
            this.btnYDTJ.Enabled = this.list_0.Contains("Menu.Bsgl.InvDataMonthlyStatistic");
        }

        private void method_1(string string_0)
        {
            ToolUtil.RunFuction(string_0);
        }

        private void method_2(object sender, EventArgs e)
        {
            this.method_1("Menu.Bsgl.InvDataStatistic");
        }

        private void method_3(object sender, EventArgs e)
        {
            this.method_1("Menu.Bsgl.InvoiceReport");
        }

        private void TaxReportNavForm_Load(object sender, EventArgs e)
        {
            Class82.smethod_0(base.MdiParent.MainMenuStrip, this.list_1, this.list_0);
            this.method_0();
        }
    }
}

