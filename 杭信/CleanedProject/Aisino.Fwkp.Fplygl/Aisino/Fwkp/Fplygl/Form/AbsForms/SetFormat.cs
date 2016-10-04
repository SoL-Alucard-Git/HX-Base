namespace Aisino.Fwkp.Fplygl.Form.AbsForms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Factory;
    using Aisino.Fwkp.Fplygl.IBLL;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class SetFormat : DockForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnSetUpdate;
        private AisinoBTN btnSyn;
        private IContainer components;
        private CustomStyleDataGrid csdgVolumns;
        private DataGridViewComboBoxColumn JPGG;
        private readonly ILYGL_JPXX jpxxDal;
        protected XmlComponentLoader xmlComponentLoader1;

        public SetFormat(List<InvVolumeApp> invVols, List<string> formats)
        {
            this.jpxxDal = BLLFactory.CreateInstant<ILYGL_JPXX>("LYGL_JPXX");
            this.InitializeWindow();
            this.Text = "增值税普通发票(卷票)规格修改";
            this.btnSetUpdate.Text = "修改";
            DataTable table = this.InitialTableData(invVols, formats);
            this.csdgVolumns.DataSource = table;
        }

        public SetFormat(List<InvVolumeApp> invVols, string defaultFormat = "NEW76mmX177mm")
        {
            this.jpxxDal = BLLFactory.CreateInstant<ILYGL_JPXX>("LYGL_JPXX");
            this.InitializeWindow();
            this.Text = "增值税普通发票(卷票)规格设置";
            this.btnSetUpdate.Text = "设置";
            this.btnSyn.Visible = false;
            DataTable table = this.CreateTableHeader();
            foreach (InvVolumeApp app in invVols)
            {
                DataRow row = table.NewRow();
                row["LBDM"] = app.TypeCode;
                row["JQSH"] = Convert.ToString(ShareMethods.GetVolumnStartNum(app)).PadLeft(8, '0');
                row["JZZH"] = Convert.ToString(ShareMethods.GetVolumnEndNum(app)).PadLeft(8, '0');
                row["LGZS"] = app.BuyNumber.ToString();
                row["LGRQ"] = app.BuyDate.ToString("yyyy-MM-dd");
                row["JPGG"] = defaultFormat;
                table.Rows.Add(row);
            }
            this.csdgVolumns.DataSource = table;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSetUpdate_Click(object sender, EventArgs e)
        {
            if (this.btnSetUpdate.Text.Equals("设置"))
            {
                this.InsertVolumnFormats();
            }
            else if (this.btnSetUpdate.Text.Equals("修改"))
            {
                this.UpdateVolumnFormats();
            }
            base.Close();
        }

        private void btnSyn_Click(object sender, EventArgs e)
        {
            List<InvVolumeApp> list = ShareMethods.FilterOutSpecType(base.TaxCardInstance.GetInvStock(), 0x29);
            List<InvVolumeApp> list2 = new List<InvVolumeApp>();
            foreach (InvVolumeApp app in list)
            {
                if (this.jpxxDal.GetSpecificFormat(app).Equals(string.Empty))
                {
                    list2.Add(app);
                }
            }
            if (list2.Count > 0)
            {
                foreach (InvVolumeApp app2 in list2)
                {
                    this.jpxxDal.InsertSingleVolumn(app2, "NEW76mmX177mm");
                }
            }
            List<InvVolumeApp> invList = new List<InvVolumeApp>();
            List<string> typeList = new List<string>();
            this.jpxxDal.SelectVolumnList(out invList, out typeList);
            DataTable table = this.InitialTableData(invList, typeList);
            this.csdgVolumns.DataSource = table;
        }

        private DataTable CreateTableHeader()
        {
            DataTable table = new DataTable();
            table.Columns.Add("LBDM", typeof(string));
            table.Columns.Add("JQSH", typeof(string));
            table.Columns.Add("JZZH", typeof(string));
            table.Columns.Add("LGZS", typeof(string));
            table.Columns.Add("LGRQ", typeof(string));
            table.Columns.Add("JPGG", typeof(string));
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

        private void gridSetting()
        {
            this.csdgVolumns.AllowUserToDeleteRows = false;
            this.csdgVolumns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.csdgVolumns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.csdgVolumns.ReadOnly = false;
            this.csdgVolumns.set_GridStyle(1);
            this.csdgVolumns.Columns["LBDM"].FillWeight = 15f;
            this.csdgVolumns.Columns["JQSH"].FillWeight = 13f;
            this.csdgVolumns.Columns["JZZH"].FillWeight = 13f;
            this.csdgVolumns.Columns["LGZS"].FillWeight = 13f;
            this.csdgVolumns.Columns["LGRQ"].FillWeight = 20f;
            this.csdgVolumns.Columns["LBDM"].ReadOnly = true;
            this.csdgVolumns.Columns["JQSH"].ReadOnly = true;
            this.csdgVolumns.Columns["JZZH"].ReadOnly = true;
            this.csdgVolumns.Columns["LGZS"].ReadOnly = true;
            this.csdgVolumns.Columns["LGRQ"].ReadOnly = true;
            this.csdgVolumns.Columns["JQSH"].Visible = false;
            this.csdgVolumns.Columns["LGZS"].Visible = false;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.csdgVolumns = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("csdgList");
            this.btnSetUpdate = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_SetUpdate");
            this.btnSyn = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_Syn");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_Cancel");
            this.btnSetUpdate.Click += new EventHandler(this.btnSetUpdate_Click);
            this.btnSyn.Click += new EventHandler(this.btnSyn_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.gridSetting();
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(SetFormat));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(600, 350);
            this.xmlComponentLoader1.TabIndex = 2;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.SetFormat\Aisino.Fwkp.Fplygl.Forms.SetFormat.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(600, 350);
            base.Controls.Add(this.xmlComponentLoader1);
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Name = "SetFormat";
            base.set_TabText("SetFormat");
            this.Text = "增值税普通发票(卷票)规格设置";
            base.ResumeLayout(false);
        }

        private void InitializeFormatColumn()
        {
            this.JPGG = new DataGridViewComboBoxColumn();
            this.JPGG.HeaderText = "卷票规格";
            this.JPGG.Name = "JPGG";
            this.JPGG.DataPropertyName = "JPGG";
            this.JPGG.FillWeight = 26f;
            string key = string.Empty;
            string str2 = string.Empty;
            DataTable formatTable = ShareMethods.GetFormatTable(out key, out str2);
            this.JPGG.DisplayMember = key;
            this.JPGG.ValueMember = str2;
            this.JPGG.DataSource = formatTable;
            this.JPGG.ReadOnly = false;
        }

        private void InitializeWindow()
        {
            this.Initialize();
            this.InitializeFormatColumn();
            this.csdgVolumns.ColumnAdd(this.JPGG);
        }

        private DataTable InitialTableData(List<InvVolumeApp> invVols, List<string> formats)
        {
            DataTable table = this.CreateTableHeader();
            for (int i = 0; i < invVols.Count; i++)
            {
                DataRow row = table.NewRow();
                row["LBDM"] = invVols[i].TypeCode;
                row["JQSH"] = Convert.ToString(ShareMethods.GetVolumnStartNum(invVols[i])).PadLeft(8, '0');
                row["JZZH"] = Convert.ToString(ShareMethods.GetVolumnEndNum(invVols[i])).PadLeft(8, '0');
                row["LGZS"] = invVols[i].BuyNumber.ToString();
                row["LGRQ"] = invVols[i].BuyDate.ToString("yyyy-MM-dd");
                row["JPGG"] = formats[i];
                table.Rows.Add(row);
            }
            return table;
        }

        private bool InsertVolumnFormats()
        {
            Dictionary<InvVolumeApp, string> volumnFormat = new Dictionary<InvVolumeApp, string>();
            foreach (DataGridViewRow row in (IEnumerable) this.csdgVolumns.Rows)
            {
                InvVolumeApp key = new InvVolumeApp {
                    TypeCode = row.Cells["LBDM"].Value.ToString(),
                    HeadCode = Convert.ToUInt32(row.Cells["JQSH"].Value),
                    Number = Convert.ToUInt16(row.Cells["LGZS"].Value),
                    BuyNumber = Convert.ToUInt16(row.Cells["LGZS"].Value),
                    BuyDate = Convert.ToDateTime(row.Cells["LGRQ"].Value)
                };
                volumnFormat.Add(key, row.Cells["JPGG"].Value.ToString());
            }
            return this.jpxxDal.InsertVolumnList(volumnFormat);
        }

        private bool UpdateVolumnFormats()
        {
            Dictionary<InvVolumeApp, string> volumnFormat = new Dictionary<InvVolumeApp, string>();
            foreach (DataGridViewRow row in (IEnumerable) this.csdgVolumns.Rows)
            {
                InvVolumeApp key = new InvVolumeApp {
                    TypeCode = row.Cells["LBDM"].Value.ToString(),
                    HeadCode = Convert.ToUInt32(row.Cells["JQSH"].Value),
                    Number = Convert.ToUInt16(row.Cells["LGZS"].Value),
                    BuyNumber = Convert.ToUInt16(row.Cells["LGZS"].Value)
                };
                volumnFormat.Add(key, row.Cells["JPGG"].Value.ToString());
            }
            return this.jpxxDal.UpdateVolumnList(volumnFormat);
        }
    }
}

