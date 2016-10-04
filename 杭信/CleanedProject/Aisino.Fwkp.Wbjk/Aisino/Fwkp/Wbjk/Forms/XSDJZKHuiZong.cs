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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class XSDJZKHuiZong : BaseForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoDataGrid aisinoDataGrid2;
        private AisinoDataGrid aisinoDataGrid3;
        private SaleBill bill = null;
        private SaleBillCtrl billBL = SaleBillCtrl.Instance;
        private AisinoBTN btnQuery;
        private AisinoCMB comboBoxDJZL;
        private AisinoCMB comboBoxYF;
        private IContainer components = null;
        private DJZKHZbll hzBLL = new DJZKHZbll();
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoRDO rdDJBH;
        private AisinoRDO rdKHMC;
        private AisinoRDO rdKHSH;
        private string selectedBH;
        private AisinoSPL splitContainer1;
        private AisinoSPL splitContainer2;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnHZYL;
        private ToolStripButton toolStripBtnSave;
        private ToolStripButton toolStripButton1;
        private AisinoTXT txtDJBH;
        private AisinoTXT txtKHMC;
        private AisinoTXT txtKHSH;
        private XmlComponentLoader xmlComponentLoader1;

        public XSDJZKHuiZong()
        {
            this.Initialize();
            this.txtDJBH.GotFocus += new EventHandler(this.txtDJBH_GotFocus);
            this.txtKHMC.GotFocus += new EventHandler(this.txtDJBH_GotFocus);
            this.txtKHSH.GotFocus += new EventHandler(this.txtDJBH_GotFocus);
        }

        private void aisinoDataGrid1_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                this.SelectedBH = e.get_CurrentRow().Cells["BH"].Value.ToString().Trim();
                this.aisinoDataGrid2.set_DataSource(this.hzBLL.QueryXSDJMX(this.SelectedBH, this.hzBLL.Pagesize, this.hzBLL.CurrentPage));
                this.bill = this.billBL.Find(this.SelectedBH);
                int count = this.aisinoDataGrid2.get_Rows().Count;
                for (int i = 0; i < count; i++)
                {
                    string str = this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value.ToString();
                    string str2 = this.aisinoDataGrid2.get_Rows()[i].Cells["XH"].Value.ToString();
                    if (((str != null) && (str != "")) && (str != "中外合作油气田"))
                    {
                        string str3 = this.billBL.ShowSLV(this.bill, str2, str);
                        if (str3 != "")
                        {
                            this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value = str3;
                        }
                    }
                }
                string str4 = this.billBL.CollectDiscount(this.bill);
                if (str4 == "0")
                {
                    this.DisplayZKHZH();
                }
                else
                {
                    this.aisinoDataGrid3.set_DataSource(this.hzBLL.QueryXSDJMX("NoExist", this.hzBLL.Pagesize, 1));
                    HandleException.Log.Error("该单据不可以进行折扣汇总:" + str4);
                    MessageBoxHelper.Show(str4);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                this.hzBLL.Pagesize = e.get_PageSize();
                this.aisinoDataGrid1.set_DataSource(this.hzBLL.QueryXSDJ(this.comboBoxYF.SelectedItem.ToString(), this.comboBoxDJZL.SelectedValue.ToString(), this.GetKeyWord(), e.get_PageSize(), e.get_PageNO(), this.GetKey()));
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void aisinoDataGrid2_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                this.aisinoDataGrid2.set_DataSource(this.hzBLL.QueryXSDJMX(this.SelectedBH, e.get_PageSize(), e.get_PageNO()));
                int count = this.aisinoDataGrid2.get_Rows().Count;
                for (int i = 0; i < count; i++)
                {
                    string str = this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value.ToString();
                    string str2 = this.aisinoDataGrid2.get_Rows()[i].Cells["XH"].Value.ToString();
                    if (((str != null) && (str != "")) && (str != "中外合作油气田"))
                    {
                        string str3 = this.billBL.ShowSLV(this.bill, str2, str);
                        if (str3 != "")
                        {
                            this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value = str3;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void aisinoDataGrid3_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                if (this.SelectedBH != "NoExist")
                {
                    this.aisinoDataGrid3.set_DataSource(this.hzBLL.MXAfterZKHuiZong(e.get_PageSize(), e.get_PageNO()));
                    int count = this.aisinoDataGrid3.get_Rows().Count;
                    for (int i = 0; i < count; i++)
                    {
                        string str = this.aisinoDataGrid3.get_Rows()[i].Cells["SLV"].Value.ToString();
                        string str2 = this.aisinoDataGrid3.get_Rows()[i].Cells["XH"].Value.ToString();
                        if (((str != null) && (str != "")) && (str != "中外合作油气田"))
                        {
                            string str3 = this.billBL.ShowSLV(this.bill, str2, str);
                            if (str3 != "")
                            {
                                this.aisinoDataGrid3.get_Rows()[i].Cells["SLV"].Value = str3;
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

        private void BindDJ()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单据编号");
            item.Add("Property", "BH");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "True");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单据种类");
            item.Add("Property", "DJZL");
            item.Add("Type", "Text");
            item.Add("HeadAlign", "MiddleLeft");
            item.Add("Align", "MiddleLeft");
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
            item.Add("AisinoLBL", "备注");
            item.Add("Property", "BZ");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            this.aisinoDataGrid1.set_ColumeHead(list);
            this.aisinoDataGrid1.get_Columns()["JEHJ"].DefaultCellStyle.Format = "0.00";
            this.aisinoDataGrid1.set_DataSource(new AisinoDataSet());
        }

        private void BindHZHMX()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "序号");
            item.Add("Property", "XH");
            item.Add("Type", "Text");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "商品名称");
            item.Add("Property", "SPMC");
            item.Add("Type", "Text");
            item.Add("Width", "150");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "规格型号");
            item.Add("Property", "GGXH");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "数量");
            item.Add("Property", "SL");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单价");
            item.Add("Property", "DJ");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "金额");
            item.Add("Property", "JE");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLV");
            item.Add("Type", "Text");
            item.Add("Width", "50");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税额");
            item.Add("Property", "SE");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "计量单位");
            item.Add("Property", "JLDW");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "商品税目");
            item.Add("Property", "SPSM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
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
            this.aisinoDataGrid3.set_ColumeHead(list);
            this.aisinoDataGrid3.get_Columns()["JE"].DefaultCellStyle.Format = "0.00";
            this.aisinoDataGrid3.get_Columns()["SE"].DefaultCellStyle.Format = "0.00";
            this.aisinoDataGrid3.set_DataSource(new AisinoDataSet());
        }

        private void BindMingXi()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "序号");
            item.Add("Property", "XH");
            item.Add("Type", "Text");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "商品名称");
            item.Add("Property", "SPMC");
            item.Add("Type", "Text");
            item.Add("Width", "150");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "规格型号");
            item.Add("Property", "GGXH");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "数量");
            item.Add("Property", "SL");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单价");
            item.Add("Property", "DJ");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "金额");
            item.Add("Property", "JE");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLV");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税额");
            item.Add("Property", "SE");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "计量单位");
            item.Add("Property", "JLDW");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "商品税目");
            item.Add("Property", "SPSM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            item.Add("Visible", "False");
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
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string dJMonth = this.comboBoxYF.SelectedItem.ToString();
                string dJType = this.comboBoxDJZL.SelectedValue.ToString();
                string keyWord = this.GetKeyWord();
                int key = this.GetKey();
                AisinoDataSet set = this.hzBLL.QueryXSDJ(dJMonth, dJType, keyWord, this.hzBLL.Pagesize, this.hzBLL.CurrentPage, key);
                this.aisinoDataGrid1.set_DataSource(set);
                this.SelectedBH = "NoExist";
                this.aisinoDataGrid2.set_DataSource(this.hzBLL.QueryXSDJMX("NoExist", this.hzBLL.Pagesize, 1));
                this.aisinoDataGrid3.set_DataSource(this.hzBLL.QueryXSDJMX("NoExist", this.hzBLL.Pagesize, 1));
                int count = this.aisinoDataGrid2.get_Rows().Count;
                for (int i = 0; i < count; i++)
                {
                    string str4 = this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value.ToString();
                    string str5 = this.aisinoDataGrid2.get_Rows()[i].Cells["XH"].Value.ToString();
                    if (((str4 != null) && (str4 != "")) && (str4 != "中外合作油气田"))
                    {
                        string str6 = this.billBL.ShowSLV(this.bill, str5, str4);
                        if (str6 != "")
                        {
                            this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value = str6;
                        }
                    }
                }
                if (set.get_Data().Rows.Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-272203");
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void DisplayZKHZH()
        {
            this.aisinoDataGrid3.set_DataSource(this.hzBLL.MXAfterZKHuiZong(this.hzBLL.Pagesize, this.hzBLL.CurrentPage));
            int count = this.aisinoDataGrid3.get_Rows().Count;
            for (int i = 0; i < count; i++)
            {
                string str = this.aisinoDataGrid3.get_Rows()[i].Cells["SLV"].Value.ToString();
                string str2 = this.aisinoDataGrid3.get_Rows()[i].Cells["XH"].Value.ToString();
                if (((str != null) && (str != "")) && (str != "中外合作油气田"))
                {
                    string str3 = this.billBL.ShowSLV(this.bill, str2, str);
                    if (str3 != "")
                    {
                        this.aisinoDataGrid3.get_Rows()[i].Cells["SLV"].Value = str3;
                    }
                }
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

        private void DJZKHZ_Load(object sender, EventArgs e)
        {
            try
            {
                this.comboBoxYF.Items.AddRange(this.billBL.SaleBillMonth());
                this.comboBoxYF.SelectedIndex = 0;
                this.comboBoxDJZL.DataSource = CbbXmlBind.ReadXmlNode("InvType", false);
                this.comboBoxDJZL.DisplayMember = "Value";
                this.comboBoxDJZL.ValueMember = "Key";
                this.BindDJ();
                this.BindMingXi();
                this.BindHZHMX();
                this.aisinoDataGrid1.set_MultiSelect(false);
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private int GetKey()
        {
            int num = 0;
            if (this.rdKHMC.Checked)
            {
                num = 2;
            }
            if (this.rdKHSH.Checked)
            {
                num = 3;
            }
            if (this.rdDJBH.Checked)
            {
                num = 1;
            }
            return num;
        }

        private string GetKeyWord()
        {
            string str = "";
            if (this.rdKHMC.Checked)
            {
                str = this.txtKHMC.Text.Trim();
            }
            if (this.rdKHSH.Checked)
            {
                str = this.txtKHSH.Text.Trim();
            }
            if (this.rdDJBH.Checked)
            {
                str = this.txtDJBH.Text.Trim();
            }
            return str;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.btnQuery = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnQuery");
            this.comboBoxYF = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxYF");
            this.comboBoxDJZL = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxDJZL");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.toolStripButton1 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton1");
            this.toolStripBtnSave = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnSave");
            this.toolStripBtnHZYL = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnHZYL");
            this.splitContainer1 = this.xmlComponentLoader1.GetControlByName<AisinoSPL>("splitContainer1");
            this.splitContainer2 = this.xmlComponentLoader1.GetControlByName<AisinoSPL>("splitContainer2");
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.aisinoDataGrid2 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid2");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.aisinoDataGrid3 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid3");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.txtKHSH = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtKHSH");
            this.rdKHSH = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rdKHSH");
            this.txtKHMC = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtKHMC");
            this.rdKHMC = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rdKHMC");
            this.txtDJBH = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtDJBH");
            this.rdDJBH = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rdDJBH");
            this.toolStripBtnHZYL.Visible = false;
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.toolStripBtnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.aisinoDataGrid1.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent));
            this.aisinoDataGrid2.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid2_GoToPageEvent));
            this.aisinoDataGrid3.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid3_GoToPageEvent));
            this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
            this.aisinoDataGrid1.add_DataGridRowClickEvent(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowClickEvent));
            this.comboBoxDJZL.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxYF.DropDownStyle = ComboBoxStyle.DropDownList;
            this.aisinoDataGrid1.set_ReadOnly(true);
            this.aisinoDataGrid1.get_DataGrid().AllowUserToDeleteRows = false;
            this.aisinoDataGrid2.set_ReadOnly(true);
            this.aisinoDataGrid2.get_DataGrid().AllowUserToDeleteRows = false;
            this.aisinoDataGrid3.set_ReadOnly(true);
            this.aisinoDataGrid3.get_DataGrid().AllowUserToDeleteRows = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(XSDJZKHuiZong));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x333, 0x242);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "销售单据折扣汇总";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.XSDJZKHuiZong\Aisino.Fwkp.Wbjk.XSDJZKHuiZong.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x333, 0x242);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "XSDJZKHuiZong";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "销售单据折扣汇总";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.DJZKHZ_Load);
            base.ResumeLayout(false);
        }

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoDataGrid1.get_SelectedRows().Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-272502");
                }
                else if (this.aisinoDataGrid3.get_Rows().Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-272506");
                }
                else if (MessageManager.ShowMsgBox("INP-272503") != DialogResult.Cancel)
                {
                    int num2;
                    string str = "";
                    double num = 0.0;
                    for (num2 = 0; num2 < this.bill.ListGoods.Count; num2++)
                    {
                        int num7;
                        Goods goods = this.bill.ListGoods[num2];
                        double round = 0.0;
                        double num4 = 0.0;
                        if (goods.DJHXZ == 4)
                        {
                            continue;
                        }
                        if (this.bill.HYSY && (goods.SLV == 0.05))
                        {
                            round = SaleBillCtrl.GetRound((double) (((goods.JE / 0.95) * 0.05) - goods.SE), 2);
                            if (Math.Abs(round) > 0.06)
                            {
                                num7 = num2 + 1;
                                str = "第" + num7.ToString() + "行，含税金额乘以税率减税额的绝对值大于0.06，不能进行折扣汇总";
                            }
                            else
                            {
                                if ((goods.DJ == 0.0) || (goods.SL == 0.0))
                                {
                                    goto Label_0544;
                                }
                                num4 = Math.Abs(SaleBillCtrl.GetRound((double) (((goods.DJ * goods.SL) - goods.JE) - goods.SE), 2));
                                if (goods.HSJBZ)
                                {
                                    if (num4 > 0.01)
                                    {
                                        num7 = num2 + 1;
                                        str = "第" + num7.ToString() + "行，单价乘以数量不等于含税金额，不能进行折扣汇总";
                                        break;
                                    }
                                    goto Label_0544;
                                }
                                num7 = num2 + 1;
                                str = "第" + num7.ToString() + "行，单价乘以数量不等于含税金额，不能进行折扣汇总";
                            }
                            break;
                        }
                        if (this.bill.JZ_50_15 && (goods.SLV == 0.015))
                        {
                            round = SaleBillCtrl.GetRound((double) (((goods.JE / 1.035) * 0.015) - goods.SE), 2);
                            if (Math.Abs(round) > 0.06)
                            {
                                num7 = num2 + 1;
                                str = "第" + num7.ToString() + "行，金额与税率税额计算后误差的绝对值大于0.06，不能进行折扣汇总";
                                break;
                            }
                            if ((goods.DJ != 0.0) && (goods.SL != 0.0))
                            {
                                if (goods.HSJBZ)
                                {
                                    if (Math.Abs(SaleBillCtrl.GetRound((double) (((goods.DJ * goods.SL) - goods.JE) - goods.SE), 2)) > 0.01)
                                    {
                                        num7 = num2 + 1;
                                        str = "第" + num7.ToString() + "行，单价乘以数量不等于含税金额，不能进行折扣汇总";
                                        break;
                                    }
                                }
                                else if (Math.Abs(SaleBillCtrl.GetRound((double) ((goods.DJ * goods.SL) - goods.JE), 2)) > 0.01)
                                {
                                    num7 = num2 + 1;
                                    str = "第" + num7.ToString() + "行，单价乘以数量不等于金额，不能进行折扣汇总";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            round = SaleBillCtrl.GetRound((double) ((goods.JE * goods.SLV) - goods.SE), 2);
                            if (Math.Abs(round) > 0.06)
                            {
                                num7 = num2 + 1;
                                str = "第" + num7.ToString() + "行，金额乘以税率减税额的绝对值大于0.06，不能进行折扣汇总";
                                break;
                            }
                            if ((goods.DJ != 0.0) && (goods.SL != 0.0))
                            {
                                if (goods.HSJBZ)
                                {
                                    if (Math.Abs(SaleBillCtrl.GetRound((double) (((goods.DJ * goods.SL) - goods.JE) - goods.SE), 2)) > 0.01)
                                    {
                                        num7 = num2 + 1;
                                        str = "第" + num7.ToString() + "行，单价乘以数量不等于含税金额，不能进行折扣汇总";
                                        break;
                                    }
                                }
                                else if (Math.Abs(SaleBillCtrl.GetRound((double) ((goods.DJ * goods.SL) - goods.JE), 2)) > 0.01)
                                {
                                    str = "第" + ((num2 + 1)).ToString() + "行，单价乘以数量不等于金额，不能进行折扣汇总";
                                    break;
                                }
                            }
                        }
                    Label_0544:
                        num += round;
                    }
                    if (str.Length > 0)
                    {
                        MessageManager.ShowMsgBox(str);
                    }
                    else
                    {
                        if (Math.Abs(SaleBillCtrl.GetRound(num, 2)) > 1.27)
                        {
                            str = "单据税额误差累计值大于1.27，不能进行折扣汇总";
                        }
                        if (str.Length > 0)
                        {
                            MessageManager.ShowMsgBox(str);
                        }
                        else
                        {
                            string str2 = new SaleBillDAL().SaveCollectDiscountToRealTable(this.bill);
                            if (str2 == "0")
                            {
                                string dJMonth = this.comboBoxYF.SelectedItem.ToString();
                                string dJType = this.comboBoxDJZL.SelectedValue.ToString();
                                string keyWord = this.GetKeyWord();
                                int key = this.GetKey();
                                this.aisinoDataGrid1.set_DataSource(this.hzBLL.QueryXSDJ(dJMonth, dJType, keyWord, this.hzBLL.Pagesize, this.hzBLL.CurrentPage, key));
                                this.SelectedBH = "NoExist";
                                this.aisinoDataGrid2.set_DataSource(this.hzBLL.QueryXSDJMX("NoExist", this.hzBLL.Pagesize, 1));
                                this.aisinoDataGrid3.set_DataSource(this.hzBLL.QueryXSDJMX("NoExist", this.hzBLL.Pagesize, 1));
                                int count = this.aisinoDataGrid2.get_Rows().Count;
                                for (num2 = 0; num2 < count; num2++)
                                {
                                    string str6 = this.aisinoDataGrid2.get_Rows()[num2].Cells["SLV"].Value.ToString();
                                    string str7 = this.aisinoDataGrid2.get_Rows()[num2].Cells["XH"].Value.ToString();
                                    if (((str6 != null) && (str6 != "")) && (str6 != "中外合作油气田"))
                                    {
                                        string str8 = this.billBL.ShowSLV(this.bill, str7, str6);
                                        if (str8 != "")
                                        {
                                            this.aisinoDataGrid2.get_Rows()[num2].Cells["SLV"].Value = str8;
                                        }
                                    }
                                }
                                MessageManager.ShowMsgBox("INP-272504");
                            }
                            else if (str2 == "NoBH")
                            {
                                MessageManager.ShowMsgBox("INP-272505");
                            }
                            else
                            {
                                MessageBoxHelper.Show(str2);
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void txtDJBH_GotFocus(object sender, EventArgs e)
        {
            this.rdKHMC.Checked = false;
            this.rdKHSH.Checked = false;
            this.rdDJBH.Checked = false;
            AisinoTXT otxt = (AisinoTXT) sender;
            if (otxt.Name == "txtDJBH")
            {
                this.rdDJBH.Checked = true;
                this.txtKHMC.Text = "";
                this.txtKHSH.Text = "";
            }
            if (otxt.Name == "txtKHMC")
            {
                this.rdKHMC.Checked = true;
                this.txtDJBH.Text = "";
                this.txtKHSH.Text = "";
            }
            if (otxt.Name == "txtKHSH")
            {
                this.rdKHSH.Checked = true;
                this.txtDJBH.Text = "";
                this.txtKHMC.Text = "";
            }
        }

        public string SelectedBH
        {
            get
            {
                return this.selectedBH;
            }
            set
            {
                this.selectedBH = value;
            }
        }
    }
}

