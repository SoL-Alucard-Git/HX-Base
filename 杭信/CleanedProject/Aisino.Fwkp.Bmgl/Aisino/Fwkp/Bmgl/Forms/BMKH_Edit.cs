namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BMKH_Edit : BMEditBase
    {
        private AisinoCMB comboBoxID;
        private AisinoCMB comboBoxKJM;
        private IContainer components;
        private InvoiceHandler invoiceHandler;
        private BMKHManager khlogical;
        private BMKHModel khModel;
        private AisinoLBL label11;
        private AisinoLBL label13;
        private AisinoLBL label14;
        private AisinoLBL label15;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL label6;
        private AisinoLBL label7;
        private AisinoLBL label9;
        private AisinoLBL labelKJM;
        private AisinoTXT textBoxBZ;
        private AisinoTXT textBoxDQBM;
        private AisinoTXT textBoxDQKM;
        private AisinoTXT textBoxDQMC;
        private AisinoTXT textBoxDZDH;
        private AisinoTXT textBoxSH;
        private AisinoTXT textBoxYHZH;
        private AisinoTXT textBoxYJDZ;
        private AisinoTXT textBoxYSKM;

        public BMKH_Edit()
        {
            this.khlogical = new BMKHManager();
            this.invoiceHandler = new InvoiceHandler();
            this.khModel = new BMKHModel();
            this.Initialize();
        }

        public BMKH_Edit(string SJBM, BMKH Father) : base(SJBM, Father)
        {
            this.khlogical = new BMKHManager();
            this.invoiceHandler = new InvoiceHandler();
            this.khModel = new BMKHModel();
            this.Initialize();
            this.InitData();
        }

        public BMKH_Edit(string BM, bool Isupdate) : base(BM, Isupdate)
        {
            this.khlogical = new BMKHManager();
            this.invoiceHandler = new InvoiceHandler();
            this.khModel = new BMKHModel();
            this.Initialize();
            this.InitData();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void InitData()
        {
            if (base.isUpdate)
            {
                this.Text = "客户编码编辑";
            }
            else
            {
                this.Text = "客户编码添加";
            }
            base.lblMC.Text = "*客户名称";
            base.baseLogical = this.khlogical;
            base.log = LogUtil.GetLogger<BMKH_Edit>();
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.textBoxWaitMC.WaterMarkString = "客户名称";
            this.comboBoxID.Items.AddRange(new object[] { "是", "否" });
            this.textBoxYJDZ.TextChanged += new EventHandler(this.textBoxYJDZ_TextChanged);
            this.textBoxDZDH.TextChanged += new EventHandler(StringUtils.textBoxYHZH_DZDH_TextChanged);
            this.textBoxYHZH.TextChanged += new EventHandler(StringUtils.textBoxYHZH_DZDH_TextChanged);
            this.textBoxBZ.TextChanged += new EventHandler(this.textBoxBZ_TextChanged);
            this.textBoxSH.KeyPress += new KeyPressEventHandler(this.textBoxSH_KeyPress);
            this.textBoxSH.TextChanged += new EventHandler(this.textBoxSH_TextChanged);
            base.textBoxSJBM.RootNodeString = "客户编码";
            base.textBoxSJBM.Text = "编码";
            this.textBoxYHZH.Multiline = true;
            this.textBoxYHZH.ScrollBars = ScrollBars.Vertical;
            this.textBoxDZDH.Multiline = true;
            this.textBoxDZDH.ScrollBars = ScrollBars.Vertical;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMKH_Edit));
            this.comboBoxID = new AisinoCMB();
            this.label15 = new AisinoLBL();
            this.label4 = new AisinoLBL();
            this.comboBoxKJM = new AisinoCMB();
            this.label3 = new AisinoLBL();
            this.textBoxSH = new AisinoTXT();
            this.textBoxDZDH = new AisinoTXT();
            this.label13 = new AisinoLBL();
            this.textBoxYHZH = new AisinoTXT();
            this.label5 = new AisinoLBL();
            this.label6 = new AisinoLBL();
            this.labelKJM = new AisinoLBL();
            this.label9 = new AisinoLBL();
            this.textBoxYJDZ = new AisinoTXT();
            this.textBoxDQBM = new AisinoTXT();
            this.textBoxYSKM = new AisinoTXT();
            this.label7 = new AisinoLBL();
            this.label11 = new AisinoLBL();
            this.textBoxDQKM = new AisinoTXT();
            this.label14 = new AisinoLBL();
            this.textBoxDQMC = new AisinoTXT();
            this.textBoxBZ = new AisinoTXT();
            base.SuspendLayout();
            base.textBoxBM.BorderStyle = BorderStyle.FixedSingle;
            base.textBoxJM.BorderStyle = BorderStyle.FixedSingle;
            base.textBoxSJBM.BorderStyle = BorderStyle.FixedSingle;
            base.textBoxSJBM.SelectBM = "";
            base.textBoxSJBM.Text = "";
            base.textBoxWaitMC.BorderStyle = BorderStyle.FixedSingle;
            base.textBoxWaitMC.Size = new Size(0xb2, 0x15);
            this.comboBoxID.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxID.FormattingEnabled = true;
            this.comboBoxID.Items.AddRange(new object[] { "是", "否" });
            this.comboBoxID.Location = new Point(0x162, 0xfc);
            this.comboBoxID.Name = "comboBoxID";
            this.comboBoxID.Size = new Size(0x62, 20);
            this.comboBoxID.TabIndex = 0x91;
            this.comboBoxID.Visible = false;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0x114, 0x100);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x41, 12);
            this.label15.TabIndex = 0xa5;
            this.label15.Text = "身份证校验";
            this.label15.Visible = false;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x1a, 0xb1);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 0x9e;
            this.label4.Text = "银行账号";
            this.comboBoxKJM.DropDownStyle = ComboBoxStyle.Simple;
            this.comboBoxKJM.FormattingEnabled = true;
            this.comboBoxKJM.Location = new Point(80, 0x11a);
            this.comboBoxKJM.Name = "comboBoxKJM";
            this.comboBoxKJM.Size = new Size(0xaf, 20);
            this.comboBoxKJM.TabIndex = 0x92;
            this.comboBoxKJM.Visible = false;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1a, 0x86);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 0x9d;
            this.label3.Text = "地址电话";
            this.textBoxSH.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxSH.Location = new Point(0x16d, 70);
            this.textBoxSH.Name = "textBoxSH";
            this.textBoxSH.Size = new Size(0xaf, 0x15);
            this.textBoxSH.TabIndex = 0x90;
            this.textBoxDZDH.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxDZDH.Location = new Point(0x5b, 130);
            this.textBoxDZDH.Multiline = true;
            this.textBoxDZDH.Name = "textBoxDZDH";
            this.textBoxDZDH.Size = new Size(0x1c1, 0x25);
            this.textBoxDZDH.TabIndex = 0x95;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x12b, 0x4a);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x35, 12);
            this.label13.TabIndex = 0x9b;
            this.label13.Text = "客户税号";
            this.textBoxYHZH.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxYHZH.Location = new Point(0x5b, 0xad);
            this.textBoxYHZH.Multiline = true;
            this.textBoxYHZH.Name = "textBoxYHZH";
            this.textBoxYHZH.Size = new Size(0x1c1, 0x2e);
            this.textBoxYHZH.TabIndex = 150;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(300, 0x68);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 0x9f;
            this.label5.Text = "邮件地址";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x120, 0x11e);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 160;
            this.label6.Text = "应收科目";
            this.label6.Visible = false;
            this.labelKJM.AutoSize = true;
            this.labelKJM.Location = new Point(0x1b, 0x11e);
            this.labelKJM.Name = "labelKJM";
            this.labelKJM.Size = new Size(0x29, 12);
            this.labelKJM.TabIndex = 0x9c;
            this.labelKJM.Text = "快捷码";
            this.labelKJM.Visible = false;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(50, 0xe5);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x1d, 12);
            this.label9.TabIndex = 0xa4;
            this.label9.Text = "备注";
            this.textBoxYJDZ.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxYJDZ.Location = new Point(0x16d, 100);
            this.textBoxYJDZ.Name = "textBoxYJDZ";
            this.textBoxYJDZ.Size = new Size(0xaf, 0x15);
            this.textBoxYJDZ.TabIndex = 0x94;
            this.textBoxDQBM.Location = new Point(80, 0x138);
            this.textBoxDQBM.Name = "textBoxDQBM";
            this.textBoxDQBM.Size = new Size(0xaf, 0x15);
            this.textBoxDQBM.TabIndex = 0x93;
            this.textBoxDQBM.Visible = false;
            this.textBoxYSKM.Location = new Point(0x162, 0x11a);
            this.textBoxYSKM.Name = "textBoxYSKM";
            this.textBoxYSKM.Size = new Size(0xaf, 0x15);
            this.textBoxYSKM.TabIndex = 0x97;
            this.textBoxYSKM.Visible = false;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(14, 0x13c);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 0xa1;
            this.label7.Text = "地区编码";
            this.label7.Visible = false;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x120, 0x13c);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x35, 12);
            this.label11.TabIndex = 0xa2;
            this.label11.Text = "地区名称";
            this.label11.Visible = false;
            this.textBoxDQKM.Location = new Point(0x162, 0x156);
            this.textBoxDQKM.Name = "textBoxDQKM";
            this.textBoxDQKM.Size = new Size(0xaf, 0x15);
            this.textBoxDQKM.TabIndex = 0x99;
            this.textBoxDQKM.Visible = false;
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x120, 0x15a);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x35, 12);
            this.label14.TabIndex = 0xa3;
            this.label14.Text = "地区科目";
            this.label14.Visible = false;
            this.textBoxDQMC.Location = new Point(0x162, 0x138);
            this.textBoxDQMC.Name = "textBoxDQMC";
            this.textBoxDQMC.Size = new Size(0xaf, 0x15);
            this.textBoxDQMC.TabIndex = 0x98;
            this.textBoxDQMC.Visible = false;
            this.textBoxBZ.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxBZ.Location = new Point(0x5b, 0xe1);
            this.textBoxBZ.Name = "textBoxBZ";
            this.textBoxBZ.Size = new Size(0x1c1, 0x15);
            this.textBoxBZ.TabIndex = 0x9a;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x23d, 0x110);
            base.Controls.Add(this.comboBoxID);
            base.Controls.Add(this.label15);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.comboBoxKJM);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textBoxSH);
            base.Controls.Add(this.textBoxDZDH);
            base.Controls.Add(this.label13);
            base.Controls.Add(this.textBoxYHZH);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.labelKJM);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.textBoxYJDZ);
            base.Controls.Add(this.textBoxDQBM);
            base.Controls.Add(this.textBoxYSKM);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.textBoxDQKM);
            base.Controls.Add(this.label14);
            base.Controls.Add(this.textBoxDQMC);
            base.Controls.Add(this.textBoxBZ);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            this.MaximumSize = new Size(700, 400);
            base.Name = "BMKH_Edit";
            this.Text = "BMKH_Edit";
            base.Controls.SetChildIndex(base.textBoxBM, 0);
            base.Controls.SetChildIndex(base.textBoxJM, 0);
            base.Controls.SetChildIndex(base.label10, 0);
            base.Controls.SetChildIndex(base.label8, 0);
            base.Controls.SetChildIndex(base.label1, 0);
            base.Controls.SetChildIndex(base.textBoxSJBM, 0);
            base.Controls.SetChildIndex(base.textBoxWaitMC, 0);
            base.Controls.SetChildIndex(base.lblMC, 0);
            base.Controls.SetChildIndex(this.textBoxBZ, 0);
            base.Controls.SetChildIndex(this.textBoxDQMC, 0);
            base.Controls.SetChildIndex(this.label14, 0);
            base.Controls.SetChildIndex(this.textBoxDQKM, 0);
            base.Controls.SetChildIndex(this.label11, 0);
            base.Controls.SetChildIndex(this.label7, 0);
            base.Controls.SetChildIndex(this.textBoxYSKM, 0);
            base.Controls.SetChildIndex(this.textBoxDQBM, 0);
            base.Controls.SetChildIndex(this.textBoxYJDZ, 0);
            base.Controls.SetChildIndex(this.label9, 0);
            base.Controls.SetChildIndex(this.labelKJM, 0);
            base.Controls.SetChildIndex(this.label6, 0);
            base.Controls.SetChildIndex(this.label5, 0);
            base.Controls.SetChildIndex(this.textBoxYHZH, 0);
            base.Controls.SetChildIndex(this.label13, 0);
            base.Controls.SetChildIndex(this.textBoxDZDH, 0);
            base.Controls.SetChildIndex(this.textBoxSH, 0);
            base.Controls.SetChildIndex(this.label3, 0);
            base.Controls.SetChildIndex(this.comboBoxKJM, 0);
            base.Controls.SetChildIndex(this.label4, 0);
            base.Controls.SetChildIndex(this.label15, 0);
            base.Controls.SetChildIndex(this.comboBoxID, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InModel()
        {
            this.khModel.BM = base.textBoxBM.Text.Trim();
            this.khModel.BZ = this.textBoxBZ.Text.Trim();
            this.khModel.DQMC = this.textBoxDQMC.Text.Trim();
            this.khModel.DZDH = this.textBoxDZDH.Text.Trim();
            this.khModel.MC = base.textBoxWaitMC.Text.Trim();
            this.khModel.SH = this.textBoxSH.Text.Trim();
            this.khModel.YHZH = this.textBoxYHZH.Text.Trim();
            this.khModel.DQBM = this.textBoxDQBM.Text.Trim();
            this.khModel.DQKM = this.textBoxDQKM.Text.Trim();
            this.khModel.YJDZ = this.textBoxYJDZ.Text.Trim();
            this.khModel.YSKM = this.textBoxYSKM.Text.Trim();
            this.khModel.SJBM = base.textBoxSJBM.Text.Trim();
            this.khModel.JM = base.textBoxJM.Text.Trim();
            this.khModel.SFZJY = this.comboBoxID.SelectedIndex == 0;
            this.khModel.WJ = 1;
            this.khModel.KJM = CommonFunc.GenerateKJM(base.textBoxWaitMC.Text.Trim());
        }

        protected override void LoadData(string BM)
        {
            base.LoadData(BM);
            this.khModel = (BMKHModel) base.baseModel;
            this.textBoxBZ.Text = this.khModel.BZ;
            this.textBoxDQMC.Text = this.khModel.DQMC;
            this.textBoxDZDH.Text = this.khModel.DZDH;
            this.textBoxSH.Text = this.khModel.SH;
            this.textBoxYHZH.Text = this.khModel.YHZH;
            this.textBoxDQBM.Text = this.khModel.DQBM;
            this.textBoxDQKM.Text = this.khModel.DQKM;
            this.textBoxYJDZ.Text = this.khModel.YJDZ;
            this.textBoxYSKM.Text = this.khModel.YSKM;
            this.comboBoxID.SelectedIndex = this.khModel.SFZJY ? 0 : 1;
            this.comboBoxKJM.DropDownStyle = ComboBoxStyle.Simple;
            this.comboBoxKJM.Text = this.khModel.KJM;
        }

        private bool SimpleValidated()
        {
            if (base.textBoxWaitMC.Text.Trim().Length == 0)
            {
                base.textBoxWaitMC.Focus();
                MessageBoxHelper.Show("客户名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (this.textBoxSH.Text.Trim().Length != 0)
            {
                string str = this.invoiceHandler.CheckTaxCode(this.textBoxSH.Text.Trim(), 0);
                if (str != "0000")
                {
                    this.textBoxSH.Focus();
                    MessageManager.ShowMsgBox(str, new string[] { "税号" });
                    return false;
                }
            }
            return true;
        }

        private void textBoxBZ_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxBZ.SelectionStart;
            this.textBoxBZ.Text = StringUtils.GetSubString(this.textBoxBZ.Text, 50);
            this.textBoxBZ.SelectionStart = selectionStart;
        }

        private void textBoxSH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (char.IsLower(e.KeyChar))
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
                if (!char.IsDigit(e.KeyChar) && !char.IsUpper(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void textBoxSH_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxSH.Text.Trim() != "")
            {
                this.textBoxSH.Text = this.textBoxSH.Text.Trim().ToUpper();
                string str = this.textBoxSH.Text.Trim();
                for (int i = 0; i < str.Length; i++)
                {
                    if (!char.IsDigit(str[i]) && !char.IsUpper(str[i]))
                    {
                        MessageBoxHelper.Show("客户税号必须为数字或字母的组合!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.textBoxSH.Text = "";
                        this.textBoxSH.Focus();
                        return;
                    }
                }
                int selectionStart = this.textBoxSH.SelectionStart;
                this.textBoxSH.Text = StringUtils.GetSubString(this.textBoxSH.Text, 20).Trim();
                this.textBoxSH.SelectionStart = selectionStart;
            }
        }

        protected override void textBoxWaitMC_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = base.textBoxWaitMC.SelectionStart;
            base.textBoxWaitMC.Text = StringUtils.GetSubString(base.textBoxWaitMC.Text, 100);
            base.textBoxWaitMC.SelectionStart = selectionStart;
        }

        private void textBoxYJDZ_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxYJDZ.SelectionStart;
            this.textBoxYJDZ.Text = StringUtils.GetSubString(this.textBoxYJDZ.Text, 40);
            this.textBoxYJDZ.SelectionStart = selectionStart;
        }

        protected override void toolStripBtnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                if (base.textBoxBM_Validating() && this.SimpleValidated())
                {
                    this.InModel();
                    string str = this.khlogical.AddCustomer(this.khModel);
                    if (str == "0")
                    {
                        MessageManager.ShowMsgBox("INP-235201");
                        ((BMBase<BMKH_Edit, BMKHFenLei, BMKHSelect>) base.father).RefreshGrid();
                        base.textBoxBM.Text = this.khlogical.TuiJianBM(base.textBoxSJBM.Text);
                        this.textBoxBZ.Text = "";
                        this.textBoxDQMC.Text = "";
                        this.textBoxDZDH.Text = "";
                        base.textBoxWaitMC.Text = "";
                        this.textBoxSH.Text = "";
                        this.textBoxYHZH.Text = "";
                        this.textBoxDQBM.Text = "";
                        this.textBoxDQKM.Text = "";
                        this.textBoxYJDZ.Text = "";
                        this.textBoxYSKM.Text = "";
                        this.comboBoxID.SelectedIndex = 1;
                        this.comboBoxKJM.DropDownStyle = ComboBoxStyle.Simple;
                        this.comboBoxKJM.Items.Clear();
                        this.comboBoxKJM.Text = "";
                        this.textBoxSH.Focus();
                    }
                    else if (str == "e1")
                    {
                        MessageManager.ShowMsgBox("INP-235108");
                    }
                    else
                    {
                        base.log.Info("客户增加失败" + str);
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        protected override void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (base.textBoxBM_Validating() && this.SimpleValidated())
                {
                    if (!base.isUpdate)
                    {
                        this.InModel();
                        string str = this.khlogical.AddCustomer(this.khModel);
                        if (str == "0")
                        {
                            MessageManager.ShowMsgBox("INP-235201");
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        else if (str == "e1")
                        {
                            MessageManager.ShowMsgBox("INP-235108");
                        }
                        else
                        {
                            base.log.Info("客户增加失败" + str);
                        }
                    }
                    else
                    {
                        this.InModel();
                        string str2 = this.khlogical.ModifyCustomer(this.khModel, base.yuanBM);
                        if (str2 == "0")
                        {
                            MessageManager.ShowMsgBox("INP-235303");
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        else if (str2 == "e1")
                        {
                            MessageManager.ShowMsgBox("INP-235108");
                        }
                        else
                        {
                            base.log.Info("客户增加失败" + str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }
    }
}

