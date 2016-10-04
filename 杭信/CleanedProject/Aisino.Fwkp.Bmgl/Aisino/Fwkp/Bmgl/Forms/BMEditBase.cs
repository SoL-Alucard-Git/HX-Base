namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.IBLL;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class BMEditBase : BaseForm
    {
        protected string _bm;
        protected string _sjbm;
        protected IBMBaseManager baseLogical;
        protected BMBaseModel baseModel;
        private IContainer components;
        protected object father;
        protected bool isUpdate;
        public AisinoLBL label1;
        public AisinoLBL label10;
        public AisinoLBL label8;
        public AisinoLBL lblMC;
        protected ILog log;
        protected string SuggestBM;
        public AisinoTXT textBoxBM;
        public AisinoTXT textBoxJM;
        public TextBoxTreeView textBoxSJBM;
        public TextBoxWait textBoxWaitMC;
        public ToolStrip toolStrip1;
        public ToolStripButton toolStripBtnCancel;
        public ToolStripButton toolStripBtnContinue;
        public ToolStripButton toolStripBtnSave;
        protected string yuanBM;

        public BMEditBase()
        {
            this._bm = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
        }

        public BMEditBase(string BM, bool Isupdate)
        {
            this._bm = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
            this._bm = BM;
            this.yuanBM = BM;
            this.isUpdate = Isupdate;
            this.InitData();
        }

        public BMEditBase(string SJBM, object Father)
        {
            this._bm = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.Initialize();
            this._sjbm = SJBM;
            this.father = Father;
            this.InitData();
        }

        protected virtual void BMEditBase_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.isUpdate)
                {
                    this.LoadData(this._bm);
                    this._sjbm = this.baseModel.SJBM;
                    this.textBoxSJBM.SelectBM = this.baseModel.SJBM;
                    this.toolStripBtnContinue.Visible = false;
                }
                else
                {
                    this.textBoxSJBM.SelectBM = this._sjbm;
                    this.SuggestBM = this.baseLogical.TuiJianBM(this._sjbm);
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

        protected virtual void InitData()
        {
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.textBoxWaitMC.WaterMarkString = "编码编辑基类";
            this.textBoxWaitMC.TextChanged += new EventHandler(this.textBoxWaitMC_TextChanged);
            this.textBoxBM.KeyPress += new KeyPressEventHandler(this.textBoxBM_KeyPress);
            this.textBoxBM.TextChanged += new EventHandler(this.textBoxBM_TextChanged);
            this.textBoxBM.Validating += new CancelEventHandler(this.textBoxBM_Validating);
            this.textBoxJM.KeyPress += new KeyPressEventHandler(this.textBoxJM_KeyPress);
            this.textBoxJM.TextChanged += new EventHandler(this.textBoxJM_TextChanged);
            this.toolStripBtnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.toolStripBtnContinue.Click += new EventHandler(this.toolStripBtnContinue_Click);
            this.toolStripBtnCancel.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.textBoxSJBM.selectChanged += new TextBoxTreeView.SelectChanged(this.textBoxSJBM_selectChanged);
            this.textBoxSJBM.getListNodes += new TextBoxTreeView.GetListNodes(this.treeviewneed_getListNodes);
            this.textBoxSJBM.RootNodeString = "基类编码";
            this.textBoxSJBM.Text = "编码";
            this.toolStripBtnContinue.Visible = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMEditBase));
            this.toolStripBtnSave = new ToolStripButton();
            this.toolStripBtnCancel = new ToolStripButton();
            this.toolStripBtnContinue = new ToolStripButton();
            this.toolStrip1 = new ToolStrip();
            this.textBoxBM = new AisinoTXT();
            this.textBoxJM = new AisinoTXT();
            this.label10 = new AisinoLBL();
            this.label8 = new AisinoLBL();
            this.label1 = new AisinoLBL();
            this.textBoxSJBM = new TextBoxTreeView();
            this.textBoxWaitMC = new TextBoxWait();
            this.lblMC = new AisinoLBL();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.toolStripBtnSave.Image = Resources.保存;
            this.toolStripBtnSave.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnSave.Name = "toolStripBtnSave";
            this.toolStripBtnSave.Size = new Size(0x34, 0x18);
            this.toolStripBtnSave.Text = "保存";
            this.toolStripBtnCancel.Image = Resources.取消;
            this.toolStripBtnCancel.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnCancel.Name = "toolStripBtnCancel";
            this.toolStripBtnCancel.Size = new Size(0x34, 0x18);
            this.toolStripBtnCancel.Text = "取消";
            this.toolStripBtnContinue.Image = Resources.保存;
            this.toolStripBtnContinue.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnContinue.Name = "toolStripBtnContinue";
            this.toolStripBtnContinue.Size = new Size(0x4c, 0x18);
            this.toolStripBtnContinue.Text = "继续添加";
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripBtnSave, this.toolStripBtnCancel, this.toolStripBtnContinue });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x259, 0x1b);
            this.toolStrip1.TabIndex = 0x30;
            this.toolStrip1.Text = "toolStrip1";
            this.textBoxBM.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxBM.Location = new Point(0x5b, 0x48);
            this.textBoxBM.Name = "textBoxBM";
            this.textBoxBM.Size = new Size(0xaf, 0x15);
            this.textBoxBM.TabIndex = 50;
            this.textBoxJM.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxJM.Location = new Point(0x5b, 0x66);
            this.textBoxJM.Name = "textBoxJM";
            this.textBoxJM.Size = new Size(0xaf, 0x15);
            this.textBoxJM.TabIndex = 0x33;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(50, 0x6a);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x1d, 12);
            this.label10.TabIndex = 0x36;
            this.label10.Text = "简码";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x1a, 0x2e);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x34;
            this.label8.Text = "上级编码";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x2c, 0x4c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 0x35;
            this.label1.Text = "*编码";
            this.textBoxSJBM.BackColor = SystemColors.Window;
            this.textBoxSJBM.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxSJBM.Cursor = Cursors.Arrow;
            this.textBoxSJBM.Location = new Point(0x5b, 0x2a);
            this.textBoxSJBM.Name = "textBoxSJBM";
            this.textBoxSJBM.ReadOnly = true;
            this.textBoxSJBM.RootNodeString = "根节点";
            this.textBoxSJBM.SelectBM = null;
            this.textBoxSJBM.Size = new Size(0xaf, 0x15);
            this.textBoxSJBM.TabIndex = 0x31;
            this.textBoxSJBM.Text = "下拉树";
            this.textBoxWaitMC.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxWaitMC.FontWater = new Font("微软雅黑", 8f, FontStyle.Italic);
            this.textBoxWaitMC.Location = new Point(0x16a, 0x25);
            this.textBoxWaitMC.Name = "textBoxWaitMC";
            this.textBoxWaitMC.Size = new Size(0xa9, 0x15);
            this.textBoxWaitMC.TabIndex = 0x8e;
            this.textBoxWaitMC.WaitMilliSeconds = 0x3e8;
            this.textBoxWaitMC.WaterMarkString = "水印文字";
            this.lblMC.AutoSize = true;
            this.lblMC.Location = new Point(0x129, 0x29);
            this.lblMC.Name = "lblMC";
            this.lblMC.Size = new Size(0x2f, 12);
            this.lblMC.TabIndex = 0x8f;
            this.lblMC.Text = "*xx名称";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.ClientSize = new Size(0x259, 0xc0);
            base.Controls.Add(this.lblMC);
            base.Controls.Add(this.textBoxWaitMC);
            base.Controls.Add(this.textBoxSJBM);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.textBoxJM);
            base.Controls.Add(this.textBoxBM);
            base.Controls.Add(this.toolStrip1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "BMEditBase";
            this.Text = "BMEditBase";
            base.Load += new EventHandler(this.BMEditBase_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected virtual void LoadData(string BM)
        {
            this.baseModel = this.baseLogical.GetModel(BM);
            this.textBoxSJBM.Text = this.baseModel.SJBM;
            this.textBoxBM.Text = this.baseModel.BM;
            this.textBoxWaitMC.Text = this.baseModel.MC;
            this.textBoxJM.Text = this.baseModel.JM;
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

        protected bool textBoxBM_Validating()
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
                if (!("NoXJBM" != this.baseLogical.ChildDetermine(this.textBoxBM.Text.Trim(), this._sjbm)) || (this.textBoxBM.Text.Length == this.baseLogical.GetSuggestBMLen(this._sjbm)))
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
                if (this.textBoxSJBM.Text != this.baseModel.SJBM)
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
            if (!("OnlyBMAndIsSelf" != this.baseLogical.ChildDetermine(this.yuanBM, this._sjbm)) || (this.textBoxBM.Text.Length == this.baseLogical.GetSuggestBMLen(this._sjbm)))
            {
                return flag;
            }
            if (this.textBoxSJBM.Text != this.baseModel.SJBM)
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

        protected void textBoxBM_Validating(object sender, CancelEventArgs e)
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
            this._sjbm = e.Node.Name;
            this.SuggestBM = this.baseLogical.TuiJianBM(this._sjbm);
            if (this.isUpdate && (this._sjbm == this.baseModel.SJBM))
            {
                this.textBoxBM.Text = this._bm;
            }
            else
            {
                this.textBoxBM.Text = this.baseLogical.TuiJianBM(e.Node.Name);
            }
            this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
        }

        protected virtual void textBoxWaitMC_TextChanged(object sender, EventArgs e)
        {
        }

        protected void toolStripBtnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected virtual void toolStripBtnContinue_Click(object sender, EventArgs e)
        {
        }

        protected virtual void toolStripBtnSave_Click(object sender, EventArgs e)
        {
        }

        private List<TreeNodeTemp> treeviewneed_getListNodes(string ParentBM)
        {
            return this.baseLogical.listNodes(ParentBM);
        }
    }
}

