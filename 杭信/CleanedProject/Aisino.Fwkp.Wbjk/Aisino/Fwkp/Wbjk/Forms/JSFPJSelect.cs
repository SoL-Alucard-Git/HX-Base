namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class JSFPJSelect : DockForm
    {
        protected List<InvVolumeApp> _ListModel = null;
        private IContainer components = null;
        public static string[] fpdmhm = new string[] { "", "" };
        private CustomStyleDataGrid FPJ;
        public AisinoLBL FPZL;
        private AisinoTXT FPZS;
        private AisinoTXT LBDM;
        private FPLX mfplx;
        public AisinoLBL QSHM;
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private ToolStripButton tool_exit;
        private ToolStripButton tool_select;
        private ToolStrip toolStrip1;
        private AisinoTXT XH;
        private XmlComponentLoader xmlComponentLoader1;

        public JSFPJSelect(FPLX fplx)
        {
            this.Initialize();
            this.mfplx = fplx;
            this.FPJ.CellDoubleClick += new DataGridViewCellEventHandler(this.FPJ_CellDoubleClick);
            base.Load += new EventHandler(this.JSFPJSelect_Load);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillGridView()
        {
            this._ListModel = base.TaxCardInstance.GetInvStock();
            if (base.TaxCardInstance.get_RetCode() > 0)
            {
                MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
            }
            else
            {
                int num = 0;
                if (this._ListModel != null)
                {
                    for (int i = 0; i < this._ListModel.Count; i++)
                    {
                        string[] strArray;
                        if ((this.mfplx == 0x29) && (this._ListModel[i].InvType == 0x29))
                        {
                            num++;
                            strArray = new string[] { num.ToString(), "增值税普通发票(卷票)", this._ListModel[i].TypeCode, FPHMTo8Wei(this._ListModel[i].HeadCode), Convert.ToString(this._ListModel[i].Number) };
                            this.FPJ.Rows.Add(strArray);
                        }
                        if ((this.mfplx == 2) && (this._ListModel[i].InvType == 2))
                        {
                            num++;
                            strArray = new string[] { num.ToString(), "增值税普通发票", this._ListModel[i].TypeCode, FPHMTo8Wei(this._ListModel[i].HeadCode), Convert.ToString(this._ListModel[i].Number) };
                            this.FPJ.Rows.Add(strArray);
                        }
                    }
                    if (num == 0)
                    {
                        MessageManager.ShowMsgBox("金税设备已无可用的发票");
                        base.Close();
                    }
                }
                else
                {
                    MessageManager.ShowMsgBox("金税设备已无可用的发票");
                }
            }
        }

        public static string FPHMTo8Wei(uint iValue)
        {
            string format = "{0:00000000}";
            if ((iValue > 0x5f5e0ff) || (iValue < 0))
            {
                iValue = 0;
            }
            return string.Format(format, iValue);
        }

        private void FPJ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.SelectFPJRow();
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.FPZL = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("FPZL");
            this.LBDM = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("LBDM");
            this.QSHM = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("QSHM");
            this.FPZS = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("FPZS");
            this.XH = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("XH");
            this.FPJ = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("FPJ");
            this.tool_exit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_exit");
            this.tool_select = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_select");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.FPJ.MultiSelect = false;
            this.FPJ.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.FPJ.RowHeadersWidth = 30;
            this.FPJ.ReadOnly = true;
            this.tool_exit.Click += new EventHandler(this.tool_exit_Click);
            this.tool_select.Click += new EventHandler(this.tool_select_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(690, 0x1e1);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.JSFPJSelect\Aisino.Fwkp.Fpkj.Form.JSFPJSelect.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(690, 0x1e1);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "JSFPJSelect";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "发票卷选择";
            base.ResumeLayout(false);
        }

        private void JSFPJSelect_Load(object sender, EventArgs e)
        {
            this.FillGridView();
        }

        private void SelectFPJRow()
        {
            if (this.FPJ.SelectedRows.Count > 0)
            {
                DataGridViewRow row = this.FPJ.SelectedRows[0];
                fpdmhm[0] = row.Cells["LBDM"].Value.ToString();
                fpdmhm[1] = row.Cells["QSHM"].Value.ToString();
                TaxCard card = TaxCardFactory.CreateTaxCard();
                string str = string.Empty;
                if (this.mfplx == 0x29)
                {
                    str = card.SetInvVols(0x29, fpdmhm[0], fpdmhm[1]);
                }
                else if (this.mfplx == 2)
                {
                    str = card.SetInvVols(2, fpdmhm[0], fpdmhm[1]);
                }
                if (ToolUtil.GetReturnErrCode(str) != 0)
                {
                    MessageManager.ShowMsgBox(str);
                }
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageManager.ShowMsgBox("至少选择一卷发票");
            }
        }

        private void tool_exit_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void tool_select_Click(object sender, EventArgs e)
        {
            this.SelectFPJRow();
        }
    }
}

