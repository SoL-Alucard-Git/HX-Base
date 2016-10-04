namespace Aisino.Fwkp.Fpzpz.Form
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpzpz.Common;
    using Aisino.Fwkp.Fpzpz.IBLL;
    using Aisino.Fwkp.Fpzpz.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class KeMuSet : BaseForm
    {
        private CpkmType _CpkmType = CpkmType.ERROR;
        private ILog _Loger = LogUtil.GetLogger<KeMuSet>();
        private YskmOrCpkmTabType _YskmOrCpkmTabType = YskmOrCpkmTabType.ERROR;
        private YskmType _YskmType = YskmType.ERROR;
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
        private AisinoCMB combo_CpkmSetYj;
        private AisinoCMB combo_YskmSetYj;
        private IContainer components;
        private CustomStyleDataGrid customStyleDataGrid_Cpkm;
        private CustomStyleDataGrid customStyleDataGrid_Yskm;
        private IKHBM khbmBll = new KHBM();
        private AisinoLBL label6;
        private ISPBM spbmBll = new SPBM();
        private AisinoTAB tabControl1;
        private TabPage tabPage_ChanPinKeMu;
        private TabPage tabPage_JiBenKeMu;
        private TabPage tabPage_YingShouKeMu;
        private AisinoTXT text_Xssrkm;
        private AisinoTXT text_Xsthkm;
        private AisinoTXT text_Yjzzskm;
        private AisinoTXT text_Yskm;
        private ToolStripButton tool_CanShu;
        private ToolStripButton tool_Edit;
        private ToolStripButton tool_Quit;
        private XmlComponentLoader xmlComponentLoader1;

        public KeMuSet()
        {
            try
            {
                this.Initialize();
                this.InitializeCtrl();
                this.tool_Edit.Checked = true;
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

        private void but_Select_YSKM_CPKM_Click(object sender, EventArgs e)
        {
            try
            {
                string subject;
                CustomStyleDataGrid grid;
                if (DingYiZhiFuChuan.isA6PzVersion)
                {
                    bool bSuccess = false;
                    subject = this.GetSubject(out bSuccess);
                    if (bSuccess)
                    {
                        this.GetYskmOrCpkmTabType();
                        grid = null;
                        switch (this._YskmOrCpkmTabType)
                        {
                            case YskmOrCpkmTabType.YskmTab:
                                grid = this.customStyleDataGrid_Yskm;
                                goto Label_004E;

                            case YskmOrCpkmTabType.CpkmTab:
                                grid = this.customStyleDataGrid_Cpkm;
                                goto Label_004E;
                        }
                    }
                }
                return;
            Label_004E:
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
                if (DingYiZhiFuChuan.isA6PzVersion)
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
                this.GetYskmOrCpkmTabType();
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
                            goto Label_0160;
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
            Label_0160:
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
                this.GetYskmOrCpkmTabType();
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
                            goto Label_0079;
                        }
                        return;

                    default:
                        return;
                }
                if ((currentCell.OwningRow != null) && currentCell.OwningColumn.Name.Equals(DingYiZhiFuChuan.KHBMCulmnDataName[11]))
                {
                    goto Label_00DA;
                }
                return;
            Label_0079:
                if ((cell2.OwningRow == null) || ((!cell2.OwningColumn.Name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[11]) && !cell2.OwningColumn.Name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[12])) && !cell2.OwningColumn.Name.Equals(DingYiZhiFuChuan.SPBMCulmnDataName[13])))
                {
                    return;
                }
            Label_00DA:
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

        private bool GetTool_EditCheckedState()
        {
            try
            {
                return this.tool_Edit.Checked;
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
            return false;
        }

        private YskmOrCpkmTabType GetYskmOrCpkmTabType()
        {
            try
            {
                YskmOrCpkmTabType eRROR = YskmOrCpkmTabType.ERROR;
                if (this.tabControl1.SelectedIndex.Equals(1))
                {
                    eRROR = YskmOrCpkmTabType.YskmTab;
                }
                else if (this.tabControl1.SelectedIndex.Equals(2))
                {
                    eRROR = YskmOrCpkmTabType.CpkmTab;
                }
                if (!this._YskmOrCpkmTabType.Equals(eRROR))
                {
                    this._YskmOrCpkmTabType = eRROR;
                }
                else
                {
                    return YskmOrCpkmTabType.ERROR;
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
            this.tabControl1 = this.xmlComponentLoader1.GetControlByName<AisinoTAB>("tabControl1");
            this.tabPage_JiBenKeMu = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage_JiBenKeMu");
            this.tabPage_YingShouKeMu = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage_YingShouKeMu");
            this.tabPage_ChanPinKeMu = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage_ChanPinKeMu");
            this.but_Select_YSKM = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_Select_YSKM");
            this.but_Select_CPKM = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_Select_CPKM");
            this.tool_Quit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Quit");
            this.tool_Edit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Edit");
            this.tool_CanShu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_CanShu");
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
            this.customStyleDataGrid_Yskm.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.customStyleDataGrid_Yskm.MultiSelect = false;
            this.customStyleDataGrid_Yskm.ColumnAdd(this.Column_Bm_Yskm);
            this.customStyleDataGrid_Yskm.ColumnAdd(this.Column_Mc_Yskm);
            this.customStyleDataGrid_Yskm.ColumnAdd(this.Column_Yskm_Yskm);
            this.customStyleDataGrid_Yskm.Controls.Add(this.but_Select_YSKM);
            this.customStyleDataGrid_Yskm.AllowUserToAddRows = false;
            this.customStyleDataGrid_Cpkm.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.customStyleDataGrid_Cpkm.MultiSelect = false;
            this.customStyleDataGrid_Cpkm.ColumnAdd(this.Column_Bm_Cpkm);
            this.customStyleDataGrid_Cpkm.ColumnAdd(this.Column_Mc_Cpkm);
            this.customStyleDataGrid_Cpkm.ColumnAdd(this.Column_Xskm_Cpkm);
            this.customStyleDataGrid_Cpkm.ColumnAdd(this.Column_Yjzzs_Cpkm);
            this.customStyleDataGrid_Cpkm.ColumnAdd(this.Column_Xsthkm_Cpkm);
            this.customStyleDataGrid_Cpkm.Controls.Add(this.but_Select_CPKM);
            this.customStyleDataGrid_Cpkm.AllowUserToAddRows = false;
            this.tool_Quit.Click += new EventHandler(this.tool_Quit_Click);
            this.tool_Edit.Click += new EventHandler(this.tool_Edit_Click);
            this.tool_CanShu.Click += new EventHandler(this.tool_CanShu_Click);
            this.but_Xsthkm.Click += new EventHandler(this.but_Yskm_Xsth_Yjzzs_XssrKm_Click);
            this.but_Yjzzskm.Click += new EventHandler(this.but_Yskm_Xsth_Yjzzs_XssrKm_Click);
            this.but_Xssrkm.Click += new EventHandler(this.but_Yskm_Xsth_Yjzzs_XssrKm_Click);
            this.but_Yskm.Click += new EventHandler(this.but_Yskm_Xsth_Yjzzs_XssrKm_Click);
            this.but_Select_YSKM.Click += new EventHandler(this.but_Select_YSKM_CPKM_Click);
            this.customStyleDataGrid_Yskm.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.DataGrid_YskmSet_CpkmSet_EditingControlShowing);
            this.customStyleDataGrid_Yskm.CellEnter += new DataGridViewCellEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellEnter);
            this.customStyleDataGrid_Yskm.CellPainting += new DataGridViewCellPaintingEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellPainting);
            this.customStyleDataGrid_Yskm.CellValueChanged += new DataGridViewCellEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellValueChanged);
            this.customStyleDataGrid_Yskm.DoubleClick += new EventHandler(this.customStyleDataGrid_Yskm_Cpkm_DoubleClick);
            this.but_Select_CPKM.Click += new EventHandler(this.but_Select_YSKM_CPKM_Click);
            this.customStyleDataGrid_Cpkm.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.DataGrid_YskmSet_CpkmSet_EditingControlShowing);
            this.customStyleDataGrid_Cpkm.CellEnter += new DataGridViewCellEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellEnter);
            this.customStyleDataGrid_Cpkm.CellPainting += new DataGridViewCellPaintingEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellPainting);
            this.customStyleDataGrid_Cpkm.CellValueChanged += new DataGridViewCellEventHandler(this.customStyleDataGrid_Yskm_Cpkm_CellValueChanged);
            this.customStyleDataGrid_Cpkm.DoubleClick += new EventHandler(this.customStyleDataGrid_Yskm_Cpkm_DoubleClick);
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
            this.combo_YskmSetYj.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Cpkm_SelectedIndexChanged);
            this.combo_CpkmSetYj.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Cpkm_SelectedIndexChanged);
            this.tabControl1.Selected += new TabControlEventHandler(this.tabControl1_Selected);
            base.FormClosing += new FormClosingEventHandler(this.KeMuSet_FormClosing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(KeMuSet));
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
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x288, 0x1c3);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "科目设置";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fpzpz.Form.KeMuSet\Aisino.Fwkp.Fpzpz.Form.KeMuSet.xml");
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
            base.ClientSize = new Size(0x288, 0x1c3);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "KeMuSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "科目设置";
            base.ResumeLayout(false);
        }

        private void InitializeCtrl()
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

        private void KeMuSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.GetYskmOrCpkmTabType();
                CustomStyleDataGrid grid = null;
                switch (this._YskmOrCpkmTabType)
                {
                    case YskmOrCpkmTabType.YskmTab:
                        grid = this.customStyleDataGrid_Yskm;
                        break;

                    case YskmOrCpkmTabType.CpkmTab:
                        grid = this.customStyleDataGrid_Cpkm;
                        break;

                    default:
                        return;
                }
                if ((this.CellEdit != null) && (grid.CurrentCell != null))
                {
                    grid.CurrentCell.Value = this.CellEdit.Text;
                }
                PropertyUtil.Save();
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

        private void SetAllCtrlEnabled(bool bEnabled)
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

        private void ShowBut_Select(int iColumnIndex, int iRowIndex)
        {
            try
            {
                AisinoBTN obtn = null;
                CustomStyleDataGrid grid = null;
                this.GetYskmOrCpkmTabType();
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
                            goto Label_0083;
                        }
                        obtn.Visible = false;
                        return;

                    default:
                        return;
                }
                grid = this.customStyleDataGrid_Yskm;
                goto Label_0091;
            Label_0083:
                grid = this.customStyleDataGrid_Cpkm;
            Label_0091:
                if (!this.GetTool_EditCheckedState())
                {
                    obtn.Visible = false;
                }
                else if (grid.CurrentCell == null)
                {
                    obtn.Visible = false;
                }
                else
                {
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

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
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

        private void text_Yskm_Xsth_Yjzzs_Xssr_Xsthkm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                AisinoTXT otxt = (AisinoTXT) sender;
                if ((!char.IsNumber(e.KeyChar) && !char.IsPunctuation(e.KeyChar)) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                else if (char.IsPunctuation(e.KeyChar))
                {
                    if ((e.KeyChar == '.') || (otxt.Text.Length == 0))
                    {
                        e.Handled = true;
                    }
                    if (otxt.Text.LastIndexOf('.') != -1)
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

        private void tool_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                this.tool_Edit.Checked = !this.tool_Edit.Checked;
                this.SetAllCtrlEnabled(this.tool_Edit.Checked);
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

        private void tool_Quit_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
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

