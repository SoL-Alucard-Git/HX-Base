namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Model;
    using Aisino.Fwkp.Wbjk.Properties;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class GenerateFPResult : BaseForm
    {
        private IContainer components = null;
        private CustomStyleDataGrid fpGrid;
        private ImageList imageList1 = new ImageList();
        private List<FPGenerateResult> ListFPGenerateResult;
        private ILog log = LogUtil.GetLogger<GenerateFPResult>();
        private AisinoPNL panel1;
        private ToolStripButton toolBtnPrint;
        private ToolStripButton toolBtnQuit;
        private ToolStripMenuItem ToolItemFP;
        private ToolStripMenuItem ToolItemSJLB;
        private ToolStripMenuItem ToolItemXHQD;
        private ToolStrip toolStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private XmlComponentLoader xmlComponentLoader1;

        public GenerateFPResult(List<FPGenerateResult> ListFPGenerateResult)
        {
            this.Initialize();
            this.ListFPGenerateResult = ListFPGenerateResult;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FpGridSingle_Click(object sender, EventArgs e)
        {
            int num = this.fpGrid.CurrentCell.RowIndex + 1;
            this.toolStripStatusLabel1.Text = num + "/" + this.ListFPGenerateResult.Count;
        }

        private void GenerateFPResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }

        private void GenerateFPResult_Load(object sender, EventArgs e)
        {
            try
            {
                if ((this.ListFPGenerateResult.Count > 0) && (this.ListFPGenerateResult[0].DJZL == "j"))
                {
                    this.fpGrid.Columns.Add("XH", "序号");
                    this.fpGrid.Columns.Add("FPZL", "发票种类");
                    this.fpGrid.Columns.Add("FPDM", "发票代码");
                    this.fpGrid.Columns.Add("FPHM", "发票号码");
                    this.fpGrid.Columns.Add("KPJG", "开具结果");
                    this.fpGrid.Columns.Add("DJH", "单据号");
                    this.fpGrid.Columns.Add("KKPX", "可开票性");
                    this.fpGrid.Columns.Add("KPZL", "开票种类");
                    this.fpGrid.Columns.Add("SXYY", "受限原因");
                    this.fpGrid.Columns.Add("SLV", "增值税税率或征收率");
                    this.fpGrid.Columns.Add("KPJE", "价税合计");
                    this.fpGrid.Columns.Add("KPSE", "增值税税额");
                }
                else
                {
                    this.fpGrid.Columns.Add("XH", "序号");
                    this.fpGrid.Columns.Add("FPZL", "发票种类");
                    this.fpGrid.Columns.Add("FPDM", "发票代码");
                    this.fpGrid.Columns.Add("FPHM", "发票号码");
                    this.fpGrid.Columns.Add("KPJG", "开具结果");
                    this.fpGrid.Columns.Add("DJH", "单据号");
                    this.fpGrid.Columns.Add("KKPX", "可开票性");
                    this.fpGrid.Columns.Add("KPZL", "开票种类");
                    this.fpGrid.Columns.Add("SXYY", "受限原因");
                    this.fpGrid.Columns.Add("SLV", "税率");
                    this.fpGrid.Columns.Add("KPJE", "开票金额");
                    this.fpGrid.Columns.Add("KPSE", "开票税额");
                }
                this.fpGrid.Columns["FPHM"].DefaultCellStyle.Format = "00000000";
                this.fpGrid.Columns["KPJE"].DefaultCellStyle.Format = "N";
                this.fpGrid.Columns["KPSE"].DefaultCellStyle.Format = "N";
                this.fpGrid.Columns["SLV"].DefaultCellStyle.Format = "0%";
                this.fpGrid.Columns["KPJE"].DefaultCellStyle.Format = "0.00";
                this.fpGrid.Columns["KPSE"].DefaultCellStyle.Format = "0.00";
                this.fpGrid.Columns["KPJE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.fpGrid.Columns["KPSE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.fpGrid.ReadOnly = true;
                this.fpGrid.AllowUserToAddRows = false;
                this.fpGrid.Rows.Clear();
                this.fpGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                int count = this.ListFPGenerateResult.Count;
                int num2 = 0;
                this.fpGrid.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    FPGenerateResult result = this.ListFPGenerateResult[i];
                    this.fpGrid.Rows[i].Cells["XH"].Value = i + 1;
                    int num4 = this.fpGrid.Rows.Count;
                    if (result.DJZL == "c")
                    {
                        if (result.TYDH == "1")
                        {
                            this.fpGrid.Rows[i].Cells["FPZL"].Value = "农产品销售发票";
                        }
                        else if (result.TYDH == "2")
                        {
                            this.fpGrid.Rows[i].Cells["FPZL"].Value = "收购发票";
                        }
                        else
                        {
                            this.fpGrid.Rows[i].Cells["FPZL"].Value = "普通发票";
                        }
                    }
                    else if (result.DJZL == "s")
                    {
                        this.fpGrid.Rows[i].Cells["FPZL"].Value = "专用发票";
                    }
                    else if (result.DJZL == "f")
                    {
                        this.fpGrid.Rows[i].Cells["FPZL"].Value = "货物运输业增值税专用发票";
                    }
                    else if (result.DJZL == "j")
                    {
                        this.fpGrid.Rows[i].Cells["FPZL"].Value = "机动车销售统一发票";
                    }
                    bool flag = false;
                    if ((result.DJZL == "c") || (result.DJZL == "s"))
                    {
                        int num5 = result.ListXSDJ_MX.Count;
                        double sLV = 0.0;
                        for (int j = 0; j < num5; j++)
                        {
                            if (j == 0)
                            {
                                sLV = result.ListXSDJ_MX[j].SLV;
                            }
                            else if (!(sLV == result.ListXSDJ_MX[j].SLV))
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    this.fpGrid.Rows[i].Cells["FPDM"].Value = result.FPDM;
                    this.fpGrid.Rows[i].Cells["FPHM"].Value = result.FPHM;
                    this.fpGrid.Rows[i].Cells["KPJG"].Value = result.KPJG;
                    this.fpGrid.Rows[i].Cells["DJH"].Value = result.BH;
                    this.fpGrid.Rows[i].Cells["KKPX"].Value = result.KKPX;
                    if ((result.KPZL == "清单开票") && (result.KPJE < 0.0))
                    {
                        this.fpGrid.Rows[i].Cells["KPZL"].Value = "清单汇总";
                    }
                    else
                    {
                        this.fpGrid.Rows[i].Cells["KPZL"].Value = result.KPZL;
                    }
                    this.fpGrid.Rows[i].Cells["SXYY"].Value = result.SXYY;
                    if (flag)
                    {
                        this.fpGrid.Rows[i].Cells["SLV"].Value = "多税率";
                    }
                    else if (result.SLV == 0.0)
                    {
                        this.fpGrid.Rows[i].Cells["SLV"].Value = "0%";
                    }
                    else if (((result.DJZL == "s") && (result.SLV == 0.05)) && result.HYSY)
                    {
                        this.fpGrid.Rows[i].Cells["SLV"].Value = "中外合作油气田";
                    }
                    else if (!((result.SLV != 0.015) || result.HYSY))
                    {
                        this.fpGrid.Rows[i].Cells["SLV"].Value = Convert.ToSingle(result.SLV).ToString("0.0%");
                    }
                    else
                    {
                        this.fpGrid.Rows[i].Cells["SLV"].Value = result.SLV;
                    }
                    if (this.fpGrid.Rows[i].Cells["FPZL"].Value.ToString().Equals("机动车销售统一发票"))
                    {
                        double round = SaleBillCtrl.GetRound((double) (result.KPJE / (1.0 + result.SLV)), 2);
                        double num9 = result.KPJE - round;
                        this.fpGrid.Rows[i].Cells["KPJE"].Value = result.KPJE;
                        this.fpGrid.Rows[i].Cells["KPSE"].Value = num9;
                    }
                    else
                    {
                        this.fpGrid.Rows[i].Cells["KPJE"].Value = result.KPJE;
                        this.fpGrid.Rows[i].Cells["KPSE"].Value = result.KPSE;
                    }
                    if (result.KPJG == "开票失败")
                    {
                        this.fpGrid.Rows[i].Cells["KPJG"].Style.ForeColor = Color.Red;
                        this.fpGrid.Rows[i].Cells["FPHM"].Value = "";
                        num2++;
                    }
                }
                if (this.ListFPGenerateResult.Count > 0)
                {
                    this.toolStripStatusLabel1.Text = 1 + "/" + this.ListFPGenerateResult.Count;
                }
                else
                {
                    this.toolStripStatusLabel1.Text = "";
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

        private void IniteFPGrid()
        {
            this.fpGrid.Columns.Add("FPZL", "发票种类");
            this.fpGrid.Columns.Add("FPDM", "发票代码");
            this.fpGrid.Columns.Add("FPHM", "发票号码");
            this.fpGrid.Columns.Add("KPJG", "开具结果");
            this.fpGrid.Columns.Add("DJH", "单据号");
            this.fpGrid.Columns.Add("KKPX", "可开票性");
            this.fpGrid.Columns.Add("KPZL", "开票种类");
            this.fpGrid.Columns.Add("SXYY", "受限原因");
            this.fpGrid.Columns.Add("SLV", "税率");
            this.fpGrid.Columns.Add("KPJE", "开票金额");
            this.fpGrid.Columns.Add("KPSE", "开票税额");
            this.fpGrid.Columns["KPJE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.fpGrid.Columns["KPSE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.fpGrid.ReadOnly = true;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.fpGrid = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("fpGrid");
            this.toolBtnQuit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolBtnQuit");
            this.toolBtnPrint = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolBtnPrint");
            this.ToolItemFP = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("ToolItemFP");
            this.ToolItemXHQD = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("ToolItemXHQD");
            this.ToolItemSJLB = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("ToolItemSJLB");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripStatusLabel1 = this.xmlComponentLoader1.GetControlByName<ToolStripStatusLabel>("toolStripStatusLabel1");
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.WindowState = FormWindowState.Maximized;
            this.toolBtnQuit.Click += new EventHandler(this.btnQuit_Click);
            this.ToolItemFP.Click += new EventHandler(this.ToolItemFP_Click);
            this.ToolItemXHQD.Click += new EventHandler(this.ToolItemXHQD_Click);
            this.ToolItemSJLB.Click += new EventHandler(this.ToolItemSJLB_Click);
            this.fpGrid.Click += new EventHandler(this.FpGridSingle_Click);
            this.fpGrid.KeyUp += new KeyEventHandler(this.FpGridSingle_Click);
            this.fpGrid.ReadOnly = true;
            this.fpGrid.AllowUserToDeleteRows = false;
            this.toolStrip1.BringToFront();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(GenerateFPResult));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x318, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.GenerateFPResult\Aisino.Fwkp.Wbjk.GenerateFPResult.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x318, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "GenerateFPResult";
            this.Text = "浏览自动生成发票日志";
            base.FormClosing += new FormClosingEventHandler(this.GenerateFPResult_FormClosing);
            base.Load += new EventHandler(this.GenerateFPResult_Load);
            base.ResumeLayout(false);
        }

        private void SetImgList()
        {
            this.imageList1.Images.Add("OK", Resources.OK);
            this.imageList1.Images.Add("No", Resources.NoAccess1);
        }

        private void ToolItemFP_Click(object sender, EventArgs e)
        {
            try
            {
                this.fpGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                if (this.fpGrid.SelectedRows.Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-273301");
                }
                else
                {
                    List<string[]> list = new List<string[]>();
                    int num2 = 0;
                    for (int i = this.fpGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        string str4 = this.fpGrid.SelectedRows[i].Cells["KPJG"].Value.ToString();
                        string str5 = this.fpGrid.SelectedRows[i].Cells["DJH"].Value.ToString();
                        if (str4 != "成功开票")
                        {
                            MessageManager.ShowMsgBox("INP-273302", "错误", new string[] { str5 });
                        }
                        else
                        {
                            string str = "c";
                            if (((this.fpGrid.SelectedRows[i].Cells["FPZL"].Value.ToString() == "普通发票") || (this.fpGrid.SelectedRows[i].Cells["FPZL"].Value.ToString() == "农产品销售发票")) || (this.fpGrid.SelectedRows[i].Cells["FPZL"].Value.ToString() == "收购发票"))
                            {
                                str = "c";
                            }
                            else if (this.fpGrid.SelectedRows[i].Cells["FPZL"].Value.ToString() == "专用发票")
                            {
                                str = "s";
                            }
                            else if (this.fpGrid.SelectedRows[i].Cells["FPZL"].Value.ToString() == "货物运输业增值税专用发票")
                            {
                                str = "f";
                            }
                            else if (this.fpGrid.SelectedRows[i].Cells["FPZL"].Value.ToString() == "机动车销售统一发票")
                            {
                                str = "j";
                            }
                            string str2 = this.fpGrid.SelectedRows[i].Cells["FPDM"].Value.ToString();
                            int num = Convert.ToInt32(this.fpGrid.SelectedRows[i].Cells["FPHM"].Value.ToString());
                            string str6 = (this.fpGrid.SelectedRows[i].Cells["KPZL"].Value.ToString() == "清单开票") ? "1" : "0";
                            string[] item = new string[] { str, str2, num.ToString(), num2.ToString(), str6 };
                            list.Add(item);
                            num2++;
                        }
                    }
                    object[] objArray = new object[] { list, 0 };
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPALLDYShareMethods", objArray);
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void ToolItemSJLB_Click(object sender, EventArgs e)
        {
            try
            {
                this.fpGrid.Print("发票生成列表", this, null, null, true, false);
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void ToolItemXHQD_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fpGrid.SelectedRows.Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-273301");
                    return;
                }
                List<string[]> list = new List<string[]>();
                int num2 = 0;
                for (int i = this.fpGrid.SelectedRows.Count - 1; i >= 0; i--)
                {
                    string str4 = this.fpGrid.SelectedRows[i].Cells["KPJG"].Value.ToString();
                    string str6 = this.fpGrid.SelectedRows[i].Cells["DJH"].Value.ToString();
                    string str5 = this.fpGrid.SelectedRows[i].Cells["KPZL"].Value.ToString();
                    if (str4 != "成功开票")
                    {
                        MessageManager.ShowMsgBox("INP-273302", "错误", new string[] { str6 });
                    }
                    else if (str5 != "清单开票")
                    {
                        switch (MessageManager.ShowMsgBox("INP-273304", "提示", new string[] { str6 }))
                        {
                            case DialogResult.No:
                                goto Label_02BB;
                        }
                    }
                    else
                    {
                        string str = (this.fpGrid.SelectedRows[i].Cells["FPZL"].Value.ToString() == "专用发票") ? "s" : "c";
                        string str2 = this.fpGrid.SelectedRows[i].Cells["FPDM"].Value.ToString();
                        int num = Convert.ToInt32(this.fpGrid.SelectedRows[i].Cells["FPHM"].Value.ToString());
                        string str7 = (this.fpGrid.SelectedRows[i].Cells["KPZL"].Value.ToString() == "清单开票") ? "1" : "0";
                        string[] item = new string[] { str, str2, num.ToString(), num2.ToString(), str7 };
                        list.Add(item);
                        num2++;
                    }
                }
            Label_02BB:;
                object[] objArray = new object[] { list, 1 };
                object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPALLDYShareMethods", objArray);
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }
    }
}

