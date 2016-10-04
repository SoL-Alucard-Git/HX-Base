namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class BMXZQY : DockForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private ToolStripButton btnAdd;
        private ToolStripButton btnDel;
        private ToolStripButton btnExit;
        private ToolStripButton btnExport;
        private ToolStripButton btnImport;
        private ToolStripButton btnModify;
        private IContainer components;
        private AisinoDataSet dataSet;
        private BMXZQYManager districtManager = new BMXZQYManager();
        private string KeyWord = "";
        private ILog log = LogUtil.GetLogger<BMXZQY>();
        private string result;
        private string selectbm = "";
        private AisinoSPL splitContainer1;
        private ToolStripTextBox textBoxWaitKey;
        private ToolStripLabel toolSearchLbl;
        private ToolStrip toolStrip1;
        private XmlComponentLoader xmlComponentLoader1;

        public BMXZQY()
        {
            this.Initialize();
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
            this.aisinoDataGrid1.ColumeHead = list;
            this.aisinoDataGrid1.MultiSelect = false;
            this.aisinoDataGrid1.AborCellPainting = true;
            this.aisinoDataGrid1.ReadOnly = true;
            this.aisinoDataGrid1.DataGrid.AllowUserToDeleteRows = false;
            this.KeyWord = "%";
            this.dataSet = this.districtManager.QueryByKey(this.KeyWord, this.districtManager.Pagesize, this.districtManager.CurrentPage);
            this.aisinoDataGrid1.DataSource = this.dataSet;
            this.aisinoDataGrid1.Refresh();
        }

        private void aisinoDataGrid1_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                BMXZQY_Edit edit = new BMXZQY_Edit(e.CurrentRow.Cells["BM"].Value.ToString(), true);
                if (edit.ShowDialog() == DialogResult.OK)
                {
                    this.dataSet = this.districtManager.QueryDistrict(this.districtManager.Pagesize, this.districtManager.CurrentPage);
                    this.aisinoDataGrid1.DataSource = this.dataSet;
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
            this.districtManager.CurrentPage = e.PageNO;
            this.districtManager.Pagesize = e.PageSize;
            this.dataSet = this.districtManager.QueryDistrict(e.PageSize, e.PageNO);
            this.aisinoDataGrid1.DataSource = this.dataSet;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                BMXZQY_Edit edit = new BMXZQY_Edit(string.Empty, this);
                if (edit.ShowDialog() == DialogResult.OK)
                {
                    this.dataSet = this.districtManager.QueryDistrict(this.districtManager.Pagesize, this.districtManager.CurrentPage);
                    this.aisinoDataGrid1.DataSource = this.dataSet;
                    this.aisinoDataGrid1.Refresh();
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoDataGrid1.SelectedRows.Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-235106");
                }
                else if (MessageManager.ShowMsgBox("INP-235107") == DialogResult.OK)
                {
                    string str2 = "";
                    string xzqEntityCode = this.aisinoDataGrid1.SelectedRows[0].Cells["BM"].Value.ToString();
                    if (this.districtManager.DeleteDistrict(xzqEntityCode) == "1")
                    {
                        str2 = "INP-235102";
                    }
                    else
                    {
                        str2 = "INP-235103";
                    }
                    this.aisinoDataGrid1.DataSource = this.districtManager.QueryDistrict(this.districtManager.Pagesize, this.districtManager.CurrentPage);
                    this.aisinoDataGrid1.Refresh();
                    MessageManager.ShowMsgBox(str2);
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportCode code = new ExportCode {
                    BmType = BMType.District
                };
                if (code.ShowDialog() == DialogResult.OK)
                {
                    string str = "";
                    if (!Directory.Exists(code.FilePath.Remove(code.FilePath.LastIndexOf(@"\"))))
                    {
                        MessageManager.ShowMsgBox("INP-235129");
                    }
                    else
                    {
                        try
                        {
                            str = this.districtManager.ExportDistrict(code.FilePath, code.Separator);
                        }
                        catch
                        {
                            throw;
                        }
                        if (str == "0")
                        {
                            MessageManager.ShowMsgBox("INP-235119");
                        }
                        else
                        {
                            MessageManager.ShowMsgBox("INP-235120", "导出失败", new string[] { str });
                        }
                        code.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                ImportCode code = new ImportCode {
                    BmType = BMType.District
                };
                if (code.ShowDialog() == DialogResult.OK)
                {
                    ImportResult result;
                    code.Visible = false;
                    try
                    {
                        result = this.districtManager.ImportDistrict(code.FilePath);
                    }
                    catch
                    {
                        throw;
                    }
                    new ImportReport(result).ShowDialog();
                    this.aisinoDataGrid1.DataSource = this.districtManager.QueryDistrict(this.districtManager.Pagesize, this.districtManager.CurrentPage);
                    this.aisinoDataGrid1.Refresh();
                }
            }
            catch (CustomException exception)
            {
                MessageBoxHelper.Show(exception.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                this.log.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoDataGrid1.SelectedRows.Count == 1)
                {
                    BMXZQY_Edit edit = new BMXZQY_Edit(this.aisinoDataGrid1.SelectedRows[0].Cells["BM"].Value.ToString(), true);
                    if (edit.ShowDialog() == DialogResult.OK)
                    {
                        this.dataSet = this.districtManager.QueryDistrict(this.districtManager.Pagesize, this.districtManager.CurrentPage);
                        this.aisinoDataGrid1.DataSource = this.dataSet;
                        this.aisinoDataGrid1.Refresh();
                    }
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-235105");
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.districtManager.CurrentPage = 1;
            this.KeyWord = this.textBoxWaitKey.Text.Trim();
            this.dataSet = this.districtManager.QueryByKey(this.KeyWord, this.districtManager.Pagesize, this.districtManager.CurrentPage);
            this.aisinoDataGrid1.DataSource = this.dataSet;
            this.aisinoDataGrid1.Refresh();
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
            this.btnModify = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolModify");
            this.btnAdd = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolAdd");
            this.btnDel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolDel");
            this.btnExport = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolExport");
            this.btnImport = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolImport");
            this.splitContainer1 = this.xmlComponentLoader1.GetControlByName<AisinoSPL>("splitContainer1");
            this.btnExit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolExit");
            this.textBoxWaitKey = this.xmlComponentLoader1.GetControlByName<ToolStripTextBox>("textBoxWaitKey");
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.aisinoDataGrid1.GoToPageEvent += new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent);
            this.aisinoDataGrid1.DataGridRowDbClickEvent += new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowDbClickEvent);
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.btnDel.Click += new EventHandler(this.btnDel_Click);
            this.textBoxWaitKey.TextChanged += new EventHandler(this.textBoxWaitKey_TextChanged);
            this.btnExport.Click += new EventHandler(this.btnExport_Click);
            this.btnImport.Click += new EventHandler(this.btnImport_Click);
            this.textBoxWaitKey.ToolTipText = "输入关键字(编码,名称)";
            this.toolSearchLbl = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolSearchLbl");
            this.toolSearchLbl.Alignment = ToolStripItemAlignment.Right;
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.textBoxWaitKey.Paint += new PaintEventHandler(this.textBoxWaitKey_Paint);
            this.textBoxWaitKey.Alignment = ToolStripItemAlignment.Right;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnImport.Enabled = false;
            this.btnExport.Enabled = false;
            this.aisinoDataGrid1.ShowAllChkVisible = false;
            this.splitContainer1.Panel1Collapsed = true;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMXZQY));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.BMXZQY\Aisino.Fwkp.Bmgl.Forms.BMXZQY.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.Name = "BMXZQY";
            this.Text = "行政区编码设置";
            base.ResumeLayout(false);
        }

        public void RefreshGrid()
        {
            this.dataSet = this.districtManager.QueryDistrict(this.districtManager.Pagesize, this.districtManager.CurrentPage);
            this.aisinoDataGrid1.DataSource = this.dataSet;
        }

        public void RefreshGridTree()
        {
            this.dataSet = this.districtManager.QueryDistrict(this.districtManager.Pagesize, this.districtManager.CurrentPage);
            this.aisinoDataGrid1.DataSource = this.dataSet;
        }

        private void textBoxWaitKey_Paint(object sender, PaintEventArgs e)
        {
            CommonFunc.DrawBorder(sender, e.Graphics, SystemColor.GRID_ALTROW_BACKCOLOR, Color.FromArgb(0, 0xbb, 0xff), this.textBoxWaitKey.Width, this.textBoxWaitKey.Height);
        }

        private void textBoxWaitKey_TextChanged(object sender, EventArgs e)
        {
            this.districtManager.CurrentPage = 1;
            this.KeyWord = (sender as ToolStripTextBox).Text.Trim();
            if (this.KeyWord != null)
            {
                if (this.KeyWord.IndexOf("%") != -1)
                {
                    this.KeyWord.Replace("%", "[%]");
                }
                else if (this.KeyWord.IndexOf("_") != -1)
                {
                    this.KeyWord.Replace("_", "[_]");
                }
                if (this.KeyWord == "")
                {
                    this.KeyWord = "%";
                }
                this.dataSet = this.districtManager.QueryByKey(this.KeyWord, this.districtManager.Pagesize, this.districtManager.CurrentPage);
                this.aisinoDataGrid1.DataSource = this.dataSet;
                this.aisinoDataGrid1.Refresh();
            }
        }

        private List<TreeNodeTemp> treeViewBM1_getListNodes(string ParentBM)
        {
            return this.districtManager.listNodes(ParentBM);
        }

        private void treeViewBM1_onClickAdd(object sender, TreeSelectEventArgs e)
        {
        }

        private void treeViewBM1_onClickAddFenLei(object sender, TreeSelectEventArgs e)
        {
            try
            {
                BMXZQYFenLei lei = new BMXZQYFenLei(e.BmString, this);
                if (lei.ShowDialog() == DialogResult.OK)
                {
                    this.dataSet = this.districtManager.QueryDistrict(this.districtManager.Pagesize, this.districtManager.CurrentPage);
                    this.aisinoDataGrid1.DataSource = this.dataSet;
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void treeViewBM1_onClickDelete(object sender, EventArgs e)
        {
        }

        private bool treeViewBM1_onClickJianWei(object sender, EventArgs e)
        {
            return this.ZengWeiJianWei(false);
        }

        private void treeViewBM1_onClickModify(object sender, TreeSelectEventArgs e)
        {
        }

        private bool treeViewBM1_onClickZengWei(object sender, EventArgs e)
        {
            return this.ZengWeiJianWei(true);
        }

        private bool treeViewBM1_onTreeNodeClick(object sender, TreeViewEventArgs e)
        {
            return false;
        }

        private bool ZengWeiJianWei(bool isZengWei)
        {
            return false;
        }
    }
}

