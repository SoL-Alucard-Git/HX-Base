namespace Aisino.Fwkp.Fplygl.Form.AbsForms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class ApplySuccessParent : DockForm
    {
        private IContainer components;
        protected CustomStyleDataGrid csdgStatusVolumn;
        protected ToolStripButton tool_Close;
        protected ToolStripButton tool_Confirm;
        protected ToolStripButton tool_Revoke;
        protected ToolStrip toolStrip;
        protected List<OneTypeVolumes> volumeList = new List<OneTypeVolumes>();
        protected XmlComponentLoader xmlComponentLoader1;

        public ApplySuccessParent()
        {
            this.Initialize();
        }

        protected void BindData(List<OneTypeVolumes> confirmList)
        {
            DataTable table = new DataTable();
            table.Columns.Add("SLXH", typeof(string));
            table.Columns.Add("YSQBH", typeof(string));
            table.Columns.Add("FPZLDM", typeof(string));
            table.Columns.Add("FPZLMC", typeof(string));
            table.Columns.Add("FPZL", typeof(string));
            table.Columns.Add("CLXX", typeof(string));
            table.Columns.Add("SLSL", typeof(string));
            table.Columns.Add("SQSJ", typeof(string));
            table.Columns.Add("CLSJ", typeof(string));
            foreach (OneTypeVolumes volumes in confirmList)
            {
                DataRow row = table.NewRow();
                row["CLXX"] = volumes.applyStatus + ":" + volumes.applyStatusMsg;
                row["SLXH"] = volumes.applyNum;
                row["FPZLDM"] = volumes.typeCode;
                row["FPZLMC"] = volumes.typeName;
                row["FPZL"] = volumes.invType;
                row["YSQBH"] = volumes.dealNum;
                row["SLSL"] = volumes.applyAmount;
                row["SQSJ"] = volumes.applyTime;
                row["CLSJ"] = volumes.dealTime;
                table.Rows.Add(row);
            }
            this.csdgStatusVolumn.DataSource = table;
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
            this.csdgStatusVolumn.AllowUserToDeleteRows = false;
            this.csdgStatusVolumn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.csdgStatusVolumn.ReadOnly = true;
            int count = this.csdgStatusVolumn.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                this.csdgStatusVolumn.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.csdgStatusVolumn.Columns["CLXX"].Width = 150;
            this.csdgStatusVolumn.Columns["SLXH"].Width = 80;
            this.csdgStatusVolumn.Columns["YSQBH"].Width = 80;
            this.csdgStatusVolumn.Columns["FPZL"].Width = 100;
            this.csdgStatusVolumn.Columns["FPZLDM"].Width = 100;
            this.csdgStatusVolumn.Columns["FPZLMC"].Width = 100;
            this.csdgStatusVolumn.Columns["SLSL"].Width = 80;
            this.csdgStatusVolumn.Columns["SQSJ"].Width = 80;
            this.csdgStatusVolumn.Columns["CLSJ"].Width = 100;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.toolStrip = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip");
            this.tool_Close = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Close");
            this.tool_Confirm = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Confirm");
            this.tool_Revoke = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Revoke");
            this.csdgStatusVolumn = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("csdgStatusVolumn");
            this.tool_Close.Click += new EventHandler(this.tool_Close_Click);
            this.tool_Confirm.Click += new EventHandler(this.tool_Confirm_Click);
            this.tool_Revoke.Click += new EventHandler(this.tool_Revoke_Click);
            this.tool_Confirm.Visible = false;
            this.tool_Revoke.Visible = false;
            this.tool_Close.Margin = new Padding(20, 1, 0, 2);
            ControlStyleUtil.SetToolStripStyle(this.toolStrip);
            this.gridSetting();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ApplySuccessParent));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x310, 0x171);
            this.xmlComponentLoader1.TabIndex = 6;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.WebWindows.ApplySuccessParent\Aisino.Fwkp.Fplygl.Forms.WebWindows.ApplySuccessParent.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x310, 0x171);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "ApplySuccessParent";
            base.set_TabText("ApplySuccessParent");
            this.Text = "申领后续操作父窗体";
            base.ResumeLayout(false);
        }

        protected virtual void ShowListWithFilteredQuery()
        {
        }

        protected void tool_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected virtual void tool_Confirm_Click(object sender, EventArgs e)
        {
        }

        protected virtual void tool_Revoke_Click(object sender, EventArgs e)
        {
        }
    }
}

