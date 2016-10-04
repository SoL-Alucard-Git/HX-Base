namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.PrintGrid;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class InvStatForm : BaseForm
    {
        private IContainer components;
        private CustomStyleDataGrid customStyleDataGridReserve;
        protected DataGridViewTextBoxColumn HJ;
        private AisinoLBL labelAllot;
        private AisinoLBL labelAllotNum;
        private AisinoLBL labelBuyNum;
        private AisinoLBL labelEarlyStockNum;
        private AisinoLBL labelEndStockNum;
        private AisinoLBL labelMinusNum;
        private AisinoLBL labelMinusWasteNum;
        private AisinoLBL labelPlusNum;
        private AisinoLBL labelPlusWasteNum;
        private AisinoLBL labelReserve;
        private AisinoLBL labelRetrive;
        private AisinoLBL labelRetriveNum;
        private AisinoLBL labelReturnNum;
        private ILog loger;
        private bool m_bMonth;
        private bool m_bShowAll;
        private List<CInvStatData> m_CInvStatDataMonthlyList;
        private List<CInvStatData> m_CInvStatDataYearlyList;
        private CommFun m_commFun;
        private InvStatBLL m_invStatBLL;
        private INV_TYPE m_invType;
        private List<InvTypeEntity> m_InvTypeEntityList;
        private QueryPrintEntity m_queryPrintEntity;
        private string[] m_strHead;
        private TaxDateSegment m_taxDateSegment;
        protected DataGridViewTextBoxColumn OTHER;
        private AisinoPNL panelReserve;
        protected DataGridViewTextBoxColumn PER13;
        protected DataGridViewTextBoxColumn PER17;
        protected DataGridViewTextBoxColumn PER4;
        protected DataGridViewTextBoxColumn PER6;
        private string[] strHead;
        private AisinoTAB tabControlType;
        private ToolStripButton toolStripButtonExit;
        private ToolStripButton toolStripButtonForm;
        private ToolStripButton toolStripButtonPrint;
        private ToolStripButton toolStripButtonQuery;
        private ToolStripButton toolStripButtonRefresh;
        private ToolStripButton toolStripButtonSum;
        private ToolStrip toolStripMenu;
        private XmlComponentLoader xmlComponentLoader1;
        protected DataGridViewTextBoxColumn XMMC;

        public InvStatForm(QueryPrintEntity _queryPrintEntity)
        {
            this.loger = LogUtil.GetLogger<InvStatForm>();
            this.m_commFun = new CommFun();
            this.m_invStatBLL = new InvStatBLL();
            this.m_taxDateSegment = new TaxDateSegment();
            this.m_queryPrintEntity = new QueryPrintEntity();
            this.m_InvTypeEntityList = new List<InvTypeEntity>();
            this.m_strHead = new string[10];
            this.m_bShowAll = true;
            this.strHead = new string[] { "项目名称", "合计", "17%", "13%", "6%", "4%", "其他" };
            this.toolStripButtonExit = new ToolStripButton();
            this.toolStripButtonQuery = new ToolStripButton();
            this.toolStripButtonPrint = new ToolStripButton();
            this.toolStripButtonSum = new ToolStripButton();
            this.toolStripButtonForm = new ToolStripButton();
            this.toolStripButtonRefresh = new ToolStripButton();
            this.tabControlType = new AisinoTAB();
            this.panelReserve = new AisinoPNL();
            this.labelReserve = new AisinoLBL();
            this.customStyleDataGridReserve = new CustomStyleDataGrid();
            this.labelEarlyStockNum = new AisinoLBL();
            this.labelBuyNum = new AisinoLBL();
            this.labelReturnNum = new AisinoLBL();
            this.labelPlusNum = new AisinoLBL();
            this.labelPlusWasteNum = new AisinoLBL();
            this.labelEndStockNum = new AisinoLBL();
            this.labelMinusNum = new AisinoLBL();
            this.labelMinusWasteNum = new AisinoLBL();
            this.labelAllotNum = new AisinoLBL();
            this.labelRetriveNum = new AisinoLBL();
            this.labelAllot = new AisinoLBL();
            this.labelRetrive = new AisinoLBL();
            this.toolStripMenu = new ToolStrip();
            this.Initial();
            ControlStyleUtil.SetToolStripStyle(this.toolStripMenu);
            this.toolStripButtonExit.Margin = new Padding(20, 1, 0, 2);
            this.tabControlType.Dock = DockStyle.Fill;
            this.m_queryPrintEntity = _queryPrintEntity;
            this.m_bShowAll = false;
        }

        public InvStatForm(TaxDateSegment _taxDateSegment, bool _bMonth)
        {
            this.loger = LogUtil.GetLogger<InvStatForm>();
            this.m_commFun = new CommFun();
            this.m_invStatBLL = new InvStatBLL();
            this.m_taxDateSegment = new TaxDateSegment();
            this.m_queryPrintEntity = new QueryPrintEntity();
            this.m_InvTypeEntityList = new List<InvTypeEntity>();
            this.m_strHead = new string[10];
            this.m_bShowAll = true;
            this.strHead = new string[] { "项目名称", "合计", "17%", "13%", "6%", "4%", "其他" };
            this.toolStripButtonExit = new ToolStripButton();
            this.toolStripButtonQuery = new ToolStripButton();
            this.toolStripButtonPrint = new ToolStripButton();
            this.toolStripButtonSum = new ToolStripButton();
            this.toolStripButtonForm = new ToolStripButton();
            this.toolStripButtonRefresh = new ToolStripButton();
            this.tabControlType = new AisinoTAB();
            this.panelReserve = new AisinoPNL();
            this.labelReserve = new AisinoLBL();
            this.customStyleDataGridReserve = new CustomStyleDataGrid();
            this.labelEarlyStockNum = new AisinoLBL();
            this.labelBuyNum = new AisinoLBL();
            this.labelReturnNum = new AisinoLBL();
            this.labelPlusNum = new AisinoLBL();
            this.labelPlusWasteNum = new AisinoLBL();
            this.labelEndStockNum = new AisinoLBL();
            this.labelMinusNum = new AisinoLBL();
            this.labelMinusWasteNum = new AisinoLBL();
            this.labelAllotNum = new AisinoLBL();
            this.labelRetriveNum = new AisinoLBL();
            this.labelAllot = new AisinoLBL();
            this.labelRetrive = new AisinoLBL();
            this.toolStripMenu = new ToolStrip();
            this.Initial();
            ControlStyleUtil.SetToolStripStyle(this.toolStripMenu);
            this.toolStripButtonExit.Margin = new Padding(20, 1, 0, 2);
            this.tabControlType.Dock = DockStyle.Fill;
            this.m_taxDateSegment = _taxDateSegment;
            this.m_bMonth = _bMonth;
            this.m_bShowAll = true;
        }

        public InvStatForm(TaxDateSegment _taxDateSegment, bool _bMonth, INV_TYPE InvType)
        {
            this.loger = LogUtil.GetLogger<InvStatForm>();
            this.m_commFun = new CommFun();
            this.m_invStatBLL = new InvStatBLL();
            this.m_taxDateSegment = new TaxDateSegment();
            this.m_queryPrintEntity = new QueryPrintEntity();
            this.m_InvTypeEntityList = new List<InvTypeEntity>();
            this.m_strHead = new string[10];
            this.m_bShowAll = true;
            this.strHead = new string[] { "项目名称", "合计", "17%", "13%", "6%", "4%", "其他" };
            this.toolStripButtonExit = new ToolStripButton();
            this.toolStripButtonQuery = new ToolStripButton();
            this.toolStripButtonPrint = new ToolStripButton();
            this.toolStripButtonSum = new ToolStripButton();
            this.toolStripButtonForm = new ToolStripButton();
            this.toolStripButtonRefresh = new ToolStripButton();
            this.tabControlType = new AisinoTAB();
            this.panelReserve = new AisinoPNL();
            this.labelReserve = new AisinoLBL();
            this.customStyleDataGridReserve = new CustomStyleDataGrid();
            this.labelEarlyStockNum = new AisinoLBL();
            this.labelBuyNum = new AisinoLBL();
            this.labelReturnNum = new AisinoLBL();
            this.labelPlusNum = new AisinoLBL();
            this.labelPlusWasteNum = new AisinoLBL();
            this.labelEndStockNum = new AisinoLBL();
            this.labelMinusNum = new AisinoLBL();
            this.labelMinusWasteNum = new AisinoLBL();
            this.labelAllotNum = new AisinoLBL();
            this.labelRetriveNum = new AisinoLBL();
            this.labelAllot = new AisinoLBL();
            this.labelRetrive = new AisinoLBL();
            this.toolStripMenu = new ToolStrip();
            this.Initial();
            ControlStyleUtil.SetToolStripStyle(this.toolStripMenu);
            this.toolStripButtonExit.Margin = new Padding(20, 1, 0, 2);
            this.tabControlType.Dock = DockStyle.Fill;
            this.m_taxDateSegment = _taxDateSegment;
            this.m_bMonth = _bMonth;
            this.m_bShowAll = true;
            this.m_invType = InvType;
        }

        private void CreateDataGrid(ref CustomStyleDataGrid dataGridView, QueryPrintEntity _queryPrintEntity)
        {
            try
            {
                this.m_invStatBLL.CreateMonthlyDataGrid(ref dataGridView, _queryPrintEntity.m_nYear, _queryPrintEntity.m_nMonth, _queryPrintEntity.m_nTaxPeriod, _queryPrintEntity.m_invType);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void CreateHead(QueryPrintEntity _queryPrintEntity)
        {
            try
            {
                this.m_strHead = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                this.m_strHead = this.m_invStatBLL.CreateMonthlyHead(_queryPrintEntity.m_nYear, _queryPrintEntity.m_nMonth, _queryPrintEntity.m_nTaxPeriod, _queryPrintEntity.m_invType);
                this.SetLabelValue(this.m_strHead);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void CreatePage(ref CustomStyleDataGrid dataGridView, QueryPrintEntity _queryPrintEntity)
        {
            try
            {
                this.CreateHead(this.m_queryPrintEntity);
                this.m_invStatBLL.CreateMonthlyDataGrid(ref dataGridView, _queryPrintEntity.m_nYear, _queryPrintEntity.m_nMonth, _queryPrintEntity.m_nTaxPeriod, _queryPrintEntity.m_invType);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
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

        private void Initial()
        {
            this.InitializeComponent();
            base.Load += new EventHandler(this.InvStatForm_Load);
            this.panelReserve = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panelReserve");
            this.panelReserve.BackColor = Color.FromArgb(0xf2, 0xf4, 0xf5);
            this.panelReserve.Visible = false;
            this.toolStripMenu = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStripMenu");
            this.toolStripMenu.Visible = true;
            this.toolStripButtonExit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonExit");
            this.toolStripButtonExit.Click += new EventHandler(this.toolStripButtonExit_Click);
            this.toolStripButtonQuery = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonQuery");
            this.toolStripButtonQuery.Visible = false;
            this.toolStripButtonPrint = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonPrint");
            this.toolStripButtonPrint.Click += new EventHandler(this.toolStripButtonPrint_Click);
            this.toolStripButtonSum = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonSum");
            this.toolStripButtonSum.Visible = false;
            this.toolStripButtonForm = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonForm");
            this.toolStripButtonForm.Click += new EventHandler(this.toolStripButtonForm_Click);
            this.toolStripButtonRefresh = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonRefresh");
            this.toolStripButtonRefresh.Visible = false;
            this.tabControlType = this.xmlComponentLoader1.GetControlByName<AisinoTAB>("tabControlType");
            this.tabControlType.SelectedIndexChanged += new EventHandler(this.tabControlType_SelectedIndexChanged);
            this.labelReserve = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelReserve");
            this.labelReserve.BorderStyle = BorderStyle.FixedSingle;
            this.customStyleDataGridReserve = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGridReserve");
            this.customStyleDataGridReserve.ReadOnly = true;
            this.customStyleDataGridReserve.AllowUserToDeleteRows = false;
            this.customStyleDataGridReserve.set_AllowUserToResizeRows(false);
            this.customStyleDataGridReserve.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.DisableResizing);
            this.customStyleDataGridReserve.Dock = DockStyle.Fill;
            this.InsertGridColumn();
            this.labelEarlyStockNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelEarlyStockNum");
            this.labelBuyNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelBuyNum");
            this.labelReturnNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelReturnNum");
            this.labelPlusNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelPlusNum");
            this.labelPlusWasteNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelPlusWasteNum");
            this.labelEndStockNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelEndStockNum");
            this.labelMinusNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelMinusNum");
            this.labelMinusWasteNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelMinusWasteNum");
            this.labelAllotNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelAllotNum");
            this.labelAllotNum.Visible = false;
            this.labelRetriveNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelRetriveNum");
            this.labelRetriveNum.Visible = false;
            this.labelAllot = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelAllot");
            this.labelAllot.Visible = false;
            this.labelRetrive = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelRetrive");
            this.labelRetrive.Visible = false;
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.InvStatForm\Aisino.Fwkp.Bsgl.InvStatForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "InvStatForm";
            this.Text = "资料统计";
            base.ResumeLayout(false);
        }

        protected virtual void InsertGridColumn()
        {
            try
            {
                this.customStyleDataGridReserve.Rows.Clear();
                this.XMMC = new DataGridViewTextBoxColumn();
                this.HJ = new DataGridViewTextBoxColumn();
                this.PER17 = new DataGridViewTextBoxColumn();
                this.PER13 = new DataGridViewTextBoxColumn();
                this.PER6 = new DataGridViewTextBoxColumn();
                this.PER4 = new DataGridViewTextBoxColumn();
                this.OTHER = new DataGridViewTextBoxColumn();
                int index = 0;
                this.XMMC.HeaderText = "项目名称";
                this.XMMC.Name = this.strHead[index];
                this.XMMC.DataPropertyName = this.strHead[index++];
                this.XMMC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.XMMC.Width = 0x5f;
                this.HJ.HeaderText = "合计";
                this.HJ.Name = this.strHead[index];
                this.HJ.DataPropertyName = this.strHead[index++];
                this.HJ.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.HJ.Width = 90;
                this.PER17.HeaderText = "17%";
                this.PER17.Name = this.strHead[index];
                this.PER17.DataPropertyName = this.strHead[index++];
                this.PER17.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.PER17.Width = 90;
                this.PER13.HeaderText = "13%";
                this.PER13.Name = this.strHead[index];
                this.PER13.DataPropertyName = this.strHead[index++];
                this.PER13.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.PER13.Width = 90;
                this.PER6.HeaderText = "6%";
                this.PER6.Name = this.strHead[index];
                this.PER6.DataPropertyName = this.strHead[index++];
                this.PER6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.PER6.Width = 90;
                this.PER4.HeaderText = "4%";
                this.PER4.Name = this.strHead[index];
                this.PER4.DataPropertyName = this.strHead[index++];
                this.PER4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.PER4.Width = 90;
                this.OTHER.HeaderText = "其他";
                this.OTHER.Name = this.strHead[index];
                this.OTHER.DataPropertyName = this.strHead[index++];
                this.OTHER.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.OTHER.Width = 90;
                int num2 = 0;
                this.customStyleDataGridReserve.ColumnAdd(this.XMMC);
                this.customStyleDataGridReserve.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridReserve.ColumnAdd(this.HJ);
                this.customStyleDataGridReserve.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridReserve.ColumnAdd(this.PER17);
                this.customStyleDataGridReserve.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridReserve.ColumnAdd(this.PER13);
                this.customStyleDataGridReserve.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridReserve.ColumnAdd(this.PER6);
                this.customStyleDataGridReserve.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridReserve.ColumnAdd(this.PER4);
                this.customStyleDataGridReserve.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridReserve.ColumnAdd(this.OTHER);
                this.customStyleDataGridReserve.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridReserve.AllowUserToAddRows = false;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void InvStatForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.m_bShowAll)
                {
                    if (this.m_bMonth)
                    {
                        this.m_CInvStatDataMonthlyList = this.m_invStatBLL.CreateMonthlyStat(this.m_taxDateSegment.m_nYear, this.m_taxDateSegment.m_nStartMonth, this.m_taxDateSegment.m_nTaxPeriod);
                        InvTypeEntity item = new InvTypeEntity();
                        if (this.m_CInvStatDataMonthlyList == null)
                        {
                            return;
                        }
                        for (int i = 0; i < this.m_CInvStatDataMonthlyList.Count; i++)
                        {
                            string strInvTypeName = this.m_CInvStatDataMonthlyList[i].m_strInvTypeName;
                            if ((this.m_invType == INV_TYPE.INV_TRANSPORTATION) && (strInvTypeName == "货物运输业增值税专用发票"))
                            {
                                TabPage page = new TabPage(strInvTypeName);
                                this.tabControlType.TabPages.Add(page);
                                item.m_invType = INV_TYPE.INV_TRANSPORTATION;
                                item.m_strInvName = "货物运输业增值税专用发票";
                                this.m_InvTypeEntityList.Add(item);
                                break;
                            }
                            if ((this.m_invType == INV_TYPE.INV_VEHICLESALES) && (strInvTypeName == "机动车销售统一发票"))
                            {
                                TabPage page2 = new TabPage(strInvTypeName);
                                this.tabControlType.TabPages.Add(page2);
                                item.m_invType = INV_TYPE.INV_VEHICLESALES;
                                item.m_strInvName = "机动车销售统一发票";
                                this.m_InvTypeEntityList.Add(item);
                                break;
                            }
                            if (((this.m_invType == INV_TYPE.INV_SPECIAL) && (strInvTypeName == "专用发票")) && base.TaxCardInstance.get_QYLX().ISZYFP)
                            {
                                TabPage page3 = new TabPage(strInvTypeName);
                                this.tabControlType.TabPages.Add(page3);
                                item = new InvTypeEntity {
                                    m_invType = INV_TYPE.INV_SPECIAL,
                                    m_strInvName = "增值税专用发票"
                                };
                                this.m_InvTypeEntityList.Add(item);
                            }
                            else if (((this.m_invType == INV_TYPE.INV_SPECIAL) && (strInvTypeName == "普通发票")) && base.TaxCardInstance.get_QYLX().ISPTFP)
                            {
                                TabPage page4 = new TabPage(strInvTypeName);
                                this.tabControlType.TabPages.Add(page4);
                                item = new InvTypeEntity {
                                    m_invType = INV_TYPE.INV_COMMON,
                                    m_strInvName = "增值税普通发票"
                                };
                                this.m_InvTypeEntityList.Add(item);
                            }
                            else
                            {
                                if ((this.m_invType == INV_TYPE.INV_PTDZ) && (strInvTypeName == "电子增值税普通发票"))
                                {
                                    TabPage page5 = new TabPage(strInvTypeName);
                                    this.tabControlType.TabPages.Add(page5);
                                    item.m_invType = INV_TYPE.INV_PTDZ;
                                    item.m_strInvName = "电子增值税普通发票";
                                    this.m_InvTypeEntityList.Add(item);
                                    break;
                                }
                                if ((this.m_invType == INV_TYPE.INV_JSFP) && (strInvTypeName == "增值税普通发票(卷票)"))
                                {
                                    TabPage page6 = new TabPage(strInvTypeName);
                                    this.tabControlType.TabPages.Add(page6);
                                    item.m_invType = INV_TYPE.INV_JSFP;
                                    item.m_strInvName = "增值税普通发票(卷票)";
                                    this.m_InvTypeEntityList.Add(item);
                                    break;
                                }
                            }
                        }
                        if (this.tabControlType.TabPages.Count > 0)
                        {
                            this.panelReserve.Visible = true;
                            this.panelReserve.Parent = this.tabControlType.TabPages[this.tabControlType.SelectedIndex];
                            this.panelReserve.Dock = DockStyle.Fill;
                            foreach (TabPage page7 in this.tabControlType.TabPages)
                            {
                                if (page7.Text == "专用发票")
                                {
                                    this.m_strHead = this.m_CInvStatDataMonthlyList[0].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataMonthlyList[0].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataMonthlyList[0].m_DataTableGrid);
                                }
                                if (page7.Text == "普通发票")
                                {
                                    this.m_strHead = this.m_CInvStatDataMonthlyList[1].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataMonthlyList[1].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataMonthlyList[1].m_DataTableGrid);
                                }
                                if (page7.Text == "货物运输业增值税专用发票")
                                {
                                    this.m_strHead = this.m_CInvStatDataMonthlyList[2].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataMonthlyList[2].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataMonthlyList[2].m_DataTableGrid);
                                }
                                if (page7.Text == "机动车销售统一发票")
                                {
                                    this.m_strHead = this.m_CInvStatDataMonthlyList[3].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataMonthlyList[3].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataMonthlyList[3].m_DataTableGrid);
                                }
                                if (page7.Text == "电子增值税普通发票")
                                {
                                    this.m_strHead = this.m_CInvStatDataMonthlyList[4].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataMonthlyList[4].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataMonthlyList[4].m_DataTableGrid);
                                }
                                if (page7.Text == "增值税普通发票(卷票)")
                                {
                                    this.m_strHead = this.m_CInvStatDataMonthlyList[5].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataMonthlyList[5].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataMonthlyList[5].m_DataTableGrid);
                                }
                            }
                        }
                    }
                    else
                    {
                        this.m_CInvStatDataYearlyList = this.m_invStatBLL.CreateYearlyStat(this.m_taxDateSegment.m_nYear, this.m_taxDateSegment.m_nStartMonth, this.m_taxDateSegment.m_nEndMonth);
                        InvTypeEntity entity2 = new InvTypeEntity();
                        if (this.m_CInvStatDataYearlyList == null)
                        {
                            return;
                        }
                        for (int j = 0; j < this.m_CInvStatDataYearlyList.Count; j++)
                        {
                            string text = this.m_CInvStatDataYearlyList[j].m_strInvTypeName;
                            if ((this.m_invType == INV_TYPE.INV_TRANSPORTATION) && (text == "货物运输业增值税专用发票"))
                            {
                                TabPage page8 = new TabPage(text);
                                this.tabControlType.TabPages.Add(page8);
                                entity2.m_invType = INV_TYPE.INV_TRANSPORTATION;
                                entity2.m_strInvName = "货物运输业增值税专用发票";
                                this.m_InvTypeEntityList.Add(entity2);
                                break;
                            }
                            if ((this.m_invType == INV_TYPE.INV_VEHICLESALES) && (text == "机动车销售统一发票"))
                            {
                                TabPage page9 = new TabPage(text);
                                this.tabControlType.TabPages.Add(page9);
                                entity2.m_invType = INV_TYPE.INV_VEHICLESALES;
                                entity2.m_strInvName = "机动车销售统一发票";
                                this.m_InvTypeEntityList.Add(entity2);
                                break;
                            }
                            if (((this.m_invType == INV_TYPE.INV_SPECIAL) && (text == "专用发票")) && base.TaxCardInstance.get_QYLX().ISZYFP)
                            {
                                TabPage page10 = new TabPage(text);
                                this.tabControlType.TabPages.Add(page10);
                                entity2 = new InvTypeEntity {
                                    m_invType = INV_TYPE.INV_SPECIAL,
                                    m_strInvName = "增值税专用发票"
                                };
                                this.m_InvTypeEntityList.Add(entity2);
                            }
                            else if (((this.m_invType == INV_TYPE.INV_SPECIAL) && (text == "普通发票")) && base.TaxCardInstance.get_QYLX().ISPTFP)
                            {
                                TabPage page11 = new TabPage(text);
                                this.tabControlType.TabPages.Add(page11);
                                entity2 = new InvTypeEntity {
                                    m_invType = INV_TYPE.INV_COMMON,
                                    m_strInvName = "增值税普通发票"
                                };
                                this.m_InvTypeEntityList.Add(entity2);
                            }
                            if ((this.m_invType == INV_TYPE.INV_PTDZ) && (text == "电子增值税普通发票"))
                            {
                                TabPage page12 = new TabPage(text);
                                this.tabControlType.TabPages.Add(page12);
                                entity2.m_invType = INV_TYPE.INV_PTDZ;
                                entity2.m_strInvName = "电子增值税普通发票";
                                this.m_InvTypeEntityList.Add(entity2);
                                break;
                            }
                            if ((this.m_invType == INV_TYPE.INV_JSFP) && (text == "增值税普通发票(卷票)"))
                            {
                                TabPage page13 = new TabPage(text);
                                this.tabControlType.TabPages.Add(page13);
                                entity2.m_invType = INV_TYPE.INV_JSFP;
                                entity2.m_strInvName = "增值税普通发票(卷票)";
                                this.m_InvTypeEntityList.Add(entity2);
                                break;
                            }
                        }
                        if (this.tabControlType.TabPages.Count > 0)
                        {
                            this.tabControlType.SelectedIndex = 0;
                            this.panelReserve.Visible = true;
                            this.panelReserve.Parent = this.tabControlType.TabPages[this.tabControlType.SelectedIndex];
                            this.panelReserve.Dock = DockStyle.Fill;
                            foreach (TabPage page14 in this.tabControlType.TabPages)
                            {
                                if (page14.Text == "专用发票")
                                {
                                    this.m_strHead = this.m_CInvStatDataYearlyList[0].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataYearlyList[0].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataYearlyList[0].m_DataTableGrid);
                                }
                                if (page14.Text == "普通发票")
                                {
                                    this.m_strHead = this.m_CInvStatDataYearlyList[1].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataYearlyList[1].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataYearlyList[1].m_DataTableGrid);
                                }
                                if (page14.Text == "货物运输业增值税专用发票")
                                {
                                    this.m_strHead = this.m_CInvStatDataYearlyList[2].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataYearlyList[2].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataYearlyList[2].m_DataTableGrid);
                                }
                                if (page14.Text == "机动车销售统一发票")
                                {
                                    this.m_strHead = this.m_CInvStatDataYearlyList[3].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataYearlyList[3].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataYearlyList[3].m_DataTableGrid);
                                }
                                if (page14.Text == "电子增值税普通发票")
                                {
                                    this.m_strHead = this.m_CInvStatDataYearlyList[4].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataYearlyList[4].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataYearlyList[4].m_DataTableGrid);
                                }
                                if (page14.Text == "增值税普通发票(卷票)")
                                {
                                    this.m_strHead = this.m_CInvStatDataYearlyList[5].m_strHeadValue;
                                    this.SetLabelValue(this.m_CInvStatDataYearlyList[5].m_strHeadValue);
                                    this.SetDataSource(ref this.customStyleDataGridReserve, this.m_CInvStatDataYearlyList[5].m_DataTableGrid);
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.m_InvTypeEntityList = this.m_commFun.GetInvTypeCollect();
                    if (this.m_InvTypeEntityList == null)
                    {
                        MessageManager.ShowMsgBox("INP-251303", new string[] { "获取发票种类失败" });
                        return;
                    }
                    InvTypeEntity entity3 = new InvTypeEntity();
                    for (int k = 0; k < this.m_InvTypeEntityList.Count; k++)
                    {
                        if (this.m_queryPrintEntity.m_invType == this.m_InvTypeEntityList[k].m_invType)
                        {
                            entity3 = this.m_InvTypeEntityList[k];
                            break;
                        }
                    }
                    int count = this.m_InvTypeEntityList.Count;
                    for (int m = 0; m < count; m++)
                    {
                        this.m_InvTypeEntityList.RemoveAt(0);
                    }
                    this.m_InvTypeEntityList.Add(entity3);
                    TabPage page15 = new TabPage(entity3.m_strInvName);
                    this.tabControlType.TabPages.Add(page15);
                    this.panelReserve.Visible = true;
                    this.panelReserve.Parent = this.tabControlType.TabPages[0];
                    this.panelReserve.Dock = DockStyle.Fill;
                    this.CreateHead(this.m_queryPrintEntity);
                    this.customStyleDataGridReserve.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
                    this.CreateDataGrid(ref this.customStyleDataGridReserve, this.m_queryPrintEntity);
                }
                this.tabControlType.SelectedIndex = this.tabControlType.TabCount - 1;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        public bool PrintTable(bool _bShow)
        {
            bool flag = false;
            try
            {
                if (!this.m_bShowAll)
                {
                    this.CreatePage(ref this.customStyleDataGridReserve, this.m_queryPrintEntity);
                }
                object[] taxCardInfo = new CommFun().GetTaxCardInfo();
                if (taxCardInfo.Length != 1)
                {
                    return false;
                }
                Dictionary<string, object> dictionary = taxCardInfo[0] as Dictionary<string, object>;
                string str = "";
                string str2 = "";
                List<PrinterItems> list = new List<PrinterItems>();
                List<PrinterItems> list2 = new List<PrinterItems>();
                string strTitle = "";
                if (this.m_bShowAll)
                {
                    strTitle = this.m_InvTypeEntityList[this.tabControlType.SelectedIndex].m_strInvName + "金税设备资料统计";
                }
                else
                {
                    strTitle = this.m_queryPrintEntity.m_strTitle;
                }
                list.Add(new PrinterItems("制表日期：" + base.TaxCardInstance.get_TaxClock().ToShortDateString(), HorizontalAlignment.Left));
                if (!this.m_bShowAll)
                {
                    if (this.m_queryPrintEntity.m_nTaxPeriod == 0)
                    {
                        str = this.m_queryPrintEntity.m_nMonth.ToString() + "月份";
                    }
                    else
                    {
                        str = string.Concat(new object[] { this.m_queryPrintEntity.m_nMonth.ToString(), "月第", this.m_queryPrintEntity.m_nTaxPeriod, "期" });
                    }
                }
                else if (this.m_taxDateSegment.m_nTaxPeriod == 0)
                {
                    str = this.m_taxDateSegment.m_nEndMonth.ToString() + "月份";
                }
                else
                {
                    str = string.Concat(new object[] { this.m_taxDateSegment.m_nEndMonth.ToString(), "月第", this.m_taxDateSegment.m_nTaxPeriod, "期" });
                }
                list.Add(new PrinterItems("所属期间：" + str, HorizontalAlignment.Left));
                if (!this.m_bShowAll)
                {
                    list.Add(new PrinterItems(this.m_queryPrintEntity.m_strSubItem, HorizontalAlignment.Left));
                    list.Add(new PrinterItems(this.m_queryPrintEntity.m_strItemDetail, HorizontalAlignment.Left));
                }
                else
                {
                    list.Add(new PrinterItems(this.Text, HorizontalAlignment.Left));
                }
                list.Add(new PrinterItems("纳税人登记号：" + dictionary["QYBH"].ToString(), HorizontalAlignment.Left));
                list.Add(new PrinterItems("企业名称：" + base.TaxCardInstance.get_Corporation(), HorizontalAlignment.Left));
                list.Add(new PrinterItems("地址电话：" + dictionary["YYDZ"].ToString() + "   " + dictionary["DHHM"].ToString(), HorizontalAlignment.Left));
                list.Add(new PrinterItems("----------------------------------------------------------------------------------------", HorizontalAlignment.Left));
                list.Add(new PrinterItems("★  发票领用存情况  ★", HorizontalAlignment.Left));
                string str4 = "";
                str = "期初库存份数";
                str = (str + this.m_strHead[0].ToString().PadLeft(10) + "    ") + "正数发票份数";
                str = (str + this.m_strHead[3].ToString().PadLeft(10) + "    ") + "负数发票份数";
                str4 = this.m_strHead[6].ToString().PadLeft(10);
                str = str + str4 + "    ";
                list.Add(new PrinterItems(str, HorizontalAlignment.Left));
                str = "购进发票份数";
                str = (str + this.m_strHead[1].ToString().PadLeft(10) + "    ") + "正数废票份数";
                str = (str + this.m_strHead[4].ToString().PadLeft(10) + "    ") + "负数废票份数";
                str4 = this.m_strHead[7].ToString().PadLeft(10);
                str = str + str4 + "    ";
                list.Add(new PrinterItems(str, HorizontalAlignment.Left));
                str = "退回发票份数";
                str = (str + this.m_strHead[2].ToString().PadLeft(10) + "    ") + "期末库存份数";
                str4 = this.m_strHead[5].ToString().PadLeft(10);
                str = str + str4 + "    ";
                if (this.m_invStatBLL.bIsMainMachine && this.m_invStatBLL.bHasChild)
                {
                    str2 = "分配发票份数";
                    str4 = this.m_strHead[8].ToString().PadLeft(10);
                    str2 = str2 + str4 + "    ";
                    str = str + str2;
                }
                list.Add(new PrinterItems(str, HorizontalAlignment.Left));
                if (this.m_invStatBLL.bIsMainMachine && this.m_invStatBLL.bHasChild)
                {
                    str = "";
                    str2 = "收回发票份数";
                    str4 = this.m_strHead[9].ToString().PadLeft(10);
                    str2 = str2 + str4;
                    str = str + str2;
                    list.Add(new PrinterItems(str, HorizontalAlignment.Left));
                }
                list.Add(new PrinterItems("----------------------------------------------------------------------------------------", HorizontalAlignment.Left));
                list.Add(new PrinterItems("★ 销项情况  ★", HorizontalAlignment.Left));
                list.Add(new PrinterItems("金额单位：元", HorizontalAlignment.Left));
                flag = this.m_invStatBLL.PrintTable(ref this.customStyleDataGridReserve, strTitle, list, list2, _bShow);
            }
            catch (Exception exception)
            {
                this.loger.Info("打印发票资料统计信息失败");
                ExceptionHandler.HandleError(exception);
                return false;
            }
            return flag;
        }

        public bool PrintTableSerial(bool _bShow, bool _isSerialPrint, string showText)
        {
            bool flag = false;
            try
            {
                if (!this.m_bShowAll)
                {
                    this.CreatePage(ref this.customStyleDataGridReserve, this.m_queryPrintEntity);
                }
                object[] taxCardInfo = new CommFun().GetTaxCardInfo();
                if (taxCardInfo.Length != 1)
                {
                    return false;
                }
                Dictionary<string, object> dictionary = taxCardInfo[0] as Dictionary<string, object>;
                string str = "";
                string str2 = "";
                List<PrinterItems> list = new List<PrinterItems>();
                List<PrinterItems> list2 = new List<PrinterItems>();
                string strTitle = "";
                if (this.m_bShowAll)
                {
                    strTitle = this.m_InvTypeEntityList[this.tabControlType.SelectedIndex].m_strInvName + "金税设备资料统计";
                }
                else
                {
                    strTitle = this.m_queryPrintEntity.m_strTitle;
                }
                list.Add(new PrinterItems("制表日期：" + base.TaxCardInstance.get_TaxClock().ToShortDateString(), HorizontalAlignment.Left));
                if (!this.m_bShowAll)
                {
                    if (this.m_queryPrintEntity.m_nTaxPeriod == 0)
                    {
                        str = this.m_queryPrintEntity.m_nMonth.ToString() + "月份";
                    }
                    else
                    {
                        str = string.Concat(new object[] { this.m_queryPrintEntity.m_nMonth.ToString(), "月第", this.m_queryPrintEntity.m_nTaxPeriod, "期" });
                    }
                }
                else if (this.m_taxDateSegment.m_nTaxPeriod == 0)
                {
                    str = this.m_taxDateSegment.m_nEndMonth.ToString() + "月份";
                }
                else
                {
                    str = string.Concat(new object[] { this.m_taxDateSegment.m_nEndMonth.ToString(), "月第", this.m_taxDateSegment.m_nTaxPeriod, "期" });
                }
                list.Add(new PrinterItems("所属期间：" + str, HorizontalAlignment.Left));
                if (!this.m_bShowAll)
                {
                    list.Add(new PrinterItems(this.m_queryPrintEntity.m_strSubItem, HorizontalAlignment.Left));
                    list.Add(new PrinterItems(this.m_queryPrintEntity.m_strItemDetail, HorizontalAlignment.Left));
                }
                else
                {
                    list.Add(new PrinterItems(this.Text, HorizontalAlignment.Left));
                }
                list.Add(new PrinterItems("纳税人登记号：" + dictionary["QYBH"].ToString(), HorizontalAlignment.Left));
                list.Add(new PrinterItems("企业名称：" + base.TaxCardInstance.get_Corporation(), HorizontalAlignment.Left));
                list.Add(new PrinterItems("地址电话：" + dictionary["YYDZ"].ToString() + "   " + dictionary["DHHM"].ToString(), HorizontalAlignment.Left));
                list.Add(new PrinterItems("----------------------------------------------------------------------------------------", HorizontalAlignment.Left));
                list.Add(new PrinterItems("★  发票领用存情况  ★", HorizontalAlignment.Left));
                string str4 = "";
                str = "期初库存份数";
                str = (str + this.m_strHead[0].ToString().PadLeft(10) + "    ") + "正数发票份数";
                str = (str + this.m_strHead[3].ToString().PadLeft(10) + "    ") + "负数发票份数";
                str4 = this.m_strHead[6].ToString().PadLeft(10);
                str = str + str4 + "    ";
                list.Add(new PrinterItems(str, HorizontalAlignment.Left));
                str = "购进发票份数";
                str = (str + this.m_strHead[1].ToString().PadLeft(10) + "    ") + "正数废票份数";
                str = (str + this.m_strHead[4].ToString().PadLeft(10) + "    ") + "负数废票份数";
                str4 = this.m_strHead[7].ToString().PadLeft(10);
                str = str + str4 + "    ";
                list.Add(new PrinterItems(str, HorizontalAlignment.Left));
                str = "退回发票份数";
                str = (str + this.m_strHead[2].ToString().PadLeft(10) + "    ") + "期末库存份数";
                str4 = this.m_strHead[5].ToString().PadLeft(10);
                str = str + str4 + "    ";
                if (this.m_invStatBLL.bIsMainMachine && this.m_invStatBLL.bHasChild)
                {
                    str2 = "分配发票份数";
                    str4 = this.m_strHead[8].ToString().PadLeft(10);
                    str2 = str2 + str4 + "    ";
                    str = str + str2;
                }
                list.Add(new PrinterItems(str, HorizontalAlignment.Left));
                if (this.m_invStatBLL.bIsMainMachine && this.m_invStatBLL.bHasChild)
                {
                    str = "";
                    str2 = "收回发票份数";
                    str4 = this.m_strHead[9].ToString().PadLeft(10);
                    str2 = str2 + str4;
                    str = str + str2;
                    list.Add(new PrinterItems(str, HorizontalAlignment.Left));
                }
                list.Add(new PrinterItems("----------------------------------------------------------------------------------------", HorizontalAlignment.Left));
                list.Add(new PrinterItems("★ 销项情况  ★", HorizontalAlignment.Left));
                list.Add(new PrinterItems("金额单位：元", HorizontalAlignment.Left));
                flag = this.m_invStatBLL.PrintTableSerial(ref this.customStyleDataGridReserve, strTitle, list, list2, _bShow, _isSerialPrint, showText);
            }
            catch (Exception exception)
            {
                if (exception.Message.Equals("用户放弃连续打印"))
                {
                    throw exception;
                }
                this.loger.Info("打印发票资料统计信息失败");
                return false;
            }
            return flag;
        }

        private void SetDataSource(ref CustomStyleDataGrid dataGridView, DataTable dtSource)
        {
            try
            {
                dataGridView.ReadOnly = true;
                dataGridView.AllowUserToAddRows = false;
                if (this.m_bMonth)
                {
                    dataGridView.Columns["17%"].Visible = true;
                    dataGridView.Columns["13%"].Visible = true;
                    dataGridView.Columns["6%"].Visible = true;
                    dataGridView.Columns["4%"].Visible = true;
                    dataGridView.Columns["其他"].Visible = true;
                }
                else
                {
                    dataGridView.Columns["17%"].Visible = false;
                    dataGridView.Columns["13%"].Visible = false;
                    dataGridView.Columns["6%"].Visible = false;
                    dataGridView.Columns["4%"].Visible = false;
                    dataGridView.Columns["其他"].Visible = false;
                }
                dataGridView.DataSource = dtSource;
                dataGridView.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        private void SetLabelValue(string[] strValue)
        {
            try
            {
                this.labelEarlyStockNum.Text = strValue[0];
                this.labelBuyNum.Text = strValue[1];
                this.labelReturnNum.Text = strValue[2];
                this.labelPlusNum.Text = strValue[3];
                this.labelPlusWasteNum.Text = strValue[4];
                this.labelEndStockNum.Text = strValue[5];
                this.labelMinusNum.Text = strValue[6];
                this.labelMinusWasteNum.Text = strValue[7];
                if (this.m_invStatBLL.bIsMainMachine && this.m_invStatBLL.bHasChild)
                {
                    this.labelAllot.Visible = true;
                    this.labelAllotNum.Visible = true;
                    this.labelAllotNum.Text = strValue[8];
                    this.labelRetrive.Visible = true;
                    this.labelRetriveNum.Visible = true;
                    this.labelRetriveNum.Text = strValue[9];
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void tabControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = this.tabControlType.SelectedIndex;
                this.m_strHead = new string[10];
                DataTable dtSource = new DataTable();
                if (this.m_bMonth)
                {
                    if (this.m_CInvStatDataMonthlyList == null)
                    {
                        return;
                    }
                    this.m_strHead = this.m_CInvStatDataMonthlyList[selectedIndex].m_strHeadValue;
                    dtSource = this.m_CInvStatDataMonthlyList[selectedIndex].m_DataTableGrid;
                }
                else
                {
                    if (this.m_CInvStatDataYearlyList == null)
                    {
                        return;
                    }
                    this.m_strHead = this.m_CInvStatDataYearlyList[selectedIndex].m_strHeadValue;
                    dtSource = this.m_CInvStatDataYearlyList[selectedIndex].m_DataTableGrid;
                }
                this.panelReserve.Visible = true;
                this.panelReserve.Parent = this.tabControlType.TabPages[selectedIndex];
                this.panelReserve.Dock = DockStyle.Fill;
                this.SetLabelValue(this.m_strHead);
                this.SetDataSource(ref this.customStyleDataGridReserve, dtSource);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolStripButtonForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGridReserve").SetColumnStyles(this.xmlComponentLoader1.get_XMLPath(), this);
            }
            catch (Exception exception)
            {
                this.loger.Info("设置格式失败");
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.PrintTable(true);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        public string m_strTitle
        {
            get
            {
                return this.labelReserve.Text;
            }
            set
            {
                this.labelReserve.Text = value;
            }
        }
    }
}

