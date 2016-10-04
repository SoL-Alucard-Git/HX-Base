namespace Aisino.Framework.Plugin.Core.MessageDlg
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Mail;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class EmailCanShuSet : BaseForm
    {
        private AisinoBTN btn_LinkTest_Rece;
        private AisinoBTN btn_LinkTest_Send;
        private AisinoBTN btn_QueDing;
        private AisinoBTN btn_QuXiao;
        private static byte[] byte_0;
        private static byte[] byte_1;
        private CheckBox chkBox_MyServerYanZheng_Send;
        private CheckBox chkBox_SendItemSet_CG;
        private CheckBox chkBox_ServerDeleteEmail_GJ;
        private CheckBox chkBox_ZhiJieSend_CG;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox5;
        private GroupBox groupBox7;
        private IContainer icontainer_2;
        private ILog ilog_0;
        private Label label1;
        private Label label13;
        private Label label14;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage3;
        private TextBoxRegex textRegex_PassWord_Rece;
        private TextBoxRegex textRegex_PassWord_Send;
        private TextBoxRegex textRegex_ReceEmail_POP3;
        private TextBoxRegex textRegex_ReceEmail_POP3_GJ;
        private TextBoxRegex textRegex_SendEmail_SMPT_GJ;
        private TextBoxRegex textRegex_SendEmail_SMTP;
        private TextBoxRegex textRegex_ZhangHuMing_Rece;
        private TextBoxRegex textRegex_ZhangHuMing_Send;

        static EmailCanShuSet()
        {
            
            byte_0 = new byte[] { 
                0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 40, 
                0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 40
             };
            byte_1 = new byte[] { 0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 40 };
        }

        public EmailCanShuSet()
        {
            
            this.ilog_0 = LogUtil.GetLogger<EmailCanShuSet>();
            try
            {
                this.method_3();
                this.method_1();
                this.btn_QueDing.Focus();
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

        private void btn_LinkTest_Rece_Click(object sender, EventArgs e)
        {
            try
            {
                string str = this.textRegex_ReceEmail_POP3.Text.Trim();
                if (string.IsNullOrEmpty(str))
                {
                    MessageManager.ShowMsgBox("INP-441153");
                }
                else
                {
                    int result = 0;
                    if (int.TryParse(this.textRegex_ReceEmail_POP3_GJ.Text.Trim(), out result) && (result > 0))
                    {
                        string str2 = this.textRegex_ZhangHuMing_Rece.Text.Trim();
                        if (string.IsNullOrEmpty(str2))
                        {
                            MessageManager.ShowMsgBox("INP-441155");
                        }
                        else
                        {
                            string str3 = this.textRegex_PassWord_Rece.Text.Trim();
                            MailService service = new MailService(str, result);
                            string str4 = "";
                            if (!service.Connect(str2, str3, out str4))
                            {
                                MessageManager.ShowMsgBox("连接测试失败！" + Environment.NewLine + str4);
                            }
                            else
                            {
                                MessageManager.ShowMsgBox("INP-442218");
                                service.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageManager.ShowMsgBox("INP-441154");
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

        private void btn_LinkTest_Send_Click(object sender, EventArgs e)
        {
            try
            {
                string str = this.textRegex_SendEmail_SMTP.Text.Trim();
                if (string.IsNullOrEmpty(str))
                {
                    MessageManager.ShowMsgBox("INP-441156");
                }
                else
                {
                    int result = 0;
                    if (int.TryParse(this.textRegex_SendEmail_SMPT_GJ.Text.Trim(), out result) && (result > 0))
                    {
                        string str2 = null;
                        string str3 = null;
                        if (this.chkBox_MyServerYanZheng_Send.Checked)
                        {
                            str2 = this.textRegex_ZhangHuMing_Send.Text.Trim();
                            if (string.IsNullOrEmpty(str2))
                            {
                                MessageManager.ShowMsgBox("INP-441158");
                                return;
                            }
                            str3 = this.textRegex_PassWord_Send.Text.Trim();
                        }
                        string str4 = string.Empty;
                        MailService service = new MailService(str, result);
                        if (!service.Connect(str2, str3, out str4))
                        {
                            MessageManager.ShowMsgBox("连接测试失败！" + Environment.NewLine + str4);
                        }
                        else
                        {
                            MessageManager.ShowMsgBox("INP-442218");
                        }
                        service.Close();
                    }
                    else
                    {
                        MessageManager.ShowMsgBox("INP-441157");
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

        private void btn_QueDing_Click(object sender, EventArgs e)
        {
            try
            {
                PropertyUtil.SetValue("POP3_SERVER", this.textRegex_ReceEmail_POP3.Text.Trim());
                int result = 110;
                string s = this.textRegex_ReceEmail_POP3_GJ.Text.Trim();
                if (int.TryParse(s, out result) && (result > 0))
                {
                    PropertyUtil.SetValue("POP3_PORT", s);
                }
                else
                {
                    s = "110";
                    PropertyUtil.SetValue("POP3_PORT", "110");
                }
                PropertyUtil.SetValue("SMTP_SERVER", this.textRegex_SendEmail_SMTP.Text.Trim());
                int num2 = 0x19;
                string str2 = this.textRegex_SendEmail_SMPT_GJ.Text.Trim();
                if (int.TryParse(str2, out num2) && (num2 > 0))
                {
                    PropertyUtil.SetValue("SMTP_PORT", str2);
                }
                else
                {
                    str2 = "25";
                    PropertyUtil.SetValue("SMTP_PORT", "25");
                }
                PropertyUtil.SetValue("POP3_USER", this.textRegex_ZhangHuMing_Rece.Text.Trim());
                PropertyUtil.SetValue("POP3_PASS", Convert.ToBase64String(AES_Crypt.Encrypt(Encoding.Default.GetBytes(this.textRegex_PassWord_Rece.Text.Trim()), byte_0, byte_1)));
                PropertyUtil.SetValue("SMTP_AUTH", this.chkBox_MyServerYanZheng_Send.Checked ? "1" : "0");
                PropertyUtil.SetValue("SMTP_USER", this.textRegex_ZhangHuMing_Send.Text.Trim());
                PropertyUtil.SetValue("SMTP_PASS", Convert.ToBase64String(AES_Crypt.Encrypt(Encoding.Default.GetBytes(this.textRegex_PassWord_Send.Text.Trim()), byte_0, byte_1)));
                PropertyUtil.SetValue("MAIL_ALL_CONFIG", this.chkBox_SendItemSet_CG.Checked ? "1" : "0");
                PropertyUtil.SetValue("MAIL_ALL_SEND", this.chkBox_ZhiJieSend_CG.Checked ? "1" : "0");
                string str3 = this.chkBox_ServerDeleteEmail_GJ.Checked ? "1" : "0";
                PropertyUtil.SetValue("MAIL_DEL", str3);
                base.DialogResult = DialogResult.OK;
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

        private void btn_QuXiao_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void chkBox_MyServerYanZheng_Send_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = ((CheckBox) sender).Checked;
                this.label5.Enabled = this.textRegex_ZhangHuMing_Send.Enabled = flag;
                this.label6.Enabled = this.textRegex_PassWord_Send.Enabled = flag;
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

        private void method_1()
        {
            try
            {
                this.textRegex_ReceEmail_POP3.Text = PropertyUtil.GetValue("POP3_SERVER").Trim();
                int result = 110;
                string s = PropertyUtil.GetValue("POP3_PORT").Trim();
                if (!int.TryParse(s, out result) || (result <= 0))
                {
                    s = "110";
                }
                this.textRegex_ReceEmail_POP3_GJ.Text = s;
                this.textRegex_SendEmail_SMTP.Text = PropertyUtil.GetValue("SMTP_SERVER").Trim();
                int num2 = 0x19;
                string str2 = PropertyUtil.GetValue("SMTP_PORT").Trim();
                if (!int.TryParse(str2, out num2) || (num2 <= 0))
                {
                    str2 = "25";
                }
                this.textRegex_SendEmail_SMPT_GJ.Text = str2;
                this.textRegex_ZhangHuMing_Rece.Text = PropertyUtil.GetValue("POP3_USER").Trim();
                this.textRegex_PassWord_Rece.Text = (PropertyUtil.GetValue("POP3_PASS").Length == 0) ? "" : Encoding.Default.GetString(AES_Crypt.Decrypt(Convert.FromBase64String(PropertyUtil.GetValue("POP3_PASS").Trim()), byte_0, byte_1, null));
                string str3 = PropertyUtil.GetValue("SMTP_AUTH").Trim();
                this.chkBox_MyServerYanZheng_Send.Checked = string.IsNullOrEmpty(str3) || (str3 != "0");
                this.textRegex_ZhangHuMing_Send.Text = PropertyUtil.GetValue("SMTP_USER").Trim();
                this.textRegex_PassWord_Send.Text = (PropertyUtil.GetValue("SMTP_PASS").Length == 0) ? "" : Encoding.Default.GetString(AES_Crypt.Decrypt(Convert.FromBase64String(PropertyUtil.GetValue("SMTP_PASS").Trim()), byte_0, byte_1, null));
                string str4 = PropertyUtil.GetValue("MAIL_ALL_CONFIG").Trim();
                this.chkBox_SendItemSet_CG.Checked = string.IsNullOrEmpty(str4) || (str4 != "0");
                string str5 = PropertyUtil.GetValue("MAIL_ALL_SEND").Trim();
                this.chkBox_ZhiJieSend_CG.Checked = string.IsNullOrEmpty(str5) || (str5 != "0");
                string str6 = PropertyUtil.GetValue("MAIL_DEL").Trim();
                this.chkBox_ServerDeleteEmail_GJ.Checked = !string.IsNullOrEmpty(str6) && (str6 != "0");
                this.chkBox_MyServerYanZheng_Send_Click(this.chkBox_MyServerYanZheng_Send, null);
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

        private void method_2()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(EmailCanShuSet));
            this.btn_QueDing = new AisinoBTN();
            this.btn_QuXiao = new AisinoBTN();
            this.tabPage3 = new TabPage();
            this.groupBox5 = new GroupBox();
            this.chkBox_ServerDeleteEmail_GJ = new CheckBox();
            this.groupBox7 = new GroupBox();
            this.chkBox_SendItemSet_CG = new CheckBox();
            this.chkBox_ZhiJieSend_CG = new CheckBox();
            this.tabPage1 = new TabPage();
            this.groupBox3 = new GroupBox();
            this.btn_LinkTest_Send = new AisinoBTN();
            this.chkBox_MyServerYanZheng_Send = new CheckBox();
            this.textRegex_PassWord_Send = new TextBoxRegex();
            this.label6 = new Label();
            this.textRegex_ZhangHuMing_Send = new TextBoxRegex();
            this.label5 = new Label();
            this.groupBox2 = new GroupBox();
            this.btn_LinkTest_Rece = new AisinoBTN();
            this.textRegex_PassWord_Rece = new TextBoxRegex();
            this.textRegex_ZhangHuMing_Rece = new TextBoxRegex();
            this.label4 = new Label();
            this.label3 = new Label();
            this.groupBox1 = new GroupBox();
            this.label14 = new Label();
            this.label13 = new Label();
            this.textRegex_ReceEmail_POP3_GJ = new TextBoxRegex();
            this.textRegex_SendEmail_SMPT_GJ = new TextBoxRegex();
            this.textRegex_SendEmail_SMTP = new TextBoxRegex();
            this.textRegex_ReceEmail_POP3 = new TextBoxRegex();
            this.label2 = new Label();
            this.label1 = new Label();
            this.tabControl1 = new TabControl();
            this.tabPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            base.SuspendLayout();
            this.btn_QueDing.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btn_QueDing.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btn_QueDing.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btn_QueDing.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btn_QueDing.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btn_QueDing.ForeColor = Color.White;
            this.btn_QueDing.Location = new Point(0x12a, 0x175);
            this.btn_QueDing.Name = "btn_QueDing";
            this.btn_QueDing.Size = new Size(80, 30);
            this.btn_QueDing.TabIndex = 0;
            this.btn_QueDing.Text = "确定";
            this.btn_QueDing.UseVisualStyleBackColor = true;
            this.btn_QueDing.Click += new EventHandler(this.btn_QueDing_Click);
            this.btn_QuXiao.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btn_QuXiao.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btn_QuXiao.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btn_QuXiao.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btn_QuXiao.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btn_QuXiao.ForeColor = Color.White;
            this.btn_QuXiao.Location = new Point(0x184, 0x175);
            this.btn_QuXiao.Name = "btn_QuXiao";
            this.btn_QuXiao.Size = new Size(80, 30);
            this.btn_QuXiao.TabIndex = 2;
            this.btn_QuXiao.Text = "取消";
            this.btn_QuXiao.UseVisualStyleBackColor = true;
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Location = new Point(4, 0x16);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new Size(0x1c0, 320);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "其它";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.groupBox5.Controls.Add(this.chkBox_ServerDeleteEmail_GJ);
            this.groupBox5.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox5.Location = new Point(0x16, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x17d, 0x48);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "邮件副本";
            this.chkBox_ServerDeleteEmail_GJ.AutoSize = true;
            this.chkBox_ServerDeleteEmail_GJ.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.chkBox_ServerDeleteEmail_GJ.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.chkBox_ServerDeleteEmail_GJ.Location = new Point(0x16, 0x22);
            this.chkBox_ServerDeleteEmail_GJ.Name = "chkBox_ServerDeleteEmail_GJ";
            this.chkBox_ServerDeleteEmail_GJ.Size = new Size(0xf3, 0x15);
            this.chkBox_ServerDeleteEmail_GJ.TabIndex = 0;
            this.chkBox_ServerDeleteEmail_GJ.Text = "成功接收邮件后，从邮件服务器删除邮件";
            this.chkBox_ServerDeleteEmail_GJ.UseVisualStyleBackColor = true;
            this.groupBox7.Controls.Add(this.chkBox_SendItemSet_CG);
            this.groupBox7.Controls.Add(this.chkBox_ZhiJieSend_CG);
            this.groupBox7.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox7.Location = new Point(0x16, 0x74);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(0x17d, 100);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "发票填开打印后……";
            this.chkBox_SendItemSet_CG.AutoSize = true;
            this.chkBox_SendItemSet_CG.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.chkBox_SendItemSet_CG.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.chkBox_SendItemSet_CG.Location = new Point(30, 0x43);
            this.chkBox_SendItemSet_CG.Name = "chkBox_SendItemSet_CG";
            this.chkBox_SendItemSet_CG.Size = new Size(0xc3, 0x15);
            this.chkBox_SendItemSet_CG.TabIndex = 1;
            this.chkBox_SendItemSet_CG.Text = "每次发送时都进行发送选项设置";
            this.chkBox_SendItemSet_CG.UseVisualStyleBackColor = true;
            this.chkBox_ZhiJieSend_CG.AutoSize = true;
            this.chkBox_ZhiJieSend_CG.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.chkBox_ZhiJieSend_CG.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.chkBox_ZhiJieSend_CG.Location = new Point(30, 0x1f);
            this.chkBox_ZhiJieSend_CG.Name = "chkBox_ZhiJieSend_CG";
            this.chkBox_ZhiJieSend_CG.Size = new Size(0xb7, 0x15);
            this.chkBox_ZhiJieSend_CG.TabIndex = 0;
            this.chkBox_ZhiJieSend_CG.Text = "发票填开打印后直接进行发送";
            this.chkBox_ZhiJieSend_CG.UseVisualStyleBackColor = true;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new Point(4, 0x16);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x1c0, 320);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "邮件服务器";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.groupBox3.Controls.Add(this.btn_LinkTest_Send);
            this.groupBox3.Controls.Add(this.chkBox_MyServerYanZheng_Send);
            this.groupBox3.Controls.Add(this.textRegex_PassWord_Send);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textRegex_ZhangHuMing_Send);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox3.Location = new Point(6, 0xc3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x1b4, 0x6f);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "邮件发送服务器";
            this.btn_LinkTest_Send.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btn_LinkTest_Send.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btn_LinkTest_Send.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btn_LinkTest_Send.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btn_LinkTest_Send.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btn_LinkTest_Send.ForeColor = Color.White;
            this.btn_LinkTest_Send.Location = new Point(0x163, 0x35);
            this.btn_LinkTest_Send.Name = "btn_LinkTest_Send";
            this.btn_LinkTest_Send.Size = new Size(0x47, 0x27);
            this.btn_LinkTest_Send.TabIndex = 12;
            this.btn_LinkTest_Send.Text = "连接测试";
            this.btn_LinkTest_Send.UseVisualStyleBackColor = true;
            this.btn_LinkTest_Send.Visible = false;
            this.btn_LinkTest_Send.Click += new EventHandler(this.btn_LinkTest_Send_Click);
            this.chkBox_MyServerYanZheng_Send.AutoSize = true;
            this.chkBox_MyServerYanZheng_Send.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.chkBox_MyServerYanZheng_Send.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.chkBox_MyServerYanZheng_Send.Location = new Point(0x15, 20);
            this.chkBox_MyServerYanZheng_Send.Name = "chkBox_MyServerYanZheng_Send";
            this.chkBox_MyServerYanZheng_Send.Size = new Size(0x9c, 0x10);
            this.chkBox_MyServerYanZheng_Send.TabIndex = 0;
            this.chkBox_MyServerYanZheng_Send.Text = "我的服务器要求身份验证";
            this.chkBox_MyServerYanZheng_Send.UseVisualStyleBackColor = true;
            this.textRegex_PassWord_Send.Location = new Point(0x4e, 80);
            this.textRegex_PassWord_Send.MaxLength = 200;
            this.textRegex_PassWord_Send.Name = "textRegex_PassWord_Send";
            this.textRegex_PassWord_Send.PasswordChar = '*';
            this.textRegex_PassWord_Send.RegexText = "";
            this.textRegex_PassWord_Send.Size = new Size(0x109, 0x17);
            this.textRegex_PassWord_Send.TabIndex = 11;
            this.label6.AutoSize = true;
            this.label6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label6.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.label6.Location = new Point(0x13, 0x53);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "密  码：";
            this.textRegex_ZhangHuMing_Send.Location = new Point(0x4e, 0x2c);
            this.textRegex_ZhangHuMing_Send.MaxLength = 200;
            this.textRegex_ZhangHuMing_Send.Name = "textRegex_ZhangHuMing_Send";
            this.textRegex_ZhangHuMing_Send.RegexText = "";
            this.textRegex_ZhangHuMing_Send.Size = new Size(0x109, 0x17);
            this.textRegex_ZhangHuMing_Send.TabIndex = 10;
            this.label5.AutoSize = true;
            this.label5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label5.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.label5.Location = new Point(0x13, 0x2f);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "账户名：";
            this.groupBox2.Controls.Add(this.btn_LinkTest_Rece);
            this.groupBox2.Controls.Add(this.textRegex_PassWord_Rece);
            this.groupBox2.Controls.Add(this.textRegex_ZhangHuMing_Rece);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox2.Location = new Point(6, 0x5e);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1b4, 0x56);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "邮件接收服务器";
            this.btn_LinkTest_Rece.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btn_LinkTest_Rece.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btn_LinkTest_Rece.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btn_LinkTest_Rece.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btn_LinkTest_Rece.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btn_LinkTest_Rece.ForeColor = Color.White;
            this.btn_LinkTest_Rece.Location = new Point(0x163, 0x20);
            this.btn_LinkTest_Rece.Name = "btn_LinkTest_Rece";
            this.btn_LinkTest_Rece.Size = new Size(0x47, 0x25);
            this.btn_LinkTest_Rece.TabIndex = 9;
            this.btn_LinkTest_Rece.Text = "连接测试";
            this.btn_LinkTest_Rece.UseVisualStyleBackColor = true;
            this.btn_LinkTest_Rece.Click += new EventHandler(this.btn_LinkTest_Rece_Click);
            this.textRegex_PassWord_Rece.Location = new Point(0x4e, 0x36);
            this.textRegex_PassWord_Rece.MaxLength = 200;
            this.textRegex_PassWord_Rece.Name = "textRegex_PassWord_Rece";
            this.textRegex_PassWord_Rece.PasswordChar = '*';
            this.textRegex_PassWord_Rece.RegexText = "";
            this.textRegex_PassWord_Rece.Size = new Size(0x109, 0x17);
            this.textRegex_PassWord_Rece.TabIndex = 8;
            this.textRegex_ZhangHuMing_Rece.Location = new Point(0x4e, 0x15);
            this.textRegex_ZhangHuMing_Rece.MaxLength = 200;
            this.textRegex_ZhangHuMing_Rece.Name = "textRegex_ZhangHuMing_Rece";
            this.textRegex_ZhangHuMing_Rece.RegexText = "";
            this.textRegex_ZhangHuMing_Rece.Size = new Size(0x109, 0x17);
            this.textRegex_ZhangHuMing_Rece.TabIndex = 7;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label4.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.label4.Location = new Point(0x13, 0x1b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "账户名：";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.label3.Location = new Point(0x13, 60);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "密  码：";
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.textRegex_ReceEmail_POP3_GJ);
            this.groupBox1.Controls.Add(this.textRegex_SendEmail_SMPT_GJ);
            this.groupBox1.Controls.Add(this.textRegex_SendEmail_SMTP);
            this.groupBox1.Controls.Add(this.textRegex_ReceEmail_POP3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox1.Location = new Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1b4, 0x52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器信息";
            this.label14.AutoSize = true;
            this.label14.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label14.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.label14.Location = new Point(0x12f, 0x35);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x29, 12);
            this.label14.TabIndex = 11;
            this.label14.Text = "端口：";
            this.label13.AutoSize = true;
            this.label13.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label13.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.label13.Location = new Point(0x12f, 0x11);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x29, 12);
            this.label13.TabIndex = 10;
            this.label13.Text = "端口：";
            this.textRegex_ReceEmail_POP3_GJ.Location = new Point(350, 0x11);
            this.textRegex_ReceEmail_POP3_GJ.MaxLength = 10;
            this.textRegex_ReceEmail_POP3_GJ.Name = "textRegex_ReceEmail_POP3_GJ";
            this.textRegex_ReceEmail_POP3_GJ.RegexText = "";
            this.textRegex_ReceEmail_POP3_GJ.Size = new Size(0x45, 0x17);
            this.textRegex_ReceEmail_POP3_GJ.TabIndex = 4;
            this.textRegex_SendEmail_SMPT_GJ.Location = new Point(350, 50);
            this.textRegex_SendEmail_SMPT_GJ.MaxLength = 10;
            this.textRegex_SendEmail_SMPT_GJ.Name = "textRegex_SendEmail_SMPT_GJ";
            this.textRegex_SendEmail_SMPT_GJ.RegexText = "";
            this.textRegex_SendEmail_SMPT_GJ.Size = new Size(0x45, 0x17);
            this.textRegex_SendEmail_SMPT_GJ.TabIndex = 6;
            this.textRegex_SendEmail_SMTP.Location = new Point(0x9d, 50);
            this.textRegex_SendEmail_SMTP.MaxLength = 200;
            this.textRegex_SendEmail_SMTP.Name = "textRegex_SendEmail_SMTP";
            this.textRegex_SendEmail_SMTP.RegexText = "";
            this.textRegex_SendEmail_SMTP.Size = new Size(0x7c, 0x17);
            this.textRegex_SendEmail_SMTP.TabIndex = 5;
            this.textRegex_ReceEmail_POP3.Location = new Point(0x9d, 0x11);
            this.textRegex_ReceEmail_POP3.MaxLength = 200;
            this.textRegex_ReceEmail_POP3.Name = "textRegex_ReceEmail_POP3";
            this.textRegex_ReceEmail_POP3.RegexText = "";
            this.textRegex_ReceEmail_POP3.Size = new Size(0x7c, 0x17);
            this.textRegex_ReceEmail_POP3.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.label2.Location = new Point(0x13, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x7d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "发送服务器（SMTP）：";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.label1.Location = new Point(0x13, 0x17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x7d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "接收服务器（POP3）：";
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x1c8, 0x15a);
            this.tabControl1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1dd, 0x1a6);
            base.Controls.Add(this.btn_QuXiao);
            base.Controls.Add(this.btn_QueDing);
            base.Controls.Add(this.tabControl1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "EmailCanShuSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "邮件参数设置";
            this.tabPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void method_3()
        {
            this.method_2();
            this.textRegex_SendEmail_SMPT_GJ.RegexText = "^[0-9]*$";
            this.textRegex_ReceEmail_POP3_GJ.RegexText = "^[0-9]*$";
            this.textRegex_ReceEmail_POP3.MaxLength = 200;
            this.textRegex_SendEmail_SMTP.MaxLength = 200;
            this.textRegex_ZhangHuMing_Rece.MaxLength = 200;
            this.textRegex_PassWord_Rece.MaxLength = 200;
            this.textRegex_ZhangHuMing_Send.MaxLength = 200;
            this.textRegex_PassWord_Send.MaxLength = 200;
            this.textRegex_SendEmail_SMPT_GJ.MaxLength = 4;
            this.textRegex_ReceEmail_POP3_GJ.MaxLength = 4;
            this.textRegex_PassWord_Rece.PasswordChar = '*';
            this.textRegex_PassWord_Send.PasswordChar = '*';
            this.chkBox_MyServerYanZheng_Send.Click += new EventHandler(this.chkBox_MyServerYanZheng_Send_Click);
            this.btn_QuXiao.Click += new EventHandler(this.btn_QuXiao_Click);
        }
    }
}

