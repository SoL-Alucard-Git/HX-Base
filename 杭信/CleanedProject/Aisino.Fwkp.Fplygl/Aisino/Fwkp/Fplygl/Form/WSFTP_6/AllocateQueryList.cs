namespace Aisino.Fwkp.Fplygl.Form.WSFTP_6
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class AllocateQueryList : DockForm
    {
        private IContainer components;
        private CustomStyleDataGrid csdgVolumns;
        protected XmlComponentLoader xmlComponentLoader1;

        public AllocateQueryList(List<AllocateInfo> allocList)
        {
            this.Initialize();
            DataTable table = this.CreateTableHeader();
            foreach (AllocateInfo info in allocList)
            {
                DataRow row = table.NewRow();
                row["FKPJH"] = info.machineNum;
                row["FPZL"] = info.typeName;
                row["FPDM"] = info.typeCode;
                row["QSHM"] = info.startNum;
                row["ZZHM"] = info.endNum;
                row["FS"] = info.count;
                row["LGRQ"] = info.buyDate;
                row["FPZT"] = info.allocStatus;
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
            table.Columns.Add("FPZT", typeof(string));
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
            this.csdgVolumns = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("csdgList");
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AllocateQueryList));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x321, 0x153);
            this.xmlComponentLoader1.TabIndex = 6;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.WebWindows.AllocateQueryList\Aisino.Fwkp.Fplygl.Forms.WebWindows.AllocateQueryList.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x321, 0x153);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "AllocateQueryList";
            base.set_TabText("AllocateQueryList");
            this.Text = "领用状态查询";
            base.ResumeLayout(false);
        }
    }
}

