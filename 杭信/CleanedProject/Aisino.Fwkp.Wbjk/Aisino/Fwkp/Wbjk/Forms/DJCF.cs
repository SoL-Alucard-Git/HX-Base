namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
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

    public class DJCF : BaseForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoDataGrid aisinoDataGrid2;
        private SaleBillCtrl billBL = SaleBillCtrl.Instance;
        private AisinoBTN button1;
        private AisinoCHK checkBox1;
        private AisinoCHK checkBox2;
        private AisinoCMB comboBoxDJZL;
        private AisinoCMB comboBoxJYGZ;
        private AisinoCMB comboBoxYF;
        private IContainer components = null;
        private AisinoDataSet dataSet = null;
        private DJCFdal djcfBLL = new DJCFdal();
        private string DJmonth;
        private string DJtype;
        private string Grid1BH;
        private string JYrule;
        private string KeyWords;
        private AisinoRDO radioButton1;
        private AisinoRDO radioButton2;
        private StatusStrip statusStrip1;
        private AisinoTXT textBoxKey;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnSD;
        private ToolStripButton toolStripBtnZD;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private XmlComponentLoader xmlComponentLoader1;

        public DJCF()
        {
            this.Initialize();
            if (TaxCardFactory.CreateTaxCard().get_StateInfo().CompanyType == 0)
            {
                this.checkBox2.Visible = false;
            }
        }

        private void aisinoDataGrid1_DataGridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (this.aisinoDataGrid1.get_Columns()[e.ColumnIndex].Name)
            {
                case "KPZT":
                    e.Value = ShowString.ShowKPZT(e.Value.ToString());
                    break;

                case "DJZT":
                    e.Value = ShowString.ShowDJZT(e.Value.ToString());
                    break;

                case "DJZL":
                    e.Value = ShowString.ShowFPZL(e.Value.ToString());
                    break;

                case "SFZJY":
                    e.Value = ShowString.ShowBool(e.Value.ToString());
                    break;

                case "HYSY":
                    e.Value = ShowString.ShowBool(e.Value.ToString());
                    break;

                case "HSJBZ":
                    e.Value = ShowString.ShowBool(e.Value.ToString());
                    break;
            }
        }

        private void aisinoDataGrid1_DataGridRowSelectionChanged(object sender, DataGridRowEventArgs e)
        {
            try
            {
                this.Grid1BH = e.get_CurrentRow().Cells["BH"].Value.ToString().Trim();
                this.dataSet = this.djcfBLL.QueryXSDJMX(this.Grid1BH, this.djcfBLL.Pagesize, 1);
                this.aisinoDataGrid2.set_DataSource(this.dataSet);
                SaleBill bill = this.billBL.Find(this.Grid1BH);
                int count = this.aisinoDataGrid2.get_Rows().Count;
                for (int i = 0; i < count; i++)
                {
                    string str = this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value.ToString();
                    string str2 = this.aisinoDataGrid2.get_Rows()[i].Cells["XH"].Value.ToString();
                    if (((str != null) && (str != "")) && (str != "中外合作油气田"))
                    {
                        string str3 = this.billBL.ShowSLV(bill, str2, str);
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

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                this.djcfBLL.Pagesize = e.get_PageSize();
                string str = e.get_PageNO().ToString();
                PropertyUtil.SetValue("WBJK_DJCF_DATAGRID1", str);
                if (this.JYrule == "s")
                {
                    this.aisinoDataGrid1.remove_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
                    this.CheckAdd(e.get_PageSize(), e.get_PageNO());
                    this.aisinoDataGrid1.add_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
                }
                else
                {
                    this.aisinoDataGrid1.set_DataSource(this.djcfBLL.QueryXSDJ(this.KeyWords, this.DJmonth, this.DJtype, this.JYrule, e.get_PageSize(), e.get_PageNO()));
                }
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
                this.djcfBLL.Pagesize = e.get_PageSize();
                this.aisinoDataGrid2.set_DataSource(this.djcfBLL.QueryXSDJMX(this.Grid1BH, e.get_PageSize(), e.get_PageNO()));
                SaleBill bill = this.billBL.Find(this.Grid1BH);
                int count = this.aisinoDataGrid2.get_Rows().Count;
                for (int i = 0; i < count; i++)
                {
                    string str = this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value.ToString();
                    string str2 = this.aisinoDataGrid2.get_Rows()[i].Cells["XH"].Value.ToString();
                    if (((str != null) && (str != "")) && (str != "中外合作油气田"))
                    {
                        string str3 = this.billBL.ShowSLV(bill, str2, str);
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

        private void BindMingXi()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "序号");
            item.Add("Property", "XH");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "商品名称");
            item.Add("Property", "SPMC");
            item.Add("Type", "Text");
            item.Add("Width", "120");
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
            item.Add("Width", "80");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单价");
            item.Add("Property", "DJ");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "金额");
            item.Add("Property", "JE");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLV");
            item.Add("Type", "Text");
            item.Add("Width", "80");
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
            item.Add("Width", "80");
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
            item.Add("Visible", "False");
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
                this.KeyWords = this.textBoxKey.Text.Trim();
                this.DJmonth = this.comboBoxYF.SelectedItem.ToString();
                this.DJtype = this.comboBoxDJZL.SelectedValue.ToString();
                this.JYrule = this.comboBoxJYGZ.SelectedValue.ToString();
                int result = 1;
                int.TryParse(PropertyUtil.GetValue("WBJK_DJCF_DATAGRID1"), out result);
                this.djcfBLL.CurrentPage = result;
                if (this.JYrule == "s")
                {
                    this.aisinoDataGrid1.remove_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
                    this.CheckAdd(this.djcfBLL.Pagesize, this.djcfBLL.CurrentPage);
                    this.aisinoDataGrid1.add_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
                }
                else
                {
                    this.dataSet = this.djcfBLL.QueryXSDJ(this.KeyWords, this.DJmonth, this.DJtype, this.JYrule, this.djcfBLL.Pagesize, this.djcfBLL.CurrentPage);
                    this.aisinoDataGrid1.set_DataSource(this.dataSet);
                }
                if (this.aisinoDataGrid1.get_DataSource().get_Data().Rows.Count == 0)
                {
                    this.aisinoDataGrid2.set_DataSource(this.djcfBLL.QueryXSDJMX("NoExcitBH", this.djcfBLL.Pagesize, 1));
                    MessageManager.ShowMsgBox("INP-272203");
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void CheckAdd(int pagesize, int page)
        {
            int num2;
            string str6;
            this.aisinoDataGrid1.set_DataSource(this.djcfBLL.QueryXSDJ("NoExistBH", "0", "a", "a", 10, 0));
            if (this.aisinoDataGrid1.get_DataSource() == null)
            {
                this.aisinoDataGrid1.set_DataSource(this.djcfBLL.QueryXSDJ(this.KeyWords, this.DJmonth, this.DJtype, this.JYrule, pagesize, page));
            }
            this.aisinoDataGrid1.get_DataSource().get_Data().Rows.Clear();
            AisinoDataSet set = this.djcfBLL.QueryXSDJ(this.KeyWords, this.DJmonth, this.DJtype, this.JYrule, pagesize, page);
            AisinoDataSet set2 = null;
            int count = set.get_Data().Rows.Count;
            for (num2 = 0; num2 < count; num2++)
            {
                string xSDJBH = set.get_Data().Rows[num2]["BH"].ToString();
                string str2 = set.get_Data().Rows[num2]["DJZL"].ToString();
                set2 = this.djcfBLL.QueryXSDJMX(xSDJBH, 0x3e8, 1);
                int num3 = set2.get_Data().Rows.Count;
                double round = 0.0;
                bool flag = false;
                for (int i = 0; i < num3; i++)
                {
                    double num11;
                    double num12;
                    string str3 = set2.get_Data().Rows[i]["DJ"].ToString();
                    string str4 = set2.get_Data().Rows[i]["SL"].ToString();
                    string str5 = set2.get_Data().Rows[i]["JE"].ToString();
                    str6 = set2.get_Data().Rows[i]["SLV"].ToString();
                    string str7 = set2.get_Data().Rows[i]["SE"].ToString();
                    double num6 = 0.0;
                    double num7 = 0.0;
                    if ((str3.Length > 0) && (str4.Length > 0))
                    {
                        num6 = Convert.ToDouble(str3);
                        num7 = Convert.ToDouble(str4);
                    }
                    double num8 = 0.0;
                    if (str5.Length > 0)
                    {
                        num8 = Convert.ToDouble(str5);
                    }
                    double num9 = 0.0;
                    if (str6.Length > 0)
                    {
                        if (str6.Equals("免税"))
                        {
                            num9 = 0.0;
                        }
                        else if (str6.Equals("中外合作油气田"))
                        {
                            num9 = 0.05;
                        }
                        else
                        {
                            num9 = Convert.ToDouble(str6);
                        }
                    }
                    double num10 = 0.0;
                    if (str7.Length > 0)
                    {
                        num10 = Convert.ToDouble(str7);
                    }
                    if (str6.Equals("中外合作油气田"))
                    {
                        num11 = 0.0;
                        if ((num6 == 0.0) && (num7 == 0.0))
                        {
                            num11 = SaleBillCtrl.GetRound((double) ((num8 / 0.95) * 0.05), 2);
                        }
                        else
                        {
                            num11 = SaleBillCtrl.GetRound((double) ((num6 * num7) * num9), 2);
                        }
                        num12 = SaleBillCtrl.GetRound((double) (num11 - num10), 2);
                        if (Math.Abs(num12) > 0.06)
                        {
                            this.aisinoDataGrid1.get_DataSource().get_Data().Rows.Add(set.get_Data().Rows[num2].ItemArray);
                            flag = true;
                            break;
                        }
                        round += num12;
                    }
                    else if (num9 == 0.015)
                    {
                        num11 = 0.0;
                        num12 = SaleBillCtrl.GetRound((double) (SaleBillCtrl.GetRound((double) ((num8 / 1.035) * 0.015), 2) - num10), 2);
                        if (Math.Abs(num12) > 0.06)
                        {
                            this.aisinoDataGrid1.get_DataSource().get_Data().Rows.Add(set.get_Data().Rows[num2].ItemArray);
                            flag = true;
                            break;
                        }
                        round += num12;
                    }
                    else
                    {
                        num12 = SaleBillCtrl.GetRound((double) (SaleBillCtrl.GetRound((double) (num8 * num9), 2) - num10), 2);
                        if (Math.Abs(num12) > 0.06)
                        {
                            this.aisinoDataGrid1.get_DataSource().get_Data().Rows.Add(set.get_Data().Rows[num2].ItemArray);
                            flag = true;
                            break;
                        }
                        round += num12;
                    }
                }
                round = SaleBillCtrl.GetRound(round, 2);
                if (!flag && (Math.Abs(round) > 1.27))
                {
                    this.aisinoDataGrid1.get_DataSource().get_Data().Rows.Add(set.get_Data().Rows[num2].ItemArray);
                }
            }
            if (this.aisinoDataGrid1.get_DataSource().get_Data().Rows.Count > 0)
            {
                this.Grid1BH = this.aisinoDataGrid1.get_DataSource().get_Data().Rows[0]["BH"].ToString();
                set2 = this.djcfBLL.QueryXSDJMX(this.Grid1BH, this.djcfBLL.Pagesize, 1);
                this.aisinoDataGrid2.set_DataSource(set2);
                SaleBill bill = this.billBL.Find(this.Grid1BH);
                int num13 = this.aisinoDataGrid2.get_Rows().Count;
                for (num2 = 0; num2 < num13; num2++)
                {
                    string str8 = this.aisinoDataGrid2.get_Rows()[num2].Cells["SLV"].Value.ToString();
                    string str9 = this.aisinoDataGrid2.get_Rows()[num2].Cells["XH"].Value.ToString();
                    if (((str8 != null) && (str8 != "")) && (str8 != "中外合作油气田"))
                    {
                        str6 = this.billBL.ShowSLV(bill, str9, str8);
                        if (str6 != "")
                        {
                            this.aisinoDataGrid2.get_Rows()[num2].Cells["SLV"].Value = str6;
                        }
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

        private void DJCF_Load(object sender, EventArgs e)
        {
            try
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
                item.Add("Visible", "True");
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
                item.Add("Width", "90");
                item.Add("Align", "MiddleCenter");
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
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "单据状态");
                item.Add("Property", "DJZT");
                item.Add("Type", "Text");
                item.Add("Visible", "False");
                list.Add(item);
                this.aisinoDataGrid1.set_ColumeHead(list);
                this.aisinoDataGrid1.set_MultiSelect(false);
                this.aisinoDataGrid1.get_Columns()["DJRQ"].DefaultCellStyle.Format = "yyyy-MM-dd";
                this.aisinoDataGrid1.get_Columns()["JEHJ"].DefaultCellStyle.Format = "0.00";
                this.aisinoDataGrid1.set_DataSource(new AisinoDataSet());
                this.comboBoxYF.Items.AddRange(this.billBL.SaleBillMonth());
                this.comboBoxYF.SelectedIndex = 0;
                this.comboBoxDJZL.DataSource = CbbXmlBind.ReadXmlNode("InvType", false);
                this.comboBoxDJZL.DisplayMember = "Value";
                this.comboBoxDJZL.ValueMember = "Key";
                this.comboBoxJYGZ.DataSource = CbbXmlBind.ReadXmlNode("JYGZ", false);
                this.comboBoxJYGZ.DisplayMember = "Value";
                this.comboBoxJYGZ.ValueMember = "Key";
                this.BindMingXi();
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripButton1 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton1");
            this.toolStripBtnZD = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnZD");
            this.toolStripBtnSD = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnSD");
            this.toolStripButton2 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton2");
            this.textBoxKey = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxKey");
            this.comboBoxYF = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxYF");
            this.comboBoxDJZL = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxDJZL");
            this.button1 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button1");
            this.comboBoxJYGZ = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxJYGZ");
            this.radioButton2 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioButton2");
            this.radioButton1 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioButton1");
            this.checkBox1 = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox1");
            this.statusStrip1 = this.xmlComponentLoader1.GetControlByName<StatusStrip>("statusStrip1");
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.aisinoDataGrid2 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid2");
            this.checkBox2 = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox2");
            this.comboBoxJYGZ.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxYF.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxDJZL.DropDownStyle = ComboBoxStyle.DropDownList;
            this.toolStripBtnSD.Click += new EventHandler(this.toolBtnSDCF_Click);
            this.toolStripButton2.Click += new EventHandler(this.toolBtnZDCF_Click);
            this.textBoxKey.TextChanged += new EventHandler(this.textBoxKey_TextChanged);
            this.button1.Click += new EventHandler(this.btnQuery_Click);
            this.aisinoDataGrid1.add_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
            this.aisinoDataGrid1.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent));
            this.aisinoDataGrid1.add_DataGridCellFormatting(new EventHandler<DataGridViewCellFormattingEventArgs>(this.aisinoDataGrid1_DataGridCellFormatting));
            this.toolStripButton1.Click += new EventHandler(this.toolBtnClose_Click);
            this.aisinoDataGrid2.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid2_GoToPageEvent));
            this.aisinoDataGrid1.set_ReadOnly(true);
            this.aisinoDataGrid1.get_DataGrid().AllowUserToDeleteRows = false;
            this.aisinoDataGrid2.set_ReadOnly(true);
            this.aisinoDataGrid2.get_DataGrid().AllowUserToDeleteRows = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DJCF));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x335, 0x22b);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "销售单据拆分";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.DJCF\Aisino.Fwkp.Wbjk.DJCF.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x335, 0x22b);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "DJCF";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "销售单据拆分";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.DJCF_Load);
            base.ResumeLayout(false);
        }

        private void textBoxKey_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.KeyWords = this.textBoxKey.Text.Trim();
                this.DJmonth = this.comboBoxYF.SelectedItem.ToString();
                this.DJtype = this.comboBoxDJZL.SelectedValue.ToString();
                this.JYrule = this.comboBoxJYGZ.SelectedValue.ToString();
                if (this.JYrule == "s")
                {
                    this.aisinoDataGrid1.remove_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
                    this.CheckAdd(this.djcfBLL.Pagesize, this.djcfBLL.CurrentPage);
                    this.aisinoDataGrid1.add_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
                }
                else
                {
                    this.dataSet = this.djcfBLL.QueryXSDJ(this.KeyWords, this.DJmonth, this.DJtype, this.JYrule, this.djcfBLL.Pagesize, this.djcfBLL.CurrentPage);
                    this.aisinoDataGrid1.set_DataSource(this.dataSet);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void toolBtnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolBtnSDCF_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.aisinoDataGrid1.get_SelectedRows().Count != 0) && (this.aisinoDataGrid1.get_SelectedRows().Count == 1))
                {
                    string bH = this.aisinoDataGrid1.get_SelectedRows()[0].Cells["BH"].Value.ToString();
                    SaleBill bill = this.billBL.Find(bH);
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    if (bill.DJZL == "c")
                    {
                        if (!card.get_QYLX().ISPTFP)
                        {
                            MessageManager.ShowMsgBox("无法取得普通发票开票限额，不能拆分!");
                            return;
                        }
                    }
                    else if ((bill.DJZL == "s") && !card.get_QYLX().ISZYFP)
                    {
                        MessageManager.ShowMsgBox("无法取得专用发票开票限额，不能拆分!");
                        return;
                    }
                    string reason = "";
                    if (this.billBL.CheckBillRecordCF(bill, "MT", true, ref reason) != "Need")
                    {
                        MessageBoxHelper.Show(reason, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        string str4 = this.billBL.CheckBeforeCF(bill);
                        if (str4 != "0")
                        {
                            string str5;
                            if (str4.StartsWith("[error_hs]"))
                            {
                                str5 = str4.Substring(10, str4.Length - 10);
                                MessageManager.ShowMsgBox("第" + str5 + "行，单价乘以数量不等于含税金额!");
                            }
                            else if (str4.StartsWith("[error_bhs]"))
                            {
                                str5 = str4.Substring(11, str4.Length - 11);
                                MessageManager.ShowMsgBox("第" + str5 + "行，单价乘以数量不等于金额!");
                            }
                            else if (str4.StartsWith("[error_1]"))
                            {
                                MessageManager.ShowMsgBox("第" + str4.Substring(9, str4.Length - 9) + "行，商品数据非法!");
                            }
                        }
                        else
                        {
                            MannualSplit split = new MannualSplit(bill);
                            if (split.ShowDialog() == DialogResult.OK)
                            {
                                if (this.JYrule == "s")
                                {
                                    this.aisinoDataGrid1.remove_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
                                    this.CheckAdd(this.djcfBLL.Pagesize, this.djcfBLL.CurrentPage);
                                    this.aisinoDataGrid1.add_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
                                }
                                else
                                {
                                    this.aisinoDataGrid1.set_DataSource(this.djcfBLL.QueryXSDJ(this.KeyWords, this.DJmonth, this.DJtype, this.JYrule, this.djcfBLL.Pagesize, this.djcfBLL.CurrentPage));
                                }
                                if (this.aisinoDataGrid1.get_Rows().Count == 0)
                                {
                                    this.aisinoDataGrid2.set_DataSource(this.djcfBLL.QueryXSDJMX("noexist", this.djcfBLL.Pagesize, 1));
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

        private void toolBtnZDCF_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoDataGrid1.get_SelectedRows().Count != 0)
                {
                    if (this.aisinoDataGrid1.get_SelectedRows().Count == 1)
                    {
                        string bH = this.aisinoDataGrid1.get_SelectedRows()[0].Cells["BH"].Value.ToString();
                        SaleBill bill = this.billBL.Find(bH);
                        InvType common = InvType.Common;
                        TaxCard card = TaxCardFactory.CreateTaxCard();
                        if (bill.DJZL == "c")
                        {
                            if (!card.get_QYLX().ISPTFP)
                            {
                                MessageManager.ShowMsgBox("无法取得普通发票开票限额，不能拆分!");
                                return;
                            }
                            common = InvType.Common;
                        }
                        else if (bill.DJZL == "s")
                        {
                            if (!card.get_QYLX().ISZYFP)
                            {
                                MessageManager.ShowMsgBox("无法取得专用发票开票限额，不能拆分!");
                                return;
                            }
                            common = InvType.Special;
                        }
                        double invAmountLimit = 0.0;
                        int count = card.get_SQInfo().PZSQType.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (card.get_SQInfo().PZSQType[i].invType == 2)
                            {
                                invAmountLimit = card.get_SQInfo().PZSQType[i].InvAmountLimit;
                            }
                            if (card.get_SQInfo().PZSQType[i].invType == 0)
                            {
                                invAmountLimit = card.get_SQInfo().PZSQType[i].InvAmountLimit;
                            }
                        }
                        bool hzfw = card.get_StateInfo().CompanyType > 0;
                        bool exEWMInfoSplit = false;
                        if (this.checkBox2.Checked)
                        {
                            if ((bill.DJZL == "c") && (bill.QDHSPMC.Length > 0))
                            {
                                exEWMInfoSplit = false;
                            }
                            else
                            {
                                exEWMInfoSplit = true;
                            }
                        }
                        else if (bill.DJZL == "s")
                        {
                            exEWMInfoSplit = true;
                        }
                        if (!hzfw)
                        {
                            exEWMInfoSplit = true;
                        }
                        int[] slvjeseIndex = GenerateInvoice.Instance.SetInvoiceTimes();
                        string reason = "";
                        string str3 = this.billBL.CheckBillRecordCF(bill, "AT", exEWMInfoSplit, ref reason, slvjeseIndex, hzfw, false);
                        if (str3 == "Needless")
                        {
                            if (reason.Equals("差额税单据不允许拆分"))
                            {
                                MessageManager.ShowMsgBox(reason);
                            }
                            else
                            {
                                MessageManager.ShowMsgBox("INP-272204");
                            }
                        }
                        else if (str3 != "Need")
                        {
                            str3 = "该单据不能进行自动拆分!" + str3.Replace("HasWrong", "");
                            MessageBoxHelper.Show(reason, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            SeparateType keepMaxJE;
                            bool mergeSmallJE = false;
                            if (this.radioButton1.Checked)
                            {
                                keepMaxJE = SeparateType.KeepMaxJE;
                            }
                            else
                            {
                                keepMaxJE = SeparateType.KeepMaxSL;
                            }
                            if (this.checkBox1.Checked)
                            {
                                mergeSmallJE = true;
                            }
                            string str4 = this.billBL.AutoSeparate(bill, keepMaxJE, mergeSmallJE, exEWMInfoSplit, slvjeseIndex, hzfw);
                            if (str4 == "0")
                            {
                                DJCFAuto auto = new DJCFAuto(bill, this.djcfBLL);
                                if (auto.ShowDialog() == DialogResult.OK)
                                {
                                    if (this.JYrule == "s")
                                    {
                                        this.aisinoDataGrid1.remove_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
                                        this.CheckAdd(this.djcfBLL.Pagesize, this.djcfBLL.CurrentPage);
                                        this.aisinoDataGrid1.add_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
                                    }
                                    else
                                    {
                                        this.dataSet = this.djcfBLL.QueryXSDJ(this.KeyWords, this.DJmonth, this.DJtype, this.JYrule, this.djcfBLL.Pagesize, this.djcfBLL.CurrentPage);
                                        this.aisinoDataGrid1.set_DataSource(this.dataSet);
                                    }
                                }
                                if (this.aisinoDataGrid1.get_Rows().Count == 0)
                                {
                                    this.aisinoDataGrid2.set_DataSource(this.djcfBLL.QueryXSDJMX("noexist", this.djcfBLL.Pagesize, 1));
                                }
                            }
                            else
                            {
                                string str6;
                                string str5 = "该单据不能进行自动拆分!";
                                switch (str4)
                                {
                                    case "-1":
                                        MessageManager.ShowMsgBox("折扣组超过拆分限制，不允许拆分!");
                                        return;

                                    case "-2":
                                        MessageManager.ShowMsgBox("非清单发票商品名称中含有“清单”字样，不允许拆分!");
                                        return;

                                    case "-3":
                                        MessageManager.ShowMsgBox(str5);
                                        return;

                                    case "-4":
                                        MessageManager.ShowMsgBox(str5);
                                        return;

                                    case "-5":
                                        MessageManager.ShowMsgBox(str5);
                                        return;

                                    case "-6":
                                        MessageManager.ShowMsgBox("INP-272204");
                                        return;

                                    case "-7":
                                        MessageManager.ShowMsgBox("商品金额乘以税率的值与税额的误差超过限制，不允许拆分!");
                                        return;

                                    case "-8":
                                        MessageManager.ShowMsgBox("商品单价乘以数量与金额误差超过限制，不允许拆分!");
                                        return;

                                    case "-9":
                                        MessageManager.ShowMsgBox("拆分后单据号长度超过50，不允许拆分!");
                                        return;

                                    case "-10":
                                        MessageManager.ShowMsgBox(str5);
                                        return;

                                    case "-11":
                                        MessageManager.ShowMsgBox(str5);
                                        return;
                                }
                                if (str4.StartsWith("[-1]"))
                                {
                                    str6 = str4.Substring(4, str4.Length - 4);
                                    if (str6.Equals("A612"))
                                    {
                                        MessageManager.ShowMsgBox("作为汉字防伪用户 该单据存在单行商品的商品名称超出范围，不允许拆分!");
                                    }
                                    else if (str6.Equals("A613"))
                                    {
                                        MessageManager.ShowMsgBox("作为汉字防伪用户 该单据存在单行商品的计量单位超出范围，不允许拆分!");
                                    }
                                    else if ((str6.Equals("A017") || str6.Equals("A018")) || str6.Equals("A128"))
                                    {
                                        MessageManager.ShowMsgBox("税率非法，不允许拆分!");
                                    }
                                    else if (str6.Equals("A052"))
                                    {
                                        MessageManager.ShowMsgBox("稀土商品单价和数量为空，不允许拆分!");
                                    }
                                    else if (str6.Equals("A024"))
                                    {
                                        MessageManager.ShowMsgBox("增值税专用发票的购方税号为空，不允许拆分!");
                                    }
                                    else if ((((str6.Equals("A631") || str6.Equals("A632")) || (str6.Equals("A633") || str6.Equals("A634"))) || (str6.Equals("A635") || str6.Equals("A636"))) || str6.Equals("A637"))
                                    {
                                        MessageManager.ShowMsgBox("该单据中购方税号不符合校验规则，系统不允许拆分!");
                                    }
                                    else
                                    {
                                        MessageManager.ShowMsgBox(str6);
                                    }
                                }
                                else if (str4.StartsWith("group"))
                                {
                                    str6 = str4.Substring(5, str4.Length - 5);
                                    MessageManager.ShowMsgBox("第" + str6 + "组折扣商品开票金额超出金税设备允许范围￥" + invAmountLimit.ToString());
                                }
                                else if (str4.StartsWith("[sql]"))
                                {
                                    MessageManager.ShowMsgBox(str4.Substring(5, str4.Length - 5));
                                }
                                else if (str4.StartsWith("[error]"))
                                {
                                    str6 = str4.Substring(7, str4.Length - 7);
                                    MessageManager.ShowMsgBox("第" + str6 + "行，金额为0!");
                                }
                                else if (str4.StartsWith("[error_1_hs]"))
                                {
                                    str6 = str4.Substring(12, str4.Length - 12);
                                    MessageManager.ShowMsgBox("第" + str6 + "行，单价乘以数量不等于含税金额!");
                                }
                                else if (str4.StartsWith("[error_1_bhs]"))
                                {
                                    str6 = str4.Substring(13, str4.Length - 13);
                                    MessageManager.ShowMsgBox("第" + str6 + "行，单价乘以数量不等于金额!");
                                }
                                else if (str4.StartsWith("[error_2]"))
                                {
                                    MessageManager.ShowMsgBox("第" + str4.Substring(9, str4.Length - 9) + "行，商品数据非法!");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageManager.ShowMsgBox("INP-272202");
                    }
                }
            }
            catch (CustomException exception)
            {
                MessageBoxHelper.Show(exception.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception exception2)
            {
                HandleException.HandleError(exception2);
            }
        }
    }
}

