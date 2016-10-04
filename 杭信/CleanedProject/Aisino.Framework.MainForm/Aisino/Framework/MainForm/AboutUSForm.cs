namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class AboutUSForm : MessageBaseForm
    {
        private AisinoBTN aisinoBTN_0;
        private GroupBox grpSetup;
        private GroupBox grpWho;
        private IContainer icontainer_1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblBQ;
        private Label lblCorpName;
        private Label lblProName;
        private Label lblTaxCode;
        private Label lblVersion;
        private LinkLabel linkLabel1;
        private PictureBox picBQTP;
        private Panel pnlBody;
        private Panel pnlBottom;
        private RichTextBox rtbSetup;

        public AboutUSForm()
        {
            
            this.InitializeComponent_1();
            this.method_1();
        }

        private void aisinoBTN_0_Click(object sender, EventArgs e)
        {
            ToolUtil.RunFuction("Menu.Xtgl.Xtzc");
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AboutUSForm));
            this.pnlBottom = new Panel();
            this.label4 = new Label();
            this.pnlBody = new Panel();
            this.label2 = new Label();
            this.linkLabel1 = new LinkLabel();
            this.grpSetup = new GroupBox();
            this.aisinoBTN_0 = new AisinoBTN();
            this.rtbSetup = new RichTextBox();
            this.grpWho = new GroupBox();
            this.lblCorpName = new Label();
            this.lblTaxCode = new Label();
            this.label3 = new Label();
            this.label1 = new Label();
            this.lblBQ = new Label();
            this.lblVersion = new Label();
            this.lblProName = new Label();
            this.picBQTP = new PictureBox();
            base.pnlForm.SuspendLayout();
            base.TitleArea.SuspendLayout();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.grpSetup.SuspendLayout();
            this.grpWho.SuspendLayout();
            ((ISupportInitialize) this.picBQTP).BeginInit();
            base.SuspendLayout();
            base.pnlForm.Size = new Size(470, 400);
            base.TitleArea.Size = new Size(470, 30);
            base.pnlCaption.Size = new Size(470, 30);
            base.lblTitle.Size = new Size(440, 30);
            base.lblTitle.Text = "版权信息";
            base.BodyBounds.Size = new Size(470, 370);
            base.BodyClient.Controls.Add(this.pnlBody);
            base.BodyClient.Controls.Add(this.pnlBottom);
            base.BodyClient.Size = new Size(0x1ca, 0x16c);
            this.pnlBottom.BackColor = Color.FromArgb(0xe4, 0xe7, 0xe9);
            this.pnlBottom.Controls.Add(this.label4);
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Location = new Point(0, 0x116);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new Size(0x1ca, 0x56);
            this.pnlBottom.TabIndex = 0;
            this.label4.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label4.ForeColor = SystemColors.ControlDarkDark;
            this.label4.Location = new Point(8, 11);
            this.label4.Name = "label4";
            this.label4.Size = new Size(440, 0x40);
            this.label4.TabIndex = 0;
            this.label4.Text = "警告：本计算机程序受著作权法和国际公约的保护，未经授权擅自复制或传播本程序的部分或全部，可能受到严厉的民事及刑事制裁，并将在法律许可的范围内受到最大可能的起诉。";
            this.pnlBody.Controls.Add(this.label2);
            this.pnlBody.Controls.Add(this.linkLabel1);
            this.pnlBody.Controls.Add(this.grpSetup);
            this.pnlBody.Controls.Add(this.grpWho);
            this.pnlBody.Controls.Add(this.lblBQ);
            this.pnlBody.Controls.Add(this.lblVersion);
            this.pnlBody.Controls.Add(this.lblProName);
            this.pnlBody.Controls.Add(this.picBQTP);
            this.pnlBody.Dock = DockStyle.Fill;
            this.pnlBody.Location = new Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new Size(0x1ca, 0x116);
            this.pnlBody.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x161, 0x3a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "保留所有权利";
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new Point(0xe1, 0x3a);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new Size(0x7d, 12);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "航天信息股份有限公司";
            this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            this.grpSetup.Controls.Add(this.aisinoBTN_0);
            this.grpSetup.Controls.Add(this.rtbSetup);
            this.grpSetup.Location = new Point(0x6c, 0xa8);
            this.grpSetup.Name = "grpSetup";
            this.grpSetup.Size = new Size(0x156, 0x65);
            this.grpSetup.TabIndex = 5;
            this.grpSetup.TabStop = false;
            this.grpSetup.Text = "已安装版本";
            this.aisinoBTN_0.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.aisinoBTN_0.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.aisinoBTN_0.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.aisinoBTN_0.Font = new Font("宋体", 9f);
            this.aisinoBTN_0.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.aisinoBTN_0.Location = new Point(0x120, 0x20);
            this.aisinoBTN_0.Name = "aisinoBTN1";
            this.aisinoBTN_0.Size = new Size(0x29, 0x35);
            this.aisinoBTN_0.TabIndex = 1;
            this.aisinoBTN_0.Text = "版本信息";
            this.aisinoBTN_0.UseVisualStyleBackColor = true;
            this.aisinoBTN_0.Click += new EventHandler(this.aisinoBTN_0_Click);
            this.rtbSetup.Location = new Point(8, 0x15);
            this.rtbSetup.Name = "rtbSetup";
            this.rtbSetup.ReadOnly = true;
            this.rtbSetup.Size = new Size(0x108, 0x4a);
            this.rtbSetup.TabIndex = 0;
            this.rtbSetup.Text = "";
            this.grpWho.Controls.Add(this.lblCorpName);
            this.grpWho.Controls.Add(this.lblTaxCode);
            this.grpWho.Controls.Add(this.label3);
            this.grpWho.Controls.Add(this.label1);
            this.grpWho.Location = new Point(0x6c, 0x55);
            this.grpWho.Name = "grpWho";
            this.grpWho.Size = new Size(0x156, 0x4a);
            this.grpWho.TabIndex = 4;
            this.grpWho.TabStop = false;
            this.grpWho.Text = "本产品使用权属于";
            this.lblCorpName.AutoSize = true;
            this.lblCorpName.Location = new Point(0x45, 50);
            this.lblCorpName.Name = "lblCorpName";
            this.lblCorpName.Size = new Size(0x35, 12);
            this.lblCorpName.TabIndex = 7;
            this.lblCorpName.Text = "测试企业";
            this.lblTaxCode.AutoSize = true;
            this.lblTaxCode.Location = new Point(0x45, 0x1b);
            this.lblTaxCode.Name = "lblTaxCode";
            this.lblTaxCode.Size = new Size(0x5f, 12);
            this.lblTaxCode.TabIndex = 6;
            this.lblTaxCode.Text = "110101123123123";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(15, 50);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "名称：";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(15, 0x1b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "税号：";
            this.lblBQ.AutoSize = true;
            this.lblBQ.Location = new Point(0x6c, 0x3a);
            this.lblBQ.Name = "lblBQ";
            this.lblBQ.Size = new Size(0x71, 12);
            this.lblBQ.TabIndex = 3;
            this.lblBQ.Text = "版权所有@2014-2016";
            this.lblVersion.Location = new Point(110, 0x24);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new Size(0x152, 12);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "V2.0.10.20141210";
            this.lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            this.lblProName.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblProName.Location = new Point(110, 11);
            this.lblProName.Name = "lblProName";
            this.lblProName.Size = new Size(0x152, 0x13);
            this.lblProName.TabIndex = 1;
            this.lblProName.Text = "税控发票开票软件（金税盘版）";
            this.lblProName.TextAlign = ContentAlignment.MiddleCenter;
            this.picBQTP.Image = (Image) manager.GetObject("picBQTP.Image");
            this.picBQTP.Location = new Point(6, 8);
            this.picBQTP.Name = "picBQTP";
            this.picBQTP.Size = new Size(0x5e, 0x108);
            this.picBQTP.TabIndex = 0;
            this.picBQTP.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(470, 400);
            base.Name = "AboutUSForm";
            this.Text = "AboutUSForm";
            base.pnlForm.ResumeLayout(false);
            base.TitleArea.ResumeLayout(false);
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.grpSetup.ResumeLayout(false);
            this.grpWho.ResumeLayout(false);
            this.grpWho.PerformLayout();
            ((ISupportInitialize) this.picBQTP).EndInit();
            base.ResumeLayout(false);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.aisino.com");
        }

        private void method_1()
        {
            this.lblVersion.Text = PropertyUtil.GetValue("MAIN_VER", "");
            TaxCard card = TaxCardFactory.CreateTaxCard();
            this.lblTaxCode.Text = card.TaxCode.ToString();
            this.lblCorpName.Text = card.Corporation;
            this.lblProName.Text = ProductName;
            string productName = ProductName;
            //TODO 此处的修改显示和原来不同了，然而这个是在About里面并没有什么影响
            if ((card.TaxCode.Length == 15) && (card.TaxCode.Substring(8, 2).ToUpper() == "DK"))
            {
                productName = ((productName + Environment.NewLine) + "    总局发布版本号：" + ProductVersion + Environment.NewLine) + "    内部版本号：" + ProductVersion + Environment.NewLine;
            }
            else
            {
                productName = ((productName + Environment.NewLine) + "    总局发布版本号：" + ProductVersion + Environment.NewLine) + "    内部版本号：" + ProductVersion + Environment.NewLine;
            }
            this.rtbSetup.Text = "";
            this.rtbSetup.AppendText(productName);
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Automation\Config.ini");
            if (File.Exists(path))
            {
                List<IniFileUtil.SetupFile> list = IniFileUtil.ReadSetupConfig(path);
                foreach (RegFileInfo info in RegisterManager.SetupRegFile().NormalRegFiles)
                {
                    using (List<IniFileUtil.SetupFile>.Enumerator enumerator2 = list.GetEnumerator())
                    {
                        IniFileUtil.SetupFile current;
                        while (enumerator2.MoveNext())
                        {
                            current = enumerator2.Current;
                            if (current.Kind.ToUpper() == info.VerFlag.ToUpper())
                            {
                                goto Label_01A1;
                            }
                        }
                        continue;
                    Label_01A1:
                        current.bNormal = true;
                        this.rtbSetup.AppendText(string.Format("{0}  {1}", current.Name.Replace("文本接口", "单据管理").Replace("组件接口", "集成开票"), current.Ver) + Environment.NewLine);
                    }
                }
            }
        }
    }
}

