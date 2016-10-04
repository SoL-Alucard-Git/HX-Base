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

    public class TextIFaceNavForm : BaseNavForm
    {
        private Button btnDJCR;
        private Button btnDJWH;
        private Button btnFPCC;
        private Button btnFPCX;
        private Button btnFPSC;
        private Button btnSPCX;
        private Button btnZFCR;
        private Button btnZFFP;
        private ContextMenuStrip cmsDJCR;
        private ContextMenuStrip cmsDJWH;
        private ContextMenuStrip cmsFPSC;
        private IContainer icontainer_1;
        private List<string> list_0;
        private List<string> list_1;
        private ToolStripMenuItem tmiDJCF;
        private ToolStripMenuItem tmiDJHB;
        private ToolStripMenuItem tmiDJHY;
        private ToolStripMenuItem tmiDJXG;
        private ToolStripMenuItem tmiHYFP;
        private ToolStripMenuItem tmiJDCFP_NEW;
        private ToolStripMenuItem tmiPTFP;
        private ToolStripMenuItem tmiWBCR;
        private ToolStripMenuItem tmiZYFP;
        private ToolStripMenuItem toolStripMenuItem_0;
        private ToolStripMenuItem toolStripMenuItem_1;
        private ToolStripMenuItem toolStripMenuItem_2;
        private ToolStripMenuItem toolStripMenuItem_3;

        public TextIFaceNavForm()
        {
            
            this.InitializeComponent_1();
            base.FormTagFlag = "NavForm";
            this.list_0 = new List<string>();
            this.list_1 = new List<string>();
            base.Load += new EventHandler(this.TextIFaceNavForm_Load);
            this.list_1.Add("_XSDJTxtCR");
            this.list_1.Add("_XSDJExcelCR");
            this.list_1.Add("_XSDJXG");
            this.list_1.Add("_XSDJCF");
            this.list_1.Add("_XSDJHB");
            this.list_1.Add("_XSDJHY");
            this.list_1.Add("_XSDJZKHZ");
            this.list_1.Add("_XSDJZFCR");
            this.list_1.Add("_FaPiaoZF");
            this.list_1.Add("_GenerateSpecial");
            this.list_1.Add("_GenerateCommon");
            this.list_1.Add("_GenerateCommonNCPXS");
            this.list_1.Add("_GenerateCommonNCPSG");
            this.list_1.Add("_GenerateTransportation");
            this.list_1.Add("_GenerateVehiclesalesNew");
            this.list_1.Add("_FaPiaoCX");
            this.list_1.Add("_ShangPinCX");
            this.list_1.Add("_FaPiaoCC");
        }

        private void btnDJCR_Click(object sender, EventArgs e)
        {
            Button control = (Button) sender;
            this.cmsDJCR.Show(control, new Point(control.Width, 0), ToolStripDropDownDirection.BelowRight);
        }

        private void btnDJWH_Click(object sender, EventArgs e)
        {
            Button control = (Button) sender;
            this.cmsDJWH.Show(control, new Point(control.Width, 0), ToolStripDropDownDirection.BelowRight);
        }

        private void btnFPCC_Click(object sender, EventArgs e)
        {
            this.method_1("_FaPiaoCC");
        }

        private void btnFPCX_Click(object sender, EventArgs e)
        {
            this.method_1("_FaPiaoCX");
        }

        private void btnFPSC_Click(object sender, EventArgs e)
        {
            Button control = (Button) sender;
            this.cmsFPSC.Show(control, new Point(control.Width, 0), ToolStripDropDownDirection.BelowRight);
        }

        private void btnSPCX_Click(object sender, EventArgs e)
        {
            this.method_1("_ShangPinCX");
        }

        private void btnZFCR_Click(object sender, EventArgs e)
        {
            this.method_1("_XSDJZFCR");
        }

        private void btnZFFP_Click(object sender, EventArgs e)
        {
            this.method_1("_FaPiaoZF");
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
            this.icontainer_1 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(TextIFaceNavForm));
            this.btnFPCC = new Button();
            this.btnSPCX = new Button();
            this.btnDJWH = new Button();
            this.btnFPCX = new Button();
            this.btnFPSC = new Button();
            this.btnZFFP = new Button();
            this.btnZFCR = new Button();
            this.btnDJCR = new Button();
            this.cmsDJCR = new ContextMenuStrip(this.icontainer_1);
            this.tmiWBCR = new ToolStripMenuItem();
            this.toolStripMenuItem_0 = new ToolStripMenuItem();
            this.cmsDJWH = new ContextMenuStrip(this.icontainer_1);
            this.tmiDJXG = new ToolStripMenuItem();
            this.tmiDJCF = new ToolStripMenuItem();
            this.tmiDJHB = new ToolStripMenuItem();
            this.tmiDJHY = new ToolStripMenuItem();
            this.toolStripMenuItem_1 = new ToolStripMenuItem();
            this.cmsFPSC = new ContextMenuStrip(this.icontainer_1);
            this.tmiZYFP = new ToolStripMenuItem();
            this.tmiPTFP = new ToolStripMenuItem();
            this.toolStripMenuItem_2 = new ToolStripMenuItem();
            this.toolStripMenuItem_3 = new ToolStripMenuItem();
            this.tmiHYFP = new ToolStripMenuItem();
            this.tmiJDCFP_NEW = new ToolStripMenuItem();
            base.NavPanle.SuspendLayout();
            this.cmsDJCR.SuspendLayout();
            this.cmsDJWH.SuspendLayout();
            this.cmsFPSC.SuspendLayout();
            base.SuspendLayout();
            base.NavPanle.BackgroundImage = Resources.smethod_45();
            base.NavPanle.Controls.Add(this.btnFPCC);
            base.NavPanle.Controls.Add(this.btnSPCX);
            base.NavPanle.Controls.Add(this.btnDJWH);
            base.NavPanle.Controls.Add(this.btnFPCX);
            base.NavPanle.Controls.Add(this.btnFPSC);
            base.NavPanle.Controls.Add(this.btnZFFP);
            base.NavPanle.Controls.Add(this.btnZFCR);
            base.NavPanle.Controls.Add(this.btnDJCR);
            this.btnFPCC.Image = Resources.smethod_18();
            this.btnFPCC.Location = new Point(0x25e, 0x146);
            this.btnFPCC.Name = "btnFPCC";
            this.btnFPCC.Size = new Size(0x41, 0x4b);
            this.btnFPCC.TabIndex = 0x17;
            this.btnFPCC.UseVisualStyleBackColor = true;
            this.btnFPCC.Click += new EventHandler(this.btnFPCC_Click);
            this.btnSPCX.Image = Resources.smethod_29();
            this.btnSPCX.Location = new Point(0x25e, 0xdb);
            this.btnSPCX.Name = "btnSPCX";
            this.btnSPCX.Size = new Size(0x41, 0x4b);
            this.btnSPCX.TabIndex = 0x16;
            this.btnSPCX.UseVisualStyleBackColor = true;
            this.btnSPCX.Click += new EventHandler(this.btnSPCX_Click);
            this.btnDJWH.Image = Resources.smethod_16();
            this.btnDJWH.Location = new Point(0xe5, 0x6d);
            this.btnDJWH.Name = "btnDJWH";
            this.btnDJWH.Size = new Size(0x41, 0x4b);
            this.btnDJWH.TabIndex = 0x15;
            this.btnDJWH.UseVisualStyleBackColor = true;
            this.btnDJWH.Click += new EventHandler(this.btnDJWH_Click);
            this.btnFPCX.Image = Resources.smethod_24();
            this.btnFPCX.Location = new Point(0x25e, 0x6d);
            this.btnFPCX.Name = "btnFPCX";
            this.btnFPCX.Size = new Size(0x41, 0x4b);
            this.btnFPCX.TabIndex = 0x18;
            this.btnFPCX.UseVisualStyleBackColor = true;
            this.btnFPCX.Click += new EventHandler(this.btnFPCX_Click);
            this.btnFPSC.Image = Resources.smethod_25();
            this.btnFPSC.Location = new Point(0x1a1, 0xdb);
            this.btnFPSC.Name = "btnFPSC";
            this.btnFPSC.Size = new Size(0x41, 0x4b);
            this.btnFPSC.TabIndex = 0x13;
            this.btnFPSC.UseVisualStyleBackColor = true;
            this.btnFPSC.Click += new EventHandler(this.btnFPSC_Click);
            this.btnZFFP.Image = Resources.smethod_12();
            this.btnZFFP.Location = new Point(0xe5, 0x146);
            this.btnZFFP.Name = "btnZFFP";
            this.btnZFFP.Size = new Size(0x41, 0x4b);
            this.btnZFFP.TabIndex = 0x19;
            this.btnZFFP.UseVisualStyleBackColor = true;
            this.btnZFFP.Click += new EventHandler(this.btnZFFP_Click);
            this.btnZFCR.Image = Resources.smethod_11();
            this.btnZFCR.Location = new Point(0x4d, 0x146);
            this.btnZFCR.Name = "btnZFCR";
            this.btnZFCR.Size = new Size(0x41, 0x4b);
            this.btnZFCR.TabIndex = 0x12;
            this.btnZFCR.UseVisualStyleBackColor = true;
            this.btnZFCR.Click += new EventHandler(this.btnZFCR_Click);
            this.btnDJCR.Image = Resources.smethod_15();
            this.btnDJCR.Location = new Point(0x4d, 0x6d);
            this.btnDJCR.Name = "btnDJCR";
            this.btnDJCR.Size = new Size(0x41, 0x4b);
            this.btnDJCR.TabIndex = 20;
            this.btnDJCR.UseVisualStyleBackColor = true;
            this.btnDJCR.Click += new EventHandler(this.btnDJCR_Click);
            this.cmsDJCR.Items.AddRange(new ToolStripItem[] { this.tmiWBCR, this.toolStripMenuItem_0 });
            this.cmsDJCR.Name = "cmsDJCR";
            this.cmsDJCR.Size = new Size(130, 0x30);
            this.tmiWBCR.Name = "tmiWBCR";
            this.tmiWBCR.Size = new Size(0x81, 0x16);
            this.tmiWBCR.Text = "文本传入";
            this.tmiWBCR.Click += new EventHandler(this.tmiWBCR_Click);
            this.toolStripMenuItem_0.Name = "tmiEXCELCR";
            this.toolStripMenuItem_0.Size = new Size(0x81, 0x16);
            this.toolStripMenuItem_0.Text = "Excel传入";
            this.toolStripMenuItem_0.Click += new EventHandler(this.toolStripMenuItem_0_Click);
            this.cmsDJWH.Items.AddRange(new ToolStripItem[] { this.tmiDJXG, this.tmiDJCF, this.tmiDJHB, this.tmiDJHY, this.toolStripMenuItem_1 });
            this.cmsDJWH.Name = "cmsDJWH";
            this.cmsDJWH.Size = new Size(0x95, 0x72);
            this.tmiDJXG.Name = "tmiDJXG";
            this.tmiDJXG.Size = new Size(0x94, 0x16);
            this.tmiDJXG.Text = "单据修改";
            this.tmiDJXG.Click += new EventHandler(this.tmiDJXG_Click);
            this.tmiDJCF.Name = "tmiDJCF";
            this.tmiDJCF.Size = new Size(0x94, 0x16);
            this.tmiDJCF.Text = "单据拆分";
            this.tmiDJCF.Click += new EventHandler(this.tmiDJCF_Click);
            this.tmiDJHB.Name = "tmiDJHB";
            this.tmiDJHB.Size = new Size(0x94, 0x16);
            this.tmiDJHB.Text = "单据合并";
            this.tmiDJHB.Click += new EventHandler(this.tmiDJHB_Click);
            this.tmiDJHY.Name = "tmiDJHY";
            this.tmiDJHY.Size = new Size(0x94, 0x16);
            this.tmiDJHY.Text = "单据还原";
            this.tmiDJHY.Click += new EventHandler(this.tmiDJHY_Click);
            this.toolStripMenuItem_1.Name = "tmiDJZKHZ";
            this.toolStripMenuItem_1.Size = new Size(0x94, 0x16);
            this.toolStripMenuItem_1.Text = "单据折扣汇总";
            this.toolStripMenuItem_1.Click += new EventHandler(this.toolStripMenuItem_1_Click);
            this.cmsFPSC.Items.AddRange(new ToolStripItem[] { this.tmiZYFP, this.tmiPTFP, this.toolStripMenuItem_2, this.toolStripMenuItem_3, this.tmiHYFP, this.tmiJDCFP_NEW });
            this.cmsFPSC.Name = "cmsFPSC";
            this.cmsFPSC.Size = new Size(0xdd, 0x9e);
            this.tmiZYFP.Name = "tmiZYFP";
            this.tmiZYFP.Size = new Size(220, 0x16);
            this.tmiZYFP.Text = "增值税专用发票";
            this.tmiZYFP.Click += new EventHandler(this.tmiZYFP_Click);
            this.tmiPTFP.Name = "tmiPTFP";
            this.tmiPTFP.Size = new Size(220, 0x16);
            this.tmiPTFP.Text = "增值税普通发票";
            this.tmiPTFP.Click += new EventHandler(this.tmiPTFP_Click);
            this.toolStripMenuItem_2.Name = "tmiNCPXSFP";
            this.toolStripMenuItem_2.Size = new Size(220, 0x16);
            this.toolStripMenuItem_2.Text = "农产品销售发票";
            this.toolStripMenuItem_2.Click += new EventHandler(this.toolStripMenuItem_2_Click);
            this.toolStripMenuItem_3.Name = "tmiNCPSGFP";
            this.toolStripMenuItem_3.Size = new Size(220, 0x16);
            this.toolStripMenuItem_3.Text = "收购发票";
            this.toolStripMenuItem_3.Click += new EventHandler(this.toolStripMenuItem_3_Click);
            this.tmiHYFP.Name = "tmiHYFP";
            this.tmiHYFP.Size = new Size(220, 0x16);
            this.tmiHYFP.Text = "货物运输业增值税专用发票";
            this.tmiHYFP.Click += new EventHandler(this.tmiHYFP_Click);
            this.tmiJDCFP_NEW.Name = "tmiJDCFP_NEW";
            this.tmiJDCFP_NEW.Size = new Size(220, 0x16);
            this.tmiJDCFP_NEW.Text = "机动车销售统一发票（新）";
            this.tmiJDCFP_NEW.Click += new EventHandler(this.tmiJDCFP_NEW_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(800, 530);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "TextIFaceNavForm";
            this.Text = "单据管理";
            base.Load += new EventHandler(this.TextIFaceNavForm_Load);
            base.Controls.SetChildIndex(base.NavPanle, 0);
            base.NavPanle.ResumeLayout(false);
            this.cmsDJCR.ResumeLayout(false);
            this.cmsDJWH.ResumeLayout(false);
            this.cmsFPSC.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.btnDJCR.Enabled = this.list_0.Contains("_XSDJTxtCR") || this.list_0.Contains("_XSDJExcelCR");
            this.btnDJWH.Enabled = ((this.list_0.Contains("_XSDJXG") || this.list_0.Contains("_XSDJCF")) || (this.list_0.Contains("_XSDJHB") || this.list_0.Contains("_XSDJHY"))) || this.list_0.Contains("_XSDJZKHZ");
            this.btnZFCR.Enabled = this.list_0.Contains("_XSDJZFCR");
            this.btnZFFP.Enabled = this.list_0.Contains("_FaPiaoZF");
            this.btnFPSC.Enabled = (((this.list_0.Contains("_GenerateSpecial") || this.list_0.Contains("_GenerateCommon")) || (this.list_0.Contains("_GenerateCommonNCPXS") || this.list_0.Contains("_GenerateCommonNCPSG"))) || this.list_0.Contains("_GenerateTransportation")) || this.list_0.Contains("_GenerateVehiclesalesNew");
            this.btnFPCX.Enabled = this.list_0.Contains("_FaPiaoCX");
            this.btnSPCX.Enabled = this.list_0.Contains("_ShangPinCX");
            this.btnFPCC.Enabled = this.list_0.Contains("_FaPiaoCC");
            this.tmiWBCR.Visible = this.list_0.Contains("_XSDJTxtCR");
            this.toolStripMenuItem_0.Visible = this.list_0.Contains("_XSDJExcelCR");
            this.tmiDJXG.Visible = this.list_0.Contains("_XSDJXG");
            this.tmiDJCF.Visible = this.list_0.Contains("_XSDJCF");
            this.tmiDJHB.Visible = this.list_0.Contains("_XSDJHB");
            this.tmiDJHY.Visible = this.list_0.Contains("_XSDJHY");
            this.toolStripMenuItem_1.Visible = this.list_0.Contains("_XSDJZKHZ");
            this.tmiZYFP.Visible = this.list_0.Contains("_GenerateSpecial");
            this.tmiPTFP.Visible = this.list_0.Contains("_GenerateCommon");
            this.toolStripMenuItem_2.Visible = this.list_0.Contains("_GenerateCommonNCPXS");
            this.toolStripMenuItem_3.Visible = this.list_0.Contains("_GenerateCommonNCPSG");
            this.tmiHYFP.Visible = this.list_0.Contains("_GenerateTransportation");
            this.tmiJDCFP_NEW.Visible = this.list_0.Contains("_GenerateVehiclesalesNew");
        }

        private void method_1(string string_0)
        {
            ToolUtil.RunFuction(string_0);
        }

        private void TextIFaceNavForm_Load(object sender, EventArgs e)
        {
            Class82.smethod_0(base.MdiParent.MainMenuStrip, this.list_1, this.list_0);
            this.method_0();
        }

        private void tmiDJCF_Click(object sender, EventArgs e)
        {
            this.method_1("_XSDJCF");
        }

        private void tmiDJHB_Click(object sender, EventArgs e)
        {
            this.method_1("_XSDJHB");
        }

        private void tmiDJHY_Click(object sender, EventArgs e)
        {
            this.method_1("_XSDJHY");
        }

        private void tmiDJXG_Click(object sender, EventArgs e)
        {
            this.method_1("_XSDJXG");
        }

        private void tmiHYFP_Click(object sender, EventArgs e)
        {
            this.method_1("_GenerateTransportation");
        }

        private void tmiJDCFP_NEW_Click(object sender, EventArgs e)
        {
            this.method_1("_GenerateVehiclesalesNew");
        }

        private void tmiPTFP_Click(object sender, EventArgs e)
        {
            this.method_1("_GenerateCommon");
        }

        private void tmiWBCR_Click(object sender, EventArgs e)
        {
            this.method_1("_XSDJTxtCR");
        }

        private void tmiZYFP_Click(object sender, EventArgs e)
        {
            this.method_1("_GenerateSpecial");
        }

        private void toolStripMenuItem_0_Click(object sender, EventArgs e)
        {
            this.method_1("_XSDJExcelCR");
        }

        private void toolStripMenuItem_1_Click(object sender, EventArgs e)
        {
            this.method_1("_XSDJZKHZ");
        }

        private void toolStripMenuItem_2_Click(object sender, EventArgs e)
        {
            this.method_1("_GenerateCommonNCPXS");
        }

        private void toolStripMenuItem_3_Click(object sender, EventArgs e)
        {
            this.method_1("_GenerateCommonNCPSG");
        }
    }
}

