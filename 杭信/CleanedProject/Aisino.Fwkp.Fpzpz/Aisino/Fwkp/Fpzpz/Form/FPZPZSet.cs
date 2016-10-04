namespace Aisino.Fwkp.Fpzpz.Form
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Plugin.Core.WebService;
    using Aisino.Fwkp.Fpzpz.BLL;
    using Aisino.Fwkp.Fpzpz.Common;
    using Aisino.Fwkp.Fpzpz.IBLL;
    using Aisino.Fwkp.Fpzpz.Model;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class FPZPZSet : BaseForm
    {
        private string[] _A6SuitList = new string[0];
        private string[] _A6UserList = new string[0];
        private CpkmType _CpkmType = CpkmType.ERROR;
        private ILog _Loger = LogUtil.GetLogger<FPZPZSet>();
        private YskmOrCpkmTabType _YskmOrCpkmTabType = YskmOrCpkmTabType.ERROR;
        private YskmType _YskmType = YskmType.ERROR;
        private AisinoLBL aisinoLBL1;
        private AisinoLBL aisinoLBL10;
        private AisinoLBL aisinoLBL11;
        private AisinoLBL aisinoLBL12;
        private AisinoLBL aisinoLBL13;
        private AisinoLBL aisinoLBL2;
        private AisinoLBL aisinoLBL3;
        private AisinoLBL aisinoLBL4;
        private AisinoLBL aisinoLBL5;
        private AisinoLBL aisinoLBL6;
        private AisinoLBL aisinoLBL7;
        private AisinoLBL aisinoLBL8;
        private AisinoLBL aisinoLBL9;
        private bool bClickGengHuan;
        private bool bGangGangShow = true;
        private AisinoBTN btnCancle;
        private AisinoBTN btnSave;
        private AisinoBTN but_GengHuan;
        private AisinoBTN but_Select_CPKM;
        private AisinoBTN but_Select_YSKM;
        private AisinoBTN but_Xssrkm;
        private AisinoBTN but_Xsthkm;
        private AisinoBTN but_Yjzzskm;
        private AisinoBTN but_Yskm;
        private DataGridViewTextBoxEditingControl CellEdit;
        private DataGridViewTextBoxColumn Column_Bm_Cpkm;
        private DataGridViewTextBoxColumn Column_Bm_Yskm;
        private DataGridViewTextBoxColumn Column_Mc_Cpkm;
        private DataGridViewTextBoxColumn Column_Mc_Yskm;
        private DataGridViewTextBoxColumn Column_Xskm_Cpkm;
        private DataGridViewTextBoxColumn Column_Xsthkm_Cpkm;
        private DataGridViewTextBoxColumn Column_Yjzzs_Cpkm;
        private DataGridViewTextBoxColumn Column_Yskm_Yskm;
        private AisinoCMB comb_Qtkm_Zdfs;
        private AisinoCMB comb_Xtsp_Zds;
        private AisinoCMB comb_Yskm_Zdfs;
        private AisinoCMB comb_Zfsfp_Zds;
        private AisinoCMB combo_CpkmSetYj;
        private AisinoCMB combo_IP;
        private AisinoCMB combo_User;
        private AisinoCMB combo_YskmSetYj;
        private AisinoCMB combo_ZhangTao;
        private IContainer components;
        private TabPage currentTab;
        private CustomStyleDataGrid customStyleDataGrid_Cpkm;
        private CustomStyleDataGrid customStyleDataGrid_Yskm;
        private bool isSaved;
        private IKHBM khbmBll = new KHBM();
        private ISPBM spbmBll = new SPBM();
        private string sUserGuide = string.Empty;
        private TabControlPw tabFPZPZSet;
        private AisinoTXT text_Xssrkm;
        private AisinoTXT text_Xsthkm;
        private AisinoTXT text_Yjzzskm;
        private AisinoTXT text_Yskm;
        private XmlComponentLoader xmlComponentLoader1;

        public FPZPZSet()
        {
            try
            {
                this.Initialize();
                this.InitServerSet();
                this.InitKMSet();
                this.InitZDFSSet();
                this.tabFPZPZSet.SelectedTab.BackColor = Color.White;
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
                this._Loger.Error(exception.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                return DingYiZhiFuChuan.strErrLinkFailTip;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
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
                this._Loger.Error(exception.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                return DingYiZhiFuChuan.strErrLinkFailTip;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
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
                                if (str8.IndexOf("=") > -1)
                                {
                                    this.combo_User.Items.Add(str8.Substring(str8.IndexOf("=") + 1));
                                }
                                else if (str8.IndexOf("#") > -1)
                                {
                                    this.combo_User.Items.Add(str8.Substring(str8.IndexOf("#") + 1));
                                }
                                else
                                {
                                    this.combo_User.Items.Add(str8);
                                }
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
                this._Loger.Error(exception.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                ExceptionHandler.HandleError(exception2);
            }
            return string.Empty;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IKMProperty property = new KMProperty();
                if (this.SaveServerSet())
                {
                    property.Delete();
                    List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                    List<string> kM = this.GetKM();
                    if ((kM != null) && (kM.Count > 0))
                    {
                        foreach (string str in kM)
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            item.Add("BM", str);
                            item.Add("iContact", string.Empty);
                            item.Add("uomName", string.Empty);
                            item.Add("cSupGUID", string.Empty);
                            item.Add("cCustGUID", string.Empty);
                            item.Add("cMateGUID", string.Empty);
                            item.Add("cDeptGUID", string.Empty);
                            item.Add("cEmpGUID", string.Empty);
                            list.Add(item);
                        }
                    }
                    if (list.Count > 0)
                    {
                        property.ReplaceRecords(list);
                    }
                    PropertyUtil.Save();
                    base.Close();
                }
            }
            catch (Exception exception)
            {
                this._Loger.Error("发票转凭证设置保存失败：" + exception.Message);
            }
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
                    Aisino.Fwkp.Fpzpz.Common.Tool.PzInterFaceLinkInfo(str, this._Loger);
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
            finally
            {
                MessageHelper.MsgWait();
            }
        }

        private void but_Select_YSKM_CPKM_Click(object sender, EventArgs e)
        {
            try
            {
                string subject;
                CustomStyleDataGrid grid;
                if (!this.SaveServerSet())
                {
                    this.tabFPZPZSet.SelectedTab = this.tabFPZPZSet.TabPages[0];
                }
                else if (DingYiZhiFuChuan.isA6PzVersion)
                {
                    bool bSuccess = false;
                    subject = this.GetSubject(out bSuccess);
                    if (bSuccess)
                    {
                        grid = null;
                        switch (this._YskmOrCpkmTabType)
                        {
                            case YskmOrCpkmTabType.YskmTab:
                                grid = this.customStyleDataGrid_Yskm;
                                goto Label_0070;

                            case YskmOrCpkmTabType.CpkmTab:
                                grid = this.customStyleDataGrid_Cpkm;
                                goto Label_0070;
                        }
                    }
                }
                return;
            Label_0070:
                if (this.CellEdit != null)
                {
                    this.CellEdit.Text = subject;
                    this.CellEdit.SelectAll();
                    if (!this.CellEdit.Focus())
                    {
                        grid.CurrentCell.Value = subject;
                    }
                }
                else
                {
                    grid.CurrentCell.Value = subject;
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

        private void but_Yskm_Xsth_Yjzzs_XssrKm_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.SaveServerSet())
                {
                    this.tabFPZPZSet.SelectedTab = this.tabFPZPZSet.TabPages[0];
                }
                else if (DingYiZhiFuChuan.isA6PzVersion)
                {
                    bool bSuccess = false;
                    string subject = this.GetSubject(out bSuccess);
                    if (bSuccess)
                    {
                        AisinoBTN obtn = (AisinoBTN) sender;
                        string name = obtn.Name;
                        if (name.Equals("but_Yskm"))
                        {
                            this.text_Yskm.Text = subject;
                            PropertyUtil.SetValue(DingYiZhiFuChuan.Ysrkm, subject);
                        }
                        else if (name.Equals("but_Xssrkm"))
                        {
                            this.text_Xssrkm.Text = subject;
                            PropertyUtil.SetValue(DingYiZhiFuChuan.Xssrkm, subject);
                        }
                        else if (name.Equals("but_Yjzzskm"))
                        {
                            this.text_Yjzzskm.Text = subject;
                            PropertyUtil.SetValue(DingYiZhiFuChuan.Yjzzskm, subject);
                        }
                        else if (name.Equals("but_Xsthkm"))
                        {
                            this.text_Xsthkm.Text = subject;
                            PropertyUtil.SetValue(DingYiZhiFuChuan.Xsthkm, subject);
                        }
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

        private void CellEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                bool flag = false;
                e.KeyChar = this.ConvertQJToBJ(e.KeyChar);
                if ((this.customStyleDataGrid_Yskm.CurrentCell != null) && (this.customStyleDataGrid_Yskm.CurrentCell.OwningColumn.Name == DingYiZhiFuChuan.KHBMCulmnDataName[11]))
                {
                    flag = true;
                }
                if ((this.customStyleDataGrid_Cpkm.CurrentCell != null) && (((this.customStyleDataGrid_Cpkm.CurrentCell.OwningColumn.Name == DingYiZhiFuChuan.SPBMCulmnDataName[11]) || (this.customStyleDataGrid_Cpkm.CurrentCell.OwningColumn.Name == DingYiZhiFuChuan.SPBMCulmnDataName[12])) || (this.customStyleDataGrid_Cpkm.CurrentCell.OwningColumn.Name == DingYiZhiFuChuan.SPBMCulmnDataName[13])))
                {
                    flag = true;
                }
                if (flag)
                {
                    if (((Convert.ToInt32(e.KeyChar) < 0x30) || (Convert.ToInt32(e.KeyChar) > 0x39)) && ((((Convert.ToInt32(e.KeyChar) != 0x18) && (Convert.ToInt32(e.KeyChar) != 3)) && ((Convert.ToInt32(e.KeyChar) != 0x16) && (Convert.ToInt32(e.KeyChar) != 0x2e))) && ((Convert.ToInt32(e.KeyChar) != 8) && (Convert.ToInt32(e.KeyChar) != 13))))
                    {
                        e.Handled = true;
                    }
                    else if (Convert.ToInt32(e.KeyChar) == 0x2e)
                    {
                        e.Handled = true;
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
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void combo_User_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (string str in this._A6UserList)
                {
                    if (str.Substring(str.IndexOf("#") + 1).Equals(this.combo_User.Text.Trim()))
                    {
                        this.sUserGuide = str.Replace("#", "=");
                        return;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void combo_Yskm_Cpkm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string str;
                string str2;
                List<SPBMModal> list4;
                this.GetYskmOrCpkmTabType();
                switch (this._YskmOrCpkmTabType)
                {
                    case YskmOrCpkmTabType.YskmTab:
                        str = this.combo_YskmSetYj.SelectedItem.ToString();
                        PropertyUtil.SetValue(DingYiZhiFuChuan.YskmItemValue, str);
                        if (!str.Equals(DingYiZhiFuChuan.YskmItem[0]))
                        {
                            goto Label_0084;
                        }
                        if (!this._YskmType.Equals(YskmType.KHFL))
                        {
                            break;
                        }
                        return;

                    case YskmOrCpkmTabType.CpkmTab:
                        str2 = this.combo_CpkmSetYj.SelectedItem.ToString();
                        PropertyUtil.SetValue(DingYiZhiFuChuan.CpkmItemValue, str2);
                        if (!str2.Equals(DingYiZhiFuChuan.CpkmItem[0]))
                        {
                            goto Label_0174;
                        }
                        if (!this._CpkmType.Equals(CpkmType.CHFL))
                        {
                            goto Label_015C;
                        }
                        return;

                    default:
                        return;
                }
                List<KHBMModal> listModel = this.khbmBll.SelectKHBM_KHFL();
                this.InsertData_Yskm(listModel, YskmType.KHFL);
                return;
            Label_0084:
                if (str.Equals(DingYiZhiFuChuan.YskmItem[1]))
                {
                    if (!this._YskmType.Equals(YskmType.KH))
                    {
                        List<KHBMModal> list2 = this.khbmBll.SelectKHBM_KH();
                        this.InsertData_Yskm(list2, YskmType.KH);
                    }
                }
                else if (str.Equals(DingYiZhiFuChuan.YskmItem[2]) && !this._YskmType.Equals(YskmType.DQFL))
                {
                    List<KHBMModal> list3 = this.khbmBll.SelectKHBM_DQFL();
                    this.InsertData_Yskm(list3, YskmType.DQFL);
                }
                return;
            Label_015C:
                list4 = this.spbmBll.SelectSPBM_CHFL();
                this.InsertData_Cpkm(list4, CpkmType.CHFL);
                return;
            Label_0174:
                if (str2.Equals(DingYiZhiFuChuan.CpkmItem[1]) && !this._CpkmType.Equals(CpkmType.CH))
                {
                    List<SPBMModal> list5 = this.spbmBll.SelectSPBM_CH();
                    this.InsertData_Cpkm(list5, CpkmType.CH);
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

        private void combo_Yskm_Qtkm_Zfsfp_Xtsp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AisinoCMB ocmb = (AisinoCMB) sender;
                if (ocmb != null)
                {
                    string name = ocmb.Name;
                    if (name.Equals("comb_Yskm_Zdfs"))
                    {
                        string str2 = PropertyUtil.GetValue(DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItemValue);
                        string str3 = this.comb_Yskm_Zdfs.SelectedItem.ToString();
                        if (!str2.Equals(str3))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItemValue, str3);
                        }
                    }
                    else if (name.Equals("comb_Qtkm_Zdfs"))
                    {
                        string str4 = PropertyUtil.GetValue(DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItemValue);
                        string str5 = this.comb_Qtkm_Zdfs.SelectedItem.ToString();
                        if (!str4.Equals(str5))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItemValue, str5);
                        }
                    }
                    else if (name.Equals("comb_Zfsfp_Zds"))
                    {
                        string str6 = PropertyUtil.GetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue);
                        string str7 = this.comb_Zfsfp_Zds.SelectedItem.ToString();
                        if (!str6.Equals(str7))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue, str7);
                        }
                    }
                    else if (name.Equals("comb_Xtsp_Zds"))
                    {
                        string str8 = PropertyUtil.GetValue(DingYiZhiFuChuan.Xtsp_ZdsItemValue);
                        string str9 = this.comb_Xtsp_Zds.SelectedItem.ToString();
                        if (!str8.Equals(str9))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.Xtsp_ZdsItemValue, str9);
                        }
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
                                if (str4.IndexOf("=") > -1)
                                {
                                    this.combo_User.Items.Add(str4.Substring(str4.IndexOf("=") + 1));
                                }
                                else if (str4.IndexOf("#") > -1)
                                {
                                    this.combo_User.Items.Add(str4.Substring(str4.IndexOf("#") + 1));
                                }
                                else
                                {
                                    this.combo_User.Items.Add(str4);
                                }
                            }
                            this.combo_User.SelectedIndex = 0;
                        }
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

        private char ConvertQJToBJ(char keyCharIn)
        {
            char ch = keyCharIn;
            int num = -1;
            try
            {
                num = Convert.ToInt32(keyCharIn);
                if ((((num - 0xfee0) > 0x2f) && ((num - 0xfee0) < 0x3a)) && (num != -1))
                {
                    ch = (char) (num - 0xfee0);
                }
            }
            catch (Exception)
            {
            }
            return ch;
        }

        private void customStyleDataGrid_Yskm_Cpkm_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.ShowBut_Select(e.ColumnIndex, e.RowIndex);
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

        private void customStyleDataGrid_Yskm_Cpkm_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                this.ShowBut_Select(-1, -1);
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

        private void customStyleDataGrid_Yskm_Cpkm_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCell currentCell;
                DataGridViewCell cell2;
                DataGridViewRow row2;
                switch (this._YskmOrCpkmTabType)
                {
                    case YskmOrCpkmTabType.YskmTab:
                        currentCell = this.customStyleDataGrid_Yskm.CurrentCell;
                        if (currentCell != null)
                        {
                            break;
                        }
                        return;

                    case YskmOrCpkmTabType.CpkmTab:
                        cell2 = this.customStyleDataGrid_Cpkm.CurrentCell;
                        if (cell2 != null)
                        {
                            goto Label_0159;
                        }
                        return;

                    default:
                        return;
                }
                DataGridViewRow owningRow = currentCell.OwningRow;
                if ((owningRow != null) && currentCell.OwningColumn.Name.Equals(DingYiZhiFuChuan.KHBMCulmnDataName[11]))
                {
                    switch (this._YskmType)
                    {
                        case YskmType.KHFL:
                        case YskmType.KH:
                        {
                            string strBm = owningRow.Cells[DingYiZhiFuChuan.KHBMCulmnDataName[0]].Value.ToString();
                            string strYskm = owningRow.Cells[DingYiZhiFuChuan.KHBMCulmnDataName[11]].Value.ToString();
                            this.khbmBll.UpdateKHBM_Yskm(strBm, strYskm);
                            break;
                        }
                        case YskmType.DQFL:
                        {
                            string strDqbm = owningRow.Cells[DingYiZhiFuChuan.KHBMCulmnDataName[0]].Value.ToString();
                            string strDqmc = owningRow.Cells[DingYiZhiFuChuan.KHBMCulmnDataName[1]].Value.ToString();
                            string strDqkm = owningRow.Cells[DingYiZhiFuChuan.KHBMCulmnDataName[11]].Value.ToString();
                            this.khbmBll.UpdateKHBM_Dqkm(strDqbm, strDqmc, strDqkm);
                            break;
                        }
                    }
                }
                return;
            Label_0159:
                row2 = cell2.OwningRow;
                if ((row2 != null) && ((cell2.OwningColumn.Name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[11]) || cell2.OwningColumn.Name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[12])) || cell2.OwningColumn.Name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[13])))
                {
                    switch (this._CpkmType)
                    {
                        case CpkmType.CHFL:
                        case CpkmType.CH:
                        {
                            string str6 = row2.Cells[DingYiZhiFuChuan.SPBMCulmnDataName[0]].Value.ToString();
                            string strXssrkm = row2.Cells[DingYiZhiFuChuan.SPBMCulmnDataName[11]].Value.ToString();
                            string strYjzzskm = row2.Cells[DingYiZhiFuChuan.SPBMCulmnDataName[12]].Value.ToString();
                            string strXsthkm = row2.Cells[DingYiZhiFuChuan.SPBMCulmnDataName[13]].Value.ToString();
                            this.spbmBll.UpdateSPBM_XSSRKM(str6, strXssrkm);
                            this.spbmBll.UpdateSPBM_YJZZSKM(str6, strYjzzskm);
                            this.spbmBll.UpdateSPBM_XSTHKM(str6, strXsthkm);
                            return;
                        }
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

        private void customStyleDataGrid_Yskm_Cpkm_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridViewCell currentCell;
                DataGridViewCell cell2;
                switch (this._YskmOrCpkmTabType)
                {
                    case YskmOrCpkmTabType.YskmTab:
                        currentCell = this.customStyleDataGrid_Yskm.CurrentCell;
                        if (currentCell != null)
                        {
                            break;
                        }
                        return;

                    case YskmOrCpkmTabType.CpkmTab:
                        cell2 = this.customStyleDataGrid_Cpkm.CurrentCell;
                        if (cell2 != null)
                        {
                            goto Label_0072;
                        }
                        return;

                    default:
                        return;
                }
                if ((currentCell.OwningRow != null) && currentCell.OwningColumn.Name.Equals(DingYiZhiFuChuan.KHBMCulmnDataName[11]))
                {
                    goto Label_00D3;
                }
                return;
            Label_0072:
                if ((cell2.OwningRow == null) || ((!cell2.OwningColumn.Name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[11]) && !cell2.OwningColumn.Name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[12])) && !cell2.OwningColumn.Name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[13])))
                {
                    return;
                }
            Label_00D3:
                this.but_Select_YSKM_CPKM_Click(null, null);
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

        private void DataGrid_YskmSet_CpkmSet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                this.CellEdit = (DataGridViewTextBoxEditingControl) e.Control;
                this.CellEdit.SelectAll();
                this.CellEdit.KeyPress += new KeyPressEventHandler(this.CellEdit_KeyPress);
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

        private void FPZPZ_Save(object sender, FormClosingEventArgs e)
        {
            PropertyUtil.Save();
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
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
            return str;
        }

        private List<string> GetKM()
        {
            List<string> list = new List<string>();
            try
            {
                string str = string.Empty;
                str = this.text_Yskm.Text.Trim();
                if (!string.IsNullOrEmpty(str) && !list.Contains(str))
                {
                    list.Add(str);
                }
                str = this.text_Xssrkm.Text.Trim();
                if (!string.IsNullOrEmpty(str) && !list.Contains(str))
                {
                    list.Add(str);
                }
                str = this.text_Xsthkm.Text.Trim();
                if (!string.IsNullOrEmpty(str) && !list.Contains(str))
                {
                    list.Add(str);
                }
                str = this.text_Yjzzskm.Text.Trim();
                if (!string.IsNullOrEmpty(str) && !list.Contains(str))
                {
                    list.Add(str);
                }
                foreach (DataGridViewRow row in (IEnumerable) this.customStyleDataGrid_Yskm.Rows)
                {
                    if ((row != null) && (row.Cells["YSKM"] != null))
                    {
                        str = row.Cells["YSKM"].Value.ToString().Trim();
                        if (!string.IsNullOrEmpty(str) && !list.Contains(str))
                        {
                            list.Add(str);
                        }
                    }
                }
                foreach (DataGridViewRow row2 in (IEnumerable) this.customStyleDataGrid_Cpkm.Rows)
                {
                    if (row2 != null)
                    {
                        if (row2.Cells["XSSRKM"] != null)
                        {
                            str = row2.Cells["XSSRKM"].Value.ToString().Trim();
                            if (!string.IsNullOrEmpty(str) && !list.Contains(str))
                            {
                                list.Add(str);
                            }
                        }
                        if (row2.Cells["YJZZSKM"] != null)
                        {
                            str = row2.Cells["YJZZSKM"].Value.ToString().Trim();
                            if (!string.IsNullOrEmpty(str) && !list.Contains(str))
                            {
                                list.Add(str);
                            }
                        }
                        if (row2.Cells["XSTHKM"] != null)
                        {
                            str = row2.Cells["XSTHKM"].Value.ToString().Trim();
                            if (!string.IsNullOrEmpty(str) && !list.Contains(str))
                            {
                                list.Add(str);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this._Loger.Error(exception.ToString());
            }
            return list;
        }

        private string GetSubject(out bool bSuccess)
        {
            bSuccess = false;
            try
            {
                if (DingYiZhiFuChuan.isA6PzVersion)
                {
                    DingYiZhiFuChuan.GetLinkUserSuit();
                    if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6ServerLink.Trim()))
                    {
                        Aisino.Fwkp.Fpzpz.Common.Tool.PzInterFaceLinkInfo(DingYiZhiFuChuan.ServerEmpty, this._Loger);
                        return string.Empty;
                    }
                    if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6SuitGuid.Trim()) || string.IsNullOrEmpty(DingYiZhiFuChuan.A6UserGuid.Trim()))
                    {
                        Aisino.Fwkp.Fpzpz.Common.Tool.PzInterFaceLinkInfo(DingYiZhiFuChuan.UserSuitEmpty, this._Loger);
                        return string.Empty;
                    }
                    string str = DingYiZhiFuChuan.A6ServerLink;
                    if (Aisino.Fwkp.Fpzpz.Common.Tool.IsA6Version())
                    {
                        str = str + DingYiZhiFuChuan.A6LinkZhiFuChuan_KeMuSelect;
                    }
                    else
                    {
                        str = str + "/gl/pz/md_cz_km_kp.html?";
                    }
                    string str2 = DingYiZhiFuChuan.A6SuitGuid;
                    string str3 = DingYiZhiFuChuan.A6UserGuid;
                    str2 = str2.Substring(0, str2.IndexOf("="));
                    str3 = str3.Substring(0, str3.IndexOf("="));
                    KeMuMsgSet set = new KeMuMsgSet(str + "accountID=" + str2);
                    if (DialogResult.OK == set.ShowDialog())
                    {
                        bSuccess = true;
                        return set.A6Km.Trim();
                    }
                    bSuccess = false;
                    return string.Empty;
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
            return string.Empty;
        }

        private YskmOrCpkmTabType GetYskmOrCpkmTabType()
        {
            try
            {
                if (this.tabFPZPZSet.SelectedIndex.Equals(2))
                {
                    this._YskmOrCpkmTabType = YskmOrCpkmTabType.YskmTab;
                }
                else if (this.tabFPZPZSet.SelectedIndex.Equals(3))
                {
                    this._YskmOrCpkmTabType = YskmOrCpkmTabType.CpkmTab;
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
            return this._YskmOrCpkmTabType;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.tabFPZPZSet = this.xmlComponentLoader1.GetControlByName<TabControlPw>("tabFPZPZSet");
            this.aisinoLBL1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL1");
            this.aisinoLBL2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL2");
            this.aisinoLBL3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL3");
            this.aisinoLBL4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL4");
            this.aisinoLBL5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL5");
            this.aisinoLBL6 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL6");
            this.aisinoLBL7 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL7");
            this.aisinoLBL8 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL8");
            this.aisinoLBL9 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL9");
            this.aisinoLBL10 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL10");
            this.aisinoLBL11 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL11");
            this.aisinoLBL12 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL12");
            this.aisinoLBL13 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL13");
            this.combo_IP = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_Server");
            this.combo_User = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_User");
            this.combo_ZhangTao = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_ZhangTao");
            this.but_GengHuan = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button1");
            this.but_Select_YSKM = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_Select_YSKM");
            this.but_Select_CPKM = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_Select_CPKM");
            this.but_Xsthkm = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_Xsthkm");
            this.text_Xsthkm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Xsthkm");
            this.but_Yjzzskm = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_Yjzzskm");
            this.text_Yjzzskm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Yjzzskm");
            this.but_Xssrkm = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_Xssrkm");
            this.text_Xssrkm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Xssrkm");
            this.but_Yskm = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_Yskm");
            this.text_Yskm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Yskm");
            this.customStyleDataGrid_Yskm = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid_Yskm");
            this.combo_YskmSetYj = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_YskmSetYj");
            this.combo_CpkmSetYj = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_CpkmSetYj");
            this.customStyleDataGrid_Cpkm = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid_CpkmSet");
            this.comb_Yskm_Zdfs = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comb_Yskm_Zdfs");
            this.comb_Qtkm_Zdfs = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comb_Qtkm_Zdfs");
            this.comb_Zfsfp_Zds = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comb_Zfsfp_Zds");
            this.comb_Xtsp_Zds = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comb_Xtsp_Zds");
            this.btnSave = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnSave");
            this.btnCancle = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancle");
            this.tabFPZPZSet.Alignment = TabAlignment.Left;
            this.tabFPZPZSet.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabFPZPZSet.ItemSize = new Size(0x23, 150);
            this.tabFPZPZSet.SizeMode = TabSizeMode.Fixed;
            this.tabFPZPZSet.Selecting += new TabControlCancelEventHandler(this.tabFPZPZSet_Selecting);
            this.btnCancle.Click += new EventHandler(this.btnCancle_Click);
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.but_GengHuan.Click += new EventHandler(this.but_GengHuan_Click);
            this.combo_IP.TextChanged += new EventHandler(this.combo_IP_TextChanged);
            this.combo_User.SelectedIndexChanged += new EventHandler(this.combo_User_TextChanged);
            this.combo_IP.MaxLength = 60;
            this.combo_User.MaxLength = 20;
            this.combo_User.DropDownStyle = ComboBoxStyle.DropDownList;
            this.combo_ZhangTao.MaxLength = 20;
            this.combo_ZhangTao.SelectedIndexChanged += new EventHandler(this.combo_ZhangTao_SelectedIndexChanged);
            this.combo_ZhangTao.DropDownStyle = ComboBoxStyle.DropDownList;
            int kMMaxInputLength = DingYiZhiFuChuan.KMMaxInputLength;
            this.text_Xsthkm.MaxLength = kMMaxInputLength;
            this.text_Xsthkm.KeyPress += new KeyPressEventHandler(this.text_Yskm_Xsth_Yjzzs_Xssr_Xsthkm_KeyPress);
            this.text_Xsthkm.TextChanged += new EventHandler(this.text_Yskm_Xsth_Yjzzs_Xssr_Xsthkm_TextChanged);
            this.text_Yjzzskm.MaxLength = kMMaxInputLength;
            this.text_Yjzzskm.KeyPress += new KeyPressEventHandler(this.text_Yskm_Xsth_Yjzzs_Xssr_Xsthkm_KeyPress);
            this.text_Yjzzskm.TextChanged += new EventHandler(this.text_Yskm_Xsth_Yjzzs_Xssr_Xsthkm_TextChanged);
            this.text_Xssrkm.MaxLength = kMMaxInputLength;
            this.text_Xssrkm.KeyPress += new KeyPressEventHandler(this.text_Yskm_Xsth_Yjzzs_Xssr_Xsthkm_KeyPress);
            this.text_Xssrkm.TextChanged += new EventHandler(this.text_Yskm_Xsth_Yjzzs_Xssr_Xsthkm_TextChanged);
            this.text_Yskm.MaxLength = kMMaxInputLength;
            this.text_Yskm.KeyPress += new KeyPressEventHandler(this.text_Yskm_Xsth_Yjzzs_Xssr_Xsthkm_KeyPress);
            this.text_Yskm.TextChanged += new EventHandler(this.text_Yskm_Xsth_Yjzzs_Xssr_Xsthkm_TextChanged);
            this.but_Xsthkm.Click += new EventHandler(this.but_Yskm_Xsth_Yjzzs_XssrKm_Click);
            this.but_Yjzzskm.Click += new EventHandler(this.but_Yskm_Xsth_Yjzzs_XssrKm_Click);
            this.but_Xssrkm.Click += new EventHandler(this.but_Yskm_Xsth_Yjzzs_XssrKm_Click);
            this.but_Yskm.Click += new EventHandler(this.but_Yskm_Xsth_Yjzzs_XssrKm_Click);
            this.customStyleDataGrid_Yskm.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.customStyleDataGrid_Yskm.MultiSelect = false;
            this.customStyleDataGrid_Yskm.ColumnAdd(this.Column_Bm_Yskm);
            this.customStyleDataGrid_Yskm.ColumnAdd(this.Column_Mc_Yskm);
            this.customStyleDataGrid_Yskm.ColumnAdd(this.Column_Yskm_Yskm);
            this.customStyleDataGrid_Yskm.Controls.Add(this.but_Select_YSKM);
            this.customStyleDataGrid_Yskm.AllowUserToAddRows = false;
            this.customStyleDataGrid_Yskm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.customStyleDataGrid_Yskm.Size = new Size(0x1e8, 370);
            this.but_Select_YSKM.Click += new EventHandler(this.but_Select_YSKM_CPKM_Click);
            this.customStyleDataGrid_Yskm.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.DataGrid_YskmSet_CpkmSet_EditingControlShowing);
            this.customStyleDataGrid_Yskm.CellEnter += new DataGridViewCellEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellEnter);
            this.customStyleDataGrid_Yskm.CellPainting += new DataGridViewCellPaintingEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellPainting);
            this.customStyleDataGrid_Yskm.CellValueChanged += new DataGridViewCellEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellValueChanged);
            this.customStyleDataGrid_Yskm.DoubleClick += new EventHandler(this.customStyleDataGrid_Yskm_Cpkm_DoubleClick);
            this.customStyleDataGrid_Cpkm.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.customStyleDataGrid_Cpkm.MultiSelect = false;
            this.customStyleDataGrid_Cpkm.ColumnAdd(this.Column_Bm_Cpkm);
            this.customStyleDataGrid_Cpkm.ColumnAdd(this.Column_Mc_Cpkm);
            this.customStyleDataGrid_Cpkm.ColumnAdd(this.Column_Xskm_Cpkm);
            this.customStyleDataGrid_Cpkm.ColumnAdd(this.Column_Yjzzs_Cpkm);
            this.customStyleDataGrid_Cpkm.ColumnAdd(this.Column_Xsthkm_Cpkm);
            this.customStyleDataGrid_Cpkm.Controls.Add(this.but_Select_CPKM);
            this.customStyleDataGrid_Cpkm.AllowUserToAddRows = false;
            this.customStyleDataGrid_Cpkm.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.customStyleDataGrid_Cpkm.Size = new Size(0x1e8, 370);
            this.customStyleDataGrid_Cpkm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.but_Select_CPKM.Click += new EventHandler(this.but_Select_YSKM_CPKM_Click);
            this.customStyleDataGrid_Cpkm.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.DataGrid_YskmSet_CpkmSet_EditingControlShowing);
            this.customStyleDataGrid_Cpkm.CellEnter += new DataGridViewCellEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellEnter);
            this.customStyleDataGrid_Cpkm.CellPainting += new DataGridViewCellPaintingEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellPainting);
            this.customStyleDataGrid_Cpkm.CellValueChanged += new DataGridViewCellEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellValueChanged);
            this.customStyleDataGrid_Cpkm.DoubleClick += new EventHandler(this.customStyleDataGrid_Yskm_Cpkm_DoubleClick);
            this.combo_YskmSetYj.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Cpkm_SelectedIndexChanged);
            this.combo_CpkmSetYj.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Cpkm_SelectedIndexChanged);
            this.comb_Yskm_Zdfs.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comb_Qtkm_Zdfs.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comb_Zfsfp_Zds.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comb_Xtsp_Zds.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comb_Yskm_Zdfs.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Qtkm_Zfsfp_Xtsp_SelectedIndexChanged);
            this.comb_Qtkm_Zdfs.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Qtkm_Zfsfp_Xtsp_SelectedIndexChanged);
            this.comb_Zfsfp_Zds.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Qtkm_Zfsfp_Xtsp_SelectedIndexChanged);
            this.comb_Xtsp_Zds.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Qtkm_Zfsfp_Xtsp_SelectedIndexChanged);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FPZPZSet));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            this.Column_Bm_Yskm = new DataGridViewTextBoxColumn();
            this.Column_Mc_Yskm = new DataGridViewTextBoxColumn();
            this.Column_Yskm_Yskm = new DataGridViewTextBoxColumn();
            this.Column_Bm_Cpkm = new DataGridViewTextBoxColumn();
            this.Column_Mc_Cpkm = new DataGridViewTextBoxColumn();
            this.Column_Xskm_Cpkm = new DataGridViewTextBoxColumn();
            this.Column_Yjzzs_Cpkm = new DataGridViewTextBoxColumn();
            this.Column_Xsthkm_Cpkm = new DataGridViewTextBoxColumn();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x296, 0x1cf);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fpzpz.Form.FPZPZSet\Aisino.Fwkp.Fpzpz.Form.FPZPZSet.xml");
            this.Column_Bm_Yskm.DataPropertyName = DingYiZhiFuChuan.KHBMCulmnDataName[0];
            this.Column_Bm_Yskm.HeaderText = DingYiZhiFuChuan.KHBMCulmnHeaderText[0];
            this.Column_Bm_Yskm.Name = DingYiZhiFuChuan.KHBMCulmnDataName[0];
            this.Column_Bm_Yskm.ReadOnly = true;
            this.Column_Bm_Yskm.ToolTipText = DingYiZhiFuChuan.KHBMCulmnHeaderText[0];
            this.Column_Mc_Yskm.DataPropertyName = DingYiZhiFuChuan.KHBMCulmnDataName[1];
            this.Column_Mc_Yskm.HeaderText = DingYiZhiFuChuan.KHBMCulmnHeaderText[1];
            this.Column_Mc_Yskm.Name = DingYiZhiFuChuan.KHBMCulmnDataName[1];
            this.Column_Mc_Yskm.ReadOnly = true;
            this.Column_Mc_Yskm.ToolTipText = DingYiZhiFuChuan.KHBMCulmnHeaderText[1];
            this.Column_Mc_Yskm.Width = 180;
            this.Column_Yskm_Yskm.DataPropertyName = DingYiZhiFuChuan.KHBMCulmnDataName[11];
            this.Column_Yskm_Yskm.HeaderText = DingYiZhiFuChuan.KHBMCulmnHeaderText[11];
            this.Column_Yskm_Yskm.MaxInputLength = DingYiZhiFuChuan.KMMaxInputLength;
            this.Column_Yskm_Yskm.Name = DingYiZhiFuChuan.KHBMCulmnDataName[11];
            this.Column_Yskm_Yskm.ToolTipText = DingYiZhiFuChuan.KHBMCulmnHeaderText[11];
            this.Column_Bm_Cpkm.DataPropertyName = DingYiZhiFuChuan.SPBMCulmnDataName[0];
            this.Column_Bm_Cpkm.HeaderText = DingYiZhiFuChuan.SPBMCulmnHeaderText[0];
            this.Column_Bm_Cpkm.Name = DingYiZhiFuChuan.SPBMCulmnDataName[0];
            this.Column_Bm_Cpkm.ReadOnly = true;
            this.Column_Bm_Cpkm.ToolTipText = DingYiZhiFuChuan.SPBMCulmnHeaderText[0];
            this.Column_Mc_Cpkm.DataPropertyName = DingYiZhiFuChuan.SPBMCulmnDataName[1];
            this.Column_Mc_Cpkm.HeaderText = DingYiZhiFuChuan.SPBMCulmnHeaderText[1];
            this.Column_Mc_Cpkm.Name = DingYiZhiFuChuan.SPBMCulmnDataName[1];
            this.Column_Mc_Cpkm.ReadOnly = true;
            this.Column_Mc_Cpkm.Width = 180;
            this.Column_Mc_Cpkm.ToolTipText = DingYiZhiFuChuan.SPBMCulmnHeaderText[1];
            this.Column_Xskm_Cpkm.DataPropertyName = DingYiZhiFuChuan.SPBMCulmnDataName[11];
            this.Column_Xskm_Cpkm.HeaderText = DingYiZhiFuChuan.SPBMCulmnHeaderText[11];
            this.Column_Xskm_Cpkm.MaxInputLength = DingYiZhiFuChuan.KMMaxInputLength;
            this.Column_Xskm_Cpkm.Name = DingYiZhiFuChuan.SPBMCulmnDataName[11];
            this.Column_Xskm_Cpkm.ReadOnly = false;
            this.Column_Xskm_Cpkm.ToolTipText = DingYiZhiFuChuan.SPBMCulmnHeaderText[11];
            this.Column_Yjzzs_Cpkm.DataPropertyName = DingYiZhiFuChuan.SPBMCulmnDataName[12];
            this.Column_Yjzzs_Cpkm.HeaderText = DingYiZhiFuChuan.SPBMCulmnHeaderText[12];
            this.Column_Yjzzs_Cpkm.MaxInputLength = DingYiZhiFuChuan.KMMaxInputLength;
            this.Column_Yjzzs_Cpkm.Name = DingYiZhiFuChuan.SPBMCulmnDataName[12];
            this.Column_Yjzzs_Cpkm.ReadOnly = false;
            this.Column_Yjzzs_Cpkm.ToolTipText = DingYiZhiFuChuan.SPBMCulmnHeaderText[12];
            this.Column_Xsthkm_Cpkm.DataPropertyName = DingYiZhiFuChuan.SPBMCulmnDataName[13];
            this.Column_Xsthkm_Cpkm.HeaderText = DingYiZhiFuChuan.SPBMCulmnHeaderText[13];
            this.Column_Xsthkm_Cpkm.MaxInputLength = DingYiZhiFuChuan.KMMaxInputLength;
            this.Column_Xsthkm_Cpkm.Name = DingYiZhiFuChuan.SPBMCulmnDataName[13];
            this.Column_Xsthkm_Cpkm.ReadOnly = false;
            this.Column_Xsthkm_Cpkm.ToolTipText = DingYiZhiFuChuan.SPBMCulmnHeaderText[13];
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x296, 0x1cf);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FPZPZSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "发票转凭证设置";
            base.ResumeLayout(false);
        }

        private void InitializeCtrlKMSet()
        {
            try
            {
                DingYiZhiFuChuan.GetLinkUserSuit();
                this.text_Xsthkm.Text = PropertyUtil.GetValue(DingYiZhiFuChuan.Xsthkm);
                this.text_Yjzzskm.Text = PropertyUtil.GetValue(DingYiZhiFuChuan.Yjzzskm);
                this.text_Xssrkm.Text = PropertyUtil.GetValue(DingYiZhiFuChuan.Xssrkm);
                this.text_Yskm.Text = PropertyUtil.GetValue(DingYiZhiFuChuan.Ysrkm);
                this.combo_YskmSetYj.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (string str in DingYiZhiFuChuan.YskmItem)
                {
                    this.combo_YskmSetYj.Items.Add(str);
                }
                string str2 = PropertyUtil.GetValue(DingYiZhiFuChuan.YskmItemValue);
                this.combo_YskmSetYj.SelectedItem = str2;
                if (string.IsNullOrEmpty(str2))
                {
                    this.combo_YskmSetYj.SelectedIndex = 0;
                    PropertyUtil.SetValue(DingYiZhiFuChuan.YskmItemValue, this.combo_YskmSetYj.SelectedItem.ToString());
                }
                this.combo_CpkmSetYj.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (string str3 in DingYiZhiFuChuan.CpkmItem)
                {
                    this.combo_CpkmSetYj.Items.Add(str3);
                }
                string str4 = PropertyUtil.GetValue(DingYiZhiFuChuan.CpkmItemValue);
                this.combo_CpkmSetYj.SelectedItem = str4;
                if (string.IsNullOrEmpty(str4))
                {
                    this.combo_CpkmSetYj.SelectedIndex = 0;
                    PropertyUtil.SetValue(DingYiZhiFuChuan.CpkmItemValue, this.combo_CpkmSetYj.SelectedItem.ToString());
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

        private void InitializeCtrlZDFS()
        {
            try
            {
                this.comb_Yskm_Zdfs.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (string str in DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItem)
                {
                    this.comb_Yskm_Zdfs.Items.Add(str);
                }
                string str2 = PropertyUtil.GetValue(DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItemValue);
                this.comb_Yskm_Zdfs.SelectedItem = str2;
                if (string.IsNullOrEmpty(str2))
                {
                    this.comb_Yskm_Zdfs.SelectedIndex = 0;
                    PropertyUtil.SetValue(DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItemValue, this.comb_Yskm_Zdfs.SelectedItem.ToString());
                }
                this.comb_Qtkm_Zdfs.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (string str3 in DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItem)
                {
                    this.comb_Qtkm_Zdfs.Items.Add(str3);
                }
                string str4 = PropertyUtil.GetValue(DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItemValue);
                this.comb_Qtkm_Zdfs.SelectedItem = str4;
                if (string.IsNullOrEmpty(str4))
                {
                    this.comb_Qtkm_Zdfs.SelectedIndex = 0;
                    PropertyUtil.SetValue(DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItemValue, this.comb_Qtkm_Zdfs.SelectedItem.ToString());
                }
                this.comb_Zfsfp_Zds.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (string str5 in DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItem)
                {
                    this.comb_Zfsfp_Zds.Items.Add(str5);
                }
                string str6 = PropertyUtil.GetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue);
                this.comb_Zfsfp_Zds.SelectedItem = str6;
                if (string.IsNullOrEmpty(str6))
                {
                    this.comb_Zfsfp_Zds.SelectedIndex = 0;
                    PropertyUtil.SetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue, this.comb_Zfsfp_Zds.SelectedItem.ToString());
                }
                this.comb_Xtsp_Zds.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (string str7 in DingYiZhiFuChuan.Xtsp_ZdsItem)
                {
                    this.comb_Xtsp_Zds.Items.Add(str7);
                }
                string str8 = PropertyUtil.GetValue(DingYiZhiFuChuan.Xtsp_ZdsItemValue);
                this.comb_Xtsp_Zds.SelectedItem = str8;
                if (string.IsNullOrEmpty(str8))
                {
                    this.comb_Xtsp_Zds.SelectedIndex = 0;
                    PropertyUtil.SetValue(DingYiZhiFuChuan.Xtsp_ZdsItemValue, this.comb_Xtsp_Zds.SelectedItem.ToString());
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

        private void InitKMSet()
        {
            try
            {
                this.InitializeCtrlKMSet();
                this.but_Select_YSKM.Visible = false;
                this.but_Select_CPKM.Visible = false;
                this.customStyleDataGrid_Yskm.SetColumnReadOnly(DingYiZhiFuChuan.KHBMCulmnDataName[0], true);
                this.customStyleDataGrid_Yskm.SetColumnReadOnly(DingYiZhiFuChuan.KHBMCulmnDataName[1], true);
                this.customStyleDataGrid_Yskm.SetColumnReadOnly(DingYiZhiFuChuan.KHBMCulmnDataName[11], false);
                this.customStyleDataGrid_Cpkm.SetColumnReadOnly(DingYiZhiFuChuan.SPBMCulmnDataName[0], true);
                this.customStyleDataGrid_Cpkm.SetColumnReadOnly(DingYiZhiFuChuan.SPBMCulmnDataName[1], true);
                this.customStyleDataGrid_Cpkm.SetColumnReadOnly(DingYiZhiFuChuan.SPBMCulmnDataName[11], false);
                this.customStyleDataGrid_Cpkm.SetColumnReadOnly(DingYiZhiFuChuan.SPBMCulmnDataName[12], false);
                this.customStyleDataGrid_Cpkm.SetColumnReadOnly(DingYiZhiFuChuan.SPBMCulmnDataName[13], false);
                this.combo_Yskm_Cpkm_SelectedIndexChanged(null, null);
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

        private void InitServerSet()
        {
            try
            {
                this.bGangGangShow = true;
                this.isSaved = false;
                this.PzInfoInit();
                this.but_GengHuan.Enabled = false;
                this.currentTab = this.tabFPZPZSet.SelectedTab;
                this.sUserGuide = string.Empty;
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

        private void InitZDFSSet()
        {
            try
            {
                this.InitializeCtrlZDFS();
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

        private void InsertData_Cpkm(List<SPBMModal> ListModel, CpkmType cpkmType)
        {
            try
            {
                DataTable table;
                if (!this._CpkmType.Equals(cpkmType))
                {
                    this._CpkmType = cpkmType;
                    if (this.customStyleDataGrid_Cpkm.DataSource != null)
                    {
                        ((DataTable) this.customStyleDataGrid_Cpkm.DataSource).Clear();
                    }
                    if ((ListModel != null) && (ListModel.Count > 0))
                    {
                        table = new DataTable();
                        table.Columns.Add(DingYiZhiFuChuan.SPBMCulmnDataName[0], typeof(string));
                        table.Columns.Add(DingYiZhiFuChuan.SPBMCulmnDataName[1], typeof(string));
                        table.Columns.Add(DingYiZhiFuChuan.SPBMCulmnDataName[11], typeof(string));
                        table.Columns.Add(DingYiZhiFuChuan.SPBMCulmnDataName[12], typeof(string));
                        table.Columns.Add(DingYiZhiFuChuan.SPBMCulmnDataName[13], typeof(string));
                        switch (cpkmType)
                        {
                            case CpkmType.CHFL:
                            case CpkmType.CH:
                                this.customStyleDataGrid_Cpkm.Columns[0].HeaderText = DingYiZhiFuChuan.SPBMCulmnHeaderText[0];
                                this.customStyleDataGrid_Cpkm.Columns[1].HeaderText = DingYiZhiFuChuan.SPBMCulmnHeaderText[1];
                                this.customStyleDataGrid_Cpkm.Columns[2].HeaderText = DingYiZhiFuChuan.SPBMCulmnHeaderText[11];
                                this.customStyleDataGrid_Cpkm.Columns[3].HeaderText = DingYiZhiFuChuan.SPBMCulmnHeaderText[12];
                                this.customStyleDataGrid_Cpkm.Columns[4].HeaderText = DingYiZhiFuChuan.SPBMCulmnHeaderText[13];
                                foreach (SPBMModal modal in ListModel)
                                {
                                    DataRow row = table.NewRow();
                                    row[DingYiZhiFuChuan.SPBMCulmnDataName[0]] = modal.BM;
                                    row[DingYiZhiFuChuan.SPBMCulmnDataName[1]] = modal.MC;
                                    row[DingYiZhiFuChuan.SPBMCulmnDataName[11]] = modal.XSSRKM;
                                    row[DingYiZhiFuChuan.SPBMCulmnDataName[12]] = modal.YJZZSKM;
                                    row[DingYiZhiFuChuan.SPBMCulmnDataName[13]] = modal.XSTHKM;
                                    table.Rows.Add(row);
                                }
                                goto Label_0246;
                        }
                    }
                }
                return;
            Label_0246:
                this.customStyleDataGrid_Cpkm.DataSource = table;
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

        private void InsertData_Yskm(List<KHBMModal> ListModel, YskmType yskmType)
        {
            try
            {
                DataTable table;
                if (!this._YskmType.Equals(yskmType))
                {
                    this._YskmType = yskmType;
                    if (this.customStyleDataGrid_Yskm.DataSource != null)
                    {
                        ((DataTable) this.customStyleDataGrid_Yskm.DataSource).Clear();
                    }
                    if ((ListModel != null) && (ListModel.Count > 0))
                    {
                        table = new DataTable();
                        table.Columns.Add(DingYiZhiFuChuan.KHBMCulmnDataName[0], typeof(string));
                        table.Columns.Add(DingYiZhiFuChuan.KHBMCulmnDataName[1], typeof(string));
                        table.Columns.Add(DingYiZhiFuChuan.KHBMCulmnDataName[11], typeof(string));
                        switch (yskmType)
                        {
                            case YskmType.KHFL:
                            case YskmType.KH:
                                this.customStyleDataGrid_Yskm.Columns[0].HeaderText = DingYiZhiFuChuan.KHBMCulmnHeaderText[0];
                                this.customStyleDataGrid_Yskm.Columns[1].HeaderText = DingYiZhiFuChuan.KHBMCulmnHeaderText[1];
                                this.customStyleDataGrid_Yskm.Columns[2].HeaderText = DingYiZhiFuChuan.KHBMCulmnHeaderText[11];
                                foreach (KHBMModal modal in ListModel)
                                {
                                    DataRow row = table.NewRow();
                                    row[DingYiZhiFuChuan.KHBMCulmnDataName[0]] = modal.BM;
                                    row[DingYiZhiFuChuan.KHBMCulmnDataName[1]] = modal.MC;
                                    row[DingYiZhiFuChuan.KHBMCulmnDataName[11]] = modal.YSKM;
                                    table.Rows.Add(row);
                                }
                                goto Label_0281;

                            case YskmType.DQFL:
                                goto Label_01A8;
                        }
                    }
                }
                return;
            Label_01A8:
                this.customStyleDataGrid_Yskm.Columns[0].HeaderText = DingYiZhiFuChuan.KHBMCulmnHeaderText[12];
                this.customStyleDataGrid_Yskm.Columns[1].HeaderText = DingYiZhiFuChuan.KHBMCulmnHeaderText[13];
                this.customStyleDataGrid_Yskm.Columns[2].HeaderText = DingYiZhiFuChuan.KHBMCulmnHeaderText[11];
                foreach (KHBMModal modal2 in ListModel)
                {
                    DataRow row2 = table.NewRow();
                    row2[DingYiZhiFuChuan.KHBMCulmnDataName[0]] = modal2.DQBM;
                    row2[DingYiZhiFuChuan.KHBMCulmnDataName[1]] = modal2.DQMC;
                    row2[DingYiZhiFuChuan.KHBMCulmnDataName[11]] = modal2.DQKM;
                    table.Rows.Add(row2);
                }
            Label_0281:
                this.customStyleDataGrid_Yskm.DataSource = table;
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
                            break;
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
                    if (!string.IsNullOrEmpty(this.A6GetUser(this.combo_IP.Text.Trim(), suitNo)))
                    {
                        return DingYiZhiFuChuan.strErrLinkFailTip;
                    }
                    string str10 = string.Empty;
                    foreach (string str11 in this._A6UserList)
                    {
                        string str12 = str11.Substring(str11.IndexOf("#") + 1);
                        if (str12.Trim() == this.combo_User.Text.Trim())
                        {
                            str10 = str12.Trim();
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(str10))
                    {
                        return DingYiZhiFuChuan.strErrLinkFailTip;
                    }
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                return DingYiZhiFuChuan.strErrLinkFailTip;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
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
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
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
                    if (str3.IndexOf("=") > -1)
                    {
                        this.combo_User.Items.Add(str3.Substring(str3.IndexOf("=") + 1));
                    }
                    else
                    {
                        this.combo_User.Items.Add(str3);
                    }
                    this.combo_User.SelectedIndex = 0;
                    this.isSaved = true;
                    return flag;
                }
                this.isSaved = false;
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
            return flag;
        }

        private bool SaveServerSet()
        {
            string str = this.IsRightA6Info();
            if (!string.IsNullOrEmpty(str))
            {
                Aisino.Fwkp.Fpzpz.Common.Tool.PzInterFaceLinkInfo(str, this._Loger);
                return false;
            }
            string str2 = PropertyUtil.GetValue(DingYiZhiFuChuan.A6ServerLinkUtil);
            string str3 = PropertyUtil.GetValue(DingYiZhiFuChuan.A6SuitUtil);
            string str4 = PropertyUtil.GetValue(DingYiZhiFuChuan.A6GuidUtil);
            if ((!str2.Equals(this.combo_IP.Text.Trim()) || !str3.Equals(this.combo_ZhangTao.Text.Trim())) || !str4.Substring(str4.IndexOf("=") + 1).Equals(this.combo_User.Text.Trim()))
            {
                PropertyUtil.SetValue(DingYiZhiFuChuan.A6ServerLinkUtil, this.combo_IP.Text.Trim());
                PropertyUtil.SetValue(DingYiZhiFuChuan.A6SuitUtil, this.combo_ZhangTao.Text.Trim());
                PropertyUtil.SetValue(DingYiZhiFuChuan.A6GuidUtil, this.sUserGuide.Trim());
            }
            DingYiZhiFuChuan.A6ServerLink = this.combo_IP.Text.Trim();
            DingYiZhiFuChuan.A6SuitGuid = this.combo_ZhangTao.Text.Trim();
            DingYiZhiFuChuan.A6UserGuid = this.sUserGuide.Trim();
            return true;
        }

        private void SetKMSetCtrlEnabled(bool bEnabled)
        {
            try
            {
                this.but_Xsthkm.Enabled = bEnabled;
                this.text_Xsthkm.Enabled = bEnabled;
                this.but_Yjzzskm.Enabled = bEnabled;
                this.text_Yjzzskm.Enabled = bEnabled;
                this.but_Xssrkm.Enabled = bEnabled;
                this.text_Xssrkm.Enabled = bEnabled;
                this.but_Yskm.Enabled = bEnabled;
                this.text_Yskm.Enabled = bEnabled;
                this.customStyleDataGrid_Yskm.ReadOnly = !bEnabled;
                this.combo_YskmSetYj.Enabled = bEnabled;
                this.combo_CpkmSetYj.Enabled = bEnabled;
                this.customStyleDataGrid_Cpkm.ReadOnly = !bEnabled;
                this.but_Select_YSKM.Enabled = bEnabled;
                this.but_Select_CPKM.Enabled = bEnabled;
                this.customStyleDataGrid_Yskm.SetColumnReadOnly(DingYiZhiFuChuan.KHBMCulmnDataName[11], !bEnabled);
                this.customStyleDataGrid_Cpkm.SetColumnReadOnly(DingYiZhiFuChuan.SPBMCulmnDataName[11], !bEnabled);
                this.customStyleDataGrid_Cpkm.SetColumnReadOnly(DingYiZhiFuChuan.SPBMCulmnDataName[12], !bEnabled);
                this.customStyleDataGrid_Cpkm.SetColumnReadOnly(DingYiZhiFuChuan.SPBMCulmnDataName[13], !bEnabled);
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

        private void SetZDFSCtrlEnabled(bool bEnabled)
        {
            try
            {
                this.comb_Yskm_Zdfs.Enabled = bEnabled;
                this.comb_Qtkm_Zdfs.Enabled = bEnabled;
                this.comb_Zfsfp_Zds.Enabled = bEnabled;
                this.comb_Xtsp_Zds.Enabled = bEnabled;
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

        private void ShowBut_Select(int iColumnIndex, int iRowIndex)
        {
            try
            {
                AisinoBTN obtn = null;
                CustomStyleDataGrid grid = null;
                switch (this._YskmOrCpkmTabType)
                {
                    case YskmOrCpkmTabType.YskmTab:
                        obtn = this.but_Select_YSKM;
                        if (!this._YskmType.Equals(YskmType.ERROR))
                        {
                            break;
                        }
                        obtn.Visible = false;
                        return;

                    case YskmOrCpkmTabType.CpkmTab:
                        obtn = this.but_Select_CPKM;
                        if (!this._CpkmType.Equals(CpkmType.ERROR))
                        {
                            goto Label_007C;
                        }
                        obtn.Visible = false;
                        return;

                    default:
                        return;
                }
                grid = this.customStyleDataGrid_Yskm;
                goto Label_008A;
            Label_007C:
                grid = this.customStyleDataGrid_Cpkm;
            Label_008A:
                if ((-1 == iColumnIndex) || (-1 == iRowIndex))
                {
                    iColumnIndex = grid.CurrentCell.ColumnIndex;
                    iRowIndex = grid.CurrentCell.RowIndex;
                }
                DataGridViewCell cell = grid[iColumnIndex, iRowIndex];
                string name = cell.OwningColumn.Name;
                if ((!name.Equals(DingYiZhiFuChuan.KHBMCulmnDataName[11]) && !name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[11])) && (!name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[12]) && !name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[13])))
                {
                    obtn.Visible = false;
                }
                else
                {
                    obtn.Visible = true;
                    int x = grid.Location.X;
                    int y = grid.Location.Y;
                    Rectangle rectangle = grid.GetCellDisplayRectangle(iColumnIndex, iRowIndex, false);
                    int num3 = rectangle.X;
                    int num4 = rectangle.Y;
                    int num5 = x + num3;
                    obtn.Height = rectangle.Height - 1;
                    obtn.Width = 0x15;
                    obtn.Top = rectangle.Y;
                    obtn.Left = (num5 + rectangle.Width) - 0x16;
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

        private void tabFPZPZSet_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                e.TabPage.BackColor = Color.White;
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
                    if ((e.TabPageIndex == 2) || (e.TabPageIndex == 3))
                    {
                        this.combo_Yskm_Cpkm_SelectedIndexChanged(null, null);
                    }
                    this.currentTab = e.TabPage;
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

        private void text_Yskm_Xsth_Yjzzs_Xssr_Xsthkm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.KeyChar = this.ConvertQJToBJ(e.KeyChar);
                if (((Convert.ToInt32(e.KeyChar) < 0x30) || (Convert.ToInt32(e.KeyChar) > 0x39)) && (Convert.ToInt32(e.KeyChar) != 8))
                {
                    e.Handled = true;
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

        private void text_Yskm_Xsth_Yjzzs_Xssr_Xsthkm_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AisinoTXT otxt = (AisinoTXT) sender;
                if (otxt != null)
                {
                    string name = otxt.Name;
                    string text = otxt.Text;
                    if (name.Equals("text_Yskm"))
                    {
                        if (!PropertyUtil.GetValue(DingYiZhiFuChuan.Ysrkm).Equals(text))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.Ysrkm, text);
                        }
                    }
                    else if (name.Equals("text_Xssrkm"))
                    {
                        if (!PropertyUtil.GetValue(DingYiZhiFuChuan.Xssrkm).Equals(text))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.Xssrkm, text);
                        }
                    }
                    else if (name.Equals("text_Yjzzskm"))
                    {
                        if (!PropertyUtil.GetValue(DingYiZhiFuChuan.Yjzzskm).Equals(text))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.Yjzzskm, text);
                        }
                    }
                    else if (name.Equals("text_Xsthkm") && !PropertyUtil.GetValue(DingYiZhiFuChuan.Xsthkm).Equals(text))
                    {
                        PropertyUtil.SetValue(DingYiZhiFuChuan.Xsthkm, text);
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

        private void tool_CanShu_Click(object sender, EventArgs e)
        {
            try
            {
                new QiYeGuanLiRuanJianMsgSet().ShowDialog();
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

        private enum CpkmType
        {
            CHFL,
            CH,
            ERROR
        }

        private enum YskmOrCpkmTabType
        {
            YskmTab,
            CpkmTab,
            ERROR
        }

        private enum YskmType
        {
            KHFL,
            KH,
            DQFL,
            ERROR
        }
    }
}

