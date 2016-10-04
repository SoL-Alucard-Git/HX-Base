using Aisino.Framework.Plugin.Core;
using Aisino.Framework.Plugin.Core.Controls;
using Aisino.Framework.Plugin.Core.Util;
using Aisino.Fwkp.BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Aisino.Fwkp.Fptk.Common.Forms
{
    public class ZheKou : BaseForm
    {
        private AisinoBTN aisinoBTN_0;
        private AisinoBTN aisinoBTN_1;
        private AisinoLBL aisinoLBL_0;
        private AisinoTXT aisinoTXT_0;
        private AisinoTXT aisinoTXT_1;
        private AisinoTXT aisinoTXT_2;
        private bool bool_0;
        private IContainer icontainer_2;
        private int int_0;
        private Invoice invoice_0;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private XmlComponentLoader xmlComponentLoader1;

        public ZheKou(Invoice invoice_1, int int_1, bool bool_1)
        {
            
            this.method_3();
            this.int_0 = int_1;
            this.invoice_0 = invoice_1;
            this.bool_0 = bool_1;
        }

        private void aisinoBTN_0_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void aisinoBTN_1_Click(object sender, EventArgs e)
        {
            if (this.isWM())
            {
                if (this.invoice_0.AddZkxx(this.int_0, int.Parse(this.string_0), this.string_2, this.string_3) > 0)
                {
                    base.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageManager.ShowMsgBox(this.invoice_0.GetCode(), this.invoice_0.Params);
                }
            }
            else
            {
                try
                {
                    int num10;
                    int num11;
                    int num12;
                    decimal num2 = decimal.Parse(this.aisinoTXT_1.Text);
                    int num3 = int.Parse(this.string_0);
                    int num4 = (this.int_0 - num3) + 1;
                    int num5 = this.int_0 + num3;
                    decimal[] numArray = new decimal[num3];
                    decimal num6 = -decimal.Parse(this.string_2);
                    decimal num7 = 0M;
                    int index = 0;
                    for (int i = 0; i < num3; i++)
                    {
                        decimal num16 = decimal.Parse(this.invoice_0.GetSpxx(num4 + i)[SPXX.JE]);
                        numArray[i] = Math.Round((decimal) (num16 * num6), 2);
                        num7 += numArray[i];
                        if (numArray[i] < numArray[index])
                        {
                            index = i;
                        }
                    }
                    num7 = num2 + num7;
                    if (num7 != 0M)
                    {
                        numArray[index] += -num7;
                    }
                    num5 = (num4 + (2 * num3)) - 1;
                    if (this.invoice_0.Zyfplx == ZYFP_LX.CEZS)
                    {
                        num10 = 0;
                        while (num10 < num3)
                        {
                            Dictionary<SPXX, string> spxx = this.invoice_0.GetSpxx(num4 + num10);
                            decimal num13 = decimal.Parse(spxx[SPXX.KCE]);
                            if (((decimal.Parse(spxx[SPXX.JE]) - num13) + numArray[num10]) < 0M)
                            {
                                goto Label_0201;
                            }
                            num10++;
                        }
                    }
                    goto Label_022A;
                Label_0201:
                    num11 = (num4 + num10) + 1;
                    MessageBox.Show("第" + num11.ToString() + "行商品的折扣额大于扣除额，不能添加折扣！");
                    return;
                Label_022A:
                    num12 = num4;
                    for (int j = 0; num12 < num5; j++)
                    {
                        Dictionary<SPXX, string> dictionary2 = this.invoice_0.GetSpxx(num12);
                        if (numArray[j] == 0M)
                        {
                            num5--;
                        }
                        else
                        {
                            this.invoice_0.AddZkxx(num12, 1, this.string_2, numArray[j].ToString());
                            this.invoice_0.SetSpbh(num12 + 1, dictionary2[SPXX.SPBH]);
                            this.invoice_0.SetXsyh(num12 + 1, dictionary2[SPXX.XSYH]);
                            this.invoice_0.SetLslvbs(num12 + 1, dictionary2[SPXX.LSLVBS]);
                            this.invoice_0.SetYhsm(num12 + 1, dictionary2[SPXX.YHSM]);
                            this.invoice_0.SetFlbm(num12 + 1, dictionary2[SPXX.FLBM]);
                            this.invoice_0.SetSpmc(num12 + 1, dictionary2[SPXX.SPMC]);
                            num12++;
                        }
                        num12++;
                    }
                    base.DialogResult = DialogResult.OK;
                }
                catch (Exception)
                {
                    MessageManager.ShowMsgBox(this.invoice_0.GetCode(), this.invoice_0.Params);
                }
            }
        }

        private void aisinoTXT_0_TextChanged(object sender, EventArgs e)
        {
            double num;
            if (!double.TryParse(this.aisinoTXT_0.Text, out num))
            {
                this.aisinoTXT_0.Text = "";
            }
            if (this.isWM())
            {
                this.string_0 = this.aisinoTXT_0.Text;
                if ((this.string_0.Length == 0) || (int.Parse(this.string_0) == 0))
                {
                    this.aisinoLBL_0.Text = "0.00";
                    this.aisinoTXT_1.Text = "";
                    this.aisinoTXT_2.Text = "";
                    this.string_1 = "";
                    this.method_1();
                    return;
                }
            }
            else
            {
                this.string_0 = this.aisinoTXT_0.Text;
                if ((this.string_0.Length == 0) || (int.Parse(this.string_0) == 0))
                {
                    this.aisinoLBL_0.Text = "0.00";
                    this.aisinoTXT_1.Text = "";
                    this.aisinoTXT_2.Text = "";
                    this.string_1 = "";
                    this.method_1();
                    return;
                }
                List<Dictionary<SPXX, string>> spxxs = this.invoice_0.GetSpxxs();
                int count = spxxs.Count;
                int num3 = this.int_0;
                while (num3 >= 0)
                {
                    if ((spxxs[num3][SPXX.FPHXZ] == "4") || (spxxs[num3][SPXX.SPMC] == ""))
                    {
                        break;
                    }
                    num3--;
                }
                if ((num3 >= 0) && (int.Parse(this.string_0) > (this.int_0 - num3)))
                {
                    string[] para = new string[] { (this.int_0 - num3).ToString() };
                    MessageManager.ShowMsgBox("INP-242205", para);
                    this.aisinoLBL_0.Text = "0.00";
                    this.aisinoTXT_1.Text = "";
                    this.aisinoTXT_2.Text = "";
                    this.string_1 = "";
                    this.method_1();
                    base.Close();
                    return;
                }
                if (this.bool_0)
                {
                    if ((this.invoice_0.Fplx == FPLX.JSFP) && ((count + int.Parse(this.string_0)) > 6))
                    {
                        string[] strArray2 = new string[] { (6 - count).ToString() };
                        MessageManager.ShowMsgBox("INP-242206", strArray2);
                        this.aisinoLBL_0.Text = "0.00";
                        this.aisinoTXT_1.Text = "";
                        this.aisinoTXT_2.Text = "";
                        this.string_1 = "";
                        this.method_1();
                        return;
                    }
                    if (((this.invoice_0.Fplx == FPLX.ZYFP) || (this.invoice_0.Fplx == FPLX.PTFP)) && ((count + int.Parse(this.string_0)) > (this.invoice_0.Hzfw ? 7 : 8)))
                    {
                        string[] strArray6 = new string[] { ((this.invoice_0.Hzfw ? 7 : 8) - count).ToString() };
                        MessageManager.ShowMsgBox("INP-242206", strArray6);
                        this.aisinoLBL_0.Text = "0.00";
                        this.aisinoTXT_1.Text = "";
                        this.aisinoTXT_2.Text = "";
                        this.string_1 = "";
                        this.method_1();
                        return;
                    }
                    if ((this.invoice_0.Fplx == FPLX.DZFP) && ((count + int.Parse(this.string_0)) > 100))
                    {
                        string[] strArray4 = new string[] { (100 - count).ToString() };
                        MessageManager.ShowMsgBox("INP-242206", strArray4);
                        this.aisinoLBL_0.Text = "0.00";
                        this.aisinoTXT_1.Text = "";
                        this.aisinoTXT_2.Text = "";
                        this.string_1 = "";
                        this.method_1();
                        return;
                    }
                }
                else
                {
                    if (base.TaxCardInstance.IsLargeInvDetail && ((count + int.Parse(this.string_0)) > 0x176f))
                    {
                        int num8 = ((0x176f - count) > 0) ? (0x176f - count) : 0;
                        MessageManager.ShowMsgBox("INP-242206", new string[] { num8.ToString() });
                        this.aisinoLBL_0.Text = "0.00";
                        this.aisinoTXT_1.Text = "";
                        this.aisinoTXT_2.Text = "";
                        this.string_1 = "";
                        this.method_1();
                        return;
                    }
                    if (!base.TaxCardInstance.IsLargeInvDetail && ((count + int.Parse(this.string_0)) > 0x95))
                    {
                        int num10 = ((0x95 - count) > 0) ? (0x95 - count) : 0;
                        MessageManager.ShowMsgBox("INP-242206", new string[] { num10.ToString() });
                        this.aisinoLBL_0.Text = "0.00";
                        this.aisinoTXT_1.Text = "";
                        this.aisinoTXT_2.Text = "";
                        this.string_1 = "";
                        this.method_1();
                        return;
                    }
                }
            }
            this.string_1 = this.invoice_0.GetZkSpJe(this.int_0, int.Parse(this.string_0));
            if (this.string_1 == null)
            {
                MessageManager.ShowMsgBox(this.invoice_0.GetCode(), this.invoice_0.Params);
            }
            else
            {
                this.aisinoLBL_0.Text = this.string_1;
                this.string_2 = this.aisinoTXT_2.Text;
                if (this.string_2.Length > 0)
                {
                    this.string_2 = decimal.Divide(decimal.Parse(this.string_2), decimal.Parse("100")).ToString();
                    this.string_3 = this.invoice_0.GetZke(this.int_0, int.Parse(this.string_0), this.string_2);
                    if (this.string_3 == null)
                    {
                        MessageManager.ShowMsgBox(this.invoice_0.GetCode(), this.invoice_0.Params);
                    }
                    else
                    {
                        this.aisinoTXT_1.Text = this.string_3.Substring(1);
                    }
                }
                else
                {
                    this.string_3 = this.aisinoTXT_1.Text;
                    if (this.string_3.Length > 0)
                    {
                        this.string_2 = this.invoice_0.GetZkLv(this.int_0, int.Parse(this.string_0), this.string_3);
                        if (this.string_2 == null)
                        {
                            MessageManager.ShowMsgBox(this.invoice_0.GetCode(), this.invoice_0.Params);
                        }
                        else
                        {
                            this.aisinoTXT_2.Text = decimal.Multiply(decimal.Parse(this.string_2), decimal.Parse("100")).ToString();
                        }
                    }
                }
            }
            this.method_1();
        }

        private void aisinoTXT_1_TextChanged(object sender, EventArgs e)
        {
            double num;
            if (!double.TryParse(this.aisinoTXT_1.Text, out num))
            {
                this.aisinoTXT_1.Text = "";
            }
            this.string_3 = this.aisinoTXT_1.Text;
            if ((this.string_3.Length > 0) && !this.string_3.Equals("."))
            {
                this.string_3 = new StringBuilder("-").Append(this.string_3).ToString();
                this.string_2 = this.invoice_0.GetZkLv(this.int_0, int.Parse(this.string_0), this.string_3);
                if (this.string_2 == null)
                {
                    MessageManager.ShowMsgBox(this.invoice_0.GetCode(), this.invoice_0.Params);
                }
                else
                {
                    this.aisinoTXT_2.TextChanged -= new EventHandler(this.aisinoTXT_2_TextChanged);
                    this.aisinoTXT_2.Text = decimal.Round(decimal.Multiply(decimal.Parse(this.string_2), decimal.Parse("100")), 3, MidpointRounding.AwayFromZero).ToString();
                    this.aisinoTXT_2.TextChanged += new EventHandler(this.aisinoTXT_2_TextChanged);
                }
            }
            else
            {
                this.aisinoTXT_2.Text = "";
            }
            this.method_1();
        }

        private void aisinoTXT_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            AisinoTXT otxt = (AisinoTXT) sender;
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '.')
            {
                if (otxt.Name.Equals("txt_zkhs"))
                {
                    e.Handled = true;
                }
                else if (otxt.Text.IndexOf(".") >= 0)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else if ((Convert.ToInt32(e.KeyChar) < 0x30) || (Convert.ToInt32(e.KeyChar) > 0x39))
            {
                e.Handled = true;
            }
        }

        private void aisinoTXT_2_TextChanged(object sender, EventArgs e)
        {
            double num;
            if (!double.TryParse(this.aisinoTXT_2.Text, out num))
            {
                this.aisinoTXT_2.Text = "";
            }
            this.string_2 = this.aisinoTXT_2.Text;
            if ((this.string_2.Length > 0) && !this.string_2.Equals("."))
            {
                this.string_2 = decimal.Round(decimal.Divide(decimal.Parse(this.string_2), decimal.Parse("100")), 5, MidpointRounding.AwayFromZero).ToString();
                this.string_3 = this.invoice_0.GetZke(this.int_0, int.Parse(this.string_0), this.string_2);
                if (this.string_3 == null)
                {
                    MessageManager.ShowMsgBox(this.invoice_0.GetCode(), this.invoice_0.Params);
                }
                else
                {
                    this.aisinoTXT_1.TextChanged -= new EventHandler(this.aisinoTXT_1_TextChanged);
                    this.aisinoTXT_1.Text = (this.string_3 == "0.00") ? "0.00" : this.string_3.Substring(1);
                    this.aisinoTXT_1.TextChanged += new EventHandler(this.aisinoTXT_1_TextChanged);
                }
            }
            else
            {
                this.aisinoTXT_1.Text = "";
            }
            this.method_1();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_2 != null))
            {
                this.icontainer_2.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool isWM()
        {
            return (TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") != "FLBM");
        }

        private void method_1()
        {
            decimal num;
            decimal num2;
            if ((this.string_1 != null) && (this.string_1.Length != 0))
            {
                this.aisinoTXT_2.Enabled = true;
                this.aisinoTXT_2.BackColor = Color.White;
                this.aisinoTXT_1.Enabled = true;
                this.aisinoTXT_1.BackColor = Color.White;
            }
            else
            {
                this.aisinoTXT_2.Enabled = false;
                this.aisinoTXT_2.BackColor = Color.LightGray;
                this.aisinoTXT_1.Enabled = false;
                this.aisinoTXT_1.BackColor = Color.LightGray;
            }
            decimal.TryParse(this.aisinoTXT_2.Text.Trim(), out num);
            decimal.TryParse(this.aisinoTXT_1.Text.Trim(), out num2);
            if (((((this.string_0 != null) && (this.string_0.Length != 0)) && (int.Parse(this.string_0) != 0)) && ((Math.Abs(num).CompareTo(decimal.Parse("0.001")) >= 0) || (Math.Abs(num2).CompareTo(decimal.Parse("0.01")) >= 0))) && ((((Math.Abs(num2).CompareTo(decimal.Parse("0.01")) >= 0) && (this.string_3 != null)) && ((this.string_2 != null) && (this.string_1 != null))) && (this.aisinoTXT_1.Enabled && this.aisinoTXT_2.Enabled)))
            {
                this.aisinoBTN_1.Enabled = true;
            }
            else
            {
                this.aisinoBTN_1.Enabled = false;
            }
        }

        private void method_2()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ZheKou));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x158, 0xd4);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Text = "折扣";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.ZheKou\Aisino.Fwkp.Fpkj.Form.ZheKou.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x158, 0xd4);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ZheKou";
            this.Text = "添加折扣行";
            base.Load += new EventHandler(this.ZheKou_Load);
            base.ResumeLayout(false);
        }

        private void method_3()
        {
            this.method_2();
            this.aisinoBTN_0 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button1");
            this.aisinoBTN_1 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button2");
            this.aisinoLBL_0 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("txt_spje");
            this.aisinoTXT_1 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_zkje");
            this.aisinoTXT_2 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_zkLv");
            this.aisinoTXT_0 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_zkhs");
            this.aisinoTXT_0.TextChanged += new EventHandler(this.aisinoTXT_0_TextChanged);
            this.aisinoTXT_2.TextChanged += new EventHandler(this.aisinoTXT_2_TextChanged);
            this.aisinoTXT_1.TextChanged += new EventHandler(this.aisinoTXT_1_TextChanged);
            this.aisinoTXT_0.KeyPress += new KeyPressEventHandler(this.aisinoTXT_2_KeyPress);
            this.aisinoTXT_1.KeyPress += new KeyPressEventHandler(this.aisinoTXT_2_KeyPress);
            this.aisinoTXT_2.KeyPress += new KeyPressEventHandler(this.aisinoTXT_2_KeyPress);
            this.aisinoBTN_1.Click += new EventHandler(this.aisinoBTN_1_Click);
            this.aisinoBTN_0.Click += new EventHandler(this.aisinoBTN_0_Click);
        }

        internal static string smethod_0(string string_4, int int_1)
        {
            return decimal.Round(decimal.Parse(string_4), int_1, MidpointRounding.AwayFromZero).ToString("f" + int_1);
        }

        private void ZheKou_Load(object sender, EventArgs e)
        {
            this.aisinoTXT_0.Text = "1";
            this.string_0 = "1";
            this.aisinoLBL_0.Text = this.invoice_0.GetZkSpJe(this.int_0, 1);
            this.string_1 = this.aisinoLBL_0.Text;
            this.aisinoTXT_0.Focus();
            this.aisinoBTN_1.Enabled = false;
        }
    }
}

