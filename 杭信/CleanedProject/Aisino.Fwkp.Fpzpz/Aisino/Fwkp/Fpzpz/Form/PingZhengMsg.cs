namespace Aisino.Fwkp.Fpzpz.Form
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PingZhengMsg : Form
    {
        private ILog _Loger = LogUtil.GetLogger<PingZhengMsg>();
        private IContainer components;
        private WebBrowser webBrowser1;
        private XmlComponentLoader xmlComponentLoader1;

        public PingZhengMsg(string strTempUrlStr)
        {
            try
            {
                this.Initialize();
                base.MinimizeBox = true;
                base.MaximizeBox = true;
                base.WindowState = FormWindowState.Maximized;
                this.webBrowser1.Navigate(strTempUrlStr);
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.webBrowser1 = this.xmlComponentLoader1.GetControlByName<WebBrowser>("webBrowser1");
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(PingZhengMsg));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x2bb, 0x1c8);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fpzpz.Form.PingZhengMsg\Aisino.Fwkp.Fpzpz.Form.PingZhengMsg.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2bb, 0x1c8);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PingZhengMsg";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "凭证信息";
            base.FormClosed += new FormClosedEventHandler(this.PingZhengMsg_FormClosed);
            base.ResumeLayout(false);
        }

        private void PingZhengMsg_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this._Loger.Error("开始调用函数：closeERP");
                this.webBrowser1.Document.InvokeScript("closeERP");
                this._Loger.Error("结束调用函数：closeERP");
                this.webBrowser1.Dispose();
                this.webBrowser1 = null;
                base.Close();
            }
            catch (Exception exception)
            {
                this._Loger.Error("结束调用函数：PingZhengMsg_FormClosed:异常：" + exception.ToString());
            }
        }
    }
}

