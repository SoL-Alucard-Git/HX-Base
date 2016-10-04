namespace Aisino.Fwkp.Fplygl.Form.WLSL_5
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using Aisino.Fwkp.Fplygl.IBLL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class ApplyVolume : DockForm
    {
        private AisinoBTN btn_Apply;
        private AisinoBTN btn_Change;
        private AisinoBTN btn_Close;
        private bool[] cardAuthorization = new bool[6];
        private AisinoCMB cmb_Fplx;
        private AisinoCMB cmb_Zjlx;
        private string cofPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Bin\");
        private IContainer components;
        private string excutiveIDKind = string.Empty;
        private string excutiveIDNum = string.Empty;
        private string excutiveName = string.Empty;
        private DataTable invTypes = new DataTable();
        private bool isJS;
        private AisinoLBL lbl_AddrInfo;
        private AisinoLBL lbl_Bcxe;
        private AisinoLBL lbl_Fpdm;
        private AisinoLBL lbl_Gx;
        private AisinoLBL lbl_LegalNotice;
        private ILog loger = LogUtil.GetLogger<ApplyVolume>();
        private bool logFlag;
        private string logPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private Dictionary<string, int> mapCode2Index = new Dictionary<string, int>();
        private AisinoPNL pnl_Post;
        private readonly ILYGL_PSXX psDal = BLLFactory.CreateInstant<ILYGL_PSXX>("LYGL_PSXX");
        private AisinoRDO rdo_Manual;
        private AisinoRDO rdo_Send;
        private AisinoRTX rtxt_Attach;
        private TextBoxRegex rtxt_Fpzs;
        private TextBoxRegex rtxt_Zjhm;
        private AisinoTXT txt_Jbr;
        private bool[] webAuthorization = new bool[6];
        protected XmlComponentLoader xmlComponentLoader1;

        public ApplyVolume(bool isJSAdmin)
        {
            try
            {
                this.Initialize();
                this.InitializeMapCode2Index();
                this.InitializeCardAuthorization();
                this.InitializeWebAuthorization();
                this.InitializeZjCMB();
                this.isJS = isJSAdmin;
                if (!isJSAdmin)
                {
                    goto Label_01B6;
                }
                this.excutiveName = PropertyUtil.GetValue("Aisino.Fwkp.Fplygl.WLSL_5.ExcutiveName");
                this.excutiveIDKind = PropertyUtil.GetValue("Aisino.Fwkp.Fplygl.WLSL_5.ExcutiveIDKind");
                this.excutiveIDNum = PropertyUtil.GetValue("Aisino.Fwkp.Fplygl.WLSL_5.ExcutiveIDNum");
                if (!string.IsNullOrEmpty(this.excutiveIDNum))
                {
                    this.rtxt_Zjhm.Text = this.excutiveIDNum;
                }
                if (!string.IsNullOrEmpty(this.excutiveName))
                {
                    this.txt_Jbr.Text = this.excutiveName;
                }
                if (!string.IsNullOrEmpty(this.excutiveIDKind))
                {
                    string excutiveIDKind = this.excutiveIDKind;
                    if (excutiveIDKind == null)
                    {
                        goto Label_01A8;
                    }
                    if (!(excutiveIDKind == "201"))
                    {
                        if (excutiveIDKind == "202")
                        {
                            goto Label_018C;
                        }
                        if (excutiveIDKind == "227")
                        {
                            goto Label_019A;
                        }
                        goto Label_01A8;
                    }
                    this.cmb_Zjlx.SelectedIndex = 0;
                }
                goto Label_01D0;
            Label_018C:
                this.cmb_Zjlx.SelectedIndex = 2;
                goto Label_01D0;
            Label_019A:
                this.cmb_Zjlx.SelectedIndex = 1;
                goto Label_01D0;
            Label_01A8:
                this.cmb_Zjlx.SelectedIndex = 3;
                goto Label_01D0;
            Label_01B6:
                this.txt_Jbr.Text = PropertyUtil.GetValue("Login_UserName", "");
            Label_01D0:
                this.rdo_Manual.Checked = true;
                this.invTypes.Columns.Add("key");
                this.invTypes.Columns.Add("value");
                this.InitializeTypeAvailable();
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

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (base.TaxCardInstance.IsOffLineInv())
                {
                    MessageManager.ShowMsgBox("INP-441292");
                }
                else if (this.rtxt_Fpzs.Text.Equals(string.Empty))
                {
                    MessageManager.ShowMsgBox("INP-441295", new string[] { "申领的本数/卷数" });
                }
                else if (Convert.ToInt32(this.rtxt_Fpzs.Text) > Convert.ToInt32(this.lbl_Bcxe.Text))
                {
                    MessageManager.ShowMsgBox("INP-441296");
                }
                else if (Convert.ToInt32(this.rtxt_Fpzs.Text) == 0)
                {
                    MessageManager.ShowMsgBox("INP-441297");
                }
                else if (this.rtxt_Zjhm.Text.Equals(string.Empty))
                {
                    MessageManager.ShowMsgBox("INP-441295", new string[] { "证件号码" });
                }
                else if (this.txt_Jbr.Text.Equals(string.Empty))
                {
                    MessageManager.ShowMsgBox("INP-441295", new string[] { "经办人姓名" });
                }
                else if (this.rdo_Send.Checked && this.lbl_AddrInfo.Text.Equals(string.Empty))
                {
                    MessageManager.ShowMsgBox("INP-4412A0");
                }
                else
                {
                    XmlDocument document;
                    string xml = string.Empty;
                    int num = 0;
                    string type = this.cmb_Fplx.SelectedValue.ToString().Split(new char[] { ';' })[2];
                    bool flag = ApplyCommon.IsHxInvType(type);
                    bool flag2 = ApplyCommon.IsZcInvType(type);
                    if (flag)
                    {
                        document = this.CreateHXApplyInput();
                        if (this.logFlag)
                        {
                            document.Save(this.logPath + "HxApplyInput.xml");
                        }
                        num = HttpsSender.SendMsg("0021", document.InnerXml, ref xml);
                    }
                    else if (flag2)
                    {
                        document = this.CreateZCApplyInput();
                        if (this.logFlag)
                        {
                            document.Save(this.logPath + "ZcApplyInput.xml");
                        }
                        num = HttpsSender.SendMsg("0024", document.InnerXml, ref xml);
                    }
                    if (num != 0)
                    {
                        MessageManager.ShowMsgBox(xml);
                        base.Close();
                    }
                    else
                    {
                        if (flag)
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(xml);
                            if (this.logFlag)
                            {
                                doc.Save(this.logPath + @"\HxApplyOutput.xml");
                            }
                            string str3 = string.Empty;
                            if (!this.ParseHXApplyOutput(doc, out str3))
                            {
                                MessageManager.ShowMsgBox(str3);
                                return;
                            }
                            MessageManager.ShowMsgBox("INP-441293");
                        }
                        else if (flag2)
                        {
                            XmlDocument document3 = new XmlDocument();
                            document3.LoadXml(xml);
                            if (this.logFlag)
                            {
                                document3.Save(this.logPath + @"\ZcApplyOutput.xml");
                            }
                            string str4 = string.Empty;
                            if (!this.ParseZCApplyOutput(document3, out str4))
                            {
                                MessageManager.ShowMsgBox(str4);
                                return;
                            }
                            MessageManager.ShowMsgBox("INP-441293");
                        }
                        base.Close();
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

        private void btn_Change_Click(object sender, EventArgs e)
        {
            DialogResult result = new ApplyAddressList().ShowDialog();
            if (DialogResult.Yes == result)
            {
                this.lbl_AddrInfo.Text = string.Empty;
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "收 件 人：" + ApplyAddressList.outAddr.receiverName + "\n";
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "配送地址：" + ApplyAddressList.outAddr.address + "\n";
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "移动电话：" + ApplyAddressList.outAddr.cellphone + "\n";
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "固定电话：" + ApplyAddressList.outAddr.landline + "\n";
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "邮    编：" + ApplyAddressList.outAddr.postcode + "\n";
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "备    注：" + ApplyAddressList.outAddr.memo + "\n";
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void cmb_Fplx_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cmb_Fplx.SelectedIndex >= 0)
                {
                    string[] strArray = this.cmb_Fplx.SelectedValue.ToString().Split(new char[] { ';' });
                    this.lbl_Fpdm.Text = strArray[0];
                    this.lbl_Bcxe.Text = strArray[1];
                    string str = this.cmb_Fplx.SelectedValue.ToString().Split(new char[] { ';' })[2];
                    if (str.Equals("026"))
                    {
                        this.rdo_Manual.Checked = true;
                        this.rdo_Manual.Enabled = false;
                        this.rdo_Send.Enabled = false;
                    }
                    else
                    {
                        this.rdo_Manual.Enabled = true;
                        this.rdo_Send.Enabled = true;
                    }
                }
                else
                {
                    this.lbl_Fpdm.Text = string.Empty;
                    this.lbl_Bcxe.Text = string.Empty;
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

        private bool CompareCardWebAuthorization(out List<string> cardMore, out List<string> cardLess)
        {
            try
            {
                List<string> list = new List<string>();
                List<string> list2 = new List<string>();
                for (int i = 0; i < 6; i++)
                {
                    if (this.cardAuthorization[i] ^ this.webAuthorization[i])
                    {
                        if (this.cardAuthorization[i])
                        {
                            list.Add(this.MapIndex2TypeName(i));
                        }
                        else
                        {
                            list2.Add(this.MapIndex2TypeName(i));
                        }
                    }
                }
                cardMore = list;
                cardLess = list2;
                return ((list.Count > 0) || (list2.Count > 0));
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                cardMore = null;
                cardLess = null;
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                cardMore = null;
                cardLess = null;
                return false;
            }
        }

        private XmlDocument CreateHXApplyInput()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.PreserveWhitespace = false;
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("FPXT");
                document.AppendChild(element);
                XmlElement element2 = document.CreateElement("INPUT");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("NSRSBH");
                element3.InnerText = base.TaxCardInstance.get_TaxCode();
                element2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("KPJH");
                element4.InnerText = base.TaxCardInstance.get_Machine().ToString();
                element2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("SBBH");
                element5.InnerText = base.TaxCardInstance.GetInvControlNum();
                element2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("DCBB");
                element6.InnerText = base.TaxCardInstance.get_StateInfo().DriverVersion;
                element2.AppendChild(element6);
                XmlElement element7 = document.CreateElement("FPSL");
                element2.AppendChild(element7);
                XmlElement element8 = document.CreateElement("FPZL");
                element8.InnerText = this.cmb_Fplx.SelectedValue.ToString().Split(new char[] { ';' })[2];
                element7.AppendChild(element8);
                XmlElement element9 = document.CreateElement("FPZLDM");
                element9.InnerText = this.lbl_Fpdm.Text;
                element7.AppendChild(element9);
                XmlElement element10 = document.CreateElement("SLSL");
                element10.InnerText = this.rtxt_Fpzs.Text;
                element7.AppendChild(element10);
                XmlElement element11 = document.CreateElement("ZJLX");
                if (this.isJS)
                {
                    if (!string.IsNullOrEmpty(this.excutiveIDKind))
                    {
                        element11.InnerText = this.excutiveIDKind;
                    }
                    else
                    {
                        switch (this.cmb_Fplx.SelectedIndex)
                        {
                            case 0:
                                element11.InnerText = "201";
                                break;

                            case 1:
                                element11.InnerText = "202";
                                break;

                            case 2:
                                element11.InnerText = "227";
                                break;
                        }
                    }
                }
                else
                {
                    element11.InnerText = this.cmb_Zjlx.SelectedValue.ToString();
                }
                element7.AppendChild(element11);
                XmlElement element12 = document.CreateElement("ZJHM");
                if (this.isJS)
                {
                    element12.InnerText = this.excutiveIDNum;
                }
                else
                {
                    element12.InnerText = this.rtxt_Zjhm.Text;
                }
                element7.AppendChild(element12);
                XmlElement element13 = document.CreateElement("JBRXM");
                if (this.isJS)
                {
                    element13.InnerText = this.excutiveName;
                }
                else
                {
                    element13.InnerText = this.txt_Jbr.Text;
                }
                element7.AppendChild(element13);
                XmlElement element14 = document.CreateElement("SLFS");
                element14.InnerText = this.rdo_Manual.Checked ? "1" : "2";
                element7.AppendChild(element14);
                XmlElement element15 = document.CreateElement("SLSM");
                element15.InnerText = this.rtxt_Attach.Text;
                element7.AppendChild(element15);
                XmlElement element16 = document.CreateElement("PSXX");
                element7.AppendChild(element16);
                if (this.rdo_Send.Checked)
                {
                    XmlElement element17 = document.CreateElement("SJRXX");
                    element16.AppendChild(element17);
                    XmlElement element18 = document.CreateElement("SJR");
                    element18.InnerText = ApplyAddressList.outAddr.receiverName;
                    element17.AppendChild(element18);
                    XmlElement element19 = document.CreateElement("YZBM");
                    element19.InnerText = ApplyAddressList.outAddr.postcode;
                    element17.AppendChild(element19);
                    XmlElement element20 = document.CreateElement("DZ");
                    element20.InnerText = ApplyAddressList.outAddr.address;
                    element17.AppendChild(element20);
                    XmlElement element21 = document.CreateElement("GH");
                    element21.InnerText = ApplyAddressList.outAddr.landline;
                    element17.AppendChild(element21);
                    XmlElement element22 = document.CreateElement("YDDH");
                    element22.InnerText = ApplyAddressList.outAddr.cellphone;
                    element17.AppendChild(element22);
                    XmlElement element23 = document.CreateElement("BZ");
                    element23.InnerText = ApplyAddressList.outAddr.memo;
                    element17.AppendChild(element23);
                }
                document.PreserveWhitespace = true;
                return document;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }

        private XmlDocument CreateHXAvailableListInput()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.PreserveWhitespace = false;
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("FPXT");
                document.AppendChild(element);
                XmlElement element2 = document.CreateElement("INPUT");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("NSRSBH");
                element3.InnerText = base.TaxCardInstance.get_TaxCode();
                element2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("KPJH");
                element4.InnerText = base.TaxCardInstance.get_Machine().ToString();
                element2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("SBBH");
                element5.InnerText = base.TaxCardInstance.GetInvControlNum();
                element2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("DCBB");
                element6.InnerText = base.TaxCardInstance.get_StateInfo().DriverVersion;
                element2.AppendChild(element6);
                document.PreserveWhitespace = true;
                return document;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }

        private XmlDocument CreateZCApplyInput()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.PreserveWhitespace = false;
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("business");
                element.SetAttribute("id", "fp_sl");
                element.SetAttribute("comment", "发票申领");
                document.AppendChild(element);
                XmlElement element2 = document.CreateElement("body");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("nsrsbh");
                element3.InnerText = base.TaxCardInstance.get_TaxCode();
                element2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("ph");
                element4.InnerText = base.TaxCardInstance.GetInvControlNum();
                element2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("fplx_dm");
                element5.InnerText = this.cmb_Fplx.SelectedValue.ToString().Split(new char[] { ';' })[2];
                element2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("slsl");
                element6.InnerText = this.rtxt_Fpzs.Text;
                element2.AppendChild(element6);
                XmlElement element7 = document.CreateElement("slsj");
                element7.InnerText = base.TaxCardInstance.GetCardClock().ToString("yyyyMMddhhmmss");
                element2.AppendChild(element7);
                XmlElement element8 = document.CreateElement("fpzl_dm");
                element8.InnerText = this.lbl_Fpdm.Text;
                element2.AppendChild(element8);
                XmlElement element9 = document.CreateElement("zjlx");
                if (this.isJS)
                {
                    if (!string.IsNullOrEmpty(this.excutiveIDKind))
                    {
                        element9.InnerText = this.excutiveIDKind;
                    }
                    else
                    {
                        switch (this.cmb_Fplx.SelectedIndex)
                        {
                            case 0:
                                element9.InnerText = "201";
                                break;

                            case 1:
                                element9.InnerText = "202";
                                break;

                            case 2:
                                element9.InnerText = "227";
                                break;
                        }
                    }
                }
                else
                {
                    element9.InnerText = this.cmb_Zjlx.SelectedValue.ToString();
                }
                element2.AppendChild(element9);
                XmlElement element10 = document.CreateElement("zjhm");
                if (this.isJS)
                {
                    element10.InnerText = this.excutiveIDNum;
                }
                else
                {
                    element10.InnerText = this.rtxt_Zjhm.Text;
                }
                element2.AppendChild(element10);
                XmlElement element11 = document.CreateElement("jbrxm");
                if (this.isJS)
                {
                    element11.InnerText = this.excutiveName;
                }
                else
                {
                    element11.InnerText = this.txt_Jbr.Text;
                }
                element2.AppendChild(element11);
                XmlElement element12 = document.CreateElement("slsm");
                element12.InnerText = this.rtxt_Attach.Text;
                element2.AppendChild(element12);
                XmlElement element13 = document.CreateElement("lpfs");
                element13.InnerText = this.rdo_Manual.Checked ? "1" : "2";
                element2.AppendChild(element13);
                if (this.rdo_Send.Checked)
                {
                    XmlElement element14 = document.CreateElement("psxx");
                    element2.AppendChild(element14);
                    XmlElement element15 = document.CreateElement("sjrxm");
                    element15.InnerText = ApplyAddressList.outAddr.receiverName;
                    element14.AppendChild(element15);
                    XmlElement element16 = document.CreateElement("sjdz");
                    element16.InnerText = ApplyAddressList.outAddr.address;
                    element14.AppendChild(element16);
                    XmlElement element17 = document.CreateElement("sjryddh");
                    element17.InnerText = ApplyAddressList.outAddr.cellphone;
                    element14.AppendChild(element17);
                    XmlElement element18 = document.CreateElement("sjrgddh");
                    element18.InnerText = ApplyAddressList.outAddr.landline;
                    element14.AppendChild(element18);
                    XmlElement element19 = document.CreateElement("sjyb");
                    element19.InnerText = ApplyAddressList.outAddr.postcode;
                    element14.AppendChild(element19);
                    XmlElement element20 = document.CreateElement("bzxx");
                    element20.InnerText = ApplyAddressList.outAddr.memo;
                    element14.AppendChild(element20);
                }
                document.PreserveWhitespace = true;
                return document;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }

        private XmlDocument CreateZCAvailableListInput()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.PreserveWhitespace = false;
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("business");
                element.SetAttribute("id", "fp_zltb");
                element.SetAttribute("comment", "发票种类同步");
                document.AppendChild(element);
                XmlElement element2 = document.CreateElement("body");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("nsrsbh");
                element3.InnerText = base.TaxCardInstance.get_TaxCode();
                element2.AppendChild(element3);
                document.PreserveWhitespace = true;
                return document;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
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
            this.cmb_Fplx = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmb_Fplx");
            this.lbl_Gx = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblGx");
            this.lbl_Fpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFpdm");
            this.lbl_Bcxe = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBcxe");
            this.rtxt_Fpzs = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("rtxtFpzs");
            this.cmb_Zjlx = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmb_zjlx");
            this.rtxt_Zjhm = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("rtxtZjhm");
            this.txt_Jbr = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_jbr");
            this.rdo_Manual = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rdo_Manual");
            this.rdo_Send = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rdo_Send");
            this.lbl_LegalNotice = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_LegalNotice");
            this.pnl_Post = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnl_Post");
            this.btn_Change = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_Change");
            this.lbl_AddrInfo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_AddrInfo");
            this.rtxt_Attach = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("rtxt_Attach");
            this.btn_Apply = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_sl");
            this.btn_Close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
            this.cmb_Fplx.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_Zjlx.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_Fplx.SelectedIndexChanged += new EventHandler(this.cmb_Fplx_SelectedIndexChanged);
            this.lbl_Gx.Click += new EventHandler(this.lbl_Gx_Click);
            this.lbl_Gx.MouseHover += new EventHandler(this.lbl_Gx_MouseHover);
            this.lbl_Gx.MouseLeave += new EventHandler(this.lbl_Gx_MouseLeave);
            this.rtxt_Zjhm.KeyPress += new KeyPressEventHandler(this.rtxt_Zjhm_KeyPress);
            this.txt_Jbr.KeyPress += new KeyPressEventHandler(this.txt_Jbr_KeyPress);
            this.rtxt_Zjhm.TextChanged += new EventHandler(this.rtxt_Zjhm_TextChanged);
            this.txt_Jbr.TextChanged += new EventHandler(this.txt_Jbr_TextChanged);
            this.rdo_Manual.CheckedChanged += new EventHandler(this.rdo_Manual_CheckedChanged);
            this.rdo_Send.CheckedChanged += new EventHandler(this.rdo_Send_CheckedChanged);
            this.lbl_LegalNotice.Click += new EventHandler(this.lbl_LegalNotice_Click);
            this.btn_Change.Click += new EventHandler(this.btn_Change_Click);
            this.rtxt_Attach.KeyPress += new KeyPressEventHandler(this.rtxt_Attach_KeyPress);
            this.rtxt_Attach.TextChanged += new EventHandler(this.rtxt_Attach_TextChanged);
            this.btn_Apply.Click += new EventHandler(this.btn_Apply_Click);
            this.btn_Close.Click += new EventHandler(this.btn_Close_Click);
            this.rtxt_Zjhm.MaxLength = 20;
            this.rtxt_Fpzs.set_RegexText("^[0-9]{0,8}$");
        }

        private void InitializeCardAuthorization()
        {
            try
            {
                this.cardAuthorization[0] = base.TaxCardInstance.get_QYLX().ISZYFP;
                this.cardAuthorization[1] = base.TaxCardInstance.get_QYLX().ISPTFP;
                this.cardAuthorization[2] = base.TaxCardInstance.get_QYLX().ISJDC;
                this.cardAuthorization[3] = base.TaxCardInstance.get_QYLX().ISHY;
                this.cardAuthorization[4] = base.TaxCardInstance.get_QYLX().ISPTFPJSP;
                this.cardAuthorization[5] = base.TaxCardInstance.get_QYLX().ISPTFPDZ;
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

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(ApplyVolume));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1d9, 440);
            this.xmlComponentLoader1.TabIndex = 4;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.ApplyVolume\Aisino.Fwkp.Fplygl.Forms.ApplyVolume.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoScrollMinSize = new Size(0x1c5, 430);
            this.xmlComponentLoader1.AutoScrollMinSize = new Size(0x1c5, 440);
            base.ClientSize = new Size(0x1d9, 440);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ApplyVolumn";
            base.set_TabText("ApplyVolumn");
            this.Text = "申领信息";
            base.ResumeLayout(false);
        }

        private void InitializeMapCode2Index()
        {
            try
            {
                this.mapCode2Index.Add("0", 0);
                this.mapCode2Index.Add("2", 1);
                this.mapCode2Index.Add("005", 2);
                this.mapCode2Index.Add("009", 3);
                this.mapCode2Index.Add("025", 4);
                this.mapCode2Index.Add("026", 5);
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

        private void InitializeTypeAvailable()
        {
            try
            {
                this.btn_Apply.Enabled = false;
                if (this.IsHxAuthoration() && File.Exists(this.cofPath + "HxAvailableListOutput.xml"))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(this.cofPath + @"\HxAvailableListOutput.xml");
                    string msg = string.Empty;
                    this.ParseHXAvailableListOutput(doc, out msg);
                }
                if (this.IsZcAuthoration() && File.Exists(this.cofPath + @"\ZcAvailableListOutput.xml"))
                {
                    XmlDocument document2 = new XmlDocument();
                    document2.Load(this.cofPath + @"\ZcAvailableListOutput.xml");
                    string str2 = string.Empty;
                    this.ParseZCAvailableListOutput(document2, out str2);
                }
                if ((this.invTypes != null) && (this.invTypes.Rows.Count > 0))
                {
                    this.cmb_Fplx.ValueMember = "value";
                    this.cmb_Fplx.DisplayMember = "key";
                    this.cmb_Fplx.DataSource = this.invTypes;
                    this.cmb_Fplx.SelectedIndex = 0;
                    this.btn_Apply.Enabled = true;
                    List<string> cardMore = null;
                    List<string> cardLess = null;
                    if (this.CompareCardWebAuthorization(out cardMore, out cardLess))
                    {
                        MessageManager.ShowMsgBox("授权与核定不一致");
                    }
                }
                else
                {
                    this.lbl_Fpdm.Text = string.Empty;
                    this.lbl_Bcxe.Text = string.Empty;
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

        private void InitializeWebAuthorization()
        {
            for (int i = 0; i < 6; i++)
            {
                this.webAuthorization[i] = false;
            }
        }

        private void InitializeZjCMB()
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("key");
                table.Columns.Add("value");
                DataRow row = table.NewRow();
                row["key"] = "身份证";
                row["value"] = "10";
                table.Rows.Add(row);
                row = table.NewRow();
                row["key"] = "护照";
                row["value"] = "20";
                table.Rows.Add(row);
                row = table.NewRow();
                row["key"] = "军官证";
                row["value"] = "30";
                table.Rows.Add(row);
                row = table.NewRow();
                row["key"] = "其他证件";
                row["value"] = "90";
                table.Rows.Add(row);
                this.cmb_Zjlx.ValueMember = "value";
                this.cmb_Zjlx.DisplayMember = "key";
                this.cmb_Zjlx.DataSource = table;
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

        private bool IsHxAuthoration()
        {
            if (!base.TaxCardInstance.get_QYLX().ISPTFP && !base.TaxCardInstance.get_QYLX().ISZYFP)
            {
                return false;
            }
            return true;
        }

        private bool IsZcAuthoration()
        {
            if ((!base.TaxCardInstance.get_QYLX().ISJDC && !base.TaxCardInstance.get_QYLX().ISHY) && !base.TaxCardInstance.get_QYLX().ISPTFPDZ)
            {
                return false;
            }
            return true;
        }

        private void lbl_Gx_Click(object sender, EventArgs e)
        {
            try
            {
                this.UpdateAuthorization();
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

        private void lbl_Gx_MouseHover(object sender, EventArgs e)
        {
            this.lbl_Gx.ForeColor = Color.Red;
        }

        private void lbl_Gx_MouseLeave(object sender, EventArgs e)
        {
            this.lbl_Gx.ForeColor = Color.Blue;
        }

        private void lbl_LegalNotice_Click(object sender, EventArgs e)
        {
            new ApplyLegalNotice(true).ShowDialog();
        }

        private string MapIndex2TypeName(int index)
        {
            try
            {
                switch (index)
                {
                    case 0:
                        return "增值税专用发票";

                    case 1:
                        return "增值税普通发票";

                    case 2:
                        return "机动车销售统一发票";

                    case 3:
                        return "货物运输业增值税专用发票";

                    case 4:
                        return "增值税普通发票(卷票)";

                    case 5:
                        return "电子增值税普通发票";
                }
                return "未知类型发票";
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return string.Empty;
            }
        }

        private bool ParseHXApplyOutput(XmlDocument doc, out string msg_xh)
        {
            try
            {
                XmlNode node = doc.SelectSingleNode("//CODE");
                XmlNode node2 = doc.SelectSingleNode("//MESS");
                if (node.InnerText.Equals("0000"))
                {
                    msg_xh = doc.SelectSingleNode("//SLXH").InnerText;
                    return true;
                }
                msg_xh = node2.InnerText;
                return false;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                msg_xh = exception.Message;
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                msg_xh = exception2.Message;
                return false;
            }
        }

        private bool ParseHXAvailableListOutput(XmlDocument doc, out string msg)
        {
            try
            {
                XmlNode node = doc.SelectSingleNode("//CODE");
                XmlNode node2 = doc.SelectSingleNode("//MESS");
                if (!node.InnerText.Equals("0000"))
                {
                    msg = node2.InnerText;
                    return false;
                }
                foreach (XmlNode node3 in doc.SelectNodes("//PZHDXX"))
                {
                    string innerText = node3.SelectSingleNode("FPZL").InnerText;
                    this.webAuthorization[this.mapCode2Index[innerText]] = true;
                    DataRow row = this.invTypes.NewRow();
                    XmlNodeList childNodes = node3.ChildNodes;
                    string str2 = string.Empty;
                    string str3 = string.Empty;
                    string str4 = string.Empty;
                    foreach (XmlNode node4 in childNodes)
                    {
                        if (node4.Name.Equals("FPZLMC"))
                        {
                            str2 = node4.InnerText;
                        }
                        else if (node4.Name.Equals("FPZLDM"))
                        {
                            str3 = node4.InnerText;
                        }
                        else if (node4.Name.Equals("MCFPXE"))
                        {
                            str4 = node4.InnerText;
                        }
                    }
                    row["key"] = str2;
                    row["value"] = str3 + ";" + str4 + ";" + innerText;
                    this.invTypes.Rows.Add(row);
                }
                msg = string.Empty;
                return true;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                msg = exception.Message;
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                msg = exception2.Message;
                return false;
            }
        }

        private bool ParseZCApplyOutput(XmlDocument doc, out string msg_xh)
        {
            try
            {
                XmlNode node = doc.SelectSingleNode("//returnCode");
                XmlNode node2 = doc.SelectSingleNode("//returnMessage");
                if (node.InnerText.Equals("00"))
                {
                    msg_xh = doc.SelectSingleNode("//slxh").InnerText;
                    return true;
                }
                msg_xh = node2.InnerText;
                return false;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                msg_xh = exception.Message;
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                msg_xh = exception2.Message;
                return false;
            }
        }

        private bool ParseZCAvailableListOutput(XmlDocument doc, out string msg)
        {
            try
            {
                XmlNode node = doc.SelectSingleNode("//returnCode");
                XmlNode node2 = doc.SelectSingleNode("//returnMessage");
                if (!node.InnerText.Equals("00"))
                {
                    msg = node2.InnerText;
                    return false;
                }
                foreach (XmlNode node3 in doc.SelectNodes("//group"))
                {
                    string innerText = node3.SelectSingleNode("fplx_dm").InnerText;
                    this.webAuthorization[this.mapCode2Index[innerText]] = true;
                    DataRow row = this.invTypes.NewRow();
                    XmlNodeList childNodes = node3.ChildNodes;
                    string str2 = string.Empty;
                    string str3 = string.Empty;
                    string str4 = string.Empty;
                    foreach (XmlNode node4 in childNodes)
                    {
                        if (node4.Name.Equals("fpzl_mc"))
                        {
                            str2 = node4.InnerText;
                        }
                        else if (node4.Name.Equals("fpzl_dm"))
                        {
                            str3 = node4.InnerText;
                        }
                        else if (node4.Name.Equals("mcfpxe"))
                        {
                            str4 = node4.InnerText;
                        }
                    }
                    row["key"] = str2;
                    row["value"] = str3 + ";" + str4 + ";" + innerText;
                    this.invTypes.Rows.Add(row);
                }
                msg = string.Empty;
                return true;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                msg = exception.Message;
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                msg = exception2.Message;
                return false;
            }
        }

        private void rdo_Manual_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo_Manual.Checked)
            {
                this.rdo_Send.Checked = false;
                this.pnl_Post.Visible = false;
            }
        }

        private void rdo_Send_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo_Send.Checked)
            {
                this.rdo_Manual.Checked = false;
                this.pnl_Post.Visible = true;
            }
            AddressInfo info = this.psDal.SelectDefaultAddr();
            this.lbl_AddrInfo.Text = string.Empty;
            if (info != null)
            {
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "收 件 人：" + info.receiverName + "\n";
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "配送地址：" + info.address + "\n";
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "移动电话：" + info.cellphone + "\n";
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "固定电话：" + info.landline + "\n";
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "邮    编：" + info.postcode + "\n";
                this.lbl_AddrInfo.Text = this.lbl_AddrInfo.Text + "备    注：" + info.memo + "\n";
                ApplyAddressList.outAddr = info;
            }
        }

        private void rtxt_Attach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((Encoding.Default.GetBytes(this.rtxt_Attach.Text).Length >= 400) && (Encoding.Default.GetBytes(this.rtxt_Attach.SelectedText).Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void rtxt_Attach_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.rtxt_Attach.SelectionStart;
            int length = 0;
            byte[] bytes = Encoding.Default.GetBytes(this.rtxt_Attach.Text);
            if (bytes.Length <= 400)
            {
                length = bytes.Length;
            }
            else
            {
                int num3 = 0;
                int num4 = 0;
                while (num4 < 400)
                {
                    if ((this.rtxt_Attach.Text[num3] >= '一') && (this.rtxt_Attach.Text[num3] <= 0x9fbb))
                    {
                        if (num4 == 0x18f)
                        {
                            break;
                        }
                        num4 += 2;
                    }
                    else
                    {
                        num4++;
                    }
                    num3++;
                }
                length = num4;
            }
            byte[] buffer2 = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer2[i] = bytes[i];
            }
            this.rtxt_Attach.Text = Encoding.Default.GetString(buffer2);
            this.rtxt_Attach.SelectionStart = this.rtxt_Attach.Text.Length;
            this.rtxt_Attach.Select(selectionStart, 0);
        }

        private void rtxt_Zjhm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((Encoding.Default.GetBytes(this.rtxt_Zjhm.Text).Length >= 20) && (Encoding.Default.GetBytes(this.rtxt_Zjhm.SelectedText).Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void rtxt_Zjhm_TextChanged(object sender, EventArgs e)
        {
            int length = 0;
            byte[] bytes = Encoding.Default.GetBytes(this.rtxt_Zjhm.Text);
            if (bytes.Length <= 20)
            {
                length = bytes.Length;
            }
            else
            {
                int num2 = 0;
                int num3 = 0;
                while (num3 < 20)
                {
                    if ((this.rtxt_Zjhm.Text[num2] >= '一') && (this.rtxt_Zjhm.Text[num2] <= 0x9fbb))
                    {
                        if (num3 == 0x13)
                        {
                            break;
                        }
                        num3 += 2;
                    }
                    else
                    {
                        num3++;
                    }
                    num2++;
                }
                length = num3;
            }
            byte[] buffer2 = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer2[i] = bytes[i];
            }
            this.rtxt_Zjhm.Text = Encoding.Default.GetString(buffer2);
        }

        private void txt_Jbr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((Encoding.Default.GetBytes(this.txt_Jbr.Text).Length >= 20) && (Encoding.Default.GetBytes(this.txt_Jbr.SelectedText).Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void txt_Jbr_TextChanged(object sender, EventArgs e)
        {
            int length = 0;
            byte[] bytes = Encoding.Default.GetBytes(this.txt_Jbr.Text);
            if (bytes.Length <= 20)
            {
                length = bytes.Length;
            }
            else
            {
                int num2 = 0;
                int num3 = 0;
                while (num3 < 20)
                {
                    if ((this.txt_Jbr.Text[num2] >= '一') && (this.txt_Jbr.Text[num2] <= 0x9fbb))
                    {
                        if (num3 == 0x13)
                        {
                            break;
                        }
                        num3 += 2;
                    }
                    else
                    {
                        num3++;
                    }
                    num2++;
                }
                length = num3;
            }
            byte[] buffer2 = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer2[i] = bytes[i];
            }
            this.txt_Jbr.Text = Encoding.Default.GetString(buffer2);
        }

        private void UpdateAuthorization()
        {
            try
            {
                this.btn_Apply.Enabled = false;
                this.InitializeWebAuthorization();
                this.invTypes.Clear();
                if (this.IsHxAuthoration())
                {
                    XmlDocument document = this.CreateHXAvailableListInput();
                    if (this.logFlag)
                    {
                        document.Save(this.logPath + @"\HxAvailableListInput.xml");
                    }
                    string xml = string.Empty;
                    if (HttpsSender.SendMsg("0023", document.InnerXml, ref xml) != 0)
                    {
                        MessageManager.ShowMsgBox(xml);
                        if (File.Exists(this.cofPath + @"\HxAvailableListOutput.xml"))
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.Load(this.cofPath + @"\HxAvailableListOutput.xml");
                            string msg = string.Empty;
                            this.ParseHXAvailableListOutput(doc, out msg);
                        }
                    }
                    else
                    {
                        XmlDocument document3 = new XmlDocument();
                        document3.LoadXml(xml);
                        document3.Save(this.cofPath + @"\HxAvailableListOutput-Update.xml");
                        string str3 = string.Empty;
                        if (!this.ParseHXAvailableListOutput(document3, out str3))
                        {
                            MessageManager.ShowMsgBox(str3);
                            if (File.Exists(this.cofPath + @"\HxAvailableListOutput.xml"))
                            {
                                XmlDocument document4 = new XmlDocument();
                                document4.Load(this.cofPath + @"\HxAvailableListOutput.xml");
                                string str4 = string.Empty;
                                this.ParseHXAvailableListOutput(document4, out str4);
                            }
                        }
                        else
                        {
                            document3.Save(this.cofPath + @"\HxAvailableListOutput.xml");
                        }
                        if (File.Exists(this.cofPath + @"\HxAvailableListOutput-Update.xml"))
                        {
                            File.Delete(this.cofPath + @"\HxAvailableListOutput-Update.xml");
                        }
                    }
                }
                else if (File.Exists(this.cofPath + @"\HxAvailableListOutput.xml"))
                {
                    File.Delete(this.cofPath + @"\HxAvailableListOutput.xml");
                }
                if (this.IsZcAuthoration())
                {
                    XmlDocument document5 = this.CreateZCAvailableListInput();
                    if (this.logFlag)
                    {
                        document5.Save(this.logPath + @"\ZcAvailableListInput.xml");
                    }
                    string str5 = string.Empty;
                    if (HttpsSender.SendMsg("0026", document5.InnerXml, ref str5) != 0)
                    {
                        MessageManager.ShowMsgBox(str5);
                        if (File.Exists(this.cofPath + @"\ZcAvailableListOutput.xml"))
                        {
                            XmlDocument document6 = new XmlDocument();
                            document6.Load(this.cofPath + @"\ZcAvailableListOutput.xml");
                            string str6 = string.Empty;
                            this.ParseZCAvailableListOutput(document6, out str6);
                        }
                    }
                    else
                    {
                        XmlDocument document7 = new XmlDocument();
                        document7.LoadXml(str5);
                        document7.Save(this.cofPath + @"\ZcAvailableListOutput-Update.xml");
                        string str7 = string.Empty;
                        if (!this.ParseZCAvailableListOutput(document7, out str7))
                        {
                            MessageManager.ShowMsgBox(str7);
                            if (File.Exists(this.cofPath + @"\ZcAvailableListOutput.xml"))
                            {
                                XmlDocument document8 = new XmlDocument();
                                document8.Load(this.cofPath + @"\ZcAvailableListOutput.xml");
                                string str8 = string.Empty;
                                this.ParseZCAvailableListOutput(document8, out str8);
                            }
                        }
                        else
                        {
                            document7.Save(this.cofPath + @"\ZcAvailableListOutput.xml");
                        }
                        if (File.Exists(this.cofPath + @"\ZcAvailableListOutput-Update.xml"))
                        {
                            File.Delete(this.cofPath + @"\ZcAvailableListOutput-Update.xml");
                        }
                    }
                }
                else if (File.Exists(this.cofPath + @"\ZcAvailableListOutput.xml"))
                {
                    File.Delete(this.cofPath + @"\ZcAvailableListOutput.xml");
                }
                if ((this.invTypes != null) && (this.invTypes.Rows.Count > 0))
                {
                    this.cmb_Fplx.DataSource = null;
                    this.cmb_Fplx.ValueMember = "value";
                    this.cmb_Fplx.DisplayMember = "key";
                    this.cmb_Fplx.DataSource = this.invTypes;
                    this.cmb_Fplx.SelectedIndex = 0;
                    this.btn_Apply.Enabled = true;
                    List<string> cardMore = null;
                    List<string> cardLess = null;
                    if (this.CompareCardWebAuthorization(out cardMore, out cardLess))
                    {
                        string.Join("，", new string[] { cardMore.ToString() });
                        string.Join("，", new string[] { cardLess.ToString() });
                        MessageManager.ShowMsgBox("授权与核定不一致");
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
    }
}

