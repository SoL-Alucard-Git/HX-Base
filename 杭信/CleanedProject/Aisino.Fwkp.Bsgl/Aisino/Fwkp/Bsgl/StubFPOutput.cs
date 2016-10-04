namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class StubFPOutput : DockForm
    {
        private AisinoBTN btnBrowse;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoCHK chkFPDetail;
        private IContainer components;
        private DateTimePicker dtpEnd;
        private DateTimePicker dtpStart;
        private AisinoGRP groupBox1;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private string lastPath = "";
        private ILog loger = LogUtil.GetLogger<StubFPOutput>();
        private bool nonSetCheck = true;
        private bool outputMX;
        private AisinoTXT txtFilePath;
        private XmlComponentLoader xmlComponentLoader1;

        public StubFPOutput()
        {
            this.Initialize();
            base.Load += new EventHandler(this.StubFPOutput_Load);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = dialog.SelectedPath;
            }
            if (string.IsNullOrEmpty(this.lastPath) || (this.lastPath != this.txtFilePath.Text.Trim()))
            {
                PropertyUtil.SetValue("Aisino.Fwkp.Bsgl.StubFPOutput.OutputPath", this.txtFilePath.Text.Trim());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.nonSetCheck || (this.outputMX != this.chkFPDetail.Checked))
            {
                PropertyUtil.SetValue("Aisino.Fwkp.Bsgl.StubFPOutput.IsCheckedFPMX", this.chkFPDetail.Checked.ToString());
            }
            if (this.txtFilePath.Text.Trim() == "")
            {
                MessageManager.ShowMsgBox("INP-251206");
                this.txtFilePath.Select();
            }
            else
            {
                FPDetailDAL ldal = new FPDetailDAL();
                List<FPDetail> fPDetailList = ldal.GetFPDetailList(this.dtpStart.Value, this.dtpEnd.Value);
                if (this.chkFPDetail.Checked)
                {
                    foreach (FPDetail detail in fPDetailList)
                    {
                        if (detail != null)
                        {
                            detail.GoodsList.AddRange(ldal.GetGoodsList(detail.FPType.ToString(), detail.FPDM, detail.FPHM));
                        }
                    }
                }
                base.TaxCardInstance.get_TaxCode();
                string str = "JK_" + base.TaxCardInstance.get_TaxCode() + DateTime.Now.ToString("yyyyMMdd") + "_KP.XML";
                string filename = Path.Combine(this.txtFilePath.Text.Trim(), str);
                try
                {
                    XmlTextWriter writer = new XmlTextWriter(filename, ToolUtil.GetEncoding()) {
                        Formatting = Formatting.Indented
                    };
                    writer.WriteStartDocument();
                    writer.WriteStartElement("FPSJJK");
                    this.WriteBaseInfo(writer, fPDetailList.Count);
                    writer.WriteStartElement("JK_FWSKKP_NSR_FPXX");
                    if (fPDetailList.Count > 0)
                    {
                        string text = "【发票头信息】\r\n发票种类(0:专用发票 1:废旧物资发票 2:普通发票)\r\n发票代码\r\n发票号码\r\n开票日期(格式:YYYYMMDD)\r\n购方纳税人识别号\r\n购方名称\r\n购方地址电话\r\n购方银行帐号\r\n合计金额\r\n合计税额\r\n税率\r\n作废标志(0:未作废 1:作废)\r\n开票人\r\n销方纳税人识别号\r\n销方名称\r\n销方地址电话\r\n销方银行帐号\r\n备注\r\n";
                        string str4 = "【发票明细信息】\r\n商品名称\r\n规格型号\r\n计量单位\r\n数量\r\n单价\r\n金额\r\n税额\r\n";
                        writer.WriteComment(text);
                        writer.WriteComment(str4);
                    }
                    foreach (FPDetail detail2 in fPDetailList)
                    {
                        writer.WriteStartElement("JK_FWSKKP_FPXX");
                        WriteFPBTXX(writer, detail2);
                        WriteFPMX(writer, detail2);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                    writer.Close();
                    MessageManager.ShowMsgBox("INP-251411", "提示", new string[] { str });
                }
                catch (Exception exception)
                {
                    MessageManager.ShowMsgBox("INP-251412");
                    this.loger.Error(exception.Message, exception);
                    return;
                }
                base.Close();
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
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.dtpStart = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dtpStart");
            this.dtpEnd = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dtpEnd");
            this.txtFilePath = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtFilePath");
            this.btnBrowse = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnBrowse");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.chkFPDetail = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkFPDetail");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1a0, 0xe5);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.StubFPOutput\Aisino.Fwkp.Bsgl.StubFPOutput.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a0, 0xe5);
            base.Controls.Add(this.xmlComponentLoader1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "StubFPOutput";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "选择开票日期区间及其传出文件路径";
            base.ResumeLayout(false);
        }

        private void StubFPOutput_Load(object sender, EventArgs e)
        {
            DateTime time = base.TaxCardInstance.get_TaxClock();
            DateTime time2 = new DateTime(time.Year, time.Month, 1);
            this.dtpStart.MinDate = time2;
            this.dtpStart.MaxDate = time;
            this.dtpStart.Value = time2;
            this.dtpEnd.MinDate = this.dtpStart.Value;
            this.dtpEnd.MaxDate = time;
            this.dtpEnd.Value = time;
            this.lastPath = PropertyUtil.GetValue("Aisino.Fwkp.Bsgl.StubFPOutput.OutputPath");
            string str = PropertyUtil.GetValue("Aisino.Fwkp.Bsgl.StubFPOutput.IsCheckedFPMX");
            if (!string.IsNullOrEmpty(str))
            {
                this.outputMX = Convert.ToBoolean(str);
                this.chkFPDetail.Checked = this.outputMX;
                this.nonSetCheck = false;
            }
            this.txtFilePath.Text = this.lastPath;
        }

        private void WriteBaseInfo(XmlTextWriter writer, int fpCount)
        {
            string text = "与防伪税控开票子系统发票数据的接口";
            string str2 = "\r\n纳税人识别号\r\n发票所属期起始日期(格式:YYYYMMDD)\r\n发票所属期截止日期(格式:YYYYMMDD)\r\n发票张数\r\n";
            writer.WriteComment(text);
            writer.WriteComment(str2);
            writer.WriteStartElement("JK_FWSKKP_NSR_BTXX");
            writer.WriteElementString("JK_FWSKKP_BTXX_NSRSBH", base.TaxCardInstance.get_TaxCode());
            writer.WriteElementString("JK_FWSKKP_BTXX_QSRQ", this.dtpStart.Value.ToString("yyyyMMdd"));
            writer.WriteElementString("JK_FWSKKP_BTXX_ZZRQ", this.dtpEnd.Value.ToString("yyyyMMdd"));
            writer.WriteElementString("JK_FWSKKP_BTXX_FPZS", XmlConvert.ToString(fpCount));
            writer.WriteEndElement();
        }

        private static void WriteFPBTXX(XmlTextWriter writer, FPDetail fpxx)
        {
            writer.WriteStartElement("JK_FWSKKP_FPXX_BTXX");
            writer.WriteElementString("JK_FWSKKP_FPXX_FPZL", XmlConvert.ToString((int) fpxx.FPType));
            writer.WriteElementString("JK_FWSKKP_FPXX_FPDM", fpxx.FPDM);
            writer.WriteElementString("JK_FWSKKP_FPXX_FPHM", XmlConvert.ToString(fpxx.FPHM));
            writer.WriteElementString("JK_FWSKKP_FPXX_KPRQ", fpxx.KPRQ.ToString("yyyyMMdd"));
            writer.WriteElementString("JK_FWSKKP_FPXX_GFSBH", fpxx.GFSH);
            writer.WriteElementString("JK_FWSKKP_FPXX_GFMC", fpxx.GFMC);
            writer.WriteElementString("JK_FWSKKP_FPXX_GFDZDH", fpxx.GFDZDH);
            writer.WriteElementString("JK_FWSKKP_FPXX_GFYHZH", fpxx.GFYHZH);
            writer.WriteElementString("JK_FWSKKP_FPXX_HJJE", XmlConvert.ToString(fpxx.HJJE));
            writer.WriteElementString("JK_FWSKKP_FPXX_HJSE", XmlConvert.ToString(fpxx.HJSE));
            writer.WriteElementString("JK_FWSKKP_FPXX_SL", XmlConvert.ToString(fpxx.SLV));
            writer.WriteElementString("JK_FWSKKP_FPXX_ZFBZ", fpxx.ZFBZ ? "1" : "0");
            writer.WriteElementString("JK_FWSKKP_FPXX_KPR", fpxx.KPR);
            writer.WriteElementString("JK_FWSKKP_FPXX_XFSBH", fpxx.XFSH);
            writer.WriteElementString("JK_FWSKKP_FPXX_XFMC", fpxx.XFMC);
            writer.WriteElementString("JK_FWSKKP_FPXX_XFDZDH", fpxx.XFDZDH);
            writer.WriteElementString("JK_FWSKKP_FPXX_XFYHZH", fpxx.XFYHZH);
            writer.WriteElementString("JK_FWSKKP_FPXX_BZ", fpxx.BZ);
            writer.WriteEndElement();
        }

        private static void WriteFPMX(XmlTextWriter writer, FPDetail fpxx)
        {
            writer.WriteStartElement("JK_FWSKKP_FPMX");
            foreach (GoodsInfo info in fpxx.GoodsList)
            {
                writer.WriteStartElement("JK_FWSKKP_FPMX_MXXX");
                writer.WriteElementString("JK_FWSKKP_MXXX_SPMC", info.Name);
                writer.WriteElementString("JK_FWSKKP_MXXX_GGXH", info.SpecMark);
                writer.WriteElementString("JK_FWSKKP_MXXX_JLDW", info.Unit);
                string str = info.Num.ToString();
                if (string.IsNullOrEmpty(info.Num))
                {
                    str = "";
                }
                writer.WriteElementString("JK_FWSKKP_MXXX_SL", str);
                string str2 = info.Price.ToString();
                if (string.IsNullOrEmpty(info.Price))
                {
                    str2 = "";
                }
                writer.WriteElementString("JK_FWSKKP_MXXX_DJ", str2);
                writer.WriteElementString("JK_FWSKKP_MXXX_JE", XmlConvert.ToString(info.Amount));
                writer.WriteElementString("JK_FWSKKP_MXXX_SE", XmlConvert.ToString(info.Tax));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}

