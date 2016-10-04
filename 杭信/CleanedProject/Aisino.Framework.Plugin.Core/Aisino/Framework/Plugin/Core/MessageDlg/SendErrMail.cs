namespace Aisino.Framework.Plugin.Core.MessageDlg
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg.Model;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class SendErrMail : BaseForm
    {
        private AisinoBTN aisinoBTN_0;
        private AisinoGRP aisinoGRP_0;
        private AisinoBTN btnSendEmail;
        private Form0 form0_0;
        private Fwsk_ErrInfo fwsk_ErrInfo_0;
        private AisinoGRP groupBox1;
        private AisinoGRP groupBox2;
        private AisinoGRP groupBox3;
        private AisinoGRP groupBox4;
        private IContainer icontainer_2;
        private ILog ilog_0;
        private AisinoLBL label10;
        private AisinoLBL label11;
        private AisinoLBL label12;
        private AisinoLBL label13;
        private AisinoLBL label14;
        private AisinoLBL label15;
        private AisinoLBL label16;
        private AisinoRTX rctContent;
        private AisinoRTX rctOperate;
        private AisinoRTX rctQuestion;
        private AisinoRTX rctSolution;
        private string string_0;
        private string string_1;
        private AisinoTXT txtAddr;
        private AisinoTXT txtComp;
        private AisinoTXT txtMailAddr;
        private AisinoTXT txtMCode;
        private AisinoTXT txtName;
        private AisinoTXT txtTax;
        private AisinoTXT txtTEL;

        public SendErrMail(string string_2, string string_3, string string_4, string string_5, string string_6)
        {
            
            this.fwsk_ErrInfo_0 = new Fwsk_ErrInfo();
            this.string_0 = string.Empty;
            this.form0_0 = new Form0();
            this.string_1 = string.Empty;
            this.ilog_0 = LogUtil.GetLogger<SendErrMail>();
            try
            {
                this.InitializeComponent_1();
                this.method_1(string_2, string_3, string_4, string_5, string_6);
                this.method_3();
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                Bitmap image = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.CopyFromScreen(0, 0, 0, 0, Screen.AllScreens[0].Bounds.Size);
                    graphics.Dispose();
                    string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "fwkp_capture.jpeg");
                    image.Save(filename, ImageFormat.Jpeg);
                }
                this.btnSendEmail.Focus();
            }
            catch (BaseException exception)
            {
                this.ilog_0.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.ilog_0.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void aisinoBTN_0_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                this.method_5();
                this.string_1 = this.method_4();
                if (string.IsNullOrEmpty(this.string_1.Trim()))
                {
                    MessageBoxHelper.Show("生成XML失败。");
                }
                else
                {
                    string str = this.method_6(this.string_1);
                    if (!str.Equals(string.Empty) && (str.Length > 0))
                    {
                        MessageBoxHelper.Show("航天在线发送状态:" + str, "提示");
                    }
                }
            }
            catch (BaseException exception)
            {
                this.ilog_0.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.ilog_0.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_2 != null))
            {
                this.icontainer_2.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SendErrMail));
            this.rctContent = new AisinoRTX();
            this.rctOperate = new AisinoRTX();
            this.txtComp = new AisinoTXT();
            this.label10 = new AisinoLBL();
            this.txtAddr = new AisinoTXT();
            this.label11 = new AisinoLBL();
            this.txtMailAddr = new AisinoTXT();
            this.label12 = new AisinoLBL();
            this.txtTEL = new AisinoTXT();
            this.label13 = new AisinoLBL();
            this.txtName = new AisinoTXT();
            this.label14 = new AisinoLBL();
            this.txtMCode = new AisinoTXT();
            this.label15 = new AisinoLBL();
            this.txtTax = new AisinoTXT();
            this.label16 = new AisinoLBL();
            this.groupBox1 = new AisinoGRP();
            this.groupBox2 = new AisinoGRP();
            this.rctSolution = new AisinoRTX();
            this.groupBox3 = new AisinoGRP();
            this.rctQuestion = new AisinoRTX();
            this.groupBox4 = new AisinoGRP();
            this.btnSendEmail = new AisinoBTN();
            this.aisinoGRP_0 = new AisinoGRP();
            this.aisinoBTN_0 = new AisinoBTN();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.aisinoGRP_0.SuspendLayout();
            base.SuspendLayout();
            this.rctContent.BackColor = Color.FromArgb(0xe3, 0xee, 0xf9);
            this.rctContent.Location = new Point(6, 20);
            this.rctContent.Name = "rctContent";
            this.rctContent.ReadOnly = true;
            this.rctContent.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.rctContent.Size = new Size(0xc1, 0x189);
            this.rctContent.TabIndex = 1;
            this.rctContent.Text = "";
            this.rctOperate.Dock = DockStyle.Fill;
            this.rctOperate.Location = new Point(3, 0x11);
            this.rctOperate.Name = "rctOperate";
            this.rctOperate.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.rctOperate.Size = new Size(0x166, 0x56);
            this.rctOperate.TabIndex = 2;
            this.rctOperate.Text = "";
            this.txtComp.Location = new Point(0x27, 14);
            this.txtComp.Name = "txtComp";
            this.txtComp.Size = new Size(0xbc, 0x15);
            this.txtComp.TabIndex = 5;
            this.label10.BackColor = Color.Transparent;
            this.label10.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label10.Location = new Point(4, 15);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x20, 0x13);
            this.label10.TabIndex = 0x16;
            this.label10.Text = "单位";
            this.label10.TextAlign = ContentAlignment.MiddleRight;
            this.txtAddr.Location = new Point(0x27, 0x27);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new Size(0xbc, 0x15);
            this.txtAddr.TabIndex = 7;
            this.label11.BackColor = Color.Transparent;
            this.label11.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label11.Location = new Point(4, 40);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x20, 0x13);
            this.label11.TabIndex = 0x18;
            this.label11.Text = "地址";
            this.label11.TextAlign = ContentAlignment.MiddleRight;
            this.txtMailAddr.Location = new Point(0x27, 0x42);
            this.txtMailAddr.Name = "txtMailAddr";
            this.txtMailAddr.Size = new Size(0x60, 0x15);
            this.txtMailAddr.TabIndex = 9;
            this.label12.BackColor = Color.Transparent;
            this.label12.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label12.Location = new Point(4, 0x43);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x20, 0x13);
            this.label12.TabIndex = 0x1a;
            this.label12.Text = "邮箱";
            this.label12.TextAlign = ContentAlignment.MiddleRight;
            this.txtTEL.Location = new Point(0xa5, 0x43);
            this.txtTEL.Name = "txtTEL";
            this.txtTEL.Size = new Size(0x3d, 0x15);
            this.txtTEL.TabIndex = 10;
            this.label13.BackColor = Color.Transparent;
            this.label13.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label13.Location = new Point(0x85, 0x45);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x20, 0x13);
            this.label13.TabIndex = 0x1c;
            this.label13.Text = "电话";
            this.label13.TextAlign = ContentAlignment.MiddleRight;
            this.txtName.Location = new Point(0x108, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x5d, 0x15);
            this.txtName.TabIndex = 6;
            this.label14.BackColor = Color.Transparent;
            this.label14.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label14.Location = new Point(0xe5, 14);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x20, 0x13);
            this.label14.TabIndex = 30;
            this.label14.Text = "姓名";
            this.label14.TextAlign = ContentAlignment.MiddleRight;
            this.txtMCode.Location = new Point(0x108, 40);
            this.txtMCode.Name = "txtMCode";
            this.txtMCode.Size = new Size(0x5d, 0x15);
            this.txtMCode.TabIndex = 8;
            this.label15.BackColor = Color.Transparent;
            this.label15.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label15.Location = new Point(0xe5, 0x29);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x20, 0x13);
            this.label15.TabIndex = 0x20;
            this.label15.Text = "邮编";
            this.label15.TextAlign = ContentAlignment.MiddleRight;
            this.txtTax.Location = new Point(0x108, 0x43);
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new Size(0x5d, 0x15);
            this.txtTax.TabIndex = 11;
            this.label16.BackColor = Color.Transparent;
            this.label16.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label16.Location = new Point(0xe5, 0x44);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x20, 0x13);
            this.label16.TabIndex = 0x22;
            this.label16.Text = "传真";
            this.label16.TextAlign = ContentAlignment.MiddleRight;
            this.groupBox1.BackColor = Color.Transparent;
            this.groupBox1.Controls.Add(this.rctOperate);
            this.groupBox1.Location = new Point(0xe5, 0x13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x16c, 0x6a);
            this.groupBox1.TabIndex = 0x24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "具体的操作步骤";
            this.groupBox2.BackColor = Color.Transparent;
            this.groupBox2.Controls.Add(this.rctSolution);
            this.groupBox2.Location = new Point(0xe2, 0x83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x16c, 0x62);
            this.groupBox2.TabIndex = 0x25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "需要的解决方案";
            this.rctSolution.Dock = DockStyle.Fill;
            this.rctSolution.Location = new Point(3, 0x11);
            this.rctSolution.Name = "rctSolution";
            this.rctSolution.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.rctSolution.Size = new Size(0x166, 0x4e);
            this.rctSolution.TabIndex = 3;
            this.rctSolution.Text = "";
            this.groupBox3.BackColor = Color.Transparent;
            this.groupBox3.Controls.Add(this.rctQuestion);
            this.groupBox3.Location = new Point(0xe2, 0xeb);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x16c, 0x62);
            this.groupBox3.TabIndex = 0x26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "还有话要说";
            this.rctQuestion.Dock = DockStyle.Fill;
            this.rctQuestion.Location = new Point(3, 0x11);
            this.rctQuestion.Name = "rctQuestion";
            this.rctQuestion.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.rctQuestion.Size = new Size(0x166, 0x4e);
            this.rctQuestion.TabIndex = 4;
            this.rctQuestion.Text = "";
            this.groupBox4.BackColor = Color.Transparent;
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txtComp);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.txtAddr);
            this.groupBox4.Controls.Add(this.txtTax);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.txtMailAddr);
            this.groupBox4.Controls.Add(this.txtMCode);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txtTEL);
            this.groupBox4.Controls.Add(this.txtName);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Location = new Point(0xe2, 0x153);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x16c, 0x63);
            this.groupBox4.TabIndex = 0x27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "联系我";
            this.btnSendEmail.BackColor = Color.Transparent;
            this.btnSendEmail.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnSendEmail.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnSendEmail.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnSendEmail.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btnSendEmail.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnSendEmail.ForeColor = Color.White;
            this.btnSendEmail.Location = new Point(0x1ed, 0x1c0);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new Size(0x61, 0x24);
            this.btnSendEmail.TabIndex = 0;
            this.btnSendEmail.Text = "立即发送";
            this.btnSendEmail.UseVisualStyleBackColor = false;
            this.btnSendEmail.Click += new EventHandler(this.btnSendEmail_Click);
            this.aisinoGRP_0.BackColor = Color.Transparent;
            this.aisinoGRP_0.Controls.Add(this.rctContent);
            this.aisinoGRP_0.Location = new Point(15, 0x13);
            this.aisinoGRP_0.Name = "aisinoGRP1";
            this.aisinoGRP_0.Size = new Size(0xcd, 0x1a3);
            this.aisinoGRP_0.TabIndex = 0x25;
            this.aisinoGRP_0.TabStop = false;
            this.aisinoGRP_0.Text = "基本信息";
            this.aisinoBTN_0.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.aisinoBTN_0.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.aisinoBTN_0.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.aisinoBTN_0.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.aisinoBTN_0.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.aisinoBTN_0.ForeColor = Color.White;
            this.aisinoBTN_0.Location = new Point(0x179, 0x1c0);
            this.aisinoBTN_0.Name = "aisinoBTN1";
            this.aisinoBTN_0.Size = new Size(0x65, 0x24);
            this.aisinoBTN_0.TabIndex = 12;
            this.aisinoBTN_0.Text = "取消";
            this.aisinoBTN_0.UseVisualStyleBackColor = true;
            this.aisinoBTN_0.Click += new EventHandler(this.aisinoBTN_0_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x253, 0x1f0);
            base.Controls.Add(this.aisinoBTN_0);
            base.Controls.Add(this.aisinoGRP_0);
            base.Controls.Add(this.btnSendEmail);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "SendErrMail";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "发送错误信息";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.aisinoGRP_0.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void method_1(string string_2, string string_3, string string_4, string string_5, string string_6)
        {
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("【操作系统信息】");
                builder.AppendLine("操作系统：" + CheckSystemUtil.GetOperateName());
                this.fwsk_ErrInfo_0.Cpu = string.Format("{0}.{1}", Environment.Version.Major, Environment.Version.Minor);
                builder.AppendLine("公共运行库版本：" + this.fwsk_ErrInfo_0.Cpu);
                this.fwsk_ErrInfo_0.TotalMem = string.Format("{0}KB", Environment.WorkingSet);
                builder.AppendLine("计算机的工作集大小：" + this.fwsk_ErrInfo_0.TotalMem);
                this.fwsk_ErrInfo_0.VerName = "防伪开票 " + PropertyUtil.GetValue("MAIN_VER", "0000");
                builder.AppendLine("【软件版本信息】");
                builder.AppendLine(this.fwsk_ErrInfo_0.VerName);
                this.fwsk_ErrInfo_0.TaxCode = card.TaxCode;
                builder.AppendLine(string.Format("企业税号：{0}", this.fwsk_ErrInfo_0.TaxCode));
                this.fwsk_ErrInfo_0.CorpName = card.Corporation;
                builder.AppendLine(string.Format("企业名称：{0}", this.fwsk_ErrInfo_0.CorpName));
                if ((!card.QYLX.ISHY && !card.QYLX.ISJDC) || (!card.QYLX.ISPTFP && !card.QYLX.ISZYFP))
                {
                    if ((!card.QYLX.ISHY && !card.QYLX.ISJDC) || (card.QYLX.ISPTFP && card.QYLX.ISZYFP))
                    {
                        if (card.StateInfo.IsMainMachine == 1)
                        {
                            this.fwsk_ErrInfo_0.MachineInfoAll = string.Format("开票机信息：主机　分机数目：{0}", card.StateInfo.MachineNumber.ToString());
                            this.fwsk_ErrInfo_0.MachineInfo = "主机";
                            this.fwsk_ErrInfo_0.MachCount = card.StateInfo.MachineNumber.ToString();
                        }
                        else
                        {
                            this.fwsk_ErrInfo_0.MachineInfoAll = string.Format("开票机信息：分机　本机号：{0}", card.Machine.ToString());
                            this.fwsk_ErrInfo_0.MachineInfo = "分机";
                            this.fwsk_ErrInfo_0.MachCount = card.Machine.ToString();
                        }
                    }
                    else
                    {
                        this.fwsk_ErrInfo_0.MachineInfoAll = string.Format("开票机信息：总参单营户", new object[0]);
                        this.fwsk_ErrInfo_0.MachineInfo = (card.Machine == 0) ? "主机" : "分机";
                        this.fwsk_ErrInfo_0.MachCount = card.Machine.ToString();
                    }
                }
                else
                {
                    this.fwsk_ErrInfo_0.MachineInfoAll = string.Format("开票机信息：混营用户", new object[0]);
                    this.fwsk_ErrInfo_0.MachineInfo = (card.Machine == 0) ? "主机" : "分机";
                    this.fwsk_ErrInfo_0.MachCount = card.Machine.ToString();
                }
                builder.AppendLine(this.fwsk_ErrInfo_0.MachineInfoAll);
                builder.AppendLine("【错误信息】");
                this.fwsk_ErrInfo_0.OpItem = string_2;
                builder.AppendLine(string.Format("操作项：{0}", this.fwsk_ErrInfo_0.OpItem));
                this.fwsk_ErrInfo_0.ErrCode = string_3;
                builder.AppendLine(string.Format("事件代码：{0}", string_3));
                this.fwsk_ErrInfo_0.FuncCode = string.Format("功能号：{0}", string_4);
                if ((string_4 != null) && !string.Empty.Equals(string_4))
                {
                    builder.AppendLine(this.fwsk_ErrInfo_0.FuncCode);
                }
                builder.AppendLine(string.Format("事件描述：{0}", string_5));
                builder.AppendLine(string.Format("可能原因：{0}", string_6));
                this.rctContent.AppendText(builder.ToString());
            }
            catch (BaseException exception)
            {
                this.ilog_0.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.ilog_0.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private string method_2()
        {
            try
            {
                Process process = new Process {
                    StartInfo = { FileName = "cmd.exe", UseShellExecute = false, RedirectStandardInput = true, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true }
                };
                process.Start();
                process.StandardInput.WriteLine("Ver");
                process.StandardInput.WriteLine("exit");
                string str = process.StandardOutput.ReadToEnd();
                return str.Substring(0, str.IndexOf("Corp.") + 5);
            }
            catch (BaseException exception)
            {
                this.ilog_0.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.ilog_0.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return string.Empty;
        }

        private void method_3()
        {
            try
            {
                this.string_0 = "http://www.cnnsr.com.cn/support/yjdp/errorreport.asp";
            }
            catch (BaseException exception)
            {
                this.ilog_0.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.ilog_0.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private string method_4()
        {
            try
            {
                return this.form0_0.method_5(this.fwsk_ErrInfo_0);
            }
            catch (BaseException exception)
            {
                this.ilog_0.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.ilog_0.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return string.Empty;
        }

        private void method_5()
        {
            try
            {
                this.fwsk_ErrInfo_0.Step = this.rctOperate.Text.Trim();
                this.fwsk_ErrInfo_0.Resolvent = this.rctSolution.Text.Trim();
                this.fwsk_ErrInfo_0.AddAdvise = this.rctQuestion.Text.Trim();
                this.fwsk_ErrInfo_0.Corp = this.txtComp.Text.Trim();
                this.fwsk_ErrInfo_0.Adress = this.txtAddr.Text.Trim();
                this.fwsk_ErrInfo_0.Email = this.txtMailAddr.Text.Trim();
                this.fwsk_ErrInfo_0.Telephone = this.txtTEL.Text.Trim();
                this.fwsk_ErrInfo_0.Name = this.txtName.Text.Trim();
                this.fwsk_ErrInfo_0.Post = this.txtMCode.Text.Trim();
                this.fwsk_ErrInfo_0.Tax = this.txtTax.Text.Trim();
            }
            catch (BaseException exception)
            {
                this.ilog_0.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.ilog_0.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private string method_6(string string_2)
        {
            try
            {
                XmlTextReader reader;
                string str = string.Empty;
                XmlDocument document = new XmlDocument();
                str = string_2;
                string str2 = "errorinfo=" + str;
                if (System.IO.File.Exists(string_2))
                {
                    System.IO.File.Delete(string_2);
                }
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.string_0);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                string s = str2;
                byte[] bytes = Encoding.Default.GetBytes(s);
                request.ContentLength = bytes.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                Stream responseStream = ((HttpWebResponse) request.GetResponse()).GetResponseStream();
                try
                {
                    if (responseStream != null)
                    {
                        long length = responseStream.Length;
                        if (responseStream.Length >= 1L)
                        {
                            goto Label_00E0;
                        }
                    }
                    return "返回状态出错";
                }
                catch (Exception)
                {
                    return "返回状态出错";
                }
            Label_00E0:
                reader = new XmlTextReader(responseStream);
                if (reader != null)
                {
                    reader.MoveToContent();
                    string xml = reader.ReadOuterXml();
                    reader.Close();
                    document = new XmlDocument();
                    document.LoadXml(xml);
                    XmlNode documentElement = document.DocumentElement;
                    if (documentElement == null)
                    {
                        return "返回状态出错";
                    }
                    if ("Return_Info" != documentElement.Name)
                    {
                        return "返回状态出错";
                    }
                    XmlNode node2 = documentElement.SelectSingleNode("ReturnDesc");
                    if (node2 != null)
                    {
                        return node2.InnerText;
                    }
                }
                return "返回状态出错";
            }
            catch (BaseException exception)
            {
                this.ilog_0.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.ilog_0.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return string.Empty;
        }
    }
}

