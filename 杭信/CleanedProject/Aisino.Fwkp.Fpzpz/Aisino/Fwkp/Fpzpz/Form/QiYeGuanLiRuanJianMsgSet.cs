namespace Aisino.Fwkp.Fpzpz.Form
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Plugin.Core.WebService;
    using Aisino.Fwkp.Fpzpz.Common;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class QiYeGuanLiRuanJianMsgSet : BaseForm
    {
        private string[] _A6SuitList = new string[0];
        private string[] _A6UserList = new string[0];
        private bool bClickGengHuan;
        private bool bGangGangShow = true;
        private AisinoBTN but_GengHuan;
        private AisinoBTN but_QueDing;
        private AisinoBTN but_QuXiao;
        private AisinoCMB combo_IP;
        private AisinoCMB combo_User;
        private AisinoCMB combo_ZhangTao;
        private IContainer components;
        private bool isSaved;
        private ILog loger = LogUtil.GetLogger<QiYeGuanLiRuanJianMsgSet>();
        private XmlComponentLoader xmlComponentLoader1;

        public QiYeGuanLiRuanJianMsgSet()
        {
            try
            {
                this.bGangGangShow = true;
                this.Initialize();
                this.isSaved = false;
                this.PzInfoInit();
                this.but_GengHuan.Enabled = false;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private string A6GetSuit(string urls)
        {
            try
            {
                if (urls.Trim().Length <= 0)
                {
                    return MessageManager.GetMessageInfo(DingYiZhiFuChuan.ServerEmpty);
                }
                string str = urls + "/pzWebService.ws";
                object obj2 = new object();
                string str2 = "getAccount";
                string[] strArray = new string[0];
                string[] strArray2 = (string[]) WebServiceFactory.InvokeWebService(str, str2, strArray);
                if (strArray2.Length <= 0)
                {
                    return DingYiZhiFuChuan.strErrLinkFailTip;
                }
                this._A6SuitList = strArray2;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                return DingYiZhiFuChuan.strErrLinkFailTip;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                return DingYiZhiFuChuan.strErrLinkFailTip;
            }
            return string.Empty;
        }

        private string A6GetUser(string urls, string suitNo)
        {
            try
            {
                if (urls.Trim().Length <= 0)
                {
                    return MessageManager.GetMessageInfo(DingYiZhiFuChuan.ServerEmpty);
                }
                if (suitNo.Trim().Length <= 0)
                {
                    return MessageManager.GetMessageInfo(DingYiZhiFuChuan.UserEmpty);
                }
                string str = urls + "/pzWebService.ws";
                string str2 = suitNo.Trim();
                if (str2.IndexOf("=") != -1)
                {
                    str2 = str2.Substring(0, str2.IndexOf("="));
                }
                object obj2 = new object();
                string str3 = "getUser";
                string[] strArray = new string[] { str2 };
                string[] strArray2 = (string[]) WebServiceFactory.InvokeWebService(str, str3, strArray);
                if (strArray2.Length <= 0)
                {
                    return DingYiZhiFuChuan.strErrLinkFailTip;
                }
                this._A6UserList = strArray2;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                return DingYiZhiFuChuan.strErrLinkFailTip;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                return DingYiZhiFuChuan.strErrLinkFailTip;
            }
            return string.Empty;
        }

        private string A6InfoInit()
        {
            try
            {
                if (this.combo_IP.Text.Trim().Length > 0)
                {
                    string iP = this.GetIP(this.combo_IP.Text.Trim());
                    string str2 = this.IsRightIP(iP);
                    if (!string.IsNullOrEmpty(str2))
                    {
                        this.combo_IP.Focus();
                        return str2;
                    }
                    string str3 = this.A6GetSuit(this.combo_IP.Text.Trim());
                    if (!string.IsNullOrEmpty(str3))
                    {
                        return str3;
                    }
                    if (this._A6SuitList.Length > 0)
                    {
                        this.combo_ZhangTao.Items.Clear();
                        foreach (string str4 in this._A6SuitList)
                        {
                            this.combo_ZhangTao.Items.Add(str4.Replace("#", "="));
                        }
                        this.combo_ZhangTao.SelectedIndex = 0;
                    }
                    if (this.combo_ZhangTao.Text.Trim().Length > 0)
                    {
                        string str5 = this.combo_ZhangTao.Text.Trim();
                        string suitNo = str5.Substring(0, str5.IndexOf("="));
                        if (string.IsNullOrEmpty(this.A6GetUser(this.combo_IP.Text.Trim(), suitNo)))
                        {
                            this.combo_User.Items.Clear();
                            foreach (string str8 in this._A6UserList)
                            {
                                this.combo_User.Items.Add(str8.Replace("#", "="));
                            }
                            this.combo_User.SelectedIndex = 0;
                        }
                        else
                        {
                            return DingYiZhiFuChuan.strErrLinkFailTip;
                        }
                    }
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                ExceptionHandler.HandleError(exception2);
            }
            return string.Empty;
        }

        private void but_GengHuan_Click(object sender, EventArgs e)
        {
            try
            {
                MessageHelper.MsgWait("正在更换服务器账套用户,请稍等...");
                this.bClickGengHuan = true;
                this.combo_ZhangTao.Items.Clear();
                this.combo_ZhangTao.Text = "";
                this.combo_User.Items.Clear();
                this.combo_User.Text = "";
                string str = this.A6InfoInit();
                if (!string.IsNullOrEmpty(str))
                {
                    Aisino.Fwkp.Fpzpz.Common.Tool.PzInterFaceLinkInfo(str, this.loger);
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            finally
            {
                MessageHelper.MsgWait();
            }
        }

        private void but_QueDing_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
                if (this.combo_IP.Text.Trim().Length <= 0)
                {
                    MessageManager.ShowMsgBox("FPZPZ-000005");
                }
                else if (this.combo_ZhangTao.Text.Trim().Length <= 0)
                {
                    MessageManager.ShowMsgBox("FPZPZ-000006");
                }
                else if (this.combo_User.Text.Trim().Length <= 0)
                {
                    MessageManager.ShowMsgBox("FPZPZ-000007");
                }
                else
                {
                    str = this.IsRightA6Info();
                    if (!string.IsNullOrEmpty(str))
                    {
                        Aisino.Fwkp.Fpzpz.Common.Tool.PzInterFaceLinkInfo(str, this.loger);
                    }
                    else
                    {
                        string str2 = PropertyUtil.GetValue(DingYiZhiFuChuan.A6ServerLinkUtil);
                        string str3 = PropertyUtil.GetValue(DingYiZhiFuChuan.A6SuitUtil);
                        string str4 = PropertyUtil.GetValue(DingYiZhiFuChuan.A6GuidUtil);
                        if ((!str2.Equals(this.combo_IP.Text.Trim()) || !str3.Equals(this.combo_ZhangTao.Text.Trim())) || !str4.Equals(this.combo_User.Text.Trim()))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.A6ServerLinkUtil, this.combo_IP.Text.Trim());
                            PropertyUtil.SetValue(DingYiZhiFuChuan.A6SuitUtil, this.combo_ZhangTao.Text.Trim());
                            PropertyUtil.SetValue(DingYiZhiFuChuan.A6GuidUtil, this.combo_User.Text.Trim());
                        }
                        DingYiZhiFuChuan.A6UserGuid = this.combo_User.Text.Trim();
                        DingYiZhiFuChuan.A6SuitGuid = this.combo_ZhangTao.Text.Trim();
                        DingYiZhiFuChuan.A6ServerLink = this.combo_IP.Text.Trim();
                        base.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void but_QuXiao_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void combo_IP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (PropertyUtil.GetValue(DingYiZhiFuChuan.A6ServerLinkUtil) != this.combo_IP.Text)
                {
                    this.but_GengHuan.Enabled = true;
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void combo_ZhangTao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.bGangGangShow)
                {
                    this.bGangGangShow = false;
                }
                else if (this.bClickGengHuan)
                {
                    this.bClickGengHuan = false;
                }
                else
                {
                    this.combo_User.Items.Clear();
                    this.combo_User.Text = "";
                    if (this.combo_ZhangTao.Text.Trim().Length > 0)
                    {
                        string str = this.combo_ZhangTao.Text.Trim();
                        string suitNo = str.Substring(0, str.IndexOf("="));
                        if (string.IsNullOrEmpty(this.A6GetUser(this.combo_IP.Text.Trim(), suitNo)))
                        {
                            foreach (string str4 in this._A6UserList)
                            {
                                this.combo_User.Items.Add(str4.Replace("#", "="));
                            }
                            this.combo_User.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
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

        public string GetIP(string strIp)
        {
            string str = string.Empty;
            string str2 = "http://";
            string str3 = "https://";
            try
            {
                string str4 = string.Empty;
                int length = 0;
                int index = 0;
                if (strIp.Length <= 0)
                {
                    return str;
                }
                if (strIp.IndexOf(str2) == 0)
                {
                    str4 = strIp.Substring(7, strIp.Length - 7);
                    length = str4.IndexOf(":");
                    index = str4.IndexOf("/");
                    if ((length > 0) && (index > 0))
                    {
                        str = str4.Substring(0, length);
                    }
                    return str;
                }
                if (strIp.IndexOf(str3) == 0)
                {
                    str4 = strIp.Substring(8, strIp.Length - 8);
                    length = str4.IndexOf(":");
                    index = str4.IndexOf("/");
                    if ((length > 0) && (index > 0))
                    {
                        str = str4.Substring(0, length);
                    }
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return str;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.combo_IP = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_Server");
            this.combo_User = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_User");
            this.combo_ZhangTao = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_ZhangTao");
            this.but_GengHuan = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button1");
            this.but_QueDing = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_QuDing");
            this.but_QuXiao = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_QuXiao");
            this.but_GengHuan.Click += new EventHandler(this.but_GengHuan_Click);
            this.but_QueDing.Click += new EventHandler(this.but_QueDing_Click);
            this.but_QuXiao.Click += new EventHandler(this.but_QuXiao_Click);
            this.combo_IP.TextChanged += new EventHandler(this.combo_IP_TextChanged);
            this.combo_IP.MaxLength = 60;
            this.combo_User.MaxLength = 20;
            this.combo_User.DropDownStyle = ComboBoxStyle.DropDownList;
            this.combo_ZhangTao.MaxLength = 20;
            this.combo_ZhangTao.SelectedIndexChanged += new EventHandler(this.combo_ZhangTao_SelectedIndexChanged);
            this.combo_ZhangTao.DropDownStyle = ComboBoxStyle.DropDownList;
            base.FormClosing += new FormClosingEventHandler(this.QiYeGuanLiRuanJianMsgSet_FormClosing);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1db, 0xca);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fpzpz.Form.QiYeGuanLiRuanJianMsgSet\Aisino.Fwkp.Fpzpz.Form.QiYeGuanLiRuanJianMsgSet.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1db, 0xca);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "QiYeGuanLiRuanJianMsgSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "企业管理软件信息设置";
            base.ResumeLayout(false);
        }

        public string IsRightA6Info()
        {
            try
            {
                if (this.combo_IP.Text.Trim().Length <= 0)
                {
                    return DingYiZhiFuChuan.strErrLinkFailTip;
                }
                string iP = this.GetIP(this.combo_IP.Text.Trim());
                string str2 = this.IsRightIP(iP);
                if (!string.IsNullOrEmpty(str2))
                {
                    return str2;
                }
                string str3 = this.A6GetSuit(this.combo_IP.Text.Trim());
                if (!string.IsNullOrEmpty(str3))
                {
                    return str3;
                }
                if (this._A6SuitList.Length > 0)
                {
                    string str4 = string.Empty;
                    foreach (string str5 in this._A6SuitList)
                    {
                        string str6 = str5.Replace("#", "=");
                        if (str6.Trim() == this.combo_ZhangTao.Text.Trim())
                        {
                            str4 = str6.Trim();
                        }
                    }
                    if (string.IsNullOrEmpty(str4))
                    {
                        return DingYiZhiFuChuan.strErrLinkFailTip;
                    }
                }
                if (this.combo_ZhangTao.Text.Trim().Length > 0)
                {
                    string str7 = this.combo_ZhangTao.Text.Trim();
                    string suitNo = str7.Substring(0, str7.IndexOf("="));
                    if (string.IsNullOrEmpty(this.A6GetUser(this.combo_IP.Text.Trim(), suitNo)))
                    {
                        string str10 = string.Empty;
                        foreach (string str11 in this._A6UserList)
                        {
                            string str12 = str11.Replace("#", "=");
                            if (str12.Trim() == this.combo_User.Text.Trim())
                            {
                                str10 = str12.Trim();
                            }
                        }
                        if (string.IsNullOrEmpty(str10))
                        {
                            return DingYiZhiFuChuan.strErrLinkFailTip;
                        }
                    }
                    else
                    {
                        return DingYiZhiFuChuan.strErrLinkFailTip;
                    }
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                return DingYiZhiFuChuan.strErrLinkFailTip;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                return DingYiZhiFuChuan.strErrLinkFailTip;
            }
            return string.Empty;
        }

        public string IsRightIP(string strIp)
        {
            string str = "请输入正确的服务器地址!";
            try
            {
                if (!(strIp.Trim() != string.Empty))
                {
                    return str;
                }
                if (strIp.IndexOf("。") > 0)
                {
                    return str;
                }
                return string.Empty;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return str;
        }

        public bool PzInfoInit()
        {
            bool flag = true;
            try
            {
                this.combo_IP.Items.Clear();
                this.combo_IP.Text = string.Empty;
                this.combo_ZhangTao.Items.Clear();
                this.combo_ZhangTao.Text = string.Empty;
                this.combo_User.Items.Clear();
                this.combo_User.Text = string.Empty;
                string item = PropertyUtil.GetValue(DingYiZhiFuChuan.A6ServerLinkUtil);
                DingYiZhiFuChuan.A6ServerLink = item.Trim();
                if (item.Trim().Length > 0)
                {
                    this.combo_IP.Items.Add(item);
                    this.combo_IP.Text = item;
                    this.isSaved = true;
                }
                else
                {
                    this.isSaved = false;
                }
                string str2 = PropertyUtil.GetValue(DingYiZhiFuChuan.A6SuitUtil);
                DingYiZhiFuChuan.A6SuitGuid = str2.Trim();
                if (str2.Trim().Length > 0)
                {
                    this.combo_ZhangTao.Items.Add(str2);
                    this.combo_ZhangTao.SelectedIndex = 0;
                    this.isSaved = true;
                }
                else
                {
                    this.bGangGangShow = false;
                    this.isSaved = false;
                }
                string str3 = PropertyUtil.GetValue(DingYiZhiFuChuan.A6GuidUtil);
                DingYiZhiFuChuan.A6UserGuid = str3.Trim();
                if (str3.Trim().Length > 0)
                {
                    this.combo_User.Items.Add(str3);
                    this.combo_User.SelectedIndex = 0;
                    this.isSaved = true;
                    return flag;
                }
                this.isSaved = false;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return flag;
        }

        private void QiYeGuanLiRuanJianMsgSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            PropertyUtil.Save();
        }
    }
}

