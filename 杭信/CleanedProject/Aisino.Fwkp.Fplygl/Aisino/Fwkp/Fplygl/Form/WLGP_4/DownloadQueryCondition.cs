namespace Aisino.Fwkp.Fplygl.Form.WLGP_4
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;

    public class DownloadQueryCondition : DockForm
    {
        private AisinoBTN btn_Cancel;
        private AisinoBTN btn_Ok;
        private AisinoCMB cmb_InvType;
        private AisinoCMB cmb_MachineNum;
        private IContainer components;
        private DateTimePicker data_jsrq;
        private DateTimePicker data_ksrq;
        private ILog loger = LogUtil.GetLogger<DownloadQueryCondition>();
        private bool logFlag;
        private string logPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private List<DownloadInfo> outList = new List<DownloadInfo>();
        protected XmlComponentLoader xmlComponentLoader1;

        public DownloadQueryCondition()
        {
            this.Initialize();
            this.InitializeCmbMachineNum();
            this.InitializeCmbInvtype();
            this.cmb_MachineNum.DropDownStyle = ComboBoxStyle.DropDown;
            this.cmb_InvType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SetDataCtrlAttritute();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (this.data_ksrq.Value > this.data_jsrq.Value)
            {
                MessageManager.ShowMsgBox("INP-441203");
            }
            else if (this.CheckValid())
            {
                base.Close();
                XmlDocument document = this.CreateQueryInput();
                if (this.logFlag)
                {
                    document.Save(this.logPath + "DownloadQueryInput.xml");
                }
                string xml = string.Empty;
                if (HttpsSender.SendMsg("0034", document.InnerXml, ref xml) != 0)
                {
                    MessageManager.ShowMsgBox(xml);
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    if (this.logFlag)
                    {
                        doc.Save(this.logPath + @"\DownloadQueryOutput.xml");
                    }
                    string msg = string.Empty;
                    if (!this.ParseDownloadQueryOutput(doc, out msg))
                    {
                        MessageManager.ShowMsgBox(msg);
                    }
                    else if (this.outList.Count == 0)
                    {
                        MessageManager.ShowMsgBox("INP-441241");
                    }
                    else
                    {
                        new DownloadQueryList(this.outList).ShowDialog();
                    }
                }
            }
        }

        private bool CheckValid()
        {
            string text = this.cmb_MachineNum.Text;
            if (text.Equals("全部") || text.Equals("主机"))
            {
                return true;
            }
            if (text.Equals(string.Empty))
            {
                MessageManager.ShowMsgBox("INP-441238");
                return false;
            }
            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                if ((ch < '0') || (ch > '9'))
                {
                    MessageManager.ShowMsgBox("INP-441239");
                    return false;
                }
            }
            int num2 = Convert.ToInt32(this.cmb_MachineNum.Text);
            if ((num2 >= 0) && (num2 <= 0x3e7))
            {
                return true;
            }
            MessageManager.ShowMsgBox("INP-441240");
            return false;
        }

        private void cmb_MachineNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.ToString().Equals("\b"))
                {
                    e.Handled = false;
                }
                else if ((this.cmb_MachineNum.Text.Length >= 3) && (this.cmb_MachineNum.SelectedText.Length <= 0))
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

        private XmlDocument CreateQueryInput()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.PreserveWhitespace = false;
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("FPXT");
                document.AppendChild(element);
                XmlElement element2 = document.CreateElement("INPUT");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("NSRSBH");
                element3.InnerText = base.TaxCardInstance.get_TaxCode();
                element2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("KPJH");
                element4.InnerText = base.TaxCardInstance.get_Machine().ToString();
                element2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("SBBH");
                element5.InnerText = base.TaxCardInstance.GetInvControlNum();
                element2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("DCBB");
                element6.InnerText = base.TaxCardInstance.get_StateInfo().DriverVersion;
                element2.AppendChild(element6);
                XmlElement element7 = document.CreateElement("CXTJ");
                element2.AppendChild(element7);
                XmlElement element8 = document.CreateElement("FKPJH");
                if (this.cmb_MachineNum.Text.Equals("主机") || this.cmb_MachineNum.Text.Equals("全部"))
                {
                    element8.InnerText = this.cmb_MachineNum.SelectedValue.ToString();
                }
                else
                {
                    element8.InnerText = this.cmb_MachineNum.Text;
                }
                element7.AppendChild(element8);
                XmlElement element9 = document.CreateElement("FPZL");
                element9.InnerText = this.cmb_InvType.SelectedValue.ToString();
                element7.AppendChild(element9);
                XmlElement element10 = document.CreateElement("QSRQ");
                element10.InnerText = this.data_ksrq.Value.ToString("yyyyMMdd");
                element7.AppendChild(element10);
                XmlElement element11 = document.CreateElement("JZRQ");
                element11.InnerText = this.data_jsrq.Value.ToString("yyyyMMdd");
                element7.AppendChild(element11);
                document.PreserveWhitespace = true;
                return document;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
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
            this.cmb_MachineNum = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmb_Zfjh");
            this.cmb_InvType = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmb_fpzl");
            this.data_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_jsrq");
            this.data_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ksrq");
            this.btn_Ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btn_Cancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.cmb_MachineNum.KeyPress += new KeyPressEventHandler(this.cmb_MachineNum_KeyPress);
            this.btn_Ok.Click += new EventHandler(this.btn_Ok_Click);
            this.btn_Cancel.Click += new EventHandler(this.btn_Cancel_Click);
        }

        private void InitializeCmbInvtype()
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
                if (base.TaxCardInstance.get_QYLX().ISHY)
                {
                    row = table.NewRow();
                    row["key"] = "货物运输业增值税专用发票";
                    row["value"] = "11";
                    table.Rows.Add(row);
                }
                if (base.TaxCardInstance.get_QYLX().ISJDC)
                {
                    row = table.NewRow();
                    row["key"] = "机动车销售统一发票";
                    row["value"] = "12";
                    table.Rows.Add(row);
                }
                if (base.TaxCardInstance.get_QYLX().ISPTFPDZ)
                {
                    row = table.NewRow();
                    row["key"] = "电子增值税普通发票";
                    row["value"] = "51";
                    table.Rows.Add(row);
                }
                if (base.TaxCardInstance.get_QYLX().ISPTFPJSP)
                {
                    row = table.NewRow();
                    row["key"] = "增值税普通发票(卷票)";
                    row["value"] = "41";
                    table.Rows.Add(row);
                }
                this.cmb_InvType.ValueMember = "value";
                this.cmb_InvType.DisplayMember = "key";
                this.cmb_InvType.DataSource = table;
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

        private void InitializeCmbMachineNum()
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
                row["key"] = "主机";
                row["value"] = "0";
                table.Rows.Add(row);
                this.cmb_MachineNum.ValueMember = "value";
                this.cmb_MachineNum.DisplayMember = "key";
                this.cmb_MachineNum.DataSource = table;
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

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DownloadQueryCondition));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x17d, 0xe7);
            this.xmlComponentLoader1.TabIndex = 4;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.DownloadQueryCondition\Aisino.Fwkp.Fplygl.Forms.DownloadQueryCondition.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x17d, 0xe7);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "DownloadQueryCondition";
            base.set_TabText("DownloadQueryCondition");
            this.Text = "票量查询设置";
            base.ResumeLayout(false);
        }

        private bool ParseDownloadQueryOutput(XmlDocument doc, out string msg)
        {
            try
            {
                this.outList.Clear();
                XmlNode node = doc.SelectSingleNode("//CODE");
                XmlNode node2 = doc.SelectSingleNode("//MESS");
                if (!node.InnerText.Equals("0000"))
                {
                    msg = node2.InnerText;
                    return false;
                }
                XmlNodeList list = doc.SelectSingleNode("//GPXX").SelectNodes("//FPJXX");
                if ((list != null) && (list.Count > 0))
                {
                    foreach (XmlNode node4 in list)
                    {
                        DownloadInfo item = new DownloadInfo();
                        foreach (XmlNode node5 in node4.ChildNodes)
                        {
                            if (node5.Name.Equals("FKPJH"))
                            {
                                item.machineNum = node5.InnerText;
                            }
                            else if (node5.Name.Equals("FPZL"))
                            {
                                item.typeName = ShareMethods.GetInvType(Convert.ToByte(node5.InnerText));
                            }
                            else if (node5.Name.Equals("FPDM"))
                            {
                                item.typeCode = node5.InnerText;
                            }
                            else if (node5.Name.Equals("QSHM"))
                            {
                                item.startNum = node5.InnerText.PadLeft(8, '0');
                            }
                            else if (node5.Name.Equals("ZZHM"))
                            {
                                item.endNum = node5.InnerText.PadLeft(8, '0');
                            }
                            else if (node5.Name.Equals("FS"))
                            {
                                item.count = node5.InnerText;
                            }
                            else if (node5.Name.Equals("LGRQ"))
                            {
                                string innerText = node5.InnerText;
                                item.buyDate = innerText.Substring(0, 4) + "-" + innerText.Substring(4, 2) + "-" + innerText.Substring(6, 2);
                            }
                        }
                        this.outList.Add(item);
                    }
                }
                msg = string.Empty;
                return true;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                msg = exception.Message;
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                msg = exception2.Message;
                return false;
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
                this.data_ksrq.Value = new DateTime(year, month, 1);
                int num3 = base.TaxCardInstance.GetCardClock().Year;
                if (num3 < 0x6d9)
                {
                    num3 = DateTime.Now.Year;
                }
                int day = DateTime.DaysInMonth(num3, month);
                this.data_jsrq.Value = new DateTime(year, month, day);
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

