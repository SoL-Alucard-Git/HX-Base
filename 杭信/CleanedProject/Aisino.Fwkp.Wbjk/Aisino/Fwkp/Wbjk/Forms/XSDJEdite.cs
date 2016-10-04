namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.CommonLibrary;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using Aisino.Fwkp.Wbjk.Properties;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class XSDJEdite : BaseForm
    {
        private int AddCount;
        private AisinoLBL aisinoLBL_RowInfo;
        private string BH;
        private SaleBill bill;
        private Button button1;
        private AisinoCHK checkBox_GF;
        private AisinoCHK checkBox_HYSY;
        private AisinoCHK checkBox_SFZJY;
        private AisinoCHK checkBox_XF;
        public AisinoMultiCombox com_fhr;
        public AisinoMultiCombox com_gfdzdh;
        public AisinoMultiCombox com_gfmc;
        public AisinoMultiCombox com_gfsbh;
        public AisinoMultiCombox com_gfzh;
        public AisinoMultiCombox com_skr;
        public AisinoMultiCombox com_xfdzdh;
        public AisinoMultiCombox com_xfmc;
        public AisinoMultiCombox com_xfsbh;
        public AisinoMultiCombox com_xfyhzh;
        private AisinoCMB comboBox_SLV;
        private AisinoCMB comboBoxDJZL;
        private IContainer components;
        private bool ContainTax;
        public PointF DataLoca;
        private DateTimePicker dateTimePicker1;
        private Color defColor;
        public DataGridView dgv_spmx;
        private DateTime dtEndEdit;
        private bool flagAdd;
        private float H;
        private float HA;
        private float HB;
        private float Hpad;
        private int insII;
        private Invoice InvoiceKP;
        private bool isHYSY;
        private PZSQType isi_Common;
        private PZSQType isi_Special;
        private bool IsUpdate;
        public AisinoLBL lab_Amount;
        private AisinoLBL lab_bz;
        private AisinoLBL lab_DJZT;
        private AisinoLBL lab_hj_jshj_dx;
        public AisinoLBL lab_hj_xx;
        private AisinoLBL lab_KPZT;
        public AisinoLBL lab_Tax;
        public AisinoLBL lab_title;
        private AisinoLBL label10;
        private AisinoLBL label11;
        private AisinoLBL label12;
        private AisinoLBL label13;
        private AisinoLBL label17;
        private AisinoLBL label18;
        private AisinoLBL label19;
        private AisinoLBL label2;
        private AisinoLBL label20;
        private AisinoLBL label21;
        private AisinoLBL label23;
        private AisinoLBL label26;
        private AisinoLBL label27;
        private AisinoLBL label29;
        private AisinoLBL label31;
        private AisinoLBL label32;
        private AisinoLBL label7;
        private AisinoLBL label8;
        private AisinoLBL label9;
        private AisinoLBL labelDJH;
        private AisinoLBL labelDJRQ;
        private AisinoLBL labelDJZL;
        private AisinoLBL labelQD;
        private ILog log;
        private bool NumError;
        private int oipoidd;
        private SaleBillCtrl saleBillBL;
        private double selectSlv;
        private bool SetSpxxSet;
        internal AisinoMultiCombox spmcBt;
        private AisinoTXT textBoxDJH;
        private AisinoTXT textBoxQDSPHMX;
        private ToolStrip toolbtnBuQi;
        private ToolStripButton toolBtnCheck;
        private ToolStripButton toolStripBtnCancel;
        private ToolStripButton toolStripBtnHSJG;
        private ToolStripButton toolStripBtnSave;
        private ToolStripButton toolStripButtonAdd;
        private ToolStripButton toolStripButtonCE;
        private ToolStripButton toolStripButtonDel;
        private ToolStripButton toolStripButtonKH;
        private ToolStripButton toolStripButtonZK;
        public AisinoTXT txt_bz;
        private int valCol;
        private int valRow;
        private float W;
        private float WA;
        private float WB;

        public XSDJEdite()
        {
            this.components = null;
            this.BH = "";
            this.IsUpdate = false;
            this.saleBillBL = SaleBillCtrl.Instance;
            this.bill = null;
            this.log = LogUtil.GetLogger<DJXG>();
            this.flagAdd = false;
            this.ContainTax = false;
            this.InvoiceKP = null;
            this.SetSpxxSet = false;
            this.oipoidd = 1;
            this.selectSlv = 0.0;
            this.dtEndEdit = new DateTime(0x76c, 1, 1, 0, 0, 0, 0);
            this.defColor = SystemColors.Control;
            this.NumError = false;
            this.valCol = -1;
            this.valRow = -1;
            this.insII = 0;
            this.AddCount = 0;
            this.W = 0f;
            this.H = 0f;
            this.Hpad = 120f;
            this.WA = 60f;
            this.WB = 60f;
            this.HA = 110f;
            this.HB = 70f;
            try
            {
                this.InitializeComponent();
                this.IsUpdate = false;
                this.Text = "销售单据添加";
                this.bill = new SaleBill();
                this.IniteControl();
                this.IniteGridMX();
                if (this.saleBillBL.XGDJZL != "a")
                {
                    this.bill.DJZL = this.saleBillBL.XGDJZL;
                }
                this.bill.DJRQ = TaxCardValue.taxCard.GetCardClock();
                this.AddGridGoodsRow(0);
                this.initInvoice();
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        public XSDJEdite(string BH)
        {
            this.components = null;
            this.BH = "";
            this.IsUpdate = false;
            this.saleBillBL = SaleBillCtrl.Instance;
            this.bill = null;
            this.log = LogUtil.GetLogger<DJXG>();
            this.flagAdd = false;
            this.ContainTax = false;
            this.InvoiceKP = null;
            this.SetSpxxSet = false;
            this.oipoidd = 1;
            this.selectSlv = 0.0;
            this.dtEndEdit = new DateTime(0x76c, 1, 1, 0, 0, 0, 0);
            this.defColor = SystemColors.Control;
            this.NumError = false;
            this.valCol = -1;
            this.valRow = -1;
            this.insII = 0;
            this.AddCount = 0;
            this.W = 0f;
            this.H = 0f;
            this.Hpad = 120f;
            this.WA = 60f;
            this.WB = 60f;
            this.HA = 110f;
            this.HB = 70f;
            try
            {
                this.InitializeComponent();
                this.IsUpdate = true;
                this.BH = BH;
                this.Text = "销售单据修改";
                this.bill = this.saleBillBL.Find(BH);
                this.textBoxDJH.Cursor = Cursors.No;
                this.textBoxDJH.ForeColor = SystemColors.GrayText;
                this.textBoxDJH.ReadOnly = true;
                this.IniteControl();
                this.IniteGridMX();
                this.saleBillBL.CleanBillWithoutFLBM(this.bill);
                this.ToView();
                if (this.bill.DJZL == "c")
                {
                    if (this.checkBox_GF.Checked)
                    {
                        TaxCard card = TaxCardFactory.CreateTaxCard();
                        this.com_gfmc.Text = card.get_Corporation();
                        this.com_gfsbh.Text = card.get_TaxCode();
                        if (this.bill.KPZT == "N")
                        {
                            this.com_gfmc.set_Edit(0);
                            this.com_gfsbh.set_Edit(0);
                            this.com_xfmc.set_Edit(1);
                            this.com_xfsbh.set_Edit(1);
                        }
                    }
                }
                else
                {
                    this.checkBox_XF.Visible = false;
                    this.checkBox_GF.Visible = false;
                }
                this.initInvoice();
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private DataTable _GfxxOnAutoCompleteDataSource(string str)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKHMore", new object[] { str, 20, "MC,SH,DZDH,YHZH" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
        }

        private void _GfxxSelect(string value, int type)
        {
            object[] khxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKH", new object[] { value, type, "MC,SH,DZDH,YHZH" });
            if (khxx != null)
            {
                this.SetGfxx(khxx);
            }
        }

        protected virtual void _SetGfxxControl(AisinoMultiCombox amc, string showText)
        {
            amc.set_IsSelectAll(true);
            amc.set_buttonStyle(0);
            amc.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            amc.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", amc.Width - 140));
            amc.set_ShowText(showText);
            amc.set_DrawHead(false);
            amc.set_AutoIndex(0);
            amc.add_OnButtonClick(new EventHandler(this.gfxx_OnButtonClick));
            amc.set_AutoComplate(2);
            amc.add_OnAutoComplate(new EventHandler(this.gfxx_OnAutoComplate));
            amc.add_OnSelectValue(new EventHandler(this.gfxx_OnSelectValue));
        }

        protected void _SetXfxxControl(AisinoMultiCombox amc, string showText)
        {
            amc.set_IsSelectAll(true);
            amc.set_buttonStyle(0);
            amc.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 120));
            amc.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", amc.Width - 120));
            amc.set_ShowText(showText);
            amc.set_DrawHead(false);
            amc.set_AutoIndex(0);
            amc.add_OnButtonClick(new EventHandler(this.xfxx_OnButtonClick));
            amc.set_AutoComplate(2);
            amc.add_OnAutoComplate(new EventHandler(this.xfxx_OnAutoComplate));
            amc.add_OnSelectValue(new EventHandler(this.xfxx_OnSelectValue));
        }

        protected DataTable _XfxxOnAutoCompleteDataSource(string str)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetXHDWMore", new object[] { str, 20, "MC,SH,DZDH,YHZH" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
        }

        protected void _XfxxSelect(string value, int type)
        {
            object[] xfxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetXHDW", new object[] { value, type, "MC,SH,DZDH,YHZH" });
            if (xfxx != null)
            {
                this.XfxxSetValue(xfxx);
            }
        }

        protected void _XfxxSetValue(Dictionary<string, string> khxx)
        {
            this.XfxxSetValue(new object[] { khxx["MC"], khxx["SH"], khxx["DZDH"], khxx["YHZH"] });
        }

        private void AddGridGoodsRow(int InsertUp = 0)
        {
            if (((this.saleBillBL.CanEditBill(this.bill) != 1) && this.dataGrid()) && !this.NumError)
            {
                int index = -1;
                if (this.dgv_spmx.Rows.Count > 0)
                {
                    index = this.dgv_spmx.CurrentCell.RowIndex;
                }
                Goods mx = new Goods();
                if (this.bill.HYSY && (this.bill.DJZL == "s"))
                {
                    mx.SLV = 0.05;
                }
                mx.SLV = 0.0;
                PZSQType type = null;
                if (this.bill.DJZL == "s")
                {
                    if (this.bill.HYSY)
                    {
                        mx.SLV = 0.05;
                    }
                    else
                    {
                        type = this.isi_Special;
                    }
                }
                else if (this.bill.DJZL == "c")
                {
                    type = this.isi_Common;
                }
                if ((this.bill.ListGoods.Count > 0) && (this.bill.ListGoods[0].SLV == 0.015))
                {
                    mx.SLV = 0.015;
                    type = null;
                }
                if (type != null)
                {
                    double num2;
                    List<TaxRateType> taxRate = type.TaxRate;
                    if ((0 < taxRate.Count) && double.TryParse(taxRate[0].Rate.ToString("F03"), out num2))
                    {
                        mx.SLV = num2;
                    }
                }
                if ((this.dgv_spmx.Rows.Count - 1) == index)
                {
                    InsertUp = 0;
                }
                if ((this.bill.ListGoods.Count >= 1) && !(this.bill.ListGoods[0].KCE == 0.0))
                {
                    MessageBoxHelper.Show("差额征收单据，只能有一个商品行");
                }
                else
                {
                    string str2 = this.saleBillBL.InsertGoods(this.bill, mx, index, InsertUp);
                    if (str2 == "0")
                    {
                        this.ToView();
                        if (index == -1)
                        {
                            index = 0;
                        }
                        this.dgv_spmx.Rows[index].Selected = true;
                        this.dgv_spmx.CurrentCell = this.dgv_spmx.Rows[index].Cells[0];
                        this.comboBox_SLV.Visible = false;
                    }
                    else if (str2 != "2")
                    {
                        MessageManager.ShowMsgBox(str2);
                    }
                }
            }
        }

        private void AddGridSpMXFirstMX()
        {
            try
            {
                this.dgv_spmx.CurrentCellChanged -= new EventHandler(this.dataGridMX1_CurrentCellChanged);
                this.dgv_spmx.Rows.Add();
                this.dgv_spmx.Rows[0].Tag = new XSDJ_MXModel();
                this.dgv_spmx.Rows[0].Cells["DJHXZ"].Value = 0;
                this.dgv_spmx.Rows[0].Cells["JE"].Value = 0;
                this.dgv_spmx.Rows[0].Cells["SE"].Value = 0;
                this.dgv_spmx.Rows[0].Cells["SPSM"].Value = 0;
                this.dgv_spmx.Rows[0].Selected = true;
                this.dgv_spmx.CurrentCellChanged += new EventHandler(this.dataGridMX1_CurrentCellChanged);
                this.dataGridMX1_CurrentCellChanged(this.dgv_spmx, new EventArgs());
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValitBtnCancel())
                {
                    this.comboBox_SLV.Visible = false;
                }
                this.dgv_spmx.EndEdit();
                this.AddGridGoodsRow(1);
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void btnAddKH_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGrid())
                {
                    object[] objArray = new object[] { this.com_gfmc.Text, this.com_gfsbh.Text, this.com_gfdzdh.Text, this.com_gfzh.Text };
                    ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddKH", objArray);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
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

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    this.textBoxLeave(sender, e);
                    string str = this.saleBillBL.CheckBill(this.bill);
                    this.ToModel(this.bill);
                    this.ToView();
                    MessageBoxHelper.Show(str, "检查结果", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                this.comboBox_SLV.Visible = false;
                this.dgv_spmx.EndEdit();
                this.oipoidd = 0;
                this.spmcBt.Visible = false;
                this.DelGridGoodsRow();
                this.NumError = false;
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MessageHelper.MsgWait("正在保存...");
                this.dgv_spmx.EndEdit();
                string str = "";
                if (this.dataGrid())
                {
                    int num2;
                    this.ToModel(this.bill);
                    int num = 0;
                    for (num2 = 0; num2 < this.bill.ListGoods.Count; num2++)
                    {
                        Goods good = this.bill.GetGood(num2);
                        if (!(string.IsNullOrEmpty(good.FPDM) || (good.FPHM == 0)))
                        {
                            num++;
                        }
                    }
                    if ((num > 0) && (num == this.bill.ListGoods.Count))
                    {
                        this.bill.KPZT = "A";
                    }
                    else if ((num > 0) && (num < this.bill.ListGoods.Count))
                    {
                        this.bill.KPZT = "P";
                    }
                    this.saleBillBL.CleanBill(this.bill);
                    if (this.bill.GFSH.Length == 0x11)
                    {
                        string str2 = this.bill.GFSH.Substring(15, 2);
                        string str3 = this.bill.GFSH.Substring(0, 15);
                        if (str2.CompareTo("XX") == 0)
                        {
                            this.bill.SFZJY = true;
                        }
                    }
                    if (CommonTool.isSPBMVersion())
                    {
                        for (num2 = 0; num2 < this.bill.ListGoods.Count; num2++)
                        {
                            if (!this.bill.ListGoods[num2].XSYH)
                            {
                                this.bill.ListGoods[num2].XSYHSM = "";
                            }
                        }
                    }
                    str = this.saleBillBL.Save(this.bill);
                    if (str == "0")
                    {
                        string str4 = this.saleBillBL.CheckBill(this.bill);
                        base.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageManager.ShowMsgBox(str);
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
            finally
            {
                MessageHelper.MsgWait();
            }
        }

        private void btnZK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGrid() && (this.CheckGridFail() <= 0))
                {
                    int rowIndex = this.dgv_spmx.SelectedCells[0].RowIndex;
                    string str = this.saleBillBL.DisCountValidate(this.bill, rowIndex);
                    if (str != "0")
                    {
                        MessageBoxHelper.Show(str, "添加折扣提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        AddZKH dzkh = new AddZKH(this.bill, rowIndex);
                        dzkh.ShowDialog();
                        if (dzkh.DiscountAdded)
                        {
                            this.saleBillBL.CleanBill(this.bill);
                            this.ToView();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FPLX fplx = 0;
            bool flag = false;
            double num = 0.13;
            double num2 = 13.0;
            Fpxx fpxx = new Fpxx(fplx, "", "");
            Invoice invoice = new Invoice(flag, fpxx, new byte[0x20], null);
            Spxx spxx = new Spxx("", "", num.ToString(), "", "吨", "", flag, 0);
            int num3 = invoice.AddSpxx(spxx);
            string code = invoice.GetCode();
            bool flag2 = invoice.SetSe(num3, "13");
            string str2 = invoice.GetCode();
            bool flag3 = invoice.SetSLv(num3, "0.13");
            string str3 = invoice.GetCode();
            Dictionary<SPXX, string> dictionary = invoice.GetSpxx(num3);
            byte[] buffer = null;
            invoice = new Invoice(false, false, false, 2, buffer, null);
            spxx = new Spxx("", "", num.ToString());
            spxx.set_SLv(num.ToString());
            spxx.set_Se(num2.ToString());
            int num4 = invoice.AddSpxx(spxx);
            string str4 = invoice.GetCode();
            Dictionary<SPXX, string> dictionary2 = invoice.GetSpxx(num4);
        }

        private void checkBox_GF_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGrid())
                {
                    this.checkBox_XF.CheckedChanged -= new EventHandler(this.checkBox_XF_CheckedChanged);
                    this.checkBox_XF.Checked = false;
                    this.checkBox_XF.CheckedChanged += new EventHandler(this.checkBox_XF_CheckedChanged);
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    if (this.checkBox_GF.Checked)
                    {
                        this.comboBox_SLV.Items.Clear();
                        this.comboBox_SLV.Items.AddRange(new object[] { "0%" });
                        this.comboBox_SLV.Text = "0%";
                        this.bill.TYDH = "2";
                        this.com_gfmc.Text = card.get_Corporation();
                        this.com_gfsbh.Text = card.get_TaxCode();
                        this.com_xfmc.Text = "";
                        this.com_xfsbh.Text = "";
                        this.com_gfmc.set_Edit(0);
                        this.com_gfsbh.set_Edit(0);
                        this.com_xfmc.set_Edit(1);
                        this.com_xfsbh.set_Edit(1);
                        this.bill.GFDZDH = "";
                        this.bill.GFYHZH = "";
                        this.bill.KHYHMC = "";
                        this.bill.KHYHZH = "";
                        this.bill.XFDZDH = "";
                        this.bill.XFYHZH = "";
                    }
                    else
                    {
                        this.comboBox_SLV.Items.Clear();
                        this.comboBox_SLV.Items.AddRange(new object[] { " 17%", " 13%", " 6%", " 4%", " 3%" });
                        this.comboBox_SLV.Text = "17%";
                        this.bill.TYDH = "";
                        this.com_gfmc.Text = "";
                        this.com_gfsbh.Text = "";
                        this.com_xfmc.Text = card.get_Corporation();
                        this.com_xfsbh.Text = card.get_TaxCode();
                        this.com_gfmc.set_Edit(1);
                        this.com_gfsbh.set_Edit(1);
                        this.com_xfmc.set_Edit(0);
                        this.com_xfsbh.set_Edit(0);
                        this.bill.GFMC = "";
                        this.bill.GFSH = "";
                        this.bill.GFDZDH = "";
                        this.bill.GFYHZH = "";
                        this.bill.XFDZDH = "";
                        this.bill.XFYHZH = "";
                    }
                    this.saleBillBL.CleanBill(this.bill);
                    this.ToView();
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void checkBox_HYSY_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string dJZL = this.bill.DJZL;
                bool hYSY = this.bill.HYSY;
                this.bill.HYSY = this.checkBox_HYSY.Checked;
                this.bill.ContainTax = false;
                this.toolStripBtnHSJG.Checked = false;
                this.saleBillBL.OceanOilHasChanged(dJZL, hYSY, this.bill);
                this.ffSlv(this.bill.DJZL);
                this.saleBillBL.CleanBill(this.bill);
                this.ToView();
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void checkBox_XF_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGrid())
                {
                    if (this.checkBox_GF.Checked)
                    {
                        TaxCard card = TaxCardFactory.CreateTaxCard();
                        this.com_gfmc.Text = "";
                        this.com_gfsbh.Text = "";
                        this.com_xfmc.Text = card.get_Corporation();
                        this.com_xfsbh.Text = card.get_TaxCode();
                        this.com_gfmc.set_Edit(1);
                        this.com_gfsbh.set_Edit(1);
                        this.com_xfmc.set_Edit(0);
                        this.com_xfsbh.set_Edit(0);
                        this.bill.GFMC = "";
                        this.bill.GFSH = "";
                        this.bill.GFDZDH = "";
                        this.bill.GFYHZH = "";
                        this.bill.XFDZDH = "";
                        this.bill.XFYHZH = "";
                    }
                    this.checkBox_GF.CheckedChanged -= new EventHandler(this.checkBox_GF_CheckedChanged);
                    this.checkBox_GF.Checked = false;
                    this.checkBox_GF.CheckedChanged += new EventHandler(this.checkBox_GF_CheckedChanged);
                    this.comboBox_SLV.Items.Clear();
                    this.comboBox_SLV.Items.AddRange(new object[] { " 17%", " 13%", " 6%", " 4%", " 3%" });
                    this.comboBox_SLV.Text = "17%";
                    if (this.checkBox_XF.Checked)
                    {
                        this.bill.TYDH = "1";
                    }
                    else
                    {
                        this.bill.TYDH = "";
                    }
                    this.saleBillBL.CleanBill(this.bill);
                    this.ToView();
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private int CheckGridFail()
        {
            if (this.dgv_spmx.Rows.Count <= 0)
            {
                return 2;
            }
            this.dgv_spmx.CurrentCell = this.dgv_spmx.Rows[this.dgv_spmx.CurrentCell.RowIndex].Cells["FPZL"];
            this.spmcBt.Visible = false;
            this.comboBox_SLV.Visible = false;
            if (this.NumError)
            {
                MessageBoxHelper.Show("数值错误");
                return 1;
            }
            return 0;
        }

        private void com_gfmc_Click(object sender, EventArgs e)
        {
        }

        private void com_gfmc_OnButtonClick(object sender, EventArgs e)
        {
            try
            {
                string text = this.com_gfmc.Text;
                this.SelKhxx(text, 0);
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void com_gfmc_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void com_gfsbh_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_gfsbh.Text.ToUpper();
            this.com_gfsbh.Text = str;
            this.com_gfsbh.set_SelectionStart(this.com_gfsbh.Text.Length);
        }

        private void com_xfmc_OnButtonClick(object sender, EventArgs e)
        {
            try
            {
                string text = this.com_xfmc.Text;
                this.SelKhxx(text, 0);
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void com_xfmc_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string text = this.com_xfmc.Text;
                    this.SelKhxx(text, 1);
                    this.dgv_spmx.Focus();
                    this.dgv_spmx.CurrentCell = this.dgv_spmx.Rows[0].Cells[1];
                    this.dgv_spmx.CurrentCell = this.dgv_spmx.Rows[0].Cells[0];
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void com_xfyhzh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.saleBillBL.IsSWDK())
                {
                    string text = this.com_xfyhzh.Text;
                    int byteCount = ToolUtil.GetByteCount(text);
                    if (byteCount > 100)
                    {
                        while (byteCount > 100)
                        {
                            int length = text.Length;
                            text = text.Substring(0, length - 1);
                            byteCount = ToolUtil.GetByteCount(text);
                        }
                    }
                    this.com_xfyhzh.Text = text;
                    this.com_xfyhzh.set_SelectionStart(this.com_xfyhzh.Text.Length);
                }
                this.ToModel(this.bill);
                this.ToView();
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void comboBox_SLV_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Decimal) && !this.comboBox_SLV.Text.Contains("."))
                {
                    this.comboBox_SLV.Text = this.comboBox_SLV.Text + ".";
                    this.comboBox_SLV.Select(this.comboBox_SLV.Text.Length, 1);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void comboBox_SLV_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox_SLV_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string str = this.comboBox_SLV.Text.Trim();
                if (str.Trim() == "减按1.5%计算")
                {
                    str = "1.5%";
                }
                string s = str.Replace("%", "");
                double result = -1.0;
                bool flag = double.TryParse(str.Replace("%", ""), out result);
                if (flag)
                {
                    result /= 100.0;
                }
                else
                {
                    result = -1.0;
                }
                if (CommonTool.isSPBMVersion())
                {
                    int rowIndex = this.dgv_spmx.CurrentCell.RowIndex;
                    Goods good = this.bill.GetGood(rowIndex);
                    if (str != "中外合作油气田")
                    {
                        string str3 = "出口零税";
                        string str4 = "免税";
                        string str5 = "不征税";
                        good.LSLVBS = "";
                        if (str.Contains(str4))
                        {
                            good.XSYH = true;
                            good.LSLVBS = "1";
                            s = "0.0";
                        }
                        else if (str.Contains(str5))
                        {
                            good.XSYH = true;
                            good.LSLVBS = "2";
                            s = "0.0";
                        }
                        if (flag)
                        {
                            if (result == 0.0)
                            {
                                if (good.XSYHSM.Contains(str3))
                                {
                                    good.XSYH = true;
                                    good.LSLVBS = "0";
                                }
                                else
                                {
                                    good.LSLVBS = "3";
                                }
                                s = "0.0";
                            }
                            else
                            {
                                List<double> list = new List<double>();
                                string[] strArray = good.XSYHSM.Split(new char[] { '、' });
                                foreach (string str6 in strArray)
                                {
                                    List<double> list2 = new SaleBillDAL().GET_YHZCSLV_BY_YHZCMC(str6);
                                    foreach (double num3 in list2)
                                    {
                                        bool flag2 = false;
                                        foreach (double num4 in list)
                                        {
                                            if (num4 == num3)
                                            {
                                                flag2 = true;
                                            }
                                        }
                                        if (!flag2)
                                        {
                                            list.Add(num3);
                                        }
                                    }
                                }
                                if (list.Count > 0)
                                {
                                    bool flag3 = false;
                                    foreach (double num5 in list)
                                    {
                                        if (num5 == result)
                                        {
                                            flag3 = true;
                                            break;
                                        }
                                    }
                                    good.XSYH = flag3;
                                }
                            }
                        }
                    }
                }
                if (s == "中外合作油气田")
                {
                    s = "0.05";
                    this.bill.HYSY = true;
                    this.bill.ContainTax = false;
                    if (!this.checkBox_HYSY.Checked)
                    {
                    }
                }
                else if (s == "免税")
                {
                    s = "0.0";
                }
                else if (s == "0")
                {
                    s = "0.0";
                }
                else
                {
                    s = result.ToString();
                }
                double num6 = 0.0;
                if (double.TryParse(s, out num6))
                {
                    double num7 = num6;
                    if (num6 > 1.0)
                    {
                        num7 = num6 / 100.0;
                    }
                    if (Math.Abs((double) (num7 - 0.015)) < 1E-05)
                    {
                        this.bill.JZ_50_15 = true;
                    }
                    else
                    {
                        this.bill.JZ_50_15 = false;
                    }
                    int columnIndex = this.dgv_spmx.SelectedCells[0].ColumnIndex;
                    if (this.dgv_spmx.Columns[columnIndex].Name == "SLV")
                    {
                        int mxID = this.dgv_spmx.SelectedCells[0].RowIndex;
                        if (this.bill.HYSY && (this.bill.ListGoods[mxID].KCE != 0.0))
                        {
                            MessageBoxHelper.Show("中外合作油气田  取消差额征税");
                        }
                        else if (this.bill.JZ_50_15 && (this.bill.ListGoods[mxID].KCE != 0.0))
                        {
                            MessageBoxHelper.Show("减按1.5%计算  取消差额征税");
                        }
                        this.bill.setSlv(mxID, num6.ToString());
                        this.saleBillBL.CleanBill(this.bill);
                        this.ToView();
                    }
                    else
                    {
                        this.dgv_spmx.Focus();
                    }
                }
                this.dgv_spmx.Focus();
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void comboBox_SLV_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.comboBox_SLV.Visible = false;
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void comboBoxDJZL_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.checkBox_HYSY.CheckedChanged -= new EventHandler(this.checkBox_HYSY_CheckedChanged);
                string dJZL = this.bill.DJZL;
                bool hYSY = this.bill.HYSY;
                this.bill.DJZL = this.comboBoxDJZL.SelectedValue.ToString();
                this.saleBillBL.OceanOilHasChanged(dJZL, hYSY, this.bill);
                if (dJZL != this.bill.DJZL)
                {
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    bool flag2 = false;
                    bool flag3 = false;
                    this.bill.TYDH = "";
                    if (this.bill.DJZL == "c")
                    {
                        if (flag2 && flag3)
                        {
                            this.checkBox_XF.Visible = true;
                            this.checkBox_GF.Visible = true;
                        }
                        else if (flag2)
                        {
                            this.checkBox_XF.Visible = true;
                            this.checkBox_GF.Visible = false;
                        }
                        else if (flag3)
                        {
                            this.checkBox_XF.Visible = false;
                            this.checkBox_GF.Visible = true;
                        }
                        else
                        {
                            this.checkBox_XF.Visible = false;
                            this.checkBox_GF.Visible = false;
                        }
                    }
                    else
                    {
                        if (this.checkBox_GF.Checked)
                        {
                            this.bill.GFMC = "";
                            this.bill.GFSH = "";
                            this.bill.GFDZDH = "";
                            this.bill.GFYHZH = "";
                            this.bill.KHYHMC = "";
                            this.bill.KHYHZH = "";
                            this.bill.XFDZDH = "";
                            this.bill.XFYHZH = "";
                        }
                        this.checkBox_XF.Visible = false;
                        this.checkBox_GF.Visible = false;
                    }
                    this.ffSlv(this.bill.DJZL);
                    this.com_gfmc.set_Edit(1);
                    this.com_gfsbh.set_Edit(1);
                    this.com_xfmc.set_Edit(0);
                    this.com_xfsbh.set_Edit(0);
                    this.com_xfmc.Text = card.get_Corporation();
                    this.com_xfsbh.Text = card.get_TaxCode();
                    this.checkBox_XF.Visible = false;
                    this.checkBox_GF.Visible = false;
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
            finally
            {
                this.checkBox_HYSY.CheckedChanged += new EventHandler(this.checkBox_HYSY_CheckedChanged);
            }
            this.saleBillBL.CleanBill(this.bill);
            this.ToView();
        }

        private void comboBoxSLV_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (this.dataGrid())
                {
                    int rowIndex;
                    bool flag;
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                            flag = false;
                            this.dgv_spmx.Focus();
                            rowIndex = this.dgv_spmx.CurrentCell.RowIndex;
                            this.dgv_spmx.CurrentCell = this.dgv_spmx.Rows[rowIndex].Cells["JE"];
                            return;

                        case Keys.Up:
                            this.comboBox_SLV.Focus();
                            return;

                        case Keys.Right:
                            flag = false;
                            this.dgv_spmx.Focus();
                            rowIndex = this.dgv_spmx.CurrentCell.RowIndex;
                            this.dgv_spmx.CurrentCell = this.dgv_spmx.Rows[rowIndex].Cells["SE"];
                            return;

                        case Keys.Down:
                            this.comboBox_SLV.Focus();
                            return;

                        case Keys.Enter:
                            this.dgv_spmx.Focus();
                            return;

                        case Keys.Tab:
                            return;
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void CompleteSlv(string FLBM, string SPBM)
        {
            List<double> list = new SaleBillDAL().GET_YHZCSLV_BY_SPBM(SPBM, "s");
            foreach (double num in list)
            {
                string str = Convert.ToString((double) (num * 100.0)) + "%";
                if (!this.comboBox_SLV.Items.Contains(str))
                {
                    this.comboBox_SLV.Items.Add(str);
                }
            }
        }

        private string ConvertXfdzdh(string xfdzdhs)
        {
            string[] strArray = xfdzdhs.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join("", strArray);
        }

        private bool dataGrid()
        {
            string str = this.saleBillBL.CheckEditGoods(this.bill);
            if (str != "0")
            {
                MessageManager.ShowMsgBox(str);
                return false;
            }
            return true;
        }

        private void dataGridMX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                switch (this.dgv_spmx.Columns[e.ColumnIndex].Name)
                {
                    case "SPMC":
                    case "SLV":
                        this.dataGridMX1_CurrentCellChanged(sender, new EventArgs());
                        break;
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void dataGridMX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dtEndEdit != new DateTime(0x76c, 1, 1, 0, 0, 0, 0))
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.dtEndEdit);
                    if (span.TotalMilliseconds < 200.0)
                    {
                        return;
                    }
                }
                this.dtEndEdit = DateTime.Now;
                bool isCurrentCellDirty = this.dgv_spmx.IsCurrentCellDirty;
                int rowIndex = e.RowIndex;
                if ((e.RowIndex <= -1) || (this.bill.ListGoods.Count <= 0))
                {
                    return;
                }
                if (this.bill.HYSY && (this.bill.ListGoods[rowIndex].KCE != 0.0))
                {
                    MessageBoxHelper.Show("中外合作油气田 取消差额征税");
                }
                else if (this.bill.JZ_50_15 && (this.bill.ListGoods[rowIndex].KCE != 0.0))
                {
                    MessageBoxHelper.Show("减按1.5%计算 取消差额征税");
                }
                string str = (e.RowIndex + 1).ToString();
                string name = this.dgv_spmx.Columns[e.ColumnIndex].Name;
                object obj2 = this.dgv_spmx.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string sJe = (obj2 != null) ? obj2.ToString() : null;
                string str4 = "0";
                string str5 = name;
                if (str5 == null)
                {
                    goto Label_02E0;
                }
                if (!(str5 == "JE"))
                {
                    if (str5 == "SE")
                    {
                        goto Label_023D;
                    }
                    if (str5 == "SL")
                    {
                        goto Label_0252;
                    }
                    if (str5 == "DJ")
                    {
                        goto Label_0290;
                    }
                    if (str5 == "KCE")
                    {
                        goto Label_02CE;
                    }
                    goto Label_02E0;
                }
                str4 = this.bill.setJe(rowIndex, sJe);
                this.bill.setKCE(rowIndex, this.bill.ListGoods[rowIndex].KCE.ToString());
                goto Label_02FA;
            Label_023D:
                str4 = this.bill.setSe(rowIndex, sJe);
                goto Label_02FA;
            Label_0252:
                str4 = this.bill.setSl(rowIndex, sJe);
                this.bill.setKCE(rowIndex, this.bill.ListGoods[rowIndex].KCE.ToString());
                goto Label_02FA;
            Label_0290:
                str4 = this.bill.setDj(rowIndex, sJe);
                this.bill.setKCE(rowIndex, this.bill.ListGoods[rowIndex].KCE.ToString());
                goto Label_02FA;
            Label_02CE:
                str4 = this.bill.setKCE(rowIndex, sJe);
                goto Label_02FA;
            Label_02E0:
                str4 = this.saleBillBL.EditGoodsBaseYY(this.bill, rowIndex, name, sJe);
            Label_02FA:
                if (str4 != "0")
                {
                    MessageManager.ShowMsgBox(str4, new string[] { str });
                }
                this.saleBillBL.CleanBill(this.bill);
                this.ToView();
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void dataGridMX1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex == 5) || (e.ColumnIndex == 7))
                {
                    if (e.Value != null)
                    {
                        double result = 0.0;
                        double.TryParse(e.Value.ToString(), out result);
                        if (this.dgv_spmx.Rows[e.RowIndex].Cells["DJHXZ"].Value.Equals(4) || this.dgv_spmx.Rows[e.RowIndex].Cells["DJHXZ"].Value.Equals(3))
                        {
                            if (result == 0.0)
                            {
                                e.Value = null;
                            }
                        }
                        else if (result == 0.0)
                        {
                            e.Value = null;
                        }
                    }
                }
                else if ((e.ColumnIndex == 3) || (e.ColumnIndex == 4))
                {
                    if ((e.Value != null) && (e.Value.ToString().Trim() == "0"))
                    {
                        e.Value = null;
                    }
                }
                else if ((e.ColumnIndex == 6) && (e.Value != null))
                {
                    double num2 = 0.0;
                    bool flag = double.TryParse(e.Value.ToString().Replace("%", ""), out num2);
                    if (this.IsHYSY)
                    {
                        if (flag)
                        {
                            if (num2 == 0.05)
                            {
                                e.Value = "中外合作油气田";
                            }
                            else if (Math.Abs((double) (num2 - 0.015)) < 1E-05)
                            {
                                e.Value = "减按1.5%计算";
                            }
                            else
                            {
                                e.Value = (Convert.ToDouble(e.Value) * 100.0) + "%";
                            }
                        }
                    }
                    else if (Convert.ToDouble(e.Value) == 0.0)
                    {
                        if (!CommonTool.isSPBMVersion())
                        {
                            e.Value = "免税";
                        }
                    }
                    else if (Math.Abs((double) (num2 - 0.015)) < 1E-05)
                    {
                        e.Value = "减按1.5%计算";
                    }
                    else
                    {
                        e.Value = (Convert.ToDouble(e.Value) * 100.0) + "%";
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void dataGridMX1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                this.valCol = e.ColumnIndex;
                this.valRow = e.RowIndex;
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void dataGridMX1_CellValidatingYuan(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                string str;
                if (this.dgv_spmx.Rows[e.RowIndex].Cells["DJHXZ"].Value.Equals(0))
                {
                    str = (e.RowIndex + 1).ToString();
                    string name = this.dgv_spmx.Columns[e.ColumnIndex].Name;
                    if (name != null)
                    {
                        if (!(name == "JE"))
                        {
                            if (name == "SE")
                            {
                                goto Label_0117;
                            }
                            if (!(name == "SPSM"))
                            {
                                if (name == "SL")
                                {
                                    goto Label_016B;
                                }
                                if (name == "DJ")
                                {
                                    goto Label_0213;
                                }
                            }
                        }
                        else if (CommonTool.RegexMatchNum(e.FormattedValue))
                        {
                            this.NumError = false;
                        }
                        else
                        {
                            MessageManager.ShowMsgBox("A115", new string[] { str });
                            this.NumError = true;
                        }
                    }
                }
                return;
            Label_0117:
                if (CommonTool.RegexMatchNum(e.FormattedValue))
                {
                    this.NumError = false;
                }
                else
                {
                    MessageManager.ShowMsgBox("A117", new string[] { str });
                    this.NumError = true;
                    e.Cancel = true;
                }
                return;
            Label_016B:
                if (CommonTool.RegexMatchNum(e.FormattedValue))
                {
                    this.NumError = false;
                }
                else if (e.FormattedValue.ToString().Trim().Length == 0)
                {
                    this.NumError = false;
                    this.dgv_spmx.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
                else
                {
                    this.NumError = true;
                    MessageManager.ShowMsgBox("A118", new string[] { str });
                }
                return;
            Label_0213:
                if (CommonTool.RegexMatchNum(e.FormattedValue))
                {
                    if (Convert.ToDouble(e.FormattedValue) < 0.0)
                    {
                        MessageManager.ShowMsgBox("A112", new string[] { str });
                        this.NumError = true;
                    }
                    else
                    {
                        this.NumError = false;
                    }
                }
                else if (e.FormattedValue.ToString().Trim().Length == 0)
                {
                    this.NumError = e.Cancel = false;
                    this.dgv_spmx.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
                else
                {
                    this.NumError = true;
                    MessageManager.ShowMsgBox("A114", new string[] { str });
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void dataGridMX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dgv_spmx.Columns[e.ColumnIndex].Name == "SLV")
                {
                    this.dataGridMX1_CellEndEdit(sender, e);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void dataGridMX1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dgv_spmx.CurrentCell != null)
                {
                    int rowIndex = this.dgv_spmx.CurrentCell.RowIndex;
                    DataGridViewColumn owningColumn = this.dgv_spmx.CurrentCell.OwningColumn;
                    object obj2 = 0;
                    if (this.dgv_spmx.CurrentRow.Cells["DJHXZ"].Value != null)
                    {
                        obj2 = this.dgv_spmx.CurrentRow.Cells["DJHXZ"].Value;
                        if (this.dgv_spmx.Rows[rowIndex].ReadOnly)
                        {
                            this.spmcBt.Visible = false;
                            this.comboBox_SLV.Visible = false;
                        }
                        else if ((rowIndex >= 0) && obj2.Equals(0))
                        {
                            Rectangle rectangle;
                            int index = owningColumn.Index;
                            if (owningColumn.Name == "SPMC")
                            {
                                object sPMC;
                                this.spmcBt.Visible = true;
                                rectangle = this.dgv_spmx.GetCellDisplayRectangle(index, rowIndex, true);
                                this.spmcBt.Location = new Point(rectangle.Location.X, rectangle.Location.Y);
                                this.spmcBt.Size = rectangle.Size;
                                if (rowIndex >= this.bill.ListGoods.Count)
                                {
                                    sPMC = this.dgv_spmx.CurrentCell.Value;
                                }
                                else
                                {
                                    sPMC = this.bill.ListGoods[rowIndex].SPMC;
                                }
                                this.spmcBt.Text = (sPMC == null) ? "" : sPMC.ToString();
                                DataTable table = this.spmcBt.get_DataSource();
                                if (table != null)
                                {
                                    table.Clear();
                                }
                                this.spmcBt.Visible = true;
                                this.spmcBt.Focus();
                                this.flagAdd = true;
                            }
                            else if (owningColumn.Name == "SLV")
                            {
                                this.comboBox_SLV.Visible = true;
                                rectangle = this.dgv_spmx.GetCellDisplayRectangle(index, rowIndex, false);
                                this.comboBox_SLV.Location = new Point(rectangle.Location.X + 2, rectangle.Location.Y + 1);
                                this.comboBox_SLV.Height = rectangle.Height;
                                this.comboBox_SLV.Width = rectangle.Width - 3;
                                this.ffSlv(this.bill.DJZL);
                                object obj4 = this.dgv_spmx.CurrentCell.Value;
                                double result = -1.0;
                                double.TryParse(obj4.ToString(), out result);
                                if (obj4 == null)
                                {
                                    this.comboBox_SLV.Text = "";
                                }
                                else if (obj4.Equals("免税"))
                                {
                                    this.comboBox_SLV.Text = "免税";
                                }
                                else if (obj4.Equals("不征税"))
                                {
                                    this.comboBox_SLV.Text = "不征税";
                                }
                                else if (obj4.Equals("中外合作油气田"))
                                {
                                    this.comboBox_SLV.Text = "中外合作油气田";
                                }
                                else if (Math.Abs((double) (result - 0.015)) < 1E-05)
                                {
                                    this.comboBox_SLV.Text = "减按1.5%计算";
                                }
                                else if (obj4.ToString().Contains("%"))
                                {
                                    this.comboBox_SLV.Text = obj4.ToString();
                                }
                                else
                                {
                                    string str = ((Convert.ToDouble(obj4) * 100.0)).ToString() + "%";
                                    int count = this.comboBox_SLV.Items.Count;
                                    if (this.comboBox_SLV.Items.Contains(str))
                                    {
                                        this.comboBox_SLV.Text = str;
                                    }
                                    else if (this.comboBox_SLV.Items.Contains("中外合作油气田") && (str == "5%"))
                                    {
                                        this.comboBox_SLV.SelectedIndex = 0;
                                    }
                                    else
                                    {
                                        this.comboBox_SLV.Items.Add(str);
                                        this.comboBox_SLV.Text = str;
                                    }
                                }
                                this.comboBox_SLV.Focus();
                            }
                            else
                            {
                                this.spmcBt.Visible = false;
                                this.comboBox_SLV.Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    if (this.comboBox_SLV != null)
                    {
                        this.comboBox_SLV.Visible = false;
                    }
                    if (this.spmcBt != null)
                    {
                        this.spmcBt.Visible = false;
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void dataGridMX1_KeyDown(object sender, KeyEventArgs e)
        {
            bool alt = e.Alt;
        }

        private void dataGridMX1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.spmcBt.Visible)
                {
                    this.spmcBt.Visible = false;
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void dataGridMX1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (this.dgv_spmx.Rows.Count == 0)
                {
                    if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Insert))
                    {
                        this.AddGridGoodsRow(0);
                    }
                }
                else
                {
                    int index = this.dgv_spmx.CurrentRow.Index;
                    int columnIndex = this.dgv_spmx.CurrentCell.ColumnIndex;
                    int count = this.dgv_spmx.Rows.Count;
                    int num4 = this.dgv_spmx.Columns.Count;
                    switch (e.KeyCode)
                    {
                        case Keys.Tab:
                        case Keys.Enter:
                        case Keys.Left:
                        case Keys.Right:
                        case Keys.Select:
                        case Keys.Print:
                        case Keys.Execute:
                        case Keys.PrintScreen:
                            return;

                        case Keys.Up:
                        {
                            DataGridViewRow row = this.dgv_spmx.Rows[index];
                            if ((((row.Cells[0].Value == null) || row.Cells[0].Value.Equals("")) && (row.Cells[1].Value == null)) && (row.Cells[2].Value == null))
                            {
                                this.DelGridGoodsRow();
                            }
                            return;
                        }
                        case Keys.Down:
                            if (index == (count - 1))
                            {
                                this.AddGridGoodsRow(0);
                            }
                            return;

                        case Keys.Insert:
                            this.AddGridGoodsRow(1);
                            return;

                        case Keys.Delete:
                            if (SaleBillCtrl.Instance.CanEditBill(this.bill) <= 0)
                            {
                                this.DelGridGoodsRow();
                            }
                            return;
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void DelGridGoodsRow()
        {
            try
            {
                if (this.dgv_spmx.Rows.Count >= 1)
                {
                    int rowIndex = this.dgv_spmx.CurrentCell.RowIndex;
                    if ((this.bill.GetGood(rowIndex).DJHXZ != 4) || (MessageBoxHelper.Show("您要删除的商品行为折扣行，删除此行将取消上行商品的折扣，您要继续吗?", "确认", MessageBoxButtons.YesNo) != DialogResult.No))
                    {
                        this.dgv_spmx.EndEdit();
                        string str = this.saleBillBL.RemoveGoods(this.bill, rowIndex);
                        if (str == "0")
                        {
                            this.saleBillBL.CleanBill(this.bill);
                            this.ToView();
                            this.spmcBt.Visible = false;
                            if (this.dgv_spmx.Rows.Count > 0)
                            {
                                int num2 = this.dgv_spmx.CurrentCell.RowIndex;
                                this.dgv_spmx.CurrentCell = this.dgv_spmx.Rows[num2].Cells["SE"];
                            }
                        }
                        else
                        {
                            MessageBoxHelper.Show(str, "单据修改", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void dgv_spmx_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ShowCurrentSPRowNumInfo();
        }

        private void dgv_spmx_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void dgv_spmx_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                this.spmcBt.Visible = false;
                this.comboBox_SLV.Visible = false;
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
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

        private void DrawLines(Graphics g)
        {
            PointF orient = new PointF(this.WA, this.HA);
            PointF tf2 = new PointF(base.Width - this.WB, this.HA);
            PointF tf3 = new PointF(this.WA, base.Height - this.HB);
            PointF tf4 = new PointF(base.Width - this.WB, base.Height - this.HB);
            float num = 100f;
            PointF tf5 = new PointF(orient.X, orient.Y + num);
            PointF tf6 = new PointF(tf2.X, tf2.Y + num);
            float num2 = 100f;
            PointF tf7 = this.NewPointF(tf3, 0f, -num2);
            PointF tf8 = this.NewPointF(tf4, 0f, -num2);
            float num3 = 25f;
            PointF tf9 = this.NewPointF(tf7, 0f, -num3);
            PointF tf10 = this.NewPointF(tf8, 0f, -num3);
            float x = 30f;
            PointF tf11 = this.NewPointF(orient, x, 0f);
            PointF tf12 = this.NewPointF(tf5, x, 0f);
            PointF tf13 = this.NewPointF(tf7, x, 0f);
            PointF tf14 = this.NewPointF(tf3, x, 0f);
            float num5 = 47f * this.W;
            PointF tf15 = this.NewPointF(orient, x + num5, 0f);
            PointF tf16 = this.NewPointF(tf5, x + num5, 0f);
            PointF tf17 = this.NewPointF(tf7, x + num5, 0f);
            this.lab_bz.Location = this.ToPoint(tf17, 2, 5);
            PointF tf18 = this.NewPointF(tf3, x + num5, 0f);
            int num6 = 120;
            this.com_gfmc.Width = Convert.ToInt32((float) (num5 - num6));
            this.com_gfsbh.Width = Convert.ToInt32((float) (num5 - num6));
            this.com_gfzh.Width = Convert.ToInt32((float) (num5 - num6));
            this.com_gfdzdh.Width = Convert.ToInt32((float) (num5 - num6));
            this.com_xfdzdh.Width = Convert.ToInt32((float) (num5 - num6));
            this.com_xfmc.Width = Convert.ToInt32((float) (num5 - num6));
            this.com_xfsbh.Width = Convert.ToInt32((float) (num5 - num6));
            this.com_xfyhzh.Width = Convert.ToInt32((float) (num5 - num6));
            float num7 = 25f;
            PointF tf19 = this.NewPointF(orient, (x + num5) + num7, 0f);
            PointF tf20 = this.NewPointF(tf5, (x + num5) + num7, 0f);
            PointF tf21 = this.NewPointF(tf7, (x + num5) + num7, 0f);
            this.txt_bz.Location = this.ToPoint(tf21, 2, 5);
            this.txt_bz.Width = (base.Width - Convert.ToInt32((float) ((((this.WA + x) + num5) + num7) + this.WB))) - 10;
            PointF tf22 = this.NewPointF(tf3, (x + num5) + num7, 0f);
            PointF tf23 = this.NewPointF(tf9, (float) (base.Width / 5), 0f);
            PointF tf24 = this.NewPointF(tf7, (float) (base.Width / 5), 0f);
            this.lab_hj_jshj_dx.Location = this.ToPoint(tf23, 2, 5);
            using (Pen pen = new Pen(Color.Black))
            {
                g.DrawLine(pen, orient, tf2);
                g.DrawLine(pen, tf2, tf4);
                g.DrawLine(pen, orient, tf3);
                g.DrawLine(pen, tf3, tf4);
                g.DrawLine(pen, tf5, tf6);
                g.DrawLine(pen, tf9, tf10);
                g.DrawLine(pen, tf7, tf8);
                g.DrawLine(pen, tf11, tf12);
                g.DrawLine(pen, tf13, tf14);
                g.DrawLine(pen, tf15, tf16);
                g.DrawLine(pen, tf17, tf18);
                g.DrawLine(pen, tf19, tf20);
                g.DrawLine(pen, tf21, tf22);
                g.DrawLine(pen, tf23, tf24);
                g.DrawLine(pen, (float) (35f * this.W), (float) ((this.Hpad / 2f) + 6f), (float) (65f * this.W), (float) ((this.Hpad / 2f) + 6f));
                g.DrawLine(pen, (float) (35f * this.W), (float) ((this.Hpad / 2f) + 3f), (float) (65f * this.W), (float) ((this.Hpad / 2f) + 3f));
            }
            float num8 = 50f;
            float y = 10f;
            float width = 15f;
            while (y < base.Height)
            {
                g.FillEllipse(SystemBrushes.ControlDark, 10f, y, width, width);
                g.FillEllipse(SystemBrushes.ControlDark, (float) (base.Width - 30), y, width, width);
                y += num8;
            }
        }

        private void ffSlv(string fplx)
        {
            PZSQType type = null;
            int num;
            double num2;
            string str2;
            double num4;
            if (fplx == "s")
            {
                if (this.bill.HYSY)
                {
                    this.comboBox_SLV.Items.Clear();
                    this.comboBox_SLV.Items.Add("中外合作油气田");
                    this.comboBox_SLV.SelectedIndex = 0;
                    return;
                }
                type = this.isi_Special;
            }
            else if (fplx == "c")
            {
                type = this.isi_Common;
            }
            this.comboBox_SLV.Items.Clear();
            if (type != null)
            {
                List<TaxRateType> taxRate = type.TaxRate;
                for (num = 0; num < taxRate.Count; num++)
                {
                    if (double.TryParse(taxRate[num].Rate.ToString("F03"), out num2))
                    {
                        num4 = num2 * 100.0;
                        str2 = num4.ToString() + "%";
                        if ((Math.Round(num2, 6) == 0.0) && !CommonTool.isSPBMVersion())
                        {
                            str2 = "免税";
                        }
                        if (!this.comboBox_SLV.Items.Contains(str2))
                        {
                            this.comboBox_SLV.Items.Add(str2);
                        }
                    }
                }
                taxRate = type.TaxRate2;
                for (num = 0; num < taxRate.Count; num++)
                {
                    if (double.TryParse(taxRate[num].Rate.ToString("F03"), out num2))
                    {
                        num4 = num2 * 100.0;
                        str2 = num4.ToString() + "%";
                        if (num2 == 0.05)
                        {
                            str2 = "中外合作油气田";
                        }
                        else if (Math.Abs((double) (num2 - 0.015)) < 1E-05)
                        {
                            str2 = "减按1.5%计算";
                        }
                        if (!this.comboBox_SLV.Items.Contains(str2))
                        {
                            this.comboBox_SLV.Items.Add(str2);
                        }
                    }
                }
            }
            if (CommonTool.isSPBMVersion() && (this.dgv_spmx.CurrentCell != null))
            {
                bool xSYH = false;
                string xSYHSM = "";
                string fLBM = "";
                string bM = "";
                string str6 = "出口零税";
                string str7 = "免税";
                string str8 = "不征税";
                int rowIndex = this.dgv_spmx.CurrentCell.RowIndex;
                Goods good = this.bill.GetGood(rowIndex);
                xSYH = good.XSYH;
                xSYHSM = good.XSYHSM;
                fLBM = good.FLBM;
                bM = good.SPBM;
                List<double> list2 = new SaleBillDAL().GET_YHZCSLV_BY_SPBM(bM, "c");
                for (num = 0; num < list2.Count; num++)
                {
                    num4 = list2[num];
                    if (double.TryParse(num4.ToString("F03"), out num2))
                    {
                        if (Finacial.Equal(num2, 0.0))
                        {
                            if ((fplx != "s") && (xSYHSM != ""))
                            {
                                if (xSYHSM.Contains(str6) && !this.comboBox_SLV.Items.Contains("0%"))
                                {
                                    this.comboBox_SLV.Items.Add("0%");
                                }
                                if (xSYHSM.Contains(str7))
                                {
                                    this.comboBox_SLV.Items.Add(str7);
                                }
                                if (xSYHSM.Contains(str8))
                                {
                                    this.comboBox_SLV.Items.Add(str8);
                                }
                            }
                        }
                        else
                        {
                            str2 = ((num2 * 100.0)).ToString() + "%";
                            if (!this.comboBox_SLV.Items.Contains(str2))
                            {
                                this.comboBox_SLV.Items.Add(str2);
                            }
                        }
                    }
                }
            }
        }

        private void gfxx_OnAutoComplate(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                text = combox.Text;
                DataTable table = this._GfxxOnAutoCompleteDataSource(text);
                if (table != null)
                {
                    combox.set_DataSource(table);
                }
            }
        }

        private void gfxx_OnButtonClick(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            this._GfxxSelect(combox.Text, 0);
        }

        private void gfxx_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.get_SelectDict();
                this.SetGfxx(new object[] { dictionary["MC"], dictionary["SH"], dictionary["DZDH"], dictionary["YHZH"] });
            }
        }

        private void IniteControl()
        {
            this.BackColor = Color.White;
            this.toolStripBtnHSJG.CheckOnClick = true;
            this.com_gfmc.set_buttonStyle(0);
            this.com_gfmc.PreviewKeyDown += new PreviewKeyDownEventHandler(this.com_gfmc_PreviewKeyDown);
            this._SetGfxxControl(this.com_gfsbh, "SH");
            this._SetGfxxControl(this.com_gfmc, "MC");
            this.com_xfmc.set_buttonStyle(0);
            this.com_xfmc.add_OnButtonClick(new EventHandler(this.com_xfmc_OnButtonClick));
            this.com_xfmc.PreviewKeyDown += new PreviewKeyDownEventHandler(this.com_xfmc_PreviewKeyDown);
            this.com_gfdzdh.set_IsSelectAll(true);
            this.com_gfdzdh.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "DZDH", this.com_gfdzdh.Width));
            this.com_gfdzdh.set_DrawHead(false);
            this.com_gfzh.set_IsSelectAll(true);
            this.com_gfzh.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "YHZH", this.com_gfzh.Width));
            this.com_gfzh.set_DrawHead(false);
            this.com_gfsbh.OnTextChanged = (EventHandler) Delegate.Combine(this.com_gfsbh.OnTextChanged, new EventHandler(this.com_gfsbh_TextChanged));
            this.dgv_spmx.AllowUserToAddRows = false;
            this.spmcBt = new AisinoMultiCombox();
            this.spmcBt.set_IsSelectAll(true);
            this.spmcBt.Name = "SPMCBT";
            this.spmcBt.Text = "";
            this.spmcBt.Padding = new Padding(0);
            this.spmcBt.Margin = new Padding(0);
            this.spmcBt.Visible = false;
            this.spmcBt.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("商品名称", "MC", 160));
            this.spmcBt.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("计量单位", "JLDW", 60));
            this.spmcBt.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("税率", "SLV", 40));
            this.spmcBt.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("单价", "DJ", 60));
            this.spmcBt.set_ShowText("MC");
            this.spmcBt.set_DrawHead(true);
            this.spmcBt.set_AutoIndex(0);
            this.spmcBt.set_AutoComplate(2);
            this.spmcBt.add_OnAutoComplate(new EventHandler(this.spmcBt_OnAutoComplate));
            this.spmcBt.set_buttonStyle(0);
            this.spmcBt.add_OnButtonClick(new EventHandler(this.spmcBt_Click));
            this.spmcBt.DoubleClick += new EventHandler(this.spmcBt_Click);
            this.spmcBt.add_OnSelectValue(new EventHandler(this.spmcBt_OnSelectValue));
            this.spmcBt.PreviewKeyDown += new PreviewKeyDownEventHandler(this.spmcBt_PreviewKeyDown);
            this.spmcBt.Validating += new CancelEventHandler(this.spmcBt_Validating);
            this.spmcBt.Leave += new EventHandler(this.spmcBt_Leave);
            this.comboBoxDJZL.DataSource = CbbXmlBind.ReadXmlNode("FaPiao", false);
            this.comboBoxDJZL.DisplayMember = "Value";
            this.comboBoxDJZL.ValueMember = "Key";
            this.comboBoxDJZL.DropDownStyle = ComboBoxStyle.DropDownList;
            if (!this.IsUpdate && (this.saleBillBL.XGDJZL != "a"))
            {
                this.comboBoxDJZL.Enabled = false;
                this.comboBoxDJZL.DropDownStyle = ComboBoxStyle.Simple;
            }
            TaxCard card = TaxCardFactory.CreateTaxCard();
            this.comboBox_SLV = new AisinoCMB();
            this.isi_Common = card.get_SQInfo().get_Item(2);
            this.isi_Special = card.get_SQInfo().get_Item(0);
            this.ffSlv(this.bill.DJZL);
            this.comboBox_SLV.Visible = false;
            this.comboBox_SLV.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_SLV.KeyUp += new KeyEventHandler(this.comboBox_SLV_KeyUp);
            this.comboBox_SLV.PreviewKeyDown += new PreviewKeyDownEventHandler(this.comboBoxSLV_PreviewKeyDown);
            this.comboBox_SLV.SelectionChangeCommitted += new EventHandler(this.comboBox_SLV_SelectionChangeCommitted);
            this.dgv_spmx.Controls.Add(this.spmcBt);
            this.dgv_spmx.Controls.Add(this.comboBox_SLV);
            this.dgv_spmx.CellClick += new DataGridViewCellEventHandler(this.dgv_spmx_CellClick);
            base.KeyPreview = true;
            this.com_xfmc.Text = card.get_Corporation();
            this.com_xfsbh.Text = card.get_TaxCode();
            this.com_xfmc.set_Edit(0);
            this.com_xfsbh.set_Edit(0);
            bool flag = false;
            bool flag2 = false;
            if (flag && flag2)
            {
                this.checkBox_XF.Visible = true;
                this.checkBox_GF.Visible = true;
            }
            else if (flag)
            {
                this.checkBox_XF.Visible = true;
                this.checkBox_GF.Visible = false;
            }
            else if (flag2)
            {
                this.checkBox_XF.Visible = false;
                this.checkBox_GF.Visible = true;
            }
            else
            {
                this.checkBox_XF.Visible = false;
                this.checkBox_GF.Visible = false;
            }
            this.checkBox_XF.Visible = false;
            this.checkBox_GF.Visible = false;
            if (RegisterManager.CheckRegFile("JIJS"))
            {
                this.comboBoxDJZL.SelectedIndex = 1;
                this.comboBoxDJZL.Enabled = false;
            }
            if ((this.bill.DJZT == "W") || (this.bill.KPZT == "A"))
            {
                this.dgv_spmx.ReadOnly = true;
                this.toolStripBtnSave.Visible = false;
                this.toolStripButtonAdd.Visible = false;
                this.toolStripButtonDel.Visible = false;
                this.toolStripButtonKH.Visible = false;
                this.toolStripButtonZK.Visible = false;
            }
            else if (this.bill.KPZT == "P")
            {
                this.toolStripBtnSave.Visible = true;
                this.toolStripButtonAdd.Visible = true;
                this.toolStripButtonDel.Visible = true;
                this.toolStripButtonKH.Visible = true;
                this.toolStripButtonZK.Visible = true;
            }
            if (this.saleBillBL.IsSWDK())
            {
                this.com_xfdzdh.Text = "";
                this.com_xfyhzh.Text = "";
                this._SetXfxxControl(this.com_xfdzdh, "DZDH");
            }
            else
            {
                this.SetXfDzdh();
                this.SetXfyhzh();
            }
            this.SetSkrAndFhr();
            this.textBoxDJH.Leave += new EventHandler(this.textBoxLeave);
            this.txt_bz.Leave += new EventHandler(this.textBoxLeave);
            this.com_fhr.Leave += new EventHandler(this.textBoxLeave);
            this.com_gfdzdh.Leave += new EventHandler(this.textBoxLeave);
            this.com_gfmc.Leave += new EventHandler(this.textBoxLeave);
            this.com_gfsbh.Leave += new EventHandler(this.textBoxLeave);
            this.com_gfzh.Leave += new EventHandler(this.textBoxLeave);
            this.textBoxQDSPHMX.Leave += new EventHandler(this.textBoxLeave);
            this.com_xfmc.Leave += new EventHandler(this.textBoxLeave);
            this.com_xfsbh.Leave += new EventHandler(this.textBoxLeave);
            this.com_skr.Leave += new EventHandler(this.textBoxLeave);
            this.com_xfdzdh.Leave += new EventHandler(this.textBoxLeave);
            this.dateTimePicker1.Leave += new EventHandler(this.textBoxLeave);
            this.checkBox_SFZJY.Leave += new EventHandler(this.textBoxLeave);
            this.textBoxDJH.TextChanged += new EventHandler(this.textBoxDJH_TextChanged);
            this.txt_bz.TextChanged += new EventHandler(this.txt_bz_TextChanged);
            this.com_xfdzdh.TextChanged += new EventHandler(this.textBoxLeave);
            this.com_xfyhzh.OnTextChanged = (EventHandler) Delegate.Combine(this.com_xfyhzh.OnTextChanged, new EventHandler(this.com_xfyhzh_TextChanged));
            this.com_skr.TextChanged += new EventHandler(this.textBoxLeave);
            this.com_fhr.TextChanged += new EventHandler(this.textBoxLeave);
            this.toolStripButtonCE.Click += new EventHandler(this.toolStripButtonCE_Click);
        }

        private void IniteGridMX()
        {
            this.dgv_spmx.AutoGenerateColumns = false;
            this.dgv_spmx.AutoSize = false;
            this.dgv_spmx.AllowUserToOrderColumns = true;
            this.dgv_spmx.AllowUserToAddRows = false;
            this.dgv_spmx.BackgroundColor = Color.WhiteSmoke;
            this.dgv_spmx.ScrollBars = ScrollBars.Both;
            this.dgv_spmx.AllowUserToResizeColumns = true;
            this.dgv_spmx.AllowUserToResizeRows = false;
            this.dgv_spmx.EnableHeadersVisualStyles = true;
            this.dgv_spmx.Columns.Add("SPMC", "货物或应税劳务、服务名称");
            this.dgv_spmx.Columns.Add("GGXH", "规格型号");
            this.dgv_spmx.Columns.Add("JLDW", "单位");
            this.dgv_spmx.Columns.Add("SL", "数量");
            this.dgv_spmx.Columns.Add("DJ", "单价");
            this.dgv_spmx.Columns.Add("JE", "金额");
            this.dgv_spmx.Columns.Add("SLV", "税率");
            this.dgv_spmx.Columns.Add("SE", "税额");
            this.dgv_spmx.Columns.Add("DJHXZ", "单据行性质");
            this.dgv_spmx.Columns.Add("SPSM", "商品税目");
            this.dgv_spmx.Columns.Add("KCE", "扣除额");
            this.dgv_spmx.Columns.Add("FPZL", "发票种类");
            this.dgv_spmx.Columns.Add("FPDM", "发票代码");
            this.dgv_spmx.Columns.Add("FPHM", "发票号码");
            this.dgv_spmx.Columns["SPMC"].Width = 150;
            this.dgv_spmx.Columns["GGXH"].Width = 70;
            this.dgv_spmx.Columns["JLDW"].Width = 50;
            this.dgv_spmx.Columns["SL"].Width = 60;
            this.dgv_spmx.Columns["DJ"].Width = 90;
            this.dgv_spmx.Columns["JE"].Width = 90;
            this.dgv_spmx.Columns["KCE"].Width = 90;
            this.dgv_spmx.Columns["SLV"].Width = 90;
            this.dgv_spmx.Columns["SE"].Width = 80;
            this.dgv_spmx.Columns["SPSM"].Width = 70;
            this.dgv_spmx.Columns["FPZL"].Width = 70;
            this.dgv_spmx.Columns["FPDM"].Width = 80;
            this.dgv_spmx.Columns["FPHM"].Width = 70;
            this.dgv_spmx.Columns["DJHXZ"].Visible = false;
            if (!CommonTool.isCEZS())
            {
                this.dgv_spmx.Columns["KCE"].Visible = false;
            }
            this.dgv_spmx.Columns["JE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_spmx.Columns["SE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_spmx.Columns["SL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_spmx.Columns["DJ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_spmx.Columns["KCE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_spmx.Columns["JE"].DefaultCellStyle.Format = "N2";
            this.dgv_spmx.Columns["SE"].DefaultCellStyle.Format = "N2";
            this.dgv_spmx.Columns["DJ"].DefaultCellStyle.Format = "G";
            this.dgv_spmx.Columns["SL"].DefaultCellStyle.Format = "G";
            this.dgv_spmx.Columns["KCE"].DefaultCellStyle.Format = "N2";
            this.dgv_spmx.Columns["SLV"].DefaultCellStyle.Format = "0%";
            this.dgv_spmx.Columns["SLV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgv_spmx.Columns["FPZL"].ReadOnly = true;
            this.dgv_spmx.Columns["FPDM"].ReadOnly = true;
            this.dgv_spmx.Columns["FPHM"].ReadOnly = true;
            this.dgv_spmx.Columns["FPDM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_spmx.Columns["FPHM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_spmx.MultiSelect = false;
            for (int i = 0; i < this.dgv_spmx.Columns.Count; i++)
            {
                this.dgv_spmx.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(XSDJEdite));
            this.dateTimePicker1 = new DateTimePicker();
            this.labelDJRQ = new AisinoLBL();
            this.textBoxQDSPHMX = new AisinoTXT();
            this.checkBox_HYSY = new AisinoCHK();
            this.checkBox_SFZJY = new AisinoCHK();
            this.checkBox_XF = new AisinoCHK();
            this.checkBox_GF = new AisinoCHK();
            this.textBoxDJH = new AisinoTXT();
            this.labelDJH = new AisinoLBL();
            this.comboBoxDJZL = new AisinoCMB();
            this.labelDJZL = new AisinoLBL();
            this.labelQD = new AisinoLBL();
            this.toolbtnBuQi = new ToolStrip();
            this.toolStripBtnCancel = new ToolStripButton();
            this.toolStripBtnSave = new ToolStripButton();
            this.toolStripButtonAdd = new ToolStripButton();
            this.toolStripButtonDel = new ToolStripButton();
            this.toolStripButtonZK = new ToolStripButton();
            this.toolStripButtonKH = new ToolStripButton();
            this.toolStripBtnHSJG = new ToolStripButton();
            this.toolBtnCheck = new ToolStripButton();
            this.toolStripButtonCE = new ToolStripButton();
            this.lab_hj_xx = new AisinoLBL();
            this.label2 = new AisinoLBL();
            this.com_skr = new AisinoMultiCombox();
            this.com_fhr = new AisinoMultiCombox();
            this.com_gfzh = new AisinoMultiCombox();
            this.com_gfsbh = new AisinoMultiCombox();
            this.com_xfyhzh = new AisinoMultiCombox();
            this.com_xfdzdh = new AisinoMultiCombox();
            this.com_xfsbh = new AisinoMultiCombox();
            this.com_xfmc = new AisinoMultiCombox();
            this.com_gfdzdh = new AisinoMultiCombox();
            this.com_gfmc = new AisinoMultiCombox();
            this.label17 = new AisinoLBL();
            this.label31 = new AisinoLBL();
            this.label32 = new AisinoLBL();
            this.label29 = new AisinoLBL();
            this.label23 = new AisinoLBL();
            this.label7 = new AisinoLBL();
            this.lab_Tax = new AisinoLBL();
            this.lab_Amount = new AisinoLBL();
            this.label12 = new AisinoLBL();
            this.dgv_spmx = new DataGridView();
            this.txt_bz = new AisinoTXT();
            this.lab_title = new AisinoLBL();
            this.lab_hj_jshj_dx = new AisinoLBL();
            this.lab_bz = new AisinoLBL();
            this.lab_KPZT = new AisinoLBL();
            this.label27 = new AisinoLBL();
            this.label26 = new AisinoLBL();
            this.label21 = new AisinoLBL();
            this.label20 = new AisinoLBL();
            this.label19 = new AisinoLBL();
            this.label18 = new AisinoLBL();
            this.label13 = new AisinoLBL();
            this.label8 = new AisinoLBL();
            this.label9 = new AisinoLBL();
            this.label10 = new AisinoLBL();
            this.label11 = new AisinoLBL();
            this.lab_DJZT = new AisinoLBL();
            this.button1 = new Button();
            this.aisinoLBL_RowInfo = new AisinoLBL();
            this.toolbtnBuQi.SuspendLayout();
            ((ISupportInitialize) this.dgv_spmx).BeginInit();
            base.SuspendLayout();
            this.dateTimePicker1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.dateTimePicker1.Location = new Point(0x367, 0x56);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(0x6a, 0x15);
            this.dateTimePicker1.TabIndex = 3;
            this.labelDJRQ.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.labelDJRQ.AutoSize = true;
            this.labelDJRQ.BackColor = SystemColors.ButtonHighlight;
            this.labelDJRQ.Location = new Point(0x32c, 90);
            this.labelDJRQ.Name = "labelDJRQ";
            this.labelDJRQ.Size = new Size(0x35, 12);
            this.labelDJRQ.TabIndex = 0xd5;
            this.labelDJRQ.Text = "单据日期";
            this.textBoxQDSPHMX.Location = new Point(0x243, 0x56);
            this.textBoxQDSPHMX.Name = "textBoxQDSPHMX";
            this.textBoxQDSPHMX.Size = new Size(0xb3, 0x15);
            this.textBoxQDSPHMX.TabIndex = 2;
            this.checkBox_HYSY.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.checkBox_HYSY.AutoSize = true;
            this.checkBox_HYSY.BackColor = SystemColors.ButtonHighlight;
            this.checkBox_HYSY.Location = new Point(0x296, 0x7a);
            this.checkBox_HYSY.Name = "checkBox_HYSY";
            this.checkBox_HYSY.Size = new Size(0x6c, 0x10);
            this.checkBox_HYSY.TabIndex = 4;
            this.checkBox_HYSY.Text = "中外合作油气田";
            this.checkBox_HYSY.UseVisualStyleBackColor = false;
            this.checkBox_HYSY.CheckedChanged += new EventHandler(this.checkBox_HYSY_CheckedChanged);
            this.checkBox_SFZJY.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.checkBox_SFZJY.AutoSize = true;
            this.checkBox_SFZJY.BackColor = SystemColors.ButtonHighlight;
            this.checkBox_SFZJY.Location = new Point(0x315, 0x7a);
            this.checkBox_SFZJY.Name = "checkBox_SFZJY";
            this.checkBox_SFZJY.Size = new Size(0x54, 0x10);
            this.checkBox_SFZJY.TabIndex = 5;
            this.checkBox_SFZJY.Text = "身份证校验";
            this.checkBox_SFZJY.UseVisualStyleBackColor = false;
            this.checkBox_SFZJY.Visible = false;
            this.checkBox_XF.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.checkBox_XF.AutoSize = true;
            this.checkBox_XF.BackColor = SystemColors.ButtonHighlight;
            this.checkBox_XF.Location = new Point(0x2ba, 0x93);
            this.checkBox_XF.Name = "checkBox_XF";
            this.checkBox_XF.Size = new Size(0x60, 0x10);
            this.checkBox_XF.TabIndex = 4;
            this.checkBox_XF.Text = "农产品销售方";
            this.checkBox_XF.UseVisualStyleBackColor = false;
            this.checkBox_GF.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.checkBox_GF.AutoSize = true;
            this.checkBox_GF.BackColor = SystemColors.ButtonHighlight;
            this.checkBox_GF.Location = new Point(0x2ba, 0xac);
            this.checkBox_GF.Name = "checkBox_GF";
            this.checkBox_GF.Size = new Size(0x60, 0x10);
            this.checkBox_GF.TabIndex = 5;
            this.checkBox_GF.Text = "农产品收购方";
            this.checkBox_GF.UseVisualStyleBackColor = false;
            this.textBoxDJH.Cursor = Cursors.IBeam;
            this.textBoxDJH.ForeColor = SystemColors.WindowText;
            this.textBoxDJH.Location = new Point(0x129, 0x56);
            this.textBoxDJH.Name = "textBoxDJH";
            this.textBoxDJH.Size = new Size(0xab, 0x15);
            this.textBoxDJH.TabIndex = 1;
            this.labelDJH.AutoSize = true;
            this.labelDJH.BackColor = SystemColors.ButtonHighlight;
            this.labelDJH.Location = new Point(0xf8, 90);
            this.labelDJH.Name = "labelDJH";
            this.labelDJH.Size = new Size(0x29, 12);
            this.labelDJH.TabIndex = 0xd0;
            this.labelDJH.Text = "单据号";
            this.comboBoxDJZL.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxDJZL.FormattingEnabled = true;
            this.comboBoxDJZL.Location = new Point(0x81, 0x56);
            this.comboBoxDJZL.Name = "comboBoxDJZL";
            this.comboBoxDJZL.Size = new Size(0x63, 20);
            this.comboBoxDJZL.TabIndex = 0;
            this.comboBoxDJZL.SelectionChangeCommitted += new EventHandler(this.comboBoxDJZL_SelectionChangeCommitted);
            this.labelDJZL.AutoSize = true;
            this.labelDJZL.BackColor = SystemColors.ButtonHighlight;
            this.labelDJZL.Location = new Point(0x41, 90);
            this.labelDJZL.Name = "labelDJZL";
            this.labelDJZL.Size = new Size(0x35, 12);
            this.labelDJZL.TabIndex = 0xce;
            this.labelDJZL.Text = "单据种类";
            this.labelQD.AutoSize = true;
            this.labelQD.BackColor = SystemColors.ButtonHighlight;
            this.labelQD.Location = new Point(0x1e4, 90);
            this.labelQD.Name = "labelQD";
            this.labelQD.Size = new Size(0x59, 12);
            this.labelQD.TabIndex = 0xcd;
            this.labelQD.Text = "清单行商品名称";
            this.toolbtnBuQi.AutoSize = false;
            this.toolbtnBuQi.Items.AddRange(new ToolStripItem[] { this.toolStripBtnCancel, this.toolStripBtnSave, this.toolStripButtonAdd, this.toolStripButtonDel, this.toolStripButtonZK, this.toolStripButtonKH, this.toolStripBtnHSJG, this.toolBtnCheck, this.toolStripButtonCE });
            this.toolbtnBuQi.Location = new Point(0, 0);
            this.toolbtnBuQi.Name = "toolbtnBuQi";
            this.toolbtnBuQi.Size = new Size(0x408, 0x19);
            this.toolbtnBuQi.TabIndex = 0xcb;
            this.toolbtnBuQi.Text = "toolStrip1";
            this.toolStripBtnCancel.Image = Resources.退出;
            this.toolStripBtnCancel.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripBtnCancel.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnCancel.Name = "toolStripBtnCancel";
            this.toolStripBtnCancel.Size = new Size(0x34, 0x16);
            this.toolStripBtnCancel.Text = "退出";
            this.toolStripBtnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.toolStripBtnSave.Image = Resources.修改;
            this.toolStripBtnSave.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripBtnSave.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnSave.Name = "toolStripBtnSave";
            this.toolStripBtnSave.Size = new Size(0x34, 0x16);
            this.toolStripBtnSave.Text = "保存";
            this.toolStripBtnSave.Click += new EventHandler(this.btnSave_Click);
            this.toolStripButtonAdd.Image = Resources.加入;
            this.toolStripButtonAdd.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripButtonAdd.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new Size(0x34, 0x16);
            this.toolStripButtonAdd.Text = "增加";
            this.toolStripButtonAdd.Click += new EventHandler(this.btnAdd_Click);
            this.toolStripButtonDel.Image = Resources.减;
            this.toolStripButtonDel.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripButtonDel.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonDel.Name = "toolStripButtonDel";
            this.toolStripButtonDel.Size = new Size(0x34, 0x16);
            this.toolStripButtonDel.Text = "删除";
            this.toolStripButtonDel.Click += new EventHandler(this.btnDel_Click);
            this.toolStripButtonZK.Image = Resources.折扣;
            this.toolStripButtonZK.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripButtonZK.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonZK.Name = "toolStripButtonZK";
            this.toolStripButtonZK.Size = new Size(0x4c, 0x16);
            this.toolStripButtonZK.Text = "添加折扣";
            this.toolStripButtonZK.Click += new EventHandler(this.btnZK_Click);
            this.toolStripButtonKH.Image = Resources.客户;
            this.toolStripButtonKH.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripButtonKH.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonKH.Name = "toolStripButtonKH";
            this.toolStripButtonKH.Size = new Size(0x4c, 0x16);
            this.toolStripButtonKH.Text = "添加客户";
            this.toolStripButtonKH.Click += new EventHandler(this.btnAddKH_Click);
            this.toolStripBtnHSJG.Image = Resources.含税;
            this.toolStripBtnHSJG.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripBtnHSJG.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnHSJG.Name = "toolStripBtnHSJG";
            this.toolStripBtnHSJG.Size = new Size(0x4c, 0x16);
            this.toolStripBtnHSJG.Text = "含税价格";
            this.toolStripBtnHSJG.CheckStateChanged += new EventHandler(this.toolStripBtnHSJG_CheckStateChanged);
            this.toolBtnCheck.Image = Resources.修改;
            this.toolBtnCheck.ImageScaling = ToolStripItemImageScaling.None;
            this.toolBtnCheck.ImageTransparentColor = Color.Magenta;
            this.toolBtnCheck.Name = "toolBtnCheck";
            this.toolBtnCheck.Size = new Size(0x34, 0x16);
            this.toolBtnCheck.Text = "检查";
            this.toolBtnCheck.Click += new EventHandler(this.btnCheck_Click);
            this.toolStripButtonCE.Image = Resources.zhekou_03;
            this.toolStripButtonCE.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonCE.Name = "toolStripButtonCE";
            this.toolStripButtonCE.Size = new Size(0x34, 0x16);
            this.toolStripButtonCE.Text = "差额";
            this.toolStripButtonCE.Visible = false;
            this.lab_hj_xx.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.lab_hj_xx.AutoSize = true;
            this.lab_hj_xx.BackColor = SystemColors.ControlLightLight;
            this.lab_hj_xx.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lab_hj_xx.Location = new Point(0x2eb, 0x1d3);
            this.lab_hj_xx.Name = "lab_hj_xx";
            this.lab_hj_xx.Size = new Size(11, 12);
            this.lab_hj_xx.TabIndex = 250;
            this.lab_hj_xx.Text = "0";
            this.label2.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.BackColor = SystemColors.ControlLightLight;
            this.label2.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.Location = new Point(0x298, 0x1d1);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x33, 15);
            this.label2.TabIndex = 0xf9;
            this.label2.Text = "(小写)：";
            this.com_skr.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.com_skr.set_AutoComplate(0);
            this.com_skr.set_AutoIndex(1);
            this.com_skr.set_BorderColor(SystemColors.Control);
            this.com_skr.set_BorderStyle(1);
            this.com_skr.set_ButtonAutoHide(true);
            this.com_skr.set_buttonStyle(1);
            this.com_skr.set_DataSource(null);
            this.com_skr.set_DrawHead(false);
            this.com_skr.set_Edit(1);
            this.com_skr.set_IsSelectAll(false);
            this.com_skr.Location = new Point(0x7e, 0x24d);
            this.com_skr.set_MaxIndex(8);
            this.com_skr.set_MaxLength(0x7fff);
            this.com_skr.Name = "com_skr";
            this.com_skr.set_ReadOnly(false);
            this.com_skr.set_SelectedIndex(-1);
            this.com_skr.set_SelectionStart(0);
            this.com_skr.set_ShowText("");
            this.com_skr.Size = new Size(0x45, 0x15);
            this.com_skr.TabIndex = 15;
            this.com_skr.set_UnderLineColor(Color.Transparent);
            this.com_skr.set_UnderLineStyle(0);
            this.com_fhr.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.com_fhr.set_AutoComplate(0);
            this.com_fhr.set_AutoIndex(1);
            this.com_fhr.set_BorderColor(SystemColors.Control);
            this.com_fhr.set_BorderStyle(1);
            this.com_fhr.set_ButtonAutoHide(true);
            this.com_fhr.set_buttonStyle(1);
            this.com_fhr.set_DataSource(null);
            this.com_fhr.set_DrawHead(false);
            this.com_fhr.set_Edit(1);
            this.com_fhr.set_IsSelectAll(false);
            this.com_fhr.Location = new Point(0x15b, 0x24d);
            this.com_fhr.set_MaxIndex(8);
            this.com_fhr.set_MaxLength(0x7fff);
            this.com_fhr.Name = "com_fhr";
            this.com_fhr.set_ReadOnly(false);
            this.com_fhr.set_SelectedIndex(-1);
            this.com_fhr.set_SelectionStart(0);
            this.com_fhr.set_ShowText("");
            this.com_fhr.Size = new Size(0x45, 0x15);
            this.com_fhr.TabIndex = 0x10;
            this.com_fhr.set_UnderLineColor(Color.Transparent);
            this.com_fhr.set_UnderLineStyle(0);
            this.com_gfzh.set_AutoComplate(0);
            this.com_gfzh.set_AutoIndex(1);
            this.com_gfzh.set_BorderColor(SystemColors.Control);
            this.com_gfzh.set_BorderStyle(1);
            this.com_gfzh.set_ButtonAutoHide(true);
            this.com_gfzh.set_buttonStyle(1);
            this.com_gfzh.set_DataSource(null);
            this.com_gfzh.set_DrawHead(true);
            this.com_gfzh.set_Edit(1);
            this.com_gfzh.set_IsSelectAll(false);
            this.com_gfzh.Location = new Point(0xc5, 0xba);
            this.com_gfzh.set_MaxIndex(8);
            this.com_gfzh.set_MaxLength(0x7fff);
            this.com_gfzh.Name = "com_gfzh";
            this.com_gfzh.set_ReadOnly(false);
            this.com_gfzh.set_SelectedIndex(-1);
            this.com_gfzh.set_SelectionStart(0);
            this.com_gfzh.set_ShowText("");
            this.com_gfzh.Size = new Size(0x160, 0x15);
            this.com_gfzh.TabIndex = 9;
            this.com_gfzh.set_UnderLineColor(Color.Transparent);
            this.com_gfzh.set_UnderLineStyle(1);
            this.com_gfsbh.set_AutoComplate(0);
            this.com_gfsbh.set_AutoIndex(1);
            this.com_gfsbh.set_BorderColor(SystemColors.Control);
            this.com_gfsbh.set_BorderStyle(1);
            this.com_gfsbh.set_ButtonAutoHide(true);
            this.com_gfsbh.set_buttonStyle(1);
            this.com_gfsbh.set_DataSource(null);
            this.com_gfsbh.set_DrawHead(true);
            this.com_gfsbh.set_Edit(1);
            this.com_gfsbh.set_IsSelectAll(false);
            this.com_gfsbh.Location = new Point(0xc5, 140);
            this.com_gfsbh.set_MaxIndex(8);
            this.com_gfsbh.set_MaxLength(0x7fff);
            this.com_gfsbh.Name = "com_gfsbh";
            this.com_gfsbh.set_ReadOnly(false);
            this.com_gfsbh.set_SelectedIndex(-1);
            this.com_gfsbh.set_SelectionStart(0);
            this.com_gfsbh.set_ShowText("");
            this.com_gfsbh.Size = new Size(0x160, 0x15);
            this.com_gfsbh.TabIndex = 7;
            this.com_gfsbh.set_UnderLineColor(Color.Transparent);
            this.com_gfsbh.set_UnderLineStyle(1);
            this.com_xfyhzh.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.com_xfyhzh.set_AutoComplate(0);
            this.com_xfyhzh.set_AutoIndex(1);
            this.com_xfyhzh.set_BorderColor(SystemColors.Control);
            this.com_xfyhzh.set_BorderStyle(1);
            this.com_xfyhzh.set_ButtonAutoHide(true);
            this.com_xfyhzh.set_buttonStyle(1);
            this.com_xfyhzh.set_DataSource(null);
            this.com_xfyhzh.set_DrawHead(false);
            this.com_xfyhzh.set_Edit(1);
            this.com_xfyhzh.set_IsSelectAll(false);
            this.com_xfyhzh.Location = new Point(0xbc, 0x22e);
            this.com_xfyhzh.set_MaxIndex(8);
            this.com_xfyhzh.set_MaxLength(0x7fff);
            this.com_xfyhzh.Name = "com_xfyhzh";
            this.com_xfyhzh.set_ReadOnly(false);
            this.com_xfyhzh.set_SelectedIndex(-1);
            this.com_xfyhzh.set_SelectionStart(0);
            this.com_xfyhzh.set_ShowText("");
            this.com_xfyhzh.Size = new Size(0x169, 0x15);
            this.com_xfyhzh.TabIndex = 14;
            this.com_xfyhzh.set_UnderLineColor(Color.Transparent);
            this.com_xfyhzh.set_UnderLineStyle(1);
            this.com_xfdzdh.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.com_xfdzdh.set_AutoComplate(0);
            this.com_xfdzdh.set_AutoIndex(1);
            this.com_xfdzdh.set_BorderColor(SystemColors.Control);
            this.com_xfdzdh.set_BorderStyle(1);
            this.com_xfdzdh.set_ButtonAutoHide(true);
            this.com_xfdzdh.set_buttonStyle(1);
            this.com_xfdzdh.set_DataSource(null);
            this.com_xfdzdh.set_DrawHead(false);
            this.com_xfdzdh.set_Edit(1);
            this.com_xfdzdh.set_IsSelectAll(false);
            this.com_xfdzdh.Location = new Point(0xbc, 0x217);
            this.com_xfdzdh.set_MaxIndex(8);
            this.com_xfdzdh.set_MaxLength(0x7fff);
            this.com_xfdzdh.Name = "com_xfdzdh";
            this.com_xfdzdh.set_ReadOnly(false);
            this.com_xfdzdh.set_SelectedIndex(-1);
            this.com_xfdzdh.set_SelectionStart(0);
            this.com_xfdzdh.set_ShowText("");
            this.com_xfdzdh.Size = new Size(0x169, 0x15);
            this.com_xfdzdh.TabIndex = 13;
            this.com_xfdzdh.set_UnderLineColor(Color.Transparent);
            this.com_xfdzdh.set_UnderLineStyle(1);
            this.com_xfsbh.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.com_xfsbh.set_AutoComplate(0);
            this.com_xfsbh.set_AutoIndex(1);
            this.com_xfsbh.set_BorderColor(SystemColors.Control);
            this.com_xfsbh.set_BorderStyle(1);
            this.com_xfsbh.set_ButtonAutoHide(true);
            this.com_xfsbh.set_buttonStyle(1);
            this.com_xfsbh.set_DataSource(null);
            this.com_xfsbh.set_DrawHead(true);
            this.com_xfsbh.set_Edit(1);
            this.com_xfsbh.set_IsSelectAll(false);
            this.com_xfsbh.Location = new Point(0xbc, 0x200);
            this.com_xfsbh.set_MaxIndex(8);
            this.com_xfsbh.set_MaxLength(0x7fff);
            this.com_xfsbh.Name = "com_xfsbh";
            this.com_xfsbh.set_ReadOnly(false);
            this.com_xfsbh.set_SelectedIndex(-1);
            this.com_xfsbh.set_SelectionStart(0);
            this.com_xfsbh.set_ShowText("");
            this.com_xfsbh.Size = new Size(0x169, 0x15);
            this.com_xfsbh.TabIndex = 12;
            this.com_xfsbh.set_UnderLineColor(Color.Transparent);
            this.com_xfsbh.set_UnderLineStyle(1);
            this.com_xfmc.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.com_xfmc.set_AutoComplate(0);
            this.com_xfmc.set_AutoIndex(1);
            this.com_xfmc.set_BorderColor(SystemColors.Control);
            this.com_xfmc.set_BorderStyle(1);
            this.com_xfmc.set_ButtonAutoHide(true);
            this.com_xfmc.set_buttonStyle(1);
            this.com_xfmc.set_DataSource(null);
            this.com_xfmc.set_DrawHead(true);
            this.com_xfmc.set_Edit(1);
            this.com_xfmc.set_IsSelectAll(false);
            this.com_xfmc.Location = new Point(0xbc, 0x1e9);
            this.com_xfmc.set_MaxIndex(8);
            this.com_xfmc.set_MaxLength(0x7fff);
            this.com_xfmc.Name = "com_xfmc";
            this.com_xfmc.set_ReadOnly(false);
            this.com_xfmc.set_SelectedIndex(-1);
            this.com_xfmc.set_SelectionStart(0);
            this.com_xfmc.set_ShowText("");
            this.com_xfmc.Size = new Size(0x169, 0x15);
            this.com_xfmc.TabIndex = 11;
            this.com_xfmc.set_UnderLineColor(Color.Transparent);
            this.com_xfmc.set_UnderLineStyle(1);
            this.com_gfdzdh.set_AutoComplate(0);
            this.com_gfdzdh.set_AutoIndex(1);
            this.com_gfdzdh.set_BorderColor(SystemColors.Control);
            this.com_gfdzdh.set_BorderStyle(1);
            this.com_gfdzdh.set_ButtonAutoHide(true);
            this.com_gfdzdh.set_buttonStyle(1);
            this.com_gfdzdh.set_DataSource(null);
            this.com_gfdzdh.set_DrawHead(true);
            this.com_gfdzdh.set_Edit(1);
            this.com_gfdzdh.set_IsSelectAll(false);
            this.com_gfdzdh.Location = new Point(0xc5, 0xa3);
            this.com_gfdzdh.set_MaxIndex(8);
            this.com_gfdzdh.set_MaxLength(0x7fff);
            this.com_gfdzdh.Name = "com_gfdzdh";
            this.com_gfdzdh.set_ReadOnly(false);
            this.com_gfdzdh.set_SelectedIndex(-1);
            this.com_gfdzdh.set_SelectionStart(0);
            this.com_gfdzdh.set_ShowText("");
            this.com_gfdzdh.Size = new Size(0x160, 0x15);
            this.com_gfdzdh.TabIndex = 8;
            this.com_gfdzdh.set_UnderLineColor(Color.Transparent);
            this.com_gfdzdh.set_UnderLineStyle(1);
            this.com_gfmc.set_AutoComplate(0);
            this.com_gfmc.set_AutoIndex(1);
            this.com_gfmc.set_BorderColor(SystemColors.Control);
            this.com_gfmc.set_BorderStyle(1);
            this.com_gfmc.set_ButtonAutoHide(true);
            this.com_gfmc.set_buttonStyle(1);
            this.com_gfmc.set_DataSource(null);
            this.com_gfmc.set_DrawHead(true);
            this.com_gfmc.set_Edit(1);
            this.com_gfmc.ForeColor = Color.Black;
            this.com_gfmc.set_IsSelectAll(false);
            this.com_gfmc.Location = new Point(0xc5, 0x75);
            this.com_gfmc.set_MaxIndex(8);
            this.com_gfmc.set_MaxLength(0x7fff);
            this.com_gfmc.Name = "com_gfmc";
            this.com_gfmc.set_ReadOnly(false);
            this.com_gfmc.set_SelectedIndex(-1);
            this.com_gfmc.set_SelectionStart(0);
            this.com_gfmc.set_ShowText("");
            this.com_gfmc.Size = new Size(0x160, 0x15);
            this.com_gfmc.TabIndex = 6;
            this.com_gfmc.set_UnderLineColor(Color.Transparent);
            this.com_gfmc.set_UnderLineStyle(1);
            this.com_gfmc.Click += new EventHandler(this.com_gfmc_Click);
            this.label17.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label17.AutoSize = true;
            this.label17.BackColor = SystemColors.ButtonHighlight;
            this.label17.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label17.Location = new Point(0x44, 0x1e9);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x13, 15);
            this.label17.TabIndex = 0xfb;
            this.label17.Text = "销";
            this.label31.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label31.AutoSize = true;
            this.label31.BackColor = SystemColors.ButtonHighlight;
            this.label31.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label31.Location = new Point(0x44, 0x22f);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x13, 15);
            this.label31.TabIndex = 0x100;
            this.label31.Text = "方";
            this.label32.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label32.AutoSize = true;
            this.label32.BackColor = SystemColors.ButtonHighlight;
            this.label32.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label32.Location = new Point(0x44, 0x20b);
            this.label32.Name = "label32";
            this.label32.Size = new Size(0x13, 15);
            this.label32.TabIndex = 0xfe;
            this.label32.Text = "售";
            this.label29.AutoSize = true;
            this.label29.BackColor = SystemColors.ButtonHighlight;
            this.label29.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label29.Location = new Point(0x42, 0xbd);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x13, 15);
            this.label29.TabIndex = 0xf2;
            this.label29.Text = "方";
            this.label23.AutoSize = true;
            this.label23.BackColor = SystemColors.ButtonHighlight;
            this.label23.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label23.Location = new Point(0x42, 0x99);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x13, 15);
            this.label23.TabIndex = 0xee;
            this.label23.Text = "买";
            this.label7.AutoSize = true;
            this.label7.BackColor = SystemColors.ButtonHighlight;
            this.label7.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label7.Location = new Point(0x42, 0x75);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x13, 15);
            this.label7.TabIndex = 0xeb;
            this.label7.Text = "购";
            this.lab_Tax.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.lab_Tax.AutoSize = true;
            this.lab_Tax.BackColor = SystemColors.ControlLightLight;
            this.lab_Tax.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lab_Tax.Location = new Point(490, 0x1bb);
            this.lab_Tax.Name = "lab_Tax";
            this.lab_Tax.Size = new Size(0x29, 12);
            this.lab_Tax.TabIndex = 0xf6;
            this.lab_Tax.Text = "税额：";
            this.lab_Amount.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.lab_Amount.AutoSize = true;
            this.lab_Amount.BackColor = SystemColors.ControlLightLight;
            this.lab_Amount.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lab_Amount.Location = new Point(0xf8, 0x1bb);
            this.lab_Amount.Name = "lab_Amount";
            this.lab_Amount.Size = new Size(0x29, 12);
            this.lab_Amount.TabIndex = 0xf5;
            this.lab_Amount.Text = "金额：";
            this.label12.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label12.AutoSize = true;
            this.label12.BackColor = SystemColors.ControlLightLight;
            this.label12.Font = new Font("Microsoft Sans Serif", 9f);
            this.label12.Location = new Point(0x5f, 0x1b9);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x31, 15);
            this.label12.TabIndex = 0xf4;
            this.label12.Text = "合      计";
            this.dgv_spmx.AllowUserToAddRows = false;
            this.dgv_spmx.AllowUserToOrderColumns = true;
            this.dgv_spmx.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.dgv_spmx.BackgroundColor = SystemColors.ButtonHighlight;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.BackColor = SystemColors.Control;
            style.Font = new Font("Microsoft Sans Serif", 9f);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.False;
            this.dgv_spmx.ColumnHeadersDefaultCellStyle = style;
            this.dgv_spmx.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_spmx.GridColor = Color.Gray;
            this.dgv_spmx.Location = new Point(0x42, 0xd9);
            this.dgv_spmx.Name = "dgv_spmx";
            this.dgv_spmx.ReadOnly = true;
            this.dgv_spmx.RowHeadersVisible = false;
            this.dgv_spmx.RowTemplate.Height = 0x17;
            this.dgv_spmx.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dgv_spmx.Size = new Size(0x393, 0xdf);
            this.dgv_spmx.TabIndex = 10;
            this.txt_bz.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.txt_bz.BorderStyle = BorderStyle.None;
            this.txt_bz.Location = new Point(0x24a, 0x1ef);
            this.txt_bz.Multiline = true;
            this.txt_bz.Name = "txt_bz";
            this.txt_bz.Size = new Size(0x146, 0x54);
            this.txt_bz.TabIndex = 0x11;
            this.lab_title.Anchor = AnchorStyles.Top;
            this.lab_title.AutoSize = true;
            this.lab_title.BackColor = SystemColors.ControlLightLight;
            this.lab_title.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Bold);
            this.lab_title.ForeColor = Color.Blue;
            this.lab_title.Location = new Point(0x1b9, 0x22);
            this.lab_title.Name = "lab_title";
            this.lab_title.Size = new Size(90, 0x18);
            this.lab_title.TabIndex = 0xe5;
            this.lab_title.Text = "销售单据";
            this.lab_hj_jshj_dx.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lab_hj_jshj_dx.AutoSize = true;
            this.lab_hj_jshj_dx.BackColor = SystemColors.ControlLightLight;
            this.lab_hj_jshj_dx.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lab_hj_jshj_dx.Location = new Point(0x15f, 0x1d1);
            this.lab_hj_jshj_dx.Name = "lab_hj_jshj_dx";
            this.lab_hj_jshj_dx.Size = new Size(0x11, 12);
            this.lab_hj_jshj_dx.TabIndex = 0xf8;
            this.lab_hj_jshj_dx.Text = "零";
            this.lab_bz.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.lab_bz.AutoSize = true;
            this.lab_bz.BackColor = SystemColors.ControlLightLight;
            this.lab_bz.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lab_bz.Location = new Point(0x22b, 0x1f5);
            this.lab_bz.Name = "lab_bz";
            this.lab_bz.Size = new Size(0x13, 90);
            this.lab_bz.TabIndex = 0xfd;
            this.lab_bz.Text = "备\r\n\r\n\r\n\r\n\r\n注";
            this.lab_KPZT.AutoSize = true;
            this.lab_KPZT.BackColor = SystemColors.ControlLightLight;
            this.lab_KPZT.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lab_KPZT.Location = new Point(0x44, 0x29);
            this.lab_KPZT.Name = "lab_KPZT";
            this.lab_KPZT.Size = new Size(0x39, 12);
            this.lab_KPZT.TabIndex = 0x106;
            this.lab_KPZT.Text = "开票状态";
            this.label27.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label27.AutoSize = true;
            this.label27.BackColor = SystemColors.ControlLightLight;
            this.label27.Font = new Font("Microsoft Sans Serif", 9f);
            this.label27.Location = new Point(0x126, 0x251);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0x2b, 15);
            this.label27.TabIndex = 0x105;
            this.label27.Text = "复核：";
            this.label26.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label26.AutoSize = true;
            this.label26.BackColor = SystemColors.ControlLightLight;
            this.label26.Font = new Font("Microsoft Sans Serif", 9f);
            this.label26.Location = new Point(0x44, 0x250);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x37, 15);
            this.label26.TabIndex = 260;
            this.label26.Text = "收款人：";
            this.label21.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label21.AutoSize = true;
            this.label21.BackColor = SystemColors.ControlLightLight;
            this.label21.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label21.Location = new Point(0x5f, 0x231);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x5b, 15);
            this.label21.TabIndex = 0x103;
            this.label21.Text = "开户行及账号：";
            this.label20.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label20.AutoSize = true;
            this.label20.BackColor = SystemColors.ControlLightLight;
            this.label20.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label20.Location = new Point(0x5f, 0x219);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x5b, 15);
            this.label20.TabIndex = 0x101;
            this.label20.Text = "地 址 、 电 话：";
            this.label19.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label19.AutoSize = true;
            this.label19.BackColor = SystemColors.ControlLightLight;
            this.label19.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label19.Location = new Point(0x5f, 0x201);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x5b, 15);
            this.label19.TabIndex = 0xff;
            this.label19.Text = "纳税人识别号：";
            this.label18.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label18.AutoSize = true;
            this.label18.BackColor = SystemColors.ControlLightLight;
            this.label18.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label18.Location = new Point(0x5f, 0x1e9);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x5b, 15);
            this.label18.TabIndex = 0xfc;
            this.label18.Text = "名                称：";
            this.label13.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label13.AutoSize = true;
            this.label13.BackColor = SystemColors.ControlLightLight;
            this.label13.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label13.Location = new Point(0x5b, 0x1cf);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x63, 15);
            this.label13.TabIndex = 0xf7;
            this.label13.Text = "价税合计(大写)：";
            this.label8.AutoSize = true;
            this.label8.BackColor = SystemColors.ControlLightLight;
            this.label8.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label8.Location = new Point(0x5f, 0x75);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x5b, 15);
            this.label8.TabIndex = 0xec;
            this.label8.Text = "名                称：";
            this.label9.AutoSize = true;
            this.label9.BackColor = SystemColors.ControlLightLight;
            this.label9.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label9.Location = new Point(0x5f, 0x8d);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x5b, 15);
            this.label9.TabIndex = 0xef;
            this.label9.Text = "纳税人识别号：";
            this.label10.AutoSize = true;
            this.label10.BackColor = SystemColors.ControlLightLight;
            this.label10.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label10.Location = new Point(0x5f, 0xa5);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x5b, 15);
            this.label10.TabIndex = 0xf1;
            this.label10.Text = "地 址 、 电 话：";
            this.label11.AutoSize = true;
            this.label11.BackColor = SystemColors.ControlLightLight;
            this.label11.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label11.Location = new Point(0x5f, 0xbd);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x5b, 15);
            this.label11.TabIndex = 0xf3;
            this.label11.Text = "开户行及账号：";
            this.lab_DJZT.AutoSize = true;
            this.lab_DJZT.BackColor = SystemColors.ControlLightLight;
            this.lab_DJZT.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lab_DJZT.Location = new Point(0x44, 0x3e);
            this.lab_DJZT.Name = "lab_DJZT";
            this.lab_DJZT.Size = new Size(0x39, 12);
            this.lab_DJZT.TabIndex = 0x13;
            this.lab_DJZT.Text = "单据状态";
            this.button1.Location = new Point(600, 0x29);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0x107;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.aisinoLBL_RowInfo.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.aisinoLBL_RowInfo.AutoSize = true;
            this.aisinoLBL_RowInfo.BackColor = SystemColors.ControlLightLight;
            this.aisinoLBL_RowInfo.Font = new Font("Microsoft Sans Serif", 9f);
            this.aisinoLBL_RowInfo.Location = new Point(0x341, 0x1bb);
            this.aisinoLBL_RowInfo.Name = "aisinoLBL_RowInfo";
            this.aisinoLBL_RowInfo.Size = new Size(90, 15);
            this.aisinoLBL_RowInfo.TabIndex = 0x108;
            this.aisinoLBL_RowInfo.Text = "共 0 行 / 第 0 行";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.ClientSize = new Size(0x408, 0x269);
            base.Controls.Add(this.aisinoLBL_RowInfo);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.lab_hj_xx);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.com_skr);
            base.Controls.Add(this.com_fhr);
            base.Controls.Add(this.com_gfzh);
            base.Controls.Add(this.com_gfsbh);
            base.Controls.Add(this.com_xfyhzh);
            base.Controls.Add(this.com_xfdzdh);
            base.Controls.Add(this.com_xfsbh);
            base.Controls.Add(this.com_xfmc);
            base.Controls.Add(this.com_gfdzdh);
            base.Controls.Add(this.com_gfmc);
            base.Controls.Add(this.label17);
            base.Controls.Add(this.label31);
            base.Controls.Add(this.label32);
            base.Controls.Add(this.label29);
            base.Controls.Add(this.label23);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.lab_Tax);
            base.Controls.Add(this.lab_Amount);
            base.Controls.Add(this.label12);
            base.Controls.Add(this.dgv_spmx);
            base.Controls.Add(this.txt_bz);
            base.Controls.Add(this.lab_title);
            base.Controls.Add(this.lab_DJZT);
            base.Controls.Add(this.lab_hj_jshj_dx);
            base.Controls.Add(this.lab_bz);
            base.Controls.Add(this.lab_KPZT);
            base.Controls.Add(this.label27);
            base.Controls.Add(this.label26);
            base.Controls.Add(this.label21);
            base.Controls.Add(this.label20);
            base.Controls.Add(this.label19);
            base.Controls.Add(this.label18);
            base.Controls.Add(this.label13);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.dateTimePicker1);
            base.Controls.Add(this.labelDJRQ);
            base.Controls.Add(this.textBoxQDSPHMX);
            base.Controls.Add(this.checkBox_HYSY);
            base.Controls.Add(this.checkBox_SFZJY);
            base.Controls.Add(this.checkBox_XF);
            base.Controls.Add(this.checkBox_GF);
            base.Controls.Add(this.textBoxDJH);
            base.Controls.Add(this.labelDJH);
            base.Controls.Add(this.comboBoxDJZL);
            base.Controls.Add(this.labelDJZL);
            base.Controls.Add(this.labelQD);
            base.Controls.Add(this.toolbtnBuQi);
            this.ForeColor = SystemColors.ControlText;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "XSDJEdite";
            this.Text = "XSDJEdite";
            base.Load += new EventHandler(this.XSDJEdite_Load);
            this.toolbtnBuQi.ResumeLayout(false);
            this.toolbtnBuQi.PerformLayout();
            ((ISupportInitialize) this.dgv_spmx).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void initInvoice()
        {
            byte[] buffer = null;
            this.InvoiceKP = new Invoice(false, false, false, 2, buffer, null);
        }

        private string LoadXSDJ(XSDJMXModel xsdj)
        {
            string str2;
            try
            {
                string str = "OK";
                int num = 1;
                for (int i = 0; i < this.dgv_spmx.Rows.Count; i++)
                {
                    DataGridViewRow row = this.dgv_spmx.Rows[i];
                    XSDJ_MXModel tag = (XSDJ_MXModel) row.Tag;
                    if ((row.Cells["SPMC"].Value == null) || (row.Cells["SPMC"].Value.ToString().Trim().Length == 0))
                    {
                        return string.Format("第{0}行 商品名称不能为空", i + 1);
                    }
                    tag.SPMC = row.Cells["SPMC"].Value.ToString();
                    tag.DJHXZ = Convert.ToInt16(row.Cells["DJHXZ"].Value);
                    if (tag.DJHXZ != 4)
                    {
                        if (!CommonTool.RegexMatchNum(row.Cells["SL"].Value))
                        {
                            return string.Format("第{0}行 数量不正确", i + 1);
                        }
                        if (!CommonTool.RegexMatchNum(row.Cells["DJ"].Value))
                        {
                            return string.Format("第{0}行 单价不正确", i + 1);
                        }
                    }
                    if (!CommonTool.RegexMatchNum(row.Cells["JE"].Value))
                    {
                        return string.Format("第{0}行 金额不正确", i + 1);
                    }
                    if (!CommonTool.RegexMatchNum(row.Cells["SE"].Value))
                    {
                        return string.Format("第{0}行 税额不正确", i + 1);
                    }
                    if (!CommonTool.RegexMatchNum(this.ToPerdouble(row.Cells["SLV"].Value)))
                    {
                        return string.Format("第{0}行 税率不正确", i + 1);
                    }
                    if (tag.SL == 0.0)
                    {
                        if ((row.Cells["SL"].Value == null) || row.Cells["SL"].Value.Equals(""))
                        {
                            tag.SL = 0.0;
                        }
                        else
                        {
                            tag.SL = Convert.ToDouble(row.Cells["SL"].Value);
                        }
                    }
                    if (tag.SLV == 0.0)
                    {
                        if (row.Cells["SLV"].Value == null)
                        {
                            row.Cells["SLV"].Value = 0.17;
                        }
                        if (row.Cells["SLV"].Value.ToString().Contains("%"))
                        {
                            tag.SLV = Convert.ToDouble(row.Cells["SLV"].Value.ToString().Replace("%", "")) / 100.0;
                        }
                        else
                        {
                            tag.SLV = Convert.ToDouble(row.Cells["SLV"].Value);
                        }
                    }
                    if (this.IsHYSY && (tag.SLV == 0.05))
                    {
                        tag.HSJBZ = true;
                    }
                    else
                    {
                        tag.HSJBZ = this.ContainTax;
                    }
                    if (tag.DJ == 0.0)
                    {
                        if (row.Cells["DJ"].Value == null)
                        {
                            tag.DJ = 0.0;
                        }
                        else
                        {
                            tag.DJ = row.Cells["DJ"].Value.Equals("") ? 0.0 : Convert.ToDouble(row.Cells["DJ"].Value);
                        }
                    }
                    if (tag.HSJBZ)
                    {
                        if (!(tag.SL == 0.0))
                        {
                            tag.DJ = (tag.JE + tag.SE) / tag.SL;
                        }
                        else
                        {
                            tag.DJ = tag.JE + tag.SE;
                        }
                    }
                    if (tag.JE == 0.0)
                    {
                        tag.JE = Convert.ToDouble(row.Cells["JE"].Value);
                    }
                    if (tag.SE == 0.0)
                    {
                        tag.SE = Convert.ToDouble(row.Cells["SE"].Value);
                    }
                    tag.SPSM = (row.Cells["SPSM"].Value == null) ? "0" : row.Cells["SPSM"].Value.ToString();
                    tag.JLDW = (row.Cells["JLDW"].Value == null) ? "" : row.Cells["JLDW"].Value.ToString();
                    tag.GGXH = (row.Cells["GGXH"].Value == null) ? "" : row.Cells["GGXH"].Value.ToString();
                    tag.XSDJBH = this.textBoxDJH.Text.Trim();
                    tag.XH = num;
                    num++;
                    xsdj.ListXSDJ_MX.Add(tag);
                    xsdj.JEHJ += tag.JE;
                }
                xsdj.BH = this.textBoxDJH.Text.Trim();
                xsdj.BZ = this.txt_bz.Text.Trim();
                xsdj.FHR = this.com_fhr.Text.Trim();
                xsdj.GFDZDH = this.com_gfdzdh.Text.Trim();
                xsdj.GFMC = this.com_gfmc.Text.Trim();
                xsdj.GFSH = this.com_gfsbh.Text.Trim();
                xsdj.GFYHZH = this.com_gfzh.Text.Trim();
                xsdj.QDHSPMC = this.textBoxQDSPHMX.Text.Trim();
                xsdj.SKR = this.com_skr.Text.Trim();
                xsdj.XFDZDH = this.com_xfdzdh.Text.Trim();
                xsdj.XFYHZH = this.com_xfyhzh.Text.Trim();
                xsdj.DJRQ = Convert.ToDateTime(this.dateTimePicker1.Text);
                xsdj.DJYF = xsdj.DJRQ.Month;
                xsdj.DJZL = this.comboBoxDJZL.SelectedValue.ToString();
                xsdj.XSBM = "";
                xsdj.SFZJY = this.checkBox_SFZJY.Checked;
                xsdj.HYSY = this.checkBox_HYSY.Checked;
                str2 = str;
            }
            catch (Exception)
            {
                throw;
            }
            return str2;
        }

        private PointF NewPointF(PointF orient, float X, float Y)
        {
            return new PointF(orient.X + X, orient.Y + Y);
        }

        private void obj_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
        }

        private void obj_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                object obj2;
                string str = (e.RowIndex + 1).ToString();
                if (this.dgv_spmx.Rows[e.RowIndex].Cells["DJHXZ"].Value.Equals(0))
                {
                    string name = this.dgv_spmx.Columns[e.ColumnIndex].Name;
                    obj2 = this.dgv_spmx.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string str3 = name;
                    if (str3 != null)
                    {
                        if (!(str3 == "JE"))
                        {
                            if (str3 == "SE")
                            {
                                goto Label_013E;
                            }
                            if (!(str3 == "SPSM"))
                            {
                                if (str3 == "SL")
                                {
                                    goto Label_0185;
                                }
                                if (str3 == "DJ")
                                {
                                    goto Label_0220;
                                }
                            }
                        }
                        else if (CommonTool.RegexMatchNum(obj2))
                        {
                            this.NumError = false;
                        }
                        else
                        {
                            this.NumError = true;
                            MessageManager.ShowMsgBox("A115", new string[] { str });
                        }
                    }
                }
                return;
            Label_013E:
                if (CommonTool.RegexMatchNum(obj2))
                {
                    this.NumError = false;
                }
                else
                {
                    this.NumError = true;
                    MessageManager.ShowMsgBox("A117", new string[] { str });
                }
                return;
            Label_0185:
                if (CommonTool.RegexMatchNum(obj2))
                {
                    this.NumError = false;
                }
                else if (obj2.ToString().Trim().Length == 0)
                {
                    this.NumError = false;
                    this.dgv_spmx.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
                else
                {
                    MessageManager.ShowMsgBox("A118", new string[] { str });
                    this.NumError = true;
                }
                return;
            Label_0220:
                if (CommonTool.RegexMatchNum(obj2))
                {
                    if (Convert.ToDouble(obj2) < 0.0)
                    {
                        MessageManager.ShowMsgBox("A112", new string[] { str });
                        this.NumError = true;
                    }
                    else
                    {
                        this.NumError = false;
                    }
                }
                else if (obj2.ToString().Trim().Length == 0)
                {
                    this.NumError = false;
                    this.dgv_spmx.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
                else
                {
                    MessageManager.ShowMsgBox("A114", new string[] { str });
                    this.NumError = true;
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.W = ((float) base.Width) / 100f;
            this.H = ((float) (base.Height - 50)) / 100f;
            Graphics g = e.Graphics;
            this.DrawLines(g);
            this.BackColor = Color.White;
        }

        private void SelKhxx(string value, int type)
        {
            object[] khxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKH", new object[] { value, type, "MC,SH,DZDH,YHZH" });
            if (khxx != null)
            {
                this.SetGfxx(khxx);
            }
        }

        private void SelSpxx(DataGridView parent, int type, int showDisableSLv)
        {
            int rowIndex = parent.CurrentCell.RowIndex;
            object obj2 = parent.Rows[rowIndex].Cells["SPMC"].Value;
            string str = this.spmcBt.Text.Trim();
            this.selectSlv = -1.0;
            string str2 = "";
            if ((this.bill.DJZL == "s") && this.bill.HYSY)
            {
                str2 = "HYSY";
            }
            else
            {
                str2 = "Except_HYSY";
            }
            TaxCardValue value2 = new TaxCardValue();
            if (value2.IsXTCorpAgent)
            {
                str2 = "";
            }
            object[] objArray = new object[] { str, this.selectSlv, type, showDisableSLv, "", str2 };
            object[] sp = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSP", objArray);
            if (sp != null)
            {
                if (CommonTool.isSPBMVersion())
                {
                    if (sp.Length > 11)
                    {
                        string str3 = sp[0].ToString().Trim();
                        string str4 = sp[1].ToString().Trim();
                        if (sp[11].ToString().Trim() == "")
                        {
                            objArray = new object[] { str4, "BM,MC,JM,SLV,SPSM,GGXH,JLDW,DJ,HSJBZ,XTHASH,HYSY,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC", str3, true };
                            sp = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray);
                            if (sp != null)
                            {
                                this.SetSpxx(sp);
                            }
                            else
                            {
                                MessageBoxHelper.Show("商品分类编码需要补全！");
                            }
                        }
                        else
                        {
                            this.SetSpxx(sp);
                        }
                    }
                }
                else
                {
                    this.SetSpxx(sp);
                }
            }
        }

        private void SetDataGridPropEven(DataGridView obj)
        {
            obj.Columns["SLV"].ReadOnly = true;
            obj.Columns["SPMC"].ReadOnly = true;
            obj.StandardTab = false;
            obj.PreviewKeyDown += new PreviewKeyDownEventHandler(this.dataGridMX1_PreviewKeyDown);
            obj.KeyDown += new KeyEventHandler(this.dataGridMX1_KeyDown);
            obj.Leave += new EventHandler(this.dataGridMX1_Leave);
            obj.CellEndEdit -= new DataGridViewCellEventHandler(this.dataGridMX1_CellEndEdit);
            obj.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridMX1_CellEndEdit);
            obj.CellValueChanged -= new DataGridViewCellEventHandler(this.dataGridMX1_CellValueChanged);
            obj.CellValueChanged += new DataGridViewCellEventHandler(this.dataGridMX1_CellValueChanged);
            obj.CellClick += new DataGridViewCellEventHandler(this.dataGridMX1_CellClick);
            obj.CellStateChanged += new DataGridViewCellStateChangedEventHandler(this.obj_CellStateChanged);
            obj.CurrentCellChanged += new EventHandler(this.dataGridMX1_CurrentCellChanged);
            obj.CellValidating += new DataGridViewCellValidatingEventHandler(this.dataGridMX1_CellValidating);
            obj.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridMX1_CellFormatting);
            obj.Scroll += new ScrollEventHandler(this.dgv_spmx_Scroll);
            obj.KeyUp += new KeyEventHandler(this.dgv_spmx_KeyUp);
        }

        private string SetDkBz(string mc, string sh)
        {
            string str3;
            string str4;
            string text = this.txt_bz.Text;
            string dKInvNotes = NotesUtil.GetDKInvNotes(sh, mc);
            if (string.IsNullOrEmpty(dKInvNotes))
            {
                return text;
            }
            if (text.Trim() == "")
            {
                return dKInvNotes;
            }
            if (text.StartsWith(dKInvNotes))
            {
                return text;
            }
            if (NotesUtil.GetDKQYFromInvNotes(text, ref str3, ref str4).Equals("0000"))
            {
                string oldValue = NotesUtil.GetDKInvNotes(str3, str4);
                return text.Replace(oldValue, dKInvNotes);
            }
            return (dKInvNotes + "\r\n" + text);
        }

        private void SetGfxx(object[] khxx)
        {
            if (khxx.Length == 4)
            {
                string[] strArray;
                int num;
                if (!this.checkBox_GF.Checked)
                {
                    this.com_gfmc.Text = khxx[0].ToString();
                    this.com_gfsbh.Text = khxx[1].ToString();
                    this.com_gfzh.Text = "";
                    strArray = khxx[3].ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    DataTable table = new DataTable();
                    DataColumn column = new DataColumn("YHZH");
                    table.Columns.Add(column);
                    for (num = 0; num < strArray.Length; num++)
                    {
                        table.Rows.Add(new object[] { strArray[num] });
                    }
                    this.com_gfzh.set_DataSource(table);
                    this.com_gfzh.Text = (strArray.Length > 0) ? strArray[0] : "";
                    DataTable table2 = new DataTable();
                    DataColumn column2 = new DataColumn("DZDH");
                    table2.Columns.Add(column2);
                    this.com_gfdzdh.Text = "";
                    strArray = khxx[2].ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    table2.Rows.Clear();
                    for (num = 0; num < strArray.Length; num++)
                    {
                        table2.Rows.Add(new object[] { strArray[num] });
                    }
                    this.com_gfdzdh.set_DataSource(table2);
                    this.com_gfdzdh.Text = (strArray.Length > 0) ? strArray[0] : "";
                }
                else
                {
                    this.com_xfmc.Text = khxx[0].ToString();
                    this.com_xfsbh.Text = khxx[1].ToString();
                    this.com_xfyhzh.Text = "";
                    strArray = khxx[3].ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    DataTable table3 = new DataTable();
                    DataColumn column3 = new DataColumn("YHZH");
                    table3.Columns.Add(column3);
                    for (num = 0; num < strArray.Length; num++)
                    {
                        table3.Rows.Add(new object[] { strArray[num] });
                    }
                    this.com_xfyhzh.set_DataSource(table3);
                    this.com_xfyhzh.Text = (strArray.Length > 0) ? strArray[0] : "";
                    DataTable table4 = table3.Clone();
                    this.com_xfdzdh.Text = "";
                    strArray = khxx[2].ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    table4.Rows.Clear();
                    for (num = 0; num < strArray.Length; num++)
                    {
                        table4.Rows.Add(new object[] { strArray[num] });
                    }
                    this.com_xfdzdh.set_DataSource(table4);
                    this.com_xfdzdh.Text = (strArray.Length > 0) ? strArray[0] : "";
                }
            }
            this.ToModel(this.bill);
        }

        private void SetHSJGGridHead(bool HSJG)
        {
            if (this.IsHYSY)
            {
                this.dgv_spmx.Columns["DJ"].HeaderText = "单价";
                this.dgv_spmx.Columns["JE"].HeaderText = "金额";
            }
            else if (HSJG)
            {
                this.dgv_spmx.Columns["DJ"].HeaderText = "单价(含税)";
                this.dgv_spmx.Columns["JE"].HeaderText = "金额(含税)";
            }
            else
            {
                this.dgv_spmx.Columns["DJ"].HeaderText = "单价";
                this.dgv_spmx.Columns["JE"].HeaderText = "金额";
            }
        }

        private void SetRowBackColor(DataGridViewRow DRow, Color BackColor)
        {
            for (int i = 0; i < 10; i++)
            {
                DRow.Cells[i].Style.BackColor = BackColor;
            }
        }

        private void SetSkrAndFhr()
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Xtgl.UserRoleService", null);
            if (objArray != null)
            {
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("YH");
                table.Columns.Add(column);
                List<string> list = objArray[0] as List<string>;
                foreach (string str in list)
                {
                    table.Rows.Add(new object[] { str });
                }
                this.com_skr.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("用户", "YH", this.com_skr.Width));
                this.com_skr.set_DataSource(table);
                if (this.com_skr.get_DataSource().Rows.Count > 0)
                {
                    this.com_skr.set_SelectedIndex(0);
                }
                this.com_fhr.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("用户", "YH", this.com_fhr.Width));
                this.com_fhr.set_DataSource(table);
                if (this.com_fhr.get_DataSource().Rows.Count > 0)
                {
                    this.com_fhr.set_SelectedIndex(0);
                }
            }
        }

        private void SetSpxx(object[] sp)
        {
            try
            {
                try
                {
                    int rowIndex = this.dgv_spmx.CurrentCell.RowIndex;
                    if (CommonTool.isSPBMVersion() && (sp.Length >= 11))
                    {
                        string str = sp[11].ToString().Trim();
                        if (str == "")
                        {
                            MessageManager.ShowMsgBox("商品没有分类编码！");
                            this.spmcBt.Text = "";
                            return;
                        }
                        bool flag = new SaleBillDAL().isXT(sp[1].ToString());
                        object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", new object[] { str, true, flag });
                        if ((objArray != null) && !bool.Parse(objArray[0].ToString()))
                        {
                            MessageManager.ShowMsgBox("INP-242207", new string[] { "商品", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" });
                            this.spmcBt.Text = "";
                            return;
                        }
                    }
                    Goods good = this.bill.GetGood(rowIndex);
                    good.DJ = 0.0;
                    good.SL = 0.0;
                    good.JE = 0.0;
                    good.SLV = -1.0;
                    good.SE = 0.0;
                    good.KCE = 0.0;
                    if ((good != null) && (sp.Length >= 0x10))
                    {
                        this.SetSpxxSet = true;
                        good.SPMC = Convert.ToString(sp[1]);
                        if (this.checkBox_GF.Checked)
                        {
                            good.SLV = 0.0;
                        }
                        else
                        {
                            double result = -1.0;
                            double.TryParse(sp[3].ToString(), out result);
                            good.SLV = result;
                        }
                        good.GGXH = Convert.ToString(sp[5]);
                        good.JLDW = Convert.ToString(sp[6]);
                        good.DJ = Convert.ToDouble(sp[7]);
                        good.SPSM = Convert.ToString(sp[4]);
                        good.HSJBZ = Convert.ToBoolean(sp[8]);
                        if (CommonTool.isSPBMVersion())
                        {
                            good.SPBM = Convert.ToString(sp[0]);
                            good.XSYH = false;
                            good.XSYH = FatchSaleBill.ToValidateXSYH(sp[12].ToString().Trim());
                            good.XSYHSM = sp[13].ToString().Trim();
                            good.XSYHSM = sp[15].ToString().Trim();
                            good.FLBM = Convert.ToString(sp[11]);
                            string str2 = "出口零税";
                            string str3 = "免税";
                            string str4 = "不征税";
                            good.LSLVBS = "";
                            if (good.XSYH && Finacial.Equal(good.SLV, 0.0))
                            {
                                if (good.XSYHSM.Contains(str2))
                                {
                                    good.LSLVBS = "0";
                                }
                                else if (good.XSYHSM.Contains(str3))
                                {
                                    good.LSLVBS = "1";
                                }
                                else if (good.XSYHSM.Contains(str4))
                                {
                                    good.LSLVBS = "2";
                                }
                                else
                                {
                                    good.LSLVBS = "3";
                                }
                            }
                            else if (good.SLV == 0.0)
                            {
                                good.LSLVBS = "3";
                            }
                        }
                        if (CommonTool.isSPBMVersion() && (good.FLBM == ""))
                        {
                            MessageBox.Show("您选择的商品没有分类编码，请重新选择！");
                        }
                        else
                        {
                            this.saleBillBL.EditGoodsBaseYY(this.bill, rowIndex, "SL", "0");
                            this.spmcBt.Text = good.SPMC;
                            this.saleBillBL.CleanBill(this.bill);
                            this.ToView();
                        }
                    }
                }
                catch
                {
                }
            }
            finally
            {
            }
        }

        private void SetXfDzdh()
        {
            string str = base.TaxCardInstance.get_Address() + " " + base.TaxCardInstance.get_Telephone();
            if (str != null)
            {
                string[] strArray = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("DZDH");
                table.Columns.Add(column);
                for (int i = 0; i < strArray.Length; i++)
                {
                    table.Rows.Add(new object[] { strArray[i] });
                }
                this.com_xfdzdh.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("销方地址电话", "DZDH", this.com_xfdzdh.Width));
                this.com_xfdzdh.set_DataSource(table);
                if (this.com_xfdzdh.get_DataSource().Rows.Count > 0)
                {
                    this.com_xfdzdh.set_SelectedIndex(0);
                }
            }
        }

        private void SetXfyhzh()
        {
            string str = base.TaxCardInstance.get_BankAccount();
            if (str != null)
            {
                string[] strArray = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("YHZH");
                table.Columns.Add(column);
                for (int i = 0; i < strArray.Length; i++)
                {
                    table.Rows.Add(new object[] { strArray[i] });
                }
                this.com_xfyhzh.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("销方银行账号", "YHZH", this.com_xfyhzh.Width));
                this.com_xfyhzh.set_DataSource(table);
                if (this.com_xfyhzh.get_DataSource().Rows.Count > 0)
                {
                    this.com_xfyhzh.set_SelectedIndex(0);
                }
            }
        }

        private void SetYHZCMX(string comboSlv_Text, Goods mx)
        {
            string str = "出口零税";
            string str2 = "免税";
            string str3 = "不征税";
            mx.LSLVBS = "";
            if (comboSlv_Text.Contains(str))
            {
                mx.LSLVBS = "0";
                mx.XSYH = true;
            }
            else if (comboSlv_Text.Contains(str2))
            {
                mx.LSLVBS = "1";
                mx.XSYH = true;
            }
            else if (comboSlv_Text.Contains(str3))
            {
                mx.LSLVBS = "2";
                mx.XSYH = true;
            }
            else
            {
                mx.XSYH = false;
                string sLValue = PresentinvMng.GetSLValue(comboSlv_Text);
                double result = -1.0;
                double.TryParse(sLValue, out result);
                if (result == -1.0)
                {
                    mx.LSLVBS = "";
                }
                else if (result == 0.0)
                {
                    mx.LSLVBS = "3";
                }
                else
                {
                    mx.LSLVBS = "";
                }
            }
        }

        private void ShowCurrentSPRowNumInfo()
        {
            int num = 0;
            int count = 0;
            if (this.dgv_spmx.CurrentCell != null)
            {
                num = this.dgv_spmx.CurrentCell.RowIndex + 1;
            }
            if (this.dgv_spmx != null)
            {
                count = this.dgv_spmx.Rows.Count;
            }
            this.aisinoLBL_RowInfo.Text = "第 " + num.ToString() + " 行 / 共 " + count.ToString() + " 行";
        }

        private void spmcBt_Click(object sender, EventArgs e)
        {
            try
            {
                this.SelSpxx(this.dgv_spmx, 0, 0);
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void spmcBt_Leave(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (CommonTool.isSPBMVersion())
                    {
                        object[] objArray;
                        object[] objArray2;
                        string mC = this.spmcBt.Text.Trim();
                        switch (mC)
                        {
                            case "":
                            case "详见对应正数发票及清单":
                                return;
                        }
                        DataTable table = new SaleBillDAL().GET_SPXX_BY_NAME(mC, "s", "");
                        if (table.Rows.Count == 0)
                        {
                            objArray = new object[] { mC, "BM,MC,JM,SLV,SPSM,GGXH,JLDW,DJ,HSJBZ,XTHASH,HYSY,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC" };
                            objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray);
                            if (objArray2 != null)
                            {
                                if ((objArray2.Length >= 12) && (objArray2[11].ToString().Trim() == ""))
                                {
                                    MessageBoxHelper.Show("商品无分类编码！");
                                }
                                this.SetSpxx(objArray2);
                            }
                            else
                            {
                                MessageBoxHelper.Show("该商品不存在，必须增加！");
                            }
                        }
                        else
                        {
                            string fLBM;
                            int rowIndex;
                            string str4;
                            if (table.Rows.Count == 1)
                            {
                                fLBM = "";
                                rowIndex = this.dgv_spmx.CurrentCell.RowIndex;
                                if (rowIndex < this.bill.ListGoods.Count)
                                {
                                    fLBM = this.bill.ListGoods[rowIndex].FLBM;
                                }
                                if (fLBM == "")
                                {
                                    double num2 = -1.0;
                                    str4 = "";
                                    if (this.bill.HYSY && (this.bill.DJZL == "s"))
                                    {
                                        num2 = 0.05;
                                        str4 = "HYSY";
                                    }
                                    objArray = new object[] { mC, num2, 1, 0, "BM,MC,JM,SLV,SPSM,GGXH,JLDW,DJ,HSJBZ,XTHASH,HYSY,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC", str4 };
                                    objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSP", objArray);
                                    if (objArray2 != null)
                                    {
                                        if (objArray2.Length > 11)
                                        {
                                            string str5 = objArray2[0].ToString().Trim();
                                            string str2 = objArray2[11].ToString().Trim();
                                            str5 = table.Rows[0]["BM"].ToString().Trim();
                                            if (table.Rows[0]["SPFL"].ToString().Trim() == "")
                                            {
                                                objArray = new object[] { mC, "BM,MC,JM,SLV,SPSM,GGXH,JLDW,DJ,HSJBZ,XTHASH,HYSY,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC", str5, true };
                                                objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray);
                                                if (objArray2 != null)
                                                {
                                                    this.SetSpxx(objArray2);
                                                }
                                                else
                                                {
                                                    MessageBoxHelper.Show("商品分类编码需要补全！");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBoxHelper.Show("没有找到对应商品！");
                                    }
                                }
                            }
                            else if (table.Rows.Count > 1)
                            {
                                fLBM = "";
                                rowIndex = this.dgv_spmx.CurrentCell.RowIndex;
                                if (rowIndex < this.bill.ListGoods.Count)
                                {
                                    fLBM = this.bill.ListGoods[rowIndex].FLBM;
                                }
                                if (fLBM == "")
                                {
                                    str4 = "";
                                    if ((this.bill.DJZL == "s") && this.bill.HYSY)
                                    {
                                        str4 = "HYSY";
                                    }
                                    else
                                    {
                                        str4 = "Except_HYSY";
                                    }
                                    objArray = new object[] { mC, -1.0, 0, 0, "BM,MC,JM,SLV,SPSM,GGXH,JLDW,DJ,HSJBZ,XTHASH,HYSY,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC", str4 };
                                    if (ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSP", objArray) == null)
                                    {
                                        MessageBoxHelper.Show("您输入的商品不唯一，需要选择确定！");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    HandleException.HandleError(exception);
                }
            }
            finally
            {
            }
        }

        private void spmcBt_OnAutoComplate(object sender, EventArgs e)
        {
            try
            {
                string text = this.spmcBt.Text;
                double num = -1.0;
                string str2 = "";
                if (this.bill.HYSY && (this.bill.DJZL == "s"))
                {
                    num = 0.05;
                    str2 = "HYSY";
                }
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSPMore", new object[] { text, 20, "BM,MC,JM,SLV,SPSM,GGXH,JLDW,DJ,HSJBZ,XTHASH,HYSY,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC", num, str2 });
                if ((objArray != null) && (objArray.Length > 0))
                {
                    DataTable table = objArray[0] as DataTable;
                    if (table != null)
                    {
                        this.spmcBt.set_DataSource(table);
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void spmcBt_OnSelectValue(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> dictionary = this.spmcBt.get_SelectDict();
                object[] sp = new object[] { dictionary["BM"].Trim(), dictionary["MC"].Trim(), dictionary["JM"].Trim(), dictionary["SLV"].Trim(), dictionary["SPSM"].Trim(), dictionary["GGXH"].Trim(), dictionary["JLDW"].Trim(), dictionary["DJ"].Trim(), dictionary["HSJBZ"].Trim(), dictionary["XTHASH"].Trim(), dictionary["HYSY"].Trim(), dictionary["SPFL"].Trim(), dictionary["YHZC"].Trim(), dictionary["SPFL_ZZSTSGL"].Trim(), dictionary["YHZC_SLV"].Trim(), dictionary["YHZCMC"].Trim() };
                if (CommonTool.isSPBMVersion())
                {
                    string str = sp[11].ToString();
                    string str2 = sp[0].ToString();
                    string str3 = sp[1].ToString();
                    if (str == "")
                    {
                        object[] objArray2 = new object[] { str3, "BM,MC,JM,SLV,SPSM,GGXH,JLDW,DJ,HSJBZ,XTHASH,HYSY,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC", str2, true };
                        object[] objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray2);
                        if (objArray3 != null)
                        {
                            this.SetSpxx(objArray3);
                        }
                        else
                        {
                            MessageBoxHelper.Show("商品分类编码需要补全！");
                        }
                    }
                    else
                    {
                        this.SetSpxx(sp);
                    }
                }
                else
                {
                    this.SetSpxx(sp);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void spmcBt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                switch (keyCode)
                {
                    case Keys.Tab:
                        return;

                    case Keys.Enter:
                        if (((this.spmcBt.get_DataSource() == null) || (this.spmcBt.get_DataSource().Rows.Count == 0)) && (this.spmcBt.Text.Trim() == ""))
                        {
                            this.SelSpxx(this.dgv_spmx, 0, 0);
                        }
                        return;
                }
                if ((keyCode == Keys.Right) && (this.spmcBt.get_SelectionStart() == this.spmcBt.Text.Length))
                {
                    this.dgv_spmx.Focus();
                    int rowIndex = this.dgv_spmx.CurrentCell.RowIndex;
                    this.dgv_spmx.CurrentCell = this.dgv_spmx.Rows[rowIndex].Cells["GGXH"];
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void spmcBt_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.oipoidd == 0)
                {
                    this.oipoidd = 1;
                }
                else if (this.dgv_spmx.RowCount != 0)
                {
                    int rowIndex = this.dgv_spmx.CurrentCell.RowIndex;
                    Goods good = this.bill.GetGood(rowIndex);
                    if (good != null)
                    {
                        this.SetSpxxSet = false;
                        if ((good.DJHXZ == 0) && (good.SPMC != this.spmcBt.Text))
                        {
                            if (string.IsNullOrEmpty(this.spmcBt.Text.Trim()))
                            {
                                this.spmcBt.Text = good.SPMC;
                            }
                            else
                            {
                                good.SPMC = this.spmcBt.Text;
                                this.ToView();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void textBoxDJH_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string text = this.textBoxDJH.Text;
                for (int i = ToolUtil.GetByteCount(text); i > 50; i = ToolUtil.GetByteCount(text))
                {
                    int length = text.Length;
                    text = text.Substring(0, length - 1);
                }
                this.textBoxDJH.Text = text;
                this.textBoxDJH.SelectionStart = this.textBoxDJH.Text.Length;
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void textBoxLeave(object sender, EventArgs e)
        {
            try
            {
                this.ToModel(this.bill);
                this.ToView();
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private double Todouble(object sldj)
        {
            try
            {
                double result = 0.0;
                if (sldj == null)
                {
                    result = 0.0;
                }
                else
                {
                    double.TryParse(sldj.ToString(), out result);
                }
                return result;
            }
            catch
            {
                return 0.0;
            }
        }

        private void ToModel(SaleBill bill)
        {
            try
            {
                try
                {
                    bill.BH = this.textBoxDJH.Text.Trim();
                    bill.BZ = this.txt_bz.Text.Trim();
                    bill.FHR = this.com_fhr.Text.Trim();
                    bill.GFDZDH = this.com_gfdzdh.Text.Trim();
                    bill.GFMC = this.com_gfmc.Text.Trim();
                    bill.GFSH = this.com_gfsbh.Text.Trim();
                    bill.GFYHZH = this.com_gfzh.Text.Trim();
                    bill.QDHSPMC = this.textBoxQDSPHMX.Text.Trim();
                    bill.SKR = this.com_skr.Text.Trim();
                    bill.XFDZDH = this.com_xfdzdh.Text.Trim();
                    bill.XFYHZH = this.com_xfyhzh.Text;
                    bill.DJRQ = this.dateTimePicker1.Value;
                    if (bill.IsANew)
                    {
                        bill.DJYF = bill.DJRQ.Month;
                    }
                    bill.DJZL = this.comboBoxDJZL.SelectedValue.ToString();
                    bill.SFZJY = this.checkBox_SFZJY.Checked;
                    bill.FHR = GetSafeData.GetSafeString(bill.FHR, 8);
                    bill.SKR = GetSafeData.GetSafeString(bill.SKR, 8);
                    bill.HYSY = this.checkBox_HYSY.Checked;
                    if (this.checkBox_XF.Checked)
                    {
                        bill.TYDH = "1";
                    }
                    else if (this.checkBox_GF.Checked)
                    {
                        bill.TYDH = "2";
                        bill.KHYHMC = this.com_xfmc.Text.Trim();
                        bill.KHYHZH = this.com_xfsbh.Text.Trim();
                    }
                    else
                    {
                        bill.TYDH = "";
                    }
                    bool flag = false;
                    foreach (Goods goods in bill.ListGoods)
                    {
                        if (Math.Abs((double) (goods.SLV - 0.015)) < 1E-05)
                        {
                            flag = true;
                            break;
                        }
                    }
                    bill.JZ_50_15 = flag;
                }
                catch
                {
                }
            }
            finally
            {
            }
        }

        private void toolStripBtnHSJG_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CheckGridFail() != 1)
                {
                    if (this.checkBox_HYSY.Checked)
                    {
                        this.toolStripBtnHSJG.Checked = false;
                    }
                    else
                    {
                        this.bill.BZ = this.txt_bz.Text.Trim();
                        this.saleBillBL.TurnContainTax(this.bill);
                        this.saleBillBL.CleanBill(this.bill);
                        this.ToView();
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void toolStripButtonCE_Click(object sender, EventArgs e)
        {
            try
            {
                if (!true)
                {
                    MessageBoxHelper.Show("不允许增加差额", "添加差额提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    new ChaE_Tax().ShowDialog();
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private double ToPerdouble(object slv)
        {
            try
            {
                double num = 0.0;
                if (slv.ToString().Contains("%"))
                {
                    num = Convert.ToDouble(slv.ToString().Replace("%", "")) / 100.0;
                }
                else
                {
                    num = Convert.ToDouble(slv);
                }
                return num;
            }
            catch
            {
                return 0.0;
            }
        }

        private Point ToPoint(PointF orient, int x, int y)
        {
            return new Point(Convert.ToInt32(orient.X) + x, Convert.ToInt32(orient.Y) + y);
        }

        private void ToStyle()
        {
            if ((this.bill.KPZT != "N") || (this.bill.DJZT == "W"))
            {
                this.txt_bz.ReadOnly = true;
                this.txt_bz.BackColor = Color.White;
                this.com_fhr.set_Edit(0);
                this.com_gfdzdh.set_Edit(0);
                this.com_gfmc.set_Edit(0);
                this.com_gfsbh.set_Edit(0);
                this.com_gfzh.set_Edit(0);
                this.textBoxQDSPHMX.ReadOnly = true;
                this.com_skr.set_Edit(0);
                this.com_xfdzdh.set_Edit(0);
                this.com_xfyhzh.set_Edit(0);
                this.com_xfmc.set_Edit(0);
                this.com_xfsbh.set_Edit(0);
                this.dateTimePicker1.Enabled = false;
                this.dateTimePicker1.BackColor = Color.White;
                this.dateTimePicker1.ForeColor = Color.Black;
                this.comboBoxDJZL.Enabled = false;
                this.comboBoxDJZL.BackColor = Color.White;
                this.comboBoxDJZL.ForeColor = Color.Black;
                this.checkBox_SFZJY.Enabled = false;
                this.checkBox_HYSY.Enabled = true;
                this.checkBox_XF.Enabled = false;
                this.checkBox_GF.Enabled = false;
            }
            if (this.bill.ContainTax)
            {
                this.dgv_spmx.Columns["DJ"].HeaderText = "单价(含税)";
                this.dgv_spmx.Columns["JE"].HeaderText = "金额(含税)";
            }
            else
            {
                this.dgv_spmx.Columns["DJ"].HeaderText = "单价";
                this.dgv_spmx.Columns["JE"].HeaderText = "金额";
            }
        }

        private void ToView()
        {
            Exception exception;
            try
            {
                int num2;
                this.spmcBt.Validating -= new CancelEventHandler(this.spmcBt_Validating);
                this.dgv_spmx.CellEndEdit -= new DataGridViewCellEventHandler(this.dataGridMX1_CellEndEdit);
                this.dgv_spmx.CellValueChanged -= new DataGridViewCellEventHandler(this.dataGridMX1_CellValueChanged);
                this.comboBox_SLV.SelectionChangeCommitted -= new EventHandler(this.comboBox_SLV_SelectionChangeCommitted);
                this.com_xfyhzh.OnTextChanged = (EventHandler) Delegate.Remove(this.com_xfyhzh.OnTextChanged, new EventHandler(this.com_xfyhzh_TextChanged));
                this.checkBox_HYSY.CheckedChanged -= new EventHandler(this.checkBox_HYSY_CheckedChanged);
                this.spmcBt.Visible = false;
                this.textBoxDJH.Text = this.bill.BH;
                this.txt_bz.Text = this.bill.BZ;
                this.com_fhr.Text = this.bill.FHR;
                this.lab_Amount.Text = this.bill.JEHJ.ToString();
                this.com_gfdzdh.Text = this.bill.GFDZDH;
                this.com_gfmc.Text = this.bill.GFMC;
                this.com_gfsbh.Text = this.bill.GFSH;
                this.com_gfzh.Text = this.bill.GFYHZH;
                this.textBoxQDSPHMX.Text = this.bill.QDHSPMC;
                this.com_skr.Text = this.bill.SKR;
                this.com_xfdzdh.Text = this.bill.XFDZDH;
                this.com_xfyhzh.Text = this.bill.XFYHZH;
                try
                {
                    this.dateTimePicker1.Text = this.bill.DJRQ.ToShortDateString();
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    MessageManager.ShowMsgBox("单据日期[" + this.bill.DJRQ.ToString("yyyy-MM-dd") + "]异常，无法正常显示！");
                }
                this.comboBoxDJZL.SelectedValue = this.bill.DJZL;
                this.checkBox_SFZJY.Checked = this.bill.SFZJY;
                this.checkBox_HYSY.Checked = this.bill.HYSY;
                this.lab_KPZT.Text = ShowString.ShowKPZT(this.bill.KPZT);
                this.lab_DJZT.Text = ShowString.ShowDJZT(this.bill.DJZT);
                this.checkBox_XF.CheckedChanged -= new EventHandler(this.checkBox_XF_CheckedChanged);
                this.checkBox_GF.CheckedChanged -= new EventHandler(this.checkBox_GF_CheckedChanged);
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if (this.bill.TYDH == "1")
                {
                    this.checkBox_XF.Checked = true;
                    this.checkBox_GF.Checked = false;
                    this.com_xfmc.Text = card.get_Corporation();
                    this.com_xfsbh.Text = card.get_TaxCode();
                }
                else if (this.bill.TYDH == "2")
                {
                    this.checkBox_XF.Checked = false;
                    this.checkBox_GF.Checked = true;
                    this.com_gfmc.Text = card.get_Corporation();
                    this.com_gfsbh.Text = card.get_TaxCode();
                    this.com_xfmc.Text = this.bill.KHYHMC;
                    this.com_xfsbh.Text = this.bill.KHYHZH;
                }
                else
                {
                    this.checkBox_XF.Checked = false;
                    this.checkBox_GF.Checked = false;
                    this.com_xfmc.Text = card.get_Corporation();
                    this.com_xfsbh.Text = card.get_TaxCode();
                }
                this.checkBox_XF.CheckedChanged += new EventHandler(this.checkBox_XF_CheckedChanged);
                this.checkBox_GF.CheckedChanged += new EventHandler(this.checkBox_GF_CheckedChanged);
                bool flag = this.dgv_spmx.Rows.Count > 0;
                this.dgv_spmx.ReadOnly = false;
                int num = this.dgv_spmx.Rows.Count - this.bill.ListGoods.Count;
                for (num2 = 0; num2 < num; num2++)
                {
                    if (this.dgv_spmx.EndEdit())
                    {
                    }
                    this.dgv_spmx.Rows.RemoveAt(0);
                }
                this.dgv_spmx.CellValueChanged -= new DataGridViewCellEventHandler(this.dataGridMX1_CellValueChanged);
                this.ToStyle();
                this.dgv_spmx.ReadOnly = false;
                for (num2 = 0; num2 < this.bill.ListGoods.Count; num2++)
                {
                    int num4;
                    Goods good = this.bill.GetGood(num2);
                    if (!flag)
                    {
                        this.dgv_spmx.Rows.Add();
                    }
                    if (this.dgv_spmx.Rows.Count <= num2)
                    {
                        this.dgv_spmx.Rows.Add();
                    }
                    DataGridViewRow dRow = this.dgv_spmx.Rows[num2];
                    dRow.Cells["SPMC"].Value = good.SPMC;
                    dRow.Cells["DJ"].Value = this.bill.getDj(num2);
                    dRow.Cells["SL"].Value = this.bill.getSl(num2);
                    dRow.Cells["JE"].Value = this.bill.getJe(num2);
                    dRow.Cells["SE"].Value = this.bill.getSe(num2);
                    dRow.Cells["KCE"].Value = good.KCE.ToString();
                    dRow.Cells["JLDW"].Value = good.JLDW;
                    dRow.Cells["GGXH"].Value = good.GGXH;
                    dRow.Cells["SPSM"].Value = good.SPSM;
                    dRow.Cells["SLV"].Value = good.SLV;
                    if (CommonTool.isSPBMVersion())
                    {
                        if (good.XSYH && Finacial.Equal(good.SLV, 0.0))
                        {
                            if (good.LSLVBS.Trim() == "1")
                            {
                                dRow.Cells["SLV"].Value = "免税";
                            }
                            else if (good.LSLVBS.Trim() == "2")
                            {
                                dRow.Cells["SLV"].Value = "不征税";
                            }
                            else
                            {
                                dRow.Cells["SLV"].Value = "0%";
                            }
                        }
                    }
                    else if (Finacial.Equal(good.SLV, 0.0))
                    {
                        dRow.Cells["SLV"].Value = "免税";
                    }
                    if (Finacial.Equal(good.SLV, 0.05) || Finacial.Equal(good.SLV, 0.0))
                    {
                        this.dgv_spmx.InvalidateCell(dRow.Cells["SLV"]);
                    }
                    dRow.Cells["DJHXZ"].Value = good.DJHXZ;
                    if (!string.IsNullOrEmpty(good.FPDM))
                    {
                        if (good.FPHM > 0)
                        {
                            dRow.Cells["FPZL"].Value = ShowString.ShowFPZL(good.FPZL);
                            dRow.Cells["FPDM"].Value = good.FPDM;
                            dRow.Cells["FPHM"].Value = good.FPHM.ToString().PadLeft(8, '0');
                            dRow.ReadOnly = true;
                        }
                    }
                    else
                    {
                        dRow.Cells["FPZL"].Value = "";
                        dRow.Cells["FPDM"].Value = "";
                        dRow.Cells["FPHM"].Value = "";
                        dRow.ReadOnly = false;
                    }
                    int num3 = 8;
                    switch (good.DJHXZ)
                    {
                        case 0:
                            this.SetRowBackColor(dRow, Color.White);
                            if (string.IsNullOrEmpty(good.FPDM))
                            {
                                break;
                            }
                            dRow.ReadOnly = true;
                            goto Label_0A49;

                        case 3:
                            this.SetRowBackColor(dRow, Color.LightCyan);
                            dRow.ReadOnly = true;
                            num4 = 0;
                            goto Label_09F2;

                        case 4:
                            this.SetRowBackColor(dRow, Color.LightBlue);
                            dRow.ReadOnly = true;
                            num4 = 0;
                            goto Label_0A39;

                        default:
                            goto Label_0A49;
                    }
                    dRow.ReadOnly = false;
                    goto Label_0A49;
                Label_09D5:
                    dRow.Cells[num4].ReadOnly = true;
                    num4++;
                Label_09F2:
                    if (num4 < num3)
                    {
                        goto Label_09D5;
                    }
                    goto Label_0A49;
                Label_0A1C:
                    dRow.Cells[num4].ReadOnly = true;
                    num4++;
                Label_0A39:
                    if (num4 < num3)
                    {
                        goto Label_0A1C;
                    }
                Label_0A49:
                    if (this.bill.ListGoods.Count >= 2)
                    {
                        dRow.Cells["KCE"].ReadOnly = true;
                    }
                }
                this.ViewSetJeSe(this.bill);
                this.dgv_spmx.Columns["FPZL"].ReadOnly = true;
                this.dgv_spmx.Columns["FPDM"].ReadOnly = true;
                this.dgv_spmx.Columns["FPHM"].ReadOnly = true;
                if ((this.dgv_spmx.SelectedCells.Count <= 0) && (this.dgv_spmx.Rows.Count > 0))
                {
                    this.dgv_spmx.Rows[0].Cells[3].Selected = true;
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                HandleException.HandleError(exception);
            }
            finally
            {
                this.spmcBt.Validating += new CancelEventHandler(this.spmcBt_Validating);
                this.dgv_spmx.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridMX1_CellEndEdit);
                this.dgv_spmx.CellValueChanged += new DataGridViewCellEventHandler(this.dataGridMX1_CellValueChanged);
                this.comboBox_SLV.SelectionChangeCommitted += new EventHandler(this.comboBox_SLV_SelectionChangeCommitted);
                this.com_xfyhzh.OnTextChanged = (EventHandler) Delegate.Combine(this.com_xfyhzh.OnTextChanged, new EventHandler(this.com_xfyhzh_TextChanged));
                this.checkBox_HYSY.CheckedChanged += new EventHandler(this.checkBox_HYSY_CheckedChanged);
            }
            this.ShowCurrentSPRowNumInfo();
        }

        private void txt_bz_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.InvoiceKP != null)
                {
                    string str = this.txt_bz.Text.Trim();
                    this.InvoiceKP.set_Bz(str);
                    string strB = this.InvoiceKP.get_Bz();
                    if (str.CompareTo(strB) != 0)
                    {
                        this.txt_bz.Text = strB;
                        this.txt_bz.SelectionStart = this.txt_bz.Text.Length;
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private bool ValitBtnCancel()
        {
            CancelEventArgs e = new CancelEventArgs {
                Cancel = false
            };
            if ((this.dgv_spmx.Rows.Count > 0) && (this.dgv_spmx.CurrentCell.ColumnIndex == 6))
            {
                this.comboBox_SLV_Validating(this.comboBox_SLV, e);
            }
            return e.Cancel;
        }

        private void ViewSetJeSe(SaleBill bill)
        {
            double num = Convert.ToDouble(bill.TotalAmount);
            double num2 = Convert.ToDouble(bill.TotalTax);
            double num3 = Convert.ToDouble(bill.TotalAmountTax);
            this.lab_Amount.Text = ("金额:￥" + num.ToString("0.00"));
            this.lab_Tax.Text = ("税额:￥" + num2.ToString("0.00"));
            this.lab_hj_xx.Text = "￥" + num3.ToString("0.00");
            this.lab_hj_jshj_dx.Text = "㊣ " + ToolUtil.RMBToDaXie(Convert.ToDecimal(num3));
        }

        private void xfxx_OnAutoComplate(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                text = combox.Text;
                DataTable table = this._XfxxOnAutoCompleteDataSource(text);
                if (table != null)
                {
                    combox.set_DataSource(table);
                }
            }
        }

        private void xfxx_OnButtonClick(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            this._XfxxSelect(combox.Text, 0);
        }

        private void xfxx_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> khxx = combox.get_SelectDict();
                this._XfxxSetValue(khxx);
            }
        }

        private void XfxxSetValue(object[] xfxx)
        {
            if ((xfxx != null) && (xfxx.Length > 2))
            {
                string mc = xfxx[0].ToString();
                string sh = xfxx[1].ToString();
                string str3 = this.ConvertXfdzdh(xfxx[2].ToString());
                this.com_xfdzdh.Text = str3;
                this.txt_bz.Text = this.SetDkBz(mc, sh);
            }
        }

        private void XSDJAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && (this.AddCount > 0))
            {
                base.DialogResult = DialogResult.OK;
            }
        }

        private void XSDJEdite_Load(object sender, EventArgs e)
        {
            try
            {
                int count = this.dgv_spmx.Columns.Count;
                if (this.dgv_spmx.Rows.Count > 0)
                {
                    this.dgv_spmx.CurrentCell = this.dgv_spmx.Rows[0].Cells[1];
                    this.SetDataGridPropEven(this.dgv_spmx);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private bool IsHYSY
        {
            get
            {
                this.isHYSY = ((this.comboBoxDJZL.SelectedValue != null) && (this.comboBoxDJZL.SelectedValue.ToString() == "s")) && this.checkBox_HYSY.Checked;
                return this.isHYSY;
            }
        }
    }
}

