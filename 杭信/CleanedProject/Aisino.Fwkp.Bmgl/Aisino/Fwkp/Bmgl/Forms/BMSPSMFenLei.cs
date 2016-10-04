namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BMSPSMFenLei : BaseForm
    {
        private string _sjbm;
        private BackgroundWorker backgroundWorker1;
        private string bm;
        private AisinoBTN btnCancel;
        private AisinoBTN btnContinue;
        private AisinoBTN btnSave;
        private AisinoCMB comboBoxKJM;
        private IContainer components;
        private BMSPSM father;
        private bool isUpdate;
        private BMSPSMManager khlogical;
        private BMSPSMModel khModel;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label8;
        private AisinoLBL labelKJM;
        private AisinoPNL panelDoing;
        private AisinoPIC pictureBox1;
        private string resultModifyBM;
        private string SuggestBM;
        private AisinoTXT textBoxBM;
        private TextBoxTreeView textBoxSJBM;
        private TextBoxWait textBoxWaitMC;
        private ToolStripButton toolStripBtnCancel;
        private ToolStripButton toolStripBtnContinue;
        private ToolStripButton toolStripBtnSave;
        private XmlComponentLoader xmlComponentLoader1;
        private string yuanBM;
        private string yuanSZ;

        public BMSPSMFenLei(string SJBM, BMSPSM Father)
        {
            this.khlogical = new BMSPSMManager();
            this.khModel = new BMSPSMModel();
            this.bm = "";
            this.yuanBM = "";
            this.yuanSZ = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
            this._sjbm = SJBM;
            this.father = Father;
            this.Text = "客户分类编码添加";
        }

        public BMSPSMFenLei(string SZ, string BM, bool Isupdate)
        {
            this.khlogical = new BMSPSMManager();
            this.khModel = new BMSPSMModel();
            this.bm = "";
            this.yuanBM = "";
            this.yuanSZ = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
            this.bm = BM;
            this.yuanBM = BM;
            this.yuanSZ = SZ;
            this.isUpdate = Isupdate;
            this.Text = "客户分类编码编辑";
        }

        private void Add()
        {
            this.InModel();
            if (this.khlogical.AddGoodsTax(this.khModel) == "0")
            {
                MessageManager.ShowMsgBox("INP-235401");
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
            else
            {
                MessageManager.ShowMsgBox("INP-235402");
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.resultModifyBM = this.khlogical.UpdateSubNodesSJBM(this.khModel, this.bm);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnSave.Enabled = true;
            this.panelDoing.Visible = false;
            if (e.Error != null)
            {
                MessageBoxHelper.Show(string.Format("An error occurred: {0}", e.Error.Message));
            }
            if (this.resultModifyBM == "0")
            {
                MessageManager.ShowMsgBox("INP-235403");
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
            else
            {
                MessageManager.ShowMsgBox("INP-235404");
            }
        }

        private void BMSPSM_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                this.comboBoxKJM.DropDownStyle = ComboBoxStyle.Simple;
                if (this.isUpdate)
                {
                    this.LoadData(this.bm);
                    this.toolStripBtnContinue.Visible = false;
                    this.btnContinue.Visible = false;
                }
                else
                {
                    this.textBoxSJBM.SelectBM = this._sjbm;
                    this.SuggestBM = this.khlogical.TuiJianBM(this._sjbm);
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
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.toolStripBtnSave = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnSave");
            this.toolStripBtnCancel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnCancel");
            this.toolStripBtnContinue = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnContinue");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.panelDoing = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panelDoing");
            this.pictureBox1 = this.xmlComponentLoader1.GetControlByName<AisinoPIC>("pictureBox1");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.label8 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label8");
            this.textBoxSJBM = this.xmlComponentLoader1.GetControlByName<TextBoxTreeView>("textBoxSJBM");
            this.textBoxBM = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxBM");
            this.comboBoxKJM = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxKJM");
            this.textBoxWaitMC = this.xmlComponentLoader1.GetControlByName<TextBoxWait>("textBoxWaitMC");
            this.labelKJM = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelKJM");
            this.btnSave = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnSave");
            this.btnContinue = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnContinue");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.textBoxBM.Validating += new CancelEventHandler(this.textBoxBM_Validating);
            this.textBoxWaitMC.TextChangedWaitGetText += new GetTextEventHandler(this.textBoxWaitMC_TextChangedWaitGetText);
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.btnContinue.Click += new EventHandler(this.toolStripBtnContinue_Click);
            this.btnCancel.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.btnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.toolStripBtnCancel.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.toolStripBtnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.toolStripBtnContinue.Click += new EventHandler(this.toolStripBtnContinue_Click);
            this.textBoxSJBM.RootNodeString = "商品税目编码";
            this.textBoxSJBM.Text = "编码";
            this.textBoxSJBM.selectChanged += new TextBoxTreeView.SelectChanged(this.textBoxSJBM_selectChanged);
            this.textBoxSJBM.getListNodes += new TextBoxTreeView.GetListNodes(this.treeviewneed_getListNodes);
            this.toolStripBtnContinue.Visible = false;
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMSPSMFenLei));
            this.backgroundWorker1 = new BackgroundWorker();
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1e4, 0xfd);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "BMKHFenLei";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.BMSPSMFenLei\Aisino.Fwkp.Bmgl.Forms.BMSPSMFenLei.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1e4, 0xfd);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            this.MaximumSize = new Size(500, 350);
            base.MinimizeBox = false;
            base.Name = "BMSPSMFenLei";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "税目分类";
            base.Load += new EventHandler(this.BMSPSM_Edit_Load);
            base.ResumeLayout(false);
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void InModel()
        {
            this.khModel.BM = this.textBoxBM.Text;
            this.khModel.MC = this.textBoxWaitMC.Text;
        }

        private void LoadData(string BM)
        {
            this.khModel = this.khlogical.GetModel(BM, "");
            this.textBoxBM.Text = this.khModel.BM;
            this.textBoxWaitMC.Text = this.khModel.MC;
        }

        private bool SimpleValidated()
        {
            bool flag = true;
            if (this.textBoxWaitMC.Text.Trim().Length == 0)
            {
                this.textBoxWaitMC.Focus();
                flag = false;
            }
            if (this.comboBoxKJM.Text.Trim().Length == 0)
            {
                this.comboBoxKJM.Focus();
                flag = false;
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox("INP-235309");
            }
            return flag;
        }

        private bool textBoxBM_Validating()
        {
            bool flag = true;
            if (!this.isUpdate)
            {
                if (!this.textBoxBM.Text.StartsWith(this._sjbm))
                {
                    this.textBoxBM.Text = this.SuggestBM;
                    this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                    MessageManager.ShowMsgBox("INP-235306");
                    return false;
                }
                if (this.textBoxBM.Text.Length != this.SuggestBM.Length)
                {
                    this.textBoxBM.Text = this.SuggestBM;
                    this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                    MessageManager.ShowMsgBox("INP-235305");
                    flag = false;
                }
                return flag;
            }
            if (!this.textBoxBM.Text.StartsWith(this._sjbm))
            {
                this.textBoxBM.Text = this.bm;
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                MessageManager.ShowMsgBox("INP-235306");
                return false;
            }
            if (this.textBoxBM.Text.Length != this.bm.Length)
            {
                this.textBoxBM.Text = this.bm;
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                MessageManager.ShowMsgBox("INP-235305");
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
            this._sjbm = e.Node.Name;
            this.bm = this.khlogical.TuiJianBM(e.Node.Name);
            this.textBoxBM.Text = this.bm;
            this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
        }

        private void textBoxWaitMC_TextChangedWaitGetText(object sender, GetTextEventArgs e)
        {
            string[] spellCode = StringUtils.GetSpellCode(this.textBoxWaitMC.Text.Trim());
            if (spellCode.Length > 1)
            {
                this.comboBoxKJM.DropDownStyle = ComboBoxStyle.DropDown;
                this.comboBoxKJM.Items.Clear();
                this.comboBoxKJM.Items.AddRange(spellCode);
                this.comboBoxKJM.SelectedIndex = 0;
            }
            else
            {
                this.comboBoxKJM.DropDownStyle = ComboBoxStyle.Simple;
                this.comboBoxKJM.Text = spellCode[0];
            }
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
                    if (this.khlogical.AddGoodsTax(this.khModel) == "0")
                    {
                        this.father.RefreshGridTree();
                        MessageManager.ShowMsgBox("INP-235401");
                        this.textBoxBM.Text = this.khlogical.TuiJianBM(this.textBoxSJBM.Text);
                        this.textBoxWaitMC.Text = "";
                        this.comboBoxKJM.DropDownStyle = ComboBoxStyle.Simple;
                        this.comboBoxKJM.Text = "";
                        this.comboBoxKJM.Items.Clear();
                        this.textBoxWaitMC.Focus();
                    }
                    else
                    {
                        MessageManager.ShowMsgBox("INP-235402");
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
                        this.Add();
                    }
                    else
                    {
                        this.UpdateKH();
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

        private void treeviewneed_onTreeNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.textBoxSJBM.Text = e.Node.Name;
            this._sjbm = this.textBoxSJBM.Text;
            this.bm = this.khlogical.TuiJianBM(this.textBoxSJBM.Text);
            this.textBoxBM.Text = this.bm;
            this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
        }

        private void UpdateKH()
        {
            this.InModel();
            if (this.yuanBM == this.khModel.BM)
            {
                switch (this.khlogical.ModifyGoodsTax(this.khModel, this.yuanSZ, this.yuanBM))
                {
                    case "0":
                        MessageManager.ShowMsgBox("INP-235403");
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                        return;

                    case "e1":
                        MessageManager.ShowMsgBox("INP-235108");
                        return;
                }
                MessageManager.ShowMsgBox("INP-235404");
            }
            else
            {
                this.resultModifyBM = this.khlogical.UpdateSubNodesSJBM(this.khModel, this.yuanBM);
                if (this.resultModifyBM == "0")
                {
                    MessageManager.ShowMsgBox("INP-235403");
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
                else if (this.resultModifyBM == "e1")
                {
                    MessageManager.ShowMsgBox("INP-235108");
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-235404");
                }
            }
        }
    }
}

