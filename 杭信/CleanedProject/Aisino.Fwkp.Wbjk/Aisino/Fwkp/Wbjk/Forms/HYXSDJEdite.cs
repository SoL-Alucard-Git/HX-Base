namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class HYXSDJEdite : Form
    {
        private AisinoMultiCombox _spmcBt;
        private SaleBill bill;
        private AisinoBTN button1;
        private AisinoBTN button2;
        private AisinoBTN button3;
        private DataGridViewTextBoxColumn colFyxm;
        private DataGridViewTextBoxColumn colJe;
        private IContainer components;
        private DateTimePicker dateTimePicker1;
        private CustomStyleDataGrid dgFyxm;
        private Invoice InvoiceKP;
        private SaleBillCtrl saleBillBL;
        private AisinoTXT textBox1;
        private AisinoTXT textBox10;
        private AisinoTXT textBox11;
        private AisinoTXT textBox12;
        private AisinoTXT textBox13;
        private AisinoTXT textBox15;
        private AisinoTXT textBox16;
        private AisinoTXT textBox17;
        private AisinoTXT textBox2;
        private AisinoTXT textBox3;
        private AisinoTXT textBox4;
        private AisinoTXT textBox5;
        private AisinoTXT textBox6;
        private AisinoTXT textBox7;
        private AisinoTXT textBox8;
        private AisinoTXT textBox9;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnHSJG;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButtonAdd;
        private ToolStripButton toolStripButtonDel;
        private XmlComponentLoader xmlComponentLoader1;

        public HYXSDJEdite()
        {
            this.components = null;
            this.saleBillBL = SaleBillCtrl.Instance;
            this.bill = null;
            this.InvoiceKP = null;
            this.Initialize();
            this.Text = "货运单据添加";
            this.toolStripButtonDel.Enabled = false;
            this.bill = new SaleBill();
            this.bill.DJRQ = TaxCardValue.taxCard.GetCardClock();
            this.Init_SLV();
            this.initdgFyxm();
            this.initInvoice();
        }

        public HYXSDJEdite(string BH)
        {
            this.components = null;
            this.saleBillBL = SaleBillCtrl.Instance;
            this.bill = null;
            this.InvoiceKP = null;
            this.Initialize();
            this.Text = "货运单据修改";
            this.textBox1.Cursor = Cursors.No;
            this.textBox1.ForeColor = SystemColors.GrayText;
            this.textBox1.ReadOnly = true;
            this.toolStripButtonDel.Enabled = false;
            this.initdgFyxm();
            this.initInvoice();
            this.bill = this.saleBillBL.Find(BH);
            this.ToView();
            if ((this.bill.DJZT == "W") || (this.bill.KPZT != "N"))
            {
                this.textBox2.Cursor = Cursors.No;
                this.textBox2.ForeColor = SystemColors.GrayText;
                this.textBox2.ReadOnly = true;
                this.textBox3.Cursor = Cursors.No;
                this.textBox3.ForeColor = SystemColors.GrayText;
                this.textBox3.ReadOnly = true;
                this.textBox4.Cursor = Cursors.No;
                this.textBox4.ForeColor = SystemColors.GrayText;
                this.textBox4.ReadOnly = true;
                this.textBox5.Cursor = Cursors.No;
                this.textBox5.ForeColor = SystemColors.GrayText;
                this.textBox5.ReadOnly = true;
                this.textBox6.Cursor = Cursors.No;
                this.textBox6.ForeColor = SystemColors.GrayText;
                this.textBox6.ReadOnly = true;
                this.textBox7.Cursor = Cursors.No;
                this.textBox7.ForeColor = SystemColors.GrayText;
                this.textBox7.ReadOnly = true;
                this.textBox8.Cursor = Cursors.No;
                this.textBox8.ForeColor = SystemColors.GrayText;
                this.textBox8.ReadOnly = true;
                this.textBox9.Cursor = Cursors.No;
                this.textBox9.ForeColor = SystemColors.GrayText;
                this.textBox9.ReadOnly = true;
                this.textBox10.Cursor = Cursors.No;
                this.textBox10.ForeColor = SystemColors.GrayText;
                this.textBox10.ReadOnly = true;
                this.textBox11.Cursor = Cursors.No;
                this.textBox11.ForeColor = SystemColors.GrayText;
                this.textBox11.ReadOnly = true;
                this.textBox12.Cursor = Cursors.No;
                this.textBox12.ForeColor = SystemColors.GrayText;
                this.textBox12.ReadOnly = true;
                this.textBox13.Cursor = Cursors.No;
                this.textBox13.ForeColor = SystemColors.GrayText;
                this.textBox13.ReadOnly = true;
                this.textBox15.Cursor = Cursors.No;
                this.textBox15.ForeColor = SystemColors.GrayText;
                this.textBox15.ReadOnly = true;
                this.textBox16.Cursor = Cursors.No;
                this.textBox16.ForeColor = SystemColors.GrayText;
                this.textBox16.ReadOnly = true;
                this.textBox17.Cursor = Cursors.No;
                this.textBox17.ForeColor = SystemColors.GrayText;
                this.textBox17.ReadOnly = true;
                this.button1.Enabled = false;
                this.button2.Enabled = false;
                this.button3.Enabled = false;
                this.dgFyxm.ReadOnly = true;
                this.dateTimePicker1.Enabled = false;
                this.toolStripButton2.Enabled = false;
                this.toolStripButtonAdd.Enabled = false;
                this.toolStripButtonDel.Enabled = false;
            }
        }

        private void _spmcBt_Click(object sender, EventArgs e)
        {
            this._SpmcSelect();
        }

        private void _spmcBt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this._SpmcSelect();
        }

        private void _spmcBt_OnAutoComplate(object sender, EventArgs e)
        {
            string text = this._spmcBt.Text;
            DataTable table = this._SpmcOnAutoCompleteDataSource(text);
            if (table != null)
            {
                this._spmcBt.set_DataSource(table);
            }
        }

        private void _spmcBt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this._SpmcSelect();
            }
        }

        private void _spmcBt_SetAutoComplateHead()
        {
            this._spmcBt.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("名称", "MC", 160));
            this._spmcBt.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("商品分类编码", "SPFL", 160));
            this._spmcBt.set_ShowText("MC");
            this._spmcBt.set_DrawHead(true);
            this._spmcBt.set_AutoIndex(1);
        }

        private void _spmcBt_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = this.dgFyxm.CurrentCell.RowIndex;
            string text = this._spmcBt.Text;
            if (this.InvoiceKP.SetSpmc(rowIndex, text))
            {
                string str2 = this.InvoiceKP.GetSpxx(rowIndex)[0];
                this._spmcBt.Text = str2;
                this.dgFyxm.Rows[rowIndex].Cells["colFyxm"].Value = this._spmcBt.Text;
            }
        }

        private DataTable _SpmcOnAutoCompleteDataSource(string str)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetFYXMMore", new object[] { str, 20, "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
        }

        private void _SpmcSelect()
        {
            try
            {
                string text = this._spmcBt.Text;
                object[] objArray = new object[] { text, 0, "MC" };
                object[] spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetFYXM", objArray);
                this.SetSpxx(spxx);
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;
            object obj2 = grid.Rows[rowIndex].Cells[columnIndex].Value;
            string s = (obj2 == null) ? "" : obj2.ToString();
            if (columnIndex == 1)
            {
                double result = 0.0;
                if (!double.TryParse(s, out result))
                {
                    grid.Rows[rowIndex].Cells[columnIndex].Value = "";
                }
                this.UpdateJE();
            }
        }

        private void dataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            AisinoMultiCombox combox = grid.Controls["SPMCBT"] as AisinoMultiCombox;
            if ((grid.CurrentCell != null) && !grid.CurrentRow.ReadOnly)
            {
                DataGridViewColumn owningColumn = grid.CurrentCell.OwningColumn;
                if (owningColumn.Name.Equals("colFyxm"))
                {
                    int index = owningColumn.Index;
                    int rowIndex = grid.CurrentCell.RowIndex;
                    Rectangle rectangle = grid.GetCellDisplayRectangle(index, rowIndex, false);
                    if (combox != null)
                    {
                        combox.Left = rectangle.Left;
                        combox.Top = rectangle.Top;
                        combox.Width = rectangle.Width;
                        combox.Height = rectangle.Height;
                        combox.Text = (grid.CurrentCell.Value == null) ? "" : grid.CurrentCell.Value.ToString();
                        DataTable table = combox.get_DataSource();
                        if (table != null)
                        {
                            table.Clear();
                        }
                        combox.Visible = true;
                        combox.Focus();
                    }
                }
                else if (combox != null)
                {
                    combox.Visible = false;
                }
            }
            else if (((grid.CurrentRow != null) && grid.CurrentRow.ReadOnly) && (combox != null))
            {
                combox.Visible = false;
            }
        }

        private void dataGridView_RowsAdded(object sender, EventArgs e)
        {
            this.dgFyxm.Rows.Add();
            this.InvoiceKP.AddSpxx(null, "0.05", 0);
            this.InvoiceKP.SetSpmc(this.dgFyxm.Rows.Count - 1, "0");
            this.InvoiceKP.SetJe(this.dgFyxm.Rows.Count - 1, "1");
            this.toolStripButtonDel.Enabled = true;
        }

        private void dataGridView_RowsRemoved(object sender, EventArgs e)
        {
            if (this.dgFyxm.CurrentCell == null)
            {
                MessageManager.ShowMsgBox("请选择单据行");
            }
            else
            {
                int rowIndex = this.dgFyxm.CurrentCell.RowIndex;
                this.dgFyxm.Rows.RemoveAt(rowIndex);
                this.InvoiceKP.DelSpxx(rowIndex);
                if (this.dgFyxm.Rows.Count == 0)
                {
                    this.toolStripButtonDel.Enabled = false;
                    AisinoMultiCombox combox = this.dgFyxm.Controls["SPMCBT"] as AisinoMultiCombox;
                    combox.Visible = false;
                }
                this.UpdateJE();
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

        private void FHRMC_BtnClick(object sender, EventArgs e)
        {
            string text = this.textBox5.Text;
            this.SetName(text, 0, 2);
        }

        private void HSJGBtnClick(object sender, EventArgs e)
        {
            string s = this.textBox16.Text.Trim();
            double result = 0.0;
            if (!double.TryParse(s, out result))
            {
                this.toolStripBtnHSJG.Checked = false;
                if (this.dgFyxm.Columns["colJe"] != null)
                {
                    this.dgFyxm.Columns["colJe"].HeaderText = this.dgFyxm.Columns["colJe"].HeaderText.Split(new char[] { '(' })[0] + "(不含税)";
                }
            }
            else
            {
                int num3;
                string str3;
                string str2 = this.toolStripBtnHSJG.Checked ? "(含税)" : "(不含税)";
                if (this.dgFyxm.Columns["colJe"] != null)
                {
                    this.dgFyxm.Columns["colJe"].HeaderText = this.dgFyxm.Columns["colJe"].HeaderText.Split(new char[] { '(' })[0] + str2;
                }
                int count = this.dgFyxm.Rows.Count;
                if (this.toolStripBtnHSJG.Checked)
                {
                    for (num3 = 0; num3 < count; num3++)
                    {
                        str3 = (this.dgFyxm.Rows[num3].Cells[1].Value == null) ? "" : this.dgFyxm.Rows[num3].Cells[1].Value.ToString();
                        double num4 = CommonTool.Todouble(str3);
                        double round = (num4 * result) + num4;
                        round = SaleBillCtrl.GetRound(round, 2);
                        this.dgFyxm.Rows[num3].Cells[1].Value = round;
                        this.UpdateJE();
                    }
                }
                else
                {
                    for (num3 = 0; num3 < count; num3++)
                    {
                        str3 = (this.dgFyxm.Rows[num3].Cells[1].Value == null) ? "" : this.dgFyxm.Rows[num3].Cells[1].Value.ToString();
                        double num6 = CommonTool.Todouble(str3) / (1.0 + result);
                        num6 = SaleBillCtrl.GetRound(num6, 2);
                        this.dgFyxm.Rows[num3].Cells[1].Value = num6;
                        this.UpdateJE();
                    }
                }
            }
        }

        private void HYXSDJEdite_FormClosing(object sender, EventArgs e)
        {
            this.XSDJ_close();
        }

        private void Init_SLV()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            int count = card.get_SQInfo().PZSQType.Count;
            List<double> list = new List<double>();
            for (int i = 0; i < count; i++)
            {
                if ((card.get_SQInfo().PZSQType[i].invType == 11) && (card.get_SQInfo().PZSQType[i].TaxRate.Count > 0))
                {
                    this.textBox16.Text = card.get_SQInfo().PZSQType[i].TaxRate[0].Rate.ToString();
                }
            }
        }

        private void initdgFyxm()
        {
            if (this.dgFyxm.Columns["colJe"] != null)
            {
                this.dgFyxm.Columns["colJe"].HeaderText = this.dgFyxm.Columns["colJe"].HeaderText.Split(new char[] { '(' })[0] + "(不含税)";
            }
            for (int i = 0; i < this.dgFyxm.Columns.Count; i++)
            {
                this.dgFyxm.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.dgFyxm.Columns["colJe"].DefaultCellStyle.Format = "N2";
            this.dgFyxm.ImeMode = ImeMode.NoControl;
        }

        private void InitHYDJ()
        {
            for (int i = 0; i < this.dgFyxm.Columns.Count; i++)
            {
                this.dgFyxm.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.textBox15.ReadOnly = true;
            this.dgFyxm.AllowUserToAddRows = false;
            this.dgFyxm.StandardTab = false;
            this.dgFyxm.MultiSelect = false;
            this.InitSpmcCmb();
            this.dgFyxm.Controls.Add(this._spmcBt);
            this.dgFyxm.CurrentCellChanged += new EventHandler(this.dataGridView_CurrentCellChanged);
            this.dgFyxm.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dgFyxm.CurrentCell = null;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripButton1 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton1");
            this.toolStripButton2 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton2");
            this.toolStripButtonAdd = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonAdd");
            this.toolStripButtonDel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonDel");
            this.toolStripBtnHSJG = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnHSJG");
            this.button1 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button1");
            this.button2 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button2");
            this.button3 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button3");
            this.textBox1 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox1");
            this.textBox2 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox2");
            this.textBox3 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox3");
            this.textBox4 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox4");
            this.textBox5 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox5");
            this.textBox6 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox6");
            this.textBox7 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox7");
            this.textBox8 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox8");
            this.textBox9 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox9");
            this.textBox10 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox10");
            this.textBox11 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox11");
            this.textBox12 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox12");
            this.textBox13 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox13");
            this.textBox15 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox15");
            this.textBox16 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox16");
            this.textBox17 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox17");
            this.dateTimePicker1 = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dateTimePicker1");
            this.dgFyxm = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("dgFyxm");
            this.colFyxm = this.xmlComponentLoader1.GetControlByName<DataGridViewTextBoxColumn>("colFyxm");
            this.colJe = this.xmlComponentLoader1.GetControlByName<DataGridViewTextBoxColumn>("colJe");
            this.toolStripButton1.Click += new EventHandler(this.QuitBtnClick);
            this.toolStripButton2.Click += new EventHandler(this.SaveBtnClick);
            this.toolStripButtonAdd.Click += new EventHandler(this.dataGridView_RowsAdded);
            this.toolStripButtonDel.Click += new EventHandler(this.dataGridView_RowsRemoved);
            this.toolStripBtnHSJG.Click += new EventHandler(this.HSJGBtnClick);
            this.toolStripBtnHSJG.CheckOnClick = true;
            this.toolStripBtnHSJG.Checked = false;
            this.button1.Click += new EventHandler(this.SHRMC_BtnClick);
            this.button2.Click += new EventHandler(this.FHRMC_BtnClick);
            this.button3.Click += new EventHandler(this.SPFMC_BtnClick);
            this.textBox16.KeyPress += new KeyPressEventHandler(this.textBox16_KeyPress);
            this.textBox16.LostFocus += new EventHandler(this.textBox16_LostFocus);
            base.FormClosing += new FormClosingEventHandler(this.HYXSDJEdite_FormClosing);
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.textBox2.TextChanged += new EventHandler(this.textBox2_TextChanged);
            this.textBox3.TextChanged += new EventHandler(this.textBox3_TextChanged);
            this.textBox4.TextChanged += new EventHandler(this.textBox4_TextChanged);
            this.textBox5.TextChanged += new EventHandler(this.textBox5_TextChanged);
            this.textBox6.TextChanged += new EventHandler(this.textBox6_TextChanged);
            this.textBox7.TextChanged += new EventHandler(this.textBox7_TextChanged);
            this.textBox8.TextChanged += new EventHandler(this.textBox8_TextChanged);
            this.textBox9.TextChanged += new EventHandler(this.textBox9_TextChanged);
            this.textBox10.TextChanged += new EventHandler(this.textBox10_TextChanged);
            this.textBox11.TextChanged += new EventHandler(this.textBox11_TextChanged);
            this.textBox12.TextChanged += new EventHandler(this.textBox12_TextChanged);
            this.textBox13.TextChanged += new EventHandler(this.textBox13_TextChanged);
            this.textBox17.TextChanged += new EventHandler(this.textBox17_TextChanged);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(HYXSDJEdite));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(800, 600);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "货运专票单据编辑";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.HYXSDJEdite\Aisino.Fwkp.Wbjk.HYXSDJEdite.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(800, 600);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "HYDJEdite";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "货运专票单据编辑";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.XSDJEdite_Load);
            base.ResumeLayout(false);
        }

        private void initInvoice()
        {
            byte[] buffer = null;
            this.InvoiceKP = new Invoice(false, false, false, 11, buffer, null);
            this.textBox2.KeyDown += new KeyEventHandler(this.textBox2_KeyDown);
            this.textBox4.KeyDown += new KeyEventHandler(this.textBox4_KeyDown);
            this.textBox5.KeyDown += new KeyEventHandler(this.textBox5_KeyDown);
        }

        private void InitSpmcCmb()
        {
            this._spmcBt = new AisinoMultiCombox();
            this._spmcBt.set_IsSelectAll(true);
            this._spmcBt.Name = "SPMCBT";
            this._spmcBt.Text = "";
            this._spmcBt.Padding = new Padding(0);
            this._spmcBt.Margin = new Padding(0);
            this._spmcBt.Visible = false;
            this._spmcBt_SetAutoComplateHead();
            this._spmcBt.set_AutoComplate(2);
            this._spmcBt.set_buttonStyle(0);
            this._spmcBt.add_OnButtonClick(new EventHandler(this._spmcBt_Click));
            this._spmcBt.MouseDoubleClick += new MouseEventHandler(this._spmcBt_MouseDoubleClick);
            this._spmcBt.OnTextChanged = (EventHandler) Delegate.Combine(this._spmcBt.OnTextChanged, new EventHandler(this._spmcBt_TextChanged));
            this._spmcBt.PreviewKeyDown += new PreviewKeyDownEventHandler(this._spmcBt_PreviewKeyDown);
        }

        private void QuitBtnClick(object sender, EventArgs e)
        {
            this.XSDJ_close();
            base.Close();
        }

        private void SaveBtnClick(object sender, EventArgs e)
        {
            string str = "";
            if (this.dgFyxm.EndEdit())
            {
            }
            this.ToModel();
            str = this.saleBillBL.Save(this.bill);
            if (str == "0")
            {
                string str2 = this.saleBillBL.CheckBill(this.bill);
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageManager.ShowMsgBox(str);
            }
        }

        private void SetName(string name, int type, int nametype)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { name, type, "MC,SH" });
            if ((objArray != null) && (objArray[0].ToString().CompareTo("Error") != 0))
            {
                if (nametype == 1)
                {
                    this.textBox4.Text = objArray[0].ToString();
                    this.textBox6.Text = objArray[1].ToString();
                }
                else if (nametype == 2)
                {
                    this.textBox5.Text = objArray[0].ToString();
                    this.textBox7.Text = objArray[1].ToString();
                }
                else if (nametype == 3)
                {
                    this.textBox2.Text = objArray[0].ToString();
                    this.textBox3.Text = objArray[1].ToString();
                }
            }
        }

        private void SetSpxx(object[] spxx)
        {
            int rowIndex = this.dgFyxm.CurrentCell.RowIndex;
            if ((spxx != null) && (spxx.Length > 0))
            {
                this.dgFyxm.Rows[rowIndex].Cells["colFyxm"].Value = spxx[0];
                this.dgFyxm.CurrentCell = this.dgFyxm.Rows[rowIndex].Cells["colJe"];
            }
            this.dgFyxm.Focus();
        }

        private void SHRMC_BtnClick(object sender, EventArgs e)
        {
            string text = this.textBox4.Text;
            this.SetName(text, 0, 1);
        }

        private void SPFMC_BtnClick(object sender, EventArgs e)
        {
            string text = this.textBox2.Text;
            this.SetName(text, 0, 3);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            for (int i = ToolUtil.GetByteCount(text); i > 20; i = ToolUtil.GetByteCount(text))
            {
                int length = text.Length;
                text = text.Substring(0, length - 1);
            }
            this.textBox1.Text = text;
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox10.Text.Trim();
                this.InvoiceKP.set_Qyd_jy_ddd(str);
                string strB = this.InvoiceKP.get_Qyd_jy_ddd();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox10.Text = strB;
                    this.textBox10.SelectionStart = this.textBox10.Text.Length;
                }
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox11.Text.Trim();
                this.InvoiceKP.set_Fhr(str);
                string strB = this.InvoiceKP.get_Fhr();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox11.Text = strB;
                    this.textBox11.SelectionStart = this.textBox11.Text.Length;
                }
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox12.Text.Trim();
                this.InvoiceKP.set_Skr(str);
                string strB = this.InvoiceKP.get_Skr();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox12.Text = strB;
                    this.textBox12.SelectionStart = this.textBox12.Text.Length;
                }
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox13.Text.Trim();
                this.InvoiceKP.set_Yshwxx(str);
                string strB = this.InvoiceKP.get_Yshwxx();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox13.Text = strB;
                    this.textBox13.SelectionStart = this.textBox13.Text.Length;
                }
            }
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void textBox16_LostFocus(object sender, EventArgs e)
        {
            string[] strArray;
            string s = this.textBox16.Text.Trim();
            double result = 0.0;
            if (double.TryParse(s, out result))
            {
                if (result >= 1.0)
                {
                    this.textBox16.LostFocus -= new EventHandler(this.textBox16_LostFocus);
                    strArray = null;
                    MessageManager.ShowMsgBox("A305", strArray);
                    this.textBox16.Focus();
                    this.textBox16.LostFocus += new EventHandler(this.textBox16_LostFocus);
                }
                this.textBox16.Text = result.ToString();
            }
            else
            {
                this.textBox16.LostFocus -= new EventHandler(this.textBox16_LostFocus);
                strArray = null;
                MessageManager.ShowMsgBox("A305", strArray);
                this.textBox16.Focus();
                this.textBox16.LostFocus += new EventHandler(this.textBox16_LostFocus);
            }
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox17.Text.Trim();
                this.InvoiceKP.set_Bz(str);
                string strB = this.InvoiceKP.get_Bz();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox17.Text = strB;
                    this.textBox17.SelectionStart = this.textBox17.Text.Length;
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox box = (TextBox) sender;
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { box.Text, 1, "MC,SH" });
                if (objArray != null)
                {
                    this.textBox2.Text = objArray[0].ToString();
                    this.textBox3.Text = objArray[1].ToString();
                    this.textBox4.Focus();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox2.Text.Trim();
                this.InvoiceKP.set_Gfmc(str);
                string strB = this.InvoiceKP.get_Gfmc();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox2.Text = strB;
                    this.textBox2.SelectionStart = this.textBox2.Text.Length;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string text = this.textBox3.Text;
                this.InvoiceKP.set_Gfsh(text);
                string strB = this.InvoiceKP.get_Gfsh();
                if (text.Trim().CompareTo(strB) != 0)
                {
                    this.textBox3.Text = strB;
                    this.textBox3.SelectionStart = this.textBox3.Text.Length;
                }
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox box = (TextBox) sender;
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { box.Text, 1, "MC,SH" });
                if (objArray != null)
                {
                    this.textBox4.Text = objArray[0].ToString();
                    this.textBox6.Text = objArray[1].ToString();
                    this.textBox5.Focus();
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox4.Text.Trim();
                this.InvoiceKP.set_Shrmc(str);
                string strB = this.InvoiceKP.get_Shrmc();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox4.Text = strB;
                    this.textBox4.SelectionStart = this.textBox4.Text.Length;
                }
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox box = (TextBox) sender;
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { box.Text, 1, "MC,SH" });
                if (objArray != null)
                {
                    this.textBox5.Text = objArray[0].ToString();
                    this.textBox7.Text = objArray[1].ToString();
                    this.textBox8.Focus();
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox5.Text.Trim();
                this.InvoiceKP.set_Fhrmc(str);
                string strB = this.InvoiceKP.get_Fhrmc();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox5.Text = strB;
                    this.textBox5.SelectionStart = this.textBox5.Text.Length;
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox6.Text.Trim();
                this.InvoiceKP.set_Shrsh(str);
                string strB = this.InvoiceKP.get_Shrsh();
                if (str.Trim().CompareTo(strB) != 0)
                {
                    this.textBox6.Text = strB;
                    this.textBox6.SelectionStart = this.textBox6.Text.Length;
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox7.Text.Trim();
                this.InvoiceKP.set_Fhrsh(str);
                string strB = this.InvoiceKP.get_Fhrsh();
                if (str.Trim().CompareTo(strB) != 0)
                {
                    this.textBox7.Text = strB;
                    this.textBox7.SelectionStart = this.textBox7.Text.Length;
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox8.Text.Trim();
                this.InvoiceKP.set_Czch(str);
                string strB = this.InvoiceKP.get_Czch();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox8.Text = strB;
                    this.textBox8.SelectionStart = this.textBox8.Text.Length;
                }
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox9.Text.Trim();
                this.InvoiceKP.set_Ccdw(str);
                string strB = this.InvoiceKP.get_Ccdw();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox9.Text = strB;
                    this.textBox9.SelectionStart = this.textBox9.Text.Length;
                }
            }
        }

        private void ToModel()
        {
            this.bill.BH = this.textBox1.Text.Trim();
            this.bill.GFMC = this.textBox2.Text.Trim();
            this.bill.GFSH = this.textBox3.Text.Trim();
            this.bill.GFDZDH = this.textBox4.Text.Trim();
            this.bill.XFDZDH = this.textBox5.Text.Trim();
            this.bill.CM = this.textBox6.Text.Trim();
            this.bill.TYDH = this.textBox7.Text.Trim();
            this.bill.QYD = this.textBox8.Text.Trim();
            this.bill.DW = this.textBox9.Text.Trim();
            this.bill.XFYHZH = this.textBox10.Text.Trim();
            this.bill.FHR = this.textBox11.Text.Trim();
            this.bill.SKR = this.textBox12.Text.Trim();
            this.bill.YSHWXX = this.textBox13.Text.Trim();
            this.bill.DJRQ = this.dateTimePicker1.Value.Date;
            this.bill.DJYF = this.bill.DJRQ.Month;
            if (this.textBox16.Text.Trim() == "")
            {
                this.bill.SLV = 0.0;
            }
            else
            {
                this.bill.SLV = CommonTool.Todouble(this.textBox16.Text.Trim());
            }
            this.bill.BZ = this.textBox17.Text.Trim();
            this.bill.DJZL = "f";
            this.bill.ListGoods.Clear();
            double num = 0.0;
            int count = this.dgFyxm.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                string str = (this.dgFyxm.Rows[i].Cells[0].Value == null) ? "" : this.dgFyxm.Rows[i].Cells[0].Value.ToString();
                string number = (this.dgFyxm.Rows[i].Cells[1].Value == null) ? "" : this.dgFyxm.Rows[i].Cells[1].Value.ToString();
                Goods item = new Goods {
                    XSDJBH = this.bill.BH,
                    XH = i + 1,
                    SPMC = str
                };
                double num4 = (number == "") ? 0.0 : CommonTool.Todouble(number);
                if (this.toolStripBtnHSJG.Checked)
                {
                    num4 /= 1.0 + this.bill.SLV;
                }
                item.JE = SaleBillCtrl.GetRound(num4, 2);
                double num5 = item.JE * this.bill.SLV;
                item.SE = SaleBillCtrl.GetRound(num5, 2);
                this.bill.ListGoods.Add(item);
                num += item.JE;
            }
            this.bill.JEHJ = num;
        }

        private void ToStyle(SaleBill bill)
        {
            if ((bill.KPZT != "N") && ((bill.KPZT == "A") || (bill.KPZT == "P")))
            {
                this.toolStripButtonAdd.Enabled = false;
                this.toolStripButtonDel.Enabled = false;
            }
        }

        private void ToView()
        {
            this.textBox1.Text = this.bill.BH;
            this.textBox2.Text = this.bill.GFMC;
            this.textBox3.Text = this.bill.GFSH;
            this.textBox4.Text = this.bill.GFDZDH;
            this.textBox5.Text = this.bill.XFDZDH;
            this.textBox6.Text = this.bill.CM;
            this.textBox7.Text = this.bill.TYDH;
            this.textBox8.Text = this.bill.QYD;
            this.textBox9.Text = this.bill.DW;
            this.textBox10.Text = this.bill.XFYHZH;
            this.textBox11.Text = this.bill.FHR;
            this.textBox12.Text = this.bill.SKR;
            this.textBox13.Text = this.bill.YSHWXX;
            this.textBox15.Text = this.bill.JEHJ.ToString();
            this.textBox16.Text = this.bill.SLV.ToString();
            this.textBox17.Text = this.bill.BZ;
            this.dateTimePicker1.Text = this.bill.DJRQ.ToShortDateString();
            int count = this.bill.ListGoods.Count;
            for (int i = 0; i < count; i++)
            {
                string sPMC = this.bill.ListGoods[i].SPMC;
                string str2 = this.bill.ListGoods[i].JE.ToString();
                this.dgFyxm.Rows.Add();
                this.InvoiceKP.AddSpxx(null, "0.05", 0);
                this.InvoiceKP.SetSpmc(i, "0");
                this.InvoiceKP.SetJe(i, "1");
                this.dgFyxm.Rows[i].Cells[0].Value = sPMC;
                this.dgFyxm.Rows[i].Cells[1].Value = str2;
                this.UpdateJE();
            }
            if (count > 0)
            {
                this.toolStripButtonDel.Enabled = true;
            }
            this.ToStyle(this.bill);
        }

        private void UpdateJE()
        {
            double num = 0.0;
            int count = this.dgFyxm.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                string number = (this.dgFyxm.Rows[i].Cells[1].Value == null) ? "" : this.dgFyxm.Rows[i].Cells[1].Value.ToString();
                if (number != "")
                {
                    num += CommonTool.Todouble(number);
                }
            }
            this.textBox15.Text = num.ToString();
        }

        private void XSDJ_close()
        {
            this.textBox16.LostFocus -= new EventHandler(this.textBox16_LostFocus);
        }

        private void XSDJEdite_Load(object sender, EventArgs e)
        {
            this.InitHYDJ();
        }
    }
}

