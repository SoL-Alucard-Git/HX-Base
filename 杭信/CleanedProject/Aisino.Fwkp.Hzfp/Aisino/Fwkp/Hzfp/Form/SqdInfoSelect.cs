namespace Aisino.Fwkp.Hzfp.Form
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class SqdInfoSelect : DockForm
    {
        private AisinoBTN but_close;
        private AisinoPNL BuyerPanel;
        private AisinoRDO BuyerRB;
        private IContainer components;
        private CustomStyleDataGrid csdgBlueInvInfo;
        private AisinoRDO GoodsRB;
        private TextBoxRegex InvCodeEdit;
        private AisinoRDO InvCodeNoRB;
        private AisinoCMB InvKindCombo;
        private TextBoxRegex InvNoEdt;
        private string InvoiceKind = "";
        private AisinoRDO KpErRefRB;
        private AisinoRDO KpErSendRB;
        private AisinoLBL lblInfo;
        private AisinoBTN NextBtn;
        private AisinoBTN OkBtn;
        private AisinoPNL panel3;
        private AisinoPNL panel4;
        private AisinoPNL panel5;
        private string Reasons = "";
        private AisinoRDO RenZRB;
        public List<object> SelectInfor = new List<object>();
        private AisinoPNL SellerPanel;
        private AisinoRDO SellerRB;
        private AisinoRDO TaxcodeRB;
        private string VersionName = "税务代开";
        private AisinoPNL WDKPanel;
        private AisinoRDO WDKRB;
        private XmlComponentLoader xmlComponentLoader1;
        private AisinoRDO YDKRB;
        private DataGridViewTextBoxColumn ZDMC = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn ZDNR = new DataGridViewTextBoxColumn();

        public SqdInfoSelect()
        {
            this.Initialize();
            this.panel3.Visible = false;
            this.NextBtn.Enabled = false;
            this.BuyerRB.Checked = true;
            this.YDKRB.Checked = true;
            this.InvCodeEdit.Enabled = false;
            this.InvNoEdt.Enabled = false;
            if (!TaxCardFactory.CreateTaxCard().QYLX.ISZYFP)
            {
                this.SellerRB.Checked = false;
                this.KpErRefRB.Checked = false;
                this.KpErSendRB.Checked = false;
                this.SellerRB.Enabled = false;
                this.KpErRefRB.Enabled = false;
                this.KpErSendRB.Enabled = false;
            }
            this.InvCodeEdit.RegexText="^[0-9]{0,10}$";
            this.InvNoEdt.RegexText="^[0-9]{0,8}$";
            if (this.VersionName == "税务代开")
            {
                this.InvKindCombo.DataSource = new string[] { "增值税专用发票" };
            }
            else
            {
                this.InvKindCombo.DataSource = new string[] { "增值税专用发票" };
            }
            this.InvKindCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BlueInvInfoGridShow(Fpxx fp, string InvKind, double Amount)
        {
            DataTable table = new DataTable();
            table.Columns.Add("ZDMC", typeof(string));
            table.Columns.Add("ZDNR", typeof(string));
            DataRow row = table.NewRow();
            row["ZDMC"] = "类别代码";
            row["ZDNR"] = fp.fpdm;
            table.Rows.Add(row);
            DataRow row2 = table.NewRow();
            row2["ZDMC"] = "发票号码";
            row2["ZDNR"] = fp.fphm;
            table.Rows.Add(row2);
            DataRow row3 = table.NewRow();
            row3["ZDMC"] = "购方名称";
            row3["ZDNR"] = fp.gfmc;
            table.Rows.Add(row3);
            DataRow row4 = table.NewRow();
            row4["ZDMC"] = "购方税号";
            row4["ZDNR"] = fp.gfsh;
            table.Rows.Add(row4);
            DataRow row5 = table.NewRow();
            row5["ZDMC"] = "购方地址电话";
            row5["ZDNR"] = fp.gfdzdh;
            table.Rows.Add(row5);
            DataRow row6 = table.NewRow();
            row6["ZDMC"] = "购方银行账号";
            row6["ZDNR"] = fp.gfyhzh;
            table.Rows.Add(row6);
            if (InvKind == "C")
            {
                DataRow row7 = table.NewRow();
                row7["ZDMC"] = "合计金额（含税）";
                row7["ZDNR"] = string.Format("{0:0.00}", Amount);
                table.Rows.Add(row7);
            }
            else
            {
                DataRow row8 = table.NewRow();
                row8["ZDMC"] = "合计金额（不含税）";
                row8["ZDNR"] = string.Format("{0:0.00}", Amount);
                table.Rows.Add(row8);
            }
            DataRow row9 = table.NewRow();
            row9["ZDMC"] = "合计税额";
            row9["ZDNR"] = fp.se;
            table.Rows.Add(row9);
            DataRow row10 = table.NewRow();
            row10["ZDMC"] = "开票日期";
            row10["ZDNR"] = fp.kprq;
            table.Rows.Add(row10);
            DataRow row11 = table.NewRow();
            row11["ZDMC"] = "开票人";
            row11["ZDNR"] = fp.kpr;
            table.Rows.Add(row11);
            DataRow row12 = table.NewRow();
            row12["ZDMC"] = "作废标志";
            row12["ZDNR"] = fp.zfbz ? "是" : "否";
            table.Rows.Add(row12);
            this.csdgBlueInvInfo.DataSource = table;
            this.csdgBlueInvInfo.Visible = true;
        }

        private void but_close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void but_next_Click(object sender, EventArgs e)
        {
            if (!(this.NextBtn.Text == "下一步"))
            {
                if (this.BuyerRB.Checked)
                {
                    this.NextBtn.Enabled = false;
                    this.OkBtn.Enabled = true;
                }
                if (this.SellerRB.Checked)
                {
                    this.NextBtn.Enabled = true;
                    this.OkBtn.Enabled = false;
                }
                this.panel3.Visible = false;
                this.panel4.Visible = true;
                this.NextBtn.Text = "下一步";
            }
            else
            {
                string str = "";
                if ((((!this.BuyerRB.Checked && !this.SellerRB.Checked) && (!this.YDKRB.Checked && !this.WDKRB.Checked)) && ((!this.RenZRB.Checked && !this.TaxcodeRB.Checked) && (!this.InvCodeNoRB.Checked && !this.GoodsRB.Checked))) && (!this.KpErRefRB.Checked && !this.KpErSendRB.Checked))
                {
                    MessageManager.ShowMsgBox("INP-431371");
                }
                else if ((this.InvKindCombo.Text == "") || (this.InvKindCombo.SelectedIndex < 0))
                {
                    str = "发票种类输入信息不正确。\n";
                    str = str + "发票种类为空，请选择发票种类！";
                    MessageManager.ShowMsgBox("INP-431372");
                }
                else
                {
                    switch (this.InvKindCombo.SelectedIndex)
                    {
                        case 0:
                            this.InvoiceKind = "s";
                            break;

                        case 1:
                            this.InvoiceKind = "c";
                            break;
                    }
                    if (!this.YDKRB.Checked)
                    {
                        if (this.InvCodeEdit.Text == "")
                        {
                            str = "发票代码输入信息不正确。\n";
                            str = str + "发票代码为空，请输入发票代码！！";
                            MessageManager.ShowMsgBox("INP-431373");
                            return;
                        }
                        if (this.InvNoEdt.Text == "")
                        {
                            str = "发票号码输入信息不正确。\n";
                            str = str + "发票号码为空，请输入发票号码！";
                            MessageManager.ShowMsgBox("INP-431374");
                            return;
                        }
                        if (this.InvCodeEdit.Text.Trim().Length < 10)
                        {
                            str = "发票代码输入信息不正确。\n";
                            str = str + "发票代码小于10位，请输入正确发票代码！";
                            MessageManager.ShowMsgBox("INP-431375");
                            return;
                        }
                        if (!this.ConValid())
                        {
                            str = "发票代码(或号码)输入信息不正确。\n";
                            MessageManager.ShowMsgBox("INP-431376");
                            return;
                        }
                        this.OkBtn.Enabled = false;
                        this.FindInv(this.InvoiceKind, this.InvCodeEdit.Text.Trim(), this.InvNoEdt.Text.Trim());
                    }
                    this.panel4.Visible = false;
                    this.panel3.Visible = true;
                    this.NextBtn.Text = "上一步";
                }
            }
        }

        private void but_ok_Click(object sender, EventArgs e)
        {
            string str = "";
            if ((((!this.BuyerRB.Checked && !this.SellerRB.Checked) && (!this.YDKRB.Checked && !this.WDKRB.Checked)) && ((!this.RenZRB.Checked && !this.TaxcodeRB.Checked) && (!this.InvCodeNoRB.Checked && !this.GoodsRB.Checked))) && (!this.KpErRefRB.Checked && !this.KpErSendRB.Checked))
            {
                MessageManager.ShowMsgBox("INP-431371");
            }
            else if ((this.InvKindCombo.Text == "") || (this.InvKindCombo.SelectedIndex < 0))
            {
                str = "发票种类输入信息不正确。\n";
                str = str + "发票种类为空，请选择发票种类！";
                MessageManager.ShowMsgBox("INP-431372");
            }
            else
            {
                switch (this.InvKindCombo.SelectedIndex)
                {
                    case 0:
                        this.InvoiceKind = "s";
                        break;

                    case 1:
                        this.InvoiceKind = "c";
                        break;
                }
                if (!this.YDKRB.Checked)
                {
                    if (this.InvCodeEdit.Text == "")
                    {
                        str = "发票代码输入信息不正确。\n";
                        str = str + "发票代码为空，请输入发票代码！！";
                        MessageManager.ShowMsgBox("INP-431373");
                        return;
                    }
                    if (this.InvNoEdt.Text == "")
                    {
                        str = "发票号码输入信息不正确。\n";
                        str = str + "发票号码为空，请输入发票号码！";
                        MessageManager.ShowMsgBox("INP-431374");
                        return;
                    }
                    if (this.InvCodeEdit.Text.Trim().Length < 10)
                    {
                        str = "发票代码输入信息不正确。\n";
                        str = str + "发票代码小于10位，请输入正确发票代码！";
                        MessageManager.ShowMsgBox("INP-431375");
                        return;
                    }
                    if (!this.ConValid())
                    {
                        str = "发票代码(或号码)输入信息不正确。\n";
                        MessageManager.ShowMsgBox("INP-431376");
                        return;
                    }
                }
                this.GetSelectReason();
                this.SelectInfor.Clear();
                this.SelectInfor.Add(this.InvCodeEdit.Text.Trim());
                this.SelectInfor.Add(this.InvNoEdt.Text.Trim());
                this.SelectInfor.Add((this.InvKindCombo.SelectedValue.ToString() == "增值税专用发票") ? "s" : "c");
                this.SelectInfor.Add(this.Reasons);
                this.SelectInfor.Add(this.BuyerRB.Checked ? "0" : "1");
                SqdTianKai kai = new SqdTianKai();
                kai.TabText=("红字发票信息表填开");
                kai.InitSqdMx(InitSqdMxType.Add, this.SelectInfor);
                if ((!SqdTianKai.noslv && !SqdTianKai.lp_error) && (SqdTianKai.lp_mxchao78 == 0))
                {
                    kai.Show(FormMain.control_0);
                }
                else if (SqdTianKai.lp_mxchao78 == 7)
                {
                    MessageManager.ShowMsgBox("INP-431363", new string[] { "7" });
                    MessageManager.ShowMsgBox("INP-431364");
                    SqdTianKai.lp_mxchao78 = 0;
                }
                else if (SqdTianKai.lp_mxchao78 == 8)
                {
                    MessageManager.ShowMsgBox("INP-431363", new string[] { "8" });
                    MessageManager.ShowMsgBox("INP-431364");
                    SqdTianKai.lp_mxchao78 = 0;
                }
                base.Close();
            }
        }

        private void BuyerRB_Click(object sender, EventArgs e)
        {
            if (this.BuyerRB.Checked)
            {
                this.SellerPanel.Enabled = false;
                this.KpErRefRB.Enabled = false;
                this.KpErSendRB.Enabled = false;
                this.KpErRefRB.Checked = false;
                this.KpErSendRB.Checked = false;
                this.BuyerPanel.Enabled = true;
                this.WDKPanel.Enabled = true;
                this.YDKRB.Enabled = true;
                this.WDKRB.Enabled = true;
                this.RenZRB.Enabled = true;
                this.TaxcodeRB.Enabled = true;
                this.InvCodeNoRB.Enabled = true;
                this.GoodsRB.Enabled = true;
                this.InvCodeEdit.Text = "";
                this.InvNoEdt.Text = "";
                this.InvCodeEdit.Enabled = false;
                this.InvNoEdt.Enabled = false;
                this.OkBtn.Enabled = false;
            }
        }

        private bool ConValid()
        {
            try
            {
                string str = "";
                int num = 0;
                if ((this.InvoiceKind == "s") || (this.InvoiceKind == "w"))
                {
                    str = this.InvCodeEdit.Text.Trim();
                    num = 10;
                    if ((str.Length < num) || (str == ""))
                    {
                        return false;
                    }
                }
                if (((this.InvNoEdt.Text == "") || (Convert.ToDouble(this.InvNoEdt.Text) <= 0.0)) || (this.InvNoEdt.Text.Trim().Length > 8))
                {
                    return false;
                }
                if (str == "0000000000")
                {
                    return false;
                }
                int length = str.Length;
                if (this.SellerRB.Checked)
                {
                    this.NextBtn.Enabled = true;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FindInv(string InvKind, string InvType, string InvNo)
        {
            this.csdgBlueInvInfo.Visible = false;
            object[] objArray = new object[] { InvKind, InvType, Convert.ToInt32(InvNo) };
            object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPXX", objArray);
            if (objArray2 != null)
            {
                Fpxx fp = objArray2[0] as Fpxx;
                if (fp != null)
                {
                    double amount = 0.0;
                    amount = Convert.ToDouble(fp.je);
                    string s = DateTime.Parse(fp.kprq).AddDays(180.0).ToString().Split(new char[] { ' ' })[0];
                    DateTime time2 = DateTime.Parse(s);
                    string str4 = base.TaxCardInstance.GetCardClock().ToString().Split(new char[] { ' ' })[0];
                    DateTime time3 = DateTime.Parse(str4);
                    if ((amount <= 0.0) || fp.zfbz)
                    {
                        string str5 = "";
                        if (amount < 0.0)
                        {
                            str5 = "发票为红字发票";
                        }
                        else if (fp.zfbz)
                        {
                            str5 = "发票已作废";
                        }
                        this.lblInfo.Text = "本张发票不可以开红字发票！\r\n原因：" + str5;
                        this.OkBtn.Enabled = false;
                    }
                    else if (!string.IsNullOrEmpty(fp.kprq) && (time2 < time3))
                    {
                        this.lblInfo.Text = "本张发票不能开具红字发票信息表！\r\n原因：对应的蓝字增值税专用发票超过认证期限";
                        this.OkBtn.Enabled = false;
                    }
                    else
                    {
                        this.lblInfo.Text = "本张发票可以开红字发票！";
                        this.OkBtn.Enabled = true;
                    }
                    this.BlueInvInfoGridShow(fp, InvKind, amount);
                }
                else
                {
                    this.lblInfo.Text = "本张发票可以开红字发票！\n但在当前库没有找到相应信息。";
                    this.OkBtn.Enabled = true;
                }
            }
        }

        private bool GetSelectReason()
        {
            bool flag = true;
            try
            {
                if (this.BuyerRB.Checked)
                {
                    this.Reasons = "1";
                    if (this.YDKRB.Checked)
                    {
                        this.Reasons = this.Reasons + "100000000";
                        return flag;
                    }
                    this.Reasons = this.Reasons + "0";
                    if (this.WDKRB.Checked)
                    {
                        this.Reasons = this.Reasons + "1";
                        if (this.RenZRB.Checked)
                        {
                            this.Reasons = this.Reasons + "1";
                        }
                        else
                        {
                            this.Reasons = this.Reasons + "0";
                        }
                        if (this.TaxcodeRB.Checked)
                        {
                            this.Reasons = this.Reasons + "1";
                        }
                        else
                        {
                            this.Reasons = this.Reasons + "0";
                        }
                        if (this.InvCodeNoRB.Checked)
                        {
                            this.Reasons = this.Reasons + "1";
                        }
                        else
                        {
                            this.Reasons = this.Reasons + "0";
                        }
                        if (this.GoodsRB.Checked)
                        {
                            this.Reasons = this.Reasons + "1";
                        }
                        else
                        {
                            this.Reasons = this.Reasons + "0";
                        }
                        this.Reasons = this.Reasons + "000";
                        return flag;
                    }
                    this.Reasons = this.Reasons + "00000000";
                    return flag;
                }
                this.Reasons = "0000000";
                if (this.SellerRB.Checked)
                {
                    this.Reasons = this.Reasons + "1";
                    if (this.KpErRefRB.Checked)
                    {
                        this.Reasons = this.Reasons + "1";
                    }
                    else
                    {
                        this.Reasons = this.Reasons + "0";
                    }
                    if (this.KpErSendRB.Checked)
                    {
                        this.Reasons = this.Reasons + "1";
                        return flag;
                    }
                    this.Reasons = this.Reasons + "0";
                    return flag;
                }
                this.Reasons = this.Reasons + "000";
            }
            catch
            {
                return false;
            }
            return flag;
        }

        private void GridShowHeaderInit()
        {
            this.csdgBlueInvInfo.Rows.Clear();
            this.csdgBlueInvInfo.GridStyle=(CustomStyle)1;
            this.csdgBlueInvInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.csdgBlueInvInfo.ReadOnly = true;
            this.csdgBlueInvInfo.MultiSelect = false;
            this.csdgBlueInvInfo.AllowUserToAddRows = false;
            this.csdgBlueInvInfo.AllowUserToDeleteRows = false;
            this.csdgBlueInvInfo.AllowColumnHeadersVisible=(false);
            this.csdgBlueInvInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.ZDMC.HeaderText = "发票字段";
            this.ZDMC.Name = "ZDMC";
            this.ZDMC.DataPropertyName = "ZDMC";
            this.ZDMC.Visible = true;
            this.ZDMC.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.ZDMC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ZDMC.Width = 150;
            this.ZDNR.HeaderText = "字段信息";
            this.ZDNR.Name = "ZDNR";
            this.ZDNR.DataPropertyName = "ZDNR";
            this.ZDNR.Visible = true;
            this.ZDNR.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.ZDNR.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ZDNR.Width = 0xbd;
            this.csdgBlueInvInfo.ColumnAdd(this.ZDMC);
            this.csdgBlueInvInfo.SetColumnReadOnly(0, true);
            this.csdgBlueInvInfo.ColumnAdd(this.ZDNR);
            this.csdgBlueInvInfo.SetColumnReadOnly(1, true);
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.BuyerRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ");
            this.SellerRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_SellerSQ");
            this.YDKRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Ydk");
            this.WDKRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk");
            this.KpErRefRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_SellerSQ_1");
            this.KpErSendRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_SellerSQ_2");
            this.RenZRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_1");
            this.TaxcodeRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_2");
            this.InvCodeNoRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_3");
            this.GoodsRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_4");
            this.RenZRB.Enabled = false;
            this.TaxcodeRB.Enabled = false;
            this.InvCodeNoRB.Enabled = false;
            this.GoodsRB.Enabled = false;
            this.KpErRefRB.Enabled = false;
            this.KpErSendRB.Enabled = false;
            this.BuyerRB.Click += new EventHandler(this.BuyerRB_Click);
            this.SellerRB.Click += new EventHandler(this.SellerRB_Click);
            this.YDKRB.Click += new EventHandler(this.YDKRB_Click);
            this.WDKRB.Click += new EventHandler(this.WDKRB_Click);
            this.KpErRefRB.Click += new EventHandler(this.KpErRefRB_Click);
            this.KpErSendRB.Click += new EventHandler(this.KpErRefRB_Click);
            this.RenZRB.Click += new EventHandler(this.RenZRB_Click);
            this.TaxcodeRB.Click += new EventHandler(this.RenZRB_Click);
            this.InvCodeNoRB.Click += new EventHandler(this.RenZRB_Click);
            this.GoodsRB.Click += new EventHandler(this.RenZRB_Click);
            this.InvKindCombo = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("txt_fpzl");
            this.NextBtn = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_next");
            this.OkBtn = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
            this.but_close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
            this.InvNoEdt = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("txt_fphm");
            this.InvCodeEdit = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("txt_fpdm");
            this.InvCodeEdit.KeyPress += new KeyPressEventHandler(this.InvCodeEdit_KeyPress);
            this.InvCodeEdit.MaxLength = 10;
            this.InvNoEdt.KeyPress += new KeyPressEventHandler(this.InvNoEdt_KeyPress);
            this.InvNoEdt.MaxLength = 8;
            this.BuyerPanel = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.SellerPanel = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.WDKPanel = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1_2");
            this.panel3 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel3");
            this.panel4 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel4");
            this.lblInfo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblInfo");
            this.csdgBlueInvInfo = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1");
            this.GridShowHeaderInit();
            this.but_close.Click += new EventHandler(this.but_close_Click);
            this.NextBtn.Click += new EventHandler(this.but_next_Click);
            this.OkBtn.Click += new EventHandler(this.but_ok_Click);
            this.InvCodeEdit.TextChanged += new EventHandler(this.txt_fphm_TextChanged);
            this.InvNoEdt.TextChanged += new EventHandler(this.txt_fphm_TextChanged);
            this.panel5 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel5");
            base.Paint += new PaintEventHandler(this.SqdInfoSelect_Paint);
            base.Resize += new EventHandler(this.SqdInfoSelect_Resize);
            this.panel5.BorderStyle = BorderStyle.Fixed3D;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SqdInfoSelect));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1bc, 0x1ba);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "红字增值税专用发票信息表信息选择";
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.Hzfp.Form.SqdInfoSelect\Aisino.Fwkp.Hzfp.Form.SqdInfoSelect.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1bc, 0x1ba);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "SqdInfoSelect";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "红字增值税专用发票信息表信息选择";
            base.ResumeLayout(false);
        }

        private void InvCodeEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
        }

        private void InvNoEdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
        }

        private void KpErRefRB_Click(object sender, EventArgs e)
        {
            this.InvCodeEdit.Enabled = true;
            this.InvNoEdt.Enabled = true;
            this.OkBtn.Enabled = false;
        }

        private void RenZRB_Click(object sender, EventArgs e)
        {
            this.WDKRB.Checked = true;
            this.InvCodeEdit.Enabled = true;
            this.InvNoEdt.Enabled = true;
        }

        private void SellerRB_Click(object sender, EventArgs e)
        {
            if (this.SellerRB.Checked)
            {
                this.SellerPanel.Enabled = true;
                this.KpErRefRB.Enabled = true;
                this.KpErSendRB.Enabled = true;
                this.BuyerPanel.Enabled = false;
                this.WDKPanel.Enabled = false;
                this.YDKRB.Enabled = false;
                this.WDKRB.Enabled = false;
                this.RenZRB.Enabled = false;
                this.TaxcodeRB.Enabled = false;
                this.InvCodeNoRB.Enabled = false;
                this.GoodsRB.Enabled = false;
                this.YDKRB.Checked = false;
                this.WDKRB.Checked = false;
                this.RenZRB.Checked = false;
                this.TaxcodeRB.Checked = false;
                this.InvCodeNoRB.Checked = false;
                this.GoodsRB.Checked = false;
                this.OkBtn.Enabled = false;
                this.InvCodeEdit.Text = "";
                this.InvNoEdt.Text = "";
                this.InvCodeEdit.Enabled = false;
                this.InvNoEdt.Enabled = false;
                this.NextBtn.Enabled = false;
            }
        }

        private void SqdInfoSelect_Paint(object sender, PaintEventArgs e)
        {
        }

        private void SqdInfoSelect_Resize(object sender, EventArgs e)
        {
        }

        private void txt_fphm_TextChanged(object sender, EventArgs e)
        {
            if (this.BuyerRB.Checked)
            {
                this.NextBtn.Enabled = false;
                this.OkBtn.Enabled = true;
            }
            if (this.SellerRB.Checked)
            {
                this.NextBtn.Enabled = true;
                this.OkBtn.Enabled = false;
            }
        }

        private void WDKRB_Click(object sender, EventArgs e)
        {
            if (this.WDKRB.Checked)
            {
                this.RenZRB.Enabled = true;
                this.TaxcodeRB.Enabled = true;
                this.InvCodeNoRB.Enabled = true;
                this.GoodsRB.Enabled = true;
                this.InvCodeEdit.Text = "";
                this.InvNoEdt.Text = "";
                this.OkBtn.Enabled = false;
            }
        }

        private void YDKRB_Click(object sender, EventArgs e)
        {
            if (this.YDKRB.Checked)
            {
                this.RenZRB.Enabled = false;
                this.TaxcodeRB.Enabled = false;
                this.InvCodeNoRB.Enabled = false;
                this.GoodsRB.Enabled = false;
                this.InvCodeEdit.Enabled = false;
                this.InvNoEdt.Enabled = false;
                this.RenZRB.Checked = false;
                this.TaxcodeRB.Checked = false;
                this.InvCodeNoRB.Checked = false;
                this.GoodsRB.Checked = false;
                this.OkBtn.Enabled = true;
                this.NextBtn.Enabled = false;
                this.OkBtn.Enabled = true;
                this.InvCodeEdit.Text = "";
                this.InvNoEdt.Text = "";
            }
        }
    }
}

