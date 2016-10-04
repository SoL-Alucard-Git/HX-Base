namespace Aisino.Fwkp.Fpzpz.Form
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fpzpz.Common;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class KeMuMsgSet : BaseForm
    {
        private ILog _Loger = LogUtil.GetLogger<KeMuMsgSet>();
        private string _strA6KmStr = string.Empty;
        private AisinoBTN but_Ok;
        private AisinoBTN but_QuXiao;
        private IContainer components;
        private WebBrowser webBrowser1;
        private XmlComponentLoader xmlComponentLoader1;

        public KeMuMsgSet(string usrStr)
        {
            try
            {
                this.Initialize();
                this.webBrowser1.ScrollBarsEnabled = false;
                this.webBrowser1.ScriptErrorsSuppressed = false;
                this.webBrowser1.Navigate(usrStr);
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

        private void but_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                this._strA6KmStr = string.Empty;
                HtmlDocument document = this.webBrowser1.Document;
                if (null == document)
                {
                    this._Loger.Error("WebService出错。\n无法得到当前选择项。");
                    ExceptionHandler.HandleError(new Exception("WebService出错。\n无法得到当前选择项。"));
                }
                else if (null != document.ActiveElement)
                {
                    if (Aisino.Fwkp.Fpzpz.Common.Tool.IsA6Version())
                    {
                        HtmlElement elementById = document.GetElementById("selectedValue");
                        if (null != elementById)
                        {
                            this._strA6KmStr = elementById.InnerText;
                        }
                    }
                    else
                    {
                        this._strA6KmStr = document.ActiveElement.InnerText;
                    }
                    if (!string.IsNullOrEmpty(this._strA6KmStr) && (this._strA6KmStr.Length > 2))
                    {
                        this._strA6KmStr = this._strA6KmStr.Substring(1, this._strA6KmStr.Length - 1);
                        int index = this._strA6KmStr.IndexOf(0xff3d);
                        if (index.Equals(-1))
                        {
                            this._strA6KmStr = string.Empty;
                            this._Loger.Error("WebService出错。\n得到当前选择项格式错误。");
                            ExceptionHandler.HandleError(new Exception("WebService出错。\n得到当前选择项格式错误。"));
                            return;
                        }
                        this._strA6KmStr = this._strA6KmStr.Substring(0, index);
                    }
                    if (!string.IsNullOrEmpty(this._strA6KmStr))
                    {
                        base.DialogResult = DialogResult.OK;
                    }
                }
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

        private void but_QuXiao_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.Cancel;
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
            this.but_QuXiao = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_QuXiao");
            this.but_Ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_Ok");
            this.but_Ok.Click += new EventHandler(this.but_Ok_Click);
            this.but_QuXiao.Click += new EventHandler(this.but_QuXiao_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(KeMuMsgSet));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x35b, 0x26d);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fpzpz.Form.KeMuMsgSet\Aisino.Fwkp.Fpzpz.Form.KeMuMsgSet.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x35b, 0x26d);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "KeMuMsgSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "科目信息设置";
            base.ResumeLayout(false);
        }

        public string A6Km
        {
            get
            {
                return this._strA6KmStr;
            }
        }
    }
}

