namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using Aisino.Fwkp.Bmgl.Common;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class BMSPSelect : BaseForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoBTN btnQuery;
        private IContainer components;
        private AisinoDataSet dataSet;
        private string HiKeyWord;
        private string KeyWord = "";
        private ILog log = LogUtil.GetLogger<BMSPSelect>();
        private string selectbm;
        public string SelectBM;
        public double SelectSlv = -1.0;
        private BMSPManager shangpinManager = new BMSPManager();
        private int ShowCanselect;
        private string SpecialFlag = string.Empty;
        private string SpecialSP = string.Empty;
        private AisinoSPL splitContainer1;
        private ToolStripTextBox textBoxWaitKey;
        private ToolStripLabel toolSearchLbl;
        private ToolStrip toolStrip1;
        private TreeViewBM treeViewBM1;
        private XmlComponentLoader xmlComponentLoader1;

        public BMSPSelect(string keyWord, double selectSlv, int showCanselect, string specialSP, string specialFlag = "")
        {
            this.SelectSlv = selectSlv;
            this.HiKeyWord = keyWord;
            this.ShowCanselect = showCanselect;
            this.SpecialSP = specialSP;
            this.SpecialFlag = specialFlag;
            this.Initialize();
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "编码");
            item.Add("Property", "BM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "True");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "名称");
            item.Add("Property", "MC");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "简码");
            item.Add("Property", "JM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "商品税目");
            item.Add("Property", "SPSM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLV");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLVStr");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "规格型号");
            item.Add("Property", "GGXH");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "计量单位");
            item.Add("Property", "JLDW");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "快捷码");
            item.Add("Property", "KJM");
            item.Add("Type", "Text");
            item.Add("Visible", "False");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "上级编码");
            item.Add("Property", "SJBM");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单价");
            item.Add("Property", "DJ");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单价Double");
            item.Add("Property", "DJStr");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "含税价标志");
            item.Add("Property", "HSJBZ");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "含税价标志");
            item.Add("Property", "HSJBZSTR");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "True");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "销售收入科目");
            item.Add("Property", "XSSRKM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "应缴增值税科目");
            item.Add("Property", "YJZZSKM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "销售退回科目");
            item.Add("Property", "XSTHKM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "中外合作油气田");
            item.Add("Property", "HYSY");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Visible", "False");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "Hash值");
            item.Add("Property", "XTHASH");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "隐藏标志");
            item.Add("Property", "ISHIDE");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "隐藏标志");
            item.Add("Property", "ISHIDEBOOL");
            item.Add("Type", "ComboBox");
            DataTable table = new DataTable();
            table.Columns.Add("ISHIDE", Type.GetType("System.String"));
            DataRow row = table.NewRow();
            row["ISHIDE"] = "否";
            table.Rows.Add(row);
            DataRow row2 = table.NewRow();
            row2["ISHIDE"] = "是";
            table.Rows.Add(row2);
            this.aisinoDataGrid1.ComboBoxColumnDataSource.Add("IsHideSource", table);
            item.Add("DataSource", "IsHideSource");
            item.Add("DisplayMember", "ISHIDE");
            item.Add("ValueMember", "ISHIDE");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "True");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "稀土商品编码");
            item.Add("Property", "XTCODE");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税收分类编码");
            item.Add("Property", "SPFL");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Width", "80");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", Flbm.IsYM().ToString());
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税收分类名称");
            item.Add("Property", "SPFLMC");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Width", "200");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", Flbm.IsYM().ToString());
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "享受优惠政策");
            item.Add("Property", "YHZC");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", Flbm.IsYM().ToString());
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "优惠政策类型");
            item.Add("Property", "YHZCMC");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Width", "200");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", Flbm.IsYM().ToString());
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "零税率标识");
            item.Add("Property", "LSLVBS");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Width", "200");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "SPZL是啥？");
            item.Add("Property", "SPZL");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "SPSX是啥？");
            item.Add("Property", "SPSX");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "文件性质");
            item.Add("Property", "WJ");
            item.Add("Type", "Text");
            item.Add("RowStyleField", "WJ");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            this.aisinoDataGrid1.ColumeHead = list;
            this.aisinoDataGrid1.MultiSelect = false;
            this.aisinoDataGrid1.AborCellPainting = true;
            this.aisinoDataGrid1.ReadOnly = true;
            this.aisinoDataGrid1.DataGrid.AllowUserToDeleteRows = false;
            this.treeViewBM1.ReadOnly = true;
            this.treeViewBM1.RootNodeString = "商品编码";
            this.treeViewBM1.getListNodes += new TreeViewBM.GetListNodes(this.treeViewBM1_getListNodes);
            this.treeViewBM1.TreeLoad();
        }

        private void aisinoDataGrid1_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                string str = e.CurrentRow.Cells["WJ"].Value.ToString();
                if ((("1" == str) && ("0" == this.SpecialFlag)) && string.IsNullOrEmpty(e.CurrentRow.Cells["SPFL"].Value.ToString()))
                {
                    MessageBox.Show("无税收分类信息的条目不可选！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if ((str == "1") && ((this.SelectSlv == -1.0) || (Convert.ToDouble(e.CurrentRow.Cells["SLV"].Value) == this.SelectSlv)))
                {
                    this.SelectBM = e.CurrentRow.Cells["BM"].Value.ToString();
                    this.SelectSlv = Convert.ToDouble(e.CurrentRow.Cells["SLV"].Value);
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                    base.Dispose();
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void aisinoDataGrid1_DataGridRowKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter) && (this.aisinoDataGrid1.SelectedRows.Count > 0))
                {
                    DataGridViewRow row = this.aisinoDataGrid1.SelectedRows[0];
                    if ((row.Cells["WJ"].Value.ToString() == "1") && ((this.SelectSlv == -1.0) || (Convert.ToDouble(row.Cells["SLV"].Value) == this.SelectSlv)))
                    {
                        this.SelectBM = row.Cells["BM"].Value.ToString();
                        this.SelectSlv = Convert.ToDouble(row.Cells["SLV"].Value);
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                        base.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            PropertyUtil.SetValue("pagesize", e.PageSize.ToString());
            this.shangpinManager.CurrentPage = e.PageNO;
            this.dataSet = this.shangpinManager.QueryData(e.PageSize, e.PageNO);
            this.aisinoDataGrid1.DataSource = this.dataSet;
            this.ChangeCanSelect();
        }

        private void BMSPSelect_Load(object sender, EventArgs e)
        {
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.shangpinManager.CurrentPage = 1;
                this.KeyWord = this.textBoxWaitKey.Text.Trim();
                this.dataSet = this.shangpinManager.QueryByKeySEL(this.KeyWord, this.shangpinManager.Pagesize, this.shangpinManager.CurrentPage);
                this.aisinoDataGrid1.DataSource = this.dataSet;
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void ChangeCanSelect()
        {
            if (this.HiKeyWord.Trim().Length > 0)
            {
                string str = this.HiKeyWord.ToUpper();
                foreach (DataGridViewRow row in (IEnumerable) this.aisinoDataGrid1.Rows)
                {
                    if ((Convert.ToString(row.Cells["MC"].Value).Contains(this.HiKeyWord) || Convert.ToString(row.Cells["BM"].Value).Contains(this.HiKeyWord)) || (Convert.ToString(row.Cells["JM"].Value).ToUpper().Contains(str) || Convert.ToString(row.Cells["KJM"].Value).Contains(str)))
                    {
                        this.aisinoDataGrid1.Rows[row.Index].Selected = true;
                        this.aisinoDataGrid1.CurrentCell = this.aisinoDataGrid1.Rows[row.Index].Cells[0];
                    }
                }
            }
            if (this.SelectSlv != -1.0)
            {
                foreach (DataGridViewRow row2 in (IEnumerable) this.aisinoDataGrid1.Rows)
                {
                    if (this.SelectSlv != Convert.ToDouble(row2.Cells["SLV"].Value))
                    {
                        row2.DefaultCellStyle.BackColor = Color.Pink;
                    }
                }
            }
            this.aisinoDataGrid1.Focus();
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
            this.treeViewBM1 = this.xmlComponentLoader1.GetControlByName<TreeViewBM>("treeViewBM1");
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.textBoxWaitKey = this.xmlComponentLoader1.GetControlByName<ToolStripTextBox>("textBoxWaitKey");
            this.btnQuery = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnQuery");
            this.btnQuery.Visible = false;
            this.treeViewBM1.onTreeNodeClick += new TreeViewBM.OnTreeNodeClick(this.treeViewBM1_onTreeNodeClick);
            this.aisinoDataGrid1.GoToPageEvent += new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent);
            this.aisinoDataGrid1.DataGridRowDbClickEvent += new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowDbClickEvent);
            this.aisinoDataGrid1.DataGridRowKeyDown += new KeyEventHandler(this.aisinoDataGrid1_DataGridRowKeyDown);
            this.textBoxWaitKey.TextChanged += new EventHandler(this.textBoxWaitKey_TextChanged);
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.aisinoDataGrid1.ShowAllChkVisible = false;
            this.textBoxWaitKey.ToolTipText = "输入关键字(商品编码,名称,简码或规格型号)";
            this.toolSearchLbl = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolSearchLbl");
            this.toolSearchLbl.Alignment = ToolStripItemAlignment.Right;
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.textBoxWaitKey.Paint += new PaintEventHandler(this.textBoxWaitKey_Paint);
            this.textBoxWaitKey.Alignment = ToolStripItemAlignment.Right;
            this.splitContainer1 = this.xmlComponentLoader1.GetControlByName<AisinoSPL>("splitContainer1");
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMSPSelect));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x44c, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "BMSPSelect";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.BMSPSelect\Aisino.Fwkp.Bmgl.Forms.BMSPSelect.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x44c, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "BMSPSelect";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "商品编码选择";
            base.Load += new EventHandler(this.BMSPSelect_Load);
            base.ResumeLayout(false);
        }

        private void textBoxWaitKey_Paint(object sender, PaintEventArgs e)
        {
            CommonFunc.DrawBorder(sender, e.Graphics, SystemColor.GRID_ALTROW_BACKCOLOR, Color.FromArgb(0, 0xbb, 0xff), this.textBoxWaitKey.Width, this.textBoxWaitKey.Height);
        }

        private void textBoxWaitKey_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.shangpinManager.CurrentPage = 1;
                this.KeyWord = (sender as ToolStripTextBox).Text.Trim();
                if (this.SpecialSP == string.Empty)
                {
                    if ((this.ShowCanselect == 0) && (this.SelectSlv >= 0.0))
                    {
                        this.dataSet = this.shangpinManager.QueryByKeyDisplaySEL(this.KeyWord, this.SelectSlv, this.shangpinManager.Pagesize, this.shangpinManager.CurrentPage);
                    }
                    else
                    {
                        this.dataSet = this.shangpinManager.QueryByKeyDisplaySEL(this.KeyWord, this.shangpinManager.Pagesize, this.shangpinManager.CurrentPage);
                    }
                }
                else
                {
                    this.dataSet = this.shangpinManager.QueryByKeyDisplaySEL(this.KeyWord, this.SpecialSP, this.shangpinManager.Pagesize, this.shangpinManager.CurrentPage);
                }
                this.aisinoDataGrid1.DataSource = this.dataSet;
                this.ChangeCanSelect();
                this.textBoxWaitKey.Focus();
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private List<TreeNodeTemp> treeViewBM1_getListNodes(string ParentBM)
        {
            return this.shangpinManager.listNodesISHIDE(ParentBM);
        }

        private bool treeViewBM1_onTreeNodeClick(object sender, TreeViewEventArgs e)
        {
            this.textBoxWaitKey.Text = "";
            if (e.Node != this.treeViewBM1.TopNode)
            {
                this.selectbm = e.Node.Name;
            }
            else
            {
                this.selectbm = "";
            }
            this.shangpinManager.CurrentPage = 1;
            if (this.SpecialSP == string.Empty)
            {
                if ((this.ShowCanselect == 0) && (this.SelectSlv >= 0.0))
                {
                    this.dataSet = this.shangpinManager.SelectNodeDisplaySEL(this.selectbm, this.SelectSlv, this.shangpinManager.Pagesize, this.shangpinManager.CurrentPage);
                }
                else
                {
                    this.dataSet = this.shangpinManager.SelectNodeDisplaySEL(this.selectbm, this.shangpinManager.Pagesize, this.shangpinManager.CurrentPage);
                }
            }
            else
            {
                this.dataSet = this.shangpinManager.SelectNodeDisplaySEL(this.selectbm, this.SpecialSP, this.shangpinManager.Pagesize, this.shangpinManager.CurrentPage);
            }
            this.aisinoDataGrid1.DataSource = this.dataSet;
            this.ChangeCanSelect();
            return true;
        }
    }
}

