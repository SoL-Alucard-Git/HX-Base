namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SelectDanJuForFP : BaseForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoDataGrid aisinoDataGrid2;
        private IContainer components = null;
        private FPTKdal fptkBLL = new FPTKdal();
        private InvType fpType;
        private string Grid1BH = string.Empty;
        private AisinoGRP groupBox1;
        private int leftInvCount = 0;
        private List<string> listBH = new List<string>();
        private ILog log = LogUtil.GetLogger<SelectDanJuForFP>();
        private int selectNum = 0;
        private DataGridViewSelectedRowCollection selectRows;
        private AisinoSPL splitContainer1;
        private ToolStripButton toolBtnClear;
        private ToolStripButton toolBtnQuit;
        private ToolStripButton toolBtnSelectAll;
        private XmlComponentLoader xmlComponentLoader1;
        private int yuanseleCount = 0;

        public SelectDanJuForFP(InvType fptype, int LeftInvCount)
        {
            this.fpType = fptype;
            this.Initialize();
            this.aisinoDataGrid1.add_DataGridRowClickEvent(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowClickEvent));
            this.leftInvCount = LeftInvCount;
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

        private void aisinoDataGrid1_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
            int count = this.aisinoDataGrid1.get_SelectedRows().Count;
            if (count > this.yuanseleCount)
            {
                e.get_CurrentRow().Cells["DJZT"].Value = count.ToString();
            }
            else
            {
                int num3;
                if (count < this.yuanseleCount)
                {
                    object obj2 = e.get_CurrentRow().Cells["DJZT"].Value;
                    int result = 1;
                    if ((obj2 != null) && (obj2.ToString().Trim() != ""))
                    {
                        int.TryParse(obj2.ToString().Trim(), out result);
                    }
                    for (num3 = 0; num3 < this.aisinoDataGrid1.get_Rows().Count; num3++)
                    {
                        object obj3 = this.aisinoDataGrid1.get_Rows()[num3].Cells["DJZT"].Value;
                        if ((obj3 != null) && (obj3.ToString().Trim() != ""))
                        {
                            if (this.aisinoDataGrid1.get_Rows()[num3].Selected)
                            {
                                if (Convert.ToInt32(obj3) > result)
                                {
                                    this.aisinoDataGrid1.get_Rows()[num3].Cells["DJZT"].Value = Convert.ToInt32(obj3) - 1;
                                }
                                else if (Convert.ToInt32(obj3) == result)
                                {
                                    this.aisinoDataGrid1.get_Rows()[num3].Cells["DJZT"].Value = count.ToString();
                                }
                            }
                            else
                            {
                                this.aisinoDataGrid1.get_Rows()[num3].Cells["DJZT"].Value = "";
                            }
                        }
                        else if (this.aisinoDataGrid1.get_Rows()[num3].Selected)
                        {
                            this.aisinoDataGrid1.get_Rows()[num3].Cells["DJZT"].Value = count.ToString();
                        }
                        else
                        {
                            this.aisinoDataGrid1.get_Rows()[num3].Cells["DJZT"].Value = "";
                        }
                    }
                }
                else
                {
                    for (num3 = 0; num3 < this.aisinoDataGrid1.get_Rows().Count; num3++)
                    {
                        if (this.aisinoDataGrid1.get_Rows()[num3].Selected)
                        {
                            this.aisinoDataGrid1.get_Rows()[num3].Cells["DJZT"].Value = "1";
                        }
                        else
                        {
                            this.aisinoDataGrid1.get_Rows()[num3].Cells["DJZT"].Value = "";
                        }
                    }
                }
            }
            this.yuanseleCount = count;
        }

        private void aisinoDataGrid1_DataGridRowSelectionChanged(object sender, DataGridRowEventArgs e)
        {
            try
            {
                this.Grid1BH = e.get_CurrentRow().Cells["BH"].Value.ToString().Trim();
                this.aisinoDataGrid2.set_DataSource(this.fptkBLL.QueryXSDJMX(this.Grid1BH, this.fptkBLL.Pagesize, 1));
                for (int i = 0; i < this.aisinoDataGrid2.get_Rows().Count; i++)
                {
                    string str = this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value.ToString();
                    if ((((str != null) && (str != "")) && (str != "免税")) && (str != "中外合作油气田"))
                    {
                        str = ((Convert.ToDouble(str) * 100.0)).ToString() + "%";
                        this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value = str;
                    }
                }
            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("超时"))
                {
                    this.log.Error(exception.ToString());
                }
                else
                {
                    HandleException.HandleError(exception);
                }
            }
        }

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                this.btnClear_Click(sender, e);
                string str = e.get_PageNO().ToString();
                PropertyUtil.SetValue("WBJK_DJXZFP_DATAGRID1", str);
                this.aisinoDataGrid1.set_DataSource(this.fptkBLL.GetXSDJ(this.fpType, e.get_PageSize(), e.get_PageNO()));
            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("超时"))
                {
                    this.log.Error(exception.ToString());
                }
                else
                {
                    HandleException.HandleError(exception);
                }
            }
        }

        private void aisinoDataGrid2_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Grid1BH))
                {
                    this.aisinoDataGrid2.set_DataSource(this.fptkBLL.QueryXSDJMX(this.Grid1BH, e.get_PageSize(), e.get_PageNO()));
                    for (int i = 0; i < this.aisinoDataGrid2.get_Rows().Count; i++)
                    {
                        string str = this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value.ToString();
                        if ((((str != null) && (str != "")) && (str != "免税")) && (str != "中外合作油气田"))
                        {
                            str = ((Convert.ToDouble(str) * 100.0)).ToString() + "%";
                            this.aisinoDataGrid2.get_Rows()[i].Cells["SLV"].Value = str;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("超时"))
                {
                    this.log.Error(exception.ToString());
                }
                else
                {
                    HandleException.HandleError(exception);
                }
            }
        }

        private void BindMingXi()
        {
            Dictionary<string, string> dictionary;
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            if ((this.fpType == InvType.Common) || (this.fpType == InvType.Special))
            {
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "序号");
                dictionary.Add("Property", "XH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "70");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "商品名称");
                dictionary.Add("Property", "SPMC");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "200");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "规格型号");
                dictionary.Add("Property", "GGXH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "数量");
                dictionary.Add("Property", "SL");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单价");
                dictionary.Add("Property", "DJ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "金额");
                dictionary.Add("Property", "JE");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "税率");
                dictionary.Add("Property", "SLV");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "税额");
                dictionary.Add("Property", "SE");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "计量单位");
                dictionary.Add("Property", "JLDW");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "含税价标志");
                dictionary.Add("Property", "HSJBZ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("Visible", "False");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单据行性质");
                dictionary.Add("Property", "DJHXZ");
                dictionary.Add("Type", "Text");
                dictionary.Add("RowStyleField", "DJHXZ");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
            }
            else if (this.fpType == InvType.transportation)
            {
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "序号");
                dictionary.Add("Property", "XH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "70");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "商品名称");
                dictionary.Add("Property", "SPMC");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "200");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "规格型号");
                dictionary.Add("Property", "GGXH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "数量");
                dictionary.Add("Property", "SL");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单价");
                dictionary.Add("Property", "DJ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "金额");
                dictionary.Add("Property", "JE");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "税率");
                dictionary.Add("Property", "SLV");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "税额");
                dictionary.Add("Property", "SE");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "计量单位");
                dictionary.Add("Property", "JLDW");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "含税价标志");
                dictionary.Add("Property", "HSJBZ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单据行性质");
                dictionary.Add("Property", "DJHXZ");
                dictionary.Add("Type", "Text");
                dictionary.Add("RowStyleField", "DJHXZ");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
            }
            else
            {
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "序号");
                dictionary.Add("Property", "XH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "70");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "商品名称");
                dictionary.Add("Property", "SPMC");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "200");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "规格型号");
                dictionary.Add("Property", "GGXH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "数量");
                dictionary.Add("Property", "SL");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单价");
                dictionary.Add("Property", "DJ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "金额");
                dictionary.Add("Property", "JE");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "税率");
                dictionary.Add("Property", "SLV");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "税额");
                dictionary.Add("Property", "SE");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleRight");
                dictionary.Add("HeadAlign", "MiddleRight");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "计量单位");
                dictionary.Add("Property", "JLDW");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "含税价标志");
                dictionary.Add("Property", "HSJBZ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单据行性质");
                dictionary.Add("Property", "DJHXZ");
                dictionary.Add("Type", "Text");
                dictionary.Add("RowStyleField", "DJHXZ");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
            }
            this.aisinoDataGrid2.set_ColumeHead(list);
            DataGridViewColumn column = this.aisinoDataGrid2.get_Columns()["JE"];
            if (null != column)
            {
                column.DefaultCellStyle.Format = "0.00";
            }
            DataGridViewColumn column2 = this.aisinoDataGrid2.get_Columns()["SE"];
            if (null != column2)
            {
                column2.DefaultCellStyle.Format = "0.00";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.aisinoDataGrid1.ClearSelection();
            for (int i = 0; i < this.aisinoDataGrid1.get_Rows().Count; i++)
            {
                this.aisinoDataGrid1.get_Rows()[i].Cells["DJZT"].Value = "";
            }
            this.yuanseleCount = 0;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.selectNum = this.aisinoDataGrid1.get_SelectedRows().Count;
            if (this.selectNum > 0)
            {
                foreach (DataGridViewRow row in this.aisinoDataGrid1.get_SelectedRows())
                {
                    this.listBH.Add(row.Cells["BH"].Value.ToString());
                }
                this.listBH.Sort();
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                base.DialogResult = DialogResult.Abort;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.aisinoDataGrid1.SelectAll();
            for (int i = 0; i < this.aisinoDataGrid1.get_Rows().Count; i++)
            {
                this.aisinoDataGrid1.get_Rows()[i].Cells["DJZT"].Value = i + 1;
            }
            this.yuanseleCount = this.aisinoDataGrid1.get_Rows().Count;
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
            this.toolBtnQuit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolBtnQuit");
            this.toolBtnSelectAll = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolBtnSelectAll");
            this.toolBtnClear = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolBtnClear");
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.aisinoDataGrid2 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid2");
            this.splitContainer1 = this.xmlComponentLoader1.GetControlByName<AisinoSPL>("splitContainer1");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.toolBtnQuit.Click += new EventHandler(this.btnQuit_Click);
            this.toolBtnClear.Click += new EventHandler(this.btnClear_Click);
            this.aisinoDataGrid1.add_DataGridRowSelectionChanged(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowSelectionChanged));
            this.aisinoDataGrid1.add_DataGridCellFormatting(new EventHandler<DataGridViewCellFormattingEventArgs>(this.aisinoDataGrid1_DataGridCellFormatting));
            this.toolBtnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.aisinoDataGrid1.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent));
            this.aisinoDataGrid2.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid2_GoToPageEvent));
            this.aisinoDataGrid1.set_ReadOnly(true);
            this.aisinoDataGrid2.set_ReadOnly(true);
            this.aisinoDataGrid1.get_DataGrid().AllowUserToDeleteRows = false;
            this.aisinoDataGrid2.get_DataGrid().AllowUserToDeleteRows = false;
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.SelectDanJuForFP\Aisino.Fwkp.Wbjk.SelectDanJuForFP.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "SelectDanJuForFP";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "选择单据";
            base.Load += new EventHandler(this.SelectDanJuForFP_Load);
            base.ResumeLayout(false);
        }

        private void SelectDanJuForFP_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> dictionary;
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            if ((this.fpType == InvType.Common) || (this.fpType == InvType.Special))
            {
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单据状态");
                dictionary.Add("Property", "DJZT");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "性质");
                dictionary.Add("Property", "KPZT");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单据号");
                dictionary.Add("Property", "BH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleCenter");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "客户名称");
                dictionary.Add("Property", "GFMC");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "200");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "客户税号");
                dictionary.Add("Property", "GFSH");
                dictionary.Add("Type", "Text");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "纳税人识别号");
                dictionary.Add("Property", "GFYHZH");
                dictionary.Add("Type", "Text");
                list.Add(dictionary);
                dictionary.Add("Visible", "False");
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "客户地址电话");
                dictionary.Add("Property", "GFDZDH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleCenter");
                dictionary.Add("HeadAlign", "MiddleCenter");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "客户银行账号");
                dictionary.Add("Property", "GFYHZH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleCenter");
                dictionary.Add("HeadAlign", "MiddleCenter");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "金额合计");
                dictionary.Add("Property", "JEHJ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "税率");
                dictionary.Add("Property", "SLV");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "备注");
                dictionary.Add("Property", "BZ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
            }
            else if (this.fpType == InvType.transportation)
            {
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单据状态");
                dictionary.Add("Property", "DJZT");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "性质");
                dictionary.Add("Property", "KPZT");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单据号");
                dictionary.Add("Property", "BH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleCenter");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "客户名称");
                dictionary.Add("Property", "GFMC");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "200");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "客户税号");
                dictionary.Add("Property", "GFSH");
                dictionary.Add("Type", "Text");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "纳税人识别号");
                dictionary.Add("Property", "GFYHZH");
                dictionary.Add("Type", "Text");
                list.Add(dictionary);
                dictionary.Add("Visible", "False");
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "客户地址电话");
                dictionary.Add("Property", "GFDZDH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleCenter");
                dictionary.Add("HeadAlign", "MiddleCenter");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "客户银行账号");
                dictionary.Add("Property", "GFYHZH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleCenter");
                dictionary.Add("HeadAlign", "MiddleCenter");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "金额合计");
                dictionary.Add("Property", "JEHJ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "税率");
                dictionary.Add("Property", "SLV");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "备注");
                dictionary.Add("Property", "BZ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
            }
            else if (this.fpType == InvType.vehiclesales)
            {
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单据状态");
                dictionary.Add("Property", "DJZT");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "性质");
                dictionary.Add("Property", "KPZT");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "单据号");
                dictionary.Add("Property", "BH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "100");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleCenter");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "购买方名称");
                dictionary.Add("Property", "GFMC");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "200");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "身份证号码/组织机构代码");
                dictionary.Add("Property", "GFSH");
                dictionary.Add("Type", "Text");
                list.Add(dictionary);
                dictionary.Add("Visible", "False");
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "纳税人识别号");
                dictionary.Add("Property", "GFYHZH");
                dictionary.Add("Type", "Text");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "客户地址电话");
                dictionary.Add("Property", "GFDZDH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleCenter");
                dictionary.Add("HeadAlign", "MiddleCenter");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "客户银行账号");
                dictionary.Add("Property", "GFYHZH");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleCenter");
                dictionary.Add("HeadAlign", "MiddleCenter");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "价税合计");
                dictionary.Add("Property", "JEHJ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "增值税税率或征收率");
                dictionary.Add("Property", "SLV");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                list.Add(dictionary);
                dictionary = new Dictionary<string, string>();
                dictionary.Add("AisinoLBL", "备注");
                dictionary.Add("Property", "BZ");
                dictionary.Add("Type", "Text");
                dictionary.Add("Width", "150");
                dictionary.Add("Align", "MiddleLeft");
                dictionary.Add("HeadAlign", "MiddleLeft");
                dictionary.Add("Visible", "False");
                list.Add(dictionary);
            }
            this.aisinoDataGrid1.set_ColumeHead(list);
            DataGridViewColumn column = this.aisinoDataGrid1.get_Columns()["JEHJ"];
            if (null != column)
            {
                column.DefaultCellStyle.Format = "0.00";
            }
            if ((((this.fpType == InvType.Common) || (this.fpType == InvType.Special)) || (this.fpType == InvType.transportation)) || (this.fpType == InvType.vehiclesales))
            {
                this.BindMingXi();
            }
            int result = 1;
            int.TryParse(PropertyUtil.GetValue("WBJK_DJXZFP_DATAGRID1"), out result);
            this.fptkBLL.CurrentPage = result;
            this.aisinoDataGrid1.set_DataSource(this.fptkBLL.GetXSDJ(this.fpType, this.fptkBLL.Pagesize, this.fptkBLL.CurrentPage));
            this.btnClear_Click(sender, e);
        }

        public List<string> ListBH
        {
            get
            {
                return this.listBH;
            }
            set
            {
                this.listBH = value;
            }
        }

        public int SelectNum
        {
            get
            {
                return this.selectNum;
            }
            set
            {
                this.selectNum = value;
            }
        }

        public DataGridViewSelectedRowCollection SelectRows
        {
            get
            {
                return this.selectRows;
            }
            set
            {
                this.selectRows = value;
            }
        }
    }
}

