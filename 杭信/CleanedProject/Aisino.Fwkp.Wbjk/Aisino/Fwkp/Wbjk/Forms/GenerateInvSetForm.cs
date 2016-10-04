namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class GenerateInvSetForm : BaseForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnGenerate;
        private AisinoCHK ckShow;
        private IContainer components = null;
        private GenerateInvoice generBL = GenerateInvoice.Instance;
        private AisinoGRP groupBox1;
        private AisinoGRP groupBox2;
        private InvType invType;
        public int IsSetOnly = 0;
        private AisinoLBL label1;
        private AisinoLBL label3;
        public int NCPBZ = 0;
        private InvSplitPara para;
        private AisinoRDO rdForbidden;
        private AisinoRDO rdForbidden2;
        private AisinoRDO rdGenerlist;
        private AisinoRDO rdGenerlist2;
        private AisinoRDO rdSplit;
        private AisinoRDO rdSplit2;

        public GenerateInvSetForm(InvType typeFP)
        {
            this.InitializeComponent();
            this.invType = typeFP;
            this.para = new InvSplitPara();
            if (typeFP == InvType.Special)
            {
                this.label1.Text = "汉字防伪开具专用发票设置";
                this.label3.Text = "提示：此设置只对汉字防伪用户开具“清单行商品名称”为空的超过汉字防伪限制的专用发票单据起作用";
                this.ckShow.Text = "在专用发票生成之前显示";
            }
            else if (typeFP == InvType.Common)
            {
                this.label1.Text = "汉字防伪开具普通发票设置";
                this.label3.Text = "提示：此设置只对汉字防伪用户开具“清单行商品名称”为空的超过汉字防伪限制的普通发票单据起作用";
                this.ckShow.Text = "在普通发票生成之前显示";
            }
            this.para.GetInvSplitPara(typeFP);
            this.rdForbidden.Checked = this.para.below7ForbiddenInv;
            this.rdSplit.Checked = this.para.below7Split;
            this.rdGenerlist.Checked = this.para.below7Generlist;
            this.rdForbidden2.Checked = this.para.above7ForbiddenInv;
            this.rdSplit2.Checked = this.para.above7Split;
            this.rdGenerlist2.Checked = this.para.above7Generlist;
            this.ckShow.Checked = this.para.ShowSetForm;
            if (WbjkEntry.RegFlag_ST || WbjkEntry.RegFlag_KT)
            {
                this.rdSplit2.Visible = false;
            }
            else if (WbjkEntry.RegFlag_JT)
            {
                this.rdGenerlist2.Visible = true;
            }
        }

        private bool _Check(GenerateFP generateFP)
        {
            TaxCard taxCard = TaxCardFactory.CreateTaxCard();
            if (!this.CanXTInv(taxCard))
            {
                MessageManager.ShowMsgBox("INP-242132");
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            this.para.below7ForbiddenInv = this.rdForbidden.Checked;
            this.para.below7Split = this.rdSplit.Checked;
            this.para.below7Generlist = this.rdGenerlist.Checked;
            this.para.above7ForbiddenInv = this.rdForbidden2.Checked;
            this.para.above7Split = this.rdSplit2.Checked;
            this.para.above7Generlist = this.rdGenerlist2.Checked;
            this.para.ShowSetForm = this.ckShow.Checked;
            this.para.SetInvSplitPara();
            base.TopMost = false;
            base.Visible = false;
            if (this.IsSetOnly == 0)
            {
                GenerateFP efp;
                JSFPJSelect select;
                string[] current;
                if (this.NCPBZ == 0)
                {
                    efp = new GenerateFP(this.invType);
                    if (!this._Check(efp))
                    {
                        base.Close();
                        return;
                    }
                    select = new JSFPJSelect(2);
                    if (select.ShowDialog() == DialogResult.OK)
                    {
                        current = efp.GetCurrent(0x29);
                        efp.ShowDialog(this);
                    }
                }
                else if (this.NCPBZ == 1)
                {
                    efp = new GenerateFP(this.invType, 1);
                    if (!this._Check(efp))
                    {
                        base.Close();
                        return;
                    }
                    select = new JSFPJSelect(2);
                    if (select.ShowDialog() == DialogResult.OK)
                    {
                        current = efp.GetCurrent(0x29);
                        efp.ShowDialog(this);
                    }
                }
                else if (this.NCPBZ == 2)
                {
                    efp = new GenerateFP(this.invType, 2);
                    if (!this._Check(efp))
                    {
                        base.Close();
                        return;
                    }
                    select = new JSFPJSelect(2);
                    if (select.ShowDialog() == DialogResult.OK)
                    {
                        current = efp.GetCurrent(0x29);
                        efp.ShowDialog(this);
                    }
                }
            }
            base.Close();
        }

        private bool CanXTInv(TaxCard taxCard)
        {
            try
            {
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMIsNeedImportXTSP", null);
                if ((((objArray != null) && (objArray.Length > 0)) && (objArray[0] != null)) && Convert.ToBoolean(objArray[0]))
                {
                    ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMImportXTSP", null);
                }
                if (taxCard.get_QYLX().ISXT)
                {
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMCheckXTSP", null);
                    if (!(((objArray2 != null) && (objArray2[0] is bool)) && Convert.ToBoolean(objArray2[0])))
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GenerateInvSetForm_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(GenerateInvSetForm));
            this.label1 = new AisinoLBL();
            this.label3 = new AisinoLBL();
            this.groupBox1 = new AisinoGRP();
            this.rdForbidden = new AisinoRDO();
            this.rdSplit = new AisinoRDO();
            this.rdGenerlist = new AisinoRDO();
            this.groupBox2 = new AisinoGRP();
            this.rdForbidden2 = new AisinoRDO();
            this.rdSplit2 = new AisinoRDO();
            this.rdGenerlist2 = new AisinoRDO();
            this.ckShow = new AisinoCHK();
            this.btnGenerate = new AisinoBTN();
            this.btnCancel = new AisinoBTN();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Font = new Font("宋体", 15f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(0x15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x105, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "汉字防伪开具专用发票设置";
            this.label3.BackColor = Color.Transparent;
            this.label3.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.ForeColor = Color.Red;
            this.label3.Location = new Point(0x16, 0x2c);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x164, 0x30);
            this.label3.TabIndex = 2;
            this.label3.Text = "提示：此设置只对汉字防伪用户开具“清单行商品名称”为空的超过汉字防伪限制的专用发票单据起作用";
            this.groupBox1.BackColor = Color.Transparent;
            this.groupBox1.Controls.Add(this.rdForbidden);
            this.groupBox1.Controls.Add(this.rdSplit);
            this.groupBox1.Controls.Add(this.rdGenerlist);
            this.groupBox1.Location = new Point(0x19, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(130, 0x67);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "小于等于7行的单据";
            this.rdForbidden.AutoSize = true;
            this.rdForbidden.Location = new Point(0x10, 0x41);
            this.rdForbidden.Name = "rdForbidden";
            this.rdForbidden.Size = new Size(0x6b, 0x10);
            this.rdForbidden.TabIndex = 2;
            this.rdForbidden.TabStop = true;
            this.rdForbidden.Text = "不允许开具发票";
            this.rdForbidden.UseVisualStyleBackColor = true;
            this.rdSplit.AutoSize = true;
            this.rdSplit.Location = new Point(0x10, 0x2b);
            this.rdSplit.Name = "rdSplit";
            this.rdSplit.Size = new Size(0x2f, 0x10);
            this.rdSplit.TabIndex = 1;
            this.rdSplit.TabStop = true;
            this.rdSplit.Text = "拆分";
            this.rdSplit.UseVisualStyleBackColor = true;
            this.rdGenerlist.AutoSize = true;
            this.rdGenerlist.Location = new Point(0x10, 0x15);
            this.rdGenerlist.Name = "rdGenerlist";
            this.rdGenerlist.Size = new Size(0x47, 0x10);
            this.rdGenerlist.TabIndex = 0;
            this.rdGenerlist.TabStop = true;
            this.rdGenerlist.Text = "开具清单";
            this.rdGenerlist.UseVisualStyleBackColor = true;
            this.groupBox2.BackColor = Color.Transparent;
            this.groupBox2.Controls.Add(this.rdForbidden2);
            this.groupBox2.Controls.Add(this.rdSplit2);
            this.groupBox2.Controls.Add(this.rdGenerlist2);
            this.groupBox2.Location = new Point(0xd6, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(130, 0x67);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "大于7行的单据";
            this.rdForbidden2.AutoSize = true;
            this.rdForbidden2.Location = new Point(0x10, 0x41);
            this.rdForbidden2.Name = "rdForbidden2";
            this.rdForbidden2.Size = new Size(0x6b, 0x10);
            this.rdForbidden2.TabIndex = 2;
            this.rdForbidden2.TabStop = true;
            this.rdForbidden2.Text = "不允许开具发票";
            this.rdForbidden2.UseVisualStyleBackColor = true;
            this.rdSplit2.AutoSize = true;
            this.rdSplit2.Location = new Point(0x10, 0x2b);
            this.rdSplit2.Name = "rdSplit2";
            this.rdSplit2.Size = new Size(0x2f, 0x10);
            this.rdSplit2.TabIndex = 1;
            this.rdSplit2.TabStop = true;
            this.rdSplit2.Text = "拆分";
            this.rdSplit2.UseVisualStyleBackColor = true;
            this.rdGenerlist2.AutoSize = true;
            this.rdGenerlist2.Location = new Point(0x10, 0x15);
            this.rdGenerlist2.Name = "rdGenerlist2";
            this.rdGenerlist2.Size = new Size(0x47, 0x10);
            this.rdGenerlist2.TabIndex = 0;
            this.rdGenerlist2.TabStop = true;
            this.rdGenerlist2.Text = "开具清单";
            this.rdGenerlist2.UseVisualStyleBackColor = true;
            this.ckShow.AutoSize = true;
            this.ckShow.BackColor = Color.Transparent;
            this.ckShow.Location = new Point(0x19, 0xee);
            this.ckShow.Name = "ckShow";
            this.ckShow.Size = new Size(0x9c, 0x10);
            this.ckShow.TabIndex = 5;
            this.ckShow.Text = "在专用发票生成之前显示";
            this.ckShow.UseVisualStyleBackColor = false;
            this.btnGenerate.Location = new Point(0xca, 0xea);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new Size(0x4b, 0x17);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "确认";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new EventHandler(this.btnGenerate_Click);
            this.btnCancel.Location = new Point(0x11b, 0xea);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "放弃";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x17b, 0x10f);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnGenerate);
            base.Controls.Add(this.ckShow);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.Name = "GenerateInvSetForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "单据管理设置";
            base.TopMost = true;
            base.Load += new EventHandler(this.GenerateInvSetForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

