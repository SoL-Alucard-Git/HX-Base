namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class JDCXSDJEdite : Form
    {
        private SaleBill bill;
        private AisinoBTN button1;
        private AisinoBTN button2;
        private IContainer components;
        private DateTimePicker dateTimePicker1;
        private Invoice InvoiceKP;
        private SaleBillCtrl saleBillBL;
        private AisinoTXT textBox1;
        private AisinoTXT textBox10;
        private AisinoTXT textBox11;
        private AisinoTXT textBox12;
        private AisinoTXT textBox13;
        private AisinoTXT textBox14;
        private AisinoTXT textBox15;
        private AisinoTXT textBox16;
        private AisinoTXT textBox17;
        private AisinoTXT textBox18;
        private AisinoTXT textBox2;
        private AisinoTXT textBox20;
        private AisinoTXT textBox21;
        private AisinoTXT textBox22;
        private AisinoTXT textBox23;
        private AisinoTXT textBox3;
        private AisinoTXT textBox4;
        private AisinoTXT textBox5;
        private AisinoTXT textBox6;
        private AisinoTXT textBox7;
        private AisinoTXT textBox8;
        private AisinoTXT textBox9;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private XmlComponentLoader xmlComponentLoader1;

        public JDCXSDJEdite()
        {
            this.components = null;
            this.saleBillBL = SaleBillCtrl.Instance;
            this.bill = null;
            this.InvoiceKP = null;
            this.Initialize();
            this.Text = "机动车单据添加";
            this.initInvoice();
            this.bill = new SaleBill();
            this.bill.DJRQ = TaxCardValue.taxCard.GetCardClock();
        }

        public JDCXSDJEdite(string BH)
        {
            this.components = null;
            this.saleBillBL = SaleBillCtrl.Instance;
            this.bill = null;
            this.InvoiceKP = null;
            this.Initialize();
            this.Text = "机动车单据修改";
            this.textBox1.Cursor = Cursors.No;
            this.textBox1.ForeColor = SystemColors.GrayText;
            this.textBox1.ReadOnly = true;
            this.initInvoice();
            this.bill = this.saleBillBL.Find(BH);
            this.ToView();
            if ((this.bill.DJZT == "W") || (this.bill.KPZT != "N"))
            {
                this.textBox2.Cursor = Cursors.No;
                this.textBox2.ForeColor = SystemColors.GrayText;
                this.textBox2.ReadOnly = true;
                this.textBox3.Cursor = Cursors.No;
                this.textBox3.ForeColor = SystemColors.GrayText;
                this.textBox3.ReadOnly = true;
                this.textBox4.Cursor = Cursors.No;
                this.textBox4.ForeColor = SystemColors.GrayText;
                this.textBox4.ReadOnly = true;
                this.textBox5.Cursor = Cursors.No;
                this.textBox5.ForeColor = SystemColors.GrayText;
                this.textBox5.ReadOnly = true;
                this.textBox6.Cursor = Cursors.No;
                this.textBox6.ForeColor = SystemColors.GrayText;
                this.textBox6.ReadOnly = true;
                this.textBox7.Cursor = Cursors.No;
                this.textBox7.ForeColor = SystemColors.GrayText;
                this.textBox7.ReadOnly = true;
                this.textBox8.Cursor = Cursors.No;
                this.textBox8.ForeColor = SystemColors.GrayText;
                this.textBox8.ReadOnly = true;
                this.textBox9.Cursor = Cursors.No;
                this.textBox9.ForeColor = SystemColors.GrayText;
                this.textBox9.ReadOnly = true;
                this.textBox10.Cursor = Cursors.No;
                this.textBox10.ForeColor = SystemColors.GrayText;
                this.textBox10.ReadOnly = true;
                this.textBox11.Cursor = Cursors.No;
                this.textBox11.ForeColor = SystemColors.GrayText;
                this.textBox11.ReadOnly = true;
                this.textBox12.Cursor = Cursors.No;
                this.textBox12.ForeColor = SystemColors.GrayText;
                this.textBox12.ReadOnly = true;
                this.textBox13.Cursor = Cursors.No;
                this.textBox13.ForeColor = SystemColors.GrayText;
                this.textBox13.ReadOnly = true;
                this.textBox14.Cursor = Cursors.No;
                this.textBox14.ForeColor = SystemColors.GrayText;
                this.textBox14.ReadOnly = true;
                this.textBox15.Cursor = Cursors.No;
                this.textBox15.ForeColor = SystemColors.GrayText;
                this.textBox15.ReadOnly = true;
                this.textBox16.Cursor = Cursors.No;
                this.textBox16.ForeColor = SystemColors.GrayText;
                this.textBox16.ReadOnly = true;
                this.textBox17.Cursor = Cursors.No;
                this.textBox17.ForeColor = SystemColors.GrayText;
                this.textBox17.ReadOnly = true;
                this.textBox18.Cursor = Cursors.No;
                this.textBox18.ForeColor = SystemColors.GrayText;
                this.textBox18.ReadOnly = true;
                this.textBox20.Cursor = Cursors.No;
                this.textBox20.ForeColor = SystemColors.GrayText;
                this.textBox20.ReadOnly = true;
                this.textBox21.Cursor = Cursors.No;
                this.textBox21.ForeColor = SystemColors.GrayText;
                this.textBox21.ReadOnly = true;
                this.textBox22.Cursor = Cursors.No;
                this.textBox22.ForeColor = SystemColors.GrayText;
                this.textBox22.ReadOnly = true;
                this.textBox23.Cursor = Cursors.No;
                this.textBox23.ForeColor = SystemColors.GrayText;
                this.textBox23.ReadOnly = true;
                this.dateTimePicker1.Enabled = false;
                this.button1.Enabled = false;
                this.button2.Enabled = false;
                this.toolStripButton2.Enabled = false;
            }
        }

        private void CLXX_BtnClick(object sender, EventArgs e)
        {
            string text = this.textBox4.Text;
            this.SetCLXX(text, 0);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GFXX_BtnClick(object sender, EventArgs e)
        {
            string text = this.textBox2.Text;
            this.SetGFXX(text, 0);
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripButton1 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton1");
            this.toolStripButton2 = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButton2");
            this.textBox1 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox1");
            this.textBox2 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox2");
            this.textBox3 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox3");
            this.textBox4 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox4");
            this.textBox5 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox5");
            this.textBox6 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox6");
            this.textBox7 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox7");
            this.textBox8 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox8");
            this.textBox9 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox9");
            this.textBox10 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox10");
            this.textBox11 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox11");
            this.textBox12 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox12");
            this.textBox13 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox13");
            this.textBox14 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox14");
            this.textBox15 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox15");
            this.textBox16 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox16");
            this.textBox17 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox17");
            this.textBox18 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox18");
            this.textBox20 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox20");
            this.textBox21 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox21");
            this.textBox22 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox22");
            this.textBox23 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox23");
            this.dateTimePicker1 = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dateTimePicker1");
            this.button1 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button1");
            this.button2 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button2");
            this.toolStripButton1.Click += new EventHandler(this.QuitBtnClick);
            this.toolStripButton2.Click += new EventHandler(this.SaveBtnClick);
            this.button1.Click += new EventHandler(this.GFXX_BtnClick);
            this.button2.Click += new EventHandler(this.CLXX_BtnClick);
            this.textBox20.LostFocus += new EventHandler(this.textBox20_LostFocus);
            this.textBox20.KeyPress += new KeyPressEventHandler(this.textBox20_KeyPress);
            base.FormClosing += new FormClosingEventHandler(this.JDCXSDJEdite_FormClosing);
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.textBox2.TextChanged += new EventHandler(this.textBox2_TextChanged);
            this.textBox3.TextChanged += new EventHandler(this.textBox3_TextChanged);
            this.textBox4.TextChanged += new EventHandler(this.textBox4_TextChanged);
            this.textBox5.TextChanged += new EventHandler(this.textBox5_TextChanged);
            this.textBox6.TextChanged += new EventHandler(this.textBox6_TextChanged);
            this.textBox7.TextChanged += new EventHandler(this.textBox7_TextChanged);
            this.textBox8.TextChanged += new EventHandler(this.textBox8_TextChanged);
            this.textBox9.TextChanged += new EventHandler(this.textBox9_TextChanged);
            this.textBox10.TextChanged += new EventHandler(this.textBox10_TextChanged);
            this.textBox11.TextChanged += new EventHandler(this.textBox11_TextChanged);
            this.textBox12.TextChanged += new EventHandler(this.textBox12_TextChanged);
            this.textBox13.TextChanged += new EventHandler(this.textBox13_TextChanged);
            this.textBox14.TextChanged += new EventHandler(this.textBox14_TextChanged);
            this.textBox15.TextChanged += new EventHandler(this.textBox15_TextChanged);
            this.textBox16.TextChanged += new EventHandler(this.textBox16_TextChanged);
            this.textBox17.TextChanged += new EventHandler(this.textBox17_TextChanged);
            this.textBox18.TextChanged += new EventHandler(this.textBox18_TextChanged);
            this.textBox22.TextChanged += new EventHandler(this.textBox22_TextChanged);
            this.textBox23.TextChanged += new EventHandler(this.textBox23_TextChanged);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(JDCXSDJEdite));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(800, 600);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "机动车专票单据编辑";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.JDCXSDJEdite\Aisino.Fwkp.Wbjk.JDCXSDJEdite.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(800, 600);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "JDCDJEdite";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "机动车专票单据编辑";
            base.Load += new EventHandler(this.XSDJEdite_Load);
            base.ResumeLayout(false);
        }

        private void initInvoice()
        {
            byte[] buffer = null;
            this.InvoiceKP = new Invoice(false, false, false, 12, buffer, null);
            this.InvoiceKP.set_Jdc_ver_new(true);
        }

        private void JDCXSDJEdite_FormClosing(object sender, EventArgs e)
        {
            this.XSDJ_close();
        }

        private void QuitBtnClick(object sender, EventArgs e)
        {
            this.XSDJ_close();
            base.Close();
        }

        private void SaveBtnClick(object sender, EventArgs e)
        {
            string str = "";
            this.ToModel();
            str = this.saleBillBL.Save(this.bill);
            if (str == "0")
            {
                string str2 = this.saleBillBL.CheckBill(this.bill);
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageManager.ShowMsgBox(str);
            }
        }

        private void SetCLXX(string name, int type)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetCL", new object[] { name, type, "MC,CPXH,CD,SCCJMC" });
            if ((objArray != null) && (objArray[0].ToString().CompareTo("Error") != 0))
            {
                this.textBox4.Text = objArray[0].ToString();
                this.textBox5.Text = objArray[1].ToString();
                this.textBox6.Text = objArray[2].ToString();
                this.textBox7.Text = objArray[3].ToString();
            }
        }

        private void SetGFXX(string name, int type)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetGHDW", new object[] { name, type, "MC,SH,IDCOC" });
            if ((objArray != null) && (objArray[0].ToString().CompareTo("Error") != 0))
            {
                this.textBox2.Text = objArray[0].ToString();
                this.textBox23.Text = objArray[1].ToString();
                this.textBox3.Text = objArray[2].ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            for (int i = ToolUtil.GetByteCount(text); i > 20; i = ToolUtil.GetByteCount(text))
            {
                int length = text.Length;
                text = text.Substring(0, length - 1);
            }
            this.textBox1.Text = text;
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox10.Text.Trim();
                this.InvoiceKP.set_Sjdh(str);
                string strB = this.InvoiceKP.get_Sjdh();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox10.Text = strB;
                    this.textBox10.SelectionStart = this.textBox10.Text.Length;
                }
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox11.Text.Trim();
                this.InvoiceKP.set_Fdjhm(str);
                string strB = this.InvoiceKP.get_Fdjhm();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox11.Text = strB;
                    this.textBox11.SelectionStart = this.textBox11.Text.Length;
                }
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox12.Text.Trim();
                this.InvoiceKP.set_Clsbdh_cjhm(str);
                string strB = this.InvoiceKP.get_Clsbdh_cjhm();
                if ((str.CompareTo(strB) != 0) && (strB.Length > 0))
                {
                    this.textBox12.Text = strB;
                    this.textBox12.SelectionStart = this.textBox12.Text.Length;
                }
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox13.Text.Trim();
                this.InvoiceKP.set_Dh(str);
                string strB = this.InvoiceKP.get_Dh();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox13.Text = strB;
                    this.textBox13.SelectionStart = this.textBox13.Text.Length;
                }
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox14.Text.Trim();
                this.InvoiceKP.set_Zh(str);
                string strB = this.InvoiceKP.get_Zh();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox14.Text = strB;
                    this.textBox14.SelectionStart = this.textBox14.Text.Length;
                }
            }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox15.Text.Trim();
                this.InvoiceKP.set_Dz(str);
                string strB = this.InvoiceKP.get_Dz();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox15.Text = strB;
                    this.textBox15.SelectionStart = this.textBox15.Text.Length;
                }
            }
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox16.Text.Trim();
                this.InvoiceKP.set_Khyh(str);
                string strB = this.InvoiceKP.get_Khyh();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox16.Text = strB;
                    this.textBox16.SelectionStart = this.textBox16.Text.Length;
                }
            }
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox17.Text.Trim();
                this.InvoiceKP.set_Dw(str);
                string strB = this.InvoiceKP.get_Dw();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox17.Text = strB;
                    this.textBox17.SelectionStart = this.textBox17.Text.Length;
                }
            }
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox18.Text.Trim();
                this.InvoiceKP.set_Xcrs(str);
                string strB = this.InvoiceKP.get_Xcrs();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox18.Text = strB;
                    this.textBox18.SelectionStart = this.textBox18.Text.Length;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox2.Text.Trim();
                this.InvoiceKP.set_Gfmc(str);
                string strB = this.InvoiceKP.get_Gfmc();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox2.Text = strB;
                    this.textBox2.SelectionStart = this.textBox2.Text.Length;
                }
            }
        }

        private void textBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void textBox20_LostFocus(object sender, EventArgs e)
        {
            string s = this.textBox20.Text.Trim();
            double result = 0.0;
            if (double.TryParse(s, out result))
            {
                this.textBox20.Text = result.ToString();
            }
            else
            {
                this.textBox20.LostFocus -= new EventHandler(this.textBox20_LostFocus);
                string[] strArray = null;
                MessageManager.ShowMsgBox("A066", strArray);
                this.textBox20.Focus();
                this.textBox20.LostFocus += new EventHandler(this.textBox20_LostFocus);
            }
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox22.Text.Trim();
                this.InvoiceKP.set_Bz(str);
                string strB = this.InvoiceKP.get_Bz();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox22.Text = strB;
                    this.textBox22.SelectionStart = this.textBox22.Text.Length;
                }
            }
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox23.Text.Trim();
                this.InvoiceKP.set_Gfsh(str);
                string strB = this.InvoiceKP.get_Gfsh();
                if ((str.CompareTo(strB) != 0) && (strB.Length > 0))
                {
                    this.textBox23.Text = strB;
                    this.textBox23.SelectionStart = this.textBox23.Text.Length;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox3.Text.Trim();
                this.InvoiceKP.set_Sfzh_zzjgdm(str);
                string strB = this.InvoiceKP.get_Sfzh_zzjgdm();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox3.Text = strB;
                    this.textBox3.SelectionStart = this.textBox3.Text.Length;
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox4.Text.Trim();
                this.InvoiceKP.set_Cllx(str);
                string strB = this.InvoiceKP.get_Cllx();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox4.Text = strB;
                    this.textBox4.SelectionStart = this.textBox4.Text.Length;
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox5.Text.Trim();
                this.InvoiceKP.set_Cpxh(str);
                string strB = this.InvoiceKP.get_Cpxh();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox5.Text = strB;
                    this.textBox5.SelectionStart = this.textBox5.Text.Length;
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox6.Text.Trim();
                this.InvoiceKP.set_Cd(str);
                string strB = this.InvoiceKP.get_Cd();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox6.Text = strB;
                    this.textBox6.SelectionStart = this.textBox6.Text.Length;
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox7.Text.Trim();
                this.InvoiceKP.set_Sccjmc(str);
                string strB = this.InvoiceKP.get_Sccjmc();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox7.Text = strB;
                    this.textBox7.SelectionStart = this.textBox7.Text.Length;
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox8.Text.Trim();
                this.InvoiceKP.set_Hgzh(str);
                string strB = this.InvoiceKP.get_Hgzh();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox8.Text = strB;
                    this.textBox8.SelectionStart = this.textBox8.Text.Length;
                }
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (this.InvoiceKP != null)
            {
                string str = this.textBox9.Text.Trim();
                this.InvoiceKP.set_Jkzmsh(str);
                string strB = this.InvoiceKP.get_Jkzmsh();
                if (str.CompareTo(strB) != 0)
                {
                    this.textBox9.Text = strB;
                    this.textBox9.SelectionStart = this.textBox9.Text.Length;
                }
            }
        }

        private void ToModel()
        {
            this.bill.BH = this.textBox1.Text.Trim();
            this.bill.GFMC = this.textBox2.Text.Trim();
            this.bill.GFSH = this.textBox3.Text.Trim();
            this.bill.GFDZDH = this.textBox4.Text.Trim();
            this.bill.XFDZ = this.textBox5.Text.Trim();
            this.bill.KHYHMC = this.textBox6.Text.Trim();
            this.bill.SCCJMC = this.textBox7.Text.Trim();
            this.bill.CM = this.textBox8.Text.Trim();
            this.bill.TYDH = this.textBox9.Text.Trim();
            this.bill.QYD = this.textBox10.Text.Trim();
            this.bill.ZHD = this.textBox11.Text.Trim();
            this.bill.XHD = this.textBox12.Text.Trim();
            this.bill.XFDH = this.textBox13.Text.Trim();
            this.bill.KHYHZH = this.textBox14.Text.Trim();
            this.bill.XFDZDH = this.textBox15.Text.Trim();
            this.bill.XFYHZH = this.textBox16.Text.Trim();
            this.bill.DW = this.textBox17.Text.Trim();
            this.bill.MDD = this.textBox18.Text.Trim();
            this.bill.DJRQ = this.dateTimePicker1.Value.Date;
            this.bill.DJYF = this.bill.DJRQ.Month;
            if (this.textBox21.Text.Trim() == "")
            {
                this.bill.SLV = 0.0;
            }
            else
            {
                this.bill.SLV = CommonTool.Todouble(this.textBox21.Text.Trim());
            }
            if (this.textBox20.Text.Trim() == "")
            {
                this.bill.JEHJ = 0.0;
            }
            else
            {
                this.bill.JEHJ = CommonTool.Todouble(this.textBox20.Text.Trim());
                this.bill.JEHJ = SaleBillCtrl.GetRound(this.bill.JEHJ, 2);
            }
            this.bill.BZ = this.textBox22.Text.Trim();
            this.bill.GFYHZH = this.textBox23.Text.Trim();
            this.bill.DJZL = "j";
        }

        private void ToView()
        {
            this.textBox1.Text = this.bill.BH;
            this.textBox2.Text = this.bill.GFMC;
            this.textBox3.Text = this.bill.GFSH;
            this.textBox4.Text = this.bill.GFDZDH;
            this.textBox5.Text = this.bill.XFDZ;
            this.textBox6.Text = this.bill.KHYHMC;
            this.textBox7.Text = this.bill.SCCJMC;
            this.textBox8.Text = this.bill.CM;
            this.textBox9.Text = this.bill.TYDH;
            this.textBox10.Text = this.bill.QYD;
            this.textBox11.Text = this.bill.ZHD;
            this.textBox12.Text = this.bill.XHD;
            this.textBox13.Text = this.bill.XFDH;
            this.textBox14.Text = this.bill.KHYHZH;
            this.textBox15.Text = this.bill.XFDZDH;
            this.textBox16.Text = this.bill.XFYHZH;
            this.textBox17.Text = this.bill.DW;
            this.textBox18.Text = this.bill.MDD;
            this.textBox21.Text = this.bill.SLV.ToString();
            this.textBox22.Text = this.bill.BZ;
            this.textBox23.Text = this.bill.GFYHZH;
            double round = this.bill.JEHJ * (1.0 + this.bill.SLV);
            round = SaleBillCtrl.GetRound(round, 2);
            this.textBox20.Text = this.bill.JEHJ.ToString();
            this.dateTimePicker1.Text = this.bill.DJRQ.ToShortDateString();
        }

        private void XSDJ_close()
        {
            this.textBox20.LostFocus -= new EventHandler(this.textBox20_LostFocus);
        }

        private void XSDJEdite_Load(object sender, EventArgs e)
        {
        }
    }
}

