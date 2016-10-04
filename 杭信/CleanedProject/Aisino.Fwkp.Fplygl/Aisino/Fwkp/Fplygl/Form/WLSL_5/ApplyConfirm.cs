namespace Aisino.Fwkp.Fplygl.Form.WLSL_5
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;

    public class ApplyConfirm : DockForm
    {
        private string applyNum = string.Empty;
        private AisinoBTN btnCancel;
        private AisinoBTN btnConfirm;
        private IContainer components;
        private CustomStyleDataGrid confirmList;
        private string invType = string.Empty;
        private bool logFlag;
        private string logPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private ToolStripButton tool_AddRow;
        private ToolStripButton tool_Close;
        private ToolStripButton tool_Confirm;
        private ToolStripButton tool_DeleteRow;
        private ToolStrip toolStrip;
        protected XmlComponentLoader xmlComponentLoader1;

        public ApplyConfirm(string slxh, string type)
        {
            this.Initialize();
            this.invType = type;
            this.applyNum = slxh;
            this.tool_AddRow_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.confirmList.SelectedRows.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-441209", new string[] { "待确认" });
            }
            else
            {
                this.confirmList.CurrentCell = this.confirmList.Rows[0].Cells[0];
                this.confirmList.CurrentCell = this.confirmList.Rows[0].Cells[1];
                int lineNum = 0;
                string emptyName = string.Empty;
                if (this.contentEmptyCheck(out lineNum, out emptyName))
                {
                    MessageManager.ShowMsgBox("INP-4412A9", new string[] { lineNum.ToString(), emptyName });
                }
                else
                {
                    XmlDocument document = null;
                    bool flag2 = false;
                    if (ApplyCommon.IsHxInvType(this.invType))
                    {
                        document = this.CreateHXConfirmInput();
                        if (this.logFlag)
                        {
                            document.Save(this.logPath + "HxConfirmInput.xml");
                        }
                        string xml = string.Empty;
                        if (HttpsSender.SendMsg("0038", document.InnerXml, ref xml) != 0)
                        {
                            MessageManager.ShowMsgBox(xml);
                            return;
                        }
                        XmlDocument docFeedback = new XmlDocument();
                        docFeedback.LoadXml(xml);
                        if (this.logFlag)
                        {
                            docFeedback.Save(this.logPath + @"\HxConfirmOutput.xml");
                        }
                        string msg = string.Empty;
                        bool flag3 = this.ParseHXConfirmOutput(docFeedback, out msg);
                        flag2 = flag3;
                        if (!flag3)
                        {
                            MessageManager.ShowMsgBox(msg);
                            return;
                        }
                    }
                    else if (ApplyCommon.IsZcInvType(this.invType))
                    {
                        document = this.CreateZCConfirmInput();
                        if (this.logFlag)
                        {
                            document.Save(this.logPath + "ZcConfirmInput.xml");
                        }
                        string str4 = string.Empty;
                        if (HttpsSender.SendMsg("0039", document.InnerXml, ref str4) != 0)
                        {
                            MessageManager.ShowMsgBox(str4);
                            return;
                        }
                        XmlDocument document3 = new XmlDocument();
                        document3.LoadXml(str4);
                        if (this.logFlag)
                        {
                            document3.Save(this.logPath + @"\ZcConfirmOutput.xml");
                        }
                        string str5 = string.Empty;
                        bool flag4 = this.ParseZCConfirmOutput(document3, out str5);
                        flag2 = flag4;
                        if (!flag4)
                        {
                            MessageManager.ShowMsgBox(str5);
                            return;
                        }
                    }
                    if (flag2)
                    {
                        base.DialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        base.DialogResult = DialogResult.No;
                    }
                    base.Close();
                }
            }
        }

        private void confirmList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox control = (TextBox) e.Control;
            control.KeyPress += new KeyPressEventHandler(this.dgvTxt_KeyPress);
        }

        private bool contentEmptyCheck(out int lineNum, out string emptyName)
        {
            lineNum = 0;
            emptyName = string.Empty;
            bool flag = false;
            List<string> list = new List<string>();
            for (int i = 0; i < this.confirmList.Rows.Count; i++)
            {
                DataGridViewRow row = this.confirmList.Rows[i];
                if (((row.Cells["FPDM"].Value == null) || (row.Cells["QSHM"].Value == null)) || (row.Cells["ZZHM"].Value == null))
                {
                    lineNum = i + 1;
                    flag = true;
                    if (row.Cells["FPDM"].Value == null)
                    {
                        list.Add("发票代码");
                    }
                    if (row.Cells["QSHM"].Value == null)
                    {
                        list.Add("起始号码");
                    }
                    if (row.Cells["ZZHM"].Value == null)
                    {
                        list.Add("终止号码");
                    }
                    emptyName = string.Join("、", list.ToArray());
                    return flag;
                }
            }
            return flag;
        }

        private XmlDocument CreateHXConfirmInput()
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
            element4.InnerText = this.applyNum;
            element2.AppendChild(element4);
            foreach (DataGridViewRow row in (IEnumerable) this.confirmList.Rows)
            {
                XmlElement element5 = document.CreateElement("QRXX");
                element2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("FPDM");
                element6.InnerText = row.Cells["FPDM"].Value.ToString();
                element5.AppendChild(element6);
                XmlElement element7 = document.CreateElement("QSHM");
                element7.InnerText = row.Cells["QSHM"].Value.ToString();
                element5.AppendChild(element7);
                XmlElement element8 = document.CreateElement("ZZHM");
                element8.InnerText = row.Cells["ZZHM"].Value.ToString();
                element5.AppendChild(element8);
            }
            document.PreserveWhitespace = true;
            return document;
        }

        private XmlDocument CreateZCConfirmInput()
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("business");
            element.SetAttribute("id", "fp_jsqr");
            element.SetAttribute("comment", "纸质票接收确认");
            document.AppendChild(element);
            XmlElement element2 = document.CreateElement("body");
            element.AppendChild(element2);
            XmlElement element3 = document.CreateElement("nsrsbh");
            element3.InnerText = base.TaxCardInstance.get_TaxCode();
            element2.AppendChild(element3);
            XmlElement element4 = document.CreateElement("slxh");
            element4.InnerText = this.applyNum;
            element2.AppendChild(element4);
            foreach (DataGridViewRow row in (IEnumerable) this.confirmList.Rows)
            {
                XmlElement element5 = document.CreateElement("qrxx");
                element2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("fpdm");
                element6.InnerText = row.Cells["FPDM"].Value.ToString();
                element5.AppendChild(element6);
                int num = Convert.ToInt32(row.Cells["QSHM"].Value);
                XmlElement element7 = document.CreateElement("qshm");
                element7.InnerText = row.Cells["QSHM"].Value.ToString();
                element5.AppendChild(element7);
                int num2 = Convert.ToInt32(row.Cells["ZZHM"].Value);
                XmlElement element8 = document.CreateElement("zzhm");
                element8.InnerText = row.Cells["ZZHM"].Value.ToString();
                element5.AppendChild(element8);
                XmlElement element9 = document.CreateElement("fpsl");
                element9.InnerText = ((num2 - num) + 1).ToString();
                element5.AppendChild(element9);
            }
            document.PreserveWhitespace = true;
            return document;
        }

        private void dgvTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox box;
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
                return;
            }
            if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
            int num = 0;
            string name = this.confirmList.CurrentCell.OwningColumn.Name;
            if (name != null)
            {
                if (!(name == "FPDM"))
                {
                    if ((name == "QSHM") || (name == "ZZHM"))
                    {
                        num = 8;
                        goto Label_00A3;
                    }
                }
                else
                {
                    if ("005" == this.invType)
                    {
                        num = 12;
                    }
                    else
                    {
                        num = 10;
                    }
                    goto Label_00A3;
                }
            }
            num = 12;
        Label_00A3:
            box = (TextBox) sender;
            if ((box.Text.Length >= num) && (box.SelectedText.Length == 0))
            {
                e.Handled = true;
            }
        }

        private void dgvTxt_MouseDown(object sender, MouseEventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void gridSetting()
        {
            this.confirmList.AllowUserToDeleteRows = false;
            this.confirmList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.confirmList.Columns["FPZS"].Visible = false;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.toolStrip = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip");
            this.tool_Close = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Close");
            this.tool_Confirm = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Confirm");
            this.tool_AddRow = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_AddRow");
            this.tool_DeleteRow = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_DeleteRow");
            this.btnConfirm = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnConfirm");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.confirmList = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("csdgList");
            this.tool_Close.Click += new EventHandler(this.tool_Close_Click);
            this.tool_Confirm.Click += new EventHandler(this.tool_Confirm_Click);
            this.tool_AddRow.Click += new EventHandler(this.tool_AddRow_Click);
            this.tool_DeleteRow.Click += new EventHandler(this.tool_DeleteRow_Click);
            this.btnConfirm.Click += new EventHandler(this.btnConfirm_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.confirmList.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.confirmList_EditingControlShowing);
            this.tool_Close.Margin = new Padding(20, 1, 0, 2);
            ControlStyleUtil.SetToolStripStyle(this.toolStrip);
            this.gridSetting();
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(ApplyConfirm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1ac, 0x153);
            this.xmlComponentLoader1.TabIndex = 5;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.WebWindows.ApplyConfirm\Aisino.Fwkp.Fplygl.Forms.WebWindows.ApplyConfirm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1ac, 0x153);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ApplyConfirm";
            base.set_TabText("ApplyConfirm");
            this.Text = "确认信息录入";
            base.ResumeLayout(false);
        }

        private bool ParseHXConfirmOutput(XmlDocument docFeedback, out string msg)
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

        private bool ParseZCConfirmOutput(XmlDocument docFeedback, out string msg)
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

        private void tool_AddRow_Click(object sender, EventArgs e)
        {
            this.confirmList.Rows.Add();
        }

        private void tool_Close_Click(object sender, EventArgs e)
        {
            this.btnCancel_Click(null, null);
        }

        private void tool_Confirm_Click(object sender, EventArgs e)
        {
            this.btnConfirm_Click(null, null);
        }

        private void tool_DeleteRow_Click(object sender, EventArgs e)
        {
            if (this.confirmList.Rows.Count > 1)
            {
                this.confirmList.Rows.RemoveAt(this.confirmList.CurrentRow.Index);
            }
            else
            {
                MessageManager.ShowMsgBox("INP-4412AC");
            }
        }
    }
}

