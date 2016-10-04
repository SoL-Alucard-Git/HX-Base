namespace Aisino.Fwkp.Fplygl.Form.WLSL_5
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class DetailList : DockForm
    {
        private IContainer components;
        private CustomStyleDataGrid detailList;
        private bool isConfirmed;
        private bool isJS;
        private ILog loger = LogUtil.GetLogger<DetailList>();
        private string type = string.Empty;
        protected XmlComponentLoader xmlComponentLoader1;

        public DetailList(List<Volumn> volumns, bool isConfirmedApply, string typeName, bool isJSAdmin)
        {
            this.Initialize();
            this.isConfirmed = isConfirmedApply;
            this.type = typeName;
            this.isJS = isJSAdmin;
            this.InitVolumns(volumns);
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
            this.detailList.AllowUserToDeleteRows = false;
            this.detailList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.detailList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.detailList.ReadOnly = true;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.detailList = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("csdgList");
            this.gridSetting();
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(DetailList));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1ac, 0x153);
            this.xmlComponentLoader1.TabIndex = 4;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.ApplyStatusDetail\Aisino.Fwkp.Fplygl.Forms.ApplyStatusDetail.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1ac, 0x153);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DetailList";
            base.set_TabText("DetailList");
            this.Text = "申领发票卷信息";
            base.ResumeLayout(false);
        }

        private void InitVolumns(List<Volumn> volumns)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("FPDM", typeof(string));
                table.Columns.Add("QSHM", typeof(string));
                table.Columns.Add("ZZHM", typeof(string));
                table.Columns.Add("FPZS", typeof(string));
                if (this.isConfirmed || this.type.Equals("电子增值税普通发票"))
                {
                    foreach (Volumn volumn in volumns)
                    {
                        DataRow row = table.NewRow();
                        row["FPDM"] = volumn.typeCode;
                        row["QSHM"] = volumn.startNum;
                        row["ZZHM"] = volumn.endNum;
                        row["FPZS"] = volumn.count;
                        table.Rows.Add(row);
                    }
                }
                else
                {
                    using (List<Volumn>.Enumerator enumerator2 = volumns.GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            Volumn current = enumerator2.Current;
                            DataRow row2 = table.NewRow();
                            row2["FPDM"] = new string('*', 12);
                            row2["QSHM"] = new string('*', 8);
                            row2["ZZHM"] = new string('*', 8);
                            row2["FPZS"] = new string('*', 5);
                            table.Rows.Add(row2);
                        }
                    }
                }
                this.detailList.DataSource = table;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }
    }
}

