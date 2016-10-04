namespace Aisino.Fwkp.Fpkj.Form.SendFP
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpkj.Common;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SendFaPiao : BaseForm
    {
        private string _Body = string.Empty;
        private string _FromEmail = string.Empty;
        private string _Subject = string.Empty;
        private string _ToEmail = string.Empty;
        private AisinoBTN btn_QueXiao;
        private AisinoBTN btn_ServerAttrituteSet;
        private IContainer components;
        private AisinoTXT FromEdit;
        private ILog loger = LogUtil.GetLogger<SendFaPiao>();
        private AisinoRTX Memo;
        private AisinoLBL NameLabel;
        private AisinoBTN OkBtn;
        private AisinoTXT RecipientsEdit;
        public static string strRecePersonAdressQueRen = "发送时进行收件人地址确认";
        public static string strSendEmailNum = "一次连接连续发送邮件数";
        public static string strSendEmailPersonAddress = "发件人邮件地址";
        public static string strSendEmailServerPassWord = "邮件发送服务器-密码";
        public static string strSendEmailServerRemberPassWord = "邮件发送服务器-记住密码";
        public static string strSendEmailServerShenFenYanZheng = "邮件发送服务器-我的服务器要求身份验证";
        public static string strSendEmailServerUser = "邮件发送服务器-账户名";
        private AisinoLBL SubjectEdit;
        private AisinoLBL TaxCodeLabel;
        private XmlComponentLoader xmlComponentLoader1;

        public SendFaPiao(string strRecipients, string strTaxCode, string strCorporation, string strFrom, int iFpNum)
        {
            try
            {
                this.Initialize();
                this.RecipientsEdit.Text = strRecipients.Trim();
                this.FromEdit.Text = strFrom.Trim();
                this.NameLabel.Text = strCorporation.Trim();
                this.TaxCodeLabel.Text = strTaxCode.Trim();
                this.SubjectEdit.Text = "HTXXFWSKKP_FPRZJK_XML";
                this.Memo.Text = "接收方企业信息\r\n企业税号：" + strTaxCode + "\r\n企业名称：" + strCorporation + "\r\n\r\n发送方企业信息\r\n企业税号：" + base.TaxCardInstance.TaxCode.Trim() + "\r\n企业名称：" + base.TaxCardInstance.Corporation.Trim() + "\r\n发票份数：" + Convert.ToString(iFpNum) + " 份";
                this.EnabValidate();
                this.GetValue();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void btn_QueXiao_Clik(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void btn_ServerAttrituteSet_Clik(object sender, EventArgs e)
        {
            try
            {
                EmailCanShuSet set = new EmailCanShuSet {
                    Owner = this,
                    StartPosition = FormStartPosition.CenterParent
                };
                if (DialogResult.OK == set.ShowDialog())
                {
                    this.FromEdit.Text = PropertyUtil.GetValue("SMTP_USER").Trim();
                }
                this.GetValue();
                base.Visible = true;
                this.Refresh();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
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

        private void EnabValidate()
        {
            try
            {
                this.OkBtn.Enabled = ShareMethods.CheckMailAddr(this.RecipientsEdit.Text.Trim()) && ShareMethods.CheckMailAddr(this.FromEdit.Text.Trim());
                this.GetValue();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void FromEdit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnabValidate();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void GetValue()
        {
            try
            {
                this._FromEmail = this.FromEdit.Text.Trim();
                this._ToEmail = this.RecipientsEdit.Text.Trim();
                this._Subject = this.SubjectEdit.Text.Trim();
                this._Body = this.Memo.Text.Trim();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.RecipientsEdit = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("RecipientsEdit");
            this.FromEdit = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("FromEdit");
            this.NameLabel = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("NameLabel");
            this.TaxCodeLabel = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("TaxCodeLabel");
            this.Memo = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("Memo");
            this.Memo.ReadOnly = true;
            this.SubjectEdit = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("SubjectEdit");
            this.SubjectEdit.Text = "HTXXFWSKKP_FPRZJK_XML";
            this.OkBtn = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("OkBtn");
            this.btn_QueXiao = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_QueXiao");
            this.btn_ServerAttrituteSet = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_ServerAttrituteSet");
            this.OkBtn.Click += new EventHandler(this.OkBtn_Clik);
            this.btn_QueXiao.Click += new EventHandler(this.btn_QueXiao_Clik);
            this.btn_ServerAttrituteSet.Click += new EventHandler(this.btn_ServerAttrituteSet_Clik);
            this.RecipientsEdit.TextChanged += new EventHandler(this.RecipientsEdit_TextChanged);
            this.FromEdit.TextChanged += new EventHandler(this.FromEdit_TextChanged);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x199, 0x1c6);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.SendFP.SendFaPiao\Aisino.Fwkp.Fpkj.Form.SendFP.SendFaPiao.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x199, 0x1c6);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "SendFaPiao";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "发送邮件";
            base.ResumeLayout(false);
        }

        private void OkBtn_Clik(object sender, EventArgs e)
        {
            try
            {
                this.GetValue();
                PropertyUtil.SetValue(strSendEmailPersonAddress, this._FromEmail.Trim());
                base.Visible = false;
                this.Refresh();
                base.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void RecipientsEdit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnabValidate();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public string Body
        {
            get
            {
                return this._Body.Trim();
            }
        }

        public string FromEmail
        {
            get
            {
                return this._FromEmail.Trim();
            }
        }

        public string Subject
        {
            get
            {
                return this._Subject.Trim();
            }
        }

        public string ToEmail
        {
            get
            {
                return this._ToEmail.Trim();
            }
        }
    }
}

