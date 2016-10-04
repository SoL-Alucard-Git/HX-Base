namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using Aisino.Fwkp.Wbjk.Properties;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class MannualSplit : Form
    {
        private SaleBill bill;
        private SaleBillCtrl billBL;
        private AisinoBTN btnCancelChaiFen;
        private AisinoBTN btnCancelFZ;
        private AisinoBTN btnCFZKZ;
        private AisinoBTN btnSGCF;
        private AisinoBTN buttonGroup;
        private IContainer components;
        private DataGridMX dgvAferBillHeader;
        private DataGridMX dgvAfterGoods;
        private CustomStyleDataGrid dgvBillHader;
        private DataGridMX dgvSplit;
        private int fenzuflag;
        private AisinoGRP groupBox1;
        private AisinoGRP groupBox2;
        private AisinoGRP groupBox3;
        private AisinoGRP groupBox4;
        private AisinoGRP groupBox5;
        private AisinoGRP groupBox6;
        private AisinoGRP groupBox7;
        private double HJSE;
        private AisinoPNL panel1;
        private AisinoRDO rdBtnDH;
        private AisinoRDO rdBtnMH;
        private AisinoRDO rdBtnXH;
        private List<int> ReadOnlyColumnIndex;
        private int[] slvjeseIndex;
        private AisinoSPL splitContainer1;
        private AisinoSPL splitContainer2;
        private AisinoSPL splitContainer3;
        private List<SaleBill> splitedBills;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnDoCF;
        private ToolStripButton toolStripButton2;
        private List<string> ValidatColumns;

        public MannualSplit()
        {
            this.fenzuflag = 0;
            this.HJSE = 0.0;
            this.billBL = SaleBillCtrl.Instance;
            this.bill = null;
            this.splitedBills = null;
            this.ValidatColumns = new List<string>();
            this.ReadOnlyColumnIndex = new List<int>();
            this.components = null;
            this.InitializeComponent();
        }

        internal MannualSplit(SaleBill bill)
        {
            this.fenzuflag = 0;
            this.HJSE = 0.0;
            this.billBL = SaleBillCtrl.Instance;
            this.bill = null;
            this.splitedBills = null;
            this.ValidatColumns = new List<string>();
            this.ReadOnlyColumnIndex = new List<int>();
            this.components = null;
            this.Initialize();
            this.bill = bill;
            this.slvjeseIndex = GenerateInvoice.Instance.SetInvoiceTimes();
            this.splitedBills = new List<SaleBill>();
            foreach (Goods goods in bill.ListGoods)
            {
                this.HJSE += goods.SE;
            }
            this.HJSE = SaleBillCtrl.GetRound(this.HJSE, 2);
        }

        private void btnCancelGroup_Click(object sender, EventArgs e)
        {
            this.fenzuflag = 0;
            this.splitedBills.Clear();
            for (int i = 0; i < this.dgvSplit.Rows.Count; i++)
            {
                this.dgvSplit.Rows[i].Cells[0].Value = 0;
                this.bill.ListGoods[i].Reserve = "0";
            }
            this.btnSGCF.Enabled = false;
            this.toolStripBtnDoCF.Enabled = false;
            this.btnCancelFZ.Enabled = false;
        }

        private void btnCancelSplit_Click(object sender, EventArgs e)
        {
            try
            {
                this.fenzuflag = 0;
                this.splitedBills.Clear();
                foreach (DataGridViewRow row in (IEnumerable) this.dgvSplit.Rows)
                {
                    row.Cells["group"].Value = 0;
                }
                this.ToViewSplited(this.splitedBills);
                this.dgvAfterGoods.Rows.Clear();
                this.btnCancelChaiFen.Enabled = false;
                this.btnSGCF.Enabled = false;
                this.btnCancelFZ.Enabled = false;
                this.buttonGroup.Enabled = true;
                this.btnCFZKZ.Enabled = true;
                this.toolStripBtnDoCF.Enabled = false;
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void btnSetGroup_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> listXH = new List<int>();
                foreach (DataGridViewCell cell in this.dgvSplit.SelectedCells)
                {
                    if (!listXH.Contains(cell.RowIndex))
                    {
                        listXH.Add(cell.RowIndex);
                    }
                }
                this.dgvSplit.ClearSelection();
                if (listXH.Count > 0)
                {
                    string index = this.dgvSplit.Rows[listXH[0]].Cells["group"].Value.ToString();
                    string str2 = this.CheckSetGroup(listXH, this.fenzuflag, index, false);
                    this.fenzuflag++;
                    if (str2 != "0")
                    {
                        this.fenzuflag--;
                    }
                    else
                    {
                        for (int i = 0; i < listXH.Count; i++)
                        {
                            DataGridViewRow row = this.dgvSplit.Rows[listXH[i]];
                            row.Cells["group"].Value = this.fenzuflag;
                            this.bill.ListGoods[listXH[i]].Reserve = this.fenzuflag.ToString();
                        }
                        this.btnCancelChaiFen.Enabled = false;
                        this.btnSGCF.Enabled = true;
                        this.btnCancelFZ.Enabled = true;
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void btnSplitDiscountGroup_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> listSelectedRows = new List<int>();
                foreach (DataGridViewCell cell in this.dgvSplit.SelectedCells)
                {
                    if (!listSelectedRows.Contains(cell.RowIndex))
                    {
                        listSelectedRows.Add(cell.RowIndex);
                    }
                }
                string str = this.billBL.SplitDiscountGroup(this.bill, listSelectedRows);
                if (str != "0")
                {
                    MessageManager.ShowMsgBox(str);
                }
                else
                {
                    this.ToViewSplit(this.bill);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void btnSplitView_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> listXH = new List<int>();
                foreach (DataGridViewRow row in (IEnumerable) this.dgvSplit.Rows)
                {
                    if ((row.Cells["group"].Value.ToString() == "0") && !listXH.Contains(row.Index))
                    {
                        listXH.Add(row.Index);
                    }
                }
                if (this.CheckSetGroup(listXH, 0, "0", true) == "0")
                {
                    int num;
                    List<int> list2 = new List<int>();
                    foreach (DataGridViewRow row in (IEnumerable) this.dgvSplit.Rows)
                    {
                        num = Convert.ToInt32(row.Cells["group"].Value);
                        if (!list2.Contains(num))
                        {
                            list2.Add(num);
                        }
                    }
                    list2.Sort();
                    for (int i = 0; i < list2.Count; i++)
                    {
                        SaleBill bill = new SaleBill(this.bill);
                        string str2 = string.Format("{0}_{1}", this.bill.BH, i);
                        if (ToolUtil.GetByteCount(str2) > 50)
                        {
                            MessageManager.ShowMsgBox("拆分后单据号长度不可超过50，请重新分组!");
                            return;
                        }
                        bill.BH = str2;
                        foreach (DataGridViewRow row in (IEnumerable) this.dgvSplit.Rows)
                        {
                            num = Convert.ToInt32(row.Cells["group"].Value);
                            if (list2[i] == num)
                            {
                                bill.ListGoods.Add(this.bill.ListGoods[row.Index]);
                            }
                        }
                        this.billBL.CleanBill(bill);
                        string str3 = this.billBL.CheckSetOneGroup(bill, this.slvjeseIndex);
                        if (str3 != "0")
                        {
                            if (str3.Equals("该组单据金额不能为0!"))
                            {
                                str3 = string.Format("分组编号为{0}的单据金额不能为0!", list2[i]);
                            }
                            MessageManager.ShowMsgBox(str3);
                            return;
                        }
                        this.splitedBills.Add(bill);
                    }
                    this.ToViewSplited(this.splitedBills);
                    this.btnCancelChaiFen.Enabled = true;
                    this.btnSGCF.Enabled = false;
                    this.btnCancelFZ.Enabled = false;
                    this.buttonGroup.Enabled = false;
                    this.btnCFZKZ.Enabled = false;
                    this.toolStripBtnDoCF.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private string CheckSetGroup(List<int> listXH, int groupID, string index = "0", bool view = false)
        {
            if (listXH.Count != 0)
            {
                int num2;
                listXH.Sort();
                SaleBill bill = new SaleBill(this.bill) {
                    BH = string.Format("{0}_{1}", this.bill.BH, groupID)
                };
                double num = 0.0;
                for (num2 = 0; num2 < this.bill.ListGoods.Count; num2++)
                {
                    double sL;
                    double dJ;
                    double jE;
                    string str;
                    double sE;
                    int num10;
                    num += this.bill.ListGoods[num2].SE;
                    if (this.bill.ListGoods[num2].HSJBZ)
                    {
                        if (this.bill.HYSY && (this.bill.ListGoods[num2].SLV == 0.05))
                        {
                            sL = this.bill.ListGoods[num2].SL;
                            dJ = this.bill.ListGoods[num2].DJ;
                            jE = this.bill.ListGoods[num2].JE + this.bill.ListGoods[num2].SE;
                            if ((sL != 0.0) || !(dJ == 0.0))
                            {
                                double num6 = sL * dJ;
                                double num7 = num6 - jE;
                                if (Math.Abs(SaleBillCtrl.GetRound((double) ((sL * dJ) - jE), 6)) > 0.01)
                                {
                                    num10 = num2 + 1;
                                    str = num10.ToString();
                                    MessageManager.ShowMsgBox("第" + str + "行单价乘以数量不等于含税金额");
                                    return "2";
                                }
                            }
                        }
                        else if (this.bill.JZ_50_15 && (this.bill.ListGoods[num2].SLV == 0.015))
                        {
                            sL = this.bill.ListGoods[num2].SL;
                            dJ = this.bill.ListGoods[num2].DJ;
                            jE = this.bill.ListGoods[num2].JE;
                            sE = this.bill.ListGoods[num2].SE;
                            if ((sL != 0.0) || !(dJ == 0.0))
                            {
                                if (Math.Abs(SaleBillCtrl.GetRound((double) (((sL * dJ) - jE) - sE), 6)) > 0.01)
                                {
                                    num10 = num2 + 1;
                                    str = num10.ToString();
                                    MessageManager.ShowMsgBox("第" + str + "行单价乘以数量不等于含税金额");
                                    return "2";
                                }
                                if ((jE < 0.0) || (sE < 0.0))
                                {
                                    num10 = num2 + 1;
                                    str = num10.ToString();
                                    MessageManager.ShowMsgBox("第" + str + "行，金额与税额没有同时为正数");
                                    return "2";
                                }
                            }
                        }
                        else
                        {
                            sL = this.bill.ListGoods[num2].SL;
                            dJ = this.bill.ListGoods[num2].DJ;
                            jE = this.bill.ListGoods[num2].JE;
                            sE = this.bill.ListGoods[num2].SE;
                            if (((sL != 0.0) || !(dJ == 0.0)) && (Math.Abs(SaleBillCtrl.GetRound((double) (((sL * dJ) - jE) - sE), 6)) > 0.01))
                            {
                                num10 = num2 + 1;
                                str = num10.ToString();
                                MessageManager.ShowMsgBox("第" + str + "行单价乘以数量不等于含税金额");
                                return "2";
                            }
                        }
                    }
                    else
                    {
                        sL = this.bill.ListGoods[num2].SL;
                        dJ = this.bill.ListGoods[num2].DJ;
                        jE = this.bill.ListGoods[num2].JE;
                        if ((sL != 0.0) || !(dJ == 0.0))
                        {
                            if (Math.Abs(SaleBillCtrl.GetRound((double) ((sL * dJ) - jE), 6)) > 0.01)
                            {
                                num10 = num2 + 1;
                                str = num10.ToString();
                                MessageManager.ShowMsgBox("第" + str + "行单价乘以数量不等于金额");
                                return "2";
                            }
                            if (this.bill.JZ_50_15 && (this.bill.ListGoods[num2].SLV == 0.015))
                            {
                                sE = this.bill.ListGoods[num2].SE;
                                if ((jE < 0.0) || (sE < 0.0))
                                {
                                    str = (num2 + 1).ToString();
                                    MessageManager.ShowMsgBox("第" + str + "行，金额与税额没有同时为正数");
                                    return "2";
                                }
                            }
                        }
                    }
                }
                if (!(SaleBillCtrl.GetRound(num, 2) == this.HJSE))
                {
                    MessageManager.ShowMsgBox("拆分前后税额不一致");
                    return "2";
                }
                for (num2 = 0; num2 < listXH.Count; num2++)
                {
                    bill.ListGoods.Add(this.bill.ListGoods[listXH[num2]]);
                }
                this.billBL.CleanBill(bill);
                string str2 = this.billBL.CheckSetOneGroup(bill, this.slvjeseIndex);
                if (str2 != "0")
                {
                    if (str2.Equals("该组单据金额不能为0!"))
                    {
                        str2 = "分组编号为" + index + "的单据金额不能为0!";
                    }
                    if (view)
                    {
                        if (str2.EndsWith("金额乘以税率减税额的绝对值大于0.06!") || str2.EndsWith("含税金额乘以税率减税额的绝对值大于0.06!"))
                        {
                            str2 = str2.Substring(3, str2.Length - 3);
                            str2 = string.Format("分组编号为{0}的", index) + str2;
                        }
                        else if (str2.Equals("该组单据超过开票限额，不能进行拆分!"))
                        {
                            str2 = str2.Substring(2, str2.Length - 2);
                            str2 = string.Format("分组编号为{0}的组，", index) + str2;
                        }
                        else if (str2.Equals("该组单据含税金额超过开票限额，不能进行拆分!"))
                        {
                            str2 = str2.Substring(4, str2.Length - 4);
                            str2 = string.Format("分组编号为{0}的组，", index) + str2;
                        }
                    }
                    MessageManager.ShowMsgBox(str2);
                    return "2";
                }
            }
            return "0";
        }

        private int Compare(SaleBill bill1, SaleBill bill2)
        {
            return bill1.BH.CompareTo(bill2.BH);
        }

        private void dataGridMX3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    this.ToViewSplitedGoodlist(this.splitedBills[e.RowIndex]);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void dataGridMX4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (Convert.ToInt32(this.dgvAfterGoods.Rows[e.RowIndex].Cells["DJHXZ"].Value))
            {
                case 3:
                    e.CellStyle.BackColor = Color.LightCyan;
                    break;

                case 4:
                    e.CellStyle.BackColor = Color.LightBlue;
                    break;
            }
        }

        private void dgvSplit_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.ReadOnlyColumnIndex.Contains(e.ColumnIndex))
            {
                e.CellStyle.BackColor = Color.LightYellow;
            }
            switch (Convert.ToInt32(this.dgvSplit.Rows[e.RowIndex].Cells["DJHXZ"].Value))
            {
                case 3:
                    if (this.ReadOnlyColumnIndex.Contains(e.ColumnIndex))
                    {
                        e.CellStyle.BackColor = Color.LightCyan;
                    }
                    break;

                case 4:
                    e.CellStyle.BackColor = Color.LightBlue;
                    break;
            }
        }

        private void dgvSplit_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string name = this.dgvSplit.Columns[e.ColumnIndex].Name;
                if (this.ValidatColumns.Contains(name))
                {
                    int rowIndex = e.RowIndex;
                    object obj2 = this.dgvSplit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string discountSplit = "每行包含折扣";
                    if (this.rdBtnMH.Checked)
                    {
                        discountSplit = "每行包含折扣";
                    }
                    else if (this.rdBtnXH.Checked)
                    {
                        discountSplit = "小行包含折扣";
                    }
                    else if (this.rdBtnDH.Checked)
                    {
                        discountSplit = "大行包含折扣";
                    }
                    string str3 = this.billBL.SplitGoodsMannual(this.bill, rowIndex, name, obj2, discountSplit);
                    this.billBL.CleanBill(this.bill);
                    this.ToViewSplit(this.bill);
                    if (str3 == "非法输入值")
                    {
                        this.dgvSplit.CancelEdit();
                    }
                    else if (str3 != "0")
                    {
                        MessageManager.ShowMsgBox(str3);
                    }
                }
            }
            catch (CustomException exception)
            {
                this.dgvSplit.CancelEdit();
                MessageBoxHelper.Show(exception.Message, "提示");
            }
            catch (Exception exception2)
            {
                HandleException.HandleError(exception2);
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.dgvSplit.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvSplit_CellFormatting);
            this.dgvSplit.CellValueChanged += new DataGridViewCellEventHandler(this.dgvSplit_CellValueChanged);
            this.btnCFZKZ.Click += new EventHandler(this.btnSplitDiscountGroup_Click);
            this.btnCancelChaiFen.Click += new EventHandler(this.btnCancelSplit_Click);
            this.btnSGCF.Click += new EventHandler(this.btnSplitView_Click);
            this.btnCancelFZ.Click += new EventHandler(this.btnCancelGroup_Click);
            this.buttonGroup.Click += new EventHandler(this.btnSetGroup_Click);
            this.dgvAferBillHeader.CellClick += new DataGridViewCellEventHandler(this.dataGridMX3_CellClick);
            this.toolStripBtnDoCF.Click += new EventHandler(this.toolBtnDoCF_Click);
            this.toolStripButton2.Click += new EventHandler(this.toolBtnCancel_Click);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            DataGridViewCellStyle style7 = new DataGridViewCellStyle();
            DataGridViewCellStyle style8 = new DataGridViewCellStyle();
            DataGridViewCellStyle style9 = new DataGridViewCellStyle();
            DataGridViewCellStyle style10 = new DataGridViewCellStyle();
            DataGridViewCellStyle style11 = new DataGridViewCellStyle();
            DataGridViewCellStyle style12 = new DataGridViewCellStyle();
            this.panel1 = new AisinoPNL();
            this.splitContainer1 = new AisinoSPL();
            this.groupBox1 = new AisinoGRP();
            this.splitContainer2 = new AisinoSPL();
            this.groupBox3 = new AisinoGRP();
            this.dgvBillHader = new CustomStyleDataGrid();
            this.groupBox5 = new AisinoGRP();
            this.groupBox7 = new AisinoGRP();
            this.btnCFZKZ = new AisinoBTN();
            this.btnCancelChaiFen = new AisinoBTN();
            this.rdBtnXH = new AisinoRDO();
            this.btnSGCF = new AisinoBTN();
            this.rdBtnDH = new AisinoRDO();
            this.btnCancelFZ = new AisinoBTN();
            this.rdBtnMH = new AisinoRDO();
            this.buttonGroup = new AisinoBTN();
            this.dgvSplit = new DataGridMX();
            this.groupBox2 = new AisinoGRP();
            this.splitContainer3 = new AisinoSPL();
            this.groupBox4 = new AisinoGRP();
            this.dgvAferBillHeader = new DataGridMX();
            this.groupBox6 = new AisinoGRP();
            this.dgvAfterGoods = new DataGridMX();
            this.toolStrip1 = new ToolStrip();
            this.toolStripButton2 = new ToolStripButton();
            this.toolStripBtnDoCF = new ToolStripButton();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.dgvBillHader.BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.dgvSplit.BeginInit();
            this.groupBox2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.dgvAferBillHeader.BeginInit();
            this.groupBox6.SuspendLayout();
            this.dgvAfterGoods.BeginInit();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(800, 0x27c);
            this.panel1.TabIndex = 0;
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0x19);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new Size(800, 0x263);
            this.splitContainer1.SplitterDistance = 0x1a6;
            this.splitContainer1.TabIndex = 4;
            this.groupBox1.Controls.Add(this.splitContainer2);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1a6, 0x263);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "拆分前";
            this.splitContainer2.Dock = DockStyle.Fill;
            this.splitContainer2.Location = new Point(3, 0x11);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = Orientation.Horizontal;
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer2.Size = new Size(0x1a0, 0x24f);
            this.splitContainer2.SplitterDistance = 0x7d;
            this.splitContainer2.TabIndex = 0;
            this.groupBox3.Controls.Add(this.dgvBillHader);
            this.groupBox3.Dock = DockStyle.Fill;
            this.groupBox3.Location = new Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x1a0, 0x7d);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "销售单据";
            this.dgvBillHader.set_AborCellPainting(false);
            this.dgvBillHader.AllowUserToAddRows = false;
            this.dgvBillHader.AllowUserToOrderColumns = true;
            style.BackColor = Color.FromArgb(210, 0xff, 0xff);
            this.dgvBillHader.AlternatingRowsDefaultCellStyle = style;
            this.dgvBillHader.BackgroundColor = Color.WhiteSmoke;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style2.BackColor = SystemColors.Control;
            this.dgvBillHader.ColumnHeadersDefaultCellStyle = style2;
            this.dgvBillHader.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
            this.dgvBillHader.Dock = DockStyle.Fill;
            this.dgvBillHader.GridColor = Color.Gray;
            this.dgvBillHader.set_GridStyle(0);
            this.dgvBillHader.Location = new Point(3, 0x11);
            this.dgvBillHader.Name = "dgvBillHader";
            this.dgvBillHader.ReadOnly = true;
            style3.BackColor = Color.White;
            style3.SelectionBackColor = Color.Teal;
            this.dgvBillHader.RowsDefaultCellStyle = style3;
            this.dgvBillHader.RowTemplate.Height = 0x17;
            this.dgvBillHader.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvBillHader.Size = new Size(410, 0x69);
            this.dgvBillHader.TabIndex = 0;
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.dgvSplit);
            this.groupBox5.Dock = DockStyle.Fill;
            this.groupBox5.Location = new Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x1a0, 0x1ce);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "销售单据明细";
            this.groupBox7.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.groupBox7.Controls.Add(this.btnCFZKZ);
            this.groupBox7.Controls.Add(this.btnCancelChaiFen);
            this.groupBox7.Controls.Add(this.rdBtnXH);
            this.groupBox7.Controls.Add(this.btnSGCF);
            this.groupBox7.Controls.Add(this.rdBtnDH);
            this.groupBox7.Controls.Add(this.btnCancelFZ);
            this.groupBox7.Controls.Add(this.rdBtnMH);
            this.groupBox7.Controls.Add(this.buttonGroup);
            this.groupBox7.Location = new Point(9, 370);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(0x191, 0x56);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.btnCFZKZ.Location = new Point(0x133, 0x35);
            this.btnCFZKZ.Name = "btnCFZKZ";
            this.btnCFZKZ.Size = new Size(0x4d, 0x17);
            this.btnCFZKZ.TabIndex = 5;
            this.btnCFZKZ.Text = "拆分折扣组";
            this.btnCFZKZ.UseVisualStyleBackColor = true;
            this.btnCancelChaiFen.Location = new Point(230, 0x35);
            this.btnCancelChaiFen.Name = "btnCancelChaiFen";
            this.btnCancelChaiFen.Size = new Size(0x47, 0x17);
            this.btnCancelChaiFen.TabIndex = 4;
            this.btnCancelChaiFen.Text = "取消拆分";
            this.btnCancelChaiFen.UseVisualStyleBackColor = true;
            this.rdBtnXH.AutoSize = true;
            this.rdBtnXH.Location = new Point(270, 20);
            this.rdBtnXH.Name = "rdBtnXH";
            this.rdBtnXH.Size = new Size(0x5f, 0x10);
            this.rdBtnXH.TabIndex = 2;
            this.rdBtnXH.Text = "小行包含折扣";
            this.rdBtnXH.UseVisualStyleBackColor = true;
            this.btnSGCF.Location = new Point(0x99, 0x35);
            this.btnSGCF.Name = "btnSGCF";
            this.btnSGCF.Size = new Size(0x47, 0x17);
            this.btnSGCF.TabIndex = 3;
            this.btnSGCF.Text = "手工拆分";
            this.btnSGCF.UseVisualStyleBackColor = true;
            this.rdBtnDH.AutoSize = true;
            this.rdBtnDH.Location = new Point(0x84, 20);
            this.rdBtnDH.Name = "rdBtnDH";
            this.rdBtnDH.Size = new Size(0x5f, 0x10);
            this.rdBtnDH.TabIndex = 1;
            this.rdBtnDH.Text = "大行包含折扣";
            this.rdBtnDH.UseVisualStyleBackColor = true;
            this.btnCancelFZ.Location = new Point(0x52, 0x35);
            this.btnCancelFZ.Name = "btnCancelFZ";
            this.btnCancelFZ.Size = new Size(0x41, 0x17);
            this.btnCancelFZ.TabIndex = 2;
            this.btnCancelFZ.Text = "取消分组";
            this.btnCancelFZ.UseVisualStyleBackColor = true;
            this.rdBtnMH.AutoSize = true;
            this.rdBtnMH.Checked = true;
            this.rdBtnMH.Location = new Point(13, 20);
            this.rdBtnMH.Name = "rdBtnMH";
            this.rdBtnMH.Size = new Size(0x5f, 0x10);
            this.rdBtnMH.TabIndex = 0;
            this.rdBtnMH.TabStop = true;
            this.rdBtnMH.Text = "每行包含折扣";
            this.rdBtnMH.UseVisualStyleBackColor = true;
            this.buttonGroup.Location = new Point(8, 0x35);
            this.buttonGroup.Name = "buttonGroup";
            this.buttonGroup.Size = new Size(0x44, 0x17);
            this.buttonGroup.TabIndex = 1;
            this.buttonGroup.Text = "设为一组";
            this.buttonGroup.UseVisualStyleBackColor = true;
            this.dgvSplit.set_AborCellPainting(false);
            this.dgvSplit.AllowUserToAddRows = false;
            this.dgvSplit.AllowUserToOrderColumns = true;
            style4.BackColor = Color.FromArgb(210, 0xff, 0xff);
            this.dgvSplit.AlternatingRowsDefaultCellStyle = style4;
            this.dgvSplit.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.dgvSplit.BackgroundColor = Color.WhiteSmoke;
            style5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style5.BackColor = SystemColors.Control;
            this.dgvSplit.ColumnHeadersDefaultCellStyle = style5;
            this.dgvSplit.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
            this.dgvSplit.GridColor = Color.Gray;
            this.dgvSplit.set_GridStyle(0);
            this.dgvSplit.set_KeyEnterConvertToTab(false);
            this.dgvSplit.Location = new Point(3, 15);
            this.dgvSplit.Name = "dgvSplit";
            this.dgvSplit.set_NewColumns(new List<string>());
            this.dgvSplit.ReadOnly = true;
            style6.BackColor = Color.White;
            style6.SelectionBackColor = Color.Teal;
            this.dgvSplit.RowsDefaultCellStyle = style6;
            this.dgvSplit.RowTemplate.Height = 0x17;
            this.dgvSplit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSplit.Size = new Size(0x197, 0x15d);
            this.dgvSplit.TabIndex = 2;
            this.groupBox2.Controls.Add(this.splitContainer3);
            this.groupBox2.Dock = DockStyle.Fill;
            this.groupBox2.Location = new Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x176, 0x263);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "拆分后";
            this.splitContainer3.Dock = DockStyle.Fill;
            this.splitContainer3.Location = new Point(3, 0x11);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = Orientation.Horizontal;
            this.splitContainer3.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer3.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer3.Size = new Size(0x170, 0x24f);
            this.splitContainer3.SplitterDistance = 0xe3;
            this.splitContainer3.TabIndex = 0;
            this.groupBox4.Controls.Add(this.dgvAferBillHeader);
            this.groupBox4.Dock = DockStyle.Fill;
            this.groupBox4.Location = new Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x170, 0xe3);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "销售单据";
            this.dgvAferBillHeader.set_AborCellPainting(false);
            this.dgvAferBillHeader.AllowUserToAddRows = false;
            this.dgvAferBillHeader.AllowUserToOrderColumns = true;
            style7.BackColor = Color.FromArgb(210, 0xff, 0xff);
            this.dgvAferBillHeader.AlternatingRowsDefaultCellStyle = style7;
            this.dgvAferBillHeader.BackgroundColor = Color.WhiteSmoke;
            style8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style8.BackColor = SystemColors.Control;
            this.dgvAferBillHeader.ColumnHeadersDefaultCellStyle = style8;
            this.dgvAferBillHeader.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
            this.dgvAferBillHeader.Dock = DockStyle.Fill;
            this.dgvAferBillHeader.GridColor = Color.Gray;
            this.dgvAferBillHeader.set_GridStyle(0);
            this.dgvAferBillHeader.set_KeyEnterConvertToTab(false);
            this.dgvAferBillHeader.Location = new Point(3, 0x11);
            this.dgvAferBillHeader.Name = "dgvAferBillHeader";
            this.dgvAferBillHeader.set_NewColumns(new List<string>());
            this.dgvAferBillHeader.ReadOnly = true;
            style9.BackColor = Color.White;
            style9.SelectionBackColor = Color.Teal;
            this.dgvAferBillHeader.RowsDefaultCellStyle = style9;
            this.dgvAferBillHeader.RowTemplate.Height = 0x17;
            this.dgvAferBillHeader.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAferBillHeader.Size = new Size(0x16a, 0xcf);
            this.dgvAferBillHeader.TabIndex = 0;
            this.groupBox6.Controls.Add(this.dgvAfterGoods);
            this.groupBox6.Dock = DockStyle.Fill;
            this.groupBox6.Location = new Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x170, 360);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "销售单据明细";
            this.dgvAfterGoods.set_AborCellPainting(false);
            this.dgvAfterGoods.AllowUserToAddRows = false;
            this.dgvAfterGoods.AllowUserToOrderColumns = true;
            style10.BackColor = Color.FromArgb(210, 0xff, 0xff);
            this.dgvAfterGoods.AlternatingRowsDefaultCellStyle = style10;
            this.dgvAfterGoods.BackgroundColor = Color.WhiteSmoke;
            style11.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style11.BackColor = SystemColors.Control;
            this.dgvAfterGoods.ColumnHeadersDefaultCellStyle = style11;
            this.dgvAfterGoods.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
            this.dgvAfterGoods.Dock = DockStyle.Fill;
            this.dgvAfterGoods.GridColor = Color.Gray;
            this.dgvAfterGoods.set_GridStyle(0);
            this.dgvAfterGoods.set_KeyEnterConvertToTab(false);
            this.dgvAfterGoods.Location = new Point(3, 0x11);
            this.dgvAfterGoods.Name = "dgvAfterGoods";
            this.dgvAfterGoods.set_NewColumns(new List<string>());
            this.dgvAfterGoods.ReadOnly = true;
            style12.BackColor = Color.White;
            style12.SelectionBackColor = Color.Teal;
            this.dgvAfterGoods.RowsDefaultCellStyle = style12;
            this.dgvAfterGoods.RowTemplate.Height = 0x17;
            this.dgvAfterGoods.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAfterGoods.Size = new Size(0x16a, 340);
            this.dgvAfterGoods.TabIndex = 1;
            this.toolStrip1.ImageScalingSize = new Size(0x19, 0x19);
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripButton2, this.toolStripBtnDoCF });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(800, 0x19);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripButton2.Image = Resources.退出;
            this.toolStripButton2.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new Size(0x31, 0x16);
            this.toolStripButton2.Text = "退出";
            this.toolStripBtnDoCF.Image = Resources.客户;
            this.toolStripBtnDoCF.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripBtnDoCF.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnDoCF.Name = "toolStripBtnDoCF";
            this.toolStripBtnDoCF.Size = new Size(0x49, 0x16);
            this.toolStripBtnDoCF.Text = "确认拆分";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(800, 0x27c);
            base.Controls.Add(this.panel1);
            base.Name = "MannualSplit";
            this.Text = "销售单据拆分预览";
            base.Load += new EventHandler(this.MannualSplit_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.dgvBillHader.EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.dgvSplit.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.dgvAferBillHeader.EndInit();
            this.groupBox6.ResumeLayout(false);
            this.dgvAfterGoods.EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void MannualSplit_Load(object sender, EventArgs e)
        {
            try
            {
                this.toolStripBtnDoCF.Enabled = false;
                this.ValidatColumns.Add("SL");
                this.ValidatColumns.Add("JE");
                this.ValidatColumns.Add("SE");
                this.dgvBillHader.Columns.Clear();
                this.dgvBillHader.ColumnCount = 5;
                this.dgvBillHader.Columns[0].Name = "单据号";
                this.dgvBillHader.Columns[1].Name = "单据种类";
                this.dgvBillHader.Columns[2].Name = "购方名称";
                this.dgvBillHader.Columns[3].Name = "购方税号";
                this.dgvBillHader.Columns[3].Width = 150;
                this.dgvBillHader.Columns[4].Name = "金额合计";
                this.dgvBillHader.ClearSelection();
                this.dgvBillHader.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.dgvBillHader.Columns[4].DefaultCellStyle.Format = "0.00";
                this.dgvSplit.Columns.Clear();
                this.dgvSplit.get_NewColumns().Add("序号;order;;40");
                this.dgvSplit.get_NewColumns().Add("分组;group;;40");
                this.dgvSplit.get_NewColumns().Add("商品名称;SPMC;;130");
                this.dgvSplit.get_NewColumns().Add("规格型号;GGXH");
                this.dgvSplit.get_NewColumns().Add("计量单位;JLDW;;60");
                this.dgvSplit.get_NewColumns().Add("数量;SL");
                this.dgvSplit.get_NewColumns().Add("单价;DJ;;100");
                this.dgvSplit.get_NewColumns().Add("金额;JE;;100;money");
                this.dgvSplit.get_NewColumns().Add("税率;SLV;;40");
                this.dgvSplit.get_NewColumns().Add("税额;SE;;100;money");
                this.dgvSplit.get_NewColumns().Add("含税价标志;HSJBZ");
                this.dgvSplit.get_NewColumns().Add("原序号;XH;Hide");
                this.dgvSplit.get_NewColumns().Add("单据行性质;DJHXZ;Hide");
                this.dgvSplit.get_NewColumns().Add("商品税目;SPSM;Hide");
                this.dgvSplit.Bind();
                this.dgvSplit.ReadOnly = false;
                this.dgvSplit.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSplit.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSplit.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSplit.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSplit.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSplit.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSplit.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSplit.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSplit.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSplit.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSplit.Columns[10].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSplit.Columns[0].ReadOnly = true;
                this.dgvSplit.Columns[1].ReadOnly = true;
                this.dgvSplit.Columns[2].ReadOnly = true;
                this.dgvSplit.Columns[3].ReadOnly = true;
                this.dgvSplit.Columns[4].ReadOnly = true;
                this.dgvSplit.Columns[6].ReadOnly = true;
                this.dgvSplit.Columns[8].ReadOnly = true;
                this.dgvSplit.Columns[10].ReadOnly = true;
                this.ReadOnlyColumnIndex.AddRange(new int[] { 0, 1, 2, 3, 4, 6, 8, 10 });
                this.dgvSplit.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                this.dgvSplit.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.dgvSplit.Columns["SLV"].DefaultCellStyle.Format = "0.0%";
                this.dgvSplit.Columns["JE"].HeaderCell.Style.BackColor = Color.AliceBlue;
                this.dgvSplit.Columns["SE"].HeaderCell.Style.BackColor = Color.AliceBlue;
                this.dgvSplit.Columns["SL"].HeaderCell.Style.BackColor = Color.AliceBlue;
                this.dgvSplit.Columns["SL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvSplit.Columns["DJ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvSplit.Columns["SLV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvSplit.ClearSelection();
                this.ToViewSplit(this.bill);
                this.dgvAferBillHeader.Columns.Clear();
                this.dgvAferBillHeader.get_NewColumns().Add("单据号;BH");
                this.dgvAferBillHeader.get_NewColumns().Add("单据种类;DJZL");
                this.dgvAferBillHeader.get_NewColumns().Add("购方名称;GFMC");
                this.dgvAferBillHeader.get_NewColumns().Add("购方税号;GFSH;;150");
                this.dgvAferBillHeader.get_NewColumns().Add("金额合计;JEHJ;;100;money");
                this.dgvAferBillHeader.Bind();
                this.dgvAferBillHeader.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.dgvAfterGoods.ColumnsClear();
                this.dgvAfterGoods.get_NewColumns().Add("序号;order");
                this.dgvAfterGoods.get_NewColumns().Add("编号;group");
                this.dgvAfterGoods.get_NewColumns().Add("商品名称;SPMC;;130");
                this.dgvAfterGoods.get_NewColumns().Add("数量;SL;;60");
                this.dgvAfterGoods.get_NewColumns().Add("单价;DJ;;60");
                this.dgvAfterGoods.get_NewColumns().Add("金额;JE;;100;money");
                this.dgvAfterGoods.get_NewColumns().Add("税率;SLV;;60");
                this.dgvAfterGoods.get_NewColumns().Add("税额;SE;;100;money");
                this.dgvAfterGoods.get_NewColumns().Add("单据行性质;DJHXZ;ZKHStyle");
                this.dgvAfterGoods.Bind();
                this.dgvAfterGoods.Columns["SLV"].DefaultCellStyle.Format = "0.0%";
                this.dgvAfterGoods.Columns["SLV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvAfterGoods.Columns["SL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvAfterGoods.Columns["DJ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvAfterGoods.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.btnCancelChaiFen.Enabled = false;
                this.btnSGCF.Enabled = false;
                this.btnCancelFZ.Enabled = false;
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void toolBtnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolBtnDoCF_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageManager.ShowMsgBox("INP-272291") == DialogResult.OK)
                {
                    SaleBillDAL ldal = new SaleBillDAL();
                    string str = ldal.SaveToTempTable(this.splitedBills, 1);
                    if (str != "0")
                    {
                        MessageManager.ShowMsgBox(str);
                    }
                    else
                    {
                        List<SaleBill> beforebill = new List<SaleBill> {
                            this.bill
                        };
                        string str2 = ldal.SaveToRealTable(beforebill, this.splitedBills, this.splitedBills[0].BH);
                        if (str2 == "0")
                        {
                            MessageManager.ShowMsgBox("INP-272292");
                            base.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageManager.ShowMsgBox("拆分失败:" + str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void ToViewSplit(SaleBill bill)
        {
            try
            {
                this.dgvBillHader.Rows.Clear();
                while (this.dgvSplit.Rows.Count > 0)
                {
                    this.dgvSplit.Rows.RemoveAt(0);
                }
                string str = ShowString.ShowFPZL(bill.DJZL);
                this.dgvBillHader.Rows.Add(new object[] { bill.BH, str, bill.GFMC, bill.GFSH, bill.JEHJ });
                for (int i = 0; i < bill.ListGoods.Count; i++)
                {
                    Goods goods = bill.ListGoods[i];
                    string str2 = ShowString.ShowBool(goods.HSJBZ);
                    string str3 = string.IsNullOrEmpty(goods.Reserve) ? "0" : goods.Reserve;
                    if (goods.SLV == 0.0)
                    {
                        string str4 = this.billBL.ShowSLV(bill, (i + 1).ToString(), "0");
                        if (str4.Equals("免税") || str4.Equals("不征税"))
                        {
                            this.dgvSplit.Rows.Add(new object[] { i + 1, str3, goods.SPMC, goods.GGXH, goods.JLDW, (decimal) goods.SL, (decimal) goods.DJ, goods.JE, str4, goods.SE, str2, goods.XH, goods.DJHXZ, goods.SPSM });
                        }
                        else
                        {
                            this.dgvSplit.Rows.Add(new object[] { i + 1, str3, goods.SPMC, goods.GGXH, goods.JLDW, (decimal) goods.SL, (decimal) goods.DJ, goods.JE, goods.SLV, goods.SE, str2, goods.XH, goods.DJHXZ, goods.SPSM });
                        }
                    }
                    else if (bill.HYSY && (goods.SLV == 0.05))
                    {
                        this.dgvSplit.Rows.Add(new object[] { i + 1, str3, goods.SPMC, goods.GGXH, goods.JLDW, (decimal) goods.SL, (decimal) goods.DJ, goods.JE, "中外合作油气田", goods.SE, str2, goods.XH, goods.DJHXZ, goods.SPSM });
                    }
                    else if (bill.JZ_50_15 && (goods.SLV == 0.015))
                    {
                        this.dgvSplit.Rows.Add(new object[] { i + 1, str3, goods.SPMC, goods.GGXH, goods.JLDW, (decimal) goods.SL, (decimal) goods.DJ, goods.JE, "减按1.5%计算", goods.SE, str2, goods.XH, goods.DJHXZ, goods.SPSM });
                    }
                    else
                    {
                        this.dgvSplit.Rows.Add(new object[] { i + 1, str3, goods.SPMC, goods.GGXH, goods.JLDW, (decimal) goods.SL, (decimal) goods.DJ, goods.JE, goods.SLV, goods.SE, str2, goods.XH, goods.DJHXZ, goods.SPSM });
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void ToViewSplited(List<SaleBill> bills)
        {
            Comparison<SaleBill> comparison = new Comparison<SaleBill>(this.Compare);
            bills.Sort(comparison);
            this.dgvAferBillHeader.Rows.Clear();
            for (int i = 0; i < bills.Count; i++)
            {
                SaleBill bill = bills[i];
                string str = ShowString.ShowFPZL(bill.DJZL);
                this.dgvAferBillHeader.Rows.Add(new object[] { bill.BH, str, bill.GFMC, bill.GFSH, bill.JEHJ });
            }
        }

        private void ToViewSplitedGoodlist(SaleBill splitbill)
        {
            this.dgvAfterGoods.Rows.Clear();
            for (int i = 0; i < splitbill.ListGoods.Count; i++)
            {
                Goods goods = splitbill.ListGoods[i];
                string str = ShowString.ShowBool(goods.HSJBZ);
                if (goods.SLV == 0.0)
                {
                    string str2 = this.billBL.ShowSLV(splitbill, (i + 1).ToString(), "0");
                    if (str2.Equals("免税") || str2.Equals("不征税"))
                    {
                        this.dgvAfterGoods.Rows.Add(new object[] { i + 1, goods.XSDJBH, goods.SPMC, (decimal) goods.SL, (decimal) goods.DJ, goods.JE, str2, goods.SE });
                    }
                    else
                    {
                        this.dgvAfterGoods.Rows.Add(new object[] { i + 1, goods.XSDJBH, goods.SPMC, (decimal) goods.SL, (decimal) goods.DJ, goods.JE, goods.SLV, goods.SE });
                    }
                }
                else if (this.bill.HYSY && (goods.SLV == 0.05))
                {
                    this.dgvAfterGoods.Rows.Add(new object[] { i + 1, goods.XSDJBH, goods.SPMC, (decimal) goods.SL, (decimal) goods.DJ, goods.JE, "中外合作油气田", goods.SE });
                }
                else if (this.bill.JZ_50_15 && (goods.SLV == 0.015))
                {
                    this.dgvAfterGoods.Rows.Add(new object[] { i + 1, goods.XSDJBH, goods.SPMC, (decimal) goods.SL, (decimal) goods.DJ, goods.JE, "减按1.5%计算", goods.SE });
                }
                else
                {
                    this.dgvAfterGoods.Rows.Add(new object[] { i + 1, goods.XSDJBH, goods.SPMC, (decimal) goods.SL, (decimal) goods.DJ, goods.JE, goods.SLV, goods.SE });
                }
                switch (goods.DJHXZ)
                {
                    case 0:
                        this.dgvAfterGoods.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                        break;

                    case 3:
                        this.dgvAfterGoods.Rows[i].DefaultCellStyle.BackColor = Color.LightCyan;
                        break;

                    case 4:
                        this.dgvAfterGoods.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                        break;
                }
            }
        }
    }
}

