namespace Aisino.Fwkp.Fpkj.Form.FPCX
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.DAL;
    using Aisino.Fwkp.Fpkj.Form.DKFP;
    using Aisino.Fwkp.Fpkj.Form.Dy;
    using Aisino.Fwkp.Fpkj.Form.FPXF;
    using Aisino.Fwkp.Print;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    public class FaPiaoChaXun : DockForm
    {
        private string _FpzlKind = " ";
        private int _iPageNO;
        private int _iPageSize;
        private object[] _Object_FPFZ = new object[] { "", "", "" };
        private string _QuickSearchText = "请输入检索关键字...";
        private SearchType _SearchType = SearchType.DATESearch;
        private int _StartMonth;
        private int _StartYear;
        private string _strFilePath = string.Empty;
        private XMLOperation _XmlOperation = new XMLOperation();
        public static AisinoDataGrid aisinoGrid;
        public int AvePageNum = 30;
        public static DateTime CardClock;
        private Aisino.Fwkp.Fpkj.Form.FPCX.ChaXunTiaoJian chaXunTiaoJian;
        private IContainer components;
        private ContextMenuStrip contextMenu_BSZT;
        private ContextMenuStrip contextMenu_DaYin;
        private EditFPCX editFPCX;
        private List<FpPrint> FpPrintList = new List<FpPrint>();
        private Hashtable FpPrintResultHash = new Hashtable();
        private bool IsInitSearch;
        private ILog loger = LogUtil.GetLogger<FaPiaoChaXun>();
        private ToolStripMenuItem MenuItem_FP;
        private ToolStripMenuItem MenuItem_FPLB;
        private ToolStripMenuItem MenuItem_Weibaosong;
        private ToolStripMenuItem MenuItem_XHQD;
        private ToolStripMenuItem MenuItem_Yanqianshibai;
        public int Month;
        private FPProgressBar progressBar;
        private int sqlType;
        private int step = 0x7d0;
        private DateTime TaxCardClock;
        private Aisino.Fwkp.Fpkj.Common.Tool tool = new Aisino.Fwkp.Fpkj.Common.Tool();
        private ToolStripButton tool_ChaKanMingXi;
        private ToolStripButton tool_chazhao;
        private ToolStripButton tool_DaikaiSC;
        private ToolStripButton tool_daying;
        private ToolStripButton tool_Exit;
        private ToolStripDropDownButton tool_fasong;
        private ToolStripSplitButton tool_FPCXBSZT;
        private ToolStripButton tool_FPDaoChu;
        private ToolStripButton tool_FPZJDaoChu;
        private ToolStripComboBox tool_Fpzl;
        private ToolStripButton tool_GeShi;
        private ToolStripButton tool_hedui;
        private ToolStripComboBox tool_Month;
        private ToolStripMenuItem tool_SendDisk;
        private ToolStripMenuItem tool_SendMail;
        private ToolStripMenuItem tool_SendToWebService;
        private ToolStripButton tool_tongji;
        private ToolStripComboBox tool_Year;
        private ToolStripButton tool_ZuoFei;
        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripTextBox toolTxt_quick;
        private XmlComponentLoader xmlComponentLoader1;
        public XXFP xxfpChaXunBll = new XXFP(false);
        public int Year = 0x7d0;

        public FaPiaoChaXun()
        {
            try
            {
                if (base.TaxCardInstance != null)
                {
                    this.TaxCardClock = base.TaxCardInstance.GetCardClock();
                    CardClock = this.TaxCardClock;
                    this.Initialize();
                    this.InsertColumn();
                    if (this.chaXunTiaoJian == null)
                    {
                        this.chaXunTiaoJian = new Aisino.Fwkp.Fpkj.Form.FPCX.ChaXunTiaoJian(CardClock, this);
                    }
                    this.ReadStartDate();
                    this.toolTxt_quick.Text = this._QuickSearchText;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void aisinoGrid_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                this.SetBtnEnabled();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void aisinoGrid_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                if ((this.editFPCX == EditFPCX.FuZhi) && this.GetValueFPFZ())
                {
                    base.Close();
                }
                else if (this.IsEmptyFPCurrentData(aisinoGrid.CurrentCell, string.Empty))
                {
                    MessageManager.ShowMsgBox("FPCX-000003");
                }
                else
                {
                    DataGridViewRowCollection rowsAll = aisinoGrid.Rows;
                    this.ChaKanMingXi(rowsAll);
                    this.SetBtnEnabled();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void aisinoGrid_DataGridRowSelectionChanged(object sender, DataGridRowEventArgs e)
        {
            try
            {
                this.SetBtnEnabled();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void aisinoGrid_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                PropertyUtil.SetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXun_aisinoGrid_PageSize", e.PageSize.ToString());
                this.ChaXunDate(e.PageNO, e.PageSize, 0, 0, this.sqlType);
                this.SetBtnEnabled();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private List<Fpxx> aisinoGridView_SelectRows(bool swdkBZ = false)
        {
            try
            {
                if (aisinoGrid.SelectedRows == null)
                {
                    return null;
                }
                if (0 >= aisinoGrid.SelectedRows.Count)
                {
                    return null;
                }
                List<Fpxx> list = new List<Fpxx>();
                foreach (DataGridViewRow row in aisinoGrid.SelectedRows)
                {
                    string fPDM = new string('0', 15);
                    string data = new string('0', 8);
                    string str3 = ShareMethods.CellToString(row.Cells["FPZL"]);
                    _InvoiceType invoiceType = this.GetInvoiceType(str3);
                    fPDM = ShareMethods.CellToString(row.Cells["FPDM"]);
                    data = ShareMethods.CellToString(row.Cells["FPHM"]);
                    Fpxx item = this.xxfpChaXunBll.GetModel(invoiceType.dbfpzl, fPDM, Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(data), "");
                    if (!swdkBZ)
                    {
                        if (!string.IsNullOrEmpty(item.mw.Trim()) && !item.zfbz)
                        {
                            list.Add(item);
                        }
                    }
                    else
                    {
                        list.Add(item);
                    }
                }
                if (0 >= list.Count)
                {
                    return null;
                }
                return list;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
                return null;
            }
        }

        private void aisinoGridViewUnSelectedRows(DataGridViewSelectedRowCollection selectedRows)
        {
            foreach (DataGridViewRow row in selectedRows)
            {
                row.Selected = false;
            }
        }

        private void btnChazhao_Click(object sender, EventArgs e)
        {
            try
            {
                this.chaXunTiaoJian.AisinoGrid = aisinoGrid;
                if (this.chaXunTiaoJian.ShowDialog(this) == DialogResult.OK)
                {
                    this.AvePageNum = this._iPageSize;
                    this.sqlType = 0;
                    this.ChaXunDate(1, this.AvePageNum, 0, -1, this.sqlType);
                    this.SetBtnEnabled();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
            finally
            {
                this.chaXunTiaoJian.IsFpzl = false;
            }
        }

        private void btnDaYing_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editFPCX == EditFPCX.ChaXun)
                {
                    Rectangle bounds = this.tool_daying.Bounds;
                    Point p = new Point(bounds.X, bounds.Y + bounds.Height);
                    if (this.contextMenu_DaYin != null)
                    {
                        this.contextMenu_DaYin.Show(base.PointToScreen(p));
                    }
                }
                else
                {
                    this.MenuItem_FPLBClick(null, null);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void btnExitClick(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void btnHeDui_Click(object sender, EventArgs e)
        {
            try
            {
                if ((aisinoGrid.SelectedRows != null) && (0 < aisinoGrid.SelectedRows.Count))
                {
                    DataGridViewRow row = aisinoGrid.SelectedRows[0];
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Clear();
                    dict.Add("lbl_FpDm", row.Cells["FPDM"].Value.ToString());
                    dict.Add("lbl_FpHm", Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(row.Cells["FPHM"].Value.ToString()));
                    dict.Add("lbl_DZSYH", row.Cells["DZSYH"].Value.ToString());
                    dict.Add("lbl_KPSXH", row.Cells["KPSXH"].Value.ToString());
                    dict.Add("lbl_KpRq", row.Cells["KPRQ"].Value.ToString());
                    HeDui dui = new HeDui();
                    if (dui.setValue(dict))
                    {
                        dui.ShowDialog();
                        if (dui != null)
                        {
                            dui.Close();
                            dui.Dispose();
                            dui = null;
                        }
                        GC.Collect();
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void but_DaikaiShangchuan_Click(object sender, EventArgs e)
        {
            if ((aisinoGrid == null) || (aisinoGrid.SelectedRows.Count <= 0))
            {
                MessageManager.ShowMsgBox("没有选择任何行！");
            }
            else
            {
                DaiKaiXml xml = new DaiKaiXml();
                List<Fpxx> fpList = this.aisinoGridView_SelectRows(true);
                xml.DaiKaiFpUpload_New(fpList);
                fpList = null;
            }
        }

        private void but_FPDaoChu_Click(object sender, EventArgs e)
        {
            try
            {
                Aisino.Fwkp.Fpkj.Form.FPCX.SelectTime time = new Aisino.Fwkp.Fpkj.Form.FPCX.SelectTime {
                    Text = "销项发票导出"
                };
                if (DialogResult.OK == time.ShowDialog())
                {
                    SelectSSQ selectSsq = new SelectSSQ();
                    if (DialogResult.OK == selectSsq.ShowDialog(this))
                    {
                        FolderBrowserDialog dialog = new FolderBrowserDialog();
                        string str = string.Empty;
                        if (!string.IsNullOrEmpty(str))
                        {
                            if (Directory.Exists(str))
                            {
                                dialog.SelectedPath = str;
                            }
                        }
                        else
                        {
                            dialog.SelectedPath = Application.StartupPath;
                        }
                        if (dialog.ShowDialog() != DialogResult.OK)
                        {
                            dialog.Dispose();
                        }
                        else
                        {
                            dialog.Dispose();
                            if (this.progressBar == null)
                            {
                                this.progressBar = new FPProgressBar();
                            }
                            this.progressBar.SetTip("正在生成销项发票记录", "请等待任务完成", "销项发票导出过程");
                            this.progressBar.fpxf_progressBar.Value = 1;
                            this.progressBar.Visible = true;
                            this.progressBar.Show();
                            this.progressBar.Refresh();
                            this.ProcessStartThread(this.step);
                            this.progressBar.Refresh();
                            List<Fpxx> xxfps = this.xxfpChaXunBll.SelectFpxx_FPDaoChu(Aisino.Fwkp.Fpkj.Form.FPCX.SelectTime.OutPutCondition);
                            if ((xxfps == null) || (xxfps.Count == 0))
                            {
                                this.progressBar.Visible = false;
                                MessageManager.ShowMsgBox("没有对应查询条件的数据记录");
                            }
                            else
                            {
                                this.progressBar.SetTip("正在生成导出文件", "请等待任务完成", "销项发票导出过程");
                                this.ProcessStartThread(this.step * 4);
                                XxfpOutUtils utils = new XxfpOutUtils();
                                if (utils.genXxfpInfo(xxfps, selectSsq, base.TaxCardInstance, dialog.SelectedPath))
                                {
                                    this.progressBar.Visible = false;
                                    this.progressBar.Refresh();
                                    MessageManager.ShowMsgBox("FPCX-000009");
                                    if (this.progressBar != null)
                                    {
                                        this.progressBar.Close();
                                        this.progressBar.Dispose();
                                        this.progressBar = null;
                                        GC.Collect();
                                    }
                                }
                                else
                                {
                                    this.progressBar.Visible = false;
                                    this.progressBar.Refresh();
                                    if (this.progressBar != null)
                                    {
                                        this.progressBar.Close();
                                        this.progressBar.Dispose();
                                        this.progressBar = null;
                                        GC.Collect();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[发票导出]" + exception.ToString());
            }
            finally
            {
                if (this.progressBar != null)
                {
                    this.progressBar.Visible = false;
                    this.progressBar.Close();
                    this.progressBar.Dispose();
                    this.progressBar = null;
                    GC.Collect();
                }
            }
        }

        private void but_GeShi_Click(object sender, EventArgs e)
        {
            try
            {
                this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid").SetColumnsStyle(this.xmlComponentLoader1.XMLPath, this);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void but_mingxi_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRowCollection rowsAll = aisinoGrid.Rows;
                this.ChaKanMingXi(rowsAll);
                this.SetBtnEnabled();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void but_Tongji_Click(object sender, EventArgs e)
        {
            try
            {
                this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid").Statistics(this);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private bool ChaKanMingXi(DataGridViewRowCollection rowsAll)
        {
            try
            {
                List<string[]> list;
                int rowIndex;
                List<int> list2;
                string str6;
                if (rowsAll.Count >= 1)
                {
                    if (aisinoGrid.SelectedRows.Count <= 0)
                    {
                        MessageManager.ShowMsgBox("FPZF-000001");
                        return false;
                    }
                    if (aisinoGrid.CurrentCell == null)
                    {
                        aisinoGrid.CurrentCell = aisinoGrid.SelectedRows[0].Cells[0];
                    }
                    DataGridViewRow row = aisinoGrid.Rows[aisinoGrid.CurrentCell.RowIndex];
                    string dbfpzl = this.GetInvoiceType(row.Cells["FPZL"].Value.ToString()).dbfpzl;
                    if (this.IsEmptyFPCurrentData(row.Cells["GFSH"], dbfpzl))
                    {
                        MessageManager.ShowMsgBox("FPCX-000003");
                        return false;
                    }
                    list = new List<string[]>();
                    int num = 0;
                    rowIndex = aisinoGrid.CurrentCell.RowIndex;
                    list2 = new List<int>();
                    list2.Clear();
                    foreach (DataGridViewRow row2 in (IEnumerable) rowsAll)
                    {
                        if (row2.Cells["FPDM"].Value != null)
                        {
                            string str2 = row2.Cells["FPDM"].Value.ToString();
                            string s = row2.Cells["FPHM"].Value.ToString();
                            string str4 = row2.Cells["ZFBZ"].Value.ToString();
                            string str5 = row2.Cells["YYSBZ"].Value.ToString();
                            int num3 = row2.Cells[0].RowIndex;
                            int result = 0;
                            int.TryParse(s, out result);
                            s = Convert.ToString(result);
                            dbfpzl = this.GetInvoiceType(row2.Cells["FPZL"].Value.ToString()).dbfpzl;
                            str4 = (str4.Trim() == "是") ? "1" : "0";
                            string[] item = new string[] { dbfpzl, str2.ToString(), s.ToString(), str4, Convert.ToString(num3), str5 };
                            if (num3 == aisinoGrid.CurrentCell.RowIndex)
                            {
                                rowIndex = aisinoGrid.CurrentCell.RowIndex - num;
                            }
                            if (this.IsEmptyFPCurrentData(row2.Cells["GFSH"], dbfpzl))
                            {
                                num++;
                                list2.Add(-1000);
                            }
                            else
                            {
                                list2.Add(list.Count);
                                list.Add(item);
                            }
                        }
                    }
                    str6 = "0";
                    if (this.editFPCX == EditFPCX.ChaXun)
                    {
                        str6 = "0";
                        goto Label_032B;
                    }
                    if (EditFPCX.ZuoFei == this.editFPCX)
                    {
                        str6 = "1";
                        goto Label_032B;
                    }
                    MessageManager.ShowMsgBox("FPCX-000027");
                }
                return false;
            Label_032B:;
                object[] objArray = new object[] { str6, rowIndex.ToString(), list };
                object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray);
                if (this.editFPCX == EditFPCX.FuZhi)
                {
                    return false;
                }
                if (objArray2 == null)
                {
                    this.loger.Error("调用PFTK接口，获取明细信息错误，返回值为null");
                    MessageManager.ShowMsgBox("FPCX-000028");
                    return false;
                }
                if (objArray2.Length < 2)
                {
                    this.loger.Error("调用PFTK接口，获取明细信息错误，返回值个数<2");
                    MessageManager.ShowMsgBox("FPCX-000028");
                    return false;
                }
                int num5 = (int) objArray2[0];
                if (this.editFPCX == EditFPCX.ChaXun)
                {
                    for (int i = 0; i < list2.Count; i++)
                    {
                        if (list2[i] == num5)
                        {
                            num5 = i;
                            break;
                        }
                    }
                    aisinoGrid.SetSelectRows(num5);
                }
                if (EditFPCX.ZuoFei == this.editFPCX)
                {
                    aisinoGrid.ClearSelection();
                    aisinoGrid.CurrentCell = null;
                    list = objArray2[1] as List<string[]>;
                    foreach (string[] strArray2 in list)
                    {
                        strArray2[0].Trim();
                        strArray2[1].Trim();
                        strArray2[2].Trim();
                        int num7 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(strArray2[4]);
                        if ("1" == strArray2[3].Trim())
                        {
                            aisinoGrid.SetSelectRows(num7);
                        }
                    }
                }
                if (((aisinoGrid != null) && (aisinoGrid.Rows != null)) && ((num5 >= 0) && (num5 < aisinoGrid.Rows.Count)))
                {
                    this.aisinoGridViewUnSelectedRows(aisinoGrid.SelectedRows);
                    aisinoGrid.SetSelectRows(num5);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return false;
            }
            aisinoGrid.Refresh();
            return true;
        }

        public void ChaXunDate(int page, int num, int TiaojianChaXun, int type = -1, int sqlType = 0)
        {
            try
            {
                Dictionary<string, object> dictionary;
                if (this.editFPCX == EditFPCX.ChaXun)
                {
                    dictionary = new Dictionary<string, object>();
                    dictionary.Clear();
                    this._iPageNO = page;
                    this._iPageSize = num;
                    if (this.Month <= 0)
                    {
                        string text1 = this.Year + "-01-01 00:00:00";
                        string text2 = string.Concat(new object[] { this.Year, "-12-", DateTime.DaysInMonth(this.Year, 12), " 23:59:59" });
                        dictionary.Add("KsRq", this.chaXunTiaoJian.DateTimeStart.ToString("yyyy-MM-dd") + " 00:00:00");
                        dictionary.Add("JsRq", this.chaXunTiaoJian.DateTimeEnd.ToString("yyyy-MM-dd") + " 23:59:59");
                        goto Label_0172;
                    }
                    if ((this.Month >= 1) && (this.Month <= 12))
                    {
                        dictionary.Add("KsRq", this.chaXunTiaoJian.DateTimeStart.ToString("yyyy-MM-dd") + " 00:00:00");
                        dictionary.Add("JsRq", this.chaXunTiaoJian.DateTimeEnd.ToString("yyyy-MM-dd") + " 23:59:59");
                        goto Label_0172;
                    }
                    MessageManager.ShowMsgBox("FPCX-000026");
                }
                return;
            Label_0172:
                dictionary.Add("FPZL", this.chaXunTiaoJian.KindQry.Trim());
                if (DingYiZhiFuChuan1._UserMsg.IsAdmin)
                {
                    dictionary.Add("AdminBz", 1);
                }
                else
                {
                    dictionary.Add("AdminBz", 1);
                }
                dictionary.Add("Admin", DingYiZhiFuChuan1._UserMsg.MC);
                dictionary.Add("BSZT1", this.chaXunTiaoJian.BSZT1);
                dictionary.Add("BSZT2", this.chaXunTiaoJian.BSZT2);
                int sortWay = 1;
                string str = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                switch (PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXunTiaoJian_SortWay"))
                {
                    case "1":
                    case "":
                    {
                        sortWay = 1;
                        string str6 = this.toolTxt_quick.Text.Trim(new char[] { ' ', ' ' });
                        str = str6;
                        str2 = str6;
                        str3 = this.chaXunTiaoJian.NameQry.Trim();
                        str3 = this.chaXunTiaoJian.NameQry.Trim();
                        if (str3 == "")
                        {
                            str3 = str6;
                        }
                        str4 = this.chaXunTiaoJian.TaxCodeQry.Trim();
                        if (str4 == "")
                        {
                            str4 = str6;
                        }
                        if (((str == this._QuickSearchText) && (str2 == this._QuickSearchText)) && ((str3 == this._QuickSearchText) && (str4 == this._QuickSearchText)))
                        {
                            str2 = "";
                            str = "";
                            str3 = "";
                            str4 = "";
                        }
                        dictionary.Add("FPDM", "%" + str + "%");
                        dictionary.Add("GFMC", "%" + str3 + "%");
                        dictionary.Add("GFSH", "%" + str4 + "%");
                        if (Aisino.Fwkp.Fpkj.Common.Tool.CharNumInString(str6, '0') != str6.Length)
                        {
                            str2 = str6.TrimStart(new char[] { '0' });
                        }
                        dictionary.Add("FPHM", "%" + str2 + "%");
                        break;
                    }
                    default:
                    {
                        sortWay = 0;
                        string str7 = this.toolTxt_quick.Text.Trim(new char[] { ' ', ' ' });
                        str = str7;
                        str2 = str7;
                        str3 = this.chaXunTiaoJian.NameQry.Trim();
                        if (str3 == "")
                        {
                            str3 = str7;
                        }
                        str4 = this.chaXunTiaoJian.TaxCodeQry.Trim();
                        if (str4 == "")
                        {
                            str4 = str7;
                        }
                        if (((str == this._QuickSearchText) && (str2 == this._QuickSearchText)) && ((str3 == this._QuickSearchText) && (str4 == this._QuickSearchText)))
                        {
                            str2 = "";
                            str = "";
                            str3 = "";
                            str4 = "";
                        }
                        dictionary.Add("FPDM", str);
                        dictionary.Add("GFMC", str3);
                        dictionary.Add("GFSH", str4);
                        if (Aisino.Fwkp.Fpkj.Common.Tool.CharNumInString(str7, '0') != str7.Length)
                        {
                            str2 = str7.TrimStart(new char[] { '0' });
                        }
                        dictionary.Add("FPHM", str2);
                        break;
                    }
                }
                AisinoDataSet set = this.xxfpChaXunBll.SelectPage(page, num, TiaojianChaXun, dictionary, sortWay, CardClock, type, sqlType);
                aisinoGrid.DataSource = set;
                this.InsertColumn();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private bool CheckDySet(DataGridViewSelectedRowCollection SelectedRows)
        {
            if ((SelectedRows != null) && (SelectedRows.Count != 0))
            {
                for (int i = 0; i < SelectedRows.Count; i++)
                {
                    if (SelectedRows[i].Cells["FPZL"].Value.ToString() != Aisino.Fwkp.Fpkj.Common.Tool.DZFP)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void DataGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            ShowMessage message = new ShowMessage();
            try
            {
                if (((e.RowIndex >= 0) && (e.ColumnIndex >= 0)) && ((e.ColumnIndex < aisinoGrid.Columns.Count) && (aisinoGrid.Columns[e.ColumnIndex].Name == "BSRZ")))
                {
                    object obj2 = aisinoGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if ((obj2 != null) && (obj2.ToString().Length != 0))
                    {
                        message.setValue(obj2.ToString());
                        message.ShowDialog();
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[DataGrid_CellMouseDown函数异常]" + exception.ToString());
            }
            finally
            {
                if (message != null)
                {
                    message.Close();
                    message.Dispose();
                    message = null;
                }
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

        public void DrawBorder(object sender, Graphics g, Color color, Color bordercolor, int x, int y)
        {
            SolidBrush brush = new SolidBrush(color);
            Pen pen = new Pen(brush, 1f);
            (sender as ToolStripTextBox).BorderStyle = BorderStyle.FixedSingle;
            (sender as ToolStripTextBox).BackColor = color;
            pen.Color = Color.White;
            Rectangle bounds = new Rectangle(0, 0, x, y);
            ControlPaint.DrawBorder(g, bounds, bordercolor, ButtonBorderStyle.Solid);
        }

        public void Edit(EditFPCX edit)
        {
            try
            {
                this.editFPCX = edit;
                if (edit == EditFPCX.ChaXun)
                {
                    this.Text = "选择发票号码查询";
                    this.tool_ZuoFei.Visible = false;
                    this.QuikSearchInit();
                }
                if (((aisinoGrid.Rows != null) && (aisinoGrid.SelectedRows != null)) && ((aisinoGrid.Rows.Count > 0) && (aisinoGrid.SelectedRows.Count <= 0)))
                {
                    aisinoGrid.SetSelectRows(0);
                }
                this.chaXunTiaoJian.Year = this.Year;
                this.chaXunTiaoJian.Month = this.Month;
                this.chaXunTiaoJian.SetSearchControlAttritute();
                this.SetBtnEnabled();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private string EscapeQueryContent(string condition)
        {
            if ((condition != null) && (condition.Length > 0))
            {
                condition = condition.Replace("[", "[[]");
                condition = condition.Replace("_", "[_]");
                condition = condition.Replace("%", "[%]");
                condition = condition.Replace("]", "[]]");
            }
            return condition;
        }

        private void FaPiaoChaXun_FormClosing(object sender, EventArgs e)
        {
            this.xxfpChaXunBll = null;
            this.progressBar = null;
            this._XmlOperation = null;
            this.FpPrintList = null;
            if (this.chaXunTiaoJian != null)
            {
                this.chaXunTiaoJian.Close();
                this.chaXunTiaoJian.Dispose();
                this.chaXunTiaoJian = null;
            }
            aisinoGrid.DataSource=null;
            aisinoGrid = null;
        }

        private void FaPiaoChaXun_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                base.Dispose();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private bool FpDaYin(int IsLastFp, bool IsChangeDateBase, Dictionary<string, object> dict, bool MultiLine = false, bool QDDQ = false)
        {
            try
            {
                bool flag = true;
                if (!QDDQ)
                {
                    if (dict == null)
                    {
                        return false;
                    }
                    if (!MultiLine)
                    {
                        DyQueRen ren = new DyQueRen();
                        if (!ren.setValue(dict))
                        {
                            return false;
                        }
                        if (DialogResult.OK != ren.ShowDialog())
                        {
                            return false;
                        }
                    }
                    string dbfpzl = this.GetInvoiceType(dict["lbl_Fpzl"].ToString()).dbfpzl;
                    int num = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(dict["lbl_Fphm"].ToString());
                    IPrint print = FPPrint.Create(dbfpzl, dict["lbl_Fpdm"].ToString(), num, true);
                    if (!MultiLine)
                    {
                        print.Print(true);
                    }
                    else if (IsLastFp == 0)
                    {
                        print.Print(true);
                    }
                    else
                    {
                        print.Print(false);
                    }
                    flag = print.IsPrint.Equals("0000");
                }
                else
                {
                    try
                    {
                        if (!Aisino.Fwkp.Fpkj.Common.Tool.ObjectToBool(dict["lbl_Qdbz"]))
                        {
                            return false;
                        }
                        string str2 = this.GetInvoiceType(dict["lbl_Fpzl"].ToString()).dbfpzl;
                        int num2 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(dict["lbl_Fphm"].ToString());
                        IPrint print2 = FPPrint.Create(str2, dict["lbl_Fpdm"].ToString(), num2, false);
                        if (!MultiLine)
                        {
                            print2.Print(true);
                        }
                        else if (IsLastFp == 0)
                        {
                            print2.Print(true);
                        }
                        else
                        {
                            print2.Print(false);
                        }
                        flag = print2.IsPrint.Equals("0000");
                    }
                    catch (Exception exception)
                    {
                        this.loger.Error("销货清单打印异常：" + exception.ToString());
                        flag = false;
                    }
                }
                return flag;
            }
            catch (Exception exception2)
            {
                this.loger.Error("发票打印异常：" + exception2.ToString());
                MessageManager.ShowMsgBox(exception2.ToString());
                return false;
            }
        }

        private _InvoiceType GetInvoiceType(string type)
        {
            _InvoiceType type2;
            switch (type)
            {
                case "普通发票":
                    type2.dbfpzl = "c";
                    type2.TaxCardfpzl = (InvoiceType)2;
                    return type2;

                case "专用发票":
                    type2.dbfpzl = "s";
                    type2.TaxCardfpzl = (InvoiceType)0;
                    return type2;

                case "农产品销售发票":
                    type2.dbfpzl = "c";
                    type2.TaxCardfpzl = (InvoiceType)2;
                    return type2;

                case "收购发票":
                    type2.dbfpzl = "c";
                    type2.TaxCardfpzl = (InvoiceType)2;
                    return type2;

                case "机动车销售统一发票":
                    type2.dbfpzl = "j";
                    type2.TaxCardfpzl = (InvoiceType)12;
                    return type2;

                case "货物运输业增值税专用发票":
                    type2.dbfpzl = "f";
                    type2.TaxCardfpzl = (InvoiceType)11;
                    return type2;

                case "电子增值税普通发票":
                    type2.dbfpzl = "p";
                    type2.TaxCardfpzl = (InvoiceType)0x33;
                    return type2;

                case "增值税普通发票(卷票)":
                    type2.dbfpzl = "q";
                    type2.TaxCardfpzl = (InvoiceType)0x29;
                    return type2;
            }
            type2.dbfpzl = "";
            type2.TaxCardfpzl = (InvoiceType)1;
            return type2;
        }

        private void GetQuickSearchData(SearchType type)
        {
            try
            {
                switch (type)
                {
                    case SearchType.FPZLSearch:
                        this.chaXunTiaoJian.KindQry = this.FpzlKind.Substring(this.tool_Fpzl.SelectedIndex, 1);
                        break;

                    case SearchType.DATESearch:
                        this.Year = CardClock.Year;
                        this.Month = CardClock.Month;
                        if (this.tool_Year.Text.Length >= 4)
                        {
                            this.Year = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.tool_Year.Text.Substring(0, 4));
                        }
                        if (this.tool_Month.Text == "全年")
                        {
                            this.Month = 0;
                        }
                        else if ((this.tool_Month.Text != null) && (this.tool_Month.Text.Length >= 2))
                        {
                            this.Month = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.tool_Month.Text.Substring(0, 2));
                        }
                        if (this.Month <= 0)
                        {
                            this.chaXunTiaoJian.DateStart = new DateTime(this.Year, 1, 1);
                            this.chaXunTiaoJian.DateEnd = new DateTime(this.Year, 12, DateTime.DaysInMonth(this.Year, 12));
                        }
                        else
                        {
                            this.chaXunTiaoJian.DateStart = new DateTime(this.Year, this.Month, 1);
                            this.chaXunTiaoJian.DateEnd = new DateTime(this.Year, this.Month, DateTime.DaysInMonth(this.Year, this.Month));
                        }
                        break;

                    case SearchType.BSZTSearch:
                        this.chaXunTiaoJian.BSZT1 = -1;
                        if (this.MenuItem_Weibaosong.Checked)
                        {
                            this.chaXunTiaoJian.BSZT1 = 0;
                        }
                        this.chaXunTiaoJian.BSZT2 = -1;
                        if (this.MenuItem_Yanqianshibai.Checked)
                        {
                            this.chaXunTiaoJian.BSZT2 = 4;
                        }
                        break;
                }
                this.chaXunTiaoJian.NameQry = "";
                this.chaXunTiaoJian.TaxCodeQry = "";
                this.chaXunTiaoJian.blNameQry = true;
                this.chaXunTiaoJian.blTaxCodeQry = true;
                this.chaXunTiaoJian.blDateStart_Bkjr = true;
                this.chaXunTiaoJian.blDateEnd_Bkjr = true;
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数GetQuickSearchData异常]" + exception.ToString());
            }
        }

        private bool GetValueFPFZ()
        {
            try
            {
                if (this.editFPCX == EditFPCX.FuZhi)
                {
                    if (aisinoGrid == null)
                    {
                        this._Object_FPFZ = null;
                        return false;
                    }
                    if (aisinoGrid.CurrentCell == null)
                    {
                        this._Object_FPFZ = null;
                        return false;
                    }
                    if (aisinoGrid.CurrentCell.OwningRow == null)
                    {
                        this._Object_FPFZ = null;
                        return false;
                    }
                    DataGridViewRow owningRow = aisinoGrid.CurrentCell.OwningRow;
                    if (owningRow == null)
                    {
                        this._Object_FPFZ = null;
                        return false;
                    }
                    string dbfpzl = this.GetInvoiceType(owningRow.Cells["FPZL"].Value.ToString()).dbfpzl;
                    string str2 = Convert.ToString(owningRow.Cells["FPDM"].Value);
                    string str3 = ShareMethods.FPHMTo8Wei(Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(owningRow.Cells["FPHM"].Value.ToString().Trim()));
                    this._Object_FPFZ = new object[] { dbfpzl.Trim(), str2.Trim(), str3.Trim() };
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
                return false;
            }
            return false;
        }

        private DateTime GetYearMonth(string date)
        {
            DateTime time = new DateTime(0x761, 12, 0x1f);
            DateTime result = new DateTime();
            if (!DateTime.TryParse(date, out result))
            {
                result = time;
            }
            return result;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripSeparator1 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator1");
            this.toolStripSeparator2 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator2");
            this.toolStripSeparator3 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator3");
            this.tool_chazhao = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_chazhao");
            this.tool_daying = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_daying");
            this.tool_tongji = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tongji");
            this.tool_GeShi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_GeShi");
            this.tool_hedui = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_hedui");
            this.tool_Exit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Exit");
            this.tool_ZuoFei = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_ZuoFei");
            this.tool_ChaKanMingXi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_ChaKanMingXi");
            this.tool_fasong = this.xmlComponentLoader1.GetControlByName<ToolStripDropDownButton>("tool_fasong");
            this.tool_SendMail = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_SendMail");
            this.tool_SendDisk = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_SendDisk");
            this.tool_SendToWebService = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_SendToWebService");
            if (base.TaxCardInstance.QYLX.ISTDQY)
            {
                this.tool_fasong.Visible = false;
            }
            this.tool_SendMail.Visible = false;
            this.tool_SendDisk.Visible = false;
            this.tool_SendToWebService.Visible = false;
            this.tool_fasong.Text = "发票上传";
            this.tool_FPDaoChu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_FPDaoChu");
            this.tool_DaikaiSC = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_DaikaiSC");
            this.tool_Year = this.xmlComponentLoader1.GetControlByName<ToolStripComboBox>("tool_Year");
            this.tool_Month = this.xmlComponentLoader1.GetControlByName<ToolStripComboBox>("tool_Month");
            this.tool_Fpzl = this.xmlComponentLoader1.GetControlByName<ToolStripComboBox>("tool_Fpzl");
            this.toolTxt_quick = this.xmlComponentLoader1.GetControlByName<ToolStripTextBox>("toolTxt_quick");
            this.contextMenu_DaYin = new ContextMenuStrip();
            this.MenuItem_FP = new ToolStripMenuItem("发    票");
            this.MenuItem_XHQD = new ToolStripMenuItem("销货清单");
            this.MenuItem_FPLB = new ToolStripMenuItem("发票列表");
            this.contextMenu_DaYin.Items.Add(this.MenuItem_FP);
            this.contextMenu_DaYin.Items.Add(this.MenuItem_XHQD);
            this.contextMenu_DaYin.Items.Add(this.MenuItem_FPLB);
            this.MenuItem_FP.Click += new EventHandler(this.MenuItem_FP_Click);
            this.MenuItem_XHQD.Click += new EventHandler(this.MenuItem_XHQD_Click);
            this.MenuItem_FPLB.Click += new EventHandler(this.MenuItem_FPLBClick);
            aisinoGrid = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid");
            aisinoGrid.ReadOnly = true;
            aisinoGrid.DataGrid.AllowUserToDeleteRows = false;
            aisinoGrid.DataGrid.CellMouseDown += DataGrid_CellMouseDown;
            aisinoGrid.DataGridRowDbClickEvent += aisinoGrid_DataGridRowDbClickEvent;
            aisinoGrid.GoToPageEvent += aisinoGrid_GoToPageEvent;
            aisinoGrid.DataGridRowClickEvent += aisinoGrid_DataGridRowClickEvent;
            aisinoGrid.DataGridRowSelectionChanged += aisinoGrid_DataGridRowSelectionChanged;
            this.tool_chazhao.Click += new EventHandler(this.btnChazhao_Click);
            this.tool_hedui.Click += new EventHandler(this.btnHeDui_Click);
            this.tool_daying.Click += new EventHandler(this.btnDaYing_Click);
            this.tool_Exit.Click += new EventHandler(this.btnExitClick);
            this.tool_ChaKanMingXi.Click += new EventHandler(this.but_mingxi_Click);
            this.tool_tongji.Click += new EventHandler(this.but_Tongji_Click);
            this.tool_GeShi.Click += new EventHandler(this.but_GeShi_Click);
            this.tool_fasong.Click += new EventHandler(this.tool_fasong_Click);
            if (base.TaxCardInstance.SoftVersion == "FWKP_V2.0_Svr_Client")
            {
                this.tool_fasong.Visible = false;
            }
            this.tool_FPDaoChu.Click += new EventHandler(this.but_FPDaoChu_Click);
            this.tool_DaikaiSC.Click += new EventHandler(this.but_DaikaiShangchuan_Click);
            this.tool_DaikaiSC.Visible = Aisino.Fwkp.Fpkj.Common.Tool.IsShuiWuDKSQ();
            this.tool_FPZJDaoChu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_FPZJDaoChu");
            this.tool_FPZJDaoChu.Click += new EventHandler(this.tool_FPZJDaoChu_Click);
            if (RegisterManager.CheckRegFile("QC"))
            {
                this.tool_FPZJDaoChu.Visible = true;
            }
            else
            {
                this.tool_FPZJDaoChu.Visible = false;
            }
            this.tool_FPCXBSZT = this.xmlComponentLoader1.GetControlByName<ToolStripSplitButton>("tool_FPCXBSZT");
            this.contextMenu_BSZT = new ContextMenuStrip();
            this.MenuItem_Weibaosong = new ToolStripMenuItem("未报送");
            this.MenuItem_Yanqianshibai = new ToolStripMenuItem("验签失败");
            this.contextMenu_BSZT.Items.Add(this.MenuItem_Weibaosong);
            this.contextMenu_BSZT.Items.Add(this.MenuItem_Yanqianshibai);
            this.tool_FPCXBSZT.Click += new EventHandler(this.tool_FPCXBSZT_Click);
            this.MenuItem_Weibaosong.Click += new EventHandler(this.MenuItem_Weibaosong_Click);
            this.MenuItem_Yanqianshibai.Click += new EventHandler(this.MenuItem_Yanqianshibai_Click);
            if (base.TaxCardInstance.QYLX.ISTDQY)
            {
                this.tool_FPCXBSZT.Visible = false;
            }
            bool flag = true;
            this.tool_Year.Visible = flag;
            this.tool_Month.Visible = flag;
            this.tool_Fpzl.Visible = flag;
            if (flag)
            {
                this.tool_Year.DropDownStyle = ComboBoxStyle.DropDownList;
                this.tool_Month.DropDownStyle = ComboBoxStyle.DropDownList;
                this.tool_Fpzl.DropDownStyle = ComboBoxStyle.DropDownList;
                this.tool_Year.SelectedIndexChanged += new EventHandler(this.tool_Year_SelectedIndexChanged);
                this.tool_Month.SelectedIndexChanged += new EventHandler(this.tool_Month_SelectedIndexChanged);
                this.tool_Fpzl.SelectedIndexChanged += new EventHandler(this.tool_Fpzl_SelectedIndexChanged);
                this.tool_Year.Alignment = ToolStripItemAlignment.Right;
                this.tool_Month.Alignment = ToolStripItemAlignment.Right;
                this.tool_Fpzl.Alignment = ToolStripItemAlignment.Right;
                this.tool_FPCXBSZT.Alignment = ToolStripItemAlignment.Right;
            }
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.SetNewStyplePrimaryScreen();
            this.tool_Exit.Margin = new Padding(20, 1, 0, 2);
            this.Text = "已开发票查询";
            this.tool_FPCXBSZT.ToolTipText = "点击选择报送状态进行选择";
            this.tool_Fpzl.Width = 180;
            this.toolTxt_quick.Alignment = ToolStripItemAlignment.Right;
            this.toolTxt_quick.AutoToolTip = true;
            this.toolTxt_quick.ToolTipText = "请输入关键字(发票代码，发票号码，购方名称，购方税号),以回车键或者ESC键结束";
            this.toolTxt_quick.KeyUp += new KeyEventHandler(this.KeyUp_QuickSearch);
            this.toolTxt_quick.Paint += new PaintEventHandler(this.toolTxt_quick_Paint);
            this.toolTxt_quick.GotFocus += new EventHandler(this.toolTxt_quick_GotFocus);
            this.toolTxt_quick.LostFocus += new EventHandler(this.toolTxt_quick_LostFocus);
            this.toolTxt_quick.ForeColor = Color.Gray;
            this.toolTxt_quick.Font = new Font("微软雅黑", 9f, FontStyle.Italic);
            base.FormClosing += new FormClosingEventHandler(this.FaPiaoChaXun_FormClosing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FaPiaoChaXun));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x249, 0x1f2);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "FaPiaoChaXun";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPCX.FaPiaoChaXun\Aisino.Fwkp.Fpkj.Form.FPCX.FaPiaoChaXun.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x249, 0x1f2);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FaPiaoChaXun";
            base.TabText = "选择发票号码查询";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "FaPiaoChaXun";
            base.FormClosing += new FormClosingEventHandler(this.FaPiaoChaXun_FormClosing);
            base.ResumeLayout(false);
        }

        private void InsertColumn()
        {
            try
            {
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                Dictionary<string, string> item = new Dictionary<string, string>();
                string str = "50";
                string str2 = "66";
                string str3 = "78";
                string str4 = "90";
                string str5 = "102";
                string str6 = "127";
                string str7 = str5;
                item.Add("AisinoLBL", "发票种类");
                item.Add("Property", "FPZL");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票代码");
                item.Add("Property", "FPDM");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票号码");
                item.Add("Property", "FPHM");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开票机号");
                item.Add("Property", "KPJH");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "购方名称");
                item.Add("Property", "GFMC");
                item.Add("Width", str7);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "购方税号");
                item.Add("Property", "GFSH");
                item.Add("Width", str7);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "购方地址电话");
                item.Add("Property", "GFDZDH");
                item.Add("Width", str7);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "购方银行账号");
                item.Add("Property", "GFYHZH");
                item.Add("Width", str7);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开票日期");
                item.Add("Property", "KPRQ");
                item.Add("Width", str6);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "报送状态");
                item.Add("Property", "BSZT");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("HeadAlign", "MiddleCenter");
                if (base.TaxCardInstance.QYLX.ISTDQY)
                {
                    item.Add("Visible", "False");
                }
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "报送日志");
                item.Add("Property", "BSRZ");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                if (base.TaxCardInstance.QYLX.ISTDQY)
                {
                    item.Add("Visible", "False");
                }
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "加密版本号");
                item.Add("Property", "JMBBH");
                item.Add("Width", str4);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "密文");
                item.Add("Property", "MW");
                item.Add("Width", "100");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "所属月份");
                item.Add("Property", "SSYF");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "合计金额");
                item.Add("Property", "HJJE");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleRight");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "税率");
                item.Add("Property", "SLv");
                item.Add("Width", str);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "合计税额");
                item.Add("Property", "HJSE");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleRight");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "主要商品名称");
                item.Add("Property", "ZYSPMC");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "商品税目");
                item.Add("Property", "SPSM");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "备注");
                item.Add("Property", "BZ");
                item.Add("Width", str);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "True");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开票人");
                item.Add("Property", "KPR");
                item.Add("Width", str2);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "收款人");
                item.Add("Property", "SKR");
                item.Add("Width", str2);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "复核人");
                item.Add("Property", "FHR");
                item.Add("Width", str2);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "打印标志");
                item.Add("Property", "DYBZ");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "作废标志");
                item.Add("Property", "ZFBZ");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "清单标志");
                item.Add("Property", "QDBZ");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "修复标志");
                item.Add("Property", "XFBZ");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "外开标志");
                item.Add("Property", "WKBZ");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销方名称");
                item.Add("Property", "XFMC");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销方税号");
                item.Add("Property", "XFSH");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销方地址电话");
                item.Add("Property", "XFDZDH");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销方银行账号");
                item.Add("Property", "XFYHZH");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "报税标志");
                item.Add("Property", "BSBZ");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "报税期");
                item.Add("Property", "BSQ");
                item.Add("Width", str2);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销售单据编号");
                item.Add("Property", "XSDJBH");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "异地销售");
                item.Add("Property", "YDXS");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "营业税标志");
                item.Add("Property", "YYSBZ");
                item.Add("Width", str4);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "选择标志");
                item.Add("Property", "XZBZ");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "凭证号码");
                item.Add("Property", "PZHM");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "凭证状态");
                item.Add("Property", "PZZT");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "凭证类别");
                item.Add("Property", "PZLB");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "凭证日期");
                item.Add("Property", "PZRQ");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "汉信码");
                item.Add("Property", "HXM");
                item.Add("Width", str2);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "凭证往来业务号");
                item.Add("Property", "PZWLYWH");
                item.Add("Width", str4);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "校验码");
                item.Add("Property", "JYM");
                item.Add("Width", str2);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "红字信息表编号");
                item.Add("Property", "HZTZDH");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "设备标志");
                item.Add("Property", "SBBZ");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "到离港日期");
                item.Add("Property", "DLGRQ");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "船名");
                item.Add("Property", "CM");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开户银行名称");
                item.Add("Property", "KHYHMC");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开户银行帐号");
                item.Add("Property", "KHYHZH");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "提运单号");
                item.Add("Property", "TYDH");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "起运地");
                item.Add("Property", "QYD");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "装货地");
                item.Add("Property", "ZHD");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "卸货地");
                item.Add("Property", "XHD");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "目的地");
                item.Add("Property", "MDD");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "营业执照号");
                item.Add("Property", "YYZZH");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "行业编码");
                item.Add("Property", "HYBM");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销方地址");
                item.Add("Property", "XFDZ");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销方电话");
                item.Add("Property", "XFDH");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "机器编号");
                item.Add("Property", "JQBH");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "生产厂家名称");
                item.Add("Property", "SCCJMC");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "运输货物信息");
                item.Add("Property", "YSHWXX");
                item.Add("Width", str5);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "签名");
                item.Add("Property", "SIGN");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("Visible", "False");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "身份证号码/组织机构代码");
                item.Add("Property", "XSBM");
                item.Add("Width", str7);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                if (base.TaxCardInstance.SQInfo.DHYBZ.Equals("H"))
                {
                    item.Add("Visible", "False");
                }
                else
                {
                    item.Add("Visible", "True");
                }
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "完税凭证号码");
                item.Add("Property", "WSPZHM");
                item.Add("Width", str3);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("Visible", "False");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "作废日期");
                item.Add("Property", "ZFRQ");
                item.Add("Width", str6);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "地址索引号");
                item.Add("Property", "DZSYH");
                item.Add("Width", str6);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开票顺序号");
                item.Add("Property", "KPSXH");
                item.Add("Width", str6);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "编码版本编号");
                item.Add("Property", "BMBBBH");
                item.Add("Width", str6);
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                list.Add(item);
                aisinoGrid.ColumeHead = list;
                DataGridViewColumn column = aisinoGrid.Columns["KPRQ"];
                if (column != null)
                {
                    DataGridViewCellStyle defaultCellStyle = column.DefaultCellStyle;
                    defaultCellStyle.ForeColor = Color.Maroon;
                    defaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                }
                DataGridViewColumn column2 = aisinoGrid.Columns["DYBZ"];
                if (column2 != null)
                {
                    column2.DefaultCellStyle.ForeColor = Color.Maroon;
                }
                DataGridViewColumn column3 = aisinoGrid.Columns["ZFBZ"];
                if (column3 != null)
                {
                    column3.DefaultCellStyle.ForeColor = Color.Maroon;
                }
                DataGridViewColumn column4 = aisinoGrid.Columns["WKBZ"];
                if (column4 != null)
                {
                    column4.DefaultCellStyle.ForeColor = Color.Maroon;
                }
                DataGridViewColumn column5 = aisinoGrid.Columns["XFBZ"];
                if (column5 != null)
                {
                    column5.DefaultCellStyle.ForeColor = Color.Maroon;
                }
                DataGridViewColumn column6 = aisinoGrid.Columns["QDBZ"];
                if (column6 != null)
                {
                    column6.DefaultCellStyle.ForeColor = Color.Maroon;
                }
                DataGridViewColumn column7 = aisinoGrid.Columns["FPHM"];
                if (column7 != null)
                {
                    column7.DefaultCellStyle.Format = new string('0', 8);
                }
                DataGridViewColumn column8 = aisinoGrid.Columns["HJJE"];
                if (column8 != null)
                {
                    column8.DefaultCellStyle.Format = "0.00";
                }
                DataGridViewColumn column9 = aisinoGrid.Columns["SLv"];
                if (column9 != null)
                {
                    column9.DefaultCellStyle.Format = "0.00";
                }
                DataGridViewColumn column10 = aisinoGrid.Columns["HJSE"];
                if (column10 != null)
                {
                    column10.DefaultCellStyle.Format = "0.00";
                }
                list = null;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private bool IsEmptyFPCurrentData(DataGridViewCell CurrentCell, string fpzl)
        {
            try
            {
                if (CurrentCell == null)
                {
                    return true;
                }
                string str = CurrentCell.OwningRow.Cells["ZFBZ"].Value.ToString().Trim();
                return ((Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(CurrentCell.OwningRow.Cells["HJJE"].Value.ToString().Trim()) == 0.0) && Aisino.Fwkp.Fpkj.Common.Tool.ObjectToBool(str));
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
                return false;
            }
        }

        private void KeyUp_QuickSearch(object sender, KeyEventArgs e)
        {
            try
            {
                if (((e.KeyValue == 13) || (e.KeyValue == 0x1b)) || (e.KeyValue == 13))
                {
                    aisinoGrid.Focus();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[toolTxt_quick_TextChanged函数异常]" + exception.ToString());
            }
        }

        private void MenuItem_FP_Click(object sender, EventArgs e)
        {
            try
            {
                if (aisinoGrid.Rows.Count > 0)
                {
                    DataGridViewSelectedRowCollection selectedRowCollection = aisinoGrid.SelectedRows;
                    if (selectedRowCollection.Count <= 0)
                    {
                        MessageManager.ShowMsgBox("FPZF-000001");
                    }
                    else
                    {
                        this.SelectedFPListPrint(selectedRowCollection, false);
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void MenuItem_FPLBClick(object sender, EventArgs e)
        {
            try
            {
                AisinoDataGrid controlByName = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid");
                if ((controlByName == null) || (controlByName.Rows.Count <= 0))
                {
                    MessageManager.ShowMsgBox("没有可打印的列表内容");
                }
                else
                {
                    controlByName.Print("选择发票号码查询", this, null, null, true);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox("发票列表打印失败");
            }
        }

        private void MenuItem_Weibaosong_Click(object sender, EventArgs e)
        {
            try
            {
                this.MenuItem_Weibaosong.Checked = !this.MenuItem_Weibaosong.Checked;
                this.QuickSearch(SearchType.BSZTSearch);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void MenuItem_XHQD_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection selectedRowCollection = aisinoGrid.SelectedRows;
                if (selectedRowCollection.Count <= 0)
                {
                    MessageManager.ShowMsgBox("FPZF-000001");
                }
                else
                {
                    this.SelectedFPListPrint(selectedRowCollection, true);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void MenuItem_Yanqianshibai_Click(object sender, EventArgs e)
        {
            try
            {
                this.MenuItem_Yanqianshibai.Checked = !this.MenuItem_Yanqianshibai.Checked;
                this.QuickSearch(SearchType.BSZTSearch);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void PerformStep(int step)
        {
            for (int i = 0; i < step; i++)
            {
                if ((this.progressBar.fpxf_progressBar.Value + 1) > this.progressBar.fpxf_progressBar.Maximum)
                {
                    this.progressBar.fpxf_progressBar.Value = this.progressBar.fpxf_progressBar.Maximum;
                }
                else
                {
                    this.progressBar.fpxf_progressBar.Value++;
                }
                this.progressBar.fpxf_progressBar.Refresh();
            }
        }

        private void ProccessBarShow(object obj)
        {
            try
            {
                int step = (int) obj;
                this.PerformStep(step);
            }
            catch (Exception exception)
            {
                this.loger.Error("[ThreadFun]" + exception.ToString());
            }
        }

        public void ProcessStartThread(int value)
        {
            this.PerformStep(value);
        }

        private void QuickSearch(SearchType type)
        {
            try
            {
                this.GetQuickSearchData(type);
                string data = PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXun_aisinoGrid_PageSize");
                this.AvePageNum = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(data);
                if (this.AvePageNum <= 0)
                {
                    this.AvePageNum = 30;
                }
                this.sqlType = 1;
                this.ChaXunDate(1, this.AvePageNum, 0, -1, this.sqlType);
                this.chaXunTiaoJian.Year = this.Year;
                this.chaXunTiaoJian.Month = this.Month;
                this.chaXunTiaoJian.SetSearchControlAttritute();
                this.SetBtnEnabled();
            }
            catch (Exception exception)
            {
                this.loger.Error("[QuickSearch函数异常]" + exception.ToString());
            }
        }

        private void QuikSearchInit()
        {
            this.IsInitSearch = true;
            this.SetFPZLAuthorization();
            this.SetYearMonthAuthorization();
        }

        public bool QuitForm_IsEmpty()
        {
            try
            {
                if (aisinoGrid.DataSource == null)
                {
                    base.Hide();
                    MessageManager.ShowMsgBox("FPCX-000001");
                    base.Close();
                    return true;
                }
                if (0 >= aisinoGrid.DataSource.AllRows)
                {
                    base.Hide();
                    MessageManager.ShowMsgBox("FPCX-000001");
                    base.Close();
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
                return false;
            }
            return false;
        }

        private string ReadStartDate()
        {
            try
            {
                string str = this.xxfpChaXunBll.SelectMinKprq(null);
                if ((str == "") || (str.CompareTo(base.TaxCardInstance.CardEffectDate) > 0))
                {
                    str = base.TaxCardInstance.CardEffectDate;
                }
                this._StartYear = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(str.Substring(0, 4));
                this._StartMonth = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(str.Substring(4, 2));
                return str;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                //逻辑修改 我们并不需要这一报错内容
                //MessageManager.ShowMsgBox(exception.ToString());
                //return base.TaxCardInstance.CardEffectDate;
                return DateTime.Now.ToString("yyyyMM");
            }
        }

        private void SelectedFPListPrint(DataGridViewSelectedRowCollection selectedRowCollection, bool ISQD = false)
        {
            if ((selectedRowCollection != null) && (selectedRowCollection.Count != 0))
            {
                int num = 0;
                int num2 = 0;
                string str = "";
                int num3 = selectedRowCollection.Count - 1;
                this.FpPrintList.Clear();
                this.FpPrintResultHash.Clear();
                for (num = 0; num <= num3; num++)
                {
                    FpPrint item = new FpPrint {
                        dybz = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToBool(selectedRowCollection[num].Cells["DYBZ"].Value)
                    };
                    str = selectedRowCollection[num].Cells["FPZL"].Value.ToString();
                    if (str != Aisino.Fwkp.Fpkj.Common.Tool.DZFP)
                    {
                        item.fpdm = selectedRowCollection[num].Cells["FPDM"].Value.ToString();
                        item.qdbz = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToBool(selectedRowCollection[num].Cells["QDBZ"].Value.ToString());
                        num2 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(selectedRowCollection[num].Cells["FPHM"].Value.ToString());
                        item.index = num;
                        if (num2 <= 0)
                        {
                            MessageManager.ShowMsgBox("FPCX-000023");
                            return;
                        }
                        item.fphm = num2;
                        switch (str)
                        {
                            case "收购发票":
                            case "农产品销售发票":
                                str = "普通发票";
                                break;
                        }
                        item.fpzl = str;
                        this.FpPrintList.Add(item);
                    }
                }
                this.WenBenSelectedFPListPrint(this.FpPrintList, ISQD);
                if ((!ISQD && (this.FpPrintResultHash != null)) && (this.FpPrintResultHash.Count > 0))
                {
                    ArrayList list = new ArrayList(this.FpPrintResultHash.Keys);
                    foreach (int num4 in list)
                    {
                        if ((selectedRowCollection[num4] != null) && (this.FpPrintResultHash[num4].ToString() == "1"))
                        {
                            selectedRowCollection[num4].Cells["DYBZ"].Value = "是";
                        }
                    }
                }
            }
        }

        public void SetBtnEnabled()
        {
            try
            {
                if (this.editFPCX == EditFPCX.FuZhi)
                {
                    this.tool_chazhao.Visible = false;
                    this.tool_hedui.Visible = false;
                    this.tool_ZuoFei.Visible = false;
                    this.tool_ChaKanMingXi.Visible = false;
                    this.toolStripSeparator2.Visible = false;
                }
                bool flag = true;
                if (aisinoGrid.Rows.Count <= 0)
                {
                    flag = false;
                }
                else
                {
                    //aisinoGrid.SelectedRows;
                    if (aisinoGrid.SelectedRows.Count > 1)
                    {
                        flag = true;
                    }
                    else if (aisinoGrid.SelectedRows.Count == 1)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                bool flag2 = this.CheckDySet(aisinoGrid.SelectedRows);
                this.tool_daying.Enabled = flag2;
                this.contextMenu_DaYin.Enabled = flag2;
                this.MenuItem_FPLB.Enabled = flag2;
                this.MenuItem_FP.Enabled = flag2;
                this.MenuItem_XHQD.Enabled = flag2;
                this.tool_ChaKanMingXi.Enabled = flag;
                this.tool_ZuoFei.Enabled = flag;
                this.tool_DaikaiSC.Enabled = flag;
                DateTime cardClock = base.TaxCardInstance.GetCardClock();
                DateTime yearMonth = new DateTime(0x761, 12, 30);
                if (((aisinoGrid != null) && (aisinoGrid.CurrentCell != null)) && (aisinoGrid.CurrentCell.OwningRow.Cells["KPRQ"].Value != null))
                {
                    string date = aisinoGrid.CurrentCell.OwningRow.Cells["KPRQ"].Value.ToString().Trim();
                    yearMonth = this.GetYearMonth(date);
                }
                if ((yearMonth.Year == cardClock.Year) && (yearMonth.Month == cardClock.Month))
                {
                    this.tool_hedui.Enabled = true;
                    if (!flag)
                    {
                        this.tool_hedui.Enabled = false;
                    }
                }
                else
                {
                    this.tool_hedui.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private AisinoDataSet SetFPHM8(AisinoDataSet DataSource)
        {
            try
            {
                string str = "FPHM";
                DataColumn column = DataSource.Data.Columns[str];
                if (column == null)
                {
                    return DataSource;
                }
                column.DataType = System.Type.GetType("AnsiString");
                for (int i = 0; i < DataSource.Data.Rows.Count; i++)
                {
                    if (DataSource.Data.Rows[i][str] != null)
                    {
                        string s = DataSource.Data.Rows[i][str].ToString();
                        if (!s.Length.Equals(8))
                        {
                            int result = 0;
                            int.TryParse(s, out result);
                            s = string.Format("{0:00000000}", result);
                            DataSource.Data.Rows[i][str] = s;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
                return DataSource;
            }
            return DataSource;
        }

        public void SetFPZLAuthorization()
        {
            try
            {
                string str = this.xxfpChaXunBll.SelectFPZLListFromDB(null);
                if (str == null)
                {
                    str = "";
                }
                this.tool_Fpzl.Items.Clear();
                this.tool_Fpzl.Items.Add("全部发票");
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISZYFP) || (str.IndexOf('s') != -1))
                {
                    this.tool_Fpzl.Items.Add("专用发票");
                    this.FpzlKind = this.FpzlKind + "s";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISPTFP) || (str.IndexOf('c') != -1))
                {
                    this.tool_Fpzl.Items.Add("普通发票");
                    this.FpzlKind = this.FpzlKind + "c";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISHY) || (str.IndexOf('f') != -1))
                {
                    this.tool_Fpzl.Items.Add("货物运输业增值税专用发票");
                    this.FpzlKind = this.FpzlKind + "f";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISJDC) || (str.IndexOf('j') != -1))
                {
                    this.tool_Fpzl.Items.Add("机动车销售统一发票");
                    this.FpzlKind = this.FpzlKind + "j";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISPTFPDZ) || (str.IndexOf('p') != -1))
                {
                    this.tool_Fpzl.Items.Add(Aisino.Fwkp.Fpkj.Common.Tool.DZFP);
                    this.FpzlKind = this.FpzlKind + "p";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISPTFPJSP) || (str.IndexOf('q') != -1))
                {
                    this.tool_Fpzl.Items.Add(Aisino.Fwkp.Fpkj.Common.Tool.JSFP);
                    this.FpzlKind = this.FpzlKind + "q";
                }
                this.tool_Fpzl.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                this.loger.Error("SetFPZLAuthorization 异常" + exception.ToString());
            }
        }

        private void SetNewStyplePrimaryScreen()
        {
            this.tool_Year.AutoSize = false;
            this.tool_Year.DropDownWidth = 70;
            this.tool_Month.DropDownWidth = 70;
            this.tool_Fpzl.DropDownWidth = 170;
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            if (width >= 0x500)
            {
                this.tool_Year.Width = 70;
                this.tool_Month.Width = 70;
                this.tool_Fpzl.Width = 170;
            }
            else if (width >= 0x400)
            {
                this.tool_Year.Width = 0x41;
                this.tool_Month.Width = 60;
                this.tool_Fpzl.Width = 100;
                this.SetToolStripStyle1024X678(this.toolStrip1);
            }
            else
            {
                this.SetToolStripStyle1024X678(this.toolStrip1);
            }
        }

        private void SetQuickSearchMonthList(int CurYear, int StartYear, int CardYear)
        {
            this.tool_Month.Items.Clear();
            int month = 12;
            int num2 = 1;
            if ((CurYear == StartYear) && (CurYear == CardYear))
            {
                month = this.TaxCardClock.Month;
                num2 = this._StartMonth;
            }
            else if ((CurYear != StartYear) && (CurYear == CardYear))
            {
                month = this.TaxCardClock.Month;
                num2 = 1;
            }
            else if ((CurYear == StartYear) && (CurYear != CardYear))
            {
                month = 12;
                num2 = this._StartMonth;
            }
            else
            {
                month = 12;
                num2 = 1;
            }
            int num3 = 0;
            bool flag = false;
            int num4 = month;
            while (num4 >= num2)
            {
                this.tool_Month.Items.Add(num4.ToString("00") + "月份");
                if (num4 == this.Month)
                {
                    this.tool_Month.SelectedIndex = num3;
                    flag = true;
                }
                num4--;
                num3++;
            }
            this.tool_Month.Items.Add("全年");
            if ((this.Month == 0) || !flag)
            {
                this.tool_Month.SelectedIndex = num3;
            }
        }

        public void SetToolFpZl(int fpzlIndex)
        {
            try
            {
                if (this.tool_Fpzl.Items.Count <= 1)
                {
                    this.tool_Fpzl.Items.Clear();
                    this.SetFPZLAuthorization();
                    this.tool_Fpzl.SelectedIndex = 0;
                    this.tool_Fpzl.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                if ((fpzlIndex >= 0) && (fpzlIndex < this.tool_Fpzl.Items.Count))
                {
                    this.tool_Fpzl.SelectedIndex = fpzlIndex;
                }
                this._SearchType = SearchType.FPZLSearch;
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数SetToolFpZl异常]" + exception.ToString());
            }
        }

        public void SetToolStripStyle1024X678(ToolStrip toolScript)
        {
            Font font = new Font("微软雅黑", 8f);
            toolScript.AutoSize = false;
            toolScript.Height = SystemColor.TOOLSCRIPT_HEIGHT;
            toolScript.BackColor = SystemColor.BACKCOLOR;
            toolScript.RightToLeft = RightToLeft.Yes;
            toolScript.Font = font;
            foreach (ToolStripItem item in toolScript.Items)
            {
                item.AutoSize = false;
                item.Height = SystemColor.TOOLSCRIPT_CONTROL_HEIGHT;
                item.Font = font;
                item.RightToLeft = RightToLeft.No;
                if ((item is ToolStripComboBox) || (item is ToolStripTextBox))
                {
                    item.BackColor = SystemColor.GRID_ALTROW_BACKCOLOR;
                    item.Font = font;
                }
                else if ((item is ToolStripButton) || (item is ToolStripLabel))
                {
                    if (item.Text.Length == 2)
                    {
                        if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                        {
                            item.Width = 0x2d;
                        }
                        else
                        {
                            item.Width = 0x2d;
                        }
                    }
                    else if (item.Text.Length == 3)
                    {
                        if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                        {
                            item.Width = 0x2d;
                        }
                        else
                        {
                            item.Width = 0x2d;
                        }
                    }
                    else if (item.Text.Length == 4)
                    {
                        if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                        {
                            item.Width = 0x44;
                        }
                        else
                        {
                            item.Width = 0x44;
                        }
                    }
                    else if (item.Text.Length == 5)
                    {
                        if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                        {
                            item.Width = 0x49;
                        }
                        else
                        {
                            item.Width = 0x49;
                        }
                    }
                    else if (item.Text.Length == 6)
                    {
                        if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                        {
                            item.Width = 90;
                        }
                        else
                        {
                            item.Width = 90;
                        }
                    }
                    else if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                    {
                        item.Width = 100;
                    }
                    else
                    {
                        item.Width = 100;
                    }
                }
            }
        }

        private void SetYearMonthAuthorization()
        {
            this.tool_Year.Items.Clear();
            this.tool_Month.Items.Clear();
            int year = this.TaxCardClock.Year;
            for (int i = 0; year >= this._StartYear; i++)
            {
                this.tool_Year.Items.Add(year.ToString() + "年");
                if (year == this.Year)
                {
                    this.tool_Year.SelectedIndex = i;
                }
                year--;
            }
        }

        private void sm_MonthChange(object sender, MonthChangeEventArgs e)
        {
            try
            {
                if (this.editFPCX != EditFPCX.FuZhi)
                {
                    string data = PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXun_aisinoGrid_PageSize");
                    this.AvePageNum = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(data);
                    if (this.AvePageNum <= 0)
                    {
                        this.AvePageNum = 30;
                    }
                    this.sqlType = 1;
                    this.ChaXunDate(1, this.AvePageNum, 0, -1, this.sqlType);
                    this.SetBtnEnabled();
                    base.Show();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private bool TestNumberSequence(long a, long b)
        {
            try
            {
                if (a > b)
                {
                    long num = a;
                    a = b;
                    b = num;
                }
                if ((a != b) && ((a + 1L) != b))
                {
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数TestNumberSequence异常]" + exception.ToString());
                return false;
            }
        }

        private void tool_fasong_Click(object sender, EventArgs e)
        {
            try
            {
                this.xxfpChaXunBll.UpdateXXFPBSZT(null);
                UpLoadCheckState.ShouGongShangChuan = true;
                UpLoadCheckState.ShouGongXiaZai = true;
                MessageManager.ShowMsgBox("FPCX-000042");
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void tool_FPCXBSZT_Click(object sender, EventArgs e)
        {
            try
            {
                Rectangle bounds = this.tool_FPCXBSZT.Bounds;
                Point p = new Point(bounds.X, bounds.Y + bounds.Height);
                if (this.contextMenu_BSZT != null)
                {
                    this.contextMenu_BSZT.Show(base.PointToScreen(p));
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void tool_FPZJDaoChu_Click(object sender, EventArgs e)
        {
            FPZJDaoChu chu = new FPZJDaoChu(this.TaxCardClock);
            try
            {
                chu.ShowDialog();
            }
            catch (Exception exception)
            {
                this.loger.Error("[tool_FPZJDaoChu_Click函数异常]" + exception.ToString());
            }
            finally
            {
                if (chu != null)
                {
                    chu.Close();
                    chu.Dispose();
                    chu = null;
                }
            }
        }

        private void tool_Fpzl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.IsInitSearch && !this.chaXunTiaoJian.IsFpzl)
            {
                this.UpdateColumn(aisinoGrid.Columns, Aisino.Fwkp.Fpkj.Common.Tool.GetFPDBType(this.tool_Fpzl.Text));
                this.QuickSearch(SearchType.FPZLSearch);
            }
        }

        private void tool_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IsInitSearch = false;
            this.QuickSearch(SearchType.DATESearch);
        }

        private void tool_SendDisk_Click(object sender, EventArgs e)
        {
            try
            {
                if ((aisinoGrid == null) || (aisinoGrid.SelectedRows.Count <= 0))
                {
                    MessageManager.ShowMsgBox("没有选择任何行！");
                }
                else
                {
                    List<Fpxx> listModel = this.aisinoGridView_SelectRows(false);
                    new EmailManager().SaveXmlToDisk(listModel);
                    listModel = null;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void tool_SendMail_Click(object sender, EventArgs e)
        {
            try
            {
                if ((aisinoGrid == null) || (aisinoGrid.SelectedRows.Count <= 0))
                {
                    MessageManager.ShowMsgBox("没有选择任何行！");
                }
                else
                {
                    List<Fpxx> listModel = this.aisinoGridView_SelectRows(false);
                    new EmailManager().SendXmlByEmail(listModel);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void tool_SendToWebService_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBoxHelper.Show("缺少将发票信息发送到购方WebService");
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void tool_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = this.TaxCardClock.Year;
            if (this.tool_Year.Text.Length >= 4)
            {
                year = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.tool_Year.Text.Substring(0, 4));
            }
            int startYear = this._StartYear;
            int cardYear = this.TaxCardClock.Year;
            this.SetQuickSearchMonthList(year, startYear, cardYear);
            this.IsInitSearch = false;
        }

        private void toolTxt_quick_GotFocus(object sender, EventArgs e)
        {
            if (this.toolTxt_quick.Text == this._QuickSearchText)
            {
                this.toolTxt_quick.Text = "";
                this.toolTxt_quick.ForeColor = Color.Black;
                this.toolTxt_quick.Font = new Font("微软雅黑", 9f);
            }
        }

        private void toolTxt_quick_LostFocus(object sender, EventArgs e)
        {
            if (this.toolTxt_quick.Text == "")
            {
                this.toolTxt_quick.Text = this._QuickSearchText;
                this.toolTxt_quick.ForeColor = Color.Gray;
                this.toolTxt_quick.Font = new Font("微软雅黑", 9f, FontStyle.Italic);
            }
            this.sqlType = 2;
            this.ChaXunDate(1, this.AvePageNum, 0, -1, this.sqlType);
            this.SetBtnEnabled();
        }

        private void toolTxt_quick_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                this.DrawBorder(sender, e.Graphics, SystemColor.GRID_ALTROW_BACKCOLOR, Color.FromArgb(0, 0xbb, 0xff), this.toolTxt_quick.Width, this.toolTxt_quick.Height);
                Graphics graphics = e.Graphics;
                graphics.MeasureString("请输入关键字", this.toolTxt_quick.Font);
                SolidBrush brush = new SolidBrush(Color.Black);
                Rectangle bounds = this.toolTxt_quick.Bounds;
                StringFormat format = new StringFormat {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                };
                graphics.DrawString("请输入关键字", this.toolTxt_quick.Font, brush, bounds, format);
            }
            catch (Exception exception)
            {
                this.loger.Error("[FaPiaoChaXun_FPFZ_Paint函数异常]" + exception.ToString());
            }
        }

        private void UpdateColumn(DataGridViewColumnCollection columnsClections, string type)
        {
            if ((columnsClections != null) && (columnsClections.Count != 0))
            {
                switch (type)
                {
                    case "s":
                    case "c":
                    case "p":
                    case "q":
                        if (columnsClections.Contains("GFMC"))
                        {
                            columnsClections["GFMC"].HeaderText = "购方名称";
                        }
                        if (columnsClections.Contains("GFSH"))
                        {
                            columnsClections["GFSH"].HeaderText = "购方税号";
                        }
                        if (columnsClections.Contains("GFDZDH"))
                        {
                            columnsClections["GFDZDH"].HeaderText = "购方地址电话";
                        }
                        if (columnsClections.Contains("GFYHZH"))
                        {
                            columnsClections["GFYHZH"].HeaderText = "购方银行账号";
                        }
                        if (columnsClections.Contains("XSBM"))
                        {
                            columnsClections["XSBM"].HeaderText = "销售部门";
                        }
                        if (columnsClections.Contains("XFMC"))
                        {
                            columnsClections["XFMC"].HeaderText = "销方名称";
                        }
                        if (columnsClections.Contains("XFSH"))
                        {
                            columnsClections["XFSH"].HeaderText = "销方税号";
                        }
                        if (columnsClections.Contains("XFDZDH"))
                        {
                            columnsClections["XFDZDH"].HeaderText = "销方地址电话";
                        }
                        if (columnsClections.Contains("XFYHZH"))
                        {
                            columnsClections["XFYHZH"].HeaderText = "销方银行账号";
                        }
                        if (columnsClections.Contains("CM"))
                        {
                            columnsClections["CM"].HeaderText = "船名";
                        }
                        if (columnsClections.Contains("KHYHMC"))
                        {
                            columnsClections["KHYHMC"].HeaderText = "开户银行名称";
                        }
                        if (columnsClections.Contains("KHYHZH"))
                        {
                            columnsClections["KHYHZH"].HeaderText = "开户银行账号";
                        }
                        if (columnsClections.Contains("TYDH"))
                        {
                            columnsClections["TYDH"].HeaderText = "提运单号";
                        }
                        if (columnsClections.Contains("QYD"))
                        {
                            columnsClections["QYD"].HeaderText = "起运地";
                        }
                        if (columnsClections.Contains("ZHD"))
                        {
                            columnsClections["ZHD"].HeaderText = "装货地";
                        }
                        if (columnsClections.Contains("XHD"))
                        {
                            columnsClections["XHD"].HeaderText = "卸货地";
                        }
                        if (columnsClections.Contains("MDD"))
                        {
                            columnsClections["MDD"].HeaderText = "目的地";
                        }
                        if (columnsClections.Contains("YYZZH"))
                        {
                            columnsClections["YYZZH"].HeaderText = "营业执照号";
                        }
                        if (columnsClections.Contains("HYBM"))
                        {
                            columnsClections["HYBM"].HeaderText = "行业编码";
                        }
                        if (columnsClections.Contains("XFDZ"))
                        {
                            columnsClections["XFDZ"].HeaderText = "销方地址";
                        }
                        if (columnsClections.Contains("XFDH"))
                        {
                            columnsClections["XFDH"].HeaderText = "销方电话";
                        }
                        if (columnsClections.Contains("HJJE"))
                        {
                            columnsClections["HJJE"].HeaderText = "合计金额";
                        }
                        if (columnsClections.Contains("HJSE"))
                        {
                            columnsClections["HJSE"].HeaderText = "合计税额";
                        }
                        return;

                    case "f":
                        if (columnsClections.Contains("GFMC"))
                        {
                            columnsClections["GFMC"].HeaderText = "实际受票方名称";
                        }
                        if (columnsClections.Contains("GFSH"))
                        {
                            columnsClections["GFSH"].HeaderText = "受票方纳税人识别号";
                        }
                        if (columnsClections.Contains("GFDZDH"))
                        {
                            columnsClections["GFDZDH"].HeaderText = "收货人名称";
                        }
                        if (columnsClections.Contains("GFYHZH"))
                        {
                            columnsClections["GFYHZH"].HeaderText = "主管税务机关名称";
                        }
                        if (columnsClections.Contains("XSBM"))
                        {
                            columnsClections["XSBM"].HeaderText = "身份证号码/组织机构代码";
                        }
                        if (columnsClections.Contains("XFMC"))
                        {
                            columnsClections["XFMC"].HeaderText = "承运人名称";
                        }
                        if (columnsClections.Contains("XFSH"))
                        {
                            columnsClections["XFSH"].HeaderText = "承运人纳税人识别号";
                        }
                        if (columnsClections.Contains("销方地址电话"))
                        {
                            columnsClections["销方地址电话"].HeaderText = "发货人名称";
                        }
                        if (columnsClections.Contains("XFYHZH"))
                        {
                            columnsClections["XFYHZH"].HeaderText = "起运地";
                        }
                        if (columnsClections.Contains("CM"))
                        {
                            columnsClections["CM"].HeaderText = "收货人纳税人识别号";
                        }
                        if (columnsClections.Contains("KHYHMC"))
                        {
                            columnsClections["KHYHMC"].HeaderText = "开户银行名称";
                        }
                        if (columnsClections.Contains("KHYHZH"))
                        {
                            columnsClections["KHYHZH"].HeaderText = "开户银行账号";
                        }
                        if (columnsClections.Contains("TYDH"))
                        {
                            columnsClections["TYDH"].HeaderText = "发货人纳税人识别号";
                        }
                        if (columnsClections.Contains("QYD"))
                        {
                            columnsClections["QYD"].HeaderText = "车种车号";
                        }
                        if (columnsClections.Contains("ZHD"))
                        {
                            columnsClections["ZHD"].HeaderText = "装货地";
                        }
                        if (columnsClections.Contains("XHD"))
                        {
                            columnsClections["XHD"].HeaderText = "卸货地";
                        }
                        if (columnsClections.Contains("MDD"))
                        {
                            columnsClections["MDD"].HeaderText = "目的地";
                        }
                        if (columnsClections.Contains("YYZZH"))
                        {
                            columnsClections["YYZZH"].HeaderText = "车船吨位";
                        }
                        if (columnsClections.Contains("HYBM"))
                        {
                            columnsClections["HYBM"].HeaderText = "主管税务机关代码";
                        }
                        if (columnsClections.Contains("XFDZ"))
                        {
                            columnsClections["XFDZ"].HeaderText = "销方地址";
                        }
                        if (columnsClections.Contains("XFDH"))
                        {
                            columnsClections["XFDH"].HeaderText = "销方电话";
                        }
                        if (columnsClections.Contains("HJJE"))
                        {
                            columnsClections["HJJE"].HeaderText = "合计金额";
                        }
                        if (columnsClections.Contains("HJSE"))
                        {
                            columnsClections["HJSE"].HeaderText = "合计税额";
                        }
                        return;

                    case "j":
                        if (columnsClections.Contains("GFMC"))
                        {
                            columnsClections["GFMC"].HeaderText = "购货单位（人）";
                        }
                        if (columnsClections.Contains("GFSH"))
                        {
                            columnsClections["GFSH"].HeaderText = "购方税号";
                        }
                        if (columnsClections.Contains("GFDZDH"))
                        {
                            columnsClections["GFDZDH"].HeaderText = "车辆类型";
                        }
                        if (columnsClections.Contains("GFYHZH"))
                        {
                            columnsClections["GFYHZH"].HeaderText = "主管税务机关名称";
                        }
                        if (columnsClections.Contains("XSBM"))
                        {
                            columnsClections["XSBM"].HeaderText = "身份证号码/组织机构代码";
                        }
                        if (columnsClections.Contains("XFMC"))
                        {
                            columnsClections["XFMC"].HeaderText = "销货单位名称";
                        }
                        if (columnsClections.Contains("XFSH"))
                        {
                            columnsClections["XFSH"].HeaderText = "纳税人识别号";
                        }
                        if (columnsClections.Contains("XFDZDH"))
                        {
                            columnsClections["XFDZDH"].HeaderText = "地址";
                        }
                        if (columnsClections.Contains("XFYHZH"))
                        {
                            columnsClections["XFYHZH"].HeaderText = "开户银行";
                        }
                        if (columnsClections.Contains("CM"))
                        {
                            columnsClections["CM"].HeaderText = "合格证书";
                        }
                        if (columnsClections.Contains("KHYHMC"))
                        {
                            columnsClections["KHYHMC"].HeaderText = "产地";
                        }
                        if (columnsClections.Contains("KHYHZH"))
                        {
                            columnsClections["KHYHZH"].HeaderText = "账号";
                        }
                        if (columnsClections.Contains("TYDH"))
                        {
                            columnsClections["TYDH"].HeaderText = "进口证明书号";
                        }
                        if (columnsClections.Contains("QYD"))
                        {
                            columnsClections["QYD"].HeaderText = "商检单号";
                        }
                        if (columnsClections.Contains("ZHD"))
                        {
                            columnsClections["ZHD"].HeaderText = "发动机号码";
                        }
                        if (columnsClections.Contains("XHD"))
                        {
                            columnsClections["XHD"].HeaderText = "车架号码/车辆识别号";
                        }
                        if (columnsClections.Contains("MDD"))
                        {
                            columnsClections["MDD"].HeaderText = "限乘人数";
                        }
                        if (columnsClections.Contains("YYZZH"))
                        {
                            columnsClections["YYZZH"].HeaderText = "吨位";
                        }
                        if (columnsClections.Contains("HYBM"))
                        {
                            columnsClections["HYBM"].HeaderText = "税务机关代码";
                        }
                        if (columnsClections.Contains("XFDZ"))
                        {
                            columnsClections["XFDZ"].HeaderText = "厂牌型号";
                        }
                        if (columnsClections.Contains("XFDH"))
                        {
                            columnsClections["XFDH"].HeaderText = "电话";
                        }
                        if (columnsClections.Contains("HJJE"))
                        {
                            columnsClections["HJJE"].HeaderText = "不含税价";
                        }
                        if (columnsClections.Contains("HJSE"))
                        {
                            columnsClections["HJSE"].HeaderText = "税额";
                        }
                        return;
                }
            }
        }

        public void WenBenSelectedFPListPrint(List<FpPrint> AllFpPrintList, bool ISQD = false)
        {
            DyPrompt prompt = new DyPrompt();
            int key = 0;
            int isLastFp = 0;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            int num3 = 0;
            int num4 = 0;
            if (AllFpPrintList.Count == 1)
            {
                dict.Clear();
                dict.Add("lbl_Fpzl", AllFpPrintList[0].fpzl);
                dict.Add("lbl_Fphm", AllFpPrintList[0].fphm);
                dict.Add("lbl_Fpdm", AllFpPrintList[0].fpdm);
                dict.Add("lbl_Qdbz", AllFpPrintList[0].qdbz);
                isLastFp = 1;
                if (!AllFpPrintList[0].dybz)
                {
                    if (ISQD && !AllFpPrintList[0].qdbz)
                    {
                        MessageManager.ShowMsgBox("FPCX-000004");
                        return;
                    }
                    if (this.FpDaYin(isLastFp, true, dict, false, ISQD))
                    {
                        num3++;
                        this.FpPrintResultHash.Add(0, 1);
                    }
                }
                else if (!ISQD)
                {
                    if ((DialogResult.OK == MessageManager.ShowMsgBox("FPCX-000024")) && this.FpDaYin(isLastFp, false, dict, false, ISQD))
                    {
                        num3++;
                        this.FpPrintResultHash.Add(0, 1);
                    }
                }
                else
                {
                    if (ISQD && !AllFpPrintList[0].qdbz)
                    {
                        MessageManager.ShowMsgBox("FPCX-000004");
                        return;
                    }
                    if (this.FpDaYin(isLastFp, false, dict, false, ISQD))
                    {
                        num3++;
                        this.FpPrintResultHash.Add(0, 1);
                    }
                }
            }
            else
            {
                bool flag = false;
                bool flag2 = false;
                string fpzl = "";
                int num5 = 0;
                int num6 = AllFpPrintList.Count - 1;
                AllFpPrintList.Sort(FpPrint.defautComparer);
                if (!ISQD)
                {
                    for (key = 0; key <= num6; key++)
                    {
                        dict.Clear();
                        dict.Add("lbl_Fpzl", AllFpPrintList[key].fpzl);
                        dict.Add("lbl_Fphm", AllFpPrintList[key].fphm);
                        dict.Add("lbl_Fpdm", AllFpPrintList[key].fpdm);
                        isLastFp = 1;
                        if (flag && flag2)
                        {
                            if (key <= num6)
                            {
                                int num7 = 0;
                                if (key > 0)
                                {
                                    num7 = key - 1;
                                }
                                fpzl = AllFpPrintList[num7].fpzl;
                                string fpdm = AllFpPrintList[num7].fpdm;
                                num5 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(AllFpPrintList[num7].fphm.ToString());
                            }
                            _InvoiceType invoiceType = this.GetInvoiceType(fpzl);
                            _InvoiceType type2 = this.GetInvoiceType(AllFpPrintList[key].fpzl);
                            string msg = "确定要继续打印发票？";
                            if (type2.dbfpzl != invoiceType.dbfpzl)
                            {
                                msg = "本张发票的发票种类与上一张发票的发票种类不同，请确认并及时更换打印机中将要打印的纸质发票！";
                                isLastFp = 0;
                            }
                            else if (!this.TestNumberSequence((long) num5, (long) AllFpPrintList[key].fphm))
                            {
                                msg = "本张发票的发票号码与上一张发票的发票号码不连续，请确认并及时更换打印机中将要打印的纸质发票。";
                                isLastFp = 0;
                            }
                            if (isLastFp == 0)
                            {
                                if (!prompt.setValue(dict, msg, ISQD))
                                {
                                    this.loger.Error("[发票查询打印确认参数设置失败]：" + msg);
                                    return;
                                }
                                DialogResult result = prompt.ShowDialog();
                                if (DialogResult.Abort != result)
                                {
                                    if (DialogResult.OK != result)
                                    {
                                        if (DialogResult.Yes != result)
                                        {
                                            if (DialogResult.Cancel == result)
                                            {
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            isLastFp = 0;
                                            flag = false;
                                            if (flag2 = this.FpDaYin(isLastFp, true, dict, false, false))
                                            {
                                                num3++;
                                                this.FpPrintResultHash.Add(key, 1);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        isLastFp = 0;
                                        flag = true;
                                        if (flag2 = this.FpDaYin(isLastFp++, true, dict, true, false))
                                        {
                                            num3++;
                                            this.FpPrintResultHash.Add(key, 1);
                                        }
                                    }
                                }
                                else
                                {
                                    flag = true;
                                }
                            }
                            else
                            {
                                flag = true;
                                if (flag2 = this.FpDaYin(isLastFp++, true, dict, true, false))
                                {
                                    num3++;
                                    this.FpPrintResultHash.Add(key, 1);
                                }
                            }
                        }
                        else
                        {
                            if (!prompt.setValue(dict, "确定要继续打印发票？", ISQD))
                            {
                                this.loger.Error("[发票查询打印确认参数设置失败]：确定要继续打印");
                                return;
                            }
                            DialogResult result2 = prompt.ShowDialog();
                            if (DialogResult.Abort == result2)
                            {
                                flag = false;
                            }
                            else if (DialogResult.OK == result2)
                            {
                                isLastFp = 0;
                                flag = true;
                                if (flag2 = this.FpDaYin(isLastFp++, true, dict, true, false))
                                {
                                    num3++;
                                    this.FpPrintResultHash.Add(key, 1);
                                }
                            }
                            else if (DialogResult.Yes == result2)
                            {
                                isLastFp = 0;
                                flag = false;
                                if (flag2 = this.FpDaYin(isLastFp++, true, dict, true, false))
                                {
                                    num3++;
                                    this.FpPrintResultHash.Add(key, 1);
                                }
                            }
                            else if (DialogResult.Cancel == result2)
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    for (key = 0; key <= num6; key++)
                    {
                        if (AllFpPrintList[key].qdbz)
                        {
                            dict.Clear();
                            dict.Add("lbl_Fpzl", AllFpPrintList[key].fpzl);
                            dict.Add("lbl_Fphm", AllFpPrintList[key].fphm);
                            dict.Add("lbl_Fpdm", AllFpPrintList[key].fpdm);
                            dict.Add("lbl_Qdbz", AllFpPrintList[key].qdbz);
                            isLastFp = 1;
                            if (flag && flag2)
                            {
                                flag = true;
                                if (flag2 = this.FpDaYin(isLastFp++, true, dict, true, true))
                                {
                                    num3++;
                                }
                            }
                            else
                            {
                                if (!prompt.setValue(dict, "确定要继续打印清单？", ISQD))
                                {
                                    this.loger.Error("[发票查询打印确认参数设置失败]：确定要继续打印");
                                    return;
                                }
                                DialogResult result3 = prompt.ShowDialog();
                                if (DialogResult.Abort == result3)
                                {
                                    flag = false;
                                }
                                else if (DialogResult.OK == result3)
                                {
                                    isLastFp = 0;
                                    flag = true;
                                    if (flag2 = this.FpDaYin(isLastFp++, true, dict, true, true))
                                    {
                                        num3++;
                                    }
                                }
                                else if (DialogResult.Yes == result3)
                                {
                                    isLastFp = 0;
                                    flag = false;
                                    if (flag2 = this.FpDaYin(isLastFp++, true, dict, true, true))
                                    {
                                        num3++;
                                    }
                                }
                                else if (DialogResult.Cancel == result3)
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            num4 = AllFpPrintList.Count - num3;
            if (num4 < 0)
            {
                num4 = 0;
            }
            MessageManager.ShowMsgBox("FPCX-000025", new string[] { num3.ToString(), num4.ToString() });
        }

        private string FILE_PATH
        {
            get
            {
                return this._strFilePath.Trim();
            }
            set
            {
                this._strFilePath = value.Trim();
            }
        }

        public string FpzlKind
        {
            get
            {
                return this._FpzlKind;
            }
            set
            {
                this._FpzlKind = value;
            }
        }

        public object[] OBJECT_FPFZ
        {
            get
            {
                return this._Object_FPFZ;
            }
        }

        public int StartMonth
        {
            get
            {
                return this._StartMonth;
            }
            set
            {
                this._StartMonth = value;
            }
        }

        public int StartYear
        {
            get
            {
                return this._StartYear;
            }
            set
            {
                this._StartYear = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct _InvoiceType
        {
            public string dbfpzl;
            public InvoiceType TaxCardfpzl;
        }

        public enum EditFPCX
        {
            ChaXun,
            ZuoFei,
            FuZhi
        }

        public class FpPrint : IComparer<FaPiaoChaXun.FpPrint>
        {
            public static IComparer<FaPiaoChaXun.FpPrint> defautComparer = new FaPiaoChaXun.FpPrint();
            public bool dybz;
            public string fpdm;
            public int fphm;
            public string fpzl;
            public int index;
            public bool qdbz;

            public int Compare(FaPiaoChaXun.FpPrint a, FaPiaoChaXun.FpPrint b)
            {
                if (a.fpdm.CompareTo(b.fpdm) != 0)
                {
                    return a.fpdm.CompareTo(b.fpdm);
                }
                if (a.fphm < b.fphm)
                {
                    return -1;
                }
                return 1;
            }
        }

        private delegate void PerformStepHandle(int step);

        private enum SearchType
        {
            FPZLSearch,
            DATESearch,
            BSZTSearch,
            OTHERSearch
        }
    }
}

