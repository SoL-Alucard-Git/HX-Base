namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class WaitForm : BaseForm
    {
        public bool bIsNeedRepair;
        private IContainer components;
        private InvoiceReportBLL invoiceReportBLL = new InvoiceReportBLL();
        private AisinoLBL labelWait = new AisinoLBL();
        private int nCurrentPos;
        private int nMonth;
        private const int nTotalStep = 8;
        private AisinoPRG progressBarWait = new AisinoPRG();
        public string strRet = "";
        private XmlComponentLoader xmlComponentLoader1;

        public WaitForm()
        {
            this.Initial();
            this.nMonth = this.invoiceReportBLL.GetDateTime().Month;
        }

        public bool DataCheck()
        {
            try
            {
                this.progressBarWait.Value = ++this.nCurrentPos;
                if (this.invoiceReportBLL.IsLocked())
                {
                    MessageManager.ShowMsgBox("INP-253201");
                    return false;
                }
                if (this.invoiceReportBLL.IsBranchMachine())
                {
                    this.progressBarWait.Value = ++this.nCurrentPos;
                    if (this.invoiceReportBLL.HasInvData())
                    {
                        this.progressBarWait.Value = ++this.nCurrentPos;
                        MessageManager.ShowMsgBox("INP-253202");
                        return false;
                    }
                    if (this.invoiceReportBLL.HasReturnInv())
                    {
                        this.progressBarWait.Value = ++this.nCurrentPos;
                        MessageManager.ShowMsgBox("INP-253203");
                        return false;
                    }
                }
                if (this.invoiceReportBLL.GetTaxcardVersion())
                {
                    this.progressBarWait.Value = ++this.nCurrentPos;
                    if (!this.invoiceReportBLL.CheckIntegrity(this.nMonth))
                    {
                        this.bIsNeedRepair = true;
                        this.progressBarWait.Value = ++this.nCurrentPos;
                        if (DialogResult.OK == MessageBoxHelper.Show("确定要进行发票修复吗？", "发票修复提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                        {
                            this.progressBarWait.Value = ++this.nCurrentPos;
                            if (!this.invoiceReportBLL.InvoiceRepair(this.nMonth))
                            {
                                this.strRet = "发票修复失败";
                            }
                            else
                            {
                                this.strRet = "发票修复成功";
                            }
                        }
                        else
                        {
                            this.strRet = "已取消发票修复！";
                        }
                    }
                    return true;
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-253207", new string[] { exception.ToString() });
            }
            return true;
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
            base.MinimizeBox = false;
            base.MaximizeBox = false;
            this.MinimumSize = new Size(400, 220);
            this.MaximumSize = new Size(400, 220);
            this.labelWait = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelWait");
            this.progressBarWait = this.xmlComponentLoader1.GetControlByName<AisinoPRG>("progressBarWait");
            this.progressBarWait.Maximum = 8;
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x188, 0xba);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.WaitForm\Aisino.Fwkp.Bsgl.WaitForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x188, 0xba);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "WaitForm";
            this.Text = "正在生成发票领用存月报表";
            base.ResumeLayout(false);
        }
    }
}

