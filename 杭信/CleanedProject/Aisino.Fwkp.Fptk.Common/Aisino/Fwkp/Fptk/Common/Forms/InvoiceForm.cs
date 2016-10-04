namespace Aisino.Fwkp.Fptk.Common.Forms
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Framework.MainForm;
    using Framework.MainForm.UpDown;
    public abstract class InvoiceForm : DockForm
    {
        protected Invoice _fpxx;
        protected bool _onlyShow;
        protected AisinoMultiCombox _spmcBt;
        protected XmlComponentLoader _xmlComponentLoader;
        private bool bool_0;
        protected bool cellEndEdit;
        protected AisinoCMB comboBox_SLV;
        private CustomStyleDataGrid customStyleDataGrid_0;
        private CustomStyleDataGrid customStyleDataGrid_1;
        private DataGridViewTextBoxEditingControl dataGridViewTextBoxEditingControl_0;
        private Dictionary<FPLX, List<SLV>> dictionary_0;
        private IContainer icontainer_3;
        private ILog ilog_0;
        protected bool QdToMx;
        public static bool qingdanflag;
        private string string_0;
        private ToolStripButton toolStripButton_0;
        private ToolStripButton toolStripButton_1;
        private ToolStripButton toolStripButton_10;
        private ToolStripButton toolStripButton_2;
        private ToolStripButton toolStripButton_3;
        private ToolStripButton toolStripButton_4;
        private ToolStripButton toolStripButton_5;
        private ToolStripButton toolStripButton_6;
        private ToolStripButton toolStripButton_7;
        private ToolStripButton toolStripButton_8;
        private ToolStripButton toolStripButton_9;

        static InvoiceForm()
        {
            
        }

        protected InvoiceForm()
        {
            
            this.string_0 = string.Empty;
            this.ilog_0 = LogUtil.GetLogger<InvoiceForm>();
        }

        protected virtual bool _AddSpxx(int int_0)
        {
            bool flag = false;
            CustomStyleDataGrid grid = null;
            if (this._DataGridView_qd == null)
            {
                grid = this._DataGridView;
            }
            else
            {
                grid = this._DataGridView_qd;
            }
            try
            {
                if (this._fpxx.CanAddSpxx(1, false))
                {
                    int num2;
                    string sLv = this._SetDefaultSpsLv();
                    if (((sLv == "0.05") && (this._fpxx.Fplx == FPLX.ZYFP)) && (this._fpxx.Zyfplx == ZYFP_LX.ZYFP))
                    {
                        this._fpxx.SetZyfpLx(ZYFP_LX.HYSY);
                    }
                    if (this._fpxx.SLv.Length > 0)
                    {
                        sLv = this._fpxx.SLv;
                    }
                    if (int_0 < 0)
                    {
                        num2 = this._fpxx.AddSpxx(this._SetDefaultSpsm(), sLv, this._fpxx.Zyfplx);
                        if (num2 >= 0)
                        {
                            int num3 = grid.Rows.Add();
                            this._ShowDataGrid(grid, this._fpxx.GetSpxx(num3), num3);
                            flag = true;
                        }
                    }
                    else
                    {
                        num2 = this._fpxx.InsertSpxx(int_0, this._SetDefaultSpsm(), sLv, this._fpxx.Zyfplx);
                        if (num2 >= 0)
                        {
                            grid.Rows.Insert(int_0, new object[0]);
                            this._ShowDataGrid(grid, this._fpxx.GetSpxx(int_0), int_0);
                            flag = true;
                        }
                    }
                    if (num2 < 0)
                    {
                        MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                    }
                    return flag;
                }
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                if (this._fpxx.GetCode() == "A122")
                {
                    int num = int.Parse(this._fpxx.Params[0]);
                    if (num > 0)
                    {
                        grid.Rows[num - 1].Cells["SL"].Value = "";
                    }
                }
            }
            catch (Exception exception)
            {
                this.ilog_0.Error("_AddSpxx", exception);
            }
            return flag;
        }

        protected virtual bool _CheckDelSPRow(CustomStyleDataGrid customStyleDataGrid_2)
        {
            return true;
        }

        protected virtual void _comboBox_SLV_KeyPress(object sender, KeyPressEventArgs e)
        {
            AisinoCMB ocmb = (AisinoCMB) sender;
            int num = ocmb.SelectionStart + ocmb.SelectionLength;
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else if (((ocmb.Text.IndexOf("%") >= 0) && (ocmb.SelectedText.IndexOf("%") < 0)) && (num > ocmb.Text.IndexOf("%")))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.')
            {
                if ((ocmb.Text.IndexOf(".") >= 0) && (ocmb.SelectedText.IndexOf(".") < 0))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else if (e.KeyChar == '%')
            {
                if (num != ocmb.Text.Length)
                {
                    e.Handled = true;
                }
            }
            else if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        protected virtual bool _comboBox_SLV_ReadOnly()
        {
            return true;
        }

        protected virtual void _comboBox_SLV_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomStyleDataGrid parent = (CustomStyleDataGrid) ((AisinoCMB) sender).Parent;
            if (parent.Rows.Count != 0)
            {
                int rowIndex = parent.CurrentCell.RowIndex;
                Dictionary<SPXX, string> spxx = this._fpxx.GetSpxx(rowIndex);
                if (spxx != null)
                {
                    string str2 = spxx[SPXX.YHSM];
                    SLV selectedItem = this.comboBox_SLV.SelectedItem as SLV;
                    string str3 = selectedItem.ToString();
                    this._fpxx.SetLslvbs(rowIndex, "");
                    this.bool_0 = false;
                    switch (str3)
                    {
                        case "0%":
                            if (str2 == "出口零税")
                            {
                                this._fpxx.SetLslvbs(rowIndex, "0");
                                this._fpxx.SetXsyh(rowIndex, "1");
                            }
                            else
                            {
                                this._fpxx.SetLslvbs(rowIndex, "3");
                                if ((!(str2 == "免税") && !(str2 == "不征税")) && this.yhzc_contain_slv(str2, str3, true, false))
                                {
                                    this._fpxx.SetXsyh(rowIndex, "1");
                                }
                                else
                                {
                                    this._fpxx.SetXsyh(rowIndex, "0");
                                }
                            }
                            break;

                        case "免税":
                            this._fpxx.SetLslvbs(rowIndex, "1");
                            this._fpxx.SetXsyh(rowIndex, "1");
                            break;

                        case "不征税":
                            this._fpxx.SetLslvbs(rowIndex, "2");
                            this._fpxx.SetXsyh(rowIndex, "1");
                            break;

                        case "中外合作油气田":
                            this._fpxx.SetLslvbs(rowIndex, "");
                            if (this.yhzc_contain_slv(str2, str3, true, true))
                            {
                                this._fpxx.SetXsyh(rowIndex, "1");
                            }
                            else
                            {
                                this._fpxx.SetXsyh(rowIndex, "0");
                            }
                            break;

                        default:
                            this._fpxx.SetLslvbs(rowIndex, "");
                            if (this.yhzc_contain_slv(str2, str3, true, false))
                            {
                                this._fpxx.SetXsyh(rowIndex, "1");
                            }
                            else
                            {
                                this._fpxx.SetXsyh(rowIndex, "0");
                            }
                            break;
                    }
                    string dataValue = selectedItem.DataValue;
                    if (str3 == "")
                    {
                        this.bool_0 = true;
                    }
                    this._UpdateSLv(parent, dataValue);
                }
            }
        }

        protected virtual void _comboBox_SLV_VisibleChanged(object sender, EventArgs e)
        {
            if ((!this._comboBox_SLV_ReadOnly() && !this.comboBox_SLV.Visible) && (this.comboBox_SLV.SelectedIndex == -1))
            {
                string text = this.comboBox_SLV.Text;
                this._UpdateSLv((CustomStyleDataGrid) ((AisinoCMB) sender).Parent, text);
            }
        }

        protected void _CommitEditGrid()
        {
            ((this._DataGridView_qd == null) ? this._DataGridView : this._DataGridView_qd).EndEdit();
        }

        protected virtual void _dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            int rowIndex = e.RowIndex;
            this._ShowDataGrid(grid, this._fpxx.GetSpxx(rowIndex), rowIndex);
        }

        protected virtual void _dataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            AisinoMultiCombox combox = grid.Controls["SPMCBT"] as AisinoMultiCombox;
            AisinoCMB ocmb = grid.Controls["SLCB"] as AisinoCMB;
            if ((grid.CurrentCell != null) && !grid.CurrentRow.ReadOnly)
            {
                int rowIndex = grid.CurrentCell.RowIndex;
                if (rowIndex >= 0)
                {
                    DataGridViewRow row = grid.Rows[rowIndex];
                    if ((row.Cells["SPMC"].Value != null) && (row.Cells["SPMC"].Value.ToString().Length > 0))
                    {
                        if (ocmb != null)
                        {
                            ocmb.Enabled = true;
                        }
                    }
                    else if (ocmb != null)
                    {
                        ocmb.Enabled = false;
                    }
                }
                DataGridViewColumn owningColumn = grid.CurrentCell.OwningColumn;
                if (owningColumn.Name.Equals("SLV") && (owningColumn.Tag == null))
                {
                    if ((grid.Rows.Count <= 1) || this._fpxx.SupportMulti)
                    {
                        int index = owningColumn.Index;
                        int num2 = grid.CurrentCell.RowIndex;
                        Rectangle rectangle = grid.GetCellDisplayRectangle(index, num2, false);
                        if (ocmb != null)
                        {
                            ocmb.Left = rectangle.Left;
                            ocmb.Top = rectangle.Top;
                            ocmb.Width = rectangle.Width;
                            ocmb.Height = rectangle.Height;
                            object obj2 = grid.Rows[num2].Cells[index].Value;
                            if (obj2 != null)
                            {
                                string str = obj2.ToString();
                                FPLX fplx = this._fpxx.Fplx;
                                if (!(str == "") || (this._fpxx.GetSpxx(rowIndex)[SPXX.FPHXZ] != FPHXZ.XJDYZSFPQD.ToString("D")))
                                {
                                    ocmb.Text = str;
                                }
                                if (ocmb.Enabled)
                                {
                                    ocmb.Visible = true;
                                    ocmb.Focus();
                                }
                            }
                        }
                    }
                }
                else if (ocmb != null)
                {
                    ocmb.Visible = false;
                }
                if (!owningColumn.Name.Equals("SPMC"))
                {
                    if (combox != null)
                    {
                        combox.Visible = false;
                    }
                }
                else
                {
                    int columnIndex = owningColumn.Index;
                    int num5 = grid.CurrentCell.RowIndex;
                    Rectangle rectangle2 = grid.GetCellDisplayRectangle(columnIndex, num5, false);
                    if (combox != null)
                    {
                        combox.Left = rectangle2.Left;
                        combox.Top = rectangle2.Top;
                        combox.Width = rectangle2.Width;
                        combox.Height = rectangle2.Height;
                        combox.Text = (grid.CurrentCell.Value == null) ? "" : grid.CurrentCell.Value.ToString();
                        DataTable dataSource = combox.DataSource;
                        if (dataSource != null)
                        {
                            dataSource.Clear();
                        }
                        combox.Visible = true;
                        combox.Focus();
                    }
                }
            }
            else if ((grid.CurrentRow != null) && grid.CurrentRow.ReadOnly)
            {
                if (ocmb != null)
                {
                    ocmb.Visible = false;
                }
                if (combox != null)
                {
                    combox.Visible = false;
                }
            }
        }

        protected virtual void _dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            string name = grid.Name;
            if ((this._DataGridView_qd != null) && !name.Equals("DataGrid1"))
            {
                if (this.toolStripButton_9 != null)
                {
                    this.toolStripButton_9.Enabled = true;
                }
            }
            else
            {
                this._DelRowButton.Enabled = true;
                if ((this._fpxx != null) && this._fpxx.Qdbz)
                {
                    this._QingdanButton.Enabled = true;
                }
                else
                {
                    this._QingdanButton.Enabled = true;
                    this._AddRowButton.Enabled = true;
                }
            }
            if (grid.CurrentRow != null)
            {
                this._UpdateStatusOfRow(grid, grid.CurrentRow.Index);
            }
        }

        protected virtual void _dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (this._DataGridView_qd == null)
            {
                if (this._DataGridView.Rows.Count == 0)
                {
                    if (this._DelRowButton != null)
                    {
                        this._DelRowButton.Enabled = false;
                    }
                    if (this._ZhekouButton != null)
                    {
                        this._ZhekouButton.Enabled = false;
                    }
                    if (this._QingdanButton != null)
                    {
                        this._QingdanButton.Enabled = true;
                    }
                }
            }
            else if (this._DataGridView_qd.Rows.Count == 0)
            {
                if (this.toolStripButton_9 != null)
                {
                    this.toolStripButton_9.Enabled = false;
                }
                if (this.toolStripButton_5 != null)
                {
                    this.toolStripButton_5.Enabled = false;
                }
            }
        }

        protected virtual void _EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            AisinoMultiCombox parent = ((DataGridViewTextBoxEditingControl) sender).Parent as AisinoMultiCombox;
            if (parent != null)
            {
                CustomStyleDataGrid grid = parent.Parent as CustomStyleDataGrid;
                if (grid != null)
                {
                    string name = grid.CurrentCell.OwningColumn.Name;
                    if ((name.Equals("DJ") || name.Equals("SL")) || (name.Equals("JE") || name.Equals("SE")))
                    {
                        DataGridViewTextBoxEditingControl control = (DataGridViewTextBoxEditingControl) sender;
                        if ((this._fpxx.IsRed && ((name.Equals("SL") || name.Equals("JE")) || name.Equals("SE"))) && (e.KeyChar.ToString() == "-"))
                        {
                            if (control.Text.IndexOf("-") >= 0)
                            {
                                e.Handled = true;
                            }
                            else
                            {
                                e.Handled = false;
                            }
                        }
                        else if (e.KeyChar.ToString() == "\b")
                        {
                            e.Handled = false;
                        }
                        else
                        {
                            if (e.KeyChar.ToString() == ".")
                            {
                                if (control.Text.IndexOf(".") >= 0)
                                {
                                    e.Handled = true;
                                }
                                else
                                {
                                    e.Handled = false;
                                }
                            }
                            else if (!char.IsDigit(e.KeyChar))
                            {
                                e.Handled = true;
                            }
                            if (((name.Equals("DJ") || name.Equals("SL")) || name.Equals("JE")) && ((char.IsDigit(e.KeyChar) && (e.KeyChar >= 0xff10)) && (e.KeyChar <= 0xff19)))
                            {
                                e.KeyChar = (char) (e.KeyChar - 0xfee0);
                            }
                        }
                    }
                }
            }
        }

        protected virtual void _FormMain_UpdateUserNameEvent(string string_1)
        {
        }

        protected SLV _GetSLv(FPLX fplx_0, string string_1, int int_0)
        {
            double num;
            if ((string_1 == "免税") || (string_1 == "不征税"))
            {
                string_1 = "0%";
            }
            if (string_1 == "中外合作油气田")
            {
                return new SLV(FPLX.ZYFP, ZYFP_LX.HYSY, "0.05", "中外合作油气田", "中外合作油气田");
            }
            if (((string_1 == "0.05") || (string_1 == "0.050")) && ((fplx_0 == FPLX.ZYFP) && (this._fpxx.Zyfplx == ZYFP_LX.HYSY)))
            {
                return new SLV(FPLX.ZYFP, ZYFP_LX.HYSY, "0.05", "中外合作油气田", "中外合作油气田");
            }
            if (string_1 == "0.015")
            {
                return new SLV(fplx_0, ZYFP_LX.JZ_50_15, "0.015", "减按1.5%计算", "减按1.5%计算");
            }
            if (int_0 != 0)
            {
                return null;
            }
            if (double.TryParse(string_1, out num))
            {
                string str = (string_1.Length == 0) ? "" : (((num * 100.0)).ToString() + "%");
                return new SLV(fplx_0, ZYFP_LX.ZYFP, (string_1.Length == 0) ? "" : num.ToString(), str, str);
            }
            return new SLV(fplx_0, ZYFP_LX.ZYFP, string_1, string_1, string_1);
        }

        protected virtual Dictionary<FPLX, List<SLV>> _GetSLvList()
        {
            Dictionary<FPLX, List<SLV>> dictionary = new Dictionary<FPLX, List<SLV>>();
            List<SLV> list = new List<SLV> {
                new SLV(0, 0, "0.17", "17%", "17%"),
                new SLV(0, 0, "0.13", "13%", "13%"),
                new SLV(0, 0, "0.06", "6%", "6%"),
                new SLV(0, 0, "0.04", "4%", "4%")
            };
            dictionary.Add(FPLX.ZYFP, list);
            List<SLV> list2 = new List<SLV> {
                new SLV(FPLX.PTFP, 0, "0.17", "17%", "17%"),
                new SLV(FPLX.PTFP, 0, "0.13", "13%", "13%"),
                new SLV(FPLX.PTFP, 0, "0.06", "6%", "6%"),
                new SLV(FPLX.PTFP, 0, "0.04", "4%", "4%"),
                new SLV(FPLX.PTFP, 0, "0.00", "0", "")
            };
            dictionary.Add(FPLX.PTFP, list2);
            List<SLV> list3 = new List<SLV> {
                new SLV(FPLX.DZFP, 0, "0.17", "17%", "17%"),
                new SLV(FPLX.DZFP, 0, "0.13", "13%", "13%"),
                new SLV(FPLX.DZFP, 0, "0.06", "6%", "6%"),
                new SLV(FPLX.DZFP, 0, "0.04", "4%", "4%"),
                new SLV(FPLX.DZFP, 0, "0.00", "0", "")
            };
            dictionary.Add(FPLX.DZFP, list3);
            new List<SLV>();
            list3.Add(new SLV(FPLX.JSFP, ZYFP_LX.ZYFP, "0.17", "17%", "17%"));
            list3.Add(new SLV(FPLX.JSFP, ZYFP_LX.ZYFP, "0.13", "13%", "13%"));
            list3.Add(new SLV(FPLX.JSFP, ZYFP_LX.ZYFP, "0.06", "6%", "6%"));
            list3.Add(new SLV(FPLX.JSFP, ZYFP_LX.ZYFP, "0.04", "4%", "4%"));
            list3.Add(new SLV(FPLX.JSFP, ZYFP_LX.ZYFP, "0.00", "0", ""));
            dictionary.Add(FPLX.JSFP, list3);
            return dictionary;
        }

        protected virtual string _GetSpmcText()
        {
            return this._spmcBt.Text.Trim();
        }

        protected virtual DataTable _GfxxOnAutoCompleteDataSource(string string_1)
        {
            return null;
        }

        protected virtual void _GfxxSelect(string string_1, int int_0)
        {
        }

        protected virtual void _GfxxSetValue(Dictionary<string, string> khxx)
        {
        }

        protected void _hsjbzButton_Click(object sender, EventArgs e)
        {
            if (this._DataGridView_qd == null)
            {
                this._SetHsjxx(this._DataGridView, this.toolStripButton_1.Checked);
            }
            else
            {
                this._SetHsjxx(this._DataGridView_qd, !this._fpxx.Hsjbz);
            }
        }

        protected virtual void _InitQingdanForm()
        {
        }

        protected virtual void _InvoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this._onlyShow && (MessageManager.ShowMsgBox("INP-242199") != DialogResult.Yes))
            {
                e.Cancel = true;
            }
            else
            {
                FormMain.UpdateUserNameEvent -= new FormMain.UpdateUserNameDelegate(this._FormMain_UpdateUserNameEvent);
                this._xmlComponentLoader = null;
                this._DataGridView = null;
                this._DataGridView_qd = null;
                this._fpxx = null;
            }
        }

        protected virtual void _InvoiceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (((this.comboBox_SLV != null) && this.comboBox_SLV.Visible) && ((e.KeyValue == 110) || (e.KeyValue == 190)))
            {
                string text = this.comboBox_SLV.Text;
                string selectedText = this.comboBox_SLV.SelectedText;
                int selectionStart = this.comboBox_SLV.SelectionStart;
                if (((text.IndexOf("%") >= 0) && (selectedText.IndexOf("%") < 0)) && (selectionStart > text.IndexOf("%")))
                {
                    e.Handled = true;
                }
                else if ((text.IndexOf(".") >= 0) && (selectedText.IndexOf(".") < 0))
                {
                    e.Handled = true;
                }
                else
                {
                    int selectionLength = this.comboBox_SLV.SelectionLength;
                    this.comboBox_SLV.SelectedIndex = -1;
                    this.comboBox_SLV.Text = text.Substring(0, selectionStart) + "." + text.Substring(selectionStart + selectionLength);
                    this.comboBox_SLV.SelectionStart = selectionStart + 1;
                }
            }
        }

        protected virtual void _InvoiceForm_Resize(object sender, EventArgs e)
        {
        }

        protected virtual void _qingdanButton_Click(object sender, EventArgs e)
        {
            if (this._DataGridView.Rows.Count > 0)
            {
                this._DataGridView.CurrentCell = this._DataGridView[0, 0];
            }
            qingdanflag = true;
            if (!this._fpxx.Qdbz && !this._fpxx.SetQdbz(true))
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
            }
            else
            {
                if (this._fpxx.GetSpxxs().Count == 0)
                {
                    this._fpxx.AddSpxx(this._SetDefaultSpsm(), this._SetDefaultSpsLv(), this._fpxx.Zyfplx);
                }
                this._InitQingdanForm();
                if (this._spmcBt != null)
                {
                    this._DataGridView_qd.Controls.Add(this._spmcBt);
                    this._spmcBt.Visible = false;
                }
                if (this.comboBox_SLV != null)
                {
                    this._DataGridView_qd.Controls.Add(this.comboBox_SLV);
                    this.comboBox_SLV.Visible = false;
                }
                this._ShowDataGridMxxx(this._DataGridView);
                this._DataGridView.ReadOnly = true;
                this._AddRowButton.Enabled = false;
                this._ZhekouButton.Enabled = false;
                this._DelRowButton.Enabled = true;
                this._ShowDataGrid(this._DataGridView_qd);
                if (this._DataGridView_qd.RowCount > 0)
                {
                    if (this._fpxx.Fplx == FPLX.JSFP)
                    {
                        this._DataGridView_qd.CurrentCell = this._DataGridView_qd.Rows[this._DataGridView_qd.RowCount - 1].Cells[this._DataGridView_qd.Columns.Count - 5];
                    }
                    else
                    {
                        this._DataGridView_qd.CurrentCell = this._DataGridView_qd.Rows[this._DataGridView_qd.RowCount - 1].Cells[this._DataGridView_qd.Columns.Count - 1];
                    }
                }
                this._ShowQingdanForm();
                if (this.comboBox_SLV != null)
                {
                    this.comboBox_SLV.Visible = false;
                    this._DataGridView.Controls.Add(this.comboBox_SLV);
                }
                this._DataGridView_qd = null;
            }
        }

        protected void _SetDataGridReadOnlyColumns(CustomStyleDataGrid customStyleDataGrid_2)
        {
            foreach (DataGridViewColumn column in customStyleDataGrid_2.Columns)
            {
                column.ReadOnly = false;
            }
            string[] strArray = this._SetReadOnlyColumns().Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (customStyleDataGrid_2.Columns.Contains(strArray[i]))
                {
                    customStyleDataGrid_2.Columns[strArray[i]].ReadOnly = true;
                }
            }
        }

        protected virtual string _SetDefaultSpsLv()
        {
            if (this._fpxx.Zyfplx == ZYFP_LX.NCP_SG)
            {
                return "0.00";
            }
            if (this._fpxx.Zyfplx == ZYFP_LX.HYSY)
            {
                return "0.05";
            }
            if (this._fpxx.Zyfplx == ZYFP_LX.JZ_50_15)
            {
                return "0.015";
            }
            string sqSLv = this._fpxx.GetSqSLv();
            if (!string.IsNullOrEmpty(sqSLv))
            {
                List<string> list = new List<string>(sqSLv.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                if (list.Contains("0.17"))
                {
                    return "0.17";
                }
                if (list.Count > 0)
                {
                    if ((list[0] == "0.05") && (list.Count > 1))
                    {
                        list.Remove("0.05");
                        list.Add("0.05");
                    }
                    return list[0];
                }
            }
            return "";
        }

        protected virtual string _SetDefaultSpsm()
        {
            return "0001";
        }

        protected virtual void _SetGfxxControl(AisinoMultiCombox aisinoMultiCombox_0, string string_1)
        {
            aisinoMultiCombox_0.IsSelectAll = true;
            aisinoMultiCombox_0.buttonStyle = ButtonStyle.Button;
            aisinoMultiCombox_0.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            aisinoMultiCombox_0.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", aisinoMultiCombox_0.Width - 140));
            aisinoMultiCombox_0.ShowText = string_1;
            aisinoMultiCombox_0.DrawHead = false;
            aisinoMultiCombox_0.AutoIndex = 0;
            aisinoMultiCombox_0.OnButtonClick += new EventHandler(this.method_13);
            aisinoMultiCombox_0.AutoComplate = AutoComplateStyle.HeadWork;
            aisinoMultiCombox_0.OnAutoComplate += new EventHandler(this.method_14);
            aisinoMultiCombox_0.OnSelectValue += new EventHandler(this.method_15);
        }

        protected virtual void _SetHsjxx(CustomStyleDataGrid customStyleDataGrid_2, bool bool_1)
        {
            string str = bool_1 ? "(含税)" : "(不含税)";
            if (customStyleDataGrid_2.Columns["DJ"] != null)
            {
                customStyleDataGrid_2.Columns["DJ"].HeaderText = customStyleDataGrid_2.Columns["DJ"].HeaderText.Split(new char[] { '(' })[0] + str;
            }
            if (customStyleDataGrid_2.Columns["JE"] != null)
            {
                customStyleDataGrid_2.Columns["JE"].HeaderText = customStyleDataGrid_2.Columns["JE"].HeaderText.Split(new char[] { '(' })[0] + str;
            }
            this._fpxx.Hsjbz = bool_1;
            if (!this._onlyShow)
            {
                PropertyUtil.SetValue("INV-HSJBZ", bool_1 ? "1" : "0");
            }
            if (this._fpxx.Qdbz)
            {
                if (this._DataGridView_qd != null)
                {
                    this._ShowDataGrid(customStyleDataGrid_2);
                    if (this._DataGridView.Columns["DJ"] != null)
                    {
                        this._DataGridView.Columns["DJ"].HeaderText = this._DataGridView.Columns["DJ"].HeaderText.Split(new char[] { '(' })[0] + str;
                    }
                    if (this._DataGridView.Columns["JE"] != null)
                    {
                        this._DataGridView.Columns["JE"].HeaderText = this._DataGridView.Columns["JE"].HeaderText.Split(new char[] { '(' })[0] + str;
                    }
                }
                this._ShowDataGridMxxx(this._DataGridView);
            }
            else
            {
                this._ShowDataGrid(customStyleDataGrid_2);
            }
            this.toolStripButton_1.Checked = bool_1;
        }

        protected virtual void _SetHzxx()
        {
        }

        protected void _SetQingdanFormProp(Form form_0, CustomStyleDataGrid customStyleDataGrid_2, ToolStripButton toolStripButton_11, ToolStripButton toolStripButton_12, ToolStripButton toolStripButton_13, ToolStripButton toolStripButton_14)
        {
            this.toolStripButton_2 = toolStripButton_11;
            this.toolStripButton_2.Checked = this._fpxx.Hsjbz;
            this.toolStripButton_2.Click += new EventHandler(this._hsjbzButton_Click);
            this.toolStripButton_2.MouseDown += new MouseEventHandler(this.toolStripButton_2_MouseDown);
            this.toolStripButton_5 = toolStripButton_12;
            this.toolStripButton_5.Click += new EventHandler(this.toolStripButton_5_Click);
            this.toolStripButton_5.MouseDown += new MouseEventHandler(this.toolStripButton_9_MouseDown);
            this.toolStripButton_7 = toolStripButton_13;
            this.toolStripButton_7.Click += new EventHandler(this.toolStripButton_7_Click);
            this.toolStripButton_7.MouseDown += new MouseEventHandler(this.toolStripButton_9_MouseDown);
            this.toolStripButton_9 = toolStripButton_14;
            this.toolStripButton_9.Click += new EventHandler(this.toolStripButton_9_Click);
            this.toolStripButton_9.MouseDown += new MouseEventHandler(this.toolStripButton_9_MouseDown);
            form_0.FormClosing += new FormClosingEventHandler(this.QingdanFormClosing);
            this._DataGridView_qd = customStyleDataGrid_2;
        }

        protected virtual string _SetReadOnlyColumns()
        {
            return "SPMC,SLV";
        }

        protected void _SetSlvList()
        {
            this.dictionary_0 = this._GetSLvList();
        }

        protected virtual void _SetXfxxControl(AisinoMultiCombox aisinoMultiCombox_0, string string_1)
        {
            aisinoMultiCombox_0.IsSelectAll = true;
            aisinoMultiCombox_0.buttonStyle = ButtonStyle.Button;
            aisinoMultiCombox_0.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 120));
            aisinoMultiCombox_0.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", aisinoMultiCombox_0.Width - 120));
            aisinoMultiCombox_0.ShowText = string_1;
            aisinoMultiCombox_0.DrawHead = false;
            aisinoMultiCombox_0.AutoIndex = 0;
            aisinoMultiCombox_0.OnButtonClick += new EventHandler(this.method_10);
            aisinoMultiCombox_0.AutoComplate = AutoComplateStyle.HeadWork;
            aisinoMultiCombox_0.OnAutoComplate += new EventHandler(this.method_11);
            aisinoMultiCombox_0.OnSelectValue += new EventHandler(this.method_12);
        }

        protected void _ShowDataGrid(CustomStyleDataGrid customStyleDataGrid_2)
        {
            int count = this._fpxx.GetSpxxs().Count;
            for (int i = 0; i < count; i++)
            {
                this._ShowDataGrid(customStyleDataGrid_2, this._fpxx.GetSpxx(i), i);
                this.ilog_0.Info(i.ToString());
            }
            this._SetHzxx();
            customStyleDataGrid_2.CurrentCellChanged += new EventHandler(this._dataGridView_CurrentCellChanged);
        }

        protected virtual void _ShowDataGrid(CustomStyleDataGrid customStyleDataGrid_2, Dictionary<SPXX, string> spxx, int int_0)
        {
            if (spxx != null)
            {
                double num;
                if (!qingdanflag)
                {
                    customStyleDataGrid_2.CurrentCellChanged -= new EventHandler(this._dataGridView_CurrentCellChanged);
                }
                while ((customStyleDataGrid_2.Rows.Count - 1) < int_0)
                {
                    customStyleDataGrid_2.Rows.Add();
                }
                FPLX fplx = this._fpxx.Fplx;
                string showValue = "";
                if (double.TryParse(spxx[SPXX.SLV].ToString(), out num))
                {
                    showValue = this._GetSLv(fplx, spxx[SPXX.SLV], 0).ShowValue;
                }
                if ((!this.isWM() && double.TryParse(spxx[SPXX.SLV].ToString(), out num)) && (num == 0.0))
                {
                    if ((spxx[SPXX.LSLVBS] != "0") && (spxx[SPXX.LSLVBS] != "3"))
                    {
                        if (spxx[SPXX.LSLVBS] == "1")
                        {
                            showValue = "免税";
                        }
                        else if (spxx[SPXX.LSLVBS] == "2")
                        {
                            showValue = "不征税";
                        }
                    }
                    else
                    {
                        showValue = "0%";
                    }
                }
                string str2 = ((spxx[SPXX.SE].Length <= 0) || (Math.Abs(double.Parse(spxx[SPXX.SE])) >= 0.009)) ? spxx[SPXX.SE] : "";
                if ((showValue == "0%") && this.isWM())
                {
                    showValue = "免税";
                }
                string str3 = showValue;
                DataGridViewRow row = customStyleDataGrid_2.Rows[int_0];
                int num2 = 0;
                while (true)
                {
                    if (num2 >= row.Cells.Count)
                    {
                        break;
                    }
                    string name = customStyleDataGrid_2.Columns[num2].Name;
                    try
                    {
                        if (name.Equals("SLV"))
                        {
                            if (this.bool_0)
                            {
                                row.Cells["SLV"].Value = "";
                            }
                            else
                            {
                                row.Cells["SLV"].Value = str3;
                            }
                        }
                        else if (name.Equals("SE"))
                        {
                            row.Cells["SE"].Value = str2;
                        }
                        else
                        {
                            row.Cells[name].Value = spxx[(SPXX) Enum.Parse(typeof(SPXX), name)];
                        }
                    }
                    catch (ArgumentException exception)
                    {
                        this.ilog_0.Error("设置数据表格内容异常", exception);
                    }
                    num2++;
                }
                if (!qingdanflag)
                {
                    this._SetHzxx();
                    customStyleDataGrid_2.CurrentCellChanged += new EventHandler(this._dataGridView_CurrentCellChanged);
                }
            }
        }

        protected void _ShowDataGridMxxx(CustomStyleDataGrid customStyleDataGrid_2)
        {
            List<Dictionary<SPXX, string>> mxxxs = this._fpxx.GetMxxxs();
            if (mxxxs != null)
            {
                int count = mxxxs.Count;
                for (int i = 0; i < count; i++)
                {
                    this._ShowDataGrid(customStyleDataGrid_2, mxxxs[i], i);
                }
                this._SetHzxx();
            }
        }

        protected virtual void _ShowQingdanForm()
        {
        }

        public virtual void _spmcBt_leave(object sender, EventArgs e)
        {
        }

        private void _spmcBt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CustomStyleDataGrid parent = (CustomStyleDataGrid) this._spmcBt.Parent;
            this._SpmcSelect(parent, 0, 0);
        }

        protected virtual void _spmcBt_OnAutoComplate(object sender, EventArgs e)
        {
            string str = this._GetSpmcText();
            CustomStyleDataGrid grid = (this._DataGridView_qd == null) ? this._DataGridView : this._DataGridView_qd;
            DataTable table = this._SpmcOnAutoCompleteDataSource(grid, str);
            if (table != null)
            {
                this._spmcBt.DataSource = table;
            }
        }

        private void _spmcBt_OnButtonClick(object sender, EventArgs e)
        {
            CustomStyleDataGrid parent = (CustomStyleDataGrid) this._spmcBt.Parent;
            if (parent != null)
            {
                this._spmcBt.buttonStyle = ButtonStyle.Button;
                this._SpmcSelect(parent, 0, 0);
            }
        }

        public virtual void _spmcBt_OnSelectValue(object sender, EventArgs e)
        {
        }

        private void _spmcBt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && ((this._spmcBt.DataSource == null) || (this._spmcBt.DataSource.Rows.Count == 0)))
            {
                this._SpmcSelect((this._DataGridView_qd == null) ? this._DataGridView : this._DataGridView_qd, 1, 0);
            }
        }

        protected virtual void _spmcBt_SetAutoComplateHead()
        {
            this._spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("商品名称", "MC", 160));
            this._spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("规格型号", "GGXH", 60));
            this._spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("计量单位", "JLDW", 60));
            this._spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("税率", "SLV", 40));
            this._spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("单价", "DJ", 60));
            this._spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("含税价标志", "HSJBZSTR", 70));
            this._spmcBt.ShowText = "MC";
            this._spmcBt.DrawHead = true;
            this._spmcBt.AutoIndex = 0;
        }

        protected virtual void _spmcBt_TextChanged(object sender, EventArgs e)
        {
            CustomStyleDataGrid grid = null;
            if (this._DataGridView_qd == null)
            {
                grid = this._DataGridView;
            }
            else
            {
                grid = this._DataGridView_qd;
            }
            int index = grid.CurrentRow.Index;
            string str = this._GetSpmcText();
            bool flag = false;
            if ((str.Trim() == "") || (str.Trim() == "0"))
            {
                str = "";
            }
            flag = this._fpxx.SetSpmc(index, str);
            this._ShowDataGrid(grid, this._fpxx.GetSpxx(index), index);
            if (!flag && (!this._SpmcChangeError() || (this._fpxx.GetCode() != "A004")))
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
            }
        }

        protected virtual bool _SpmcChangeError()
        {
            return false;
        }

        protected virtual DataTable _SpmcOnAutoCompleteDataSource(CustomStyleDataGrid customStyleDataGrid_2, string string_1)
        {
            return null;
        }

        protected virtual void _SpmcOnSelectValue(CustomStyleDataGrid customStyleDataGrid_2, Dictionary<string, string> value)
        {
        }

        protected virtual void _SpmcSelect(CustomStyleDataGrid customStyleDataGrid_2, int int_0, int int_1)
        {
        }

        protected void _UpdateSLv(CustomStyleDataGrid customStyleDataGrid_2, string string_1)
        {
            string_1 = this.GetSL(string_1);
            FPLX fplx = this._fpxx.Fplx;
            string_1 = this._GetSLv(fplx, string_1, 0).DataValue;
            int rowIndex = customStyleDataGrid_2.CurrentCell.RowIndex;
            Dictionary<SPXX, string> spxx = this._fpxx.GetSpxx(rowIndex);
            if (((spxx == null) || (spxx[SPXX.FPHXZ] != "3")) && !this._fpxx.SetSLv(rowIndex, string_1))
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
            }
            this._ShowDataGrid(customStyleDataGrid_2, this._fpxx.GetSpxx(rowIndex), rowIndex);
        }

        protected virtual void _UpdateStatusOfRow(CustomStyleDataGrid customStyleDataGrid_2, int int_0)
        {
            Dictionary<SPXX, string> spxx = null;
            if (this._fpxx != null)
            {
                if ((this._DataGridView_qd == null) && this._fpxx.Qdbz)
                {
                    List<Dictionary<SPXX, string>> mxxxs = this._fpxx.GetMxxxs();
                    if ((mxxxs != null) && (mxxxs.Count != 0))
                    {
                        spxx = mxxxs[int_0];
                    }
                    else
                    {
                        spxx = null;
                    }
                }
                else
                {
                    spxx = this._fpxx.GetSpxx(int_0);
                }
                if ((spxx != null) && ((spxx[SPXX.FPHXZ].Equals(FPHXZ.XHQDZK.ToString("D")) || spxx[SPXX.FPHXZ].Equals(FPHXZ.XJXHQD.ToString("D"))) || (spxx[SPXX.FPHXZ].Equals(FPHXZ.SPXX_ZK.ToString("D")) || spxx[SPXX.FPHXZ].Equals(FPHXZ.ZKXX.ToString("D")))))
                {
                    customStyleDataGrid_2.Rows[int_0].ReadOnly = true;
                    if (this._DataGridView_qd == null)
                    {
                        this._ZhekouButton.Enabled = false;
                    }
                    else
                    {
                        this.toolStripButton_5.Enabled = false;
                    }
                }
                else
                {
                    if (this._DataGridView_qd == null)
                    {
                        this._ZhekouButton.Enabled = true;
                    }
                    else
                    {
                        this.toolStripButton_5.Enabled = true;
                    }
                    customStyleDataGrid_2.Rows[int_0].ReadOnly = false;
                    this._SetDataGridReadOnlyColumns(customStyleDataGrid_2);
                }
            }
        }

        protected virtual DataTable _XfxxOnAutoCompleteDataSource(string string_1)
        {
            return null;
        }

        protected virtual void _XfxxSelect(string string_1, int int_0)
        {
        }

        protected virtual void _XfxxSetValue(Dictionary<string, string> khxx)
        {
        }

        private void comboBox_SLV_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            CustomStyleDataGrid parent = null;
            if ((this.comboBox_SLV != null) && this.comboBox_SLV.Visible)
            {
                AisinoCMB ocmb = sender as AisinoCMB;
                if (ocmb != null)
                {
                    parent = ocmb.Parent as CustomStyleDataGrid;
                }
            }
            else
            {
                parent = sender as CustomStyleDataGrid;
            }
            if ((parent != null) && (parent.CurrentRow != null))
            {
                int index = parent.CurrentRow.Index;
                int columnIndex = parent.CurrentCell.ColumnIndex;
                int keyValue = e.KeyValue;
                if ((this.comboBox_SLV != null) && this.comboBox_SLV.Visible)
                {
                    switch (keyValue)
                    {
                        case 13:
                        case 9:
                            if ((columnIndex + 1) < parent.ColumnCount)
                            {
                                parent.CurrentCell = parent.Rows[index].Cells[columnIndex + 1];
                            }
                            parent.Focus();
                            this.comboBox_SLV.Visible = false;
                            break;
                    }
                }
                else
                {
                    int count = parent.Rows.Count;
                    int num5 = parent.Columns.Count;
                    if (this._fpxx.Fplx == FPLX.JSFP)
                    {
                        num5 -= 2;
                    }
                    if (keyValue == 40)
                    {
                        if (this._fpxx.Qdbz && (this.customStyleDataGrid_1 == null))
                        {
                            return;
                        }
                        if (((index == (count - 1)) && !this._onlyShow) && !parent.ReadOnly)
                        {
                            this._AddSpxx(-1);
                        }
                    }
                    if ((((keyValue == 9) && (index == (count - 1))) && ((columnIndex == (num5 - 1)) && !this._onlyShow)) && (!parent.ReadOnly && (!this._fpxx.Qdbz || (this.customStyleDataGrid_1 != null))))
                    {
                        this._AddSpxx(-1);
                    }
                    if (keyValue == 13)
                    {
                        if (columnIndex < (num5 - 1))
                        {
                            if ((this._fpxx.Fplx == FPLX.JSFP) && (columnIndex == 0))
                            {
                                return;
                            }
                            parent.CurrentCell = parent.Rows[index].Cells[columnIndex + 1];
                        }
                        else if (((index == (count - 1)) && !this._onlyShow) && !parent.ReadOnly)
                        {
                            if (this._AddSpxx(-1))
                            {
                                parent.CurrentCell = parent.Rows[count].Cells[0];
                            }
                        }
                        else if (index < (count - 1))
                        {
                            parent.CurrentCell = parent.Rows[index + 1].Cells[0];
                        }
                        else
                        {
                            SendKeys.Send("{TAB}");
                        }
                    }
                    if ((keyValue == 0x2e) && !parent.ReadOnly)
                    {
                        this.toolStripButton_9_Click(null, null);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_3 != null))
            {
                this.icontainer_3.Dispose();
            }
            base.Dispose(disposing);
        }

        protected string getbmbbbh()
        {
            if (this.isWM())
            {
                return "";
            }
            SPFLService service = new SPFLService();
            return service.GetMaxBMBBBH();
        }

        protected string GetSL(string string_1)
        {
            if (string_1 == "减按1.5%计算")
            {
                return "0.015";
            }
            if (string_1.EndsWith("%") && (string_1 != "0%"))
            {
                decimal num;
                if (decimal.TryParse(string_1.Substring(0, string_1.Length - 1), out num))
                {
                    string_1 = decimal.Divide(num, decimal.Parse("100")).ToString();
                }
                return string_1;
            }
            if (((string_1 == "0%") || (string_1 == "免税")) || (string_1 == "不征税"))
            {
                return "0.00";
            }
            if (string_1 == "中外合作油气田")
            {
                return "0.05";
            }
            return string_1;
        }

        public object[] GetSlvList(FPLX fplx_0)
        {
            return this.dictionary_0[fplx_0].ToArray();
        }

        public string[] GetSYSlv(string string_1, bool bool_1)
        {
            if (string_1.Length != 0x13)
            {
                return new string[0];
            }
            int num = string_1.Length - 1;
            while (string_1[num] == '0')
            {
                num--;
            }
            int count = (string_1.Length - num) - 1;
            if ((count % 2) != 0)
            {
                count--;
            }
            string_1 = string_1.Remove(0x13 - count, count);
            object[] objArray3 = new object[] { string_1 };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", objArray3);
            if ((objArray == null) || (objArray.Length == 0))
            {
                return new string[0];
            }
            DataTable table = objArray[0] as DataTable;
            if (table.Rows.Count == 0)
            {
                return new string[0];
            }
            DataRow row = table.Rows[0];
            string[] source = row["SLV"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == "1.5%_5%")
                {
                    source[i] = "1.5%";
                }
            }
            string[] strArray4 = row["YHZC_SLV"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < strArray4.Length; j++)
            {
                if (strArray4[j] == "1.5%_5%")
                {
                    strArray4[j] = "1.5%";
                }
            }
            if ((source.Length + strArray4.Length) == 0)
            {
                return new string[0];
            }
            List<string> list = new List<string>();
            list.AddRange(source);
            if (bool_1)
            {
                list.AddRange(strArray4);
            }
            string[] strArray3 = new string[list.Count];
            for (int k = 0; k < list.Count; k++)
            {
                if ((list[k].ToString().Length != 0) && !(list[k].ToString() == ""))
                {
                    strArray3[k] = list[k].ToString().Remove(list[k].ToString().Length - 1);
                    strArray3[k] = (decimal.Parse(strArray3[k]) / 100M).ToString();
                }
            }
            return strArray3;
        }

        protected void Initialize(byte[] byte_0)
        {
            this.method_3();
            base.KeyPreview = true;
            base.KeyDown += new KeyEventHandler(this._InvoiceForm_KeyDown);
            if (!this._onlyShow)
            {
                this._SetSlvList();
                this.comboBox_SLV = new AisinoCMB();
                this.comboBox_SLV.Name = "SLCB";
                FPLX fplx = this._fpxx.Fplx;
                this.comboBox_SLV.Items.AddRange(this.GetSlvList(fplx));
                this.comboBox_SLV.SelectedItem = this._GetSLv(fplx, this._SetDefaultSpsLv(), 0);
                this.comboBox_SLV.Visible = false;
                this.comboBox_SLV.PreviewKeyDown += new PreviewKeyDownEventHandler(this.comboBox_SLV_PreviewKeyDown);
                this.comboBox_SLV.KeyPress += new KeyPressEventHandler(this._comboBox_SLV_KeyPress);
                this.comboBox_SLV.SelectedIndexChanged += new EventHandler(this._comboBox_SLV_SelectedIndexChanged);
                this.comboBox_SLV.VisibleChanged += new EventHandler(this._comboBox_SLV_VisibleChanged);
                if (this._comboBox_SLV_ReadOnly())
                {
                    this.comboBox_SLV.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                this._spmcBt = new AisinoMultiCombox();
                this._spmcBt.IsSelectAll = true;
                this._spmcBt.Name = "SPMCBT";
                this._spmcBt.Text = "";
                this._spmcBt.Padding = new Padding(0);
                this._spmcBt.Margin = new Padding(0);
                this._spmcBt.Visible = false;
                this._spmcBt_SetAutoComplateHead();
                this._spmcBt.AutoComplate = AutoComplateStyle.HeadWork;
                this._spmcBt.OnAutoComplate += new EventHandler(this._spmcBt_OnAutoComplate);
                this._spmcBt.buttonStyle = ButtonStyle.Button;
                this._spmcBt.OnButtonClick += new EventHandler(this._spmcBt_OnButtonClick);
                this._spmcBt.MouseDoubleClick += new MouseEventHandler(this._spmcBt_MouseDoubleClick);
                this._spmcBt.OnTextChanged = (EventHandler) Delegate.Combine(this._spmcBt.OnTextChanged, new EventHandler(this._spmcBt_TextChanged));
                this._spmcBt.OnSelectValue += new EventHandler(this._spmcBt_OnSelectValue);
                this._spmcBt.PreviewKeyDown += new PreviewKeyDownEventHandler(this._spmcBt_PreviewKeyDown);
                this._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
                base.FormClosing += new FormClosingEventHandler(this._InvoiceForm_FormClosing);
                FormMain.UpdateUserNameEvent += new FormMain.UpdateUserNameDelegate(this._FormMain_UpdateUserNameEvent);
            }
            base.Resize += new EventHandler(this._InvoiceForm_Resize);
        }

        public bool isWM()
        {
            return (TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") != "FLBM");
        }

        public bool isXT(string string_1)
        {
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("MC", string_1);
            return (baseDAOSQLite.querySQL("aisino.fwkp.fptk.selectXThash", parameter).Count > 0);
        }

        private void method_10(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            this._XfxxSelect(combox.Text, 0);
        }

        private void method_11(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                text = combox.Text;
                DataTable table = this._XfxxOnAutoCompleteDataSource(text);
                if (table != null)
                {
                    combox.DataSource = table;
                }
            }
        }

        private void method_12(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> selectDict = combox.SelectDict;
                this._XfxxSetValue(selectDict);
            }
        }

        private void method_13(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            this._GfxxSelect(combox.Text, 0);
        }

        private void method_14(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                text = combox.Text;
                DataTable table = this._GfxxOnAutoCompleteDataSource(text);
                if (table != null)
                {
                    combox.DataSource = table;
                }
            }
        }

        private void method_15(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> selectDict = combox.SelectDict;
                this._GfxxSetValue(selectDict);
            }
        }

        private void method_3()
        {
            this._xmlComponentLoader = new XmlComponentLoader();
            base.SuspendLayout();
            this._xmlComponentLoader.Dock = DockStyle.Fill;
            this._xmlComponentLoader.Location = new Point(0, 0);
            this._xmlComponentLoader.Dock = DockStyle.Fill;
            this._xmlComponentLoader.Name = "xmlComponentLoader";
            this._xmlComponentLoader.AutoScroll = true;
            if (this._fpxx.Fplx.ToString() == "JSFP")
            {
                this._xmlComponentLoader.AutoScrollMinSize = new Size(550, 460);
            }
            else
            {
                this._xmlComponentLoader.AutoScrollMinSize = new Size(780, 460);
            }
            if (!this.string_0.Equals(string.Empty))
            {
                this._xmlComponentLoader.XMLPath = this.string_0;
            }
            this._xmlComponentLoader.Size = new Size(200, 200);
            this._xmlComponentLoader.TabIndex = 0;
            if (this._fpxx.Fplx.ToString() == "JSFP")
            {
                this.MinimumSize = new Size(550, 540);
                base.Size = new Size(570, 740);
            }
            else
            {
                this.MinimumSize = new Size(830, 540);
                base.Size = new Size(980, 740);
            }
            base.Controls.Add(this._xmlComponentLoader);
            base.Name = "InvoiceForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.ResumeLayout(false);
        }

        private void method_4(CustomStyleDataGrid customStyleDataGrid_2)
        {
            customStyleDataGrid_2.RowHeadersWidth = 0x19;
            for (int i = 0; i < customStyleDataGrid_2.Columns.Count; i++)
            {
                customStyleDataGrid_2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            customStyleDataGrid_2.AllowUserToAddRows = false;
            customStyleDataGrid_2.AllowUserToDeleteRows = false;
            if (this._onlyShow)
            {
                customStyleDataGrid_2.ReadOnly = true;
                customStyleDataGrid_2.GridStyle = CustomStyle.invWare;
            }
            else
            {
                this._SetDataGridReadOnlyColumns(customStyleDataGrid_2);
                customStyleDataGrid_2.StandardTab = false;
                customStyleDataGrid_2.PreviewKeyDown += new PreviewKeyDownEventHandler(this.comboBox_SLV_PreviewKeyDown);
                customStyleDataGrid_2.CellEndEdit += new DataGridViewCellEventHandler(this._dataGridView_CellEndEdit);
                customStyleDataGrid_2.CellMouseDown += new DataGridViewCellMouseEventHandler(this.method_7);
                customStyleDataGrid_2.CurrentCellChanged += new EventHandler(this._dataGridView_CurrentCellChanged);
                customStyleDataGrid_2.RowEnter += new DataGridViewCellEventHandler(this.method_8);
                customStyleDataGrid_2.RowsAdded += new DataGridViewRowsAddedEventHandler(this._dataGridView_RowsAdded);
                customStyleDataGrid_2.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this._dataGridView_RowsRemoved);
                customStyleDataGrid_2.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.method_9);
                customStyleDataGrid_2.CSDGridColumnWidthChanged += new CustomStyleDataGrid.CSDGridColumnWidthChangedHandler(this.method_6);
                customStyleDataGrid_2.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.method_5);
                if (this._fpxx != null)
                {
                    this._ShowDataGrid(customStyleDataGrid_2, this._fpxx.GetSpxx(0), 0);
                    int count = customStyleDataGrid_2.Rows.Count;
                }
            }
        }

        private void method_5(object sender, DataGridViewColumnEventArgs e)
        {
            if (this._spmcBt.Visible)
            {
                this._spmcBt.Visible = false;
            }
            if (this.comboBox_SLV.Visible)
            {
                this.comboBox_SLV.Visible = false;
            }
        }

        private void method_6(DataGridViewColumnEventArgs dataGridViewColumnEventArgs_0)
        {
            DataGridView dataGridView = dataGridViewColumnEventArgs_0.Column.DataGridView;
            DataGridViewColumn column = dataGridViewColumnEventArgs_0.Column;
            AisinoMultiCombox combox = dataGridView.Controls["SPMCBT"] as AisinoMultiCombox;
            if ((column != null) && column.Name.Equals("SPMC"))
            {
                int index = column.Index;
                int rowIndex = dataGridView.CurrentCell.RowIndex;
                Rectangle rectangle = dataGridView.GetCellDisplayRectangle(index, rowIndex, false);
                if (combox != null)
                {
                    combox.Left = rectangle.Left;
                    combox.Top = rectangle.Top;
                    combox.Width = rectangle.Width;
                    combox.Height = rectangle.Height;
                }
            }
        }

        private void method_7(object sender, DataGridViewCellMouseEventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            if (columnIndex == -1)
            {
                if (rowIndex >= 0)
                {
                    grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    grid.Rows[rowIndex].Selected = true;
                }
            }
            else
            {
                grid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
        }

        private void method_8(object sender, DataGridViewCellEventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            this._UpdateStatusOfRow(grid, e.RowIndex);
        }

        private void method_9(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            this.dataGridViewTextBoxEditingControl_0 = (DataGridViewTextBoxEditingControl) e.Control;
            this.dataGridViewTextBoxEditingControl_0.KeyPress += new KeyPressEventHandler(this._EditingControl_KeyPress);
        }

        protected virtual void QingdanFormClosing(object sender, FormClosingEventArgs e)
        {
            this.cellEndEdit = true;
            this._CommitEditGrid();
            if (!this.cellEndEdit)
            {
                e.Cancel = true;
            }
            else
            {
                List<Dictionary<SPXX, string>> spxxs = this._fpxx.GetSpxxs();
                if ((spxxs.Count == 1) && (((((((spxxs[0][SPXX.SPMC].Length + spxxs[0][SPXX.GGXH].Length) + spxxs[0][SPXX.JLDW].Length) + spxxs[0][SPXX.SL].Length) + spxxs[0][SPXX.DJ].Length) + spxxs[0][SPXX.JE].Length) + spxxs[0][SPXX.SE].Length) == 0))
                {
                    this._fpxx.DelSpxxAll();
                    spxxs.Clear();
                }
                if (!this._fpxx.CanAddSpxx(1, false))
                {
                    MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                    e.Cancel = true;
                }
                else if (this.QdToMx && (spxxs.Count > (this._fpxx.Hzfw ? 7 : 8)))
                {
                    MessageManager.ShowMsgBox("A094");
                    e.Cancel = true;
                }
                else
                {
                    this._DataGridView.Rows.Clear();
                    if (this._spmcBt != null)
                    {
                        this._spmcBt.Visible = false;
                        this._DataGridView.Controls.Add(this._spmcBt);
                        while ((this._DataGridView.Rows.Count - 1) < 0)
                        {
                            this._DataGridView.Rows.Add();
                        }
                    }
                    this._SetDataGridReadOnlyColumns(this._DataGridView);
                    if ((spxxs.Count > 0) && !this.QdToMx)
                    {
                        this._ShowDataGridMxxx(this._DataGridView);
                    }
                    else
                    {
                        bool flag = this._fpxx.SetQdbz(false);
                        qingdanflag = false;
                        if (!flag)
                        {
                            e.Cancel = true;
                            MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                        }
                        else
                        {
                            if (spxxs.Count == 0)
                            {
                                this._fpxx.AddSpxx(this._SetDefaultSpsm(), this._SetDefaultSpsLv(), this._fpxx.Zyfplx);
                            }
                            this._DataGridView.ReadOnly = false;
                            this._ShowDataGrid(this._DataGridView);
                            if (this._DataGridView.RowCount > 0)
                            {
                                if (this._fpxx.Fplx == FPLX.JSFP)
                                {
                                    this._DataGridView.CurrentCell = this._DataGridView.Rows[this._DataGridView.RowCount - 1].Cells[this._DataGridView.Columns.Count - 5];
                                }
                                else
                                {
                                    this._DataGridView.CurrentCell = this._DataGridView.Rows[this._DataGridView.RowCount - 1].Cells[this._DataGridView.Columns.Count - 1];
                                }
                            }
                            this._AddRowButton.Enabled = true;
                            this._ZhekouButton.Enabled = true;
                            this._DelRowButton.Enabled = true;
                        }
                    }
                }
            }
        }

        public void reset_fpxx(bool bool_1)
        {
            List<Dictionary<SPXX, string>> spxxs = this._fpxx.GetSpxxs();
            for (int i = 0; i < spxxs.Count; i++)
            {
                if (spxxs[i][SPXX.LSLVBS] == "0")
                {
                    this._fpxx.SetYhsm(i, "出口零税");
                    continue;
                }
                if (spxxs[i][SPXX.LSLVBS] == "1")
                {
                    this._fpxx.SetYhsm(i, "免税");
                    continue;
                }
                if (spxxs[i][SPXX.LSLVBS] == "2")
                {
                    this._fpxx.SetYhsm(i, "不征税");
                    continue;
                }
                if (!(spxxs[i][SPXX.XSYH].ToString() == "1"))
                {
                    goto Label_0172;
                }
                string[] strArray2 = spxxs[i][SPXX.YHSM].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                bool flag = false;
                int index = 0;
                while (index < strArray2.Length)
                {
                    if (this.yhzc_contain_slv(strArray2[index], spxxs[i][SPXX.SLV], false, bool_1))
                    {
                        goto Label_0132;
                    }
                    index++;
                }
                goto Label_0146;
            Label_0132:
                this._fpxx.SetYhsm(i, strArray2[index]);
                flag = true;
            Label_0146:
                if (flag)
                {
                    this._fpxx.SetXsyh(i, "1");
                }
                else
                {
                    this._fpxx.SetXsyh(i, "0");
                }
                continue;
            Label_0172:
                this._fpxx.SetYhsm(i, "");
            }
        }

        private void toolStripButton_0_Click(object sender, EventArgs e)
        {
            if (this._DataGridView_qd == null)
            {
                this._DataGridView.Statistics(this);
            }
            else
            {
                this._DataGridView_qd.Statistics(this);
            }
        }

        private void toolStripButton_2_MouseDown(object sender, MouseEventArgs e)
        {
            this._CommitEditGrid();
        }

        private void toolStripButton_4_Click(object sender, EventArgs e)
        {
            Dictionary<SPXX, string> dictionary;
            if (this.toolStripButton_4.Checked)
            {
                if (this._fpxx.GetSpxxs().Count > 1)
                {
                    MessageManager.ShowMsgBox("INP-242187", new string[] { "明细行超过两行的差额税发票不能转化成普通发票！" });
                }
                else
                {
                    this._fpxx.SetZyfpLx(ZYFP_LX.ZYFP);
                    if (this._fpxx.GetSpxxs().Count == 1)
                    {
                        this._fpxx.SetSL(0, "");
                        this._fpxx.SetDj(0, "");
                        this._fpxx.SetJe(0, "");
                        this._fpxx.SetKce(0, "0");
                        this._ShowDataGridMxxx(this._DataGridView);
                    }
                    this.toolStripButton_4.Checked = false;
                    this._QingdanButton.Enabled = true;
                }
                return;
            }
            if ((this._fpxx.GetSpxxs().Count > 1) || this._fpxx.Qdbz)
            {
                MessageManager.ShowMsgBox("INP-242187", new string[] { "清单发票或者明细行超过两行不能转化成差额税发票！" });
                return;
            }
            if (this._fpxx.GetSpxxs().Count != 0)
            {
                goto Label_012C;
            }
            bool flag = false;
            string s = "0.17";
            if (this.comboBox_SLV.Items.Count <= 1)
            {
                goto Label_010E;
            }
            IEnumerator enumerator = this.comboBox_SLV.Items.GetEnumerator();
            {
                double num;
                double num2;
                while (enumerator.MoveNext())
                {
                    SLV current = (SLV) enumerator.Current;
                    if (!(current.ToString() == ""))
                    {
                        s = current.ToString();
                        s = s.Remove(s.Length - 1);
                        if (double.TryParse(s, out num))
                        {
                            goto Label_00D2;
                        }
                    }
                }
                goto Label_0103;
            Label_00D2:
                num2 = num / 100.0;
                s = num2.ToString();
                flag = true;
            }
        Label_0103:
            if (!flag)
            {
                s = "0.17";
            }
        Label_010E:
            this._fpxx.AddSpxx("", s, this._fpxx.Zyfplx);
        Label_012C:
            dictionary = this._fpxx.GetSpxx(0);
            if (((dictionary != null) && (dictionary[SPXX.SPMC].ToString() != "")) && this.isXT(dictionary[SPXX.SPMC].ToString()))
            {
                MessageManager.ShowMsgBox("INP-242187", new string[] { "稀土商品不能填开差额税发票！" });
            }
            else if (!this._fpxx.SetZyfpLx(ZYFP_LX.CEZS))
            {
                MessageManager.ShowMsgBox("INP-242187", new string[] { "特殊税率商品明细不能开具差额税发票！" });
            }
            else
            {
                ChaE_Tax tax = new ChaE_Tax();
                if (tax.ShowDialog() == DialogResult.OK)
                {
                    this._fpxx.Hsjbz = true;
                    this._fpxx.SetSL(0, "");
                    this._fpxx.SetDj(0, "");
                    this._fpxx.SetJe(0, "");
                    string str = tax.kce.ToString("F2");
                    this._fpxx.SetKce(0, str);
                    this._DataGridView.Columns["DJ"].HeaderText = "单价(含税)";
                    this._DataGridView.Columns["JE"].HeaderText = "金额(含税)";
                    this._HsjbzButton.Checked = true;
                    this._ShowDataGridMxxx(this._DataGridView);
                    this.toolStripButton_4.Checked = true;
                    this._QingdanButton.Enabled = false;
                }
                else
                {
                    this._fpxx.SetZyfpLx(ZYFP_LX.ZYFP);
                }
            }
        }

        private void toolStripButton_5_Click(object sender, EventArgs e)
        {
            CustomStyleDataGrid grid = (this._DataGridView_qd == null) ? this._DataGridView : this._DataGridView_qd;
            bool flag = this._DataGridView_qd == null;
            DataGridViewRow currentRow = grid.CurrentRow;
            if (currentRow != null)
            {
                if (this._fpxx.CanAddSpxx(1, true))
                {
                    int index = currentRow.Index;
                    if (Convert.ToDouble(this._fpxx.GetZkSpJe(index, 1)) == 0.0)
                    {
                        MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                    }
                    else
                    {
                        ZheKou kou = new ZheKou(this._fpxx, index, flag);
                        if (kou.ShowDialog() == DialogResult.OK)
                        {
                            grid.Rows.Add();
                            this._ShowDataGrid(grid);
                            if (this._fpxx.Zyfplx == ZYFP_LX.CEZS)
                            {
                                this._QingdanButton.Enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                }
            }
        }

        private void toolStripButton_7_Click(object sender, EventArgs e)
        {
            this._spmcBt.OnTextChanged = (EventHandler) Delegate.Remove(this._spmcBt.OnTextChanged, new EventHandler(this._spmcBt_TextChanged));
            this._spmcBt.Leave -= new EventHandler(this._spmcBt_leave);
            try
            {
                if (!this._fpxx.CanAddSpxx(1, false))
                {
                    MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                }
                else
                {
                    string str = "0";
                    if (((this._fpxx.Fplx == FPLX.PTFP) || (this._fpxx.Fplx == FPLX.ZYFP)) && this.toolStripButton_4.Checked)
                    {
                        ChaE_Tax tax = new ChaE_Tax();
                        if (tax.ShowDialog() == DialogResult.OK)
                        {
                            str = tax.kce.ToString("F2");
                        }
                        else
                        {
                            return;
                        }
                    }
                    CustomStyleDataGrid grid = (this._DataGridView_qd == null) ? this._DataGridView : this._DataGridView_qd;
                    DataGridViewSelectedRowCollection selectedRows = grid.SelectedRows;
                    int index = -1;
                    if (selectedRows.Count > 0)
                    {
                        index = grid.SelectedRows[0].Index;
                        this._AddSpxx(index);
                    }
                    else if ((grid.Rows.Count > 0) && (grid.CurrentCell.RowIndex != (grid.Rows.Count - 1)))
                    {
                        index = grid.CurrentCell.RowIndex;
                        this._AddSpxx(index);
                    }
                    else if (this._AddSpxx(-1))
                    {
                        index = grid.Rows.Count - 1;
                        if (index == -1)
                        {
                            index = 0;
                        }
                    }
                    if (index > -1)
                    {
                        grid.CurrentCell = grid.Rows[index].Cells[0];
                        if (((this._fpxx.Fplx == FPLX.PTFP) || (this._fpxx.Fplx == FPLX.ZYFP)) && this.toolStripButton_4.Checked)
                        {
                            this._fpxx.SetKce(index, str);
                        }
                    }
                    if (this._fpxx.Zyfplx == ZYFP_LX.CEZS)
                    {
                        this.toolStripButton_10.Enabled = false;
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this._spmcBt.OnTextChanged = (EventHandler) Delegate.Combine(this._spmcBt.OnTextChanged, new EventHandler(this._spmcBt_TextChanged));
                this._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
            }
        }

        private void toolStripButton_9_Click(object sender, EventArgs e)
        {
            this._spmcBt.OnTextChanged = (EventHandler) Delegate.Remove(this._spmcBt.OnTextChanged, new EventHandler(this._spmcBt_TextChanged));
            this._spmcBt.Leave -= new EventHandler(this._spmcBt_leave);
            try
            {
                CustomStyleDataGrid grid = (this._DataGridView_qd == null) ? this._DataGridView : this._DataGridView_qd;
                if ((grid.CurrentCell != null) && this._CheckDelSPRow(grid))
                {
                    if (this._spmcBt.Visible)
                    {
                        this._spmcBt.Visible = false;
                    }
                    if (this.comboBox_SLV.Visible)
                    {
                        this.comboBox_SLV.Visible = false;
                    }
                    int rowIndex = grid.CurrentCell.RowIndex;
                    if (((this._DataGridView_qd == null) && !this._fpxx.Qdbz) || (this._DataGridView_qd != null))
                    {
                        if (this._fpxx.DelSpxx(rowIndex))
                        {
                            grid.Rows.Clear();
                            this._ShowDataGrid(grid);
                            if (grid.Rows.Count > 0)
                            {
                                if (this._fpxx.Fplx == FPLX.JSFP)
                                {
                                    grid.CurrentCell = grid.Rows[0].Cells[this.customStyleDataGrid_0.Columns.Count - 5];
                                }
                                else
                                {
                                    grid.CurrentCell = grid.Rows[0].Cells[this.customStyleDataGrid_0.Columns.Count - 1];
                                }
                            }
                        }
                        else
                        {
                            MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                        }
                    }
                    else if (this._fpxx.Qdbz)
                    {
                        if (this._fpxx.DelSpxxAll())
                        {
                            this._fpxx.SetQdbz(false);
                            grid.Rows.Clear();
                            this._DataGridView.ReadOnly = false;
                            this._SetDataGridReadOnlyColumns(this._DataGridView);
                            if (this._DataGridView.CurrentRow != null)
                            {
                                this._ZhekouButton.Enabled = true;
                            }
                            this._AddRowButton.Enabled = true;
                        }
                        else
                        {
                            MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                        }
                    }
                    if (grid.RowCount > 0)
                    {
                        if (this._fpxx.Fplx == FPLX.JSFP)
                        {
                            grid.CurrentCell = grid[grid.ColumnCount - 5, grid.RowCount - 1];
                        }
                        else
                        {
                            grid.CurrentCell = grid[grid.ColumnCount - 1, grid.RowCount - 1];
                        }
                    }
                    this._SetHzxx();
                    if (this._fpxx.Zyfplx == ZYFP_LX.CEZS)
                    {
                        this.toolStripButton_10.Enabled = false;
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this._spmcBt.OnTextChanged = (EventHandler) Delegate.Combine(this._spmcBt.OnTextChanged, new EventHandler(this._spmcBt_TextChanged));
                this._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
            }
        }

        private void toolStripButton_9_MouseDown(object sender, MouseEventArgs e)
        {
            this._CommitEditGrid();
        }

        public bool Update_SP(object[] object_0)
        {
            try
            {
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                parameter.Add("BM", object_0[11].ToString());
                ArrayList list = baseDAOSQLite.querySQL("aisino.fwkp.fptk.SelectYHZCS", parameter);
                object_0[12] = "否";
                bool flag = object_0[10].ToString() == "True";
                foreach (Dictionary<string, object> dictionary2 in list)
                {
                    string[] strArray2 = dictionary2["ZZSTSGL"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                    int index = 0;
                    while (index < strArray2.Length)
                    {
                        if (this.yhzc_contain_slv(strArray2[index], object_0[3].ToString(), false, flag))
                        {
                            goto Label_00D1;
                        }
                        index++;
                    }
                    continue;
                Label_00D1:
                    object_0[12] = "是";
                    object_0[15] = strArray2[index];
                }
                parameter.Clear();
                parameter.Add("YHZC", object_0[12].ToString());
                parameter.Add("YHZCMC", object_0[15].ToString());
                parameter.Add("BM", object_0[0].ToString());
                parameter.Add("MC", object_0[1].ToString());
                string str = "";
                if (object_0[15].ToString() == "免税")
                {
                    str = "1";
                }
                else if (object_0[15].ToString() == "不征税")
                {
                    str = "2";
                }
                else if (object_0[15].ToString() == "出口零税")
                {
                    str = "0";
                }
                parameter.Add("LSLVBS", str);
                baseDAOSQLite.未确认DAO方法2_疑似updateSQL("aisino.fwkp.fptk.UpdataBM_SP", parameter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool yhzc_contain_slv(string string_1, string string_2, bool bool_1, bool bool_2)
        {
            if (string_2 == "减按1.5%计算")
            {
                string_2 = "1.5%";
            }
            string str = "aisino.fwkp.fptk.SelectYhzcs";
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            ArrayList list = baseDAOSQLite.querySQL(str, parameter);
            if (bool_2)
            {
                foreach (Dictionary<string, object> dictionary in list)
                {
                    if ((dictionary["YHZCMC"].ToString() == string_1) && (dictionary["SLV"].ToString() == ""))
                    {
                        return true;
                    }
                }
                return false;
            }
            if (!(string_2 == "免税") && !(string_2 == "不征税"))
            {
                if (!bool_1)
                {
                    string_2 = ((double.Parse(string_2) * 100.0)).ToString() + "%";
                }
            }
            else
            {
                string_2 = "0%";
            }
            foreach (Dictionary<string, object> dictionary3 in list)
            {
                string[] source = dictionary3["SLV"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] == "1.5%_5%")
                    {
                        source[i] = "1.5%";
                    }
                }
                if ((dictionary3["YHZCMC"].ToString() == string_1) && (dictionary3["SLV"].ToString() == ""))
                {
                    return true;
                }
                if ((dictionary3["YHZCMC"].ToString() == string_1))
                {
                    foreach(string tmpString in source)
                        if(str == string_2)return true;
                }
            }
            return false;
        }

        public List<string> yhzc2slv(string string_1)
        {
            List<string> list = new List<string>();
            if (string_1 != "")
            {
                string str = "aisino.fwkp.fptk.SelectYhzcs";
                IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                foreach (Dictionary<string, object> dictionary2 in baseDAOSQLite.querySQL(str, parameter))
                {
                    if (dictionary2["YHZCMC"].ToString() == string_1)
                    {
                        string[] strArray = dictionary2["SLV"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (strArray[i] == "1.5%_5%")
                            {
                                strArray[i] = "1.5%";
                            }
                            string item = (double.Parse(strArray[i].Remove(strArray[i].Length - 1)) / 100.0).ToString();
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        protected ToolStripButton _AddRowButton
        {
            get
            {
                return this.toolStripButton_6;
            }
            set
            {
                this.toolStripButton_6 = value;
                if (this._onlyShow)
                {
                    this.toolStripButton_6.Visible = false;
                }
                else if (value != null)
                {
                    this.toolStripButton_6.ToolTipText = "添加商品明细";
                    this.toolStripButton_6.Click += new EventHandler(this.toolStripButton_7_Click);
                    this.toolStripButton_6.MouseDown += new MouseEventHandler(this.toolStripButton_9_MouseDown);
                }
            }
        }

        protected ToolStripButton _ChaeButton
        {
            get
            {
                return this.toolStripButton_4;
            }
            set
            {
                this.toolStripButton_4 = value;
                if (this._onlyShow)
                {
                    this.toolStripButton_4.Visible = false;
                }
                else if (value != null)
                {
                    this.toolStripButton_4.ToolTipText = "差额征税";
                    this.toolStripButton_4.Click += new EventHandler(this.toolStripButton_4_Click);
                }
            }
        }

        protected CustomStyleDataGrid _DataGridView
        {
            get
            {
                return this.customStyleDataGrid_0;
            }
            set
            {
                this.customStyleDataGrid_0 = value;
                if (this.customStyleDataGrid_0 != null)
                {
                    this.method_4(this.customStyleDataGrid_0);
                    if (!this._onlyShow)
                    {
                        this._DataGridView.Controls.Add(this.comboBox_SLV);
                        this._DataGridView.Controls.Add(this._spmcBt);
                    }
                }
            }
        }

        protected CustomStyleDataGrid _DataGridView_qd
        {
            get
            {
                return this.customStyleDataGrid_1;
            }
            set
            {
                this.customStyleDataGrid_1 = value;
                if (this.customStyleDataGrid_1 != null)
                {
                    this.method_4(this.customStyleDataGrid_1);
                }
            }
        }

        protected ToolStripButton _DelRowButton
        {
            get
            {
                return this.toolStripButton_8;
            }
            set
            {
                this.toolStripButton_8 = value;
                if (this._onlyShow)
                {
                    this.toolStripButton_8.Visible = false;
                }
                else if (value != null)
                {
                    this.toolStripButton_8.ToolTipText = "删除商品明细";
                    this.toolStripButton_8.Click += new EventHandler(this.toolStripButton_9_Click);
                    this.toolStripButton_8.MouseDown += new MouseEventHandler(this.toolStripButton_9_MouseDown);
                }
            }
        }

        protected ToolStripButton _HsjbzButton
        {
            get
            {
                return this.toolStripButton_1;
            }
            set
            {
                this.toolStripButton_1 = value;
                if (value != null)
                {
                    this.toolStripButton_1.ToolTipText = "转换含税价标志";
                    this.toolStripButton_1.Click += new EventHandler(this._hsjbzButton_Click);
                    this.toolStripButton_1.MouseDown += new MouseEventHandler(this.toolStripButton_2_MouseDown);
                    this.toolStripButton_1.CheckOnClick = true;
                }
            }
        }

        protected ToolStripButton _QingdanButton
        {
            get
            {
                return this.toolStripButton_10;
            }
            set
            {
                this.toolStripButton_10 = value;
                this.toolStripButton_10.ToolTipText = "销货清单";
                this.toolStripButton_10.Click += new EventHandler(this._qingdanButton_Click);
            }
        }

        protected ToolStripButton _StatisticButton
        {
            set
            {
                this.toolStripButton_0 = value;
                if (value != null)
                {
                    this.toolStripButton_0.Visible = false;
                    this.toolStripButton_0.Click += new EventHandler(this.toolStripButton_0_Click);
                }
            }
        }

        protected ToolStripButton _ZhekouButton
        {
            get
            {
                return this.toolStripButton_3;
            }
            set
            {
                this.toolStripButton_3 = value;
                if (this._onlyShow)
                {
                    this.toolStripButton_3.Visible = false;
                }
                else if (value != null)
                {
                    this.toolStripButton_3.ToolTipText = "添加折扣";
                    this.toolStripButton_3.Click += new EventHandler(this.toolStripButton_5_Click);
                    this.toolStripButton_3.MouseDown += new MouseEventHandler(this.toolStripButton_9_MouseDown);
                }
            }
        }

        protected string XmlComponentFile
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
                if (this._xmlComponentLoader != null)
                {
                    this._xmlComponentLoader.XMLPath = this.XmlComponentFile;
                }
            }
        }
    }
}

