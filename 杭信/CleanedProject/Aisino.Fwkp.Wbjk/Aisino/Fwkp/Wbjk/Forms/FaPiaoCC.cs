namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FaPiaoCC : DockForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoBTN btnOK;
        private AisinoBTN btnQuit;
        private AisinoCHK checkBox1;
        private AisinoCHK checkBox2 = new AisinoCHK();
        private AisinoCMB comboBoxDJZL;
        private IContainer components = null;
        private AisinoDataSet dataSet = null;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private DateTime EndDate;
        private FileControl fileControl1;
        private FPCCbll fpccBLL = new FPCCbll();
        private string FpType;
        private ILog log = LogUtil.GetLogger<FaPiaoCC>();
        private DateTime m_dtLastRepDate;
        private int m_nTotalCount = 0;
        private int nMonth = 1;
        private int nYear = DateTime.Now.Year;
        private DateTime StartDate;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonExit;
        private ToolStripButton toolStripButtonPrint;
        private ToolStripButton toolStripButtonStyle;
        private XmlComponentLoader xmlComponentLoader1;

        public FaPiaoCC()
        {
            this.Initialize();
            this.m_dtLastRepDate = this.fpccBLL.GetLastRepDate();
            this.nYear = this.fpccBLL.GetTaxCardDate().Year;
            this.nMonth = this.fpccBLL.GetTaxCardDate().Month;
        }

        private void aisinoDataGrid1_DataGridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string name = this.aisinoDataGrid1.get_Columns()[e.ColumnIndex].Name;
            if (name != null)
            {
                if (!(name == "KPZT"))
                {
                    if (name == "DJZT")
                    {
                        e.Value = ShowString.ShowDJZT(e.Value.ToString());
                    }
                    else if (name == "FPZL")
                    {
                        e.Value = ShowString.ShowFPZL(e.Value.ToString());
                    }
                    else if (name == "QDBZ")
                    {
                        e.Value = ShowString.ShowBool(e.Value.ToString());
                    }
                    else if (name == "ZFBZ")
                    {
                        e.Value = ShowString.ShowBool(e.Value.ToString());
                    }
                }
                else
                {
                    e.Value = ShowString.ShowKPZT(e.Value.ToString());
                }
            }
        }

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            string str = e.get_PageNO().ToString();
            PropertyUtil.SetValue("WBJK_FPCC_DATAGRID", str);
            this.dataSet = this.fpccBLL.QueryFaPiao(this.StartDate, this.EndDate, this.FpType, e.get_PageSize(), e.get_PageNO());
            this.aisinoDataGrid1.set_DataSource(this.dataSet);
            int count = this.aisinoDataGrid1.get_Rows().Count;
            for (int i = 0; i < count; i++)
            {
                string str2 = this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value.ToString();
                if ((((str2 != null) && (str2 != "")) && ((str2 != "免税") && (str2 != "中外合作油气田"))) && (str2 != "多税率"))
                {
                    str2 = ((Convert.ToDouble(str2) * 100.0)).ToString() + "%";
                    this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = str2;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.dataSet == null) || (this.m_nTotalCount <= 0))
                {
                    MessageManager.ShowMsgBox("INP-275101");
                }
                else
                {
                    int count = this.aisinoDataGrid1.get_SelectedRows().Count;
                    int nTotalCount = this.m_nTotalCount;
                    List<InvoiceData> invoiceDataList = new List<InvoiceData>();
                    List<InvoiceData> list2 = new List<InvoiceData>();
                    for (int i = count - 1; i >= 0; i--)
                    {
                        InvoiceData item = new InvoiceData {
                            m_strInvType = this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPZL"].Value.ToString(),
                            m_strInvCode = this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPDM"].Value.ToString(),
                            m_strInvNum = this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPHM"].Value.ToString()
                        };
                        list2.Add(item);
                        invoiceDataList.Add(item);
                    }
                    for (int j = 0; j < invoiceDataList.Count; j++)
                    {
                        int result = 0;
                        int.TryParse(invoiceDataList[j].m_strInvNum, out result);
                        for (int k = j; k < invoiceDataList.Count; k++)
                        {
                            int num7 = 0;
                            int.TryParse(invoiceDataList[k].m_strInvNum, out num7);
                            if (result > num7)
                            {
                                InvoiceData data2 = invoiceDataList[k];
                                invoiceDataList[k] = invoiceDataList[j];
                                invoiceDataList[j] = data2;
                            }
                        }
                    }
                    bool exportAll = this.checkBox2.Checked;
                    if (!(exportAll || (count > 0)))
                    {
                        MessageManager.ShowMsgBox("INP-275104", new string[] { "没有选中任何记录" });
                    }
                    else
                    {
                        PropValue.InvExportTxtPath = this.fileControl1.get_TextBoxFile().Text.Trim();
                        PropValue.CheckBoxKqd = this.checkBox1.Checked;
                        int num8 = this.fpccBLL.ExportInvoiceToTxt(invoiceDataList, exportAll);
                        MessageManager.ShowMsgBox("INP-275102", "", new string[] { num8.ToString() });
                    }
                }
            }
            catch (Exception exception)
            {
                if (((((exception.ToString().Contains("路径格式错误") || exception.ToString().Contains("没有设置传出文件路径")) || (exception.ToString().Contains("未能找到路径中的某个部分") || exception.ToString().Contains("未能找到路径"))) || ((exception.ToString().Contains("不支持给定路径") || exception.ToString().Contains("ArgumentNullException")) || (exception.ToString().Contains("SecurityException") || exception.ToString().Contains("ArgumentException")))) || (exception.ToString().Contains("UnauthorizedAccessException") || exception.ToString().Contains("PathTooLongException"))) || exception.ToString().Contains("NotSupportedException"))
                {
                    MessageManager.ShowMsgBox("A320");
                }
                else if (exception.ToString().Contains("超时"))
                {
                    this.log.Error(exception.ToString());
                }
                else
                {
                    HandleException.HandleError(exception);
                }
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.StartDate = Convert.ToDateTime(this.dateTimePicker1.Text);
                this.EndDate = Convert.ToDateTime(this.dateTimePicker2.Text).AddDays(1.0);
                this.FpType = this.comboBoxDJZL.SelectedValue.ToString();
                if (this.aisinoDataGrid1.get_Rows().Count > 0)
                {
                    int num = this.aisinoDataGrid1.get_Rows().Count;
                    while (num-- > 0)
                    {
                        this.aisinoDataGrid1.get_Rows().RemoveAt(0);
                    }
                }
                int result = 1;
                int.TryParse(PropertyUtil.GetValue("WBJK_FPCC_DATAGRID"), out result);
                this.fpccBLL.CurrentPage = result;
                this.dataSet = this.fpccBLL.QueryFaPiao(this.StartDate, this.EndDate, this.FpType, this.fpccBLL.Pagesize, this.fpccBLL.CurrentPage);
                this.aisinoDataGrid1.set_DataSource(this.dataSet);
                this.m_nTotalCount = this.dataSet.get_AllRows();
                if ((this.FpType == "f") || (this.FpType == "j"))
                {
                    this.checkBox1.Visible = false;
                }
                else
                {
                    this.checkBox1.Visible = true;
                }
                int count = this.aisinoDataGrid1.get_Rows().Count;
                for (int i = 0; i < count; i++)
                {
                    string str = this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value.ToString();
                    if ((((str != null) && (str != "")) && ((str != "免税") && (str != "中外合作油气田"))) && (str != "多税率"))
                    {
                        str = ((Convert.ToDouble(str) * 100.0)).ToString() + "%";
                        this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = str;
                    }
                }
            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("超时"))
                {
                    this.log.Error(exception.ToString());
                }
                else
                {
                    HandleException.HandleError(exception);
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

        private void FaPiaoCC_Load(object sender, EventArgs e)
        {
            try
            {
                int num;
                this.fileControl1.set_FileFilter("文本文件(*.txt)|*.txt");
                this.fileControl1.get_TextBoxFile().Text = PropValue.InvExportTxtPath;
                this.fileControl1.set_CheckFileExists(false);
                this.checkBox1.Checked = PropValue.CheckBoxKqd;
                this.comboBoxDJZL.DataSource = CbbXmlBind.ReadXmlNode("InvType", true);
                this.comboBoxDJZL.DisplayMember = "Value";
                this.comboBoxDJZL.ValueMember = "Key";
                if (RegisterManager.CheckRegFile("JIJS"))
                {
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    if (card.get_QYLX().ISPTFP && card.get_QYLX().ISZYFP)
                    {
                        this.comboBoxDJZL.SelectedIndex = 2;
                        this.comboBoxDJZL.Enabled = false;
                    }
                    else if (card.get_QYLX().ISPTFP)
                    {
                        this.btnOK.Enabled = false;
                    }
                    else if (card.get_QYLX().ISZYFP)
                    {
                        this.comboBoxDJZL.SelectedIndex = 1;
                        this.comboBoxDJZL.Enabled = false;
                    }
                    else
                    {
                        this.btnOK.Enabled = false;
                    }
                }
                if ((base.TaxCardInstance.get_QYLX().ISPTFP || base.TaxCardInstance.get_QYLX().ISZYFP) && (this.nMonth != this.m_dtLastRepDate.Month))
                {
                    this.nYear = this.m_dtLastRepDate.Year;
                    this.nMonth = this.m_dtLastRepDate.Month;
                }
                this.dateTimePicker1.Text = this.nYear.ToString() + "-" + this.nMonth.ToString() + "-01";
                if (this.nMonth == 12)
                {
                    num = this.nYear + 1;
                    this.dateTimePicker2.Text = Convert.ToDateTime(num.ToString() + "-01-01").AddDays(-1.0).ToShortDateString();
                }
                else
                {
                    num = this.nMonth + 1;
                    this.dateTimePicker2.Text = Convert.ToString(Convert.ToDateTime(this.nYear.ToString() + "-" + num.ToString() + "-01").AddDays(-1.0));
                }
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "单据编号");
                item.Add("Property", "XSDJBH");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票种类");
                item.Add("Property", "FPZL");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票代码");
                item.Add("Property", "FPDM");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票号码");
                item.Add("Property", "FPHM");
                item.Add("Type", "Text");
                item.Add("Width", "150");
                item.Add("Align", "MiddleRight");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "作废标志");
                item.Add("Property", "ZFBZ");
                item.Add("Type", "Text");
                item.Add("Width", "80");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "清单标志");
                item.Add("Property", "QDBZ");
                item.Add("Type", "Text");
                item.Add("Width", "80");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "税率");
                item.Add("Property", "SLV");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "合计金额");
                item.Add("Property", "HJJE");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleCenter");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开票日期");
                item.Add("Property", "KPRQ");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                this.aisinoDataGrid1.set_ColumeHead(list);
                this.aisinoDataGrid1.get_Columns()["KPRQ"].DefaultCellStyle.Format = "yyyy-MM-dd";
                this.aisinoDataGrid1.get_Columns()["HJJE"].DefaultCellStyle.Format = "0.00";
                DataGridViewColumn column = this.aisinoDataGrid1.get_Columns()["FPHM"];
                if (null != column)
                {
                    column.DefaultCellStyle.Format = new string('0', 8);
                }
                this.dateTimePicker1_CloseUp(sender, e);
                this.toolStripButtonPrint.Visible = false;
            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("超时"))
                {
                    this.log.Error(exception.ToString());
                }
                else
                {
                    HandleException.HandleError(exception);
                }
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.checkBox1 = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox1");
            this.checkBox2 = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox2");
            this.comboBoxDJZL = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxDJZL");
            this.fileControl1 = this.xmlComponentLoader1.GetControlByName<FileControl>("fileControl1");
            this.btnQuit = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnQuit");
            this.btnQuit.Click += new EventHandler(this.btnQuit_Click);
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.dateTimePicker2 = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dateTimePicker2");
            this.dateTimePicker1 = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dateTimePicker1");
            this.aisinoDataGrid1.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent));
            this.aisinoDataGrid1.add_DataGridCellFormatting(new EventHandler<DataGridViewCellFormattingEventArgs>(this.aisinoDataGrid1_DataGridCellFormatting));
            this.comboBoxDJZL.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxDJZL.SelectionChangeCommitted += new EventHandler(this.dateTimePicker1_CloseUp);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.dateTimePicker2.CloseUp += new EventHandler(this.dateTimePicker1_CloseUp);
            this.dateTimePicker1.CloseUp += new EventHandler(this.dateTimePicker1_CloseUp);
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripButtonExit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonExit");
            this.toolStripButtonExit.Click += new EventHandler(this.toolStripButtonExit_Click);
            this.toolStripButtonPrint = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonPrint");
            this.toolStripButtonPrint.Click += new EventHandler(this.toolStripButtonPrint_Click);
            this.toolStripButtonStyle = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonStyle");
            this.toolStripButtonStyle.Click += new EventHandler(this.toolStripButtonStyle_Click);
            this.aisinoDataGrid1.set_ReadOnly(true);
            this.aisinoDataGrid1.get_DataGrid().AllowUserToDeleteRows = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FaPiaoCC));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "发票文本传出";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.FaPiaoCC\Aisino.Fwkp.Wbjk.FaPiaoCC.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "FaPiaoCC";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "发票文本传出";
            base.Load += new EventHandler(this.FaPiaoCC_Load);
            base.ResumeLayout(false);
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.aisinoDataGrid1.Print("发票文本传出", this, null, null, true);
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void toolStripButtonStyle_Click(object sender, EventArgs e)
        {
            try
            {
                this.aisinoDataGrid1.SetColumnsStyle(this.xmlComponentLoader1.get_XMLPath(), this);
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }
    }
}

