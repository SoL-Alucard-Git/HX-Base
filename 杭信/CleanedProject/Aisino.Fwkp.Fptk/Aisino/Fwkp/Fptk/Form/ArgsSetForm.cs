namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Http;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;

    public class ArgsSetForm : BaseForm
    {
        public AisinoLBL aisinoLBL1;
        private AisinoBTN btnCancel;
        private AisinoBTN btnConnectTest;
        private AisinoBTN btnOk;
        private IContainer components;
        public AisinoLBL lblGfmc;
        private ILog loger = LogUtil.GetLogger<ArgsSetForm>();
        private AisinoTXT txtDk;
        private AisinoTXT txtFwqdz;

        public ArgsSetForm()
        {
            this.InitializeComponent();
            string str = PropertyUtil.GetValue("SWDK_SERVER");
            if (!string.IsNullOrEmpty(str))
            {
                this.txtFwqdz.Text = str.Substring(0, str.LastIndexOf(":"));
                this.txtDk.Text = str.Substring(str.LastIndexOf(":") + 1, (str.LastIndexOf("/") - str.LastIndexOf(":")) - 1);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnConnectTest_Click(object sender, EventArgs e)
        {
            this.testConnect(false);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string str = "";
            if (!this.testConnect(true))
            {
                MessageManager.ShowMsgBox("SWDK-0010");
            }
            else
            {
                str = this.txtFwqdz.Text + ":" + this.txtDk.Text + "/ReceiveWlbpServlet";
                PropertyUtil.SetValue("SWDK_SERVER", str);
                MessageManager.ShowMsgBox("SWDK-0011");
                base.Close();
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

        private string genTestXml()
        {
            string innerXml = "";
            XmlDocument document1 = new XmlDocument();
            XmlDeclaration newChild = document1.CreateXmlDeclaration("1.0", "UTF-8", null);
            document1.AppendChild(newChild);
            XmlNode node = document1.CreateElement("WLFPTEST");
            document1.AppendChild(node);
            XmlNode node2 = document1.CreateElement("MYTEST");
            node2.InnerText = "test";
            node.AppendChild(node2);
            innerXml = document1.InnerXml;
            document1.Save(@"d:\税务代开发票-Send-NetTest-test.xml");
            return innerXml;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ArgsSetForm));
            this.lblGfmc = new AisinoLBL();
            this.txtFwqdz = new AisinoTXT();
            this.aisinoLBL1 = new AisinoLBL();
            this.txtDk = new AisinoTXT();
            this.btnOk = new AisinoBTN();
            this.btnCancel = new AisinoBTN();
            this.btnConnectTest = new AisinoBTN();
            base.SuspendLayout();
            this.lblGfmc.AutoSize = true;
            this.lblGfmc.Font = new Font("微软雅黑", 10f);
            this.lblGfmc.Location = new Point(8, 40);
            this.lblGfmc.Name = "lblGfmc";
            this.lblGfmc.Size = new Size(0x4f, 20);
            this.lblGfmc.TabIndex = 10;
            this.lblGfmc.Text = "服务器地址";
            this.txtFwqdz.Location = new Point(0x5d, 40);
            this.txtFwqdz.Name = "txtFwqdz";
            this.txtFwqdz.Size = new Size(0x176, 0x15);
            this.txtFwqdz.TabIndex = 11;
            this.aisinoLBL1.AutoSize = true;
            this.aisinoLBL1.Font = new Font("微软雅黑", 10f);
            this.aisinoLBL1.Location = new Point(0x2c, 0x56);
            this.aisinoLBL1.Name = "aisinoLBL1";
            this.aisinoLBL1.Size = new Size(0x25, 20);
            this.aisinoLBL1.TabIndex = 12;
            this.aisinoLBL1.Text = "端口";
            this.txtDk.Location = new Point(0x5d, 0x56);
            this.txtDk.Name = "txtDk";
            this.txtDk.Size = new Size(0x41, 0x15);
            this.txtDk.TabIndex = 13;
            this.btnOk.BackColorActive=Color.FromArgb(0x19, 0x76, 210);
            this.btnOk.ColorDefaultA=Color.FromArgb(0, 0xac, 0xfb);
            this.btnOk.ColorDefaultB=Color.FromArgb(0, 0x91, 0xe0);
            this.btnOk.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btnOk.FontColor=Color.FromArgb(0xff, 0xff, 0xff);
            this.btnOk.ForeColor = Color.White;
            this.btnOk.Location = new Point(0x113, 0x83);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(0x4b, 30);
            this.btnOk.TabIndex = 0x10;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.btnCancel.BackColorActive=Color.FromArgb(0x19, 0x76, 210);
            this.btnCancel.ColorDefaultA=Color.FromArgb(0, 0xac, 0xfb);
            this.btnCancel.ColorDefaultB=Color.FromArgb(0, 0x91, 0xe0);
            this.btnCancel.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btnCancel.FontColor=Color.FromArgb(0xff, 0xff, 0xff);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(0x175, 0x83);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 30);
            this.btnCancel.TabIndex = 0x11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnConnectTest.BackColorActive=Color.FromArgb(0x19, 0x76, 210);
            this.btnConnectTest.ColorDefaultA=Color.FromArgb(0, 0xac, 0xfb);
            this.btnConnectTest.ColorDefaultB=Color.FromArgb(0, 0x91, 0xe0);
            this.btnConnectTest.Font = new Font("宋体", 9f);
            this.btnConnectTest.FontColor=Color.FromArgb(0xff, 0xff, 0xff);
            this.btnConnectTest.ForeColor = Color.White;
            this.btnConnectTest.Location = new Point(0xc1, 0x54);
            this.btnConnectTest.Name = "btnConnectTest";
            this.btnConnectTest.Size = new Size(0x4b, 0x17);
            this.btnConnectTest.TabIndex = 0x12;
            this.btnConnectTest.Text = "连接测试";
            this.btnConnectTest.UseVisualStyleBackColor = true;
            this.btnConnectTest.Click += new EventHandler(this.btnConnectTest_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1db, 0xad);
            base.Controls.Add(this.btnConnectTest);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOk);
            base.Controls.Add(this.txtDk);
            base.Controls.Add(this.aisinoLBL1);
            base.Controls.Add(this.txtFwqdz);
            base.Controls.Add(this.lblGfmc);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "ArgsSetForm";
            this.Text = "税务代开参数设置";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private string parseXML(XmlDocument xml)
        {
            return xml.SelectSingleNode("/TESTBACK").SelectSingleNode("RESULT").InnerText;
        }

        private bool testConnect(bool isSave)
        {
            bool flag = false;
            try
            {
                if ((this.txtFwqdz.Text.Length <= 0) || (this.txtDk.Text.Length <= 0))
                {
                    MessageManager.ShowMsgBox("SWDK-0012");
                    return flag;
                }
                int num = 0;
                string str = this.txtFwqdz.Text + ":" + this.txtDk.Text + "/ReceiveWlbpServlet";
                string xml = "";
                string str3 = "";
                byte[] bytes = ToolUtil.GetBytes(this.genTestXml());
                if ((bytes != null) && (bytes.Length != 0))
                {
                    str3 = Convert.ToBase64String(bytes);
                }
                string s = new WebClient().Post(str, str3, out num);
                if ((s != null) && (s.Length > 0))
                {
                    xml = ToolUtil.GetString(Convert.FromBase64String(s));
                }
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                if (this.parseXML(document) == "1")
                {
                    if (!isSave)
                    {
                        MessageManager.ShowMsgBox("SWDK-0013");
                    }
                    return true;
                }
                MessageManager.ShowMsgBox("SWDK-0014");
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("SWDK-0014");
                this.loger.Error(exception.Message);
            }
            return flag;
        }
    }
}

