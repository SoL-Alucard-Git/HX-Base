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
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DJHBYL : BaseForm
    {
        private AisinoDataGrid aisinoDataGridh;
        private AisinoDataGrid aisinoDataGridq;
        private string BeforeMergeSelectedBH;
        private SaleBillCtrl billBL = SaleBillCtrl.Instance;
        private IContainer components = null;
        private DataGridMX dataGridMXDJHBH;
        private DataGridMX dataGridMXDJHBQ;
        private DJHBdal djhbBLL;
        private List<string> listBH = new List<string>();
        private List<SaleBill> listBill = new List<SaleBill>();
        private ILog log = LogUtil.GetLogger<DJHBYL>();
        private SaleBill mergedBill = new SaleBill();
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private XmlComponentLoader xmlComponentLoader1;

        internal DJHBYL(DJHBdal djhbBLL, List<SaleBill> listBill, SaleBill mergedBill)
        {
            this.Initialize();
            this.djhbBLL = djhbBLL;
            this.listBill = listBill;
            this.mergedBill = mergedBill;
        }

        private void aisinoDataGridh_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.djhbBLL.HandleGoToPageEventArgs(e);
            int pageno = e.get_PageNO();
            int pagesize = e.get_PageSize();
            this.aisinoDataGridh.set_DataSource(this.djhbBLL.GetMingXi(pagesize, pageno));
            int count = this.aisinoDataGridh.get_Rows().Count;
            for (int i = 0; i < count; i++)
            {
                string str = this.aisinoDataGridh.get_Rows()[i].Cells["SLV"].Value.ToString();
                string str2 = this.aisinoDataGridh.get_Rows()[i].Cells["XH"].Value.ToString();
                if (((str != null) && (str != "")) && (str != "中外合作油气田"))
                {
                    string str3 = this.billBL.ShowSLV(this.mergedBill, str2, str);
                    if (str3 != "")
                    {
                        this.aisinoDataGridh.get_Rows()[i].Cells["SLV"].Value = str3;
                    }
                }
            }
        }

        private void aisinoDataGridq_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            int currentpage = e.get_PageNO();
            int pagesize = e.get_PageSize();
            this.aisinoDataGridq.set_DataSource(this.djhbBLL.QueryXSDJMX_1(this.BeforeMergeSelectedBH, pagesize, currentpage));
            SaleBill bill = this.billBL.Find(this.BeforeMergeSelectedBH);
            int count = this.aisinoDataGridq.get_Rows().Count;
            for (int i = 0; i < count; i++)
            {
                string str = this.aisinoDataGridq.get_Rows()[i].Cells["SLV"].Value.ToString();
                string str2 = this.aisinoDataGridq.get_Rows()[i].Cells["XH"].Value.ToString();
                if (((str != null) && (str != "")) && (str != "中外合作油气田"))
                {
                    string str3 = this.billBL.ShowSLV(bill, str2, str);
                    if (str3 != "")
                    {
                        this.aisinoDataGridq.get_Rows()[i].Cells["SLV"].Value = str3;
                    }
                }
            }
        }

        private void dataGridMXDJHBQ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    this.BeforeMergeSelectedBH = this.dataGridMXDJHBQ[0, e.RowIndex].Value.ToString().Trim();
                    string s = PropertyUtil.GetValue("WBJK_DJHB_DATAGRID_HBMX_1");
                    int result = 1;
                    int.TryParse(s, out result);
                    this.aisinoDataGridq.set_DataSource(this.djhbBLL.QueryXSDJMX_1(this.BeforeMergeSelectedBH, 20, result));
                    SaleBill bill = this.billBL.Find(this.BeforeMergeSelectedBH);
                    int count = this.aisinoDataGridq.get_Rows().Count;
                    for (int i = 0; i < count; i++)
                    {
                        string str2 = this.aisinoDataGridq.get_Rows()[i].Cells["SLV"].Value.ToString();
                        string str3 = this.aisinoDataGridq.get_Rows()[i].Cells["XH"].Value.ToString();
                        if (((str2 != null) && (str2 != "")) && (str2 != "中外合作油气田"))
                        {
                            string str4 = this.billBL.ShowSLV(bill, str3, str2);
                            if (str4 != "")
                            {
                                this.aisinoDataGridq.get_Rows()[i].Cells["SLV"].Value = str4;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DJHBYL_Load(object sender, EventArgs e)
        {
            try
            {
                int num;
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "序号");
                item.Add("Property", "XH");
                item.Add("Type", "Text");
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
                item.Add("Width", "100");
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
                item.Add("Width", "50");
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
                item.Add("Width", "80");
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
                this.aisinoDataGridq.set_ColumeHead(list);
                this.aisinoDataGridq.get_Columns()["JE"].DefaultCellStyle.Format = "0.00";
                this.aisinoDataGridq.get_Columns()["SE"].DefaultCellStyle.Format = "0.00";
                this.aisinoDataGridq.set_DataSource(new AisinoDataSet());
                this.dataGridMXDJHBQ.Columns.Clear();
                this.dataGridMXDJHBQ.get_NewColumns().Add("单据号;BH");
                this.dataGridMXDJHBQ.get_NewColumns().Add("单据种类;DJZL;;80");
                this.dataGridMXDJHBQ.get_NewColumns().Add("购方名称;GFMC;;130");
                this.dataGridMXDJHBQ.get_NewColumns().Add("购方税号;GFSH;;100");
                this.dataGridMXDJHBQ.get_NewColumns().Add("金额合计;JEHJ;;100;money");
                this.dataGridMXDJHBQ.get_NewColumns().Add("单据日期;DJRQ;;100");
                this.dataGridMXDJHBQ.get_NewColumns().Add("备注;BZ;;80");
                this.dataGridMXDJHBQ.Bind();
                for (num = 0; num < this.listBill.Count; num++)
                {
                    this.dataGridMXDJHBQ.Rows.Add(new object[] { this.listBill[num].BH, ShowString.ShowFPZL(this.listBill[num].DJZL), this.listBill[num].GFMC, this.listBill[num].GFSH, this.listBill[num].JEHJ, this.listBill[num].DJRQ, this.listBill[num].BZ });
                }
                this.dataGridMXDJHBQ.Columns["DJRQ"].DefaultCellStyle.Format = "yyyy-MM-dd";
                this.dataGridMXDJHBH.Columns.Clear();
                this.dataGridMXDJHBH.get_NewColumns().Add("单据号;BH");
                this.dataGridMXDJHBH.get_NewColumns().Add("单据种类;DJZL");
                this.dataGridMXDJHBH.get_NewColumns().Add("购方名称;GFMC;;130");
                this.dataGridMXDJHBH.get_NewColumns().Add("购方税号;GFSH;;130");
                this.dataGridMXDJHBH.get_NewColumns().Add("金额合计;JEHJ;;100;money");
                this.dataGridMXDJHBH.get_NewColumns().Add("单据日期;DJRQ");
                this.dataGridMXDJHBH.get_NewColumns().Add("备注;BZ;;150");
                this.dataGridMXDJHBH.Bind();
                this.dataGridMXDJHBH.Rows.Add(new object[] { this.mergedBill.BH, ShowString.ShowFPZL(this.mergedBill.DJZL), this.mergedBill.GFMC, this.mergedBill.GFSH, this.mergedBill.JEHJ, this.mergedBill.DJRQ, this.mergedBill.BZ });
                this.dataGridMXDJHBH.Columns["DJRQ"].DefaultCellStyle.Format = "yyyy-MM-dd";
                list = new List<Dictionary<string, string>>();
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "序号");
                item.Add("Property", "XH");
                item.Add("Type", "Text");
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
                item.Add("Width", "100");
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
                item.Add("Width", "50");
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
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "商品税目");
                item.Add("Property", "SPSM");
                item.Add("Type", "Text");
                item.Add("Width", "80");
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
                this.aisinoDataGridh.set_ColumeHead(list);
                this.aisinoDataGridh.get_Columns()["JE"].DefaultCellStyle.Format = "0.00";
                this.aisinoDataGridh.get_Columns()["SE"].DefaultCellStyle.Format = "0.00";
                this.aisinoDataGridh.set_DataSource(new AisinoDataSet());
                string s = PropertyUtil.GetValue("WBJK_DJHB_DATAGRID_HBMX");
                int result = 1;
                int.TryParse(s, out result);
                this.aisinoDataGridh.set_DataSource(this.djhbBLL.GetMingXi(20, result));
                this.dataGridMXDJHBH.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.dataGridMXDJHBQ.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.aisinoDataGridq.set_SelectionMode(DataGridViewSelectionMode.CellSelect);
                this.aisinoDataGridh.set_SelectionMode(DataGridViewSelectionMode.CellSelect);
                int count = this.aisinoDataGridh.get_Rows().Count;
                for (num = 0; num < count; num++)
                {
                    string str2 = this.aisinoDataGridh.get_Rows()[num].Cells["SLV"].Value.ToString();
                    string str3 = this.aisinoDataGridh.get_Rows()[num].Cells["XH"].Value.ToString();
                    if (((str2 != null) && (str2 != "")) && (str2 != "中外合作油气田"))
                    {
                        string str4 = this.billBL.ShowSLV(this.mergedBill, str3, str2);
                        if (str4 != "")
                        {
                            this.aisinoDataGridh.get_Rows()[num].Cells["SLV"].Value = str4;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.aisinoDataGridq = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGridq");
            this.dataGridMXDJHBH = this.xmlComponentLoader1.GetControlByName<DataGridMX>("dataGridMXDJHBH");
            this.aisinoDataGridh = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGridh");
            this.toolStripButton1 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton1");
            this.toolStripButton2 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton2");
            this.dataGridMXDJHBQ = this.xmlComponentLoader1.GetControlByName<DataGridMX>("dataGridMXDJHBQ");
            this.toolStripButton1.Click += new EventHandler(this.toolBtnSave_Click);
            this.toolStripButton2.Click += new EventHandler(this.toolBtnQuit_Click);
            this.dataGridMXDJHBQ.CellClick += new DataGridViewCellEventHandler(this.dataGridMXDJHBQ_CellClick);
            this.aisinoDataGridh.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGridh_GoToPageEvent));
            this.aisinoDataGridq.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGridq_GoToPageEvent));
            this.dataGridMXDJHBQ.ReadOnly = true;
            this.dataGridMXDJHBQ.AllowUserToDeleteRows = false;
            this.aisinoDataGridq.set_ReadOnly(true);
            this.aisinoDataGridq.get_DataGrid().AllowUserToDeleteRows = false;
            this.dataGridMXDJHBH.ReadOnly = true;
            this.dataGridMXDJHBH.AllowUserToDeleteRows = false;
            this.aisinoDataGridh.set_ReadOnly(true);
            this.aisinoDataGridh.get_DataGrid().AllowUserToDeleteRows = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DJHBYL));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "DJHBYL";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.DJHBYL\Aisino.Fwkp.Wbjk.DJHBYL.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "DJHBYL";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "销售单据合并预览";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.DJHBYL_Load);
            base.ResumeLayout(false);
        }

        private void toolBtnQuit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageManager.ShowMsgBox("INP-272307") == DialogResult.OK)
                {
                    List<SaleBill> afterbill = new List<SaleBill> {
                        this.mergedBill
                    };
                    string str = new SaleBillDAL().SaveToRealTable(this.listBill, afterbill, this.mergedBill.BH);
                    if (str == "0")
                    {
                        MessageManager.ShowMsgBox("INP-272308");
                        base.Close();
                    }
                    else if (str.StartsWith("从预览表转正存库Fail:   System.Data.SQLite.SQLiteException (0x80004005): constraint failed\r\nUNIQUE constraint failed: XSDJ_MX.XSDJBH, XSDJ_MX.XH\r\n"))
                    {
                        MessageManager.ShowMsgBox("合并后的单据号和原始单据号有重复，请删除原始单据，重新进行合并！\r\n单据合并未能完成，确定后将退出此界面");
                        base.Close();
                    }
                    else
                    {
                        MessageBoxHelper.Show(str);
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }
    }
}

