namespace Aisino.Fwkp.Fplygl.Form.WSFTP_6
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Windows.Forms;

    public class AllocateVolume : ParentForm
    {
        private IContainer components;
        private ILog loger = LogUtil.GetLogger<AllocateVolume>();

        public AllocateVolume()
        {
            try
            {
                this.Initialize();
                base.tool_XuanZe.Visible = true;
                base.tool_XuanZe.Click += new EventHandler(this.tool_XuanZe_Click);
                base.customStyleDataGrid1.DoubleClick += new EventHandler(this.tool_XuanZe_Click);
                base.strDaYinTitle = "金税设备库存发票分配";
                base.Name = "XfjfpfpForm";
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

        protected override bool CheckEmpty(List<InvVolumeApp> ListModel)
        {
            if ((ListModel != null) && (ListModel.Count > 0))
            {
                return true;
            }
            MessageManager.ShowMsgBox("INP-4412B3");
            base._bError = true;
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FenPeiFaPiaoJuan()
        {
            try
            {
                DataGridViewRow currentRow = base.customStyleDataGrid1.CurrentRow;
                if ((currentRow == null) || (base.customStyleDataGrid1.RowCount <= 0))
                {
                    return false;
                }
                base.SelectedItems.Clear();
                base.SelectedItems.Add(currentRow.Cells["FPZL"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["LBDM"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["MC"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["QSHM"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["SYZS"].Value.ToString().Trim());
                if (base.SelectedItems.Count <= 0)
                {
                    MessageManager.ShowMsgBox("INP-441202");
                    return false;
                }
                AllocateInput input = new AllocateInput(base.SelectedItems);
                if (DialogResult.OK == input.ShowDialog())
                {
                    this.QueryIndexChanged(null, null);
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
            return true;
        }

        private List<InvVolumeApp> filterTransAndElec()
        {
            List<InvVolumeApp> list = new List<InvVolumeApp>();
            foreach (InvVolumeApp app in base._ListModel)
            {
                if ((11 != app.InvType) && (0x33 != app.InvType))
                {
                    list.Add(app);
                }
            }
            return list;
        }

        protected override void FlushGridData()
        {
            base._ListModel = base.TaxCardInstance.GetInvStock();
            if (0 < base.TaxCardInstance.get_RetCode())
            {
                MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                base._bError = true;
                base.Close();
            }
            else
            {
                base._ListModel = this.filterTransAndElec();
                this.InsertData(base._ListModel);
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "XfjfpfpForm";
        }

        protected override void InitializeQueryInvType()
        {
            base.toolCmbFpzl.Items.Clear();
            base.toolCmbFpzl.Items.Add("全部");
            base.toolCmbFpzl.Items.Add("增值税专用发票");
            base.toolCmbFpzl.Items.Add("增值税普通发票");
            base.toolCmbFpzl.Items.Add("机动车销售统一发票");
            base.toolCmbFpzl.SelectedIndex = 0;
        }

        protected override void InsertData(List<InvVolumeApp> ListModel)
        {
            try
            {
                if (base.customStyleDataGrid1.DataSource != null)
                {
                    ((DataTable) base.customStyleDataGrid1.DataSource).Clear();
                }
                if (this.CheckEmpty(ListModel))
                {
                    DataTable table = this.CreateTableHeader();
                    int num = 0;
                    int yearExist = 0x7fffffff;
                    foreach (InvVolumeApp app in ListModel)
                    {
                        if (((11 != app.InvType) && (0x33 != app.InvType)) && (0x29 != app.InvType))
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
                                row["MC"] = ShareMethods.GetFPLBMC(app, base._dictFPLBBM);
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
                    }
                    base.customStyleDataGrid1.DataSource = table;
                    this.InitializeQueryComponents(yearExist);
                }
            }
            catch (BaseException exception)
            {
                base._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                base._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected override void QueryIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (base.queryInitialized)
                {
                    base._ListModel = base.TaxCardInstance.GetInvStock();
                    if (0 < base.TaxCardInstance.get_RetCode())
                    {
                        MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                        base._bError = true;
                        base.Close();
                    }
                    else
                    {
                        base._ListModel = this.filterTransAndElec();
                        if (base.customStyleDataGrid1.DataSource != null)
                        {
                            ((DataTable) base.customStyleDataGrid1.DataSource).Clear();
                        }
                        DataTable table = this.CreateTableHeader();
                        int num = 0;
                        int num2 = 0;
                        while (num2 < base._ListModel.Count)
                        {
                            if (((11 != base._ListModel[num2].InvType) && (0x33 != base._ListModel[num2].InvType)) && (0x29 != base._ListModel[num2].InvType))
                            {
                                DataRow row = table.NewRow();
                                string invType = ShareMethods.GetInvType(base._ListModel[num2].InvType);
                                if (this.InvTypeMatched(invType))
                                {
                                    row["FPZL"] = invType;
                                    DateTime buyDate = base._ListModel[num2].BuyDate;
                                    if (this.InvYearMatched(buyDate.Year.ToString()) && this.InvMonthMatched(buyDate.Month.ToString()))
                                    {
                                        row["LGRQ"] = buyDate.ToString("yyyy-MM-dd");
                                        row["KPXE"] = this.GetInvUpLimit(invType);
                                        row["JH"] = Convert.ToString(num);
                                        row["LBDM"] = base._ListModel[num2].TypeCode;
                                        row["MC"] = ShareMethods.GetFPLBMC(base._ListModel[num2], base._dictFPLBBM);
                                        row["QSHM"] = ShareMethods.FPHMTo8Wei(base._ListModel[num2].HeadCode);
                                        row["SYZS"] = Convert.ToString(base._ListModel[num2].Number);
                                        uint num5 = (base._ListModel[num2].HeadCode + base._ListModel[num2].Number) - 1;
                                        row["JZZH"] = num5.ToString().PadLeft(8, '0');
                                        row["LGZS"] = base._ListModel[num2].BuyNumber.ToString();
                                        table.Rows.Add(row);
                                    }
                                }
                            }
                            num2++;
                            num++;
                        }
                        base.customStyleDataGrid1.DataSource = table;
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

        private void tool_XuanZe_Click(object sender, EventArgs e)
        {
            try
            {
                this.FenPeiFaPiaoJuan();
            }
            catch (BaseException exception)
            {
                base._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                base._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }
    }
}

