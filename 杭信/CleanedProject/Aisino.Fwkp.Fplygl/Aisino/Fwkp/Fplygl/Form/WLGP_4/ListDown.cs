namespace Aisino.Fwkp.Fplygl.Form.WLGP_4
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.Common;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;

    public class ListDown : DockForm
    {
        public List<int> availableType = new List<int>();
        private AisinoBTN btnFpxz;
        private AisinoBTN btnQuery;
        private AisinoBTN btnSgtj;
        private AisinoCHK chkJZ;
        private AisinoCHK chkQS;
        private IContainer components;
        private CustomStyleDataGrid csdgList;
        private DateTimePicker data_jsrq;
        private DateTimePicker data_ksrq;
        private ILog loger = LogUtil.GetLogger<ListDown>();
        private bool logFlag;
        private string logPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        public List<InvVolumeApp> reqInvList = new List<InvVolumeApp>();
        protected XmlComponentLoader xmlComponentLoader1;

        public ListDown()
        {
            this.Initialize();
            this.GetSQType();
            this.SetDataCtrlAttritute();
            this.btnFpxz.Enabled = false;
        }

        private void btnFpxz_Click(object sender, EventArgs e)
        {
            if (this.OutDownList())
            {
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.reqInvList.Clear();
            if ((this.chkQS.Checked && this.chkJZ.Checked) && (this.data_ksrq.Value > this.data_jsrq.Value))
            {
                MessageManager.ShowMsgBox("INP-441203");
            }
            else
            {
                string xml = string.Empty;
                if (HttpsSender.SendMsg("0019", this.RequestListInput(), ref xml) != 0)
                {
                    MessageManager.ShowMsgBox(xml);
                }
                else
                {
                    XmlDocument invList = new XmlDocument();
                    invList.LoadXml(xml);
                    if (this.logFlag)
                    {
                        invList.Save(this.logPath + @"\RequestListOutput.xml");
                    }
                    List<InvVolumeApp> listModel = this.RequestListOutput(invList);
                    listModel.Sort(delegate (InvVolumeApp left, InvVolumeApp right) {
                        if (left.InvType > right.InvType)
                        {
                            return 1;
                        }
                        if (left.InvType == right.InvType)
                        {
                            if (Convert.ToInt64(left.TypeCode) > Convert.ToInt64(right.TypeCode))
                            {
                                return 1;
                            }
                            if (Convert.ToInt64(left.TypeCode) != Convert.ToInt64(right.TypeCode))
                            {
                                return -1;
                            }
                            if (left.HeadCode > right.HeadCode)
                            {
                                return 1;
                            }
                            if (left.HeadCode == right.HeadCode)
                            {
                                return 0;
                            }
                        }
                        return -1;
                    });
                    this.InsertData(listModel);
                }
            }
        }

        private void btnSgtj_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.No;
            base.Close();
        }

        private void chkJZ_CheckedChanged(object sender, EventArgs e)
        {
            this.data_jsrq.Enabled = this.chkJZ.Checked;
        }

        private void chkQS_CheckedChanged(object sender, EventArgs e)
        {
            this.data_ksrq.Enabled = this.chkQS.Checked;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GetSQType()
        {
            if (base.TaxCardInstance.get_QYLX().ISZYFP)
            {
                this.availableType.Add(0);
            }
            if (base.TaxCardInstance.get_QYLX().ISPTFP)
            {
                this.availableType.Add(2);
            }
            if (base.TaxCardInstance.get_QYLX().ISHY)
            {
                this.availableType.Add(11);
            }
            if (base.TaxCardInstance.get_QYLX().ISJDC)
            {
                this.availableType.Add(12);
            }
            if (base.TaxCardInstance.get_QYLX().ISPTFPJSP)
            {
                this.availableType.Add(0x29);
            }
        }

        private void gridSetting()
        {
            this.csdgList.AllowUserToDeleteRows = false;
            this.csdgList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.csdgList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.csdgList.Columns["fpzl"].ReadOnly = true;
            this.csdgList.Columns["fpdm"].ReadOnly = true;
            this.csdgList.Columns["qshm"].ReadOnly = true;
            this.csdgList.Columns["fpzs"].ReadOnly = true;
            this.csdgList.Columns["xz"].FillWeight = 10f;
            this.csdgList.Columns["fpzl"].FillWeight = 25f;
            this.csdgList.Columns["fpdm"].FillWeight = 20f;
            this.csdgList.Columns["qshm"].FillWeight = 20f;
            this.csdgList.Columns["fpzs"].FillWeight = 15f;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.data_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_jsrq");
            this.data_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ksrq");
            this.chkQS = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkQS");
            this.chkJZ = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkJZ");
            this.btnQuery = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_cx");
            this.btnSgtj = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_sgtj");
            this.btnFpxz = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_fpxz");
            this.csdgList = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("csdgList");
            this.chkQS.CheckedChanged += new EventHandler(this.chkQS_CheckedChanged);
            this.chkJZ.CheckedChanged += new EventHandler(this.chkJZ_CheckedChanged);
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.btnSgtj.Click += new EventHandler(this.btnSgtj_Click);
            this.btnFpxz.Click += new EventHandler(this.btnFpxz_Click);
            this.gridSetting();
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(ListDown));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(640, 330);
            this.xmlComponentLoader1.TabIndex = 3;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.ListDown\Aisino.Fwkp.Fplygl.Forms.ListDown.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(640, 330);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Location = new Point(0, 0);
            base.MinimizeBox = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Name = "ListReq";
            base.set_TabText("ListReq");
            this.Text = "网上领票";
            base.ResumeLayout(false);
        }

        private void InsertData(List<InvVolumeApp> ListModel)
        {
            try
            {
                if (this.csdgList.DataSource != null)
                {
                    ((DataTable) this.csdgList.DataSource).Clear();
                }
                if ((ListModel == null) || (ListModel.Count <= 0))
                {
                    MessageManager.ShowMsgBox("INP-441242");
                }
                else
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("xz", typeof(bool));
                    table.Columns.Add("fpzl", typeof(string));
                    table.Columns.Add("fpdm", typeof(string));
                    table.Columns.Add("qshm", typeof(string));
                    table.Columns.Add("fpzs", typeof(string));
                    foreach (InvVolumeApp app in ListModel)
                    {
                        DataRow row = table.NewRow();
                        row["xz"] = true;
                        row["fpzl"] = ShareMethods.GetInvType(app.InvType);
                        row["fpdm"] = app.TypeCode;
                        row["qshm"] = ShareMethods.FPHMTo8Wei(app.HeadCode);
                        row["fpzs"] = Convert.ToString(app.Number);
                        table.Rows.Add(row);
                    }
                    this.csdgList.DataSource = table;
                    this.btnFpxz.Enabled = true;
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

        private bool OutDownList()
        {
            this.reqInvList.Clear();
            foreach (DataGridViewRow row in (IEnumerable) this.csdgList.Rows)
            {
                if ((bool) row.Cells["xz"].Value)
                {
                    InvVolumeApp item = new InvVolumeApp {
                        InvType = this.Str2InvType(row.Cells["fpzl"].Value.ToString()),
                        TypeCode = row.Cells["fpdm"].Value.ToString(),
                        HeadCode = Convert.ToUInt32(row.Cells["qshm"].Value.ToString()),
                        Number = Convert.ToUInt16(row.Cells["fpzs"].Value.ToString())
                    };
                    this.reqInvList.Add(item);
                }
            }
            if (this.reqInvList.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-441243");
                return false;
            }
            return true;
        }

        private string RequestListInput()
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
            XmlElement element7 = document.CreateElement("FSSJ_Q");
            if (this.chkQS.Checked)
            {
                element7.InnerText = this.data_ksrq.Value.ToString("yyyyMMdd");
            }
            else
            {
                element7.InnerText = string.Empty;
            }
            element2.AppendChild(element7);
            XmlElement element8 = document.CreateElement("FSSJ_Z");
            if (this.chkJZ.Checked)
            {
                element8.InnerText = this.data_jsrq.Value.ToString("yyyyMMdd");
            }
            else
            {
                element8.InnerText = string.Empty;
            }
            element2.AppendChild(element8);
            XmlElement element9 = document.CreateElement("QYXZ");
            element9.InnerText = base.TaxCardInstance.get_SQInfo().DHYBZ;
            element2.AppendChild(element9);
            document.PreserveWhitespace = true;
            if (this.logFlag)
            {
                document.Save(this.logPath + @"\RequestListInput.xml");
            }
            return document.InnerXml;
        }

        private List<InvVolumeApp> RequestListOutput(XmlDocument invList)
        {
            XmlNode node = invList.SelectSingleNode("//CODE");
            XmlNode node2 = invList.SelectSingleNode("//MESS");
            List<InvVolumeApp> list = new List<InvVolumeApp>();
            if (node.InnerText.Equals("0000"))
            {
                InvVolumeApp locked = null;
                InvVolumeApp app2 = null;
                UnlockInvoice invoice = null;
                invoice = base.TaxCardInstance.NInvGetUnlockInvoice(0);
                if (!DownloadCommon.CheckEmpty(invoice.Buffer))
                {
                    locked = new InvVolumeApp {
                        InvType = Convert.ToByte(invoice.get_Kind()),
                        TypeCode = invoice.get_TypeCode(),
                        HeadCode = Convert.ToUInt32(invoice.get_Number()),
                        Number = Convert.ToUInt16(invoice.get_Count())
                    };
                }
                invoice = base.TaxCardInstance.NInvGetUnlockInvoice(11);
                if (!DownloadCommon.CheckEmpty(invoice.Buffer))
                {
                    app2 = new InvVolumeApp {
                        InvType = Convert.ToByte(invoice.get_Kind()),
                        TypeCode = invoice.get_TypeCode(),
                        HeadCode = Convert.ToUInt32(invoice.get_Number()),
                        Number = Convert.ToUInt16(invoice.get_Count())
                    };
                }
                foreach (XmlNode node4 in invList.SelectSingleNode("//DATA").ChildNodes)
                {
                    XmlElement element = (XmlElement) node4;
                    if (element.Name.Equals("FPMX"))
                    {
                        bool flag = false;
                        InvVolumeApp tarInv = new InvVolumeApp();
                        foreach (XmlNode node5 in element.ChildNodes)
                        {
                            if (node5.Name.Equals("FPZL"))
                            {
                                int item = Convert.ToByte(node5.InnerText);
                                if (-1 == this.availableType.IndexOf(item))
                                {
                                    flag = true;
                                    break;
                                }
                                tarInv.InvType = Convert.ToByte(node5.InnerText);
                            }
                            else if (node5.Name.Equals("LBDM"))
                            {
                                tarInv.TypeCode = node5.InnerText;
                            }
                            else if (node5.Name.Equals("QSHM"))
                            {
                                tarInv.HeadCode = Convert.ToUInt32(node5.InnerText);
                            }
                            else if (node5.Name.Equals("FPFS"))
                            {
                                tarInv.Number = Convert.ToUInt16(node5.InnerText);
                            }
                            else if (node5.Name.Equals("GMRQ"))
                            {
                                string innerText = node5.InnerText;
                                int year = Convert.ToInt32(innerText.Substring(0, 4));
                                int month = Convert.ToInt32(innerText.Substring(4, 2));
                                int day = Convert.ToInt32(innerText.Substring(6, 2));
                                int hour = Convert.ToInt32(innerText.Substring(8, 2));
                                int minute = Convert.ToInt32(innerText.Substring(10, 2));
                                int second = Convert.ToInt32(innerText.Substring(12, 2));
                                DateTime time = new DateTime(year, month, day, hour, minute, second);
                                tarInv.BuyDate = time;
                            }
                        }
                        if (!flag)
                        {
                            if (ShareMethods.IsHXInv(tarInv.InvType) && ((locked == null) || !DownloadCommon.CheckRepeat(locked, tarInv)))
                            {
                                list.Add(tarInv);
                            }
                            else if (ShareMethods.IsZCInv(tarInv.InvType) && ((app2 == null) || !DownloadCommon.CheckRepeat(app2, tarInv)))
                            {
                                list.Add(tarInv);
                            }
                        }
                    }
                }
                return list;
            }
            MessageManager.ShowMsgBox(node.InnerText + ":" + node2);
            return list;
        }

        private void SetDataCtrlAttritute()
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

        private byte Str2InvType(string str)
        {
            switch (str)
            {
                case "增值税专用发票":
                    return 0;

                case "增值税普通发票":
                    return 2;

                case "货物运输业增值税专用发票":
                    return 11;

                case "机动车销售统一发票":
                    return 12;

                case "电子增值税普通发票":
                    return 0x33;

                case "增值税普通发票(卷票)":
                    return 0x29;
            }
            return 0xff;
        }
    }
}

