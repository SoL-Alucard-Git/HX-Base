namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.IBLL;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public abstract class BMBase<tEdit, tFenLei, tSelect> : DockForm
    {
        protected AisinoDataGrid aisinoDataGrid1;
        protected IBMBaseManager bllManager;
        private IContainer components;
        private AisinoDataSet dataSet;
        private string KeyWord;
        protected ILog log;
        protected AisinoPNL panel1;
        private string selectbm;
        protected AisinoSPL splitContainer1;
        protected ToolStripTextBox textBoxWaitKey;
        private ToolStripButton tool_ChaZhao;
        private ToolStripButton tool_GeShi;
        private ToolStripButton tool_TongJi;
        protected ToolStripButton toolAdd;
        protected ToolStripButton toolDel;
        protected ToolStripButton toolExit;
        protected ToolStripButton toolExport;
        protected ToolStripButton toolImport;
        protected ToolStripButton toolModify;
        private ToolStripButton toolPrint;
        protected ToolStripLabel toolSearchLbl;
        protected ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        protected ToolStripSeparator toolStripSeparator2;
        protected ToolStripSeparator toolStripSeparator3;
        protected TreeViewBM treeViewBM1;

        public BMBase()
        {
            this.dataSet = new AisinoDataSet();
            this.KeyWord = string.Empty;
            this.selectbm = "";
            this.Initialize();
        }

        protected virtual void aisinoDataGrid1_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            if ((("Object" != typeof(tEdit).Name) || ("Object" != typeof(tFenLei).Name)) || ("Object" != typeof(tSelect).Name))
            {
                try
                {
                    string str = e.CurrentRow.Cells["BM"].Value.ToString();
                    if ("0" == e.CurrentRow.Cells["WJ"].Value.ToString())
                    {
                        if (((BaseForm) Activator.CreateInstance(typeof(tFenLei), new object[] { this, str, true })).ShowDialog() == DialogResult.OK)
                        {
                            this.bllManager.CurrentPage = 1;
                            this.dataSet = this.bllManager.SelectNodeDisplay(this.selectbm, this.bllManager.Pagesize, this.bllManager.CurrentPage);
                            this.aisinoDataGrid1.DataSource = this.dataSet;
                            this.treeViewBM1.TreeLoad();
                            this.treeViewBM1.SelectNodeByText(this.selectbm);
                        }
                    }
                    else if (((BaseForm) Activator.CreateInstance(typeof(tEdit), new object[] { str, true })).ShowDialog() == DialogResult.OK)
                    {
                        this.dataSet = this.bllManager.QueryData(this.bllManager.Pagesize, this.bllManager.CurrentPage);
                        this.aisinoDataGrid1.DataSource = this.dataSet;
                    }
                }
                catch (Exception exception)
                {
                    this.log.Error(exception.ToString());
                    ExceptionHandler.HandleError(exception);
                }
            }
        }

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.bllManager.CurrentPage = e.PageNO;
            this.bllManager.Pagesize = e.PageSize;
            this.dataSet = this.bllManager.QueryData(e.PageSize, e.PageNO);
            this.aisinoDataGrid1.DataSource = this.dataSet;
        }

        private void BMBase_Load(object sender, EventArgs e)
        {
            this.treeViewBM1.TreeLoad();
            this.treeViewBM1_onTreeNodeClick(this.treeViewBM1, new TreeViewEventArgs(this.treeViewBM1.TopNode));
        }

        protected virtual void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = string.Empty;
                if (this.aisinoDataGrid1.SelectedRows.Count != 1)
                {
                    if (this.treeViewBM1.SelectedNode == null)
                    {
                        MessageManager.ShowMsgBox("INP-235104");
                        return;
                    }
                    name = this.treeViewBM1.SelectedNode.Name;
                }
                else if ("0" == this.aisinoDataGrid1.SelectedRows[0].Cells["WJ"].Value.ToString())
                {
                    name = this.aisinoDataGrid1.SelectedRows[0].Cells["BM"].Value.ToString();
                }
                else
                {
                    name = this.aisinoDataGrid1.SelectedRows[0].Cells["SJBM"].Value.ToString();
                }
                if (((BaseForm) Activator.CreateInstance(typeof(tEdit), new object[] { name, this })).ShowDialog() == DialogResult.OK)
                {
                    this.dataSet = this.bllManager.QueryData(this.bllManager.Pagesize, this.bllManager.CurrentPage);
                    this.aisinoDataGrid1.DataSource = this.dataSet;
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        protected virtual void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoDataGrid1.SelectedRows.Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-235106");
                    return;
                }
                if (MessageManager.ShowMsgBox("INP-235107") != DialogResult.OK)
                {
                    return;
                }
                string str2 = "";
                string fenLeiCodeBM = this.aisinoDataGrid1.SelectedRows[0].Cells["BM"].Value.ToString();
                string str4 = this.aisinoDataGrid1.SelectedRows[0].Cells["WJ"].Value.ToString();
                if (str4 != null)
                {
                    if (!(str4 == "0"))
                    {
                        if (str4 == "1")
                        {
                            goto Label_00F4;
                        }
                    }
                    else
                    {
                        string str = this.bllManager.DeleteDataFenLei(fenLeiCodeBM);
                        if (str == "1")
                        {
                            str2 = "INP-235102";
                        }
                        else if (str == "e")
                        {
                            str2 = "INP-235101";
                        }
                        else
                        {
                            str2 = "INP-235103";
                        }
                    }
                }
                goto Label_011C;
            Label_00F4:
                if (this.bllManager.DeleteData(fenLeiCodeBM) == "1")
                {
                    str2 = "INP-235102";
                }
                else
                {
                    str2 = "INP-235103";
                }
            Label_011C:
                this.aisinoDataGrid1.DataSource = this.bllManager.QueryData(this.bllManager.Pagesize, this.bllManager.CurrentPage);
                this.treeViewBM1.TreeLoad();
                MessageManager.ShowMsgBox(str2);
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
                    BmType = this.GetBMType()
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
                        ProgressHinter instance = ProgressHinter.GetInstance();
                        try
                        {
                            instance.SetMsg("正在导出" + this.treeViewBM1.RootNodeString + "...");
                            instance.StartCycle();
                            str = this.bllManager.ExportData(code.FilePath, code.Separator, null);
                        }
                        catch
                        {
                            throw;
                        }
                        finally
                        {
                            instance.CloseCycle();
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
                    BmType = this.GetBMType()
                };
                if (code.ShowDialog() == DialogResult.OK)
                {
                    code.Visible = false;
                    ImportReport report = new ImportReport(this.ImportMethod(code.FilePath));
                    if (this.GetBMType() != BMType.BM_SPFL)
                    {
                        report.ShowDialog();
                    }
                    ProgressHinter instance = ProgressHinter.GetInstance();
                    instance.SetMsg("正在调整" + this.treeViewBM1.RootNodeString + "...");
                    instance.StartCycle();
                    try
                    {
                        ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.AdjustTopBM", new object[] { this.GetBMType().ToString() });
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        instance.CloseCycle();
                    }
                    this.aisinoDataGrid1.DataSource = this.bllManager.QueryData(this.bllManager.Pagesize, this.bllManager.CurrentPage);
                    this.treeViewBM1.TreeLoad();
                    this.treeViewBM1.SelectNodeByText(this.treeViewBM1.RootNodeString);
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
                    string str = this.aisinoDataGrid1.SelectedRows[0].Cells["BM"].Value.ToString();
                    if ("0" == this.aisinoDataGrid1.SelectedRows[0].Cells["WJ"].Value.ToString())
                    {
                        if (((BaseForm) Activator.CreateInstance(typeof(tFenLei), new object[] { this, str, true })).ShowDialog() == DialogResult.OK)
                        {
                            this.bllManager.CurrentPage = 1;
                            this.dataSet = this.bllManager.SelectNodeDisplay(this.selectbm, this.bllManager.Pagesize, this.bllManager.CurrentPage);
                            this.aisinoDataGrid1.DataSource = this.dataSet;
                            this.treeViewBM1.TreeLoad();
                            this.treeViewBM1.SelectNodeByText(this.selectbm);
                        }
                    }
                    else if (((BaseForm) Activator.CreateInstance(typeof(tEdit), new object[] { str, true })).ShowDialog() == DialogResult.OK)
                    {
                        this.dataSet = this.bllManager.QueryData(this.bllManager.Pagesize, this.bllManager.CurrentPage);
                        this.aisinoDataGrid1.DataSource = this.dataSet;
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
            this.bllManager.CurrentPage = 1;
            this.KeyWord = this.textBoxWaitKey.Text.Trim();
            this.dataSet = this.bllManager.QueryByKey(this.KeyWord, this.bllManager.Pagesize, this.bllManager.CurrentPage);
            this.aisinoDataGrid1.DataSource = this.dataSet;
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
            switch (typeof(tEdit).FullName)
            {
                case "Aisino.Fwkp.Bmgl.Forms.BMKH_Edit":
                    return BMType.BM_KH;

                case "Aisino.Fwkp.Bmgl.Forms.BMSP_Edit":
                    return BMType.BM_SP;

                case "Aisino.Fwkp.Bmgl.Forms.BMSFHR_Edit":
                    return BMType.BM_SFHR;

                case "Aisino.Fwkp.Bmgl.Forms.BMFYXM_Edit":
                    return BMType.BM_FYXM;

                case "Aisino.Fwkp.Bmgl.Forms.BMGHDW_Edit":
                    return BMType.BM_GHDW;

                case "Aisino.Fwkp.Bmgl.Forms.BMCL_Edit":
                    return BMType.BM_CL;

                case "Aisino.Fwkp.Bmgl.Forms.BMXHDW_Edit":
                    return BMType.BM_XHDW;
            }
            return BMType.BM_SPFL;
        }

        protected virtual ImportResult ImportMethod(string path)
        {
            ImportResult result;
            ProgressHinter instance = ProgressHinter.GetInstance();
            instance.SetMsg("正在导入" + this.treeViewBM1.RootNodeString + "...");
            instance.StartCycle();
            try
            {
                if (path.EndsWith(".txt"))
                {
                    return this.bllManager.ImportData(path);
                }
                result = new ImportResult();
            }
            catch
            {
                throw;
            }
            finally
            {
                instance.CloseCycle();
            }
            return result;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.aisinoDataGrid1.GoToPageEvent += new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent);
            this.aisinoDataGrid1.DataGridRowDbClickEvent += new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowDbClickEvent);
            this.toolAdd.Click += new EventHandler(this.btnAdd_Click);
            this.toolModify.Click += new EventHandler(this.btnModify_Click);
            this.toolDel.Click += new EventHandler(this.btnDel_Click);
            this.textBoxWaitKey.TextChanged += new EventHandler(this.textBoxWaitKey_TextChanged);
            this.toolExport.Click += new EventHandler(this.btnExport_Click);
            this.toolImport.Click += new EventHandler(this.btnImport_Click);
            this.toolExit.Click += new EventHandler(this.btnExit_Click);
            this.treeViewBM1.onClickAddFenLei += new TreeViewBM.OnClickAddFenLei(this.treeViewBM1_onClickAddFenLei);
            this.treeViewBM1.onClickAdd += new TreeViewBM.OnClickAdd(this.treeViewBM1_onClickAdd);
            this.treeViewBM1.onClickZengWei += new TreeViewBM.OnClickZengWei(this.treeViewBM1_onClickZengWei);
            this.treeViewBM1.onTreeNodeClick += new TreeViewBM.OnTreeNodeClick(this.treeViewBM1_onTreeNodeClick);
            this.treeViewBM1.onClickModify += new TreeViewBM.OnClickModify(this.treeViewBM1_onClickModify);
            this.treeViewBM1.onClickDelete += new TreeViewBM.OnClickDelete(this.treeViewBM1_onClickDelete);
            this.treeViewBM1.onClickJianWei += new TreeViewBM.OnClickJianWei(this.treeViewBM1_onClickJianWei);
            this.treeViewBM1.Focus();
            this.aisinoDataGrid1.ShowAllChkVisible = false;
            this.treeViewBM1.ChildText = "增加***编码";
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.textBoxWaitKey.Paint += new PaintEventHandler(this.textBoxWaitKey_Paint);
            this.aisinoDataGrid1.MultiSelect = false;
            this.aisinoDataGrid1.AborCellPainting = true;
            this.aisinoDataGrid1.ReadOnly = true;
            this.aisinoDataGrid1.DataGrid.AllowUserToDeleteRows = false;
            this.treeViewBM1.getListNodes += new TreeViewBM.GetListNodes(this.treeViewBM1_getListNodes);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.tool_GeShi.Enabled = false;
            this.tool_ChaZhao.Enabled = false;
            this.tool_TongJi.Enabled = false;
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(BMBase<tEdit, tFenLei, tSelect>));
            this.aisinoDataGrid1 = new AisinoDataGrid();
            this.splitContainer1 = new AisinoSPL();
            this.treeViewBM1 = new TreeViewBM();
            this.toolStrip1 = new ToolStrip();
            this.toolExit = new ToolStripButton();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.toolExport = new ToolStripButton();
            this.toolImport = new ToolStripButton();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.tool_GeShi = new ToolStripButton();
            this.tool_TongJi = new ToolStripButton();
            this.tool_ChaZhao = new ToolStripButton();
            this.toolPrint = new ToolStripButton();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.toolDel = new ToolStripButton();
            this.toolModify = new ToolStripButton();
            this.toolAdd = new ToolStripButton();
            this.toolSearchLbl = new ToolStripLabel();
            this.textBoxWaitKey = new ToolStripTextBox();
            this.panel1 = new AisinoPNL();
            (this.splitContainer1 as ISupportInitialize).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.aisinoDataGrid1.AborCellPainting = false;
            this.aisinoDataGrid1.AutoSize = true;
            this.aisinoDataGrid1.BackColor = Color.White;
            this.aisinoDataGrid1.CurrentCell = null;
            this.aisinoDataGrid1.DataSource = null;
            this.aisinoDataGrid1.Dock = DockStyle.Fill;
            this.aisinoDataGrid1.FirstDisplayedScrollingRowIndex = -1;
            this.aisinoDataGrid1.IsShowAll = false;
            this.aisinoDataGrid1.Location = new Point(0, 0);
            this.aisinoDataGrid1.Name = "aisinoDataGrid1";
            this.aisinoDataGrid1.ReadOnly = false;
            this.aisinoDataGrid1.RightToLeft = RightToLeft.No;
            this.aisinoDataGrid1.ShowAllChkVisible = true;
            this.aisinoDataGrid1.Size = new Size(0x311, 0xe5);
            this.aisinoDataGrid1.TabIndex = 0;
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.treeViewBM1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.aisinoDataGrid1);
            this.splitContainer1.Size = new Size(0x3dc, 0xe5);
            this.splitContainer1.SplitterDistance = 0xc7;
            this.splitContainer1.TabIndex = 0;
            this.treeViewBM1.ChildText = "";
            this.treeViewBM1.Dock = DockStyle.Fill;
            this.treeViewBM1.Location = new Point(0, 0);
            this.treeViewBM1.Name = "treeViewBM1";
            this.treeViewBM1.ReadOnly = false;
            this.treeViewBM1.SelectedNode = null;
            this.treeViewBM1.Size = new Size(0xc7, 0xe5);
            this.treeViewBM1.TabIndex = 0;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolExit, this.toolStripSeparator2, this.toolExport, this.toolImport, this.toolStripSeparator3, this.tool_GeShi, this.tool_TongJi, this.tool_ChaZhao, this.toolPrint, this.toolStripSeparator1, this.toolDel, this.toolModify, this.toolAdd, this.toolSearchLbl, this.textBoxWaitKey });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x311, 0x19);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolExit.Image = Resources.退出;
            this.toolExit.ImageTransparentColor = Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new Size(0x34, 0x16);
            this.toolExit.Text = "退出";
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 0x19);
            this.toolExport.Image = Resources.导出;
            this.toolExport.ImageTransparentColor = Color.Magenta;
            this.toolExport.Name = "toolExport";
            this.toolExport.Size = new Size(0x34, 0x16);
            this.toolExport.Text = "导出";
            this.toolImport.Image = Resources.导入;
            this.toolImport.ImageTransparentColor = Color.Magenta;
            this.toolImport.Name = "toolImport";
            this.toolImport.Size = new Size(0x34, 0x16);
            this.toolImport.Text = "导入";
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(6, 0x19);
            this.tool_GeShi.Image = Resources.格式;
            this.tool_GeShi.ImageTransparentColor = Color.Magenta;
            this.tool_GeShi.Name = "tool_GeShi";
            this.tool_GeShi.Size = new Size(0x34, 0x16);
            this.tool_GeShi.Text = "格式";
            this.tool_GeShi.Click += new EventHandler(this.tool_GeShi_Click);
            this.tool_TongJi.Image = Resources.统计;
            this.tool_TongJi.ImageTransparentColor = Color.Magenta;
            this.tool_TongJi.Name = "tool_TongJi";
            this.tool_TongJi.Size = new Size(0x34, 0x16);
            this.tool_TongJi.Text = "统计";
            this.tool_ChaZhao.Image = Resources.搜索;
            this.tool_ChaZhao.ImageTransparentColor = Color.Magenta;
            this.tool_ChaZhao.Name = "tool_ChaZhao";
            this.tool_ChaZhao.Size = new Size(0x34, 0x16);
            this.tool_ChaZhao.Text = "查找";
            this.tool_ChaZhao.Click += new EventHandler(this.tool_ChaZhao_Click);
            this.toolPrint.Image = Resources.打印;
            this.toolPrint.ImageTransparentColor = Color.Magenta;
            this.toolPrint.Name = "toolPrint";
            this.toolPrint.Size = new Size(0x34, 0x16);
            this.toolPrint.Text = "打印";
            this.toolPrint.Click += new EventHandler(this.toolPrint_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            this.toolDel.Image = Resources.删除;
            this.toolDel.ImageTransparentColor = Color.Magenta;
            this.toolDel.Name = "toolDel";
            this.toolDel.Size = new Size(0x34, 0x16);
            this.toolDel.Text = "删除";
            this.toolModify.Image = Resources.修改;
            this.toolModify.ImageTransparentColor = Color.Magenta;
            this.toolModify.Name = "toolModify";
            this.toolModify.Size = new Size(0x34, 0x16);
            this.toolModify.Text = "修改";
            this.toolAdd.Image = Resources.增加;
            this.toolAdd.ImageTransparentColor = Color.Magenta;
            this.toolAdd.Name = "toolAdd";
            this.toolAdd.Size = new Size(0x34, 0x16);
            this.toolAdd.Text = "增加";
            this.toolSearchLbl.Alignment = ToolStripItemAlignment.Right;
            this.toolSearchLbl.Image = Resources.搜索;
            this.toolSearchLbl.Name = "toolSearchLbl";
            this.toolSearchLbl.Size = new Size(0x30, 0x16);
            this.toolSearchLbl.Text = "检索";
            this.textBoxWaitKey.Alignment = ToolStripItemAlignment.Right;
            this.textBoxWaitKey.Name = "textBoxWaitKey";
            this.textBoxWaitKey.Size = new Size(150, 0x19);
            this.textBoxWaitKey.ToolTipText = "输入关键字(******)";
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x3dc, 0xe5);
            this.panel1.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3dc, 0xe5);
            base.Controls.Add(this.panel1);
            base.Name = "BMBase";
            base.TabText = "**编码设置";
            this.Text = "**编码设置";
            base.Load += new EventHandler(this.BMBase_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            (this.splitContainer1 as ISupportInitialize).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void RefreshGrid()
        {
            this.dataSet = this.bllManager.QueryData(this.bllManager.Pagesize, this.bllManager.CurrentPage);
            this.aisinoDataGrid1.DataSource = this.dataSet;
        }

        public void RefreshGridTree()
        {
            this.dataSet = this.bllManager.QueryData(this.bllManager.Pagesize, this.bllManager.CurrentPage);
            this.aisinoDataGrid1.DataSource = this.dataSet;
            this.treeViewBM1.SubTreeLoad();
        }

        private void textBoxWaitKey_Paint(object sender, PaintEventArgs e)
        {
            CommonFunc.DrawBorder(sender, e.Graphics, SystemColor.GRID_ALTROW_BACKCOLOR, Color.FromArgb(0, 0xbb, 0xff), this.textBoxWaitKey.Width, this.textBoxWaitKey.Height);
            this.textBoxWaitKey.Alignment = ToolStripItemAlignment.Right;
        }

        private void textBoxWaitKey_TextChanged(object sender, EventArgs e)
        {
            this.bllManager.CurrentPage = 1;
            this.KeyWord = (sender as ToolStripTextBox).Text.Trim();
            this.dataSet = this.bllManager.QueryByKey(this.KeyWord, this.bllManager.Pagesize, this.bllManager.CurrentPage);
            this.aisinoDataGrid1.DataSource = this.dataSet;
        }

        private void tool_ChaZhao_Click(object sender, EventArgs e)
        {
        }

        private void tool_GeShi_Click(object sender, EventArgs e)
        {
            try
            {
                this.aisinoDataGrid1.SetColumnsStyle("c:aaa.xml", this);
            }
            catch (Exception exception)
            {
                this.log.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        protected virtual void toolPrint_Click(object sender, EventArgs e)
        {
            try
            {
                AisinoDataGrid grid = this.aisinoDataGrid1;
                if ((grid == null) || (grid.Rows.Count <= 0))
                {
                    MessageManager.ShowMsgBox("没有可打印的列表内容");
                }
                else
                {
                    grid.Print(this.treeViewBM1.RootNodeString, this, null, null, true);
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private List<TreeNodeTemp> treeViewBM1_getListNodes(string ParentBM)
        {
            return this.bllManager.listNodes(ParentBM);
        }

        protected virtual void treeViewBM1_onClickAdd(object sender, TreeSelectEventArgs e)
        {
            try
            {
                if (((BaseForm) Activator.CreateInstance(typeof(tEdit), new object[] { e.BmString, this })).ShowDialog() == DialogResult.OK)
                {
                    this.dataSet = this.bllManager.QueryData(this.bllManager.Pagesize, this.bllManager.CurrentPage);
                    this.aisinoDataGrid1.DataSource = this.dataSet;
                    this.treeViewBM1.SubTreeLoad();
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        protected virtual void treeViewBM1_onClickAddFenLei(object sender, TreeSelectEventArgs e)
        {
            try
            {
                if (((BaseForm) Activator.CreateInstance(typeof(tFenLei), new object[] { e.BmString, this })).ShowDialog() == DialogResult.OK)
                {
                    this.bllManager.CurrentPage = 1;
                    this.dataSet = this.bllManager.SelectNodeDisplay(this.selectbm, this.bllManager.Pagesize, this.bllManager.CurrentPage);
                    this.aisinoDataGrid1.DataSource = this.dataSet;
                    this.treeViewBM1.TreeLoad();
                    this.treeViewBM1.SelectNodeByText(this.selectbm);
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        protected virtual void treeViewBM1_onClickDelete(object sender, EventArgs e)
        {
            try
            {
                string str;
                bool flag;
                if (this.treeViewBM1.SelectedNode.Name.Length > 0)
                {
                    str = "INP-235115";
                    flag = false;
                }
                else
                {
                    str = "INP-235116";
                    flag = true;
                }
                if (MessageManager.ShowMsgBox(str) == DialogResult.OK)
                {
                    string str2 = this.bllManager.deleteFenLei(this.treeViewBM1.SelectedNode.Name);
                    if (str2 == "0")
                    {
                        MessageManager.ShowMsgBox("INP-235117");
                    }
                    else
                    {
                        if (!flag)
                        {
                            this.treeViewBM1.SelectedNode.Remove();
                        }
                        else
                        {
                            this.treeViewBM1.SelectedNode.Nodes.Clear();
                        }
                        MessageManager.ShowMsgBox("INP-235118", "", new string[] { str2 });
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private bool treeViewBM1_onClickJianWei(object sender, EventArgs e)
        {
            return this.ZengWeiJianWei(false);
        }

        protected virtual void treeViewBM1_onClickModify(object sender, TreeSelectEventArgs e)
        {
            try
            {
                if (((BaseForm) Activator.CreateInstance(typeof(tFenLei), new object[] { this, e.BmString, true })).ShowDialog() == DialogResult.OK)
                {
                    this.bllManager.CurrentPage = 1;
                    this.dataSet = this.bllManager.SelectNodeDisplay(this.selectbm, this.bllManager.Pagesize, this.bllManager.CurrentPage);
                    this.aisinoDataGrid1.DataSource = this.dataSet;
                    this.treeViewBM1.TreeLoad();
                    this.treeViewBM1.SelectNodeByText(this.selectbm);
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private bool treeViewBM1_onClickZengWei(object sender, EventArgs e)
        {
            return this.ZengWeiJianWei(true);
        }

        protected virtual bool treeViewBM1_onTreeNodeClick(object sender, TreeViewEventArgs e)
        {
            bool flag = false;
            try
            {
                if ((e.Node != this.treeViewBM1.TopNode) && (e.Node != null))
                {
                    this.selectbm = e.Node.Name;
                }
                else
                {
                    this.selectbm = "";
                }
                this.bllManager.CurrentPage = 1;
                this.dataSet = this.bllManager.SelectNodeDisplay(this.selectbm, this.bllManager.Pagesize, this.bllManager.CurrentPage);
                this.aisinoDataGrid1.DataSource = this.dataSet;
                if (this.aisinoDataGrid1.DataSource.Data.Rows.Count > 1)
                {
                    flag = true;
                }
                return flag;
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return flag;
            }
        }

        private bool ZengWeiJianWei(bool isZengWei)
        {
            bool flag = false;
            try
            {
                string str = "";
                ProgressHinter instance = ProgressHinter.GetInstance();
                try
                {
                    instance.SetMsg("正在修改" + this.treeViewBM1.RootNodeString + "...");
                    instance.StartCycle();
                    this.selectbm = this.treeViewBM1.SelectedNode.Name;
                    str = this.bllManager.ExecZengJianWei(this.selectbm, isZengWei);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    instance.CloseCycle();
                }
                if (str == "0")
                {
                    this.bllManager.CurrentPage = 1;
                    this.dataSet = this.bllManager.SelectNodeDisplay(this.selectbm, this.bllManager.Pagesize, this.bllManager.CurrentPage);
                    this.aisinoDataGrid1.DataSource = this.dataSet;
                    flag = true;
                }
                else if (str == "ezw")
                {
                    MessageManager.ShowMsgBox("INP-235111");
                }
                else if (str == "ejw")
                {
                    MessageManager.ShowMsgBox("INP-235112");
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-235114");
                }
                return flag;
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return flag;
            }
        }

        public string Selectbm
        {
            get
            {
                return this.selectbm;
            }
            set
            {
                this.selectbm = value;
            }
        }
    }
}

