namespace Aisino.Fwkp.Fplygl.Form.AbsForms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class InvInfoMsg : DockForm
    {
        protected Dictionary<string, object> _dictFPLBBM;
        protected List<InvVolumeApp> _ListModel;
        protected AisinoBTN btnCancel;
        protected AisinoBTN btnExecute;
        protected AisinoBTN btnOK;
        private IContainer components;
        protected CustomStyleDataGrid dgInvInfo;
        protected DataGridViewTextBoxColumn FPZL;
        protected DataGridViewTextBoxColumn FPZS;
        protected DataGridViewTextBoxColumn KPXE;
        protected DataGridViewTextBoxColumn LBDM;
        protected AisinoLBL lblMsg;
        protected DataGridViewTextBoxColumn LGRQ;
        protected DataGridViewTextBoxColumn MC;
        protected DataGridViewTextBoxColumn QSHM;
        protected string[] strHead = new string[] { "FPZL", "KPXE", "LBDM", "MC", "QSHM", "FPZS", "LGRQ" };
        protected XmlComponentLoader xmlComponentLoader1;

        public InvInfoMsg()
        {
            this.Initialize();
            this.InsertGridColumn();
            this._dictFPLBBM = ShareMethods.GetFPLBBM();
            this.ValidationNTxt();
        }

        protected virtual void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.No;
            base.Close();
        }

        protected virtual void btnExecute_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Yes;
            base.Close();
        }

        protected virtual void btnOK_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private DataTable CreateTableHeader()
        {
            DataTable table = new DataTable();
            int num = 0;
            table.Columns.Add(this.strHead[num++], typeof(string));
            table.Columns.Add(this.strHead[num++], typeof(string));
            table.Columns.Add(this.strHead[num++], typeof(string));
            table.Columns.Add(this.strHead[num++], typeof(string));
            table.Columns.Add(this.strHead[num++], typeof(string));
            table.Columns.Add(this.strHead[num++], typeof(string));
            table.Columns.Add(this.strHead[num++], typeof(string));
            return table;
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
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOk");
            this.btnExecute = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnExecute");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.lblMsg = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblMsg");
            this.dgInvInfo = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1");
            this.dgInvInfo.AllowUserToDeleteRows = false;
            this.dgInvInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgInvInfo.set_GridStyle(1);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnExecute.Click += new EventHandler(this.btnExecute_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(InvInfoMsg));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(800, 0xef);
            this.xmlComponentLoader1.TabIndex = 2;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.InvInfoMsg\Aisino.Fwkp.Fplygl.Forms.InvInfoMsg.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(800, 0xef);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "InvInfoMsg";
            base.set_TabText("InvInfoMsg");
            this.Text = "发票信息";
            base.ResumeLayout(false);
        }

        protected virtual void InsertGridColumn()
        {
            this.dgInvInfo.Rows.Clear();
            this.FPZL = new DataGridViewTextBoxColumn();
            this.KPXE = new DataGridViewTextBoxColumn();
            this.LBDM = new DataGridViewTextBoxColumn();
            this.MC = new DataGridViewTextBoxColumn();
            this.QSHM = new DataGridViewTextBoxColumn();
            this.FPZS = new DataGridViewTextBoxColumn();
            this.LGRQ = new DataGridViewTextBoxColumn();
            int index = 0;
            this.FPZL.HeaderText = "发票种类";
            this.FPZL.Name = this.strHead[index];
            this.FPZL.DataPropertyName = this.strHead[index++];
            this.FPZL.Visible = true;
            this.FPZL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.FPZL.Width = 160;
            this.KPXE.HeaderText = "开票限额";
            this.KPXE.Name = this.strHead[index];
            this.KPXE.DataPropertyName = this.strHead[index++];
            this.KPXE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.KPXE.Width = 90;
            this.LBDM.HeaderText = "类别代码";
            this.LBDM.Name = this.strHead[index];
            this.LBDM.DataPropertyName = this.strHead[index++];
            this.LBDM.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.LBDM.Width = 90;
            this.MC.HeaderText = "类别名称";
            this.MC.Name = this.strHead[index];
            this.MC.DataPropertyName = this.strHead[index++];
            this.MC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.MC.Width = 180;
            this.QSHM.HeaderText = "起始号码";
            this.QSHM.Name = this.strHead[index];
            this.QSHM.DataPropertyName = this.strHead[index++];
            this.QSHM.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.QSHM.Width = 70;
            this.FPZS.HeaderText = "发票张数";
            this.FPZS.Name = this.strHead[index];
            this.FPZS.DataPropertyName = this.strHead[index++];
            this.FPZS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.FPZS.Width = 70;
            this.LGRQ.HeaderText = "领购日期";
            this.LGRQ.Name = this.strHead[index];
            this.LGRQ.DataPropertyName = this.strHead[index++];
            this.LGRQ.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.LGRQ.Width = 80;
            this.FPZL.FillWeight = 22f;
            this.KPXE.FillWeight = 12f;
            this.LBDM.FillWeight = 12f;
            this.MC.FillWeight = 25f;
            this.QSHM.FillWeight = 10f;
            this.FPZS.FillWeight = 9f;
            this.LGRQ.FillWeight = 10f;
            int num2 = 0;
            this.dgInvInfo.ColumnAdd(this.FPZL);
            this.dgInvInfo.SetColumnReadOnly(num2++, true);
            this.dgInvInfo.ColumnAdd(this.KPXE);
            this.dgInvInfo.SetColumnReadOnly(num2++, true);
            this.dgInvInfo.ColumnAdd(this.LBDM);
            this.dgInvInfo.SetColumnReadOnly(num2++, true);
            this.dgInvInfo.ColumnAdd(this.MC);
            this.dgInvInfo.SetColumnReadOnly(num2++, true);
            this.dgInvInfo.ColumnAdd(this.QSHM);
            this.dgInvInfo.SetColumnReadOnly(num2++, true);
            this.dgInvInfo.ColumnAdd(this.FPZS);
            this.dgInvInfo.SetColumnReadOnly(num2++, true);
            this.dgInvInfo.ColumnAdd(this.LGRQ);
            this.dgInvInfo.SetColumnReadOnly(num2++, true);
            this.dgInvInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgInvInfo.MultiSelect = false;
            this.dgInvInfo.ReadOnly = true;
            this.dgInvInfo.AllowUserToAddRows = false;
        }

        public void InsertInvVolume(List<InvVolumeApp> invList)
        {
            InvSQInfo invUpLimit = base.TaxCardInstance.get_SQInfo();
            int num = 0;
            DataTable table = this.CreateTableHeader();
            foreach (InvVolumeApp app in invList)
            {
                num = 0;
                DataRow row = table.NewRow();
                string invType = ShareMethods.GetInvType(app.InvType);
                row[this.strHead[num++]] = invType;
                double upLimit = ShareMethods.GetUpLimit(invUpLimit, invType);
                row[this.strHead[num++]] = Convert.ToString(upLimit);
                row[this.strHead[num++]] = app.TypeCode;
                row[this.strHead[num++]] = ShareMethods.GetFPLBMC(app, this._dictFPLBBM);
                row[this.strHead[num++]] = ShareMethods.FPHMTo8Wei(app.HeadCode);
                int number = app.Number;
                row[this.strHead[num++]] = Convert.ToString(number);
                row[this.strHead[num++]] = app.BuyDate.ToString("yyyy-MM-dd");
                table.Rows.Add(row);
            }
            this.dgInvInfo.DataSource = table;
        }

        public void InsertInvVolume(DataGridViewRow row)
        {
            DataTable table = this.CreateTableHeader();
            DataRow row2 = table.NewRow();
            row2["FPZL"] = row.Cells["FPZL"].Value.ToString().Trim();
            row2["KPXE"] = row.Cells["KPXE"].Value.ToString().Trim();
            row2["LBDM"] = row.Cells["LBDM"].Value.ToString().Trim();
            row2["MC"] = row.Cells["MC"].Value.ToString().Trim();
            row2["QSHM"] = row.Cells["QSHM"].Value.ToString().Trim();
            row2["FPZS"] = row.Cells["SYZS"].Value.ToString().Trim();
            row2["LGRQ"] = row.Cells["LGRQ"].Value.ToString().Trim();
            table.Rows.Add(row2);
            this.dgInvInfo.DataSource = table;
        }

        public void InsertInvVolume(DataGridViewRowCollection rowArr)
        {
            foreach (DataGridViewRow row in (IEnumerable) rowArr)
            {
                this.InsertInvVolume(row);
            }
        }

        protected virtual void ValidationNTxt()
        {
        }
    }
}

