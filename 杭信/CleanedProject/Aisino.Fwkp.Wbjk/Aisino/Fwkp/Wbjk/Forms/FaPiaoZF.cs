namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class FaPiaoZF : DockForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoBTN btnClear;
        private AisinoBTN btnSelectAll;
        private AisinoBTN btnZF;
        private IContainer components = null;
        private FPZFbll fpzfBL = new FPZFbll();
        private bool IsAdmin = false;
        private ILog log = LogUtil.GetLogger<FaPiaoZF>();
        private static ILog loger = LogUtil.GetLogger<FaPiaoZF>();
        private string mc = "";
        private StatusStrip statusStrip1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripAll;
        private ToolStripButton toolStripClean;
        private ToolStripButton toolStripExit;
        private ToolStripButton toolStripWaste;
        private XmlComponentLoader xmlComponentLoader1;

        public FaPiaoZF()
        {
            this.Initialize();
            this.IsAdmin = UserInfo.get_IsAdmin();
            this.mc = (UserInfo.get_Yhmc() == null) ? "" : UserInfo.get_Yhmc().Trim();
        }

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                this.aisinoDataGrid1.set_DataSource(this.fpzfBL.GetZuoFeiXSDJ(e.get_PageSize(), e.get_PageNO(), this.IsAdmin, this.mc));
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FaPiaoZF_Load(object sender, EventArgs e)
        {
            try
            {
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票状态");
                item.Add("Property", "ZFBZ");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "单据状态");
                item.Add("Property", "DJZT");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开票状态");
                item.Add("Property", "KPZT");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票种类");
                item.Add("Property", "FPZL");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "单据编号");
                item.Add("Property", "BH");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "True");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票类别");
                item.Add("Property", "FPDM");
                item.Add("Type", "Text");
                item.Add("Width", "150");
                item.Add("Align", "MiddleCenter");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票号码");
                item.Add("Property", "FPHM");
                item.Add("Type", "Text");
                item.Add("Width", "150");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                list.Add(item);
                this.aisinoDataGrid1.set_ColumeHead(list);
                DataGridViewColumn column = this.aisinoDataGrid1.get_Columns()["FPHM"];
                if (null != column)
                {
                    column.DefaultCellStyle.Format = new string('0', 8);
                }
                AisinoDataSet set = this.fpzfBL.GetZuoFeiXSDJ(this.fpzfBL.Pagesize, this.fpzfBL.CurrentPage, this.IsAdmin, this.mc);
                this.aisinoDataGrid1.set_DataSource(set);
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.btnZF = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnZF");
            this.btnSelectAll = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnSelectAll");
            this.btnClear = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnClear");
            this.statusStrip1 = this.xmlComponentLoader1.GetControlByName<StatusStrip>("statusStrip1");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripExit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripExit");
            this.toolStripWaste = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripWaste");
            this.toolStripAll = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripAll");
            this.toolStripClean = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripClean");
            this.aisinoDataGrid1.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent));
            this.toolStripExit.Click += new EventHandler(this.toolStripExit_Click);
            this.toolStripWaste.Click += new EventHandler(this.toolStripWaste_Click);
            this.toolStripAll.Click += new EventHandler(this.toolStripAll_Click);
            this.toolStripClean.Click += new EventHandler(this.toolStripClean_Click);
            this.aisinoDataGrid1.set_ReadOnly(true);
            this.aisinoDataGrid1.get_DataGrid().AllowUserToDeleteRows = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FaPiaoZF));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "作废已生成发票";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.FaPiaoZF\Aisino.Fwkp.Wbjk.FaPiaoZF.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "FaPiaoZF";
            base.set_TabText("作废已生成发票");
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "作废已生成发票";
            base.Load += new EventHandler(this.FaPiaoZF_Load);
            base.ResumeLayout(false);
        }

        private void toolStripAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.aisinoDataGrid1.get_Rows().Count; i++)
                {
                    this.aisinoDataGrid1.get_Rows()[i].Selected = true;
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void toolStripClean_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.aisinoDataGrid1.get_Rows().Count; i++)
                {
                    this.aisinoDataGrid1.get_Rows()[i].Selected = false;
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void toolStripExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolStripWaste_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoDataGrid1.get_SelectedRows().Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-274201");
                }
                else
                {
                    DataTable asds = new DataTable();
                    asds.Columns.Add("ZFBZ", typeof(string));
                    asds.Columns.Add("FPZL", typeof(string));
                    asds.Columns.Add("FPDM", typeof(string));
                    asds.Columns.Add("FPHM", typeof(string));
                    asds.Columns.Add("BH", typeof(string));
                    int num = 0;
                    int num2 = 0;
                    int num3 = 0;
                    int count = this.aisinoDataGrid1.get_SelectedRows().Count;
                    loger.Debug("选中条数:" + count);
                    string str = "";
                    for (int i = 0; i < count; i++)
                    {
                        string str2 = "c";
                        if (((this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPZL"].Value.ToString() == "普通发票") || (this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPZL"].Value.ToString() == "农产品销售发票")) || (this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPZL"].Value.ToString() == "收购发票"))
                        {
                            str2 = "c";
                        }
                        else if (this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPZL"].Value.ToString() == "专用发票")
                        {
                            str2 = "s";
                        }
                        else if (this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPZL"].Value.ToString() == "货物运输业增值税专用发票")
                        {
                            str2 = "f";
                        }
                        else if (this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPZL"].Value.ToString() == "机动车销售统一发票")
                        {
                            str2 = "j";
                        }
                        string str3 = this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPDM"].Value.ToString();
                        string str4 = this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPHM"].Value.ToString();
                        string str5 = this.aisinoDataGrid1.get_SelectedRows()[i].Cells["BH"].Value.ToString();
                        object[] objArray = new object[] { str2, str3, str4, 1 };
                        if ((bool) ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPYiKaiZuoFeiWenBenJieKouShareMethods", objArray)[0])
                        {
                            num++;
                            str = "成功";
                        }
                        else
                        {
                            num2++;
                            str = "失败";
                        }
                        asds.Rows.Add(new object[] { str, this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPZL"].Value, this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPDM"].Value, this.aisinoDataGrid1.get_SelectedRows()[i].Cells["FPHM"].Value, this.aisinoDataGrid1.get_SelectedRows()[i].Cells["BH"].Value });
                    }
                    this.aisinoDataGrid1.set_DataSource(this.fpzfBL.GetZuoFeiXSDJ(this.fpzfBL.Pagesize, this.fpzfBL.CurrentPage, this.IsAdmin, this.mc));
                    num3 = (count - num) - num2;
                    if (MessageManager.ShowMsgBox("INP-274202", "确认对话框", new string[] { count.ToString(), num.ToString(), num2.ToString(), num3.ToString() }) == DialogResult.OK)
                    {
                        new FaPiaoZFresult(asds).ShowDialog();
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
    }
}

