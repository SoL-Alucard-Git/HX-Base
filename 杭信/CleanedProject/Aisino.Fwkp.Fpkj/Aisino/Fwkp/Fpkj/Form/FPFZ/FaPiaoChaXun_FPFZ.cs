namespace Aisino.Fwkp.Fpkj.Form.FPFZ
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.DAL;
    using Aisino.Fwkp.Fpkj.Form.Dy;
    using Aisino.Fwkp.Fpkj.Form.FPCX;
    using Aisino.Fwkp.Print;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class FaPiaoChaXun_FPFZ : DockForm
    {
        private int _iPageNO;
        private int _iPageSize;
        private object[] _Object_FPFZ = new object[] { "", "", "" };
        private string _QuickSearchText = "请输入检索关键字...";
        private string _strFilePath = string.Empty;
        private XMLOperation _XmlOperation = new XMLOperation();
        private DateTime _YueFenDate = DingYiZhiFuChuan1.dataTimeCCRQ;
        private AisinoDataGrid aisinoGrid;
        public int AvePageNum = 10;
        public static DateTime CardClock;
        private ChaXunTiaoJian chaXunTiaoJian;
        private IContainer components;
        private ContextMenuStrip contextMenu_DaYin;
        private FaPiaoChaXun.EditFPCX editFPCX = FaPiaoChaXun.EditFPCX.FuZhi;
        private ILog loger = LogUtil.GetLogger<FaPiaoChaXun_FPFZ>();
        public int Month;
        private ToolStripButton tool_ChaKanMingXi;
        private ToolStripButton tool_chazhao;
        private ToolStripButton tool_daying;
        private ToolStripButton tool_Exit;
        private ToolStripButton tool_GeShi;
        private ToolStripButton tool_tongji;
        private ToolStripButton tool_xuanzhe;
        private ToolStripLabel toolLabel_quick;
        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripTextBox toolTxt_quick;
        private XmlComponentLoader xmlComponentLoader1;
        private XXFP xxfpChaXunBll = new XXFP(false);
        public int Year = 0x7d0;

        public FaPiaoChaXun_FPFZ()
        {
            try
            {
                base.MinimizeBox = false;
                if (CardClock.Year <= DingYiZhiFuChuan1.dataTimeCCRQ.Year)
                {
                    CardClock = base.TaxCardInstance.GetCardClock();
                }
                this._YueFenDate = CardClock;
                this.Initialize();
                this.Text = "发票复制";
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

        private void aisinoGrid_DataGridCellEndEditEvent(object sender, DataGridRowEventArgs e)
        {
        }

        private void aisinoGrid_DataGridCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
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
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.ToString());
                MessageManager.ShowMsgBox(exception2.ToString());
            }
        }

        private void aisinoGrid_DataGridRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
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
                PropertyUtil.SetValue("Aisino.Fwkp.Fpkj.Form.FPFZ_FaPiaoChaXun_FPFZ_aisinoGrid_PageSize", e.PageSize.ToString());
                this.ChaXunDate(e.PageNO, e.PageSize, 1);
                this.SetBtnEnabled();
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.ToString());
                MessageManager.ShowMsgBox(exception2.ToString());
            }
        }

        private void btnChazhao_Click(object sender, EventArgs e)
        {
            try
            {
                this.aisinoGrid.Select(this);
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
                    if (this.editFPCX == FaPiaoChaXun.EditFPCX.ChaXun)
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

        private void btnXuanzhe_Click(object sender, EventArgs e)
        {
            try
            {
                if (((this.aisinoGrid.SelectedRows.Count >= 1) && (this.editFPCX == FaPiaoChaXun.EditFPCX.FuZhi)) && this.GetValueFPFZ())
                {
                    base.Close();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void but_back_Click(object sender, EventArgs e)
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

        private void but_up_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBoxHelper.Show("上一步按钮已经取消。");
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
                List<int> list2;
                string str6;
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
                    list2 = new List<int>();
                    list2.Clear();
                    foreach (DataGridViewRow row in (IEnumerable) rowsAll)
                    {
                        string str2 = Convert.ToString(row.Cells["FPDM"].Value);
                        string s = Convert.ToString(row.Cells["FPHM"].Value);
                        string str4 = Convert.ToString(row.Cells["ZFBZ"].Value);
                        int rowIndex = row.Cells[0].RowIndex;
                        int result = 0;
                        int.TryParse(s, out result);
                        s = Convert.ToString(result);
                        string invoiceType = this.GetInvoiceType(row.Cells["FPZL"].Value.ToString());
                        str4 = (str4.Trim() == "是") ? "1" : "0";
                        string[] item = new string[] { invoiceType, str2.ToString(), s.ToString(), str4, Convert.ToString(rowIndex) };
                        if (rowIndex == this.aisinoGrid.CurrentCell.RowIndex)
                        {
                            str = Convert.ToString((int) (this.aisinoGrid.CurrentCell.RowIndex - num));
                        }
                        if (!this.IsEmptyFPCurrentData(row.Cells["GFSH"], invoiceType))
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
                    if (list.Count <= 0)
                    {
                        MessageManager.ShowMsgBox("FPCX-000003");
                        return false;
                    }
                    str6 = "0";
                    if (this.editFPCX == FaPiaoChaXun.EditFPCX.ChaXun)
                    {
                        str6 = "0";
                        goto Label_0296;
                    }
                    if (FaPiaoChaXun.EditFPCX.ZuoFei == this.editFPCX)
                    {
                        str6 = "1";
                        goto Label_0296;
                    }
                    MessageManager.ShowMsgBox("FPCX-000027");
                }
                return false;
            Label_0296:;
                object[] objArray = new object[] { str6, str, list };
                object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray);
                if (this.editFPCX == FaPiaoChaXun.EditFPCX.FuZhi)
                {
                    return false;
                }
                if (objArray2 == null)
                {
                    MessageManager.ShowMsgBox("FPCX-000028");
                    return false;
                }
                if (objArray2.Length < 2)
                {
                    MessageManager.ShowMsgBox("FPCX-000028");
                    return false;
                }
                int num4 = (int) objArray2[0];
                if (this.editFPCX == FaPiaoChaXun.EditFPCX.ChaXun)
                {
                    for (int i = 0; i < list2.Count; i++)
                    {
                        if (list2[i] == num4)
                        {
                            num4 = i;
                            break;
                        }
                    }
                    this.aisinoGrid.SetSelectRows(num4);
                }
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
                        int num6 = Convert.ToUInt16(strArray2[4].Trim());
                        if ("1" == strArray2[3].Trim())
                        {
                            this.aisinoGrid.SetSelectRows(num6);
                        }
                    }
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

        public void ChaXunDate(int page, int num, int TiaojianChaXun)
        {
            try
            {
                int num2;
                string str3;
                AisinoDataSet set = null;
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Clear();
                this._iPageNO = page;
                this._iPageSize = num;
                if (this.editFPCX != FaPiaoChaXun.EditFPCX.FuZhi)
                {
                    return;
                }
                string strB = this.toolTxt_quick.Text.Trim(new char[] { ' ', ' ' });
                if (this._QuickSearchText.CompareTo(strB) == 0)
                {
                    strB = "";
                }
                dict.Add("FPDM", "%" + strB + "%");
                dict.Add("GFMC", "%" + strB + "%");
                dict.Add("GFSH", "%" + strB + "%");
                dict.Add("Fpzl", Aisino.Fwkp.Fpkj.Common.Tool.FPZL);
                if (Aisino.Fwkp.Fpkj.Common.Tool.CharNumInString(strB, '0') != strB.Length)
                {
                    strB = strB.TrimStart(new char[] { '0' });
                }
                dict.Add("FPHM", "%" + strB + "%");
                if (((str3 = Aisino.Fwkp.Fpkj.Common.Tool.FPZL) != null) && (str3 == "c"))
                {
                    switch ((int)Aisino.Fwkp.Fpkj.Common.Tool.ZYFPZL)
                    {
                        case 8:
                            dict.Add("YYSBZ1", "_____1____");
                            dict.Add("YYSBZ2", "_____1____");
                            goto Label_01C4;

                        case 9:
                            dict.Add("YYSBZ1", "_____2____");
                            dict.Add("YYSBZ2", "_____2____");
                            goto Label_01C4;
                    }
                    dict.Add("YYSBZ1", "_____0____");
                    dict.Add("YYSBZ2", "_____1____");
                }
                else
                {
                    dict.Add("YYSBZ1", "%%");
                    dict.Add("YYSBZ2", "%%");
                }
            Label_01C4:
                num2 = 1;
                switch (PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXunTiaoJian_SortWay"))
                {
                    case "1":
                    case "":
                        num2 = 1;
                        break;

                    default:
                        num2 = 0;
                        break;
                }
                set = this.xxfpChaXunBll.SelectPage(page, num, TiaojianChaXun, dict, num2, this._YueFenDate, -1, 0);
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
                base.Hide();
                this.editFPCX = edit;
                if (edit == FaPiaoChaXun.EditFPCX.FuZhi)
                {
                    this.Month = CardClock.Month;
                    this.Year = CardClock.Year;
                    string data = PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.FPFZ_FaPiaoChaXun_FPFZ_aisinoGrid_PageSize");
                    this.AvePageNum = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(data);
                    if (this.AvePageNum <= 0)
                    {
                        this.AvePageNum = 30;
                    }
                    this.ChaXunDate(1, this.AvePageNum, 1);
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

        private void FaPiaoChaXun_FPFZ_FormClosing(object sender, EventArgs e)
        {
            this._XmlOperation = null;
            this.xxfpChaXunBll = null;
            this.aisinoGrid = null;
            base.Dispose();
        }

        private void FaPiaoChaXun_FPFZ_Paint(object sender, PaintEventArgs e)
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

        private void FaPiaoZuoFei_YiKai_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FaPiaoZuoFei_YiKai_Load(object sender, EventArgs e)
        {
        }

        private bool FpDaYin(string strFpzl, string strFpDm, int iFphm, bool IsLastFp, bool IsChangeDateBase, Dictionary<string, object> dict)
        {
            try
            {
                bool flag = true;
                string str = string.Format(DingYiZhiFuChuan1.strFphmFormat, iFphm);
                if (dict == null)
                {
                    return false;
                }
                DyQueRen ren = new DyQueRen();
                if (!ren.setValue(dict))
                {
                    return false;
                }
                if (DialogResult.OK != ren.ShowDialog())
                {
                    return false;
                }
                FPPrint.Create(this.GetInvoiceType(strFpzl), strFpDm, iFphm, true).Print(true);
                string str3 = string.Empty;
                if (flag)
                {
                    str3 = DingYiZhiFuChuan1.strDaYinDefault[0];
                }
                else
                {
                    str3 = DingYiZhiFuChuan1.strDaYinDefault[1];
                }
                if (IsLastFp)
                {
                    MessageManager.ShowMsgBox("FPCX-000034", "提示", new string[] { str, str3 });
                }
                else
                {
                    return (DialogResult.Yes == MessageManager.ShowMsgBox("FPCX-000035", "提示", new string[] { str, str3 }));
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

        private string GetInvoiceType(string type)
        {
            switch (type)
            {
                case "普通发票":
                case "农产品销售发票":
                case "收购发票":
                    return "c";

                case "专用发票":
                    return "s";

                case "机动车销售统一发票":
                    return "j";

                case "货物运输业增值税专用发票":
                    return "f";

                case "电子增值税普通发票":
                    return "p";

                case "增值税普通发票(卷票)":
                    return "q";
            }
            return "s";
        }

        private void GetQyxx(out string strKhmc, string strKhsh, string strKmdzdh, string strKhyhzh, out string strEmail, out string strKhshOut)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            try
            {
                object[] objArray = new object[] { strKhsh.Trim(), 1 };
                object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKHbySH", objArray);
                strKhmc = string.Empty;
                if (objArray2 == null)
                {
                    str = string.Empty;
                    strKhsh = string.Empty;
                    strKmdzdh = string.Empty;
                    strKhyhzh = string.Empty;
                    str2 = string.Empty;
                }
                if (objArray2.Length > 4)
                {
                    str = objArray2[0].ToString();
                    strKhsh = objArray2[1].ToString();
                    strKmdzdh = objArray2[2].ToString();
                    strKhyhzh = objArray2[3].ToString();
                    str2 = objArray2[4].ToString();
                }
                else
                {
                    str = string.Empty;
                    strKhsh = string.Empty;
                    strKmdzdh = string.Empty;
                    strKhyhzh = string.Empty;
                    str2 = string.Empty;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
            strKhmc = str;
            strEmail = str2;
            strKhshOut = strKhsh;
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
                    bool flag = TaxCardFactory.CreateTaxCard().StateInfo.CompanyType != 0;
                    bool flag2 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToBool(owningRow.Cells["QDBZ"].Value);
                    string invoiceType = this.GetInvoiceType(owningRow.Cells["FPZL"].Value.ToString());
                    if ((flag && (invoiceType == "s")) && flag2)
                    {
                        this._Object_FPFZ = null;
                        MessageManager.ShowMsgBox("FPCX-000033");
                        return false;
                    }
                    string str2 = Convert.ToString(owningRow.Cells["FPDM"].Value);
                    string str3 = ShareMethods.FPHMTo8Wei(Convert.ToInt32(owningRow.Cells["FPHM"].Value.ToString().Trim()));
                    this._Object_FPFZ = new object[] { invoiceType.Trim(), str2.Trim(), str3.Trim() };
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
            this.tool_xuanzhe = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_xuanzhe");
            this.tool_chazhao = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_chazhao");
            this.tool_daying = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_daying");
            this.tool_tongji = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tongji");
            this.tool_GeShi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_GeShi");
            this.tool_Exit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Exit");
            this.contextMenu_DaYin = new ContextMenuStrip();
            this.tool_ChaKanMingXi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_ChaKanMingXi");
            this.toolLabel_quick = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolLabel_quick");
            this.toolTxt_quick = this.xmlComponentLoader1.GetControlByName<ToolStripTextBox>("toolTxt_quick");
            this.aisinoGrid = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid");
            this.aisinoGrid.ReadOnly = true;
            this.aisinoGrid.DataGrid.AllowUserToDeleteRows = false;
            this.aisinoGrid.DataGridRowPostPaint += aisinoGrid_DataGridRowPostPaint;
            this.aisinoGrid.DataGridCellPainting += aisinoGrid_DataGridCellPainting;
            this.aisinoGrid.DataGridCellEndEditEvent += aisinoGrid_DataGridCellEndEditEvent;
            this.aisinoGrid.DataGridRowDbClickEvent += aisinoGrid_DataGridRowDbClickEvent;
            this.aisinoGrid.GoToPageEvent += aisinoGrid_GoToPageEvent;
            this.aisinoGrid.DataGridRowClickEvent += aisinoGrid_DataGridRowClickEvent;
            this.aisinoGrid.DataGridRowSelectionChanged += aisinoGrid_DataGridRowSelectionChanged;
            this.tool_xuanzhe.Click += new EventHandler(this.btnXuanzhe_Click);
            this.tool_chazhao.Click += new EventHandler(this.btnChazhao_Click);
            this.tool_daying.Click += new EventHandler(this.btnDaYing_Click);
            this.tool_Exit.Click += new EventHandler(this.btnExitClick);
            this.tool_tongji.Click += new EventHandler(this.but_Tongji_Click);
            this.tool_GeShi.Click += new EventHandler(this.but_GeShi_Click);
            this.toolTxt_quick.Paint += new PaintEventHandler(this.FaPiaoChaXun_FPFZ_Paint);
            this.tool_ChaKanMingXi.Visible = false;
            this.tool_daying.Visible = false;
            this.tool_tongji.Visible = false;
            this.toolLabel_quick.Alignment = ToolStripItemAlignment.Right;
            this.toolTxt_quick.Alignment = ToolStripItemAlignment.Right;
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.tool_Exit.Margin = new Padding(20, 1, 0, 2);
            this.toolTxt_quick.AutoToolTip = true;
            this.toolTxt_quick.ToolTipText = "请输入关键字(发票代码，发票号码，购方名称，购方税号),以回车键或者ESC键结束";
            this.toolTxt_quick.GotFocus += new EventHandler(this.toolTxt_quick_GotFocus);
            this.toolTxt_quick.LostFocus += new EventHandler(this.toolTxt_quick_LostFocus);
            this.toolTxt_quick.KeyUp += new KeyEventHandler(this.KeyUp_QuickSearch);
            this.toolTxt_quick.ForeColor = Color.Gray;
            this.toolTxt_quick.Font = new Font("微软雅黑", 9f, FontStyle.Italic);
            base.FormClosing += new FormClosingEventHandler(this.FaPiaoChaXun_FPFZ_FormClosing);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x30d, 0x1aa);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPFZ.FaPiaoChaXun_FPFZ\Aisino.Fwkp.Fpkj.Form.FPFZ.FaPiaoChaXun_FPFZ.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x30d, 0x1aa);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "FaPiaoChaXun_FPFZ";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "发票复制";
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
            item.Add("AisinoLBL", "报送状态");
            item.Add("Property", "BSZT");
            item.Add("Width", str3);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
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
            item.Add("AisinoLBL", "开票日期");
            item.Add("Property", "KPRQ");
            item.Add("Width", str6);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
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
            item.Add("Visible", "False");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "作废日期");
            item.Add("Property", "ZFRQ");
            item.Add("Width", str3);
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
                if (string.IsNullOrEmpty(fpzl))
                {
                    fpzl = this.GetInvoiceType(CurrentCell.OwningRow.Cells["FPZL"].Value.ToString().Trim());
                }
                string str = CurrentCell.OwningRow.Cells["GFSH"].Value.ToString().Trim();
                if (fpzl.Equals("c"))
                {
                    if (((new string('0', 15) == str) || (new string('0', 0x12) == str)) || (new string('0', 20) == str))
                    {
                        return true;
                    }
                }
                else if (fpzl.Equals("s"))
                {
                    if (((string.Empty == str) || (new string('0', 15) == str)) || ((new string('0', 0x12) == str) || (new string('0', 20) == str)))
                    {
                        return true;
                    }
                }
                else if (fpzl.Equals("j"))
                {
                    if (((new string('0', 15) == str) || (new string('0', 0x12) == str)) || (new string('0', 20) == str))
                    {
                        return true;
                    }
                }
                else if (fpzl.Equals("f"))
                {
                    if (((string.Empty == str) || (new string('0', 15) == str)) || ((new string('0', 0x12) == str) || (new string('0', 20) == str)))
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
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

        private void MenuItem_FP_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoGrid.Rows.Count > 0)
                {
                    DataGridViewSelectedRowCollection rows2 = this.aisinoGrid.SelectedRows;
                    if (rows2.Count <= 0)
                    {
                        MessageManager.ShowMsgBox("FPZF-000001");
                    }
                    else
                    {
                        DyPrompt prompt = new DyPrompt();
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        bool flag = false;
                        for (int i = 0; i < rows2.Count; i++)
                        {
                            string str = Convert.ToString(rows2[i].Cells["DYBZ"].Value);
                            string str2 = Convert.ToString(rows2[i].Cells["FPZL"].Value);
                            string s = Convert.ToString(rows2[i].Cells["FPHM"].Value);
                            string str4 = Convert.ToString(rows2[i].Cells["FPDM"].Value);
                            int num2 = 0;
                            if (!int.TryParse(s, out num2) || (num2 <= 0))
                            {
                                MessageManager.ShowMsgBox("FPCX-000023");
                                return;
                            }
                            s = string.Format(DingYiZhiFuChuan1.strFphmFormat, num2);
                            dict.Clear();
                            dict.Add("lbl_Fpzl", str2);
                            dict.Add("lbl_Fphm", s);
                            dict.Add("lbl_Fpdm", str4);
                            bool isLastFp = i >= (rows2.Count - 1);
                            if (rows2.Count == 1)
                            {
                                if (str == "否")
                                {
                                    if (this.FpDaYin(str2, str4, num2, isLastFp, true, dict))
                                    {
                                    }
                                }
                                else if (((DialogResult.OK == MessageManager.ShowMsgBox("FPCX-000024")) && (str == "是")) && !this.FpDaYin(str2, str4, num2, isLastFp, false, dict))
                                {
                                }
                                return;
                            }
                            if (flag)
                            {
                                if (this.FpDaYin(str2, str4, num2, isLastFp, true, dict))
                                {
                                    continue;
                                }
                                return;
                            }
                            if (str == "是")
                            {
                                if (prompt.setValue(dict))
                                {
                                    DialogResult result = prompt.ShowDialog();
                                    if (DialogResult.Abort == result)
                                    {
                                        continue;
                                    }
                                    if (DialogResult.OK == result)
                                    {
                                        flag = true;
                                        if (this.FpDaYin(str2, str4, num2, isLastFp, true, dict))
                                        {
                                            continue;
                                        }
                                    }
                                    else if (DialogResult.Yes == result)
                                    {
                                        if (this.FpDaYin(str2, str4, num2, isLastFp, true, dict))
                                        {
                                            continue;
                                        }
                                    }
                                    else if (DialogResult.Cancel != result)
                                    {
                                        continue;
                                    }
                                }
                                return;
                            }
                            if (!this.FpDaYin(str2, str4, num2, isLastFp, true, dict))
                            {
                                return;
                            }
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

        private void MenuItem_FPLBClick(object sender, EventArgs e)
        {
            try
            {
                this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid").Print("选择发票号码查询", this, null, null, true);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void MenuItem_XHQD_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoGrid.Rows.Count > 0)
                {
                    DataGridViewSelectedRowCollection rows2 = this.aisinoGrid.SelectedRows;
                    if (rows2.Count <= 0)
                    {
                        MessageManager.ShowMsgBox("FPZF-000001");
                    }
                    else
                    {
                        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                        for (int i = 0; i < rows2.Count; i++)
                        {
                            string str = Convert.ToString(rows2[i].Cells["DYBZ"].Value);
                            string str2 = Convert.ToString(rows2[i].Cells["FPZL"].Value);
                            string str3 = Convert.ToString(rows2[i].Cells["FPHM"].Value);
                            string str4 = Convert.ToString(rows2[i].Cells["FPDM"].Value);
                            string str5 = Convert.ToString(rows2[i].Cells["QDBZ"].Value);
                            if ("是" == str5)
                            {
                                Dictionary<string, object> item = new Dictionary<string, object>();
                                item.Clear();
                                item.Add("DYBZ", str);
                                item.Add("FPZL", str2);
                                item.Add("FPHM", str3);
                                item.Add("FPDM", str4);
                                item.Add("QDBZ", str5);
                                list.Add(item);
                            }
                        }
                        if (0 >= list.Count)
                        {
                            MessageManager.ShowMsgBox("FPCX-000004");
                        }
                        else
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                Dictionary<string, object> dictionary2 = list[j];
                                string text1 = (string) dictionary2["DYBZ"];
                                string strFpzl = (string) dictionary2["FPZL"];
                                string s = (string) dictionary2["FPHM"];
                                string strFpDm = (string) dictionary2["FPDM"];
                                string str9 = (string) dictionary2["QDBZ"];
                                int result = 0;
                                if (!int.TryParse(s, out result) || (result <= 0))
                                {
                                    MessageManager.ShowMsgBox("FPCX-000023");
                                    return;
                                }
                                bool isLastFp = j >= (list.Count - 1);
                                if ((str9 == "是") && !this.XHQDDaYin(strFpzl, strFpDm, result, isLastFp))
                                {
                                    return;
                                }
                            }
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

        public bool QuitForm_IsEmpty()
        {
            try
            {
                if (this.aisinoGrid.DataSource == null)
                {
                    base.Hide();
                    MessageManager.ShowMsgBox("FPCX-000001");
                    base.Close();
                    return true;
                }
                if (0 >= this.aisinoGrid.DataSource.AllRows)
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

        public void SetBtnEnabled()
        {
            try
            {
                if (this.editFPCX == FaPiaoChaXun.EditFPCX.FuZhi)
                {
                    this.toolStripSeparator2.Visible = false;
                }
                bool flag = true;
                if (this.aisinoGrid.Rows.Count <= 0)
                {
                    flag = false;
                }
                else
                {
                    //this.aisinoGrid.SelectedRows;
                    if (this.aisinoGrid.SelectedRows.Count > 1)
                    {
                        flag = true;
                    }
                    else if (this.aisinoGrid.SelectedRows.Count == 1)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                this.tool_xuanzhe.Enabled = flag;
                this.tool_daying.Enabled = true;
                this.tool_xuanzhe.Enabled = flag;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void sm_MonthChange(object sender, MonthChangeEventArgs e)
        {
            try
            {
                if (this.editFPCX != FaPiaoChaXun.EditFPCX.FuZhi)
                {
                    this.ChaXunDate(1, this.AvePageNum, 1);
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
            this.toolTxt_quick_TextChanged(sender, e);
        }

        private void toolTxt_quick_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.toolTxt_quick != null)
                {
                    this.ChaXunDate(1, this.AvePageNum, 1);
                    this.SetBtnEnabled();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[toolTxt_quick_TextChanged函数异常]" + exception.ToString());
            }
        }

        private bool XHQDDaYin(string strFpzl, string strFpDm, int iFphm, bool IsLastFp)
        {
            try
            {
                bool flag = true;
                string str = string.Format(DingYiZhiFuChuan1.strFphmFormat, iFphm);
                IPrint print = FPPrint.Create(this.GetInvoiceType(strFpzl.Trim()), strFpDm, iFphm, false);
                print.Print(true);
                flag = print.IsPrint.Equals("0000");
                string str3 = string.Empty;
                if (flag)
                {
                    str3 = DingYiZhiFuChuan1.strDaYinDefault[0];
                }
                else
                {
                    str3 = DingYiZhiFuChuan1.strDaYinDefault[1];
                }
                if (IsLastFp)
                {
                    MessageManager.ShowMsgBox("FPCX-000029", "提示", new string[] { str, str3 });
                }
                else
                {
                    return (DialogResult.Yes == MessageManager.ShowMsgBox("FPCX-000030", "提示", new string[] { str, str3 }));
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
    }
}

