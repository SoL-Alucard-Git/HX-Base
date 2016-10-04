namespace Aisino.Fwkp.Fplygl.Form.WLGP_4
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class DownloadQueryList : DockForm
    {
        private IContainer components;
        private CustomStyleDataGrid csdgVolumns;
        protected XmlComponentLoader xmlComponentLoader1;

        public DownloadQueryList(List<DownloadInfo> downList)
        {
            this.Initialize();
            DataTable table = this.CreateTableHeader();
            foreach (DownloadInfo info in downList)
            {
                DataRow row = table.NewRow();
                row["FKPJH"] = info.machineNum;
                row["FPZL"] = info.typeName;
                row["FPDM"] = info.typeCode;
                row["QSHM"] = info.startNum;
                row["ZZHM"] = info.endNum;
                row["FS"] = info.count;
                row["LGRQ"] = info.buyDate;
                table.Rows.Add(row);
            }
            this.csdgVolumns.DataSource = table;
        }

        private DataTable CreateTableHeader()
        {
            DataTable table = new DataTable();
            table.Columns.Add("FKPJH", typeof(string));
            table.Columns.Add("FPZL", typeof(string));
            table.Columns.Add("FPDM", typeof(string));
            table.Columns.Add("QSHM", typeof(string));
            table.Columns.Add("ZZHM", typeof(string));
            table.Columns.Add("FS", typeof(string));
            table.Columns.Add("LGRQ", typeof(string));
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
            this.csdgVolumns.ReadOnly = true;
            this.csdgVolumns.Columns["FKPJH"].FillWeight = 5f;
            this.csdgVolumns.Columns["FPZL"].FillWeight = 34f;
            this.csdgVolumns.Columns["FPDM"].FillWeight = 17f;
            this.csdgVolumns.Columns["QSHM"].FillWeight = 11f;
            this.csdgVolumns.Columns["ZZHM"].FillWeight = 11f;
            this.csdgVolumns.Columns["FS"].FillWeight = 8f;
            this.csdgVolumns.Columns["LGRQ"].FillWeight = 14f;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.csdgVolumns = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("csdgList");
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DownloadQueryList));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(720, 390);
            this.xmlComponentLoader1.TabIndex = 5;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.DownloadQueryList\Aisino.Fwkp.Fplygl.Forms.DownloadQueryList.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(720, 390);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MinimizeBox = false;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "DownloadQueryList";
            base.set_TabText("DownloadQueryList");
            this.Text = "下载票量查询";
            base.ResumeLayout(false);
        }
    }
}

