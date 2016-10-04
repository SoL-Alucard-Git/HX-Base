namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.PrintGrid;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class InvoiceReportForm : DockForm
    {
        private IContainer components;
        private CustomStyleDataGrid customStyleDataGridDetail = new CustomStyleDataGrid();
        private Dictionary<string, DataTable> dicData = new Dictionary<string, DataTable>();
        private string lastRepDateHY = "";
        private string lastRepDateJDC = "";
        private ILog log = LogUtil.GetLogger<InvoiceReportForm>();
        private DateTime m_dtCurrent;
        private InvoiceReportBLL m_InvoiceReportBLL = new InvoiceReportBLL();
        private int m_nMonth;
        private int m_nYear;
        private FPProgressBar progressBar = new FPProgressBar();
        private ToolStripComboBox toolComboBox_yuefen;
        private ToolStripLabel toolLabel_yuefen;
        private ToolStripButton toolStripButtonAdd = new ToolStripButton();
        private ToolStripButton toolStripButtonDelete = new ToolStripButton();
        private ToolStripButton toolStripButtonExit = new ToolStripButton();
        private ToolStripButton toolStripButtonForm = new ToolStripButton();
        private ToolStripButton toolStripButtonPrint = new ToolStripButton();
        private ToolStripButton toolStripButtonQuery = new ToolStripButton();
        private ToolStripButton toolStripButtonRefresh = new ToolStripButton();
        private ToolStripButton toolStripButtonSum = new ToolStripButton();
        private ToolStrip toolStripMenu;
        private XmlComponentLoader xmlComponentLoader1;

        public InvoiceReportForm()
        {
            this.Initial();
            this.customStyleDataGridDetail.Dock = DockStyle.Fill;
            ControlStyleUtil.SetToolStripStyle(this.toolStripMenu);
            this.toolStripButtonExit.Margin = new Padding(20, 1, 0, 2);
            this.toolLabel_yuefen.Alignment = ToolStripItemAlignment.Right;
            this.toolComboBox_yuefen.Alignment = ToolStripItemAlignment.Right;
            this.toolComboBox_yuefen.DropDownStyle = ComboBoxStyle.DropDownList;
            this.m_dtCurrent = this.m_InvoiceReportBLL.GetDateTime();
            this.m_nYear = this.m_dtCurrent.Year;
            this.m_nMonth = this.m_dtCurrent.Month;
            object text = this.Text;
            this.Text = string.Concat(new object[] { text, "(", this.m_dtCurrent.Year, "年", this.m_dtCurrent.Month, "月)" });
            base.set_TabText(this.Text);
            this.InitialControl();
            this.InitDicData();
            this.RefreshData();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string getMonth(string strYearMonth)
        {
            return int.Parse(strYearMonth.Substring(5, 2)).ToString();
        }

        private void InitDicData()
        {
            string key = "";
            int num = 0;
            int num2 = 0;
            if (this.progressBar == null)
            {
                this.progressBar = new FPProgressBar();
            }
            this.progressBar.fpxf_progressBar.Value = 1;
            this.progressBar.SetTip("正在查询数据...", "请等待任务完成", "发票领用存月报表");
            this.progressBar.Show();
            this.progressBar.Refresh();
            this.ProcessStartThread(0xbb8);
            this.progressBar.Refresh();
            this.ProcessStartThread(0x1388);
            this.progressBar.Refresh();
            for (int i = 0; i < this.toolComboBox_yuefen.Items.Count; i++)
            {
                key = this.toolComboBox_yuefen.Items[i].ToString();
                num = int.Parse(key.Substring(0, 4));
                num2 = int.Parse(key.Substring(5, 2));
                DataTable table = new DataTable();
                table = this.m_InvoiceReportBLL.CreateDataTable(num, num2);
                this.dicData.Add(key, table);
            }
            this.ProcessStartThread(0x7d0);
            this.progressBar.Refresh();
            if (this.progressBar != null)
            {
                this.progressBar.Close();
                this.progressBar = null;
            }
        }

        private void Initial()
        {
            this.InitializeComponent();
            this.customStyleDataGridDetail = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGridDetail");
            this.customStyleDataGridDetail.ReadOnly = true;
            this.customStyleDataGridDetail.AllowUserToDeleteRows = false;
            this.customStyleDataGridDetail.set_AllowUserToResizeRows(false);
            this.customStyleDataGridDetail.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.DisableResizing);
            this.toolStripMenu = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStripMenu");
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
            this.toolStripButtonRefresh.Click += new EventHandler(this.toolStripButtonRefresh_Click);
            this.toolComboBox_yuefen = this.xmlComponentLoader1.GetControlByName<ToolStripComboBox>("toolComboBox_yuefen");
            this.toolLabel_yuefen = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolLabel_yuefen");
            this.toolComboBox_yuefen.SelectedIndexChanged += new EventHandler(this.toolComboBox_yuefen_SelectedIndexChanged);
        }

        private void InitialControl()
        {
            this.toolComboBox_yuefen.Items.Clear();
            DateTime time = new DateTime();
            string str = base.TaxCardInstance.get_CardEffectDate();
            if (string.IsNullOrEmpty(str))
            {
                time = base.TaxCardInstance.get_TaxClock();
            }
            else if (str.Length == 6)
            {
                int year = Convert.ToInt32(str.Substring(0, 4));
                int month = Convert.ToInt32(str.Substring(4, 2));
                time = new DateTime(year, month, 1);
            }
            for (DateTime time2 = new DateTime(this.m_nYear, this.m_nMonth, 1); DateTime.Compare(time, time2) <= 0; time2 = time2.AddMonths(-1))
            {
                string item = "";
                item = time2.ToString("yyyy年MM月");
                this.toolComboBox_yuefen.Items.Add(item);
            }
            if (this.toolComboBox_yuefen.Items.Count > 0)
            {
                this.toolComboBox_yuefen.SelectedIndex = 0;
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(InvoiceReportForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "InvoiceReportForm";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.InvoiceReportForm\Aisino.Fwkp.Bsgl.InvoiceReportForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "InvoiceReportForm";
            base.set_TabText("增值税发票领用存月报表");
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "增值税发票领用存月报表";
            base.ResumeLayout(false);
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

        private bool IsNumeric(string strInput)
        {
            if ((strInput != null) && (strInput.Length > 0))
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                foreach (byte num in encoding.GetBytes(strInput))
                {
                    if ((num >= 0x30) && (num <= 0x39))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void PerformStep(object step)
        {
            this.InvokePerformStep(step);
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

        public void RefreshData()
        {
            try
            {
                if (this.IsNumeric(this.getMonth(this.toolComboBox_yuefen.Text)))
                {
                    this.m_nYear = int.Parse(this.toolComboBox_yuefen.Text.Substring(0, 4));
                    this.m_nMonth = int.Parse(this.toolComboBox_yuefen.Text.Substring(5, 2));
                    string text = this.Text;
                    text = text.Replace(text.Substring(text.IndexOf("(") + 1, (text.IndexOf(")") - text.IndexOf("(")) - 1), this.m_nYear.ToString() + "年" + this.m_nMonth.ToString("D2") + "月");
                    this.Text = text;
                    base.set_TabText(this.Text);
                    DataTable table = null;
                    if (this.dicData.ContainsKey(this.toolComboBox_yuefen.Text))
                    {
                        table = this.dicData[this.toolComboBox_yuefen.Text];
                    }
                    if (table != null)
                    {
                        this.customStyleDataGridDetail.ReadOnly = true;
                        this.customStyleDataGridDetail.AllowUserToAddRows = false;
                        this.customStyleDataGridDetail.DataSource = table;
                        this.Refresh();
                    }
                }
            }
            catch (Exception exception)
            {
                this.log.Debug(exception.Message);
                MessageManager.ShowMsgBox("INP-253208", new string[] { exception.Message });
            }
        }

        private void toolComboBox_yuefen_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolStripButtonForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.customStyleDataGridDetail.SetColumnStyles(this.xmlComponentLoader1.get_XMLPath(), this);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-253208", new string[] { exception.Message });
            }
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                List<PrinterItems> list = new List<PrinterItems>();
                List<PrinterItems> list2 = new List<PrinterItems>();
                object[] taxCardInfo = new CommFun().GetTaxCardInfo();
                if (taxCardInfo.Length == 1)
                {
                    Dictionary<string, object> dictionary = taxCardInfo[0] as Dictionary<string, object>;
                    if (dictionary.ContainsKey("QYMC"))
                    {
                        string str = base.TaxCardInstance.get_Corporation();
                        string str2 = "资料所属期：";
                        string str3 = str2;
                        str2 = (((str3 + this.m_nYear.ToString() + "年" + this.m_nMonth.ToString() + "月") + "                ") + "填报日期：" + DateTime.Now.Date.ToShortDateString()) + "                " + "发票单位：份";
                        list.Add(new PrinterItems(str2, HorizontalAlignment.Left));
                        str2 = "名称(章)：";
                        str2 = str2 + str;
                        list.Add(new PrinterItems(str2, HorizontalAlignment.Left));
                        list2.Add(new PrinterItems("\r\n" + str2, HorizontalAlignment.Left));
                        this.m_InvoiceReportBLL.PrintTable(ref this.customStyleDataGridDetail, "增值税发票领用存月报表", list, list2);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-253208", new string[] { exception.Message });
            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.RefreshData();
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-253208", new string[] { exception.Message });
            }
        }
    }
}

