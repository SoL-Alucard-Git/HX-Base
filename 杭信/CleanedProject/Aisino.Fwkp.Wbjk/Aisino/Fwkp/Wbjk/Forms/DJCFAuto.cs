namespace Aisino.Fwkp.Wbjk.Forms
{
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
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class DJCFAuto : BaseForm
    {
        private AisinoDataGrid aisinoDataGrid2;
        private SaleBill bill = null;
        private SaleBillCtrl billBL = SaleBillCtrl.Instance;
        private IContainer components = null;
        private DataGridMX dataGridMX1;
        private DataGridMX dataGridMX2;
        private DataGridMX dataGridMXCFH;
        private DJCFdal djcfBLL;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private XmlComponentLoader xmlComponentLoader1;

        internal DJCFAuto(SaleBill bill, DJCFdal djcfBLL)
        {
            this.Initialize();
            this.bill = bill;
            this.djcfBLL = djcfBLL;
        }

        private void AfterChaiFenDanJu()
        {
            this.dataGridMX2.get_NewColumns().Clear();
            this.dataGridMX2.get_NewColumns().Add("单据号;BH");
            this.dataGridMX2.get_NewColumns().Add("单据种类;DJZL");
            this.dataGridMX2.get_NewColumns().Add("购方名称;GFMC");
            this.dataGridMX2.get_NewColumns().Add("购方税号;GFSH");
            this.dataGridMX2.get_NewColumns().Add("金额合计;JEHJ;;100;money");
            this.dataGridMX2.Bind();
            this.dataGridMX2.DataSource = this.djcfBLL.GetCFHDanJu(this.bill.BH);
        }

        private void AfterChaifenMX()
        {
            this.dataGridMXCFH.get_NewColumns().Add("序号;order");
            this.dataGridMXCFH.get_NewColumns().Add("编号;XSDJBH");
            this.dataGridMXCFH.get_NewColumns().Add("商品名称;SPMC;;130");
            this.dataGridMXCFH.get_NewColumns().Add("数量;SL");
            this.dataGridMXCFH.get_NewColumns().Add("单价;DJ");
            this.dataGridMXCFH.get_NewColumns().Add("金额;JE;;100;money");
            this.dataGridMXCFH.get_NewColumns().Add("税率;SLV;;50");
            this.dataGridMXCFH.get_NewColumns().Add("税额;SE;;100;money");
            this.dataGridMXCFH.get_NewColumns().Add("DJHXZ;DJHXZ;ZKHStyle");
            this.dataGridMXCFH.Bind();
            this.dataGridMXCFH.Columns["SLV"].DefaultCellStyle.Format = "0.0%";
        }

        private void aisinoDataGrid2_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.aisinoDataGrid2.set_DataSource(this.djcfBLL.QueryXSDJMX(this.bill.BH, e.get_PageSize(), e.get_PageNO()));
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

        private void CFHMXLoad(string BH)
        {
            DataTable cFHmx = this.djcfBLL.GetCFHmx(BH);
            this.dataGridMXCFH.Rows.Clear();
            for (int i = 0; i < cFHmx.Rows.Count; i++)
            {
                DataRow row = cFHmx.Rows[i];
                string str = GetSafeData.ValidateValue<string>(row, "XSDJBH");
                string str2 = GetSafeData.ValidateValue<string>(row, "SPMC");
                double num2 = GetSafeData.ValidateValue<double>(row, "SL");
                double num3 = GetSafeData.ValidateValue<double>(row, "DJ");
                double num4 = GetSafeData.ValidateValue<double>(row, "JE");
                double num5 = GetSafeData.ValidateValue<double>(row, "SLV");
                double num6 = GetSafeData.ValidateValue<double>(row, "SE");
                int num7 = GetSafeData.ValidateValue<int>(row, "DJHXZ");
                string str3 = ((decimal) num2).ToString();
                if (num5 == 0.0)
                {
                    string str4 = this.billBL.ShowSLV(this.bill, (i + 1).ToString(), "0");
                    if (str4.Equals("免税") || str4.Equals("不征税"))
                    {
                        this.dataGridMXCFH.Rows.Add(new object[] { i + 1, str, str2, str3, num3, num4, str4, num6, num7 });
                    }
                    else
                    {
                        this.dataGridMXCFH.Rows.Add(new object[] { i + 1, str, str2, str3, num3, num4, num5, num6, num7 });
                    }
                }
                else if (this.bill.HYSY && (num5 == 0.05))
                {
                    this.dataGridMXCFH.Rows.Add(new object[] { i + 1, str, str2, str3, num3, num4, "中外合作油气田", num6, num7 });
                }
                else if (this.bill.JZ_50_15 && (num5 == 0.015))
                {
                    this.dataGridMXCFH.Rows.Add(new object[] { i + 1, str, str2, str3, num3, num4, "减按1.5%计算", num6, num7 });
                }
                else
                {
                    this.dataGridMXCFH.Rows.Add(new object[] { i + 1, str, str2, str3, num3, num4, num5, num6, num7 });
                }
            }
        }

        private void DanJuBeforeCF()
        {
            this.dataGridMX1.Columns.Clear();
            this.dataGridMX1.ColumnCount = 5;
            this.dataGridMX1.Columns[0].Name = "单据号";
            this.dataGridMX1.Columns[1].Name = "单据种类";
            this.dataGridMX1.Columns[2].Name = "购方名称";
            this.dataGridMX1.Columns[3].Name = "购方税号";
            this.dataGridMX1.Columns[4].Name = "金额合计";
            string str = ShowString.ShowFPZL(this.bill.DJZL);
            this.dataGridMX1.Rows.Add(new object[] { this.bill.BH, str, this.bill.GFMC, this.bill.GFSH, this.bill.JEHJ });
            this.dataGridMX1.Columns[4].DefaultCellStyle = this.dataGridMX1.styleMoney;
            this.dataGridMX1.ClearSelection();
        }

        private void dataGridMX2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string name = this.dataGridMX2.Columns[e.ColumnIndex].Name;
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

        private void dataGridMX2_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridMX2.SelectedRows.Count > 0)
            {
                string bH = this.dataGridMX2.SelectedRows[0].Cells["BH"].Value.ToString();
                this.CFHMXLoad(bH);
                if (this.dataGridMXCFH.Rows.Count > 0)
                {
                    this.dataGridMXCFH.Rows[0].Selected = false;
                }
            }
        }

        private void dataGridMXCFH_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (Convert.ToInt32(this.dataGridMXCFH.Rows[e.RowIndex].Cells["DJHXZ"].Value))
            {
                case 3:
                    e.CellStyle.BackColor = Color.LightCyan;
                    break;

                case 4:
                {
                    e.CellStyle.BackColor = Color.LightBlue;
                    string name = this.dataGridMXCFH.Columns[e.ColumnIndex].Name;
                    if ((((name == "SL") || (name == "DJ")) && (e.Value != null)) && (e.Value.ToString().Trim() == "0"))
                    {
                        e.Value = null;
                    }
                    break;
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.dataGridMX2 = this.xmlComponentLoader1.GetControlByName<DataGridMX>("dataGridMX2");
            this.dataGridMXCFH = this.xmlComponentLoader1.GetControlByName<DataGridMX>("dataGridMXCFH");
            this.dataGridMX1 = this.xmlComponentLoader1.GetControlByName<DataGridMX>("dataGridMX1");
            this.aisinoDataGrid2 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid2");
            this.toolStripButton1 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton1");
            this.toolStripButton2 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton2");
            this.dataGridMX2.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridMX2_CellFormatting);
            this.dataGridMX2.SelectionChanged += new EventHandler(this.dataGridMX2_SelectionChanged);
            this.dataGridMXCFH.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridMXCFH_CellFormatting);
            this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
            this.toolStripButton2.Click += new EventHandler(this.toolStripButton2_Click);
            this.aisinoDataGrid2.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid2_GoToPageEvent));
            this.aisinoDataGrid2.set_ReadOnly(true);
            this.aisinoDataGrid2.get_DataGrid().AllowUserToDeleteRows = false;
            this.dataGridMX2.AllowUserToDeleteRows = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DJCFAuto));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "销售单据拆分自动拆分";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.DJCFAuto\Aisino.Fwkp.Wbjk.DJCFAuto.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "DJCFAuto";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "销售单据拆分自动拆分";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.XSDJChaiFenZD_Load);
            base.ResumeLayout(false);
        }

        private void MXBeforeCF()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "序号");
            item.Add("Property", "XH");
            item.Add("Type", "Text");
            item.Add("Width", "70");
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
            AisinoDataSet set = this.djcfBLL.QueryXSDJMX(this.bill.BH, this.djcfBLL.Pagesize, this.djcfBLL.CurrentPage);
            this.aisinoDataGrid2.set_DataSource(set);
            int count = this.aisinoDataGrid2.get_Rows().Count;
            for (int i = 0; i < count; i++)
            {
                string str = this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value.ToString();
                string str2 = this.aisinoDataGrid2.get_Rows()[i].Cells["XH"].Value.ToString();
                if ((((str != null) && (str != "")) && (str != "中外合作油气田")) && (this.billBL.ShowSLV(this.bill, str2, str) != ""))
                {
                    this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value = str;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (MessageManager.ShowMsgBox("INP-272291") == DialogResult.OK)
            {
                string str = this.djcfBLL.SaveAutoSplit(this.bill.BH);
                if (str == "OK")
                {
                    MessageManager.ShowMsgBox("INP-272292");
                    base.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-270003", "", new string[] { str });
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void XSDJChaiFenZD_Load(object sender, EventArgs e)
        {
            try
            {
                this.DanJuBeforeCF();
                this.MXBeforeCF();
                this.AfterChaifenMX();
                this.AfterChaiFenDanJu();
                this.dataGridMX1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.aisinoDataGrid2.set_SelectionMode(DataGridViewSelectionMode.CellSelect);
                this.dataGridMXCFH.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }
    }
}

