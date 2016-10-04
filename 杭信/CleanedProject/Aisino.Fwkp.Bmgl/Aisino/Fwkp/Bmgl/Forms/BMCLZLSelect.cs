namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BMCLZLSelect : BaseForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoBTN btnQuery;
        private BMCLZLManager clzlManager;
        private IContainer components;
        private AisinoDataSet dataSet;
        private string HiKeyWord;
        private string KeyWord;
        private ToolStripStatusLabel lblRowNum;
        private ILog log;
        public string SelectedBM;
        private ToolStripTextBox textBoxWaitKey;
        private ToolStripLabel toolSearchLbl;
        private ToolStrip toolStrip1;
        private XmlComponentLoader xmlComponentLoader1;

        public BMCLZLSelect()
        {
            this.SelectedBM = "";
            this.clzlManager = new BMCLZLManager();
            this.log = LogUtil.GetLogger<BMCLZLSelect>();
            this.HiKeyWord = string.Empty;
            this.KeyWord = "";
            this.HiKeyWord = "";
            this.Initialize();
        }

        public BMCLZLSelect(string keyWord)
        {
            this.SelectedBM = "";
            this.clzlManager = new BMCLZLManager();
            this.log = LogUtil.GetLogger<BMCLZLSelect>();
            this.HiKeyWord = string.Empty;
            this.KeyWord = "";
            this.HiKeyWord = keyWord;
            this.Initialize();
        }

        private void aisinoDataGrid1_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                this.SelectedBM = e.CurrentRow.Cells["CLZLMC"].Value.ToString();
                base.DialogResult = DialogResult.OK;
                base.Close();
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
                    int rowIndex = 0;
                    rowIndex = this.aisinoDataGrid1.CurrentCell.RowIndex;
                    DataGridViewRow row = this.aisinoDataGrid1.Rows[rowIndex];
                    this.SelectedBM = row.Cells["CLZLMC"].Value.ToString();
                    base.DialogResult = DialogResult.OK;
                    base.Close();
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
            this.clzlManager.Pagesize = e.PageSize;
            this.dataSet = this.clzlManager.QueryTable(e.PageSize, e.PageNO);
            this.aisinoDataGrid1.DataSource = this.dataSet;
        }

        private void BMCLZLSelect_Load(object sender, EventArgs e)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "车辆种类代码");
            item.Add("Property", "CLZLDM");
            item.Add("Type", "Text");
            item.Add("Width", "120");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "车辆大类名称");
            item.Add("Property", "CLDLMC");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "车辆种类名称");
            item.Add("Property", "CLZLMC");
            item.Add("Type", "Text");
            item.Add("Width", "200");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "True");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "XSSX");
            item.Add("Property", "XSSX");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "XY标志");
            item.Add("Property", "XYBZ");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "YX标志");
            item.Add("Property", "YXBZ");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            this.aisinoDataGrid1.ColumeHead = list;
            this.aisinoDataGrid1.MultiSelect = false;
            this.aisinoDataGrid1.AborCellPainting = true;
            this.aisinoDataGrid1.ReadOnly = true;
            this.aisinoDataGrid1.DataGrid.AllowUserToDeleteRows = false;
            this.aisinoDataGrid1.DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.LoadTable();
            this.SelectRow();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.clzlManager.CurrentPage = 1;
                this.KeyWord = this.textBoxWaitKey.Text.Trim();
                this.dataSet = this.clzlManager.QueryByKey(this.KeyWord, this.clzlManager.Pagesize, this.clzlManager.CurrentPage);
                this.aisinoDataGrid1.DataSource = this.dataSet;
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
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
            this.textBoxWaitKey = this.xmlComponentLoader1.GetControlByName<ToolStripTextBox>("textBoxWaitKey");
            this.btnQuery = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnQuery");
            this.btnQuery.Visible = false;
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.textBoxWaitKey.TextChanged += new EventHandler(this.textBoxWaitKey_TextChanged);
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.aisinoDataGrid1.GoToPageEvent += new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent);
            this.aisinoDataGrid1.DataGridRowDbClickEvent += new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowDbClickEvent);
            this.aisinoDataGrid1.DataGridRowKeyDown += new KeyEventHandler(this.aisinoDataGrid1_DataGridRowKeyDown);
            this.aisinoDataGrid1.ShowAllChkVisible = false;
            this.lblRowNum = this.xmlComponentLoader1.GetControlByName<ToolStripStatusLabel>("lblRowNum");
            this.textBoxWaitKey.ToolTipText = "输入关键字(车辆种类代码,车辆大类名称,车辆种类名称)";
            this.toolSearchLbl = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolSearchLbl");
            this.toolSearchLbl.Alignment = ToolStripItemAlignment.Right;
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.textBoxWaitKey.Paint += new PaintEventHandler(this.textBoxWaitKey_Paint);
            this.textBoxWaitKey.Alignment = ToolStripItemAlignment.Right;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMCLZLSelect));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x215, 410);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "BMCLZLSelect";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.BMCLZLSelect\Aisino.Fwkp.Bmgl.Forms.BMCLZLSelect.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x216, 0x192);
            base.Controls.Add(this.xmlComponentLoader1);
            base.KeyPreview = true;
            base.Name = "BMCLZLSelect";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "车辆种类选择";
            base.Load += new EventHandler(this.BMCLZLSelect_Load);
            base.ResumeLayout(false);
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
        }

        private void LoadTable()
        {
            try
            {
                this.dataSet = this.clzlManager.QueryTable(this.clzlManager.Pagesize, this.clzlManager.CurrentPage);
                this.aisinoDataGrid1.DataSource = this.dataSet;
                this.aisinoDataGrid1.ShowPageBar = false;
                this.lblRowNum.Text = "共" + this.aisinoDataGrid1.Rows.Count + "条记录";
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void SelectRow()
        {
            if (this.aisinoDataGrid1.Rows.Count > 0)
            {
                if (this.HiKeyWord.Trim().Length > 0)
                {
                    int index = 0;
                    foreach (DataGridViewRow row in (IEnumerable) this.aisinoDataGrid1.Rows)
                    {
                        if (Convert.ToString(row.Cells["CLZLMC"].Value).Contains(this.HiKeyWord))
                        {
                            index = row.Index;
                            break;
                        }
                    }
                    this.aisinoDataGrid1.Rows[index].Selected = true;
                    this.aisinoDataGrid1.CurrentCell = this.aisinoDataGrid1.Rows[index].Cells[0];
                    this.aisinoDataGrid1.FirstDisplayedScrollingRowIndex = index;
                }
                this.aisinoDataGrid1.Focus();
            }
        }

        private void textBoxWaitKey_Paint(object sender, PaintEventArgs e)
        {
            CommonFunc.DrawBorder(sender, e.Graphics, SystemColor.GRID_ALTROW_BACKCOLOR, Color.FromArgb(0, 0xbb, 0xff), this.textBoxWaitKey.Width, this.textBoxWaitKey.Height);
        }

        private void textBoxWaitKey_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.KeyWord = (sender as ToolStripTextBox).Text.Trim();
                this.dataSet = this.clzlManager.QueryByKey(this.KeyWord, this.clzlManager.Pagesize, this.clzlManager.CurrentPage);
                this.aisinoDataGrid1.DataSource = this.dataSet;
            }
            catch (Exception exception)
            {
                this.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }
    }
}

