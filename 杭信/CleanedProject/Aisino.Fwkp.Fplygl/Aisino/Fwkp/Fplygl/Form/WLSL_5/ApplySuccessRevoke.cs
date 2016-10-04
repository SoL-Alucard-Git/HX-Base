namespace Aisino.Fwkp.Fplygl.Form.WLSL_5
{
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;

    public class ApplySuccessRevoke : ApplySuccessParent
    {
        private string cofPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Bin\");
        private IContainer components;
        private ILog loger = LogUtil.GetLogger<ApplySuccessRevoke>();
        private bool logFlag;
        private string logPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");

        public ApplySuccessRevoke(out bool dataExist)
        {
            this.Initialize();
            this.ShowListWithFilteredQuery();
            if (base.volumeList.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-4412AH");
                dataExist = false;
            }
            else
            {
                base.BindData(base.volumeList);
                dataExist = true;
            }
        }

        private XmlDocument CreateHxRevokeInput(string applySeqNum, string applyTypeCode, string applyAmount)
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
            XmlElement element4 = document.CreateElement("SLXH");
            element4.InnerText = applySeqNum;
            element2.AppendChild(element4);
            XmlElement element5 = document.CreateElement("FPZLDM");
            element5.InnerText = applyTypeCode;
            element2.AppendChild(element5);
            XmlElement element6 = document.CreateElement("SLSL");
            element6.InnerText = applyAmount;
            element2.AppendChild(element6);
            document.PreserveWhitespace = true;
            return document;
        }

        private XmlDocument CreateZcRevokeInput(string applySeqNum)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("business");
            element.SetAttribute("id", "fp_cxsl");
            element.SetAttribute("comment", "撤销网上申领");
            document.AppendChild(element);
            XmlElement element2 = document.CreateElement("body");
            element.AppendChild(element2);
            XmlElement element3 = document.CreateElement("nsrsbh");
            element3.InnerText = base.TaxCardInstance.get_TaxCode();
            element2.AppendChild(element3);
            XmlElement element4 = document.CreateElement("slxh");
            element4.InnerText = applySeqNum;
            element2.AppendChild(element4);
            document.PreserveWhitespace = true;
            return document;
        }

        private void csdgStatusVolumn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridViewRow row = base.csdgStatusVolumn.SelectedRows[0];
            string applySeqNum = row.Cells["SLXH"].Value.ToString();
            string applyTypeCode = row.Cells["FPZLDM"].Value.ToString();
            string applyAmount = row.Cells["SLSL"].Value.ToString();
            string invTypeName = ApplyCommon.Invtype2CodeMix(row.Cells["FPZL"].Value.ToString());
            if ((DialogResult.Yes == MessageManager.ShowMsgBox("INP-4412AA")) && this.RevokeVolumes(invTypeName, applySeqNum, applyTypeCode, applyAmount))
            {
                MessageManager.ShowMsgBox("INP-4412AJ");
                base.volumeList.RemoveAt(base.csdgStatusVolumn.SelectedRows[0].Index);
                base.BindData(base.volumeList);
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
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "申领撤销";
            base.tool_Revoke.Visible = true;
            base.csdgStatusVolumn.MouseDoubleClick += new MouseEventHandler(this.csdgStatusVolumn_MouseDoubleClick);
        }

        private bool ParseHxRevokeOutput(XmlDocument docFeedback, out string msg)
        {
            XmlNode node = docFeedback.SelectSingleNode("//CODE");
            XmlNode node2 = docFeedback.SelectSingleNode("//MESS");
            if (!node.InnerText.Equals("0000"))
            {
                msg = node2.InnerText;
                return false;
            }
            msg = string.Empty;
            return true;
        }

        private bool ParseZcRevokeOutput(XmlDocument docFeedback, out string msg)
        {
            XmlNode node = docFeedback.SelectSingleNode("//returnCode");
            XmlNode node2 = docFeedback.SelectSingleNode("//returnMessage");
            if (!node.InnerText.Equals("00"))
            {
                msg = node2.InnerText;
                return false;
            }
            msg = string.Empty;
            return true;
        }

        private bool RevokeVolumes(string invTypeName, string applySeqNum, string applyTypeCode, string applyAmount)
        {
            if (ApplyCommon.IsHxInvType(invTypeName))
            {
                XmlDocument document = this.CreateHxRevokeInput(applySeqNum, applyTypeCode, applyAmount);
                if (this.logFlag)
                {
                    document.Save(this.logPath + "HxRevokeInput.xml");
                }
                string xml = string.Empty;
                if (HttpsSender.SendMsg("0041", document.InnerXml, ref xml) != 0)
                {
                    MessageManager.ShowMsgBox(xml);
                    return false;
                }
                XmlDocument docFeedback = new XmlDocument();
                docFeedback.LoadXml(xml);
                if (this.logFlag)
                {
                    docFeedback.Save(this.logPath + @"\HxRevokeOutput.xml");
                }
                string msg = string.Empty;
                if (!this.ParseHxRevokeOutput(docFeedback, out msg))
                {
                    MessageManager.ShowMsgBox(msg);
                    return false;
                }
                return true;
            }
            if (ApplyCommon.IsZcInvType(invTypeName))
            {
                XmlDocument document3 = this.CreateZcRevokeInput(applySeqNum);
                if (this.logFlag)
                {
                    document3.Save(this.logPath + "ZcRevokeInput.xml");
                }
                string str3 = string.Empty;
                if (HttpsSender.SendMsg("0042", document3.InnerXml, ref str3) != 0)
                {
                    MessageManager.ShowMsgBox(str3);
                    return false;
                }
                XmlDocument document4 = new XmlDocument();
                document4.LoadXml(str3);
                if (this.logFlag)
                {
                    document4.Save(this.logPath + @"\ZcRevokeOutput.xml");
                }
                string str4 = string.Empty;
                if (!this.ParseZcRevokeOutput(document4, out str4))
                {
                    MessageManager.ShowMsgBox(str4);
                    return false;
                }
                return true;
            }
            MessageManager.ShowMsgBox("INP-441200");
            return false;
        }

        protected override void ShowListWithFilteredQuery()
        {
            QueryCondition qCondition = new QueryCondition {
                startTime = string.Empty,
                endTime = string.Empty,
                invType = string.Empty,
                status = string.Empty
            };
            List<OneTypeVolumes> queryList = new List<OneTypeVolumes>();
            QueryConfirmCommon.QueryController(qCondition, queryList, false);
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string> { "1", "0" };
            foreach (OneTypeVolumes volumes in queryList)
            {
                string type = ApplyCommon.Invtype2CodeMix(volumes.invType);
                if (ApplyCommon.IsHxInvType(type))
                {
                    if (list2.IndexOf(volumes.applyStatus) != -1)
                    {
                        base.volumeList.Add(volumes);
                    }
                }
                else if (ApplyCommon.IsZcInvType(type) && (list3.IndexOf(volumes.applyStatus) != -1))
                {
                    base.volumeList.Add(volumes);
                }
            }
        }

        protected override void tool_Revoke_Click(object sender, EventArgs e)
        {
            if (base.csdgStatusVolumn.SelectedRows.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-441209", new string[] { "待撤回" });
            }
            else
            {
                this.csdgStatusVolumn_MouseDoubleClick(null, null);
            }
        }
    }
}

