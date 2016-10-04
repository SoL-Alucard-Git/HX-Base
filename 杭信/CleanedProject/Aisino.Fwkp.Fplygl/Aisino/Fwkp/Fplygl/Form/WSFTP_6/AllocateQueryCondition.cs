namespace Aisino.Fwkp.Fplygl.Form.WSFTP_6
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.Form.WSFTP_6.Common;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;

    public class AllocateQueryCondition : DockForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoCMB cmb_fpzl;
        private AisinoCMB cmb_fpzt;
        private IContainer components;
        private DateTimePicker date_jsrq;
        private DateTimePicker date_ksrq;
        private ILog loger = LogUtil.GetLogger<AllocateQueryCondition>();
        private bool logFlag;
        private string logPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private List<AllocateInfo> outList = new List<AllocateInfo>();
        public List<string> queryParams = new List<string>();
        private TextBoxRegex tbxMachine;
        protected XmlComponentLoader xmlComponentLoader1;

        public AllocateQueryCondition()
        {
            this.Initialize();
            this.InitializeTypeCMB();
            this.InitializeStatusCMB();
            this.SetDataCtrlAttritute();
            this.tbxMachine.set_RegexText("^[0-9]*$");
            this.tbxMachine.MaxLength = 3;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.date_ksrq.Value > this.date_jsrq.Value)
            {
                MessageManager.ShowMsgBox("INP-441203");
            }
            else if (this.CheckValid())
            {
                base.Close();
                XmlDocument document = AllocateCommon.CreateAllocateQueryInput(this.queryParams);
                if (this.logFlag)
                {
                    document.Save(this.logPath + "AllocateQueryInput.xml");
                }
                string xml = string.Empty;
                if (HttpsSender.SendMsg("0040", document.InnerXml, ref xml) != 0)
                {
                    MessageManager.ShowMsgBox(xml);
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    if (this.logFlag)
                    {
                        doc.Save(this.logPath + @"\AllocateQueryOutput.xml");
                    }
                    string msg = string.Empty;
                    if (!AllocateCommon.ParseAllocateQueryOutput(doc, out msg, out this.outList))
                    {
                        MessageManager.ShowMsgBox(msg);
                    }
                    else if (this.outList.Count == 0)
                    {
                        MessageManager.ShowMsgBox("INP-441207", new string[] { "查询", "分配" });
                    }
                    else
                    {
                        new AllocateQueryList(this.outList).ShowDialog();
                    }
                }
            }
        }

        private bool CheckValid()
        {
            if (this.tbxMachine.Text.Equals(string.Empty))
            {
                this.tbxMachine.Focus();
                MessageManager.ShowMsgBox("INP-441206", new string[] { "分开票机号" });
                return false;
            }
            int num = Convert.ToInt32(this.tbxMachine.Text);
            if ((num < 1) || (num > 0x3e7))
            {
                this.tbxMachine.Focus();
                MessageManager.ShowMsgBox("INP-441205", new string[] { "开票机号", "为1-999内的整数" });
                return false;
            }
            this.queryParams.Add(this.tbxMachine.Text);
            this.queryParams.Add(this.cmb_fpzl.SelectedValue.ToString());
            this.queryParams.Add(this.cmb_fpzt.SelectedValue.ToString());
            this.queryParams.Add(this.date_ksrq.Value.ToString("yyyyMMdd"));
            this.queryParams.Add(this.date_jsrq.Value.ToString("yyyyMMdd"));
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.tbxMachine = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("tbxMachine");
            this.cmb_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmb_fpzl");
            this.cmb_fpzt = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmb_fpzt");
            this.date_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("date_ksrq");
            this.date_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("date_jsrq");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.tbxMachine.KeyPress += new KeyPressEventHandler(this.tbxMachine_KeyPress);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AllocateQueryCondition));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x17a, 0x109);
            this.xmlComponentLoader1.TabIndex = 8;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.WebWindows.AllocateQueryCondition\Aisino.Fwkp.Fplygl.Forms.WebWindows.AllocateQueryCondition.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x17a, 0x109);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "AllocateQueryCondition";
            base.set_TabText("AllocateQuery");
            this.Text = "领用状态查询条件";
            base.ResumeLayout(false);
        }

        private void InitializeStatusCMB()
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("key");
                table.Columns.Add("value");
                DataRow row = table.NewRow();
                row["key"] = "全部";
                row["value"] = "";
                table.Rows.Add(row);
                row = table.NewRow();
                row["key"] = "已下载";
                row["value"] = "YXZ";
                table.Rows.Add(row);
                row = table.NewRow();
                row["key"] = "未下载";
                row["value"] = "WXZ";
                table.Rows.Add(row);
                this.cmb_fpzt.ValueMember = "value";
                this.cmb_fpzt.DisplayMember = "key";
                this.cmb_fpzt.DataSource = table;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void InitializeTypeCMB()
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("key");
                table.Columns.Add("value");
                DataRow row = table.NewRow();
                row["key"] = "全部";
                row["value"] = "";
                table.Rows.Add(row);
                if (base.TaxCardInstance.get_QYLX().ISZYFP)
                {
                    row = table.NewRow();
                    row["key"] = "增值税专用发票";
                    row["value"] = "0";
                    table.Rows.Add(row);
                }
                if (base.TaxCardInstance.get_QYLX().ISPTFP)
                {
                    row = table.NewRow();
                    row["key"] = "增值税普通发票";
                    row["value"] = "2";
                    table.Rows.Add(row);
                }
                if (base.TaxCardInstance.get_QYLX().ISJDC)
                {
                    row = table.NewRow();
                    row["key"] = "机动车销售统一发票";
                    row["value"] = "12";
                    table.Rows.Add(row);
                }
                this.cmb_fpzl.ValueMember = "value";
                this.cmb_fpzl.DisplayMember = "key";
                this.cmb_fpzl.DataSource = table;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void SetDataCtrlAttritute()
        {
            try
            {
                int month = base.TaxCardInstance.GetCardClock().Month;
                int year = base.TaxCardInstance.GetCardClock().Year;
                if (year < 0x6d9)
                {
                    year = DateTime.Now.Year;
                }
                DateTime.DaysInMonth(year, month);
                this.date_ksrq.Value = new DateTime(year, month, 1);
                int num3 = base.TaxCardInstance.GetCardClock().Year;
                if (num3 < 0x6d9)
                {
                    num3 = DateTime.Now.Year;
                }
                int day = DateTime.DaysInMonth(num3, month);
                this.date_jsrq.Value = new DateTime(year, month, day);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void tbxMachine_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.ToString().Equals("\b"))
                {
                    e.Handled = false;
                }
                else if ((this.tbxMachine.Text.Length >= 3) && (this.tbxMachine.SelectedText.Length <= 0))
                {
                    e.Handled = true;
                }
                else if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }
    }
}

