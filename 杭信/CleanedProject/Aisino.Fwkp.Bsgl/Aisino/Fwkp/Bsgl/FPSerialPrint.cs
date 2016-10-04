namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FPSerialPrint : BaseForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private bool checkCause1;
        private bool checkCause2;
        private bool checkCause3;
        private bool checkCause4;
        private bool checkCause5;
        private bool checkCause6;
        private AisinoCHK chkDZFP;
        private AisinoCHK chkFPTotal;
        private AisinoCHK chkHWYS;
        private AisinoCHK chkJDC;
        private AisinoCHK chkJSFP;
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
        private AisinoCMB cmbXZSQ5;
        private AisinoCMB cmbXZSQ6;
        private AisinoCMB cmbXZYF1;
        private AisinoCMB cmbXZYF2;
        private AisinoCMB cmbXZYF3;
        private AisinoCMB cmbXZYF4;
        private AisinoCMB cmbXZYF5;
        private AisinoCMB cmbXZYF6;
        private IContainer components;
        private AisinoGRP groupBox3;
        private AisinoGRP groupBoxTJXZ;
        private bool isInit;
        private AisinoLBL lableXZSQ;
        private AisinoLBL lableXZYF;
        private AisinoLBL lableXZZL;
        private string lastRepDateDZFP = "";
        private string lastRepDateHY = "";
        private string lastRepDateJDC = "";
        private string lastRepDateJSFP = "";
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
        private TaxcardEntityBLL taxcardEntityBLL = new TaxcardEntityBLL();
        private XmlComponentLoader xmlComponentLoader1;

        public FPSerialPrint()
        {
            this.InitializeComponent();
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
                if (this.m_InvTypeEntityList.Count <= 0)
                {
                    MessageManager.ShowMsgBox("INP-251303", new string[] { "获取发票种类失败" });
                }
                else if (((!this.chkZZSZYFP.Checked && !this.chkZZSPTFP.Checked) && (!this.chkHWYS.Checked && !this.chkJDC.Checked)) && (!this.chkDZFP.Checked && !this.chkJSFP.Checked))
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
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        private void chkDZFP_CheckedChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                this.checkCause5 = true;
                if (!this.chkDZFP.Checked)
                {
                    this.cmbXZYF5.SelectedIndex = -1;
                    this.cmbXZSQ5.SelectedIndex = -1;
                }
                else
                {
                    this.cmbXZYF5.SelectedIndex = this.cmbXZYF5.Items.Count - 1;
                    this.cmbXZSQ5.SelectedIndex = this.cmbXZSQ5.Items.Count - 1;
                }
                this.cmbXZYF5.Enabled = this.chkDZFP.Checked;
                this.cmbXZSQ5.Enabled = this.chkDZFP.Checked;
                this.checkCause5 = false;
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
                this.cmbXZYF3.Enabled = this.chkHWYS.Checked;
                this.cmbXZSQ3.Enabled = this.chkHWYS.Checked;
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
                this.cmbXZYF4.Enabled = this.chkJDC.Checked;
                this.cmbXZSQ4.Enabled = this.chkJDC.Checked;
                this.checkCause4 = false;
            }
        }

        private void chkJSFP_CheckedChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                this.checkCause6 = true;
                if (!this.chkJSFP.Checked)
                {
                    this.cmbXZYF6.SelectedIndex = -1;
                    this.cmbXZSQ6.SelectedIndex = -1;
                }
                else
                {
                    this.cmbXZYF6.SelectedIndex = this.cmbXZYF6.Items.Count - 1;
                    this.cmbXZSQ6.SelectedIndex = this.cmbXZSQ6.Items.Count - 1;
                }
                this.cmbXZYF6.Enabled = this.chkJSFP.Checked;
                this.cmbXZSQ6.Enabled = this.chkJSFP.Checked;
                this.checkCause6 = false;
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
                this.cmbXZYF2.Enabled = this.chkZZSPTFP.Checked;
                this.cmbXZSQ2.Enabled = this.chkZZSPTFP.Checked;
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
                this.cmbXZYF1.Enabled = this.chkZZSZYFP.Checked;
                this.cmbXZSQ1.Enabled = this.chkZZSZYFP.Checked;
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

        private void cmbXZYF5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                if (!this.checkCause5)
                {
                    this.InitialPeriod_(this.cmbXZYF5, this.cmbXZSQ5, 0x33);
                }
                this.cmbXZSQ5.SelectedIndex = this.cmbXZSQ5.Items.Count - 1;
            }
        }

        private void cmbXZYF6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.isInit)
            {
                if (!this.checkCause6)
                {
                    this.InitialPeriod_(this.cmbXZYF6, this.cmbXZSQ6, 0x29);
                }
                this.cmbXZSQ6.SelectedIndex = this.cmbXZSQ6.Items.Count - 1;
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
                this.chkDZFP_CheckedChanged(null, null);
                this.chkHWYS_CheckedChanged(null, null);
                this.chkJDC_CheckedChanged(null, null);
                this.chkZZSPTFP_CheckedChanged(null, null);
                this.chkZZSZYFP_CheckedChanged(null, null);
                this.chkJSFP_CheckedChanged(null, null);
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
            if (this.chkDZFP.Checked)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_PTDZ,
                    m_strInvName = "电子增值税普通发票"
                };
                list.Add(item);
            }
            if (this.chkJSFP.Checked)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_JSFP,
                    m_strInvName = "增值税普通发票(卷票)"
                };
                list.Add(item);
            }
            return list;
        }

        private void Initial()
        {
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Load += new EventHandler(this.FPQuery_Load);
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
            this.cmbXZSQ6 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZSQ6");
            this.cmbXZSQ5 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZSQ5");
            this.cmbXZSQ4 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZSQ4");
            this.cmbXZSQ3 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZSQ3");
            this.cmbXZSQ2 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZSQ2");
            this.cmbXZYF6 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZYF6");
            this.cmbXZYF5 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZYF5");
            this.cmbXZYF4 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZYF4");
            this.cmbXZYF3 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZYF3");
            this.cmbXZYF2 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZYF2");
            this.chkJSFP = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkJSFP");
            this.chkJDC = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkJDC");
            this.chkHWYS = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkHWYS");
            this.chkDZFP = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkDZFP");
            this.chkZZSPTFP = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkZZSPTFP");
            this.chkZZSZYFP = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkZZSZYFP");
            this.cmbXZSQ1 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZSQ1");
            this.cmbXZYF1 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbXZYF1");
            this.lableXZSQ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableXZSQ");
            this.lableXZYF = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableXZYF");
            this.lableXZZL = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableXZZL");
            this.cmbXZSQ1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZSQ2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZSQ3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZSQ4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZSQ5.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZSQ6.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZYF1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZYF2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZYF3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZYF4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZYF5.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbXZYF6.DropDownStyle = ComboBoxStyle.DropDownList;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.chkZZSPTFP.CheckedChanged += new EventHandler(this.chkZZSPTFP_CheckedChanged);
            this.chkZZSZYFP.CheckedChanged += new EventHandler(this.chkZZSZYFP_CheckedChanged);
            this.chkHWYS.CheckedChanged += new EventHandler(this.chkHWYS_CheckedChanged);
            this.chkJDC.CheckedChanged += new EventHandler(this.chkJDC_CheckedChanged);
            this.chkDZFP.CheckedChanged += new EventHandler(this.chkDZFP_CheckedChanged);
            this.chkJSFP.CheckedChanged += new EventHandler(this.chkJSFP_CheckedChanged);
            this.cmbXZYF1.SelectedIndexChanged += new EventHandler(this.cmbXZYF1_SelectedIndexChanged);
            this.cmbXZYF2.SelectedIndexChanged += new EventHandler(this.cmbXZYF2_SelectedIndexChanged);
            this.cmbXZYF3.SelectedIndexChanged += new EventHandler(this.cmbXZYF3_SelectedIndexChanged);
            this.cmbXZYF4.SelectedIndexChanged += new EventHandler(this.cmbXZYF4_SelectedIndexChanged);
            this.cmbXZYF5.SelectedIndexChanged += new EventHandler(this.cmbXZYF5_SelectedIndexChanged);
            this.cmbXZYF6.SelectedIndexChanged += new EventHandler(this.cmbXZYF6_SelectedIndexChanged);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Size = new Size(0x1de, 0x1e8);
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
                if (info2.InvType == 0x33)
                {
                    this.lastRepDateDZFP = info2.LastRepDate;
                }
                if (info2.InvType == 0x29)
                {
                    this.lastRepDateJSFP = info2.LastRepDate;
                }
            }
            this.LoadInvTypePrint();
            this.InitialMonthPrint();
            this.InitialPeriodprint();
            this.cmbXZSQ1.SelectedIndex = -1;
            this.cmbXZSQ2.SelectedIndex = -1;
            this.cmbXZSQ3.SelectedIndex = -1;
            this.cmbXZSQ4.SelectedIndex = -1;
            this.cmbXZSQ5.SelectedIndex = -1;
            this.cmbXZSQ6.SelectedIndex = -1;
            this.cmbXZYF1.SelectedIndex = -1;
            this.cmbXZYF2.SelectedIndex = -1;
            this.cmbXZYF3.SelectedIndex = -1;
            this.cmbXZYF4.SelectedIndex = -1;
            this.cmbXZYF5.SelectedIndex = -1;
            this.cmbXZYF6.SelectedIndex = -1;
            this.isInit = true;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FPSerialPrint));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1ce, 0x1a6);
            this.xmlComponentLoader1.TabIndex = 1;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.FPSerialPrint\Aisino.Fwkp.Bsgl.FPSerialPrint.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1ce, 0x1a6);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FPSerialPrint";
            this.Text = "连续打印";
            base.ResumeLayout(false);
            this.Initial();
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
            else if (fpzl == 0x33)
            {
                dictionary = this.taxcardEntityBLL.getYearMonthDZFP();
            }
            else if (fpzl == 0x29)
            {
                dictionary = this.taxcardEntityBLL.getYearMonthJSFP();
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
            if (this.chkDZFP.Enabled)
            {
                this.InitialMonth_(this.cmbXZYF5, 0x33);
            }
            if (this.chkJSFP.Enabled)
            {
                this.InitialMonth_(this.cmbXZYF6, 0x29);
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
                    if (invType == 0x33)
                    {
                        string lastRepDateDZFP = this.lastRepDateDZFP;
                        int num11 = -1;
                        int num12 = -1;
                        if ((lastRepDateDZFP.Length > 0) && lastRepDateDZFP.Contains("-"))
                        {
                            num12 = int.Parse(lastRepDateDZFP.Split(new char[] { '-' })[0]);
                            num11 = int.Parse(lastRepDateDZFP.Split(new char[] { '-' })[1]);
                        }
                        DateTime time5 = new DateTime(num12, num11, 1);
                        if (DateTime.Compare(time5, time) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(0x33)[1];
                        }
                        else if (DateTime.Compare(time, time5.AddMonths(-1)) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(0x33)[0];
                        }
                        else
                        {
                            num4 = 0;
                        }
                    }
                    if (invType == 0x29)
                    {
                        string lastRepDateJSFP = this.lastRepDateJSFP;
                        int num13 = -1;
                        int num14 = -1;
                        if ((lastRepDateJSFP.Length > 0) && lastRepDateJSFP.Contains("-"))
                        {
                            num14 = int.Parse(lastRepDateJSFP.Split(new char[] { '-' })[0]);
                            num13 = int.Parse(lastRepDateJSFP.Split(new char[] { '-' })[1]);
                        }
                        DateTime time6 = new DateTime(num14, num13, 1);
                        if (DateTime.Compare(time6, time) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(0x29)[1];
                        }
                        else if (DateTime.Compare(time, time6.AddMonths(-1)) == 0)
                        {
                            num4 = base.TaxCardInstance.GetPeriodCount(0x29)[0];
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
                            int num23 = i + 1;
                            cboShuiqi.Items.Add("第" + num23.ToString() + "期");
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
                        string str9 = currentMonth.Substring(0, length);
                        string str10 = currentMonth.Substring(length + 1);
                        int num17 = Convert.ToInt32(str9);
                        int num18 = Convert.ToInt32(str10);
                        if ((year == num17) && (month == num18))
                        {
                            int currentRepPeriod = this.queryPrintBLL.GetCurrentRepPeriod();
                            if (currentRepPeriod > 0)
                            {
                                for (int j = 0; j < currentRepPeriod; j++)
                                {
                                    int num24 = j + 1;
                                    cboShuiqi.Items.Add("第" + num24.ToString() + "期");
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
                        else if (((year == num17) && ((month + 1) == num18)) || ((((year + 1) == num17) && (month == 12)) && (num18 == 1)))
                        {
                            int lastRepPeroid = this.queryPrintBLL.GetLastRepPeroid();
                            if (lastRepPeroid > 0)
                            {
                                for (int k = 0; k < lastRepPeroid; k++)
                                {
                                    int num25 = k + 1;
                                    cboShuiqi.Items.Add("第" + num25.ToString() + "期");
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
            if (this.chkDZFP.Enabled)
            {
                this.InitialPeriod_(this.cmbXZYF5, this.cmbXZSQ5, 0x33);
            }
            if (this.chkJSFP.Enabled)
            {
                this.InitialPeriod_(this.cmbXZYF6, this.cmbXZSQ6, 0x29);
            }
        }

        private void LoadInvTypePrint()
        {
            this.chkZZSZYFP.Enabled = false;
            this.chkZZSPTFP.Enabled = false;
            this.chkHWYS.Enabled = false;
            this.chkJDC.Enabled = false;
            this.chkDZFP.Enabled = false;
            this.chkJSFP.Enabled = false;
            this.cmbXZSQ1.Enabled = false;
            this.cmbXZSQ2.Enabled = false;
            this.cmbXZSQ3.Enabled = false;
            this.cmbXZSQ4.Enabled = false;
            this.cmbXZSQ5.Enabled = false;
            this.cmbXZSQ6.Enabled = false;
            this.cmbXZYF1.Enabled = false;
            this.cmbXZYF2.Enabled = false;
            this.cmbXZYF3.Enabled = false;
            this.cmbXZYF4.Enabled = false;
            this.cmbXZYF5.Enabled = false;
            this.cmbXZYF6.Enabled = false;
            this.m_InvTypeEntityList = this.m_commFun.GetInvTypeCollect();
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
                if (entity.m_invType == INV_TYPE.INV_PTDZ)
                {
                    this.chkDZFP.Enabled = true;
                    this.cmbXZSQ5.Enabled = true;
                    this.cmbXZYF5.Enabled = true;
                }
                if (entity.m_invType == INV_TYPE.INV_JSFP)
                {
                    this.chkJSFP.Enabled = true;
                    this.cmbXZSQ6.Enabled = true;
                    this.cmbXZYF6.Enabled = true;
                }
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
                    if (item.m_InvTypeEntity.m_invType == INV_TYPE.INV_PTDZ)
                    {
                        this.setQueryPrintEntity(queryPrintEntity, this.cmbXZYF5, this.cmbXZSQ5);
                    }
                    if (item.m_InvTypeEntity.m_invType == INV_TYPE.INV_JSFP)
                    {
                        this.setQueryPrintEntity(queryPrintEntity, this.cmbXZYF6, this.cmbXZSQ6);
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
                bool flag2 = false;
                if (this.m_InvTypeEntityListPrint.Count > 1)
                {
                    flag2 = true;
                }
                string str = "";
                bool flag3 = false;
                for (int j = 0; j < this.m_InvTypeEntityListPrint.Count; j++)
                {
                    if (flag3)
                    {
                        break;
                    }
                    if (this.m_PrintEntityList[j].m_ItemEntity.Count > 1)
                    {
                        flag2 = true;
                    }
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
                            if (!flag)
                            {
                                MessageHelper.MsgWait("正在打印：" + this.m_QueryPrintEntityList[j].m_strSubItem + "--" + this.m_QueryPrintEntityList[j].m_strItemDetail);
                            }
                            InvStatForm form = new InvStatForm(this.m_QueryPrintEntityList[j]);
                            try
                            {
                                if (form.PrintTableSerial(flag, flag2, "正在打印：" + this.m_QueryPrintEntityList[j].m_strSubItem + "--" + this.m_QueryPrintEntityList[j].m_strItemDetail))
                                {
                                    string str3 = this.m_QueryPrintEntityList[j].m_strSubItem + "-" + this.m_QueryPrintEntityList[j].m_strItemDetail + "：打印成功！";
                                    str = str + str3 + "\r\n";
                                }
                                else
                                {
                                    string str4 = this.m_QueryPrintEntityList[j].m_strSubItem + "-" + this.m_QueryPrintEntityList[j].m_strItemDetail + "：打印失败或者取消打印！";
                                    str = str + str4 + "\r\n";
                                }
                                goto Label_06AE;
                            }
                            catch (Exception exception)
                            {
                                if (!exception.Message.Equals("用户放弃连续打印"))
                                {
                                    throw exception;
                                }
                                flag3 = true;
                                break;
                            }
                        }
                        this.m_QueryPrintEntityList[j].m_strTitle = strInvName + "明细表";
                        if (!flag)
                        {
                            MessageHelper.MsgWait("正在打印：" + this.m_QueryPrintEntityList[j].m_strSubItem + "--" + this.m_QueryPrintEntityList[j].m_strItemDetail);
                        }
                        InvoiceResultForm form2 = new InvoiceResultForm(this.m_QueryPrintEntityList[j]);
                        try
                        {
                            if (form2.PrintTableSerial(flag, flag2, "正在打印：" + this.m_QueryPrintEntityList[j].m_strSubItem + "--" + this.m_QueryPrintEntityList[j].m_strItemDetail))
                            {
                                string str5 = this.m_QueryPrintEntityList[j].m_strSubItem + ":" + this.m_QueryPrintEntityList[j].m_strItemDetail + ":打印成功！";
                                str = str + str5 + "\r\n";
                            }
                            else
                            {
                                string str6 = this.m_QueryPrintEntityList[j].m_strSubItem + "-" + this.m_QueryPrintEntityList[j].m_strItemDetail + ":打印失败或者取消打印！";
                                str = str + str6 + "\r\n";
                            }
                        }
                        catch (Exception exception2)
                        {
                            if (!exception2.Message.Equals("用户放弃连续打印"))
                            {
                                throw exception2;
                            }
                            flag3 = true;
                            break;
                        }
                    Label_06AE:
                        flag = false;
                    }
                }
                MessageHelper.MsgWait();
                if (!flag3)
                {
                    MessageManager.ShowMsgBox("INP-251303", new string[] { str });
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-251305");
                }
            }
            catch (Exception exception3)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception3.Message });
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

