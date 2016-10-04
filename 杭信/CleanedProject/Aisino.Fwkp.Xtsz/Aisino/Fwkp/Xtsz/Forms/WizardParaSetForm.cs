namespace Aisino.Fwkp.Xtsz.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.Xtsz;
    using Aisino.Fwkp.Xtsz.BLL;
    using Aisino.Fwkp.Xtsz.Common;
    using Aisino.Fwkp.Xtsz.Model;
    using log4net;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class WizardParaSetForm : BaseForm
    {
        private AisinoLBL aisinoLBL10;
        private AisinoLBL aisinoLBL11;
        private AisinoLBL aisinoLBL2 = new AisinoLBL();
        private AisinoLBL aisinoLBL3 = new AisinoLBL();
        private AisinoLBL aisinoLBL5;
        private AisinoLBL aisinoLBL6;
        private AisinoLBL aisinoLBL7;
        private AisinoLBL aisinoLBL8;
        private AisinoLBL aisinoLBL9;
        private ButtonPw btnNext = new ButtonPw();
        private ButtonPw btnPre = new ButtonPw();
        private ButtonPw btnPSCancel = new ButtonPw();
        private ButtonPw btnPSOK = new ButtonPw();
        private ButtonPw btnTestUrl = new ButtonPw();
        private CheckBox chkBoxAuthConfirm;
        private CheckBox chkBoxProxy;
        private IContainer components;
        private CommFun.CorporationInfo corporationInfo = new CommFun.CorporationInfo();
        private GroupBox groupBox1;
        private GroupBox groupBox4;
        private bool isInitialFormFlag = true;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label7;
        private Label label8;
        private AisinoLBL labelCorporationName = new AisinoLBL();
        private AisinoLBL labelDateYear = new AisinoLBL();
        private AisinoLBL labelMonth = new AisinoLBL();
        private AisinoLBL labelName = new AisinoLBL();
        private AisinoLBL labelTaxCode = new AisinoLBL();
        private AisinoLBL labelYear = new AisinoLBL();
        private AisinoLBL labelZGJGDM = new AisinoLBL();
        private AisinoLBL labelZGJGMC = new AisinoLBL();
        private AisinoLBL lblDataSize;
        private AisinoLBL lblRecvPlatform;
        private AisinoLBL lblUploadByNum;
        private AisinoLBL lblUploadByNum1;
        private AisinoLBL lblUploadByTime;
        private AisinoLBL lblUploadByTime1;
        private AisinoLBL lblUploadNow;
        private ILog loger = LogUtil.GetLogger<WizardParaSetForm>();
        public bool m_bInitial;
        private DZDZInfoModel m_dzdzInfoModel = new DZDZInfoModel();
        private ParaSetBLL m_ParaSetBLL = new ParaSetBLL();
        private string m_strManagerName = "";
        private TaxEntityBLL m_taxEntityBLL = new TaxEntityBLL();
        private SysTaxInfoModel m_TaxModel = new SysTaxInfoModel();
        private PictureBox pictureBox1;
        private RadioButton radioButtonBasic;
        private RadioButton radioButtonHttp;
        private RadioButton radioButtonNTLM;
        private RadioButton radioButtonSocks;
        private AisinoRDO radioButtonStart = new AisinoRDO();
        private AisinoRDO radioButtonStop = new AisinoRDO();
        private AisinoRTX richTextBoxBankAccount = new AisinoRTX();
        private AisinoSPL splitContainer1 = new AisinoSPL();
        private AisinoSPL splitContainer2 = new AisinoSPL();
        private TabControlPwSkin tabCtrlFoundation = new TabControlPwSkin();
        private TabPage tabPageBaseInfo = new TabPage();
        private TabPage tabPageDone = new TabPage();
        private TabPage tabPageDZDZ;
        private TabPage tabPageInitial = new TabPage();
        private TabPage tabPagePZ = new TabPage();
        private TabPage tabPageWelcome = new TabPage();
        private AisinoTXT textBoxAccumulateNum;
        private AisinoTXT textBoxConfirmPasswd = new AisinoTXT();
        private AisinoTXT textBoxCorprationAddress = new AisinoTXT();
        private AisinoTXT textBoxIntervalTime;
        private AisinoTXT textBoxMonth = new AisinoTXT();
        private AisinoTXT textBoxName = new AisinoTXT();
        private TextBox textBoxProxyAddr;
        private TextBox textBoxProxyPasswd;
        private TextBox textBoxProxyPort;
        private TextBox textBoxProxyUserName;
        private AisinoTXT textBoxRecvPlatformAddr;
        private AisinoTXT textBoxTelephone = new AisinoTXT();
        private AisinoTXT textBoxUserPasswd = new AisinoTXT();
        private AisinoTXT textBoxYear = new AisinoTXT();
        private TreeNode treeNodeInitial = new TreeNode();
        private TreeNode treeNodeRoot = new TreeNode();
        private XmlComponentLoader xmlComponentLoaderParaSet;

        public WizardParaSetForm()
        {
            this.Initial();
            try
            {
                this.corporationInfo = this.m_ParaSetBLL.GetCorporationInfo();
                this.m_bInitial = this.IsInitialed();
            }
            catch (Exception)
            {
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.tabCtrlFoundation.SelectedTab == this.tabPageInitial)
            {
                if (this.textBoxName.Text.ToString().Trim() != "")
                {
                    if ((!this.IsInitialed() || this.IsParamEmpty()) && (this.textBoxName.Text.ToString().Trim() == "管理员"))
                    {
                        this.textBoxName.Focus();
                        MessageManager.ShowMsgBox("INP-233114");
                        return;
                    }
                }
                else
                {
                    this.tabCtrlFoundation.SelectedTab = this.tabPageInitial;
                    this.textBoxName.Focus();
                    MessageBoxHelper.Show("请输入系统主管名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                if (this.textBoxUserPasswd.Text.Trim().Length < 4)
                {
                    MessageBoxHelper.Show("密码长度至少4位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.textBoxUserPasswd.Focus();
                    return;
                }
            }
            if (this.tabCtrlFoundation.SelectedTab == this.tabPageBaseInfo)
            {
                if (this.textBoxCorprationAddress.Text.Trim() == "")
                {
                    MessageManager.ShowMsgBox("INP-233101");
                    if (this.tabCtrlFoundation.SelectedTab != this.tabPageBaseInfo)
                    {
                        if (!this.tabCtrlFoundation.TabPages.Contains(this.tabPageBaseInfo))
                        {
                            this.InitTabs();
                        }
                        this.tabCtrlFoundation.SelectedTab = this.tabPageBaseInfo;
                    }
                    this.textBoxCorprationAddress.Focus();
                    return;
                }
                if (this.textBoxTelephone.Text.Trim() == "")
                {
                    MessageManager.ShowMsgBox("INP-233102");
                    if (this.tabCtrlFoundation.SelectedTab != this.tabPageBaseInfo)
                    {
                        if (!this.tabCtrlFoundation.TabPages.Contains(this.tabPageBaseInfo))
                        {
                            this.InitTabs();
                        }
                        this.tabCtrlFoundation.SelectedTab = this.tabPageBaseInfo;
                    }
                    this.textBoxTelephone.Focus();
                    return;
                }
            }
            if ((this.tabCtrlFoundation.SelectedTab != this.tabPageDZDZ) || this.CheckCtrlsInDZDZPage())
            {
                if (this.tabCtrlFoundation.SelectedIndex != (this.tabCtrlFoundation.TabPages.Count - 1))
                {
                    this.tabCtrlFoundation.SelectedIndex++;
                }
                this.UpdateLbls();
                this.UpdateBtns();
            }
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (this.tabCtrlFoundation.SelectedIndex != 0)
            {
                this.tabCtrlFoundation.SelectedIndex--;
            }
            this.UpdateLbls();
            this.UpdateBtns();
        }

        private void btnPSCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnPSOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_bInitial = this.RunInitial();
                if (this.m_bInitial)
                {
                    this.SetInitialStatus("0");
                    if (!this.SetAccountDate(base.TaxCardInstance.get_CardEffectDate()))
                    {
                        this.loger.Error("Set Account Date failed");
                        MessageManager.ShowMsgBox("INP-233116");
                    }
                    else
                    {
                        string str = "";
                        string[] separator = new string[] { "\n", Environment.NewLine };
                        string[] strArray2 = this.richTextBoxBankAccount.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < strArray2.Length; i++)
                        {
                            str = str + strArray2[i] + Environment.NewLine;
                        }
                        str = str.Trim();
                        if (this.corporationInfo == null)
                        {
                            MessageManager.ShowMsgBox("INP-233110");
                        }
                        else
                        {
                            this.m_TaxModel.YYDZ = this.textBoxCorprationAddress.Text;
                            this.m_TaxModel.DHHM = this.textBoxTelephone.Text;
                            this.m_TaxModel.YHZH = str;
                            this.m_TaxModel.QYBH = this.corporationInfo.m_strSignCode;
                            if (!this.m_ParaSetBLL.UpdateSysTaxInfo(this.m_TaxModel))
                            {
                                this.loger.Error("update infomation failed");
                                MessageManager.ShowMsgBox("INP-233108");
                            }
                            else if (!this.m_ParaSetBLL.WriteTaxCardInfo())
                            {
                                this.loger.Error("write card failed");
                                MessageManager.ShowMsgBox("INP-233109");
                            }
                            else
                            {
                                if (this.tabPagePZ == this.tabCtrlFoundation.SelectedTab)
                                {
                                    if (this.radioButtonStart.Checked)
                                    {
                                        PropertyUtil.SetValue("启用停用发票转凭证接口", "1");
                                        PropertyUtil.Save();
                                    }
                                    else
                                    {
                                        PropertyUtil.SetValue("启用停用发票转凭证接口", "0");
                                        PropertyUtil.Save();
                                    }
                                }
                                this.InDZDZInfoModel();
                                if (!Config.CreateDZDZXML(this.m_dzdzInfoModel))
                                {
                                    this.loger.Error("update DZDZInfo failed!");
                                }
                                else
                                {
                                    base.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-233107", new string[] { exception.Message });
            }
        }

        private void btnTestUrl_Click(object sender, EventArgs e)
        {
            if (this.CheckCtrlsInDZDZPage())
            {
                string message = string.Empty;
                string str2 = this.textBoxRecvPlatformAddr.Text.Trim();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("ACCEPT_WEB_SERVER", str2);
                if (this.chkBoxProxy.Checked)
                {
                    dictionary.Add("proxyType", this.radioButtonHttp.Checked ? "HTTP" : "SOCKS");
                    dictionary.Add("proxyHost", this.textBoxProxyAddr.Text.Trim());
                    dictionary.Add("proxyPort", this.textBoxProxyPort.Text.Trim());
                }
                else
                {
                    dictionary.Add("proxyType", "");
                    dictionary.Add("proxyHost", "");
                    dictionary.Add("proxyPort", "");
                }
                if (this.chkBoxAuthConfirm.Enabled && this.chkBoxAuthConfirm.Checked)
                {
                    dictionary.Add("proxyAuthType", this.radioButtonBasic.Checked ? "BASIC" : "NTLM");
                    dictionary.Add("proxyAuthUser", this.textBoxProxyUserName.Text.Trim());
                    dictionary.Add("proxyAuthPassword", this.textBoxProxyPasswd.Text.Trim());
                }
                else
                {
                    dictionary.Add("proxyAuthType", "");
                    dictionary.Add("proxyAuthUser", "");
                    dictionary.Add("proxyAuthPassword", "");
                }
                try
                {
                    if (string.IsNullOrEmpty(str2))
                    {
                        MessageBoxHelper.Show("请输入服务器地址！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        int num = HttpsSender.TestConnect(dictionary, ref message);
                        if (num == 0)
                        {
                            MessageBoxHelper.Show("连接成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            this.loger.Error(message);
                            MessageBoxHelper.Show("连接失败！\n错误号：" + num.ToString() + "\n错误信息：" + message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.loger.Error(exception.Message);
                }
            }
        }

        private bool CheckCtrlsInDZDZPage()
        {
            if (this.textBoxRecvPlatformAddr.Text.Trim() == "")
            {
                MessageManager.ShowMsgBox("INP-234101");
                this.textBoxRecvPlatformAddr.Focus();
                return false;
            }
            if (this.chkBoxProxy.Checked && ((this.textBoxProxyAddr.Text.Trim() == "") || (this.textBoxProxyPort.Text.Trim() == "")))
            {
                MessageBoxHelper.Show("代理服务器地址及端口均不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if ((this.chkBoxAuthConfirm.Enabled && this.chkBoxAuthConfirm.Checked) && (this.textBoxProxyUserName.Text.Trim() == ""))
            {
                MessageBoxHelper.Show("身份认证用户名不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            return true;
        }

        private void CheckNameAndPwd()
        {
            try
            {
                string str = this.textBoxName.Text.Trim();
                string str2 = this.textBoxUserPasswd.Text.Trim();
                string str3 = this.textBoxConfirmPasswd.Text.Trim();
                if (this.tabCtrlFoundation.SelectedTab == this.tabPageInitial)
                {
                    if (!this.IsInitialed() || this.IsParamEmpty())
                    {
                        if (((str2.Length > 0) && (str3.Length > 0)) && (!string.IsNullOrEmpty(str) && (str2 == str3)))
                        {
                            this.btnPSOK.Enabled = true;
                            this.btnNext.Enabled = true;
                        }
                        else
                        {
                            this.btnPSOK.Enabled = false;
                            this.btnNext.Enabled = false;
                        }
                    }
                    else if ((str == UserInfo.get_Yhmc()) && this.m_taxEntityBLL.IsMatch(this.textBoxName.Text.Trim(), this.textBoxUserPasswd.Text.Trim()))
                    {
                        this.btnPSOK.Enabled = true;
                        this.btnNext.Enabled = true;
                    }
                    else
                    {
                        this.btnPSOK.Enabled = false;
                        this.btnNext.Enabled = false;
                    }
                }
                else
                {
                    this.btnPSOK.Enabled = true;
                    this.btnNext.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-231101", new string[] { exception.Message });
            }
        }

        private void chkBoxAuthConfirm_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            if (!box.Checked)
            {
                this.aisinoLBL6.Enabled = false;
                this.aisinoLBL7.Enabled = false;
                this.aisinoLBL8.Enabled = false;
                this.radioButtonBasic.Enabled = false;
                this.radioButtonNTLM.Enabled = false;
                this.textBoxProxyUserName.Enabled = false;
                this.textBoxProxyPasswd.Enabled = false;
            }
            else
            {
                this.aisinoLBL6.Enabled = true;
                this.aisinoLBL7.Enabled = true;
                this.aisinoLBL8.Enabled = true;
                this.radioButtonBasic.Enabled = true;
                this.radioButtonNTLM.Enabled = true;
                this.textBoxProxyUserName.Enabled = true;
                this.textBoxProxyPasswd.Enabled = true;
            }
        }

        private void chkBoxAuthConfirm_EnabledChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            if (!box.Enabled)
            {
                this.aisinoLBL6.Enabled = false;
                this.aisinoLBL7.Enabled = false;
                this.aisinoLBL8.Enabled = false;
                this.radioButtonBasic.Enabled = false;
                this.radioButtonNTLM.Enabled = false;
                this.textBoxProxyUserName.Enabled = false;
                this.textBoxProxyPasswd.Enabled = false;
            }
            else if (box.Enabled && !box.Checked)
            {
                this.aisinoLBL6.Enabled = false;
                this.aisinoLBL7.Enabled = false;
                this.aisinoLBL8.Enabled = false;
                this.radioButtonBasic.Enabled = false;
                this.radioButtonNTLM.Enabled = false;
                this.textBoxProxyUserName.Enabled = false;
                this.textBoxProxyPasswd.Enabled = false;
            }
            else if (box.Enabled && box.Checked)
            {
                this.aisinoLBL6.Enabled = true;
                this.aisinoLBL7.Enabled = true;
                this.aisinoLBL8.Enabled = true;
                this.radioButtonBasic.Enabled = true;
                this.radioButtonNTLM.Enabled = true;
                this.textBoxProxyUserName.Enabled = true;
                this.textBoxProxyPasswd.Enabled = true;
            }
        }

        private void chkBoxProxy_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            if (!box.Checked)
            {
                this.aisinoLBL6.Enabled = false;
                this.aisinoLBL7.Enabled = false;
                this.aisinoLBL8.Enabled = false;
                this.aisinoLBL9.Enabled = false;
                this.aisinoLBL10.Enabled = false;
                this.aisinoLBL11.Enabled = false;
                this.radioButtonHttp.Enabled = false;
                this.radioButtonSocks.Enabled = false;
                this.textBoxProxyAddr.Enabled = false;
                this.textBoxProxyPort.Enabled = false;
                this.chkBoxAuthConfirm.Enabled = false;
            }
            else
            {
                this.aisinoLBL6.Enabled = true;
                this.aisinoLBL7.Enabled = true;
                this.aisinoLBL8.Enabled = true;
                this.aisinoLBL9.Enabled = true;
                this.aisinoLBL10.Enabled = true;
                this.aisinoLBL11.Enabled = true;
                this.radioButtonHttp.Enabled = true;
                this.radioButtonSocks.Enabled = true;
                this.textBoxProxyAddr.Enabled = true;
                this.textBoxProxyPort.Enabled = true;
                this.chkBoxAuthConfirm.Enabled = true;
            }
        }

        private bool CleanupData()
        {
            try
            {
                return this.m_taxEntityBLL.CleanTable();
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string GetAccountDate()
        {
            return Config.GetAccountDate();
        }

        private void InDZDZInfoModel()
        {
            this.m_dzdzInfoModel.AcceptWebServer = this.textBoxRecvPlatformAddr.Text.Trim();
            this.m_dzdzInfoModel.IsUseProxy = this.chkBoxProxy.Checked;
            this.m_dzdzInfoModel.ProxyType = this.radioButtonHttp.Checked ? "HTTP" : "SOCKS";
            this.m_dzdzInfoModel.ProxyHost = this.textBoxProxyAddr.Text;
            this.m_dzdzInfoModel.ProxyPort = this.textBoxProxyPort.Text;
            this.m_dzdzInfoModel.IsAuthConfirm = this.chkBoxAuthConfirm.Checked;
            this.m_dzdzInfoModel.ProxyAuthType = this.radioButtonBasic.Checked ? "BASIC" : "NTLM";
            this.m_dzdzInfoModel.ProxyAuthUser = this.textBoxProxyUserName.Text;
            this.m_dzdzInfoModel.ProxyAuthPassword = this.textBoxProxyPasswd.Text;
        }

        private void Initial()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Load += new EventHandler(this.ParaSetForm_Load);
            base.FormClosing += new FormClosingEventHandler(this.ParaSetForm_FormClosing);
            this.tabCtrlFoundation = this.xmlComponentLoaderParaSet.GetControlByName<TabControlPwSkin>("tabCtrlFoundation");
            this.tabCtrlFoundation.BringToFront();
            this.tabCtrlFoundation.Alignment = TabAlignment.Left;
            this.tabCtrlFoundation.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabCtrlFoundation.ItemSize = new Size(30, 130);
            this.tabCtrlFoundation.Location = new Point(-134, -5);
            this.tabCtrlFoundation.SizeMode = TabSizeMode.Fixed;
            this.tabCtrlFoundation.SelectedIndexChanged += new EventHandler(this.tabCtrlFoundation_SelectedIndexChanged);
            this.splitContainer1 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoSPL>("splitContainer1");
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.splitContainer2 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoSPL>("splitContainer2");
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Panel2Collapsed = true;
            this.pictureBox1 = this.xmlComponentLoaderParaSet.GetControlByName<PictureBox>("pictureBox1");
            this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            this.label7 = this.xmlComponentLoaderParaSet.GetControlByName<Label>("label7");
            this.label8 = this.xmlComponentLoaderParaSet.GetControlByName<Label>("label8");
            this.label10 = this.xmlComponentLoaderParaSet.GetControlByName<Label>("label10");
            this.label11 = this.xmlComponentLoaderParaSet.GetControlByName<Label>("label11");
            this.label12 = this.xmlComponentLoaderParaSet.GetControlByName<Label>("label12");
            this.btnPSOK = this.xmlComponentLoaderParaSet.GetControlByName<ButtonPw>("btnPSOK");
            this.btnPSOK.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnPSOK.Visible = true;
            this.btnPSOK.Click += new EventHandler(this.btnPSOK_Click);
            this.btnPSCancel = this.xmlComponentLoaderParaSet.GetControlByName<ButtonPw>("btnPSCancel");
            this.btnPSCancel.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnPSCancel.Visible = true;
            this.btnPSCancel.Click += new EventHandler(this.btnPSCancel_Click);
            this.btnPre = this.xmlComponentLoaderParaSet.GetControlByName<ButtonPw>("btnPre");
            this.btnPre.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnPre.Visible = true;
            this.btnPre.Click += new EventHandler(this.btnPre_Click);
            this.btnNext = this.xmlComponentLoaderParaSet.GetControlByName<ButtonPw>("btnNext");
            this.btnNext.ImageAlign = ContentAlignment.MiddleRight;
            this.btnNext.Visible = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.tabPageWelcome = this.xmlComponentLoaderParaSet.GetControlByName<TabPage>("tabPageWelcome");
            this.tabPageWelcome.BackColor = System.Drawing.Color.White;
            this.tabPageBaseInfo = this.xmlComponentLoaderParaSet.GetControlByName<TabPage>("tabPageBaseInfo");
            this.tabPageBaseInfo.BackColor = System.Drawing.Color.White;
            this.tabPagePZ = this.xmlComponentLoaderParaSet.GetControlByName<TabPage>("tabPagePZ");
            this.tabPagePZ.BackColor = System.Drawing.Color.White;
            this.tabPageInitial = this.xmlComponentLoaderParaSet.GetControlByName<TabPage>("tabPageInitial");
            this.tabPageInitial.BackColor = System.Drawing.Color.White;
            this.tabPageDone = this.xmlComponentLoaderParaSet.GetControlByName<TabPage>("tabPageDone");
            this.tabPageDone.BackColor = System.Drawing.Color.White;
            this.radioButtonStart = this.xmlComponentLoaderParaSet.GetControlByName<AisinoRDO>("radioButtonStart");
            this.radioButtonStart.Visible = true;
            this.radioButtonStart.Checked = true;
            this.radioButtonStop = this.xmlComponentLoaderParaSet.GetControlByName<AisinoRDO>("radioButtonStop");
            this.radioButtonStop.Visible = true;
            this.richTextBoxBankAccount = this.xmlComponentLoaderParaSet.GetControlByName<AisinoRTX>("richTextBoxBankAccount");
            this.richTextBoxBankAccount.Multiline = true;
            this.richTextBoxBankAccount.TextChanged += new EventHandler(this.richTextBoxBankAccount_TextChanged);
            this.textBoxCorprationAddress = this.xmlComponentLoaderParaSet.GetControlByName<AisinoTXT>("textBoxCorprationAddress");
            this.textBoxCorprationAddress.TextChanged += new EventHandler(this.textBoxCorprationAddress_TextChanged);
            this.textBoxTelephone = this.xmlComponentLoaderParaSet.GetControlByName<AisinoTXT>("textBoxTelephone");
            this.textBoxTelephone.TextChanged += new EventHandler(this.textBoxTelephone_TextChanged);
            this.textBoxName = this.xmlComponentLoaderParaSet.GetControlByName<AisinoTXT>("textBoxName");
            this.textBoxName.KeyPress += new KeyPressEventHandler(this.textBoxName_KeyPress);
            this.textBoxName.TextChanged += new EventHandler(this.textBoxName_TextChanged);
            this.textBoxUserPasswd = this.xmlComponentLoaderParaSet.GetControlByName<AisinoTXT>("textBoxUserPasswd");
            this.textBoxUserPasswd.PasswordChar = '*';
            this.textBoxUserPasswd.MaxLength = 0x10;
            this.textBoxUserPasswd.TextChanged += new EventHandler(this.textBoxUserPasswd_TextChanged);
            this.textBoxConfirmPasswd = this.xmlComponentLoaderParaSet.GetControlByName<AisinoTXT>("textBoxConfirmPasswd");
            this.textBoxConfirmPasswd.PasswordChar = '*';
            this.textBoxConfirmPasswd.MaxLength = 0x10;
            this.textBoxConfirmPasswd.TextChanged += new EventHandler(this.textBoxConfirmPasswd_TextChanged);
            this.textBoxConfirmPasswd.Visible = false;
            this.textBoxYear = this.xmlComponentLoaderParaSet.GetControlByName<AisinoTXT>("textBoxYear");
            this.textBoxYear.ReadOnly = true;
            this.textBoxYear.Enabled = false;
            this.textBoxMonth = this.xmlComponentLoaderParaSet.GetControlByName<AisinoTXT>("textBoxMonth");
            this.textBoxMonth.ReadOnly = true;
            this.textBoxMonth.Enabled = false;
            this.labelCorporationName = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("labelCorporationName");
            this.labelTaxCode = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("labelTaxCode");
            this.aisinoLBL2 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("aisinoLBL2");
            this.labelZGJGDM = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("labelZGJGDM");
            this.aisinoLBL3 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("aisinoLBL3");
            this.labelZGJGMC = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("labelZGJGMC");
            this.tabPageDZDZ = this.xmlComponentLoaderParaSet.GetControlByName<TabPage>("tabPageDZDZ");
            this.tabPageDZDZ.BackColor = System.Drawing.Color.White;
            this.groupBox1 = this.xmlComponentLoaderParaSet.GetControlByName<GroupBox>("groupBox1");
            this.lblDataSize = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("lblDataSize");
            this.lblDataSize.Visible = true;
            this.lblUploadByNum = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("lblUploadByNum");
            this.lblUploadByNum.Visible = true;
            this.lblUploadByTime = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("lblUploadByTime");
            this.lblUploadByTime.Visible = true;
            this.lblUploadNow = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("lblUploadNow");
            this.lblUploadNow.Visible = true;
            this.textBoxRecvPlatformAddr = this.xmlComponentLoaderParaSet.GetControlByName<AisinoTXT>("textBoxRecvPlatformAddr");
            this.lblRecvPlatform = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("lblRecvPlatform");
            this.textBoxIntervalTime = this.xmlComponentLoaderParaSet.GetControlByName<AisinoTXT>("textBoxIntervalTime");
            this.textBoxIntervalTime.Enabled = false;
            this.textBoxIntervalTime.TextAlign = HorizontalAlignment.Center;
            this.textBoxIntervalTime.Height = 14;
            this.textBoxIntervalTime.Visible = true;
            this.lblUploadByNum1 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("lblUploadByNum1");
            this.lblUploadByNum1.Visible = true;
            this.textBoxAccumulateNum = this.xmlComponentLoaderParaSet.GetControlByName<AisinoTXT>("textBoxAccumulateNum");
            this.textBoxAccumulateNum.Enabled = false;
            this.textBoxAccumulateNum.TextAlign = HorizontalAlignment.Center;
            this.textBoxAccumulateNum.Height = 14;
            this.textBoxAccumulateNum.Visible = true;
            this.lblUploadByTime1 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("lblUploadByTime1");
            this.lblUploadByTime1.Visible = true;
            this.aisinoLBL5 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("aisinoLBL5");
            this.aisinoLBL5.Visible = false;
            this.label14 = this.xmlComponentLoaderParaSet.GetControlByName<Label>("label14");
            this.label14.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.label15 = this.xmlComponentLoaderParaSet.GetControlByName<Label>("label15");
            this.label15.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.label16 = this.xmlComponentLoaderParaSet.GetControlByName<Label>("label16");
            this.labelYear = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("labelYear");
            this.labelMonth = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("labelMonth");
            this.btnTestUrl = this.xmlComponentLoaderParaSet.GetControlByName<ButtonPw>("btnTestUrl");
            this.btnTestUrl.Click += new EventHandler(this.btnTestUrl_Click);
            this.chkBoxProxy = this.xmlComponentLoaderParaSet.GetControlByName<CheckBox>("chkBoxProxy");
            this.chkBoxProxy.Visible = true;
            this.chkBoxProxy.CheckedChanged += new EventHandler(this.chkBoxProxy_CheckedChanged);
            this.radioButtonHttp = this.xmlComponentLoaderParaSet.GetControlByName<RadioButton>("radioButtonHttp");
            this.radioButtonHttp.Visible = true;
            this.radioButtonSocks = this.xmlComponentLoaderParaSet.GetControlByName<RadioButton>("radioButtonSocks");
            this.radioButtonSocks.Visible = true;
            this.textBoxProxyAddr = this.xmlComponentLoaderParaSet.GetControlByName<TextBox>("textBoxProxyAddr");
            this.textBoxProxyAddr.Visible = true;
            this.textBoxProxyPort = this.xmlComponentLoaderParaSet.GetControlByName<TextBox>("textBoxProxyPort");
            this.textBoxProxyPort.Visible = true;
            this.chkBoxAuthConfirm = this.xmlComponentLoaderParaSet.GetControlByName<CheckBox>("chkBoxAuthConfirm");
            this.chkBoxAuthConfirm.Visible = true;
            this.chkBoxAuthConfirm.CheckedChanged += new EventHandler(this.chkBoxAuthConfirm_CheckedChanged);
            this.chkBoxAuthConfirm.EnabledChanged += new EventHandler(this.chkBoxAuthConfirm_EnabledChanged);
            this.radioButtonBasic = this.xmlComponentLoaderParaSet.GetControlByName<RadioButton>("radioButtonBasic");
            this.radioButtonBasic.Visible = true;
            this.radioButtonNTLM = this.xmlComponentLoaderParaSet.GetControlByName<RadioButton>("radioButtonNTLM");
            this.radioButtonNTLM.Visible = true;
            this.textBoxProxyUserName = this.xmlComponentLoaderParaSet.GetControlByName<TextBox>("textBoxProxyUserName");
            this.textBoxProxyUserName.Visible = true;
            this.textBoxProxyPasswd = this.xmlComponentLoaderParaSet.GetControlByName<TextBox>("textBoxProxyPasswd");
            this.textBoxProxyPasswd.Visible = true;
            this.textBoxProxyPasswd.PasswordChar = '*';
            this.aisinoLBL6 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("aisinoLBL6");
            this.aisinoLBL7 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("aisinoLBL7");
            this.aisinoLBL8 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("aisinoLBL8");
            this.aisinoLBL9 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("aisinoLBL9");
            this.aisinoLBL10 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("aisinoLBL10");
            this.aisinoLBL11 = this.xmlComponentLoaderParaSet.GetControlByName<AisinoLBL>("aisinoLBL11");
            this.aisinoLBL6.Visible = true;
            this.aisinoLBL7.Visible = true;
            this.aisinoLBL8.Visible = true;
            this.aisinoLBL9.Visible = true;
            this.aisinoLBL10.Visible = true;
            this.aisinoLBL11.Visible = true;
            this.groupBox4 = this.xmlComponentLoaderParaSet.GetControlByName<GroupBox>("groupBox4");
            this.groupBox4.Visible = true;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(WizardParaSetForm));
            this.xmlComponentLoaderParaSet = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoaderParaSet.BackColor = System.Drawing.Color.Transparent;
            this.xmlComponentLoaderParaSet.Dock = DockStyle.Fill;
            this.xmlComponentLoaderParaSet.Location = new Point(0, 0);
            this.xmlComponentLoaderParaSet.Name = "xmlComponentLoaderParaSet";
            this.xmlComponentLoaderParaSet.Size = new Size(0x27a, 0x187);
            this.xmlComponentLoaderParaSet.TabIndex = 0;
            this.xmlComponentLoaderParaSet.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Xtsz.WizardParaSetForm\Aisino.Fwkp.Xtsz.WizardParaSetForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x27a, 0x187);
            base.Controls.Add(this.xmlComponentLoaderParaSet);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "WizardParaSetForm";
            this.Text = "系统参数设置";
            base.ResumeLayout(false);
        }

        private void InitLblsAndBtns()
        {
            this.btnPre.Visible = false;
            this.btnNext.Visible = true;
            this.btnNext.Enabled = true;
            if (!this.IsInitialed() || this.IsParamEmpty())
            {
                this.btnPSCancel.Visible = false;
                this.btnPre.Location = this.btnNext.Location;
                this.btnPSOK.Location = this.btnPSCancel.Location;
                this.btnNext.Location = this.btnPSCancel.Location;
            }
            this.label12.Font = new Font(this.label12.Font, FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label7.Font = new Font(this.label7.Font, FontStyle.Regular);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Enabled = false;
            this.label8.Font = new Font(this.label8.Font, FontStyle.Regular);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Enabled = false;
            this.label10.Font = new Font(this.label10.Font, FontStyle.Regular);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Enabled = false;
            this.label11.Font = new Font(this.label11.Font, FontStyle.Regular);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Enabled = false;
            if (base.TaxCardInstance.get_QYLX().ISTDQY)
            {
                this.pictureBox1.Image = Resources.无上传设置;
                this.label10.Visible = false;
                this.label11.Location = this.label10.Location;
                this.label11.Text = "3.完成";
            }
            else
            {
                this.pictureBox1.Image = Resources.有上传设置;
            }
            if (this.IsInitialed() && !this.IsParamEmpty())
            {
                this.textBoxName.Enabled = false;
            }
        }

        private void InitPwdTextBox()
        {
            if (!this.IsInitialed() || this.IsParamEmpty())
            {
                this.label16.Text = "管理员设置";
                this.label14.Text = "主管用户设置：";
                this.aisinoLBL5.Visible = true;
                this.textBoxConfirmPasswd.Visible = true;
                this.label15.Location = new Point(0x23, 0xc1);
                this.labelYear.Location = new Point(0x23, 0xe7);
                this.textBoxYear.Location = new Point(0x84, 0xe4);
                this.labelMonth.Location = new Point(220, 0xe7);
                this.textBoxMonth.Location = new Point(330, 0xe4);
                this.textBoxName.Enabled = true;
            }
        }

        private void InitTabs()
        {
            this.tabCtrlFoundation.TabPages.Clear();
            this.tabCtrlFoundation.TabPages.Add(this.tabPageWelcome);
            this.tabCtrlFoundation.TabPages.Add(this.tabPageInitial);
            this.tabCtrlFoundation.TabPages.Add(this.tabPageBaseInfo);
            if (!base.TaxCardInstance.get_QYLX().ISTDQY)
            {
                this.tabCtrlFoundation.TabPages.Add(this.tabPageDZDZ);
            }
            this.tabCtrlFoundation.TabPages.Add(this.tabPageDone);
            this.tabCtrlFoundation.SelectedTab = this.tabPageWelcome;
        }

        internal bool IsInitialed()
        {
            string str = PropertyUtil.GetValue("FullInstall");
            if ((str != null) && (str.Trim() == "0"))
            {
                return true;
            }
            object obj2 = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\航天信息\新版开票\安装", "FullInstall", "");
            if (obj2 != null)
            {
                string str2 = obj2.ToString();
                if (!string.IsNullOrEmpty(str2) && ("0" == str2))
                {
                    PropertyUtil.SetValue("FullInstall", "0");
                    PropertyUtil.Save();
                    return true;
                }
            }
            return false;
        }

        internal bool IsInitialTaxCodeInXTSWXX()
        {
            SysTaxInfoModel sysTaxInfoModel = new SysTaxInfoModel();
            this.m_ParaSetBLL.GetCorporationInfoFromDB("110101556688880", ref sysTaxInfoModel);
            return (!string.IsNullOrEmpty(sysTaxInfoModel.QYMC) && ("测试企业" == sysTaxInfoModel.QYMC));
        }

        internal bool IsParamEmpty()
        {
            SysTaxInfoModel sysTaxInfoModel = new SysTaxInfoModel();
            this.m_ParaSetBLL.GetCorporationInfoFromDB(this.corporationInfo.m_strSignCode, ref sysTaxInfoModel);
            return (string.IsNullOrEmpty(sysTaxInfoModel.DHHM) || string.IsNullOrEmpty(sysTaxInfoModel.YYDZ));
        }

        private void ParaSetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsInitialed() || this.IsParamEmpty())
            {
                MessageManager.ShowMsgBox("INP-111002");
                e.Cancel = true;
            }
        }

        private void ParaSetForm_Load(object sender, EventArgs e)
        {
            this.InitTabs();
            this.InitLblsAndBtns();
            this.InitPwdTextBox();
            string str = PropertyUtil.GetValue("启用停用发票转凭证接口");
            if (((str == null) || (str.Trim() == "")) || (str.Trim() == "0"))
            {
                this.radioButtonStop.Checked = true;
            }
            else
            {
                this.radioButtonStart.Checked = true;
            }
            try
            {
                if (!this.SetDZDZControlInitialStatus())
                {
                    this.tabCtrlFoundation.SelectedTab = this.tabPageDZDZ;
                }
                if (this.corporationInfo != null)
                {
                    this.SetControlInitialStatus();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox("INP-111001", "提示", new string[] { exception.Message });
            }
        }

        private void richTextBoxBankAccount_TextChanged(object sender, EventArgs e)
        {
            string[] lines = this.richTextBoxBankAccount.Lines;
            int selectionStart = this.richTextBoxBankAccount.SelectionStart;
            string str = string.Empty;
            bool flag = false;
            List<int> list = new List<int>();
            for (int i = 0; i < this.richTextBoxBankAccount.Lines.Length; i++)
            {
                if (ToolUtil.GetBytes(this.richTextBoxBankAccount.Lines[i]).Length > 100)
                {
                    list.Add(i);
                    flag = true;
                    lines[i] = StringUtils.GetSubString(this.richTextBoxBankAccount.Lines[i], 100);
                }
            }
            this.richTextBoxBankAccount.Lines = lines;
            if (flag)
            {
                this.richTextBoxBankAccount.SelectionStart = selectionStart - 1;
            }
            else
            {
                this.richTextBoxBankAccount.SelectionStart = selectionStart;
            }
            if (list.Count == 1)
            {
                MessageBoxHelper.Show("企业单条银行账号不能超过100个字符！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (list.Count > 1)
            {
                foreach (int num4 in list)
                {
                    str = str + "第" + ((num4 + 1)).ToString() + "行,";
                }
                MessageBoxHelper.Show(str.Substring(0, str.Length - 1) + "超过100个字符，已自动截取!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private bool RunInitial()
        {
            this.m_strManagerName = this.textBoxName.Text.Trim().ToString();
            string str = this.textBoxUserPasswd.Text.Trim();
            if (!this.IsInitialed() || this.IsParamEmpty())
            {
                if (string.IsNullOrEmpty(this.m_strManagerName) || string.IsNullOrEmpty(str))
                {
                    this.loger.Error("initial failed");
                    this.tabCtrlFoundation.SelectedTab = this.tabPageInitial;
                    this.textBoxName.Focus();
                    MessageBoxHelper.Show("用户名密码均不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                if (!this.SetAdminAndPwd(this.m_strManagerName, str))
                {
                    this.loger.Error("Set Manager Failed!");
                    MessageManager.ShowMsgBox("INP-231103");
                    return false;
                }
                this.m_taxEntityBLL.SetManagerName(this.m_strManagerName);
                UserLoginUtil.UpdateRemaindPasswrodByName(this.m_strManagerName);
                PropertyUtil.Save();
            }
            if (!this.m_taxEntityBLL.DataBack())
            {
                MessageManager.ShowMsgBox("INP-231104");
                return false;
            }
            if (!this.CleanupData())
            {
                MessageManager.ShowMsgBox("INP-231102");
                return false;
            }
            return true;
        }

        private bool SetAccountDate(string strValue)
        {
            return Config.SetAccountDate(strValue);
        }

        private bool SetAdminAndPwd(string strAdminName, string strPwd)
        {
            try
            {
                return this.m_taxEntityBLL.SetAdminAndPwd(strAdminName, strPwd);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox("INP-111001", "提示", new string[] { exception.Message });
            }
            return false;
        }

        private void SetControlInitialStatus()
        {
            try
            {
                this.m_taxEntityBLL.GetLoginName(ref this.m_strManagerName);
                this.textBoxName.Text = this.m_strManagerName;
                this.textBoxUserPasswd.Text = "";
                string accountDate = this.GetAccountDate();
                if (string.IsNullOrEmpty(accountDate) && !this.SetAccountDate(base.TaxCardInstance.get_CardEffectDate()))
                {
                    this.loger.Error("set account date failed");
                    MessageManager.ShowMsgBox("INP-233116");
                }
                else
                {
                    if (!string.IsNullOrEmpty(accountDate) && (accountDate.Length >= 6))
                    {
                        this.textBoxYear.Text = accountDate.Substring(0, 4);
                        this.textBoxMonth.Text = accountDate.Substring(4);
                    }
                    else
                    {
                        this.textBoxYear.Text = base.TaxCardInstance.get_CardEffectDate().Substring(0, 4);
                        this.textBoxMonth.Text = base.TaxCardInstance.get_CardEffectDate().Substring(4, 2);
                    }
                    this.labelCorporationName.Text = this.corporationInfo.m_strCorpName;
                    this.textBoxCorprationAddress.Text = this.corporationInfo.m_strAddress;
                    this.textBoxTelephone.Text = this.corporationInfo.m_strTelephone;
                    this.richTextBoxBankAccount.Text = this.corporationInfo.m_strBankAccount;
                    this.labelTaxCode.Text = this.corporationInfo.m_strSignCode;
                    string[] strArray = base.TaxCardInstance.get_SQInfo().ZGJGDMMC.Split(new char[] { ',' });
                    this.labelZGJGDM.Text = strArray[0];
                    this.labelZGJGMC.Text = strArray[1];
                    if (base.TaxCardInstance.get_SQInfo().DHYBZ == "H")
                    {
                        this.aisinoLBL2.Visible = false;
                        this.labelZGJGDM.Visible = false;
                        this.aisinoLBL3.Visible = false;
                        this.labelZGJGMC.Visible = false;
                    }
                    if ((this.m_ParaSetBLL.GetCorporationInfoFromDB(base.TaxCardInstance.get_TaxCode(), ref this.m_TaxModel) || this.m_ParaSetBLL.GetCorporationInfoFromDB(this.corporationInfo.m_strCorpCode, ref this.m_TaxModel)) || this.m_ParaSetBLL.GetCorporationInfoFromDB(this.corporationInfo.m_strSignCode, ref this.m_TaxModel))
                    {
                        this.textBoxCorprationAddress.Text = this.m_TaxModel.YYDZ;
                        this.textBoxTelephone.Text = this.m_TaxModel.DHHM;
                        this.richTextBoxBankAccount.Text = this.m_TaxModel.YHZH;
                    }
                    else
                    {
                        this.m_ParaSetBLL.AddSysTaxInfo(this.corporationInfo, ref this.m_TaxModel);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-233107", new string[] { exception.Message });
            }
        }

        private bool SetDZDZControlInitialStatus()
        {
            try
            {
                if (!Config.GetDZDZInfoFromXML(ref this.m_dzdzInfoModel))
                {
                    this.loger.Error("Initial DZDZ Controls failed!");
                    return false;
                }
                if (this.m_dzdzInfoModel != null)
                {
                    this.textBoxRecvPlatformAddr.Text = this.m_dzdzInfoModel.AcceptWebServer;
                    this.chkBoxProxy.Checked = this.m_dzdzInfoModel.IsUseProxy;
                    this.radioButtonHttp.Checked = this.m_dzdzInfoModel.ProxyType == "HTTP";
                    this.radioButtonSocks.Checked = this.m_dzdzInfoModel.ProxyType == "SOCKS";
                    this.textBoxProxyAddr.Text = this.m_dzdzInfoModel.ProxyHost;
                    this.textBoxProxyPort.Text = this.m_dzdzInfoModel.ProxyPort;
                    this.chkBoxAuthConfirm.Checked = this.m_dzdzInfoModel.IsAuthConfirm;
                    this.radioButtonBasic.Checked = this.m_dzdzInfoModel.ProxyAuthType == "BASIC";
                    this.radioButtonNTLM.Checked = this.m_dzdzInfoModel.ProxyAuthType == "NTLM";
                    this.textBoxProxyUserName.Text = this.m_dzdzInfoModel.ProxyAuthUser;
                    this.textBoxProxyPasswd.Text = this.m_dzdzInfoModel.ProxyAuthPassword;
                    EventArgs e = new EventArgs();
                    this.chkBoxProxy_CheckedChanged(this.chkBoxProxy, e);
                    this.chkBoxAuthConfirm_EnabledChanged(this.chkBoxAuthConfirm, e);
                    if (this.m_dzdzInfoModel.UploadNowFlag)
                    {
                        this.lblUploadNow.Enabled = true;
                        this.lblUploadByTime.Enabled = false;
                        this.lblUploadByTime1.Enabled = false;
                        this.lblUploadByNum.Enabled = false;
                        this.lblUploadByNum1.Enabled = false;
                    }
                    else
                    {
                        this.lblUploadNow.Enabled = false;
                        this.lblUploadByTime.Enabled = true;
                        this.lblUploadByTime1.Enabled = true;
                        this.lblUploadByNum.Enabled = true;
                        this.lblUploadByNum1.Enabled = true;
                    }
                    this.lblUploadNow.Text = this.m_dzdzInfoModel.UploadNowFlag ? "√ " : "   ";
                    this.lblUploadNow.Text = this.lblUploadNow.Text + "单张上传(每开一张发票即时上传)";
                    this.lblUploadByTime.Text = this.m_dzdzInfoModel.IntervalFlag ? "√ " : "   ";
                    this.lblUploadByTime.Text = this.lblUploadByTime.Text + "间隔";
                    this.textBoxIntervalTime.Text = (this.m_dzdzInfoModel.IntervalTime > 0) ? this.m_dzdzInfoModel.IntervalTime.ToString() : "";
                    this.lblUploadByNum.Text = this.m_dzdzInfoModel.AccumulateFlag ? "√ " : "   ";
                    this.lblUploadByNum.Text = this.lblUploadByNum.Text + "未上传张数达到";
                    this.textBoxAccumulateNum.Text = (this.m_dzdzInfoModel.AccumulateNum > 0) ? this.m_dzdzInfoModel.AccumulateNum.ToString() : "";
                    this.lblDataSize.Text = "   ";
                    this.lblDataSize.Text = this.lblDataSize.Text + "上传数据包大小: " + this.m_dzdzInfoModel.DataSize.ToString() + " 张/包";
                }
                return true;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error("Initial DZDZ Controls failed!");
                return false;
            }
        }

        internal bool SetInitialStatus(string strCode)
        {
            try
            {
                PropertyUtil.SetValue("FullInstall", strCode);
                PropertyUtil.Save();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void tabCtrlFoundation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateLbls();
            if (this.tabCtrlFoundation.SelectedTab == this.tabPageWelcome)
            {
                this.btnPre.Visible = false;
            }
            else
            {
                this.btnPre.Visible = true;
            }
            this.CheckNameAndPwd();
        }

        private void textBoxConfirmPasswd_TextChanged(object sender, EventArgs e)
        {
            if ((!this.IsInitialed() || this.IsParamEmpty()) && ((this.textBoxUserPasswd.Text.Trim().Length > 0) && (this.textBoxUserPasswd.Text.Trim() == this.textBoxConfirmPasswd.Text.Trim())))
            {
                this.btnNext.Enabled = true;
            }
            else
            {
                this.btnNext.Enabled = false;
            }
        }

        private void textBoxCorprationAddress_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxCorprationAddress.SelectionStart;
            this.textBoxCorprationAddress.Text = StringUtils.GetSubString(this.textBoxCorprationAddress.Text, 100);
            this.textBoxCorprationAddress.SelectionStart = selectionStart;
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char.IsControl(e.KeyChar);
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxName.SelectionStart;
            this.textBoxName.Text = StringUtils.GetSubString(this.textBoxName.Text, 8);
            this.textBoxName.SelectionStart = selectionStart;
            this.CheckNameAndPwd();
        }

        private void textBoxTelephone_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxTelephone.SelectionStart;
            this.textBoxTelephone.Text = StringUtils.GetSubString(this.textBoxTelephone.Text, 100);
            this.textBoxTelephone.SelectionStart = selectionStart;
        }

        private void textBoxUserPasswd_TextChanged(object sender, EventArgs e)
        {
            this.CheckNameAndPwd();
        }

        private void UpdateBtns()
        {
            if (this.tabCtrlFoundation.SelectedTab == this.tabPageWelcome)
            {
                this.btnPre.Visible = false;
            }
            else
            {
                this.btnPre.Visible = true;
            }
            if (this.tabCtrlFoundation.SelectedIndex == (this.tabCtrlFoundation.TabPages.Count - 1))
            {
                this.btnNext.Visible = false;
            }
            else
            {
                this.btnNext.Visible = true;
            }
        }

        private void UpdateLbls()
        {
            this.label12.Font = new Font(this.label12.Font, FontStyle.Regular);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label7.Font = new Font(this.label7.Font, FontStyle.Regular);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label8.Font = new Font(this.label8.Font, FontStyle.Regular);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label10.Font = new Font(this.label10.Font, FontStyle.Regular);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label11.Font = new Font(this.label11.Font, FontStyle.Regular);
            this.label11.ForeColor = System.Drawing.Color.Black;
            if (this.tabCtrlFoundation.SelectedTab == this.tabPageWelcome)
            {
                this.label12.Font = new Font(this.label12.Font, FontStyle.Bold);
                this.label12.ForeColor = System.Drawing.Color.Blue;
                this.label12.Enabled = true;
            }
            if (this.tabCtrlFoundation.SelectedTab == this.tabPageInitial)
            {
                this.label7.Font = new Font(this.label7.Font, FontStyle.Bold);
                this.label7.ForeColor = System.Drawing.Color.Blue;
                this.label7.Enabled = true;
            }
            if (this.tabCtrlFoundation.SelectedTab == this.tabPageBaseInfo)
            {
                this.label8.Font = new Font(this.label8.Font, FontStyle.Bold);
                this.label8.ForeColor = System.Drawing.Color.Blue;
                this.label8.Enabled = true;
            }
            if (this.tabCtrlFoundation.SelectedTab == this.tabPageDZDZ)
            {
                this.label10.Font = new Font(this.label10.Font, FontStyle.Bold);
                this.label10.ForeColor = System.Drawing.Color.Blue;
                this.label10.Enabled = true;
            }
            if (this.tabCtrlFoundation.SelectedTab == this.tabPageDone)
            {
                this.label11.Font = new Font(this.label11.Font, FontStyle.Bold);
                this.label11.ForeColor = System.Drawing.Color.Blue;
                this.label11.Enabled = true;
            }
        }

        public bool IsInitialFormFlag
        {
            get
            {
                return this.isInitialFormFlag;
            }
            set
            {
                this.isInitialFormFlag = value;
            }
        }
    }
}

