namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class DJXG : BaseForm
    {
        private ToolStripButton AddGrid_toolStripButton;
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoDataGrid aisinoDataGrid2;
        private StringBuilder biaoti;
        private SaleBillCtrl billBL;
        private AisinoBTN btnGetGF;
        private AisinoBTN btnSP;
        private AisinoBTN button1;
        private AisinoCMB comboBoxDJZL;
        private AisinoCMB comboBoxSlv;
        private AisinoCMB comboBoxYF;
        private IContainer components;
        private AisinoDataSet dataSet;
        private DateTimePicker dateTimePicker1;
        private DJXGdal djxgBLL;
        private string Global_DJMonth;
        private string Global_DJType;
        private string Global_DJYear;
        private bool Global_QuitJianYan;
        private string Global_VersionName;
        private AisinoGRP groupBox1;
        private string KPZT;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private ILog log;
        private XSDJ_MXModel mx;
        private StatusStrip statusStrip1;
        private AisinoTAB tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private AisinoTXT textBox1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnCheck;
        private ToolStripButton toolStripBtnDJAdd;
        private ToolStripButton toolStripBtnDJDel;
        private ToolStripButton toolStripBtnEdit;
        private ToolStripButton toolStripBtnHSJG;
        private ToolStripButton toolStripBtnMXAdd;
        private ToolStripButton toolStripBtnMXDel;
        private ToolStripButton toolStripBtnQuit;
        private ToolStripButton toolStripBtnTJ;
        private ToolStripButton toolStripBtnZK;
        private ToolStripButton toolStripBtnZuoFei;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton6;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private bool UseYear;
        private XmlComponentLoader xmlComponentLoader1;
        private XSDJModel xsdj;
        private string XSDJBH;

        public DJXG(string DJMonth, string DJType, bool QuitJiaoYan)
        {
            this.djxgBLL = new DJXGdal();
            this.log = LogUtil.GetLogger<DJXG>();
            this.billBL = SaleBillCtrl.Instance;
            this.dataSet = null;
            this.xsdj = new XSDJModel();
            this.mx = new XSDJ_MXModel();
            this.UseYear = true;
            this.Global_QuitJianYan = false;
            this.biaoti = new StringBuilder();
            this.XSDJBH = "";
            this.KPZT = "";
            this.components = null;
            this.Global_DJMonth = DJMonth;
            this.Global_DJType = DJType;
            this.Global_QuitJianYan = QuitJiaoYan;
            this.Initialize();
            this.aisinoDataGrid1.DoubleClick += new EventHandler(this.aisinoDataGrid1_DoubleClick);
        }

        public DJXG(string DJYear, string DJMonth, string DJType, bool QuitJiaoYan)
        {
            this.djxgBLL = new DJXGdal();
            this.log = LogUtil.GetLogger<DJXG>();
            this.billBL = SaleBillCtrl.Instance;
            this.dataSet = null;
            this.xsdj = new XSDJModel();
            this.mx = new XSDJ_MXModel();
            this.UseYear = true;
            this.Global_QuitJianYan = false;
            this.biaoti = new StringBuilder();
            this.XSDJBH = "";
            this.KPZT = "";
            this.components = null;
            this.Global_DJYear = DJYear;
            this.Global_DJMonth = DJMonth;
            this.Global_DJType = DJType;
            this.Global_QuitJianYan = QuitJiaoYan;
            this.Initialize();
            this.aisinoDataGrid1.DoubleClick += new EventHandler(this.aisinoDataGrid1_DoubleClick);
        }

        private void aisinoDataGrid1_DataGridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (this.aisinoDataGrid1.get_Columns()[e.ColumnIndex].Name)
            {
                case "SLV":
                {
                    double result = 0.0;
                    if (double.TryParse(e.Value.ToString(), out result))
                    {
                        e.Value = Convert.ToString((double) (result * 100.0)) + "%";
                    }
                    break;
                }
            }
        }

        private void aisinoDataGrid1_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                try
                {
                    string bH = e.get_CurrentRow().Cells["BH"].Value.ToString();
                    if (((this.Global_DJType.CompareTo("a") == 0) || (this.Global_DJType.CompareTo("c") == 0)) || (this.Global_DJType.CompareTo("s") == 0))
                    {
                        XSDJEdite edite = new XSDJEdite(bH);
                        if (edite.ShowDialog() == DialogResult.OK)
                        {
                            if (this.UseYear)
                            {
                                this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                            }
                            else
                            {
                                this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                            }
                        }
                    }
                    else if (this.Global_DJType.CompareTo("f") == 0)
                    {
                        HYFPtiankai_new _new = new HYFPtiankai_new(11, bH, "xg");
                        if (_new.ShowDialog() == DialogResult.OK)
                        {
                            if (this.UseYear)
                            {
                                this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                            }
                            else
                            {
                                this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                            }
                        }
                    }
                    else if (this.Global_DJType.CompareTo("j") == 0)
                    {
                        JDCFPtiankai_new_new _new2 = new JDCFPtiankai_new_new(12, bH, "xg");
                        if (_new2.ShowDialog() == DialogResult.OK)
                        {
                            if (this.UseYear)
                            {
                                this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                            }
                            else
                            {
                                this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                            }
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
            finally
            {
            }
        }

        private void aisinoDataGrid1_DataGridRowSelectionChanged(object sender, DataGridRowEventArgs e)
        {
            this.biaoti.Remove(0, this.biaoti.Length);
            this.biaoti.Append("销售单据录入查询(单据号:");
            this.biaoti.Append(e.get_CurrentRow().Cells["BH"].Value.ToString());
            this.biaoti.Append("  客户名称:");
            this.biaoti.Append(e.get_CurrentRow().Cells["GFMC"].Value.ToString());
            this.biaoti.Append(")");
            this.Text = this.biaoti.ToString();
            this.XSDJBH = e.get_CurrentRow().Cells["BH"].Value.ToString();
            this.btnGetGF.Visible = false;
            this.dateTimePicker1.Visible = false;
            this.KPZT = e.get_CurrentRow().Cells["KPZT"].Value.ToString();
            string str = e.get_CurrentRow().Cells["DJZT"].Value.ToString();
            this.toolStripBtnDJDel.Enabled = true;
            if (((str == "W") || (this.KPZT == "A")) || (this.KPZT == "P"))
            {
                this.toolStripBtnDJDel.Enabled = false;
            }
        }

        private void aisinoDataGrid1_DoubleClick(object sender, EventArgs e)
        {
            if (this.aisinoDataGrid1.get_Rows().Count == 0)
            {
                MessageBoxHelper.Show("单据日期不能为空！", "单据查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                this.djxgBLL.HandleGoToPageEventArgs(e);
                if (this.UseYear)
                {
                    this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                }
                else
                {
                    this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                }
                if (this.aisinoDataGrid1.get_Rows().Count > 0)
                {
                    this.aisinoDataGrid1.get_Rows()[0].Selected = true;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (((this.Global_DJType.CompareTo("a") == 0) || (this.Global_DJType.CompareTo("c") == 0)) || (this.Global_DJType.CompareTo("s") == 0))
                {
                    XSDJEdite edite = new XSDJEdite();
                    if (edite.ShowDialog() == DialogResult.OK)
                    {
                        if (this.UseYear)
                        {
                            this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                        }
                        else
                        {
                            this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                        }
                    }
                }
                else if (this.Global_DJType.CompareTo("f") == 0)
                {
                    HYFPtiankai_new _new = new HYFPtiankai_new(11, "销售单据", "销售单据");
                    if (_new.ShowDialog() == DialogResult.OK)
                    {
                        if (this.UseYear)
                        {
                            this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                        }
                        else
                        {
                            this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                        }
                    }
                }
                else if (this.Global_DJType.CompareTo("j") == 0)
                {
                    JDCFPtiankai_new_new _new2 = new JDCFPtiankai_new_new(12, "未开销售单据", "未开销售单据");
                    if (_new2.ShowDialog() == DialogResult.OK)
                    {
                        if (this.UseYear)
                        {
                            this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                        }
                        else
                        {
                            this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                        }
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                int count = this.aisinoDataGrid1.get_SelectedRows().Count;
                if (count == 0)
                {
                    MessageBoxHelper.Show("请先选择要删除的单据，再点击删除按钮！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (MessageManager.ShowMsgBox("INP-272104") == DialogResult.OK)
                    {
                        List<string> listBH = new List<string>();
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewRow row = this.aisinoDataGrid1.get_SelectedRows()[i];
                            if (!row.Cells["KPZT"].Value.ToString().Equals("N:未开票"))
                            {
                                MessageManager.ShowMsgBox("A330");
                                return;
                            }
                            string item = row.Cells["BH"].Value.ToString();
                            listBH.Add(item);
                        }
                        if (this.billBL.DeleteSaleBill(listBH) == 0)
                        {
                            MessageBoxHelper.Show("已作废单据不能删除，没有删除数据", "删除结果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    if (this.UseYear)
                    {
                        this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                    }
                    else
                    {
                        this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.aisinoDataGrid1.Print("销售单据", this, null, null, true);
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void btnZF_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoDataGrid1.get_SelectedRows().Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-272119");
                }
                else
                {
                    string bH = this.aisinoDataGrid1.get_SelectedRows()[0].Cells["BH"].Value.ToString();
                    SaleBill bill = this.billBL.Find(bH);
                    string str2 = this.billBL.WasteSaleBill(bill);
                    int index = this.aisinoDataGrid1.get_SelectedRows()[0].Index;
                    if (this.UseYear)
                    {
                        this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                    }
                    else
                    {
                        this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                    }
                    if (index < this.aisinoDataGrid1.get_Rows().Count)
                    {
                        this.aisinoDataGrid1.get_Rows()[index].Selected = true;
                        this.aisinoDataGrid1.set_CurrentCell(this.aisinoDataGrid1.get_Rows()[index].Cells[this.aisinoDataGrid1.get_CurrentCell().ColumnIndex]);
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

        private void but_Tongji_Click(object sender, EventArgs e)
        {
            try
            {
                this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1").Statistics(this);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.aisinoDataGrid1.get_CurrentCell().Value = this.dateTimePicker1.Value.ToShortDateString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DJXG_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if (this.Global_QuitJianYan)
                    {
                        MessageHelper.MsgWait("正在检查单据，请稍候...");
                        this.billBL.CheckBillMonth(this.Global_DJMonth, this.Global_DJType, true);
                    }
                    base.FormClosing -= new FormClosingEventHandler(this.DJXG_FormClosing);
                    base.Close();
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
            finally
            {
                MessageHelper.MsgWait();
            }
        }

        private void DJXG_Load(object sender, EventArgs e)
        {
            try
            {
                this.Global_VersionName = "1";
                this.tabControl1.TabPages.Remove(this.tabPage2);
                this.toolStripBtnHSJG.Visible = false;
                this.btnGetGF.Visible = false;
                this.dateTimePicker1.Visible = false;
                this.btnSP.Visible = false;
                this.comboBoxSlv.Visible = false;
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                Dictionary<string, string> item = new Dictionary<string, string>();
                if (((this.Global_DJType.CompareTo("a") == 0) || (this.Global_DJType.CompareTo("c") == 0)) || (this.Global_DJType.CompareTo("s") == 0))
                {
                    item.Add("AisinoLBL", "单据状态");
                    item.Add("Property", "DJZT");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "开票状态");
                    item.Add("Property", "KPZT");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "单据种类");
                    item.Add("Property", "DJZL");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "单据号");
                    item.Add("Property", "BH");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    item.Add("Visible", "True");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "购方名称");
                    item.Add("Property", "GFMC");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "购方税号");
                    item.Add("Property", "GFSH");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "购方地址电话");
                    item.Add("Property", "GFDZDH");
                    item.Add("Type", "Text");
                    item.Add("Width", "150");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "购方银行账号");
                    item.Add("Property", "GFYHZH");
                    item.Add("Type", "Text");
                    item.Add("Width", "150");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "销售部门");
                    item.Add("Property", "XSBM");
                    item.Add("Type", "Text");
                    item.Add("Visible", "false");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "异地销售");
                    item.Add("Property", "YDXS");
                    item.Add("Type", "Text");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "金额合计");
                    item.Add("Property", "JEHJ");
                    item.Add("Type", "Text");
                    item.Add("Width", "150");
                    item.Add("Align", "MiddleRight");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "单据日期");
                    item.Add("Property", "DJRQ");
                    item.Add("Type", "Text");
                    item.Add("Width", "150");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "登记月份");
                    item.Add("Property", "DJYF");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "备注");
                    item.Add("Property", "BZ");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "复核人");
                    item.Add("Property", "FHR");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "收款人");
                    item.Add("Property", "SKR");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "清单行商品名称");
                    item.Add("Property", "QDHSPMC");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    if (this.Global_VersionName == "1")
                    {
                        item.Add("AisinoLBL", "销方银行账号");
                    }
                    else if (this.Global_VersionName == "0")
                    {
                        item.Add("AisinoLBL", "完税凭证号");
                    }
                    item.Add("Property", "XFYHZH");
                    item.Add("Type", "Text");
                    item.Add("Width", "150");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    if (this.Global_VersionName == "1")
                    {
                        item.Add("AisinoLBL", "销方地址电话");
                    }
                    else if (this.Global_VersionName == "0")
                    {
                        item.Add("AisinoLBL", "代开企业地址电话");
                    }
                    item.Add("Property", "XFDZDH");
                    item.Add("Type", "Text");
                    item.Add("Width", "150");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "拆分合并");
                    item.Add("Property", "CFHB");
                    item.Add("Type", "Text");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "身份证校验");
                    item.Add("Property", "SFZJY");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "中外合作油气田");
                    item.Add("Width", "130");
                    item.Add("Property", "HYSY");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "船名");
                    item.Add("Property", "CM");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "到离港日期");
                    item.Add("Property", "DLGRQ");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "开户银行名称");
                    item.Add("Property", "KHYHMC");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "开户银行账号");
                    item.Add("Property", "KHYHZH");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "提运单号");
                    item.Add("Property", "TYDH");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "起运地");
                    item.Add("Property", "QYD");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "装货地");
                    item.Add("Property", "ZHD");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "卸货地");
                    item.Add("Property", "XHD");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "目的地");
                    item.Add("Property", "MDD");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "销方地址");
                    item.Add("Property", "XFDZ");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "销方电话");
                    item.Add("Property", "XFDH");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "运输货物信息");
                    item.Add("Property", "YSHWXX");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "生产企业名称");
                    item.Add("Property", "SCCJMC");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "税率");
                    item.Add("Property", "SLV");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "吨位");
                    item.Add("Property", "DW");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                }
                else if (this.Global_DJType.CompareTo("f") == 0)
                {
                    item.Add("AisinoLBL", "单据状态");
                    item.Add("Property", "DJZT");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "开票状态");
                    item.Add("Property", "KPZT");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "单据种类");
                    item.Add("Property", "DJZL");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "单据号");
                    item.Add("Property", "BH");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    item.Add("Visible", "True");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "实际受票方名称");
                    item.Add("Property", "GFMC");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "实际受票方税号");
                    item.Add("Property", "GFSH");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "收货人名称");
                    item.Add("Property", "GFDZDH");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "收货人税号");
                    item.Add("Property", "CM");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "发货人名称");
                    item.Add("Property", "XFDZDH");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "发货人税号");
                    item.Add("Property", "TYDH");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "税率");
                    item.Add("Property", "SLV");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "起运地、经由、到达地");
                    item.Add("Property", "XFYHZH");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "车种车号");
                    item.Add("Property", "QYD");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "车船吨位");
                    item.Add("Property", "DW");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "运输货物信息");
                    item.Add("Property", "YSHWXX");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "备注");
                    item.Add("Property", "BZ");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "复核人");
                    item.Add("Property", "FHR");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "收款人");
                    item.Add("Property", "SKR");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "单据日期");
                    item.Add("Property", "DJRQ");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "购方银行账号");
                    item.Add("Property", "GFYHZH");
                    item.Add("Type", "Text");
                    item.Add("Width", "150");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "销售部门");
                    item.Add("Property", "XSBM");
                    item.Add("Type", "Text");
                    item.Add("Width", "150");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "异地销售");
                    item.Add("Property", "YDXS");
                    item.Add("Type", "Text");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "金额合计");
                    item.Add("Property", "JEHJ");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleRight");
                    item.Add("HeadAlign", "MiddleCenter");
                    item.Add("Visible", "True");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "登记月份");
                    item.Add("Property", "DJYF");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "清单行商品名称");
                    item.Add("Property", "QDHSPMC");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "拆分合并");
                    item.Add("Property", "CFHB");
                    item.Add("Type", "Text");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "身份证校验");
                    item.Add("Property", "SFZJY");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "中外合作油气田");
                    item.Add("Width", "130");
                    item.Add("Property", "HYSY");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "到离港日期");
                    item.Add("Property", "DLGRQ");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "开户银行名称");
                    item.Add("Property", "KHYHMC");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "开户银行账号");
                    item.Add("Property", "KHYHZH");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "装货地");
                    item.Add("Property", "ZHD");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "卸货地");
                    item.Add("Property", "XHD");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "目的地");
                    item.Add("Property", "MDD");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "销方地址");
                    item.Add("Property", "XFDZ");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "销方电话");
                    item.Add("Property", "XFDH");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "生产企业名称");
                    item.Add("Property", "SCCJMC");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                }
                else if (this.Global_DJType.CompareTo("j") == 0)
                {
                    item.Add("AisinoLBL", "单据状态");
                    item.Add("Property", "DJZT");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "开票状态");
                    item.Add("Property", "KPZT");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "单据种类");
                    item.Add("Property", "DJZL");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "单据号");
                    item.Add("Property", "BH");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    item.Add("Visible", "True");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "购货单位");
                    item.Add("Property", "GFMC");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "身份证号码/组织机构代码");
                    item.Add("Property", "GFSH");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "价税合计");
                    item.Add("Property", "JEHJ");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleRight");
                    item.Add("HeadAlign", "MiddleCenter");
                    item.Add("Visible", "True");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "税率");
                    item.Add("Property", "SLV");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "车辆类型");
                    item.Add("Property", "GFDZDH");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "厂牌型号");
                    item.Add("Property", "XFDZ");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "产地");
                    item.Add("Property", "KHYHMC");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "生产企业名称");
                    item.Add("Property", "SCCJMC");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "合格证号");
                    item.Add("Property", "CM");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "进口证明书号");
                    item.Add("Property", "TYDH");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "商检单号");
                    item.Add("Property", "QYD");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "发动机号码");
                    item.Add("Property", "ZHD");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "车辆识别代号/车架号码");
                    item.Add("Property", "XHD");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "电话");
                    item.Add("Property", "XFDH");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "账号");
                    item.Add("Property", "KHYHZH");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "地址");
                    item.Add("Property", "XFDZDH");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "开户银行");
                    item.Add("Property", "XFYHZH");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "吨位");
                    item.Add("Property", "DW");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "限乘人数");
                    item.Add("Property", "MDD");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "单据日期");
                    item.Add("Property", "DJRQ");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "备注");
                    item.Add("Property", "BZ");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "纳税人识别号");
                    item.Add("Property", "GFYHZH");
                    item.Add("Type", "Text");
                    item.Add("Width", "150");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "销售部门");
                    item.Add("Property", "XSBM");
                    item.Add("Type", "Text");
                    item.Add("Width", "150");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "异地销售");
                    item.Add("Property", "YDXS");
                    item.Add("Type", "Text");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "登记月份");
                    item.Add("Property", "DJYF");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "复核人");
                    item.Add("Property", "FHR");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleCenter");
                    item.Add("HeadAlign", "MiddleCenter");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "收款人");
                    item.Add("Property", "SKR");
                    item.Add("Type", "Text");
                    item.Add("Width", "100");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "清单行商品名称");
                    item.Add("Property", "QDHSPMC");
                    item.Add("Type", "Text");
                    item.Add("Width", "200");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "拆分合并");
                    item.Add("Property", "CFHB");
                    item.Add("Type", "Text");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "身份证校验");
                    item.Add("Property", "SFZJY");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "中外合作油气田");
                    item.Add("Width", "130");
                    item.Add("Property", "HYSY");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "到离港日期");
                    item.Add("Property", "DLGRQ");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                    item = new Dictionary<string, string>();
                    item.Add("AisinoLBL", "运输货物信息");
                    item.Add("Property", "YSHWXX");
                    item.Add("Type", "Text");
                    item.Add("Align", "MiddleLeft");
                    item.Add("HeadAlign", "MiddleLeft");
                    item.Add("Visible", "False");
                    list.Add(item);
                }
                this.aisinoDataGrid1.set_ColumeHead(list);
                this.aisinoDataGrid1.set_MultiSelect(true);
                DataGridViewColumn column = this.aisinoDataGrid1.get_Columns()["JEHJ"];
                if (null != column)
                {
                    column.DefaultCellStyle.Format = "0.00";
                }
                if (((this.Global_DJType.CompareTo("a") == 0) || (this.Global_DJType.CompareTo("c") == 0)) || (this.Global_DJType.CompareTo("s") == 0))
                {
                    this.toolStripBtnZK.Click += new EventHandler(this.toolStripBtnZK_Click);
                    this.toolStripBtnZK.Text = "客户信息维护";
                    this.toolStripBtnZK.Visible = true;
                }
                else
                {
                    this.toolStripBtnZK.Visible = false;
                }
                this.comboBoxSlv.DataSource = readXMLSlv.ReadXMLSlvTable();
                this.comboBoxSlv.DisplayMember = "Display";
                this.comboBoxSlv.ValueMember = "Value";
                if (this.UseYear)
                {
                    this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                }
                else
                {
                    this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                }
                if (this.aisinoDataGrid1.get_Rows().Count > 0)
                {
                    this.aisinoDataGrid1.get_Rows()[0].Selected = true;
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.statusStrip1 = this.xmlComponentLoader1.GetControlByName<StatusStrip>("statusStrip1");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.button1 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button1");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.comboBoxYF = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxYF");
            this.textBox1 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox1");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.comboBoxDJZL = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxDJZL");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripBtnQuit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnQuit");
            this.toolStripSeparator1 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator1");
            this.toolStripButton3 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton3");
            this.toolStripBtnCheck = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnCheck");
            this.toolStripSeparator2 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator2");
            this.toolStripBtnEdit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnEdit");
            this.toolStripBtnHSJG = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnHSJG");
            this.toolStripSeparator3 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator3");
            this.toolStripButton6 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton6");
            this.AddGrid_toolStripButton = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("AddGrid_toolStripButton");
            this.tabControl1 = this.xmlComponentLoader1.GetControlByName<AisinoTAB>("tabControl1");
            this.tabPage1 = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage1");
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.tabPage2 = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage2");
            this.btnGetGF = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnGetGF");
            this.dateTimePicker1 = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dateTimePicker1");
            this.btnSP = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnSP");
            this.comboBoxSlv = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxSlv");
            this.aisinoDataGrid2 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid2");
            this.toolStripBtnDJAdd = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnDJAdd");
            this.toolStripBtnDJDel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnDJDel");
            this.toolStripBtnZuoFei = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnZuoFei");
            this.toolStripBtnMXAdd = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnMXAdd");
            this.toolStripBtnMXDel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnMXDel");
            this.toolStripBtnZK = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnZK");
            this.toolStripBtnTJ = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnTJ");
            this.toolStripBtnZK.Visible = true;
            this.toolStripBtnMXDel.Visible = false;
            this.toolStripBtnMXAdd.Visible = false;
            this.toolStripBtnHSJG.CheckOnClick = true;
            this.toolStripBtnQuit.Click += new EventHandler(this.toolStripBtnQuit_Click);
            this.aisinoDataGrid1.add_DataGridRowDbClickEvent(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowDbClickEvent));
            this.aisinoDataGrid1.add_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
            this.aisinoDataGrid1.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent));
            this.toolStripBtnZuoFei.Click += new EventHandler(this.btnZF_Click);
            this.toolStripBtnDJDel.Click += new EventHandler(this.btnDel_Click);
            this.toolStripBtnDJAdd.Click += new EventHandler(this.btnAdd_Click);
            this.toolStripButton3.Click += new EventHandler(this.btnPrint_Click);
            this.toolStripBtnCheck.Click += new EventHandler(this.toolStripBtnCheck_Click);
            this.toolStripBtnEdit.Click += new EventHandler(this.toolStripBtnEdit_Click);
            this.toolStripBtnTJ.Click += new EventHandler(this.but_Tongji_Click);
            this.dateTimePicker1.ValueChanged += new EventHandler(this.dateTimePicker1_ValueChanged);
            this.aisinoDataGrid1.add_DataGridCellFormatting(new EventHandler<DataGridViewCellFormattingEventArgs>(this.aisinoDataGrid1_DataGridCellFormatting));
            this.aisinoDataGrid1.set_ReadOnly(true);
            this.aisinoDataGrid1.set_ReadOnly(true);
            this.aisinoDataGrid1.get_DataGrid().AllowUserToDeleteRows = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DJXG));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x373, 0x26a);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "销售单据录入查询";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.DJXG\Aisino.Fwkp.Wbjk.DJXG.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x373, 0x26a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "DJXG";
            base.StartPosition = FormStartPosition.CenterParent;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "销售单据录入查询";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.DJXG_Load);
            base.FormClosing += new FormClosingEventHandler(this.DJXG_FormClosing);
            base.ResumeLayout(false);
        }

        private void SaveLogInfo(object datasource)
        {
            try
            {
                AisinoDataSet set = (AisinoDataSet) datasource;
                string str = "";
                foreach (DataColumn column in set.get_Data().Columns)
                {
                    str = str + " - " + column.ColumnName;
                }
                this.log.Debug("DataGrid数据源：" + str);
            }
            catch
            {
            }
        }

        private void toolStripBtnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoDataGrid1.get_SelectedRows().Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-272119");
                }
                else
                {
                    string str;
                    SaleBill bill;
                    string str2;
                    bool flag;
                    MessageHelper.MsgWait("正在检查单据，请稍候...");
                    if (this.aisinoDataGrid1.get_SelectedRows().Count == 1)
                    {
                        str = this.aisinoDataGrid1.get_SelectedRows()[0].Cells["BH"].Value.ToString();
                        bill = this.billBL.Find(str);
                        str2 = this.billBL.CheckBill(bill);
                        int index = this.aisinoDataGrid1.get_SelectedRows()[0].Index;
                        flag = this.aisinoDataGrid1.get_IsShowAll();
                        if (this.UseYear)
                        {
                            this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                        }
                        else
                        {
                            this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                        }
                        this.aisinoDataGrid1.set_IsShowAll(flag);
                        if (index < this.aisinoDataGrid1.get_Rows().Count)
                        {
                            this.aisinoDataGrid1.get_Rows()[index].Selected = true;
                            this.aisinoDataGrid1.set_CurrentCell(this.aisinoDataGrid1.get_Rows()[index].Cells[1]);
                        }
                        MessageBoxHelper.Show(str2, "检查结果", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        List<int> list = new List<int>();
                        int num2 = 0;
                        for (int i = 0; i < this.aisinoDataGrid1.get_SelectedRows().Count; i++)
                        {
                            str = this.aisinoDataGrid1.get_SelectedRows()[i].Cells["BH"].Value.ToString();
                            bill = this.billBL.Find(str);
                            str2 = this.billBL.CheckBill(bill);
                            num2++;
                            list.Add(this.aisinoDataGrid1.get_SelectedRows()[i].Index);
                        }
                        flag = this.aisinoDataGrid1.get_IsShowAll();
                        if (this.UseYear)
                        {
                            this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                        }
                        else
                        {
                            this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                        }
                        this.aisinoDataGrid1.set_IsShowAll(flag);
                        foreach (int num4 in list)
                        {
                            if (num4 < this.aisinoDataGrid1.get_Rows().Count)
                            {
                                this.aisinoDataGrid1.get_Rows()[num4].Selected = true;
                            }
                        }
                        MessageBoxHelper.Show("检查完成，共校验 " + num2.ToString() + " 条");
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
            finally
            {
                MessageHelper.MsgWait();
            }
        }

        private void toolStripBtnEdit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.toolStripBtnEdit.Checked)
                {
                    this.aisinoDataGrid1.set_ReadOnly(false);
                    this.aisinoDataGrid1.get_Columns()["BH"].ReadOnly = true;
                    this.aisinoDataGrid1.get_Columns()["DJZT"].ReadOnly = true;
                    this.aisinoDataGrid1.get_Columns()["KPZT"].ReadOnly = true;
                    this.aisinoDataGrid1.get_Columns()["JEHJ"].ReadOnly = true;
                    for (int i = 0; i < this.aisinoDataGrid1.get_Rows().Count; i++)
                    {
                        if (this.aisinoDataGrid1.get_Rows()[i].Cells["DJZT"].Value.ToString().StartsWith("W"))
                        {
                            this.aisinoDataGrid1.get_Rows()[i].ReadOnly = true;
                        }
                    }
                }
                else
                {
                    this.aisinoDataGrid1.set_ReadOnly(true);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void toolStripBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (this.aisinoDataGrid1.get_SelectedRows().Count > 0)
                    {
                        string str = this.aisinoDataGrid1.get_SelectedRows()[0].Cells["KPZT"].Value.ToString();
                        string bH = this.aisinoDataGrid1.get_SelectedRows()[0].Cells["BH"].Value.ToString();
                        if (((this.Global_DJType.CompareTo("a") == 0) || (this.Global_DJType.CompareTo("c") == 0)) || (this.Global_DJType.CompareTo("s") == 0))
                        {
                            XSDJEdite edite = new XSDJEdite(bH);
                            if (edite.ShowDialog() == DialogResult.OK)
                            {
                                if (this.UseYear)
                                {
                                    this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                                }
                                else
                                {
                                    this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                                }
                            }
                        }
                        else if (this.Global_DJType.CompareTo("f") == 0)
                        {
                            HYFPtiankai_new _new = new HYFPtiankai_new(11, bH, "xg");
                            if (_new.ShowDialog() == DialogResult.OK)
                            {
                                if (this.UseYear)
                                {
                                    this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                                }
                                else
                                {
                                    this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                                }
                            }
                        }
                        else if (this.Global_DJType.CompareTo("j") == 0)
                        {
                            JDCFPtiankai_new_new _new2 = new JDCFPtiankai_new_new(12, bH, "xg");
                            if (_new2.ShowDialog() == DialogResult.OK)
                            {
                                if (this.UseYear)
                                {
                                    this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                                }
                                else
                                {
                                    this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
                                }
                            }
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
            finally
            {
            }
        }

        private void toolStripBtnQuit_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void toolStripBtnZK_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    int count = this.aisinoDataGrid1.get_SelectedRows().Count;
                    for (int i = 0; i < count; i++)
                    {
                        string bH = this.aisinoDataGrid1.get_SelectedRows()[i].Cells["BH"].Value.ToString();
                        SaleBill bill = null;
                        if (bH != null)
                        {
                            bill = SaleBillCtrl.Instance.Find(bH);
                            if (bill != null)
                            {
                                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetKhbyMcAndShMore", new object[] { bill.GFMC, bill.GFSH, "MC,SH,DZDH,YHZH" });
                                if ((objArray != null) && (objArray.Length == 4))
                                {
                                    string[] strArray;
                                    if (string.IsNullOrEmpty(bill.GFMC))
                                    {
                                        bill.GFMC = Convert.ToString(objArray[0]);
                                    }
                                    if (string.IsNullOrEmpty(bill.GFSH))
                                    {
                                        bill.GFSH = Convert.ToString(objArray[1]);
                                    }
                                    if (string.IsNullOrEmpty(bill.GFDZDH))
                                    {
                                        strArray = Convert.ToString(objArray[2]).Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                        bill.GFDZDH = strArray[0];
                                    }
                                    if (string.IsNullOrEmpty(bill.GFYHZH))
                                    {
                                        strArray = Convert.ToString(objArray[3]).Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                        bill.GFYHZH = strArray[0];
                                    }
                                    SaleBillCtrl.Instance.Save(bill);
                                    SaleBillCtrl.Instance.CheckBill(bill);
                                }
                            }
                        }
                    }
                    if (this.UseYear)
                    {
                        this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJYear, this.Global_DJMonth, this.Global_DJType));
                    }
                    else
                    {
                        this.aisinoDataGrid1.set_DataSource(this.djxgBLL.QueryXSDJ(this.Global_DJMonth, this.Global_DJType));
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
            finally
            {
            }
        }
    }
}

