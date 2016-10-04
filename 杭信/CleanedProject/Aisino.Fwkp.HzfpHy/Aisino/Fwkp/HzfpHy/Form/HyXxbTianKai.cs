namespace Aisino.Fwkp.HzfpHy.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class HyXxbTianKai : DockForm
    {
        private AisinoCMB cmb_Rate;
        private IContainer components;
        private CustomStyleDataGrid csdg_Item;
        private AisinoLBL lbl_BlueInveCode;
        private AisinoLBL lbl_BlueInveNumber;
        private AisinoLBL lbl_BlueInveType;
        private AisinoLBL lbl_Date;
        private AisinoLBL lbl_JBR;
        private AisinoLBL lbl_Machine;
        private AisinoLBL lbl_No;
        private AisinoLBL lbl_Price;
        private AisinoLBL lbl_Tax;
        private AisinoLBL lbl_Title;
        private AisinoMultiCombox mcmb_CarrierName;
        private AisinoMultiCombox mcmb_CarrierNumber;
        private AisinoMultiCombox mcmb_DraweeName;
        private AisinoMultiCombox mcmb_DraweeNumber;
        private AisinoMultiCombox mcmb_ReceiverName;
        private AisinoMultiCombox mcmb_ReceiverNumber;
        private AisinoMultiCombox mcmb_ShipperName;
        private AisinoMultiCombox mcmb_ShipperNumber;
        private AisinoPNL pnl_Reason;
        private AisinoPNL pnl_ReasonOverDate;
        private AisinoRDO rbt_Carrier;
        private AisinoRDO rbt_CReason1;
        private AisinoRDO rbt_CReason2;
        private AisinoRDO rbt_Deduated;
        private AisinoRDO rbt_Drawee;
        private AisinoRDO rbt_NDReason1;
        private AisinoRDO rbt_NDReason2;
        private AisinoRDO rbt_NDReason3;
        private AisinoRDO rbt_NDReason4;
        private AisinoRDO rbt_NotDeduated;
        private AisinoRDO rbt_OverDateReason1;
        private AisinoRDO rbt_OverDateReason2;
        private AisinoRDO rbt_OverDateReason3;
        private AisinoRDO rbt_OverDateReason4;
        private AisinoRDO rbt_OverDateReason5;
        private AisinoRDO rbt_OverDateReason6;
        private AisinoRDO rbt_OverDateReason7;
        private AisinoRDO rbt_OverDateReason8;
        private AisinoRTX rtxt_Cargo;
        private ToolStripButton tool_Close;
        private ToolStripButton tool_Edit;
        private ToolStripButton tool_Price;
        private ToolStripButton tool_Print;
        private ToolStripButton tool_RowAdd;
        private ToolStripButton tool_RowDelete;
        private ToolStrip toolStrip;
        private AisinoTXT txt_CargoLoad;
        private AisinoTXT txt_CargoNumber;
        private XmlComponentLoader xmlComponentLoader1;

        public HyXxbTianKai()
        {
            this.Initialize();
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
            this.toolStrip = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip");
            this.tool_Close = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Close");
            this.tool_Print = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Print");
            this.tool_Edit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Edit");
            this.tool_Price = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Price");
            this.tool_RowAdd = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_RowAdd");
            this.tool_RowDelete = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_RowDelete");
            this.lbl_Title = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblTitle");
            this.lbl_Date = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblDate");
            this.lbl_JBR = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJBR");
            this.lbl_No = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblNo");
            this.mcmb_CarrierName = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("mcmbCarrierName");
            this.mcmb_CarrierNumber = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("mcmbCarrierNumber");
            this.mcmb_DraweeName = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("mcmbDraweeName");
            this.mcmb_DraweeNumber = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("mcmbDraweeNumber");
            this.mcmb_ReceiverName = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("mcmbReceiverName");
            this.mcmb_ReceiverNumber = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("mcmbReceiverNumber");
            this.mcmb_ShipperName = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("mcmbShipperName");
            this.mcmb_ShipperNumber = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("mcmbShipperNumber");
            this.csdg_Item = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("csdgItem");
            this.rtxt_Cargo = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("rtxtCargo");
            this.lbl_Price = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblPrice");
            this.cmb_Rate = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbRate");
            this.lbl_Tax = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblTax");
            this.lbl_Machine = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblMachine");
            this.txt_CargoNumber = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCargoNumber");
            this.txt_CargoLoad = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCargoLoad");
            this.pnl_Reason = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnlReason");
            this.rbt_Drawee = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtDrawee");
            this.rbt_Deduated = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtDeduated");
            this.rbt_NotDeduated = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtNotDeduated");
            this.rbt_NDReason1 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtNDReason1");
            this.rbt_NDReason2 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtNDReason2");
            this.rbt_NDReason3 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtNDReason3");
            this.rbt_NDReason4 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtNDReason4");
            this.rbt_Carrier = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtCarrier");
            this.rbt_CReason1 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtCReason1");
            this.rbt_CReason2 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtCReason2");
            this.pnl_ReasonOverDate = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnlReasonOverDate");
            this.rbt_OverDateReason1 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtOverDateReason1");
            this.rbt_OverDateReason2 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtOverDateReason2");
            this.rbt_OverDateReason3 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtOverDateReason3");
            this.rbt_OverDateReason4 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtOverDateReason4");
            this.rbt_OverDateReason5 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtOverDateReason5");
            this.rbt_OverDateReason6 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtOverDateReason6");
            this.rbt_OverDateReason7 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtOverDateReason7");
            this.rbt_OverDateReason8 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtOverDateReason8");
            this.lbl_BlueInveType = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBlueInveType");
            this.lbl_BlueInveNumber = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBlueInveNumber");
            this.lbl_BlueInveCode = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBlueInveCode");
            this.tool_Close.Margin = new Padding(20, 1, 0, 2);
            ControlStyleUtil.SetToolStripStyle(this.toolStrip);
            this.mcmb_DraweeNumber.IsSelectAll=(true);
            this.mcmb_DraweeNumber.buttonStyle=(0);
            this.mcmb_DraweeNumber.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.mcmb_DraweeNumber.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.mcmb_DraweeNumber.Width - 140));
            this.mcmb_DraweeNumber.ShowText=("SH");
            this.mcmb_DraweeNumber.DrawHead=(false);
            this.mcmb_DraweeName.IsSelectAll=(true);
            this.mcmb_DraweeName.buttonStyle=(0);
            this.mcmb_DraweeName.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.mcmb_DraweeName.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.mcmb_DraweeName.Width - 140));
            this.mcmb_DraweeName.ShowText=("MC");
            this.mcmb_DraweeName.DrawHead=(false);
            this.mcmb_CarrierNumber.IsSelectAll=(true);
            this.mcmb_CarrierNumber.buttonStyle=(0);
            this.mcmb_CarrierNumber.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.mcmb_CarrierNumber.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.mcmb_CarrierNumber.Width - 140));
            this.mcmb_CarrierNumber.ShowText=("SH");
            this.mcmb_CarrierNumber.DrawHead=(false);
            this.mcmb_CarrierName.IsSelectAll=(true);
            this.mcmb_CarrierName.buttonStyle=(0);
            this.mcmb_CarrierName.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.mcmb_CarrierName.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.mcmb_CarrierName.Width - 140));
            this.mcmb_CarrierName.ShowText=("MC");
            this.mcmb_CarrierName.DrawHead=(false);
            this.mcmb_ReceiverNumber.IsSelectAll=(true);
            this.mcmb_ReceiverNumber.buttonStyle=(0);
            this.mcmb_ReceiverNumber.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.mcmb_ReceiverNumber.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.mcmb_ReceiverNumber.Width - 140));
            this.mcmb_ReceiverNumber.ShowText=("SH");
            this.mcmb_ReceiverNumber.DrawHead=(false);
            this.mcmb_ReceiverName.IsSelectAll=(true);
            this.mcmb_ReceiverName.buttonStyle=(0);
            this.mcmb_ReceiverName.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.mcmb_ReceiverName.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.mcmb_ReceiverName.Width - 140));
            this.mcmb_ReceiverName.ShowText=("MC");
            this.mcmb_ReceiverName.DrawHead=(false);
            this.mcmb_ShipperNumber.IsSelectAll=(true);
            this.mcmb_ShipperNumber.buttonStyle=(0);
            this.mcmb_ShipperNumber.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.mcmb_ShipperNumber.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.mcmb_ShipperNumber.Width - 140));
            this.mcmb_ShipperNumber.ShowText=("SH");
            this.mcmb_ShipperNumber.DrawHead=(false);
            this.mcmb_ShipperName.IsSelectAll=(true);
            this.mcmb_ShipperName.buttonStyle=(0);
            this.mcmb_ShipperName.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.mcmb_ShipperName.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.mcmb_ShipperName.Width - 140));
            this.mcmb_ShipperName.ShowText=("MC");
            this.mcmb_ShipperName.DrawHead=(false);
            this.csdg_Item.AllowUserToAddRows = false;
            this.csdg_Item.AllowUserToDeleteRows = false;
            this.csdg_Item.AllowUserToResizeRows=(false);
            this.csdg_Item.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.csdg_Item.GridStyle=((CustomStyle)1);
            this.pnl_Reason.Visible = false;
            this.pnl_ReasonOverDate.Visible = true;
            this.rbt_Drawee.Enabled = false;
            this.rbt_Deduated.Enabled = false;
            this.rbt_NotDeduated.Enabled = false;
            this.rbt_NDReason1.Enabled = false;
            this.rbt_NDReason2.Enabled = false;
            this.rbt_NDReason3.Enabled = false;
            this.rbt_NDReason4.Enabled = false;
            this.rbt_Carrier.Enabled = false;
            this.rbt_CReason1.Enabled = false;
            this.rbt_CReason2.Enabled = false;
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(HyXxbTianKai));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.AutoScroll = true;
            this.xmlComponentLoader1.AutoScrollMinSize = new Size(980, 0x22b);
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x198, 0x153);
            this.xmlComponentLoader1.TabIndex = 2;
            this.xmlComponentLoader1.Text = "开具红字货物运输业增值税专用发票信息表";
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.HzfpHy.Form.HyXxbTianKai_Split\Aisino.Fwkp.HzfpHy.Form.HyXxbTianKai_Split.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x198, 0x153);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "HyXxbTianKai";
            base.TabText=("HyXxbTianKai");
            this.Text = "HyXxbTianKai";
            base.ResumeLayout(false);
        }
    }
}

