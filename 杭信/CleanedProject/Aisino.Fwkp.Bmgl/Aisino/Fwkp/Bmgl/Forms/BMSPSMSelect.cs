namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BMSPSMSelect : BaseForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoBTN btnQuery;
        private IContainer components;
        private BMSPSMManager customerManager;
        private AisinoDataSet dataSet;
        private string HiKeyWord;
        private string KeyWord;
        private ILog log;
        private string selectbm;
        public string SelectedBM;
        public string SelectedSJBM;
        private int SelectSJBM;
        private AisinoSPL splitContainer1;
        private TextBoxWait textBoxWaitKey;
        private TreeViewBM treeViewBM1;
        private XmlComponentLoader xmlComponentLoader1;

        public BMSPSMSelect()
        {
            this.SelectedBM = "";
            this.customerManager = new BMSPSMManager();
            this.log = LogUtil.GetLogger<BMSPSMSelect>();
            this.SelectedSJBM = "";
            this.HiKeyWord = string.Empty;
            this.KeyWord = "";
            this.selectbm = "";
            this.SelectSJBM = 1;
            this.Initialize();
        }

        public BMSPSMSelect(string keyWord)
        {
            this.SelectedBM = "";
            this.customerManager = new BMSPSMManager();
            this.log = LogUtil.GetLogger<BMSPSMSelect>();
            this.SelectedSJBM = "";
            this.HiKeyWord = string.Empty;
            this.KeyWord = "";
            this.selectbm = "";
            this.SelectSJBM = 0;
            this.HiKeyWord = keyWord;
            this.Initialize();
        }

        private void aisinoDataGrid1_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                string str = e.CurrentRow.Cells["WJ"].Value.ToString();
                if (this.SelectSJBM == 0)
                {
                    if (str == "1")
                    {
                        this.SelectedBM = e.CurrentRow.Cells["BM"].Value.ToString();
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                        base.Dispose();
                    }
                }
                else if ((this.SelectSJBM == 1) && (str == "0"))
                {
                    this.SelectedSJBM = e.CurrentRow.Cells["BM"].Value.ToString();
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
                    string str = row.Cells["WJ"].Value.ToString();
                    if (this.SelectSJBM == 0)
                    {
                        if (str == "1")
                        {
                            this.SelectedBM = row.Cells["BM"].Value.ToString();
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                            base.Dispose();
                        }
                    }
                    else if ((this.SelectSJBM == 1) && (str == "0"))
                    {
                        this.SelectedSJBM = row.Cells["BM"].Value.ToString();
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
            this.customerManager.Pagesize = e.PageSize;
            this.dataSet = this.customerManager.QueryGoodsTax(e.PageSize, e.PageNO);
            this.aisinoDataGrid1.DataSource = this.dataSet;
            this.ChangeCanSelect();
        }

        private void BMSPSMSelect_Load(object sender, EventArgs e)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "编码");
            item.Add("Property", "BM");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "True");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "名称");
            item.Add("Property", "MC");
            item.Add("Type", "Text");
            item.Add("Width", "200");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "简码");
            item.Add("Property", "JM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税号");
            item.Add("Property", "SH");
            item.Add("Type", "Text");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "快捷码");
            item.Add("Property", "KJM");
            item.Add("Type", "Text");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "上级编码");
            item.Add("Property", "SJBM");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "地址电话");
            item.Add("Property", "DZDH");
            item.Add("Type", "Text");
            item.Add("Width", "160");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "银行账号");
            item.Add("Property", "YHZH");
            item.Add("Type", "Text");
            item.Add("Width", "160");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "邮件地址");
            item.Add("Property", "YJDZ");
            item.Add("Type", "Text");
            item.Add("Width", "200");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "备注");
            item.Add("Property", "BZ");
            item.Add("Type", "Text");
            item.Add("Width", "200");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "应收科目");
            item.Add("Property", "YSKM");
            item.Add("Type", "Text");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "地区编码");
            item.Add("Property", "DQBM");
            item.Add("Type", "Text");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "地区名称");
            item.Add("Property", "DQMC");
            item.Add("Type", "Text");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "地区科目");
            item.Add("Property", "DQKM");
            item.Add("Type", "Text");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "文件性质");
            item.Add("Property", "WJ");
            item.Add("Type", "Text");
            item.Add("RowStyleField", "WJ");
            item.Add("Visible", "False");
            list.Add(item);
            this.aisinoDataGrid1.ColumeHead = list;
            this.aisinoDataGrid1.MultiSelect = false;
            this.aisinoDataGrid1.AborCellPainting = true;
            this.aisinoDataGrid1.ReadOnly = true;
            this.aisinoDataGrid1.DataGrid.AllowUserToDeleteRows = false;
            this.treeViewBM1.ReadOnly = true;
            this.treeViewBM1.RootNodeString = "客户编码";
            this.treeViewBM1.getListNodes += new TreeViewBM.GetListNodes(this.treeViewBM1_getListNodes);
            this.treeViewBM1.TreeLoad();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.customerManager.CurrentPage = 1;
                this.KeyWord = this.textBoxWaitKey.Text.Trim();
                this.dataSet = this.customerManager.QueryByKey(this.KeyWord, this.customerManager.Pagesize, this.customerManager.CurrentPage);
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
            if (this.aisinoDataGrid1.Rows.Count > 0)
            {
                if (this.HiKeyWord.Trim().Length > 0)
                {
                    string str = this.HiKeyWord.ToUpper();
                    foreach (DataGridViewRow row in (IEnumerable) this.aisinoDataGrid1.Rows)
                    {
                        if ((Convert.ToString(row.Cells["MC"].Value).Contains(this.HiKeyWord) || Convert.ToString(row.Cells["BM"].Value).Contains(this.HiKeyWord)) || ((Convert.ToString(row.Cells["JM"].Value).ToUpper().Contains(str) || Convert.ToString(row.Cells["KJM"].Value).Contains(str)) || Convert.ToString(row.Cells["SH"].Value).Contains(this.HiKeyWord)))
                        {
                            this.aisinoDataGrid1.Rows[row.Index].Selected = true;
                            this.aisinoDataGrid1.CurrentCell = this.aisinoDataGrid1.Rows[row.Index].Cells[0];
                            break;
                        }
                    }
                }
                this.aisinoDataGrid1.Focus();
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
            this.textBoxWaitKey = this.xmlComponentLoader1.GetControlByName<TextBoxWait>("textBoxWaitKey");
            this.textBoxWaitKey.WaterMarkString = "输入关键字(编码,名称)";
            this.btnQuery = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnQuery");
            this.btnQuery.Visible = false;
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.treeViewBM1 = this.xmlComponentLoader1.GetControlByName<TreeViewBM>("treeViewBM1");
            this.splitContainer1 = this.xmlComponentLoader1.GetControlByName<AisinoSPL>("splitContainer1");
            this.textBoxWaitKey.FontWater = new Font("微软雅黑", 8f, FontStyle.Italic);
            this.textBoxWaitKey.TextChangedWaitGetText += new GetTextEventHandler(this.textBoxWaitKey_TextChangedWaitGetText);
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.aisinoDataGrid1.GoToPageEvent += new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent);
            this.aisinoDataGrid1.DataGridRowDbClickEvent += new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowDbClickEvent);
            this.aisinoDataGrid1.DataGridRowKeyDown += new KeyEventHandler(this.aisinoDataGrid1_DataGridRowKeyDown);
            this.aisinoDataGrid1.ShowAllChkVisible = false;
            this.treeViewBM1.onTreeNodeClick += new TreeViewBM.OnTreeNodeClick(this.treeViewBM1_onTreeNodeClick);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMSPSMSelect));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x3e8, 600);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "BMKHSelect";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.BMSPSMSelect\Aisino.Fwkp.Bmgl.Forms.BMSPSMSelect.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3e8, 600);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.KeyPreview = true;
            base.Location = new Point(0, 0);
            base.Name = "BMSPSMSelect";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "商品税目选择";
            base.Load += new EventHandler(this.BMSPSMSelect_Load);
            base.ResumeLayout(false);
        }

        private void textBoxWaitKey_TextChangedWaitGetText(object sender, GetTextEventArgs e)
        {
            try
            {
                this.customerManager.CurrentPage = 1;
                this.KeyWord = e.StartText.Trim();
                this.dataSet = this.customerManager.QueryByKey(this.KeyWord, this.customerManager.Pagesize, this.customerManager.CurrentPage);
                this.aisinoDataGrid1.DataSource = this.dataSet;
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private List<TreeNodeTemp> treeViewBM1_getListNodes(string ParentBM)
        {
            return this.customerManager.listNodes(ParentBM);
        }

        private bool treeViewBM1_onTreeNodeClick(object sender, TreeViewEventArgs e)
        {
            if (e.Node != this.treeViewBM1.TopNode)
            {
                this.selectbm = e.Node.Name;
            }
            else
            {
                this.selectbm = "";
            }
            this.customerManager.CurrentPage = 1;
            this.dataSet = this.customerManager.SelectNodeDisplay(this.selectbm, this.customerManager.Pagesize, this.customerManager.CurrentPage);
            this.aisinoDataGrid1.DataSource = this.dataSet;
            this.ChangeCanSelect();
            return true;
        }
    }
}

