namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FPQuery : BaseForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoCMB cboFPType;
        private AisinoCMB cboMonth;
        private AisinoCMB cboShuiqi;
        private bool checkCause1;
        private bool checkCause2;
        private bool checkCause3;
        private bool checkCause4;
        private AisinoCHK chkFPTotal;
        private AisinoCHK chkHWYS;
        private AisinoCHK chkJDC;
        private AisinoCHK chkMinusFP;
        private AisinoCHK chkObsMinusFP;
        private AisinoCHK chkObsPlusFP;
        private AisinoCHK chkPlusFP;
        private AisinoCHK chkZZSPTFP;
        private AisinoCHK chkZZSZYFP;
        private AisinoCMB cmbXZSQ1;
        private AisinoCMB cmbXZSQ2;
        private AisinoCMB cmbXZSQ3;
        private AisinoCMB cmbXZSQ4;
        private AisinoCMB cmbXZYF1;
        private AisinoCMB cmbXZYF2;
        private AisinoCMB cmbXZYF3;
        private AisinoCMB cmbXZYF4;
        private IContainer components;
        private AisinoGRP groupBox1;
        private AisinoGRP groupBox2;
        private AisinoGRP groupBox3;
        private AisinoGRP groupBox4;
        private AisinoGRP groupBoxTJXZ;
        private bool isInit;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL lableXZSQ;
        private AisinoLBL lableXZYF;
        private AisinoLBL lableXZZL;
        private string lastRepDateHY = "";
        private string lastRepDateJDC = "";
        private ILog loger = LogUtil.GetLogger<FPQuery>();
        private List<AisinoCHK> m_checkBoxList = new List<AisinoCHK>();
        private CommFun m_commFun = new CommFun();
        private List<InvTypeEntity> m_InvTypeEntityList = new List<InvTypeEntity>();
        private List<InvTypeEntity> m_InvTypeEntityListPrint = new List<InvTypeEntity>();
        private List<ItemEntity> m_ItemsList = new List<ItemEntity>();
        public List<PrintEntity> m_PrintEntityList = new List<PrintEntity>();
        private QueryPrintEntity m_QueryPrintEntity = new QueryPrintEntity();
        private List<QueryPrintEntity> m_QueryPrintEntityList = new List<QueryPrintEntity>();
        private int nMonth;
        private int nYear;
        private QueryPrintBLL queryPrintBLL = new QueryPrintBLL();
        private QueryPrintEntity queryPrintEntity = new QueryPrintEntity();
        private AisinoRDO rbtFPTotal;
        private AisinoRDO rbtnMinusFP;
        private AisinoRDO rbtnObsMinusFP;
        private AisinoRDO rbtnObsPlusFP;
        private AisinoRDO rbtnPlusFP;
        private AisinoRDO rbtnPrint;
        private AisinoRDO rbtnQuery;
        private TaxcardEntityBLL taxcardEntityBLL = new TaxcardEntityBLL();
        private XmlComponentLoader xmlComponentLoader1;

        public FPQuery()
        {
            this.Initial();
            this.nYear = this.taxcardEntityBLL.GetTaxDate().Year;
            this.nMonth = this.taxcardEntityBLL.GetTaxDate().Month;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "";
                if (this.cboMonth.Text.Trim() != "")
                {
                    string text = this.cboMonth.Text;
                    int index = text.IndexOf("年");
                    if (index >= 0)
                    {
                        text = text.Substring(0, index);
                        if (text == "本")
                        {
                            this.queryPrintEntity.m_nYear = this.nYear;
                        }
                        else
                        {
                            this.queryPrintEntity.m_nYear = Convert.ToInt32(text);
                        }
                        text = this.cboMonth.Text;
                        index = text.IndexOf("年");
                        if (index >= 0)
                        {
                            text = text.Substring(index + 1, (text.IndexOf("月") - index) - 1);
                            this.queryPrintEntity.m_nMonth = Convert.ToInt32(text);
                            if (this.cboShuiqi.Text.Trim() != "")
                            {
                                if (this.cboShuiqi.Text == "本月累计")
                                {
                                    this.queryPrintEntity.m_nTaxPeriod = 0;
                                }
                                else
                                {
                                    string str3 = this.cboShuiqi.Text;
                                    int num2 = str3.IndexOf("第");
                                    if (num2 < 0)
                                    {
                                        return;
                                    }
                                    str3 = str3.Substring(num2 + 1, (str3.IndexOf("期") - num2) - 1);
                                    this.queryPrintEntity.m_nTaxPeriod = Convert.ToInt32(str3);
                                }
                                if (this.rbtnPrint.Checked)
                                {
                                    if (this.m_InvTypeEntityList.Count <= 0)
                                    {
                                        MessageManager.ShowMsgBox("INP-251303", new string[] { "获取发票种类失败" });
                                    }
                                    else if ((!this.chkZZSZYFP.Checked && !this.chkZZSPTFP.Checked) && (!this.chkHWYS.Checked && !this.chkJDC.Checked))
                                    {
                                        MessageManager.ShowMsgBox("INP-251303", new string[] { "请选择你要打印的发票种类！" });
                                    }
                                    else if (((!this.chkFPTotal.Checked && !this.chkPlusFP.Checked) && (!this.chkMinusFP.Checked && !this.chkObsPlusFP.Checked)) && !this.chkObsMinusFP.Checked)
                                    {
                                        MessageManager.ShowMsgBox("INP-251303", new string[] { "请选择你要打印的类型！" });
                                    }
                                    else
                                    {
                                        this.m_InvTypeEntityListPrint.Clear();
                                        this.m_InvTypeEntityListPrint = this.GetPrintInvTypeCollection();
                                        this.m_ItemsList.Clear();
                                        this.m_ItemsList = this.GetItemTypeCollect();
                                        this.m_QueryPrintEntityList.Clear();
                                        this.SerialPrint();
                                    }
                                }
                                else if (this.cboFPType.Items.Count <= 0)
                                {
                                    MessageManager.ShowMsgBox("INP-251303", new string[] { "加载发票种类失败" });
                                }
                                else
                                {
                                    this.queryPrintEntity.m_bPrint = false;
                                    this.queryPrintEntity.m_bShowDialog = true;
                                    if (this.cboFPType.Text.Trim() != "")
                                    {
                                        if (this.cboFPType.Text == "增值税普通发票")
                                        {
                                            this.queryPrintEntity.m_invType = INV_TYPE.INV_COMMON;
                                        }
                                        else if (this.cboFPType.Text == "增值税专用发票")
                                        {
                                            this.queryPrintEntity.m_invType = INV_TYPE.INV_SPECIAL;
                                        }
                                        else if (this.cboFPType.Text == "货物运输业增值税专用发票")
                                        {
                                            this.queryPrintEntity.m_invType = INV_TYPE.INV_TRANSPORTATION;
                                        }
                                        else if (this.cboFPType.Text == "机动车销售统一发票")
                                        {
                                            this.queryPrintEntity.m_invType = INV_TYPE.INV_VEHICLESALES;
                                        }
                                        else
                                        {
                                            this.queryPrintEntity.m_invType = INV_TYPE.INV_OTHER;
                                        }
                                    }
                                    if (this.rbtFPTotal.Checked)
                                    {
                                        this.queryPrintEntity.m_itemAction = ITEM_ACTION.ITEM_TOTAL;
                                        if (this.queryPrintEntity.m_invType == INV_TYPE.INV_COMMON)
                                        {
                                            this.queryPrintEntity.m_strTitle = "增值税普通发票汇总表";
                                            this.queryPrintEntity.m_strSubItem = "增值税普通发票统计表 1-01";
                                        }
                                        else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_SPECIAL)
                                        {
                                            this.queryPrintEntity.m_strTitle = "增值税专用发票汇总表";
                                            this.queryPrintEntity.m_strSubItem = "增值税专用发票统计表 1-01";
                                        }
                                        else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                                        {
                                            this.queryPrintEntity.m_strTitle = "货物运输业增值税专用发票汇总表";
                                            this.queryPrintEntity.m_strSubItem = "货物运输业增值税专用发票统计表 1-01";
                                        }
                                        else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_VEHICLESALES)
                                        {
                                            this.queryPrintEntity.m_strTitle = "机动车销售统一发票汇总表";
                                            this.queryPrintEntity.m_strSubItem = "机动车销售统一发票统计表 1-01";
                                        }
                                        else
                                        {
                                            this.queryPrintEntity.m_strTitle = "其他发票汇总表";
                                            this.queryPrintEntity.m_strSubItem = "其他发票统计表 1-01";
                                        }
                                        this.queryPrintEntity.m_strItemDetail = string.Concat(new object[] { "增值税发票汇总表(", this.queryPrintEntity.m_nYear, "年", this.queryPrintEntity.m_nMonth, "月)" });
                                        InvStatForm form = new InvStatForm(this.queryPrintEntity) {
                                            Text = this.queryPrintEntity.m_strItemDetail
                                        };
                                        if (this.queryPrintEntity.m_nTaxPeriod == 0)
                                        {
                                            form.m_strTitle = "税档数据所属期为   " + this.queryPrintEntity.m_nMonth.ToString() + "月份";
                                        }
                                        else
                                        {
                                            form.m_strTitle = string.Concat(new object[] { "税档数据所属期为   ", this.queryPrintEntity.m_nMonth.ToString(), "月第", this.queryPrintEntity.m_nTaxPeriod, "期" });
                                        }
                                        form.StartPosition = FormStartPosition.CenterScreen;
                                        form.ShowInTaskbar = false;
                                        form.ShowDialog();
                                    }
                                    else
                                    {
                                        if (this.rbtnPlusFP.Checked)
                                        {
                                            this.queryPrintEntity.m_itemAction = ITEM_ACTION.ITEM_PLUS;
                                            str = string.Concat(new object[] { "正数发票清单(", this.queryPrintEntity.m_nYear, "年", this.queryPrintEntity.m_nMonth, "月)" });
                                            if (this.queryPrintEntity.m_invType == INV_TYPE.INV_COMMON)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "增值税普通发票统计表  1-02";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_SPECIAL)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "增值税专用发票统计表  1-02";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "货物运输业增值税专用发票统计表  1-02";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_VEHICLESALES)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "机动车销售统一发票统计表  1-02";
                                            }
                                            else
                                            {
                                                this.queryPrintEntity.m_strSubItem = "其他发票统计表  1-02";
                                            }
                                        }
                                        else if (this.rbtnMinusFP.Checked)
                                        {
                                            this.queryPrintEntity.m_itemAction = ITEM_ACTION.ITEM_MINUS;
                                            str = string.Concat(new object[] { "负数发票清单(", this.queryPrintEntity.m_nYear, "年", this.queryPrintEntity.m_nMonth, "月)" });
                                            if (this.queryPrintEntity.m_invType == INV_TYPE.INV_COMMON)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "增值税普通发票统计表  1-03";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_SPECIAL)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "增值税专用发票统计表  1-03";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "货物运输业增值税专用发票统计表  1-03";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_VEHICLESALES)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "机动车销售统一发票统计表  1-03";
                                            }
                                            else
                                            {
                                                this.queryPrintEntity.m_strSubItem = "其他发票统计表  1-03";
                                            }
                                        }
                                        else if (this.rbtnObsPlusFP.Checked)
                                        {
                                            this.queryPrintEntity.m_itemAction = ITEM_ACTION.ITEM_PLUS_WASTE;
                                            str = string.Concat(new object[] { "正数发票废票清单(", this.queryPrintEntity.m_nYear, "年", this.queryPrintEntity.m_nMonth, "月)" });
                                            if (this.queryPrintEntity.m_invType == INV_TYPE.INV_COMMON)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "增值税普通发票统计表  1-04";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_SPECIAL)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "增值税专用发票统计表  1-04";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "货物运输业增值税专用发票统计表  1-04";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_VEHICLESALES)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "机动车销售统一发票统计表  1-04";
                                            }
                                            else
                                            {
                                                this.queryPrintEntity.m_strSubItem = "其他发票统计表  1-04";
                                            }
                                        }
                                        else if (this.rbtnObsMinusFP.Checked)
                                        {
                                            this.queryPrintEntity.m_itemAction = ITEM_ACTION.ITEM_MINUS_WASTE;
                                            str = string.Concat(new object[] { "负数发票废票清单(", this.queryPrintEntity.m_nYear, "年", this.queryPrintEntity.m_nMonth, "月)" });
                                            if (this.queryPrintEntity.m_invType == INV_TYPE.INV_COMMON)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "增值税普通发票统计表  1-05";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_SPECIAL)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "增值税专用发票统计表  1-05";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "货物运输业增值税专用发票统计表  1-05";
                                            }
                                            else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_VEHICLESALES)
                                            {
                                                this.queryPrintEntity.m_strSubItem = "机动车销售统一发票统计表  1-05";
                                            }
                                            else
                                            {
                                                this.queryPrintEntity.m_strSubItem = "其他发票统计表  1-05";
                                            }
                                        }
                                        if (this.queryPrintEntity.m_invType == INV_TYPE.INV_COMMON)
                                        {
                                            this.queryPrintEntity.m_strTitle = "增值税普通发票明细表";
                                        }
                                        else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_SPECIAL)
                                        {
                                            this.queryPrintEntity.m_strTitle = "增值税普通发票明细表";
                                        }
                                        else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                                        {
                                            this.queryPrintEntity.m_strTitle = "货物运输业增值税专用发票";
                                        }
                                        else if (this.queryPrintEntity.m_invType == INV_TYPE.INV_VEHICLESALES)
                                        {
                                            this.queryPrintEntity.m_strTitle = "机动车销售统一发票";
                                        }
                                        else
                                        {
                                            this.queryPrintEntity.m_strTitle = "其他发票明细表";
                                        }
                                        if (this.queryPrintEntity.m_nTaxPeriod != 0)
                                        {
                                            this.queryPrintEntity.m_strItemDetail = string.Concat(new object[] { str, "第", this.queryPrintEntity.m_nTaxPeriod, "期" });
                                        }
                                        else
                                        {
                                            this.queryPrintEntity.m_strItemDetail = str;
                                        }
                                        new InvoiceResultForm(this.queryPrintEntity) { Text = str, StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = false }.ShowDialog();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        private void cboFPType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                this.cboMonth.Items.Clear();
                this.InitialMonth_();
                if (this.cboFPType.SelectedItem.Equals("增值税专用发票") || this.cboFPType.SelectedItem.Equals("增值税普通发票"))
                {
                    this.cboShuiqi.Items.Clear();
                    this.InitialPeriod_(this.cboMonth, this.cboShuiqi, 0);
                }
                if (this.cboFPType.SelectedItem.Equals("货物运输业增值税专用发票"))
                {
                    this.cboShuiqi.Items.Clear();
                    this.InitialPeriod_(this.cboMonth, this.cboShuiqi, 11);
                }
                if (this.cboFPType.SelectedItem.Equals("机动车销售统一发票"))
                {
                    this.cboShuiqi.Items.Clear();
                    this.InitialPeriod_(this.cboMonth, this.cboShuiqi, 12);
                }
            }
        }

        public void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.InitialPeriod_();
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        private void chkHWYS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                this.checkCause3 = true;
                if (!this.chkHWYS.Checked)
                {
                    this.cmbXZYF3.SelectedIndex = -1;
                    this.cmbXZSQ3.SelectedIndex = -1;
                }
                else
                {
                    this.cmbXZYF3.SelectedIndex = this.cmbXZYF3.Items.Count - 1;
                    this.cmbXZSQ3.SelectedIndex = this.cmbXZSQ3.Items.Count - 1;
                }
                this.checkCause3 = false;
            }
        }

        private void chkJDC_CheckedChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                this.checkCause4 = true;
                if (!this.chkJDC.Checked)
                {
                    this.cmbXZYF4.SelectedIndex = -1;
                    this.cmbXZSQ4.SelectedIndex = -1;
                }
                else
                {
                    this.cmbXZYF4.SelectedIndex = this.cmbXZYF4.Items.Count - 1;
                    this.cmbXZSQ4.SelectedIndex = this.cmbXZSQ4.Items.Count - 1;
                }
                this.checkCause4 = false;
            }
        }

        private void chkZZSPTFP_CheckedChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                this.checkCause2 = true;
                if (!this.chkZZSPTFP.Checked)
                {
                    this.cmbXZYF2.SelectedIndex = -1;
                    this.cmbXZSQ2.SelectedIndex = -1;
                }
                else
                {
                    this.cmbXZYF2.SelectedIndex = this.cmbXZYF2.Items.Count - 1;
                    this.cmbXZSQ2.SelectedIndex = this.cmbXZSQ2.Items.Count - 1;
                }
                this.checkCause2 = false;
            }
        }

        private void chkZZSZYFP_CheckedChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                this.checkCause1 = true;
                if (!this.chkZZSZYFP.Checked)
                {
                    this.cmbXZYF1.SelectedIndex = -1;
                    this.cmbXZSQ1.SelectedIndex = -1;
                }
                else
                {
                    this.cmbXZYF1.SelectedIndex = this.cmbXZYF1.Items.Count - 1;
                    this.cmbXZSQ1.SelectedIndex = this.cmbXZSQ1.Items.Count - 1;
                }
                this.checkCause1 = false;
            }
        }

        private void cmbXZYF1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                if (!this.checkCause1)
                {
                    this.InitialPeriod_(this.cmbXZYF1, this.cmbXZSQ1, 0);
                }
                this.cmbXZSQ1.SelectedIndex = this.cmbXZSQ1.Items.Count - 1;
            }
        }

        private void cmbXZYF2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                if (!this.checkCause2)
                {
                    this.InitialPeriod_(this.cmbXZYF2, this.cmbXZSQ2, 0);
                }
                this.cmbXZSQ2.SelectedIndex = this.cmbXZSQ2.Items.Count - 1;
            }
        }

        private void cmbXZYF3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                if (!this.checkCause3)
                {
                    this.InitialPeriod_(this.cmbXZYF3, this.cmbXZSQ3, 11);
                }
                this.cmbXZSQ3.SelectedIndex = this.cmbXZSQ3.Items.Count - 1;
            }
        }

        private void cmbXZYF4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                if (!this.checkCause4)
                {
                    this.InitialPeriod_(this.cmbXZYF4, this.cmbXZSQ4, 12);
                }
                this.cmbXZSQ4.SelectedIndex = this.cmbXZSQ4.Items.Count - 1;
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

        private void FPQuery_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitialControl();
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        private List<ItemEntity> GetItemTypeCollect()
        {
            List<ItemEntity> list = new List<ItemEntity>();
            ItemEntity item = new ItemEntity();
            if (this.chkFPTotal.Checked)
            {
                item = new ItemEntity {
                    m_ItemType = ITEM_ACTION.ITEM_TOTAL,
                    m_strItemName = "增值税发票汇总表",
                    m_bStatus = false
                };
                list.Add(item);
            }
            if (this.chkPlusFP.Checked)
            {
                item = new ItemEntity {
                    m_ItemType = ITEM_ACTION.ITEM_PLUS,
                    m_strItemName = "正数发票清单",
                    m_bStatus = false
                };
                list.Add(item);
            }
            if (this.chkMinusFP.Checked)
            {
                item = new ItemEntity {
                    m_ItemType = ITEM_ACTION.ITEM_MINUS,
                    m_strItemName = "负数发票清单",
                    m_bStatus = false
                };
                list.Add(item);
            }
            if (this.chkObsPlusFP.Checked)
            {
                item = new ItemEntity {
                    m_ItemType = ITEM_ACTION.ITEM_PLUS_WASTE,
                    m_strItemName = "正数发票废票清单",
                    m_bStatus = false
                };
                list.Add(item);
            }
            if (this.chkObsMinusFP.Checked)
            {
                item = new ItemEntity {
                    m_ItemType = ITEM_ACTION.ITEM_MINUS_WASTE,
                    m_strItemName = "负数发票废票清单",
                    m_bStatus = false
                };
                list.Add(item);
            }
            return list;
        }

        private List<InvTypeEntity> GetPrintInvTypeCollection()
        {
            List<InvTypeEntity> list = new List<InvTypeEntity>();
            InvTypeEntity item = new InvTypeEntity();
            if (this.chkZZSZYFP.Checked)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_SPECIAL,
                    m_strInvName = "增值税专用发票"
                };
                list.Add(item);
            }
            if (this.chkZZSPTFP.Checked)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_COMMON,
                    m_strInvName = "增值税普通发票"
                };
                list.Add(item);
            }
            if (this.chkHWYS.Checked)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_TRANSPORTATION,
                    m_strInvName = "货物运输业增值税专用发票"
                };
                list.Add(item);
            }
            if (this.chkJDC.Checked)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_VEHICLESALES,
                    m_strInvName = "机动车销售统一发票"
                };
                list.Add(item);
            }
            return list;
        }

        private void Initial()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Load += new EventHandler(this.FPQuery_Load);
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.groupBox1.BackColor = Color.FromArgb(0xf2, 0xf4, 0xf5);
            this.groupBox2 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox2");
            this.groupBox2.BackColor = Color.FromArgb(0xf2, 0xf4, 0xf5);
            this.groupBox3 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox3");
            this.groupBox3.BackColor = Color.FromArgb(0xf2, 0xf4, 0xf5);
            this.groupBox4 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox4");
            this.groupBox4.BackColor = Color.FromArgb(0xf2, 0xf4, 0xf5);
            this.groupBox4.Visible = false;
            this.rbtnPrint = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtnPrint");
            this.rbtnQuery = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtnQuery");
            this.rbtnQuery.Checked = true;
            this.rbtnObsMinusFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtnObsMinusFP");
            this.rbtnObsPlusFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtnObsPlusFP");
            this.rbtnMinusFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtnMinusFP");
            this.rbtFPTotal = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtFPTotal");
            this.rbtFPTotal.Checked = true;
            this.rbtnPlusFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtnPlusFP");
            this.cboShuiqi = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cboShuiqi");
            this.cboMonth = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cboMonth");
            this.cboFPType = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cboFPType");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.label5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label5");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.chkObsMinusFP = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkObsMinusFP");
            this.chkObsMinusFP.Checked = true;
            this.chkObsPlusFP = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkObsPlusFP");
            this.chkObsPlusFP.Checked = true;
            this.chkMinusFP = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkMinusFP");
            this.chkMinusFP.Checked = true;
            this.chkPlusFP = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkPlusFP");
            this.chkPlusFP.Checked = true;
            this.chkFPTotal = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkFPTotal");
            this.chkFPTotal.Checked = true;
            this.groupBoxTJXZ = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBoxTJXZ");
            this.groupBoxTJXZ.BackColor = Color.FromArgb(0xf2, 0xf4, 0xf5);
            this.cmbXZSQ4 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZSQ4");
            this.cmbXZSQ3 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZSQ3");
            this.cmbXZSQ2 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZSQ2");
            this.cmbXZYF4 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZYF4");
            this.cmbXZYF3 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZYF3");
            this.cmbXZYF2 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZYF2");
            this.chkJDC = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkJDC");
            this.chkHWYS = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkHWYS");
            this.chkZZSPTFP = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkZZSPTFP");
            this.chkZZSZYFP = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkZZSZYFP");
            this.cmbXZSQ1 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZSQ1");
            this.cmbXZYF1 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZYF1");
            this.lableXZSQ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableXZSQ");
            this.lableXZYF = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableXZYF");
            this.lableXZZL = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableXZZL");
            this.cboFPType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboMonth.SelectedIndexChanged += new EventHandler(this.cboMonth_SelectedIndexChanged);
            this.cboShuiqi.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZSQ1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZSQ2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZSQ3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZSQ4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZYF1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZYF2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZYF3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZYF4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.rbtnQuery.CheckedChanged += new EventHandler(this.rbtnQuery_CheckedChanged);
            this.rbtnPrint.CheckedChanged += new EventHandler(this.rbtnPrint_CheckedChanged);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.chkZZSPTFP.CheckedChanged += new EventHandler(this.chkZZSPTFP_CheckedChanged);
            this.chkZZSZYFP.CheckedChanged += new EventHandler(this.chkZZSZYFP_CheckedChanged);
            this.chkHWYS.CheckedChanged += new EventHandler(this.chkHWYS_CheckedChanged);
            this.chkJDC.CheckedChanged += new EventHandler(this.chkJDC_CheckedChanged);
            this.cmbXZYF1.SelectedIndexChanged += new EventHandler(this.cmbXZYF1_SelectedIndexChanged);
            this.cmbXZYF2.SelectedIndexChanged += new EventHandler(this.cmbXZYF2_SelectedIndexChanged);
            this.cmbXZYF3.SelectedIndexChanged += new EventHandler(this.cmbXZYF3_SelectedIndexChanged);
            this.cmbXZYF4.SelectedIndexChanged += new EventHandler(this.cmbXZYF4_SelectedIndexChanged);
            this.cboFPType.SelectedIndexChanged += new EventHandler(this.cboFPType_SelectedIndexChanged);
        }

        private void InitialControl()
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
            }
            this.LoadInvType();
            this.InitialMonth_();
            this.InitialPeriod_();
            this.groupBoxTJXZ.Visible = false;
            this.groupBox4.Visible = false;
            this.LoadInvTypePrint();
            this.InitialMonthPrint();
            this.InitialPeriodprint();
            this.cmbXZSQ1.SelectedIndex = -1;
            this.cmbXZSQ2.SelectedIndex = -1;
            this.cmbXZSQ3.SelectedIndex = -1;
            this.cmbXZSQ4.SelectedIndex = -1;
            this.cmbXZYF1.SelectedIndex = -1;
            this.cmbXZYF2.SelectedIndex = -1;
            this.cmbXZYF3.SelectedIndex = -1;
            this.cmbXZYF4.SelectedIndex = -1;
            this.isInit = true;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FPQuery));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1bd, 0x1c3);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.FPQuery\Aisino.Fwkp.Bsgl.FPQuery.xml");
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1bd, 0x1c3);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.Name = "FPQuery";
            this.Text = "发票资料查询打印";
            base.ResumeLayout(false);
        }

        private void InitialMonth_()
        {
            this.cboMonth.Items.Clear();
            DateTime currentDate = this.queryPrintBLL.GetCurrentDate();
            List<int> list = new List<int>();
            List<string> list2 = new List<string>();
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            if (this.cboFPType.SelectedItem.Equals("增值税专用发票") || this.cboFPType.SelectedItem.Equals("增值税普通发票"))
            {
                dictionary = this.taxcardEntityBLL.getYearMonthZP();
            }
            else if (this.cboFPType.SelectedItem.Equals("货物运输业增值税专用发票"))
            {
                dictionary = this.taxcardEntityBLL.getYearMonthHY();
            }
            if (this.cboFPType.SelectedItem.Equals("机动车销售统一发票"))
            {
                dictionary = this.taxcardEntityBLL.getYearMonthJDC();
            }
            foreach (KeyValuePair<int, List<int>> pair in dictionary)
            {
                list.Add(pair.Key);
            }
            for (int i = 0; i < list.Count; i++)
            {
                List<int> list3 = dictionary[list[i]];
                string str = "";
                if (list[i] == currentDate.Year)
                {
                    str = "本年";
                }
                else
                {
                    str = list[i].ToString() + "年";
                }
                for (int j = 0; j < list3.Count; j++)
                {
                    list2.Add(str + list3[j].ToString() + "月");
                }
            }
            string str2 = list2[list2.Count - 1];
            int index = str2.IndexOf("年");
            if (index >= 0)
            {
                string str3 = str2.Substring(0, index);
                int year = 0;
                if (str3 == "本")
                {
                    year = currentDate.Year;
                }
                else
                {
                    year = Convert.ToInt32(str3);
                }
                int num5 = Convert.ToInt32(str2.Substring(index + 1, (str2.IndexOf("月") - index) - 1));
                for (int k = 0; k < list2.Count; k++)
                {
                    this.cboMonth.Items.Add(list2[k]);
                }
                if (this.cboMonth.Items.Count > 0)
                {
                    if ((year == currentDate.Year) && (num5 == currentDate.Month))
                    {
                        this.cboMonth.Text = "本年" + currentDate.Month.ToString() + "月";
                    }
                    else if (currentDate.Month == 1)
                    {
                        this.cboMonth.Text = currentDate.AddMonths(-1).Year.ToString() + "年12月";
                    }
                    else
                    {
                        this.cboMonth.Text = currentDate.Year.ToString() + "年" + currentDate.AddMonths(-1).Month.ToString() + "月";
                    }
                    this.cboMonth.SelectedIndex = this.cboMonth.Items.Count - 1;
                }
                else
                {
                    this.cboMonth.SelectedIndex = -1;
                }
            }
        }

        private void InitialMonth_(AisinoCMB cboMonth, int fpzl)
        {
            DateTime currentDate = this.queryPrintBLL.GetCurrentDate();
            List<int> list = new List<int>();
            List<string> list2 = new List<string>();
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            if (fpzl == 0)
            {
                dictionary = this.taxcardEntityBLL.getYearMonthZP();
            }
            else if (fpzl == 11)
            {
                dictionary = this.taxcardEntityBLL.getYearMonthHY();
            }
            else if (fpzl == 12)
            {
                dictionary = this.taxcardEntityBLL.getYearMonthJDC();
            }
            foreach (KeyValuePair<int, List<int>> pair in dictionary)
            {
                list.Add(pair.Key);
            }
            for (int i = 0; i < list.Count; i++)
            {
                List<int> list3 = dictionary[list[i]];
                string str = "";
                if (list[i] == currentDate.Year)
                {
                    str = "本年";
                }
                else
                {
                    str = list[i].ToString() + "年";
                }
                for (int j = 0; j < list3.Count; j++)
                {
                    list2.Add(str + list3[j].ToString() + "月");
                }
            }
            string str2 = list2[list2.Count - 1];
            int index = str2.IndexOf("年");
            if (index >= 0)
            {
                string str3 = str2.Substring(0, index);
                int year = 0;
                if (str3 == "本")
                {
                    year = currentDate.Year;
                }
                else
                {
                    year = Convert.ToInt32(str3);
                }
                int num5 = Convert.ToInt32(str2.Substring(index + 1, (str2.IndexOf("月") - index) - 1));
                for (int k = 0; k < list2.Count; k++)
                {
                    cboMonth.Items.Add(list2[k]);
                }
                if (cboMonth.Items.Count > 0)
                {
                    if ((year == currentDate.Year) && (num5 == currentDate.Month))
                    {
                        cboMonth.Text = "本年" + currentDate.Month.ToString() + "月";
                    }
                    else if (currentDate.Month == 1)
                    {
                        cboMonth.Text = currentDate.AddMonths(-1).Year.ToString() + "年12月";
                    }
                    else
                    {
                        cboMonth.Text = currentDate.Year.ToString() + "年" + currentDate.AddMonths(-1).Month.ToString() + "月";
                    }
                    cboMonth.SelectedIndex = cboMonth.Items.Count - 1;
                }
                else
                {
                    cboMonth.SelectedIndex = -1;
                }
            }
        }

        private void InitialMonthPrint()
        {
            if (this.chkZZSZYFP.Enabled)
            {
                this.InitialMonth_(this.cmbXZYF1, 0);
            }
            if (this.chkZZSPTFP.Enabled)
            {
                this.InitialMonth_(this.cmbXZYF2, 0);
            }
            if (this.chkHWYS.Enabled)
            {
                this.InitialMonth_(this.cmbXZYF3, 11);
            }
            if (this.chkJDC.Enabled)
            {
                this.InitialMonth_(this.cmbXZYF4, 12);
            }
        }

        private void InitialPeriod_()
        {
            this.cboShuiqi.Items.Clear();
            string text = this.cboMonth.Text;
            int index = text.IndexOf("年");
            if (index >= 0)
            {
                string str2 = text.Substring(0, index);
                string str3 = text.Substring(index + 1, (text.IndexOf("月") - index) - 1);
                int year = 0;
                int month = 0;
                if (str2 == "本")
                {
                    year = this.nYear;
                }
                else
                {
                    year = Convert.ToInt32(str2);
                }
                month = Convert.ToInt32(str3);
                this.cboShuiqi.Items.Add("本月累计");
                if (this.queryPrintBLL.GetTaxcardVersion())
                {
                    int num4 = -1;
                    DateTime time = new DateTime(year, month, 1);
                    if (this.cboFPType.SelectedItem.Equals("增值税专用发票") || this.cboFPType.SelectedItem.Equals("增值税普通发票"))
                    {
                        int num5 = base.TaxCardInstance.get_LastRepDateYear();
                        int num6 = base.TaxCardInstance.get_LastRepDateMonth();
                        DateTime time2 = new DateTime(num5, num6, 1);
                        if (DateTime.Compare(time2, time) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(0)[1];
                        }
                        else if (DateTime.Compare(time, time2.AddMonths(-1)) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(0)[0];
                        }
                        else
                        {
                            num4 = 0;
                        }
                    }
                    if (this.cboFPType.SelectedItem.Equals("货物运输业增值税专用发票"))
                    {
                        string lastRepDateHY = this.lastRepDateHY;
                        int num7 = -1;
                        int num8 = -1;
                        if ((lastRepDateHY.Length > 0) && lastRepDateHY.Contains("-"))
                        {
                            num8 = int.Parse(lastRepDateHY.Split(new char[] { '-' })[0]);
                            num7 = int.Parse(lastRepDateHY.Split(new char[] { '-' })[1]);
                        }
                        DateTime time3 = new DateTime(num8, num7, 1);
                        if (DateTime.Compare(time3, time) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(11)[1];
                        }
                        else if (DateTime.Compare(time, time3.AddMonths(-1)) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(11)[0];
                        }
                        else
                        {
                            num4 = 0;
                        }
                    }
                    if (this.cboFPType.SelectedItem.Equals("机动车销售统一发票"))
                    {
                        string lastRepDateJDC = this.lastRepDateJDC;
                        int num9 = -1;
                        int num10 = -1;
                        if ((lastRepDateJDC.Length > 0) && lastRepDateJDC.Contains("-"))
                        {
                            num10 = int.Parse(lastRepDateJDC.Split(new char[] { '-' })[0]);
                            num9 = int.Parse(lastRepDateJDC.Split(new char[] { '-' })[1]);
                        }
                        DateTime time4 = new DateTime(num10, num9, 1);
                        if (DateTime.Compare(time4, time) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(12)[1];
                        }
                        else if (DateTime.Compare(time, time4.AddMonths(-1)) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(12)[0];
                        }
                        else
                        {
                            num4 = 0;
                        }
                    }
                    if (num4 > 0)
                    {
                        for (int i = 0; i < num4; i++)
                        {
                            int num19 = i + 1;
                            this.cboShuiqi.Items.Add("第" + num19.ToString() + "期");
                        }
                    }
                    if (this.cboShuiqi.Items.Count > 0)
                    {
                        this.cboShuiqi.SelectedIndex = this.cboShuiqi.Items.Count - 1;
                    }
                    else
                    {
                        this.cboShuiqi.SelectedIndex = -1;
                    }
                }
                else
                {
                    string currentMonth = this.queryPrintBLL.GetCurrentMonth();
                    int length = currentMonth.IndexOf("-");
                    if (length >= 0)
                    {
                        string str7 = currentMonth.Substring(0, length);
                        string str8 = currentMonth.Substring(length + 1);
                        int num13 = Convert.ToInt32(str7);
                        int num14 = Convert.ToInt32(str8);
                        if ((year == num13) && (month == num14))
                        {
                            int currentRepPeriod = this.queryPrintBLL.GetCurrentRepPeriod();
                            if (currentRepPeriod > 0)
                            {
                                for (int j = 0; j < currentRepPeriod; j++)
                                {
                                    int num20 = j + 1;
                                    this.cboShuiqi.Items.Add("第" + num20.ToString() + "期");
                                }
                            }
                            if (this.cboShuiqi.Items.Count > 0)
                            {
                                this.cboShuiqi.SelectedIndex = this.cboShuiqi.Items.Count - 1;
                            }
                            else
                            {
                                this.cboShuiqi.SelectedIndex = -1;
                            }
                        }
                        else if (((year == num13) && ((month + 1) == num14)) || ((((year + 1) == num13) && (month == 12)) && (num14 == 1)))
                        {
                            int lastRepPeroid = this.queryPrintBLL.GetLastRepPeroid();
                            if (lastRepPeroid > 0)
                            {
                                for (int k = 0; k < lastRepPeroid; k++)
                                {
                                    int num21 = k + 1;
                                    this.cboShuiqi.Items.Add("第" + num21.ToString() + "期");
                                }
                                this.cboShuiqi.Text = "第1期";
                            }
                            else
                            {
                                this.cboShuiqi.Text = "本月累计";
                            }
                        }
                        else if (this.cboShuiqi.Items.Count > 0)
                        {
                            this.cboShuiqi.SelectedIndex = 0;
                        }
                    }
                }
            }
        }

        private void InitialPeriod_(AisinoCMB cboMonth, AisinoCMB cboShuiqi, int invType)
        {
            cboShuiqi.Items.Clear();
            string text = cboMonth.Text;
            int index = text.IndexOf("年");
            if (index >= 0)
            {
                string str2 = text.Substring(0, index);
                string str3 = text.Substring(index + 1, (text.IndexOf("月") - index) - 1);
                int year = 0;
                int month = 0;
                if (str2 == "本")
                {
                    year = this.nYear;
                }
                else
                {
                    year = Convert.ToInt32(str2);
                }
                month = Convert.ToInt32(str3);
                DateTime time = new DateTime(year, month, 1);
                cboShuiqi.Items.Add("本月累计");
                if (this.queryPrintBLL.GetTaxcardVersion())
                {
                    int num4 = -1;
                    if (invType == 0)
                    {
                        int num5 = base.TaxCardInstance.get_LastRepDateYear();
                        int num6 = base.TaxCardInstance.get_LastRepDateMonth();
                        DateTime time2 = new DateTime(num5, num6, 1);
                        if (DateTime.Compare(time2, time) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(0)[1];
                        }
                        else if (DateTime.Compare(time, time2.AddMonths(-1)) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(0)[0];
                        }
                        else
                        {
                            num4 = 0;
                        }
                    }
                    if (invType == 11)
                    {
                        string lastRepDateHY = this.lastRepDateHY;
                        int num7 = -1;
                        int num8 = -1;
                        if ((lastRepDateHY.Length > 0) && lastRepDateHY.Contains("-"))
                        {
                            num8 = int.Parse(lastRepDateHY.Split(new char[] { '-' })[0]);
                            num7 = int.Parse(lastRepDateHY.Split(new char[] { '-' })[1]);
                        }
                        DateTime time3 = new DateTime(num8, num7, 1);
                        if (DateTime.Compare(time3, time) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(11)[1];
                        }
                        else if (DateTime.Compare(time, time3.AddMonths(-1)) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(11)[0];
                        }
                        else
                        {
                            num4 = 0;
                        }
                    }
                    if (invType == 12)
                    {
                        string lastRepDateJDC = this.lastRepDateJDC;
                        int num9 = -1;
                        int num10 = -1;
                        if ((lastRepDateJDC.Length > 0) && lastRepDateJDC.Contains("-"))
                        {
                            num10 = int.Parse(lastRepDateJDC.Split(new char[] { '-' })[0]);
                            num9 = int.Parse(lastRepDateJDC.Split(new char[] { '-' })[1]);
                        }
                        DateTime time4 = new DateTime(num10, num9, 1);
                        if (DateTime.Compare(time4, time) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(12)[1];
                        }
                        else if (DateTime.Compare(time, time4.AddMonths(-1)) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(12)[0];
                        }
                        else
                        {
                            num4 = 0;
                        }
                    }
                    if (num4 > 0)
                    {
                        for (int i = 0; i < num4; i++)
                        {
                            int num19 = i + 1;
                            cboShuiqi.Items.Add("第" + num19.ToString() + "期");
                        }
                    }
                    if (cboShuiqi.Items.Count > 0)
                    {
                        cboShuiqi.SelectedIndex = cboShuiqi.Items.Count - 1;
                    }
                    else
                    {
                        cboShuiqi.SelectedIndex = -1;
                    }
                }
                else
                {
                    string currentMonth = this.queryPrintBLL.GetCurrentMonth();
                    int length = currentMonth.IndexOf("-");
                    if (length >= 0)
                    {
                        string str7 = currentMonth.Substring(0, length);
                        string str8 = currentMonth.Substring(length + 1);
                        int num13 = Convert.ToInt32(str7);
                        int num14 = Convert.ToInt32(str8);
                        if ((year == num13) && (month == num14))
                        {
                            int currentRepPeriod = this.queryPrintBLL.GetCurrentRepPeriod();
                            if (currentRepPeriod > 0)
                            {
                                for (int j = 0; j < currentRepPeriod; j++)
                                {
                                    int num20 = j + 1;
                                    cboShuiqi.Items.Add("第" + num20.ToString() + "期");
                                }
                            }
                            if (cboShuiqi.Items.Count > 0)
                            {
                                cboShuiqi.SelectedIndex = cboShuiqi.Items.Count - 1;
                            }
                            else
                            {
                                cboShuiqi.SelectedIndex = -1;
                            }
                        }
                        else if (((year == num13) && ((month + 1) == num14)) || ((((year + 1) == num13) && (month == 12)) && (num14 == 1)))
                        {
                            int lastRepPeroid = this.queryPrintBLL.GetLastRepPeroid();
                            if (lastRepPeroid > 0)
                            {
                                for (int k = 0; k < lastRepPeroid; k++)
                                {
                                    int num21 = k + 1;
                                    cboShuiqi.Items.Add("第" + num21.ToString() + "期");
                                }
                                cboShuiqi.Text = "第1期";
                            }
                            else
                            {
                                cboShuiqi.Text = "本月累计";
                            }
                        }
                        else if (cboShuiqi.Items.Count > 0)
                        {
                            cboShuiqi.SelectedIndex = 0;
                        }
                    }
                }
            }
        }

        private void InitialPeriodprint()
        {
            if (this.chkZZSZYFP.Enabled)
            {
                this.InitialPeriod_(this.cmbXZYF1, this.cmbXZSQ1, 0);
            }
            if (this.chkZZSPTFP.Enabled)
            {
                this.InitialPeriod_(this.cmbXZYF2, this.cmbXZSQ2, 0);
            }
            if (this.chkHWYS.Enabled)
            {
                this.InitialPeriod_(this.cmbXZYF3, this.cmbXZSQ3, 11);
            }
            if (this.chkJDC.Enabled)
            {
                this.InitialPeriod_(this.cmbXZYF4, this.cmbXZSQ4, 12);
            }
        }

        private void LoadInvType()
        {
            try
            {
                this.m_InvTypeEntityList = this.m_commFun.GetInvTypeCollect();
                if (this.m_InvTypeEntityList == null)
                {
                    MessageManager.ShowMsgBox("INP-251303", new string[] { "获取发票种类失败" });
                }
                this.cboFPType.Items.Clear();
                for (int i = 0; i < this.m_InvTypeEntityList.Count; i++)
                {
                    this.cboFPType.Items.Add(this.m_InvTypeEntityList[i].m_strInvName);
                }
                if (this.cboFPType.Items.Count > 0)
                {
                    this.cboFPType.SelectedIndex = 0;
                }
                this.m_checkBoxList.Clear();
                int num2 = 0;
                int num3 = 0;
                int right = 0;
                int bottom = 0;
                for (int j = 0; j < this.m_InvTypeEntityList.Count; j++)
                {
                    AisinoCHK item = new AisinoCHK {
                        Text = this.m_InvTypeEntityList[j].m_strInvName,
                        Checked = true
                    };
                    if (j == 0)
                    {
                        num2 = 15;
                        num3 = 10;
                    }
                    else
                    {
                        num3 = right + 5;
                    }
                    if (right > this.groupBox2.Width)
                    {
                        num2 = bottom + 1;
                        num3 = 10;
                    }
                    item.Top = num2;
                    item.Left = num3;
                    right = item.Right;
                    bottom = item.Bottom;
                    item.Parent = this.groupBox2;
                    this.m_checkBoxList.Add(item);
                }
                for (int k = 0; k < this.m_checkBoxList.Count; k++)
                {
                    this.m_checkBoxList[k].Visible = false;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void LoadInvTypePrint()
        {
            this.chkZZSZYFP.Enabled = false;
            this.chkZZSPTFP.Enabled = false;
            this.chkHWYS.Enabled = false;
            this.chkJDC.Enabled = false;
            this.cmbXZSQ1.Enabled = false;
            this.cmbXZSQ2.Enabled = false;
            this.cmbXZSQ3.Enabled = false;
            this.cmbXZSQ4.Enabled = false;
            this.cmbXZYF1.Enabled = false;
            this.cmbXZYF2.Enabled = false;
            this.cmbXZYF3.Enabled = false;
            this.cmbXZYF4.Enabled = false;
            foreach (InvTypeEntity entity in this.m_InvTypeEntityList)
            {
                if (entity.m_invType == INV_TYPE.INV_SPECIAL)
                {
                    this.chkZZSZYFP.Enabled = true;
                    this.cmbXZSQ1.Enabled = true;
                    this.cmbXZYF1.Enabled = true;
                }
                if (entity.m_invType == INV_TYPE.INV_COMMON)
                {
                    this.chkZZSPTFP.Enabled = true;
                    this.cmbXZSQ2.Enabled = true;
                    this.cmbXZYF2.Enabled = true;
                }
                if (entity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                {
                    this.chkHWYS.Enabled = true;
                    this.cmbXZSQ3.Enabled = true;
                    this.cmbXZYF3.Enabled = true;
                }
                if (entity.m_invType == INV_TYPE.INV_VEHICLESALES)
                {
                    this.chkJDC.Enabled = true;
                    this.cmbXZSQ4.Enabled = true;
                    this.cmbXZYF4.Enabled = true;
                }
            }
        }

        private void rbtnPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnPrint.Checked)
            {
                this.groupBox2.Visible = false;
                this.groupBox3.Visible = false;
                this.groupBox4.Visible = true;
                this.groupBoxTJXZ.Visible = true;
            }
        }

        private void rbtnQuery_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnQuery.Checked)
            {
                this.groupBox2.Visible = true;
                this.groupBox3.Visible = true;
                this.groupBox4.Visible = false;
                this.groupBoxTJXZ.Visible = false;
            }
        }

        private void SerialPrint()
        {
            this.m_PrintEntityList.Clear();
            this.m_QueryPrintEntityList.Clear();
            try
            {
                for (int i = 0; i < this.m_InvTypeEntityListPrint.Count; i++)
                {
                    PrintEntity item = new PrintEntity {
                        m_InvTypeEntity = this.m_InvTypeEntityListPrint[i]
                    };
                    QueryPrintEntity queryPrintEntity = new QueryPrintEntity();
                    for (int k = 0; k < this.m_ItemsList.Count; k++)
                    {
                        item.m_ItemEntity.Add(this.m_ItemsList[k]);
                    }
                    if (item.m_InvTypeEntity.m_invType == INV_TYPE.INV_SPECIAL)
                    {
                        this.setQueryPrintEntity(queryPrintEntity, this.cmbXZYF1, this.cmbXZSQ1);
                    }
                    if (item.m_InvTypeEntity.m_invType == INV_TYPE.INV_COMMON)
                    {
                        this.setQueryPrintEntity(queryPrintEntity, this.cmbXZYF2, this.cmbXZSQ2);
                    }
                    if (item.m_InvTypeEntity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                    {
                        this.setQueryPrintEntity(queryPrintEntity, this.cmbXZYF3, this.cmbXZSQ3);
                    }
                    if (item.m_InvTypeEntity.m_invType == INV_TYPE.INV_VEHICLESALES)
                    {
                        this.setQueryPrintEntity(queryPrintEntity, this.cmbXZYF4, this.cmbXZSQ4);
                    }
                    queryPrintEntity.m_bPrint = true;
                    if (this.queryPrintBLL.IsLocked())
                    {
                        queryPrintEntity.m_bIsLocked = true;
                    }
                    this.m_PrintEntityList.Add(item);
                    this.m_QueryPrintEntityList.Add(queryPrintEntity);
                }
                bool flag = true;
                for (int j = 0; j < this.m_InvTypeEntityListPrint.Count; j++)
                {
                    for (int m = 0; m < this.m_PrintEntityList[j].m_ItemEntity.Count; m++)
                    {
                        this.m_QueryPrintEntityList[j].m_invType = this.m_PrintEntityList[j].m_InvTypeEntity.m_invType;
                        string strInvName = this.m_PrintEntityList[j].m_InvTypeEntity.m_strInvName;
                        this.m_QueryPrintEntityList[j].m_itemAction = this.m_PrintEntityList[j].m_ItemEntity[m].m_ItemType;
                        this.m_QueryPrintEntityList[j].m_strSubItem = strInvName + "统计表  1-0" + ((m + 1)).ToString();
                        if (this.m_QueryPrintEntityList[j].m_nTaxPeriod == 0)
                        {
                            this.m_QueryPrintEntityList[j].m_strItemDetail = this.m_PrintEntityList[j].m_ItemEntity[m].m_strItemName + "(" + this.m_QueryPrintEntityList[j].m_nYear.ToString() + "年" + this.m_QueryPrintEntityList[j].m_nMonth.ToString() + "月)";
                        }
                        else
                        {
                            this.m_QueryPrintEntityList[j].m_strItemDetail = this.m_PrintEntityList[j].m_ItemEntity[m].m_strItemName + "(" + this.m_QueryPrintEntityList[j].m_nYear.ToString() + "年" + this.m_QueryPrintEntityList[j].m_nMonth.ToString() + "月第" + this.m_QueryPrintEntityList[j].m_nTaxPeriod.ToString() + "期)";
                        }
                        if (this.m_PrintEntityList[j].m_ItemEntity[m].m_ItemType == ITEM_ACTION.ITEM_TOTAL)
                        {
                            this.m_QueryPrintEntityList[j].m_strTitle = strInvName + "汇总表";
                            InvStatForm form = new InvStatForm(this.m_QueryPrintEntityList[j]);
                            if (form.PrintTable(flag))
                            {
                                string str2 = this.m_QueryPrintEntityList[j].m_strSubItem + "\r\n" + this.m_QueryPrintEntityList[j].m_strItemDetail + "\r\n\t打印成功！";
                                MessageManager.ShowMsgBox("INP-251303", new string[] { str2 });
                            }
                            else
                            {
                                string str3 = this.m_QueryPrintEntityList[j].m_strSubItem + "\r\n" + this.m_QueryPrintEntityList[j].m_strItemDetail + "\r\n打印失败或者取消打印！";
                                MessageManager.ShowMsgBox("INP-251303", new string[] { str3 });
                            }
                        }
                        else
                        {
                            this.m_QueryPrintEntityList[j].m_strTitle = strInvName + "明细表";
                            InvoiceResultForm form2 = new InvoiceResultForm(this.m_QueryPrintEntityList[j]);
                            if (form2.PrintTable(flag))
                            {
                                string str4 = this.m_QueryPrintEntityList[j].m_strSubItem + "\r\n" + this.m_QueryPrintEntityList[j].m_strItemDetail + "\r\n\t打印成功！";
                                MessageManager.ShowMsgBox("INP-251303", new string[] { str4 });
                            }
                            else
                            {
                                string str5 = this.m_QueryPrintEntityList[j].m_strSubItem + "\r\n" + this.m_QueryPrintEntityList[j].m_strItemDetail + "\r\n打印失败或者取消打印！";
                                MessageManager.ShowMsgBox("INP-251303", new string[] { str5 });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        private void setQueryPrintEntity(QueryPrintEntity queryPrintEntity, AisinoCMB cboMonth, AisinoCMB cboShuiqi)
        {
            if (cboMonth.Text.Trim() != "")
            {
                string text = cboMonth.Text;
                int index = text.IndexOf("年");
                if (index >= 0)
                {
                    text = text.Substring(0, index);
                    if (text == "本")
                    {
                        queryPrintEntity.m_nYear = this.nYear;
                    }
                    else
                    {
                        queryPrintEntity.m_nYear = Convert.ToInt32(text);
                    }
                    text = cboMonth.Text;
                    index = text.IndexOf("年");
                    if (index >= 0)
                    {
                        text = text.Substring(index + 1, (text.IndexOf("月") - index) - 1);
                        queryPrintEntity.m_nMonth = Convert.ToInt32(text);
                        if (cboShuiqi.Text.Trim() != "")
                        {
                            if (cboShuiqi.Text == "本月累计")
                            {
                                queryPrintEntity.m_nTaxPeriod = 0;
                            }
                            else
                            {
                                string str2 = cboShuiqi.Text;
                                int num2 = str2.IndexOf("第");
                                if (num2 >= 0)
                                {
                                    str2 = str2.Substring(num2 + 1, (str2.IndexOf("期") - num2) - 1);
                                    queryPrintEntity.m_nTaxPeriod = Convert.ToInt32(str2);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

