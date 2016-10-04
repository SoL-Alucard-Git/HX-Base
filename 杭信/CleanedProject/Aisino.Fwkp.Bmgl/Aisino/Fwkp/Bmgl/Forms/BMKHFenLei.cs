namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.DAL;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class BMKHFenLei : BaseForm
    {
        private string _sjbm;
        private BackgroundWorker backgroundWorker1;
        private string bm;
        private AisinoBTN btnCancel;
        private AisinoBTN btnContinue;
        private AisinoBTN btnSave;
        private IContainer components;
        private BMKH father;
        private bool isUpdate;
        private BLL.BMKHManager khlogical;
        private BMKHModel khModel;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label8;
        private AisinoLBL labelJM;
        private AisinoPNL panelDoing;
        private AisinoPIC pictureBox1;
        private string resultModifyBM;
        private string SuggestBM;
        private AisinoTXT textBoxBM;
        private AisinoTXT textBoxJM;
        private TextBoxTreeView textBoxSJBM;
        private TextBoxWait textBoxWaitMC;
        private ToolStripButton toolStripBtnCancel;
        private ToolStripButton toolStripBtnContinue;
        private ToolStripButton toolStripBtnSave;
        private XmlComponentLoader xmlComponentLoader1;
        private string yuanBM;

        public BMKHFenLei(string SJBM, BMKH Father)
        {
            this.khlogical = new BLL.BMKHManager();
            this.khModel = new BMKHModel();
            this.bm = "";
            this.yuanBM = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
            this._sjbm = SJBM;
            this.father = Father;
            this.Text = "客户分类编码添加";
        }

        public BMKHFenLei(string BM, bool Isupdate)
        {
            this.khlogical = new BLL.BMKHManager();
            this.khModel = new BMKHModel();
            this.bm = "";
            this.yuanBM = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
            this.bm = BM;
            this.yuanBM = BM;
            this.isUpdate = Isupdate;
            this.Text = "客户分类编码编辑";
        }

        public BMKHFenLei(BMKH Father, string BM, bool Isupdate)
        {
            this.khlogical = new BLL.BMKHManager();
            this.khModel = new BMKHModel();
            this.bm = "";
            this.yuanBM = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
            this.bm = BM;
            this.yuanBM = BM;
            this.isUpdate = Isupdate;
            this.father = Father;
            this.Text = "客户分类编码编辑";
        }

        private void Add()
        {
            this.InModel();
            if (this.khlogical.AddCustomer(this.khModel) == "0")
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

        private void BMKH_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.isUpdate)
                {
                    this.LoadData(this.bm);
                    this._sjbm = this.khModel.SJBM;
                    this.textBoxSJBM.SelectBM = this.khModel.SJBM;
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
            this.textBoxJM = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxJM");
            this.textBoxWaitMC = this.xmlComponentLoader1.GetControlByName<TextBoxWait>("textBoxWaitMC");
            this.textBoxWaitMC.WaterMarkString = "客户分类名称";
            this.textBoxWaitMC.TextChangedWaitGetText += new GetTextEventHandler(this.textBoxWaitMC_TextChangedWaitGetText);
            this.labelJM = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelKJM");
            this.btnSave = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnSave");
            this.btnContinue = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnContinue");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.textBoxBM.KeyPress += new KeyPressEventHandler(this.textBoxBM_KeyPress);
            this.textBoxBM.TextChanged += new EventHandler(this.textBoxBM_TextChanged);
            this.textBoxBM.Validating += new CancelEventHandler(this.textBoxBM_Validating);
            this.textBoxWaitMC.TextChangedWaitGetText += new GetTextEventHandler(this.textBoxWaitMC_TextChangedWaitGetText);
            this.textBoxWaitMC.TextChanged += new EventHandler(this.textBoxWaitMC_TextChanged);
            this.textBoxJM.KeyPress += new KeyPressEventHandler(this.textBoxJM_KeyPress);
            this.textBoxJM.TextChanged += new EventHandler(this.textBoxJM_TextChanged);
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.btnContinue.Click += new EventHandler(this.toolStripBtnContinue_Click);
            this.btnCancel.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.btnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.toolStripBtnCancel.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.toolStripBtnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.toolStripBtnContinue.Click += new EventHandler(this.toolStripBtnContinue_Click);
            this.textBoxSJBM.RootNodeString = "客户编码";
            this.textBoxSJBM.Text = "编码";
            this.textBoxSJBM.selectChanged += new TextBoxTreeView.SelectChanged(this.textBoxSJBM_selectChanged);
            this.textBoxSJBM.getListNodes += new TextBoxTreeView.GetListNodes(this.treeviewneed_getListNodes);
            this.toolStripBtnContinue.Visible = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMKHFenLei));
            this.backgroundWorker1 = new BackgroundWorker();
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x157, 0xb5);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "BMKHFenLei";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.BMKHFenLei\Aisino.Fwkp.Bmgl.Forms.BMKHFenLei.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x157, 0xb5);
            base.Controls.Add(this.xmlComponentLoader1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "BMKHFenLei";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "BMKHFenLei";
            base.Load += new EventHandler(this.BMKH_Edit_Load);
            base.ResumeLayout(false);
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void InModel()
        {
            if (this.father != null)
            {
                this.father.Selectbm = this.textBoxBM.Text;
            }
            this.khModel.BM = this.textBoxBM.Text;
            this.khModel.MC = this.textBoxWaitMC.Text;
            this.khModel.SJBM = this.textBoxSJBM.Text;
            this.khModel.WJ = 0;
            this.khModel.JM = this.textBoxJM.Text.Trim();
            this.khModel.KJM = CommonFunc.GenerateKJM(this.textBoxWaitMC.Text.Trim());
        }

        private void LoadData(string BM)
        {
            this.khModel = (BMKHModel) this.khlogical.GetModel(BM);
            this.textBoxBM.Text = this.khModel.BM;
            this.textBoxWaitMC.Text = this.khModel.MC;
            this.textBoxSJBM.Text = this.khModel.SJBM;
            this.textBoxJM.Text = this.khModel.JM;
        }

        private bool SimpleValidated()
        {
            bool flag = true;
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
                if (!("NoXJBM" != this.khlogical.ChildDetermine(this.textBoxBM.Text.Trim(), this._sjbm)) || (this.textBoxBM.Text.Length == this.khlogical.GetSuggestBMLen(this._sjbm)))
                {
                    return flag;
                }
                this.textBoxBM.Text = this.SuggestBM;
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                MessageManager.ShowMsgBox("INP-235305");
                return false;
            }
            if (this.textBoxBM.Text.Length > this.yuanBM.Length)
            {
                DAL.BMKHManager manager = new DAL.BMKHManager();
                int num = manager.BmMaxLenth(this.yuanBM) + Math.Abs((int) (this.yuanBM.Length - this.textBoxBM.Text.Length));
                if (num > 0x10)
                {
                    MessageBoxHelper.Show("修改之后将导致其下级编码过长\n请重新录入编码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
            }
            if (!this.textBoxBM.Text.StartsWith(this._sjbm))
            {
                if (this.textBoxSJBM.Text != this.khModel.SJBM)
                {
                    this.textBoxBM.Text = this.SuggestBM;
                }
                else
                {
                    this.textBoxBM.Text = this.yuanBM;
                }
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                MessageManager.ShowMsgBox("INP-235306");
                return false;
            }
            if (!("OnlyBMAndIsSelf" != this.khlogical.ChildDetermine(this.yuanBM, this._sjbm)) || (this.textBoxBM.Text.Length == this.khlogical.GetSuggestBMLen(this._sjbm)))
            {
                return flag;
            }
            if (this.textBoxSJBM.Text != this.khModel.SJBM)
            {
                this.textBoxBM.Text = this.SuggestBM;
            }
            else
            {
                this.textBoxBM.Text = this.yuanBM;
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

        private void textBoxSJBM_selectChanged(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.isUpdate && e.Node.Name.StartsWith(this.yuanBM))
            {
                MessageBoxHelper.Show("请选择非原编码族的类别！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.textBoxSJBM.Text = this._sjbm;
            }
            else
            {
                this._sjbm = e.Node.Name;
                if (this.isUpdate && (this._sjbm == this.khModel.SJBM))
                {
                    this.textBoxBM.Text = this.yuanBM;
                }
                else
                {
                    this.textBoxBM.Text = this.khlogical.TuiJianBM(e.Node.Name);
                }
                this.SuggestBM = this.khlogical.TuiJianBM(e.Node.Name);
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
            }
        }

        private void textBoxWaitMC_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxWaitMC.SelectionStart;
            this.textBoxWaitMC.Text = StringUtils.GetSubString(this.textBoxWaitMC.Text, 100);
            this.textBoxWaitMC.SelectionStart = selectionStart;
        }

        private void textBoxWaitMC_TextChangedWaitGetText(object sender, GetTextEventArgs e)
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
                    if (this.khlogical.AddCustomer(this.khModel) == "0")
                    {
                        this.father.RefreshGridTree();
                        MessageManager.ShowMsgBox("INP-235401");
                        this.textBoxBM.Text = this.khlogical.TuiJianBM(this.textBoxSJBM.Text);
                        this.textBoxWaitMC.Text = "";
                        this.textBoxJM.Text = "";
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
                switch (this.khlogical.ModifyCustomer(this.khModel, this.yuanBM))
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

