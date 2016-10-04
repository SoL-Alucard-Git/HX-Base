namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.DAL;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BMSPFLSelect : BaseForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoBTN btnQuery;
        private IContainer components;
        private AisinoDataSet dataSet;
        protected object father;
        private BLL.BMSPFLManager fpflManager = new BLL.BMSPFLManager();
        private string HiKeyWord;
        public bool Isxtsp;
        private string KeyWord = "";
        private ILog log = LogUtil.GetLogger<BMSPSelect>();
        private string selectbm;
        public string SelectBM;
        public string SelectBMMC;
        private AisinoSPL splitContainer1;
        private BLL.BMSPManager spManager = new BLL.BMSPManager();
        private ToolStripTextBox textBoxWaitKey;
        private ToolStripLabel toolSearchLbl;
        private ToolStrip toolStrip1;
        private TreeViewBM treeViewBM1;
        private XmlComponentLoader xmlComponentLoader1;

        public BMSPFLSelect(string keyWord, bool isxtsp, object father)
        {
            this.HiKeyWord = keyWord;
            this.father = father;
            this.Isxtsp = isxtsp;
            this.Initialize();
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "编码");
            item.Add("Property", "BM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "合并编码");
            item.Add("Property", "HBBM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "名称");
            item.Add("Property", "MC");
            item.Add("Type", "Text");
            item.Add("Width", "200");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "说明");
            item.Add("Property", "SM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLVStr");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLV");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "增值税特殊管理");
            item.Add("Property", "ZZSTSGL");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "增值税政策依据");
            item.Add("Property", "ZZSZCYJ");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "增值税特殊内容代码");
            item.Add("Property", "ZZSTSNRDM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "消费税管理");
            item.Add("Property", "XFSGL");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "消费税政策依据");
            item.Add("Property", "XFSZCYJ");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "消费税特殊内容代码");
            item.Add("Property", "XFSTSNRDM");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "关键字");
            item.Add("Property", "GJZ");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "统计局编码");
            item.Add("Property", "TJJBM");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "海关进出口商品品目");
            item.Add("Property", "HGJCKSPPM");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "汇总项");
            item.Add("Property", "HZX");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Visible", "False");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "汇总项");
            item.Add("Property", "HZXStr");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "是否隐藏");
            item.Add("Property", "ISHIDE");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Visible", "False");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "是否隐藏");
            item.Add("Property", "ISHIDEStr");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "海关进出口商品品目");
            item.Add("Property", "HGJCKSPPM");
            item.Add("Type", "Text");
            item.Add("Width", "200");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "编码表版本号");
            item.Add("Property", "BMB_BBH");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "版本号");
            item.Add("Property", "BBH");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "可用状态");
            item.Add("Property", "KYZT");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "启用时间");
            item.Add("Property", "QYSJ");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "过渡期截止时间");
            item.Add("Property", "GDQJZSJ");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "更新时间");
            item.Add("Property", "GXSJ");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "上级编码");
            item.Add("Property", "SJBM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
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
            this.treeViewBM1.RootNodeString = "税收分类编码";
            this.treeViewBM1.getListNodes += new TreeViewBM.GetListNodes(this.treeViewBM1_getListNodes);
            this.treeViewBM1.TreeLoad();
        }

        private void aisinoDataGrid1_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                this.SelectBM = e.CurrentRow.Cells["BM"].Value.ToString();
                this.SelectBMMC = e.CurrentRow.Cells["MC"].Value.ToString();
                DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                if (this.Isxtsp)
                {
                    bool isSPBMSel = BMType.BM_SP == this.GetBMType();
                    if (manager.CanUseThisSPFLBM(this.SelectBM, isSPBMSel, true))
                    {
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                        base.Dispose();
                    }
                }
                else
                {
                    bool flag2 = BMType.BM_SP == this.GetBMType();
                    if (manager.CanUseThisSPFLBM(this.SelectBM, flag2, false))
                    {
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

        private void aisinoDataGrid1_DataGridRowKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter) && (this.aisinoDataGrid1.SelectedRows.Count > 0))
                {
                    string str = this.aisinoDataGrid1.SelectedRows[0].Cells["WJ"].Value.ToString();
                    this.SelectBM = this.aisinoDataGrid1.SelectedRows[0].Cells["BM"].Value.ToString();
                    this.SelectBMMC = this.aisinoDataGrid1.SelectedRows[0].Cells["MC"].Value.ToString();
                    string str2 = this.aisinoDataGrid1.SelectedRows[0].Cells["HZX"].Value.ToString().Trim();
                    DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                    if (this.Isxtsp)
                    {
                        if (((str == "1") && (str2 == "N")) && manager.CanUseThisSPFLBM(this.SelectBM, true, true))
                        {
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                            base.Dispose();
                        }
                    }
                    else
                    {
                        bool isSPBMSel = BMType.BM_SP == this.GetBMType();
                        if (((str == "1") && (str2 == "N")) && manager.CanUseThisSPFLBM(this.SelectBM, isSPBMSel, false))
                        {
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                            base.Dispose();
                        }
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
            this.fpflManager.CurrentPage = e.PageNO;
            this.dataSet = this.fpflManager.QueryData(e.PageSize, e.PageNO);
            this.aisinoDataGrid1.DataSource = this.dataSet;
            this.ChangeCanSelect();
        }

        private void ChangeCanSelect()
        {
            if (this.HiKeyWord.Trim().Length > 0)
            {
                string str = this.HiKeyWord.ToUpper();
                foreach (DataGridViewRow row in (IEnumerable) this.aisinoDataGrid1.Rows)
                {
                    if ((Convert.ToString(row.Cells["MC"].Value).Contains(this.HiKeyWord) || Convert.ToString(row.Cells["BM"].Value).Contains(this.HiKeyWord)) || Convert.ToString(row.Cells["GJZ"].Value).Contains(str))
                    {
                        this.aisinoDataGrid1.Rows[row.Index].Selected = true;
                    }
                    string str2 = row.Cells["BM"].Value.ToString().Trim();
                    if (!(str2 == "303") && !(str2 == "30301"))
                    {
                        bool flag1 = str2 == "30302";
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

        private BMType GetBMType()
        {
            switch (this.father.GetType().FullName)
            {
                case "Aisino.Fwkp.Bmgl.Forms.BMSP_Edit":
                    return BMType.BM_SP;

                case "Aisino.Fwkp.Bmgl.Forms.BMFYXM_Edit":
                    return BMType.BM_FYXM;

                case "Aisino.Fwkp.Bmgl.Forms.BMCL_Edit":
                    return BMType.BM_CL;
            }
            return BMType.BM_SPFL;
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
            this.aisinoDataGrid1.ShowAllChkVisible = false;
            this.textBoxWaitKey.ToolTipText = "输入关键字(税收分类编码,名称,关键字)";
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
            this.Text = "税收分类编码选择";
            base.ResumeLayout(false);
        }

        private bool IsXTSP(string bm)
        {
            if ((bm == null) || string.Empty.Equals(bm))
            {
                return false;
            }
            BMSPModel model = (BMSPModel) this.spManager.GetModel(bm);
            return ((model.XTHASH != null) && (model.XTHASH != ""));
        }

        private void textBoxWaitKey_Paint(object sender, PaintEventArgs e)
        {
            CommonFunc.DrawBorder(sender, e.Graphics, SystemColor.GRID_ALTROW_BACKCOLOR, Color.FromArgb(0, 0xbb, 0xff), this.textBoxWaitKey.Width, this.textBoxWaitKey.Height);
        }

        private void textBoxWaitKey_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.fpflManager.CurrentPage = 1;
                this.KeyWord = (sender as ToolStripTextBox).Text.Trim();
                this.dataSet = this.fpflManager.QueryByKeyDisplaySEL(this.KeyWord, this.fpflManager.Pagesize, this.fpflManager.CurrentPage);
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
            return this.fpflManager.listNodesISHIDE(ParentBM);
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
            this.fpflManager.CurrentPage = 1;
            this.dataSet = this.fpflManager.SelectNodeDisplaySEL(this.selectbm, this.fpflManager.Pagesize, this.fpflManager.CurrentPage);
            this.aisinoDataGrid1.DataSource = this.dataSet;
            this.ChangeCanSelect();
            return true;
        }
    }
}

