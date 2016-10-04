namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class DanJuExcelCR : BaseForm
    {
        private AisinoBTN btnCR;
        private AisinoBTN btnSet;
        private AisinoBTN button3;
        private AisinoCHK checkBox_HYSYDJ;
        private AisinoCMB comboBoxJESM;
        private IContainer components = null;
        private FileControl fileControl1;
        private FileControl fileControl2;
        private AisinoGRP groupBox1;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoRDO radioBtnHYFP;
        private AisinoRDO radioBtnJDCFP;
        private AisinoRDO radioBtnPTFP;
        private AisinoRDO radioBtnZYFP;
        private XmlComponentLoader xmlComponentLoader1;

        public DanJuExcelCR()
        {
            this.Initialize();
            DataTable table = new DataTable();
            table.Columns.Add("Display", typeof(string));
            table.Columns.Add("Value", typeof(string));
            table.Rows.Add(new object[] { "含税金额", "HanShui" });
            table.Rows.Add(new object[] { "不含税金额", "BuHanShui" });
            this.comboBoxJESM.DataSource = table;
            this.comboBoxJESM.DisplayMember = "Display";
            this.comboBoxJESM.ValueMember = "Value";
        }

        private void btnCR_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(this.fileControl1.get_TextBoxFile().Text.Trim()))
                {
                    MessageManager.ShowMsgBox("INP-271201");
                }
                else
                {
                    string text = this.fileControl1.get_TextBoxFile().Text;
                    string str2 = this.fileControl2.get_TextBoxFile().Text;
                    PriceType priceType = (PriceType) Enum.Parse(typeof(PriceType), this.comboBoxJESM.SelectedValue.ToString());
                    InvType common = InvType.Common;
                    if (this.radioBtnPTFP.Checked)
                    {
                        common = InvType.Common;
                        IniRead.type = "c";
                    }
                    else if (this.radioBtnZYFP.Checked)
                    {
                        common = InvType.Special;
                        IniRead.type = "s";
                    }
                    else if (this.radioBtnHYFP.Checked)
                    {
                        common = InvType.transportation;
                        IniRead.type = "f";
                    }
                    else if (this.radioBtnJDCFP.Checked)
                    {
                        common = InvType.vehiclesales;
                        IniRead.type = "v";
                    }
                    if ((PropValue.SingleDoubleTable == "2") && !File.Exists(this.fileControl2.get_TextBoxFile().Text.Trim()))
                    {
                        MessageManager.ShowMsgBox("INP-271202");
                    }
                    else
                    {
                        PropValue.ExcelFile1Path = text;
                        PropValue.ExcelFile2Path = str2;
                        PropValue.ExcelAmountType = this.comboBoxJESM.SelectedValue.ToString();
                        if (this.radioBtnPTFP.Checked)
                        {
                            PropValue.ExcelInvType = "Common";
                        }
                        else if (this.radioBtnZYFP.Checked)
                        {
                            PropValue.ExcelInvType = "Special";
                        }
                        else if (this.radioBtnHYFP.Checked)
                        {
                            PropValue.ExcelInvType = "transportation";
                        }
                        else if (this.radioBtnJDCFP.Checked)
                        {
                            PropValue.ExcelInvType = "vehiclesales";
                        }
                        FatchSaleBill bill = new FatchSaleBill();
                        new ResultForm(bill.ImportSaleBillExcel(text, str2, priceType, common)).ShowDialog();
                    }
                }
            }
            catch (CustomException exception)
            {
                MessageManager.ShowMsgBox(exception.Message);
            }
            catch (Exception exception2)
            {
                HandleException.HandleError(exception2);
            }
            finally
            {
                base.Close();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                InvType common = InvType.Common;
                if (this.radioBtnPTFP.Checked)
                {
                    common = InvType.Common;
                    IniRead.type = "c";
                }
                else if (this.radioBtnZYFP.Checked)
                {
                    common = InvType.Special;
                    IniRead.type = "s";
                }
                else if (this.radioBtnHYFP.Checked)
                {
                    common = InvType.transportation;
                    IniRead.type = "f";
                }
                else if (this.radioBtnJDCFP.Checked)
                {
                    common = InvType.vehiclesales;
                    IniRead.type = "j";
                }
                ExcelPassword password = new ExcelPassword();
                password.ShowDialog();
                if (password.DialogResult == DialogResult.Yes)
                {
                    new ExcelSetForm(common).ShowDialog();
                }
                else
                {
                    base.Close();
                    return;
                }
                this.fileControl1.get_TextBoxFile().Text = PropValue.ExcelFile1Path;
                this.fileControl2.get_TextBoxFile().Text = PropValue.ExcelFile2Path;
                if (PropValue.SingleDoubleTable == "1")
                {
                    this.label2.Visible = false;
                    this.fileControl2.Visible = false;
                    this.label1.Text = "传入路径";
                }
                else if (PropValue.SingleDoubleTable == "2")
                {
                    this.label2.Visible = true;
                    this.fileControl2.Visible = true;
                    this.label1.Text = "主表路径";
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void DanJuExcelCR_Load(object sender, EventArgs e)
        {
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                this.fileControl1.set_FileFilter(FatchSaleBill.FileFilterExcel);
                this.fileControl2.set_FileFilter(FatchSaleBill.FileFilterExcel);
                bool flag = false;
                if (PropValue.ExcelInvType == "Special")
                {
                    if (card.get_QYLX().ISZYFP)
                    {
                        IniRead.type = "s";
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else if (PropValue.ExcelInvType == "Common")
                {
                    if (card.get_QYLX().ISPTFP)
                    {
                        IniRead.type = "c";
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else if (PropValue.ExcelInvType == "transportation")
                {
                    if (card.get_QYLX().ISHY)
                    {
                        IniRead.type = "f";
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else if (PropValue.ExcelInvType == "vehiclesales")
                {
                    if (card.get_QYLX().ISJDC)
                    {
                        IniRead.type = "j";
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    if (card.get_QYLX().ISPTFP)
                    {
                        IniRead.type = "c";
                        PropValue.ExcelInvType = "Common";
                    }
                    else if (card.get_QYLX().ISZYFP)
                    {
                        IniRead.type = "s";
                        PropValue.ExcelInvType = "Special";
                    }
                    else if (card.get_QYLX().ISHY)
                    {
                        IniRead.type = "f";
                        PropValue.ExcelInvType = "transportation";
                    }
                    else if (card.get_QYLX().ISJDC)
                    {
                        IniRead.type = "v";
                        PropValue.ExcelInvType = "vehiclesales";
                    }
                }
                this.fileControl1.get_TextBoxFile().Text = PropValue.ExcelFile1Path;
                this.fileControl2.get_TextBoxFile().Text = PropValue.ExcelFile2Path;
                this.comboBoxJESM.SelectedValue = PropValue.ExcelAmountType;
                this.radioBtnZYFP.Checked = PropValue.ExcelInvType == "Special";
                this.radioBtnPTFP.Checked = PropValue.ExcelInvType == "Common";
                this.radioBtnHYFP.Checked = PropValue.ExcelInvType == "transportation";
                this.radioBtnJDCFP.Checked = PropValue.ExcelInvType == "vehiclesales";
                if (PropValue.SingleDoubleTable == "1")
                {
                    this.label2.Visible = false;
                    this.fileControl2.Visible = false;
                    this.label1.Text = "传入路径";
                }
                else if (PropValue.SingleDoubleTable == "2")
                {
                    this.label2.Visible = true;
                    this.fileControl2.Visible = true;
                    this.label1.Text = "主表路径";
                }
                this.radioBtnPTFP.Visible = false;
                this.radioBtnZYFP.Visible = false;
                this.radioBtnHYFP.Visible = false;
                this.radioBtnJDCFP.Visible = false;
                int num = 0;
                if (card.get_QYLX().ISZYFP)
                {
                    this.radioBtnZYFP.Visible = true;
                    num++;
                }
                if (card.get_QYLX().ISPTFP)
                {
                    this.radioBtnPTFP.Visible = true;
                    if (num == 0)
                    {
                        this.radioBtnPTFP.Location = new Point(7, 0x15);
                    }
                    num++;
                }
                if (card.get_QYLX().ISHY)
                {
                    this.radioBtnHYFP.Visible = true;
                    if (num == 0)
                    {
                        this.radioBtnHYFP.Location = new Point(7, 0x15);
                    }
                    else if (num == 1)
                    {
                        this.radioBtnHYFP.Location = new Point(7, 50);
                    }
                    num++;
                }
                if (card.get_QYLX().ISJDC)
                {
                    this.radioBtnJDCFP.Visible = true;
                    switch (num)
                    {
                        case 0:
                            this.radioBtnJDCFP.Location = new Point(7, 0x15);
                            break;

                        case 1:
                            this.radioBtnJDCFP.Location = new Point(7, 50);
                            break;

                        case 2:
                            this.radioBtnJDCFP.Location = new Point(0x7a, 0x15);
                            break;
                    }
                }
                if (this.radioBtnJDCFP.Checked)
                {
                    this.comboBoxJESM.SelectedIndex = 0;
                    this.comboBoxJESM.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
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
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.radioBtnZYFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioBtnZYFP");
            this.radioBtnPTFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioBtnPTFP");
            this.radioBtnHYFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioBtnHYFP");
            this.radioBtnJDCFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioBtnJDCFP");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.comboBoxJESM = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxJESM");
            this.comboBoxJESM.DropDownStyle = ComboBoxStyle.DropDownList;
            this.fileControl2 = this.xmlComponentLoader1.GetControlByName<FileControl>("fileControl2");
            this.button3 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button3");
            this.btnCR = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCR");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.btnSet = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnSet");
            this.fileControl1 = this.xmlComponentLoader1.GetControlByName<FileControl>("fileControl1");
            this.checkBox_HYSYDJ = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox_HYSYDJ");
            this.button3.Click += new EventHandler(this.btnQuit_Click);
            this.btnCR.Click += new EventHandler(this.btnCR_Click);
            this.btnSet.Click += new EventHandler(this.btnSet_Click);
            this.radioBtnPTFP.CheckedChanged += new EventHandler(this.radioBtnPTFP_CheckedChanged);
            this.radioBtnZYFP.CheckedChanged += new EventHandler(this.radioBtnZYFP_CheckedChanged);
            this.radioBtnHYFP.CheckedChanged += new EventHandler(this.radioBtnHYFP_checkedChanged);
            this.radioBtnJDCFP.CheckedChanged += new EventHandler(this.radioBtnJDCFP_CheckedChanged);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DanJuExcelCR));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x23c, 0x105);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "从Excel传入";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.DanJuExcelCR\Aisino.Fwkp.Wbjk.DanJuExcelCR.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x23c, 0x105);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DanJuExcelCR";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "从Excel传入";
            base.Load += new EventHandler(this.DanJuExcelCR_Load);
            base.ResumeLayout(false);
        }

        private List<List<string>> QZSQ_Split(string SQ)
        {
            string str = ",";
            string str2 = "-";
            List<List<string>> list = new List<List<string>>();
            List<string> list2 = new List<string>();
            string[] strArray = SQ.Split(new string[] { str }, StringSplitOptions.None);
            foreach (string str3 in strArray)
            {
                list2.Add(str3);
            }
            int count = list2.Count;
            for (int i = 0; i < count; i++)
            {
                string str4 = list2[i];
                List<string> item = new List<string>();
                string[] strArray2 = str4.Split(new string[] { str2 }, StringSplitOptions.None);
                foreach (string str5 in strArray2)
                {
                    item.Add(str5);
                }
                list.Add(item);
            }
            return list;
        }

        private void radioBtnHYFP_checkedChanged(object sender, EventArgs e)
        {
            IniRead.type = "f";
            if (PropValue.SingleDoubleTable == "1")
            {
                this.label2.Visible = false;
                this.fileControl2.Visible = false;
                this.label1.Text = "传入路径";
            }
            else if (PropValue.SingleDoubleTable == "2")
            {
                this.label2.Visible = true;
                this.fileControl2.Visible = true;
                this.label1.Text = "主表路径";
            }
        }

        private void radioBtnJDCFP_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioBtnJDCFP.Checked)
            {
                this.comboBoxJESM.SelectedIndex = 0;
                this.comboBoxJESM.Enabled = false;
            }
            else
            {
                this.comboBoxJESM.Enabled = true;
            }
            IniRead.type = "j";
            if (PropValue.SingleDoubleTable == "1")
            {
                this.label2.Visible = false;
                this.fileControl2.Visible = false;
                this.label1.Text = "传入路径";
            }
            else if (PropValue.SingleDoubleTable == "2")
            {
                this.label2.Visible = true;
                this.fileControl2.Visible = true;
                this.label1.Text = "主表路径";
            }
        }

        private void radioBtnPTFP_CheckedChanged(object sender, EventArgs e)
        {
            IniRead.type = "c";
            if (PropValue.SingleDoubleTable == "1")
            {
                this.label2.Visible = false;
                this.fileControl2.Visible = false;
                this.label1.Text = "传入路径";
            }
            else if (PropValue.SingleDoubleTable == "2")
            {
                this.label2.Visible = true;
                this.fileControl2.Visible = true;
                this.label1.Text = "主表路径";
            }
        }

        private void radioBtnZYFP_CheckedChanged(object sender, EventArgs e)
        {
            IniRead.type = "s";
            if (PropValue.SingleDoubleTable == "1")
            {
                this.label2.Visible = false;
                this.fileControl2.Visible = false;
                this.label1.Text = "传入路径";
            }
            else if (PropValue.SingleDoubleTable == "2")
            {
                this.label2.Visible = true;
                this.fileControl2.Visible = true;
                this.label1.Text = "主表路径";
            }
        }
    }
}

