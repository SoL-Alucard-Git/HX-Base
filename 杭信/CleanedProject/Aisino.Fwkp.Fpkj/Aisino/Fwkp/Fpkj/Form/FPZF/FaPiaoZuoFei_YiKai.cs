namespace Aisino.Fwkp.Fpkj.Form.FPZF
{
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.DAL;
    using Aisino.Fwkp.Fpkj.Form.FPCX;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class FaPiaoZuoFei_YiKai : DockForm
    {
        private int _iPageNO;
        private int _iPageSize;
        private object[] _Object_FPFZ = new object[] { "", "", "" };
        private string _QuickSearchText = "请输入检索关键字...";
        private string _strFilePath = string.Empty;
        private XMLOperation _XmlOperation = new XMLOperation();
        public AisinoDataGrid aisinoGrid;
        public int AvePageNum = 30;
        public static DateTime CardClock;
        private ChaXunTiaoJian chaXunTiaoJian;
        private IContainer components;
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        private ContextMenuStrip contextMenu_BSZT;
        private XXFP Dal = new XXFP(false);
        private FaPiaoChaXun.EditFPCX editFPCX = FaPiaoChaXun.EditFPCX.ZuoFei;
        public Dictionary<string, object> gridviewRowDict = new Dictionary<string, object>();
        private ILog loger = LogUtil.GetLogger<FaPiaoZuoFei_YiKai>();
        private ToolStripMenuItem MenuItem_Yanqianshibai;
        public int Month;
        private int sqlType;
        private ToolStripButton tool_ChaKanMingXi;
        private ToolStripButton tool_chazhao;
        private ToolStripButton tool_daying;
        private ToolStripButton tool_Exit;
        private ToolStripSplitButton tool_FPZFBSZT;
        private ToolStripButton tool_GeShi;
        private ToolStripButton tool_tongji;
        private ToolStripButton tool_ZuoFei;
        private ToolStripLabel toolLabel_quick;
        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripTextBox toolTxt_quick;
        public List<Dictionary<string, object>> WasteFpCondition = new List<Dictionary<string, object>>();
        private XmlComponentLoader xmlComponentLoader1;
        public int Year = 0x7d0;

        public FaPiaoZuoFei_YiKai()
        {
            try
            {
                base.Hide();
                if (CardClock.Year <= DingYiZhiFuChuan1.dataTimeCCRQ.Year)
                {
                    CardClock = base.TaxCardInstance.GetCardClock();
                }
                this.Initialize();
                base.TabText = "选择发票号码作废";
                this.Text = "选择发票号码作废";
                base.Hide();
                this.InsertColumn();
                if (this.chaXunTiaoJian == null)
                {
                    this.chaXunTiaoJian = new ChaXunTiaoJian(CardClock);
                }
                this.toolTxt_quick.Text = this._QuickSearchText;
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
                if ((this.editFPCX == FaPiaoChaXun.EditFPCX.FuZhi) && this.GetValueFPFZ())
                {
                    base.Close();
                }
                else if (!this.IsEmptyFPCurrentData(this.aisinoGrid.CurrentCell, string.Empty))
                {
                    DataGridViewRowCollection rowsAll = this.aisinoGrid.Rows;
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
                PropertyUtil.SetValue("Aisino.Fwkp.Fpkj.Form.FPZF_FaPiaoZuoFei_YiKai_aisinoGrid_PageSize", e.PageSize.ToString());
                this.ChaXunDate(e.PageNO, e.PageSize, 2, 0, this.sqlType);
                this.SetBtnEnabled();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
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
                this.chaXunTiaoJian.AisinoGrid = this.aisinoGrid;
                if (this.chaXunTiaoJian.ShowDialog(this) == DialogResult.OK)
                {
                    this.AvePageNum = this._iPageSize;
                    this.sqlType = 0;
                    this.ChaXunDate(1, this.AvePageNum, 2, -1, this.sqlType);
                    this.SetBtnEnabled();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void btnDaYing_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoGrid.SelectedRows.Count > 0)
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
                if ((this.aisinoGrid.SelectedRows != null) && (0 < this.aisinoGrid.SelectedRows.Count))
                {
                    DataGridViewRow row = this.aisinoGrid.SelectedRows[0];
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Clear();
                    dict.Add("lbl_FpHm", Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(row.Cells["FPHM"].Value.ToString()));
                    dict.Add("lbl_FpDm", row.Cells["FPDM"].Value.ToString());
                    dict.Add("lbl_GfSh", row.Cells["GFSH"].Value.ToString());
                    dict.Add("lbl_XfSh", base.TaxCardInstance.TaxCode);
                    dict.Add("lbl_JE", Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(row.Cells["HJJE"].Value.ToString()));
                    dict.Add("lbl_SE", Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(row.Cells["HJSE"].Value.ToString()));
                    dict.Add("lbl_ZfBz", row.Cells["ZFBZ"].Value.ToString());
                    dict.Add("lbl_KpRq", row.Cells["KPRQ"].Value.ToString());
                    dict.Add("lbl_SLv", Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(row.Cells["SLv"].Value));
                    dict.Add("lbl_Fpzl", Convert.ToString(row.Cells["FPZL"].Value.ToString()));
                    dict.Add("lbl_Zfbz", Convert.ToString(row.Cells["ZFBZ"].Value.ToString()));
                    HeDui dui = new HeDui();
                    if (dui.setValue(dict))
                    {
                        dui.ShowDialog();
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void btnXuanzhe_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoGrid.SelectedRows.Count >= 1)
                {
                    if ((this.editFPCX == FaPiaoChaXun.EditFPCX.FuZhi) && this.GetValueFPFZ())
                    {
                        base.Close();
                    }
                    else
                    {
                        DataGridViewRowCollection rowsAll = this.aisinoGrid.Rows;
                        this.ChaKanMingXi(rowsAll);
                        this.SetBtnEnabled();
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
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
                DataGridViewRowCollection rowsAll = this.aisinoGrid.Rows;
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

        private void but_zuofei_Click(object sender, EventArgs e)
        {
            try
            {
                if (0 < this.aisinoGrid.Rows.Count)
                {
                    DataGridViewSelectedRowCollection rows = this.aisinoGrid.SelectedRows;
                    if (this.aisinoGrid.SelectedRows.Count <= 0)
                    {
                        MessageManager.ShowMsgBox("FPZF-000001");
                    }
                    else if (DialogResult.OK == MessageManager.ShowMsgBox("FPCX-000037", "发票作废", new string[] { Convert.ToString(rows.Count) }))
                    {
                        int num = 0;
                        int num2 = 0;
                        List<DaiKaiXml.SWDKDMHM> swdkZyList = null;
                        List<DaiKaiXml.SWDKDMHM> swdkPtList = null;
                        if (Aisino.Fwkp.Fpkj.Common.Tool.IsShuiWuDKSQ())
                        {
                            swdkZyList = new List<DaiKaiXml.SWDKDMHM>();
                            swdkPtList = new List<DaiKaiXml.SWDKDMHM>();
                        }
                        int count = rows.Count;
                        this.WasteFpCondition.Clear();
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewRow row = rows[i];
                            this.gridviewRowDict.Clear();
                            for (int j = 0; j < this.aisinoGrid.Columns.Count; j++)
                            {
                                string key = this.aisinoGrid.Columns[j].Name.ToString();
                                string fplb = row.Cells[key].Value.ToString();
                                string str8 = key;
                                if (str8 != null)
                                {
                                    if (!(str8 == "FPZL"))
                                    {
                                        if (str8 == "SLV")
                                        {
                                            goto Label_014E;
                                        }
                                        if (str8 == "PZZT")
                                        {
                                            goto Label_0159;
                                        }
                                        if (str8 == "BSZT")
                                        {
                                            goto Label_0164;
                                        }
                                    }
                                    else
                                    {
                                        fplb = Aisino.Fwkp.Fpkj.Common.Tool.GetFPDBType(fplb);
                                    }
                                }
                                goto Label_016D;
                            Label_014E:
                                fplb = Aisino.Fwkp.Fpkj.Common.Tool.GetDBSlv(fplb);
                                goto Label_016D;
                            Label_0159:
                                fplb = Aisino.Fwkp.Fpkj.Common.Tool.GetDBPZZT(fplb);
                                goto Label_016D;
                            Label_0164:
                                fplb = Aisino.Fwkp.Fpkj.Common.Tool.GetDBPZZT(fplb);
                            Label_016D:
                                this.gridviewRowDict.Add(key, fplb);
                            }
                            string fPDBType = Aisino.Fwkp.Fpkj.Common.Tool.GetFPDBType(row.Cells["FPZL"].Value.ToString());
                            string fpdm = row.Cells["FPDM"].Value.ToString();
                            string fphm = row.Cells["FPHM"].Value.ToString();
                            string str6 = this.YiKaiZuoFeiMainFunction(fPDBType, fpdm, fphm, swdkZyList, swdkPtList, 0);
                            if ("0000" == str6)
                            {
                                this.tool_ZuoFei.Enabled = false;
                                num++;
                                row.Cells["ZFBZ"].Value = "是";
                            }
                            else if (str6 == "0001")
                            {
                                break;
                            }
                        }
                        this.SaveToDB();
                        num2 = rows.Count - num;
                        string message = string.Concat(new object[] { "本期作废发票:", rows.Count, "张，\r\n成功作废发票:  ", num, "张，\r\n失败作废发票:  ", num2, "张。" });
                        MessageManager.ShowMsgBox("FPZF-000007", "提示", new string[] { Convert.ToString(rows.Count), Convert.ToString(num), Convert.ToString(num2) });
                        if (Aisino.Fwkp.Fpkj.Common.Tool.IsShuiWuDKSQ())
                        {
                            new DaiKaiXml().DaiKaiFpZuoFeiUpload(swdkZyList, swdkPtList);
                        }
                        this.loger.Info(message);
                        if (num > 0)
                        {
                            this.ChaXunDate(this._iPageNO, this._iPageSize, 2, -1, this.sqlType);
                            this.SetBtnEnabled();
                        }
                    }
                }
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
                string str;
                string str7;
                if (rowsAll.Count >= 1)
                {
                    if (this.aisinoGrid.SelectedRows.Count <= 0)
                    {
                        MessageManager.ShowMsgBox("FPZF-000001");
                        return false;
                    }
                    if (this.aisinoGrid.CurrentCell == null)
                    {
                        this.aisinoGrid.CurrentCell = this.aisinoGrid.SelectedRows[0].Cells[0];
                    }
                    list = new List<string[]>();
                    str = Convert.ToString(this.aisinoGrid.CurrentCell.RowIndex);
                    int num = 0;
                    List<int> list2 = new List<int>();
                    list2.Clear();
                    foreach (DataGridViewRow row in (IEnumerable) rowsAll)
                    {
                        if (row.Cells["FPDM"].Value != null)
                        {
                            string str2 = Convert.ToString(row.Cells["FPDM"].Value);
                            string s = Convert.ToString(row.Cells["FPHM"].Value);
                            string str4 = Convert.ToString(row.Cells["ZFBZ"].Value);
                            string str5 = row.Cells["YYSBZ"].Value.ToString();
                            int rowIndex = row.Cells[0].RowIndex;
                            int result = 0;
                            int.TryParse(s, out result);
                            s = Convert.ToString(result);
                            string dbfpzl = this.GetInvoiceType(row.Cells["FPZL"].Value.ToString()).dbfpzl;
                            str4 = (str4.Trim() == "是") ? "1" : "0";
                            string[] item = new string[] { dbfpzl, str2.ToString(), s.ToString(), str4, Convert.ToString(rowIndex), str5 };
                            if (rowIndex == this.aisinoGrid.CurrentCell.RowIndex)
                            {
                                str = Convert.ToString((int) (this.aisinoGrid.CurrentCell.RowIndex - num));
                            }
                            if (!this.IsEmptyFPCurrentData(row.Cells["GFSH"], dbfpzl))
                            {
                                list2.Add(list.Count);
                                list.Add(item);
                            }
                            else
                            {
                                num++;
                                list2.Add(-1000);
                            }
                        }
                    }
                    if (list.Count <= 0)
                    {
                        MessageManager.ShowMsgBox("FPCX-000003");
                        return false;
                    }
                    str7 = "0";
                    if (this.editFPCX == FaPiaoChaXun.EditFPCX.ChaXun)
                    {
                        str7 = "0";
                        goto Label_02DD;
                    }
                    if (FaPiaoChaXun.EditFPCX.ZuoFei == this.editFPCX)
                    {
                        str7 = "1";
                        goto Label_02DD;
                    }
                    MessageManager.ShowMsgBox("FPCX-000027");
                }
                return false;
            Label_02DD:;
                object[] objArray = new object[] { str7, str, list };
                object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray);
                if (this.editFPCX == FaPiaoChaXun.EditFPCX.FuZhi)
                {
                    return false;
                }
                if ((objArray2 == null) || (objArray2.Length < 2))
                {
                    MessageManager.ShowMsgBox("FPCX-000028");
                    return false;
                }
                int num4 = (int) objArray2[0];
                bool flag = false;
                if (FaPiaoChaXun.EditFPCX.ZuoFei == this.editFPCX)
                {
                    this.aisinoGrid.ClearSelection();
                    this.aisinoGrid.CurrentCell = null;
                    list = objArray2[1] as List<string[]>;
                    foreach (string[] strArray2 in list)
                    {
                        strArray2[0].Trim();
                        strArray2[1].Trim();
                        strArray2[2].Trim();
                        Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(strArray2[4].Trim());
                        if ("1" == strArray2[3].Trim())
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    this.ChaXunDate(this._iPageNO, this._iPageSize, 2, -1, this.sqlType);
                    this.SetBtnEnabled();
                }
                if (((this.aisinoGrid != null) && (this.aisinoGrid.Rows != null)) && ((num4 >= 0) && (num4 < this.aisinoGrid.Rows.Count)))
                {
                    this.aisinoGridViewUnSelectedRows(this.aisinoGrid.SelectedRows);
                    this.aisinoGrid.SetSelectRows(num4);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
                return false;
            }
            return true;
        }

        public void ChaXunDate(int page, int num, int TiaojianChaXun, int type = -1, int sqlType = 0)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Clear();
                this._iPageNO = page;
                this._iPageSize = num;
                dict.Add("ZFBZ", "0");
                string str = string.Concat(new object[] { CardClock.Year, "-", CardClock.Month.ToString("00"), "-01 00:00:00" });
                string str2 = string.Concat(new object[] { CardClock.Year, "-", CardClock.Month.ToString("00"), "-", DateTime.DaysInMonth(CardClock.Year, CardClock.Month), " 23:59:59" });
                string strB = this.chaXunTiaoJian.DateStart.ToString("yyyy-MM-dd 00:00:00");
                string str4 = this.chaXunTiaoJian.DateEnd.ToString("yyyy-MM-dd 23:59:59");
                if (str.CompareTo(strB) < 0)
                {
                    str = strB;
                }
                if (str2.CompareTo(str4) > 0)
                {
                    str2 = str4;
                }
                dict.Add("KsRq", str);
                dict.Add("JsRq", str2);
                dict.Add("FPZL", this.chaXunTiaoJian.KindQry.Trim());
                if (DingYiZhiFuChuan1._UserMsg.IsAdmin)
                {
                    dict.Add("AdminBz", 1);
                }
                else
                {
                    dict.Add("AdminBz", 0);
                }
                dict.Add("Admin", DingYiZhiFuChuan1._UserMsg.MC);
                if (this.MenuItem_Yanqianshibai.Checked)
                {
                    dict.Add("BSZT", 4);
                }
                else
                {
                    dict.Add("BSZT", -1);
                }
                int sortWay = 1;
                string str5 = "";
                string str6 = "";
                string str7 = "";
                string str8 = "";
                switch (PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXunTiaoJian_SortWay"))
                {
                    case "1":
                    case "":
                    {
                        sortWay = 1;
                        string str10 = this.toolTxt_quick.Text.Trim(new char[] { ' ', ' ' });
                        str5 = str10;
                        str6 = str10;
                        str7 = this.chaXunTiaoJian.NameQry.Trim();
                        str7 = this.chaXunTiaoJian.NameQry.Trim();
                        if (str7 == "")
                        {
                            str7 = str10;
                        }
                        str8 = this.chaXunTiaoJian.TaxCodeQry.Trim();
                        if (str8 == "")
                        {
                            str8 = str10;
                        }
                        if (((str5 == this._QuickSearchText) && (str6 == this._QuickSearchText)) && ((str7 == this._QuickSearchText) && (str8 == this._QuickSearchText)))
                        {
                            str6 = "";
                            str5 = "";
                            str7 = "";
                            str8 = "";
                        }
                        dict.Add("FPDM", "%" + str5 + "%");
                        dict.Add("GFMC", "%" + str7 + "%");
                        dict.Add("GFSH", "%" + str8 + "%");
                        if (Aisino.Fwkp.Fpkj.Common.Tool.CharNumInString(str10, '0') != str10.Length)
                        {
                            str6 = str10.TrimStart(new char[] { '0' });
                        }
                        dict.Add("FPHM", "%" + str6 + "%");
                        break;
                    }
                    default:
                    {
                        sortWay = 0;
                        string str11 = this.toolTxt_quick.Text.Trim(new char[] { ' ', ' ' });
                        str5 = str11;
                        str6 = str11;
                        str7 = this.chaXunTiaoJian.NameQry.Trim();
                        if (str7 == "")
                        {
                            str7 = str11;
                        }
                        str8 = this.chaXunTiaoJian.TaxCodeQry.Trim();
                        if (str8 == "")
                        {
                            str8 = str11;
                        }
                        if (((str5 == this._QuickSearchText) && (str6 == this._QuickSearchText)) && ((str7 == this._QuickSearchText) && (str8 == this._QuickSearchText)))
                        {
                            str6 = "";
                            str5 = "";
                            str7 = "";
                            str8 = "";
                        }
                        dict.Add("FPDM", str5);
                        dict.Add("GFMC", str7);
                        dict.Add("GFSH", str8);
                        if (Aisino.Fwkp.Fpkj.Common.Tool.CharNumInString(str11, '0') != str11.Length)
                        {
                            str6 = str11.TrimStart(new char[] { '0' });
                        }
                        dict.Add("FPHM", str6);
                        break;
                    }
                }
                AisinoDataSet set = this.Dal.SelectPage(page, num, TiaojianChaXun, dict, sortWay, CardClock, type, sqlType);
                this.aisinoGrid.DataSource = set;
                this.InsertColumn();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
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

        public void Edit(FaPiaoChaXun.EditFPCX edit)
        {
            try
            {
                this.editFPCX = edit;
                if (edit == FaPiaoChaXun.EditFPCX.ZuoFei)
                {
                    this.Text = "选择发票号码作废";
                    base.TabText = "选择发票号码作废";
                    this.tool_ZuoFei.Visible = true;
                    this.tool_chazhao.Visible = true;
                    this.Month = CardClock.Month;
                    this.Year = CardClock.Year;
                    string data = PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.FPZF_FaPiaoZuoFei_YiKai_aisinoGrid_PageSize");
                    new Aisino.Fwkp.Fpkj.Common.Tool();
                    this.AvePageNum = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(data);
                    if (this.AvePageNum <= 0)
                    {
                        this.AvePageNum = 30;
                    }
                    this.sqlType = 1;
                    this.ChaXunDate(1, this.AvePageNum, 2, -1, this.sqlType);
                    if (this.QuitForm_IsEmpty())
                    {
                        return;
                    }
                    base.Visible = true;
                }
                if (((this.aisinoGrid.Rows != null) && (this.aisinoGrid.SelectedRows != null)) && ((this.aisinoGrid.Rows.Count > 0) && (this.aisinoGrid.SelectedRows.Count <= 0)))
                {
                    this.aisinoGrid.SetSelectRows(0);
                }
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

        private void FaPiaoZuoFei_YiKai_FormClosing(object sender, EventArgs e)
        {
            this.Dal = null;
            this._XmlOperation = null;
            this.loger = null;
            this.condition = null;
            this.aisinoGrid = null;
        }

        private void FaPiaoZuoFei_YiKai_FormClosing(object sender, FormClosingEventArgs e)
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

        private _InvoiceType GetInvoiceType(string type)
        {
            _InvoiceType type2;
            switch (type)
            {
                case "普通发票":
                case "收购发票":
                case "农产品销售发票":
                    type2.dbfpzl = "c";
                    type2.TaxCardfpzl = (InvoiceType)(InvoiceType)2;
                    type2.fplx = (FPLX)2;
                    return type2;

                case "专用发票":
                    type2.dbfpzl = "s";
                    type2.TaxCardfpzl = (InvoiceType)0;
                    type2.fplx = (FPLX)0;
                    return type2;

                case "机动车销售统一发票":
                    type2.dbfpzl = "j";
                    type2.TaxCardfpzl = (InvoiceType)12;
                    type2.fplx = (FPLX)12;
                    return type2;

                case "货物运输业增值税专用发票":
                    type2.dbfpzl = "f";
                    type2.TaxCardfpzl = (InvoiceType)11;
                    type2.fplx = (FPLX)11;
                    return type2;

                case "电子增值税普通发票":
                    type2.dbfpzl = "p";
                    type2.TaxCardfpzl = (InvoiceType)0x33;
                    type2.fplx = (FPLX)0x33;
                    return type2;

                case "增值税普通发票(卷票)":
                    type2.dbfpzl = "q";
                    type2.TaxCardfpzl = (InvoiceType)0x29;
                    type2.fplx = (FPLX)0x29;
                    return type2;
            }
            type2.dbfpzl = "s";
            type2.TaxCardfpzl = (InvoiceType)0;
            type2.fplx = 0;
            return type2;
        }

        private bool GetValueFPFZ()
        {
            try
            {
                if (this.editFPCX == FaPiaoChaXun.EditFPCX.FuZhi)
                {
                    if (this.aisinoGrid == null)
                    {
                        this._Object_FPFZ = null;
                        return false;
                    }
                    if (this.aisinoGrid.CurrentCell == null)
                    {
                        this._Object_FPFZ = null;
                        return false;
                    }
                    if (this.aisinoGrid.CurrentCell.OwningRow == null)
                    {
                        this._Object_FPFZ = null;
                        return false;
                    }
                    DataGridViewRow owningRow = this.aisinoGrid.CurrentCell.OwningRow;
                    if (owningRow == null)
                    {
                        this._Object_FPFZ = null;
                        return false;
                    }
                    _InvoiceType invoiceType = this.GetInvoiceType(owningRow.Cells["FPZL"].Value.ToString());
                    string str = Convert.ToString(owningRow.Cells["FPDM"].Value);
                    string str2 = ShareMethods.FPHMTo8Wei(Convert.ToInt32(owningRow.Cells["FPHM"].Value.ToString().Trim()));
                    this._Object_FPFZ = new object[] { invoiceType.dbfpzl.Trim(), str.Trim(), str2.Trim() };
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripSeparator1 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator1");
            this.toolStripSeparator2 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator2");
            this.tool_chazhao = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_chazhao");
            this.tool_daying = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_daying");
            this.tool_tongji = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tongji");
            this.tool_GeShi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_GeShi");
            this.tool_Exit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Exit");
            this.tool_ZuoFei = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_ZuoFei");
            this.tool_ChaKanMingXi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_ChaKanMingXi");
            this.toolTxt_quick = this.xmlComponentLoader1.GetControlByName<ToolStripTextBox>("toolTxt_quick");
            this.toolLabel_quick = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolLabel_quick");
            this.tool_FPZFBSZT = this.xmlComponentLoader1.GetControlByName<ToolStripSplitButton>("tool_FPZFBSZT");
            this.aisinoGrid = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid");
            this.aisinoGrid.ReadOnly = true;
            this.aisinoGrid.DataGrid.AllowUserToDeleteRows = false;
            this.aisinoGrid.DataGridRowDbClickEvent += aisinoGrid_DataGridRowDbClickEvent;
            this.aisinoGrid.GoToPageEvent += aisinoGrid_GoToPageEvent;
            this.aisinoGrid.DataGridRowClickEvent += aisinoGrid_DataGridRowClickEvent;
            this.aisinoGrid.DataGridRowSelectionChanged += aisinoGrid_DataGridRowSelectionChanged;
            this.tool_chazhao.Click += new EventHandler(this.btnChazhao_Click);
            this.tool_daying.Click += new EventHandler(this.btnDaYing_Click);
            this.tool_Exit.Click += new EventHandler(this.btnExitClick);
            this.tool_ZuoFei.Click += new EventHandler(this.but_zuofei_Click);
            this.tool_ChaKanMingXi.Click += new EventHandler(this.but_mingxi_Click);
            this.tool_tongji.Click += new EventHandler(this.but_Tongji_Click);
            this.tool_GeShi.Click += new EventHandler(this.but_GeShi_Click);
            this.tool_daying.Visible = true;
            this.tool_tongji.Visible = false;
            this.tool_Exit.Margin = new Padding(20, 1, 0, 2);
            this.Text = "已开发票作废";
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.toolTxt_quick.Alignment = ToolStripItemAlignment.Right;
            this.toolLabel_quick.Alignment = ToolStripItemAlignment.Right;
            this.tool_FPZFBSZT.Alignment = ToolStripItemAlignment.Right;
            this.toolTxt_quick.AutoToolTip = true;
            this.toolTxt_quick.ToolTipText = "请输入关键字(发票代码，发票号码，购方名称，购方税号),以回车键或者ESC键结束";
            this.toolTxt_quick.KeyUp += new KeyEventHandler(this.KeyUp_QuickSearch);
            this.toolTxt_quick.Paint += new PaintEventHandler(this.toolTxt_quick_Paint);
            this.toolTxt_quick.GotFocus += new EventHandler(this.toolTxt_quick_GotFocus);
            this.toolTxt_quick.LostFocus += new EventHandler(this.toolTxt_quick_LostFocus);
            this.toolTxt_quick.ForeColor = Color.Gray;
            this.toolTxt_quick.Font = new Font("微软雅黑", 9f, FontStyle.Italic);
            this.tool_FPZFBSZT = this.xmlComponentLoader1.GetControlByName<ToolStripSplitButton>("tool_FPZFBSZT");
            this.contextMenu_BSZT = new ContextMenuStrip();
            this.MenuItem_Yanqianshibai = new ToolStripMenuItem("验签失败");
            this.contextMenu_BSZT.Items.Add(this.MenuItem_Yanqianshibai);
            this.tool_FPZFBSZT.Click += new EventHandler(this.tool_FPZFBSZT_Click);
            this.MenuItem_Yanqianshibai.Click += new EventHandler(this.MenuItem_Yanqianshibai_Click);
            if (base.TaxCardInstance.QYLX.ISTDQY)
            {
                this.tool_FPZFBSZT.Visible = false;
            }
            base.FormClosing += new FormClosingEventHandler(this.FaPiaoZuoFei_YiKai_FormClosing);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x315, 0x1f2);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Text = "FaPiaoZuoFei_YiKai";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPZF.FaPiaoZuoFei_YiKai\Aisino.Fwkp.Fpkj.Form.FPZF.FaPiaoZuoFei_YiKai.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x315, 0x1f2);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "FaPiaoZuoFei_YiKai";
            this.Text = "FaPiaoZuoFei_YiKai";
            base.FormClosing += new FormClosingEventHandler(this.FaPiaoZuoFei_YiKai_FormClosing);
            base.ResumeLayout(false);
        }

        private void InsertColumn()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            string str = "50";
            string str2 = "66";
            string str3 = "78";
            string str4 = "90";
            string str5 = "102";
            string str6 = "125";
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
            if (base.TaxCardInstance.QYLX.ISTDQY)
            {
                item.Add("Visible", "False");
            }
            item.Add("HeadAlign", "MiddleCenter");
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
            item.Add("Property", "SLV");
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
            item.Add("Visible", "False");
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
            item.Add("AisinoLBL", "身份证号码/组织机构代码");
            item.Add("Property", "XSBM");
            item.Add("Width", str3);
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
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
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
            item.Add("AisinoLBL", "完税凭证号码");
            item.Add("Property", "WSPZHM");
            item.Add("Width", str3);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Visible", "False");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "蓝字代码号码");
            item.Add("Property", "LZDMHM");
            item.Add("Width", str3);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Visible", "False");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "凭证业务号");
            item.Add("Property", "PZYWH");
            item.Add("Width", str3);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Visible", "False");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "开票顺序号");
            item.Add("Property", "KPSXH");
            item.Add("Width", str3);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Visible", "False");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "地址索引号");
            item.Add("Property", "DZSYH");
            item.Add("Width", str3);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Visible", "False");
            item.Add("HeadAlign", "MiddleCenter");
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
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "商品税目编码");
            item.Add("Property", "SPSM_BM");
            item.Add("Width", str6);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            this.aisinoGrid.ColumeHead = list;
            DataGridViewColumn column = this.aisinoGrid.Columns["KPRQ"];
            if (column != null)
            {
                DataGridViewCellStyle defaultCellStyle = column.DefaultCellStyle;
                defaultCellStyle.ForeColor = Color.Maroon;
                defaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            }
            DataGridViewColumn column2 = this.aisinoGrid.Columns["DYBZ"];
            if (column2 != null)
            {
                column2.DefaultCellStyle.ForeColor = Color.Maroon;
            }
            DataGridViewColumn column3 = this.aisinoGrid.Columns["ZFBZ"];
            if (column3 != null)
            {
                column3.DefaultCellStyle.ForeColor = Color.Maroon;
            }
            DataGridViewColumn column4 = this.aisinoGrid.Columns["WKBZ"];
            if (column4 != null)
            {
                column4.DefaultCellStyle.ForeColor = Color.Maroon;
            }
            DataGridViewColumn column5 = this.aisinoGrid.Columns["XFBZ"];
            if (column5 != null)
            {
                column5.DefaultCellStyle.ForeColor = Color.Maroon;
            }
            DataGridViewColumn column6 = this.aisinoGrid.Columns["QDBZ"];
            if (column6 != null)
            {
                column6.DefaultCellStyle.ForeColor = Color.Maroon;
            }
            DataGridViewColumn column7 = this.aisinoGrid.Columns["FPHM"];
            if (column7 != null)
            {
                column7.DefaultCellStyle.Format = new string('0', 8);
            }
            DataGridViewColumn column8 = this.aisinoGrid.Columns["HJJE"];
            if (column8 != null)
            {
                column8.DefaultCellStyle.Format = "0.00";
            }
            DataGridViewColumn column9 = this.aisinoGrid.Columns["SLv"];
            if (column9 != null)
            {
                column9.DefaultCellStyle.Format = "0.00";
            }
            DataGridViewColumn column10 = this.aisinoGrid.Columns["HJSE"];
            if (column10 != null)
            {
                column10.DefaultCellStyle.Format = "0.00";
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
                new Aisino.Fwkp.Fpkj.Common.Tool();
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

        private bool IsEmptyXXFP()
        {
            try
            {
                if (this.aisinoGrid.SelectedRows == null)
                {
                    return true;
                }
                if (0 >= this.aisinoGrid.SelectedRows.Count)
                {
                    return true;
                }
                int num = 0;
                foreach (DataGridViewRow row in this.aisinoGrid.SelectedRows)
                {
                    string str = new string('0', 15);
                    string str2 = "否";
                    str = ShareMethods.CellToString(row.Cells["GFMC"]);
                    str2 = ShareMethods.CellToString(row.Cells["ZFBZ"]);
                    if (!str.Equals(new string('0', 15)) && str2.Equals("否"))
                    {
                        num++;
                    }
                }
                if (0 >= num)
                {
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

        private void KeyUp_QuickSearch(object sender, KeyEventArgs e)
        {
            try
            {
                if (((e.KeyValue == 13) || (e.KeyValue == 0x1b)) || (e.KeyValue == 13))
                {
                    this.aisinoGrid.Focus();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[toolTxt_quick_TextChanged函数异常]" + exception.ToString());
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
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void MenuItem_Yanqianshibai_Click(object sender, EventArgs e)
        {
            try
            {
                this.MenuItem_Yanqianshibai.Checked = !this.MenuItem_Yanqianshibai.Checked;
                this.sqlType = 1;
                this.ChaXunDate(1, this.AvePageNum, 2, -1, this.sqlType);
                this.SetBtnEnabled();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        public bool QuitForm_IsEmpty()
        {
            try
            {
                if ((this.aisinoGrid.DataSource == null) || (0 >= this.aisinoGrid.DataSource.AllRows))
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

        public int SaveToDB()
        {
            if ((this.WasteFpCondition == null) || (this.WasteFpCondition.Count <= 0))
            {
                return 0;
            }
            try
            {
                UpLoadCheckState.SetFpxfState(true);
                this.loger.Error("开始作废写入数据库");
                this.Dal.ZuoFei(this.WasteFpCondition);
                this.loger.Error("结束作废写入数据库");
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
            finally
            {
                UpLoadCheckState.SetFpxfState(false);
            }
            return 1;
        }

        public void SetBtnEnabled()
        {
            try
            {
                if (this.editFPCX == FaPiaoChaXun.EditFPCX.FuZhi)
                {
                    this.tool_chazhao.Visible = false;
                    this.tool_ZuoFei.Visible = false;
                    this.tool_ChaKanMingXi.Visible = false;
                    this.toolStripSeparator2.Visible = false;
                }
                bool flag = true;
                bool flag2 = true;
                if (this.aisinoGrid.Rows.Count <= 0)
                {
                    flag = false;
                    flag2 = false;
                }
                else
                {
                    DataGridViewSelectedRowCollection rows = this.aisinoGrid.SelectedRows;
                    if (rows.Count >= 1)
                    {
                        flag = true;
                        foreach (DataGridViewRow row in rows)
                        {
                            if (row.Cells["ZFBZ"].Value.ToString() == "否")
                            {
                                flag = true;
                                break;
                            }
                        }
                        flag2 = true;
                    }
                    else
                    {
                        flag = false;
                        flag2 = false;
                    }
                }
                this.tool_daying.Enabled = true;
                this.tool_ChaKanMingXi.Enabled = flag2;
                this.tool_ZuoFei.Enabled = flag;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void sm_MonthChange(object sender, MonthChangeEventArgs e)
        {
            try
            {
                if (this.editFPCX != FaPiaoChaXun.EditFPCX.FuZhi)
                {
                    this.sqlType = 0;
                    this.ChaXunDate(1, this.AvePageNum, 2, -1, 0);
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

        private Fpxx TaxCardZuoFei(Fpxx zffp, bool IsFromGridView = false)
        {
            byte[] sourceArray = Invoice.TypeByte;
            byte[] destinationArray = new byte[0x20];
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer3 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
            byte[] buffer4 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer3);
            if (IsFromGridView)
            {
                zffp.bz = Aisino.Fwkp.Fpkj.Common.Tool.ToBase64(zffp.bz);
                zffp.yshwxx = Aisino.Fwkp.Fpkj.Common.Tool.ToBase64(zffp.yshwxx);
            }
            string kprq = zffp.kprq;
            string dzsyh = zffp.dzsyh;
            Invoice invoice = null;
            if (zffp.fplx == FPLX.JSFP)
            {
                invoice = new Invoice(false, zffp, buffer4, zffp.dy_mb);
            }
            else
            {
                invoice = new Invoice(false, zffp, buffer4, "NEW76mmX177mm");
            }
            invoice.Hjje = zffp.je;
            invoice.Hjse = zffp.se;
            string str3 = "Aisino.Fwkp.Invoice" + invoice.Fpdm + invoice.Fphm;
            byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str3));
            byte[] buffer6 = new byte[0x20];
            Array.Copy(bytes, 0, buffer6, 0, 0x20);
            byte[] buffer7 = new byte[0x10];
            Array.Copy(bytes, 0x20, buffer7, 0, 0x10);
            byte[] inArray = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(DateTime.Now.ToString("F")), buffer6, buffer7);
            zffp.gfmc = Convert.ToBase64String(AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(Convert.ToBase64String(inArray) + (";" + invoice.Gfmc)), buffer6, buffer7));
            zffp.zfbz = true;
            zffp.bszt = 0;
            zffp.kprq = kprq;
            zffp.dzsyh = dzsyh;
            zffp.bz = Aisino.Fwkp.Fpkj.Common.Tool.FromBase64(zffp.bz);
            zffp.yshwxx = Aisino.Fwkp.Fpkj.Common.Tool.FromBase64(zffp.yshwxx);
            this.loger.Info("开始调用框架作废接口");
            if (invoice.MakeCardInvoice(zffp, true))
            {
                this.loger.Error("结束调用框架作废接口，作废成功");
                return zffp;
            }
            this.loger.Error("结束调用框架作废接口,作废失败");
            zffp.zfbz = false;
            zffp.zfsj = "";
            return zffp;
        }

        private void tool_FPZFBSZT_Click(object sender, EventArgs e)
        {
            try
            {
                Rectangle bounds = this.tool_FPZFBSZT.Bounds;
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
            this.toolTxt_QuickSearch(sender, e);
        }

        private void toolTxt_quick_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                this.DrawBorder(sender, e.Graphics, SystemColor.GRID_ALTROW_BACKCOLOR, Color.FromArgb(0, 0xbb, 0xff), this.toolTxt_quick.Width, this.toolTxt_quick.Height);
            }
            catch (Exception exception)
            {
                this.loger.Error("[FaPiaoChaXun_FPFZ_Paint函数异常]" + exception.ToString());
            }
        }

        private void toolTxt_QuickSearch(object sender, EventArgs e)
        {
            try
            {
                this.sqlType = 0;
                this.ChaXunDate(1, this.AvePageNum, 2, -1, this.sqlType);
                this.SetBtnEnabled();
            }
            catch (Exception exception)
            {
                this.loger.Error("[toolTxt_quick_TextChanged函数异常]" + exception.ToString());
            }
        }

        public string YiKaiZuoFeiMainFunction(string fpzl, string fpdm, string fphm, List<DaiKaiXml.SWDKDMHM> swdkZyList, List<DaiKaiXml.SWDKDMHM> swdkPtList, int type = 0)
        {
            string str = fpdm + "_" + ShareMethods.FPHMTo8Wei(fphm);
            this.condition.Clear();
            this.condition.Add("LZDMHM", str);
            this.condition.Add("FPZL", fpzl);
            Fpxx zffp = null;
            bool isFromGridView = false;
            if (type == 0)
            {
                zffp = this.Dal.GetXxfp(this.gridviewRowDict);
                if ((this.gridviewRowDict == null) || (this.gridviewRowDict.Count == 0))
                {
                    isFromGridView = false;
                    this.loger.Info("作废时读取DB发票对象");
                    zffp = this.Dal.GetModel(fpzl, fpdm, Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(fphm), "");
                    this.loger.Info("结束读取DB发票对象");
                }
                else
                {
                    isFromGridView = true;
                }
            }
            else
            {
                this.loger.Info("作废时读取DB发票对象");
                zffp = this.Dal.GetModel(fpzl, fpdm, Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(fphm), "");
                this.loger.Info("结束读取DB发票对象");
            }
            if (zffp.zfbz)
            {
                this.loger.Debug("重复作废发票");
                return "0000";
            }
            if (zffp == null)
            {
                this.loger.Error("本张发票在数据库中未找到对应记录");
                return "FPZF-000015";
            }
            if ((Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(zffp.je) > 0.0) && (this.Dal.IsExistHZFP(this.condition) != 0))
            {
                if (type != 2)
                {
                    MessageManager.ShowMsgBox("FPZF-000003");
                }
                this.loger.Error("已开发票的对应的红字发票存在，不能作废!");
                return "FPZF-000003";
            }
            Fpxx fpxx2 = this.TaxCardZuoFei(zffp, isFromGridView);
            if (fpxx2 == null)
            {
                this.loger.Error("金税设备作废发票，返回对象为空");
                return fpxx2.retCode;
            }
            if (fpxx2.zfbz)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("FPZL", fpzl);
                item.Add("FPDM", fpdm);
                item.Add("FPHM", Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(fphm));
                item.Add("ZFBZ", "true");
                item.Add("ZFRQ", fpxx2.zfsj);
                item.Add("SIGN", (fpxx2.sign == null) ? "" : fpxx2.sign);
                item.Add("DZSYH", fpxx2.dzsyh);
                this.WasteFpCondition.Add(item);
                if (Aisino.Fwkp.Fpkj.Common.Tool.IsShuiWuDKSQ())
                {
                    DaiKaiXml.SWDKDMHM swdkdmhm = new DaiKaiXml.SWDKDMHM {
                        fpdm = fpdm,
                        fphm = fphm.ToString()
                    };
                    if ((swdkZyList != null) && (fpzl == "s"))
                    {
                        swdkZyList.Add(swdkdmhm);
                    }
                    if ((swdkPtList != null) && (fpzl == "c"))
                    {
                        swdkPtList.Add(swdkdmhm);
                    }
                }
                if (type != 1)
                {
                    try
                    {
                        object[] objArray = new object[] { fpxx2.fpdm, fpxx2.fphm, fpxx2.xsdjbh, type };
                        ServiceFactory.InvokePubService("Aisino.Fwkp.Wbjk.FPZFSuccess", objArray);
                    }
                    catch (Exception exception)
                    {
                        this.loger.Debug(exception.ToString());
                    }
                }
                return "0000";
            }
            string str2 = ShareMethods.FPHMTo8Wei(fphm);
            if (type != 2)
            {
                if (Aisino.Fwkp.Fpkj.Common.Tool.GetReturnErrCode(fpxx2.retCode) == 0x2c2)
                {
                    MessageManager.ShowMsgBox(fpxx2.retCode);
                    return "0001";
                }
                MessageManager.ShowMsgBox(fpxx2.retCode);
            }
            this.loger.Error("发票代码：" + fpxx2.fpdm + "号码：" + str2 + "的发票作废失败原因：写金税设备时失败，错误码：" + fpxx2.retCode);
            return fpxx2.retCode;
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

        public object[] OBJECT_FPFZ
        {
            get
            {
                return this._Object_FPFZ;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct _InvoiceType
        {
            public string dbfpzl;
            public InvoiceType TaxCardfpzl;
            public FPLX fplx;
        }
    }
}

