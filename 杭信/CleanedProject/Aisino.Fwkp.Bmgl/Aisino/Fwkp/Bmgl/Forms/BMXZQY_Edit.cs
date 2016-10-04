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
    using System.Windows.Forms;

    public class BMXZQY_Edit : BaseForm
    {
        private string _bm;
        private AisinoLBL aisinoLBL_BM;
        private AisinoLBL aisinoLBL_MC;
        private IContainer components;
        private BMXZQY father;
        private bool isUpdate;
        private BMXZQYManager khlogical;
        private ILog log;
        private string SuggestBM;
        private AisinoTXT textBox_BM;
        private AisinoTXT textBox_MC;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnCancel;
        private ToolStripButton toolStripBtnContinue;
        private ToolStripButton toolStripBtnSave;
        private XmlComponentLoader xmlComponentLoader1;
        private BMXZQYModel xzqyModel;
        private string yuanBM;

        public BMXZQY_Edit(string SJBM, BMXZQY Father)
        {
            this.khlogical = new BMXZQYManager();
            this.xzqyModel = new BMXZQYModel();
            this.log = LogUtil.GetLogger<BMXZQY_Edit>();
            this._bm = "";
            this.SuggestBM = "";
            this.Initialize();
            this.father = Father;
            this.Text = "行政区域编码添加";
        }

        public BMXZQY_Edit(string BM, bool Isupdate)
        {
            this.khlogical = new BMXZQYManager();
            this.xzqyModel = new BMXZQYModel();
            this.log = LogUtil.GetLogger<BMXZQY_Edit>();
            this._bm = "";
            this.SuggestBM = "";
            this.Initialize();
            this._bm = BM;
            this.yuanBM = BM;
            this.isUpdate = Isupdate;
            this.Text = "行政区域编码编辑";
        }

        private void BMXZQY_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.isUpdate)
                {
                    this.LoadData(this._bm);
                    this.toolStripBtnContinue.Visible = false;
                }
                else
                {
                    this.textBox_BM.Text = "";
                    this.textBox_MC.Text = "";
                }
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
            this.aisinoLBL_BM = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL_BM");
            this.textBox_BM = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox_BM");
            this.aisinoLBL_MC = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL_MC");
            this.textBox_MC = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox_MC");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripBtnSave = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnSave");
            this.toolStripBtnContinue = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnContinue");
            this.toolStripBtnCancel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripBtnCancel");
            this.toolStripBtnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.toolStripBtnContinue.Click += new EventHandler(this.toolStripBtnContinue_Click);
            this.toolStripBtnCancel.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.textBox_BM.KeyPress += new KeyPressEventHandler(this.textBox_BM_KeyPress);
            this.textBox_BM.TextChanged += new EventHandler(this.textBox_BM_TextChanged);
            this.textBox_MC.TextChanged += new EventHandler(this.textBox_MC_TextChanged);
            this.toolStripBtnContinue.Visible = false;
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMXZQY_Edit));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x13a, 0x8a);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "BMKH_Edit";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Bmgl.Forms.BMXZQY_Edit\Aisino.Fwkp.Bmgl.Forms.BMXZQY_Edit.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x13a, 0x8a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            this.MaximumSize = new Size(700, 400);
            base.MinimizeBox = false;
            base.Name = "BMXZQY_Edit";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "行政区编码编辑";
            base.Load += new EventHandler(this.BMXZQY_Edit_Load);
            base.ResumeLayout(false);
        }

        private void InModel()
        {
            this.xzqyModel.BM = this.textBox_BM.Text.Trim();
            this.xzqyModel.MC = "  " + this.textBox_MC.Text.Trim();
        }

        private void LoadData(string BM)
        {
            this.xzqyModel = this.khlogical.GetModel(BM);
            this.textBox_BM.Text = this.xzqyModel.BM;
            this.textBox_MC.Text = this.xzqyModel.MC;
        }

        private bool SimpleValidated()
        {
            bool flag = true;
            if (this.textBox_BM.Text.Trim().Length == 0)
            {
                this.textBox_BM.Focus();
                flag = false;
            }
            if (this.textBox_MC.Text.Trim().Length == 0)
            {
                this.textBox_MC.Focus();
                flag = false;
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox("INP-235309");
            }
            return flag;
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
                string str = this.textBox_BM.Text.Trim();
                for (int i = 0; i < str.Length; i++)
                {
                    if (!char.IsDigit(str[i]) && !char.IsUpper(str[i]))
                    {
                        MessageBoxHelper.Show("编码必须为数字!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            if (this.textBox_MC.Text.Trim() != "")
            {
                int selectionStart = this.textBox_MC.SelectionStart;
                this.textBox_MC.Text = StringUtils.GetSubString(this.textBox_MC.Text, 40);
                this.textBox_MC.SelectionStart = selectionStart;
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
            this._bm = this.khlogical.TuiJianBM(e.Node.Name);
            this.textBox_BM.Text = this._bm;
            this.textBox_BM.Select(this.textBox_BM.Text.Length, 0);
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
                    string str = "";
                    if (this.textBox_BM.Text.Length == 4)
                    {
                        str = this.textBox_BM.Text.Substring(2, 2);
                        if (str == "00")
                        {
                            this.xzqyModel.MC = this.xzqyModel.MC.Trim();
                            if (MessageBoxHelper.Show(" 确定要改动上级编码？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                        }
                    }
                    int xZQYSJBM = this.khlogical.GetXZQYSJBM(this.textBox_BM.Text);
                    if (xZQYSJBM == 0)
                    {
                        MessageBoxHelper.Show("上级编码不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if ((1 == xZQYSJBM) && (str == "00"))
                    {
                        MessageBoxHelper.Show("上级编码已经存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        string str2 = this.khlogical.AddDistrict(this.xzqyModel);
                        if (str2 == "0")
                        {
                            this.father.RefreshGrid();
                            this.textBox_BM.Text = "";
                            this.textBox_MC.Text = "";
                            this.textBox_BM.Focus();
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

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBoxBM_Validating() && this.SimpleValidated())
                {
                    if (!this.isUpdate)
                    {
                        this.InModel();
                        string str = "";
                        if (this.textBox_BM.Text.Length == 4)
                        {
                            str = this.textBox_BM.Text.Substring(2, 2);
                            if (str == "00")
                            {
                                this.xzqyModel.MC = this.xzqyModel.MC.Trim();
                            }
                        }
                        int xZQYSJBM = this.khlogical.GetXZQYSJBM(this.textBox_BM.Text);
                        if ((xZQYSJBM == 0) && (str != "00"))
                        {
                            MessageBoxHelper.Show("上级编码不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else if ((1 == xZQYSJBM) && (str == "00"))
                        {
                            MessageBoxHelper.Show("上级编码已经存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            string str2 = this.khlogical.AddDistrict(this.xzqyModel);
                            if (str2 == "0")
                            {
                                MessageManager.ShowMsgBox("INP-235201");
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
                    else
                    {
                        this.InModel();
                        string str3 = "";
                        if (this.textBox_BM.Text.Length == 4)
                        {
                            str3 = this.textBox_BM.Text.Substring(2, 2);
                            if (str3 == "00")
                            {
                                this.xzqyModel.MC = this.xzqyModel.MC.Trim();
                                if (MessageBoxHelper.Show(" 确定要改动上级编码？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    return;
                                }
                            }
                        }
                        if (this.yuanBM != this.xzqyModel.BM)
                        {
                            int num2 = this.khlogical.GetXZQYSJBM(this.textBox_BM.Text);
                            if ((num2 == 0) && (str3 != "00"))
                            {
                                MessageBoxHelper.Show("上级编码不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                return;
                            }
                            if ((1 == num2) && (str3 == "00"))
                            {
                                MessageBoxHelper.Show("上级编码已经存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                return;
                            }
                        }
                        string str4 = this.khlogical.ModifyDistrict(this.xzqyModel, this.yuanBM);
                        if (str4 == "0")
                        {
                            MessageManager.ShowMsgBox("INP-235303");
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        else if (str4 == "e1")
                        {
                            MessageManager.ShowMsgBox("INP-235108");
                        }
                        else
                        {
                            MessageManager.ShowMsgBox("INP-235304");
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

