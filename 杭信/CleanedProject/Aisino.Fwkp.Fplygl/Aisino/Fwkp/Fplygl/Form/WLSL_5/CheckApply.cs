namespace Aisino.Fwkp.Fplygl.Form.WLSL_5
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class CheckApply : DockForm
    {
        private List<OneTypeVolumes> applyQueryList = new List<OneTypeVolumes>();
        private AisinoBTN btnQuery;
        private AisinoCHK chkJZ;
        private AisinoCHK chkQS;
        private AisinoCMB cmbInvType;
        private AisinoCMB cmbStatus;
        private IContainer components;
        private CustomStyleDataGrid csdgStatusVolumn;
        private DateTimePicker data_jsrq;
        private DateTimePicker data_ksrq;
        private bool isJS;
        private ILog loger = LogUtil.GetLogger<CheckApply>();
        private AisinoTXT txtApplyNo;
        protected XmlComponentLoader xmlComponentLoader1;

        public CheckApply(bool isJSAdmin)
        {
            this.isJS = isJSAdmin;
            this.Initialize();
            this.InitializeTypeCMB();
            this.InitializeStatusCMB();
            this.SetDataCtrlAttritute();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.chkQS.Checked && this.chkJZ.Checked) && (this.data_ksrq.Value > this.data_jsrq.Value))
                {
                    MessageManager.ShowMsgBox("INP-441203");
                }
                else
                {
                    QueryCondition qCondition = new QueryCondition();
                    if (this.chkQS.Checked)
                    {
                        qCondition.startTime = this.data_ksrq.Value.ToString("yyyyMMdd");
                    }
                    else
                    {
                        qCondition.startTime = string.Empty;
                    }
                    if (this.chkJZ.Checked)
                    {
                        qCondition.endTime = this.data_jsrq.Value.ToString("yyyyMMdd");
                    }
                    else
                    {
                        qCondition.endTime = string.Empty;
                    }
                    qCondition.invType = this.cmbInvType.SelectedValue.ToString();
                    qCondition.status = this.cmbStatus.SelectedValue.ToString();
                    this.applyQueryList.Clear();
                    QueryConfirmCommon.QueryController(qCondition, this.applyQueryList, false);
                    DataTable table = new DataTable();
                    table.Columns.Add("CLXX", typeof(string));
                    table.Columns.Add("SLXH", typeof(string));
                    table.Columns.Add("FPZLDM", typeof(string));
                    table.Columns.Add("FPZLMC", typeof(string));
                    table.Columns.Add("FPZL", typeof(string));
                    table.Columns.Add("YSQBH", typeof(string));
                    table.Columns.Add("SLSL", typeof(string));
                    table.Columns.Add("SQSJ", typeof(string));
                    table.Columns.Add("CLSJ", typeof(string));
                    table.Columns.Add("FPJMX", typeof(string));
                    foreach (OneTypeVolumes volumes in this.applyQueryList)
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
                        row["FPJMX"] = "明细";
                        table.Rows.Add(row);
                    }
                    this.csdgStatusVolumn.DataSource = table;
                }
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

        private void chkJZ_CheckedChanged(object sender, EventArgs e)
        {
            this.data_jsrq.Enabled = this.chkJZ.Checked;
        }

        private void chkQS_CheckedChanged(object sender, EventArgs e)
        {
            this.data_ksrq.Enabled = this.chkQS.Checked;
        }

        private void csdgStatusVolumn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.csdgStatusVolumn.Columns["FPJMX"].Index)
                {
                    int rowIndex = e.RowIndex;
                    new DetailList(this.applyQueryList[rowIndex].volumns, this.applyQueryList[rowIndex].isConfirmed, this.applyQueryList[rowIndex].invType, this.isJS).ShowDialog();
                }
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
            this.csdgStatusVolumn.Columns["SLXH"].Visible = false;
            this.csdgStatusVolumn.Columns["YSQBH"].Visible = false;
            this.csdgStatusVolumn.Columns["CLXX"].Width = 150;
            this.csdgStatusVolumn.Columns["SLXH"].Width = 80;
            this.csdgStatusVolumn.Columns["YSQBH"].Width = 80;
            this.csdgStatusVolumn.Columns["FPZL"].Width = 100;
            this.csdgStatusVolumn.Columns["FPZLDM"].Width = 100;
            this.csdgStatusVolumn.Columns["FPZLMC"].Width = 100;
            this.csdgStatusVolumn.Columns["SLSL"].Width = 80;
            this.csdgStatusVolumn.Columns["SQSJ"].Width = 80;
            this.csdgStatusVolumn.Columns["CLSJ"].Width = 100;
            this.csdgStatusVolumn.Columns["FPJMX"].Width = 100;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.data_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_jsrq");
            this.data_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ksrq");
            this.chkQS = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkQS");
            this.chkJZ = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkJZ");
            this.btnQuery = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_cx");
            this.cmbInvType = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmb_fpzl");
            this.cmbStatus = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmb_Slzt");
            this.txtApplyNo = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_slxh");
            this.csdgStatusVolumn = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("csdgStatusVolumn");
            this.cmbInvType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.gridSetting();
            this.chkQS.CheckedChanged += new EventHandler(this.chkQS_CheckedChanged);
            this.chkJZ.CheckedChanged += new EventHandler(this.chkJZ_CheckedChanged);
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.csdgStatusVolumn.CellContentClick += new DataGridViewCellEventHandler(this.csdgStatusVolumn_CellContentClick);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(CheckApply));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x32b, 0x1fd);
            this.xmlComponentLoader1.TabIndex = 4;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.ApplyStatusList\Aisino.Fwkp.Fplygl.Forms.ApplyStatusList.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x32b, 0x1fd);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MinimizeBox = false;
            base.Name = "ApplyStatusList";
            base.set_TabText("ApplyStatusList");
            this.Text = "申领状态查询";
            base.ResumeLayout(false);
        }

        private void InitializeStatusCMB()
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("key");
                table.Columns.Add("value");
                DataRow row = table.NewRow();
                row["key"] = "全部";
                row["value"] = "";
                table.Rows.Add(row);
                row = table.NewRow();
                row["key"] = "成功";
                row["value"] = "1";
                table.Rows.Add(row);
                row = table.NewRow();
                row["key"] = "失败";
                row["value"] = "2";
                table.Rows.Add(row);
                row = table.NewRow();
                row["key"] = "申领中";
                row["value"] = "0";
                table.Rows.Add(row);
                this.cmbStatus.ValueMember = "value";
                this.cmbStatus.DisplayMember = "key";
                this.cmbStatus.DataSource = table;
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

        private void InitializeTypeCMB()
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("key");
                table.Columns.Add("value");
                DataRow row = table.NewRow();
                row["key"] = "全部";
                row["value"] = "";
                table.Rows.Add(row);
                if (base.TaxCardInstance.get_QYLX().ISZYFP)
                {
                    row = table.NewRow();
                    row["key"] = "增值税专用发票";
                    row["value"] = "0";
                    table.Rows.Add(row);
                }
                if (base.TaxCardInstance.get_QYLX().ISPTFP)
                {
                    row = table.NewRow();
                    row["key"] = "增值税普通发票";
                    row["value"] = "2";
                    table.Rows.Add(row);
                }
                if (base.TaxCardInstance.get_QYLX().ISHY)
                {
                    row = table.NewRow();
                    row["key"] = "货物运输业增值税专用发票";
                    row["value"] = "009";
                    table.Rows.Add(row);
                }
                if (base.TaxCardInstance.get_QYLX().ISJDC)
                {
                    row = table.NewRow();
                    row["key"] = "机动车销售统一发票";
                    row["value"] = "005";
                    table.Rows.Add(row);
                }
                if (base.TaxCardInstance.get_QYLX().ISPTFPJSP)
                {
                    row = table.NewRow();
                    row["key"] = "增值税普通发票(卷票)";
                    row["value"] = "025";
                    table.Rows.Add(row);
                }
                if (base.TaxCardInstance.get_QYLX().ISPTFPDZ)
                {
                    row = table.NewRow();
                    row["key"] = "电子增值税普通发票";
                    row["value"] = "026";
                    table.Rows.Add(row);
                }
                this.cmbInvType.ValueMember = "value";
                this.cmbInvType.DisplayMember = "key";
                this.cmbInvType.DataSource = table;
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

        private void SetDataCtrlAttritute()
        {
            try
            {
                int month = base.TaxCardInstance.GetCardClock().Month;
                int year = base.TaxCardInstance.GetCardClock().Year;
                if (year < 0x6d9)
                {
                    year = DateTime.Now.Year;
                }
                DateTime.DaysInMonth(year, month);
                this.data_ksrq.Value = new DateTime(year, month, 1);
                int num3 = base.TaxCardInstance.GetCardClock().Year;
                if (num3 < 0x6d9)
                {
                    num3 = DateTime.Now.Year;
                }
                int day = DateTime.DaysInMonth(num3, month);
                this.data_jsrq.Value = new DateTime(year, month, day);
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

