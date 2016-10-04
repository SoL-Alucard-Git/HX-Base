namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using Aisino.Fwkp.BusinessObject;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class BMGHDW_Edit : BaseForm
    {
        private string _bm;
        private string _sjbm;
        private IContainer components;
        private BMGHDW father;
        private BMGHDWManager ghdwlogical;
        private BMGHDWModel ghdwModel;
        private InvoiceHandler invoiceHandler;
        private bool isUpdate;
        private AisinoLBL label1;
        private AisinoLBL label10;
        private AisinoLBL label11;
        private AisinoLBL label13;
        private AisinoLBL label14;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL label6;
        private AisinoLBL label7;
        private AisinoLBL label8;
        private AisinoLBL label9;
        private ILog log;
        private string SuggestBM;
        private AisinoTXT textBoxBM;
        private AisinoTXT textBoxDZDH;
        private AisinoTXT textBoxIDCOC;
        private AisinoTXT textBoxJM;
        private AisinoTXT textBoxSH;
        private TextBoxTreeView textBoxSJBM;
        private TextBoxWait textBoxWaitMC;
        private AisinoTXT textBoxYHZH;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnCancel;
        private ToolStripButton toolStripBtnContinue;
        private ToolStripButton toolStripBtnSave;
        private XmlComponentLoader xmlComponentLoader1;
        private string yuanBM;

        public BMGHDW_Edit(string SJBM, BMGHDW Father)
        {
            this.ghdwlogical = new BMGHDWManager();
            this.ghdwModel = new BMGHDWModel();
            this.log = LogUtil.GetLogger<BMGHDW_Edit>();
            this.invoiceHandler = new InvoiceHandler();
            this._bm = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
            this._sjbm = SJBM;
            this.father = Father;
            this.Text = "购货单位编码添加";
        }

        public BMGHDW_Edit(string BM, bool Isupdate)
        {
            this.ghdwlogical = new BMGHDWManager();
            this.ghdwModel = new BMGHDWModel();
            this.log = LogUtil.GetLogger<BMGHDW_Edit>();
            this.invoiceHandler = new InvoiceHandler();
            this._bm = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
            this._bm = BM;
            this.yuanBM = BM;
            this.isUpdate = Isupdate;
            this.Text = "购货单位编码编辑";
        }

        private void BMGHDW_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.isUpdate)
                {
                    this.LoadData(this._bm);
                    this._sjbm = this.ghdwModel.SJBM;
                    this.textBoxSJBM.SelectBM = this.ghdwModel.SJBM;
                    this.toolStripBtnContinue.Visible = false;
                }
                else
                {
                    this.textBoxSJBM.SelectBM = this._sjbm;
                    this.SuggestBM = this.ghdwlogical.TuiJianBM(this._sjbm);
                    if (this.SuggestBM == "NoNode")
                    {
                        MessageManager.ShowMsgBox("INP-235121");
                        base.Close();
                        return;
                    }
                    if (this.SuggestBM.Length > 0x10)
                    {
                        MessageManager.ShowMsgBox("INP-235111");
                        base.Close();
                        return;
                    }
                    this.textBoxBM.Text = this.SuggestBM;
                }
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                this.textBoxSJBM.TreeLoad();
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
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
            this.textBoxWaitMC = this.xmlComponentLoader1.GetControlByName<TextBoxWait>("textBoxWaitMC");
            this.textBoxSH = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxSH");
            this.label10 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label10");
            this.textBoxJM = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxJM");
            this.textBoxBM = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxBM");
            this.textBoxSJBM = this.xmlComponentLoader1.GetControlByName<TextBoxTreeView>("textBoxSJBM");
            this.label8 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label8");
            this.label7 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label7");
            this.textBoxIDCOC = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxIDCOC");
            this.label14 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label14");
            this.label11 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label11");
            this.label9 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label9");
            this.label6 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label6");
            this.label5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label5");
            this.textBoxYHZH = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxYHZH");
            this.textBoxDZDH = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxDZDH");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripBtnSave = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnSave");
            this.toolStripBtnContinue = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnContinue");
            this.label13 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label13");
            this.toolStripBtnCancel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnCancel");
            this.textBoxWaitMC.WaterMarkString = "购货单位名称";
            this.textBoxWaitMC.TextChangedWaitGetText += new GetTextEventHandler(this.textBoxWaitMC_TextChangedWaitGetText);
            this.textBoxWaitMC.TextChanged += new EventHandler(this.textBoxWaitMC_TextChanged);
            this.textBoxJM.KeyPress += new KeyPressEventHandler(this.textBoxJM_KeyPress);
            this.textBoxJM.TextChanged += new EventHandler(this.textBoxJM_TextChanged);
            this.textBoxSH.KeyPress += new KeyPressEventHandler(this.textBoxSH_KeyPress);
            this.textBoxSH.TextChanged += new EventHandler(this.textBoxSH_TextChanged);
            this.textBoxBM.KeyPress += new KeyPressEventHandler(this.textBoxBM_KeyPress);
            this.textBoxBM.TextChanged += new EventHandler(this.textBoxBM_TextChanged);
            this.textBoxBM.Validating += new CancelEventHandler(this.textBoxBM_Validating);
            this.textBoxIDCOC.KeyPress += new KeyPressEventHandler(this.textBoxIDCOC_KeyPress);
            this.textBoxIDCOC.TextChanged += new EventHandler(this.textBoxIDCOC_TextChanged);
            this.toolStripBtnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.toolStripBtnContinue.Click += new EventHandler(this.toolStripBtnContinue_Click);
            this.toolStripBtnCancel.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.textBoxSJBM.RootNodeString = "购货单位编码";
            this.textBoxSJBM.Text = "编码";
            this.textBoxSJBM.selectChanged += new TextBoxTreeView.SelectChanged(this.textBoxSJBM_selectChanged);
            this.textBoxSJBM.getListNodes += new TextBoxTreeView.GetListNodes(this.treeviewneed_getListNodes);
            this.textBoxYHZH.Multiline = true;
            this.textBoxYHZH.ScrollBars = ScrollBars.Vertical;
            this.textBoxYHZH.TextChanged += new EventHandler(StringUtils.textBoxYHZH_DZDH_TextChanged);
            this.textBoxDZDH.Multiline = true;
            this.textBoxDZDH.ScrollBars = ScrollBars.Vertical;
            this.textBoxDZDH.TextChanged += new EventHandler(StringUtils.textBoxYHZH_DZDH_TextChanged);
            this.toolStripBtnContinue.Visible = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMGHDW_Edit));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(600, 0xf7);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "BMGHDW_Edit";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.BMGHDW_Edit\Aisino.Fwkp.Bmgl.Forms.BMGHDW_Edit.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(600, 0xf7);
            base.Controls.Add(this.xmlComponentLoader1);
            base.MaximizeBox = false;
            this.MaximumSize = new Size(700, 400);
            base.MinimizeBox = false;
            base.Name = "BMGHDW_Edit";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "BMGHDW_Edit";
            base.Load += new EventHandler(this.BMGHDW_Edit_Load);
            base.ResumeLayout(false);
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void InModel()
        {
            this.ghdwModel.BM = this.textBoxBM.Text.Trim();
            this.ghdwModel.DZDH = this.textBoxDZDH.Text.Trim();
            this.ghdwModel.MC = this.textBoxWaitMC.Text.Trim();
            this.ghdwModel.SH = this.textBoxSH.Text.Trim();
            this.ghdwModel.YHZH = this.textBoxYHZH.Text.Trim();
            this.ghdwModel.IDCOC = this.textBoxIDCOC.Text.Trim();
            this.ghdwModel.SJBM = this.textBoxSJBM.Text.Trim();
            this.ghdwModel.JM = this.textBoxJM.Text.Trim();
            this.ghdwModel.KJM = CommonFunc.GenerateKJM(this.textBoxWaitMC.Text.Trim());
            this.ghdwModel.WJ = 1;
        }

        private void LoadData(string BM)
        {
            this.ghdwModel = (BMGHDWModel) this.ghdwlogical.GetModel(BM);
            this.textBoxBM.Text = this.ghdwModel.BM;
            this.textBoxWaitMC.Text = this.ghdwModel.MC;
            this.textBoxJM.Text = this.ghdwModel.JM;
            this.textBoxSH.Text = this.ghdwModel.SH;
            this.textBoxYHZH.Text = this.ghdwModel.YHZH;
            this.textBoxDZDH.Text = this.ghdwModel.DZDH;
            this.textBoxIDCOC.Text = this.ghdwModel.IDCOC;
            this.textBoxSJBM.Text = this.ghdwModel.SJBM;
        }

        private bool SimpleValidated()
        {
            bool flag = true;
            if (this.textBoxJM.Text.Trim().Trim().Length != 0)
            {
                string input = this.textBoxJM.Text.Trim();
                if ((input[0] < 'A') || (input[0] > 'Z'))
                {
                    this.textBoxJM.Focus();
                    flag = false;
                    MessageManager.ShowMsgBox("INP-235501");
                    return flag;
                }
                string pattern = "^[A-Z0-9]+$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(input))
                {
                    this.textBoxJM.Focus();
                    flag = false;
                    MessageManager.ShowMsgBox("INP-235502");
                    return flag;
                }
            }
            if (this.textBoxSH.Text.Trim().Length != 0)
            {
                string str3 = this.invoiceHandler.CheckTaxCode(this.textBoxSH.Text.Trim(), (FPLX)12);
                if (str3 != "0000")
                {
                    this.textBoxSH.Focus();
                    MessageManager.ShowMsgBox(str3, new string[] { "税号" });
                    return false;
                }
            }
            string str4 = this.invoiceHandler.CheckZzjgdm_New(this.textBoxIDCOC.Text.Trim());
            if (str4 != "0000")
            {
                this.textBoxIDCOC.Focus();
                MessageManager.ShowMsgBox(str4);
                return false;
            }
            if (this.textBoxWaitMC.Text.Trim().Length == 0)
            {
                this.textBoxWaitMC.Focus();
                flag = false;
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox("INP-235309");
            }
            return flag;
        }

        private void textBoxBM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (char.IsUpper(e.KeyChar))
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                }
                if ((!char.IsDigit(e.KeyChar) && !char.IsLower(e.KeyChar)) || (this.textBoxBM.Text.Trim().Length >= 0x10))
                {
                    e.Handled = true;
                }
            }
        }

        private void textBoxBM_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxBM.Text.Trim() != "")
            {
                int selectionStart = this.textBoxBM.SelectionStart;
                this.textBoxBM.Text = StringUtils.GetSubString(this.textBoxBM.Text, 0x10).Trim();
                this.textBoxBM.SelectionStart = selectionStart;
            }
        }

        private bool textBoxBM_Validating()
        {
            bool flag = true;
            string input = this.textBoxBM.Text.Trim();
            string pattern = "^[a-z0-9]+$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(input))
            {
                MessageManager.ShowMsgBox("INP-235506");
                return false;
            }
            if (!this.isUpdate)
            {
                if (!this.textBoxBM.Text.StartsWith(this._sjbm))
                {
                    this.textBoxBM.Text = this.SuggestBM;
                    this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                    MessageManager.ShowMsgBox("INP-235306");
                    return false;
                }
                if (!("NoXJBM" != this.ghdwlogical.ChildDetermine(this.textBoxBM.Text.Trim(), this._sjbm)) || (this.textBoxBM.Text.Length == this.ghdwlogical.GetSuggestBMLen(this._sjbm)))
                {
                    return flag;
                }
                this.textBoxBM.Text = this.SuggestBM;
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                MessageManager.ShowMsgBox("INP-235305");
                return false;
            }
            if (!this.textBoxBM.Text.StartsWith(this._sjbm))
            {
                if (this.textBoxSJBM.Text != this.ghdwModel.SJBM)
                {
                    this.textBoxBM.Text = this.SuggestBM;
                }
                else
                {
                    this.textBoxBM.Text = this._bm;
                }
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                MessageManager.ShowMsgBox("INP-235306");
                return false;
            }
            if (!("OnlyBMAndIsSelf" != this.ghdwlogical.ChildDetermine(this.yuanBM, this._sjbm)) || (this.textBoxBM.Text.Length == this.ghdwlogical.GetSuggestBMLen(this._sjbm)))
            {
                return flag;
            }
            if (this.textBoxSJBM.Text != this.ghdwModel.SJBM)
            {
                this.textBoxBM.Text = this.SuggestBM;
            }
            else
            {
                this.textBoxBM.Text = this._bm;
            }
            this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
            MessageManager.ShowMsgBox("INP-235305");
            return false;
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

        private void textBoxIDCOC_KeyPress(object sender, KeyPressEventArgs e)
        {
            char.IsControl(e.KeyChar);
        }

        private void textBoxIDCOC_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxIDCOC.Text.Trim() != "")
            {
                int selectionStart = this.textBoxIDCOC.SelectionStart;
                this.textBoxIDCOC.Text = StringUtils.GetSubString(this.textBoxIDCOC.Text, 0x16).Trim();
                this.textBoxIDCOC.SelectionStart = selectionStart;
            }
        }

        private void textBoxJM_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxJM_TextChanged(object sender, EventArgs e)
        {
            this.textBoxJM.Text = this.textBoxJM.Text.Trim().ToUpper();
            if (this.textBoxJM.Text.Trim() != "")
            {
                string str = this.textBoxJM.Text.Trim();
                if (!char.IsLetter(str[0]))
                {
                    MessageBoxHelper.Show("简码必须以大写字母开头!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.textBoxJM.Text = "";
                    this.textBoxJM.Focus();
                }
                else
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (!char.IsDigit(str[i]) && !char.IsUpper(str[i]))
                        {
                            MessageBoxHelper.Show("简码只能用大写字母和数字!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.textBoxJM.Text = "";
                            this.textBoxJM.Focus();
                            return;
                        }
                    }
                    int selectionStart = this.textBoxJM.SelectionStart;
                    this.textBoxJM.Text = StringUtils.GetSubString(this.textBoxJM.Text, 6).Trim();
                    this.textBoxJM.SelectionStart = selectionStart;
                }
            }
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
                        MessageBoxHelper.Show("税号必须为数字或字母的组合!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void textBoxSJBM_selectChanged(object sender, TreeNodeMouseClickEventArgs e)
        {
            this._sjbm = e.Node.Name;
            if (this.isUpdate && (this._sjbm == this.ghdwModel.SJBM))
            {
                this.textBoxBM.Text = this._bm;
            }
            else
            {
                this.textBoxBM.Text = this.ghdwlogical.TuiJianBM(e.Node.Name);
            }
            this.SuggestBM = this.ghdwlogical.TuiJianBM(this._sjbm);
            this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
        }

        private void textBoxWaitMC_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxWaitMC.SelectionStart;
            this.textBoxWaitMC.Text = StringUtils.GetSubString(this.textBoxWaitMC.Text, 0x6c);
            this.textBoxWaitMC.SelectionStart = selectionStart;
        }

        private void textBoxWaitMC_TextChangedWaitGetText(object sender, GetTextEventArgs e)
        {
            StringUtils.GetSpellCode(this.textBoxWaitMC.Text.Trim());
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
                    string str = this.ghdwlogical.AddPurchase(this.ghdwModel);
                    if (str == "0")
                    {
                        this.father.RefreshGrid();
                        this.textBoxBM.Text = this.ghdwlogical.TuiJianBM(this.textBoxSJBM.Text);
                        this.textBoxWaitMC.Text = "";
                        this.textBoxSH.Text = "";
                        this.textBoxYHZH.Text = "";
                        this.textBoxDZDH.Text = "";
                        this.textBoxIDCOC.Text = "";
                        this.textBoxSH.Focus();
                    }
                    else
                    {
                        this.log.Info("购货单位增加失败" + str);
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
                        string str = this.ghdwlogical.AddPurchase(this.ghdwModel);
                        if (str == "0")
                        {
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        else if (str == "e1")
                        {
                            MessageManager.ShowMsgBox("INP-235108");
                        }
                        else
                        {
                            this.log.Info("购货单位增加失败" + str);
                        }
                    }
                    else
                    {
                        this.InModel();
                        string str2 = this.ghdwlogical.ModifyPurchase(this.ghdwModel, this.yuanBM);
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
                            this.log.Info("购货单位增加失败" + str2);
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
            return this.ghdwlogical.listNodes(ParentBM);
        }
    }
}

