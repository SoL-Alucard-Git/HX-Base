namespace Aisino.Fwkp.Fpkj.Form.DKFP
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.DAL;
    using Aisino.Fwkp.Fpkj.Form.FPXF;
    using Aisino.Fwkp.Fpkj.IDAL;
    using Aisino.Fwkp.Fpkj.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml;

    public class DiKouFaPiaoXiaZai : BaseForm
    {
        private AisinoBTN btn_cancle;
        private AisinoBTN btn_download;
        private AisinoCMB cmb_fpzl;
        private IContainer components;
        private DateTimePicker data_kprq;
        private ILog loger = LogUtil.GetLogger<DiKouFaPiaoXiaZai>();
        private FPProgressBar progressBar = new FPProgressBar();
        private int step = 0x7d0;
        private AisinoTXT txt_fpdm;
        private AisinoTXT txt_fphm;
        private XmlComponentLoader xmlComponentLoader1;

        public DiKouFaPiaoXiaZai()
        {
            try
            {
                this.Initialize();
                this.cmb_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmb_fpzl");
                this.txt_fpdm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_fpdm");
                this.txt_fphm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_fphm");
                this.data_kprq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_kprq");
                this.btn_cancle = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_cancle");
                this.btn_download = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_download");
                this.btn_download.Click += new EventHandler(this.BtnDownload_Click);
                this.btn_cancle.Click += new EventHandler(this.BtnCancle_Click);
                this.cmb_fpzl.Items.Add("增值税专用发票");
                this.cmb_fpzl.Items.Add("货物运输业增值税专用发票");
                this.cmb_fpzl.Items.Add("机动车销售统一发票");
                this.cmb_fpzl.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmb_fpzl.BackColor = Color.White;
                this.cmb_fpzl.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void BtnCancle_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            string xml = string.Empty;
            try
            {
                base.DialogResult = DialogResult.OK;
                int num = 0;
                str = this.GenDownloadXML();
                if (str == null)
                {
                    MessageBoxHelper.Show("请正确输入抵扣发票下载条件！");
                    base.DialogResult = DialogResult.Retry;
                }
                else
                {
                    this.progressBar.SetTip("正在连接服务器", "请等待任务完成", "发票下载中");
                    this.progressBar.fpxf_progressBar.Value = 0x3e8;
                    this.progressBar.Visible = true;
                    this.progressBar.Refresh();
                    this.progressBar.Show();
                    this.ProcessStartThread(this.step);
                    this.progressBar.Refresh();
                    HttpsSender.SendMsg("0012", str, out xml);
                    this.progressBar.SetTip("正在下载发票", "请等待任务完成", "发票下载中");
                    this.ProcessStartThread(this.step * 3);
                    this.progressBar.Refresh();
                    switch (xml)
                    {
                        case null:
                        case "":
                            xml = "受理服务器没有返回数据。";
                            break;
                    }
                    List<Aisino.Fwkp.Fpkj.Model.DKFP> dkfps = new List<Aisino.Fwkp.Fpkj.Model.DKFP>();
                    XmlDocument document = new XmlDocument();
                    string innerText = "";
                    if (xml != "")
                    {
                        document.LoadXml(xml);
                        System.Xml.XmlNode node2 = document.SelectSingleNode("/FPXT").SelectSingleNode("OUTPUT");
                        if (node2 != null)
                        {
                            System.Xml.XmlNode node3 = node2.SelectSingleNode("CODE");
                            if (node3 != null)
                            {
                                innerText = node3.InnerText;
                                if (node3.InnerText == "0000")
                                {
                                    System.Xml.XmlNode node4 = node2.SelectSingleNode("DATA");
                                    if (node4 != null)
                                    {
                                        System.Xml.XmlNode node5 = node4.SelectSingleNode("RZFP");
                                        if (node5 != null)
                                        {
                                            XmlNodeList list2 = node5.SelectNodes("FPXX");
                                            if (list2 != null)
                                            {
                                                foreach (System.Xml.XmlNode node6 in list2)
                                                {
                                                    Aisino.Fwkp.Fpkj.Model.DKFP item = new Aisino.Fwkp.Fpkj.Model.DKFP {
                                                        FPDM = node6.SelectSingleNode("FPDM").InnerText.Trim(),
                                                        FPHM = Convert.ToInt32(node6.SelectSingleNode("FPHM").InnerText.Trim()),
                                                        FPLX = node6.SelectSingleNode("FPLX").InnerText.Trim(),
                                                        RZLX = node6.SelectSingleNode("RZLX").InnerText.Trim(),
                                                        KPRQ = Convert.ToDateTime(this.ConvertDateFormat(node6.SelectSingleNode("KPRQ").InnerText.Trim())),
                                                        RZRQ = Convert.ToDateTime(this.ConvertDateFormat(node6.SelectSingleNode("RZRQ").InnerText.Trim())),
                                                        XFSBH = node6.SelectSingleNode("XFSBH").InnerText.Trim(),
                                                        JE = Convert.ToDouble(node6.SelectSingleNode("JE").InnerText.Trim()),
                                                        SE = Convert.ToDouble(node6.SelectSingleNode("SE").InnerText.Trim()),
                                                        JSPH = "",
                                                        KPJH = "",
                                                        XZSJ = DateTime.Now
                                                    };
                                                    dkfps.Add(item);
                                                    num++;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    System.Xml.XmlNode node7 = node2.SelectSingleNode("MESS");
                                    if ((node7.InnerText == null) || (node7.InnerText == ""))
                                    {
                                        node7.InnerText = "下载服务器错误";
                                    }
                                    MessageManager.ShowMsgBox("DKFPXZ-0003", "提示", new string[] { node7.InnerText });
                                }
                            }
                        }
                        if (num > 0)
                        {
                            this.progressBar.SetTip("正在保存发票", "请等待任务完成", "发票下载中");
                            this.ProcessStartThread(this.step * 4);
                            this.progressBar.Refresh();
                            this.progressBar.Visible = false;
                            int num2 = this.SaveDkfpToDb(dkfps);
                            if (num == num2)
                            {
                                MessageManager.ShowMsgBox("DKFPXZ-0004", "提示", new string[] { num2.ToString() });
                            }
                            else
                            {
                                string[] strArray3 = new string[] { num.ToString(), num2.ToString(), (num - num2).ToString() };
                                MessageManager.ShowMsgBox("DKFPXZ-0005", "错误", strArray3);
                            }
                            this.ProcessStartThread(this.step * 4);
                            this.progressBar.Refresh();
                            this.progressBar.Visible = false;
                        }
                        else if ((num == 0) && (innerText == "0000"))
                        {
                            MessageManager.ShowMsgBox("DKFPXZ-0006");
                            this.progressBar.Visible = false;
                            this.Refresh();
                        }
                    }
                    else
                    {
                        MessageManager.ShowMsgBox("DKFPXZ-0003", "提示", new string[] { xml });
                        this.ProcessStartThread(this.step * 4);
                        this.progressBar.Refresh();
                        this.progressBar.Visible = false;
                    }
                    this.Refresh();
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("DKFPXZ-0003", "提示", new string[] { xml });
                this.loger.Error(exception.Message);
                this.ProcessStartThread(this.step * 4);
                this.progressBar.Visible = false;
                this.Refresh();
            }
            finally
            {
                this.progressBar.Visible = false;
                this.Refresh();
            }
        }

        public string ConvertDateFormat(string str)
        {
            string str2 = "";
            if (str.Length == 8)
            {
                str2 = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
            }
            return str2;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string GenDownloadXML()
        {
            XmlDocument document = new XmlDocument();
            document.CreateXmlDeclaration("1.0", "GBK", "yes");
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.AppendChild(newChild);
            System.Xml.XmlNode node = document.CreateElement("FPXT");
            System.Xml.XmlNode node2 = document.CreateElement("INPUT");
            System.Xml.XmlNode node3 = document.CreateElement("NSRSBH");
            node3.InnerText = base.TaxCardInstance.TaxCode;
            System.Xml.XmlNode node4 = document.CreateElement("JSPH");
            node4.InnerText = base.TaxCardInstance.GetInvControlNum();
            System.Xml.XmlNode node5 = document.CreateElement("KPJH");
            node5.InnerText = base.TaxCardInstance.Machine.ToString();
            System.Xml.XmlNode node6 = document.CreateElement("FPDM");
            node6.InnerText = this.txt_fpdm.Text.Trim();
            System.Xml.XmlNode node7 = document.CreateElement("FPHM");
            node7.InnerText = ShareMethods.FPHMTo8Wei(this.txt_fphm.Text.Trim());
            System.Xml.XmlNode node8 = document.CreateElement("KPRQ");
            System.Xml.XmlNode node10 = document.CreateElement("FPLX");
            string str = "";
            this.cmb_fpzl.DropDownStyle = ComboBoxStyle.DropDownList;
            if (this.cmb_fpzl.SelectedIndex == 0)
            {
                str = "01";
            }
            else if (this.cmb_fpzl.SelectedIndex == 1)
            {
                str = "02";
            }
            else if (this.cmb_fpzl.SelectedIndex == 2)
            {
                str = "03";
            }
            else if (this.cmb_fpzl.SelectedIndex == 3)
            {
                str = "04";
            }
            node10.InnerText = str;
            node8.InnerText = "";
            if (this.data_kprq.Checked)
            {
                DateTime time1 = this.data_kprq.Value;
                node8.InnerText = this.data_kprq.Value.ToString("yyyyMMdd");
            }
            System.Xml.XmlNode node9 = document.CreateElement("XFSH");
            node9.InnerText = "";
            document.AppendChild(node);
            node.AppendChild(node2);
            node2.AppendChild(node3);
            node2.AppendChild(node4);
            node2.AppendChild(node5);
            node2.AppendChild(node6);
            node2.AppendChild(node7);
            node2.AppendChild(node8);
            node2.AppendChild(node9);
            node2.AppendChild(node10);
            if (((this.txt_fpdm.Text.Length != 0) && (this.txt_fphm.Text.Length != 0)) && ((str.Length != 0) && this.data_kprq.Checked))
            {
                int result = 0;
                if ((this.txt_fphm.Text.Length <= 8) && int.TryParse(this.txt_fphm.Text.ToString(), out result))
                {
                    return document.InnerXml;
                }
            }
            return null;
        }

        private void Initialize()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DiKouFaPiaoXiaZai));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x159, 0x13c);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Text = "抵扣发票下载";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.DKFP.DiKouFaPiaoXiaZai\Aisino.Fwkp.Fpkj.Form.DKFP.DiKouFaPiaoXiaZai.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x159, 0x13c);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DiKouFaPiaoXiaZai";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "抵扣发票下载";
            base.ResumeLayout(false);
        }

        private void PerformStep(int step)
        {
            for (int i = 0; i < step; i++)
            {
                this.progressBar.fpxf_progressBar.Value++;
            }
            this.Refresh();
        }

        private void ProccessBarShow(object obj)
        {
            try
            {
                int step = (int) obj;
                this.PerformStep(step);
            }
            catch (Exception exception)
            {
                this.loger.Info("[ThreadFun]" + exception.Message);
            }
        }

        public void ProcessStartThread(int value)
        {
            this.ProccessBarShow(value);
        }

        private int SaveDkfpToDb(List<Aisino.Fwkp.Fpkj.Model.DKFP> Dkfps)
        {
            int num = 0;
            IDKFP idkfp = new Aisino.Fwkp.Fpkj.DAL.DKFP();
            foreach (Aisino.Fwkp.Fpkj.Model.DKFP dkfp in Dkfps)
            {
                if (idkfp.Add(dkfp))
                {
                    num++;
                }
            }
            return num;
        }

        private delegate void PerformStepHandle(int step);
    }
}

