namespace Aisino.Fwkp.HzfpHy.Form
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

    public class HySqdInfoSelect : DockForm
    {
        private AisinoBTN BtnClose;
        private AisinoBTN BtnNext;
        private AisinoBTN BtnOK;
        private AisinoRDO Carrier_MistakeNonDeliverRB;
        private AisinoRDO Carrier_MistakeRejectRB;
        private AisinoPNL CarrierPanel;
        private AisinoRDO CarrierRB;
        private IContainer components;
        private CustomStyleDataGrid csdgBlueInvInfo;
        private AisinoPNL DraweePanel;
        private AisinoRDO DraweeRB;
        private AisinoRDO GoodsRB;
        private TextBoxRegex InvCodeEdit;
        private AisinoRDO InvCodeNoRB;
        private AisinoCMB InvKindCombo;
        private TextBoxRegex InvNumEdit;
        private string InvoiceKind = "";
        private AisinoLBL lblInfo;
        private AisinoPNL panel3;
        private AisinoPNL panel4;
        private AisinoPNL panel5;
        private string Reasons = "";
        private AisinoRDO RenZRB;
        public List<object> SelectInfor = new List<object>();
        private AisinoRDO TaxcodeRB;
        private string VersionName = "税务代开";
        private AisinoPNL WDKPanel;
        private AisinoRDO WDKRB;
        private XmlComponentLoader xmlComponentLoader1;
        private AisinoRDO YDKRB;
        private DataGridViewTextBoxColumn ZDMC = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn ZDNR = new DataGridViewTextBoxColumn();

        public HySqdInfoSelect()
        {
            this.Initialize();
            this.panel3.Visible = false;
            this.BtnNext.Enabled = false;
            this.DraweeRB.Checked = true;
            this.YDKRB.Checked = true;
            this.InvCodeEdit.Enabled = false;
            this.InvNumEdit.Enabled = false;
            if (!TaxCardFactory.CreateTaxCard().QYLX.ISHY)
            {
                this.CarrierRB.Checked = false;
                this.Carrier_MistakeRejectRB.Checked = false;
                this.Carrier_MistakeNonDeliverRB.Checked = false;
                this.CarrierRB.Enabled = false;
                this.Carrier_MistakeRejectRB.Enabled = false;
                this.Carrier_MistakeNonDeliverRB.Enabled = false;
            }
            this.InvCodeEdit.RegexText=("^[0-9]{0,10}$");
            this.InvNumEdit.RegexText=("^[0-9]{0,8}$");
            if (this.VersionName == "税务代开")
            {
                this.InvKindCombo.DataSource = new string[] { "货物运输业增值税专用发票" };
            }
            else
            {
                this.InvKindCombo.DataSource = new string[] { "货物运输业增值税专用发票" };
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
            row3["ZDMC"] = "实际受票方名称";
            row3["ZDNR"] = fp.spfmc;
            table.Rows.Add(row3);
            DataRow row4 = table.NewRow();
            row4["ZDMC"] = "实际受票方税号";
            row4["ZDNR"] = fp.spfnsrsbh;
            table.Rows.Add(row4);
            DataRow row5 = table.NewRow();
            row5["ZDMC"] = "发货人名称";
            row5["ZDNR"] = fp.fhrmc;
            table.Rows.Add(row5);
            DataRow row6 = table.NewRow();
            row6["ZDMC"] = "发货人税号";
            row6["ZDNR"] = fp.fhrnsrsbh;
            table.Rows.Add(row6);
            DataRow row7 = table.NewRow();
            row7["ZDMC"] = "收货人名称";
            row7["ZDNR"] = fp.shrmc;
            table.Rows.Add(row7);
            DataRow row8 = table.NewRow();
            row8["ZDMC"] = "收货人税号";
            row8["ZDNR"] = fp.shrnsrsbh;
            table.Rows.Add(row8);
            DataRow row9 = table.NewRow();
            row9["ZDMC"] = "车种车号";
            row9["ZDNR"] = fp.czch;
            table.Rows.Add(row9);
            DataRow row10 = table.NewRow();
            row10["ZDMC"] = "车船吨位";
            row10["ZDNR"] = fp.ccdw;
            table.Rows.Add(row10);
            DataRow row11 = table.NewRow();
            row11["ZDMC"] = "主管税务机关名称";
            row11["ZDNR"] = fp.zgswjgmc;
            table.Rows.Add(row11);
            DataRow row12 = table.NewRow();
            row12["ZDMC"] = "主管税务机关代码";
            row12["ZDNR"] = fp.zgswjgdm;
            table.Rows.Add(row12);
            if (InvKind == "C")
            {
                DataRow row13 = table.NewRow();
                row13["ZDMC"] = "合计金额（含税）";
                row13["ZDNR"] = string.Format("{0:0.00}", Amount);
                table.Rows.Add(row13);
            }
            else
            {
                DataRow row14 = table.NewRow();
                row14["ZDMC"] = "合计金额（不含税）";
                row14["ZDNR"] = string.Format("{0:0.00}", Amount);
                table.Rows.Add(row14);
            }
            DataRow row15 = table.NewRow();
            row15["ZDMC"] = "合计税额";
            row15["ZDNR"] = fp.se;
            table.Rows.Add(row15);
            DataRow row16 = table.NewRow();
            row16["ZDMC"] = "开票日期";
            row16["ZDNR"] = fp.kprq;
            table.Rows.Add(row16);
            DataRow row17 = table.NewRow();
            row17["ZDMC"] = "开票人";
            row17["ZDNR"] = fp.kpr;
            table.Rows.Add(row17);
            DataRow row18 = table.NewRow();
            row18["ZDMC"] = "作废标志";
            row18["ZDNR"] = fp.zfbz ? "是" : "否";
            table.Rows.Add(row18);
            this.csdgBlueInvInfo.DataSource = table;
            this.csdgBlueInvInfo.Visible = true;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (!(this.BtnNext.Text == "下一步"))
            {
                if (this.DraweeRB.Checked)
                {
                    this.BtnNext.Enabled = false;
                    this.BtnOK.Enabled = true;
                }
                if (this.CarrierRB.Checked)
                {
                    this.BtnNext.Enabled = true;
                    this.BtnOK.Enabled = false;
                }
                this.BtnNext.Text = "下一步";
                this.panel3.Visible = false;
                this.panel4.Visible = true;
            }
            else if ((((!this.DraweeRB.Checked && !this.CarrierRB.Checked) && (!this.YDKRB.Checked && !this.WDKRB.Checked)) && ((!this.RenZRB.Checked && !this.TaxcodeRB.Checked) && (!this.InvCodeNoRB.Checked && !this.GoodsRB.Checked))) && (!this.Carrier_MistakeRejectRB.Checked && !this.Carrier_MistakeNonDeliverRB.Checked))
            {
                MessageManager.ShowMsgBox("INP-431426");
            }
            else if ((this.InvKindCombo.Text == "") || (this.InvKindCombo.SelectedIndex < 0))
            {
                MessageManager.ShowMsgBox("INP-431427");
            }
            else
            {
                switch (this.InvKindCombo.SelectedIndex)
                {
                    case 0:
                        this.InvoiceKind = "f";
                        break;

                    case 1:
                        this.InvoiceKind = "c";
                        break;
                }
                if (!this.YDKRB.Checked)
                {
                    if (this.InvCodeEdit.Text == "")
                    {
                        MessageManager.ShowMsgBox("INP-431428");
                        return;
                    }
                    if (this.InvNumEdit.Text == "")
                    {
                        MessageManager.ShowMsgBox("INP-431429");
                        return;
                    }
                    if (this.InvCodeEdit.Text.Trim().Length < 10)
                    {
                        MessageManager.ShowMsgBox("INP-431430");
                        return;
                    }
                    if (!this.ConValid())
                    {
                        MessageManager.ShowMsgBox("INP-431431");
                        return;
                    }
                    this.BtnOK.Enabled = false;
                    this.FindInv(this.InvoiceKind, this.InvCodeEdit.Text.Trim(), this.InvNumEdit.Text.Trim());
                }
                this.panel4.Visible = false;
                this.panel3.Visible = true;
                this.BtnNext.Text = "上一步";
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if ((((!this.DraweeRB.Checked && !this.CarrierRB.Checked) && (!this.YDKRB.Checked && !this.WDKRB.Checked)) && ((!this.RenZRB.Checked && !this.TaxcodeRB.Checked) && (!this.InvCodeNoRB.Checked && !this.GoodsRB.Checked))) && (!this.Carrier_MistakeRejectRB.Checked && !this.Carrier_MistakeNonDeliverRB.Checked))
            {
                MessageManager.ShowMsgBox("INP-431426");
            }
            else if ((this.InvKindCombo.Text == "") || (this.InvKindCombo.SelectedIndex < 0))
            {
                MessageBoxHelper.Show("INP-431427");
            }
            else
            {
                switch (this.InvKindCombo.SelectedIndex)
                {
                    case 0:
                        this.InvoiceKind = "f";
                        break;

                    case 1:
                        this.InvoiceKind = "c";
                        break;
                }
                if (!this.YDKRB.Checked)
                {
                    if (this.InvCodeEdit.Text == "")
                    {
                        MessageManager.ShowMsgBox("INP-431428");
                        return;
                    }
                    if (this.InvNumEdit.Text == "")
                    {
                        MessageManager.ShowMsgBox("INP-431429");
                        return;
                    }
                    if (this.InvCodeEdit.Text.Trim().Length < 10)
                    {
                        MessageManager.ShowMsgBox("INP-431430");
                        return;
                    }
                    if (!this.ConValid())
                    {
                        MessageManager.ShowMsgBox("INP-431431");
                        return;
                    }
                }
                this.GetSelectReason();
                this.SelectInfor.Clear();
                this.SelectInfor.Add(this.InvCodeEdit.Text.Trim());
                string str = this.InvNumEdit.Text.Trim();
                if ((str.Length < 8) && !string.IsNullOrEmpty(str))
                {
                    int length = str.Length;
                    for (int i = 0; i < (8 - length); i++)
                    {
                        str = "0" + str;
                    }
                }
                this.SelectInfor.Add(str);
                this.SelectInfor.Add((this.InvKindCombo.SelectedValue.ToString() == "货物运输业增值税专用发票") ? "f" : "c");
                this.SelectInfor.Add(this.Reasons);
                this.SelectInfor.Add(this.DraweeRB.Checked ? "0" : "1");
                try
                {
                    HySqdTianKai kai = new HySqdTianKai();
                    kai.TabText=("开具红字货物运输业增值税专用发票信息表");
                    kai.InitSqdMx(InitSqdMxType.Add, this.SelectInfor);
                    if (!kai.BlueInvErr)
                    {
                        kai.Show(FormMain.control_0);
                    }
                }
                catch (BaseException exception)
                {
                    ExceptionHandler.HandleError(exception);
                }
                catch (Exception exception2)
                {
                    if (exception2.Message.Equals("BlueRateRevoked"))
                    {
                        MessageManager.ShowMsgBox("INP-431404");
                    }
                    else
                    {
                        ExceptionHandler.HandleError(exception2);
                    }
                }
                base.Close();
            }
        }

        private void Carrier_MistakeRB_Click(object sender, EventArgs e)
        {
            this.InvCodeEdit.Enabled = true;
            this.InvNumEdit.Enabled = true;
            this.BtnOK.Enabled = false;
        }

        private void CarrierRB_Click(object sender, EventArgs e)
        {
            if (this.CarrierRB.Checked)
            {
                this.CarrierPanel.Enabled = true;
                this.Carrier_MistakeRejectRB.Enabled = true;
                this.Carrier_MistakeNonDeliverRB.Enabled = true;
                this.DraweePanel.Enabled = false;
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
                this.BtnOK.Enabled = false;
                this.InvCodeEdit.Text = "";
                this.InvNumEdit.Text = "";
                this.InvCodeEdit.Enabled = false;
                this.InvNumEdit.Enabled = false;
                this.BtnNext.Enabled = false;
            }
        }

        private bool ConValid()
        {
            try
            {
                string str = "";
                int num = 0;
                if ((this.InvoiceKind == "s") || (this.InvoiceKind == "f"))
                {
                    str = this.InvCodeEdit.Text.Trim();
                    num = 10;
                    if ((str.Length < num) || (str == ""))
                    {
                        return false;
                    }
                }
                if (((this.InvNumEdit.Text == "") || (Convert.ToDouble(this.InvNumEdit.Text) <= 0.0)) || (this.InvNumEdit.Text.Trim().Length > 8))
                {
                    return false;
                }
                if (str == "0000000000")
                {
                    return false;
                }
                int length = str.Length;
                if (this.CarrierRB.Checked)
                {
                    this.BtnNext.Enabled = true;
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

        private void DraweeRB_Click(object sender, EventArgs e)
        {
            if (this.DraweeRB.Checked)
            {
                this.CarrierPanel.Enabled = false;
                this.Carrier_MistakeRejectRB.Enabled = false;
                this.Carrier_MistakeNonDeliverRB.Enabled = false;
                this.Carrier_MistakeRejectRB.Checked = false;
                this.Carrier_MistakeNonDeliverRB.Checked = false;
                this.DraweePanel.Enabled = true;
                this.WDKPanel.Enabled = true;
                this.YDKRB.Enabled = true;
                this.WDKRB.Enabled = true;
                this.RenZRB.Enabled = true;
                this.TaxcodeRB.Enabled = true;
                this.InvCodeNoRB.Enabled = true;
                this.GoodsRB.Enabled = true;
                this.InvCodeEdit.Text = "";
                this.InvNumEdit.Text = "";
                this.InvCodeEdit.Enabled = false;
                this.InvNumEdit.Enabled = false;
                this.BtnOK.Enabled = false;
            }
        }

        private void FindInv(string InvKind, string InvType, string InvNum)
        {
            this.csdgBlueInvInfo.Visible = false;
            object[] objArray = new object[] { InvKind, InvType, Convert.ToInt32(InvNum) };
            object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPXX", objArray);
            if (objArray2 != null)
            {
                Fpxx fp = objArray2[0] as Fpxx;
                if (fp != null)
                {
                    double amount = 0.0;
                    amount = Convert.ToDouble(fp.je);
                    DateTime time = DateTime.Parse(fp.kprq);
                    DateTime cardClock = base.TaxCardInstance.GetCardClock();
                    if ((amount <= 0.0) || fp.zfbz)
                    {
                        string str = "";
                        if (amount < 0.0)
                        {
                            str = "发票为负数发票";
                        }
                        else if (fp.zfbz)
                        {
                            str = "发票已作废";
                        }
                        this.lblInfo.Text = "本张发票不可以开红字发票！\r\n原因：" + str;
                        this.BtnOK.Enabled = false;
                    }
                    else if (!string.IsNullOrEmpty(fp.kprq) && (time.AddDays(180.0) < cardClock.Date))
                    {
                        this.lblInfo.Text = "本张发票不能开具红字发票信息表！\r\n原因：对应的蓝字货物运输业增值税专用发票超过认证期限";
                        this.BtnOK.Enabled = false;
                    }
                    else
                    {
                        this.lblInfo.Text = "本张发票可以开红字发票！";
                        this.BtnOK.Enabled = true;
                    }
                    this.BlueInvInfoGridShow(fp, InvKind, amount);
                }
                else
                {
                    this.lblInfo.Text = "本张发票可以开红字发票！\n但在当前库没有找到相应信息。";
                    this.BtnOK.Enabled = true;
                }
            }
        }

        private bool GetSelectReason()
        {
            bool flag = true;
            try
            {
                if (this.DraweeRB.Checked)
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
                if (this.CarrierRB.Checked)
                {
                    this.Reasons = this.Reasons + "1";
                    if (this.Carrier_MistakeRejectRB.Checked)
                    {
                        this.Reasons = this.Reasons + "1";
                    }
                    else
                    {
                        this.Reasons = this.Reasons + "0";
                    }
                    if (this.Carrier_MistakeNonDeliverRB.Checked)
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
            this.csdgBlueInvInfo.GridStyle=((CustomStyle)1);
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

        private void HySqdInfoSelect_Paint(object sender, PaintEventArgs e)
        {
        }

        private void HySqdInfoSelect_Resize(object sender, EventArgs e)
        {
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.DraweeRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ");
            this.CarrierRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_SellerSQ");
            this.YDKRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Ydk");
            this.WDKRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk");
            this.Carrier_MistakeRejectRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_SellerSQ_1");
            this.Carrier_MistakeNonDeliverRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_SellerSQ_2");
            this.RenZRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_1");
            this.TaxcodeRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_2");
            this.InvCodeNoRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_3");
            this.GoodsRB = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_4");
            this.RenZRB.Enabled = false;
            this.TaxcodeRB.Enabled = false;
            this.InvCodeNoRB.Enabled = false;
            this.GoodsRB.Enabled = false;
            this.Carrier_MistakeRejectRB.Enabled = false;
            this.Carrier_MistakeNonDeliverRB.Enabled = false;
            this.DraweeRB.Click += new EventHandler(this.DraweeRB_Click);
            this.CarrierRB.Click += new EventHandler(this.CarrierRB_Click);
            this.YDKRB.Click += new EventHandler(this.YDKRB_Click);
            this.WDKRB.Click += new EventHandler(this.WDKRB_Click);
            this.Carrier_MistakeRejectRB.Click += new EventHandler(this.Carrier_MistakeRB_Click);
            this.Carrier_MistakeNonDeliverRB.Click += new EventHandler(this.Carrier_MistakeRB_Click);
            this.RenZRB.Click += new EventHandler(this.WDK_SubItem_Click);
            this.TaxcodeRB.Click += new EventHandler(this.WDK_SubItem_Click);
            this.InvCodeNoRB.Click += new EventHandler(this.WDK_SubItem_Click);
            this.GoodsRB.Click += new EventHandler(this.WDK_SubItem_Click);
            this.InvKindCombo = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("txt_fpzl");
            this.BtnNext = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_next");
            this.BtnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
            this.BtnClose = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
            this.InvNumEdit = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("txt_fphm");
            this.InvCodeEdit = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("txt_fpdm");
            this.InvCodeEdit.KeyPress += new KeyPressEventHandler(this.InvCodeEdit_KeyPress);
            this.InvCodeEdit.MaxLength = 10;
            this.InvNumEdit.KeyPress += new KeyPressEventHandler(this.InvNumEdit_KeyPress);
            this.InvNumEdit.MaxLength = 8;
            this.DraweePanel = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.CarrierPanel = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.WDKPanel = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1_2");
            this.panel3 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel3");
            this.panel4 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel4");
            this.lblInfo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblInfo");
            this.csdgBlueInvInfo = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1");
            this.GridShowHeaderInit();
            this.BtnClose.Click += new EventHandler(this.btn_Close_Click);
            this.BtnNext.Click += new EventHandler(this.btn_Next_Click);
            this.BtnOK.Click += new EventHandler(this.btn_Ok_Click);
            this.InvCodeEdit.TextChanged += new EventHandler(this.txt_fphm_TextChanged);
            this.InvNumEdit.TextChanged += new EventHandler(this.txt_fphm_TextChanged);
            this.panel5 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel5");
            base.Paint += new PaintEventHandler(this.HySqdInfoSelect_Paint);
            base.Resize += new EventHandler(this.HySqdInfoSelect_Resize);
            this.panel5.BorderStyle = BorderStyle.Fixed3D;
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(HySqdInfoSelect));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(450, 450);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Text = "红字货物运输业增值税专用发票信息表信息选择";
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.HzfpHy.Form.HySqdInfoSelect\Aisino.Fwkp.HzfpHy.Form.HySqdInfoSelect.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(450, 450);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "HySqdInfoSelect";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "红字货物运输业增值税专用发票信息表信息选择";
            base.ResumeLayout(false);
        }

        private void InvCodeEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
            else if ((ToolUtil.GetBytes(this.InvCodeEdit.Text).Length >= 10) && (this.InvCodeEdit.SelectedText.Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void InvNumEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
            else if ((ToolUtil.GetBytes(this.InvNumEdit.Text).Length >= 8) && (this.InvNumEdit.SelectedText.Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void txt_fphm_TextChanged(object sender, EventArgs e)
        {
            if (this.DraweeRB.Checked)
            {
                this.BtnNext.Enabled = false;
                this.BtnOK.Enabled = true;
            }
            if (this.CarrierRB.Checked)
            {
                this.BtnNext.Enabled = true;
                this.BtnOK.Enabled = false;
            }
        }

        private void WDK_SubItem_Click(object sender, EventArgs e)
        {
            this.WDKRB.Checked = true;
            this.InvCodeEdit.Enabled = true;
            this.InvNumEdit.Enabled = true;
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
                this.InvNumEdit.Text = "";
                this.BtnOK.Enabled = false;
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
                this.InvNumEdit.Enabled = false;
                this.RenZRB.Checked = false;
                this.TaxcodeRB.Checked = false;
                this.InvCodeNoRB.Checked = false;
                this.GoodsRB.Checked = false;
                this.BtnOK.Enabled = true;
                this.BtnNext.Enabled = false;
                this.InvCodeEdit.Text = "";
                this.InvNumEdit.Text = "";
            }
        }
    }
}

