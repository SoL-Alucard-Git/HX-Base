namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DJHY : BaseForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoDataGrid aisinoDataGrid2;
        private AisinoDataGrid aisinoDataGrid3;
        private AisinoDataGrid aisinoDataGrid4;
        private SaleBillCtrl billBL = SaleBillCtrl.Instance;
        private AisinoBTN btnQuery;
        private IContainer components = null;
        private string CurrentBH;
        private DJHYbll djhyBLL = new DJHYbll();
        private string KeyWord;
        private ILog log = LogUtil.GetLogger<DJHY>();
        private string OriginalBH;
        private StatusStrip statusStrip1;
        private AisinoTXT textBoxKey;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private XmlComponentLoader xmlComponentLoader1;

        public DJHY()
        {
            this.Initialize();
        }

        private void aisinoDataGrid1_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                int num2;
                string str;
                string str2;
                string str3;
                this.CurrentBH = e.get_CurrentRow().Cells["BH"].Value.ToString();
                this.aisinoDataGrid2.set_DataSource(this.djhyBLL.GetDJMX(this.CurrentBH, this.djhyBLL.Pagesize, this.djhyBLL.CurrentPage));
                this.aisinoDataGrid3.set_DataSource(this.djhyBLL.GetYSDJ(this.CurrentBH, this.djhyBLL.Pagesize, this.djhyBLL.CurrentPage));
                this.OriginalBH = this.aisinoDataGrid3.get_Rows()[0].Cells["BH"].Value.ToString();
                this.aisinoDataGrid4.set_DataSource(this.djhyBLL.GetYSDJMX(this.OriginalBH, this.djhyBLL.Pagesize, this.djhyBLL.CurrentPage));
                SaleBill bill = this.billBL.Find(this.CurrentBH);
                int count = this.aisinoDataGrid2.get_Rows().Count;
                for (num2 = 0; num2 < count; num2++)
                {
                    str = this.aisinoDataGrid2.get_Rows()[num2].Cells["SLV"].Value.ToString();
                    str2 = this.aisinoDataGrid2.get_Rows()[num2].Cells["XH"].Value.ToString();
                    if (((str != null) && (str != "")) && (str != "中外合作油气田"))
                    {
                        str3 = this.billBL.ShowSLV(bill, str2, str);
                        if (str3 != "")
                        {
                            this.aisinoDataGrid2.get_Rows()[num2].Cells["SLV"].Value = str3;
                        }
                    }
                }
                this.OriginalBH = this.aisinoDataGrid3.get_Rows()[0].Cells["YSBH"].Value.ToString();
                SaleBill bill2 = this.billBL.Find(this.OriginalBH);
                int num3 = this.aisinoDataGrid4.get_Rows().Count;
                for (num2 = 0; num2 < num3; num2++)
                {
                    str = this.aisinoDataGrid4.get_Rows()[num2].Cells["SLV"].Value.ToString();
                    str2 = this.aisinoDataGrid4.get_Rows()[num2].Cells["XH"].Value.ToString();
                    if (((str != null) && (str != "")) && (str != "中外合作油气田"))
                    {
                        str3 = this.billBL.ShowSLV(bill2, str2, str);
                        if (str3 != "")
                        {
                            this.aisinoDataGrid4.get_Rows()[num2].Cells["SLV"].Value = str3;
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
            this.djhyBLL.Pagesize = e.get_PageSize();
            this.aisinoDataGrid1.set_DataSource(this.djhyBLL.QueryXSDJ(this.KeyWord, e.get_PageSize(), e.get_PageNO()));
        }

        private void aisinoDataGrid2_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.djhyBLL.Pagesize = e.get_PageSize();
            this.aisinoDataGrid2.set_DataSource(this.djhyBLL.GetDJMX(this.CurrentBH, e.get_PageSize(), e.get_PageNO()));
            SaleBill bill = this.billBL.Find(this.CurrentBH);
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

        private void aisinoDataGrid3_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                string str2;
                AisinoDataSet set;
                this.OriginalBH = e.get_CurrentRow().Cells["YSBH"].Value.ToString();
                if (this.OriginalBH.Substring(this.OriginalBH.Length - 2) == "~0")
                {
                    str2 = e.get_CurrentRow().Cells["BH"].Value.ToString();
                    set = this.djhyBLL.GetYSDJMX(str2, this.djhyBLL.Pagesize, this.djhyBLL.CurrentPage);
                    this.aisinoDataGrid4.set_DataSource(set);
                }
                else
                {
                    str2 = e.get_CurrentRow().Cells["BH"].Value.ToString();
                    set = this.djhyBLL.GetYSDJMX(str2, this.djhyBLL.Pagesize, this.djhyBLL.CurrentPage);
                    this.aisinoDataGrid4.set_DataSource(set);
                }
                SaleBill bill = this.billBL.Find(this.OriginalBH);
                int count = this.aisinoDataGrid4.get_Rows().Count;
                for (int i = 0; i < count; i++)
                {
                    string str3 = this.aisinoDataGrid4.get_Rows()[i].Cells["SLV"].Value.ToString();
                    string str4 = this.aisinoDataGrid4.get_Rows()[i].Cells["XH"].Value.ToString();
                    if (((str3 != null) && (str3 != "")) && (str3 != "中外合作油气田"))
                    {
                        string str5 = this.billBL.ShowSLV(bill, str4, str3);
                        if (str5 != "")
                        {
                            this.aisinoDataGrid4.get_Rows()[i].Cells["SLV"].Value = str5;
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
            this.djhyBLL.Pagesize = e.get_PageSize();
            this.aisinoDataGrid3.set_DataSource(this.djhyBLL.GetYSDJ(this.CurrentBH, e.get_PageSize(), e.get_PageNO()));
        }

        private void aisinoDataGrid4_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.djhyBLL.Pagesize = e.get_PageSize();
            this.aisinoDataGrid4.set_DataSource(this.djhyBLL.GetYSDJMX(this.OriginalBH, e.get_PageSize(), e.get_PageNO()));
            SaleBill bill = this.billBL.Find(this.OriginalBH);
            int count = this.aisinoDataGrid4.get_Rows().Count;
            for (int i = 0; i < count; i++)
            {
                string str = this.aisinoDataGrid4.get_Rows()[i].Cells["SLV"].Value.ToString();
                string str2 = this.aisinoDataGrid4.get_Rows()[i].Cells["XH"].Value.ToString();
                if (((str != null) && (str != "")) && (str != "中外合作油气田"))
                {
                    string str3 = this.billBL.ShowSLV(bill, str2, str);
                    if (str3 != "")
                    {
                        this.aisinoDataGrid4.get_Rows()[i].Cells["SLV"].Value = str3;
                    }
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.QueryYSDanJu();
            if (this.aisinoDataGrid1.get_DataSource().get_Data().Rows.Count == 0)
            {
                MessageManager.ShowMsgBox("INP-272203");
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
            this.toolStripButton1 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton1");
            this.toolStripButton2 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton2");
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.aisinoDataGrid2 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid2");
            this.aisinoDataGrid3 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid3");
            this.aisinoDataGrid4 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid4");
            this.statusStrip1 = this.xmlComponentLoader1.GetControlByName<StatusStrip>("statusStrip1");
            this.btnQuery = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnQuery");
            this.textBoxKey = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxKey");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripButton1.Click += new EventHandler(this.toolBtnHY_Click);
            this.toolStripButton2.Click += new EventHandler(this.toolBtnQuit_Click);
            this.aisinoDataGrid1.add_DataGridRowClickEvent(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowClickEvent));
            this.aisinoDataGrid3.add_DataGridRowClickEvent(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid3_DataGridRowClickEvent));
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.aisinoDataGrid1.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent));
            this.aisinoDataGrid2.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid2_GoToPageEvent));
            this.aisinoDataGrid3.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid3_GoToPageEvent));
            this.aisinoDataGrid4.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid4_GoToPageEvent));
            this.aisinoDataGrid1.set_ReadOnly(true);
            this.aisinoDataGrid1.get_DataGrid().AllowUserToDeleteRows = false;
            this.aisinoDataGrid2.set_ReadOnly(true);
            this.aisinoDataGrid2.get_DataGrid().AllowUserToDeleteRows = false;
            this.aisinoDataGrid3.set_ReadOnly(true);
            this.aisinoDataGrid3.get_DataGrid().AllowUserToDeleteRows = false;
            this.aisinoDataGrid4.set_ReadOnly(true);
            this.aisinoDataGrid4.get_DataGrid().AllowUserToDeleteRows = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DJHY));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "销售单据";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.DJHY\Aisino.Fwkp.Wbjk.DJHY.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "DJHY";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "销售单据还原";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.XSDJHY_Load);
            base.ResumeLayout(false);
        }

        private void QueryYSDanJu()
        {
            try
            {
                this.KeyWord = this.textBoxKey.Text.Trim();
                this.aisinoDataGrid1.set_DataSource(this.djhyBLL.QueryXSDJ(this.KeyWord, this.djhyBLL.Pagesize, this.djhyBLL.CurrentPage));
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void toolBtnHY_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.aisinoDataGrid1.get_SelectedRows().Count == 0) || (this.aisinoDataGrid1.get_SelectedRows().Count > 1))
                {
                    MessageManager.ShowMsgBox("INP-272401");
                }
                else
                {
                    this.aisinoDataGrid3.get_Rows()[0].Selected = true;
                    string bH = this.aisinoDataGrid1.get_SelectedRows()[0].Cells["BH"].Value.ToString();
                    string str2 = this.djhyBLL.SaveHuanYuan(bH);
                    if ((str2 != "Cancel") && (str2 == "OK"))
                    {
                        int num2;
                        string str3;
                        string str4;
                        string str5;
                        this.QueryYSDanJu();
                        this.aisinoDataGrid2.set_DataSource(this.djhyBLL.GetDJMX(this.CurrentBH, this.djhyBLL.Pagesize, 1));
                        this.aisinoDataGrid3.set_DataSource(this.djhyBLL.GetYSDJ(this.CurrentBH, this.djhyBLL.Pagesize, 1));
                        this.aisinoDataGrid4.set_DataSource(this.djhyBLL.GetYSDJMX(this.OriginalBH, this.djhyBLL.Pagesize, 1));
                        SaleBill bill = this.billBL.Find(this.CurrentBH);
                        int count = this.aisinoDataGrid2.get_Rows().Count;
                        for (num2 = 0; num2 < count; num2++)
                        {
                            str3 = this.aisinoDataGrid2.get_Rows()[num2].Cells["SLV"].Value.ToString();
                            str4 = this.aisinoDataGrid2.get_Rows()[num2].Cells["XH"].Value.ToString();
                            if (((str3 != null) && (str3 != "")) && (str3 != "中外合作油气田"))
                            {
                                str5 = this.billBL.ShowSLV(bill, str4, str3);
                                if (str5 != "")
                                {
                                    this.aisinoDataGrid2.get_Rows()[num2].Cells["SLV"].Value = str5;
                                }
                            }
                        }
                        SaleBill bill2 = this.billBL.Find(this.OriginalBH);
                        int num3 = this.aisinoDataGrid4.get_Rows().Count;
                        for (num2 = 0; num2 < num3; num2++)
                        {
                            str3 = this.aisinoDataGrid4.get_Rows()[num2].Cells["SLV"].Value.ToString();
                            str4 = this.aisinoDataGrid4.get_Rows()[num2].Cells["XH"].Value.ToString();
                            if (((str3 != null) && (str3 != "")) && (str3 != "中外合作油气田"))
                            {
                                str5 = this.billBL.ShowSLV(bill2, str4, str3);
                                if (str5 != "")
                                {
                                    this.aisinoDataGrid4.get_Rows()[num2].Cells["SLV"].Value = str5;
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

        private void toolBtnQuit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void XSDJHY_Load(object sender, EventArgs e)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单据编号");
            item.Add("Property", "BH");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "True");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单据种类");
            item.Add("Property", "DJZL");
            item.Add("Type", "Text");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "购方名称");
            item.Add("Property", "GFMC");
            item.Add("Type", "Text");
            item.Add("Width", "120");
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
            item.Add("Width", "80");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单据日期");
            item.Add("Property", "DJRQ");
            item.Add("Type", "Text");
            item.Add("Width", "80");
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
            List<Dictionary<string, string>> list2 = new List<Dictionary<string, string>>();
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "序号");
            dictionary2.Add("Property", "XH");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "70");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            list2.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "商品名称");
            dictionary2.Add("Property", "SPMC");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "120");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            list2.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "规格型号");
            dictionary2.Add("Property", "GGXH");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "80");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            dictionary2.Add("Visible", "False");
            list2.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "数量");
            dictionary2.Add("Property", "SL");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "60");
            dictionary2.Add("Align", "MiddleRight");
            dictionary2.Add("HeadAlign", "MiddleRight");
            list2.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "单价");
            dictionary2.Add("Property", "DJ");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "80");
            dictionary2.Add("Align", "MiddleRight");
            dictionary2.Add("HeadAlign", "MiddleRight");
            list2.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "金额");
            dictionary2.Add("Property", "JE");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "100");
            dictionary2.Add("Align", "MiddleRight");
            dictionary2.Add("HeadAlign", "MiddleRight");
            list2.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "税率");
            dictionary2.Add("Property", "SLV");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "60");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            list2.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "税额");
            dictionary2.Add("Property", "SE");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "100");
            dictionary2.Add("Align", "MiddleRight");
            dictionary2.Add("HeadAlign", "MiddleRight");
            list2.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "计量单位");
            dictionary2.Add("Property", "JLDW");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "80");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            dictionary2.Add("Visible", "False");
            list2.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "含税价标志");
            dictionary2.Add("Property", "HSJBZ");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "80");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            list2.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "单据行性质");
            dictionary2.Add("Property", "DJHXZ");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Visible", "False");
            list2.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "销售单据编号");
            dictionary2.Add("Property", "XSDJBH");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Visible", "False");
            list2.Add(dictionary2);
            this.aisinoDataGrid2.set_ColumeHead(list2);
            this.aisinoDataGrid2.get_Columns()["JE"].DefaultCellStyle.Format = "0.00";
            this.aisinoDataGrid2.get_Columns()["SE"].DefaultCellStyle.Format = "0.00";
            this.aisinoDataGrid2.set_DataSource(new AisinoDataSet());
            List<Dictionary<string, string>> list3 = new List<Dictionary<string, string>>();
            Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
            dictionary3.Add("AisinoLBL", "单据编号");
            dictionary3.Add("Property", "BH");
            dictionary3.Add("Type", "Text");
            dictionary3.Add("Width", "80");
            dictionary3.Add("Align", "MiddleLeft");
            dictionary3.Add("HeadAlign", "MiddleCenter");
            dictionary3.Add("Visible", "True");
            list3.Add(dictionary3);
            dictionary3 = new Dictionary<string, string>();
            dictionary3.Add("AisinoLBL", "单据种类");
            dictionary3.Add("Property", "DJZL");
            dictionary3.Add("Type", "Text");
            dictionary3.Add("Width", "80");
            dictionary3.Add("HeadAlign", "MiddleLeft");
            list3.Add(dictionary3);
            dictionary3 = new Dictionary<string, string>();
            dictionary3.Add("AisinoLBL", "购方名称");
            dictionary3.Add("Property", "GFMC");
            dictionary3.Add("Type", "Text");
            dictionary3.Add("Width", "120");
            dictionary3.Add("Align", "MiddleLeft");
            dictionary3.Add("HeadAlign", "MiddleLeft");
            list3.Add(dictionary3);
            dictionary3 = new Dictionary<string, string>();
            dictionary3.Add("AisinoLBL", "购方税号");
            dictionary3.Add("Property", "GFSH");
            dictionary3.Add("Type", "Text");
            list3.Add(dictionary3);
            dictionary3 = new Dictionary<string, string>();
            dictionary3.Add("AisinoLBL", "金额合计");
            dictionary3.Add("Property", "JEHJ");
            dictionary3.Add("Type", "Text");
            dictionary3.Add("Width", "80");
            dictionary3.Add("Align", "MiddleRight");
            dictionary3.Add("HeadAlign", "MiddleLeft");
            list3.Add(dictionary3);
            dictionary3 = new Dictionary<string, string>();
            dictionary3.Add("AisinoLBL", "单据日期");
            dictionary3.Add("Property", "DJRQ");
            dictionary3.Add("Type", "Text");
            dictionary3.Add("Width", "80");
            dictionary3.Add("Align", "MiddleLeft");
            dictionary3.Add("HeadAlign", "MiddleLeft");
            list3.Add(dictionary3);
            dictionary3 = new Dictionary<string, string>();
            dictionary3.Add("AisinoLBL", "备注");
            dictionary3.Add("Property", "BZ");
            dictionary3.Add("Type", "Text");
            dictionary3.Add("Width", "100");
            dictionary3.Add("Align", "MiddleLeft");
            dictionary3.Add("HeadAlign", "MiddleLeft");
            list3.Add(dictionary3);
            dictionary3 = new Dictionary<string, string>();
            dictionary3.Add("AisinoLBL", "hide编号");
            dictionary3.Add("Property", "YSBH");
            dictionary3.Add("Type", "Text");
            dictionary3.Add("Visible", "False");
            list3.Add(dictionary3);
            this.aisinoDataGrid3.set_ColumeHead(list3);
            this.aisinoDataGrid3.get_Columns()["JEHJ"].DefaultCellStyle.Format = "0.00";
            this.aisinoDataGrid3.set_DataSource(new AisinoDataSet());
            List<Dictionary<string, string>> list4 = new List<Dictionary<string, string>>();
            dictionary2.Clear();
            dictionary2.Add("AisinoLBL", "序号");
            dictionary2.Add("Property", "XH");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "70");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            list4.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "商品名称");
            dictionary2.Add("Property", "SPMC");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "120");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            list4.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "规格型号");
            dictionary2.Add("Property", "GGXH");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "80");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            dictionary2.Add("Visible", "False");
            list4.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "数量");
            dictionary2.Add("Property", "SL");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "80");
            dictionary2.Add("Align", "MiddleRight");
            dictionary2.Add("HeadAlign", "MiddleRight");
            list4.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "单价");
            dictionary2.Add("Property", "DJ");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "80");
            dictionary2.Add("Align", "MiddleRight");
            dictionary2.Add("HeadAlign", "MiddleRight");
            list4.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "金额");
            dictionary2.Add("Property", "JE");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "100");
            dictionary2.Add("Align", "MiddleRight");
            dictionary2.Add("HeadAlign", "MiddleRight");
            list4.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "税率");
            dictionary2.Add("Property", "SLV");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "60");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            list4.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "税额");
            dictionary2.Add("Property", "SE");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "80");
            dictionary2.Add("Align", "MiddleRight");
            dictionary2.Add("HeadAlign", "MiddleRight");
            list4.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "计量单位");
            dictionary2.Add("Property", "JLDW");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "100");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            dictionary2.Add("Visible", "False");
            list4.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "含税价标志");
            dictionary2.Add("Property", "HSJBZ");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Width", "80");
            dictionary2.Add("Align", "MiddleLeft");
            dictionary2.Add("HeadAlign", "MiddleLeft");
            list4.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "单据行性质");
            dictionary2.Add("Property", "DJHXZ");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Visible", "False");
            list4.Add(dictionary2);
            dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("AisinoLBL", "销售单据编号");
            dictionary2.Add("Property", "XSDJBH");
            dictionary2.Add("Type", "Text");
            dictionary2.Add("Visible", "False");
            list4.Add(dictionary2);
            this.aisinoDataGrid4.set_ColumeHead(list4);
            this.aisinoDataGrid4.get_Columns()["JE"].DefaultCellStyle.Format = "0.00";
            this.aisinoDataGrid4.get_Columns()["SE"].DefaultCellStyle.Format = "0.00";
            this.aisinoDataGrid4.set_DataSource(new AisinoDataSet());
        }
    }
}

