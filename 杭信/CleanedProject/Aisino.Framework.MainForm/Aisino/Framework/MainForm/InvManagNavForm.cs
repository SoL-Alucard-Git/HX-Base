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

    public class InvManagNavForm : BaseNavForm
    {
        private Button btnFPCX;
        private Button btnFPDR;
        private Button btnFPFP;
        private Button btnFPTH;
        private Button btnFPTK;
        private Button btnFPXF;
        private Button btnFPZF;
        private Button btnKCCX;
        private Button btnXXBTK;
        private ContextMenuStrip cmsFPTK;
        private ContextMenuStrip cmsXXBTK;
        private IContainer icontainer_1;
        private List<string> list_0;
        private List<string> list_1;
        private ToolStripMenuItem tmiFPTK_DZFP;
        private ToolStripMenuItem tmiFPTK_HYFP;
        private ToolStripMenuItem tmiFPTK_JDCFP;
        private ToolStripMenuItem tmiFPTK_JSFP;
        private ToolStripMenuItem tmiFPTK_NCPSG;
        private ToolStripMenuItem tmiFPTK_NCPXS;
        private ToolStripMenuItem tmiFPTK_PTFP;
        private ToolStripMenuItem tmiFPTK_SNY;
        private ToolStripMenuItem tmiFPTK_ZYFP;
        private ToolStripMenuItem tmiXXBTK_HYFP;
        private ToolStripMenuItem tmiXXBTK_HYFP_YQ;
        private ToolStripMenuItem tmiXXBTK_ZYFP;
        private ToolStripMenuItem tmiXXBTK_ZYFP_YQ;

        public InvManagNavForm()
        {
            
            this.InitializeComponent_1();
            base.FormTagFlag = "NavForm";
            this.list_0 = new List<string>();
            this.list_1 = new List<string>();
            base.Load += new EventHandler(this.InvManagNavForm_Load);
            this.list_1.Add("Menu.Fplygl.Lygl.Drxgfp.Drxgfpcard");
            this.list_1.Add("Menu.Fplygl.Lygl.Fjfpgl.Xfjfpfp");
            this.list_1.Add("Menu.Fplygl.Fpkccx");
            this.list_1.Add("Menu.Fplygl.Lygl.Drxgfp.YgfpthCard");
            this.list_1.Add("Menu.Fpgl.Fptk.ZyFptk");
            this.list_1.Add("Menu.Fpgl.Fptk.PtFptk");
            this.list_1.Add("Menu.Fpgl.Fptk.HyFptk");
            this.list_1.Add("Menu.Fpgl.Fptk.JdcFptk");
            this.list_1.Add("Menu.Fpgl.Fptk.SNYFptk");
            this.list_1.Add("Menu.Fpgl.Fptk.NCPXSFptk");
            this.list_1.Add("Menu.Fpgl.Fptk.NCPSGFptk");
            this.list_1.Add("Menu.Fpgl.Fptk.DzFptk");
            this.list_1.Add("Menu.Fpgl.Fptk.JsFptk");
            this.list_1.Add("Menu.Fpgl.Fpkj.ykfpcx");
            this.list_1.Add("Menu.Fpgl.Fpkj.ykfpzf");
            this.list_1.Add("Menu.Fpgl.Fpkj.Fpxf");
            this.list_1.Add("Menu.Hzfp.TianKai");
            this.list_1.Add("Menu.Hzfp.TianKaiYQ");
            this.list_1.Add("Menu.HzfpHy.HyTianKai");
            this.list_1.Add("Menu.HzfpHy.TianKaiYQ");
        }

        private void btnFPCX_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fpkj.ykfpcx");
        }

        private void btnFPDR_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fplygl.Lygl.Drxgfp.Drxgfpcard");
        }

        private void btnFPFP_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fplygl.Lygl.Fjfpgl.Xfjfpfp");
        }

        private void btnFPTH_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fplygl.Lygl.Drxgfp.YgfpthCard");
        }

        private void btnFPTK_Click(object sender, EventArgs e)
        {
            Button control = (Button) sender;
            this.cmsFPTK.Show(control, new Point(0, 0), ToolStripDropDownDirection.BelowLeft);
        }

        private void btnFPXF_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fpkj.Fpxf");
        }

        private void btnFPZF_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fpkj.ykfpzf");
        }

        private void btnKCCX_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fplygl.Fpkccx");
        }

        private void btnXXBTK_Click(object sender, EventArgs e)
        {
            Button control = (Button) sender;
            this.cmsXXBTK.Show(control, new Point(0, 0), ToolStripDropDownDirection.BelowLeft);
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(InvManagNavForm));
            this.btnXXBTK = new Button();
            this.btnFPTK = new Button();
            this.btnFPXF = new Button();
            this.btnFPTH = new Button();
            this.btnFPZF = new Button();
            this.btnKCCX = new Button();
            this.btnFPCX = new Button();
            this.btnFPFP = new Button();
            this.btnFPDR = new Button();
            this.cmsFPTK = new ContextMenuStrip(this.icontainer_1);
            this.tmiFPTK_ZYFP = new ToolStripMenuItem();
            this.tmiFPTK_PTFP = new ToolStripMenuItem();
            this.tmiFPTK_HYFP = new ToolStripMenuItem();
            this.tmiFPTK_JDCFP = new ToolStripMenuItem();
            this.tmiFPTK_SNY = new ToolStripMenuItem();
            this.tmiFPTK_NCPXS = new ToolStripMenuItem();
            this.tmiFPTK_NCPSG = new ToolStripMenuItem();
            this.tmiFPTK_DZFP = new ToolStripMenuItem();
            this.tmiFPTK_JSFP = new ToolStripMenuItem();
            this.cmsXXBTK = new ContextMenuStrip(this.icontainer_1);
            this.tmiXXBTK_ZYFP = new ToolStripMenuItem();
            this.tmiXXBTK_ZYFP_YQ = new ToolStripMenuItem();
            this.tmiXXBTK_HYFP = new ToolStripMenuItem();
            this.tmiXXBTK_HYFP_YQ = new ToolStripMenuItem();
            base.NavPanle.SuspendLayout();
            this.cmsFPTK.SuspendLayout();
            this.cmsXXBTK.SuspendLayout();
            base.SuspendLayout();
            base.NavPanle.BackgroundImage = Resources.smethod_42();
            base.NavPanle.Controls.Add(this.btnXXBTK);
            base.NavPanle.Controls.Add(this.btnFPTK);
            base.NavPanle.Controls.Add(this.btnFPXF);
            base.NavPanle.Controls.Add(this.btnFPTH);
            base.NavPanle.Controls.Add(this.btnFPZF);
            base.NavPanle.Controls.Add(this.btnKCCX);
            base.NavPanle.Controls.Add(this.btnFPCX);
            base.NavPanle.Controls.Add(this.btnFPFP);
            base.NavPanle.Controls.Add(this.btnFPDR);
            this.btnXXBTK.Image = Resources.smethod_13();
            this.btnXXBTK.Location = new Point(0x2a7, 0x145);
            this.btnXXBTK.Name = "btnXXBTK";
            this.btnXXBTK.Size = new Size(0x41, 0x4b);
            this.btnXXBTK.TabIndex = 9;
            this.btnXXBTK.UseVisualStyleBackColor = true;
            this.btnXXBTK.Click += new EventHandler(this.btnXXBTK_Click);
            this.btnFPTK.Image = Resources.smethod_22();
            this.btnFPTK.Location = new Point(0x18c, 0xe4);
            this.btnFPTK.Name = "btnFPTK";
            this.btnFPTK.Size = new Size(0x41, 0x4b);
            this.btnFPTK.TabIndex = 12;
            this.btnFPTK.UseVisualStyleBackColor = true;
            this.btnFPTK.Click += new EventHandler(this.btnFPTK_Click);
            this.btnFPXF.Image = Resources.smethod_20();
            this.btnFPXF.Location = new Point(0x2a7, 0x80);
            this.btnFPXF.Name = "btnFPXF";
            this.btnFPXF.Size = new Size(0x41, 0x4b);
            this.btnFPXF.TabIndex = 11;
            this.btnFPXF.UseVisualStyleBackColor = true;
            this.btnFPXF.Click += new EventHandler(this.btnFPXF_Click);
            this.btnFPTH.Image = Resources.smethod_28();
            this.btnFPTH.Location = new Point(0xd9, 0x18e);
            this.btnFPTH.Name = "btnFPTH";
            this.btnFPTH.Size = new Size(0x41, 0x4b);
            this.btnFPTH.TabIndex = 13;
            this.btnFPTH.UseVisualStyleBackColor = true;
            this.btnFPTH.Click += new EventHandler(this.btnFPTH_Click);
            this.btnFPZF.Image = Resources.smethod_19();
            this.btnFPZF.Location = new Point(0x221, 0x145);
            this.btnFPZF.Name = "btnFPZF";
            this.btnFPZF.Size = new Size(0x41, 0x4b);
            this.btnFPZF.TabIndex = 10;
            this.btnFPZF.UseVisualStyleBackColor = true;
            this.btnFPZF.Click += new EventHandler(this.btnFPZF_Click);
            this.btnKCCX.Image = Resources.smethod_33();
            this.btnKCCX.Location = new Point(0xd9, 0xe4);
            this.btnKCCX.Name = "btnKCCX";
            this.btnKCCX.Size = new Size(0x41, 0x4b);
            this.btnKCCX.TabIndex = 14;
            this.btnKCCX.UseVisualStyleBackColor = true;
            this.btnKCCX.Click += new EventHandler(this.btnKCCX_Click);
            this.btnFPCX.Image = Resources.smethod_23();
            this.btnFPCX.Location = new Point(0x221, 0x80);
            this.btnFPCX.Name = "btnFPCX";
            this.btnFPCX.Size = new Size(0x41, 0x4b);
            this.btnFPCX.TabIndex = 7;
            this.btnFPCX.UseVisualStyleBackColor = true;
            this.btnFPCX.Click += new EventHandler(this.btnFPCX_Click);
            this.btnFPFP.Image = Resources.smethod_21();
            this.btnFPFP.Location = new Point(0xd9, 0x3a);
            this.btnFPFP.Name = "btnFPFP";
            this.btnFPFP.Size = new Size(0x41, 0x4b);
            this.btnFPFP.TabIndex = 6;
            this.btnFPFP.UseVisualStyleBackColor = true;
            this.btnFPFP.Click += new EventHandler(this.btnFPFP_Click);
            this.btnFPDR.Image = Resources.smethod_26();
            this.btnFPDR.Location = new Point(50, 0xe4);
            this.btnFPDR.Name = "btnFPDR";
            this.btnFPDR.Size = new Size(0x41, 0x4b);
            this.btnFPDR.TabIndex = 8;
            this.btnFPDR.UseVisualStyleBackColor = true;
            this.btnFPDR.Click += new EventHandler(this.btnFPDR_Click);
            this.cmsFPTK.Items.AddRange(new ToolStripItem[] { this.tmiFPTK_ZYFP, this.tmiFPTK_PTFP, this.tmiFPTK_HYFP, this.tmiFPTK_JDCFP, this.tmiFPTK_SNY, this.tmiFPTK_NCPXS, this.tmiFPTK_NCPSG, this.tmiFPTK_DZFP, this.tmiFPTK_JSFP });
            this.cmsFPTK.Name = "cmsFPTK";
            this.cmsFPTK.Size = new Size(0xf5, 0xe0);
            this.tmiFPTK_ZYFP.Name = "tmiFPTK_ZYFP";
            this.tmiFPTK_ZYFP.Size = new Size(0xf4, 0x16);
            this.tmiFPTK_ZYFP.Text = "增值税专用发票填开";
            this.tmiFPTK_ZYFP.Click += new EventHandler(this.tmiFPTK_ZYFP_Click);
            this.tmiFPTK_PTFP.Name = "tmiFPTK_PTFP";
            this.tmiFPTK_PTFP.Size = new Size(0xf4, 0x16);
            this.tmiFPTK_PTFP.Text = "增值税普通发票填开";
            this.tmiFPTK_PTFP.Click += new EventHandler(this.tmiFPTK_PTFP_Click);
            this.tmiFPTK_HYFP.Name = "tmiFPTK_HYFP";
            this.tmiFPTK_HYFP.Size = new Size(0xf4, 0x16);
            this.tmiFPTK_HYFP.Text = "货物运输业增值税专用发票填开";
            this.tmiFPTK_HYFP.Click += new EventHandler(this.tmiFPTK_HYFP_Click);
            this.tmiFPTK_JDCFP.Name = "tmiFPTK_JDCFP";
            this.tmiFPTK_JDCFP.Size = new Size(0xf4, 0x16);
            this.tmiFPTK_JDCFP.Text = "机动车销售统一发票填开";
            this.tmiFPTK_JDCFP.Click += new EventHandler(this.tmiFPTK_JDCFP_Click);
            this.tmiFPTK_SNY.Name = "tmiFPTK_SNY";
            this.tmiFPTK_SNY.Size = new Size(0xf4, 0x16);
            this.tmiFPTK_SNY.Text = "石脑油、燃料油发票填开";
            this.tmiFPTK_SNY.Click += new EventHandler(this.tmiFPTK_SNY_Click);
            this.tmiFPTK_NCPXS.Name = "tmiFPTK_NCPXS";
            this.tmiFPTK_NCPXS.Size = new Size(0xf4, 0x16);
            this.tmiFPTK_NCPXS.Text = "农产品销售发票填开";
            this.tmiFPTK_NCPXS.Click += new EventHandler(this.tmiFPTK_NCPXS_Click);
            this.tmiFPTK_NCPSG.Name = "tmiFPTK_NCPSG";
            this.tmiFPTK_NCPSG.Size = new Size(0xf4, 0x16);
            this.tmiFPTK_NCPSG.Text = "收购发票填开";
            this.tmiFPTK_NCPSG.Click += new EventHandler(this.tmiFPTK_NCPSG_Click);
            this.tmiFPTK_DZFP.Name = "tmiFPTK_DZFP";
            this.tmiFPTK_DZFP.Size = new Size(0xf4, 0x16);
            this.tmiFPTK_DZFP.Text = "电子增值税普通发票填开";
            this.tmiFPTK_DZFP.Click += new EventHandler(this.tmiFPTK_DZFP_Click);
            this.tmiFPTK_JSFP.Name = "tmiFPTK_JSFP";
            this.tmiFPTK_JSFP.Size = new Size(0xf4, 0x16);
            this.tmiFPTK_JSFP.Text = "增值税普通发票(卷票)填开";
            this.tmiFPTK_JSFP.Click += new EventHandler(this.tmiFPTK_JSFP_Click);
            this.cmsXXBTK.Items.AddRange(new ToolStripItem[] { this.tmiXXBTK_ZYFP, this.tmiXXBTK_ZYFP_YQ, this.tmiXXBTK_HYFP, this.tmiXXBTK_HYFP_YQ });
            this.cmsXXBTK.Name = "cmsXXBTK";
            this.cmsXXBTK.Size = new Size(0x149, 0x5c);
            this.tmiXXBTK_ZYFP.Name = "tmiXXBTK_ZYFP";
            this.tmiXXBTK_ZYFP.Size = new Size(0x148, 0x16);
            this.tmiXXBTK_ZYFP.Text = "红字增值税专用发票信息表填开";
            this.tmiXXBTK_ZYFP.Click += new EventHandler(this.tmiXXBTK_ZYFP_Click);
            this.tmiXXBTK_ZYFP_YQ.Name = "tmiXXBTK_ZYFP_YQ";
            this.tmiXXBTK_ZYFP_YQ.Size = new Size(0x148, 0x16);
            this.tmiXXBTK_ZYFP_YQ.Text = "逾期红字增值税专用发票信息表填开";
            this.tmiXXBTK_ZYFP_YQ.Click += new EventHandler(this.tmiXXBTK_ZYFP_YQ_Click);
            this.tmiXXBTK_HYFP.Name = "tmiXXBTK_HYFP";
            this.tmiXXBTK_HYFP.Size = new Size(0x148, 0x16);
            this.tmiXXBTK_HYFP.Text = "红字货物运输业增值税专用发票信息表填开";
            this.tmiXXBTK_HYFP.Click += new EventHandler(this.tmiXXBTK_HYFP_Click);
            this.tmiXXBTK_HYFP_YQ.Name = "tmiXXBTK_HYFP_YQ";
            this.tmiXXBTK_HYFP_YQ.Size = new Size(0x148, 0x16);
            this.tmiXXBTK_HYFP_YQ.Text = "逾期红字货物运输业增值税专用发票信息表填开";
            this.tmiXXBTK_HYFP_YQ.Click += new EventHandler(this.tmiXXBTK_HYFP_YQ_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(800, 530);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "InvManagNavForm";
            this.Text = "发票管理";
            base.Load += new EventHandler(this.InvManagNavForm_Load);
            base.Controls.SetChildIndex(base.NavPanle, 0);
            base.NavPanle.ResumeLayout(false);
            this.cmsFPTK.ResumeLayout(false);
            this.cmsXXBTK.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void InvManagNavForm_Load(object sender, EventArgs e)
        {
            Class82.smethod_0(base.MdiParent.MainMenuStrip, this.list_1, this.list_0);
            this.method_0();
        }

        private void method_0()
        {
            this.btnFPDR.Enabled = this.list_0.Contains("Menu.Fplygl.Lygl.Drxgfp.Drxgfpcard");
            this.btnFPFP.Enabled = this.list_0.Contains("Menu.Fplygl.Lygl.Fjfpgl.Xfjfpfp");
            this.btnKCCX.Enabled = this.list_0.Contains("Menu.Fplygl.Fpkccx");
            this.btnFPTH.Enabled = this.list_0.Contains("Menu.Fplygl.Lygl.Drxgfp.YgfpthCard");
            this.btnFPCX.Enabled = this.list_0.Contains("Menu.Fpgl.Fpkj.ykfpcx");
            this.btnFPZF.Enabled = this.list_0.Contains("Menu.Fpgl.Fpkj.ykfpzf");
            this.btnFPXF.Enabled = this.list_0.Contains("Menu.Fpgl.Fpkj.Fpxf");
            this.btnFPTK.Enabled = (((this.list_0.Contains("Menu.Fpgl.Fptk.ZyFptk") || this.list_0.Contains("Menu.Fpgl.Fptk.PtFptk")) || (this.list_0.Contains("Menu.Fpgl.Fptk.HyFptk") || this.list_0.Contains("Menu.Fpgl.Fptk.JdcFptk"))) || ((this.list_0.Contains("Menu.Fpgl.Fptk.SNYFptk") || this.list_0.Contains("Menu.Fpgl.Fptk.NCPXSFptk")) || (this.list_0.Contains("Menu.Fpgl.Fptk.NCPSGFptk") || this.list_0.Contains("Menu.Fpgl.Fptk.JsFptk")))) || this.list_0.Contains("Menu.Fpgl.Fptk.DzFptk");
            this.btnXXBTK.Enabled = ((this.list_0.Contains("Menu.Hzfp.TianKai") || this.list_0.Contains("Menu.Hzfp.TianKaiYQ")) || this.list_0.Contains("Menu.HzfpHy.HyTianKai")) || this.list_0.Contains("Menu.HzfpHy.TianKaiYQ");
            this.tmiFPTK_ZYFP.Visible = this.list_0.Contains("Menu.Fpgl.Fptk.ZyFptk");
            this.tmiFPTK_PTFP.Visible = this.list_0.Contains("Menu.Fpgl.Fptk.PtFptk");
            this.tmiFPTK_HYFP.Visible = this.list_0.Contains("Menu.Fpgl.Fptk.HyFptk");
            this.tmiFPTK_JDCFP.Visible = this.list_0.Contains("Menu.Fpgl.Fptk.JdcFptk");
            this.tmiFPTK_SNY.Visible = this.list_0.Contains("Menu.Fpgl.Fptk.SNYFptk");
            this.tmiFPTK_NCPXS.Visible = this.list_0.Contains("Menu.Fpgl.Fptk.NCPXSFptk");
            this.tmiFPTK_NCPSG.Visible = this.list_0.Contains("Menu.Fpgl.Fptk.NCPSGFptk");
            this.tmiFPTK_DZFP.Visible = this.list_0.Contains("Menu.Fpgl.Fptk.DzFptk");
            this.tmiFPTK_JSFP.Visible = this.list_0.Contains("Menu.Fpgl.Fptk.JsFptk");
            this.tmiXXBTK_ZYFP.Visible = this.list_0.Contains("Menu.Hzfp.TianKai");
            this.tmiXXBTK_ZYFP_YQ.Visible = this.list_0.Contains("Menu.Hzfp.TianKaiYQ");
            this.tmiXXBTK_HYFP.Visible = this.list_0.Contains("Menu.HzfpHy.HyTianKai");
            this.tmiXXBTK_HYFP_YQ.Visible = this.list_0.Contains("Menu.HzfpHy.TianKaiYQ");
        }

        private void method_1(string string_0)
        {
            ToolUtil.RunFuction(string_0);
        }

        private void tmiFPTK_DZFP_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fptk.DzFptk");
        }

        private void tmiFPTK_HYFP_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fptk.HyFptk");
        }

        private void tmiFPTK_JDCFP_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fptk.JdcFptk");
        }

        private void tmiFPTK_JSFP_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fptk.JsFptk");
        }

        private void tmiFPTK_NCPSG_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fptk.NCPSGFptk");
        }

        private void tmiFPTK_NCPXS_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fptk.NCPXSFptk");
        }

        private void tmiFPTK_PTFP_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fptk.PtFptk");
        }

        private void tmiFPTK_SNY_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fptk.SNYFptk");
        }

        private void tmiFPTK_ZYFP_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Fpgl.Fptk.ZyFptk");
        }

        private void tmiXXBTK_HYFP_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.HzfpHy.HyTianKai");
        }

        private void tmiXXBTK_HYFP_YQ_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.HzfpHy.TianKaiYQ");
        }

        private void tmiXXBTK_ZYFP_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Hzfp.TianKai");
        }

        private void tmiXXBTK_ZYFP_YQ_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Hzfp.TianKaiYQ");
        }
    }
}

