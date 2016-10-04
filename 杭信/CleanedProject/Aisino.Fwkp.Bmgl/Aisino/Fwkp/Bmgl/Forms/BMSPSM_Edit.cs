namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class BMSPSM_Edit : BaseForm
    {
        private string _bm;
        private string _sjbm;
        private AisinoLBL aisinoLBL_BM;
        private AisinoLBL aisinoLBL_FHDBZ;
        private AisinoLBL aisinoLBL_MC;
        private AisinoLBL aisinoLBL_SLV;
        private AisinoLBL aisinoLBL_SZ;
        private AisinoLBL aisinoLBL_ZSL;
        private AisinoCMB comboBox_FHDBZ;
        private AisinoCMB comboBox_SLV;
        private AisinoCMB comboBox_ZSL;
        private IContainer components;
        private BMSPSM father;
        private bool isUpdate;
        private BMSPSMManager khlogical;
        private ILog log;
        private BMSPSMModel spsmModel;
        private string SuggestBM;
        private AisinoTXT textBox_BM;
        private AisinoTXT textBox_MC;
        private AisinoTXT textBox_SZ;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnCancel;
        private ToolStripButton toolStripBtnContinue;
        private ToolStripButton toolStripBtnSave;
        private XmlComponentLoader xmlComponentLoader1;
        private string yuanBM;
        private string yuanSZ;

        public BMSPSM_Edit(string SJBM, BMSPSM Father)
        {
            this.khlogical = new BMSPSMManager();
            this.spsmModel = new BMSPSMModel();
            this.log = LogUtil.GetLogger<BMSPSM_Edit>();
            this._bm = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
            this._sjbm = SJBM;
            this.father = Father;
            this.Text = "税目编码添加";
        }

        public BMSPSM_Edit(string SZ, string BM, bool Isupdate)
        {
            this.khlogical = new BMSPSMManager();
            this.spsmModel = new BMSPSMModel();
            this.log = LogUtil.GetLogger<BMSPSM_Edit>();
            this._bm = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
            this._bm = BM;
            this.yuanBM = BM;
            this.yuanSZ = SZ;
            this.isUpdate = Isupdate;
            this.Text = "税目编码编辑";
        }

        private void BMSPSM_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> list = new List<string>();
                List<double> taxRateNoTax = TaxCardFactory.CreateTaxCard().TaxRateAuthorize.TaxRateNoTax;
                if (taxRateNoTax != null)
                {
                    foreach (double num in taxRateNoTax)
                    {
                        if (num.ToString("f2") == "0.00")
                        {
                            list.Add("免税");
                        }
                        else
                        {
                            list.Add(num.ToString("f2"));
                        }
                    }
                }
                else
                {
                    string[] collection = new string[] { "0.04", "0.05", "0.06", "0.13", "0.17" };
                    list.AddRange(collection);
                }
                foreach (string str in list)
                {
                    this.comboBox_SLV.Items.Add(this.ChangeTaxRate(str));
                }
                if (this.comboBox_SLV.Items.Count > 0)
                {
                    this.comboBox_SLV.SelectedIndex = 0;
                }
                else
                {
                    this.comboBox_SLV.Text = "";
                }
                if (this.isUpdate)
                {
                    this.LoadData(this.yuanSZ, this.yuanBM);
                    this.toolStripBtnContinue.Visible = false;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private string ChangeTaxRate(string rate)
        {
            string str = string.Empty;
            try
            {
                double result = 0.0;
                if (double.TryParse(rate, out result))
                {
                    return result.ToString("#%");
                }
                return rate;
            }
            catch (Exception)
            {
                str = string.Empty;
            }
            return str;
        }

        private void comboBox_SLV_Leave(object sender, EventArgs e)
        {
            if ((this.comboBox_SLV.Text != "System.Data.DataRowView") && (this.comboBox_SLV.Text != ""))
            {
                this.comboBox_SLV_Validat();
            }
        }

        private bool comboBox_SLV_Validat()
        {
            decimal num;
            bool flag = true;
            if (this.comboBox_SLV.Text.Trim() == "免税")
            {
                return true;
            }
            if (!this.comboBox_SLV.Text.Contains("%"))
            {
                string pattern = "^[0-9]*.?[0-9]+$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(this.comboBox_SLV.Text.Trim()))
                {
                    if (this.comboBox_SLV.Items.Count > 0)
                    {
                        this.comboBox_SLV.SelectedIndex = 0;
                    }
                    else
                    {
                        this.comboBox_SLV.Text = "";
                    }
                    MessageBoxHelper.Show("税率应为小数或百分数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.comboBox_SLV.Focus();
                    return false;
                }
                double num2 = double.Parse(this.comboBox_SLV.Text.Trim());
                if ((num2 < 0.0) || (num2 > 100.0))
                {
                    if (this.comboBox_SLV.Items.Count > 0)
                    {
                        this.comboBox_SLV.SelectedIndex = 0;
                    }
                    else
                    {
                        this.comboBox_SLV.Text = "";
                    }
                    MessageBoxHelper.Show("税率不合法！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.comboBox_SLV.Focus();
                    return false;
                }
                if ("0.00" == num2.ToString("f2"))
                {
                    this.comboBox_SLV.Text = "免税";
                    return true;
                }
                if ((num2 > 0.0) && (num2 <= 1.0))
                {
                    if ((num2 > 0.0) && (num2 < 0.01))
                    {
                        this.comboBox_SLV.Text = "0%";
                        return true;
                    }
                    num2 *= 100.0;
                    this.comboBox_SLV.Text = num2.ToString("F0") + "%";
                    return true;
                }
                this.comboBox_SLV.Text = num2.ToString("F0") + "%";
                return flag;
            }
            if (!decimal.TryParse(this.comboBox_SLV.Text.Trim(new char[] { '%' }), out num))
            {
                if (this.comboBox_SLV.Items.Count > 0)
                {
                    this.comboBox_SLV.SelectedIndex = 0;
                }
                else
                {
                    this.comboBox_SLV.Text = "";
                }
                MessageBoxHelper.Show("税率应为小数或百分数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.comboBox_SLV.Focus();
                return false;
            }
            if ((num < 0M) || (num > 100M))
            {
                if (this.comboBox_SLV.Items.Count > 0)
                {
                    this.comboBox_SLV.SelectedIndex = 0;
                }
                else
                {
                    this.comboBox_SLV.Text = "";
                }
                MessageBoxHelper.Show("税率不合法！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.comboBox_SLV.Focus();
                return false;
            }
            if ((num > 0M) && (num < 1M))
            {
                this.comboBox_SLV.Text = "免税";
                return flag;
            }
            this.comboBox_SLV.Text = num.ToString("F0") + "%";
            return flag;
        }

        private void comboBox_ZSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) && ((e.KeyChar != '.') || this.comboBox_ZSL.Text.Contains("."))))
            {
                e.Handled = true;
            }
        }

        private void comboBox_ZSL_TextChanged(object sender, EventArgs e)
        {
            if (this.comboBox_ZSL.Text.Trim() != "")
            {
                decimal num;
                if (!decimal.TryParse(this.comboBox_ZSL.Text.Trim(), out num) || (num < 0M))
                {
                    MessageBoxHelper.Show("征收率必须为正数!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.comboBox_ZSL.Text = "";
                    this.comboBox_ZSL.Focus();
                }
                else
                {
                    int selectionStart = this.comboBox_ZSL.SelectionStart;
                    this.comboBox_ZSL.Text = StringUtils.GetSubString(this.comboBox_ZSL.Text, 15).Trim();
                    this.comboBox_ZSL.SelectionStart = selectionStart;
                }
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
            this.aisinoLBL_SZ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL_SZ");
            this.textBox_SZ = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox_SZ");
            this.textBox_SZ.KeyPress += new KeyPressEventHandler(this.textBox_SZ_KeyPress);
            this.textBox_SZ.TextChanged += new EventHandler(this.textBox_SZ_TextChanged);
            this.aisinoLBL_BM = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL_BM");
            this.textBox_BM = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox_BM");
            this.textBox_BM.KeyPress += new KeyPressEventHandler(this.textBox_BM_KeyPress);
            this.textBox_BM.TextChanged += new EventHandler(this.textBox_BM_TextChanged);
            this.aisinoLBL_MC = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL_MC");
            this.textBox_MC = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox_MC");
            this.aisinoLBL_SLV = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL_SLV");
            this.comboBox_SLV = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBox_SLV");
            this.comboBox_SLV.Leave += new EventHandler(this.comboBox_SLV_Leave);
            this.comboBox_SLV.SelectedValueChanged += new EventHandler(this.comboBox_SLV_Leave);
            this.aisinoLBL_ZSL = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL_ZSL");
            this.comboBox_ZSL = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBox_ZSL");
            this.comboBox_ZSL.DropDownStyle = ComboBoxStyle.Simple;
            this.comboBox_ZSL.KeyPress += new KeyPressEventHandler(this.comboBox_ZSL_KeyPress);
            this.comboBox_ZSL.TextChanged += new EventHandler(this.comboBox_ZSL_TextChanged);
            this.aisinoLBL_FHDBZ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL_FHDBZ");
            this.comboBox_FHDBZ = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBox_FHDBZ");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripBtnSave = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnSave");
            this.toolStripBtnContinue = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnContinue");
            this.toolStripBtnCancel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnCancel");
            this.toolStripBtnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.toolStripBtnContinue.Click += new EventHandler(this.toolStripBtnContinue_Click);
            this.toolStripBtnCancel.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.textBox_MC.TextChanged += new EventHandler(this.textBox_MC_TextChanged);
            this.toolStripBtnContinue.Visible = false;
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.comboBox_FHDBZ.Items.AddRange(new string[] { "否", "是" });
            this.comboBox_FHDBZ.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_FHDBZ.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMSPSM_Edit));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x289, 0x93);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "BMKH_Edit";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.BMSPSM_Edit\Aisino.Fwkp.Bmgl.Forms.BMSPSM_Edit.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x289, 0x93);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            this.MaximumSize = new Size(700, 400);
            base.MinimizeBox = false;
            base.Name = "BMSPSM_Edit";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "税目编辑";
            base.Load += new EventHandler(this.BMSPSM_Edit_Load);
            base.ResumeLayout(false);
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void InModel()
        {
            this.spsmModel.SZ = this.textBox_SZ.Text.Trim();
            this.spsmModel.BM = this.textBox_BM.Text.Trim();
            this.spsmModel.MC = this.textBox_MC.Text.Trim();
            this.spsmModel.ZSL = double.Parse(this.comboBox_ZSL.Text.Trim());
            this.spsmModel.ZSL = Math.Round(this.spsmModel.ZSL, 5);
            if (this.comboBox_SLV.Text.Trim() == "免税")
            {
                this.spsmModel.SLV = 0.0;
            }
            else
            {
                this.spsmModel.SLV = Convert.ToDouble(this.comboBox_SLV.Text.Trim(new char[] { '%' })) / 100.0;
            }
            this.spsmModel.FHDBZ = this.comboBox_FHDBZ.Text.Trim() != "否";
        }

        private void LoadData(string SZ, string BM)
        {
            this.spsmModel = this.khlogical.GetModel(BM, SZ);
            this.yuanSZ = this.spsmModel.SZ.ToString();
            this.yuanBM = this.spsmModel.BM.ToString();
            this.textBox_SZ.Text = this.spsmModel.SZ.ToString();
            this.textBox_MC.Text = this.spsmModel.MC;
            this.textBox_BM.Text = this.spsmModel.BM;
            this.comboBox_SLV.Text = this.spsmModel.SLV.ToString();
            if ("0.00" == this.spsmModel.SLV.ToString("f2"))
            {
                this.comboBox_SLV.Text = "免税";
            }
            else
            {
                this.comboBox_SLV.Text = this.spsmModel.SLV.ToString("#%");
            }
            this.comboBox_FHDBZ.SelectedIndex = !this.spsmModel.FHDBZ ? 0 : 1;
            this.comboBox_ZSL.Text = this.spsmModel.ZSL.ToString();
        }

        private bool SimpleValidated()
        {
            if (this.textBox_SZ.Text.Trim().Length != 2)
            {
                MessageBoxHelper.Show("税种必须为2位数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.textBox_SZ.Focus();
                return false;
            }
            if (this.textBox_BM.Text.Trim().Length == 0)
            {
                MessageBoxHelper.Show("税目编码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.textBox_BM.Focus();
                return false;
            }
            if (this.textBox_MC.Text.Trim().Length == 0)
            {
                MessageBoxHelper.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.textBox_MC.Focus();
                return false;
            }
            if (!this.comboBox_SLV_Validat())
            {
                this.comboBox_SLV.Focus();
                return false;
            }
            if (this.comboBox_ZSL.Text.Trim().Length == 0)
            {
                this.comboBox_ZSL.Focus();
                MessageBoxHelper.Show("征收率不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void textBox_BM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox_BM_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox_BM.Text.Trim() != "")
            {
                foreach (char ch in this.textBox_BM.Text.Trim())
                {
                    if (!char.IsDigit(ch))
                    {
                        MessageBoxHelper.Show("编码必须为数字!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.textBox_BM.Text = "";
                        this.textBox_BM.Focus();
                        return;
                    }
                }
                int selectionStart = this.textBox_BM.SelectionStart;
                this.textBox_BM.Text = StringUtils.GetSubString(this.textBox_BM.Text, 4).Trim();
                this.textBox_BM.SelectionStart = selectionStart;
            }
        }

        private void textBox_MC_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBox_MC.SelectionStart;
            this.textBox_MC.Text = StringUtils.GetSubString(this.textBox_MC.Text, 60);
            this.textBox_MC.SelectionStart = selectionStart;
        }

        private void textBox_SZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox_SZ_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox_SZ.Text.Trim() != "")
            {
                foreach (char ch in this.textBox_SZ.Text.Trim())
                {
                    if (!char.IsDigit(ch))
                    {
                        MessageBoxHelper.Show("税种必须为数字!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.textBox_SZ.Text = "";
                        this.textBox_SZ.Focus();
                        return;
                    }
                }
                int selectionStart = this.textBox_SZ.SelectionStart;
                this.textBox_SZ.Text = StringUtils.GetSubString(this.textBox_SZ.Text, 2).Trim();
                this.textBox_SZ.SelectionStart = selectionStart;
            }
        }

        private bool textBoxBM_Validating()
        {
            bool flag = true;
            if (this.textBox_BM.Text.Trim().Length != 4)
            {
                MessageBoxHelper.Show("编码长度必须为四位!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag = false;
            }
            return flag;
        }

        private void textBoxBM_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!this.textBoxBM_Validating())
                {
                    e.Cancel = true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void textBoxSJBM_selectChanged(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        private void toolStripBtnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolStripBtnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SimpleValidated())
                {
                    this.InModel();
                    string str = this.khlogical.AddGoodsTax(this.spsmModel);
                    if (str == "0")
                    {
                        this.father.RefreshGrid();
                        this.textBox_MC.Text = "";
                        this.textBox_BM.Text = "";
                        this.comboBox_FHDBZ.Text = "";
                        this.textBox_BM.Focus();
                    }
                    else
                    {
                        this.log.Info("商品税目增加失败" + str);
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBoxBM_Validating() && this.SimpleValidated())
                {
                    if (!this.isUpdate)
                    {
                        this.InModel();
                        string str = this.khlogical.AddGoodsTax(this.spsmModel);
                        if (str == "0")
                        {
                            MessageManager.ShowMsgBox("INP-235301");
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        else if (str == "e1")
                        {
                            MessageManager.ShowMsgBox("INP-235108");
                        }
                        else
                        {
                            this.log.Info("客户增加失败" + str);
                        }
                    }
                    else
                    {
                        this.InModel();
                        string str2 = this.khlogical.ModifyGoodsTax(this.spsmModel, this.yuanSZ, this.yuanBM);
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
                            this.log.Info("客户增加失败" + str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private List<TreeNodeTemp> treeviewneed_getListNodes(string ParentBM)
        {
            return this.khlogical.listNodes(ParentBM);
        }
    }
}

