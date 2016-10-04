namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.DAL;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class BMSPFenLei : BaseForm
    {
        private string _bm;
        private string _sjbm;
        private int BmLength;
        private AisinoBTN button1;
        private AisinoBTN button2;
        private AisinoBTN button3;
        private AisinoCMB chbXTHide;
        private IContainer components;
        private BMSP father;
        private string hashString;
        private bool isSaveSubHide;
        private bool isUpdate;
        private bool IsXT;
        private AisinoLBL label1;
        private AisinoLBL label10;
        private AisinoLBL label2;
        private AisinoLBL label8;
        private AisinoLBL labelDoing;
        private AisinoLBL labelKJM;
        private AisinoLBL lblXTHide;
        private ILog log;
        private string oldHide;
        private AisinoPNL panelDoing;
        private AisinoPIC pictureBox1;
        private string resultModifyBM;
        private BLL.BMSPManager splogical;
        private BMSPModel spModel;
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

        public BMSPFenLei(string SJBM, BMSP Father)
        {
            this.splogical = new BLL.BMSPManager();
            this.spModel = new BMSPModel();
            this.log = LogUtil.GetLogger<BMSPFenLei>();
            this._bm = "";
            this._sjbm = "";
            this.oldHide = string.Empty;
            this.hashString = string.Empty;
            this.SuggestBM = "";
            this.Initialize();
            this._sjbm = SJBM;
            this.father = Father;
            this.Text = "商品分类编码添加";
        }

        public BMSPFenLei(string BM, bool Isupdate = true)
        {
            this.splogical = new BLL.BMSPManager();
            this.spModel = new BMSPModel();
            this.log = LogUtil.GetLogger<BMSPFenLei>();
            this._bm = "";
            this._sjbm = "";
            this.oldHide = string.Empty;
            this.hashString = string.Empty;
            this.SuggestBM = "";
            this.Initialize();
            this._bm = BM;
            this.yuanBM = BM;
            this.isUpdate = Isupdate;
            this.Text = "商品分类编码编辑";
        }

        public BMSPFenLei(BMSP Father, string BM, bool Isupdate = true)
        {
            this.splogical = new BLL.BMSPManager();
            this.spModel = new BMSPModel();
            this.log = LogUtil.GetLogger<BMSPFenLei>();
            this._bm = "";
            this._sjbm = "";
            this.oldHide = string.Empty;
            this.hashString = string.Empty;
            this.SuggestBM = "";
            this.Initialize();
            this._bm = BM;
            this.yuanBM = BM;
            this.isUpdate = Isupdate;
            this.father = Father;
            this.Text = "商品分类编码编辑";
        }

        private void Add()
        {
            this.InModel();
            string str = this.splogical.AddMerchandise(this.spModel);
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
                MessageManager.ShowMsgBox("INP-235402");
                this.log.Error("商品分类增加失败：" + str);
            }
        }

        private void BMSP_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.isUpdate)
                {
                    this.LoadData(this._bm);
                    this._sjbm = this.spModel.SJBM;
                    this.textBoxSJBM.SelectBM = this.spModel.SJBM;
                    this.toolStripBtnContinue.Visible = false;
                    this.button3.Visible = false;
                    if (!string.IsNullOrEmpty(this.spModel.XTHASH))
                    {
                        this.IsXT = true;
                        this.chbXTHide.Visible = true;
                        this.lblXTHide.Visible = true;
                        this.chbXTHide.Items.Add("是");
                        this.chbXTHide.Items.Add("否");
                        this.textBoxWaitMC.Enabled = false;
                        if (this.spModel.ISHIDE.Substring(0, 1) == "1")
                        {
                            this.chbXTHide.SelectedText = "是";
                        }
                        else
                        {
                            this.chbXTHide.SelectedText = "否";
                        }
                        this.oldHide = this.spModel.ISHIDE.Substring(0, 1);
                        this.hashString = this.spModel.XTHASH;
                    }
                }
                else
                {
                    this.textBoxSJBM.SelectBM = this._sjbm;
                    this.SuggestBM = this.splogical.TuiJianBM(this._sjbm);
                    this.BmLength = this.SuggestBM.Length;
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
            this.textBoxSJBM = this.xmlComponentLoader1.GetControlByName<TextBoxTreeView>("textBoxSJBM");
            this.panelDoing = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panelDoing");
            this.pictureBox1 = this.xmlComponentLoader1.GetControlByName<AisinoPIC>("pictureBox1");
            this.labelDoing = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelDoing");
            this.toolStripBtnSave = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnSave");
            this.toolStripBtnCancel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnCancel");
            this.toolStripBtnContinue = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnContinue");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label8 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label8");
            this.textBoxBM = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxBM");
            this.button1 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button1");
            this.button3 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button3");
            this.button2 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button2");
            this.textBoxJM = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxJM");
            this.labelKJM = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelKJM");
            this.label10 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label10");
            this.chbXTHide = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("chbXTHide");
            this.lblXTHide = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblXTHide");
            this.textBoxWaitMC = this.xmlComponentLoader1.GetControlByName<TextBoxWait>("textBoxWaitMC");
            this.toolStripBtnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.toolStripBtnCancel.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.toolStripBtnContinue.Click += new EventHandler(this.toolStripBtnContinue_Click);
            this.textBoxBM.KeyPress += new KeyPressEventHandler(this.textBoxBM_KeyPress);
            this.textBoxBM.TextChanged += new EventHandler(this.textBoxBM_TextChanged);
            this.textBoxBM.Validating += new CancelEventHandler(this.textBoxBM_Validating);
            this.textBoxWaitMC.TextChangedWaitGetText += new GetTextEventHandler(this.textBoxWaitBM_TextChangedWaitGetText);
            this.textBoxWaitMC.WaterMarkString = "商品分类名称";
            this.button1.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.button2.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.button3.Click += new EventHandler(this.toolStripBtnContinue_Click);
            this.textBoxSJBM.RootNodeString = "商品编码";
            this.textBoxSJBM.Text = "编码";
            this.textBoxSJBM.selectChanged += new TextBoxTreeView.SelectChanged(this.textBoxSJBM_selectChanged);
            this.textBoxSJBM.getListNodes += new TextBoxTreeView.GetListNodes(this.treeviewneed_getListNodes);
            this.toolStripBtnContinue.Visible = false;
            this.chbXTHide.Visible = false;
            this.lblXTHide.Visible = false;
            this.textBoxWaitMC.TextChanged += new EventHandler(this.textBoxWaitMC_TextChanged);
            this.textBoxJM.KeyPress += new KeyPressEventHandler(this.textBoxJM_KeyPress);
            this.textBoxJM.TextChanged += new EventHandler(this.textBoxJM_TextChanged);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMSPFenLei));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x157, 0xb5);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "BMSPFenLei";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.BMSPFenLei\Aisino.Fwkp.Bmgl.Forms.BMSPFenLei.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x157, 0xb5);
            base.Controls.Add(this.xmlComponentLoader1);
            base.MaximizeBox = false;
            this.MaximumSize = new Size(500, 350);
            base.MinimizeBox = false;
            base.Name = "BMSPFenLei";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "BMSPFenLei";
            base.Load += new EventHandler(this.BMSP_Edit_Load);
            base.ResumeLayout(false);
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void InModel()
        {
            if (this.father != null)
            {
                this.father.Selectbm = this.textBoxBM.Text.Trim();
            }
            this.spModel.BM = this.textBoxBM.Text.Trim();
            this.spModel.MC = this.textBoxWaitMC.Text.Trim();
            this.spModel.JM = this.textBoxJM.Text.Trim();
            this.spModel.SJBM = this.textBoxSJBM.Text.Trim();
            this.spModel.SLV = 0.0;
            this.spModel.DJ = 0.0;
            this.spModel.HSJBZ = true;
            this.spModel.HYSY = false;
            this.spModel.KJM = CommonFunc.GenerateKJM(this.textBoxWaitMC.Text.Trim());
            this.spModel.JM = this.textBoxJM.Text.Trim();
            this.spModel.WJ = 0;
            if (this.IsXT)
            {
                string str = "0";
                if (this.chbXTHide.Text == "是")
                {
                    str = "1";
                }
                this.spModel.ISHIDE = str + this.spModel.ISHIDE.Substring(1, this.spModel.ISHIDE.Length - 1);
                if ((this.oldHide != str) && (MessageManager.ShowMsgBox("INP-235127") == DialogResult.OK))
                {
                    this.isSaveSubHide = true;
                }
            }
            else
            {
                this.spModel.ISHIDE = "0000000000";
            }
        }

        private void LoadData(string BM)
        {
            this.spModel = (BMSPModel) this.splogical.GetModel(BM);
            this.textBoxBM.Text = this.spModel.BM;
            this.textBoxWaitMC.Text = this.spModel.MC;
            this.textBoxJM.Text = this.spModel.JM;
            this.textBoxSJBM.Text = this.spModel.SJBM;
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
                return flag;
            }
            if (this.IsXT && (this.hashString.Length != 1))
            {
                string str = this.hashString.Substring(0, 1);
                string str2 = this.hashString.Substring(1, this.hashString.Length - 1);
                if (!XTSP_Crypt.EncodeXTGoodsName(str + this.spModel.MC).Equals(str2))
                {
                    MessageManager.ShowMsgBox("INP-235126");
                    flag = false;
                }
                if (this.spModel.SJBM != this.textBoxSJBM.Text.Trim())
                {
                    MessageManager.ShowMsgBox("235128");
                    flag = false;
                }
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
                if (("NoXJBM" != this.splogical.ChildDetermine(this.textBoxBM.Text.Trim(), this._sjbm)) && (this.textBoxBM.Text.Length != this.splogical.GetSuggestBMLen(this._sjbm)))
                {
                    this.textBoxBM.Text = this.SuggestBM;
                    this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                    MessageManager.ShowMsgBox("INP-235305");
                    flag = false;
                }
                return flag;
            }
            if (this.textBoxBM.Text.Length > this.yuanBM.Length)
            {
                DAL.BMSPManager manager = new DAL.BMSPManager();
                int num = manager.BmMaxLenth(this.yuanBM) + Math.Abs((int) (this.yuanBM.Length - this.textBoxBM.Text.Length));
                if (num > 0x10)
                {
                    MessageBoxHelper.Show("修改之后将导致其下级编码过长\n请重新录入编码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
            }
            if (!this.textBoxBM.Text.StartsWith(this._sjbm))
            {
                if (this.textBoxSJBM.Text != this.spModel.SJBM)
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
            if (!("OnlyBMAndIsSelf" != this.splogical.ChildDetermine(this.yuanBM, this._sjbm)) || (this.textBoxBM.Text.Length == this.splogical.GetSuggestBMLen(this._sjbm)))
            {
                return flag;
            }
            if (this.textBoxSJBM.Text != this.spModel.SJBM)
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
                MessageBoxHelper.Show(exception.ToString());
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
                MessageBoxHelper.Show("请选择非原编码族的类别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.textBoxSJBM.Text = this._sjbm;
            }
            else
            {
                this._sjbm = e.Node.Name;
                if (this.isUpdate && (this._sjbm == this.spModel.SJBM))
                {
                    this.textBoxBM.Text = this.yuanBM;
                }
                else
                {
                    this.textBoxBM.Text = this.splogical.TuiJianBM(e.Node.Name);
                }
                this.BmLength = this._bm.Length;
                this.SuggestBM = this.splogical.TuiJianBM(e.Node.Name);
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
            }
        }

        private void textBoxWaitBM_TextChangedWaitGetText(object sender, GetTextEventArgs e)
        {
        }

        private void textBoxWaitMC_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxWaitMC.SelectionStart;
            this.textBoxWaitMC.Text = StringUtils.GetSubString(this.textBoxWaitMC.Text, 100);
            this.textBoxWaitMC.SelectionStart = selectionStart;
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
                    string str = this.splogical.AddMerchandise(this.spModel);
                    if (str == "0")
                    {
                        this.father.RefreshGridTree();
                        this.textBoxBM.Text = this.splogical.TuiJianBM(this.textBoxSJBM.Text);
                        this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                        this.textBoxWaitMC.Text = "";
                        this.textBoxJM.Text = "";
                    }
                    else
                    {
                        MessageManager.ShowMsgBox("INP-235402");
                        this.log.Error("商品分类增加失败：" + str);
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
                        this.UpdateSP();
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
            return this.splogical.listNodes(ParentBM);
        }

        private void UpdateSP()
        {
            this.InModel();
            if (this.yuanBM == this.spModel.BM)
            {
                string str = this.splogical.ModifyData(this.spModel, this.yuanBM);
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
                    MessageManager.ShowMsgBox("INP-235404");
                    this.log.Error("商品分类修改失败：" + str);
                }
            }
            else
            {
                this.resultModifyBM = this.splogical.UpdateSubNodesSJBM(this.spModel, this.yuanBM);
                if (this.resultModifyBM == "0")
                {
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
                    this.log.Error("商品分类修改失败：" + this.resultModifyBM);
                }
            }
            if (this.IsXT && this.isSaveSubHide)
            {
                this.splogical.UpdateXTIsHide(this.yuanBM, this.spModel.ISHIDE);
            }
        }
    }
}

