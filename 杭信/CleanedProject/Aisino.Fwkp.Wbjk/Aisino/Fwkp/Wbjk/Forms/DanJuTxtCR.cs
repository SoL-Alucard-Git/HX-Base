namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class DanJuTxtCR : BaseForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoCHK checkBox_HYSYDJ;
        private AisinoCMB comboBoxJESM;
        private IContainer components = null;
        private FileControl fileControl1;
        private AisinoGRP groupBox1;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private ILog log = LogUtil.GetLogger<DanJuTxtCR>();
        private AisinoRDO radioBtnHYFP;
        private AisinoRDO radioBtnJDCFP;
        private AisinoRDO radioBtnPTFP;
        private AisinoRDO radioBtnZYFP;
        private XmlComponentLoader xmlComponentLoader1;

        public DanJuTxtCR()
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                string path = this.fileControl1.get_TextBoxFile().Text.Trim();
                PriceType priceType = (PriceType) Enum.Parse(typeof(PriceType), this.comboBoxJESM.SelectedValue.ToString());
                InvType common = InvType.Common;
                if (this.radioBtnPTFP.Checked)
                {
                    common = InvType.Common;
                }
                else if (this.radioBtnZYFP.Checked)
                {
                    common = InvType.Special;
                }
                else if (this.radioBtnHYFP.Checked)
                {
                    common = InvType.transportation;
                }
                else if (this.radioBtnJDCFP.Checked)
                {
                    common = InvType.vehiclesales;
                }
                if (!File.Exists(this.fileControl1.get_TextBoxFile().Text.Trim()))
                {
                    MessageManager.ShowMsgBox("INP-271101");
                }
                else
                {
                    PropValue.TxtPath = path;
                    PropValue.TxtAmountType = this.comboBoxJESM.SelectedValue.ToString();
                    if (this.radioBtnPTFP.Checked)
                    {
                        PropValue.TxtInvType = "Common";
                    }
                    else if (this.radioBtnZYFP.Checked)
                    {
                        PropValue.TxtInvType = "Special";
                    }
                    else if (this.radioBtnHYFP.Checked)
                    {
                        PropValue.TxtInvType = "transportation";
                    }
                    else if (this.radioBtnJDCFP.Checked)
                    {
                        PropValue.TxtInvType = "vehiclesales";
                    }
                    ErrorResolver errorResolver = new FatchSaleBill().ImportSaleBillTxt(path, priceType, common);
                    double totalSeconds = DateTime.Now.Subtract(now).TotalSeconds;
                    new ResultForm(errorResolver).ShowDialog();
                }
            }
            catch (CustomException)
            {
                MessageManager.ShowMsgBox("INP-271102");
            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("文本文件与票种不匹配"))
                {
                    MessageManager.ShowMsgBox("INP-271103");
                }
                else
                {
                    HandleException.HandleError(exception);
                }
            }
            finally
            {
                base.Close();
            }
        }

        private void DanJuTxtCR_Load(object sender, EventArgs e)
        {
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                bool flag = false;
                if (PropValue.TxtInvType == "Special")
                {
                    if (!card.get_QYLX().ISZYFP)
                    {
                        flag = true;
                    }
                }
                else if (PropValue.TxtInvType == "Common")
                {
                    if (!card.get_QYLX().ISPTFP)
                    {
                        flag = true;
                    }
                }
                else if (PropValue.TxtInvType == "transportation")
                {
                    if (!card.get_QYLX().ISHY)
                    {
                        flag = true;
                    }
                }
                else if ((PropValue.TxtInvType == "vehiclesales") && !card.get_QYLX().ISJDC)
                {
                    flag = true;
                }
                if (flag)
                {
                    if (card.get_QYLX().ISPTFP)
                    {
                        PropValue.TxtInvType = "Common";
                    }
                    else if (card.get_QYLX().ISZYFP)
                    {
                        PropValue.TxtInvType = "Special";
                    }
                    else if (card.get_QYLX().ISHY)
                    {
                        PropValue.TxtInvType = "transportation";
                    }
                    else if (card.get_QYLX().ISJDC)
                    {
                        PropValue.TxtInvType = "vehiclesales";
                    }
                }
                this.fileControl1.set_FileFilter(FatchSaleBill.FileFilterTxt);
                this.fileControl1.get_TextBoxFile().Text = PropValue.TxtPath;
                this.comboBoxJESM.SelectedValue = PropValue.TxtAmountType;
                this.radioBtnZYFP.Checked = PropValue.TxtInvType == "Special";
                this.radioBtnPTFP.Checked = PropValue.TxtInvType == "Common";
                this.radioBtnHYFP.Checked = PropValue.TxtInvType == "transportation";
                this.radioBtnJDCFP.Checked = PropValue.TxtInvType == "vehiclesales";
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
                        this.radioBtnPTFP.Location = new Point(8, 20);
                    }
                    num++;
                }
                if (card.get_QYLX().ISHY)
                {
                    this.radioBtnHYFP.Visible = true;
                    if (num == 0)
                    {
                        this.radioBtnHYFP.Location = new Point(8, 20);
                    }
                    else if (num == 1)
                    {
                        this.radioBtnHYFP.Location = new Point(8, 50);
                    }
                    num++;
                }
                if (card.get_QYLX().ISJDC)
                {
                    this.radioBtnJDCFP.Visible = true;
                    switch (num)
                    {
                        case 0:
                            this.radioBtnJDCFP.Location = new Point(8, 20);
                            break;

                        case 1:
                            this.radioBtnJDCFP.Location = new Point(8, 50);
                            break;

                        case 2:
                            this.radioBtnJDCFP.Location = new Point(120, 20);
                            break;
                    }
                }
                if (this.radioBtnJDCFP.Checked)
                {
                    this.comboBoxJESM.SelectedIndex = 0;
                    this.comboBoxJESM.Enabled = false;
                }
                if (RegisterManager.CheckRegFile("JIJS"))
                {
                    this.radioBtnPTFP.Visible = false;
                    this.radioBtnHYFP.Visible = false;
                    this.radioBtnJDCFP.Visible = false;
                    this.radioBtnZYFP.Checked = true;
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
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.comboBoxJESM = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxJESM");
            this.radioBtnPTFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioBtnPTFP");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.radioBtnZYFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioBtnZYFP");
            this.fileControl1 = this.xmlComponentLoader1.GetControlByName<FileControl>("fileControl1");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.radioBtnHYFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioBtnHYFP");
            this.radioBtnJDCFP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioBtnJDCFP");
            this.checkBox_HYSYDJ = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox_HYSYDJ");
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.comboBoxJESM.DropDownStyle = ComboBoxStyle.DropDownList;
            this.radioBtnJDCFP.CheckedChanged += new EventHandler(this.radioBtnJDCFP_CheckedChanged);
            this.radioBtnZYFP.CheckedChanged += new EventHandler(this.radioBtnZYFP_CheckedChanged);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DanJuTxtCR));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x223, 0x10a);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "文本传入";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.DanJuTxtCR\Aisino.Fwkp.Wbjk.DanJuTxtCR.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x223, 0x10a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DanJuTxtCR";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "文本传入";
            base.Load += new EventHandler(this.DanJuTxtCR_Load);
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
        }

        private void radioBtnZYFP_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}

