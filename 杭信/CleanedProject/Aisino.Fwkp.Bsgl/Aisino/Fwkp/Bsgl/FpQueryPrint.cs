namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.PrintGrid;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FPQueryPrint : DockForm
    {
        private bool _isTabIndexChanged;
        private bool checkCause1;
        private bool checkCause2;
        private bool checkCause3;
        private bool checkCause4;
        private ToolStripComboBox comboBox2;
        private ToolStripComboBox comboBox3;
        private ToolStripComboBox comboBox4;
        private IContainer components;
        private CustomStyleDataGrid customStyleDataGridQD;
        private CustomStyleDataGrid customStyleDataGridReserve;
        protected DataGridViewTextBoxColumn FPHM;
        private const string FpNameDZ = "电子增值税普通发票";
        private const string FpNameHY = "货物运输业增值税专用发票";
        private const string FpNameJDC = "机动车销售统一发票";
        private const string FpNameJSFP = "增值税普通发票(卷票)";
        private const string FpNamePT = "增值税普通发票";
        private const string FpNameZY = "增值税专用发票";
        protected DataGridViewTextBoxColumn FPZL;
        protected DataGridViewTextBoxColumn GFSH;
        protected DataGridViewTextBoxColumn HJ;
        protected DataGridViewTextBoxColumn HJJE;
        protected DataGridViewTextBoxColumn HJSE;
        private bool initFinished;
        private bool isInit;
        protected DataGridViewTextBoxColumn KPRQ;
        private ToolStripLabel label2;
        private ToolStripLabel label3;
        private ToolStripLabel label4;
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
        private string lastRepDateDZ = "";
        private string lastRepDateHY = "";
        private string lastRepDateJDC = "";
        private string lastRepDateJSFP = "";
        protected DataGridViewTextBoxColumn LBDM;
        private ILog loger = LogUtil.GetLogger<FPQuery>();
        private List<AisinoCHK> m_checkBoxList = new List<AisinoCHK>();
        private List<CInvStatData> m_CInvStatDataMonthlyList;
        private List<CInvStatData> m_CInvStatDataYearlyList;
        private CommFun m_commFun = new CommFun();
        private InvStatBLL m_invStatBLL = new InvStatBLL();
        private List<InvTypeEntity> m_InvTypeEntityList = new List<InvTypeEntity>();
        private List<InvTypeEntity> m_InvTypeEntityListPrint = new List<InvTypeEntity>();
        private List<ItemEntity> m_ItemsList = new List<ItemEntity>();
        private object[] m_objHead;
        public List<PrintEntity> m_PrintEntityList = new List<PrintEntity>();
        private QueryPrintBLL m_queryPrintBLL = new QueryPrintBLL();
        private QueryPrintEntity m_QueryPrintEntity = new QueryPrintEntity();
        private List<QueryPrintEntity> m_QueryPrintEntityList = new List<QueryPrintEntity>();
        private string[] m_strHead = new string[10];
        private int nMonth;
        private int nYear;
        protected DataGridViewTextBoxColumn OTHER;
        private Panel panel2;
        private Panel panel3;
        private AisinoPNL panelTop;
        protected DataGridViewTextBoxColumn PER13;
        protected DataGridViewTextBoxColumn PER17;
        protected DataGridViewTextBoxColumn PER4;
        protected DataGridViewTextBoxColumn PER6;
        private FPProgressBar progressBar = new FPProgressBar();
        private QueryPrintBLL queryPrintBLL = new QueryPrintBLL();
        private QueryPrintEntity queryPrintEntity = new QueryPrintEntity();
        protected DataGridViewTextBoxColumn SLV;
        private string[] strHead = new string[] { "项目名称", "合计", "17%", "13%", "6%", "4%", "其他" };
        private string[] strHeadQD = new string[] { "发票种类", "类别代码", "发票号码", "开票日期", "购方税号", "合计金额", "合计税额", "税率" };
        private AisinoTAB tabControlType;
        private TabPage tabPage1;
        private TaxcardEntityBLL taxcardEntityBLL = new TaxcardEntityBLL();
        private TabPage thePage;
        private int thePageIndex;
        private ToolStripButton toolStripButtonExit;
        private ToolStripButton toolStripButtonFormat;
        private ToolStripButton toolStripButtonPreView;
        private ToolStripButton toolStripButtonPrint;
        private ToolStripButton toolStripButtonQuery;
        private ToolStripButton toolStripButtonRefresh;
        private ToolStrip toolStripMenu;
        private XmlComponentLoader xmlComponentLoader1;
        protected DataGridViewTextBoxColumn XMMC;

        public FPQueryPrint()
        {
            this.InitializeComponent();
            this.Initial();
            this.InitQueryPrint();
            this.initFinished = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.initFinished && !this._isTabIndexChanged)
            {
                this.InitPeriod();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.initFinished && !this._isTabIndexChanged)
            {
                this.toolStripButtonQuery_Click(null, null);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.initFinished && !this._isTabIndexChanged)
            {
                this.toolStripButtonQuery_Click(null, null);
            }
        }

        private void CreateDataGrid(ref CustomStyleDataGrid dataGridView, QueryPrintEntity _queryPrintEntity)
        {
            try
            {
                this.m_invStatBLL.CreateMonthlyDataGridCXDY(ref dataGridView, _queryPrintEntity.m_nYear, _queryPrintEntity.m_nMonth, _queryPrintEntity.m_nTaxPeriod, _queryPrintEntity.m_invType);
            }
            catch (Exception)
            {
            }
        }

        private void CreateHead(QueryPrintEntity _queryPrintEntity)
        {
            try
            {
                this.m_strHead = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                this.m_strHead = this.m_invStatBLL.CreateMonthlyHeadCXDY(_queryPrintEntity.m_nYear, _queryPrintEntity.m_nMonth, _queryPrintEntity.m_nTaxPeriod, _queryPrintEntity.m_invType);
                this.SetLabelValue(this.m_strHead);
            }
            catch (Exception)
            {
            }
        }

        private void CreatePage(ref CustomStyleDataGrid dataGridView, QueryPrintEntity _queryPrintEntity)
        {
            try
            {
                this.CreateHead(_queryPrintEntity);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void CreateTitle(QueryPrintEntity _queryPrintEntity)
        {
            string str = "资料统计";
            if (this.comboBox3.SelectedItem.Equals("本月累计"))
            {
                str = "税档资料所属期为   " + _queryPrintEntity.m_nMonth.ToString() + "月份";
            }
            else
            {
                str = "税档资料所属期为   " + _queryPrintEntity.m_nMonth.ToString() + "月第" + _queryPrintEntity.m_nTaxPeriod.ToString() + "期";
            }
            this.labelReserve.Text = str;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitColor()
        {
            this.labelReserve.BackColor = SystemColor.GRID_TITLE_BACKCOLOR;
            this.BackColor = SystemColor.INVGRID_ROW_BACKCOLOR;
            this.labelReserve.Font = SystemColor.GRID_TITLE_FONT;
            this.labelReserve.ForeColor = SystemColor.GRID_TITLE_FONTCOLOR;
        }

        private void InitCXXX()
        {
            this.comboBox4.Items.Clear();
            this.comboBox4.Items.Add("增值税发票汇总表");
            this.comboBox4.Items.Add("正数发票清单");
            this.comboBox4.Items.Add("负数发票清单");
            this.comboBox4.Items.Add("正数发票废票清单");
            this.comboBox4.Items.Add("负数发票废票清单");
            this.comboBox4.SelectedIndex = 0;
        }

        private void InitHYJDC()
        {
            foreach (InvTypeInfo info2 in base.TaxCardInstance.get_StateInfo().InvTypeInfo)
            {
                if (info2.InvType == 11)
                {
                    this.lastRepDateHY = info2.LastRepDate;
                }
                if (info2.InvType == 12)
                {
                    this.lastRepDateJDC = info2.LastRepDate;
                }
                if (info2.InvType == 0x33)
                {
                    this.lastRepDateDZ = info2.LastRepDate;
                }
                if (info2.InvType == 0x29)
                {
                    this.lastRepDateJSFP = info2.LastRepDate;
                }
            }
        }

        private void Initial()
        {
            this.panel2 = this.xmlComponentLoader1.GetControlByName<Panel>("panel2");
            this.panel3 = this.xmlComponentLoader1.GetControlByName<Panel>("panel3");
            this.label2 = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("label2");
            this.label3 = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("label3");
            this.label4 = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("label4");
            this.comboBox2 = this.xmlComponentLoader1.GetControlByName<ToolStripComboBox>("comboBox2");
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox3 = this.xmlComponentLoader1.GetControlByName<ToolStripComboBox>("comboBox3");
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox4 = this.xmlComponentLoader1.GetControlByName<ToolStripComboBox>("comboBox4");
            this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.panelTop = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panelTop");
            this.labelRetriveNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelRetriveNum");
            this.labelAllotNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelAllotNum");
            this.labelMinusWasteNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelMinusWasteNum");
            this.labelMinusNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelMinusNum");
            this.labelEndStockNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelEndStockNum");
            this.labelPlusWasteNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelPlusWasteNum");
            this.labelPlusNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelPlusNum");
            this.labelReturnNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelReturnNum");
            this.labelBuyNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelBuyNum");
            this.labelEarlyStockNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelEarlyStockNum");
            this.customStyleDataGridReserve = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGridReserve");
            this.customStyleDataGridQD = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGridQD");
            this.tabControlType = this.xmlComponentLoader1.GetControlByName<AisinoTAB>("tabControlType");
            this.tabPage1 = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage1");
            this.labelReserve = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelReserve");
            this.labelAllot = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelAllot");
            this.labelRetrive = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelRetrive");
            this.labelAllot.Visible = false;
            this.labelAllotNum.Visible = false;
            this.labelRetrive.Visible = false;
            this.labelRetriveNum.Visible = false;
            this.toolStripMenu = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStripMenu");
            this.toolStripButtonRefresh = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonRefresh");
            this.toolStripButtonPrint = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonPrint");
            this.toolStripButtonPreView = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonPreView");
            this.toolStripButtonQuery = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonQuery");
            this.toolStripButtonExit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonExit");
            this.toolStripButtonFormat = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonFormat");
            this.toolStripButtonQuery.Click += new EventHandler(this.toolStripButtonQuery_Click);
            this.toolStripButtonPrint.Click += new EventHandler(this.toolStripButtonPrint_Click);
            this.toolStripButtonRefresh.Click += new EventHandler(this.toolStripButtonRefresh_Click);
            this.toolStripButtonPreView.Click += new EventHandler(this.toolStripButtonPreView_Click);
            this.toolStripButtonExit.Click += new EventHandler(this.toolStripButtonExit_Click);
            this.toolStripButtonFormat.Click += new EventHandler(this.toolStripButtonFormat_Click);
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBox3.SelectedIndexChanged += new EventHandler(this.comboBox3_SelectedIndexChanged);
            this.comboBox4.SelectedIndexChanged += new EventHandler(this.comboBox4_SelectedIndexChanged);
            this.tabControlType.SelectedIndexChanged += new EventHandler(this.tabControlType_SelectedIndexChanged);
            this.tabControlType.Dock = DockStyle.Fill;
            this.labelReserve.Dock = DockStyle.Top;
            this.panelTop.Dock = DockStyle.Top;
            this.customStyleDataGridQD.Dock = DockStyle.Top;
            this.customStyleDataGridQD.ScrollBars = ScrollBars.Both;
            this.customStyleDataGridReserve.Dock = DockStyle.Top;
            this.InsertGridColumn();
            this.InsertGridColumnQD();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FPQueryPrint));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x338, 0x296);
            this.xmlComponentLoader1.TabIndex = 1;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.FPQueryPrint\Aisino.Fwkp.Bsgl.FPQueryPrint.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x338, 0x296);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FPQueryPrint";
            base.set_TabText("发票资料查询打印");
            this.Text = "发票资料查询打印";
            base.ResumeLayout(false);
        }

        private void InitInvType()
        {
            try
            {
                this.m_InvTypeEntityList = this.m_commFun.GetInvTypeCollect();
                if (this.m_InvTypeEntityList == null)
                {
                    MessageManager.ShowMsgBox("INP-251303", new string[] { "获取发票种类失败" });
                }
                for (int i = 0; i < this.m_InvTypeEntityList.Count; i++)
                {
                    TabPage page = new TabPage(this.m_InvTypeEntityList[i].m_strInvName);
                    this.tabControlType.TabPages.Add(page);
                }
                if (this.tabControlType.TabPages.Count > 0)
                {
                    this.tabControlType.SelectedIndex = 0;
                }
                this.customStyleDataGridReserve.Parent = this.tabControlType.SelectedTab;
                this.customStyleDataGridQD.Parent = this.tabControlType.SelectedTab;
                this.labelReserve.Parent = this.tabControlType.SelectedTab;
                this.panelTop.Parent = this.tabControlType.SelectedTab;
            }
            catch (Exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { "获取发票种类失败" });
            }
        }

        private void InitMonth()
        {
            this.comboBox2.Items.Clear();
            this.thePageIndex = this.tabControlType.SelectedIndex;
            TabPage page = this.tabControlType.TabPages[this.thePageIndex];
            this.queryPrintBLL.GetCurrentDate();
            new List<int>();
            new List<int>();
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            if (page.Text.Equals("增值税专用发票") || page.Text.Equals("增值税普通发票"))
            {
                dictionary = this.taxcardEntityBLL.getYearMonthZP();
            }
            else if (page.Text.Equals("货物运输业增值税专用发票"))
            {
                dictionary = this.taxcardEntityBLL.getYearMonthHY();
            }
            else if (page.Text.Equals("机动车销售统一发票"))
            {
                dictionary = this.taxcardEntityBLL.getYearMonthJDC();
            }
            else if (page.Text.Equals("电子增值税普通发票"))
            {
                dictionary = this.taxcardEntityBLL.getYearMonthPTDZ();
            }
            else if (page.Text.Equals("增值税普通发票(卷票)"))
            {
                dictionary = this.taxcardEntityBLL.getYearMonthJSFP();
            }
            foreach (KeyValuePair<int, List<int>> pair in dictionary)
            {
                int key = pair.Key;
                foreach (int num in pair.Value)
                {
                    string item = "";
                    item = pair.Key.ToString() + "年" + num.ToString("D2") + "月";
                    this.comboBox2.Items.Add(item);
                }
            }
            if (this.comboBox2.Items.Count > 0)
            {
                this.comboBox2.SelectedIndex = this.comboBox2.Items.Count - 1;
            }
        }

        private void InitPeriod()
        {
            try
            {
                this.comboBox3.Items.Clear();
                this.thePageIndex = this.tabControlType.SelectedIndex;
                this.thePage = this.tabControlType.SelectedTab;
                string text = "";
                text = this.thePage.Text;
                int year = 0;
                int month = 0;
                string s = "";
                string str3 = "";
                s = this.comboBox2.SelectedItem.ToString().Substring(0, 4);
                str3 = this.comboBox2.SelectedItem.ToString().Substring(5, 2);
                year = int.Parse(s);
                month = int.Parse(str3);
                DateTime time = new DateTime(year, month, 1);
                int num3 = 0;
                if (text.Equals("增值税专用发票") || text.Equals("增值税普通发票"))
                {
                    int num4 = base.TaxCardInstance.get_LastRepDateYear();
                    int num5 = base.TaxCardInstance.get_LastRepDateMonth();
                    DateTime time2 = new DateTime(num4, num5, 1);
                    if (DateTime.Compare(time2, time) == 0)
                    {
                        num3 = base.TaxCardInstance.GetPeriodCount(0)[1];
                    }
                    else if (DateTime.Compare(time, time2.AddMonths(-1)) == 0)
                    {
                        num3 = base.TaxCardInstance.GetPeriodCount(0)[0];
                    }
                    else
                    {
                        num3 = 0;
                    }
                }
                else if (text.Equals("货物运输业增值税专用发票"))
                {
                    string lastRepDateHY = this.lastRepDateHY;
                    int num6 = -1;
                    int num7 = -1;
                    if ((lastRepDateHY.Length > 0) && lastRepDateHY.Contains("-"))
                    {
                        num7 = int.Parse(lastRepDateHY.Split(new char[] { '-' })[0]);
                        num6 = int.Parse(lastRepDateHY.Split(new char[] { '-' })[1]);
                    }
                    DateTime time3 = new DateTime(num7, num6, 1);
                    if (DateTime.Compare(time3, time) == 0)
                    {
                        num3 = base.TaxCardInstance.GetPeriodCount(11)[1];
                    }
                    else if (DateTime.Compare(time, time3.AddMonths(-1)) == 0)
                    {
                        num3 = base.TaxCardInstance.GetPeriodCount(11)[0];
                    }
                    else
                    {
                        num3 = 0;
                    }
                }
                else if (text.Equals("机动车销售统一发票"))
                {
                    string lastRepDateJDC = this.lastRepDateJDC;
                    int num8 = -1;
                    int num9 = -1;
                    if ((lastRepDateJDC.Length > 0) && lastRepDateJDC.Contains("-"))
                    {
                        num9 = int.Parse(lastRepDateJDC.Split(new char[] { '-' })[0]);
                        num8 = int.Parse(lastRepDateJDC.Split(new char[] { '-' })[1]);
                    }
                    DateTime time4 = new DateTime(num9, num8, 1);
                    if (DateTime.Compare(time4, time) == 0)
                    {
                        num3 = base.TaxCardInstance.GetPeriodCount(12)[1];
                    }
                    else if (DateTime.Compare(time, time4.AddMonths(-1)) == 0)
                    {
                        num3 = base.TaxCardInstance.GetPeriodCount(12)[0];
                    }
                    else
                    {
                        num3 = 0;
                    }
                }
                else if (text.Equals("电子增值税普通发票"))
                {
                    string lastRepDateDZ = this.lastRepDateDZ;
                    int num10 = -1;
                    int num11 = -1;
                    if ((lastRepDateDZ.Length > 0) && lastRepDateDZ.Contains("-"))
                    {
                        num11 = int.Parse(lastRepDateDZ.Split(new char[] { '-' })[0]);
                        num10 = int.Parse(lastRepDateDZ.Split(new char[] { '-' })[1]);
                    }
                    DateTime time5 = new DateTime(num11, num10, 1);
                    if (DateTime.Compare(time5, time) == 0)
                    {
                        num3 = base.TaxCardInstance.GetPeriodCount(0x33)[1];
                    }
                    else if (DateTime.Compare(time, time5.AddMonths(-1)) == 0)
                    {
                        num3 = base.TaxCardInstance.GetPeriodCount(0x33)[0];
                    }
                    else
                    {
                        num3 = 0;
                    }
                }
                else if (text.Equals("增值税普通发票(卷票)"))
                {
                    string lastRepDateJSFP = this.lastRepDateJSFP;
                    int num12 = -1;
                    int num13 = -1;
                    if ((lastRepDateJSFP.Length > 0) && lastRepDateJSFP.Contains("-"))
                    {
                        num13 = int.Parse(lastRepDateJSFP.Split(new char[] { '-' })[0]);
                        num12 = int.Parse(lastRepDateJSFP.Split(new char[] { '-' })[1]);
                    }
                    DateTime time6 = new DateTime(num13, num12, 1);
                    if (DateTime.Compare(time6, time) == 0)
                    {
                        num3 = base.TaxCardInstance.GetPeriodCount(0x29)[1];
                    }
                    else if (DateTime.Compare(time, time6.AddMonths(-1)) == 0)
                    {
                        num3 = base.TaxCardInstance.GetPeriodCount(0x29)[0];
                    }
                    else
                    {
                        num3 = 0;
                    }
                }
                this.comboBox3.Items.Add("本月累计");
                if (num3 > 0)
                {
                    for (int i = 0; i < num3; i++)
                    {
                        int num15 = i + 1;
                        this.comboBox3.Items.Add("第" + num15.ToString() + "期");
                    }
                }
                if (this.comboBox3.Items.Count > 0)
                {
                    this.comboBox3.SelectedIndex = this.comboBox3.Items.Count - 1;
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        private void InitQueryPrint()
        {
            ControlStyleUtil.SetToolStripStyle(this.toolStripMenu);
            this.toolStripButtonExit.Margin = new Padding(20, 1, 0, 2);
            this.label2.Alignment = ToolStripItemAlignment.Right;
            this.label3.Alignment = ToolStripItemAlignment.Right;
            this.label4.Alignment = ToolStripItemAlignment.Right;
            this.comboBox2.Alignment = ToolStripItemAlignment.Right;
            this.comboBox3.Alignment = ToolStripItemAlignment.Right;
            this.comboBox4.Alignment = ToolStripItemAlignment.Right;
            this.toolStripButtonQuery.Alignment = ToolStripItemAlignment.Right;
            this.panel2.Visible = false;
            this.InitHYJDC();
            this.InitInvType();
            this.InitMonth();
            this.InitPeriod();
            this.InitCXXX();
            this.InitColor();
            this.SetModel();
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

        protected virtual void InsertGridColumnQD()
        {
            try
            {
                this.customStyleDataGridQD.Rows.Clear();
                this.FPZL = new DataGridViewTextBoxColumn();
                this.LBDM = new DataGridViewTextBoxColumn();
                this.FPHM = new DataGridViewTextBoxColumn();
                this.KPRQ = new DataGridViewTextBoxColumn();
                this.GFSH = new DataGridViewTextBoxColumn();
                this.HJJE = new DataGridViewTextBoxColumn();
                this.HJSE = new DataGridViewTextBoxColumn();
                this.SLV = new DataGridViewTextBoxColumn();
                int index = 0;
                this.FPZL.HeaderText = "发票种类";
                this.FPZL.Name = this.strHeadQD[index];
                this.FPZL.DataPropertyName = this.strHeadQD[index++];
                this.FPZL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.FPZL.Width = 80;
                this.LBDM.HeaderText = "类别代码";
                this.LBDM.Name = this.strHeadQD[index];
                this.LBDM.DataPropertyName = this.strHeadQD[index++];
                this.LBDM.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.LBDM.Width = 80;
                this.FPHM.HeaderText = "发票号码";
                this.FPHM.Name = this.strHeadQD[index];
                this.FPHM.DataPropertyName = this.strHeadQD[index++];
                this.FPHM.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.FPHM.Width = 80;
                this.KPRQ.HeaderText = "开票日期";
                this.KPRQ.Name = this.strHeadQD[index];
                this.KPRQ.DataPropertyName = this.strHeadQD[index++];
                this.KPRQ.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.KPRQ.Width = 80;
                this.GFSH.HeaderText = "购方税号";
                this.GFSH.Name = this.strHeadQD[index];
                this.GFSH.DataPropertyName = this.strHeadQD[index++];
                this.GFSH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.GFSH.Width = 100;
                this.HJJE.HeaderText = "合计金额";
                this.HJJE.Name = this.strHeadQD[index];
                this.HJJE.DataPropertyName = this.strHeadQD[index++];
                this.HJJE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.HJJE.Width = 80;
                this.HJSE.HeaderText = "合计税额";
                this.HJSE.Name = this.strHeadQD[index];
                this.HJSE.DataPropertyName = this.strHeadQD[index++];
                this.HJSE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.HJSE.Width = 80;
                this.SLV.HeaderText = "税率";
                this.SLV.Name = this.strHeadQD[index];
                this.SLV.DataPropertyName = this.strHeadQD[index++];
                this.SLV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.SLV.Width = 80;
                int num2 = 0;
                this.customStyleDataGridQD.ColumnAdd(this.FPZL);
                this.customStyleDataGridQD.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridQD.ColumnAdd(this.LBDM);
                this.customStyleDataGridQD.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridQD.ColumnAdd(this.FPHM);
                this.customStyleDataGridQD.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridQD.ColumnAdd(this.KPRQ);
                this.customStyleDataGridQD.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridQD.ColumnAdd(this.GFSH);
                this.customStyleDataGridQD.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridQD.ColumnAdd(this.HJJE);
                this.customStyleDataGridQD.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridQD.ColumnAdd(this.HJSE);
                this.customStyleDataGridQD.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridQD.ColumnAdd(this.SLV);
                this.customStyleDataGridQD.SetColumnReadOnly(num2++, true);
                this.customStyleDataGridQD.AllowUserToAddRows = false;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void InvokePerformStep(object step)
        {
            int num = int.Parse(step.ToString());
            for (int i = 0; i < num; i++)
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

        private void PerformStep(object step)
        {
            this.InvokePerformStep(step);
        }

        public bool PrintTableHX(QueryPrintEntity _queryPrintEntity, bool _bShow)
        {
            bool flag = false;
            try
            {
                bool flag2 = false;
                if (!flag2)
                {
                    this.CreatePage(ref this.customStyleDataGridReserve, _queryPrintEntity);
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
                if (flag2)
                {
                    strTitle = this.m_InvTypeEntityList[this.tabControlType.SelectedIndex].m_strInvName + "金税设备资料统计";
                }
                else
                {
                    strTitle = _queryPrintEntity.m_strTitle;
                }
                list.Add(new PrinterItems("制表日期：" + base.TaxCardInstance.get_TaxClock().ToShortDateString(), HorizontalAlignment.Left));
                if (!flag2)
                {
                    if (_queryPrintEntity.m_nTaxPeriod == 0)
                    {
                        str = _queryPrintEntity.m_nMonth.ToString() + "月份";
                    }
                    else
                    {
                        str = string.Concat(new object[] { _queryPrintEntity.m_nMonth.ToString(), "月第", _queryPrintEntity.m_nTaxPeriod, "期" });
                    }
                }
                list.Add(new PrinterItems("所属期间：" + str, HorizontalAlignment.Left));
                if (!flag2)
                {
                    list.Add(new PrinterItems(_queryPrintEntity.m_strSubItem, HorizontalAlignment.Left));
                    list.Add(new PrinterItems(_queryPrintEntity.m_strItemDetail, HorizontalAlignment.Left));
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
            catch (Exception)
            {
                this.loger.Info("打印发票资料统计信息失败");
                MessageManager.ShowMsgBox("打印发票资料统计信息失败");
                return false;
            }
            return flag;
        }

        public bool PrintTableMX(QueryPrintEntity _queryPrintEntity, bool _bShow)
        {
            bool flag = false;
            try
            {
                this.m_objHead = this.m_commFun.GetTaxCardInfo();
                if (this.m_objHead.Length != 1)
                {
                    return false;
                }
                Dictionary<string, object> dictionary = this.m_objHead[0] as Dictionary<string, object>;
                List<PrinterItems> list = new List<PrinterItems>();
                List<PrinterItems> list2 = new List<PrinterItems> {
                    new PrinterItems("制表日期：" + base.TaxCardInstance.get_TaxClock().ToShortDateString(), HorizontalAlignment.Left),
                    new PrinterItems(_queryPrintEntity.m_strSubItem, HorizontalAlignment.Left),
                    new PrinterItems(_queryPrintEntity.m_strItemDetail, HorizontalAlignment.Left),
                    new PrinterItems("纳税人登记号：" + dictionary["QYBH"].ToString(), HorizontalAlignment.Left),
                    new PrinterItems("企业名称：" + base.TaxCardInstance.get_Corporation(), HorizontalAlignment.Left),
                    new PrinterItems("地址电话：" + dictionary["YYDZ"].ToString() + "  " + dictionary["DHHM"].ToString(), HorizontalAlignment.Left),
                    new PrinterItems("金额单位：元", HorizontalAlignment.Left),
                    new PrinterItems("", HorizontalAlignment.Left),
                    new PrinterItems("填表人：     抽样员：      录入员：      复核员：      审核员：      ", HorizontalAlignment.Left)
                };
                flag = this.m_queryPrintBLL.PrintTable(ref this.customStyleDataGridQD, _queryPrintEntity.m_strTitle, list, list2, _bShow);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox(exception.Message);
                return false;
            }
            return flag;
        }

        private void ProccessBarShow(object obj)
        {
            try
            {
                this.PerformStep(obj);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox(exception.ToString());
            }
        }

        public void ProcessStartThread(int value)
        {
            this.ProccessBarShow(value);
        }

        private void RefreshHZBData(QueryPrintEntity _queryPrintEntity)
        {
            new InvTypeEntity();
            this.m_invStatBLL.InitTaxMonthStatDataCXDY(_queryPrintEntity.m_nYear, _queryPrintEntity.m_nMonth, _queryPrintEntity.m_nTaxPeriod);
            this.CreateHead(_queryPrintEntity);
            this.CreateTitle(_queryPrintEntity);
            this.CreateDataGrid(ref this.customStyleDataGridReserve, _queryPrintEntity);
        }

        private void RefreshQDData(QueryPrintEntity _queryPrintEntity)
        {
            this.m_queryPrintBLL.MakeTable(ref this.customStyleDataGridQD, _queryPrintEntity);
        }

        private void resetSelTab()
        {
            this.customStyleDataGridReserve.Parent = this.tabControlType.SelectedTab;
            this.customStyleDataGridQD.Parent = this.tabControlType.SelectedTab;
            this.labelReserve.Parent = this.tabControlType.SelectedTab;
            this.panelTop.Parent = this.tabControlType.SelectedTab;
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
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void SetModel()
        {
            this.thePage = this.tabControlType.SelectedTab;
            string str = this.comboBox2.SelectedItem.ToString();
            string str2 = this.comboBox3.SelectedItem.ToString();
            int num = 0;
            string str3 = this.comboBox4.SelectedItem.ToString();
            int num2 = int.Parse(str.Substring(0, 4));
            int num3 = int.Parse(str.Substring(5, 2));
            if (str2.Equals("本月累计"))
            {
                num = 0;
            }
            else if (str2.Contains("第") && str2.Contains("期"))
            {
                num = int.Parse(str2.Replace("第", "").Replace("期", ""));
            }
            this.queryPrintEntity.m_nYear = num2;
            this.queryPrintEntity.m_nMonth = num3;
            this.queryPrintEntity.m_nTaxPeriod = num;
            this.queryPrintEntity.m_bPrint = false;
            this.queryPrintEntity.m_bShowDialog = true;
            if (this.thePage.Text.Equals("增值税专用发票"))
            {
                this.queryPrintEntity.m_invType = INV_TYPE.INV_SPECIAL;
                this.queryPrintEntity.m_strTitle = this.thePage.Text + "明细表";
            }
            else if (this.thePage.Text.Equals("增值税普通发票"))
            {
                this.queryPrintEntity.m_invType = INV_TYPE.INV_COMMON;
                this.queryPrintEntity.m_strTitle = this.thePage.Text + "明细表";
            }
            else if (this.thePage.Text.Equals("货物运输业增值税专用发票"))
            {
                this.queryPrintEntity.m_invType = INV_TYPE.INV_TRANSPORTATION;
                this.queryPrintEntity.m_strTitle = this.thePage.Text + "明细表";
            }
            else if (this.thePage.Text.Equals("电子增值税普通发票"))
            {
                this.queryPrintEntity.m_invType = INV_TYPE.INV_PTDZ;
                this.queryPrintEntity.m_strTitle = this.thePage.Text + "明细表";
            }
            else if (this.thePage.Text.Equals("机动车销售统一发票"))
            {
                this.queryPrintEntity.m_invType = INV_TYPE.INV_VEHICLESALES;
                this.queryPrintEntity.m_strTitle = this.thePage.Text + "明细表";
            }
            else if (this.thePage.Text.Equals("增值税普通发票(卷票)"))
            {
                this.queryPrintEntity.m_invType = INV_TYPE.INV_JSFP;
                this.queryPrintEntity.m_strTitle = this.thePage.Text + "明细表";
            }
            string str4 = "";
            if (str3.Equals("增值税发票汇总表"))
            {
                this.queryPrintEntity.m_itemAction = ITEM_ACTION.ITEM_TOTAL;
                this.queryPrintEntity.m_strTitle = this.thePage.Text + "汇总表";
                this.queryPrintEntity.m_strSubItem = this.thePage.Text + "统计表 1-01";
                this.queryPrintEntity.m_strItemDetail = string.Concat(new object[] { "增值税发票汇总表(", this.queryPrintEntity.m_nYear, "年", this.queryPrintEntity.m_nMonth, "月)" });
            }
            else if (str3.Equals("正数发票清单"))
            {
                this.queryPrintEntity.m_itemAction = ITEM_ACTION.ITEM_PLUS;
                str4 = string.Concat(new object[] { "正数发票清单(", this.queryPrintEntity.m_nYear, "年", this.queryPrintEntity.m_nMonth, "月)" });
                this.queryPrintEntity.m_strSubItem = this.thePage.Text + "统计表  1-02";
            }
            else if (str3.Equals("负数发票清单"))
            {
                this.queryPrintEntity.m_itemAction = ITEM_ACTION.ITEM_MINUS;
                str4 = string.Concat(new object[] { "负数发票清单(", this.queryPrintEntity.m_nYear, "年", this.queryPrintEntity.m_nMonth, "月)" });
                this.queryPrintEntity.m_strSubItem = this.thePage.Text + "统计表  1-03";
            }
            else if (str3.Equals("正数发票废票清单"))
            {
                this.queryPrintEntity.m_itemAction = ITEM_ACTION.ITEM_PLUS_WASTE;
                str4 = string.Concat(new object[] { "正数发票废票清单(", this.queryPrintEntity.m_nYear, "年", this.queryPrintEntity.m_nMonth, "月)" });
                this.queryPrintEntity.m_strSubItem = this.thePage.Text + "统计表  1-04";
            }
            else if (str3.Equals("负数发票废票清单"))
            {
                this.queryPrintEntity.m_itemAction = ITEM_ACTION.ITEM_MINUS_WASTE;
                str4 = string.Concat(new object[] { "负数发票废票清单(", this.queryPrintEntity.m_nYear, "年", this.queryPrintEntity.m_nMonth, "月)" });
                this.queryPrintEntity.m_strSubItem = this.thePage.Text + "统计表  1-05";
            }
            if (!str3.Equals("增值税发票汇总表"))
            {
                if (this.queryPrintEntity.m_nTaxPeriod != 0)
                {
                    this.queryPrintEntity.m_strItemDetail = string.Concat(new object[] { str4, "第", this.queryPrintEntity.m_nTaxPeriod, "期" });
                }
                else
                {
                    this.queryPrintEntity.m_strItemDetail = str4;
                }
            }
            if (this.progressBar == null)
            {
                this.progressBar = new FPProgressBar();
            }
            this.progressBar.fpxf_progressBar.Value = 1;
            this.progressBar.SetTip("正在查询数据...", "请等待任务完成", "发票资料查询打印");
            this.progressBar.Show();
            this.progressBar.Refresh();
            this.ProcessStartThread(0xbb8);
            this.progressBar.Refresh();
            this.ProcessStartThread(0x1388);
            this.progressBar.Refresh();
            if (str3.Equals("增值税发票汇总表"))
            {
                this.panelTop.Visible = true;
                this.labelReserve.Visible = true;
                this.customStyleDataGridReserve.Visible = true;
                this.customStyleDataGridQD.Visible = false;
                this.RefreshHZBData(this.queryPrintEntity);
            }
            else
            {
                this.panelTop.Visible = false;
                this.labelReserve.Visible = false;
                this.customStyleDataGridReserve.Visible = false;
                this.customStyleDataGridQD.Visible = true;
                this.RefreshQDData(this.queryPrintEntity);
            }
            this.ProcessStartThread(0x7d0);
            this.progressBar.Refresh();
            if (this.progressBar != null)
            {
                this.progressBar.Close();
                this.progressBar = null;
            }
        }

        private void tabControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.initFinished)
            {
                this._isTabIndexChanged = true;
                this.InitMonth();
                this.InitPeriod();
                this.resetSelTab();
                this.SetModel();
                this._isTabIndexChanged = false;
            }
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolStripButtonFormat_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBox4.SelectedItem.Equals("增值税发票汇总表"))
                {
                    this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGridReserve").SetColumnStyles(this.xmlComponentLoader1.get_XMLPath(), this);
                }
                else
                {
                    this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGridQD").SetColumnStyles(this.xmlComponentLoader1.get_XMLPath(), this);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void toolStripButtonPreView_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBox4.SelectedItem.ToString().Equals("增值税发票汇总表"))
                {
                    this.PrintTableHX(this.queryPrintEntity, true);
                }
                else
                {
                    this.PrintTableMX(this.queryPrintEntity, true);
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            new FPSerialPrint { ShowInTaskbar = true, StartPosition = FormStartPosition.CenterScreen }.ShowDialog();
        }

        private void toolStripButtonQuery_Click(object sender, EventArgs e)
        {
            this.SetModel();
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            if (this.initFinished)
            {
                this.resetSelTab();
                this.SetModel();
            }
        }
    }
}

