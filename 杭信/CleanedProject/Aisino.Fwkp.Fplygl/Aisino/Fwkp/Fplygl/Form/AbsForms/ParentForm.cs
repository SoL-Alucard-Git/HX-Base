namespace Aisino.Fwkp.Fplygl.Form.AbsForms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class ParentForm : DockForm
    {
        protected bool _bError;
        protected Dictionary<string, object> _dictFPLBBM;
        protected List<InvVolumeApp> _ListModel;
        private IContainer components;
        protected CustomStyleDataGrid customStyleDataGrid1;
        protected DataGridViewTextBoxColumn FPZL;
        protected DataGridViewTextBoxColumn JH;
        protected DataGridViewTextBoxColumn JZZH;
        protected DataGridViewTextBoxColumn KPXE;
        protected DataGridViewTextBoxColumn LBDM;
        protected AisinoLBL lblAttentionDetail;
        protected AisinoLBL lblAttentionHeader;
        protected DataGridViewTextBoxColumn LGRQ;
        protected DataGridViewTextBoxColumn LGZS;
        private ILog loger = LogUtil.GetLogger<ParentForm>();
        protected DataGridViewTextBoxColumn MC;
        protected AisinoPNL pnlAttention;
        protected AisinoPNL pnlManipulation;
        protected DataGridViewTextBoxColumn QSHM;
        protected bool queryInitialized;
        public List<object> SelectedItems = new List<object>();
        protected string strDaYinTitle = "金税设备库存发票";
        private string[] strHead = new string[] { "FPZL", "KPXE", "JH", "LBDM", "MC", "QSHM", "SYZS", "JZZH", "LGRQ", "LGZS" };
        protected DataGridViewTextBoxColumn SYZS;
        protected ToolStripButton tool_Close;
        protected ToolStripButton tool_Find;
        protected ToolStripButton tool_FPTuiHui;
        protected ToolStripButton tool_GeShi;
        protected ToolStripButton tool_Print;
        protected ToolStripButton tool_TongJi;
        protected ToolStripButton tool_XuanZe;
        protected ToolStripComboBox toolCmbFpzl;
        protected ToolStripComboBox toolCmbMonth;
        protected ToolStripComboBox toolCmbYear;
        protected ToolStripLabel toolLblFpzl;
        protected ToolStripLabel toolLblMonth;
        protected ToolStripLabel toolLblRetrieve;
        protected ToolStripLabel toolLblYear;
        protected ToolStrip toolStrip;
        protected ToolStripSeparator toolStripSeparator1;
        protected ToolStripTextBox toolTxtRetrieve;
        protected XmlComponentLoader xmlComponentLoader1;

        public ParentForm()
        {
            try
            {
                this.Initialize();
                base.Hide();
            }
            catch (BaseException exception)
            {
                this._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected virtual bool CheckEmpty(List<InvVolumeApp> ListModel)
        {
            if ((ListModel != null) && (ListModel.Count > 0))
            {
                return true;
            }
            MessageManager.ShowMsgBox("INP-441201");
            this._bError = true;
            return false;
        }

        protected virtual DataTable CreateTableHeader()
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

        protected virtual void FlushGridData()
        {
            try
            {
                this._ListModel = base.TaxCardInstance.GetInvStock();
                if (0 < base.TaxCardInstance.get_RetCode())
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                    this._bError = true;
                    base.Close();
                }
                else
                {
                    this.InsertData(this._ListModel);
                }
            }
            catch (BaseException exception)
            {
                this._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public virtual void FormAction()
        {
            try
            {
                this.FlushGridData();
                this.queryInitialized = true;
            }
            catch (BaseException exception)
            {
                this._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected virtual string GetInvUpLimit(string strFpzl)
        {
            double minValue = double.MinValue;
            string str2 = strFpzl;
            if (str2 != null)
            {
                if (!(str2 == "增值税专用发票"))
                {
                    if (str2 == "增值税普通发票")
                    {
                        if (!base.TaxCardInstance.get_QYLX().ISPTFP)
                        {
                            return new string('-', 12);
                        }
                    }
                    else if (str2 == "机动车销售统一发票")
                    {
                        if (!base.TaxCardInstance.get_QYLX().ISJDC)
                        {
                            return new string('-', 12);
                        }
                    }
                    else if (str2 == "货物运输业增值税专用发票")
                    {
                        if (!base.TaxCardInstance.get_QYLX().ISHY)
                        {
                            return new string('-', 12);
                        }
                    }
                    else if (str2 == "电子增值税普通发票")
                    {
                        if (!base.TaxCardInstance.get_QYLX().ISPTFPDZ)
                        {
                            return new string('-', 12);
                        }
                    }
                    else if ((str2 == "增值税普通发票(卷票)") && !base.TaxCardInstance.get_QYLX().ISPTFPJSP)
                    {
                        return new string('-', 12);
                    }
                }
                else if (!base.TaxCardInstance.get_QYLX().ISZYFP)
                {
                    return new string('-', 12);
                }
            }
            minValue = ShareMethods.GetUpLimit(base.TaxCardInstance.get_SQInfo(), strFpzl);
            return string.Format("{0:0.00}", minValue);
        }

        private void Initialize()
        {
            try
            {
                this.InitializeComponent();
                this.SetXmlCtrl();
                base.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (BaseException exception)
            {
                this._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x3fa, 0x1f6);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.ParentForm\Aisino.Fwkp.Fplygl.Forms.ParentForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3fa, 0x1f6);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "Parent";
            base.set_TabText("父窗口用于继承");
            this.Text = "父窗口用于继承";
            base.ResumeLayout(false);
        }

        protected virtual void InitializeQueryComponents(int yearExist)
        {
            this.InitializeQueryInvType();
            this.InitializeQueryYear(yearExist);
            this.InitializeQueryMonth();
        }

        protected virtual void InitializeQueryInvType()
        {
            this.toolCmbFpzl.Items.Clear();
            this.toolCmbFpzl.Items.Add("全部");
            this.toolCmbFpzl.Items.Add("增值税专用发票");
            this.toolCmbFpzl.Items.Add("增值税普通发票");
            this.toolCmbFpzl.Items.Add("货物运输业增值税专用发票");
            this.toolCmbFpzl.Items.Add("机动车销售统一发票");
            this.toolCmbFpzl.Items.Add("电子增值税普通发票");
            this.toolCmbFpzl.Items.Add("增值税普通发票(卷票)");
            this.toolCmbFpzl.SelectedIndex = 0;
        }

        protected virtual void InitializeQueryMonth()
        {
            this.toolCmbMonth.Items.Clear();
            this.toolCmbMonth.Items.Add("全部");
            for (int i = 1; i <= 12; i++)
            {
                this.toolCmbMonth.Items.Add(i.ToString() + "月");
            }
            this.toolCmbMonth.SelectedIndex = 0;
        }

        protected virtual void InitializeQueryYear(int yearExist)
        {
            int year = base.TaxCardInstance.GetCardClock().Year;
            this.toolCmbYear.Items.Clear();
            this.toolCmbYear.Items.Add("全部");
            for (int i = year; i >= yearExist; i--)
            {
                this.toolCmbYear.Items.Add(i.ToString() + "年");
            }
            this.toolCmbYear.SelectedIndex = 0;
        }

        protected virtual void InsertData(List<InvVolumeApp> ListModel)
        {
            try
            {
                if (this.customStyleDataGrid1.DataSource != null)
                {
                    ((DataTable) this.customStyleDataGrid1.DataSource).Clear();
                }
                if (this.CheckEmpty(ListModel))
                {
                    DataTable table = this.CreateTableHeader();
                    int num = 0;
                    int yearExist = 0x7fffffff;
                    foreach (InvVolumeApp app in ListModel)
                    {
                        int number = app.Number;
                        if (('0' != app.Status) && (number > 0))
                        {
                            DataRow row = table.NewRow();
                            string invType = ShareMethods.GetInvType(app.InvType);
                            row["FPZL"] = invType;
                            row["KPXE"] = this.GetInvUpLimit(invType);
                            row["JH"] = Convert.ToString(num++);
                            row["LBDM"] = app.TypeCode;
                            row["MC"] = ShareMethods.GetFPLBMC(app, this._dictFPLBBM);
                            row["QSHM"] = ShareMethods.FPHMTo8Wei(app.HeadCode);
                            row["SYZS"] = Convert.ToString(number);
                            uint num4 = (app.HeadCode + app.Number) - 1;
                            row["JZZH"] = num4.ToString().PadLeft(8, '0');
                            row["LGRQ"] = app.BuyDate.ToString("yyyy-MM-dd");
                            row["LGZS"] = app.BuyNumber.ToString();
                            if (app.BuyDate.Year < yearExist)
                            {
                                yearExist = app.BuyDate.Year;
                            }
                            table.Rows.Add(row);
                        }
                    }
                    this.customStyleDataGrid1.DataSource = table;
                    this.InitializeQueryComponents(yearExist);
                }
            }
            catch (BaseException exception)
            {
                this._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected virtual void InsertGridColumn()
        {
            try
            {
                this.customStyleDataGrid1.Rows.Clear();
                this.FPZL = new DataGridViewTextBoxColumn();
                this.KPXE = new DataGridViewTextBoxColumn();
                this.JH = new DataGridViewTextBoxColumn();
                this.LBDM = new DataGridViewTextBoxColumn();
                this.MC = new DataGridViewTextBoxColumn();
                this.QSHM = new DataGridViewTextBoxColumn();
                this.SYZS = new DataGridViewTextBoxColumn();
                this.JZZH = new DataGridViewTextBoxColumn();
                this.LGRQ = new DataGridViewTextBoxColumn();
                this.LGZS = new DataGridViewTextBoxColumn();
                this.LGZS.Visible = false;
                int index = 0;
                this.FPZL.HeaderText = "发票种类";
                this.FPZL.Name = this.strHead[index];
                this.FPZL.DataPropertyName = this.strHead[index++];
                this.FPZL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.FPZL.Width = 210;
                this.KPXE.HeaderText = "开票限额";
                this.KPXE.Name = this.strHead[index];
                this.KPXE.DataPropertyName = this.strHead[index++];
                this.KPXE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.KPXE.Width = 120;
                this.JH.HeaderText = "卷号";
                this.JH.Name = this.strHead[index];
                this.JH.DataPropertyName = this.strHead[index++];
                this.JH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.JH.Width = 70;
                this.LBDM.HeaderText = "类别代码";
                this.LBDM.Name = this.strHead[index];
                this.LBDM.DataPropertyName = this.strHead[index++];
                this.LBDM.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.LBDM.Width = 110;
                this.MC.HeaderText = "类别名称";
                this.MC.Name = this.strHead[index];
                this.MC.DataPropertyName = this.strHead[index++];
                this.MC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.MC.Width = 230;
                this.QSHM.HeaderText = "起始号码";
                this.QSHM.Name = this.strHead[index];
                this.QSHM.DataPropertyName = this.strHead[index++];
                this.QSHM.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.QSHM.Width = 90;
                this.SYZS.HeaderText = "发票张数";
                this.SYZS.Name = this.strHead[index];
                this.SYZS.DataPropertyName = this.strHead[index++];
                this.SYZS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.SYZS.Width = 80;
                this.JZZH.HeaderText = "卷终止号";
                this.JZZH.Name = this.strHead[index];
                this.JZZH.DataPropertyName = this.strHead[index++];
                this.JZZH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.JZZH.Width = 80;
                this.LGRQ.HeaderText = "领购日期";
                this.LGRQ.Name = this.strHead[index];
                this.LGRQ.DataPropertyName = this.strHead[index++];
                this.LGRQ.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.LGRQ.Width = 90;
                this.LGZS.HeaderText = "领购张数";
                this.LGZS.Name = this.strHead[index];
                this.LGZS.DataPropertyName = this.strHead[index++];
                this.LGZS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.LGZS.Width = 90;
                this.FPZL.FillWeight = 21f;
                this.KPXE.FillWeight = 12f;
                this.JH.FillWeight = 7f;
                this.LBDM.FillWeight = 11f;
                this.MC.FillWeight = 23f;
                this.QSHM.FillWeight = 9f;
                this.SYZS.FillWeight = 8f;
                this.JZZH.FillWeight = 9f;
                this.LGRQ.FillWeight = 9f;
                this.LGZS.FillWeight = 9f;
                int num2 = 0;
                this.customStyleDataGrid1.ColumnAdd(this.FPZL);
                this.customStyleDataGrid1.SetColumnReadOnly(num2++, true);
                this.customStyleDataGrid1.ColumnAdd(this.KPXE);
                this.customStyleDataGrid1.SetColumnReadOnly(num2++, true);
                this.customStyleDataGrid1.ColumnAdd(this.JH);
                this.customStyleDataGrid1.SetColumnReadOnly(num2++, true);
                this.customStyleDataGrid1.ColumnAdd(this.LBDM);
                this.customStyleDataGrid1.SetColumnReadOnly(num2++, true);
                this.customStyleDataGrid1.ColumnAdd(this.MC);
                this.customStyleDataGrid1.SetColumnReadOnly(num2++, true);
                this.customStyleDataGrid1.ColumnAdd(this.QSHM);
                this.customStyleDataGrid1.SetColumnReadOnly(num2++, true);
                this.customStyleDataGrid1.ColumnAdd(this.SYZS);
                this.customStyleDataGrid1.SetColumnReadOnly(num2++, true);
                this.customStyleDataGrid1.ColumnAdd(this.JZZH);
                this.customStyleDataGrid1.SetColumnReadOnly(num2++, true);
                this.customStyleDataGrid1.ColumnAdd(this.LGRQ);
                this.customStyleDataGrid1.SetColumnReadOnly(num2++, true);
                this.customStyleDataGrid1.ColumnAdd(this.LGZS);
                this.customStyleDataGrid1.SetColumnReadOnly(num2++, true);
                this.customStyleDataGrid1.AllowUserToAddRows = false;
            }
            catch (BaseException exception)
            {
                this._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected virtual bool InvMonthMatched(string invMonth)
        {
            if (this.toolCmbMonth.SelectedItem == null)
            {
                return true;
            }
            string str = this.toolCmbMonth.SelectedItem.ToString();
            return (str.Equals("全部") || str.Substring(0, str.IndexOf('月')).Equals(invMonth));
        }

        protected virtual bool InvTypeMatched(string invType)
        {
            if (this.toolCmbFpzl.SelectedItem == null)
            {
                return true;
            }
            string str = this.toolCmbFpzl.SelectedItem.ToString();
            return (str.Equals("全部") || str.Equals(invType));
        }

        protected virtual bool InvYearMatched(string invYear)
        {
            if (this.toolCmbYear.SelectedItem == null)
            {
                return true;
            }
            string str = this.toolCmbYear.SelectedItem.ToString();
            return (str.Equals("全部") || str.Substring(0, 4).Equals(invYear));
        }

        protected void QueryComponentsLayoutSet()
        {
            this.toolLblYear.Alignment = ToolStripItemAlignment.Right;
            this.toolCmbYear.Alignment = ToolStripItemAlignment.Right;
            this.toolLblMonth.Alignment = ToolStripItemAlignment.Right;
            this.toolCmbMonth.Alignment = ToolStripItemAlignment.Right;
            this.toolLblFpzl.Alignment = ToolStripItemAlignment.Right;
            this.toolCmbFpzl.Alignment = ToolStripItemAlignment.Right;
            this.toolLblRetrieve.Alignment = ToolStripItemAlignment.Right;
            this.toolTxtRetrieve.Alignment = ToolStripItemAlignment.Right;
            this.toolCmbYear.Width = 70;
            this.toolCmbMonth.Width = 0x37;
            this.toolCmbFpzl.Width = 0xb9;
            this.toolCmbFpzl.DropDownWidth = 170;
            this.toolTxtRetrieve.Width = 120;
        }

        protected virtual void QueryIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.queryInitialized)
                {
                    this._ListModel = base.TaxCardInstance.GetInvStock();
                    if (0 < base.TaxCardInstance.get_RetCode())
                    {
                        MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                        this._bError = true;
                        base.Close();
                    }
                    else
                    {
                        if (this.customStyleDataGrid1.DataSource != null)
                        {
                            ((DataTable) this.customStyleDataGrid1.DataSource).Clear();
                        }
                        DataTable table = this.CreateTableHeader();
                        int num = 0;
                        int num2 = 0;
                        while (num2 < this._ListModel.Count)
                        {
                            DataRow row = table.NewRow();
                            string invType = ShareMethods.GetInvType(this._ListModel[num2].InvType);
                            if (this.InvTypeMatched(invType))
                            {
                                row["FPZL"] = invType;
                                DateTime buyDate = this._ListModel[num2].BuyDate;
                                if (this.InvYearMatched(buyDate.Year.ToString()) && this.InvMonthMatched(buyDate.Month.ToString()))
                                {
                                    row["LGRQ"] = buyDate.ToString("yyyy-MM-dd");
                                    row["KPXE"] = this.GetInvUpLimit(invType);
                                    row["JH"] = Convert.ToString(num);
                                    row["LBDM"] = this._ListModel[num2].TypeCode;
                                    row["MC"] = ShareMethods.GetFPLBMC(this._ListModel[num2], this._dictFPLBBM);
                                    row["QSHM"] = ShareMethods.FPHMTo8Wei(this._ListModel[num2].HeadCode);
                                    row["SYZS"] = Convert.ToString(this._ListModel[num2].Number);
                                    uint num5 = (this._ListModel[num2].HeadCode + this._ListModel[num2].Number) - 1;
                                    row["JZZH"] = num5.ToString().PadLeft(8, '0');
                                    row["LGZS"] = this._ListModel[num2].BuyNumber.ToString();
                                    table.Rows.Add(row);
                                }
                            }
                            num2++;
                            num++;
                        }
                        this.customStyleDataGrid1.DataSource = table;
                    }
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

        protected void SetXmlCtrl()
        {
            try
            {
                this.pnlManipulation = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnlManipulation");
                this.pnlAttention = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnlAttention");
                this.lblAttentionHeader = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblAttentionHeader");
                this.lblAttentionDetail = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblAttentionDetail");
                this.pnlAttention.Visible = false;
                this.lblAttentionHeader.Visible = false;
                this.lblAttentionDetail.Visible = false;
                this.toolStrip = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
                this.tool_Close = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_TuiChu");
                this.tool_XuanZe = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_XuanZe");
                this.tool_FPTuiHui = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_FPTuiHui");
                this.toolStripSeparator1 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator1");
                this.tool_Find = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Find");
                this.tool_Print = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Print");
                this.tool_TongJi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_TongJi");
                this.tool_GeShi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_GeShi");
                this.toolLblRetrieve = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolLblRetrieve");
                this.toolLblFpzl = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolLblFpzl");
                this.toolLblYear = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolLblYear");
                this.toolLblMonth = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolLblMonth");
                this.toolTxtRetrieve = this.xmlComponentLoader1.GetControlByName<ToolStripTextBox>("toolTxtRetrieve");
                this.toolCmbFpzl = this.xmlComponentLoader1.GetControlByName<ToolStripComboBox>("toolCmbFpzl");
                this.toolCmbYear = this.xmlComponentLoader1.GetControlByName<ToolStripComboBox>("toolCmbYear");
                this.toolCmbMonth = this.xmlComponentLoader1.GetControlByName<ToolStripComboBox>("toolCmbMonth");
                this.tool_Close.Visible = true;
                this.tool_XuanZe.Visible = false;
                this.tool_FPTuiHui.Visible = false;
                this.toolStripSeparator1.Visible = true;
                this.tool_Find.Visible = true;
                this.tool_Print.Visible = true;
                this.tool_TongJi.Visible = true;
                this.tool_GeShi.Visible = true;
                this.toolLblRetrieve.Visible = false;
                this.toolTxtRetrieve.Visible = false;
                this.tool_Close.Click += new EventHandler(this.tool_Close_Click);
                this.tool_Find.Click += new EventHandler(this.tool_Find_Click);
                this.tool_Print.Click += new EventHandler(this.tool_Print_Click);
                this.tool_TongJi.Click += new EventHandler(this.tool_TongJi_Click);
                this.tool_GeShi.Click += new EventHandler(this.tool_GeShi_Click);
                this.toolCmbFpzl.SelectedIndexChanged += new EventHandler(this.QueryIndexChanged);
                this.toolCmbYear.SelectedIndexChanged += new EventHandler(this.QueryIndexChanged);
                this.toolCmbMonth.SelectedIndexChanged += new EventHandler(this.QueryIndexChanged);
                this.toolCmbFpzl.DropDownStyle = ComboBoxStyle.DropDownList;
                this.toolCmbYear.DropDownStyle = ComboBoxStyle.DropDownList;
                this.toolCmbMonth.DropDownStyle = ComboBoxStyle.DropDownList;
                this.tool_Close.Margin = new Padding(20, 1, 0, 2);
                ControlStyleUtil.SetToolStripStyle(this.toolStrip);
                this.QueryComponentsLayoutSet();
                this.customStyleDataGrid1 = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1");
                this.customStyleDataGrid1.AllowUserToDeleteRows = false;
                this.customStyleDataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                this.customStyleDataGrid1.ReadOnly = true;
                this.customStyleDataGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                this.customStyleDataGrid1.MultiSelect = false;
                this.InsertGridColumn();
                this._dictFPLBBM = ShareMethods.GetFPLBBM();
                this.pnlManipulation.Controls.Add(this.customStyleDataGrid1);
                this.pnlManipulation.Controls.Add(this.toolStrip);
                this.pnlManipulation.Controls.Add(this.pnlAttention);
            }
            catch (BaseException exception)
            {
                this._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected virtual void tool_Close_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (BaseException exception)
            {
                this._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected virtual void tool_Find_Click(object sender, EventArgs e)
        {
            try
            {
                this.customStyleDataGrid1.Select(this);
            }
            catch (BaseException exception)
            {
                this._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected virtual void tool_GeShi_Click(object sender, EventArgs e)
        {
            try
            {
                this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1").SetColumnStyles(this.xmlComponentLoader1.get_XMLPath(), this);
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

        protected virtual void tool_Print_Click(object sender, EventArgs e)
        {
            try
            {
                this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1").Print(this.strDaYinTitle, this, null, null, true);
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

        protected virtual void tool_TongJi_Click(object sender, EventArgs e)
        {
            try
            {
                this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1").Statistics(this);
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

