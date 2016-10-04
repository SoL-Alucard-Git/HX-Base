namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DataSumForm : BaseForm
    {
        private bool bMonth;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoCMB comBoxEndMonth;
        private AisinoCMB comBoxInvType;
        private AisinoCMB comBoxTaxPeriod;
        private AisinoCMB comBoxYearOrStartMonth;
        private IContainer components;
        private DataSumDAL dataSumDAL = new DataSumDAL();
        private DateTime dtNow;
        private bool hasInit;
        private bool isLastYear;
        private AisinoLBL labelEndMonth = new AisinoLBL();
        private AisinoLBL labelTaxPeriod = new AisinoLBL();
        private AisinoLBL labelYearOrStartMonth = new AisinoLBL();
        private string lastRepDateDZ = "";
        private string lastRepDateHY = "";
        private string lastRepDateJDC = "";
        private string lastRepDateJSFP = "";
        private ILog log = LogUtil.GetLogger<DataSumForm>();
        private CommFun m_commFun = new CommFun();
        private DateTime m_dtStart;
        public INV_TYPE m_invType;
        public int nEndMonth;
        public int nStartMonth;
        public int nTaxYear;
        private AisinoRTX richTextBox1;
        private AisinoRTX richTextBoxRemind;
        public string strDlgTitle = "";
        public string strLabelTitle = "";
        public string strTaxPeriod = "0";
        private TaxcardEntityBLL taxcardEntityBLL = new TaxcardEntityBLL();
        private XmlComponentLoader xmlComponentLoader1;
        private Dictionary<int, List<int>> yearMonth = new Dictionary<int, List<int>>();
        private Dictionary<int, List<int>> yearMonthDZ = new Dictionary<int, List<int>>();
        private Dictionary<int, List<int>> yearMonthHY = new Dictionary<int, List<int>>();
        private Dictionary<int, List<int>> yearMonthJDC = new Dictionary<int, List<int>>();
        private Dictionary<int, List<int>> yearMonthJSFP = new Dictionary<int, List<int>>();
        private Dictionary<int, List<int>> yearMonthZP = new Dictionary<int, List<int>>();

        public DataSumForm(bool bType)
        {
            this.Initial();
            this.bMonth = bType;
            this.dtNow = this.taxcardEntityBLL.GetTaxDate();
            this.m_dtStart = this.taxcardEntityBLL.GetStartDate();
            this.strComBoxYearOrStartMonth = "0";
            this.strComBoxEndMonth = "0";
            this.strComBoxTaxPeriod = "0";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.bMonth)
                {
                    if (((this.comBoxYearOrStartMonth.Text.Trim() == "") || (this.comBoxEndMonth.Text.Trim() == "")) || (this.comBoxTaxPeriod.Text.Trim() == ""))
                    {
                        MessageManager.ShowMsgBox("INP-253105");
                        return;
                    }
                    if (((this.comBoxYearOrStartMonth.Text.Trim() != "") && (this.comBoxEndMonth.Text.Trim() != "")) && (this.comBoxTaxPeriod.Text.Trim() != ""))
                    {
                        this.nTaxYear = Convert.ToInt32(this.comBoxYearOrStartMonth.Text);
                        this.nStartMonth = Convert.ToInt32(this.comBoxEndMonth.Text);
                        this.nEndMonth = Convert.ToInt32(this.comBoxEndMonth.Text);
                        this.strComBoxYearOrStartMonth = this.comBoxYearOrStartMonth.Text;
                        this.strComBoxEndMonth = this.comBoxEndMonth.Text;
                        if (this.comBoxTaxPeriod.Text == "本月累计")
                        {
                            this.strTaxPeriod = "0";
                        }
                        else
                        {
                            this.strTaxPeriod = this.comBoxTaxPeriod.Text.Substring(this.comBoxTaxPeriod.Text.IndexOf("第") + 1, (this.comBoxTaxPeriod.Text.IndexOf("期") - this.comBoxTaxPeriod.Text.IndexOf("第")) - 1);
                            this.log.Debug(this.strTaxPeriod);
                        }
                        this.strDlgTitle = "金税设备" + this.strComBoxYearOrStartMonth + "年" + this.strComBoxEndMonth + "月资料统计";
                        if (this.comBoxTaxPeriod.Text != "本月累计")
                        {
                            this.strLabelTitle = "税档资料所属期为   " + this.nEndMonth.ToString() + "月第" + this.strTaxPeriod + "期";
                        }
                        else
                        {
                            this.strLabelTitle = "税档资料所属期为   " + this.nEndMonth.ToString() + "月份";
                        }
                    }
                }
                else
                {
                    if (!(this.comBoxYearOrStartMonth.Text.Trim() != "") || !(this.comBoxEndMonth.Text.Trim() != ""))
                    {
                        return;
                    }
                    if ((this.comBoxYearOrStartMonth.Text.Trim() == "") || (this.comBoxEndMonth.Text.Trim() == ""))
                    {
                        MessageManager.ShowMsgBox("INP-253105");
                        return;
                    }
                    if (Convert.ToInt32(this.comBoxYearOrStartMonth.Text.Trim()) > Convert.ToInt32(this.comBoxEndMonth.Text.Trim()))
                    {
                        this.nStartMonth = Convert.ToInt32(this.comBoxEndMonth.Text);
                        this.nEndMonth = Convert.ToInt32(this.comBoxYearOrStartMonth.Text);
                    }
                    else
                    {
                        this.nStartMonth = Convert.ToInt32(this.comBoxYearOrStartMonth.Text);
                        this.nEndMonth = Convert.ToInt32(this.comBoxEndMonth.Text);
                    }
                    this.strTaxPeriod = "0";
                    if (this.comBoxYearOrStartMonth.Text != this.comBoxEndMonth.Text)
                    {
                        this.strDlgTitle = "金税设备" + this.nTaxYear.ToString() + "年" + this.nStartMonth.ToString() + "月-" + this.nEndMonth.ToString() + "月资料统计";
                        this.strLabelTitle = "税档资料所属期为 " + this.nStartMonth.ToString() + "月-" + this.nEndMonth.ToString() + "月";
                    }
                    else
                    {
                        this.strDlgTitle = "金税设备" + this.nTaxYear.ToString() + "年" + this.nStartMonth.ToString() + "月资料统计";
                        this.strLabelTitle = "税档资料所属期为 " + this.nStartMonth.ToString() + "月份";
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        public void comBoxEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.hasInit)
                {
                    this.InitialPeriod_();
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-253107", new string[] { exception.Message });
            }
        }

        public void comBoxInvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.hasInit)
                {
                    if (this.strComBoxInvType == "增值税专普票")
                    {
                        this.m_invType = INV_TYPE.INV_SPECIAL;
                    }
                    if (this.strComBoxInvType == "货物运输业增值税专用发票")
                    {
                        this.m_invType = INV_TYPE.INV_TRANSPORTATION;
                    }
                    if (this.strComBoxInvType == "机动车销售统一发票")
                    {
                        this.m_invType = INV_TYPE.INV_VEHICLESALES;
                    }
                    if (this.strComBoxInvType == "电子增值税普通发票")
                    {
                        this.m_invType = INV_TYPE.INV_PTDZ;
                    }
                    if (this.strComBoxInvType == "增值税普通发票(卷票)")
                    {
                        this.m_invType = INV_TYPE.INV_JSFP;
                    }
                    this.InitialYearOrStartMonth_();
                    this.InitialEndMonth_();
                    this.InitialPeriod_();
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-253107", new string[] { exception.Message });
            }
        }

        public void comBoxYearOrStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.hasInit)
                {
                    this.InitialEndMonth_();
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-253107", new string[] { exception.Message });
            }
        }

        private void DataSumForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitialControl();
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-253107", new string[] { exception.Message });
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

        private void Initial()
        {
            this.InitializeComponent();
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Load += new EventHandler(this.DataSumForm_Load);
            this.comBoxInvType = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comBoxInvType");
            this.comBoxInvType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comBoxInvType.SelectedIndexChanged += new EventHandler(this.comBoxInvType_SelectedIndexChanged);
            this.comBoxYearOrStartMonth = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comBoxYearOrStartMonth");
            this.comBoxYearOrStartMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comBoxYearOrStartMonth.SelectedIndexChanged += new EventHandler(this.comBoxYearOrStartMonth_SelectedIndexChanged);
            this.comBoxEndMonth = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comBoxEndMonth");
            this.comBoxEndMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comBoxEndMonth.SelectedIndexChanged += new EventHandler(this.comBoxEndMonth_SelectedIndexChanged);
            this.comBoxTaxPeriod = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comBoxTaxPeriod");
            this.comBoxTaxPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.richTextBox1 = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("richTextBox1");
            this.richTextBox1.Enabled = false;
            this.richTextBox1.BackColor = Color.White;
            this.richTextBox1.BackColor = SystemColors.Control;
            this.richTextBox1.BorderStyle = BorderStyle.None;
            this.richTextBoxRemind = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("richTextBoxRemind");
            this.richTextBoxRemind.Enabled = false;
            this.richTextBoxRemind.BackColor = Color.White;
            this.richTextBoxRemind.BackColor = SystemColors.Control;
            this.richTextBoxRemind.BorderStyle = BorderStyle.None;
            this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.xmlComponentLoader1.GetControlByName<AisinoPIC>("pictureBox1");
            this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.labelYearOrStartMonth = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelYearOrStartMonth");
            this.labelEndMonth = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelEndMonth");
            this.labelTaxPeriod = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelTaxPeriod");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        }

        private void InitialControl()
        {
            foreach (InvTypeInfo info2 in base.TaxCardInstance.get_StateInfo().InvTypeInfo)
            {
                if (info2.InvType == 11)
                {
                    this.lastRepDateHY = info2.LastRepDate;
                }
                if (info2.InvType == 12)
                {
                    this.lastRepDateJDC = info2.LastRepDate;
                }
                if (info2.InvType == 0x33)
                {
                    List<string> cSDate = base.TaxCardInstance.GetCSDate(info2.InvType);
                    if (cSDate != null)
                    {
                        this.lastRepDateDZ = cSDate[0];
                    }
                }
                if (info2.InvType == 0x29)
                {
                    List<string> list3 = base.TaxCardInstance.GetCSDate(info2.InvType);
                    if (list3 != null)
                    {
                        this.lastRepDateJSFP = list3[0];
                    }
                }
            }
            this.yearMonth = this.taxcardEntityBLL.getYearMonth();
            if (base.TaxCardInstance.get_QYLX().ISZYFP || base.TaxCardInstance.get_QYLX().ISPTFP)
            {
                this.yearMonthZP = this.taxcardEntityBLL.getYearMonthZP();
            }
            if (base.TaxCardInstance.get_QYLX().ISHY)
            {
                this.yearMonthHY = this.taxcardEntityBLL.getYearMonthHY();
            }
            if (base.TaxCardInstance.get_QYLX().ISJDC)
            {
                this.yearMonthJDC = this.taxcardEntityBLL.getYearMonthJDC();
            }
            if (base.TaxCardInstance.get_QYLX().ISPTFPDZ)
            {
                this.yearMonthDZ = this.taxcardEntityBLL.getYearMonthDZ();
            }
            if (base.TaxCardInstance.get_QYLX().ISPTFPJSP)
            {
                this.yearMonthJSFP = this.taxcardEntityBLL.getYearMonthJSFP();
            }
            this.InitialInvType();
            this.InitialYearOrStartMonth_();
            this.InitialEndMonth_();
            this.InitialPeriod_();
            this.hasInit = true;
            if (this.comBoxInvType.Items.Count > 0)
            {
                this.comBoxInvType.SelectedIndex = 0;
            }
        }

        private void InitialEndMonth_()
        {
            try
            {
                this.comBoxEndMonth.Items.Clear();
                List<int> list = new List<int>();
                if (this.bMonth)
                {
                    string text = this.comBoxYearOrStartMonth.Text;
                    if ((text != null) && (text.Trim() != ""))
                    {
                        if (this.comBoxInvType.SelectedItem.Equals("增值税专普票"))
                        {
                            list = this.yearMonthZP[Convert.ToInt32(text)];
                        }
                        if (this.comBoxInvType.SelectedItem.Equals("货物运输业增值税专用发票"))
                        {
                            list = this.yearMonthHY[Convert.ToInt32(text)];
                        }
                        if (this.comBoxInvType.SelectedItem.Equals("机动车销售统一发票"))
                        {
                            list = this.yearMonthJDC[Convert.ToInt32(text)];
                        }
                        if (this.comBoxInvType.SelectedItem.Equals("电子增值税普通发票"))
                        {
                            list = this.yearMonthDZ[Convert.ToInt32(text)];
                        }
                        if (this.comBoxInvType.SelectedItem.Equals("增值税普通发票(卷票)"))
                        {
                            list = this.yearMonthJSFP[Convert.ToInt32(text)];
                        }
                    }
                }
                else
                {
                    if (this.comBoxInvType.SelectedItem.Equals("增值税专普票"))
                    {
                        if (this.yearMonthZP.ContainsKey(this.dtNow.Year))
                        {
                            list = this.yearMonthZP[this.dtNow.Year];
                        }
                        else if (this.yearMonthZP.ContainsKey(this.dtNow.Year - 1))
                        {
                            list = this.yearMonthZP[this.dtNow.Year - 1];
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("货物运输业增值税专用发票"))
                    {
                        if (this.yearMonthHY.ContainsKey(this.dtNow.Year))
                        {
                            list = this.yearMonthHY[this.dtNow.Year];
                        }
                        else if (this.yearMonthHY.ContainsKey(this.dtNow.Year - 1))
                        {
                            list = this.yearMonthHY[this.dtNow.Year - 1];
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("机动车销售统一发票"))
                    {
                        if (this.yearMonthJDC.ContainsKey(this.dtNow.Year))
                        {
                            list = this.yearMonthJDC[this.dtNow.Year];
                        }
                        else if (this.yearMonthJDC.ContainsKey(this.dtNow.Year - 1))
                        {
                            list = this.yearMonthJDC[this.dtNow.Year - 1];
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("电子增值税普通发票"))
                    {
                        if (this.yearMonthDZ.ContainsKey(this.dtNow.Year))
                        {
                            list = this.yearMonthDZ[this.dtNow.Year];
                        }
                        else if (this.yearMonthDZ.ContainsKey(this.dtNow.Year - 1))
                        {
                            list = this.yearMonthDZ[this.dtNow.Year - 1];
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("增值税普通发票(卷票)"))
                    {
                        if (this.yearMonthJSFP.ContainsKey(this.dtNow.Year))
                        {
                            list = this.yearMonthJSFP[this.dtNow.Year];
                        }
                        else if (this.yearMonthJSFP.ContainsKey(this.dtNow.Year - 1))
                        {
                            list = this.yearMonthJSFP[this.dtNow.Year - 1];
                        }
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    this.comBoxEndMonth.Items.Add(list[i]);
                }
                if (this.comBoxEndMonth.Items.Count > 0)
                {
                    this.comBoxEndMonth.SelectedIndex = this.comBoxEndMonth.Items.Count - 1;
                }
                else
                {
                    this.comBoxEndMonth.SelectedIndex = -1;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void InitialInvType()
        {
            List<InvTypeEntity> invTypeCollect = new List<InvTypeEntity>();
            invTypeCollect = this.m_commFun.GetInvTypeCollect();
            this.comBoxInvType.Items.Clear();
            foreach (InvTypeEntity entity in invTypeCollect)
            {
                if (((entity.m_invType == INV_TYPE.INV_SPECIAL) || (entity.m_invType == INV_TYPE.INV_COMMON)) && !this.comBoxInvType.Items.Contains("增值税专普票"))
                {
                    this.comBoxInvType.Items.Add("增值税专普票");
                }
                if (entity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                {
                    this.comBoxInvType.Items.Add("货物运输业增值税专用发票");
                }
                if (entity.m_invType == INV_TYPE.INV_VEHICLESALES)
                {
                    this.comBoxInvType.Items.Add("机动车销售统一发票");
                }
                if (entity.m_invType == INV_TYPE.INV_PTDZ)
                {
                    this.comBoxInvType.Items.Add("电子增值税普通发票");
                }
                if (entity.m_invType == INV_TYPE.INV_JSFP)
                {
                    this.comBoxInvType.Items.Add("增值税普通发票(卷票)");
                }
            }
            if (this.comBoxInvType.Items.Count > 0)
            {
                this.comBoxInvType.SelectedIndex = 0;
            }
            else
            {
                this.comBoxInvType.SelectedIndex = -1;
            }
            if (this.strComBoxInvType == "增值税专普票")
            {
                this.m_invType = INV_TYPE.INV_SPECIAL;
            }
            if (this.strComBoxInvType == "货物运输业增值税专用发票")
            {
                this.m_invType = INV_TYPE.INV_TRANSPORTATION;
            }
            if (this.strComBoxInvType == "机动车销售统一发票")
            {
                this.m_invType = INV_TYPE.INV_VEHICLESALES;
            }
            if (this.strComBoxInvType == "电子增值税普通发票")
            {
                this.m_invType = INV_TYPE.INV_PTDZ;
            }
            if (this.strComBoxInvType == "增值税普通发票(卷票)")
            {
                this.m_invType = INV_TYPE.INV_JSFP;
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DataSumForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x197, 0x160);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.DataSumForm\Aisino.Fwkp.Bsgl.DataSumForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x197, 0x160);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DataSumForm";
            base.ShowIcon = false;
            this.Text = "指定汇总范围";
            base.ResumeLayout(false);
        }

        private void InitialPeriod_()
        {
            this.comBoxTaxPeriod.Items.Clear();
            this.comBoxTaxPeriod.Items.Add("本月累计");
            int num = 0;
            int month = 0;
            int year = 0;
            if (!string.IsNullOrEmpty(this.comBoxYearOrStartMonth.Text))
            {
                year = Convert.ToInt32(this.comBoxYearOrStartMonth.Text);
            }
            else
            {
                this.comBoxInvType.Items.Remove(this.comBoxInvType.SelectedItem);
            }
            if (!string.IsNullOrEmpty(this.comBoxEndMonth.Text))
            {
                month = Convert.ToInt32(this.comBoxEndMonth.Text);
            }
            else
            {
                this.comBoxInvType.Items.Remove(this.comBoxInvType.SelectedItem);
            }
            DateTime time = new DateTime(year, month, 1);
            if (this.m_invType == INV_TYPE.INV_SPECIAL)
            {
                int num4 = base.TaxCardInstance.get_LastRepDateYear();
                int num5 = base.TaxCardInstance.get_LastRepDateMonth();
                DateTime time2 = new DateTime(num4, num5, 1);
                if (DateTime.Compare(time2, time) == 0)
                {
                    num = base.TaxCardInstance.GetPeriodCount(0)[1];
                }
                else if (DateTime.Compare(time, time2.AddMonths(-1)) == 0)
                {
                    num = base.TaxCardInstance.GetPeriodCount(0)[0];
                }
                else
                {
                    num = 0;
                }
            }
            if (this.m_invType == INV_TYPE.INV_TRANSPORTATION)
            {
                string lastRepDateHY = this.lastRepDateHY;
                int num6 = -1;
                int num7 = -1;
                if ((lastRepDateHY.Length > 0) && lastRepDateHY.Contains("-"))
                {
                    num7 = int.Parse(lastRepDateHY.Split(new char[] { '-' })[0]);
                    num6 = int.Parse(lastRepDateHY.Split(new char[] { '-' })[1]);
                }
                DateTime time3 = new DateTime(num7, num6, 1);
                if (DateTime.Compare(time3, time) == 0)
                {
                    num = base.TaxCardInstance.GetPeriodCount(11)[1];
                }
                else if (DateTime.Compare(time, time3.AddMonths(-1)) == 0)
                {
                    num = base.TaxCardInstance.GetPeriodCount(11)[0];
                }
                else
                {
                    num = 0;
                }
            }
            if (this.m_invType == INV_TYPE.INV_VEHICLESALES)
            {
                string lastRepDateJDC = this.lastRepDateJDC;
                int num8 = -1;
                int num9 = -1;
                if ((lastRepDateJDC.Length > 0) && lastRepDateJDC.Contains("-"))
                {
                    num9 = int.Parse(lastRepDateJDC.Split(new char[] { '-' })[0]);
                    num8 = int.Parse(lastRepDateJDC.Split(new char[] { '-' })[1]);
                }
                DateTime time4 = new DateTime(num9, num8, 1);
                if (DateTime.Compare(time4, time) == 0)
                {
                    num = base.TaxCardInstance.GetPeriodCount(12)[1];
                }
                else if (DateTime.Compare(time, time4.AddMonths(-1)) == 0)
                {
                    num = base.TaxCardInstance.GetPeriodCount(12)[0];
                }
                else
                {
                    num = 0;
                }
            }
            if (this.m_invType == INV_TYPE.INV_PTDZ)
            {
                string lastRepDateDZ = this.lastRepDateDZ;
                int num10 = -1;
                int num11 = -1;
                if ((lastRepDateDZ.Length > 0) && lastRepDateDZ.Contains("-"))
                {
                    num11 = int.Parse(lastRepDateDZ.Split(new char[] { '-' })[0]);
                    num10 = int.Parse(lastRepDateDZ.Split(new char[] { '-' })[1]);
                }
                DateTime time5 = new DateTime(num11, num10, 1);
                if (DateTime.Compare(time5, time) == 0)
                {
                    num = base.TaxCardInstance.GetPeriodCount(0x33)[1];
                }
                else if (DateTime.Compare(time, time5.AddMonths(-1)) == 0)
                {
                    num = base.TaxCardInstance.GetPeriodCount(0x33)[0];
                }
                else
                {
                    num = 0;
                }
            }
            if (this.m_invType == INV_TYPE.INV_JSFP)
            {
                string lastRepDateJSFP = this.lastRepDateJSFP;
                int num12 = -1;
                int num13 = -1;
                if ((lastRepDateJSFP.Length > 0) && lastRepDateJSFP.Contains("-"))
                {
                    num13 = int.Parse(lastRepDateJSFP.Split(new char[] { '-' })[0]);
                    num12 = int.Parse(lastRepDateJSFP.Split(new char[] { '-' })[1]);
                }
                DateTime time6 = new DateTime(num13, num12, 1);
                if (DateTime.Compare(time6, time) == 0)
                {
                    num = base.TaxCardInstance.GetPeriodCount(0x29)[1];
                }
                else if (DateTime.Compare(time, time6.AddMonths(-1)) == 0)
                {
                    num = base.TaxCardInstance.GetPeriodCount(0x29)[0];
                }
                else
                {
                    num = 0;
                }
            }
            for (int i = 0; i < num; i++)
            {
                int num15 = i + 1;
                this.comBoxTaxPeriod.Items.Add("第" + num15.ToString() + "期");
            }
            if (this.comBoxTaxPeriod.Items.Count > 0)
            {
                this.comBoxTaxPeriod.SelectedIndex = this.comBoxTaxPeriod.Items.Count - 1;
            }
            else
            {
                this.comBoxTaxPeriod.SelectedIndex = -1;
            }
        }

        private void InitialYearOrStartMonth()
        {
            try
            {
                this.comBoxYearOrStartMonth.Items.Clear();
                if (this.bMonth)
                {
                    if (this.m_dtStart > this.dtNow)
                    {
                        this.m_dtStart = this.dtNow;
                        this.comBoxYearOrStartMonth.Items.Add(this.dtNow.Year);
                    }
                    else
                    {
                        DateTime dtNow = this.dtNow;
                        while (dtNow.Year >= this.m_dtStart.Year)
                        {
                            if (!this.yearMonth.ContainsKey(dtNow.Year))
                            {
                                dtNow = dtNow.AddYears(-1);
                            }
                            else
                            {
                                List<int> list = this.yearMonth[dtNow.Year];
                                if ((list != null) && (list.Count > 0))
                                {
                                    if (this.comBoxInvType.SelectedItem.Equals("增值税专普票"))
                                    {
                                        if (dtNow.Year < base.TaxCardInstance.get_SysYear())
                                        {
                                            this.comBoxYearOrStartMonth.Items.Add(dtNow.Year);
                                        }
                                        else if (((base.TaxCardInstance.get_LastRepDateMonth() == 1) || (list.Count != 1)) || (list[0] != 1))
                                        {
                                            this.comBoxYearOrStartMonth.Items.Add(dtNow.Year);
                                        }
                                    }
                                    if (this.comBoxInvType.SelectedItem.Equals("货物运输业增值税专用发票"))
                                    {
                                        string lastRepDateHY = this.lastRepDateHY;
                                        int num = -1;
                                        if ((lastRepDateHY.Length > 0) && lastRepDateHY.Contains("-"))
                                        {
                                            int.Parse(lastRepDateHY.Split(new char[] { '-' })[0]);
                                            num = int.Parse(lastRepDateHY.Split(new char[] { '-' })[1]);
                                        }
                                        if (dtNow.Year < base.TaxCardInstance.get_SysYear())
                                        {
                                            this.comBoxYearOrStartMonth.Items.Add(dtNow.Year);
                                        }
                                        else if (((num == 1) || (list.Count != 1)) || (list[0] != 1))
                                        {
                                            this.comBoxYearOrStartMonth.Items.Add(dtNow.Year);
                                        }
                                    }
                                    if (this.comBoxInvType.SelectedItem.Equals("机动车销售统一发票"))
                                    {
                                        string lastRepDateJDC = this.lastRepDateJDC;
                                        int num2 = -1;
                                        if ((lastRepDateJDC.Length > 0) && lastRepDateJDC.Contains("-"))
                                        {
                                            int.Parse(lastRepDateJDC.Split(new char[] { '-' })[0]);
                                            num2 = int.Parse(lastRepDateJDC.Split(new char[] { '-' })[1]);
                                        }
                                        if (dtNow.Year < base.TaxCardInstance.get_SysYear())
                                        {
                                            this.comBoxYearOrStartMonth.Items.Add(dtNow.Year);
                                        }
                                        else if (((num2 == 1) || (list.Count != 1)) || (list[0] != 1))
                                        {
                                            this.comBoxYearOrStartMonth.Items.Add(dtNow.Year);
                                        }
                                    }
                                }
                                dtNow = dtNow.AddYears(-1);
                            }
                        }
                    }
                    if (this.comBoxYearOrStartMonth.Items.Count > 0)
                    {
                        this.comBoxYearOrStartMonth.SelectedIndex = 0;
                    }
                }
                else
                {
                    List<int> list2 = this.yearMonth[this.dtNow.Year];
                    for (int i = 0; i < list2.Count; i++)
                    {
                        if (list2[i] == base.TaxCardInstance.get_SysMonth())
                        {
                            int num4 = -1;
                            int num5 = -1;
                            this.isLastYear = false;
                            if (this.comBoxInvType.SelectedItem.Equals("增值税专普票"))
                            {
                                if (base.TaxCardInstance.get_LastRepDateMonth() == base.TaxCardInstance.get_SysMonth())
                                {
                                    this.comBoxYearOrStartMonth.Items.Add(list2[i]);
                                }
                                else if (base.TaxCardInstance.get_SysMonth() == 1)
                                {
                                    List<int> list3 = this.yearMonth[this.dtNow.Year - 1];
                                    if (list3.Count == 0)
                                    {
                                        this.comBoxYearOrStartMonth.Items.Add("12");
                                    }
                                    else if (list3.Count > 0)
                                    {
                                        for (int j = 0; j < list3.Count; j++)
                                        {
                                            this.comBoxYearOrStartMonth.Items.Add(list3[j]);
                                        }
                                    }
                                    this.isLastYear = true;
                                }
                            }
                            else if (this.comBoxInvType.SelectedItem.Equals("货物运输业增值税专用发票"))
                            {
                                string str3 = this.lastRepDateHY;
                                num4 = -1;
                                if ((str3.Length > 0) && str3.Contains("-"))
                                {
                                    num4 = int.Parse(str3.Split(new char[] { '-' })[1]);
                                }
                                if (num4 == base.TaxCardInstance.get_SysMonth())
                                {
                                    this.comBoxYearOrStartMonth.Items.Add(list2[i]);
                                }
                                else if (base.TaxCardInstance.get_SysMonth() == 1)
                                {
                                    List<int> list4 = this.yearMonth[this.dtNow.Year - 1];
                                    if (list4.Count == 0)
                                    {
                                        this.comBoxYearOrStartMonth.Items.Add("12");
                                    }
                                    else if (list4.Count > 0)
                                    {
                                        for (int k = 0; k < list4.Count; k++)
                                        {
                                            this.comBoxYearOrStartMonth.Items.Add(list4[k]);
                                        }
                                    }
                                    this.isLastYear = true;
                                }
                            }
                            else if (this.comBoxInvType.SelectedItem.Equals("机动车销售统一发票"))
                            {
                                string str4 = this.lastRepDateJDC;
                                num5 = -1;
                                if ((str4.Length > 0) && str4.Contains("-"))
                                {
                                    num5 = int.Parse(str4.Split(new char[] { '-' })[1]);
                                }
                                if (num5 == base.TaxCardInstance.get_SysMonth())
                                {
                                    this.comBoxYearOrStartMonth.Items.Add(list2[i]);
                                }
                                else if (base.TaxCardInstance.get_SysMonth() == 1)
                                {
                                    List<int> list5 = this.yearMonth[this.dtNow.Year - 1];
                                    if (list5.Count == 0)
                                    {
                                        this.comBoxYearOrStartMonth.Items.Add("12");
                                    }
                                    else if (list5.Count > 0)
                                    {
                                        for (int m = 0; m < list5.Count; m++)
                                        {
                                            this.comBoxYearOrStartMonth.Items.Add(list5[m]);
                                        }
                                    }
                                    this.isLastYear = true;
                                }
                            }
                        }
                        else
                        {
                            this.comBoxYearOrStartMonth.Items.Add(list2[i]);
                        }
                    }
                    if (this.comBoxYearOrStartMonth.Items.Count > 0)
                    {
                        this.comBoxYearOrStartMonth.SelectedIndex = this.comBoxYearOrStartMonth.Items.Count - 1;
                    }
                    else
                    {
                        this.comBoxYearOrStartMonth.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void InitialYearOrStartMonth_()
        {
            try
            {
                this.comBoxYearOrStartMonth.Items.Clear();
                if (this.bMonth)
                {
                    if (this.comBoxInvType.SelectedItem.Equals("增值税专普票"))
                    {
                        foreach (KeyValuePair<int, List<int>> pair in this.yearMonthZP)
                        {
                            this.comBoxYearOrStartMonth.Items.Add(pair.Key);
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("货物运输业增值税专用发票"))
                    {
                        foreach (KeyValuePair<int, List<int>> pair2 in this.yearMonthHY)
                        {
                            this.comBoxYearOrStartMonth.Items.Add(pair2.Key);
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("机动车销售统一发票"))
                    {
                        foreach (KeyValuePair<int, List<int>> pair3 in this.yearMonthJDC)
                        {
                            this.comBoxYearOrStartMonth.Items.Add(pair3.Key);
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("电子增值税普通发票"))
                    {
                        foreach (KeyValuePair<int, List<int>> pair4 in this.yearMonthDZ)
                        {
                            this.comBoxYearOrStartMonth.Items.Add(pair4.Key);
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("增值税普通发票(卷票)"))
                    {
                        foreach (KeyValuePair<int, List<int>> pair5 in this.yearMonthJSFP)
                        {
                            this.comBoxYearOrStartMonth.Items.Add(pair5.Key);
                        }
                    }
                    if (this.comBoxYearOrStartMonth.Items.Count > 0)
                    {
                        this.comBoxYearOrStartMonth.SelectedIndex = this.comBoxYearOrStartMonth.Items.Count - 1;
                    }
                }
                else
                {
                    if (this.comBoxInvType.SelectedItem.Equals("增值税专普票"))
                    {
                        if (this.yearMonthZP.ContainsKey(this.dtNow.Year))
                        {
                            List<int> list = this.yearMonthZP[this.dtNow.Year];
                            for (int i = 0; i < list.Count; i++)
                            {
                                this.comBoxYearOrStartMonth.Items.Add(list[i]);
                            }
                            this.nTaxYear = this.dtNow.Year;
                        }
                        else if (this.yearMonthZP.ContainsKey(this.dtNow.Year - 1))
                        {
                            List<int> list2 = this.yearMonthZP[this.dtNow.Year - 1];
                            for (int j = 0; j < list2.Count; j++)
                            {
                                this.comBoxYearOrStartMonth.Items.Add(list2[j]);
                            }
                            this.nTaxYear = this.dtNow.Year - 1;
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("货物运输业增值税专用发票"))
                    {
                        if (this.yearMonthHY.ContainsKey(this.dtNow.Year))
                        {
                            List<int> list3 = this.yearMonthHY[this.dtNow.Year];
                            for (int k = 0; k < list3.Count; k++)
                            {
                                this.comBoxYearOrStartMonth.Items.Add(list3[k]);
                            }
                            this.nTaxYear = this.dtNow.Year;
                        }
                        else if (this.yearMonthHY.ContainsKey(this.dtNow.Year - 1))
                        {
                            List<int> list4 = this.yearMonthHY[this.dtNow.Year - 1];
                            for (int m = 0; m < list4.Count; m++)
                            {
                                this.comBoxYearOrStartMonth.Items.Add(list4[m]);
                            }
                            this.nTaxYear = this.dtNow.Year - 1;
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("机动车销售统一发票"))
                    {
                        if (this.yearMonthJDC.ContainsKey(this.dtNow.Year))
                        {
                            List<int> list5 = this.yearMonthJDC[this.dtNow.Year];
                            for (int n = 0; n < list5.Count; n++)
                            {
                                this.comBoxYearOrStartMonth.Items.Add(list5[n]);
                            }
                            this.nTaxYear = this.dtNow.Year;
                        }
                        else if (this.yearMonthJDC.ContainsKey(this.dtNow.Year - 1))
                        {
                            List<int> list6 = this.yearMonthJDC[this.dtNow.Year - 1];
                            for (int num6 = 0; num6 < list6.Count; num6++)
                            {
                                this.comBoxYearOrStartMonth.Items.Add(list6[num6]);
                            }
                            this.nTaxYear = this.dtNow.Year - 1;
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("电子增值税普通发票"))
                    {
                        if (this.yearMonthDZ.ContainsKey(this.dtNow.Year))
                        {
                            List<int> list7 = this.yearMonthDZ[this.dtNow.Year];
                            for (int num7 = 0; num7 < list7.Count; num7++)
                            {
                                this.comBoxYearOrStartMonth.Items.Add(list7[num7]);
                            }
                            this.nTaxYear = this.dtNow.Year;
                        }
                        else if (this.yearMonthDZ.ContainsKey(this.dtNow.Year - 1))
                        {
                            List<int> list8 = this.yearMonthDZ[this.dtNow.Year - 1];
                            for (int num8 = 0; num8 < list8.Count; num8++)
                            {
                                this.comBoxYearOrStartMonth.Items.Add(list8[num8]);
                            }
                            this.nTaxYear = this.dtNow.Year - 1;
                        }
                    }
                    if (this.comBoxInvType.SelectedItem.Equals("增值税普通发票(卷票)"))
                    {
                        if (this.yearMonthJSFP.ContainsKey(this.dtNow.Year))
                        {
                            List<int> list9 = this.yearMonthJSFP[this.dtNow.Year];
                            for (int num9 = 0; num9 < list9.Count; num9++)
                            {
                                this.comBoxYearOrStartMonth.Items.Add(list9[num9]);
                            }
                            this.nTaxYear = this.dtNow.Year;
                        }
                        else if (this.yearMonthJSFP.ContainsKey(this.dtNow.Year - 1))
                        {
                            List<int> list10 = this.yearMonthJSFP[this.dtNow.Year - 1];
                            for (int num10 = 0; num10 < list10.Count; num10++)
                            {
                                this.comBoxYearOrStartMonth.Items.Add(list10[num10]);
                            }
                            this.nTaxYear = this.dtNow.Year - 1;
                        }
                    }
                    if (this.comBoxYearOrStartMonth.Items.Count > 0)
                    {
                        this.comBoxYearOrStartMonth.SelectedIndex = this.comBoxYearOrStartMonth.Items.Count - 1;
                    }
                    else
                    {
                        this.comBoxYearOrStartMonth.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        public bool bComBoxEndMonth
        {
            get
            {
                return this.comBoxEndMonth.Visible;
            }
            set
            {
                this.comBoxEndMonth.Visible = value;
            }
        }

        public bool bComBoxInvType
        {
            get
            {
                return this.comBoxInvType.Visible;
            }
            set
            {
                this.comBoxInvType.Visible = value;
            }
        }

        public bool bComboxTaxPeriod
        {
            get
            {
                return this.comBoxTaxPeriod.Visible;
            }
            set
            {
                this.comBoxTaxPeriod.Visible = value;
            }
        }

        public bool bComBoxYearOrStartMonth
        {
            get
            {
                return this.comBoxYearOrStartMonth.Visible;
            }
            set
            {
                this.comBoxYearOrStartMonth.Visible = value;
            }
        }

        public bool bLabelTaxPeriod
        {
            set
            {
                this.labelTaxPeriod.Visible = value;
            }
        }

        public string strComBoxEndMonth
        {
            get
            {
                return this.comBoxEndMonth.Text;
            }
            set
            {
                this.comBoxEndMonth.Text = value;
            }
        }

        public string strComBoxInvType
        {
            get
            {
                return this.comBoxInvType.Text;
            }
            set
            {
                this.comBoxInvType.Text = value;
            }
        }

        public string strComBoxTaxPeriod
        {
            get
            {
                return this.comBoxTaxPeriod.Text;
            }
            set
            {
                this.comBoxTaxPeriod.Text = value;
            }
        }

        public string strComBoxYearOrStartMonth
        {
            get
            {
                return this.comBoxYearOrStartMonth.Text;
            }
            set
            {
                this.comBoxYearOrStartMonth.Text = value;
            }
        }

        public string strLabelEndMonth
        {
            get
            {
                return this.labelEndMonth.Text;
            }
            set
            {
                this.labelEndMonth.Text = value;
            }
        }

        public string strLabelYearOrStartMonth
        {
            get
            {
                return this.labelYearOrStartMonth.Text;
            }
            set
            {
                this.labelYearOrStartMonth.Text = value;
            }
        }

        public string strRichTextBoxRemind
        {
            set
            {
                this.richTextBoxRemind.Text = value;
            }
        }
    }
}

