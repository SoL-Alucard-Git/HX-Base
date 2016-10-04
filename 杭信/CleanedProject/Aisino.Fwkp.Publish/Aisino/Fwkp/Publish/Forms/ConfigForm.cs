namespace Aisino.Fwkp.Publish.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Publish.BLL;
    using Aisino.Fwkp.Publish.Entry;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfigForm : DockForm
    {
        private AisinoCHK autoConncb;
        private AisinoBTN button1;
        private AisinoTXT clientPorttb;
        private IContainer components;
        private GroupBox groupBox1;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private Label label5;
        private AisinoTXT maxWaitTimetb;
        private RichTextBox richTextBox1;
        private AisinoTXT serverIPtb;
        private AisinoTXT serverPorttb;
        private AisinoCHK setClientPortcb;

        public ConfigForm()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Validate())
            {
                if ((!string.Equals(Config.serverIP, this.serverIPtb.Text) || !string.Equals(Config.serverPort, this.serverPorttb.Text)) && PubClient.Instance.PubCLientConnected)
                {
                    MessageManager.ShowMsgBox("C0002");
                }
                else
                {
                    if (!string.Equals(PropertyUtil.GetValue("PUB_SERVER_IP", ""), this.serverIPtb.Text))
                    {
                        PropertyUtil.SetValue("PUB_MAX_MESS_ID", "0");
                    }
                    PropertyUtil.SetValue("PUB_SERVER_IP", this.serverIPtb.Text);
                    PropertyUtil.SetValue("PUB_SERVER_PORT", this.serverPorttb.Text);
                    PropertyUtil.SetValue("PUB_CLIENT_PORT", this.setClientPortcb.Checked ? this.clientPorttb.Text : "0");
                    PropertyUtil.SetValue("PUB_MAX_CONN_TIME", this.maxWaitTimetb.Text);
                    PropertyUtil.SetValue("PUB_CONN_ONLOAD", this.autoConncb.Checked ? "1" : "0");
                    Config.reLoad();
                    base.Close();
                }
            }
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            this.serverIPtb.Text = Config.serverIP;
            this.serverPorttb.Text = Config.serverPort;
            this.maxWaitTimetb.Text = Config.maxConnWaitTime;
            if (Config.clientPort.Equals("0"))
            {
                this.setClientPortcb.Checked = false;
                this.clientPorttb.Enabled = false;
            }
            else
            {
                this.setClientPortcb.Checked = true;
                this.clientPorttb.Enabled = true;
                this.clientPorttb.Text = Config.clientPort;
            }
            if (Config.connOnLoad.Equals("0"))
            {
                this.autoConncb.Checked = false;
            }
            else
            {
                this.autoConncb.Checked = true;
            }
            this.richTextBox1.Text = PubClient.status;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ConfigForm));
            this.label1 = new AisinoLBL();
            this.serverIPtb = new AisinoTXT();
            this.label2 = new AisinoLBL();
            this.serverPorttb = new AisinoTXT();
            this.clientPorttb = new AisinoTXT();
            this.setClientPortcb = new AisinoCHK();
            this.label3 = new AisinoLBL();
            this.maxWaitTimetb = new AisinoTXT();
            this.autoConncb = new AisinoCHK();
            this.button1 = new AisinoBTN();
            this.groupBox1 = new GroupBox();
            this.label5 = new Label();
            this.richTextBox1 = new RichTextBox();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = Color.Black;
            this.label1.Location = new Point(0x18, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "地址：";
            this.serverIPtb.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.serverIPtb.Location = new Point(0x47, 0x1d);
            this.serverIPtb.Name = "serverIPtb";
            this.serverIPtb.Size = new Size(0xae, 0x15);
            this.serverIPtb.TabIndex = 0;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = Color.Black;
            this.label2.Location = new Point(0x11b, 0x20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "端口：";
            this.serverPorttb.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.serverPorttb.Location = new Point(330, 0x1d);
            this.serverPorttb.Name = "serverPorttb";
            this.serverPorttb.Size = new Size(0x3b, 0x15);
            this.serverPorttb.TabIndex = 1;
            this.clientPorttb.Location = new Point(0x8e, 0x5c);
            this.clientPorttb.Name = "clientPorttb";
            this.clientPorttb.Size = new Size(0x3b, 0x15);
            this.clientPorttb.TabIndex = 4;
            this.setClientPortcb.AutoSize = true;
            this.setClientPortcb.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.setClientPortcb.ForeColor = Color.Black;
            this.setClientPortcb.Location = new Point(0x19, 0x5f);
            this.setClientPortcb.Name = "setClientPortcb";
            this.setClientPortcb.Size = new Size(0x6c, 0x10);
            this.setClientPortcb.TabIndex = 3;
            this.setClientPortcb.Text = "指定本地端口：";
            this.setClientPortcb.UseVisualStyleBackColor = true;
            this.setClientPortcb.CheckedChanged += new EventHandler(this.setClientPortcb_CheckedChanged);
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.ForeColor = Color.Black;
            this.label3.Location = new Point(260, 0x60);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x59, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "连接超时(秒)：";
            this.maxWaitTimetb.Location = new Point(0x163, 0x5d);
            this.maxWaitTimetb.Name = "maxWaitTimetb";
            this.maxWaitTimetb.Size = new Size(0x3b, 0x15);
            this.maxWaitTimetb.TabIndex = 2;
            this.autoConncb.AutoSize = true;
            this.autoConncb.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.autoConncb.ForeColor = Color.Black;
            this.autoConncb.Location = new Point(0x19, 0x80);
            this.autoConncb.Name = "autoConncb";
            this.autoConncb.Size = new Size(0xcc, 0x10);
            this.autoConncb.TabIndex = 5;
            this.autoConncb.Text = "开票软件启动时自动登录到服务器";
            this.autoConncb.UseVisualStyleBackColor = true;
            this.button1.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.button1.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.button1.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.button1.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.button1.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.button1.Location = new Point(0xc3, 0xd5);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x3f, 0x17);
            this.button1.TabIndex = 7;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.groupBox1.Controls.Add(this.serverIPtb);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.serverPorttb);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox1.ForeColor = Color.FromArgb(2, 0x3d, 0x7f);
            this.groupBox1.Location = new Point(0x19, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x199, 0x40);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器信息";
            this.label5.AutoSize = true;
            this.label5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label5.ForeColor = Color.Black;
            this.label5.Location = new Point(0x17, 0xa2);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x41, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "连接状态：";
            this.richTextBox1.BackColor = Color.White;
            this.richTextBox1.BorderStyle = BorderStyle.None;
            this.richTextBox1.Location = new Point(0x57, 0x9f);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new Size(0x147, 40);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoValidate = AutoValidate.Disable;
            base.ClientSize = new Size(0x1cd, 250);
            base.Controls.Add(this.richTextBox1);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.autoConncb);
            base.Controls.Add(this.maxWaitTimetb);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.setClientPortcb);
            base.Controls.Add(this.clientPorttb);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ConfigForm";
            base.TabText = "公告属性配置";
            this.Text = "公告属性配置";
            base.Load += new EventHandler(this.ConfigForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void setClientPortcb_CheckedChanged(object sender, EventArgs e)
        {
            if (this.setClientPortcb.Checked)
            {
                this.clientPorttb.Enabled = true;
            }
            else
            {
                this.clientPorttb.Text = "";
                this.clientPorttb.Enabled = false;
            }
        }

        private bool Validate()
        {
            if (this.serverIPtb.Text.Length == 0)
            {
                MessageManager.ShowMsgBox("C0003");
                this.serverIPtb.Focus();
                return false;
            }
            if (this.serverPorttb.Text.Length == 0)
            {
                MessageManager.ShowMsgBox("C0004");
                this.serverPorttb.Focus();
                return false;
            }
            if (this.maxWaitTimetb.Text.Length == 0)
            {
                MessageManager.ShowMsgBox("C0005");
                this.maxWaitTimetb.Focus();
                return false;
            }
            if (this.setClientPortcb.Checked && (this.clientPorttb.Text.Length == 0))
            {
                MessageManager.ShowMsgBox("C0006");
                this.clientPorttb.Focus();
                return false;
            }
            return true;
        }
    }
}

