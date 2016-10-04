namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AddZKH : BaseForm
    {
        private SaleBill bill;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components;
        private bool ContainTax;
        private DataGridView dataGridMX;
        public bool DiscountAdded;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL label6;
        private AisinoLBL labelSPJE;
        private ILog log;
        private SaleBillCtrl saleLogic;
        private int selectIndex;
        private double slv;
        private AisinoTXT textBoxZKHS;
        private AisinoTXT textBoxZKJE;
        private AisinoTXT textBoxZKL;

        public AddZKH(SaleBill bill, int selectIndex)
        {
            this.components = null;
            this.log = LogUtil.GetLogger<AddZKH>();
            this.ContainTax = false;
            this.bill = null;
            this.selectIndex = 0;
            this.DiscountAdded = false;
            this.saleLogic = SaleBillCtrl.Instance;
            this.slv = 0.0;
            this.InitializeComponent();
            this.textBoxZKHS.TextChanged += new EventHandler(this.textBoxZKHS_TextChanged);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.AddZkhDJ_Load);
            this.textBoxZKJE.TextChanged += new EventHandler(this.textBoxZKJE_TextChanged);
            this.textBoxZKL.TextChanged += new EventHandler(this.textBoxZKL_TextChanged);
            this.textBoxZKL.KeyDown += new KeyEventHandler(this.textBoxZKL_KeyDown);
            this.bill = bill;
            this.selectIndex = selectIndex;
            this.textBoxZKHS.Text = "1";
            this.textBoxZKHS.Focus();
            this.btnOK.Enabled = false;
        }

        public AddZKH(bool ContainTax, DataGridView dataGridMX1)
        {
            this.components = null;
            this.log = LogUtil.GetLogger<AddZKH>();
            this.ContainTax = false;
            this.bill = null;
            this.selectIndex = 0;
            this.DiscountAdded = false;
            this.saleLogic = SaleBillCtrl.Instance;
            this.slv = 0.0;
            this.InitializeComponent();
            this.dataGridMX = dataGridMX1;
            this.ContainTax = ContainTax;
            this.textBoxZKHS.Text = "1";
            this.textBoxZKHS.Focus();
            this.btnOK.Enabled = false;
        }

        private void AddZkhDJ_Load(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaleBillCheck check = new SaleBillCheck();
            if (check.CheckCEZS(this.bill))
            {
                if (this.bill.ListGoods.Count > 1)
                {
                    MessageManager.ShowMsgBox("差额税单据，只能有一行商品");
                    return;
                }
                double result = 0.0;
                double.TryParse(this.textBoxZKJE.Text.Trim(), out result);
                double num2 = (this.bill.ListGoods[this.selectIndex].JE + this.bill.ListGoods[this.selectIndex].SE) - this.bill.ListGoods[this.selectIndex].KCE;
                if (!this.bill.ContainTax)
                {
                    num2 = this.bill.ListGoods[this.selectIndex].JE - this.bill.ListGoods[this.selectIndex].KCE;
                }
                num2 = Math.Round(num2, 2);
                if (result > num2)
                {
                    MessageManager.ShowMsgBox("差额税单据，折扣金额大于差额：" + num2.ToString());
                    return;
                }
            }
            this.ComfirmAddZK();
        }

        private void ComfirmAddZK()
        {
            try
            {
                int result = 0;
                int.TryParse(this.textBoxZKHS.Text, out result);
                int selectIndex = this.selectIndex;
                double round = SaleBillCtrl.GetRound(Convert.ToDouble(this.textBoxZKL.Text), 3) / 100.0;
                round = SaleBillCtrl.GetRound(round, 5);
                double num4 = 0.0;
                double.TryParse(this.textBoxZKJE.Text.Trim(), out num4);
                if (CommonTool.isSPBMVersion())
                {
                    this.saleLogic.DisCount2(this.bill, selectIndex, result, round, num4);
                }
                else
                {
                    this.saleLogic.DisCount(this.bill, selectIndex, result, round, num4, 0.0);
                }
                this.DiscountAdded = true;
                base.Close();
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

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AddZKH));
            this.textBoxZKJE = new AisinoTXT();
            this.label6 = new AisinoLBL();
            this.textBoxZKL = new AisinoTXT();
            this.textBoxZKHS = new AisinoTXT();
            this.label5 = new AisinoLBL();
            this.label4 = new AisinoLBL();
            this.labelSPJE = new AisinoLBL();
            this.label1 = new AisinoLBL();
            this.label2 = new AisinoLBL();
            this.btnCancel = new AisinoBTN();
            this.btnOK = new AisinoBTN();
            base.SuspendLayout();
            this.textBoxZKJE.Location = new Point(0x6b, 0x7f);
            this.textBoxZKJE.Name = "textBoxZKJE";
            this.textBoxZKJE.Size = new Size(0x79, 0x15);
            this.textBoxZKJE.TabIndex = 2;
            this.label6.AutoSize = true;
            this.label6.BackColor = Color.Transparent;
            this.label6.Location = new Point(0xea, 0x5c);
            this.label6.Name = "label6";
            this.label6.Size = new Size(11, 12);
            this.label6.TabIndex = 0x27;
            this.label6.Text = "%";
            this.textBoxZKL.Location = new Point(0x6b, 0x59);
            this.textBoxZKL.Name = "textBoxZKL";
            this.textBoxZKL.Size = new Size(0x79, 0x15);
            this.textBoxZKL.TabIndex = 1;
            this.textBoxZKHS.Location = new Point(0x6b, 0x1c);
            this.textBoxZKHS.Name = "textBoxZKHS";
            this.textBoxZKHS.Size = new Size(0x79, 0x15);
            this.textBoxZKHS.TabIndex = 0;
            this.label5.AutoSize = true;
            this.label5.BackColor = Color.Transparent;
            this.label5.Location = new Point(0x30, 0x7f);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 0x24;
            this.label5.Text = "折扣金额";
            this.label4.AutoSize = true;
            this.label4.BackColor = Color.Transparent;
            this.label4.Location = new Point(0x30, 0x5c);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x29, 12);
            this.label4.TabIndex = 0x23;
            this.label4.Text = "折扣率";
            this.labelSPJE.AutoSize = true;
            this.labelSPJE.BackColor = Color.Transparent;
            this.labelSPJE.Location = new Point(0x80, 60);
            this.labelSPJE.Name = "labelSPJE";
            this.labelSPJE.Size = new Size(11, 12);
            this.labelSPJE.TabIndex = 0x22;
            this.labelSPJE.Text = "0";
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Location = new Point(0x30, 0x1f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "折扣行数";
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.Location = new Point(0x30, 60);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0x1f;
            this.label2.Text = "商品金额";
            this.btnCancel.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnCancel.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnCancel.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnCancel.Font = new Font("宋体", 9f);
            this.btnCancel.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnCancel.Location = new Point(0x99, 0xb1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnOK.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnOK.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnOK.Font = new Font("宋体", 9f);
            this.btnOK.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnOK.Location = new Point(50, 0xb1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0xd8);
            base.Controls.Add(this.textBoxZKJE);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.textBoxZKL);
            base.Controls.Add(this.textBoxZKHS);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.labelSPJE);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AddZKH";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "添加折扣行";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void textBoxZKHS_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            double num2 = 0.0;
            double zkje = 0.0;
            double totalAmount = 0.0;
            if (!int.TryParse(this.textBoxZKHS.Text, out result))
            {
                this.textBoxZKHS.Text = "1";
            }
            else
            {
                if ((this.textBoxZKL.Text.Trim().Length > 0) && double.TryParse(this.textBoxZKL.Text.Trim(), out num2))
                {
                    num2 = Finacial.Div(num2, 100.0, 15);
                    if (num2 > 1.0)
                    {
                        this.btnOK.Enabled = false;
                    }
                    else
                    {
                        this.btnOK.Enabled = true;
                    }
                }
                this.saleLogic.DisCountChangeRowNO(this.bill, this.selectIndex, ref result, num2, ref totalAmount, ref zkje);
                zkje = Finacial.GetRound(zkje, 2);
                this.textBoxZKHS.Text = result.ToString();
                this.labelSPJE.Text = totalAmount.ToString();
                this.textBoxZKJE.Text = zkje.ToString("0.00");
                if (zkje == 0.0)
                {
                    this.btnOK.Enabled = false;
                }
            }
        }

        private void textBoxZKJE_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.textBoxZKJE.Focused)
                {
                    if (this.textBoxZKJE.Text.Trim().Length > 0)
                    {
                        double result = 0.0;
                        if (double.TryParse(this.textBoxZKJE.Text.Trim(), out result))
                        {
                            result = SaleBillCtrl.GetRound(result, 2);
                            double num3 = 0.0;
                            double num4 = 0.0;
                            double.TryParse(this.labelSPJE.Text, out num3);
                            num4 = (num3 != 0.0) ? (result / num3) : result;
                            num4 = Finacial.Mul(num4, 100.0, 3);
                            double num5 = 0.0;
                            double.TryParse(this.labelSPJE.Text.Trim(), out num5);
                            if ((result <= num5) && (((num4 >= 0.001) && (num4 <= 100.0)) || ((result >= 0.01) && (num4 == 0.0))))
                            {
                                this.textBoxZKL.Text = num4.ToString("0.000");
                                this.btnOK.Enabled = true;
                                return;
                            }
                        }
                        else
                        {
                            this.textBoxZKJE.Text = "0.00";
                        }
                    }
                    this.textBoxZKL.Text = "0.000";
                    this.btnOK.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void textBoxZKL_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter) && this.btnOK.Enabled)
            {
                this.ComfirmAddZK();
            }
        }

        private void textBoxZKL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.textBoxZKL.Focused)
                {
                    if (this.textBoxZKL.Text.Trim().Length > 0)
                    {
                        double result = 0.0;
                        if (double.TryParse(this.textBoxZKL.Text.Trim(), out result))
                        {
                            double num2 = result;
                            result = SaleBillCtrl.GetRound(result, 3);
                            if ((result >= 0.001) && (result <= 100.0))
                            {
                                double round = SaleBillCtrl.GetRound(SaleBillCtrl.GetRound((double) (SaleBillCtrl.GetRound((double) (result / 100.0), 5) * Convert.ToDouble(this.labelSPJE.Text)), 5), 2);
                                this.textBoxZKJE.Text = round.ToString("0.00");
                                if (round > 0.0)
                                {
                                    this.btnOK.Enabled = true;
                                }
                                else
                                {
                                    this.btnOK.Enabled = false;
                                }
                                return;
                            }
                        }
                        else
                        {
                            this.textBoxZKL.Text = "";
                        }
                    }
                    this.textBoxZKJE.Text = "0.00";
                    this.btnOK.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }
    }
}

