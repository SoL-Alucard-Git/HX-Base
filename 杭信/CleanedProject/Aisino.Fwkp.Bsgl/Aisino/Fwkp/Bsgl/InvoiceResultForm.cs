namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.PrintGrid;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class InvoiceResultForm : BaseForm
    {
        private IContainer components;
        private CustomStyleDataGrid customStyleDataGridDetail = new CustomStyleDataGrid();
        private CommFun m_commFun = new CommFun();
        private object[] m_objHead;
        private QueryPrintBLL m_queryPrintBLL = new QueryPrintBLL();
        private QueryPrintEntity m_queryPrintEntity = new QueryPrintEntity();
        private ToolStripButton toolStripButtonExit = new ToolStripButton();
        private ToolStripButton toolStripButtonForm = new ToolStripButton();
        private ToolStripButton toolStripButtonPrint = new ToolStripButton();
        private ToolStripButton toolStripButtonQuery = new ToolStripButton();
        private ToolStripButton toolStripButtonRefresh = new ToolStripButton();
        private ToolStripButton toolStripButtonSum = new ToolStripButton();
        private ToolStrip toolStripMenu;
        private XmlComponentLoader xmlComponentLoader1;

        public InvoiceResultForm(QueryPrintEntity _queryPrintEntity)
        {
            this.Initial();
            ControlStyleUtil.SetToolStripStyle(this.toolStripMenu);
            this.toolStripButtonExit.Margin = new Padding(20, 1, 0, 2);
            this.m_queryPrintEntity = _queryPrintEntity;
            this.customStyleDataGridDetail.Dock = DockStyle.Fill;
            this.customStyleDataGridDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.customStyleDataGridDetail.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
            this.m_queryPrintBLL.MakeTable(ref this.customStyleDataGridDetail, _queryPrintEntity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Initial()
        {
            this.InitializeComponent();
            this.customStyleDataGridDetail = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGridDetail");
            base.Load += new EventHandler(this.InvoiceResultForm_Load);
            this.toolStripMenu = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStripMenu");
            this.toolStripButtonExit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonExit");
            this.toolStripButtonExit.Click += new EventHandler(this.toolStripButtonExit_Click);
            this.toolStripButtonQuery = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonQuery");
            this.toolStripButtonQuery.Click += new EventHandler(this.toolStripButtonQuery_Click);
            this.toolStripButtonQuery.Enabled = false;
            this.toolStripButtonPrint = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonPrint");
            this.toolStripButtonPrint.Click += new EventHandler(this.toolStripButtonPrint_Click);
            this.toolStripButtonSum = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonSum");
            this.toolStripButtonSum.Click += new EventHandler(this.toolStripButtonSum_Click);
            this.toolStripButtonSum.Enabled = false;
            this.toolStripButtonForm = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonForm");
            this.toolStripButtonForm.Click += new EventHandler(this.toolStripButtonForm_Click);
            this.toolStripButtonRefresh = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonRefresh");
            this.toolStripButtonRefresh.Visible = false;
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.FPQueryResult\Aisino.Fwkp.Bsgl.FPQueryResult.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "InvoiceResultForm";
            this.Text = "发票清单";
            base.ResumeLayout(false);
        }

        private void InvoiceResultForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.m_queryPrintEntity.m_bPrint)
                {
                    base.Show();
                    this.PrintTable(this.m_queryPrintEntity.m_bShowDialog);
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        public bool PrintTable(bool _bShow)
        {
            bool flag = false;
            try
            {
                this.m_objHead = this.m_commFun.GetTaxCardInfo();
                if (this.m_objHead.Length != 1)
                {
                    return false;
                }
                Dictionary<string, object> dictionary = this.m_objHead[0] as Dictionary<string, object>;
                List<PrinterItems> list = new List<PrinterItems>();
                List<PrinterItems> list2 = new List<PrinterItems> {
                    new PrinterItems("制表日期：" + base.TaxCardInstance.get_TaxClock().ToShortDateString(), HorizontalAlignment.Left),
                    new PrinterItems(this.m_queryPrintEntity.m_strSubItem, HorizontalAlignment.Left),
                    new PrinterItems(this.m_queryPrintEntity.m_strItemDetail, HorizontalAlignment.Left),
                    new PrinterItems("纳税人登记号：" + dictionary["QYBH"].ToString(), HorizontalAlignment.Left),
                    new PrinterItems("企业名称：" + base.TaxCardInstance.get_Corporation(), HorizontalAlignment.Left),
                    new PrinterItems("地址电话：" + dictionary["YYDZ"].ToString() + "  " + dictionary["DHHM"].ToString(), HorizontalAlignment.Left),
                    new PrinterItems("金额单位：元", HorizontalAlignment.Left),
                    new PrinterItems("", HorizontalAlignment.Left),
                    new PrinterItems("填表人：     抽样员：      录入员：      复核员：      审核员：      ", HorizontalAlignment.Left)
                };
                flag = this.m_queryPrintBLL.PrintTable(ref this.customStyleDataGridDetail, this.m_queryPrintEntity.m_strTitle, list, list2, _bShow);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                return false;
            }
            return flag;
        }

        public bool PrintTableSerial(bool _bShow, bool _isSerialPrint, string showText)
        {
            bool flag = false;
            try
            {
                this.m_objHead = this.m_commFun.GetTaxCardInfo();
                if (this.m_objHead.Length != 1)
                {
                    return false;
                }
                Dictionary<string, object> dictionary = this.m_objHead[0] as Dictionary<string, object>;
                List<PrinterItems> list = new List<PrinterItems>();
                List<PrinterItems> list2 = new List<PrinterItems> {
                    new PrinterItems("制表日期：" + base.TaxCardInstance.get_TaxClock().ToShortDateString(), HorizontalAlignment.Left),
                    new PrinterItems(this.m_queryPrintEntity.m_strSubItem, HorizontalAlignment.Left),
                    new PrinterItems(this.m_queryPrintEntity.m_strItemDetail, HorizontalAlignment.Left),
                    new PrinterItems("纳税人登记号：" + dictionary["QYBH"].ToString(), HorizontalAlignment.Left),
                    new PrinterItems("企业名称：" + base.TaxCardInstance.get_Corporation(), HorizontalAlignment.Left),
                    new PrinterItems("地址电话：" + dictionary["YYDZ"].ToString() + "  " + dictionary["DHHM"].ToString(), HorizontalAlignment.Left),
                    new PrinterItems("金额单位：元", HorizontalAlignment.Left),
                    new PrinterItems("", HorizontalAlignment.Left),
                    new PrinterItems("填表人：     抽样员：      录入员：      复核员：      审核员：      ", HorizontalAlignment.Left)
                };
                flag = this.m_queryPrintBLL.PrintTableSerial(ref this.customStyleDataGridDetail, this.m_queryPrintEntity.m_strTitle, list, list2, _bShow, _isSerialPrint, showText);
            }
            catch (Exception exception)
            {
                if (exception.Message.Equals("用户放弃连续打印"))
                {
                    throw exception;
                }
                return false;
            }
            return flag;
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolStripButtonForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.customStyleDataGridDetail.SetColumnStyles(this.xmlComponentLoader1.get_XMLPath(), this);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-253208", new string[] { exception.Message });
            }
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.PrintTable(this.m_queryPrintEntity.m_bShowDialog);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-253208", new string[] { exception.Message });
            }
        }

        private void toolStripButtonQuery_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButtonSum_Click(object sender, EventArgs e)
        {
        }
    }
}

