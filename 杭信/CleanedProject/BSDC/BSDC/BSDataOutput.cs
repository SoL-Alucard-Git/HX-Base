namespace BSDC
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.CommonLibrary;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.BusinessObject;
    public class BSDataOutput : DockForm
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private BSData bsdata_0;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoBTN btnSelect;
        private AisinoCMB cmbMonth;
        private AisinoCMB cmbPiaoZhong;
        private CommFun commFun_0;
        private AisinoGRP groupBox1;
        private IContainer icontainer_3;
        private ILog ilog_0;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private Label label3;
        private Label label5;
        private AisinoLBL lablePiaoZhong;
        private List<InvTypeEntity> list_0;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private AisinoPIC pictureBox1;
        private string string_0;
        private string string_1;
        private AisinoTXT txtPath;

        public BSDataOutput()
        {
            
            this.string_0 = "";
            this.string_1 = "";
            this.ilog_0 = LogUtil.GetLogger<BSDataOutput>();
            this.list_0 = new List<InvTypeEntity>();
            this.commFun_0 = new CommFun();
            this.bool_2 = true;
            this.ilog_0.Debug("进入报税导出");
            this.InitializeComponent_1();
            List<InvTypeInfo> invTypeInfo = base.TaxCardInstance.StateInfo.InvTypeInfo;
            this.ilog_0.Debug("初始化票种");
            foreach (InvTypeInfo info2 in invTypeInfo)
            {
                if (info2.InvType == 11)
                {
                    this.string_0 = info2.LastRepDate;
                }
                if (info2.InvType == 12)
                {
                    this.string_1 = info2.LastRepDate;
                }
            }
            this.method_3();
            this.ilog_0.Debug("填充月份");
            this.method_4();
            this.ilog_0.Debug("填充月份完毕");
            string str = PropertyUtil.GetValue("Aisino.Fwkp.Bsgl.BSDataOutput.OutputPath", "");
            this.txtPath.Text = str;
            this.txtPath.Enabled = true;
            this.txtPath.ReadOnly = false;
            this.ilog_0.Debug("报税导出初始化完毕");
            TaxCard card = TaxCardFactory.CreateTaxCard();
            this.bool_2 = card.GetExtandParams("FLBMFlag") == "FLBM";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtPath.Text.Trim() == "")
            {
                MessageManager.ShowMsgBox("INP-251206");
            }
            else if (this.method_6())
            {
                base.TaxCardInstance.GetStateInfo(false);
                this.bool_0 = true;
                this.method_7();
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
                {
                    this.bool_1 = true;
                    this.method_9();
                    if (this.bool_1)
                    {
                        this.method_21();
                    }
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票"))
                {
                    this.bool_1 = true;
                    this.method_16();
                    if (this.bool_1)
                    {
                        this.method_21();
                    }
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
                {
                    this.bool_1 = true;
                    this.method_17();
                    if (this.bool_1)
                    {
                        this.method_21();
                    }
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("电子增值税普通发票"))
                {
                    this.bool_1 = true;
                    this.method_18();
                    if (this.bool_1)
                    {
                        this.method_21();
                    }
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税普通发票(卷票)"))
                {
                    this.bool_1 = true;
                    this.method_19();
                    if (this.bool_1)
                    {
                        this.method_21();
                    }
                }
                PropertyUtil.SetValue("Aisino.Fwkp.Bsgl.BSDataOutput.OutputPath", this.txtPath.Text.Trim());
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = dialog.SelectedPath;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_3 != null))
            {
                this.icontainer_3.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BSDataOutput));
            this.panel1 = new AisinoPNL();
            this.label5 = new Label();
            this.pictureBox1 = new AisinoPIC();
            this.panel2 = new AisinoPNL();
            this.label3 = new Label();
            this.btnCancel = new AisinoBTN();
            this.btnOK = new AisinoBTN();
            this.groupBox1 = new AisinoGRP();
            this.cmbPiaoZhong = new AisinoCMB();
            this.lablePiaoZhong = new AisinoLBL();
            this.cmbMonth = new AisinoCMB();
            this.btnSelect = new AisinoBTN();
            this.txtPath = new AisinoTXT();
            this.label2 = new AisinoLBL();
            this.label1 = new AisinoLBL();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x5c, 0x12d);
            this.panel1.TabIndex = 1;
            this.label5.AutoSize = true;
            this.label5.Font = new Font("微软雅黑", 12f);
            this.label5.ForeColor = Color.Blue;
            this.label5.Location = new Point(12, 0x4f);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1a, 0xa8);
            this.label5.TabIndex = 11;
            this.label5.Text = "报\r\n税\r\n资\r\n料\r\n传\r\n出\r\n\r\n\r\n";
            this.pictureBox1.Image = (Image) manager.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(0x27, 120);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x35, 60);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0x5c, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x148, 0x12d);
            this.panel2.TabIndex = 2;
            this.label3.Font = new Font("宋体", 10.5f);
            this.label3.ForeColor = Color.DarkRed;
            this.label3.Location = new Point(11, 15);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x12d, 0x6b);
            this.label3.TabIndex = 4;
            this.label3.Text = "    1.本功能用于将本机报税明细资料传到指定\r\n\r\n磁盘上，生成本期报税资料磁盘文件。\r\n\r\n    2.资料传出产生一个报税文件：本报税月的\r\n\r\n销项防伪发票文件。\r\n";
            this.btnCancel.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnCancel.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnCancel.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnCancel.Font = new Font("宋体", 9f);
            this.btnCancel.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(240, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnOK.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnOK.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnOK.Font = new Font("宋体", 9f);
            this.btnOK.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnOK.ForeColor = Color.White;
            this.btnOK.Location = new Point(0x9c, 260);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 30);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.groupBox1.Controls.Add(this.cmbPiaoZhong);
            this.groupBox1.Controls.Add(this.lablePiaoZhong);
            this.groupBox1.Controls.Add(this.cmbMonth);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(6, 0x7d);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x138, 0x7d);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.cmbPiaoZhong.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPiaoZhong.FormattingEnabled = true;
            this.cmbPiaoZhong.Location = new Point(0x41, 0x38);
            this.cmbPiaoZhong.Name = "cmbPiaoZhong";
            this.cmbPiaoZhong.Size = new Size(0xac, 20);
            this.cmbPiaoZhong.TabIndex = 4;
            this.lablePiaoZhong.AutoSize = true;
            this.lablePiaoZhong.Location = new Point(6, 0x3b);
            this.lablePiaoZhong.Name = "lablePiaoZhong";
            this.lablePiaoZhong.Size = new Size(0x35, 12);
            this.lablePiaoZhong.TabIndex = 1;
            this.lablePiaoZhong.Text = "票种选择";
            this.cmbMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new Point(0x41, 0x17);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new Size(0xac, 20);
            this.cmbMonth.TabIndex = 3;
            this.btnSelect.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnSelect.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnSelect.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnSelect.Font = new Font("宋体", 9f);
            this.btnSelect.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnSelect.ForeColor = Color.White;
            this.btnSelect.Location = new Point(0xfc, 0x56);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(0x36, 0x19);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "浏览";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.txtPath.Location = new Point(0x41, 0x58);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new Size(0xac, 0x15);
            this.txtPath.TabIndex = 5;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 0x5c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "传出路径";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 0x1a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "传出月份";
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(420, 0x12d);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "BSDataOutput";
            base.TabText = "报税资料传出";
            this.Text = "报税资料传出";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private string method_10()
        {
            StringBuilder builder = new StringBuilder();
            if (this.bsdata_0 != null)
            {
                builder.Append(this.bsdata_0.SWJGDM + " ");
                builder.Append(this.bsdata_0.KPNY + " ");
                builder.Append(this.bsdata_0.MXJLS.ToString("D6") + " ");
                builder.Append(this.bsdata_0.KPJH.ToString("D3"));
            }
            return builder.ToString();
        }

        private string method_11()
        {
            StringBuilder builder = new StringBuilder();
            if (this.bsdata_0 != null)
            {
                builder.Append("#");
                builder.Append(this.bsdata_0.NSRName + " ");
                builder.Append(this.bsdata_0.NSRID + " ");
                builder.Append(this.bsdata_0.FPLB.ToString() + "\r\n");
            }
            return builder.ToString();
        }

        private List<string> method_12()
        {
            List<string> list = new List<string>();
            if (this.bsdata_0 != null)
            {
                Encoding encoding = ToolUtil.GetEncoding();
                foreach (FPDetail detail in this.bsdata_0.FPDetailList)
                {
                    if (detail != null)
                    {
                        StringBuilder builder = new StringBuilder();
                        if (detail.ZFBZ)
                        {
                            builder.Append("#");
                        }
                        builder.Append(((int) detail.FPType) + " ");
                        builder.Append(detail.FPDM + " ");
                        builder.Append(detail.FPHM.ToString("D8") + " ");
                        builder.Append(detail.KPRQ.ToString("yyyyMMdd").Substring(2) + " ");
                        builder.Append(detail.GFSH + " ");
                        builder.Append(detail.XFSH + " ");
                        builder.Append(string.Format("{0,16:F2}", detail.HJJE) + " ");
                        if (detail.SLV == 0.015f)
                        {
                            builder.Append(string.Format("{0,6:F2}", 99.01f) + " ");
                        }
                        else if (((detail.FPType == FPType.s) && (detail.SLV == 0.05f)) && (detail.YYSBZ.Substring(8, 1) == "1"))
                        {
                            builder.Append(string.Format("{0,6:F2}", 99.01f) + " ");
                        }
                        else if (detail.YYSBZ.Substring(8, 1) == "2")
                        {
                            builder.Append(string.Format("{0,6:F2}", 99.01f) + " ");
                        }
                        else
                        {
                            builder.Append(string.Format("{0,6:F2}", detail.SLV) + " ");
                        }
                        builder.Append(string.Format("{0,14:F2}", detail.HJSE));
                        builder.Append(this.method_13(detail));
                        if (!this.bool_0 && string.IsNullOrEmpty(detail.HXM))
                        {
                            builder.Append(" 0\r\n");
                        }
                        else
                        {
                            FPDetailDAL ldal = new FPDetailDAL();
                            detail.GoodsList.AddRange(ldal.GetGoodsList_(detail.FPType.ToString(), detail.FPDM, detail.FPHM));
                            if (detail.QDBZ)
                            {
                                detail.QDList.AddRange(ldal.GetGoodsQDList_(detail.FPType.ToString(), detail.FPDM, detail.FPHM));
                            }
                            builder.Append(" 0 ");
                            string str = smethod_0(detail);
                            string str2 = BitConverter.ToString(MD5_Crypt.GetHash(encoding.GetBytes("x" + str + "y"))).Replace("-", "");
                            builder.Append(str2);
                            builder.Append(" ");
                            string str3 = this.method_14(detail);
                            builder.Append(str3);
                            builder.Append("\x00a4");
                            byte[] hash = MD5_Crypt.GetHash(encoding.GetBytes(str3));
                            builder.Append(BitConverter.ToString(hash).Replace("-", ""));
                            builder.Append("\x00a4");
                            builder.Append(encoding.GetBytes(str3).Length);
                            builder.Append("\r\n");
                        }
                        list.Add(builder.ToString());
                    }
                }
            }
            return list;
        }

        private string method_13(FPDetail fpdetail_0)
        {
            string str = "";
            try
            {
                if ((fpdetail_0 == null) || (fpdetail_0.HJJE >= 0M))
                {
                    return str;
                }
                int num = 0;
                if (fpdetail_0.FPType == FPType.s)
                {
                    num = 1;
                }
                else if (fpdetail_0.FPType == FPType.c)
                {
                    num = 0;
                }
                string str5 = NotesUtil.GetInfo(fpdetail_0.BZ, num, "");
                if (fpdetail_0.FPType == FPType.s)
                {
                    str5 = fpdetail_0.HZTZDH.ToString();
                    str = " " + str5 + " TZD";
                }
                if (fpdetail_0.FPType == FPType.c)
                {
                    string str2 = "";
                    string str3 = "";
                    string str4 = fpdetail_0.LZDMHM.ToString();
                    if (((str4.Length > 0) && str4.Contains("_")) && (str4.Split(new char[] { '_' }).Length == 2))
                    {
                        str2 = str4.Split(new char[] { '_' })[0];
                        str3 = str4.Split(new char[] { '_' })[1];
                        str = " " + str2 + " " + str3;
                    }
                }
            }
            catch (Exception)
            {
            }
            return str;
        }

        private string method_14(FPDetail fpdetail_0)
        {
            string str = "";
            if (fpdetail_0 != null)
            {
                str = str + "BARCODEKEY\x00a4";
                string str3 = string.Empty;
                if (fpdetail_0.YYSBZ.Substring(2, 1) == "1")
                {
                    str3 = "V2";
                }
                else if (fpdetail_0.YYSBZ.Substring(2, 1) == "2")
                {
                    str3 = "V3";
                }
                else if (fpdetail_0.YYSBZ.Substring(2, 1) == "3")
                {
                    str3 = "V4";
                }
                else if (fpdetail_0.YYSBZ.Substring(5, 1) == "1")
                {
                    str3 = "V5";
                }
                else if (fpdetail_0.YYSBZ.Substring(5, 1) == "2")
                {
                    str3 = "V6";
                }
                else if (fpdetail_0.YYSBZ.Substring(1, 1) == "1")
                {
                    str3 = "V1";
                }
                if (this.bool_2 && (fpdetail_0.BMBBBH.Trim() != ""))
                {
                    str3 = str3 + "B" + fpdetail_0.BMBBBH;
                }
                if (((fpdetail_0.FPType != FPType.s) || (fpdetail_0.SLV != 0.05f)) || (fpdetail_0.YYSBZ.Substring(8, 1) != "0"))
                {
                    if (fpdetail_0.SLV == 0.015f)
                    {
                        str3 = str3 + "J";
                    }
                    else
                    {
                        str3 = str3 + "H";
                    }
                }
                if (fpdetail_0.YYSBZ.Substring(8, 1) == "2")
                {
                    str3 = str3 + "C";
                }
                str = str + fpdetail_0.FPDM + str3 + "\x00a4";
                string str5 = fpdetail_0.XFMC.Replace("\n", "").Replace("\r", "");
                if (string.Empty.Equals(str5))
                {
                    str5 = " ";
                }
                str = str + str5 + "\x00a4";
                string gFMC = fpdetail_0.GFMC;
                if (gFMC.Trim().Length <= 0)
                {
                    gFMC = " ";
                }
                gFMC = gFMC.Replace("\n", "").Replace("\r", "");
                str = str + gFMC + "\x00a4";
                string mW = fpdetail_0.MW;
                str = str + mW + "\x00a4";
                string jYM = fpdetail_0.JYM;
                if (string.IsNullOrEmpty(jYM))
                {
                    jYM = " ";
                }
                str = str + jYM + "\x00a4";
                string jQBH = fpdetail_0.JQBH;
                if (string.IsNullOrEmpty(jQBH))
                {
                    jQBH = " ";
                }
                str = str + jQBH + "\x00a4";
                string gFDZDH = fpdetail_0.GFDZDH;
                if (string.IsNullOrEmpty(gFDZDH))
                {
                    gFDZDH = " ";
                }
                str = str + gFDZDH + "\x00a4";
                string gFYHZH = fpdetail_0.GFYHZH;
                if (string.IsNullOrEmpty(gFYHZH))
                {
                    gFYHZH = " ";
                }
                str = str + gFYHZH + "\x00a4";
                string xFDZDH = fpdetail_0.XFDZDH;
                if (string.IsNullOrEmpty(xFDZDH))
                {
                    xFDZDH = " ";
                }
                str = str + xFDZDH + "\x00a4";
                string xFYHZH = fpdetail_0.XFYHZH;
                if (string.IsNullOrEmpty(xFYHZH))
                {
                    xFYHZH = " ";
                }
                str = str + xFYHZH + "\x00a4";
                string sKR = fpdetail_0.SKR;
                string fHR = fpdetail_0.FHR;
                string kPR = fpdetail_0.KPR;
                if (string.IsNullOrEmpty(sKR))
                {
                    sKR = " ";
                }
                if (string.IsNullOrEmpty(fHR))
                {
                    fHR = " ";
                }
                if (string.IsNullOrEmpty(kPR))
                {
                    kPR = " ";
                }
                str = ((str + sKR + "\x00a4") + fHR + "\x00a4") + kPR + "\x00a4";
                string bZ = fpdetail_0.BZ;
                if (bZ.Contains(Environment.NewLine))
                {
                    bZ = bZ.Replace(Environment.NewLine, "￠\x00a7");
                }
                if (string.IsNullOrEmpty(bZ))
                {
                    bZ = " ";
                }
                str = str + bZ + "\x00a4";
                string sIGN = fpdetail_0.SIGN;
                str = str + sIGN + "\x00a4";
                DateTime zFRQ = fpdetail_0.ZFRQ;
                string str7 = null;
                str7 = string.Format("{0:yyyyMMddHHmmss}", zFRQ);
                if (!fpdetail_0.ZFBZ)
                {
                    str7 = " ";
                }
                str = str + str7 + "\x00a4";
                string str8 = Convert.ToString((decimal) (fpdetail_0.HJJE + fpdetail_0.HJSE));
                str = str + str8 + "\x00a4";
                if ((fpdetail_0.QDBZ && (fpdetail_0.QDList != null)) && (fpdetail_0.QDList.Count > 0))
                {
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, fpdetail_0.GoodsNum, " ", fpdetail_0.QDList.Count.ToString(), "\x00a4" });
                }
                else
                {
                    str = str + fpdetail_0.GoodsNum + "\x00a4";
                }
                string str11 = "";
                foreach (GoodsInfo info in fpdetail_0.GoodsList)
                {
                    str11 = str11 + this.method_15(info, fpdetail_0.BMBBBH);
                }
                if (str11.Length >= 1)
                {
                    str = str + str11.Remove(str11.Length - 1);
                }
                if ((fpdetail_0.QDBZ && (fpdetail_0.QDList != null)) && (fpdetail_0.QDList.Count > 0))
                {
                    str11 = "";
                    str11 = str11 + "\x00a4";
                    foreach (GoodsInfo info2 in fpdetail_0.QDList)
                    {
                        str11 = str11 + this.method_15(info2, fpdetail_0.BMBBBH);
                    }
                    if (str11.Length >= 1)
                    {
                        str = str + str11.Remove(str11.Length - 1);
                    }
                }
            }
            return str;
        }

        private string method_15(GoodsInfo goodsInfo_0, string string_2)
        {
            StringBuilder builder = new StringBuilder();
            if (goodsInfo_0 != null)
            {
                builder.Append(goodsInfo_0.RowNo + "\x00a4");
                if (this.bool_2 && (string_2.Trim() != ""))
                {
                    if (goodsInfo_0.SPBM.Trim() == "")
                    {
                        builder.Append(" \x00a4");
                    }
                    else
                    {
                        builder.Append(goodsInfo_0.SPBM.Trim() + "\x00a4");
                    }
                    if (goodsInfo_0.QYZBM.Trim() == "")
                    {
                        builder.Append(" \x00a4");
                    }
                    else
                    {
                        builder.Append(goodsInfo_0.QYZBM.Trim() + "\x00a4");
                    }
                    if (goodsInfo_0.SFYH.Trim() == "")
                    {
                        builder.Append(" \x00a4");
                    }
                    else
                    {
                        builder.Append(goodsInfo_0.SFYH.Trim() + "\x00a4");
                    }
                    if (goodsInfo_0.ZZSTSGL.Trim() == "")
                    {
                        builder.Append(" \x00a4");
                    }
                    else
                    {
                        builder.Append(goodsInfo_0.ZZSTSGL.Trim() + "\x00a4");
                    }
                    if (goodsInfo_0.LSLBS.Trim() == "")
                    {
                        builder.Append(" \x00a4");
                    }
                    else
                    {
                        builder.Append(goodsInfo_0.LSLBS.Trim() + "\x00a4");
                    }
                }
                builder.Append(goodsInfo_0.Name.Trim() + "\x00a4");
                builder.Append(goodsInfo_0.SpecMark.Trim() + " \x00a4");
                builder.Append(goodsInfo_0.Unit.Trim() + " \x00a4");
                builder.Append(goodsInfo_0.Num.ToString() + " \x00a4");
                builder.Append(goodsInfo_0.Price.ToString() + " \x00a4");
                builder.Append(goodsInfo_0.Amount.ToString() + "\x00a4");
                string str = "";
                if (goodsInfo_0.SLV == -1f)
                {
                    str = " ";
                }
                else if (goodsInfo_0.SLV == 0.015f)
                {
                    str = string.Format("{0,6:F3}", goodsInfo_0.SLV);
                }
                else
                {
                    str = string.Format("{0,6:F2}", goodsInfo_0.SLV);
                }
                builder.Append(str + " \x00a4");
                builder.Append(goodsInfo_0.Tax.ToString("F2") + "\x00a4");
                builder.Append(Convert.ToInt32(goodsInfo_0.TaxSign) + "\x00a4");
            }
            return builder.ToString();
        }

        private void method_16()
        {
            string str2;
            string str6;
            if (this.bsdata_0 != null)
            {
                decimal num3 = 0M;
                decimal num5 = 0M;
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("business");
                element.SetAttribute("version", this.bool_2 ? "2.0" : "1.0");
                element.SetAttribute("comment", "货物运输业增值税专用发票");
                element.SetAttribute("id", "HWYSYZZSZYFP");
                for (int i = 0; i < this.bsdata_0.FPDetailList.Count; i++)
                {
                    XmlElement element3;
                    bool flag;
                    FPDetail detail = this.bsdata_0.FPDetailList[i];
                    XmlElement element4 = document.CreateElement("body");
                    element4.SetAttribute("no", (i + 1).ToString());
                    if (this.bool_2 && (detail.BMBBBH != ""))
                    {
                        element3 = document.CreateElement("bmb_bbh");
                        element3.InnerText = detail.BMBBBH;
                        element4.AppendChild(element3);
                    }
                    element3 = document.CreateElement("fpdm");
                    element3.InnerText = detail.FPDM;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("fphm");
                    element3.InnerText = string.Format("{0:00000000}", detail.FPHM);
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("kprq");
                    element3.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("kpsj");
                    element3.InnerText = "";
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("jqbh");
                    element3.InnerText = detail.JQBH;
                    element4.AppendChild(element3);
                    decimal hJJE = detail.HJJE;
                    element3 = document.CreateElement("hjje");
                    element3.InnerText = hJJE.ToString("F2");
                    element4.AppendChild(element3);
                    if (!(flag = detail.ZFBZ))
                    {
                        num3 += hJJE;
                    }
                    element3 = document.CreateElement("slv");
                    element3.InnerText = (detail.SLV * 100f).ToString();
                    element4.AppendChild(element3);
                    decimal hJSE = detail.HJSE;
                    element3 = document.CreateElement("se");
                    element3.InnerText = hJSE.ToString("F2");
                    element4.AppendChild(element3);
                    if (!flag)
                    {
                        num5 += hJSE;
                    }
                    decimal num8 = hJJE + hJSE;
                    element3 = document.CreateElement("jshj");
                    element3.InnerText = num8.ToString("F2");
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("czch");
                    element3.InnerText = detail.QYD;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("ccdw");
                    element3.InnerText = detail.YYZZH;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("wspzhm");
                    element3.InnerText = detail.WSPZHM;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("skm");
                    element3.InnerText = detail.MW;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("cyrmc");
                    element3.InnerText = detail.XFMC;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("cyrsbh");
                    element3.InnerText = detail.XFSH;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("spfmc");
                    element3.InnerText = detail.GFMC;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("spfsbh");
                    element3.InnerText = detail.GFSH;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("shrmc");
                    element3.InnerText = detail.GFDZDH;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("shrsbh");
                    element3.InnerText = detail.CM;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("fhrmc");
                    element3.InnerText = detail.XFDZDH;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("fhrsbh");
                    element3.InnerText = detail.TYDH;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("qyd");
                    element3.InnerText = detail.XFYHZH;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("yshwxx");
                    element3.InnerText = detail.YSHWXX;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("bz");
                    element3.InnerText = detail.BZ;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("swjgmc");
                    element3.InnerText = detail.GFYHZH;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("swjgdm");
                    element3.InnerText = detail.HYBM;
                    element4.AppendChild(element3);
                    bool zFBZ = detail.ZFBZ;
                    int num9 = this.method_20(zFBZ, hJJE);
                    element3 = document.CreateElement("fpbz");
                    element3.InnerText = num9.ToString();
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("skr");
                    element3.InnerText = detail.SKR;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("fhr");
                    element3.InnerText = detail.FHR;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("kpr");
                    element3.InnerText = detail.KPR;
                    element4.AppendChild(element3);
                    string str5 = "";
                    if (hJJE < 0M)
                    {
                        string str7 = "";
                        string str8 = "";
                        element3 = document.CreateElement("yfpdm");
                        element3.InnerText = str7;
                        element4.AppendChild(element3);
                        element3 = document.CreateElement("yfphm");
                        element3.InnerText = str8;
                        element4.AppendChild(element3);
                        str5 = detail.HZTZDH.ToString();
                    }
                    if (zFBZ)
                    {
                        element3 = document.CreateElement("zfrq");
                        element3.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                        element4.AppendChild(element3);
                        element3 = document.CreateElement("zfsj");
                        element3.InnerText = "";
                        element4.AppendChild(element3);
                        element3 = document.CreateElement("zfr");
                        element3.InnerText = detail.KPR;
                        element4.AppendChild(element3);
                    }
                    element3 = document.CreateElement("tzdh");
                    element3.InnerText = str5;
                    element4.AppendChild(element3);
                    element3 = document.CreateElement("qmz");
                    element3.InnerText = detail.SIGN;
                    element4.AppendChild(element3);
                    if (detail.GoodsNum > 0)
                    {
                        for (int j = 0; j < detail.GoodsList.Count; j++)
                        {
                            element3 = document.CreateElement("zb");
                            XmlElement element2 = document.CreateElement("xh");
                            element2.InnerText = detail.GoodsList[j].FPMXXH;
                            element3.AppendChild(element2);
                            if (this.bool_2 && (detail.BMBBBH != ""))
                            {
                                element2 = document.CreateElement("spbm");
                                element2.InnerText = detail.GoodsList[j].SPBM;
                                element3.AppendChild(element2);
                                element2 = document.CreateElement("zxbm");
                                element2.InnerText = detail.GoodsList[j].QYZBM;
                                element3.AppendChild(element2);
                                element2 = document.CreateElement("yhzcbs");
                                element2.InnerText = detail.GoodsList[j].SFYH;
                                element3.AppendChild(element2);
                                element2 = document.CreateElement("lslbs");
                                element2.InnerText = detail.GoodsList[j].LSLBS;
                                element3.AppendChild(element2);
                                element2 = document.CreateElement("zzstsgl");
                                element2.InnerText = detail.GoodsList[j].ZZSTSGL;
                                element3.AppendChild(element2);
                            }
                            element2 = document.CreateElement("fyxm");
                            element2.InnerText = detail.GoodsList[j].Name;
                            element3.AppendChild(element2);
                            element2 = document.CreateElement("je");
                            element2.InnerText = detail.GoodsList[j].Amount.ToString("F2");
                            element3.AppendChild(element2);
                            element4.AppendChild(element3);
                        }
                    }
                    element.AppendChild(element4);
                }
                document.AppendChild(element);
                string str = "XTHTDATA";
                MemoryStream outStream = new MemoryStream();
                document.Save(outStream);
                byte[] bytes = ToolUtil.GetBytes(str);
                byte[] buffer = outStream.GetBuffer();
                str2 = IDEA_Ctypt.DataToCryp(bytes, buffer, true);
                outStream.Flush();
                outStream.Close();
                string str3 = "fpzl.txt";
                string invControlNum = base.TaxCardInstance.GetInvControlNum();
                if (this.bsdata_0.NSRID != null)
                {
                    str3 = "CGLPLBL_HH_" + this.bsdata_0.NSRID + "_" + invControlNum + "_" + this.bsdata_0.KPNY + "_0001_0001.DAT";
                    string text1 = "HH" + string.Format("{0, -20}", this.bsdata_0.NSRID) + string.Format("{0, -12}", invControlNum) + this.bsdata_0.KPNY + "0001";
                }
                str6 = Path.Combine(this.txtPath.Text, str3);
                try
                {
                    if (!File.Exists(str6))
                    {
                        goto Label_0ACC;
                    }
                    if (MessageManager.ShowMsgBox("INP-251208") == DialogResult.Yes)
                    {
                        this.bool_1 = true;
                        File.Delete(str6);
                        goto Label_0ACC;
                    }
                    this.bool_1 = false;
                }
                catch (Exception)
                {
                    MessageManager.ShowMsgBox("INP-251203", "错误", new string[] { str6 });
                }
            }
            return;
        Label_0ACC:
            try
            {
                using (StreamWriter writer = new StreamWriter(str6, false, ToolUtil.GetEncoding()))
                {
                    writer.Write(str2);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception)
            {
                MessageManager.ShowMsgBox("INP-251202");
            }
        }

        private void method_17()
        {
            string str2;
            string str7;
            if (this.bsdata_0 != null)
            {
                decimal num9 = 0M;
                decimal num8 = 0M;
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(newChild);
                XmlElement element3 = document.CreateElement("business");
                element3.SetAttribute("version", this.bool_2 ? "2.0" : "1.0");
                element3.SetAttribute("comment", "机动车销售统一发票");
                element3.SetAttribute("id", "JDCFP");
                for (int i = 0; i < this.bsdata_0.FPDetailList.Count; i++)
                {
                    XmlElement element2;
                    bool flag2;
                    FPDetail detail = this.bsdata_0.FPDetailList[i];
                    XmlElement element = document.CreateElement("body");
                    element.SetAttribute("no", (i + 1).ToString());
                    if (this.bool_2)
                    {
                        element2 = document.CreateElement("bmb_bbh");
                        element2.InnerText = detail.BMBBBH;
                        element.AppendChild(element2);
                    }
                    element2 = document.CreateElement("fpdm");
                    element2.InnerText = detail.FPDM;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("fphm");
                    element2.InnerText = string.Format("{0:00000000}", detail.FPHM);
                    element.AppendChild(element2);
                    element2 = document.CreateElement("jqbh");
                    element2.InnerText = detail.JQBH;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("kprq");
                    element2.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                    element.AppendChild(element2);
                    element2 = document.CreateElement("kpsj");
                    element2.InnerText = "";
                    element.AppendChild(element2);
                    element2 = document.CreateElement("skm");
                    element2.InnerText = detail.MW;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("ghdw");
                    element2.InnerText = detail.GFMC;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("scqymc");
                    element2.InnerText = detail.SCCJMC;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("sfzhm");
                    element2.InnerText = detail.XSBM;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("gfsbh");
                    element2.InnerText = detail.GFSH;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("xhdwmc");
                    element2.InnerText = detail.XFMC;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("nsrsbh");
                    element2.InnerText = detail.XFSH;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("dz");
                    element2.InnerText = detail.XFDZDH;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("dh");
                    element2.InnerText = detail.XFDH;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("khyh");
                    element2.InnerText = detail.XFYHZH;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("zh");
                    element2.InnerText = detail.KHYHZH;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("cpxh");
                    element2.InnerText = detail.XFDZ;
                    element.AppendChild(element2);
                    if (this.bool_2)
                    {
                        element2 = document.CreateElement("spbm");
                        element2.InnerText = detail.SPBM;
                        element.AppendChild(element2);
                        element2 = document.CreateElement("zxbm");
                        element2.InnerText = detail.QYZBM;
                        element.AppendChild(element2);
                        element2 = document.CreateElement("yhzcbs");
                        element2.InnerText = detail.SFYH;
                        element.AppendChild(element2);
                        element2 = document.CreateElement("lslbs");
                        element2.InnerText = detail.LSLBS;
                        element.AppendChild(element2);
                        element2 = document.CreateElement("zzstsgl");
                        element2.InnerText = detail.ZZSTSGL;
                        element.AppendChild(element2);
                    }
                    element2 = document.CreateElement("cllx");
                    element2.InnerText = detail.GFDZDH;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("hgzs");
                    element2.InnerText = detail.CM;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("jkzmsh");
                    element2.InnerText = detail.TYDH;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("cd");
                    element2.InnerText = detail.KHYHMC;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("sjdh");
                    element2.InnerText = detail.QYD;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("fdjhm");
                    element2.InnerText = detail.ZHD;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("cjhm");
                    element2.InnerText = detail.XHD;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("cjfy");
                    element2.InnerText = "";
                    element.AppendChild(element2);
                    element2 = document.CreateElement("zzssl");
                    element2.InnerText = (detail.SLV * 100f).ToString();
                    element.AppendChild(element2);
                    decimal hJSE = detail.HJSE;
                    element2 = document.CreateElement("zzsse");
                    element2.InnerText = hJSE.ToString("F2");
                    element.AppendChild(element2);
                    decimal hJJE = detail.HJJE;
                    if (!(flag2 = detail.ZFBZ))
                    {
                        num9 += hJJE;
                    }
                    if (!flag2)
                    {
                        num8 += hJSE;
                    }
                    decimal num5 = hJJE + hJSE;
                    element2 = document.CreateElement("jshj");
                    element2.InnerText = num5.ToString("F2");
                    element.AppendChild(element2);
                    element2 = document.CreateElement("kpr");
                    element2.InnerText = detail.KPR;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("dw");
                    element2.InnerText = detail.YYZZH;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("xcrs");
                    element2.InnerText = detail.MDD;
                    element.AppendChild(element2);
                    bool zFBZ = detail.ZFBZ;
                    int num6 = this.method_20(zFBZ, hJJE);
                    element2 = document.CreateElement("fpztbz");
                    element2.InnerText = num6.ToString();
                    element.AppendChild(element2);
                    element2 = document.CreateElement("swjg_dm");
                    element2.InnerText = detail.HYBM;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("swjg_mc");
                    element2.InnerText = detail.GFYHZH;
                    element.AppendChild(element2);
                    if (hJJE < 0M)
                    {
                        string str3 = "";
                        string str4 = "";
                        string str5 = detail.LZDMHM.ToString();
                        if (((str5.Length > 0) && str5.Contains("_")) && (str5.Split(new char[] { '_' }).Length == 2))
                        {
                            str3 = str5.Split(new char[] { '_' })[0];
                            str4 = str5.Split(new char[] { '_' })[1];
                        }
                        element2 = document.CreateElement("yfpdm");
                        element2.InnerText = str3;
                        element.AppendChild(element2);
                        element2 = document.CreateElement("yfphm");
                        element2.InnerText = str4;
                        element.AppendChild(element2);
                    }
                    if (zFBZ)
                    {
                        element2 = document.CreateElement("zfrq");
                        element2.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                        element.AppendChild(element2);
                        element2 = document.CreateElement("zfsj");
                        element2.InnerText = "";
                        element.AppendChild(element2);
                        element2 = document.CreateElement("zfr");
                        element2.InnerText = detail.KPR;
                        element.AppendChild(element2);
                    }
                    element2 = document.CreateElement("wspzhm");
                    element2.InnerText = detail.WSPZHM;
                    element.AppendChild(element2);
                    element2 = document.CreateElement("qmz");
                    element2.InnerText = detail.SIGN;
                    element.AppendChild(element2);
                    element3.AppendChild(element);
                }
                document.AppendChild(element3);
                string str6 = "XTHTDATA";
                MemoryStream outStream = new MemoryStream();
                document.Save(outStream);
                byte[] bytes = ToolUtil.GetBytes(str6);
                byte[] buffer = outStream.GetBuffer();
                str7 = IDEA_Ctypt.DataToCryp(bytes, buffer, true);
                outStream.Flush();
                outStream.Close();
                string str = "fpzl.txt";
                string invControlNum = base.TaxCardInstance.GetInvControlNum();
                if (this.bsdata_0.NSRID != null)
                {
                    str = "CGLPLBL_HH_" + this.bsdata_0.NSRID + "_" + invControlNum + "_" + this.bsdata_0.KPNY + "_0001_0002.DAT";
                    string text1 = "HH" + string.Format("{0, -20}", this.bsdata_0.NSRID) + string.Format("{0, -12}", invControlNum) + this.bsdata_0.KPNY + "0001";
                }
                str2 = Path.Combine(this.txtPath.Text, str);
                try
                {
                    if (!File.Exists(str2))
                    {
                        goto Label_0A22;
                    }
                    if (MessageManager.ShowMsgBox("INP-251208") == DialogResult.Yes)
                    {
                        this.bool_1 = true;
                        File.Delete(str2);
                        goto Label_0A22;
                    }
                    this.bool_1 = false;
                }
                catch (Exception)
                {
                    MessageManager.ShowMsgBox("INP-251203", "错误", new string[] { str2 });
                }
            }
            return;
        Label_0A22:
            try
            {
                using (StreamWriter writer = new StreamWriter(str2, false, ToolUtil.GetEncoding()))
                {
                    writer.Write(str7);
                }
            }
            catch (Exception)
            {
                MessageManager.ShowMsgBox("INP-251202");
            }
        }

        private void method_18()
        {
            string str5;
            string str8;
            if (this.bsdata_0 != null)
            {
                decimal num = 0M;
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("business");
                element.SetAttribute("id", "DZFP");
                element.SetAttribute("comment", "电子增值税普通发票");
                element.SetAttribute("version", this.bool_2 ? "2.0" : "1.0");
                for (int i = 0; i < this.bsdata_0.FPDetailList.Count; i++)
                {
                    XmlElement element2;
                    FPDetail detail = this.bsdata_0.FPDetailList[i];
                    XmlElement element3 = document.CreateElement("body");
                    element3.SetAttribute("no", (i + 1).ToString());
                    if (this.bool_2)
                    {
                        element2 = document.CreateElement("bmb_bbh");
                        element2.InnerText = detail.BMBBBH;
                        element3.AppendChild(element2);
                    }
                    element2 = document.CreateElement("fpdm");
                    element2.InnerText = detail.FPDM;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("fphm");
                    element2.InnerText = string.Format("{0:00000000}", detail.FPHM);
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("kprq");
                    element2.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("kpsj");
                    element2.InnerText = detail.KPRQ.ToString("HH:mm:ss");
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("sbbh");
                    element2.InnerText = detail.JQBH;
                    element3.AppendChild(element2);
                    decimal hJJE = detail.HJJE;
                    bool zFBZ = detail.ZFBZ;
                    bool flag2 = detail.ZFBZ;
                    decimal hJSE = detail.HJSE;
                    int num10 = this.method_20(flag2, hJJE);
                    element2 = document.CreateElement("fpztbz");
                    element2.InnerText = num10.ToString();
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("xsf_nsrsbh");
                    element2.InnerText = detail.XFSH;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("xsf_mc");
                    element2.InnerText = detail.XFMC;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("xsf_dzdh");
                    element2.InnerText = detail.XFDZDH;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("xsf_yhzh");
                    element2.InnerText = detail.XFYHZH;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("gmf_nsrsbh");
                    element2.InnerText = detail.GFSH;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("gmf_mc");
                    element2.InnerText = detail.GFMC;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("gmf_dzdh");
                    element2.InnerText = detail.GFDZDH;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("gmf_yhzh");
                    element2.InnerText = detail.GFYHZH;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("zfrq");
                    if (detail.ZFRQ == DateTime.MinValue)
                    {
                        element2.InnerText = "";
                    }
                    else
                    {
                        element2.InnerText = detail.ZFRQ.ToString("yyyy-MM-dd");
                    }
                    element3.AppendChild(element2);
                    if (!zFBZ)
                    {
                        num += hJSE;
                    }
                    decimal num9 = hJJE + hJSE;
                    element2 = document.CreateElement("jshj");
                    element2.InnerText = num9.ToString("F2");
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("hjje");
                    element2.InnerText = hJJE.ToString("F2");
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("hjse");
                    element2.InnerText = hJSE.ToString("F2");
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("bz");
                    element2.InnerText = detail.BZ;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("kpr");
                    element2.InnerText = detail.KPR;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("skr");
                    element2.InnerText = detail.SKR;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("fhr");
                    element2.InnerText = detail.FHR;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("yfpdm");
                    string str2 = "";
                    string str3 = "";
                    string str = detail.LZDMHM.ToString();
                    if (((str.Length > 0) && str.Contains("_")) && (str.Split(new char[] { '_' }).Length == 2))
                    {
                        str2 = str.Split(new char[] { '_' })[0];
                        str3 = str.Split(new char[] { '_' })[1];
                    }
                    element2.InnerText = str2;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("yfphm");
                    element2.InnerText = str3;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("skm");
                    element2.InnerText = detail.MW;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("jym");
                    element2.InnerText = detail.JYM;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("fpqm");
                    element2.InnerText = detail.SIGN;
                    element3.AppendChild(element2);
                    if (detail.GoodsNum > 0)
                    {
                        for (int j = 0; j < detail.GoodsList.Count; j++)
                        {
                            element2 = document.CreateElement("zb");
                            XmlElement element4 = document.CreateElement("xh");
                            element4.InnerText = detail.GoodsList[j].FPMXXH;
                            element2.AppendChild(element4);
                            if (this.bool_2)
                            {
                                element4 = document.CreateElement("spbm");
                                element4.InnerText = detail.GoodsList[j].SPBM;
                                element2.AppendChild(element4);
                                element4 = document.CreateElement("zxbm");
                                element4.InnerText = detail.GoodsList[j].QYZBM;
                                element2.AppendChild(element4);
                                element4 = document.CreateElement("yhzcbs");
                                element4.InnerText = detail.GoodsList[j].SFYH;
                                element2.AppendChild(element4);
                                element4 = document.CreateElement("zzstsgl");
                                element4.InnerText = detail.GoodsList[j].ZZSTSGL;
                                element2.AppendChild(element4);
                                element4 = document.CreateElement("lslbs");
                                element4.InnerText = detail.GoodsList[j].LSLBS;
                                element2.AppendChild(element4);
                            }
                            element4 = document.CreateElement("xmmc");
                            element4.InnerText = detail.GoodsList[j].Name;
                            element2.AppendChild(element4);
                            element4 = document.CreateElement("dw");
                            element4.InnerText = detail.GoodsList[j].Unit;
                            element2.AppendChild(element4);
                            element4 = document.CreateElement("ggxh");
                            element4.InnerText = detail.GoodsList[j].SpecMark;
                            element2.AppendChild(element4);
                            element4 = document.CreateElement("xmsl");
                            element4.InnerText = detail.GoodsList[j].Num;
                            element2.AppendChild(element4);
                            element4 = document.CreateElement("xmdj");
                            element4.InnerText = detail.GoodsList[j].Price;
                            element2.AppendChild(element4);
                            element4 = document.CreateElement("je");
                            element4.InnerText = detail.GoodsList[j].Amount.ToString("F2");
                            element2.AppendChild(element4);
                            element4 = document.CreateElement("sl");
                            element4.InnerText = detail.GoodsList[j].SLV.ToString("F2");
                            element2.AppendChild(element4);
                            element4 = document.CreateElement("se");
                            element4.InnerText = detail.GoodsList[j].Tax.ToString("F2");
                            element2.AppendChild(element4);
                            element3.AppendChild(element2);
                        }
                    }
                    element.AppendChild(element3);
                }
                document.AppendChild(element);
                string str4 = "XTHTDATA";
                MemoryStream outStream = new MemoryStream();
                document.Save(outStream);
                byte[] bytes = ToolUtil.GetBytes(str4);
                byte[] buffer = outStream.GetBuffer();
                str5 = IDEA_Ctypt.DataToCryp(bytes, buffer, true);
                outStream.Flush();
                outStream.Close();
                string str6 = "fpzl.txt";
                string invControlNum = base.TaxCardInstance.GetInvControlNum();
                if (this.bsdata_0.NSRID != null)
                {
                    str6 = "CGLPLBL_HH_" + this.bsdata_0.NSRID + "_" + invControlNum + "_" + this.bsdata_0.KPNY + "_0001_0003.DAT";
                    string text1 = "HH" + string.Format("{0, -20}", this.bsdata_0.NSRID) + string.Format("{0, -12}", invControlNum) + this.bsdata_0.KPNY + "0001";
                }
                str8 = Path.Combine(this.txtPath.Text, str6);
                try
                {
                    if (!File.Exists(str8))
                    {
                        goto Label_0AD9;
                    }
                    if (MessageManager.ShowMsgBox("INP-251208") == DialogResult.Yes)
                    {
                        this.bool_1 = true;
                        File.Delete(str8);
                        goto Label_0AD9;
                    }
                    this.bool_1 = false;
                }
                catch (Exception)
                {
                    MessageManager.ShowMsgBox("INP-251203", "错误", new string[] { str8 });
                }
            }
            return;
        Label_0AD9:
            try
            {
                using (StreamWriter writer = new StreamWriter(str8, false, ToolUtil.GetEncoding()))
                {
                    writer.Write(str5);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception)
            {
                MessageManager.ShowMsgBox("INP-251202");
            }
        }

        private void method_19()
        {
            string str10;
            string str11;
            if (this.bsdata_0 != null)
            {
                decimal num6 = 0M;
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(newChild);
                XmlElement element4 = document.CreateElement("business");
                element4.SetAttribute("id", "JSZZP");
                element4.SetAttribute("comment", "卷式增普票");
                element4.SetAttribute("version", "1.0");
                for (int i = 0; i < this.bsdata_0.FPDetailList.Count; i++)
                {
                    XmlElement element2;
                    FPDetail detail = this.bsdata_0.FPDetailList[i];
                    XmlElement element3 = document.CreateElement("body");
                    element3.SetAttribute("no", (i + 1).ToString());
                    if (this.bool_2)
                    {
                        element2 = document.CreateElement("bmb_bbh");
                        element2.InnerText = detail.BMBBBH;
                        element3.AppendChild(element2);
                    }
                    element2 = document.CreateElement("fpdm");
                    element2.InnerText = detail.FPDM;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("fphm");
                    element2.InnerText = string.Format("{0:00000000}", detail.FPHM);
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("kprq");
                    element2.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("kpsj");
                    element2.InnerText = detail.KPRQ.ToString("HH:mm:ss");
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("sbbh");
                    element2.InnerText = detail.JQBH;
                    element3.AppendChild(element2);
                    decimal hJJE = detail.HJJE;
                    bool zFBZ = detail.ZFBZ;
                    bool flag2 = detail.ZFBZ;
                    decimal hJSE = detail.HJSE;
                    element2 = document.CreateElement("hjje");
                    element2.InnerText = hJJE.ToString("F2");
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("se");
                    element2.InnerText = hJSE.ToString("F2");
                    element3.AppendChild(element2);
                    decimal num10 = hJJE + hJSE;
                    element2 = document.CreateElement("jshj");
                    element2.InnerText = num10.ToString("F2");
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("jym");
                    element2.InnerText = detail.JYM;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("xfmc");
                    element2.InnerText = detail.XFMC;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("xfsh");
                    element2.InnerText = detail.XFSH;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("gfmc");
                    element2.InnerText = detail.GFMC;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("gfsh");
                    element2.InnerText = detail.GFSH;
                    element3.AppendChild(element2);
                    int num11 = this.method_20(flag2, hJJE);
                    element2 = document.CreateElement("fpbz");
                    element2.InnerText = num11.ToString();
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("skr");
                    element2.InnerText = detail.SKR;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("bz");
                    element2.InnerText = detail.BZ;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("yfpdm");
                    string str4 = "";
                    string str5 = "";
                    string str3 = detail.LZDMHM.ToString();
                    if (((str3.Length > 0) && str3.Contains("_")) && (str3.Split(new char[] { '_' }).Length == 2))
                    {
                        str4 = str3.Split(new char[] { '_' })[0];
                        str5 = str3.Split(new char[] { '_' })[1];
                    }
                    element2.InnerText = str4;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("yfphm");
                    element2.InnerText = str5;
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("zfrq");
                    if (detail.ZFRQ == DateTime.MinValue)
                    {
                        element2.InnerText = "";
                    }
                    else
                    {
                        element2.InnerText = detail.ZFRQ.ToString("yyyy-MM-dd");
                    }
                    element3.AppendChild(element2);
                    element2 = document.CreateElement("zfsj");
                    if (detail.ZFRQ == DateTime.MinValue)
                    {
                        element2.InnerText = "";
                    }
                    else
                    {
                        element2.InnerText = detail.ZFRQ.ToString("HH:mm:ss");
                    }
                    element3.AppendChild(element2);
                    if (!zFBZ)
                    {
                        num6 += hJSE;
                    }
                    element2 = document.CreateElement("qmz");
                    element2.InnerText = detail.SIGN;
                    element3.AppendChild(element2);
                    if (detail.GoodsNum > 0)
                    {
                        GetFpInfoDal dal = new GetFpInfoDal();
                        string str8 = string.Concat(new object[] { detail.FPDM, ",", detail.FPHM, ",", detail.FPType });
                        List<Fpxx> list = dal.GetFpInfo("1", "1", UpdateTransMethod.BszAndWbs, "", "", "", str8);
                        for (int j = 0; j < list[0].Mxxx.Count; j++)
                        {
                            string str = list[0].Mxxx[j][SPXX.DJ];
                            string str2 = list[0].Get_Print_Dj(list[0].Mxxx[j], 1, null);
                            double result = 0.0;
                            double.TryParse(list[0].Mxxx[j][SPXX.JE], out result);
                            double num3 = 0.0;
                            double.TryParse(list[0].Mxxx[j][SPXX.SE], out num3);
                            element2 = document.CreateElement("zb");
                            XmlElement element = document.CreateElement("xh");
                            element.InnerText = list[0].Mxxx[j][SPXX.XH];
                            element2.AppendChild(element);
                            element = document.CreateElement("xm");
                            element.InnerText = list[0].Mxxx[j][SPXX.SPMC];
                            element2.AppendChild(element);
                            element = document.CreateElement("sl");
                            element.InnerText = list[0].Mxxx[j][SPXX.SL];
                            element2.AppendChild(element);
                            element = document.CreateElement("hsdj");
                            if (list[0].Mxxx[j][SPXX.HSJBZ].Equals("1"))
                            {
                                element.InnerText = str;
                            }
                            else
                            {
                                element.InnerText = str2;
                            }
                            element2.AppendChild(element);
                            element = document.CreateElement("hsje");
                            element.InnerText = (result + num3).ToString("F2");
                            element2.AppendChild(element);
                            element = document.CreateElement("dj");
                            if (list[0].Mxxx[j][SPXX.HSJBZ].Equals("0"))
                            {
                                element.InnerText = str;
                            }
                            else
                            {
                                element.InnerText = str2;
                            }
                            element2.AppendChild(element);
                            element = document.CreateElement("je");
                            element.InnerText = result.ToString("F2");
                            element2.AppendChild(element);
                            element = document.CreateElement("zsl");
                            element.InnerText = list[0].Mxxx[j][SPXX.SLV];
                            element2.AppendChild(element);
                            element = document.CreateElement("se");
                            element.InnerText = num3.ToString("F2");
                            element2.AppendChild(element);
                            if (this.bool_2)
                            {
                                element = document.CreateElement("spbm");
                                element.InnerText = detail.GoodsList[j].SPBM;
                                element2.AppendChild(element);
                                element = document.CreateElement("zxbm");
                                element.InnerText = detail.GoodsList[j].QYZBM;
                                element2.AppendChild(element);
                                element = document.CreateElement("yhzcbs");
                                element.InnerText = detail.GoodsList[j].SFYH;
                                element2.AppendChild(element);
                                element = document.CreateElement("lslbs");
                                element.InnerText = detail.GoodsList[j].LSLBS;
                                element2.AppendChild(element);
                                element = document.CreateElement("zzstsgl");
                                element.InnerText = detail.GoodsList[j].ZZSTSGL;
                                element2.AppendChild(element);
                            }
                            element3.AppendChild(element2);
                        }
                    }
                    element4.AppendChild(element3);
                }
                document.AppendChild(element4);
                string str9 = "XTHTDATA";
                MemoryStream outStream = new MemoryStream();
                document.Save(outStream);
                byte[] bytes = ToolUtil.GetBytes(str9);
                byte[] buffer = outStream.GetBuffer();
                str10 = IDEA_Ctypt.DataToCryp(bytes, buffer, true);
                outStream.Flush();
                outStream.Close();
                string str7 = "fpzl.txt";
                string invControlNum = base.TaxCardInstance.GetInvControlNum();
                if (this.bsdata_0.NSRID != null)
                {
                    str7 = "CGLPLBL_HH_" + this.bsdata_0.NSRID + "_" + invControlNum + "_" + this.bsdata_0.KPNY + "_0001_0004.DAT";
                    string text1 = "HH" + string.Format("{0, -20}", this.bsdata_0.NSRID) + string.Format("{0, -12}", invControlNum) + this.bsdata_0.KPNY + "0001";
                }
                str11 = Path.Combine(this.txtPath.Text, str7);
                try
                {
                    if (!File.Exists(str11))
                    {
                        goto Label_0B46;
                    }
                    if (MessageManager.ShowMsgBox("INP-251208") == DialogResult.Yes)
                    {
                        this.bool_1 = true;
                        File.Delete(str11);
                        goto Label_0B46;
                    }
                    this.bool_1 = false;
                }
                catch (Exception)
                {
                    MessageManager.ShowMsgBox("INP-251203", "错误", new string[] { str11 });
                }
            }
            return;
        Label_0B46:
            try
            {
                using (StreamWriter writer = new StreamWriter(str11, false, ToolUtil.GetEncoding()))
                {
                    writer.Write(str10);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception)
            {
                MessageManager.ShowMsgBox("INP-251202");
            }
        }

        private int method_20(bool bool_3, decimal decimal_0)
        {
            if (decimal_0 > 0M)
            {
                if (bool_3)
                {
                    return 3;
                }
                return 0;
            }
            if (decimal_0 < 0M)
            {
                if (bool_3)
                {
                    return 4;
                }
                return 1;
            }
            if ((Convert.ToDouble(Math.Abs(decimal_0)) < 1E-08) && bool_3)
            {
                return 2;
            }
            return 0;
        }

        private void method_21()
        {
            int num5;
            int num6;
            decimal d = 0M;
            decimal num2 = 0M;
            if (this.bsdata_0 != null)
            {
                foreach (FPDetail detail in this.bsdata_0.FPDetailList)
                {
                    if (!detail.ZFBZ)
                    {
                        d += detail.HJJE;
                        num2 += detail.HJSE;
                    }
                }
            }
            decimal num4 = 0M;
            decimal num3 = 0M;
            string str2 = this.cmbMonth.SelectedValue.ToString();
            int.TryParse(str2.Substring(0, 4), out num5);
            int.TryParse(str2.Substring(4, 2), out num6);
            try
            {
                TaxStatisData data = base.TaxCardInstance.GetMonthStatistics(num5, num6, 0);
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
                {
                    InvAmountTaxStati stati2 = data.InvTypeStatData(0);
                    num4 = Convert.ToDecimal(stati2.Total.SJXSJE);
                    num3 = Convert.ToDecimal(stati2.Total.SJXXSE);
                    stati2 = data.InvTypeStatData(2);
                    num4 += Convert.ToDecimal(stati2.Total.SJXSJE);
                    num3 += Convert.ToDecimal(stati2.Total.SJXXSE);
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票"))
                {
                    InvAmountTaxStati stati5 = data.InvTypeStatData(11);
                    num4 = Convert.ToDecimal(stati5.Total.SJXSJE);
                    num3 = Convert.ToDecimal(stati5.Total.SJXXSE);
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
                {
                    InvAmountTaxStati stati4 = data.InvTypeStatData(12);
                    num4 = Convert.ToDecimal(stati4.Total.SJXSJE);
                    num3 = Convert.ToDecimal(stati4.Total.SJXXSE);
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("电子增值税普通发票"))
                {
                    InvAmountTaxStati stati3 = data.InvTypeStatData(0x33);
                    num4 = Convert.ToDecimal(stati3.Total.SJXSJE);
                    num3 = Convert.ToDecimal(stati3.Total.SJXXSE);
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税普通发票(卷票)"))
                {
                    InvAmountTaxStati stati = data.InvTypeStatData(0x29);
                    num4 = Convert.ToDecimal(stati.Total.SJXSJE);
                    num3 = Convert.ToDecimal(stati.Total.SJXXSE);
                }
            }
            catch (Exception)
            {
                if (base.TaxCardInstance.RetCode > 0)
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.ErrCode);
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-111001", "错误", new string[] { "获取金税设备中统计金额税额异常！" });
                }
                return;
            }
            d = decimal.Round(d, 2);
            num2 = decimal.Round(num2, 2);
            num4 = decimal.Round(num4, 2);
            num3 = decimal.Round(num3, 2);
            if ((d != num4) || (num2 != num3))
            {
                string str = "";
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
                {
                    str = "增值税专普票";
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票"))
                {
                    str = "货物运输业增值税专用发票";
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
                {
                    str = "机动车销售统一发票";
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("电子增值税普通发票"))
                {
                    str = "电子增值税普通发票";
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税普通发票(卷票)"))
                {
                    str = "增值税普通发票(卷票)";
                }
                MessageManager.ShowMsgBox("INP-251209", "提示", new string[] { str, num4.ToString("C"), d.ToString("C"), num3.ToString("C"), num2.ToString("C") });
                MessageManager.ShowMsgBox("INP-251210");
            }
            else
            {
                MessageManager.ShowMsgBox("INP-251210");
            }
        }

        private void method_3()
        {
            this.list_0 = this.commFun_0.GetInvTypeCollect();
            foreach (InvTypeEntity entity in this.list_0)
            {
                if (((entity.m_invType == INV_TYPE.INV_SPECIAL) || (entity.m_invType == INV_TYPE.INV_COMMON)) && !this.cmbPiaoZhong.Items.Contains("增值税专普票"))
                {
                    this.cmbPiaoZhong.Items.Add("增值税专普票");
                }
                if (entity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                {
                    this.cmbPiaoZhong.Items.Add("货物运输业增值税专用发票");
                }
                if (entity.m_invType == INV_TYPE.INV_VEHICLESALES)
                {
                    this.cmbPiaoZhong.Items.Add("机动车销售统一发票");
                }
                if (entity.m_invType == INV_TYPE.INV_PTFPDZ)
                {
                    this.cmbPiaoZhong.Items.Add("电子增值税普通发票");
                }
                if (entity.m_invType == INV_TYPE.INV_JSFP)
                {
                    this.cmbPiaoZhong.Items.Add("增值税普通发票(卷票)");
                }
            }
            if (this.cmbPiaoZhong.Items.Count > 0)
            {
                this.cmbPiaoZhong.SelectedIndex = 0;
            }
        }

        private void method_4()
        {
            this.ilog_0.Debug("进入 FillCmbMonth_");
            List<IdTextPair> list = new List<IdTextPair>();
            DateTime taxClock = base.TaxCardInstance.TaxClock;
            DateTime time = this.method_5();
            try
            {
                for (DateTime time2 = new DateTime(base.TaxCardInstance.SysYear, base.TaxCardInstance.SysMonth, 1); DateTime.Compare(time, time2) <= 0; time2 = time2.AddMonths(-1))
                {
                    int num = int.Parse(time2.ToString("yyyyMM"));
                    string str = time2.ToString("yyyy年MM月");
                    list.Add(new IdTextPair(num, str));
                }
                this.cmbMonth.DataSource = list;
                this.cmbMonth.DisplayMember = "Text";
                this.cmbMonth.ValueMember = "Id";
            }
            catch (Exception)
            {
            }
        }

        private DateTime method_5()
        {
            this.ilog_0.Debug("进入 getInitDate");
            DateTime time2 = base.TaxCardInstance.TaxClock.AddMonths(-12);
            this.ilog_0.Debug("initDate:" + time2.ToString());
            string cardEffectDate = base.TaxCardInstance.CardEffectDate;
            this.ilog_0.Debug("yearMonth:" + cardEffectDate);
            if (!string.IsNullOrEmpty(cardEffectDate))
            {
                int year = int.Parse(cardEffectDate.Substring(0, 4));
                int month = int.Parse(cardEffectDate.Substring(4, 2));
                this.ilog_0.Debug(string.Concat(new object[] { "year:", year, "   month:", month }));
                time2 = new DateTime(year, month, 1);
            }
            this.ilog_0.Debug("初始化 FPDetailDAL");
            FPDetailDAL ldal = new FPDetailDAL();
            this.ilog_0.Debug("初始化 FPDetailDAL结束");
            object qiShiDate = ldal.GetQiShiDate();
            this.ilog_0.Debug("qsrq:" + qiShiDate);
            if (qiShiDate != null)
            {
                DateTime time3 = (DateTime) qiShiDate;
                DateTime time4 = new DateTime(time3.Year, time3.Month, 1);
                if (DateTime.Compare(time2, time4) >= 0)
                {
                    return time4;
                }
            }
            return time2;
        }

        private bool method_6()
        {
            string driveName = "";
            try
            {
                if (!Directory.Exists(this.txtPath.Text))
                {
                    MessageManager.ShowMsgBox("INP-251207");
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            if (this.txtPath.Text.Length < 3)
            {
                MessageManager.ShowMsgBox("INP-251207");
                return false;
            }
            driveName = this.txtPath.Text.Substring(0, 3);
            try
            {
                DriveInfo info = new DriveInfo(driveName);
                if (info.AvailableFreeSpace < 0x989680L)
                {
                    MessageManager.ShowMsgBox("INP-251201");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageManager.ShowMsgBox("INP-251205");
                return false;
            }
            return true;
        }

        private void method_7()
        {
            try
            {
                this.bsdata_0 = new BSData();
                if (base.TaxCardInstance.TaxCode != null)
                {
                    this.bsdata_0.SWJGDM = base.TaxCardInstance.TaxCode.Substring(0, 6);
                }
                this.bsdata_0.KPNY = this.method_8();
                this.bsdata_0.KPJH = base.TaxCardInstance.Machine;
                this.bsdata_0.NSRName = base.TaxCardInstance.Corporation;
                this.bsdata_0.NSRID = base.TaxCardInstance.TaxCode;
                this.bsdata_0.Address = base.TaxCardInstance.Address;
                this.bsdata_0.Phone = base.TaxCardInstance.Telephone;
                this.bsdata_0.FPLB = FPLB.V61;
                FPDetailDAL ldal = new FPDetailDAL();
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
                {
                    this.bsdata_0.FPDetailList.AddRange(ldal.GetFPDetailListByFPZL_((int) this.cmbMonth.SelectedValue, base.TaxCardInstance.TaxCode, "s"));
                    this.bsdata_0.FPDetailList.AddRange(ldal.GetFPDetailListByFPZL_((int) this.cmbMonth.SelectedValue, base.TaxCardInstance.TaxCode, "c"));
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票"))
                {
                    this.bsdata_0.FPDetailList.AddRange(ldal.GetFPDetailListByFPZL_((int) this.cmbMonth.SelectedValue, base.TaxCardInstance.TaxCode, "f"));
                    List<FPDetail> list = new List<FPDetail>();
                    foreach (FPDetail detail in this.bsdata_0.FPDetailList)
                    {
                        if (detail != null)
                        {
                            detail.GoodsList.AddRange(ldal.GetGoodsList_(detail.FPType.ToString(), detail.FPDM, detail.FPHM));
                        }
                    }
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
                {
                    this.bsdata_0.FPDetailList.AddRange(ldal.GetFPDetailListByFPZL_((int) this.cmbMonth.SelectedValue, base.TaxCardInstance.TaxCode, "j"));
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("电子增值税普通发票"))
                {
                    this.bsdata_0.FPDetailList.AddRange(ldal.GetFPDetailListByFPZL_((int) this.cmbMonth.SelectedValue, base.TaxCardInstance.TaxCode, "p"));
                    List<FPDetail> list2 = new List<FPDetail>();
                    foreach (FPDetail detail2 in this.bsdata_0.FPDetailList)
                    {
                        if (detail2 != null)
                        {
                            detail2.GoodsList.AddRange(ldal.GetGoodsList_(detail2.FPType.ToString(), detail2.FPDM, detail2.FPHM));
                        }
                    }
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税普通发票(卷票)"))
                {
                    this.bsdata_0.FPDetailList.AddRange(ldal.GetFPDetailListByFPZL_((int) this.cmbMonth.SelectedValue, base.TaxCardInstance.TaxCode, "q"));
                    List<FPDetail> list3 = new List<FPDetail>();
                    foreach (FPDetail detail3 in this.bsdata_0.FPDetailList)
                    {
                        if (detail3 != null)
                        {
                            detail3.GoodsList.AddRange(ldal.GetGoodsList_(detail3.FPType.ToString(), detail3.FPDM, detail3.FPHM));
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private string method_8()
        {
            object selectedValue = this.cmbMonth.SelectedValue;
            if (selectedValue != null)
            {
                string str = selectedValue.ToString();
                if (str.Length == 6)
                {
                    if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
                    {
                        return str.Substring(2);
                    }
                    if ((this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票") || this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票")) || (this.cmbPiaoZhong.SelectedItem.Equals("电子增值税普通发票") || this.cmbPiaoZhong.SelectedItem.Equals("增值税普通发票(卷票)")))
                    {
                        return str;
                    }
                }
            }
            if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
            {
                return DateTime.Today.ToString("yyyyMMdd").Substring(2, 4);
            }
            return DateTime.Today.ToString("yyyyMMdd");
        }

        private void method_9()
        {
            if (this.bsdata_0 != null)
            {
                FileStream stream;
                string str2 = "fpzl.txt";
                if (this.bsdata_0.NSRID != null)
                {
                    string nSRID = this.bsdata_0.NSRID;
                    if (nSRID.Length < 7)
                    {
                        nSRID = nSRID.PadRight(7, '0');
                    }
                    str2 = "X" + nSRID.Substring(nSRID.Length - 7, 7) + "." + this.bsdata_0.KPNY.Substring(2, 2);
                }
                string path = Path.Combine(this.txtPath.Text, str2);
                try
                {
                    if (File.Exists(path))
                    {
                        if (MessageManager.ShowMsgBox("INP-251208") == DialogResult.Yes)
                        {
                            this.bool_1 = true;
                            File.Delete(path);
                        }
                        else
                        {
                            this.bool_1 = false;
                            return;
                        }
                    }
                    stream = File.Create(path);
                }
                catch (Exception)
                {
                    MessageManager.ShowMsgBox("INP-251203", "错误", new string[] { path });
                    return;
                }
                string str4 = this.method_10();
                byte[] bytes = ToolUtil.GetBytes(str4 + "\0\r\n");
                string str6 = this.method_11();
                byte[] buffer2 = ToolUtil.GetBytes(str4);
                byte[] buffer = IDEA_Ctypt.DataToCryp(buffer2, ToolUtil.GetBytes(str6));
                List<string> list = this.method_12();
                try
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Write(buffer, 0, buffer.Length);
                    foreach (string str7 in list)
                    {
                        byte[] buffer4 = IDEA_Ctypt.DataToCryp(buffer2, ToolUtil.GetBytes(str7));
                        stream.Write(buffer4, 0, buffer4.Length);
                    }
                    stream.Flush();
                    stream.Close();
                }
                catch (Exception)
                {
                    MessageManager.ShowMsgBox("INP-251202");
                }
            }
        }

        private static string smethod_0(FPDetail fpdetail_0)
        {
            string str = "";
            if (fpdetail_0 == null)
            {
                return str;
            }
            if (fpdetail_0.ZFBZ)
            {
                str = str + "#";
            }
            return ((((str + ((int) fpdetail_0.FPType).ToString().Trim() + fpdetail_0.FPDM.Trim()) + fpdetail_0.FPHM.ToString("D8").Trim() + fpdetail_0.KPRQ.ToString("yyyyMMdd").Substring(2).Trim()) + fpdetail_0.GFSH.Trim() + fpdetail_0.XFSH.Trim()) + string.Format("{0:F2}", fpdetail_0.HJJE) + string.Format("{0:F2}", fpdetail_0.HJSE));
        }
    }
}

