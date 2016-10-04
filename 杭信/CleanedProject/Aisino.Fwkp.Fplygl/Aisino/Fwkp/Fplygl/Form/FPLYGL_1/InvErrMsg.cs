namespace Aisino.Fwkp.Fplygl.Form.FPLYGL_1
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class InvErrMsg : DockForm
    {
        private AisinoBTN btnOK;
        private IContainer components;
        private CustomStyleDataGrid csdgList;
        private AisinoLBL lblMsg;
        protected XmlComponentLoader xmlComponentLoader1;

        public InvErrMsg()
        {
            this.Initialize();
            this.lblMsg.Text = "未成功读入发票卷信息如下：";
            this.lblMsg.Location = new Point((base.Width / 2) - (this.lblMsg.Width / 2), this.lblMsg.Location.Y);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.Close();
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
            this.csdgList.AllowUserToDeleteRows = false;
            this.csdgList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.csdgList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.csdgList.set_GridStyle(1);
            this.csdgList.Columns["CWH"].FillWeight = 20f;
            this.csdgList.Columns["FPDM"].FillWeight = 30f;
            this.csdgList.Columns["QSHM"].FillWeight = 30f;
            this.csdgList.Columns["FPZS"].FillWeight = 20f;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.lblMsg = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblMsg");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOk");
            this.csdgList = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("csdgErrVolumn");
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.gridSetting();
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(InvErrMsg));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x198, 0xef);
            this.xmlComponentLoader1.TabIndex = 1;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.InvInfoErrMsg\Aisino.Fwkp.Fplygl.Forms.InvInfoErrMsg.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x198, 0xef);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "InvErrMsg";
            base.set_TabText("InvErrMsg");
            this.Text = "未读入的发票卷";
            base.ResumeLayout(false);
        }

        public void InsertData(List<NoReadInvStock> allRepeats)
        {
            try
            {
                if (this.csdgList.DataSource != null)
                {
                    ((DataTable) this.csdgList.DataSource).Clear();
                }
                DataTable table = new DataTable();
                table.Columns.Add("CWH", typeof(string));
                table.Columns.Add("FPDM", typeof(string));
                table.Columns.Add("QSHM", typeof(string));
                table.Columns.Add("FPZS", typeof(string));
                foreach (NoReadInvStock stock in allRepeats)
                {
                    DataRow row = table.NewRow();
                    row["CWH"] = stock.ErrNo.ToString();
                    row["FPDM"] = stock.InvCode;
                    row["QSHM"] = ShareMethods.FPHMTo8Wei(stock.InvNo);
                    row["FPZS"] = stock.Count.ToString();
                    table.Rows.Add(row);
                }
                this.csdgList.DataSource = table;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
        }
    }
}

