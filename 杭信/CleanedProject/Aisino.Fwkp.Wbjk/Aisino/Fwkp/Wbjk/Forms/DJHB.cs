namespace Aisino.Fwkp.Wbjk.Forms
{
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
    using System.Windows.Forms;

    public class DJHB : BaseForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoDataGrid aisinoDataGrid2;
        private SaleBillCtrl billBL = SaleBillCtrl.Instance;
        private AisinoBTN btnQuery;
        private AisinoCHK checkBoxHBBZ;
        private AisinoCHK checkBoxKBZ;
        private AisinoCMB comboBoxDJZL;
        private AisinoCMB comboBoxYF;
        private IContainer components = null;
        private DJHBdal djhbBLL = new DJHBdal();
        private string DJmonth = "";
        private string DJtype = "";
        private string GFname = "";
        private string GFsh = "";
        private AisinoGRP groupBox1;
        private AisinoGRP groupBox2;
        private AisinoGRP groupBox3;
        private AisinoGRP groupBox4;
        private AisinoLBL label1;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL labHBJE;
        private AisinoLBL labHBSE;
        private AisinoPNL panel1;
        private AisinoRDO radioButtonComplicate;
        private AisinoRDO radioButtonMC;
        private AisinoRDO radioButtonSH;
        private AisinoRDO radioButtonSimple;
        private AisinoSPL splitContainer1;
        private AisinoTXT textBoxMC;
        private AisinoTXT textBoxSH;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnHB;
        private ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator3;

        public DJHB()
        {
            this.InitializeComponent();
            base.Load += new EventHandler(this.DJHB_Load);
            this.aisinoDataGrid1.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent));
            this.aisinoDataGrid1.add_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
            this.aisinoDataGrid1.add_DataGridCellFormatting(new EventHandler<DataGridViewCellFormattingEventArgs>(this.aisinoDataGrid1_DataGridCellFormatting));
            this.radioButtonMC.CheckedChanged += new EventHandler(this.radioButtonMC_CheckedChanged);
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.toolStripButton1.Click += new EventHandler(this.toolBtnQuit_Click);
            this.toolStripBtnHB.Click += new EventHandler(this.toolBtnHB_Click);
            this.aisinoDataGrid2.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid2_GoToPageEvent));
            this.checkBoxHBBZ.CheckedChanged += new EventHandler(this.checkBoxHBBZ_CheckedChanged);
            this.comboBoxYF.DropDownStyle = ComboBoxStyle.DropDownList;
            if (!this.checkBoxHBBZ.Checked)
            {
                this.checkBoxKBZ.Enabled = false;
            }
            else
            {
                this.checkBoxKBZ.Enabled = true;
            }
            this.textBoxMC.GotFocus += new EventHandler(this.txtDJBH_GotFocus);
            this.textBoxSH.GotFocus += new EventHandler(this.txtDJBH_GotFocus);
            this.aisinoDataGrid1.set_ReadOnly(true);
            this.aisinoDataGrid1.get_DataGrid().AllowUserToDeleteRows = false;
            this.aisinoDataGrid2.set_ReadOnly(true);
            this.aisinoDataGrid2.get_DataGrid().AllowUserToDeleteRows = false;
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
                    else if (name == "DJZL")
                    {
                        e.Value = ShowString.ShowFPZL(e.Value.ToString());
                    }
                    else if (name == "SFZJY")
                    {
                        e.Value = ShowString.ShowBool(e.Value.ToString());
                    }
                    else if (name == "HYSY")
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

        private void aisinoDataGrid1_DataGridRowSelectionChanged(object sender, DataGridRowEventArgs e)
        {
            SaleBill bill = null;
            int num3;
            string xSDJBH = e.get_CurrentRow().Cells["BH"].Value.ToString().Trim();
            this.aisinoDataGrid2.set_DataSource(this.djhbBLL.QueryXSDJMX(xSDJBH));
            double round = 0.0;
            double num2 = 0.0;
            for (num3 = 0; num3 < this.aisinoDataGrid1.get_Rows().Count; num3++)
            {
                xSDJBH = this.aisinoDataGrid1.get_Rows()[num3].Cells["BH"].Value.ToString().Trim();
                if (this.aisinoDataGrid1.get_Rows()[num3].Selected)
                {
                    bill = this.billBL.Find(xSDJBH);
                    round += bill.JEHJ;
                    foreach (Goods goods in bill.ListGoods)
                    {
                        num2 += goods.SE;
                    }
                }
            }
            int count = this.aisinoDataGrid2.get_Rows().Count;
            for (num3 = 0; num3 < count; num3++)
            {
                string str2 = this.aisinoDataGrid2.get_Rows()[num3].Cells["SLV"].Value.ToString();
                string str3 = this.aisinoDataGrid2.get_Rows()[num3].Cells["XH"].Value.ToString();
                if (((str2 != null) && (str2 != "")) && (str2 != "中外合作油气田"))
                {
                    string str4 = this.billBL.ShowSLV(bill, str3, str2);
                    if (str4 != "")
                    {
                        this.aisinoDataGrid2.get_Rows()[num3].Cells["SLV"].Value = str4;
                    }
                }
            }
            round = SaleBillCtrl.GetRound(round, 2);
            num2 = SaleBillCtrl.GetRound(num2, 2);
            this.labHBJE.Text = "合计金额 " + round.ToString("0.00");
            this.labHBSE.Text = "合计税额 " + num2.ToString("0.00");
        }

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.djhbBLL.HandleGoToPageEventArgs(e);
            this.aisinoDataGrid1.set_DataSource(this.djhbBLL.QueryXSDJ(this.DJmonth, this.DJtype, this.GFname, this.GFsh));
        }

        private void aisinoDataGrid2_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                this.djhbBLL.HandleGoToPageMXEventArgs(e);
                if (this.aisinoDataGrid1.get_SelectedRows().Count > 0)
                {
                    string xSDJBH = this.aisinoDataGrid1.get_SelectedRows()[0].Cells["BH"].Value.ToString();
                    this.aisinoDataGrid2.set_DataSource(this.djhbBLL.QueryXSDJMX(xSDJBH));
                    SaleBill bill = this.billBL.Find(xSDJBH);
                    int count = this.aisinoDataGrid2.get_Rows().Count;
                    for (int i = 0; i < count; i++)
                    {
                        string str2 = this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value.ToString();
                        string str3 = this.aisinoDataGrid2.get_Rows()[i].Cells["XH"].Value.ToString();
                        if (((str2 != null) && (str2 != "")) && (str2 != "中外合作油气田"))
                        {
                            string str4 = this.billBL.ShowSLV(bill, str3, str2);
                            if (str4 != "")
                            {
                                this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value = str4;
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

        private void BindMingxi()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "商品名称");
            item.Add("Property", "SPMC");
            item.Add("Type", "Text");
            item.Add("Width", "200");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "规格型号");
            item.Add("Property", "GGXH");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "数量");
            item.Add("Property", "SL");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单价");
            item.Add("Property", "DJ");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "金额");
            item.Add("Property", "JE");
            item.Add("Type", "Text");
            item.Add("Width", "150");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLV");
            item.Add("Type", "Text");
            item.Add("Width", "150");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税额");
            item.Add("Property", "SE");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "计量单位");
            item.Add("Property", "JLDW");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "含税价标志");
            item.Add("Property", "HSJBZ");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单据行性质");
            item.Add("Property", "DJHXZ");
            item.Add("Type", "Text");
            item.Add("RowStyleField", "DJHXZ");
            item.Add("Visible", "False");
            list.Add(item);
            this.aisinoDataGrid2.set_ColumeHead(list);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.DJmonth = this.comboBoxYF.SelectedItem.ToString();
                this.DJtype = this.comboBoxDJZL.SelectedValue.ToString();
                this.GFname = this.textBoxMC.Text.Trim();
                this.GFsh = this.textBoxSH.Text.Trim();
                this.aisinoDataGrid1.set_DataSource(this.djhbBLL.QueryXSDJ(this.DJmonth, this.DJtype, this.GFname, this.GFsh));
                if (this.aisinoDataGrid1.get_Rows().Count > 0)
                {
                    this.aisinoDataGrid1.get_Rows()[0].Selected = true;
                }
                if (this.aisinoDataGrid1.get_DataSource().get_Data().Rows.Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-272203");
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void checkBoxHBBZ_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.checkBoxHBBZ.Checked)
            {
                this.checkBoxKBZ.Checked = false;
                this.checkBoxKBZ.Enabled = false;
            }
            else
            {
                this.checkBoxKBZ.Enabled = true;
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

        private void DJHB_Load(object sender, EventArgs e)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单据种类");
            item.Add("Property", "DJZL");
            item.Add("Type", "Text");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单据编号");
            item.Add("Property", "BH");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "购方名称");
            item.Add("Property", "GFMC");
            item.Add("Type", "Text");
            item.Add("Width", "200");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "购方税号");
            item.Add("Property", "GFSH");
            item.Add("Type", "Text");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "金额合计");
            item.Add("Property", "JEHJ");
            item.Add("Type", "Text");
            item.Add("Width", "150");
            item.Add("Align", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单据日期");
            item.Add("Property", "DJRQ");
            item.Add("Type", "Text");
            item.Add("Width", "150");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "备注");
            item.Add("Property", "BZ");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "中外合作油气田");
            item.Add("Property", "HYSY");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Visible", "False");
            list.Add(item);
            this.aisinoDataGrid1.set_ColumeHead(list);
            this.aisinoDataGrid1.get_Columns()["JEHJ"].DefaultCellStyle.Format = "0.00";
            this.aisinoDataGrid1.set_DataSource(new AisinoDataSet());
            this.comboBoxYF.Items.AddRange(this.billBL.SaleBillMonth());
            this.comboBoxYF.SelectedIndex = 0;
            this.comboBoxDJZL.DataSource = CbbXmlBind.ReadXmlNode("InvType", false);
            this.comboBoxDJZL.DisplayMember = "Value";
            this.comboBoxDJZL.ValueMember = "Key";
            list = new List<Dictionary<string, string>>();
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "序号");
            item.Add("Property", "XH");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "商品名称");
            item.Add("Property", "SPMC");
            item.Add("Type", "Text");
            item.Add("Width", "200");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "规格型号");
            item.Add("Property", "GGXH");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "数量");
            item.Add("Property", "SL");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单价");
            item.Add("Property", "DJ");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "金额");
            item.Add("Property", "JE");
            item.Add("Type", "Text");
            item.Add("Width", "150");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLV");
            item.Add("Type", "Text");
            item.Add("Width", "150");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税额");
            item.Add("Property", "SE");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "计量单位");
            item.Add("Property", "JLDW");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "含税价标志");
            item.Add("Property", "HSJBZ");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单据行性质");
            item.Add("Property", "DJHXZ");
            item.Add("Type", "Text");
            item.Add("RowStyleField", "DJHXZ");
            item.Add("Visible", "False");
            list.Add(item);
            this.aisinoDataGrid2.set_ColumeHead(list);
            this.aisinoDataGrid2.get_Columns()["JE"].DefaultCellStyle.Format = "0.00";
            this.aisinoDataGrid2.get_Columns()["SE"].DefaultCellStyle.Format = "0.00";
            this.aisinoDataGrid2.set_DataSource(new AisinoDataSet());
            this.GetUserStateConfig();
        }

        private void GetUserStateConfig()
        {
            if (PropValue.KHSH == "KH")
            {
                this.radioButtonMC.Checked = true;
                this.textBoxMC.Focus();
            }
            else
            {
                this.radioButtonSH.Checked = true;
                this.textBoxSH.Focus();
            }
            if (PropValue.SimpleComplex == "Simple")
            {
                this.radioButtonSimple.Checked = true;
            }
            else
            {
                this.radioButtonComplicate.Checked = true;
            }
            this.checkBoxHBBZ.Checked = PropValue.IsMergeBZ;
            this.checkBoxKBZ.Checked = PropValue.IsBlankBZAdd;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DJHB));
            this.panel1 = new AisinoPNL();
            this.labHBSE = new AisinoLBL();
            this.labHBJE = new AisinoLBL();
            this.groupBox1 = new AisinoGRP();
            this.radioButtonSH = new AisinoRDO();
            this.textBoxSH = new AisinoTXT();
            this.radioButtonMC = new AisinoRDO();
            this.textBoxMC = new AisinoTXT();
            this.comboBoxYF = new AisinoCMB();
            this.label4 = new AisinoLBL();
            this.comboBoxDJZL = new AisinoCMB();
            this.btnQuery = new AisinoBTN();
            this.label3 = new AisinoLBL();
            this.toolStrip1 = new ToolStrip();
            this.toolStripButton1 = new ToolStripButton();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.toolStripBtnHB = new ToolStripButton();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.splitContainer1 = new AisinoSPL();
            this.groupBox3 = new AisinoGRP();
            this.aisinoDataGrid1 = new AisinoDataGrid();
            this.groupBox4 = new AisinoGRP();
            this.aisinoDataGrid2 = new AisinoDataGrid();
            this.groupBox2 = new AisinoGRP();
            this.label1 = new AisinoLBL();
            this.checkBoxKBZ = new AisinoCHK();
            this.radioButtonComplicate = new AisinoRDO();
            this.radioButtonSimple = new AisinoRDO();
            this.checkBoxHBBZ = new AisinoCHK();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.panel1.BackColor = Color.Transparent;
            this.panel1.Controls.Add(this.labHBSE);
            this.panel1.Controls.Add(this.labHBJE);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(890, 0x236);
            this.panel1.TabIndex = 0;
            this.labHBSE.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.labHBSE.AutoSize = true;
            this.labHBSE.Location = new Point(0x1fb, 0x221);
            this.labHBSE.Name = "labHBSE";
            this.labHBSE.Size = new Size(0x35, 12);
            this.labHBSE.TabIndex = 0x1c;
            this.labHBSE.Text = "合计税额";
            this.labHBJE.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.labHBJE.AutoSize = true;
            this.labHBJE.Location = new Point(0x142, 0x221);
            this.labHBJE.Name = "labHBJE";
            this.labHBJE.Size = new Size(0x35, 12);
            this.labHBJE.TabIndex = 0x1b;
            this.labHBJE.Text = "合计金额";
            this.groupBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.radioButtonSH);
            this.groupBox1.Controls.Add(this.textBoxSH);
            this.groupBox1.Controls.Add(this.radioButtonMC);
            this.groupBox1.Controls.Add(this.textBoxMC);
            this.groupBox1.Controls.Add(this.comboBoxYF);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxDJZL);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new Point(6, 0x1a);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x36e, 0x2b);
            this.groupBox1.TabIndex = 0x18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            this.radioButtonSH.Anchor = AnchorStyles.Left;
            this.radioButtonSH.AutoSize = true;
            this.radioButtonSH.Location = new Point(0xe8, 0x12);
            this.radioButtonSH.Name = "radioButtonSH";
            this.radioButtonSH.Size = new Size(0x47, 0x10);
            this.radioButtonSH.TabIndex = 11;
            this.radioButtonSH.TabStop = true;
            this.radioButtonSH.Text = "客户税号";
            this.radioButtonSH.UseVisualStyleBackColor = true;
            this.textBoxSH.Anchor = AnchorStyles.Left;
            this.textBoxSH.Location = new Point(0x135, 0x10);
            this.textBoxSH.Name = "textBoxSH";
            this.textBoxSH.Size = new Size(0x76, 0x15);
            this.textBoxSH.TabIndex = 10;
            this.radioButtonMC.Anchor = AnchorStyles.Left;
            this.radioButtonMC.AutoSize = true;
            this.radioButtonMC.Location = new Point(6, 0x12);
            this.radioButtonMC.Name = "radioButtonMC";
            this.radioButtonMC.Size = new Size(0x47, 0x10);
            this.radioButtonMC.TabIndex = 9;
            this.radioButtonMC.TabStop = true;
            this.radioButtonMC.Text = "客户名称";
            this.radioButtonMC.UseVisualStyleBackColor = true;
            this.textBoxMC.Anchor = AnchorStyles.Left;
            this.textBoxMC.Location = new Point(0x53, 0x10);
            this.textBoxMC.Name = "textBoxMC";
            this.textBoxMC.Size = new Size(0x76, 0x15);
            this.textBoxMC.TabIndex = 4;
            this.comboBoxYF.Anchor = AnchorStyles.Left;
            this.comboBoxYF.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxYF.FormattingEnabled = true;
            this.comboBoxYF.Location = new Point(0x2b9, 0x10);
            this.comboBoxYF.Name = "comboBoxYF";
            this.comboBoxYF.Size = new Size(0x33, 20);
            this.comboBoxYF.TabIndex = 7;
            this.label4.Anchor = AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x299, 20);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "月份";
            this.comboBoxDJZL.Anchor = AnchorStyles.Left;
            this.comboBoxDJZL.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxDJZL.FormattingEnabled = true;
            this.comboBoxDJZL.Location = new Point(0x202, 15);
            this.comboBoxDJZL.Name = "comboBoxDJZL";
            this.comboBoxDJZL.Size = new Size(0x87, 20);
            this.comboBoxDJZL.TabIndex = 6;
            this.btnQuery.Anchor = AnchorStyles.Left;
            this.btnQuery.Location = new Point(0x2ff, 0x10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new Size(0x2e, 0x17);
            this.btnQuery.TabIndex = 8;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.label3.Anchor = AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1c7, 0x13);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "单据种类";
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new Size(40, 40);
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripButton1, this.toolStripSeparator1, this.toolStripBtnHB, this.toolStripSeparator3 });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(890, 0x19);
            this.toolStrip1.TabIndex = 0x17;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripButton1.Image = Resources.退出;
            this.toolStripButton1.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new Size(0x33, 0x16);
            this.toolStripButton1.Text = "退出";
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            this.toolStripBtnHB.Image = Resources.折扣;
            this.toolStripBtnHB.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripBtnHB.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnHB.Name = "toolStripBtnHB";
            this.toolStripBtnHB.Size = new Size(0x63, 0x16);
            this.toolStripBtnHB.Text = "单据合并预览";
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(6, 0x19);
            this.splitContainer1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.splitContainer1.Location = new Point(3, 0x8e);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Size = new Size(0x374, 0x18c);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.TabIndex = 0x1a;
            this.groupBox3.Controls.Add(this.aisinoDataGrid1);
            this.groupBox3.Dock = DockStyle.Fill;
            this.groupBox3.Location = new Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x374, 260);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "销售单据";
            this.aisinoDataGrid1.set_AborCellPainting(false);
            this.aisinoDataGrid1.AutoSize = true;
            this.aisinoDataGrid1.BackColor = SystemColors.Control;
            this.aisinoDataGrid1.set_CurrentCell(null);
            this.aisinoDataGrid1.set_DataSource(null);
            this.aisinoDataGrid1.Dock = DockStyle.Fill;
            this.aisinoDataGrid1.set_FirstDisplayedScrollingRowIndex(-1);
            this.aisinoDataGrid1.set_IsShowAll(false);
            this.aisinoDataGrid1.Location = new Point(3, 0x11);
            this.aisinoDataGrid1.Name = "aisinoDataGrid1";
            this.aisinoDataGrid1.RightToLeft = RightToLeft.No;
            this.aisinoDataGrid1.set_ShowAllChkVisible(true);
            this.aisinoDataGrid1.Size = new Size(0x36e, 240);
            this.aisinoDataGrid1.TabIndex = 1;
            this.groupBox4.Controls.Add(this.aisinoDataGrid2);
            this.groupBox4.Dock = DockStyle.Fill;
            this.groupBox4.Location = new Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x374, 0x84);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "销售单据明细";
            this.aisinoDataGrid2.set_AborCellPainting(false);
            this.aisinoDataGrid2.AutoSize = true;
            this.aisinoDataGrid2.set_CurrentCell(null);
            this.aisinoDataGrid2.set_DataSource(null);
            this.aisinoDataGrid2.Dock = DockStyle.Fill;
            this.aisinoDataGrid2.set_FirstDisplayedScrollingRowIndex(-1);
            this.aisinoDataGrid2.set_IsShowAll(false);
            this.aisinoDataGrid2.Location = new Point(3, 0x11);
            this.aisinoDataGrid2.Name = "aisinoDataGrid2";
            this.aisinoDataGrid2.RightToLeft = RightToLeft.No;
            this.aisinoDataGrid2.set_ShowAllChkVisible(true);
            this.aisinoDataGrid2.Size = new Size(0x36e, 0x70);
            this.aisinoDataGrid2.TabIndex = 0;
            this.groupBox2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.checkBoxKBZ);
            this.groupBox2.Controls.Add(this.radioButtonComplicate);
            this.groupBox2.Controls.Add(this.radioButtonSimple);
            this.groupBox2.Controls.Add(this.checkBoxHBBZ);
            this.groupBox2.Location = new Point(6, 0x47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x36e, 0x41);
            this.groupBox2.TabIndex = 0x19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "合并选项";
            this.label1.AutoSize = true;
            this.label1.ForeColor = Color.Brown;
            this.label1.Location = new Point(0x1f5, 20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x83, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "合并选择不能超过800行";
            this.checkBoxKBZ.AutoSize = true;
            this.checkBoxKBZ.Location = new Point(0x202, 0x2a);
            this.checkBoxKBZ.Name = "checkBoxKBZ";
            this.checkBoxKBZ.Size = new Size(120, 0x10);
            this.checkBoxKBZ.TabIndex = 3;
            this.checkBoxKBZ.Text = "空备注添加单据号";
            this.checkBoxKBZ.UseVisualStyleBackColor = true;
            this.radioButtonComplicate.AutoSize = true;
            this.radioButtonComplicate.Location = new Point(0x1f, 0x2a);
            this.radioButtonComplicate.Name = "radioButtonComplicate";
            this.radioButtonComplicate.Size = new Size(0x179, 0x10);
            this.radioButtonComplicate.TabIndex = 2;
            this.radioButtonComplicate.Tag = "合并后的统计为一条折扣行";
            this.radioButtonComplicate.Text = "复杂合并(商品名称、规格型号、计量单位、单价 相同合并为一行)";
            this.radioButtonComplicate.UseVisualStyleBackColor = true;
            this.radioButtonSimple.AutoSize = true;
            this.radioButtonSimple.Checked = true;
            this.radioButtonSimple.Location = new Point(0x1f, 20);
            this.radioButtonSimple.Name = "radioButtonSimple";
            this.radioButtonSimple.Size = new Size(0xcb, 0x10);
            this.radioButtonSimple.TabIndex = 1;
            this.radioButtonSimple.TabStop = true;
            this.radioButtonSimple.Text = "简单合并(合并后商品行保持不变)";
            this.radioButtonSimple.UseVisualStyleBackColor = true;
            this.checkBoxHBBZ.AutoSize = true;
            this.checkBoxHBBZ.Location = new Point(0x1b4, 0x2b);
            this.checkBoxHBBZ.Name = "checkBoxHBBZ";
            this.checkBoxHBBZ.Size = new Size(0x48, 0x10);
            this.checkBoxHBBZ.TabIndex = 0;
            this.checkBoxHBBZ.Text = "合并备注";
            this.checkBoxHBBZ.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(890, 0x236);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "DJHB";
            this.Text = "销售单据合并";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

        private void radioButtonMC_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonMC.Checked)
            {
                this.textBoxMC.Focus();
                this.textBoxSH.Text = "";
            }
            else
            {
                this.textBoxSH.Focus();
                this.textBoxMC.Text = "";
            }
        }

        private void toolBtnHB_Click(object sender, EventArgs e)
        {
            try
            {
                MergerRemarkType noMergerRemark;
                List<string> list = new List<string>();
                List<DataGridViewRow> list2 = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in (IEnumerable) this.aisinoDataGrid1.get_Rows())
                {
                    if (row.Selected)
                    {
                        list2.Add(row);
                        list.Add(row.Cells["BH"].Value.ToString());
                    }
                }
                MergerType mType = this.radioButtonSimple.Checked ? MergerType.SimpleMerger : MergerType.ComplexMerger;
                if (this.checkBoxHBBZ.Checked)
                {
                    noMergerRemark = this.checkBoxKBZ.Checked ? MergerRemarkType.DoMergerRemarkFill : MergerRemarkType.OnlyMergerRemark;
                }
                else
                {
                    noMergerRemark = MergerRemarkType.NoMergerRemark;
                }
                List<SaleBill> listBill = new List<SaleBill>();
                foreach (string str in list)
                {
                    listBill.Add(this.billBL.Find(str));
                }
                SaleBill mergedBill = new SaleBill();
                string str2 = this.billBL.MergeSaleBill(listBill, mergedBill, mType, noMergerRemark);
                if (str2 == "0")
                {
                    new DJHBYL(this.djhbBLL, listBill, mergedBill).ShowDialog();
                    this.aisinoDataGrid1.set_DataSource(this.djhbBLL.QueryXSDJ(this.DJmonth, this.DJtype, this.GFname, this.GFsh));
                    PropValue.KHSH = this.radioButtonMC.Checked ? "KH" : "SH";
                    PropValue.SimpleComplex = this.radioButtonSimple.Checked ? "Simple" : "Complex";
                    PropValue.IsMergeBZ = this.checkBoxHBBZ.Checked;
                    PropValue.IsBlankBZAdd = this.checkBoxKBZ.Checked;
                    if (this.aisinoDataGrid1.get_Rows().Count == 0)
                    {
                        this.aisinoDataGrid2.set_DataSource(this.djhbBLL.QueryXSDJMX("noexist"));
                    }
                }
                else
                {
                    MessageManager.ShowMsgBox(str2);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void toolBtnQuit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void txtDJBH_GotFocus(object sender, EventArgs e)
        {
            this.radioButtonMC.Checked = false;
            this.radioButtonSH.Checked = false;
            AisinoTXT otxt = (AisinoTXT) sender;
            if (otxt.Name == "textBoxMC")
            {
                this.radioButtonMC.Checked = true;
            }
            if (otxt.Name == "textBoxSH")
            {
                this.radioButtonSH.Checked = true;
            }
        }
    }
}

